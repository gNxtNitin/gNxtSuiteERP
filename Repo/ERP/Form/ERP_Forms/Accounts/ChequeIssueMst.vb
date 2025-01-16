Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmChequeIssueMst
    Inherits System.Windows.Forms.Form
    Dim RsChq As ADODB.Recordset
    ''''Private PvtDBCn As ADODB.Connection					
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim mChequeNo As String

    Dim FormActive As Boolean
    Dim MasterNo As Object
    Dim Sqlstr As String
    Private Sub ViewGrid()
        On Error GoTo ErrorPart
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
        MainClass.ButtonStatus(Me, XRIGHT, RsChq, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()
        txtBankName.Text = ""
        txtChequeNoFrom.Text = ""
        txtChequeNoTo.Text = ""
        txtPartyName.Text = ""
        txtAmount.Text = ""
        txtVNo.Text = ""
        txtVDate.Text = ""
        FraChqDetail.Enabled = False

        chkTo.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkTo.Enabled = True
        txtChequeNoTo.Enabled = False

        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStatus.Enabled = False
        txtBankName.Enabled = True
        cmdSearch.Enabled = True

        MainClass.ButtonStatus(Me, XRIGHT, RsChq, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkTo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkTo.CheckStateChanged
        txtChequeNoTo.Enabled = IIf(chkTo.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click
        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Cheque Already Issue, so cann't be Modified.")
                Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            txtBankName.Enabled = False
            txtChequeNoFrom.Enabled = False
            cmdSearch.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsChq, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo ERR1
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            '        txtChequeNoFromFrom.SetFocus					
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        Dim mBankCode As String

        Sqlstr = ""

        If MainClass.ValidateWithMasterTable(txtBankName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = True Then
            mBankCode = MasterNo
        Else
            MsgBox("Bank Does Not Exist In Master.")
            Exit Function
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "FIN_CHEQUE_MST", (txtChequeNoFrom.Text), RsChq, "CHEQUE_NO", "D") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "FIN_CHEQUE_MST", "BANKCODE || ':' || CHEQUE_NO", mBankCode & ":" & txtChequeNoFrom.Text) = False Then GoTo DeleteErr

        Sqlstr = "DELETE FROM FIN_CHEQUE_MST " & vbCrLf _
            & "WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & "AND BANKCODE='" & MainClass.AllowSingleQuote(mBankCode) & "' " & vbCrLf _
            & "AND CHEQUE_NO='" & txtChequeNoFrom.Text & "'"

        PubDBCn.Execute(Sqlstr)
        PubDBCn.CommitTrans()
        RsChq.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans() ''					
        RsChq.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This No.", MsgBoxStyle.Information)
            Exit Function
        End If
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cheque Already Issue, so cann't be deleted.")
            Exit Sub
        End If

        If txtChequeNoFrom.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsChq.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.					
                If Delete1() = False Then GoTo DelErrPart
                If RsChq.EOF = True Then
                    Clear1()
                Else
                    Clear1()
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        ErrorMsg("Record Not Deleted", "DELETE", MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo SearchError
        If MainClass.SearchGridMaster(txtBankName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = True Then
            txtBankName.Text = AcName
            txtBankName_Validating(txtBankName, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
SearchError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmChequeIssueMst_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmChequeIssueMst_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        Clear1()
        SprdView.Row = SprdView.ActiveRow
        SprdView.Col = 1
        txtBankName.Text = Trim(SprdView.Text)
        SprdView.Col = 2
        txtChequeNoFrom.Text = Trim(SprdView.Text)
        txtChequeNoFrom_Validating(txtChequeNoFrom, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtBankName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.DoubleClick
        cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Private Sub txtBankName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBankName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBankName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Private Sub txtBankName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBankName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim mBankCode As String

        Sqlstr = ""
        If Trim(txtBankName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtBankName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = True Then
            mBankCode = MasterNo
        Else
            MsgBox("Bank Does Not Exist In Master.")
            Cancel = True
            Exit Sub
        End If

        If Trim(txtChequeNoFrom.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsChq.EOF = False Then mChequeNo = RsChq.Fields("CHEQUE_NO").Value

        Sqlstr = "SELECT * FROM FIN_CHEQUE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND BANKCODE='" & MainClass.AllowSingleQuote(mBankCode) & "'" & vbCrLf _
            & " AND CHEQUE_NO='" & txtChequeNoFrom.Text & "'"


        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChq, ADODB.LockTypeEnum.adLockReadOnly)

        If RsChq.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Cheque No. Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                Sqlstr = ""
                Sqlstr = "SELECT * FROM FIN_CHEQUE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND BANKCODE='" & MainClass.AllowSingleQuote(mBankCode) & "'" & vbCrLf & " AND CHEQUE_NO='" & mChequeNo & "'"

                MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChq, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtChequeNoFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChequeNoFrom.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtChequeNoFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChequeNoFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim mBankCode As String

        Sqlstr = ""
        If Trim(txtChequeNoFrom.Text) = "" Then GoTo EventExitSub

        If Trim(txtBankName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtBankName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = True Then
            mBankCode = MasterNo
        Else
            MsgBox("Bank Does Not Exist In Master.")
            Cancel = True
            Exit Sub
        End If


        If MODIFYMode = True And RsChq.EOF = False Then mChequeNo = RsChq.Fields("CHEQUE_NO").Value

        Sqlstr = "SELECT * FROM FIN_CHEQUE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND BANKCODE='" & MainClass.AllowSingleQuote(mBankCode) & "'" & vbCrLf _
            & " AND CHEQUE_NO='" & txtChequeNoFrom.Text & "'"


        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChq, ADODB.LockTypeEnum.adLockReadOnly)

        If RsChq.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Cheque No. Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                Sqlstr = ""
                Sqlstr = "SELECT * FROM FIN_CHEQUE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND BANKCODE='" & MainClass.AllowSingleQuote(mBankCode) & "'" & vbCrLf & " AND CHEQUE_NO='" & mChequeNo & "'"

                MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChq, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub frmChequeIssueMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From FIN_CHEQUE_MST Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChq, ADODB.LockTypeEnum.adLockReadOnly)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        AssignGrid(False)
        SetTextLengths()
        Clear1()
        FormActive = True
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmChequeIssueMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '''Set PvtDBCn = New ADODB.Connection					
        '''PvtDBCn.Open StrConn					

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CmdView.Text = ConCmdGridViewCaption
        ADDMode = False
        MODIFYMode = False
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(5220)
        'Me.Width = VB6.TwipsToPixelsX(8265)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmChequeIssueMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsChq = Nothing
        RsChq.Close()
    End Sub
    Private Sub Show1()
        On Error GoTo ShowErrPart
        Dim mBankCode As String
        Dim mBankName As String
        Dim mPVMkey As String

        If Not RsChq.EOF Then
            mBankCode = IIf(IsDBNull(RsChq.Fields("BANKCODE").Value), "", RsChq.Fields("BANKCODE").Value)

            If MainClass.ValidateWithMasterTable(mBankCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = True Then
                mBankName = MasterNo
            Else
                MsgBox("Bank Does Not Exist In Master.")
                Exit Sub
            End If

            txtBankName.Text = mBankName
            txtChequeNoFrom.Text = IIf(IsDBNull(RsChq.Fields("CHEQUE_NO").Value), "", RsChq.Fields("CHEQUE_NO").Value)
            chkStatus.CheckState = IIf(RsChq.Fields("CHEQUE_STATUS").Value = "O", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            mChequeNo = IIf(IsDBNull(RsChq.Fields("CHEQUE_NO").Value), "", RsChq.Fields("CHEQUE_NO").Value)
            mPVMkey = IIf(IsDBNull(RsChq.Fields("VMkey").Value), "", RsChq.Fields("VMkey").Value)

            Call ShowDetail1(mPVMkey)

            txtBankName.Enabled = False
            txtChequeNoFrom.Enabled = True
            cmdSearch.Enabled = False
            chkTo.Enabled = False

        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsChq, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume					
    End Sub
    Private Sub ShowDetail1(ByRef mPVMkey As String)
        On Error GoTo ShowErrPart
        Dim Sqlstr As String
        Dim RsTemp As ADODB.Recordset

        Sqlstr = "SELECT IH.VNO, IH.VDATE, MAX(CMST.SUPP_CUST_NAME) SUPP_CUST_NAME, ABS(SUM(AMOUNT * DECODE(DC,'D',1,-1))) AS AMOUNT" & vbCrLf & " FROM FIN_VOUCHER_HDR IH, FIN_VOUCHER_DET ID, FIN_SUPP_CUST_MST CMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND ID.ACCOUNTCODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.MKEY='" & mPVMkey & "'"

        Sqlstr = Sqlstr & vbCrLf & " GROUP BY IH.VNO, IH.VDATE"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If Not RsTemp.EOF Then
            txtVNo.Text = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
            txtVDate.Text = IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value)
            txtPartyName.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
            txtAmount.Text = IIf(IsDBNull(RsTemp.Fields("Amount").Value), "", RsTemp.Fields("Amount").Value)
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsChq, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume					
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtChequeNoFrom_Validating(txtChequeNoFrom, New System.ComponentModel.CancelEventArgs(False))
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
        Dim I As Double
        Dim mStatus As String
        Dim mBankCode As String
        Dim mChequeNoStr As String
        Dim mChequeLen As Integer
        Dim xFormat As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O")

        If MainClass.ValidateWithMasterTable(txtBankName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = True Then
            mBankCode = MasterNo
        End If

        Sqlstr = ""
        mChequeLen = Len(txtChequeNoFrom.Text)
        xFormat = New String("0", mChequeLen)
        mChequeNo = CStr(Val(txtChequeNoFrom.Text))

        If ADDMode = True Then
            If chkTo.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mChequeNoStr = VB6.Format(mChequeNo, xFormat)
                Sqlstr = "INSERT INTO FIN_CHEQUE_MST (" & vbCrLf _
                        & " COMPANY_CODE, BANKCODE, CHEQUE_NO, CHEQUE_STATUS,  " & vbCrLf _
                        & " ADDUSER, ADDDATE, MODUSER, MODDATE " & vbCrLf _
                        & " ) VALUES ( " & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & mBankCode & "', " & vbCrLf _
                        & " '" & mChequeNoStr & "', '" & mStatus & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
                PubDBCn.Execute(Sqlstr)
            Else
                For I = CInt(mChequeNo) To Val(txtChequeNoTo.Text)

                    mChequeNoStr = VB6.Format(I, xFormat)
                    Sqlstr = "INSERT INTO FIN_CHEQUE_MST (" & vbCrLf _
                        & " COMPANY_CODE, BANKCODE, CHEQUE_NO, CHEQUE_STATUS,  " & vbCrLf _
                        & " ADDUSER, ADDDATE, MODUSER, MODDATE " & vbCrLf _
                        & " ) VALUES ( " & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & mBankCode & "', " & vbCrLf _
                        & " '" & mChequeNoStr & "', '" & mStatus & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
                    PubDBCn.Execute(Sqlstr)
                Next
            End If
        Else
            mChequeNoStr = VB6.Format(mChequeNo, xFormat)
            Sqlstr = " UPDATE FIN_CHEQUE_MST  SET " & vbCrLf & " BANKCODE='" & mBankCode & "', " & vbCrLf & " CHEQUE_STATUS='" & mStatus & "', " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CHEQUE_NO= '" & mChequeNoStr & "'"
            PubDBCn.Execute(Sqlstr)
        End If
UpdatePart:

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans() ''					
        RsChq.Requery() '''.Refresh					
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtChequeNoFrom.MaxLength = RsChq.Fields("CHEQUE_NO").DefinedSize
        txtChequeNoTo.MaxLength = RsChq.Fields("CHEQUE_NO").DefinedSize
        txtBankName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If

        If MODIFYMode = True And RsChq.EOF = True Then Exit Function

        If Trim(txtBankName.Text) = "" Then
            MsgInformation("Cheque No From is empty. Cannot Save")
            txtChequeNoFrom.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtBankName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = False Then
            MsgBox("Bank Does Not Exist In Master.")
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtChequeNoFrom.Text) = "" Then
            MsgInformation("Cheque No From is empty. Cannot Save")
            txtChequeNoFrom.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If chkTo.CheckState = System.Windows.Forms.CheckState.Checked Then
            If Trim(txtChequeNoTo.Text) = "" Then
                MsgInformation(" Cheque No To is empty. Cannot Save")
                txtChequeNoTo.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        Dim Sqlstr As String

        Sqlstr = ""

        Sqlstr = " SELECT B.SUPP_CUST_NAME BANK_NAME, CHEQUE_NO, " & vbCrLf & " DECODE(CHEQUE_STATUS,'O','OPEN','CLOSED') AS STATUS" & vbCrLf & " FROM FIN_CHEQUE_MST A, FIN_SUPP_CUST_MST B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf & " AND A.BANKCODE=B.SUPP_CUST_CODE"

        MainClass.AssignDataInSprd8(Sqlstr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()
        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 35)
            .set_ColWidth(2, 12)
            .set_ColWidth(3, 12)
            '        .ColWidth(4) = 12					
            '        .ColWidth(5) = 12					
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '''OperationModeSingle					
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        Dim mTitle As String
        On Error GoTo ERR1
        mTitle = ""
        Report1.Reset()
        mTitle = "Invoive Type"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\InvType.rpt"
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    'Private Sub txtChequeNoFrom_Change()					
    '    MainClass.SaveStatus Me, ADDMode, MODIFYMode					
    'End Sub					
    '					
    'Private Sub txtChequeNoFrom_KeyPress(KeyAscii As Integer)					
    '    KeyAscii = MainClass.UpperCase(KeyAscii, txtChequeNoFrom)					
    'End Sub					

    Private Sub txtChequeNoTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChequeNoTo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChequeNoTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChequeNoTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtChequeNoTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
