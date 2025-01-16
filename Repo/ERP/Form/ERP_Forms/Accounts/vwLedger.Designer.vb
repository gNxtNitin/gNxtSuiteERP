Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmViewLedger
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
        'Me.MDIParent = AccountGST.Master
        'AccountGST.Master.Show
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
    Public WithEvents chkShowExp As System.Windows.Forms.CheckBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents _chkGroup_8 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_5 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_2 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_4 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_1 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_6 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_3 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_0 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_7 As System.Windows.Forms.CheckBox
    Public WithEvents FraOption As System.Windows.Forms.GroupBox
    Public WithEvents _OptSumDet_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSumDet_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSumDet_2 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents _Lbl_7 As System.Windows.Forms.Label
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents chkAllAccount As System.Windows.Forms.CheckBox
    Public WithEvents cmdAgtsearch As System.Windows.Forms.Button
    Public WithEvents TxtAgtAccount As System.Windows.Forms.TextBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents cmdOptional As System.Windows.Forms.Button
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_5 As System.Windows.Forms.Label
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents ChkWithRunBal As System.Windows.Forms.CheckBox
    Public WithEvents chkOption As System.Windows.Forms.CheckBox
    Public WithEvents txtCondAmount As System.Windows.Forms.TextBox
    Public WithEvents cboCond As System.Windows.Forms.ComboBox
    Public WithEvents FraAmountCond As System.Windows.Forms.GroupBox
    Public WithEvents FraConditional As System.Windows.Forms.GroupBox
    Public WithEvents cboExpHead As System.Windows.Forms.ComboBox
    Public WithEvents CboDept As System.Windows.Forms.ComboBox
    Public WithEvents CboCC As System.Windows.Forms.ComboBox
    Public WithEvents cboEmp As System.Windows.Forms.ComboBox
    Public WithEvents _Lbl_6 As System.Windows.Forms.Label
    Public WithEvents _Lbl_3 As System.Windows.Forms.Label
    Public WithEvents _Lbl_2 As System.Windows.Forms.Label
    Public WithEvents _Lbl_4 As System.Windows.Forms.Label
    Public WithEvents FraOthers As System.Windows.Forms.GroupBox
    Public WithEvents _OptOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents lblShow As System.Windows.Forms.Label
    Public WithEvents lblPrintCount As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblAcCode As System.Windows.Forms.Label
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents OptOrderBy As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents OptSumDet As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents chkGroup As Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewLedger))
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdAgtsearch = New System.Windows.Forms.Button()
        Me.TxtAgtAccount = New System.Windows.Forms.TextBox()
        Me.cmdOptional = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.ChkWithRunBal = New System.Windows.Forms.CheckBox()
        Me.cmdExport = New System.Windows.Forms.Button()
        Me.cmdOutstanding = New System.Windows.Forms.Button()
        Me.cmdMasterDetail = New System.Windows.Forms.Button()
        Me.txtCreditLimit = New System.Windows.Forms.TextBox()
        Me.txtPaymentTerms = New System.Windows.Forms.TextBox()
        Me.txtSecurityDeposit = New System.Windows.Forms.TextBox()
        Me.txtSecurityAmount = New System.Windows.Forms.TextBox()
        Me.txtSecurityChqNo = New System.Windows.Forms.TextBox()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.txtSaleRep = New System.Windows.Forms.TextBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.chkShowExp = New System.Windows.Forms.CheckBox()
        Me.FraOption = New System.Windows.Forms.GroupBox()
        Me._chkGroup_8 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_5 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_2 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_4 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_1 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_6 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_3 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_0 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_7 = New System.Windows.Forms.CheckBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._OptSumDet_0 = New System.Windows.Forms.RadioButton()
        Me._OptSumDet_1 = New System.Windows.Forms.RadioButton()
        Me._OptSumDet_2 = New System.Windows.Forms.RadioButton()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me._Lbl_7 = New System.Windows.Forms.Label()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.optGroup = New System.Windows.Forms.RadioButton()
        Me.optAccount = New System.Windows.Forms.RadioButton()
        Me.cboAccount = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.chkAllAccount = New System.Windows.Forms.CheckBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_5 = New System.Windows.Forms.Label()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.FraOthers = New System.Windows.Forms.GroupBox()
        Me.FraConditional = New System.Windows.Forms.GroupBox()
        Me.chkOption = New System.Windows.Forms.CheckBox()
        Me.FraAmountCond = New System.Windows.Forms.GroupBox()
        Me.txtCondAmount = New System.Windows.Forms.TextBox()
        Me.cboCond = New System.Windows.Forms.ComboBox()
        Me.cboExpHead = New System.Windows.Forms.ComboBox()
        Me.CboDept = New System.Windows.Forms.ComboBox()
        Me.CboCC = New System.Windows.Forms.ComboBox()
        Me.cboEmp = New System.Windows.Forms.ComboBox()
        Me._Lbl_6 = New System.Windows.Forms.Label()
        Me._Lbl_3 = New System.Windows.Forms.Label()
        Me._Lbl_2 = New System.Windows.Forms.Label()
        Me._Lbl_4 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._OptOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me.lblShow = New System.Windows.Forms.Label()
        Me.lblPrintCount = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblAcCode = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptSumDet = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.chkGroup = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstCompanyName = New System.Windows.Forms.CheckedListBox()
        Me.UltraDataSource1 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.UltraGridExcelExporter1 = New Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(Me.components)
        Me.chkAdjustDetail = New System.Windows.Forms.CheckBox()
        Me.fraMasterDetail = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame5.SuspendLayout()
        Me.FraOption.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        CType(Me.cboAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.FraOthers.SuspendLayout()
        Me.FraConditional.SuspendLayout()
        Me.FraAmountCond.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptSumDet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.UltraDataSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraMasterDetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdAgtsearch
        '
        Me.cmdAgtsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAgtsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAgtsearch.Enabled = False
        Me.cmdAgtsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAgtsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAgtsearch.Image = CType(resources.GetObject("cmdAgtsearch.Image"), System.Drawing.Image)
        Me.cmdAgtsearch.Location = New System.Drawing.Point(349, 17)
        Me.cmdAgtsearch.Name = "cmdAgtsearch"
        Me.cmdAgtsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAgtsearch.Size = New System.Drawing.Size(29, 22)
        Me.cmdAgtsearch.TabIndex = 13
        Me.cmdAgtsearch.TabStop = False
        Me.cmdAgtsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAgtsearch, "Search")
        Me.cmdAgtsearch.UseVisualStyleBackColor = False
        '
        'TxtAgtAccount
        '
        Me.TxtAgtAccount.AcceptsReturn = True
        Me.TxtAgtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtAgtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAgtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtAgtAccount.Enabled = False
        Me.TxtAgtAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAgtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAgtAccount.Location = New System.Drawing.Point(11, 18)
        Me.TxtAgtAccount.MaxLength = 0
        Me.TxtAgtAccount.Name = "TxtAgtAccount"
        Me.TxtAgtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAgtAccount.Size = New System.Drawing.Size(337, 20)
        Me.TxtAgtAccount.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.TxtAgtAccount, "Press F1 For Help")
        '
        'cmdOptional
        '
        Me.cmdOptional.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOptional.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOptional.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOptional.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOptional.Image = CType(resources.GetObject("cmdOptional.Image"), System.Drawing.Image)
        Me.cmdOptional.Location = New System.Drawing.Point(66, 64)
        Me.cmdOptional.Name = "cmdOptional"
        Me.cmdOptional.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOptional.Size = New System.Drawing.Size(53, 21)
        Me.cmdOptional.TabIndex = 44
        Me.cmdOptional.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdOptional, "Show Record")
        Me.cmdOptional.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdPreview.Location = New System.Drawing.Point(137, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 25
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
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(71, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 24
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClose.Location = New System.Drawing.Point(271, 11)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 26
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdShow.Location = New System.Drawing.Point(4, 11)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(67, 37)
        Me.cmdShow.TabIndex = 23
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'ChkWithRunBal
        '
        Me.ChkWithRunBal.BackColor = System.Drawing.SystemColors.Control
        Me.ChkWithRunBal.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkWithRunBal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkWithRunBal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkWithRunBal.Location = New System.Drawing.Point(0, 64)
        Me.ChkWithRunBal.Name = "ChkWithRunBal"
        Me.ChkWithRunBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkWithRunBal.Size = New System.Drawing.Size(119, 13)
        Me.ChkWithRunBal.TabIndex = 42
        Me.ChkWithRunBal.Text = "Running Balance"
        Me.ToolTip1.SetToolTip(Me.ChkWithRunBal, "Selecting this option may cause slow reporting")
        Me.ChkWithRunBal.UseVisualStyleBackColor = False
        Me.ChkWithRunBal.Visible = False
        '
        'cmdExport
        '
        Me.cmdExport.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExport.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExport.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExport.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExport.Location = New System.Drawing.Point(204, 11)
        Me.cmdExport.Name = "cmdExport"
        Me.cmdExport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExport.Size = New System.Drawing.Size(67, 37)
        Me.cmdExport.TabIndex = 27
        Me.cmdExport.Text = "&Export"
        Me.cmdExport.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdExport, "Excel Export")
        Me.cmdExport.UseVisualStyleBackColor = False
        '
        'cmdOutstanding
        '
        Me.cmdOutstanding.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOutstanding.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOutstanding.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOutstanding.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOutstanding.Location = New System.Drawing.Point(903, 9)
        Me.cmdOutstanding.Name = "cmdOutstanding"
        Me.cmdOutstanding.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOutstanding.Size = New System.Drawing.Size(86, 29)
        Me.cmdOutstanding.TabIndex = 65
        Me.cmdOutstanding.Text = "&Outstanding"
        Me.ToolTip1.SetToolTip(Me.cmdOutstanding, "Outstanding")
        Me.cmdOutstanding.UseVisualStyleBackColor = False
        '
        'cmdMasterDetail
        '
        Me.cmdMasterDetail.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMasterDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMasterDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMasterDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMasterDetail.Location = New System.Drawing.Point(903, 38)
        Me.cmdMasterDetail.Name = "cmdMasterDetail"
        Me.cmdMasterDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMasterDetail.Size = New System.Drawing.Size(86, 29)
        Me.cmdMasterDetail.TabIndex = 67
        Me.cmdMasterDetail.Text = "&Master Details"
        Me.ToolTip1.SetToolTip(Me.cmdMasterDetail, "Master Details")
        Me.cmdMasterDetail.UseVisualStyleBackColor = False
        '
        'txtCreditLimit
        '
        Me.txtCreditLimit.AcceptsReturn = True
        Me.txtCreditLimit.BackColor = System.Drawing.SystemColors.Window
        Me.txtCreditLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCreditLimit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCreditLimit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreditLimit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCreditLimit.Location = New System.Drawing.Point(132, 13)
        Me.txtCreditLimit.MaxLength = 0
        Me.txtCreditLimit.Name = "txtCreditLimit"
        Me.txtCreditLimit.ReadOnly = True
        Me.txtCreditLimit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCreditLimit.Size = New System.Drawing.Size(205, 20)
        Me.txtCreditLimit.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtCreditLimit, "Press F1 For Help")
        '
        'txtPaymentTerms
        '
        Me.txtPaymentTerms.AcceptsReturn = True
        Me.txtPaymentTerms.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaymentTerms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaymentTerms.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaymentTerms.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentTerms.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPaymentTerms.Location = New System.Drawing.Point(132, 36)
        Me.txtPaymentTerms.MaxLength = 0
        Me.txtPaymentTerms.Name = "txtPaymentTerms"
        Me.txtPaymentTerms.ReadOnly = True
        Me.txtPaymentTerms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaymentTerms.Size = New System.Drawing.Size(205, 20)
        Me.txtPaymentTerms.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.txtPaymentTerms, "Press F1 For Help")
        '
        'txtSecurityDeposit
        '
        Me.txtSecurityDeposit.AcceptsReturn = True
        Me.txtSecurityDeposit.BackColor = System.Drawing.SystemColors.Window
        Me.txtSecurityDeposit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSecurityDeposit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSecurityDeposit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSecurityDeposit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSecurityDeposit.Location = New System.Drawing.Point(132, 59)
        Me.txtSecurityDeposit.MaxLength = 0
        Me.txtSecurityDeposit.Name = "txtSecurityDeposit"
        Me.txtSecurityDeposit.ReadOnly = True
        Me.txtSecurityDeposit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSecurityDeposit.Size = New System.Drawing.Size(205, 20)
        Me.txtSecurityDeposit.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtSecurityDeposit, "Press F1 For Help")
        '
        'txtSecurityAmount
        '
        Me.txtSecurityAmount.AcceptsReturn = True
        Me.txtSecurityAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtSecurityAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSecurityAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSecurityAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSecurityAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSecurityAmount.Location = New System.Drawing.Point(132, 82)
        Me.txtSecurityAmount.MaxLength = 0
        Me.txtSecurityAmount.Name = "txtSecurityAmount"
        Me.txtSecurityAmount.ReadOnly = True
        Me.txtSecurityAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSecurityAmount.Size = New System.Drawing.Size(205, 20)
        Me.txtSecurityAmount.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtSecurityAmount, "Press F1 For Help")
        '
        'txtSecurityChqNo
        '
        Me.txtSecurityChqNo.AcceptsReturn = True
        Me.txtSecurityChqNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSecurityChqNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSecurityChqNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSecurityChqNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSecurityChqNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSecurityChqNo.Location = New System.Drawing.Point(132, 105)
        Me.txtSecurityChqNo.MaxLength = 0
        Me.txtSecurityChqNo.Name = "txtSecurityChqNo"
        Me.txtSecurityChqNo.ReadOnly = True
        Me.txtSecurityChqNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSecurityChqNo.Size = New System.Drawing.Size(205, 20)
        Me.txtSecurityChqNo.TabIndex = 60
        Me.ToolTip1.SetToolTip(Me.txtSecurityChqNo, "Press F1 For Help")
        '
        'txtBankName
        '
        Me.txtBankName.AcceptsReturn = True
        Me.txtBankName.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBankName.Location = New System.Drawing.Point(132, 128)
        Me.txtBankName.MaxLength = 0
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.ReadOnly = True
        Me.txtBankName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankName.Size = New System.Drawing.Size(205, 20)
        Me.txtBankName.TabIndex = 62
        Me.ToolTip1.SetToolTip(Me.txtBankName, "Press F1 For Help")
        '
        'txtSaleRep
        '
        Me.txtSaleRep.AcceptsReturn = True
        Me.txtSaleRep.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaleRep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSaleRep.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaleRep.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleRep.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSaleRep.Location = New System.Drawing.Point(132, 151)
        Me.txtSaleRep.MaxLength = 0
        Me.txtSaleRep.Name = "txtSaleRep"
        Me.txtSaleRep.ReadOnly = True
        Me.txtSaleRep.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaleRep.Size = New System.Drawing.Size(205, 20)
        Me.txtSaleRep.TabIndex = 64
        Me.ToolTip1.SetToolTip(Me.txtSaleRep, "Press F1 For Help")
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.chkShowExp)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(176, 564)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(102, 46)
        Me.Frame5.TabIndex = 59
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Show Only"
        '
        'chkShowExp
        '
        Me.chkShowExp.BackColor = System.Drawing.SystemColors.Control
        Me.chkShowExp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShowExp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowExp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShowExp.Location = New System.Drawing.Point(4, 12)
        Me.chkShowExp.Name = "chkShowExp"
        Me.chkShowExp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShowExp.Size = New System.Drawing.Size(86, 34)
        Me.chkShowExp.TabIndex = 60
        Me.chkShowExp.Text = "VDate && Exp Date Mismatch"
        Me.chkShowExp.UseVisualStyleBackColor = False
        '
        'FraOption
        '
        Me.FraOption.BackColor = System.Drawing.SystemColors.Control
        Me.FraOption.Controls.Add(Me._chkGroup_8)
        Me.FraOption.Controls.Add(Me._chkGroup_5)
        Me.FraOption.Controls.Add(Me._chkGroup_2)
        Me.FraOption.Controls.Add(Me._chkGroup_4)
        Me.FraOption.Controls.Add(Me._chkGroup_1)
        Me.FraOption.Controls.Add(Me._chkGroup_6)
        Me.FraOption.Controls.Add(Me._chkGroup_3)
        Me.FraOption.Controls.Add(Me._chkGroup_0)
        Me.FraOption.Controls.Add(Me._chkGroup_7)
        Me.FraOption.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOption.Location = New System.Drawing.Point(0, 109)
        Me.FraOption.Name = "FraOption"
        Me.FraOption.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOption.Size = New System.Drawing.Size(346, 53)
        Me.FraOption.TabIndex = 30
        Me.FraOption.TabStop = False
        '
        '_chkGroup_8
        '
        Me._chkGroup_8.AutoSize = True
        Me._chkGroup_8.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_8.Checked = True
        Me._chkGroup_8.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_8, CType(8, Short))
        Me._chkGroup_8.Location = New System.Drawing.Point(7, 32)
        Me._chkGroup_8.Name = "_chkGroup_8"
        Me._chkGroup_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_8.Size = New System.Drawing.Size(48, 18)
        Me._chkGroup_8.TabIndex = 7
        Me._chkGroup_8.Text = "PDC"
        Me._chkGroup_8.UseVisualStyleBackColor = False
        '
        '_chkGroup_5
        '
        Me._chkGroup_5.AutoSize = True
        Me._chkGroup_5.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_5.Checked = True
        Me._chkGroup_5.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_5, CType(5, Short))
        Me._chkGroup_5.Location = New System.Drawing.Point(158, 32)
        Me._chkGroup_5.Name = "_chkGroup_5"
        Me._chkGroup_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_5.Size = New System.Drawing.Size(88, 18)
        Me._chkGroup_5.TabIndex = 10
        Me._chkGroup_5.Text = "Credit Note"
        Me._chkGroup_5.UseVisualStyleBackColor = False
        '
        '_chkGroup_2
        '
        Me._chkGroup_2.AutoSize = True
        Me._chkGroup_2.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_2.Checked = True
        Me._chkGroup_2.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_2, CType(2, Short))
        Me._chkGroup_2.Location = New System.Drawing.Point(258, 32)
        Me._chkGroup_2.Name = "_chkGroup_2"
        Me._chkGroup_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_2.Size = New System.Drawing.Size(49, 18)
        Me._chkGroup_2.TabIndex = 6
        Me._chkGroup_2.Text = "Sale"
        Me._chkGroup_2.UseVisualStyleBackColor = False
        '
        '_chkGroup_4
        '
        Me._chkGroup_4.AutoSize = True
        Me._chkGroup_4.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_4.Checked = True
        Me._chkGroup_4.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_4, CType(4, Short))
        Me._chkGroup_4.Location = New System.Drawing.Point(158, 13)
        Me._chkGroup_4.Name = "_chkGroup_4"
        Me._chkGroup_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_4.Size = New System.Drawing.Size(82, 18)
        Me._chkGroup_4.TabIndex = 9
        Me._chkGroup_4.Text = "Debit Note"
        Me._chkGroup_4.UseVisualStyleBackColor = False
        '
        '_chkGroup_1
        '
        Me._chkGroup_1.AutoSize = True
        Me._chkGroup_1.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_1.Checked = True
        Me._chkGroup_1.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_1, CType(1, Short))
        Me._chkGroup_1.Location = New System.Drawing.Point(67, 13)
        Me._chkGroup_1.Name = "_chkGroup_1"
        Me._chkGroup_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_1.Size = New System.Drawing.Size(54, 18)
        Me._chkGroup_1.TabIndex = 5
        Me._chkGroup_1.Text = "Cash"
        Me._chkGroup_1.UseVisualStyleBackColor = False
        '
        '_chkGroup_6
        '
        Me._chkGroup_6.AutoSize = True
        Me._chkGroup_6.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_6.Checked = True
        Me._chkGroup_6.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_6, CType(6, Short))
        Me._chkGroup_6.Location = New System.Drawing.Point(258, 13)
        Me._chkGroup_6.Name = "_chkGroup_6"
        Me._chkGroup_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_6.Size = New System.Drawing.Size(67, 18)
        Me._chkGroup_6.TabIndex = 11
        Me._chkGroup_6.Text = "Journal"
        Me._chkGroup_6.UseVisualStyleBackColor = False
        '
        '_chkGroup_3
        '
        Me._chkGroup_3.AutoSize = True
        Me._chkGroup_3.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_3.Checked = True
        Me._chkGroup_3.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_3, CType(3, Short))
        Me._chkGroup_3.Location = New System.Drawing.Point(67, 32)
        Me._chkGroup_3.Name = "_chkGroup_3"
        Me._chkGroup_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_3.Size = New System.Drawing.Size(78, 18)
        Me._chkGroup_3.TabIndex = 8
        Me._chkGroup_3.Text = "Purchase"
        Me._chkGroup_3.UseVisualStyleBackColor = False
        '
        '_chkGroup_0
        '
        Me._chkGroup_0.AutoSize = True
        Me._chkGroup_0.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_0.Checked = True
        Me._chkGroup_0.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_0, CType(0, Short))
        Me._chkGroup_0.Location = New System.Drawing.Point(7, 13)
        Me._chkGroup_0.Name = "_chkGroup_0"
        Me._chkGroup_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_0.Size = New System.Drawing.Size(53, 18)
        Me._chkGroup_0.TabIndex = 4
        Me._chkGroup_0.Text = "Bank"
        Me._chkGroup_0.UseVisualStyleBackColor = False
        '
        '_chkGroup_7
        '
        Me._chkGroup_7.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_7.Checked = True
        Me._chkGroup_7.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_7, CType(7, Short))
        Me._chkGroup_7.Location = New System.Drawing.Point(268, 15)
        Me._chkGroup_7.Name = "_chkGroup_7"
        Me._chkGroup_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_7.Size = New System.Drawing.Size(71, 13)
        Me._chkGroup_7.TabIndex = 15
        Me._chkGroup_7.Text = "Contra"
        Me._chkGroup_7.UseVisualStyleBackColor = False
        Me._chkGroup_7.Visible = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._OptSumDet_0)
        Me.Frame2.Controls.Add(Me._OptSumDet_1)
        Me.Frame2.Controls.Add(Me._OptSumDet_2)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(348, 109)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(207, 53)
        Me.Frame2.TabIndex = 34
        Me.Frame2.TabStop = False
        '
        '_OptSumDet_0
        '
        Me._OptSumDet_0.AutoSize = True
        Me._OptSumDet_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptSumDet_0.Checked = True
        Me._OptSumDet_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSumDet_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSumDet_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSumDet.SetIndex(Me._OptSumDet_0, CType(0, Short))
        Me._OptSumDet_0.Location = New System.Drawing.Point(7, 20)
        Me._OptSumDet_0.Name = "_OptSumDet_0"
        Me._OptSumDet_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_0.Size = New System.Drawing.Size(69, 18)
        Me._OptSumDet_0.TabIndex = 19
        Me._OptSumDet_0.TabStop = True
        Me._OptSumDet_0.Text = "Detailed"
        Me._OptSumDet_0.UseVisualStyleBackColor = False
        '
        '_OptSumDet_1
        '
        Me._OptSumDet_1.AutoSize = True
        Me._OptSumDet_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptSumDet_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSumDet_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSumDet_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSumDet.SetIndex(Me._OptSumDet_1, CType(1, Short))
        Me._OptSumDet_1.Location = New System.Drawing.Point(81, 21)
        Me._OptSumDet_1.Name = "_OptSumDet_1"
        Me._OptSumDet_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_1.Size = New System.Drawing.Size(50, 18)
        Me._OptSumDet_1.TabIndex = 20
        Me._OptSumDet_1.TabStop = True
        Me._OptSumDet_1.Text = "Daily"
        Me._OptSumDet_1.UseVisualStyleBackColor = False
        '
        '_OptSumDet_2
        '
        Me._OptSumDet_2.AutoSize = True
        Me._OptSumDet_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptSumDet_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSumDet_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSumDet_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSumDet.SetIndex(Me._OptSumDet_2, CType(2, Short))
        Me._OptSumDet_2.Location = New System.Drawing.Point(131, 21)
        Me._OptSumDet_2.Name = "_OptSumDet_2"
        Me._OptSumDet_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_2.Size = New System.Drawing.Size(69, 18)
        Me._OptSumDet_2.TabIndex = 21
        Me._OptSumDet_2.TabStop = True
        Me._OptSumDet_2.Text = "Monthly"
        Me._OptSumDet_2.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.cboDivision)
        Me.Frame7.Controls.Add(Me._Lbl_7)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(557, 109)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(435, 53)
        Me.Frame7.TabIndex = 53
        Me.Frame7.TabStop = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(66, 18)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(366, 22)
        Me.cboDivision.TabIndex = 54
        '
        '_Lbl_7
        '
        Me._Lbl_7.AutoSize = True
        Me._Lbl_7.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_7, CType(7, Short))
        Me._Lbl_7.Location = New System.Drawing.Point(8, 22)
        Me._Lbl_7.Name = "_Lbl_7"
        Me._Lbl_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_7.Size = New System.Drawing.Size(56, 14)
        Me._Lbl_7.TabIndex = 55
        Me._Lbl_7.Text = "Division :"
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.optGroup)
        Me.FraAccount.Controls.Add(Me.optAccount)
        Me.FraAccount.Controls.Add(Me.cboAccount)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(131, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(425, 67)
        Me.FraAccount.TabIndex = 39
        Me.FraAccount.TabStop = False
        Me.FraAccount.Text = "Account"
        '
        'optGroup
        '
        Me.optGroup.AutoSize = True
        Me.optGroup.BackColor = System.Drawing.SystemColors.Control
        Me.optGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.optGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optGroup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGroup.Location = New System.Drawing.Point(238, 13)
        Me.optGroup.Name = "optGroup"
        Me.optGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optGroup.Size = New System.Drawing.Size(89, 18)
        Me.optGroup.TabIndex = 120
        Me.optGroup.Text = "Group Wise"
        Me.optGroup.UseVisualStyleBackColor = False
        '
        'optAccount
        '
        Me.optAccount.AutoSize = True
        Me.optAccount.BackColor = System.Drawing.SystemColors.Control
        Me.optAccount.Checked = True
        Me.optAccount.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAccount.Location = New System.Drawing.Point(118, 13)
        Me.optAccount.Name = "optAccount"
        Me.optAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAccount.Size = New System.Drawing.Size(100, 18)
        Me.optAccount.TabIndex = 119
        Me.optAccount.TabStop = True
        Me.optAccount.Text = "Account Wise"
        Me.optAccount.UseVisualStyleBackColor = False
        '
        'cboAccount
        '
        Me.cboAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cboAccount.AutoSize = False
        Me.cboAccount.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Appearance1.BackColor = System.Drawing.SystemColors.Window
        Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cboAccount.DisplayLayout.Appearance = Appearance1
        Me.cboAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cboAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.BorderColor = System.Drawing.SystemColors.Window
        Me.cboAccount.DisplayLayout.GroupByBox.Appearance = Appearance2
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cboAccount.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
        Me.cboAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance4.BackColor2 = System.Drawing.SystemColors.Control
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cboAccount.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
        Me.cboAccount.DisplayLayout.MaxColScrollRegions = 1
        Me.cboAccount.DisplayLayout.MaxRowScrollRegions = 1
        Appearance5.BackColor = System.Drawing.SystemColors.Window
        Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboAccount.DisplayLayout.Override.ActiveCellAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.SystemColors.Highlight
        Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cboAccount.DisplayLayout.Override.ActiveRowAppearance = Appearance6
        Me.cboAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cboAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance7.BackColor = System.Drawing.SystemColors.Window
        Me.cboAccount.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cboAccount.DisplayLayout.Override.CellAppearance = Appearance8
        Me.cboAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cboAccount.DisplayLayout.Override.CellPadding = 0
        Appearance9.BackColor = System.Drawing.SystemColors.Control
        Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Window
        Me.cboAccount.DisplayLayout.Override.GroupByRowAppearance = Appearance9
        Appearance10.TextHAlignAsString = "Left"
        Me.cboAccount.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.cboAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cboAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.cboAccount.DisplayLayout.Override.RowAppearance = Appearance11
        Me.cboAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cboAccount.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
        Me.cboAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cboAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cboAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cboAccount.Font = New System.Drawing.Font("Verdana", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAccount.Location = New System.Drawing.Point(11, 38)
        Me.cboAccount.Name = "cboAccount"
        Me.cboAccount.Size = New System.Drawing.Size(407, 20)
        Me.cboAccount.TabIndex = 116
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.chkAllAccount)
        Me.Frame3.Controls.Add(Me.cmdAgtsearch)
        Me.Frame3.Controls.Add(Me.TxtAgtAccount)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(131, 66)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(425, 47)
        Me.Frame3.TabIndex = 43
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Against Account Name"
        '
        'chkAllAccount
        '
        Me.chkAllAccount.AutoSize = True
        Me.chkAllAccount.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllAccount.Checked = True
        Me.chkAllAccount.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllAccount.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllAccount.Location = New System.Drawing.Point(381, 20)
        Me.chkAllAccount.Name = "chkAllAccount"
        Me.chkAllAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllAccount.Size = New System.Drawing.Size(48, 18)
        Me.chkAllAccount.TabIndex = 14
        Me.chkAllAccount.Text = "ALL"
        Me.chkAllAccount.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.cmdOptional)
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me.txtDateFrom)
        Me.Frame6.Controls.Add(Me._Lbl_5)
        Me.Frame6.Controls.Add(Me._Lbl_1)
        Me.Frame6.Controls.Add(Me._Lbl_0)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(128, 111)
        Me.Frame6.TabIndex = 27
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(45, 34)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(78, 20)
        Me.txtDateTo.TabIndex = 1
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(45, 11)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(78, 20)
        Me.txtDateFrom.TabIndex = 0
        '
        '_Lbl_5
        '
        Me._Lbl_5.AutoSize = True
        Me._Lbl_5.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_5, CType(5, Short))
        Me._Lbl_5.Location = New System.Drawing.Point(6, 66)
        Me._Lbl_5.Name = "_Lbl_5"
        Me._Lbl_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_5.Size = New System.Drawing.Size(58, 14)
        Me._Lbl_5.TabIndex = 45
        Me._Lbl_5.Text = "Optional :"
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(16, 37)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(26, 14)
        Me._Lbl_1.TabIndex = 29
        Me._Lbl_1.Text = "To :"
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(4, 14)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(42, 14)
        Me._Lbl_0.TabIndex = 28
        Me._Lbl_0.Text = "From :"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.UltraGrid1)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 159)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(992, 404)
        Me.Frame4.TabIndex = 31
        Me.Frame4.TabStop = False
        '
        'UltraGrid1
        '
        Appearance13.BackColor = System.Drawing.SystemColors.ScrollBar
        Appearance13.BorderColor = System.Drawing.Color.White
        Me.UltraGrid1.DisplayLayout.Appearance = Appearance13
        Me.UltraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance14.BackColor = System.Drawing.Color.PaleGreen
        Appearance14.BackColor2 = System.Drawing.Color.White
        Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance14.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.GroupByBox.Appearance = Appearance14
        Appearance15.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance15
        Me.UltraGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance16.BackColor2 = System.Drawing.SystemColors.Control
        Appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance16.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.PromptAppearance = Appearance16
        Me.UltraGrid1.DisplayLayout.MaxColScrollRegions = 1
        Me.UltraGrid1.DisplayLayout.MaxRowScrollRegions = 1
        Appearance17.BackColor = System.Drawing.SystemColors.Window
        Appearance17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.UltraGrid1.DisplayLayout.Override.ActiveCellAppearance = Appearance17
        Appearance18.BackColor = System.Drawing.SystemColors.Highlight
        Appearance18.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.UltraGrid1.DisplayLayout.Override.ActiveRowAppearance = Appearance18
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance19.BackColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.CardAreaAppearance = Appearance19
        Appearance20.BorderColor = System.Drawing.Color.Silver
        Appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.UltraGrid1.DisplayLayout.Override.CellAppearance = Appearance20
        Me.UltraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.UltraGrid1.DisplayLayout.Override.CellPadding = 0
        Appearance21.BackColor = System.Drawing.SystemColors.Control
        Appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance21.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.GroupByRowAppearance = Appearance21
        Appearance22.TextHAlignAsString = "Left"
        Me.UltraGrid1.DisplayLayout.Override.HeaderAppearance = Appearance22
        Me.UltraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.UltraGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance23.BackColor = System.Drawing.SystemColors.Window
        Appearance23.BorderColor = System.Drawing.Color.Silver
        Me.UltraGrid1.DisplayLayout.Override.RowAppearance = Appearance23
        Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance24.BackColor = System.Drawing.SystemColors.ControlLight
        Me.UltraGrid1.DisplayLayout.Override.TemplateAddRowAppearance = Appearance24
        Me.UltraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.UltraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.UltraGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.UltraGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGrid1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGrid1.Location = New System.Drawing.Point(0, 13)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(992, 391)
        Me.UltraGrid1.TabIndex = 24
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 70)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 23
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdExport)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(646, 562)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(344, 49)
        Me.FraMovement.TabIndex = 32
        Me.FraMovement.TabStop = False
        '
        'FraOthers
        '
        Me.FraOthers.BackColor = System.Drawing.SystemColors.Control
        Me.FraOthers.Controls.Add(Me.FraConditional)
        Me.FraOthers.Controls.Add(Me.cboExpHead)
        Me.FraOthers.Controls.Add(Me.CboDept)
        Me.FraOthers.Controls.Add(Me.CboCC)
        Me.FraOthers.Controls.Add(Me.cboEmp)
        Me.FraOthers.Controls.Add(Me._Lbl_6)
        Me.FraOthers.Controls.Add(Me._Lbl_3)
        Me.FraOthers.Controls.Add(Me._Lbl_2)
        Me.FraOthers.Controls.Add(Me._Lbl_4)
        Me.FraOthers.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOthers.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOthers.Location = New System.Drawing.Point(139, 8)
        Me.FraOthers.Name = "FraOthers"
        Me.FraOthers.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOthers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOthers.Size = New System.Drawing.Size(541, 88)
        Me.FraOthers.TabIndex = 35
        Me.FraOthers.TabStop = False
        Me.FraOthers.Visible = False
        '
        'FraConditional
        '
        Me.FraConditional.BackColor = System.Drawing.SystemColors.Control
        Me.FraConditional.Controls.Add(Me.chkOption)
        Me.FraConditional.Controls.Add(Me.FraAmountCond)
        Me.FraConditional.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraConditional.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraConditional.Location = New System.Drawing.Point(382, 0)
        Me.FraConditional.Name = "FraConditional"
        Me.FraConditional.Padding = New System.Windows.Forms.Padding(0)
        Me.FraConditional.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraConditional.Size = New System.Drawing.Size(159, 79)
        Me.FraConditional.TabIndex = 48
        Me.FraConditional.TabStop = False
        '
        'chkOption
        '
        Me.chkOption.BackColor = System.Drawing.SystemColors.Control
        Me.chkOption.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOption.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOption.Location = New System.Drawing.Point(6, 12)
        Me.chkOption.Name = "chkOption"
        Me.chkOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOption.Size = New System.Drawing.Size(127, 18)
        Me.chkOption.TabIndex = 52
        Me.chkOption.Text = "Conditional Check"
        Me.chkOption.UseVisualStyleBackColor = False
        '
        'FraAmountCond
        '
        Me.FraAmountCond.BackColor = System.Drawing.SystemColors.Control
        Me.FraAmountCond.Controls.Add(Me.txtCondAmount)
        Me.FraAmountCond.Controls.Add(Me.cboCond)
        Me.FraAmountCond.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAmountCond.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAmountCond.Location = New System.Drawing.Point(0, 30)
        Me.FraAmountCond.Name = "FraAmountCond"
        Me.FraAmountCond.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAmountCond.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAmountCond.Size = New System.Drawing.Size(159, 49)
        Me.FraAmountCond.TabIndex = 49
        Me.FraAmountCond.TabStop = False
        Me.FraAmountCond.Text = "Amount is"
        Me.FraAmountCond.Visible = False
        '
        'txtCondAmount
        '
        Me.txtCondAmount.AcceptsReturn = True
        Me.txtCondAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtCondAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCondAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCondAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCondAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCondAmount.Location = New System.Drawing.Point(68, 18)
        Me.txtCondAmount.MaxLength = 0
        Me.txtCondAmount.Name = "txtCondAmount"
        Me.txtCondAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCondAmount.Size = New System.Drawing.Size(87, 20)
        Me.txtCondAmount.TabIndex = 51
        '
        'cboCond
        '
        Me.cboCond.BackColor = System.Drawing.SystemColors.Window
        Me.cboCond.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCond.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCond.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCond.Location = New System.Drawing.Point(8, 18)
        Me.cboCond.Name = "cboCond"
        Me.cboCond.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCond.Size = New System.Drawing.Size(59, 22)
        Me.cboCond.TabIndex = 50
        '
        'cboExpHead
        '
        Me.cboExpHead.BackColor = System.Drawing.SystemColors.Window
        Me.cboExpHead.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboExpHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExpHead.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboExpHead.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboExpHead.Location = New System.Drawing.Point(228, 46)
        Me.cboExpHead.Name = "cboExpHead"
        Me.cboExpHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboExpHead.Size = New System.Drawing.Size(150, 22)
        Me.cboExpHead.TabIndex = 46
        '
        'CboDept
        '
        Me.CboDept.BackColor = System.Drawing.SystemColors.Window
        Me.CboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboDept.Location = New System.Drawing.Point(38, 46)
        Me.CboDept.Name = "CboDept"
        Me.CboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboDept.Size = New System.Drawing.Size(150, 22)
        Me.CboDept.TabIndex = 17
        '
        'CboCC
        '
        Me.CboCC.BackColor = System.Drawing.SystemColors.Window
        Me.CboCC.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboCC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboCC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboCC.Location = New System.Drawing.Point(38, 16)
        Me.CboCC.Name = "CboCC"
        Me.CboCC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboCC.Size = New System.Drawing.Size(150, 22)
        Me.CboCC.TabIndex = 16
        '
        'cboEmp
        '
        Me.cboEmp.BackColor = System.Drawing.SystemColors.Window
        Me.cboEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboEmp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEmp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboEmp.Location = New System.Drawing.Point(228, 16)
        Me.cboEmp.Name = "cboEmp"
        Me.cboEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboEmp.Size = New System.Drawing.Size(150, 22)
        Me.cboEmp.TabIndex = 18
        '
        '_Lbl_6
        '
        Me._Lbl_6.AutoSize = True
        Me._Lbl_6.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_6, CType(6, Short))
        Me._Lbl_6.Location = New System.Drawing.Point(194, 49)
        Me._Lbl_6.Name = "_Lbl_6"
        Me._Lbl_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_6.Size = New System.Drawing.Size(29, 14)
        Me._Lbl_6.TabIndex = 47
        Me._Lbl_6.Text = "Exp."
        '
        '_Lbl_3
        '
        Me._Lbl_3.AutoSize = True
        Me._Lbl_3.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_3, CType(3, Short))
        Me._Lbl_3.Location = New System.Drawing.Point(4, 49)
        Me._Lbl_3.Name = "_Lbl_3"
        Me._Lbl_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_3.Size = New System.Drawing.Size(32, 14)
        Me._Lbl_3.TabIndex = 38
        Me._Lbl_3.Text = "Dept"
        '
        '_Lbl_2
        '
        Me._Lbl_2.AutoSize = True
        Me._Lbl_2.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_2, CType(2, Short))
        Me._Lbl_2.Location = New System.Drawing.Point(4, 20)
        Me._Lbl_2.Name = "_Lbl_2"
        Me._Lbl_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_2.Size = New System.Drawing.Size(29, 14)
        Me._Lbl_2.TabIndex = 37
        Me._Lbl_2.Text = "C.C."
        '
        '_Lbl_4
        '
        Me._Lbl_4.AutoSize = True
        Me._Lbl_4.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_4, CType(4, Short))
        Me._Lbl_4.Location = New System.Drawing.Point(194, 20)
        Me._Lbl_4.Name = "_Lbl_4"
        Me._Lbl_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_4.Size = New System.Drawing.Size(31, 14)
        Me._Lbl_4.TabIndex = 36
        Me._Lbl_4.Text = "Emp"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._OptOrderBy_1)
        Me.Frame1.Controls.Add(Me._OptOrderBy_0)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 564)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(172, 47)
        Me.Frame1.TabIndex = 56
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Order"
        '
        '_OptOrderBy_1
        '
        Me._OptOrderBy_1.AutoSize = True
        Me._OptOrderBy_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_1, CType(1, Short))
        Me._OptOrderBy_1.Location = New System.Drawing.Point(82, 17)
        Me._OptOrderBy_1.Name = "_OptOrderBy_1"
        Me._OptOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_1.Size = New System.Drawing.Size(71, 18)
        Me._OptOrderBy_1.TabIndex = 58
        Me._OptOrderBy_1.TabStop = True
        Me._OptOrderBy_1.Text = "Exp Date"
        Me._OptOrderBy_1.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_0
        '
        Me._OptOrderBy_0.AutoSize = True
        Me._OptOrderBy_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_0.Checked = True
        Me._OptOrderBy_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_0, CType(0, Short))
        Me._OptOrderBy_0.Location = New System.Drawing.Point(6, 17)
        Me._OptOrderBy_0.Name = "_OptOrderBy_0"
        Me._OptOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_0.Size = New System.Drawing.Size(60, 18)
        Me._OptOrderBy_0.TabIndex = 57
        Me._OptOrderBy_0.TabStop = True
        Me._OptOrderBy_0.Text = "V Date"
        Me._OptOrderBy_0.UseVisualStyleBackColor = False
        '
        'lblShow
        '
        Me.lblShow.BackColor = System.Drawing.SystemColors.Control
        Me.lblShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShow.Location = New System.Drawing.Point(603, 570)
        Me.lblShow.Name = "lblShow"
        Me.lblShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShow.Size = New System.Drawing.Size(47, 13)
        Me.lblShow.TabIndex = 61
        Me.lblShow.Text = "Show"
        Me.lblShow.Visible = False
        '
        'lblPrintCount
        '
        Me.lblPrintCount.BackColor = System.Drawing.SystemColors.Control
        Me.lblPrintCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPrintCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPrintCount.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrintCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPrintCount.Location = New System.Drawing.Point(433, 568)
        Me.lblPrintCount.Name = "lblPrintCount"
        Me.lblPrintCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPrintCount.Size = New System.Drawing.Size(185, 43)
        Me.lblPrintCount.TabIndex = 41
        Me.lblPrintCount.Text = "lblPrintCount"
        Me.lblPrintCount.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblPrintCount.Visible = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(554, 590)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(67, 17)
        Me.lblBookType.TabIndex = 40
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'lblAcCode
        '
        Me.lblAcCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAcCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAcCode.Location = New System.Drawing.Point(4, 360)
        Me.lblAcCode.Name = "lblAcCode"
        Me.lblAcCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcCode.Size = New System.Drawing.Size(55, 11)
        Me.lblAcCode.TabIndex = 33
        Me.lblAcCode.Text = "lblAcCode"
        Me.lblAcCode.Visible = False
        '
        'OptSumDet
        '
        '
        'chkGroup
        '
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.lstCompanyName)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(557, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(340, 114)
        Me.GroupBox1.TabIndex = 63
        Me.GroupBox1.TabStop = False
        '
        'lstCompanyName
        '
        Me.lstCompanyName.BackColor = System.Drawing.SystemColors.Window
        Me.lstCompanyName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstCompanyName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstCompanyName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCompanyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstCompanyName.IntegralHeight = False
        Me.lstCompanyName.Location = New System.Drawing.Point(0, 13)
        Me.lstCompanyName.Name = "lstCompanyName"
        Me.lstCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstCompanyName.Size = New System.Drawing.Size(340, 101)
        Me.lstCompanyName.TabIndex = 4
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'chkAdjustDetail
        '
        Me.chkAdjustDetail.BackColor = System.Drawing.SystemColors.Control
        Me.chkAdjustDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAdjustDetail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAdjustDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAdjustDetail.Location = New System.Drawing.Point(288, 575)
        Me.chkAdjustDetail.Name = "chkAdjustDetail"
        Me.chkAdjustDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAdjustDetail.Size = New System.Drawing.Size(139, 31)
        Me.chkAdjustDetail.TabIndex = 64
        Me.chkAdjustDetail.Text = "Adjustment details"
        Me.chkAdjustDetail.UseVisualStyleBackColor = False
        '
        'fraMasterDetail
        '
        Me.fraMasterDetail.BackColor = System.Drawing.SystemColors.Control
        Me.fraMasterDetail.Controls.Add(Me.Label7)
        Me.fraMasterDetail.Controls.Add(Me.txtSaleRep)
        Me.fraMasterDetail.Controls.Add(Me.Label6)
        Me.fraMasterDetail.Controls.Add(Me.txtBankName)
        Me.fraMasterDetail.Controls.Add(Me.Label5)
        Me.fraMasterDetail.Controls.Add(Me.txtSecurityChqNo)
        Me.fraMasterDetail.Controls.Add(Me.Label4)
        Me.fraMasterDetail.Controls.Add(Me.Label3)
        Me.fraMasterDetail.Controls.Add(Me.Label2)
        Me.fraMasterDetail.Controls.Add(Me.Label1)
        Me.fraMasterDetail.Controls.Add(Me.txtSecurityAmount)
        Me.fraMasterDetail.Controls.Add(Me.txtSecurityDeposit)
        Me.fraMasterDetail.Controls.Add(Me.txtPaymentTerms)
        Me.fraMasterDetail.Controls.Add(Me.txtCreditLimit)
        Me.fraMasterDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMasterDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraMasterDetail.Location = New System.Drawing.Point(650, 117)
        Me.fraMasterDetail.Name = "fraMasterDetail"
        Me.fraMasterDetail.Padding = New System.Windows.Forms.Padding(0)
        Me.fraMasterDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraMasterDetail.Size = New System.Drawing.Size(340, 178)
        Me.fraMasterDetail.TabIndex = 66
        Me.fraMasterDetail.TabStop = False
        Me.fraMasterDetail.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(66, 154)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(60, 14)
        Me.Label7.TabIndex = 65
        Me.Label7.Text = "Sale Rep :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(52, 131)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(74, 14)
        Me.Label6.TabIndex = 63
        Me.Label6.Text = "Bank Name :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(26, 108)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(100, 14)
        Me.Label5.TabIndex = 61
        Me.Label5.Text = "Security Chq No :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(21, 85)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(105, 14)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "Security Amount :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(15, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(111, 14)
        Me.Label3.TabIndex = 58
        Me.Label3.Text = "Security Cheques :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(26, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(100, 14)
        Me.Label2.TabIndex = 57
        Me.Label2.Text = "Payment Terms :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(48, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(78, 14)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "Credit Limit :"
        '
        'frmViewLedger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(994, 611)
        Me.Controls.Add(Me.cmdMasterDetail)
        Me.Controls.Add(Me.fraMasterDetail)
        Me.Controls.Add(Me.cmdOutstanding)
        Me.Controls.Add(Me.chkAdjustDetail)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.FraOption)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.ChkWithRunBal)
        Me.Controls.Add(Me.FraOthers)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.lblShow)
        Me.Controls.Add(Me.lblPrintCount)
        Me.Controls.Add(Me.lblBookType)
        Me.Controls.Add(Me.lblAcCode)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmViewLedger"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "View Ledger"
        Me.Frame5.ResumeLayout(False)
        Me.FraOption.ResumeLayout(False)
        Me.FraOption.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        CType(Me.cboAccount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.FraOthers.ResumeLayout(False)
        Me.FraOthers.PerformLayout()
        Me.FraConditional.ResumeLayout(False)
        Me.FraAmountCond.ResumeLayout(False)
        Me.FraAmountCond.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptSumDet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.UltraDataSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraMasterDetail.ResumeLayout(False)
        Me.fraMasterDetail.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdLedg.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        'SprdLedg.DataSource = Nothing
    End Sub

    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents lstCompanyName As CheckedListBox
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraDataSource1 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents UltraGridExcelExporter1 As Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter
    Public WithEvents cmdExport As Button
    Friend WithEvents cboAccount As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraGridColumnChooser1 As Infragistics.Win.UltraWinGrid.UltraGridColumnChooser
    Public WithEvents chkAdjustDetail As CheckBox
    Public WithEvents cmdOutstanding As Button
    Public WithEvents fraMasterDetail As GroupBox
    Public WithEvents cmdMasterDetail As Button
    Public WithEvents Label4 As Label
    Public WithEvents Label3 As Label
    Public WithEvents Label2 As Label
    Public WithEvents Label1 As Label
    Public WithEvents txtSecurityAmount As TextBox
    Public WithEvents txtSecurityDeposit As TextBox
    Public WithEvents txtPaymentTerms As TextBox
    Public WithEvents txtCreditLimit As TextBox
    Public WithEvents optGroup As RadioButton
    Public WithEvents optAccount As RadioButton
    Public WithEvents Label7 As Label
    Public WithEvents txtSaleRep As TextBox
    Public WithEvents Label6 As Label
    Public WithEvents txtBankName As TextBox
    Public WithEvents Label5 As Label
    Public WithEvents txtSecurityChqNo As TextBox
#End Region
End Class