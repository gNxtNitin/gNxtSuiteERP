Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility

Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Configuration

''Imports Newtonsoft.Json
Imports System.Xml
'Imports System.Web.Script.Serialization
Imports System.Xml.Linq

Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Imports QRCoder
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Color

Imports System.Drawing
Imports System.Drawing.Printing

Friend Class FrmCust_SaleGST
    Inherits System.Windows.Forms.Form
    Dim RsSuppPurchMain As ADODB.Recordset ''Recordset
    Dim RsSuppPurchDetail As ADODB.Recordset ''Recordset
    Dim RsSuppPurchExp As ADODB.Recordset ''Recordset

    Dim FileDBCn As ADODB.Connection

    'Private PvtDBCn As ADODB.Connection
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim mAuthorised As Boolean
    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    'Dim SqlStr As String
    Dim mSupplierCode As String
    Dim pRound As Double

    Private DataLoading As Boolean
    'Private Const mBookType = "J"
    ''Private Const mBookSubType = "C"
    Dim mBookType As String
    Dim mBookSubType As String
    Private Const ConRowHeight As Short = 12
    Dim mAddUser As String
    Dim mAddDate As String
    Dim mModUser As String
    Dim mModDate As String
    Private Const ColItemCode As Short = 1
    Private Const ColPartNo As Short = 2
    Private Const ColHSNCode As Short = 3
    Private Const ColItemDesc As Short = 4
    Private Const ColUnit As Short = 5


    Private Const ColModel As Short = 6
    Private Const ColDrawingNo As Short = 7
    Private Const ColChargeableWidth As Short = 8
    Private Const ColChargeableHeight As Short = 9

    Private Const ColPURFYear As Short = 10
    Private Const ColPURMkey As Short = 11
    Private Const ColBillNo As Short = 12
    Private Const ColBillDate As Short = 13
    Private Const ColInvoiceNo As Short = 14
    Private Const ColBillQty As Short = 15
    Private Const ColBillRate As Short = 16
    Private Const ColPORate As Short = 17
    Private Const ColQty As Short = 18
    Private Const ColRate As Short = 19
    Private Const ColAmount As Short = 20
    Private Const ColTaxableAmount As Short = 21
    Private Const ColCGSTPer As Short = 22
    Private Const ColCGSTAmount As Short = 23
    Private Const ColSGSTPer As Short = 24
    Private Const ColSGSTAmount As Short = 25
    Private Const ColIGSTPer As Short = 26
    Private Const ColIGSTAmount As Short = 27

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

    Private Const ColPayBillNo As Short = 1
    Private Const ColPayBillDate As Short = 2
    Private Const ColPayBillAmount As Short = 3
    Private Const ColPayBalAmount As Short = 4
    Private Const ColPayBalDC As Short = 5
    Private Const ColPayPaymentAmt As Short = 6

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer


    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
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
    Private Sub chkGSTApplicable_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkGSTApplicable.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub
    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkFinalPost_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFinalPost.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkItemDetails_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkItemDetails.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)




        If chkItemDetails.CheckState = System.Windows.Forms.CheckState.Checked Then
            fraService.Enabled = False
            SprdMain.Enabled = True
        Else
            fraService.Enabled = True
            SprdMain.Enabled = False
        End If

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
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart
        Dim xDCNo As String
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mBookCode As Integer
        Dim mLockBookCode As Integer
        Dim mJVMKEY As String
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

        If mAuthorised = False Then
            If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Bill Final Posted, so cann't be deleted.")
                Exit Sub
            End If
        End If

        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Bill Cann't be Deleted.")
            Exit Sub
        End If
        If Trim(txtIRNNo.Text) <> "" Then
            MsgInformation("IRN No Made against this invoice So cann't be Deleted.")
            Exit Sub
        End If
        If CheckBillPayment(mSupplierCode, (txtBillNo.Text), "B", (txtBillDate.Text)) = True Then Exit Sub
        If Not RsSuppPurchMain.EOF Then
            '        mJVMKEY = RsSuppPurchMain!JVMKEY
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_SALE_HDR", LblMKey.Text, RsSuppPurchMain, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_SALE_DET", LblMKey.Text, RsSuppPurchDetail, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_SALE_EXP", LblMKey.Text, RsSuppPurchExp, "MKEY", "D") = False Then GoTo DelErrPart

                If InsertIntoDeleteTrn(PubDBCn, "FIN_SUPP_SALE_HDR", "MKEY", (LblMKey.Text)) = False Then GoTo DelErrPart



                '            If InsertIntoDelAudit(PubDBCn, "FIN_VOUCHER_HDR", txtJVVNO.Text, RsSuppPurchMain, "VNO") = False Then GoTo DelErrPart:
                PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & mBookType & "' AND BOOKCODE='" & LblBookCode.Text & "'")
                PubDBCn.Execute("Delete from FIN_SUPP_SALE_EXP Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("Delete from FIN_SUPP_SALE_DET Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("DELETE FROM FIN_SUPP_SALE_HDR WHERE MKey='" & LblMKey.Text & "' ")
                '            PubDBCn.Execute "DELETE FROM FIN_POSTED_TRN WHERE MKey='" & mJVMKey & "' AND BookType='" & vb.Left(ConJournal, 1) & "' AND BookSubType='" & Right(ConJournal, 1) & "'"
                '
                SqlStr = "DELETE FROM FIN_BILLDETAILS_TRN WHERE Mkey='" & LblMKey.Text & "' "
                PubDBCn.Execute(SqlStr)
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
                SqlStr = "DELETE FROM FIN_POSTED_TRN  WHERE Mkey='" & LblMKey.Text & "'" & vbCrLf & " AND BookType='" & mBookType & "'" & vbCrLf & " AND BOOKCODE='" & LblBookCode.Text & "' "
                PubDBCn.Execute(SqlStr)
                SqlStr = "DELETE FROM TDS_TRN WHERE Mkey='" & LblMKey.Text & "' AND BOOKCODE=-1 "
                PubDBCn.Execute(SqlStr)
                PubDBCn.CommitTrans()
                RsSuppPurchMain.Requery() ''.Refresh
                RsSuppPurchDetail.Requery() ''.Refresh
                RsSuppPurchExp.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        '        Resume
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

        End If

        If PubUserID <> "G0416" Then
            If Trim(txtIRNNo.Text) <> "" Then
                MsgInformation("IRN No Made against this invoice So cann't be Modified.")
                Exit Sub
            End If
        End If

        If mAuthorised = False Then
            If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Final Bill  Post Cann't be Modified")
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
    Private Sub ReportOnPurchase(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String
        Dim mRptFileName As String
        Dim mVNo As String
        Dim mPrintOption As String
        Dim cntRow As Integer
        Dim mOriginialInvNo As String
        Dim mCheckOriginialInvNo As String
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Invoice Cann't be Print.")
            Exit Sub
        End If

        frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(4).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(3).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).Enabled = False

        frmPrintInvCopy.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If

        'If frmPrintInvCopy.optShow(3).Checked = True Or frmPrintInvCopy.optShow(4).Checked = True Then
        '    Call ReportOnPackingSlip(Crystal.DestinationConstants.crptToWindow, IIf(frmPrintInvCopy.optShow(3).Checked = True, "I", "O"))
        '    frmPrintInvCopy.Dispose()
        '    frmPrintInvCopy.Close()
        '    Exit Sub
        'End If


        'With SprdMain
        '    .Row = 1
        '    .Col = ColInvoiceNo
        '    mCheckOriginialInvNo = CStr(Val(.Text))
        '    For cntRow = 1 To .MaxRows - 1
        '        .Row = cntRow
        '        .Col = ColInvoiceNo
        '        mOriginialInvNo = CStr(Val(.Text))
        '        If mCheckOriginialInvNo <> mOriginialInvNo Then
        '            GoTo NextCheck
        '        End If
        '    Next
        'End With
        Call ReportOnInvoice(Crystal.DestinationConstants.crptToWindow, "G")
        frmPrintInvCopy.Close()
        Exit Sub
NextCheck:
        frmPrintInvoice.OptInvoiceAnnex.Enabled = True
        frmPrintInvoice.OptInvoiceAnnex.Visible = True
        frmPrintInvoice.optSubsidiaryChallan.Enabled = False
        frmPrintInvoice.optSubsidiaryChallan.Visible = False
        frmPrintInvoice.FraF4.Enabled = False
        frmPrintInvoice.FraF4.Visible = False
        frmPrintInvoice.ShowDialog()
        If G_PrintLedg = False Then
            frmPrintInvoice.Close()
            Exit Sub
        Else
            mPrintOption = IIf(frmPrintInvoice.OptInvoiceAnnex.Checked = True, "A", "I") 'A-Annex , I-Invoice, G-GST Invoice
            Call ReportOnInvoice(Crystal.DestinationConstants.crptToWindow, mPrintOption)
            frmPrintInvoice.Close()
            Exit Sub
        End If
        Exit Sub
ERR1:
        frmPrintInvCopy.Close()
        MsgInformation(Err.Description)
    End Sub
    Private Sub ReportOnInvoice(ByRef Mode As Crystal.DestinationConstants, ByRef mPrintOption As String)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String
        Dim mRptFileName As String
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)
        SqlStr = ""
        Call SelectQryForVoucher(SqlStr)

        If mPrintOption = "A" Then
            mTitle = IIf(LblBookCode.Text = -21, "Credit Note Annex.", "Debit Note Annex.") '' "Purchase Voucher"
            mRptFileName = "Cust_Sale_Annx.rpt"
            mSubTitle = txtSupplier.Text & " Bill No : " & txtBillNo.Text & " Bill Date : " & VB6.Format(txtBillDate.Text, "DD/MM/YYYY")
        ElseIf mPrintOption = "I" Then
            mTitle = IIf(LblBookCode.Text = -21, "Credit Note", "Debit Note")  ''"Credit Note"
            mRptFileName = "Cust_Sale_Group.rpt"
            mSubTitle = Trim(Mid(cboReason.Text, 3))
        ElseIf mPrintOption = "G" Then
            mTitle = IIf(LblBookCode.Text = -21, "Credit Note", "Debit Note")  ''"Credit Note"
            mRptFileName = IIf(chkItemDetails.CheckState = System.Windows.Forms.CheckState.Unchecked, "Cust_Sale_WOItem.rpt", "Cust_Sale.rpt")
            mSubTitle = Trim(Mid(cboReason.Text, 3))
        End If

        'Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, True)
        Dim mPDFPrint As Boolean = False
        Dim mInvoicePrintType As String = ""

        If frmPrintInvCopy.optShow(0).Checked = True Then     ''mPDF
            mPDFPrint = False
        Else
            mPDFPrint = True
        End If

        Call ShowExcisePDFReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, True, mInvoicePrintType, mPDFPrint)


        Exit Sub
ERR1:
        'Resume
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowExcisePDFReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef IsSubReport As Boolean, ByRef mInvoicePrintType As String, ByVal mPDF As Boolean)

        On Error GoTo ErrPart
        Dim RsTemp As New ADODB.Recordset
        Dim RS As New ADODB.Recordset

        Dim fPath As String
        Dim mBillNoStr As String

        Dim mAmountInword As String
        Dim SqlStrSub As String
        Dim mDutyInword As String
        Dim SqlStr As String = ""

        Dim RsTempShip As ADODB.Recordset = Nothing
        Dim mNetAmount As Double
        Dim mNetDuty As Double
        Dim mPrepTime As String = ""
        Dim mRemovalTime As String = ""
        Dim mManuAVInWord As String
        Dim mManuCessInWord As String
        Dim mManuEDInWord As String
        Dim mManuHCessInWord As String
        Dim mDealerDetail As String
        Dim mDealerAddress As String
        Dim mManuAddress As String
        Dim mSO As Double
        Dim mPayTerms As String
        Dim mBalPayTerms As String
        Dim mJurisdiction As String
        Dim mShipToSameParty As String
        Dim mShipToCode As String

        Dim mShipToName As String = ""
        Dim mShipToAddress As String = ""
        Dim mShipToCity As String = ""
        Dim mShipToGSTN As String = ""
        Dim mCompanyDetail As String = ""
        Dim mCompanyeMail As String = ""
        Dim mCompanyWebSite As String = ""
        Dim mShipToState As String = ""
        Dim mShipToStateCode As String = ""
        Dim mStateName As String = ""
        Dim mStateCode As String = ""
        Dim mWithInState As String = ""
        Dim mWithInCountry As String = ""
        Dim mPlaceofSupply As String = ""
        Dim mExpHeading As String
        Dim mLUT As String
        Dim mCustomerCode As String
        Dim pBarCodeString As String

        Dim mShipFromOtherThan As String
        Dim mShipFromCode As String
        Dim mShipFromName As String
        Dim mShipFromAddress As String
        Dim mShipFromCity As String
        Dim mShipFromState As String
        Dim mShipFromStateCode As String
        Dim mShipFromGSTN As String
        Dim mExWork As String
        Dim path As String
        Dim mCurrency As String
        Dim mRateTitle As String
        Dim mAmountTitle As String
        Dim mShipLocation As String
        Dim mHour As String = ""
        Dim mMin As String = ""
        Dim mShipToPAN As String = ""

        Dim CrReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument  ' Report Name 
        Dim CrExportOptions As ExportOptions
        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions

        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions


        mRptFileName = PubReportFolderPath & mRptFileName      ''"PDF_" &
        'mRptFileName = "G:\VBDotNetERP_Blank\Form\bin\Debug\Reports\PDF_Invoice_SGSTNew.rpt"
        CrReport.Load(mRptFileName)

        SqlStrSub = " SELECT * " & vbCrLf _
            & " FROM FIN_SUPP_SALE_EXP, FIN_SUPP_SALE_HDR, FIN_INTERFACE_MST " & vbCrLf _
            & " WHERE FIN_SUPP_SALE_EXP.MKEY = FIN_SUPP_SALE_HDR.MKEY " & vbCrLf _
            & " AND FIN_SUPP_SALE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf _
            & " AND FIN_INVOICE_HDR.COMPANY_CODE = FIN_INTERFACE_MST.COMPANY_CODE" & vbCrLf _
            & " And FIN_SUPP_SALE_EXP.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'" & vbCrLf _
            & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AMOUNT<>0"

        SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"

        SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"

        Call Connect_MainReport_To_Database_11(CrReport)        '', mSqlStr
        Call Connect_SubReport_To_Database_11(CrReport, "PurExp")      '

        CrReport.RecordSelectionFormula = "{IH.COMPANY_CODE} = " & RsCompany.Fields("COMPANY_CODE").Value & " AND {IH.MKEY} = '" & MainClass.AllowSingleQuote(LblMKey.Text) & "' AND {IH.FYEAR} = '" & RsCompany.Fields("FYEAR").Value & "' AND {BP.USER_ID} = '" & MainClass.AllowSingleQuote(PubUserID) & "'"

        ClearCRpt11Formulas(CrReport)
        CrReport.ReportOptions.EnableSaveDataWithReport = False
        SetCompanyReport11(CrReport, 1, mTitle, mSubTitle)
        'CrReport.VerifyDatabase()   .VerifyOnEveryPrint = True '' blnVerifyOnEveryPrint		
        CrReport.Refresh()

        mStateName = ""
        mStateCode = ""

        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = MasterNo
        End If

        mStateName = GetPartyBusinessDetail(Trim(mCustomerCode), Trim(txtBillTo.Text), "SUPP_CUST_STATE")
        mStateCode = GetStateCode(mStateName)

        mWithInState = GetPartyBusinessDetail(Trim(mCustomerCode), Trim(txtBillTo.Text), "WITHIN_STATE")

        'If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mStateName = MasterNo
        '    mStateCode = GetStateCode(mStateName)
        'End If

        'If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mWithInState = MasterNo
        'End If

        If mWithInState = "N" Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithInCountry = MasterNo
            End If
        End If

        mPlaceofSupply = VB6.Format(mStateCode, "00") & "-" & mStateName ''IIf(mWithInState = "Y", "INTRA STATE", IIf(mWithInCountry = "Y", "INTER STATE", "EXPORT"))							


        '    Report1.ReportFileName = PubReportFolderPath & mRptFileName							
        '    Report1.SQLQuery = mSqlStr							
        '    Report1.WindowShowGroupTree = False							
        '							
        '    Report1.WindowShowPrintBtn = False '' IIf(PubSuperUser = "S", True, False)							
        '    Report1.WindowShowPrintSetupBtn = False ''IIf(PubSuperUser = "S", True, False)							
        '    Report1.WindowShowExportBtn = IIf(PubSuperUser = "S", True, False)							

        SqlStr = " SELECT NETVALUE, ITEMVALUE, " & vbCrLf _
            & " (SELECT SUM(CGST_AMOUNT) FROM FIN_SUPP_SALE_DET WHERE COMPANY_CODE=IH.COMPANY_CODE AND MKEY=IH.MKEY) AS NETCGST_AMOUNT, " & vbCrLf _
            & " (SELECT SUM(SGST_AMOUNT) FROM FIN_SUPP_SALE_DET WHERE COMPANY_CODE=IH.COMPANY_CODE AND MKEY=IH.MKEY) AS  NETSGST_AMOUNT, " & vbCrLf _
            & " (SELECT SUM(IGST_AMOUNT) FROM FIN_SUPP_SALE_DET WHERE COMPANY_CODE=IH.COMPANY_CODE AND MKEY=IH.MKEY) AS NETIGST_AMOUNT, '' AS INV_PREP_TIME, " & vbCrLf _
            & " SUPP_CUST_CODE AS SHIPPED_TO_PARTY_CODE, '' AS REMOVAL_TIME, -1 AS OUR_AUTO_KEY_SO, 'Y' AS SHIPPED_TO_SAMEPARTY, " & vbCrLf _
            & " 'N' AS IS_DESP_OTHERTHAN_BILL, '' AS SHIPPED_FROM_PARTY_CODE, 'N' AS IS_SHIPPTO_EX_WORK" & vbCrLf _
            & " FROM FIN_SUPP_SALE_HDR IH" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mNetAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
            mNetDuty = IIf(IsDBNull(RsTemp.Fields("NETCGST_AMOUNT").Value), 0, RsTemp.Fields("NETCGST_AMOUNT").Value)
            mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETSGST_AMOUNT").Value), 0, RsTemp.Fields("NETSGST_AMOUNT").Value)
            mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("NETIGST_AMOUNT").Value), 0, RsTemp.Fields("NETIGST_AMOUNT").Value)


            mShipToSameParty = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value), "Y", RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value)
            mShipToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
            mExWork = "N"       ''IIf(IsDBNull(RsTemp.Fields("IS_SHIPPTO_EX_WORK").Value), "N", RsTemp.Fields("IS_SHIPPTO_EX_WORK").Value)

            mPrepTime = "" '' VB6.Format(IIf(IsDBNull(RsTemp.Fields("INV_PREP_TIME").Value), "", RsTemp.Fields("INV_PREP_TIME").Value), "HH:MM")
            mRemovalTime = "" ''VB6.Format(IIf(IsDBNull(RsTemp.Fields("REMOVAL_TIME").Value), "", RsTemp.Fields("REMOVAL_TIME").Value), "HH:MM")
            mSO = IIf(IsDBNull(RsTemp.Fields("OUR_AUTO_KEY_SO").Value), "", RsTemp.Fields("OUR_AUTO_KEY_SO").Value)

            'mHour = HoursInText(VB.Left(mRemovalTime, 2))
            'mMin = MinInText(VB.Right(mRemovalTime, 2))

            mHour = mHour & " " & mMin

            mShipToCode = mCustomerCode
            mShipLocation = Trim(txtBillTo.Text)

            SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE,  " & vbCrLf _
                & " SUPP_CUST_PIN, GST_RGN_NO" & vbCrLf _
                & " FROM FIN_SUPP_CUST_BUSINESS_MST " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShipToCode) & "' AND LOCATION_ID='" & MainClass.AllowSingleQuote(mShipLocation) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTempShip.EOF = False Then
                mShipToName = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_NAME").Value), "", RsTempShip.Fields("SUPP_CUST_NAME").Value)
                mShipToAddress = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_ADDR").Value), "", RsTempShip.Fields("SUPP_CUST_ADDR").Value)
                mShipToAddress = Replace(mShipToAddress, vbCrLf, "")
                mShipToCity = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_CITY").Value), "", RsTempShip.Fields("SUPP_CUST_CITY").Value)
                mShipToCity = mShipToCity & " " & IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)
                mShipToState = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_STATE").Value), "", RsTempShip.Fields("SUPP_CUST_STATE").Value)
                mShipToStateCode = GetStateCode(mShipToState)
                mShipToGSTN = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)

                mShipToPAN = ""

                If MainClass.ValidateWithMasterTable(mShipToName, "SUPP_CUST_NAME", "PAN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mShipToPAN = MasterNo
                End If

            End If



            'mShipFromOtherThan = IIf(IsDBNull(RsTemp.Fields("IS_DESP_OTHERTHAN_BILL").Value), "N", RsTemp.Fields("IS_DESP_OTHERTHAN_BILL").Value)
            'mShipFromCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_FROM_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_FROM_PARTY_CODE").Value)

            mShipFromName = ""
            mShipFromAddress = ""
            mShipFromAddress = ""
            mShipFromCity = ""
            mShipFromCity = ""
            mShipFromState = ""
            mShipFromStateCode = ""
            mShipFromGSTN = ""

        End If

        AssignCRpt11Formulas(CrReport, "InvoicePrintType", "'" & mInvoicePrintType & "'")
        AssignCRpt11Formulas(CrReport, "CompanyCity", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyGSTIN", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & "'")

        ''-------------
        AssignCRpt11Formulas(CrReport, "CompanyAddressNew", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyPin", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyState", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyPhone", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PHONE").Value), "", RsCompany.Fields("COMPANY_PHONE").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyFax", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_FAXNO").Value), "", RsCompany.Fields("COMPANY_FAXNO").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyEmail", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", RsCompany.Fields("COMPANY_MAILID").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyWeb", "'" & IIf(IsDBNull(RsCompany.Fields("WEBSITE").Value), "", RsCompany.Fields("WEBSITE").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyPAN", "'" & IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value) & "'")
        Dim mCompanyStateCode As String = GetStateCode(IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyStateCode", "'" & mCompanyStateCode & "'")
        ''---------------
        mJurisdiction = "All Disputes Subject to " & IIf(IsDBNull(RsCompany.Fields("JURISDICTION").Value), "", RsCompany.Fields("JURISDICTION").Value) & " Jurisdiction."

        AssignCRpt11Formulas(CrReport, "COMPANYTINNo", "'" & IIf(IsDBNull(RsCompany.Fields("TINNO").Value), "", RsCompany.Fields("TINNO").Value) & "'")
        AssignCRpt11Formulas(CrReport, "COMPANYCINNo", "'" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & "'")

        mCompanyeMail = IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", "e-mail : " & RsCompany.Fields("COMPANY_MAILID").Value)
        mCompanyWebSite = IIf(IsDBNull(RsCompany.Fields("WEBSITE").Value), "", "WebSite : " & RsCompany.Fields("WEBSITE").Value)
        mCompanyDetail = mCompanyeMail & ", " & mCompanyWebSite

        AssignCRpt11Formulas(CrReport, "COMPANYDETAIL", "'" & mCompanyDetail & "'")
        AssignCRpt11Formulas(CrReport, "PrepTime", "'" & mPrepTime & "'")
        AssignCRpt11Formulas(CrReport, "RemovalTime", "'" & mRemovalTime & "'")
        AssignCRpt11Formulas(CrReport, "RemovalTimeInWord", "'" & mHour & "'")
        AssignCRpt11Formulas(CrReport, "ShipToPAN", "'" & mShipToPAN & "'")


        'AssignCRpt11Formulas(CrReport, "JWRemarks", "'" & mJWRemarks & "'")
        AssignCRpt11Formulas(CrReport, "Jurisdiction", "'" & mJurisdiction & "'")
        AssignCRpt11Formulas(CrReport, "mShipToName", "'" & mShipToName & "'")
        AssignCRpt11Formulas(CrReport, "mShipToAddress", "'" & mShipToAddress & "'")
        AssignCRpt11Formulas(CrReport, "mShipToCity", "'" & mShipToCity & "'")
        AssignCRpt11Formulas(CrReport, "mShipToGSTN", "'" & mShipToGSTN & "'")
        AssignCRpt11Formulas(CrReport, "mShipToState", "'" & mShipToState & "'")
        AssignCRpt11Formulas(CrReport, "mShipToStateCode", "'" & mShipToStateCode & "'")
        AssignCRpt11Formulas(CrReport, "mStateName", "'" & mStateName & "'")
        AssignCRpt11Formulas(CrReport, "mStateCode", "'" & mStateCode & "'")
        AssignCRpt11Formulas(CrReport, "mPlaceofSupply", "'" & mPlaceofSupply & "'")
        'AssignCRpt11Formulas(CrReport, "mServiceName", "'" & Trim(txtServProvided.Text) & "'")



        'If Val(lblInvoiceSeq.Text) = 6 Or Val(lblInvoiceSeq.Text) = 7 Then
        '    If chkLUT.CheckState = System.Windows.Forms.CheckState.Checked Then
        '        mLUT = GetLUT((txtBillDate.Text))
        '    Else
        '        mLUT = ""
        '    End If

        '    AssignCRpt11Formulas(CrReport, "LUTNo", "'" & mLUT & "'")
        '    mExpHeading = "SUPPLY MEANT FOR EXPORT ON PAYMENT OF IGST OR SUPPLY MEANT FOR EXPORT UNDER BOND OR LETTER OF UNDERTAKING WITHOUT PAYMENT OF IGST"
        '    MainClass.AssignCRptFormulas(Report1, "ExpHeading=""" & mExpHeading & """")

        'End If

        'mPayTerms = ""

        If IsSubReport = True Then

            If mNetAmount = 0 Then
                mAmountInword = " Zero"
            Else
                mAmountInword = MainClass.RupeesConversion(mNetAmount)
            End If

            mDutyInword = MainClass.RupeesConversion(mNetDuty)

            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
                AssignCRpt11Formulas(CrReport, "AmountInWord", "'Rs. Zero'")
                AssignCRpt11Formulas(CrReport, "DutyInword", "'Rs. Zero'")
                AssignCRpt11Formulas(CrReport, "NetAmount", "'0.00'")
            Else
                AssignCRpt11Formulas(CrReport, "AmountInWord", "'" & mAmountInword & "'")
                AssignCRpt11Formulas(CrReport, "NetAmount", "'" & VB6.Format(mNetAmount, "0.00") & "'")
                AssignCRpt11Formulas(CrReport, "DutyInword", "'" & mDutyInword & "'")
            End If

            'SqlStrSub = " SELECT FIN_SUPP_SALE_EXP.MKEY, FIN_SUPP_SALE_EXP.SUBROWNO, FIN_SUPP_SALE_EXP.EXPPERCENT, FIN_SUPP_SALE_EXP.AMOUNT, FIN_INTERFACE_MST.COMPANY_CODE, FIN_INTERFACE_MST.NAME" & vbCrLf _
            '    & " FROM FIN_SUPP_SALE_EXP, FIN_SUPP_SALE_HDR, FIN_INTERFACE_MST " & vbCrLf _
            '    & " WHERE FIN_SUPP_SALE_EXP.MKEY = FIN_SUPP_SALE_HDR.MKEY AND FIN_SUPP_SALE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf _
            '    & " AND FIN_SUPP_SALE_EXP.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'" & vbCrLf _
            '    & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AMOUNT<>0"

            'SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"

            'SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"

            'Report1.SubreportToChange = "PurExp" ''Report1.GetNthSubreportName(0)						
            'Report1.Connect = STRRptConn
            'Report1.SQLQuery = SqlStrSub
            'MainClass.AssignCRptFormulas(Report1, "JWSTRemarks=""" & mJWSTRemarks & """")
            '          Report1.SubreportToChange = ""			

        End If


        Dim mBMPFileName As String = ""
        mBillNoStr = Trim(txtVNoPrefix.Text) & Trim(txtVNo.Text)
        mBillNoStr = Replace(mBillNoStr, "/", "_")
        mBillNoStr = Replace(mBillNoStr, "\", "_")
        mBMPFileName = RefreshQRCode(LblMKey.Text, mBillNoStr, txtIRNNo.Text)

        If Not FILEExists(mBMPFileName) Then
            mBMPFileName = ""
        End If
        Application.DoEvents()

        AssignCRpt11Formulas(CrReport, "PicLocation", "'" & mBMPFileName & "'")

        Dim mClientLogoPath As String = ""
        If Not FILEExists(PubClientLogoPath) Then
            mClientLogoPath = ""
        Else
            mClientLogoPath = PubClientLogoPath
        End If
        AssignCRpt11Formulas(CrReport, "CompanyLogo", "'" & mClientLogoPath & "'")

        If mPDF = True Then
            Dim pOutPutFileName As String = ""
            mBillNoStr = Trim(txtVNoPrefix.Text) & Trim(txtVNo.Text)
            mBillNoStr = Replace(mBillNoStr, "/", "_")
            mBillNoStr = Replace(mBillNoStr, "\", "_")

            fPath = mPubBarCodePath & "\CustCredit_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"
            pOutPutFileName = mPubBarCodePath & "\CustCredit_DS_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"

            'FrmInvoiceViewer.CrystalReportViewer1.ReportSource = Nothing
            'FrmInvoiceViewer.CrystalReportViewer1.ReportSource = CrReport
            'FrmInvoiceViewer.CrystalReportViewer1.Show()

            CrDiskFileDestinationOptions.DiskFileName = fPath
            CrExportOptions = CrReport.ExportOptions

            With CrExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.PortableDocFormat
                .DestinationOptions = CrDiskFileDestinationOptions
                .FormatOptions = CrFormatTypeOptions
            End With
            CrReport.Export()

            If FILEExists(fPath) Then
                If frmPrintInvCopy.optShow(1).Checked = True Then
                    Process.Start("explorer.exe", fPath)
                End If
            End If

            If frmPrintInvCopy.optShow(2).Checked = True Then

                ''My test

                'Dim mSignerName As String
                Dim mPrintDigitalSign As String
                mPrintDigitalSign = "Authorised Signatory" ''"For " & RsCompany.Fields("PRINT_COMPANY_NAME").Value  ''"Authorised Signatory"
                'mSignerName = GetDigitalSignName(PubUserID)
                'If mSignerName <> "" Then
                pOutPutFileName = mPubBarCodePath & "\TaxInvoice_DigialSign_" & RsCompany.Fields("COMPANY_CODE").Value & "_" & mBillNoStr & ".pdf"
                If SignPdf(fPath, pOutPutFileName, mPrintDigitalSign) = False Then Exit Sub

                If FILEExists(pOutPutFileName) Then
                    Process.Start("explorer.exe", pOutPutFileName)
                End If
            End If
            'End If
        Else
            If mMode = Crystal.DestinationConstants.crptToWindow Then
                FrmInvoiceViewer.CrystalReportViewer1.ReportSource = Nothing
                'FrmInvoiceViewer.CrystalReportViewer1.DataBindings()
                FrmInvoiceViewer.CrystalReportViewer1.ReportSource = CrReport
                FrmInvoiceViewer.CrystalReportViewer1.Show()
                FrmInvoiceViewer.MdiParent = Me.MdiParent
                FrmInvoiceViewer.CrystalReportViewer1.ShowGroupTreeButton = False
                FrmInvoiceViewer.CrystalReportViewer1.DisplayGroupTree = False
                FrmInvoiceViewer.Dock = DockStyle.Fill
                FrmInvoiceViewer.Show()
            Else

                'CrReport.PrintToPrinter(1, False, 1, 99)

                'For Each prt In PrinterSettings.InstalledPrinters       ''Printers
                '    If UCase(prt) = UCase("Universal Printer") Then
                '        CrReport.PrintOptions.PrinterName = prt.DeviceName
                '        Exit For
                '    End If
                'Next
                Dim settings As PrinterSettings = New PrinterSettings()
                For Each printer As String In PrinterSettings.InstalledPrinters

                    If settings.IsDefaultPrinter Then
                        settings.PrinterName = printer
                        Exit For
                    End If
                Next

                CrReport.PrintToPrinter(1, False, 1, 99)
                CrReport.Dispose()
            End If
        End If


        Exit Sub
ErrPart:
        'Resume		
        CrReport.Dispose()
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowReportOld(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef IsSubReport As Boolean)
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
        mStateName = ""
        mStateCode = ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mStateName = MasterNo
            mStateCode = GetStateCode(mStateName)
        End If
        MainClass.AssignCRptFormulas(Report1, "mStateCode=""" & mStateCode & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyGSTIN=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "COMPANYCINNo=""" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & """")
        If IsSubReport = True Then
            mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(lblNetAmount.Text) = 0, 0, lblNetAmount.Text)))
            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
                MainClass.AssignCRptFormulas(Report1, "AmountInWord=""Rs. Zero""")
                MainClass.AssignCRptFormulas(Report1, "NetAmount=""0.00""")
            Else
                MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")
                MainClass.AssignCRptFormulas(Report1, "NetAmount=""" & lblNetAmount.Text & """")
            End If
            SqlStrSub = " SELECT MKEY, SUBROWNO, EXPPERCENT, AMOUNT, COMPANY_CODE, NAME" & vbCrLf & " FROM FIN_SUPP_SALE_EXP, FIN_INTERFACE_MST " & vbCrLf & " WHERE FIN_SUPP_SALE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf & " AND FIN_SUPP_SALE_EXP.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'" & vbCrLf & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND GST_ENABLED='Y' " & vbCrLf & " ORDER BY SUBROWNO"
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
        MsgBox(Err.Description)
    End Sub
    Private Function SelectQryForVoucher(ByRef mSqlStr As String) As String
        On Error GoTo ErrPart
        Dim SqlStr As String

        ''SELECT CLAUSE...

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_BARCODE_PRINT WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        For CntCount = 0 To 0
            SqlStr = "INSERT INTO TEMP_BARCODE_PRINT ( " & vbCrLf _
               & " USER_ID, MKEY, BARCODE_VALUE, PRINT_INVOICE_TYPE,PRINT_SEQ  ) VALUES (" & vbCrLf _
               & " '" & MainClass.AllowSingleQuote(PubUserID) & "','" & LblMKey.Text & "','',''," & CntCount & ")"

            PubDBCn.Execute(SqlStr)
        Next

        PubDBCn.CommitTrans()

        mSqlStr = " SELECT * "

        ''FROM CLAUSE...							
        mSqlStr = mSqlStr & vbCrLf _
            & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_SALE_DET ID, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST GMST, TEMP_BARCODE_PRINT BP "


        ''WHERE CLAUSE...							
        mSqlStr = mSqlStr & vbCrLf _
            & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.MKEY=BP.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.MKEY='" & LblMKey.Text & "'" & vbCrLf _
            & " AND BP.USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        ''ORDER CLAUSE...							

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY BP.PRINT_SEQ,BP.PRINT_INVOICE_TYPE,ID.SUBROWNO"

        'mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*, CMST.SUPP_CUST_NAME "

        '''FROM CLAUSE...
        'mSqlStr = mSqlStr & vbCrLf & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_SALE_DET ID, FIN_SUPP_CUST_MST CMST "

        '''WHERE CLAUSE...
        'mSqlStr = mSqlStr & vbCrLf _
        '    & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY " & vbCrLf _
        '    & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
        '    & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
        '    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        '    & " AND IH.MKEY='" & LblMKey.Text & "'" & vbCrLf _
        '    & " AND IH.BOOKTYPE='" & mBookType & "'" & vbCrLf _
        '    & " AND IH.BOOKSUBTYPE='" & mBookSubType & "' AND GOODS_SERVICE='" & lblGoodService.Text & "'"


        '''ORDER CLAUSE...
        'mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SUBROWNO"
        SelectQryForVoucher = mSqlStr
        Exit Function
ErrPart:

    End Function
    Private Sub cmdReCalculate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdReCalculate.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim CntCheckRow As Integer
        Dim mMainItemDesc As String
        Dim mCheckItemDesc As String
        Dim mOldRate As Double
        Dim mCheckRate As Double
        Dim mCheckQty As Double
        Dim mOriginalRate As Double
        With SprdPostingDetail
            For CntCheckRow = 1 To SprdPostingDetail.MaxRows
                .Row = CntCheckRow
                .Col = 2
                mMainItemDesc = Trim(.Text)
                If Trim(.Text) <> "" Then
                    .Row = CntCheckRow
                    .Col = 1
                    If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                        If MsgQuestion("You not Select the Item :  " & mMainItemDesc & " for Calculation, Are you want to Continue .. ? ") = CStr(MsgBoxResult.No) Then
                            Exit Sub
                        End If
                    End If
                End If
            Next
        End With
        CntCheckRow = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemDesc
                mMainItemDesc = Trim(.Text)
                .Col = ColRate
                mOriginalRate = Val(.Text)
                For CntCheckRow = 1 To SprdPostingDetail.MaxRows
                    SprdPostingDetail.Row = CntCheckRow
                    SprdPostingDetail.Col = 2
                    mCheckItemDesc = Trim(SprdPostingDetail.Text)
                    SprdPostingDetail.Col = 3
                    mOldRate = Val(SprdPostingDetail.Text)
                    SprdPostingDetail.Col = 4
                    mCheckQty = Val(SprdPostingDetail.Text)
                    SprdPostingDetail.Col = 5
                    mCheckRate = Val(SprdPostingDetail.Text)
                    If mMainItemDesc = mCheckItemDesc And mOriginalRate = mOldRate Then
                        SprdPostingDetail.Col = 1
                        If SprdPostingDetail.Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                            .Col = ColQty
                            .Text = CStr(0)
                        Else
                            '                        .Col = ColQty
                            '                        .Text = Format(mCheckQty, "0.00")
                            .Col = ColRate
                            .Text = VB6.Format(mCheckRate, "0.00")
                        End If
                        Exit For
                    End If
                Next
            Next
        End With
        Call CalcTots()
        Exit Sub
ErrPart:
        '    Resume
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
        If UpdateMain1("") = True Then
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
    Private Sub CmdSearchAmend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchAmend.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        If Val(txtPONo.Text) = 0 Then
            Exit Sub
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ORDER_TYPE='O'" & vbCrLf & " AND AUTO_KEY_SO=" & Val(txtPONo.Text) & ""
        If MainClass.SearchGridMaster("", "DSP_SALEORDER_HDR", "AMEND_NO", "CUST_AMEND_NO", "AMEND_WEF_FROM", , SqlStr) = True Then
            txtPOAmendNo.Text = AcName
            txtPONO_Validating(txtPONO, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdsearchPO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchPO.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mCustCode As String
        If Trim(txtSupplier.Text) = "" Then
            MsgInformation("Please Select Customer First")
            Exit Sub
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mCustCode = MasterNo
        Else
            MsgInformation("No Such Account in Account Master")
            Exit Sub
        End If
        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustCode) & "'" ''& vbCrLf |            & " AND ORDER_TYPE='O'"

        'If MainClass.SearchGridMasterBySQL((txtPONo.Text), "DSP_SALEORDER_HDR", "AUTO_KEY_SO", "AMEND_WEF_FROM AS WEF", "AMEND_NO", "CUST_PO_NO || '-' || CUST_AMEND_NO AS CUSTOMER_PONO", SqlStr) = True Then

        SqlStr = " SELECT AUTO_KEY_SO, AMEND_WEF_FROM AS , AMEND_NO, CUST_PO_NO || '-' || CUST_AMEND_NO AS CUSTOMER_PONO" & vbCrLf _
            & " FROM DSP_SALEORDER_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustCode) & "'"

        If MainClass.SearchGridMasterBySQL2((txtPONo.Text), SqlStr) = True Then
            txtPONo.Text = AcName
            txtPOAmendNo.Text = AcName1
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
        Dim mShowBillRate As Double
        Dim mItemDesc As String
        Dim mBillQty As Double
        Dim mOldBillRate As Double
        Dim mBillRate As Double
        Dim mBillAmount As Double
        FraPostingDtl.Visible = Not FraPostingDtl.Visible
        If FraPostingDtl.Visible = True Then
            MainClass.ClearGrid(SprdPostingDetail)
            FraPostingDtl.BringToFront()
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
                    SprdPostingDetail.Col = 3
                    mOldBillRate = Val(SprdPostingDetail.Text)
                    SprdPostingDetail.Col = 5
                    mBillRate = Val(SprdPostingDetail.Text)
                    If (mCheckItemDesc = mItemDesc) And (mCheckBillRate = mBillRate) Then ''And (mCheckBillRate = mBillRate) Then
                        GoTo NextRec
                    End If
                Next
                cntRowMain = 1
                For cntRowMain = 1 To SprdMain.MaxRows
                    SprdMain.Row = cntRowMain
                    SprdMain.Col = ColItemDesc
                    mShowItemDesc = Trim(SprdMain.Text)
                    SprdMain.Col = ColRate
                    mShowBillRate = Val(SprdMain.Text)
                    If mShowItemDesc = mCheckItemDesc And mShowBillRate = mCheckBillRate Then
                        SprdMain.Col = ColQty
                        mBillQty = mBillQty + Val(SprdMain.Text)
                        SprdMain.Col = ColRate
                        mBillRate = Val(SprdMain.Text)
                    End If
                Next
                SprdPostingDetail.Row = SprdPostingDetail.MaxRows
                SprdPostingDetail.Col = 1
                SprdPostingDetail.Value = IIf(mBillAmount > 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                SprdPostingDetail.Col = 2
                SprdPostingDetail.Text = mCheckItemDesc
                SprdPostingDetail.Col = 3
                SprdPostingDetail.Text = VB6.Format(mBillRate, "0.00")
                SprdPostingDetail.Col = 4
                SprdPostingDetail.Text = VB6.Format(mBillQty, "0.00")
                SprdPostingDetail.Col = 5
                SprdPostingDetail.Text = VB6.Format(mBillRate, "0.00")
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
            .set_ColWidth(2, 25)
            For I = 3 To 6
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMin = CDbl("-99999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(I, 7.5)
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
        Call GetPONOValidate()
        cboReason.Enabled = False
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
                .Col = 4
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
    Private Sub txtOBillDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOBillDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtOBillDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtOBillDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
        Call txtOBillNo_Validating(txtOBillNo, New System.ComponentModel.CancelEventArgs(False))
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtOBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOBillNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtOBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtOBillNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtOBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOBillDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtOBillNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOBillNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim mValue As String
        Dim mBillNo As String
        Dim RsTemp As ADODB.Recordset
        Dim mInvoiceSeq As Long
        Dim mPackingNo As Double
        '    txtOBillDate.Enabled = True
        If Trim(txtOBillDate.Text) = "" Then
            'MsgInformation("Please Select the Original Bill Date.")
            txtOBillDate.Enabled = True
            GoTo EventExitSub
        End If
        If Not IsDate(txtOBillDate.Text) Then
            'MsgInformation("Please Original Bill Date.")
            txtOBillDate.Enabled = True
            GoTo EventExitSub
        End If
        ''AND FYEAR=" & RsCompany.fields("FYEAR").value & "
        mSqlStr = " SELECT INVOICE_DATE, OUR_AUTO_KEY_SO, OUR_SO_DATE, " & vbCrLf _
            & " TRNTYPE, SUPP_CUST_CODE, ACCOUNTCODE, DIV_CODE, SAC_CODE, REMARKS, ITEMVALUE, INVOICESEQTYPE" & vbCrLf _
            & " FROM FIN_INVOICE_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND BILLNO='" & txtOBillNo.Text & "' " & vbCrLf _
            & " AND INVOICE_DATE=TO_DATE('" & VB6.Format(txtOBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then

            mInvoiceSeq = IIf(IsDBNull(RsTemp.Fields("INVOICESEQTYPE").Value), 0, RsTemp.Fields("INVOICESEQTYPE").Value)
            txtOBillDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")

            If mInvoiceSeq = 6 Then
                txtPONo.Text = ""
                mPackingNo = IIf(IsDBNull(RsTemp.Fields("OUR_AUTO_KEY_SO").Value), "", RsTemp.Fields("OUR_AUTO_KEY_SO").Value)
                If MainClass.ValidateWithMasterTable(mPackingNo, "AUTO_KEY_PACK", "AUTO_KEY_SO", "DSP_PACKING_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtPONo.Text = MasterNo
                End If
            Else
                txtPONo.Text = IIf(IsDBNull(RsTemp.Fields("OUR_AUTO_KEY_SO").Value), "", RsTemp.Fields("OUR_AUTO_KEY_SO").Value)
            End If

            txtPODate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
            txtPOAmendNo.Text = CStr(1)
            txtWEFDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
            txtToDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
            mValue = IIf(IsDBNull(RsTemp.Fields("TRNTYPE").Value), "-1", RsTemp.Fields("TRNTYPE").Value)
            If MainClass.ValidateWithMasterTable(mValue, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
                cboInvType.Text = MasterNo
            End If
            mValue = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "-1", RsTemp.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mValue, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtSupplier.Text = MasterNo
            End If
            mValue = IIf(IsDBNull(RsTemp.Fields("ACCOUNTCODE").Value), "-1", RsTemp.Fields("ACCOUNTCODE").Value)
            If MainClass.ValidateWithMasterTable(mValue, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtDebitAccount.Text = MasterNo
            End If
            mValue = IIf(IsDBNull(RsTemp.Fields("DIV_CODE").Value), "-1", RsTemp.Fields("DIV_CODE").Value)
            If MainClass.ValidateWithMasterTable(mValue, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                cboDivision.Text = MasterNo
            End If
            If lblGoodService.Text = "S" Or chkItemDetails.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                txtRemarks1.Text = IIf(IsDBNull(RsTemp.Fields("Remarks").Value), "", RsTemp.Fields("Remarks").Value)
                txtSACCode.Text = IIf(IsDBNull(RsTemp.Fields("SAC_CODE").Value), "", RsTemp.Fields("SAC_CODE").Value)
                txtTotItemValue.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEMVALUE").Value), 0, RsTemp.Fields("ITEMVALUE").Value), "0.00")
                lblTotItemValue.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEMVALUE").Value), 0, RsTemp.Fields("ITEMVALUE").Value), "0.00")
            End If
        Else
            MsgInformation("Please select valid Original Bill No & Date.")
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ErrPart:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtPartyDNNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyDNNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPartyDNNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartyDNNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartyDNNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPOAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPOAmendNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
        Dim mSupplierCode As String

        If Val(txtPONo.Text) = 0 Then GoTo EventExitSub
        If Len(txtPONo.Text) < 6 Then
            txtPONo.Text = Val(txtPONo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If
        mPONo = Val(txtPONo.Text)
        '    If Val(txtPOAmendNo.Text) = 0 Then Exit Sub

        mSupplierCode = ""
        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        End If

        SqlStr = "SELECT * FROM DSP_SALEORDER_HDR " & vbCrLf _
            & " WHERE AUTO_KEY_SO=" & Val(mPONo) & "" & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " ''& vbCrLf |            & " AND ORDER_TYPE='O'"

        If mSupplierCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & mSupplierCode & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPOMain.EOF = False Then
            Clear1()
            txtPONo.Text = IIf(IsDbNull(RsPOMain.Fields("AUTO_KEY_SO").Value), "", RsPOMain.Fields("AUTO_KEY_SO").Value)
            txtPODate.Text = VB6.Format(IIf(IsDbNull(RsPOMain.Fields("SO_DATE").Value), "", RsPOMain.Fields("SO_DATE").Value), "DD/MM/YYYY")
            txtWEFDate.Text = VB6.Format(IIf(IsDbNull(RsPOMain.Fields("AMEND_WEF_FROM").Value), "", RsPOMain.Fields("AMEND_WEF_FROM").Value), "DD/MM/YYYY")
            mSupplierCode = IIf(IsDBNull(RsPOMain.Fields("SUPP_CUST_CODE").Value), "", RsPOMain.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mSupplierCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = MasterNo
            End If
            txtSupplier.Text = mAccountName
            txtBillTo.Text = IIf(IsDBNull(RsPOMain.Fields("BILL_TO_LOC_ID").Value), "", RsPOMain.Fields("BILL_TO_LOC_ID").Value)        ''
        Else
            MsgBox("Invalid PO NO.", MsgBoxStyle.Information)
            Cancel = True
        End If
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
        If Val(txtPONo.Text) = 0 Then Exit Sub
        If Len(txtPONo.Text) < 6 Then
            txtPONo.Text = Val(txtPONo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If
        mPONo = Val(txtPONo.Text)
        SqlStr = "SELECT * FROM DSP_SALEORDER_HDR " & " WHERE AUTO_KEY_SO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " '' & vbCrLf |            & " AND ORDER_TYPE='O'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPOMain.EOF = False Then
            txtBillTo.Text = Trim(IIf(IsDBNull(RsPOMain.Fields("BILL_TO_LOC_ID").Value), "", RsPOMain.Fields("BILL_TO_LOC_ID").Value))
            If InsertIntoTemp() = False Then GoTo ERR1
            Call ShowPODetail1()
            Call FillSprdExp()
            FormatSprdMain(-1)
            Call CalcTots()
        Else
            MsgBox("Invalid PO No.", MsgBoxStyle.Information)
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
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mHSNCode As String
        Dim mPartyGSTNo As String
        Dim mLocal As String

        mLocal = GetPartyBusinessDetail(Trim(txtSupplier.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtSupplier.Text), Trim(txtBillTo.Text), "GST_RGN_NO")

        'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mLocal = Trim(MasterNo)
        'Else
        '    mLocal = "N"
        'End If
        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If
        SqlStr = ""
        SqlStr = " SELECT TRN.*, ITEM.ITEM_SHORT_DESC, ITEM.CUSTOMER_PART_NO " & vbCrLf _
            & " FROM TEMP_DNCN_TRN TRN, INV_ITEM_MST ITEM " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " TRN.USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf & " AND TRN.ITEM_CODE=ITEM.ITEM_CODE" & vbCrLf & " AND TRN.PORATE>0"
        SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.ITEM_CODE, INVOICE_DATE, BILLNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPODetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPODetail
            If .EOF = True Then Exit Sub
            I = 1
            Do While Not .EOF
                mItemCode = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Row = I
                SprdMain.Col = ColItemCode
                SprdMain.Text = mItemCode
                SprdMain.Col = ColItemDesc
                mItemDesc = Trim(IIf(IsDbNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value))
                SprdMain.Text = mItemDesc
                SprdMain.Col = ColPartNo
                mPartNo = Trim(IIf(IsDbNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value))
                SprdMain.Text = mPartNo
                SprdMain.Col = ColUnit
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value))
                SprdMain.Col = ColHSNCode
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("HSNCODE").Value), "", .Fields("HSNCODE").Value))
                mHSNCode = Trim(IIf(IsDbNull(.Fields("HSNCODE").Value), "", .Fields("HSNCODE").Value))
                If mHSNCode = "" Then
                    mHSNCode = GetHSNCode(mItemCode)
                End If


                SprdMain.Col = ColModel
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_MODEL").Value), "", .Fields("ITEM_MODEL").Value)

                SprdMain.Col = ColDrawingNo
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_DRAWINGNO").Value), "", .Fields("ITEM_DRAWINGNO").Value)

                SprdMain.Col = ColChargeableWidth
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("CHARGEABLE_WIDTH").Value), 0, .Fields("CHARGEABLE_WIDTH").Value), "0.000")

                SprdMain.Col = ColChargeableHeight
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("CHARGEABLE_HEIGHT").Value), 0, .Fields("CHARGEABLE_HEIGHT").Value), "0.000")

                ''ITEM_MODEL, ITEM_DRAWINGNO, CHARGEABLE_HEIGHT, CHARGEABLE_WIDTH

                SprdMain.Col = ColPURFYear
                SprdMain.Text = IIf(IsDbNull(.Fields("FYEAR").Value), "", .Fields("FYEAR").Value)
                SprdMain.Col = ColPURMkey
                SprdMain.Text = IIf(IsDbNull(.Fields("mKey").Value), "", .Fields("mKey").Value)
                SprdMain.Col = ColBillNo
                SprdMain.Text = IIf(IsDbNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                SprdMain.Col = ColBillDate
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                SprdMain.Col = ColInvoiceNo
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("AUTO_KEY_MRR").Value), 0, .Fields("AUTO_KEY_MRR").Value), "0")
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
                If cboReason.SelectedIndex = 0 Then
                    mRate = System.Math.Abs(mRate - IIf(IsDbNull(.Fields("PORATE").Value), 0, .Fields("PORATE").Value))
                Else
                    mRate = System.Math.Abs(mRate)
                End If
                SprdMain.Text = VB6.Format(mRate, "0.000")
                mAmount = mRate * mAcceptedQty
                SprdMain.Col = ColAmount
                SprdMain.Text = VB6.Format(mAmount, "0.000")
                '            If GetHSNDetails(mHSNCode, mCGSTAmount, mCGSTAmount, mCGSTAmount, mCGSTAmount, mCGSTAmount, mCGSTAmount) = False Then GoTo ERR1
                '            SprdMain.Col = ColCGSTPer
                '            SprdMain.Text = Format(mAmount, "0.000")
                If chkGSTApplicable.CheckState = System.Windows.Forms.CheckState.Checked Then
                    If GetHSNDetails(mHSNCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, "G", mPartyGSTNo) = False Then GoTo ERR1
                End If
                SprdMain.Row = I
                SprdMain.Col = ColHSNCode
                SprdMain.Text = Trim(mHSNCode)
                SprdMain.Col = ColCGSTPer
                SprdMain.Text = VB6.Format(mCGSTPer, "0.00")
                SprdMain.Col = ColSGSTPer
                SprdMain.Text = VB6.Format(mSGSTPer, "0.00")
                SprdMain.Col = ColIGSTPer
                SprdMain.Text = VB6.Format(mIGSTPer, "0.00")
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
        InsertIntoTemp = False
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mFYear = GetCurrentFYNo(PubDBCn, (txtToDate.Text))
        SqlStr = "DELETE FROM TEMP_DNCN_TRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        mSqlStr = " INSERT INTO TEMP_DNCN_TRN ( " & vbCrLf _
            & " USERID, MKEY, COMPANY_CODE, " & vbCrLf _
            & " FYEAR, ACCOUNTCODE_DR, ACCOUNTCODE_CR, " & vbCrLf _
            & " VNO, VDATE, BILLNO, " & vbCrLf _
            & " INVOICE_DATE, AUTO_KEY_MRR, MRRDATE, " & vbCrLf _
            & " CUST_REF_NO, ITEM_CODE, ITEM_UOM, ITEM_QTY, " & vbCrLf _
            & " ACCPETED, ITEM_RATE, DNCN_RATE, " & vbCrLf _
            & " SUPP_RATE, PORATE, HSNCODE,ITEM_MODEL, ITEM_DRAWINGNO, CHARGEABLE_HEIGHT, CHARGEABLE_WIDTH ) "

        SqlStr = ""
        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
            & " IH.MKEY, IH.COMPANY_CODE, IH.FYEAR, " & vbCrLf _
            & " IH.SUPP_CUST_CODE, IH.ACCOUNTCODE, " & vbCrLf _
            & " IH.BILLNO, IH.INVOICE_DATE, IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf _
            & " IH.AUTO_KEY_INVOICE, IH.INVOICE_DATE," & vbCrLf _
            & " '-1', ID.ITEM_CODE, ID.ITEM_UOM, "

        If cboReason.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " SUM(ID.ITEM_QTY) AS ITEM_QTY, " & vbCrLf _
                & " TO_CHAR(SUM(NVL(ID.ITEM_QTY,0))) AS ACCPETED, " & vbCrLf _
                & " MAX(ID.ITEM_RATE) AS ITEM_RATE, " & vbCrLf _
                & " NVL(GETSALEDEBITRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE),0) AS DNCN_RATE,  " & vbCrLf _
                & " NVL( GETSALESUPPBILLPRICE(IH.COMPANY_CODE,ID.ITEM_CODE, IH.SUPP_CUST_CODE,IH.AUTO_KEY_INVOICE),0) AS SUPP_RATE, " & vbCrLf _
                & " GetSORATE(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE) AS PORATE, ID.HSNCODE, ID.ITEM_MODEL,ID.ITEM_DRAWINGNO, ID.CHARGEABLE_HEIGHT, ID.CHARGEABLE_WIDTH"
        ElseIf cboReason.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf _
               & " ID.ITEM_QTY AS ITEM_QTY, " & vbCrLf _
               & " ID.ITEM_QTY AS ACCPETED, " & vbCrLf _
               & " ID.ITEM_RATE AS ITEM_RATE, " & vbCrLf & " 0 AS DNCN_RATE,  " & vbCrLf _
               & " 0 AS SUPP_RATE, " & vbCrLf _
               & " ID.ITEM_RATE AS PORATE, ID.HSNCODE, ID.ITEM_MODEL, ID.ITEM_DRAWINGNO, ID.CHARGEABLE_HEIGHT, ID.CHARGEABLE_WIDTH"
        Else
            SqlStr = SqlStr & vbCrLf _
                & " SUM(ID.ITEM_QTY) AS ITEM_QTY, " & vbCrLf _
                & " TO_CHAR(SUM(NVL(ID.ITEM_QTY,0))) AS ACCPETED, " & vbCrLf _
                & " MAX(ID.ITEM_RATE) AS ITEM_RATE, " & vbCrLf & " 0 AS DNCN_RATE,  " & vbCrLf _
                & " 0 AS SUPP_RATE, " & vbCrLf _
                & " MAX(ID.ITEM_RATE) AS PORATE, ID.HSNCODE, ID.ITEM_MODEL,ID.ITEM_DRAWINGNO, ID.CHARGEABLE_HEIGHT, ID.CHARGEABLE_WIDTH"
        End If

        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID "
        ''WHERE CLAUSE...''IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "

        If cboReason.SelectedIndex = 4 Then
            SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY"
        Else
            SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY(+)"
        End If


        If cboReason.SelectedIndex = 3 Or cboReason.SelectedIndex = 4 Then
        Else
            SqlStr = SqlStr & vbCrLf & " AND (OUR_AUTO_KEY_SO IS NOT NULL OR OUR_AUTO_KEY_SO<>'')"
        End If

        If cboReason.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND GetSORATE(IH.COMPANY_CODE,IH.INVOICE_DATE,IH.OUR_AUTO_KEY_SO,ID.ITEM_CODE) <>  " & vbCrLf & " (ID.ITEM_RATE -NVL(GETSALEDEBITRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE),0) " & vbCrLf & " + NVL( GETSALESUPPBILLPRICE(IH.COMPANY_CODE,ID.ITEM_CODE, IH.SUPP_CUST_CODE,IH.AUTO_KEY_INVOICE),0)) "
        End If
        ''NVL( GETSALESUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, ID.ITEM_CODE),0))
        ''TO_CHAR(GETSALESUPPBILLPRICE(IH.COMPANY_CODE, ID.ITEM_CODE, IH.SUPP_CUST_CODE,IH.AUTO_KEY_INVOICE))

        If cboReason.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.REF_DESP_TYPE='U' AND CANCELLED='N'"
        Else
            SqlStr = SqlStr & vbCrLf & "AND IH.REF_DESP_TYPE<>'U' AND CANCELLED='N'"
        End If

        If Trim(txtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                pSuppCustCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(UCase(pSuppCustCode)) & "'"
            End If
        End If

        If Trim(txtOBillNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BILLNO='" & Trim(txtOBillNo.Text) & "' AND IH.INVOICE_DATE=TO_DATE('" & VB6.Format(txtOBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtWEFDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        If cboReason.SelectedIndex = 3 Then
        Else
            SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf _
                & " IH.MKEY, IH.COMPANY_CODE, IH.FYEAR, " & vbCrLf _
                & " IH.SUPP_CUST_CODE, IH.ACCOUNTCODE, " & vbCrLf _
                & " IH.BILLNO, IH.INVOICE_DATE, IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf _
                & " IH.AUTO_KEY_INVOICE, IH.INVOICE_DATE," & vbCrLf _
                & " ID.ITEM_CODE, ID.ITEM_UOM, OUR_AUTO_KEY_SO, " & vbCrLf _
                & " ID.HSNCODE,ID.ITEM_MODEL,ID.ITEM_DRAWINGNO, ID.CHARGEABLE_HEIGHT, ID.CHARGEABLE_WIDTH "
        End If
        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.SUPP_CUST_CODE, IH.INVOICE_DATE, IH.AUTO_KEY_INVOICE "
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
        mSqlStr = "SELECT (NVL(ID.ITEM_PRICE,0)) AS GROSS_AMT " & vbCrLf & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AUTO_KEY_SO=" & Val(txtPONo.Text) & "" & vbCrLf & " AND IH.ORDER_TYPE='O'"
        If Trim(pItemCode) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
        End If
        If Trim(txtPOAmendNo.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.AMEND_NO=" & Val(txtPOAmendNo.Text) - 1 & ""
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
        SqlStr = " SELECT IH.AUTO_KEY_MRR,IH.MRRDATE, IH.MKEY, IH.FYEAR, VNO, IH.VDATE, BILLNO, IH.INVOICE_DATE, " & vbCrLf & " ITEM_QTY, " & vbCrLf & " NVL(ID.ITEM_QTY,0)-NVL(ID.SHORTAGE_QTY,0)-NVL(ID.REJECTED_QTY,0) As ACCPT_QTY," & vbCrLf & " ITEM_RATE - " & vbCrLf & " NVL(GETDNCNRATE(IH.COMPANY_CODE, IH.FYEAR, IH.SUPP_CUST_CODE, IH.BILLNO, IH.INVOICE_DATE, ID.ITEM_CODE,'R',ID.CUST_REF_NO),0) + " & vbCrLf & " NVL(GETSUPPRATE(IH.COMPANY_CODE, IH.FYEAR, IH.MKEY, IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, ID.ITEM_CODE,'R'),0) AS I_RATE " & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID " & vbCrLf & " Where " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf & " AND INVOICE_DATE>=TO_DATE('" & VB6.Format(txtWEFDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND INVOICE_DATE<=TO_DATE('" & VB6.Format(txtToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISFINALPOST='Y' AND CANCELLED='N' ORDER BY BILLNO, INVOICE_DATE"
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
        Dim xIName As String
        Dim SqlStr As String
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
        SprdMain.Row = SprdMain.ActiveRow
        Select Case eventArgs.Col
            Case ColQty
                '            If CheckQty() = True Then
                '                MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight
                '                FormatSprdMain SprdMain.MaxRows
                '            End If
            Case ColRate
                Call CheckRate()
        End Select
        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function CheckQty() As Boolean
        On Error GoTo ERR1

        CheckQty = True
        Exit Function

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
        Exit Sub
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
            .Row = eventArgs.row

            .Col = 1
            'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 And RsCompany.Fields("FYEAR").Value >= 2024 Then    ''Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104
            txtVNoPrefix.Text = .Text
            'Else
            '    txtVNoPrefix.Text = Mid(.Text, 1, 1)
            'End If

            .Col = 2
            If Trim(.Text) = "" Then
                cboInvType.SelectedIndex = -1
            Else
                cboInvType.Text = Trim(.Text)
            End If
            .Col = 3
            txtVNo.Text = Trim(.Text)
            '        .Col = 3
            '        txtVNo.Text = Format(.Text, "0000000")
            .Col = 5
            txtVDate.Text = VB6.Format(.Text, "DD/MM/YYYY")
            txtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))
            CmdView_Click(CmdView, New System.EventArgs())
        End With
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
    Private Sub txtSACCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSACCode.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSACCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSACCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSACCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSACCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtSACCode.Text) = "" Then GoTo EventExitSub
        If lblGoodService.Text = "S" Then
            If MainClass.ValidateWithMasterTable((txtSACCode.Text), "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = False Then
                MsgInformation("Invaild SAC Code.")
                Cancel = True
                GoTo EventExitSub
            End If
        Else
            If MainClass.ValidateWithMasterTable((txtSACCode.Text), "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G'") = False Then
                MsgInformation("Invaild HSN Code.")
                Cancel = True
                GoTo EventExitSub
            End If
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
        If Trim(txtVNo.Text) = "" Then GoTo EventExitSub

        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then ''Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104
        '    txtVNo.Text = VB6.Format(Val(txtVNo.Text), ConBillFormat)
        'Else
        txtVNo.Text = VB6.Format(Val(txtVNo.Text), "0000000")
        'End If


        If MODIFYMode = True And RsSuppPurchMain.EOF = False Then xMKey = RsSuppPurchMain.Fields("mKey").Value
        '  mBillNo = Trim(Trim(txtBillNoPrefix.Text) & vb6.Format(Val(mBillNoSeq), "00000000") & Trim(txtBillNoSuffix.Text))

        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then    ''Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104
        '    mVNo = Trim(txtVNoPrefix.Text) & VB6.Format(Val(txtVNo.Text), ConBillFormat)
        'Else

        'End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 And RsCompany.Fields("FYEAR").Value >= 2024 Then    ''Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104
            mVNo = Trim(txtVNoPrefix.Text) & VB6.Format(Val(txtVNo.Text), "00000")
        Else
            mVNo = Trim(txtVNoPrefix.Text) & VB6.Format(Val(txtVNo.Text), "0000000")
        End If


        SqlStr = " SELECT * FROM FIN_SUPP_SALE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND VNo='" & MainClass.AllowSingleQuote(mVNo) & "' " & vbCrLf & " AND BookType='" & mBookType & "' AND GOODS_SERVICE='" & lblGoodService.Text & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsSuppPurchMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Voucher, Use Generate Voucher Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM FIN_SUPP_SALE_HDR " & " WHERE Mkey='" & MainClass.AllowSingleQuote(xMKey) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Public Function AutoCreditNoteNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsMRRMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String
        Dim mPreFix As String
        Dim mPrefixLen As Long

        mPreFix = GetDocumentPrefix("P", IIf(LblBookCode.Text = ConSaleCreditBookCode, "M", "R"), cboDivision.Text)     ''GetDocumentPrefix("P", "R")

        'GetDocumentPrefix("P", IIf(LblBookCode.Text = ConSaleCreditBookCode, "M", "R"), cboDivision.Text)

        mPrefixLen = IIf(Trim(mPreFix) = "", 0, Len(Trim(mPreFix)))
        SqlStr = ""
        ''select BILLNO, NVL(LENGTH(BILLNOPREFIX),0), LENGTH(BILLNO),SUBSTR(REJ_CREDITNOTE,NVL(LENGTH(BILLNOPREFIX),0)+1,LENGTH(REJ_CREDITNOTE)-NVL(LENGTH(BILLNOPREFIX),0)),

        SqlStr = "SELECT MAX(MaxNo)  AS MaxNo FROM ("

        SqlStr = SqlStr & vbCrLf _
            & " Select Max(TO_NUMBER(SUBSTR(REJ_CREDITNOTE," & mPrefixLen + 1 & ",LENGTH(REJ_CREDITNOTE)-" & mPrefixLen & "))) As MaxNo " & vbCrLf _
            & " FROM FIN_PURCHASE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " And FYEAR =" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " And PURCHASESEQTYPE=2"

        If mPreFix <> "" Then
            SqlStr = SqlStr & vbCrLf & " And SUBSTR(REJ_CREDITNOTE,1," & mPrefixLen & ")='" & mPreFix & "'"
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 103 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then

            SqlStr = SqlStr & vbCrLf & " UNION ALL"

            SqlStr = SqlStr & vbCrLf _
                & "SELECT Max(TO_NUMBER(SUBSTR(PARTY_DNCN_NO," & mPrefixLen + 1 & ",LENGTH(PARTY_DNCN_NO)-" & mPrefixLen & "))) AS MaxNo " & vbCrLf _
                & " FROM FIN_SUPP_SALE_HDR " & vbCrLf _
                & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND FYEAR =" & RsCompany.Fields("FYEAR").Value & ""

            If mPreFix <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND SUBSTR(PARTY_DNCN_NO,1," & mPrefixLen & ")='" & mPreFix & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " )"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMRRMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = mMaxValue
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            AutoCreditNoteNo = mPreFix & VB6.Format(mNewSeqNo, ConBillFormat)   '' mStartingSNo = CDbl(VB6.Format(pStartingSNo, ConBillFormat))
        ElseIf RsCompany.Fields("ERP_CUSTOMER_ID").Value = 103 Then
            AutoCreditNoteNo = mPreFix & VB6.Format(mNewSeqNo, "0000")
        Else
            AutoCreditNoteNo = mPreFix & mNewSeqNo
        End If


        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateMain1(pType As String) As Boolean
        On Error GoTo ErrPart
        Dim I As Short
        Dim SqlStr As String
        Dim nMkey As String
        Dim mTRNType As String
        Dim mVNoSeq As Double
        Dim mVNo As String
        Dim mSuppCustCode As String
        Dim mModvatSuppCode As String
        Dim mAccountCode As String
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
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim mItemDetails As String

        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        'lblGoodService.Text = "G"
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
        ''chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked
        mFinalPost = IIf(chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mItemDetails = IIf(chkItemDetails.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mSuppCustCode = CStr(-1)
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = CStr(-1)
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If
        '*********
        If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        Else
            mAccountCode = "-1"
            MsgBox("Credit Account Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If
        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
            mBookSubType = MasterNo
        Else
            mBookSubType = CStr(-1)
        End If
        mItemValue = Val(lblTotItemValue.Text)
        mTOTSTAMT = 0
        mTOTCHARGES = 0
        mTotEDAmount = 0
        mTOTEXPAMT = Val(lblTotExpAmt.Text)
        mNETVALUE = Val(lblNetAmount.Text)
        mSTPERCENT = 0
        mTOTFREIGHT = 0
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
        mIsSuppBill = "N"
        mSTType = "0"
        If Val(txtVno.Text) = 0 Then
            mVNoSeq = AutoGenSeqBillNo()
        Else
            mVNoSeq = Val(txtVno.Text)
        End If

        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then    ''Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104
        '    ' mStartingSNo = CDbl(VB6.Format(pStartingSNo, ConBillFormat))
        '    txtVNo.Text = VB6.Format(Val(CStr(mVNoSeq)), ConBillFormat)
        '    mVNo = Trim(txtVNoPrefix.Text) & VB6.Format(Val(CStr(mVNoSeq)), ConBillFormat)
        'Else
        txtVNo.Text = VB6.Format(Val(CStr(mVNoSeq)), "0000000")
        ''mVNo = Trim(txtVNoPrefix.Text) & VB6.Format(Val(CStr(mVNoSeq)), "0000000")
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 And RsCompany.Fields("FYEAR").Value >= 2024 Then    ''Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104
            mVNo = Trim(txtVNoPrefix.Text) & VB6.Format(Val(CStr(mVNoSeq)), "00000")
        Else
            mVNo = Trim(txtVNoPrefix.Text) & VB6.Format(Val(CStr(mVNoSeq)), "0000000")
        End If


        pJVVnoStr = ""
        pJVMKey = ""
        'If Trim(txtNarration.Text) = "" Then
        '    txtNarration.Text = "Rates Revised wide PO NO " & txtPONo.Text & "/" & txtPOAmendNo.Text & " WEF. " & VB6.Format(txtWEFDate.Text, "DD/MM/YYYY") & " Till Bill Date " & VB6.Format(txtToDate.Text, "DD/MM/YYYY")
        'End If
        If chkGSTApplicable.CheckState = System.Windows.Forms.CheckState.Checked Then
            If Trim(txtPartyDNNo.Text) = "" And mFinalPost = "Y" And (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 103 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104) Then
                txtPartyDNNo.Text = AutoCreditNoteNo()
                txtPartyDNDate.Text = txtVDate.Text
                txtRecdDate.Text = txtVDate.Text
            End If
        End If

        If MODIFYMode = True Then
            If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_SALE_HDR", LblMKey.Text, RsSuppPurchMain, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_SALE_DET", LblMKey.Text, RsSuppPurchDetail, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_SALE_EXP", LblMKey.Text, RsSuppPurchExp, "MKEY", "M") = False Then GoTo ErrPart
        End If

        SqlStr = ""
        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("FIN_SUPP_PUR_HDR", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo
            LblMKey.Text = nMkey

            SqlStr = "INSERT INTO FIN_SUPP_SALE_HDR( " & vbCrLf & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf _
                & " ROWNO, TRNTYPE, VNOPREFIX, VNOSEQ, " & vbCrLf & " VNO, VDATE, BILLNO, " & vbCrLf & " INVOICE_DATE, AUTO_KEY_SO, SO_DATE, " & vbCrLf & " AMEND_NO, SO_WEFDATE, SUPP_CUST_CODE, " & vbCrLf & " ACCOUNTCODE, TARIFFHEADING, BOOKTYPE, " & vbCrLf & " BOOKSUBTYPE, REMARKS, ITEMDESC, " & vbCrLf & " ITEMVALUE, STPERCENT, TOTSTAMT, " & vbCrLf & " TOTFREIGHT, TOTCHARGES, EDPERCENT, " & vbCrLf & " TOTEDAMOUNT, TOTSURCHARGEAMT, TOTDISCAMOUNT, " & vbCrLf & " TOTMSCAMOUNT, TOTRO, TOTEXPAMT, " & vbCrLf & " TOTTAXABLEAMOUNT, NETVALUE, TOTQTY, " & vbCrLf & " STTYPE, STFORMCODE, STFORMNAME, "
            SqlStr = SqlStr & vbCrLf & " STFORMDATE, STDUEFORMCODE, STDUEFORMNAME, " & vbCrLf & " STDUEFORMDATE, ISREGDNO, LSTCST, " & vbCrLf & " WITHFORM, CANCELLED, NARRATION, " & vbCrLf & " JVNO, JVMKEY, ISFINALPOST, " & vbCrLf & " PAYMENTDATE, TOTEDUPERCENT, TOTEDUAMOUNT, " & vbCrLf & " CESSABLEAMOUNT, TO_DATE, SHECPERCENT, " & vbCrLf & " SHECAMOUNT, " & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE, DIV_CODE, " & vbCrLf & " TOTCGST_AMOUNT, TOTSGST_AMOUNT, TOTIGST_AMOUNT, GST_APP, REASON, PARTY_DNCN_NO, PARTY_DNCN_DATE, PARTY_DNCN_RECDDATE, GOODS_SERVICE, SAC_CODE, O_BILLNO, O_INVOICE_DATE, TOTCGST_PER, TOTSGST_PER, TOTIGST_PER, BILL_TO_LOC_ID, IS_ITEMDETAIL "

            SqlStr = SqlStr & vbCrLf & " ) VALUES ( " & vbCrLf _
                & " '" & nMkey & "', " & RsCompany.Fields("Company_Code").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                & " " & mCurRowNo & ", " & Val(mTRNType) & ", '" & MainClass.AllowSingleQuote(txtVNoPrefix.Text) & "', " & mVNoSeq & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mVNo) & "', TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtPONo.Text) & ", TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & "  " & Val(txtPOAmendNo.Text) & ", TO_DATE('" & VB6.Format(txtWEFDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mSuppCustCode & "'," & vbCrLf & "  '" & mAccountCode & "', '" & MainClass.AllowSingleQuote(txtTariff.Text) & "', '" & mBookType & "'," & vbCrLf & "  '" & mBookSubType & "', '" & MainClass.AllowSingleQuote(IIf(lblGoodService.Text = "G" And chkItemDetails.CheckState = System.Windows.Forms.CheckState.Checked, txtRemarks.Text, txtRemarks1.Text)) & "', '" & MainClass.AllowSingleQuote(txtItemType.Text) & "'," & vbCrLf & "  " & mItemValue & ", " & mSTPERCENT & ", " & mTOTSTAMT & "," & vbCrLf & "  " & mTOTFREIGHT & ", " & mTOTCHARGES & ", " & mEDPERCENT & ", " & vbCrLf & "  " & mTotEDAmount & ", " & mSURAmount & ", " & mTotDiscount & "," & vbCrLf & "  " & mMSC & ", " & mRO & ", " & mTOTEXPAMT & "," & vbCrLf & "  " & mTOTTAXABLEAMOUNT & ", " & mNETVALUE & ", " & mTotQty & "," & vbCrLf & "  '" & mSTType & "', " & mFormRecdCode & ", '',"

            SqlStr = SqlStr & vbCrLf & "  '', " & mFormDueCode & ",'', " & vbCrLf & " '', '" & mIsRegdNo & "', '" & mLSTCST & "'," & vbCrLf & " '" & mWITHFORM & "', '" & mCancelled & "', '" & MainClass.AllowSingleQuote(txtNarration.Text) & "'," & vbCrLf & " '" & pJVVnoStr & "', '" & pJVMKey & "', '" & mFinalPost & "'," & vbCrLf & " TO_DATE('" & VB6.Format(txtPaymentdate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ,0,0,  " & vbCrLf & " 0, TO_DATE('" & VB6.Format(txtToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), 0," & vbCrLf & " 0, " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf & " '', '', " & mDivisionCode & "," & vbCrLf & " " & Val(lblTotCGST.Text) & ", " & Val(lblTotSGST.Text) & "," & Val(lblTotIGST.Text) & ", " & vbCrLf & " '" & IIf(chkGSTApplicable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "','" & VB.Left(cboReason.Text, 1) & "','" & MainClass.AllowSingleQuote(txtPartyDNNo.Text) & "', TO_DATE('" & VB6.Format(txtPartyDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtRecdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ,'" & lblGoodService.Text & "', '" & MainClass.AllowSingleQuote(txtSACCode.Text) & "', '" & txtOBillNo.Text & "', TO_DATE('" & VB6.Format(txtOBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(lblCGSTPer.Text) & ", " & Val(lblSGSTPer.Text) & ", " & Val(lblIGSTPer.Text) & ",'" & Trim(txtBillTo.Text) & "','" & mItemDetails & "')"
        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE FIN_SUPP_SALE_HDR SET " & vbCrLf & " TRNTYPE= " & Val(mTRNType) & "," & vbCrLf _
                & " VNOPREFIX='" & MainClass.AllowSingleQuote(txtVNoPrefix.Text) & "', VNOSEQ= " & mVNoSeq & "," & vbCrLf & " VNO= '" & MainClass.AllowSingleQuote(mVNo) & "'," & vbCrLf & " VDATE= TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " BILLNO= '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "'," & vbCrLf & " INVOICE_DATE= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " AUTO_KEY_SO= " & Val(txtPONo.Text) & "," & vbCrLf & " SO_DATE= TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " AMEND_NO= " & Val(txtPOAmendNo.Text) & ", SO_WEFDATE=TO_DATE('" & VB6.Format(txtWEFDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),"

            SqlStr = SqlStr & vbCrLf & " SUPP_CUST_CODE= '" & mSuppCustCode & "'," & vbCrLf & " ACCOUNTCODE= '" & mAccountCode & "'," & vbCrLf & " TARIFFHEADING= '" & MainClass.AllowSingleQuote(txtTariff.Text) & "'," & vbCrLf & " BOOKTYPE= '" & mBookType & "'," & vbCrLf & " BOOKSUBTYPE= '" & mBookSubType & "'," & vbCrLf & " REMARKS= '" & MainClass.AllowSingleQuote(IIf(lblGoodService.Text = "G" And chkItemDetails.CheckState = System.Windows.Forms.CheckState.Checked, txtRemarks.Text, txtRemarks1.Text)) & "'," & vbCrLf & " ITEMDESC= '" & MainClass.AllowSingleQuote(txtItemType.Text) & "'," & vbCrLf & " ITEMVALUE= " & mItemValue & "," & vbCrLf & " STPERCENT= " & mSTPERCENT & "," & vbCrLf & " TOTSTAMT= " & mTOTSTAMT & "," & vbCrLf & " TOTFREIGHT= " & mTOTFREIGHT & "," & vbCrLf & " TOTCHARGES= " & mTOTCHARGES & ", GOODS_SERVICE='" & lblGoodService.Text & "', SAC_CODE='" & MainClass.AllowSingleQuote(txtSACCode.Text) & "', "
            SqlStr = SqlStr & vbCrLf & " EDPERCENT= " & mEDPERCENT & "," & vbCrLf & " TOTEDAMOUNT= " & mTotEDAmount & "," & vbCrLf & " TOTSURCHARGEAMT=" & mSURAmount & "," & vbCrLf & " TOTDISCAMOUNT= " & mTotDiscount & "," & vbCrLf & " TOTMSCAMOUNT= " & mMSC & "," & vbCrLf & " TOTRO= " & mRO & "," & vbCrLf & " TOTEXPAMT= " & mTOTEXPAMT & "," & vbCrLf & " TOTTAXABLEAMOUNT= " & mTOTTAXABLEAMOUNT & "," & vbCrLf & " NETVALUE=" & mNETVALUE & "," & vbCrLf & " TOTQTY= " & mTotQty & ", TOTCGST_PER=" & Val(lblCGSTPer.Text) & ", TOTSGST_PER=" & Val(lblSGSTPer.Text) & ", TOTIGST_PER=" & Val(lblIGSTPer.Text) & ","
            '
            SqlStr = SqlStr & vbCrLf & " STTYPE= '" & mSTType & "'," & vbCrLf & " STFORMCODE= " & mFormRecdCode & "," & vbCrLf & " STFORMNAME= ''," & vbCrLf & " STFORMNO= ''," & vbCrLf & " STFORMDATE= ''," & vbCrLf & " STDUEFORMCODE= " & mFormDueCode & "," & vbCrLf & " STDUEFORMNAME= ''," & vbCrLf & " STDUEFORMNO= ''," & vbCrLf & " STDUEFORMDATE= ''," & vbCrLf & " ISREGDNO= '" & mIsRegdNo & "'," & vbCrLf & " LSTCST= '" & mLSTCST & "'," & vbCrLf & " WITHFORM= '" & mWITHFORM & "'," & vbCrLf & " CANCELLED= '" & mCancelled & "'," & vbCrLf & " NARRATION= '" & MainClass.AllowSingleQuote(txtNarration.Text) & "', O_BILLNO = '" & txtOBillNo.Text & "', O_INVOICE_DATE = TO_DATE('" & VB6.Format(txtOBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), "
            SqlStr = SqlStr & vbCrLf & " ISFINALPOST= '" & mFinalPost & "'," & vbCrLf & " PAYMENTDATE= TO_DATE('" & VB6.Format(txtPaymentdate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & vbCrLf & " TOTEDUPERCENT= 0," & vbCrLf & " TOTEDUAMOUNT= 0," & vbCrLf & " CESSABLEAMOUNT= 0," & vbCrLf & " TO_DATE=TO_DATE('" & VB6.Format(txtToDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " JVNO='" & pJVVnoStr & "', " & vbCrLf & " JVMKEY='" & pJVMKey & "'," & vbCrLf & " SHECPERCENT=0," & vbCrLf & " PARTY_DNCN_NO='" & MainClass.AllowSingleQuote(txtPartyDNNo.Text) & "', " & vbCrLf & " PARTY_DNCN_DATE=TO_DATE('" & VB6.Format(txtPartyDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " PARTY_DNCN_RECDDATE=TO_DATE('" & VB6.Format(txtRecdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " SHECAMOUNT=0, DIV_CODE=" & mDivisionCode & "," & vbCrLf & " TOTCGST_AMOUNT=" & Val(lblTotCGST.Text) & ", " & vbCrLf & " TOTSGST_AMOUNT=" & Val(lblTotSGST.Text) & ", " & vbCrLf & " TOTIGST_AMOUNT=" & Val(lblTotIGST.Text) & ", GST_APP='" & IIf(chkGSTApplicable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "',REASON= '" & VB.Left(cboReason.Text, 1) & "',"
            SqlStr = SqlStr & vbCrLf & " IS_ITEMDETAIL='" & mItemDetails & "',BILL_TO_LOC_ID='" & Trim(txtBillTo.Text) & "',ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"
        End If

        If pType = "" Then
            PubDBCn.Execute(SqlStr)
            'If chkItemDetails.CheckState = System.Windows.Forms.CheckState.Checked Then
            If UpdateDetail1(mNarration, mVNo, mSuppCustCode, mAccountCode, mDivisionCode) = False Then GoTo ErrPart
            'Else
            '    If UpdateExp1() = False Then GoTo ErrPart
            'End If
        End If

        If UpdatePaymentDetail1(mSuppCustCode, mDivisionCode) = False Then GoTo ErrPart


        'mVNo, txtVDate.Text,
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                Dim mMannualAdjustment As String
                Dim mRow As Long
                Dim mBalanceAmount As Double
                Dim mItemCGST As Double
                Dim mItemSGST As Double
                Dim mItemIGST As Double
                Dim mNetExpAmount As Double
                Dim xNETVALUE As Double
                Dim mFirstRow As Boolean

                mMannualAdjustment = IIf(IsDBNull(RsCompany.Fields("MANNUAL_BILL_ADJUST").Value), "N", RsCompany.Fields("MANNUAL_BILL_ADJUST").Value)
                mRow = SprdPaymentDetail.MaxRows
                mBalanceAmount = Val(lblNetAmount.Text)

                If mMannualAdjustment = "Y" Then
                    If mCompanyGSTNo = mPartyGSTNo Then
                        mNetExpAmount = Val(lblTotExpAmt.Text)
                        mItemCGST = 0
                        mItemSGST = 0
                        mItemIGST = 0
                    Else
                        mItemCGST = Val(lblTotCGST.Text)
                        mItemSGST = Val(lblTotSGST.Text)
                        mItemIGST = Val(lblTotIGST.Text)

                        'If VB.Left(cboGSTStatus.Text, 1) = "I" Then     ''VB.Left(cboGSTStatus.Text, 1) = "G" Or 
                        '    mNetExpAmount = Val(lblTotExpAmt.Text) + Val(lblTotCGSTAmount.Text) + Val(lblTotIGSTAmount.Text) + Val(lblTotSGSTAmount.Text)
                        'Else
                        mNetExpAmount = Val(lblTotExpAmt.Text)
                        'End If
                    End If

                    mFirstRow = True

                    With SprdPaymentDetail
                        For mRow = 1 To SprdPaymentDetail.MaxRows - 1
                            .Row = mRow

                            .Col = ColPayBillNo
                            mSRBillNo = Trim(.Text)

                            .Col = ColPayBillDate
                            mSRBillDate = Trim(.Text)

                            .Col = ColPayPaymentAmt
                            xNETVALUE = Val(.Text)
                            mBalanceAmount = mBalanceAmount - xNETVALUE

                            If SalePostTRN_GST(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mVNo, (txtVDate.Text),
                                               mTRNType, mSuppCustCode, mAccountCode, Val(CStr(xNETVALUE)), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False),
                                               pDueDate, False, IIf(lblGoodService.Text = "G", txtRemarks.Text, txtRemarks1.Text), False, "", 0, 0, Val(lblTotCGST.Text), Val(lblTotIGST.Text),
                                               Val(lblTotSGST.Text), ADDMode, mAddUser, mAddDate, Val(CStr(mItemValue)), mDivisionCode, CStr(0), 0, 0, 0, txtBillTo.Text, Trim(mSRBillNo),
                                              mSRBillDate, mFirstRow, IIf(chkItemDetails.CheckState = System.Windows.Forms.CheckState.Checked, False, True), 0, txtNarration.Text) = False Then GoTo ErrPart

                            mFirstRow = False
                        Next
                    End With
                    If mBalanceAmount <> 0 Then
                        xNETVALUE = mBalanceAmount
                        mSRBillNo = mVNo
                        mSRBillDate = txtVDate.Text

                        If SalePostTRN_GST(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mVNo, (txtVDate.Text),
                                               mTRNType, mSuppCustCode, mAccountCode, Val(CStr(xNETVALUE)), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False),
                                               pDueDate, False, IIf(lblGoodService.Text = "G", txtRemarks.Text, txtRemarks1.Text), False, "", 0, 0, Val(lblTotCGST.Text), Val(lblTotIGST.Text),
                                               Val(lblTotSGST.Text), ADDMode, mAddUser, mAddDate, Val(CStr(mItemValue)), mDivisionCode, CStr(0), 0, 0, 0, txtBillTo.Text, Trim(mSRBillNo),
                                               mSRBillDate, mFirstRow, IIf(chkItemDetails.CheckState = System.Windows.Forms.CheckState.Checked, False, True), Val(lblOthersAmount.Text), txtNarration.Text) = False Then GoTo ErrPart

                    End If
                Else
                    mFirstRow = True
                    If SalePostTRN_GST(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mVNo, (txtVDate.Text),
                                               mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mNETVALUE)), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False),
                                               pDueDate, False, IIf(lblGoodService.Text = "G", txtRemarks.Text, txtRemarks1.Text), False, "", 0, 0, Val(lblTotCGST.Text), Val(lblTotIGST.Text),
                                               Val(lblTotSGST.Text), ADDMode, mAddUser, mAddDate, Val(CStr(mItemValue)), mDivisionCode, CStr(0), 0, 0, 0, txtBillTo.Text, Trim(txtBillNo.Text),
                                               txtBillDate.Text, mFirstRow, IIf(chkItemDetails.CheckState = System.Windows.Forms.CheckState.Checked, False, True), Val(lblOthersAmount.Text), txtNarration.Text) = False Then GoTo ErrPart

                End If
            End If
        End If

        If VB.Left(cboReason.Text, 1) = 6 Then

            SqlStr = "UPDATE INV_MISC_GATE_HDR SET " & vbCrLf _
               & " IS_OUT='Y', IS_REVERSED='Y', OUT_DATE= TO_DATE('" & VB6.Format(txtVDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
               & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
               & " WHERE BILL_NO ='" & MainClass.AllowSingleQuote(Trim(txtBillNo.Text)) & "' AND IS_OUT='N'"

            PubDBCn.Execute(SqlStr)
        End If

        '' Val(lblTotExpAmt.text)
        PubDBCn.CommitTrans()
        UpdateMain1 = True
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsSuppPurchMain.Requery() ''.Refresh
        RsSuppPurchDetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        'If Err.Number = -2147217900 Then
        '    ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        'Else
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'End If
        '    Resume
    End Function
    Private Function AutoGenSeqBillNo() As String
        On Error GoTo AutoGenSeqBillNoErr
        Dim RsSuppPurchMainGen As ADODB.Recordset
        Dim mNewSeqBillNo As Double
        Dim SqlStr As String
        Dim mStartingSNo As Double
        Dim pStartingSNo As Double
        Dim xFYear As Integer
        Dim mMAxNo As Double
        Dim mMonth As String

        SqlStr = ""

        ''
        Dim mStartMonth As String
        Dim mEndMonth As String


        pStartingSNo = 1
        If RsCompany.Fields("FYEAR").Value >= 2024 And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            mMonth = VB6.Format(txtVDate.Text, "YYMM")
            mStartMonth = "01/" & VB6.Format(txtVDate.Text, "MM/YYYY")
            mEndMonth = MainClass.LastDay(Month(txtVDate.Text), Year(txtVDate.Text)) & "/" & VB6.Format(txtVDate.Text, "MM/YYYY")

            xFYear = CInt(VB6.Format(RsCompany.Fields("START_DATE").Value, "YY"))

            'mStartingSNo = CDbl(xFYear & VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & VB6.Format(pStartingSNo, "0000"))
            mStartingSNo = CDbl(mMonth & VB6.Format(pStartingSNo, "0000"))

        Else
            xFYear = CInt(VB6.Format(RsCompany.Fields("START_DATE").Value, "YY"))

            mStartingSNo = CDbl(xFYear & VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & VB6.Format(pStartingSNo, "00000"))

        End If



        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then    ''Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104
        '    mStartingSNo = 1
        'Else
        'End If

        SqlStr = "SELECT Max(VNOSEQ)  FROM FIN_SUPP_SALE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'"

        If RsCompany.Fields("FYEAR").Value >= 2024 And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            SqlStr = SqlStr & vbCrLf _
                & " AND VNOPREFIX='" & txtVNoPrefix.Text & "' " & vbCrLf _
                & " AND VDATE>=TO_DATE('" & VB6.Format(mStartMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND VDATE<=TO_DATE('" & VB6.Format(mEndMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        ''SqlStr = SqlStr & vbCrLf & " AND VDATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSuppPurchMainGen
            If .EOF = False Then
                mMAxNo = IIf(IsDBNull(.Fields(0).Value), -1, .Fields(0).Value)
                If mMAxNo = -1 Then
                    mNewSeqBillNo = mStartingSNo
                ElseIf Not IsDBNull(.Fields(0).Value) Then
                    mNewSeqBillNo = .Fields(0).Value + 1
                Else
                    mNewSeqBillNo = mStartingSNo
                End If
            Else
                mNewSeqBillNo = mStartingSNo
            End If
        End With
        AutoGenSeqBillNo = CStr(mNewSeqBillNo)
        Exit Function
AutoGenSeqBillNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateDetail1(ByRef xNarration As String, ByRef pVNo As String, ByRef pSuppCustCode As String, ByRef mDebitAccountCode As String, ByRef mDivisionCode As Double) As Boolean
        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String
        Dim I As Integer
        Dim mItemCode As String
        Dim mPartNo As String
        Dim mItemDesc As String
        Dim mUnit As String
        Dim mExicseableAmt As Double
        Dim mCessableAmt As Double
        Dim mSTableAmt As Double
        Dim mCESSAmt As Double
        Dim mTotExicseableAmt As Double
        Dim mTotSTableAmt As Double
        Dim mTotCessableAmt As Double
        Dim mServiceAmt As Double
        Dim mHSNCode As String
        Dim mPurFYear As Integer
        Dim mPurMkey As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillQty As Double
        Dim mBillRate As Double
        Dim mPORate As Double
        Dim mQty As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mInvoiceNo As String
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mStockType As String
        Dim mDespNoteNo As Double = -1

        Dim mModelNo As String
        Dim mDrawingNo As String
        Dim mHeight As Double
        Dim mWidth As Double
        Dim mTaxableAmount As Double

        mTotExicseableAmt = 0
        mTotSTableAmt = 0
        mTotCessableAmt = 0
        PubDBCn.Execute("Delete From FIN_POSTED_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & mBookType & "' AND BOOKCODE='" & LblBookCode.Text & "'")
        PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & mBookType & "' AND BOOKCODE='" & LblBookCode.Text & "'")
        PubDBCn.Execute("Delete From FIN_SUPP_SALE_DET Where Mkey='" & LblMKey.Text & "'")

        If VB.Left(cboReason.Text, 1) = 6 Then
            If DeleteStockTRN(PubDBCn, ConStockRefType_DSP, (LblMKey.Text)) = False Then GoTo UpdateDetail1
        End If


        If chkItemDetails.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            'If UpdateGSTTRN(PubDBCn, (LblMKey.Text), (LblBookCode.Text), mBookType, mBookSubType, pVNo, VB6.Format(txtVDate.Text, "DD-MMM-YYYY"), Trim(txtBillNo.Text), VB6.Format(txtBillDate.Text, "DD-MMM-YYYY"), mBillNo, VB6.Format(mBillDate, "DD-MMM-YYYY"), pSuppCustCode, mDebitAccountCode, "Y", pSuppCustCode, I, mItemCode, mQty, mUnit, mRate, mAmount, mAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, mDivisionCode, mHSNCode, mItemDesc, "", "N", mBookType, "G", "N", "D", VB6.Format(txtBillDate.Text, "DD-MMM-YYYY"), "N") = False Then GoTo UpdateDetail1
            UpdateDetail1 = True
            UpdateDetail1 = UpdateExp1()
            Exit Function
        End If

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
                SprdMain.Col = ColHSNCode
                mHSNCode = MainClass.AllowSingleQuote(.Text)
                .Col = ColPURFYear
                mPurFYear = Val(.Text)
                mPurFYear = IIf(mPurFYear <= 0, 2021, mPurFYear)
                .Col = ColPURMkey
                mPurMkey = MainClass.AllowSingleQuote(.Text)
                mPurMkey = IIf(mPurMkey = "", -1, mPurMkey)
                .Col = ColBillNo
                mBillNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColTaxableAmount
                mTaxableAmount = Val(.Text)

                mStockType = "ST"

                If MainClass.ValidateWithMasterTable(mBillNo, "BILLNO", "AUTO_KEY_DESP", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "") = True Then
                    mDespNoteNo = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(mDespNoteNo, "AUTO_KEY_DESP", "STOCK_TYPE", "DSP_DESPATCH_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowDoubleQuote(mItemCode) & "'") = True Then
                    mStockType = MasterNo
                End If

                .Col = ColBillDate
                mBillDate = Trim(.Text)          '' IIf(Trim(.Text) = "", "", VB6.Format(.Text, "DD/MM/YYYY"))
                .Col = ColInvoiceNo
                mInvoiceNo = Trim(.Text)
                mInvoiceNo = IIf(Val(mInvoiceNo) <= 0, -1, mInvoiceNo)
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

                .Col = ColModel
                mModelNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColDrawingNo
                mDrawingNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColChargeableWidth
                mWidth = Val(.Text)

                .Col = ColChargeableHeight
                mHeight = Val(.Text)

                mExicseableAmt = 0
                mCessableAmt = 0
                mServiceAmt = 0
                mCESSAmt = 0
                mSTableAmt = 0
                SqlStr = ""

                If mItemCode <> "" And mQty > 0 And mRate > 0 Then

                    SqlStr = " INSERT INTO FIN_SUPP_SALE_DET ( " & vbCrLf _
                        & " MKEY , SUBROWNO, " & vbCrLf _
                        & " ITEM_CODE , CUSTOMER_PART_NO, HSNCODE, " & vbCrLf _
                        & " ITEM_DESC, ITEM_UOM, " & vbCrLf _
                        & " SALE_FYEAR, SALE_MKEY, " & vbCrLf & " BILL_NO, INVOICE_DATE, " & vbCrLf _
                        & " BILL_QTY, BILL_RATE, " & vbCrLf & " SO_RATE, QTY, " & vbCrLf & " RATE, AMOUNT, " & vbCrLf _
                        & " ITEM_ED, ITEM_ST, " & vbCrLf & " ITEM_CESS, COMPANY_CODE, AUTO_KEY_INVOICE, " & vbCrLf & " CGST_PER, CGST_AMOUNT, " & vbCrLf _
                        & " SGST_PER, SGST_AMOUNT, " & vbCrLf _
                        & " IGST_PER, IGST_AMOUNT, ITEM_MODEL, ITEM_DRAWINGNO, CHARGEABLE_HEIGHT, CHARGEABLE_WIDTH,GSTABLE_AMT " & vbCrLf _
                        & " ) "

                    SqlStr = SqlStr & vbCrLf & " VALUES ('" & LblMKey.Text & "'," & I & ", " & vbCrLf _
                        & " '" & mItemCode & "', '" & mPartNo & "', '" & mHSNCode & "', " & vbCrLf & " '" & mItemDesc & "', '" & mUnit & "'," & vbCrLf _
                        & " " & mPurFYear & ", '" & mPurMkey & "', " & vbCrLf & " '" & mBillNo & "',TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " " & mBillQty & ", " & mBillRate & ", " & vbCrLf & " " & mPORate & ", " & mQty & ", " & vbCrLf _
                        & " " & mRate & ", " & mAmount & ", " & vbCrLf & " " & mExicseableAmt & ", " & mSTableAmt & "," & vbCrLf _
                        & " " & mCESSAmt & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & mInvoiceNo & "'," & vbCrLf _
                        & " " & mCGSTPer & ", " & mCGSTAmount & "," & vbCrLf & " " & mSGSTPer & ", " & mSGSTAmount & "," & vbCrLf _
                        & " " & mIGSTPer & ", " & mIGSTAmount & ", " & vbCrLf _
                        & " '" & mModelNo & "','" & mDrawingNo & "'," & mHeight & "," & mWidth & "," & mTaxableAmount & ") "

                    PubDBCn.Execute(SqlStr)

                    If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                            If VB.Left(cboReason.Text, 1) = 6 Then
                                If UpdateStockTRN(PubDBCn, ConStockRefType_DSP, LblMKey.Text, I, txtVDate.Text, (txtVDate.Text), mStockType, mItemCode, mUnit, "", mQty, 0, "I", 0, 0, "", "", "", "PAD", "", "N", " From : (Reversal of Bill No : " & mBillNo & ") " & txtSupplier.Text, pSuppCustCode, ConWH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1
                            End If
                            If UpdateGSTTRN(PubDBCn, (LblMKey.Text), (LblBookCode.Text), mBookType, mBookSubType, pVNo, VB6.Format(txtVDate.Text, "DD-MMM-YYYY"), Trim(txtBillNo.Text), VB6.Format(txtBillDate.Text, "DD-MMM-YYYY"), mBillNo, VB6.Format(mBillDate, "DD-MMM-YYYY"), pSuppCustCode, mDebitAccountCode, "Y", pSuppCustCode, I, mItemCode, mQty, mUnit, mRate, mAmount, mAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, mDivisionCode, mHSNCode, mItemDesc, "", "N", mBookType, "G", "N", "D", VB6.Format(txtBillDate.Text, "DD-MMM-YYYY"), "N") = False Then GoTo UpdateDetail1
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
        PubDBCn.Execute("Delete From FIN_SUPP_SALE_EXP Where Mkey='" & LblMKey.Text & "'")
        With SprdExp
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColExpName
                If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND GST_ENABLED='Y' ") = True Then
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
                    SqlStr = "Insert Into  FIN_SUPP_SALE_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO,DebitAmount) " & vbCrLf & " Values ('" & LblMKey.Text & "'," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mCalcOn & ",'" & mRO & "'," & mDebitAmount & ")"
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
        mAgtPO = False
        FieldsVarification = True
        If ValidateBranchLocking((TxtVDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        mLockBookCode = CInt(ConLockJournal)
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

        If CDate(VB6.Format(txtVDate.Text, "DD/MM/YYYY")) > CDate(VB6.Format(PubCurrDate, "DD/MM/YYYY")) Then
            MsgInformation("Voucher Date is Greater Than Current Date. Cannot Save")
            txtVDate.Focus()
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
        If IsDate(txtWEFDate.Text) Then
            If CDate(TxtVDate.Text) < CDate(txtWEFDate.Text) Then
                MsgBox("VDate Can Not be Less Than WEFDate.")
                FieldsVarification = False
                TxtVDate.Focus()
                Exit Function
            End If
        End If

        If CDate(TxtVDate.Text) < CDate(txtBillDate.Text) Then
            MsgBox("VDate Can Not be Less Than BillDate.")
            FieldsVarification = False
            If txtBillDate.Enabled = True Then txtBillDate.Focus()
            Exit Function
        End If
        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If
        If Trim(txtSupplier.Text) = "" Then
            MsgBox("Supplier Cannot Be Blank", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Supplier Does Not Exist In Master", MsgBoxStyle.Information)
            'txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If
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
        'If Trim(txtItemType.Text) = "" Then
        '    MsgBox("Item Type is Blank", MsgBoxStyle.Information)
        '    FieldsVarification = False
        '    txtItemType.Focus()
        '    Exit Function
        'End If
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
        'If Trim(txtItemType.Text) = "" Then
        '    MsgBox("Item Type is Blank", MsgBoxStyle.Information)
        '    FieldsVarification = False
        '    txtItemType.Focus()
        '    Exit Function
        'End If
        If lblGoodService.Text = "S" And chkItemDetails.CheckState = System.Windows.Forms.CheckState.Unchecked And chkGSTApplicable.CheckState = System.Windows.Forms.CheckState.Checked Then
            If MainClass.ValidateWithMasterTable((txtSACCode.Text), "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = False Then
                MsgBox("Invalid SAC Code", MsgBoxStyle.Information)
                FieldsVarification = False
                txtSACCode.Focus()
                Exit Function
            End If
        ElseIf lblGoodService.Text = "G" And chkItemDetails.CheckState = System.Windows.Forms.CheckState.Unchecked And chkGSTApplicable.CheckState = System.Windows.Forms.CheckState.checked Then
            If MainClass.ValidateWithMasterTable((txtSACCode.Text), "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G'") = False Then
                MsgBox("Invalid HSN Code", MsgBoxStyle.Information)
                FieldsVarification = False
                txtSACCode.Focus()
                Exit Function
            End If
        End If
        If lblGoodService.Text = "G" Then
            If cboReason.SelectedIndex = 4 Then
            ElseIf chkItemDetails.CheckState = System.Windows.Forms.CheckState.Checked Then
                If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
            End If

        End If
        Dim mPaymentAmount As Double = 0

        With SprdPaymentDetail
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColPayBillNo
                If Trim(.Text) <> "" Then
                    .Col = ColPayPaymentAmt
                    mPaymentAmount = mPaymentAmount + Val(.Text)
                End If
            Next
        End With
        If mPaymentAmount > Val(lblNetAmount.Text) Then
            MsgBox("Payment Cann't be greater than Bill Amount", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        '    If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False: Exit Function
        With SprdExp
            For mRow = 1 To .MaxRows
                .Row = mRow
                .Col = ColExpIdent
                If Trim(.Text) = "ED" Then
                    .Col = ColExpAmt
                    If Val(.Text) > 0 Then
                        mExciseDutyAmt = Val(.Text)
                        mSTTaxcount = mSTTaxcount + 1
                        If mSTTaxcount > 1 Then Exit For
                    End If
                End If
                If Trim(.Text) = "EDU" Then
                    .Col = ColExpAmt
                    If Val(.Text) > 0 Then
                        mEDUAmt = Val(.Text)
                    End If
                End If
                If Trim(.Text) = "SHC" Then
                    .Col = ColExpAmt
                    If Val(.Text) > 0 Then
                        mSHECessAmt = Val(.Text)
                    End If
                End If
                If Trim(.Text) = "SER" Then
                    .Col = ColExpAmt
                    If Val(.Text) > 0 Then
                        mServiceTaxAmt = Val(.Text)
                    End If
                End If
            Next
        End With
        If mSTTaxcount > 1 Then
            MsgBox("Please Check Excise Duty Expenses.", MsgBoxStyle.Information)
            FieldsVarification = False
            Call MainClass.SetFocusToCell(SprdExp, mRow, ColExpAmt)
            Exit Function
        End If
        mSTTaxcount = 0
        With SprdExp
            For mRow = 1 To .MaxRows
                .Row = mRow
                .Col = ColExpIdent
                If Trim(.Text) = "ST" Then
                    .Col = ColExpAmt
                    If Val(.Text) > 0 Then
                        mSalesTaxAmount = Val(.Text)
                        mSTTaxcount = mSTTaxcount + 1
                        If mSTTaxcount > 1 Then Exit For
                    End If
                End If
            Next
        End With
        If mSTTaxcount > 1 Then
            MsgBox("Please Check Sales Tax Expenses.", MsgBoxStyle.Information)
            FieldsVarification = False
            Call MainClass.SetFocusToCell(SprdExp, mRow, ColExpAmt)
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
    Public Sub FrmCust_SaleGST_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim SqlStr As String
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Me.Text = "Customer Debit Note"
        SqlStr = ""
        SqlStr = "Select * from FIN_SUPP_SALE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchMain, ADODB.LockTypeEnum.adLockReadOnly)
        SqlStr = ""
        SqlStr = "Select * from FIN_SUPP_SALE_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchDetail, ADODB.LockTypeEnum.adLockReadOnly)
        SqlStr = ""
        SqlStr = "Select * from FIN_SUPP_SALE_EXP Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSuppPurchExp, ADODB.LockTypeEnum.adLockReadOnly)
        Call AssignGrid(False)
        Call SetTextLengths()
        mSupplierCode = CStr(-1)
        cboInvType.Enabled = True
        mBookType = VB.Left(lblBookType.Text, 1)
        FillCboSaleType()
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

        SqlStr = "SELECT VNOPREFIX, FIN_INVTYPE_MST.NAME AS INVOICE_TYPE," & vbCrLf _
            & " TO_CHAR(VNOSEQ), VNO, VDATE, " & vbCrLf _
            & " BILLNO, INVOICE_DATE  AS BILLDATE, " & vbCrLf _
            & " AUTO_KEY_SO AS PONO, SO_DATE, " & vbCrLf _
            & " A.SUPP_CUST_NAME AS SUPPLIER, B.SUPP_CUST_NAME AS CREDIT_ACCOUNT, " & vbCrLf _
            & " ITEMDESC, TARIFFHEADING AS TARIFF,ITEMVALUE,"

        SqlStr = SqlStr & vbCrLf _
            & "TOTEDAMOUNT AS EDAMT,TOTEDUAMOUNT AS CESS_AMT,NETVALUE "

        SqlStr = SqlStr & vbCrLf _
            & " FROM " & vbCrLf _
            & " FIN_SUPP_SALE_HDR, FIN_INVTYPE_MST, FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_MST B " & vbCrLf _
            & " WHERE FIN_SUPP_SALE_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " And FIN_SUPP_SALE_HDR.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND FIN_SUPP_SALE_HDR.COMPANY_CODE=FIN_INVTYPE_MST.COMPANY_CODE(+) " & vbCrLf _
            & " AND FIN_SUPP_SALE_HDR.TRNTYPE=FIN_INVTYPE_MST.CODE " & vbCrLf _
            & " AND FIN_SUPP_SALE_HDR.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf _
            & " AND FIN_SUPP_SALE_HDR.SUPP_CUST_CODE=A.SUPP_CUST_CODE " & vbCrLf _
            & " AND FIN_SUPP_SALE_HDR.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
            & " AND FIN_SUPP_SALE_HDR.ACCOUNTCODE=B.SUPP_CUST_CODE " & vbCrLf _
            & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' AND GOODS_SERVICE='" & lblGoodService.Text & "'"

        SqlStr = SqlStr & vbCrLf & " Order by FIN_SUPP_SALE_HDR.VDATE, FIN_SUPP_SALE_HDR.VNO"
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
            .set_ColWidth(3, 0)
            .set_ColWidth(4, 1200)
            .set_ColWidth(5, 1200)
            .set_ColWidth(6, 1200)
            .set_ColWidth(7, 1300)
            .set_ColWidth(8, 1200)
            .set_ColWidth(9, 1300)
            .set_ColWidth(10, 1200)
            .set_ColWidth(11, 1200)
            .set_ColWidth(12, 2000)
            .set_ColWidth(13, 2000)
            .set_ColWidth(14, 1200)
            .set_ColWidth(15, 1200)
            .set_ColWidth(16, 1200)
            .set_ColWidth(17, 1200)
            .set_ColWidth(18, 1200)
            .set_ColWidth(19, 1200)
            .set_ColWidth(20, 800)
            .set_ColWidth(21, 800)
            For cntCol = 18 To 21
                .Col = cntCol
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            Next
            .ColsFrozen = 9
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle
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
            .set_ColWidth(ColExpName, 22)

            .Col = ColExpPercent
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = 0.0#
            .TypeFloatMax = 99.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeEditMultiLine = False
            .Col = ColExpAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(ColExpAmt, 8)
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
            '.ColHidden = True

            .ColsFrozen = ColItemDesc
            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsSuppPurchDetail.Fields("Item_Desc").DefinedSize ''
            .set_ColWidth(ColItemDesc, 15)
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
            .TypeEditLen = RsSuppPurchDetail.Fields("SALE_FYEAR").DefinedSize ''
            .set_ColWidth(ColPURFYear, 8)
            .ColHidden = True
            .Col = ColPURMkey
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .TypeEditLen = RsSuppPurchDetail.Fields("SALE_MKEY").DefinedSize ''
            .set_ColWidth(ColPURMkey, 8)
            .ColHidden = True
            For I = ColBillNo To ColBillDate
                .Col = I
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditMultiLine = True
                .set_ColWidth(I, 7.5)
                .ColHidden = IIf(I = ColBillNo, False, True)
            Next
            .Col = ColInvoiceNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColInvoiceNo, 7.5)
            .ColHidden = False

            For I = ColBillQty To ColAmount
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMin = CDbl("-99999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(I, IIf(I = ColQty, 7, 8))
                '            .ColHidden = True
            Next

            .Col = ColTaxableAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(.Col, 9)


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
            .set_ColWidth(ColCGSTAmount, 6)
            .Col = ColSGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColSGSTAmount, 6)
            .Col = ColIGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColIGSTAmount, 6)

            For cntCol = ColChargeableWidth To ColChargeableHeight
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 7)
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            Next

            .Col = ColDrawingNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsSuppPurchDetail.Fields("ITEM_DRAWINGNO").DefinedSize ''				
            .set_ColWidth(.Col, 20)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColModel
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsSuppPurchDetail.Fields("ITEM_MODEL").DefinedSize ''				
            .set_ColWidth(.Col, 10)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

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
        SprdMain.EditModeReplace = True
        SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        '    Resume
        If Err.Number = -2147418113 Then RsSuppPurchDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsSuppPurchMain
            'txtVNoPrefix.Maxlength = .Fields("BOOKTYPE").DefinedSize
            txtVNo.Maxlength = .Fields("VNOSEQ").DefinedSize
            TxtVDate.Maxlength = 10
            txtBillNo.Maxlength = .Fields("BillNo").Precision
            txtBillDate.Maxlength = 10
            txtOBillNo.Maxlength = .Fields("BillNo").Precision
            txtOBillDate.Maxlength = 10
            txtPartyDNNo.Maxlength = .Fields("PARTY_DNCN_NO").DefinedSize ''
            txtPartyDNDate.Maxlength = 10
            txtRecdDate.Maxlength = 10
            txtPONo.Maxlength = .Fields("AUTO_KEY_SO").DefinedSize
            txtPODate.Maxlength = 10
            txtPOAmendNo.Maxlength = .Fields("AMEND_NO").DefinedSize
            txtWEFDate.Maxlength = 10
            txtSupplier.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtDebitAccount.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtTariff.Maxlength = .Fields("TARIFFHEADING").DefinedSize
            txtItemType.Maxlength = .Fields("ItemDesc").DefinedSize
            txtRemarks.Maxlength = .Fields("Remarks").DefinedSize
            txtRemarks1.Maxlength = .Fields("Remarks").DefinedSize
            txtNarration.Maxlength = .Fields("NARRATION").DefinedSize
            txtPaymentDate.Maxlength = 10
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
        Dim mSupplierCode As String
        Dim mReason As Integer
        Dim mVNO As String

        Clear1()
        With RsSuppPurchMain
            If Not .EOF Then


                LblMKey.Text = .Fields("MKey").Value

                'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then        ''Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104
                '    txtVNoPrefix.Text = GetDocumentPrefix("P", IIf(LblBookCode.Text = ConSaleCreditBookCode, "M", "R"), cboDivision.Text)
                'Else

                mVNO = IIf(IsDBNull(.Fields("VNO").Value), "", .Fields("VNO").Value)

                txtVNoPrefix.Text = IIf(IsDBNull(.Fields("VNOPREFIX").Value), "", .Fields("VNOPREFIX").Value)

                'End If

                'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then    ''Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104
                '    txtVNo.Text = VB6.Format(IIf(IsDBNull(.Fields("VNOSEQ").Value), "", .Fields("VNOSEQ").Value), ConBillFormat)
                'Else
                txtVNo.Text = VB6.Format(IIf(IsDBNull(.Fields("VNOSEQ").Value), "", .Fields("VNOSEQ").Value), "0000000")
                'End If


                txtVDate.Text = VB6.Format(IIf(IsDbNull(.Fields("VDate").Value), "", .Fields("VDate").Value), "DD/MM/YYYY")
                txtBillNo.Text = IIf(IsDbNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDbNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                txtPONo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_SO").Value), "", .Fields("AUTO_KEY_SO").Value)
                txtPODate.Text = IIf(IsDbNull(.Fields("SO_DATE").Value), "", .Fields("SO_DATE").Value)
                txtPOAmendNo.Text = IIf(IsDbNull(.Fields("AMEND_NO").Value), "", .Fields("AMEND_NO").Value)
                txtWEFDate.Text = IIf(IsDbNull(.Fields("SO_WEFDATE").Value), "", .Fields("SO_WEFDATE").Value)
                txtToDate.Text = VB6.Format(IIf(IsDbNull(.Fields("TO_DATE").Value), "", .Fields("TO_DATE").Value), "DD/MM/YYYY")
                txtPartyDNNo.Text = IIf(IsDbNull(.Fields("PARTY_DNCN_NO").Value), "", .Fields("PARTY_DNCN_NO").Value)
                txtPartyDNDate.Text = VB6.Format(IIf(IsDbNull(.Fields("PARTY_DNCN_DATE").Value), "", .Fields("PARTY_DNCN_DATE").Value), "DD/MM/YYYY")
                txtRecdDate.Text = VB6.Format(IIf(IsDBNull(.Fields("PARTY_DNCN_RECDDATE").Value), "", .Fields("PARTY_DNCN_RECDDATE").Value), "DD/MM/YYYY")

                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)


                '            txtPartyDNNo.Enabled = IIf(Trim(txtPartyDNNo.Text) = "", False, True)
                '            txtPartyDNDate.Enabled = IIf(Trim(txtPartyDNDate.Text) = "", False, True)
                '            txtRecdDate.Enabled = IIf(Trim(txtRecdDate.Text) = "", False, True)
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

                chkItemDetails.CheckState = IIf(.Fields("IS_ITEMDETAIL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkCancelled.CheckState = IIf(.Fields("CANCELLED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkCancelled.Enabled = IIf(.Fields("CANCELLED").Value = "Y", False, True)
                chkGSTApplicable.CheckState = IIf(.Fields("GST_APP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkGSTApplicable.Enabled = False
                txtTariff.Text = IIf(IsDbNull(.Fields("TARIFFHEADING").Value), "", .Fields("TARIFFHEADING").Value)
                txtItemType.Text = IIf(IsDbNull(.Fields("ITEMDESC").Value), "", .Fields("ITEMDESC").Value)
                If lblGoodService.Text = "G" And chkItemDetails.CheckState = System.Windows.Forms.CheckState.Checked Then
                    txtRemarks.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                    txtSACCode.Text = ""
                    lblCGSTPer.Text = "0.00"
                    lblSGSTPer.Text = "0.00"
                    lblIGSTPer.Text = "0.00"
                Else
                    txtRemarks1.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                    txtSACCode.Text = IIf(IsDbNull(.Fields("SAC_CODE").Value), "", .Fields("SAC_CODE").Value)
                    txtOBillNo.Text = IIf(IsDbNull(.Fields("O_BILLNO").Value), "", .Fields("O_BILLNO").Value)
                    txtOBillDate.Text = VB6.Format(IIf(IsDbNull(.Fields("O_INVOICE_DATE").Value), "", .Fields("O_INVOICE_DATE").Value), "DD/MM/YYYY")
                    lblCGSTPer.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTCGST_PER").Value), 0, .Fields("TOTCGST_PER").Value), "0.00")
                    lblSGSTPer.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTSGST_PER").Value), 0, .Fields("TOTSGST_PER").Value), "0.00")
                    lblIGSTPer.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTIGST_PER").Value), 0, .Fields("TOTIGST_PER").Value), "0.00")
                    txtOBillNo.Enabled = False
                    txtOBillDate.Enabled = False
                End If
                txtNarration.Text = IIf(IsDbNull(.Fields("NARRATION").Value), "", .Fields("NARRATION").Value)
                txtPaymentDate.Text = IIf(IsDbNull(.Fields("PAYMENTDATE").Value), "", .Fields("PAYMENTDATE").Value)
                chkFinalPost.CheckState = IIf(.Fields("ISFINALPOST").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkFinalPost.Enabled = IIf(.Fields("ISFINALPOST").Value = "Y", False, True)

                lblTotQty.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTQTY").Value), 0, .Fields("TOTQTY").Value), "0.00")
                If lblGoodService.Text = "S" Or chkItemDetails.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    txtTotItemValue.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEMVALUE").Value), 0, .Fields("ITEMVALUE").Value), "0.00")
                Else
                    lblTotItemValue.Text = VB6.Format(IIf(IsDbNull(.Fields("ITEMVALUE").Value), 0, .Fields("ITEMVALUE").Value), "0.00")
                End If
                lblNetAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("NETVALUE").Value), 0, .Fields("NETVALUE").Value), "0.00")
                mAddUser = IIf(IsDbNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                mAddDate = VB6.Format(IIf(IsDbNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                mModUser = IIf(IsDbNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                mModDate = VB6.Format(IIf(IsDBNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")

                txtIRNNo.Text = IIf(IsDBNull(.Fields("IRN_NO").Value), "", .Fields("IRN_NO").Value)
                txteInvAckNo.Text = IIf(IsDBNull(.Fields("IRN_ACK_NO").Value), "", .Fields("IRN_ACK_NO").Value)
                txteInvAckDate.Text = VB6.Format(IIf(IsDBNull(.Fields("IRN_ACK_DATE").Value), "", .Fields("IRN_ACK_DATE").Value), "DD/MM/YYYY HH:MM")

                If Trim(txtIRNNo.Text) = "" Then
                    cmdeInvoice.Enabled = True ' IIf(PubUserID = "EINV", True, IIf(RsCompany.Fields("E_INVOICE_APP").Value = "Y", True, False))
                Else
                    cmdeInvoice.Enabled = False
                End If

                mDivisionCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)
                mSupplierCode = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If
                cboDivision.Enabled = False
                mReason = Val(IIf(IsDbNull(.Fields("reason").Value), 1, .Fields("reason").Value))
                cboReason.SelectedIndex = mReason - 1
                cboReason.Enabled = IIf(mAuthorised = True, True, False)
                Call ShowDetail1((LblMKey.Text), mCustRefNo)
                Call ShowPaymentDetail1((LblMKey.Text), mSupplierCode)
                Call ShowExp1((LblMKey.Text))
                Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))
                txtSupplier.Enabled = IIf(mAuthorised = True, True, False)
                ''Call CalcTots
            End If
        End With
        txtVno.Enabled = True
        cmdShowPO.Enabled = IIf(mAuthorised = True, True, False)
        txtPONo.Enabled = IIf(mAuthorised = True, True, False)
        txtPODate.Enabled = IIf(mAuthorised = True, True, False)
        CmdSearchPO.Enabled = IIf(mAuthorised = True, True, False)
        CmdSearchAmend.Enabled = IIf(mAuthorised = True, True, False)
        txtToDate.Enabled = IIf(mAuthorised = True, True, False)
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
    Private Sub txtTotItemValue_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotItemValue.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTotItemValue_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotItemValue.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTotItemValue_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotItemValue.Leave
        Call CalcTots()
    End Sub
    Private Sub txtTotItemValue_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTotItemValue.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub ShowExp1(ByRef mMkey As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim pExpId As String
        Dim SqlStr As String
        Call FillSprdExp()
        SqlStr = ""
        SqlStr = "Select FIN_SUPP_SALE_EXP.EXPCODE,FIN_SUPP_SALE_EXP.EXPPERCENT, " & vbCrLf & " FIN_SUPP_SALE_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO,DebitAmount " & vbCrLf & " From FIN_SUPP_SALE_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_SUPP_SALE_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND FIN_SUPP_SALE_EXP.Mkey='" & mMkey & "'  AND GST_ENABLED='Y' " & vbCrLf & " ORDER BY SUBROWNO"
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
                        .Text = CStr(Val(IIf(IsDbNull(RsSuppPurchExp.Fields("Amount").Value), "", RsSuppPurchExp.Fields("Amount").Value)))
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
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM FIN_SUPP_SALE_DET " & vbCrLf _
            & " Where Mkey='" & mMkey & "'" & vbCrLf _
            & " Order By SubRowNo"

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
                SprdMain.Text = IIf(IsDbNull(.Fields("HSNCODE").Value), "", .Fields("HSNCODE").Value)
                If Trim(SprdMain.Text) = "" Then
                    SprdMain.Text = GetHSNCode(mItemCode)
                End If
                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColModel
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_MODEL").Value), "", .Fields("ITEM_MODEL").Value)

                SprdMain.Col = ColDrawingNo
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_DRAWINGNO").Value), "", .Fields("ITEM_DRAWINGNO").Value)


                SprdMain.Col = ColChargeableWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_WIDTH").Value), 0, .Fields("CHARGEABLE_WIDTH").Value)))

                SprdMain.Col = ColChargeableHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_HEIGHT").Value), 0, .Fields("CHARGEABLE_HEIGHT").Value)))


                SprdMain.Col = ColPURFYear
                SprdMain.Text = IIf(IsDbNull(.Fields("SALE_FYEAR").Value), "", .Fields("SALE_FYEAR").Value)
                SprdMain.Col = ColPURMkey
                SprdMain.Text = IIf(IsDbNull(.Fields("SALE_MKEY").Value), "", .Fields("SALE_MKEY").Value)
                SprdMain.Col = ColBillNo
                SprdMain.Text = IIf(IsDbNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                SprdMain.Col = ColBillDate
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                SprdMain.Col = ColInvoiceNo
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("AUTO_KEY_INVOICE").Value), "", .Fields("AUTO_KEY_INVOICE").Value))
                SprdMain.Col = ColBillQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("BILL_QTY").Value), 0, .Fields("BILL_QTY").Value)))
                SprdMain.Col = ColBillRate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("BILL_RATE").Value), 0, .Fields("BILL_RATE").Value)))
                SprdMain.Col = ColPORate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("SO_RATE").Value), 0, .Fields("SO_RATE").Value)))
                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("QTY").Value), 0, .Fields("QTY").Value)))
                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("RATE").Value), 0, .Fields("RATE").Value)))
                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("AMOUNT").Value), 0, .Fields("AMOUNT").Value)))

                SprdMain.Col = ColTaxableAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("GSTABLE_AMT").Value), 0, .Fields("GSTABLE_AMT").Value)))

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
        Dim mAmount As Double
        Dim mQty As Double
        Dim mRate As Double
        Dim mItemAmount As Double
        Dim mItemValue As Double
        Dim mTotQty As Double
        Dim j As Integer
        Dim I As Integer
        Dim mItemCode As String
        Dim xStr As String
        Dim mExpPercent As Double
        Dim mNetAccessAmt As Double
        Dim mShortage As Double
        Dim mTotItemAmount As Double
        Dim pTotCustomDuty As Double
        Dim pTotMSC As Double
        Dim pTotDiscount As Double
        Dim pTotServiceTax As Double
        Dim pTotRO As Double
        Dim pTotTCS As Double
        Dim mTotExp As Double
        Dim pTCSPer As Double
        Dim pTotOthers As Double
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mTotCGSTAmount As Double
        Dim mTotSGSTAmount As Double
        Dim mTotIGSTAmount As Double
        Dim mTaxableAmount As Double
        Dim mOtherTaxableAmount As Double
        Dim mIsTaxable As String
        Dim mExpName As String
        Dim mSACCode As String
        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mAccountCode As String
        Dim mTotTaxableItemAmount As String

        pRound = 0
        mQty = 0
        mRate = 0
        '    mST = 0
        mItemAmount = 0
        mTotItemAmount = 0
        mItemValue = 0
        mTotExp = 0
        pTotRO = 0
        If Trim(txtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = Trim(MasterNo)
            End If
        End If

        mLocal = GetPartyBusinessDetail(Trim(mAccountCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(mAccountCode), Trim(txtBillTo.Text), "GST_RGN_NO")

        If cboReason.SelectedIndex = 4 Then
            mTotIGSTAmount = 0
            mTotSGSTAmount = 0
            mTotCGSTAmount = 0
            Call BillExpensesOnlyGSTCalc(SprdExp, (txtBillDate.Text), mNetAccessAmt, mTotItemAmount, mTotItemAmount, 0, 0, 0, mTotIGSTAmount, mTotSGSTAmount, mTotCGSTAmount, 0, 0, 0, pTotOthers, 0, pTotCustomDuty, pTotMSC, pTotDiscount, 0, pTotRO, pTotTCS, mTotExp, pTCSPer, "CS")
            lblTotItemValue.Text = VB6.Format(0, "#0.00")
            lblTotFreight.Text = VB6.Format(0, "#0.00")
            lblTotCharges.Text = CStr(0) ''Format(mRO, "#0.00")
            lblTotExpAmt.Text = VB6.Format(mTotExp, "#0.00")
            lblTotTaxableAmt.Text = VB6.Format(0, "#0.00")
            lblRO.Text = VB6.Format(pTotRO, "#0.00")
            lblDiscount.Text = VB6.Format(0, "#0.00")
            lblMSC.Text = VB6.Format(0, "#0.00")
            lblTotQty.Text = VB6.Format(0, "#0.00")
            lblTotItemValue.Text = VB6.Format(0, "#0.00")
            lblTotCGST.Text = VB6.Format(mTotCGSTAmount, "#0.00")
            lblTotSGST.Text = VB6.Format(mTotSGSTAmount, "#0.00")
            lblTotIGST.Text = VB6.Format(mTotIGSTAmount, "#0.00")
            lblNetAmount.Text = VB6.Format(mTotCGSTAmount + mTotSGSTAmount + mTotIGSTAmount, "#0.00")
            Exit Sub
        End If
        If lblGoodService.Text = "G" And chkItemDetails.CheckState = System.Windows.Forms.CheckState.Checked Then

            With SprdExp
                For I = 1 To SprdExp.MaxRows
                    .Row = I
                    .Col = ColExpName
                    mExpName = Trim(.Text)
                    If MainClass.ValidateWithMasterTable(mExpName, "NAME", "TAXABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GST_ENABLED='Y' AND TAXABLE='Y'") = True Then
                        mIsTaxable = MasterNo
                    Else
                        mIsTaxable = "N"
                    End If
                    If mIsTaxable = "Y" Then
                        .Col = ColExpAmt
                        mOtherTaxableAmount = mOtherTaxableAmount + CDbl(VB6.Format(.Text, "0.00"))
                    End If
                Next
            End With
        End If
        If chkItemDetails.CheckState = System.Windows.Forms.CheckState.Unchecked Then ''lblGoodService.Text = "S" Or
            mTotItemAmount = CDbl(VB6.Format(Val(txtTotItemValue.Text), "0.00"))
            mTotQty = 1
            'mLocal = "N"
            'If Trim(txtSupplier.Text) <> "" Then
            '    'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    '    mLocal = Trim(MasterNo)
            '    'End If
            '    'mLocal = GetPartyBusinessDetail(Trim(txtSupplier.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
            '    'mPartyGSTNo = GetPartyBusinessDetail(Trim(txtSupplier.Text), Trim(txtBillTo.Text), "GST_RGN_NO")
            'End If
            'mPartyGSTNo = ""
            'If MainClass.ValidateWithMasterTable(Trim(txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mPartyGSTNo = MasterNo
            'End If

            If lblGoodService.Text = "S" Then
                If MainClass.ValidateWithMasterTable((txtSACCode.Text), "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                    mSACCode = MasterNo
                End If
            Else
                If MainClass.ValidateWithMasterTable((txtSACCode.Text), "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G'") = True Then
                    mSACCode = MasterNo
                End If
            End If

            mCGSTPer = 0
            mSGSTPer = 0
            mIGSTPer = 0

            If lblGoodService.Text = "S" Then
                If GetSACDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, mPartyGSTNo, IIf(chkGSTApplicable.CheckState = System.Windows.Forms.CheckState.Checked, "G", "N")) = False Then GoTo ERR1
            Else
                If GetHSNDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, "G", mPartyGSTNo) = False Then GoTo ERR1
            End If
            mTotCGSTAmount = CDbl(VB6.Format(mTotItemAmount * mCGSTPer * 0.01, "0.00"))
            mTotSGSTAmount = CDbl(VB6.Format(mTotItemAmount * mSGSTPer * 0.01, "0.00"))
            mTotIGSTAmount = CDbl(VB6.Format(mTotItemAmount * mIGSTPer * 0.01, "0.00"))
            lblCGSTPer.Text = VB6.Format(mCGSTPer, "0.00")
            lblSGSTPer.Text = VB6.Format(mSGSTPer, "0.00")
            lblIGSTPer.Text = VB6.Format(mIGSTPer, "0.00")
        Else

            With SprdMain
                j = .MaxRows
                For I = 1 To j
                    .Row = I
                    .Col = 0
                    If .Text = "Del" Then GoTo DontCalc

                    .Col = ColItemCode
                    If .Text = "" Then GoTo DontCalc
                    mItemCode = .Text

                    .Col = ColQty
                    mQty = Val(.Text)

                    .Col = ColRate
                    mRate = Val(.Text)

                    .Col = ColAmount
                    .Text = VB6.Format(mQty * mRate, "0.00")


                    mTotItemAmount = mTotItemAmount + CDbl(VB6.Format(mQty * mRate, "0.00"))
DontCalc:
                Next I
            End With

            mTotTaxableItemAmount = mTotItemAmount + mOtherTaxableAmount


            With SprdMain
                j = .MaxRows
                For I = 1 To j
                    .Row = I
                    .Col = 0
                    If .Text = "Del" Then GoTo DontCalc1
                    .Col = ColItemCode
                    If .Text = "" Then GoTo DontCalc1
                    mItemCode = .Text

                    .Col = ColHSNCode
                    mSACCode = Trim(.Text)


                    .Col = ColRate
                    mRate = Val(.Text)

                    .Col = ColQty
                    mQty = Val(.Text)

                    mTotQty = mTotQty + mQty

                    .Col = ColAmount
                    mAmount = CDbl(VB6.Format(mRate * mQty, "0.00"))
                    .Text = CStr(mAmount)

                    .Col = ColTaxableAmount
                    If mTotItemAmount = 0 Then
                        mTaxableAmount = 0
                    Else
                        mTaxableAmount = mAmount + CDbl(VB6.Format(mOtherTaxableAmount * mAmount / mTotItemAmount, "0.00")) '' Format(Val(.Text), "0.00")				
                    End If
                    .Text = VB6.Format(Val(CStr(mTaxableAmount)), "0.00")


                    If chkGSTApplicable.CheckState = System.Windows.Forms.CheckState.Checked Then
                        If lblGoodService.Text = "S" Then
                            If GetSACDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, mPartyGSTNo, IIf(chkGSTApplicable.CheckState = System.Windows.Forms.CheckState.Checked, "G", "N")) = False Then GoTo ERR1
                        Else
                            If GetHSNDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, "G", mPartyGSTNo) = False Then GoTo ERR1
                        End If


                        lblCGSTPer.Text = VB6.Format(mCGSTPer, "0.00")
                        lblSGSTPer.Text = VB6.Format(mSGSTPer, "0.00")
                        lblIGSTPer.Text = VB6.Format(mIGSTPer, "0.00")

                        .Col = ColCGSTPer
                        .Text = mCGSTPer

                        .Col = ColSGSTPer
                        .Text = mSGSTPer

                        .Col = ColIGSTPer
                        .Text = mIGSTPer

                    Else
                        mCGSTPer = 0
                        mSGSTPer = 0
                        mIGSTPer = 0
                    End If

                    mItemAmount = CDbl(VB6.Format(mAmount, "0.00")) '- mDiscount
                    mItemValue = CDbl(VB6.Format(mAmount, "0.00"))
                    'mTotItemAmount = mTotItemAmount + mItemAmount
                    mCGSTAmount = CDbl(VB6.Format(mTaxableAmount * mCGSTPer * 0.01, "0.00"))
                    mSGSTAmount = CDbl(VB6.Format(mTaxableAmount * mSGSTPer * 0.01, "0.00"))
                    mIGSTAmount = CDbl(VB6.Format(mTaxableAmount * mIGSTPer * 0.01, "0.00"))
                    mTotCGSTAmount = mTotCGSTAmount + mCGSTAmount
                    mTotSGSTAmount = mTotSGSTAmount + mSGSTAmount
                    mTotIGSTAmount = mTotIGSTAmount + mIGSTAmount
                    .Col = ColCGSTAmount
                    .Text = VB6.Format(mCGSTAmount, "0.00")
                    .Col = ColSGSTAmount
                    .Text = VB6.Format(mSGSTAmount, "0.00")
                    .Col = ColIGSTAmount
                    .Text = VB6.Format(mIGSTAmount, "0.00")
DontCalc1:
                Next I
            End With
        End If
DirectCalc:

        mNetAccessAmt = Val(CStr(mTotItemAmount))
        '    Call BillExpensesCalcTots(SprdExp, txtBillDate.Text, False, mNetAccessAmt, mExciseableAmount, mTaxableAmount, _
        ''                                mCEDCessAble, mADDCessAble, mCESSableAmount, mTotItemAmount, _
        ''                                pTotExciseDuty, pTotEduCess, pTotSHECess, pTotADE, pTotExportExp, pTotOthers, _
        ''                                pTotSalesTax, pTotSurcharge, pTotCustomDuty, pTotAddCess, pTotCustomDutyExport, pTotCustomDutyCess, _
        ''                                pTotMSC, pTotDiscount, pTotServiceTax, pTotRO, pTotTCS, mTotExp, pEDPer, pSTPer, pServPer, pCessPer, pSHECPer, pTCSPer, "CS", mNetAccessAmt, pTotKKCAmount)



        Call BillExpensesCalcTots_GST(SprdExp, (txtBillDate.Text), mNetAccessAmt, mTotItemAmount, mTotItemAmount, 0, 0, 0, mTotIGSTAmount, mTotSGSTAmount, mTotCGSTAmount, 0, 0, 0, pTotOthers, 0, pTotCustomDuty, pTotMSC, pTotDiscount, 0, pTotRO, pTotTCS, mTotExp, pTCSPer, "CS")
        lblTotItemValue.Text = VB6.Format(mTotItemAmount, "#0.00")
        lblTotFreight.Text = VB6.Format(pTotOthers, "#0.00")
        lblTotCharges.Text = CStr(0) ''Format(mRO, "#0.00")
        lblTotExpAmt.Text = VB6.Format(0, "#0.00")
        lblOthersAmount.Text = VB6.Format(mTotExp, "#0.00")
        lblTotTaxableAmt.Text = VB6.Format(Val(CStr(mTotItemAmount + mOtherTaxableAmount)), "#0.00")
        lblRO.Text = VB6.Format(pTotRO, "#0.00")
        lblDiscount.Text = VB6.Format(pTotDiscount, "#0.00")
        lblMSC.Text = VB6.Format(pTotMSC, "#0.00")
        lblTotQty.Text = VB6.Format(mTotQty, "#0.00")
        lblTotItemValue.Text = VB6.Format(mTotItemAmount, "#0.00")
        lblTotCGST.Text = VB6.Format(mTotCGSTAmount, "#0.00")
        lblTotSGST.Text = VB6.Format(mTotSGSTAmount, "#0.00")
        lblTotIGST.Text = VB6.Format(mTotIGSTAmount, "#0.00")
        lblNetAmount.Text = VB6.Format(mTotExp + mTotItemAmount + mTotCGSTAmount + mTotSGSTAmount + mTotIGSTAmount, "#0.00")
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
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
        txtBillTo.Text = ""
        mSupplierCode = CStr(-1)
        txtVno.Text = ""
        If Not IsDate(TxtVDate.Text) Then
            TxtVDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        End If
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 And RsCompany.Fields("FYEAR").Value >= 2024 Then    ''Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104
            txtVNoPrefix.Text = GetDocumentPrefix("S", IIf(LblBookCode.Text = ConSaleCreditBookCode, "C", "D"), cboDivision.Text)
        Else
            txtVNoPrefix.Text = mBookType ''& vb6.Format(IIf(IsNull(RsCompany!INVOICE_PREFIX), "", RsCompany!INVOICE_PREFIX), "00") & Val(lblInvoiceSeq.text)
        End If


        txtBillNo.Text = ""
        txtBillDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtOBillNo.Text = ""
        txtOBillDate.Text = ""
        txtOBillDate.Enabled = True
        txtOBillNo.Enabled = True
        txtPartyDNNo.Text = ""
        txtPartyDNDate.Text = ""
        txtRecdDate.Text = ""
        txtSupplier.Text = ""
        txtDebitAccount.Text = ""
        txtPONo.Text = ""
        txtPODate.Text = ""
        txtPOAmendNo.Text = ""
        txtWEFDate.Text = ""
        txtTariff.Text = ""
        txtItemType.Text = ""
        txtRemarks.Text = ""
        txtRemarks1.Text = ""
        txtNarration.Text = ""
        txtSACCode.Text = ""
        lblTotTaxableAmt.Text = CStr(0)
        lblCGSTPer.Text = VB6.Format(0, "0.00")
        lblSGSTPer.Text = VB6.Format(0, "0.00")
        lblIGSTPer.Text = VB6.Format(0, "0.00")
        txtPaymentdate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        chkItemDetails.CheckState = System.Windows.Forms.CheckState.Checked
        chkItemDetails.Enabled = True
        fraService.Enabled = False
        SprdMain.Enabled = True

        chkCancelled.Enabled = True
        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkGSTApplicable.Enabled = True
        chkGSTApplicable.CheckState = System.Windows.Forms.CheckState.Checked
        chkFinalPost.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkFinalPost.Enabled = True
        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True
        lblTotQty.Text = "0.00"
        lblTotItemValue.Text = "0.00"
        lblNetAmount.Text = "0.00"
        txtTotItemValue.Text = "0.00"
        lblTotItemValue.Text = VB6.Format(0, "#0.00")
        lblNetAmount.Text = VB6.Format(0, "#0.00")
        lblTotFreight.Text = VB6.Format(0, "#0.00")
        lblTotCharges.Text = VB6.Format(0, "#0.00")
        lblTotExpAmt.Text = VB6.Format(0, "#0.00")

        lblTotCGST.Text = VB6.Format(0, "#0.00")
        lblTotSGST.Text = VB6.Format(0, "#0.00")
        lblTotIGST.Text = VB6.Format(0, "#0.00")
        lblOthersAmount.Text = VB6.Format(0, "#0.00")

        '    lblTotTaxableAmt.text = Format(0, "#0.00")
        lblRO.Text = VB6.Format(0, "#0.00")
        lblDiscount.Text = VB6.Format(0, "#0.00")
        lblSurcharge.Text = VB6.Format(0, "#0.00")
        lblMSC.Text = VB6.Format(0, "#0.00")
        lblTotExportExp.Text = VB6.Format(0, "#0.00")
        lblCDPer.Text = VB6.Format(0, "#0.00")
        txtPONo.Enabled = True
        txtPODate.Enabled = True
        cmdSearchPO.Enabled = True
        CmdSearchAmend.Enabled = True
        txtToDate.Enabled = True
        ''    cboInvType.ListIndex = -1
        cboReason.Enabled = True
        txtSupplier.Enabled = True
        cmdShowPO.Enabled = True

        txtIRNNo.Text = ""
        txteInvAckNo.Text = ""
        txteInvAckDate.Text = ""
        cmdeInvoice.Enabled = False

        lblPaymentTotal.Text = "0.00"
        lblPaymentDC.Text = ""

        lblDiffAmt.Text = "0.00"

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ClearGrid(SprdExp)
        FraPostingDtl.Visible = False

        MainClass.ClearGrid(SprdPaymentDetail)
        Call FormatSprdPaymentDetail(-1, False)

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
            mLocal = GetPartyBusinessDetail(Trim(txtSupplier.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
            'mPartyGSTNo = GetPartyBusinessDetail(Trim(txtSupplier.Text), Trim(txtBillTo.Text), "GST_RGN_NO")
            'If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mLocal = IIf(MasterNo = "Y", "L", "C")
            'Else
            '    mLocal = ""
            'End If
        Else
            mLocal = ""
        End If
        SqlStr = "Select * From FIN_INTERFACE_MST " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (Type='S' OR Type='B') AND GST_ENABLED='Y' Order By PrintSequence"
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
    Private Sub FrmCust_SaleGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmCust_SaleGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub FrmCust_SaleGST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RS As ADODB.Recordset

        CurrFormHeight = 7245
        CurrFormWidth = 11355

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
        mAuthorised = IIf(InStr(1, XRIGHT, "S") > 0, True, False)
        SprdMain.Enabled = True
        SprdExp.Enabled = True
        txtVNoPrefix.Enabled = False
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
        'AdoDCMain.Visible = False
        cboDivision.Items.Clear()
        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " ORDER BY DIV_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If
        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        Call FrmCust_SaleGST_Activated(eventSender, eventArgs)
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
                    SqlStr = "Select * From FIN_INTERFACE_MST Where COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND Name= '" & m_Exp & "'  AND GST_ENABLED='Y' "
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
                        If MainClass.ValidateWithMasterTable(m_xpn, "Name", "RoundOff", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND GST_ENABLED='Y' ") Then
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
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub txtBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillNo.Text, "N", "N")
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
    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub SearchAccounts()
        On Error GoTo ERR1
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        ''MainClass.SearchMaster txtSupplier, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(txtSupplier.Text, "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "SUPP_CUST_ADDR", "LOCATION_ID", SqlStr)
        ''  MainClass.SearchGridMaster(txtSupplier.Text, "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "SUPP_CUST_ADDR || ', ' || SUPP_CUST_CITY || ', ' || SUPP_CUST_STATE", "LOCATION_ID", SqlStr)

        If AcName <> "" Then
            txtSupplier.Text = AcName
            txtBillTo.Text = AcName3
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub
    Private Sub txtsupplier_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSupplier.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        If txtSupplier.Text = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtSupplier.Text = UCase(Trim(txtSupplier.Text))
        Else
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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
    Private Sub txtRemarks1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks1.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRemarks1_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks1.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks1.Text)
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
        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='S' ORDER BY NAME "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleType, ADODB.LockTypeEnum.adLockReadOnly)
        If RsSaleType.EOF = False Then
            Do While Not RsSaleType.EOF
                cboInvType.Items.Add(RsSaleType.Fields("NAME").Value)
                RsSaleType.MoveNext()
            Loop
        End If
        cboReason.Items.Clear()
        cboReason.Items.Add("1. Rate Diff")
        cboReason.Items.Add("2. Shortage")
        cboReason.Items.Add("3. Others")
        cboReason.Items.Add("4. Reversed Supp Bill")
        cboReason.Items.Add("5. Only GST Credit Note")
        cboReason.Items.Add("6. Reversed Invoice")
        cboReason.SelectedIndex = 0
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
    Private Sub txtVNoPrefix_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNoPrefix.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
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
                mPartNo = Trim(IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value))
                mHSNCode = Trim(IIf(IsDBNull(RsTemp.Fields("HSN_CODE").Value), "", RsTemp.Fields("HSN_CODE").Value))
            Else
                GoTo NextRecord
            End If



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

            xSqlStr = " SELECT B.HSNCODE " & vbCrLf _
                    & " FROM FIN_INVOICE_HDR A, FIN_INVOICE_DET B " & vbCrLf _
                    & " WHERE A.MKEY=B.MKEY AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND A.BILLNO='" & mOBillNo & "' AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(mItemCode) & "'"

            MainClass.UOpenRecordSet(xSqlStr, LocalPubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mHSNCode = Trim(IIf(IsDBNull(RsTemp.Fields("HSNCODE").Value), "", RsTemp.Fields("HSNCODE").Value))
            End If

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
    Private Sub SprdPaymentDetail_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdPaymentDetail.Change
        MainClass.SaveStatus(frmAtrn.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdPaymentDetail_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdPaymentDetail.ClickEvent

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCompanyCode As Long
        Dim mShortName As String
        Dim mSupplierCode As String

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        Else
            mSupplierCode = "-1"
        End If
        Select Case eventArgs.col
            Case 0
                If eventArgs.row > 0 Then
                    MainClass.DeleteSprdRow(SprdPaymentDetail, eventArgs.row, ColPayBillNo)
                    MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                    CalcTots()
                End If
            Case ColPayBillNo
                If eventArgs.row = 0 Then
                    SearchBill(mSupplierCode)
                End If
        End Select
        Exit Sub
ERR1:
        ''Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdPaymentDetail_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdPaymentDetail.KeyDownEvent

        Dim mPayType As String
        Dim mActiveCol As Integer
        Dim mActiveRow As Integer
        Dim mSupplierCode As String

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        Else
            mSupplierCode = "-1"
        End If

        mActiveCol = SprdPaymentDetail.ActiveCol
        mActiveRow = SprdPaymentDetail.ActiveRow

        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColPayPaymentAmt Then
                SprdPaymentDetail.Row = SprdPaymentDetail.ActiveRow
                SprdPaymentDetail.Col = ColPayPaymentAmt
                If Val(SprdPaymentDetail.Text) <> 0 Then
                    If SprdPaymentDetail.MaxRows = SprdPaymentDetail.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdPaymentDetail, ColPayBillNo, ConRowHeight)
                        FormatSprdPaymentDetail((SprdPaymentDetail.MaxRows), False)
                        MainClass.SetFocusToCell(SprdPaymentDetail, mActiveRow, ColPayPaymentAmt)
                    End If
                End If

            End If
        ElseIf eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then
            If SprdPaymentDetail.ActiveCol = ColPayBillNo Then SearchBill(mSupplierCode)
        End If
        eventArgs.keyCode = 9999
    End Sub
    Private Sub SprdPaymentDetail_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdPaymentDetail.LeaveCell

        On Error GoTo ERR1

        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        Dim mPayType As String
        Dim mBillNo As String
        Dim mAmount As Double
        Dim mBillDate As String
        Dim mDueDays As Double
        Dim mPayCode As String
        Dim mPONo As String
        Dim mAccountCode As String = ""
        Dim mPrevBillAmount As Double
        Dim mCurrBillAmount As Double
        Dim mPOAmount As Double
        Dim mCompanyCode As Long
        Dim mCurrCompanyCode As Long
        Dim mBillCompanyName As String
        Dim mSupplierCode As String
        Dim pRowNo As Long
        If eventArgs.newRow = -1 Then Exit Sub

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        Else
            mSupplierCode = "-1"
        End If

        SprdPaymentDetail.Row = eventArgs.row
        pRowNo = eventArgs.row

        SprdPaymentDetail.Col = ColPayBillNo
        mBillNo = SprdPaymentDetail.Text

        SprdPaymentDetail.Col = ColPayBillDate
        mBillDate = SprdPaymentDetail.Text


        mCompanyCode = RsCompany.Fields("COMPANY_CODE").Value   ''GetCompanyCode(mBillNo, mBillDate, lblAccountCode.Text)       ' IIf(Val(SprdPaymentDetail.Text) <= 0, RsCompany.Fields("COMPANY_CODE").Value, Val(SprdPaymentDetail.Text))

        Dim mAccountName As String
        Select Case eventArgs.col

            Case ColPayBillNo

                If DuplicatePayBillNo(pRowNo) = False Then
                    If CheckBillNo(mSupplierCode, pRowNo) = True Then

                    End If
                    SprdPaymentDetail.Row = eventArgs.row

                    SprdPaymentDetail.Col = ColPayBillNo
                    mBillNo = SprdPaymentDetail.Text

                    '-------- FILLING BILL AMT TO AMT COL

                    SprdPaymentDetail.Col = ColPayBalAmount
                    mAmount = Val(SprdPaymentDetail.Text)
                    SprdPaymentDetail.Col = ColPayPaymentAmt
                    If Val(SprdPaymentDetail.Text) = 0 Then
                        SprdPaymentDetail.Text = IIf(Val(lblDiffAmt.Text) >= mAmount, mAmount, Val(lblDiffAmt.Text))
                    End If
                    '                MainClass.SetFocusToCell SprdPaymentDetail, Row, ColPayPaymentAmt
                    '                SprdPaymentDetail.Col = ColPayType
                End If
            Case ColPayBillDate
                SprdPaymentDetail.Row = eventArgs.row
                pRowNo = eventArgs.row
                If DuplicatePayBillNo(pRowNo) = False Then
                    If CheckBillNo(mSupplierCode, pRowNo) = True Then

                    End If
                    If mPayType = "N" Then
                        SprdPaymentDetail.Row = eventArgs.row
                        SprdPaymentDetail.Col = ColPayBillDate
                        mBillDate = SprdPaymentDetail.Text

                        SprdPaymentDetail.Col = ColPayPaymentAmt
                        If Val(SprdPaymentDetail.Text) = 0 Then SprdPaymentDetail.Text = CStr(Val(lblDiffAmt.Text))
                    End If
                End If
            Case ColPayPaymentAmt
                SprdPaymentDetail.Row = eventArgs.row        ''SprdPaymentDetail.ActiveRow
                SprdPaymentDetail.Col = ColPayBillNo
                mBillNo = SprdPaymentDetail.Text
                SprdPaymentDetail.Col = ColPayPaymentAmt

                If CheckPayAmount() = False Then
                    MainClass.SetFocusToCell(SprdPaymentDetail, eventArgs.row, ColPayPaymentAmt)
                    Exit Sub
                End If

            Case ColPayBalDC
                SprdPaymentDetail.Col = ColPayBalDC
                SprdPaymentDetail.Row = eventArgs.row
                If UCase(SprdPaymentDetail.Text) = "DR" Or UCase(SprdPaymentDetail.Text) = "D" Then
                    SprdPaymentDetail.Text = "Dr"
                    Exit Sub
                ElseIf UCase(SprdPaymentDetail.Text) = "CR" Or UCase(SprdPaymentDetail.Text) = "C" Then
                    SprdPaymentDetail.Text = "Cr"
                    Exit Sub
                Else
                    eventArgs.col = ColPayBalDC
                    SprdPaymentDetail.Text = "Dr"
                    Exit Sub
                End If
                '            If Row <> NewRow Then CheckForEqualAmount

        End Select
        CalcTotsPayment()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Function DuplicatePayBillNo(ByRef pRowNo As Long) As Boolean

        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckBillNo As String
        Dim mBillNo As String
        Dim mFYear As Integer

        With SprdPaymentDetail
            .Row = pRowNo   ''.ActiveRow
            .Col = ColPayBillNo
            mCheckBillNo = Trim(UCase(.Text))

            .Col = ColPayBillDate
            If Trim(.Text) <> "" Then
                If Month(CDate(VB6.Format(.Text, "DD/MM/YYYY"))) >= 4 And Month(CDate(VB6.Format(.Text, "DD/MM/YYYY"))) <= 12 Then
                    mFYear = CInt(VB6.Format(.Text, "YYYY"))
                Else
                    mFYear = CDbl(VB6.Format(.Text, "YYYY")) - 1
                End If
            End If

            mCheckBillNo = mCheckBillNo & ":" & VB6.Format(mFYear, "0000")

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColPayBillNo
                mBillNo = Trim(UCase(.Text))

                .Col = ColPayBillDate
                If Trim(.Text) <> "" Then
                    If Month(CDate(VB6.Format(.Text, "DD/MM/YYYY"))) >= 4 And Month(CDate(VB6.Format(.Text, "DD/MM/YYYY"))) <= 12 Then
                        mFYear = CInt(VB6.Format(.Text, "YYYY"))
                    Else
                        mFYear = CDbl(VB6.Format(.Text, "YYYY")) - 1
                    End If
                End If
                mBillNo = mBillNo & ":" & VB6.Format(mFYear, "0000")

                If (mBillNo = mCheckBillNo And mCheckBillNo <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicatePayBillNo = True
                    MainClass.SetFocusToCell(SprdPaymentDetail, pRowNo, ColPayBillNo, "Duplicate Bill No. : " & Mid(mCheckBillNo, 2))
                    Exit Function
                End If
            Next
        End With
    End Function
    Private Function CheckBillNo(ByRef pSupplierCode As String, ByRef pRowNo As Long) As Boolean
        On Error GoTo ERR1
        Dim RS As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        Dim mPayType As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillAmount As Double
        Dim mDC As String
        Dim mPaymentAmt As Double

        Dim mBalance As Double
        Dim mRow As Integer
        Dim cntRow As Integer
        Dim mOldAmount As Double

        With SprdPaymentDetail
            mRow = pRowNo ''.ActiveRow
            .Row = mRow
            .Col = ColPayBillNo
            mBillNo = Trim(.Text)

            If mBillNo = "" Then
                .Row = mRow
                .Col = ColPayBillNo
                .Text = ""

                .Col = ColPayBillDate
                .Text = ""

                .Col = ColPayBillAmount
                .Text = "0.00"

                .Col = ColPayBalAmount
                .Text = "0.00"

                .Col = ColPayPaymentAmt
                .Text = "0.00"

                CheckBillNo = True
                Exit Function
            End If



            .Col = ColPayBillDate
            mBillDate = .Text

            Call GetBalanceAmount(mRow, pSupplierCode, mBillNo, mBillDate, "B")
            'Call PickUpBillPayment("B", mBillNo, mOldAmount, "D")

        End With
        CheckBillNo = True
        Exit Function
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub FormatSprdPaymentDetail(ByRef Arow As Integer, ByRef mFromPopulate As Boolean)

        On Error GoTo ErrPart
        Dim RsTRN As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        SqlStr = "SELECT * FROM FIN_POSTED_TRN WHERE 1=2"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRN, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdPaymentDetail
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = 0
            .set_ColWidth(0, 3)

            .Col = ColPayBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsTRN.Fields("BillNo").DefinedSize ''
            .set_ColWidth(.Col, 12)

            .ColsFrozen = ColPayBillNo


            .Col = ColPayBillDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(.Col, 9)


            .Col = ColPayBillAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 9)


            .Col = ColPayBalAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 9)

            .Col = ColPayBalDC
            .CellType = SS_CELL_TYPE_EDIT
            If mFromPopulate = False Then
                .Text = "Cr"    ''IIf(VB.Left(lblDC.Text, 1) = "C", "Cr", "Dr")
            End If
            .set_ColWidth(.Col, 3)

            .Col = ColPayPaymentAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 9)

            .Row = Arow
            MainClass.ProtectCell(SprdPaymentDetail, 1, .MaxRows, ColPayBillDate, ColPayBalDC)
            'MainClass.ProtectCell(SprdPaymentDetail, 1, .MaxRows, ColCompanyCode, ColCompanyCode)
            MainClass.SetSpreadColor(SprdPaymentDetail, Arow)


        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub GetBalanceAmount(ByRef pRow As Integer, ByRef pAccountCode As String, ByRef pBillNo As String, ByRef pBillDate As String, ByRef pPayType As String)

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBalance As Double
        Dim mActBillAmount As Double
        Dim mBillAmount As Double
        Dim mPaymentAmt As Double
        Dim mDueDays As Double
        Dim mBillDate As String
        Dim mPayCode As String
        Dim mBillDC As String
        Dim mDivCode As Double
        Dim mLocCode As String
        Dim mCompanyCode As Long
        Dim mCompanyName As String

        SqlStr = " Select Company_Code,BillNo, BillDate,MAX(EXPDATE) AS DueDate , " & vbCrLf _
            & " SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) AS BillAMT, " & vbCrLf _
            & " SUM(DECODE(BILLTYPE,'B',0,1)*DECODE(DC,'D',1,-1)*Amount) AS PayAMT " & vbCrLf _
            & " FROM FIN_POSTED_TRN  "

        SqlStr = SqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " FYEAR =" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND ACCOUNTCODE = '" & MainClass.AllowSingleQuote(pAccountCode) & "'"

        'If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        'Else
        SqlStr = SqlStr & vbCrLf & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""
        'End If
        SqlStr = SqlStr & vbCrLf & " AND BillNo='" & MainClass.AllowSingleQuote(pBillNo) & "'"

        SqlStr = SqlStr & vbCrLf & " AND VNO<>'" & MainClass.AllowSingleQuote(txtVNoPrefix.Text & txtVNo.Text) & "'"

        ''18-03-2010  ''Check New Bill Also.....
        If pPayType = "N" Then
            SqlStr = SqlStr & vbCrLf & " AND BillDate>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND BillDate<=TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            If pBillDate <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND BillDate=TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY Company_Code,BillNo, BillDate " & vbCrLf & " ORDER BY BillNo, BillDate,ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount))-SUM(DECODE(BILLTYPE,'B',0,1)*DECODE(DC,'D',1,-1)*Amount) DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdPaymentDetail
            .Row = pRow
            If RsTemp.EOF = False Then


                mCompanyCode = RsCompany.Fields("COMPANY_CODE").Value

                .Col = ColPayBillDate
                mBillDate = IIf(IsDBNull(RsTemp.Fields("BillDate").Value), "", VB6.Format(RsTemp.Fields("BillDate").Value, "DD/MM/YYYY"))
                .Text = IIf(IsDBNull(RsTemp.Fields("BillDate").Value), "", VB6.Format(RsTemp.Fields("BillDate").Value, "DD/MM/YYYY"))

                .Col = ColPayBillAmount
                mActBillAmount = GetBillAmount(pAccountCode, pBillNo, mBillDate, Val(IIf(IsDBNull(RsTemp.Fields("BILLAMT").Value), 0, RsTemp.Fields("BILLAMT").Value)))
                mBillAmount = Val(IIf(IsDBNull(RsTemp.Fields("BILLAMT").Value), 0, RsTemp.Fields("BILLAMT").Value))
                .Text = Str(System.Math.Abs(mActBillAmount))

                '.Col = ColBillAmountDC
                '.Text = IIf(mActBillAmount >= 0, "Dr", "Cr")
                'mBillDC = IIf(mBillAmount >= 0, "Dr", "Cr")

                .Col = ColPayBalAmount
                mPaymentAmt = Val(IIf(IsDBNull(RsTemp.Fields("PAYAMT").Value), 0, RsTemp.Fields("PAYAMT").Value))
                mBalance = mBillAmount + mPaymentAmt
                .Text = Str(System.Math.Abs(mBalance))
                '.Text = Str(Abs(mBalance) + Abs(mPRAmount))

                .Col = ColPayBalDC
                If mBalance = 0 Then
                    .Text = mBillDC
                Else
                    .Text = IIf(mBalance > 0, "Dr", "Cr")
                End If

                '********************
                .Row = .MaxRows
                .Row2 = .MaxRows
                .Col = 1
                .Col2 = .MaxCols
                .BlockMode = True
                .BackColor = System.Drawing.ColorTranslator.FromOle(IIf(UCase(mBillDC) = "CR", &H8000000F, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White))) ''&H80FF80
                .BlockMode = False
                '********************
            End If
        End With
    End Sub
    Private Function GetBillAmount(ByRef xAccountCode As String, ByRef xBillNo As String, ByRef xBillDate As String, ByRef xBillAmount As Double) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheck As Integer
        Dim mBillYear As Integer


        mBillYear = GetCurrentFYNo(PubDBCn, xBillDate)
        If mBillYear = RsCompany.Fields("FYEAR").Value Then
            GetBillAmount = xBillAmount
            Exit Function
        End If

        mCheck = 1

NextSearch:
        GetBillAmount = 0
        SqlStr = " Select SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) AS BillAMT " & vbCrLf _
            & " FROM FIN_POSTED_TRN  " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " ACCOUNTCODE = '" & MainClass.AllowSingleQuote(xAccountCode) & "'"

        'If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        'Else
        SqlStr = SqlStr & vbCrLf & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""
        'End If
        If mCheck = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE<>'O'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND BOOKTYPE='O'"
        End If

        '    SqlStr = SqlStr & vbCrLf & " AND DIV_CODE =" & Val(lblDivisionCode.text) & ""
        SqlStr = SqlStr & vbCrLf & " AND BillNo='" & MainClass.AllowSingleQuote(xBillNo) & "'"
        SqlStr = SqlStr & vbCrLf & " AND BillDate=TO_DATE('" & VB6.Format(xBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetBillAmount = IIf(IsDBNull(RsTemp.Fields("BillAMT").Value), 0, RsTemp.Fields("BillAMT").Value)
        Else
            If mCheck = 2 Then
                GetBillAmount = 0
            Else
                '            mBillYear = GetCurrentFYNo(PubDBCn, xBillDate)
                If mBillYear = RsCompany.Fields("FYEAR").Value Then
                    GetBillAmount = 0
                Else
                    mCheck = 2
                    GoTo NextSearch
                End If
            End If
        End If
        Exit Function
ErrPart:
        GetBillAmount = 0
    End Function
    Private Sub SearchBill(ByRef pSupplierCode As String)

        'Dim I As Integer
        Dim SqlStr As String = ""
        Dim mBillAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mPayAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String

        mBillAmtStr = "SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
        mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        mPayAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        ''mBalAmtStr = "ABS(" & mBillAmtStr & ")-ABS(" & mPayAmtStr & ")"
        mBalAmtStr = "" & mBillAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " +" & mCNAmtStr & " + " & mPayAmtStr & ""


        mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf _
            & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf _
            & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf _
            & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf _
            & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"

        SqlStr = " Select BillNo, BillDate, LOCATION_ID," & vbCrLf _
            & " TO_CHAR(ABS(" & mBillAmtStr & ")) AS BillAMT, " & vbCrLf _
            & " CASE WHEN " & mBillAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS DC , " & vbCrLf _
            & " TO_CHAR(ABS(" & mADVAmtStr & ")) AS ADV, " & vbCrLf _
            & " TO_CHAR(ABS(" & mDNAmtStr & ")) AS DNOTE, " & vbCrLf _
            & " TO_CHAR(ABS(" & mCNAmtStr & ")) AS CNOTE, " & vbCrLf _
            & " TO_CHAR(ABS(" & mTDSAmtStr & ")) AS TDS, " & vbCrLf _
            & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS BALANCE, " & vbCrLf _
            & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS DC, MAX(DUEDATE) AS DUEDATE,COMPANY_CODE  " & vbCrLf _
            & " FROM FIN_POSTED_TRN  " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AccountCode = '" & pSupplierCode & "'"      '' AND TRNTYPE='B'"

        'If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        'Else
        SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        'End If
        '    SqlStr = SqlStr & vbCrLf & " AND DIV_CODE =" & Val(lblDivisionCode.text) & ""

        SqlStr = SqlStr & vbCrLf & " AND LOCATION_ID='" & txtBillTo.Text & "'"

        SqlStr = SqlStr & vbCrLf & " GROUP BY  BillDate, BillNo,COMPANY_CODE,LOCATION_ID" & vbCrLf _
            & " HAVING " & mBalAmtStr & " <>0 " & vbCrLf _
            & " ORDER BY BillDate, BillNo "

        MainClass.SearchGridMasterBySQL("", SqlStr)

        If AcName <> "" Then
            SprdPaymentDetail.Row = SprdPaymentDetail.ActiveRow
            SprdPaymentDetail.Col = ColPayBillNo
            SprdPaymentDetail.Text = AcName
            SprdPaymentDetail.Col = ColPayBillDate
            SprdPaymentDetail.Text = AcName1
            MainClass.SetFocusToCell(SprdPaymentDetail, SprdPaymentDetail.ActiveRow, ColPayBillNo)
        End If
        Exit Sub

    End Sub
    Private Function CheckPayAmount() As Boolean
        Dim mDC As String
        Dim mBalance As Double
        Dim mBalanceDC As String
        Dim mOldAmount As Double
        Dim mOldDC As String
        Dim mNetBalance As Double
        Dim mCurrAmount As Double

        With SprdPaymentDetail

            .Col = ColPayBalDC
            mBalanceDC = VB.Left(.Text, 1)

            .Col = ColPayBalAmount
            mBalance = Val(.Text) * IIf(mBalanceDC = "D", 1, -1)

            mNetBalance = mBalance + mOldAmount

            mDC = mBalanceDC

            .Col = ColAmount
            mCurrAmount = Val(.Text) * IIf(mDC = "D", -1, 1)

            If System.Math.Abs(mCurrAmount) > System.Math.Abs(mNetBalance) Then
                ErrorMsg("Amount Exceeds", "", MsgBoxStyle.Critical)
                CheckPayAmount = False
            Else
                CheckPayAmount = True
            End If


        End With
    End Function
    Private Sub CalcTotsPayment()
        On Error GoTo ErrSprdTotal
        Dim mDAmt As Double = 0
        Dim mCAmt As Double = 0
        Dim mNetAmt As Double = 0
        Dim MTotalAmt As Double = 0
        Dim cntRow As Integer = 0
        Dim mDC As String = ""
        Dim mDrCr As String = ""

        With SprdPaymentDetail
            For cntRow = 1 To .MaxRows - 1 Step 1
                .Row = cntRow


                .Col = ColPayBalDC
                mDC = VB.Left(.Text, 1)

                .Col = ColPayPaymentAmt
                If mDC = "D" Then
                    mDAmt = mDAmt + Val(.Value)
                Else
                    mCAmt = mCAmt + Val(.Value)
                End If
NextRow:
            Next cntRow
        End With

        mNetAmt = System.Math.Abs(mCAmt - mDAmt)
        mNetAmt = VB6.Format(mNetAmt, "0.00")

        lblPaymentTotal.Text = VB6.Format(mNetAmt, "0.00")
        lblPaymentDC.Text = IIf(mCAmt - mDAmt > 0, "Cr", "Dr")

        lblDiffAmt.Text = Val(lblNetAmount.Text) - Val(mNetAmt)

ErrSprdTotal:
    End Sub
    Private Function UpdatePaymentDetail1(ByRef pSuppCustCode As String, ByRef pDivCode As Double) As Boolean
        On Error GoTo UpdatePaymentDetail1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mPayBillNo As String
        Dim mPayBillDate As String
        Dim mPayBillAmount As Double
        Dim mPayBalDC As String
        Dim mPayPaymentAmt As Double


        PubDBCn.Execute("Delete From FIN_PURBILLDETAILS_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & UCase(mBookType) & "'")

        With SprdPaymentDetail
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColPayBillNo
                mPayBillNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColPayBillDate
                mPayBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColPayBillAmount
                mPayBillAmount = Val(.Text)

                .Col = ColPayBalDC
                mPayBalDC = Mid(.Text, 1, 1)

                .Col = ColPayPaymentAmt
                mPayPaymentAmt = Val(.Text)

                SqlStr = ""
                If mPayBillNo <> "" And mPayPaymentAmt > 0 Then
                    SqlStr = " INSERT INTO FIN_PURBILLDETAILS_TRN (COMPANY_CODE, " & vbCrLf _
                        & " MKEY , SUBROWNO, " & vbCrLf _
                        & " ACCOUNTCODE , BILLNO, BILLDATE, BILLAMOUNT, BILLDC, " & vbCrLf _
                        & " AMOUNT , DC, BOOKTYPE) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & LblMKey.Text & "'," & I & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(pSuppCustCode) & "','" & mPayBillNo & "',TO_DATE('" & VB6.Format(mPayBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & mPayBillAmount & ", " & vbCrLf _
                        & " '" & mPayBalDC & "'," & mPayPaymentAmt & ",'" & mPayBalDC & "','" & UCase(mBookType) & "') "

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdatePaymentDetail1 = True
        Exit Function
UpdatePaymentDetail1:
        UpdatePaymentDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Sub ShowPaymentDetail1(ByRef mMkey As String, ByRef mSupplierCode As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim SqlStr As String = ""
        Dim mBillNo As String
        Dim mBillDate As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM FIN_PURBILLDETAILS_TRN " & vbCrLf _
            & " Where Mkey='" & mMkey & "' AND BookType='" & UCase(mBookType) & "'" & vbCrLf _
            & " Order By SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTemp
            If .EOF = True Then Exit Sub
            FormatSprdPaymentDetail(-1, False)
            I = 1
            .MoveFirst()
            Do While Not .EOF
                SprdPaymentDetail.Row = I
                SprdPaymentDetail.Col = ColPayBillNo
                SprdPaymentDetail.Text = IIf(IsDBNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                mBillNo = IIf(IsDBNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)

                SprdPaymentDetail.Col = ColPayBillDate
                SprdPaymentDetail.Text = VB6.Format(IIf(IsDBNull(.Fields("BILLDATE").Value), "", .Fields("BILLDATE").Value), "DD/MM/YYYY")
                mBillDate = VB6.Format(IIf(IsDBNull(.Fields("BILLDATE").Value), "", .Fields("BILLDATE").Value), "DD/MM/YYYY")

                SprdPaymentDetail.Col = ColPayBillAmount
                SprdPaymentDetail.Text = CStr(Val(IIf(IsDBNull(.Fields("BILLAMOUNT").Value), 0, .Fields("BILLAMOUNT").Value)))

                'SprdPaymentDetail.Col = ColPayBalAmount
                'SprdPaymentDetail.Text = CStr(Val(IIf(IsDBNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)))

                SprdPaymentDetail.Col = ColPayBalDC
                SprdPaymentDetail.Text = IIf(IsDBNull(.Fields("DC").Value), "D", .Fields("DC").Value)

                SprdPaymentDetail.Col = ColPayPaymentAmt
                SprdPaymentDetail.Text = CStr(Val(IIf(IsDBNull(.Fields("AMOUNT").Value), 0, .Fields("AMOUNT").Value)))

                Call GetBalanceAmount(I, (mSupplierCode), mBillNo, mBillDate, "B")

                .MoveNext()
                I = I + 1
                SprdPaymentDetail.MaxRows = I
            Loop
        End With
        CalcTotsPayment()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub

    Private Sub CmdUpdatePayment_Click(sender As Object, e As EventArgs) Handles CmdUpdatePayment.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        cboInvType_Validating(cboInvType, New System.ComponentModel.CancelEventArgs(False))
        'If FieldsVarification() = False Then
        '    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Exit Sub
        'End If
        Call CalcTots()
        If UpdateMain1("P") = True Then
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

    Private Sub FrmCust_SaleGST_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 350, mReFormWidth - 350, mReFormWidth))
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        'Frame1.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame6.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))

        SSTab1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdPopPaymentFromFile_Click(sender As Object, e As EventArgs) Handles CmdPopPaymentFromFile.Click
        On Error GoTo ErrPart
        Dim strFilePath As String
        Dim mFormat As String

        ' Let user locate the Excel file.
        '

        DataLoading = True
        strFilePath = My.Application.Info.DirectoryPath

        If Not fOpenFile(strFilePath, "*.xls", "Excel Data", CommonDialogOpen) Then
            GoTo NormalExit
        End If

        '
        ' Load it into the grid.

        Call PopulateFromXLSTVSNewFile(strFilePath)

        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
        '    mFormat = InputBox("Press 1 for TVS, 2 for HHML Format : ", "Format", "")

        '    'If Val(mFormat) = 1 Then
        '    Call PopulateFromXLSTVSNewFile(strFilePath)
        '    'ElseIf Val(mFormat) = 2 Then
        '    '    Call PopulateFromXLSHHMLFile(strFilePath)
        '    'End If

        '    'ElseIf RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
        '    '    mFormat = InputBox("Press 1 for Honda, 2 for Honda SPD, 3 For Yamaha & 4 For JCB Format : ", "Format", "")

        '    '    Call PopulateFromFile_RR(strFilePath, mFormat)


        'Else
        '    mFormat = InputBox("Press 1 for Format : ", "Format", "")
        'End If




        DataLoading = False
NormalExit:
        Exit Sub
ErrPart:
    End Sub
    Private Sub PopulateFromXLSTVSNewFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mBillAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mPayAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String
        Dim mPayType As String
        Dim RsTempPRDetail As ADODB.Recordset = Nothing
        Dim mDrCr As String = ""

        Dim mSqlStr As String
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String

        Dim mFileBillNo As String
        Dim mFileTrnType As String
        Dim mFileAmount As Double
        Dim mFileAmountStr As String
        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""
        Dim xAccountCode As String
        Dim xAccountAlias As String
        Dim mFileBillDate As String = ""
        Dim mFileBillFromDate As String = ""
        Dim mDivCode As Double
        Dim mLocCode As String
        Dim mCompanyCode As Long
        Dim mLocationID As String
        Dim mOnAccountPayment As Double = 0
        Dim mBillBalAmount As Double = 0
        Dim mBillBalDC As String
        Dim mPaymentPostAmount As Double = 0
        Dim mPaymentBalAmount As Double = 0
        Dim pRowNo As Long
        'Dim FPath As String

        'Dim ErrorFile As System.IO.StreamWriter

        'FPath = mPubBarCodePath & "\BillImportError.txt"

        'If FILEExists(FPath) Then
        '    Kill(FPath)
        'End If

        Dim mSupplierCode As String

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        Else
            mSupplierCode = "-1"
        End If

        'ErrorFile = My.Computer.FileSystem.OpenTextFileWriter(FPath, True)

        'If MainClass.ValidateWithMasterTable(Trim(lblAccountCode.Text), "SUPP_CUST_CODE", "ALIAS_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    xAccountAlias = MasterNo
        'Else
        '    xAccountAlias = ""
        'End If
        mFileBillFromDate = "01/04/2022"

        ''MainClass.ClearGrid(SprdPaymentDetail)

        '' MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPayType, SprdMain.MaxCols)

        '    Call GetExcelRecord

        '    FileConnStr = "DSN=PAYMENT"
        '    Set FileDBCn = New ADODB.Connection
        '    FileDBCn.Open FileConnStr

        FileConnStr = "Provider=MSDASQL.1;Connect Timeout=15;Extended Properties='DSN=Excel Files;DBQ=XXLSFILEX;DefaultDir=XXLSDIRX;DriverId=790;FIL=excel 8.0;MaxBufferSize=2048;PageTimeout=5;UID=admin;';Locale Identifier=1033"
        FileConnStr = Replace(FileConnStr, "XXLSFILEX", strXLSFile)
        strTemp = Mid(strXLSFile, 1, InStrRev(strXLSFile, "\") - 1)
        FileConnStr = Replace(FileConnStr, "XXLSDIRX", strTemp)

        If Not XLSConnect(Trim(FileConnStr), FileDBCn) Then
            GoTo ErrPart
        End If

        RsFile = FileDBCn.OpenSchema(ADODB.SchemaEnum.adSchemaTables)
        strWkShName = RsFile.Fields("Table_Name").Value

        mSqlStr = "SELECT * FROM ""XWKSHTX"" " ''WHERE F1 <> NULL"
        mSqlStr = Replace(mSqlStr, "XWKSHTX", strWkShName)

        '    MainClass.UOpenRecordSet mSqlStr, FileDBCn, adOpenStatic, RsFile
        '    RsFile.Open mSqlStr, FileDBCn, , adLockReadOnly, adCmdText
        If OpenExcelRecordSet(mSqlStr, RsFile, strError, FileDBCn, False) = 0 Then

            If RsFile.EOF = False Then
                Do While Not RsFile.EOF
                    mFileBillNo = IIf(IsDBNull(RsFile.Fields(0).Value), "", RsFile.Fields(0).Value)
                    mFileBillDate = VB6.Format(IIf(IsDBNull(RsFile.Fields(1).Value), "", RsFile.Fields(1).Value), "DD/MM/YYYY")
                    mFileAmountStr = IIf(IsDBNull(RsFile.Fields(3).Value), "", RsFile.Fields(3).Value)
                    mFileAmount = Val(mFileAmountStr)



                    With SprdPaymentDetail
                        .Row = .MaxRows
                        pRowNo = .MaxRows

                        .Col = ColPayBillNo
                        .Text = mFileBillNo

                        .Col = ColPayBillDate
                        .Text = mFileBillDate

                        If DuplicatePayBillNo(pRowNo) = False Then
                            If CheckBillNo(mSupplierCode, pRowNo) = True Then

                            End If
                            .Row = .MaxRows

                            .Col = ColPayPaymentAmt
                            .Text = VB6.Format(System.Math.Abs(mFileAmount), "0.00") ''Val(RsTempPRDetail.Fields("Amount").Value)

                            .MaxRows = .MaxRows + 1
                        Else
                            If MsgQuestion("Want to Continue ? ") = CStr(MsgBoxResult.No) Then
                                GoTo ExitExcelFile
                            End If
                        End If
                    End With

NextRecord:
                    RsFile.MoveNext()
                Loop
            End If
        End If
ExitExcelFile:
        FormatSprdPaymentDetail(-1, True)
        'SetSprdCellFormat()
        CalcTotsPayment()

        MainClass.ProtectCell(SprdPaymentDetail, 1, SprdPaymentDetail.MaxRows, ColPayBillDate, ColPayBalDC)
        If RsFile.State = ADODB.ObjectStateEnum.adStateOpen Then RsFile.Close()
        RsFile = Nothing

        If FileDBCn.State = ADODB.ObjectStateEnum.adStateOpen Then
            FileDBCn.Close()
            FileDBCn = Nothing
            '        FileDBCn = ""
        End If

        strTemp = ""
        strXLSFile = ""

        '        MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColBillDate, ColBalanceDC
        If SprdMain.Visible = True Then MainClass.SetFocusToCell(SprdMain, 1, ColBillNo)
        '    End With

        'ErrorFile.Close()

        'If FILEExists(FPath) Then
        '    Process.Start("notepad.exe", FPath)            ''Process.Start("explorer.exe", FPath)
        'End If


        Exit Sub
ErrPart:
        'ErrorFile.Close()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub
End Class
