Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmLayoutInspPlan
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
    Public WithEvents txtDeputed As System.Windows.Forms.TextBox
    Public WithEvents txtStage As System.Windows.Forms.TextBox
    Public WithEvents CmdSearchCustomer As System.Windows.Forms.Button
    Public WithEvents txtCustomer As System.Windows.Forms.TextBox
    Public WithEvents txtMayActual As System.Windows.Forms.TextBox
    Public WithEvents txtJulActual As System.Windows.Forms.TextBox
    Public WithEvents txtAugActual As System.Windows.Forms.TextBox
    Public WithEvents txtOctActual As System.Windows.Forms.TextBox
    Public WithEvents txtNovActual As System.Windows.Forms.TextBox
    Public WithEvents txtDecActual As System.Windows.Forms.TextBox
    Public WithEvents txtJanActual As System.Windows.Forms.TextBox
    Public WithEvents txtMarActual As System.Windows.Forms.TextBox
    Public WithEvents txtJunActual As System.Windows.Forms.TextBox
    Public WithEvents txtSepActual As System.Windows.Forms.TextBox
    Public WithEvents txtAprActual As System.Windows.Forms.TextBox
    Public WithEvents txtFebActual As System.Windows.Forms.TextBox
    Public WithEvents txtMayPlan As System.Windows.Forms.TextBox
    Public WithEvents txtJulPlan As System.Windows.Forms.TextBox
    Public WithEvents txtAugPlan As System.Windows.Forms.TextBox
    Public WithEvents txtOctPlan As System.Windows.Forms.TextBox
    Public WithEvents txtNovPlan As System.Windows.Forms.TextBox
    Public WithEvents txtDecPlan As System.Windows.Forms.TextBox
    Public WithEvents txtJanPlan As System.Windows.Forms.TextBox
    Public WithEvents txtMarPlan As System.Windows.Forms.TextBox
    Public WithEvents txtJunPlan As System.Windows.Forms.TextBox
    Public WithEvents txtSepPlan As System.Windows.Forms.TextBox
    Public WithEvents txtAprPlan As System.Windows.Forms.TextBox
    Public WithEvents txtFebPlan As System.Windows.Forms.TextBox
    Public WithEvents txtYear As System.Windows.Forms.TextBox
    Public WithEvents txtNumber As System.Windows.Forms.TextBox
    Public WithEvents txtProduct As System.Windows.Forms.TextBox
    Public WithEvents CmdSearchProduct As System.Windows.Forms.Button
    Public WithEvents txtPreparedBy As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchPrepBy As System.Windows.Forms.Button
    Public WithEvents cmdSearchNumber As System.Windows.Forms.Button
    Public WithEvents lblAutoKeyName As System.Windows.Forms.Label
    Public WithEvents lblTableName As System.Windows.Forms.Label
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents lblCustomer As System.Windows.Forms.Label
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblPreparedBy As System.Windows.Forms.Label
    Public WithEvents lblProduct As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblLabels As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLayoutInspPlan))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdSearchCustomer = New System.Windows.Forms.Button()
        Me.CmdSearchProduct = New System.Windows.Forms.Button()
        Me.cmdSearchPrepBy = New System.Windows.Forms.Button()
        Me.cmdSearchNumber = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtDeputed = New System.Windows.Forms.TextBox()
        Me.txtStage = New System.Windows.Forms.TextBox()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.txtMayActual = New System.Windows.Forms.TextBox()
        Me.txtJulActual = New System.Windows.Forms.TextBox()
        Me.txtAugActual = New System.Windows.Forms.TextBox()
        Me.txtOctActual = New System.Windows.Forms.TextBox()
        Me.txtNovActual = New System.Windows.Forms.TextBox()
        Me.txtDecActual = New System.Windows.Forms.TextBox()
        Me.txtJanActual = New System.Windows.Forms.TextBox()
        Me.txtMarActual = New System.Windows.Forms.TextBox()
        Me.txtJunActual = New System.Windows.Forms.TextBox()
        Me.txtSepActual = New System.Windows.Forms.TextBox()
        Me.txtAprActual = New System.Windows.Forms.TextBox()
        Me.txtFebActual = New System.Windows.Forms.TextBox()
        Me.txtMayPlan = New System.Windows.Forms.TextBox()
        Me.txtJulPlan = New System.Windows.Forms.TextBox()
        Me.txtAugPlan = New System.Windows.Forms.TextBox()
        Me.txtOctPlan = New System.Windows.Forms.TextBox()
        Me.txtNovPlan = New System.Windows.Forms.TextBox()
        Me.txtDecPlan = New System.Windows.Forms.TextBox()
        Me.txtJanPlan = New System.Windows.Forms.TextBox()
        Me.txtMarPlan = New System.Windows.Forms.TextBox()
        Me.txtJunPlan = New System.Windows.Forms.TextBox()
        Me.txtSepPlan = New System.Windows.Forms.TextBox()
        Me.txtAprPlan = New System.Windows.Forms.TextBox()
        Me.txtFebPlan = New System.Windows.Forms.TextBox()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.txtNumber = New System.Windows.Forms.TextBox()
        Me.txtProduct = New System.Windows.Forms.TextBox()
        Me.txtPreparedBy = New System.Windows.Forms.TextBox()
        Me.lblAutoKeyName = New System.Windows.Forms.Label()
        Me.lblTableName = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPreparedBy = New System.Windows.Forms.Label()
        Me.lblProduct = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Frame4.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdSearchCustomer
        '
        Me.CmdSearchCustomer.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchCustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchCustomer.Image = CType(resources.GetObject("CmdSearchCustomer.Image"), System.Drawing.Image)
        Me.CmdSearchCustomer.Location = New System.Drawing.Point(226, 38)
        Me.CmdSearchCustomer.Name = "CmdSearchCustomer"
        Me.CmdSearchCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchCustomer.Size = New System.Drawing.Size(27, 21)
        Me.CmdSearchCustomer.TabIndex = 76
        Me.CmdSearchCustomer.TabStop = False
        Me.CmdSearchCustomer.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchCustomer, "Search")
        Me.CmdSearchCustomer.UseVisualStyleBackColor = False
        '
        'CmdSearchProduct
        '
        Me.CmdSearchProduct.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchProduct.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchProduct.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchProduct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchProduct.Image = CType(resources.GetObject("CmdSearchProduct.Image"), System.Drawing.Image)
        Me.CmdSearchProduct.Location = New System.Drawing.Point(226, 64)
        Me.CmdSearchProduct.Name = "CmdSearchProduct"
        Me.CmdSearchProduct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchProduct.Size = New System.Drawing.Size(27, 21)
        Me.CmdSearchProduct.TabIndex = 15
        Me.CmdSearchProduct.TabStop = False
        Me.CmdSearchProduct.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchProduct, "Search")
        Me.CmdSearchProduct.UseVisualStyleBackColor = False
        '
        'cmdSearchPrepBy
        '
        Me.cmdSearchPrepBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPrepBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPrepBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPrepBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPrepBy.Image = CType(resources.GetObject("cmdSearchPrepBy.Image"), System.Drawing.Image)
        Me.cmdSearchPrepBy.Location = New System.Drawing.Point(226, 454)
        Me.cmdSearchPrepBy.Name = "cmdSearchPrepBy"
        Me.cmdSearchPrepBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPrepBy.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearchPrepBy.TabIndex = 13
        Me.cmdSearchPrepBy.TabStop = False
        Me.cmdSearchPrepBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPrepBy, "Search")
        Me.cmdSearchPrepBy.UseVisualStyleBackColor = False
        '
        'cmdSearchNumber
        '
        Me.cmdSearchNumber.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchNumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchNumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchNumber.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchNumber.Image = CType(resources.GetObject("cmdSearchNumber.Image"), System.Drawing.Image)
        Me.cmdSearchNumber.Location = New System.Drawing.Point(226, 12)
        Me.cmdSearchNumber.Name = "cmdSearchNumber"
        Me.cmdSearchNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchNumber.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearchNumber.TabIndex = 12
        Me.cmdSearchNumber.TabStop = False
        Me.cmdSearchNumber.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchNumber, "Search")
        Me.cmdSearchNumber.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(6, 14)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(60, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(66, 14)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(60, 37)
        Me.CmdModify.TabIndex = 2
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(246, 14)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(60, 37)
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
        Me.CmdSave.Location = New System.Drawing.Point(126, 14)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 37)
        Me.CmdSave.TabIndex = 3
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(424, 14)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(60, 37)
        Me.CmdView.TabIndex = 8
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(482, 14)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 37)
        Me.CmdClose.TabIndex = 9
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(306, 14)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 6
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtDeputed)
        Me.Frame4.Controls.Add(Me.txtStage)
        Me.Frame4.Controls.Add(Me.CmdSearchCustomer)
        Me.Frame4.Controls.Add(Me.txtCustomer)
        Me.Frame4.Controls.Add(Me.txtMayActual)
        Me.Frame4.Controls.Add(Me.txtJulActual)
        Me.Frame4.Controls.Add(Me.txtAugActual)
        Me.Frame4.Controls.Add(Me.txtOctActual)
        Me.Frame4.Controls.Add(Me.txtNovActual)
        Me.Frame4.Controls.Add(Me.txtDecActual)
        Me.Frame4.Controls.Add(Me.txtJanActual)
        Me.Frame4.Controls.Add(Me.txtMarActual)
        Me.Frame4.Controls.Add(Me.txtJunActual)
        Me.Frame4.Controls.Add(Me.txtSepActual)
        Me.Frame4.Controls.Add(Me.txtAprActual)
        Me.Frame4.Controls.Add(Me.txtFebActual)
        Me.Frame4.Controls.Add(Me.txtMayPlan)
        Me.Frame4.Controls.Add(Me.txtJulPlan)
        Me.Frame4.Controls.Add(Me.txtAugPlan)
        Me.Frame4.Controls.Add(Me.txtOctPlan)
        Me.Frame4.Controls.Add(Me.txtNovPlan)
        Me.Frame4.Controls.Add(Me.txtDecPlan)
        Me.Frame4.Controls.Add(Me.txtJanPlan)
        Me.Frame4.Controls.Add(Me.txtMarPlan)
        Me.Frame4.Controls.Add(Me.txtJunPlan)
        Me.Frame4.Controls.Add(Me.txtSepPlan)
        Me.Frame4.Controls.Add(Me.txtAprPlan)
        Me.Frame4.Controls.Add(Me.txtFebPlan)
        Me.Frame4.Controls.Add(Me.txtYear)
        Me.Frame4.Controls.Add(Me.txtNumber)
        Me.Frame4.Controls.Add(Me.txtProduct)
        Me.Frame4.Controls.Add(Me.CmdSearchProduct)
        Me.Frame4.Controls.Add(Me.txtPreparedBy)
        Me.Frame4.Controls.Add(Me.cmdSearchPrepBy)
        Me.Frame4.Controls.Add(Me.cmdSearchNumber)
        Me.Frame4.Controls.Add(Me.lblAutoKeyName)
        Me.Frame4.Controls.Add(Me.lblTableName)
        Me.Frame4.Controls.Add(Me.Label30)
        Me.Frame4.Controls.Add(Me.Label29)
        Me.Frame4.Controls.Add(Me.lblCustomer)
        Me.Frame4.Controls.Add(Me.lblMkey)
        Me.Frame4.Controls.Add(Me.Label27)
        Me.Frame4.Controls.Add(Me.Label26)
        Me.Frame4.Controls.Add(Me.Label25)
        Me.Frame4.Controls.Add(Me.Label24)
        Me.Frame4.Controls.Add(Me.Label23)
        Me.Frame4.Controls.Add(Me.Label22)
        Me.Frame4.Controls.Add(Me.Label21)
        Me.Frame4.Controls.Add(Me.Label20)
        Me.Frame4.Controls.Add(Me.Label19)
        Me.Frame4.Controls.Add(Me.Label18)
        Me.Frame4.Controls.Add(Me.Label17)
        Me.Frame4.Controls.Add(Me.Label16)
        Me.Frame4.Controls.Add(Me.Label15)
        Me.Frame4.Controls.Add(Me.Label14)
        Me.Frame4.Controls.Add(Me.Label13)
        Me.Frame4.Controls.Add(Me.Label12)
        Me.Frame4.Controls.Add(Me.Label11)
        Me.Frame4.Controls.Add(Me.Label10)
        Me.Frame4.Controls.Add(Me.Label9)
        Me.Frame4.Controls.Add(Me.Label8)
        Me.Frame4.Controls.Add(Me.Label7)
        Me.Frame4.Controls.Add(Me.Label6)
        Me.Frame4.Controls.Add(Me.Label3)
        Me.Frame4.Controls.Add(Me.Label2)
        Me.Frame4.Controls.Add(Me.Label1)
        Me.Frame4.Controls.Add(Me.lblPreparedBy)
        Me.Frame4.Controls.Add(Me.lblProduct)
        Me.Frame4.Controls.Add(Me._lblLabels_0)
        Me.Frame4.Controls.Add(Me._lblLabels_2)
        Me.Frame4.Controls.Add(Me.Label4)
        Me.Frame4.Controls.Add(Me.Label5)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, -6)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(546, 480)
        Me.Frame4.TabIndex = 11
        Me.Frame4.TabStop = False
        '
        'txtDeputed
        '
        Me.txtDeputed.AcceptsReturn = True
        Me.txtDeputed.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeputed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeputed.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeputed.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeputed.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDeputed.Location = New System.Drawing.Point(108, 116)
        Me.txtDeputed.MaxLength = 0
        Me.txtDeputed.Name = "txtDeputed"
        Me.txtDeputed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeputed.Size = New System.Drawing.Size(427, 21)
        Me.txtDeputed.TabIndex = 80
        '
        'txtStage
        '
        Me.txtStage.AcceptsReturn = True
        Me.txtStage.BackColor = System.Drawing.SystemColors.Window
        Me.txtStage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStage.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStage.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStage.Location = New System.Drawing.Point(108, 90)
        Me.txtStage.MaxLength = 0
        Me.txtStage.Name = "txtStage"
        Me.txtStage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStage.Size = New System.Drawing.Size(427, 21)
        Me.txtStage.TabIndex = 79
        '
        'txtCustomer
        '
        Me.txtCustomer.AcceptsReturn = True
        Me.txtCustomer.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomer.Location = New System.Drawing.Point(108, 38)
        Me.txtCustomer.MaxLength = 0
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomer.Size = New System.Drawing.Size(115, 21)
        Me.txtCustomer.TabIndex = 75
        '
        'txtMayActual
        '
        Me.txtMayActual.AcceptsReturn = True
        Me.txtMayActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtMayActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMayActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMayActual.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMayActual.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMayActual.Location = New System.Drawing.Point(444, 168)
        Me.txtMayActual.MaxLength = 0
        Me.txtMayActual.Name = "txtMayActual"
        Me.txtMayActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMayActual.Size = New System.Drawing.Size(91, 21)
        Me.txtMayActual.TabIndex = 72
        '
        'txtJulActual
        '
        Me.txtJulActual.AcceptsReturn = True
        Me.txtJulActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtJulActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJulActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtJulActual.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJulActual.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtJulActual.Location = New System.Drawing.Point(444, 220)
        Me.txtJulActual.MaxLength = 0
        Me.txtJulActual.Name = "txtJulActual"
        Me.txtJulActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJulActual.Size = New System.Drawing.Size(91, 21)
        Me.txtJulActual.TabIndex = 70
        '
        'txtAugActual
        '
        Me.txtAugActual.AcceptsReturn = True
        Me.txtAugActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtAugActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAugActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAugActual.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAugActual.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAugActual.Location = New System.Drawing.Point(444, 246)
        Me.txtAugActual.MaxLength = 0
        Me.txtAugActual.Name = "txtAugActual"
        Me.txtAugActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAugActual.Size = New System.Drawing.Size(91, 21)
        Me.txtAugActual.TabIndex = 68
        '
        'txtOctActual
        '
        Me.txtOctActual.AcceptsReturn = True
        Me.txtOctActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtOctActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOctActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOctActual.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOctActual.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtOctActual.Location = New System.Drawing.Point(444, 298)
        Me.txtOctActual.MaxLength = 0
        Me.txtOctActual.Name = "txtOctActual"
        Me.txtOctActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOctActual.Size = New System.Drawing.Size(91, 21)
        Me.txtOctActual.TabIndex = 66
        '
        'txtNovActual
        '
        Me.txtNovActual.AcceptsReturn = True
        Me.txtNovActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtNovActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNovActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNovActual.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNovActual.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNovActual.Location = New System.Drawing.Point(444, 324)
        Me.txtNovActual.MaxLength = 0
        Me.txtNovActual.Name = "txtNovActual"
        Me.txtNovActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNovActual.Size = New System.Drawing.Size(91, 21)
        Me.txtNovActual.TabIndex = 64
        '
        'txtDecActual
        '
        Me.txtDecActual.AcceptsReturn = True
        Me.txtDecActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtDecActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDecActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDecActual.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDecActual.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDecActual.Location = New System.Drawing.Point(444, 350)
        Me.txtDecActual.MaxLength = 0
        Me.txtDecActual.Name = "txtDecActual"
        Me.txtDecActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDecActual.Size = New System.Drawing.Size(91, 21)
        Me.txtDecActual.TabIndex = 62
        '
        'txtJanActual
        '
        Me.txtJanActual.AcceptsReturn = True
        Me.txtJanActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtJanActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJanActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtJanActual.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJanActual.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtJanActual.Location = New System.Drawing.Point(444, 376)
        Me.txtJanActual.MaxLength = 0
        Me.txtJanActual.Name = "txtJanActual"
        Me.txtJanActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJanActual.Size = New System.Drawing.Size(91, 21)
        Me.txtJanActual.TabIndex = 60
        '
        'txtMarActual
        '
        Me.txtMarActual.AcceptsReturn = True
        Me.txtMarActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtMarActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMarActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMarActual.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMarActual.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMarActual.Location = New System.Drawing.Point(444, 428)
        Me.txtMarActual.MaxLength = 0
        Me.txtMarActual.Name = "txtMarActual"
        Me.txtMarActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMarActual.Size = New System.Drawing.Size(91, 21)
        Me.txtMarActual.TabIndex = 58
        '
        'txtJunActual
        '
        Me.txtJunActual.AcceptsReturn = True
        Me.txtJunActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtJunActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJunActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtJunActual.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJunActual.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtJunActual.Location = New System.Drawing.Point(444, 194)
        Me.txtJunActual.MaxLength = 0
        Me.txtJunActual.Name = "txtJunActual"
        Me.txtJunActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJunActual.Size = New System.Drawing.Size(91, 21)
        Me.txtJunActual.TabIndex = 56
        '
        'txtSepActual
        '
        Me.txtSepActual.AcceptsReturn = True
        Me.txtSepActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtSepActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSepActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSepActual.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSepActual.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSepActual.Location = New System.Drawing.Point(444, 272)
        Me.txtSepActual.MaxLength = 0
        Me.txtSepActual.Name = "txtSepActual"
        Me.txtSepActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSepActual.Size = New System.Drawing.Size(91, 21)
        Me.txtSepActual.TabIndex = 54
        '
        'txtAprActual
        '
        Me.txtAprActual.AcceptsReturn = True
        Me.txtAprActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtAprActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAprActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAprActual.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAprActual.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAprActual.Location = New System.Drawing.Point(444, 142)
        Me.txtAprActual.MaxLength = 0
        Me.txtAprActual.Name = "txtAprActual"
        Me.txtAprActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAprActual.Size = New System.Drawing.Size(91, 21)
        Me.txtAprActual.TabIndex = 52
        '
        'txtFebActual
        '
        Me.txtFebActual.AcceptsReturn = True
        Me.txtFebActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtFebActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFebActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFebActual.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFebActual.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFebActual.Location = New System.Drawing.Point(444, 402)
        Me.txtFebActual.MaxLength = 0
        Me.txtFebActual.Name = "txtFebActual"
        Me.txtFebActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFebActual.Size = New System.Drawing.Size(91, 21)
        Me.txtFebActual.TabIndex = 50
        '
        'txtMayPlan
        '
        Me.txtMayPlan.AcceptsReturn = True
        Me.txtMayPlan.BackColor = System.Drawing.SystemColors.Window
        Me.txtMayPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMayPlan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMayPlan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMayPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMayPlan.Location = New System.Drawing.Point(108, 168)
        Me.txtMayPlan.MaxLength = 0
        Me.txtMayPlan.Name = "txtMayPlan"
        Me.txtMayPlan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMayPlan.Size = New System.Drawing.Size(91, 21)
        Me.txtMayPlan.TabIndex = 48
        '
        'txtJulPlan
        '
        Me.txtJulPlan.AcceptsReturn = True
        Me.txtJulPlan.BackColor = System.Drawing.SystemColors.Window
        Me.txtJulPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJulPlan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtJulPlan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJulPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtJulPlan.Location = New System.Drawing.Point(108, 220)
        Me.txtJulPlan.MaxLength = 0
        Me.txtJulPlan.Name = "txtJulPlan"
        Me.txtJulPlan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJulPlan.Size = New System.Drawing.Size(91, 21)
        Me.txtJulPlan.TabIndex = 46
        '
        'txtAugPlan
        '
        Me.txtAugPlan.AcceptsReturn = True
        Me.txtAugPlan.BackColor = System.Drawing.SystemColors.Window
        Me.txtAugPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAugPlan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAugPlan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAugPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAugPlan.Location = New System.Drawing.Point(108, 246)
        Me.txtAugPlan.MaxLength = 0
        Me.txtAugPlan.Name = "txtAugPlan"
        Me.txtAugPlan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAugPlan.Size = New System.Drawing.Size(91, 21)
        Me.txtAugPlan.TabIndex = 44
        '
        'txtOctPlan
        '
        Me.txtOctPlan.AcceptsReturn = True
        Me.txtOctPlan.BackColor = System.Drawing.SystemColors.Window
        Me.txtOctPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOctPlan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOctPlan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOctPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtOctPlan.Location = New System.Drawing.Point(108, 298)
        Me.txtOctPlan.MaxLength = 0
        Me.txtOctPlan.Name = "txtOctPlan"
        Me.txtOctPlan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOctPlan.Size = New System.Drawing.Size(91, 21)
        Me.txtOctPlan.TabIndex = 42
        '
        'txtNovPlan
        '
        Me.txtNovPlan.AcceptsReturn = True
        Me.txtNovPlan.BackColor = System.Drawing.SystemColors.Window
        Me.txtNovPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNovPlan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNovPlan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNovPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNovPlan.Location = New System.Drawing.Point(108, 324)
        Me.txtNovPlan.MaxLength = 0
        Me.txtNovPlan.Name = "txtNovPlan"
        Me.txtNovPlan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNovPlan.Size = New System.Drawing.Size(91, 21)
        Me.txtNovPlan.TabIndex = 40
        '
        'txtDecPlan
        '
        Me.txtDecPlan.AcceptsReturn = True
        Me.txtDecPlan.BackColor = System.Drawing.SystemColors.Window
        Me.txtDecPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDecPlan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDecPlan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDecPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDecPlan.Location = New System.Drawing.Point(108, 350)
        Me.txtDecPlan.MaxLength = 0
        Me.txtDecPlan.Name = "txtDecPlan"
        Me.txtDecPlan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDecPlan.Size = New System.Drawing.Size(91, 21)
        Me.txtDecPlan.TabIndex = 38
        '
        'txtJanPlan
        '
        Me.txtJanPlan.AcceptsReturn = True
        Me.txtJanPlan.BackColor = System.Drawing.SystemColors.Window
        Me.txtJanPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJanPlan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtJanPlan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJanPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtJanPlan.Location = New System.Drawing.Point(108, 376)
        Me.txtJanPlan.MaxLength = 0
        Me.txtJanPlan.Name = "txtJanPlan"
        Me.txtJanPlan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJanPlan.Size = New System.Drawing.Size(91, 21)
        Me.txtJanPlan.TabIndex = 36
        '
        'txtMarPlan
        '
        Me.txtMarPlan.AcceptsReturn = True
        Me.txtMarPlan.BackColor = System.Drawing.SystemColors.Window
        Me.txtMarPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMarPlan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMarPlan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMarPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMarPlan.Location = New System.Drawing.Point(108, 428)
        Me.txtMarPlan.MaxLength = 0
        Me.txtMarPlan.Name = "txtMarPlan"
        Me.txtMarPlan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMarPlan.Size = New System.Drawing.Size(91, 21)
        Me.txtMarPlan.TabIndex = 34
        '
        'txtJunPlan
        '
        Me.txtJunPlan.AcceptsReturn = True
        Me.txtJunPlan.BackColor = System.Drawing.SystemColors.Window
        Me.txtJunPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJunPlan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtJunPlan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJunPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtJunPlan.Location = New System.Drawing.Point(108, 194)
        Me.txtJunPlan.MaxLength = 0
        Me.txtJunPlan.Name = "txtJunPlan"
        Me.txtJunPlan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJunPlan.Size = New System.Drawing.Size(91, 21)
        Me.txtJunPlan.TabIndex = 32
        '
        'txtSepPlan
        '
        Me.txtSepPlan.AcceptsReturn = True
        Me.txtSepPlan.BackColor = System.Drawing.SystemColors.Window
        Me.txtSepPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSepPlan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSepPlan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSepPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSepPlan.Location = New System.Drawing.Point(108, 272)
        Me.txtSepPlan.MaxLength = 0
        Me.txtSepPlan.Name = "txtSepPlan"
        Me.txtSepPlan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSepPlan.Size = New System.Drawing.Size(91, 21)
        Me.txtSepPlan.TabIndex = 30
        '
        'txtAprPlan
        '
        Me.txtAprPlan.AcceptsReturn = True
        Me.txtAprPlan.BackColor = System.Drawing.SystemColors.Window
        Me.txtAprPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAprPlan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAprPlan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAprPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAprPlan.Location = New System.Drawing.Point(108, 142)
        Me.txtAprPlan.MaxLength = 0
        Me.txtAprPlan.Name = "txtAprPlan"
        Me.txtAprPlan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAprPlan.Size = New System.Drawing.Size(91, 21)
        Me.txtAprPlan.TabIndex = 28
        '
        'txtFebPlan
        '
        Me.txtFebPlan.AcceptsReturn = True
        Me.txtFebPlan.BackColor = System.Drawing.SystemColors.Window
        Me.txtFebPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFebPlan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFebPlan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFebPlan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFebPlan.Location = New System.Drawing.Point(108, 402)
        Me.txtFebPlan.MaxLength = 0
        Me.txtFebPlan.Name = "txtFebPlan"
        Me.txtFebPlan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFebPlan.Size = New System.Drawing.Size(91, 21)
        Me.txtFebPlan.TabIndex = 26
        '
        'txtYear
        '
        Me.txtYear.AcceptsReturn = True
        Me.txtYear.BackColor = System.Drawing.SystemColors.Window
        Me.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtYear.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtYear.Location = New System.Drawing.Point(444, 12)
        Me.txtYear.MaxLength = 0
        Me.txtYear.Name = "txtYear"
        Me.txtYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtYear.Size = New System.Drawing.Size(91, 21)
        Me.txtYear.TabIndex = 24
        '
        'txtNumber
        '
        Me.txtNumber.AcceptsReturn = True
        Me.txtNumber.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNumber.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumber.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNumber.Location = New System.Drawing.Point(108, 12)
        Me.txtNumber.MaxLength = 0
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumber.Size = New System.Drawing.Size(115, 21)
        Me.txtNumber.TabIndex = 17
        '
        'txtProduct
        '
        Me.txtProduct.AcceptsReturn = True
        Me.txtProduct.BackColor = System.Drawing.SystemColors.Window
        Me.txtProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProduct.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProduct.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProduct.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProduct.Location = New System.Drawing.Point(108, 64)
        Me.txtProduct.MaxLength = 0
        Me.txtProduct.Name = "txtProduct"
        Me.txtProduct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProduct.Size = New System.Drawing.Size(115, 21)
        Me.txtProduct.TabIndex = 16
        '
        'txtPreparedBy
        '
        Me.txtPreparedBy.AcceptsReturn = True
        Me.txtPreparedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtPreparedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPreparedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPreparedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPreparedBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPreparedBy.Location = New System.Drawing.Point(108, 454)
        Me.txtPreparedBy.MaxLength = 0
        Me.txtPreparedBy.Name = "txtPreparedBy"
        Me.txtPreparedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPreparedBy.Size = New System.Drawing.Size(115, 21)
        Me.txtPreparedBy.TabIndex = 14
        '
        'lblAutoKeyName
        '
        Me.lblAutoKeyName.BackColor = System.Drawing.SystemColors.Control
        Me.lblAutoKeyName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAutoKeyName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutoKeyName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAutoKeyName.Location = New System.Drawing.Point(204, 408)
        Me.lblAutoKeyName.Name = "lblAutoKeyName"
        Me.lblAutoKeyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAutoKeyName.Size = New System.Drawing.Size(171, 17)
        Me.lblAutoKeyName.TabIndex = 83
        Me.lblAutoKeyName.Text = "lblAutoKeyName"
        Me.lblAutoKeyName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTableName
        '
        Me.lblTableName.BackColor = System.Drawing.SystemColors.Control
        Me.lblTableName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTableName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTableName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTableName.Location = New System.Drawing.Point(204, 430)
        Me.lblTableName.Name = "lblTableName"
        Me.lblTableName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTableName.Size = New System.Drawing.Size(171, 17)
        Me.lblTableName.TabIndex = 82
        Me.lblTableName.Text = "lblTableName"
        Me.lblTableName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(4, 122)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(88, 13)
        Me.Label30.TabIndex = 81
        Me.Label30.Text = "Deputed Person"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(4, 44)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(56, 13)
        Me.Label29.TabIndex = 78
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
        Me.lblCustomer.Location = New System.Drawing.Point(258, 38)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCustomer.Size = New System.Drawing.Size(277, 21)
        Me.lblCustomer.TabIndex = 77
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(204, 384)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(171, 17)
        Me.lblMkey.TabIndex = 74
        Me.lblMkey.Text = "lblMkey"
        Me.lblMkey.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(351, 174)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(63, 13)
        Me.Label27.TabIndex = 73
        Me.Label27.Text = "May Actual"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(351, 226)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(54, 13)
        Me.Label26.TabIndex = 71
        Me.Label26.Text = "Jul Actual"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(351, 252)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(61, 13)
        Me.Label25.TabIndex = 69
        Me.Label25.Text = "Aug Actual"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(351, 304)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(58, 13)
        Me.Label24.TabIndex = 67
        Me.Label24.Text = "Oct Actual"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(351, 330)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(62, 13)
        Me.Label23.TabIndex = 65
        Me.Label23.Text = "Nov Actual"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(351, 356)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(60, 13)
        Me.Label22.TabIndex = 63
        Me.Label22.Text = "Dec Actual"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(351, 382)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(57, 13)
        Me.Label21.TabIndex = 61
        Me.Label21.Text = "Jan Actual"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(351, 434)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(61, 13)
        Me.Label20.TabIndex = 59
        Me.Label20.Text = "Mar Actual"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(351, 200)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(57, 13)
        Me.Label19.TabIndex = 57
        Me.Label19.Text = "Jun Actual"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(351, 278)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(60, 13)
        Me.Label18.TabIndex = 55
        Me.Label18.Text = "Sep Actual"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(351, 148)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(59, 13)
        Me.Label17.TabIndex = 53
        Me.Label17.Text = "Apr Actual"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(351, 408)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(60, 13)
        Me.Label16.TabIndex = 51
        Me.Label16.Text = "Feb Actual"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(4, 174)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(53, 13)
        Me.Label15.TabIndex = 49
        Me.Label15.Text = "May Plan"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(4, 226)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(44, 13)
        Me.Label14.TabIndex = 47
        Me.Label14.Text = "Jul Plan"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(4, 252)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(51, 13)
        Me.Label13.TabIndex = 45
        Me.Label13.Text = "Aug Plan"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(4, 304)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(48, 13)
        Me.Label12.TabIndex = 43
        Me.Label12.Text = "Oct Plan"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(4, 330)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(52, 13)
        Me.Label11.TabIndex = 41
        Me.Label11.Text = "Nov Plan"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(4, 356)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(50, 13)
        Me.Label10.TabIndex = 39
        Me.Label10.Text = "Dec Plan"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(4, 382)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(47, 13)
        Me.Label9.TabIndex = 37
        Me.Label9.Text = "Jan Plan"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(4, 434)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(51, 13)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "Mar Plan"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(4, 200)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(47, 13)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Jun Plan"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(4, 278)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(50, 13)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Sep Plan"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(4, 148)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Apr Plan"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(4, 408)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Feb Plan"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(351, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Cal Year"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPreparedBy
        '
        Me.lblPreparedBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblPreparedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPreparedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPreparedBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPreparedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPreparedBy.Location = New System.Drawing.Point(258, 454)
        Me.lblPreparedBy.Name = "lblPreparedBy"
        Me.lblPreparedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPreparedBy.Size = New System.Drawing.Size(277, 21)
        Me.lblPreparedBy.TabIndex = 23
        '
        'lblProduct
        '
        Me.lblProduct.BackColor = System.Drawing.SystemColors.Control
        Me.lblProduct.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProduct.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProduct.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProduct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProduct.Location = New System.Drawing.Point(258, 64)
        Me.lblProduct.Name = "lblProduct"
        Me.lblProduct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProduct.Size = New System.Drawing.Size(277, 21)
        Me.lblProduct.TabIndex = 22
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.AutoSize = True
        Me._lblLabels_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_0, CType(0, Short))
        Me._lblLabels_0.Location = New System.Drawing.Point(4, 18)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(48, 13)
        Me._lblLabels_0.TabIndex = 21
        Me._lblLabels_0.Text = "Number"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.AutoSize = True
        Me._lblLabels_2.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_2, CType(2, Short))
        Me._lblLabels_2.Location = New System.Drawing.Point(4, 70)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(46, 13)
        Me._lblLabels_2.TabIndex = 20
        Me._lblLabels_2.Text = "Product"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(4, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(36, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Stage"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(4, 460)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(69, 13)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Prepared By"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 13
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 470)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(547, 55)
        Me.FraMovement.TabIndex = 1
        Me.FraMovement.TabStop = False
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.Location = New System.Drawing.Point(366, 14)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.cmdPreview.TabIndex = 7
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPreview.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(186, 14)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdSavePrint.TabIndex = 4
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(545, 475)
        Me.SprdView.TabIndex = 10
        '
        'frmLayoutInspPlan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(547, 526)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.SprdView)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLayoutInspPlan"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Layout Inspection Plan"
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
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