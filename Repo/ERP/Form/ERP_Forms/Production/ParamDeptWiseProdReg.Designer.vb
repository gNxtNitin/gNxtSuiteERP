Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamDeptWiseProdReg
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
    Public WithEvents chkShowFinal As System.Windows.Forms.CheckBox
    Public WithEvents chkSPD As System.Windows.Forms.CheckBox
    Public WithEvents _OptDetSumm_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptDetSumm_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptDetSumm_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents chkTime As System.Windows.Forms.CheckBox
    Public WithEvents txtTMFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtTMTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_3 As System.Windows.Forms.Label
    Public WithEvents _Lbl_2 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _OptOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtOperatorName As System.Windows.Forms.TextBox
    Public WithEvents chkOperatorAll As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchOperator As System.Windows.Forms.Button
    Public WithEvents txtOperator As System.Windows.Forms.TextBox
    Public WithEvents txtOpr As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchOpr As System.Windows.Forms.Button
    Public WithEvents chkOprAll As System.Windows.Forms.CheckBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtItemName As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents _optDate_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optDate_0 As System.Windows.Forms.RadioButton
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents cboApproved As System.Windows.Forms.ComboBox
    Public WithEvents cboShift As System.Windows.Forms.ComboBox
    Public WithEvents cboDept As System.Windows.Forms.ComboBox
    Public WithEvents cboType As System.Windows.Forms.ComboBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents lblAcCode As System.Windows.Forms.Label
    Public WithEvents lblTrnType As System.Windows.Forms.Label
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents OptDetSumm As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents OptOrderBy As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optDate As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamDeptWiseProdReg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtOperatorName = New System.Windows.Forms.TextBox()
        Me.cmdSearchOperator = New System.Windows.Forms.Button()
        Me.txtOperator = New System.Windows.Forms.TextBox()
        Me.txtOpr = New System.Windows.Forms.TextBox()
        Me.cmdSearchOpr = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.TxtItemName = New System.Windows.Forms.TextBox()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.chkShowFinal = New System.Windows.Forms.CheckBox()
        Me.chkSPD = New System.Windows.Forms.CheckBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._OptDetSumm_2 = New System.Windows.Forms.RadioButton()
        Me._OptDetSumm_1 = New System.Windows.Forms.RadioButton()
        Me._OptDetSumm_0 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.chkTime = New System.Windows.Forms.CheckBox()
        Me.txtTMFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtTMTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_3 = New System.Windows.Forms.Label()
        Me._Lbl_2 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._OptOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkOperatorAll = New System.Windows.Forms.CheckBox()
        Me.chkOprAll = New System.Windows.Forms.CheckBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me._optDate_1 = New System.Windows.Forms.RadioButton()
        Me._optDate_0 = New System.Windows.Forms.RadioButton()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.cboApproved = New System.Windows.Forms.ComboBox()
        Me.cboShift = New System.Windows.Forms.ComboBox()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblAcCode = New System.Windows.Forms.Label()
        Me.lblTrnType = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptDetSumm = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optDate = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstCompanyName = New System.Windows.Forms.CheckedListBox()
        Me.cboMachineNo = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Frame3.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptDetSumm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtOperatorName
        '
        Me.txtOperatorName.AcceptsReturn = True
        Me.txtOperatorName.BackColor = System.Drawing.SystemColors.Window
        Me.txtOperatorName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOperatorName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOperatorName.Enabled = False
        Me.txtOperatorName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOperatorName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOperatorName.Location = New System.Drawing.Point(150, 54)
        Me.txtOperatorName.MaxLength = 0
        Me.txtOperatorName.Name = "txtOperatorName"
        Me.txtOperatorName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOperatorName.Size = New System.Drawing.Size(235, 20)
        Me.txtOperatorName.TabIndex = 52
        Me.ToolTip1.SetToolTip(Me.txtOperatorName, "Press F1 For Help")
        '
        'cmdSearchOperator
        '
        Me.cmdSearchOperator.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchOperator.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchOperator.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchOperator.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchOperator.Image = CType(resources.GetObject("cmdSearchOperator.Image"), System.Drawing.Image)
        Me.cmdSearchOperator.Location = New System.Drawing.Point(388, 54)
        Me.cmdSearchOperator.Name = "cmdSearchOperator"
        Me.cmdSearchOperator.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchOperator.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchOperator.TabIndex = 49
        Me.cmdSearchOperator.TabStop = False
        Me.cmdSearchOperator.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchOperator, "Search")
        Me.cmdSearchOperator.UseVisualStyleBackColor = False
        '
        'txtOperator
        '
        Me.txtOperator.AcceptsReturn = True
        Me.txtOperator.BackColor = System.Drawing.SystemColors.Window
        Me.txtOperator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOperator.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOperator.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOperator.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOperator.Location = New System.Drawing.Point(78, 54)
        Me.txtOperator.MaxLength = 0
        Me.txtOperator.Name = "txtOperator"
        Me.txtOperator.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOperator.Size = New System.Drawing.Size(71, 20)
        Me.txtOperator.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.txtOperator, "Press F1 For Help")
        '
        'txtOpr
        '
        Me.txtOpr.AcceptsReturn = True
        Me.txtOpr.BackColor = System.Drawing.SystemColors.Window
        Me.txtOpr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOpr.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOpr.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOpr.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOpr.Location = New System.Drawing.Point(78, 32)
        Me.txtOpr.MaxLength = 0
        Me.txtOpr.Name = "txtOpr"
        Me.txtOpr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOpr.Size = New System.Drawing.Size(307, 20)
        Me.txtOpr.TabIndex = 40
        Me.ToolTip1.SetToolTip(Me.txtOpr, "Press F1 For Help")
        '
        'cmdSearchOpr
        '
        Me.cmdSearchOpr.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchOpr.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchOpr.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchOpr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchOpr.Image = CType(resources.GetObject("cmdSearchOpr.Image"), System.Drawing.Image)
        Me.cmdSearchOpr.Location = New System.Drawing.Point(388, 32)
        Me.cmdSearchOpr.Name = "cmdSearchOpr"
        Me.cmdSearchOpr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchOpr.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchOpr.TabIndex = 39
        Me.cmdSearchOpr.TabStop = False
        Me.cmdSearchOpr.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchOpr, "Search")
        Me.cmdSearchOpr.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(388, 10)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearch.TabIndex = 3
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'TxtItemName
        '
        Me.TxtItemName.AcceptsReturn = True
        Me.TxtItemName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtItemName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtItemName.Location = New System.Drawing.Point(78, 10)
        Me.TxtItemName.MaxLength = 0
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtItemName.Size = New System.Drawing.Size(307, 20)
        Me.TxtItemName.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.TxtItemName, "Press F1 For Help")
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(123, 9)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.CmdPreview.TabIndex = 8
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
        Me.cmdPrint.Location = New System.Drawing.Point(63, 9)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 7
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
        Me.cmdClose.Location = New System.Drawing.Point(184, 9)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 37)
        Me.cmdClose.TabIndex = 9
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
        Me.cmdShow.Location = New System.Drawing.Point(4, 9)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(60, 37)
        Me.cmdShow.TabIndex = 6
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'chkShowFinal
        '
        Me.chkShowFinal.AutoSize = True
        Me.chkShowFinal.BackColor = System.Drawing.SystemColors.Control
        Me.chkShowFinal.Checked = True
        Me.chkShowFinal.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShowFinal.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShowFinal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowFinal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShowFinal.Location = New System.Drawing.Point(547, 575)
        Me.chkShowFinal.Name = "chkShowFinal"
        Me.chkShowFinal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShowFinal.Size = New System.Drawing.Size(105, 18)
        Me.chkShowFinal.TabIndex = 45
        Me.chkShowFinal.Text = "Show Final Only"
        Me.chkShowFinal.UseVisualStyleBackColor = False
        '
        'chkSPD
        '
        Me.chkSPD.AutoSize = True
        Me.chkSPD.BackColor = System.Drawing.SystemColors.Control
        Me.chkSPD.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSPD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSPD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSPD.Location = New System.Drawing.Point(547, 599)
        Me.chkSPD.Name = "chkSPD"
        Me.chkSPD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSPD.Size = New System.Drawing.Size(78, 18)
        Me.chkSPD.TabIndex = 44
        Me.chkSPD.Text = "Show SPD"
        Me.chkSPD.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._OptDetSumm_2)
        Me.Frame3.Controls.Add(Me._OptDetSumm_1)
        Me.Frame3.Controls.Add(Me._OptDetSumm_0)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(299, 565)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(243, 49)
        Me.Frame3.TabIndex = 23
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Report Option"
        '
        '_OptDetSumm_2
        '
        Me._OptDetSumm_2.AutoSize = True
        Me._OptDetSumm_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptDetSumm_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDetSumm_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDetSumm_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDetSumm.SetIndex(Me._OptDetSumm_2, CType(2, Short))
        Me._OptDetSumm_2.Location = New System.Drawing.Point(116, 14)
        Me._OptDetSumm_2.Name = "_OptDetSumm_2"
        Me._OptDetSumm_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDetSumm_2.Size = New System.Drawing.Size(157, 18)
        Me._OptDetSumm_2.TabIndex = 53
        Me._OptDetSumm_2.TabStop = True
        Me._OptDetSumm_2.Text = "Summarised Operator Wise"
        Me._OptDetSumm_2.UseVisualStyleBackColor = False
        '
        '_OptDetSumm_1
        '
        Me._OptDetSumm_1.AutoSize = True
        Me._OptDetSumm_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptDetSumm_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDetSumm_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDetSumm_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDetSumm.SetIndex(Me._OptDetSumm_1, CType(1, Short))
        Me._OptDetSumm_1.Location = New System.Drawing.Point(6, 32)
        Me._OptDetSumm_1.Name = "_OptDetSumm_1"
        Me._OptDetSumm_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDetSumm_1.Size = New System.Drawing.Size(84, 18)
        Me._OptDetSumm_1.TabIndex = 25
        Me._OptDetSumm_1.TabStop = True
        Me._OptDetSumm_1.Text = "Summarised"
        Me._OptDetSumm_1.UseVisualStyleBackColor = False
        '
        '_OptDetSumm_0
        '
        Me._OptDetSumm_0.AutoSize = True
        Me._OptDetSumm_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptDetSumm_0.Checked = True
        Me._OptDetSumm_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDetSumm_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDetSumm_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDetSumm.SetIndex(Me._OptDetSumm_0, CType(0, Short))
        Me._OptDetSumm_0.Location = New System.Drawing.Point(6, 14)
        Me._OptDetSumm_0.Name = "_OptDetSumm_0"
        Me._OptDetSumm_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDetSumm_0.Size = New System.Drawing.Size(63, 18)
        Me._OptDetSumm_0.TabIndex = 24
        Me._OptDetSumm_0.TabStop = True
        Me._OptDetSumm_0.Text = "Detailed"
        Me._OptDetSumm_0.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.chkTime)
        Me.Frame1.Controls.Add(Me.txtTMFrom)
        Me.Frame1.Controls.Add(Me.txtTMTo)
        Me.Frame1.Controls.Add(Me._Lbl_3)
        Me.Frame1.Controls.Add(Me._Lbl_2)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(592, -2)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(230, 44)
        Me.Frame1.TabIndex = 22
        Me.Frame1.TabStop = False
        '
        'chkTime
        '
        Me.chkTime.AutoSize = True
        Me.chkTime.BackColor = System.Drawing.SystemColors.Control
        Me.chkTime.Checked = True
        Me.chkTime.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTime.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTime.Location = New System.Drawing.Point(173, 14)
        Me.chkTime.Name = "chkTime"
        Me.chkTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTime.Size = New System.Drawing.Size(46, 18)
        Me.chkTime.TabIndex = 33
        Me.chkTime.Text = "ALL"
        Me.chkTime.UseVisualStyleBackColor = False
        '
        'txtTMFrom
        '
        Me.txtTMFrom.AllowPromptAsInput = False
        Me.txtTMFrom.Enabled = False
        Me.txtTMFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTMFrom.Location = New System.Drawing.Point(40, 14)
        Me.txtTMFrom.Mask = "##:##"
        Me.txtTMFrom.Name = "txtTMFrom"
        Me.txtTMFrom.Size = New System.Drawing.Size(43, 20)
        Me.txtTMFrom.TabIndex = 34
        '
        'txtTMTo
        '
        Me.txtTMTo.AllowPromptAsInput = False
        Me.txtTMTo.Enabled = False
        Me.txtTMTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTMTo.Location = New System.Drawing.Point(110, 14)
        Me.txtTMTo.Mask = "##:##"
        Me.txtTMTo.Name = "txtTMTo"
        Me.txtTMTo.Size = New System.Drawing.Size(43, 20)
        Me.txtTMTo.TabIndex = 35
        '
        '_Lbl_3
        '
        Me._Lbl_3.AutoSize = True
        Me._Lbl_3.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_3, CType(3, Short))
        Me._Lbl_3.Location = New System.Drawing.Point(86, 17)
        Me._Lbl_3.Name = "_Lbl_3"
        Me._Lbl_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_3.Size = New System.Drawing.Size(24, 14)
        Me._Lbl_3.TabIndex = 37
        Me._Lbl_3.Text = "To :"
        Me._Lbl_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Lbl_2
        '
        Me._Lbl_2.AutoSize = True
        Me._Lbl_2.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_2, CType(2, Short))
        Me._Lbl_2.Location = New System.Drawing.Point(4, 17)
        Me._Lbl_2.Name = "_Lbl_2"
        Me._Lbl_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_2.Size = New System.Drawing.Size(37, 14)
        Me._Lbl_2.TabIndex = 36
        Me._Lbl_2.Text = "From :"
        Me._Lbl_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._OptOrderBy_0)
        Me.Frame2.Controls.Add(Me._OptOrderBy_1)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(161, 565)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(137, 49)
        Me.Frame2.TabIndex = 19
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Order By"
        '
        '_OptOrderBy_0
        '
        Me._OptOrderBy_0.AutoSize = True
        Me._OptOrderBy_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_0.Checked = True
        Me._OptOrderBy_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_0, CType(0, Short))
        Me._OptOrderBy_0.Location = New System.Drawing.Point(4, 14)
        Me._OptOrderBy_0.Name = "_OptOrderBy_0"
        Me._OptOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_0.Size = New System.Drawing.Size(61, 18)
        Me._OptOrderBy_0.TabIndex = 21
        Me._OptOrderBy_0.TabStop = True
        Me._OptOrderBy_0.Text = "Ref No."
        Me._OptOrderBy_0.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_1
        '
        Me._OptOrderBy_1.AutoSize = True
        Me._OptOrderBy_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_1, CType(1, Short))
        Me._OptOrderBy_1.Location = New System.Drawing.Point(4, 32)
        Me._OptOrderBy_1.Name = "_OptOrderBy_1"
        Me._OptOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_1.Size = New System.Drawing.Size(101, 18)
        Me._OptOrderBy_1.TabIndex = 20
        Me._OptOrderBy_1.TabStop = True
        Me._OptOrderBy_1.Text = "Item Description"
        Me._OptOrderBy_1.UseVisualStyleBackColor = False
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.txtOperatorName)
        Me.FraAccount.Controls.Add(Me.chkOperatorAll)
        Me.FraAccount.Controls.Add(Me.cmdSearchOperator)
        Me.FraAccount.Controls.Add(Me.txtOperator)
        Me.FraAccount.Controls.Add(Me.txtOpr)
        Me.FraAccount.Controls.Add(Me.cmdSearchOpr)
        Me.FraAccount.Controls.Add(Me.chkOprAll)
        Me.FraAccount.Controls.Add(Me.chkAll)
        Me.FraAccount.Controls.Add(Me.cmdsearch)
        Me.FraAccount.Controls.Add(Me.TxtItemName)
        Me.FraAccount.Controls.Add(Me.Label7)
        Me.FraAccount.Controls.Add(Me.Label3)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(122, -2)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(469, 79)
        Me.FraAccount.TabIndex = 13
        Me.FraAccount.TabStop = False
        '
        'chkOperatorAll
        '
        Me.chkOperatorAll.AutoSize = True
        Me.chkOperatorAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkOperatorAll.Checked = True
        Me.chkOperatorAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOperatorAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOperatorAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOperatorAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOperatorAll.Location = New System.Drawing.Point(420, 56)
        Me.chkOperatorAll.Name = "chkOperatorAll"
        Me.chkOperatorAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOperatorAll.Size = New System.Drawing.Size(46, 18)
        Me.chkOperatorAll.TabIndex = 50
        Me.chkOperatorAll.Text = "ALL"
        Me.chkOperatorAll.UseVisualStyleBackColor = False
        '
        'chkOprAll
        '
        Me.chkOprAll.AutoSize = True
        Me.chkOprAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkOprAll.Checked = True
        Me.chkOprAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOprAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOprAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOprAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOprAll.Location = New System.Drawing.Point(420, 34)
        Me.chkOprAll.Name = "chkOprAll"
        Me.chkOprAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOprAll.Size = New System.Drawing.Size(46, 18)
        Me.chkOprAll.TabIndex = 38
        Me.chkOprAll.Text = "ALL"
        Me.chkOprAll.UseVisualStyleBackColor = False
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(420, 12)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(46, 18)
        Me.chkAll.TabIndex = 4
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(19, 56)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(56, 14)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "Operator :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(13, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(60, 14)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Operation :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(9, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(62, 14)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Item Name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me._optDate_1)
        Me.Frame6.Controls.Add(Me._optDate_0)
        Me.Frame6.Controls.Add(Me.txtDateFrom)
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me._Lbl_1)
        Me.Frame6.Controls.Add(Me._Lbl_0)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, -2)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(121, 107)
        Me.Frame6.TabIndex = 10
        Me.Frame6.TabStop = False
        '
        '_optDate_1
        '
        Me._optDate_1.AutoSize = True
        Me._optDate_1.BackColor = System.Drawing.SystemColors.Control
        Me._optDate_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optDate_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optDate_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDate.SetIndex(Me._optDate_1, CType(1, Short))
        Me._optDate_1.Location = New System.Drawing.Point(2, 30)
        Me._optDate_1.Name = "_optDate_1"
        Me._optDate_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optDate_1.Size = New System.Drawing.Size(67, 18)
        Me._optDate_1.TabIndex = 43
        Me._optDate_1.TabStop = True
        Me._optDate_1.Text = "Ref Date"
        Me._optDate_1.UseVisualStyleBackColor = False
        '
        '_optDate_0
        '
        Me._optDate_0.AutoSize = True
        Me._optDate_0.BackColor = System.Drawing.SystemColors.Control
        Me._optDate_0.Checked = True
        Me._optDate_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optDate_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optDate_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDate.SetIndex(Me._optDate_0, CType(0, Short))
        Me._optDate_0.Location = New System.Drawing.Point(2, 12)
        Me._optDate_0.Name = "_optDate_0"
        Me._optDate_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optDate_0.Size = New System.Drawing.Size(101, 18)
        Me._optDate_0.TabIndex = 42
        Me._optDate_0.TabStop = True
        Me._optDate_0.Text = "Production Date"
        Me._optDate_0.UseVisualStyleBackColor = False
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(42, 48)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(75, 20)
        Me.txtDateFrom.TabIndex = 0
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(42, 72)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(75, 20)
        Me.txtDateTo.TabIndex = 1
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(4, 74)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(24, 14)
        Me._Lbl_1.TabIndex = 12
        Me._Lbl_1.Text = "To :"
        Me._Lbl_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(4, 52)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(37, 14)
        Me._Lbl_0.TabIndex = 11
        Me._Lbl_0.Text = "From :"
        Me._Lbl_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.cboApproved)
        Me.Frame5.Controls.Add(Me.cboShift)
        Me.Frame5.Controls.Add(Me.cboDept)
        Me.Frame5.Controls.Add(Me.cboType)
        Me.Frame5.Controls.Add(Me.Label6)
        Me.Frame5.Controls.Add(Me.Label4)
        Me.Frame5.Controls.Add(Me.Label5)
        Me.Frame5.Controls.Add(Me.Label1)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(122, 72)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(627, 33)
        Me.Frame5.TabIndex = 26
        Me.Frame5.TabStop = False
        '
        'cboApproved
        '
        Me.cboApproved.BackColor = System.Drawing.SystemColors.Window
        Me.cboApproved.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboApproved.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboApproved.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboApproved.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboApproved.Location = New System.Drawing.Point(480, 8)
        Me.cboApproved.Name = "cboApproved"
        Me.cboApproved.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboApproved.Size = New System.Drawing.Size(85, 22)
        Me.cboApproved.TabIndex = 46
        '
        'cboShift
        '
        Me.cboShift.BackColor = System.Drawing.SystemColors.Window
        Me.cboShift.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShift.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShift.Location = New System.Drawing.Point(353, 8)
        Me.cboShift.Name = "cboShift"
        Me.cboShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShift.Size = New System.Drawing.Size(49, 22)
        Me.cboShift.TabIndex = 31
        '
        'cboDept
        '
        Me.cboDept.BackColor = System.Drawing.SystemColors.Window
        Me.cboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDept.Location = New System.Drawing.Point(170, 8)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(143, 22)
        Me.cboDept.TabIndex = 29
        '
        'cboType
        '
        Me.cboType.BackColor = System.Drawing.SystemColors.Window
        Me.cboType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboType.Location = New System.Drawing.Point(42, 8)
        Me.cboType.Name = "cboType"
        Me.cboType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboType.Size = New System.Drawing.Size(85, 22)
        Me.cboType.TabIndex = 27
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(416, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(61, 14)
        Me.Label6.TabIndex = 47
        Me.Label6.Text = "Approved :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(316, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(35, 14)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Shift :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(130, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "Dept :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(4, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(36, 14)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Type :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 100)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(979, 460)
        Me.Frame4.TabIndex = 14
        Me.Frame4.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 13)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(979, 447)
        Me.SprdMain.TabIndex = 5
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 70)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 6
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(698, 565)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(247, 49)
        Me.FraMovement.TabIndex = 15
        Me.FraMovement.TabStop = False
        '
        'lblAcCode
        '
        Me.lblAcCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAcCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAcCode.Location = New System.Drawing.Point(122, 428)
        Me.lblAcCode.Name = "lblAcCode"
        Me.lblAcCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcCode.Size = New System.Drawing.Size(87, 13)
        Me.lblAcCode.TabIndex = 17
        Me.lblAcCode.Text = "lblAcCode"
        Me.lblAcCode.Visible = False
        '
        'lblTrnType
        '
        Me.lblTrnType.AutoSize = True
        Me.lblTrnType.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrnType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTrnType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrnType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTrnType.Location = New System.Drawing.Point(172, 432)
        Me.lblTrnType.Name = "lblTrnType"
        Me.lblTrnType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTrnType.Size = New System.Drawing.Size(56, 14)
        Me.lblTrnType.TabIndex = 16
        Me.lblTrnType.Text = "lblTrnType"
        Me.lblTrnType.Visible = False
        '
        'OptDetSumm
        '
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.lstCompanyName)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(825, -3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(154, 111)
        Me.GroupBox1.TabIndex = 49
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Company Name"
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
        Me.lstCompanyName.Size = New System.Drawing.Size(154, 98)
        Me.lstCompanyName.TabIndex = 3
        '
        'cboMachineNo
        '
        Me.cboMachineNo.BackColor = System.Drawing.SystemColors.Window
        Me.cboMachineNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMachineNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMachineNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMachineNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMachineNo.Location = New System.Drawing.Point(660, 52)
        Me.cboMachineNo.Name = "cboMachineNo"
        Me.cboMachineNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMachineNo.Size = New System.Drawing.Size(159, 22)
        Me.cboMachineNo.TabIndex = 50
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(596, 54)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(53, 14)
        Me.Label8.TabIndex = 51
        Me.Label8.Text = "Machine :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmParamDeptWiseProdReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(980, 614)
        Me.Controls.Add(Me.cboMachineNo)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.chkShowFinal)
        Me.Controls.Add(Me.chkSPD)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.lblAcCode)
        Me.Controls.Add(Me.lblTrnType)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamDeptWiseProdReg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Production Register"
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptDetSumm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdMain.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdMain.DataSource = Nothing
    End Sub

    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents lstCompanyName As CheckedListBox
    Public WithEvents cboMachineNo As ComboBox
    Public WithEvents Label8 As Label
#End Region
End Class