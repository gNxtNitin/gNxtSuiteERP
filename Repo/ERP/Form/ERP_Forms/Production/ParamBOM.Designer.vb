Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamBOM
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
    Public WithEvents txtMainCode As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchProdCode As System.Windows.Forms.Button
    Public WithEvents txtProductCode As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchWEF As System.Windows.Forms.Button
    Public WithEvents txtWEF As System.Windows.Forms.TextBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblAmendNo As System.Windows.Forms.Label
    Public WithEvents lblStatus As System.Windows.Forms.Label
    Public WithEvents lblUOM As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents lblOutputQty As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents lblModel As System.Windows.Forms.Label
    Public WithEvents lblPartNo As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents lblMKey As System.Windows.Forms.Label
    Public WithEvents lblProductDesc As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents chkApproved As System.Windows.Forms.CheckBox
    Public WithEvents chkVendor As System.Windows.Forms.CheckBox
    Public WithEvents chkRate As System.Windows.Forms.CheckBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public CommonDialog1Open As System.Windows.Forms.OpenFileDialog
    Public CommonDialog1Save As System.Windows.Forms.SaveFileDialog
    Public CommonDialog1Font As System.Windows.Forms.FontDialog
    Public CommonDialog1Color As System.Windows.Forms.ColorDialog
    Public CommonDialog1Print As System.Windows.Forms.PrintDialog
    Public WithEvents lblProductCode As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamBOM))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtMainCode = New System.Windows.Forms.TextBox()
        Me.cmdSearchProdCode = New System.Windows.Forms.Button()
        Me.txtProductCode = New System.Windows.Forms.TextBox()
        Me.cmdSearchWEF = New System.Windows.Forms.Button()
        Me.txtWEF = New System.Windows.Forms.TextBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblAmendNo = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblUOM = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblOutputQty = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblModel = New System.Windows.Forms.Label()
        Me.lblPartNo = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.lblProductDesc = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.chkApproved = New System.Windows.Forms.CheckBox()
        Me.chkVendor = New System.Windows.Forms.CheckBox()
        Me.chkRate = New System.Windows.Forms.CheckBox()
        Me.lblProductCode = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtMainCode
        '
        Me.txtMainCode.AcceptsReturn = True
        Me.txtMainCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtMainCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMainCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMainCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMainCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMainCode.Location = New System.Drawing.Point(98, 36)
        Me.txtMainCode.MaxLength = 0
        Me.txtMainCode.Name = "txtMainCode"
        Me.txtMainCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMainCode.Size = New System.Drawing.Size(89, 20)
        Me.txtMainCode.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.txtMainCode, "Press F1 For Help")
        '
        'cmdSearchProdCode
        '
        Me.cmdSearchProdCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchProdCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchProdCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchProdCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchProdCode.Image = CType(resources.GetObject("cmdSearchProdCode.Image"), System.Drawing.Image)
        Me.cmdSearchProdCode.Location = New System.Drawing.Point(188, 10)
        Me.cmdSearchProdCode.Name = "cmdSearchProdCode"
        Me.cmdSearchProdCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchProdCode.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchProdCode.TabIndex = 10
        Me.cmdSearchProdCode.TabStop = False
        Me.cmdSearchProdCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchProdCode, "Search")
        Me.cmdSearchProdCode.UseVisualStyleBackColor = False
        '
        'txtProductCode
        '
        Me.txtProductCode.AcceptsReturn = True
        Me.txtProductCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtProductCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProductCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProductCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProductCode.Location = New System.Drawing.Point(98, 10)
        Me.txtProductCode.MaxLength = 0
        Me.txtProductCode.Name = "txtProductCode"
        Me.txtProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProductCode.Size = New System.Drawing.Size(89, 20)
        Me.txtProductCode.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtProductCode, "Press F1 For Help")
        '
        'cmdSearchWEF
        '
        Me.cmdSearchWEF.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchWEF.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchWEF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchWEF.Image = CType(resources.GetObject("cmdSearchWEF.Image"), System.Drawing.Image)
        Me.cmdSearchWEF.Location = New System.Drawing.Point(718, 10)
        Me.cmdSearchWEF.Name = "cmdSearchWEF"
        Me.cmdSearchWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchWEF.Size = New System.Drawing.Size(19, 19)
        Me.cmdSearchWEF.TabIndex = 8
        Me.cmdSearchWEF.TabStop = False
        Me.cmdSearchWEF.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchWEF, "Search")
        Me.cmdSearchWEF.UseVisualStyleBackColor = False
        '
        'txtWEF
        '
        Me.txtWEF.AcceptsReturn = True
        Me.txtWEF.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEF.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEF.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWEF.Location = New System.Drawing.Point(636, 10)
        Me.txtWEF.MaxLength = 0
        Me.txtWEF.Name = "txtWEF"
        Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEF.Size = New System.Drawing.Size(81, 20)
        Me.txtWEF.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtWEF, "Press F1 For Help")
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(822, 15)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 35)
        Me.cmdClose.TabIndex = 2
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(756, 15)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 35)
        Me.CmdPreview.TabIndex = 4
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Enabled = False
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(690, 15)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 35)
        Me.cmdPrint.TabIndex = 3
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(624, 15)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(67, 35)
        Me.cmdShow.TabIndex = 1
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtMainCode)
        Me.Frame4.Controls.Add(Me.cmdSearchProdCode)
        Me.Frame4.Controls.Add(Me.txtProductCode)
        Me.Frame4.Controls.Add(Me.cmdSearchWEF)
        Me.Frame4.Controls.Add(Me.txtWEF)
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Controls.Add(Me.lblAmendNo)
        Me.Frame4.Controls.Add(Me.lblStatus)
        Me.Frame4.Controls.Add(Me.lblUOM)
        Me.Frame4.Controls.Add(Me.Label7)
        Me.Frame4.Controls.Add(Me.lblOutputQty)
        Me.Frame4.Controls.Add(Me.Label5)
        Me.Frame4.Controls.Add(Me.Label2)
        Me.Frame4.Controls.Add(Me.Label9)
        Me.Frame4.Controls.Add(Me.lblModel)
        Me.Frame4.Controls.Add(Me.lblPartNo)
        Me.Frame4.Controls.Add(Me.Label4)
        Me.Frame4.Controls.Add(Me.lblMKey)
        Me.Frame4.Controls.Add(Me.lblProductDesc)
        Me.Frame4.Controls.Add(Me.Label1)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, -6)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(898, 562)
        Me.Frame4.TabIndex = 5
        Me.Frame4.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 70)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(894, 488)
        Me.SprdMain.TabIndex = 6
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 118)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 21
        '
        'lblAmendNo
        '
        Me.lblAmendNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblAmendNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAmendNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmendNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAmendNo.Location = New System.Drawing.Point(742, 12)
        Me.lblAmendNo.Name = "lblAmendNo"
        Me.lblAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAmendNo.Size = New System.Drawing.Size(21, 13)
        Me.lblAmendNo.TabIndex = 30
        Me.lblAmendNo.Text = "lblAmendNo"
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatus.Location = New System.Drawing.Point(740, 38)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatus.Size = New System.Drawing.Size(21, 13)
        Me.lblStatus.TabIndex = 29
        Me.lblStatus.Text = "lblStatus"
        '
        'lblUOM
        '
        Me.lblUOM.BackColor = System.Drawing.SystemColors.Control
        Me.lblUOM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUOM.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblUOM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUOM.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUOM.Location = New System.Drawing.Point(348, 34)
        Me.lblUOM.Name = "lblUOM"
        Me.lblUOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblUOM.Size = New System.Drawing.Size(35, 19)
        Me.lblUOM.TabIndex = 27
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(216, 38)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(65, 14)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "Output Qty :"
        '
        'lblOutputQty
        '
        Me.lblOutputQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblOutputQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOutputQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblOutputQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOutputQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOutputQty.Location = New System.Drawing.Point(286, 34)
        Me.lblOutputQty.Name = "lblOutputQty"
        Me.lblOutputQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblOutputQty.Size = New System.Drawing.Size(61, 19)
        Me.lblOutputQty.TabIndex = 25
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(23, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(63, 14)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Main Code :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(567, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(57, 14)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Model No :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(582, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(42, 14)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "W.E.F. :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModel
        '
        Me.lblModel.BackColor = System.Drawing.SystemColors.Control
        Me.lblModel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModel.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModel.Location = New System.Drawing.Point(636, 34)
        Me.lblModel.Name = "lblModel"
        Me.lblModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModel.Size = New System.Drawing.Size(81, 19)
        Me.lblModel.TabIndex = 16
        '
        'lblPartNo
        '
        Me.lblPartNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblPartNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPartNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPartNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPartNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPartNo.Location = New System.Drawing.Point(450, 34)
        Me.lblPartNo.Name = "lblPartNo"
        Me.lblPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPartNo.Size = New System.Drawing.Size(99, 19)
        Me.lblPartNo.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(394, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(48, 14)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Part No :"
        '
        'lblMKey
        '
        Me.lblMKey.AutoSize = True
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Location = New System.Drawing.Point(514, 36)
        Me.lblMKey.Name = "lblMKey"
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.Size = New System.Drawing.Size(44, 14)
        Me.lblMKey.TabIndex = 13
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblMKey.Visible = False
        '
        'lblProductDesc
        '
        Me.lblProductDesc.BackColor = System.Drawing.SystemColors.Control
        Me.lblProductDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProductDesc.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProductDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductDesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProductDesc.Location = New System.Drawing.Point(216, 10)
        Me.lblProductDesc.Name = "lblProductDesc"
        Me.lblProductDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProductDesc.Size = New System.Drawing.Size(333, 19)
        Me.lblProductDesc.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(78, 14)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Product Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.chkApproved)
        Me.Frame3.Controls.Add(Me.chkVendor)
        Me.Frame3.Controls.Add(Me.chkRate)
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdShow)
        Me.Frame3.Controls.Add(Me.lblProductCode)
        Me.Frame3.Controls.Add(Me.Label3)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(-1, 553)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(903, 57)
        Me.Frame3.TabIndex = 0
        Me.Frame3.TabStop = False
        '
        'chkApproved
        '
        Me.chkApproved.AutoSize = True
        Me.chkApproved.BackColor = System.Drawing.SystemColors.Control
        Me.chkApproved.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkApproved.Enabled = False
        Me.chkApproved.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkApproved.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkApproved.Location = New System.Drawing.Point(266, 26)
        Me.chkApproved.Name = "chkApproved"
        Me.chkApproved.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkApproved.Size = New System.Drawing.Size(74, 18)
        Me.chkApproved.TabIndex = 28
        Me.chkApproved.Text = "Approved"
        Me.chkApproved.UseVisualStyleBackColor = False
        '
        'chkVendor
        '
        Me.chkVendor.AutoSize = True
        Me.chkVendor.BackColor = System.Drawing.SystemColors.Control
        Me.chkVendor.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkVendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVendor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkVendor.Location = New System.Drawing.Point(449, 26)
        Me.chkVendor.Name = "chkVendor"
        Me.chkVendor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkVendor.Size = New System.Drawing.Size(140, 18)
        Me.chkVendor.TabIndex = 23
        Me.chkVendor.Text = "Vendor Name Required."
        Me.chkVendor.UseVisualStyleBackColor = False
        '
        'chkRate
        '
        Me.chkRate.AutoSize = True
        Me.chkRate.BackColor = System.Drawing.SystemColors.Control
        Me.chkRate.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRate.Location = New System.Drawing.Point(346, 26)
        Me.chkRate.Name = "chkRate"
        Me.chkRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRate.Size = New System.Drawing.Size(94, 18)
        Me.chkRate.TabIndex = 22
        Me.chkRate.Text = "Rate Required"
        Me.chkRate.UseVisualStyleBackColor = False
        '
        'lblProductCode
        '
        Me.lblProductCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblProductCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProductCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProductCode.Location = New System.Drawing.Point(16, 9)
        Me.lblProductCode.Name = "lblProductCode"
        Me.lblProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProductCode.Size = New System.Drawing.Size(93, 15)
        Me.lblProductCode.TabIndex = 24
        Me.lblProductCode.Text = "lblProductCode"
        Me.lblProductCode.Visible = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(3, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(240, 19)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "(*)  OR (**) - Denoted Alternate Item Code"
        '
        'frmParamBOM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 16)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamBOM"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Print BOM"
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class