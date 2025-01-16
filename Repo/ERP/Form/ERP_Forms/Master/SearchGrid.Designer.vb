Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSearchGrid
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
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents Text1 As System.Windows.Forms.TextBox
    Public WithEvents _optOrderType_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optOrderType_0 As System.Windows.Forms.RadioButton
    Public WithEvents lblFieldType As System.Windows.Forms.Label
    Public WithEvents lblItemCol As System.Windows.Forms.Label
    Public WithEvents lblStockShow As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdSelect As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents lblName As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents optOrderType As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSearchGrid))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Text1 = New System.Windows.Forms.TextBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.lblGroupBy = New System.Windows.Forms.Label()
        Me.lblQuery = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optOrderType_1 = New System.Windows.Forms.RadioButton()
        Me._optOrderType_0 = New System.Windows.Forms.RadioButton()
        Me.lblFieldType = New System.Windows.Forms.Label()
        Me.lblItemCol = New System.Windows.Forms.Label()
        Me.lblStockShow = New System.Windows.Forms.Label()
        Me.cmdSelect = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.lblName = New System.Windows.Forms.Label()
        Me.optOrderType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.cmdSearchAnyWhere = New System.Windows.Forms.Button()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.optOrderType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 33)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(662, 348)
        Me.SprdView.TabIndex = 1
        '
        'Text1
        '
        Me.Text1.AcceptsReturn = True
        Me.Text1.BackColor = System.Drawing.SystemColors.Window
        Me.Text1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Text1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Text1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Text1.ForeColor = System.Drawing.Color.Blue
        Me.Text1.Location = New System.Drawing.Point(0, 0)
        Me.Text1.MaxLength = 0
        Me.Text1.Multiline = True
        Me.Text1.Name = "Text1"
        Me.Text1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text1.Size = New System.Drawing.Size(660, 27)
        Me.Text1.TabIndex = 0
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.lblGroupBy)
        Me.Frame2.Controls.Add(Me.lblQuery)
        Me.Frame2.Controls.Add(Me.Frame1)
        Me.Frame2.Controls.Add(Me.cmdSelect)
        Me.Frame2.Controls.Add(Me.cmdCancel)
        Me.Frame2.Controls.Add(Me.lblName)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 379)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(662, 43)
        Me.Frame2.TabIndex = 4
        Me.Frame2.TabStop = False
        '
        'lblGroupBy
        '
        Me.lblGroupBy.AutoSize = True
        Me.lblGroupBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblGroupBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblGroupBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGroupBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblGroupBy.Location = New System.Drawing.Point(286, 14)
        Me.lblGroupBy.Name = "lblGroupBy"
        Me.lblGroupBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblGroupBy.Size = New System.Drawing.Size(33, 13)
        Me.lblGroupBy.TabIndex = 12
        Me.lblGroupBy.Text = "False"
        Me.lblGroupBy.Visible = False
        '
        'lblQuery
        '
        Me.lblQuery.AutoSize = True
        Me.lblQuery.BackColor = System.Drawing.SystemColors.Control
        Me.lblQuery.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblQuery.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQuery.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblQuery.Location = New System.Drawing.Point(336, 16)
        Me.lblQuery.Name = "lblQuery"
        Me.lblQuery.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblQuery.Size = New System.Drawing.Size(50, 13)
        Me.lblQuery.TabIndex = 11
        Me.lblQuery.Text = "lblQuery"
        Me.lblQuery.Visible = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optOrderType_1)
        Me.Frame1.Controls.Add(Me._optOrderType_0)
        Me.Frame1.Controls.Add(Me.lblFieldType)
        Me.Frame1.Controls.Add(Me.lblItemCol)
        Me.Frame1.Controls.Add(Me.lblStockShow)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(74, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(227, 43)
        Me.Frame1.TabIndex = 6
        Me.Frame1.TabStop = False
        Me.Frame1.Visible = False
        '
        '_optOrderType_1
        '
        Me._optOrderType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderType_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderType.SetIndex(Me._optOrderType_1, CType(1, Short))
        Me._optOrderType_1.Location = New System.Drawing.Point(116, 16)
        Me._optOrderType_1.Name = "_optOrderType_1"
        Me._optOrderType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderType_1.Size = New System.Drawing.Size(109, 22)
        Me._optOrderType_1.TabIndex = 8
        Me._optOrderType_1.TabStop = True
        Me._optOrderType_1.Text = "Whole Word"
        Me._optOrderType_1.UseVisualStyleBackColor = False
        '
        '_optOrderType_0
        '
        Me._optOrderType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderType_0.Checked = True
        Me._optOrderType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderType_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderType.SetIndex(Me._optOrderType_0, CType(0, Short))
        Me._optOrderType_0.Location = New System.Drawing.Point(6, 16)
        Me._optOrderType_0.Name = "_optOrderType_0"
        Me._optOrderType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderType_0.Size = New System.Drawing.Size(113, 22)
        Me._optOrderType_0.TabIndex = 7
        Me._optOrderType_0.TabStop = True
        Me._optOrderType_0.Text = "Alphabetically"
        Me._optOrderType_0.UseVisualStyleBackColor = False
        '
        'lblFieldType
        '
        Me.lblFieldType.BackColor = System.Drawing.SystemColors.Control
        Me.lblFieldType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFieldType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFieldType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFieldType.Location = New System.Drawing.Point(114, 34)
        Me.lblFieldType.Name = "lblFieldType"
        Me.lblFieldType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFieldType.Size = New System.Drawing.Size(39, 11)
        Me.lblFieldType.TabIndex = 11
        Me.lblFieldType.Text = "lblFieldType"
        '
        'lblItemCol
        '
        Me.lblItemCol.AutoSize = True
        Me.lblItemCol.BackColor = System.Drawing.SystemColors.Control
        Me.lblItemCol.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblItemCol.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCol.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblItemCol.Location = New System.Drawing.Point(190, 34)
        Me.lblItemCol.Name = "lblItemCol"
        Me.lblItemCol.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblItemCol.Size = New System.Drawing.Size(60, 13)
        Me.lblItemCol.TabIndex = 10
        Me.lblItemCol.Text = "lblItemCol"
        Me.lblItemCol.Visible = False
        '
        'lblStockShow
        '
        Me.lblStockShow.AutoSize = True
        Me.lblStockShow.BackColor = System.Drawing.SystemColors.Control
        Me.lblStockShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStockShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStockShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStockShow.Location = New System.Drawing.Point(214, 26)
        Me.lblStockShow.Name = "lblStockShow"
        Me.lblStockShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStockShow.Size = New System.Drawing.Size(75, 13)
        Me.lblStockShow.TabIndex = 9
        Me.lblStockShow.Text = "lblStockShow"
        Me.lblStockShow.Visible = False
        '
        'cmdSelect
        '
        Me.cmdSelect.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSelect.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSelect.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSelect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSelect.Location = New System.Drawing.Point(4, 10)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSelect.Size = New System.Drawing.Size(67, 29)
        Me.cmdSelect.TabIndex = 2
        Me.cmdSelect.Text = "&Select"
        Me.cmdSelect.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(580, 10)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(73, 29)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.SystemColors.Control
        Me.lblName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblName.Location = New System.Drawing.Point(82, 16)
        Me.lblName.Name = "lblName"
        Me.lblName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblName.Size = New System.Drawing.Size(2, 15)
        Me.lblName.TabIndex = 5
        Me.lblName.Visible = False
        '
        'optOrderType
        '
        '
        'cmdSearchAnyWhere
        '
        Me.cmdSearchAnyWhere.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSearchAnyWhere.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchAnyWhere.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchAnyWhere.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchAnyWhere.Location = New System.Drawing.Point(561, -1)
        Me.cmdSearchAnyWhere.Name = "cmdSearchAnyWhere"
        Me.cmdSearchAnyWhere.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAnyWhere.Size = New System.Drawing.Size(102, 23)
        Me.cmdSearchAnyWhere.TabIndex = 5
        Me.cmdSearchAnyWhere.Text = "Any Where"
        Me.cmdSearchAnyWhere.UseVisualStyleBackColor = False
        Me.cmdSearchAnyWhere.Visible = False
        '
        'frmSearchGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(663, 423)
        Me.ControlBox = False
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Text1)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.cmdSearchAnyWhere)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSearchGrid"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Search"
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.optOrderType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
        'SprdViewHdr.DataSource = Nothing
    End Sub

    Public WithEvents cmdSearchAnyWhere As Button
    Public WithEvents lblQuery As Label
    Public WithEvents lblGroupBy As Label
#End Region
End Class