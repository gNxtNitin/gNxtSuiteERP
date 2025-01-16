Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmViewTrailBalMISAcct
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
        'Me.MDIParent = MIS.Master

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
    Public WithEvents ChkHideZeroBal As System.Windows.Forms.CheckBox
    Public WithEvents ChkHideZeroTrans As System.Windows.Forms.CheckBox
    Public WithEvents FraHideRow As System.Windows.Forms.GroupBox
    Public CMDialog1Open As System.Windows.Forms.OpenFileDialog
    Public CMDialog1Save As System.Windows.Forms.SaveFileDialog
    Public CMDialog1Font As System.Windows.Forms.FontDialog
    Public CMDialog1Color As System.Windows.Forms.ColorDialog
    Public CMDialog1Print As System.Windows.Forms.PrintDialog
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents lblAccountName As System.Windows.Forms.Label
    Public WithEvents lblGroupCode As System.Windows.Forms.Label
    Public WithEvents lblDateTo As System.Windows.Forms.Label
    Public WithEvents lblDateFrom As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewTrailBalMISAcct))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.FraHideRow = New System.Windows.Forms.GroupBox()
        Me.ChkHideZeroBal = New System.Windows.Forms.CheckBox()
        Me.ChkHideZeroTrans = New System.Windows.Forms.CheckBox()
        Me.CMDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CMDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CMDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CMDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CMDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblAccountName = New System.Windows.Forms.Label()
        Me.lblGroupCode = New System.Windows.Forms.Label()
        Me.lblDateTo = New System.Windows.Forms.Label()
        Me.lblDateFrom = New System.Windows.Forms.Label()
        Me.FraHideRow.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
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
        Me.CmdPreview.Location = New System.Drawing.Point(138, 9)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 2
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
        Me.cmdPrint.Location = New System.Drawing.Point(71, 9)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 1
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdExit.Location = New System.Drawing.Point(206, 9)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(67, 37)
        Me.cmdExit.TabIndex = 3
        Me.cmdExit.Text = "&Close"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdExit, "Close the Form")
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Enabled = False
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdShow.Location = New System.Drawing.Point(4, 9)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(67, 37)
        Me.cmdShow.TabIndex = 0
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'FraHideRow
        '
        Me.FraHideRow.BackColor = System.Drawing.SystemColors.Control
        Me.FraHideRow.Controls.Add(Me.ChkHideZeroBal)
        Me.FraHideRow.Controls.Add(Me.ChkHideZeroTrans)
        Me.FraHideRow.Enabled = False
        Me.FraHideRow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraHideRow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraHideRow.Location = New System.Drawing.Point(741, 0)
        Me.FraHideRow.Name = "FraHideRow"
        Me.FraHideRow.Padding = New System.Windows.Forms.Padding(0)
        Me.FraHideRow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraHideRow.Size = New System.Drawing.Size(157, 57)
        Me.FraHideRow.TabIndex = 4
        Me.FraHideRow.TabStop = False
        Me.FraHideRow.Text = "Hide"
        Me.FraHideRow.Visible = False
        '
        'ChkHideZeroBal
        '
        Me.ChkHideZeroBal.AutoSize = True
        Me.ChkHideZeroBal.BackColor = System.Drawing.SystemColors.Control
        Me.ChkHideZeroBal.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkHideZeroBal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkHideZeroBal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkHideZeroBal.Location = New System.Drawing.Point(8, 16)
        Me.ChkHideZeroBal.Name = "ChkHideZeroBal"
        Me.ChkHideZeroBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkHideZeroBal.Size = New System.Drawing.Size(91, 18)
        Me.ChkHideZeroBal.TabIndex = 5
        Me.ChkHideZeroBal.Text = "Zero Balance"
        Me.ChkHideZeroBal.UseVisualStyleBackColor = False
        '
        'ChkHideZeroTrans
        '
        Me.ChkHideZeroTrans.AutoSize = True
        Me.ChkHideZeroTrans.BackColor = System.Drawing.SystemColors.Control
        Me.ChkHideZeroTrans.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkHideZeroTrans.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkHideZeroTrans.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkHideZeroTrans.Location = New System.Drawing.Point(8, 36)
        Me.ChkHideZeroTrans.Name = "ChkHideZeroTrans"
        Me.ChkHideZeroTrans.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkHideZeroTrans.Size = New System.Drawing.Size(128, 18)
        Me.ChkHideZeroTrans.TabIndex = 6
        Me.ChkHideZeroTrans.Text = "Without Transactions"
        Me.ChkHideZeroTrans.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(84, 138)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 5
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 58)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(902, 508)
        Me.SprdMain.TabIndex = 8
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdExit)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(624, 562)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(277, 49)
        Me.FraMovement.TabIndex = 7
        Me.FraMovement.TabStop = False
        '
        'lblAccountName
        '
        Me.lblAccountName.BackColor = System.Drawing.SystemColors.Control
        Me.lblAccountName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAccountName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAccountName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountName.ForeColor = System.Drawing.Color.Black
        Me.lblAccountName.Location = New System.Drawing.Point(0, 28)
        Me.lblAccountName.Name = "lblAccountName"
        Me.lblAccountName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAccountName.Size = New System.Drawing.Size(349, 25)
        Me.lblAccountName.TabIndex = 12
        Me.lblAccountName.Text = "lblAccountName"
        '
        'lblGroupCode
        '
        Me.lblGroupCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblGroupCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblGroupCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGroupCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblGroupCode.Location = New System.Drawing.Point(482, 16)
        Me.lblGroupCode.Name = "lblGroupCode"
        Me.lblGroupCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblGroupCode.Size = New System.Drawing.Size(105, 25)
        Me.lblGroupCode.TabIndex = 11
        Me.lblGroupCode.Text = "lblGroupCode"
        Me.lblGroupCode.Visible = False
        '
        'lblDateTo
        '
        Me.lblDateTo.BackColor = System.Drawing.SystemColors.Control
        Me.lblDateTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDateTo.Location = New System.Drawing.Point(304, 10)
        Me.lblDateTo.Name = "lblDateTo"
        Me.lblDateTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDateTo.Size = New System.Drawing.Size(163, 21)
        Me.lblDateTo.TabIndex = 10
        Me.lblDateTo.Text = "lblDateTo"
        Me.lblDateTo.Visible = False
        '
        'lblDateFrom
        '
        Me.lblDateFrom.BackColor = System.Drawing.SystemColors.Control
        Me.lblDateFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDateFrom.Location = New System.Drawing.Point(142, 10)
        Me.lblDateFrom.Name = "lblDateFrom"
        Me.lblDateFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDateFrom.Size = New System.Drawing.Size(143, 17)
        Me.lblDateFrom.TabIndex = 9
        Me.lblDateFrom.Text = "lblDateFrom"
        Me.lblDateFrom.Visible = False
        '
        'frmViewTrailBalMISAcct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.FraHideRow)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.SprdMain)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.lblAccountName)
        Me.Controls.Add(Me.lblGroupCode)
        Me.Controls.Add(Me.lblDateTo)
        Me.Controls.Add(Me.lblDateFrom)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(5, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmViewTrailBalMISAcct"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.FraHideRow.ResumeLayout(False)
        Me.FraHideRow.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdMain.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdMain.DataSource = Nothing
    End Sub
#End Region
End Class