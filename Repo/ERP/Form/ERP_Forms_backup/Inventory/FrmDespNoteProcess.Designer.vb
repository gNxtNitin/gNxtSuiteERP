Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmDespNoteProcess
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
        'Me.MDIParent = SalesGST.Master

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
    Public WithEvents cboInvType As System.Windows.Forms.ComboBox
    Public WithEvents chkSuppInvoice As System.Windows.Forms.CheckBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents _OptAmendType_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptAmendType_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptAmendType_0 As System.Windows.Forms.RadioButton
    Public WithEvents txtCustAmendNo As System.Windows.Forms.TextBox
    Public WithEvents txtCustAmendDate As System.Windows.Forms.TextBox
    Public WithEvents txtAmendNo As System.Windows.Forms.TextBox
    Public WithEvents txtPONo As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents chkSaleReturn As System.Windows.Forms.CheckBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents _OptItem_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptItem_1 As System.Windows.Forms.RadioButton
    Public WithEvents cmdSearchItem As System.Windows.Forms.Button
    Public WithEvents txtItem As System.Windows.Forms.TextBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents TxtAccount As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents TxtDtFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents TxtDtTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents LblDtto As System.Windows.Forms.Label
    Public WithEvents LblDtfr As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cmdProcess As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents FraButton As System.Windows.Forms.GroupBox
    Public WithEvents OptAmendType As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents OptItem As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDespNoteProcess))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtCustAmendNo = New System.Windows.Forms.TextBox()
        Me.txtCustAmendDate = New System.Windows.Forms.TextBox()
        Me.txtAmendNo = New System.Windows.Forms.TextBox()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.cmdSearchItem = New System.Windows.Forms.Button()
        Me.txtItem = New System.Windows.Forms.TextBox()
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdProcess = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.cboInvType = New System.Windows.Forms.ComboBox()
        Me.chkSuppInvoice = New System.Windows.Forms.CheckBox()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me._OptAmendType_2 = New System.Windows.Forms.RadioButton()
        Me._OptAmendType_1 = New System.Windows.Forms.RadioButton()
        Me._OptAmendType_0 = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.chkSaleReturn = New System.Windows.Forms.CheckBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me._OptItem_0 = New System.Windows.Forms.RadioButton()
        Me._OptItem_1 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.TxtDtFrom = New System.Windows.Forms.MaskedTextBox()
        Me.TxtDtTo = New System.Windows.Forms.MaskedTextBox()
        Me.LblDtto = New System.Windows.Forms.Label()
        Me.LblDtfr = New System.Windows.Forms.Label()
        Me.FraButton = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.OptAmendType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptItem = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame5.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.FraButton.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptAmendType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCustAmendNo
        '
        Me.txtCustAmendNo.AcceptsReturn = True
        Me.txtCustAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustAmendNo.Enabled = False
        Me.txtCustAmendNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustAmendNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustAmendNo.Location = New System.Drawing.Point(106, 104)
        Me.txtCustAmendNo.MaxLength = 0
        Me.txtCustAmendNo.Name = "txtCustAmendNo"
        Me.txtCustAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustAmendNo.Size = New System.Drawing.Size(59, 19)
        Me.txtCustAmendNo.TabIndex = 29
        Me.ToolTip1.SetToolTip(Me.txtCustAmendNo, "Press F1 For Help")
        '
        'txtCustAmendDate
        '
        Me.txtCustAmendDate.AcceptsReturn = True
        Me.txtCustAmendDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustAmendDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustAmendDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustAmendDate.Enabled = False
        Me.txtCustAmendDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustAmendDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustAmendDate.Location = New System.Drawing.Point(250, 104)
        Me.txtCustAmendDate.MaxLength = 0
        Me.txtCustAmendDate.Name = "txtCustAmendDate"
        Me.txtCustAmendDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustAmendDate.Size = New System.Drawing.Size(71, 19)
        Me.txtCustAmendDate.TabIndex = 28
        Me.ToolTip1.SetToolTip(Me.txtCustAmendDate, "Press F1 For Help")
        '
        'txtAmendNo
        '
        Me.txtAmendNo.AcceptsReturn = True
        Me.txtAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmendNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmendNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAmendNo.Location = New System.Drawing.Point(250, 82)
        Me.txtAmendNo.MaxLength = 0
        Me.txtAmendNo.Name = "txtAmendNo"
        Me.txtAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmendNo.Size = New System.Drawing.Size(71, 19)
        Me.txtAmendNo.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.txtAmendNo, "Press F1 For Help")
        '
        'txtPONo
        '
        Me.txtPONo.AcceptsReturn = True
        Me.txtPONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPONo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPONo.Location = New System.Drawing.Point(76, 82)
        Me.txtPONo.MaxLength = 0
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPONo.Size = New System.Drawing.Size(89, 19)
        Me.txtPONo.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.txtPONo, "Press F1 For Help")
        '
        'cmdSearchItem
        '
        Me.cmdSearchItem.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchItem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchItem.Image = CType(resources.GetObject("cmdSearchItem.Image"), System.Drawing.Image)
        Me.cmdSearchItem.Location = New System.Drawing.Point(294, 29)
        Me.cmdSearchItem.Name = "cmdSearchItem"
        Me.cmdSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchItem.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchItem.TabIndex = 7
        Me.cmdSearchItem.TabStop = False
        Me.cmdSearchItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchItem, "Search")
        Me.cmdSearchItem.UseVisualStyleBackColor = False
        '
        'txtItem
        '
        Me.txtItem.AcceptsReturn = True
        Me.txtItem.BackColor = System.Drawing.SystemColors.Window
        Me.txtItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItem.Location = New System.Drawing.Point(4, 29)
        Me.txtItem.MaxLength = 0
        Me.txtItem.Name = "txtItem"
        Me.txtItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItem.Size = New System.Drawing.Size(289, 19)
        Me.txtItem.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtItem, "Press F1 For Help")
        '
        'TxtAccount
        '
        Me.TxtAccount.AcceptsReturn = True
        Me.TxtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(4, 16)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(289, 19)
        Me.TxtAccount.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(294, 16)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearch.TabIndex = 4
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdProcess
        '
        Me.cmdProcess.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdProcess.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdProcess.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdProcess.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdProcess.Image = CType(resources.GetObject("cmdProcess.Image"), System.Drawing.Image)
        Me.cmdProcess.Location = New System.Drawing.Point(4, 12)
        Me.cmdProcess.Name = "cmdProcess"
        Me.cmdProcess.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdProcess.Size = New System.Drawing.Size(80, 34)
        Me.cmdProcess.TabIndex = 14
        Me.cmdProcess.Text = "&Process"
        Me.cmdProcess.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdProcess, "Save Current Record")
        Me.cmdProcess.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Image = CType(resources.GetObject("cmdCancel.Image"), System.Drawing.Image)
        Me.cmdCancel.Location = New System.Drawing.Point(210, 12)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(80, 34)
        Me.cmdCancel.TabIndex = 8
        Me.cmdCancel.Text = "&Close"
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdCancel, "Close")
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.cboInvType)
        Me.Frame5.Controls.Add(Me.chkSuppInvoice)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(0, 201)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(327, 55)
        Me.Frame5.TabIndex = 25
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Invoice"
        '
        'cboInvType
        '
        Me.cboInvType.BackColor = System.Drawing.SystemColors.Window
        Me.cboInvType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboInvType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInvType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInvType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboInvType.Location = New System.Drawing.Point(56, 28)
        Me.cboInvType.Name = "cboInvType"
        Me.cboInvType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboInvType.Size = New System.Drawing.Size(265, 22)
        Me.cboInvType.TabIndex = 27
        '
        'chkSuppInvoice
        '
        Me.chkSuppInvoice.AutoSize = True
        Me.chkSuppInvoice.BackColor = System.Drawing.SystemColors.Control
        Me.chkSuppInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSuppInvoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSuppInvoice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSuppInvoice.Location = New System.Drawing.Point(56, 12)
        Me.chkSuppInvoice.Name = "chkSuppInvoice"
        Me.chkSuppInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSuppInvoice.Size = New System.Drawing.Size(186, 18)
        Me.chkSuppInvoice.TabIndex = 26
        Me.chkSuppInvoice.Text = "Generate Supplementary Invoice "
        Me.chkSuppInvoice.UseVisualStyleBackColor = False
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me._OptAmendType_2)
        Me.Frame8.Controls.Add(Me._OptAmendType_1)
        Me.Frame8.Controls.Add(Me._OptAmendType_0)
        Me.Frame8.Controls.Add(Me.txtCustAmendNo)
        Me.Frame8.Controls.Add(Me.txtCustAmendDate)
        Me.Frame8.Controls.Add(Me.txtAmendNo)
        Me.Frame8.Controls.Add(Me.txtPONo)
        Me.Frame8.Controls.Add(Me.Label4)
        Me.Frame8.Controls.Add(Me.Label3)
        Me.Frame8.Controls.Add(Me.Label2)
        Me.Frame8.Controls.Add(Me.Label1)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(0, 258)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(327, 131)
        Me.Frame8.TabIndex = 19
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Sale Order No"
        '
        '_OptAmendType_2
        '
        Me._OptAmendType_2.AutoSize = True
        Me._OptAmendType_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptAmendType_2.Checked = True
        Me._OptAmendType_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptAmendType_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptAmendType_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptAmendType.SetIndex(Me._OptAmendType_2, CType(2, Short))
        Me._OptAmendType_2.Location = New System.Drawing.Point(56, 34)
        Me._OptAmendType_2.Name = "_OptAmendType_2"
        Me._OptAmendType_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptAmendType_2.Size = New System.Drawing.Size(195, 18)
        Me._OptAmendType_2.TabIndex = 34
        Me._OptAmendType_2.TabStop = True
        Me._OptAmendType_2.Text = "As Per Amend Wise (Rate Last PO)"
        Me._OptAmendType_2.UseVisualStyleBackColor = False
        '
        '_OptAmendType_1
        '
        Me._OptAmendType_1.AutoSize = True
        Me._OptAmendType_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptAmendType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptAmendType_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptAmendType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptAmendType.SetIndex(Me._OptAmendType_1, CType(1, Short))
        Me._OptAmendType_1.Location = New System.Drawing.Point(56, 54)
        Me._OptAmendType_1.Name = "_OptAmendType_1"
        Me._OptAmendType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptAmendType_1.Size = New System.Drawing.Size(132, 18)
        Me._OptAmendType_1.TabIndex = 33
        Me._OptAmendType_1.TabStop = True
        Me._OptAmendType_1.Text = "As Per Orignal Invoice"
        Me._OptAmendType_1.UseVisualStyleBackColor = False
        '
        '_OptAmendType_0
        '
        Me._OptAmendType_0.AutoSize = True
        Me._OptAmendType_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptAmendType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptAmendType_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptAmendType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptAmendType.SetIndex(Me._OptAmendType_0, CType(0, Short))
        Me._OptAmendType_0.Location = New System.Drawing.Point(56, 16)
        Me._OptAmendType_0.Name = "_OptAmendType_0"
        Me._OptAmendType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptAmendType_0.Size = New System.Drawing.Size(205, 18)
        Me._OptAmendType_0.TabIndex = 32
        Me._OptAmendType_0.TabStop = True
        Me._OptAmendType_0.Text = "As Per Amend Wise (Rate Sale Price)"
        Me._OptAmendType_0.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(168, 106)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(74, 14)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = " Amend Date :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(8, 106)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(87, 14)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Cust Amend No :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(57, 14)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Order No :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(182, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(63, 14)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Amend No :"
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.cboDivision)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(0, 42)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(327, 35)
        Me.Frame7.TabIndex = 17
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Division"
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Enabled = False
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(56, 10)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(245, 22)
        Me.cboDivision.TabIndex = 18
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.chkSaleReturn)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 76)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(327, 27)
        Me.Frame3.TabIndex = 15
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Type"
        '
        'chkSaleReturn
        '
        Me.chkSaleReturn.AutoSize = True
        Me.chkSaleReturn.BackColor = System.Drawing.SystemColors.Control
        Me.chkSaleReturn.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSaleReturn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSaleReturn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSaleReturn.Location = New System.Drawing.Point(56, 10)
        Me.chkSaleReturn.Name = "chkSaleReturn"
        Me.chkSaleReturn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSaleReturn.Size = New System.Drawing.Size(109, 18)
        Me.chkSaleReturn.TabIndex = 16
        Me.chkSaleReturn.Text = "Less Sale Return"
        Me.chkSaleReturn.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtItem)
        Me.Frame4.Controls.Add(Me._OptItem_0)
        Me.Frame4.Controls.Add(Me._OptItem_1)
        Me.Frame4.Controls.Add(Me.cmdSearchItem)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 146)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(327, 54)
        Me.Frame4.TabIndex = 13
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Item"
        '
        '_OptItem_0
        '
        Me._OptItem_0.AutoSize = True
        Me._OptItem_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptItem_0.Checked = True
        Me._OptItem_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptItem_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptItem_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptItem.SetIndex(Me._OptItem_0, CType(0, Short))
        Me._OptItem_0.Location = New System.Drawing.Point(50, 10)
        Me._OptItem_0.Name = "_OptItem_0"
        Me._OptItem_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptItem_0.Size = New System.Drawing.Size(37, 18)
        Me._OptItem_0.TabIndex = 24
        Me._OptItem_0.TabStop = True
        Me._OptItem_0.Text = "All"
        Me._OptItem_0.UseVisualStyleBackColor = False
        '
        '_OptItem_1
        '
        Me._OptItem_1.AutoSize = True
        Me._OptItem_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptItem_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptItem_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptItem_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptItem.SetIndex(Me._OptItem_1, CType(1, Short))
        Me._OptItem_1.Location = New System.Drawing.Point(214, 10)
        Me._OptItem_1.Name = "_OptItem_1"
        Me._OptItem_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptItem_1.Size = New System.Drawing.Size(76, 18)
        Me._OptItem_1.TabIndex = 5
        Me._OptItem_1.TabStop = True
        Me._OptItem_1.Text = "Particulars"
        Me._OptItem_1.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.TxtAccount)
        Me.Frame1.Controls.Add(Me.cmdsearch)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 104)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(327, 41)
        Me.Frame1.TabIndex = 12
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Customer"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.TxtDtFrom)
        Me.Frame2.Controls.Add(Me.TxtDtTo)
        Me.Frame2.Controls.Add(Me.LblDtto)
        Me.Frame2.Controls.Add(Me.LblDtfr)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(327, 41)
        Me.Frame2.TabIndex = 0
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Date Range"
        '
        'TxtDtFrom
        '
        Me.TxtDtFrom.AllowPromptAsInput = False
        Me.TxtDtFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDtFrom.Location = New System.Drawing.Point(80, 14)
        Me.TxtDtFrom.Mask = "##/##/####"
        Me.TxtDtFrom.Name = "TxtDtFrom"
        Me.TxtDtFrom.Size = New System.Drawing.Size(81, 20)
        Me.TxtDtFrom.TabIndex = 1
        '
        'TxtDtTo
        '
        Me.TxtDtTo.AllowPromptAsInput = False
        Me.TxtDtTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDtTo.Location = New System.Drawing.Point(228, 14)
        Me.TxtDtTo.Mask = "##/##/####"
        Me.TxtDtTo.Name = "TxtDtTo"
        Me.TxtDtTo.Size = New System.Drawing.Size(81, 20)
        Me.TxtDtTo.TabIndex = 2
        '
        'LblDtto
        '
        Me.LblDtto.AutoSize = True
        Me.LblDtto.BackColor = System.Drawing.SystemColors.Control
        Me.LblDtto.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDtto.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDtto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblDtto.Location = New System.Drawing.Point(172, 16)
        Me.LblDtto.Name = "LblDtto"
        Me.LblDtto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDtto.Size = New System.Drawing.Size(49, 14)
        Me.LblDtto.TabIndex = 11
        Me.LblDtto.Text = "Date To :"
        '
        'LblDtfr
        '
        Me.LblDtfr.AutoSize = True
        Me.LblDtfr.BackColor = System.Drawing.SystemColors.Control
        Me.LblDtfr.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDtfr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDtfr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblDtfr.Location = New System.Drawing.Point(10, 16)
        Me.LblDtfr.Name = "LblDtfr"
        Me.LblDtfr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDtfr.Size = New System.Drawing.Size(62, 14)
        Me.LblDtfr.TabIndex = 9
        Me.LblDtfr.Text = "Date From :"
        '
        'FraButton
        '
        Me.FraButton.BackColor = System.Drawing.SystemColors.Control
        Me.FraButton.Controls.Add(Me.cmdProcess)
        Me.FraButton.Controls.Add(Me.cmdCancel)
        Me.FraButton.Controls.Add(Me.Report1)
        Me.FraButton.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraButton.Location = New System.Drawing.Point(0, 386)
        Me.FraButton.Name = "FraButton"
        Me.FraButton.Padding = New System.Windows.Forms.Padding(0)
        Me.FraButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraButton.Size = New System.Drawing.Size(327, 49)
        Me.FraButton.TabIndex = 10
        Me.FraButton.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(6, 10)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 15
        '
        'OptItem
        '
        '
        'FrmDespNoteProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(328, 435)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame8)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.FraButton)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDespNoteProcess"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Despatch Note Process for Supplementary Invoice"
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.FraButton.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptAmendType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class