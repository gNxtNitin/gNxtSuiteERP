Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamGaugeFixMst
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
    Public WithEvents cmdSearchDRGNo As System.Windows.Forms.Button
    Public WithEvents chkAllDRG As System.Windows.Forms.CheckBox
    Public WithEvents txtDrawingNo As System.Windows.Forms.TextBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents cboLCDate As System.Windows.Forms.ComboBox
    Public WithEvents txtDate4 As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDate3 As System.Windows.Forms.MaskedTextBox
    Public WithEvents lblDate3 As System.Windows.Forms.Label
    Public WithEvents lblDate4 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents cboStatus As System.Windows.Forms.ComboBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents _OptOrderBy_7 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_6 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_5 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_4 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_3 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cboCDDate As System.Windows.Forms.ComboBox
    Public WithEvents txtDate2 As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDate1 As System.Windows.Forms.MaskedTextBox
    Public WithEvents lblDate1 As System.Windows.Forms.Label
    Public WithEvents lblDate2 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents chkAllComponent As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchComponent As System.Windows.Forms.Button
    Public WithEvents txtComponent As System.Windows.Forms.TextBox
    Public WithEvents chkAllType As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchType As System.Windows.Forms.Button
    Public WithEvents txtType As System.Windows.Forms.TextBox
    Public WithEvents txtLocation As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchLocation As System.Windows.Forms.Button
    Public WithEvents chkAllLocation As System.Windows.Forms.CheckBox
    Public WithEvents txtTypeNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchTypeNo As System.Windows.Forms.Button
    Public WithEvents chkAllTypeNo As System.Windows.Forms.CheckBox
    Public WithEvents txtCustomer As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchCustomer As System.Windows.Forms.Button
    Public WithEvents chkAllCustomer As System.Windows.Forms.CheckBox
    Public WithEvents chkAllModel As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchModel As System.Windows.Forms.Button
    Public WithEvents txtModel As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents OptOrderBy As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamGaugeFixMst))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchDRGNo = New System.Windows.Forms.Button()
        Me.txtDrawingNo = New System.Windows.Forms.TextBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdSearchComponent = New System.Windows.Forms.Button()
        Me.txtComponent = New System.Windows.Forms.TextBox()
        Me.cmdSearchType = New System.Windows.Forms.Button()
        Me.txtType = New System.Windows.Forms.TextBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.cmdSearchLocation = New System.Windows.Forms.Button()
        Me.txtTypeNo = New System.Windows.Forms.TextBox()
        Me.cmdSearchTypeNo = New System.Windows.Forms.Button()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.cmdSearchCustomer = New System.Windows.Forms.Button()
        Me.cmdSearchModel = New System.Windows.Forms.Button()
        Me.txtModel = New System.Windows.Forms.TextBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.chkAllDRG = New System.Windows.Forms.CheckBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.cboLCDate = New System.Windows.Forms.ComboBox()
        Me.txtDate4 = New System.Windows.Forms.MaskedTextBox()
        Me.txtDate3 = New System.Windows.Forms.MaskedTextBox()
        Me.lblDate3 = New System.Windows.Forms.Label()
        Me.lblDate4 = New System.Windows.Forms.Label()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._OptOrderBy_7 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_6 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_5 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_4 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_2 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_3 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboCDDate = New System.Windows.Forms.ComboBox()
        Me.txtDate2 = New System.Windows.Forms.MaskedTextBox()
        Me.txtDate1 = New System.Windows.Forms.MaskedTextBox()
        Me.lblDate1 = New System.Windows.Forms.Label()
        Me.lblDate2 = New System.Windows.Forms.Label()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkAllComponent = New System.Windows.Forms.CheckBox()
        Me.chkAllType = New System.Windows.Forms.CheckBox()
        Me.chkAllLocation = New System.Windows.Forms.CheckBox()
        Me.chkAllTypeNo = New System.Windows.Forms.CheckBox()
        Me.chkAllCustomer = New System.Windows.Forms.CheckBox()
        Me.chkAllModel = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.OptOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame5.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchDRGNo
        '
        Me.cmdSearchDRGNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDRGNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDRGNo.Enabled = False
        Me.cmdSearchDRGNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDRGNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDRGNo.Image = CType(resources.GetObject("cmdSearchDRGNo.Image"), System.Drawing.Image)
        Me.cmdSearchDRGNo.Location = New System.Drawing.Point(132, 16)
        Me.cmdSearchDRGNo.Name = "cmdSearchDRGNo"
        Me.cmdSearchDRGNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDRGNo.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchDRGNo.TabIndex = 57
        Me.cmdSearchDRGNo.TabStop = False
        Me.cmdSearchDRGNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDRGNo, "Search")
        Me.cmdSearchDRGNo.UseVisualStyleBackColor = False
        '
        'txtDrawingNo
        '
        Me.txtDrawingNo.AcceptsReturn = True
        Me.txtDrawingNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDrawingNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDrawingNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDrawingNo.Enabled = False
        Me.txtDrawingNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDrawingNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDrawingNo.Location = New System.Drawing.Point(4, 16)
        Me.txtDrawingNo.MaxLength = 0
        Me.txtDrawingNo.Name = "txtDrawingNo"
        Me.txtDrawingNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDrawingNo.Size = New System.Drawing.Size(127, 20)
        Me.txtDrawingNo.TabIndex = 55
        Me.ToolTip1.SetToolTip(Me.txtDrawingNo, "Press F1 For Help")
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(250, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 37)
        Me.cmdClose.TabIndex = 37
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(182, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 36
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
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
        Me.CmdPreview.TabIndex = 35
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
        Me.cmdPrint.TabIndex = 34
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
        Me.cmdShow.TabIndex = 33
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'cmdSearchComponent
        '
        Me.cmdSearchComponent.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchComponent.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchComponent.Enabled = False
        Me.cmdSearchComponent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchComponent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchComponent.Image = CType(resources.GetObject("cmdSearchComponent.Image"), System.Drawing.Image)
        Me.cmdSearchComponent.Location = New System.Drawing.Point(312, 108)
        Me.cmdSearchComponent.Name = "cmdSearchComponent"
        Me.cmdSearchComponent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchComponent.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchComponent.TabIndex = 49
        Me.cmdSearchComponent.TabStop = False
        Me.cmdSearchComponent.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchComponent, "Search")
        Me.cmdSearchComponent.UseVisualStyleBackColor = False
        '
        'txtComponent
        '
        Me.txtComponent.AcceptsReturn = True
        Me.txtComponent.BackColor = System.Drawing.SystemColors.Window
        Me.txtComponent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComponent.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtComponent.Enabled = False
        Me.txtComponent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComponent.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtComponent.Location = New System.Drawing.Point(74, 108)
        Me.txtComponent.MaxLength = 0
        Me.txtComponent.Name = "txtComponent"
        Me.txtComponent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtComponent.Size = New System.Drawing.Size(237, 20)
        Me.txtComponent.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.txtComponent, "Press F1 For Help")
        '
        'cmdSearchType
        '
        Me.cmdSearchType.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchType.Enabled = False
        Me.cmdSearchType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchType.Image = CType(resources.GetObject("cmdSearchType.Image"), System.Drawing.Image)
        Me.cmdSearchType.Location = New System.Drawing.Point(312, 88)
        Me.cmdSearchType.Name = "cmdSearchType"
        Me.cmdSearchType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchType.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchType.TabIndex = 45
        Me.cmdSearchType.TabStop = False
        Me.cmdSearchType.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchType, "Search")
        Me.cmdSearchType.UseVisualStyleBackColor = False
        '
        'txtType
        '
        Me.txtType.AcceptsReturn = True
        Me.txtType.BackColor = System.Drawing.SystemColors.Window
        Me.txtType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtType.Enabled = False
        Me.txtType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtType.Location = New System.Drawing.Point(74, 88)
        Me.txtType.MaxLength = 0
        Me.txtType.Name = "txtType"
        Me.txtType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtType.Size = New System.Drawing.Size(237, 20)
        Me.txtType.TabIndex = 44
        Me.ToolTip1.SetToolTip(Me.txtType, "Press F1 For Help")
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
        Me.txtLocation.Location = New System.Drawing.Point(74, 68)
        Me.txtLocation.MaxLength = 0
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocation.Size = New System.Drawing.Size(237, 20)
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
        Me.cmdSearchLocation.Location = New System.Drawing.Point(312, 68)
        Me.cmdSearchLocation.Name = "cmdSearchLocation"
        Me.cmdSearchLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchLocation.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchLocation.TabIndex = 10
        Me.cmdSearchLocation.TabStop = False
        Me.cmdSearchLocation.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchLocation, "Search")
        Me.cmdSearchLocation.UseVisualStyleBackColor = False
        '
        'txtTypeNo
        '
        Me.txtTypeNo.AcceptsReturn = True
        Me.txtTypeNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTypeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTypeNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTypeNo.Enabled = False
        Me.txtTypeNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTypeNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTypeNo.Location = New System.Drawing.Point(74, 48)
        Me.txtTypeNo.MaxLength = 0
        Me.txtTypeNo.Name = "txtTypeNo"
        Me.txtTypeNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTypeNo.Size = New System.Drawing.Size(237, 20)
        Me.txtTypeNo.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtTypeNo, "Press F1 For Help")
        '
        'cmdSearchTypeNo
        '
        Me.cmdSearchTypeNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchTypeNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchTypeNo.Enabled = False
        Me.cmdSearchTypeNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchTypeNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchTypeNo.Image = CType(resources.GetObject("cmdSearchTypeNo.Image"), System.Drawing.Image)
        Me.cmdSearchTypeNo.Location = New System.Drawing.Point(312, 48)
        Me.cmdSearchTypeNo.Name = "cmdSearchTypeNo"
        Me.cmdSearchTypeNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchTypeNo.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchTypeNo.TabIndex = 7
        Me.cmdSearchTypeNo.TabStop = False
        Me.cmdSearchTypeNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchTypeNo, "Search")
        Me.cmdSearchTypeNo.UseVisualStyleBackColor = False
        '
        'txtCustomer
        '
        Me.txtCustomer.AcceptsReturn = True
        Me.txtCustomer.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomer.Enabled = False
        Me.txtCustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomer.Location = New System.Drawing.Point(74, 28)
        Me.txtCustomer.MaxLength = 0
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomer.Size = New System.Drawing.Size(237, 20)
        Me.txtCustomer.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtCustomer, "Press F1 For Help")
        '
        'cmdSearchCustomer
        '
        Me.cmdSearchCustomer.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCustomer.Enabled = False
        Me.cmdSearchCustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCustomer.Image = CType(resources.GetObject("cmdSearchCustomer.Image"), System.Drawing.Image)
        Me.cmdSearchCustomer.Location = New System.Drawing.Point(312, 28)
        Me.cmdSearchCustomer.Name = "cmdSearchCustomer"
        Me.cmdSearchCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCustomer.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchCustomer.TabIndex = 4
        Me.cmdSearchCustomer.TabStop = False
        Me.cmdSearchCustomer.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCustomer, "Search")
        Me.cmdSearchCustomer.UseVisualStyleBackColor = False
        '
        'cmdSearchModel
        '
        Me.cmdSearchModel.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchModel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchModel.Enabled = False
        Me.cmdSearchModel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchModel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchModel.Image = CType(resources.GetObject("cmdSearchModel.Image"), System.Drawing.Image)
        Me.cmdSearchModel.Location = New System.Drawing.Point(312, 8)
        Me.cmdSearchModel.Name = "cmdSearchModel"
        Me.cmdSearchModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchModel.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchModel.TabIndex = 1
        Me.cmdSearchModel.TabStop = False
        Me.cmdSearchModel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchModel, "Search")
        Me.cmdSearchModel.UseVisualStyleBackColor = False
        '
        'txtModel
        '
        Me.txtModel.AcceptsReturn = True
        Me.txtModel.BackColor = System.Drawing.SystemColors.Window
        Me.txtModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModel.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModel.Enabled = False
        Me.txtModel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtModel.Location = New System.Drawing.Point(74, 8)
        Me.txtModel.MaxLength = 0
        Me.txtModel.Name = "txtModel"
        Me.txtModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModel.Size = New System.Drawing.Size(237, 20)
        Me.txtModel.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtModel, "Press F1 For Help")
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.cmdSearchDRGNo)
        Me.Frame5.Controls.Add(Me.chkAllDRG)
        Me.Frame5.Controls.Add(Me.txtDrawingNo)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(394, 88)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(215, 43)
        Me.Frame5.TabIndex = 54
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Drawing No"
        '
        'chkAllDRG
        '
        Me.chkAllDRG.AutoSize = True
        Me.chkAllDRG.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllDRG.Checked = True
        Me.chkAllDRG.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllDRG.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllDRG.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllDRG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllDRG.Location = New System.Drawing.Point(162, 20)
        Me.chkAllDRG.Name = "chkAllDRG"
        Me.chkAllDRG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllDRG.Size = New System.Drawing.Size(43, 17)
        Me.chkAllDRG.TabIndex = 56
        Me.chkAllDRG.Text = "ALL"
        Me.chkAllDRG.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.cboLCDate)
        Me.Frame4.Controls.Add(Me.txtDate4)
        Me.Frame4.Controls.Add(Me.txtDate3)
        Me.Frame4.Controls.Add(Me.lblDate3)
        Me.Frame4.Controls.Add(Me.lblDate4)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(394, 42)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(369, 45)
        Me.Frame4.TabIndex = 38
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "V. Done On Condition"
        '
        'cboLCDate
        '
        Me.cboLCDate.BackColor = System.Drawing.SystemColors.Window
        Me.cboLCDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboLCDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLCDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLCDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboLCDate.Location = New System.Drawing.Point(4, 16)
        Me.cboLCDate.Name = "cboLCDate"
        Me.cboLCDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboLCDate.Size = New System.Drawing.Size(99, 22)
        Me.cboLCDate.TabIndex = 39
        '
        'txtDate4
        '
        Me.txtDate4.AllowPromptAsInput = False
        Me.txtDate4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate4.Location = New System.Drawing.Point(288, 16)
        Me.txtDate4.Mask = "##/##/####"
        Me.txtDate4.Name = "txtDate4"
        Me.txtDate4.Size = New System.Drawing.Size(76, 20)
        Me.txtDate4.TabIndex = 40
        '
        'txtDate3
        '
        Me.txtDate3.AllowPromptAsInput = False
        Me.txtDate3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate3.Location = New System.Drawing.Point(156, 16)
        Me.txtDate3.Mask = "##/##/####"
        Me.txtDate3.Name = "txtDate3"
        Me.txtDate3.Size = New System.Drawing.Size(76, 20)
        Me.txtDate3.TabIndex = 41
        '
        'lblDate3
        '
        Me.lblDate3.AutoSize = True
        Me.lblDate3.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate3.Location = New System.Drawing.Point(106, 21)
        Me.lblDate3.Name = "lblDate3"
        Me.lblDate3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate3.Size = New System.Drawing.Size(47, 13)
        Me.lblDate3.TabIndex = 43
        Me.lblDate3.Text = "Date 1 : "
        Me.lblDate3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDate4
        '
        Me.lblDate4.AutoSize = True
        Me.lblDate4.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate4.Location = New System.Drawing.Point(230, 20)
        Me.lblDate4.Name = "lblDate4"
        Me.lblDate4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate4.Size = New System.Drawing.Size(49, 13)
        Me.lblDate4.TabIndex = 42
        Me.lblDate4.Text = "Date 2 : "
        Me.lblDate4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 132)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(760, 275)
        Me.SprdMain.TabIndex = 15
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cboStatus)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(610, 88)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(149, 43)
        Me.Frame3.TabIndex = 24
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Status"
        '
        'cboStatus
        '
        Me.cboStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboStatus.Location = New System.Drawing.Point(48, 14)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboStatus.Size = New System.Drawing.Size(99, 22)
        Me.cboStatus.TabIndex = 25
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(444, 408)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(315, 51)
        Me.FraMovement.TabIndex = 32
        Me.FraMovement.TabStop = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._OptOrderBy_7)
        Me.Frame2.Controls.Add(Me._OptOrderBy_6)
        Me.Frame2.Controls.Add(Me._OptOrderBy_5)
        Me.Frame2.Controls.Add(Me._OptOrderBy_4)
        Me.Frame2.Controls.Add(Me._OptOrderBy_0)
        Me.Frame2.Controls.Add(Me._OptOrderBy_1)
        Me.Frame2.Controls.Add(Me._OptOrderBy_2)
        Me.Frame2.Controls.Add(Me._OptOrderBy_3)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 408)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(379, 51)
        Me.Frame2.TabIndex = 26
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Order By"
        '
        '_OptOrderBy_7
        '
        Me._OptOrderBy_7.AutoSize = True
        Me._OptOrderBy_7.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_7, CType(7, Short))
        Me._OptOrderBy_7.Location = New System.Drawing.Point(274, 34)
        Me._OptOrderBy_7.Name = "_OptOrderBy_7"
        Me._OptOrderBy_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_7.Size = New System.Drawing.Size(85, 17)
        Me._OptOrderBy_7.TabIndex = 58
        Me._OptOrderBy_7.TabStop = True
        Me._OptOrderBy_7.Text = "Drawing No"
        Me._OptOrderBy_7.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_6
        '
        Me._OptOrderBy_6.AutoSize = True
        Me._OptOrderBy_6.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_6, CType(6, Short))
        Me._OptOrderBy_6.Location = New System.Drawing.Point(274, 16)
        Me._OptOrderBy_6.Name = "_OptOrderBy_6"
        Me._OptOrderBy_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_6.Size = New System.Drawing.Size(85, 17)
        Me._OptOrderBy_6.TabIndex = 53
        Me._OptOrderBy_6.TabStop = True
        Me._OptOrderBy_6.Text = "Component"
        Me._OptOrderBy_6.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_5
        '
        Me._OptOrderBy_5.AutoSize = True
        Me._OptOrderBy_5.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_5, CType(5, Short))
        Me._OptOrderBy_5.Location = New System.Drawing.Point(184, 34)
        Me._OptOrderBy_5.Name = "_OptOrderBy_5"
        Me._OptOrderBy_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_5.Size = New System.Drawing.Size(49, 17)
        Me._OptOrderBy_5.TabIndex = 52
        Me._OptOrderBy_5.TabStop = True
        Me._OptOrderBy_5.Text = "Type"
        Me._OptOrderBy_5.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_4
        '
        Me._OptOrderBy_4.AutoSize = True
        Me._OptOrderBy_4.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_4, CType(4, Short))
        Me._OptOrderBy_4.Location = New System.Drawing.Point(184, 16)
        Me._OptOrderBy_4.Name = "_OptOrderBy_4"
        Me._OptOrderBy_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_4.Size = New System.Drawing.Size(63, 17)
        Me._OptOrderBy_4.TabIndex = 31
        Me._OptOrderBy_4.TabStop = True
        Me._OptOrderBy_4.Text = "Doc No"
        Me._OptOrderBy_4.UseVisualStyleBackColor = False
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
        Me._OptOrderBy_0.Location = New System.Drawing.Point(4, 16)
        Me._OptOrderBy_0.Name = "_OptOrderBy_0"
        Me._OptOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_0.Size = New System.Drawing.Size(74, 17)
        Me._OptOrderBy_0.TabIndex = 30
        Me._OptOrderBy_0.TabStop = True
        Me._OptOrderBy_0.Text = "V. Due On"
        Me._OptOrderBy_0.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_1
        '
        Me._OptOrderBy_1.AutoSize = True
        Me._OptOrderBy_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_1, CType(1, Short))
        Me._OptOrderBy_1.Location = New System.Drawing.Point(4, 34)
        Me._OptOrderBy_1.Name = "_OptOrderBy_1"
        Me._OptOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_1.Size = New System.Drawing.Size(58, 17)
        Me._OptOrderBy_1.TabIndex = 29
        Me._OptOrderBy_1.TabStop = True
        Me._OptOrderBy_1.Text = "Model"
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
        Me._OptOrderBy_2.Location = New System.Drawing.Point(96, 16)
        Me._OptOrderBy_2.Name = "_OptOrderBy_2"
        Me._OptOrderBy_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_2.Size = New System.Drawing.Size(74, 17)
        Me._OptOrderBy_2.TabIndex = 28
        Me._OptOrderBy_2.TabStop = True
        Me._OptOrderBy_2.Text = "Customer"
        Me._OptOrderBy_2.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_3
        '
        Me._OptOrderBy_3.AutoSize = True
        Me._OptOrderBy_3.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_3, CType(3, Short))
        Me._OptOrderBy_3.Location = New System.Drawing.Point(96, 34)
        Me._OptOrderBy_3.Name = "_OptOrderBy_3"
        Me._OptOrderBy_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_3.Size = New System.Drawing.Size(68, 17)
        Me._OptOrderBy_3.TabIndex = 27
        Me._OptOrderBy_3.TabStop = True
        Me._OptOrderBy_3.Text = "Location"
        Me._OptOrderBy_3.UseVisualStyleBackColor = False
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
        Me.Frame1.Size = New System.Drawing.Size(369, 41)
        Me.Frame1.TabIndex = 21
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "V. Due On Condition"
        '
        'cboCDDate
        '
        Me.cboCDDate.BackColor = System.Drawing.SystemColors.Window
        Me.cboCDDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCDDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCDDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCDDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCDDate.Location = New System.Drawing.Point(4, 16)
        Me.cboCDDate.Name = "cboCDDate"
        Me.cboCDDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCDDate.Size = New System.Drawing.Size(99, 22)
        Me.cboCDDate.TabIndex = 12
        '
        'txtDate2
        '
        Me.txtDate2.AllowPromptAsInput = False
        Me.txtDate2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate2.Location = New System.Drawing.Point(288, 14)
        Me.txtDate2.Mask = "##/##/####"
        Me.txtDate2.Name = "txtDate2"
        Me.txtDate2.Size = New System.Drawing.Size(76, 20)
        Me.txtDate2.TabIndex = 14
        '
        'txtDate1
        '
        Me.txtDate1.AllowPromptAsInput = False
        Me.txtDate1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate1.Location = New System.Drawing.Point(156, 14)
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
        Me.lblDate1.Location = New System.Drawing.Point(106, 18)
        Me.lblDate1.Name = "lblDate1"
        Me.lblDate1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate1.Size = New System.Drawing.Size(47, 13)
        Me.lblDate1.TabIndex = 23
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
        Me.lblDate2.Location = New System.Drawing.Point(230, 18)
        Me.lblDate2.Name = "lblDate2"
        Me.lblDate2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate2.Size = New System.Drawing.Size(49, 13)
        Me.lblDate2.TabIndex = 22
        Me.lblDate2.Text = "Date 2 : "
        Me.lblDate2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.chkAllComponent)
        Me.FraAccount.Controls.Add(Me.cmdSearchComponent)
        Me.FraAccount.Controls.Add(Me.txtComponent)
        Me.FraAccount.Controls.Add(Me.chkAllType)
        Me.FraAccount.Controls.Add(Me.cmdSearchType)
        Me.FraAccount.Controls.Add(Me.txtType)
        Me.FraAccount.Controls.Add(Me.txtLocation)
        Me.FraAccount.Controls.Add(Me.cmdSearchLocation)
        Me.FraAccount.Controls.Add(Me.chkAllLocation)
        Me.FraAccount.Controls.Add(Me.txtTypeNo)
        Me.FraAccount.Controls.Add(Me.cmdSearchTypeNo)
        Me.FraAccount.Controls.Add(Me.chkAllTypeNo)
        Me.FraAccount.Controls.Add(Me.txtCustomer)
        Me.FraAccount.Controls.Add(Me.cmdSearchCustomer)
        Me.FraAccount.Controls.Add(Me.chkAllCustomer)
        Me.FraAccount.Controls.Add(Me.chkAllModel)
        Me.FraAccount.Controls.Add(Me.cmdSearchModel)
        Me.FraAccount.Controls.Add(Me.txtModel)
        Me.FraAccount.Controls.Add(Me.Label6)
        Me.FraAccount.Controls.Add(Me.Label5)
        Me.FraAccount.Controls.Add(Me.Label4)
        Me.FraAccount.Controls.Add(Me.Label3)
        Me.FraAccount.Controls.Add(Me.Label1)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(0, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(393, 131)
        Me.FraAccount.TabIndex = 16
        Me.FraAccount.TabStop = False
        '
        'chkAllComponent
        '
        Me.chkAllComponent.AutoSize = True
        Me.chkAllComponent.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllComponent.Checked = True
        Me.chkAllComponent.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllComponent.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllComponent.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllComponent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllComponent.Location = New System.Drawing.Point(342, 112)
        Me.chkAllComponent.Name = "chkAllComponent"
        Me.chkAllComponent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllComponent.Size = New System.Drawing.Size(43, 17)
        Me.chkAllComponent.TabIndex = 50
        Me.chkAllComponent.Text = "ALL"
        Me.chkAllComponent.UseVisualStyleBackColor = False
        '
        'chkAllType
        '
        Me.chkAllType.AutoSize = True
        Me.chkAllType.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllType.Checked = True
        Me.chkAllType.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllType.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllType.Location = New System.Drawing.Point(342, 92)
        Me.chkAllType.Name = "chkAllType"
        Me.chkAllType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllType.Size = New System.Drawing.Size(43, 17)
        Me.chkAllType.TabIndex = 46
        Me.chkAllType.Text = "ALL"
        Me.chkAllType.UseVisualStyleBackColor = False
        '
        'chkAllLocation
        '
        Me.chkAllLocation.AutoSize = True
        Me.chkAllLocation.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllLocation.Checked = True
        Me.chkAllLocation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllLocation.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllLocation.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllLocation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllLocation.Location = New System.Drawing.Point(342, 72)
        Me.chkAllLocation.Name = "chkAllLocation"
        Me.chkAllLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllLocation.Size = New System.Drawing.Size(43, 17)
        Me.chkAllLocation.TabIndex = 11
        Me.chkAllLocation.Text = "ALL"
        Me.chkAllLocation.UseVisualStyleBackColor = False
        '
        'chkAllTypeNo
        '
        Me.chkAllTypeNo.AutoSize = True
        Me.chkAllTypeNo.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllTypeNo.Checked = True
        Me.chkAllTypeNo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllTypeNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllTypeNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllTypeNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllTypeNo.Location = New System.Drawing.Point(342, 52)
        Me.chkAllTypeNo.Name = "chkAllTypeNo"
        Me.chkAllTypeNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllTypeNo.Size = New System.Drawing.Size(43, 17)
        Me.chkAllTypeNo.TabIndex = 8
        Me.chkAllTypeNo.Text = "ALL"
        Me.chkAllTypeNo.UseVisualStyleBackColor = False
        '
        'chkAllCustomer
        '
        Me.chkAllCustomer.AutoSize = True
        Me.chkAllCustomer.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllCustomer.Checked = True
        Me.chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllCustomer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllCustomer.Location = New System.Drawing.Point(342, 32)
        Me.chkAllCustomer.Name = "chkAllCustomer"
        Me.chkAllCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllCustomer.Size = New System.Drawing.Size(43, 17)
        Me.chkAllCustomer.TabIndex = 5
        Me.chkAllCustomer.Text = "ALL"
        Me.chkAllCustomer.UseVisualStyleBackColor = False
        '
        'chkAllModel
        '
        Me.chkAllModel.AutoSize = True
        Me.chkAllModel.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllModel.Checked = True
        Me.chkAllModel.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllModel.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllModel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllModel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllModel.Location = New System.Drawing.Point(342, 12)
        Me.chkAllModel.Name = "chkAllModel"
        Me.chkAllModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllModel.Size = New System.Drawing.Size(43, 17)
        Me.chkAllModel.TabIndex = 2
        Me.chkAllModel.Text = "ALL"
        Me.chkAllModel.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(-2, 110)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(76, 13)
        Me.Label6.TabIndex = 51
        Me.Label6.Text = "Component : "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(33, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 47
        Me.Label5.Text = "Type : "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(3, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Location : "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(3, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Type No : "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(3, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Customer : "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(3, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Model : "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 174)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 56
        '
        'frmParamGaugeFixMst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(760, 459)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.SprdMain)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Report1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(11, 27)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamGaugeFixMst"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Gauge Fixture Master List"
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.FraMovement.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
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