Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSRQCEntry
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
        'Me.MDIParent = Production.Master

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
    Public WithEvents cmdResetMRR As System.Windows.Forms.Button
    Public WithEvents txtEmp As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchEmp As System.Windows.Forms.Button
    Public WithEvents chkQC As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchRef As System.Windows.Forms.Button
    Public WithEvents txtRefNo As System.Windows.Forms.TextBox
    Public WithEvents txtRefDate As System.Windows.Forms.TextBox
    Public WithEvents txtBillDate As System.Windows.Forms.TextBox
    Public WithEvents txtBillNo As System.Windows.Forms.TextBox
    Public WithEvents txtMRRDate As System.Windows.Forms.TextBox
    Public WithEvents txtMRRNo As System.Windows.Forms.TextBox
    Public WithEvents TxtSupplier As System.Windows.Forms.TextBox
    Public WithEvents cmdMRRSearch As System.Windows.Forms.Button
    Public WithEvents TxtRemarks As System.Windows.Forms.TextBox
    Public WithEvents lblDivisionCode As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblEmp As System.Windows.Forms.Label
    Public WithEvents lblDivisionName As System.Windows.Forms.Label
    Public WithEvents lblRefType As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents LblMkey As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents lblEntryDate As System.Windows.Forms.Label
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents Frasupp As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdModify As System.Windows.Forms.Button
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSRQCEntry))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdResetMRR = New System.Windows.Forms.Button()
        Me.cmdSearchEmp = New System.Windows.Forms.Button()
        Me.cmdSearchRef = New System.Windows.Forms.Button()
        Me.cmdMRRSearch = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.Frasupp = New System.Windows.Forms.GroupBox()
        Me.txtEmp = New System.Windows.Forms.TextBox()
        Me.chkQC = New System.Windows.Forms.CheckBox()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me.txtRefDate = New System.Windows.Forms.TextBox()
        Me.txtBillDate = New System.Windows.Forms.TextBox()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.txtMRRDate = New System.Windows.Forms.TextBox()
        Me.txtMRRNo = New System.Windows.Forms.TextBox()
        Me.TxtSupplier = New System.Windows.Forms.TextBox()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.lblDivisionCode = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblEmp = New System.Windows.Forms.Label()
        Me.lblDivisionName = New System.Windows.Forms.Label()
        Me.lblRefType = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblMkey = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblEntryDate = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.FraFront.SuspendLayout()
        Me.Frasupp.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdResetMRR
        '
        Me.cmdResetMRR.BackColor = System.Drawing.SystemColors.Control
        Me.cmdResetMRR.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdResetMRR.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdResetMRR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdResetMRR.Image = CType(resources.GetObject("cmdResetMRR.Image"), System.Drawing.Image)
        Me.cmdResetMRR.Location = New System.Drawing.Point(472, 12)
        Me.cmdResetMRR.Name = "cmdResetMRR"
        Me.cmdResetMRR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdResetMRR.Size = New System.Drawing.Size(27, 19)
        Me.cmdResetMRR.TabIndex = 45
        Me.cmdResetMRR.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdResetMRR, "Preview")
        Me.cmdResetMRR.UseVisualStyleBackColor = False
        '
        'cmdSearchEmp
        '
        Me.cmdSearchEmp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchEmp.Image = CType(resources.GetObject("cmdSearchEmp.Image"), System.Drawing.Image)
        Me.cmdSearchEmp.Location = New System.Drawing.Point(184, 92)
        Me.cmdSearchEmp.Name = "cmdSearchEmp"
        Me.cmdSearchEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchEmp.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchEmp.TabIndex = 13
        Me.cmdSearchEmp.TabStop = False
        Me.cmdSearchEmp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchEmp, "Search")
        Me.cmdSearchEmp.UseVisualStyleBackColor = False
        '
        'cmdSearchRef
        '
        Me.cmdSearchRef.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchRef.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchRef.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchRef.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchRef.Image = CType(resources.GetObject("cmdSearchRef.Image"), System.Drawing.Image)
        Me.cmdSearchRef.Location = New System.Drawing.Point(164, 32)
        Me.cmdSearchRef.Name = "cmdSearchRef"
        Me.cmdSearchRef.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchRef.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchRef.TabIndex = 7
        Me.cmdSearchRef.TabStop = False
        Me.cmdSearchRef.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchRef, "Search")
        Me.cmdSearchRef.UseVisualStyleBackColor = False
        '
        'cmdMRRSearch
        '
        Me.cmdMRRSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdMRRSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMRRSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMRRSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMRRSearch.Image = CType(resources.GetObject("cmdMRRSearch.Image"), System.Drawing.Image)
        Me.cmdMRRSearch.Location = New System.Drawing.Point(164, 12)
        Me.cmdMRRSearch.Name = "cmdMRRSearch"
        Me.cmdMRRSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMRRSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdMRRSearch.TabIndex = 2
        Me.cmdMRRSearch.TabStop = False
        Me.cmdMRRSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdMRRSearch, "Search")
        Me.cmdMRRSearch.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(604, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 25
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(538, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 24
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
        Me.CmdPreview.Location = New System.Drawing.Point(472, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 23
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
        Me.cmdPrint.Location = New System.Drawing.Point(405, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 22
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(339, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 21
        Me.cmdSavePrint.Text = "Save && Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.Location = New System.Drawing.Point(272, 10)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 20
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDelete, "Delete")
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(206, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 19
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdModify
        '
        Me.cmdModify.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModify.Image = CType(resources.GetObject("cmdModify.Image"), System.Drawing.Image)
        Me.cmdModify.Location = New System.Drawing.Point(139, 10)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 18
        Me.cmdModify.Text = "&Modify"
        Me.cmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdModify, "Modify ")
        Me.cmdModify.UseVisualStyleBackColor = False
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(72, 10)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.TabIndex = 0
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.Frasupp)
        Me.FraFront.Controls.Add(Me.SprdMain)
        Me.FraFront.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -4)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(907, 579)
        Me.FraFront.TabIndex = 28
        Me.FraFront.TabStop = False
        '
        'Frasupp
        '
        Me.Frasupp.BackColor = System.Drawing.SystemColors.Control
        Me.Frasupp.Controls.Add(Me.cmdResetMRR)
        Me.Frasupp.Controls.Add(Me.txtEmp)
        Me.Frasupp.Controls.Add(Me.cmdSearchEmp)
        Me.Frasupp.Controls.Add(Me.chkQC)
        Me.Frasupp.Controls.Add(Me.cmdSearchRef)
        Me.Frasupp.Controls.Add(Me.txtRefNo)
        Me.Frasupp.Controls.Add(Me.txtRefDate)
        Me.Frasupp.Controls.Add(Me.txtBillDate)
        Me.Frasupp.Controls.Add(Me.txtBillNo)
        Me.Frasupp.Controls.Add(Me.txtMRRDate)
        Me.Frasupp.Controls.Add(Me.txtMRRNo)
        Me.Frasupp.Controls.Add(Me.TxtSupplier)
        Me.Frasupp.Controls.Add(Me.cmdMRRSearch)
        Me.Frasupp.Controls.Add(Me.TxtRemarks)
        Me.Frasupp.Controls.Add(Me.lblDivisionCode)
        Me.Frasupp.Controls.Add(Me.Label3)
        Me.Frasupp.Controls.Add(Me.lblEmp)
        Me.Frasupp.Controls.Add(Me.lblDivisionName)
        Me.Frasupp.Controls.Add(Me.lblRefType)
        Me.Frasupp.Controls.Add(Me.Label2)
        Me.Frasupp.Controls.Add(Me.Label1)
        Me.Frasupp.Controls.Add(Me.LblMkey)
        Me.Frasupp.Controls.Add(Me.Label4)
        Me.Frasupp.Controls.Add(Me.Label6)
        Me.Frasupp.Controls.Add(Me.Label5)
        Me.Frasupp.Controls.Add(Me.Label15)
        Me.Frasupp.Controls.Add(Me.Label14)
        Me.Frasupp.Controls.Add(Me.lblEntryDate)
        Me.Frasupp.Controls.Add(Me.Label25)
        Me.Frasupp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frasupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frasupp.Location = New System.Drawing.Point(0, 2)
        Me.Frasupp.Name = "Frasupp"
        Me.Frasupp.Padding = New System.Windows.Forms.Padding(0)
        Me.Frasupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frasupp.Size = New System.Drawing.Size(907, 145)
        Me.Frasupp.TabIndex = 31
        Me.Frasupp.TabStop = False
        '
        'txtEmp
        '
        Me.txtEmp.AcceptsReturn = True
        Me.txtEmp.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmp.ForeColor = System.Drawing.Color.Blue
        Me.txtEmp.Location = New System.Drawing.Point(78, 92)
        Me.txtEmp.MaxLength = 0
        Me.txtEmp.Name = "txtEmp"
        Me.txtEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmp.Size = New System.Drawing.Size(105, 20)
        Me.txtEmp.TabIndex = 12
        '
        'chkQC
        '
        Me.chkQC.AutoSize = True
        Me.chkQC.BackColor = System.Drawing.SystemColors.Control
        Me.chkQC.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkQC.Enabled = False
        Me.chkQC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkQC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkQC.Location = New System.Drawing.Point(542, 114)
        Me.chkQC.Name = "chkQC"
        Me.chkQC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkQC.Size = New System.Drawing.Size(69, 18)
        Me.chkQC.TabIndex = 16
        Me.chkQC.Text = "QC Done"
        Me.chkQC.UseVisualStyleBackColor = False
        '
        'txtRefNo
        '
        Me.txtRefNo.AcceptsReturn = True
        Me.txtRefNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.ForeColor = System.Drawing.Color.Blue
        Me.txtRefNo.Location = New System.Drawing.Point(78, 32)
        Me.txtRefNo.MaxLength = 0
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefNo.Size = New System.Drawing.Size(85, 20)
        Me.txtRefNo.TabIndex = 6
        '
        'txtRefDate
        '
        Me.txtRefDate.AcceptsReturn = True
        Me.txtRefDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefDate.ForeColor = System.Drawing.Color.Blue
        Me.txtRefDate.Location = New System.Drawing.Point(230, 32)
        Me.txtRefDate.MaxLength = 0
        Me.txtRefDate.Name = "txtRefDate"
        Me.txtRefDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefDate.Size = New System.Drawing.Size(81, 20)
        Me.txtRefDate.TabIndex = 8
        '
        'txtBillDate
        '
        Me.txtBillDate.AcceptsReturn = True
        Me.txtBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillDate.Enabled = False
        Me.txtBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillDate.ForeColor = System.Drawing.Color.Blue
        Me.txtBillDate.Location = New System.Drawing.Point(230, 72)
        Me.txtBillDate.MaxLength = 0
        Me.txtBillDate.Name = "txtBillDate"
        Me.txtBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillDate.Size = New System.Drawing.Size(81, 20)
        Me.txtBillDate.TabIndex = 11
        '
        'txtBillNo
        '
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.Enabled = False
        Me.txtBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.ForeColor = System.Drawing.Color.Blue
        Me.txtBillNo.Location = New System.Drawing.Point(78, 72)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(91, 20)
        Me.txtBillNo.TabIndex = 10
        '
        'txtMRRDate
        '
        Me.txtMRRDate.AcceptsReturn = True
        Me.txtMRRDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRDate.Enabled = False
        Me.txtMRRDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRDate.ForeColor = System.Drawing.Color.Blue
        Me.txtMRRDate.Location = New System.Drawing.Point(230, 12)
        Me.txtMRRDate.MaxLength = 0
        Me.txtMRRDate.Name = "txtMRRDate"
        Me.txtMRRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRDate.Size = New System.Drawing.Size(81, 20)
        Me.txtMRRDate.TabIndex = 3
        '
        'txtMRRNo
        '
        Me.txtMRRNo.AcceptsReturn = True
        Me.txtMRRNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRNo.ForeColor = System.Drawing.Color.Blue
        Me.txtMRRNo.Location = New System.Drawing.Point(78, 12)
        Me.txtMRRNo.MaxLength = 0
        Me.txtMRRNo.Name = "txtMRRNo"
        Me.txtMRRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRNo.Size = New System.Drawing.Size(85, 20)
        Me.txtMRRNo.TabIndex = 1
        '
        'TxtSupplier
        '
        Me.TxtSupplier.AcceptsReturn = True
        Me.TxtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.TxtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtSupplier.Enabled = False
        Me.TxtSupplier.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSupplier.ForeColor = System.Drawing.Color.Blue
        Me.TxtSupplier.Location = New System.Drawing.Point(78, 52)
        Me.TxtSupplier.MaxLength = 0
        Me.TxtSupplier.Name = "TxtSupplier"
        Me.TxtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtSupplier.Size = New System.Drawing.Size(369, 20)
        Me.TxtSupplier.TabIndex = 9
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AcceptsReturn = True
        Me.TxtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtRemarks.Location = New System.Drawing.Point(78, 112)
        Me.TxtRemarks.MaxLength = 0
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtRemarks.Size = New System.Drawing.Size(369, 20)
        Me.TxtRemarks.TabIndex = 15
        '
        'lblDivisionCode
        '
        Me.lblDivisionCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblDivisionCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDivisionCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDivisionCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDivisionCode.Location = New System.Drawing.Point(562, 70)
        Me.lblDivisionCode.Name = "lblDivisionCode"
        Me.lblDivisionCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDivisionCode.Size = New System.Drawing.Size(31, 11)
        Me.lblDivisionCode.TabIndex = 44
        Me.lblDivisionCode.Text = "MKEY"
        Me.lblDivisionCode.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(13, 95)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(59, 14)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "Employee :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblEmp
        '
        Me.lblEmp.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEmp.Location = New System.Drawing.Point(210, 92)
        Me.lblEmp.Name = "lblEmp"
        Me.lblEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEmp.Size = New System.Drawing.Size(247, 19)
        Me.lblEmp.TabIndex = 14
        '
        'lblDivisionName
        '
        Me.lblDivisionName.BackColor = System.Drawing.SystemColors.Control
        Me.lblDivisionName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDivisionName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDivisionName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDivisionName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDivisionName.Location = New System.Drawing.Point(542, 12)
        Me.lblDivisionName.Name = "lblDivisionName"
        Me.lblDivisionName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDivisionName.Size = New System.Drawing.Size(205, 19)
        Me.lblDivisionName.TabIndex = 5
        '
        'lblRefType
        '
        Me.lblRefType.BackColor = System.Drawing.SystemColors.Control
        Me.lblRefType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRefType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRefType.Location = New System.Drawing.Point(616, 42)
        Me.lblRefType.Name = "lblRefType"
        Me.lblRefType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRefType.Size = New System.Drawing.Size(73, 19)
        Me.lblRefType.TabIndex = 41
        Me.lblRefType.Text = "lblRefType"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(26, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(46, 14)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Ref No :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(194, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(35, 14)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "Date :"
        '
        'LblMkey
        '
        Me.LblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.LblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblMkey.Location = New System.Drawing.Point(558, 44)
        Me.LblMkey.Name = "LblMkey"
        Me.LblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMkey.Size = New System.Drawing.Size(31, 11)
        Me.LblMkey.TabIndex = 38
        Me.LblMkey.Text = "MKEY"
        Me.LblMkey.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(20, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(52, 14)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Supplier :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(194, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(35, 14)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = "Date :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(30, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(42, 14)
        Me.Label5.TabIndex = 35
        Me.Label5.Text = "Bill No :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(194, 14)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(35, 14)
        Me.Label15.TabIndex = 34
        Me.Label15.Text = "Date :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(21, 14)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(51, 14)
        Me.Label14.TabIndex = 33
        Me.Label14.Text = "MRR No :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblEntryDate
        '
        Me.lblEntryDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblEntryDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEntryDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEntryDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntryDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEntryDate.Location = New System.Drawing.Point(314, 12)
        Me.lblEntryDate.Name = "lblEntryDate"
        Me.lblEntryDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEntryDate.Size = New System.Drawing.Size(157, 19)
        Me.lblEntryDate.TabIndex = 4
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(17, 114)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(55, 14)
        Me.Label25.TabIndex = 32
        Me.Label25.Text = "Remarks :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(4, 148)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(898, 425)
        Me.SprdMain.TabIndex = 17
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 29
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.cmdDelete)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.lblBookType)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 571)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(907, 51)
        Me.Frame3.TabIndex = 26
        Me.Frame3.TabStop = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(4, 18)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(33, 17)
        Me.lblBookType.TabIndex = 30
        Me.lblBookType.Text = "lblBookType"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(907, 565)
        Me.SprdView.TabIndex = 29
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(31, 11)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "MKEY"
        Me.Label7.Visible = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(314, 50)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(46, 14)
        Me.Label18.TabIndex = 27
        Me.Label18.Text = "Supplier"
        '
        'frmSRQCEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label18)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSRQCEntry"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Sales Return QC Entry"
        Me.FraFront.ResumeLayout(False)
        Me.Frasupp.ResumeLayout(False)
        Me.Frasupp.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(AdataItem, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class