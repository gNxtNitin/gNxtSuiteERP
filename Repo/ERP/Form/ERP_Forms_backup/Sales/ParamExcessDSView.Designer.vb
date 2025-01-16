Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamExcessDSView
#Region "Windows Form Designer generated code "
    <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
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
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents fraAccounts As System.Windows.Forms.GroupBox
    Public WithEvents ADataGrid As VB6.ADODC
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents lblDate As System.Windows.Forms.Label
    Public WithEvents lblCustomerCode As System.Windows.Forms.Label
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public CommonDialogOpen As System.Windows.Forms.OpenFileDialog
    Public CommonDialogSave As System.Windows.Forms.SaveFileDialog
    Public CommonDialogFont As System.Windows.Forms.FontDialog
    Public CommonDialogColor As System.Windows.Forms.ColorDialog
    Public CommonDialogPrint As System.Windows.Forms.PrintDialog
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmParamExcessDSView))
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.fraAccounts = New System.Windows.Forms.GroupBox
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread
        Me.ADataGrid = New VB6.ADODC
        Me.Report1 = New AxCrystal.AxCrystalReport
        Me.FraMovement = New System.Windows.Forms.GroupBox
        Me.CmdClose = New System.Windows.Forms.Button
        Me.lblDate = New System.Windows.Forms.Label
        Me.lblCustomerCode = New System.Windows.Forms.Label
        Me.lblMkey = New System.Windows.Forms.Label
        Me.CommonDialogOpen = New System.Windows.Forms.OpenFileDialog
        Me.CommonDialogSave = New System.Windows.Forms.SaveFileDialog
        Me.CommonDialogFont = New System.Windows.Forms.FontDialog
        Me.CommonDialogColor = New System.Windows.Forms.ColorDialog
        Me.CommonDialogPrint = New System.Windows.Forms.PrintDialog
        Me.fraAccounts.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        Me.ToolTip1.Active = True
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Text = "Excess Delivery Schedule Approval - View"
        Me.ClientSize = New System.Drawing.Size(710, 457)
        Me.Location = New System.Drawing.Point(-242, 29)
        Me.Icon = CType(resources.GetObject("frmParamExcessDSView.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ControlBox = True
        Me.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = True
        Me.HelpButton = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Normal
        Me.Name = "frmParamExcessDSView"
        Me.fraAccounts.Size = New System.Drawing.Size(709, 411)
        Me.fraAccounts.Location = New System.Drawing.Point(0, 0)
        Me.fraAccounts.TabIndex = 3
        Me.fraAccounts.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAccounts.BackColor = System.Drawing.SystemColors.Control
        Me.fraAccounts.Enabled = True
        Me.fraAccounts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraAccounts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraAccounts.Visible = True
        Me.fraAccounts.Padding = New System.Windows.Forms.Padding(0)
        Me.fraAccounts.Name = "fraAccounts"
        SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(706, 403)
        Me.SprdMain.Location = New System.Drawing.Point(0, 6)
        Me.SprdMain.TabIndex = 6
        Me.SprdMain.Name = "SprdMain"
        Me.ADataGrid.Size = New System.Drawing.Size(113, 23)
        Me.ADataGrid.Location = New System.Drawing.Point(0, 56)
        Me.ADataGrid.Visible = 0
        Me.ADataGrid.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Me.ADataGrid.ConnectionTimeout = 15
        Me.ADataGrid.CommandTimeout = 30
        Me.ADataGrid.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.ADataGrid.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.ADataGrid.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.ADataGrid.CacheSize = 50
        Me.ADataGrid.MaxRecords = 0
        Me.ADataGrid.BOFAction = VB6.ADODC.BOFActionEnum.adDoMoveFirst
        Me.ADataGrid.EOFAction = VB6.ADODC.EOFActionEnum.adDoMoveLast
        Me.ADataGrid.BackColor = System.Drawing.SystemColors.Window
        Me.ADataGrid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ADataGrid.Orientation = VB6.ADODC.OrientationEnum.adHorizontal
        Me.ADataGrid.Enabled = True
        Me.ADataGrid.UserName = ""
        Me.ADataGrid.RecordSource = ""
        Me.ADataGrid.Text = "Adodc1"
        Me.ADataGrid.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADataGrid.ConnectionString = ""
        Me.ADataGrid.Name = "ADataGrid"
        Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.FraMovement.Size = New System.Drawing.Size(709, 51)
        Me.FraMovement.Location = New System.Drawing.Point(0, 406)
        Me.FraMovement.TabIndex = 1
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Enabled = True
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Visible = True
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.Name = "FraMovement"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.Location = New System.Drawing.Point(614, 11)
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.CausesValidation = True
        Me.CmdClose.Enabled = True
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.TabStop = True
        Me.CmdClose.Name = "CmdClose"
        Me.lblDate.Text = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(35, 19)
        Me.lblDate.Location = New System.Drawing.Point(282, 18)
        Me.lblDate.TabIndex = 5
        Me.lblDate.Visible = False
        Me.lblDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate.Enabled = True
        Me.lblDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate.UseMnemonic = True
        Me.lblDate.AutoSize = False
        Me.lblDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblDate.Name = "lblDate"
        Me.lblCustomerCode.Text = "lblCustomerCode"
        Me.lblCustomerCode.Size = New System.Drawing.Size(45, 15)
        Me.lblCustomerCode.Location = New System.Drawing.Point(84, 18)
        Me.lblCustomerCode.TabIndex = 4
        Me.lblCustomerCode.Visible = False
        Me.lblCustomerCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerCode.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblCustomerCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblCustomerCode.Enabled = True
        Me.lblCustomerCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCustomerCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCustomerCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCustomerCode.UseMnemonic = True
        Me.lblCustomerCode.AutoSize = False
        Me.lblCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblCustomerCode.Name = "lblCustomerCode"
        Me.lblMkey.Text = "lblMkey"
        Me.lblMkey.Size = New System.Drawing.Size(45, 17)
        Me.lblMkey.Location = New System.Drawing.Point(10, 18)
        Me.lblMkey.TabIndex = 2
        Me.lblMkey.Visible = False
        Me.lblMkey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Enabled = True
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.UseMnemonic = True
        Me.lblMkey.AutoSize = False
        Me.lblMkey.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblMkey.Name = "lblMkey"
        Me.Controls.Add(fraAccounts)
        Me.Controls.Add(ADataGrid)
        Me.Controls.Add(Report1)
        Me.Controls.Add(FraMovement)
        Me.fraAccounts.Controls.Add(SprdMain)
        Me.FraMovement.Controls.Add(CmdClose)
        Me.FraMovement.Controls.Add(lblDate)
        Me.FraMovement.Controls.Add(lblCustomerCode)
        Me.FraMovement.Controls.Add(lblMkey)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraAccounts.ResumeLayout(False)
        Me.FraMovement.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
#End Region
End Class