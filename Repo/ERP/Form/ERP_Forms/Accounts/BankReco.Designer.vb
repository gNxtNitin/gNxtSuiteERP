Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBankReco
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
        'Me.MDIParent = AccountGST.Master
        'AccountGST.Master.Show
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
    Public WithEvents cboBank As System.Windows.Forms.ComboBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents txtClearDate As System.Windows.Forms.TextBox
    Public WithEvents txtChqNo As System.Windows.Forms.TextBox
    Public WithEvents lblClearDate As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents TxtBankStmtBal As System.Windows.Forms.TextBox
    Public WithEvents TxtBankBookBal As System.Windows.Forms.TextBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents lblBSDrCr As System.Windows.Forms.Label
    Public WithEvents lblBBDrCr As System.Windows.Forms.Label
    'Public WithEvents Line1 As Microsoft.VisualBasic.PowerPacks.LineShape
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents _optShow_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents TxtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents TxtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents LblDate_Issue_Receive As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents optShow As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'Public WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBankReco))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.cboBank = New System.Windows.Forms.ComboBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtClearDate = New System.Windows.Forms.TextBox()
        Me.txtChqNo = New System.Windows.Forms.TextBox()
        Me.lblClearDate = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.txtBankBalance = New System.Windows.Forms.TextBox()
        Me.lblBankBDrCr = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblNIOBDrCr = New System.Windows.Forms.Label()
        Me.txtNotInOurBook = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtNotClear = New System.Windows.Forms.TextBox()
        Me.lblNCDRCr = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtBankStmtBal = New System.Windows.Forms.TextBox()
        Me.TxtBankBookBal = New System.Windows.Forms.TextBox()
        Me.lblBSDrCr = New System.Windows.Forms.Label()
        Me.lblBBDrCr = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._optShow_3 = New System.Windows.Forms.RadioButton()
        Me._optShow_2 = New System.Windows.Forms.RadioButton()
        Me._optShow_1 = New System.Windows.Forms.RadioButton()
        Me._optShow_0 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtAsOnDate = New System.Windows.Forms.MaskedTextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.TxtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.LblDate_Issue_Receive = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.optShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstCompanyName = New System.Windows.Forms.CheckedListBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.optClearDate = New System.Windows.Forms.RadioButton()
        Me.optVDate = New System.Windows.Forms.RadioButton()
        Me.Frame5.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdShow.Location = New System.Drawing.Point(772, 10)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(67, 37)
        Me.cmdShow.TabIndex = 22
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdExit.Location = New System.Drawing.Point(972, 10)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(67, 37)
        Me.cmdExit.TabIndex = 21
        Me.cmdExit.Text = "&Close"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdExit, "Close the Form")
        Me.cmdExit.UseVisualStyleBackColor = False
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
        Me.CmdPreview.Location = New System.Drawing.Point(905, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 4
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
        Me.cmdPrint.Location = New System.Drawing.Point(839, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 5
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.cboBank)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(216, -2)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(358, 61)
        Me.Frame5.TabIndex = 28
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Bank"
        '
        'cboBank
        '
        Me.cboBank.BackColor = System.Drawing.SystemColors.Window
        Me.cboBank.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBank.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBank.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.cboBank.Location = New System.Drawing.Point(3, 22)
        Me.cboBank.Name = "cboBank"
        Me.cboBank.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboBank.Size = New System.Drawing.Size(347, 22)
        Me.cboBank.Sorted = True
        Me.cboBank.TabIndex = 29
        Me.cboBank.TabStop = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtClearDate)
        Me.Frame4.Controls.Add(Me.txtChqNo)
        Me.Frame4.Controls.Add(Me.lblClearDate)
        Me.Frame4.Controls.Add(Me.Label6)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(216, 56)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(358, 35)
        Me.Frame4.TabIndex = 23
        Me.Frame4.TabStop = False
        '
        'txtClearDate
        '
        Me.txtClearDate.AcceptsReturn = True
        Me.txtClearDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtClearDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClearDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtClearDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClearDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtClearDate.Location = New System.Drawing.Point(280, 10)
        Me.txtClearDate.MaxLength = 0
        Me.txtClearDate.Name = "txtClearDate"
        Me.txtClearDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtClearDate.Size = New System.Drawing.Size(70, 20)
        Me.txtClearDate.TabIndex = 25
        Me.txtClearDate.Visible = False
        '
        'txtChqNo
        '
        Me.txtChqNo.AcceptsReturn = True
        Me.txtChqNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtChqNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChqNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChqNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChqNo.Location = New System.Drawing.Point(60, 10)
        Me.txtChqNo.MaxLength = 0
        Me.txtChqNo.Name = "txtChqNo"
        Me.txtChqNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChqNo.Size = New System.Drawing.Size(70, 20)
        Me.txtChqNo.TabIndex = 24
        '
        'lblClearDate
        '
        Me.lblClearDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblClearDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblClearDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClearDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblClearDate.Location = New System.Drawing.Point(226, 14)
        Me.lblClearDate.Name = "lblClearDate"
        Me.lblClearDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblClearDate.Size = New System.Drawing.Size(67, 13)
        Me.lblClearDate.TabIndex = 27
        Me.lblClearDate.Text = "Clearing Date"
        Me.lblClearDate.Visible = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(6, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(67, 13)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "Chq. No."
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.txtBankBalance)
        Me.FraMovement.Controls.Add(Me.lblBankBDrCr)
        Me.FraMovement.Controls.Add(Me.Label13)
        Me.FraMovement.Controls.Add(Me.lblNIOBDrCr)
        Me.FraMovement.Controls.Add(Me.txtNotInOurBook)
        Me.FraMovement.Controls.Add(Me.Label10)
        Me.FraMovement.Controls.Add(Me.txtNotClear)
        Me.FraMovement.Controls.Add(Me.lblNCDRCr)
        Me.FraMovement.Controls.Add(Me.Label9)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Controls.Add(Me.cmdExit)
        Me.FraMovement.Controls.Add(Me.TxtBankStmtBal)
        Me.FraMovement.Controls.Add(Me.TxtBankBookBal)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.lblBSDrCr)
        Me.FraMovement.Controls.Add(Me.lblBBDrCr)
        Me.FraMovement.Controls.Add(Me.Label5)
        Me.FraMovement.Controls.Add(Me.Label4)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 568)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(1042, 51)
        Me.FraMovement.TabIndex = 14
        Me.FraMovement.TabStop = False
        '
        'txtBankBalance
        '
        Me.txtBankBalance.AcceptsReturn = True
        Me.txtBankBalance.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankBalance.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankBalance.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankBalance.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.txtBankBalance.Location = New System.Drawing.Point(620, 30)
        Me.txtBankBalance.MaxLength = 0
        Me.txtBankBalance.Name = "txtBankBalance"
        Me.txtBankBalance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankBalance.Size = New System.Drawing.Size(124, 20)
        Me.txtBankBalance.TabIndex = 30
        Me.txtBankBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBankBDrCr
        '
        Me.lblBankBDrCr.BackColor = System.Drawing.SystemColors.Control
        Me.lblBankBDrCr.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBankBDrCr.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankBDrCr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBankBDrCr.Location = New System.Drawing.Point(747, 30)
        Me.lblBankBDrCr.Name = "lblBankBDrCr"
        Me.lblBankBDrCr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBankBDrCr.Size = New System.Drawing.Size(19, 17)
        Me.lblBankBDrCr.TabIndex = 31
        Me.lblBankBDrCr.Text = "Dr"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(520, 30)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(108, 17)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "Bank Balance:"
        '
        'lblNIOBDrCr
        '
        Me.lblNIOBDrCr.BackColor = System.Drawing.SystemColors.Control
        Me.lblNIOBDrCr.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNIOBDrCr.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNIOBDrCr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNIOBDrCr.Location = New System.Drawing.Point(500, 30)
        Me.lblNIOBDrCr.Name = "lblNIOBDrCr"
        Me.lblNIOBDrCr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNIOBDrCr.Size = New System.Drawing.Size(19, 17)
        Me.lblNIOBDrCr.TabIndex = 28
        Me.lblNIOBDrCr.Text = "Dr"
        '
        'txtNotInOurBook
        '
        Me.txtNotInOurBook.AcceptsReturn = True
        Me.txtNotInOurBook.BackColor = System.Drawing.SystemColors.Window
        Me.txtNotInOurBook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotInOurBook.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNotInOurBook.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotInOurBook.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.txtNotInOurBook.Location = New System.Drawing.Point(389, 30)
        Me.txtNotInOurBook.MaxLength = 0
        Me.txtNotInOurBook.Name = "txtNotInOurBook"
        Me.txtNotInOurBook.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNotInOurBook.Size = New System.Drawing.Size(107, 20)
        Me.txtNotInOurBook.TabIndex = 27
        Me.txtNotInOurBook.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(275, 30)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(116, 17)
        Me.Label10.TabIndex = 26
        Me.Label10.Text = "Not In Our Book :"
        '
        'txtNotClear
        '
        Me.txtNotClear.AcceptsReturn = True
        Me.txtNotClear.BackColor = System.Drawing.SystemColors.Window
        Me.txtNotClear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotClear.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNotClear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotClear.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.txtNotClear.Location = New System.Drawing.Point(389, 10)
        Me.txtNotClear.MaxLength = 0
        Me.txtNotClear.Name = "txtNotClear"
        Me.txtNotClear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNotClear.Size = New System.Drawing.Size(107, 20)
        Me.txtNotClear.TabIndex = 24
        Me.txtNotClear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblNCDRCr
        '
        Me.lblNCDRCr.BackColor = System.Drawing.SystemColors.Control
        Me.lblNCDRCr.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNCDRCr.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNCDRCr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNCDRCr.Location = New System.Drawing.Point(500, 10)
        Me.lblNCDRCr.Name = "lblNCDRCr"
        Me.lblNCDRCr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNCDRCr.Size = New System.Drawing.Size(19, 17)
        Me.lblNCDRCr.TabIndex = 25
        Me.lblNCDRCr.Text = "Dr"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(275, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(102, 17)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "Not Clear :"
        '
        'TxtBankStmtBal
        '
        Me.TxtBankStmtBal.AcceptsReturn = True
        Me.TxtBankStmtBal.BackColor = System.Drawing.SystemColors.Window
        Me.TxtBankStmtBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBankStmtBal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtBankStmtBal.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankStmtBal.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.TxtBankStmtBal.Location = New System.Drawing.Point(128, 30)
        Me.TxtBankStmtBal.MaxLength = 0
        Me.TxtBankStmtBal.Name = "TxtBankStmtBal"
        Me.TxtBankStmtBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtBankStmtBal.Size = New System.Drawing.Size(123, 20)
        Me.TxtBankStmtBal.TabIndex = 18
        Me.TxtBankStmtBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtBankBookBal
        '
        Me.TxtBankBookBal.AcceptsReturn = True
        Me.TxtBankBookBal.BackColor = System.Drawing.SystemColors.Window
        Me.TxtBankBookBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBankBookBal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtBankBookBal.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankBookBal.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.TxtBankBookBal.Location = New System.Drawing.Point(128, 10)
        Me.TxtBankBookBal.MaxLength = 0
        Me.TxtBankBookBal.Name = "TxtBankBookBal"
        Me.TxtBankBookBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtBankBookBal.Size = New System.Drawing.Size(123, 20)
        Me.TxtBankBookBal.TabIndex = 17
        Me.TxtBankBookBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBSDrCr
        '
        Me.lblBSDrCr.BackColor = System.Drawing.SystemColors.Control
        Me.lblBSDrCr.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBSDrCr.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBSDrCr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBSDrCr.Location = New System.Drawing.Point(253, 30)
        Me.lblBSDrCr.Name = "lblBSDrCr"
        Me.lblBSDrCr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBSDrCr.Size = New System.Drawing.Size(19, 17)
        Me.lblBSDrCr.TabIndex = 20
        Me.lblBSDrCr.Text = "Dr"
        '
        'lblBBDrCr
        '
        Me.lblBBDrCr.BackColor = System.Drawing.SystemColors.Control
        Me.lblBBDrCr.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBBDrCr.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBBDrCr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBBDrCr.Location = New System.Drawing.Point(253, 10)
        Me.lblBBDrCr.Name = "lblBBDrCr"
        Me.lblBBDrCr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBBDrCr.Size = New System.Drawing.Size(19, 17)
        Me.lblBBDrCr.TabIndex = 19
        Me.lblBBDrCr.Text = "Dr"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(4, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(127, 17)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Book Cl Balance :"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(4, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(127, 17)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Book Op Balance :"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._optShow_3)
        Me.Frame2.Controls.Add(Me._optShow_2)
        Me.Frame2.Controls.Add(Me._optShow_1)
        Me.Frame2.Controls.Add(Me._optShow_0)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(577, -2)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(232, 92)
        Me.Frame2.TabIndex = 10
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Show"
        '
        '_optShow_3
        '
        Me._optShow_3.AutoSize = True
        Me._optShow_3.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_3, CType(3, Short))
        Me._optShow_3.Location = New System.Drawing.Point(96, 58)
        Me._optShow_3.Name = "_optShow_3"
        Me._optShow_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_3.Size = New System.Drawing.Size(135, 18)
        Me._optShow_3.TabIndex = 3
        Me._optShow_3.Text = "Not Clear as on Date"
        Me._optShow_3.UseVisualStyleBackColor = False
        '
        '_optShow_2
        '
        Me._optShow_2.AutoSize = True
        Me._optShow_2.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_2, CType(2, Short))
        Me._optShow_2.Location = New System.Drawing.Point(96, 23)
        Me._optShow_2.Name = "_optShow_2"
        Me._optShow_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_2.Size = New System.Drawing.Size(106, 18)
        Me._optShow_2.TabIndex = 2
        Me._optShow_2.Text = "Not Reconciled"
        Me._optShow_2.UseVisualStyleBackColor = False
        '
        '_optShow_1
        '
        Me._optShow_1.AutoSize = True
        Me._optShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_1.Checked = True
        Me._optShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_1, CType(1, Short))
        Me._optShow_1.Location = New System.Drawing.Point(6, 58)
        Me._optShow_1.Name = "_optShow_1"
        Me._optShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_1.Size = New System.Drawing.Size(85, 18)
        Me._optShow_1.TabIndex = 1
        Me._optShow_1.TabStop = True
        Me._optShow_1.Text = "Reconciled"
        Me._optShow_1.UseVisualStyleBackColor = False
        '
        '_optShow_0
        '
        Me._optShow_0.AutoSize = True
        Me._optShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_0, CType(0, Short))
        Me._optShow_0.Location = New System.Drawing.Point(6, 23)
        Me._optShow_0.Name = "_optShow_0"
        Me._optShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_0.Size = New System.Drawing.Size(39, 18)
        Me._optShow_0.TabIndex = 0
        Me._optShow_0.Text = "All"
        Me._optShow_0.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtAsOnDate)
        Me.Frame1.Controls.Add(Me.Label8)
        Me.Frame1.Controls.Add(Me.TxtDateTo)
        Me.Frame1.Controls.Add(Me.TxtDateFrom)
        Me.Frame1.Controls.Add(Me.LblDate_Issue_Receive)
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.Controls.Add(Me.lblBookType)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, -2)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(144, 92)
        Me.Frame1.TabIndex = 6
        Me.Frame1.TabStop = False
        '
        'txtAsOnDate
        '
        Me.txtAsOnDate.AllowPromptAsInput = False
        Me.txtAsOnDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAsOnDate.Location = New System.Drawing.Point(57, 64)
        Me.txtAsOnDate.Mask = "##/##/####"
        Me.txtAsOnDate.Name = "txtAsOnDate"
        Me.txtAsOnDate.Size = New System.Drawing.Size(80, 20)
        Me.txtAsOnDate.TabIndex = 34
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(3, 66)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(46, 14)
        Me.Label8.TabIndex = 33
        Me.Label8.Text = "As On :"
        '
        'TxtDateTo
        '
        Me.TxtDateTo.AllowPromptAsInput = False
        Me.TxtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateTo.Location = New System.Drawing.Point(57, 39)
        Me.TxtDateTo.Mask = "##/##/####"
        Me.TxtDateTo.Name = "TxtDateTo"
        Me.TxtDateTo.Size = New System.Drawing.Size(80, 20)
        Me.TxtDateTo.TabIndex = 32
        '
        'TxtDateFrom
        '
        Me.TxtDateFrom.AllowPromptAsInput = False
        Me.TxtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateFrom.Location = New System.Drawing.Point(57, 14)
        Me.TxtDateFrom.Mask = "##/##/####"
        Me.TxtDateFrom.Name = "TxtDateFrom"
        Me.TxtDateFrom.Size = New System.Drawing.Size(80, 20)
        Me.TxtDateFrom.TabIndex = 31
        '
        'LblDate_Issue_Receive
        '
        Me.LblDate_Issue_Receive.AutoSize = True
        Me.LblDate_Issue_Receive.BackColor = System.Drawing.SystemColors.Control
        Me.LblDate_Issue_Receive.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDate_Issue_Receive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDate_Issue_Receive.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblDate_Issue_Receive.Location = New System.Drawing.Point(7, 18)
        Me.LblDate_Issue_Receive.Name = "LblDate_Issue_Receive"
        Me.LblDate_Issue_Receive.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDate_Issue_Receive.Size = New System.Drawing.Size(42, 14)
        Me.LblDate_Issue_Receive.TabIndex = 9
        Me.LblDate_Issue_Receive.Text = "From :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(23, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(26, 14)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "To :"
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(167, 23)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(40, 13)
        Me.lblBookType.TabIndex = 7
        Me.lblBookType.Visible = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.SprdMain)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 85)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(1110, 466)
        Me.Frame3.TabIndex = 11
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(26, 198)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 0
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 13)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(1110, 453)
        Me.SprdMain.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(0, 555)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(231, 15)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Press +/- for Mark/Unmark as Cleared"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(446, 555)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(305, 15)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Double Click on Grid or Press <RTN> Key"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(262, 555)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(183, 15)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Mark/Unmark as Cleared :"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.lstCompanyName)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(812, -2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(298, 92)
        Me.GroupBox1.TabIndex = 65
        Me.GroupBox1.TabStop = False
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
        Me.lstCompanyName.Size = New System.Drawing.Size(298, 79)
        Me.lstCompanyName.TabIndex = 4
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox2.Controls.Add(Me.optVDate)
        Me.GroupBox2.Controls.Add(Me.optClearDate)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox2.Location = New System.Drawing.Point(145, -2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox2.Size = New System.Drawing.Size(68, 92)
        Me.GroupBox2.TabIndex = 66
        Me.GroupBox2.TabStop = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(167, 23)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(40, 13)
        Me.Label15.TabIndex = 7
        Me.Label15.Visible = False
        '
        'optClearDate
        '
        Me.optClearDate.BackColor = System.Drawing.SystemColors.Control
        Me.optClearDate.Checked = True
        Me.optClearDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.optClearDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optClearDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optClearDate.Location = New System.Drawing.Point(5, 10)
        Me.optClearDate.Name = "optClearDate"
        Me.optClearDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optClearDate.Size = New System.Drawing.Size(57, 39)
        Me.optClearDate.TabIndex = 8
        Me.optClearDate.TabStop = True
        Me.optClearDate.Text = "Clear Date"
        Me.optClearDate.UseVisualStyleBackColor = False
        '
        'optVDate
        '
        Me.optVDate.BackColor = System.Drawing.SystemColors.Control
        Me.optVDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.optVDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optVDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optVDate.Location = New System.Drawing.Point(5, 48)
        Me.optVDate.Name = "optVDate"
        Me.optVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optVDate.Size = New System.Drawing.Size(57, 39)
        Me.optVDate.TabIndex = 9
        Me.optVDate.Text = "VDate"
        Me.optVDate.UseVisualStyleBackColor = False
        '
        'frmBankReco
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1114, 621)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBankReco"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Bank Reconciliation"
        Me.Frame5.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.FraMovement.ResumeLayout(False)
        Me.FraMovement.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
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

    Public WithEvents txtNotClear As TextBox
    Public WithEvents lblNCDRCr As Label
    Public WithEvents Label9 As Label
    Public WithEvents txtNotInOurBook As TextBox
    Public WithEvents Label10 As Label
    Public WithEvents txtBankBalance As TextBox
    Public WithEvents lblBankBDrCr As Label
    Public WithEvents Label13 As Label
    Public WithEvents lblNIOBDrCr As Label
    Public WithEvents _optShow_3 As RadioButton
    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents lstCompanyName As CheckedListBox
    Public WithEvents txtAsOnDate As MaskedTextBox
    Public WithEvents Label8 As Label
    Public WithEvents GroupBox2 As GroupBox
    Public WithEvents optVDate As RadioButton
    Public WithEvents optClearDate As RadioButton
    Public WithEvents Label15 As Label
#End Region
End Class