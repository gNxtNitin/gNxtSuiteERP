Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmItemWarrantyEntry
    Inherits System.Windows.Forms.Form
    Dim RSROMain As ADODB.Recordset
    Dim RSRODetail As ADODB.Recordset

    'Private PvtDBCn As ADODB.Connection
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer

    Dim mSupplierCode As String
    Dim pRound As Double

    Private Const mBookType As String = "I"

    Private Const ConRowHeight As Short = 12

    Private Const ColPONo As Short = 1
    Private Const ColItemCode As Short = 2
    Private Const ColItemDesc As Short = 3
    Private Const ColUnit As Short = 4
    Private Const ColRate As Short = 5
    Private Const ColBillQty As Short = 6
    Private Const ColAcceptQty As Short = 7
    Private Const ColSerialNo As Short = 8
    Private Const ColMake As Short = 9
    Private Const ColInstDate As Short = 10
    Private Const ColWarranty As Short = 11
    Private Const ColRemarks As Short = 12

    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True

            txtMRRNo.Enabled = True
            txtMRRDate.Enabled = False
            cmdMRRSearch.Enabled = True
            txtRefNo.Enabled = False
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Clear1()
            Show1()
            SprdMain.Enabled = True

            txtMRRNo.Enabled = False
            txtMRRDate.Enabled = False
            cmdMRRSearch.Enabled = False
            txtRefNo.Enabled = True
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart

        Dim xDCNo As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBookCode As Integer

        If ValidateBranchLocking((txtRefDate.Text)) = True Then
            Exit Sub
        End If
        If ValidateBookLocking(PubDBCn, CInt(ConLockReoffer), txtRefDate.Text) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, txtRefDate.Text, (TxtSupplier.Text), mSupplierCode) = True Then
            Exit Sub
        End If

        If Trim(txtRefNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub


        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Re-offer Cann't be Deleted.")
            Exit Sub
        End If


        '    If CheckBillPayment(mSupplierCode, txtBillNo.Text, "B") = True Then Exit Sub

        If Not RSROMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_WARRANTY_HDR", (txtRefNo.Text), RSROMain, "AUTO_KEY_REF") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_WARRANTY_HDR", "AUTO_KEY_REF", (lblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("Delete from INV_WARRANTY_DET Where AUTO_KEY_REF=" & Val(lblMKey.Text) & "")
                PubDBCn.Execute("Delete from INV_WARRANTY_HDR Where AUTO_KEY_REF=" & Val(lblMKey.Text) & "")

                PubDBCn.CommitTrans()

                RSROMain.Requery() ''.Refresh
                RSRODetail.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RSROMain.Requery() ''.Refresh
        RSRODetail.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '        Resume
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If PubSuperUser = "U" Then
            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Cancelled Reoffer Cann't be Modified")
                Exit Sub
            End If
        End If

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RSROMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True

            txtRefNo.Enabled = False
            txtMRRNo.Enabled = False
            cmdMRRSearch.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdMRRSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdMRRSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""



        SqlStr = "SELECT DISTINCT IH.AUTO_KEY_MRR, IH.MRR_DATE " & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR " & vbCrLf & " AND IH.QC_STATUS='Y' AND IH.MRR_STATUS='N' " & vbCrLf & " AND (IH.SEND_AC_DATE IS NOT NULL OR IH.SEND_AC_DATE<>'')" & vbCrLf & " AND ID.REJECTED_QTY>0 ORDER BY IH.MRR_DATE,IH.AUTO_KEY_MRR DESC"

        If MainClass.SearchGridMasterBySQL2((txtMRRNo.Text), SqlStr) = True Then
            txtMRRNo.Text = AcName
            TxtMRRNo_Validating(TxtMRRNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONReOffer(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONReOffer(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportONReOffer(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String
        Exit Sub

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""
        Call SelectQryForMRR(SqlStr)
        Call MainClass.ClearCRptFormulas(Report1)

        mTitle = "Re-Offer MRR"
        mSubTitle = ""
        mRptFileName = "ReOfferMRR.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Function SelectQryForMRR(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC,"

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO"

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_WARRANTY_HDR IH, INV_WARRANTY_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"
        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.AUTO_KEY_REF=" & Val(txtRefNo.Text) & ""


        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForMRR = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdReOfferSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdReOfferSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster((txtRefNo.Text), "INV_WARRANTY_HDR", "AUTO_KEY_REF", "REF_DATE", "AUTO_KEY_MRR", "", SqlStr) = True Then
            txtRefNo.Text = AcName
            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
            If txtRefNo.Enabled = True Then txtRefNo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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

            If CmdAdd.Enabled = True And CmdAdd.Visible = True Then CmdAdd.Focus()
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

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        'Dim mItemCode As String
        'Dim DelStatus As Boolean


        '    If eventArgs.Col = 0 And eventArgs.Row > 0 Then
        '        SprdMain.Row=eventArgs.Row
        '        SprdMain.Col = ColItemCode
        '        If eventArgs.Row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
        '            SprdMain.Col = ColItemCode
        '            mItemCode = SprdMain.Text
        '
        '            MainClass.DeleteSprdRow SprdMain, Row, ColItemCode, DelStatus
        '            FormatSprdMain -1
        '            MainClass.SaveStatus Me, ADDMode, MODIFYMode
        '        End If
        '    End If

    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        Dim xInstDate As String
        Dim xWarranty As String

        If eventArgs.NewRow = -1 Then Exit Sub

        SprdMain.Row = eventArgs.Row


        Select Case eventArgs.Col

            Case ColInstDate
                SprdMain.Col = ColInstDate
                xInstDate = VB6.Format(SprdMain.Text, "DD/MM/YYYY")
                If IsDate(xInstDate) Then
                    If CDate(xInstDate) < CDate(txtMRRDate.Text) Then
                        MsgInformation("Installation Date Cann't be Less Than MRR Date.")
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColInstDate)
                        eventArgs.Cancel = True
                        Exit Sub
                    End If
                End If
            Case ColWarranty
                SprdMain.Col = ColInstDate
                xInstDate = VB6.Format(SprdMain.Text, "DD/MM/YYYY")

                SprdMain.Col = ColWarranty
                xWarranty = VB6.Format(SprdMain.Text, "DD/MM/YYYY")
                If IsDate(xWarranty) Then
                    If CDate(xWarranty) < CDate(txtMRRDate.Text) Then
                        MsgInformation("Warranty Date Cann't be Less Than MRR Date.")
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColWarranty)
                        eventArgs.Cancel = True
                        Exit Sub
                    End If
                End If

                If IsDate(xInstDate) And IsDate(xWarranty) Then
                    If CDate(xWarranty) < CDate(xInstDate) Then
                        MsgInformation("Warranty Date Cann't be Less Than Installation Date.")
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColWarranty)
                        eventArgs.Cancel = True
                        Exit Sub
                    End If
                End If


        End Select

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function DuplicateItemCode() As Boolean

        Dim CntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mItemCode As String
        Dim mPONo As Double
        Dim xCheckCode As String

        With SprdMain
            .Row = .ActiveRow
            .Col = ColPONo
            mCheckItemCode = CStr(Val(.Text))

            .Col = ColItemCode
            mCheckItemCode = mCheckItemCode & Trim(UCase(.Text))

            For CntRow = 1 To .MaxRows
                .Row = CntRow

                .Col = ColPONo
                mPONo = Val(.Text)

                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                xCheckCode = mPONo & mItemCode

                If (xCheckCode = mCheckItemCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItemCode = True
                    MsgInformation("Duplicate Item : " & mItemCode & " For PoNo : " & mPONo)
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                    Exit Function
                End If
            Next
        End With
    End Function

    Private Function FillGridRow(ByRef mPONo As String, ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mOrdQty As Object
        Dim mRecvQty As Double
        Dim xPoNo As String
        Dim xFYNo As Integer
        Dim xSupplierCode As Integer
        Dim mOrderSno As Integer
        Dim SqlStr As String = ""

        If mItemCode = "" Then Exit Function

        SqlStr = ""
        SqlStr = " SELECT Item_Code,ITEM_SHORT_DESC AS NAME," & vbCrLf & " PURCHASE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND Item_Code='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc

                SprdMain.Col = ColItemCode
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                SprdMain.Col = ColItemDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("Name").Value), "", .Fields("Name").Value))

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)
            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.row

            .Col = 1
            txtRefNo.Text = CStr(Val(.Text))

            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub


    Private Sub txtEMailID_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEMailID.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMRRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMRRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Not IsDate(txtMRRDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtMRRDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtMRRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtMRRNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.DoubleClick
        cmdMRRSearch_Click(cmdMRRSearch, New System.EventArgs())
    End Sub

    Private Sub TxtMRRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMRRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub TxtMRRNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMRRNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdMRRSearch_Click(cmdMRRSearch, New System.EventArgs())
    End Sub

    Public Sub TxtMRRNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim mMRRNO As Double
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(txtMRRNo.Text) = "" Then GoTo EventExitSub

        If Len(txtMRRNo.Text) < 6 Then
            txtMRRNo.Text = Val(txtMRRNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mMRRNO = Val(txtMRRNo.Text)

        If MainClass.ValidateWithMasterTable(mMRRNO, "AUTO_KEY_MRR", "AUTO_KEY_MRR", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            Clear1()
            Call ShowFromMRR(mMRRNO, True)
        Else
            MsgBox("Invalid MRR No.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim I As Short
        Dim SqlStr As String = ""
        Dim nMkey As String
        Dim mVNoSeq As Double
        Dim mSuppCustCode As String
        Dim mTotQty As Double
        Dim mCancelled As String
        Dim mPONOs As String
        Dim mQCStatus As String
        Dim mDivisionCode As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mSuppCustCode = CStr(-1)
        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = CStr(-1)
            MsgBox("Supplier Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mDivisionCode = -1
        If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = MasterNo
        Else
            mDivisionCode = -1
            MsgBox("Division Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If Val(txtRefNo.Text) = 0 Then
            mVNoSeq = CDbl(AutoGenSeqNo())
        Else
            mVNoSeq = Val(txtRefNo.Text)
        End If

        txtRefNo.Text = CStr(Val(CStr(mVNoSeq)))

        If CheckValidVDate(mVNoSeq) = False Then GoTo ErrPart

        SqlStr = ""

        If ADDMode = True Then
            lblMKey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO INV_WARRANTY_HDR( " & vbCrLf & " COMPANY_CODE, " & vbCrLf & " AUTO_KEY_REF, REF_DATE," & vbCrLf & " AUTO_KEY_MRR, CANCELLED_STATUS," & vbCrLf & " EMAIL_ID, REMARKS," & vbCrLf & " ADDUSER, ADDDATE) "


            SqlStr = SqlStr & vbCrLf & " VALUES( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & Val(CStr(mVNoSeq)) & ", TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & Val(txtMRRNo.Text) & ", '" & mCancelled & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtEMailID.Text)) & "','" & MainClass.AllowSingleQuote((TxtRemarks.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE INV_WARRANTY_HDR SET " & vbCrLf & " AUTO_KEY_REF =" & Val(CStr(mVNoSeq)) & " , REF_DATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & "," & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote((TxtRemarks.Text)) & "',"

            SqlStr = SqlStr & vbCrLf & " EMAIL_ID='" & MainClass.AllowSingleQuote(txtEMailID.Text) & "', " & vbCrLf & " CANCELLED_STATUS='" & mCancelled & "'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE AUTO_KEY_REF ='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(mSuppCustCode, mDivisionCode) = False Then GoTo ErrPart
        UpdateMain1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RSROMain.Requery() ''.Refresh
        RSRODetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '    Resume
    End Function

    Private Function CheckValidVDate(ByRef pREFNoSeq As Double) As Object

        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim mRsCheck1 As ADODB.Recordset = Nothing
        Dim mRsCheck2 As ADODB.Recordset = Nothing
        Dim mBackBillDate As String = ""
        Dim mMaxInvStrfNo As Integer
        CheckValidVDate = True

        If txtRefNo.Text = 1 & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") Then Exit Function

        SqlStr = "SELECT MAX(REF_DATE)" & vbCrLf & " FROM INV_WARRANTY_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_REF<" & Val(CStr(pREFNoSeq)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDbNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If

        SqlStr = "SELECT MIN(REF_DATE)" & " FROM INV_WARRANTY_HDR " & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_REF>" & Val(CStr(pREFNoSeq)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck1, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck1.EOF = False And Not IsDbNull(mRsCheck1.Fields(0).Value) And mRsCheck2.EOF = False And Not IsDbNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtRefDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("REF Date Is Greater Than The REF Date Of Next REF No.")
                CheckValidVDate = False
            ElseIf CDate(txtRefDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("REF Date Is Less Than The REF Date Of Previous REF No.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck1.EOF = False And Not IsDbNull(mRsCheck1.Fields(0).Value) Then
            If CDate(txtRefDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("REF Date Is Greater Than The REF Date Of Next REF No.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck2.EOF = False And Not IsDbNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtRefDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("REF Date Is Less Than The REF Date Of Previous REF No.")
                CheckValidVDate = False
            End If
        End If

        Exit Function
CheckERR:
        CheckValidVDate = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function AutoGenSeqNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RSROMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_REF)  " & vbCrLf & " FROM INV_WARRANTY_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSROMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RSROMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With
        AutoGenSeqNo = mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateDetail1(ByRef pSupplierCode As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetail1Err

        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mPONo As Double
        Dim mItemCode As String
        Dim mUnit As String
        Dim mBillQty As Double
        Dim mAcceptQty As Double

        Dim mItemSerialNo As String
        Dim mMake As String
        Dim mInstDate As String
        Dim mWarranty As String
        Dim mRemarks As String


        PubDBCn.Execute("Delete From INV_WARRANTY_DET Where AUTO_KEY_REF='" & lblMKey.Text & "'")

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColPONo
                mPONo = Val(.Text)

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColBillQty
                mBillQty = Val(.Text)

                .Col = ColAcceptQty
                mAcceptQty = Val(.Text)

                .Col = ColSerialNo
                mItemSerialNo = Trim(.Text)

                .Col = ColMake
                mMake = Trim(.Text)

                .Col = ColInstDate
                mInstDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColWarranty
                mWarranty = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColRemarks
                mRemarks = Trim(.Text)

                SqlStr = ""

                If mItemCode <> "" Then
                    SqlStr = " INSERT INTO INV_WARRANTY_DET ( " & vbCrLf & " COMPANY_CODE, AUTO_KEY_REF, SERIAL_NO, " & vbCrLf & " REF_PO_NO, ITEM_CODE," & vbCrLf & " ITEM_UOM, BILL_QTY," & vbCrLf & " ACCEPTED_QTY," & vbCrLf & " ITEM_SERIAL_NO, ITEM_MAKE, " & vbCrLf & " INST_DATE, WARRANTY_DUE_DATE," & vbCrLf & " REMARKS) "


                    SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & LblMkey.Text & "'," & I & ", " & vbCrLf & " " & Val(CStr(mPONo)) & ", '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf & " '" & mUnit & "', " & mBillQty & ", " & vbCrLf & " " & mAcceptQty & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mItemSerialNo) & "', '" & MainClass.AllowSingleQuote(mMake) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(mInstDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(mWarranty, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(mRemarks) & "'" & vbCrLf & " ) "


                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        '    If mRecord = False Then
        '        MsgInformation "Nothing to Save."
        '        UpdateDetail1 = False
        '        Exit Function
        '    End If

        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function


    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim CntRow As Integer
        Dim xShortageQty As Double
        Dim xRejectedQty As Double
        Dim xPORate As Double
        Dim xRate As Double
        Dim xRateDiffDN As Double
        Dim xRateDiffCN As Double
        Dim mExciseDutyAmt As Double
        Dim mSalesTaxAmount As Double
        Dim mWithInState As String
        Dim mReOfferedQty As Double
        Dim mAcceptQty As Double
        Dim mQty As Double
        Dim mRejStock As Double
        Dim mItemCode As String
        Dim mItemUOM As String = ""
        Dim mDespQty As Double
        Dim mDivisionCode As Double


        FieldsVarification = True
        If ValidateBranchLocking((txtRefDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ValidateBookLocking(PubDBCn, CInt(ConLockReoffer), txtRefDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ValidateAccountLocking(PubDBCn, txtRefDate.Text, (TxtSupplier.Text), mSupplierCode) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RSROMain.EOF = True Then Exit Function


        If MODIFYMode = True And txtRefNo.Text = "" Then
            MsgInformation("Ref No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtRefDate.Text = "" Then
            MsgBox("Ref Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtRefDate.Focus()
            Exit Function
        ElseIf FYChk((txtRefDate.Text)) = False Then
            FieldsVarification = False
            If txtRefDate.Enabled = True Then txtRefDate.Focus()
            Exit Function
        End If

        If txtMRRNo.Text = "" Then
            MsgInformation("MRR No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtMRRDate.Text = "" Then
            MsgBox("VDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtMRRDate.Focus()
            Exit Function
        End If

        If txtBillNo.Text = "" Then
            MsgInformation("Bill No. is Blank")
            FieldsVarification = False
            txtBillNo.Focus()
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
        If CDate(txtMRRDate.Text) < CDate(txtBillDate.Text) Then
            MsgBox("Bill Date Can Not be Less Than BillDate.")
            FieldsVarification = False
            txtMRRDate.Focus()
            Exit Function
        End If

        If CDate(txtMRRDate.Text) < CDate(txtBillDate.Text) Then
            MsgBox("Bill Date Can Not be Less Than BillDate.")
            FieldsVarification = False
            If txtBillDate.Enabled = True Then txtBillDate.Focus()
            Exit Function
        End If

        If Trim(TxtSupplier.Text) = "" Then
            MsgBox("Supplier Cannot Be Blank", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If

        mWithInState = "Y"
        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Supplier Does Not Exist In Master", MsgBoxStyle.Information)
            'txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        Else
            mWithInState = IIf(IsDbNull(MasterNo), "Y", MasterNo)
        End If

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If


        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Item Code Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColUnit, "S", "Item Unit Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColBillQty, "N", "Please Check Bill Quantity.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColAcceptQty, "N", "Please Check Acceptqty Quantity.") = False Then FieldsVarification = False : Exit Function

        If MainClass.ValidDataInGrid(SprdMain, ColSerialNo, "S", "Please Check Serial No.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColMake, "S", "Please Check Make.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColInstDate, "D", "Please Check Installation Date.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColWarranty, "D", "Please Check Warranty Date.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmItemWarrantyEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Item Warranty Entry"

        SqlStr = ""
        SqlStr = "Select * from INV_WARRANTY_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSROMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_WARRANTY_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSRODetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        mSupplierCode = CStr(-1)
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
        Dim SqlStr As String = ""
        SqlStr = ""

        MainClass.ClearGrid(SprdView)

        SqlStr = "Select IH.AUTO_KEY_REF as REF_NO," & vbCrLf & " IH.AUTO_KEY_MRR as MRR_NO, " & vbCrLf & " TO_CHAR(GH.MRR_DATE,'DD-MM-YYYY') as MRR_Date, " & vbCrLf & " AC.SUPP_CUST_NAME AS SupplierName, " & vbCrLf & " GH.BILL_NO, " & vbCrLf & " TO_CHAR(GH.BILL_DATE,'DD-MM-YYYY') AS BillDate " & vbCrLf & " FROM INV_WARRANTY_HDR IH, INV_GATE_HDR GH, FIN_SUPP_CUST_MST AC " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=GH.COMPANY_CODE " & vbCrLf & " AND IH.AUTO_KEY_MRR=GH.AUTO_KEY_MRR " & vbCrLf & " AND GH.COMPANY_CODE=AC.COMPANY_CODE " & vbCrLf & " AND GH.SUPP_CUST_CODE=AC.SUPP_CUST_CODE " & vbCrLf & " Order by IH.AUTO_KEY_REF"

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

            .set_ColWidth(1, 600)
            .set_ColWidth(2, 1200)
            .set_ColWidth(3, 1200)
            .set_ColWidth(4, 4500)
            .set_ColWidth(5, 1200)
            .set_ColWidth(6, 1200)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RSRODetail.Fields("REF_PO_NO").Precision ''
            '        .ColHidden = True
            .set_ColWidth(ColPONo, 8)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RSRODetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemDesc, 15)
            .ColsFrozen = ColItemDesc


            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RSRODetail.Fields("ITEM_UOM").DefinedSize ''
            .set_ColWidth(ColUnit, 4)

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRate, 9)

            For cntCol = ColBillQty To ColAcceptQty
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMin = 0 '"-99999999999.99"
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 7.5)
            Next

            .Col = ColSerialNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RSRODetail.Fields("ITEM_SERIAL_NO").DefinedSize
            .set_ColWidth(ColSerialNo, 12)

            .Col = ColMake
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RSRODetail.Fields("ITEM_MAKE").DefinedSize
            .set_ColWidth(ColMake, 12)

            .Col = ColInstDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeEditLen = RSRODetail.Fields("INST_DATE").DefinedSize
            .set_ColWidth(ColInstDate, 10)

            .Col = ColWarranty
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            '        .TypeEditLen = RSRODetail.Fields("WARRANTY_DUE_DATE").DefinedSize
            .set_ColWidth(ColWarranty, 10)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RSRODetail.Fields("REMARKS").DefinedSize
            .set_ColWidth(ColRemarks, 10)



        End With

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPONo, ColAcceptQty)
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RSRODetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RSROMain
            txtRefNo.Maxlength = .Fields("AUTO_KEY_REF").Precision
            txtRefDate.Maxlength = 10
            txtMRRNo.Maxlength = .Fields("AUTO_KEY_MRR").Precision
            txtMRRDate.Maxlength = 10
            txtRemarks.Maxlength = .Fields("REMARKS").DefinedSize
            txtEMailID.Maxlength = .Fields("EMAIL_ID").DefinedSize
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        With RSROMain
            If Not .EOF Then
                lblMKey.Text = .Fields("AUTO_KEY_REF").Value

                txtRefNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_REF").Value), "", .Fields("AUTO_KEY_REF").Value)
                txtRefDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")

                txtMRRNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)

                Call ShowFromMRR(Val(txtMRRNo.Text), False)
                txtEMailID.Text = IIf(IsDbNull(.Fields("EMAIL_ID").Value), "", .Fields("EMAIL_ID").Value)
                txtRemarks.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)


                chkCancelled.CheckState = IIf(.Fields("CANCELLED_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                Call ShowDetail1((lblMKey.Text), "")
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RSROMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        txtRefNo.Enabled = True
        txtMRRNo.Enabled = False
        cmdMRRSearch.Enabled = False

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub


    Private Sub ShowFromMRR(ByRef pMRRNo As Double, ByRef mIsShowDetail As Boolean)

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String = ""
        Dim SqlStr As String = ""

        SqlStr = " SELECT * FROM INV_GATE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND AUTO_KEY_MRR=" & Val(CStr(pMRRNo)) & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        With RsTemp
            If Not .EOF Then

                txtMRRNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                txtMRRDate.Text = VB6.Format(IIf(IsDbNull(.Fields("MRR_DATE").Value), "", .Fields("MRR_DATE").Value), "DD/MM/YYYY")

                If MainClass.ValidateWithMasterTable(.Fields("SUPP_CUST_CODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtSupplier.Text = MasterNo
                End If
                mSupplierCode = .Fields("SUPP_CUST_CODE").Value

                txtBillNo.Text = IIf(IsDbNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDbNull(.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value), "DD/MM/YYYY")
                txtRemarks.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                End If

                cboDivision.Text = mDivisionDesc

                If mIsShowDetail = True Then
                    Call ShowDetailFromMRR(pMRRNo)
                End If
            End If
        End With
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub ShowDetail1(ByRef mMKEY As String, ByRef pRefType As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mShortageQty As Double
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim SqlStr As String = ""
        Dim mPOQty As Double
        Dim mRecdQty As Double
        Dim mBalQty As Double
        Dim mRefPoNo As Double
        Dim mReworkQty As Double
        Dim mRejQty As Double
        Dim mDespQty As Double

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_WARRANTY_DET " & vbCrLf & " Where AUTO_KEY_REF=" & Val(mMKEY) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSRODetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RSRODetail
            If .EOF = True Then Exit Sub
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColPONo
                mRefPoNo = Val(IIf(IsDbNull(.Fields("REF_PO_NO").Value), -1, .Fields("REF_PO_NO").Value))

                SprdMain.Text = CStr(mRefPoNo)

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)


                SqlStr = "SELECT GetITEMPRICE_NEW(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mRefPoNo & ",'" & mItemCode & "') AS PORATE FROM DUAL "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("PORATE").Value), 0, RsTemp.Fields("PORATE").Value)))

                SprdMain.Col = ColBillQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("BILL_QTY").Value), 0, .Fields("BILL_QTY").Value)))

                SprdMain.Col = ColAcceptQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ACCEPTED_QTY").Value), 0, .Fields("ACCEPTED_QTY").Value)))

                SprdMain.Col = ColSerialNo
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_SERIAL_NO").Value), "", .Fields("ITEM_SERIAL_NO").Value))

                SprdMain.Col = ColMake
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_MAKE").Value), "", .Fields("ITEM_MAKE").Value))

                SprdMain.Col = ColInstDate
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("INST_DATE").Value), "", .Fields("INST_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColWarranty
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("WARRANTY_DUE_DATE").Value), "", .Fields("WARRANTY_DUE_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColRemarks
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value))


                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Sub ShowDetailFromMRR(ByRef mAutoKey As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mShortageQty As Double
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim SqlStr As String = ""
        Dim mPOQty As Double
        Dim mRecdQty As Double
        Dim mBalQty As Double
        Dim mRefPoNo As Double
        Dim mReworkQty As Double
        Dim RSTempDetail As ADODB.Recordset = Nothing
        Dim mRejQty As Double
        Dim mDespQty As Double

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_GATE_DET " & vbCrLf & " WHERE AUTO_KEY_MRR=" & Val(CStr(mAutoKey)) & "" & vbCrLf & " ORDER BY " & vbCrLf & " SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSTempDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RSTempDetail
            If .EOF = True Then Exit Sub
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColPONo
                mRefPoNo = Val(IIf(IsDbNull(.Fields("REF_AUTO_KEY_NO").Value), -1, .Fields("REF_AUTO_KEY_NO").Value))
                SprdMain.Text = CStr(mRefPoNo)

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Row = I
                SprdMain.Col = ColBillQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("BILL_QTY").Value), 0, .Fields("BILL_QTY").Value)))

                SqlStr = "SELECT GetITEMPRICE_NEW(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mRefPoNo & ",'" & mItemCode & "') AS PORATE FROM DUAL "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("PORATE").Value), 0, RsTemp.Fields("PORATE").Value)))

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                SprdMain.Col = ColAcceptQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("LOT_ACCEPT").Value), 0, .Fields("LOT_ACCEPT").Value)))

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub

    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            AssignGrid(True)
            '        AdataItem.Refresh
            FormatSprdView()
            SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RSROMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        lblMKey.Text = ""

        mSupplierCode = CStr(-1)
        txtRefNo.Text = ""
        txtRefDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtMRRNo.Text = ""
        txtMRRDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1

        chkCancelled.Enabled = False
        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked



        TxtSupplier.Text = ""
        txtBillNo.Text = ""
        txtBillDate.Text = "" 'VB6.Format(RunDate, "DD/MM/YYYY")
        txtRemarks.Text = ""
        txtEMailID.Text = ""
        txtRefDate.Enabled = False
        txtMRRDate.Enabled = False
        txtBillDate.Enabled = False
        TxtSupplier.Enabled = False
        txtBillNo.Enabled = False

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RSROMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FrmItemWarrantyEntry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmItemWarrantyEntry_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub FrmItemWarrantyEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        '    If InStr(1, XRIGHT, "D", vbTextCompare) > 1 Then
        '        chkCancelled.Enabled = True
        '    Else
        chkCancelled.Enabled = False
        '    End If

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245) '8000
        ''Me.Width = VB6.TwipsToPixelsX(11355) '11900


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

        AdataItem.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        Dim mPONo As Double

        mCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))

        SprdMain.Refresh()



        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F3 And mCol = ColPONo And SprdMain.ActiveRow > 1 Then
            SprdMain.Row = SprdMain.ActiveRow - 1
            SprdMain.Col = ColPONo
            mPONo = Val(SprdMain.Text)

            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = ColPONo
            SprdMain.Text = CStr(mPONo)

        End If
        ''SprdMain_Click ColItemName, 0

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

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRefDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtRefDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtRefDate.Text)) = False Then
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
        Dim xMkey As String = ""
        Dim mRefNo As String
        Dim SqlStr As String = ""

        If Trim(txtRefNo.Text) = "" Then GoTo EventExitSub

        If Len(txtRefNo.Text) < 6 Then
            txtRefNo.Text = Val(txtRefNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RSROMain.EOF = False Then xMkey = RSROMain.Fields("AUTO_KEY_REF").Value
        mRefNo = Trim(txtRefNo.Text)

        SqlStr = " SELECT * FROM INV_WARRANTY_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND AUTO_KEY_REF=" & Val(mRefNo) & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSROMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RSROMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Ref, Use Generate MRR Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM INV_WARRANTY_HDR " & " WHERE AUTO_KEY_REF=" & Val(xMkey) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSROMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtSupplier.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
