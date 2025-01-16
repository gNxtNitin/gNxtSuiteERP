Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamIMTEMst
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
    Public WithEvents cboLCDate As System.Windows.Forms.ComboBox
    Public WithEvents txtDate4 As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDate3 As System.Windows.Forms.MaskedTextBox
    Public WithEvents lblDate4 As System.Windows.Forms.Label
    Public WithEvents lblDate3 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cboMaster As System.Windows.Forms.ComboBox
    Public WithEvents cboType As System.Windows.Forms.ComboBox
    Public WithEvents cboCaliFacil As System.Windows.Forms.ComboBox
    Public WithEvents cboStatus As System.Windows.Forms.ComboBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents _OptOrderBy_3 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_4 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cboCDDate As System.Windows.Forms.ComboBox
    Public WithEvents txtDate2 As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDate1 As System.Windows.Forms.MaskedTextBox
    Public WithEvents lblDate1 As System.Windows.Forms.Label
    Public WithEvents lblDate2 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtENo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchENo As System.Windows.Forms.Button
    Public WithEvents chkAllENo As System.Windows.Forms.CheckBox
    Public WithEvents txtLocation As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchLocation As System.Windows.Forms.Button
    Public WithEvents chkAllLocation As System.Windows.Forms.CheckBox
    Public WithEvents txtIssueTo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchIssueTo As System.Windows.Forms.Button
    Public WithEvents chkAllIssueTo As System.Windows.Forms.CheckBox
    Public WithEvents chkAllEName As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchEName As System.Windows.Forms.Button
    Public WithEvents txtEName As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents OptOrderBy As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamIMTEMst))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtENo = New System.Windows.Forms.TextBox()
        Me.cmdSearchENo = New System.Windows.Forms.Button()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.cmdSearchLocation = New System.Windows.Forms.Button()
        Me.txtIssueTo = New System.Windows.Forms.TextBox()
        Me.cmdSearchIssueTo = New System.Windows.Forms.Button()
        Me.cmdSearchEName = New System.Windows.Forms.Button()
        Me.txtEName = New System.Windows.Forms.TextBox()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.cboLCDate = New System.Windows.Forms.ComboBox()
        Me.txtDate4 = New System.Windows.Forms.MaskedTextBox()
        Me.txtDate3 = New System.Windows.Forms.MaskedTextBox()
        Me.lblDate4 = New System.Windows.Forms.Label()
        Me.lblDate3 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.cboMaster = New System.Windows.Forms.ComboBox()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.cboCaliFacil = New System.Windows.Forms.ComboBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._OptOrderBy_3 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_2 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_4 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboCDDate = New System.Windows.Forms.ComboBox()
        Me.txtDate2 = New System.Windows.Forms.MaskedTextBox()
        Me.txtDate1 = New System.Windows.Forms.MaskedTextBox()
        Me.lblDate1 = New System.Windows.Forms.Label()
        Me.lblDate2 = New System.Windows.Forms.Label()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkAllENo = New System.Windows.Forms.CheckBox()
        Me.chkAllLocation = New System.Windows.Forms.CheckBox()
        Me.chkAllIssueTo = New System.Windows.Forms.CheckBox()
        Me.chkAllEName = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.OptOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame4.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame5.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtENo
        '
        Me.txtENo.AcceptsReturn = True
        Me.txtENo.BackColor = System.Drawing.SystemColors.Window
        Me.txtENo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtENo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtENo.Enabled = False
        Me.txtENo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtENo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtENo.Location = New System.Drawing.Point(74, 32)
        Me.txtENo.MaxLength = 0
        Me.txtENo.Name = "txtENo"
        Me.txtENo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtENo.Size = New System.Drawing.Size(237, 19)
        Me.txtENo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtENo, "Press F1 For Help")
        '
        'cmdSearchENo
        '
        Me.cmdSearchENo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchENo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchENo.Enabled = False
        Me.cmdSearchENo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchENo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchENo.Image = CType(resources.GetObject("cmdSearchENo.Image"), System.Drawing.Image)
        Me.cmdSearchENo.Location = New System.Drawing.Point(312, 32)
        Me.cmdSearchENo.Name = "cmdSearchENo"
        Me.cmdSearchENo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchENo.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchENo.TabIndex = 4
        Me.cmdSearchENo.TabStop = False
        Me.cmdSearchENo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchENo, "Search")
        Me.cmdSearchENo.UseVisualStyleBackColor = False
        '
        'txtLocation
        '
        Me.txtLocation.AcceptsReturn = True
        Me.txtLocation.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocation.Enabled = False
        Me.txtLocation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLocation.Location = New System.Drawing.Point(74, 76)
        Me.txtLocation.MaxLength = 0
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocation.Size = New System.Drawing.Size(237, 19)
        Me.txtLocation.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtLocation, "Press F1 For Help")
        '
        'cmdSearchLocation
        '
        Me.cmdSearchLocation.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchLocation.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchLocation.Enabled = False
        Me.cmdSearchLocation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchLocation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchLocation.Image = CType(resources.GetObject("cmdSearchLocation.Image"), System.Drawing.Image)
        Me.cmdSearchLocation.Location = New System.Drawing.Point(312, 76)
        Me.cmdSearchLocation.Name = "cmdSearchLocation"
        Me.cmdSearchLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchLocation.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchLocation.TabIndex = 10
        Me.cmdSearchLocation.TabStop = False
        Me.cmdSearchLocation.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchLocation, "Search")
        Me.cmdSearchLocation.UseVisualStyleBackColor = False
        '
        'txtIssueTo
        '
        Me.txtIssueTo.AcceptsReturn = True
        Me.txtIssueTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtIssueTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIssueTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIssueTo.Enabled = False
        Me.txtIssueTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssueTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIssueTo.Location = New System.Drawing.Point(74, 54)
        Me.txtIssueTo.MaxLength = 0
        Me.txtIssueTo.Name = "txtIssueTo"
        Me.txtIssueTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIssueTo.Size = New System.Drawing.Size(237, 19)
        Me.txtIssueTo.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtIssueTo, "Press F1 For Help")
        '
        'cmdSearchIssueTo
        '
        Me.cmdSearchIssueTo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchIssueTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchIssueTo.Enabled = False
        Me.cmdSearchIssueTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchIssueTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchIssueTo.Image = CType(resources.GetObject("cmdSearchIssueTo.Image"), System.Drawing.Image)
        Me.cmdSearchIssueTo.Location = New System.Drawing.Point(312, 54)
        Me.cmdSearchIssueTo.Name = "cmdSearchIssueTo"
        Me.cmdSearchIssueTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchIssueTo.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchIssueTo.TabIndex = 7
        Me.cmdSearchIssueTo.TabStop = False
        Me.cmdSearchIssueTo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchIssueTo, "Search")
        Me.cmdSearchIssueTo.UseVisualStyleBackColor = False
        '
        'cmdSearchEName
        '
        Me.cmdSearchEName.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchEName.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchEName.Enabled = False
        Me.cmdSearchEName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchEName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchEName.Image = CType(resources.GetObject("cmdSearchEName.Image"), System.Drawing.Image)
        Me.cmdSearchEName.Location = New System.Drawing.Point(312, 10)
        Me.cmdSearchEName.Name = "cmdSearchEName"
        Me.cmdSearchEName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchEName.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchEName.TabIndex = 1
        Me.cmdSearchEName.TabStop = False
        Me.cmdSearchEName.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchEName, "Search")
        Me.cmdSearchEName.UseVisualStyleBackColor = False
        '
        'txtEName
        '
        Me.txtEName.AcceptsReturn = True
        Me.txtEName.BackColor = System.Drawing.SystemColors.Window
        Me.txtEName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEName.Enabled = False
        Me.txtEName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEName.Location = New System.Drawing.Point(74, 10)
        Me.txtEName.MaxLength = 0
        Me.txtEName.Name = "txtEName"
        Me.txtEName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEName.Size = New System.Drawing.Size(237, 19)
        Me.txtEName.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtEName, "Press F1 For Help")
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(242, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 24
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        Me.CmdSave.Visible = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(310, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 37)
        Me.cmdClose.TabIndex = 25
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
        Me.CmdPreview.Location = New System.Drawing.Point(123, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.CmdPreview.TabIndex = 23
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
        Me.cmdPrint.Location = New System.Drawing.Point(63, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 22
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
        Me.cmdShow.Location = New System.Drawing.Point(4, 10)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(60, 37)
        Me.cmdShow.TabIndex = 21
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.cboLCDate)
        Me.Frame4.Controls.Add(Me.txtDate4)
        Me.Frame4.Controls.Add(Me.txtDate3)
        Me.Frame4.Controls.Add(Me.lblDate4)
        Me.Frame4.Controls.Add(Me.lblDate3)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(394, 40)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(369, 39)
        Me.Frame4.TabIndex = 43
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "LC Date Condition"
        '
        'cboLCDate
        '
        Me.cboLCDate.BackColor = System.Drawing.SystemColors.Window
        Me.cboLCDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboLCDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLCDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLCDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboLCDate.Location = New System.Drawing.Point(4, 14)
        Me.cboLCDate.Name = "cboLCDate"
        Me.cboLCDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboLCDate.Size = New System.Drawing.Size(99, 22)
        Me.cboLCDate.TabIndex = 44
        '
        'txtDate4
        '
        Me.txtDate4.AllowPromptAsInput = False
        Me.txtDate4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate4.Location = New System.Drawing.Point(290, 12)
        Me.txtDate4.Mask = "##/##/####"
        Me.txtDate4.Name = "txtDate4"
        Me.txtDate4.Size = New System.Drawing.Size(76, 20)
        Me.txtDate4.TabIndex = 45
        '
        'txtDate3
        '
        Me.txtDate3.AllowPromptAsInput = False
        Me.txtDate3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate3.Location = New System.Drawing.Point(156, 12)
        Me.txtDate3.Mask = "##/##/####"
        Me.txtDate3.Name = "txtDate3"
        Me.txtDate3.Size = New System.Drawing.Size(76, 20)
        Me.txtDate3.TabIndex = 46
        '
        'lblDate4
        '
        Me.lblDate4.AutoSize = True
        Me.lblDate4.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate4.Location = New System.Drawing.Point(230, 16)
        Me.lblDate4.Name = "lblDate4"
        Me.lblDate4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate4.Size = New System.Drawing.Size(49, 14)
        Me.lblDate4.TabIndex = 48
        Me.lblDate4.Text = "Date 2 : "
        Me.lblDate4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDate3
        '
        Me.lblDate3.AutoSize = True
        Me.lblDate3.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate3.Location = New System.Drawing.Point(106, 16)
        Me.lblDate3.Name = "lblDate3"
        Me.lblDate3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate3.Size = New System.Drawing.Size(49, 14)
        Me.lblDate3.TabIndex = 47
        Me.lblDate3.Text = "Date 1 : "
        Me.lblDate3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cboMaster)
        Me.Frame3.Controls.Add(Me.cboType)
        Me.Frame3.Controls.Add(Me.cboCaliFacil)
        Me.Frame3.Controls.Add(Me.cboStatus)
        Me.Frame3.Controls.Add(Me.Label8)
        Me.Frame3.Controls.Add(Me.Label7)
        Me.Frame3.Controls.Add(Me.Label5)
        Me.Frame3.Controls.Add(Me.Label3)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(394, 80)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(369, 59)
        Me.Frame3.TabIndex = 35
        Me.Frame3.TabStop = False
        '
        'cboMaster
        '
        Me.cboMaster.BackColor = System.Drawing.SystemColors.Window
        Me.cboMaster.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMaster.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaster.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMaster.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMaster.Location = New System.Drawing.Point(246, 34)
        Me.cboMaster.Name = "cboMaster"
        Me.cboMaster.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMaster.Size = New System.Drawing.Size(113, 22)
        Me.cboMaster.TabIndex = 41
        '
        'cboType
        '
        Me.cboType.BackColor = System.Drawing.SystemColors.Window
        Me.cboType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboType.Location = New System.Drawing.Point(246, 11)
        Me.cboType.Name = "cboType"
        Me.cboType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboType.Size = New System.Drawing.Size(113, 22)
        Me.cboType.TabIndex = 39
        '
        'cboCaliFacil
        '
        Me.cboCaliFacil.BackColor = System.Drawing.SystemColors.Window
        Me.cboCaliFacil.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCaliFacil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCaliFacil.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCaliFacil.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCaliFacil.Location = New System.Drawing.Point(74, 34)
        Me.cboCaliFacil.Name = "cboCaliFacil"
        Me.cboCaliFacil.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCaliFacil.Size = New System.Drawing.Size(113, 22)
        Me.cboCaliFacil.TabIndex = 16
        '
        'cboStatus
        '
        Me.cboStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboStatus.Location = New System.Drawing.Point(74, 11)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboStatus.Size = New System.Drawing.Size(113, 22)
        Me.cboStatus.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(190, 38)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(55, 14)
        Me.Label8.TabIndex = 42
        Me.Label8.Text = "Master : "
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(200, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(42, 14)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "Type : "
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(20, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(51, 14)
        Me.Label5.TabIndex = 37
        Me.Label5.Text = "Status : "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(4, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(63, 14)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "CaliFacil. : "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._OptOrderBy_3)
        Me.Frame2.Controls.Add(Me._OptOrderBy_1)
        Me.Frame2.Controls.Add(Me._OptOrderBy_2)
        Me.Frame2.Controls.Add(Me._OptOrderBy_4)
        Me.Frame2.Controls.Add(Me._OptOrderBy_0)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 100)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(393, 41)
        Me.Frame2.TabIndex = 34
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Order By"
        '
        '_OptOrderBy_3
        '
        Me._OptOrderBy_3.AutoSize = True
        Me._OptOrderBy_3.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_3, CType(3, Short))
        Me._OptOrderBy_3.Location = New System.Drawing.Point(236, 20)
        Me._OptOrderBy_3.Name = "_OptOrderBy_3"
        Me._OptOrderBy_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_3.Size = New System.Drawing.Size(72, 18)
        Me._OptOrderBy_3.TabIndex = 20
        Me._OptOrderBy_3.TabStop = True
        Me._OptOrderBy_3.Text = "Location"
        Me._OptOrderBy_3.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_1
        '
        Me._OptOrderBy_1.AutoSize = True
        Me._OptOrderBy_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_1, CType(1, Short))
        Me._OptOrderBy_1.Location = New System.Drawing.Point(160, 20)
        Me._OptOrderBy_1.Name = "_OptOrderBy_1"
        Me._OptOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_1.Size = New System.Drawing.Size(72, 18)
        Me._OptOrderBy_1.TabIndex = 19
        Me._OptOrderBy_1.TabStop = True
        Me._OptOrderBy_1.Text = "Issue To"
        Me._OptOrderBy_1.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_2
        '
        Me._OptOrderBy_2.AutoSize = True
        Me._OptOrderBy_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_2, CType(2, Short))
        Me._OptOrderBy_2.Location = New System.Drawing.Point(84, 20)
        Me._OptOrderBy_2.Name = "_OptOrderBy_2"
        Me._OptOrderBy_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_2.Size = New System.Drawing.Size(68, 18)
        Me._OptOrderBy_2.TabIndex = 18
        Me._OptOrderBy_2.TabStop = True
        Me._OptOrderBy_2.Text = "E. Name"
        Me._OptOrderBy_2.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_4
        '
        Me._OptOrderBy_4.AutoSize = True
        Me._OptOrderBy_4.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_4.Checked = True
        Me._OptOrderBy_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_4, CType(4, Short))
        Me._OptOrderBy_4.Location = New System.Drawing.Point(312, 20)
        Me._OptOrderBy_4.Name = "_OptOrderBy_4"
        Me._OptOrderBy_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_4.Size = New System.Drawing.Size(67, 18)
        Me._OptOrderBy_4.TabIndex = 49
        Me._OptOrderBy_4.TabStop = True
        Me._OptOrderBy_4.Text = "LC Date"
        Me._OptOrderBy_4.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_0
        '
        Me._OptOrderBy_0.AutoSize = True
        Me._OptOrderBy_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_0, CType(0, Short))
        Me._OptOrderBy_0.Location = New System.Drawing.Point(8, 20)
        Me._OptOrderBy_0.Name = "_OptOrderBy_0"
        Me._OptOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_0.Size = New System.Drawing.Size(67, 18)
        Me._OptOrderBy_0.TabIndex = 17
        Me._OptOrderBy_0.TabStop = True
        Me._OptOrderBy_0.Text = "CD Date"
        Me._OptOrderBy_0.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboCDDate)
        Me.Frame1.Controls.Add(Me.txtDate2)
        Me.Frame1.Controls.Add(Me.txtDate1)
        Me.Frame1.Controls.Add(Me.lblDate1)
        Me.Frame1.Controls.Add(Me.lblDate2)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(394, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(369, 39)
        Me.Frame1.TabIndex = 31
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "CD Date Condition"
        '
        'cboCDDate
        '
        Me.cboCDDate.BackColor = System.Drawing.SystemColors.Window
        Me.cboCDDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCDDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCDDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCDDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCDDate.Location = New System.Drawing.Point(4, 14)
        Me.cboCDDate.Name = "cboCDDate"
        Me.cboCDDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCDDate.Size = New System.Drawing.Size(99, 22)
        Me.cboCDDate.TabIndex = 12
        '
        'txtDate2
        '
        Me.txtDate2.AllowPromptAsInput = False
        Me.txtDate2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate2.Location = New System.Drawing.Point(290, 12)
        Me.txtDate2.Mask = "##/##/####"
        Me.txtDate2.Name = "txtDate2"
        Me.txtDate2.Size = New System.Drawing.Size(76, 20)
        Me.txtDate2.TabIndex = 14
        '
        'txtDate1
        '
        Me.txtDate1.AllowPromptAsInput = False
        Me.txtDate1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate1.Location = New System.Drawing.Point(156, 12)
        Me.txtDate1.Mask = "##/##/####"
        Me.txtDate1.Name = "txtDate1"
        Me.txtDate1.Size = New System.Drawing.Size(76, 20)
        Me.txtDate1.TabIndex = 13
        '
        'lblDate1
        '
        Me.lblDate1.AutoSize = True
        Me.lblDate1.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate1.Location = New System.Drawing.Point(106, 16)
        Me.lblDate1.Name = "lblDate1"
        Me.lblDate1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate1.Size = New System.Drawing.Size(49, 14)
        Me.lblDate1.TabIndex = 33
        Me.lblDate1.Text = "Date 1 : "
        Me.lblDate1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDate2
        '
        Me.lblDate2.AutoSize = True
        Me.lblDate2.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate2.Location = New System.Drawing.Point(230, 16)
        Me.lblDate2.Name = "lblDate2"
        Me.lblDate2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate2.Size = New System.Drawing.Size(49, 14)
        Me.lblDate2.TabIndex = 32
        Me.lblDate2.Text = "Date 2 : "
        Me.lblDate2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.txtENo)
        Me.FraAccount.Controls.Add(Me.cmdSearchENo)
        Me.FraAccount.Controls.Add(Me.chkAllENo)
        Me.FraAccount.Controls.Add(Me.txtLocation)
        Me.FraAccount.Controls.Add(Me.cmdSearchLocation)
        Me.FraAccount.Controls.Add(Me.chkAllLocation)
        Me.FraAccount.Controls.Add(Me.txtIssueTo)
        Me.FraAccount.Controls.Add(Me.cmdSearchIssueTo)
        Me.FraAccount.Controls.Add(Me.chkAllIssueTo)
        Me.FraAccount.Controls.Add(Me.chkAllEName)
        Me.FraAccount.Controls.Add(Me.cmdSearchEName)
        Me.FraAccount.Controls.Add(Me.txtEName)
        Me.FraAccount.Controls.Add(Me.Label6)
        Me.FraAccount.Controls.Add(Me.Label4)
        Me.FraAccount.Controls.Add(Me.Label1)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(0, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(393, 99)
        Me.FraAccount.TabIndex = 26
        Me.FraAccount.TabStop = False
        '
        'chkAllENo
        '
        Me.chkAllENo.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllENo.Checked = True
        Me.chkAllENo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllENo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllENo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllENo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllENo.Location = New System.Drawing.Point(342, 36)
        Me.chkAllENo.Name = "chkAllENo"
        Me.chkAllENo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllENo.Size = New System.Drawing.Size(49, 13)
        Me.chkAllENo.TabIndex = 5
        Me.chkAllENo.Text = "ALL"
        Me.chkAllENo.UseVisualStyleBackColor = False
        '
        'chkAllLocation
        '
        Me.chkAllLocation.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllLocation.Checked = True
        Me.chkAllLocation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllLocation.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllLocation.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllLocation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllLocation.Location = New System.Drawing.Point(342, 80)
        Me.chkAllLocation.Name = "chkAllLocation"
        Me.chkAllLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllLocation.Size = New System.Drawing.Size(49, 13)
        Me.chkAllLocation.TabIndex = 11
        Me.chkAllLocation.Text = "ALL"
        Me.chkAllLocation.UseVisualStyleBackColor = False
        '
        'chkAllIssueTo
        '
        Me.chkAllIssueTo.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllIssueTo.Checked = True
        Me.chkAllIssueTo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllIssueTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllIssueTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllIssueTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllIssueTo.Location = New System.Drawing.Point(342, 58)
        Me.chkAllIssueTo.Name = "chkAllIssueTo"
        Me.chkAllIssueTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllIssueTo.Size = New System.Drawing.Size(49, 13)
        Me.chkAllIssueTo.TabIndex = 8
        Me.chkAllIssueTo.Text = "ALL"
        Me.chkAllIssueTo.UseVisualStyleBackColor = False
        '
        'chkAllEName
        '
        Me.chkAllEName.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllEName.Checked = True
        Me.chkAllEName.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllEName.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllEName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllEName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllEName.Location = New System.Drawing.Point(342, 14)
        Me.chkAllEName.Name = "chkAllEName"
        Me.chkAllEName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllEName.Size = New System.Drawing.Size(49, 13)
        Me.chkAllEName.TabIndex = 2
        Me.chkAllEName.Text = "ALL"
        Me.chkAllEName.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(30, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(42, 14)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "E_No : "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(3, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(63, 14)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Location : "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(12, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(63, 14)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Issue To : "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(14, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(59, 14)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "E_Name : "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 174)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 45
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.SprdMain)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(0, 134)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(761, 283)
        Me.Frame5.TabIndex = 50
        Me.Frame5.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(756, 273)
        Me.SprdMain.TabIndex = 51
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(390, 410)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(373, 49)
        Me.FraMovement.TabIndex = 27
        Me.FraMovement.TabStop = False
        '
        'frmParamIMTEMst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(763, 459)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamIMTEMst"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "IMTE Master List"
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame5.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdMain.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdMain.DataSource = Nothing
    End Sub
#End Region
End Class