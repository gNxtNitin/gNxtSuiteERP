Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub


    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblCompanyName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblFYear = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Conn = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Caps = New System.Windows.Forms.ToolStripStatusLabel()
        Me.num = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ins = New System.Windows.Forms.ToolStripStatusLabel()
        Me.RunDate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.UserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasswordChangeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WindowsMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.CascadeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogoutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.naviBar1 = New Guifreaks.NavigationBar.NaviBar(Me.components)
        Me.UltraTabbedMdiManager1 = New Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager(Me.components)
        Me.StatusStrip.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.naviBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabbedMdiManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Status, Me.lblCompanyName, Me.lblFYear, Me.Conn, Me.Caps, Me.num, Me.ins, Me.RunDate})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 429)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(632, 24)
        Me.StatusStrip.TabIndex = 7
        Me.StatusStrip.Text = "StatusStrip"
        '
        'Status
        '
        Me.Status.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Status.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(43, 19)
        Me.Status.Text = "Status"
        '
        'lblCompanyName
        '
        Me.lblCompanyName.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblCompanyName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.lblCompanyName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(338, 19)
        Me.lblCompanyName.Spring = True
        Me.lblCompanyName.Text = "CompanyName"
        '
        'lblFYear
        '
        Me.lblFYear.BackColor = System.Drawing.Color.Lime
        Me.lblFYear.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblFYear.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.lblFYear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.lblFYear.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFYear.Name = "lblFYear"
        Me.lblFYear.Size = New System.Drawing.Size(41, 19)
        Me.lblFYear.Text = "FYear"
        '
        'Conn
        '
        Me.Conn.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Conn.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.Conn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Conn.Name = "Conn"
        Me.Conn.Size = New System.Drawing.Size(40, 19)
        Me.Conn.Text = "Conn"
        '
        'Caps
        '
        Me.Caps.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Caps.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.Caps.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Caps.Name = "Caps"
        Me.Caps.Size = New System.Drawing.Size(37, 19)
        Me.Caps.Text = "Caps"
        '
        'num
        '
        Me.num.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.num.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.num.Name = "num"
        Me.num.Size = New System.Drawing.Size(36, 19)
        Me.num.Text = "num"
        '
        'ins
        '
        Me.ins.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ins.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ins.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ins.Name = "ins"
        Me.ins.Size = New System.Drawing.Size(26, 19)
        Me.ins.Text = "ins"
        '
        'RunDate
        '
        Me.RunDate.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.RunDate.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.RunDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.RunDate.Name = "RunDate"
        Me.RunDate.Size = New System.Drawing.Size(56, 19)
        Me.RunDate.Text = "RunDate"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserToolStripMenuItem, Me.WindowsMenu})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.MdiWindowListItem = Me.WindowsMenu
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(632, 24)
        Me.MenuStrip1.TabIndex = 15
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'UserToolStripMenuItem
        '
        Me.UserToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PasswordChangeToolStripMenuItem})
        Me.UserToolStripMenuItem.Name = "UserToolStripMenuItem"
        Me.UserToolStripMenuItem.Size = New System.Drawing.Size(42, 20)
        Me.UserToolStripMenuItem.Text = "User"
        '
        'PasswordChangeToolStripMenuItem
        '
        Me.PasswordChangeToolStripMenuItem.Name = "PasswordChangeToolStripMenuItem"
        Me.PasswordChangeToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.PasswordChangeToolStripMenuItem.Text = "Password Change"
        '
        'WindowsMenu
        '
        Me.WindowsMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CascadeToolStripMenuItem, Me.CloseAllToolStripMenuItem, Me.LogoutToolStripMenuItem})
        Me.WindowsMenu.Name = "WindowsMenu"
        Me.WindowsMenu.Size = New System.Drawing.Size(68, 20)
        Me.WindowsMenu.Text = "&Windows"
        '
        'CascadeToolStripMenuItem
        '
        Me.CascadeToolStripMenuItem.Enabled = False
        Me.CascadeToolStripMenuItem.Name = "CascadeToolStripMenuItem"
        Me.CascadeToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.CascadeToolStripMenuItem.Text = "&Cascade"
        '
        'CloseAllToolStripMenuItem
        '
        Me.CloseAllToolStripMenuItem.Name = "CloseAllToolStripMenuItem"
        Me.CloseAllToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.CloseAllToolStripMenuItem.Text = "C&lose All"
        '
        'LogoutToolStripMenuItem
        '
        Me.LogoutToolStripMenuItem.Name = "LogoutToolStripMenuItem"
        Me.LogoutToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.LogoutToolStripMenuItem.Text = "Logout"
        '
        'naviBar1
        '
        Me.naviBar1.ActiveBand = Nothing
        Me.naviBar1.BackColor = System.Drawing.SystemColors.Control
        Me.naviBar1.Dock = System.Windows.Forms.DockStyle.Left
        Me.naviBar1.LayoutStyle = Guifreaks.NavigationBar.NaviLayoutStyle.Office2007Silver
        Me.naviBar1.Location = New System.Drawing.Point(0, 24)
        Me.naviBar1.Name = "naviBar1"
        Me.naviBar1.Size = New System.Drawing.Size(250, 405)
        Me.naviBar1.TabIndex = 17
        Me.naviBar1.Text = "naviBar1"
        '
        'UltraTabbedMdiManager1
        '
        Me.UltraTabbedMdiManager1.AllowHorizontalTabGroups = False
        Me.UltraTabbedMdiManager1.MdiParent = Me
        Me.UltraTabbedMdiManager1.TabGroupSettings.CloseButtonLocation = Infragistics.Win.UltraWinTabs.TabCloseButtonLocation.Tab
        Me.UltraTabbedMdiManager1.TabSettings.CloseButtonAlignment = Infragistics.Win.UltraWinTabs.TabCloseButtonAlignment.AfterContent
        Me.UltraTabbedMdiManager1.TabSettings.CloseButtonVisibility = Infragistics.Win.UltraWinTabs.TabCloseButtonVisibility.WhenSelected
        Me.UltraTabbedMdiManager1.ViewStyle = Infragistics.Win.UltraWinTabbedMdi.ViewStyle.Office2007
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(632, 453)
        Me.Controls.Add(Me.naviBar1)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FormMain"
        Me.Text = "ERP"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.naviBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabbedMdiManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents WindowsMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CascadeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogoutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents naviBar1 As Guifreaks.NavigationBar.NaviBar
    Friend WithEvents Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblCompanyName As ToolStripStatusLabel
    Friend WithEvents Conn As ToolStripStatusLabel
    Friend WithEvents Caps As ToolStripStatusLabel
    Friend WithEvents num As ToolStripStatusLabel
    Friend WithEvents ins As ToolStripStatusLabel
    Friend WithEvents RunDate As ToolStripStatusLabel
    Friend WithEvents UltraTabbedMdiManager1 As Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager
    Friend WithEvents UserToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PasswordChangeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents lblFYear As ToolStripStatusLabel
End Class
