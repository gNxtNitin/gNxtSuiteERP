Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json.Serialization
Imports QRCoder
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Color
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Friend Class FrmInvoiceAgtDI
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColMKey As Short = 1
    Private Const CoDIDate As Short = 2
    Private Const CoDivision As Short = 3
    Private Const ColCustomerName As Short = 4
    Private Const ColLocation As Short = 5
    Private Const ColSONO As Short = 6
    Private Const ColItemCode As Short = 7
    Private Const ColItemDesc As Short = 8
    Private Const ColPartNo As Short = 9
    Private Const ColStoreLoc As Short = 10
    Private Const ColOD As Short = 11
    Private Const ColStockQty As Short = 12
    Private Const ColQty As Short = 13

    'Private Const ColDespatchNoteNo As Short = 13

    Private Const ColFlag As Short = 14
    Dim mDespatchNoteNo As Double

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        cmdShow.Enabled = True
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        cmdShow.Enabled = True
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()

    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String
        Dim mUpdateCount As Integer
        Dim mMKey As String
        Dim mCustomerName As String
        Dim mItemCode As String
        Dim mStoreLoc As String
        Dim mODNo As String
        Dim mQty As Double
        Dim mStockQty As Double
        Dim mDivisionCode As Long
        Dim mSchdDate As String
        Dim mRow As UltraGridRow
        Dim mMaxRow As Long
        mUpdateCount = 0
        Dim mFlag As String
        Dim mBatchCount As Long
        Dim mDINo As String
        Dim mPrevDINo As String
        Dim mSuppCustCode As String
        Dim mDIDate As String
        Dim mLocation As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSONo As Double
        Dim mDespQty As Double
        Dim mScheduleQty As Double = 0
        Dim mDayScheduleQty As Double = 0
        Dim mDIRequired As String
        Dim pPackQty As Double = 0

        mMaxRow = UltraGrid1.Rows.Count
        mBatchCount = 0
        mPrevDINo = ""

        If CDate(RunDate) <> CDate(PubCurrDate) Then
            MsgInformation("Run Date is not match with Current Date.")
            Exit Sub
        End If

        With UltraGrid1
            For cntRow = 0 To mMaxRow - 1
                mRow = Me.UltraGrid1.Rows(cntRow)
                mFlag = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColFlag - 1))
                If UCase(mFlag) = "TRUE" Then

                    mDIDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(CoDIDate - 1))
                    mCustomerName = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerName - 1))
                    mItemCode = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1))
                    mLocation = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocation - 1))
                    mSchdDate = "01/" & VB6.Format(mDIDate, "MM/YYYY")

                    If CDate(mDIDate) > CDate(RunDate) Then
                        MsgInformation("DI Date Can't be Greater Than Run Date." & mItemCode)
                        Exit Sub
                    End If

                    mSuppCustCode = "-1"
                    If MainClass.ValidateWithMasterTable((mCustomerName), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mSuppCustCode = MasterNo
                    End If
                    If CheckRTVPending(mSuppCustCode, mItemCode, mDIDate, mLocation) = True Then
                        MsgInformation("Customer : " & mCustomerName & " RTV is pending for Item Code : " & mItemCode)
                        Exit Sub
                    End If

                    If CheckDDR(mSuppCustCode, mItemCode, mDIDate, mLocation) = True Then
                        MsgInformation("Customer : " & mCustomerName & " DDR for Item Code : " & mItemCode)
                        Exit Sub
                    End If

                    If CheckInvoiceReceiptPending(mSuppCustCode, mItemCode, mLocation) = True Then
                        MsgInformation("Customer : " & mCustomerName & " Receipt is pending for Item Code : " & mItemCode)
                        Exit Sub
                    End If

                    mDINo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKey - 1))
                    mStoreLoc = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColStoreLoc - 1))
                    mODNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColOD - 1))
                    mSONo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColSONO - 1))
                    pPackQty = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1))

                    If MainClass.ValidateWithMasterTable(Val(mSONo), "AUTO_KEY_SO", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And SUPP_CUST_CODE='" & Trim(mSuppCustCode) & "' AND SO_APPROVED='Y'") = True Then
                        mDIRequired = MasterNo
                    Else
                        mDIRequired = "N"
                    End If

                    SqlStr = " SELECT AUTO_KEY_DESP " & vbCrLf _
                        & " FROM DSP_DI_DET WHERE AUTO_KEY_DESP=" & mDINo & "" & vbCrLf _
                        & " AND ITEM_CODE='" & mItemCode & "' AND IS_INVOICE_MADE='Y'"

                    If mStoreLoc = "" Then
                        SqlStr = SqlStr & vbCrLf & " AND LOC_CODE IS NULL"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND LOC_CODE='" & mStoreLoc & "'"
                    End If

                    If mODNo = "" Then
                        SqlStr = SqlStr & vbCrLf & " AND OD_NO IS NULL"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND OD_NO='" & mODNo & "'"
                    End If

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = False Then
                        MsgInformation("DI Already Made for Item Code : " & mItemCode)
                        Exit Sub
                    End If

                    SqlStr = "SELECT " & vbCrLf _
                           & " SUM(PLANNED_QTY) AS PLANNED_QTY, " & vbCrLf _
                           & " SUM(CASE WHEN SERIAL_DATE=TO_DATE('" & VB6.Format(mDIDate, "DD/MMM/YYYY") & "','DD-MON-YYYY') THEN PLANNED_QTY ELSE 0 END) AS DAY_PLANNED_QTY " & vbCrLf _
                           & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID" & vbCrLf _
                           & " WHERE IH.AUTO_KEY_DELV = ID.AUTO_KEY_DELV" & vbCrLf _
                           & " AND IH.SUPP_CUST_CODE='" & Trim(mSuppCustCode) & "'" & vbCrLf _
                           & " AND IH.AUTO_KEY_SO=" & Val(mSONo) & "" & vbCrLf _
                           & " AND IH.SCHLD_DATE =TO_DATE('" & VB6.Format(mSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                           & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

                    If mStoreLoc = "" Then
                        SqlStr = SqlStr & vbCrLf & " AND ID.LOC_CODE IS NULL"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND ID.LOC_CODE='" & mStoreLoc & "'"
                    End If

                    If mODNo = "" Then
                        SqlStr = SqlStr & vbCrLf & " AND ID.OD_NO IS NULL"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND ID.OD_NO='" & mODNo & "'"
                    End If


                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    mScheduleQty = 0
                    mDayScheduleQty = 0

                    If RsTemp.EOF = False Then
                        mScheduleQty = IIf(IsDBNull(RsTemp.Fields("PLANNED_QTY").Value), 0, RsTemp.Fields("PLANNED_QTY").Value)
                        mDayScheduleQty = IIf(IsDBNull(RsTemp.Fields("DAY_PLANNED_QTY").Value), 0, RsTemp.Fields("DAY_PLANNED_QTY").Value)
                    End If

                    mDespQty = GetTotMonthDespatchQty(mSONo, mSuppCustCode, mDIDate, mItemCode, mDIRequired, mODNo,,, mStoreLoc)

                    mScheduleQty = mScheduleQty - mDespQty

                    If mScheduleQty < pPackQty And pPackQty > 0 Then
                        MsgInformation("Schedule Qty is Less than Plan Qty for Item Code :" & mItemCode)
                        Exit Sub
                    End If

                End If
            Next
        End With

        If chkBatchGeneration.Checked = False Then
            With UltraGrid1
                For cntRow = 0 To mMaxRow - 1
                    mRow = Me.UltraGrid1.Rows(cntRow)
                    mFlag = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColFlag - 1))
                    If UCase(mFlag) = "TRUE" Then
                        mDINo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKey - 1))


                        mDIDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(CoDIDate - 1))
                        mCustomerName = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerName - 1))
                        mItemCode = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1))

                        If CDate(mDIDate) > CDate(RunDate) Then
                            MsgInformation("DI Date Can't be Greater Than Run Date." & mItemCode)
                            Exit Sub
                        End If

                        'mSuppCustCode = "-1"
                        'If MainClass.ValidateWithMasterTable((mCustomerName), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        '    mSuppCustCode = MasterNo
                        'End If
                        'If CheckRTVPending(mSuppCustCode, mItemCode, mDIDate) = True Then
                        '    MsgInformation("Customer : " & mCustomerName & " RTV is pending for Item Code : " & mItemCode)
                        '    Exit Sub
                        'End If

                        'If CheckInvoiceReceiptPending(mSuppCustCode, mItemCode) = True Then
                        '    MsgInformation("Customer : " & mCustomerName & " Receipt is pending for Item Code : " & mItemCode)
                        '    Exit Sub
                        'End If


                        If mPrevDINo <> "" Then
                            If mPrevDINo <> mDINo Then
                                MsgInformation("You can Make Despatch Note with in Same Sale Order / DI.")
                                Exit Sub
                            End If
                        End If
                        mPrevDINo = mDINo
                    End If
                Next
            End With
        End If

        With UltraGrid1
            For cntRow = 0 To mMaxRow - 1
                mRow = Me.UltraGrid1.Rows(cntRow)
                mFlag = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColFlag - 1))

                '.Row = cntRow
                '.Col = ColFlag
                If UCase(mFlag) = "TRUE" Then
                    mBatchCount = mBatchCount + 1
                    mMKey = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKey - 1))
                    mDivisionCode = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(CoDivision - 1))
                    mCustomerName = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerName - 1))
                    mItemCode = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1))
                    mStoreLoc = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColStoreLoc - 1))
                    mODNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColOD - 1))
                    mStockQty = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColStockQty - 1))
                    mQty = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1))

                    mDIDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(CoDIDate - 1))

                    If UpdateDespatchMain1(mMKey, mDivisionCode, mCustomerName, mItemCode, mStoreLoc, mODNo, mQty, IIf((chkBatchGeneration.Checked = False And mBatchCount = 1) Or chkBatchGeneration.Checked = True, True, False)) = False Then GoTo ErrPart
                End If

NextRowNo:

            Next
        End With

        CmdSave.Enabled = False
        'PubDBCn.CommitTrans()

        'MsgBox("Total " & mUpdateCount & " Invoice Generated.", MsgBoxStyle.Information)
        Exit Sub
ErrPart:
        ''Resume
        ErrorMsg(Err.Description, err.number, MsgBoxStyle.Critical)
        'PubDBCn.RollbackTrans()
    End Sub
    Private Function GetTotMonthDespatchQty(ByRef pSONo As Double, ByRef pSupplierCode As String, ByRef pDIDate As String, ByRef pItemCode As String, mDIRequired As String, mODNo As String, Optional ByRef pOverAllSOQty As String = "", Optional ByRef mWEF As String = "", Optional ByRef mStoreLoc As String = "") As Double

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mOrderType As String

        GetTotMonthDespatchQty = 0

        '& " AND IH.AUTO_KEY_SO=" & Val(txtSONo) & "" & vbCrLf _
        '
        If MainClass.ValidateWithMasterTable(Val(pSONo), "AUTO_KEY_SO", "ORDER_TYPE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & Trim(pSupplierCode) & "' AND SO_APPROVED='Y'") = True Then
            mOrderType = MasterNo
        Else
            mOrderType = "O"
        End If

        mSqlStr = " SELECT SUM(PACKED_QTY) AS ITEM_QTY " & vbCrLf _
            & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID " & vbCrLf _
            & " WHERE IH.AUTO_KEY_DESP = ID.AUTO_KEY_DESP" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & Trim(pSupplierCode) & "'" & vbCrLf _
            & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'"

        mSqlStr = mSqlStr & " AND IH.DESP_TYPE IN ('G','P','S') AND DESP_STATUS<>2 "

        ''AND ID.STOCK_TYPE='FG' 

        If mDIRequired = "Y" Then
            If mODNo = "" Then
                'mSqlStr = mSqlStr & vbCrLf & " AND (OD_NO='' OR OD_NO IS NULL)"
            Else
                mSqlStr = mSqlStr & vbCrLf & " AND OD_NO='" & mODNo & "'"
            End If
        End If

        If mStoreLoc <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND ID.LOC_CODE='" & mStoreLoc & "'"
        End If

        If mOrderType = "C" Or pOverAllSOQty = "Y" Then
            mSqlStr = mSqlStr & " AND IH.AUTO_KEY_SO=" & Val(pSONo) & " "
            If mWEF <> "" Then
                mSqlStr = mSqlStr & " AND IH.DESP_DATE >=TO_DATE('" & VB6.Format(mWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            End If
        Else
            mSqlStr = mSqlStr & " AND TO_CHAR(IH.DESP_DATE,'YYYYMM')='" & VB6.Format(pDIDate, "YYYYMM") & "' "
        End If

        'If Val(txtDNNo.Text) <> 0 Then
        '    mSqlStr = mSqlStr & " AND IH.AUTO_KEY_DI<>" & Val(txtDNNo.Text) & ""
        'End If

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetTotMonthDespatchQty = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00"))
        End If
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
            mSqlStr = " SELECT SUM(ID.BILL_QTY) AS ITEM_QTY " & vbCrLf _
                    & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, FIN_INVOICE_HDR IIH, FIN_INVOICE_DET IID, DSP_DESPATCH_DET DD " & vbCrLf _
                    & " WHERE IH.AUTO_KEY_MRR = ID.AUTO_KEY_MRR" & vbCrLf _
                    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE='" & Trim(pSupplierCode) & "'" & vbCrLf _
                    & " AND IH.REF_TYPE='I' " & vbCrLf _
                    & " AND IH.COMPANY_CODE=IIH.COMPANY_CODE " & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE=IIH.SUPP_CUST_CODE " & vbCrLf _
                    & " AND ID.REF_PO_NO=IIH.AUTO_KEY_INVOICE " & vbCrLf _
                    & " AND IIH.MKEY=IID.MKEY " & vbCrLf _
                    & " AND IIH.AUTO_KEY_DESP=DD.AUTO_KEY_DESP " & vbCrLf _
                    & " AND IID.ITEM_CODE=DD.ITEM_CODE " & vbCrLf _
                    & " AND IID.SUBROWNO=DD.SERIAL_NO " & vbCrLf _
                    & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'"

            If mStoreLoc <> "" Then
                mSqlStr = mSqlStr & vbCrLf & " AND NVL(DD.LOC_CODE,'')='" & mStoreLoc & "'"
            End If

            mSqlStr = mSqlStr & " AND IIH.OUR_AUTO_KEY_SO=" & Val(pSONo) & " "

            mSqlStr = mSqlStr & " AND TO_CHAR(IH.MRR_DATE,'YYYYMM')='" & VB6.Format(pDIDate, "YYYYMM") & "' "
            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                GetTotMonthDespatchQty = GetTotMonthDespatchQty - CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00"))
            End If

            mSqlStr = " SELECT SUM(IID.ITEM_SHORT_RECD_QTY) AS ITEM_QTY " & vbCrLf _
                   & " FROM FIN_INVOICE_HDR IIH, FIN_INVOICE_DET IID, DSP_DESPATCH_DET DD " & vbCrLf _
                   & " WHERE IIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND IIH.SUPP_CUST_CODE='" & Trim(pSupplierCode) & "'" & vbCrLf _
                   & " AND CANCELLED='N' " & vbCrLf _
                   & " AND IIH.MKEY=IID.MKEY " & vbCrLf _
                   & " AND IIH.AUTO_KEY_DESP=DD.AUTO_KEY_DESP " & vbCrLf _
                   & " AND IID.ITEM_CODE=DD.ITEM_CODE " & vbCrLf _
                   & " AND IID.SUBROWNO=DD.SERIAL_NO " & vbCrLf _
                   & " AND IID.ITEM_CODE='" & Trim(pItemCode) & "'"

            If mStoreLoc <> "" Then
                mSqlStr = mSqlStr & vbCrLf & " AND NVL(DD.LOC_CODE,'')='" & mStoreLoc & "'"
            End If

            mSqlStr = mSqlStr & " AND IIH.OUR_AUTO_KEY_SO=" & Val(pSONo) & " "

            mSqlStr = mSqlStr & " AND TO_CHAR(IIH.GRNDATE,'YYYYMM')='" & VB6.Format(pDIDate, "YYYYMM") & "' "
            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                GetTotMonthDespatchQty = GetTotMonthDespatchQty - CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00"))
            End If

        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateDespatchMain1(ByVal mMKey As Double, ByVal mDivisionCode As Long, ByVal mCustomerName As String, ByVal mItemCode As String, ByVal mStoreLoc As String, ByVal mODNo As String, ByVal mQty As Double, pBatchTrue As Boolean) As Boolean

        On Error GoTo ErrPart
        Dim I As Short
        Dim SqlStr As String = ""
        Dim nMkey As String
        Dim mVNoSeq As Double
        Dim mSuppCustCode As String
        Dim mDespatchSeqType As Long
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If pBatchTrue = False Then
            mVNoSeq = mDespatchNoteNo
            GoTo DetailPart
        End If
        mSuppCustCode = "-1"
        If MainClass.ValidateWithMasterTable((mCustomerName), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = "-1"
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mDespatchSeqType = 1

        'If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mDivisionCode = CDbl(Trim(MasterNo))
        'End If

        mVNoSeq = CDbl(AutoGenSeqNo(mDivisionCode))

        mDespatchNoteNo = mVNoSeq

        SqlStr = "INSERT INTO DSP_DESPATCH_HDR( " & vbCrLf _
                & " COMPANY_CODE, AUTO_KEY_DESP, DESP_DATE," & vbCrLf _
                & " SUPP_CUST_CODE, " & vbCrLf _
                & " TRANSPORTER_NAME, VEHICLE_NO," & vbCrLf _
                & " LOADING_TIME, PRE_EMP_CODE, " & vbCrLf _
                & " DESP_STATUS, DESP_TYPE, " & vbCrLf _
                & " AUTO_KEY_SO, SO_DATE, " & vbCrLf _
                & " VENDOR_PO, VENDOR_PO_DATE, " & vbCrLf _
                & " GRNO,GRDATE," & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE, DIV_CODE, DESPATCHTYPE, " & vbCrLf _
                & " SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE,BILL_TO_LOC_ID,SHIP_TO_LOC_ID,LOC_CODE) "

        SqlStr = SqlStr & vbCrLf _
                & " SELECT " & vbCrLf _
                & " COMPANY_CODE, " & mVNoSeq & ", TO_DATE('" & VB6.Format(RunDate, "DD/MMM/YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " SUPP_CUST_CODE, " & vbCrLf _
                & " TRANSPORTER_NAME, VEHICLE_NO," & vbCrLf _
                & " LOADING_TIME, PRE_EMP_CODE, " & vbCrLf _
                & " DESP_STATUS, DESP_TYPE, " & vbCrLf _
                & " AUTO_KEY_SO, SO_DATE, " & vbCrLf _
                & " VENDOR_PO, VENDOR_PO_DATE, " & vbCrLf _
                & " GRNO,GRDATE," & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE, DIV_CODE, DESPATCHTYPE, " & vbCrLf _
                & " SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE,BILL_TO_LOC_ID,SHIP_TO_LOC_ID,LOC_CODE " & vbCrLf _
                & " FROM  DSP_DI_HDR WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND AUTO_KEY_DESP=" & mMKey & ""

        PubDBCn.Execute(SqlStr)

DetailPart:
        If UpdateDetail1(mMKey, Val(CStr(mVNoSeq)), mDivisionCode, "P", mCustomerName, mItemCode, mStoreLoc, mODNo, mQty) = False Then GoTo ErrPart

        If chkBatchGeneration.Checked = True Then
            If UpdateInvoiceMain1(Val(mVNoSeq), mDivisionCode, mCustomerName, mStoreLoc) = False Then GoTo ErrPart
            'UpdateInvoiceMain1(ByVal mDespatchNo As Double, ByVal mDivisionCode As Long, ByVal mCustomerName As String, pCustLoc As String)
        End If
        UpdateDespatchMain1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        '    Resume
        UpdateDespatchMain1 = False
        PubDBCn.RollbackTrans() ''
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume
    End Function
    Private Function UpdateDetail1(ByVal pDINo As Double, ByRef pNewMey As Double, ByRef mDivisionCode As Double, ByRef mDespType As String,
                                   ByVal mCustomerName As String, ByVal mItemCode As String, ByVal mStoreLoc As String, ByVal mODNo As String, ByVal mQty As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mSubRowNo As Integer
        Dim mUnit As String
        Dim mStockType As String = ""
        Dim mPackQty As Double
        Dim mPktQty As Double
        Dim mPDIRNo As String = ""
        Dim mRefNo As String
        Dim mMRRNo As Double
        Dim mMRRDate As String = ""
        Dim pPartyF4Date As String = ""
        Dim pOurVDate As String = ""
        Dim mHeadType As String = ""
        Dim mSqlStr As String
        Dim RsBOM As ADODB.Recordset = Nothing
        Dim mStdQty As Double
        Dim mRMChildCode As String
        Dim mRMUOM As String
        Dim pErrorDesc As String = ""
        Dim mStockRowNo As Integer
        Dim cntRow As Integer
        Dim mScrapQty As Double

        Dim xSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSoNo As Double
        Dim mSODate As String
        Dim mCustomerNo As String
        Dim mCustomerDate As String
        Dim mLotNo As String
        Dim mHeatNo As String
        Dim mBatchNo As String
        Dim mJITCallNo As String

        Dim mCustomerCode As String = ""
        Dim mOrgBillNO As Double
        Dim mOrdBillDate As String = ""
        Dim mCRItemRate As Double
        Dim mRefDate As String
        Dim mShippedCode As String
        Dim mStockStatus As String

        Dim mColInnerBoxQty As Double
        Dim mColInnerBoxCode As String
        Dim mColOuterBoxQty As Double
        Dim mColOuterBoxCode As String
        Dim pDespSNO As Long
        Dim xStoreLoc As String
        'PubDBCn.Execute("Delete From DSP_DESPATCH_DET Where AUTO_KEY_DESP='" & pNewMey & "'")

        'If DeleteStockTRN(PubDBCn, ConStockRefType_DSP, (pNewMey)) = False Then GoTo UpdateDetail1Err

        If MainClass.ValidateWithMasterTable(Trim(mCustomerName), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = Trim(MasterNo)
        End If

        pDespSNO = 1

        'mShippedCode = "-1"
        'End If

        mSubRowNo = 0
        cntRow = 1
        mStockRowNo = 1

        SqlStr = "SELECT IH.AUTO_KEY_DESP, IH.DESP_DATE,DIV_CODE, ACM.SUPP_CUST_NAME,  IH.BILL_TO_LOC_ID, " & vbCrLf _
                & " ID.* "

        SqlStr = SqlStr & vbCrLf _
                & " FROM DSP_DI_HDR IH, DSP_DI_DET ID, FIN_SUPP_CUST_MST ACM, INV_ITEM_MST IMST" & vbCrLf _
                & " WHERE IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " And IH.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND ID.ITEM_CODE=IMST.ITEM_CODE " & vbCrLf _
                & " And IH.Company_Code=IMST.Company_Code " & vbCrLf _
                & " And IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.AUTO_KEY_DESP=" & pDINo & ""

        SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & mItemCode & "'"

        If mStoreLoc = "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.LOC_CODE IS NULL"
        Else
            SqlStr = SqlStr & vbCrLf & " AND ID.LOC_CODE='" & mStoreLoc & "'"
        End If

        If mODNo = "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.OD_NO IS NULL"
        Else
            SqlStr = SqlStr & vbCrLf & " AND ID.OD_NO='" & mODNo & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)



        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                'mSubRowNo = IIf(IsDBNull(RsTemp.Fields("SERIAL_NO").Value), 0, RsTemp.Fields("SERIAL_NO").Value)
                mSoNo = IIf(IsDBNull(RsTemp.Fields("SONO").Value), -1, RsTemp.Fields("SONO").Value)
                mSODate = IIf(IsDBNull(RsTemp.Fields("SODATE").Value), "", RsTemp.Fields("SODATE").Value)
                xStoreLoc = IIf(IsDBNull(RsTemp.Fields("LOC_CODE").Value), "", RsTemp.Fields("LOC_CODE").Value)
                mCustomerNo = IIf(IsDBNull(RsTemp.Fields("CUST_PO").Value), -1, RsTemp.Fields("CUST_PO").Value)
                mCustomerDate = IIf(IsDBNull(RsTemp.Fields("CUST_PO_DATE").Value), "", RsTemp.Fields("CUST_PO_DATE").Value)

                mUnit = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                mHeatNo = IIf(IsDBNull(RsTemp.Fields("HEAT_NO").Value), "", RsTemp.Fields("HEAT_NO").Value)
                mBatchNo = IIf(IsDBNull(RsTemp.Fields("BATCH_NO").Value), "", RsTemp.Fields("BATCH_NO").Value)


                mODNo = IIf(IsDBNull(RsTemp.Fields("OD_NO").Value), "", RsTemp.Fields("OD_NO").Value)
                mLotNo = "" ''Trim(.Text)
                mStockType = IIf(IsDBNull(RsTemp.Fields("STOCK_TYPE").Value), "", RsTemp.Fields("STOCK_TYPE").Value)
                mMRRNo = IIf(IsDBNull(RsTemp.Fields("MRR_REF_NO").Value), "", RsTemp.Fields("MRR_REF_NO").Value)
                mRefNo = IIf(IsDBNull(RsTemp.Fields("OUR_REF_NO").Value), "", RsTemp.Fields("OUR_REF_NO").Value)
                mRefDate = IIf(IsDBNull(RsTemp.Fields("OUR_REF_DATE").Value), "", RsTemp.Fields("OUR_REF_DATE").Value)
                mPackQty = IIf(IsDBNull(RsTemp.Fields("PACKED_QTY").Value), 0, RsTemp.Fields("PACKED_QTY").Value)
                mPktQty = IIf(IsDBNull(RsTemp.Fields("NO_OF_PACKETS").Value), 0, RsTemp.Fields("NO_OF_PACKETS").Value)
                mJITCallNo = IIf(IsDBNull(RsTemp.Fields("JITCALLNO").Value), "", RsTemp.Fields("JITCALLNO").Value)

                mColInnerBoxQty = IIf(IsDBNull(RsTemp.Fields("INNER_PACK_QTY").Value), 0, RsTemp.Fields("INNER_PACK_QTY").Value)
                mColInnerBoxCode = IIf(IsDBNull(RsTemp.Fields("INNER_PACK_ITEM_CODE").Value), "", RsTemp.Fields("INNER_PACK_ITEM_CODE").Value)
                mColOuterBoxQty = IIf(IsDBNull(RsTemp.Fields("OUTER_PACK_QTY").Value), 0, RsTemp.Fields("OUTER_PACK_QTY").Value)

                mColOuterBoxCode = IIf(IsDBNull(RsTemp.Fields("OUTER_PACK_ITEM_CODE").Value), "", RsTemp.Fields("OUTER_PACK_ITEM_CODE").Value)



                SqlStr = ""
                '            mRefNo = 907
                If mItemCode <> "" And mPackQty > 0 Then
                    mSubRowNo = GetMaxDCSubRow(pNewMey)     '' mSubRowNo + 1       

                    SqlStr = " INSERT INTO DSP_DESPATCH_DET (AUTO_KEY_DESP, SERIAL_NO, ITEM_CODE,ITEM_UOM, STOCK_TYPE, " & vbCrLf _
                            & " PACKED_QTY,NO_OF_PACKETS, PDIR_NO, REF_NO, REF_DATE, MRR_REF_NO, COMPANY_CODE, " & vbCrLf _
                            & " SONO, SODATE,CUST_PO, CUST_PO_DATE, LOT_NO,JITCALLNO,HEAT_NO,BATCH_NO, OD_NO," & vbCrLf _
                            & " INNER_PACK_QTY, INNER_PACK_ITEM_CODE, OUTER_PACK_QTY, OUTER_PACK_ITEM_CODE,LOC_CODE) " & vbCrLf _
                            & " VALUES ('" & pNewMey & "'," & mSubRowNo & ",'" & mItemCode & "', '" & mUnit & "'," & vbCrLf _
                            & " '" & mStockType & "'," & mPackQty & ", " & mPktQty & ", '" & mPDIRNo & "'," & vbCrLf _
                            & " '" & mRefNo & "', TO_DATE('" & VB6.Format(mRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " " & mMRRNo & ", " & RsCompany.Fields("COMPANY_CODE").Value & "," & mSoNo & ", " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(mSODate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mCustomerNo) & "'," & vbCrLf _
                            & " TO_DATE('" & VB6.Format(mCustomerDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " '" & mLotNo & "','" & mJITCallNo & "','" & mHeatNo & "','" & mBatchNo & "','" & mODNo & "'," & vbCrLf _
                            & " " & mColInnerBoxQty & ",'" & mColInnerBoxCode & "'," & mColOuterBoxQty & ",'" & mColOuterBoxCode & "','" & xStoreLoc & "') "

                    PubDBCn.Execute(SqlStr)

                    'pDespSNO = pDespSNO + 1
                    SqlStr = " UPDATE DSP_DI_DET SET IS_INVOICE_MADE='Y' WHERE AUTO_KEY_DESP=" & pDINo & ""

                    SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & mItemCode & "'"

                    If mStoreLoc = "" Then
                        SqlStr = SqlStr & vbCrLf & " AND LOC_CODE IS NULL"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND LOC_CODE='" & mStoreLoc & "'"
                    End If

                    If mODNo = "" Then
                        SqlStr = SqlStr & vbCrLf & " AND OD_NO IS NULL"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND OD_NO='" & mODNo & "'"
                    End If

                    PubDBCn.Execute(SqlStr)

                    If UpdateStockTRN(PubDBCn, ConStockRefType_DSP, pNewMey, mStockRowNo, VB6.Format(PubCurrDate, "DD/MM/YYYY"), VB6.Format(PubCurrDate, "DD/MM/YYYY"), mStockType, mItemCode, mUnit, mLotNo, mPackQty, 0, "O", 0, 0, "", "", "", "PAD", "", "N", " To : " & mCustomerName, mCustomerCode, ConWH, mDivisionCode, mDespType, "") = False Then GoTo UpdateDetail1Err
                    mStockRowNo = mStockRowNo + 1

NextRow:
                End If
                RsTemp.MoveNext()
            Loop
        End If

        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function GetMaxDCSubRow(ByRef pNewMey As Double) As Integer

        On Error GoTo AutoGenSeqNoErr
        Dim RsDNMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""

        SqlStr = ""
        mNewSeqNo = 1



        SqlStr = "SELECT Max(SERIAL_NO)  " & vbCrLf _
            & " FROM DSP_DESPATCH_DET " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND AUTO_KEY_DESP =" & pNewMey & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsDNMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mNewSeqNo = .Fields(0).Value
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = 1 '' 1
                End If
            End If
        End With
        GetMaxDCSubRow = mNewSeqNo
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function AutoGenSeqNo(ByRef mDivisionCode As Double) As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsDNMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mStartingSNo As Double
        Dim mSeparateSeries As String = "N"
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMaxValue As String

        SqlStr = ""
        mStartingSNo = 1


        mSeparateSeries = IIf(IsDBNull(RsCompany.Fields("SEPARATE_DSP_SERIES").Value), "N", RsCompany.Fields("SEPARATE_DSP_SERIES").Value)

        SqlStr = "SELECT DSP_SERIES " & vbCrLf & " FROM INV_DIVISION_MST " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            '        mSeparateSeries = IIf(IsNull(RsTemp!SEPARATE_DSP_SERIES), "N", RsTemp!SEPARATE_DSP_SERIES)
            If mSeparateSeries = "Y" Then
                mStartingSNo = IIf(IsDBNull(RsTemp.Fields("DSP_SERIES").Value), 1, RsTemp.Fields("DSP_SERIES").Value)
                mStartingSNo = IIf(mStartingSNo = 0, 1, mStartingSNo)
            End If
        End If

        SqlStr = "SELECT Max(AUTO_KEY_DESP)  " & vbCrLf & " FROM DSP_DESPATCH_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DESP,LENGTH(AUTO_KEY_DESP)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""


        If mSeparateSeries = "Y" Then
            SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
        End If
        SqlStr = SqlStr & vbCrLf & " AND DESPATCHTYPE<>2"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsDNMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = Mid(mMaxValue, 1, Len(mMaxValue) - 6)
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = mStartingSNo '' 1
                End If
            End If
        End With
        AutoGenSeqNo = mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateInvoiceMain1(ByVal mDespatchNo As Double, ByVal mDivisionCode As Long, ByVal mCustomerName As String, pCustLoc As String) As Boolean

        On Error GoTo ErrPart
        Dim I As Short
        Dim SqlStr As String = ""
        Dim nMkey As String
        Dim mVNoSeq As Double
        Dim mSuppCustCode As String
        Dim mDespatchSeqType As Long
        Dim mCurRowNo As Double
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTempSO As ADODB.Recordset = Nothing

        Dim mBillNoPrefix As String = ""
        Dim mBillNoSuffix As String = ""
        Dim mAutoKeyNo As Double
        Dim mBillNoSeq As Double
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillTm As String
        Dim mDCDate As String
        Dim mPONo As String
        Dim mPODate As String
        Dim mPOAmendNo As String
        Dim mPOWEFDate As String
        Dim mRemovalDate As String
        Dim mRemovalTime As String
        Dim mAccountCode As String
        Dim mCreditDaysFrom As String
        Dim mCreditDaysTo As String
        Dim mAUTHSIGN As String
        Dim mAUTHDATE As String
        Dim mGRNo As String
        Dim mGRDate As String
        Dim mMode As String
        Dim mDocsThru As String
        Dim mVehicle As String
        Dim mCarriers As String
        Dim mFREIGHTCHARGES As String = "Paid"
        Dim mTariff As String
        Dim mEXEMPT_NOTIF_NO As String
        Dim mBookCode As String
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mSALETAXCODE As String
        Dim mRemarks As String
        Dim mItemType As String
        Dim mItemValue As String
        Dim mTOTSTAMT As Double = 0
        Dim mTOTCHARGES As Double = 0
        Dim mTotEDAmount As Double = 0
        Dim mTOTEXPAMT As Double = 0
        Dim mNETVALUE As Double = 0
        Dim mTotQty As Double = 0
        Dim mFormRecdCode As Double = -1
        Dim mFormDueCode As Double = -1
        Dim mSTType As String = "0"
        Dim mIsRegdNo As String
        Dim mLSTCST As String
        Dim mWITHFORM As String
        Dim mFOC As String
        Dim mPRINTED As String = "N"
        Dim mCancelled As String = "N"
        Dim mNarration As String
        Dim mSTPERCENT As Double = 0
        Dim mTOTFREIGHT As Double = 0
        Dim mEDPERCENT As Double = 0
        Dim mTOTTAXABLEAMOUNT As String
        Dim mSURAmount As Double = 0
        Dim mTotDiscount As Double = 0
        Dim mMSC As Double = 0
        Dim mRO As Double = 0
        Dim mREJECTION As String
        Dim mD3 As String
        Dim mPackMat As String
        Dim mChallanMade As String
        Dim mStockTrf As String
        Dim mTCSPER As Double = 0
        Dim mTCSAMOUNT As Double = 0
        Dim pDNNo As String
        Dim pDNDate As String
        Dim mTotEDUPercent As Double = 0
        Dim mTotEDUAmount As Double = 0
        Dim mTotServicePercent As Double = 0
        Dim mTotServiceAmount As Double = 0
        Dim pServProvided As String
        Dim pSuppFromDate As String
        Dim pSuppToDate As String
        Dim pIntRate As Double = 0
        Dim mCT3 As String = "N"
        Dim mCT3Date As String
        Dim pDespRef As String
        Dim pPoNo As String
        Dim pSoDate As String
        Dim pShippingNo As String = ""
        Dim pShippingDate As String = ""
        Dim mVehicleType As String = "O"
        Dim pResponseId As String = ""
        Dim pEWayBillNo As String = ""
        Dim mDespatchFrom As String
        Dim mShippedFromCode As String
        Dim mShippToExWork As String
        Dim pBillTo As String
        Dim pShipTo As String
        Dim mTRNType As Double
        Dim pVendorCode As String
        Dim pPacking As String = ""
        Dim pARE1No As String = ""
        Dim pARE1Date As String = ""
        Dim pPortCode As String = ""
        Dim pExportBillNo As String = ""
        Dim pExportBillDate As String = ""
        Dim pTotExportExp As Double = 0
        Dim pExchangeRate As Double = 0
        Dim pTotalEuro As Double = 0
        Dim pAdvLicense As String = ""
        Dim pLocation As String = ""
        Dim pProcessNature As String = ""
        Dim pMRPValue As Double = 0
        Dim mTaxOnMRP As String = "N"
        Dim pAbatementPer As Double = 0
        Dim pTotCD As Double = 0
        Dim pEDUOnCDAmount As Double = 0
        Dim mBuyerCode As String = ""
        Dim mCoBuyerCode As String = ""
        Dim mSHECPercent As Double = 0
        Dim mSHECAmount As Double = 0
        Dim mDutyForgone As Double = 0
        Dim mDutyFreePurchase As String = "N"
        Dim mDutyIncluded As Double = 0
        Dim pAdvDate As String = ""
        Dim pAdvAdjust As Double = 0
        Dim pAdvCGST As Double = 0
        Dim pAdvSGST As Double = 0
        Dim pAdvIGST As Double = 0
        Dim pItemAdvAdjust As Double = 0
        Dim mLUT As String = "N"
        Dim mCT1 As String = "N"
        Dim mCT1Date As String = ""
        Dim mAgtPermission As String = "N"
        Dim mCustMatValue As Double = 0
        Dim mTotCGSTAmount As Double = 0
        Dim mTotSGSTAmount As Double = 0
        Dim mTotIGSTAmount As Double = 0
        Dim mSACCode As String = ""
        Dim pAdvVNo As String = ""
        Dim mShippedToSame As String
        Dim mShippedToCode As String
        Dim meRefNo As String = ""
        Dim pInvoiceSeq As Double
        Dim pTransportCode As String
        Dim mTransMode As String = 1
        Dim pDistance As Double = 0

        Dim mItemCode As String
        Dim mPartNo As String
        Dim mItemDesc As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mMRP As Double
        Dim mAmount As Double
        Dim mExicseableAmt As Double
        Dim mSTableAmt As Double
        Dim mCessableAmt As Double
        Dim mCESSAmt As Double
        Dim mSHECAmt As Double
        Dim mRefNo As String
        Dim mRefDate As String
        Dim UpdateRec As String

        Dim mTotExicseableAmt As Double
        Dim mTotSTableAmt As Double
        Dim mTotCessableAmt As Double
        Dim mIsSaleComp As String
        Dim mIsSuppInv As String
        Dim mServiceAmt As Double
        Dim mTaxableMRP As Double
        Dim mJITCallNo As String
        Dim mCustItemValue As Integer
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mOBillNo As String
        Dim mOBillDate As String
        Dim mHSNCode As String
        Dim mPOS As String
        Dim mState As String
        Dim mGoodsServices As String
        Dim mTaxableAmount As Double

        Dim mNoofStrip As Double
        Dim mStripRate As Double
        Dim mItemSNo As String

        Dim mAddItemDesc As String = ""
        Dim mMRRNo As Double
        Dim mODNo As String = ""
        Dim mHeatNo As String = ""
        Dim mBatchNo As String = ""

        Dim mColInnerBoxQty As Double
        Dim mColInnerBoxCode As String
        Dim mColOuterBoxQty As Double
        Dim mColOuterBoxCode As String
        Dim mAcctCode As String = ""
        Dim mAcctName As String

        Dim mPartyGSTNo As String
        Dim mSameGSTNo As String
        Dim mTotItemValue As String

        pDNNo = ""
        pDNDate = ""
        pServProvided = ""
        pSuppFromDate = ""
        pSuppToDate = ""
        mCT3 = ""
        mCT3Date = ""
        mFREIGHTCHARGES = 0
        mTariff = ""
        mEXEMPT_NOTIF_NO = ""

        mSALETAXCODE = "-1"
        mRemarks = ""
        mItemValue = 0
        mSTType = ""
        mIsRegdNo = "N"
        mLSTCST = ""
        mWITHFORM = ""
        mFOC = "N"
        mNarration = ""
        mTOTTAXABLEAMOUNT = 0
        mREJECTION = "N"
        mD3 = "N"
        mPackMat = "N"
        mChallanMade = "Y"
        Dim mLocal As String

        mBookCode = ConSalesBookCode
        mBookType = ConSaleBook


        pInvoiceSeq = 1


        mSuppCustCode = "-1"
        If MainClass.ValidateWithMasterTable((mCustomerName), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = "-1"
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If


        SqlStr = SqlStr & vbCrLf & " AND DESPATCHTYPE<>2"

        SqlStr = " SELECT " & vbCrLf _
                & " COMPANY_CODE, AUTO_KEY_DESP, DESP_DATE," & vbCrLf _
                & " SUPP_CUST_CODE, " & vbCrLf _
                & " TRANSPORTER_NAME, VEHICLE_NO," & vbCrLf _
                & " LOADING_TIME, PRE_EMP_CODE, " & vbCrLf _
                & " DESP_STATUS, DESP_TYPE, " & vbCrLf _
                & " AUTO_KEY_SO, SO_DATE, " & vbCrLf _
                & " VENDOR_PO, VENDOR_PO_DATE, " & vbCrLf _
                & " GRNO,GRDATE," & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE, DIV_CODE, DESPATCHTYPE, " & vbCrLf _
                & " SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE,BILL_TO_LOC_ID,SHIP_TO_LOC_ID,LOC_CODE " & vbCrLf _
                & " FROM DSP_DESPATCH_HDR  " & vbCrLf _
                & " WHERE AUTO_KEY_DESP=" & mDespatchNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then

            mBillDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("DESP_DATE").Value), "", RsTemp.Fields("DESP_DATE").Value), "DD/MM/YYYY")
            mBillTm = VB6.Format(IIf(IsDBNull(RsTemp.Fields("LOADING_TIME").Value), "", RsTemp.Fields("LOADING_TIME").Value), "HH:MM")

            mDCDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("DESP_DATE").Value), "", RsTemp.Fields("DESP_DATE").Value), "DD/MM/YYYY")
            mPONo = IIf(IsDBNull(RsTemp.Fields("VENDOR_PO").Value), "", RsTemp.Fields("VENDOR_PO").Value)
            mPODate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VENDOR_PO_DATE").Value), "", RsTemp.Fields("VENDOR_PO_DATE").Value), "DD/MM/YYYY")
            mPOAmendNo = 0
            mPOWEFDate = ""
            mRemovalDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("DESP_DATE").Value), "", RsTemp.Fields("DESP_DATE").Value), "DD/MM/YYYY")
            mRemovalTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("LOADING_TIME").Value), "", RsTemp.Fields("LOADING_TIME").Value), "HH:MM")

            mAUTHSIGN = ""
            mAUTHDATE = ""
            mGRNo = IIf(IsDBNull(RsTemp.Fields("GRNO").Value), "", RsTemp.Fields("GRNO").Value)
            mGRDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("GRDATE").Value), "", RsTemp.Fields("GRDATE").Value), "DD/MM/YYYY")
            mGRDate = IIf(mGRDate = "", mDCDate, mGRDate)
            mMode = "BY ROAD"
            mDocsThru = ""
            mVehicle = IIf(IsDBNull(RsTemp.Fields("VEHICLE_NO").Value), "", RsTemp.Fields("VEHICLE_NO").Value)
            mCarriers = IIf(IsDBNull(RsTemp.Fields("TRANSPORTER_NAME").Value), "", RsTemp.Fields("TRANSPORTER_NAME").Value)
            pTransportCode = ""
            mItemType = ""


            If MainClass.ValidateWithMasterTable(mSuppCustCode, "SUPP_CUST_CODE", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mStockTrf = MasterNo
            Else
                mStockTrf = "N"
            End If

            pDespRef = IIf(IsDBNull(RsTemp.Fields("DESP_TYPE").Value), "", RsTemp.Fields("DESP_TYPE").Value)
            pPoNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_SO").Value), "", RsTemp.Fields("AUTO_KEY_SO").Value)
            pSoDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SO_DATE").Value), "", RsTemp.Fields("SO_DATE").Value), "DD/MM/YYYY")

            mDespatchFrom = "N"
            mShippedFromCode = "-1"
            mShippToExWork = "N"

            mShippedToSame = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value), "Y", RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value)
            mShippedToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), mSuppCustCode, RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
            pBillTo = IIf(IsDBNull(RsTemp.Fields("BILL_TO_LOC_ID").Value), "", RsTemp.Fields("BILL_TO_LOC_ID").Value)
            pShipTo = IIf(IsDBNull(RsTemp.Fields("SHIP_TO_LOC_ID").Value), "", RsTemp.Fields("SHIP_TO_LOC_ID").Value)

            mPOS = ""
            If mShippedToSame = "Y" Then
                If MainClass.ValidateWithMasterTable(mSuppCustCode, "SUPP_CUST_CODE", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                    mState = MasterNo
                    If MainClass.ValidateWithMasterTable(mState, "NAME", "STATE_CODE", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                        mPOS = MasterNo
                    End If
                End If
            Else
                If MainClass.ValidateWithMasterTable(mShippedToCode, "SUPP_CUST_CODE", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                    mState = MasterNo
                    If MainClass.ValidateWithMasterTable(mState, "NAME", "STATE_CODE", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                        mPOS = MasterNo
                    End If
                End If
            End If

            mLocal = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(pBillTo), "WITHIN_STATE")
            mPartyGSTNo = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(pBillTo), "GST_RGN_NO")

            mSameGSTNo = IIf(mPartyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "Y", "N")


            SqlStr = "SELECT DISTINCT ID.ACCOUNT_POSTING_CODE, ITYPE.ACCOUNTPOSTCODE, IH.VENDOR_CODE, ITYPE.IDENTIFICATION" & vbCrLf _
                            & " FROM  DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, FIN_INVTYPE_MST ITYPE" & vbCrLf _
                            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND IH.MKEY=ID.MKEY AND IH.SUPP_CUST_CODE='" & mSuppCustCode & "'" & vbCrLf _
                            & " AND ID.COMPANY_CODE=ITYPE.COMPANY_CODE AND ID.ACCOUNT_POSTING_CODE=ITYPE.CODE" & vbCrLf _
                            & " AND IH.AUTO_KEY_SO=" & Val(pPoNo) & " AND SO_APPROVED='Y'" & vbCrLf _
                            & " AND IH.MKEY = ("

            SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf _
                            & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & mSuppCustCode & "'" & vbCrLf _
                            & " AND SIH.AUTO_KEY_SO=" & Val(pPoNo) & " AND SO_APPROVED='Y'" & vbCrLf _
                            & " AND SID.AMEND_WEF <=TO_DATE('" & VB6.Format(mDCDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"



            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempSO, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTempSO.EOF = False Then
                mTRNType = IIf(IsDBNull(RsTempSO.Fields("ACCOUNT_POSTING_CODE").Value), "", RsTempSO.Fields("ACCOUNT_POSTING_CODE").Value)
                pVendorCode = IIf(IsDBNull(RsTempSO.Fields("VENDOR_CODE").Value), "", RsTempSO.Fields("VENDOR_CODE").Value)
                mAccountCode = IIf(IsDBNull(RsTempSO.Fields("ACCOUNTPOSTCODE").Value), "", RsTempSO.Fields("ACCOUNTPOSTCODE").Value)
                mBookSubType = IIf(IsDBNull(RsTempSO.Fields("IDENTIFICATION").Value), "", RsTempSO.Fields("IDENTIFICATION").Value)
            End If


            SqlStr = "SELECT FROM_DAYS, TO_DAYS FROM " & vbCrLf _
                & " FIN_SUPP_CUST_HDR A, FIN_PAYTERM_MST B" & vbCrLf _
                & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
                & " AND A.PAYMENT_CODE=B.PAY_TERM_CODE AND SUPP_CUST_CODE='" & mSuppCustCode & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempSO, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTempSO.EOF = False Then
                mCreditDaysFrom = IIf(IsDBNull(RsTempSO.Fields("FROM_DAYS").Value), 0, RsTempSO.Fields("FROM_DAYS").Value)
                mCreditDaysTo = IIf(IsDBNull(RsTempSO.Fields("TO_DAYS").Value), 0, RsTempSO.Fields("TO_DAYS").Value)
            End If

        End If

        mBillNoPrefix = GetDocumentPrefix("S", 1)
        mBillNoSuffix = ""
        Dim mStartingNo As Double = 1
        mBillNoSeq = CDbl(AutoGenSeqBillNo(mBookType, mBookSubType, mStartingNo, mDivisionCode, mBillNoPrefix))



        mBillNo = Trim(Trim(mBillNoPrefix) & VB6.Format(Val(CStr(mBillNoSeq)), ConBillFormat) & Trim(mBillNoSuffix))

        mAutoKeyNo = VB6.Format(VB6.Format(Val(CStr(mBillNoSeq)), ConBillFormat) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))


        mCurRowNo = MainClass.AutoGenRowNo("FIN_INVOICE_HDR", "RowNo", PubDBCn)
        nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo

        SqlStr = "INSERT INTO FIN_INVOICE_HDR (" & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf _
            & " ROWNO, TRNTYPE, BILLNOPREFIX, " & vbCrLf _
            & " AUTO_KEY_INVOICE, BILLNOSEQ, BILLNOSUFFIX, BILLNO, " & vbCrLf _
            & " INVOICE_DATE, INV_PREP_DATE, INV_PREP_TIME, " & vbCrLf _
            & " AUTO_KEY_DESP, DCDATE, CUST_PO_NO, CUST_PO_DATE, " & vbCrLf _
            & " AMEND_NO, AMEND_DATE, AMEND_WEF_FROM, REMOVAL_DATE, " & vbCrLf _
            & " REMOVAL_TIME, SUPP_CUST_CODE, ACCOUNTCODE, ST_38_NO, " & vbCrLf _
            & " DUEDAYSFROM, DUEDAYSTO, AUTHSIGN, AUTHDATE, " & vbCrLf _
            & " GRNO, GRDATE, DESPATCHMODE, DOCSTHROUGH, " & vbCrLf _
            & " VEHICLENO, CARRIERS, FREIGHTCHARGES, " & vbCrLf _
            & " TARIFFHEADING, EXEMPT_NOTIF_NO, " & vbCrLf _
            & " BOOKCODE,BOOKTYPE, BOOKSUBTYPE, SALETAXCODE, " & vbCrLf _
            & " REMARKS, ITEMDESC, ITEMVALUE, " & vbCrLf _
            & " TOTSTAMT, TOTCHARGES, TOTEDAMOUNT, " & vbCrLf _
            & " TOTEXPAMT, NETVALUE, TOTQTY, "


        SqlStr = SqlStr & vbCrLf _
            & " STFORMCODE, STFORMNAME, STFORMNO, STFORMDATE, " & vbCrLf _
            & " STDUEFORMCODE, STDUEFORMNAME, STDUEFORMNO, STDUEFORMDATE,  " & vbCrLf _
            & " STTYPE, IsRegdNo,LSTCST, WITHFORM, FOC, PRINTED," & vbCrLf _
            & " CANCELLED, NARRATION,  " & vbCrLf _
            & " STPERCENT, TOTFREIGHT, EDPERCENT, TOTTAXABLEAMOUNT, " & vbCrLf _
            & " TOTSURCHARGEAMT, TOTDISCAMOUNT, TOTMSCAMOUNT, TotRO,REJECTION,AGTD3, " & vbCrLf _
            & " PACK_MAT_FLAG, CHALLAN_MADE,PRDDate, " & vbCrLf _
            & " ADDUSER, ADDDATE, MODUSER, MODDATE,ISSTOCKTRF,TCSPER, TCSAMOUNT,DNCNNO,DNCNDATE," & vbCrLf _
            & " TOTEDUPERCENT,TOTEDUAMOUNT,TOTSERVICEPERCENT,TOTSERVICEAMOUNT,SERV_PROV," & vbCrLf _
            & " SUPP_FROM_DATE, SUPP_TO_DATE, INTRATE, " & vbCrLf _
            & " AGTCT3, CT_NO, CT3_DATE, ARE_NO, " & vbCrLf _
            & " REF_DESP_TYPE, OUR_AUTO_KEY_SO, OUR_SO_DATE, "

        SqlStr = SqlStr & vbCrLf & " SHIPPING_NO, SHIPPING_DATE, " & vbCrLf _
            & " ARE1_NO, ARE1_DATE, " & vbCrLf _
            & " PORT_CODE, EXPBILLNO, EXPINV_DATE, TOT_EXPORTEXP,EXCHANGE_RATE, " & vbCrLf _
            & " TOTEXCHANGEVALUE, ADV_LICENSE, DESP_LOCATION, NATURE," & vbCrLf _
            & " TOTMRPVALUE, TAX_ON_MRP, ABATEMENT_PER, " & vbCrLf _
            & " TOT_CUSTOMDUTY, TOT_CD_CESS, CD_PER, CD_CESS_PER, BUYER_CODE, CO_BUYER_CODE," & vbCrLf _
            & " TOTSHECPERCENT, TOTSHECAMOUNT,UPDATE_FROM,ISDUTY_FORGONE, AGT_DUTYFREE_PUR," & vbCrLf _
            & " DUTY_INCLUDED_ITEM, ED_PAYABLE, CESS_PAYABLE, SHEC_PAYABLE,DIV_CODE, " & vbCrLf _
            & " AGTCT1, CT1_NO, CT1_DATE,AGT_Permission,CUST_ITEM_VALUE, " & vbCrLf _
            & " NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT," & vbCrLf _
            & " SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE,E_REFNO,INVOICESEQTYPE,SAC_CODE," & vbCrLf _
            & " ADV_VNO, ADV_VDATE, ADV_ADJUSTED_AMT, " & vbCrLf _
            & " ADV_CGST_AMT, ADV_SGST_AMT, ADV_IGST_AMT,ADV_ITEM_AMT,IS_LUT, " & vbCrLf _
            & " TRANSPORT_MODE, TRANSPORTER_GSTNO, TRANS_DISTANCE, " & vbCrLf _
            & " VEHICLE_TYPE, EWAYRESPONSEID, E_BILLWAYNO," & vbCrLf _
            & " IS_DESP_OTHERTHAN_BILL, SHIPPED_FROM_PARTY_CODE, " & vbCrLf _
            & " IS_SHIPPTO_EX_WORK, BILL_TO_LOC_ID, SHIP_TO_LOC_ID, VENDOR_CODE,PACKING_DETAILS" & vbCrLf _
            & " )"

        SqlStr = SqlStr & vbCrLf & " VALUES('" & nMkey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
            & " " & mCurRowNo & "," & Val(mTRNType) & ", '" & MainClass.AllowSingleQuote(mBillNoPrefix) & "', " & vbCrLf _
            & " " & mAutoKeyNo & "," & mBillNoSeq & ", '" & MainClass.AllowSingleQuote(mBillNoSuffix) & "', '" & MainClass.AllowSingleQuote(mBillNo) & "', " & vbCrLf _
            & " TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & mBillTm & "','HH24:MI')," & vbCrLf _
            & " " & Val(mDespatchNo) & ", TO_DATE('" & VB6.Format(mDCDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mPONo) & "', TO_DATE(TO_DATE('" & VB6.Format(mPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))," & vbCrLf _
            & " " & Val(mPOAmendNo) & ",'',TO_DATE('" & VB6.Format(mPOWEFDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mRemovalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
            & " TO_DATE('" & mRemovalTime & "','HH24:MI'),'" & mSuppCustCode & "','" & mAccountCode & "','', " & vbCrLf _
            & " " & Val(mCreditDaysFrom) & ", " & Val(mCreditDaysTo) & ", '" & mAUTHSIGN & "', TO_DATE('" & VB6.Format(mAUTHDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(mGRNo) & "', TO_DATE('" & VB6.Format(mGRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mMode) & "', '" & MainClass.AllowSingleQuote(mDocsThru) & "', " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(mVehicle) & "', '" & MainClass.AllowSingleQuote(mCarriers) & "', '" & mFREIGHTCHARGES & "', " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(mTariff) & "', '" & mEXEMPT_NOTIF_NO & "', " & vbCrLf _
            & " '" & mBookCode & "', '" & mBookType & "', '" & mBookSubType & "', " & mSALETAXCODE & ", " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(mRemarks) & "', '" & MainClass.AllowSingleQuote(mItemType) & "', " & mItemValue & ", " & vbCrLf _
            & " " & mTOTSTAMT & ", " & mTOTCHARGES & ", " & mTotEDAmount & ", " & vbCrLf _
            & " " & mTOTEXPAMT & ", " & mNETVALUE & ", " & mTotQty & ", " & vbCrLf _
            & " " & mFormRecdCode & ", '','', '', " & vbCrLf _
            & " " & mFormDueCode & ", '','', '', " & vbCrLf _
            & " '" & mSTType & "','" & mIsRegdNo & "', '" & mLSTCST & "', " & vbCrLf _
            & " '" & mWITHFORM & "', '" & mFOC & "', '" & mPRINTED & "', " & vbCrLf _
            & " '" & mCancelled & "', '" & MainClass.AllowSingleQuote(mNarration) & "',  "


        SqlStr = SqlStr & vbCrLf _
            & "" & mSTPERCENT & "," & mTOTFREIGHT & "," & mEDPERCENT & "," & mTOTTAXABLEAMOUNT & "," & vbCrLf _
            & "" & mSURAmount & "," & mTotDiscount & "," & mMSC & "," & mRO & ",'" & mREJECTION & "','" & mD3 & "', " & vbCrLf _
            & "'" & mPackMat & "','" & mChallanMade & "','', " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','" & mStockTrf & "'," & vbCrLf _
            & " " & mTCSPER & "," & mTCSAMOUNT & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(pDNNo) & "'," & vbCrLf _
            & " TO_DATE('" & VB6.Format(pDNDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            & " " & mTotEDUPercent & ", " & mTotEDUAmount & "," & vbCrLf _
            & " " & mTotServicePercent & "," & mTotServiceAmount & ",'" & MainClass.AllowSingleQuote(pServProvided) & "'," & vbCrLf _
            & " TO_DATE('" & VB6.Format(pSuppFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(pSuppToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
            & " " & Val(pIntRate) & ", '" & mCT3 & "', 0, TO_DATE('" & VB6.Format(mCT3Date, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  0," & vbCrLf _
            & " '" & pDespRef & "', " & Val(pPoNo) & ", TO_DATE('" & VB6.Format(pSoDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), "


        SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(pShippingNo) & "', TO_DATE('" & VB6.Format(pShippingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(pARE1No) & "', TO_DATE('" & VB6.Format(pARE1Date, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(pPortCode) & "', '" & MainClass.AllowSingleQuote(pExportBillNo) & "', TO_DATE('" & VB6.Format(pExportBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " " & Val(pTotExportExp) & "," & Val(pExchangeRate) & ", " & vbCrLf _
                & " " & Val(pTotalEuro) & ", '" & MainClass.AllowSingleQuote(pAdvLicense) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(pLocation) & "', '" & MainClass.AllowSingleQuote(pProcessNature) & "'," & vbCrLf _
                & " " & Val(pMRPValue) & ", '" & mTaxOnMRP & "', " & Val(pAbatementPer) & ", " & vbCrLf _
                & " " & Val(pTotCD) & " , " & Val(pEDUOnCDAmount) & ", 0, 0, " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mBuyerCode) & "', '" & MainClass.AllowSingleQuote(mCoBuyerCode) & "'," & vbCrLf _
                & " " & Val(CStr(mSHECPercent)) & ", " & Val(CStr(mSHECAmount)) & ",'N','" & mDutyForgone & "','" & mDutyFreePurchase & "', " & vbCrLf _
                & " '" & mDutyIncluded & "', 0, 0, 0," & mDivisionCode & "," & vbCrLf _
                & " '" & mCT1 & "',0, TO_DATE('" & VB6.Format(mCT1Date, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mAgtPermission & "'," & Val(mCustMatValue) & "," & vbCrLf _
                & " " & Val(mTotCGSTAmount) & "," & Val(mTotSGSTAmount) & "," & Val(mTotIGSTAmount) & "," & vbCrLf _
                & " '" & mShippedToSame & "','" & mShippedToCode & "','" & Trim(meRefNo) & "'," & Val(pInvoiceSeq) & ",'" & mSACCode & "'," & vbCrLf _
                & " '" & Trim(pAdvVNo) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(pAdvDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(pAdvAdjust) & ", " & vbCrLf _
                & " " & Val(pAdvCGST) & ", " & Val(pAdvSGST) & ", " & Val(pAdvIGST) & ", " & Val(pItemAdvAdjust) & ",'" & mLUT & "', " & vbCrLf _
                & " '" & mTransMode & "', '" & MainClass.AllowSingleQuote(pTransportCode) & "', " & Val(pDistance) & ", " & vbCrLf _
                & " '" & mVehicleType & "', '" & MainClass.AllowSingleQuote(pResponseId) & "','" & MainClass.AllowSingleQuote(pEWayBillNo) & "'," & vbCrLf _
                & " '" & mDespatchFrom & "', '" & MainClass.AllowSingleQuote(mShippedFromCode) & "'," & vbCrLf _
                & " '" & mShippToExWork & "', '" & MainClass.AllowSingleQuote(pBillTo) & "', '" & MainClass.AllowSingleQuote(pShipTo) & "' , '" & MainClass.AllowSingleQuote(pVendorCode) & "', '" & MainClass.AllowSingleQuote(pPacking) & "'" & vbCrLf _
                & " )"

        PubDBCn.Execute(SqlStr)


        SqlStr = " SELECT " & vbCrLf _
                & " * " & vbCrLf _
                & " FROM DSP_DESPATCH_DET " & vbCrLf _
                & " WHERE AUTO_KEY_DESP=" & mDespatchNo & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Dim CntRow As Long = 1
            Do While RsTemp.EOF = False


                CntRow = IIf(IsDBNull(RsTemp.Fields("SERIAL_NO").Value), 0, RsTemp.Fields("SERIAL_NO").Value)
                mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                mRefNo = ""
                mRefDate = ""
                mJITCallNo = IIf(IsDBNull(RsTemp.Fields("JITCALLNO").Value), "", RsTemp.Fields("JITCALLNO").Value)
                mQty = IIf(IsDBNull(RsTemp.Fields("PACKED_QTY").Value), 0, RsTemp.Fields("PACKED_QTY").Value)
                mUnit = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                mMRP = 0
                mTaxableMRP = 0
                mCustItemValue = 0
                mExicseableAmt = 0
                mCessableAmt = 0
                mTotExicseableAmt = 0
                mServiceAmt = 0
                mTotCessableAmt = 0
                mCESSAmt = 0
                mSHECAmt = 0
                mTotSTableAmt = 0
                mSTableAmt = 0
                mNoofStrip = 0
                mStripRate = 0
                mMRRNo = -1
                mODNo = IIf(IsDBNull(RsTemp.Fields("OD_NO").Value), "", RsTemp.Fields("OD_NO").Value)
                mHeatNo = IIf(IsDBNull(RsTemp.Fields("HEAT_NO").Value), "", RsTemp.Fields("HEAT_NO").Value)
                mBatchNo = IIf(IsDBNull(RsTemp.Fields("BATCH_NO").Value), "", RsTemp.Fields("BATCH_NO").Value)
                mColInnerBoxQty = IIf(IsDBNull(RsTemp.Fields("INNER_PACK_QTY").Value), "", RsTemp.Fields("INNER_PACK_QTY").Value)
                mColInnerBoxCode = IIf(IsDBNull(RsTemp.Fields("INNER_PACK_ITEM_CODE").Value), "", RsTemp.Fields("INNER_PACK_ITEM_CODE").Value)
                mColOuterBoxQty = IIf(IsDBNull(RsTemp.Fields("OUTER_PACK_QTY").Value), "", RsTemp.Fields("OUTER_PACK_QTY").Value)
                mColOuterBoxCode = IIf(IsDBNull(RsTemp.Fields("OUTER_PACK_ITEM_CODE").Value), "", RsTemp.Fields("OUTER_PACK_ITEM_CODE").Value)

                mItemSNo = ""
                mPartNo = ""
                mHSNCode = ""
                mAddItemDesc = ""
                mRate = 0
                mAcctCode = ""

                mCGSTPer = 0
                mSGSTPer = 0
                mIGSTPer = 0
                mTotItemValue = 0
                mNETVALUE = 0
                SqlStr = "SELECT ID.*, ITYPE.ACCOUNTPOSTCODE" & vbCrLf _
                            & " FROM  DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, FIN_INVTYPE_MST ITYPE" & vbCrLf _
                            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND IH.MKEY=ID.MKEY AND IH.SUPP_CUST_CODE='" & mSuppCustCode & "'" & vbCrLf _
                            & " AND ID.COMPANY_CODE=ITYPE.COMPANY_CODE AND ID.ACCOUNT_POSTING_CODE=ITYPE.CODE" & vbCrLf _
                            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
                            & " AND IH.AUTO_KEY_SO=" & Val(pPoNo) & " AND SO_APPROVED='Y'" & vbCrLf _
                            & " AND IH.MKEY = ("

                SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf _
                            & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & mSuppCustCode & "'" & vbCrLf _
                            & " AND SIH.AUTO_KEY_SO=" & Val(pPoNo) & " AND SO_APPROVED='Y'" & vbCrLf _
                            & " AND SID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
                            & " AND SID.AMEND_WEF <=TO_DATE('" & VB6.Format(mDCDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                'If pCustLoc = "" Then
                '    SqlStr = SqlStr & vbCrLf & " AND (CUST_STORE_LOC IS NULL OR CUST_STORE_LOC='')"
                'Else
                '    SqlStr = SqlStr & vbCrLf & " AND CUST_STORE_LOC ='" & pCustLoc & "'"
                'End If

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempSO, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTempSO.EOF = False Then
                    mItemSNo = IIf(IsDBNull(RsTempSO.Fields("ITEM_SNO").Value), "", RsTempSO.Fields("ITEM_SNO").Value)
                    mPartNo = IIf(IsDBNull(RsTempSO.Fields("PART_NO").Value), "", RsTempSO.Fields("PART_NO").Value)
                    mHSNCode = IIf(IsDBNull(RsTempSO.Fields("HSN_CODE").Value), "", RsTempSO.Fields("HSN_CODE").Value)
                    mAddItemDesc = IIf(IsDBNull(RsTempSO.Fields("ADD_ITEM_DESCRIPTION").Value), "", RsTempSO.Fields("ADD_ITEM_DESCRIPTION").Value)
                    mRate = IIf(IsDBNull(RsTempSO.Fields("ITEM_PRICE").Value), 0, RsTempSO.Fields("ITEM_PRICE").Value)
                    mAcctCode = IIf(IsDBNull(RsTempSO.Fields("ACCOUNT_POSTING_CODE").Value), 0, RsTempSO.Fields("ACCOUNT_POSTING_CODE").Value)

                    mCGSTPer = IIf(IsDBNull(RsTempSO.Fields("CGST_PER").Value), 0, RsTempSO.Fields("CGST_PER").Value)
                    mSGSTPer = IIf(IsDBNull(RsTempSO.Fields("SGST_PER").Value), 0, RsTempSO.Fields("SGST_PER").Value)
                    mIGSTPer = IIf(IsDBNull(RsTempSO.Fields("IGST_PER").Value), 0, RsTempSO.Fields("IGST_PER").Value)
                End If

                mAmount = VB6.Format(Val(mQty * mRate), "0.00")
                mTaxableAmount = VB6.Format(Val(mQty * mRate), "0.00")
                mCGSTAmount = VB6.Format(Val(mTaxableAmount * mCGSTPer * 0.01), "0.00")
                mSGSTAmount = VB6.Format(Val(mTaxableAmount * mSGSTPer * 0.01), "0.00")
                mIGSTAmount = VB6.Format(Val(mTaxableAmount * mIGSTPer * 0.01), "0.00")

                mTotItemValue = mAmount
                If mSameGSTNo = "Y" Then
                    mNETVALUE = mAmount
                Else
                    mNETVALUE = mAmount + mCGSTAmount + mSGSTAmount + mIGSTAmount
                End If

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemDesc = MasterNo
                    mItemDesc = MainClass.AllowSingleQuote(mItemDesc)
                End If

                SqlStr = ""

                If mItemCode <> "" And mQty > 0 Then
                    SqlStr = " INSERT INTO FIN_INVOICE_DET ( " & vbCrLf _
                    & " MKEY , AUTO_KEY_INVOICE, SUBROWNO, " & vbCrLf _
                    & " ITEM_CODE , ITEM_DESC, HSNCODE, CUSTOMER_PART_NO,ITEM_SNO,ITEM_QTY, " & vbCrLf _
                    & " ITEM_UOM , ITEM_RATE, ITEM_AMT, GSTABLE_AMT," & vbCrLf _
                    & " ITEM_ED, ITEM_ST,ITEM_CESS,ITEM_SERVICE, " & vbCrLf _
                    & " COMPANY_CODE,ITEM_MRP,ITEM_SHEC,JIT_CALLNO, " & vbCrLf _
                    & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf _
                    & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, NO_OF_STRIP, STRIP_RATE, " & vbCrLf _
                    & " OD_NO, MRR_REF_NO, MRR_REF_DATE, " & vbCrLf _
                    & " OUR_REF_NO, OUR_REF_DATE, " & vbCrLf _
                    & " BATCH_NO, HEAT_NO, ADD_ITEM_DESCRIPTION,INNER_PACK_QTY, INNER_PACK_ITEM_CODE, OUTER_PACK_QTY, OUTER_PACK_ITEM_CODE,ACCOUNT_POSTING_CODE" & vbCrLf _
                    & " ) "

                    SqlStr = SqlStr & vbCrLf _
                    & " VALUES ('" & nMkey & "'," & mAutoKeyNo & ", " & CntRow & ", " & vbCrLf _
                    & " '" & mItemCode & "','" & mItemDesc & "', '" & mHSNCode & "', '" & mPartNo & "', '" & mItemSNo & "', " & mQty & ", " & vbCrLf _
                    & " '" & mUnit & "'," & mRate & "," & mAmount & ", " & mTaxableAmount & "," & vbCrLf _
                    & " " & mExicseableAmt & "," & mSTableAmt & "," & mCESSAmt & "," & vbCrLf _
                    & " " & mServiceAmt & "," & RsCompany.Fields("COMPANY_CODE").Value & "," & mMRP & ", " & vbCrLf _
                    & " " & mSHECAmt & ",'" & mJITCallNo & "'," & vbCrLf & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & "," & vbCrLf _
                    & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & ", " & vbCrLf _
                    & " " & mNoofStrip & ", " & mStripRate & ", " & vbCrLf _
                    & " '" & mODNo & "', " & mMRRNo & ", '', " & vbCrLf _
                    & " '" & mRefNo & "',TO_DATE('" & VB6.Format(mRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " '" & mBatchNo & "', '" & mHeatNo & "', '" & mAddItemDesc & "'," & mColInnerBoxQty & ",'" & mColInnerBoxCode & "'," & mColOuterBoxQty & ",'" & mColOuterBoxCode & "','" & MainClass.AllowSingleQuote(mAcctCode) & "'" & vbCrLf _
                    & " ) "

                    PubDBCn.Execute(SqlStr)

                    If mSameGSTNo = "Y" Then

                    Else
                        If mCGSTAmount + mSGSTAmount + mIGSTAmount > 0 Then
                            mOBillNo = ""
                            mOBillDate = ""

                            mGoodsServices = "G"

                            If UpdateGSTTRN(PubDBCn, (nMkey), mBookCode, mBookType, mBookSubType, mBillNo, mBillDate, mBillNo, mBillDate, mOBillNo, mOBillDate,
                                            mSuppCustCode, mAccountCode, mShippedToSame, mShippedToCode, I, mItemCode, mQty, mUnit, mRate, mAmount, mTaxableAmount,
                                            mMRP, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount,
                                            mDivisionCode, mHSNCode, Trim(mItemDesc), mPOS, "N", pDespRef, mGoodsServices, "N", "D", mBillDate, "N") = False Then GoTo ErrPart

                        End If
                    End If
                End If
                RsTemp.MoveNext()
                'CntRow = CntRow + 1
            Loop
        End If

        SqlStr = "UPDATE FIN_INVOICE_HDR SET ITEMVALUE=" & mAmount & ", " & vbCrLf _
                & " TOTTAXABLEAMOUNT=" & Val(mTaxableAmount) & "," & vbCrLf _
                & " NETVALUE=" & Val(mNETVALUE) & "," & vbCrLf _
                & " TOTQTY=" & Val(mQty) & "," & vbCrLf _
                & " NETCGST_AMOUNT = " & Val(mCGSTAmount) & "," & vbCrLf _
                & " NETSGST_AMOUNT = " & Val(mSGSTAmount) & "," & vbCrLf _
                & " NETIGST_AMOUNT = " & Val(mIGSTAmount) & "" & vbCrLf _
                & " WHERE MKEY='" & nMkey & "' " & vbCrLf _
                & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""

        PubDBCn.Execute(SqlStr)

        Dim mExpCode As Double
        If mCGSTAmount <> 0 Then
            If MainClass.ValidateWithMasterTable("CGS", "IDENTIFICATION", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mExpCode = MasterNo
                SqlStr = "Insert Into  FIN_INVOICE_EXP (MKEY,SUBROWNO, " & vbCrLf _
                        & "EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO,DUTYFORGONE) " & vbCrLf _
                        & "Values ('" & nMkey & "',1, " & vbCrLf _
                        & "" & mExpCode & ",0," & mCGSTAmount & ",0,'N','N')"

                PubDBCn.Execute(SqlStr)
            End If
        End If

        If mSGSTAmount <> 0 Then
            If MainClass.ValidateWithMasterTable("SGS", "IDENTIFICATION", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mExpCode = MasterNo
                SqlStr = "Insert Into  FIN_INVOICE_EXP (MKEY,SUBROWNO, " & vbCrLf _
                        & "EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO,DUTYFORGONE) " & vbCrLf _
                        & "Values ('" & nMkey & "',2, " & vbCrLf _
                        & "" & mExpCode & ",0," & mSGSTAmount & ",0,'N','N')"

                PubDBCn.Execute(SqlStr)
            End If
        End If

        If mIGSTAmount <> 0 Then
            If MainClass.ValidateWithMasterTable("IGS", "IDENTIFICATION", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mExpCode = MasterNo
                SqlStr = "Insert Into  FIN_INVOICE_EXP (MKEY,SUBROWNO, " & vbCrLf _
                        & "EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO,DUTYFORGONE) " & vbCrLf _
                        & "Values ('" & nMkey & "',3, " & vbCrLf _
                        & "" & mExpCode & ",0," & mIGSTAmount & ",0,'N','N')"

                PubDBCn.Execute(SqlStr)
            End If
        End If

        Dim pDueDate As String
        pDueDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, Val(mCreditDaysTo), CDate(mBillDate)))

        If SalePostTRN_GST(PubDBCn, (nMkey), mCurRowNo, (mBookCode), mBookType, mBookSubType, mBillNo, mBillDate,
                mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mNETVALUE)), False, pDueDate, False, "", False,
                mSuppCustCode, mTotServiceAmount, 0, IIf(mSameGSTNo = "Y", 0, Val(mCGSTAmount)),
                IIf(mSameGSTNo = "Y", 0, Val(mIGSTAmount)), IIf(mSameGSTNo = "Y", 0, Val(mSGSTAmount)), True, PubUserID, VB.Format(PubCurrDate, "DD/MM/YYYY"), Val(mTotItemValue), mDivisionCode,
                 "N", 0, 0, 0, Trim(pBillTo)) = False Then GoTo ErrPart


        SqlStr = "UPDATE DSP_DESPATCH_HDR SET DESP_STATUS=1 " & vbCrLf _
                & " WHERE AUTO_KEY_DESP=" & Val(mDespatchNo) & " " & vbCrLf _
                & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""

        PubDBCn.Execute(SqlStr)

        UpdateInvoiceMain1 = True


        Exit Function
ErrPart:
        '    Resume
        UpdateInvoiceMain1 = False
        If Err.Description = "" Then Exit Function
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Function AutoGenSeqBillNo(ByRef mBookType As String, ByRef mBookSubType As String, ByRef pStartingSNo As Double, ByRef mDivisionCode As Double, pBillNoPrefix As String) As String

        On Error GoTo AutoGenSeqBillNoErr
        Dim RsSaleMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqBillNo As Integer
        Dim mStartingSNo As Double
        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xFYear As Integer
        Dim mPrefix As Double
        Dim mMaxValue As String
        Dim mSeqNo As Double
        Dim mFormat As String
        Dim mBillPrefix As String
        Dim SqlStr As String

        SqlStr = ""

        xFYear = CInt(VB6.Format(RsCompany.Fields("Start_Date").Value, "YY"))

        mBillPrefix = IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value)

        'If mBillPrefix = "" Then
        mStartingSNo = CDbl(VB6.Format(pStartingSNo, ConBillFormat))


        SqlStr = ""


        SqlStr = "SELECT Max(BILLNOSEQ)  FROM FIN_INVOICE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND BookType='" & mBookType & "'" ''& vbCrLf |            & " AND BookSubType  IN ( "							

        ''31/03/2022
        'SqlStr = SqlStr & vbCrLf & " AND INVOICESEQTYPE=" & Val(lblInvoiceSeq.Text) & ""

        If Trim(pBillNoPrefix) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND BILLNOPREFIX='" & Trim(pBillNoPrefix) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSaleMainGen
            If .EOF = False Then

                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mSeqNo = mMaxValue + 1     '' Mid(mMaxValue, 6, Len(mMaxValue) - 5) + 1

                    'mNewSeqBillNo = .Fields(0).Value + 1
                Else
                    mSeqNo = mStartingSNo
                    'mNewSeqBillNo = mStartingSNo
                End If
            Else
                mSeqNo = mStartingSNo
                'mNewSeqBillNo = mStartingSNo
            End If
        End With

        mNewSeqBillNo = mSeqNo      ''VB6.Format(mSeqNo, mFormat)

        ''& vb6.Format(IIf(IsNull(RsCompany!INVOICE_PREFIX), "", RsCompany!INVOICE_PREFIX), "00") & Val(lblInvoiceSeq.text)							

        AutoGenSeqBillNo = CStr(mNewSeqBillNo)
        Exit Function
AutoGenSeqBillNoErr:
        '    Resume							
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        If FieldsVerification() = False Then Exit Sub
        'MainClass.ClearGrid(SprdMain, RowHeight)
        'OptSelection(1).Checked = True
        Show1("S")
        CmdSave.Enabled = True
        FormatSprdMain()
        cmdShow.Enabled = True
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.Focus() : Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus

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

        If CheckValidBillDate() = False Then
            MsgInformation("Invoice Made after rundate. please Check Rundate.")
            FieldsVerification = False
            Exit Function
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Function CheckValidBillDate() As Boolean

        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim mRsCheck1 As ADODB.Recordset = Nothing
        Dim mBackBillDate As String = ""
        Dim mMaxInvStrfNo As Integer

        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckValidBillDate = True

        If RsCompany.Fields("STOCKBALCHECK").Value = "N" Then
            Exit Function
        End If

        mSeparateSeries = IIf(IsDBNull(RsCompany.Fields("SEPARATE_INV_SERIES").Value), "N", RsCompany.Fields("SEPARATE_INV_SERIES").Value)

        SqlStr = "SELECT MAX(INVOICE_DATE)" & vbCrLf _
            & " FROM FIN_INVOICE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BOOKCode = " & Val(ConSalesBookCode) & " " & vbCrLf & " AND BookType='" & ConSaleBook & "' " & vbCrLf _
            & " AND INVOICESEQTYPE=1 " & vbCrLf _
            & " AND INVOICE_DATE>TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        ''If mSeparateSeries = "Y" Then
        'SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
        ''End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck1, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck1.EOF = False And Not IsDBNull(mRsCheck1.Fields(0).Value) Then
            CheckValidBillDate = False
        End If

        Exit Function
CheckERR:
        CheckValidBillDate = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Sub FrmInvoiceAgtDI_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FrmInvoiceAgtDI_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo BSLError
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

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

        cboShow.Items.Clear()
        cboShow.Items.Add("ALL")
        cboShow.Items.Add("PENDING")
        cboShow.Items.Add("COMPLETE")

        cboShow.SelectedIndex = 0

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False

        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        FormatSprdMain()
        Show1("L")
        cmdShow.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1(pShowType As String)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mDivision As Double

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)

        'If VB.Left(lblBookType.Text, 2) = "II" Then  ''I Invoice , I - IRN


        SqlStr = "SELECT IH.AUTO_KEY_DESP, IH.DESP_DATE,DIV_CODE, ACM.SUPP_CUST_NAME,  IH.BILL_TO_LOC_ID, IH.AUTO_KEY_SO," & vbCrLf _
                & " ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, IMST.CUSTOMER_PART_NO, " & vbCrLf _
                & " ID.LOC_CODE, ID.OD_NO, 0, PACKED_QTY, " & vbCrLf _
                & " '' "

        SqlStr = SqlStr & vbCrLf _
                & " FROM DSP_DI_HDR IH, DSP_DI_DET ID, FIN_SUPP_CUST_MST ACM, INV_ITEM_MST IMST" & vbCrLf _
                & " WHERE IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " And IH.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND ID.ITEM_CODE=IMST.ITEM_CODE " & vbCrLf _
                & " And IH.Company_Code=IMST.Company_Code " & vbCrLf _
                & " And IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IS_INVOICE_MADE='N' AND PACKED_QTY>0"

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "And IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.DESP_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.DESP_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If pShowType = "L" Then
            SqlStr = SqlStr & vbCrLf & "And 1=2"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.AUTO_KEY_DESP"


        'MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        ClearGroupFromUltraGrid(UltraGrid1)
        ClearFilterFromUltraGrid(UltraGrid1)

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        Me.UltraGrid1.DataSource = ds
        Me.UltraGrid1.DataMember = ""

        CreateGridHeader()


        oledbAdapter.Dispose()
        oledbCnn.Close()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
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
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Delivery No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Delivery Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Division"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Customer Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "Location"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "SO No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Item Code"

            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "Item Desc"
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "Customer Part No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Header.Caption = "Customer Location"
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Header.Caption = "OD"
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Header.Caption = "Stock Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Header.Caption = "Item Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Header.Caption = "Status"


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColFlag - 1).Style = UltraWinGrid.ColumnStyle.CheckBox
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStockQty - 1).Style = UltraWinGrid.ColumnStyle.Double

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStockQty - 1).CellAppearance.TextHAlign = HAlign.Right

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment

                'UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.FixedHeaderIndicator = FixedHeaderIndicator.None
                'UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Fixed = True
            Next

            For inti = 0 To UltraGrid1.Rows.Count - 1
                UltraGrid1.Rows(inti).Cells(ColFlag - 1).Value = False
            Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColFlag - 1).CellActivation = Activation.AllowEdit
            UltraGrid1.DisplayLayout.Bands(0).Columns(CoDivision - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSONO - 1).Hidden = True


            'col = Me.UltraGrid1.DisplayLayout.Bands(0).Columns(intLoop + 1)
            'strCelltype = col.Style

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Width = 70
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Width = 0
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Width = 50
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Width = 50
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Width = 85
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Width = 85
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Width = 20

            'UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.MFGQty).MaskInput = "99999"

            'Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            'Me.UltraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

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
    Private Sub FormatSprdMain()
        'With SprdMain

        '    .MaxCols = ColFlag
        '    .set_RowHeight(0, RowHeight * 1.5)
        '    .set_ColWidth(0, 4.5)
        '    .set_RowHeight(-1, RowHeight)

        '    .Row = -1

        '    .Col = ColMKey
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .set_ColWidth(ColMKey, 11)
        '    .ColHidden = True

        '    .Col = CoDIDate
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .set_ColWidth(CoDIDate, 8)

        '    .Col = CoDivision
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(CoDivision, 5)
        '    .ColHidden = False

        '    .Col = ColCustomerName
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColCustomerName, 25)

        '    .Col = ColLocation
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColLocation, 8)

        '    .Col = ColItemCode
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = False
        '    .set_ColWidth(ColItemCode, 8)

        '    .Col = ColItemDesc
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = False
        '    .set_ColWidth(ColItemDesc, 25)

        '    .Col = ColPartNo
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = False
        '    .set_ColWidth(ColPartNo, 14)

        '    .Col = ColStoreLoc
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = False
        '    .set_ColWidth(ColStoreLoc, 6)

        '    .Col = ColOD
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = False
        '    .set_ColWidth(ColOD, 6)


        '    .Col = ColStockQty
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = False
        '    .set_ColWidth(ColStockQty, 8)

        '    .Col = ColQty
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = False
        '    .set_ColWidth(ColQty, 8)

        '    .Col = ColFlag
        '    .CellType = SS_CELL_TYPE_CHECKBOX
        '    .TypeHAlign = SS_CELL_H_ALIGN_CENTER
        '    .set_ColWidth(ColFlag, 8)
        '    .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

        '    MainClass.SetSpreadColor(SprdMain, -1)

        '    MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColQty)
        '    '    SprdMain.OperationMode = OperationModeSingle
        '    '    SprdMain.DAutoCellTypes = True
        '    '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        '    '    SprdMain.GridColor = &HC00000
        'End With
        WriteColHeadings()
    End Sub
    Private Sub WriteColHeadings()
        'With SprdMain
        '    .Row = 0

        '    .Col = ColMKey
        '    .Text = "DI No"

        '    .Col = CoDIDate
        '    .Text = "DI Date"

        '    .Col = CoDivision
        '    .Text = "Division"

        '    .Col = ColCustomerName
        '    .Text = "Customer Name"

        '    .Col = ColLocation
        '    .Text = "Customer Location"

        '    .Col = ColItemCode
        '    .Text = "Item Code"

        '    .Col = ColItemDesc
        '    .Text = "Item Description"


        '    .Col = ColPartNo
        '    .Text = "Part No"


        '    .Col = ColStoreLoc
        '    .Text = "Store Location"

        '    .Col = ColOD
        '    .Text = "OD No"


        '    .Col = ColStockQty
        '    .Text = "Stock Qty"

        '    .Col = ColQty
        '    .Text = "DI Qty"

        '    .Col = ColFlag
        '    .Text = "Generate (Yes/No)"

        'End With
    End Sub
    Private Sub FrmInvoiceAgtDI_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub
    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        'If eventSender.Checked Then
        '    Dim Index As Short = OptSelection.GetIndex(eventSender)
        '    Dim cntRow As Integer
        '    With SprdMain
        '        For cntRow = 1 To .MaxRows
        '            .Row = cntRow
        '            .Col = ColFlag
        '            .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        '        Next
        '    End With
        'End If
    End Sub
    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent)
        'SprdMain.Row = -1
        'SprdMain.Col = eventArgs.col
        'SprdMain.DAutoCellTypes = True
        'SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        'SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        cmdShow.Enabled = True
    End Sub
    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(TxtDateFrom.Text)) = False Then
        '        TxtDateFrom.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        cmdShow.Enabled = True
    End Sub
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
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And SUPP_CUST_TYPE In ('S','C')"
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
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
        Dim SqlStr As String

        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

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

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtAccount.Enabled = True
            cmdsearch.Enabled = True
        End If
        cmdShow.Enabled = True
    End Sub

    Private Sub cmdsearch_Click(sender As Object, e As EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub

    Private Sub TxtAccount_TextChanged(sender As Object, e As EventArgs) Handles TxtAccount.TextChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub _OptSelection_1_Click(sender As Object, e As EventArgs) Handles _OptSelection_1.Click
        cmdShow.Enabled = True
    End Sub

    Private Sub _OptSelection_0_Click(sender As Object, e As EventArgs) Handles _OptSelection_0.Click
        cmdShow.Enabled = True
    End Sub

    Private Sub CmdSave_ClientSizeChanged(sender As Object, e As EventArgs) Handles CmdSave.ClientSizeChanged

    End Sub

    Private Sub FrmInvoiceAgtDI_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
