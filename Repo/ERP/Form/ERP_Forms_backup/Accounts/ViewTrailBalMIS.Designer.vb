Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmViewTrailBalMIS
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
    Public WithEvents _txtDate_1 As System.Windows.Forms.MaskedTextBox
    Public WithEvents _txtDate_0 As System.Windows.Forms.MaskedTextBox
    Public WithEvents _txtDate1_0 As System.Windows.Forms.TextBox
    Public WithEvents _txtDate1_1 As System.Windows.Forms.TextBox
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
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
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents txtDate As Microsoft.VisualBasic.Compatibility.VB6.MaskedTextBoxArray
    Public WithEvents txtDate1 As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewTrailBalMIS))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.FraHideRow = New System.Windows.Forms.GroupBox()
        Me.ChkHideZeroBal = New System.Windows.Forms.CheckBox()
        Me.ChkHideZeroTrans = New System.Windows.Forms.CheckBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me._txtDate_1 = New System.Windows.Forms.MaskedTextBox()
        Me._txtDate_0 = New System.Windows.Forms.MaskedTextBox()
        Me._txtDate1_0 = New System.Windows.Forms.TextBox()
        Me._txtDate1_1 = New System.Windows.Forms.TextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.CMDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CMDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CMDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CMDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CMDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.txtDate = New Microsoft.VisualBasic.Compatibility.VB6.MaskedTextBoxArray(Me.components)
        Me.txtDate1 = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.FraHideRow.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.CmdPreview.TabIndex = 6
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
        Me.cmdPrint.TabIndex = 5
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
        Me.cmdExit.TabIndex = 7
        Me.cmdExit.Text = "&Close"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdExit, "Close the Form")
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdShow.Location = New System.Drawing.Point(4, 9)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(67, 37)
        Me.cmdShow.TabIndex = 4
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
        Me.FraHideRow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraHideRow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraHideRow.Location = New System.Drawing.Point(741, 0)
        Me.FraHideRow.Name = "FraHideRow"
        Me.FraHideRow.Padding = New System.Windows.Forms.Padding(0)
        Me.FraHideRow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraHideRow.Size = New System.Drawing.Size(157, 57)
        Me.FraHideRow.TabIndex = 11
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
        Me.ChkHideZeroBal.TabIndex = 12
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
        Me.ChkHideZeroTrans.TabIndex = 13
        Me.ChkHideZeroTrans.Text = "Without Transactions"
        Me.ChkHideZeroTrans.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me._txtDate_1)
        Me.Frame4.Controls.Add(Me._txtDate_0)
        Me.Frame4.Controls.Add(Me._txtDate1_0)
        Me.Frame4.Controls.Add(Me._txtDate1_1)
        Me.Frame4.Controls.Add(Me._Lbl_1)
        Me.Frame4.Controls.Add(Me._Lbl_0)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, -2)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(121, 57)
        Me.Frame4.TabIndex = 8
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Date"
        '
        '_txtDate_1
        '
        Me._txtDate_1.AllowPromptAsInput = False
        Me._txtDate_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.SetIndex(Me._txtDate_1, CType(1, Short))
        Me._txtDate_1.Location = New System.Drawing.Point(36, 32)
        Me._txtDate_1.Mask = "##/##/####"
        Me._txtDate_1.Name = "_txtDate_1"
        Me._txtDate_1.Size = New System.Drawing.Size(81, 20)
        Me._txtDate_1.TabIndex = 1
        '
        '_txtDate_0
        '
        Me._txtDate_0.AllowPromptAsInput = False
        Me._txtDate_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.SetIndex(Me._txtDate_0, CType(0, Short))
        Me._txtDate_0.Location = New System.Drawing.Point(36, 12)
        Me._txtDate_0.Mask = "##/##/####"
        Me._txtDate_0.Name = "_txtDate_0"
        Me._txtDate_0.Size = New System.Drawing.Size(81, 20)
        Me._txtDate_0.TabIndex = 0
        '
        '_txtDate1_0
        '
        Me._txtDate1_0.AcceptsReturn = True
        Me._txtDate1_0.BackColor = System.Drawing.SystemColors.Window
        Me._txtDate1_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._txtDate1_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtDate1_0.Enabled = False
        Me._txtDate1_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtDate1_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDate1.SetIndex(Me._txtDate1_0, CType(0, Short))
        Me._txtDate1_0.Location = New System.Drawing.Point(36, 12)
        Me._txtDate1_0.MaxLength = 0
        Me._txtDate1_0.Name = "_txtDate1_0"
        Me._txtDate1_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtDate1_0.Size = New System.Drawing.Size(77, 20)
        Me._txtDate1_0.TabIndex = 2
        Me._txtDate1_0.Visible = False
        '
        '_txtDate1_1
        '
        Me._txtDate1_1.AcceptsReturn = True
        Me._txtDate1_1.BackColor = System.Drawing.SystemColors.Window
        Me._txtDate1_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._txtDate1_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtDate1_1.Enabled = False
        Me._txtDate1_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtDate1_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDate1.SetIndex(Me._txtDate1_1, CType(1, Short))
        Me._txtDate1_1.Location = New System.Drawing.Point(36, 33)
        Me._txtDate1_1.MaxLength = 0
        Me._txtDate1_1.Name = "_txtDate1_1"
        Me._txtDate1_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtDate1_1.Size = New System.Drawing.Size(77, 20)
        Me._txtDate1_1.TabIndex = 3
        Me._txtDate1_1.Visible = False
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(10, 36)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(18, 14)
        Me._Lbl_1.TabIndex = 10
        Me._Lbl_1.Text = "To"
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(4, 13)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(31, 14)
        Me._Lbl_0.TabIndex = 9
        Me._Lbl_0.Text = "From"
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(84, 138)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 12
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 56)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(900, 512)
        Me.SprdMain.TabIndex = 15
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
        Me.FraMovement.Location = New System.Drawing.Point(622, 564)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(277, 49)
        Me.FraMovement.TabIndex = 14
        Me.FraMovement.TabStop = False
        '
        'txtDate
        '
        '
        'frmViewTrailBalMIS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.FraHideRow)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.SprdMain)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(5, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmViewTrailBalMIS"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.FraHideRow.ResumeLayout(False)
        Me.FraHideRow.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate1, System.ComponentModel.ISupportInitialize).EndInit()
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