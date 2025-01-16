Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEmployeeCategoryMst
    Inherits System.Windows.Forms.Form
    Dim RsCategory As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As Connection
    Dim Shw As Boolean
    Dim xCategoryDesc As String
    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            FraGridView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraGridView.SendToBack()
        End If

        MainClass.ButtonStatus(Me, XRIGHT, RsCategory, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        cboCatgeory.SelectedIndex = -1

        txtSBDebit.Text = ""
        txtSIncDebit.Text = ""
        txtSBonusDebit.Text = ""
        txtSLTCDebit.Text = ""
        txtSBCredit.Text = ""
        txtSIncCredit.Text = ""
        txtSBonusCredit.Text = ""
        txtSLTCCredit.Text = ""
        txtPFCredit.Text = ""
        txtESICredit.Text = ""
        txtWelfare_GS.Text = ""
        txtELPostingHead.Text = ""
        txtGRPostingHead.Text = ""
        optCategoryType(0).Checked = False
        optCategoryType(1).Checked = False
        FraCatType.Enabled = True
        cboCatgeory.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsCategory, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cboCatgeory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCatgeory.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboCatgeory_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCatgeory.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdELSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdELSearch.Click
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchMaster((txtELPostingHead.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
            txtELPostingHead.Text = AcName
        End If

        Exit Sub
    End Sub

    Private Sub cmdGRSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGRSearch.Click
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchMaster((txtGRPostingHead.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
            txtGRPostingHead.Text = AcName
        End If

        Exit Sub
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            cboCatgeory.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsCategory, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            If cboCatgeory.Enabled = True Then cboCatgeory.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsCategory.EOF = False Then RsCategory.MoveFirst()
            cboCatgeory.Enabled = True
            Show1()
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If cboCatgeory.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsCategory.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1 = False Then GoTo DelErrPart
                If RsCategory.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub frmEmployeeCategoryMst_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmEmployeeCategoryMst_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub optCategoryType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCategoryType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optCategoryType.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        SqlStr = ""

        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        SqlStr = " SELECT * from PAY_CATEGORY_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY_DESC  ='" & MainClass.AllowSingleQuote((SprdView.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCategory, ADODB.LockTypeEnum.adLockReadOnly)

        If RsCategory.EOF = False Then
            Show1()
            CmdView_Click(CmdView, New System.EventArgs())
        End If
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub cboCatgeory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboCatgeory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        If Trim(cboCatgeory.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsCategory.EOF = False Then xCategoryDesc = RsCategory.Fields("CATEGORY_DESC").Value
        SqlStr = ""
        SqlStr = " SELECT * from  PAY_CATEGORY_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY_DESC  ='" & MainClass.AllowSingleQuote(Trim(cboCatgeory.Text)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCategory, ADODB.LockTypeEnum.adLockReadOnly)
        If RsCategory.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("New Entry, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * from  PAY_CATEGORY_MST Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_DESC  ='" & xCategoryDesc & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCategory, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub frmEmployeeCategoryMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From PAY_CATEGORY_MST Where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCategory, ADODB.LockTypeEnum.adLockReadOnly)
        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        settextlength()

        cboCatgeory.Items.Clear()
        cboCatgeory.Items.Add("General Staff")
        cboCatgeory.Items.Add("Production Staff")
        cboCatgeory.Items.Add("Export Staff")
        cboCatgeory.Items.Add("Regular Worker")
        cboCatgeory.Items.Add("Staff R & D")
        cboCatgeory.Items.Add("Director")
        cboCatgeory.Items.Add("Trainee Staff")
        cboCatgeory.Items.Add("1. Trainee Worker")
        cboCatgeory.Items.Add("2. Export Worker")
        cboCatgeory.Items.Add("3. R & D Worker")
        cboCatgeory.Items.Add(IIf(RsCompany.Fields("COMPANY_CODE").Value = 16, "4. Apprentice", "4. Robo Trainee Staff"))
        If RsCompany.Fields("COMPANY_CODE").Value <> 16 Then
            cboCatgeory.Items.Add("Apprentice")
        End If

        Clear1()

        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmEmployeeCategoryMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Left = VB6.TwipsToPixelsX(20)
        Me.Top = VB6.TwipsToPixelsY(20)
        Me.Height = VB6.TwipsToPixelsY(7395)
        Me.Width = VB6.TwipsToPixelsX(10350)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmEmployeeCategoryMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsCategory = Nothing
        'frmDeptMaster = Nothing
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mSqlStr As String
        Dim mCatType As String

        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        Shw = True
        If Not RsCategory.EOF Then
            cboCatgeory.Text = IIf(IsDbNull(RsCategory.Fields("CATEGORY_DESC").Value), "", RsCategory.Fields("CATEGORY_DESC").Value)
            mCatType = IIf(IsDbNull(RsCategory.Fields("CATEGORY_TYPE").Value), "", RsCategory.Fields("CATEGORY_TYPE").Value)

            If mCatType = "S" Then
                optCategoryType(0).Checked = True
            Else
                optCategoryType(1).Checked = True
            End If
            FraCatType.Enabled = False

            If IsDbNull(RsCategory.Fields("POSTSAL_DEBITCODE").Value) Then
                txtSBDebit.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsCategory.Fields("POSTSAL_DEBITCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
                txtSBDebit.Text = MasterNo
            Else
                txtSBDebit.Text = ""
            End If

            If IsDbNull(RsCategory.Fields("POSTINC_DEBITCODE").Value) Then
                txtSIncDebit.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsCategory.Fields("POSTINC_DEBITCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
                txtSIncDebit.Text = MasterNo
            Else
                txtSIncDebit.Text = ""
            End If

            If IsDbNull(RsCategory.Fields("POSTBONUS_DEBITCODE").Value) Then
                txtSBonusDebit.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsCategory.Fields("POSTBONUS_DEBITCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
                txtSBonusDebit.Text = MasterNo
            Else
                txtSBonusDebit.Text = ""
            End If

            If IsDbNull(RsCategory.Fields("POSTLTC_DEBITCODE").Value) Then
                txtSLTCDebit.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsCategory.Fields("POSTLTC_DEBITCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
                txtSLTCDebit.Text = MasterNo
            Else
                txtSLTCDebit.Text = ""
            End If

            If IsDbNull(RsCategory.Fields("POSTSAL_CREDITCODE").Value) Then
                txtSBCredit.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsCategory.Fields("POSTSAL_CREDITCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
                txtSBCredit.Text = MasterNo
            Else
                txtSBCredit.Text = ""
            End If

            If IsDbNull(RsCategory.Fields("POSTINC_CREDITCODE").Value) Then
                txtSIncCredit.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsCategory.Fields("POSTINC_CREDITCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
                txtSIncCredit.Text = MasterNo
            Else
                txtSIncCredit.Text = ""
            End If

            If IsDbNull(RsCategory.Fields("POSTBONUS_CREDITCODE").Value) Then
                txtSBonusCredit.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsCategory.Fields("POSTBONUS_CREDITCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
                txtSBonusCredit.Text = MasterNo
            Else
                txtSBonusCredit.Text = ""
            End If

            If IsDbNull(RsCategory.Fields("POSTLTC_CREDITCODE").Value) Then
                txtSLTCCredit.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsCategory.Fields("POSTLTC_CREDITCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
                txtSLTCCredit.Text = MasterNo
            Else
                txtSLTCCredit.Text = ""
            End If

            If IsDbNull(RsCategory.Fields("POSTPF_ACCTCODE").Value) Then
                txtPFCredit.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsCategory.Fields("POSTPF_ACCTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
                txtPFCredit.Text = MasterNo
            Else
                txtPFCredit.Text = ""
            End If

            If IsDbNull(RsCategory.Fields("POSTESI_ACCTCODE").Value) Then
                txtESICredit.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsCategory.Fields("POSTESI_ACCTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
                txtESICredit.Text = MasterNo
            Else
                txtESICredit.Text = ""
            End If

            If IsDbNull(RsCategory.Fields("POSTWELFARE_ACCTCODE").Value) Then
                txtWelfare_GS.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsCategory.Fields("POSTWELFARE_ACCTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
                txtWelfare_GS.Text = MasterNo
            Else
                txtWelfare_GS.Text = ""
            End If

            If IsDbNull(RsCategory.Fields("POSTEL_ACCTCODE").Value) Then
                txtELPostingHead.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsCategory.Fields("POSTEL_ACCTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
                txtELPostingHead.Text = MasterNo
            Else
                txtELPostingHead.Text = ""
            End If

            If IsDbNull(RsCategory.Fields("POSTGR_ACCTCODE").Value) Then
                txtGRPostingHead.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsCategory.Fields("POSTGR_ACCTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
                txtGRPostingHead.Text = MasterNo
            Else
                txtGRPostingHead.Text = ""
            End If

            cboCatgeory.Enabled = False
        End If
        Shw = False
        ADDMode = False
        MODIFYMode = False
        If RsCategory.EOF = False Then
            xCategoryDesc = RsCategory.Fields("CATEGORY_DESC").Value
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsCategory, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            cboCatgeory_Validating(cboCatgeory, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim SqlStr As String = ""
        Dim mStaffDAccountCode As String
        Dim mStaffCAccountCode As String
        Dim mPostPFContCr As String
        Dim mPostESIContCr As String
        Dim mSqlStr As String
        Dim mSIncDebitCode As String
        Dim mSBonusDebitCode As String
        Dim mSLTCDebitCode As String
        Dim mSIncCreditCode As String
        Dim mSBonusCreditCode As String
        Dim mSLTCCreditCode As String
        Dim mWelfare_GS As String

        Dim mELAcctCode As String
        Dim mGRAcctCode As String
        Dim mCatType As String


        PubDBCn.BeginTrans()



        SqlStr = ""


        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        'DEBIT HEAD
        If MainClass.ValidateWithMasterTable((txtSBDebit.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            mStaffDAccountCode = MasterNo
        Else
            mStaffDAccountCode = ""
        End If

        If MainClass.ValidateWithMasterTable((txtSIncDebit.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            mSIncDebitCode = MasterNo
        Else
            mSIncDebitCode = ""
        End If

        If MainClass.ValidateWithMasterTable((txtSBonusDebit.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            mSBonusDebitCode = MasterNo
        Else
            mSBonusDebitCode = ""
        End If

        If MainClass.ValidateWithMasterTable((txtSLTCDebit.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            mSLTCDebitCode = MasterNo
        Else
            mSLTCDebitCode = ""
        End If

        'CREDIT HEAD
        If MainClass.ValidateWithMasterTable((txtSBCredit.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            mStaffCAccountCode = MasterNo
        Else
            mStaffCAccountCode = ""
        End If

        If MainClass.ValidateWithMasterTable((txtSIncCredit.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            mSIncCreditCode = MasterNo
        Else
            mSIncCreditCode = ""
        End If

        If MainClass.ValidateWithMasterTable((txtSBonusCredit.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            mSBonusCreditCode = MasterNo
        Else
            mSBonusCreditCode = ""
        End If

        If MainClass.ValidateWithMasterTable((txtSLTCCredit.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            mSLTCCreditCode = MasterNo
        Else
            mSLTCCreditCode = ""
        End If

        If MainClass.ValidateWithMasterTable((txtPFCredit.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            mPostPFContCr = MasterNo
        Else
            mPostPFContCr = ""
        End If


        If MainClass.ValidateWithMasterTable((txtESICredit.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            mPostESIContCr = MasterNo
        Else
            mPostESIContCr = ""
        End If


        If MainClass.ValidateWithMasterTable((txtWelfare_GS.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            mWelfare_GS = MasterNo
        Else
            mWelfare_GS = ""
        End If

        If MainClass.ValidateWithMasterTable((txtELPostingHead.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            mELAcctCode = MasterNo
        Else
            mELAcctCode = ""
        End If

        If MainClass.ValidateWithMasterTable((txtGRPostingHead.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            mGRAcctCode = MasterNo
        Else
            mGRAcctCode = ""
        End If

        If optCategoryType(0).Checked = True Then
            mCatType = "S"
        Else
            mCatType = "W"
        End If

        SqlStr = ""
        If ADDMode = True Then
            SqlStr = " INSERT INTO PAY_CATEGORY_MST ( " & vbCrLf & " COMPANY_CODE, CATEGORY_CODE, CATEGORY_DESC, CATEGORY_TYPE, " & vbCrLf & " POSTSAL_DEBITCODE, POSTINC_DEBITCODE, POSTBONUS_DEBITCODE, " & vbCrLf & " POSTLTC_DEBITCODE, POSTSAL_CREDITCODE, POSTINC_CREDITCODE, " & vbCrLf & " POSTBONUS_CREDITCODE, POSTLTC_CREDITCODE, POSTPF_ACCTCODE, " & vbCrLf & " POSTESI_ACCTCODE, POSTWELFARE_ACCTCODE,POSTEL_ACCTCODE,POSTGR_ACCTCODE ) VALUES (" & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & VB.Left(cboCatgeory.Text, 1) & "', '" & Trim(cboCatgeory.Text) & "', '" & mCatType & "'," & vbCrLf & " '" & mStaffDAccountCode & "', '" & mSIncDebitCode & "', '" & mSBonusDebitCode & "', " & vbCrLf & " '" & mSLTCDebitCode & "', '" & mStaffCAccountCode & "', '" & mSIncCreditCode & "'," & vbCrLf & " '" & mSBonusCreditCode & "', '" & mSLTCCreditCode & "', '" & mPostPFContCr & "'," & vbCrLf & " '" & mPostESIContCr & "', '" & mWelfare_GS & "','" & mELAcctCode & "','" & mGRAcctCode & "'" & vbCrLf & " )"
        Else
            SqlStr = "UPDATE  PAY_CATEGORY_MST SET CATEGORY_TYPE='" & mCatType & "'," & vbCrLf & " POSTSAL_DEBITCODE='" & mStaffDAccountCode & "', " & vbCrLf & " POSTINC_DEBITCODE='" & mSIncDebitCode & "'," & vbCrLf & " POSTBONUS_DEBITCODE='" & mSBonusDebitCode & "'," & vbCrLf & " POSTLTC_DEBITCODE='" & mSLTCDebitCode & "'," & vbCrLf & " POSTSAL_CREDITCODE='" & mStaffCAccountCode & "', " & vbCrLf & " POSTINC_CREDITCODE='" & mSIncCreditCode & "'," & vbCrLf & " POSTBONUS_CREDITCODE='" & mSBonusCreditCode & "'," & vbCrLf & " POSTLTC_CREDITCODE='" & mSLTCCreditCode & "'," & vbCrLf & " POSTPF_ACCTCODE ='" & mPostPFContCr & "', " & vbCrLf & " POSTESI_ACCTCODE ='" & mPostESIContCr & "', " & vbCrLf & " POSTWELFARE_ACCTCODE='" & mWelfare_GS & "'," & vbCrLf & " POSTEL_ACCTCODE ='" & mELAcctCode & "', " & vbCrLf & " POSTGR_ACCTCODE='" & mGRAcctCode & "'" & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY_DESC='" & MainClass.AllowSingleQuote(Trim(cboCatgeory.Text)) & "'"
        End If

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Update1 = True
        RsCategory.Requery()
        Exit Function
UpdatePart:
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        RsCategory.Requery()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        SqlStr = ""
        FieldsVarification = True
        If Trim(cboCatgeory.Text) = "" Then
            MsgInformation("Category is empty. Cannot Save")
            If cboCatgeory.Enabled = True Then cboCatgeory.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If cboCatgeory.SelectedIndex = -1 Then
            MsgInformation("Please Select category.")
            If cboCatgeory.Enabled = True Then cboCatgeory.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If optCategoryType(0).Checked = False And optCategoryType(1).Checked = False Then
            MsgInformation("Please select the Category Type. Cannot Save")
            If FraCatType.Enabled = True Then optCategoryType(0).Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Category or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And (RsCategory.EOF = 0 Or RsCategory.EOF = True) Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub settextlength()

        On Error GoTo ERR1

        txtSBDebit.Text = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtSIncDebit.Text = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtSBonusDebit.Text = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtSLTCDebit.Text = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtSBCredit.Text = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtSIncCredit.Text = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtSBonusCredit.Text = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtSLTCCredit.Text = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtPFCredit.Text = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtESICredit.Text = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtWelfare_GS.Text = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtELPostingHead.Text = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtGRPostingHead.Text = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)


        SqlStr = "SELECT CATEGORY_DESC,   " & vbCrLf & " GetAccountName (" & RsCompany.Fields("COMPANY_CODE").Value & ",POSTSAL_DEBITCODE) AS SAL_DEBIT, " & vbCrLf & " GetAccountName (" & RsCompany.Fields("COMPANY_CODE").Value & ",POSTINC_DEBITCODE) AS INC_DEBIT, " & vbCrLf & " GetAccountName (" & RsCompany.Fields("COMPANY_CODE").Value & ",POSTBONUS_DEBITCODE) AS BONUS_DEBIT, " & vbCrLf & " GetAccountName (" & RsCompany.Fields("COMPANY_CODE").Value & ",POSTLTC_DEBITCODE) AS LTC_DEBIT, " & vbCrLf & " GetAccountName (" & RsCompany.Fields("COMPANY_CODE").Value & ",POSTSAL_CREDITCODE) AS SAL_CREDIT, " & vbCrLf & " GetAccountName (" & RsCompany.Fields("COMPANY_CODE").Value & ",POSTINC_CREDITCODE) AS INC_CREDIT, " & vbCrLf & " GetAccountName (" & RsCompany.Fields("COMPANY_CODE").Value & ",POSTBONUS_CREDITCODE) AS BONUS_CREDIT, " & vbCrLf & " GetAccountName (" & RsCompany.Fields("COMPANY_CODE").Value & ",POSTLTC_CREDITCODE) AS LTC_CREDIT, " & vbCrLf & " GetAccountName (" & RsCompany.Fields("COMPANY_CODE").Value & ",POSTPF_ACCTCODE) AS PF_ACCTCODE, " & vbCrLf & " GetAccountName (" & RsCompany.Fields("COMPANY_CODE").Value & ",POSTESI_ACCTCODE) AS ESI_ACCTCODE, " & vbCrLf & " GetAccountName (" & RsCompany.Fields("COMPANY_CODE").Value & ",POSTWELFARE_ACCTCODE) AS WELFARE_ACCTCODE, " & vbCrLf & " GetAccountName (" & RsCompany.Fields("COMPANY_CODE").Value & ",POSTEL_ACCTCODE) AS ENCASH_ACCTCODE," & vbCrLf & " GetAccountName (" & RsCompany.Fields("COMPANY_CODE").Value & ",POSTGR_ACCTCODE) AS GRATUITY_ACCTCODE" & vbCrLf & " FROM PAY_CATEGORY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY CATEGORY_DESC  "

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)
            .set_ColWidth(2, 16)
            .set_ColWidth(3, 12)
            .set_ColWidth(4, 12)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""
        If Trim(cboCatgeory.Text) = "" Then Delete1 = False : Exit Function

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "PAY_CATEGORY_MST", (cboCatgeory.Text), RsCategory) = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "PAY_CATEGORY_MST", "CATEGORY_DESC  ", (cboCatgeory.Text)) = False Then GoTo DeleteErr

        SqlStr = " DELETE FROM PAY_CATEGORY_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY_DESC  ='" & MainClass.AllowSingleQuote((cboCatgeory.Text)) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsCategory.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsCategory.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "Designation Listing"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\Department.rpt"

        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub


    Private Sub cmdSBCSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSBCSearch.Click
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchMaster((txtSBCredit.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
            txtSBCredit.Text = AcName
        End If

        Exit Sub
    End Sub

    Private Sub cmdSBDSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSBDSearch.Click
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchMaster((txtSBDebit.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
            txtSBDebit.Text = AcName
        End If

        Exit Sub
    End Sub

    Private Sub cmdSBonusCSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSBonusCSearch.Click
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchMaster((txtSBonusCredit.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
            txtSBonusCredit.Text = AcName
        End If

        Exit Sub
    End Sub

    Private Sub cmdSBonusDSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSBonusDSearch.Click
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchMaster((txtSBonusDebit.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
            txtSBonusDebit.Text = AcName
        End If

        Exit Sub
    End Sub

    Private Sub cmdSIncCSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSIncCSearch.Click
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchMaster((txtSIncCredit.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
            txtSIncCredit.Text = AcName
        End If

        Exit Sub
    End Sub

    Private Sub cmdSIncDSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSIncDSearch.Click
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchMaster((txtSIncDebit.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
            txtSIncDebit.Text = AcName
        End If

        Exit Sub
    End Sub

    Private Sub cmdSLTCCSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSLTCCSearch.Click
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchMaster((txtSLTCCredit.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
            txtSLTCCredit.Text = AcName
        End If

        Exit Sub
    End Sub

    Private Sub cmdSLTCDSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSLTCDSearch.Click
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchMaster((txtSLTCDebit.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
            txtSLTCDebit.Text = AcName
        End If

        Exit Sub
    End Sub

    Private Sub cmdWelfare_GS_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdWelfare_GS.Click
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchMaster((txtWelfare_GS.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
            txtWelfare_GS.Text = AcName
        End If

        Exit Sub
    End Sub


    Private Sub txtELPostingHead_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtELPostingHead.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtELPostingHead_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtELPostingHead.DoubleClick
        cmdELSearch_Click(cmdELSearch, New System.EventArgs())
    End Sub

    Private Sub txtELPostingHead_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtELPostingHead.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtELPostingHead.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtELPostingHead_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtELPostingHead.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdELSearch_Click(cmdELSearch, New System.EventArgs())
        End If
    End Sub

    Private Sub txtELPostingHead_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtELPostingHead.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsACM As ADODB.Recordset

        If Trim(UCase(txtELPostingHead.Text)) = "" Then GoTo EventExitSub
        SqlStr = "Select SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE UPPER(trim(SUPP_CUST_NAME)) = '" & MainClass.AllowSingleQuote(Trim(UCase(txtELPostingHead.Text))) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF = False Then
            txtELPostingHead.Text = IIf(IsDbNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invaild Account Name.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtESICredit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtESICredit.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGRPostingHead_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGRPostingHead.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGRPostingHead_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGRPostingHead.DoubleClick
        cmdGRSearch_Click(cmdGRSearch, New System.EventArgs())
    End Sub

    Private Sub txtGRPostingHead_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGRPostingHead.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtGRPostingHead.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtGRPostingHead_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtGRPostingHead.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdGRSearch_Click(cmdGRSearch, New System.EventArgs())
        End If
    End Sub

    Private Sub txtGRPostingHead_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGRPostingHead.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsACM As ADODB.Recordset

        If Trim(UCase(txtGRPostingHead.Text)) = "" Then GoTo EventExitSub
        SqlStr = "Select SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE UPPER(trim(SUPP_CUST_NAME)) = '" & MainClass.AllowSingleQuote(Trim(UCase(txtGRPostingHead.Text))) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF = False Then
            txtGRPostingHead.Text = IIf(IsDbNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invaild Account Name.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPFCredit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPFCredit.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPFCredit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPFCredit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPFCredit.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSBCredit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSBCredit.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSBCredit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSBCredit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSBCredit.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSBDebit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSBDebit.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSBDebit_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSBDebit.DoubleClick
        cmdSBDSearch_Click(cmdSBDSearch, New System.EventArgs())
    End Sub

    Private Sub txtSBDebit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSBDebit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSBDebit.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSBDebit_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSBDebit.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdSBDSearch_Click(cmdSBDSearch, New System.EventArgs())
        End If
    End Sub

    Private Sub txtSBDebit_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSBDebit.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsACM As ADODB.Recordset

        If Trim(UCase(txtSBDebit.Text)) = "" Then GoTo EventExitSub
        SqlStr = "Select SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE UPPER(trim(SUPP_CUST_NAME)) = '" & MainClass.AllowSingleQuote(Trim(UCase(txtSBDebit.Text))) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF = False Then
            txtSBDebit.Text = IIf(IsDbNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invaild Account Name.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSBCredit_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSBCredit.DoubleClick
        cmdSBCSearch_Click(cmdSBCSearch, New System.EventArgs())
    End Sub

    Private Sub txtSBCredit_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSBCredit.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdSBCSearch_Click(cmdSBCSearch, New System.EventArgs())
        End If
    End Sub

    Private Sub txtSBCredit_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSBCredit.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsACM As ADODB.Recordset

        If Trim(UCase(txtSBCredit.Text)) = "" Then GoTo EventExitSub
        SqlStr = "Select SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE UPPER(trim(SUPP_CUST_NAME)) = '" & MainClass.AllowSingleQuote(Trim(UCase(txtSBCredit.Text))) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF = False Then
            txtSBCredit.Text = IIf(IsDbNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invaild Account Name.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtPFCredit_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPFCredit.DoubleClick
        cmdPFCSearch_Click(cmdPFCSearch, New System.EventArgs())
    End Sub

    Private Sub txtPFCredit_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPFCredit.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdPFCSearch_Click(cmdPFCSearch, New System.EventArgs())
        End If
    End Sub

    Private Sub txtPFCredit_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPFCredit.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsACM As ADODB.Recordset

        If Trim(UCase(txtPFCredit.Text)) = "" Then GoTo EventExitSub
        SqlStr = "Select SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE UPPER(trim(SUPP_CUST_NAME)) = '" & MainClass.AllowSingleQuote(Trim(UCase(txtPFCredit.Text))) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF = False Then
            txtPFCredit.Text = IIf(IsDbNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invaild Account Name.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdPFCSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPFCSearch.Click
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchMaster((txtPFCredit.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
            txtPFCredit.Text = AcName
        End If

        Exit Sub
    End Sub
    Private Sub txtESICredit_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtESICredit.DoubleClick
        cmdESICSearch_Click(cmdESICSearch, New System.EventArgs())
    End Sub

    Private Sub txtESICredit_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtESICredit.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdESICSearch_Click(cmdESICSearch, New System.EventArgs())
        End If
    End Sub

    Private Sub txtESICredit_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtESICredit.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsACM As ADODB.Recordset

        If Trim(UCase(txtESICredit.Text)) = "" Then GoTo EventExitSub
        SqlStr = "Select SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE UPPER(trim(SUPP_CUST_NAME)) = '" & MainClass.AllowSingleQuote(Trim(UCase(txtESICredit.Text))) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF = False Then
            txtESICredit.Text = IIf(IsDbNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invaild Account Name.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdESICSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdESICSearch.Click
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchMaster((txtESICredit.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
            txtESICredit.Text = AcName
        End If

        Exit Sub
    End Sub
    Private Sub txtSBonusCredit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSBonusCredit.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSBonusCredit_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSBonusCredit.DoubleClick
        cmdSBonusCSearch_Click(cmdSBonusCSearch, New System.EventArgs())
    End Sub

    Private Sub txtSBonusCredit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSBonusCredit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSBonusCredit.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSBonusCredit_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSBonusCredit.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdSBonusCSearch_Click(cmdSBonusCSearch, New System.EventArgs())
        End If
    End Sub

    Private Sub txtSBonusCredit_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSBonusCredit.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsACM As ADODB.Recordset

        If Trim(UCase(txtSBonusCredit.Text)) = "" Then GoTo EventExitSub
        SqlStr = "Select SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE UPPER(trim(SUPP_CUST_NAME)) = '" & MainClass.AllowSingleQuote(Trim(UCase(txtSBonusCredit.Text))) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF = False Then
            txtSBonusCredit.Text = IIf(IsDbNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invaild Account Name.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSBonusDebit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSBonusDebit.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSBonusDebit_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSBonusDebit.DoubleClick
        cmdSBonusDSearch_Click(cmdSBonusDSearch, New System.EventArgs())
    End Sub

    Private Sub txtSBonusDebit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSBonusDebit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSBonusDebit.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSBonusDebit_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSBonusDebit.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdSBonusDSearch_Click(cmdSBonusDSearch, New System.EventArgs())
        End If
    End Sub

    Private Sub txtSBonusDebit_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSBonusDebit.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsACM As ADODB.Recordset

        If Trim(UCase(txtSBonusDebit.Text)) = "" Then GoTo EventExitSub
        SqlStr = "Select SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE UPPER(trim(SUPP_CUST_NAME)) = '" & MainClass.AllowSingleQuote(Trim(UCase(txtSBonusDebit.Text))) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF = False Then
            txtSBonusDebit.Text = IIf(IsDbNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invaild Account Name.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub



    Private Sub txtSIncCredit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSIncCredit.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSIncCredit_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSIncCredit.DoubleClick
        cmdSIncCSearch_Click(cmdSIncCSearch, New System.EventArgs())
    End Sub

    Private Sub txtSIncCredit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSIncCredit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSIncCredit.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSIncCredit_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSIncCredit.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdSIncCSearch_Click(cmdSIncCSearch, New System.EventArgs())
        End If
    End Sub

    Private Sub txtSIncCredit_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSIncCredit.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsACM As ADODB.Recordset

        If Trim(UCase(txtSIncCredit.Text)) = "" Then GoTo EventExitSub
        SqlStr = "Select SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE UPPER(trim(SUPP_CUST_NAME)) = '" & MainClass.AllowSingleQuote(Trim(UCase(txtSIncCredit.Text))) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF = False Then
            txtSIncCredit.Text = IIf(IsDbNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invaild Account Name.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSIncDebit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSIncDebit.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSIncDebit_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSIncDebit.DoubleClick
        cmdSIncDSearch_Click(cmdSIncDSearch, New System.EventArgs())
    End Sub

    Private Sub txtSIncDebit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSIncDebit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSIncDebit.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSIncDebit_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSIncDebit.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdSIncDSearch_Click(cmdSIncDSearch, New System.EventArgs())
        End If
    End Sub

    Private Sub txtSIncDebit_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSIncDebit.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsACM As ADODB.Recordset

        If Trim(UCase(txtSIncDebit.Text)) = "" Then GoTo EventExitSub
        SqlStr = "Select SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE UPPER(trim(SUPP_CUST_NAME)) = '" & MainClass.AllowSingleQuote(Trim(UCase(txtSIncDebit.Text))) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF = False Then
            txtSIncDebit.Text = IIf(IsDbNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invaild Account Name.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSLTCCredit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSLTCCredit.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSLTCCredit_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSLTCCredit.DoubleClick
        cmdSLTCCSearch_Click(cmdSLTCCSearch, New System.EventArgs())
    End Sub

    Private Sub txtSLTCCredit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSLTCCredit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSLTCCredit.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSLTCCredit_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSLTCCredit.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdSLTCCSearch_Click(cmdSLTCCSearch, New System.EventArgs())
        End If
    End Sub

    Private Sub txtSLTCCredit_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSLTCCredit.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsACM As ADODB.Recordset

        If Trim(UCase(txtSLTCCredit.Text)) = "" Then GoTo EventExitSub
        SqlStr = "Select SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE UPPER(trim(SUPP_CUST_NAME)) = '" & MainClass.AllowSingleQuote(Trim(UCase(txtSLTCCredit.Text))) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF = False Then
            txtSLTCCredit.Text = IIf(IsDbNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invaild Account Name.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSLTCDebit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSLTCDebit.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSLTCDebit_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSLTCDebit.DoubleClick
        cmdSLTCDSearch_Click(cmdSLTCDSearch, New System.EventArgs())
    End Sub

    Private Sub txtSLTCDebit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSLTCDebit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSLTCDebit.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSLTCDebit_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSLTCDebit.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdSLTCDSearch_Click(cmdSLTCDSearch, New System.EventArgs())
        End If
    End Sub

    Private Sub txtSLTCDebit_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSLTCDebit.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsACM As ADODB.Recordset

        If Trim(UCase(txtSLTCDebit.Text)) = "" Then GoTo EventExitSub
        SqlStr = "Select SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE UPPER(trim(SUPP_CUST_NAME)) = '" & MainClass.AllowSingleQuote(Trim(UCase(txtSLTCDebit.Text))) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF = False Then
            txtSLTCDebit.Text = IIf(IsDbNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invaild Account Name.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtWelfare_GS_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWelfare_GS.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtWelfare_GS_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWelfare_GS.DoubleClick
        cmdWelfare_GS_Click(cmdWelfare_GS, New System.EventArgs())
    End Sub
    Private Sub txtWelfare_GS_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWelfare_GS.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtWelfare_GS.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtWelfare_GS_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtWelfare_GS.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdWelfare_GS_Click(cmdWelfare_GS, New System.EventArgs())
        End If
    End Sub
    Private Sub txtWelfare_GS_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWelfare_GS.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsACM As ADODB.Recordset

        If Trim(UCase(txtWelfare_GS.Text)) = "" Then GoTo EventExitSub
        SqlStr = "Select SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE UPPER(trim(SUPP_CUST_NAME)) = '" & MainClass.AllowSingleQuote(Trim(UCase(txtWelfare_GS.Text))) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF = False Then
            txtWelfare_GS.Text = IIf(IsDbNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invaild Account Name.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
