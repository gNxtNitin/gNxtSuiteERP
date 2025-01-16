Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmGSTChallan
    Inherits System.Windows.Forms.Form
    Dim RsChallan As ADODB.Recordset ''Recordset
    Dim RsChallanDet As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection
    Private Const ConRowHeight As Short = 12

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim SqlStr As String = ""

    Private Sub chkAccountPost_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAccountPost.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtRefNo.Enabled = False
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            ClearGrid1()
            Call FormatSprdMain(-1)
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub ClearGrid1()
        On Error GoTo AddErr
        Dim cntRow As Integer
        Dim cntCol As Integer

        For cntRow = 1 To SprdMain.MaxRows
            For cntCol = 2 To SprdMain.MaxCols
                SprdMain.Row = cntRow
                SprdMain.Col = cntCol
                SprdMain.Text = ""
            Next
        Next

        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart


        If ValidateBranchLocking((txtRefDate.Text)) = True Then
            Exit Sub
        End If

        If Trim(txtRefNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If chkAccountPost.Enabled = False And chkAccountPost.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Account Posting is Done, So can't be delete")
            Exit Sub
        End If

        If Not RsChallan.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "FIN_GSTCHALLAN_HDR", (txtRefNo.Text), RsChallan, "REF_NO") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "FIN_GSTCHALLAN_HDR", "REF_NO", (lblMkey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM FIN_GSTCHALLAN_DET WHERE REF_NO='" & lblMkey.Text & "' ")
                PubDBCn.Execute("DELETE FROM FIN_GSTCHALLAN_HDR WHERE REF_NO='" & lblMkey.Text & "' ")


                PubDBCn.CommitTrans()
                RsChallan.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsChallan.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '        Resume
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then

            If PubUserID <> "G0416" Then
                If chkAccountPost.Enabled = False And chkAccountPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                    MsgInformation("Account Posting is Done, So can't be modify")
                    Exit Sub
                End If
            End If
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsChallan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtRefNo.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForShow(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForShow(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String

        SqlStr = ""

        SqlStr = MakeSQL


        mTitle = "Payment of Tax (GST Challan)"
        mSubTitle = "For the Month of : " & VB6.Format(txtRefDate.Text, "MMMM-YYYY")
        mRPTName = "GSTChallan.Rpt"

        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        ''Resume
    End Sub
    Private Function MakeSQL() As String
        On Error GoTo ERR1


        MakeSQL = "SELECT IH.*, " & vbCrLf & " ID.SERIAL_NO, ID.GST_TYPE, ID.TAX_PAYABLE," & vbCrLf & " ID.PAID_FROM_IGST, ID.PAID_FROM_CGST, ID.PAID_FROM_SGST, " & vbCrLf & " ID.PAID_FROM_CESS, ID.CASH_PAID, ID.INTEREST_AMT, " & vbCrLf & " ID.LATE_FEE"


        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_GSTCHALLAN_HDR IH, FIN_GSTCHALLAN_DET ID"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE" & vbCrLf & " AND IH.FYEAR=ID.FYEAR" & vbCrLf & " AND IH.REF_NO=ID.REF_NO " & vbCrLf & " AND IH.REF_NO=" & Val(txtRefNo.Text) & ""


        ''ORDER CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.REF_NO, ID.SERIAL_NO"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol

        '    If KeyCode = vbKeyF1 And mCol = ColItemCode Then SprdMain_Click ColItemCode, 0
        '    If KeyCode = vbKeyF1 And mCol = ColItemDesc Then SprdMain_Click ColItemDesc, 0

        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub SprdMain_LeaveRow(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveRowEvent) Handles SprdMain.LeaveRow
        '    SprdMain.Row = Row
        '    SprdMain.Row2 = Row
        '    SprdMain.Col = 1
        '    SprdMain.col2 = SprdMain.ActiveCol
        '    SprdMain.BlockMode = True
        '    SprdMain.BackColor = &HFFFF80
        '    SprdMain.BlockMode = False
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart

        If eventArgs.NewRow = -1 Then Exit Sub

        Call CalcQty()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Function CalcQty() As Double
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim cntCol As Integer

        CalcQty = 0
        For cntRow = 1 To SprdMain.MaxRows
            For cntCol = 3 To SprdMain.MaxCols
                SprdMain.Row = cntRow
                SprdMain.Col = cntCol
                CalcQty = CalcQty + Val(SprdMain.Text)
            Next
        Next

        txtTotAmount.Text = VB6.Format(CalcQty, "0.00")

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        Dim mCapital As String
        Dim mPLA As String
        Dim mRePost As String
        With SprdView
            .Row = eventArgs.Row

            .Col = 1
            txtRefNo.Text = VB6.Format(.Text, "00000")

            .Col = 2
            txtRefDate.Text = VB6.Format(.Text, "DD/MM/YYYY")

            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))

            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub

    Private Sub txtBSRCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBSRCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBSRCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBSRCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBSRCode.Text) ' MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtChallanDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChallanDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtChallanDate.Text) = "" Then
            GoTo EventExitSub
        End If

        If Not IsDate(txtChallanDate.Text) Then
            MsgBox("Not a Valid Ref Date.", MsgBoxStyle.Critical)
            txtChallanDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRefDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRefDate.Text) = "" Then
            GoTo EventExitSub
        End If

        If Not IsDate(txtRefDate.Text) Then
            MsgBox("Not a Valid Ref Date.", MsgBoxStyle.Critical)
            txtRefDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtRefNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRefNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub



    Public Sub txtRefNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xRefNo As Integer

        If Val(txtRefNo.Text) = 0 Then GoTo EventExitSub

        If Len(txtRefNo.Text) < 6 Then
            txtRefNo.Text = VB6.Format(Val(txtRefNo.Text), "00000") & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsChallan.EOF = False Then xRefNo = RsChallan.Fields("REF_NO").Value


        SqlStr = " SELECT * FROM FIN_GSTCHALLAN_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        SqlStr = SqlStr & vbCrLf & " AND REF_NO=" & Val(txtRefNo.Text) & ""

        SqlStr = SqlStr & vbCrLf & " AND IS_RC='" & Trim(lblIsReverseCharge.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallan, ADODB.LockTypeEnum.adLockReadOnly)

        If RsChallan.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Voucher, Use Generate Voucher Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM FIN_GSTCHALLAN_HDR " & " WHERE REF_NO=" & Val(CStr(xRefNo)) & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallan, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim mRefNo As Integer
        Dim mChallanType As String
        Dim pBankVoucherMkey As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If Trim(txtRefNo.Text) = "" Then
            mRefNo = CInt(AutoGenSeqBillNo())
        Else
            mRefNo = Val(txtRefNo.Text)
        End If

        txtRefNo.Text = CStr(Val(CStr(mRefNo)))

        If Trim(txtRemarks.Text) = "" Then
            txtRemarks.Text = "GST PAYMENT FOR THE MONTH OF " & UCase(VB6.Format(txtVDate.Text, "MMMM-YYYY")) & " AGAINST REF NO " & txtChallanNo.Text & " DATE " & VB6.Format(txtChallanDate.Text, "DD/MM/YYYY")
        End If

        SqlStr = ""
        If ADDMode = True Then
            lblMkey.Text = CStr(mRefNo)
            SqlStr = "INSERT INTO FIN_GSTCHALLAN_HDR( " & vbCrLf & " COMPANY_CODE, FYEAR, " & vbCrLf & " REF_NO, REF_DATE, " & vbCrLf & " CHALLANNO, CHALLANDATE, " & vbCrLf & " TOTALAMOUNT, " & vbCrLf & " BSR_CODE, NARRATION, IS_RC, " & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE) VALUES ("

            SqlStr = SqlStr & vbCrLf & " " & RsCompany.Fields("Company_Code").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & Val(CStr(mRefNo)) & ", TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtChallanNo.Text)) & "',TO_DATE('" & VB6.Format(txtChallanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " " & Val(txtTotAmount.Text) & ",  " & vbCrLf & " '" & txtBSRCode.Text & "', '" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', '" & MainClass.AllowSingleQuote((lblIsReverseCharge.Text)) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE FIN_GSTCHALLAN_HDR SET " & vbCrLf & " REF_NO=" & Val(CStr(mRefNo)) & ", " & vbCrLf & " REF_DATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " CHALLANNO='" & MainClass.AllowSingleQuote((txtChallanNo.Text)) & "', " & vbCrLf & " CHALLANDATE=TO_DATE('" & VB6.Format(txtChallanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " TOTALAMOUNT=" & Val(txtTotAmount.Text) & ", " & vbCrLf & " BSR_CODE='" & txtBSRCode.Text & "'," & vbCrLf & " NARRATION='" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "'," & vbCrLf & " IS_RC='" & MainClass.AllowSingleQuote((lblIsReverseCharge.Text)) & "'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf & " WHERE REF_NO ='" & MainClass.AllowSingleQuote(lblMkey.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)

        UpdateDetail1()

        If chkAccountPost.CheckState = System.Windows.Forms.CheckState.Checked Then
            If chkAccountPost.Enabled = True Then
                pBankVoucherMkey = ""
                If GenerateBankVoucher(pBankVoucherMkey, 1, True) = False Then
                    GoTo ErrPart
                End If
                SqlStr = "UPDATE FIN_GSTCHALLAN_HDR SET AC_POST='Y', BANKVOUCHERMKEY='" & pBankVoucherMkey & "' WHERE REF_NO='" & MainClass.AllowSingleQuote(lblMkey.Text) & "'"
                PubDBCn.Execute(SqlStr)
            Else
                pBankVoucherMkey = lblBankMKey.Text
                If GenerateBankVoucher(pBankVoucherMkey, 1, False) = False Then
                    GoTo ErrPart
                End If
            End If

            If Trim(txtVType.Text & txtVNo.Text) <> "" And ADDMode = True Then
                MsgBox(" Voucher No. " & Trim(txtVType.Text & txtVNo.Text) & " Created. ", MsgBoxStyle.Information)
            End If

        End If

        UpdateMain1 = True

        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        Resume
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsChallan.Requery() ''.Refresh
        RsChallanDet.Requery()
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '    Resume
    End Function
    Private Function GenerateBankVoucher(ByRef pBankVoucherMkey As String, ByRef mDivCode As Double, ByRef pAddMode As Boolean) As Boolean

        On Error GoTo ErrPart
        Dim mBookCode As String
        Dim SqlStr As String = ""
        Dim mAccountCode As Integer
        Dim mDrCr As String
        Dim mVAmount As Double

        Dim mVnoStr As String
        Dim mVType As String

        Dim mVNoPrefix As String
        Dim mVNoSuffix As String

        Dim mBookType As String
        Dim mBookSubType As String
        Dim mVNO As String
        Dim mCancelled As String
        Dim pRowNo As Integer
        Dim CurBankMKey As String
        Dim pBankBookType As String

        '    If Right(lblBookType.Caption, 1) = "R" Then
        '        pBankBookType = ConBankReceipt
        '    Else
        '        pBankBookType = ConBankPayment
        '    End If

        pBankBookType = ConJournal

        txtVType.Text = ConJournal
        mBookType = VB.Left(pBankBookType, 1)
        mBookSubType = VB.Right(pBankBookType, 1)

        If pAddMode = True Then
            mVNO = GenBankVno(pBankBookType)
        Else
            mVNO = VB6.Format(txtVNo.Text, "00000")
        End If
        mVNoPrefix = ""
        mVNoSuffix = ""
        mVType = Trim(txtVType.Text)
        mVnoStr = mVNoPrefix & mVType & mVNO & mVNoSuffix
        txtVNo.Text = mVNO

        mCancelled = "N"

        '    If MainClass.ValidateWithMasterTable(txtBankName, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mBookCode = MasterNo
        '    End If

        mBookCode = CStr(ConJournalBookCode)

        If pAddMode = True Then
            pRowNo = MainClass.AutoGenRowNo("FIN_VOUCHER_HDR", "RowNo", PubDBCn)
            CurBankMKey = (VB6.Format(RsCompany.Fields("COMPANY_CODE").Value)) & VB6.Format(RsCompany.Fields("FYEAR").Value) & VB6.Format(pRowNo)
            pBankVoucherMkey = CurBankMKey

            SqlStr = " INSERT INTO FIN_VOUCHER_HDR ( " & vbCrLf & " Mkey, COMPANY_CODE, " & vbCrLf & " FYEAR,RowNo, VType, VNoPrefix, VNoSeq, VNoSuffix, " & vbCrLf & " Vno, Vdate, BookType,BookSubType, " & vbCrLf & " BookCode, Narration, CANCELLED, " & vbCrLf & " AddUser, AddDate, ModUser, ModDate,UPDATE_FROM, EXPDATE) VALUES ( " & vbCrLf & " '" & CurBankMKey & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & pRowNo & ", " & vbCrLf & " '" & mVType & "', '" & mVNoPrefix & "', " & vbCrLf & " " & Val(mVNO) & ", '" & mVNoSuffix & "', '" & mVnoStr & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mBookType & "', '" & mBookSubType & "', " & vbCrLf & " '" & mBookCode & "', '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', '" & mCancelled & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','H', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        Else
            CurBankMKey = lblBankMKey.Text
            pBankVoucherMkey = CurBankMKey
            SqlStr = "UPDATE FIN_VOUCHER_HDR SET " & vbCrLf _
                & " Vdate=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " EXPDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " VType= '" & mVType & "'," & vbCrLf & " VnoPrefix='" & mVNoPrefix & "', " & vbCrLf & " VnoSeq=" & Val(mVNO) & ", " & vbCrLf & " VnoSuffix='" & mVNoSuffix & "', " & vbCrLf & " Vno='" & mVnoStr & "', " & vbCrLf & " BookCode='" & mBookCode & "', " & vbCrLf & " Narration='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf & " CANCELLED='" & mCancelled & "', " & vbCrLf & " BookType='" & mBookType & "', " & vbCrLf & " BookSubType='" & mBookSubType & "', " & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " Where Mkey='" & CurBankMKey & "'"
        End If

        PubDBCn.Execute(SqlStr)
        If GenerateBankDetail(CurBankMKey, pRowNo, mBookCode, pBankBookType, mVType, mVnoStr, (txtVDate.Text), (txtRemarks.Text), mDivCode, PubDBCn) = False Then GoTo ErrPart


        '    mVAmount = Val(CDbl(lblNetAmount.Caption))
        '    mDrCr = "C"
        '
        '    If UpdateTRN(PubDBCn, CurBankMKey, pRowNo, -1, mBookCode, mVType, mBookType, _
        ''            mBookSubType, mBookCode, mVnoStr, txtVDate.Text, mVnoStr, txtVDate.Text, _
        ''            mVAmount, mDrCr, "P", "", "", -1, -1, -1, -1, "", _
        ''            "", "P", "", "", txtRemarks.Text, "", txtVDate.Text, ADDMode, PubUserID, Format(PubCurrDate, "DD-MMM-YYYY"), mDivCode, "N") = False Then GoTo ErrPart
        '
        '    If (pBankBookType = ConBankPayment) And Trim(txtChqNo.Text) <> "" Then
        '        If UpdateChequeDetail(Trim(txtChqNo.Text), CurBankMKey, "C") = False Then GoTo ErrPart
        '    End If

        GenerateBankVoucher = True
        Exit Function
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GenerateBankVoucher = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function GenerateBankDetail(ByRef mMKey As String, ByRef mRowNo As Integer, ByRef mBookCode As String, ByRef pBankBookType As String, ByRef mVType As String, ByRef mVNO As String, ByRef mVDate As String, ByRef pNarration As String, ByRef mDivCode As Double, ByRef pDBCn As ADODB.Connection) As Boolean

        On Error GoTo ErrDetail

        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mAccountName As String
        Dim mAccountCode As String
        Dim mChequeNo As String
        Dim mChqDate As String
        Dim mAmount As Double
        Dim mCCCode As String
        Dim mDeptCode As String
        Dim mEmpCode As String
        Dim mExpCode As String
        Dim mIBRNo As String
        Dim mDC As String
        Dim mRemarks As String
        Dim mPRRowNo As Integer
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mClearDate As String
        Dim mParticulars As String
        'Dim cntRow As Long
        Dim mCreditApplicable As String


        mBookType = VB.Left(pBankBookType, 1)
        mBookSubType = VB.Right(pBankBookType, 1)

        SqlStr = "Delete From FIN_BILLDETAILS_TRN Where Mkey='" & mMKey & "'"
        pDBCn.Execute(SqlStr)

        SqlStr = "Delete From FIN_VOUCHER_DET Where Mkey='" & mMKey & "'"
        pDBCn.Execute(SqlStr)


        SqlStr = "DELETE FROM FIN_POSTED_TRN  WHERE " & vbCrLf & " MKEY ='" & mMKey & "' " & vbCrLf & " AND BookType='" & mBookType & "'" & vbCrLf & " AND BooksubType='" & mBookSubType & "'"
        pDBCn.Execute(SqlStr)

        mChequeNo = ""
        mChqDate = ""
        mCCCode = "001"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "001"
        mIBRNo = "-1"
        mClearDate = ""
        mParticulars = pNarration
        '    cntRow = 1
        mPRRowNo = 1
        I = 0

        '************************** Posting Payable

        I = I + 1
        mPRRowNo = I
        mDC = "D"
        SprdMain.Row = 1
        SprdMain.Col = 2
        mAmount = Val(SprdMain.Text)
        If lblIsReverseCharge.Text = "Y" Then
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("IGST_RC_SALECODE").Value), "-1", RsCompany.Fields("IGST_RC_SALECODE").Value)
        Else
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("IGST_SALECODE").Value), "-1", RsCompany.Fields("IGST_SALECODE").Value)
        End If

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid IGST Payable Code, Please contact to Administrator.")
            GenerateBankDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pBankBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If


        I = I + 1
        mPRRowNo = I
        mDC = "D"
        SprdMain.Row = 2
        SprdMain.Col = 2
        mAmount = Val(SprdMain.Text)
        If lblIsReverseCharge.Text = "Y" Then
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("CGST_RC_SALECODE").Value), "-1", RsCompany.Fields("CGST_RC_SALECODE").Value)
        Else
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("CGST_SALECODE").Value), "-1", RsCompany.Fields("CGST_SALECODE").Value)
        End If

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid CGST Payable Code, Please contact to Administrator.")
            GenerateBankDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pBankBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        I = I + 1
        mPRRowNo = I
        mDC = "D"
        SprdMain.Row = 3
        SprdMain.Col = 2
        mAmount = Val(SprdMain.Text)
        If lblIsReverseCharge.Text = "Y" Then
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("SGST_RC_SALECODE").Value), "-1", RsCompany.Fields("SGST_RC_SALECODE").Value)
        Else
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("SGST_SALECODE").Value), "-1", RsCompany.Fields("SGST_SALECODE").Value)
        End If

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid SGST Payable Code, Please contact to Administrator.")
            GenerateBankDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pBankBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        '************************** Posting IGST Recoverable

        mAmount = 0
        I = I + 1
        mPRRowNo = I
        mDC = "C"
        SprdMain.Row = 1
        SprdMain.Col = 3
        mAmount = Val(SprdMain.Text)

        SprdMain.Row = 2
        SprdMain.Col = 3
        mAmount = mAmount + Val(SprdMain.Text)

        SprdMain.Row = 3
        SprdMain.Col = 3
        mAmount = mAmount + Val(SprdMain.Text)

        If lblIsReverseCharge.Text = "Y" Then
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("IGST_RC_REFUNDCODE").Value), "-1", RsCompany.Fields("IGST_RC_REFUNDCODE").Value)
        Else
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("IGST_REFUNDCODE").Value), "-1", RsCompany.Fields("IGST_REFUNDCODE").Value)
        End If

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid IGST Recovery Code, Please contact to Administrator.")
            GenerateBankDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pBankBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        '************************** Posting CGST Recoverable

        mAmount = 0
        I = I + 1
        mPRRowNo = I
        mDC = "C"
        SprdMain.Row = 1
        SprdMain.Col = 4
        mAmount = Val(SprdMain.Text)

        SprdMain.Row = 2
        SprdMain.Col = 4
        mAmount = mAmount + Val(SprdMain.Text)

        SprdMain.Row = 3
        SprdMain.Col = 4
        mAmount = mAmount + Val(SprdMain.Text)

        If lblIsReverseCharge.Text = "Y" Then
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("CGST_RC_REFUNDCODE").Value), "-1", RsCompany.Fields("CGST_RC_REFUNDCODE").Value)
        Else
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("CGST_REFUNDCODE").Value), "-1", RsCompany.Fields("CGST_REFUNDCODE").Value)
        End If

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid CGST Recovery Code, Please contact to Administrator.")
            GenerateBankDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pBankBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        '************************** Posting SGST Recoverable

        mAmount = 0
        I = I + 1
        mPRRowNo = I
        mDC = "C"
        SprdMain.Row = 1
        SprdMain.Col = 5
        mAmount = Val(SprdMain.Text)

        SprdMain.Row = 2
        SprdMain.Col = 5
        mAmount = mAmount + Val(SprdMain.Text)

        SprdMain.Row = 3
        SprdMain.Col = 5
        mAmount = mAmount + Val(SprdMain.Text)

        If lblIsReverseCharge.Text = "Y" Then
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("SGST_RC_REFUNDCODE").Value), "-1", RsCompany.Fields("SGST_RC_REFUNDCODE").Value)
        Else
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("SGST_REFUNDCODE").Value), "-1", RsCompany.Fields("SGST_REFUNDCODE").Value)
        End If

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid SGST Recovery Code, Please contact to Administrator.")
            GenerateBankDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pBankBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        '************************** Posting Cash Payment

        mAmount = 0
        I = I + 1
        mPRRowNo = I
        mDC = "C"
        SprdMain.Row = 1
        SprdMain.Col = 7
        mAmount = Val(SprdMain.Text)

        If lblIsReverseCharge.Text = "Y" Then
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("E_RCIGSTLEDGER_ACCTCODE").Value), "-1", RsCompany.Fields("E_RCIGSTLEDGER_ACCTCODE").Value)
        Else
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("E_IGSTLEDGER_ACCTCODE").Value), "-1", RsCompany.Fields("E_IGSTLEDGER_ACCTCODE").Value)
        End If

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid CGST Electronic Ledger Code, Please contact to Administrator.")
            GenerateBankDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pBankBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        mAmount = 0
        I = I + 1
        mPRRowNo = I
        mDC = "C"
        SprdMain.Row = 2
        SprdMain.Col = 7
        mAmount = Val(SprdMain.Text)

        If lblIsReverseCharge.Text = "Y" Then
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("E_RCCGSTLEDGER_ACCTCODE").Value), "-1", RsCompany.Fields("E_RCCGSTLEDGER_ACCTCODE").Value)
        Else
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("E_CGSTLEDGER_ACCTCODE").Value), "-1", RsCompany.Fields("E_CGSTLEDGER_ACCTCODE").Value)
        End If

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid SGST Electronic Ledger Code, Please contact to Administrator.")
            GenerateBankDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pBankBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        mAmount = 0
        I = I + 1
        mPRRowNo = I
        mDC = "C"
        SprdMain.Row = 3
        SprdMain.Col = 7
        mAmount = Val(SprdMain.Text)

        If lblIsReverseCharge.Text = "Y" Then
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("E_RCSGSTLEDGER_ACCTCODE").Value), "-1", RsCompany.Fields("E_RCSGSTLEDGER_ACCTCODE").Value)
        Else
            mAccountCode = IIf(IsDbNull(RsCompany.Fields("E_SGSTLEDGER_ACCTCODE").Value), "-1", RsCompany.Fields("E_SGSTLEDGER_ACCTCODE").Value)
        End If

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid Electronic Ledger Code, Please contact to Administrator.")
            GenerateBankDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pBankBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        '************************** Posting Interest Fees

        mAmount = 0
        I = I + 1
        mPRRowNo = I
        mDC = "C"
        SprdMain.Row = 1
        SprdMain.Col = 8
        mAmount = Val(SprdMain.Text)

        SprdMain.Row = 2
        SprdMain.Col = 8
        mAmount = mAmount + Val(SprdMain.Text)

        SprdMain.Row = 3
        SprdMain.Col = 8
        mAmount = mAmount + Val(SprdMain.Text)

        mAccountCode = IIf(IsDbNull(RsCompany.Fields("GST_INTEREST_ACCTCODE").Value), "-1", RsCompany.Fields("GST_INTEREST_ACCTCODE").Value)

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid Interest Account Code, Please contact to Administrator.")
            GenerateBankDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pBankBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        '************************** Posting Late Fees

        mAmount = 0
        I = I + 1
        mPRRowNo = I
        mDC = "C"
        SprdMain.Row = 1
        SprdMain.Col = 9
        mAmount = Val(SprdMain.Text)

        SprdMain.Row = 2
        SprdMain.Col = 9
        mAmount = mAmount + Val(SprdMain.Text)

        SprdMain.Row = 3
        SprdMain.Col = 9
        mAmount = mAmount + Val(SprdMain.Text)

        mAccountCode = IIf(IsDbNull(RsCompany.Fields("GST_LATE_ACCTCODE").Value), "-1", RsCompany.Fields("GST_LATE_ACCTCODE").Value)

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid Late Fee Account Code, Please contact to Administrator.")
            GenerateBankDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pBankBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If


        GenerateBankDetail = True
        Exit Function
ErrDetail:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GenerateBankDetail = False
        ''Resume
    End Function


    Private Function GeneratePostingDetail(ByRef mMKey As String, ByRef mRowNo As Integer, ByRef mBookCode As String, ByRef pBankBookType As String, ByRef mVType As String, ByRef mVNO As String, ByRef mVDate As String, ByRef pNarration As String, ByRef mDivCode As Double, ByRef pDBCn As ADODB.Connection, ByRef mAccountCode As String, ByRef mAmount As Double, ByRef mChequeNo As String, ByRef mChqDate As String, ByRef mCCCode As String, ByRef mDeptCode As String, ByRef mEmpCode As String, ByRef mExpCode As String, ByRef mIBRNo As String, ByRef mDC As String, ByRef mRemarks As String, ByRef mPRRowNo As Integer, ByRef I As Integer, ByRef mBookType As String, ByRef mBookSubType As String, ByRef mClearDate As String, ByRef mParticulars As String) As Boolean

        On Error GoTo ErrDetail

        ''mAccountCode

        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMKey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivCode & " )"

            PubDBCn.Execute(SqlStr)

            If UpdatePRDetail(pDBCn, mMKey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNO, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", "N", mBookType, mBookSubType, VB6.Format(mVDate, "DD/MM/YYYY"), ADDMode, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivCode, -1, "N") = False Then GoTo ErrDetail
        End If



        GeneratePostingDetail = True
        Exit Function
ErrDetail:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GeneratePostingDetail = False
        ''Resume
    End Function
    Private Function GenBankVno(ByRef pBankBookType As String) As String

        On Error GoTo ERR1
        Dim mVNo1 As String
        Dim SqlStr2 As String
        Dim SqlStr As String = ""
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mVType As String


        mBookType = VB.Left(pBankBookType, 1)
        mBookSubType = VB.Right(pBankBookType, 1)
        mVType = Trim(txtVType.Text)

        If ADDMode = True Or txtVNo.Text = "" Then
            SqlStr = "SELECT MAX(VNOSeq) From FIN_VOUCHER_HDR WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BookType='" & mBookType & "'" & vbCrLf & " AND BookSubType='" & mBookSubType & "'" & vbCrLf & " AND VTYPE='" & MainClass.AllowSingleQuote(mVType) & "'"

            GenBankVno = VB6.Format(MainClass.AutoGenVNo(SqlStr, PubDBCn), "00000")
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description)
        'Resume
    End Function
    Private Function UpdateDetail1() As Boolean
        On Error GoTo UpdateDetail1Err

        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mGSTType As String
        Dim mTaxPayable As Double

        Dim mPaidFromIGST As Double
        Dim mPaidFromCGST As Double
        Dim mPaidFromSGST As Double
        Dim mPaidFromCESS As Double
        Dim mPaidCash As Double
        Dim mInterest As Double
        Dim mLateFee As Double

        PubDBCn.Execute("Delete From FIN_GSTCHALLAN_DET Where REF_NO='" & lblMkey.Text & "'")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                If I = 1 Then
                    mGSTType = "IGST"
                ElseIf I = 2 Then
                    mGSTType = "CGST"
                ElseIf I = 3 Then
                    mGSTType = "SGST"
                ElseIf I = 4 Then
                    mGSTType = "CESS"
                End If

                .Col = 2
                mTaxPayable = Val(.Text)

                .Col = 3
                mPaidFromIGST = Val(.Text)

                .Col = 4
                mPaidFromCGST = Val(.Text)

                .Col = 5
                mPaidFromSGST = Val(.Text)

                .Col = 6
                mPaidFromCESS = Val(.Text)

                .Col = 7
                mPaidCash = Val(.Text)

                .Col = 8
                mInterest = Val(.Text)

                .Col = 9
                mLateFee = Val(.Text)

                SqlStr = ""

                SqlStr = " INSERT INTO FIN_GSTCHALLAN_DET ( " & vbCrLf & " COMPANY_CODE, FYEAR, " & vbCrLf & " REF_NO, REF_DATE, " & vbCrLf & " SERIAL_NO, GST_TYPE," & vbCrLf & " TAX_PAYABLE, PAID_FROM_IGST, " & vbCrLf & " PAID_FROM_CGST, PAID_FROM_SGST," & vbCrLf & " PAID_FROM_CESS, CASH_PAID, " & vbCrLf & " INTEREST_AMT, LATE_FEE) "

                SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf & " " & Val(LblMKey.Text) & ", TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & Val(CStr(I)) & ", '" & mGSTType & "'," & vbCrLf & " " & Val(CStr(mTaxPayable)) & ", " & Val(CStr(mPaidFromIGST)) & ", " & vbCrLf & " " & Val(CStr(mPaidFromCGST)) & ", " & Val(CStr(mPaidFromSGST)) & "," & vbCrLf & " " & Val(CStr(mPaidFromCESS)) & ", " & Val(CStr(mPaidCash)) & ", " & vbCrLf & " " & Val(CStr(mInterest)) & ", " & Val(CStr(mLateFee)) & " " & vbCrLf & " )"

                PubDBCn.Execute(SqlStr)

            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 2)
            .set_RowHeight(0, ConRowHeight * 3)

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            '        .TypeEditLen = RsChallanDet.Fields("AUTO_KEY_SO").Precision
            .set_ColWidth(1, 15)

            For I = 2 To 9
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMin = CDbl("-99999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(I, 9)
            Next
        End With

        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, 9)

        If lblIsReverseCharge.Text = "N" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, 1)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, 6, 6)
            MainClass.ProtectCell(SprdMain, 2, 2, 5, 5)
            MainClass.ProtectCell(SprdMain, 3, 3, 4, 4)
            MainClass.ProtectCell(SprdMain, 4, 4, 3, 5)
        Else
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, 1)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, 3, 6)
            '        MainClass.ProtectCell SprdMain, 2, 2, 5, 5
            '        MainClass.ProtectCell SprdMain, 3, 3, 4, 4
            '        MainClass.ProtectCell SprdMain, 4, 4, 3, 5
        End If

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsChallanDet.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowDetail1(ByRef mMKey As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM FIN_GSTCHALLAN_DET " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND REF_NO=" & Val(txtRefNo.Text) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallanDet, ADODB.LockTypeEnum.adLockReadOnly)

        With RsChallanDet
            If .EOF = True Then Exit Sub
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = 2
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("TAX_PAYABLE").Value), 0, .Fields("TAX_PAYABLE").Value), "0.00")

                SprdMain.Col = 3
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("PAID_FROM_IGST").Value), 0, .Fields("PAID_FROM_IGST").Value), "0.00")

                SprdMain.Col = 4
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("PAID_FROM_CGST").Value), 0, .Fields("PAID_FROM_CGST").Value), "0.00")

                SprdMain.Col = 5
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("PAID_FROM_SGST").Value), 0, .Fields("PAID_FROM_SGST").Value), "0.00")

                SprdMain.Col = 6
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("PAID_FROM_CESS").Value), 0, .Fields("PAID_FROM_CESS").Value), "0.00")

                SprdMain.Col = 7
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("CASH_PAID").Value), 0, .Fields("CASH_PAID").Value), "0.00")

                SprdMain.Col = 8
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("INTEREST_AMT").Value), 0, .Fields("INTEREST_AMT").Value), "0.00")

                SprdMain.Col = 9
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("LATE_FEE").Value), 0, .Fields("LATE_FEE").Value), "0.00")

                .MoveNext()

                I = I + 1
                '            SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Function AutoGenSeqBillNo() As String

        On Error GoTo AutoGenSeqBillNoErr
        Dim RsChallanGen As ADODB.Recordset
        Dim mNewSeqNo As Double
        SqlStr = ""


        SqlStr = "SELECT Max(REF_NO)  FROM FIN_GSTCHALLAN_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(REF_NO, LENGTH(REF_NO) - 5, 4) = " & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallanGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsChallanGen
            If .EOF = False Then
                If Not IsDbNull(.Fields(0).Value) Then
                    mNewSeqNo = CDbl(Mid(.Fields(0).Value, 1, Len(.Fields(0).Value) - 6))
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = 1 '' 1
                End If
            End If
        End With
        AutoGenSeqBillNo = mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")

        Exit Function
AutoGenSeqBillNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mLockBookCode As Integer
        Dim cntRow As Integer
        Dim mPayable As Double
        Dim mRecoverable As Double

        FieldsVarification = True
        If ValidateBranchLocking((txtRefDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If CheckVoucherUnLockApproval(PubDBCn, MainClass.AllowSingleQuote(Trim(txtVType.Text)) & Trim(txtVNo.Text), (txtVDate.Text), "") = False Then
            If ValidateBookLocking(PubDBCn, CInt(ConLockJournal), (txtVDate.Text)) = True Then
                FieldsVarification = False
                Exit Function
            End If
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsChallan.EOF = True Then Exit Function

        If MODIFYMode = True And txtRefNo.Text = "" Then
            MsgInformation("Ref No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtRefDate.Text = "" Then
            MsgBox("txtRefDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtRefDate.Focus()
            Exit Function
        ElseIf FYChk((txtRefDate.Text)) = False Then
            FieldsVarification = False
            If txtRefDate.Enabled = True Then txtRefDate.Focus()
            Exit Function
        End If

        If Trim(txtChallanNo.Text) = "" Then
            MsgInformation("Challan No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtChallanDate.Text = "" Then
            MsgBox("Challan Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtChallanDate.Focus()
            Exit Function
        ElseIf Not IsDate(txtChallanDate.Text) Then
            MsgBox("Invalid Challan Date", MsgBoxStyle.Information)
            FieldsVarification = False
            txtChallanDate.Focus()
            Exit Function
        End If

        If Val(txtTotAmount.Text) = 0 Then
            MsgInformation("Total Amount. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtBSRCode.Text) = "" Then
            MsgInformation("BSR Code is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtVDate.Text = "" Then
            MsgBox("Vourcher Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtVDate.Focus()
            Exit Function
        ElseIf FYChk((txtVDate.Text)) = False Then
            FieldsVarification = False
            If txtVDate.Enabled = True Then txtVDate.Focus()
            Exit Function
        End If

        With SprdMain
            For cntRow = 1 To 3
                mPayable = 0
                mRecoverable = 0

                .Row = cntRow
                .Col = 2
                mPayable = Val(.Text)

                .Col = 3
                mRecoverable = Val(.Text)

                .Col = 4
                mRecoverable = mRecoverable + Val(.Text)

                .Col = 5
                mRecoverable = mRecoverable + Val(.Text)

                .Col = 7
                mRecoverable = mRecoverable + Val(.Text)

                If VB6.Format(mPayable, "0.00") <> VB6.Format(mRecoverable, "0.00") Then
                    If MsgQuestion("Payable (" & mPayable & ") & Paid (" & mRecoverable & ") is not Matched, Want to Continue ? ") = CStr(MsgBoxResult.No) Then
                        '                MsgInformation "Payable (" & mPayable & ") & Paid (" & mRecoverable & ") is not Matched, So Can't be Save."
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub frmGSTChallan_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblIsReverseCharge.Text = "N" Then
            Me.Text = "GST Challan (Other Than Reverse Charge) Entry Form"
        Else
            Me.Text = "GST Challan (Reverse Charge) Entry Form"
        End If

        SqlStr = ""
        SqlStr = "Select * from FIN_GSTCHALLAN_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallan, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from FIN_GSTCHALLAN_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallanDet, ADODB.LockTypeEnum.adLockReadOnly)


        Call AssignGrid(False)
        Call SetTextLengths()

        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        SqlStr = ""


        SqlStr = "SELECT " & vbCrLf & " REF_NO, REF_DATE, " & vbCrLf & " CHALLANNO, CHALLANDATE, " & vbCrLf & " TOTALAMOUNT, BSR_CODE "

        SqlStr = SqlStr & vbCrLf & " FROM " & vbCrLf & " FIN_GSTCHALLAN_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " And FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        SqlStr = SqlStr & vbCrLf & " AND IS_RC='" & Trim(lblIsReverseCharge.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " Order by REF_NO, REF_DATE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 600)

            .set_ColWidth(0, 600)


            .set_ColWidth(1, 800)
            .set_ColWidth(2, 1000)
            .set_ColWidth(3, 800)
            .set_ColWidth(4, 1200)
            .set_ColWidth(5, 1200)
            .set_ColWidth(6, 1200)
            .set_ColWidth(7, 1200)
            .set_ColWidth(8, 1200)
            .set_ColWidth(9, 1200)
            .set_ColWidth(10, 1200)
            .set_ColWidth(11, 3000)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsChallan

            txtRefNo.Maxlength = .Fields("REF_NO").DefinedSize
            txtRefDate.Maxlength = 10
            txtChallanNo.Maxlength = .Fields("CHALLANNO").DefinedSize
            txtChallanDate.Maxlength = 10
            txtTotAmount.Maxlength = .Fields("TOTALAMOUNT").Precision
            txtBSRCode.Maxlength = .Fields("BSR_CODE").DefinedSize
            txtRemarks.Maxlength = .Fields("NARRATION").DefinedSize
        End With
        Exit Sub
ERR1:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mType As String
        Dim mStatus As String
        Dim mVoucherMKEY As String

        Dim mSqlStr As String
        Dim RsMisc As ADODB.Recordset = Nothing

        With RsChallan
            If Not .EOF Then
                lblMkey.Text = .Fields("REF_NO").Value

                txtRefNo.Text = IIf(IsDbNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)
                txtRefDate.Text = IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value)
                txtChallanNo.Text = IIf(IsDbNull(.Fields("CHALLANNO").Value), "", .Fields("CHALLANNO").Value)
                txtChallanDate.Text = IIf(IsDbNull(.Fields("CHALLANDATE").Value), "", .Fields("CHALLANDATE").Value)
                txtTotAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTALAMOUNT").Value), "", .Fields("TOTALAMOUNT").Value), "0.00")
                txtBSRCode.Text = IIf(IsDbNull(.Fields("BSR_CODE").Value), "", .Fields("BSR_CODE").Value)
                txtRemarks.Text = IIf(IsDbNull(.Fields("NARRATION").Value), "", .Fields("NARRATION").Value)
                mStatus = IIf(IsDbNull(.Fields("AC_POST").Value), "N", .Fields("AC_POST").Value)

                chkAccountPost.CheckState = IIf(mStatus = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkAccountPost.Enabled = IIf(mStatus = "Y", False, True)

                mVoucherMKEY = IIf(IsDbNull(.Fields("BANKVOUCHERMKEY").Value), "", .Fields("BANKVOUCHERMKEY").Value)
                lblBankMKey.Text = mVoucherMKEY

                mSqlStr = "SELECT IH.VNO, VTYPE,VNOSEQ,VNOSUFFIX, IH.VDATE, CMST.SUPP_CUST_NAME  " & vbCrLf & " FROM FIN_VOUCHER_HDR IH, FIN_SUPP_CUST_MST CMST" & vbCrLf & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY='" & mVoucherMKEY & "'" & vbCrLf & " AND IH.COMPANY_CODE = CMST.COMPANY_CODE " & vbCrLf & " AND IH.BOOKCODE =" & ConJournalBookCode & " "

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)

                If RsMisc.EOF = False Then
                    txtVType.Text = IIf(IsDbNull(RsMisc.Fields("VTYPE").Value), "", RsMisc.Fields("VTYPE").Value)
                    txtVNo.Text = VB6.Format(IIf(IsDbNull(RsMisc.Fields("VNOSEQ").Value), 0, RsMisc.Fields("VNOSEQ").Value), "00000")
                    '                txtVNoSuffix.Text = IIf(IsNull(RsMisc.Fields("VNOSUFFIX").Value), "", RsMisc.Fields("VNOSUFFIX").Value)
                    txtVDate.Text = VB6.Format(IIf(IsDbNull(RsMisc.Fields("VDATE").Value), "", RsMisc.Fields("VDATE").Value), "DD/MM/YYYY")
                    txtVDate.Enabled = IIf(mStatus = "Y", False, True)
                End If

                Call ShowDetail1((lblMkey.Text))
            End If
        End With
        txtRefNo.Enabled = True
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsChallan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        AdoDCMain.Refresh
            FormatSprdView()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsChallan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        lblMkey.Text = ""

        txtRefNo.Text = ""
        txtRefDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtChallanNo.Text = ""
        txtChallanDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtTotAmount.Text = "0.00"
        txtBSRCode.Text = ""
        txtRemarks.Text = ""
        txtVType.Text = ""
        txtVNo.Text = ""
        txtVDate.Text = ""

        txtVType.Enabled = False
        txtVNo.Enabled = False
        txtVDate.Enabled = True
        lblBankMKey.Text = ""
        chkAccountPost.Enabled = True
        chkAccountPost.CheckState = System.Windows.Forms.CheckState.UnChecked
        ClearGrid1()
        Call FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsChallan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmGSTChallan_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmGSTChallan_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmGSTChallan_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(6945) '8000
        'Me.Width = VB6.TwipsToPixelsX(11355) '11900

        '    Me.Height = 4920
        '    Me.Width = 9255
        'AdoDCMain.Visible = False


        txtChallanDate.Enabled = True

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtChallanDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallanDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChallanNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallanNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChallanNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChallanNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtChallanNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text) ' MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtVDate.Text) = "" Then
            GoTo EventExitSub
        End If

        If Not IsDate(txtVDate.Text) Then
            MsgBox("Not a Valid Voucher Date.", MsgBoxStyle.Critical)
            txtVDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If

        If Not FYChk((txtVDate.Text)) Then
            txtVDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtVno_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
End Class
