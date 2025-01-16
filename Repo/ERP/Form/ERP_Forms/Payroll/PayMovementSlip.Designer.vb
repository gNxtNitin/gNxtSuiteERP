Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPayMovementSlip
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
    Public WithEvents cboSHalf As System.Windows.Forms.ComboBox
    Public WithEvents cboFHalf As System.Windows.Forms.ComboBox
    Public WithEvents chkAgainstLeave As System.Windows.Forms.CheckBox
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtDistance As System.Windows.Forms.TextBox
    Public WithEvents chkHRApproval As System.Windows.Forms.CheckBox
    Public WithEvents cboVehicle As System.Windows.Forms.ComboBox
    Public WithEvents cboVisitedFrom As System.Windows.Forms.ComboBox
    Public WithEvents txtRefDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents cmdAthSearch As System.Windows.Forms.Button
    Public WithEvents txtAthCode As System.Windows.Forms.TextBox
    Public WithEvents TxtEmpName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents txtPlace As System.Windows.Forms.TextBox
    Public WithEvents txtDept As System.Windows.Forms.TextBox
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents txtRefNo As System.Windows.Forms.TextBox
    Public WithEvents _optMoveType_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optMoveType_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optMoveType_0 As System.Windows.Forms.RadioButton
    Public WithEvents optType As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents txtFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtTotalHrs As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtRefDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents lblMovementType As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents Fragridview As System.Windows.Forms.GroupBox
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents optMoveType As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPayMovementSlip))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdAthSearch = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtOTHr = New System.Windows.Forms.TextBox()
        Me.chkAgtOT = New System.Windows.Forms.CheckBox()
        Me.cboSHalf = New System.Windows.Forms.ComboBox()
        Me.cboFHalf = New System.Windows.Forms.ComboBox()
        Me.chkAgainstLeave = New System.Windows.Forms.CheckBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtDistance = New System.Windows.Forms.TextBox()
        Me.chkHRApproval = New System.Windows.Forms.CheckBox()
        Me.cboVehicle = New System.Windows.Forms.ComboBox()
        Me.cboVisitedFrom = New System.Windows.Forms.ComboBox()
        Me.txtRefDate = New System.Windows.Forms.MaskedTextBox()
        Me.txtAthCode = New System.Windows.Forms.TextBox()
        Me.TxtEmpName = New System.Windows.Forms.TextBox()
        Me.txtPlace = New System.Windows.Forms.TextBox()
        Me.txtDept = New System.Windows.Forms.TextBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me.optType = New System.Windows.Forms.GroupBox()
        Me._optMoveType_2 = New System.Windows.Forms.RadioButton()
        Me._optMoveType_1 = New System.Windows.Forms.RadioButton()
        Me._optMoveType_0 = New System.Windows.Forms.RadioButton()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.txtFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtTo = New System.Windows.Forms.MaskedTextBox()
        Me.txtTotalHrs = New System.Windows.Forms.MaskedTextBox()
        Me.txtRefDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.lblMovementType = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Fragridview = New System.Windows.Forms.GroupBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.optMoveType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.txtOTThisMonth = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.FraView.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.optType.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Fragridview.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.optMoveType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdAthSearch
        '
        Me.cmdAthSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAthSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAthSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAthSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAthSearch.Image = CType(resources.GetObject("cmdAthSearch.Image"), System.Drawing.Image)
        Me.cmdAthSearch.Location = New System.Drawing.Point(184, 206)
        Me.cmdAthSearch.Name = "cmdAthSearch"
        Me.cmdAthSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAthSearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdAthSearch.TabIndex = 15
        Me.cmdAthSearch.TabStop = False
        Me.cmdAthSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAthSearch, "Search")
        Me.cmdAthSearch.UseVisualStyleBackColor = False
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(184, 38)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearch.TabIndex = 4
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(184, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdSavePrint.TabIndex = 21
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Bill")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(364, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.CmdPreview.TabIndex = 24
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(484, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 37)
        Me.CmdClose.TabIndex = 26
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(304, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 23
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Close the Form")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(424, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(60, 37)
        Me.CmdView.TabIndex = 25
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "Close the Form")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(244, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(60, 37)
        Me.CmdDelete.TabIndex = 22
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(124, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 37)
        Me.CmdSave.TabIndex = 20
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(64, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(60, 37)
        Me.CmdModify.TabIndex = 19
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Refresh Record(s)")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(4, 12)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(60, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.FraView.Controls.Add(Me.Frame1)
        Me.FraView.Controls.Add(Me.txtDistance)
        Me.FraView.Controls.Add(Me.chkHRApproval)
        Me.FraView.Controls.Add(Me.cboVehicle)
        Me.FraView.Controls.Add(Me.cboVisitedFrom)
        Me.FraView.Controls.Add(Me.txtRefDate)
        Me.FraView.Controls.Add(Me.cmdAthSearch)
        Me.FraView.Controls.Add(Me.txtAthCode)
        Me.FraView.Controls.Add(Me.TxtEmpName)
        Me.FraView.Controls.Add(Me.cmdSearch)
        Me.FraView.Controls.Add(Me.txtPlace)
        Me.FraView.Controls.Add(Me.txtDept)
        Me.FraView.Controls.Add(Me.txtEmpCode)
        Me.FraView.Controls.Add(Me.txtRefNo)
        Me.FraView.Controls.Add(Me.optType)
        Me.FraView.Controls.Add(Me.Report1)
        Me.FraView.Controls.Add(Me.txtFrom)
        Me.FraView.Controls.Add(Me.txtTo)
        Me.FraView.Controls.Add(Me.txtTotalHrs)
        Me.FraView.Controls.Add(Me.txtRefDateTo)
        Me.FraView.Controls.Add(Me.lblMovementType)
        Me.FraView.Controls.Add(Me.Label14)
        Me.FraView.Controls.Add(Me.Label13)
        Me.FraView.Controls.Add(Me.lblBookType)
        Me.FraView.Controls.Add(Me.Label12)
        Me.FraView.Controls.Add(Me.Label11)
        Me.FraView.Controls.Add(Me.Label10)
        Me.FraView.Controls.Add(Me.Label9)
        Me.FraView.Controls.Add(Me.Label8)
        Me.FraView.Controls.Add(Me.Label7)
        Me.FraView.Controls.Add(Me.Label6)
        Me.FraView.Controls.Add(Me.Label5)
        Me.FraView.Controls.Add(Me.Label4)
        Me.FraView.Controls.Add(Me.Label3)
        Me.FraView.Controls.Add(Me.Label2)
        Me.FraView.Controls.Add(Me.Label1)
        Me.FraView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(1, -5)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(549, 332)
        Me.FraView.TabIndex = 27
        Me.FraView.TabStop = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Frame1.Controls.Add(Me.txtOTThisMonth)
        Me.Frame1.Controls.Add(Me.Label18)
        Me.Frame1.Controls.Add(Me.Label17)
        Me.Frame1.Controls.Add(Me.txtOTHr)
        Me.Frame1.Controls.Add(Me.chkAgtOT)
        Me.Frame1.Controls.Add(Me.cboSHalf)
        Me.Frame1.Controls.Add(Me.cboFHalf)
        Me.Frame1.Controls.Add(Me.chkAgainstLeave)
        Me.Frame1.Controls.Add(Me.Label16)
        Me.Frame1.Controls.Add(Me.Label15)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(218, 202)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(331, 130)
        Me.Frame1.TabIndex = 49
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Leave Details"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(198, 36)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(68, 14)
        Me.Label17.TabIndex = 57
        Me.Label17.Text = "OT (In Min)  :"
        '
        'txtOTHr
        '
        Me.txtOTHr.AcceptsReturn = True
        Me.txtOTHr.BackColor = System.Drawing.SystemColors.Window
        Me.txtOTHr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOTHr.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOTHr.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOTHr.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOTHr.Location = New System.Drawing.Point(269, 32)
        Me.txtOTHr.MaxLength = 0
        Me.txtOTHr.Name = "txtOTHr"
        Me.txtOTHr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOTHr.Size = New System.Drawing.Size(58, 20)
        Me.txtOTHr.TabIndex = 56
        Me.txtOTHr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkAgtOT
        '
        Me.chkAgtOT.AutoSize = True
        Me.chkAgtOT.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkAgtOT.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAgtOT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAgtOT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAgtOT.Location = New System.Drawing.Point(222, 14)
        Me.chkAgtOT.Name = "chkAgtOT"
        Me.chkAgtOT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAgtOT.Size = New System.Drawing.Size(80, 18)
        Me.chkAgtOT.TabIndex = 55
        Me.chkAgtOT.Text = "Against OT"
        Me.chkAgtOT.UseVisualStyleBackColor = False
        '
        'cboSHalf
        '
        Me.cboSHalf.BackColor = System.Drawing.SystemColors.Window
        Me.cboSHalf.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSHalf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSHalf.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSHalf.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboSHalf.Location = New System.Drawing.Point(83, 56)
        Me.cboSHalf.Name = "cboSHalf"
        Me.cboSHalf.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSHalf.Size = New System.Drawing.Size(105, 22)
        Me.cboSHalf.TabIndex = 53
        '
        'cboFHalf
        '
        Me.cboFHalf.BackColor = System.Drawing.SystemColors.Window
        Me.cboFHalf.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboFHalf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFHalf.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFHalf.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboFHalf.Location = New System.Drawing.Point(83, 32)
        Me.cboFHalf.Name = "cboFHalf"
        Me.cboFHalf.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboFHalf.Size = New System.Drawing.Size(105, 22)
        Me.cboFHalf.TabIndex = 51
        '
        'chkAgainstLeave
        '
        Me.chkAgainstLeave.AutoSize = True
        Me.chkAgainstLeave.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkAgainstLeave.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAgainstLeave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAgainstLeave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAgainstLeave.Location = New System.Drawing.Point(94, 14)
        Me.chkAgainstLeave.Name = "chkAgainstLeave"
        Me.chkAgainstLeave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAgainstLeave.Size = New System.Drawing.Size(96, 18)
        Me.chkAgainstLeave.TabIndex = 50
        Me.chkAgainstLeave.Text = "Against Leave"
        Me.chkAgainstLeave.UseVisualStyleBackColor = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(8, 60)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(72, 14)
        Me.Label16.TabIndex = 54
        Me.Label16.Text = "Second Half :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(8, 36)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(56, 14)
        Me.Label15.TabIndex = 52
        Me.Label15.Text = "First Half :"
        '
        'txtDistance
        '
        Me.txtDistance.AcceptsReturn = True
        Me.txtDistance.BackColor = System.Drawing.SystemColors.Window
        Me.txtDistance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDistance.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDistance.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistance.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDistance.Location = New System.Drawing.Point(502, 132)
        Me.txtDistance.MaxLength = 0
        Me.txtDistance.Name = "txtDistance"
        Me.txtDistance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDistance.Size = New System.Drawing.Size(36, 20)
        Me.txtDistance.TabIndex = 9
        Me.txtDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkHRApproval
        '
        Me.chkHRApproval.AutoSize = True
        Me.chkHRApproval.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkHRApproval.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHRApproval.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHRApproval.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHRApproval.Location = New System.Drawing.Point(90, 234)
        Me.chkHRApproval.Name = "chkHRApproval"
        Me.chkHRApproval.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHRApproval.Size = New System.Drawing.Size(86, 18)
        Me.chkHRApproval.TabIndex = 16
        Me.chkHRApproval.Text = "HR Approval"
        Me.chkHRApproval.UseVisualStyleBackColor = False
        '
        'cboVehicle
        '
        Me.cboVehicle.BackColor = System.Drawing.SystemColors.Window
        Me.cboVehicle.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboVehicle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVehicle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboVehicle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboVehicle.Location = New System.Drawing.Point(298, 130)
        Me.cboVehicle.Name = "cboVehicle"
        Me.cboVehicle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboVehicle.Size = New System.Drawing.Size(123, 22)
        Me.cboVehicle.TabIndex = 8
        '
        'cboVisitedFrom
        '
        Me.cboVisitedFrom.BackColor = System.Drawing.SystemColors.Window
        Me.cboVisitedFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboVisitedFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVisitedFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboVisitedFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboVisitedFrom.Location = New System.Drawing.Point(90, 130)
        Me.cboVisitedFrom.Name = "cboVisitedFrom"
        Me.cboVisitedFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboVisitedFrom.Size = New System.Drawing.Size(105, 22)
        Me.cboVisitedFrom.TabIndex = 7
        '
        'txtRefDate
        '
        Me.txtRefDate.AllowPromptAsInput = False
        Me.txtRefDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefDate.Location = New System.Drawing.Point(282, 14)
        Me.txtRefDate.Mask = "##/##/####"
        Me.txtRefDate.Name = "txtRefDate"
        Me.txtRefDate.Size = New System.Drawing.Size(84, 20)
        Me.txtRefDate.TabIndex = 2
        '
        'txtAthCode
        '
        Me.txtAthCode.AcceptsReturn = True
        Me.txtAthCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtAthCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAthCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAthCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAthCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAthCode.Location = New System.Drawing.Point(90, 206)
        Me.txtAthCode.MaxLength = 0
        Me.txtAthCode.Name = "txtAthCode"
        Me.txtAthCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAthCode.Size = New System.Drawing.Size(92, 20)
        Me.txtAthCode.TabIndex = 14
        '
        'TxtEmpName
        '
        Me.TxtEmpName.AcceptsReturn = True
        Me.TxtEmpName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtEmpName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtEmpName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtEmpName.Enabled = False
        Me.TxtEmpName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmpName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtEmpName.Location = New System.Drawing.Point(218, 38)
        Me.TxtEmpName.MaxLength = 0
        Me.TxtEmpName.Name = "TxtEmpName"
        Me.TxtEmpName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtEmpName.Size = New System.Drawing.Size(319, 20)
        Me.TxtEmpName.TabIndex = 5
        '
        'txtPlace
        '
        Me.txtPlace.AcceptsReturn = True
        Me.txtPlace.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPlace.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPlace.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlace.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPlace.Location = New System.Drawing.Point(90, 158)
        Me.txtPlace.MaxLength = 0
        Me.txtPlace.Name = "txtPlace"
        Me.txtPlace.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPlace.Size = New System.Drawing.Size(448, 20)
        Me.txtPlace.TabIndex = 10
        '
        'txtDept
        '
        Me.txtDept.AcceptsReturn = True
        Me.txtDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDept.Enabled = False
        Me.txtDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDept.Location = New System.Drawing.Point(90, 62)
        Me.txtDept.MaxLength = 0
        Me.txtDept.Name = "txtDept"
        Me.txtDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDept.Size = New System.Drawing.Size(92, 20)
        Me.txtDept.TabIndex = 6
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpCode.Location = New System.Drawing.Point(90, 38)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(92, 20)
        Me.txtEmpCode.TabIndex = 3
        '
        'txtRefNo
        '
        Me.txtRefNo.AcceptsReturn = True
        Me.txtRefNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefNo.Location = New System.Drawing.Point(90, 14)
        Me.txtRefNo.MaxLength = 0
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefNo.Size = New System.Drawing.Size(92, 20)
        Me.txtRefNo.TabIndex = 1
        '
        'optType
        '
        Me.optType.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.optType.Controls.Add(Me._optMoveType_2)
        Me.optType.Controls.Add(Me._optMoveType_1)
        Me.optType.Controls.Add(Me._optMoveType_0)
        Me.optType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.Location = New System.Drawing.Point(106, 84)
        Me.optType.Name = "optType"
        Me.optType.Padding = New System.Windows.Forms.Padding(0)
        Me.optType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optType.Size = New System.Drawing.Size(327, 39)
        Me.optType.TabIndex = 39
        Me.optType.TabStop = False
        '
        '_optMoveType_2
        '
        Me._optMoveType_2.AutoSize = True
        Me._optMoveType_2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._optMoveType_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optMoveType_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optMoveType_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMoveType.SetIndex(Me._optMoveType_2, CType(2, Short))
        Me._optMoveType_2.Location = New System.Drawing.Point(202, 16)
        Me._optMoveType_2.Name = "_optMoveType_2"
        Me._optMoveType_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optMoveType_2.Size = New System.Drawing.Size(83, 18)
        Me._optMoveType_2.TabIndex = 47
        Me._optMoveType_2.TabStop = True
        Me._optMoveType_2.Text = "Manual (I/O)"
        Me._optMoveType_2.UseVisualStyleBackColor = False
        '
        '_optMoveType_1
        '
        Me._optMoveType_1.AutoSize = True
        Me._optMoveType_1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._optMoveType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optMoveType_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optMoveType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMoveType.SetIndex(Me._optMoveType_1, CType(1, Short))
        Me._optMoveType_1.Location = New System.Drawing.Point(108, 16)
        Me._optMoveType_1.Name = "_optMoveType_1"
        Me._optMoveType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optMoveType_1.Size = New System.Drawing.Size(67, 18)
        Me._optMoveType_1.TabIndex = 18
        Me._optMoveType_1.TabStop = True
        Me._optMoveType_1.Text = "Personal"
        Me._optMoveType_1.UseVisualStyleBackColor = False
        '
        '_optMoveType_0
        '
        Me._optMoveType_0.AutoSize = True
        Me._optMoveType_0.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._optMoveType_0.Checked = True
        Me._optMoveType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optMoveType_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optMoveType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMoveType.SetIndex(Me._optMoveType_0, CType(0, Short))
        Me._optMoveType_0.Location = New System.Drawing.Point(12, 16)
        Me._optMoveType_0.Name = "_optMoveType_0"
        Me._optMoveType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optMoveType_0.Size = New System.Drawing.Size(59, 18)
        Me._optMoveType_0.TabIndex = 17
        Me._optMoveType_0.TabStop = True
        Me._optMoveType_0.Text = "Official"
        Me._optMoveType_0.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(446, 92)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 50
        '
        'txtFrom
        '
        Me.txtFrom.AllowPromptAsInput = False
        Me.txtFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrom.Location = New System.Drawing.Point(90, 182)
        Me.txtFrom.Mask = "##:##"
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(92, 20)
        Me.txtFrom.TabIndex = 11
        '
        'txtTo
        '
        Me.txtTo.AllowPromptAsInput = False
        Me.txtTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTo.Location = New System.Drawing.Point(228, 182)
        Me.txtTo.Mask = "##:##"
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(92, 20)
        Me.txtTo.TabIndex = 12
        '
        'txtTotalHrs
        '
        Me.txtTotalHrs.AllowPromptAsInput = False
        Me.txtTotalHrs.Enabled = False
        Me.txtTotalHrs.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalHrs.Location = New System.Drawing.Point(446, 182)
        Me.txtTotalHrs.Mask = "##:##"
        Me.txtTotalHrs.Name = "txtTotalHrs"
        Me.txtTotalHrs.Size = New System.Drawing.Size(92, 20)
        Me.txtTotalHrs.TabIndex = 13
        '
        'txtRefDateTo
        '
        Me.txtRefDateTo.AllowPromptAsInput = False
        Me.txtRefDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefDateTo.Location = New System.Drawing.Point(452, 14)
        Me.txtRefDateTo.Mask = "##/##/####"
        Me.txtRefDateTo.Name = "txtRefDateTo"
        Me.txtRefDateTo.Size = New System.Drawing.Size(84, 20)
        Me.txtRefDateTo.TabIndex = 41
        '
        'lblMovementType
        '
        Me.lblMovementType.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.lblMovementType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMovementType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMovementType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMovementType.Location = New System.Drawing.Point(28, 264)
        Me.lblMovementType.Name = "lblMovementType"
        Me.lblMovementType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMovementType.Size = New System.Drawing.Size(107, 13)
        Me.lblMovementType.TabIndex = 55
        Me.lblMovementType.Text = "lblMovementType"
        Me.lblMovementType.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(6, 96)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(88, 14)
        Me.Label14.TabIndex = 48
        Me.Label14.Text = "Movement Type :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(430, 134)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(66, 14)
        Me.Label13.TabIndex = 46
        Me.Label13.Text = "Appox. KM :"
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(452, 64)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(73, 17)
        Me.lblBookType.TabIndex = 45
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(198, 134)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(90, 14)
        Me.Label12.TabIndex = 44
        Me.Label12.Text = "Mode of Vehicle :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(6, 134)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(61, 14)
        Me.Label11.TabIndex = 43
        Me.Label11.Text = "Visit From :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(372, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(69, 14)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "Ref Date To :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(6, 208)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(61, 14)
        Me.Label9.TabIndex = 40
        Me.Label9.Text = "Ath. Code :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(378, 182)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(55, 14)
        Me.Label8.TabIndex = 38
        Me.Label8.Text = "Total Hrs :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(196, 184)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(24, 14)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "To :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(6, 184)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(37, 14)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = "From :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(6, 160)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(73, 14)
        Me.Label5.TabIndex = 35
        Me.Label5.Text = "Place to visit :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(6, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(35, 14)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "Dept :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(188, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(82, 14)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Ref Date From :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(46, 14)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "Ref No :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(61, 14)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "Emp Code :"
        '
        'Fragridview
        '
        Me.Fragridview.BackColor = System.Drawing.SystemColors.Control
        Me.Fragridview.Controls.Add(Me.SprdView)
        Me.Fragridview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Fragridview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Fragridview.Location = New System.Drawing.Point(0, -5)
        Me.Fragridview.Name = "Fragridview"
        Me.Fragridview.Padding = New System.Windows.Forms.Padding(0)
        Me.Fragridview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Fragridview.Size = New System.Drawing.Size(551, 339)
        Me.Fragridview.TabIndex = 28
        Me.Fragridview.TabStop = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(2, 8)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(547, 324)
        Me.SprdView.TabIndex = 30
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 326)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(551, 53)
        Me.FraMovement.TabIndex = 29
        Me.FraMovement.TabStop = False
        '
        'optMoveType
        '
        '
        'txtOTThisMonth
        '
        Me.txtOTThisMonth.AcceptsReturn = True
        Me.txtOTThisMonth.BackColor = System.Drawing.SystemColors.Window
        Me.txtOTThisMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOTThisMonth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOTThisMonth.Enabled = False
        Me.txtOTThisMonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOTThisMonth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOTThisMonth.Location = New System.Drawing.Point(269, 55)
        Me.txtOTThisMonth.MaxLength = 0
        Me.txtOTThisMonth.Name = "txtOTThisMonth"
        Me.txtOTThisMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOTThisMonth.Size = New System.Drawing.Size(58, 20)
        Me.txtOTThisMonth.TabIndex = 58
        Me.txtOTThisMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(197, 57)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(74, 14)
        Me.Label18.TabIndex = 59
        Me.Label18.Text = "OT Till Month :"
        '
        'frmPayMovementSlip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(551, 381)
        Me.Controls.Add(Me.FraView)
        Me.Controls.Add(Me.Fragridview)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPayMovementSlip"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Employee Movement Slip"
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.optType.ResumeLayout(False)
        Me.optType.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Fragridview.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.optMoveType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(ADataGrid, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub

    Public WithEvents txtOTHr As TextBox
    Public WithEvents chkAgtOT As CheckBox
    Public WithEvents Label17 As Label
    Public WithEvents txtOTThisMonth As TextBox
    Public WithEvents Label18 As Label
#End Region
End Class