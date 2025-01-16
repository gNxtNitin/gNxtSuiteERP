Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmADJ_RGP
    Inherits System.Windows.Forms.Form
    Dim RsRGPMain As ADODB.Recordset
    Dim RsRGPDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer


    Dim mSupplierCode As String
    Dim pRound As Double
    Private Const ConRowHeight As Short = 12

    Private Const ColRGPNo As Short = 1
    Private Const ColRGPDate As Short = 2
    Private Const ColRGPItemCode As Short = 3
    Private Const ColItemCode As Short = 4
    Private Const ColItemDesc As Short = 5
    Private Const ColUnit As Short = 6
    Private Const ColRGPQty As Short = 7
    Private Const ColBalQty As Short = 8
    Private Const ColAdjQty As Short = 9
    Private Const ColStockType As Short = 10
    Private Const ColRemarks As Short = 11

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Function ValidateRGP(ByRef mPONo As String) As Boolean

        On Error GoTo ERR1
        Dim RS As ADODB.Recordset = Nothing
        Dim mRefType As String
        Dim xPoNo As String
        Dim SqlStr As String = ""
        Dim ErrMsg As String = ""

        If mPONo = "" Then Exit Function
        ValidateRGP = True
        SqlStr = ""

        If Trim(TxtSupplier.Text) = "" Then
            mSupplierCode = "-1"
        ElseIf MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        End If


        SqlStr = "SELECT AUTO_KEY_PASSNO,GATEPASS_STATUS AS CLOSED,SUPP_CUST_CODE  from INV_GATEPASS_HDR WHERE " & vbCrLf _
            & " AUTO_KEY_PASSNO=" & Val(mPONo) & "" & vbCrLf _
            & " AND Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " ''& vbCrLf |            & " AND SUBSTR(AUTO_KEY_PASSNO,LENGTH(AUTO_KEY_PASSNO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""


        If CDbl(mSupplierCode) <> -1 Then
            SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "'"
        End If

        If Trim(txtBillTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
        End If



        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = True Then
            ValidateRGP = False
            If CDbl(mSupplierCode) = -1 Then MsgInformation("Either No Such RGP No.(s) Or " & ErrMsg & " , Press F1 On PoNo.(s) For Help... ")
            If CDbl(mSupplierCode) <> -1 Then MsgInformation("RGP No(s) Not Belong to Same Supplier")
        Else

            If RS.Fields("CLOSED").Value = "Y" Then ValidateRGP = False : MsgInformation("This RGP had been Completed, So Can Not Be Used For Further Transaction.")
            If RS.Fields("CLOSED").Value = "C" Then ValidateRGP = False : MsgInformation("This RGP Marked As Closed, So Can Not Be Used For Further Transaction.")

            mSupplierCode = RS.Fields("SUPP_CUST_CODE").Value
        End If
        Exit Function
ERR1:
        ValidateRGP = False
        MsgBox(Err.Description)
    End Function
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
            txtRefNo.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            txtRefDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
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

        If ValidateBranchLocking((txtRefDate.Text)) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, (txtRefDate.Text), (TxtSupplier.Text), mSupplierCode) = True Then
            Exit Sub
        End If

        If Trim(txtRefNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Cann't be Deleted.")
            Exit Sub
        End If

        If Not RsRGPMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_ADJ_RGP_HDR", (txtRefNo.Text), RsRGPMain, "REFNo") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_ADJ_RGP_HDR", "AUTO_KEY_NO", (lblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM INV_RGP_REG_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REF_NO='" & lblMKey.Text & "'  AND BOOKTYPE='A' AND ITEM_IO='I'")
                PubDBCn.Execute("Delete from INV_ADJ_RGP_DET Where AUTO_KEY_NO=" & Val(lblMKey.Text) & "")
                PubDBCn.Execute("Delete from INV_ADJ_RGP_HDR Where AUTO_KEY_NO=" & Val(lblMKey.Text) & "")

                PubDBCn.CommitTrans()
                RsRGPMain.Requery() ''.Refresh
                RsRGPDetail.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsRGPMain.Requery() ''.Refresh
        RsRGPDetail.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '        Resume
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If PubSuperUser = "U" Then
            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Cancelled Bill Cann't be Modified")
                Exit Sub
            End If
        Else
            TxtSupplier.Enabled = True
            cmdsearch.Enabled = True
        End If

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsRGPMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtRefNo.Enabled = False
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

        Call SelectQryFofReport(SqlStr)


        mTitle = "Adjustment RGP Report"
        mSubTitle = ""
        mRptFileName = "ADJ_RGP.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryFofReport(ByRef mSqlStr As String) As String

        'SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC,"

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO"

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_ADJ_RGP_HDR IH, INV_ADJ_RGP_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST "

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_NO=ID.AUTO_KEY_NO" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_NO=" & Val(txtRefNo.Text) & ""



        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryFofReport = mSqlStr
    End Function
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
    Private Sub cmdSavePrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSavePrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""      ''& vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster(TxtSupplier.Text, "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", "SUPP_CUST_ADDR || SUPP_CUST_CITY || SUPP_CUST_STATE", SqlStr) = True Then
            TxtSupplier.Text = AcName
            txtBillTo.Text = AcName2
            txtsupplier_Validating(TxtSupplier, New System.ComponentModel.CancelEventArgs(False))
        End If

        'If MainClass.SearchGridMaster((TxtSupplier.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
        '    TxtSupplier.Text = AcName
        '    txtsupplier_Validating(txtsupplier, New System.ComponentModel.CancelEventArgs(False))
        'End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xIName As String
        Dim xPoNo As String
        Dim SqlStr As String = ""
        Dim xSuppCode As String
        Dim xRefNo As String
        Dim xRGPCode As String


        If eventArgs.row = 0 And eventArgs.col = ColRGPNo Then
            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = ColRGPNo
            xPoNo = Trim(SprdMain.Text)


            SqlStr = "SELECT DISTINCT RGP_NO,  OUTWARD_ITEM_CODE AS ITEM_CODE, RGP_DATE, TO_CHAR(SUM(DECODE(ITEM_IO,'O',1,-1) * RGP_QTY)) AS Balance,F4NO" & vbCrLf _
                & " FROM INV_RGP_REG_TRN" & vbCrLf _
                & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

            If Trim(TxtSupplier.Text) <> "" Then
                If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    xSuppCode = MasterNo
                    SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & xSuppCode & "'"

                    If Trim(txtBillTo.Text) <> "" Then
                        SqlStr = SqlStr & vbCrLf & " AND BILL_TO_LOC_ID='" & txtBillTo.Text & "'"
                    End If
                End If
            End If

            SqlStr = SqlStr & vbCrLf & " AND RGP_NO Like '" & xPoNo & "%'"

            If Val(txtRefNo.Text) <> 0 Then
                SqlStr = SqlStr & vbCrLf & " AND REF_NO<>" & Val(txtRefNo.Text) & ""
            End If

            If IsDate(txtRefDate.Text) Then
                SqlStr = SqlStr & vbCrLf & " AND RGP_DATE<=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'O',1,-1) * RGP_QTY)>0 "

            SqlStr = SqlStr & vbCrLf & " GROUP BY RGP_NO, RGP_DATE, OUTWARD_ITEM_CODE,F4NO "

            SqlStr = SqlStr & vbCrLf & " ORDER BY RGP_NO "


            If SqlStr <> "" Then
                With SprdMain
                    .Row = .ActiveRow
                    .Col = ColRGPNo
                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColRGPNo
                        .Text = AcName

                        .Col = ColRGPItemCode
                        .Text = AcName1

                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColRGPNo)
                    End If
                End With
            End If
        End If

        If eventArgs.row = 0 And eventArgs.col = ColRGPItemCode Then
            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = ColRGPNo
            xPoNo = Trim(SprdMain.Text)


            SqlStr = "SELECT DISTINCT OUTWARD_ITEM_CODE AS ITEM_CODE, INVITEM.ITEM_SHORT_DESC" & vbCrLf _
                & " FROM INV_RGP_REG_TRN TRN, INV_ITEM_MST INVITEM" & vbCrLf _
                & " WHERE TRN.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

            SqlStr = SqlStr & vbCrLf _
                & " AND TRN.Company_Code=INVITEM.Company_Code" & vbCrLf _
                & " AND TRN.OUTWARD_ITEM_CODE=INVITEM.ITEM_CODE"


            If Trim(TxtSupplier.Text) <> "" Then
                If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    xSuppCode = MasterNo
                    SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & xSuppCode & "'"

                    If Trim(txtBillTo.Text) <> "" Then
                        SqlStr = SqlStr & vbCrLf & " AND BILL_TO_LOC_ID='" & txtBillTo.Text & "'"
                    End If
                End If
            End If

            SqlStr = SqlStr & vbCrLf & " AND RGP_NO Like '" & xPoNo & "%'"

            'If IsDate(txtRefDate.Text) Then
            '    SqlStr = SqlStr & vbCrLf & " AND RGP_DATE<=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            'End If

            SqlStr = SqlStr & vbCrLf & " ORDER BY OUTWARD_ITEM_CODE "


            If SqlStr <> "" Then
                With SprdMain
                    .Row = .ActiveRow
                    .Col = ColRGPItemCode
                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColRGPItemCode
                        .Text = AcName

                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColRGPNo)
                    End If
                End With
            End If
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode

                .Col = ColRGPNo
                xRefNo = Trim(.Text)

                .Col = ColRGPItemCode
                xRGPCode = Trim(.Text)

                .Row = .ActiveRow
                .Col = ColItemCode

                SqlStr = SelectQuery(xRefNo, True, xRGPCode)

                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName)
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

                .Col = ColRGPNo
                xRefNo = Trim(.Text)

                .Row = .ActiveRow
                .Col = ColItemCode

                SqlStr = SelectQuery(xRefNo, False)

                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = Trim(AcName)
                Else
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = xIName
                End If

                MainClass.ValidateWithMasterTable(.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                .Row = .ActiveRow
                .Col = ColItemCode
                .Text = Trim(MasterNo)
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

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

        Dim mPONo As String
        Dim mItemCode As String
        Dim DelStatus As Boolean
        If eventArgs.col = 0 And eventArgs.row > 0 Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColRGPNo
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then

                mPONo = SprdMain.Text

                SprdMain.Col = ColItemCode
                mItemCode = SprdMain.Text

                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColRGPNo, DelStatus)
                FormatSprdMain(-1)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
            End If
        End If

    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        Dim xPoNo As String
        Dim xICode As String
        Dim mQty As Double
        'Dim mAcceptQty As Double
        'Dim mItemClassType As String
        'Dim mLotNoRequied As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xRGPItemCode As String

        If eventArgs.NewRow = -1 Then Exit Sub

        SprdMain.Row = eventArgs.Row

        Select Case eventArgs.col
            Case ColRGPNo
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColRGPNo
                xPoNo = SprdMain.Text

                If Val(xPoNo) = 0 Then
                    Exit Sub
                End If

                If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PASSNO", "AUTO_KEY_PASSNO", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & mSupplierCode & "'") = False Then
                    MsgInformation("Invalid Ref No for Such Supplier")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColRGPNo)
                    Exit Sub
                End If

                SqlStr = "SELECT RGP_NO, RGP_DATE, OUTWARD_ITEM_CODE AS ITEM_CODE" & vbCrLf & " FROM INV_RGP_REG_TRN" & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUPP_CUST_CODE='" & mSupplierCode & "'" & vbCrLf & " AND RGP_NO = " & xPoNo & " AND ITEM_IO='O'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColRGPDate
                    SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("RGP_DATE").Value), "", RsTemp.Fields("RGP_DATE").Value)
                    '                        SprdMain.Col = ColRGPItemCode
                    '                        SprdMain.Text = IIf(IsNull(RsTemp!ITEM_CODE), "", RsTemp!ITEM_CODE)
                Else
                    MsgInformation("Invalid Ref No for Such Supplier")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColRGPNo)
                End If

            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColRGPNo
                xPoNo = SprdMain.Text

                SprdMain.Col = ColRGPItemCode
                xRGPItemCode = Trim(SprdMain.Text)


                If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PASSNO", "AUTO_KEY_PASSNO", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & mSupplierCode & "'") = False Then
                    MsgInformation("Invalid Ref No for Such Supplier")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColRGPNo)
                    Exit Sub
                End If

                SprdMain.Col = ColItemCode
                xICode = Trim(SprdMain.Text)
                If xICode = "" Then Exit Sub
                If MainClass.ValidateWithMasterTable(xICode, "Item_Code", "Item_Code", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If DuplicateItemCode() = False Then
                        If FillGridRow(xPoNo, xICode, xRGPItemCode) = False Then Exit Sub
                        FormatSprdMain(eventArgs.row)
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColAdjQty)
                    Else
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                End If

            Case ColItemDesc
                SprdMain.Col = ColItemDesc
                If MainClass.ValidateWithMasterTable(SprdMain.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                End If

            Case ColAdjQty
                SprdMain.Col = ColRGPNo
                xPoNo = SprdMain.Text
                If xPoNo = "" Then Exit Sub

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                If CheckBillQty(eventArgs.col, eventArgs.row) = True Then
                    SprdMain.Col = ColAdjQty
                    mQty = Val(SprdMain.Text)
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain(eventArgs.row)
                End If

            Case ColStockType
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColStockType
                If Trim(SprdMain.Text) = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(SprdMain.Text, "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("InValid Stock Type")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
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
            .Col = ColRGPNo
            mCheckItemCode = CStr(Val(.Text))

            .Col = ColItemCode
            mCheckItemCode = mCheckItemCode & Trim(UCase(.Text))

            For CntRow = 1 To .MaxRows
                .Row = CntRow

                .Col = ColRGPNo
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

    Private Function FillGridRow(ByRef mPONo As String, ByRef mItemCode As String, ByRef mOutItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mOrdQty As Object
        Dim mRecvQty As Double
        Dim xPoNo As String
        Dim xFYNo As Integer
        Dim xSupplierCode As Integer
        Dim mOrderSno As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mQCEmpCode As String

        If mItemCode = "" Then Exit Function

        SqlStr = ""
        SqlStr = " SELECT Item_Code,ITEM_SHORT_DESC AS NAME," & vbCrLf & " PURCHASE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND Item_Code='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc
                If CollectPOData(mPONo, mItemCode, mOutItemCode, (SprdMain.Row)) = False Then
                    MsgInformation("Invalid Item Code for PONo " & mPONo)
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                    FillGridRow = False
                    Exit Function
                End If

                SprdMain.Row = SprdMain.ActiveRow
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
    Private Function CheckQty(ByVal Col As Integer, ByVal Row As Integer) As Boolean

        On Error GoTo ERR1
        Dim mPOQty As Double
        Dim mBillQty As Double
        Dim mEXQty As Double
        With SprdMain

            'sk   '25-10-2004
            '    If mWithOutOrder = True Then CheckQty = True: Exit Function

            .Row = Row
            .Col = ColRGPQty
            mPOQty = Val(.Text)

            .Col = ColAdjQty
            mBillQty = Val(.Text)

            mEXQty = mBillQty * IIf(IsDbNull(RsCompany.Fields("GRExcessPer").Value), 0, RsCompany.Fields("GRExcessPer").Value) / 100
            .Col = ColAdjQty

            If Val(.Text) > mBillQty + mEXQty Then
                MsgInformation("Receipt Qty can not be greater than Bill Qty") ' & RsCompany!GRExcessPer & "%"
                MainClass.SetFocusToCell(SprdMain, Row, Col)
                CheckQty = False

            Else

                CheckQty = True
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function CheckBillQty(ByVal Col As Integer, ByVal Row As Integer) As Boolean

        On Error GoTo ERR1
        Dim mBalQty As Double
        Dim mAdjQty As Double
        With SprdMain

            .Row = Row
            .Col = ColBalQty
            mBalQty = Val(.Text)

            .Col = ColAdjQty
            mAdjQty = Val(.Text)
            If mAdjQty = 0 Then CheckBillQty = True : Exit Function

            If mAdjQty > mBalQty Then
                MsgInformation("Adjustment Qty can not be greater than Balance Qty") ' & RsCompany!GRExcessPer & "%"
                MainClass.SetFocusToCell(SprdMain, Row, ColAdjQty)
                CheckBillQty = False
            Else

                CheckBillQty = True
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
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
        Dim mMRRNO As String
        Dim SqlStr As String = ""

        If Trim(txtRefNo.Text) = "" Then GoTo EventExitSub

        If Len(txtRefNo.Text) < 6 Then
            txtRefNo.Text = Val(txtRefNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsRGPMain.EOF = False Then xMkey = RsRGPMain.Fields("AUTO_KEY_NO").Value
        mMRRNO = Trim(txtRefNo.Text)

        SqlStr = " SELECT * FROM INV_ADJ_RGP_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_NO=" & Val(mMRRNO) & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRGPMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsRGPMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Ref No, Use Generate MRR Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM INV_ADJ_RGP_HDR " & " WHERE AUTO_KEY_NO=" & Val(xMkey) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRGPMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        Dim mCancelled As String

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
            SqlStr = "INSERT INTO INV_ADJ_RGP_HDR( " & vbCrLf _
                & " COMPANY_CODE, " & vbCrLf _
                & " AUTO_KEY_NO, REF_DATE," & vbCrLf _
                & " SUPP_CUST_CODE, CANCELLED, REMARKS," & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE, BILL_TO_LOC_ID) "


            SqlStr = SqlStr & vbCrLf _
                & " VALUES(" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                & " " & Val(CStr(mVNoSeq)) & ", TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mSuppCustCode) & "', " & vbCrLf _
                & " '" & mCancelled & "', '" & MainClass.AllowSingleQuote((TxtRemarks.Text)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','" & MainClass.AllowSingleQuote(txtBillTo.Text) & "')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE INV_ADJ_RGP_HDR SET " & vbCrLf _
                & " AUTO_KEY_NO =" & Val(CStr(mVNoSeq)) & " ," & vbCrLf _
                & " REF_DATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "'," & vbCrLf _
                & " CANCELLED='" & mCancelled & "', BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'," & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote((TxtRemarks.Text)) & "',"

            SqlStr = SqlStr & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE AUTO_KEY_NO ='" & MainClass.AllowSingleQuote(LblMkey.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(mSuppCustCode) = False Then GoTo ErrPart

        UpdateMain1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsRGPMain.Requery() ''.Refresh
        RsRGPDetail.Requery() ''.Refresh
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

        If txtRefNo.Text = 1 & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") Then Exit Function

        SqlStr = "SELECT MAX(REF_DATE)" & vbCrLf & " FROM INV_ADJ_RGP_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_NO<" & Val(CStr(pMRRNoSeq)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDbNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If

        SqlStr = "SELECT MIN(REF_DATE)" & " FROM INV_ADJ_RGP_HDR " & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_NO>" & Val(CStr(pMRRNoSeq)) & ""

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
        Dim RsRGPMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_NO)  " & vbCrLf & " FROM INV_ADJ_RGP_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRGPMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsRGPMainGen
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

    Private Function UpdateDetail1(ByRef pSupplierCode As String) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mRGPNo As Double
        Dim mRGPDate As String
        Dim mItemCode As String
        Dim mUnit As String
        Dim mStockType As String = ""
        Dim mAdjQty As Double

        Dim mOutwardF4No As Double
        Dim mOutwardF4Date As String = ""
        Dim mCheckF4 As Boolean

        Dim mRecord As Boolean

        Dim mRGPQty As Double
        Dim mRGPItemCode As String
        Dim mRemarks As String
        Dim mExpRtnDate As String = ""

        mRecord = False
        PubDBCn.Execute("Delete From INV_ADJ_RGP_DET Where AUTO_KEY_NO='" & lblMKey.Text & "'")

        PubDBCn.Execute("DELETE FROM INV_RGP_REG_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REF_NO='" & lblMKey.Text & "'  AND BOOKTYPE='A' AND ITEM_IO='I'")

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColRGPNo
                mRGPNo = Val(.Text)

                .Col = ColRGPDate
                mRGPDate = MainClass.AllowSingleQuote(.Text)

                .Col = ColRGPItemCode
                mRGPItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColStockType
                mStockType = IIf(Trim(.Text) = "", "ST", MainClass.AllowSingleQuote(.Text))

                .Col = ColAdjQty
                mAdjQty = Val(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mItemCode <> "" And mAdjQty <> 0 Then

                    SqlStr = " INSERT INTO INV_ADJ_RGP_DET ( " & vbCrLf _
                        & " COMPANY_CODE, " & vbCrLf _
                        & " AUTO_KEY_NO, SERIAL_NO, " & vbCrLf _
                        & " ITEM_CODE, ITEM_UOM," & vbCrLf _
                        & " REF_PO_NO, REF_DATE, " & vbCrLf _
                        & " RGP_ITEM_CODE, STOCK_TYPE," & vbCrLf _
                        & " ADJ_QTY, REMARKS ) VALUES ( "

                    SqlStr = SqlStr & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " '" & LblMkey.Text & "', " & I & ", " & vbCrLf _
                        & " '" & mItemCode & "', '" & mUnit & "', " & vbCrLf _
                        & " " & Val(CStr(mRGPNo)) & ", TO_DATE('" & VB6.Format(mRGPDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " '" & mRGPItemCode & "', '" & mStockType & "'," & vbCrLf _
                        & " " & mAdjQty & ", '" & mRemarks & "')"

                    PubDBCn.Execute(SqlStr)

                    If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        Call GetF4detailFromRGP(mRGPNo, mCheckF4, mOutwardF4No, mOutwardF4Date, mExpRtnDate)

                        If UpdateRGP_TRN(PubDBCn, mRGPNo, VB6.Format(mRGPDate, "DD/MM/YYYY"), CDbl(txtRefNo.Text), VB6.Format(txtRefDate.Text, "DD/MM/YYYY"), pSupplierCode, mOutwardF4No, VB6.Format(mOutwardF4Date, "DD/MM/YYYY"), "A" & txtRefNo.Text, (txtRefDate.Text), Trim(mRGPItemCode), mItemCode, mRGPQty, mAdjQty, "I", I, "A", mExpRtnDate, txtBillTo.Text) = False Then GoTo UpdateDetail1Err
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


        If ValidateBranchLocking((txtRefDate.Text)) = True Then
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
        If MODIFYMode = True And RsRGPMain.EOF = True Then Exit Function

        If MODIFYMode = True And txtRefNo.Text = "" Then
            MsgInformation("Voucher No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtRefDate.Text = "" Then
            MsgBox("VDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtRefDate.Focus()
            Exit Function
        ElseIf FYChk((txtRefDate.Text)) = False Then
            FieldsVarification = False
            If txtRefDate.Enabled = True Then txtRefDate.Focus()
            Exit Function
        End If

        If Trim(TxtSupplier.Text) = "" Then
            MsgBox("Supplier Cannot Be Blank", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
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


        If Trim(txtBillTo.Text) = "" Then
            MsgInformation("Bill To is blank. Cannot Save")
            txtBillTo.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtSupplier.Text) & "'") = False Then
                MsgBox("Invalid Location Id for such Supplier.", MsgBoxStyle.Information)
                If txtBillTo.Enabled = True Then txtBillTo.Focus()
                FieldsVarification = False
            End If
        End If


        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColAdjQty, "N", "Please Check Bill Quantity.") = False Then FieldsVarification = False: Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmADJ_RGP_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "RGP - Adjustment Entry"

        SqlStr = ""
        SqlStr = "Select * from INV_ADJ_RGP_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRGPMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_ADJ_RGP_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRGPDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        mSupplierCode = CStr(-1)
        FormatSprdMain(-1)

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

        SqlStr = "Select GR.AUTO_KEY_NO as Ref_No," & vbCrLf & " TO_CHAR(GR.REF_DATE,'DD-MM-YYYY') as REF_Date, " & vbCrLf & " AC.SUPP_CUST_NAME AS SupplierName " & vbCrLf & " FROM INV_ADJ_RGP_HDR GR,FIN_SUPP_CUST_MST AC " & vbCrLf & " WHERE " & vbCrLf & " GR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND GR.COMPANY_CODE=AC.COMPANY_CODE " & vbCrLf & " AND GR.SUPP_CUST_CODE=AC.SUPP_CUST_CODE " & vbCrLf & " Order by AUTO_KEY_NO"

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

            .set_ColWidth(1, 1200)
            .set_ColWidth(2, 1200)
            .set_ColWidth(3, 4500)


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
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.25)

            .Col = ColRGPNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsRGPDetail.Fields("REF_PO_NO").Precision ''
            '        .ColHidden = True
            .set_ColWidth(ColRGPNo, 12)

            .Col = ColRGPDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 10 ''
            .set_ColWidth(ColRGPDate, 10)

            .Col = ColRGPItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsRGPDetail.Fields("ITEM_CODE").Precision ''
            .set_ColWidth(ColRGPItemCode, 8)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsRGPDetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemDesc, 30)
            .ColsFrozen = ColItemDesc

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsRGPDetail.Fields("ITEM_UOM").DefinedSize ''
            .set_ColWidth(ColUnit, 4)

            .Col = ColRGPQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRGPQty, 9)
            .ColHidden = True

            .Col = ColBalQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBalQty, 9)

            .Col = ColAdjQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAdjQty, 9)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsRGPDetail.Fields("STOCK_TYPE").DefinedSize ''
            .set_ColWidth(ColStockType, 5)
            .ColHidden = True

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsRGPDetail.Fields("REMARKS").DefinedSize ''
            .set_ColWidth(ColRemarks, 17)


        End With

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUnit)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRGPQty, ColBalQty)

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRGPDate, ColRGPItemCode)
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsRGPDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsRGPMain

            txtRefNo.Maxlength = .Fields("AUTO_KEY_NO").Precision
            txtRefDate.Maxlength = 10
            TxtSupplier.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtBillTo.MaxLength = MainClass.SetMaxLength("LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn)
            TxtRemarks.Maxlength = .Fields("REMARKS").DefinedSize

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing

        With RsRGPMain
            If Not .EOF Then
                lblMKey.Text = .Fields("AUTO_KEY_NO").Value


                txtRefNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_NO").Value), "", .Fields("AUTO_KEY_NO").Value)
                txtRefDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")

                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtSupplier.Text = MasterNo
                End If
                mSupplierCode = .Fields("SUPP_CUST_CODE").Value

                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)

                TxtRemarks.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                chkCancelled.CheckState = IIf(.Fields("CANCELLED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                Call ShowDetail1((lblMKey.Text))
                TxtSupplier.Enabled = False
                cmdsearch.Enabled = False

            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsRGPMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        txtRefNo.Enabled = True

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
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim SqlStr As String = ""
        Dim mRGPQty As Double
        Dim mRecdQty As Double
        Dim mBalQty As Double
        Dim mRefPoNo As Double
        Dim mRGPItemCode As String
        Dim mOpenOrder As Boolean

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_ADJ_RGP_DET " & vbCrLf & " Where AUTO_KEY_NO=" & Val(mMKEY) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRGPDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsRGPDetail
            If .EOF = True Then Exit Sub
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColRGPNo
                mRefPoNo = Val(IIf(IsDbNull(.Fields("REF_PO_NO").Value), -1, .Fields("REF_PO_NO").Value))

                SprdMain.Text = CStr(mRefPoNo)

                SprdMain.Col = ColRGPDate
                SprdMain.Text = IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value)

                SprdMain.Col = ColRGPItemCode
                mRGPItemCode = Trim(IIf(IsDbNull(.Fields("RGP_ITEM_CODE").Value), "", .Fields("RGP_ITEM_CODE").Value))
                SprdMain.Text = Trim(mRGPItemCode)

                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                Call CollectPOData(Str(mRefPoNo), Trim(mItemCode), Trim(mRGPItemCode), I)

                SprdMain.Row = I
                SprdMain.Col = ColAdjQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ADJ_QTY").Value), 0, .Fields("ADJ_QTY").Value)))

                SprdMain.Col = ColStockType
                SprdMain.Text = IIf(IsDbNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

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
        MainClass.ButtonStatus(Me, XRIGHT, RsRGPMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        lblMKey.Text = ""

        mSupplierCode = CStr(-1)
        txtRefNo.Text = ""
        txtRefDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        chkCancelled.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)

        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        TxtSupplier.Text = ""
        txtBillTo.Text = ""
        TxtRemarks.Text = ""

        txtRefDate.Enabled = False
        TxtSupplier.Enabled = True
        cmdsearch.Enabled = True

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsRGPMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FrmADJ_RGP_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    'Private Sub Form_KeyUp(KeyCode As Integer, Shift As Integer)
    '    MainClass.DoFunctionKey Me, KeyCode
    'End Sub

    Private Sub FrmADJ_RGP_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
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
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245) '8000
        ''Me.Width = VB6.TwipsToPixelsX(11355) '11900

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

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColRGPNo Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColRGPNo, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColStockType Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColStockType, 0))

        SprdMain.Refresh()

    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False
        End With
    End Sub
    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtSupplier.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillTo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBillTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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
    Private Function CollectPOData(ByRef xPoNo As String, ByRef xItemCode As String, ByRef xOutItemCode As String, ByRef mRowNo As Integer) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsPO As ADODB.Recordset = Nothing
        Dim xFYNo As Integer
        Dim jj As Integer
        Dim mSprdRowNo As Integer
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mInConUnit As Double
        Dim mOutConUnit As Double

        Dim mMultiItemCode As Boolean
        Dim mMKEY As String = ""
        Dim mCheckOutItem As String = ""


        SqlStr = ""

        If Trim(xItemCode) = Trim(xOutItemCode) Then
            mOutConUnit = 1
        Else
            SqlStr = "SELECT IH.MKEY, IH.PRODUCT_CODE, ID.RM_CODE As ITEM_CODE,STD_QTY ITEM_QTY " & vbCrLf _
                & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID " & vbCrLf _
                & " WHERE " & vbCrLf _
                & " IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.PRODUCT_CODE='" & xItemCode & "' AND STATUS='O'"

            SqlStr = SqlStr & vbCrLf _
                & " AND IH.WEF = (" & vbCrLf _
                & " SELECT MAX(WEF) " & vbCrLf _
                & " FROM PRD_NEWBOM_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND PRODUCT_CODE='" & xItemCode & "'" & vbCrLf _
                & " AND WEF<=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


            mMultiItemCode = False
            mInConUnit = 1
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    mMKEY = IIf(IsDbNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)
                    mCheckOutItem = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), 0, RsTemp.Fields("ITEM_CODE").Value)

                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        mMultiItemCode = True
                        Exit Do
                    End If
                Loop
                RsTemp.MoveFirst()
            Else
                CollectPOData = False
                Exit Function
            End If
        End If

        If mCheckOutItem <> "" Then
            If Trim(mCheckOutItem) = Trim(xOutItemCode) Then
                mOutConUnit = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
            Else
                'SqlStr = "SELECT ALTER_ITEM_CODE, ALTER_ITEM_QTY " & vbCrLf & " FROM PRD_OUTBOM_ALTER_DET " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY='" & mMKEY & "'" & vbCrLf & " AND PRODUCT_CODE='" & xItemCode & "'" & vbCrLf & " AND ALTER_ITEM_CODE='" & xOutItemCode & "'"

                'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                'If RsTemp.EOF = False Then
                '    mOutConUnit = IIf(IsDBNull(RsTemp.Fields("ALTER_ITEM_QTY").Value), 0, RsTemp.Fields("ALTER_ITEM_QTY").Value)
                'Else
                CollectPOData = False
                Exit Function
                'End If
            End If
        End If

        SqlStr = " SELECT POM.*, " & vbCrLf _
            & " POD.*, " & vbCrLf _
            & " AC.SUPP_CUST_NAME as SuppName " & vbCrLf _
            & " FROM INV_GATEPASS_HDR POM,INV_GATEPASS_DET POD,FIN_SUPP_CUST_MST AC " & vbCrLf _
            & " WHERE POM.AUTO_KEY_PASSNO = POD.AUTO_KEY_PASSNO " & vbCrLf _
            & " And POM.Company_Code = AC.Company_Code " & vbCrLf _
            & " And POM.SUPP_CUST_CODE = AC.SUPP_CUST_CODE " & vbCrLf _
            & " And POM.AUTO_KEY_PASSNO=" & Val(xPoNo) & " " & vbCrLf _
            & " And POM.SUPP_CUST_CODE='" & mSupplierCode & "' " & vbCrLf _
            & " And POM.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " and POM.GATEPASS_STATUS='N' AND POD.ITEM_CODE = '" & Trim(xOutItemCode) & "'" & vbCrLf _
            & " order by POD.SERIAL_NO"

        If SqlStr = "" Then Exit Function

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPO, ADODB.LockTypeEnum.adLockReadOnly)

        If RsPO.EOF = False Then
            If mMultiItemCode = False Then
                FillRGPDetailPart(RsPO, Val(xPoNo), mRowNo, Trim(xItemCode), Trim(xOutItemCode), mInConUnit, mOutConUnit, mSupplierCode)
            End If
            CollectPOData = True
        Else
            CollectPOData = False
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        CollectPOData = False
    End Function


    Private Sub FillRGPDetailPart(ByRef RsPO As ADODB.Recordset, ByRef mRefNo As Double, ByRef SprdRowNo As Integer, ByRef xInItemCode As String, ByRef xOutItemCode As String, ByRef xInConUnit As Double, ByRef xOutConUnit As Double, ByRef pSupplierCode As String)


        On Error GoTo ERR1
        Dim mOrdQty As Object
        Dim mRecvQty As Double
        Dim mOutItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        Dim mCheckUOM As String
        Dim mPOQty As Double
        Dim mRecdQty As Double
        Dim mBalQty As Double
        Dim mRejQty As Double

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mQCEmpCode As String

        If RsPO.EOF Then Exit Sub
        RsPO.MoveFirst()
        mOutItemCode = xOutItemCode 'Trim(IIf(IsNull(RsPO!ITEM_CODE), "", RsPO!ITEM_CODE))

        With SprdMain

            .Row = SprdRowNo

            .Col = ColItemCode
            .Text = xInItemCode

            .Col = ColItemDesc
            MainClass.ValidateWithMasterTable(xInItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mItemDesc = MasterNo
            .Text = mItemDesc

            .Col = ColUnit
            If xInItemCode = xOutItemCode Then
                mItemUOM = IIf(IsDbNull(RsPO.Fields("ITEM_UOM").Value), "", RsPO.Fields("ITEM_UOM").Value)
            Else
                If MainClass.ValidateWithMasterTable(xInItemCode, "Item_Code", "ISSUE_UOM", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemUOM = MasterNo
                End If
            End If

            .Text = mItemUOM

            .Col = ColRGPQty
            If xInItemCode = xOutItemCode Then
                mPOQty = IIf(IsDbNull(RsPO.Fields("ITEM_QTY").Value), "", RsPO.Fields("ITEM_QTY").Value)
            Else
                mPOQty = xInConUnit * IIf(IsDbNull(RsPO.Fields("ITEM_QTY").Value), "", RsPO.Fields("ITEM_QTY").Value) / xOutConUnit
            End If

            .Text = CStr(mPOQty)

            mRecdQty = CalcRGPRecvQty(mRefNo, xOutItemCode, mSupplierCode)

            If xInItemCode <> xOutItemCode Then
                mRecdQty = xInConUnit * mRecdQty / xOutConUnit
            End If

            mRejQty = 0

            mBalQty = mPOQty - (mRecdQty + mRejQty)
            .Col = ColBalQty
            .Text = CStr(mBalQty)

            .Col = ColStockType
            .Text = IIf(Trim(.Text) = "", "ST", Trim(.Text))

        End With
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Function CalcPOQty(ByRef pSupplierCode As String, ByRef pPONO As Double, ByRef pItemCode As String, ByRef pRefType As String, ByRef pOpenOrder As Boolean) As Double

        On Error GoTo ErrPart
        Dim mSchdDate As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        mSchdDate = "01/" & VB6.Format(txtRefDate.Text, "MM") & "/" & VB6.Format(txtRefDate.Text, "YYYY")

        SqlStr = "SELECT ITEM_QTY" & vbCrLf & " FROM INV_GATEPASS_HDR POMain, INV_GATEPASS_DET PODetail" & vbCrLf & " WHERE POMain.AUTO_KEY_PASSNO=PODetail.AUTO_KEY_PASSNO" & vbCrLf & " AND POMain.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND POMain.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND PODetail.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND PODetail.AUTO_KEY_PASSNO=" & Val(CStr(pPONO)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CalcPOQty = IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If


        Exit Function
ErrPart:
        CalcPOQty = 0
    End Function
    Private Function CalcRGPRecvQty(ByRef CurrPONo As Double, ByRef CurrItemCode As String, ByRef pSupplierCode As String) As Double

        On Error GoTo CalcRecvQtyErr
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim xPoNo As String
        Dim xFYNo As Integer
        Dim SqlStr As String = ""
        Dim CurrMrrNo As Double


        CurrMrrNo = IIf(Trim(txtRefNo.Text) = "", -1, Val(txtRefNo.Text))

        SqlStr = ""

        SqlStr = "SELECT SUM(DECODE(ITEM_IO,'O',0,1)*TRN.RGP_QTY) AS RECDQTY " & vbCrLf & " FROM INV_RGP_REG_TRN TRN WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(UCase(pSupplierCode)) & "' " & vbCrLf & " AND TRN.RGP_NO=" & Val(CStr(CurrPONo)) & " " & vbCrLf & " AND TRN.OUTWARD_ITEM_CODE='" & MainClass.AllowSingleQuote(CurrItemCode) & "' "

        If CurrMrrNo <> CDbl("-1") Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.REF_NO<>" & Val(CStr(CurrMrrNo)) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            CalcRGPRecvQty = Val(IIf(IsDbNull(RsMisc.Fields(0).Value), 0, RsMisc.Fields(0).Value))
        Else
            CalcRGPRecvQty = 0.0#
        End If
        Exit Function
CalcRecvQtyErr:
        CalcRGPRecvQty = 0.0#
        MsgBox(Err.Description)
    End Function


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
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mSupplierCode = MasterNo
        Else
            mSupplierCode = "-1"
            Cancel = True
        End If

        If Trim(txtBillTo.Text) = "" Then
            txtBillTo.Text = GetDefaultLocation(mSupplierCode)
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub





    Private Sub GetF4detailFromRGP(ByRef mPONo As Double, ByRef mCheckF4 As Boolean, ByRef mOutwardF4No As Double, ByRef mOutwardF4Date As String, ByRef mExpRtnDate As String)

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        mCheckF4 = False
        mOutwardF4No = CDbl("0")
        mOutwardF4Date = ""

        mSqlStr = " SELECT OUTWARD_57F4NO,GATEPASS_DATE,EXP_RTN_DATE " & vbCrLf & " FROM INV_GATEPASS_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PASSNO=" & mPONo & ""

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mOutwardF4No = IIf(IsDbNull(RsTemp.Fields("OUTWARD_57F4NO").Value), "0", RsTemp.Fields("OUTWARD_57F4NO").Value)
            mOutwardF4Date = VB6.Format(IIf(IsDbNull(RsTemp.Fields("GATEPASS_DATE").Value), "", RsTemp.Fields("GATEPASS_DATE").Value), "DD/MM/YYYY")
            mExpRtnDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("EXP_RTN_DATE").Value), "", RsTemp.Fields("EXP_RTN_DATE").Value), "DD/MM/YYYY")

            If Val(CStr(mOutwardF4No)) = 0 Then
                mCheckF4 = False
            Else
                mCheckF4 = True
            End If
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function SelectQuery(ByRef xRefNo As String, ByRef xIsItemCode As Boolean, Optional ByRef pRGPItemCode As String = "") As String
        On Error GoTo ErrPart
        Dim mSuppCode As String
        Dim SelectQuery1 As String

        If xIsItemCode = True Then
            SelectQuery = "SELECT DISTINCT INVMST.ITEM_CODE, INVMST.ITEM_SHORT_DESC "
        Else
            SelectQuery = "SELECT DISTINCT INVMST.ITEM_SHORT_DESC, INVMST.ITEM_CODE "
        End If

        SelectQuery = SelectQuery & vbCrLf _
            & " FROM INV_GATEPASS_HDR IH, INV_GATEPASS_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_PASSNO = ID.AUTO_KEY_PASSNO " & vbCrLf _
            & " AND INVMST.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.Company_Code=INVMST.Company_Code" & vbCrLf _
            & " AND ID.ITEM_Code=INVMST.ITEM_Code" & vbCrLf _
            & " AND ID.ITEM_CODE='" & pRGPItemCode & "'" & vbCrLf _
            & " AND IH.GATEPASS_TYPE ='R'  " & vbCrLf _
            & " AND IH.AUTO_KEY_PASSNO=" & Val(xRefNo) & ""

        If xIsItemCode = True Then
            SelectQuery1 = "SELECT DISTINCT INVMST.ITEM_CODE, INVMST.ITEM_SHORT_DESC "
        Else
            SelectQuery1 = "SELECT DISTINCT INVMST.ITEM_SHORT_DESC, INVMST.ITEM_CODE "
        End If

        SelectQuery = SelectQuery & vbCrLf _
            & " UNION " & SelectQuery1 & vbCrLf _
            & " FROM  " & vbCrLf & " PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, " & vbCrLf _
            & " INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.MKEY = ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE = INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.PRODUCT_CODE = INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ID.RM_CODE='" & pRGPItemCode & "'"
        '
        '             SelectQuery = SelectQuery & vbCrLf _
        ''                    & " AND IH.WEF = (" & vbCrLf _
        ''                    & " SELECT MAX(WEF) " & vbCrLf _
        ''                    & " FROM PRD_OUTBOM_HDR " & vbCrLf _
        ''                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''                    & " AND PRODUCT_CODE='" & xItemCode & "'" & vbCrLf _
        ''                    & " AND WEF<='" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "')"


        'SelectQuery = SelectQuery & vbCrLf _
        '    & " UNION " & SelectQuery1 & vbCrLf _
        '    & " FROM  " & vbCrLf _
        '    & " PRD_OUTBOM_HDR IH,PRD_OUTBOM_ALTER_DET ID, " & vbCrLf _
        '    & " INV_ITEM_MST INVMST" & vbCrLf _
        '    & " WHERE IH.MKEY = ID.MKEY " & vbCrLf _
        '    & " AND IH.COMPANY_CODE = INVMST.COMPANY_CODE " & vbCrLf _
        '    & " AND ID.PRODUCT_CODE = INVMST.ITEM_CODE " & vbCrLf _
        '    & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND ID.ALTER_ITEM_CODE='" & pRGPItemCode & "'"


        SelectQuery = SelectQuery & vbCrLf & " ORDER BY 1 "

        Exit Function
ErrPart:
        SelectQuery = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub FrmADJ_RGP_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        'UltraGrid2.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        Frasprd.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))

        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
