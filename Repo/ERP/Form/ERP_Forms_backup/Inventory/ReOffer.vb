Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmReOffer
    Inherits System.Windows.Forms.Form
    Dim RSROMain As ADODB.Recordset
    Dim RSRODetail As ADODB.Recordset
    Dim RSROExp As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection
    Dim pQCDate As String
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer

    Dim mSupplierCode As String
    Dim pRound As Double

    Private Const mBookType As String = "R"
    Private Const mBookSubType As String = "O"

    Private Const ConRowHeight As Short = 12

    Private Const ColPONo As Short = 1
    Private Const ColItemCode As Short = 2
    Private Const ColItemDesc As Short = 3
    Private Const ColBatchNo As Short = 4
    Private Const ColUnit As Short = 5
    Private Const ColBillQty As Short = 6
    Private Const ColReceivedQty As Short = 7
    Private Const ColAcceptQty As Short = 8
    Private Const ColRejQty As Short = 9
    Private Const ColReOfferedQty As Short = 10
    Private Const ColDespQty As Short = 11
    Private Const ColRework As Short = 12
    Private Const ColStockType As Short = 13
    Private Const ColRate As Short = 14
    Private Const ColAmount As Short = 15
    Private Const ColItemCost As Short = 16
    Private Const ColQCEMP As Short = 17
    Private Const ColQCDate As Short = 18


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

    Dim pDnCnNo As String
    Dim mDNCnNO As Integer
    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkCNReleased_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCNReleased.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub chkReofferPost_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkReofferPost.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            SprdExp.Enabled = True
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
            SprdExp.Enabled = False
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
        Me.Hide()
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

        If chkCNReleased.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Credit Note Released. Re-offer Cann't be Deleted.")
            Exit Sub
        End If

        If chkReofferPost.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("Reoffer is Posted, So Cann't be Deleted", MsgBoxStyle.Information)
            Exit Sub
        End If

        '    If CheckBillPayment(mSupplierCode, txtBillNo.Text, "B") = True Then Exit Sub

        If Not RSROMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_REOFFER_HDR", (txtRefNo.Text), RSROMain, "AUTO_KEY_REF") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_REOFFER_HDR", "AUTO_KEY_REF", (lblMKey.Text)) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_REOFFER, (txtRefNo.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("Delete from INV_REOFFER_EXP Where AUTO_KEY_REF='" & lblMKey.Text & "'")
                PubDBCn.Execute("Delete from INV_REOFFER_DET Where AUTO_KEY_REF=" & Val(lblMKey.Text) & "")
                PubDBCn.Execute("Delete from INV_REOFFER_HDR Where AUTO_KEY_REF=" & Val(lblMKey.Text) & "")

                PubDBCn.CommitTrans()

                '            If UpdateMRRRWK(True) = False Then GoTo DelErrPart

                RSROMain.Requery() ''.Refresh
                RSRODetail.Requery() ''.Refresh
                RSROExp.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RSROMain.Requery() ''.Refresh
        RSRODetail.Requery() ''.Refresh
        RSROExp.Requery() ''.Refresh
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
            If chkCNReleased.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Credit Note Released so Bill Cann't be Modified")
                Exit Sub
            End If
        End If

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RSROMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            SprdExp.Enabled = True
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

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, PREBY.EMP_NAME"

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_REOFFER_HDR IH, INV_REOFFER_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST,PAY_EMPLOYEE_MST PREBY"
        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND ID.COMPANY_CODE=PREBY.COMPANY_CODE" & vbCrLf & " AND ID.QC_EMP_CODE=PREBY.EMP_CODE" & vbCrLf & " AND IH.AUTO_KEY_REF=" & Val(txtRefNo.Text) & ""


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
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IS_POSTED='N'"
        If MainClass.SearchGridMaster((txtRefNo.Text), "INV_REOFFER_HDR", "AUTO_KEY_REF", "REF_DATE", "AUTO_KEY_MRR", "MRR_DATE", SqlStr) = True Then
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

        Call CalcTots()
        pDnCnNo = ""

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
    Private Sub SprdExp_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdExp.ButtonClicked
        If FormActive = True Then
            SprdExp.Col = ColExpAmt
            SprdExp.Row = eventArgs.Row
            If Val(SprdExp.Text) <> 0 Then
                Call CalcTots()
            End If
        End If
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim mItemCode As String
        Dim DelStatus As Boolean

        If eventArgs.row = 0 And eventArgs.col = ColStockType Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColStockType
                If MainClass.SearchGridMaster(.Text, "INV_TYPE_MST", "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColStockType
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColQCEMP Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColQCEMP

                If MainClass.SearchGridMaster(.Text, "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColQCEMP
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColItemCode
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                SprdMain.Col = ColItemCode
                mItemCode = SprdMain.Text

                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode, DelStatus)
                FormatSprdMain(-1)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
            End If
        End If

        CalcTots()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        'Dim xPoNo As String
        'Dim xICode As String
        Dim mQty As Double
        Dim mReOfferedQty As Double
        Dim mAcceptQty As Double
        Dim mDespQty As Double

        If eventArgs.NewRow = -1 Then Exit Sub

        SprdMain.Row = eventArgs.Row


        Select Case eventArgs.Col

            Case ColRework

                SprdMain.Col = ColReOfferedQty
                mReOfferedQty = Val(SprdMain.Text)

                SprdMain.Col = ColDespQty
                mDespQty = Val(SprdMain.Text)

                SprdMain.Col = ColRejQty
                mQty = Val(SprdMain.Text)
                mQty = mQty - mReOfferedQty - mDespQty

                SprdMain.Col = ColRework
                mAcceptQty = Val(SprdMain.Text)



                If mAcceptQty > mQty Then
                    MsgInformation("Rework Qty Cann't be Greater Than Rejected Qty")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColRework)
                End If
                '
                '        Case ColRate
                '            ''Not required in case of foc item.... '06/10/2001
                '            ''CheckRate Col, Row
            Case ColStockType
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColStockType
                If Trim(SprdMain.Text) = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(SprdMain.Text, "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("InValid Stock Type")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColStockType)
                Else
                    If Trim(SprdMain.Text) = "RJ" Then
                        MsgInformation("You Cann't Select 'RJ' Stock Type.")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColStockType)
                    End If
                End If
            Case ColQCEMP
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColQCEMP
                If Trim(SprdMain.Text) = "" Then Exit Sub
                SprdMain.Text = VB6.Format(SprdMain.Text, "000000")

                If MainClass.ValidateWithMasterTable(SprdMain.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("InValid QC Employee")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColQCEMP)
                End If
        End Select
        Call CalcTots()
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

            .Col = 2
            txtRefNo.Text = CStr(Val(.Text))

            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
            CmdView_Click(CmdView, New System.EventArgs())
        End With
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
        Dim mMRRNO As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(txtMRRNo.Text) = "" Then GoTo EventExitSub

        If Len(txtMRRNo.Text) < 6 Then
            txtMRRNo.Text = Val(txtMRRNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mMRRNO = Trim(txtMRRNo.Text)
        SqlStr = " SELECT * FROM INV_GATE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND AUTO_KEY_MRR=" & Val(mMRRNO) & " "

        '            & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        '
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Clear1()
            Call ShowFromMRR(RsTemp)
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
        Dim mCNReleased As String
        Dim mReofferPost As String
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
        mCNReleased = IIf(chkCNReleased.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mReofferPost = IIf(chkReofferPost.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

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
            SqlStr = "INSERT INTO INV_REOFFER_HDR( " & vbCrLf & " COMPANY_CODE, FYEAR, " & vbCrLf & " AUTO_KEY_REF, REF_DATE," & vbCrLf & " AUTO_KEY_MRR, MRR_DATE," & vbCrLf & " SUPP_CUST_CODE, BILL_NO, BILL_DATE," & vbCrLf & " REF_TYPE, REMARKS, " & vbCrLf & " ASSESS_AMT, EXCISE_PER, EXCISE_AMT," & vbCrLf & " DISCOUNT_PER, DISCOUNT_AMT, TAXABLE_AMT," & vbCrLf & " SALETAX_PER, SALETAX_AMT, FREIGHT_AMT," & vbCrLf & " INVOICE_AMT, " & vbCrLf & " MRR_FINAL_FLAG, CANCELLED_STATUS, IS_POSTED, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE,DIV_CODE) "


            SqlStr = SqlStr & vbCrLf & " VALUES( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & Val(CStr(mVNoSeq)) & ", TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & Val(txtMRRNo.Text) & ", TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(mSuppCustCode) & "', '" & MainClass.AllowSingleQuote((txtBillNo.Text)) & "', TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & txtRefType.Text & "', '" & MainClass.AllowSingleQuote((TxtRemarks.Text)) & "'," & vbCrLf & " " & Val(lblTotItemValue.Text) & ", " & Val(lblEDPercentage.Text) & ", " & Val(lblTotED.Text) & "," & vbCrLf & " 0," & Val(lblDiscount.Text) & "," & Val(lblTotTaxableAmt.Text) & ", " & vbCrLf & " " & Val(lblSTPercentage.Text) & "," & Val(lblTotST.Text) & "," & Val(lblTotFreight.Text) & "," & vbCrLf & "  " & Val(lblNetAmount.Text) & ", " & vbCrLf & " '" & mCNReleased & "','" & mCancelled & "', '" & mReofferPost & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'',''," & Val(CStr(mDivisionCode)) & ")"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE INV_REOFFER_HDR SET " & vbCrLf & " AUTO_KEY_REF =" & Val(CStr(mVNoSeq)) & " ," & vbCrLf & " MRR_DATE=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "'," & vbCrLf & " BILL_NO='" & MainClass.AllowSingleQuote((txtBillNo.Text)) & "'," & vbCrLf & " BILL_DATE=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " REF_TYPE='" & txtRefType.Text & "'," & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote((TxtRemarks.Text)) & "',"

            SqlStr = SqlStr & vbCrLf & " ASSESS_AMT= " & Val(lblTotItemValue.Text) & ", " & vbCrLf & " EXCISE_PER= " & Val(lblEDPercentage.Text) & ", " & vbCrLf & " EXCISE_AMT= " & Val(lblTotED.Text) & "," & vbCrLf & " DISCOUNT_PER= 0, " & vbCrLf & " DISCOUNT_AMT= " & Val(lblDiscount.Text) & "," & vbCrLf & " TAXABLE_AMT= " & Val(lblTotTaxableAmt.Text) & ", " & vbCrLf & " SALETAX_PER= " & Val(lblSTPercentage.Text) & "," & vbCrLf & " SALETAX_AMT= " & Val(lblTotST.Text) & "," & vbCrLf & " FREIGHT_AMT= " & Val(lblTotFreight.Text) & "," & vbCrLf & " INVOICE_AMT= " & Val(lblNetAmount.Text) & ", " & vbCrLf & " DIV_CODE= " & Val(CStr(mDivisionCode)) & ", " & vbCrLf & " MRR_FINAL_FLAG='" & mCNReleased & "', CANCELLED_STATUS='" & mCancelled & "'," & vbCrLf & " IS_POSTED='" & mReofferPost & "'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE AUTO_KEY_REF ='" & MainClass.AllowSingleQuote((lblMKey.Text)) & "'"
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(mSuppCustCode, mDivisionCode) = False Then GoTo ErrPart
        UpdateMain1 = True
        PubDBCn.CommitTrans()

        '    If UpdateMRRRWK() = False Then GoTo ErrPart


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
        ''Resume
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

        SqlStr = "SELECT MAX(REF_DATE)" & vbCrLf & " FROM INV_REOFFER_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_REF<" & Val(CStr(pREFNoSeq)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDbNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If

        SqlStr = "SELECT MIN(REF_DATE)" & " FROM INV_REOFFER_HDR " & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_REF>" & Val(CStr(pREFNoSeq)) & ""

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
        SqlStr = "SELECT Max(AUTO_KEY_REF)  " & vbCrLf & " FROM INV_REOFFER_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

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
        Dim mStockType As String = ""
        Dim mBillQty As Double
        Dim mRecdQty As Double
        Dim mRejQty As Double
        Dim mAcceptQty As Double
        Dim mRework As Double
        Dim mQCEmp As String
        Dim mItemRate As Double
        Dim mItemCost As Double
        Dim mBatchNo As String
        Dim mAmount As Double
        Dim mRecord As Boolean
        Dim mMRRQCDate As String

        mRecord = False
        PubDBCn.Execute("Delete From INV_REOFFER_DET Where AUTO_KEY_REF='" & lblMKey.Text & "'")
        If DeleteStockTRN(PubDBCn, ConStockRefType_REOFFER, (txtRefNo.Text)) = False Then GoTo UpdateDetail1Err

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColPONo
                mPONo = Val(.Text)

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColStockType
                mStockType = MainClass.AllowSingleQuote(.Text)

                .Col = ColBatchNo
                mBatchNo = Trim(.Text)

                .Col = ColBillQty
                mBillQty = Val(.Text)

                .Col = ColReceivedQty
                mRecdQty = Val(.Text)

                .Col = ColAcceptQty
                mAcceptQty = Val(.Text)

                .Col = ColRejQty
                mRejQty = Val(.Text)

                .Col = ColRework
                mRework = Val(.Text)

                '            .Col = ColReOfferedQty
                '            mRework = mRework + Val(.Text)

                .Col = ColQCEMP
                mQCEmp = MainClass.AllowSingleQuote(.Text)

                .Col = ColRate
                mItemRate = Val(.Text)

                .Col = ColAmount
                mAmount = Val(.Text)

                .Col = ColItemCost
                mItemCost = Val(.Text)

                .Col = ColQCDate
                mMRRQCDate = IIf(IsDate(.Text), .Text, txtRefDate.Text)

                SqlStr = ""

                If mItemCode <> "" And mRework > 0 Then
                    SqlStr = " INSERT INTO INV_REOFFER_DET ( " & vbCrLf & " AUTO_KEY_REF, SERIAL_NO, " & vbCrLf & " REF_PO_NO, ITEM_CODE," & vbCrLf & " BATCH_NO," & vbCrLf & " ITEM_UOM, BILL_QTY," & vbCrLf & " RECEIVED_QTY, ACCEPTED_QTY," & vbCrLf & " REJECTED_QTY, LOT_ACC_RWK, " & vbCrLf & " STOCK_TYPE, ITEM_RATE," & vbCrLf & " AMOUNT, ITEM_COST," & vbCrLf & " QC_EMP_CODE, MRR_QCDATE, COMPANY_CODE) "


                    SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " '" & LblMkey.Text & "'," & I & ", " & vbCrLf & " " & Val(CStr(mPONo)) & ", '" & mItemCode & "', " & vbCrLf & " '" & mBatchNo & "', " & vbCrLf & " '" & mUnit & "', " & mBillQty & ", " & vbCrLf & " " & mRecdQty & ", " & mAcceptQty & ", " & vbCrLf & " " & mRejQty & ", " & mRework & ", " & vbCrLf & " '" & mStockType & "', " & mItemRate & ", " & vbCrLf & " " & mAmount & ", " & mItemCost & ", " & vbCrLf & " '" & mQCEmp & "', TO_DATE('" & VB6.Format(mMRRQCDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ") "


                    PubDBCn.Execute(SqlStr)

                    If chkReofferPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                        If UpdateStockTRN(PubDBCn, ConStockRefType_REOFFER, (txtRefNo.Text), I, (txtRefDate.Text), (txtRefDate.Text), mStockType, mItemCode, mUnit, mBatchNo, mRework, 0, "I", mItemRate, mItemCost, "", "", "STR", "", "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), "From : " & TxtSupplier.Text, pSupplierCode, ConWH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err

                        If UpdateStockTRN(PubDBCn, ConStockRefType_REOFFER, (txtRefNo.Text), I, (txtRefDate.Text), (txtRefDate.Text), "RJ", mItemCode, mUnit, mBatchNo, mRework, 0, "O", mItemRate, mItemCost, "", "", "STR", "", "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), "From : " & TxtSupplier.Text, pSupplierCode, ConWH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
                    End If
                    mRecord = True
                End If
            Next
        End With
        If mRecord = False Then
            MsgInformation("Nothing to Save.")
            UpdateDetail1 = False
            Exit Function
        End If

        UpdateDetail1 = True
        UpdateDetail1 = UpdateExp1
        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function


    Private Function UpdateMRRRWK(ByRef mIsDelete As Boolean) As Boolean

        On Error GoTo UpdateDetail1Err

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mReworkQty As Double

        Dim I As Integer
        Dim mPONo As Double
        Dim mItemCode As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColPONo
                mPONo = Val(.Text)

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)


                SqlStr = "SELECT SUM(LOT_ACC_RWK) LOT_ACC_RWK" & vbCrLf & " FROM INV_REOFFER_HDR IH, INV_REOFFER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF " & vbCrLf & " AND IH.AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & " " & vbCrLf & " AND ID.REF_PO_NO=" & Val(CStr(mPONo)) & " " & vbCrLf & " AND ID.ITEM_CODE='" & Trim(mItemCode) & "' "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    If mIsDelete = True Then
                        mReworkQty = IIf(IsDbNull(RsTemp.Fields("LOT_ACC_RWK").Value), 0, RsTemp.Fields("LOT_ACC_RWK").Value)
                    Else
                        mReworkQty = 0
                    End If

                    SqlStr = " UPDATE INV_GATE_DET SET " & vbCrLf & " LOT_ACC_RWK=" & Val(CStr(mReworkQty)) & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR = " & Val(txtMRRNo.Text) & ""

                    If Val(CStr(mPONo)) <> -1 And Val(CStr(mPONo)) <> 0 Then
                        SqlStr = SqlStr & vbCrLf & " AND REF_AUTO_KEY_NO=" & Val(CStr(mPONo)) & " "
                    End If

                    SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & Trim(mItemCode) & "' "

                    PubDBCn.Execute(SqlStr)

                End If
            Next
        End With

        PubDBCn.CommitTrans()
        UpdateMRRRWK = True

        Exit Function
UpdateDetail1Err:
        UpdateMRRRWK = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
        '    Resume
    End Function
    Private Function UpdateExp1() As Boolean
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String

        PubDBCn.Execute("Delete From INV_REOFFER_EXP Where AUTO_KEY_REF='" & lblMKey.Text & "'")
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

                .Col = ColRO
                mRO = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                SqlStr = ""
                If mCalcOn <> 0 Or mExpAmount <> 0 Then
                    SqlStr = "Insert Into  INV_REOFFER_EXP (AUTO_KEY_REF,SERIAL_NO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO) " & vbCrLf & " Values ('" & lblMKey.Text & "'," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mCalcOn & ",'" & mRO & "')"
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

        If lblPost.Text = "N" And chkReofferPost.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("Reoffer is Posted, So Cann't be Modified", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If lblPost.Text = "Y" And chkReofferPost.Enabled = False Then
            MsgBox("Reoffer is Posted, So Cann't be Modified", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If PubUserID <> "G0416" Then
            If CDate(txtRefDate.Text) >= CDate(PubGSTApplicableDate) Then
                MsgBox("Can't be Save Reoffer.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        If PubSuperUser = "S" Then

        Else
            '            If lblPost.text = "Y" And chkReofferPost.Enabled = True Then
            If DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(txtMRRDate.Text), PubCurrDate) > 2 Then
                    MsgBox("You have no rights to Post Reoffer after 48 Hrs.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
                '            End If
            End If

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

        For mRow = 1 To SprdMain.MaxRows
            SprdMain.Row = mRow
            SprdMain.Col = ColItemCode
            mItemCode = Trim(SprdMain.Text)

            SprdMain.Col = ColUnit
            mItemUOM = Trim(SprdMain.Text)

            SprdMain.Col = ColReOfferedQty
            mReOfferedQty = Val(SprdMain.Text)

            SprdMain.Col = ColDespQty
            mDespQty = GetDespatchQty(mItemCode, mItemUOM)

            SprdMain.Col = ColRejQty
            mQty = Val(SprdMain.Text)
            mQty = mQty - mReOfferedQty - mDespQty

            SprdMain.Col = ColRework
            mAcceptQty = Val(SprdMain.Text)

            If mAcceptQty > 0 Then
                mRejStock = GetBalanceStockQty(mItemCode, (txtRefDate.Text), mItemUOM, "STR", "RJ", "", ConWH, mDivisionCode, ConStockRefType_REOFFER, Val(txtRefNo.Text))
                If mAcceptQty > mQty Then
                    MsgInformation("Rework Qty Cann't be Greater Than Balance Rejected Qty")
                    FieldsVarification = False
                    MainClass.SetFocusToCell(SprdMain, mRow, ColRework)
                    Exit Function
                End If

                If mRejStock < mAcceptQty Then
                    MsgInformation("You have not enough Rejected Stock, Cann't be Saved.")
                    FieldsVarification = False
                    MainClass.SetFocusToCell(SprdMain, mRow, ColRework)
                    Exit Function
                End If
            End If
        Next

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColBillQty, "N", "Please Check Bill Quantity.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColQCEMP, "S", "Please QC EMP.") = False Then FieldsVarification = False : Exit Function

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
        '    Resume
    End Function

    Private Function GetDespatchQty(ByRef pItemCode As String, ByRef pUOM As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing



        '    SqlStr = "SELECT SUM(DECODE(INVMST.ISSUE_UOM,'" & pUOM & "',1,INVMST.UOM_FACTOR) * ID.ITEM_QTY * DECODE(IH.BOOKSUBTYPE,'D',1,-1)) AS QTY " & vbCrLf _
        ''               & " FROM " & vbCrLf _
        ''               & " FIN_DNCN_HDR IH, FIN_DNCN_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
        ''               & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
        ''               & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
        ''               & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
        ''               & " AND IH.Company_Code=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
        ''               & " AND ID.MRR_REF_NO=" & Val(txtMRRNo.Text) & "" & vbCrLf _
        ''               & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf _
        ''               & " AND IH.DNCNTYPE='R' AND CANCELLED='N' AND IH.DNCNFROM IN ('M','R') AND APPROVED='Y' AND ISDESPATCHED='Y'"

        SqlStr = "SELECT SUM( ID.PACKED_QTY/DECODE(INVMST.ISSUE_UOM,'" & pUOM & "',1,INVMST.UOM_FACTOR)) AS QTY " & vbCrLf & " FROM " & vbCrLf & " DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ID.MRR_REF_NO=" & Val(txtMRRNo.Text) & "" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND IH.DESP_TYPE IN ('Q')" '','L'


        '    If Trim(LblMkey.text) <> "" Then
        '        SqlStr = SqlStr & vbCrLf & " AND IH.MKEY<>'" & LblMkey.text & "'"
        '    End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetDespatchQty = IIf(IsDbNull(RsTemp.Fields("QTY").Value), 0, RsTemp.Fields("QTY").Value)
        End If


        Exit Function
ErrPart:
        GetDespatchQty = 0
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmReOffer_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Re-Offer MRR Entry"

        SqlStr = ""
        SqlStr = "Select * from INV_REOFFER_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSROMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_REOFFER_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSRODetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_REOFFER_EXP Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSROExp, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = "Select REF_TYPE,GR.AUTO_KEY_REF as MRR_No," & vbCrLf & " TO_CHAR(GR.MRR_DATE,'DD-MM-YYYY') as MRR_Date, " & vbCrLf & " AC.SUPP_CUST_NAME AS SupplierName, " & vbCrLf & " GR.BILL_NO, " & vbCrLf & " TO_CHAR(GR.BILL_DATE,'DD-MM-YYYY') AS BillDate " & vbCrLf & " FROM INV_REOFFER_HDR GR,FIN_SUPP_CUST_MST AC " & vbCrLf & " WHERE " & vbCrLf & " GR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND GR.COMPANY_CODE=AC.COMPANY_CODE " & vbCrLf & " AND GR.SUPP_CUST_CODE=AC.SUPP_CUST_CODE " & vbCrLf & " Order by AUTO_KEY_REF"

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
    Private Sub FormatSprdExp(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdExp
            .Row = Arow
            .set_RowHeight(Arow, 10)

            .Col = ColExpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(.Col, 20)

            .Col = ColExpPercent
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = 0.0#
            .TypeFloatMax = 99.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeEditMultiLine = False
            .set_ColWidth(.Col, 6)

            .Col = ColExpAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(ColExpAmt, 8)
            .TypeEditMultiLine = False
            .set_ColWidth(.Col, 10)

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


            .Col = ColRO
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColRO, 2)
            '.Value = vbUnchecked

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
            .set_ColWidth(ColItemCode, 5)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemDesc, 10)
            .ColsFrozen = ColItemDesc

            .Col = ColBatchNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RSRODetail.Fields("BATCH_NO").DefinedSize ''
            '        .CellType = SS_CELL_TYPE_INTEGER
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            '        If lblBookType.text = "Q" Then
            .ColHidden = True
            '        Else
            '            .ColHidden = False
            '        End If

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RSRODetail.Fields("ITEM_UOM").DefinedSize ''
            .set_ColWidth(ColUnit, 4)

            For cntCol = ColBillQty To ColRework
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMin = 0 '"-99999999999.99"
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 7.5)
            Next

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RSRODetail.Fields("STOCK_TYPE").DefinedSize ''
            .set_ColWidth(ColStockType, 5)

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRate, 9)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColAmount, 9)

            .Col = ColItemCost
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .ColHidden = True

            .Col = ColQCEMP
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RSRODetail.Fields("QC_EMP_CODE").DefinedSize ''
            .set_ColWidth(ColQCEMP, 6)

            .Col = ColQCDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 10
            .set_ColWidth(ColQCDate, 9)
            .ColHidden = True

        End With

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPONo, ColDespQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRate, ColItemCost)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColQCDate, ColQCDate)
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
            TxtSupplier.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtBillNo.Maxlength = .Fields("BILL_NO").DefinedSize
            txtBillDate.Maxlength = 10
            txtRemarks.Maxlength = .Fields("REMARKS").DefinedSize
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
                txtMRRDate.Text = VB6.Format(IIf(IsDbNull(.Fields("MRR_DATE").Value), "", .Fields("MRR_DATE").Value), "DD/MM/YYYY")

                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtSupplier.Text = MasterNo
                End If
                mSupplierCode = .Fields("SUPP_CUST_CODE").Value

                txtBillNo.Text = IIf(IsDbNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDbNull(.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value), "DD/MM/YYYY")
                txtRemarks.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                txtRefType.Text = IIf(IsDbNull(.Fields("REF_TYPE").Value), "", .Fields("REF_TYPE").Value)

                chkCancelled.CheckState = IIf(.Fields("CANCELLED_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkCNReleased.CheckState = IIf(.Fields("MRR_FINAL_FLAG").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkReofferPost.CheckState = IIf(.Fields("IS_POSTED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkReofferPost.Enabled = IIf(.Fields("IS_POSTED").Value = "Y", False, IIf(lblPost.Text = "N", False, True))


                If MainClass.ValidateWithMasterTable(Val(txtMRRNo.Text), "AUTO_KEY_MRR", "DIV_CODE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionCode = Val(MasterNo)
                    If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDivisionDesc = Trim(MasterNo)
                        cboDivision.Text = mDivisionDesc
                    End If
                End If

                Call ShowDetail1((LblMkey.Text), txtRefType.Text)
                Call ShowExp1((lblMKey.Text))
                Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))
                Call CalcTots()
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RSROMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        SprdExp.Enabled = False
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


    Private Sub ShowFromMRR(ByRef mRsTemp As ADODB.Recordset)
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mMRRNO As Double
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String = ""

        With mRsTemp
            If Not .EOF Then

                mMRRNO = IIf(IsDbNull(.Fields("AUTO_KEY_MRR").Value), -1, .Fields("AUTO_KEY_MRR").Value)

                txtMRRNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                txtMRRDate.Text = VB6.Format(IIf(IsDbNull(.Fields("MRR_DATE").Value), "", .Fields("MRR_DATE").Value), "DD/MM/YYYY")

                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtSupplier.Text = MasterNo
                End If
                mSupplierCode = .Fields("SUPP_CUST_CODE").Value

                txtBillNo.Text = IIf(IsDbNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDbNull(.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value), "DD/MM/YYYY")
                txtRemarks.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                txtRefType.Text = IIf(IsDbNull(.Fields("REF_TYPE").Value), "F", .Fields("REF_TYPE").Value)

                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                End If

                cboDivision.Text = mDivisionDesc

                Call ShowDetailFromMRR(mMRRNO)
                Call ShowExpFromMRR(mMRRNO)
                Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))
                Call CalcTots()
            End If
        End With
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub ShowExp1(ByRef mMKEY As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim pExpId As String
        Dim SqlStr As String = ""

        Call FillSprdExp()

        SqlStr = ""
        SqlStr = "Select INV_REOFFER_EXP.EXPCODE,INV_REOFFER_EXP.EXPPERCENT, " & vbCrLf & " INV_REOFFER_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO,DebitAmount " & vbCrLf & " From INV_REOFFER_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INV_REOFFER_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND INV_REOFFER_EXP.AUTO_KEY_REF='" & mMKEY & "'"

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSROExp, ADODB.LockTypeEnum.adLockReadOnly)
        If RSROExp.EOF = False Then
            RSROExp.MoveFirst()
            With SprdExp
                Do While Not RSROExp.EOF
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
                        If .Text = RSROExp.Fields("Name").Value Then Exit For
                    Next I

                    .Col = ColExpPercent 'Exp. %
                    .Text = CStr(Val(IIf(IsDbNull(RSROExp.Fields("ExpPercent").Value), "", RSROExp.Fields("ExpPercent").Value)))

                    .Col = ColExpAmt
                    If RSROExp.Fields("Identification").Value = "RO" Then '30.10.2001   ''Allow '-' if exp. is ropund off
                        .Text = CStr(Val(IIf(IsDbNull(RSROExp.Fields("Amount").Value), "", RSROExp.Fields("Amount").Value)))
                    Else
                        .Text = CStr(System.Math.Abs(Val(IIf(IsDbNull(RSROExp.Fields("Amount").Value), "", RSROExp.Fields("Amount").Value))))
                    End If

                    .Col = ColExpSTCode
                    .Text = CStr(Val(IIf(IsDbNull(RSROExp.Fields("CODE").Value), 0, RSROExp.Fields("CODE").Value)))

                    .Col = ColExpAddDeduct 'ExpFlag
                    .Text = IIf(RSROExp.Fields("Add_Ded").Value = "A", "A", "D")

                    .Col = ColExpIdent
                    .Text = IIf(IsDbNull(RSROExp.Fields("Identification").Value), "", RSROExp.Fields("Identification").Value)
                    If .Text = "RO" Then 'round off
                        .Col = ColExpAmt
                        pRound = Val(.Text)
                    End If

                    SprdExp.Col = ColTaxable
                    SprdExp.Text = IIf(IsDbNull(RSROExp.Fields("Taxable").Value), "N", RSROExp.Fields("Taxable").Value)

                    SprdExp.Col = ColExciseable
                    SprdExp.Text = IIf(IsDbNull(RSROExp.Fields("Exciseable").Value), "N", RSROExp.Fields("Exciseable").Value)

                    SprdExp.Col = ColExpCalcOn
                    SprdExp.Text = CStr(Val(IIf(IsDbNull(RSROExp.Fields("CalcOn").Value), "", RSROExp.Fields("CalcOn").Value)))

                    .Col = ColRO
                    .Value = IIf(RSROExp.Fields("RO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


                    RSROExp.MoveNext()
                Loop
            End With
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub
    Private Sub ShowExpFromMRR(ByRef mMKEY As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim pExpId As String
        Dim SqlStr As String = ""
        Dim RSTempExp As ADODB.Recordset = Nothing

        Call FillSprdExp()

        SqlStr = ""
        SqlStr = "Select INV_GATE_EXP.EXPCODE,INV_GATE_EXP.EXPPERCENT, " & vbCrLf & " INV_GATE_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO,DebitAmount " & vbCrLf & " From INV_GATE_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INV_GATE_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND INV_GATE_EXP.Mkey='" & mMKEY & "'"

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY SUBROWNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSTempExp, ADODB.LockTypeEnum.adLockReadOnly)
        If RSTempExp.EOF = False Then
            RSTempExp.MoveFirst()
            With SprdExp
                Do While Not RSROExp.EOF
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
                        If .Text = RSROExp.Fields("Name").Value Then Exit For
                    Next I

                    .Col = ColExpPercent 'Exp. %
                    .Text = CStr(Val(IIf(IsDbNull(RSROExp.Fields("ExpPercent").Value), "", RSROExp.Fields("ExpPercent").Value)))

                    .Col = ColExpAmt
                    If RSROExp.Fields("Identification").Value = "RO" Then '30.10.2001   ''Allow '-' if exp. is ropund off
                        .Text = CStr(Val(IIf(IsDbNull(RSROExp.Fields("Amount").Value), "", RSROExp.Fields("Amount").Value)))
                    Else
                        .Text = CStr(System.Math.Abs(Val(IIf(IsDbNull(RSROExp.Fields("Amount").Value), "", RSROExp.Fields("Amount").Value))))
                    End If

                    .Col = ColExpSTCode
                    .Text = CStr(Val(IIf(IsDbNull(RSROExp.Fields("CODE").Value), 0, RSROExp.Fields("CODE").Value)))

                    .Col = ColExpAddDeduct 'ExpFlag
                    .Text = IIf(RSROExp.Fields("Add_Ded").Value = "A", "A", "D")

                    .Col = ColExpIdent
                    .Text = IIf(IsDbNull(RSROExp.Fields("Identification").Value), "", RSROExp.Fields("Identification").Value)
                    If .Text = "RO" Then 'round off
                        .Col = ColExpAmt
                        pRound = Val(.Text)
                    End If

                    SprdExp.Col = ColTaxable
                    SprdExp.Text = IIf(IsDbNull(RSROExp.Fields("Taxable").Value), "N", RSROExp.Fields("Taxable").Value)

                    SprdExp.Col = ColExciseable
                    SprdExp.Text = IIf(IsDbNull(RSROExp.Fields("Exciseable").Value), "N", RSROExp.Fields("Exciseable").Value)

                    SprdExp.Col = ColExpCalcOn
                    SprdExp.Text = CStr(Val(IIf(IsDbNull(RSROExp.Fields("CalcOn").Value), "", RSROExp.Fields("CalcOn").Value)))

                    .Col = ColRO
                    .Value = IIf(RSROExp.Fields("RO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


                    RSROExp.MoveNext()
                Loop
            End With
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

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
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_REOFFER_DET " & vbCrLf & " Where AUTO_KEY_REF=" & Val(mMKEY) & "" & vbCrLf & " Order By SERIAL_NO"

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

                SprdMain.Col = ColBatchNo
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value))

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Row = I
                SprdMain.Col = ColBillQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("BILL_QTY").Value), 0, .Fields("BILL_QTY").Value)))

                SprdMain.Col = ColReceivedQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("RECEIVED_QTY").Value), 0, .Fields("RECEIVED_QTY").Value)))

                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))

                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("Amount").Value), 0, .Fields("Amount").Value)))

                SprdMain.Col = ColItemCost
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_COST").Value), 0, .Fields("ITEM_COST").Value)))

                SprdMain.Col = ColStockType
                SprdMain.Text = IIf(IsDbNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)

                SprdMain.Col = ColAcceptQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ACCEPTED_QTY").Value), 0, .Fields("ACCEPTED_QTY").Value)))

                SprdMain.Col = ColReOfferedQty
                mReworkQty = GetReworkQty(mRefPoNo, mItemCode)
                SprdMain.Text = CStr(mReworkQty)

                SprdMain.Col = ColRejQty
                mRejQty = GetRejectedQty(mRefPoNo, mItemCode)
                SprdMain.Text = CStr(mRejQty) ''- mReworkQty

                SprdMain.Col = ColDespQty
                mDespQty = GetDespatchQty(mItemCode, IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value))
                SprdMain.Text = CStr(mDespQty)

                SprdMain.Col = ColRework
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("LOT_ACC_RWK").Value), 0, .Fields("LOT_ACC_RWK").Value)))

                SprdMain.Col = ColQCEMP
                SprdMain.Text = IIf(IsDbNull(.Fields("QC_EMP_CODE").Value), "", .Fields("QC_EMP_CODE").Value)

                SprdMain.Col = ColQCDate
                SprdMain.Text = IIf(IsDbNull(.Fields("MRR_QCDATE").Value), "", .Fields("MRR_QCDATE").Value)

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
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_GATE_DET " & vbCrLf & " Where AUTO_KEY_MRR=" & Val(CStr(mAutoKey)) & "" & vbCrLf & " AND REJECTED_QTY-LOT_ACC_RWK>0 " & vbCrLf & " Order By SERIAL_NO"

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

                SprdMain.Col = ColBatchNo
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("LOT_NO").Value), "", .Fields("LOT_NO").Value))

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Row = I
                SprdMain.Col = ColBillQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("BILL_QTY").Value), 0, .Fields("BILL_QTY").Value)))

                SprdMain.Col = ColReceivedQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("RECEIVED_QTY").Value), 0, .Fields("RECEIVED_QTY").Value)))

                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))

                SprdMain.Col = ColItemCost
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_COST").Value), 0, .Fields("ITEM_COST").Value)))

                SprdMain.Col = ColStockType
                SprdMain.Text = IIf(IsDbNull(.Fields("STOCK_TYPE").Value) Or .Fields("STOCK_TYPE").Value = "RJ", "ST", .Fields("STOCK_TYPE").Value)

                SprdMain.Col = ColAcceptQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("LOT_ACCEPT").Value), 0, .Fields("LOT_ACCEPT").Value)))

                SprdMain.Col = ColReOfferedQty
                mReworkQty = GetReworkQty(mRefPoNo, mItemCode)
                SprdMain.Text = CStr(mReworkQty)

                SprdMain.Col = ColRejQty
                '            mReworkQty = GetReworkQty(mRefPoNo, mItemCode)
                mRejQty = GetRejectedQty(mRefPoNo, mItemCode)
                SprdMain.Text = CStr(mRejQty) ''Val(IIf(IsNull(!REJECTED_QTY), 0, !REJECTED_QTY)) - mReworkQty

                SprdMain.Col = ColDespQty
                mDespQty = GetDespatchQty(mItemCode, IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value))
                SprdMain.Text = CStr(mDespQty)

                SprdMain.Col = ColRework
                SprdMain.Text = CStr(0)

                SprdMain.Col = ColQCEMP
                SprdMain.Text = IIf(IsDbNull(.Fields("QC_EMP_CODE").Value), "", .Fields("QC_EMP_CODE").Value)

                SprdMain.Col = ColQCDate
                SprdMain.Text = IIf(IsDbNull(.Fields("MRR_QCDATE").Value), "", .Fields("MRR_QCDATE").Value)

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
    Private Sub CalcTots()
        On Error GoTo ERR1

        Dim CntRow As Integer

        Dim mQty As Double
        Dim mRate As Double
        Dim mDiscount As Double
        Dim mItemValue As Double

        Dim mTaxableExpValue As Double
        Dim mTotalValue As Double

        Dim mSalesTax As String
        Dim mSTPERCENT As Double
        Dim mST As Double
        Dim mTotalST As Double

        Dim mAmount As Double

        Dim mTotAmt As Double
        Dim mTotQty As Double
        Dim mTotExp As Double
        Dim mNetAmt As Double


        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow

                .Col = ColPONo
                If .Text = "" Then GoTo DontCalc

                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc

                .Col = ColReceivedQty
                mQty = Val(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                mItemValue = (mQty * mRate)

                .Col = ColAmount
                .Text = CStr(mItemValue)

                mTotAmt = mTotAmt + CDbl(VB6.Format(mItemValue, "0.00"))
                mTotQty = mTotQty + mQty
DontCalc:
            Next CntRow
        End With

        Call CalcExpTots(mTotAmt)

        '    mTotExp = lblTotExpValue.text
        '    mNetAmt = mTotAmt + mTotExp
        lblTotItemValue.Text = VB6.Format(mTotAmt, "0.00")
        lblTotQty.Text = VB6.Format(mTotQty, "0.00")

        Exit Sub
ERR1:
        'Resume
        If Err.Number = 6 Then Resume Next 'OverFlow
        MsgInformation(Err.Description)
    End Sub

    Private Sub CalcLandedCost()
        Dim ii As Integer

        Dim mItemAmount As Double
        Dim mItemValue As Double
        Dim mExpAmount As Double
        Dim mItemCost As Double
        Dim mQty As Double
        Dim mRate As Double

        On Error GoTo ERR1
        mItemAmount = CalcItemAmount
        mExpAmount = Val(lblTotExpAmt.Text)

        With SprdMain
            For ii = 1 To .MaxRows - 1
                .Row = ii
                .Col = ColAmount
                mItemValue = Val(.Text)

                If mItemAmount = 0 Then
                    mItemCost = 0
                Else
                    mItemCost = mExpAmount * mItemValue / mItemAmount
                End If

                .Col = ColBillQty
                mQty = Val(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColItemCost
                If mQty > 0 Then
                    .Text = CStr(mRate + (mItemCost / mQty))
                Else
                    .Text = CStr(0)
                End If

            Next ii
        End With
        Exit Sub
ERR1:
        ''Resume
        MsgInformation(Err.Description)
    End Sub
    Function CalcItemAmount() As Double
        Dim ii As Integer

        On Error GoTo ERR1
        CalcItemAmount = 0
        With SprdMain
            For ii = 1 To .MaxRows - 1
                .Row = ii
                .Col = ColAmount
                CalcItemAmount = CalcItemAmount + Val(.Text)

                ''            .Col = ColSTAmt
                ''            CalcItemAmount = CalcItemAmount + Val(.Text)
            Next ii
        End With

        Exit Function
ERR1:
        'Resume
        MsgInformation(Err.Description)
    End Function
    Private Sub CalcExpTots(ByRef mTotAmt As Double)
        On Error GoTo ERR1

        Dim CntRow As Integer

        Dim mExpCode As Integer
        Dim xStr As String
        Dim mExpPercent As Double
        'Dim xName As String
        Dim mExp As Double
        Dim mTotExp As Double
        Dim mSTPERCENT As Double
        'Dim mTotalST As Double
        'Dim mTotDiscount As Double
        Dim mRoType As String
        Dim mExpAddDeduct As String
        Dim mTotItemAmount As Double
        Dim mDiscount As Double
        Dim mTotDiscount As Double
        Dim mExciseableAmount As Double
        Dim mNetAccessAmt As Double
        Dim mTaxableAmount As Double
        Dim xEDAmount As Double
        Dim mEDAmount As Double
        Dim mSTAmount As Double
        Dim mSURAmount As Double
        Dim mMSC As Double
        Dim mOTRCharges As Double
        Dim mRO As Double
        Dim mADEAmount As Double

        mExp = 0
        mTotExp = 0
        mSTPERCENT = 0
        mADEAmount = 0

        mNetAccessAmt = Val(CStr(mTotAmt))
        mTaxableAmount = Val(lblTotItemValue.Text)

        With SprdExp
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = 0
                If .Text = "Del" Then GoTo DontCalc1

                .Col = ColRO
                mRoType = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColExpIdent
                xStr = .Text

                .Col = ColExpPercent
                mExpPercent = Val(.Text)

                .Col = ColExpAddDeduct
                mExpAddDeduct = Trim(.Text)

                .Col = ColExpSTCode
                mExpCode = Val(.Text)
                '            If MainClass.ValidateWithMasterTable(.Text, "Code", "ROUNDOFF", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                mRoType = MasterNo
                '            Else
                '                mRoType = MasterNo
                '            End If

                .Col = ColExpAmt
                Select Case xStr

                    Case "DOB"
                        If mExpPercent <> 0 Then
                            .Text = VB6.Format(Val(CStr(mTotAmt)) * mExpPercent / 100, "0.00")
                            If mRoType = "Y" Then
                                .Text = CStr(System.Math.Round(Val(.Text), 0))
                            End If
                        End If

                        mDiscount = Val(.Text)
                        mTotDiscount = mTotDiscount + (mDiscount * IIf(mExpAddDeduct = "D", -1, 1))

                        If MainClass.ValidateWithMasterTable(mExpCode, "CODE", "EXCISEABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            If MasterNo = "Y" Then
                                mExciseableAmount = Val(CStr(mExciseableAmount)) - Val(CStr(mDiscount))
                                mNetAccessAmt = Val(CStr(mNetAccessAmt)) - Val(CStr(mDiscount))
                            End If
                        End If

                        If MainClass.ValidateWithMasterTable(mExpCode, "CODE", "TAXABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            If MasterNo = "Y" Then
                                mTaxableAmount = Val(CStr(mTaxableAmount)) - Val(CStr(mDiscount))
                            End If
                        End If



                        mExp = mDiscount
                    Case "ED"
                        If mExpPercent <> 0 Then
                            .Text = VB6.Format(Val(CStr(mNetAccessAmt)) * mExpPercent / 100, "0.00")
                            If mRoType = "Y" Then
                                .Text = CStr(System.Math.Round(Val(.Text), 0))
                            End If
                        End If

                        xEDAmount = Val(.Text)
                        mEDAmount = mEDAmount + Val(.Text)
                        If Val(.Text) <> 0 Then
                            lblEDPercentage.Text = CStr(Val(CStr(mExpPercent)))
                        End If
                        ''mExciseableAmount = mNetAccessAmt   ''+ mEDAmount

                        If MainClass.ValidateWithMasterTable(mExpCode, "CODE", "TAXABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            If MasterNo = "Y" Then
                                mTaxableAmount = Val(CStr(mTaxableAmount)) + xEDAmount
                            End If
                        End If

                        mExp = xEDAmount
                    Case "ADE"
                        If mExpPercent <> 0 Then
                            .Text = VB6.Format(mEDAmount * mExpPercent / 100, "0.00")
                            If mRoType = "Y" Then
                                .Text = CStr(System.Math.Round(Val(.Text), 0))
                            End If
                        End If

                        mADEAmount = Val(.Text)
                        If MainClass.ValidateWithMasterTable(mExpCode, "CODE", "TAXABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            If MasterNo = "Y" Then
                                mTaxableAmount = Val(CStr(mTaxableAmount)) + mADEAmount
                            End If
                        End If
                        mExp = mADEAmount
                    Case "ST"
                        If mExpPercent <> 0 Then
                            .Text = VB6.Format(mTaxableAmount * mExpPercent / 100, "0.00")
                            If mRoType = "Y" Then
                                .Text = CStr(System.Math.Round(Val(.Text), 0))
                            End If
                        End If
                        If Val(.Text) <> 0 Then
                            lblSTPercentage.Text = CStr(Val(CStr(mExpPercent)))
                        End If
                        mSTAmount = Val(CStr(mSTAmount)) + Val(.Text)
                        mExp = Val(.Text)
                    Case "SUR"
                        If mExpPercent <> 0 Then
                            .Text = VB6.Format(mSTAmount * mExpPercent / 100, "0.00")
                            If mRoType = "Y" Then
                                .Text = CStr(System.Math.Round(Val(.Text), 0))
                            End If
                        End If
                        '                    If Val(.Text) <> 0 Then
                        '                        lblSTPercentage.text = Val(mExpPercent)
                        '                    End If
                        mSURAmount = mSURAmount + Val(.Text)
                        mExp = Val(.Text)
                    Case "MSC"
                        mMSC = mMSC + (Val(.Text) * IIf(mExpAddDeduct = "D", -1, 1))
                        mExp = Val(.Text)
                    Case "OTR", "FRO"
                        mOTRCharges = mOTRCharges + (Val(.Text) * IIf(mExpAddDeduct = "D", -1, 1))
                        If MainClass.ValidateWithMasterTable(mExpCode, "CODE", "TAXABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            If MasterNo = "Y" Then
                                mTaxableAmount = Val(CStr(mTaxableAmount)) + (Val(.Text) * IIf(mExpAddDeduct = "D", -1, 1))
                                '                            mTotSTRefundableAmt = Val(mTotSTRefundableAmt) + (Val(.Text) * IIf(mExpAddDeduct = "D", -1, 1))
                            End If
                        End If
                        '                    mOTRCharges = mOTRCharges + (Val(.Text) * IIf(mExpAddDeduct = "D", -1, 1))
                        mExp = Val(.Text)

                        '                    mOTRCharges = mOTRCharges + (Val(.Text) * IIf(mExpAddDeduct = "D", -1, 1))
                        '                    mExp = Val(.Text)
                    Case "RO"
                        mRO = mRO + (Val(.Text) * IIf(mExpAddDeduct = "D", -1, 1))
                        mExp = Val(.Text)
                End Select

                .Col = ColExpAddDeduct
                If xStr = "RO" Then
                    mTotExp = mTotExp + mExp
                Else
                    mTotExp = mTotExp + IIf(.Text = "D", -mExp, mExp)
                End If
DontCalc1:
            Next CntRow
        End With

        lblTotItemValue.Text = VB6.Format(mTotAmt, "#0.00")
        lblTotST.Text = VB6.Format(mSTAmount, "#0.00")
        lblTotED.Text = VB6.Format(mEDAmount, "#0.00")
        lblNetAmount.Text = VB6.Format(mTotExp + mTotAmt, "#0.00")
        lblTotFreight.Text = VB6.Format(mOTRCharges, "#0.00")
        lblTotCharges.Text = CStr(0) ''VB6.Format(mRO, "#0.00")
        lblTotExpAmt.Text = VB6.Format(mTotExp, "#0.00")
        lblTotTaxableAmt.Text = VB6.Format(mTaxableAmount, "#0.00")

        lblRO.Text = VB6.Format(mRO, "#0.00")
        lblDiscount.Text = VB6.Format(mTotDiscount, "#0.00")
        lblSurcharge.Text = VB6.Format(mSURAmount, "#0.00")
        lblMSC.Text = VB6.Format(mMSC, "#0.00")
        '    lblTotQty.text = VB6.Format(mTotQty, "#0.00")

        Call CalcLandedCost()

        Exit Sub
ERR1:
        ''Resume
        If Err.Number = 6 Then Resume Next 'OverFlow
        MsgInformation(Err.Description)

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

        chkCNReleased.Enabled = False
        chkCNReleased.CheckState = System.Windows.Forms.CheckState.Unchecked

        TxtSupplier.Text = ""
        txtBillNo.Text = ""
        txtBillDate.Text = "" 'VB6.Format(RunDate, "DD/MM/YYYY")
        txtRemarks.Text = ""

        txtRefDate.Enabled = False
        txtMRRDate.Enabled = False
        txtBillDate.Enabled = False
        TxtSupplier.Enabled = False
        txtBillNo.Enabled = False


        lblTotQty.Text = VB6.Format(0, "#0.00")
        lblTotItemValue.Text = VB6.Format(0, "#0.00")
        lblTotED.Text = VB6.Format(0, "#0.00")
        lblTotST.Text = VB6.Format(0, "#0.00")
        lblNetAmount.Text = VB6.Format(0, "#0.00")
        lblDiscount.Text = VB6.Format(0, "#0.00")
        lblSurcharge.Text = VB6.Format(0, "#0.00")
        lblRO.Text = VB6.Format(0, "#0.00")
        lblMSC.Text = VB6.Format(0, "#0.00")
        lblTotCharges.Text = VB6.Format(0, "#0.00")
        lblTotExpAmt.Text = VB6.Format(0, "#0.00")
        lblSTPercentage.Text = VB6.Format(0, "#0.00")
        lblEDPercentage.Text = VB6.Format(0, "#0.00")
        lblTotFreight.Text = VB6.Format(0, "#0.00")
        lblTotTaxableAmt.Text = VB6.Format(0, "#0.00")
        pQCDate = ""

        chkReofferPost.Enabled = IIf(lblPost.Text = "Y", True, False)
        cmdReOfferSearch.Visible = IIf(lblPost.Text = "Y", True, False)
        cmdReOfferSearch.Enabled = IIf(lblPost.Text = "Y", True, False)
        chkReofferPost.CheckState = System.Windows.Forms.CheckState.Unchecked

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ClearGrid(SprdExp)
        Call FillSprdExp()
        MainClass.ButtonStatus(Me, XRIGHT, RSROMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FillSprdExp()

        On Error GoTo ERR1
        Dim mLocal As String
        Dim SqlStr As String = ""

        Dim RS As ADODB.Recordset = Nothing
        Dim I As Integer

        MainClass.ClearGrid(SprdExp)

        If Trim(TxtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(TxtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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
    Private Sub FrmReOffer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmReOffer_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub FrmReOffer_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        chkCNReleased.Enabled = False
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

        'AdataItem.Visible = False
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
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        ESCol = eventArgs.col
        ESRow = eventArgs.row
        Select Case eventArgs.Col
            Case 1 'Exp.Name
                If eventArgs.NewRow >= ESRow Or eventArgs.NewRow = -1 Then
                    SprdExp.Row = ESRow

                    SprdExp.Col = 1
                    m_Exp = MainClass.AllowSingleQuote(SprdExp.Text)

                    If SprdExp.Text = "" Then Exit Sub
                    If m_Exp <> "" Then Exit Sub

                    SprdExp.Col = ColExpIdent
                    mIDENT = SprdExp.Text

                    SqlStr = "Select * From FIN_INTERFACE_MST Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND Name= '" & m_Exp & "'"
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
                If eventArgs.NewRow >= ESRow Or eventArgs.NewRow = -1 Then
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
                    ESRow = eventArgs.NewRow
                    GoTo ErrPart
                End If

        End Select
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

        SqlStr = " SELECT * FROM INV_REOFFER_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_REF=" & Val(mRefNo) & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSROMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RSROMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Ref, Use Generate MRR Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM INV_REOFFER_HDR " & " WHERE AUTO_KEY_REF=" & Val(xMkey) & " "
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




    Private Function GetReworkQty(ByRef pRefNo As Double, ByRef pItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mVNo As Double
        Dim mMRRNO As Double

        GetReworkQty = 0
        If Trim(txtRefNo.Text) = "" Then
            mVNo = -1
        Else
            mVNo = Val(txtRefNo.Text)
        End If

        If Trim(txtMRRNo.Text) = "" Then
            mMRRNO = -1
        Else
            mMRRNO = Val(txtMRRNo.Text)
        End If

        SqlStr = " SELECT SUM(LOT_ACC_RWK) AS ReQty" & vbCrLf & " FROM INV_REOFFER_HDR IH, INV_REOFFER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF " & vbCrLf & " AND IH.AUTO_KEY_MRR=" & Val(CStr(mMRRNO)) & ""

        If Val(CStr(pRefNo)) <> -1 Then
            SqlStr = SqlStr & vbCrLf & " AND ID.REF_PO_NO=" & Val(CStr(pRefNo)) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.AUTO_KEY_REF<>" & mVNo & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetReworkQty = IIf(IsDbNull(RsTemp.Fields("ReQty").Value), 0, RsTemp.Fields("ReQty").Value)
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetReworkQty = 0
    End Function

    Private Function GetRejectedQty(ByRef pRefNo As Double, ByRef pItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMRRNO As Double

        GetRejectedQty = 0

        If Trim(txtMRRNo.Text) = "" Then
            mMRRNO = -1
        Else
            mMRRNO = Val(txtMRRNo.Text)
        End If

        SqlStr = " SELECT SUM(REJECTED_QTY) AS RejQty" & vbCrLf & " FROM INV_GATE_DET ID" & vbCrLf & " WHERE  " & vbCrLf & " ID.AUTO_KEY_MRR=" & Val(CStr(mMRRNO)) & ""

        If Val(CStr(pRefNo)) <> -1 Then
            SqlStr = SqlStr & vbCrLf & " AND ID.REF_AUTO_KEY_NO=" & Val(CStr(pRefNo)) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetRejectedQty = IIf(IsDbNull(RsTemp.Fields("RejQty").Value), 0, RsTemp.Fields("RejQty").Value)
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetRejectedQty = 0
    End Function
End Class
