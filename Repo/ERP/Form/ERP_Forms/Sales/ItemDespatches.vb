Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Friend Class frmItemDespatches
    Inherits System.Windows.Forms.Form

    'Friend WithEvents UltraDataSource2 As Infragistics.Win.UltraWinDataSource.UltraDataSource

    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Dim mAccountCode As String
    Private Const ColLocked As Short = 1
    Private Const ColChallanNo As Short = 2
    Private Const ColChallanDate As Short = 3
    Private Const ColBillNo As Short = 4
    Private Const ColBillDate As Short = 5

    Private Const ColPartyCode As Short = 6
    Private Const ColPartyName As Short = 7
    Private Const ColPartyVendorCode As Short = 8
    Private Const ColPartyAddress As Short = 9
    Private Const ColPartyCity As Short = 10
    Private Const ColPartyState As Short = 11
    Private Const ColPartyPIN As Short = 12
    Private Const ColPartyGSTNo As Short = 13

    Private Const ColItemCode As Short = 14
    Private Const ColItemName As Short = 15
    Private Const ColPartNo As Short = 16
    Private Const ColHSNCode As Short = 17

    Private Const ColItemUOM As Short = 18
    Private Const ColQuantity As Short = 19

    Private Const ColPerPiece As Short = 20
    Private Const ColNetWeight As Short = 21
    Private Const ColGrossWeight As Short = 22

    Private Const ColRate As Short = 23
    Private Const ColAmount As Short = 24
    Private Const ColStockQty As Short = 25

    Private Const ColTime As Short = 26
    Private Const ColCGST As Short = 27
    Private Const ColSGST As Short = 28
    Private Const ColIGST As Short = 29
    Private Const ColBillAmount As Short = 30
    Private Const ColPackQty As Short = 31
    Private Const ColVehicleNo As Short = 32

    Private Const ColGRNo As Short = 33
    Private Const ColCarrier As Short = 34
    Private Const ColGRNNo As Short = 35

    Private Const ColGRNDate As Short = 36
    Private Const ColReceiptDate As Short = 37
    Private Const ColApproved As Short = 38

    Private Const ColInvoiceType As Short = 39
    Private Const ColAccountName As Short = 40
    Private Const ColCancel As Short = 41
    Private Const ColAgtD3 As Short = 42
    Private Const ColCustomerPONo As Short = 43
    Private Const ColRefInvoiceNo As Short = 44
    Private Const ColRefInvoiceDate As Short = 45


    Private Const ColSameShipPartyCode As Short = 46
    Private Const ColShipPartyCode As Short = 47
    Private Const ColShipPartyName As Short = 48
    Private Const ColShipPartyAddress As Short = 49
    Private Const ColShipPartyCity As Short = 50
    Private Const ColShipPartyState As Short = 51
    Private Const ColShipPartyPIN As Short = 52
    Private Const ColShipPartyGSTNo As Short = 53
    Private Const ColUnitName As Short = 54
    Private Const ColModUser As Short = 55
    Private Const ColModDate As Short = 56
    Private Const ColAddUser As Short = 57
    Private Const ColAddDate As Short = 58

    Private Const ColThickness As Short = 59
    Private Const ColColor As Short = 60
    Private Const ColSubCategory As Short = 61
    Private Const ColSQM As Short = 62

    Private Const ColMKEY As Short = 63

    Dim mClickProcess As Boolean


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
        cmdCustomReport.Enabled = pPrintEnable
    End Sub

    Private Sub cboAgtD3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAgtD3.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboAgtD3_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAgtD3.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub


    Private Sub cboCancelled_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCancelled.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCancelled_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCancelled.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub


    Private Sub cboCT1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCT1.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCT1_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCT1.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCT3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCT3.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCT3_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCT3.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboExport_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExport.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboExport_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExport.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboFOC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFOC.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub cboFOC_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFOC.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboLocation.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboRejection_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRejection.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboRejection_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRejection.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub


    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtAccount.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub

    Private Sub chkAllCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCategory.CheckStateChanged
        Call PrintStatus(False)
        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCategory.Enabled = False
            cmdsearchCategory.Enabled = False
        Else
            txtCategory.Enabled = True
            cmdsearchCategory.Enabled = True
        End If
    End Sub

    Private Sub chkAllSubCat_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSubCat.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSubCategory.Enabled = False
            cmdSubCatsearch.Enabled = False
        Else
            txtSubCategory.Enabled = True
            cmdSubCatsearch.Enabled = True
        End If
    End Sub
    Private Sub SearchSubCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""

        If txtCategory.Text = "" Then
            MsgInformation("Please Select category .")
            If txtCategory.Enabled = True Then txtCategory.Focus()
            Exit Sub
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mCatCode = MasterNo
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"


        If MainClass.SearchGridMaster((txtSubCategory.Text), "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtSubCategory.Text = AcName
            End If
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub chkAllItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllItem.CheckStateChanged
        Call PrintStatus(False)
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtItemName.Enabled = False
            cmdSearchItem.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdSearchItem.Enabled = True
        End If
    End Sub


    Private Sub chkMonthWise_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call PrintStatus(False)
    End Sub

    Private Sub chkTime_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkTime.CheckStateChanged
        Call PrintStatus(False)
        If chkTime.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTMFrom.Enabled = False
            txtTMTo.Enabled = False
        Else
            txtTMFrom.Enabled = True
            txtTMTo.Enabled = True
        End If
        txtTMFrom.Text = GetServerTime()
        txtTMTo.Text = GetServerTime()
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub ReportonShow(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String = ""
        Dim mSubTitle1 As String = ""

        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mSelected As Boolean

        Report1.Reset()

        mTitle = "Productwise Bill Register"        ''Item Despatches

        mSelected = True
        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                '            mSubTitle = IIf(mSubTitle = "", mInvoiceType, mSubTitle & "/" & mInvoiceType)
            Else
                mSelected = False
            End If
        Next
        If mSelected = True Then
            mSubTitle = ""
        Else
            mSubTitle = " (" & mSubTitle & ")"
        End If

        mSubTitle = "From : " & txtDateFrom.Text & " To : " & txtDateTo.Text & mSubTitle

        If cboAgtD3.SelectedIndex = 1 Then
            mSubTitle1 = "AGT D3"
        End If

        If cboFOC.SelectedIndex = 1 Then
            mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "FOC", "/FOC")
        End If

        If cboRejection.SelectedIndex = 1 Then
            mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "Rejection", "/Rejetion")
        End If

        If cboCancelled.SelectedIndex = 1 Then
            mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "Cancelled", "/Cancelled")
        End If

        If cboCT3.SelectedIndex = 1 Then
            mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "CT3", "/CT3")
        End If

        If cboCT1.SelectedIndex = 1 Then
            mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "CT1", "/CT1")
        End If

        If cboExport.SelectedIndex = 1 Then
            mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "Export", "/Export")
        End If

        mSubTitle = mSubTitle & IIf(mSubTitle1 = "", "", " (" & mSubTitle1 & ")")

        mSubTitle = Mid(mSubTitle, 1, 254)



        'If chkStock.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        If optType(0).Checked = True Then
            If optOrderBy(0).Checked = True Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ItemDespatch.RPT"
            Else
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\IDBillWise.RPT"
            End If
        Else
            'If chkMonthWise.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ItemDespatchSumm.RPT"
            'Else
            '    Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ItemDespMonthSumm.RPT"
            'End If
        End If
        SqlStr = MakeSQL("S")
        'Else
        '    If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr

        '    SqlStr = ""
        '    SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
        '    Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ItemDespatchWithStock.RPT"
        'End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub ReportonCustomShow(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String = ""
        Dim mSubTitle1 As String = ""

        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mSelected As Boolean

        Report1.Reset()

        mTitle = "Despatch Detail"        ''Item Despatches

        mSelected = True
        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                '            mSubTitle = IIf(mSubTitle = "", mInvoiceType, mSubTitle & "/" & mInvoiceType)
            Else
                mSelected = False
            End If
        Next
        If mSelected = True Then
            mSubTitle = ""
        Else
            mSubTitle = " (" & mSubTitle & ")"
        End If

        mSubTitle = "From : " & txtDateFrom.Text & " To : " & txtDateTo.Text & IIf(chkAll.Checked = True, "", " (" & TxtAccount.Text & ")")

        'mSubTitle = mSubTitle & IIf(mSubTitle1 = "", "", " (" & mSubTitle1 & ")")

        'mSubTitle = Mid(mSubTitle, 1, 254)

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ItemDespatchSamsung.RPT"


        'SqlStr = MakeSQL("S")

        If InsertIntoPrintdummyData() = False Then GoTo ReportErr

        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mRow As UltraGridRow
        Dim mFieldStr1 As String
        Dim mFieldStr2 As String

        Dim mValueStr1 As String
        Dim mValueStr2 As String
        Dim mDepatchedQty As Double

        Dim mMkey As String
        Dim mUpdateQry As String
        Dim mItemCode As String
        Dim pCustomerCode As String
        Dim pInvoiceDate As String
        Dim SODate As String
        Dim mDSQty As Double

        With UltraGrid1
            For cntRow = 0 To .DisplayLayout.Rows.Count - 1
                mRow = Me.UltraGrid1.Rows(cntRow)
                mMkey = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1))
                mItemCode = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1))
                pCustomerCode = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyCode - 1))
                pInvoiceDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDate - 1))

                SODate = ""
                If MainClass.ValidateWithMasterTable(mMkey, "MKEY", "OUR_SO_DATE", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "") = True Then
                    SODate = Val(MasterNo)
                Else
                    SODate = ""
                End If

                mDSQty = GetSalesDSQty(mItemCode, mMkey, pCustomerCode, pInvoiceDate)
                mValueStr1 = VB6.Format(mDSQty, "0.00")

                mDepatchedQty = GetTotMonthDespatchQty(mItemCode, mMkey, pCustomerCode, pInvoiceDate)
                mValueStr2 = VB6.Format(mDSQty - mDepatchedQty, "0.00")

                SqlStr = " UPDATE TEMP_PRINTDUMMYDATA SET " & vbCrLf _
                    & " FIELD51='" & mValueStr1 & "', FIELD52='" & mValueStr2 & "', FIELD53='" & SODate & "'" & vbCrLf _
                    & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' AND SUBROW=" & cntRow + 1 & " "

                PubDBCn.Execute(SqlStr)
            Next
        End With


        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Function GetSalesDSQty(ByRef pItemCode As String, pMKey As String, pCustomerCode As String, pDate As String) As Double

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mOrderType As String
        Dim mSONo As Double

        GetSalesDSQty = 0

        If MainClass.ValidateWithMasterTable(pMKey, "MKEY", "OUR_AUTO_KEY_SO", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "") = True Then
            mSONo = Val(MasterNo)
        Else
            mSONo = -1
        End If

        If MainClass.ValidateWithMasterTable(Val(mSONo), "AUTO_KEY_SO", "ORDER_TYPE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "SUPP_CUST_CODE='" & Trim(pCustomerCode) & "' AND SO_APPROVED='Y'") = True Then
            mOrderType = MasterNo
        Else
            mOrderType = "O"
        End If


        'If mDIRequired = "N" Then

        mSqlStr = " SELECT SUM(ITEM_QTY) AS ITEM_QTY " & vbCrLf _
                & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DELV_SCHLD_DET ID " & vbCrLf _
                & " WHERE IH.AUTO_KEY_DELV = ID.AUTO_KEY_DELV" & vbCrLf _
                & " --AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE='" & Trim(pCustomerCode) & "'" & vbCrLf _
                & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf _
                & " AND IH.AUTO_KEY_SO=" & Val(mSONo) & ""


        If mOrderType = "C" Then
            '        mSqlStr = mSqlStr & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'YYYYMM')<='" & VB6.Format(txtDNDate, "YYYYMM") & "'"
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'YYYYMM')='" & VB6.Format(pDate, "YYYYMM") & "'"
        End If


        'Else
        '    mSqlStr = " SELECT SUM(PLANNED_QTY) AS ITEM_QTY " & vbCrLf _
        '            & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID " & vbCrLf _
        '            & " WHERE IH.AUTO_KEY_DELV = ID.AUTO_KEY_DELV" & vbCrLf _
        '            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '            & " AND IH.SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "'" & vbCrLf _
        '            & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf _
        '            & " AND IH.AUTO_KEY_SO=" & Val(txtSONo.Text) & ""

        '    If mODNo = "" Then
        '        mSqlStr = mSqlStr & vbCrLf & " AND (OD_NO='' OR OD_NO IS NULL)"
        '    Else
        '        mSqlStr = mSqlStr & vbCrLf & " AND OD_NO='" & mODNo & "'"
        '    End If

        '    'If mOrderType = "C" Then
        '    '    '        mSqlStr = mSqlStr & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'YYYYMM')<='" & VB6.Format(txtDNDate, "YYYYMM") & "'"
        '    'Else
        '    '    mSqlStr = mSqlStr & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'YYYYMM')='" & VB6.Format(txtDNDate.Text, "YYYYMM") & "'"
        '    'End If
        'End If

        'If mStoreLoc = "" Then
        '    mSqlStr = mSqlStr & vbCrLf & " AND (LOC_CODE='' OR LOC_CODE IS NULL)"
        'Else
        '    mSqlStr = mSqlStr & vbCrLf & " AND LOC_CODE='" & mStoreLoc & "'"
        'End If

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetSalesDSQty = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00"))
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetTotMonthDespatchQty(ByRef pItemCode As String, ByRef pMKey As String, ByRef pCustomerCode As String, ByRef pDate As String) As Double

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mOrderType As String
        Dim mSONo As Double

        GetTotMonthDespatchQty = 0

        If MainClass.ValidateWithMasterTable(pMKey, "MKEY", "OUR_AUTO_KEY_SO", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "") = True Then
            mSONo = Val(MasterNo)
        Else
            mSONo = -1
        End If


        If MainClass.ValidateWithMasterTable(Val(mSONo), "AUTO_KEY_SO", "ORDER_TYPE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "SUPP_CUST_CODE='" & Trim(pCustomerCode) & "' AND SO_APPROVED='Y'") = True Then
            mOrderType = MasterNo
        Else
            mOrderType = "O"
        End If

        mSqlStr = " SELECT SUM(PACKED_QTY) AS ITEM_QTY " & vbCrLf _
            & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID " & vbCrLf _
            & " WHERE IH.AUTO_KEY_DESP = ID.AUTO_KEY_DESP" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & Trim(pCustomerCode) & "'" & vbCrLf _
            & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'"

        mSqlStr = mSqlStr & " AND IH.DESP_TYPE IN ('G','P','S')  AND DESP_STATUS<>2 "   ''ID.STOCK_TYPE='FG'


        If mOrderType = "C" Then
            mSqlStr = mSqlStr & " AND IH.AUTO_KEY_SO=" & Val(mSONo) & " "
        Else
            mSqlStr = mSqlStr & " AND TO_CHAR(IH.DESP_DATE,'YYYYMM')='" & VB6.Format(pDate, "YYYYMM") & "' "
        End If

        mSqlStr = mSqlStr & " AND IH.DESP_DATE<=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetTotMonthDespatchQty = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00"))
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function InsertIntoPrintdummyData() As Boolean

        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mInsertSQL As String
        Dim mValueSQL As String
        Dim mFieldStr As String
        Dim mValueStr As String

        Dim mRow As UltraGridRow
        Dim mValue As String

        'Dim mCol As UltraGridCol

        On Error GoTo ERR1

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PRINTDUMMYDATA WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        With UltraGrid1
            For cntRow = 0 To .DisplayLayout.Rows.Count - 1
                mRow = Me.UltraGrid1.Rows(cntRow)

                mInsertSQL = ""
                mValueSQL = ""
                SqlStr = ""

                mInsertSQL = "Insert into TEMP_PrintDummyData (UserID,SubRow,"
                mValueSQL = " Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow + 1 & ", "

                For cntCol = 0 To .DisplayLayout.Bands(0).Columns.Count - 1
                    mValue = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(cntCol))

                    If cntCol = .DisplayLayout.Bands(0).Columns.Count - 1 Then
                        mFieldStr = "FIELD" & cntCol + 1
                        mValueStr = "'" & mValue & "'"
                    Else
                        mFieldStr = "FIELD" & cntCol + 1 & ","
                        mValueStr = "'" & mValue & "'" & ","
                    End If
                    mInsertSQL = mInsertSQL & mFieldStr
                    mValueSQL = mValueSQL & mValueStr


                Next
                mInsertSQL = mInsertSQL & ")"
                mValueSQL = mValueSQL & ")"

                SqlStr = mInsertSQL & vbCrLf & mValueSQL
                PubDBCn.Execute(SqlStr)
            Next
        End With
        PubDBCn.CommitTrans()
        InsertIntoPrintdummyData = True
        Exit Function
ERR1:
        'Resume	
        PubDBCn.RollbackTrans()
        InsertIntoPrintdummyData = False
        MsgInformation(Err.Description)
    End Function
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonShow(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub

    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCategory.Click
        SearchCategory()
    End Sub

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchItem.Click
        SearchItem()
    End Sub


    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        'MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1("S") = False Then GoTo ErrPart
        Call PrintStatus(True)

        'FormatSprdMain(-1)
        Call CalcTots()

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        'FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CalcTots()

        On Error GoTo ERR1
        Dim mTotQty As Double
        Dim mTotItemAmount As Double
        Dim mTotEDAmount As Double
        Dim mTotCessAmount As Double
        Dim mTotSHCessAmount As Double
        Dim mTotSalesTaxAmount As Double
        Dim mTotServiceAmount As Double
        Dim mTotCGSTAmount As Double
        Dim mTotSGSTAmount As Double
        Dim mTotIGSTAmount As Double

        Dim I As Integer
        Dim j As Integer
        Dim mItemCode As String


        mTotQty = 0
        mTotItemAmount = 0
        mTotEDAmount = 0
        mTotCessAmount = 0
        mTotSHCessAmount = 0
        mTotSalesTaxAmount = 0
        mTotServiceAmount = 0
        mTotCGSTAmount = 0
        mTotSGSTAmount = 0
        mTotIGSTAmount = 0

        '        With SprdMain
        '            j = .MaxRows
        '            For I = 1 To j
        '                .Row = I

        '                .Col = ColItemCode
        '                If .Text = "" Then GoTo DontCalc
        '                mItemCode = .Text

        '                .Col = ColQuantity
        '                mTotQty = mTotQty + Val(.Text)

        '                .Col = ColAmount
        '                mTotItemAmount = mTotItemAmount + Val(.Text)

        '                '.Col = ColExciseDuty
        '                'mTotEDAmount = mTotEDAmount + Val(.Text)

        '                '.Col = ColCessTax
        '                'mTotCessAmount = mTotCessAmount + Val(.Text)

        '                '.Col = ColSHCessTax
        '                'mTotSHCessAmount = mTotSHCessAmount + Val(.Text)

        '                '.Col = ColSalesTax
        '                'mTotSalesTaxAmount = mTotSalesTaxAmount + Val(.Text)

        '                '.Col = ColServiceTax
        '                'mTotServiceAmount = mTotServiceAmount + Val(.Text)

        '                .Col = ColCGST
        '                mTotCGSTAmount = mTotCGSTAmount + Val(.Text)

        '                .Col = ColSGST
        '                mTotSGSTAmount = mTotSGSTAmount + Val(.Text)

        '                .Col = ColIGST
        '                mTotIGSTAmount = mTotIGSTAmount + Val(.Text)

        'DontCalc:
        '            Next I

        '            Call MainClass.AddBlankfpSprdRow(SprdMain, ColItemCode)

        '            .Col = ColItemName
        '            .Row = .MaxRows
        '            .Text = "GRAND TOTAL :"
        '            .Font = VB6.FontChangeBold(.Font, True)

        '            .Row = .MaxRows
        '            .Row2 = .MaxRows
        '            .Col = 1
        '            .col2 = .MaxCols
        '            .BlockMode = True
        '            .BackColor = System.Drawing.ColorTranslator.FromOle(&H80FF80)
        '            .BlockMode = False

        '            .Row = .MaxRows

        '            .Col = ColQuantity
        '            .Text = VB6.Format(mTotQty, "0.00")
        '            .Font = VB6.FontChangeBold(.Font, True)

        '            .Col = ColAmount
        '            .Text = VB6.Format(mTotItemAmount, "0.00")
        '            .Font = VB6.FontChangeBold(.Font, True)

        '            '.Col = ColExciseDuty
        '            '.Text = VB6.Format(mTotEDAmount, "0.00")
        '            '.Font = VB6.FontChangeBold(.Font, True)

        '            '.Col = ColCessTax
        '            '.Text = VB6.Format(mTotCessAmount, "0.00")
        '            '.Font = VB6.FontChangeBold(.Font, True)

        '            '.Col = ColSHCessTax
        '            '.Text = VB6.Format(mTotSHCessAmount, "0.00")
        '            '.Font = VB6.FontChangeBold(.Font, True)

        '            '.Col = ColSalesTax
        '            '.Text = VB6.Format(mTotSalesTaxAmount, "0.00")
        '            '.Font = VB6.FontChangeBold(.Font, True)

        '            '.Col = ColServiceTax
        '            '.Text = VB6.Format(mTotServiceAmount, "0.00")
        '            '.Font = VB6.FontChangeBold(.Font, True)

        '            .Col = ColCGST
        '            .Text = VB6.Format(mTotCGSTAmount, "0.00")
        '            .Font = VB6.FontChangeBold(.Font, True)

        '            .Col = ColSGST
        '            .Text = VB6.Format(mTotSGSTAmount, "0.00")
        '            .Font = VB6.FontChangeBold(.Font, True)

        '            .Col = ColIGST
        '            .Text = VB6.Format(mTotIGSTAmount, "0.00")
        '            .Font = VB6.FontChangeBold(.Font, True)

        '        End With

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub cmdSubCatsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSubCatsearch.Click
        SearchSubCategory()
    End Sub

    Private Sub frmItemDespatches_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Item Despatch"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmItemDespatches_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        'chkMonthWise.Enabled = False

        cboAgtD3.Items.Clear()
        cboFOC.Items.Clear()
        cboRejection.Items.Clear()
        cboCancelled.Items.Clear()
        cboExport.Items.Clear()
        cboCT3.Items.Clear()
        cboCT1.Items.Clear()
        cboLocation.Items.Clear()

        cboAgtD3.Items.Add("BOTH")
        cboAgtD3.Items.Add("YES")
        cboAgtD3.Items.Add("NO")

        cboFOC.Items.Add("BOTH")
        cboFOC.Items.Add("YES")
        cboFOC.Items.Add("NO")

        cboRejection.Items.Add("BOTH")
        cboRejection.Items.Add("YES")
        cboRejection.Items.Add("NO")

        cboCancelled.Items.Add("BOTH")
        cboCancelled.Items.Add("YES")
        cboCancelled.Items.Add("NO")

        cboExport.Items.Add("BOTH")
        cboExport.Items.Add("YES")
        cboExport.Items.Add("NO")

        cboCT3.Items.Add("BOTH")
        cboCT3.Items.Add("YES")
        cboCT3.Items.Add("NO")

        cboCT1.Items.Add("BOTH")
        cboCT1.Items.Add("YES")
        cboCT1.Items.Add("NO")

        cboAgtD3.SelectedIndex = 0
        cboFOC.SelectedIndex = 0
        cboRejection.SelectedIndex = 0
        cboCancelled.SelectedIndex = 2
        cboExport.SelectedIndex = 0
        cboCT3.SelectedIndex = 0
        cboCT1.SelectedIndex = 0

        cboInvoiceType.Items.Clear()
        cboInvoiceType.Items.Add("All")
        cboInvoiceType.Items.Add("1. Tax Invoice")
        cboInvoiceType.Items.Add("2. Jobwork Invoice")
        cboInvoiceType.Items.Add("3. Delivery Challan")
        cboInvoiceType.Items.Add("4. Service / Rental")
        cboInvoiceType.Items.Add("5. Delivery Challan Supp. Invoice (Internal Memo)")
        cboInvoiceType.Items.Add("6. Tax Invoice Export")
        '    cboInvoiceType.AddItem "7. Reverse Charge - Goods"
        '    cboInvoiceType.AddItem "8. Reverse Charge - Services"
        cboInvoiceType.Items.Add("9. Supplementary Invoice")
        cboInvoiceType.Items.Add("0. Bill of Supply")
        cboInvoiceType.SelectedIndex = 0

        Call FillInvoiceType()



        lblTrnType.Text = CStr(-1)
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False
        txtItemName.Enabled = False
        cmdsearchItem.Enabled = False

        txtCategory.Enabled = False
        cmdsearchCategory.Enabled = False
        txtSubCategory.Enabled = False
        cmdSubCatsearch.Enabled = False
        'txtPONo.Enabled = False
        'cmdSearchPONo.Enabled = False

        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtInTransitAsonDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        If Show1("L") = False Then GoTo BSLError     ''CreateGridHeader("L")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmItemDespatches_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmItemDespatches_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub lstInvoiceType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstInvoiceType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub optType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optType.GetIndex(eventSender)
            Call PrintStatus(False)
            'chkMonthWise.Enabled = IIf(Index = 0, False, True)
        End If
    End Sub

    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub

    Private Sub txtCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.DoubleClick
        SearchCategory()
    End Sub
    Private Sub txtCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCategory()
    End Sub
    Private Sub txtCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtCategory.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtCategory.Text = UCase(Trim(txtCategory.Text))
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
    Private Sub SearchCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.SearchGridMaster((txtCategory.Text), "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtCategory.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtInTransitAsonDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInTransitAsonDate.TextChanged
        Call PrintStatus(False)
    End Sub
    ''
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        '''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        'MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "CUSTOMER_PART_NO", , SqlStr)
        'If AcName <> "" Then
        '    txtItemName.Text = AcName
        'End If

        SqlStr = " SELECT DISTINCT ITEMMST.ITEM_SHORT_DESC, ID.ITEM_CODE,  ID.CUSTOMER_PART_NO "

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, INV_ITEM_MST ITEMMST"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " And ID.COMPANY_CODE=ITEMMST.COMPANY_CODE" & vbCrLf _
            & " And ID.ITEM_CODE=ITEMMST.ITEM_CODE"



        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
            End If
        End If

        MainClass.SearchGridMasterBySQL2(txtItemName.Text, SqlStr)
        If AcName <> "" Then
            txtItemName.Text = AcName
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub TxtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub
    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
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
    Private Function Show1(mType As String) As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mStock As Double
        Dim mUOM As String = ""

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL(mType)
        'MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        FillUltraGrid(SqlStr)

        'If chkStock.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    With SprdMain
        '        For cntRow = 1 To .MaxRows
        '            .Row = cntRow
        '            .Col = ColItemCode
        '            mItemCode = Trim(.Text)

        '            If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                mUOM = MasterNo
        '            End If

        '            mStock = GetBalanceStockQty(mItemCode, (txtDateTo.Text), mUOM, "PAD", "FG", "", ConWH, -1)
        '            .Col = ColStockQty
        '            .Text = VB6.Format(mStock, "0.00")
        '        Next
        '    End With
        'End If
        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function MakeSQLCUSTOM(mType As String) As String

        On Error GoTo ERR1
        Dim mTrnCode As String
        Dim mTrnTypeStr As String = ""
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mCatCode As String = ""
        Dim mSubCatCode As String
        Dim mAccountCode As String
        Dim mItemCode As String
        Dim mShowAll As Boolean
        Dim mDivision As Double

        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String

        ''SELECT CLAUSE...

        If optType(0).Checked = True Then
            MakeSQLCUSTOM = " SELECT IH.COMPANY_CODE, IH.AUTO_KEY_DESP,IH.DCDATE,  " & vbCrLf _
                & " IH.BILLNO, TO_CHAR(IH.INVOICE_DATE,'DD/MM/YYYY') AS INVOICE_DATE," & vbCrLf _
                & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, IH.VENDOR_CODE," & vbCrLf _
                & " CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, CMST.SUPP_CUST_PIN, CMST.GST_RGN_NO, " & vbCrLf _
                & " ID.ITEM_CODE, ITEMMST.ITEM_SHORT_DESC, ID.CUSTOMER_PART_NO, ID.HSNCODE,  ID.ITEM_UOM, " & vbCrLf _
                & " ID.ITEM_QTY, ID.ITEM_RATE, (ID.ITEM_QTY * ID.ITEM_RATE) AS AMOUNT, " & vbCrLf _
                & " '', To_CHAR(IH.REMOVAL_TIME,'HH24:MI'), " & vbCrLf _
                & " CASE WHEN IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE CGST_AMOUNT END AS CGST_AMOUNT, " & vbCrLf _
                & " CASE WHEN IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE SGST_AMOUNT END AS SGST_AMOUNT, " & vbCrLf _
                & " CASE WHEN IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE IGST_AMOUNT END As IGST_AMOUNT, " & vbCrLf _
                & " (ID.ITEM_QTY * ID.ITEM_RATE) + CASE WHEN IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE CGST_AMOUNT+SGST_AMOUNT+IGST_AMOUNT END As NET_AMOUNT, " & vbCrLf _
                & " ID.INNER_PACK_QTY ||  ' ' || ID.PACK_TYPE AS PACK_QTY, " & vbCrLf _
                & " IH.VEHICLENO, IH.GRNO, IH.CARRIERS, IH.GRNNO, IH.GRNDATE, '' AS RECEIPTDATE, 'APPROVED' AS APPROVED, " & vbCrLf _
                & " INVMST.NAME, ACCTMST.SUPP_CUST_NAME,DECODE(IH.CANCELLED,'N','NO','YES') AS CANCELLED, " & vbCrLf _
                & " DECODE(IH.REF_DESP_TYPE,'S','YES','NO') AS AGTD3, IH.CUST_PO_NO, '', '', IH.MODDATE , IH.MODUSER, IH.ADDDATE, IH.ADDUSER, IH.MKEY "


        Else
            'If chkMonthWise.CheckState = System.Windows.Forms.CheckState.Checked Then
            '    MakeSQLCUSTOM = " SELECT TO_CHAR(IH.INVOICE_DATE,'MM-YYYY'),"
            'Else
            MakeSQLCUSTOM = " SELECT '',"
            'End If

            MakeSQLCUSTOM = " SELECT IH.COMPANY_CODE, '','',  " & vbCrLf _
                & " '', '' AS INVOICE_DATE," & vbCrLf _
                & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, IH.VENDOR_CODE," & vbCrLf _
                & " CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, CMST.SUPP_CUST_PIN, CMST.GST_RGN_NO, " & vbCrLf _
                & " ID.ITEM_CODE, ITEMMST.ITEM_SHORT_DESC, ID.CUSTOMER_PART_NO, ID.HSNCODE,  ID.ITEM_UOM, " & vbCrLf _
                & " TO_CHAR(SUM(ID.ITEM_QTY)) AS ITEM_QTY, ID.ITEM_RATE, SUM(ID.ITEM_QTY) * ID.ITEM_RATE AS  AMOUNT, " & vbCrLf _
                & " '', '', " & vbCrLf _
                & " SUM(CASE WHEN IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE CGST_AMOUNT END) AS CGST_AMOUNT, " & vbCrLf _
                & " SUM(CASE WHEN IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE SGST_AMOUNT END) AS SGST_AMOUNT, " & vbCrLf _
                & " SUM(CASE WHEN IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE IGST_AMOUNT END) AS IGST_AMOUNT, " & vbCrLf _
                & " SUM((ID.ITEM_QTY * ID.ITEM_RATE) + CASE WHEN IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE CGST_AMOUNT+SGST_AMOUNT+IGST_AMOUNT END) As NET_AMOUNT, " & vbCrLf _
                & " '' AS PACK_QTY, " & vbCrLf _
                & " '', '', '', '', '', '' AS RECEIPTDATE, '' AS APPROVED, " & vbCrLf _
                & " INVMST.NAME, ACCTMST.SUPP_CUST_NAME,DECODE(IH.CANCELLED,'N','NO','YES') AS CANCELLED, " & vbCrLf _
                & " '' AS AGTD3, '', '', '', '' , '', '', '', '' "

        End If

        ''FROM CLAUSE...
        MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, DSP_DELV_SCHLD_HDR DIH, DSP_DAILY_SCHLD_DET DID, FIN_SUPP_CUST_MST ACM,  " & vbCrLf _
            & " FIN_SUPP_CUST_BUSINESS_MST CMST, INV_ITEM_MST ITEMMST, FIN_INVTYPE_MST INVMST, FIN_SUPP_CUST_MST ACCTMST"

        ''WHERE CLAUSE...
        MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY"

        If CDate(txtDateFrom.Text) >= CDate(RsCompany.Fields("START_DATE").Value) And CDate(txtDateFrom.Text) <= CDate(RsCompany.Fields("END_DATE").Value) Then
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & " AND IH.FYEAR='" & RsCompany.Fields("FYEAR").Value & "'"
        End If

        MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf _
            & " And IH.COMPANY_CODE = ACM.COMPANY_CODE" & vbCrLf _
            & " And IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE" & vbCrLf _
            & " And IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " And IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " And IH.BILL_TO_LOC_ID=CMST.LOCATION_ID" & vbCrLf _
            & " And IH.COMPANY_CODE=ACCTMST.COMPANY_CODE" & vbCrLf _
            & " And IH.ACCOUNTCODE=ACCTMST.SUPP_CUST_CODE" & vbCrLf _
            & " And ID.COMPANY_CODE=ITEMMST.COMPANY_CODE" & vbCrLf _
            & " And ID.ITEM_CODE=ITEMMST.ITEM_CODE" & vbCrLf _
            & " And IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " And IH.TRNTYPE=INVMST.CODE"     '' AND IH.ITEMVALUE<>0"


        MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf _
            & " And DIH.AUTO_KEY_DELV = DID.AUTO_KEY_DELV" & vbCrLf _
            & " And IH.COMPANY_CODE=DIH.COMPANY_CODE" & vbCrLf _
            & " And IH.SUPP_CUST_CODE=DIH.SUPP_CUST_CODE" & vbCrLf _
            & " And IH.OUR_AUTO_KEY_SO=DIH.AUTO_KEY_SO" & vbCrLf _
            & " And ID.ITEM_CODE=DID.ITEM_CODE"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
                MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND CMST.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
            End If
        End If

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND TRIM(ID.ITEM_CODE)='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        'If chkAllPONo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPONo.Text) <> "" Then
        '    MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND IH.OUR_AUTO_KEY_SO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "'"
        'End If

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
                MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND ITEMMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
                mSubCatCode = MasterNo
                MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND ITEMMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        '    If cboInvoiceType.Text = "ALL" Then
        '        MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND INVMST.IDENTIFICATION<>'J'"
        '    Else
        '        MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND IH.TRNTYPE=" & Val(lblTrnType.text) & ""
        '    End If

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & " AND IH.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        mShowAll = True
        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                '            lstInvoiceType.ListIndex = CntLst
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = "'" & IIf(IsDBNull(MasterNo), "", MasterNo) & "'"
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            Else
                mShowAll = False
            End If
        Next

        If mShowAll = False Then
            If mTrnTypeStr <> "" Then
                mTrnTypeStr = "(" & mTrnTypeStr & ")"
                MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & " AND ID.TRNTYPE IN " & mTrnTypeStr & ""
            End If
        End If

        If cboInvoiceType.SelectedIndex > 0 Then
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND IH.INVOICESEQTYPE='" & VB.Left(cboInvoiceType.Text, 1) & "'"
        End If

        MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND NVL(IH.INVOICESEQTYPE,-1) NOT IN (7,8) "

        If cboAgtD3.SelectedIndex > 0 Then
            '        MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND IH.AGTD3='" & vb.Left(cboAgtD3.Text, 1) & "'"
            If cboAgtD3.SelectedIndex = 1 Then
                MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & " AND IH.REF_DESP_TYPE ='S'"
            Else
                MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & " AND IH.REF_DESP_TYPE <>'S'"
            End If
        End If

        ''
        If cboCustomerGroup.SelectedIndex > 0 Then
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND ACM.CUSTOMER_GROUP='" & cboCustomerGroup.Text & "'"
        End If

        If cboFOC.SelectedIndex > 0 Then
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND IH.FOC='" & VB.Left(cboFOC.Text, 1) & "'"
        End If

        If cboRejection.SelectedIndex > 0 Then
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND IH.REJECTION='" & VB.Left(cboRejection.Text, 1) & "'"
        End If

        If cboCT3.SelectedIndex > 0 Then
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND IH.AGTCT3='" & VB.Left(cboCT3.Text, 1) & "'"
        End If

        If cboCT1.SelectedIndex > 0 Then
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND IH.AGTCT1='" & VB.Left(cboCT1.Text, 1) & "'"
        End If

        If cboCancelled.SelectedIndex > 0 Then
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND IH.CANCELLED='" & VB.Left(cboCancelled.Text, 1) & "'"
        End If

        'If Trim(txtTariffHeading.Text) <> "" Then
        '    MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND IH.TARIFFHEADING='" & MainClass.AllowSingleQuote(txtTariffHeading.Text) & "'"
        'End If

        If Trim(txtVehicleNo.Text) <> "" Then
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND IH.VEHICLENO='" & MainClass.AllowSingleQuote(txtVehicleNo.Text) & "'"
        End If

        If cboExport.SelectedIndex = 1 Then
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND CMST.WITHIN_COUNTRY='N'"
        ElseIf cboExport.SelectedIndex = 2 Then
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND CMST.WITHIN_COUNTRY='Y'"
        End If

        If cboLocation.SelectedIndex > 0 Then
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "AND IH.BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(cboLocation.Text) & "'"
        End If

        '    If chkTime.Value = vbUnchecked Then
        '        MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf _
        ''                & " AND IH.REMOVAL_TIME>=TO_DATE('" & txtTMFrom.Text & "', 'HH24:MI')" & vbCrLf _
        ''                & " AND IH.REMOVAL_TIME<=TO_DATE('" & txtTMTo.Text & "', 'HH24:MI')"
        '
        '    End If

        If chkTime.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & " AND TO_CHAR(IH.INVOICE_DATE,'YYYYMMDD')||TO_CHAR(IH.REMOVAL_TIME,'HH24MI')>=TO_CHAR('" & VB6.Format(txtDateFrom.Text, "YYYYMMDD") & "') || '" & VB6.Format(txtTMFrom.Text, "HHMM") & "'" & vbCrLf & " AND TO_CHAR(IH.INVOICE_DATE,'YYYYMMDD')||TO_CHAR(IH.REMOVAL_TIME,'HH24MI')<=TO_CHAR('" & VB6.Format(txtDateTo.Text, "YYYYMMDD") & "') || '" & VB6.Format(txtTMTo.Text, "HHMM") & "'"
        End If

        '' GROUP BY CLAUSE
        If optType(1).Checked = True Then
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & " GROUP BY "
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf _
                & " IH.COMPANY_CODE, CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME,  IH.VENDOR_CODE, " & vbCrLf _
                & " CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, CMST.SUPP_CUST_PIN, CMST.GST_RGN_NO, " & vbCrLf _
                & " ID.ITEM_CODE , ITEMMST.ITEM_SHORT_DESC, ID.HSNCODE, ID.ITEM_RATE,ID.CUSTOMER_PART_NO,ID.ITEM_UOM,INVMST.NAME, ACCTMST.SUPP_CUST_NAME,DECODE(IH.CANCELLED,'N','NO','YES') "
        End If


        If mType = "L" Then
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & " AND 1=2"
        End If
        ''ORDER BY CLAUSE...

        If optType(0).Checked = True Then
            'If optOrderBy(0).Checked = True Then
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "ORDER BY ITEMMST.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME,IH.BILLNO, IH.INVOICE_DATE"
            'Else
            '    MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "ORDER BY IH.BILLNO,CMST.SUPP_CUST_NAME,ITEMMST.ITEM_SHORT_DESC, IH.INVOICE_DATE"
            'End If
        Else
            '        If optOrderBy(0).Value = True Then
            '            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "ORDER BY ID.ITEM_DESC,CMST.SUPP_CUST_NAME"
            '        Else
            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & "ORDER BY "

            'If chkMonthWise.CheckState = System.Windows.Forms.CheckState.Checked Then
            '    MakeSQLCUSTOM = MakeSQLCUSTOM & "TO_CHAR(IH.INVOICE_DATE,'MM-YYYY'), "
            'End If

            MakeSQLCUSTOM = MakeSQLCUSTOM & vbCrLf & " CMST.SUPP_CUST_NAME,ID.ITEM_CODE , ITEMMST.ITEM_SHORT_DESC,ID.ITEM_RATE"
            '        End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQL(mType As String) As String

        On Error GoTo ERR1
        Dim mTrnCode As String
        Dim mTrnTypeStr As String = ""
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mCatCode As String = ""
        Dim mSubCatCode As String
        Dim mAccountCode As String
        Dim mItemCode As String
        Dim mShowAll As Boolean
        Dim mDivision As Double

        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String

        ''SELECT CLAUSE...

        If optType(0).Checked = True Then
            MakeSQL = " SELECT IH.COMPANY_CODE, IH.AUTO_KEY_DESP,IH.DCDATE,  " & vbCrLf _
                & " IH.BILLNO, TO_CHAR(IH.INVOICE_DATE,'DD/MM/YYYY') AS INVOICE_DATE," & vbCrLf _
                & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, IH.VENDOR_CODE," & vbCrLf _
                & " CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, CMST.SUPP_CUST_PIN, CMST.GST_RGN_NO, " & vbCrLf _
                & " ID.ITEM_CODE, ITEMMST.ITEM_SHORT_DESC, ID.CUSTOMER_PART_NO, ID.HSNCODE,  ID.ITEM_UOM, " & vbCrLf _
                & " ID.ITEM_QTY,ITEMMST.ITEM_WEIGHT AS PER_PIECE,((ID.ITEM_QTY * ITEMMST.ITEM_WEIGHT)/1000) AS NET_WEIGHT,((ID.ITEM_QTY *(ITEMMST.ITEM_WEIGHT + ITEMMST.SHEAR_SCRAP_WGT))/1000) AS GROSS_WEIGHT, ID.ITEM_RATE, (ID.ITEM_QTY * ID.ITEM_RATE) AS AMOUNT, " & vbCrLf _
                & " '', To_CHAR(IH.REMOVAL_TIME,'HH24:MI'), " & vbCrLf _
                & " CASE WHEN IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE CGST_AMOUNT END AS CGST_AMOUNT, " & vbCrLf _
                & " CASE WHEN IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE SGST_AMOUNT END AS SGST_AMOUNT, " & vbCrLf _
                & " CASE WHEN IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE IGST_AMOUNT END As IGST_AMOUNT, " & vbCrLf _
                & " (ID.ITEM_QTY * ID.ITEM_RATE) + CASE WHEN IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE CGST_AMOUNT+SGST_AMOUNT+IGST_AMOUNT END As NET_AMOUNT, " & vbCrLf _
                & " ID.INNER_PACK_QTY ||  ' ' || ID.PACK_TYPE AS PACK_QTY, " & vbCrLf _
                & " IH.VEHICLENO, IH.GRNO, IH.CARRIERS, IH.GRNNO, IH.GRNDATE, '' AS RECEIPTDATE, 'APPROVED' AS APPROVED, " & vbCrLf _
                & " INVMST.NAME, ACCTMST.SUPP_CUST_NAME,DECODE(IH.CANCELLED,'N','NO','YES') AS CANCELLED, " & vbCrLf _
                & " DECODE(IH.REF_DESP_TYPE,'S','YES','NO') AS AGTD3, IH.CUST_PO_NO, IH.OUR_AUTO_KEY_SO,IH.OUR_SO_DATE, " & vbCrLf _
                & " DECODE(IH.SHIPPED_TO_SAMEPARTY,'Y','YES','NO') AS SHIPPED_TO_SAMEPARTY, CSMST.SUPP_CUST_CODE,CSMST.SUPP_CUST_NAME, " & vbCrLf _
                & " CSMST.SUPP_CUST_ADDR, CSMST.SUPP_CUST_CITY, CSMST.SUPP_CUST_STATE, CSMST.SUPP_CUST_PIN, CSMST.GST_RGN_NO, " & vbCrLf _
                & " GMST.COMPANY_SHORTNAME, IH.MODDATE , IH.MODUSER, IH.ADDDATE, IH.ADDUSER, " & vbCrLf _
                & " ITEMMST.MAT_THICHNESS, ITEMMST.ITEM_COLOR, SUBMST.SUBCATEGORY_DESC, (ID.CHARGEABLEGLASS_AREA * ID.ITEM_QTY) AS SALEQTYSQM," & vbCrLf _
                & " IH.MKEY" & vbCrLf

        Else
            'If chkMonthWise.CheckState = System.Windows.Forms.CheckState.Checked Then
            '    MakeSQL = " SELECT TO_CHAR(IH.INVOICE_DATE,'MM-YYYY'),"
            'Else
            MakeSQL = " SELECT '',"
            'End If

            MakeSQL = " SELECT IH.COMPANY_CODE, '','',  " & vbCrLf _
                & " '', '' AS INVOICE_DATE," & vbCrLf _
                & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, IH.VENDOR_CODE," & vbCrLf _
                & " CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, CMST.SUPP_CUST_PIN, CMST.GST_RGN_NO, " & vbCrLf _
                & " ID.ITEM_CODE, ITEMMST.ITEM_SHORT_DESC, ID.CUSTOMER_PART_NO, ID.HSNCODE,  ID.ITEM_UOM, " & vbCrLf _
                & " TO_CHAR(SUM(ID.ITEM_QTY)) AS ITEM_QTY,ITEMMST.ITEM_WEIGHT AS PER_PIECE,((ID.ITEM_QTY * ITEMMST.ITEM_WEIGHT)/1000) AS NET_WEIGHT,((ID.ITEM_QTY *(ITEMMST.ITEM_WEIGHT + ITEMMST.SHEAR_SCRAP_WGT))/1000) AS GROSS_WEIGHT, ID.ITEM_RATE, SUM(ID.ITEM_QTY) * ID.ITEM_RATE AS  AMOUNT, " & vbCrLf _
                & " '', '', " & vbCrLf _
                & " SUM(CASE WHEN IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE CGST_AMOUNT END) AS CGST_AMOUNT, " & vbCrLf _
                & " SUM(CASE WHEN IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE SGST_AMOUNT END) AS SGST_AMOUNT, " & vbCrLf _
                & " SUM(CASE WHEN IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE IGST_AMOUNT END) AS IGST_AMOUNT, " & vbCrLf _
                & " SUM((ID.ITEM_QTY * ID.ITEM_RATE) + CASE WHEN IH.INVOICESEQTYPE IN (3,5) THEN 0 ELSE CGST_AMOUNT+SGST_AMOUNT+IGST_AMOUNT END) As NET_AMOUNT, " & vbCrLf _
                & " '' AS PACK_QTY, " & vbCrLf _
                & " '', '', '', '', '', '' AS RECEIPTDATE, '' AS APPROVED, " & vbCrLf _
                & " INVMST.NAME, ACCTMST.SUPP_CUST_NAME,DECODE(IH.CANCELLED,'N','NO','YES') AS CANCELLED, " & vbCrLf _
                & " '' AS AGTD3, '','','', '', '', '' , '', '', '', '','',GMST.COMPANY_SHORTNAME,'','','',''," & vbCrLf _
                & " ITEMMST.MAT_THICHNESS, ITEMMST.ITEM_COLOR, SUBMST.SUBCATEGORY_DESC,SUM(ID.CHARGEABLEGLASS_AREA * ID.ITEM_QTY) AS SALEQTYSQM," & vbCrLf

        End If

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_MST ACM,  " & vbCrLf _
            & " FIN_SUPP_CUST_BUSINESS_MST CMST, FIN_SUPP_CUST_BUSINESS_MST CSMST, INV_ITEM_MST ITEMMST, " & vbCrLf _
            & " FIN_INVTYPE_MST INVMST, FIN_SUPP_CUST_MST ACCTMST, GEN_COMPANY_MST GMST, INV_SUBCATEGORY_MST SUBMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY And IH.COMPANY_CODE = GMST.COMPANY_CODE"

        If CDate(txtDateFrom.Text) >= CDate(RsCompany.Fields("START_DATE").Value) And CDate(txtDateFrom.Text) <= CDate(RsCompany.Fields("END_DATE").Value) Then
            MakeSQL = MakeSQL & vbCrLf & " And IH.FYEAR='" & RsCompany.Fields("FYEAR").Value & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf _
            & " And IH.COMPANY_CODE = ACM.COMPANY_CODE" & vbCrLf _
            & " And IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE" & vbCrLf _
            & " And IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " And IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " And IH.BILL_TO_LOC_ID=CMST.LOCATION_ID" & vbCrLf _
            & " And IH.COMPANY_CODE=CSMST.COMPANY_CODE" & vbCrLf _
            & " And IH.SHIPPED_TO_PARTY_CODE=CSMST.SUPP_CUST_CODE" & vbCrLf _
            & " And IH.SHIP_TO_LOC_ID=CSMST.LOCATION_ID" & vbCrLf _
            & " And IH.COMPANY_CODE=ACCTMST.COMPANY_CODE" & vbCrLf _
            & " And ID.COMPANY_CODE=ITEMMST.COMPANY_CODE" & vbCrLf _
            & " And ID.ITEM_CODE=ITEMMST.ITEM_CODE" & vbCrLf _
            & " And IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " And NVL(ID.ACCOUNT_POSTING_CODE,IH.TRNTYPE)=INVMST.CODE " & vbCrLf _
            & " And NVL(ID.INV_ACCOUNT_CODE,IH.ACCOUNTCODE)=ACCTMST.SUPP_CUST_CODE"     '' AND IH.ITEMVALUE<>0"


        MakeSQL = MakeSQL & vbCrLf _
            & " And ITEMMST.COMPANY_CODE = SUBMST.COMPANY_CODE" & vbCrLf _
            & " And ITEMMST.CATEGORY_CODE = SUBMST.CATEGORY_CODE" & vbCrLf _
            & " And ITEMMST.SUBCATEGORY_CODE=SUBMST.SUBCATEGORY_CODE"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND CMST.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
            End If
        End If

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND TRIM(ID.ITEM_CODE)='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        'If chkAllPONo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPONo.Text) <> "" Then
        '    MakeSQL = MakeSQL & vbCrLf & "AND IH.OUR_AUTO_KEY_SO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "'"
        'End If

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND ITEMMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
                mSubCatCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND ITEMMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        '    If cboInvoiceType.Text = "ALL" Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND INVMST.IDENTIFICATION<>'J'"
        '    Else
        '        MakeSQL = MakeSQL & vbCrLf & "AND IH.TRNTYPE=" & Val(lblTrnType.text) & ""
        '    End If

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            MakeSQL = MakeSQL & vbCrLf & " AND IH.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        'mShowAll = True
        'For CntLst = 1 To lstInvoiceType.Items.Count - 1
        '    If lstInvoiceType.GetItemChecked(CntLst) = True Then
        '        '            lstInvoiceType.ListIndex = CntLst
        '        mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
        '        If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
        '        End If
        '        mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
        '    Else
        '        mShowAll = False
        '    End If
        'Next

        mShowAll = True
        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                '            lstInvoiceType.ListIndex = CntLst
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = "'" & IIf(IsDBNull(MasterNo), "", MasterNo) & "'"
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            Else
                mShowAll = False
            End If
        Next

        If mShowAll = False Then
            If mTrnTypeStr <> "" Then
                mTrnTypeStr = "(" & mTrnTypeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " AND NVL(ID.INV_ACCOUNT_CODE,IH.ACCOUNTCODE) IN " & mTrnTypeStr & ""
                'MakeSQL = MakeSQL & vbCrLf & " AND NVL(INVMST.ACCOUNTPOSTCODE,IH.ACCOUNTCODE) IN " & mTrnTypeStr & ""
                'MakeSQL = MakeSQL & vbCrLf & " AND IH.ACCOUNTCODE IN " & mTrnTypeStr & ""
                ''ACCTMST.SUPP_CUST_NAME
            End If
        End If

        If cboInvoiceType.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.INVOICESEQTYPE='" & VB.Left(cboInvoiceType.Text, 1) & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & "AND NVL(IH.INVOICESEQTYPE,-1) NOT IN (7,8) "

        If cboAgtD3.SelectedIndex > 0 Then
            '        MakeSQL = MakeSQL & vbCrLf & "AND IH.AGTD3='" & vb.Left(cboAgtD3.Text, 1) & "'"
            If cboAgtD3.SelectedIndex = 1 Then
                MakeSQL = MakeSQL & vbCrLf & " AND IH.REF_DESP_TYPE ='S'"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND IH.REF_DESP_TYPE <>'S'"
            End If
        End If

        ''
        If cboCustomerGroup.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND ACM.CUSTOMER_GROUP='" & cboCustomerGroup.Text & "'"
        End If

        If cboFOC.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.FOC='" & VB.Left(cboFOC.Text, 1) & "'"
        End If

        If cboRejection.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.REJECTION='" & VB.Left(cboRejection.Text, 1) & "'"
        End If

        If cboCT3.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.AGTCT3='" & VB.Left(cboCT3.Text, 1) & "'"
        End If

        If cboCT1.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.AGTCT1='" & VB.Left(cboCT1.Text, 1) & "'"
        End If

        If cboCancelled.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.CANCELLED='" & VB.Left(cboCancelled.Text, 1) & "'"
        End If

        'If Trim(txtTariffHeading.Text) <> "" Then
        '    MakeSQL = MakeSQL & vbCrLf & "AND IH.TARIFFHEADING='" & MainClass.AllowSingleQuote(txtTariffHeading.Text) & "'"
        'End If

        If Trim(txtVehicleNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.VEHICLENO='" & MainClass.AllowSingleQuote(txtVehicleNo.Text) & "'"
        End If

        If cboExport.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_COUNTRY='N'"
        ElseIf cboExport.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_COUNTRY='Y'"
        End If

        If cboLocation.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(cboLocation.Text) & "'"
        End If

        '    If chkTime.Value = vbUnchecked Then
        '        MakeSQL = MakeSQL & vbCrLf _
        ''                & " AND IH.REMOVAL_TIME>=TO_DATE('" & txtTMFrom.Text & "', 'HH24:MI')" & vbCrLf _
        ''                & " AND IH.REMOVAL_TIME<=TO_DATE('" & txtTMTo.Text & "', 'HH24:MI')"
        '
        '    End If

        If chkInTransit.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND ACM.INTER_UNIT='Y'"

            MakeSQL = MakeSQL & vbCrLf & "AND (IH.IS_GATENTRY_MADE='N' OR NVL((SELECT GATE_DATE FROM INV_GATEENTRY_HDR WHERE BILL_NO=IH.BILLNO AND BILL_DATE=IH.INVOICE_DATE),IH.INVOICE_DATE) > TO_DATE('" & VB6.Format(txtInTransitAsonDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        End If

        If chkTime.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQL = MakeSQL & vbCrLf & " AND TO_CHAR(IH.INVOICE_DATE,'YYYYMMDD')||TO_CHAR(IH.REMOVAL_TIME,'HH24MI')>=TO_CHAR('" & VB6.Format(txtDateFrom.Text, "YYYYMMDD") & "') || '" & VB6.Format(txtTMFrom.Text, "HHMM") & "'" & vbCrLf & " AND TO_CHAR(IH.INVOICE_DATE,'YYYYMMDD')||TO_CHAR(IH.REMOVAL_TIME,'HH24MI')<=TO_CHAR('" & VB6.Format(txtDateTo.Text, "YYYYMMDD") & "') || '" & VB6.Format(txtTMTo.Text, "HHMM") & "'"
        End If

        '' GROUP BY CLAUSE
        If optType(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " GROUP BY "
            MakeSQL = MakeSQL & vbCrLf _
                & " GMST.COMPANY_SHORTNAME,IH.COMPANY_CODE, CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME,  IH.VENDOR_CODE, " & vbCrLf _
                & " CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, CMST.SUPP_CUST_PIN, CMST.GST_RGN_NO, " & vbCrLf _
                & " ID.ITEM_CODE , ITEMMST.ITEM_SHORT_DESC, ID.HSNCODE, ID.ITEM_RATE,ID.CUSTOMER_PART_NO,ID.ITEM_UOM,INVMST.NAME," & vbCrLf _
                & " ITEMMST.MAT_THICHNESS, ITEMMST.ITEM_COLOR, SUBMST.SUBCATEGORY_DESC,ACCTMST.SUPP_CUST_NAME,DECODE(IH.CANCELLED,'N','NO','YES') "
        End If


        If mType = "L" Then
            MakeSQL = MakeSQL & vbCrLf & " AND 1=2"
        End If
        ''ORDER BY CLAUSE...

        If optType(0).Checked = True Then
            'If optOrderBy(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY ITEMMST.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME,IH.BILLNO, IH.INVOICE_DATE"
            'Else
            '    MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.BILLNO,CMST.SUPP_CUST_NAME,ITEMMST.ITEM_SHORT_DESC, IH.INVOICE_DATE"
            'End If
        Else
            '        If optOrderBy(0).Value = True Then
            '            MakeSQL = MakeSQL & vbCrLf & "ORDER BY ID.ITEM_DESC,CMST.SUPP_CUST_NAME"
            '        Else
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY "

            'If chkMonthWise.CheckState = System.Windows.Forms.CheckState.Checked Then
            '    MakeSQL = MakeSQL & "TO_CHAR(IH.INVOICE_DATE,'MM-YYYY'), "
            'End If

            MakeSQL = MakeSQL & vbCrLf & " CMST.SUPP_CUST_NAME,ID.ITEM_CODE , ITEMMST.ITEM_SHORT_DESC,ID.ITEM_RATE"
            '        End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub FillInvoiceType()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer
        Dim pCompanyCode As Long
        Dim mRights As String

        lstInvoiceType.Items.Clear()

        SqlStr = "SELECT DISTINCT B.SUPP_CUST_NAME FROM FIN_INVTYPE_MST A, FIN_SUPP_CUST_MST B " & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
            & " AND A.ACCOUNTPOSTCODE=B.SUPP_CUST_CODE" & vbCrLf _
            & " AND A.CATEGORY='S' ORDER BY B.SUPP_CUST_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstInvoiceType.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstInvoiceType.Items.Add(RS.Fields("SUPP_CUST_NAME").Value)
                lstInvoiceType.SetItemChecked(CntLst, False)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstInvoiceType.SelectedIndex = 0

        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_SHORTNAME, COMPANY_CODE FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_SHORTNAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                pCompanyCode = RS.Fields("COMPANY_CODE").Value
                mRights = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn, pCompanyCode)
                If mRights <> "" Then
                    lstCompanyName.Items.Add(RS.Fields("COMPANY_SHORTNAME").Value)
                    lstCompanyName.SetItemChecked(CntLst, IIf(RS.Fields("COMPANY_SHORTNAME").Value = RsCompany.Fields("COMPANY_SHORTNAME").Value, True, False))
                    CntLst = CntLst + 1
                End If
                RS.MoveNext()
            Loop
        End If

        lstCompanyName.SelectedIndex = 0

        ''select distinct CUSTOMER_GROUP from fin_supp_cust_mst where supp_cust_type='S'

        cboCustomerGroup.Items.Clear()

        SqlStr = "SELECT DISTINCT CUSTOMER_GROUP FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')" & vbCrLf & " ORDER BY CUSTOMER_GROUP"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboCustomerGroup.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboCustomerGroup.Items.Add(RS.Fields("CUSTOMER_GROUP").Value)
                RS.MoveNext()
            Loop
        End If

        cboCustomerGroup.SelectedIndex = 0

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDivision.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = 0

        SqlStr = "SELECT DISTINCT BILL_TO_LOC_ID FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " ORDER BY BILL_TO_LOC_ID"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboLocation.Items.Clear()
        cboLocation.Items.Add("All")

        Do While RS.EOF = False
            cboLocation.Items.Add(IIf(IsDBNull(RS.Fields("BILL_TO_LOC_ID").Value), "", RS.Fields("BILL_TO_LOC_ID").Value))
            RS.MoveNext()
        Loop

        cboLocation.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub lstInvoiceType_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstInvoiceType.ItemCheck
        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstInvoiceType.GetItemChecked(0) = True Then
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstInvoiceType.GetItemChecked(e.Index - 1) = False Then
                    lstInvoiceType.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lstCompanyName_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstCompanyName.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstCompanyName.GetItemChecked(0) = True Then
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstCompanyName.GetItemChecked(e.Index - 1) = False Then
                    lstCompanyName.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If chkInTransit.CheckState = System.Windows.Forms.CheckState.Checked Then
            If MainClass.ChkIsdateF(txtInTransitAsonDate) = False Then Exit Function
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
        'Dim xVDate As String
        'Dim xMkey As String = ""
        'Dim xVNo As String
        'Dim xBookType As String = ""
        ''Dim xBookSubType As String

        'If optType(1).Checked = True Then Exit Sub

        'SprdMain.Row = SprdMain.ActiveRow

        'SprdMain.Col = ColBillDate
        'xVDate = Me.SprdMain.Text

        'If CDate(VB6.Format(xVDate, "DD/MM/YYYY")) < CDate(VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")) Then
        '    MsgInformation("Cann't open Last Year Voucher")
        '    Exit Sub
        'End If

        'SprdMain.Col = ColMKEY
        'xMkey = Me.SprdMain.Text

        'SprdMain.Col = ColBillNo
        'xVNo = Me.SprdMain.Text

        ''Call ShowTrn(xMkey, xVDate, "", xVNo, "S", "")

        'Dim SqlStr As String
        'Dim RsTemp As ADODB.Recordset
        'Dim mBookCode As String
        'Dim MyVnoPrefix As String
        'Dim mBillSeq As Long
        'Dim mAutoBillNo As Double

        'SqlStr = " SELECT BILLNOPREFIX, BILLNOSEQ, BOOKCODE, INVOICESEQTYPE FROM FIN_INVOICE_HDR" & vbCrLf _
        '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND FYEAR =" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        '    & " AND BILLNO='" & xVNo & "'"

        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        'If RsTemp.EOF = False Then
        '    mBookCode = IIf(IsDBNull(RsTemp.Fields("BOOKCODE").Value), "-2", RsTemp.Fields("BOOKCODE").Value)
        '    MyVnoPrefix = IIf(IsDBNull(RsTemp.Fields("BILLNOPREFIX").Value), "S", RsTemp.Fields("BILLNOPREFIX").Value)
        '    mBillSeq = IIf(IsDBNull(RsTemp.Fields("INVOICESEQTYPE").Value), "", RsTemp.Fields("INVOICESEQTYPE").Value)
        '    mAutoBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNOSEQ").Value), 0, RsTemp.Fields("BILLNOSEQ").Value)
        'Else
        '    Exit Sub
        'End If

        'FrmInvoiceGST.MdiParent = Me.MdiParent
        'FrmInvoiceGST.LblBookCode.Text = mBookCode
        'FrmInvoiceGST.lblInvoiceSeq.Text = mBillSeq
        'FrmInvoiceGST.Show()
        'FrmInvoiceGST.FrmInvoiceGST_Activated(Nothing, New System.EventArgs())
        'FrmInvoiceGST.txtBillNoPrefix.Text = MyVnoPrefix
        'FrmInvoiceGST.txtBillNo.Text = mAutoBillNo
        'FrmInvoiceGST.txtBillNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))


    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent)
        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Then
            'SprdMain_DblClick(SprdMain, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
        End If
        '    If KeyCode = vbKeyEscape Then cmdClose = True
    End Sub

    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.DoubleClick
        SearchItem()
    End Sub


    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub


    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtItemName.Text = UCase(Trim(txtItemName.Text))
        Else
            MsgInformation("No Such Item in Item Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSubCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtSubCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.DoubleClick
        SearchSubCategory()
    End Sub


    Private Sub txtSubCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSubCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSubCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSubCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSubCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSubCategory()
    End Sub

    Private Sub txtSubCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSubCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""

        If txtSubCategory.Text = "" Then GoTo EventExitSub


        If txtCategory.Text = "" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"
            If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                mCatCode = MasterNo
            End If
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"
        End If

        If MainClass.ValidateWithMasterTable((txtSubCategory.Text), "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("No Such Sub Category ")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SearchVehicleNo()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtVehicleNo.Text), "FIN_VEHICLE_MST", "NAME", "NAME", , , SqlStr) = True Then
            txtVehicleNo.Text = AcName
            '        txtTariff_Validate False
            If txtVehicleNo.Enabled = True Then txtVehicleNo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then
        '        txtDateFrom.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtInTransitAsonDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInTransitAsonDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtInTransitAsonDate) = False Then
            txtInTransitAsonDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then
        '        txtDateFrom.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    '
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtDateTo.Text)) = False Then
        '        txtDateTo.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTMFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTMFrom.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtTMTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTMTo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtVehicleNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicleNo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtVehicleNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicleNo.DoubleClick
        SearchVehicleNo()
    End Sub

    Private Sub txtVehicleNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicleNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicleNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVehicleNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVehicleNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchVehicleNo()
    End Sub
    Private Sub CreateGridHeader()
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   to create the grid header
        'Comments       :   Nil
        '----------------------------------------------------------------------------
        Try
            Dim inti As Integer
            'create column header

            UltraGrid1.DisplayLayout.Bands(0).Columns(0).RowLayoutColumnInfo.PreferredLabelSize = New System.Drawing.Size(0, 40)
            UltraGrid1.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True

            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Key = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColChallanNo - 1).Header.Caption = "Challan No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColChallanDate - 1).Header.Caption = "Challan Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNo - 1).Header.Caption = "Bill No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDate - 1).Header.Caption = "Bill Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyCode - 1).Header.Caption = "Customer Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyName - 1).Header.Caption = "Customer Name"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyVendorCode - 1).Header.Caption = "Customer Vendor Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyAddress - 1).Header.Caption = "Customer Address"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyCity - 1).Header.Caption = "Customer City"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyState - 1).Header.Caption = "Customer State"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyPIN - 1).Header.Caption = "Customer PIN"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyGSTNo - 1).Header.Caption = "Customer GSTNo"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1).Header.Caption = "Item Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemName - 1).Header.Caption = "Item Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartNo - 1).Header.Caption = "Part No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColHSNCode - 1).Header.Caption = "HSN Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemUOM - 1).Header.Caption = "Item UOM"



            UltraGrid1.DisplayLayout.Bands(0).Columns(ColQuantity - 1).Header.Caption = "Quantity"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRate - 1).Header.Caption = "Rate"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmount - 1).Header.Caption = "Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStockQty - 1).Header.Caption = "Stock Qty"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTime - 1).Header.Caption = "Time"


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCGST - 1).Header.Caption = "CGST Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSGST - 1).Header.Caption = "SGST Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIGST - 1).Header.Caption = "IGST Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillAmount - 1).Header.Caption = "Net Amount"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPackQty - 1).Header.Caption = "Pack Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVehicleNo - 1).Header.Caption = "Vehicle No"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRNo - 1).Header.Caption = "GR No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCarrier - 1).Header.Caption = "Transporter Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRNNo - 1).Header.Caption = "Customer GRN No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRNDate - 1).Header.Caption = "Customer GRN Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColReceiptDate - 1).Header.Caption = "Receipt Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColApproved - 1).Header.Caption = "Approved"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColInvoiceType - 1).Header.Caption = "Invoice Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAccountName - 1).Header.Caption = "Account Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCancel - 1).Header.Caption = "Cancel"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAgtD3 - 1).Header.Caption = "AgtD3"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerPONo - 1).Header.Caption = "Customer PO No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRefInvoiceNo - 1).Header.Caption = "Ref Invoice No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRefInvoiceDate - 1).Header.Caption = "Ref Invoice Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSameShipPartyCode - 1).Header.Caption = "Ship Party Same As Bill Party"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyCode - 1).Header.Caption = "Ship Party Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyName - 1).Header.Caption = "Ship Party Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyAddress - 1).Header.Caption = "Ship Party Address"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyCity - 1).Header.Caption = "Ship Party City"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyState - 1).Header.Caption = "Ship Party State"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyPIN - 1).Header.Caption = "Ship Party PIN"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyGSTNo - 1).Header.Caption = "Ship Party GST No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnitName - 1).Header.Caption = "Unit Name"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColModUser - 1).Header.Caption = "Updated By"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColModDate - 1).Header.Caption = "Updateed Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddUser - 1).Header.Caption = "Created By"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddDate - 1).Header.Caption = "Created Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColThickness - 1).Header.Caption = "Thickness"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColColor - 1).Header.Caption = "Color"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSubCategory - 1).Header.Caption = "Sub Category"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSQM - 1).Header.Caption = "SQM"



            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Header.Caption = "MKey"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPerPiece - 1).Header.Caption = "Per Piece (In GM)"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNetWeight - 1).Header.Caption = "Net Weight (In Kg)"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGrossWeight - 1).Header.Caption = "Gross Weight (In Kg)"




            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Appearance.TextHAlign = HAlign.Right

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment
            Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColQuantity - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRate - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmount - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStockQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCGST - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSGST - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIGST - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillAmount - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColThickness - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSQM - 1).Style = UltraWinGrid.ColumnStyle.Double

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPerPiece - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNetWeight - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGrossWeight - 1).Style = UltraWinGrid.ColumnStyle.Double

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColQuantity - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRate - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmount - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStockQty - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCGST - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSGST - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIGST - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillAmount - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColThickness - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSQM - 1).CellAppearance.TextHAlign = HAlign.Right

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPerPiece - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNetWeight - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGrossWeight - 1).CellAppearance.TextHAlign = HAlign.Right

            ''for hidden
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStockQty - 1).Hidden = True

            If optType(1).Checked = True Then
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColChallanNo - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColChallanDate - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNo - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDate - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColTime - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColStockQty - 1).Hidden = True

                UltraGrid1.DisplayLayout.Bands(0).Columns(ColVehicleNo - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColPackQty - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRNo - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRNNo - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRNDate - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColReceiptDate - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColApproved - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColModUser - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColModDate - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddUser - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddDate - 1).Hidden = True

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                    UltraGrid1.DisplayLayout.Bands(0).Columns(ColThickness - 1).Hidden = False
                    UltraGrid1.DisplayLayout.Bands(0).Columns(ColColor - 1).Hidden = False
                    UltraGrid1.DisplayLayout.Bands(0).Columns(ColSubCategory - 1).Hidden = False
                    UltraGrid1.DisplayLayout.Bands(0).Columns(ColSQM - 1).Hidden = False
                Else
                    UltraGrid1.DisplayLayout.Bands(0).Columns(ColThickness - 1).Hidden = True
                    UltraGrid1.DisplayLayout.Bands(0).Columns(ColColor - 1).Hidden = True
                    UltraGrid1.DisplayLayout.Bands(0).Columns(ColSubCategory - 1).Hidden = True
                    UltraGrid1.DisplayLayout.Bands(0).Columns(ColSQM - 1).Hidden = True
                End If

                UltraGrid1.DisplayLayout.Bands(0).Columns(ColAgtD3 - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerPONo - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColRefInvoiceNo - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColRefInvoiceDate - 1).Hidden = True


                UltraGrid1.DisplayLayout.Bands(0).Columns(ColSameShipPartyCode - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyCode - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyName - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyAddress - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyCity - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyState - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyPIN - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyGSTNo - 1).Hidden = True
            Else
                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                    UltraGrid1.DisplayLayout.Bands(0).Columns(ColThickness - 1).Hidden = False
                    UltraGrid1.DisplayLayout.Bands(0).Columns(ColColor - 1).Hidden = False
                    UltraGrid1.DisplayLayout.Bands(0).Columns(ColSubCategory - 1).Hidden = False
                    UltraGrid1.DisplayLayout.Bands(0).Columns(ColSQM - 1).Hidden = False
                Else
                    UltraGrid1.DisplayLayout.Bands(0).Columns(ColThickness - 1).Hidden = True
                    UltraGrid1.DisplayLayout.Bands(0).Columns(ColColor - 1).Hidden = True
                    UltraGrid1.DisplayLayout.Bands(0).Columns(ColSubCategory - 1).Hidden = True
                    UltraGrid1.DisplayLayout.Bands(0).Columns(ColSQM - 1).Hidden = True
                End If

            End If
                ' to define width of the columns
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemName - 1).Width = 350
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColHSNCode - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColChallanNo - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColChallanDate - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNo - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDate - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyCode - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyName - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColQuantity - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRate - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStockQty - 1).Width = 90

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyVendorCode - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyAddress - 1).Width = 300
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyCity - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyState - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyPIN - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyGSTNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemUOM - 1).Width = 90



            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTime - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmount - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCGST - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSGST - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIGST - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillAmount - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVehicleNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPackQty - 1).Width = 150

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRNo - 1).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCarrier - 1).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRNNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRNDate - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColReceiptDate - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColApproved - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColModUser - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColModDate - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddUser - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddDate - 1).Width = 100

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColThickness - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColColor - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSubCategory - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSQM - 1).Width = 100


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColInvoiceType - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAccountName - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCancel - 1).Width = 90

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAgtD3 - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerPONo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRefInvoiceNo - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRefInvoiceDate - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Width = 90


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSameShipPartyCode - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyCode - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyName - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyAddress - 1).Width = 300
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyCity - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyState - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyPIN - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShipPartyGSTNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnitName - 1).Width = 100

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPerPiece - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNetWeight - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGrossWeight - 1).Width = 100

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSQM - 1).MaskInput = "9999999.9999"

            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")

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
    Private Sub FillUltraGrid(pMakeSql As String)
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        UltraDataSource2.Rows.Clear()
        Me.UltraGrid1.DataSource = Nothing
        oledbCnn = New OleDbConnection(StrConn)
        Try

            ClearGroupFromUltraGrid(UltraGrid1)
            ClearFilterFromUltraGrid(UltraGrid1)
            oledbCnn.Open()
            oledbAdapter = New OleDbDataAdapter(pMakeSql, oledbCnn)

            oledbAdapter.Fill(ds)

            ' Set the data source and data member to bind the grid.
            Me.UltraGrid1.DataSource = ds
            Me.UltraGrid1.DataMember = ""

            CreateGridHeader()


            oledbAdapter.Dispose()
            oledbCnn.Close()
        Catch ex As Exception
            MsgBox("Can not open connection ! ")
        End Try
    End Sub

    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick
        Dim xVDate As String
        Dim xMkey As String = ""
        Dim xVNo As String
        Dim xBookType As String = ""

        Dim mBillNoPrefix As String = ""
        Dim mBillNo As String = ""
        Dim mBillNoSuffix As String = ""
        Dim mRow As UltraGridRow
        Dim mCol As UltraGridColumn

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)
        mCol = Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1)

        xVDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDate - 1))

        If xVDate = "" Then Exit Sub

        If optType(1).Checked = True Then Exit Sub

        If CDate(VB6.Format(xVDate, "DD/MM/YYYY")) < CDate(VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")) Then
            MsgInformation("Cann't open Last Year Voucher")
            Exit Sub
        End If

        xMkey = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1))
        xVNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNo - 1))


        'Call ShowTrn(xMkey, xVDate, "", xVNo, "S", "")

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mBookCode As String
        Dim MyVnoPrefix As String
        Dim mBillSeq As Long
        Dim mAutoBillNo As Double

        SqlStr = " SELECT BILLNOPREFIX, BILLNOSEQ, BOOKCODE, INVOICESEQTYPE FROM FIN_INVOICE_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR =" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND MKEY='" & xMkey & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mBookCode = IIf(IsDBNull(RsTemp.Fields("BOOKCODE").Value), "-2", RsTemp.Fields("BOOKCODE").Value)
            MyVnoPrefix = IIf(IsDBNull(RsTemp.Fields("BILLNOPREFIX").Value), "", RsTemp.Fields("BILLNOPREFIX").Value)
            mBillSeq = IIf(IsDBNull(RsTemp.Fields("INVOICESEQTYPE").Value), "", RsTemp.Fields("INVOICESEQTYPE").Value)
            mAutoBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNOSEQ").Value), 0, RsTemp.Fields("BILLNOSEQ").Value)
        Else
            Exit Sub
        End If

        FrmInvoiceGST.MdiParent = Me.MdiParent
        FrmInvoiceGST.LblBookCode.Text = mBookCode
        FrmInvoiceGST.lblInvoiceSeq.Text = mBillSeq
        FrmInvoiceGST.Show()
        FrmInvoiceGST.FrmInvoiceGST_Activated(Nothing, New System.EventArgs())
        FrmInvoiceGST.txtBillNoPrefix.Text = MyVnoPrefix
        FrmInvoiceGST.txtBillNo.Text = mAutoBillNo
        FrmInvoiceGST.txtBillNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

        'Dim mBillNoPrefix As String
        'Dim mBillNo As String
        'Dim mBillNoSuffix As String
        'Dim mRow As UltraGridRow
        'Dim mCol As UltraGridColumn

        'If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        'mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)
        'mCol = Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1)

        'mBillNoPrefix = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1))
        'mBillNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(2))       ''ultrow.SetCellValue(m_udtColumns.EntryNo, dtRow.Item("EntryNo"))
        'mBillNoSuffix = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(3))

        'txtBillNoPrefix.Text = mBillNoPrefix
        'txtBillNo.Text = VB6.Format(mBillNo, ConBillFormat)
        'txtBillNoSuffix.Text = mBillNoSuffix

        'txtBillNo_Validating(txtBillNo, New System.ComponentModel.CancelEventArgs(False))
        'CmdView_Click(CmdView, New System.EventArgs())

    End Sub

    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click

        Dim lngLoop As Integer  'loop variable

        Dim objMode As Object 'to store the mode of the row
        Dim objChk As Object 'to get the check status of the first column
        Dim strSplit() As String 'split variable
        Dim intAns As Integer ' to store the result from msgbox
        Dim lngRow As Long
        Try


            'If m_blnChangeInData = True Then
            '    MessageFromResFile(7284, MessageType.Information)
            '    GridSetFocus(UltraGrid1.ActiveRow.Tag.ToString, UltraGrid1.ActiveCell.Column.Index)
            '    Exit Sub
            'End If

            ''Please provide a location where you whould like to export the data to
            'MessageFromResFile(7304, MessageType.Information, GetLabelDes("7305"))

            Try
                SaveFileDialog1.FileName = Me.Text
            Catch
                SaveFileDialog1.FileName = "File1"
            End Try

            Dim strAction As String = ""
            Try
                strAction = SaveFileDialog1.ShowDialog()
            Catch
                SaveFileDialog1.FileName = "File1"
                strAction = SaveFileDialog1.ShowDialog()
            End Try

            If strAction = "1" Then
                ExportToExcel(SaveFileDialog1.FileName)
            End If
            'Me.Cursor = Cursors.Default

        Catch ex As Exception
            ErrorMsg(Err.Description, Err.Number)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ExportToExcel(ByVal strFileName As String)
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   To export the report to excel/csv/text file
        'Comments       :   This function will be called from clickinvoked and enterpressed events
        '                   THIS FUNCTION HAS TO BE OVERRIDED IN THE DERIVED FORM   
        '----------------------------------------------------------------------------
        Me.Cursor = Cursors.WaitCursor
        Dim start As DateTime
        'Dim timespan As TimeSpan
        start = DateTime.Now
        Try
            Me.UltraGridExcelExporter1.FileLimitBehaviour = ExcelExport.FileLimitBehaviour.TruncateData
            Me.UltraGridExcelExporter1.ExportAsync(Me.UltraGrid1, strFileName & ".xlsx")  ''(.xlsx)
            ' timespan = DateTime.Now.Subtract(start)
            'Exported To File : 
            '  MessageFromResFile(7292, MessageType.Information, strFileName)
        Catch
            'Specified Path Does Not Exist,Invalid File Name
            ErrorMsg(Err.Description, Err.Number)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub UltraGrid1_InitializeLayout(sender As Object, e As InitializeLayoutEventArgs) Handles UltraGrid1.InitializeLayout

        ''Allowing Summaries in the UltraGrid 
        e.Layout.Override.AllowRowSummaries = AllowRowSummaries.True
        '' Setting the Sum Summary for the desired column

        e.Layout.Bands(0).Summaries.Add("ColQuantity", SummaryType.Sum, e.Layout.Bands(0).Columns(ColQuantity - 1))
        e.Layout.Bands(0).Summaries.Add("ColAmount", SummaryType.Sum, e.Layout.Bands(0).Columns(ColAmount - 1))
        e.Layout.Bands(0).Summaries.Add("ColCGST", SummaryType.Sum, e.Layout.Bands(0).Columns(ColCGST - 1))
        e.Layout.Bands(0).Summaries.Add("ColSGST", SummaryType.Sum, e.Layout.Bands(0).Columns(ColSGST - 1))
        e.Layout.Bands(0).Summaries.Add("ColIGST", SummaryType.Sum, e.Layout.Bands(0).Columns(ColIGST - 1))
        e.Layout.Bands(0).Summaries.Add("ColBillAmount", SummaryType.Sum, e.Layout.Bands(0).Columns(ColBillAmount - 1))
        e.Layout.Bands(0).Summaries.Add("ColSQM", SummaryType.Sum, e.Layout.Bands(0).Columns(ColSQM - 1))

        ''Set the display format to be just the number 
        e.Layout.Bands(0).Summaries("ColQuantity").DisplayFormat = "{0:###0.00}"
        e.Layout.Bands(0).Summaries("ColAmount").DisplayFormat = "{0:###0.00}"
        e.Layout.Bands(0).Summaries("ColCGST").DisplayFormat = "{0:###0.00}"
        e.Layout.Bands(0).Summaries("ColSGST").DisplayFormat = "{0:###0.00}"
        e.Layout.Bands(0).Summaries("ColIGST").DisplayFormat = "{0:###0.00}"
        e.Layout.Bands(0).Summaries("ColBillAmount").DisplayFormat = "{0:###0.00}"
        e.Layout.Bands(0).Summaries("ColSQM").DisplayFormat = "{0:###0.00}"

        ''Hide the SummaryFooterCaption row 
        e.Layout.Bands(0).Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False

        'band.SummaryFooterCaption = "Subtotal:"

        e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.FontData.Bold = DefaultableBoolean.True

        e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.BackColor = Color.LightSteelBlue

        e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.ForeColor = Color.Black
        '     / Here, I want to add grand total

        e.Layout.Bands(0).Summaries("ColQuantity").Appearance.TextHAlign = HAlign.Right
        e.Layout.Bands(0).Summaries("ColAmount").Appearance.TextHAlign = HAlign.Right
        e.Layout.Bands(0).Summaries("ColCGST").Appearance.TextHAlign = HAlign.Right
        e.Layout.Bands(0).Summaries("ColSGST").Appearance.TextHAlign = HAlign.Right
        e.Layout.Bands(0).Summaries("ColIGST").Appearance.TextHAlign = HAlign.Right
        e.Layout.Bands(0).Summaries("ColBillAmount").Appearance.TextHAlign = HAlign.Right
        e.Layout.Bands(0).Summaries("ColSQM").Appearance.TextHAlign = HAlign.Right
        ''
        'Disable grid default highlight

        'UltraGrid1.DisplayLayout.Override.ResetActiveRowAppearance()

        'UltraGrid1.DisplayLayout.Override.ResetActiveCellAppearance()

        'UltraGrid1.DisplayLayout.Override.ActiveAppearancesEnabled = DefaultableBoolean.False

        e.Layout.ViewStyleBand = ViewStyleBand.OutlookGroupBy
    End Sub

    Private Sub cmdCustomReport_Click(sender As Object, e As EventArgs) Handles cmdCustomReport.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonCustomShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
