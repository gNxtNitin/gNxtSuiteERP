Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility

Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Configuration

Friend Class FrmSupp_PurchaseGST
    Inherits System.Windows.Forms.Form
    Dim RsSuppPurchMain As ADODB.Recordset ''Recordset
    Dim RsSuppPurchDetail As ADODB.Recordset ''Recordset
    Dim RsSuppPurchExp As ADODB.Recordset ''Recordset
    'Private PvtDBCn As ADODB.Connection
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    'Dim SqlStr As String
    Dim mSupplierCode As String
    Dim pRound As Double
    'Private Const mBookType = "J"
    ''Private Const mBookSubType = "C"
    Dim mBookType As String
    Dim mBookSubType As String
    Private Const ConRowHeight As Short = 12
    Dim mAddUser As String
    Dim mAddDate As String
    Dim mModUser As String
    Dim mModDate As String
    Dim pProcessKey As Double
    Private Const ColItemCode As Short = 1
    Private Const ColPartNo As Short = 2
    Private Const ColItemDesc As Short = 3
    Private Const ColUnit As Short = 4
    Private Const ColHSNCode As Short = 5
    Private Const ColPONo As Short = 6
    Private Const ColPODate As Short = 7
    Private Const ColPURFYear As Short = 8
    Private Const ColPURMkey As Short = 9
    Private Const ColVNo As Short = 10
    Private Const ColVDate As Short = 11
    Private Const ColBillNo As Short = 12
    Private Const ColBillDate As Short = 13
    Private Const ColMRRNo As Short = 14
    Private Const ColMRRDate As Short = 15
    Private Const ColBillQty As Short = 16
    Private Const ColBillRate As Short = 17
    Private Const ColPORate As Short = 18
    Private Const ColQty As Short = 19
    Private Const ColRate As Short = 20
    Private Const ColAmount As Short = 21
    Private Const ColCGSTPer As Short = 22
    Private Const ColCGSTAmount As Short = 23
    Private Const ColSGSTPer As Short = 24
    Private Const ColSGSTAmount As Short = 25
    Private Const ColIGSTPer As Short = 26
    Private Const ColIGSTAmount As Short = 27
    Private Const ColInvType As Short = 28
    Private Const ColLocationID As Short = 29

    Private Const ColRO As Short = 1
    Private Const ColExpName As Short = 2
    Private Const ColExpPercent As Short = 3
    Private Const ColExpAmt As Short = 4
    Private Const ColExpSTCode As Short = 5
    Private Const ColExpAddDeduct As Short = 6
    Private Const ColExpIdent As Short = 7
    Private Const ColTaxable As Short = 8
    Private Const ColExciseable As Short = 9
    Private Const ColExpCalcOn As Short = 10
    Private Const ColExpDebitAmt As Short = 11

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboGoodService_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGoodService.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboGSTStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGSTStatus.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboInvType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInvType.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboInvType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInvType.SelectedIndexChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboInvType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboInvType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        If Trim(cboInvType.Text) = "" Then GoTo EventExitSub
        SqlStr = "SELECT SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST,FIN_INVTYPE_MST " & vbCrLf & " WHERE FIN_SUPP_CUST_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_SUPP_CUST_MST.COMPANY_CODE=FIN_INVTYPE_MST.COMPANY_CODE " & vbCrLf & " AND FIN_SUPP_CUST_MST.SUPP_CUST_CODE=FIN_INVTYPE_MST.ACCOUNTPOSTCODE " & vbCrLf & " AND FIN_INVTYPE_MST.NAME='" & MainClass.AllowSingleQuote((cboInvType.Text)) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            txtDebitAccount.Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub ChkCapital_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkCapital.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If FormActive = False Then Exit Sub
        Call CalcTots()
    End Sub
    Private Sub chkFinalPost_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFinalPost.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            txtVno.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            cboInvType.Enabled = True
        Else
            cmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart
        Dim xDCNo As String
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mBookCode As Integer
        Dim mLockBookCode As Integer
        Dim mJVMKEY As String
        If PubUserID <> "G0416" Then
            Exit Sub
        End If
        If ValidateBranchLocking((TxtVDate.Text)) = True Then
            Exit Sub
        End If
        mLockBookCode = CInt(ConLockJournal)
        If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, TxtVDate.Text, (txtSupplier.Text), mSupplierCode) = True Then
            Exit Sub
        End If
        If Trim(txtBillNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        '    If Trim(txtJVVNO.Text) <> "" Then
        '        MsgInformation "Journal Voucher Post so Cann't be Deleted. First Deleted Journal Voucher No " & Trim(txtJVVNO.Text)
        '        Exit Sub
        '    End If
        If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Approved Post Cann't be Deleted.")
            Exit Sub
        End If
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Bill Cann't be Deleted.")
            Exit Sub
        End If
        If CheckBillPayment(mSupplierCode, (txtBillNo.Text), "B", (txtBillDate.Text)) = True Then Exit Sub
        If Not RsSuppPurchMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User choose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_PURCHASE_HDR", (LblMKey.Text), RsSuppPurchMain, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_PURCHASE_DET", (LblMKey.Text), RsSuppPurchDetail, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_PURCHASE_EXP", (LblMKey.Text), RsSuppPurchExp, "MKEY", "D") = False Then GoTo DelErrPart

                If InsertIntoDeleteTrn(PubDBCn, "FIN_SUPP_PURCHASE_HDR", "MKEY", (LblMKey.Text)) = False Then GoTo DelErrPart
                '            If InsertIntoDelAudit(PubDBCn, "FIN_VOUCHER_HDR", txtJVVNO.Text, RsSuppPurchMain, "VNO") = False Then GoTo DelErrPart:
                PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & mBookType & "' AND BOOKCODE='" & LblBookCode.Text & "'")
                PubDBCn.Execute("DELETE FROM FIN_POSTED_TRN WHERE MKey='" & LblMKey.Text & "' AND BookType='" & mBookType & "' AND BookSubType='" & mBookSubType & "'")
                PubDBCn.Execute("Delete from FIN_SUPP_PURCHASE_EXP Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("Delete from FIN_SUPP_PURCHASE_DET Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("DELETE FROM FIN_SUPP_PURCHASE_HDR WHERE MKey='" & LblMKey.Text & "' ")
                '            PubDBCn.Execute "DELETE FROM FIN_GST_SEQ_MST " & vbCrLf _
                ''                        & " WHERE MKEY= '" & LblMKey.text & "'" & vbCrLf _
                ''                        & " AND COMPANY_CODE = " & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
                ''                        & " AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                ''                        & " AND BOOKCODE = '" & LblBookCode.text & "'" & vbCrLf _
                ''                        & " AND BOOKTYPE = '" & mBookType & "'"
                '            PubDBCn.Execute "DELETE FROM FIN_POSTED_TRN WHERE MKey='" & mJVMKey & "' AND BookType='" & vb.Left(ConJournal, 1) & "' AND BookSubType='" & Right(ConJournal, 1) & "'"
                '
                '             SqlStr = "DELETE FROM FIN_BILLDETAILS_TRN WHERE Mkey='" & mJVMKey & "' "
                '            PubDBCn.Execute SqlStr
                '
                '            SqlStr = "DELETE FROM PAY_LOAN_MST WHERE Mkey='" & mJVMKey & "' "
                '            PubDBCn.Execute SqlStr
                '
                '            SqlStr = "Delete From FIN_VOUCHER_DET Where Mkey='" & mJVMKey & "'"
                '            PubDBCn.Execute SqlStr
                '
                '            SqlStr = "Delete From FIN_VOUCHER_HDR Where Mkey='" & mJVMKey & "'"
                '            PubDBCn.Execute SqlStr
                '
                '            SqlStr = "DELETE FROM FIN_POSTED_TRN  WHERE Mkey='" & mJVMKey & "'" & vbCrLf _
                ''                    & " AND BookType='" & vb.Left(ConJournal, 1) & "'" & vbCrLf _
                ''                    & " AND BooksubType='" & Right(ConJournal, 1) & "' "
                '            PubDBCn.Execute SqlStr
                '
                '            SqlStr = "DELETE FROM TDS_TRN WHERE Mkey='" & mJVMKey & "' AND BOOKCODE=-1 "
                '            PubDBCn.Execute SqlStr
                PubDBCn.CommitTrans()
                RsSuppPurchMain.Requery() ''.Refresh
                RsSuppPurchDetail.Requery() ''.Refresh
                RsSuppPurchExp.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsSuppPurchMain.Requery() ''.Refresh
        RsSuppPurchDetail.Requery() ''.Refresh
        RsSuppPurchExp.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '        Resume
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click
        On Error GoTo ModifyErr
        If PubUserID <> "G0416" Then
            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Cancelled Bill Cann't be Modified")
                Exit Sub
            End If
            If chkGSTClaim.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("GST Claim Done, So that Vourcher Post Cann't be Modified")
                Exit Sub
            End If
        End If
        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsSuppPurchMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            txtVno.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdPostingHead_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPostingHead.Click
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim cntRow As Integer
        FraAcctPostDetail.Visible = Not FraAcctPostDetail.Visible
        If FraAcctPostDetail.Visible = True Then
            FraAcctPostDetail.BringToFront()
            MainClass.ClearGrid(sprdAcctPostDetail)
            SqlStr = " SELECT ACM.SUPP_CUST_NAME, " & vbCrLf & " ABS(SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))) AS AMOUNT, " & vbCrLf & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<=0 THEN 'Cr' ELSE 'Dr' END AS DC "
            SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM " & vbCrLf & " WHERE  " & vbCrLf & " TRN.Company_Code=ACM.Company_Code " & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "
            SqlStr = SqlStr & vbCrLf & " AND TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKTYPE ='" & mBookType & "'" & vbCrLf & " AND BOOKSUBTYPE = '" & mBookSubType & "'" & vbCrLf & " AND TRN.MKEY='" & LblMKey.Text & "'"
            SqlStr = SqlStr & vbCrLf & " GROUP BY ACM.SUPP_CUST_NAME"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            cntRow = 1
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    sprdAcctPostDetail.Row = cntRow
                    sprdAcctPostDetail.Col = 1
                    sprdAcctPostDetail.Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    sprdAcctPostDetail.Col = 2
                    sprdAcctPostDetail.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("Amount").Value), "0.00", RsTemp.Fields("Amount").Value), "0.00")
                    sprdAcctPostDetail.Col = 3
                    sprdAcctPostDetail.Text = IIf(IsDbNull(RsTemp.Fields("DC").Value), "", RsTemp.Fields("DC").Value)
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        cntRow = cntRow + 1
                        sprdAcctPostDetail.MaxRows = cntRow
                    End If
                Loop
            End If
            FraAcctPostDetail.BringToFront()
            Call FormatsprdAcctPostDetail(-1)
        End If
    End Sub
    Private Sub FormatsprdAcctPostDetail(ByRef Arow As Integer)
        On Error GoTo ERR1
        With sprdAcctPostDetail
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(1, 30)
            .Col = 2
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(2, 12)
            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditMultiLine = True
            .set_ColWidth(3, 5)
        End With
        MainClass.ProtectCell(sprdAcctPostDetail, 1, sprdAcctPostDetail.MaxRows, 1, 3)
        MainClass.SetSpreadColor(sprdAcctPostDetail, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnPurchase(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnPurchase(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef pIsPO As String)
        'Dim Printer As New Printer
        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim SqlStrSub As String
        Dim mStateName As String
        Dim mStateCode As String
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mStateName = MasterNo
            mStateCode = GetStateCode(mStateName)
        End If
        MainClass.AssignCRptFormulas(Report1, "COMPANYCINNo=""" & IIf(IsDbNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyGSTIN=""" & IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "mStateCode=""" & mStateCode & """")
        If pIsPO = "Y" Then
            Report1.SubreportToChange = Report1.GetNthSubreportName(0)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub
            Report1.SubreportToChange = ""
        Else
            mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(lblNetAmount.Text) = 0, 0, lblNetAmount.Text)))
            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
                MainClass.AssignCRptFormulas(Report1, "AmountInWord=""Rs. Zero""")
                MainClass.AssignCRptFormulas(Report1, "NetAmount=""0.00""")
            Else
                MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")
                MainClass.AssignCRptFormulas(Report1, "NetAmount=""" & lblNetAmount.Text & """")
            End If
            SqlStrSub = " SELECT MKEY, SUBROWNO, EXPPERCENT, AMOUNT, COMPANY_CODE, NAME" & vbCrLf & " FROM FIN_SUPP_PURCHASE_EXP, FIN_INTERFACE_MST " & vbCrLf & " WHERE FIN_SUPP_PURCHASE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf & " AND FIN_SUPP_PURCHASE_EXP.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'" & vbCrLf & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " "
            If CDate(TxtVDate.Text) >= CDate(PubGSTApplicableDate) Then 'Change on 29/010/2017 before If CDate(txtVDate.Text) < CDate(PubGSTApplicableDate) Then
                SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"
            Else
                SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='N'"
            End If
            SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"
            Report1.SubreportToChange = Report1.GetNthSubreportName(0)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub
            Report1.SubreportToChange = ""
        End If
        'Dim prt As Printer
        'If PubUniversalPrinter = "Y" And mMode = Crystal.DestinationConstants.crptToPrinter Then
        '    For Each prt In Printers
        '        If UCase(prt.DeviceName) = UCase("Universal Printer") Then
        '            Printer = prt
        '            Report1.PrinterName = prt.DeviceName
        '            Report1.PrinterDriver = prt.DriverName
        '            Report1.PrinterPort = prt.Port
        '            Exit For
        '        End If
        '    Next prt
        'End If
        Report1.Action = 1
        Report1.ReportFileName = ""
        Report1.Reset()
        Exit Sub
ErrPart:
        '    Resume
        MsgBox(Err.Description)
    End Sub
    Private Sub ReportOnPurchase(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String
        Dim mRptFileName As String
        Dim mVNo As String
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)
        SqlStr = ""
        Call SelectQryForVoucher(SqlStr)
        mTitle = "Purchase Supplementary Invoice"
        mRptFileName = "PurchaseSuppGST.rpt"
        mSubTitle = ""
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, "N")
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryForVoucher(ByRef mSqlStr As String) As String
        ''SELECT CLAUSE...
        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*, CMST.SUPP_CUST_NAME "
        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM FIN_SUPP_PURCHASE_HDR IH, FIN_SUPP_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST " ' & vbCrLf |
        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.MKEY='" & LblMKey.Text & "'" & vbCrLf & " AND IH.BOOKTYPE='" & mBookType & "'" & vbCrLf & " AND IH.BOOKSUBTYPE='" & mBookSubType & "'" & vbCrLf & " AND IH.ISFINALPOST='Y'"
        ''ORDER CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SUBROWNO"
        SelectQryForVoucher = mSqlStr
    End Function
    Private Sub cmdReCalculate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdReCalculate.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim CntCheckRow As Integer
        Dim mMainItemDesc As String
        Dim mCheckItemDesc As String
        Dim mCheckRate As Double
        Dim mCheckQty As Double
        Dim mNewRate As Double
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemDesc
                mMainItemDesc = Trim(.Text)
                For CntCheckRow = 1 To SprdPostingDetail.MaxRows
                    SprdPostingDetail.Row = CntCheckRow
                    SprdPostingDetail.Col = 2
                    mCheckItemDesc = Trim(SprdPostingDetail.Text)
                    SprdPostingDetail.Col = 3
                    mCheckQty = Val(SprdPostingDetail.Text)
                    SprdPostingDetail.Col = 4
                    mCheckRate = Val(SprdPostingDetail.Text)
                    SprdPostingDetail.Col = 5
                    mNewRate = Val(SprdPostingDetail.Text)
                    .Col = ColRate
                    If optBaseOn(0).Checked = True Then
                        If mMainItemDesc = mCheckItemDesc Then
                            SprdPostingDetail.Col = 1
                            If SprdPostingDetail.Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                                .Col = ColQty
                                .Text = CStr(0)
                            Else
                                '                        .Col = ColQty
                                '                        .Text = Format(mCheckQty, "0.00")
                                .Col = ColRate
                                '                        If Val(.Text) = Val(Format(mCheckRate, "0.0000")) Then
                                .Text = VB6.Format(mNewRate, "0.0000")
                                '                        Else
                                '                            MsgBox Val(.Text) & " - " & mCheckRate
                                '                        End If
                            End If
                            Exit For
                        End If
                    Else
                        If mMainItemDesc = mCheckItemDesc And Val(.Text) = Val(VB6.Format(mCheckRate, "0.0000")) Then
                            SprdPostingDetail.Col = 1
                            If SprdPostingDetail.Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                                .Col = ColQty
                                .Text = CStr(0)
                            Else
                                '                        .Col = ColQty
                                '                        .Text = Format(mCheckQty, "0.00")
                                .Col = ColRate
                                '                        If Val(.Text) = Val(Format(mCheckRate, "0.0000")) Then
                                .Text = VB6.Format(mNewRate, "0.0000")
                                '                        Else
                                '                            MsgBox Val(.Text) & " - " & mCheckRate
                                '                        End If
                            End If
                            Exit For
                        End If
                    End If
                Next
            Next
        End With
        Call CalcTots()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        cboInvType_Validating(cboInvType, New System.ComponentModel.CancelEventArgs(False))
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        Call CalcTots()
        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))
            If cmdAdd.Enabled = True Then cmdAdd.Focus()
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
    Private Sub cmdsearchPO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchPO.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mDivisionCode As Double
        Dim mSupplierCode As Double

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        SqlStr = "SELECT IH.AUTO_KEY_PO, IH.AMEND_NO, IH.PUR_ORD_DATE, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME" & vbCrLf _
                & " FROM PUR_PURCHASE_HDR IH, FIN_SUPP_CUST_MST CMST"

        'AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)=" & RsCompany.fields("FYEAR").value & "
        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE = CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE = CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.PUR_TYPE IN ('J','P')" & vbCrLf _
            & " AND IH.ORDER_TYPE IN ('C','O') AND IH.DIV_CODE=" & mDivisionCode & ""

        If Trim(txtSupplier.Text) <> "" Then
            'If MainClass.ValidateWithMasterTable(Trim(txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mSupplierCode = CDbl(Trim(MasterNo))
            SqlStr = SqlStr & vbCrLf & " AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'"
            'End If
        End If
        'If MainClass.SearchGridMaster((txtPONo.Text), "PUR_PURCHASE_HDR", "AUTO_KEY_PO", "AMEND_NO", "PUR_ORD_DATE", "SUPP_CUST_CODE", SqlStr) = True Then
        If MainClass.SearchGridMasterBySQL2((txtPONo.Text), SqlStr) = True Then
            txtPONo.Text = AcName
            txtAmendNo.Text = AcName1
            txtPODate.Text = AcName2
            txtPONO_Validating(txtPONo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        Dim cntRowMain As Integer
        Dim cntRow As Integer
        Dim cntRowSub As Integer
        Dim mCheckItemDesc As String
        Dim mCheckBillRate As Double
        Dim mShowItemDesc As String
        Dim mItemDesc As String
        Dim mBillQty As Double
        Dim mBillRate As Double
        Dim mShowBillRate As Double
        Dim mBillAmount As Double
        FraPostingDtl.Visible = Not FraPostingDtl.Visible
        If FraPostingDtl.Visible = True Then
            MainClass.ClearGrid(SprdPostingDetail)
            FraPostingDtl.Enabled = True
            SprdPostingDetail.Enabled = True
            cntRow = 1
            For cntRow = 1 To SprdMain.MaxRows
                SprdMain.Row = cntRow
                SprdMain.Col = ColItemDesc
                mCheckItemDesc = Trim(SprdMain.Text)
                SprdMain.Col = ColRate
                mCheckBillRate = Val(SprdMain.Text)
                For cntRowSub = 1 To SprdPostingDetail.MaxRows
                    SprdPostingDetail.Row = cntRowSub
                    SprdPostingDetail.Col = 2
                    mItemDesc = Trim(SprdPostingDetail.Text)
                    SprdPostingDetail.Col = 4
                    mBillRate = Val(SprdPostingDetail.Text)
                    SprdPostingDetail.Col = 5
                    SprdPostingDetail.Text = CStr(mBillRate)
                    If optBaseOn(0).Checked = True Then
                        If (mCheckItemDesc = mItemDesc) Then
                            GoTo NextRec
                        End If
                    Else
                        If (mCheckItemDesc = mItemDesc) And (mCheckBillRate = mBillRate) Then
                            GoTo NextRec
                        End If
                    End If
                Next
                cntRowMain = 1
                For cntRowMain = 1 To SprdMain.MaxRows
                    SprdMain.Row = cntRowMain
                    SprdMain.Col = ColItemDesc
                    mShowItemDesc = Trim(SprdMain.Text)
                    SprdMain.Col = ColRate
                    mShowBillRate = Val(SprdMain.Text)
                    If optBaseOn(0).Checked = True Then
                        If mShowItemDesc = mCheckItemDesc Then
                            SprdMain.Col = ColQty
                            mBillQty = mBillQty + Val(SprdMain.Text)
                            mBillRate = mCheckBillRate
                        End If
                    Else
                        If mShowItemDesc = mCheckItemDesc And mShowBillRate = mCheckBillRate Then
                            SprdMain.Col = ColQty
                            mBillQty = mBillQty + Val(SprdMain.Text)
                            SprdMain.Col = ColRate
                            mBillRate = mShowBillRate ''Val(SprdMain.Text)
                        End If
                    End If
                Next
                SprdPostingDetail.Row = SprdPostingDetail.MaxRows
                SprdPostingDetail.Col = 1
                SprdPostingDetail.Value = CStr(System.Windows.Forms.CheckState.Checked) ''IIf(mBillAmount > 0, vbChecked, vbUnchecked)
                SprdPostingDetail.Col = 2
                SprdPostingDetail.Text = mCheckItemDesc
                SprdPostingDetail.Col = 3
                SprdPostingDetail.Text = VB6.Format(mBillQty, "0.0000")
                SprdPostingDetail.Col = 4
                SprdPostingDetail.Text = VB6.Format(mBillRate, "0.0000")
                SprdPostingDetail.Col = 5
                SprdPostingDetail.Text = VB6.Format(mBillRate, "0.0000")
                SprdPostingDetail.Col = 6
                SprdPostingDetail.Text = VB6.Format(mBillQty * mBillRate, "0.00")
                SprdPostingDetail.MaxRows = SprdPostingDetail.MaxRows + 1
NextRec:
                mShowItemDesc = ""
                mBillQty = 0
                mBillAmount = 0
            Next
            Call FormatSprdPostingDetail(-1)
        End If
    End Sub
    Private Sub FormatSprdPostingDetail(ByRef Arow As Integer)
        On Error GoTo ERR1
        Dim I As Integer
        With SprdPostingDetail
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .Col = 1
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(1, 2)
            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(2, 20)
            For I = 3 To 6
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMin = CDbl("-99999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(I, 7)
            Next
        End With
        MainClass.ProtectCell(SprdPostingDetail, 1, SprdPostingDetail.MaxRows, 2, 4)
        MainClass.ProtectCell(SprdPostingDetail, 1, SprdPostingDetail.MaxRows, 6, 6)
        MainClass.SetSpreadColor(SprdPostingDetail, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdShowPO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShowPO.Click
        If SprdMain.MaxRows > 1 Then
            If MsgQuestion("Are you want to Add in Existing Row ...") = vbNo Then
                MainClass.ClearGrid(SprdMain)
                Call FormatSprdMain(-1)
                MainClass.ClearGrid(SprdExp)
                MainClass.ClearGrid(SprdPostingDetail)
            End If
        End If
        Call GetPONOValidate()
        txtSupplier.Enabled = False
    End Sub
    Private Sub optBaseOn_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optBaseOn.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optBaseOn.GetIndex(eventSender)
            FraPostingDtl.Visible = Not FraPostingDtl.Visible
            Call cmdShow_Click(cmdShow, New System.EventArgs())
        End If
    End Sub
    Private Sub SprdPostingDetail_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdPostingDetail.LeaveCell
        On Error GoTo ErrPart
        Dim I As Integer
        Dim mQty As Double
        Dim mRate As Double
        If eventArgs.NewRow = -1 Then Exit Sub
        With SprdPostingDetail
            For I = 1 To .MaxRows
                .Row = I
                .Col = 3
                mQty = Val(.Text)
                .Col = 5
                mRate = Val(.Text)
                .Col = 6
                .Text = VB6.Format(mRate * mQty, "0.00")
            Next
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmendNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtGSTNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGSTNo.DoubleClick
        'Dim RS As ADODB.Recordset
        'Dim SqlStr As String
        '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        ''            & " AND SUPP_CUST_CODE='" & mSupplierCode & "'"
        '
        ''    If Trim(txtBillNo.Text) <> "" Then
        '    SqlStr = SqlStr & vbCrLf & " AND BILLNO='" & MainClass.AllowSingleQuote(txtBillNo.Text) & "'"
        '
        '    SqlStr = SqlStr & vbCrLf & " AND INVOICE_DATE='" & vb6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "'"
        '
        ''    End If
        '
        '    SqlStr = SqlStr & vbCrLf & " AND ISGSTAPPLICABLE='Y' AND ISFINALPOST='N'"
        '
        '
        '    If MainClass.SearchGridMaster(txtGSTNo.Text, "FIN_PURCHASE_HDR", "GST_CLAIM_NO", "BILLNO", "GST_CLAIM_DATE", "INVOICE_DATE", SqlStr) = True Then
        '        txtGSTNo.Text = AcName
        '        txtBillNo.Text = AcName1
        '    End If
    End Sub
    Private Sub txtGSTNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtGSTNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    If KeyCode = vbKeyF1 Then txtGSTNo_DblClick
    End Sub
    Private Sub txtOBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOBillNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtOBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtOBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOBillDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtOBillDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOBillDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mSuppCode As String
        If Trim(txtOBillDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtOBillDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
        mSuppCode = ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCode = Trim(MasterNo)
        End If
        If MainClass.ValidateWithMasterTable(txtOBillNo, "BILLNO", "INVOICE_DATE", "FIN_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & mSuppCode & "' AND INVOICE_DATE=TO_DATE('" & VB6.Format(txtOBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = False Then
            MsgInformation("Invalid Bill Date")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtOBillNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOBillNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mSuppCode As String
        mSuppCode = ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCode = Trim(MasterNo)
        End If
        If MainClass.ValidateWithMasterTable(txtOBillNo.Text, "BILLNO", "INVOICE_DATE", "FIN_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & mSuppCode & "'") = True Then
            txtOBillDate.Text = Trim(MasterNo)
        End If
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtPONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPONO_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.DoubleClick
        cmdsearchPO_Click(cmdsearchPO, New System.EventArgs())
    End Sub
    Private Sub txtPONO_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPONo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearchPO_Click(cmdsearchPO, New System.EventArgs())
    End Sub
    Public Sub txtPONO_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPONo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim xMKey As String
        Dim mPONo As Double
        Dim SqlStr As String
        Dim RsPOMain As ADODB.Recordset
        Dim mAccountName As String
        Dim mDivisionCode As Double
        If Val(txtPONo.Text) = 0 Then GoTo EventExitSub
        '    If Val(txtAmendNo.Text) = 0 Then Exit Sub
        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            GoTo EventExitSub
        End If
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If
        If Len(txtPONo.Text) < 6 Then
            txtPONo.Text = Val(txtPONo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If
        mPONo = Val(txtPONo.Text)
        xMKey = Val(txtPONo.Text) & VB6.Format(Val(txtAmendNo.Text), "000")

        'If txtSupplier.Text <> "" Then

        'Else
        SqlStr = "SELECT * FROM PUR_PURCHASE_HDR " & " WHERE MKEY='" & MainClass.AllowSingleQuote(UCase(xMKey)) & "'" & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND " & vbCrLf & " PUR_TYPE IN ('J','P')" & vbCrLf _
            & " AND ORDER_TYPE IN ('C','O') AND DIV_CODE=" & mDivisionCode & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPOMain.EOF = False Then
            Clear1()
            txtPONo.Text = IIf(IsDBNull(RsPOMain.Fields("AUTO_KEY_PO").Value), "", RsPOMain.Fields("AUTO_KEY_PO").Value)
            txtPODate.Text = VB6.Format(IIf(IsDBNull(RsPOMain.Fields("PUR_ORD_DATE").Value), "", RsPOMain.Fields("PUR_ORD_DATE").Value), "DD/MM/YYYY")
            txtAmendNo.Text = IIf(IsDBNull(RsPOMain.Fields("AMEND_NO").Value), "", RsPOMain.Fields("AMEND_NO").Value)
            txtWEFDate.Text = VB6.Format(IIf(IsDBNull(RsPOMain.Fields("AMEND_WEF_DATE").Value), "", RsPOMain.Fields("AMEND_WEF_DATE").Value), "DD/MM/YYYY")
            txtLocationID.Text = IIf(IsDBNull(RsPOMain.Fields("BILL_TO_LOC_ID").Value), "", RsPOMain.Fields("BILL_TO_LOC_ID").Value)
            mSupplierCode = IIf(IsDBNull(RsPOMain.Fields("SUPP_CUST_CODE").Value), -1, RsPOMain.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mSupplierCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = MasterNo
            End If
            txtSupplier.Text = mAccountName
            mDivisionCode = IIf(IsDBNull(RsPOMain.Fields("DIV_CODE").Value), "", RsPOMain.Fields("DIV_CODE").Value)
            If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                cboDivision.Text = Trim(MasterNo)
            End If
        Else
            MsgBox("Invalid PO NO.", MsgBoxStyle.Information)
            Cancel = True
        End If
        'End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Public Sub GetPONOValidate()
        On Error GoTo ERR1
        Dim xMKey As String
        Dim mPONo As Double
        Dim SqlStr As String
        Dim RsPOMain As ADODB.Recordset
        Dim mDivisionCode As Double

        'If Val(txtPONo.Text) = 0 Then Exit Sub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If



        mPONo = Val(txtPONo.Text)

        If mPONo <= 0 Then
            If Trim(txtSupplier.Text) = "" And Trim(txtLocationID.Text) = "" Then
            Else
                If InsertIntoTemp() = False Then GoTo ERR1
                Call ShowPODetail1()
                Call FillSprdExp()
                FormatSprdMain(-1)
                Call CalcTots()
            End If

        Else
            If Len(txtPONo.Text) < 6 Then
                txtPONo.Text = Val(txtPONo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
            End If

            SqlStr = "SELECT * FROM PUR_PURCHASE_HDR " & vbCrLf _
                & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf _
                & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND PUR_TYPE IN ('J','P')" & vbCrLf _
                & " AND ORDER_TYPE IN ('C','O') AND DIV_CODE=" & mDivisionCode & ""

            If Trim(txtAmendNo.Text) <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND AMEND_NO=" & Val(txtAmendNo.Text) & ""
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
            If RsPOMain.EOF = False Then
                txtLocationID.Text = IIf(IsDBNull(RsPOMain.Fields("BILL_TO_LOC_ID").Value), "", RsPOMain.Fields("BILL_TO_LOC_ID").Value)
                If InsertIntoTemp() = False Then GoTo ERR1
                Call ShowPODetail1()
                Call FillSprdExp()
                FormatSprdMain(-1)
                Call CalcTots()
            Else
                MsgBox("Invalid PO No.", MsgBoxStyle.Information)
            End If
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowPODetail1()
        On Error GoTo ERR1
        Dim RsPODetail As ADODB.Recordset
        Dim RsPurDetail As ADODB.Recordset
        Dim I As Integer
        Dim SqlStr As String
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim mPrice As Double
        Dim mDisc As Double
        Dim mPORate As Double
        Dim mAcceptedQty As Double
        Dim mBillRate As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mSqlStr As String
        Dim mPrevValue As Double
        Dim mReOfferQty As Double
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mHSNCode As String
        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mBillDate As String
        Dim mPurType As String
        Dim mPONo As Double
        Dim mPODate As String
        Dim mAccountCode As String

        mLocal = "N"
        'If Trim(txtSupplier.Text) <> "" Then
        '    If MainClass.ValidateWithMasterTable(Trim(txtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mLocal = Trim(MasterNo)
        '    End If
        'End If
        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = Trim(MasterNo)
        End If

        If Trim(txtSupplier.Text) <> "" Then
            mLocal = GetPartyBusinessDetail(Trim(mAccountCode), Trim(txtLocationID.Text), "WITHIN_STATE")
        End If
        mPartyGSTNo = ""
        mPartyGSTNo = GetPartyBusinessDetail(mAccountCode, Trim(txtLocationID.Text), "GST_RGN_NO")

        If MainClass.ValidateWithMasterTable((txtPONo.Text), "AUTO_KEY_PO", "PUR_TYPE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPurType = MasterNo
            mPurType = Trim(mPurType)
        End If
        SqlStr = ""
        SqlStr = " SELECT TRN.*, ITEM.ITEM_SHORT_DESC, ITEM.CUSTOMER_PART_NO " & vbCrLf & " FROM TEMP_DNCN_TRN TRN, INV_ITEM_MST ITEM " & vbCrLf & " WHERE " & vbCrLf & " TRN.USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf & " AND TRN.ITEM_CODE=ITEM.ITEM_CODE" & vbCrLf & " AND TRN.PORATE>0"

        If chkViewAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If VB.Left(lblBookType.Text, 1) = ConPurchaseSuppBook Then
                SqlStr = SqlStr & vbCrLf & " AND PORATE > (ITEM_RATE-DNCN_RATE-SUPP_RATE)"
            ElseIf VB.Left(lblBookType.Text, 1) = ConPurchaseCreditBook Then
                SqlStr = SqlStr & vbCrLf & " AND PORATE < (ITEM_RATE-DNCN_RATE-SUPP_RATE)"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.ITEM_CODE, INVOICE_DATE, BILLNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPODetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPODetail
            If .EOF = True Then Exit Sub
            I = If(SprdMain.MaxRows > 1, SprdMain.MaxRows, 1)

            Do While Not .EOF
                mItemCode = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                mBillDate = VB6.Format(IIf(IsDbNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                SprdMain.Row = I
                SprdMain.Col = ColItemCode
                SprdMain.Text = mItemCode
                SprdMain.Col = ColItemDesc
                mItemDesc = Trim(IIf(IsDbNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value))
                SprdMain.Text = mItemDesc
                SprdMain.Col = ColPartNo
                mPartNo = Trim(IIf(IsDbNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value))
                SprdMain.Text = mPartNo
                SprdMain.Col = ColHSNCode
                If CDate(mBillDate) < CDate(PubGSTApplicableDate) Then
                    mHSNCode = GetHSNCode(mItemCode)
                Else
                    mHSNCode = Trim(IIf(IsDbNull(.Fields("HSNCODE").Value), "", .Fields("HSNCODE").Value)) 'GetHSNCode(mItemCode)
                End If
                SprdMain.Text = mHSNCode
                SprdMain.Col = ColPONo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("CUST_REF_NO").Value), "", .Fields("CUST_REF_NO").Value))
                mPONo = Val(IIf(IsDBNull(.Fields("CUST_REF_NO").Value), -1, .Fields("CUST_REF_NO").Value))

                mPODate = ""
                If MainClass.ValidateWithMasterTable(mPONo, "AUTO_KEY_PO", "PUR_ORD_DATE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mPODate = MasterNo
                End If

                SprdMain.Col = ColPODate
                SprdMain.Text = VB6.Format(mPODate, "DD/MM/YYYY")

                SprdMain.Col = ColUnit
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value))
                SprdMain.Col = ColPURFYear
                SprdMain.Text = IIf(IsDbNull(.Fields("FYEAR").Value), "", .Fields("FYEAR").Value)
                SprdMain.Col = ColPURMkey
                SprdMain.Text = IIf(IsDbNull(.Fields("mKey").Value), "", .Fields("mKey").Value)
                SprdMain.Col = ColVNo
                SprdMain.Text = IIf(IsDbNull(.Fields("VNO").Value), "", .Fields("VNO").Value)
                SprdMain.Col = ColVDate
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("VDATE").Value), "", .Fields("VDATE").Value), "DD/MM/YYYY")
                SprdMain.Col = ColBillNo
                SprdMain.Text = IIf(IsDbNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                SprdMain.Col = ColBillDate
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                SprdMain.Col = ColMRRNo
                SprdMain.Text = Str(IIf(IsDbNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value))
                SprdMain.Col = ColMRRDate
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("MRRDATE").Value), "", .Fields("MRRDATE").Value), "DD/MM/YYYY")
                SprdMain.Col = ColBillQty
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value), "0.000")
                SprdMain.Col = ColBillRate
                mRate = System.Math.Abs(IIf(IsDbNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value))
                mRate = mRate - IIf(IsDbNull(.Fields("DNCN_RATE").Value), 0, .Fields("DNCN_RATE").Value)
                mRate = mRate + IIf(IsDbNull(.Fields("SUPP_RATE").Value), 0, .Fields("SUPP_RATE").Value)
                SprdMain.Text = VB6.Format(mRate, "0.000")
                SprdMain.Col = ColPORate
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("PORATE").Value), 0, .Fields("PORATE").Value), "0.000")
                SprdMain.Col = ColQty
                mAcceptedQty = CDbl(VB6.Format(IIf(IsDbNull(.Fields("ACCPETED").Value), 0, .Fields("ACCPETED").Value), "0.000"))
                SprdMain.Text = VB6.Format(mAcceptedQty, "0.000")
                SprdMain.Col = ColRate
                mRate = System.Math.Abs(mRate - IIf(IsDbNull(.Fields("PORATE").Value), 0, .Fields("PORATE").Value))
                SprdMain.Text = VB6.Format(mRate, "0.000")
                mAmount = mRate * mAcceptedQty
                SprdMain.Col = ColAmount
                SprdMain.Text = VB6.Format(mAmount, "0.000")
                '            mHSNCode = GetHSNCode(mItemCode)
                If mPurType = "J" Then
                    cboGoodService.SelectedIndex = 1
                    If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, "G") = False Then GoTo ERR1
                Else
                    cboGoodService.SelectedIndex = 0
                    If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo) = False Then GoTo ERR1
                End If
                SprdMain.Col = ColCGSTPer
                SprdMain.Text = VB6.Format(pCGSTPer, "0.00")
                SprdMain.Col = ColSGSTPer
                SprdMain.Text = VB6.Format(pSGSTPer, "0.00")
                SprdMain.Col = ColIGSTPer
                SprdMain.Text = VB6.Format(pIGSTPer, "0.00")
                I = I + 1
                SprdMain.MaxRows = I
                .MoveNext()
            Loop
        End With
        Call FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Function InsertIntoTemp() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSqlStr As String
        Dim pSuppCustCode As String
        Dim mFYear As Integer
        Dim mPurType As String = "P"
        Dim mPONO As Double

        InsertIntoTemp = False
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mFYear = GetCurrentFYNo(PubDBCn, (txtToDate.Text))
        If MainClass.ValidateWithMasterTable((txtPONo.Text), "AUTO_KEY_PO", "PUR_TYPE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPurType = MasterNo
            mPurType = Trim(mPurType)
        End If

        mPONO = IIf(Trim(txtPONo.Text) = "", -1, txtPONo.Text)

        SqlStr = "DELETE FROM TEMP_DNCN_TRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        '    If cboShowAgt.ListIndex = 0 Then
        '        SqlStr = SqlStr & vbCrLf & " AND GH.REF_TYPE='P'"
        '    ElseIf cboShowAgt.ListIndex = 1 Then
        '        SqlStr = SqlStr & vbCrLf & " AND GH.REF_TYPE='I'"
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND GH.REF_TYPE NOT IN ('P','I')"
        '    End If
        '
        mSqlStr = " INSERT INTO TEMP_DNCN_TRN ( " & vbCrLf _
            & " USERID, MKEY, COMPANY_CODE, " & vbCrLf _
            & " FYEAR, ACCOUNTCODE_DR, ACCOUNTCODE_CR, " & vbCrLf _
            & " VNO, VDATE, BILLNO, " & vbCrLf _
            & " INVOICE_DATE, AUTO_KEY_MRR, MRRDATE, " & vbCrLf _
            & " CUST_REF_NO, ITEM_CODE, ITEM_UOM, ITEM_QTY, " & vbCrLf _
            & " ACCPETED, ITEM_RATE, DNCN_RATE, " & vbCrLf _
            & " SUPP_RATE, PORATE,HSNCODE,LOCATION_ID ) "

        SqlStr = ""
        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
            & " IH.MKEY, IH.COMPANY_CODE, IH.FYEAR, " & vbCrLf _
            & " IH.SUPP_CUST_CODE, IH.ACCOUNTCODE, " & vbCrLf _
            & " IH.VNO, IH.VDATE, IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf _
            & " GH.AUTO_KEY_MRR, GH.MRR_DATE," & vbCrLf _
            & " NVL(ID.CUST_REF_NO,'-1'), ID.ITEM_CODE, ID.ITEM_UOM, "
        ''- GETLINEREJQTY(" & RsCompany.fields("COMPANY_CODE").value & ",IH.AUTO_KEY_MRR,IH.MRRDATE,IH.SUPP_CUST_CODE,ID.ITEM_CODE)
        SqlStr = SqlStr & vbCrLf & " ID.ITEM_QTY, " & vbCrLf _
            & " TO_CHAR(NVL(ID.ITEM_QTY,0)-NVL(ID.SHORTAGE_QTY,0)-NVL(ID.REJECTED_QTY,0) " & vbCrLf _
            & " - GETLINEREJECTIONQTY(" & RsCompany.Fields("COMPANY_CODE").Value & ",GH.AUTO_KEY_MRR,GH.MRR_DATE,IH.SUPP_CUST_CODE,ID.ITEM_CODE)" & vbCrLf _
            & " + GETREOFFERQTY(" & RsCompany.Fields("COMPANY_CODE").Value & ",GH.AUTO_KEY_MRR,GH.MRR_DATE,IH.SUPP_CUST_CODE,ID.ITEM_CODE) " & vbCrLf _
            & " ) AS ACCPETED, " & vbCrLf _
            & " ID.ITEM_RATE, " & vbCrLf _
            & " NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'R',ID.CUST_REF_NO),0) AS DNCN_RATE,  " & vbCrLf _
            & " NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'R'),0) AS SUPP_RATE, "
        If Trim(txtAmendNo.Text) = "" Then
            SqlStr = SqlStr & vbCrLf & " CASE WHEN SUBSTR(ID.CUST_REF_NO,1,1)<>'S' THEN GetITEMPRICE_NEW(IH.FYEAR, " & mFYear & ", IH.INVOICE_DATE,DECODE(GH.REF_TYPE,'P',ID.CUST_REF_NO," & mPONo & "),ID.ITEM_CODE) ELSE 0 END AS PORATE,"
        Else
            SqlStr = SqlStr & vbCrLf & " CASE WHEN SUBSTR(ID.CUST_REF_NO,1,1)<>'S' THEN GetITEMPORATE(DECODE(GH.REF_TYPE,'P',ID.CUST_REF_NO," & mPONo & ")," & Val(txtAmendNo.Text) & ",ID.ITEM_CODE) ELSE 0 END AS PORATE,"
        End If
        SqlStr = SqlStr & vbCrLf & " ID.HSNCODE, IH.BILL_TO_LOC_ID"
        ''+ " & vbCrLf _
        '& " GETREOFFERQTY(" & RsCompany.fields("COMPANY_CODE").value & ",IH.AUTO_KEY_MRR,IH.MRRDATE,IH.SUPP_CUST_CODE,ID.ITEM_CODE)
        ''& " NVL(GETCNFROMDN(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE),0) AS DNCN_RATE," & vbCrLf _
        '
        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID,INV_GATE_HDR GH "
        ''WHERE CLAUSE...''IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=GH.COMPANY_CODE AND DECODE(IH.PURCHASESEQTYPE,3,ID.MRRNO, IH.AUTO_KEY_MRR)=GH.AUTO_KEY_MRR"
        SqlStr = SqlStr & vbCrLf & " AND GH.REF_TYPE IN ('P','R')"

        SqlStr = SqlStr & vbCrLf & " AND IH.BILL_TO_LOC_ID='" & txtLocationID.Text & "'"

        '    SqlStr = SqlStr & vbCrLf & " AND SUBSTR(ID.CUST_REF_NO,1,1)<>'S' AND CUST_REF_NO IS NOT NULL"
        SqlStr = SqlStr & vbCrLf & " AND (CUST_REF_NO IS NOT NULL OR CUST_REF_NO<>'')"

        '    If optType(0).Value = True Then
        '        SqlStr = SqlStr & vbCrLf _
        ''            & " AND CASE WHEN SUBSTR(ID.CUST_REF_NO,1,1)<>'S' THEN GetITEMPRICE_NEW(IH.FYEAR, " & RsCompany.fields("FYEAR").value & ", IH.INVOICE_DATE,ID.CUST_REF_NO,ID.ITEM_CODE) ELSE 0 END< " & vbCrLf _
        ''            & " (ID.ITEM_RATE - NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'R'),0) " & vbCrLf _
        ''            & " + NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'R'),0)) "
        '
        '            ''& " + NVL(GETCNFROMDN(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE),0)) "
        '    Else
        If chkViewAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If VB.Left(lblBookType.Text, 1) = VB.Left(ConPurchaseSupp, 1) Then
                If Trim(txtAmendNo.Text) = "" Then
                    SqlStr = SqlStr & vbCrLf & " AND CASE WHEN SUBSTR(ID.CUST_REF_NO,1,1)<>'S' THEN GetITEMPRICE_NEW(IH.FYEAR, " & mFYear & ", IH.INVOICE_DATE,DECODE(GH.REF_TYPE,'P',ID.CUST_REF_NO," & mPONO & "),ID.ITEM_CODE) ELSE 0 END >  " & vbCrLf & " (ID.ITEM_RATE -NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'R',ID.CUST_REF_NO),0) " & vbCrLf & " + NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'R'),0)) "
                Else
                    SqlStr = SqlStr & vbCrLf & " AND CASE WHEN SUBSTR(ID.CUST_REF_NO,1,1)<>'S' THEN GetITEMPORATE(DECODE(GH.REF_TYPE,'P',ID.CUST_REF_NO," & mPONO & ")," & Val(txtAmendNo.Text) & ",ID.ITEM_CODE) ELSE 0 END >  " & vbCrLf & " (ID.ITEM_RATE -NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'R',ID.CUST_REF_NO),0) " & vbCrLf & " + NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'R'),0)) "
                End If
            Else
                If Trim(txtAmendNo.Text) = "" Then
                    SqlStr = SqlStr & vbCrLf & " AND CASE WHEN SUBSTR(ID.CUST_REF_NO,1,1)<>'S' THEN GetITEMPRICE_NEW(IH.FYEAR, " & mFYear & ", IH.INVOICE_DATE,DECODE(GH.REF_TYPE,'P',ID.CUST_REF_NO," & mPONO & "),ID.ITEM_CODE) ELSE 0 END <  " & vbCrLf & " (ID.ITEM_RATE -NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'R',ID.CUST_REF_NO),0) " & vbCrLf & " + NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'R'),0)) "
                Else
                    SqlStr = SqlStr & vbCrLf & " AND CASE WHEN SUBSTR(ID.CUST_REF_NO,1,1)<>'S' THEN GetITEMPORATE(DECODE(GH.REF_TYPE,'P',ID.CUST_REF_NO," & mPONO & ")," & Val(txtAmendNo.Text) & ",ID.ITEM_CODE) ELSE 0 END <  " & vbCrLf & " (ID.ITEM_RATE -NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'R',ID.CUST_REF_NO),0) " & vbCrLf & " + NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'R'),0)) "
                End If
            End If
        Else
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(ID.CUST_REF_NO,1,1)<>'S'"
        End If
        ''& " + NVL(GETCNFROMDN(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE),0)) "
        '    End If
        If Trim(txtOBillNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BILLNO='" & Trim(txtOBillNo.Text) & "' AND IH.INVOICE_DATE=TO_DATE('" & VB6.Format(txtOBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If
        SqlStr = SqlStr & vbCrLf & "AND IH.VNO<>'-1' AND IH.ISFINALPOST='Y' AND CANCELLED='N'"
        If chkViewAllPO.CheckState = System.Windows.Forms.CheckState.UnChecked Then
            If mPurType = "J" Then
                SqlStr = SqlStr & vbCrLf & " AND ID.CUST_REF_NO IN (" & vbCrLf & " SELECT A.GATEPASS_NO FROM INV_GATEPASS_HDR A, INV_GATEPASS_DET B" & vbCrLf & " WHERE A.GATEPASS_NO=B.GATEPASS_NO" & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.SUPP_CUST_CODE=IH.SUPP_CUST_CODE AND A.PURPOSE='B'" & vbCrLf & " AND B.AUTO_KEY_WO='" & MainClass.AllowSingleQuote(mPONO) & "')"
            Else
                If Trim(txtPONo.Text) <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND ID.CUST_REF_NO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "'"
                End If
            End If
            End If
        If Trim(txtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                pSuppCustCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(UCase(pSuppCustCode)) & "'"
            End If
        End If
        '    If optBaseOn(0).Value = True Then
        SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtWEFDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        '    ElseIf optBaseOn(1).Value = True Then
        '        SqlStr = SqlStr & vbCrLf _
        ''                & " AND IH.VDATE>='" & vb6.Format(txtWEFDate.Text, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''                & " AND IH.VDATE<='" & vb6.Format(txtToDate.Text, "DD-MMM-YYYY") & "'"
        '    Else
        '        SqlStr = SqlStr & vbCrLf _
        ''                & " AND IH.AUTO_KEY_MRR>='" & vb6.Format(txtWEFDate.Text, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''                & " AND IH.AUTO_KEY_MRR<='" & vb6.Format(txtToDate.Text, "DD-MMM-YYYY") & "'"
        '    End If
        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.SUPP_CUST_CODE,GH.AUTO_KEY_MRR, IH.VNO, IH.VDATE "
        SqlStr = mSqlStr & vbCrLf & SqlStr
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        InsertIntoTemp = True
        Exit Function
ErrPart:
        MsgInformation(Err.Description)
        PubDBCn.RollbackTrans()
        InsertIntoTemp = False
    End Function
    Private Function GetPreviousItemGross(ByRef pItemCode As String) As Double
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        GetPreviousItemGross = 0
        mSqlStr = "SELECT (NVL(ID.ITEM_PRICE,0) - ROUND((NVL(ID.ITEM_PRICE,0) * ID.ITEM_DIS_PER)/100,4)) AS GROSS_AMT " & vbCrLf _
            & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf _
            & " WHERE IH.MKEy=ID.MKEY" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(IH.AUTO_KEY_PO,LENGTH(IH.AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_PO=" & Val(txtPONo.Text) & "" & vbCrLf _
            & " AND IH.PUR_TYPE IN ('J','P')" & vbCrLf _
            & " AND IH.ORDER_TYPE IN ('C','O')"
        If Trim(pItemCode) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
        End If
        If Trim(txtAmendNo.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.AMEND_NO=" & Val(txtAmendNo.Text) - 1 & ""
        End If
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetPreviousItemGross = IIf(IsDbNull(RsTemp.Fields("GROSS_AMT").Value), 0, RsTemp.Fields("GROSS_AMT").Value)
        End If
        Exit Function
ErrPart:
        GetPreviousItemGross = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetPurchaseEntrySQL(ByRef mItemCode As String, ByRef pSupplierCode As String) As String
        On Error GoTo ERR1
        Dim SqlStr As String
        SqlStr = ""
        GetPurchaseEntrySQL = ""
        ''& " NVL(GETCNFROMDN(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE),0) + " & vbCrLf _
        '
        SqlStr = " SELECT IH.AUTO_KEY_MRR,IH.MRRDATE, IH.MKEY, IH.FYEAR, VNO, IH.VDATE, BILLNO, IH.INVOICE_DATE, " & vbCrLf & " ITEM_QTY, " & vbCrLf & " NVL(ID.ITEM_QTY,0)-NVL(ID.SHORTAGE_QTY,0)-NVL(ID.REJECTED_QTY,0) As ACCPT_QTY," & vbCrLf & " ITEM_RATE - " & vbCrLf & " NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'R',ID.CUST_REF_NO),0) + " & vbCrLf & " NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'R'),0) AS I_RATE " & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID " & vbCrLf & " Where " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf & " AND INVOICE_DATE>=TO_DATE('" & VB6.Format(txtWEFDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND INVOICE_DATE<=TO_DATE('" & VB6.Format(txtToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISFINALPOST='Y' AND CANCELLED='N' ORDER BY BILLNO, INVOICE_DATE"
        ''IH.FYEAR=" & RsCompany.fields("FYEAR").value & "   'AND ID.CUST_REF_NO='" & Val(txtPONo.Text) & "'
        GetPurchaseEntrySQL = SqlStr
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetPurchaseEntrySQL = ""
    End Function
    Private Sub SprdExp_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdExp.ButtonClicked
        If FormActive = True Then
            SprdExp.Col = ColExpAmt
            SprdExp.Row = eventArgs.row
            If Val(SprdExp.Text) <> 0 Then
                Call CalcTots()
            End If
        End If
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        'Dim xIName As String
        'Dim SqlStr As String
        If eventArgs.Row = 0 And eventArgs.Col = ColInvType Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColInvType
                If MainClass.SearchGridMaster(.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", , , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='O'") = True Then
                    .Row = .ActiveRow
                    .Col = ColInvType
                    .Text = AcName
                    '                MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColInvType
                End If
            End With
        End If
        Exit Sub
        '    If Row = 0 And Col = ColItemCode Then
        '        With SprdMain
        '            .Row = .ActiveRow
        '            .Col = ColItemCode
        ''            If mainclass.SearchMaster(.Text, "vwITEM", "ITEMCODE", SqlStr) = True Then
        ''                .Row = .ActiveRow
        ''                .Col = ColItemCode
        ''                .Text = AcName
        ''            End If
        '            MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColItemCode
        '        End With
        '    End If
        '
        '    If Row = 0 And Col = ColItemDesc Then
        '        With SprdMain
        '            .Row = .ActiveRow
        '            .Col = ColItemDesc
        '            xIName = .Text
        '            .Text = ""
        ''            If mainclass.SearchMaster(.Text, "vwITEM", "Name", SqlStr) = True Then
        ''                .Row = .ActiveRow
        ''                .Col = ColItemDesc
        ''                .Text = AcName
        ''            Else
        ''                .Row = .ActiveRow
        ''                .Col = ColItemDesc
        ''                .Text = xIName
        ''            End If
        '            MainClass.ValidateWithMasterTable .Text, "Name", "ItemCode", "Item", PubDBCn, MasterNo
        '            .Row = .ActiveRow
        '            .Col = ColItemCode
        '            .Text = MasterNo
        '            MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColItemCode
        '        End With
        '    End If
        '    If Col = 0 And Row > 0 Then    '***ROW DEL. OPTION NOT REQ IN INVOICE
        '        SprdMain.Row = Row
        '        SprdMain.Col = ColSONo
        '        If Row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
        '            mainclass.DeleteSprdRow SprdMain, Row, ColSONo
        '            mainclass.SaveStatus Me, ADDMode, MODIFYMode
        '            FormatSprdMain Row
        ''            Call DistributeExpInMainGrid
        ''            Call CalcTots
        '        End If
        '    End If
        Call CalcTots()
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim RIBBONSGroup As Boolean
        Dim xSoNo As String
        Dim xICode As String
        If eventArgs.NewRow = -1 Then Exit Sub
        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If
        SprdMain.Row = SprdMain.ActiveRow
        Select Case eventArgs.col
            Case ColQty
                '            If CheckQty() = True Then
                '                MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight
                '                FormatSprdMain SprdMain.MaxRows
                '            End If
            Case ColRate
                Call CheckRate()
            Case ColInvType
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColInvType
                If Trim(SprdMain.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='O'") = False Then
                        MsgBox("Invoice Name Does Not Exist In Master", MsgBoxStyle.Information)
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColInvType)
                        eventArgs.cancel = True
                    End If
                End If
        End Select
        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function CheckQty() As Boolean
        On Error GoTo ERR1
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function
            .Col = ColQty
            If Val(.Text) > 0 Then
                CheckQty = True
            Else
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColQty)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub CheckRate()
        On Error GoTo ERR1
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Sub
            .Col = ColQty
            If Val(.Text) > 0 Then
                .Col = ColRate
                If Val(.Text) = 0 Then
                    MsgInformation("Please Enter the Rate.")
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColRate)
                End If
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            If eventArgs.Row = 0 Then Exit Sub
            .Row = eventArgs.Row
            .Col = 1
            If Trim(.Text) = "" Then
                cboInvType.SelectedIndex = -1
            Else
                cboInvType.Text = Trim(.Text)
            End If
            .Col = 3
            txtVNoPrefix.Text = Trim(.Text)
            .Col = 2
            txtVno.Text = VB6.Format(.Text, "00000")
            .Col = 7
            TxtVDate.Text = VB6.Format(.Text, "DD/MM/YYYY")
            .Col = 22
            chkCapital.CheckState = IIf(VB.Left(.Text, 1) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            txtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Sub txtGSTNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGSTNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtGSTNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGSTNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPaymentDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaymentdate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPaymentDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPaymentdate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPaymentDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtPaymentDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtTariff_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariff.DoubleClick
        SearchTariff()
    End Sub
    Private Sub txtTariff_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtTariff.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchTariff()
    End Sub
    Private Sub txtTariff_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTariff.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        If Trim(txtTariff.Text) = "" Then GoTo EventExitSub
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable((txtTariff.Text), "TARRIF_CODE", "TARRIF_DESC", "FIN_TARRIF_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            ErrorMsg("Please Enter Vaild Tariff.", "", MsgBoxStyle.Critical)
            Cancel = True
        Else
            txtItemType.Text = MasterNo
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtToDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtToDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtToDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtToDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtToDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(txtVDate.Text) = False Then
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(TxtVDate.Text) = "" Then GoTo EventExitSub
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
    Public Sub txtVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim xMKey As String
        Dim mVNo As String
        Dim SqlStr As String
        If Trim(txtVno.Text) = "" Then GoTo EventExitSub
        txtVno.Text = VB6.Format(Val(txtVno.Text), "00000")
        If MODIFYMode = True And RsSuppPurchMain.EOF = False Then xMKey = RsSuppPurchMain.Fields("mKey").Value
        mVNo = Trim(Trim(txtVNoPrefix.Text) & Trim(txtVno.Text))
        SqlStr = " SELECT * FROM FIN_SUPP_PURCHASE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND VNo='" & MainClass.AllowSingleQuote(mVNo) & "' " & vbCrLf & " AND BookType='" & mBookType & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsSuppPurchMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Voucher, Use Generate Voucher Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM FIN_SUPP_PURCHASE_HDR " & " WHERE Mkey='" & MainClass.AllowSingleQuote(xMKey) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Public Sub txtGSTNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGSTNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim mGSTNo As String
        Dim mCapital As String
        Dim SqlStr As String
        Dim xSuppCode As String
        Dim xGSTDate As String
        Dim xCGSTAmount As Double
        Dim xIGSTAmount As Double
        Dim xSGSTAmount As Double
        '    If Val(txtGSTNo.Text) = 0 Then Exit Sub
        '    If Trim(txtBillNo.Text) = "" Then
        '        MsgInformation "Please Enter Bill NO."
        '        If txtBillNo.Enabled = True Then txtBillNo.SetFocus
        '        Exit Sub
        '    End If
        '
        '    txtCGSTRefundAmt.Text = "0.00"
        '    txtSGSTRefundAmt.Text = "0.00"
        '    txtIGSTRefundAmt.Text = "0.00"
        '    If chkGST.Value = vbChecked Then
        '
        '        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '            xSuppCode = MasterNo
        '        End If
        '
        '        txtGSTNo.Text = Format(Val(txtGSTNo.Text), "00000")
        '        mCapital = IIf(ChkCapital = vbChecked, "Y", "N")
        '        mGSTNo = Trim(txtGSTNo.Text)
        '
        '        If ValidateGSTNo(xSuppCode, Trim(txtBillNo.Text), Trim(txtBillDate.Text), Trim(txtGSTNo.Text), xGSTDate, xCGSTAmount, xIGSTAmount, xSGSTAmount, 0) = True Then
        '            txtGSTDate.Text = Format(xGSTDate, "DD/MM/YYYY")
        '            txtCGSTRefundAmt.Text = Format(xCGSTAmount, "0.00")
        '            txtSGSTRefundAmt.Text = Format(xSGSTAmount, "0.00")
        '            txtIGSTRefundAmt.Text = Format(xIGSTAmount, "0.00")
        '        Else
        '            MsgBox "Invalid GST No.", vbInformation
        '            Cancel = True
        '        End If
        '    End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Public Function ValidateGSTNo(ByRef pSuppCode As String, ByRef pBillNo As String, ByRef pBillDate As String, ByRef pGSTNo As Double, ByRef pGetGSTDate As String, ByRef pGetCGSTAmount As Double, ByRef pGetIGSTAmount As Double, ByRef pGetSGSTAmount As Double, ByRef pNetBillAmount As Double) As Boolean
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim mGSTNo As String
        Dim mCapital As String
        Dim SqlStr As String
        Dim xSuppCode As String
        ValidateGSTNo = False
        pGetCGSTAmount = 0
        pGetSGSTAmount = 0
        pGetIGSTAmount = 0
        pNetBillAmount = 0
        SqlStr = " SELECT * FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSuppCode) & "'" & vbCrLf & " AND BILLNO='" & MainClass.AllowSingleQuote(pBillNo) & "'" & vbCrLf & " AND INVOICE_DATE=TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND GST_CLAIM_NO=" & Val(CStr(pGSTNo)) & "" & vbCrLf & " AND ISGSTAPPLICABLE='Y' AND ISPLA='N' " ' AND ISCAPITAL='" & mCapital & "'
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            pGetGSTDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("GST_CLAIM_DATE").Value), "", RsTemp.Fields("GST_CLAIM_DATE").Value), "DD/MM/YYYY")
            pGetCGSTAmount = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("TOTCGST_REFUNDAMT").Value), 0, RsTemp.Fields("TOTCGST_REFUNDAMT").Value), "0.00"))
            pGetSGSTAmount = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("TOTSGST_REFUNDAMT").Value), 0, RsTemp.Fields("TOTSGST_REFUNDAMT").Value), "0.00"))
            pGetIGSTAmount = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("TOTIGST_REFUNDAMT").Value), 0, RsTemp.Fields("TOTIGST_REFUNDAMT").Value), "0.00"))
            pNetBillAmount = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value), "0.00"))
            ValidateGSTNo = True
        End If
        Exit Function
ERR1:
        ValidateGSTNo = False
        '    ErrorMsg err.Description, err.Number, vbCritical
    End Function
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim I As Short
        Dim SqlStr As String
        Dim nMkey As String
        Dim mTRNType As String
        Dim mVNoSeq As Integer
        Dim mVNo As String
        Dim mSuppCustCode As String
        Dim mModvatSuppCode As String
        Dim mAccountCode As String
        Dim mControlAcctCode As String
        Dim pNewPosting As Boolean
        Dim mFREIGHTCHARGES As String
        Dim mEXEMPT_NOTIF_NO As String
        Dim mItemValue As Double
        Dim mTOTSTAMT As Double
        Dim mTOTCHARGES As Double
        Dim mTotEDAmount As Double
        Dim mTotEDUAmount As Double
        Dim mTOTEXPAMT As Double
        Dim mNETVALUE As Double
        Dim mTotQty As Double
        Dim mLSTCST As String
        Dim mWITHFORM As String
        Dim mPRINTED As String
        Dim mCancelled As String
        Dim mIsRegdNo As String
        Dim mSTType As String
        Dim mBookCode As Integer
        Dim mStartingNo As Integer
        Dim mSTPERCENT As Double
        Dim mTOTFREIGHT As Double
        Dim mEDPERCENT As Double
        Dim mEDUPERCENT As Double
        Dim mTOTTAXABLEAMOUNT As Double
        Dim mSRBillNo As String
        Dim mSRBillDate As String
        Dim mRO As Double
        Dim mSURAmount As Double
        Dim mMSC As Double
        Dim mTotDiscount As Double
        Dim mREJECTION As String
        Dim pDueDate As String
        Dim mModvatNo As Integer
        Dim mSERVNo As Integer
        Dim mSTCLAIMNo As Integer
        Dim mCapital As String
        Dim mNarration As String
        Dim mISMODVAT As String
        Dim mIsServClaim As String
        Dim mISSTREFUND As String
        Dim mFinalPost As String
        Dim mISGST As String
        Dim mItemType As String
        Dim cntRow As Integer
        Dim xShortageQty As Double
        Dim xRejectedQty As Double
        Dim xDebitAmt As Double
        Dim xPORate As Double
        Dim xRate As Double
        Dim xRateDiffDN As Double
        Dim xRateDiffCN As Double
        Dim mISTDSDEDUCT As String
        Dim mISESIDEDUCT As String
        Dim mISSTDSDEDUCT As String
        Dim mPDIRItem As Integer
        Dim PDIRAmount As Double
        Dim mDNCNCreated As Boolean
        Dim xExpDiffDN As Boolean
        Dim mFormRecdCode As Integer
        Dim mFormDueCode As Integer
        Dim mISFOC As String
        Dim mIsSuppBill As String
        Dim mServTax_Repost As String
        Dim mApproved As String
        Dim pJVVnoStr As String
        Dim pVType As String
        Dim pJVNo As String
        Dim pJVMKey As String
        Dim pRowNo As Integer
        Dim mDivisionCode As Double
        Dim mGSTNo As Double
        Dim mTotalGSTValue As Double
        Dim mGoodsService As String
        Dim pJVTMKey As String
        Dim pSectionCode As Long

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If
        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "ITEMTYPE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mItemType = MasterNo
        End If
        mFormRecdCode = -1
        mFormDueCode = -1
        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mTRNType = MasterNo
        Else
            mTRNType = CStr(-1)
            MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If
        mFinalPost = IIf(chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mSuppCustCode = CStr(-1)
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = CStr(-1)
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mISTDSDEDUCT = IIf(chkTDS.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mISESIDEDUCT = IIf(chkESI.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mISSTDSDEDUCT = IIf(ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        pSectionCode = -1

        If Trim(txtSection.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtSection.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                pSectionCode = MasterNo
            End If
        End If

        '*********
        If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        Else
            mAccountCode = CStr(-1)
            MsgBox("Credit Account Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If
        mControlAcctCode = "-1"
        If MainClass.ValidateWithMasterTable((RsCompany.Fields("COMPANY_CODE").Value), "COMPANY_CODE", "RATE_DIFF_ACCOUNTCODE", "FIN_PRINT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mControlAcctCode = MasterNo
        End If
        If MainClass.ValidateWithMasterTable(mControlAcctCode, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            mControlAcctCode = CStr(-1)
            MsgBox("Control Account Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If
        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
            mBookSubType = MasterNo
        Else
            mBookSubType = CStr(-1)
        End If
        mItemValue = Val(lblTotItemValue.Text)
        mTOTSTAMT = 0
        mTOTCHARGES = Val(lblTotCharges.Text)
        mTotEDAmount = 0
        mTOTEXPAMT = Val(lblTotExpAmt.Text)
        mNETVALUE = Val(lblNetAmount.Text)
        mSTPERCENT = 0
        mTOTFREIGHT = Val(lblTotFreight.Text)
        mEDPERCENT = 0
        mEDUPERCENT = 0
        mTOTTAXABLEAMOUNT = Val(lblTotTaxableAmt.Text)
        mRO = Val(lblRO.Text)
        mTotDiscount = Val(lblDiscount.Text)
        mSURAmount = Val(lblSurcharge.Text)
        mMSC = Val(lblMSC.Text)
        mTotQty = Val(lblTotQty.Text)
        mLSTCST = ""
        mWITHFORM = ""
        mPRINTED = "N"
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsRegdNo = "N"
        mCapital = IIf(chkCapital.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mISMODVAT = "N"
        mISSTREFUND = "N"
        mISGST = VB.Left(cboGSTStatus.Text, 1)
        mGoodsService = VB.Left(cboGoodService.Text, 1)
        mIsSuppBill = "N"
        mSTType = "0"
        mModvatNo = 0
        mSTCLAIMNo = 0
        pJVVnoStr = "-1"
        pJVMKey = "-1"
        If Val(txtVno.Text) = 0 Then
            mVNoSeq = CInt(AutoGenSeqBillNo())
        Else
            mVNoSeq = Val(txtVno.Text)
        End If
        '    If Left(cboGSTStatus.Text, 1) = "G" Then
        '        If Trim(txtGSTNo.Text) = "" Or Val(txtGSTNo.Text) = 0 Then
        '            mGSTNo = AutoGenSeqGSTNo()
        '        Else
        '            mGSTNo = Val(txtGSTNo.Text)
        '        End If
        '    End If
        mGSTNo = Val(txtGSTNo.Text)
        txtVno.Text = VB6.Format(Val(CStr(mVNoSeq)), "00000")
        mVNo = Trim(Trim(txtVNoPrefix.Text) & VB6.Format(Val(CStr(mVNoSeq)), "00000"))
        mTotalGSTValue = Val(lblTotCGST.Text) + Val(lblTotSGST.Text) + Val(lblTotIGST.Text)
        If Trim(txtNarration.Text) = "" Then
            txtNarration.Text = "Rates Revised wide PO NO " & txtPONo.Text & "/" & txtAmendNo.Text & " WEF. " & VB6.Format(txtWEFDate.Text, "DD/MM/YYYY") & " Till Bill Date " & VB6.Format(txtToDate.Text, "DD/MM/YYYY")
        End If

        If MODIFYMode = True Then
            If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_PURCHASE_HDR", (LblMKey.Text), RsSuppPurchMain, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_PURCHASE_DET", (LblMKey.Text), RsSuppPurchDetail, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_PURCHASE_EXP", (LblMKey.Text), RsSuppPurchExp, "MKEY", "M") = False Then GoTo ErrPart

        End If

        SqlStr = ""
        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("FIN_SUPP_PUR_HDR", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo
            LblMKey.Text = nMkey
            SqlStr = "INSERT INTO FIN_SUPP_PURCHASE_HDR( " & vbCrLf & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf & " ROWNO, TRNTYPE, VNOPREFIX, " & vbCrLf & " VNOSEQ, VNO, " & vbCrLf & " VDATE, BILLNO, INVOICE_DATE, " & vbCrLf & " AUTO_KEY_PO, PO_DATE, AMEND_NO, PO_WEFDATE, " & vbCrLf & " SUPP_CUST_CODE, ACCOUNTCODE, TARIFFHEADING, " & vbCrLf & " BOOKTYPE, BOOKSUBTYPE, REMARKS, " & vbCrLf & " ITEMDESC, ITEMVALUE, STPERCENT, " & vbCrLf & " TOTSTAMT, TOTFREIGHT, TOTCHARGES, " & vbCrLf & " EDPERCENT, TOTEDAMOUNT, TOTSURCHARGEAMT, " & vbCrLf & " TOTDISCAMOUNT, TOTMSCAMOUNT, TOTRO, " & vbCrLf & " TOTEXPAMT, TOTTAXABLEAMOUNT, NETVALUE, " & vbCrLf & " TOTQTY, STTYPE, STFORMCODE, "
            SqlStr = SqlStr & vbCrLf & " STFORMNAME, STFORMDATE, " & vbCrLf & " STDUEFORMCODE, STDUEFORMNAME, " & vbCrLf & " STDUEFORMDATE, " & vbCrLf & " ISREGDNO, LSTCST, WITHFORM, " & vbCrLf & " CANCELLED, NARRATION, MODVATNO, " & vbCrLf & " MODVATPER, MODVATAMOUNT, STCLAIMNO, " & vbCrLf & " STCLAIMPER, STCLAIMAMOUNT, SUR_VATCLAIMAMOUNT," & vbCrLf & " JVNO, JVMKEY, " & vbCrLf & " ISCAPITAL, ISMODVAT, " & vbCrLf & " ISSTREFUND, ISFINALPOST, PAYMENTDATE, " & vbCrLf & " MODVATITEMVALUE, " & vbCrLf & " TOTEDUPERCENT, TOTEDUAMOUNT, CESSABLEAMOUNT, " & vbCrLf & " CESSPER, CESSAMOUNT, TO_DATE," & vbCrLf & " SHECPERCENT, SHECAMOUNT, SHECMODVATPER, SHECMODVATAMOUNT, " & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE,DIV_CODE, " & vbCrLf & " ISGSTAPPLICABLE, GST_CLAIM_NO, GST_CLAIM_DATE, " & vbCrLf & " TOTALGSTVALUE, TOTCGST_REFUNDAMT, TOTSGST_REFUNDAMT, " & vbCrLf _
                & " TOTIGST_REFUNDAMT, TOTCGST_AMOUNT, TOTSGST_AMOUNT, TOTIGST_AMOUNT, GOODS_SERVICE,BILL_TO_LOC_ID," & vbCrLf _
                & " ISTDSDEDUCT,TDSPER,TDSAMOUNT, TDS_DEDUCT_ON, " & vbCrLf _
                & " ISESIDEDUCT,ESIPER,ESIAMOUNT, STDS_DEDUCT_ON, " & vbCrLf _
                & " ISSTDSDEDUCT,STDSPER,STDSAMOUNT, ESI_DEDUCT_ON, SECTION_CODE"


            SqlStr = SqlStr & vbCrLf _
                & " ) VALUES ( " & vbCrLf & " '" & nMkey & "', " & RsCompany.Fields("Company_Code").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & mCurRowNo & ", " & Val(mTRNType) & ", '" & MainClass.AllowSingleQuote(txtVNoPrefix.Text) & "'," & vbCrLf & " " & mVNoSeq & ", '" & MainClass.AllowSingleQuote(mVNo) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "',TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtPONo.Text) & ", TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtAmendNo.Text) & ", TO_DATE('" & VB6.Format(txtWEFDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mSuppCustCode & "', '" & mAccountCode & "', '" & MainClass.AllowSingleQuote(txtTariff.Text) & "', " & vbCrLf & " '" & mBookType & "', '" & mBookSubType & "', '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtItemType.Text) & "', " & mItemValue & ", " & mSTPERCENT & ", " & vbCrLf & " " & mTOTSTAMT & ", " & mTOTFREIGHT & ", " & mTOTCHARGES & ", " & vbCrLf & " " & mEDPERCENT & ", " & mTotEDAmount & ", " & mSURAmount & ", " & vbCrLf & " " & mTotDiscount & ", " & mMSC & ", " & mRO & ", " & vbCrLf & " " & mTOTEXPAMT & ", " & mTOTTAXABLEAMOUNT & ", " & mNETVALUE & "," & vbCrLf & " " & mTotQty & ", '" & mSTType & "', " & mFormRecdCode & ","

            SqlStr = SqlStr & vbCrLf & " '', '', " & vbCrLf & " " & mFormDueCode & ",'', " & vbCrLf & " '', " & vbCrLf & " '" & mIsRegdNo & "', '" & mLSTCST & "', '" & mWITHFORM & "', " & vbCrLf & " '" & mCancelled & "', '" & MainClass.AllowSingleQuote(txtNarration.Text) & "', '" & mModvatNo & "'," & vbCrLf & " 0,0, '" & mSTCLAIMNo & "'," & vbCrLf & " 0,0, 0, " & vbCrLf & " '" & pJVVnoStr & "', '" & pJVMKey & "', " & vbCrLf & " '" & mCapital & "', '" & mISMODVAT & "', " & vbCrLf & " '" & mISSTREFUND & "', '" & mFinalPost & "', TO_DATE('" & VB6.Format(txtPaymentdate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ," & vbCrLf & " 0, " & vbCrLf & " 0, 0, 0, " & vbCrLf & " 0 , 0 , " & vbCrLf & " TO_DATE('" & VB6.Format(txtToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " 0, 0, " & vbCrLf & " 0, 0, " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf & " '', ''," & mDivisionCode & ", " & vbCrLf & " '" & mISGST & "', '" & mGSTNo & "',TO_DATE('" & VB6.Format(txtGSTDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & mTotalGSTValue & ", " & Val(txtCGSTRefundAmt.Text) & ", " & Val(txtSGSTRefundAmt.Text) & "," & vbCrLf _
                & " " & Val(txtIGSTRefundAmt.Text) & ", " & Val(lblTotCGST.Text) & ", " & Val(lblTotSGST.Text) & "," & Val(lblTotIGST.Text) & ",'" & mGoodsService & "','" & txtLocationID.Text & "', " & vbCrLf _
                & " '" & mISTDSDEDUCT & "'," & Val(txtTDSRate.Text) & ", " & Val(txtTDSAmount.Text) & ", '" & Val(txtTDSDeductOn.Text) & "'," & vbCrLf _
                & " '" & mISESIDEDUCT & "'," & Val(txtESIRate.Text) & ", " & Val(txtESIAmount.Text) & ", " & Val(txtSTDSDeductOn.Text) & ", " & vbCrLf _
                & " '" & mISSTDSDEDUCT & "'," & Val(txtSTDSRate.Text) & ", " & Val(txtSTDSAmount.Text) & "," & Val(txtESIDeductOn.Text) & ", " & IIf(pSectionCode = -1, "NULL", pSectionCode) & "" & vbCrLf _
                & " ) "

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE FIN_SUPP_PURCHASE_HDR SET " & vbCrLf & " VNOPREFIX = '" & MainClass.AllowSingleQuote(txtVNoPrefix.Text) & "'," & vbCrLf & " TRNTYPE= " & Val(mTRNType) & ", GOODS_SERVICE='" & mGoodsService & "'," & vbCrLf & " VNOSEQ= " & mVNoSeq & "," & vbCrLf & " VNO= '" & MainClass.AllowSingleQuote(mVNo) & "'," & vbCrLf & " VDATE= TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " BILLNO= '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "'," & vbCrLf & " INVOICE_DATE= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " AUTO_KEY_PO= " & Val(txtPONo.Text) & "," & vbCrLf & " PO_DATE= TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " AMEND_NO= " & Val(txtAmendNo.Text) & ", PO_WEFDATE=TO_DATE('" & VB6.Format(txtWEFDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),"
            SqlStr = SqlStr & vbCrLf & " SUPP_CUST_CODE= '" & mSuppCustCode & "'," & vbCrLf & " ACCOUNTCODE= '" & mAccountCode & "'," & vbCrLf & " TARIFFHEADING= '" & MainClass.AllowSingleQuote(txtTariff.Text) & "'," & vbCrLf & " BOOKTYPE= '" & mBookType & "'," & vbCrLf & " BOOKSUBTYPE= '" & mBookSubType & "'," & vbCrLf & " REMARKS= '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf & " ITEMDESC= '" & MainClass.AllowSingleQuote(txtItemType.Text) & "'," & vbCrLf & " ITEMVALUE= " & mItemValue & "," & vbCrLf & " STPERCENT= " & mSTPERCENT & "," & vbCrLf & " TOTSTAMT= " & mTOTSTAMT & "," & vbCrLf & " TOTFREIGHT= " & mTOTFREIGHT & "," & vbCrLf & " TOTCHARGES= " & mTOTCHARGES & ","
            SqlStr = SqlStr & vbCrLf & " EDPERCENT= " & mEDPERCENT & "," & vbCrLf & " TOTEDAMOUNT= " & mTotEDAmount & "," & vbCrLf & " TOTSURCHARGEAMT=" & mSURAmount & "," & vbCrLf & " TOTDISCAMOUNT= " & mTotDiscount & "," & vbCrLf & " TOTMSCAMOUNT= " & mMSC & "," & vbCrLf & " TOTRO= " & mRO & "," & vbCrLf & " TOTEXPAMT= " & mTOTEXPAMT & "," & vbCrLf & " TOTTAXABLEAMOUNT= " & mTOTTAXABLEAMOUNT & "," & vbCrLf & " NETVALUE=" & mNETVALUE & "," & vbCrLf & " TOTQTY= " & mTotQty & ","
            '
            SqlStr = SqlStr & vbCrLf & " STTYPE= '" & mSTType & "'," & vbCrLf & " STFORMCODE= " & mFormRecdCode & "," & vbCrLf & " STFORMNAME= ''," & vbCrLf & " STFORMNO= ''," & vbCrLf & " STFORMDATE= ''," & vbCrLf & " STDUEFORMCODE= " & mFormDueCode & "," & vbCrLf & " STDUEFORMNAME= ''," & vbCrLf & " STDUEFORMNO= ''," & vbCrLf & " STDUEFORMDATE= ''," & vbCrLf & " ISREGDNO= '" & mIsRegdNo & "'," & vbCrLf & " LSTCST= '" & mLSTCST & "'," & vbCrLf & " WITHFORM= '" & mWITHFORM & "'," & vbCrLf & " CANCELLED= '" & mCancelled & "'," & vbCrLf & " NARRATION= '" & MainClass.AllowSingleQuote(txtNarration.Text) & "',"
            SqlStr = SqlStr & vbCrLf & " MODVATNO= '" & mModvatNo & "'," & vbCrLf & " MODVATPER=0 ," & vbCrLf & " MODVATAMOUNT= 0," & vbCrLf & " STCLAIMNO= '" & mSTCLAIMNo & "'," & vbCrLf & " STCLAIMPER= 0," & vbCrLf & " STCLAIMAMOUNT= 0," & vbCrLf & " SUR_VATCLAIMAMOUNT= 0," & vbCrLf & " ISCAPITAL= '" & mCapital & "'," & vbCrLf & " ISMODVAT= '" & mISMODVAT & "'," & vbCrLf & " ISSTREFUND= '" & mISSTREFUND & "'," & vbCrLf & " ISFINALPOST= '" & mFinalPost & "'," & vbCrLf & " PAYMENTDATE=  TO_DATE('" & VB6.Format(txtPaymentdate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ," & vbCrLf & " MODVATITEMVALUE=0 ," & vbCrLf & " TOTEDUPERCENT= 0," & vbCrLf & " TOTEDUAMOUNT= 0," & vbCrLf & " CESSABLEAMOUNT= 0," & vbCrLf & " CESSPER= 0, TO_DATE=TO_DATE('" & VB6.Format(txtToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " CESSAMOUNT= 0, " & vbCrLf & " JVNO='" & pJVVnoStr & "', " & vbCrLf & " JVMKEY='" & pJVMKey & "'," & vbCrLf & " SHECPERCENT=0," & vbCrLf & " SHECAMOUNT=0," & vbCrLf & " SHECMODVATPER=0," & vbCrLf & " SHECMODVATAMOUNT=0,DIV_CODE=" & mDivisionCode & ","
            SqlStr = SqlStr & vbCrLf _
                & " ISGSTAPPLICABLE='" & mISGST & "',  " & vbCrLf _
                & " GST_CLAIM_NO='" & mGSTNo & "', " & vbCrLf _
                & " GST_CLAIM_DATE= TO_DATE('" & VB6.Format(txtGSTDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " GST_CLAIM='" & IIf(chkGSTClaim.CheckState = System.Windows.Forms.CheckState.Unchecked, "N", lblClaimStatus.Text) & "', " & vbCrLf _
                & " TOTALGSTVALUE=" & mTotalGSTValue & ", " & vbCrLf _
                & " TOTCGST_REFUNDAMT= " & Val(txtCGSTRefundAmt.Text) & ", " & vbCrLf _
                & " TOTSGST_REFUNDAMT= " & Val(txtSGSTRefundAmt.Text) & ", " & vbCrLf _
                & " TOTIGST_REFUNDAMT= " & Val(txtIGSTRefundAmt.Text) & ", " & vbCrLf _
                & " TOTCGST_AMOUNT=" & Val(lblTotCGST.Text) & ", " & vbCrLf _
                & " TOTSGST_AMOUNT=" & Val(lblTotSGST.Text) & ", " & vbCrLf _
                & " TOTIGST_AMOUNT=" & Val(lblTotIGST.Text) & "," & vbCrLf _
                & " ISTDSDEDUCT='" & mISTDSDEDUCT & "',TDSPER=" & Val(txtTDSRate.Text) & ",TDSAMOUNT=" & Val(txtTDSAmount.Text) & ", TDS_DEDUCT_ON='" & Val(txtTDSDeductOn.Text) & "', " & vbCrLf _
                & " ISESIDEDUCT='" & mISESIDEDUCT & "',ESIPER=" & Val(txtESIRate.Text) & ",ESIAMOUNT=" & Val(txtESIAmount.Text) & ", STDS_DEDUCT_ON=" & Val(txtSTDSDeductOn.Text) & ", " & vbCrLf _
                & " ISSTDSDEDUCT='" & mISSTDSDEDUCT & "',STDSPER=" & Val(txtSTDSRate.Text) & ",STDSAMOUNT=" & Val(txtSTDSAmount.Text) & ", ESI_DEDUCT_ON=" & Val(txtESIDeductOn.Text) & ", SECTION_CODE=" & IIf(pSectionCode = -1, "NULL", pSectionCode) & "," & vbCrLf

            SqlStr = SqlStr & vbCrLf _
                & " BILL_TO_LOC_ID='" & txtLocationID.Text & "', ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)

        pNewPosting = False
        If UpdateDetail1(mNarration, mVNo, mSuppCustCode, mDivisionCode, mControlAcctCode, pNewPosting) = False Then GoTo ErrPart
        '    If Left(cboGSTStatus.Text, 1) = "G" Then ''chkCancelled.Value = vbUnchecked
        '        If UpdateGSTSeqMaster(PubDBCn, LblMKey.text, LblBookCode, mBookType, mBookSubType, _
        ''                        mGSTNo, Format(txtGSTDate.Text, "DD-MMM-YYYY"), mCapital, "N", "G" _
        ''                        ) = False Then GoTo ErrPart:
        '    End If
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then

                If PurchaseSuppPostTRNGST(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mVNo, (txtVDate.Text), (txtBillNo.Text), (txtBillDate.Text), mTRNType, mSuppCustCode, IIf(pNewPosting = True, mControlAcctCode, mAccountCode), Val(CStr(mItemValue)), Val(CStr(mNETVALUE)), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False), False, pDueDate, VB.Left(mNarration, 254), (txtRemarks.Text), Val(lblTotExpAmt.Text), IIf(mISGST = "G", "Y", "N"), Val(txtCGSTRefundAmt.Text), Val(txtSGSTRefundAmt.Text), Val(txtIGSTRefundAmt.Text), (txtVDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, Trim(txtPONo.Text), txtLocationID.Text) = False Then GoTo ErrPart
                '            If Left(cboGSTStatus.Text, 1) = "G" Then
                '                If UpDateSuppBill(Val(txtGSTNo.Text), mVNo, mCapital) = False Then GoTo ErrPart
                '            End If
            End If
        End If

        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Or chkESI.CheckState = System.Windows.Forms.CheckState.Checked Or ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            pJVTMKey = lblJVTMKey.Text
            If Val(txtTDSAmount.Text) + Val(txtESIAmount.Text) + Val(txtSTDSAmount.Text) > 0 Then
                If UpdateTDSVoucher(mDivisionCode, pJVTMKey) = False Then GoTo ErrPart
                SqlStr = "UPDATE FIN_SUPP_PURCHASE_HDR SET JVNO='" & txtJVVNO.Text & "', " & vbCrLf _
                            & " JVT_MKEY='" & pJVTMKey & "'," & vbCrLf _
                            & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                            & " WHERE MKEY='" & LblMKey.Text & "'"
                PubDBCn.Execute(SqlStr)
            End If
        End If
        'End If

        PubDBCn.CommitTrans()
        If ADDMode = True And Trim(txtJVVNO.Text) <> "" Then
            MsgBox("TDS Journal Voucher No. " & txtJVVNO.Text & " Created. ", MsgBoxStyle.Information)
        End If
        UpdateMain1 = True
        Exit Function
ErrPart:
        ' Resume
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsSuppPurchMain.Requery() ''.Refresh
        RsSuppPurchDetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '    Resume
    End Function
    Private Function UpdateTDSVoucher(ByRef mDivisionCode As Double, ByRef pJVTMKey As String) As Boolean
        On Error GoTo ErrPart
        Dim mBookCode As String
        Dim SqlStr As String = ""
        Dim mAccountCode As String
        Dim mDrCr As String
        Dim mVAmount As Double
        Dim mVnoStr As String
        Dim mVType As String
        Dim mVNoPrefix As String
        Dim mVNoSuffix As String
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mVNo As String
        Dim mCancelled As String
        Dim pRowNo As Integer
        Dim CurJVMKey As String = ""
        mBookType = VB.Left(ConJournal, 1)
        mBookSubType = VB.Right(ConJournal, 1)
        Dim pAddMode As Boolean
        mVType = "JVT"

        If pJVTMKey = "" Then
            mVNo = GenJVVno(mVType)
            mVNoPrefix = GenPrefixVNo(txtVDate.Text)
            mVNoSuffix = ""
            mVnoStr = mVType & mVNoPrefix & mVNo & mVNoSuffix
            txtJVVNO.Text = mVnoStr
            pAddMode = True
        Else
            mVnoStr = txtJVVNO.Text
            pAddMode = False
        End If

        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mBookCode = CStr(ConJournalBookCode)
        If pAddMode = True Then
            pRowNo = MainClass.AutoGenRowNo("FIN_VOUCHER_HDR", "RowNo", PubDBCn)
            CurJVMKey = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value) & VB6.Format(RsCompany.Fields("FYEAR").Value) & VB6.Format(pRowNo)
            pJVTMKey = CurJVMKey
            SqlStr = " INSERT INTO FIN_VOUCHER_HDR ( " & vbCrLf _
                & " Mkey, COMPANY_CODE, " & vbCrLf _
                & " FYEAR,RowNo, VType, VNoPrefix, VNoSeq, VNoSuffix, " & vbCrLf _
                & " Vno, Vdate, BookType,BookSubType, " & vbCrLf _
                & " BookCode, Narration, CANCELLED, " & vbCrLf _
                & " AddUser, AddDate, ModUser, ModDate,UPDATE_FROM,EXPDATE,IS_REVERSAL_MADE, IS_REVERSAL_VOUCHER, REVERSAL_MKEY ) VALUES ( " & vbCrLf _
                & " '" & CurJVMKey & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                & " " & pRowNo & ", " & vbCrLf & " '" & mVType & "', '" & mVNoPrefix & "', " & vbCrLf & " " & Val(mVNo) & ", '" & mVNoSuffix & "', '" & mVnoStr & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & mBookType & "', '" & mBookSubType & "', " & vbCrLf _
                & " '" & mBookCode & "', '" & MainClass.AllowSingleQuote("") & "', '" & mCancelled & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','N'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'N','N','')"
        Else                ''If MODIFYMode = True Then
            SqlStr = "UPDATE FIN_VOUCHER_HDR SET " & vbCrLf _
                & " Vdate=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " EXPDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " Narration='" & MainClass.AllowSingleQuote(txtNarration.Text) & "', " & vbCrLf _
                & " CANCELLED='" & mCancelled & "', " & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " Where Mkey='" & pJVTMKey & "'"
        End If

        'SqlStr = "UPDATE FIN_VOUCHER_HDR SET " & vbCrLf _
        '        & " Vdate=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
        '        & " EXPDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
        '        & " VType= '" & mVType & "'," & vbCrLf _
        '        & " VnoPrefix='" & mVNoPrefix & "', " & vbCrLf _
        '        & " VnoSeq=" & Val(mVNo) & ", " & vbCrLf _
        '        & " VnoSuffix='" & mVNoSuffix & "', " & vbCrLf _
        '        & " Vno='" & mVnoStr & "', " & vbCrLf _
        '        & " BookCode='" & mBookCode & "', " & vbCrLf _
        '        & " Narration='" & MainClass.AllowSingleQuote(txtNarration.Text) & "', " & vbCrLf _
        '        & " CANCELLED='" & mCancelled & "', " & vbCrLf _
        '        & " BookType='" & mBookType & "', " & vbCrLf _
        '        & " BookSubType='" & mBookSubType & "', " & vbCrLf _
        '        & " UPDATE_FROM='N'," & vbCrLf _
        '        & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
        '        & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
        '        & " Where Mkey='" & pJVTMKey & "'"

        PubDBCn.Execute(SqlStr)

        If UpdateJVDetail(pJVTMKey, pRowNo, mBookCode, mVType, mVnoStr, (txtVDate.Text), "", PubDBCn, mDivisionCode) = False Then GoTo ErrPart

        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            If UpdateTDSCreditDetail(pJVTMKey, mVnoStr, mBookType, mBookSubType, pAddMode) = False Then GoTo ErrPart
        End If
        '    txtVno.Text = mVNo
        UpdateTDSVoucher = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateTDSVoucher = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateTDSCreditDetail(ByRef pMKey As String, ByRef pVNoStr As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pAddMode As Boolean) As Boolean
        On Error GoTo UpdateError
        Dim SqlStr As String = ""
        Dim mTDSAccountCode As String
        Dim mExempted As String
        Dim mSectionCode As Integer
        Dim mAmountPaid As Double
        Dim mTdsRate As Double
        Dim mPartyName As String
        Dim mPartyCode As String
        SqlStr = ""
        'SqlStr = "DELETE FROM TDS_TRN WHERE MKey= '" & pMKey & "'"
        'PubDBCn.Execute(SqlStr)
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Unchecked Or chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            UpdateTDSCreditDetail = True
            Exit Function
        End If
        mTDSAccountCode = GetTDSAccountCode(txtSection.Text)       '' IIf(IsDBNull(RsCompany.Fields("TDSCREDITACCOUNT").Value), "", RsCompany.Fields("TDSCREDITACCOUNT").Value)
        If mTDSAccountCode = "" Then
            ErrorMsg("TDS ACCOUNT Code not Defined into System Pref.", "", MsgBoxStyle.Critical)
            UpdateTDSCreditDetail = False
        End If
        mPartyName = Trim(txtSupplier.Text)
        If MainClass.ValidateWithMasterTable(mPartyName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyCode = MasterNo
        Else
            mPartyCode = "-1"
        End If
        'If MainClass.ValidateWithMasterTable(mPartyCode, "SUPP_CUST_CODE", "SECTIONCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mSectionCode = MasterNo
        'Else
        '    mSectionCode = CInt("-1")
        'End If

        mSectionCode = -1

        If Trim(txtSection.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtSection.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSectionCode = MasterNo
            End If
        End If

        mAmountPaid = Val(CStr(CDbl(txtTDSDeductOn.Text)))
        mTdsRate = Val(txtTDSRate.Text)
        mExempted = "N"
        If pAddMode = True Then
            SqlStr = "INSERT INTO TDS_TRN ( MKEY, COMPANY_CODE, " & vbCrLf _
                & " FYEAR, ROWNO, SUBROWNO, VNO,VDATE, " & vbCrLf _
                & " BOOKCODE, BOOKTYPE, BOOKSUBTYPE, ACCOUNTCODE, " & vbCrLf _
                & " PARTYCODE,PARTYNAME, AMOUNTPAID, SECTIONCODE, " & vbCrLf _
                & " TDSRATE, ISEXEPTED, EXEPTIONCNO, " & vbCrLf _
                & " TDSAMOUNT, ADDUSER, ADDDATE, MODUSER, MODDATE,UPDATE_FROM) VALUES ( "
            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(pMKey)) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " 1,1,'" & MainClass.AllowSingleQuote(pVNoStr) & "', TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & -1 & ",'" & pBookType & "', '" & pBookSubType & "', " & vbCrLf & " '" & mTDSAccountCode & "', '" & MainClass.AllowSingleQuote(mPartyCode) & "', '" & MainClass.AllowSingleQuote(mPartyName) & "', " & vbCrLf & " " & Val(CStr(mAmountPaid)) & "," & mSectionCode & "," & Val(CStr(mTdsRate)) & ", " & vbCrLf & " '" & mExempted & "','', " & vbCrLf & " " & Val(txtTDSAmount.Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','N')"
        Else
            SqlStr = " UPDATE TDS_TRN SET " & vbCrLf _
                & " VDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " ACCOUNTCODE='" & mTDSAccountCode & "', " & vbCrLf _
                & " PARTYCODE='" & MainClass.AllowSingleQuote(mPartyCode) & "', PARTYNAME='" & MainClass.AllowSingleQuote(mPartyName) & "', " & vbCrLf _
                & " VNO='" & MainClass.AllowSingleQuote(pVNoStr) & "', " & vbCrLf _
                & " AMOUNTPAID=" & Val(CStr(mAmountPaid)) & ", " & vbCrLf _
                & " SECTIONCODE=" & mSectionCode & "," & vbCrLf _
                & " TDSRATE=" & Val(CStr(mTdsRate)) & ", " & vbCrLf _
                & " ISEXEPTED='" & mExempted & "', " & vbCrLf _
                & " EXEPTIONCNO='', " & vbCrLf _
                & " TDSAMOUNT=" & Val(txtTDSAmount.Text) & ", UPDATE_FROM='N'," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE MKey= '" & pMKey & "'"
        End If
        PubDBCn.Execute(SqlStr)
        UpdateTDSCreditDetail = True
        Exit Function
UpdateError:
        UpdateTDSCreditDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Function AutoGenSeqBillNo() As String
        On Error GoTo AutoGenSeqBillNoErr
        Dim RsSuppPurchMainGen As ADODB.Recordset
        Dim mNewSeqBillNo As Double
        Dim SqlStr As String
        Dim pStartingNo As String
        Dim xFyear As Integer
        Dim mMaxNo As Double
        SqlStr = ""
        xFyear = CInt(VB6.Format(RsCompany.Fields("START_DATE").Value, "YY"))
        pStartingNo = CStr(1)

        If RsCompany.Fields("FYEAR").Value >= 2020 Then
            pStartingNo = xFyear & VB6.Format(IIf(IsDbNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & VB6.Format(pStartingNo, "00000")
        End If
        SqlStr = ""
        SqlStr = "SELECT Max(VNOSEQ)  FROM FIN_SUPP_PURCHASE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSuppPurchMainGen
            If .EOF = False Then
                mMaxNo = IIf(IsDBNull(.Fields(0).Value), -1, .Fields(0).Value)
                If mMaxNo <= 0 Then
                    mNewSeqBillNo = CInt(pStartingNo)
                ElseIf Not IsDBNull(.Fields(0).Value) Then
                    mNewSeqBillNo = .Fields(0).Value + 1
                Else
                    mNewSeqBillNo = CInt(pStartingNo)
                End If
            Else
                mNewSeqBillNo = CInt(pStartingNo)
            End If
        End With
        AutoGenSeqBillNo = CStr(mNewSeqBillNo)
        Exit Function
AutoGenSeqBillNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateDetail1(ByRef xNarration As String, ByRef pVNo As String, ByRef pSuppCustCode As String, ByRef mDivisionCode As Double, ByRef mControlAcctCode As String, ByRef pNewPosting As Boolean) As Boolean
        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String
        Dim I As Integer
        Dim mItemCode As String
        Dim mPartNo As String
        Dim mItemDesc As String
        Dim mUnit As String
        Dim mPurFYear As Integer
        Dim mPurMkey As String
        Dim mVNo As String
        Dim mVDate As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillQty As Double
        Dim mBillRate As Double
        Dim mPORate As Double
        Dim mQty As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mMRRNO As Double
        Dim mMRRDate As String
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim pInvType As String
        Dim mInvTypeCode As Double
        Dim mDebitAccountCode As String
        Dim mAccountCode As String
        Dim mHSNCode As String
        Dim mPONo As Double
        Dim mPODate As String
        If Trim(txtDebitAccount.Text) = "" Then
            mAccountCode = "-1"
        Else
            If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                mAccountCode = "-1"
                MsgBox("Debit Account Does Not Exist In Master", MsgBoxStyle.Information)
                GoTo UpdateDetail1
            End If
        End If
        PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & mBookType & "' AND BOOKCODE='" & LblBookCode.Text & "'")
        PubDBCn.Execute("Delete From FIN_SUPP_PURCHASE_DET Where Mkey='" & LblMKey.Text & "'")
        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I
                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)
                .Col = ColPartNo
                mPartNo = MainClass.AllowSingleQuote(.Text)
                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)
                xNarration = xNarration & IIf(xNarration = "", "", ", ") & mItemDesc
                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)
                .Col = ColHSNCode
                mHSNCode = MainClass.AllowSingleQuote(.Text)
                .Col = ColPONo
                mPONo = Val(.Text)
                .Col = ColPODate
                mPODate = VB6.Format(.Text, "DD/MM/YYYY")
                .Col = ColPURFYear
                mPurFYear = Val(.Text)
                .Col = ColPURMkey
                mPurMkey = MainClass.AllowSingleQuote(.Text)
                .Col = ColVNo
                mVNo = MainClass.AllowSingleQuote(.Text)
                .Col = ColVDate
                mVDate = VB6.Format(.Text, "DD/MM/YYYY")
                .Col = ColBillNo
                mBillNo = MainClass.AllowSingleQuote(.Text)
                .Col = ColBillDate
                mBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                If CDate(mBillDate) >= CDate("01/04/2022") Then
                    pNewPosting = True
                End If

                .Col = ColMRRNo
                mMRRNO = MainClass.AllowSingleQuote(.Text)
                .Col = ColMRRDate
                mMRRDate = VB6.Format(.Text, "DD/MM/YYYY")
                .Col = ColBillQty
                mBillQty = Val(.Text)
                .Col = ColBillRate
                mBillRate = Val(.Text)
                .Col = ColPORate
                mPORate = Val(.Text)
                .Col = ColQty
                mQty = Val(.Text)
                .Col = ColRate
                mRate = Val(.Text)
                .Col = ColAmount
                mAmount = Val(.Text)
                .Col = ColCGSTPer
                mCGSTPer = Val(.Text)
                .Col = ColCGSTAmount
                mCGSTAmount = Val(.Text)
                .Col = ColSGSTPer
                mSGSTPer = Val(.Text)
                .Col = ColSGSTAmount
                mSGSTAmount = Val(.Text)
                .Col = ColIGSTPer
                mIGSTPer = Val(.Text)
                .Col = ColIGSTAmount
                mIGSTAmount = Val(.Text)
                .Col = ColInvType
                pInvType = Trim(.Text)
                If pInvType = "" Then
                    pInvType = Trim(cboInvType.Text)
                    mDebitAccountCode = mAccountCode
                Else
                    '                mDebitAccountCode = GetDebitNameOfInvType(pInvType, "N")
                    If MainClass.ValidateWithMasterTable(pInvType, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='O'") = True Then
                        mDebitAccountCode = MasterNo
                    Else
                        MsgBox("Invoice Type Does Not Exist In Master", MsgBoxStyle.Information)
                        GoTo UpdateDetail1
                    End If
                    If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                        If mDebitAccountCode = "-1" Then MsgBox("Account Code not Defined For Item Code : " & mItemCode) : GoTo UpdateDetail1
                    End If
                End If
                ''Temp Check
                mControlAcctCode = mDebitAccountCode

                '            If MainClass.ValidateWithMasterTable(pInvType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                mInvTypeCode = MasterNo
                '            Else
                '                MsgBox "Invoice Type Does Not Exist In Master", vbInformation
                '                GoTo UpdateDetail1
                '            End If
                SqlStr = ""
                If mItemCode <> "" And mAmount > 0 Then
                    SqlStr = " INSERT INTO FIN_SUPP_PURCHASE_DET ( " & vbCrLf & " MKEY , SUBROWNO, " & vbCrLf & " ITEM_CODE , CUSTOMER_PART_NO, " & vbCrLf & " ITEM_DESC, HSNCODE, ITEM_UOM, " & vbCrLf & " PUR_FYEAR, PUR_MKEY, " & vbCrLf & " PURNO, PUR_DATE, " & vbCrLf & " BILL_NO, BILLDATE, " & vbCrLf & " BILL_QTY, BILL_RATE, " & vbCrLf & " PO_RATE, QTY, " & vbCrLf & " RATE, AMOUNT, " & vbCrLf & " ITEM_ED, ITEM_ST, " & vbCrLf & " ITEM_CESS, COMPANY_CODE, " & vbCrLf & " AUTO_KEY_MRR, MRRDATE, " & vbCrLf & " CGST_PER, CGST_AMOUNT, " & vbCrLf & " SGST_PER, SGST_AMOUNT, " & vbCrLf & " IGST_PER, IGST_AMOUNT, PUR_ACCOUNT_CODE, ITEM_TRNTYPE,PONO,PODATE " & vbCrLf & " ) "
                    SqlStr = SqlStr & vbCrLf & " VALUES ('" & LblMKey.Text & "'," & I & ", " & vbCrLf _
                        & " '" & mItemCode & "', '" & mPartNo & "'," & vbCrLf _
                        & " '" & mItemDesc & "', '" & mHSNCode & "', '" & mUnit & "'," & vbCrLf & " " & mPurFYear & ", '" & mPurMkey & "', " & vbCrLf _
                        & " '" & mVNo & "', TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " '" & mBillNo & "', TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " " & mBillQty & ", " & mBillRate & ", " & vbCrLf & " " & mPORate & ", " & mQty & ", " & vbCrLf & " " & mRate & ", " & mAmount & ", " & vbCrLf _
                        & " 0, 0," & vbCrLf & " 0," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " " & Val(CStr(mMRRNO)) & ", TO_DATE('" & VB6.Format(mMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " " & mCGSTPer & ", " & mCGSTAmount & "," & vbCrLf & " " & mSGSTPer & ", " & mSGSTAmount & "," & vbCrLf _
                        & " " & mIGSTPer & ", " & mIGSTAmount & ", '" & MainClass.AllowSingleQuote(IIf(pNewPosting = True, mControlAcctCode, mDebitAccountCode)) & "', " & mInvTypeCode & "," & vbCrLf _
                        & " " & Val(CStr(mPONo)) & ", TO_DATE('" & VB6.Format(mPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

                    PubDBCn.Execute(SqlStr)

                    If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked And VB.Left(cboGSTStatus.Text, 1) = "G" Then
                            If UpdateGSTTRN(PubDBCn, (LblMKey.Text), (LblBookCode.Text), mBookType, mBookSubType, pVNo, VB6.Format(txtVDate.Text, "DD-MMM-YYYY"), Trim(txtBillNo.Text), VB6.Format(txtBillDate.Text, "DD-MMM-YYYY"), mBillNo, VB6.Format(mBillDate, "DD-MMM-YYYY"), pSuppCustCode, mDebitAccountCode, "Y", pSuppCustCode, I, mItemCode, mQty, mUnit, mRate, mAmount, mAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, mDivisionCode, mHSNCode, mItemDesc, "", "N", "", "G", IIf(VB.Left(cboGSTStatus.Text, 1) = "R", "Y", "N"), "C", VB6.Format(txtGSTDate.Text, "DD-MMM-YYYY"), "N") = False Then GoTo UpdateDetail1
                        End If
                    End If
                End If
            Next
        End With
        UpdateDetail1 = True
        UpdateDetail1 = UpdateExp1
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function UpdateExp1() As Boolean
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim SqlStr As String
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String
        Dim mDebitAmount As String
        PubDBCn.Execute("Delete From FIN_SUPP_PURCHASE_EXP Where Mkey='" & LblMKey.Text & "'")
        With SprdExp
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColExpName
                If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mExpCode = MasterNo
                Else
                    mExpCode = -1
                End If
                .Col = ColExpPercent
                mPercent = Val(.Text)
                .Col = ColExpAmt
                mExpAmount = Val(.Text)
                SprdExp.Col = ColExpAddDeduct
                m_AD = SprdExp.Text
                If m_AD = "D" Then
                    mExpAmount = mExpAmount * -1
                End If
                SprdExp.Col = ColExpCalcOn
                mCalcOn = Val(.Text)
                .Col = ColExpDebitAmt
                mDebitAmount = CStr(Val(.Text))
                .Col = ColRO
                mRO = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
                SqlStr = ""
                If mCalcOn <> 0 Or mExpAmount <> 0 Then
                    SqlStr = "Insert Into  FIN_SUPP_PURCHASE_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO,DebitAmount) " & vbCrLf & " Values ('" & LblMKey.Text & "'," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mCalcOn & ",'" & mRO & "'," & mDebitAmount & ")"
                    PubDBCn.Execute(SqlStr)
                End If
            Next I
        End With
        UpdateExp1 = True
        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateExp1 = False
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim cntRow As Integer
        Dim xShortageQty As Double
        Dim xRejectedQty As Double
        Dim xPORate As Double
        Dim xRate As Double
        Dim xRateDiffDN As Double
        Dim xRateDiffCN As Double
        Dim mExciseDutyAmt As Double
        Dim mServiceTaxAmt As Double
        Dim mEDUAmt As Double
        Dim mSHECessAmt As Double
        Dim mSalesTaxAmount As Double
        Dim mWithInState As String
        Dim xPoNo As String
        Dim mPORateZero As Boolean
        Dim mLockBookCode As Integer
        Dim mAgtPO As Boolean
        Dim xSuppCode As String
        Dim xGSTDate As String
        Dim xCGSTAmount As Double
        Dim xIGSTAmount As Double
        Dim xSGSTAmount As Double
        Dim xBillAmount As Double
        Dim pISGSTRegd As String
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim pErrorMsg As String
        Dim mPurType As String
        Dim mPONo As Double
        Dim mAccountCode As String
        FieldsVarification = True
        mCompanyGSTNo = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = Trim(MasterNo)
        End If

        mPartyGSTNo = ""
        mPartyGSTNo = GetPartyBusinessDetail(mAccountCode, Trim(txtLocationID.Text), "GST_RGN_NO")

        mAgtPO = False
        If ValidateBranchLocking((TxtVDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        mLockBookCode = CInt(ConLockPurchase)
        If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateAccountLocking(PubDBCn, TxtVDate.Text, (txtSupplier.Text), mSupplierCode) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsSuppPurchMain.EOF = True Then Exit Function
        If MODIFYMode = True And txtVno.Text = "" Then
            MsgInformation("Voucher No. is Blank")
            FieldsVarification = False
            Exit Function
        End If
        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If
        If Val(lblNetAmount.Text) <= 0 Then
            MsgBox("Cann't Save. Net Value is less than 0", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        If TxtVDate.Text = "" Then
            MsgBox("VDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            TxtVDate.Focus()
            Exit Function
        ElseIf FYChk((TxtVDate.Text)) = False Then
            FieldsVarification = False
            If TxtVDate.Enabled = True Then TxtVDate.Focus()
            Exit Function
        End If
        If txtBillNo.Text = "" Then
            MsgInformation("Bill No. is Blank")
            FieldsVarification = False
            Exit Function
        End If
        If txtBillDate.Text = "" Then
            MsgBox("BillDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtBillDate.Focus()
            Exit Function
        ElseIf Not IsDate(txtBillDate.Text) Then
            MsgBox("Invalid Bill Date", MsgBoxStyle.Information)
            FieldsVarification = False
            txtBillDate.Focus()
            Exit Function
        End If
        If txtToDate.Text = "" Then
            MsgBox("To Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtToDate.Focus()
            Exit Function
        ElseIf Not IsDate(txtToDate.Text) Then
            MsgBox("Invalid To Date", MsgBoxStyle.Information)
            FieldsVarification = False
            txtToDate.Focus()
            Exit Function
        End If
        If CDate(TxtVDate.Text) < CDate(txtWEFDate.Text) Then
            MsgBox("VDate Can Not be Less Than WEFDate.")
            FieldsVarification = False
            TxtVDate.Focus()
            Exit Function
        End If
        If CDate(TxtVDate.Text) < CDate(txtBillDate.Text) Then
            MsgBox("VDate Can Not be Less Than BillDate.")
            FieldsVarification = False
            If txtBillDate.Enabled = True Then txtBillDate.Focus()
            Exit Function
        End If
        If ValidateBillNo((txtBillNo.Text), pErrorMsg) = False Then
            MsgInformation(pErrorMsg)
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtSupplier.Text) = "" Then
            MsgBox("Supplier Cannot Be Blank", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xSuppCode = MasterNo
        Else
            MsgBox("Supplier Does Not Exist In Master", MsgBoxStyle.Information)
            'txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If
        pISGSTRegd = "N"
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            pISGSTRegd = MasterNo
        End If
        If cboGSTStatus.SelectedIndex = -1 Then
            MsgBox("Please select GST Status", MsgBoxStyle.Information)
            If cboGSTStatus.Enabled = True Then cboGSTStatus.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If cboGoodService.SelectedIndex = -1 Then
            MsgBox("Please select Goods or Service", MsgBoxStyle.Information)
            If cboGoodService.Enabled = True Then cboGoodService.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtPONo.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((txtPONo.Text), "AUTO_KEY_PO", "PUR_TYPE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPurType = MasterNo
            mPurType = Trim(mPurType)
        End If
        If mPurType = "" Then
            SprdMain.Row = 1
            SprdMain.Col = ColPONo
            mPONo = Val(SprdMain.Text)
            If MainClass.ValidateWithMasterTable(mPONo, "AUTO_KEY_PO", "PUR_TYPE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mPurType = MasterNo
                    mPurType = Trim(mPurType)
                End If
            End If
            If mPurType = "J" Then
                If cboGoodService.SelectedIndex = 0 Then
                    MsgBox("Please Select the Service.", MsgBoxStyle.Information)
                    If cboGoodService.Enabled = True Then cboGoodService.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
            Else
                If cboGoodService.SelectedIndex = 1 Then
                    MsgBox("Please Select the Goods.", MsgBoxStyle.Information)
                    If cboGoodService.Enabled = True Then cboGoodService.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If
        If pISGSTRegd = "Y" And VB.Left(cboGSTStatus.Text, 1) <> "G" Then
            MsgBox("Supplier is registered, please select the GST Refund.", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If
        If pISGSTRegd = "N" And VB.Left(cboGSTStatus.Text, 1) <> "R" Then
            MsgBox("Supplier is not registered, please select the Reverse Charge.", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If
        If pISGSTRegd = "E" And VB.Left(cboGSTStatus.Text, 1) <> "E" Then
            MsgBox("GST Exempted Supplier, please select the GST Exempted.", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If
        If VB.Left(cboGSTStatus.Text, 1) = "G" Then
            '        If Val(txtGSTNo.Text) <= 0 Then
            '            MsgBox "GST No Cannot Be Blank", vbInformation
            '            txtGSTNo.SetFocus
            '            FieldsVarification = False
            '            Exit Function
            '        End If
            '
            '        If txtGSTDate.Text = "" Then
            '            MsgBox "GST Date is Blank", vbInformation
            '            FieldsVarification = False
            '            Exit Function
            '        ElseIf Not IsDate(txtGSTDate.Text) Then
            '            MsgBox "Invalid GST Date", vbInformation
            '            FieldsVarification = False
            '            Exit Function
            '        End If
            '        If Trim(txtTariff.Text) = "" Then
            '            MsgBox "Tariff Heading Cannot Be Blank", vbInformation
            '            txtTariff.SetFocus
            '            FieldsVarification = False
            '            Exit Function
            '        End If
            '        If ValidateGSTNo(xSuppCode, Trim(txtBillNo.Text), Trim(txtBillDate.Text), Trim(txtGSTNo.Text), xGSTDate, xCGSTAmount, xIGSTAmount, xSGSTAmount, xBillAmount) = True Then
            '            If xBillAmount <> Val(lblNetAmount.text) Then
            '                MsgBox "Bill Amount is Not Match.", vbInformation
            '                FieldsVarification = False
            '                Exit Function
            '            End If
            '
            '            If Val(xCGSTAmount) <> Val(txtCGSTRefundAmt.Text) Then
            '                MsgBox "CGST Amount is Not Match.", vbInformation
            '                FieldsVarification = False
            '                Exit Function
            '            End If
            '
            '            If Val(xSGSTAmount) <> Val(txtSGSTRefundAmt.Text) Then
            '                MsgBox "SGST Amount is Not Match.", vbInformation
            '                FieldsVarification = False
            '                Exit Function
            '            End If
            '
            '            If Val(xIGSTAmount) <> Val(txtIGSTRefundAmt.Text) Then
            '                MsgBox "IGST Amount is Not Match.", vbInformation
            '                FieldsVarification = False
            '                Exit Function
            '            End If
            '        Else
            '            MsgBox "Invalid GST No.", vbInformation
            '            FieldsVarification = False
            '            Exit Function
            '        End If
            '        Call txtGSTNo_Validate(False)
        End If
        '    If Val(txtGSTNo.Text) > 0 And Left(cboGSTStatus.Text, 1) <> "G" Then
        '        MsgBox "Please check GST Refund.", vbInformation
        '        If txtGSTNo.Enabled = True Then txtGSTNo.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
            cboInvType.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Debit Account Does Not Exist In Master", MsgBoxStyle.Information)
            'txtDebitAccount.SetFocus
            FieldsVarification = False
            Exit Function
        End If
        '    If Trim(txtItemType.Text) = "" Then
        '        MsgBox "Item Type is Blank", vbInformation
        '        FieldsVarification = False
        '        txtItemType.SetFocus
        '        Exit Function
        '    End If
        If txtPaymentDate.Text = "" Then
            MsgBox("Payment Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPaymentDate.Focus()
            Exit Function
        ElseIf Not IsDate(txtPaymentDate.Text) Then
            MsgBox("Invalid Payment Date", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPaymentDate.Focus()
            Exit Function
        End If
        If Trim(mPartyGSTNo) <> Trim(mCompanyGSTNo) Then
            If VB.Left(cboGSTStatus.Text, 1) <> "E" And (Val(txtCGSTRefundAmt.Text) + Val(txtSGSTRefundAmt.Text) + Val(txtIGSTRefundAmt.Text)) = 0 Then
                MsgBox("GST Amount Cann't be Zero.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If
        '    If chkModvat.Value = vbChecked And Val(txtsheCESSAmount.Text) = 0 Then
        '        If MsgQuestion("S. H. E. Cess Amount is Zero. You Want to Continue ...") = vbNo Then
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If
        mWithInState = "Y"
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mWithInState = IIf(IsDbNull(MasterNo), "Y", MasterNo)
        End If
        '
        '    If Trim(txtItemType.Text) = "" Then
        '        MsgBox "Item Type is Blank", vbInformation
        '        FieldsVarification = False
        '        txtItemType.SetFocus
        '        Exit Function
        '    End If
        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False: Exit Function
        If Trim(mPartyGSTNo) <> Trim(mCompanyGSTNo) Then
            If VB.Left(cboGSTStatus.Text, 1) = "G" Then
                If Val(txtCGSTRefundAmt.Text) + Val(txtSGSTRefundAmt.Text) + Val(txtIGSTRefundAmt.Text) <> Val(lblTotCGST.Text) + Val(lblTotSGST.Text) + Val(lblTotIGST.Text) Then
                    MsgInformation("GST Refund Amount And GST Amount Not Match. You cann't be Save")
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If
        mSTTaxcount = 0

        If Val(txtTDSRate.Text) > 100 Then
            MsgBox("TDS RATE Cann't be greater than 100.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        If Val(txtESIRate.Text) > 100 Then
            MsgBox("ESI RATE Cann't be greater than 100.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        If Val(txtSTDSRate.Text) > 100 Then
            MsgBox("STDS RATE Cann't be greater than 100.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmSupp_PurchaseGST_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim SqlStr As String
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If VB.Left(lblBookType.Text, 1) = "U" Then
            Me.Text = "Purchase Supplementary Invoice"
        Else
            Me.Text = "Supplier Credit Note (Rate Diff)"
        End If
        SqlStr = ""
        SqlStr = "Select * from FIN_SUPP_PURCHASE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchMain, ADODB.LockTypeEnum.adLockReadOnly)
        SqlStr = ""
        SqlStr = "Select * from FIN_SUPP_PURCHASE_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchDetail, ADODB.LockTypeEnum.adLockReadOnly)
        SqlStr = ""
        SqlStr = "Select * from FIN_SUPP_PURCHASE_EXP Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchExp, ADODB.LockTypeEnum.adLockReadOnly)
        Call AssignGrid(False)
        Call SetTextLengths()
        mSupplierCode = CStr(-1)
        cboInvType.Enabled = True
        mBookType = VB.Left(lblBookType.Text, 1)
        '    mBookSubType = Right(lblBookType.text, 1)
        FillCboSaleType()
        cboGSTStatus.Items.Clear()
        cboGSTStatus.Items.Add("GST Input")
        cboGSTStatus.Items.Add("Reverse Charge")
        cboGSTStatus.Items.Add("Exempt")
        cboGSTStatus.SelectedIndex = -1
        cboGoodService.Items.Clear()
        cboGoodService.Items.Add("Goods")
        cboGoodService.Items.Add("Service")
        cboGoodService.SelectedIndex = -1
        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
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
        Dim SqlStr As String
        SqlStr = ""
        MainClass.ClearGrid(SprdView)
        SqlStr = "SELECT FIN_INVTYPE_MST.NAME AS INVOICE_TYPE, " & vbCrLf & " TO_CHAR(VNOSEQ), VNOPREFIX," & vbCrLf & " VNO,VDATE, "
        SqlStr = SqlStr & vbCrLf & " BILLNO, INVOICE_DATE  AS BILLDATE, " & vbCrLf & " AUTO_KEY_PO AS PONO, PO_DATE, " & vbCrLf & " A.SUPP_CUST_NAME AS SUPPLIER, B.SUPP_CUST_NAME AS CREDIT_ACCOUNT, " & vbCrLf & " ITEMDESC, TARIFFHEADING AS TARIFF,ITEMVALUE,"
        SqlStr = SqlStr & vbCrLf & "TOTEDAMOUNT AS EDAMT,TOTEDUAMOUNT AS CESS_AMT,NETVALUE,DECODE(ISCAPITAL,'Y','YES','NO') AS ISCAPITAL ,DECODE(ISFINALPOST,'Y','YES','NO') AS  ISFINALPOST"
        SqlStr = SqlStr & vbCrLf & " FROM " & vbCrLf & " FIN_SUPP_PURCHASE_HDR, FIN_INVTYPE_MST, FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_MST B " & vbCrLf & " WHERE FIN_SUPP_PURCHASE_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " And FIN_SUPP_PURCHASE_HDR.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND FIN_SUPP_PURCHASE_HDR.COMPANY_CODE=FIN_INVTYPE_MST.COMPANY_CODE(+) " & vbCrLf & " AND FIN_SUPP_PURCHASE_HDR.TRNTYPE=FIN_INVTYPE_MST.CODE " & vbCrLf & " AND FIN_SUPP_PURCHASE_HDR.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf & " AND FIN_SUPP_PURCHASE_HDR.SUPP_CUST_CODE=A.SUPP_CUST_CODE " & vbCrLf & " AND FIN_SUPP_PURCHASE_HDR.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND FIN_SUPP_PURCHASE_HDR.ACCOUNTCODE=B.SUPP_CUST_CODE " & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'"
        '    SqlStr = SqlStr & vbCrLf & " AND ISFINALPOST='Y' "
        SqlStr = SqlStr & vbCrLf & " Order by FIN_SUPP_PURCHASE_HDR.VDATE, FIN_SUPP_PURCHASE_HDR.VNO"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume
    End Sub
    Private Sub FormatSprdView()
        Dim cntCol As Integer
        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 600)
            .set_ColWidth(1, 0)
            .set_ColWidth(2, 0)
            .set_ColWidth(3, 1200)
            .set_ColWidth(4, 1200)
            .set_ColWidth(5, 1200)
            .set_ColWidth(6, 1300)
            .set_ColWidth(7, 1200)
            .set_ColWidth(8, 1300)
            .set_ColWidth(9, 1200)
            .set_ColWidth(10, 1200)
            .set_ColWidth(11, 2000)
            .set_ColWidth(12, 2000)
            .set_ColWidth(13, 1200)
            .set_ColWidth(14, 1200)
            .set_ColWidth(15, 1200)
            .set_ColWidth(16, 1200)
            .set_ColWidth(17, 1200)
            .set_ColWidth(18, 1200)
            .set_ColWidth(19, 800)
            .set_ColWidth(20, 800)
            For cntCol = 17 To 20
                .Col = cntCol
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            Next
            .ColsFrozen = 8
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            SprdView.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdExp(ByRef Arow As Integer)
        On Error GoTo ERR1
        With SprdExp
            .Row = Arow
            .set_RowHeight(Arow, 10)

            .Col = ColExpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColExpName, 15)

            .Col = ColExpPercent
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = 0.0#
            .TypeFloatMax = 99.999
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeEditMultiLine = False
            .set_ColWidth(ColExpName, 8)

            .Col = ColExpAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(ColExpAmt, 10)
            .TypeEditMultiLine = False

            .Col = ColExpSTCode
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMin = CDbl("-9999999999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = True
            .Col = ColExpAddDeduct 'ExpFlag (For Add or Deduct) Hidden Column
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .ColHidden = True
            .Col = ColExpIdent
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .ColHidden = True
            .Col = ColTaxable
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .ColHidden = True
            .Col = ColExciseable
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .ColHidden = True
            SprdExp.Col = ColExpCalcOn
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeEditMultiLine = False
            .ColHidden = True
            .Col = ColExpDebitAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(ColExpDebitAmt, 8)
            .TypeEditMultiLine = False
            .Col = ColRO
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColRO, 2)
            '.Value = vbUnchecked
            MainClass.UnProtectCell(SprdExp, 1, .MaxRows, 1, ColExpDebitAmt)
            If ADDMode = True Then
                '            MainClass.UnProtectCell SprdExp, 1, .MaxRows, ColExpDebitAmt, ColExpDebitAmt
            Else
                MainClass.ProtectCell(SprdExp, 1, .MaxRows, ColExpDebitAmt, ColExpDebitAmt)
            End If
            MainClass.ProtectCell(SprdExp, 1, .MaxRows, ColExpName, ColExpName)
        End With
        MainClass.SetSpreadColor(SprdExp, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)
        On Error GoTo ERR1
        Dim I As Integer
        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsSuppPurchDetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColItemCode, 8)
            .ColsFrozen = ColItemCode

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsSuppPurchDetail.Fields("CUSTOMER_PART_NO").DefinedSize
            .ColHidden = True

            .ColsFrozen = ColItemDesc
            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsSuppPurchDetail.Fields("Item_Desc").DefinedSize ''
            .set_ColWidth(ColItemDesc, 20)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsSuppPurchDetail.Fields("Item_Desc").DefinedSize ''
            .set_ColWidth(ColUnit, 4)

            .Col = ColPURFYear
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .TypeEditLen = RsSuppPurchDetail.Fields("PUR_FYEAR").DefinedSize ''
            .set_ColWidth(ColPURFYear, 8)
            .ColHidden = True

            .Col = ColPURMkey
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .TypeEditLen = RsSuppPurchDetail.Fields("PUR_MKEY").DefinedSize ''
            .set_ColWidth(ColPURMkey, 8)
            .ColHidden = True

            For I = ColPONo To ColPODate
                .Col = I
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditMultiLine = True
                .set_ColWidth(I, 9)
                .ColHidden = False
            Next

            For I = ColVNo To ColBillDate
                .Col = I
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditMultiLine = True
                .set_ColWidth(I, 9)
                .ColHidden = IIf(I = ColBillNo, False, IIf(I = ColVNo, False, True))
            Next

            For I = ColMRRNo To ColMRRDate
                .Col = I
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditMultiLine = True
                .set_ColWidth(I, 9)
                .ColHidden = True
            Next
            For I = ColBillQty To ColAmount
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMin = CDbl("-99999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(I, 9)
                '            .ColHidden = True
            Next
            .Col = ColCGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCGSTPer, 6)

            .Col = ColSGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColSGSTPer, 6)

            .Col = ColIGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColIGSTPer, 6)

            .Col = ColCGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCGSTAmount, 9)

            .Col = ColSGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColSGSTAmount, 9)
            .Col = ColIGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColIGSTAmount, 9)
            '        .Col = ColShowPO
            '        .CellType = SS_CELL_TYPE_BUTTON
            '        '.Lock = False
            '        .TypeButtonText = "Show"
            '        .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            '        .ColWidth(ColShowPO) = 5
        End With
        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColAmount)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColPORate)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmount, ColIGSTAmount)
        '    SprdMain.EditMode = False
        '    SprdMain.EditModePermanent = True
        SprdMain.EditModeReplace = True
        SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        '    SprdMain.GridColor = &HC00000
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsSuppPurchDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsSuppPurchMain
            txtVNoPrefix.Maxlength = .Fields("VNoPrefix").DefinedSize
            txtVno.Maxlength = .Fields("Vno").DefinedSize
            TxtVDate.Maxlength = 10
            txtBillNo.Maxlength = .Fields("BillNo").Precision
            txtBillDate.Maxlength = 10
            txtPONo.Maxlength = .Fields("AUTO_KEY_PO").DefinedSize
            txtPODate.Maxlength = 10
            txtAmendNo.Maxlength = .Fields("AMEND_NO").DefinedSize
            txtWEFDate.Maxlength = 10
            txtSupplier.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtDebitAccount.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            '        txtJVVNO.MaxLength = .Fields("JVNO").DefinedSize
            '        txtModvatPer.MaxLength = .Fields("MODVATPER").Precision
            '        txtCESSPer.MaxLength = .Fields("CESSPER").Precision
            '        txtSHECessPer.MaxLength = .Fields("SHECMODVATPER").Precision
            '        txtSTClaimPer.MaxLength = .Fields("STCLAIMPER").Precision
            '        txtModvatAmount
            '        txtServiceAmtModvat.MaxLength = .Fields("SERVCLAIMPERCENT").Precision
            '        txtCESSAmount
            '        txtStClaimAmount
            txtTariff.Maxlength = .Fields("TARIFFHEADING").DefinedSize
            txtItemType.Maxlength = .Fields("ItemDesc").DefinedSize
            txtRemarks.Maxlength = .Fields("Remarks").DefinedSize
            txtNarration.Maxlength = .Fields("NARRATION").DefinedSize
            txtPaymentDate.Maxlength = 10
            '        txtFormRecvName.MaxLength = .Fields("STFORMNAME").DefinedSize           ''
            '        txtFormRecvNo.MaxLength = .Fields("STFORMNO").DefinedSize           ''
            '        txtFormRecvDate.MaxLength = 10
            '        txtFormDueName.MaxLength = .Fields("STDUEFORMNAME").DefinedSize           ''
            '        txtFormDueNo.MaxLength = .Fields("STDUEFORMNO").DefinedSize           ''
            '        txtFormDueDate.MaxLength = 10
            '        txtModvatNo.MaxLength = .Fields("MODVATNO").DefinedSize
            '        txtStClaimNo.MaxLength = .Fields("STCLAIMNO").DefinedSize
            txtGSTNo.Maxlength = .Fields("GST_CLAIM_NO").DefinedSize
            '        txtStClaimNo.MaxLength = .Fields("STCLAIMNO").DefinedSize
            txtTDSRate.MaxLength = .Fields("TDSPer").Precision ''
            txtTDSAmount.MaxLength = .Fields("TDSAMOUNT").Precision ''
            txtESIRate.MaxLength = .Fields("ESIPER").Precision ''
            txtESIAmount.MaxLength = .Fields("ESIAMOUNT").Precision ''
            txtSTDSRate.MaxLength = .Fields("STDSPER").Precision ''
            txtSTDSAmount.MaxLength = .Fields("STDSAMOUNT").Precision ''
            txtJVVNO.MaxLength = .Fields("JVNO").DefinedSize ''
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub Show1()
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset
        Dim mCustRefNo As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mGSTStatus As String
        Dim mGoodsService As String
        Dim pSectionCode As Long
        Clear1()
        With RsSuppPurchMain
            If Not .EOF Then
                LblMKey.Text = .Fields("MKey").Value
                txtVNoPrefix.Text = IIf(IsDbNull(.Fields("VNOPREFIX").Value), "", .Fields("VNOPREFIX").Value)
                txtVno.Text = VB6.Format(IIf(IsDbNull(.Fields("VNOSEQ").Value), "", .Fields("VNOSEQ").Value), "00000")
                TxtVDate.Text = VB6.Format(IIf(IsDbNull(.Fields("VDate").Value), "", .Fields("VDate").Value), "DD/MM/YYYY")
                txtBillNo.Text = IIf(IsDbNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDbNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                txtPONo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_PO").Value), "", .Fields("AUTO_KEY_PO").Value)
                txtPODate.Text = IIf(IsDbNull(.Fields("PO_DATE").Value), "", .Fields("PO_DATE").Value)
                txtAmendNo.Text = IIf(IsDBNull(.Fields("AMEND_NO").Value), "", .Fields("AMEND_NO").Value)

                txtLocationID.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)
                txtWEFDate.Text = IIf(IsDbNull(.Fields("PO_WEFDATE").Value), "", .Fields("PO_WEFDATE").Value)
                txtToDate.Text = VB6.Format(IIf(IsDbNull(.Fields("TO_DATE").Value), "", .Fields("TO_DATE").Value), "DD/MM/YYYY")
                If MainClass.ValidateWithMasterTable((.Fields("TRNTYPE").Value), "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    cboInvType.Text = MasterNo
                End If
                If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                    mBookSubType = MasterNo
                Else
                    mBookSubType = CStr(-1)
                End If
                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSupplier.Text = MasterNo
                End If
                If MainClass.ValidateWithMasterTable((.Fields("ACCOUNTCODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtDebitAccount.Text = MasterNo
                End If
                '            If .Fields("JVNO").Value = "-1" Then
                '                txtJVVNO.Text = ""
                '                lblJVMkey.text = ""
                '            Else
                '                txtJVVNO.Text = IIf(IsNull(.Fields("JVNO").Value), "", .Fields("JVNO").Value)
                '                lblJVMkey.text = IIf(IsNull(.Fields("JVMKEY").Value), "", .Fields("JVMKEY").Value)
                '            End If
                chkCancelled.CheckState = IIf(.Fields("CANCELLED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkCancelled.Enabled = IIf(.Fields("CANCELLED").Value = "Y", False, True)
                '
                '            txtModvatPer.Text = IIf(IsNull(.Fields("MODVATPER").Value), "", .Fields("MODVATPER").Value)
                ''            txtServicePerModvat.Text = IIf(IsNull(.Fields("SERVCLAIMPERCENT").Value), "", .Fields("SERVCLAIMPERCENT").Value)
                '
                '            txtCESSPer.Text = IIf(IsNull(.Fields("CESSPER").Value), "", .Fields("CESSPER").Value)
                '            txtSHECessPer.Text = IIf(IsNull(.Fields("SHECMODVATPER").Value), "", .Fields("SHECMODVATPER").Value)
                '
                '            txtSTClaimPer.Text = IIf(IsNull(.Fields("STCLAIMPER").Value), "", .Fields("STCLAIMPER").Value)
                '            txtModvatAmount.Text = Format(IIf(IsNull(.Fields("MODVATAMOUNT").Value), "", .Fields("MODVATAMOUNT").Value), "0.00")
                ''            txtServiceAmtModvat.Text = Format(IIf(IsNull(.Fields("SERVICECLAIMAMOUNT").Value), "", .Fields("SERVICECLAIMAMOUNT").Value), "0.00")
                '            txtCESSAmount.Text = Format(IIf(IsNull(.Fields("CESSAMOUNT").Value), "", .Fields("CESSAMOUNT").Value), "0.00")
                '            txtSHECESSAmount.Text = Format(IIf(IsNull(.Fields("SHECMODVATAMOUNT").Value), "", .Fields("SHECMODVATAMOUNT").Value), "0.00")
                '            txtStClaimAmount.Text = Format(IIf(IsNull(.Fields("STCLAIMAMOUNT").Value), "", .Fields("STCLAIMAMOUNT").Value), "0.00")
                '
                '            txtSurchargeOnSTAmount.Text = Format(IIf(IsNull(.Fields("SUR_VATCLAIMAMOUNT").Value), "", .Fields("SUR_VATCLAIMAMOUNT").Value), "0.00")
                txtTariff.Text = IIf(IsDbNull(.Fields("TARIFFHEADING").Value), "", .Fields("TARIFFHEADING").Value)
                txtItemType.Text = IIf(IsDbNull(.Fields("ITEMDESC").Value), "", .Fields("ITEMDESC").Value)
                txtRemarks.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                txtNarration.Text = IIf(IsDbNull(.Fields("NARRATION").Value), "", .Fields("NARRATION").Value)
                txtPaymentDate.Text = IIf(IsDbNull(.Fields("PAYMENTDATE").Value), "", .Fields("PAYMENTDATE").Value)
                '            optSTType(0).Value = IIf(!STTYPE = 0, True, False)
                '            optSTType(1).Value = IIf(!STTYPE = 1, True, False)
                '            optSTType(2).Value = IIf(!STTYPE = 2, True, False)
                '            chkRegDealer.Value = IIf(.Fields("ISREGDNO").Value = "Y", vbChecked, vbUnchecked)
                '            If Not IsNull(.Fields("STFORMCODE").Value) Then
                '                If MainClass.ValidateWithMasterTable(.Fields("STFORMCODE").Value, "CODE", "NAME", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                    txtFormRecvName.Text = MasterNo
                '                Else
                '                    txtFormRecvName.Text = ""
                '                End If
                '            End If
                '            txtFormRecvNo.Text = IIf(IsNull(.Fields("STFORMNO").Value), "", .Fields("STFORMNO").Value)
                '            txtFormRecvDate.Text = Format(IIf(IsNull(.Fields("STFORMDATE").Value), "", .Fields("STFORMDATE").Value), "DD/MM/YYYY")
                '            If Not IsNull(.Fields("STDUEFORMCODE").Value) Then
                '                If MainClass.ValidateWithMasterTable(.Fields("STDUEFORMCODE").Value, "CODE", "NAME", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                    txtFormDueName.Text = MasterNo
                '                Else
                '                    txtFormDueName.Text = ""
                '                End If
                '            End If
                '            txtFormDueNo.Text = IIf(IsNull(.Fields("STDUEFORMNO").Value), "", .Fields("STDUEFORMNO").Value)
                '            txtFormDueDate.Text = Format(IIf(IsNull(.Fields("STDUEFORMDATE").Value), "", .Fields("STDUEFORMDATE").Value), "DD/MM/YYYY")
                '            chkGST.Value = IIf(.Fields("ISGSTAPPLICABLE").Value = "Y", vbChecked, vbUnchecked)
                mGSTStatus = IIf(IsDbNull(.Fields("ISGSTAPPLICABLE").Value), "", .Fields("ISGSTAPPLICABLE").Value)
                If mGSTStatus = "G" Then
                    cboGSTStatus.SelectedIndex = 0
                ElseIf mGSTStatus = "R" Then
                    cboGSTStatus.SelectedIndex = 1
                Else
                    cboGSTStatus.SelectedIndex = 2
                End If
                cboGSTStatus.Enabled = False
                mGoodsService = IIf(IsDbNull(.Fields("GOODS_SERVICE").Value), "G", .Fields("GOODS_SERVICE").Value)
                If mGoodsService = "G" Then
                    cboGoodService.SelectedIndex = 0
                Else
                    cboGoodService.SelectedIndex = 1
                End If
                cboGoodService.Enabled = False
                Dim mValue As String
                mValue = IIf(IsDBNull(.Fields("ISCAPITAL").Value), "N", .Fields("ISCAPITAL").Value)
                ChkCapital.CheckState = IIf(mValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                mValue = IIf(IsDBNull(.Fields("ISFINALPOST").Value), "N", .Fields("ISFINALPOST").Value)
                chkFinalPost.Enabled = IIf(mValue = "Y", False, True)
                chkFinalPost.CheckState = IIf(mValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                txtGSTDate.Text = VB6.Format(IIf(IsDbNull(.Fields("GST_CLAIM_DATE").Value), "", .Fields("GST_CLAIM_DATE").Value), "DD/MM/YYYY")
                txtGSTNo.Text = IIf(IsDbNull(.Fields("GST_CLAIM_NO").Value), "", .Fields("GST_CLAIM_NO").Value)
                chkGSTClaim.CheckState = IIf(.Fields("GST_CLAIM").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                lblClaimStatus.Text = IIf(IsDbNull(.Fields("GST_CLAIM").Value), "N", .Fields("GST_CLAIM").Value)
                lblTotQty.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTQTY").Value), 0, .Fields("TOTQTY").Value), "0.00")
                lblTotItemValue.Text = VB6.Format(IIf(IsDbNull(.Fields("ITEMVALUE").Value), 0, .Fields("ITEMVALUE").Value), "0.00")
                lblNetAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("NETVALUE").Value), 0, .Fields("NETVALUE").Value), "0.00")
                txtCGSTRefundAmt.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTCGST_REFUNDAMT").Value), 0, .Fields("TOTCGST_REFUNDAMT").Value), "0.00")
                txtSGSTRefundAmt.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTSGST_REFUNDAMT").Value), 0, .Fields("TOTSGST_REFUNDAMT").Value), "0.00")
                txtIGSTRefundAmt.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTIGST_REFUNDAMT").Value), 0, .Fields("TOTIGST_REFUNDAMT").Value), "0.00")
                mAddUser = IIf(IsDbNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                mAddDate = VB6.Format(IIf(IsDbNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                mModUser = IIf(IsDbNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                mModDate = VB6.Format(IIf(IsDbNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")
                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)
                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If
                cboDivision.Enabled = False

                chkTDS.CheckState = IIf(.Fields("ISTDSDEDUCT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkTDS.Enabled = IIf(.Fields("ISTDSDEDUCT").Value = "Y", False, True)
                txtTDSRate.Text = VB6.Format(IIf(IsDBNull(.Fields("TDSPer").Value), "", .Fields("TDSPer").Value), "0.000")
                txtTDSAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("TDSAMOUNT").Value), "", .Fields("TDSAMOUNT").Value), "0.00")
                chkESI.CheckState = IIf(.Fields("ISESIDEDUCT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkESI.Enabled = IIf(.Fields("ISESIDEDUCT").Value = "Y", False, True)
                txtESIRate.Text = VB6.Format(IIf(IsDBNull(.Fields("ESIPer").Value), "", .Fields("ESIPer").Value), "0.000")
                txtESIAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("ESIAMOUNT").Value), "", .Fields("ESIAMOUNT").Value), "0.00")
                ChkSTDS.CheckState = IIf(.Fields("ISSTDSDEDUCT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                ChkSTDS.Enabled = IIf(.Fields("ISSTDSDEDUCT").Value = "Y", False, True)
                txtSTDSRate.Text = VB6.Format(IIf(IsDBNull(.Fields("STDSPer").Value), "", .Fields("STDSPer").Value), "0.000")
                txtSTDSAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("STDSAMOUNT").Value), "", .Fields("STDSAMOUNT").Value), "0.00")
                txtTDSDeductOn.Text = VB6.Format(IIf(IsDBNull(.Fields("TDS_DEDUCT_ON").Value), "", .Fields("TDS_DEDUCT_ON").Value), "0.00")
                txtSTDSDeductOn.Text = VB6.Format(IIf(IsDBNull(.Fields("STDS_DEDUCT_ON").Value), "", .Fields("STDS_DEDUCT_ON").Value), "0.00")
                txtESIDeductOn.Text = VB6.Format(IIf(IsDBNull(.Fields("ESI_DEDUCT_ON").Value), "", .Fields("ESI_DEDUCT_ON").Value), "0.00")
                txtJVVNO.Text = IIf(IsDBNull(.Fields("JVNO").Value), "", .Fields("JVNO").Value)
                lblJVTMKey.Text = IIf(IsDBNull(.Fields("JVT_MKEY").Value), "", .Fields("JVT_MKEY").Value)

                If lblJVTMKey.Text <> "" Then
                    If MainClass.ValidateWithMasterTable((lblJVTMKey.Text), "MKEY", "VNO", "FIN_VOUCHER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtJVVNO.Text = Trim(MasterNo)
                    End If
                End If

                pSectionCode = IIf(IsDBNull(.Fields("SECTION_CODE").Value), -1, .Fields("SECTION_CODE").Value)

                If pSectionCode > 0 Then
                    If MainClass.ValidateWithMasterTable(pSectionCode, "CODE", "NAME", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtSection.Text = MasterNo
                    End If
                End If

                Call ShowDetail1((LblMKey.Text), mCustRefNo)
                Call ShowExp1((LblMKey.Text))
                Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))
                chkCapital.Enabled = False
                '            chkSTRefund.Enabled = False
                ''Call CalcTots
            End If
        End With
        txtSupplier.Enabled = False
        txtVNo.Enabled = True
        txtGSTNo.Enabled = False
        '    txtStClaimNo.Enabled = False
        cmdShowPO.Enabled = False
        txtPONo.Enabled = False
        txtPODate.Enabled = False
        cmdSearchPO.Enabled = False
        txtToDate.Enabled = False
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsSuppPurchMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        FormatSprdMain(-1)
        SprdMain.Enabled = True
        SprdExp.Enabled = True
        cboInvType.Enabled = IIf(CDbl(PubUserLevel) = 1 Or CDbl(PubUserLevel) = 2, True, False)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub ShowExp1(ByRef mMkey As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim pExpId As String
        Dim SqlStr As String
        Call FillSprdExp()
        SqlStr = ""
        SqlStr = "Select FIN_SUPP_PURCHASE_EXP.EXPCODE,FIN_SUPP_PURCHASE_EXP.EXPPERCENT, " & vbCrLf & " FIN_SUPP_PURCHASE_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO,DebitAmount " & vbCrLf & " From FIN_SUPP_PURCHASE_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_SUPP_PURCHASE_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND FIN_SUPP_PURCHASE_EXP.Mkey='" & mMkey & "'"
        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If
        SqlStr = SqlStr & vbCrLf & " ORDER BY SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchExp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsSuppPurchExp.EOF = False Then
            RsSuppPurchExp.MoveFirst()
            With SprdExp
                Do While Not RsSuppPurchExp.EOF
                    For I = 1 To .MaxRows
                        .Row = I
                        '
                        '                    .Col = ColExpIdent
                        '                    pExpId = Trim(.Text)
                        '
                        '                    If pExpId = "ST" Then
                        '
                        '                    End If
                        .Col = ColExpName
                        If .Text = RsSuppPurchExp.Fields("Name").Value Then Exit For
                    Next I
                    .Col = ColExpPercent 'Exp. %
                    .Text = CStr(Val(IIf(IsDbNull(RsSuppPurchExp.Fields("ExpPercent").Value), "", RsSuppPurchExp.Fields("ExpPercent").Value)))
                    .Col = ColExpAmt
                    If RsSuppPurchExp.Fields("Identification").Value = "RO" Then '30.10.2001   ''Allow '-' if exp. is ropund off
                        .Text = CStr(Val(IIf(IsDbNull(RsSuppPurchExp.Fields("Amount").Value), "", RsSuppPurchExp.Fields("Amount").Value)))
                    Else
                        .Text = CStr(System.Math.Abs(Val(IIf(IsDbNull(RsSuppPurchExp.Fields("Amount").Value), "", RsSuppPurchExp.Fields("Amount").Value))))
                    End If
                    .Col = ColExpSTCode
                    .Text = CStr(Val(IIf(IsDbNull(RsSuppPurchExp.Fields("CODE").Value), 0, RsSuppPurchExp.Fields("CODE").Value)))
                    .Col = ColExpAddDeduct 'ExpFlag
                    .Text = IIf(RsSuppPurchExp.Fields("Add_Ded").Value = "A", "A", "D")
                    .Col = ColExpIdent
                    .Text = IIf(IsDbNull(RsSuppPurchExp.Fields("Identification").Value), "", RsSuppPurchExp.Fields("Identification").Value)
                    If .Text = "RO" Then 'round off
                        .Col = ColExpAmt
                        pRound = Val(.Text)
                    End If
                    SprdExp.Col = ColTaxable
                    SprdExp.Text = IIf(IsDbNull(RsSuppPurchExp.Fields("Taxable").Value), "N", RsSuppPurchExp.Fields("Taxable").Value)
                    SprdExp.Col = ColExciseable
                    SprdExp.Text = IIf(IsDbNull(RsSuppPurchExp.Fields("Exciseable").Value), "N", RsSuppPurchExp.Fields("Exciseable").Value)
                    SprdExp.Col = ColExpCalcOn
                    SprdExp.Text = CStr(Val(IIf(IsDbNull(RsSuppPurchExp.Fields("CalcOn").Value), "", RsSuppPurchExp.Fields("CalcOn").Value)))
                    .Col = ColExpDebitAmt
                    .Text = CStr(Val(IIf(IsDbNull(RsSuppPurchExp.Fields("DebitAmount").Value), "", RsSuppPurchExp.Fields("DebitAmount").Value)))
                    .Col = ColRO
                    .Value = IIf(RsSuppPurchExp.Fields("RO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                    RsSuppPurchExp.MoveNext()
                Loop
            End With
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowDetail1(ByRef mMkey As String, ByRef mCustRefType As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim SqlStr As String
        Dim mAccountPostCode As String
        Dim mAccountPostName As String
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM FIN_SUPP_PURCHASE_DET " & vbCrLf & " Where Mkey='" & mMkey & "'" & vbCrLf & " Order By SubRowNo"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSuppPurchDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = I
                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = mItemCode
                SprdMain.Col = ColItemDesc
                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemDesc = MasterNo
                Else
                    mItemDesc = ""
                End If
                SprdMain.Text = mItemDesc
                SprdMain.Col = ColPartNo
                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "CUSTOMER_PART_NO", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mPartNo = MasterNo
                Else
                    mPartNo = ""
                End If
                SprdMain.Text = mPartNo
                SprdMain.Col = ColHSNCode
                SprdMain.Text = IIf(IsDbNull(.Fields("HSNCODE").Value), "", .Fields("HSNCODE").Value) 'GetHSNCode(mItemCode) ''
                SprdMain.Col = ColPONo
                SprdMain.Text = Trim(Str(IIf(IsDbNull(.Fields("PONO").Value), "", .Fields("PONO").Value)))
                SprdMain.Col = ColPODate
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("PODATE").Value), "", .Fields("PODATE").Value), "DD/MM/YYYY")
                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                SprdMain.Col = ColPURFYear
                SprdMain.Text = IIf(IsDbNull(.Fields("PUR_FYEAR").Value), "", .Fields("PUR_FYEAR").Value)
                SprdMain.Col = ColPURMkey
                SprdMain.Text = CStr(IIf(IsDbNull(.Fields("PUR_MKEY").Value), "", .Fields("PUR_MKEY").Value))
                SprdMain.Col = ColVNo
                SprdMain.Text = IIf(IsDbNull(.Fields("PURNO").Value), "", .Fields("PURNO").Value)
                SprdMain.Col = ColVDate
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("PUR_DATE").Value), "", .Fields("PUR_DATE").Value), "DD/MM/YYYY")
                SprdMain.Col = ColBillNo
                SprdMain.Text = IIf(IsDbNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                SprdMain.Col = ColBillDate
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("BILLDATE").Value), "", .Fields("BILLDATE").Value), "DD/MM/YYYY")
                SprdMain.Col = ColMRRNo
                SprdMain.Text = Str(IIf(IsDbNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value))
                SprdMain.Col = ColMRRDate
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("MRRDATE").Value), "", .Fields("MRRDATE").Value), "DD/MM/YYYY")
                SprdMain.Col = ColBillQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("BILL_QTY").Value), 0, .Fields("BILL_QTY").Value)))
                SprdMain.Col = ColBillRate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("BILL_RATE").Value), 0, .Fields("BILL_RATE").Value)))
                SprdMain.Col = ColPORate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("PO_RATE").Value), 0, .Fields("PO_RATE").Value)))
                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("QTY").Value), 0, .Fields("QTY").Value)))
                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("RATE").Value), 0, .Fields("RATE").Value)))
                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("AMOUNT").Value), 0, .Fields("AMOUNT").Value)))
                SprdMain.Col = ColCGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("CGST_PER").Value), 0, .Fields("CGST_PER").Value)))
                SprdMain.Col = ColSGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("SGST_PER").Value), 0, .Fields("SGST_PER").Value)))
                SprdMain.Col = ColIGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)))
                SprdMain.Col = ColCGSTAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("CGST_AMOUNT").Value), 0, .Fields("CGST_AMOUNT").Value)))
                SprdMain.Col = ColSGSTAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("SGST_AMOUNT").Value), 0, .Fields("SGST_AMOUNT").Value)))
                SprdMain.Col = ColIGSTAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("IGST_AMOUNT").Value), 0, .Fields("IGST_AMOUNT").Value)))
                SprdMain.Col = ColInvType
                mAccountPostCode = IIf(IsDbNull(.Fields("PUR_ACCOUNT_CODE").Value), "", .Fields("PUR_ACCOUNT_CODE").Value)
                If MainClass.ValidateWithMasterTable(mAccountPostCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountPostName = MasterNo
                Else
                    mAccountPostName = ""
                End If
                SprdMain.Text = mAccountPostName
                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Sub ViewGrid()
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            AssignGrid((True))
            '        AdoDCMain.Refresh
            FormatSprdView()
            SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsSuppPurchMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1
        ''Dim RsMisc As ADODB.Recordset
        Dim mAmount As Double
        Dim mQty As Double
        Dim mRate As Double
        Dim mItemAmount As Double
        Dim mTotQty As Double
        Dim j As Integer
        Dim I As Integer
        Dim mItemCode As String
        Dim xStr As String
        Dim mExpPercent As Double
        Dim mNetAccessAmt As Double
        Dim mShortage As Double
        Dim mTotItemAmount As Double
        Dim pTotExportExp As Double
        Dim pTotOthers As Double
        Dim pTotCustomDuty As Double
        Dim pTotMSC As Double
        Dim pTotDiscount As Double
        Dim pTotRO As Double
        Dim pTotTCS As Double
        Dim mTotExp As Double
        Dim pTCSPer As Double
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mTotCGSTAmount As Double
        Dim mTotSGSTAmount As Double
        Dim mTotIGSTAmount As Double
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim mAccountCode As String

        mCompanyGSTNo = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = Trim(MasterNo)
        End If

        mPartyGSTNo = GetPartyBusinessDetail(mAccountCode, Trim(txtLocationID.Text), "GST_RGN_NO")

        pRound = 0
        mQty = 0
        mRate = 0
        '    mST = 0
        mItemAmount = 0
        mTotItemAmount = 0
        mTotExp = 0
        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                .Col = 0
                If .Text = "Del" Then GoTo DontCalc
                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc
                mItemCode = .Text
                .Col = ColRate
                mRate = Val(.Text)
                .Col = ColQty
                mQty = Val(.Text)
                mTotQty = mTotQty + mQty
                .Col = ColAmount
                mAmount = CDbl(VB6.Format(mRate * mQty, "0.00"))
                .Text = CStr(mAmount)
                .Col = ColCGSTPer
                mCGSTPer = Val(.Text)
                .Col = ColSGSTPer
                mSGSTPer = Val(.Text)
                .Col = ColIGSTPer
                mIGSTPer = Val(.Text)
                mItemAmount = CDbl(VB6.Format(mAmount, "0.00")) '- mDiscount
                mTotItemAmount = mTotItemAmount + mItemAmount
                mCGSTAmount = CDbl(VB6.Format(mItemAmount * mCGSTPer * 0.01, "0.00"))
                mSGSTAmount = CDbl(VB6.Format(mItemAmount * mSGSTPer * 0.01, "0.00"))
                mIGSTAmount = CDbl(VB6.Format(mItemAmount * mIGSTPer * 0.01, "0.00"))
                mTotCGSTAmount = mTotCGSTAmount + mCGSTAmount
                mTotSGSTAmount = mTotSGSTAmount + mSGSTAmount
                mTotIGSTAmount = mTotIGSTAmount + mIGSTAmount
                .Col = ColCGSTAmount
                .Text = VB6.Format(mCGSTAmount, "0.00")
                .Col = ColSGSTAmount
                .Text = VB6.Format(mSGSTAmount, "0.00")
                .Col = ColIGSTAmount
                .Text = VB6.Format(mIGSTAmount, "0.00")
DontCalc:
            Next I
        End With
        '    Call BillExpensesCalcTots(SprdExp, txtBillDate.Text, False, mNetAccessAmt, mExciseableAmount, mTaxableAmount, _
        ''                                mCEDCessAble, mADDCessAble, mCESSableAmount, mTotItemAmount, _
        ''                                pTotExciseDuty, pTotEduCess, pTotSHECess, pTotADE, pTotExportExp, pTotOthers, _
        ''                                pTotSalesTax, pTotSurcharge, pTotCustomDuty, pTotAddCess, pTotCustomDutyExport, pTotCustomDutyCess, _
        ''                                pTotMSC, pTotDiscount, pTotServiceTax, pTotRO, pTotTCS, mTotExp, pEDPer, pSTPer, pServPer, pCessPer, pSHECPer, pTCSPer, "SP", mNetAccessAmt, pTotKKCAmount)
        Call BillExpensesCalcTots_GST(SprdExp, (txtBillDate.Text), mNetAccessAmt, mTotItemAmount, mTotItemAmount, 0, 0, 0, mTotIGSTAmount, mTotSGSTAmount, mTotCGSTAmount, pTotExportExp, 0, 0, pTotOthers, 0, pTotCustomDuty, pTotMSC, pTotDiscount, 0, pTotRO, pTotTCS, mTotExp, pTCSPer, "SP")
        lblTotItemValue.Text = VB6.Format(mTotItemAmount, "#0.00")
        lblTotCGST.Text = VB6.Format(mTotCGSTAmount, "#0.00")
        lblTotSGST.Text = VB6.Format(mTotSGSTAmount, "#0.00")
        lblTotIGST.Text = VB6.Format(mTotIGSTAmount, "#0.00")
        If VB.Left(cboGSTStatus.Text, 1) = "G" Then
            If mCompanyGSTNo = mPartyGSTNo Then
                txtCGSTRefundAmt.Text = "0.00"
                txtSGSTRefundAmt.Text = "0.00"
                txtIGSTRefundAmt.Text = "0.00"
            Else
                txtCGSTRefundAmt.Text = VB6.Format(mTotCGSTAmount, "#0.00")
                txtSGSTRefundAmt.Text = VB6.Format(mTotSGSTAmount, "#0.00")
                txtIGSTRefundAmt.Text = VB6.Format(mTotIGSTAmount, "#0.00")
            End If
        Else
            txtCGSTRefundAmt.Text = "0.00"
            txtSGSTRefundAmt.Text = "0.00"
            txtIGSTRefundAmt.Text = "0.00"
        End If
        lblOthersAmount.Text = VB6.Format(mTotExp, "#0.00")
        If mCompanyGSTNo = mPartyGSTNo Then
            lblNetAmount.Text = VB6.Format(mTotItemAmount + mTotExp, "#0.00")
        Else
            lblNetAmount.Text = VB6.Format(mTotExp + mTotItemAmount + mTotCGSTAmount + mTotSGSTAmount + mTotIGSTAmount, "#0.00")
        End If
        lblTotFreight.Text = VB6.Format(pTotOthers, "#0.00")
        lblTotCharges.Text = CStr(0) ''Format(mRO, "#0.00")
        lblTotExpAmt.Text = VB6.Format(mTotExp, "#0.00")
        lblRO.Text = VB6.Format(pTotRO, "#0.00")
        lblDiscount.Text = VB6.Format(pTotDiscount, "#0.00")
        lblMSC.Text = VB6.Format(pTotMSC, "#0.00")
        lblTotQty.Text = VB6.Format(mTotQty, "#0.00")

        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTDSDeductOn.Text = VB6.Format(IIf(Val(txtTDSDeductOn.Text) = 0, lblTotItemValue.Text, txtTDSDeductOn.Text), "#0.00")
        Else
            txtTDSDeductOn.Text = VB6.Format(lblTotItemValue.Text, "#0.00")
        End If

        If chkESI.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtESIDeductOn.Text = VB6.Format(IIf(Val(txtESIDeductOn.Text) = 0, lblNetAmount.Text, txtESIDeductOn.Text), "#0.00")
        Else
            txtESIDeductOn.Text = VB6.Format(lblNetAmount.Text, "#0.00")
        End If
        If ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSTDSDeductOn.Text = VB6.Format(IIf(Val(txtSTDSDeductOn.Text) = 0, lblNetAmount.Text, txtSTDSDeductOn.Text), "#0.00")
        Else
            txtSTDSDeductOn.Text = VB6.Format(lblNetAmount.Text, "#0.00")
        End If

        'If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    If ChkTDSRO.CheckState = System.Windows.Forms.CheckState.Checked Then
        '        txtTDSAmount.Text = VB6.Format(System.Math.Round(Val(txtTDSRate.Text) * Val(txtTDSDeductOn.Text) / 100, 0), "0.00")
        '        If Val(txtTDSRate.Text) > 0 And Val(txtTDSDeductOn.Text) > 0 And Val(txtTDSAmount.Text) <= 1 Then
        '            txtTDSAmount.Text = 1
        '        End If
        '    Else
        '        txtTDSAmount.Text = VB6.Format(Val(txtTDSRate.Text) * Val(txtTDSDeductOn.Text) / 100, "0.00")
        '    End If
        'Else
        '    txtTDSAmount.Text = "0.00"
        'End If

        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTDSAmount.Text = VB6.Format(Val(txtTDSRate.Text) * Val(txtTDSDeductOn.Text) / 100, "0.00")

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                txtTDSAmount.Text = IIf(Val(txtTDSAmount.Text) > Int(txtTDSAmount.Text), Int(txtTDSAmount.Text) + 1, Val(txtTDSAmount.Text))
            Else
                If ChkTDSRO.CheckState = System.Windows.Forms.CheckState.Checked Then
                    txtTDSAmount.Text = VB6.Format(System.Math.Round(Val(txtTDSAmount.Text), 0), "0.00")
                    If Val(txtTDSRate.Text) > 0 And Val(txtTDSDeductOn.Text) > 0 And Val(txtTDSAmount.Text) <= 1 Then
                        txtTDSAmount.Text = 1
                    End If
                End If
            End If

        Else
            txtTDSAmount.Text = "0.00"
        End If

        If chkESI.CheckState = System.Windows.Forms.CheckState.Checked Then
            If ChkESIRO.CheckState = System.Windows.Forms.CheckState.Checked Then
                txtESIAmount.Text = VB6.Format(System.Math.Round(Val(txtESIRate.Text) * Val(txtESIDeductOn.Text) / 100, 0), "0.00")
            Else
                txtESIAmount.Text = VB6.Format(Val(txtESIRate.Text) * Val(txtESIDeductOn.Text) / 100, "0.00")
            End If
        Else
            txtESIAmount.Text = "0.00"
        End If
        If ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            If ChkSTDSRO.CheckState = System.Windows.Forms.CheckState.Checked Then
                txtSTDSAmount.Text = VB6.Format(System.Math.Round(Val(txtSTDSRate.Text) * Val(txtSTDSDeductOn.Text) / 100, 0), "0.00")
            Else
                txtSTDSAmount.Text = VB6.Format(Val(txtSTDSRate.Text) * Val(txtSTDSDeductOn.Text) / 100, "0.00")
            End If
        Else
            txtSTDSAmount.Text = "0.00"
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Function UpDateSuppBill(ByRef xGSTNo As Integer, ByRef xVnoStr As String, ByRef pISCapital As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        If xGSTNo <> 0 Then
            SqlStr = "UPDATE FIN_PURCHASE_HDR SET ISFINALPOST='Y', " & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND GST_CLAIM_NO=" & xGSTNo & " AND GST_CLAIM_DATE=TO_DATE('" & VB6.Format(txtGSTDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND VNO='-1' AND ISPLA='N'" 'AND ISCAPITAL='" & pISCapital & "' ,JVNO='" & xVnoStr & "',
            PubDBCn.Execute(SqlStr)
        End If
        UpDateSuppBill = True
        Exit Function
ErrPart:
        UpDateSuppBill = False
    End Function
    Private Function CheckExpHead(ByRef mAcctName As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        CheckExpHead = False
        SqlStr = "Select BSGROUP.BSGROUP_ACCTTYPE " & vbCrLf & " FROM FIN_SUPP_CUST_MST,FIN_GROUP_MST ACMGROUP, " & vbCrLf & " FIN_BSGROUP_MST BSGROUP WHERE " & vbCrLf & " FIN_SUPP_CUST_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_SUPP_CUST_MST.COMPANY_CODE=ACMGROUP.COMPANY_CODE" & vbCrLf & " AND BSGROUP.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE" & vbCrLf & " AND FIN_SUPP_CUST_MST.GROUPCODE=GROUP_Code " & vbCrLf & " AND GROUP_BSCodeDr=BSGROUP_Code " & vbCrLf & " AND BSGROUP_ACCTTYPE IN (" & ConIncome & "," & ConExpenses & ")" & vbCrLf & " AND FIN_SUPP_CUST_MST.SUPP_CUST_NAME = '" & MainClass.AllowSingleQuote(Trim(mAcctName)) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
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
    Private Sub Clear1()
        SSTab1.SelectedIndex = 0
        mAddUser = ""
        mAddDate = ""
        mModUser = ""
        mModDate = ""
        LblMKey.Text = ""
        lblPMKey.Text = ""
        mSupplierCode = CStr(-1)
        txtAmendNo.Text = ""
        txtLocationID.Text = ""
        txtVNo.Text = ""
        txtVNoPrefix.Text = mBookType
        If Not IsDate(TxtVDate.Text) Then
            TxtVDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        End If
        txtBillNo.Text = ""
        txtBillDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtSupplier.Text = ""
        txtSupplier.Enabled = True
        txtDebitAccount.Text = ""
        txtPONo.Text = ""
        txtPODate.Text = ""
        txtAmendNo.Text = ""
        txtWEFDate.Text = ""
        txtTariff.Text = ""
        txtItemType.Text = ""
        txtRemarks.Text = ""
        txtNarration.Text = ""
        txtGSTNo.Text = ""
        chkGSTClaim.CheckState = System.Windows.Forms.CheckState.Unchecked
        lblClaimStatus.Text = ""
        txtCGSTRefundAmt.Text = "0.00"
        txtSGSTRefundAmt.Text = "0.00"
        txtIGSTRefundAmt.Text = "0.00"
        txtPaymentDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        '    txtJVVNO.Text = ""
        '    lblJVMkey.text = ""
        chkCancelled.Enabled = True
        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        cboGSTStatus.SelectedIndex = -1
        cboGSTStatus.Enabled = True
        cboGoodService.SelectedIndex = -1
        cboGoodService.Enabled = True
        chkCapital.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkFinalPost.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkFinalPost.Enabled = True
        chkCapital.Enabled = True
        '    optSTType(0).Value = True
        '    optSTType(1).Value = False
        '    optSTType(2).Value = False
        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True
        lblTotQty.Text = "0.00"
        lblTotItemValue.Text = "0.00"
        '    lblTotED.text = "0.00"
        '    lblEDUAmount.text = "0.00"
        '    lblServiceAmount.text = "0.00"
        '    lblTotST.text = "0.00"
        lblNetAmount.Text = "0.00"
        '    lblModvatableAmount.text = "0.00"
        '    lblCESSableAmount.text = "0.00"
        '    lblEDUPercent.text = "0.00"
        '
        '    lblSHEPer.text = Format(0, "#0.00")
        '    lblSHEAmount.text = Format(0, "#0.00")
        lblTotItemValue.Text = VB6.Format(0, "#0.00")
        '    lblTotST.text = Format(0, "#0.00")
        '    lblTotED.text = Format(0, "#0.00")
        '    lblServiceAmount.text = Format(0, "#0.00")
        '    lblEDUAmount.text = Format(0, "#0.00")
        lblNetAmount.Text = VB6.Format(0, "#0.00")
        lblTotFreight.Text = VB6.Format(0, "#0.00")
        lblTotCharges.Text = VB6.Format(0, "#0.00")
        lblTotExpAmt.Text = VB6.Format(0, "#0.00")
        lblTotTaxableAmt.Text = VB6.Format(0, "#0.00")
        lblRO.Text = VB6.Format(0, "#0.00")
        lblDiscount.Text = VB6.Format(0, "#0.00")
        lblSurcharge.Text = VB6.Format(0, "#0.00")
        lblMSC.Text = VB6.Format(0, "#0.00")
        txtGSTNo.Enabled = False
        '    txtStClaimNo.Enabled = False
        txtPONo.Enabled = True
        txtPODate.Enabled = True
        cmdSearchPO.Enabled = True
        txtToDate.Enabled = True
        ''    cboInvType.ListIndex = -1

        chkTDS.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtTDSRate.Text = "0.000"
        txtTDSAmount.Text = "0.00"
        chkTDS.Enabled = True
        chkESI.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtESIRate.Text = "0.000"
        txtESIAmount.Text = "0.00"
        chkESI.Enabled = True
        ChkSTDS.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtSTDSRate.Text = "0.000"
        txtSTDSAmount.Text = "0.00"
        ChkSTDS.Enabled = True
        txtJVVNO.Text = ""
        lblJVTMKey.Text = ""
        txtTDSDeductOn.Text = "0.00"
        txtESIDeductOn.Text = "0.00"
        txtSTDSDeductOn.Text = "0.00"
        txtSection.Text = ""

        ChkTDSRO.CheckState = System.Windows.Forms.CheckState.Checked
        ChkESIRO.CheckState = System.Windows.Forms.CheckState.Checked
        ChkSTDSRO.CheckState = System.Windows.Forms.CheckState.Checked

        pProcessKey = MainClass.AutoGenRowNo("TEMP_DSP_DESPATCH", "RowNo", PubDBCn)
        cmdShowPO.Enabled = True
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ClearGrid(SprdExp)
        FraPostingDtl.Visible = False
        MainClass.ClearGrid(SprdPostingDetail)
        Call FormatSprdPostingDetail(-1)
        Call FillSprdExp()
        MainClass.ButtonStatus(Me, XRIGHT, RsSuppPurchMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub FillSprdExp()
        On Error GoTo ERR1
        Dim mLocal As String
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim I As Integer
        MainClass.ClearGrid(SprdExp)
        If Trim(txtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLocal = IIf(MasterNo = "Y", "L", "C")
            Else
                mLocal = ""
            End If
        Else
            mLocal = ""
        End If
        SqlStr = "Select * From FIN_INTERFACE_MST " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (Type='P' OR Type='B') "
        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If
        SqlStr = SqlStr & vbCrLf & " Order By PrintSequence"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            I = 0
            Do While Not RS.EOF
                I = I + 1
                SprdExp.Row = I
                SprdExp.Col = ColRO
                SprdExp.Value = IIf(RS.Fields("ROUNDOFF").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                SprdExp.Col = ColExpName
                SprdExp.Text = RS.Fields("Name").Value
                SprdExp.Col = ColExpPercent
                If ADDMode = True Then
                    SprdExp.Text = Str(IIf(IsDbNull(RS.Fields("DefaultPercent").Value), 0, Str(RS.Fields("DefaultPercent").Value)))
                Else
                    SprdExp.Text = ""
                End If
                SprdExp.Col = ColExpAmt
                SprdExp.Text = "0"
                SprdExp.Col = ColExpSTCode
                SprdExp.Text = CStr(Val(IIf(IsDbNull(RS.Fields("CODE").Value), -1, RS.Fields("CODE").Value)))
                SprdExp.Col = ColExpAddDeduct
                SprdExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")
                SprdExp.Col = ColExpIdent
                SprdExp.Text = IIf(IsDbNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)
                If SprdExp.Text = "DAM" Then MainClass.ProtectCell(SprdExp, I, I, 1, SprdExp.MaxCols)
                SprdExp.Col = ColTaxable
                SprdExp.Text = IIf(IsDbNull(RS.Fields("Taxable").Value), "N", RS.Fields("Taxable").Value)
                SprdExp.Col = ColExciseable
                SprdExp.Text = IIf(IsDbNull(RS.Fields("Exciseable").Value), "N", RS.Fields("Exciseable").Value)
                If RS.Fields("Identification").Value = "ST" Then
                    If RS.Fields("STTYPE").Value = mLocal Then
                        SprdExp.RowHidden = False
                    Else
                        SprdExp.RowHidden = True
                    End If
                End If
                RS.MoveNext()
                If RS.EOF = False Then
                    SprdExp.MaxRows = SprdExp.MaxRows + 1
                End If
            Loop
        End If
        FormatSprdExp(-1)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub FrmSupp_PurchaseGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmSupp_PurchaseGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub FrmSupp_PurchaseGST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        If InStr(1, XRIGHT, "D", CompareMethod.Text) > 1 Then
            chkCancelled.Enabled = True
        Else
            chkCancelled.Enabled = False
        End If
        txtVNoPrefix.Text = mBookType
        SprdMain.Enabled = True
        SprdExp.Enabled = True
        txtVno.Enabled = True
        txtToDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7755) '8000
        'Me.Width = VB6.TwipsToPixelsX(11355) '11900
        SSTab1.SelectedIndex = 0
        AdoDCMain.Visible = False
        cboDivision.Items.Clear()
        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If
        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        txtSupplier.Enabled = True

        Call FrmSupp_PurchaseGST_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub SprdExp_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdExp.Change
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdExp_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdExp.LeaveCell
        On Error GoTo ErrPart
        Static ESCol As Object
        Static ESRow As Integer
        Static m_Exp As Object
        Static mIDENT As String
        Static m_Amt As Object
        Static m_ExpPercent As Double
        Static m_xp As Object
        Static m_xpn As String
        Static p_DebitAmt As Double
        Static p_Amt As Double
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        ESCol = eventArgs.col
        ESRow = eventArgs.row
        Select Case eventArgs.col
            Case 1 'Exp.Name
                If eventArgs.newRow >= ESRow Or eventArgs.newRow = -1 Then
                    SprdExp.Row = ESRow
                    SprdExp.Col = 1
                    m_Exp = MainClass.AllowSingleQuote(SprdExp.Text)
                    If SprdExp.Text = "" Then Exit Sub
                    If m_Exp <> "" Then Exit Sub
                    SprdExp.Col = ColExpIdent
                    mIDENT = SprdExp.Text
                    SqlStr = "Select * From FIN_INTERFACE_MST Where COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND Name= '" & m_Exp & "'"
                    If PubGSTApplicable = True Then
                        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
                    End If
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
                    If RS.EOF = True Then
                        ESCol = 1
                        GoTo ErrPart
                    Else
                        If mIDENT = "ST" Then
                            SprdExp.Col = 2
                            SprdExp.Text = CStr(0)
                        End If
                        If RS.EOF = False Then
                            SprdExp.Row = ESRow
                            SprdExp.Col = 4
                            SprdExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")
                        End If
                        SprdExp.Col = 1
                        If SprdExp.Text <> "" Then
                            If SprdExp.MaxRows = ESRow Then
                                MainClass.AddBlankSprdRow(SprdExp, ColExpName)
                                FormatSprdExp((SprdExp.MaxRows))
                            End If
                        End If
                    End If
                End If
            Case 2 'Exp. %
                If eventArgs.newRow >= ESRow Or eventArgs.newRow = -1 Then
                    SprdExp.Row = ESRow
                    SprdExp.Col = 1
                    If SprdExp.Text = "" Then Exit Sub
                    '               mExp = SprdExp.Text
                    m_xpn = SprdExp.Text
                    SprdExp.Col = 2
                    SprdExp.Row = ESRow
                    m_ExpPercent = Val(SprdExp.Value)
                    If m_ExpPercent = 0 Then
                        Exit Sub
                    Else
                        SprdExp.Col = ColExpIdent
                        mIDENT = SprdExp.Text
                        If mIDENT = "ST" Or mIDENT = "ED" Or mIDENT = "RO" Then
                            Call CalcTots()
                            Exit Sub
                        End If
                        SprdExp.Row = ESRow
                        SprdExp.Col = 3
                        If MainClass.ValidateWithMasterTable(m_xpn, "Name", "RoundOff", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                            If MasterNo = True Then
                                SprdExp.Text = VB6.Format((CDbl(m_ExpPercent) * CDbl(lblTotItemValue.Text)) / 100, "0")
                            Else
                                SprdExp.Text = VB6.Format((CDbl(m_ExpPercent) * CDbl(lblTotItemValue.Text)) / 100, "0.00")
                            End If
                        End If
                    End If
                Else
                    ESCol = 2
                    ESRow = eventArgs.newRow
                    GoTo ErrPart
                End If
            Case ColExpDebitAmt
                If eventArgs.newRow = -1 Then Exit Sub
                SprdExp.Row = ESRow
                SprdExp.Col = ColExpAmt
                p_Amt = Val(SprdExp.Text)
                SprdExp.Col = ColExpDebitAmt
                p_DebitAmt = Val(SprdExp.Text)
                If p_Amt < p_DebitAmt And p_DebitAmt <> 0 Then
                    MsgBox("Debit Amount Cann't be Greater Than Exp Amount.", MsgBoxStyle.Information)
                    Call MainClass.SetFocusToCell(SprdExp, ESRow, ColExpDebitAmt)
                    '                    Exit Sub
                End If
        End Select
        'Call DistributeExpInMainGrid
        Call CalcTots()
        Exit Sub
ErrPart:
        SprdExp.Col = ESCol
        SprdExp.col2 = ESCol
        SprdExp.Row = ESRow
        SprdExp.Row2 = ESRow
        SprdExp.BlockMode = True
        SprdExp.Action = 0
        SprdExp.BlockMode = False
        SprdExp.Focus()
    End Sub
    Private Sub SprdExp_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdExp.ClickEvent
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        'With SprdMain
        '    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With
    End Sub
    Private Sub txtBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDebitAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDebitAccount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDebitAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDebitAccount.DoubleClick
        On Error GoTo ErrPart
        If MainClass.SearchGridMaster((txtDebitAccount.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtDebitAccount.Text = AcName
            'txtMRRNo_Validate False
            txtDebitAccount.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtDebitAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDebitAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtDebitAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDebitAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDebitAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtDebitAccount_DoubleClick(txtDebitAccount, New System.EventArgs())
    End Sub
    Private Sub txtDebitAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDebitAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtDebitAccount.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            Cancel = True
            ErrorMsg("Please Enter the Valid Credit Account.", "", MsgBoxStyle.Critical)
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.DoubleClick
        SearchSupplier()
    End Sub
    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSupplier()
    End Sub
    Private Sub txtSupplier_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSupplier.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(txtSupplier.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND LOCATION_ID='" & Trim(txtLocationID.Text) & "'") = True Then
            xAcctCode = MasterNo
        Else
            MsgBox("Invalid Customer Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        'If Trim(txtVendorCode.Text) = "" Then
        '    If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "VENDOR_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
        '        txtVendorCode.Text = MasterNo
        '    End If
        'End If
        'txtBillTo.Text = GetDefaultLocation(xAcctCode)
        ''txtShipTo.Text = GetDefaultLocation(xAcctCode)

        'If txtBillTo.Text <> "" Then
        '    If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "SUPP_CUST_ADDR || ',' || SUPP_CUST_CITY || ',' || SUPP_CUST_STATE || ',' || ' GST NO :' || GST_RGN_NO", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'") = True Then
        '        txtAddress.Text = MasterNo
        '    End If
        'Else
        '    txtAddress.Text = ""
        'End If
        'Call AutoCompleteSearch("FIN_SUPP_CUST_BUSINESS_MST ", "LOCATION_ID", "SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'", txtBillTo)
        '''Call AutoCompleteSearch("FIN_SUPP_CUST_BUSINESS_MST ", "LOCATION_ID", "SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'", txtShipTo)


        'If ADDMode = True Then
        '    If MsgQuestion("Populate Data From Customer Detail ...") = CStr(MsgBoxResult.Yes) Then
        '        Call FillItemFromSuppCustDetail()
        '    End If
        '    txtPONo.Focus()
        'End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchSupplier()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" '' AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((txtSupplier.Text), "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", "SUPP_CUST_ADDR||SUPP_CUST_CITY", SqlStr) = True Then
            txtSupplier.Text = AcName
            txtLocationID.Text = AcName2
            txtSupplier_Validating(txtSupplier, New System.ComponentModel.CancelEventArgs(False))

            'txtBillTo.Text = AcName2
            'txtBillTo_Validating(txtBillTo, New System.ComponentModel.CancelEventArgs(False))
            'If txtCode.Enabled = True Then txtCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemType.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtItemType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtNarration_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNarration.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtNarration_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNarration.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtNarration.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPODate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTariff_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariff.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTariff_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTariff.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FillCboSaleType()
        On Error GoTo ErrPart
        Dim RsSaleType As ADODB.Recordset
        Dim SqlStr As String
        cboInvType.Items.Clear()
        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='P' ORDER BY NAME "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleType, ADODB.LockTypeEnum.adLockReadOnly)
        If RsSaleType.EOF = False Then
            Do While Not RsSaleType.EOF
                cboInvType.Items.Add(RsSaleType.Fields("NAME").Value)
                RsSaleType.MoveNext()
            Loop
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemType.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster((txtItemType.Text), "FIN_ITEMTYPE_MST", "NAME", , , , SqlStr) = True Then
            txtItemType.Text = AcName
            If txtItemType.Enabled = True Then txtItemType.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtItemType_DoubleClick(txtItemType, New System.EventArgs())
    End Sub
    Private Sub SearchTariff()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster((txtTariff.Text), "FIN_TARRIF_MST", "TARRIF_CODE", "TARRIF_DESC", , , SqlStr) = True Then
            txtTariff.Text = AcName
            txtItemType.Text = AcName1
            '        txtTariff_Validate False
            If txtTariff.Enabled = True Then txtTariff.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetReofferQty(ByRef pAutoKeyMrr As Double, ByRef pItemCode As String) As Double
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        GetReofferQty = 0
        mSqlStr = "SELECT SUM(LOT_ACC_RWK) AS QTY" & vbCrLf & " FROM INV_REOFFER_HDR IH, INV_REOFFER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF " & vbCrLf & " AND IH.AUTO_KEY_MRR=" & pAutoKeyMrr & "" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND MRR_FINAL_FLAG='Y' AND CANCELLED_STATUS='N' AND IS_POSTED='Y'"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetReofferQty = IIf(IsDbNull(RsTemp.Fields("QTY").Value), 0, RsTemp.Fields("QTY").Value)
        Else
            GetReofferQty = 0
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub txtWEFDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEFDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtWEFDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEFDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtWEFDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtWEFDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub CmdPopFromFile_Click(sender As Object, e As EventArgs) Handles CmdPopFromFile.Click
        Try
            Dim strFilePath As String = ""
            Dim intflag As Integer
            CommonDialogOpen.FileName = ""

            If Trim(txtSupplier.Text) = "" Then
                MsgBox("Please select Dept First.")
                Exit Sub
            End If

            If Trim(cboDivision.Text) = "" Then
                MsgBox("Please select Division First.")
                Exit Sub
            End If

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
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String = ""
        Dim mStockType As String = ""
        Dim mStockQty As Double
        Dim mPhyQty As Double
        Dim xSqlStr As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String
        Dim mPurYear As Long

        Dim mPartno As String
        Dim mHSNCode As String
        Dim mOBillNo As String
        Dim mOBillDate As String
        Dim mBillQty As Double
        Dim mBillRate As Double
        Dim mPORate As Double
        Dim mQty As Double
        Dim mRate As Double
        Dim mCgstPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""
        Dim mDivisionCode As Double
        Dim mTagNo As Double
        Dim mRemarks As String
        Dim mDeptCode As String

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

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
            mItemCode = Trim(IIf(IsDBNull(dtRow.Item(0)), "", dtRow.Item(0)))      ''Trim(IIf(IsDBNull(RsFile.Fields(0).Value), "", RsFile.Fields(0).Value))
            OpenLocalConnection()

            xSqlStr = " SELECT ITEM_SHORT_DESC,ISSUE_UOM,CUSTOMER_PART_NO, HSN_CODE " & vbCrLf _
                    & " FROM INV_ITEM_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(mItemCode) & "'"

            MainClass.UOpenRecordSet(xSqlStr, LocalPubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mItemDesc = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))
                mUOM = Trim(IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                mPartno = Trim(IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value))
                mHSNCode = Trim(IIf(IsDBNull(RsTemp.Fields("HSN_CODE").Value), "", RsTemp.Fields("HSN_CODE").Value))
            Else
                GoTo NextRecord
            End If

            ''Item Code	Item Description	Unit	HSN Code	PO No	PO Date	VNo	Bill No	Qty	Rate	Amount	CGST %	CGST Amount	SGST %	SGST Amount	IGST %	IGST Amount	InvoiceType

            mOBillNo = Trim(IIf(IsDBNull(dtRow.Item(5)), "", dtRow.Item(5)))
            mOBillDate = Trim(IIf(IsDBNull(dtRow.Item(6)), "", dtRow.Item(6)))
            mOBillDate = VB6.Format(mOBillDate, "DD/MM/YYYY")
            mBillQty = Val(IIf(IsDBNull(dtRow.Item(7)), 0, dtRow.Item(7)))
            mBillRate = Val(IIf(IsDBNull(dtRow.Item(8)), 0, dtRow.Item(8)))
            mPORate = Val(IIf(IsDBNull(dtRow.Item(9)), 0, dtRow.Item(9)))
            mQty = Val(IIf(IsDBNull(dtRow.Item(10)), 0, dtRow.Item(10)))
            mRate = Val(IIf(IsDBNull(dtRow.Item(11)), 0, dtRow.Item(11)))
            mCgstPer = Val(IIf(IsDBNull(dtRow.Item(12)), 0, dtRow.Item(12)))
            mSGSTPer = Val(IIf(IsDBNull(dtRow.Item(13)), 0, dtRow.Item(13)))
            mIGSTPer = Val(IIf(IsDBNull(dtRow.Item(14)), 0, dtRow.Item(14)))

            SprdMain.Row = SprdMain.MaxRows

            SprdMain.Col = ColItemCode
            SprdMain.Text = mItemCode

            SprdMain.Col = ColItemDesc
            SprdMain.Text = mItemDesc

            SprdMain.Col = ColPartNo
            SprdMain.Text = mPartno

            SprdMain.Col = ColHSNCode
            SprdMain.Text = mHSNCode

            SprdMain.Col = ColUnit
            SprdMain.Text = mUOM

            SprdMain.Col = ColBillNo
            SprdMain.Text = mOBillNo

            SprdMain.Col = ColBillDate
            SprdMain.Text = mOBillDate

            SprdMain.Col = ColPONo
            SprdMain.Text = txtPONo.Text

            SprdMain.Col = ColPODate
            SprdMain.Text = txtPODate.Text

            SprdMain.Col = ColPURFYear
            If Trim(mOBillDate) = "" Then
                mPurYear = -1
            Else
                mPurYear = GetCurrentFYNo(PubDBCn, mOBillDate)
            End If

            SprdMain.Text = mPurYear

            SprdMain.Col = ColPURMkey
            SprdMain.Text = "-1"

            SprdMain.Col = ColVNo
            SprdMain.Text = "-1"

            SprdMain.Col = ColVDate
            SprdMain.Text = txtVDate.Text

            SprdMain.Col = ColMRRNo
            SprdMain.Text = -1

            SprdMain.Col = ColMRRDate
            SprdMain.Text = txtVDate.Text


            SprdMain.Col = ColBillQty
            SprdMain.Text = VB6.Format(mBillQty, "0.000")

            SprdMain.Col = ColBillRate
            SprdMain.Text = VB6.Format(mBillRate, "0.000")

            SprdMain.Col = ColPORate
            SprdMain.Text = VB6.Format(mPORate, "0.000")

            SprdMain.Col = ColQty
            SprdMain.Text = VB6.Format(mQty, "0.000")

            SprdMain.Col = ColRate
            SprdMain.Text = VB6.Format(mRate, "0.000")

            SprdMain.Col = ColAmount
            SprdMain.Text = VB6.Format(mQty * mRate, "0.000")

            SprdMain.Col = ColCGSTPer
            SprdMain.Text = VB6.Format(mCgstPer, "0.000")

            SprdMain.Col = ColCGSTAmount
            SprdMain.Text = VB6.Format((mQty * mRate * mCgstPer) * 0.01, "0.000")

            SprdMain.Col = ColSGSTPer
            SprdMain.Text = VB6.Format(mSGSTPer, "0.000")

            SprdMain.Col = ColSGSTAmount
            SprdMain.Text = VB6.Format((mQty * mRate * mSGSTPer) * 0.01, "0.000")

            SprdMain.Col = ColIGSTPer
            SprdMain.Text = VB6.Format(mIGSTPer, "0.000")

            SprdMain.Col = ColIGSTAmount
            SprdMain.Text = VB6.Format((mQty * mRate * mIGSTPer) * 0.01, "0.000")

            SprdMain.MaxRows = SprdMain.MaxRows + 1

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
    Private Sub chkTDS_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkTDS.CheckStateChanged
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTDSRate.Enabled = True
            txtTDSDeductOn.Enabled = True
            txtSection.Enabled = True

            SqlStr = "SELECT NAME, TDS_DEFAULT_PER FROM TDS_SECTION_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND TDS_ON='P'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                If Trim(txtSection.Text) = "" Then
                    txtSection.Text = IIf(IsDBNull(RsTemp.Fields("NAME").Value), "", RsTemp.Fields("NAME").Value)
                End If
                If Val(txtTDSRate.Text) = 0 Then
                    txtTDSRate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TDS_DEFAULT_PER").Value), 0, RsTemp.Fields("TDS_DEFAULT_PER").Value), "0.000")
                End If
            End If

            'If Val(txtTDSRate.Text) = 0 Then
            '    SqlStr = "SELECT TDS_PER FROM FIN_SUPP_CUST_MST " & vbCrLf _
            '        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            '        & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'"
            '    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            '    If RsTemp.EOF = False Then
            '        txtTDSRate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TDS_PER").Value), 0, RsTemp.Fields("TDS_PER").Value), "0.000")
            '    End If
            'End If
        Else
            txtTDSDeductOn.Enabled = False
            txtSection.Enabled = False
            txtTDSRate.Enabled = False
            txtTDSRate.Text = CStr(0)
        End If
        txtTDSRate.Text = VB6.Format(txtTDSRate.Text, "0.000")
        If FormActive = False Then Exit Sub
        CalcTots()
    End Sub
    Private Sub ChkTDSRO_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkTDSRO.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If FormActive = False Then Exit Sub
        If FormActive = False Then Exit Sub
        CalcTots()
    End Sub
    Private Sub chkESI_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkESI.CheckStateChanged
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If chkESI.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtESIRate.Enabled = True
            txtESIDeductOn.Enabled = True
            If Val(txtESIRate.Text) = 0 Then
                SqlStr = "SELECT ESI_PER FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    txtESIRate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ESI_PER").Value), 0, RsTemp.Fields("ESI_PER").Value), "0.000")
                End If
            End If
        Else
            txtESIRate.Enabled = False
            txtESIDeductOn.Enabled = False
            txtESIRate.Text = CStr(0)
        End If
        txtESIRate.Text = VB6.Format(txtESIRate.Text, "0.000")
        If FormActive = False Then Exit Sub
        CalcTots()
    End Sub
    Private Sub ChkESIRO_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkESIRO.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If FormActive = False Then Exit Sub
        CalcTots()
    End Sub
    Private Sub ChkSTDSRO_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkSTDSRO.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If FormActive = False Then Exit Sub
        CalcTots()
    End Sub
    Private Sub ChkSTDS_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkSTDS.CheckStateChanged
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSTDSRate.Enabled = True
            txtSTDSDeductOn.Enabled = True
            If Val(txtSTDSRate.Text) = 0 Then
                SqlStr = "SELECT STDS_PER FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    txtSTDSRate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("STDS_PER").Value), 0, RsTemp.Fields("STDS_PER").Value), "0.000")
                End If
            End If
        Else
            txtSTDSRate.Enabled = False
            txtSTDSDeductOn.Enabled = False
            txtSTDSRate.Text = CStr(0)
        End If
        txtSTDSRate.Text = VB6.Format(txtSTDSRate.Text, "0.000")
        If FormActive = False Then Exit Sub
        CalcTots()
    End Sub
    Private Sub txtTDSAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSAmount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTDSAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTDSAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTDSAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtTDSDeductOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSDeductOn.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTDSDeductOn_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSDeductOn.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTDSDeductOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTDSDeductOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtTDSDeductOn.Text = VB6.Format(txtTDSDeductOn.Text, "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtTdsRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSRate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTdsRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTdsRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTDSRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtTDSRate.Text = VB6.Format(txtTDSRate.Text, "0.000")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtESIAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtESIAmount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtESIAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtESIAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtESIAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtESIAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtESIDeductOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtESIDeductOn.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtESIDeductOn_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtESIDeductOn.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtESIDeductOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtESIDeductOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtESIDeductOn.Text = VB6.Format(txtESIDeductOn.Text, "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtESIRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtESIRate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtESIRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtESIRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtESIRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtESIRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtESIRate.Text = VB6.Format(txtESIRate.Text, "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSTDSAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTDSAmount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSTDSAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSTDSAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSTDSAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSTDSAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSTDSDeductOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTDSDeductOn.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSTDSDeductOn_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSTDSDeductOn.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSTDSDeductOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSTDSDeductOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtSTDSDeductOn.Text = VB6.Format(txtSTDSDeductOn.Text, "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSTDSRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTDSRate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSTDSRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSTDSRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSTDSRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSTDSRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtSTDSRate.Text = VB6.Format(txtSTDSRate.Text, "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Function GenJVVno(ByRef xBookType As String) As String
        On Error GoTo ERR1
        Dim mVNo1 As String
        Dim SqlStr2 As String
        Dim SqlStr As String = ""
        Dim mBookType As String
        Dim mBookSubType As String
        ''    Call GenPrefixVNo
        ''
        GenJVVno = ""
        mBookType = VB.Left(ConJournal, 1)
        mBookSubType = VB.Right(ConJournal, 1)
        'If ADDMode = True Then
        SqlStr = "SELECT MAX(VNOSeq) From FIN_VOUCHER_HDR WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BookType='" & mBookType & "'" & vbCrLf _
            & " AND BookSubType='" & mBookSubType & "'" & vbCrLf _
            & " AND VTYPE='" & MainClass.AllowSingleQuote(xBookType) & "'"

        If RsCompany.Fields("CBJVoucherSeq").Value = "D" Then
            SqlStr = SqlStr & vbCrLf & " AND VDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "M" Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(VDATE,'MMMYYYY')=TO_CHAR('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','MMMYYYY')"
        ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "Y" Then

        End If

        GenJVVno = VB6.Format(MainClass.AutoGenVNo(SqlStr, PubDBCn), "00000")
        'End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description)
        'Resume
    End Function
    Private Function UpdateJVDetail(ByRef mMkey As String, ByRef mRowNo As Integer, ByRef mBookCode As String, ByRef mVType As String, ByRef mVNo As String, ByRef mVDate As String, ByRef pNarration As String, ByRef pDBCn As ADODB.Connection, ByRef mDivisionCode As Double) As Boolean
        On Error GoTo ErrDetail
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mAccountName As String
        Dim mAccountCode As String = ""
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
        mBookType = VB.Left(ConJournal, 1)
        mBookSubType = VB.Right(ConJournal, 1)
        SqlStr = "Delete From FIN_TEMPBILL_TRN Where UserId='" & MainClass.AllowSingleQuote(PubUserID) & "' AND TEMPMKEY=" & pProcessKey & ""
        pDBCn.Execute(SqlStr)
        SqlStr = "Delete From FIN_BILLDETAILS_TRN Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)
        SqlStr = "Delete From FIN_VOUCHER_DET Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)
        mRemarks = " agt Bill No(s) " & txtBillNo.Text & " Dt. " & txtBillDate.Text
        SqlStr = "DELETE FROM FIN_POSTED_TRN  WHERE " & vbCrLf & " MKEY ='" & mMkey & "' " & vbCrLf & " AND BookType='" & mBookType & "'" & vbCrLf & " AND BooksubType='" & mBookSubType & "'"
        pDBCn.Execute(SqlStr)
        '    Call InsertTempBill(mAccountCode, mAmount, mRemarks)
        '******SUPPLIER ACCOUNT POSTING
        mAccountName = txtSupplier.Text
        If mAccountName <> "" Then
            mPRRowNo = 1
            mDC = "D"
            mAccountCode = IIf(MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", pDBCn, mAccountCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mAccountCode, -1)
            mAmount = Val(txtTDSAmount.Text) + Val(txtESIAmount.Text) + Val(txtSTDSAmount.Text)
            mParticulars = "Bill No : " & txtBillNo.Text

            If Val(txtTDSAmount.Text) > 0 Then
                mParticulars = mParticulars & " (TDS DEDUCT ON RS. " & VB6.Format(txtTDSDeductOn.Text, "0.000") & " @ " & VB6.Format(txtTDSRate.Text, "0.000") & "%)"
            End If

            If Val(txtESIAmount.Text) > 0 Then
                mParticulars = mParticulars & " (ESI DEDUCT ON RS. " & VB6.Format(txtESIDeductOn.Text, "0.000") & " @ " & VB6.Format(txtESIRate.Text, "0.000") & "%)"
            End If

            If Val(txtSTDSAmount.Text) > 0 Then
                mParticulars = mParticulars & " (STDS DEDUCT ON RS. " & VB6.Format(txtSTDSDeductOn.Text, "0.000") & " @ " & VB6.Format(txtSTDSRate.Text, "0.000") & "%)"
            End If

            mChequeNo = ""
            mChqDate = ""
            mCCCode = "-1"
            mDeptCode = "-1"
            mEmpCode = "-1"
            mExpCode = "-1"
            mIBRNo = "-1"
            mClearDate = ""
            I = 1
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "','" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdateSuppPRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, mAmount, mRemarks, mDivisionCode) = False Then GoTo ErrDetail
        End If
        '******TDS ACCOUNT POSTING
        mPRRowNo = 2
        mDC = "C"
        mAccountCode = GetTDSAccountCode(txtSection.Text)       ''' IIf(IsDBNull(RsCompany.Fields("TDSCREDITACCOUNT").Value), "-1", RsCompany.Fields("TDSCREDITACCOUNT").Value)
        If Trim(mAccountCode) = "" Then
            MsgInformation("TDS Head Not Defined.")
            UpdateJVDetail = False
            Exit Function
        End If
        mParticulars = ""
        mParticulars = "Bill No : " & txtBillNo.Text & " (TDS DEDUCT ON RS. " & VB6.Format(txtTDSDeductOn.Text, "0.000") & " @ " & VB6.Format(txtTDSRate.Text, "0.000") & "%)"
        mAmount = Val(txtTDSAmount.Text)
        mChequeNo = ""
        mChqDate = ""
        mCCCode = "-1"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "-1"
        mIBRNo = "-1"
        mClearDate = ""
        I = 2
        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mBookType, mBookSubType, (txtVDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, pProcessKey) = False Then GoTo ErrDetail
        End If
        '******ESI ACCOUNT POSTING
        mPRRowNo = 3
        mDC = "C"
        mAccountCode = IIf(IsDBNull(RsCompany.Fields("ESICREDITACCOUNT").Value), "-1", RsCompany.Fields("ESICREDITACCOUNT").Value)
        mParticulars = "Bill No : " & txtBillNo.Text & " (ESI DEDUCT ON RS. " & VB6.Format(txtESIDeductOn.Text, "0.000") & " @ " & VB6.Format(txtESIRate.Text, "0.000") & "%)"
        mAmount = Val(txtESIAmount.Text)
        mChequeNo = ""
        mChqDate = ""
        mCCCode = "-1"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "-1"
        mIBRNo = "-1"
        mClearDate = ""
        I = 3
        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mBookType, mBookSubType, (txtVDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, pProcessKey) = False Then GoTo ErrDetail
        End If
        '******STDS ACCOUNT POSTING
        mPRRowNo = 4
        mDC = "C"
        mAccountCode = IIf(IsDBNull(RsCompany.Fields("STDSCREDITACCOUNT").Value), "-1", RsCompany.Fields("STDSCREDITACCOUNT").Value)
        mParticulars = "Bill No : " & txtBillNo.Text & " (STDS DEDUCT ON RS. " & VB6.Format(txtSTDSDeductOn.Text, "0.000") & " @ " & VB6.Format(txtSTDSRate.Text, "0.000") & "%)"
        mAmount = Val(txtSTDSAmount.Text)
        mChequeNo = ""
        mChqDate = ""
        mCCCode = "-1"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "-1"
        mIBRNo = "-1"
        mClearDate = ""
        I = 4
        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mBookType, mBookSubType, (txtVDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, pProcessKey) = False Then GoTo ErrDetail
        End If
        UpdateJVDetail = True
        Exit Function
ErrDetail:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateJVDetail = False
        ''Resume
    End Function
    Public Function UpdateSuppPRDetail(ByRef pDBCn As ADODB.Connection, ByRef pMKey As String, ByRef pRowNo As Integer, ByRef pTRNDtlSubRow As Integer, ByRef pAccountCode As String, ByRef pBookCode As String, ByRef pVType As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pVNo As String, ByRef pVDate As String, ByRef pTrnDC As String, ByRef pTrnAmount As Double, ByRef pChequeNo As String, ByRef pChqDate As String, ByRef pCostCCode As String, ByRef pDeptCode As String, ByRef pEmpCode As String, ByRef pExpCode As String, ByRef pIBRNo As String, ByRef pClearDate As String, ByRef pLocked As String, ByRef pNarration As String, ByRef xAmount As Double, ByRef xRemarks As String, ByRef mDivisionCode As Double) As Boolean
        On Error GoTo ErrDetail
        Dim RsTempPRDetail As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim pTRNType As String
        Dim pBillNo As String
        Dim pBillDate As String
        Dim pBillAmount As Double
        Dim pBillDC As String
        Dim pAmount As Double
        Dim pDC As String
        Dim pBillType As String
        Dim pSubRowNo As Integer
        Dim pRemarks As String
        Dim pDueDate As String

        Dim mAccountCode As String = "-1"
        pSubRowNo = 1000 * pRowNo
        pSubRowNo = pSubRowNo + 1
        pTRNType = "T"
        pBillNo = txtBillNo.Text
        pBillDate = txtBillDate.Text
        pBillAmount = Val(lblNetAmount.Text)
        pBillDC = "C"
        pAmount = xAmount
        pDC = "D"
        pRemarks = xRemarks
        pDueDate = txtPaymentdate.Text
        If GetAccountBalancingMethod(pAccountCode, True) = "D" Then
            SqlStr = "INSERT INTO FIN_BILLDETAILS_TRN ( " & vbCrLf _
                & " MKey, TRNDtlSubRowNo ,SubRowNo," & vbCrLf _
                & " AccountCode, TrnType, BillNo, BillDate," & vbCrLf _
                & " BillAmount,BillDc, Amount,Dc,REMARKS,DUEDATE,BILL_TO_LOC_ID,COMPANY_CODE,BILL_COMPANY_CODE,BOOKTYPE ) VALUES ( " & vbCrLf _
                & " '" & pMKey & "', " & pTRNDtlSubRow & "," & pSubRowNo & ", " & vbCrLf _
                & " '" & pAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(UCase(pTRNType)) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(pBillNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & pBillAmount & ", '" & pBillDC & "', " & pAmount & ", '" & pDC & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(pRemarks) & "',TO_DATE('" & VB6.Format(pDueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') ,'" & MainClass.AllowSingleQuote(txtLocationID.Text) & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("COMPANY_CODE").Value & ",'" & pBookType & "')"
            pDBCn.Execute(SqlStr)
        End If
        If pTRNType = "N" Then
            pBillType = "B"
        ElseIf pTRNType = "B" Or pTRNType = "O" Or pTRNType = "A" Then
            pBillType = "P"
        Else
            pBillType = pTRNType
        End If
        mAccountCode = IIf(MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", pDBCn, mAccountCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mAccountCode, "-1")

        If UpdateTRN(pDBCn, pMKey, pTRNDtlSubRow, pSubRowNo, pBookCode, pVType, pBookType, pBookSubType, pAccountCode, pVNo, pVDate, pBillNo, pBillDate, pAmount, pDC, pTRNType, pChequeNo, pChqDate, pCostCCode, pDeptCode, pEmpCode, pExpCode, pDueDate, pIBRNo, pBillType, pClearDate, pLocked, pNarration, pRemarks, (txtVDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, txtLocationID.Text) = False Then GoTo ErrDetail
        UpdateSuppPRDetail = True
        Exit Function
ErrDetail:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateSuppPRDetail = False
        'Resume
    End Function
End Class
