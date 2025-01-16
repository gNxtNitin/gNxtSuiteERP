Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmDeviation
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
    Public WithEvents txtNoSets As System.Windows.Forms.TextBox
    Public WithEvents cboGranted As System.Windows.Forms.ComboBox
    Public WithEvents cmdSearchQCMan As System.Windows.Forms.Button
    Public WithEvents txtQCManager As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchHOD As System.Windows.Forms.Button
    Public WithEvents txtHOD As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSuper As System.Windows.Forms.Button
    Public WithEvents txtSuperviser As System.Windows.Forms.TextBox
    Public WithEvents txtComment As System.Windows.Forms.TextBox
    Public WithEvents txtDefDate As System.Windows.Forms.TextBox
    Public WithEvents txtDefQuantity As System.Windows.Forms.TextBox
    Public WithEvents txtDefect As System.Windows.Forms.TextBox
    Public WithEvents txtPrevDevRef As System.Windows.Forms.TextBox
    Public WithEvents txtDevParam As System.Windows.Forms.TextBox
    Public WithEvents txtRequestDate As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchOperation As System.Windows.Forms.Button
    Public WithEvents txtOperation As System.Windows.Forms.TextBox
    Public WithEvents txtDevQuantity As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchComp As System.Windows.Forms.Button
    Public WithEvents txtComponent As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchShop As System.Windows.Forms.Button
    Public WithEvents txtShop As System.Windows.Forms.TextBox
    Public WithEvents CmdSearchCustomer As System.Windows.Forms.Button
    Public WithEvents txtCustomer As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSlipNo As System.Windows.Forms.Button
    Public WithEvents txtDate As System.Windows.Forms.TextBox
    Public WithEvents txtSlipNo As System.Windows.Forms.TextBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents lblModel As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents lblQCManager As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblHOD As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents lblSuperviser As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents lblOperation As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblComponent As System.Windows.Forms.Label
    Public WithEvents Lbl12 As System.Windows.Forms.Label
    Public WithEvents lblShop As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents lblCustomer As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDeviation))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchQCMan = New System.Windows.Forms.Button()
        Me.cmdSearchHOD = New System.Windows.Forms.Button()
        Me.cmdSearchSuper = New System.Windows.Forms.Button()
        Me.cmdSearchOperation = New System.Windows.Forms.Button()
        Me.cmdSearchComp = New System.Windows.Forms.Button()
        Me.cmdSearchShop = New System.Windows.Forms.Button()
        Me.CmdSearchCustomer = New System.Windows.Forms.Button()
        Me.cmdSearchSlipNo = New System.Windows.Forms.Button()
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
        Me.txtNoSets = New System.Windows.Forms.TextBox()
        Me.cboGranted = New System.Windows.Forms.ComboBox()
        Me.txtQCManager = New System.Windows.Forms.TextBox()
        Me.txtHOD = New System.Windows.Forms.TextBox()
        Me.txtSuperviser = New System.Windows.Forms.TextBox()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.txtDefDate = New System.Windows.Forms.TextBox()
        Me.txtDefQuantity = New System.Windows.Forms.TextBox()
        Me.txtDefect = New System.Windows.Forms.TextBox()
        Me.txtPrevDevRef = New System.Windows.Forms.TextBox()
        Me.txtDevParam = New System.Windows.Forms.TextBox()
        Me.txtRequestDate = New System.Windows.Forms.TextBox()
        Me.txtOperation = New System.Windows.Forms.TextBox()
        Me.txtDevQuantity = New System.Windows.Forms.TextBox()
        Me.txtComponent = New System.Windows.Forms.TextBox()
        Me.txtShop = New System.Windows.Forms.TextBox()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.txtSlipNo = New System.Windows.Forms.TextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblModel = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblQCManager = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblHOD = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblSuperviser = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblOperation = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblComponent = New System.Windows.Forms.Label()
        Me.Lbl12 = New System.Windows.Forms.Label()
        Me.lblShop = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.fraTop1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSearchQCMan
        '
        Me.cmdSearchQCMan.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchQCMan.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchQCMan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchQCMan.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchQCMan.Image = CType(resources.GetObject("cmdSearchQCMan.Image"), System.Drawing.Image)
        Me.cmdSearchQCMan.Location = New System.Drawing.Point(196, 300)
        Me.cmdSearchQCMan.Name = "cmdSearchQCMan"
        Me.cmdSearchQCMan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchQCMan.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchQCMan.TabIndex = 60
        Me.cmdSearchQCMan.TabStop = False
        Me.cmdSearchQCMan.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchQCMan, "Search")
        Me.cmdSearchQCMan.UseVisualStyleBackColor = False
        '
        'cmdSearchHOD
        '
        Me.cmdSearchHOD.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchHOD.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchHOD.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchHOD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchHOD.Image = CType(resources.GetObject("cmdSearchHOD.Image"), System.Drawing.Image)
        Me.cmdSearchHOD.Location = New System.Drawing.Point(196, 278)
        Me.cmdSearchHOD.Name = "cmdSearchHOD"
        Me.cmdSearchHOD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchHOD.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchHOD.TabIndex = 56
        Me.cmdSearchHOD.TabStop = False
        Me.cmdSearchHOD.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchHOD, "Search")
        Me.cmdSearchHOD.UseVisualStyleBackColor = False
        '
        'cmdSearchSuper
        '
        Me.cmdSearchSuper.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSuper.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSuper.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSuper.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSuper.Image = CType(resources.GetObject("cmdSearchSuper.Image"), System.Drawing.Image)
        Me.cmdSearchSuper.Location = New System.Drawing.Point(196, 256)
        Me.cmdSearchSuper.Name = "cmdSearchSuper"
        Me.cmdSearchSuper.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSuper.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchSuper.TabIndex = 52
        Me.cmdSearchSuper.TabStop = False
        Me.cmdSearchSuper.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSuper, "Search")
        Me.cmdSearchSuper.UseVisualStyleBackColor = False
        '
        'cmdSearchOperation
        '
        Me.cmdSearchOperation.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchOperation.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchOperation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchOperation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchOperation.Image = CType(resources.GetObject("cmdSearchOperation.Image"), System.Drawing.Image)
        Me.cmdSearchOperation.Location = New System.Drawing.Point(196, 124)
        Me.cmdSearchOperation.Name = "cmdSearchOperation"
        Me.cmdSearchOperation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchOperation.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchOperation.TabIndex = 34
        Me.cmdSearchOperation.TabStop = False
        Me.cmdSearchOperation.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchOperation, "Search")
        Me.cmdSearchOperation.UseVisualStyleBackColor = False
        '
        'cmdSearchComp
        '
        Me.cmdSearchComp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchComp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchComp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchComp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchComp.Image = CType(resources.GetObject("cmdSearchComp.Image"), System.Drawing.Image)
        Me.cmdSearchComp.Location = New System.Drawing.Point(196, 78)
        Me.cmdSearchComp.Name = "cmdSearchComp"
        Me.cmdSearchComp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchComp.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchComp.TabIndex = 28
        Me.cmdSearchComp.TabStop = False
        Me.cmdSearchComp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchComp, "Search")
        Me.cmdSearchComp.UseVisualStyleBackColor = False
        '
        'cmdSearchShop
        '
        Me.cmdSearchShop.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchShop.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchShop.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchShop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchShop.Image = CType(resources.GetObject("cmdSearchShop.Image"), System.Drawing.Image)
        Me.cmdSearchShop.Location = New System.Drawing.Point(196, 56)
        Me.cmdSearchShop.Name = "cmdSearchShop"
        Me.cmdSearchShop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchShop.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchShop.TabIndex = 24
        Me.cmdSearchShop.TabStop = False
        Me.cmdSearchShop.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchShop, "Search")
        Me.cmdSearchShop.UseVisualStyleBackColor = False
        '
        'CmdSearchCustomer
        '
        Me.CmdSearchCustomer.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchCustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchCustomer.Image = CType(resources.GetObject("CmdSearchCustomer.Image"), System.Drawing.Image)
        Me.CmdSearchCustomer.Location = New System.Drawing.Point(196, 34)
        Me.CmdSearchCustomer.Name = "CmdSearchCustomer"
        Me.CmdSearchCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchCustomer.Size = New System.Drawing.Size(23, 19)
        Me.CmdSearchCustomer.TabIndex = 20
        Me.CmdSearchCustomer.TabStop = False
        Me.CmdSearchCustomer.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchCustomer, "Search")
        Me.CmdSearchCustomer.UseVisualStyleBackColor = False
        '
        'cmdSearchSlipNo
        '
        Me.cmdSearchSlipNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSlipNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSlipNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSlipNo.Image = CType(resources.GetObject("cmdSearchSlipNo.Image"), System.Drawing.Image)
        Me.cmdSearchSlipNo.Location = New System.Drawing.Point(196, 12)
        Me.cmdSearchSlipNo.Name = "cmdSearchSlipNo"
        Me.cmdSearchSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSlipNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchSlipNo.TabIndex = 15
        Me.cmdSearchSlipNo.TabStop = False
        Me.cmdSearchSlipNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSlipNo, "Search")
        Me.cmdSearchSlipNo.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(456, 11)
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(254, 11)
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
        Me.cmdPrint.Location = New System.Drawing.Point(388, 11)
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
        Me.CmdClose.Location = New System.Drawing.Point(590, 11)
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
        Me.CmdView.Location = New System.Drawing.Point(522, 11)
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
        Me.CmdDelete.Location = New System.Drawing.Point(320, 11)
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
        Me.CmdSave.Location = New System.Drawing.Point(186, 11)
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
        Me.CmdModify.Location = New System.Drawing.Point(118, 11)
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
        Me.CmdAdd.Location = New System.Drawing.Point(52, 11)
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
        Me.fraTop1.Controls.Add(Me.txtNoSets)
        Me.fraTop1.Controls.Add(Me.cboGranted)
        Me.fraTop1.Controls.Add(Me.cmdSearchQCMan)
        Me.fraTop1.Controls.Add(Me.txtQCManager)
        Me.fraTop1.Controls.Add(Me.cmdSearchHOD)
        Me.fraTop1.Controls.Add(Me.txtHOD)
        Me.fraTop1.Controls.Add(Me.cmdSearchSuper)
        Me.fraTop1.Controls.Add(Me.txtSuperviser)
        Me.fraTop1.Controls.Add(Me.txtComment)
        Me.fraTop1.Controls.Add(Me.txtDefDate)
        Me.fraTop1.Controls.Add(Me.txtDefQuantity)
        Me.fraTop1.Controls.Add(Me.txtDefect)
        Me.fraTop1.Controls.Add(Me.txtPrevDevRef)
        Me.fraTop1.Controls.Add(Me.txtDevParam)
        Me.fraTop1.Controls.Add(Me.txtRequestDate)
        Me.fraTop1.Controls.Add(Me.cmdSearchOperation)
        Me.fraTop1.Controls.Add(Me.txtOperation)
        Me.fraTop1.Controls.Add(Me.txtDevQuantity)
        Me.fraTop1.Controls.Add(Me.cmdSearchComp)
        Me.fraTop1.Controls.Add(Me.txtComponent)
        Me.fraTop1.Controls.Add(Me.cmdSearchShop)
        Me.fraTop1.Controls.Add(Me.txtShop)
        Me.fraTop1.Controls.Add(Me.CmdSearchCustomer)
        Me.fraTop1.Controls.Add(Me.txtCustomer)
        Me.fraTop1.Controls.Add(Me.cmdSearchSlipNo)
        Me.fraTop1.Controls.Add(Me.txtDate)
        Me.fraTop1.Controls.Add(Me.txtSlipNo)
        Me.fraTop1.Controls.Add(Me.SprdMain)
        Me.fraTop1.Controls.Add(Me.Label17)
        Me.fraTop1.Controls.Add(Me.Label13)
        Me.fraTop1.Controls.Add(Me.Label15)
        Me.fraTop1.Controls.Add(Me.lblModel)
        Me.fraTop1.Controls.Add(Me.Label18)
        Me.fraTop1.Controls.Add(Me.lblQCManager)
        Me.fraTop1.Controls.Add(Me.Label16)
        Me.fraTop1.Controls.Add(Me.lblHOD)
        Me.fraTop1.Controls.Add(Me.Label14)
        Me.fraTop1.Controls.Add(Me.lblSuperviser)
        Me.fraTop1.Controls.Add(Me.Label12)
        Me.fraTop1.Controls.Add(Me.Label11)
        Me.fraTop1.Controls.Add(Me.Label10)
        Me.fraTop1.Controls.Add(Me.Label9)
        Me.fraTop1.Controls.Add(Me.Label6)
        Me.fraTop1.Controls.Add(Me.Label5)
        Me.fraTop1.Controls.Add(Me.Label3)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Controls.Add(Me.lblOperation)
        Me.fraTop1.Controls.Add(Me.Label1)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.lblComponent)
        Me.fraTop1.Controls.Add(Me.Lbl12)
        Me.fraTop1.Controls.Add(Me.lblShop)
        Me.fraTop1.Controls.Add(Me.Label29)
        Me.fraTop1.Controls.Add(Me.lblCustomer)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, -6)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(711, 467)
        Me.fraTop1.TabIndex = 12
        Me.fraTop1.TabStop = False
        '
        'txtNoSets
        '
        Me.txtNoSets.AcceptsReturn = True
        Me.txtNoSets.BackColor = System.Drawing.SystemColors.Window
        Me.txtNoSets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoSets.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNoSets.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoSets.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNoSets.Location = New System.Drawing.Point(612, 300)
        Me.txtNoSets.MaxLength = 0
        Me.txtNoSets.Name = "txtNoSets"
        Me.txtNoSets.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNoSets.Size = New System.Drawing.Size(93, 20)
        Me.txtNoSets.TabIndex = 67
        '
        'cboGranted
        '
        Me.cboGranted.BackColor = System.Drawing.SystemColors.Window
        Me.cboGranted.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboGranted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGranted.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGranted.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboGranted.Location = New System.Drawing.Point(612, 256)
        Me.cboGranted.Name = "cboGranted"
        Me.cboGranted.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboGranted.Size = New System.Drawing.Size(93, 22)
        Me.cboGranted.TabIndex = 65
        '
        'txtQCManager
        '
        Me.txtQCManager.AcceptsReturn = True
        Me.txtQCManager.BackColor = System.Drawing.SystemColors.Window
        Me.txtQCManager.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQCManager.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtQCManager.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQCManager.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtQCManager.Location = New System.Drawing.Point(101, 300)
        Me.txtQCManager.MaxLength = 0
        Me.txtQCManager.Name = "txtQCManager"
        Me.txtQCManager.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtQCManager.Size = New System.Drawing.Size(93, 20)
        Me.txtQCManager.TabIndex = 59
        '
        'txtHOD
        '
        Me.txtHOD.AcceptsReturn = True
        Me.txtHOD.BackColor = System.Drawing.SystemColors.Window
        Me.txtHOD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHOD.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHOD.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHOD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtHOD.Location = New System.Drawing.Point(101, 278)
        Me.txtHOD.MaxLength = 0
        Me.txtHOD.Name = "txtHOD"
        Me.txtHOD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHOD.Size = New System.Drawing.Size(93, 20)
        Me.txtHOD.TabIndex = 55
        '
        'txtSuperviser
        '
        Me.txtSuperviser.AcceptsReturn = True
        Me.txtSuperviser.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuperviser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuperviser.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuperviser.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuperviser.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSuperviser.Location = New System.Drawing.Point(101, 256)
        Me.txtSuperviser.MaxLength = 0
        Me.txtSuperviser.Name = "txtSuperviser"
        Me.txtSuperviser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuperviser.Size = New System.Drawing.Size(93, 20)
        Me.txtSuperviser.TabIndex = 51
        '
        'txtComment
        '
        Me.txtComment.AcceptsReturn = True
        Me.txtComment.BackColor = System.Drawing.SystemColors.Window
        Me.txtComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComment.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtComment.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtComment.Location = New System.Drawing.Point(101, 234)
        Me.txtComment.MaxLength = 0
        Me.txtComment.Name = "txtComment"
        Me.txtComment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtComment.Size = New System.Drawing.Size(409, 20)
        Me.txtComment.TabIndex = 49
        '
        'txtDefDate
        '
        Me.txtDefDate.AcceptsReturn = True
        Me.txtDefDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDefDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDefDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDefDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDefDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDefDate.Location = New System.Drawing.Point(612, 212)
        Me.txtDefDate.MaxLength = 0
        Me.txtDefDate.Name = "txtDefDate"
        Me.txtDefDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDefDate.Size = New System.Drawing.Size(93, 20)
        Me.txtDefDate.TabIndex = 47
        '
        'txtDefQuantity
        '
        Me.txtDefQuantity.AcceptsReturn = True
        Me.txtDefQuantity.BackColor = System.Drawing.SystemColors.Window
        Me.txtDefQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDefQuantity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDefQuantity.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDefQuantity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDefQuantity.Location = New System.Drawing.Point(101, 212)
        Me.txtDefQuantity.MaxLength = 0
        Me.txtDefQuantity.Name = "txtDefQuantity"
        Me.txtDefQuantity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDefQuantity.Size = New System.Drawing.Size(93, 20)
        Me.txtDefQuantity.TabIndex = 45
        '
        'txtDefect
        '
        Me.txtDefect.AcceptsReturn = True
        Me.txtDefect.BackColor = System.Drawing.SystemColors.Window
        Me.txtDefect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDefect.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDefect.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDefect.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDefect.Location = New System.Drawing.Point(101, 190)
        Me.txtDefect.MaxLength = 0
        Me.txtDefect.Name = "txtDefect"
        Me.txtDefect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDefect.Size = New System.Drawing.Size(409, 20)
        Me.txtDefect.TabIndex = 43
        '
        'txtPrevDevRef
        '
        Me.txtPrevDevRef.AcceptsReturn = True
        Me.txtPrevDevRef.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrevDevRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrevDevRef.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrevDevRef.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrevDevRef.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPrevDevRef.Location = New System.Drawing.Point(101, 168)
        Me.txtPrevDevRef.MaxLength = 0
        Me.txtPrevDevRef.Name = "txtPrevDevRef"
        Me.txtPrevDevRef.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrevDevRef.Size = New System.Drawing.Size(409, 20)
        Me.txtPrevDevRef.TabIndex = 41
        '
        'txtDevParam
        '
        Me.txtDevParam.AcceptsReturn = True
        Me.txtDevParam.BackColor = System.Drawing.SystemColors.Window
        Me.txtDevParam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDevParam.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDevParam.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDevParam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDevParam.Location = New System.Drawing.Point(101, 146)
        Me.txtDevParam.MaxLength = 0
        Me.txtDevParam.Name = "txtDevParam"
        Me.txtDevParam.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDevParam.Size = New System.Drawing.Size(409, 20)
        Me.txtDevParam.TabIndex = 39
        '
        'txtRequestDate
        '
        Me.txtRequestDate.AcceptsReturn = True
        Me.txtRequestDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtRequestDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRequestDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRequestDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequestDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRequestDate.Location = New System.Drawing.Point(612, 124)
        Me.txtRequestDate.MaxLength = 0
        Me.txtRequestDate.Name = "txtRequestDate"
        Me.txtRequestDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRequestDate.Size = New System.Drawing.Size(93, 20)
        Me.txtRequestDate.TabIndex = 37
        '
        'txtOperation
        '
        Me.txtOperation.AcceptsReturn = True
        Me.txtOperation.BackColor = System.Drawing.SystemColors.Window
        Me.txtOperation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOperation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOperation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOperation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtOperation.Location = New System.Drawing.Point(101, 124)
        Me.txtOperation.MaxLength = 0
        Me.txtOperation.Name = "txtOperation"
        Me.txtOperation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOperation.Size = New System.Drawing.Size(93, 20)
        Me.txtOperation.TabIndex = 33
        '
        'txtDevQuantity
        '
        Me.txtDevQuantity.AcceptsReturn = True
        Me.txtDevQuantity.BackColor = System.Drawing.SystemColors.Window
        Me.txtDevQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDevQuantity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDevQuantity.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDevQuantity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDevQuantity.Location = New System.Drawing.Point(612, 78)
        Me.txtDevQuantity.MaxLength = 0
        Me.txtDevQuantity.Name = "txtDevQuantity"
        Me.txtDevQuantity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDevQuantity.Size = New System.Drawing.Size(93, 20)
        Me.txtDevQuantity.TabIndex = 31
        '
        'txtComponent
        '
        Me.txtComponent.AcceptsReturn = True
        Me.txtComponent.BackColor = System.Drawing.SystemColors.Window
        Me.txtComponent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComponent.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtComponent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComponent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtComponent.Location = New System.Drawing.Point(101, 78)
        Me.txtComponent.MaxLength = 0
        Me.txtComponent.Name = "txtComponent"
        Me.txtComponent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtComponent.Size = New System.Drawing.Size(93, 20)
        Me.txtComponent.TabIndex = 27
        '
        'txtShop
        '
        Me.txtShop.AcceptsReturn = True
        Me.txtShop.BackColor = System.Drawing.SystemColors.Window
        Me.txtShop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShop.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShop.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShop.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtShop.Location = New System.Drawing.Point(101, 56)
        Me.txtShop.MaxLength = 0
        Me.txtShop.Name = "txtShop"
        Me.txtShop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShop.Size = New System.Drawing.Size(93, 20)
        Me.txtShop.TabIndex = 23
        '
        'txtCustomer
        '
        Me.txtCustomer.AcceptsReturn = True
        Me.txtCustomer.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomer.Location = New System.Drawing.Point(101, 34)
        Me.txtCustomer.MaxLength = 0
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomer.Size = New System.Drawing.Size(93, 20)
        Me.txtCustomer.TabIndex = 19
        '
        'txtDate
        '
        Me.txtDate.AcceptsReturn = True
        Me.txtDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDate.Location = New System.Drawing.Point(612, 12)
        Me.txtDate.MaxLength = 0
        Me.txtDate.Name = "txtDate"
        Me.txtDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDate.Size = New System.Drawing.Size(93, 20)
        Me.txtDate.TabIndex = 14
        '
        'txtSlipNo
        '
        Me.txtSlipNo.AcceptsReturn = True
        Me.txtSlipNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSlipNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSlipNo.Location = New System.Drawing.Point(101, 12)
        Me.txtSlipNo.MaxLength = 0
        Me.txtSlipNo.Name = "txtSlipNo"
        Me.txtSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSlipNo.Size = New System.Drawing.Size(93, 20)
        Me.txtSlipNo.TabIndex = 13
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 322)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(707, 143)
        Me.SprdMain.TabIndex = 18
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(516, 304)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(59, 13)
        Me.Label17.TabIndex = 68
        Me.Label17.Text = "No.'s/Sets"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(516, 262)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(48, 13)
        Me.Label13.TabIndex = 66
        Me.Label13.Text = "Granted"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(6, 104)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(40, 13)
        Me.Label15.TabIndex = 64
        Me.Label15.Text = "Model"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModel
        '
        Me.lblModel.BackColor = System.Drawing.SystemColors.Control
        Me.lblModel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModel.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModel.Location = New System.Drawing.Point(101, 100)
        Me.lblModel.Name = "lblModel"
        Me.lblModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModel.Size = New System.Drawing.Size(409, 19)
        Me.lblModel.TabIndex = 63
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(6, 304)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(72, 13)
        Me.Label18.TabIndex = 62
        Me.Label18.Text = "Q.C.Manager"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblQCManager
        '
        Me.lblQCManager.BackColor = System.Drawing.SystemColors.Control
        Me.lblQCManager.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblQCManager.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblQCManager.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQCManager.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblQCManager.Location = New System.Drawing.Point(221, 300)
        Me.lblQCManager.Name = "lblQCManager"
        Me.lblQCManager.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblQCManager.Size = New System.Drawing.Size(289, 19)
        Me.lblQCManager.TabIndex = 61
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(6, 282)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(39, 13)
        Me.Label16.TabIndex = 58
        Me.Label16.Text = "H.O.D."
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblHOD
        '
        Me.lblHOD.BackColor = System.Drawing.SystemColors.Control
        Me.lblHOD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHOD.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblHOD.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHOD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHOD.Location = New System.Drawing.Point(221, 278)
        Me.lblHOD.Name = "lblHOD"
        Me.lblHOD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblHOD.Size = New System.Drawing.Size(289, 19)
        Me.lblHOD.TabIndex = 57
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(6, 260)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(60, 13)
        Me.Label14.TabIndex = 54
        Me.Label14.Text = "Superviser"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSuperviser
        '
        Me.lblSuperviser.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperviser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSuperviser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSuperviser.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuperviser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSuperviser.Location = New System.Drawing.Point(221, 256)
        Me.lblSuperviser.Name = "lblSuperviser"
        Me.lblSuperviser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSuperviser.Size = New System.Drawing.Size(289, 19)
        Me.lblSuperviser.TabIndex = 53
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(6, 238)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(57, 13)
        Me.Label12.TabIndex = 50
        Me.Label12.Text = "Comment"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(516, 216)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(31, 13)
        Me.Label11.TabIndex = 48
        Me.Label11.Text = "Date"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(6, 216)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(73, 13)
        Me.Label10.TabIndex = 46
        Me.Label10.Text = "Def. Quantity"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(6, 194)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(40, 13)
        Me.Label9.TabIndex = 44
        Me.Label9.Text = "Defect"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(6, 172)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(78, 13)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "Prev. Dev. Ref."
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(6, 150)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(84, 13)
        Me.Label5.TabIndex = 40
        Me.Label5.Text = "Dev. Parameter"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(516, 128)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "Request Date"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(6, 128)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 36
        Me.Label4.Text = "Operation"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblOperation
        '
        Me.lblOperation.BackColor = System.Drawing.SystemColors.Control
        Me.lblOperation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOperation.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblOperation.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOperation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOperation.Location = New System.Drawing.Point(221, 124)
        Me.lblOperation.Name = "lblOperation"
        Me.lblOperation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblOperation.Size = New System.Drawing.Size(289, 19)
        Me.lblOperation.TabIndex = 35
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(516, 82)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Dev. Quantity"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(6, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Component"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblComponent
        '
        Me.lblComponent.BackColor = System.Drawing.SystemColors.Control
        Me.lblComponent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblComponent.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblComponent.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComponent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblComponent.Location = New System.Drawing.Point(221, 78)
        Me.lblComponent.Name = "lblComponent"
        Me.lblComponent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblComponent.Size = New System.Drawing.Size(289, 19)
        Me.lblComponent.TabIndex = 29
        '
        'Lbl12
        '
        Me.Lbl12.AutoSize = True
        Me.Lbl12.BackColor = System.Drawing.SystemColors.Control
        Me.Lbl12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Lbl12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl12.Location = New System.Drawing.Point(6, 60)
        Me.Lbl12.Name = "Lbl12"
        Me.Lbl12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Lbl12.Size = New System.Drawing.Size(75, 13)
        Me.Lbl12.TabIndex = 26
        Me.Lbl12.Text = "Shop/Section"
        Me.Lbl12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblShop
        '
        Me.lblShop.BackColor = System.Drawing.SystemColors.Control
        Me.lblShop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblShop.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShop.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShop.Location = New System.Drawing.Point(221, 56)
        Me.lblShop.Name = "lblShop"
        Me.lblShop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShop.Size = New System.Drawing.Size(289, 19)
        Me.lblShop.TabIndex = 25
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(6, 38)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(56, 13)
        Me.Label29.TabIndex = 22
        Me.Label29.Text = "Customer"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCustomer
        '
        Me.lblCustomer.BackColor = System.Drawing.SystemColors.Control
        Me.lblCustomer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCustomer.Location = New System.Drawing.Point(221, 34)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCustomer.Size = New System.Drawing.Size(289, 19)
        Me.lblCustomer.TabIndex = 21
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(516, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Date"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(6, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Number"
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
        Me.SprdView.Size = New System.Drawing.Size(710, 461)
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
        Me.FraMovement.Location = New System.Drawing.Point(0, 458)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(711, 51)
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
        'frmDeviation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(711, 509)
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
        Me.Name = "frmDeviation"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Deviation Format"
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
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