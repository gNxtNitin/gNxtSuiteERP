Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamMonthlySchld
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection						
    Private Const RowHeight As Short = 22

    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColUnit As Short = 3
    Private Const ColStockQty As Short = 4
    Private Const ColMinQty As Short = 5
    Private Const ColPlanQty As Short = 6
    Private Const ColPackingStd As Short = 7
    Private Const ColNetPlanQty As Short = 8
    Private Const ColActualSchdQty As Short = 9
    Private Const ColPartyCode As Short = 10
    Private Const ColPartyName As Short = 11
    Private Const ColSOB As Short = 12
    Private Const ColSchdQty As Short = 13
    Private Const ColActualPartySchdQty As Short = 14
    Private Const ColItemRate As Short = 15
    Private Const ColItemAmount As Short = 16


    Private Const mStockQtyStr As String = "Stock Qty On "
    Private Const mPlanQtyStr As String = "Plan Qty On "

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim mFixedCol As Short

    Dim mMaxRow As Integer
    Dim mMaxCol As Integer
    Dim mColWidth As Single
    Dim mClickProcess As Boolean

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
        cmdProcess.Enabled = pPrintEnable
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
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE IN ('C')"

        If MainClass.SearchGridMaster(txtCategoryDesc.Text, "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", , , SqlStr) = True Then
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
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster(txtPartyName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtPartyName.Text = AcName
            txtPartyName_Validating(txtPartyName, New System.ComponentModel.CancelEventArgs(False))
            txtPartyName.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdProcess_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdProcess.Click
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim CntRow As Integer
        Dim mItemCode As String
        Dim mUnit As String
        Dim mPartyCode As String
        Dim mSchdQty As Double
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim pAddMode As Boolean
        Dim pPONO As Double
        Dim pPODate As String
        Dim pAmendNo As Integer
        Dim pAmendDate As String
        Dim pWEFDate As String
        Dim RsTempPO As ADODB.Recordset
        Dim mDSPost As String
        Dim pDSNo As Double

        Dim pDSdate As String
        Dim pDSAmendNo As Integer
        Dim pDSAmendDate As String
        Dim mSchdStatus As String
        Dim pScheduleDate As String
        Dim mPackingStd As Double
        Dim mActualSchdQty As Double
        Dim mTillDatePurQty As Double
        Dim mExtraApprovalQty As Double

        If optDetSummarised(1).Checked = False Then
            MsgInformation("Please Select Partywise for Run Schedule.")
            Exit Sub
        End If

        If chkAllParty.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MsgInformation("Please Select All Party for Run Schedule.")
            Exit Sub
        End If

        If chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MsgInformation("Please Select All Product for Run Schedule.")
            Exit Sub
        End If


        If chkItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MsgQuestion("Are you want to Run Single BOM/RM Item Schedule. ? ") = CStr(MsgBoxResult.No) Then
                '            MsgInformation "Please Select All BOM/RM Item for Run Schedule."						
                Exit Sub
            End If
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        pScheduleDate = "01/" & VB6.Format(lblYear.Text, "MM/YYYY")
        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColItemCode
                mItemCode = IIf(Trim(.Text) = "", mItemCode, Trim(.Text))

                .Col = ColUnit
                mUnit = IIf(Trim(.Text) = "", mItemCode, Trim(.Text))

                .Col = ColPackingStd
                mPackingStd = IIf(Trim(.Text) = "", mPackingStd, Val(.Text))

                .Col = ColPartyCode
                mPartyCode = Trim(.Text)

                .Col = ColSchdQty
                mSchdQty = Val(.Text)

                .Col = ColActualPartySchdQty
                mActualSchdQty = Val(.Text)


                If mItemCode <> "" Then
                    mTillDatePurQty = GetTotalPurchaseQty(mItemCode, mUnit, mPartyCode, pScheduleDate)
                    '                mExtraApprovalQty = GetExtraApprovalQty(pItemCode, mItemUOM, pPartyCode, pSchdDate)						
                End If

                '						
                '						
                If mTillDatePurQty > mSchdQty Then
                    mSchdQty = mTillDatePurQty
                End If

                If mItemCode <> "" And mPartyCode <> "" And (mSchdQty - mActualSchdQty) <> 0 Then ''mSchdQty > 0 And						

			SqlStr = " SELECT AUTO_KEY_PO, PUR_ORD_DATE, AMEND_NO, AMEND_DATE, AMEND_WEF_DATE " & vbCrLf _
                        & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").value & "" & vbCrLf _
                        & " AND IH.MKEY=ID.MKEY" & vbCrLf _
                        & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'" & vbCrLf _
                        & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND PUR_TYPE='P' AND ORDER_TYPE='O'" & vbCrLf _
                        & " AND IH.PO_STATUS='Y' AND PO_CLOSED='N' " & vbCrLf _
                        & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & ""
                

                    If CDate(pScheduleDate) < CDate(PubGSTApplicableDate) Then
                        SqlStr = SqlStr & vbCrLf & " AND ISGSTENABLE_PO='N'"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND ISGSTENABLE_PO='Y'"
                    End If

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPO, ADODB.LockTypeEnum.adLockReadOnly)

                    mDSPost = "N"
                    pDSNo = 0
                    pDSdate = ""
                    pDSAmendNo = 0
                    pDSAmendDate = ""
                    mSchdStatus = "N"

                    If RsTempPO.EOF = False Then
                        pPONO = IIf(IsDBNull(RsTempPO.Fields("AUTO_KEY_PO").Value), -1, RsTempPO.Fields("AUTO_KEY_PO").Value)
                        pPODate = VB6.Format(IIf(IsDBNull(RsTempPO.Fields("PUR_ORD_DATE").Value), "", RsTempPO.Fields("PUR_ORD_DATE").Value), "DD/MM/YYYY")
                        pAmendNo = IIf(IsDBNull(RsTempPO.Fields("AMEND_NO").Value), -1, RsTempPO.Fields("AMEND_NO").Value)
                        pAmendDate = VB6.Format(IIf(IsDBNull(RsTempPO.Fields("AMEND_DATE").Value), "", RsTempPO.Fields("AMEND_DATE").Value), "DD/MM/YYYY")
                        pWEFDate = VB6.Format(IIf(IsDBNull(RsTempPO.Fields("AMEND_WEF_DATE").Value), "", RsTempPO.Fields("AMEND_WEF_DATE").Value), "DD/MM/YYYY")

                        SqlStr = " SELECT AUTO_KEY_DELV,DELV_SCHLD_DATE,POST_FLAG,DELV_AMEND_NO,DELV_AMEND_DATE,SCHLD_STATUS FROM PUR_DELV_SCHLD_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'" & vbCrLf & " AND TO_CHAR(SCHLD_DATE,'YYYYMM')='" & VB6.Format(pScheduleDate, "YYYYMM") & "'" & vbCrLf & " AND AUTO_KEY_PO=" & Val(CStr(pPONO)) & ""

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = True Then
                            pAddMode = True
                            pDSNo = AutoGenDSNoSeq()
                            pDSdate = CStr(PubCurrDate)
                            mDSPost = "N"
                            mSchdStatus = "N"
                            pDSAmendNo = 0
                            pDSAmendDate = CStr(PubCurrDate)
                        Else
                            pAddMode = False
                            pDSNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_DELV").Value), -1, RsTemp.Fields("AUTO_KEY_DELV").Value)
                            pDSdate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("DELV_SCHLD_DATE").Value), "", RsTemp.Fields("DELV_SCHLD_DATE").Value), "DD/MM/YYYY")
                            mDSPost = IIf(IsDBNull(RsTemp.Fields("POST_FLAG").Value), "N", RsTemp.Fields("POST_FLAG").Value)
                            mSchdStatus = "N"
                            If mDSPost = "Y" Then
                                pDSAmendNo = IIf(IsDBNull(RsTemp.Fields("DELV_AMEND_NO").Value), 0, RsTemp.Fields("DELV_AMEND_NO").Value) + 1
                                pDSAmendDate = CStr(PubCurrDate)
                            Else
                                pDSAmendNo = IIf(IsDBNull(RsTemp.Fields("DELV_AMEND_NO").Value), 0, RsTemp.Fields("DELV_AMEND_NO").Value)
                                pDSAmendDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("DELV_AMEND_DATE").Value), "", RsTemp.Fields("DELV_AMEND_DATE").Value), "DD/MM/YYYY")
                            End If
                        End If
                        '                Else						
                        If UpdateDS(pAddMode, pDSNo, pDSdate, pDSAmendNo, pDSAmendDate, pPONO, mPartyCode, pScheduleDate, mDSPost, mSchdStatus, pPODate, pAmendNo, pAmendDate, pWEFDate, mItemCode, mUnit, mSchdQty, mPackingStd) = False Then GoTo ErrPart
                    End If
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        cmdProcess.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        PubDBCn.RollbackTrans()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function UpdateDS(ByRef pAddMode As Boolean, ByRef pDSNo As Double, ByRef pDSdate As String, ByRef pDSAmendNo As Integer, ByRef pDSAmendDate As String, ByRef pPONO As Double, ByRef mPartyCode As String, ByRef pSchdDate As String, ByRef mDSPost As String, ByRef mSchdStatus As String, ByRef pPODate As String, ByRef pAmendNo As Integer, ByRef pAmendDate As String, ByRef pWEFDate As String, ByRef pItemCode As String, ByRef pUnit As String, ByRef pDSQty As Double, ByRef mPackingStd As Double) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart

        Dim SqlStr As String


        If pAddMode = True Then
            SqlStr = " INSERT INTO PUR_DELV_SCHLD_HDR ( " & vbCrLf & "  COMPANY_CODE , AUTO_KEY_DELV," & vbCrLf & "  DELV_SCHLD_DATE , DELV_AMEND_NO," & vbCrLf & "  DELV_AMEND_DATE , AUTO_KEY_PO," & vbCrLf & "  SUPP_CUST_CODE , SCHLD_DATE," & vbCrLf & "  EMP_CODE , SCHLD_STATUS," & vbCrLf & "  REMARKS , POST_FLAG," & vbCrLf & "  PO_DATE , PO_AMEND_NO," & vbCrLf & "  AMEND_DATE , AMEND_WEF_DATE, " & vbCrLf & "  ADDUSER, ADDDATE, MODUSER, MODDATE, IS_MAIL) "

            SqlStr = SqlStr & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & pDSNo & ", '" & VB6.Format(pDSdate, "DD-MMM-YYYY") & "', " & vbCrLf _
                & " " & Val(pDSAmendNo) & ", '" & VB6.Format(pDSAmendDate, "DD-MMM-YYYY") & "', " & vbCrLf _
                & " " & Val(pPONO) & ", '" & MainClass.AllowSingleQuote(mPartyCode) & "', " & vbCrLf _
                & " '" & VB6.Format(pSchdDate, "DD-MMM-YYYY") & "', '', '" & mSchdStatus & "'," & vbCrLf _
                & " '', 'N', '" & VB6.Format(pPODate, "DD-MMM-YYYY") & "', " & vbCrLf _
                & " " & Val(pAmendNo) & ", '" & VB6.Format(pAmendDate, "DD-MMM-YYYY") & "', " & vbCrLf _
                & " '" & VB6.Format(pWEFDate, "DD-MMM-YYYY") & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "','" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','','','N')"


            PubDBCn.Execute(SqlStr)
        ElseIf mDSPost = "N" Then
            SqlStr = " UPDATE PUR_DELV_SCHLD_HDR SET " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " Moddate='" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "' " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_DELV =" & Val(CStr(pDSNo)) & ""
            PubDBCn.Execute(SqlStr)
        ElseIf mDSPost = "Y" Then
            SqlStr = " UPDATE PUR_DELV_SCHLD_HDR SET " & vbCrLf & " AUTO_KEY_DELV= " & pDSNo & "," & vbCrLf & " DELV_SCHLD_DATE='" & VB6.Format(pDSdate, "DD-MMM-YYYY") & "', " & vbCrLf & " DELV_AMEND_NO=" & Val(CStr(pDSAmendNo)) & ", " & vbCrLf & " DELV_AMEND_DATE='" & VB6.Format(pDSAmendDate, "DD-MMM-YYYY") & "', " & vbCrLf & " AUTO_KEY_PO=" & Val(CStr(pPONO)) & ", " & vbCrLf & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "', " & vbCrLf & " SCHLD_DATE='" & VB6.Format(pSchdDate, "DD-MMM-YYYY") & "', " & vbCrLf & " EMP_CODE=''," & vbCrLf & " SCHLD_STATUS='N'," & vbCrLf & " REMARKS='', " & vbCrLf & " POST_FLAG='N'," & vbCrLf & " PO_DATE='" & VB6.Format(pPODate, "DD-MMM-YYYY") & "', " & vbCrLf & " PO_AMEND_NO=" & Val(CStr(pAmendNo)) & ", " & vbCrLf & " AMEND_DATE='" & VB6.Format(pAmendDate, "DD-MMM-YYYY") & "', " & vbCrLf & " AMEND_WEF_DATE='" & VB6.Format(pWEFDate, "DD-MMM-YYYY") & "', " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate='" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "' " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_DELV =" & Val(CStr(pDSNo)) & ""
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
        Dim MainClass_Renamed As Object
        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim I As Integer
        Dim mItemCode As String
        Dim mWeek1Qty As Double
        Dim mWeek2Qty As Double
        Dim mWeek3Qty As Double
        Dim mWeek4Qty As Double
        Dim mWeek5Qty As Double


        Dim RsTemp As ADODB.Recordset

        Dim mDay As Integer
        Dim mDate As String
        Dim mLastDay As Integer
        Dim mWorkingDays As Double
        Dim mDailyPlanQty As Double
        Dim mDailySchdQty As Double
        Dim mBalQty As Double
        Dim RsTempUOM As ADODB.Recordset
        Dim mPurchaseUOM As String
        Dim mFactor As Double
        Dim mExtraApprovalQty As Double
        Dim mTillDatePurQty As Double

	 SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR,PURCHASE_COST FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempUOM, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempUOM.EOF = False Then
            '        mIssueUOM = IIf(IsNull(RsTempUOM!ISSUE_UOM), "", RsTempUOM!ISSUE_UOM)						
            mPurchaseUOM = IIf(IsDBNull(RsTempUOM.Fields("PURCHASE_UOM").Value), "", RsTempUOM.Fields("PURCHASE_UOM").Value)
            mFactor = IIf(IsDBNull(RsTempUOM.Fields("UOM_FACTOR").Value) Or RsTempUOM.Fields("UOM_FACTOR").Value = 0, 1, RsTempUOM.Fields("UOM_FACTOR").Value)
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
            I = IIf(IsDBNull(RsTemp.Fields("SERIAL_NO").Value), 1, RsTemp.Fields("SERIAL_NO").Value)
        Else
            SqlStr = "SELECT MAX(SERIAL_NO) AS SERIAL_NO FROM PUR_DELV_SCHLD_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_DELV=" & Val(CStr(pDSNo)) & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                I = IIf(IsDBNull(RsTemp.Fields("SERIAL_NO").Value), 1, RsTemp.Fields("SERIAL_NO").Value)
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

            SqlStr = "INSERT INTO TEMP_PUR_DAILY_SCHLD_DET (" & vbCrLf & " USERID, AUTO_KEY_DELV,  ITEM_CODE, " & vbCrLf & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE )" & vbCrLf & " VALUES ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & Val(CStr(pDSNo)) & ", '" & MainClass.AllowSingleQuote(pItemCode) & "', " & vbCrLf & " '" & VB6.Format(mDate, "DD-MMM-YYYY") & "', " & mDailySchdQty & ", 0, " & vbCrLf & " 0, '" & MainClass.AllowSingleQuote(pPartyCode) & "', '" & VB6.Format(pSchdDate, "DD-MMM-YYYY") & "') "

            PubDBCn.Execute(SqlStr)
        Next

        ''SERIAL_NO, " & mDay & ",						

        SqlStr = ""

        If pItemCode <> "" Then '''And mTotQty > 0 '''If DS Amend Then Print ...						
            SqlStr = " INSERT INTO PUR_DELV_SCHLD_DET ( " & vbCrLf & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf & " ITEM_UOM, WEEK1_QTY, WEEK2_QTY, " & vbCrLf & " WEEK3_QTY, WEEK4_QTY, " & vbCrLf & " WEEK5_QTY, TOTAL_QTY, " & vbCrLf & " REC_QTY, SHORT_QTY, COMPANY_CODE) "

            SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(CStr(pDSNo)) & "," & I & ", " & vbCrLf & " '" & pItemCode & "','" & mPurchaseUOM & "', " & vbCrLf & " " & mWeek1Qty & ", " & mWeek2Qty & ", " & vbCrLf & " " & mWeek3Qty & "," & mWeek4Qty & "," & mWeek5Qty & ", " & vbCrLf & " " & pDSQty & "," & vbCrLf & " " & 0 & "," & 0 & "," & RsCompany.Fields("COMPANY_CODE").Value & ") "

            PubDBCn.Execute(SqlStr)

            SqlStr = "INSERT INTO PUR_DAILY_SCHLD_DET (" & vbCrLf & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE )" & vbCrLf & " SELECT " & vbCrLf & " AUTO_KEY_DELV, " & I & ", ITEM_CODE, " & vbCrLf & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE " & vbCrLf & " FROM TEMP_PUR_DAILY_SCHLD_DET " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pPartyCode) & "'" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(pSchdDate, "DD-MMM-YYYY") & "')"

            PubDBCn.Execute(SqlStr)

            SqlStr = "INSERT INTO PUR_DAILY_SCHLD_HIS_DET (" & vbCrLf & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE,DELV_AMEND_NO )" & vbCrLf & " SELECT " & vbCrLf & " " & Val(CStr(pDSNo)) & ", " & I & ", ITEM_CODE, " & vbCrLf & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE," & Val(CStr(pDSAmendNo)) & " " & vbCrLf & " FROM TEMP_PUR_DAILY_SCHLD_DET " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pPartyCode) & "'" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(pSchdDate, "DD-MMM-YYYY") & "')"

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
        Dim MainClass_Renamed As Object
        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        GetExtraApprovalQty = 0


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetExtraApprovalQty = IIf(IsDBNull(RsTemp.Fields("APP_QTY").Value), 0, RsTemp.Fields("APP_QTY").Value)
        End If

        Exit Function
UpdateDetail1:

        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume						
    End Function

    Private Function GetTotalPurchaseQty(ByRef pItemCode As String, ByRef mItemUOM As String, ByRef pPartyCode As String, ByRef pSchdDate As String) As Double
        Dim MainClass_Renamed As Object
        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mHDRTable As String
        Dim mDETTable As String


        GetTotalPurchaseQty = 0

        If RsCompany.Fields("MRR_AGT_GE").Value = "N" Then
        Else
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
    Private Function IsHoliday(ByRef pDate As String) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        IsHoliday = True
        If IsDate(pDate) Then
            SqlStr = " SELECT HOLIDAY_DATE FROM PAY_HOLIDAY_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HOLIDAY_DATE='" & VB6.Format(pDate, "DD-MMM-YYYY") & "'"

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
        Dim MainClass_Renamed As Object
        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Integer
        Dim SqlStr As String
        Dim mStartingChk As Double

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
                    mAutoGen = CInt(Mid(.Fields(0).Value, 1, Len(.Fields(0).Value) - 6))
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
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtFGName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
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
        Dim SqlStr As String
        Dim mCategoryCode As String


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItemName.Text = AcName
            TxtItemName_Validating(txtItemName, New System.ComponentModel.CancelEventArgs(False))
            txtItemName.Focus()
        End If

        '    If chkFG.Value = vbUnchecked Then						
        '        If MainClass.ValidateWithMasterTable(txtFGName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then						
        '            mCategoryCode = MasterNo						
        '            SqlStr = SqlStr & " AND CATEGORY_CODE='" & mCategoryCode & "'"						
        '        End If						
        '    End If						

        '    If MainClass.SearchGridMaster(TxtItemName.Text, "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", , , SqlStr) = True Then						
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        Dim MainClass_Renamed As Object
        MainClass.ClearGrid(SprdMain, RowHeight)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '    If optDetSummarised(3).Value = True Then						
        '        ShowDetail1						
        '    Else						
        Show1()
        '    End If						

        FormatSprdMain(True)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        SprdMain.Focus()
        Call PrintStatus(True)
    End Sub
    Private Sub Show1()
        Dim MainClass_Renamed As Object
        On Error GoTo LedgError
        Dim RsShow As ADODB.Recordset
        Dim SqlStr As String
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
        Dim mCatCode As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mDate = VB6.Format(lblRunDate.Text, "DD/MM/YYYY")

        mWorkingDays = GetWorkingDays(mDate)

        SqlStr = ""


        SqlStr = " SELECT IH.RM_CODE, INVMST.ITEM_SHORT_DESC AS RM_NAME, INVMST.ISSUE_UOM, " & vbCrLf & " MAX(IH.STOCK_QTY) AS STOCK_QTY, INVMST.MINIMUM_QTY, INVMST.PACK_STD," & vbCrLf & " SUM(IH.RM_QTY) AS RM_QTY, "

        '    If RsCompany.Fields("COMPANY_CODE").Value = 1 Then						
        '        SqlStr = SqlStr & vbCrLf & " SUM(IH.RM_QTY) AS NET_REQ_RM_QTY"						
        '    Else						
        SqlStr = SqlStr & vbCrLf & " SUM(IH.RM_QTY) + MAX(INVMST.MINIMUM_QTY) - MAX(IH.STOCK_QTY) AS NET_REQ_RM_QTY "
        '    End If						

        SqlStr = SqlStr & vbCrLf & " FROM INV_PROCESS_MONTHLY_SCHLD IH, INV_ITEM_MST INVMST " & vbCrLf & " WHERE IH.COMPANY_CODE = INVMST.COMPANY_CODE " & vbCrLf & " AND IH.RM_CODE = INVMST.ITEM_CODE"


        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.BOOKTYPE='" & lblBookType.Text & "' " & vbCrLf & " AND IH.BOOKSUBTYPE='" & lblBookSubType.Text & "' " & vbCrLf & " AND TO_CHAR(IH.PROCESS_DATE,'YYYYMM') ='" & VB6.Format(mDate, "YYYYMM") & "' "

        If chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtFGName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtFGName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mFGCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mFGCode) & "' "
            End If
        End If

        If chkItem.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItemName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND IH.RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' "
            End If
        End If

        'If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCategoryDesc.Text) <> "" Then
        '    If MainClass.ValidateWithMasterTable(txtCategoryDesc.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE IN ('C')") = True Then
        '        mCatCode = MasterNo
        '        SqlStr = SqlStr & vbCrLf & " AND INVMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "' "
        '    End If
        'End If

        Dim mRMCatCodeStr As String = ""
        Dim mMaterialType As String = ""
        Dim mRMCatCode As String = ""

        If lstMaterialType.GetItemChecked(0) = True Then
            mRMCatCodeStr = ""
        Else
            For CntLst = 1 To lstMaterialType.Items.Count - 1
                If lstMaterialType.GetItemChecked(CntLst) = True Then
                    mMaterialType = VB6.GetItemString(lstMaterialType, CntLst)
                    If MainClass.ValidateWithMasterTable(mMaterialType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                        mRMCatCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mRMCatCodeStr = IIf(mRMCatCodeStr = "", "'" & mRMCatCode & "'", mRMCatCodeStr & "," & "'" & mRMCatCode & "'")
                End If
            Next
        End If

        If mRMCatCodeStr <> "" Then
            mRMCatCodeStr = "(" & mRMCatCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND INVMST.CATEGORY_CODE IN " & mRMCatCodeStr & ""
        End If

        If chkAllParty.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPartyName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCustCode = MasterNo
                SqlStr = SqlStr & " AND IH.RM_CODE IN (" & vbCrLf _
                    & " SELECT DISTINCT ITEM_CODE " & vbCrLf _
                    & " FROM FIN_SUPP_CUST_DET" & vbCrLf _
                    & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mSuppCustCode) & "'" & vbCrLf _
                    & " AND OP_QTY>0)"
            End If
        End If


        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.RM_CODE, INVMST.ITEM_SHORT_DESC,INVMST.MINIMUM_QTY, INVMST.PACK_STD, INVMST.ISSUE_UOM,IH.PROCESS_DATE "

        SqlStr = SqlStr & vbCrLf & " Order By IH.RM_CODE,IH.PROCESS_DATE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        mcntRow = 1


        With SprdMain
            If Not RsShow.EOF Then
                Do While Not RsShow.EOF
                    .Row = mcntRow

                    mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                    mRMName = IIf(IsDBNull(RsShow.Fields("RM_NAME").Value), "", RsShow.Fields("RM_NAME").Value)
                    mUnit = IIf(IsDBNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)
                    mStockQty = CDbl(VB6.Format(IIf(IsDBNull(RsShow.Fields("STOCK_QTY").Value), 0, RsShow.Fields("STOCK_QTY").Value), "0.00"))
                    mMinInv = CDbl(VB6.Format(IIf(IsDBNull(RsShow.Fields("MINIMUM_QTY").Value), 0, RsShow.Fields("MINIMUM_QTY").Value), "0.00")) 'IIf(RsCompany.Fields("COMPANY_CODE").Value = 1, 0, Format(IIf(IsNull(RsShow!MINIMUM_QTY), 0, RsShow!MINIMUM_QTY), "0.00"))						
                    mPlanQty = CDbl(VB6.Format(IIf(IsDBNull(RsShow.Fields("RM_QTY").Value), 0, RsShow.Fields("RM_QTY").Value), "0.00"))
                    mPackingStd = CDbl(VB6.Format(IIf(IsDBNull(RsShow.Fields("PACK_STD").Value), 0, RsShow.Fields("PACK_STD").Value), "0.00"))
                    mNetPlanQty = CDbl(VB6.Format(IIf(IsDBNull(RsShow.Fields("NET_REQ_RM_QTY").Value), 0, RsShow.Fields("NET_REQ_RM_QTY").Value), "0.00"))
                    mNetPlanQty = IIf(mNetPlanQty < 0, 0, mNetPlanQty)
                    mActualSchdQty = GetActualDSQty(mRMCode, mUnit, "")
                    '                If mPackingStd > 0 Then						
                    '                    mNetPlanQty = mNetPlanQty / mPackingStd						
                    '                    mNetPlanQty = IIf(Int(mNetPlanQty) = mNetPlanQty, mNetPlanQty, Int(mNetPlanQty) + 1) * mPackingStd						
                    '                End If						

                    .Col = ColItemCode
                    .Text = mRMCode
                    .Font = VB6.FontChangeBold(.Font, False)

                    .Col = ColItemName
                    .Text = mRMName
                    .Font = VB6.FontChangeBold(.Font, False)

                    .Col = ColUnit
                    .Text = mUnit
                    .Font = VB6.FontChangeBold(.Font, False)

                    .Col = ColStockQty
                    .Text = VB6.Format(mStockQty, "0.00")
                    .Font = VB6.FontChangeBold(.Font, False)

                    .Col = ColMinQty
                    .Text = VB6.Format(mMinInv, "0.00")
                    .Font = VB6.FontChangeBold(.Font, False)

                    .Col = ColPlanQty
                    .Text = VB6.Format(mPlanQty, "0.00")
                    .Font = VB6.FontChangeBold(.Font, False)

                    .Col = ColPackingStd
                    .Text = VB6.Format(mPackingStd, "0.00")
                    .Font = VB6.FontChangeBold(.Font, False)

                    .Col = ColNetPlanQty
                    If mPackingStd > 0 Then
                        mNetPlanQty = mNetPlanQty / mPackingStd
                        mNetPlanQty = IIf(Int(mNetPlanQty) = mNetPlanQty, mNetPlanQty, Int(mNetPlanQty) + 1) * mPackingStd
                    End If
                    mBalanceQty = mNetPlanQty
                    .Text = VB6.Format(mNetPlanQty, "0.00")
                    .Font = VB6.FontChangeBold(.Font, False)

                    .Col = ColActualSchdQty
                    .Text = VB6.Format(mActualSchdQty, "0.00")
                    .Font = VB6.FontChangeBold(.Font, False)

                    If (chkAllParty.CheckState = System.Windows.Forms.CheckState.Unchecked And optDetSummarised(1).Checked = True) Or (chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked And optDetSummarised(2).Checked = True) Then

                    Else
                        If optDetSummarised(0).Checked = False Then
                            .Row = mcntRow
                            .Col = -1
                            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 'Gray						
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue						
                            .Font = VB6.FontChangeBold(.Font, True)
                            .Font = VB6.FontChangeBold(.Font, True)
                            SprdMain.SetCellBorder(ColItemCode, mcntRow, SprdMain.MaxCols, mcntRow, SS_BORDER_TYPE_OUTLINE, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)
                        End If
                    End If

                    If optDetSummarised(1).Checked = True Then
                        If ShowPartyDetail(mRMCode, mUnit, mNetPlanQty, mPackingStd, mBalanceQty, mcntRow) = False Then GoTo LedgError
                    ElseIf optDetSummarised(2).Checked = True Then
                        If ShowProdDetail(mRMCode, mcntRow, mDate) = False Then GoTo LedgError
                    End If

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
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset '' ADODB.Recordset						
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
                mSchdQty = IIf(IsDBNull(RsTemp.Fields("TOTAL_QTY").Value), 0, RsTemp.Fields("TOTAL_QTY").Value)
                mRMUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                mPURUOM = IIf(IsDBNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                mFactor = IIf(IsDBNull(RsTemp.Fields("UOM_FACTOR").Value), 1, RsTemp.Fields("UOM_FACTOR").Value)

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
    Private Function ShowPartyDetail(ByRef pItemCode As String, ByRef mUnit As String, ByRef mNetPlanQty As Double, ByRef mPackingStd As Double, ByRef mBalanceQty As Double, ByRef mcntRow As Integer) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo LedgError
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String

        Dim mPartyCode As String
        Dim mPartyName As String
        Dim mSOB As Double
        Dim mSchdQty As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mActualPartySchdQty As Double
        Dim xSchdDate As String
        Dim mExcessSchd As Double

        ShowPartyDetail = False

        SqlStr = " SELECT ACM.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME, ACMDetail.OP_QTY " & vbCrLf _
                & " FROM FIN_SUPP_CUST_MST ACM, FIN_SUPP_CUST_DET ACMDetail" & vbCrLf _
                & " WHERE ACM.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ACM.COMPANY_CODE = ACMDetail.COMPANY_CODE " & vbCrLf _
                & " AND ACM.SUPP_CUST_CODE = ACMDetail.SUPP_CUST_CODE" & vbCrLf _
                & " AND ACMDetail.ITEM_CODE = '" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                & " AND OP_QTY>0"

        '    If chkFG.Value = vbUnchecked And Trim(txtFGName.Text) <> "" Then						
        '        If MainClass.ValidateWithMasterTable(txtFGName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then						
        '            mFGCode = MasterNo						
        '            SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mFGCode) & "' "						
        '        End If						
        '    End If						

        If chkItem.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItemName.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ACMDetail.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' "
        End If


        If chkAllParty.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPartyName.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtPartyName.Text) & "' "
        End If

        SqlStr = SqlStr & vbCrLf & " Order By ACMDetail.OP_QTY DESC, ACM.SUPP_CUST_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        xSchdDate = "01/" & VB6.Format(lblYear.Text, "MM/YYYY")

        With SprdMain
            If Not RsTemp.EOF Then
                If chkAllParty.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mcntRow = mcntRow + 1
                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                End If
                Do While Not RsTemp.EOF
                    .Row = mcntRow

                    mPartyCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                    mPartyName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    mSOB = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("OP_QTY").Value), 0, RsTemp.Fields("OP_QTY").Value), "0.00"))
                    mSchdQty = CDbl(VB6.Format(mNetPlanQty * mSOB * 0.01, "0.00"))
                    mSchdQty = System.Math.Round(mSchdQty, 0)

                    mExcessSchd = CheckExcessDSApprovalQty(pItemCode, xSchdDate, mPartyCode, 0)

                    mSchdQty = mSchdQty + CheckInterChangeDSApprovalQty(pItemCode, xSchdDate, mPartyCode)

                    mActualPartySchdQty = GetActualDSQty(pItemCode, mUnit, mPartyCode)

                    If mPackingStd > 0 Then
                        mSchdQty = mSchdQty / mPackingStd
                        mSchdQty = IIf(Int(mSchdQty) = mSchdQty, mSchdQty, Int(mSchdQty) + 1) * mPackingStd
                    End If

                    If mBalanceQty < mSchdQty Then
                        mSchdQty = mBalanceQty
                    ElseIf mSchdQty < 0 Then
                        mSchdQty = 0
                    End If

                    If chkRateRequired.CheckState = System.Windows.Forms.CheckState.Checked Then
                        mRate = CDbl(GetLastPORate(pItemCode, Trim(mPartyCode), xSchdDate))
                    Else
                        mRate = 0
                    End If
                    mAmount = CDbl(VB6.Format(mSchdQty * mRate, "0.00"))

                    .Col = ColPartyCode
                    .Text = mPartyCode

                    .Col = ColPartyName
                    .Text = mPartyName

                    .Col = ColSOB
                    .Text = VB6.Format(mSOB, "0.00")

                    .Col = ColSchdQty
                    .Text = VB6.Format(mSchdQty + mExcessSchd, "0.00")

                    .Col = ColActualPartySchdQty
                    .Text = VB6.Format(mActualPartySchdQty, "0.00")

                    .Col = ColItemRate
                    .Text = VB6.Format(mRate, "0.00")

                    .Col = ColItemAmount
                    .Text = VB6.Format(mAmount, "0.00")

                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        mBalanceQty = mBalanceQty - mSchdQty ''Less Excess Approval Schd						
                        mExcessSchd = 0
                        mcntRow = mcntRow + 1
                        .MaxRows = .MaxRows + 1
                    End If
                Loop
            End If
        End With

        RsTemp.Cancel()
        RsTemp.Close()
        RsTemp = Nothing
        ShowPartyDetail = True
        Exit Function
LedgError:
        MsgInformation(Err.Description)
        ShowPartyDetail = False
        '    Resume						
    End Function
    Private Function ShowProdDetail(ByRef pItemCode As String, ByRef mcntRow As Integer, ByRef mDate As String) As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo LedgError
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String

        Dim mProdCode As String
        Dim mProdName As String
        Dim mStdQty As Double
        Dim mSchdQty As Double
        Dim mRate As Double
        Dim mAmount As Double

        Dim mFGCode As String
        Dim mItemCode As String
        Dim mSuppCustCode As String
        Dim mActualPartySchdQty As Double
        ShowProdDetail = False


        SqlStr = " SELECT IH.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC, SUM(STD_QTY) AS STD_QTY, SUM(DPLAN_QTY) AS DPLAN_QTY" & vbCrLf _
                & " FROM INV_PROCESS_MONTHLY_SCHLD IH, INV_ITEM_MST INVMST" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.COMPANY_CODE = INVMST.COMPANY_CODE " & vbCrLf _
                & " AND IH.PRODUCT_CODE = INVMST.ITEM_CODE" & vbCrLf _
                & " AND IH.RM_CODE = '" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                & " AND IH.BOOKTYPE='" & lblBookType.Text & "' " & vbCrLf _
                & " AND IH.BOOKSUBTYPE='" & lblBookSubType.Text & "' " & vbCrLf _
                & " AND TO_CHAR(IH.PROCESS_DATE,'YYYYMM') ='" & VB6.Format(mDate, "YYYYMM") & "' "


        If chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtFGName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtFGName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mFGCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mFGCode) & "' "
            End If
        End If

        If chkItem.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItemName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND IH.RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' "
            End If
        End If


        If chkAllParty.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPartyName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCustCode = MasterNo
                SqlStr = SqlStr & " AND IH.RM_CODE IN (" & vbCrLf & " SELECT DISTINCT ITEM_CODE " & vbCrLf & " FROM FIN_SUPP_CUST_DET" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mSuppCustCode) & "'" & vbCrLf & " AND OP_QTY>0)"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP By IH.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC"

        SqlStr = SqlStr & vbCrLf & " Order By INVMST.ITEM_SHORT_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdMain
            If Not RsTemp.EOF Then
                If chkFG.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mcntRow = mcntRow + 1
                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                End If
                Do While Not RsTemp.EOF
                    .Row = mcntRow

                    mProdCode = IIf(IsDBNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value)
                    mProdName = IIf(IsDBNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)
                    mStdQty = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value), "0.00"))
                    mSchdQty = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("DPLAN_QTY").Value), 0, RsTemp.Fields("DPLAN_QTY").Value), "0.00"))
                    mRate = 0
                    mActualPartySchdQty = 0
                    mAmount = CDbl(VB6.Format(mSchdQty * mRate, "0.00"))

                    .Col = ColPartyCode
                    .Text = mProdCode

                    .Col = ColPartyName
                    .Text = mProdName

                    .Col = ColSOB
                    .Text = VB6.Format(mStdQty, "0.00")

                    .Col = ColSchdQty
                    .Text = VB6.Format(mSchdQty, "0.00")

                    .Col = ColActualPartySchdQty
                    .Text = VB6.Format(mActualPartySchdQty, "0.00")

                    .Col = ColItemRate
                    .Text = VB6.Format(mRate, "0.00")

                    .Col = ColItemAmount
                    .Text = VB6.Format(mAmount, "0.00")

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

    Private Function GetLastPORate(ByRef pItemCode As String, ByRef mSuppCustCode As String, ByRef xScheduleDate As String) As String
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        GetLastPORate = CStr(0)

        If mSuppCustCode = "" Then

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

            If RsTemp.EOF = False Then
                mSuppCustCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
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
            GetLastPORate = IIf(IsDBNull(RsTemp.Fields("PO_RATE").Value), 0, RsTemp.Fields("PO_RATE").Value)
        End If
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Function
    Private Sub ShowDetail1()
        'On Error GoTo LedgError						
        'Dim RsShow As ADODB.Recordset						
        'Dim SqlStr As String						
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
        '    mDate = Format(lblRunDate.Caption, "DD/MM/YYYY")						
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
        '                        mProcessDate = Format(IIf(IsNull(RsShow!PROCESS_DATE), "", RsShow!PROCESS_DATE), "DD-MM-YYYY")						
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
        ''                        .Text = Format(mStockQty, "0.00")						
        '						
        '                        .Col = ColSchdQty						
        '                        .Text = Format(mFGPlan, "0.00")						
        '                        .FontBold = False						
        '						
        ''                        .Col = ColDiff						
        ''                        .Text = Format(mFGPlan, "0.00")						
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
        '                        .Text = Format(mStockQty, "0.00")						
        '						
        '                        .Col = ColMinQty						
        '                        .Text = Format(mMinInv, "0.00")						
        '						
        '                        .Col = ColPlanQty						
        '                        .Text = Format(mPlanQty, "0.00")						
        '						
        '                        .Col = ColDiff						
        '                        If optDetSummarised(2).Value = True Then						
        '                            .Text = Format(IIf(mDiff < 0, 0, mDiff), "0.00")						
        '                        Else						
        '                            mDiff = mPlanQty - mStockQty						
        '                            .Text = Format(IIf(mDiff < 0, 0, mDiff), "0.00")						
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
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset


        GetWorkingDays = MainClass.LastDay(Month(CDate(pDate)), Year(CDate(pDate)))

        SqlStr = " SELECT COUNT(1) AS LDAYS FROM PAY_HOLIDAY_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(HOLIDAY_DATE,'MON-YYYY')='" & UCase(VB6.Format(pDate, "MMM-YYYY")) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetWorkingDays = GetWorkingDays - IIf(IsDBNull(RsTemp.Fields("LDAYS").Value), 0, RsTemp.Fields("LDAYS").Value)
        End If
        Exit Function
ErrPart:
        GetWorkingDays = 0
    End Function
    Public Sub frmParamMonthlySchld_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        If lblBookType.Text = VB.Left(ConDespatchPlan, 1) And lblBookSubType.Text = VB.Right(ConDespatchPlan, 1) Then
            Me.Text = "Revised Despatch & Production Planning"
            optDetSummarised(2).Enabled = False
        ElseIf lblBookType.Text = VB.Left(ConPurchase, 1) And lblBookSubType.Text = VB.Right(ConPurchase, 1) Then
            Me.Text = "Monthly Schedule Planning"
        End If
        FormatSprdMain(False)
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamMonthlySchld_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim MainClass_Renamed As Object
        On Error GoTo BSLError

        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection						
        ''PvtDBCn.Open StrConn						
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7440)
        'Me.Width = VB6.TwipsToPixelsX(11625)

        CurrFormHeight = 7650 ''789, 510
        CurrFormWidth = 11520

        lblRunDate.Text = CStr(RunDate)
        FillMonth(CDate(lblRunDate.Text))
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)

        chkFG.CheckState = System.Windows.Forms.CheckState.Checked
        chkFG_CheckStateChanged(chkFG, New System.EventArgs())
        chkItem.CheckState = System.Windows.Forms.CheckState.Checked
        chkItem_CheckStateChanged(chkItem, New System.EventArgs())

        'chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        'chkCategory_CheckStateChanged(chkCategory, New System.EventArgs())

        lstMaterialType.Items.Clear()
        SqlStr = "SELECT GEN_DESC FROM INV_GENERAL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GEN_TYPE='C' ORDER BY GEN_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstMaterialType.Items.Add("ALL")
            lstMaterialType.SetItemChecked(CntLst, True)
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstMaterialType.Items.Add(RS.Fields("GEN_DESC").Value)
                lstMaterialType.SetItemChecked(CntLst, True)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain(ByRef mFillColHeading As Boolean)
        Dim MainClass_Renamed As Object
        Dim cntCol As Integer

        With SprdMain
            .MaxCols = ColItemAmount
            .set_RowHeight(-1, RowHeight * 0.75)

            .Row = -1
            .set_ColWidth(0, 4)


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

            For cntCol = ColStockQty To ColActualSchdQty
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 10)
            Next

            .Col = ColPartyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColPartyCode, 6)
            .ColHidden = IIf(optDetSummarised(0).Checked = True, True, False)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 35)
            .ColHidden = IIf(optDetSummarised(0).Checked = True, True, False)

            For cntCol = ColSOB To ColItemAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 10)
                .ColHidden = IIf(optDetSummarised(0).Checked = True, True, False)
                If cntCol = ColActualPartySchdQty And optDetSummarised(2).Checked = True Then
                    .ColHidden = True
                End If
            Next

            '        .Col = ColItemCode						
            '        .ColMerge = MergeAlways						
            '        .Col = ColItemName						
            '        .ColMerge = MergeAlways						

            Call FillHeading()

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle						

        End With

    End Sub

    Private Sub frmParamMonthlySchld_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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

    Private Sub frmParamMonthlySchld_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnSchedule(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub InsertIntoPrintdummyData()
        Dim MainClass_Renamed As Object
        Dim SqlStr As String
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

                If optDetSummarised(2).Checked = True Then
                    mColStart = 1
                Else
                    mColStart = 2
                End If

                For cntCol = mColStart To .MaxCols
                    .Col = cntCol

                    If optDetSummarised(2).Checked = True Then
                        FieldSeq = cntCol
                    Else
                        FieldSeq = cntCol - 1
                    End If

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
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mCustDealer As String
        Dim SqlStr As String

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

        If lblBookType.Text = VB.Left(ConDespatchPlan, 1) And lblBookSubType.Text = VB.Right(ConDespatchPlan, 1) Then
            mTitle = "Revised Despatch & Production Planning"
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\RevDespProdPlan.rpt"
        ElseIf lblBookType.Text = VB.Left(ConPurchase, 1) And lblBookSubType.Text = VB.Right(ConPurchase, 1) Then
            mTitle = "MONTHLY SCHEDULE Planning"
            If optDetSummarised(0).Checked = True Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\Month_schld_plan_Summarised.rpt"
            ElseIf optDetSummarised(1).Checked = True Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\Month_schld_plan_partywise.rpt"
            Else
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\Month_schld_plan_det_productwise.rpt"
            End If
        End If

        If optDetSummarised(0).Checked = True Then
            mTitle = mTitle & " [Summarised]"
        ElseIf optDetSummarised(1).Checked = True Then
            mTitle = mTitle & " [Party Wise]"
        ElseIf optDetSummarised(2).Checked = True Then
            mTitle = mTitle & " [Product wise]"
        End If

        mSubTitle = "FOR THE MONTH OF : " & VB6.Format(lblRunDate.Text, "MMM-YYYY")

        If chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtFGName.Text) <> "" Then
            mSubTitle = mSubTitle & " [Category : " & txtFGName.Text & "]"
        End If
        If chkItem.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItemName.Text) <> "" Then
            mSubTitle = mSubTitle & " [Item Name : " & txtItemName.Text & "]"
        End If
        If chkAllParty.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPartyName.Text) <> "" Then
            mSubTitle = mSubTitle & " [Supplier : " & txtPartyName.Text & "]"
        End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Exit Sub



ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Dim ii As Integer
        Dim mHeadStr As String
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

    Private Sub FillHeading()

        SprdMain.Row = 0
        SprdMain.Col = ColPartyCode
        SprdMain.Text = IIf(optDetSummarised(1).Checked = True, "Party Code", "Product Code")

        SprdMain.Col = ColPartyName
        SprdMain.Text = IIf(optDetSummarised(1).Checked = True, "Party Name", "Product Name")

        SprdMain.Col = ColSOB
        SprdMain.Text = IIf(optDetSummarised(1).Checked = True, "S.O.B. %", "Std Qty")

        SprdMain.Col = ColSchdQty
        SprdMain.Text = IIf(optDetSummarised(1).Checked = True, "Supplier Schedule Qty - Plan", "Customer Schedule Qty")

        SprdMain.Col = ColActualPartySchdQty
        SprdMain.Text = IIf(optDetSummarised(1).Checked = True, "Supplier Schedule Qty - Actual Given", "Customer Schedule Qty")

        SprdMain.Col = ColItemRate
        SprdMain.Text = IIf(optDetSummarised(1).Checked = True, "Item Rate", "Product Rate")

        SprdMain.Col = ColItemAmount
        SprdMain.Text = IIf(optDetSummarised(1).Checked = True, "Item Amount", "Product Amount")

    End Sub



    Private Sub optDetSummarised_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optDetSummarised.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optDetSummarised.GetIndex(eventSender)
            PrintStatus(False)
            '    txtPartyName.Enabled = False						
            '    cmdPartyName.Enabled = False						
            '    chkAllParty.Value = vbChecked						
            '    chkAllParty.Enabled = IIf(optDetSummarised(2).Value = True, True, False)						
        End If
    End Sub

    Private Sub txtCategoryDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategoryDesc.TextChanged
        PrintStatus(False)
    End Sub
    Private Sub txtCategoryDesc_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategoryDesc.DoubleClick
        Call cmdCategory_Click(cmdCategory, New System.EventArgs())
    End Sub

    Private Sub txtCategoryDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCategoryDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim MainClass_Renamed As Object
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
        Dim MainClass_Renamed As Object
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
        '        lblCatCode.Caption = MasterNo						
        '    End If						


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
        Dim MainClass_Renamed As Object
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
        Dim MainClass_Renamed As Object
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
        Dim SqlStr As String
        Dim mCategoryCode As String

        If Trim(txtItemName.Text) = "" Then GoTo EventExitSub
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        '    If chkFG.Value = vbUnchecked Then						
        '        If MainClass.ValidateWithMasterTable(txtFGName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then						
        '            mCategoryCode = MasterNo						
        '            SqlStr = SqlStr & " AND CATEGORY_CODE='" & mCategoryCode & "'"						
        '        End If						
        '    End If						

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
        Dim MainClass_Renamed As Object
        Dim Daysinmonth As Integer
        Dim cntCol As Integer
        Dim Tempdate As String
        Dim mDay As Integer
        Dim NewDate As Date

        MainClass.ClearGrid(SprdMain)

        Tempdate = "01/" & Month(xDate) & "/" & Year(xDate)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

    End Sub

    Private Sub lblYear_ValueChanged(sender As Object, e As EventArgs) Handles lblYear.ValueChanged
        lblRunDate.Text = lblYear.Text
    End Sub
    Private Sub lstMaterialType_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstMaterialType.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstMaterialType.GetItemChecked(0) = True Then
                    For I = 1 To lstMaterialType.Items.Count - 1
                        lstMaterialType.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstMaterialType.Items.Count - 1
                        lstMaterialType.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstMaterialType.GetItemChecked(e.Index - 1) = False Then
                    lstMaterialType.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub
End Class
