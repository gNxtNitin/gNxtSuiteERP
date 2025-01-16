Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLockingBookWise
   Inherits System.Windows.Forms.Form
   Dim RsLocking As ADODB.Recordset
   'Dim PvtDBCN As ADODB.Connection
   Private Const ColBookCode As Short = 1
   Private Const ColBookName As Short = 2
   Private Const ColFromDate As Short = 3
   Private Const ColToDate As Short = 4
   Private Const ConRowHeight As Short = 13
   Dim mFormLoad As Boolean
   Private Sub Show1()

      On Error GoTo Errshow1
      Dim cntCol As Short
        Dim SqlStr As String = ""
        Dim mCheckBookCode As Integer
        Dim mBookCode As Integer


        For cntCol = 1 To SprdMain.MaxRows
            SprdMain.Row = cntCol
            SprdMain.Col = ColBookCode
            mBookCode = Val(SprdMain.Text)

            SqlStr = " SELECT * FROM GEN_LOCKING_MST " & vbCrLf _
               & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
               & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
               & " AND BOOKCODE=" & mBookCode & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLocking, ADODB.LockTypeEnum.adLockReadOnly)

            If RsLocking.EOF = False Then
                SprdMain.Col = ColFromDate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(RsLocking.Fields("LOCK_FROM").Value), "", RsLocking.Fields("LOCK_FROM").Value), "dd/MM/yyyy")

                SprdMain.Col = ColToDate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(RsLocking.Fields("LOCK_TO").Value), "", RsLocking.Fields("LOCK_TO").Value), "dd/MM/yyyy")
            Else
                SprdMain.Col = ColFromDate
                SprdMain.Text = ""

                SprdMain.Col = ColToDate
                SprdMain.Text = ""
            End If
        Next

        mFormLoad = True
        Exit Sub
Errshow1:
        MsgBox(Err.Description)
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)


        On Error GoTo ErrPart

        With SprdMain
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = ColBookCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .ColHidden = True

            .Col = ColBookName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(.Col, 30)

            .Col = ColFromDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(.Col, 10)

            .Col = ColToDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(.Col, 10)

            MainClass.ProtectCell(SprdMain, Arow, .MaxRows, ColBookCode, ColBookName)

            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Clear1()
        FormatSprdMain(-1)
    End Sub
    Private Sub FillSprdMain()
        On Error GoTo ErrPart

        SprdMain.MaxRows = 36
        If GetInsertRow(1, CInt(ConLockBankPayment), "BANK PAYMENT") = False Then GoTo ErrPart
        If GetInsertRow(2, CInt(ConLockBankReceipt), "BANK RECEIPT") = False Then GoTo ErrPart
        If GetInsertRow(3, CInt(ConLockCashPayment), "CASH PAYMENT") = False Then GoTo ErrPart
        If GetInsertRow(4, CInt(ConLockCashReceipt), "CASH RECEIPT") = False Then GoTo ErrPart
        If GetInsertRow(5, CInt(ConLockPDCPayment), "PDC PAYMENT") = False Then GoTo ErrPart
        If GetInsertRow(6, CInt(ConLockPDCReceipt), "PDC RECEIPT") = False Then GoTo ErrPart
        If GetInsertRow(7, CInt(ConLockJournal), "JOURNAL BOOK") = False Then GoTo ErrPart


        If GetInsertRow(8, CInt(ConLockDebitNote), "DEBIT NOTE") = False Then GoTo ErrPart
        If GetInsertRow(9, CInt(ConLockCreditNote), "CREDIT NOTE") = False Then GoTo ErrPart
        If GetInsertRow(10, CInt(ConLockPurchase), "PURCHASE") = False Then GoTo ErrPart
        If GetInsertRow(11, CInt(ConLockSale), "SALE") = False Then GoTo ErrPart
        If GetInsertRow(12, CInt(ConLockMRREntry), "MRR ENTRY") = False Then GoTo ErrPart
        If GetInsertRow(13, CInt(ConLockMRRQC), "MRR QC") = False Then GoTo ErrPart
        If GetInsertRow(14, CInt(ConLockINDENT), "INDENT") = False Then GoTo ErrPart

        If GetInsertRow(15, CInt(ConLockPO), "PURCHASE ORDER") = False Then GoTo ErrPart
        If GetInsertRow(16, CInt(ConLockSO), "SALES ORDER") = False Then GoTo ErrPart
        If GetInsertRow(17, CInt(ConLockPO_DS), "PURCHASE DELEVERY SCHEDULE") = False Then GoTo ErrPart
        If GetInsertRow(18, CInt(ConLockSO_DS), "SALE DELEVERY SCHEDULE") = False Then GoTo ErrPart

        If GetInsertRow(19, CInt(ConLockReoffer), "REOFFER MRR") = False Then GoTo ErrPart
        If GetInsertRow(20, CInt(ConLockMiscMRR), "MISC MRR") = False Then GoTo ErrPart
        If GetInsertRow(21, CInt(ConLockStoreReq), "STORE REQUISTION") = False Then GoTo ErrPart
        If GetInsertRow(22, CInt(ConLockIssueNote), "ISSUE NOTE") = False Then GoTo ErrPart
        If GetInsertRow(23, CInt(ConLockGatePassReq), "GATEPASS REQUISTION") = False Then GoTo ErrPart
        If GetInsertRow(24, CInt(ConLockGatePass), "GATEPASS") = False Then GoTo ErrPart
        If GetInsertRow(25, CInt(ConLockSTN), "STORE RETURN NOTE") = False Then GoTo ErrPart
        If GetInsertRow(26, CInt(ConLockDespatch), "DESPATCH NOTE") = False Then GoTo ErrPart
        If GetInsertRow(27, CInt(ConLockModvat), "MODVAT ENTRY") = False Then GoTo ErrPart
        If GetInsertRow(28, CInt(ConLockSalesTaxRefund), "SALES TAX REFUND") = False Then GoTo ErrPart
        If GetInsertRow(29, CInt(ConLockServiceTaxRefund), "SERVICE TAX REFUND") = False Then GoTo ErrPart
        If GetInsertRow(30, CInt(ConLockAssetEntry), "ASSET ENTRY") = False Then GoTo ErrPart
        If GetInsertRow(31, CInt(ConLockProvision), "PROVISION ENTRY") = False Then GoTo ErrPart
        If GetInsertRow(32, CInt(ConLockPhysical), "PHYSICAL ENTRY") = False Then GoTo ErrPart

        If GetInsertRow(33, CInt(ConLockEmpSalaryProcess), "EMPLOYEE SALARY PROCESS ") = False Then GoTo ErrPart
        If GetInsertRow(34, CInt(ConLockEmpOTProcess), "CAUSAL SALARY PROCESS") = False Then GoTo ErrPart
        If GetInsertRow(35, CInt(ConLockContSalaryProcess), "EMPLOYEE OT PROCESS") = False Then GoTo ErrPart
        If GetInsertRow(36, CInt(ConLockPerksProcess), "EMPLOYEE PERKS PROCESS") = False Then GoTo ErrPart

        FormatSprdMain(-1)


        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function GetInsertRow(ByRef pRow As Integer, ByRef pBookCode As Integer, ByRef pBookName As String) As Boolean
        On Error GoTo ErrPart

        With SprdMain
            .Row = pRow
            .Col = ColBookCode
            .Text = Str(pBookCode)


            .Col = ColBookName
            .Text = pBookName
        End With

        GetInsertRow = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        GetInsertRow = False
    End Function

    Private Sub chkLockFY_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkLockFY.CheckStateChanged
        Dim cntRow As Integer
        Dim mFromDate As String
        Dim mToDate As String

        CmdSave.Enabled = True

        mFromDate = VB6.Format(RsCompany.Fields("Start_Date").Value, "dd/MM/yyyy")
        mToDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "dd/MM/yyyy")

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColFromDate
                .Text = mFromDate

                .Col = ColToDate
                .Text = mToDate
            Next
        End With

    End Sub

    Private Sub chkUnlock_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkUnlock.CheckStateChanged
        Dim cntRow As Integer
        Dim mFromDate As String
        Dim mToDate As String

        CmdSave.Enabled = True

        mFromDate = ""
        mToDate = ""

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColFromDate
                .Text = mFromDate

                .Col = ColToDate
                .Text = mToDate
            Next
        End With

    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrSave
        Dim mSqlStr As String = ""
        Dim cntCol As Short

        Dim SqlStr As String = ""
        Dim mBookCode As Integer
        Dim mFromDate As String
        Dim mToDate As String
        Dim mLockingRights As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mLockingRights = GetUserPermission("BOOK_LOCKING", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)

        If mLockingRights = "N" Then
            MsgInformation("You Have no enough Rights.")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        mSqlStr = " Delete From GEN_LOCKING_MST " & vbCrLf _
           & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
           & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        PubDBCn.Execute(mSqlStr)


        ''**************************
        For cntCol = 1 To SprdMain.MaxRows
            SprdMain.Row = cntCol

            SprdMain.Col = ColBookCode
            mBookCode = Val(SprdMain.Text)

            SprdMain.Col = ColFromDate
            mFromDate = VB6.Format(SprdMain.Text, "dd-MMM-yyyy")

            SprdMain.Col = ColToDate
            mToDate = VB6.Format(SprdMain.Text, "dd-MMM-yyyy")


            If mBookCode <> 0 Then
                SqlStr = ""
                SqlStr = " INSERT INTO GEN_LOCKING_MST (" & vbCrLf _
                                  & " COMPANY_CODE, FYEAR, " & vbCrLf _
                                  & " BOOKCODE, LOCK_FROM, LOCK_TO, " & vbCrLf _
                                  & " ADDUSER, ADDDATE, MODUSER, MODDATE " & vbCrLf _
                                  & " ) VALUES (" & vbCrLf _
                                  & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf _
                                  & " " & mBookCode & ", " & vbCrLf _
                                  & " TO_DATE('" & VB6.Format(mFromDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(mToDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                                  & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                                  & " '', '')"

                PubDBCn.Execute(SqlStr)
            End If
LabelSave:
        Next

        Show1()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        mFormLoad = True
        CmdSave.Enabled = False
        Exit Sub
ErrSave:
        MsgBox(Err.Description)
        ''Resume
    End Sub

    Private Sub frmLockingBookWise_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim SqlStr As String = ""

        Call SetMainFormCordinate(Me)

        'Set PvtDBCN = New ADODB.Connection
        'PvtDBCN.Open StrConn
        'Me.Top = 0
        'Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(6075) '8000
        ''Me.Width = VB6.TwipsToPixelsX(7935) '11900

        Clear1()

        '    SqlStr = " Select * From GEN_LOCKING_MST " & vbCrLf _
        ''            & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsLocking, adLockOptimistic
        MainClass.SetControlsColor(Me)

        FillSprdMain()
        Show1()
        mFormLoad = True

    End Sub

   Private Sub frmLockingBookWise_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
      Dim Cancel As Boolean = eventArgs.Cancel
      Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
      RsLocking.Close()
      RsLocking = Nothing
      eventArgs.Cancel = Cancel
   End Sub

   Private Sub frmLockingBookWise_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
   End Sub
   Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
      CmdSave.Enabled = True
   End Sub



   Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

      Dim mFromDate As String
      Dim mToDate As String

      If eventArgs.NewRow = -1 Then Exit Sub

      SprdMain.Row = eventArgs.row

      Select Case eventArgs.col
         Case ColFromDate
            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = ColFromDate
            mFromDate = Trim(SprdMain.Text)
            If mFromDate = "" Then Exit Sub

            If Not IsDate(mFromDate) Then
               MsgInformation("Invaild Date.")
               MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColFromDate)
            End If

            If FYChk(mFromDate) = False Then
               MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColFromDate)
            End If

            SprdMain.Col = ColToDate
            mToDate = Trim(SprdMain.Text)

            If mToDate <> "" Then
               If CDate(mToDate) < CDate(mFromDate) Then
                  MsgInformation("From Date Cann't be Greater Than To Date.")
                  MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColFromDate)
               End If
            End If
         Case ColToDate
            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = ColToDate
            mToDate = Trim(SprdMain.Text)
            If mToDate = "" Then Exit Sub

            If Not IsDate(mToDate) Then
               MsgInformation("Invaild Date.")
               MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColToDate)
            End If

            If FYChk(mToDate) = False Then
               MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColToDate)
            End If

            SprdMain.Col = ColFromDate
            mFromDate = Trim(SprdMain.Text)

            If mFromDate = "" Then
               MsgInformation("Please Enter From Date First.")
               MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColToDate)
            Else
               If CDate(mToDate) < CDate(mFromDate) Then
                  MsgInformation("To Date Cann't be Less Than From Date.")
                  MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColToDate)
               End If
            End If
      End Select
      Exit Sub
ErrPart:
      MsgBox(Err.Description)
   End Sub
End Class
