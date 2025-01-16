Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility


Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Configuration
Imports System.Drawing.Color

Imports System.Drawing
Imports System.Drawing.Printing
Friend Class frmProvisionVoucher
    Inherits System.Windows.Forms.Form
    Private RsTRNMain As ADODB.Recordset '' ADODB.Recordset			
    Private RsTRNDetail As ADODB.Recordset ''ADODB.Recordset			
    Private XRIGHT As String
    Private ADDMode As Boolean
    Private MODIFYMode As Boolean
    Private FormLoaded As Boolean

    Private CurMKey As String
    Dim mRowNo As Integer
    '''Private PvtDBCn As ADODB.Connection			


    Private Const ColPRRowNo As Short = 1
    Private Const ColDC As Short = 2
    Private Const ColAccountName As Short = 3
    Private Const ColParticulars As Short = 4
    Private Const ColChequeNo As Short = 5
    Private Const ColChequeDate As Short = 6
    Private Const ColEmp As Short = 7
    Private Const ColDept As Short = 8
    Private Const ColCC As Short = 9
    Private Const ColExp As Short = 10
    Private Const ColDivisionCode As Short = 11
    Private Const ColIBRNo As Short = 12
    Private Const ColAmount As Short = 13
    Private Const ColClearDate As Short = 14

    Private Const ConRowHeight As Short = 22
    Dim mAuthorised As String
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer


    Private Function CheckExpHead(ByRef mAcctName As String) As Boolean
        On Error GoTo ErrPart
        Dim Sqlstr As String
        Dim RsTemp As ADODB.Recordset

        CheckExpHead = False

        Sqlstr = "Select BSGROUP.BSGROUP_ACCTTYPE " & vbCrLf & " FROM FIN_SUPP_CUST_MST,FIN_GROUP_MST ACMGROUP, " & vbCrLf & " FIN_BSGROUP_MST BSGROUP WHERE " & vbCrLf & " FIN_SUPP_CUST_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_SUPP_CUST_MST.COMPANY_CODE=ACMGROUP.COMPANY_CODE" & vbCrLf & " AND BSGROUP.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE" & vbCrLf & " AND FIN_SUPP_CUST_MST.GROUPCODE=GROUP_Code " & vbCrLf & " AND GROUP_BSCodeDr=BSGROUP_Code " & vbCrLf & " AND BSGROUP_ACCTTYPE IN (" & ConIncome & "," & ConExpenses & ")" & vbCrLf & " AND FIN_SUPP_CUST_MST.SUPP_CUST_NAME = '" & MainClass.AllowSingleQuote(Trim(mAcctName)) & "'"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckExpHead = True
        Else
            CheckExpHead = False
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        CheckExpHead = False
    End Function

    Private Function CheckGroupHead(ByRef mAcctName As String) As String
        On Error GoTo ErrPart
        Dim Sqlstr As String
        Dim RsTemp As ADODB.Recordset

        CheckGroupHead = ""

        Sqlstr = "Select ACMGROUP.GROUP_HEAD " & vbCrLf & " FROM FIN_SUPP_CUST_MST,FIN_GROUP_MST ACMGROUP, " & vbCrLf & " FIN_BSGROUP_MST BSGROUP WHERE " & vbCrLf & " FIN_SUPP_CUST_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_SUPP_CUST_MST.COMPANY_CODE=ACMGROUP.COMPANY_CODE" & vbCrLf & " AND BSGROUP.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE" & vbCrLf & " AND FIN_SUPP_CUST_MST.GROUPCODE=GROUP_Code " & vbCrLf & " AND GROUP_BSCodeDr=BSGROUP_Code " & vbCrLf & " AND BSGROUP_ACCTTYPE IN (" & ConIncome & "," & ConExpenses & ")" & vbCrLf & " AND FIN_SUPP_CUST_MST.SUPP_CUST_NAME = '" & MainClass.AllowSingleQuote(Trim(mAcctName)) & "'"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckGroupHead = IIf(IsDBNull(RsTemp.Fields("GROUP_HEAD").Value), "", RsTemp.Fields("GROUP_HEAD").Value)
        Else
            CheckGroupHead = ""
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        CheckGroupHead = ""
    End Function

    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        If cmdAdd.Text = ConCmdAddCaption Then
            Clear1()

            ADDMode = True
            MODIFYMode = False
            cmdAdd.Text = ConCmdCancelCaption
            SprdMain.Enabled = True
            MainClass.SetFocusToCell(SprdMain, 1, ColDC)
            Clear1()
        Else
            ADDMode = False
            MODIFYMode = False
            cmdAdd.Text = ConCmdAddCaption
            Clear1()
            Show1()
        End If
    End Sub

    Private Sub Clear1()
        On Error GoTo ERR1
        Dim Sqlstr As String

        TxtVDate.Text = IIf(TxtVDate.Text = "", RunDate, TxtVDate.Text)
        txtExpDate.Text = IIf(TxtVDate.Text = "", RunDate, TxtVDate.Text)

        TxtVDate.Enabled = True
        txtExpDate.Enabled = True
        txtPopulateVNo.Text = ""
        txtPopulateVNo.Enabled = True
        Call GenPrefixVNo()


        txtVType.Text = "H" ''IIf(Trim(txtVType.Text) = "", GetVType, Trim(txtVType.Text))			


        txtVno.Text = ""
        txtVno.Enabled = True
        txtVNoSuffix.Text = ""

        txtNarration.Text = ""
        LblDrAmt.Text = ""
        LblCrAmt.Text = ""
        LblNetAmt.Text = ""

        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked

        '    If InStr(1, XRIGHT, "M") = 0 Then			
        '        chkCancelled.Enabled = False			
        '    Else			
        chkCancelled.Enabled = True
        '    End If			

        MainClass.ClearGrid(SprdMain)


        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""

        mAuthorised = "N"

        CurMKey = ""
        '    lblSR.Caption = ""			

        SprdMain.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsTRNMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdAuthorised_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAuthorised.Click
        On Error GoTo ErrPart
        Dim Sqlstr As String
        Dim mAccountCode As String
        Dim mLockBookCode As Integer

        If ValidateBranchLocking((TxtVDate.Text)) = True Then
            Exit Sub
        End If

        mLockBookCode = CInt(ConLockProvision)

        If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
            Exit Sub
        End If
        '			
        '     If RsTRNMain.Fields("AUTHORISED").Value = "Y" Then			
        '        MsgBox "Already Authorised.", vbInformation			
        '        cmdAuthorised.Enabled = False			
        '        Exit Sub			
        '    End If			

        If MsgQuestion("Want to Authorised Such Voucher. Once Authorised Cann't be Deleted or Modified.") = CStr(MsgBoxResult.No) Then Exit Sub

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        Sqlstr = "UPDATE FIN_PROVISION_HDR SET " & vbCrLf _
            & " UPDATE_FROM='H'," & vbCrLf _
            & " AUTHORISED='Y', " & vbCrLf _
            & " AUTHORISED_CODE='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
            & " AUTHORISED_DATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
            & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        Sqlstr = Sqlstr & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND MKEY='" & RsTRNMain.Fields("mKey").Value & "'           "

        PubDBCn.Execute(Sqlstr)

        PubDBCn.CommitTrans()

        TxtVNo_Validating(txtVno, New System.ComponentModel.CancelEventArgs(False))

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Information)
        PubDBCn.RollbackTrans()
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        On Error Resume Next
        '    If PvtDBCn.State = adStateOpen Then			
        '        'PvtDBCn.Close			
        '        'Set PvtDBCn = Nothing			
        '    End If			

        If ADDMode = True Or MODIFYMode = True Then
            If MsgQuestion("Are you sure to Exit") = CStr(MsgBoxResult.No) Then
                Exit Sub
            End If
        End If

        RsTRNMain.Close()
        RsTRNMain = Nothing

        RsTRNDetail.Close()
        RsTRNDetail = Nothing
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        Dim Sqlstr As String
        On Error GoTo DelErrPart
        Dim ii As Integer
        Dim mVnoStr As String
        Dim mAccountCode As String
        Dim mLockBookCode As Integer
        Dim mIsCapital As String
        Dim pTDSChallanNo As String
        Dim pClaimNo As String
        Dim mChequeNo As String
        Dim VMkey As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        If ValidateBranchLocking((TxtVDate.Text)) = True Then
            Exit Sub
        End If

        mLockBookCode = CInt(ConLockProvision)


        If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
            Exit Sub
        End If

        With SprdMain
            For ii = 1 To .MaxRows
                .Row = ii
                .Col = ColAccountName
                mAccountCode = Trim(.Text)
                If ValidateAccountLocking(PubDBCn, TxtVDate.Text, mAccountCode) = True Then
                    Exit Sub
                End If
            Next
        End With

        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("You Cann't Delete Cancelled Voucher", MsgBoxStyle.Information)
            Exit Sub
        End If

        '    If RsTRNMain!Authorised = "Y" Then			
        '        MsgBox "You Cann't Delete Authorised Voucher", vbInformation			
        '        Exit Sub			
        '    End If			

        If MainClass.GetUserCanModify((TxtVDate.Text)) = False Then
            MsgBox("You Have Not Rights to delete back Voucher", MsgBoxStyle.Information)
            Exit Sub
        End If

        If Trim(txtVno.Text) = "" Then
            MsgBox("Nothing to Delete.", MsgBoxStyle.Information)
            Exit Sub
        End If

        If MsgQuestion("Want to Delete the Complete Voucher") = CStr(MsgBoxResult.No) Then Exit Sub

        mVnoStr = txtVNo1.Text & Trim(txtVType.Text) & txtVno.Text & txtVNoSuffix.Text
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If InsertIntoDelAudit(PubDBCn, "FIN_PROVISION_HDR", mVnoStr, RsTRNMain, "", "D") = False Then GoTo DelErrPart
        If InsertIntoDelAudit(PubDBCn, "FIN_PROVISION_DET", mVnoStr, RsTRNDetail, "", "D") = False Then GoTo DelErrPart

            If InsertIntoDeleteTrn(PubDBCn, "FIN_PROVISION_HDR", "MKEY", RsTRNMain.Fields("mKey").Value) = False Then GoTo DelErrPart

        Sqlstr = "Delete From FIN_PROVISION_DET Where Mkey='" & RsTRNMain.Fields("mKey").Value & "'"
        PubDBCn.Execute(Sqlstr)

        Sqlstr = "Delete From FIN_PROVISION_HDR Where Mkey='" & RsTRNMain.Fields("mKey").Value & "'"
        PubDBCn.Execute(Sqlstr)

        Sqlstr = "DELETE FROM FIN_PROVISION_TRN  WHERE Mkey='" & RsTRNMain.Fields("mKey").Value & "'" & vbCrLf & " AND BookType='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND BooksubType='" & VB.Right(lblBookType.Text, 1) & "' "
        PubDBCn.Execute(Sqlstr)

        PubDBCn.CommitTrans()

        RsTRNMain.Requery() ''refresh			
        Clear1()
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''			
        RsTRNMain.Requery() ''RsTRNMain.Refresh			
        RsTRNDetail.Requery() ''.Refresh			
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume			
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click
        Dim mIsAuthorisedUser As String

        If cmdModify.Text = ConcmdmodifyCaption Then

            If RsTRNMain.Fields("CANCELLED").Value = "Y" Then
                MsgBox("You Cann't Modify Cancelled Voucher", MsgBoxStyle.Information)
                Exit Sub
            End If

            '        mIsAuthorisedUser = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)			
            '        If InStr(1, mIsAuthorisedUser, "S") = 0 Then			
            '            If RsTRNMain.Fields("AUTHORISED").Value = "Y" Then			
            '                MsgBox "You Cann't Modify Authorised Voucher", vbInformation			
            '                Exit Sub			
            '            End If			
            '        End If			


            '        If PubSuperUser = "U" Then			
            '            If Trim(UCase(RsTRNMain!ADDUSER)) = Trim(UCase(PubUserID)) Then			
            '                MsgBox "Same User Cann't be Modify Voucher", vbInformation			
            '                Exit Sub			
            '            End If			
            '        End If			

            ADDMode = False
            MODIFYMode = True

            cmdModify.Text = ConCmdCancelCaption
            SprdMain.Enabled = True

            txtVno.Enabled = True '''' 1/5/2003   IIf(PubUserID = "ADMIN", True, False)			
        Else
            cmdModify.Text = ConcmdmodifyCaption
            ADDMode = False
            MODIFYMode = False
            SprdMain.Enabled = False
            txtVno.Enabled = True
            ''Show1			
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsTRNMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        cmdAuthorised.Enabled = IIf(mAuthorised = "Y", False, cmdAuthorised.Enabled)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnTrnVoucher(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnTrnVoucher(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnTrnVoucher(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mBranchCode As Integer
        Dim mCategoryCode As Integer
        Dim mVNO As String
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mBookCode As String
        Dim Sqlstr As String
        Dim mMultiLine As Boolean
        Dim mRptFileName As String
        Dim cntRow As Integer
        Dim mNarration As String
        Dim mAccountName As String
        Dim mNarrDetail As String
        Dim mChequeNo As String
        Dim mNarrAcct As String
        Dim mDCType As String
        Dim mBankName As String
        Dim mPartyOpBal As String
        Dim pOpBal As Double
        Dim mAccountCode As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        Sqlstr = ""
        mVNO = Trim(txtVType.Text) & txtVNo1.Text & Trim(txtVno.Text) & Trim(txtVNoSuffix.Text)
        mBookType = lblBookType.Text

        ''if voucher is not Journal..			

        mBookCode = CStr(ConProvisionBookCode)

        mSubTitle = ""
        cntRow = 0

        Call MainClass.ClearCRptFormulas(Report1)


        mTitle = "Provisional Voucher"
        mNarration = "Narration : " & txtNarration.Text '' mNarrDetail			


        mNarration = VB.Left(mNarration, 254)
        Call SelectQryForVoucher(Sqlstr, mVNO, CDate(TxtVDate.Text), mBookType, mBookCode)
        mRptFileName = "TrnProvVoucher.rpt"

        mTitle = mTitle & IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, " (CANCELLED )", "")

        If ShowReport(Sqlstr, Mode, mTitle, mSubTitle, mRptFileName, mNarration, mBankName, mNarrAcct, mPartyOpBal) = False Then GoTo ERR1

        Exit Sub
ERR1:
        If Err.Number <> 0 Then
            MsgInformation(Err.Number & " : " & Err.Description)
        End If
        'Resume			
        frmPrintVoucher.Close()
    End Sub
    Private Function ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef mNarration As String, ByRef mBankName As String, ByRef mAccountName As String, ByRef pPartyOpBal As String) As Boolean
        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mReceivedBy As String
        Dim mDrCrNo As String
        Dim mVoucherAmount As Double

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        MainClass.AssignCRptFormulas(Report1, "Narration=""" & mNarration & """")
        MainClass.AssignCRptFormulas(Report1, "BankName=""" & mBankName & """")

        mVoucherAmount = GetVoucherNetAmount()

        MainClass.AssignCRptFormulas(Report1, "ReceivedBy=""" & mReceivedBy & """")
        mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(CStr(mVoucherAmount)) = 0, 0, mVoucherAmount)))

        MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        ShowReport = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        ShowReport = False
    End Function
    Private Function SelectQryForVoucher(ByRef mSqlStr As String, ByRef mVNO As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef mBookCode As String) As String

        mSqlStr = " SELECT TRN.VNO,TRN.VDATE,TRN.BOOKTYPE,TRN.BOOKSUBTYPE, " & vbCrLf _
            & " TRN.ACCOUNTCODE,TRN.BOOKCODE,TRN.NARRATION, " & vbCrLf _
            & " TRN.AMOUNT,TRN.DC,TRN.CHEQUENO,TRN.CHQDATE, " & vbCrLf _
            & " TRN.IBRNO,TRN.CLEARDATE, " & vbCrLf _
            & " A.SUPP_CUST_Name,A.SUPP_CUST_NAME " & vbCrLf _
            & " FROM FIN_PROVISION_TRN TRN,FIN_SUPP_CUST_MST A" & vbCrLf _
            & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf _
            & " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf _
            & " TRN.COMPANY_CODE=A.COMPANY_CODE AND " & vbCrLf _
            & " TRN.AccountCode=A.SUPP_CUST_CODE AND" & vbCrLf _
            & " VNO='" & mVNO & "' AND " & vbCrLf _
            & " VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & vbCrLf _
            & " BookType='" & Mid(mBookType, 1, 1) & "' AND " & vbCrLf _
            & " BookSubType='" & Mid(mBookType, 2, 1) & "' "

        mSqlStr = mSqlStr & vbCrLf & " ORDER BY TRN.SubRowNo"

        SelectQryForVoucher = mSqlStr
    End Function


    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo SaveErrPart
        Dim RsTemp As ADODB.Recordset
        Dim Sqlstr As String
        Dim mVNO As String
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        If FieldsVerification() = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1() = True Then
            TxtVNo_Validating(txtVno, New System.ComponentModel.CancelEventArgs(False))
        Else
            MsgInformation("Record not Saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
SaveErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)
        On Error GoTo ErrPart

        With SprdMain
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = ColPRRowNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColDC
            .CellType = SS_CELL_TYPE_EDIT
            '        If lblBookType.Caption = ConCashReceipt Or lblBookType.Caption = ConBankReceipt Or lblBookType.Caption = ConPDCReceipt Then			
            '            .Text = "Cr"			
            '        ElseIf lblBookType.Caption = ConCashPayment Or lblBookType.Caption = ConBankPayment Or lblBookType.Caption = ConPDCPayment Then			
            .Text = "Dr"
            '        End If			
            .set_ColWidth(ColDC, 3)

            .Col = ColAccountName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColAccountName, 35)

            .Col = ColParticulars
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsTRNDetail.Fields("PARTICULARS").DefinedSize ''			
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColParticulars, 30)

            .Col = ColChequeNo
            .TypeEditLen = RsTRNDetail.Fields("ChequeNo").DefinedSize ''			
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = False
            .set_ColWidth(ColChequeNo, 8)
            .ColHidden = True


            .Col = ColChequeDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(ColChequeDate, 8)
            .ColHidden = True

            .Col = ColExp
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColExp, 4)

            .Col = ColDivisionCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .TypeEditCharSet = FPSpreadADO.TypeEditCharSetConstants.TypeEditCharSetNumeric
            .set_ColWidth(ColDivisionCode, 4)

            .Col = ColCC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColCC, 4)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColDept, 4)

            .Col = ColEmp
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColEmp, 5)

            .Col = ColIBRNo
            .TypeEditLen = RsTRNDetail.Fields("IBRNo").DefinedSize ''			
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColAmount, 15)

            .Col = ColClearDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = 10
            .ColHidden = True

        End With

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ErrPart:
        'Resume			
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub ViewGrid()
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh			
            SprdView.Refresh()
            FormatSprdView()
            SprdView.Focus()
            fraGridView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            fraGridView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsTRNMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmProvisionVoucher_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmProvisionVoucher_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")

        If ADDMode = True Or MODIFYMode = True Then
            If KeyAscii = System.Windows.Forms.Keys.Escape Then cmdClose_Click(cmdClose, New System.EventArgs())
        End If
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub frmProvisionVoucher_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ERR1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection			
        ''PvtDBCn.Open StrConn		


        CurrFormHeight = 7245
        CurrFormWidth = 11355
        Call SetMainFormCordinate(Me)
        ADDMode = False
        MODIFYMode = False
        FormLoaded = False
        mAuthorised = "N"
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        TxtVDate.Text = IIf(TxtVDate.Text = "", RunDate, TxtVDate.Text)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MainClass.SetControlsColor(Me)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume			
    End Sub

    Public Sub frmProvisionVoucher_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ErrPart
        Dim Sqlstr As String

        SprdMain.Refresh()
        If FormLoaded = True Then Exit Sub
        FormLoaded = True

        Sqlstr = "Select * From FIN_PROVISION_HDR Where 1=2 "
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRNMain, ADODB.LockTypeEnum.adLockReadOnly)

        Sqlstr = "Select * From FIN_PROVISION_DET Where 1=2 "
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRNDetail, ADODB.LockTypeEnum.adLockReadOnly)


        FormatSprdMain(-1)
        AssignGrid(False)

        InitialiseTRN()
        SetTextLengths()
        '    CalcAccountBal			
        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        Dim RsTDSDetail As ADODB.Recordset
        Dim Sqlstr As String

        Sqlstr = "SELECT * FROM TDS_TRN WHERE 1=2"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDSDetail, ADODB.LockTypeEnum.adLockReadOnly)

        txtVno.MaxLength = RsTRNMain.Fields("VNoSeq").Precision '' .Precision     ''			
        txtVNoSuffix.MaxLength = RsTRNMain.Fields("VNOSUFFIX").DefinedSize ''			
        txtVNo1.MaxLength = RsTRNMain.Fields("VNOPREFIX").DefinedSize ''			
        txtVType.MaxLength = RsTRNMain.Fields("VTYPE").DefinedSize ''			
        txtNarration.MaxLength = RsTRNMain.Fields("NARRATION").DefinedSize ''			

        Exit Sub
ERR1:
        '    Resume			
        ErrorMsg(Err.Description)
    End Sub
    Private Sub InitialiseTRN()
        Dim Sqlstr As String

        Me.Text = "Provisional Voucher"


    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        On Error GoTo ERR1
        Dim Sqlstr As String

        Sqlstr = " Select TO_CHAR(VDATE,'DD/MM/YYYY') as VDate, " & vbCrLf _
            & " VNOPREFIX as VNoPrefix, VTYPE AS VType, " & vbCrLf _
            & " To_CHAR(VnoSeq) as VNoSeq, DECODE(CANCELLED,'Y','<<CANCELLED>>',Vno) as VNo, " & vbCrLf _
            & " VNOSUFFIX as VNoSuffix, FIN_SUPP_CUST_MST.SUPP_CUST_NAME AS  Account_Name, "

        Sqlstr = Sqlstr & vbCrLf _
            & " '' as BankName, FIN_PROVISION_DET.Amount  as Amount " & vbCrLf _
            & " FROM FIN_PROVISION_HDR,FIN_PROVISION_DET, FIN_SUPP_CUST_MST  " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " FIN_PROVISION_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND FIN_PROVISION_HDR.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND FIN_PROVISION_HDR.BookType='" & VB.Left(lblBookType.Text, 1) & "' " & vbCrLf _
            & " AND FIN_PROVISION_HDR.BookSubType='" & VB.Right(lblBookType.Text, 1) & "' " & vbCrLf _
            & " AND FIN_PROVISION_HDR.Mkey=FIN_PROVISION_DET.Mkey " & vbCrLf _
            & " AND FIN_SUPP_CUST_MST.COMPANY_CODE=FIN_PROVISION_HDR.COMPANY_CODE " & vbCrLf _
            & " AND FIN_SUPP_CUST_MST.SUPP_CUST_CODE=FIN_PROVISION_DET.AccountCode"

        Sqlstr = Sqlstr & vbCrLf & " ORDER BY TO_DATE(VDATE,'DD/MM/YYYY'), VNO,FIN_PROVISION_DET.SUBROWNO"

        FormatSprdView()
        MainClass.AssignDataInSprd8(Sqlstr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))

        Exit Sub

ERR1:
        'Resume			
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdView()
        With SprdView
            .Row = -1

            .set_ColWidth(0, 600)
            .set_ColWidth(1, 1100)
            .set_ColWidth(2, 0)
            .set_ColWidth(3, 0)
            .set_ColWidth(4, 0)
            .set_ColWidth(5, 1300)
            .set_ColWidth(6, 0)
            .set_ColWidth(7, 4850)
            .set_ColWidth(8, 0)
            .set_ColWidth(9, 1200)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 450)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '''OperationModeSingle			
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With

    End Sub
    Private Sub frmProvisionVoucher_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next

        If ADDMode = True Or MODIFYMode = True Then
            If MsgQuestion("Are you sure to Exit") = CStr(MsgBoxResult.No) Then
                Exit Sub
            End If
        End If
        RsTRNMain.Close()
        RsTRNMain = Nothing

        RsTRNDetail.Close()
        RsTRNDetail = Nothing
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        On Error GoTo ERR1
        Select Case eventArgs.col
            Case 0
                If eventArgs.row > 0 And SprdMain.Enabled = True Then
                    MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColAccountName)
                    MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                    CalcTots()
                End If
            Case ColAccountName, ColCC, ColDept, ColEmp, ColExp, ColDivisionCode
                If eventArgs.row = 0 Then NameSearch(eventArgs.col, (SprdMain.ActiveRow))
        End Select
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub NameSearch(ByRef Col As Integer, ByRef Row As Integer)
        On Error GoTo ERR1
        Dim Sqlstr As String
        Dim mString As String
        Dim mTableName As String
        Dim mFieldName1 As String
        Dim mFieldName2 As String
        Dim mDeptCode As String


        SprdMain.Row = Row
        SprdMain.Col = Col
        mString = SprdMain.Text
        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        Select Case Col
            Case ColAccountName
                Sqlstr = Sqlstr & " AND STATUS='O'"
                mTableName = "FIN_SUPP_CUST_MST"
                mFieldName1 = "SUPP_CUST_NAME"
                mFieldName2 = "SUPP_CUST_CODE"
            Case ColExp
                If ADDMode = True Then
                    Sqlstr = Sqlstr & " AND STATUS='O'"
                End If

                mTableName = "CST_CENTER_MST"
                mFieldName1 = "COST_CENTER_CODE"
                mFieldName2 = "COST_CENTER_DESC"
            Case ColDivisionCode
                mTableName = "INV_DIVISION_MST"
                mFieldName1 = "DIV_CODE"
                mFieldName2 = "DIV_DESC"
            Case ColCC
                mTableName = "FIN_CCENTER_HDR"
                mFieldName1 = "CC_CODE"
                mFieldName2 = "CC_DESC"
            Case ColDept
                mTableName = "PAY_DEPT_MST"
                mFieldName1 = "DEPT_CODE"
                mFieldName2 = "DEPT_DESC"
            Case ColEmp
                mTableName = "PAY_EMPLOYEE_MST"
                mFieldName1 = "EMP_CODE"
                mFieldName2 = "EMP_NAME"
        End Select

        If Col = ColAccountName Then
            MainClass.SearchGridMaster(mString, mTableName, mFieldName1, mFieldName2,  ,  , Sqlstr)

            If AcName <> "" Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = Col
                SprdMain.Text = AcName
            End If

        ElseIf Col = ColCC Then
            SprdMain.Row = Row
            SprdMain.Col = ColDept
            mDeptCode = SprdMain.Text

            Sqlstr = " SELECT IH.CC_DESC,IH.CC_CODE, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE"

            If Trim(mDeptCode) <> "" Then
                Sqlstr = Sqlstr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'"
            End If

            MainClass.SearchGridMasterBySQL2("", Sqlstr)
            SprdMain.Row = Row
            SprdMain.Col = Col
            If AcName <> "" Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = Col
                SprdMain.Text = AcName1
            End If
        Else
            MainClass.SearchGridMaster("", mTableName, mFieldName2, mFieldName1,  ,  , Sqlstr)

            If AcName <> "" Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = Col
                SprdMain.Text = AcName1
            End If
        End If

        SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(Col, SprdMain.ActiveRow, Col, SprdMain.ActiveRow, False))

        SprdMain.Refresh()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent
        On Error GoTo ERR1

        If SprdMain.ActiveRow <= 0 Then Exit Sub

        Select Case SprdMain.ActiveCol
            Case ColAccountName, ColCC, ColDept, ColEmp, ColExp, ColDivisionCode
                If eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then NameSearch((SprdMain.ActiveCol), (SprdMain.ActiveRow))

            Case ColAmount
                If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColAccountName, ConRowHeight)
                        'FormatSprdMain -1			
                    End If
                End If
        End Select
        eventArgs.keyCode = 9999
        Exit Sub
ERR1:
        ErrorMsg(Err.Description)
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ERR1
        Dim mAmount As Double
        Dim pAccountName As String
        Dim mAccountCode As String
        Dim mOPBal As Double
        Dim mEmpCode As String
        Dim mDiv As Integer

        If eventArgs.newRow = -1 Then Exit Sub
        Select Case eventArgs.col
            Case ColDC
                SprdMain.Col = ColDC
                SprdMain.Row = eventArgs.row
                If UCase(SprdMain.Text) = "DR" Or UCase(SprdMain.Text) = "D" Then
                    SprdMain.Text = "Dr"
                    Exit Sub
                ElseIf UCase(SprdMain.Text) = "CR" Or UCase(SprdMain.Text) = "C" Then
                    SprdMain.Text = "Cr"
                    Exit Sub
                Else
                    SprdMain.Col = ColDC
                    SprdMain.Text = "Dr"
                    Exit Sub
                End If

            Case ColAccountName

                SprdMain.Row = eventArgs.row
                SprdMain.Col = ColDivisionCode
                mDiv = Val(SprdMain.Text)

                SprdMain.Col = ColAccountName
                pAccountName = Trim(SprdMain.Text)
                If CheckAccountName(pAccountName, eventArgs.col, eventArgs.row) = True Then

                End If
                Call FillPRRowNo((SprdMain.ActiveRow))
                MainClass.ValidateWithMasterTable(pAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mAccountCode = MasterNo
                '            If IsDate(TxtVDate.Text) Then			
                mOPBal = GetOpeningBal(mAccountCode, VB6.Format(RunDate, "DD/MM/YYYY"))
                '            End If			
                lblAcBalAmt.Text = VB6.Format(System.Math.Abs(mOPBal), "0.00")
                lblAcBalDC.Text = IIf(mOPBal >= 0, "Dr", "Cr")

                If GetHeadType(pAccountName) = "L" Then
                    SprdMain.Col = ColEmp
                    SprdMain.Row = eventArgs.row
                    If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "EMP_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Text = MasterNo
                    End If

                    mEmpCode = Trim(SprdMain.Text)
                    If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Col = ColDept
                        SprdMain.Text = IIf(Trim(SprdMain.Text) = "", Trim(MasterNo), SprdMain.Text)
                    End If

                    If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "COST_CENTER_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Col = ColCC
                        SprdMain.Text = IIf(Trim(SprdMain.Text) = "", Trim(MasterNo), SprdMain.Text)
                    End If

                End If

            Case ColChequeNo

            Case ColChequeDate

            Case ColExp
                If CheckMst(ColExp, "CST_CENTER_MST", "COST_CENTER_CODE") = False Then
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColDivisionCode
                If CheckMst(ColDivisionCode, "INV_DIVISION_MST", "DIV_CODE") = False Then
                    eventArgs.cancel = True
                    Exit Sub
                End If
'			
'            SprdMain.Row = Row			
'            SprdMain.Col = ColDivisionCode			
'            mDiv = Val(SprdMain.Text)			
'			
'            SprdMain.Col = ColAccountName			
'            pAccountName = Trim(SprdMain.Text)			

'            If pAccountName <> "" Then			
'                If MainClass.ValidateWithMasterTable(pAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany!COMPANY_CODE & "") = True Then			
'                    mAccountCode = MasterNo			
'			
'                    mOPBalDiv = GetOpeningBal(mAccountCode, Format(RunDate, "DD/MM/YYYY"), "", mDiv)			
'			
'                    lblAcBalAmtDiv.Caption = Format(Abs(mOPBalDiv), "0.00")			
'                    lblAcBalDCDiv.Caption = IIf(mOPBalDiv >= 0, "Dr", "Cr")			
'                End If			
'            End If			
            Case ColCC
                If CheckMst(ColCC, "FIN_CCENTER_HDR", "CC_CODE") = False Then
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColDept
                If CheckMst(ColDept, "PAY_DEPT_MST", "DEPT_CODE") = False Then
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColEmp
                If CheckMst(ColEmp, "PAY_EMPLOYEE_MST", "EMP_CODE") = False Then
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColAmount
                '            SprdMain.Col = ColAmount			
                '            SprdMain.Row = Row			
                '            If Val(SprdMain.Text) = 0 Then			
                '                MainClass.SetFocusToCell SprdMain, Row, ColAmount			
                '                Exit Sub			
                '            End If			
                '            Call PayDetailForm(SprdMain.ActiveRow)			

        End Select
        CalcTots()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume			
    End Sub

    Private Sub CalcTots()
        On Error GoTo ErrSprdTotal
        Dim mDAmt As Double
        Dim mCAmt As Double
        Dim mNetAmt As Double
        Dim MTotalAmt As Double
        Dim cntRow As Integer
        Dim mPartyAmt As Double

        mPartyAmt = 0
        For cntRow = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = cntRow

            SprdMain.Col = ColDC
            If VB.Left(SprdMain.Text, 1) = "D" Then
                SprdMain.Col = ColAmount
                mDAmt = mDAmt + Val(SprdMain.Value)
            Else
                SprdMain.Col = ColAmount
                mCAmt = mCAmt + Val(SprdMain.Value)
                If mPartyAmt = 0 Then
                    mPartyAmt = Val(SprdMain.Value)
                End If
            End If
            mNetAmt = System.Math.Abs(mCAmt - mDAmt)
NextRow:
        Next cntRow

        LblDrAmt.Text = VB6.Format(mDAmt, "##,##,##,##0.00")
        LblCrAmt.Text = VB6.Format(mCAmt, "##,##,##,##0.00")
        LblNetAmt.Text = VB6.Format(mNetAmt, "##,##,##,##0.00")

        Exit Sub

ErrSprdTotal:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckMst(ByRef mCol As Integer, ByRef TabName As String, Optional ByRef pCode As String = "") As Boolean
        On Error GoTo ERR1
        Dim mAcctName As String
        Dim mSqlStr As String
        Dim mEmpCode As String

        CheckMst = False

        With SprdMain
            .Row = .ActiveRow
            .Col = ColAccountName
            If Trim(.Text) = "" Then
                CheckMst = True
                Exit Function
            Else
                mAcctName = Trim(.Text)
                ''Validate only if acct is income/expenses ...			
                If CheckExpHead(mAcctName) = True Then
                    .Col = mCol
                    If (UCase(TabName) = UCase("CST_CENTER_MST") Or UCase(TabName) = UCase("FIN_CCENTER_HDR")) And Trim(.Text) = "" Then
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, mCol, "This Field is must.")
                        Exit Function
                    End If
                End If
            End If

            .Col = mCol
            If Trim(.Text) <> "" Then
                mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                If UCase(TabName) = UCase("CST_CENTER_MST") And ADDMode = True Then
                    mSqlStr = mSqlStr & " AND STATUS='O'"
                End If

                If MainClass.ValidateWithMasterTable(.Text, pCode, pCode, TabName, PubDBCn, MasterNo,  , mSqlStr) = False Then
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, .ActiveCol, "Invalid Alias.")
                    Exit Function
                End If

                If UCase(TabName) = "PAY_EMPLOYEE_MST" Then
                    mEmpCode = Trim(.Text)
                    If MainClass.ValidateWithMasterTable(mEmpCode, pCode, "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , mSqlStr) = True Then
                        .Col = ColDept
                        .Text = IIf(Trim(.Text) = "", Trim(MasterNo), .Text)
                    End If

                    If MainClass.ValidateWithMasterTable(mEmpCode, pCode, "COST_CENTER_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , mSqlStr) = True Then
                        .Col = ColCC
                        .Text = IIf(Trim(.Text) = "", Trim(MasterNo), .Text)
                    End If
                End If
            End If

        End With
        CheckMst = True
        Exit Function
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub FillPRRowNo(ByRef mRow As Integer)
        Dim cntRow As Integer
        Dim mMaxRowNo As Integer
        With SprdMain

            .Row = mRow
            .Col = ColPRRowNo
            If Trim(.Text) <> "" Then Exit Sub

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColPRRowNo
                If Val(.Text) > mMaxRowNo Then
                    mMaxRowNo = Val(.Text)
                End If
            Next

            .Row = mRow
            .Col = ColPRRowNo
            .Text = CStr(mMaxRowNo + 1)
        End With
    End Sub

    Private Sub SprdMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdMain.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If SprdMain.ActiveCol = ColAmount Then
            Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(SprdMain.ActiveCol, SprdMain.ActiveRow, ColDC, SprdMain.ActiveCol + 1, False))
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        TxtVDate.Text = SprdView.Text

        SprdView.Col = 2
        txtVNo1.Text = SprdView.Text

        SprdView.Col = 3
        txtVType.Text = SprdView.Text

        SprdView.Col = 6
        txtVNoSuffix.Text = SprdView.Text

        SprdView.Col = 4
        txtVno.Text = VB6.Format(SprdView.Text, "00000")

        TxtVNo_Validating(txtVno, New System.ComponentModel.CancelEventArgs(False))
        If SprdMain.Enabled = True Then SprdMain.Focus()
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub


    Private Sub txtExpDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExpDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExpDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExpDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtExpDate.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtExpDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExpDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtExpDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtExpDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNarration_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNarration.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Function CheckAccountName(ByRef pAccountName As String, ByRef col2 As Integer, ByRef Row2 As Integer) As Boolean
        On Error GoTo ERR1
        Dim Sqlstr As String
        Dim RS As ADODB.Recordset '' ADODB.Recordset			

        CheckAccountName = False
        If pAccountName = "" Then
            Exit Function
        End If

        Sqlstr = " SELECT SUPP_CUST_CODE FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(Trim(pAccountName)) & "'"


        If ADDMode = True Then
            Sqlstr = Sqlstr & vbCrLf & " AND STATUS='O' "
        End If

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = True Then
            MainClass.SetFocusToCell(SprdMain, Row2, col2, "Invalid Account.")
            Exit Function
        End If

        CheckAccountName = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        CheckAccountName = True
        RS.Close()
        RS = Nothing

        Exit Function
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        CheckAccountName = False
        RS.Close()
        RS = Nothing
    End Function

    Private Function CheckDivisionWiseDRCRMatch(ByRef mDRCRBal As Double, ByRef xDivName As String) As Boolean
        On Error GoTo ErrPart
        Dim Sqlstr As String
        Dim RsTemp As ADODB.Recordset
        'Dim mDRCRBal As Double			
        Dim cntRow As Integer
        Dim mDivisionCode As Double
        Dim mCheckDivisionCode As Double
        Dim mAccountName As String
        Dim mDC As String
        Dim mSuppCustDC As String
        Dim mSuppCustAmount As Double
        Dim mPRowNo As Integer

        CheckDivisionWiseDRCRMatch = False

        Sqlstr = "SELECT DIV_CODE,DIV_DESC FROM INV_DIVISION_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY DIV_CODE"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mDivisionCode = IIf(IsDBNull(RsTemp.Fields("DIV_CODE").Value), -1, RsTemp.Fields("DIV_CODE").Value)
                xDivName = IIf(IsDBNull(RsTemp.Fields("DIV_DESC").Value), "", RsTemp.Fields("DIV_DESC").Value)
                mDRCRBal = 0
                With SprdMain
                    For cntRow = 1 To .MaxRows - 1

                        .Row = cntRow
                        .Col = ColAccountName
                        mAccountName = Trim(.Text)

                        .Col = ColPRRowNo
                        mPRowNo = Val(.Text)


                        .Col = ColDivisionCode
                        mCheckDivisionCode = IIf(Val(.Text) <= 0, 1, Val(.Text))

                        If mDivisionCode = mCheckDivisionCode Then
                            .Col = ColDC
                            mDC = UCase(Trim(.Text))

                            .Col = ColAmount
                            mDRCRBal = CDbl(VB6.Format(mDRCRBal + Val(CStr(CDbl(.Text) * IIf(mDC = "DR", 1, -1))), "0.00"))
                        End If

                    Next
                End With

                If mDRCRBal <> 0 Then
                    CheckDivisionWiseDRCRMatch = False
                    Exit Function
                End If
                RsTemp.MoveNext()
            Loop
        End If

        CheckDivisionWiseDRCRMatch = True
        Exit Function
ErrPart:
        'Resume			
        MsgInformation(Err.Description)
        CheckDivisionWiseDRCRMatch = False
    End Function

    Private Sub txtPopulateVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPopulateVNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPopulateVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPopulateVNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPopulateVNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPopulateVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPopulateVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrTxtVno

        If ADDMode = True Then
            CopyVouchExistance()
        End If
        GoTo EventExitSub
ErrTxtVno:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtVDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtVDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtVDate.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(TxtVDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((TxtVDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtVno_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVno.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVno.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Public Sub TxtVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVno.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrTxtVno

        txtVno.Text = VB6.Format(txtVno.Text, "00000")
        CheckVouchExistance()
        GoTo EventExitSub
ErrTxtVno:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Public Sub CheckVouchExistance()
        On Error GoTo ERR1
        Dim mBookCode As String
        Dim mVDate As String
        Dim mVNO As String
        Dim Sqlstr As String


        '    If MainClass.ValidateWithMasterTable(txtPartyName.Text, "Name", "Code", "FIN_SUPP_CUST_MST", PubDBCn, mBookCode) = False Then			
        '        ErrorMsg "Please Select Book First"			
        '        txtPartyName.SetFocus			
        '    End If			

        mVNO = txtVNo1.Text & txtVType.Text & txtVno.Text & txtVNoSuffix.Text
        mVDate = TxtVDate.Text

        If MODIFYMode = True And RsTRNMain.EOF = False Then CurMKey = RsTRNMain.Fields("MKey").Value

        Sqlstr = " Select * From FIN_PROVISION_HDR WHERE " & vbCrLf & " Vno='" & mVNO & "'" & vbCrLf & " AND Booktype='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND BookSubType='" & VB.Right(lblBookType.Text, 1) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRNMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTRNMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            '        Clear1			
            Show1()
            If MODIFYMode = True Then SprdMain.Enabled = True
        End If

        '    Call CalcAccountBal			
        Exit Sub
ERR1:
        ErrorMsg(Err.Description)
        '    Resume			
    End Sub

    Public Sub CopyVouchExistance()
        On Error GoTo ERR1
        Dim mBookCode As String
        Dim mVDate As String
        Dim mVNO As String
        Dim Sqlstr As String
        Dim RsTRNTemp As ADODB.Recordset
        Dim RSTempDetail As ADODB.Recordset
        Dim mKey As String

        mVNO = Trim(txtPopulateVNo.Text)

        Sqlstr = " Select * From FIN_PROVISION_HDR WHERE " & vbCrLf & " Vno='" & mVNO & "'" & vbCrLf & " AND Booktype='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND BookSubType='" & VB.Right(lblBookType.Text, 1) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRNTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTRNTemp.EOF = False Then
            mKey = RsTRNTemp.Fields("mKey").Value
            Clear1()
            Sqlstr = "SELECT FIN_PROVISION_DET.*" & vbCrLf & " FROM FIN_PROVISION_DET WHERE MKEY= '" & mKey & "' Order By SubRowNo"
            MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSTempDetail, ADODB.LockTypeEnum.adLockReadOnly)

            If RSTempDetail.EOF = True Then Exit Sub

            Do While RSTempDetail.EOF = False

                SprdMain.Row = SprdMain.MaxRows

                SprdMain.Col = ColPRRowNo
                SprdMain.Text = Str(IIf(IsDBNull(RSTempDetail.Fields("PRRowNo").Value), 0, RSTempDetail.Fields("PRRowNo").Value))

                SprdMain.Col = ColDC
                SprdMain.Text = RSTempDetail.Fields("DC").Value + "r"


                SprdMain.Col = ColAccountName
                If MainClass.ValidateWithMasterTable(RSTempDetail.Fields("ACCOUNTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If

                '        SprdMain.Text = IIf(IsNull(RsTempDetail.Fields("AccountName").Value), "", RsTempDetail.Fields("AccountName").Value)			

                SprdMain.Col = ColParticulars
                SprdMain.Text = IIf(IsDBNull(RSTempDetail.Fields("PARTICULARS").Value), "", RSTempDetail.Fields("PARTICULARS").Value)

                SprdMain.Col = ColChequeNo
                SprdMain.Text = IIf(Not IsDBNull(RSTempDetail.Fields("ChequeNo").Value), RSTempDetail.Fields("ChequeNo").Value, "")

                SprdMain.Col = ColChequeDate
                SprdMain.Text = VB6.Format(IIf(Not IsDBNull(RSTempDetail.Fields("CHQDATE").Value), RSTempDetail.Fields("CHQDATE").Value, ""), "DD/MM/YYYY")

                SprdMain.Col = ColCC
                If RSTempDetail.Fields("COSTCCODE").Value <> -1 Then
                    '            If MainClass.ValidateWithMasterTable(RsTempDetail.Fields("CostCCode").Value, "COST_CENTER_CODE", "Alias", "CST_CENTER_MST", PubDBCn, MasterNo) = True Then			
                    '                SprdMain.Text = IIf(IsNull(MasterNo), "", MasterNo)			
                    '            End If			
                    SprdMain.Text = IIf(IsDBNull(RSTempDetail.Fields("COSTCCODE").Value), "", RSTempDetail.Fields("COSTCCODE").Value)
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColExp
                If RSTempDetail.Fields("EXP_CODE").Value <> -1 Then
                    SprdMain.Text = IIf(IsDBNull(RSTempDetail.Fields("EXP_CODE").Value), "", RSTempDetail.Fields("EXP_CODE").Value)
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColDivisionCode
                If RSTempDetail.Fields("DIV_CODE").Value <> -1 Then
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(RSTempDetail.Fields("DIV_CODE").Value), "", RSTempDetail.Fields("DIV_CODE").Value)))
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColDept
                If RSTempDetail.Fields("DeptCode").Value <> -1 Then
                    '            If MainClass.ValidateWithMasterTable(RsTempDetail.Fields("DeptCode").Value, "Code", "Alias", "Dept", PubDBCn, MasterNo) = True Then			
                    '                SprdMain.Text = IIf(IsNull(MasterNo), "", MasterNo)			
                    '            End If			
                    SprdMain.Text = IIf(IsDBNull(RSTempDetail.Fields("DeptCode").Value), "", RSTempDetail.Fields("DeptCode").Value)
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColEmp
                If RSTempDetail.Fields("EMPCODE").Value <> -1 Then
                    '            If MainClass.ValidateWithMasterTable(RsTempDetail.Fields("EMPCODE").Value, "Code", "Alias", "Emp", PubDBCn, MasterNo) = True Then			
                    '                SprdMain.Text = IIf(IsNull(MasterNo), "", MasterNo)			
                    '            End If			
                    SprdMain.Text = IIf(IsDBNull(RSTempDetail.Fields("EMPCODE").Value), "", RSTempDetail.Fields("EMPCODE").Value)
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColIBRNo
                SprdMain.Text = IIf(Not IsDBNull(RSTempDetail.Fields("IBRNo").Value), RSTempDetail.Fields("IBRNo").Value, "")

                SprdMain.Col = ColAmount
                SprdMain.Text = Str(RSTempDetail.Fields("Amount").Value)

                SprdMain.Col = ColClearDate
                SprdMain.Text = IIf(IsDBNull(RSTempDetail.Fields("ClearDate").Value), "", RSTempDetail.Fields("ClearDate").Value)

                SprdMain.MaxRows = SprdMain.MaxRows + 1
                RSTempDetail.MoveNext()
            Loop

        End If

        '    Call CalcAccountBal			
        Exit Sub
ERR1:
        ErrorMsg(Err.Description)
        '    Resume			
    End Sub

    Private Sub InsertIntoGrid(ByRef cntRow As Integer, ByRef pDC As String, ByRef pAccountName As String, ByRef pParticulars As String, ByRef pEmpCode As String, ByRef pDeptCode As String, ByRef pCCCode As String, ByRef pExpCode As String, ByRef pAmount As Double, ByRef pDivisionCode As Double)
        On Error GoTo ErrPart
        Dim pRevDC As String

        pRevDC = IIf(UCase(pDC) = "DR", "CR", "DR")

        If pAmount <> 0 Then
            With SprdMain
                .Row = cntRow
                .Col = 1
                .Col = ColPRRowNo
                .Text = CStr(cntRow)

                .Col = ColDC
                .Text = IIf(pAmount < 0, pRevDC, pDC)

                .Col = ColAccountName
                .Text = pAccountName

                .Col = ColParticulars
                .Text = pParticulars

                .Col = ColEmp
                .Text = pEmpCode

                .Col = ColDept
                .Text = pDeptCode

                .Col = ColCC
                .Text = pCCCode

                .Col = ColExp
                .Text = pExpCode

                .Col = ColDivisionCode
                .Text = CStr(pDivisionCode)

                .Col = ColAmount
                .Text = VB6.Format(System.Math.Abs(pAmount), "0.00")

                cntRow = cntRow + 1
                .MaxRows = cntRow
            End With
        End If

        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1()
        On Error GoTo ShowErrPart
        Dim PKey As String
        Dim Sqlstr As String
        Dim RS As ADODB.Recordset '' ADODB.Recordset			
        Dim mOPBal As Double

        Dim mPartyCode As String


        Clear1()
        If RsTRNMain.EOF = True Then Exit Sub

        TxtVDate.Enabled = True

        CurMKey = RsTRNMain.Fields("mKey").Value
        mRowNo = RsTRNMain.Fields("RowNo").Value

        txtVNo1.Text = IIf(IsDBNull(RsTRNMain.Fields("VNoPrefix").Value), "", RsTRNMain.Fields("VNoPrefix").Value)
        txtVType.Text = IIf(IsDBNull(RsTRNMain.Fields("VTYPE").Value), "", RsTRNMain.Fields("VTYPE").Value)
        txtVNoSuffix.Text = IIf(IsDBNull(RsTRNMain.Fields("VNOSUFFIX").Value), "", RsTRNMain.Fields("VNOSUFFIX").Value)

        txtVno.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("VNOSeq").Value), "", RsTRNMain.Fields("VNOSeq").Value), "00000")
        TxtVDate.Text = IIf(IsDBNull(RsTRNMain.Fields("VDate").Value), "", RsTRNMain.Fields("VDate").Value)
        txtExpDate.Text = IIf(IsDBNull(RsTRNMain.Fields("EXPDate").Value), "", RsTRNMain.Fields("EXPDate").Value)

        txtNarration.Text = IIf(IsDBNull(RsTRNMain.Fields("NARRATION").Value), "", RsTRNMain.Fields("NARRATION").Value)

        chkCancelled.CheckState = IIf(RsTRNMain.Fields("CANCELLED").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

        '    If InStr(1, XRIGHT, "M") = 0 Then			
        '        chkCancelled.Enabled = False			
        '    Else			
        chkCancelled.Enabled = True
        '        chkCancelled.Enabled = IIf(RsTRNMain.Fields("CANCELLED").Value = "N", True, False)			
        '    End If			
        mAuthorised = IIf(IsDBNull(RsTRNMain.Fields("Authorised").Value), "N", RsTRNMain.Fields("Authorised").Value)

        lblAddUser.Text = IIf(IsDBNull(RsTRNMain.Fields("ADDUSER").Value), "", RsTRNMain.Fields("ADDUSER").Value)
        lblAddDate.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("ADDDATE").Value), "", RsTRNMain.Fields("ADDDATE").Value), "DD/MM/YYYY")
        lblModUser.Text = IIf(IsDBNull(RsTRNMain.Fields("MODUSER").Value), "", RsTRNMain.Fields("MODUSER").Value)
        lblModDate.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("MODDATE").Value), "", RsTRNMain.Fields("MODDATE").Value), "DD/MM/YYYY")

        '    txtModvatNo.Enabled = False			
        '    txtSTRefundNo.Enabled = False			
        '			
        '    chkSuppBill.Enabled = False			
        '    chkCapital.Enabled = False			

        ShowDetail()
        CalcTots()

        SprdMain.Enabled = False
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsTRNMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        cmdAuthorised.Enabled = IIf(mAuthorised = "Y", False, cmdAuthorised.Enabled)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume			
    End Sub
    Private Sub ShowDetail()
        On Error GoTo ShowErr
        Dim Sqlstr As String


        ', FIN_SUPP_CUST_MST.SUPP_CUST_NAME AS ACCOUNTNAME			
        '',FIN_SUPP_CUST_MST			

        Sqlstr = "SELECT FIN_PROVISION_DET.*" & vbCrLf & " FROM FIN_PROVISION_DET WHERE MKEY= '" & CurMKey & "' Order By SubRowNo" '''& vbCrLf |            & " AND FIN_PROVISION_DET.ACCOUNTCODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE AND " & vbCrLf |            & " "			
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRNDetail, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTRNDetail.EOF = True Then Exit Sub

        Do While RsTRNDetail.EOF = False

            SprdMain.Row = SprdMain.MaxRows

            SprdMain.Col = ColPRRowNo
            SprdMain.Text = Str(IIf(IsDBNull(RsTRNDetail.Fields("PRRowNo").Value), 0, RsTRNDetail.Fields("PRRowNo").Value))

            SprdMain.Col = ColDC
            SprdMain.Text = RsTRNDetail.Fields("DC").Value + "r"


            SprdMain.Col = ColAccountName
            If MainClass.ValidateWithMasterTable(RsTRNDetail.Fields("ACCOUNTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                SprdMain.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
            End If

            '        SprdMain.Text = IIf(IsNull(RsTRNDetail.Fields("AccountName").Value), "", RsTRNDetail.Fields("AccountName").Value)			

            SprdMain.Col = ColParticulars
            SprdMain.Text = IIf(IsDBNull(RsTRNDetail.Fields("PARTICULARS").Value), "", RsTRNDetail.Fields("PARTICULARS").Value)

            SprdMain.Col = ColChequeNo
            SprdMain.Text = IIf(Not IsDBNull(RsTRNDetail.Fields("ChequeNo").Value), RsTRNDetail.Fields("ChequeNo").Value, "")

            SprdMain.Col = ColChequeDate
            SprdMain.Text = VB6.Format(IIf(Not IsDBNull(RsTRNDetail.Fields("CHQDATE").Value), RsTRNDetail.Fields("CHQDATE").Value, ""), "DD/MM/YYYY")

            SprdMain.Col = ColCC
            If RsTRNDetail.Fields("COSTCCODE").Value <> -1 Then
                '            If MainClass.ValidateWithMasterTable(RsTRNDetail.Fields("CostCCode").Value, "COST_CENTER_CODE", "Alias", "CST_CENTER_MST", PubDBCn, MasterNo) = True Then			
                '                SprdMain.Text = IIf(IsNull(MasterNo), "", MasterNo)			
                '            End If			
                SprdMain.Text = IIf(IsDBNull(RsTRNDetail.Fields("COSTCCODE").Value), "", RsTRNDetail.Fields("COSTCCODE").Value)
            Else
                SprdMain.Text = ""
            End If

            SprdMain.Col = ColExp
            If RsTRNDetail.Fields("EXP_CODE").Value <> -1 Then
                SprdMain.Text = IIf(IsDBNull(RsTRNDetail.Fields("EXP_CODE").Value), "", RsTRNDetail.Fields("EXP_CODE").Value)
            Else
                SprdMain.Text = ""
            End If

            SprdMain.Col = ColDivisionCode
            If RsTRNDetail.Fields("DIV_CODE").Value <> -1 Then
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsTRNDetail.Fields("DIV_CODE").Value), "", RsTRNDetail.Fields("DIV_CODE").Value)))
            Else
                SprdMain.Text = ""
            End If

            SprdMain.Col = ColDept
            If RsTRNDetail.Fields("DeptCode").Value <> -1 Then
                '            If MainClass.ValidateWithMasterTable(RsTRNDetail.Fields("DeptCode").Value, "Code", "Alias", "Dept", PubDBCn, MasterNo) = True Then			
                '                SprdMain.Text = IIf(IsNull(MasterNo), "", MasterNo)			
                '            End If			
                SprdMain.Text = IIf(IsDBNull(RsTRNDetail.Fields("DeptCode").Value), "", RsTRNDetail.Fields("DeptCode").Value)
            Else
                SprdMain.Text = ""
            End If

            SprdMain.Col = ColEmp
            If RsTRNDetail.Fields("EMPCODE").Value <> -1 Then
                '            If MainClass.ValidateWithMasterTable(RsTRNDetail.Fields("EMPCODE").Value, "Code", "Alias", "Emp", PubDBCn, MasterNo) = True Then			
                '                SprdMain.Text = IIf(IsNull(MasterNo), "", MasterNo)			
                '            End If			
                SprdMain.Text = IIf(IsDBNull(RsTRNDetail.Fields("EMPCODE").Value), "", RsTRNDetail.Fields("EMPCODE").Value)
            Else
                SprdMain.Text = ""
            End If

            SprdMain.Col = ColIBRNo
            SprdMain.Text = IIf(Not IsDBNull(RsTRNDetail.Fields("IBRNo").Value), RsTRNDetail.Fields("IBRNo").Value, "")

            SprdMain.Col = ColAmount
            SprdMain.Text = Str(RsTRNDetail.Fields("Amount").Value)

            SprdMain.Col = ColClearDate
            SprdMain.Text = IIf(IsDBNull(RsTRNDetail.Fields("ClearDate").Value), "", RsTRNDetail.Fields("ClearDate").Value)

            SprdMain.MaxRows = SprdMain.MaxRows + 1
            RsTRNDetail.MoveNext()
        Loop
        'FormatSprdMain -1			
        Exit Sub
ShowErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume			
    End Sub
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        Dim cntRow As Integer
        Dim mAcctCode As String
        Dim mPRRowNo As Integer
        Dim mAmount As Double
        Dim mDC As String
        Dim ii As Integer
        Dim mEmpCode As String
        Dim mLockBookCode As Integer
        Dim mIsTDSAccount As Boolean
        Dim pTDSChallanNo As String
        Dim pVNo As String
        Dim mServiceClaimCode As String
        Dim mISServiceClaim As Boolean
        Dim pClaimNo As String
        Dim mServiceTaxHeadCount As Integer
        Dim mPartyName As String
        Dim mChequeNo As String
        Dim mIsAuthorisedUser As String
        Dim mPANNo As String
        Dim mHeadType As String
        Dim mDRCRBal As Double
        Dim xDivName As String
        Dim mGroupHead As String

        mIsTDSAccount = False
        '    mISServiceClaim = False			
        FieldsVerification = False

        '    If ValidateBranchLocking(txtVDate.Text) = True Then			
        '        FieldsVerification = False			
        '        Exit Function			
        '    End If			
        '			
        '    mLockBookCode = ConLockProvision			
        '			
        '    If ValidateBookLocking(PubDBCn, mLockBookCode, txtVDate) = True Then			
        '        FieldsVerification = False			
        '        Exit Function			
        '    End If			

        '    If MODIFYMode = True Then			
        '        mIsAuthorisedUser = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)			
        '        If InStr(1, mIsAuthorisedUser, "S") = 0 Then			
        '            If RsTRNMain.Fields("AUTHORISED").Value = "Y" Then			
        '                MsgBox "You Cann't Modify Authorised Voucher", vbInformation			
        '                FieldsVerification = False			
        '                Exit Function			
        '            End If			
        '        End If			
        '    End If			

        '    If MainClass.GetUserCanModify(TxtVDate.Text) = False Then			
        '        MsgBox "You Have Not Rights to change back Voucher", vbInformation			
        '        FieldsVerification = False			
        '        Exit Function			
        '    End If			

        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            If MsgQuestion("Want to Cancelled the Complete Voucher") = CStr(MsgBoxResult.No) Then
                FieldsVerification = False
                Exit Function
            End If
        End If

        With SprdMain
            For ii = 1 To .MaxRows
                .Row = ii
                .Col = ColAccountName
                If Trim(.Text) = "" Then Exit For
                If ValidateAccountLocking(PubDBCn, TxtVDate.Text, .Text) = True Then
                    FieldsVerification = False
                    Exit Function
                End If
            Next
        End With

        If MainClass.ValidDataInGrid(SprdMain, ColAmount, "N", "Please check. Either Amount is Missing all the rows are marked for deletion") = False Then Exit Function

        FieldsVerification = False

        If Trim(txtVType.Text) = "" Then
            MsgInformation("Voucher Type is Blank")
            txtVType.Focus()
            FieldsVerification = False
            Exit Function
        End If

        If Trim(txtVno.Text) <> "" Then
            If Val(txtVno.Text) = 0 Then
                MsgInformation("Invalid Voucher No. Cann't be Saved.")
                txtVno.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If MODIFYMode = True And Trim(txtVno.Text) = "" Then
            MsgInformation("Voucher No. is Blank")
            txtVno.Focus()
            FieldsVerification = False
            Exit Function
        End If


        If FYChk(TxtVDate.Text) = False Then
            '        MsgInformation "Date is not in the Current Financial Year"			
            If TxtVDate.Enabled = True Then TxtVDate.Focus()
            Exit Function
        End If

NextLine:

        If Val(LblNetAmt.Text) <> 0 Then
            MsgInformation("Dr./Cr. Mismatch, Voucher Not Saved")
            Exit Function
        End If

        mDRCRBal = 0
        xDivName = ""
        If CheckDivisionWiseDRCRMatch(mDRCRBal, xDivName) = False Then
            MsgInformation("Division Wise Dr./Cr. Mismatch. Amount Diff is " & mDRCRBal & " in Division : " & xDivName & ". Voucher Not Saved")
            Exit Function
        End If

        For cntRow = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = cntRow

            SprdMain.Col = ColPRRowNo
            mPRRowNo = Val(SprdMain.Text)

            SprdMain.Col = ColAmount
            mAmount = Val(SprdMain.Text)

            SprdMain.Col = ColDC
            mDC = SprdMain.Text

            SprdMain.Col = ColChequeNo
            mChequeNo = SprdMain.Text

            SprdMain.Col = ColAccountName
            If MainClass.ValidateWithMasterTable(SprdMain.Text, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAcctCode = MasterNo
                mGroupHead = CheckGroupHead((SprdMain.Text))

                'If mGroupHead = "I1" Or mGroupHead = "I2" Or mGroupHead = "I3" Or mGroupHead = "I4" Or mGroupHead = "I5" Or mGroupHead = "I7" Or mGroupHead = "I8" Then
                '    MsgInformation("Income Provision Cann't be enter of (" & SprdMain.Text & ")")
                '    MainClass.SetFocusToCell(SprdMain, cntRow, ColAccountName)
                '    Exit Function
                'End If
                'If CheckExpHead((SprdMain.Text)) = True Then
                '    SprdMain.Col = ColCC
                '    If Trim(SprdMain.Text) = "" Then
                '        MsgInformation("Please Check Cost Centre is Missing.")
                '        MainClass.SetFocusToCell(SprdMain, cntRow, ColCC)
                '        Exit Function
                '    End If

                '    SprdMain.Col = ColExp
                '    If Trim(SprdMain.Text) = "" Then
                '        MsgInformation("Please Check Expenses Centre is Missing.")
                '        MainClass.SetFocusToCell(SprdMain, cntRow, ColExp)
                '        Exit Function
                '    End If

                'End If
            Else
                MsgInformation("Invaild Account Name.")
                MainClass.SetFocusToCell(SprdMain, cntRow, ColAccountName)
                Exit Function
            End If

            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SprdMain.Col = ColAccountName
                If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "SUPP_CUST_NAME", "HEADTYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mHeadType = MasterNo
                Else
                    mHeadType = ""
                End If
                '            If GetAccountBalancingMethod(Trim(SprdMain.Text), False) = "D" Then			
                '                If PayDetailExists(mAcctCode, mPRRowNo, mAmount, mDC) = False Then			
                '                    MsgInformation "Payment Detail missing"			
                '                    MainClass.SetFocusToCell SprdMain, cntRow, ColAmount			
                '                    Exit Function			
                '                End If			
                '            End If			
            End If
        Next
        'If MainClass.ValidDataInGrid(SprdMain, ColDivisionCode, "S", "Division Is Blank.") = False Then FieldsVerification = False : Exit Function

        FieldsVerification = True
        Exit Function
ERR1:
        '    Resume			
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FieldsVerification = False
    End Function

    Private Function CheckLastestVDate(ByRef mVDate As Date, ByRef mVType As String) As Boolean
        On Error GoTo CheckLastestVDateErr
        Dim Sqlstr As String
        Dim RsCheck As ADODB.Recordset '' ADODB.Recordset			
        Dim mBookSubType As String
        Dim mBookType As String

        mBookType = VB.Left(lblBookType.Text, 1)
        mBookSubType = VB.Right(lblBookType.Text, 1)

        CheckLastestVDate = True
        Sqlstr = "SELECT VDATE FROM FIN_PROVISION_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf _
            & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf _
            & " BookType='" & mBookType & "' AND " & vbCrLf _
            & " BookSubType='" & mBookSubType & "' AND " & vbCrLf _
            & " VTYPE='" & MainClass.AllowSingleQuote(Trim(mVType)) & "' AND " & vbCrLf _
            & " VDATE>TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCheck, ADODB.LockTypeEnum.adLockReadOnly)

        If RsCheck.EOF = False Then
            CheckLastestVDate = False
        End If
        Exit Function

CheckLastestVDateErr:
        CheckLastestVDate = False
    End Function


    Private Function CheckLastestVNo(ByRef mVNO As Integer, ByRef mVDate As Date, ByRef mVType As String) As Boolean
        On Error GoTo CheckLastestVNoErr
        Dim Sqlstr As String
        Dim RsCheckPVNo As ADODB.Recordset '' ADODB.Recordset			
        Dim RsCheckLVNo As ADODB.Recordset ''ADODB.Recordset			
        Dim mBookSubType As String
        Dim mBookType As String

        CheckLastestVNo = True
        mBookType = VB.Left(lblBookType.Text, 1)
        mBookSubType = VB.Right(lblBookType.Text, 1)

        ''Checl Previous VNO...			
        Sqlstr = "SELECT Max(VNOSeq) AS VNoSeq FROM FIN_PROVISION_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf _
            & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf _
            & " BookType='" & mBookType & "' AND " & vbCrLf _
            & " BookSubType='" & mBookSubType & "' AND " & vbCrLf _
            & " VTYPE='" & MainClass.AllowSingleQuote(Trim(mVType)) & "' AND " & vbCrLf _
            & " VDATE<TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCheckPVNo, ADODB.LockTypeEnum.adLockReadOnly)

        ''Checl Later VNO...			
        Sqlstr = "SELECT Max(VNOSeq) AS VNoSeq FROM FIN_PROVISION_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf _
            & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf _
            & " BookType='" & mBookType & "' AND " & vbCrLf _
            & " BookSubType='" & mBookSubType & "' AND " & vbCrLf _
            & " VTYPE='" & MainClass.AllowSingleQuote(Trim(mVType)) & "' AND " & vbCrLf _
            & " VDATE>TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCheckLVNo, ADODB.LockTypeEnum.adLockReadOnly)

        If RsCheckPVNo.EOF = False Then
            If Val(IIf(IsDBNull(RsCheckPVNo.Fields("VNoSeq").Value), 1, RsCheckPVNo.Fields("VNoSeq").Value)) > Val(CStr(mVNO)) Then
                CheckLastestVNo = False
            End If
        End If

        If RsCheckLVNo.EOF = False Then
            If Val(IIf(IsDBNull(RsCheckPVNo.Fields("VNoSeq").Value), 1, RsCheckPVNo.Fields("VNoSeq").Value)) < Val(CStr(mVNO)) Then
                CheckLastestVNo = False
            End If
        End If
        Exit Function

CheckLastestVNoErr:
        CheckLastestVNo = False
    End Function
    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim mBookCode As String
        Dim Sqlstr As String
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

        Dim i As Integer
        Dim mVDate As String
        Dim mExpDate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mBookType = VB.Left(lblBookType.Text, 1)
        mBookSubType = VB.Right(lblBookType.Text, 1)
        mVType = MainClass.AllowSingleQuote(Trim(txtVType.Text))

        If Trim(txtExpDate.Text) = "" Or Not IsDate(txtExpDate.Text) Then
            txtExpDate.Text = VB6.Format(TxtVDate.Text, "DD/MM/YYYY")
            mExpDate = VB6.Format(txtExpDate.Text, "DD/MM/YYYY")
        Else
            mExpDate = VB6.Format(txtExpDate.Text, "DD/MM/YYYY")
        End If

        If txtVno.Text = "" Then
            mVNO = GenVno()
        Else
            mVNO = txtVno.Text
        End If

        mVNoPrefix = MainClass.AllowSingleQuote(Trim(txtVNo1.Text))
        mVNoSuffix = MainClass.AllowSingleQuote(Trim(txtVNoSuffix.Text))
        mVnoStr = mVNoPrefix & mVType & mVNO & mVNoSuffix
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mBookCode = CStr(ConProvisionBookCode)

        If ADDMode = True Then
            mRowNo = MainClass.AutoGenRowNo("FIN_PROVISION_HDR", "RowNo", PubDBCn)
            CurMKey = (50 + RsCompany.Fields("COMPANY_CODE").Value) & VB6.Format(RsCompany.Fields("FYEAR").Value) & VB6.Format(mRowNo)

            Sqlstr = " INSERT INTO FIN_PROVISION_HDR ( " & vbCrLf _
                & " Mkey, COMPANY_CODE, " & vbCrLf _
                & " FYEAR,RowNo, VType, VNoPrefix, VNoSeq, VNoSuffix, " & vbCrLf _
                & " Vno, Vdate, BookType,BookSubType, " & vbCrLf _
                & " BookCode, Narration, CANCELLED, " & vbCrLf _
                & " AUTHORISED, " & vbCrLf _
                & " AddUser, AddDate, ModUser, ModDate,UPDATE_FROM,EXPDATE " & vbCrLf _
                & " ) VALUES ( "

            Sqlstr = Sqlstr & vbCrLf _
                & " '" & CurMKey & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                & " " & mRowNo & ", " & vbCrLf & " '" & mVType & "', '" & mVNoPrefix & "', " & vbCrLf _
                & " " & Val(mVNO) & ", '" & mVNoSuffix & "', '" & mVnoStr & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & mBookType & "', '" & mBookSubType & "', " & vbCrLf _
                & " '" & mBookCode & "', '" & MainClass.AllowSingleQuote(txtNarration.Text) & "', '" & mCancelled & "', " & vbCrLf _
                & " 'N', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','H', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(mExpDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


        ElseIf MODIFYMode = True Then

            Sqlstr = "UPDATE FIN_PROVISION_HDR SET " & vbCrLf _
                & " Vdate=TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " VType= '" & mVType & "'," & vbCrLf _
                & " VnoPrefix='" & mVNoPrefix & "', " & vbCrLf _
                & " VnoSeq=" & Val(mVNO) & ", " & vbCrLf _
                & " VnoSuffix='" & mVNoSuffix & "', " & vbCrLf _
                & " Vno='" & mVnoStr & "', " & vbCrLf _
                & " BookCode='" & mBookCode & "', " & vbCrLf _
                & " Narration='" & MainClass.AllowSingleQuote(txtNarration.Text) & "', " & vbCrLf _
                & " CANCELLED='" & mCancelled & "', " & vbCrLf _
                & " BookType='" & mBookType & "', " & vbCrLf _
                & " BookSubType='" & mBookSubType & "', " & vbCrLf _
                & " UPDATE_FROM='H'," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " EXPDATE=TO_DATE('" & VB6.Format(mExpDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            Sqlstr = Sqlstr & vbCrLf & " Where Mkey='" & CurMKey & "'"

        End If

        PubDBCn.Execute(Sqlstr)
        If UpdateDetail(CurMKey, mRowNo, mBookCode, mVType, mVnoStr, (TxtVDate.Text), (txtNarration.Text), PubDBCn) = False Then GoTo ErrPart

        PubDBCn.CommitTrans()
        txtVno.Text = mVNO

        Update1 = True

        Exit Function
ErrPart:
        '    Resume			
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''			
        Update1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Function
    Private Function GenVno() As String
        On Error GoTo ERR1
        Dim mVNo1 As String
        Dim SqlStr2 As String
        Dim Sqlstr As String
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mVType As String

        Call GenPrefixVNo()

        mBookType = VB.Left(lblBookType.Text, 1)
        mBookSubType = VB.Right(lblBookType.Text, 1)
        mVType = Trim(txtVType.Text)

        If ADDMode = True Or txtVno.Text = "" Then
            Sqlstr = "SELECT MAX(VNOSeq) From FIN_PROVISION_HDR WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BookType='" & mBookType & "'" & vbCrLf & " AND BookSubType='" & mBookSubType & "'" & vbCrLf & " AND " & vbCrLf & " VTYPE='" & MainClass.AllowSingleQuote(mVType) & "'"

            GenVno = VB6.Format(MainClass.AutoGenVNo(Sqlstr, PubDBCn), "00000")
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description)
        'Resume			
    End Function
    Private Sub GenPrefixVNo()
        On Error GoTo ERR1
        Dim mVNo1 As String

        '    If RsCompany.Fields("CBJVoucherSeq").Value = "D" Then			
        '        mVNo1 = Format(Day(txtVDate.Text), "00") & vb6.Format(Month(txtVDate.Text), "00")			
        '    ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "M" Then			
        '        mVNo1 = Format(Month(txtVDate.Text), "00")			
        '    ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "Y" Then			
        'mVNo1 = Right(Year(txtVDate.Text), 2)			
        mVNo1 = VB.Right(RsCompany.Fields("FYEAR").Value, 2)
        '    End If			


        ''txtVNo1.Text = Left(RsCompany.Fields("Alias").Value, 3) & mVNo1			
        txtVNo1.Text = "" ''mVNo1			
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
        ''Resume			
    End Sub
    Private Function UpdateDetail(ByRef mMKey As String, ByRef mRowNo As Integer, ByRef mBookCode As String, ByRef mVType As String, ByRef mVNO As String, ByRef mVDate As String, ByRef pNarration As String, ByRef pDBCn As ADODB.Connection) As Boolean

        On Error GoTo ErrDetail

        Dim i As Integer
        Dim Sqlstr As String
        Dim mAccountName As String
        Dim mAccountCode As String
        Dim mChequeNo As String
        Dim mChqDate As String
        Dim mAmount As Double
        Dim mCCCode As String
        Dim mExpCode As String
        Dim mDeptCode As String
        Dim mEmpCode As String
        Dim mIBRNo As String
        Dim mDC As String
        Dim mRemarks As String
        Dim mPRRowNo As Integer
        Dim mSubRowNo As Integer
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mClearDate As String
        Dim mParticulars As String
        Dim VMkey As String
        Dim mIsFixedAssets As String
        Dim mSameVNo As Boolean
        Dim mDivisionCode As Double


        mBookType = VB.Left(lblBookType.Text, 1)
        mBookSubType = VB.Right(lblBookType.Text, 1)

        Sqlstr = "Delete From FIN_PROVISION_DET Where Mkey='" & mMKey & "'"
        pDBCn.Execute(Sqlstr)

        Sqlstr = "DELETE FROM FIN_PROVISION_TRN  WHERE " & vbCrLf & " MKEY ='" & mMKey & "' " & vbCrLf & " AND BookType='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND BooksubType='" & VB.Right(lblBookType.Text, 1) & "'"
        pDBCn.Execute(Sqlstr)

        mSameVNo = False
        With SprdMain
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColAccountName
                mAccountName = Trim(.Text)
                .Col = 0
                If mAccountName <> "" Then
                    .Col = ColPRRowNo
                    mPRRowNo = Val(.Text)

                    mSubRowNo = mPRRowNo

                    .Col = ColDC
                    mDC = UCase(VB.Left(.Text, 1))

                    .Col = ColAccountName
                    mAccountCode = IIf(MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", pDBCn, mAccountCode,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mAccountCode, "-1")

                    If MainClass.ValidateWithMasterTable(mAccountCode, "ACCOUNTPOSTCODE", "ISFIXASSETS", "FIN_INVTYPE_MST", pDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                        mIsFixedAssets = MasterNo
                    Else
                        mIsFixedAssets = "N"
                    End If

                    .Col = ColParticulars
                    mParticulars = IIf(Trim(.Text) = "", pNarration, Trim(.Text))

                    .Col = ColAmount
                    mAmount = Val(.Text) ''IIf(chkCancelled.Value = vbChecked, 0, Val(.Text))			

                    .Col = ColChequeNo
                    mChequeNo = Trim(.Text)

                    .Col = ColChequeDate
                    mChqDate = IIf(mChequeNo = "", mVDate, Trim(.Text))

                    .Col = ColCC
                    mCCCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "CC_CODE", "CC_CODE", "FIN_CCENTER_HDR", pDBCn, mCCCode,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mCCCode, -1)

                    .Col = ColExp
                    mExpCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "COST_CENTER_CODE", "COST_CENTER_CODE", "CST_CENTER_MST", pDBCn, mExpCode,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mExpCode, -1)

                    .Col = ColDept
                    mDeptCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "DEPT_CODE", "DEPT_CODE", "PAY_DEPT_MST", pDBCn, mDeptCode,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mDeptCode, -1)

                    .Col = ColEmp
                    mEmpCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", pDBCn, mEmpCode,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mEmpCode, -1)

                    .Col = ColDivisionCode
                    SprdMain.Text = IIf(Val(SprdMain.Text) <= 0, 1, Val(SprdMain.Text))
                    mDivisionCode = IIf(MainClass.ValidateWithMasterTable(Val(SprdMain.Text), "DIV_CODE", "DIV_CODE", "INV_DIVISION_MST", pDBCn, mDivisionCode,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mDivisionCode, -1)
                    mDivisionCode = IIf(mDivisionCode <= 0, 1, mDivisionCode)

                    .Col = ColIBRNo
                    mIBRNo = .Text

                    .Col = ColClearDate
                    mClearDate = .Text

                    Sqlstr = "INSERT INTO FIN_PROVISION_DET ( " & vbCrLf _
                        & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf _
                        & " ChequeNo,ChqDate,CostCCode, " & vbCrLf _
                        & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf _
                        & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & ",  " & vbCrLf _
                        & " '" & mMKey & "', " & mPRRowNo & ", " & vbCrLf _
                        & " " & mSubRowNo & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & "  )"

                    PubDBCn.Execute(Sqlstr)

                    If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        If UpdateProvisionTRN(pDBCn, mMKey, mPRRowNo, i, mBookCode, mVType, mBookType, mBookSubType, mAccountCode, mVNO, mVDate, mVNO, mVDate, mAmount, mDC, "0", "", "", mCCCode, mDeptCode, mEmpCode, mExpCode, mDivisionCode, VB6.Format(txtExpDate.Text, "DD/MM/YYYY"), mIBRNo, "P", mClearDate, "N", mParticulars, "", VB6.Format(txtExpDate.Text, "DD/MM/YYYY"), ADDMode, (lblAddUser.Text), (lblAddDate.Text), "N") = False Then GoTo ErrDetail

                    End If

                End If
                mSameVNo = True
            Next i

        End With
        UpdateDetail = True
        Exit Function
ErrDetail:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateDetail = False
        'Resume			
    End Function

    Private Sub txtVNo1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNo1.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVNo1_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVNo1.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtVNo1.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtVNoSuffix_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNoSuffix.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVNoSuffix_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVNoSuffix.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtVNoSuffix.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVNoSuffix_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVNoSuffix.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call TxtVNo_Validating(txtVno, New System.ComponentModel.CancelEventArgs(False))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtVType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVType.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtVType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtVType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub



    Private Sub TxtVType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtVType.Text) = "" Then
            Cancel = True
            MsgInformation("Invalid Voucher Type")
            GoTo EventExitSub
        Else
            txtVType.Text = Trim(txtVType.Text)
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function GetVoucherNetAmount() As Double
        Dim Sqlstr As String
        Dim RsTemp As ADODB.Recordset


        GetVoucherNetAmount = CDbl(IIf(IsNumeric(LblDrAmt.Text), LblDrAmt.Text, 0))

        Exit Function

        Sqlstr = "  SELECT SUM(AMOUNT) as AMOUNT FROM FIN_PROVISION_TRN " & vbCrLf & " WHERE MKEY='" & CurMKey & "'" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND BOOKSUBTYPE='" & VB.Right(lblBookType.Text, 1) & "' AND SUBROWNO=-1"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetVoucherNetAmount = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If
    End Function

    Private Sub frmProvisionVoucher_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        fraGridView.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        FraTrans.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)

        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdPopFromFile_Click(sender As Object, e As EventArgs) Handles CmdPopFromFile.Click
        Try
            Dim strFilePath As String = ""
            Dim intflag As Integer
            CommonDialogOpen.FileName = ""

            intflag = CommonDialogOpen.ShowDialog()

            If intflag = 1 Then
                If CommonDialogOpen.FileName <> "" Then
                    strFilePath = CommonDialogOpen.FileName
                    'strfilename = CommonDialogOpen.SafeFileName
                    Call PopulateFromXLSFile(strFilePath)
                End If
            End If

        Catch ex As Exception

        End Try
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:

    End Sub
    Private Sub PopulateFromXLSFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""

        Dim xSqlStr As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String

        Dim mDC As String
        Dim mAccountCode As String
        Dim mAccountName As String
        Dim mParticulars As String
        Dim mDivCode As Long
        Dim mAmount As Double

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""
        Dim CntRow As Long = 1


        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)

        Dim ErrorFile As System.IO.StreamWriter


        Dim FileName As String = Path.GetFileName(strXLSFile)
        Dim Extension As String = Path.GetExtension(strXLSFile)


        Dim conStr As String = ""
        Select Case UCase(Extension)
            Case ".XLS"
                'Excel 97-03 
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strXLSFile & ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'"
                Exit Select
            Case ".XLSX"
                'Excel 07 
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strXLSFile & ";Extended Properties='Excel 12.0 Xml;HDR=Yes'"
                Exit Select
        End Select

        conStr = String.Format(conStr, strXLSFile)    ''isHDR='Yes'

        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        'Get the name of First Sheet 
        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()

        connExcel.Close()

        'Read Data from First Sheet 
        connExcel.Open()

        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"      '' ORDER BY 4 DESC
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()

        For Each dtRow In dt.Rows




            mDC = UCase(Trim(IIf(IsDBNull(dtRow.Item(0)), "", dtRow.Item(0))))
            mAccountCode = UCase(Trim(IIf(IsDBNull(dtRow.Item(1)), "", dtRow.Item(1))))
            mAccountName = UCase(Trim(IIf(IsDBNull(dtRow.Item(2)), "", dtRow.Item(2))))
            mParticulars = UCase(Trim(IIf(IsDBNull(dtRow.Item(3)), "", dtRow.Item(3))))
            mDivCode = Val(IIf(IsDBNull(dtRow.Item(4)), 0, dtRow.Item(4)))
            mAmount = Val(IIf(IsDBNull(dtRow.Item(5)), 0, dtRow.Item(5)))

            OpenLocalConnection()

            If Trim(mAccountCode) <> "" Then
                xSqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_CODE " & vbCrLf _
                   & " FROM FIN_SUPP_CUST_MST " & vbCrLf _
                   & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
            Else
                xSqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_CODE " & vbCrLf _
                   & " FROM FIN_SUPP_CUST_MST " & vbCrLf _
                   & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND SUPP_CUST_NAME ='" & MainClass.AllowSingleQuote(mAccountName) & "'"
            End If

            MainClass.UOpenRecordSet(xSqlStr, LocalPubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mAccountCode = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value))
                mAccountName = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value))
            Else
                GoTo NextRecord
            End If

            SprdMain.Row = SprdMain.MaxRows

            SprdMain.Col = ColPRRowNo
            SprdMain.Text = CntRow

            SprdMain.Col = ColDC
            SprdMain.Text = IIf(Mid(mDC, 1, 1) = "D", "DR", "CR")

            SprdMain.Col = ColAccountName
            SprdMain.Text = mAccountName

            SprdMain.Col = ColParticulars
            SprdMain.Text = mParticulars

            SprdMain.Col = ColDivisionCode
            SprdMain.Text = IIf(mDivCode <= 0, 1, mDivCode)

            SprdMain.Col = ColAmount
            SprdMain.Text = mAmount

            SprdMain.MaxRows = SprdMain.MaxRows + 1
            CntRow = CntRow + 1

            RsTemp.Close()
            RsTemp = Nothing

            CloseLocalConnection()
NextRecord:

        Next

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub
End Class
