Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmProcessProductWiseInventory
    Inherits System.Windows.Forms.Form
    ''Dim PvtDBCn As ADODB.Connection
    Dim mRowCnt As Integer
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdProcess_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdProcess.Click
        'Dim SqlStr As String = ""

        If IsDate(txtFromDate.Text) = False Then
            MsgInformation("From Date Is Empty ")
            txtFromDate.Focus()
            Exit Sub
        End If

        If IsDate(txtFromDate.Text) = False Then
            MsgInformation("From Date Is Empty ")
            txtFromDate.Focus()
            Exit Sub
        End If

        If IsDate(txtStockDate.Text) = False Then
            MsgInformation("Stock Date Is Empty ")
            txtStockDate.Focus()
            Exit Sub
        End If

        If CDate(txtStockDate.Text) < CDate(txtFromDate.Text) Then
            MsgInformation("Stock Date Should not be less Than from Date.")
            txtStockDate.Focus()
            Exit Sub
        End If

        If CDate(txtStockDate.Text) >= CDate(PubCurrDate) Then
            MsgInformation("Stock Date Should be Less Than cURRENT Date.")
            txtStockDate.Focus()
            Exit Sub
        End If



        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor


        If Update1 = True Then
            MsgInformation("Processed Successfully")
        Else
            MsgInformation("Process Failed")
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub
    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsPlan As ADODB.Recordset = Nothing
        Dim mProdCode As String
        Dim mPlanQty As Double
        Dim mSqlStrTemp As String
        Dim mD3SaleQty As Double

        SqlStr = "SELECT ITEM_CODE, SUM(ID.PLANNED_QTY) AS DPLAN_QTY " & vbCrLf & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV " & vbCrLf & " AND TO_CHAR(ID.SCHLD_DATE,'MMYYYY')='" & VB6.Format(txtFromDate.Text, "MMYYYY") & "' " & vbCrLf & " AND SCHLD_STATUS='O' HAVING SUM(ID.PLANNED_QTY)>0 " & vbCrLf & " GROUP BY ITEM_CODE " & vbCrLf & " ORDER BY ITEM_CODE"

        ''AND ITEM_CODE IN ('FG6666','FG5757','FG5577')
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPlan, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsPlan.EOF Then
            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()

            mSqlStrTemp = "DELETE FROM TEMP_INV_PROCESS" '' WHERE USERID='" & PubUserID & "'"
            PubDBCn.Execute(mSqlStrTemp)

            'PBar.Min = 0
            'PBar.Max = GetRecordCount()
            'PBar.Value = PBar.Min

            Do While Not RsPlan.EOF
                mRowCnt = 1
                mProdCode = Trim(IIf(IsDbNull(RsPlan.Fields("ITEM_CODE").Value), "-1", RsPlan.Fields("ITEM_CODE").Value))
                mPlanQty = GetSaleQty(mProdCode, "N") 'Val(IIf(IsNull(RsPlan!DPLAN_QTY), "0", RsPlan!DPLAN_QTY))
                mD3SaleQty = GetSaleQty(mProdCode, "Y")
                If InsertIntoProcessTable(mProdCode, mPlanQty, mPlanQty, mD3SaleQty) = False Then GoTo ErrPart
                RsPlan.MoveNext()
                'PBar.Value = PBar.Value + 1
            Loop
            PubDBCn.CommitTrans()
        End If
        Update1 = True
        Exit Function
ErrPart:
        '    Resume
        Update1 = False
        MsgBox(Err.Description)
        PubDBCn.RollbackTrans()
    End Function

    Private Function GetSaleQty(ByRef pProdCode As String, ByRef mAgtD3Sale As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetSaleQty = 0

        SqlStr = "SELECT SUM(ID.ITEM_QTY) AS ITEM_QTY " & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pProdCode) & "' " & vbCrLf & " AND IH.CANCELLED='N' AND REF_DESP_TYPE<>'U'"

        If mAgtD3Sale = "N" Then
            SqlStr = SqlStr & " AND REF_DESP_TYPE<>'S'"
        Else
            SqlStr = SqlStr & " AND REF_DESP_TYPE='S'"
        End If


        SqlStr = SqlStr & vbCrLf & " AND INVOICE_DATE>=TO_DATE('" & VB6.Format(txtFromDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND INVOICE_DATE<=TO_DATE('" & VB6.Format(txtStockDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            GetSaleQty = IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If

        Exit Function
ErrPart:
        GetSaleQty = 0
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


        SqlStr = "SELECT COUNT(1) " & vbCrLf & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DELV_SCHLD_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV " & vbCrLf & " AND TO_CHAR(SCHLD_DATE,'MMYYYY')='" & VB6.Format(txtFromDate.Text, "MMYYYY") & "'" & vbCrLf & " AND SCHLD_STATUS='O'"

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

    Private Function InsertIntoProcessTable(ByRef pProductCode As String, ByRef pPlanQty As Double, ByRef mSaleQty As Double, ByRef mD3SaleQty As Double) As Boolean

        On Error GoTo InsertErr
        Dim i As Integer
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mWef As String
        Dim mPUOM As String
        Dim mRMCode As String
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

        Dim mOPStockQty As Double
        Dim mPurchaseQty As Double
        Dim mIssueQty As Double
        Dim mCLStockQty As Double
        Dim mFromDate As String

        mWef = ""
        mPUOM = ""
        mRMCode = ""
        mOPStockQty = 0
        mCLStockQty = 0
        mRMUOM = ""
        mStdQty = 0
        mRMReqd = 0
        mPurchaseQty = 0
        mIssueQty = 0

        mRM_PURCHASE_COST = 0
        mRM_LANDED_COST = 0

        '    mFromDate = "01/" & VB6.Format(txtDate.Text, "MM/YYYY")

        mMainItemCode = GetMainItemCode(Trim(pProductCode))

        i = 1


        SqlStr = " SELECT DISTINCT IH.PRODUCT_CODE, IH.WEF, " & vbCrLf & " ID.RM_CODE, INVITEM.ISSUE_UOM AS ISSUE_UOM, INVITEM.UOM_FACTOR, " & vbCrLf & " (ID.STD_QTY + GROSS_WT_SCRAP) AS STD_QTY, ID.DEPT_CODE " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVITEM" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVITEM.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVITEM.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND UPPER(TRIM(IH.PRODUCT_CODE))='" & MainClass.AllowSingleQuote(UCase(mMainItemCode)) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR "

        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND UPPER(TRIM(PRODUCT_CODE))='" & MainClass.AllowSingleQuote(UCase(mMainItemCode)) & "' " & vbCrLf & " AND WEF<= TO_DATE('" & VB6.Format(txtStockDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND STATUS='O') AND IH.STATUS='O'"

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
                    Call FillGridCol(RsShow, i, mLevel, pProductCode, mMainItemCode, mPUOM, xPlanQty, mSaleQty, mD3SaleQty)
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

                        mOPStockQty = GetBalanceStockQty(mRMCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtFromDate.Text))), mRMUOM, "", "ST", "", ConWH, -1) ' IIf(RsCompany.fields("COMPANY_CODE").value = 1, 0, GetBalanceStockQty(mRMCode, txtStockDate.Text, mRMUOM, "", "ST", "", ConWH, -1))
                        mOPStockQty = mOPStockQty + GetBalanceStockQty(mRMCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtFromDate.Text))), mRMUOM, "", "QC", "", ConWH, -1)

                        mCLStockQty = GetBalanceStockQty(mRMCode, (txtStockDate.Text), mRMUOM, "", "ST", "", ConWH, -1) ' IIf(RsCompany.fields("COMPANY_CODE").value = 1, 0, GetBalanceStockQty(mRMCode, txtStockDate.Text, mRMUOM, "", "ST", "", ConWH, -1))
                        mCLStockQty = mCLStockQty + GetBalanceStockQty(mRMCode, (txtStockDate.Text), mRMUOM, "", "QC", "", ConWH, -1)

                        mPurchaseQty = GetPurchaseQty(mRMCode)
                        mIssueQty = GetIssueQty(mRMCode)

                        SqlStr = "INSERT INTO TEMP_INV_PROCESS (" & vbCrLf & " USERID, PROCESS_DATE, COMPANY_CODE, " & vbCrLf & " PRODUCT_CODE, SERIAL_NO, WEF, " & vbCrLf & " P_UOM, SALE_QTY, RM_TYPE, " & vbCrLf & " RM_CODE, STD_QTY, OP_STOCK_QTY, " & vbCrLf & " PURCHASE_QTY, D3_SALE_QTY, ISSUE_QTY, " & vbCrLf & " CL_STOCK_QTY, RM_UOM, RM_QTY, RM_LANDCOST " & vbCrLf & " ) VALUES ( "

                        SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(txtFromDate.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(mMainItemCode) & "'," & i & ", '" & mWef & "'," & vbCrLf & " '" & mPUOM & "', " & mSaleQty & ", 'P'," & vbCrLf & " '" & mRMCode & "'," & mStdQty & ", " & mOPStockQty & ", " & vbCrLf & " " & mPurchaseQty & ", " & mD3SaleQty & ", " & mIssueQty & "," & vbCrLf & " " & mCLStockQty & ", '" & mRMUOM & "'," & mRMReqd & "," & mRM_LANDED_COST & "" & vbCrLf & " )"


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

    Private Function GetIssueQty(ByRef mItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTableName As String

        GetIssueQty = 0

        mTableName = ConInventoryTable


        SqlStr = " SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'O',1,-1)) AS ISSUE_QTY" & vbCrLf & " FROM " & mTableName & " INV " & vbCrLf & " WHERE INV.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND INV.STOCK_ID IN ('WH')" & vbCrLf & " AND INV.STATUS='O'" & vbCrLf & " AND REF_TYPE IN ('PMD','ISS','SRN') AND STOCK_TYPE IN ('ST','QC','RJ')"

        SqlStr = SqlStr & vbCrLf & " AND (INV.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"


        SqlStr = SqlStr & vbCrLf & " OR INV.ITEM_CODE IN ( " & vbCrLf & " SELECT ALTER_ITEM_CODE FROM INV_ITEM_ALTER_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'))"



        SqlStr = SqlStr & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtFromDate.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') "
        SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtStockDate.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetIssueQty = IIf(IsDbNull(RsTemp.Fields("ISSUE_QTY").Value), 0, RsTemp.Fields("ISSUE_QTY").Value)
        End If


        Exit Function
ErrPart:
        MsgBox(Err.Description)
        GetIssueQty = 0
    End Function

    Private Function GetPurchaseQty(ByRef pItemCode As String) As Double


        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPurchaseReturn As Double


        SqlStr = ""


        SqlStr = "SELECT SUM(ID.RECEIVED_QTY * DECODE(TRIM(ID.ITEM_UOM),TRIM(INVMST.PURCHASE_UOM),INVMST.UOM_FACTOR,1)) AS INQTY"
        SqlStr = SqlStr & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, INV_ITEM_MST INVMST"

        SqlStr = SqlStr & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE AND IH.REF_TYPE IN ('P','3')" & vbCrLf & " AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"


        SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE>=TO_DATE('" & VB6.Format(txtFromDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE<=TO_DATE('" & VB6.Format(txtStockDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDbNull(RsTemp.Fields(0).Value) Then
                GetPurchaseQty = 0
            Else
                GetPurchaseQty = RsTemp.Fields(0).Value
            End If
        Else
            GetPurchaseQty = 0
        End If

        SqlStr = ""

        SqlStr = "SELECT ABS(SUM(ID.PACKED_QTY * DECODE(TRIM(ID.ITEM_UOM),TRIM(INVMST.PURCHASE_UOM),INVMST.UOM_FACTOR,1))) AS INQTY"
        SqlStr = SqlStr & vbCrLf & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID, INV_ITEM_MST INVMST"

        SqlStr = SqlStr & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE AND IH.DESP_TYPE<>'U'" & vbCrLf & " AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"


        SqlStr = SqlStr & vbCrLf & " AND IH.DESP_DATE>=TO_DATE('" & VB6.Format(txtFromDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND IH.DESP_DATE<=TO_DATE('" & VB6.Format(txtStockDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDbNull(RsTemp.Fields(0).Value) Then
                mPurchaseReturn = 0
            Else
                mPurchaseReturn = RsTemp.Fields(0).Value
            End If
        Else
            mPurchaseReturn = 0
        End If

        GetPurchaseQty = GetPurchaseQty - mPurchaseReturn
        RsTemp = Nothing

        Exit Function
ErrPart:
        GetPurchaseQty = 0
    End Function

    Private Sub FillGridCol(ByRef pRs As ADODB.Recordset, ByRef pSRNo As Integer, ByRef pLevel As Integer, ByRef pProductCode As String, ByRef pParentCode As String, ByRef pProductUOM As String, ByRef mPlanQty As Double, ByRef mSaleQty As Double, ByRef mD3SaleQty As Double)


        On Error GoTo FillGERR
        Dim SqlStr As String = ""
        Dim mRMCode As String
        Dim mItemUOM As String = ""
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

        Dim mOPStockQty As Double
        Dim mPurchaseQty As Double
        Dim mIssueQty As Double
        Dim mCLStockQty As Double



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

                mOPStockQty = GetBalanceStockQty(mRMCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtFromDate.Text))), mRMUOM, "", "ST", "", ConWH, -1) ' IIf(RsCompany.fields("COMPANY_CODE").value = 1, 0, GetBalanceStockQty(mRMCode, txtStockDate.Text, mRMUOM, "", "ST", "", ConWH, -1))
                mOPStockQty = mOPStockQty + GetBalanceStockQty(mRMCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtFromDate.Text))), mRMUOM, "", "QC", "", ConWH, -1)

                mCLStockQty = GetBalanceStockQty(mRMCode, (txtStockDate.Text), mRMUOM, "", "ST", "", ConWH, -1) ' IIf(RsCompany.fields("COMPANY_CODE").value = 1, 0, GetBalanceStockQty(mRMCode, txtStockDate.Text, mRMUOM, "", "ST", "", ConWH, -1))
                mCLStockQty = mCLStockQty + GetBalanceStockQty(mRMCode, (txtStockDate.Text), mRMUOM, "", "QC", "", ConWH, -1)

                mPurchaseQty = GetPurchaseQty(mRMCode)
                mIssueQty = GetIssueQty(mRMCode)

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

                If GetLatestItemCostFromPO(mRMCode, mRM_PURCHASE_COST, mRM_LANDED_COST, (txtStockDate.Text), "ST", "-1", mRMUOM, mFactor) = False Then GoTo FillGERR


                SqlStr = "INSERT INTO TEMP_INV_PROCESS (" & vbCrLf & " USERID, PROCESS_DATE, COMPANY_CODE, " & vbCrLf & " PRODUCT_CODE, SERIAL_NO, WEF, " & vbCrLf & " P_UOM, SALE_QTY, RM_TYPE, " & vbCrLf & " RM_CODE, STD_QTY, OP_STOCK_QTY, " & vbCrLf & " PURCHASE_QTY, D3_SALE_QTY, ISSUE_QTY, " & vbCrLf & " CL_STOCK_QTY, RM_UOM, RM_QTY, RM_LANDCOST " & vbCrLf & " ) VALUES ( "

                SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(txtStockDate.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(pProductCode) & "'," & mRowCnt & ", '" & mWef & "'," & vbCrLf & " '" & pProductUOM & "', " & mSaleQty & ", 'P'," & vbCrLf & " '" & mRMCode & "'," & mStdQty & ", " & mOPStockQty & ", " & vbCrLf & " " & mPurchaseQty & ", " & mD3SaleQty & ", " & mIssueQty & "," & vbCrLf & " " & mCLStockQty & ", '" & mRMUOM & "'," & mRMReqd & "," & mRM_LANDED_COST & "" & vbCrLf & " )"

                '                         SqlStr = "INSERT INTO INV_PROCESS_MONTHLY_SCHLD " & vbCrLf _
                ''                    & " (BOOKTYPE, BOOKSUBTYPE, PROCESS_DATE, " & vbCrLf _
                ''                    & " COMPANY_CODE, PRODUCT_CODE, SERIAL_NO, " & vbCrLf _
                ''                    & " WEF, P_UOM,DPLAN_QTY, RM_TYPE, " & vbCrLf _
                ''                    & " RM_CODE, STD_QTY, STOCK_QTY, " & vbCrLf _
                ''                    & " RM_UOM, RM_QTY, RM_PURCHASECOST, RM_LANDCOST " & vbCrLf _
                ''                    & " ) VALUES('" & lblBookType.text & "','" & lblBookSubType.text & "', " & vbCrLf _
                ''                    & " '" & VB6.Format(txtDate.Text, "DD/MMM/YYYY") & "'," & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                ''                    & " '" & MainClass.AllowSingleQuote(pProductCode) & "'," & mRowCnt & ", " & vbCrLf _
                ''                    & " '" & mWef & "','" & pProductUOM & "'," & mPlanQty & ", " & vbCrLf _
                ''                    & " 'P','" & mRMCode & "'," & mStdQty & ", " & vbCrLf _
                ''                    & " " & mStockQty & ",'" & mRMUOM & "'," & mRMReqd & "," & mRM_PURCHASE_COST & ", " & mRM_LANDED_COST & ")"
                '
                PubDBCn.Execute(SqlStr)
                mRowCnt = mRowCnt + 1
            End If

            pLevel = pLevel + 1
            Call FillSubRecord(mRMCode, "", pSRNo, pLevel, pProductCode, pProductUOM, mPlanQty, mSaleQty, mD3SaleQty)

        Else

            mWef = VB6.Format(IIf(IsDbNull(pRs.Fields("WEF").Value), "", pRs.Fields("WEF").Value), "DD/MMM/YYYY")
            mRMUOM = MainClass.AllowSingleQuote(IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value))
            mRMCode = MainClass.AllowSingleQuote(IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
            mDeptCode = MainClass.AllowSingleQuote(IIf(IsDbNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value))
            If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "UOM_FACTOR", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mFactor = MasterNo
            End If


            mOPStockQty = GetBalanceStockQty(mRMCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtFromDate.Text))), mRMUOM, "", "ST", "", ConWH, -1) ' IIf(RsCompany.fields("COMPANY_CODE").value = 1, 0, GetBalanceStockQty(mRMCode, txtStockDate.Text, mRMUOM, "", "ST", "", ConWH, -1))
            mOPStockQty = mOPStockQty + GetBalanceStockQty(mRMCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtFromDate.Text))), mRMUOM, "", "QC", "", ConWH, -1)

            mCLStockQty = GetBalanceStockQty(mRMCode, (txtStockDate.Text), mRMUOM, "", "ST", "", ConWH, -1) ' IIf(RsCompany.fields("COMPANY_CODE").value = 1, 0, GetBalanceStockQty(mRMCode, txtStockDate.Text, mRMUOM, "", "ST", "", ConWH, -1))
            mCLStockQty = mCLStockQty + GetBalanceStockQty(mRMCode, (txtStockDate.Text), mRMUOM, "", "QC", "", ConWH, -1)

            mPurchaseQty = GetPurchaseQty(mRMCode)
            mIssueQty = GetIssueQty(mRMCode)

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
            If GetLatestItemCostFromPO(mRMCode, mRM_PURCHASE_COST, mRM_LANDED_COST, (txtStockDate.Text), "ST", "-1", mRMUOM, mFactor) = False Then GoTo FillGERR

            SqlStr = "INSERT INTO TEMP_INV_PROCESS (" & vbCrLf & " USERID, PROCESS_DATE, COMPANY_CODE, " & vbCrLf & " PRODUCT_CODE, SERIAL_NO, WEF, " & vbCrLf & " P_UOM, SALE_QTY, RM_TYPE, " & vbCrLf & " RM_CODE, STD_QTY, OP_STOCK_QTY, " & vbCrLf & " PURCHASE_QTY, D3_SALE_QTY, ISSUE_QTY, " & vbCrLf & " CL_STOCK_QTY, RM_UOM, RM_QTY, RM_LANDCOST " & vbCrLf & " ) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(txtStockDate.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(pProductCode) & "'," & mRowCnt & ", '" & mWef & "'," & vbCrLf & " '" & pProductUOM & "', " & mSaleQty & ", 'P'," & vbCrLf & " '" & mRMCode & "'," & mStdQty & ", " & mOPStockQty & ", " & vbCrLf & " " & mPurchaseQty & ", " & mD3SaleQty & ", " & mIssueQty & "," & vbCrLf & " " & mCLStockQty & ", '" & mRMUOM & "'," & mRMReqd & "," & mRM_LANDED_COST & "" & vbCrLf & " )"

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
    Private Sub FillSubRecord(ByRef pProductCode As String, ByRef pWEF As String, ByVal pSrn As Integer, ByRef pLevel As Integer, ByRef pMainProductCode As String, ByRef pProductUOM As String, ByRef mPlanQty As Double, ByRef mSaleQty As Double, ByRef mD3SaleQty As Double)


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
                Call FillGridCol(RsShow, pSrn, pLevel, pMainProductCode, pProductCode, pProductUOM, xPlanQty, mSaleQty, mD3SaleQty)
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
                    Call FillGridCol(RsShow, pSrn, pLevel, pMainProductCode, pProductCode, pProductUOM, xPlanQty, mSaleQty, mD3SaleQty)
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

        SqlStr = " SELECT IH.PRODUCT_CODE " & vbCrLf & " FROM PRD_NEWBOM_HDR IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(xProductCode) & "' " & vbCrLf & " AND IH.IS_BOP='Y' AND IH.IS_APPROVED='Y' AND IH.STATUS='O'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(xProductCode)) & "' " & vbCrLf & " AND WEF<= TO_DATE('" & VB6.Format(txtStockDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IS_BOP='Y' AND IS_APPROVED='Y' AND STATUS='O')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsBOM, ADODB.LockTypeEnum.adLockReadOnly)

        If Not mRsBOM.EOF Then
            ISBOP_InHouse = True
        End If
        Exit Function
InsertErr:
        ISBOP_InHouse = False
        MsgBox(Err.Description)
    End Function
    Private Sub frmProcessProductWiseInventory_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        txtFromDate.Enabled = True
        txtStockDate.Enabled = True
    End Sub

    Private Sub frmProcessProductWiseInventory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmProcessProductWiseInventory_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        Me.Top = VB6.TwipsToPixelsY(20)
        Me.Left = VB6.TwipsToPixelsX(20)
        ''Me.Width = VB6.TwipsToPixelsX(5640)
        'Me.Height = VB6.TwipsToPixelsY(3540)
        txtFromDate.Text = "01/" & VB6.Format(RunDate, "MM/YYYY")
        txtStockDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
    End Sub
    Private Sub txtFromDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtFromDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtFromDate.Text) = True Then
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
