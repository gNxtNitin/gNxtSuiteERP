Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamIMTERpr
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
    Public WithEvents _OptOrderBy_4 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_3 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cboRepairDate As System.Windows.Forms.ComboBox
    Public WithEvents txtDate2 As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDate1 As System.Windows.Forms.MaskedTextBox
    Public WithEvents lblDate2 As System.Windows.Forms.Label
    Public WithEvents lblDate1 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents txtDocNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchDocNo As System.Windows.Forms.Button
    Public WithEvents chkAllDocNo As System.Windows.Forms.CheckBox
    Public WithEvents txtRepairAgency As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchRepairAgency As System.Windows.Forms.Button
    Public WithEvents chkAllRepairAgency As System.Windows.Forms.CheckBox
    Public WithEvents chkAllEName As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchEName As System.Windows.Forms.Button
    Public WithEvents txtEName As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents OptOrderBy As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamIMTERpr))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtDocNo = New System.Windows.Forms.TextBox()
        Me.cmdSearchDocNo = New System.Windows.Forms.Button()
        Me.txtRepairAgency = New System.Windows.Forms.TextBox()
        Me.cmdSearchRepairAgency = New System.Windows.Forms.Button()
        Me.cmdSearchEName = New System.Windows.Forms.Button()
        Me.txtEName = New System.Windows.Forms.TextBox()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._OptOrderBy_4 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_3 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_2 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboRepairDate = New System.Windows.Forms.ComboBox()
        Me.txtDate2 = New System.Windows.Forms.MaskedTextBox()
        Me.txtDate1 = New System.Windows.Forms.MaskedTextBox()
        Me.lblDate2 = New System.Windows.Forms.Label()
        Me.lblDate1 = New System.Windows.Forms.Label()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkAllDocNo = New System.Windows.Forms.CheckBox()
        Me.chkAllRepairAgency = New System.Windows.Forms.CheckBox()
        Me.chkAllEName = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.OptOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraAccount.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtDocNo
        '
        Me.txtDocNo.AcceptsReturn = True
        Me.txtDocNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocNo.Enabled = False
        Me.txtDocNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDocNo.Location = New System.Drawing.Point(106, 40)
        Me.txtDocNo.MaxLength = 0
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocNo.Size = New System.Drawing.Size(389, 19)
        Me.txtDocNo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtDocNo, "Press F1 For Help")
        '
        'cmdSearchDocNo
        '
        Me.cmdSearchDocNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDocNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDocNo.Enabled = False
        Me.cmdSearchDocNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDocNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDocNo.Image = CType(resources.GetObject("cmdSearchDocNo.Image"), System.Drawing.Image)
        Me.cmdSearchDocNo.Location = New System.Drawing.Point(496, 40)
        Me.cmdSearchDocNo.Name = "cmdSearchDocNo"
        Me.cmdSearchDocNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDocNo.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchDocNo.TabIndex = 4
        Me.cmdSearchDocNo.TabStop = False
        Me.cmdSearchDocNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDocNo, "Search")
        Me.cmdSearchDocNo.UseVisualStyleBackColor = False
        '
        'txtRepairAgency
        '
        Me.txtRepairAgency.AcceptsReturn = True
        Me.txtRepairAgency.BackColor = System.Drawing.SystemColors.Window
        Me.txtRepairAgency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRepairAgency.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRepairAgency.Enabled = False
        Me.txtRepairAgency.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRepairAgency.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRepairAgency.Location = New System.Drawing.Point(106, 62)
        Me.txtRepairAgency.MaxLength = 0
        Me.txtRepairAgency.Name = "txtRepairAgency"
        Me.txtRepairAgency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRepairAgency.Size = New System.Drawing.Size(389, 19)
        Me.txtRepairAgency.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtRepairAgency, "Press F1 For Help")
        '
        'cmdSearchRepairAgency
        '
        Me.cmdSearchRepairAgency.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchRepairAgency.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchRepairAgency.Enabled = False
        Me.cmdSearchRepairAgency.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchRepairAgency.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchRepairAgency.Image = CType(resources.GetObject("cmdSearchRepairAgency.Image"), System.Drawing.Image)
        Me.cmdSearchRepairAgency.Location = New System.Drawing.Point(496, 62)
        Me.cmdSearchRepairAgency.Name = "cmdSearchRepairAgency"
        Me.cmdSearchRepairAgency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchRepairAgency.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchRepairAgency.TabIndex = 7
        Me.cmdSearchRepairAgency.TabStop = False
        Me.cmdSearchRepairAgency.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchRepairAgency, "Search")
        Me.cmdSearchRepairAgency.UseVisualStyleBackColor = False
        '
        'cmdSearchEName
        '
        Me.cmdSearchEName.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchEName.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchEName.Enabled = False
        Me.cmdSearchEName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchEName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchEName.Image = CType(resources.GetObject("cmdSearchEName.Image"), System.Drawing.Image)
        Me.cmdSearchEName.Location = New System.Drawing.Point(496, 18)
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
        Me.txtEName.Location = New System.Drawing.Point(106, 18)
        Me.txtEName.MaxLength = 0
        Me.txtEName.Name = "txtEName"
        Me.txtEName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEName.Size = New System.Drawing.Size(389, 19)
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
        Me.CmdSave.TabIndex = 13
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
        Me.cmdClose.TabIndex = 14
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
        Me.CmdPreview.TabIndex = 12
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
        Me.cmdPrint.TabIndex = 11
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
        Me.cmdShow.TabIndex = 10
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._OptOrderBy_4)
        Me.Frame2.Controls.Add(Me._OptOrderBy_3)
        Me.Frame2.Controls.Add(Me._OptOrderBy_2)
        Me.Frame2.Controls.Add(Me._OptOrderBy_1)
        Me.Frame2.Controls.Add(Me._OptOrderBy_0)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 88)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(585, 35)
        Me.Frame2.TabIndex = 20
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Order By"
        '
        '_OptOrderBy_4
        '
        Me._OptOrderBy_4.AutoSize = True
        Me._OptOrderBy_4.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_4, CType(4, Short))
        Me._OptOrderBy_4.Location = New System.Drawing.Point(472, 14)
        Me._OptOrderBy_4.Name = "_OptOrderBy_4"
        Me._OptOrderBy_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_4.Size = New System.Drawing.Size(104, 18)
        Me._OptOrderBy_4.TabIndex = 31
        Me._OptOrderBy_4.TabStop = True
        Me._OptOrderBy_4.Text = "Repair Agency"
        Me._OptOrderBy_4.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_3
        '
        Me._OptOrderBy_3.AutoSize = True
        Me._OptOrderBy_3.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_3, CType(3, Short))
        Me._OptOrderBy_3.Location = New System.Drawing.Point(372, 14)
        Me._OptOrderBy_3.Name = "_OptOrderBy_3"
        Me._OptOrderBy_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_3.Size = New System.Drawing.Size(68, 18)
        Me._OptOrderBy_3.TabIndex = 30
        Me._OptOrderBy_3.TabStop = True
        Me._OptOrderBy_3.Text = "E. Name"
        Me._OptOrderBy_3.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_2
        '
        Me._OptOrderBy_2.AutoSize = True
        Me._OptOrderBy_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_2, CType(2, Short))
        Me._OptOrderBy_2.Location = New System.Drawing.Point(172, 14)
        Me._OptOrderBy_2.Name = "_OptOrderBy_2"
        Me._OptOrderBy_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_2.Size = New System.Drawing.Size(79, 18)
        Me._OptOrderBy_2.TabIndex = 29
        Me._OptOrderBy_2.TabStop = True
        Me._OptOrderBy_2.Text = "Recd Date"
        Me._OptOrderBy_2.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_1
        '
        Me._OptOrderBy_1.AutoSize = True
        Me._OptOrderBy_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_1, CType(1, Short))
        Me._OptOrderBy_1.Location = New System.Drawing.Point(280, 14)
        Me._OptOrderBy_1.Name = "_OptOrderBy_1"
        Me._OptOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_1.Size = New System.Drawing.Size(62, 18)
        Me._OptOrderBy_1.TabIndex = 28
        Me._OptOrderBy_1.TabStop = True
        Me._OptOrderBy_1.Text = "Doc No"
        Me._OptOrderBy_1.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_0
        '
        Me._OptOrderBy_0.AutoSize = True
        Me._OptOrderBy_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_0.Checked = True
        Me._OptOrderBy_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_0, CType(0, Short))
        Me._OptOrderBy_0.Location = New System.Drawing.Point(64, 14)
        Me._OptOrderBy_0.Name = "_OptOrderBy_0"
        Me._OptOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_0.Size = New System.Drawing.Size(80, 18)
        Me._OptOrderBy_0.TabIndex = 27
        Me._OptOrderBy_0.TabStop = True
        Me._OptOrderBy_0.Text = "Send Date"
        Me._OptOrderBy_0.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboRepairDate)
        Me.Frame1.Controls.Add(Me.txtDate2)
        Me.Frame1.Controls.Add(Me.txtDate1)
        Me.Frame1.Controls.Add(Me.lblDate2)
        Me.Frame1.Controls.Add(Me.lblDate1)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(592, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(169, 123)
        Me.Frame1.TabIndex = 21
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Repaired On Condition"
        '
        'cboRepairDate
        '
        Me.cboRepairDate.BackColor = System.Drawing.SystemColors.Window
        Me.cboRepairDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboRepairDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRepairDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRepairDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboRepairDate.Location = New System.Drawing.Point(16, 22)
        Me.cboRepairDate.Name = "cboRepairDate"
        Me.cboRepairDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboRepairDate.Size = New System.Drawing.Size(143, 22)
        Me.cboRepairDate.TabIndex = 22
        '
        'txtDate2
        '
        Me.txtDate2.AllowPromptAsInput = False
        Me.txtDate2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate2.Location = New System.Drawing.Point(72, 92)
        Me.txtDate2.Mask = "##/##/####"
        Me.txtDate2.Name = "txtDate2"
        Me.txtDate2.Size = New System.Drawing.Size(84, 20)
        Me.txtDate2.TabIndex = 23
        '
        'txtDate1
        '
        Me.txtDate1.AllowPromptAsInput = False
        Me.txtDate1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate1.Location = New System.Drawing.Point(72, 60)
        Me.txtDate1.Mask = "##/##/####"
        Me.txtDate1.Name = "txtDate1"
        Me.txtDate1.Size = New System.Drawing.Size(84, 20)
        Me.txtDate1.TabIndex = 24
        '
        'lblDate2
        '
        Me.lblDate2.AutoSize = True
        Me.lblDate2.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate2.Location = New System.Drawing.Point(6, 96)
        Me.lblDate2.Name = "lblDate2"
        Me.lblDate2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate2.Size = New System.Drawing.Size(49, 14)
        Me.lblDate2.TabIndex = 26
        Me.lblDate2.Text = "Date 2 : "
        Me.lblDate2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDate1
        '
        Me.lblDate1.AutoSize = True
        Me.lblDate1.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate1.Location = New System.Drawing.Point(6, 64)
        Me.lblDate1.Name = "lblDate1"
        Me.lblDate1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate1.Size = New System.Drawing.Size(49, 14)
        Me.lblDate1.TabIndex = 25
        Me.lblDate1.Text = "Date 1 : "
        Me.lblDate1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 124)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(762, 285)
        Me.SprdMain.TabIndex = 9
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.txtDocNo)
        Me.FraAccount.Controls.Add(Me.cmdSearchDocNo)
        Me.FraAccount.Controls.Add(Me.chkAllDocNo)
        Me.FraAccount.Controls.Add(Me.txtRepairAgency)
        Me.FraAccount.Controls.Add(Me.cmdSearchRepairAgency)
        Me.FraAccount.Controls.Add(Me.chkAllRepairAgency)
        Me.FraAccount.Controls.Add(Me.chkAllEName)
        Me.FraAccount.Controls.Add(Me.cmdSearchEName)
        Me.FraAccount.Controls.Add(Me.txtEName)
        Me.FraAccount.Controls.Add(Me.Label6)
        Me.FraAccount.Controls.Add(Me.Label1)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(0, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(585, 87)
        Me.FraAccount.TabIndex = 15
        Me.FraAccount.TabStop = False
        '
        'chkAllDocNo
        '
        Me.chkAllDocNo.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllDocNo.Checked = True
        Me.chkAllDocNo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllDocNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllDocNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllDocNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllDocNo.Location = New System.Drawing.Point(526, 44)
        Me.chkAllDocNo.Name = "chkAllDocNo"
        Me.chkAllDocNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllDocNo.Size = New System.Drawing.Size(49, 13)
        Me.chkAllDocNo.TabIndex = 5
        Me.chkAllDocNo.Text = "ALL"
        Me.chkAllDocNo.UseVisualStyleBackColor = False
        '
        'chkAllRepairAgency
        '
        Me.chkAllRepairAgency.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllRepairAgency.Checked = True
        Me.chkAllRepairAgency.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllRepairAgency.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllRepairAgency.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllRepairAgency.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllRepairAgency.Location = New System.Drawing.Point(526, 66)
        Me.chkAllRepairAgency.Name = "chkAllRepairAgency"
        Me.chkAllRepairAgency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllRepairAgency.Size = New System.Drawing.Size(49, 13)
        Me.chkAllRepairAgency.TabIndex = 8
        Me.chkAllRepairAgency.Text = "ALL"
        Me.chkAllRepairAgency.UseVisualStyleBackColor = False
        '
        'chkAllEName
        '
        Me.chkAllEName.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllEName.Checked = True
        Me.chkAllEName.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllEName.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllEName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllEName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllEName.Location = New System.Drawing.Point(526, 22)
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
        Me.Label6.Location = New System.Drawing.Point(50, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(53, 14)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Doc No : "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(10, 66)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(95, 14)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Repair Agency : "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(46, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(59, 14)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "E_Name : "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.FraMovement.TabIndex = 16
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 174)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 23
        '
        'frmParamIMTERpr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(763, 459)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.SprdMain)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Report1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamIMTERpr"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "IMTE Repair History"
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
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