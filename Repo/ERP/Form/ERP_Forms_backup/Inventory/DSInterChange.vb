Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmDSInterChange
    Inherits System.Windows.Forms.Form
    Dim RsTransMain As ADODB.Recordset ''Recordset
    Dim RsTransDetail As ADODB.Recordset ''Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim xMyMenu As String

    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColUom As Short = 3
    Private Const ColSupplierCode As Short = 4
    Private Const ColSupplierName As Short = 5
    Private Const ColDSNo As Short = 6
    Private Const ColDSAmend As Short = 7
    Private Const ColDSBalQty As Short = 8
    Private Const ColQty As Short = 9
    Private Const ColRemarks As Short = 10


    Private Sub chkApproval_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkApproval.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtRefNo.Enabled = True

            SprdMain.Enabled = True
        Else
            CmdAdd.Text = ConCmdAddCaption
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

    Private Sub cmdAuthSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAuthSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND RGP_AUTH='Y'"

        If ADDMode = True Then
            SqlStr = SqlStr & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR (EMP_LEAVE_DATE >= TO_DATE('" & VB6.Format(txtSchdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')))"
        End If

        If MainClass.SearchGridMaster((txtAuthority.Text), "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , SqlStr) = True Then
            txtAuthority.Text = AcName
            txtAuthorityName.Text = AcName1
            txtAuthority_Validating(txtAuthority, New System.ComponentModel.CancelEventArgs(False))
            If txtAuthority.Enabled = True Then txtAuthority.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart
        Dim mSqlStr As String

        If chkApproval.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Voucher Already Approved, So cann't be deleted")
            Exit Sub
        End If

        If Not RsTransMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PUR_DS_INTERCHANGE_HDR", (txtRefNo.Text), RsTransMain, "REF_NO") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PUR_DS_INTERCHANGE_HDR", "REF_NO", (txtRefNo.Text)) = False Then GoTo DelErrPart

                mSqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND REF_NO=" & Val(txtRefNo.Text) & ""

                PubDBCn.Execute("Delete from PUR_DS_INTERCHANGE_DET Where " & mSqlStr)
                PubDBCn.Execute("Delete from PUR_DS_INTERCHANGE_HDR Where " & mSqlStr)

                PubDBCn.CommitTrans()
                RsTransMain.Requery() ''.Refresh
                RsTransDetail.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        ''Resume
        PubDBCn.RollbackTrans() ''
        RsTransMain.Requery() ''.Refresh
        RsTransDetail.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If cmdModify.Text = ConcmdmodifyCaption Then
            If chkApproval.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Voucher Already Approved, So cann't be modify")
                Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtRefNo.Enabled = False
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
        Call ReportONPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportONPrint(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String

        '    Report1.Reset
        '    MainClass.ClearCrptFormulas Report1
        '
        '    SqlStr = ""
        '
        '    Call MainClass.ClearCrptFormulas(Report1)
        '
        '
        '    mTitle = "Item RelationShip"
        '    mSubTitle = ""
        '    mRptFileName = "IR.rpt"
        '
        '    Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub



    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
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

    Private Sub frmDSInterChange_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        On Error GoTo ErrPart
        Dim xIName As String
        Dim xSupp As String
        Dim SqlStr As String = ""

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemDesc
                xIName = .Text
                .Text = ""
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = AcName
                Else
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = xIName
                End If
                MainClass.ValidateWithMasterTable(.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                .Row = .ActiveRow
                .Col = ColItemCode
                .Text = MasterNo
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColSupplierCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColSupplierCode
                If MainClass.SearchGridMaster(.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", "SUPP_CUST_NAME", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColSupplierCode
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColSupplierCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColSupplierName Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColSupplierName
                xIName = .Text
                .Text = ""
                If MainClass.SearchGridMaster(.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColSupplierName
                    .Text = AcName
                Else
                    .Row = .ActiveRow
                    .Col = ColSupplierName
                    .Text = xIName
                End If
                MainClass.ValidateWithMasterTable(.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                .Row = .ActiveRow
                .Col = ColSupplierCode
                .Text = MasterNo
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColSupplierCode)
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColItemCode
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                FormatSprdMain(eventArgs.row)
            End If
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent

        Dim mActiveCol As Integer

        mActiveCol = SprdMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColItemCode Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                If Trim(SprdMain.Text) <> "" Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        FormatSprdMain((SprdMain.MaxRows))
                    End If
                End If
                '            SprdMain.Row = SprdMain.MaxRows
            End If
        ElseIf eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then
            If mActiveCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
            If mActiveCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))

            If mActiveCol = ColSupplierCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColSupplierCode, 0))
            If mActiveCol = ColSupplierName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColSupplierName, 0))

            '    Else
            '        MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, mActiveCol
        End If
        eventArgs.keyCode = 9999
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim mItemCode As String
        Dim mUOM As String = ""
        Dim mSupplierName As String = ""
        Dim mSupplierCode As String = ""
        Dim mDSNo As Double
        Dim mDSAmend As Double
        Dim mDSBalQty As Double

        If eventArgs.newRow = -1 Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Col = ColItemCode
                Call FillItemDescFromItemCode((SprdMain.Text))
                If DuplicateItem() = False Then
                    '                FormatSprdMain -1
                End If

            Case ColItemDesc
                SprdMain.Col = ColItemDesc
                Call FillItemDescFromItemDesc((SprdMain.Text))
                If DuplicateItem() = False Then
                End If
            Case ColSupplierCode
                SprdMain.Col = ColSupplierCode

                If MainClass.ValidateWithMasterTable((SprdMain.Text), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSupplierName = MasterNo
                End If

                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColSupplierName
                SprdMain.Text = Trim(mSupplierName)

                If DuplicateItem() = False Then
                    '                FormatSprdMain -1
                End If

            Case ColSupplierName
                SprdMain.Col = ColSupplierName

                If MainClass.ValidateWithMasterTable((SprdMain.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSupplierCode = MasterNo
                End If

                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColSupplierCode
                SprdMain.Text = Trim(mSupplierCode)

                If DuplicateItem() = False Then
                End If

        End Select

        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColItemCode
        mItemCode = Trim(SprdMain.Text)

        SprdMain.Col = ColSupplierCode
        mSupplierCode = Trim(SprdMain.Text)

        If Trim(mItemCode) = "" Then Exit Sub
        If Trim(mSupplierCode) = "" Then Exit Sub

        If FillDSDetails(mItemCode, mSupplierCode, mDSNo, mDSAmend, mDSBalQty, (txtSchdDate.Text)) = False Then GoTo ErrPart

        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColDSNo
        SprdMain.Text = Trim(CStr(mDSNo))

        SprdMain.Col = ColDSAmend
        SprdMain.Text = Trim(CStr(mDSAmend))

        SprdMain.Col = ColDSBalQty
        SprdMain.Text = VB6.Format(mDSBalQty, "0.00")

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function DuplicateItem() As Boolean

        Dim CntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mItemCode As String

        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            mCheckItemCode = Trim(UCase(.Text))

            .Col = ColSupplierCode
            mCheckItemCode = mCheckItemCode & ":" & Trim(UCase(.Text))

            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                .Col = ColSupplierCode
                mItemCode = mCheckItemCode & ":" & Trim(UCase(.Text))

                If (mItemCode = mCheckItemCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItem = True
                    MsgInformation("Duplicate Item : " & mCheckItemCode)
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                    Exit Function
                End If
            Next
        End With
    End Function


    Private Function CheckItemQty(ByRef pChkItemCode As String) As Boolean
        Dim CntRow As Integer
        Dim mItemQty As Double
        Dim mItemCode As String

        mItemQty = 0

        With SprdMain

            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                If mItemCode = pChkItemCode Then
                    .Col = ColQty
                    mItemQty = mItemQty + Val(.Text)
                End If
            Next

            If mItemQty > 0 Then
                CheckItemQty = False
                MsgInformation("Total Interchange Qty Cann't be  Greater Than 0. (" & pChkItemCode & ")")
            Else
                CheckItemQty = True
            End If

        End With
    End Function

    Private Sub FillItemDescFromItemCode(ByRef pItemCode As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(pItemCode) = "" Then Exit Sub


        With SprdMain
            SqlStr = "SELECT ITEM_SHORT_DESC,PURCHASE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                .Row = .ActiveRow
                .Col = ColItemDesc
                .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColUom
                .Text = IIf(IsDBNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)

            Else
                MsgInformation("Invaild Item Code")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Function FillDSDetails(ByRef mItemCode As String, ByRef mSupplierCode As String, ByRef mDSNo As Double, ByRef mDSAmend As Double, ByRef mDSBalQty As Double, ByRef mSchdDate As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDSQty As String

        If Trim(mItemCode) = "" Then Exit Function
        If Trim(mSupplierCode) = "" Then Exit Function

        mDSNo = 0
        mDSAmend = 0
        mDSQty = CStr(0)

        With SprdMain
            SqlStr = "SELECT SUM(ID.TOTAL_QTY) AS TOTAL_QTY, IH.AUTO_KEY_DELV, IH.DELV_AMEND_NO " & vbCrLf _
                & " FROM PUR_DELV_SCHLD_HDR IH,  PUR_DELV_SCHLD_DET ID" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf _
                & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf _
                & " AND IH.SCHLD_DATE=TO_DATE('" & VB6.Format(mSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " GROUP BY IH.AUTO_KEY_DELV, IH.DELV_AMEND_NO "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    mDSQty = CStr(CDbl(mDSQty) + IIf(IsDBNull(RsTemp.Fields("TOTAL_QTY").Value), 0, RsTemp.Fields("TOTAL_QTY").Value))
                    mDSNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_DELV").Value), "", RsTemp.Fields("AUTO_KEY_DELV").Value)
                    mDSAmend = IIf(IsDBNull(RsTemp.Fields("DELV_AMEND_NO").Value), "", RsTemp.Fields("DELV_AMEND_NO").Value)
                    RsTemp.MoveNext()
                Loop
            End If
        End With

        mDSBalQty = CDbl(mDSQty) - GetTotalPurchaseQty(mItemCode, "", mSupplierCode, mSchdDate)

        FillDSDetails = True
        Exit Function
ERR1:
        FillDSDetails = False
        MsgInformation(Err.Description)
    End Function

    Private Function GetTotalPurchaseQty(ByRef pItemCode As String, ByRef mItemUOM As String, ByRef pPartyCode As String, ByRef pSchdDate As String) As Double

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mHDRTable As String
        Dim mDETTable As String


        GetTotalPurchaseQty = 0

        If RsCompany.Fields("MRR_AGT_GE").Value = "N" Then
            SqlStr = " SELECT SUM(BILL_QTY) AS BILL_QTY " & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID  " & vbCrLf & " Where IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.COMPANY_CODE=ID.COMPANY_CODE" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pPartyCode) & "'" & vbCrLf & " AND TO_CHAR(IH.MRR_DATE,'YYYYMM')='" & VB6.Format(pSchdDate, "YYYYMM") & "' "
        Else
            SqlStr = " SELECT SUM(BILL_QTY) AS BILL_QTY " & vbCrLf & " FROM INV_GATEENTRY_HDR IH, INV_GATEENTRY_DET ID  " & vbCrLf & " Where IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.COMPANY_CODE=ID.COMPANY_CODE" & vbCrLf & " AND IH.AUTO_KEY_GATE=ID.AUTO_KEY_GATE" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pPartyCode) & "'" & vbCrLf & " AND TO_CHAR(IH.GATE_DATE,'YYYYMM')='" & VB6.Format(pSchdDate, "YYYYMM") & "' "
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetTotalPurchaseQty = IIf(IsDBNull(RsTemp.Fields("BILL_QTY").Value), 0, RsTemp.Fields("BILL_QTY").Value)
        End If

        Exit Function
UpdateDetail1:

        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Function
    Private Sub FillItemDescFromItemDesc(ByRef pItemDesc As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(pItemDesc) = "" Then Exit Sub
        With SprdMain
            SqlStr = "SELECT ITEM_CODE,CUSTOMER_PART_NO,PURCHASE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(ITEM_SHORT_DESC))='" & MainClass.AllowSingleQuote(pItemDesc) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                .Row = .ActiveRow
                .Col = ColItemCode
                .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                .Col = ColUom
                .Text = IIf(IsDBNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
            Else
                MsgInformation("Invaild Item Description")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdView.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.row
            .Col = 1
            txtRefNo.Text = CStr(Val(.Text))

            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
            If txtRefNo.Enabled = True Then txtRefNo.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mSchdDate As String
        Dim mRefNo As Double
        Dim mApproval As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""

        mRefNo = Val(txtRefNo.Text)
        If Val(txtRefNo.Text) = 0 Then
            mRefNo = AutoGenSeq()
        End If
        txtRefNo.Text = CStr(mRefNo)

        mSchdDate = VB6.Format(txtSchdDate.Text, "DD/MM/YYYY")

        mApproval = IIf(chkApproval.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If ADDMode = True Then
            SqlStr = "INSERT INTO PUR_DS_INTERCHANGE_HDR (" & vbCrLf & " COMPANY_CODE, FYEAR, " & vbCrLf & " REF_NO, REF_DATE, " & vbCrLf & " SCHD_DATE, AUTH_GIVEN_BY, ISAPPROVAL, " & vbCrLf & " ADDUSER,ADDDATE,MODUSER,MODDATE)" & vbCrLf & " VALUES( " & vbCrLf & " " & RsCompany.Fields("Company_Code").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & mRefNo & ", TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " TO_DATE('" & VB6.Format(mSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtAuthority.Text)) & "', '" & mApproval & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE PUR_DS_INTERCHANGE_HDR SET " & vbCrLf & " REF_DATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " ISAPPROVAL='" & mApproval & "'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND REF_NO= " & mRefNo & ""
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1(mRefNo) = False Then GoTo ErrPart

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        '    Resume
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsTransMain.Requery() ''.Refresh
        RsTransDetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Ref Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If

    End Function

    Private Function UpdateDetail1(ByRef mRefNo As Double) As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mRemarks As String
        Dim mItemQty As Double

        Dim mItemUOM As String = ""
        Dim mSupplierCode As String
        Dim mDSNo As Double
        Dim mAmendNo As Double

        SqlStr = " Delete From PUR_DS_INTERCHANGE_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO=" & Val(CStr(mRefNo)) & ""

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = Trim(MainClass.AllowSingleQuote(.Text))

                .Col = ColUom
                mItemUOM = Trim(MainClass.AllowSingleQuote(.Text))

                .Col = ColSupplierCode
                mSupplierCode = Trim(MainClass.AllowSingleQuote(.Text))

                .Col = ColDSNo
                mDSNo = Val(.Text)

                .Col = ColDSAmend
                mAmendNo = Val(.Text)

                .Col = ColQty
                mItemQty = Val(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)


                SqlStr = ""


                If mItemCode <> "" Then
                    SqlStr = " INSERT INTO PUR_DS_INTERCHANGE_DET ( " & vbCrLf & " COMPANY_CODE, REF_NO, SERIAL_NO, " & vbCrLf & " ITEM_CODE, ITEM_UOM, SUPP_CUST_CODE, " & vbCrLf & " AUTO_KEY_DELV, DELV_AMEND_NO, IC_QTY, " & vbCrLf & " REMARKS) "

                    SqlStr = SqlStr & vbCrLf & " VALUES (" & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(CStr(mRefNo)) & ", " & I & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mItemCode) & "', '" & MainClass.AllowSingleQuote(mItemUOM) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mSupplierCode) & "', " & vbCrLf & "  " & Val(CStr(mDSNo)) & ",  " & Val(CStr(mAmendNo)) & ",  " & Val(CStr(mItemQty)) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mRemarks) & "') "

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mItemCode As String
        Dim mRGPDate As String
        Dim mQty As Double
        Dim mDSQty As Double

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsTransMain.EOF = True Then Exit Function

        If MODIFYMode = True And Val(txtRefNo.Text) = 0 Then
            MsgInformation("Ref No can not be Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtAuthority.Text = "" Then
            MsgBox("Authority code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtAuthority.Focus()
            Exit Function
        End If

        With SprdMain
            For mRow = 1 To .MaxRows
                .Row = mRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColDSBalQty
                mDSQty = Val(.Text)

                If mItemCode <> "" Then
                    If mQty = 0 Then
                        MsgInformation("Please Enter The Valid Qty")
                        MainClass.SetFocusToCell(SprdMain, mRow, ColQty)
                        FieldsVarification = False
                        Exit Function
                    End If

                    If CheckItemQty(mItemCode) = False Then
                        FieldsVarification = False
                        Exit Function
                    End If

                    If mQty < 0 Then
                        If mDSQty < 0 Then
                            MsgInformation("DS Qty is Less Than Zero for Item Code " & mItemCode)
                            MainClass.SetFocusToCell(SprdMain, mRow, ColQty)
                            FieldsVarification = False
                            Exit Function
                        End If

                        If mDSQty < System.Math.Abs(mQty) Then
                            MsgInformation("There is no Enough Qty to Interchange for Item Code " & mItemCode)
                            MainClass.SetFocusToCell(SprdMain, mRow, ColQty)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If
            Next
        End With

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Item Code Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColSupplierCode, "S", "Supplier Code Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColDSNo, "N", "Delivery Schedule Is Blank.") = False Then FieldsVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColDSAmend, "N", "Amend Is Blank.") = False Then FieldsVarification = False: Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Qty Is Blank.") = False Then FieldsVarification = False: Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub frmDSInterChange_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = ""
        SqlStr = "Select * from PUR_DS_INTERCHANGE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PUR_DS_INTERCHANGE_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        '    Call SetTextLengths

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
        Dim SqlStr As String = ""

        SqlStr = ""

        ''SELECT CLAUSE...

        SqlStr = "SELECT  IH.REF_NO, IH.REF_DATE"

        ''FROM CLAUSE...

        SqlStr = SqlStr & vbCrLf & " FROM PUR_DS_INTERCHANGE_HDR IH"

        ''WHERE CLAUSE...

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        ''ORDER BY CLAUSE...

        SqlStr = SqlStr & vbCrLf & " Order by IH.REF_DATE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Function AutoGenSeq() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Integer
        Dim SqlStr As String = ""
        Dim mStartingChk As Double
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(REF_NO)  " & vbCrLf & " FROM PUR_DS_INTERCHANGE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mAutoGen = mAutoGen + 1
                End If
            End If
        End With

        AutoGenSeq = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 400)

            .set_ColWidth(0, 600)
            .set_ColWidth(1, 1000)
            .set_ColWidth(2, 3500)
            .set_ColWidth(3, 1500)
            .set_ColWidth(4, 1500)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .set_RowHeight(0, ConRowHeight * 2.5)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.5)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsTransDetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemDesc, 18)

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("PURCHASE_UOM", "INV_ITEM_MST", PubDBCn) ''
            .set_ColWidth(ColUom, 4)

            .Col = ColSupplierCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsTransDetail.Fields("SUPP_CUST_CODE").DefinedSize ''
            .set_ColWidth(ColSupplierCode, 6)

            .Col = ColSupplierName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            .set_ColWidth(ColSupplierName, 18)

            .Col = ColDSNo
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsTransDetail.Fields("AUTO_KEY_DELV").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDSNo, 8)

            .Col = ColDSAmend
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsTransDetail.Fields("DELV_AMEND_NO").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDSAmend, 4)

            .Col = ColDSBalQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsTransDetail.Fields("IC_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDSBalQty, 10)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsTransDetail.Fields("IC_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQty, 10)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("REMARKS", "PUR_DS_INTERCHANGE_DET", PubDBCn)
            .set_ColWidth(ColRemarks, 15)

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUom)

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColSupplierName, ColDSBalQty)

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsTransDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsTransMain
            '        txtSupplier.MaxLength = .Fields("SUPP_CUST_CODE").DefinedSize
            txtSchdDate.MaxLength = 10
            txtAuthority.MaxLength = .Fields("AUTH_GIVEN_BY").DefinedSize

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mApproval As String

        MainClass.ClearGrid(SprdMain)

        With RsTransMain
            If Not .EOF Then

                txtRefNo.Text = IIf(IsDBNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)
                txtRefDate.Text = VB6.Format(IIf(IsDBNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")
                txtSchdDate.Text = VB6.Format(IIf(IsDBNull(.Fields("SCHD_DATE").Value), "", .Fields("SCHD_DATE").Value), "DD/MM/YYYY")

                mApproval = IIf(IsDBNull(.Fields("ISAPPROVAL").Value), "N", .Fields("ISAPPROVAL").Value)

                chkApproval.CheckState = IIf(mApproval = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkApproval.Enabled = IIf(chkApproval.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

                txtAuthority.Text = IIf(IsDBNull(.Fields("AUTH_GIVEN_BY").Value), "", .Fields("AUTH_GIVEN_BY").Value)
                If MainClass.ValidateWithMasterTable(txtAuthority.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtAuthorityName.Text = MasterNo
                End If

                Call ShowDetail1()
                txtRefNo.Enabled = True

                txtSchdDate.Enabled = False
                txtAuthority.Enabled = False
                cmdAuthSearch.Enabled = False
            End If
        End With
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        '    FormatSprdMain -1

        SprdMain.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String = ""
        Dim mQty As Double
        Dim mSupplierName As String = ""
        Dim mSupplierCode As String
        Dim mDSNo As Double
        Dim mDSAmend As Double
        Dim mDSBalQty As Double

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM PUR_DS_INTERCHANGE_DET  " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO=" & Val(txtRefNo.Text) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTransDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I


                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = mItemCode

                SprdMain.Col = ColItemDesc
                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemDesc = MasterNo
                Else
                    mItemDesc = ""
                End If
                SprdMain.Text = mItemDesc


                SprdMain.Col = ColUom
                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Purchase_UOM", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mUOM = MasterNo
                Else
                    mUOM = ""
                End If
                SprdMain.Text = mUOM

                SprdMain.Col = ColSupplierCode
                mSupplierCode = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)
                SprdMain.Text = mSupplierCode

                SprdMain.Col = ColSupplierName
                If MainClass.ValidateWithMasterTable(mSupplierCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSupplierName = MasterNo
                Else
                    mSupplierName = ""
                End If
                SprdMain.Text = mSupplierName

                '            SprdMain.Col = ColDSNo
                '            SprdMain.Text = VB6.Format(IIf(IsNull(.Fields("AUTO_KEY_DELV").Value), 0, .Fields("AUTO_KEY_DELV").Value), "0")
                '
                '            SprdMain.Col = ColDSAmend
                '            SprdMain.Text = VB6.Format(IIf(IsNull(.Fields("DELV_AMEND_NO").Value), 0, .Fields("DELV_AMEND_NO").Value), "0")
                '

                If FillDSDetails(mItemCode, mSupplierCode, mDSNo, mDSAmend, mDSBalQty, (txtSchdDate.Text)) = False Then GoTo ERR1

                SprdMain.Row = I
                SprdMain.Col = ColDSNo
                SprdMain.Text = Trim(CStr(mDSNo))

                SprdMain.Col = ColDSAmend
                SprdMain.Text = Trim(CStr(mDSAmend))

                SprdMain.Col = ColDSBalQty
                SprdMain.Text = VB6.Format(mDSBalQty, "0.00")

                SprdMain.Col = ColQty
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("IC_QTY").Value), "", .Fields("IC_QTY").Value), "0.00")

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

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
        MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub Clear1()


        txtRefNo.Text = ""
        txtRefDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")
        txtRefNo.Enabled = False
        txtRefDate.Enabled = False
        txtSchdDate.Text = ""
        txtAuthority.Text = ""
        txtAuthorityName.Text = ""

        chkApproval.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkApproval.Enabled = True

        txtSchdDate.Enabled = True
        txtAuthority.Enabled = True
        cmdAuthSearch.Enabled = True

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmDSInterChange_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmDSInterChange_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode
    End Sub

    Public Sub frmDSInterChange_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        'AdoDCMain.Visible = False

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

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub txtAuthority_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthority.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAuthority_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthority.DoubleClick
        Call cmdAuthSearch_Click(cmdAuthSearch, New System.EventArgs())
    End Sub

    Private Sub txtAuthority_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAuthority.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAuthority.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAuthority_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAuthority.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtAuthority_DoubleClick(txtAuthority, New System.EventArgs())
    End Sub

    Private Sub txtAuthority_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAuthority.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtAuthority.Text) = "" Then GoTo EventExitSub

        txtAuthority.Text = VB6.Format(Trim(txtAuthority.Text), "000000")
        If MainClass.ValidateWithMasterTable((txtAuthority.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND RGP_AUTH='Y'") = True Then
            txtAuthorityName.Text = MasterNo
        Else
            MsgInformation("Invalid Employee Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSchdDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSchdDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSchdDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSchdDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtSchdDate.Text) = "" Then GoTo EventExitSub

        If IsDate(txtSchdDate.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtSchdDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If

        txtSchdDate.Text = VB6.Format("01/" & VB6.Format(txtSchdDate.Text, "MM/YYYY"), "DD/MM/YYYY")

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
    Private Sub txtRefNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim xMkey As Double

        If Trim(txtRefNo.Text) = "" Then GoTo EventExitSub

        If Len(txtRefNo.Text) < 6 Then
            txtRefNo.Text = Val(txtRefNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If


        If MODIFYMode = True And RsTransMain.BOF = False Then xMkey = RsTransMain.Fields("REF_NO").Value


        SqlStr = "Select * From PUR_DS_INTERCHANGE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO=" & Val(txtRefNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTransMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Record, Click add to Generate New", MsgBoxStyle.Information)
                Cancel = True
                GoTo EventExitSub
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From PUR_DS_INTERCHANGE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO=" & Val(CStr(xMkey)) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
