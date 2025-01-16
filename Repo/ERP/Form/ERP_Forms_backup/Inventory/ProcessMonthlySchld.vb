Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmProcessMonthlySchld
    Inherits System.Windows.Forms.Form
    ''Dim PvtDBCn As ADODB.Connection
    Dim mRowCnt As Integer
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()

    End Sub

    Private Sub cmdProcess_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdProcess.Click
        Dim SqlStr As String = ""

        If IsDate(txtDate.Text) = False Then
            MsgInformation("Planning Date Is Empty ")
            txtDate.Focus()
            Exit Sub
        End If

        If IsDate(txtStockDate.Text) = False Then
            MsgInformation("Stock Date Is Empty ")
            txtStockDate.Focus()
            Exit Sub
        End If

        If CDate(txtStockDate.Text) > CDate(txtDate.Text) Then
            MsgInformation("Stock Date Should be Less Than Schedule Date.")
            txtStockDate.Focus()
            Exit Sub
        End If

        If CDate(txtStockDate.Text) >= CDate(PubCurrDate) Then
            MsgInformation("Stock Date Should be Less Than CURRENT Date.")
            txtStockDate.Focus()
            Exit Sub
        End If

        'If PubSuperUser = "S" Or PubSuperUser = "A" Then
        '    If Val(txtBufferPer.Text) > 2 Then
        '        MsgInformation("Buffer % Cann't be Greater than 2.")
        '        txtBufferPer.Focus()
        '        Exit Sub
        '    End If
        'Else
        '    If Val(txtBufferPer.Text) > 0 Then
        '        MsgInformation("Buffer % Cann't be greater than 0.")
        '        txtBufferPer.Focus()
        '        Exit Sub
        '    End If
        'End If

        If Val(txtBufferPer.Text) < 0 Then
            MsgInformation("Buffer % Cann't be Less than 0.")
            txtBufferPer.Focus()
            Exit Sub
        End If



        If lblBookType.Text = VB.Left(ConPurchase, 1) And lblBookSubType.Text = VB.Right(ConPurchase, 1) Then
            If MsgQuestion("For your Information, Now Stock will be Consider for Procurement. Want to Continue ? ") = CStr(MsgBoxResult.No) Then
                Exit Sub
            End If
        End If

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If IsProcessed = True Then
            If lblBookType.Text = VB.Left(ConDespatchPlan, 1) And lblBookSubType.Text = VB.Right(ConDespatchPlan, 1) Then
                If MsgBox(" Revised Despatch & Production Planning Already Processed In This Date." & vbCrLf & " Still Want To Process", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                Else
                    SqlStr = "DELETE FROM INV_PROCESS_MONTHLY_SCHLD " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "' " & vbCrLf & " AND BOOKSUBTYPE='" & lblBookSubType.Text & "' " & vbCrLf _
                        & " AND PROCESS_DATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')"
                    PubDBCn.Execute(SqlStr)
                End If
            ElseIf lblBookType.Text = VB.Left(ConPurchase, 1) And lblBookSubType.Text = VB.Right(ConPurchase, 1) Then
                If MsgBox(" Monthly Schedule Already Processed In This Month." & vbCrLf & " Still Want To Process", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                Else
                    SqlStr = "DELETE FROM INV_PROCESS_MONTHLY_SCHLD " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "' " & vbCrLf & " AND BOOKSUBTYPE='" & lblBookSubType.Text & "' " & vbCrLf _
                        & " AND TO_CHAR(PROCESS_DATE,'MMYYYY')='" & VB6.Format(txtDate.Text, "MMYYYY") & "' "
                    PubDBCn.Execute(SqlStr)
                End If
            End If
        End If



        If Update1 = True Then
            MsgInformation("Processed Successfully")
        Else
            MsgInformation("Process Failed")
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub
    Private Function IsProcessed() As Boolean

        On Error GoTo IsERR
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCount As Double

        SqlStr = "SELECT COUNT(*) AS COUNT_NO  " & vbCrLf & " FROM INV_PROCESS_MONTHLY_SCHLD " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "' " & vbCrLf & " AND BOOKSUBTYPE='" & lblBookSubType.Text & "' "
        If lblBookType.Text = VB.Left(ConDespatchPlan, 1) And lblBookSubType.Text = VB.Right(ConDespatchPlan, 1) Then
            SqlStr = SqlStr & vbCrLf & " AND PROCESS_DATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')"
        ElseIf lblBookType.Text = VB.Left(ConPurchase, 1) And lblBookSubType.Text = VB.Right(ConPurchase, 1) Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(PROCESS_DATE,'MMYYYY')='" & VB6.Format(txtDate.Text, "MMYYYY") & "' "
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsTemp.EOF Then
            mCount = Val(IIf(IsDbNull(RsTemp.Fields("COUNT_NO").Value), "", RsTemp.Fields("COUNT_NO").Value))
        Else
            mCount = 0
        End If
        If mCount > 0 Then IsProcessed = True
        Exit Function
IsERR:
        MsgBox(Err.Description)

    End Function
    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsPlan As ADODB.Recordset = Nothing
        Dim mProdCode As String
        Dim mPlanQty As Double

        '' AND PRODUCT_CODE='F0065'     ' AND PRODUCT_CODE='F0388'    ' AND PRODUCT_CODE='F0388'  'AND ITEM_CODE='F00398' TO : ASSEMBLY (Production) -PMD-F00398

        '    SqlStr = "SELECT IH.PRODUCT_CODE AS ITEM_CODE,SUM(IH.CUST_ORD_QTY) AS DPLAN_QTY " & vbCrLf _
        ''            & " FROM PRD_PRODPLAN_HDR IH " & vbCrLf _
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
        ''            & " AND TO_CHAR(SCHLD_DATE,'MMYYYY')='" & VB6.Format(txtDate.Text, "MMYYYY") & "'" & vbCrLf _
        ''            & " GROUP BY PRODUCT_CODE " & vbCrLf _
        ''            & " ORDER BY PRODUCT_CODE "

        If optCustomerSchedule.Checked = True Then
            SqlStr = "SELECT ITEM_CODE, SUM(ID.PLANNED_QTY) + ROUND((SUM(ID.PLANNED_QTY) * " & Val(txtBufferPer.Text) & " * .01),0) AS DPLAN_QTY " & vbCrLf _
                    & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID " & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV " & vbCrLf _
                    & " AND TO_CHAR(ID.SCHLD_DATE,'MMYYYY')='" & VB6.Format(txtDate.Text, "MMYYYY") & "' " & vbCrLf _
                    & " AND SCHLD_STATUS='O' HAVING SUM(ID.PLANNED_QTY)>0 " & vbCrLf & " GROUP BY ITEM_CODE " & vbCrLf & " ORDER BY ITEM_CODE"
        Else
            SqlStr = "SELECT PRODUCT_CODE AS ITEM_CODE, SUM(CUST_ORD_QTY) + ROUND((SUM(CUST_ORD_QTY) * " & Val(txtBufferPer.Text) & " * .01),0) AS DPLAN_QTY " & vbCrLf _
                    & " FROM PRD_PRODPLAN_HDR " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND TO_CHAR(SCHLD_DATE,'MMYYYY')='" & VB6.Format(txtDate.Text, "MMYYYY") & "' " & vbCrLf _
                    & " HAVING SUM(CUST_ORD_QTY)>0 " & vbCrLf _
                    & " GROUP BY PRODUCT_CODE " & vbCrLf & " ORDER BY PRODUCT_CODE"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPlan, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsPlan.EOF Then
            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()
            'PBar.Min = 0
            'PBar.Max = GetRecordCount()
            'PBar.Value = PBar.Min

            Do While Not RsPlan.EOF
                mRowCnt = 1
                mProdCode = Trim(IIf(IsDbNull(RsPlan.Fields("ITEM_CODE").Value), "-1", RsPlan.Fields("ITEM_CODE").Value))
                mPlanQty = Val(IIf(IsDbNull(RsPlan.Fields("DPLAN_QTY").Value), "0", RsPlan.Fields("DPLAN_QTY").Value))
                If InsertIntoProcessTable(mProdCode, mPlanQty) = False Then GoTo ErrPart
                RsPlan.MoveNext()
                'PBar.Value = PBar.Value + 1
            Loop
            PubDBCn.CommitTrans()
        End If
        Update1 = True
        Exit Function
ErrPart:
        Update1 = False
        MsgBox(Err.Description)
        PubDBCn.RollbackTrans()
    End Function
    Private Function GetRecordCount() As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mRsCount As ADODB.Recordset = Nothing

        GetRecordCount = 0

        '    SqlStr = "SELECT COUNT(1) " & vbCrLf _
        ''            & " FROM PRD_PRODPLAN_HDR IH " & vbCrLf _
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
        ''            & " AND TO_CHAR(SCHLD_DATE,'MMYYYY')='" & VB6.Format(txtDate.Text, "MMYYYY") & "'"


        SqlStr = "SELECT COUNT(1) " & vbCrLf & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DELV_SCHLD_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV " & vbCrLf _
            & " AND TO_CHAR(SCHLD_DATE,'MMYYYY')='" & VB6.Format(txtDate.Text, "MMYYYY") & "'" & vbCrLf & " AND SCHLD_STATUS='O'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCount, ADODB.LockTypeEnum.adLockReadOnly)
        If mRsCount.EOF = False Then
            GetRecordCount = mRsCount.Fields(0).Value
        Else
            GetRecordCount = 0
        End If
        mRsCount.Close()
        mRsCount = Nothing
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function InsertIntoProcessTable(ByRef pProductCode As String, ByRef pPlanQty As Double) As Boolean

        On Error GoTo InsertErr
        Dim i As Integer
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mWef As String
        Dim mPUOM As String
        Dim mRMCode As String
        Dim mStockQty As Double
        Dim mRMUOM As String
        Dim mStdQty As Double
        Dim mRMReqd As Double
        Dim mRM_PURCHASE_COST As Double
        Dim mRM_LANDED_COST As Double
        Dim mFactor As Double
        Dim mLevel As Integer
        Dim mMainItemCode As String
        Dim xPlanQty As Double
        Dim mProductType As String

        mWef = ""
        mPUOM = ""
        mRMCode = ""
        mStockQty = 0
        mRMUOM = ""
        mStdQty = 0
        mRMReqd = 0
        mRM_PURCHASE_COST = 0
        mRM_LANDED_COST = 0

        mMainItemCode = GetMainItemCode(Trim(pProductCode))

        i = 1

        '    SqlStr = " SELECT  " & vbCrLf _
        ''            & " LEVEL,TRN.PRODUCT_CODE, TRN.STD_QTY+  GROSS_WT_SCRAP AS STD_QTY, DEPT_CODE" & vbCrLf _
        ''            & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf _
        ''            & " WHERE TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND STATUS='O'"
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " START WITH  TRIM(RM_CODE) || '-' || TRN.COMPANY_CODE='" & MainClass.AllowSingleQuote(nItemCode) & "-" & RsCompany.fields("COMPANY_CODE").value & "'" & vbCrLf _
        ''            & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE) || ' '=TRIM(RM_CODE) || COMPANY_CODE || ' '"


        SqlStr = " SELECT DISTINCT IH.PRODUCT_CODE, IH.WEF, " & vbCrLf & " ID.RM_CODE, INVITEM.ISSUE_UOM AS ISSUE_UOM, INVITEM.UOM_FACTOR, " & vbCrLf & " (ID.STD_QTY + GROSS_WT_SCRAP) AS STD_QTY, ID.DEPT_CODE " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVITEM" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVITEM.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVITEM.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND UPPER(TRIM(IH.PRODUCT_CODE))='" & MainClass.AllowSingleQuote(UCase(mMainItemCode)) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR "

        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND UPPER(TRIM(PRODUCT_CODE))='" & MainClass.AllowSingleQuote(UCase(mMainItemCode)) & "' " & vbCrLf _
            & " AND WEF<= TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STATUS='O') AND IH.STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.RM_CODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        mLevel = 1
        i = 1
        With RsShow
            If Not .EOF Then
                Do While Not .EOF

                    If MainClass.ValidateWithMasterTable(mMainItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mPUOM = MasterNo
                    End If
                    '                MsgBox RsShow!RM_CODE
                    xPlanQty = pPlanQty * IIf(IsDbNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)
                    Call FillGridCol(RsShow, i, mLevel, pProductCode, mMainItemCode, mPUOM, xPlanQty)
                    i = i + 1
NextRecd:
                    .MoveNext()
                Loop
            Else
                If mMainItemCode <> "" Then
                    mProductType = GetProductionType(mMainItemCode)
                    If mProductType = "B" Or mProductType = "R" Or mProductType = "D" Or mProductType = "3" Then
                        mRMCode = mMainItemCode
                        If MainClass.ValidateWithMasterTable(mMainItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mPUOM = MasterNo
                        End If
                        mRMUOM = mPUOM
                        mRMReqd = pPlanQty
                        mStockQty = GetBalanceStockQty(mRMCode, (txtStockDate.Text), mRMUOM, "", "ST", "", ConWH, -1)
                        mStockQty = mStockQty + GetBalanceStockQty(mRMCode, (txtStockDate.Text), mRMUOM, "", "QC", "", ConWH, -1)

                        SqlStr = "INSERT INTO INV_PROCESS_MONTHLY_SCHLD " & vbCrLf & " (BOOKTYPE, BOOKSUBTYPE, PROCESS_DATE, " & vbCrLf & " COMPANY_CODE, PRODUCT_CODE, SERIAL_NO, " & vbCrLf & " WEF, P_UOM,DPLAN_QTY, RM_TYPE, " & vbCrLf & " RM_CODE, STD_QTY, STOCK_QTY, " & vbCrLf & " RM_UOM, RM_QTY, RM_PURCHASECOST, RM_LANDCOST " & vbCrLf & " ) VALUES('" & lblBookType.Text & "','" & lblBookSubType.Text & "', " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(txtDate.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mMainItemCode) & "'," & i & ", " & vbCrLf & " '" & mWef & "','" & mPUOM & "'," & pPlanQty & ", " & vbCrLf & " 'P','" & mRMCode & "'," & mStdQty & ", " & vbCrLf & " " & mStockQty & ",'" & mRMUOM & "'," & mRMReqd & "," & mRM_PURCHASE_COST & ", " & mRM_LANDED_COST & ")"
                        PubDBCn.Execute(SqlStr)
                        i = i + 1
                    End If
                End If
            End If
        End With
        InsertIntoProcessTable = True
        Exit Function
InsertErr:
        '    Resume
        InsertIntoProcessTable = False
        MsgBox(Err.Description)
    End Function

    Private Sub FillGridCol(ByRef pRs As ADODB.Recordset, ByRef pSRNo As Integer, ByRef pLevel As Integer, ByRef pProductCode As String, ByRef pParentCode As String, ByRef pProductUOM As String, ByRef mPlanQty As Double)


        On Error GoTo FillGERR
        Dim SqlStr As String = ""
        Dim mRMCode As String
        Dim mItemUOM As String = ""
        Dim mStockQty As Double
        Dim mStdQty As Double
        Dim mDeptCode As String
        Dim mRate As Double
        Dim mFactorQty As Double
        Dim mChildRMCode As String
        Dim mWef As String
        Dim mRMUOM As String
        Dim mRMReqd As Double
        Dim mRM_PURCHASE_COST As Double
        Dim mRM_LANDED_COST As Double
        Dim mFactor As Double

        mRMCode = IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)
        If CheckSubRecord(mRMCode) = True Then
            If ISBOP_InHouse(mRMCode) = True Then
                mWef = VB6.Format(IIf(IsDbNull(pRs.Fields("WEF").Value), "", pRs.Fields("WEF").Value), "DD/MMM/YYYY")
                mRMUOM = MainClass.AllowSingleQuote(IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value))
                mRMCode = MainClass.AllowSingleQuote(IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
                mDeptCode = MainClass.AllowSingleQuote(IIf(IsDbNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value))
                If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "UOM_FACTOR", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mFactor = MasterNo
                End If

                mStockQty = GetBalanceStockQty(mRMCode, (txtStockDate.Text), mRMUOM, "", "ST", "", ConWH, -1)
                mStockQty = mStockQty + GetBalanceStockQty(mRMCode, (txtStockDate.Text), mRMUOM, "", "QC", "", ConWH, -1)
                mStdQty = Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value))

                mFactorQty = 1

                If mDeptCode = "J/W" Then
                    If mRMUOM = "KGS" Then
                        mFactorQty = 1
                    ElseIf mRMUOM = "TON" Then
                        mFactorQty = 1 / 1000
                        '                    mFactorQty = mFactorQty * 1000
                    End If
                Else
                    If mRMUOM = "KGS" Then
                        mFactorQty = 1 / 1000
                    ElseIf mRMUOM = "TON" Then
                        mFactorQty = 1 / (1000 * 1000)
                    End If
                End If

                mRMReqd = mPlanQty * mFactorQty ''* mStdQty
                '                mRate = GetCurrentItemRate(mRMCode, VB6.Format(RunDate, "DD/MM/YYYY"))

                mRM_PURCHASE_COST = 0
                mRM_LANDED_COST = 0
                '                If RsCompany.fields("COMPANY_CODE").value <> 1 Then
                '                    If GetLatestItemCostFromPO(mRMCode, mRM_PURCHASE_COST, mRM_LANDED_COST, txtDate.Text, "ST", "-1", mRMUOM, mFactor) = False Then GoTo FillGERR
                '                End If

                SqlStr = "INSERT INTO INV_PROCESS_MONTHLY_SCHLD " & vbCrLf & " (BOOKTYPE, BOOKSUBTYPE, PROCESS_DATE, " & vbCrLf & " COMPANY_CODE, PRODUCT_CODE, SERIAL_NO, " & vbCrLf & " WEF, P_UOM,DPLAN_QTY, RM_TYPE, " & vbCrLf & " RM_CODE, STD_QTY, STOCK_QTY, " & vbCrLf & " RM_UOM, RM_QTY, RM_PURCHASECOST, RM_LANDCOST " & vbCrLf & " ) VALUES('" & lblBookType.Text & "','" & lblBookSubType.Text & "', " & vbCrLf _
                    & " TO_DATE('" & VB6.Format(txtDate.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(pProductCode) & "'," & mRowCnt & ", " & vbCrLf & " '" & mWef & "','" & pProductUOM & "'," & mPlanQty & ", " & vbCrLf & " 'P','" & mRMCode & "'," & mStdQty & ", " & vbCrLf & " " & mStockQty & ",'" & mRMUOM & "'," & mRMReqd & "," & mRM_PURCHASE_COST & ", " & mRM_LANDED_COST & ")"
                PubDBCn.Execute(SqlStr)
                mRowCnt = mRowCnt + 1
            End If

            pLevel = pLevel + 1
            Call FillSubRecord(mRMCode, "", pSRNo, pLevel, pProductCode, pProductUOM, mPlanQty)

        Else

            mWef = VB6.Format(IIf(IsDbNull(pRs.Fields("WEF").Value), "", pRs.Fields("WEF").Value), "DD/MMM/YYYY")
            mRMUOM = MainClass.AllowSingleQuote(IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value))
            mRMCode = MainClass.AllowSingleQuote(IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
            mDeptCode = MainClass.AllowSingleQuote(IIf(IsDbNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value))
            If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "UOM_FACTOR", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mFactor = MasterNo
            End If



            mStockQty = GetBalanceStockQty(mRMCode, (txtStockDate.Text), mRMUOM, "", "ST", "", ConWH, -1)
            mStockQty = mStockQty + GetBalanceStockQty(mRMCode, (txtStockDate.Text), mRMUOM, "", "QC", "", ConWH, -1)
            mStdQty = Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value))

            mFactorQty = 1

            If mDeptCode = "J/W" Then
                If mRMUOM = "KGS" Then
                    mFactorQty = 1
                ElseIf mRMUOM = "TON" Then
                    mFactorQty = 1 / 1000
                    '                    mFactorQty = mFactorQty * 1000
                End If
            Else
                If mRMUOM = "KGS" Then
                    mFactorQty = 1 / 1000
                ElseIf mRMUOM = "TON" Then
                    mFactorQty = 1 / (1000 * 1000)
                End If
            End If

            mRMReqd = mPlanQty * mFactorQty ''* mStdQty
            '                mRate = GetCurrentItemRate(mRMCode, VB6.Format(RunDate, "DD/MM/YYYY"))

            mRM_PURCHASE_COST = 0
            mRM_LANDED_COST = 0
            '            If RsCompany.fields("COMPANY_CODE").value <> 1 Then
            '                If GetLatestItemCostFromPO(mRMCode, mRM_PURCHASE_COST, mRM_LANDED_COST, txtDate.Text, "ST", "-1", mRMUOM, mFactor) = False Then GoTo FillGERR
            '            End If

            SqlStr = "INSERT INTO INV_PROCESS_MONTHLY_SCHLD " & vbCrLf & " (BOOKTYPE, BOOKSUBTYPE, PROCESS_DATE, " & vbCrLf & " COMPANY_CODE, PRODUCT_CODE, SERIAL_NO, " & vbCrLf & " WEF, P_UOM,DPLAN_QTY, RM_TYPE, " & vbCrLf & " RM_CODE, STD_QTY, STOCK_QTY, " & vbCrLf & " RM_UOM, RM_QTY, RM_PURCHASECOST, RM_LANDCOST " & vbCrLf & " ) VALUES('" & lblBookType.Text & "','" & lblBookSubType.Text & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtDate.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(pProductCode) & "'," & mRowCnt & ", " & vbCrLf & " '" & mWef & "','" & pProductUOM & "'," & mPlanQty & ", " & vbCrLf & " 'P','" & mRMCode & "'," & mStdQty & ", " & vbCrLf & " " & mStockQty & ",'" & mRMUOM & "'," & mRMReqd & "," & mRM_PURCHASE_COST & ", " & mRM_LANDED_COST & ")"
            PubDBCn.Execute(SqlStr)
            mRowCnt = mRowCnt + 1
        End If

        '    Call FillSubAlterRecord(mRMCode, "", pSRNo, pLevel, pProductCode, mDeptCode, pParentCode, mPlanQty)
        '    Call FillSubRecord(mRMCode, "", pSRNo, pLevel, pProductCode, mPlanQty)

        Exit Sub
FillGERR:
        '    Resume
        MsgBox(Err.Description)
    End Sub
    Private Sub FillSubRecord(ByRef pProductCode As String, ByRef pWEF As String, ByVal pSrn As Integer, ByRef pLevel As Integer, ByRef pMainProductCode As String, ByRef pProductUOM As String, ByRef mPlanQty As Double)


        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String
        Dim xPlanQty As Double

        pLevel = pLevel + 1
        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, IH.WEF," & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY, ID.GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

        SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') " '& vbCrLf |            & " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "' AND STATUS='O')" & vbCrLf |
        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                '            pSrn = pSrn + 1
                mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                xPlanQty = mPlanQty * (Val(IIf(IsDbNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)) + Val(IIf(IsDbNull(RsShow.Fields("GROSS_WT_SCRAP").Value), 0, RsShow.Fields("GROSS_WT_SCRAP").Value)))
                Call FillGridCol(RsShow, pSrn, pLevel, pMainProductCode, pProductCode, pProductUOM, xPlanQty)
                RsShow.MoveNext()
            Loop
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, IH.WEF," & vbCrLf & " ID.ITEM_CODE AS RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ITEM_QTY AS STD_QTY, ID.SCRAP_QTY AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

            SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

            SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'"

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                Do While Not RsShow.EOF
                    '                pSrn = pSrn + 1
                    mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                    xPlanQty = mPlanQty * (Val(IIf(IsDbNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)) + Val(IIf(IsDbNull(RsShow.Fields("GROSS_WT_SCRAP").Value), 0, RsShow.Fields("GROSS_WT_SCRAP").Value)))
                    Call FillGridCol(RsShow, pSrn, pLevel, pMainProductCode, pProductCode, pProductUOM, xPlanQty)
                    RsShow.MoveNext()
                Loop
            End If
        End If
        RsShow = Nothing
        '        RsShow.Close

        Exit Sub
FillERR:
        MsgBox(Err.Description)
        '    Resume
    End Sub

    Private Function CheckSubRecord(ByRef pProductCode As String) As Boolean


        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        'Dim mRMCode As String
        'Dim mSrn As String
        'Dim xSrn As String
        'Dim j As Long
        '
        CheckSubRecord = False
        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, ID.RM_CODE " & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') " '& vbCrLf |            & " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "'  AND STATUS='O')" & vbCrLf |
        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            '        Do While Not RsShow.EOF
            '           mRMCode = Trim(IIf(IsNull(RsShow!RM_CODE), "", RsShow!RM_CODE))
            CheckSubRecord = True
            '        Loop
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, ID.ITEM_CODE AS RM_CODE " & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

            SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'"

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                '            Do While Not RsShow.EOF
                '                mRMCode = Trim(IIf(IsNull(RsShow!RM_CODE), "", RsShow!RM_CODE))
                CheckSubRecord = True
                RsShow.MoveNext()
                '            Loop
            End If
        End If
        RsShow = Nothing
        '        RsShow.Close

        Exit Function
FillERR:
        CheckSubRecord = False
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function ISBOP_InHouse(ByRef xProductCode As String) As Boolean

        On Error GoTo InsertErr
        Dim i As Integer
        Dim mRsBOM As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        ISBOP_InHouse = False

        If chkInhouseItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            Exit Function
        End If

        SqlStr = " SELECT IH.PRODUCT_CODE " & vbCrLf & " FROM PRD_NEWBOM_HDR IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(xProductCode) & "' " & vbCrLf & " AND IH.IS_BOP='Y' AND IH.IS_APPROVED='Y' AND IH.STATUS='O'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(xProductCode)) & "' " & vbCrLf & " AND WEF<= TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IS_BOP='Y' AND IS_APPROVED='Y' AND STATUS='O')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsBOM, ADODB.LockTypeEnum.adLockReadOnly)

        If Not mRsBOM.EOF Then
            ISBOP_InHouse = True
        End If
        Exit Function
InsertErr:
        ISBOP_InHouse = False
        MsgBox(Err.Description)
    End Function
    Private Sub frmProcessMonthlySchld_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If lblBookType.Text = VB.Left(ConDespatchPlan, 1) And lblBookSubType.Text = VB.Right(ConDespatchPlan, 1) Then
            Me.Text = "Process Revised Despatch & Production Planning"
            txtDate.Enabled = False
        ElseIf lblBookType.Text = VB.Left(ConPurchase, 1) And lblBookSubType.Text = VB.Right(ConPurchase, 1) Then
            Me.Text = "Process Monthly Schedule"
            txtDate.Enabled = True
        End If
        txtStockDate.Enabled = True
    End Sub

    Private Sub frmProcessMonthlySchld_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmProcessMonthlySchld_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        Me.Top = VB6.TwipsToPixelsY(20)
        Me.Left = VB6.TwipsToPixelsX(20)
        ''Me.Width = VB6.TwipsToPixelsX(5640)
        'Me.Height = VB6.TwipsToPixelsY(3540)
        txtDate.Text = "01/" & VB6.Format(RunDate, "MM/YYYY")
        txtStockDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
    End Sub
    Private Sub txtBufferPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBufferPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDate.Text) = True Then
            MsgBox("Not a valid date")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtStockDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtStockDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtStockDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtStockDate.Text) = True Then
            MsgBox("Not a valid date")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
