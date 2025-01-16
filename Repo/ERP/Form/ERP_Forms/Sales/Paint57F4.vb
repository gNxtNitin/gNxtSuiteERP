Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmPaint57F4
    Inherits System.Windows.Forms.Form
    Dim RsSaleMain As ADODB.Recordset ''Recordset
    Dim RsSaleDetail As ADODB.Recordset ''Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim SqlStr As String = ""
    Dim mCustomerCode As String
    Dim mBookType As String
    Dim mBookSubType As String

    'Private Const mBookType = "P"
    'Private Const mBookSubType = "I"
    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColHSNCode As Short = 2
    Private Const ColPartNo As Short = 3
    Private Const ColItemDesc As Short = 4
    Private Const ColUnit As Short = 5
    Private Const ColQty As Short = 6
    Private Const ColRate As Short = 7
    Private Const ColAmount As Short = 8
    Private Const ColCGSTPer As Short = 9
    Private Const ColSGSTPer As Short = 10
    Private Const ColIGSTPer As Short = 11
    Private Const ColTariff As Short = 12

    Private Sub chkClosed_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkClosed.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkOpening_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOpening.CheckStateChanged
        If chkOpening.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtMRRNo.Enabled = False
            txtMRRDate.Enabled = False
            CmdSearchMRR.Enabled = False
            txtCustomer.Enabled = True
            chkRejection.Enabled = True
        Else
            txtMRRNo.Enabled = True
            txtMRRDate.Enabled = True
            CmdSearchMRR.Enabled = True
            txtCustomer.Enabled = False
            chkRejection.Enabled = False
        End If
        Call FormatSprdMain(-1)
    End Sub

    Private Sub chkRejection_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRejection.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True

            txtVNo.Enabled = False
            txtMRRNo.Enabled = True
            CmdSearchMRR.Enabled = True
        Else
            cmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click

        On Error GoTo DelErrPart

        Dim xDCNo As String
        Dim RsTemp As ADODB.Recordset = Nothing

        If ValidateBranchLocking((txtVDate.Text)) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, txtVDate.Text, (txtCustomer.Text), mCustomerCode) = True Then
            Exit Sub
        End If

        If Trim(txtVNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If PubSuperUser <> "S" Then

            SqlStr = "SELECT *  From DSP_PAINT57F4_TRN " & vbCrLf & " Where " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustomerCode) & " '" & vbCrLf & " AND PARTY_F4NO='" & Txt57F4No.Text & "'" & vbCrLf & " AND BOOKTYPE='D' AND BOOKSUBTYPE='O' AND ISSCRAP='N'"

            If chkOpening.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & " AND BILL_NO='" & Trim(txtBillNo.Text) & "'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                MsgBox("Transaction is  made Cann't Deleted.", MsgBoxStyle.Information)
                Exit Sub
            End If
        End If


        If Not RsSaleMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "DSP_PAINT57F4_HDR", (txtVNo.Text), RsSaleMain, "VNo") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "DSP_PAINT57F4_HDR", "MKEY", (LblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM DSP_PAINT57F4_TRN WHERE MKey='" & LblMKey.Text & "' AND BookType='" & mBookType & "' AND BookSubType='" & mBookSubType & "' AND TRNTYPE='O'")

                PubDBCn.Execute("Delete from DSP_PAINT57F4_DET Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("Delete from DSP_PAINT57F4_HDR Where Mkey='" & LblMKey.Text & "'")


                PubDBCn.CommitTrans()
                RsSaleMain.Requery() ''.Refresh
                RsSaleDetail.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsSaleMain.Requery() ''.Refresh
        RsSaleDetail.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtMRRNo.Enabled = False
            CmdSearchMRR.Enabled = False
            txtMRRDate.Enabled = False
            '        txtVNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
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

        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            TxtVNo_Validating(TxtVNo, New System.ComponentModel.CancelEventArgs(False))
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

    Private Sub CmdSearchMRR_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchMRR.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MRR_FINAL_FLAG='N'"

        If MainClass.SearchGridMaster((txtMRRNo.Text), "INV_GATE_HDR", "AUTO_KEY_MRR", "MRR_DATE", , , SqlStr) = True Then
            txtMRRNo.Text = AcName
            txtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
            If txtMRRNo.Enabled = True Then txtMRRNo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Txt57F4Date_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles Txt57F4Date.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(Txt57F4Date.Text) = "" Then GoTo EventExitSub
        If Not IsDate(Txt57F4Date.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub Txt57F4No_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles Txt57F4No.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim m57No As String
        Dim mCustCode As String
        If Trim(Txt57F4No.Text) = "" Then GoTo EventExitSub

        If Trim(txtCustomer.Text) = "" Then
            MsgInformation("Please Select Customer First.")
            GoTo EventExitSub
        Else
            If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCustCode = MasterNo
            Else
                MsgInformation("Invaild Customer.")
                GoTo EventExitSub
            End If
        End If

        If MODIFYMode = True And RsSaleMain.EOF = False Then xMKey = RsSaleMain.Fields("mKey").Value
        m57No = Trim(Txt57F4No.Text)

        SqlStr = " SELECT * FROM DSP_PAINT57F4_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustCode) & "' " & vbCrLf & " AND PARTY_F4NO='" & MainClass.AllowSingleQuote(m57No) & "' " & vbCrLf & " AND BookType='" & mBookType & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsSaleMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Voucher, Use Generate Voucher Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM DSP_PAINT57F4_HDR " & " WHERE Mkey='" & MainClass.AllowSingleQuote(xMKey) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCustomer_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.DoubleClick
        Call SearchSupplierName()
    End Sub

    Private Sub txtCustomer_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomer.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchSupplierName()
    End Sub

    Private Sub txtCustomer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim pAccountCode As String

        If Trim(txtCustomer.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Supplier Name")
            Cancel = True
        Else
            pAccountCode = MasterNo
        End If

        If ADDMode = True Then
            If MsgQuestion("Populate Data From Customer Detail ...") = CStr(MsgBoxResult.Yes) Then
                Call FillItemFromSuppCustDetail(pAccountCode)
            End If
            Txt57F4No.Focus()
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDespatchDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDespatchDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDespatchDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDespatchDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FillItemFromSuppCustDetail(ByRef xAcctCode As String)

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim i As Integer

        MainClass.ClearGrid(SprdMain, ConRowHeight)

        SqlStr = " SELECT  ID.ITEM_CODE,  INVMST.PURCHASE_UOM, INVMST.ITEM_SHORT_DESC, INVMST.HSN_CODE, " & vbCrLf & " ID.ITEM_RATE,  ID.DISC_PER,INVMST.CUSTOMER_PART_NO,INVMST.ITEM_COLOR,INVMST.TARIFF_CODE " & vbCrLf & " FROM FIN_SUPP_CUST_HDR IH, FIN_SUPP_CUST_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=ID.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'" & vbCrLf & " AND TRN_TYPE ='J' ORDER BY ID.ITEM_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        i = 1
        If RsTemp.EOF = False Then
            With SprdMain
                Do While Not RsTemp.EOF
                    .Row = i
                    .Col = ColItemCode
                    .Text = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                    .Col = ColItemDesc
                    .Text = IIf(IsDbNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)

                    .Col = ColHSNCode
                    .Text = IIf(IsDBNull(RsTemp.Fields("HSN_CODE").Value), "", RsTemp.Fields("HSN_CODE").Value)

                    .Col = ColUnit
                    .Text = IIf(IsDbNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)

                    .Col = ColPartNo
                    .Text = IIf(IsDbNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value)


                    .Col = ColRate
                    .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("ITEM_RATE").Value), "", RsTemp.Fields("ITEM_RATE").Value)))

                    .Col = ColTariff
                    .Text = IIf(IsDbNull(RsTemp.Fields("TARIFF_CODE").Value), "", RsTemp.Fields("TARIFF_CODE").Value)

                    i = i + 1
                    .MaxRows = i
                    RsTemp.MoveNext()
                Loop
            End With
        End If
        FormatSprdMain(-1)
        Call CalcTots()

        Exit Sub

ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchSupplierName()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((txtCustomer.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtCustomer.Text = AcName
            txtCustomer_Validating(txtCustomer, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtDuration_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDuration.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDuration.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDuration.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtIssueDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIssueDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtIssueDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtIssueDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMRRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtMRRDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtMRRDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMRRNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(txtMRRNo.Text) = "" Then GoTo EventExitSub
        If Len(txtMRRNo.Text) < 6 Then
            txtMRRNo.Text = VB6.Format(Val(txtMRRNo.Text), "00000") & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If txtMRRNo.Enabled = False Then GoTo EventExitSub

        SqlStr = " SELECT * FROM DSP_PAINT57F4_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsSaleMain.EOF = False Then
            Clear1()
            Call Show1()
            GoTo EventExitSub
        Else

            SqlStr = " SELECT * FROM INV_GATE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                Clear1()
                If ShowFromMRRMain(RsTemp) = False Then
                    Cancel = True
                    GoTo EventExitSub
                End If
            Else
                ErrorMsg("Please Enter Vaild MRR No.", "", MsgBoxStyle.Critical)
                Cancel = True
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FrmPaint57F4_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.hide()
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""



        If eventArgs.Row = 0 And eventArgs.Col = ColItemCode And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                SqlStr = GetSearchItem("Y")
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName)
                    .Col = ColItemDesc
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColItemDesc And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemDesc
                SqlStr = GetSearchItem("N")
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = Trim(AcName)
                    .Col = ColItemCode
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColItemDesc)
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
        Call CalcTots()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim RIBBONSGroup As Boolean
        Dim xSoNo As String
        Dim xICode As String

        If eventArgs.NewRow = -1 Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                If GetValidItem(xICode) = True Then
                    If CheckDuplicateItem(xICode) = False Then
                        If FillGridRow(xICode) = False Then Exit Sub
                        '                    FormatSprdMain Row
                        '                MainClass.SetFocusToCell SprdMain, Row, ColItemRate
                    End If
                Else
                    '                MainClass.SetFocusToCell SprdMain, Row, ColItemCode
                End If

            Case ColQty
                If CheckQty() = True Then
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
            Case ColRate
                Call CheckRate()
        End Select
        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function GetValidItem(ByRef pItemCode As String) As Boolean
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(txtCustomer.Text) = "" Then
            MsgInformation("Please Select Customer First.")
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xSuppCode = MasterNo
            Else
                MsgInformation("Invaild Customer.")
                Exit Function
            End If
        End If

        '    mSqlStr = "SELECT B.ITEM_CODE " & vbCrLf _
        ''            & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf _
        ''            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
        ''            & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf _
        ''            & " AND B.SUPP_CUST_CODE='" & xSuppCode & "'" & vbCrLf _
        ''            & " AND B.ITEM_CODE LIKE '" & pItemCode & "%'"
        '    MainClass.UOpenRecordSet mSqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '    If RsTemp.EOF = False Then
        '        GetValidItem = True
        '    Else
        '        MsgInformation "Please Check Item."
        '        GetValidItem = True
        '    End If
        GetValidItem = True
        Exit Function
ErrPart:
        GetValidItem = False
    End Function
    Private Function CheckDuplicateItem(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemRept As Integer

        If mItemCode = "" Then CheckDuplicateItem = True : Exit Function
        With SprdMain
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColItemCode
                If UCase(.Text) = UCase(mItemCode) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Item Code")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FillGridRow(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim xSuppCode As String

        If mItemCode = "" Then Exit Function

        If Trim(txtCustomer.Text) = "" Then
            MsgInformation("Please Select Customer First.")
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xSuppCode = MasterNo
            Else
                MsgInformation("Invaild Customer.")
                Exit Function
            End If
        End If

        SqlStr = ""
        SqlStr = " Select INVMST.ITEM_CODE,INVMST.ITEM_SHORT_DESC,INVMST.PURCHASE_UOM, HSN_CODE, " & vbCrLf _
            & " CUSTOMER_PART_NO,ITEM_COLOR " & vbCrLf _
            & " FROM  INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND INVMST.ITEM_CODE='" & Trim(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc

                SprdMain.Col = ColItemDesc
                SprdMain.Text = IIf(IsDbNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)

                SprdMain.Col = ColPartNo
                SprdMain.Text = IIf(IsDbNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)

                SprdMain.Col = ColHSNCode
                SprdMain.Text = IIf(IsDBNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value)

                MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                FormatSprdMain(-1)

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

    Private Function GetSearchItem(ByRef mByCode As String) As String
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String

        If Trim(txtCustomer.Text) = "" Then
            MsgInformation("Please Select Customer First.")
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xSuppCode = MasterNo
            Else
                MsgInformation("Invaild Customer.")
                Exit Function
            End If
        End If

        If mByCode = "Y" Then
            mSqlStr = "SELECT B.ITEM_CODE,A.ITEM_SHORT_DESC "
        Else
            mSqlStr = "SELECT A.ITEM_SHORT_DESC,B.ITEM_CODE "
        End If

        mSqlStr = mSqlStr & vbCrLf & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf & " AND B.SUPP_CUST_CODE='" & xSuppCode & "'"
        GetSearchItem = mSqlStr
        Exit Function
ErrPart:
        GetSearchItem = ""

    End Function
    Private Sub CheckRate()

        On Error GoTo ERR1
        With SprdMain

            Exit Sub

            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Sub

            .Col = ColRate
            If Val(.Text) <= 0 Then
                MsgInformation("Please Enter the Rate.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColRate)
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function CheckQty() As Boolean

        On Error GoTo ERR1

        If chkOpening.CheckState = System.Windows.Forms.CheckState.Checked Then
            CheckQty = True
            Exit Function
        End If

        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColQty
            If Val(.Text) > 0 Then
                CheckQty = True
            Else
                MsgInformation("Please Check Qty.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColQty)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.Row

            .Col = 3
            txtVNo.Text = VB6.Format(.Text, "00000")

            TxtVNo_Validating(TxtVNo, New System.ComponentModel.CancelEventArgs(False))
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub

    Private Sub Txt57F4Date_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Txt57F4Date.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub Txt57F4No_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Txt57F4No.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub Txt57F4No_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles Txt57F4No.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomer.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDespatchDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDespatchDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDespatchNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDespatchNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDespatchNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDespatchNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomer.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDuration_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDuration.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIssueDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIssueDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMRRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMRRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMRRNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.DoubleClick
        CmdSearchMRR_Click(CmdSearchMRR, New System.EventArgs())
    End Sub


    Private Sub txtMRRNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMRRNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then CmdSearchMRR_Click(CmdSearchMRR, New System.EventArgs())
    End Sub

    Private Sub txtNature_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNature.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNature_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNature.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomer.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtVDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtVDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Public Sub TxtVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mVNO As String
        If Trim(txtVNo.Text) = "" Then GoTo EventExitSub

        txtVNo.Text = VB6.Format(Val(txtVNo.Text), "00000")

        If MODIFYMode = True And RsSaleMain.EOF = False Then xMKey = RsSaleMain.Fields("mKey").Value
        mVNO = Trim(txtVNo.Text)

        SqlStr = " SELECT * FROM DSP_PAINT57F4_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND VNo='" & MainClass.AllowSingleQuote(mVNO) & "' " & vbCrLf & " AND BookType='" & mBookType & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsSaleMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Voucher, Use Generate Voucher Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM DSP_PAINT57F4_HDR " & " WHERE Mkey='" & MainClass.AllowSingleQuote(xMKey) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        Dim i As Short
        Dim nMkey As String
        Dim mTRNType As String
        Dim mAutoKeyNo As Double
        Dim mBillNoSeq As Integer
        Dim mBillNo As String
        Dim mSuppCustCode As String
        Dim mAccountCode As String
        Dim mNETVALUE As Double
        Dim mTotQty As Double
        Dim mStatus As String
        Dim mREJECTION As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        mSuppCustCode = "-1"
        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = "-1"
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mNETVALUE = Val(lblTotItemValue.Text)
        mTotQty = Val(lblTotQty.Text)
        mStatus = IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O")
        mREJECTION = IIf(chkRejection.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If Trim(txtVNo.Text) = "" Then
            mBillNoSeq = CInt(AutoGenSeqBillNo(mBookType, mBookSubType))
        Else
            mBillNoSeq = Val(txtVNo.Text)
        End If

        txtVNo.Text = VB6.Format(Val(CStr(mBillNoSeq)), "00000")

        If CheckValidBillDate(mBillNoSeq) = False Then GoTo ErrPart

        mBillNo = CStr(Val(CStr(mBillNoSeq)))

        SqlStr = ""
        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("DSP_PAINT57F4_HDR", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo
            LblMKey.Text = nMkey

            SqlStr = "INSERT INTO DSP_PAINT57F4_HDR (" & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf _
                & " ROWNO, BookType, BOOKSUBTYPE, " & vbCrLf _
                & " AUTO_KEY_MRR,  MRR_DATE, VNO, " & vbCrLf _
                & " VDATE,  SUPP_CUST_CODE,  BILL_NO, " & vbCrLf _
                & " BILL_DATE,  PARTY_F4NO,  PARTY_F4DATE, " & vbCrLf _
                & " ISSUE_DATE,  NATURE, EXPECTED_DATE, " & vbCrLf _
                & " DESPATCH_NO, DESPATCH_DATE, NETVALUE, " & vbCrLf _
                & " TOTQTY, STATUS, ADDUSER, ADDDATE, " & vbCrLf _
                & " MODUSER, MODDATE,ISREJECTION )"

            SqlStr = SqlStr & vbCrLf _
                & " VALUES('" & nMkey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                & " " & mCurRowNo & ", '" & mBookType & "', '" & mBookSubType & "'," & vbCrLf _
                & " " & Val(txtMRRNo.Text) & ", TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtVNo.Text) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSuppCustCode) & "', '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & Txt57F4No.Text & "', TO_DATE('" & VB6.Format(Txt57F4Date.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtIssueDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtNature.Text) & "',TO_DATE('" & VB6.Format(txtDuration.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDespatchNo.Text) & "', TO_DATE('" & VB6.Format(txtDespatchDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " " & mNETVALUE & ", " & mTotQty & ", '" & mStatus & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','" & mREJECTION & "')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE DSP_PAINT57F4_HDR SET " & vbCrLf _
                & " BOOKTYPE= '" & mBookType & "'," & vbCrLf _
                & " BOOKSUBTYPE= '" & mBookSubType & "'," & vbCrLf _
                & " AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ", " & vbCrLf _
                & " MRR_DATE=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " VNO='" & MainClass.AllowSingleQuote(txtVNo.Text) & "', " & vbCrLf _
                & " VDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "', " & vbCrLf _
                & " BILL_NO='" & MainClass.AllowSingleQuote(txtBillNo.Text) & "', " & vbCrLf _
                & " BILL_DATE=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " PARTY_F4NO='" & Trim(Txt57F4No.Text) & "', " & vbCrLf _
                & " PARTY_F4DATE=TO_DATE('" & VB6.Format(Txt57F4Date.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " ISSUE_DATE=TO_DATE('" & VB6.Format(txtIssueDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " NATURE='" & MainClass.AllowSingleQuote(txtNature.Text) & "', " & vbCrLf _
                & " EXPECTED_DATE=TO_DATE('" & VB6.Format(txtDuration.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " DESPATCH_NO='" & MainClass.AllowSingleQuote(txtDespatchNo.Text) & "', " & vbCrLf _
                & " DESPATCH_DATE=TO_DATE('" & VB6.Format(txtDespatchDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " STATUS='" & mStatus & "'," & vbCrLf _
                & " NETVALUE=" & mNETVALUE & ", " & vbCrLf _
                & " TOTQTY=" & mTotQty & ", " & vbCrLf _
                & " ISREJECTION='" & mREJECTION & "'," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1() = False Then GoTo ErrPart
        UpdateMain1 = True

        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsSaleMain.Requery() ''.Refresh
        RsSaleDetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume
    End Function
    Private Function CheckValidBillDate(ByRef pBillNoSeq As Integer) As Object

        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim mRsCheck1 As ADODB.Recordset
        Dim mRsCheck2 As ADODB.Recordset
        Dim mBackBillDate As String
        Dim mMaxInvStrfNo As Integer
        CheckValidBillDate = True

        If txtVNo.Text = "000001" Then Exit Function

        SqlStr = "SELECT MAX(VDATE)" & vbCrLf & " FROM DSP_PAINT57F4_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BookType='" & mBookType & "' " & vbCrLf & " AND BookSubType='" & mBookSubType & "' " & vbCrLf & " AND VNO<" & Val(CStr(pBillNoSeq)) & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDbNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If

        SqlStr = "SELECT MIN(VDATE)" & " FROM DSP_PAINT57F4_HDR " & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BookType='" & mBookType & "' " & vbCrLf & " AND BookSubType='" & mBookSubType & "' " & vbCrLf & " AND VNO>" & Val(CStr(pBillNoSeq)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck1, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck1.EOF = False And Not IsDbNull(mRsCheck1.Fields(0).Value) And mRsCheck2.EOF = False And Not IsDbNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtVDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Voucher Date Is Greater Than The Voucher Date Of Next Voucher No.")
                CheckValidBillDate = False
            ElseIf CDate(txtVDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Voucher Date Is Less Than The Voucher Date Of Previous Voucher No.")
                CheckValidBillDate = False
            End If
        ElseIf mRsCheck1.EOF = False And Not IsDbNull(mRsCheck1.Fields(0).Value) Then
            If CDate(txtVDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Voucher Date Is Greater Than The Voucher Date Of Next Voucher No.")
                CheckValidBillDate = False
            End If
        ElseIf mRsCheck2.EOF = False And Not IsDbNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtVDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Voucher Date Is Less Than The Voucher Date Of Previous Voucher No.")
                CheckValidBillDate = False
            End If
        End If

        Exit Function
CheckERR:
        CheckValidBillDate = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function AutoGenSeqBillNo(ByRef mBookType As String, ByRef mBookSubType As String) As String

        On Error GoTo AutoGenSeqBillNoErr
        Dim RsSaleMainGen As ADODB.Recordset
        Dim mNewSeqBillNo As Integer
        SqlStr = ""


        SqlStr = "SELECT Max(VNO)  FROM DSP_PAINT57F4_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BookType='" & mBookType & "' AND BookSubType='" & mBookSubType & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSaleMainGen
            If .EOF = False Then
                If Not IsDbNull(.Fields(0).Value) Then
                    mNewSeqBillNo = .Fields(0).Value + 1
                Else
                    mNewSeqBillNo = 1
                End If
            Else
                mNewSeqBillNo = 1
            End If
        End With
        AutoGenSeqBillNo = CStr(mNewSeqBillNo)
        Exit Function
AutoGenSeqBillNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim i As Integer
        Dim mItemCode As String
        Dim mTariff As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mAmount As Double



        PubDBCn.Execute("Delete From DSP_PAINT57F4_DET Where Mkey='" & LblMKey.Text & "'")
        PubDBCn.Execute("Delete From DSP_PAINT57F4_TRN Where Mkey='" & LblMKey.Text & "'" & vbCrLf & " AND BOOKTYPE='" & mBookType & "' AND BOOKSUBTYPE='" & mBookSubType & "' AND TRNTYPE='O'")


        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColAmount
                mAmount = Val(.Text)

                .Col = ColTariff
                mTariff = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mItemCode <> "" And mQty <> 0 Then
                    SqlStr = " INSERT INTO DSP_PAINT57F4_DET ( " & vbCrLf & " MKEY , SUBROWNO, " & vbCrLf & " ITEM_CODE , ITEM_QTY, " & vbCrLf & " ITEM_UOM , ITEM_RATE, ITEM_AMT, " & vbCrLf & " ITEM_TARIFF, COMPANY_CODE) "
                    SqlStr = SqlStr & vbCrLf & " VALUES ('" & LblMKey.Text & "', " & i & ", " & vbCrLf & " '" & mItemCode & "'," & mQty & ", " & vbCrLf & " '" & mUnit & "'," & mRate & "," & mAmount & ", " & vbCrLf & " '" & mTariff & "'," & RsCompany.Fields("COMPANY_CODE").Value & " ) "

                    PubDBCn.Execute(SqlStr)

                    If UpdatePaintDetail(PubDBCn, (LblMKey.Text), mBookType, mBookSubType, mCustomerCode, Trim(Txt57F4No.Text), Txt57F4Date.Text, (txtBillNo.Text), (txtBillDate.Text), mItemCode, mQty, "I", i, "O", (txtVDate.Text)) = False Then GoTo UpdateDetail1
                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim xCustomerCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double

        FieldsVarification = True
        If ValidateBranchLocking((txtVDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateAccountLocking(PubDBCn, txtVDate.Text, (txtCustomer.Text), mCustomerCode) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsSaleMain.EOF = True Then Exit Function

        If MODIFYMode = True And txtVNo.Text = "" Then
            MsgInformation("Voucher No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If chkOpening.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtMRRNo.Text = "-1"
            txtMRRDate.Text = VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")
            txtBillNo.Text = "OP"
            txtBillDate.Text = VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")
        Else
            If txtMRRNo.Text = "" Then
                MsgBox("MRR No is Blank", MsgBoxStyle.Information)
                FieldsVarification = False
                txtMRRNo.Focus()
                Exit Function
            End If
            If txtMRRDate.Text = "" Then
                MsgBox("MRR Date is Blank", MsgBoxStyle.Information)
                FieldsVarification = False
                txtMRRDate.Focus()
                Exit Function
                '    ElseIf FYChk(txtMRRDate.Text) = False Then
                '        FieldsVarification = False
                '        txtMRRDate.SetFocus
                '        Exit Function
            End If
        End If
        If txtVDate.Text = "" Then
            MsgBox("Voucher Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtVDate.Focus()
            Exit Function
        ElseIf FYChk((txtVDate.Text)) = False Then
            FieldsVarification = False
            txtVDate.Focus()
            Exit Function
        End If
        If CDate(txtVDate.Text) < CDate(txtMRRDate.Text) Then
            MsgBox("Voucher Date Can Not be Less Than MRR Date.")
            FieldsVarification = False
            txtVDate.Focus()
            Exit Function
        End If

        If Trim(txtCustomer.Text) = "" Then
            MsgBox("Customer Cannot Be Blank", MsgBoxStyle.Information)
            ' txtCustomer.SetFocus
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            'txtCustomer.SetFocus
            FieldsVarification = False
            Exit Function
        Else
            xCustomerCode = Trim(MasterNo)
        End If

        If Txt57F4No.Text = "" Then
            MsgBox("Party 57F4 No is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            Txt57F4No.Focus()
            Exit Function
        End If
        If Txt57F4Date.Text = "" Then
            MsgBox("Party 57F4 Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            Txt57F4Date.Focus()
            Exit Function
        End If

        If Not IsDate(Txt57F4Date.Text) Then
            MsgBox("Invalid Party 57F4 Date", MsgBoxStyle.Information)
            FieldsVarification = False
            Txt57F4Date.Focus()
            Exit Function
        End If

        If txtIssueDate.Text = "" Then
            MsgBox("Issue Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtIssueDate.Focus()
            Exit Function
        End If

        If Not IsDate(txtIssueDate.Text) Then
            MsgBox("Invalid Issue Date", MsgBoxStyle.Information)
            FieldsVarification = False
            txtIssueDate.Focus()
            Exit Function
        End If

        If txtDuration.Text = "" Then
            MsgBox("Expected Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDuration.Focus()
            Exit Function
        End If

        If Not IsDate(txtDuration.Text) Then
            MsgBox("Invalid Expected Date", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDuration.Focus()
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function

        If chkOpening.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False : Exit Function
        End If

        SqlStr = "SELECT *  From DSP_PAINT57F4_TRN " & vbCrLf & " Where " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xCustomerCode) & " '" & vbCrLf & " AND PARTY_F4NO='" & Txt57F4No.Text & "'" & vbCrLf & " AND BOOKTYPE='D' AND BOOKSUBTYPE='O' AND ISSCRAP='N'"

        If chkOpening.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND BILL_NO='" & Trim(txtBillNo.Text) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If PubSuperUser = "S" Then ''PubATHUSER = False
                If MsgQuestion("Transaction had Made Against This 57F4. Are You want to Continue...") = CStr(MsgBoxResult.No) Then
                    FieldsVarification = False
                    Exit Function
                End If
            Else
                MsgBox("Transaction had Made Against This 57F4.So Cann't modify.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        ''Check Back F4 Entered.....
        SqlStr = " SELECT DISTINCT PARTY_F4NO  From DSP_PAINT57F4_TRN " & vbCrLf & " Where " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xCustomerCode) & " '" & vbCrLf & " AND PARTY_F4DATE >TO_DATE('" & VB6.Format(Txt57F4Date.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND BOOKTYPE='D' AND BOOKSUBTYPE='O' AND ISSCRAP='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If PubSuperUser = "S" Then ''PubATHUSER = False
                If MsgQuestion("Back 57F4 Date Cann't be Allow. Are You want to Continue...") = CStr(MsgBoxResult.No) Then
                    FieldsVarification = False
                    Exit Function
                End If
            Else
                MsgBox("Back 57F4 Date Cann't be Allow. So Cann't Add Or Modify.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColItemCode

                mItemCode = Trim(.Text)

                .Col = ColCGSTPer
                mCGSTPer = Val(.Text)

                .Col = ColSGSTPer
                mSGSTPer = Val(.Text)

                .Col = ColIGSTPer
                mIGSTPer = Val(.Text)

                If Trim(mItemCode) <> "" Then
                    If mCGSTPer + mSGSTPer + mIGSTPer = 0 Then
                        MsgBox("Please check the GST Rate.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If

            Next
        End With


        If MainClass.ValidDataInGrid(SprdMain, ColHSNCode, "N", "Please HSN Code.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColRate, "N", "Please Check Rate.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColAmount, "N", "Please Check Amount.") = False Then FieldsVarification = False : Exit Function
        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmPaint57F4_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblBookType.Text = "P" Then
            Me.Text = "Paint 57F4"
        Else
            Me.Text = "Jobwork 57F4"
        End If

        mBookType = lblBookType.Text
        mBookSubType = lblBookSubType.Text


        SqlStr = ""
        SqlStr = "Select * from DSP_PAINT57F4_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from DSP_PAINT57F4_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        mCustomerCode = CStr(-1)

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
        SqlStr = ""

        SqlStr = "SELECT " & vbCrLf _
            & " AUTO_KEY_MRR AS MRRNO, MRR_DATE, VNO, VDATE, " & vbCrLf _
            & " SUPP_CUST_NAME AS CUSTOMER_NAME, BILL_NO, BILL_DATE, " & vbCrLf _
            & " PARTY_F4NO AS F4_NO, PARTY_F4DATE AS F4_DATE, ISSUE_DATE, " & vbCrLf _
            & " NATURE, EXPECTED_DATE, DESPATCH_NO, " & vbCrLf _
            & " DESPATCH_DATE, NETVALUE, TOTQTY " & vbCrLf _
            & " FROM " & vbCrLf _
            & " DSP_PAINT57F4_HDR, FIN_SUPP_CUST_MST A " & vbCrLf _
            & " WHERE DSP_PAINT57F4_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " And DSP_PAINT57F4_HDR.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND DSP_PAINT57F4_HDR.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf _
            & " AND DSP_PAINT57F4_HDR.SUPP_CUST_CODE=A.SUPP_CUST_CODE AND DSP_PAINT57F4_HDR.BOOKTYPE='" & lblBookType.Text & "'"

        SqlStr = SqlStr & vbCrLf & " Order by VDATE,VNo"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 600)

            .set_ColWidth(0, 600)
            .set_ColWidth(1, 1200)
            .set_ColWidth(2, 1200)
            .set_ColWidth(3, 1200)
            .set_ColWidth(4, 1200)
            .set_ColWidth(5, 3000)
            .set_ColWidth(6, 1200)
            .set_ColWidth(7, 1200)
            .set_ColWidth(8, 1200)
            .set_ColWidth(9, 1200)
            .set_ColWidth(10, 1200)
            .set_ColWidth(11, 2000)
            .set_ColWidth(12, 2000)
            .set_ColWidth(13, 2000)
            .set_ColWidth(14, 2000)
            .set_ColWidth(15, 1200)
            .set_ColWidth(16, 1200)
            .Col = 15
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .Col = 16
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsSaleDetail.Fields("ITEM_CODE").DefinedSize ''

            .Col = ColHSNCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("HSN_CODE", "GEN_HSN_MST", PubDBCn) ''

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn)
            .ColsFrozen = ColPartNo

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)

            .Col = ColTariff
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsSaleDetail.Fields("ITEM_TARIFF").DefinedSize

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsSaleDetail.Fields("ITEM_UOM").DefinedSize

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")

            .Col = ColCGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")

            .Col = ColSGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")

            .Col = ColIGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")

        End With

        If chkOpening.CheckState = System.Windows.Forms.CheckState.Checked Then
            MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColItemCode)
            MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColQty, ColQty)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPartNo, ColUnit)
        Else
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColQty, ColQty)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColUnit)
        End If

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmount, ColIGSTPer)
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsSaleDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsSaleMain
            txtMRRNo.Maxlength = .Fields("AUTO_KEY_MRR").Precision
            txtMRRDate.Maxlength = 10
            txtCustomer.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtVNo.Maxlength = .Fields("VNO").Precision
            txtVDate.Maxlength = 10
            txtBillNo.Maxlength = .Fields("BILL_NO").DefinedSize
            txtBillDate.Maxlength = 10
            Txt57F4No.Maxlength = .Fields("Party_F4No").DefinedSize
            Txt57F4Date.Maxlength = 10
            txtIssueDate.Maxlength = 10
            txtNature.Maxlength = .Fields("NATURE").DefinedSize
            txtDuration.Maxlength = 10
            txtDespatchNo.Maxlength = .Fields("DESPATCH_NO").DefinedSize
            txtDespatchDate.Maxlength = 10

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mCustomerCode As String

        With RsSaleMain
            If Not .EOF Then
                If lblBookType.Text <> .Fields("BOOKTYPE").Value Then
                    Clear1()
                    MsgInformation("This MRR already Used.")
                    Exit Sub
                End If
                txtMRRNo.Enabled = False
                LblMKey.Text = .Fields("MKey").Value
                txtMRRNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                txtMRRDate.Text = IIf(IsDbNull(.Fields("MRR_DATE").Value), "", .Fields("MRR_DATE").Value)
                txtVNo.Text = VB6.Format(IIf(IsDbNull(.Fields("VNO").Value), "", .Fields("VNO").Value), "00000")
                txtVDate.Text = VB6.Format(IIf(IsDbNull(.Fields("VDATE").Value), "", .Fields("VDATE").Value), "DD/MM/YYYY")

                If MainClass.ValidateWithMasterTable((.Fields("AUTO_KEY_MRR").Value), "AUTO_KEY_MRR", "SUPP_CUST_CODE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCustomerCode = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCustomer.Text = MasterNo
                End If

                txtBillNo.Text = IIf(IsDbNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                txtBillDate.Text = IIf(IsDbNull(.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value)

                If txtBillNo.Text = "OP" Then
                    chkOpening.CheckState = System.Windows.Forms.CheckState.Checked
                    chkOpening.Enabled = False
                    mCustomerCode = IIf(IsDbNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)
                    If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtCustomer.Text = MasterNo
                    End If

                End If

                chkClosed.CheckState = IIf(.Fields("STATUS").Value = "C", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkRejection.CheckState = IIf(.Fields("ISREJECTION").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                Txt57F4No.Text = IIf(IsDbNull(.Fields("PARTY_F4NO").Value), "", .Fields("PARTY_F4NO").Value)
                Txt57F4Date.Text = IIf(IsDbNull(.Fields("PARTY_F4DATE").Value), "", .Fields("PARTY_F4DATE").Value)
                txtIssueDate.Text = IIf(IsDbNull(.Fields("ISSUE_DATE").Value), "", .Fields("ISSUE_DATE").Value)
                txtNature.Text = IIf(IsDbNull(.Fields("NATURE").Value), "", .Fields("NATURE").Value)
                txtDuration.Text = IIf(IsDbNull(.Fields("EXPECTED_DATE").Value), "", .Fields("EXPECTED_DATE").Value)
                txtDespatchNo.Text = IIf(IsDbNull(.Fields("DESPATCH_NO").Value), "", .Fields("DESPATCH_NO").Value)
                txtDespatchDate.Text = IIf(IsDbNull(.Fields("DESPATCH_DATE").Value), "", .Fields("DESPATCH_DATE").Value)

                Call ShowDetail1()
                Call CalcTots()
            End If
        End With
        txtVNo.Enabled = True
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        FormatSprdMain(-1)

        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        CmdSearchMRR.Enabled = False
        txtMRRDate.Enabled = False
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String

        Dim mHSNCode As String
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double

        Dim mLocal As String
        Dim mPartyGSTNo As String

        mLocal = "N"
        If Trim(txtCustomer.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLocal = Trim(MasterNo)
            End If
        End If

        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If

        SqlStr = ""
        SqlStr = " SELECT * FROM DSP_PAINT57F4_DET " & vbCrLf & " Where Mkey='" & LblMKey.Text & "'" & vbCrLf & " Order By SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSaleDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            i = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = i

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = mItemCode

                mHSNCode = GetHSNCode(mItemCode)

                SprdMain.Col = ColHSNCode
                SprdMain.Text = mHSNCode

                If GetHSNDetails(mHSNCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, "G", mPartyGSTNo) = False Then GoTo ERR1

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColPartNo
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "CUSTOMER_PART_NO", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mPartNo = MasterNo
                SprdMain.Text = mPartNo

                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)))

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))

                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_AMT").Value), 0, .Fields("ITEM_AMT").Value)))

                SprdMain.Col = ColTariff
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_TARIFF").Value), "", .Fields("ITEM_TARIFF").Value)

                SprdMain.Col = ColCGSTPer
                SprdMain.Text = VB6.Format(mCGSTPer, "0.00")

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = VB6.Format(mSGSTPer, "0.00")

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = VB6.Format(mIGSTPer, "0.00")

                .MoveNext()

                i = i + 1
                SprdMain.MaxRows = i
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
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        AdoDCMain.Refresh
            FormatSprdView()
            SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mQty As Double
        Dim mRate As Double
        Dim mItemAmount As Double
        Dim mItemValue As Double
        Dim mTotQty As Double
        Dim mTotItemAmount As Double
        Dim i As Integer
        Dim j As Integer
        Dim mItemCode As String
        Dim mHSNCode As String
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        'Dim mLocal As String
        'Dim mPartyGSTNo As String
        Dim mTariff As String
        Dim mTariffDesc As String
        Dim mLocal As String
        Dim mPartyGSTNo As String

        mLocal = "N"
        If Trim(txtCustomer.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLocal = Trim(MasterNo)
            End If
        End If

        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If

        mQty = 0
        mRate = 0
        mItemAmount = 0
        mTotItemAmount = 0
        mItemValue = 0

        With SprdMain
            j = .MaxRows
            For i = 1 To j
                .Row = i
                .Col = 0
                If .Text = "Del" Then GoTo DontCalc

                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc
                mItemCode = .Text

                .Col = ColHSNCode
                mHSNCode = .Text

                If mHSNCode = "" Then
                    mHSNCode = GetHSNCode(mItemCode)
                End If

                If GetHSNDetails(mHSNCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, "G", mPartyGSTNo) = False Then GoTo ERR1


                .Col = ColQty
                mQty = Val(.Text)

                mTotQty = mTotQty + mQty

                .Col = ColCGSTPer
                .Text = VB6.Format(mCGSTPer, "0.00")

                .Col = ColSGSTPer
                .Text = VB6.Format(mSGSTPer, "0.00")

                .Col = ColIGSTPer
                .Text = VB6.Format(mIGSTPer, "0.00")

                If GetTariffHeading(mItemCode, mTariff, mTariffDesc) = True Then
                    .Col = ColTariff
                    .Text = mTariff
                End If

                .Col = ColRate
                mRate = Val(.Text)
                .Text = CStr(mRate)

                .Col = ColAmount
                .Text = VB6.Format(mQty * mRate, "0.00")

                mItemAmount = CDbl(VB6.Format(mQty * mRate, "0.00")) '- mDiscount
                mTotItemAmount = mTotItemAmount + mItemAmount

                mItemValue = CDbl(VB6.Format(mQty * mRate, "0.00"))

DontCalc:
            Next i
        End With


        lblTotItemValue.Text = VB6.Format(mTotItemAmount, "#0.00")
        lblTotQty.Text = VB6.Format(mTotQty, "#0.00")
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub Clear1()

        LblMKey.Text = ""
        mCustomerCode = CStr(-1)
        txtMRRNo.Text = ""
        txtMRRDate.Text = ""
        txtVNo.Text = ""
        txtVDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtCustomer.Text = ""

        txtMRRNo.Text = ""
        txtMRRDate.Text = ""
        txtBillNo.Text = ""
        txtBillDate.Text = ""
        Txt57F4No.Text = ""
        Txt57F4Date.Text = ""
        txtIssueDate.Text = ""
        txtNature.Text = ""
        txtDuration.Text = ""
        txtDespatchNo.Text = ""
        txtDespatchDate.Text = ""

        chkOpening.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkOpening.Enabled = True
        chkClosed.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkClosed.Enabled = True

        chkRejection.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkRejection.Enabled = False

        lblTotQty.Text = "0.00"
        lblTotItemValue.Text = "0.00"

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsSaleMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub FrmPaint57F4_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmPaint57F4_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Public Sub FrmPaint57F4_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Height = VB6.TwipsToPixelsY(7245) '8000
        Me.Width = VB6.TwipsToPixelsX(11355) '11900


        AdoDCMain.Visible = False

        txtCustomer.Enabled = False
        txtVDate.Enabled = IIf(CDbl(PubUserLevel) = 1 Or CDbl(PubUserLevel) = 2 Or XRIGHT = "AMDV", True, False) ''IIf(XRIGHT = "AMDV", True, False)
        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
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


    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVno_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNo.TextChanged

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



    Private Sub txtCustomer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCustomer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomer.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Function ShowFromMRRMain(ByRef mRSMRR As ADODB.Recordset) As Boolean
        On Error GoTo ErrPart
        Dim mBillDate As String
        Dim mRefType As String

        txtMRRNo.Text = IIf(IsDbNull(mRSMRR.Fields("AUTO_KEY_MRR").Value), 0, mRSMRR.Fields("AUTO_KEY_MRR").Value)
        txtMRRDate.Text = IIf(IsDbNull(mRSMRR.Fields("MRR_DATE").Value), "", mRSMRR.Fields("MRR_DATE").Value)

        If MainClass.ValidateWithMasterTable((mRSMRR.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCustomer.Text = MasterNo
            mCustomerCode = Trim(mRSMRR.Fields("SUPP_CUST_CODE").Value)
        End If
        txtBillNo.Text = IIf(IsDbNull(mRSMRR.Fields("BILL_NO").Value), "", mRSMRR.Fields("BILL_NO").Value)
        txtBillDate.Text = IIf(IsDbNull(mRSMRR.Fields("BILL_DATE").Value), "", mRSMRR.Fields("BILL_DATE").Value)

        txtIssueDate.Text = IIf(IsDbNull(mRSMRR.Fields("BILL_DATE").Value), "", mRSMRR.Fields("BILL_DATE").Value)
        mBillDate = IIf(IsDbNull(mRSMRR.Fields("BILL_DATE").Value), "", mRSMRR.Fields("BILL_DATE").Value)

        txtDuration.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 180, CDate(mBillDate)))

        mRefType = IIf(IsDbNull(mRSMRR.Fields("REF_TYPE").Value), "", mRSMRR.Fields("REF_TYPE").Value)

        chkRejection.CheckState = IIf(mRefType = "I" Or mRefType = "1", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        If ShowFromMRRDetail((mRSMRR.Fields("AUTO_KEY_MRR").Value), mCustomerCode) = False Then GoTo ErrPart
        ShowFromMRRMain = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ShowFromMRRMain = False
        '    Resume
    End Function
    Private Function ShowFromMRRDetail(ByRef mMRRNo As Double, ByRef pCustomerCode As String) As Boolean

        On Error GoTo ErrPart
        Dim RSMRR As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mTariff As String
        Dim mTariffDesc As String
        Dim mHSNCode As String
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double

        Dim mLocal As String
        Dim mPartyGSTNo As String

        mLocal = "N"
        If Trim(txtCustomer.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLocal = Trim(MasterNo)
            End If
        End If

        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If



        SqlStr = "SELECT * FROM INV_GATE_DET " & vbCrLf & " WHERE AUTO_KEY_MRR=" & mMRRNo & "" & vbCrLf & " ORDER BY SERIAL_NO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSMRR, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdMain
            cntRow = 1
            If RSMRR.EOF = False Then
                Do While Not RSMRR.EOF


                    .Row = cntRow
                    .Col = ColItemCode
                    .Text = IIf(IsDbNull(RSMRR.Fields("ITEM_CODE").Value), "", RSMRR.Fields("ITEM_CODE").Value)
                    mItemCode = IIf(IsDbNull(RSMRR.Fields("ITEM_CODE").Value), "", RSMRR.Fields("ITEM_CODE").Value)

                    mHSNCode = GetHSNCode(mItemCode)

                    .Col = ColHSNCode
                    .Text = mHSNCode

                    If GetHSNDetails(mHSNCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, "G", mPartyGSTNo) = False Then GoTo ErrPart

                    .Col = ColItemDesc
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Text = MasterNo
                    End If

                    .Col = ColPartNo
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Text = MasterNo
                    End If

                    .Col = ColUnit
                    .Text = IIf(IsDbNull(RSMRR.Fields("ITEM_UOM").Value), "", RSMRR.Fields("ITEM_UOM").Value)

                    .Col = ColQty
                    .Text = CStr(Val(IIf(IsDbNull(RSMRR.Fields("RECEIVED_QTY").Value), "", RSMRR.Fields("RECEIVED_QTY").Value)))

                    '                .Col = ColRate
                    '                .Text = GetSORate(mItemCode, pCustomerCode)

                    .Col = ColCGSTPer
                    .Text = VB6.Format(mCGSTPer, "0.00")

                    .Col = ColSGSTPer
                    .Text = VB6.Format(mSGSTPer, "0.00")

                    .Col = ColIGSTPer
                    .Text = VB6.Format(mIGSTPer, "0.00")

                    If GetTariffHeading(mItemCode, mTariff, mTariffDesc) = True Then
                        .Col = ColTariff
                        .Text = mTariff
                    End If

                    RSMRR.MoveNext()
                    cntRow = cntRow + 1
                    .MaxRows = .MaxRows + 1
                Loop
            End If
        End With
        FormatSprdMain(-1)
        Call CalcTots()
        ShowFromMRRDetail = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ShowFromMRRDetail = False
    End Function

End Class
