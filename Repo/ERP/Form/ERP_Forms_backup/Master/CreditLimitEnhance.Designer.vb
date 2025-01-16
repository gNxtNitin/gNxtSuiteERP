Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmCreditLimitEnhance
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

    'InventoryGST.Master.Show
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
Public WithEvents txtAppDate As System.Windows.Forms.TextBox
Public WithEvents txtCreditLimit As System.Windows.Forms.TextBox
    Public WithEvents txtReason As System.Windows.Forms.TextBox
    Public WithEvents txtAuthorityName As System.Windows.Forms.TextBox
Public WithEvents txtRequestName As System.Windows.Forms.TextBox
Public WithEvents txtSupplier As System.Windows.Forms.TextBox
Public WithEvents Label15 As System.Windows.Forms.Label
Public WithEvents Label14 As System.Windows.Forms.Label
Public WithEvents Label2 As System.Windows.Forms.Label
Public WithEvents Label1 As System.Windows.Forms.Label
Public WithEvents Label8 As System.Windows.Forms.Label
Public WithEvents lblSupplierName As System.Windows.Forms.Label
Public WithEvents _Label3_0 As System.Windows.Forms.Label
Public WithEvents FraFront As System.Windows.Forms.GroupBox
Public WithEvents AdoDCMain As VB6.ADODC
Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
Public WithEvents cmdAdd As System.Windows.Forms.Button
Public WithEvents cmdModify As System.Windows.Forms.Button
Public WithEvents cmdSave As System.Windows.Forms.Button
Public WithEvents cmdDelete As System.Windows.Forms.Button
Public WithEvents CmdView As System.Windows.Forms.Button
Public WithEvents cmdPrint As System.Windows.Forms.Button
Public WithEvents CmdPreview As System.Windows.Forms.Button
Public WithEvents cmdSavePrint As System.Windows.Forms.Button
Public WithEvents cmdClose As System.Windows.Forms.Button
Public WithEvents Report1 As AxCrystal.AxCrystalReport
Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents Label3 As VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCreditLimitEnhance))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.txtAppDate = New System.Windows.Forms.TextBox()
        Me.txtCreditLimit = New System.Windows.Forms.TextBox()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.txtAuthorityName = New System.Windows.Forms.TextBox()
        Me.txtRequestName = New System.Windows.Forms.TextBox()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblSupplierName = New System.Windows.Forms.Label()
        Me._Label3_0 = New System.Windows.Forms.Label()
        Me.AdoDCMain = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Label3 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblCurrentLimit = New System.Windows.Forms.Label()
        Me.lblLedgerBal = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.FraFront.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(22, 12)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.TabIndex = 10
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'cmdModify
        '
        Me.cmdModify.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModify.Image = CType(resources.GetObject("cmdModify.Image"), System.Drawing.Image)
        Me.cmdModify.Location = New System.Drawing.Point(89, 12)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 11
        Me.cmdModify.Text = "&Modify"
        Me.cmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdModify, "Modify ")
        Me.cmdModify.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(156, 12)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 12
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.Location = New System.Drawing.Point(223, 12)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 13
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDelete, "Delete")
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(490, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 17
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View Listing")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(357, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 15
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(424, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 16
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(291, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 14
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(558, 12)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 18
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.lblLedgerBal)
        Me.FraFront.Controls.Add(Me.Label7)
        Me.FraFront.Controls.Add(Me.lblCurrentLimit)
        Me.FraFront.Controls.Add(Me.Label4)
        Me.FraFront.Controls.Add(Me.txtAppDate)
        Me.FraFront.Controls.Add(Me.txtCreditLimit)
        Me.FraFront.Controls.Add(Me.txtReason)
        Me.FraFront.Controls.Add(Me.txtAuthorityName)
        Me.FraFront.Controls.Add(Me.txtRequestName)
        Me.FraFront.Controls.Add(Me.txtSupplier)
        Me.FraFront.Controls.Add(Me.Label15)
        Me.FraFront.Controls.Add(Me.Label14)
        Me.FraFront.Controls.Add(Me.Label2)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Controls.Add(Me.Label8)
        Me.FraFront.Controls.Add(Me.lblSupplierName)
        Me.FraFront.Controls.Add(Me._Label3_0)
        Me.FraFront.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -6)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(655, 235)
        Me.FraFront.TabIndex = 21
        Me.FraFront.TabStop = False
        '
        'txtAppDate
        '
        Me.txtAppDate.AcceptsReturn = True
        Me.txtAppDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtAppDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAppDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAppDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAppDate.ForeColor = System.Drawing.Color.Blue
        Me.txtAppDate.Location = New System.Drawing.Point(120, 51)
        Me.txtAppDate.MaxLength = 0
        Me.txtAppDate.Name = "txtAppDate"
        Me.txtAppDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAppDate.Size = New System.Drawing.Size(81, 22)
        Me.txtAppDate.TabIndex = 4
        '
        'txtCreditLimit
        '
        Me.txtCreditLimit.AcceptsReturn = True
        Me.txtCreditLimit.BackColor = System.Drawing.SystemColors.Window
        Me.txtCreditLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCreditLimit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCreditLimit.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreditLimit.ForeColor = System.Drawing.Color.Blue
        Me.txtCreditLimit.Location = New System.Drawing.Point(120, 135)
        Me.txtCreditLimit.MaxLength = 0
        Me.txtCreditLimit.Name = "txtCreditLimit"
        Me.txtCreditLimit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCreditLimit.Size = New System.Drawing.Size(85, 22)
        Me.txtCreditLimit.TabIndex = 7
        '
        'txtReason
        '
        Me.txtReason.AcceptsReturn = True
        Me.txtReason.BackColor = System.Drawing.SystemColors.Window
        Me.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReason.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReason.Enabled = False
        Me.txtReason.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReason.ForeColor = System.Drawing.Color.Blue
        Me.txtReason.Location = New System.Drawing.Point(120, 163)
        Me.txtReason.MaxLength = 0
        Me.txtReason.Name = "txtReason"
        Me.txtReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReason.Size = New System.Drawing.Size(419, 22)
        Me.txtReason.TabIndex = 8
        Me.txtReason.Text = " "
        '
        'txtAuthorityName
        '
        Me.txtAuthorityName.AcceptsReturn = True
        Me.txtAuthorityName.BackColor = System.Drawing.SystemColors.Window
        Me.txtAuthorityName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAuthorityName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAuthorityName.Enabled = False
        Me.txtAuthorityName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAuthorityName.ForeColor = System.Drawing.Color.Blue
        Me.txtAuthorityName.Location = New System.Drawing.Point(120, 107)
        Me.txtAuthorityName.MaxLength = 0
        Me.txtAuthorityName.Name = "txtAuthorityName"
        Me.txtAuthorityName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAuthorityName.Size = New System.Drawing.Size(419, 22)
        Me.txtAuthorityName.TabIndex = 6
        Me.txtAuthorityName.Text = " "
        '
        'txtRequestName
        '
        Me.txtRequestName.AcceptsReturn = True
        Me.txtRequestName.BackColor = System.Drawing.SystemColors.Window
        Me.txtRequestName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRequestName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRequestName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequestName.ForeColor = System.Drawing.Color.Blue
        Me.txtRequestName.Location = New System.Drawing.Point(120, 79)
        Me.txtRequestName.MaxLength = 0
        Me.txtRequestName.Name = "txtRequestName"
        Me.txtRequestName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRequestName.Size = New System.Drawing.Size(419, 22)
        Me.txtRequestName.TabIndex = 5
        Me.txtRequestName.Text = " "
        '
        'txtSupplier
        '
        Me.txtSupplier.AcceptsReturn = True
        Me.txtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplier.Enabled = False
        Me.txtSupplier.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplier.ForeColor = System.Drawing.Color.Blue
        Me.txtSupplier.Location = New System.Drawing.Point(120, 23)
        Me.txtSupplier.MaxLength = 0
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplier.Size = New System.Drawing.Size(79, 22)
        Me.txtSupplier.TabIndex = 3
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(34, 56)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(87, 13)
        Me.Label15.TabIndex = 27
        Me.Label15.Text = "Approval Date :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(5, 139)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(116, 13)
        Me.Label14.TabIndex = 26
        Me.Label14.Text = "Credit Limit Enhance :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(13, 111)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(108, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Authority Given By :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(71, 168)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Reason :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(51, 83)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(70, 13)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Request By :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSupplierName
        '
        Me.lblSupplierName.BackColor = System.Drawing.SystemColors.Control
        Me.lblSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSupplierName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSupplierName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupplierName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSupplierName.Location = New System.Drawing.Point(200, 23)
        Me.lblSupplierName.Name = "lblSupplierName"
        Me.lblSupplierName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSupplierName.Size = New System.Drawing.Size(337, 19)
        Me.lblSupplierName.TabIndex = 4
        '
        '_Label3_0
        '
        Me._Label3_0.AutoSize = True
        Me._Label3_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label3_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label3_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label3_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.SetIndex(Me._Label3_0, CType(0, Short))
        Me._Label3_0.Location = New System.Drawing.Point(26, 26)
        Me._Label3_0.Name = "_Label3_0"
        Me._Label3_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label3_0.Size = New System.Drawing.Size(95, 13)
        Me._Label3_0.TabIndex = 22
        Me._Label3_0.Text = "Customer Name :"
        Me._Label3_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'AdoDCMain
        '
        Me.AdoDCMain.BackColor = System.Drawing.SystemColors.Window
        Me.AdoDCMain.CommandTimeout = 0
        Me.AdoDCMain.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.AdoDCMain.ConnectionString = Nothing
        Me.AdoDCMain.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.AdoDCMain.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdoDCMain.ForeColor = System.Drawing.SystemColors.WindowText
        Me.AdoDCMain.Location = New System.Drawing.Point(56, 106)
        Me.AdoDCMain.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.AdoDCMain.Name = "AdoDCMain"
        Me.AdoDCMain.Size = New System.Drawing.Size(313, 33)
        Me.AdoDCMain.TabIndex = 22
        Me.AdoDCMain.Text = "Adodc1"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(655, 226)
        Me.SprdView.TabIndex = 20
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdDelete)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 223)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(655, 53)
        Me.Frame3.TabIndex = 19
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 19
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(3, 198)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(112, 13)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Current Credit Limit :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCurrentLimit
        '
        Me.lblCurrentLimit.BackColor = System.Drawing.SystemColors.Control
        Me.lblCurrentLimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCurrentLimit.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCurrentLimit.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentLimit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCurrentLimit.Location = New System.Drawing.Point(120, 194)
        Me.lblCurrentLimit.Name = "lblCurrentLimit"
        Me.lblCurrentLimit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCurrentLimit.Size = New System.Drawing.Size(114, 24)
        Me.lblCurrentLimit.TabIndex = 29
        '
        'lblLedgerBal
        '
        Me.lblLedgerBal.BackColor = System.Drawing.SystemColors.Control
        Me.lblLedgerBal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLedgerBal.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLedgerBal.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLedgerBal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLedgerBal.Location = New System.Drawing.Point(377, 194)
        Me.lblLedgerBal.Name = "lblLedgerBal"
        Me.lblLedgerBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLedgerBal.Size = New System.Drawing.Size(114, 24)
        Me.lblLedgerBal.TabIndex = 31
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(260, 198)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(90, 13)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "Ledger Balance :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmCreditLimitEnhance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(655, 278)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.AdoDCMain)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCreditLimitEnhance"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Credit Limit Enhance Approval"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
    'SprdView.DataSource = CType(AdoDCMain, MSDATASRC.DataSource)
End Sub
Public Sub VB6_RemoveADODataBinding()
    SprdView.DataSource = Nothing
End Sub

    Public WithEvents lblLedgerBal As Label
    Public WithEvents Label7 As Label
    Public WithEvents lblCurrentLimit As Label
    Public WithEvents Label4 As Label
#End Region
End Class