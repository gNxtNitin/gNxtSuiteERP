Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamWIPDetail
#Region "Windows Form Designer generated code "
    <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
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
    Public WithEvents txtToDate As System.Windows.Forms.TextBox
    Public WithEvents txtFromDate As System.Windows.Forms.TextBox
    Public WithEvents txtConsumption As System.Windows.Forms.TextBox
    Public WithEvents txtItemName As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents lblItemUOM As System.Windows.Forms.Label
    Public WithEvents lblItemCode As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents AData1 As VB6.ADODC
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmParamWIPDetail))
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.Frame3 = New System.Windows.Forms.GroupBox
        Me.txtToDate = New System.Windows.Forms.TextBox
        Me.txtFromDate = New System.Windows.Forms.TextBox
        Me.txtConsumption = New System.Windows.Forms.TextBox
        Me.txtItemName = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread
        Me.Report1 = New AxCrystal.AxCrystalReport
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.CmdPreview = New System.Windows.Forms.Button
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.cmdExit = New System.Windows.Forms.Button
        Me.lblItemUOM = New System.Windows.Forms.Label
        Me.lblItemCode = New System.Windows.Forms.Label
        Me.AData1 = New VB6.ADODC
        Me.Frame3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.SuspendLayout()
        Me.ToolTip1.Active = True
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Text = "WIP Detail"
        Me.ClientSize = New System.Drawing.Size(624, 458)
        Me.Location = New System.Drawing.Point(3, 23)
        Me.Icon = CType(resources.GetObject("frmParamWIPDetail.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ControlBox = True
        Me.Enabled = True
        Me.KeyPreview = False
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = True
        Me.HelpButton = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Normal
        Me.Name = "frmParamWIPDetail"
        Me.Frame3.Size = New System.Drawing.Size(623, 63)
        Me.Frame3.Location = New System.Drawing.Point(0, -4)
        Me.Frame3.TabIndex = 5
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Enabled = True
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Visible = True
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.Name = "Frame3"
        Me.txtToDate.AutoSize = False
        Me.txtToDate.Enabled = False
        Me.txtToDate.ForeColor = System.Drawing.Color.Blue
        Me.txtToDate.Size = New System.Drawing.Size(101, 19)
        Me.txtToDate.Location = New System.Drawing.Point(518, 38)
        Me.txtToDate.TabIndex = 14
        Me.txtToDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.AcceptsReturn = True
        Me.txtToDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtToDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtToDate.CausesValidation = True
        Me.txtToDate.HideSelection = True
        Me.txtToDate.ReadOnly = False
        Me.txtToDate.MaxLength = 0
        Me.txtToDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToDate.Multiline = False
        Me.txtToDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtToDate.TabStop = True
        Me.txtToDate.Visible = True
        Me.txtToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToDate.Name = "txtToDate"
        Me.txtFromDate.AutoSize = False
        Me.txtFromDate.Enabled = False
        Me.txtFromDate.ForeColor = System.Drawing.Color.Blue
        Me.txtFromDate.Size = New System.Drawing.Size(101, 19)
        Me.txtFromDate.Location = New System.Drawing.Point(114, 38)
        Me.txtFromDate.TabIndex = 11
        Me.txtFromDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDate.AcceptsReturn = True
        Me.txtFromDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtFromDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtFromDate.CausesValidation = True
        Me.txtFromDate.HideSelection = True
        Me.txtFromDate.ReadOnly = False
        Me.txtFromDate.MaxLength = 0
        Me.txtFromDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFromDate.Multiline = False
        Me.txtFromDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFromDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtFromDate.TabStop = True
        Me.txtFromDate.Visible = True
        Me.txtFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtConsumption.AutoSize = False
        Me.txtConsumption.Enabled = False
        Me.txtConsumption.ForeColor = System.Drawing.Color.Blue
        Me.txtConsumption.Size = New System.Drawing.Size(101, 19)
        Me.txtConsumption.Location = New System.Drawing.Point(518, 12)
        Me.txtConsumption.TabIndex = 10
        Me.txtConsumption.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConsumption.AcceptsReturn = True
        Me.txtConsumption.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtConsumption.BackColor = System.Drawing.SystemColors.Window
        Me.txtConsumption.CausesValidation = True
        Me.txtConsumption.HideSelection = True
        Me.txtConsumption.ReadOnly = False
        Me.txtConsumption.MaxLength = 0
        Me.txtConsumption.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtConsumption.Multiline = False
        Me.txtConsumption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtConsumption.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtConsumption.TabStop = True
        Me.txtConsumption.Visible = True
        Me.txtConsumption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtConsumption.Name = "txtConsumption"
        Me.txtItemName.AutoSize = False
        Me.txtItemName.Enabled = False
        Me.txtItemName.ForeColor = System.Drawing.Color.Blue
        Me.txtItemName.Size = New System.Drawing.Size(317, 19)
        Me.txtItemName.Location = New System.Drawing.Point(114, 12)
        Me.txtItemName.TabIndex = 9
        Me.txtItemName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemName.AcceptsReturn = True
        Me.txtItemName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtItemName.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemName.CausesValidation = True
        Me.txtItemName.HideSelection = True
        Me.txtItemName.ReadOnly = False
        Me.txtItemName.MaxLength = 0
        Me.txtItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemName.Multiline = False
        Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemName.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtItemName.TabStop = True
        Me.txtItemName.Visible = True
        Me.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemName.Name = "txtItemName"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label4.Text = "To Date :"
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Size = New System.Drawing.Size(107, 11)
        Me.Label4.Location = New System.Drawing.Point(408, 40)
        Me.Label4.TabIndex = 15
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Enabled = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.UseMnemonic = True
        Me.Label4.Visible = True
        Me.Label4.AutoSize = False
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label4.Name = "Label4"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label3.Text = "From Date :"
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Size = New System.Drawing.Size(107, 11)
        Me.Label3.Location = New System.Drawing.Point(4, 40)
        Me.Label3.TabIndex = 8
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Enabled = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.UseMnemonic = True
        Me.Label3.Visible = True
        Me.Label3.AutoSize = False
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label3.Name = "Label3"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label2.Text = "Consumption :"
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Size = New System.Drawing.Size(117, 15)
        Me.Label2.Location = New System.Drawing.Point(398, 14)
        Me.Label2.TabIndex = 7
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Enabled = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.UseMnemonic = True
        Me.Label2.Visible = True
        Me.Label2.AutoSize = False
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label2.Name = "Label2"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label1.Text = "Item Description :"
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Size = New System.Drawing.Size(107, 13)
        Me.Label1.Location = New System.Drawing.Point(4, 14)
        Me.Label1.TabIndex = 6
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Enabled = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.UseMnemonic = True
        Me.Label1.Visible = True
        Me.Label1.AutoSize = False
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label1.Name = "Label1"
        SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(623, 347)
        Me.SprdMain.Location = New System.Drawing.Point(0, 60)
        Me.SprdMain.TabIndex = 0
        Me.SprdMain.Name = "SprdMain"
        Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Location = New System.Drawing.Point(84, 138)
        Me.Report1.Name = "Report1"
        Me.Frame2.Size = New System.Drawing.Size(623, 51)
        Me.Frame2.Location = New System.Drawing.Point(0, 407)
        Me.Frame2.TabIndex = 4
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Enabled = True
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Visible = True
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.Name = "Frame2"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.CmdPreview.Location = New System.Drawing.Point(64, 10)
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.CausesValidation = True
        Me.CmdPreview.Enabled = True
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.TabStop = True
        Me.CmdPreview.Name = "CmdPreview"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.Location = New System.Drawing.Point(4, 10)
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.CausesValidation = True
        Me.cmdPrint.Enabled = True
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.TabStop = True
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdExit.Text = "&Close"
        Me.cmdExit.Size = New System.Drawing.Size(60, 37)
        Me.cmdExit.Location = New System.Drawing.Point(560, 10)
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmdExit, "Close the Form")
        Me.cmdExit.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.CausesValidation = True
        Me.cmdExit.Enabled = True
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.TabStop = True
        Me.cmdExit.Name = "cmdExit"
        Me.lblItemUOM.Text = "lblItemUOM"
        Me.lblItemUOM.Size = New System.Drawing.Size(55, 13)
        Me.lblItemUOM.Location = New System.Drawing.Point(192, 34)
        Me.lblItemUOM.TabIndex = 13
        Me.lblItemUOM.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemUOM.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblItemUOM.BackColor = System.Drawing.SystemColors.Control
        Me.lblItemUOM.Enabled = True
        Me.lblItemUOM.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblItemUOM.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblItemUOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblItemUOM.UseMnemonic = True
        Me.lblItemUOM.Visible = True
        Me.lblItemUOM.AutoSize = True
        Me.lblItemUOM.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblItemUOM.Name = "lblItemUOM"
        Me.lblItemCode.Text = "lblItemCode"
        Me.lblItemCode.Size = New System.Drawing.Size(55, 13)
        Me.lblItemCode.Location = New System.Drawing.Point(192, 22)
        Me.lblItemCode.TabIndex = 12
        Me.lblItemCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCode.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblItemCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblItemCode.Enabled = True
        Me.lblItemCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblItemCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblItemCode.UseMnemonic = True
        Me.lblItemCode.Visible = True
        Me.lblItemCode.AutoSize = True
        Me.lblItemCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblItemCode.Name = "lblItemCode"
        Me.AData1.Size = New System.Drawing.Size(113, 28)
        Me.AData1.Location = New System.Drawing.Point(134, 202)
        Me.AData1.Visible = 0
        Me.AData1.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Me.AData1.ConnectionTimeout = 15
        Me.AData1.CommandTimeout = 30
        Me.AData1.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.AData1.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.AData1.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.AData1.CacheSize = 50
        Me.AData1.MaxRecords = 0
        Me.AData1.BOFAction = VB6.ADODC.BOFActionEnum.adDoMoveFirst
        Me.AData1.EOFAction = VB6.ADODC.EOFActionEnum.adDoMoveLast
        Me.AData1.BackColor = System.Drawing.SystemColors.Window
        Me.AData1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.AData1.Orientation = VB6.ADODC.OrientationEnum.adHorizontal
        Me.AData1.Enabled = True
        Me.AData1.UserName = ""
        Me.AData1.RecordSource = ""
        Me.AData1.Text = "Adodc1"
        Me.AData1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AData1.ConnectionString = ""
        Me.AData1.Name = "AData1"
        Me.Controls.Add(Frame3)
        Me.Controls.Add(SprdMain)
        Me.Controls.Add(Report1)
        Me.Controls.Add(Frame2)
        Me.Controls.Add(AData1)
        Me.Frame3.Controls.Add(txtToDate)
        Me.Frame3.Controls.Add(txtFromDate)
        Me.Frame3.Controls.Add(txtConsumption)
        Me.Frame3.Controls.Add(txtItemName)
        Me.Frame3.Controls.Add(Label4)
        Me.Frame3.Controls.Add(Label3)
        Me.Frame3.Controls.Add(Label2)
        Me.Frame3.Controls.Add(Label1)
        Me.Frame2.Controls.Add(CmdPreview)
        Me.Frame2.Controls.Add(cmdPrint)
        Me.Frame2.Controls.Add(cmdExit)
        Me.Frame2.Controls.Add(lblItemUOM)
        Me.Frame2.Controls.Add(lblItemCode)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
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
#End Region
End Class