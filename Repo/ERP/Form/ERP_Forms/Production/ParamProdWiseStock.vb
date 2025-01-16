Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamProdWiseStock
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection
    Private Const RowHeight As Short = 22



    Private Const ColProdCode As Short = 1
    Private Const ColProdName As Short = 2
    Private Const ColProdUnit As Short = 3
    Private Const ColDespQty As Short = 4
    Private Const ColDespQtyAgtD3 As Short = 5
    Private Const ColNetDespQty As Short = 6
    Private Const ColItemCode As Short = 7
    Private Const ColItemName As Short = 8
    Private Const ColUnit As Short = 9
    Private Const ColItemRate As Short = 10
    Private Const colStdQty As Short = 11
    Private Const ColOPStockQty As Short = 12
    Private Const ColPurchaseQty As Short = 13
    Private Const ColIssueQty As Short = 14
    Private Const ColRMDespQty As Short = 15
    Private Const ColClStockQty As Short = 16

    'Private Const mStockQtyStr = "Stock Qty On "
    'Private Const mPlanQtyStr = "Plan Qty On "

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim mFixedCol As Short

    Dim mMaxRow As Integer
    Dim mMaxCol As Integer
    Dim mColWidth As Single


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkAllParty_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllParty.CheckStateChanged
        txtPartyName.Enabled = IIf(chkAllParty.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdPartyName.Enabled = IIf(chkAllParty.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        txtCategoryDesc.Enabled = IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdCategory.Enabled = IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub

    Private Sub chkFG_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFG.CheckStateChanged
        txtFGName.Enabled = IIf(chkFG.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearchFG.Enabled = IIf(chkFG.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub


    Private Sub chkItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkItem.CheckStateChanged
        txtItemName.Enabled = IIf(chkItem.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearchItem.Enabled = IIf(chkItem.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub

    Private Sub cmdCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCategory.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE IN ('C')"

        If MainClass.SearchGridMaster((txtCategoryDesc.Text), "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", , , SqlStr) = True Then
            txtCategoryDesc.Text = AcName
            txtCategoryDesc_Validating(txtCategoryDesc, New System.ComponentModel.CancelEventArgs(False))
            txtCategoryDesc.Focus()
        End If
        Exit Sub

ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdPartyName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPartyName.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((txtPartyName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtPartyName.Text = AcName
            txtPartyName_Validating(txtPartyName, New System.ComponentModel.CancelEventArgs(False))
            txtPartyName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Function UpdateDS(ByRef pAddMode As Boolean, ByRef pDSNo As Double, ByRef pDSdate As String, ByRef pDSAmendNo As Integer, ByRef pDSAmendDate As String, ByRef pPONO As Double, ByRef mPartyCode As String, ByRef pSchdDate As String, ByRef mDSPost As String, ByRef mSchdStatus As String, ByRef pPODate As String, ByRef pAmendNo As Integer, ByRef pAmendDate As String, ByRef pWEFDate As String, ByRef pItemCode As String, ByRef pUnit As String, ByRef pDSQty As Double, ByRef mPackingStd As Double) As Boolean

        On Error GoTo ErrPart

        Dim SqlStr As String = ""


        If pAddMode = True Then
            SqlStr = " INSERT INTO PUR_DELV_SCHLD_HDR ( " & vbCrLf & "  COMPANY_CODE , AUTO_KEY_DELV," & vbCrLf & "  DELV_SCHLD_DATE , DELV_AMEND_NO," & vbCrLf & "  DELV_AMEND_DATE , AUTO_KEY_PO," & vbCrLf & "  SUPP_CUST_CODE , SCHLD_DATE," & vbCrLf & "  EMP_CODE , SCHLD_STATUS," & vbCrLf & "  REMARKS , POST_FLAG," & vbCrLf & "  PO_DATE , PO_AMEND_NO," & vbCrLf & "  AMEND_DATE , AMEND_WEF_DATE, " & vbCrLf & "  ADDUSER, ADDDATE, MODUSER, MODDATE, IS_MAIL) "

            SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & pDSNo & ", TO_DATE('" & VB6.Format(pDSdate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(CStr(pDSAmendNo)) & ", TO_DATE('" & VB6.Format(pDSAmendDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(CStr(pPONO)) & ", '" & MainClass.AllowSingleQuote(mPartyCode) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(pSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '', '" & mSchdStatus & "'," & vbCrLf & " '', 'N', TO_DATE('" & VB6.Format(pPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(CStr(pAmendNo)) & ", TO_DATE('" & VB6.Format(pAmendDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " TO_DATE('" & VB6.Format(pWEFDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','N')"

            PubDBCn.Execute(SqlStr)
        ElseIf mDSPost = "N" Then
            SqlStr = " UPDATE PUR_DELV_SCHLD_HDR SET " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_DELV =" & Val(CStr(pDSNo)) & ""
            PubDBCn.Execute(SqlStr)
        ElseIf mDSPost = "Y" Then
            SqlStr = " UPDATE PUR_DELV_SCHLD_HDR SET " & vbCrLf & " AUTO_KEY_DELV= " & pDSNo & "," & vbCrLf & " DELV_SCHLD_DATE=TO_DATE('" & VB6.Format(pDSdate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " DELV_AMEND_NO=" & Val(CStr(pDSAmendNo)) & ", " & vbCrLf & " DELV_AMEND_DATE=TO_DATE('" & VB6.Format(pDSAmendDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " AUTO_KEY_PO=" & Val(CStr(pPONO)) & ", " & vbCrLf & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "', " & vbCrLf & " SCHLD_DATE=TO_DATE('" & VB6.Format(pSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " EMP_CODE=''," & vbCrLf & " SCHLD_STATUS='N'," & vbCrLf & " REMARKS='', " & vbCrLf & " POST_FLAG='N'," & vbCrLf & " PO_DATE=TO_DATE('" & VB6.Format(pPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " PO_AMEND_NO=" & Val(CStr(pAmendNo)) & ", " & vbCrLf & " AMEND_DATE=TO_DATE('" & VB6.Format(pAmendDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " AMEND_WEF_DATE=TO_DATE('" & VB6.Format(pWEFDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_DELV =" & Val(CStr(pDSNo)) & ""
            PubDBCn.Execute(SqlStr)
        End If

        If UpdateDetail1(pDSNo, pItemCode, pUnit, pDSQty, mPartyCode, pSchdDate, mPackingStd, pDSAmendNo, mDSPost) = False Then GoTo ErrPart


        UpdateDS = True
        Exit Function
ErrPart:
        UpdateDS = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function UpdateDetail1(ByRef pDSNo As Double, ByRef pItemCode As String, ByRef mItemUOM As String, ByRef pDSQty As Double, ByRef pPartyCode As String, ByRef pSchdDate As String, ByRef mPackingStd As Double, ByRef pDSAmendNo As Integer, ByRef mDSPost As String) As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mWeek1Qty As Double
        Dim mWeek2Qty As Double
        Dim mWeek3Qty As Double
        Dim mWeek4Qty As Double
        Dim mWeek5Qty As Double


        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mDay As Integer
        Dim mDate As String
        Dim mLastDay As Integer
        Dim mWorkingDays As Double
        Dim mDailyPlanQty As Double
        Dim mDailySchdQty As Double
        Dim mBalQty As Double
        Dim RsTempUOM As ADODB.Recordset = Nothing
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim mExtraApprovalQty As Double
        Dim mTillDatePurQty As Double

        SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR,PURCHASE_COST FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempUOM, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempUOM.EOF = False Then
            '        mIssueUOM = IIf(IsNull(RsTempUOM!ISSUE_UOM), "", RsTempUOM!ISSUE_UOM)
            mPurchaseUOM = IIf(IsDbNull(RsTempUOM.Fields("PURCHASE_UOM").Value), "", RsTempUOM.Fields("PURCHASE_UOM").Value)
            mFactor = IIf(IsDbNull(RsTempUOM.Fields("UOM_FACTOR").Value) Or RsTempUOM.Fields("UOM_FACTOR").Value = 0, 1, RsTempUOM.Fields("UOM_FACTOR").Value)
        End If

        '    mTillDatePurQty = GetTotalPurchaseQty(pItemCode, mItemUOM, pPartyCode, pSchdDate)
        '    mExtraApprovalQty = GetExtraApprovalQty(pItemCode, mItemUOM, pPartyCode, pSchdDate)
        '
        '    pDSQty = pDSQty + mExtraApprovalQty
        '
        '    If mTillDatePurQty > pDSQty Then
        '        pDSQty = pDSQty
        '    End If

        pDSQty = System.Math.Round(pDSQty / mFactor, 0)
        mPackingStd = System.Math.Round(mPackingStd / mFactor, 0)

        mLastDay = MainClass.LastDay(Month(CDate(pSchdDate)), Year(CDate(pSchdDate)))

        I = 1
        SqlStr = "SELECT SERIAL_NO FROM PUR_DELV_SCHLD_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_DELV=" & Val(CStr(pDSNo)) & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            I = IIf(IsDbNull(RsTemp.Fields("SERIAL_NO").Value), 1, RsTemp.Fields("SERIAL_NO").Value)
        Else
            SqlStr = "SELECT MAX(SERIAL_NO) AS SERIAL_NO FROM PUR_DELV_SCHLD_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_DELV=" & Val(CStr(pDSNo)) & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                I = IIf(IsDbNull(RsTemp.Fields("SERIAL_NO").Value), 1, RsTemp.Fields("SERIAL_NO").Value)
            End If
        End If

        SqlStr = "DELETE FROM TEMP_PUR_DAILY_SCHLD_DET WHERE UserId='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        If mDSPost = "N" Then
            SqlStr = "DELETE FROM PUR_DAILY_SCHLD_HIS_DET  " & vbCrLf & " WHERE AUTO_KEY_DELV=" & Val(CStr(pDSNo)) & " " & vbCrLf & " AND DELV_AMEND_NO=" & Val(CStr(pDSAmendNo)) & " " & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

            PubDBCn.Execute(SqlStr)
        End If

        SqlStr = "DELETE FROM PUR_DAILY_SCHLD_DET  " & vbCrLf & " WHERE AUTO_KEY_DELV=" & Val(CStr(pDSNo)) & " " & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        PubDBCn.Execute(SqlStr)


        SqlStr = "DELETE FROM PUR_DELV_SCHLD_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_DELV=" & Val(CStr(pDSNo)) & " " & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        PubDBCn.Execute(SqlStr)


        For mDay = 1 To mLastDay
            mDate = VB6.Format(mDay & "/" & VB6.Format(pSchdDate, "MM/YYYY"), "DD/MM/YYYY")
            If IsHoliday(mDate) = False Then
                mWorkingDays = mWorkingDays + 1
            End If
        Next

        If mWorkingDays > 0 Then
            mDailyPlanQty = System.Math.Round(pDSQty / mWorkingDays, 0)
        End If

        If mPackingStd > 0 Then
            mDailyPlanQty = mDailyPlanQty / mPackingStd
            mDailyPlanQty = IIf(Int(mDailyPlanQty) = mDailyPlanQty, mDailyPlanQty, Int(mDailyPlanQty) + 1) * mPackingStd
        End If


        mBalQty = pDSQty
        For mDay = 1 To mLastDay
            mDate = VB6.Format(mDay & "/" & VB6.Format(pSchdDate, "MM/YYYY"), "DD/MM/YYYY")
            If IsHoliday(mDate) = False Then
                If mBalQty > mDailyPlanQty Then
                    mDailySchdQty = mDailyPlanQty
                Else
                    mDailySchdQty = mBalQty
                End If
                mBalQty = mBalQty - mDailySchdQty
                If mBalQty < 0 Then
                    mBalQty = 0
                End If
            Else
                mDailySchdQty = 0
            End If

            If mDay < 8 Then
                mWeek1Qty = mWeek1Qty + mDailySchdQty
            ElseIf mDay < 15 Then
                mWeek2Qty = mWeek2Qty + mDailySchdQty
            ElseIf mDay < 22 Then
                mWeek3Qty = mWeek3Qty + mDailySchdQty
            ElseIf mDay < 29 Then
                mWeek4Qty = mWeek4Qty + mDailySchdQty
            Else
                mWeek5Qty = mWeek5Qty + mDailySchdQty
            End If

            SqlStr = "INSERT INTO TEMP_PUR_DAILY_SCHLD_DET (" & vbCrLf & " USERID, AUTO_KEY_DELV,  ITEM_CODE, " & vbCrLf & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE )" & vbCrLf & " VALUES ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & Val(CStr(pDSNo)) & ", '" & MainClass.AllowSingleQuote(pItemCode) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & mDailySchdQty & ", 0, " & vbCrLf & " 0, '" & MainClass.AllowSingleQuote(pPartyCode) & "', TO_DATE('" & VB6.Format(pSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

            PubDBCn.Execute(SqlStr)
        Next

        ''SERIAL_NO, " & mDay & ",

        SqlStr = ""

        If pItemCode <> "" Then 'And mTotQty > 0 'If DS Amend Then Print ...
            SqlStr = " INSERT INTO PUR_DELV_SCHLD_DET ( " & vbCrLf & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf & " ITEM_UOM, WEEK1_QTY, WEEK2_QTY, " & vbCrLf & " WEEK3_QTY, WEEK4_QTY, " & vbCrLf & " WEEK5_QTY, TOTAL_QTY, " & vbCrLf & " REC_QTY, SHORT_QTY, COMPANY_CODE) "

            SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(CStr(pDSNo)) & "," & I & ", " & vbCrLf & " '" & pItemCode & "','" & mPurchaseUOM & "', " & vbCrLf & " " & mWeek1Qty & ", " & mWeek2Qty & ", " & vbCrLf & " " & mWeek3Qty & "," & mWeek4Qty & "," & mWeek5Qty & ", " & vbCrLf & " " & pDSQty & "," & vbCrLf & " " & 0 & "," & 0 & "," & RsCompany.Fields("COMPANY_CODE").Value & ") "

            PubDBCn.Execute(SqlStr)

            SqlStr = "INSERT INTO PUR_DAILY_SCHLD_DET (" & vbCrLf & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE )" & vbCrLf & " SELECT " & vbCrLf & " AUTO_KEY_DELV, " & I & ", ITEM_CODE, " & vbCrLf & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE " & vbCrLf & " FROM TEMP_PUR_DAILY_SCHLD_DET " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pPartyCode) & "'" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(pSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            PubDBCn.Execute(SqlStr)

            SqlStr = "INSERT INTO PUR_DAILY_SCHLD_HIS_DET (" & vbCrLf & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE,DELV_AMEND_NO )" & vbCrLf & " SELECT " & vbCrLf & " " & Val(CStr(pDSNo)) & ", " & I & ", ITEM_CODE, " & vbCrLf & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE," & Val(CStr(pDSAmendNo)) & " " & vbCrLf & " FROM TEMP_PUR_DAILY_SCHLD_DET " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pPartyCode) & "'" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(pSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            PubDBCn.Execute(SqlStr)

        End If

        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Function
    Private Function GetExtraApprovalQty(ByRef pItemCode As String, ByRef mItemUOM As String, ByRef pPartyCode As String, ByRef pSchdDate As String) As Double

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetExtraApprovalQty = 0

        SqlStr = " SELECT SUM(APP_QTY) AS APP_QTY " & vbCrLf & " FROM INV_EXCESS_DS_APP_DET  " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pPartyCode) & "'" & vbCrLf & " AND TO_CHAR(SCHD_DATE,'YYYYMM')='" & VB6.Format(pSchdDate, "YYYYMM") & "' AND IS_APPROVED='Y'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetExtraApprovalQty = IIf(IsDbNull(RsTemp.Fields("APP_QTY").Value), 0, RsTemp.Fields("APP_QTY").Value)
        End If

        Exit Function
UpdateDetail1:

        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
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
            GetTotalPurchaseQty = IIf(IsDbNull(RsTemp.Fields("BILL_QTY").Value), 0, RsTemp.Fields("BILL_QTY").Value)
        End If

        Exit Function
UpdateDetail1:

        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Function
    Private Function IsHoliday(ByRef pDate As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        IsHoliday = True
        If IsDate(pDate) Then
            SqlStr = " SELECT HOLIDAY_DATE FROM PAY_HOLIDAY_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HOLIDAY_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                IsHoliday = True
            Else
                IsHoliday = False
            End If
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function AutoGenDSNoSeq() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Integer
        Dim SqlStr As String = ""
        Dim mStartingChk As Double
        Dim mMaxValue As String
        mAutoGen = 1

        mStartingChk = CDbl(50000 & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_DELV)  " & vbCrLf & " FROM PUR_DELV_SCHLD_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DELV,LENGTH(AUTO_KEY_DELV)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        'If PubHO = "Y" Then
        '    SqlStr = SqlStr & " AND AUTO_KEY_DELV<=" & Val(CStr(mStartingChk)) & "  "
        'Else
        '    SqlStr = SqlStr & " AND AUTO_KEY_DELV>" & Val(CStr(mStartingChk)) & "  "
        'End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mAutoGen = mAutoGen + 1
                Else
                    'If PubHO = "Y" Then
                    mAutoGen = 1
                        'Else
                        '    mAutoGen = 50001
                        'End If
                    End If
            End If
        End With

        AutoGenDSNoSeq = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Sub cmdSearchFG_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchFG.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtFGName.Text), "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtFGName.Text = AcName
            txtFGName_Validating(txtFGName, New System.ComponentModel.CancelEventArgs(False))
            txtFGName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItem.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mCategoryCode As String = ""


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        '    If chkFG.Value = vbUnchecked Then
        '        If MainClass.ValidateWithMasterTable(txtFGName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
        '            mCategoryCode = MasterNo
        '            SqlStr = SqlStr & " AND CATEGORY_CODE='" & mCategoryCode & "'"
        '        End If
        '    End If

        '    If MainClass.SearchGridMaster(TxtItemName.Text, "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", , , SqlStr) = True Then
        If MainClass.SearchGridMaster((txtItemName.Text), "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItemName.Text = AcName
            TxtItemName_Validating(TxtItemName, New System.ComponentModel.CancelEventArgs(False))
            txtItemName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        MainClass.ClearGrid(SprdMain, RowHeight)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        Show1()


        FormatSprdMain(True)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        SprdMain.Focus()
        Call PrintStatus(True)
    End Sub
    Private Sub Show1()

        On Error GoTo LedgError
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        Dim mProdCode As String
        Dim mProdName As String
        Dim mProdUnit As String
        Dim mGrossDespQty As Double
        Dim mDespQtyAgtD3 As Double
        Dim mDespQty As Double


        Dim mRMCode As String
        Dim mRMName As String
        Dim mUnit As String
        Dim mStockQty As Double
        Dim mPlanQty As Double
        Dim mPackingStd As Double
        Dim mNetPlanQty As Double
        Dim mMinInv As Double
        Dim mWorkingDays As Double
        Dim mDate As String

        Dim mFGCode As String
        Dim mItemCode As String
        Dim mcntRow As Integer
        Dim mSuppCustCode As String
        Dim mActualSchdQty As Double
        Dim mBalanceQty As Double
        Dim mCatCode As String = ""

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mDate = VB6.Format(lblRunDate.Text, "DD/MM/YYYY")

        mWorkingDays = GetWorkingDays(mDate)

        SqlStr = ""


        SqlStr = " SELECT DISTINCT IH.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC AS PROD_NAME, INVMST.ISSUE_UOM, " & vbCrLf & " SALE_QTY+D3_SALE_QTY AS GROSS_SALE, D3_SALE_QTY, SALE_QTY"

        SqlStr = SqlStr & vbCrLf & " FROM TEMP_INV_PROCESS IH, INV_ITEM_MST INVMST " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND IH.COMPANY_CODE = INVMST.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE = INVMST.ITEM_CODE"


        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TO_CHAR(IH.PROCESS_DATE,'YYYYMM') ='" & VB6.Format(mDate, "YYYYMM") & "' "

        If chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtFGName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtFGName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mFGCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mFGCode) & "' "
            End If
        End If

        If chkItem.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItemName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND IH.RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' "
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCategoryDesc.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtCategoryDesc.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE IN ('C')") = True Then
                mCatCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND INVMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "' "
            End If
        End If

        If chkAllParty.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPartyName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCustCode = MasterNo
                SqlStr = SqlStr & " AND IH.RM_CODE IN (" & vbCrLf & " SELECT DISTINCT ITEM_CODE " & vbCrLf & " FROM FIN_SUPP_CUST_DET" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mSuppCustCode) & "'" & vbCrLf & " AND OP_QTY>0)"
            End If
        End If


        '   SqlStr = SqlStr & vbCrLf _
        ''            & " GROUP BY IH.RM_CODE, INVMST.ITEM_SHORT_DESC,INVMST.MINIMUM_QTY, INVMST.PACK_STD, INVMST.ISSUE_UOM,IH.PROCESS_DATE "

        SqlStr = SqlStr & vbCrLf & " Order By SALE_QTY DESC, IH.PRODUCT_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        mcntRow = 1


        With SprdMain
            If Not RsShow.EOF Then
                Do While Not RsShow.EOF
                    .Row = mcntRow

                    mProdCode = Trim(IIf(IsDbNull(RsShow.Fields("PRODUCT_CODE").Value), "", RsShow.Fields("PRODUCT_CODE").Value))
                    mProdName = IIf(IsDbNull(RsShow.Fields("PROD_NAME").Value), "", RsShow.Fields("PROD_NAME").Value)
                    mProdUnit = IIf(IsDbNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)
                    mGrossDespQty = CDbl(VB6.Format(IIf(IsDbNull(RsShow.Fields("GROSS_SALE").Value), 0, RsShow.Fields("GROSS_SALE").Value), "0.00"))
                    mDespQtyAgtD3 = CDbl(VB6.Format(IIf(IsDbNull(RsShow.Fields("D3_SALE_QTY").Value), 0, RsShow.Fields("D3_SALE_QTY").Value), "0.00"))
                    mDespQty = CDbl(VB6.Format(IIf(IsDbNull(RsShow.Fields("SALE_QTY").Value), 0, RsShow.Fields("SALE_QTY").Value), "0.00"))

                    .Col = ColProdCode
                    .Text = mProdCode
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Col = ColProdName
                    .Text = mProdName
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Col = ColProdUnit
                    .Text = mProdUnit
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Col = ColDespQty
                    .Text = VB6.Format(mGrossDespQty, "0.00")
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Col = ColDespQtyAgtD3
                    .Text = VB6.Format(mDespQtyAgtD3, "0.00")
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Col = ColNetDespQty
                    .Text = VB6.Format(mDespQty, "0.00")
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Row = mcntRow
                    .Col = -1
                    .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 'Gray
                    .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue
                    .Font = VB6.FontChangeBold(.Font, True)
                    .Font = VB6.FontChangeBold(.Font, True)
                    'SprdMain.SetCellBorder(ColProdCode, mcntRow, SprdMain.MaxCols, mcntRow, SS_BORDER_TYPE_OUTLINE, System.Drawing.ColorTranslator.FromOle(0), FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)

                    If ShowProdDetail(mProdCode, mcntRow, mDate, mDespQty) = False Then GoTo LedgError


                    RsShow.MoveNext()
                    If RsShow.EOF = False Then
                        mcntRow = mcntRow + 1
                        .MaxRows = .MaxRows + 1
                    End If
                Loop
            End If
        End With

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        RsShow.Cancel()
        RsShow.Close()
        RsShow = Nothing
        Exit Sub
LedgError:
        MsgInformation(Err.Description)
        '    Resume
    End Sub

    Private Function GetActualDSQty(ByRef nItemCode As String, ByRef mUOM As String, ByRef mPartyCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing '' ADODB.Recordset
        Dim mRMUOM As String
        Dim mPURUOM As String
        Dim mFactor As String
        Dim mSchdQty As Double
        Dim mTotSchdQty As Double

        SqlStr = " SELECT SUM(ID.TOTAL_QTY) AS TOTAL_QTY, ID.ITEM_CODE, ID.ITEM_UOM, INVMST.PURCHASE_UOM, INVMST.UOM_FACTOR " & vbCrLf & " FROM PUR_DELV_SCHLD_HDR IH, PUR_DELV_SCHLD_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf & " AND ID.COMPANY_CODE = INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE = INVMST.ITEM_CODE" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(nItemCode) & "'"

        If Trim(mPartyCode) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & Trim(mPartyCode) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'YYYYMM')='" & VB6.Format(lblYear.Text, "YYYYMM") & "'" & vbCrLf & " GROUP BY ID.ITEM_CODE, ID.ITEM_UOM, INVMST.PURCHASE_UOM,INVMST.UOM_FACTOR"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mSchdQty = 0
        mTotSchdQty = 0
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mSchdQty = IIf(IsDbNull(RsTemp.Fields("TOTAL_QTY").Value), 0, RsTemp.Fields("TOTAL_QTY").Value)
                mRMUOM = IIf(IsDbNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                mPURUOM = IIf(IsDbNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                mFactor = IIf(IsDbNull(RsTemp.Fields("UOM_FACTOR").Value), 1, RsTemp.Fields("UOM_FACTOR").Value)

                If mUOM <> mPURUOM Then
                    mSchdQty = mSchdQty * CDbl(mFactor)
                End If

                mTotSchdQty = mTotSchdQty + mSchdQty

                RsTemp.MoveNext()
            Loop
        End If

        GetActualDSQty = mTotSchdQty
        Exit Function
ErrPart:
        GetActualDSQty = 0
    End Function

    Private Function ShowProdDetail(ByRef pItemCode As String, ByRef mcntRow As Integer, ByRef mDate As String, ByRef pDespatchQty As Double) As Boolean

        On Error GoTo LedgError
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        Dim mRMCode As String
        Dim mRMName As String
        Dim mStdQty As Double
        Dim mRMUOM As String
        Dim mRate As Double

        Dim mOPStock As Double
        Dim mPurchase As Double
        Dim mIssue As Double
        Dim mClStock As Double
        Dim mDespatchQty As Double
        Dim mFGCode As String
        Dim mItemCode As String
        Dim mSuppCustCode As String
        Dim mActualPartySchdQty As Double
        Dim mRatio As Double

        ShowProdDetail = False


        SqlStr = " SELECT IH.SERIAL_NO, IH.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.ISSUE_UOM, " & vbCrLf & " RM_LANDCOST, STD_QTY, OP_STOCK_QTY, PURCHASE_QTY, ISSUE_QTY, RM_QTY, CL_STOCK_QTY " & vbCrLf & " FROM TEMP_INV_PROCESS IH, INV_ITEM_MST INVMST" & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.COMPANY_CODE = INVMST.COMPANY_CODE " & vbCrLf & " AND IH.RM_CODE = INVMST.ITEM_CODE" & vbCrLf & " AND IH.PRODUCT_CODE = '" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND TO_CHAR(IH.PROCESS_DATE,'YYYYMM') ='" & VB6.Format(mDate, "YYYYMM") & "' "


        If chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtFGName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtFGName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mFGCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mFGCode) & "' "
            End If
        End If

        If chkItem.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItemName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND IH.RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' "
            End If
        End If


        If chkAllParty.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPartyName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCustCode = MasterNo
                SqlStr = SqlStr & " AND IH.RM_CODE IN (" & vbCrLf & " SELECT DISTINCT ITEM_CODE " & vbCrLf & " FROM FIN_SUPP_CUST_DET" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mSuppCustCode) & "'" & vbCrLf & " AND OP_QTY>0)"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdMain
            If Not RsTemp.EOF Then

                mcntRow = mcntRow + 1
                SprdMain.MaxRows = SprdMain.MaxRows + 1

                Do While Not RsTemp.EOF
                    .Row = mcntRow

                    mRMCode = IIf(IsDbNull(RsTemp.Fields("RM_CODE").Value), "", RsTemp.Fields("RM_CODE").Value)
                    mRMName = IIf(IsDbNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)
                    mRMUOM = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                    mStdQty = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value), "0.00"))
                    mRate = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("RM_LANDCOST").Value), 0, RsTemp.Fields("RM_LANDCOST").Value), "0.00"))

                    mRatio = GetDespatchRatio(pItemCode, mRMCode, pDespatchQty)

                    mOPStock = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("OP_STOCK_QTY").Value), 0, RsTemp.Fields("OP_STOCK_QTY").Value), "0.00"))
                    mPurchase = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("PURCHASE_QTY").Value), 0, RsTemp.Fields("PURCHASE_QTY").Value), "0.00"))
                    mIssue = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("ISSUE_QTY").Value), 0, RsTemp.Fields("ISSUE_QTY").Value), "0.00"))
                    mClStock = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("CL_STOCK_QTY").Value), 0, RsTemp.Fields("CL_STOCK_QTY").Value), "0.00"))
                    mDespatchQty = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("RM_QTY").Value), 0, RsTemp.Fields("RM_QTY").Value), "0.00"))

                    mOPStock = System.Math.Round(mOPStock * mRatio, 0)
                    mPurchase = System.Math.Round(mPurchase * mRatio, 0)
                    mIssue = System.Math.Round(mIssue * mRatio, 0)
                    mClStock = System.Math.Round(mClStock * mRatio, 0)


                    .Col = ColItemCode
                    .Text = mRMCode

                    .Col = ColItemName
                    .Text = mRMName

                    .Col = ColUnit
                    .Text = mRMUOM

                    .Col = colStdQty
                    .Text = VB6.Format(mStdQty, "0.00")

                    .Col = ColItemRate
                    .Text = VB6.Format(mRate, "0.00")

                    .Col = ColOPStockQty
                    .Text = VB6.Format(mOPStock, "0.00")

                    .Col = ColPurchaseQty
                    .Text = VB6.Format(mPurchase, "0.00")

                    .Col = ColIssueQty
                    .Text = VB6.Format(mIssue, "0.00")

                    .Col = ColRMDespQty
                    .Text = VB6.Format(mDespatchQty, "0.00")

                    .Col = ColClStockQty
                    .Text = VB6.Format(mClStock, "0.00")


                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        mcntRow = mcntRow + 1
                        .MaxRows = .MaxRows + 1
                    End If
                Loop
            End If
        End With

        RsTemp.Cancel()
        RsTemp.Close()
        RsTemp = Nothing
        ShowProdDetail = True
        Exit Function
LedgError:
        MsgInformation(Err.Description)
        ShowProdDetail = False
        '    Resume
    End Function

    Private Function GetDespatchRatio(ByRef pProdCode As String, ByRef mRMCode As String, ByRef pDespatchQty As Double) As Double

        On Error GoTo LedgError
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mTotalDespatch As Double

        GetDespatchRatio = 0


        SqlStr = " SELECT SUM(SALE_QTY) AS SALE_QTY " & vbCrLf & " FROM TEMP_INV_PROCESS IH" & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.RM_CODE = '" & MainClass.AllowSingleQuote(mRMCode) & "'" & vbCrLf & " AND TO_CHAR(IH.PROCESS_DATE,'YYYYMM') ='" & VB6.Format(lblRunDate.Text, "YYYYMM") & "' "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If Not RsTemp.EOF Then
            mTotalDespatch = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("SALE_QTY").Value), 0, RsTemp.Fields("SALE_QTY").Value), "0.00"))
        End If

        If mTotalDespatch = 0 Then
            GetDespatchRatio = 0
        Else
            GetDespatchRatio = CDbl(VB6.Format(pDespatchQty / mTotalDespatch, "0.0000"))
        End If

        Exit Function
LedgError:
        MsgInformation(Err.Description)
        '    Resume
    End Function

    Private Function GetLastPORate(ByRef pItemCode As String, ByRef mSuppCustCode As String, ByRef xScheduleDate As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetLastPORate = CStr(0)

        If mSuppCustCode = "" Then
            SqlStr = " SELECT IH.SUPP_CUST_CODE" & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND IH.REF_TYPE='P'" & vbCrLf & " ORDER BY IH.MRR_DATE DESC "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

            If RsTemp.EOF = False Then
                mSuppCustCode = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
            Else
                GetLastPORate = CStr(0)
                Exit Function
            End If
        End If

        SqlStr = " SELECT NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,2) AS PO_RATE" & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.PO_STATUS='Y' AND IH.PUR_TYPE='P' AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "'"

        If CDate(xScheduleDate) < CDate(PubGSTApplicableDate) Then
            SqlStr = SqlStr & vbCrLf & " AND ISGSTENABLE_PO='N'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND ISGSTENABLE_PO='Y'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND AMEND_WEF_DATE=(" & vbCrLf & " SELECT MAX(AMEND_WEF_DATE) " & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.PO_STATUS='Y' AND IH.PUR_TYPE='P' AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "'"

        If CDate(xScheduleDate) < CDate(PubGSTApplicableDate) Then
            SqlStr = SqlStr & vbCrLf & " AND ISGSTENABLE_PO='N'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND ISGSTENABLE_PO='Y'"
        End If

        SqlStr = SqlStr & vbCrLf & " )"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            GetLastPORate = IIf(IsDbNull(RsTemp.Fields("PO_RATE").Value), 0, RsTemp.Fields("PO_RATE").Value)
        End If
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Function
    Private Sub ShowDetail1()
        'On Error GoTo LedgError
        'Dim RsShow As ADODB.Recordset=Nothing
        'Dim SqlStr As String = ""
        '
        'Dim mProcessDate As String
        'Dim mProdCode As String
        'Dim mProdName As String
        'Dim mRMCode As String
        'Dim mRMName As String
        'Dim mStdQty As Double
        'Dim mUnit As String
        'Dim mStockQty As Double
        'Dim mPlanQty As Double
        '
        'Dim mSFFRowNo  As Long
        'Dim mcntRow As Long
        'Dim cntCol As Long
        'Dim mPartyName As String
        'Dim mFGPlan As Double
        'Dim mDiff As Double
        'Dim mMinInv As Double
        'Dim mWorkingDays As Double
        'Dim mDate As String
        '
        '
        '    Screen.MousePointer = vbHourglass
        '
        '    mDate = VB6.Format(lblRunDate.text, "DD/MM/YYYY")
        '
        '    mWorkingDays = GetWorkingDays(mDate)
        '
        '    SqlStr = ""
        '
        '
        '    SqlStr = " SELECT TO_CHAR(IH.PROCESS_DATE,'DD-MM-YYYY') AS PROCESS_DATE,IH.PRODUCT_CODE, " & vbCrLf _
        ''            & " ITEM1.ITEM_SHORT_DESC AS P_NAME,ITEM1.ISSUE_UOM AS P_UOM,IH.DPLAN_QTY, " & vbCrLf _
        ''            & " IH.RM_CODE,ITEM2.ITEM_SHORT_DESC AS RM_NAME,IH.STD_QTY, " & vbCrLf _
        ''            & " ITEM2.ISSUE_UOM AS RM_UOM,IH.STOCK_QTY,IH.RM_QTY,ITEM2.ECONOMIC_QTY "
        '
        '    SqlStr = MakeCondSQL(SqlStr, True, False)
        '
        '
        '    SqlStr = SqlStr & vbCrLf & " Order By IH.RM_CODE, IH.PRODUCT_CODE, IH.PROCESS_DATE "
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsShow, adLockReadOnly
        '
        '    mcntRow = 0
        '
        '    With SprdMain
        '        If Not RsShow.EOF Then
        '            Do While Not RsShow.EOF
        ''                If Not IsNull(RsShow!PRODUCT_CODE) Then
        '
        '                        mcntRow = mcntRow + 1
        '                        .MaxRows = .MaxRows + 1
        '
        '                        mSFFRowNo = mcntRow
        '
        '                        mProdCode = IIf(IsNull(RsShow!PRODUCT_CODE), "", RsShow!PRODUCT_CODE)
        '                        mProdName = IIf(IsNull(RsShow!P_NAME), "", RsShow!P_NAME)
        '                        mStdQty = Val(IIf(IsNull(RsShow!STD_QTY), "", RsShow!STD_QTY))
        '                        mUnit = IIf(IsNull(RsShow!P_UOM), "", RsShow!P_UOM)
        '                        mProcessDate = VB6.Format(IIf(IsNull(RsShow!PROCESS_DATE), "", RsShow!PROCESS_DATE), "DD-MM-YYYY")
        '                        mStockQty = Val(IIf(IsNull(RsShow!STOCK_QTY), "", RsShow!STOCK_QTY))
        '                        mFGPlan = Val(IIf(IsNull(RsShow!DPLAN_QTY), "", RsShow!DPLAN_QTY))
        '
        '                        .Row = mSFFRowNo
        '
        '                        .Col = ColMainProd
        '                        .Text = "Y"
        '
        '                        .Col = ColProdCode
        '                        .Text = mProdCode
        '                        .FontBold = False
        '
        '                        .Col = ColProdName
        '                        .Text = mProdName
        '                        .FontBold = False
        '
        '                        .Col = ColUnit
        '                        .Text = mUnit
        '                        .FontBold = False
        ''                        .Col = ColStockQty
        ''                        .Text = VB6.Format(mStockQty, "0.00")
        '
        '                        .Col = ColSchdQty
        '                        .Text = VB6.Format(mFGPlan, "0.00")
        '                        .FontBold = False
        '
        ''                        .Col = ColDiff
        ''                        .Text = VB6.Format(mFGPlan, "0.00")
        ''                        .FontBold = True
        '
        ''                        .Row = mSFFRowNo
        ''                        .Row2 = mSFFRowNo
        ''                        .Col = ColPartyName
        ''                        .col2 = ColDiff
        ''                        .BlockMode = True
        ''                        .BackColor = &H8000000F
        ''                        .BlockMode = False
        ''
        ''
        ''                    .Row = mSFFRowNo
        ''                    cntCol = ColUnit + 1
        ''                End If
        ''
        ''                If Not IsNull(RsShow!RM_CODE) Then
        ''                    If mRMCode <> IIf(IsNull(RsShow!RM_CODE), "", RsShow!RM_CODE) And mProdCode = IIf(IsNull(RsShow!PRODUCT_CODE), "", RsShow!PRODUCT_CODE) Then
        ''                        mcntRow = mcntRow + 1
        ''                        .MaxRows = .MaxRows + 1
        '
        '                        mRMCode = Trim(IIf(IsNull(RsShow!RM_CODE), "", RsShow!RM_CODE))
        '                        mRMName = IIf(IsNull(RsShow!RM_NAME), "", RsShow!RM_NAME)
        '                        mStdQty = Val(IIf(IsNull(RsShow!STD_QTY), "", RsShow!STD_QTY))
        '                        mUnit = IIf(IsNull(RsShow!RM_UOM), "", RsShow!RM_UOM)
        '                        mStockQty = Val(IIf(IsNull(RsShow!STOCK_QTY), "", RsShow!STOCK_QTY))
        '
        '                        If optDetSummarised(2).Value = True Then
        '                            mPlanQty = Val(IIf(IsNull(RsShow!PRM_QTY), "", RsShow!PRM_QTY))
        '                            mDiff = Val(IIf(IsNull(RsShow!RM_QTY), "", RsShow!RM_QTY)) ''- mStockQty
        '                        Else
        '                            mPlanQty = Val(IIf(IsNull(RsShow!RM_QTY), "", RsShow!RM_QTY))
        '                        End If
        '
        '                        mMinInv = Round(Val(IIf(IsNull(RsShow!ECONOMIC_QTY), "", RsShow!ECONOMIC_QTY)) * mPlanQty / mWorkingDays, 0)
        '
        '                        If optDetSummarised(2).Value = True Then
        '                            mPartyName = IIf(IsNull(RsShow!SUPP_CUST_NAME), "", RsShow!SUPP_CUST_NAME)
        '                        End If
        '
        '                        .Row = mcntRow
        '
        '                        .Col = ColPartyName
        '                        .Text = mPartyName
        '
        '                        .Col = ColMainProd
        '                        .Text = "N"
        '
        '                        .Col = ColItemCode
        '                        .Text = mRMCode
        '
        '                        .Col = ColItemName
        '                        .Text = mRMName
        '
        '                        .Col = ColStdQty
        '                        .Text = mStdQty
        '
        '                        .Col = ColUnit
        '                        .Text = mUnit
        '
        '                        .Col = ColStockQty
        '                        .Text = VB6.Format(mStockQty, "0.00")
        '
        '                        .Col = ColMinQty
        '                        .Text = VB6.Format(mMinInv, "0.00")
        '
        '                        .Col = ColPlanQty
        '                        .Text = VB6.Format(mPlanQty, "0.00")
        '
        '                        .Col = ColDiff
        '                        If optDetSummarised(2).Value = True Then
        '                            .Text = VB6.Format(IIf(mDiff < 0, 0, mDiff), "0.00")
        '                        Else
        '                            mDiff = mPlanQty - mStockQty
        '                            .Text = VB6.Format(IIf(mDiff < 0, 0, mDiff), "0.00")
        '
        ''                            If mDiff <= 0 Then
        ''                                .Row = mcntRow
        ''                                .Row2 = mcntRow
        ''                                .Col = ColPartyName
        ''                                .col2 = ColDiff
        ''                                .BlockMode = True
        ''                                .ForeColor = vbRed
        ''                                .BlockMode = False
        ''                            End If
        '                        End If
        ''                    End If
        ''                End If
        '                RsShow.MoveNext
        '            Loop
        '        End If
        '    End With
        '    Screen.MousePointer = vbDefault
        '    RsShow.Cancel
        '    RsShow.Close
        '    Set RsShow = Nothing
        Exit Sub
LedgError:
        MsgInformation(Err.Description)
        '    Resume
    End Sub
    Private Function GetWorkingDays(ByRef pDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        GetWorkingDays = MainClass.LastDay(Month(CDate(pDate)), Year(CDate(pDate)))

        SqlStr = " SELECT COUNT(1) AS LDAYS FROM PAY_HOLIDAY_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(HOLIDAY_DATE,'MON-YYYY')='" & UCase(VB6.Format(pDate, "MMM-YYYY")) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetWorkingDays = GetWorkingDays - IIf(IsDbNull(RsTemp.Fields("LDAYS").Value), 0, RsTemp.Fields("LDAYS").Value)
        End If
        Exit Function
ErrPart:
        GetWorkingDays = 0
    End Function
    Public Sub frmParamProdWiseStock_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        FormatSprdMain(False)
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamProdWiseStock_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = VB6.TwipsToPixelsY(24)
        Me.Left = VB6.TwipsToPixelsX(24)
        'Me.Height = VB6.TwipsToPixelsY(7440)
        ''Me.Width = VB6.TwipsToPixelsX(11625)

        CurrFormHeight = 7440
        CurrFormWidth = 11625

        lblRunDate.Text = CStr(RunDate)
        FillMonth(CDate(lblRunDate.Text))
        UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)

        chkFG.CheckState = System.Windows.Forms.CheckState.Checked
        chkFG_CheckStateChanged(chkFG, New System.EventArgs())
        chkItem.CheckState = System.Windows.Forms.CheckState.Checked
        chkItem_CheckStateChanged(chkItem, New System.EventArgs())

        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        chkCategory_CheckStateChanged(chkCategory, New System.EventArgs())

        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain(ByRef mFillColHeading As Boolean)

        Dim cntCol As Integer

        With SprdMain
            .MaxCols = ColClStockQty
            .set_RowHeight(-1, RowHeight * 0.75)

            .Row = -1
            .set_ColWidth(0, 4)

            .Col = ColProdCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColProdCode, 6)

            .Col = ColProdName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColProdName, 25)

            .Col = ColProdUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColProdUnit, 4)

            For cntCol = ColDespQty To ColNetDespQty
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 10)
            Next

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColItemName, 25)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColUnit, 4)

            For cntCol = ColItemRate To ColClStockQty
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 10)
            Next


            '        .Col = ColItemCode
            '        .ColMerge = MergeAlways
            '        .Col = ColItemName
            '        .ColMerge = MergeAlways



            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle

        End With

    End Sub

    Private Sub frmParamProdWiseStock_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamProdWiseStock_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnSchedule(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub InsertIntoPrintdummyData()

        Dim SqlStr As String = ""
        Dim CntRow As Integer
        Dim cntCol As Integer
        Dim mColStart As Integer
        Dim FieldSeq As Integer
        Dim mInsertSQL As String
        Dim mValueSQL As String
        Dim mFieldStr As String
        Dim mValueStr As String

        On Error GoTo ERR1

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdMain
            For CntRow = 1 To .MaxRows - 1
                .Row = CntRow

                mInsertSQL = ""
                mValueSQL = ""
                SqlStr = ""

                mInsertSQL = "Insert into TEMP_PrintDummyData (UserID,SubRow,"
                mValueSQL = " Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & CntRow & ", "


                mColStart = 1


                For cntCol = mColStart To .MaxCols
                    .Col = cntCol


                    FieldSeq = cntCol


                    If cntCol = .MaxCols Then
                        mFieldStr = "FIELD" & FieldSeq
                        mValueStr = "'" & MainClass.AllowSingleQuote(.Text) & "'"
                    Else
                        mFieldStr = "FIELD" & FieldSeq & ","
                        mValueStr = "'" & MainClass.AllowSingleQuote(.Text) & "'" & ","
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
        Exit Sub
ERR1:
        'Resume
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Sub ReportOnSchedule(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim mCustDealer As String
        Dim SqlStr As String = ""

        Report1.Reset()
        SqlStr = ""
        PubDBCn.Execute("DELETE FROM TEMP_PRINTDUMMYDATA WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'")

        MainClass.ClearCRptFormulas(Report1)

        '    Call InsertIntoPrintdummyData

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
        '*************** Fetching Record For Report ***************************
        '    SqlStr = ""
        '    SqlStr = "SELECT * " & vbCrLf _
        ''            & " FROM TEMP_PRINTDUMMYDATA " & vbCrLf _
        ''            & " WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
        ''            & " ORDER BY SUBROW"

        '    If lblBookType.text = Left(ConDespatchPlan, 1) And lblBookSubType.text = Right(ConDespatchPlan, 1) Then
        '        mTitle = "Revised Despatch & Production Planning"
        '        Report1.ReportFileName = App.path & "\Reports\RevDespProdPlan.rpt"
        '    ElseIf lblBookType.text = Left(ConPurchase, 1) And lblBookSubType.text = Right(ConPurchase, 1) Then
        '        mTitle = "MONTHLY SCHEDULE Planning"
        '        If optDetSummarised(0).Value = True Then
        '            Report1.ReportFileName = App.path & "\Reports\Month_schld_plan_Summarised.rpt"
        '        ElseIf optDetSummarised(1).Value = True Then
        '            Report1.ReportFileName = App.path & "\Reports\Month_schld_plan_partywise.rpt"
        '        Else
        '            Report1.ReportFileName = App.path & "\Reports\Month_schld_plan_det_productwise.rpt"
        '        End If
        '    End If
        '
        '    If optDetSummarised(0).Value = True Then
        '        mTitle = mTitle & " [Summarised]"
        '    ElseIf optDetSummarised(1).Value = True Then
        '        mTitle = mTitle & " [Party Wise]"
        '    ElseIf optDetSummarised(2).Value = True Then
        '        mTitle = mTitle & " [Product wise]"
        '    End If
        '
        '    mSubTitle = "FOR THE MONTH OF : " & VB6.Format(lblRunDate.text, "MMM-YYYY")
        '
        '    If chkFG.Value = vbUnchecked And Trim(txtFGName.Text) <> "" Then
        '        mSubTitle = mSubTitle & " [Category : " & txtFGName.Text & "]"
        '    End If
        '    If chkItem.Value = vbUnchecked And Trim(txtItemName.Text) <> "" Then
        '        mSubTitle = mSubTitle & " [Item Name : " & txtItemName.Text & "]"
        '    End If
        '    If chkAllParty.Value = vbUnchecked And Trim(txtPartyName.Text) <> "" Then
        '        mSubTitle = mSubTitle & " [Supplier : " & txtPartyName.Text & "]"
        '    End If
        '
        '    Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Exit Sub



ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        'Dim ii As Integer
        'Dim mHeadStr As String
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        '    With SprdMain
        '        .Row = 0
        '        If optDetSummarised(2).Value = True Then
        '            For ii = ColPartyName To .MaxCols
        '                .Col = ii
        '                mHeadStr = "FldHead" & ii & "=""" & .Text & """"
        '                MainClass.AssignCRptFormulas Report1, mHeadStr
        '            Next
        '
        '        Else
        '            For ii = ColMainProd To .MaxCols
        '                .Col = ii
        '                mHeadStr = "FldHead" & ii - 1 & "=""" & .Text & """"
        '                MainClass.AssignCRptFormulas Report1, mHeadStr
        '            Next
        '        End If
        '    End With
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnSchedule(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub




    Private Sub optDetSummarised_Click(ByRef Index As Short)
        PrintStatus(False)
        '    txtPartyName.Enabled = False
        '    cmdPartyName.Enabled = False
        '    chkAllParty.Value = vbChecked
        '    chkAllParty.Enabled = IIf(optDetSummarised(2).Value = True, True, False)
    End Sub

    Private Sub txtCategoryDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategoryDesc.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub txtCategoryDesc_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategoryDesc.DoubleClick
        Call cmdCategory_Click(cmdCategory, New System.EventArgs())
    End Sub

    Private Sub txtCategoryDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCategoryDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCategoryDesc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCategoryDesc_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCategoryDesc.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdCategory_Click(cmdCategory, New System.EventArgs())
    End Sub

    Private Sub txtCategoryDesc_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCategoryDesc.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtCategoryDesc.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtCategoryDesc.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE IN ('C')") = False Then
            MsgBox("Invalid Category.")
            Cancel = True
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtFGName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFGName.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub txtFGName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFGName.DoubleClick
        Call cmdSearchFG_Click(cmdSearchFG, New System.EventArgs())
    End Sub

    Private Sub txtFGName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFGName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFGName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFGName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtFGName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchFG_Click(cmdSearchFG, New System.EventArgs())
    End Sub

    Private Sub txtFGName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFGName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtFGName.Text) = "" Then GoTo EventExitSub
        '    If MainClass.ValidateWithMasterTable(txtFGName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = False Then
        '        MsgBox "Invalid Category Code."
        '        Cancel = True
        '    Else
        '        lblCatCode.text = MasterNo
        '    End If

        If MainClass.ValidateWithMasterTable((txtFGName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Invalid Item Code.")
            Cancel = True
        Else
            lblCatCode.Text = MasterNo
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPartyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub txtPartyName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.DoubleClick
        Call cmdPartyName_Click(cmdPartyName, New System.EventArgs())
    End Sub

    Private Sub txtPartyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartyName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPartyName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartyName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdPartyName_Click(cmdPartyName, New System.EventArgs())
    End Sub

    Private Sub txtPartyName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartyName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPartyName.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgBox("Invalid Party Code.")
            Cancel = True
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.DoubleClick
        Call cmdSearchItem_Click(cmdSearchItem, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchItem_Click(cmdSearchItem, New System.EventArgs())
    End Sub

    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String = ""
        Dim mCategoryCode As String = ""

        If Trim(txtItemName.Text) = "" Then GoTo EventExitSub
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        '    If chkFG.Value = vbUnchecked Then
        '        If MainClass.ValidateWithMasterTable(txtFGName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
        '            mCategoryCode = MasterNo
        '            SqlStr = SqlStr & " AND CATEGORY_CODE='" & mCategoryCode & "'"
        '        End If
        '    End If

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            '    If MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = False Then
            ErrorMsg("Invalid BOP Item Code.", , MsgBoxStyle.Information)
            Cancel = True
        Else
            lblSubCatCode.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub UpDYear_DownClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
        FillMonth(CDate(lblRunDate.Text))
        'RefreshScreen
    End Sub

    Private Sub UpDYear_UpClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
        FillMonth(CDate(lblRunDate.Text))
        'RefreshScreen
    End Sub
    Private Sub FillMonth(ByRef xDate As Date)

        'Dim Daysinmonth As Integer
        'Dim cntCol As Integer
        Dim Tempdate As String
        'Dim mDay As Integer
        Dim NewDate As Date

        MainClass.ClearGrid(SprdMain)

        Tempdate = "01/" & Month(xDate) & "/" & Year(xDate)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

    End Sub
End Class
