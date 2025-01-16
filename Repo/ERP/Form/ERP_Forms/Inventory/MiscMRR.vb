Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Imports AxFPSpreadADO
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb


Friend Class FrmMiscMRR
    Inherits System.Windows.Forms.Form
    Dim RsGEMain As ADODB.Recordset
    Dim RsGEDetail As ADODB.Recordset

    'Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim mSupplierCode As String


    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColPartNo As Short = 3
    Private Const ColUnit As Short = 4
    Private Const ColStoreLoc As Short = 5
    Private Const ColBillQty As Short = 6
    Private Const ColReceivedQty As Short = 7
    Private Const ColRate As Short = 8
    Private Const ColAmount As Short = 9

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub chkMaterialOut_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMaterialOut.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkGateEntryMade_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkGateEntryMade.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtMRRNo.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            txtMRRDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            cmdMRRSearch.Enabled = False
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Clear1()
            Show1()
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

        If ValidateBranchLocking((txtMRRDate.Text)) = True Then
            Exit Sub
        End If
        If ValidateBookLocking(PubDBCn, CInt(ConLockMiscMRR), txtMRRDate.Text) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, txtMRRDate.Text, (TxtSupplier.Text), mSupplierCode) = True Then
            Exit Sub
        End If

        If Trim(txtMRRNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If CheckBillPayment(mSupplierCode, (txtBillNo.Text), "B", (txtBillDate.Text)) = True Then Exit Sub

        If chkGateEntryMade.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("Gate Entry Made, So that can't be deleted.", MsgBoxStyle.Information)
            Exit Sub
        End If

        If chkMaterialOut.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("Material Out, So that can't be deleted.", MsgBoxStyle.Information)
            Exit Sub
        End If

        If chkReversed.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("Bill reversed, So that can't be deleted.", MsgBoxStyle.Information)
            Exit Sub
        End If

        If Not RsGEMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_MISC_GATE_HDR", (txtMRRNo.Text), RsGEMain, "REFNo") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_MISC_GATE_HDR", "AUTO_KEY_NO", (lblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("Delete from INV_MISC_GATE_DET Where AUTO_KEY_NO=" & Val(lblMKey.Text) & "")
                PubDBCn.Execute("Delete from INV_MISC_GATE_HDR Where AUTO_KEY_NO=" & Val(lblMKey.Text) & "")

                PubDBCn.CommitTrans()
                RsGEMain.Requery() ''.Refresh
                RsGEDetail.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsGEMain.Requery() ''.Refresh
        RsGEDetail.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '        Resume
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsGEMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & " "


        SqlStr = "SELECT AUTO_KEY_NO, REF_DATE, SUPP_CUST_CODE,BILL_TO_LOC_ID, BILL_NO, BILL_DATE, DECODE(IS_OUT,'Y','YES','NO') AS IS_OUT " & vbCrLf _
                & " FROM INV_MISC_GATE_HDR" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & " "


        '& vbCrLf _
        '        & " And AUTO_KEY_NO='" & MainClass.AllowSingleQuote(xAcctCode) & "'"



        ''

        'If MainClass.SearchGridMaster((txtMRRNo.Text), "INV_MISC_GATE_HDR", "AUTO_KEY_NO", "REF_DATE", "SUPP_CUST_CODE", , SqlStr) = True Then
        If MainClass.SearchGridMasterBySQL2(txtMRRNo.Text, SqlStr) = True Then
            txtMRRNo.Text = AcName
            TxtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONMRR(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONMRR(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportONMRR(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""

        Call MainClass.ClearCRptFormulas(Report1)

        Call SelectQryForMRR(SqlStr)


        mTitle = "MISC GATE ENTRY"
        mSubTitle = ""
        mRptFileName = "MISCMRR.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Function SelectQryForMRR(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*, CMST.*"

        'mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf _
        '    & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf _
        '    & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf _
        '    & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf _
        '    & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO "

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf _
            & " FROM INV_MISC_GATE_HDR IH, INV_MISC_GATE_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_BUSINESS_MST CMST"

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.AUTO_KEY_NO=ID.AUTO_KEY_NO" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_NO=" & Val(txtMRRNo.Text) & ""


        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForMRR = mSqlStr
    End Function


    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        Call CalcTots()

        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            TxtMRRNo_Validating(TxtMRRNo, New System.ComponentModel.CancelEventArgs(False))

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
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""      ''& vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((TxtSupplier.Text), "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", , SqlStr) = True Then
            TxtSupplier.Text = AcName
            txtBillTo.Text = AcName2
            txtsupplier_Validating(TxtSupplier, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent


        Dim mItemDesc As String
        Dim DelStatus As Boolean

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "CUSTOMER_PART_NO", "ISSUE_UOM", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName)

                    .Col = ColItemDesc
                    .Text = Trim(AcName1)

                    .Col = ColPartNo
                    .Text = Trim(AcName2)

                    .Col = ColUnit
                    .Text = Trim(AcName3)

                End If

                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColItemCode
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then

                mItemDesc = SprdMain.Text


                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode, DelStatus)
                FormatSprdMain(-1)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
            End If
        End If

        CalcTots()
    End Sub
    Private Function FillGridRow(ByRef mRow As Long, ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsMisc As ADODB.Recordset = Nothing

        If mItemCode = "" Then Exit Function

        SqlStr = ""
        SqlStr = " SELECT Item_Code,ITEM_SHORT_DESC AS NAME, HSN_CODE," & vbCrLf _
            & " PURCHASE_UOM, CUSTOMER_PART_NO " & vbCrLf _
            & " FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND Item_Code='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = mRow
            With RsMisc

                SprdMain.Col = ColItemCode
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                SprdMain.Col = ColItemDesc
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("Name").Value), "", .Fields("Name").Value))

                SprdMain.Col = ColPartNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value))

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)

            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, mRow, ColItemCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell


        Dim mBillQty As Double
        Dim mRecdQty As Double
        Dim SqlStr As String = ""
        Dim xItemDesc As String
        Dim pRow As Long

        If eventArgs.NewRow = -1 Then Exit Sub


        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow
                pRow = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                xItemDesc = Trim(SprdMain.Text)
                If xItemDesc = "" Then Exit Sub

                If DuplicateItemCode("C") = False Then
                    SprdMain.Row = pRow
                    If FillGridRow(pRow, xItemDesc) = False Then Exit Sub
                    FormatSprdMain(eventArgs.row)
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                End If
            Case ColItemDesc
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemDesc
                xItemDesc = Trim(SprdMain.Text)
                If xItemDesc = "" Then Exit Sub

                If DuplicateItemCode("D") = False Then
                    FormatSprdMain(eventArgs.row)
                    MainClass.AddBlankSprdRow(SprdMain, ColItemDesc, ConRowHeight)
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemDesc)
                End If

            Case ColBillQty
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xItemDesc = SprdMain.Text
                If xItemDesc = "" Then Exit Sub

                SprdMain.Col = ColBillQty
                mBillQty = Val(SprdMain.Text)

                SprdMain.Col = ColReceivedQty
                mRecdQty = Val(SprdMain.Text)

                If mBillQty < mRecdQty Then
                    MsgInformation("Bill Qty Cann't be less than Recd Qty.")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColBillQty)
                End If

            Case ColReceivedQty
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xItemDesc = SprdMain.Text
                If xItemDesc = "" Then Exit Sub

                SprdMain.Col = ColBillQty
                mBillQty = Val(SprdMain.Text)

                SprdMain.Col = ColReceivedQty
                mRecdQty = Val(SprdMain.Text)

                If mRecdQty > mBillQty Then
                    MsgInformation("Recd Qty Cann't be Greater than Bill Qty.")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColReceivedQty)
                End If
        End Select
        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function DuplicateItemCode(pCheckOn As String) As Boolean

        Dim CntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemDesc As String
        Dim mItemDesc As String

        With SprdMain
            .Row = .ActiveRow
            If pCheckOn = "C" Then
                .Col = ColItemCode
            Else
                .Col = ColItemDesc
            End If
            mCheckItemDesc = Trim(UCase(.Text))

            For CntRow = 1 To .MaxRows
                .Row = CntRow

                If pCheckOn = "C" Then
                    .Col = ColItemCode
                Else
                    .Col = ColItemDesc
                End If

                mItemDesc = Trim(UCase(.Text))

                If (mItemDesc = mCheckItemDesc And mCheckItemDesc <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItemCode = True
                    MsgInformation("Duplicate Item Description. ")
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                    Exit Function
                End If
            Next
        End With
    End Function
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mMRRNo As String

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        mMRRNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(0))

        txtMRRNo.Text = CStr(Val(mMRRNo))

        TxtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())

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
        Dim xMkey As String = ""
        Dim mMRRNO As String
        Dim SqlStr As String = ""

        If Trim(txtMRRNo.Text) = "" Then GoTo EventExitSub

        If Len(txtMRRNo.Text) < 6 Then
            txtMRRNo.Text = Val(txtMRRNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsGEMain.EOF = False Then xMkey = RsGEMain.Fields("AUTO_KEY_NO").Value
        mMRRNO = Trim(txtMRRNo.Text)

        SqlStr = " SELECT * FROM INV_MISC_GATE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_NO=" & Val(mMRRNO) & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGEMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsGEMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such MRR, Use Generate MRR Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM INV_MISC_GATE_HDR " & " WHERE AUTO_KEY_NO=" & Val(xMkey) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGEMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        Dim I As Short
        Dim SqlStr As String = ""
        Dim nMkey As String
        Dim mVNoSeq As Double
        Dim mSuppCustCode As String
        Dim mGateEntryMade As String
        Dim mIsMaterialOut As String
        Dim mIsReversed As String

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


        If Val(txtMRRNo.Text) = 0 Then
            mVNoSeq = CDbl(AutoGenSeqNo())
        Else
            mVNoSeq = Val(txtMRRNo.Text)
        End If

        txtMRRNo.Text = CStr(Val(CStr(mVNoSeq)))
        mGateEntryMade = IIf(chkGateEntryMade.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mIsMaterialOut = IIf(chkMaterialOut.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsReversed = IIf(chkReversed.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If CheckValidVDate(mVNoSeq) = False Then GoTo ErrPart

        SqlStr = ""

        If ADDMode = True Then
            lblMKey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO INV_MISC_GATE_HDR( " & vbCrLf _
                & " COMPANY_CODE, AUTO_KEY_NO, REF_DATE," & vbCrLf _
                & " SUPP_CUST_CODE, BILL_NO, BILL_DATE," & vbCrLf _
                & " TRANSPORT_MODE, VEHICLE, REMARKS, GATE_ENTRY_MADE, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE,BILL_TO_LOC_ID, IS_OUT, OUT_DATE, IS_REVERSED) "


            SqlStr = SqlStr & vbCrLf _
                & " VALUES(" & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(CStr(mVNoSeq)) & ", TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mSuppCustCode) & "', '" & MainClass.AllowSingleQuote((txtBillNo.Text)) & "', TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((TxtTransporter.Text)) & "', '" & MainClass.AllowSingleQuote((txtVehicle.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((TxtRemarks.Text)) & "', '" & mGateEntryMade & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','" & MainClass.AllowSingleQuote(txtBillTo.Text) & "', '" & mIsMaterialOut & "', TO_DATE('" & VB6.Format(txtMaterialOutDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'" & mIsReversed & "')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE INV_MISC_GATE_HDR SET " & vbCrLf _
                & " AUTO_KEY_NO =" & Val(CStr(mVNoSeq)) & " , GATE_ENTRY_MADE='" & mGateEntryMade & "'," & vbCrLf _
                & " REF_DATE=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "'," & vbCrLf _
                & " BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote((txtBillTo.Text)) & "', " & vbCrLf _
                & " BILL_NO='" & MainClass.AllowSingleQuote((txtBillNo.Text)) & "'," & vbCrLf _
                & " BILL_DATE=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " VEHICLE='" & MainClass.AllowSingleQuote((txtVehicle.Text)) & "'," & vbCrLf _
                & " TRANSPORT_MODE='" & MainClass.AllowSingleQuote((TxtTransporter.Text)) & "',IS_REVERSED='" & mIsReversed & "',IS_OUT='" & mIsMaterialOut & "',OUT_DATE= TO_DATE('" & VB6.Format(txtMaterialOutDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote((TxtRemarks.Text)) & "'," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE AUTO_KEY_NO ='" & MainClass.AllowSingleQuote((LblMkey.Text)) & "'"
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1() = False Then GoTo ErrPart

        UpdateMain1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsGEMain.Requery() ''.Refresh
        RsGEDetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume
    End Function

    Private Function CheckValidVDate(ByRef pMRRNoSeq As Double) As Object

        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim mRsCheck1 As ADODB.Recordset = Nothing
        Dim mRsCheck2 As ADODB.Recordset = Nothing
        Dim mBackBillDate As String = ""
        Dim mMaxInvStrfNo As Integer
        CheckValidVDate = True

        If txtMRRNo.Text = 1 & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") Then Exit Function

        SqlStr = "SELECT MAX(REF_DATE)" & vbCrLf & " FROM INV_MISC_GATE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_NO<" & Val(CStr(pMRRNoSeq)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDbNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If

        SqlStr = "SELECT MIN(REF_DATE)" & " FROM INV_MISC_GATE_HDR " & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_NO>" & Val(CStr(pMRRNoSeq)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck1, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck1.EOF = False And Not IsDbNull(mRsCheck1.Fields(0).Value) And mRsCheck2.EOF = False And Not IsDbNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtMRRDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Gate Entry Date Is Greater Than The Gate Entry Date Of Next MRR No.")
                CheckValidVDate = False
            ElseIf CDate(txtMRRDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Gate Entry Date Is Less Than The Gate Entry Date Of Previous MRR No.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck1.EOF = False And Not IsDbNull(mRsCheck1.Fields(0).Value) Then
            If CDate(txtMRRDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Gate Entry Date Is Greater Than The Gate Entry Date Of Next MRR No.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck2.EOF = False And Not IsDbNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtMRRDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Gate Entry Date Is Less Than The Gate Entry Date Of Previous MRR No.")
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
        Dim RsGEMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_NO)  " & vbCrLf & " FROM INV_MISC_GATE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGEMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsGEMainGen
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

    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemDesc As String
        Dim mUnit As String
        Dim mBillQty As Double
        Dim mRecdQty As Double
        Dim mRecord As Boolean
        Dim mRate As Double
        Dim mAmount As Double
        Dim mItemCode As String
        Dim mStoreLoc As String

        mRecord = False
        PubDBCn.Execute("Delete From INV_MISC_GATE_DET Where AUTO_KEY_NO='" & lblMKey.Text & "'")

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColBillQty
                mBillQty = Val(.Text)

                .Col = ColReceivedQty
                mRecdQty = Val(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColAmount
                mAmount = Val(.Text)

                .Col = ColStoreLoc
                mStoreLoc = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mItemDesc <> "" And mBillQty > 0 Then
                    SqlStr = " INSERT INTO INV_MISC_GATE_DET ( " & vbCrLf _
                        & " COMPANY_CODE, AUTO_KEY_NO, SERIAL_NO, " & vbCrLf _
                        & " ITEM_CODE, ITEM_DESC, ITEM_UOM, " & vbCrLf _
                        & " BILL_QTY, RECEIVED_QTY, ITEM_RATE, ITEM_AMOUNT, LOC_CODE) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " '" & LblMkey.Text & "'," & I & ", " & vbCrLf _
                        & " '" & mItemCode & "', '" & mItemDesc & "', '" & mUnit & "'," & vbCrLf _
                        & " " & mBillQty & ", " & mRecdQty & ", " & mRate & ", " & mAmount & ",'" & mStoreLoc & "')"

                    PubDBCn.Execute(SqlStr)

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
        Dim mItemCode As String
        Dim mLotNoRequied As String

        FieldsVarification = True

        If ValidateBranchLocking((txtMRRDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateBookLocking(PubDBCn, CInt(ConLockMiscMRR), txtMRRDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateAccountLocking(PubDBCn, txtMRRDate.Text, (TxtSupplier.Text), mSupplierCode) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Supplier Does Not Exist In Master", MsgBoxStyle.Information)
            'txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        Else
            mSupplierCode = MasterNo
        End If


        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsGEMain.EOF = True Then Exit Function

        If MODIFYMode = True And txtMRRNo.Text = "" Then
            MsgInformation("Voucher No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtMRRDate.Text = "" Then
            MsgBox("VDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtMRRDate.Focus()
            Exit Function
        ElseIf FYChk((txtMRRDate.Text)) = False Then
            FieldsVarification = False
            If txtMRRDate.Enabled = True Then txtMRRDate.Focus()
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
            MsgBox("VDate Can Not be Less Than BillDate.")
            FieldsVarification = False
            If txtBillDate.Enabled = True Then txtBillDate.Focus()
            Exit Function
        End If

        If CDate(txtMRRDate.Text) < CDate(txtBillDate.Text) Then
            MsgBox("VDate Can Not be Less Than BillDate.")
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

        If DuplicateBillNo(mSupplierCode) = True Then
            MsgBox("Duplicate Bill No for Such Supplier.", MsgBoxStyle.Information)
            If txtBillNo.Enabled = True Then txtBillNo.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Supplier Does Not Exist In Master", MsgBoxStyle.Information)
            'txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtBillTo.Text) = "" Then
            MsgInformation("Bill To is blank. Cannot Save")
            txtBillTo.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "'") = False Then
                MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
                txtBillTo.Focus()
                FieldsVarification = False
            End If
        End If

        mWithInState = GetPartyBusinessDetail(Trim(TxtSupplier.Text), Trim(txtBillTo.Text), "WITHIN_STATE")

        If chkGateEntryMade.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("Gate Entry Made, So that can't be change.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If chkMaterialOut.CheckState = System.Windows.Forms.CheckState.Checked Then     '' And Not IsDate(txtMaterialOutDate.Text)
            'MsgBox("Please Enter the Out Time.", MsgBoxStyle.Information)  
            'FieldsVarification = False
            'Exit Function
            If txtMaterialOutDate.Text = "" Then
                MsgBox("Out Date is Blank", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtMaterialOutDate.Enabled = True Then txtMaterialOutDate.Focus()
                Exit Function
            ElseIf Not IsDate(txtMaterialOutDate.Text) Then
                MsgBox("Invalid Out Date", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtMaterialOutDate.Enabled = True Then txtMaterialOutDate.Focus()
                Exit Function
            End If
        End If

        If chkMaterialOut.CheckState = System.Windows.Forms.CheckState.Checked And chkMaterialOut.Enabled = False Then
            MsgBox("Material Out, So that can't be change.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If chkMaterialOut.CheckState = System.Windows.Forms.CheckState.Unchecked And chkReversed.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("Please select Material Out, So that can't be change.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Itrem Code Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColItemDesc, "S", "ItemDesc Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColBillQty, "N", "Please Check Bill Quantity.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmMiscMRR_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Misc. Gate Entry"

        SqlStr = ""
        SqlStr = "Select * from INV_MISC_GATE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGEMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_MISC_GATE_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGEDetail, ADODB.LockTypeEnum.adLockReadOnly)

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
    Private Sub AssignGrid(ByVal mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)
        SqlStr = ""

        'MainClass.ClearGrid(SprdView)

        SqlStr = "Select GR.AUTO_KEY_NO as MRR_No," & vbCrLf _
            & " TO_CHAR(GR.REF_DATE,'DD-MM-YYYY') as REF_DATE, " & vbCrLf _
            & " AC.SUPP_CUST_NAME AS SupplierName, " & vbCrLf _
            & " GR.BILL_NO, " & vbCrLf _
            & " TO_CHAR(GR.BILL_DATE,'DD-MM-YYYY') AS BillDate, IS_OUT, OUT_DATE " & vbCrLf _
            & " FROM INV_MISC_GATE_HDR GR,FIN_SUPP_CUST_MST AC " & vbCrLf _
            & " WHERE " & vbCrLf & " GR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND GR.COMPANY_CODE=AC.COMPANY_CODE " & vbCrLf & " AND GR.SUPP_CUST_CODE=AC.SUPP_CUST_CODE " & vbCrLf _
            & " Order by AUTO_KEY_NO"

        'MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        'FormatSprdView()

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        Me.UltraGrid1.DataSource = ds
        Me.UltraGrid1.DataMember = ""

        CreateGridHeader("S")

        MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")
        oledbAdapter.Dispose()
        oledbCnn.Close()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume						
    End Sub

    Private Sub CreateGridHeader(pShowType As String)
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   to create the grid header
        'Comments       :   Nil
        '----------------------------------------------------------------------------
        Try
            Dim inti As Integer
            'create column header

            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Key = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "MRR No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "MRR Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Supplier Namee"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Bill No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "Bill Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "Is Out"
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Out Date"


            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment
            Next

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Width = 350
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 100



            'UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.MFGQty).MaskInput = "99999"

            Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            Me.UltraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

            'fill labels 
            'FillLabelsFromResFile(Me)
            'Catch sqlex As SqlException
            '    ErrorTrap(sqlex.Message, "frmRMReturn.vb", "CreateHeader", "", "", "Sql Exception")
            '    Me.Cursor = Windows.Forms.Cursors.Default
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub
    'Private Sub FormatSprdView()

    '    With SprdView
    '        .Row = -1

    '        .set_RowHeight(0, 600)

    '        .set_ColWidth(0, 600)

    '        .set_ColWidth(1, 1000)
    '        .set_ColWidth(2, 1000)
    '        .set_ColWidth(3, 3500)
    '        .set_ColWidth(4, 1200)
    '        .set_ColWidth(5, 1000)


    '        MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
    '        MainClass.SetSpreadColor(SprdView, -1)
    '        SprdView.set_RowHeight(-1, 300)
    '        .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
    '        MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
    '    End With
    'End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_CODE", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .ColsFrozen = ColItemDesc
            .set_ColWidth(ColItemDesc, 35)

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColPartNo, 12)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsGEDetail.Fields("ITEM_UOM").DefinedSize ''
            .set_ColWidth(ColUnit, 4)

            .Col = ColStoreLoc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsGEDetail.Fields("LOC_CODE").DefinedSize ''
            .set_ColWidth(ColStoreLoc, 8)


            For cntCol = ColBillQty To ColAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMin = CDbl("-99999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 12)
            Next

        End With

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColStoreLoc)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmount, ColAmount)

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsGEDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsGEMain

            txtMRRNo.Maxlength = .Fields("AUTO_KEY_NO").Precision
            txtMRRDate.Maxlength = 10
            TxtSupplier.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtBillNo.Maxlength = .Fields("BILL_NO").DefinedSize
            txtBillDate.Maxlength = 10
            TxtTransporter.Maxlength = .Fields("TRANSPORT_MODE").DefinedSize
            txtVehicle.Maxlength = .Fields("VEHICLE").DefinedSize
            txtBillTo.MaxLength = .Fields("BILL_TO_LOC_ID").DefinedSize
            TxtRemarks.Maxlength = .Fields("REMARKS").DefinedSize

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing

        With RsGEMain
            If Not .EOF Then
                lblMKey.Text = .Fields("AUTO_KEY_NO").Value
                txtMRRNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_NO").Value), "", .Fields("AUTO_KEY_NO").Value)
                txtMRRDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")

                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtSupplier.Text = MasterNo
                End If
                mSupplierCode = .Fields("SUPP_CUST_CODE").Value

                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)

                txtBillNo.Text = IIf(IsDbNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDbNull(.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value), "DD/MM/YYYY")
                txtVehicle.Text = IIf(IsDbNull(.Fields("VEHICLE").Value), "", .Fields("VEHICLE").Value)

                TxtTransporter.Text = IIf(IsDbNull(.Fields("TRANSPORT_MODE").Value), "", .Fields("TRANSPORT_MODE").Value)
                txtRemarks.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                chkGateEntryMade.CheckState = IIf(.Fields("GATE_ENTRY_MADE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                txtGateEntryNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_GATE").Value), "", .Fields("AUTO_KEY_GATE").Value)
                txtGateEntryDate.Text = VB6.Format(IIf(IsDbNull(.Fields("GATE_DATE").Value), "", .Fields("GATE_DATE").Value), "DD/MM/YYYY")

                chkMaterialOut.CheckState = IIf(.Fields("IS_OUT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                txtMaterialOutDate.Text = VB6.Format(IIf(IsDBNull(.Fields("OUT_DATE").Value), "", .Fields("OUT_DATE").Value), "DD/MM/YYYY")

                chkReversed.CheckState = IIf(.Fields("IS_REVERSED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                Call ShowDetail1((lblMKey.Text))

                Call CalcTots()
                TxtSupplier.Enabled = False
                cmdsearch.Enabled = False
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsGEMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True

        txtMRRNo.Enabled = True
        cmdMRRSearch.Enabled = True

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub ShowDetail1(ByRef mMKEY As String)

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
        Dim mRGPItemCode As String
        Dim mOpenOrder As Boolean


        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM INV_MISC_GATE_DET " & vbCrLf _
            & " Where AUTO_KEY_NO=" & Val(mMKEY) & "" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGEDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsGEDetail
            If .EOF = True Then Exit Sub
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = mItemCode

                SprdMain.Col = ColItemDesc
                mItemDesc = IIf(IsDbNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value)
                SprdMain.Text = mItemDesc

                mPartNo = ""
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mPartNo = MasterNo
                End If

                SprdMain.Col = ColPartNo
                SprdMain.Text = mPartNo

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColStoreLoc
                SprdMain.Text = IIf(IsDBNull(.Fields("LOC_CODE").Value), "", .Fields("LOC_CODE").Value)

                SprdMain.Row = I
                SprdMain.Col = ColBillQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("BILL_QTY").Value), 0, .Fields("BILL_QTY").Value)))

                SprdMain.Col = ColReceivedQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("RECEIVED_QTY").Value), 0, .Fields("RECEIVED_QTY").Value)))

                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))

                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_AMOUNT").Value), 0, .Fields("ITEM_AMOUNT").Value)))

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
            'FormatSprdView()
            'SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsGEMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1

        Dim CntRow As Integer

        Dim mQty As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mTotQty As Double


        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow

                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc

                .Col = ColBillQty
                mQty = Val(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColAmount
                .Text = VB6.Format(mQty * mRate, "0.00")

                mTotQty = mTotQty + mQty
DontCalc:
            Next CntRow
        End With

        lblTotQty.Text = VB6.Format(mTotQty, "0.00")
        Exit Sub
ERR1:
        'Resume
        If Err.Number = 6 Then Resume Next 'OverFlow
        MsgInformation(Err.Description)
    End Sub

    Private Sub Clear1()

        lblMKey.Text = ""


        mSupplierCode = CStr(-1)
        txtMRRNo.Text = ""
        txtMRRDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        TxtSupplier.Text = ""
        txtBillNo.Text = ""
        txtBillDate.Text = "" 'VB6.Format(RunDate, "DD/MM/YYYY")
        TxtTransporter.Text = ""
        txtVehicle.Text = ""
        txtRemarks.Text = ""
        txtMRRDate.Enabled = False
        txtBillDate.Enabled = True
        TxtSupplier.Enabled = True
        txtGateEntryNo.Text = ""
        txtGateEntryDate.Text = ""
        txtBillTo.Text = ""
        cmdsearch.Enabled = True
        chkGateEntryMade.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkMaterialOut.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtMaterialOutDate.Text = ""

        chkReversed.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkReversed.Enabled = False

        lblTotQty.Text = VB6.Format(0, "#0.00")

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsGEMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FrmMiscMRR_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmMiscMRR_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub FrmMiscMRR_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)


        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        CurrFormHeight = 7245
        CurrFormWidth = 11355


        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(6480) '8000
        ''Me.Width = VB6.TwipsToPixelsX(9240) '11900


        'AdataItem.Visible = False

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
        'Dim mPONo As Double

        mCol = SprdMain.ActiveCol

        SprdMain.Refresh()


        '    mCol = SprdMain.ActiveCol
        '    If KeyCode = vbKeyF3 And mCol = ColPONo And SprdMain.ActiveRow > 1 And Left(cboRefType.Text, 1) <> "R" Then
        '        SprdMain.Row = SprdMain.ActiveRow - 1
        '        SprdMain.Col = ColPONo
        '        mPONo = Val(SprdMain.Text)
        '
        '        SprdMain.Row = SprdMain.ActiveRow
        '        SprdMain.Col = ColPONo
        '        SprdMain.Text = mPONo
        '
        '    End If
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

        '    KeyAscii = MainClass.SetNumericField(KeyAscii)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtSupplier.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtSupplier.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
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
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PO " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY AUTO_KEY_PO,AMEND_NO"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart

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
    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtsupplier_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtSupplier.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(TxtSupplier.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""      ''& vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')"


        If Trim(txtBillTo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND LOCATION_ID='" & Trim(txtBillTo.Text) & "'"
        End If

        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mSupplierCode = MasterNo
        Else
            mSupplierCode = "-1"
            MsgInformation("Invalid Supplier Name.")
            Cancel = True
        End If

        If Trim(txtBillTo.Text) = "" Then
            txtBillTo.Text = GetDefaultLocation(mSupplierCode)
        End If



        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTransporter_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtTransporter.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtTransporter_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtTransporter.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtTransporter.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub



    Private Function DuplicateBillNo(ByRef pSuppCode As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMRRNO As Double

        If Trim(txtMRRNo.Text) = "" Then
            mMRRNO = -1
        Else
            mMRRNO = Val(txtMRRNo.Text)
        End If

        DuplicateBillNo = False
        SqlStr = "Select BILL_NO " & vbCrLf _
            & " FROM INV_MISC_GATE_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " And SUPP_CUST_CODE='" & pSuppCode & "'" & vbCrLf _
            & " AND AUTO_KEY_NO<>" & mMRRNO & " AND BILL_NO='" & Trim(txtBillNo.Text) & "' AND IS_OUT='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            DuplicateBillNo = True
        End If

        Exit Function
ErrPart:
        DuplicateBillNo = False
    End Function

    Private Sub txtVehicle_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtVehicle_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicle.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicle.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBillTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillTo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillTo.DoubleClick
        cmdBillToSearch_Click(cmdBillToSearch, New System.EventArgs())
    End Sub
    Private Sub txtBillTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBillTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBillTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdBillToSearch_Click(cmdBillToSearch, New System.EventArgs())
    End Sub
    Private Sub txtBillTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(TxtSupplier.Text) = "" Then GoTo EventExitSub
        If Trim(txtBillTo.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        Else
            MsgBox("Invalid Customer Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'") = False Then
            MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdBillToSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBillToSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim xAcctCode As String = ""

        If Trim(TxtSupplier.Text) = "" Then
            MsgInformation("Please select the Customer First")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        End If

        SqlStr = "SELECT LOCATION_ID, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE, GST_RGN_NO " & vbCrLf _
                & " FROM FIN_SUPP_CUST_BUSINESS_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'"

        If MainClass.SearchGridMasterBySQL2((txtBillTo.Text), SqlStr) = True Then
            txtBillTo.Text = AcName
            txtBillTo_Validating(txtBillTo, New System.ComponentModel.CancelEventArgs(False))
            If txtBillTo.Enabled = True Then txtBillTo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub FrmMiscMRR_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        Frasprd.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        Frasupp.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)

        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPopulateBill_Click(sender As Object, e As EventArgs) Handles cmdPopulateBill.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim xAcctCode As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTempDet As ADODB.Recordset = Nothing
        Dim mSaleMKey As String
        Dim cntRow As Long
        Dim mItemCode As String
        Dim mDespNo As Double
        Dim mSNo As Double

        If txtBillNo.Text = "" Then MsgInformation("Please Select the Bill No") : Exit Sub
        If TxtSupplier.Text = "" Then MsgInformation("Please Select the Customer Name") : Exit Sub

        If MainClass.ValidateWithMasterTable(Trim(TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            MsgInformation("Invaild Customer Name.")
            Exit Sub
        End If

        MainClass.ClearGrid(SprdMain)

        SqlStr = "SELECT MKEY,AUTO_KEY_DESP, BILLNO,INVOICE_DATE,CUST_PO_NO,CUST_PO_DATE,E_BILLWAYNO,VEHICLENO " & vbCrLf _
                & " FROM FIN_INVOICE_HDR" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'" & vbCrLf _
                & " AND BILLNO='" & txtBillNo.Text & "' AND CANCELLED='N' AND (GRNNO IS NULL OR GRNNO='')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        With RsTemp
            If Not .EOF Then
                'txtBillNo.Text = IIf(IsDBNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDBNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                mSaleMKey = IIf(IsDBNull(.Fields("MKEY").Value), "", .Fields("MKEY").Value)
                txtVehicle.Text = IIf(IsDBNull(.Fields("VEHICLENO").Value), "", .Fields("VEHICLENO").Value)
                mDespNo = IIf(IsDBNull(.Fields("AUTO_KEY_DESP").Value), 0, .Fields("AUTO_KEY_DESP").Value)

                ''Detail Part.....

                SqlStr = "SELECT * " & vbCrLf _
                        & " FROM FIN_INVOICE_DET ID" & vbCrLf _
                        & " WHERE ID.MKEY='" & mSaleMKey & "'"


                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)



                cntRow = 1
                With SprdMain
                    If RsTempDet.EOF = False Then
                        Do While RsTempDet.EOF = False
                            .Row = cntRow

                            mSNo = IIf(IsDBNull(RsTempDet.Fields("SUBROWNO").Value), 0, RsTempDet.Fields("SUBROWNO").Value)

                            .Col = ColItemCode
                            .Text = Trim(IIf(IsDBNull(RsTempDet.Fields("ITEM_CODE").Value), "", RsTempDet.Fields("ITEM_CODE").Value))
                            mItemCode = Trim(IIf(IsDBNull(RsTempDet.Fields("ITEM_CODE").Value), "", RsTempDet.Fields("ITEM_CODE").Value))

                            .Col = ColItemDesc
                            If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                .Text = Trim(MasterNo)
                            Else
                                .Text = ""
                            End If

                            .Col = ColUnit
                            .Text = Trim(IIf(IsDBNull(RsTempDet.Fields("ITEM_UOM").Value), "", RsTempDet.Fields("ITEM_UOM").Value))

                            .Col = ColStoreLoc
                            .Text = GetCustomerStoreLoc(mSNo, mDespNo, mItemCode)

                            .Col = ColPartNo
                            .Text = Trim(IIf(IsDBNull(RsTempDet.Fields("CUSTOMER_PART_NO").Value), "", RsTempDet.Fields("CUSTOMER_PART_NO").Value))

                            .Col = ColBillQty
                            .Text = Trim(IIf(IsDBNull(RsTempDet.Fields("ITEM_QTY").Value), "", RsTempDet.Fields("ITEM_QTY").Value))

                            .Col = ColReceivedQty
                            .Text = Trim(IIf(IsDBNull(RsTempDet.Fields("ITEM_QTY").Value), "", RsTempDet.Fields("ITEM_QTY").Value))

                            .Col = ColRate
                            .Text = Trim(IIf(IsDBNull(RsTempDet.Fields("ITEM_RATE").Value), "", RsTempDet.Fields("ITEM_RATE").Value))

                            .Col = ColAmount
                            .Text = Trim(IIf(IsDBNull(RsTempDet.Fields("ITEM_AMT").Value), "", RsTempDet.Fields("ITEM_AMT").Value))

                            cntRow = CntRow + 1
                            .MaxRows = CntRow
                            RsTempDet.MoveNext()
                        Loop
                    End If
                End With
            Else
                MsgInformation("Invalid Bill No.")
            End If
        End With



        FormatSprdMain(-1)

        Call CalcTots()

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

End Class
