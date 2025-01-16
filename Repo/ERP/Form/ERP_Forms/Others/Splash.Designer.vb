Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSplash
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
    Public WithEvents imgHema As System.Windows.Forms.PictureBox
    Public WithEvents lblVersion As System.Windows.Forms.Label
    Public WithEvents lblLic As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    'Public WithEvents Line1 As Microsoft.VisualBasic.PowerPacks.LineShape
    Public WithEvents lblCopyRight As System.Windows.Forms.Label
    'Public WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSplash))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblLic = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblCopyRight = New System.Windows.Forms.Label()
        Me.imgHema = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.imgHema, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblVersion
        '
        Me.lblVersion.BackColor = System.Drawing.SystemColors.Menu
        Me.lblVersion.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVersion.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblVersion.Location = New System.Drawing.Point(894, 703)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVersion.Size = New System.Drawing.Size(119, 13)
        Me.lblVersion.TabIndex = 4
        Me.lblVersion.Text = "Version 1.0.1"
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLic
        '
        Me.lblLic.AutoSize = True
        Me.lblLic.BackColor = System.Drawing.SystemColors.Menu
        Me.lblLic.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLic.Font = New System.Drawing.Font("Segoe UI Semibold", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLic.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLic.Location = New System.Drawing.Point(1199, 754)
        Me.lblLic.Name = "lblLic"
        Me.lblLic.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLic.Size = New System.Drawing.Size(132, 23)
        Me.lblLic.TabIndex = 3
        Me.lblLic.Text = "M/s Licensed To"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Menu
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(1201, 737)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(89, 17)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Licensed To :"
        '
        'lblCopyRight
        '
        Me.lblCopyRight.AutoSize = True
        Me.lblCopyRight.BackColor = System.Drawing.SystemColors.Menu
        Me.lblCopyRight.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCopyRight.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyRight.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCopyRight.Location = New System.Drawing.Point(872, 683)
        Me.lblCopyRight.Name = "lblCopyRight"
        Me.lblCopyRight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCopyRight.Size = New System.Drawing.Size(141, 20)
        Me.lblCopyRight.TabIndex = 1
        Me.lblCopyRight.Text = "(c) Copyrights 2024"
        '
        'imgHema
        '
        Me.imgHema.BackColor = System.Drawing.Color.Transparent
        Me.imgHema.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.imgHema.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgHema.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.imgHema.ForeColor = System.Drawing.SystemColors.WindowText
        Me.imgHema.Image = CType(resources.GetObject("imgHema.Image"), System.Drawing.Image)
        Me.imgHema.Location = New System.Drawing.Point(535, 103)
        Me.imgHema.Name = "imgHema"
        Me.imgHema.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.imgHema.Size = New System.Drawing.Size(478, 478)
        Me.imgHema.TabIndex = 6
        Me.imgHema.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(531, 601)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(179, 21)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Company Address line1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1199, 777)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(217, 21)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "ClientCompanyAddress line1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(531, 622)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(187, 21)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Company Address LIne2"
        Me.Label3.UseMnemonic = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1200, 798)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(220, 21)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "ClientCompanyAddress line2"
        '
        'frmSplash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Menu
        Me.ClientSize = New System.Drawing.Size(1559, 841)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.imgHema)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblLic)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblCopyRight)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSplash"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.imgHema, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
#End Region
End Class