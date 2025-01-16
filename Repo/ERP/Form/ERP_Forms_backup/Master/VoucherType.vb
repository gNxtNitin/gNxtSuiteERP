Option Strict Off
Option Explicit On
Friend Class frmVoucherType
   Inherits System.Windows.Forms.Form
   Dim RsVType As ADODB.Recordset ''ADODB.Recordset			
   'Private PvtDBCn As ADODB.Connection			
   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String
   Dim FormActive As Boolean
   Dim mVType As String
   Private Sub FillBookCombo()
      On Error GoTo ErrPart

      cboBookName.Items.Clear()

      cboBookName.Items.Add("Cash")
      cboBookName.Items.Add("Bank")
      cboBookName.Items.Add("PDC")
      cboBookName.Items.Add("Journal")
      cboBookName.Items.Add("Contra")
      cboBookName.Items.Add("Debit Note")
      cboBookName.Items.Add("Credit Note")
      '    cboBookName.AddItem "Sale"		
      '    cboBookName.AddItem "Purchase"		
      '    cboBookName.AddItem "Opening"		
      cboBookName.SelectedIndex = -1

      Exit Sub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
   End Sub
   Private Function GetBookType(ByRef mBookName As String) As String
      On Error GoTo ErrPart
      GetBookType = ""
      Select Case mBookName
         Case "Cash"
            GetBookType = ConCashBook
         Case "Bank"
            GetBookType = ConBankBook
         Case "PDC"
            GetBookType = ConPDCBook
         Case "Journal"
            GetBookType = ConJournalBook
         Case "Contra"
            GetBookType = ConContraBook
         Case "Debit Note"
            GetBookType = ConDebitNoteBook
         Case "Credit Note"
            GetBookType = ConCreditNoteBook
         Case "Sale"
            GetBookType = ConSaleBook
         Case "Purchase"
            GetBookType = ConPurchaseBook
         Case "Good Recepit"
            GetBookType = ConGRBook
         Case "Opening"
            GetBookType = ConOpeningBook
      End Select
      Exit Function
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

   End Function
   Private Function SetBookType(ByRef mBookType As String) As String
      On Error GoTo ErrPart
      SetBookType = ""
      Select Case mBookType
         Case ConCashBook
            SetBookType = "Cash"
         Case ConBankBook
            SetBookType = "Bank"
         Case ConPDCBook
            SetBookType = "PDC"
         Case ConJournalBook
            SetBookType = "Journal"
         Case ConContraBook
            SetBookType = "Contra"
         Case ConDebitNoteBook
            SetBookType = "Debit Note"
         Case ConCreditNoteBook
            SetBookType = "Credit Note"
         Case ConSaleBook
            SetBookType = "Sale"
         Case ConPurchaseBook
            SetBookType = "Purchase"
         Case ConGRBook
            SetBookType = "Good Recepit"
         Case ConOpeningBook
            SetBookType = "Opening"
      End Select
      Exit Function
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

   End Function
   Private Sub ViewGrid()

      Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
      If CmdView.Text = ConCmdGridViewCaption Then
         CmdView.Text = ConCmdViewCaption
         MainClass.ClearGrid(SprdView)
         AssignGrid(True)
         '        ADataMain.Refresh				
         FormatSprdView()
         SprdView.Refresh()
         SprdView.Focus()
         SprdView.BringToFront()
      Else
         CmdView.Text = ConCmdGridViewCaption
         SprdView.SendToBack()
      End If
      MainClass.ButtonStatus(Me, XRIGHT, RsVType, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
      Me.Cursor = System.Windows.Forms.Cursors.Default
   End Sub
   Private Sub Show1()

      On Error GoTo ShowErrPart

      If RsVType.EOF = False Then
         mVType = RsVType.Fields("VTYPE").Value
         txtVType.Text = IIf(IsDbNull(RsVType.Fields("VTYPE").Value), "", RsVType.Fields("VTYPE").Value)
         txtVName.Text = IIf(IsDbNull(RsVType.Fields("VNAME").Value), "", RsVType.Fields("VNAME").Value)

         chkHOVoucher.CheckState = IIf(RsVType.Fields("FOR_HO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

         cboBookName.Text = SetBookType(IIf(IsDbNull(RsVType.Fields("BOOKTYPE").Value), "", RsVType.Fields("BOOKTYPE").Value))
         cboBookName.Enabled = False
         txtVType.Enabled = False

      End If

      ADDMode = False
      MODIFYMode = False
      MainClass.ButtonStatus(Me, XRIGHT, RsVType, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
      Exit Sub
ShowErrPart:
      MsgBox(Err.Description)
      ' Resume				
   End Sub

   Private Sub Clear1()

      txtVType.Text = ""
      txtVName.Text = ""
      cboBookName.SelectedIndex = -1
      cboBookName.Enabled = True
      txtVType.Enabled = True
      chkHOVoucher.CheckState = System.Windows.Forms.CheckState.Unchecked

      Call AutoCompleteSearch("FIN_VOUCHERTYPE_MST", "VTYPE", "", TxtVType)


      MainClass.ButtonStatus(Me, XRIGHT, RsVType, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
   End Sub
   Private Function FieldsVerification() As Boolean
      On Error GoTo FieldsVerificationErrpart

        Dim SqlStr As String = ""

        FieldsVerification = True
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVerification = False
            Exit Function
        End If

        If TxtVType.Text = "" Then
            FieldsVerification = False
            MsgInformation("Voucher Type Missing")
            TxtVType.Focus()
            FieldsVerification = False
            Exit Function
        End If

        If txtVName.Text = "" Then
            FieldsVerification = False
            MsgInformation("Voucher Name Missing")
            txtVName.Focus()
            FieldsVerification = False
            Exit Function
        End If

        If cboBookName.Text = "" Then
            FieldsVerification = False
            MsgInformation("Book Name Missing")
            cboBookName.Focus()
            FieldsVerification = False
            Exit Function
        End If

        Select Case cboBookName.Text

            Case "Cash"
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='1'"

                If MainClass.ValidateWithMasterTable(txtVName.Text, "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                    MsgInformation("Invalid Voucher Name")
                    txtVName.Focus()
                    FieldsVerification = False
                    Exit Function
                End If


            Case "Bank", "PDC"
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'"

                If MainClass.ValidateWithMasterTable(txtVName.Text, "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                    MsgInformation("Invalid Voucher Name")
                    txtVName.Focus()
                    FieldsVerification = False
                    Exit Function
                End If


        End Select



        Exit Function
FieldsVerificationErrpart:
        MsgBox(Err.Description)
        FieldsVerification = False
    End Function

    Private Sub chkHOVoucher_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkHOVoucher.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            If TxtVType.Enabled = True Then TxtVType.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click

        On Error GoTo DelErrPart
        Dim SqlStr As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If TxtVType.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            MsgInformation("Cann't be Delete.")
            Exit Sub
        End If
        If RsVType.EOF Then Exit Sub
        If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then

            If InsertIntoDelAudit(PubDBCn, "FIN_VOUCHERTYPE_MST", (TxtVType.Text), RsVType) = False Then GoTo DelErrPart
            If InsertIntoDeleteTrn(PubDBCn, "FIN_VOUCHERTYPE_MST", "VTYPE", (TxtVType.Text)) = False Then GoTo DelErrPart

            SqlStr = " Delete from FIN_VOUCHERTYPE_MST " & vbCrLf & " WHERE VTYPE='" & MainClass.AllowSingleQuote(TxtVType.Text) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            PubDBCn.Execute(SqlStr)
            PubDBCn.CommitTrans()
            RsVType.Requery() ''.Refresh				
            Clear1()
        End If
        Exit Sub
DelErrPart:
        'Resume				
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''				
        RsVType.Requery() ''.Refresh				
        'PubDBCn.Errors.Clear				

    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsVType, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mBookType As String
        Dim mFOR_HO As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xCompanyCode As Long

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mBookType = GetBookType((cboBookName.Text))

        mFOR_HO = IIf(chkHOVoucher.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Or mBookType = ConBankBook Or mBookType = ConPDCBook Then
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        Else
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                If ADDMode = True Then
                    SqlStr = ""
                    SqlStr = " INSERT INTO FIN_VOUCHERTYPE_MST ( " & vbCrLf _
                            & " COMPANY_CODE, VTYPE,  " & vbCrLf _
                            & " VNAME, BOOKTYPE,FOR_HO ) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & xCompanyCode & ",'" & MainClass.AllowSingleQuote(TxtVType.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtVName.Text) & "', " & vbCrLf _
                            & " '" & mBookType & "','O' )"
                Else
                    SqlStr = ""
                    SqlStr = " UPDATE FIN_VOUCHERTYPE_MST SET  " & vbCrLf _
                            & " VNAME= '" & MainClass.AllowSingleQuote(txtVName.Text) & "' ," & vbCrLf _
                            & " BOOKTYPE= '" & mBookType & "' " & vbCrLf _
                            & " Where COMPANY_CODE=" & xCompanyCode & "" & vbCrLf _
                            & " AND VTYPE = '" & mVType & "' "
                End If
                PubDBCn.Execute(SqlStr)
                RsTemp.MoveNext()
            Loop
        End If

        PubDBCn.CommitTrans()

        Update1 = True
        Exit Function
ErrPart:
        'Resume				
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Update1 = False
        PubDBCn.RollbackTrans() ''				
        PubDBCn.Errors.Clear()
        RsVType.Requery()
    End Function

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo SaveErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVerification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            TxtVType_Validating(TxtVType, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record Not Saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
SaveErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster(TxtVType.Text, "FIN_VOUCHERTYPE_MST", "VTYPE", "VNAME", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtVType.Text = AcName
            TxtVType_Validating(TxtVType, New System.ComponentModel.CancelEventArgs(True))
        End If
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ViewGrid()
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
    End Sub

    Private Sub frmVoucherType_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        AssignGrid(False)
        SqlStr = "Select * From FIN_VOUCHERTYPE_MST WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVType, ADODB.LockTypeEnum.adLockReadOnly)
        Call SetTextLength()
        Clear1()

        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '   Resume				
    End Sub

    Private Sub frmVoucherType_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmVoucherType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        TxtVType.Text = Trim(SprdView.Text)
        TxtVType_Validating(TxtVType, New System.ComponentModel.CancelEventArgs(True))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtVName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVName.DoubleClick
        SearchAccount()
    End Sub

    Private Sub txtVName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            SearchAccount()
        End If
    End Sub

    Private Sub txtVName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtVName.Text) = "" Then GoTo EventExitSub

        Select Case cboBookName.Text

            Case "Cash"
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " And SUPP_CUST_TYPE='1'"
                If MainClass.ValidateWithMasterTable(txtVName.Text, "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                    MsgInformation("Invalid Cash Account Name")
                    Cancel = True
                End If
            Case "Bank"
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND SUPP_CUST_TYPE='2'"
                If MainClass.ValidateWithMasterTable(txtVName.Text, "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                    MsgInformation("Invalid Bank Account Name")
                    Cancel = True
                End If
            Case "PDC"
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND SUPP_CUST_TYPE='2'"
                If MainClass.ValidateWithMasterTable(txtVName.Text, "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                    MsgInformation("Invalid Bank Account Name")
                    Cancel = True
                End If

        End Select


        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtVType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtVType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtVType.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtVType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtVType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtVType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtVType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdsearch_Click(cmdsearch, New System.EventArgs())
        End If
    End Sub
    Private Sub TxtVType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtVType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(TxtVType.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsVType.EOF = False Then mVType = RsVType.Fields("VTYPE").Value

        SqlStr = "Select * from FIN_VOUCHERTYPE_MST WHERE " & vbCrLf & " VTYPE='" & MainClass.AllowSingleQuote(Trim(TxtVType.Text)) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVType, ADODB.LockTypeEnum.adLockReadOnly)
        If RsVType.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Voucher Type Does Not Exist, Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * from FIN_VOUCHERTYPE_MST WHERE " & vbCrLf & " VTYPE='" & mVType & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVType, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtVName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmVoucherType_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim ii As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection				
        ''PvtDBCn.Open StrConn				
        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CmdView.Text = ConCmdGridViewCaption
        FillBookCombo()
        ADDMode = False
        MODIFYMode = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume				
    End Sub
    Private Sub SetTextLength()
        On Error GoTo ERR1
        TxtVType.MaxLength = RsVType.Fields("VTYPE").DefinedSize ''				
        txtVName.MaxLength = RsVType.Fields("VNAME").DefinedSize ''				

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub frmVoucherType_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        frmAcmGroup = Nothing
        RsVType = Nothing
    End Sub
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String = ""
        mTitle = ""
        Report1.Reset()
        mTitle = "List Of Voucher Type"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\VType.rpt"
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = " SELECT VTYPE,VNAME," & vbCrLf _
           & " CASE WHEN BOOkTYPE='" & ConCashBook & "' THEN 'Cash' " & vbCrLf _
           & " WHEN BOOkTYPE='" & ConBankBook & "' THEN 'Bank' " & vbCrLf _
           & " WHEN BOOkTYPE='" & ConPDCBook & "' THEN 'PDC' " & vbCrLf _
           & " WHEN BOOkTYPE='" & ConJournalBook & "' THEN 'Journal' " & vbCrLf _
           & " WHEN BOOkTYPE='" & ConContraBook & "' THEN 'Contra' " & vbCrLf _
           & " WHEN BOOkTYPE='" & ConDebitNoteBook & "' THEN 'Debit Note' " & vbCrLf _
           & " WHEN BOOkTYPE='" & ConCreditNoteBook & "' THEN 'Credit Note' " & vbCrLf _
           & " WHEN BOOkTYPE='" & ConSaleBook & "' THEN 'Sale' " & vbCrLf _
           & " WHEN BOOkTYPE='" & ConPurchaseBook & "' THEN 'Purchase' " & vbCrLf _
           & " WHEN BOOkTYPE='" & ConGRBook & "' THEN 'Good Recepit' " & vbCrLf _
           & " WHEN BOOKTYPE='" & ConOpeningBook & "' THEN 'Opening' ELSE BOOKTYPE END " & vbCrLf _
           & "  AS BOOKTYPE, DECODE(FOR_HO,'Y','YES','NO') AS FOR_HO " & vbCrLf _
           & " FROM FIN_VOUCHERTYPE_MST " & vbCrLf _
           & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
           & " ORDER BY VTYPE,BOOKTYPE "

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 12)
            .set_ColWidth(2, 20)
            .set_ColWidth(3, 15)
            .set_ColWidth(3, 15)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle				
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SearchAccount()
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        Select Case cboBookName.Text
            Case "Cash"
                SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_TYPE='1'"
            Case "Bank"
                SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_TYPE='2'"
            Case "PDC"
                SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_TYPE='2'"
            Case Else
                Exit Sub
        End Select

        If MainClass.SearchGridMaster(txtVName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtVName.Text = AcName
            txtVName_Validating(txtVName, New System.ComponentModel.CancelEventArgs(True))
        End If
    End Sub

    Private Sub cboBookName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cboBookName.Validating
        Dim SqlStr As String = ""

        Call AutoCompleteSearch("FIN_VOUCHERTYPE_MST", "VNAME", "", txtVName)

        Select Case cboBookName.Text
            Case "Cash"
                Sqlstr = "SUPP_CUST_TYPE='1'"
            Case "Bank", "PDC"
                SqlStr = "SUPP_CUST_TYPE='2'"
        End Select

        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", Sqlstr, txtVName)

    End Sub
End Class
