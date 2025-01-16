Option Strict Off
Option Explicit On

Imports Microsoft.VisualBasic.Compatibility
Friend Class frmStoreAdjustmentProcess
    Inherits System.Windows.Forms.Form
    Dim mStockID As String
    Dim XRIGHT As String
    Dim xMyMenu As String

    Private Sub chkAllItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllItem.CheckStateChanged

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItemCode.Enabled = False
            cmdSearch.Enabled = False
        Else
            txtItemCode.Enabled = True
            cmdSearch.Enabled = True
        End If

    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler
        Dim mDivisionCode As Double

        If Trim(txtEmpCode.Text) = "" Then Exit Sub

        If MainClass.ValidateWithMasterTable(Trim(txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Emp Code")
            Exit Sub
        End If

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtItemCode.Text) = "" Then Exit Sub
            If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
                Exit Sub
            End If
        End If

        If Trim(txtDept.Text) = "" Then Exit Sub

        If MainClass.ValidateWithMasterTable(Trim(txtDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Depatment Code")
            Exit Sub
        End If

        If TxtAdjDate.Text = "__/__/____" Then Exit Sub
        If IsDate(TxtAdjDate.Text) = False Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            TxtAdjDate.Focus()
            Exit Sub
        End If

        If FYChk(VB6.Format(TxtAdjDate.Text, "DD/MM/YYYY")) = False Then
            Exit Sub
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Sub
        End If

        If Trim(cboDivision.Text) = "ALL" Then
            mDivisionCode = -1
        Else
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = Trim(MasterNo)
            End If
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If UpdateMain1(mDivisionCode) = False Then
            '    If UpdateMainKJ = False Then						
            '    If UpdateTemp = False Then						
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

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        Call TxtDept_DoubleClick(txtDept, New System.EventArgs())
    End Sub

    Private Sub frmStoreAdjustmentProcess_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Function AutoGenSeqNo() As String
        On Error GoTo AutoGenSeqNoErr
        Dim RsMRRMainGen As ADODB.Recordset
        Dim mNewSeqNo As Integer
        Dim SqlStr As String
        Dim mMaxValue As String

        OpenLocalConnection()



        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_ADJ)  " & vbCrLf _
            & " FROM INV_ADJ_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_ADJ,LENGTH(AUTO_KEY_ADJ)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, LocalPubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMRRMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = Mid(mMaxValue, 1, Len(mMaxValue) - 6)
                    'mNewSeqNo = CInt(Mid(.Fields(0).Value, 1, Len(.Fields(0).Value) - 6))
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With
        AutoGenSeqNo = mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")

        RsMRRMainGen.Close()
        RsMRRMainGen = Nothing
        CloseLocalConnection()
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateMain1(ByRef mDivisionCode As Double) As Boolean
        On Error GoTo ErrPart
        'Dim mSqlStr As String						
        Dim SqlStr As String
        Dim mSqlStr As String
        Dim RsPhy As ADODB.Recordset
        Dim RsStock As ADODB.Recordset
        Dim mVNoSeq As Double
        '						
        Dim mAdjDate As String
        Dim mDeptCode As String
        Dim mEmpCode As String
        Dim mStkType As String
        Dim mItemCode As String
        Dim mUOM As String
        Dim mAdjQty As Double
        Dim mActualQty As Double
        Dim mPhyQty As Double
        Dim mRecordCount As Double
        Dim mIO As String
        Dim mBookType As String
        Dim mCostCentre As String
        Dim mStrSql As String
        Dim RsItem As ADODB.Recordset
        Dim mInvTable As String
        Dim mHDRUpdate As Boolean
        Dim mNarration As String

        Dim mPhyItemCode As String
        Dim mItemUOM As String
        Dim mStockType As String
        Dim mPhyItemQty As Double
        Dim mCatCode As String

        Dim mRMCatCode As String
        Dim mRMCatCodeStr As String
        Dim CntLst As Integer
        Dim mMaterialType As String
        Dim mChkItemCode As String

        Dim mBatchNo As String
        Dim mHeatNo As String

        mDeptCode = Trim(txtDept.Text)
        mAdjDate = VB6.Format(TxtAdjDate.Text, "DD/MM/YYYY")

        If optWareHouse(0).Checked = True Then
            mBookType = ConWH
        ElseIf optWareHouse(1).Checked = True Then
            mBookType = ConPH
        Else
            mBookType = ConSH
        End If

        mInvTable = ConInventoryTable
        OpenLocalConnection()
        LocalPubDBCn.Errors.Clear()
        LocalPubDBCn.BeginTrans()

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then

            SqlStr = "DELETE FROM " & mInvTable & " " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'" & vbCrLf _
                & " AND REF_TYPE='" & ConStockRefType_ADJ & "'" & vbCrLf _
                & " AND REF_DATE=TO_DATE('" & VB6.Format(mAdjDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND STOCK_ID='" & mBookType & "'"

            If mBookType = ConPH Or mBookType = ConSH Then
                SqlStr = SqlStr & vbCrLf & " AND (DEPT_CODE_TO='" & Trim(txtDept.Text) & "' OR DEPT_CODE_FROM='" & Trim(txtDept.Text) & "')"
            End If

            If mDivisionCode <> -1 Then
                SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
            End If

            If CboSType.Text <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "'"
            End If


            LocalPubDBCn.Execute(SqlStr)


            SqlStr = "DELETE FROM INV_ADJ_DET " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'"

            If CboSType.Text <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "'"
            End If

            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_ADJ IN (" & vbCrLf _
                & " SELECT AUTO_KEY_ADJ FROM INV_ADJ_HDR" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ADJ_DATE=TO_DATE('" & VB6.Format(mAdjDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


            If mBookType = ConPH Or mBookType = ConSH Then
                SqlStr = SqlStr & vbCrLf & " AND (DEPT_CODE='" & Trim(txtDept.Text) & "')"
            End If

            If mDivisionCode <> -1 Then
                SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
            End If

            SqlStr = SqlStr & vbCrLf & ")"

            LocalPubDBCn.Execute(SqlStr)
        End If


        mSqlStr = "DELETE FROM TEMP_PHY_STOCK_TRN"
        LocalPubDBCn.Execute(mSqlStr)

        mSqlStr = "INSERT INTO TEMP_PHY_STOCK_TRN (" & vbCrLf _
            & " DEPT_CODE, ITEM_CODE, ISSUE_UOM, " & vbCrLf _
            & " STOCK_TYPE, ITEM_QTY, STOCK_ID, STOCK_COUNT, DIV_CODE,BATCH_NO,HEAT_NO )" & vbCrLf _
            & " SELECT  '" & Trim(txtDept.Text) & "'," & vbCrLf _
            & " STOCK.ITEM_CODE,ISSUE_UOM," & vbCrLf _
            & " CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(TxtAdjDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END ," & vbCrLf _
            & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)),'9999999999.9999') as Closing ,'" & mBookType & "','A',DIV_CODE, STOCK.BATCH_NO, STOCK.HEAT_NO" & vbCrLf _
            & " FROM " & mInvTable & " STOCK," & vbCrLf _
            & " INV_ITEM_MST Item  Where " & vbCrLf _
            & " STOCK.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND STOCK.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND STOCK.STOCK_ID='" & mBookType & "' " & vbCrLf _
            & " AND STOCK.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf _
            & " AND STOCK.ITEM_CODE=ITEM.ITEM_CODE"

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", LocalPubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mChkItemCode = MasterNo
                mSqlStr = mSqlStr & vbCrLf & "AND STOCK.ITEM_CODE='" & MainClass.AllowSingleQuote(mChkItemCode) & "'"
            End If
        End If

        If mBookType = ConPH Or mBookType = ConSH Then
            mSqlStr = mSqlStr & vbCrLf & " AND (STOCK.DEPT_CODE_TO='" & Trim(txtDept.Text) & "' OR STOCK.DEPT_CODE_FROM='" & Trim(txtDept.Text) & "')"
        End If

        If mDivisionCode <> -1 Then
            mSqlStr = mSqlStr & vbCrLf & " AND STOCK.DIV_CODE=" & mDivisionCode & ""
        End If

        If CboSType.Text <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND STOCK.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "'"
        End If

        If optUpdate(1).Checked = True Then
            mSqlStr = mSqlStr & vbCrLf _
                & " AND STOCK.ITEM_CODE IN (" & vbCrLf _
                & " SELECT ID.ITEM_CODE " & vbCrLf _
                & " FROM INV_PHY_HDR IH, INV_PHY_DET ID " & vbCrLf _
                & " WHERE " & vbCrLf _
                & " IH.AUTO_KEY_PHY=ID.AUTO_KEY_PHY" & vbCrLf _
                & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.DEPT_CODE = '" & MainClass.AllowSingleQuote(Trim(txtDept.Text)) & "'" & vbCrLf _
                & " AND IH.BOOKTYPE = '" & MainClass.AllowSingleQuote(mBookType) & "'"

            If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", LocalPubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mChkItemCode = MasterNo
                    mSqlStr = mSqlStr & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mChkItemCode) & "'"
                End If
            End If

            If mDivisionCode <> -1 Then
                mSqlStr = mSqlStr & vbCrLf & " AND IH.DIV_CODE=" & mDivisionCode & ""
            End If
            mSqlStr = mSqlStr & vbCrLf & " AND IH.PHY_DATE= TO_DATE('" & VB6.Format(mAdjDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        End If

        '    mSqlStr = mSqlStr & vbCrLf & "AND STOCK.ITEM_CODE='R00109'"						


        If mDivisionCode <> -1 Then
            mSqlStr = mSqlStr & vbCrLf & " AND STOCK.DIV_CODE=" & mDivisionCode & ""
        End If

        If CboSType.Text <> "" Then
            If CboSType.Text = "QC" Then
                mSqlStr = mSqlStr & vbCrLf _
                    & " AND (STOCK.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "' OR E_DATE>TO_DATE('" & VB6.Format(TxtAdjDate.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'))"
            ElseIf CboSType.Text = "ST" Or CboSType.Text = "RJ" Then
                mSqlStr = mSqlStr & vbCrLf _
                    & " AND (STOCK.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "' AND E_DATE<=TO_DATE('" & VB6.Format(TxtAdjDate.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'))"
            Else
                mSqlStr = mSqlStr & vbCrLf _
                    & " AND STOCK.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "'"
            End If
        End If

        mSqlStr = mSqlStr & vbCrLf & " AND STOCK.STATUS = 'O'"

        mSqlStr = mSqlStr & vbCrLf _
            & " AND REF_DATE<=TO_DATE('" & VB6.Format(TxtAdjDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " Group By  STOCK.ITEM_CODE, ISSUE_UOM,DIV_CODE,BATCH_NO,HEAT_NO," & vbCrLf _
            & " CASE WHEN REF_TYPE='MRR' AND E_DATE>TO_DATE('" & VB6.Format(TxtAdjDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 'QC' ELSE STOCK.STOCK_TYPE END" & vbCrLf _
            & " Order By STOCK.ITEM_CODE"

        '    MsgBox "ok2"						
        LocalPubDBCn.Execute(mSqlStr)
        LocalPubDBCn.CommitTrans()
        CloseLocalConnection()


        OpenLocalConnection()
        LocalPubDBCn.Errors.Clear()
        LocalPubDBCn.BeginTrans()

        mSqlStr = ""

        mSqlStr = "INSERT INTO TEMP_PHY_STOCK_TRN (" & vbCrLf _
            & " DEPT_CODE, ITEM_CODE, ISSUE_UOM, " & vbCrLf _
            & " STOCK_TYPE, ITEM_QTY, STOCK_ID, STOCK_COUNT, DIV_CODE,BATCH_NO, HEAT_NO )"

        mSqlStr = mSqlStr & vbCrLf _
            & " SELECT  '" & Trim(txtDept.Text) & "', " & vbCrLf _
            & " TRIM(ID.ITEM_CODE) AS ITEM_CODE, TRIM(ID.ITEM_UOM) AS ITEM_UOM," & vbCrLf _
            & " TRIM(ID.STOCK_TYPE) AS STOCK_TYPE," & vbCrLf _
            & " ID.PHY_QTY * DECODE(ID.ITEM_IO,'I',1,-1) AS ITEM_QTY, '" & mBookType & "','P', DIV_CODE,BATCH_NO, HEAT_NO" & vbCrLf _
            & " FROM INV_PHY_HDR IH, INV_PHY_DET ID " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.AUTO_KEY_PHY=ID.AUTO_KEY_PHY" & vbCrLf _
            & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.DEPT_CODE = '" & MainClass.AllowSingleQuote(Trim(txtDept.Text)) & "'" & vbCrLf _
            & " AND IH.BOOKTYPE = '" & MainClass.AllowSingleQuote(mBookType) & "'" & vbCrLf _
            & " AND IH.PHY_DATE= TO_DATE('" & VB6.Format(mAdjDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", LocalPubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mChkItemCode = MasterNo
                mSqlStr = mSqlStr & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mChkItemCode) & "'"
            End If
        End If

        If mDivisionCode <> -1 Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.DIV_CODE=" & mDivisionCode & ""
        End If

        If CboSType.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "'"
        End If

        '    mSqlStr = mSqlStr & vbCrLf & " AND ITEM_CODE='R00109'"						

        '     mSqlStr = mSqlStr & vbCrLf _						
        ''            & " Group By  ID.ITEM_CODE, ID.ITEM_UOM, ID.STOCK_TYPE "						

        'MainClass.UOpenRecordSet(mSqlStr, LocalPubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPhy, ADODB.LockTypeEnum.adLockReadOnly)

        'If RsPhy.EOF = False Then
        '    Do While Not RsPhy.EOF

        '        mPhyItemCode = IIf(IsDBNull(RsPhy.Fields("ITEM_CODE").Value), "", RsPhy.Fields("ITEM_CODE").Value)
        '        mItemUOM = IIf(IsDBNull(RsPhy.Fields("ITEM_UOM").Value), "", RsPhy.Fields("ITEM_UOM").Value)
        '        mStockType = IIf(IsDBNull(RsPhy.Fields("STOCK_TYPE").Value), "", RsPhy.Fields("STOCK_TYPE").Value)
        '        mPhyItemQty = CDbl(VB6.Format(IIf(IsDBNull(RsPhy.Fields("PHYQTY").Value), 0, RsPhy.Fields("PHYQTY").Value), "0.0000"))
        '        mDivisionCode = IIf(IsDBNull(RsPhy.Fields("DIV_CODE").Value), -1, RsPhy.Fields("DIV_CODE").Value)

        '        mSqlStr = " INSERT INTO TEMP_PHY_STOCK_TRN (" & vbCrLf _
        '            & " DEPT_CODE, ITEM_CODE, ISSUE_UOM, " & vbCrLf _
        '            & " STOCK_TYPE, ITEM_QTY, STOCK_ID, STOCK_COUNT,DIV_CODE ) VALUES (" & vbCrLf _
        '            & " '" & Trim(txtDept.Text) & "'," & vbCrLf _
        '            & " '" & MainClass.AllowSingleQuote(mPhyItemCode) & "','" & MainClass.AllowSingleQuote(mItemUOM) & "'," & vbCrLf _
        '            & " '" & mStockType & "'," & vbCrLf _
        '            & " " & mPhyItemQty & ", '" & mBookType & "','P'," & mDivisionCode & ")"

        '        '            MsgBox "ok3"						
        '        LocalPubDBCn.Execute(mSqlStr)
        '        RsPhy.MoveNext()
        '    Loop
        'End If

        LocalPubDBCn.Execute(mSqlStr)

        LocalPubDBCn.CommitTrans()
        CloseLocalConnection()


        mSqlStr = "SELECT STOCK.DEPT_CODE, INV.ITEM_CODE, INV.ISSUE_UOM, STOCK.STOCK_TYPE, DIV_CODE,DECODE(BATCH_NO,'-1',NULL,BATCH_NO) AS BATCH_NO, HEAT_NO," & vbCrLf _
            & " SUM(DECODE(STOCK_COUNT,'P',1,-1)*ITEM_QTY) AS QTY_DIFF" & vbCrLf _
            & " FROM TEMP_PHY_STOCK_TRN STOCK, INV_ITEM_MST INV" & vbCrLf _
            & " WHERE INV.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND STOCK.ITEM_CODE=INV.ITEM_CODE "

        For CntLst = 0 To lstMaterialType.Items.Count - 1
            If lstMaterialType.GetItemChecked(CntLst) = True Then
                mMaterialType = VB6.GetItemString(lstMaterialType, CntLst)
                If MainClass.ValidateWithMasterTable(mMaterialType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mRMCatCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mRMCatCodeStr = IIf(mRMCatCodeStr = "", "'" & mRMCatCode & "'", mRMCatCodeStr & "," & "'" & mRMCatCode & "'")
            End If
        Next

        If mRMCatCodeStr <> "" Then
            mRMCatCodeStr = "(" & mRMCatCodeStr & ")"
            mSqlStr = mSqlStr & vbCrLf & "AND INV.CATEGORY_CODE IN " & mRMCatCodeStr & ""
        End If

        mSqlStr = mSqlStr & vbCrLf & " AND STOCK_ID='" & mBookType & "'"

        If mDivisionCode <> -1 Then
            mSqlStr = mSqlStr & vbCrLf & " AND STOCK.DIV_CODE=" & mDivisionCode & ""
        End If

        'If PubUserID <> "G0416" Then
        mSqlStr = mSqlStr & vbCrLf & " AND STOCK.STOCK_TYPE NOT IN ('QC')"
        'End If

        If CboSType.Text <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND STOCK.STOCK_TYPE='" & MainClass.AllowSingleQuote(CboSType.Text) & "'"
        End If

        mSqlStr = mSqlStr & vbCrLf & " HAVING SUM(DECODE(STOCK_COUNT,'P',1,-1)*ITEM_QTY)<>0"

        mSqlStr = mSqlStr & vbCrLf & " GROUP BY STOCK.DEPT_CODE,INV.ITEM_CODE, INV.ISSUE_UOM, STOCK.STOCK_TYPE,DIV_CODE,DECODE(BATCH_NO,'-1',NULL,BATCH_NO), HEAT_NO "

        mSqlStr = mSqlStr & vbCrLf & " ORDER BY DIV_CODE"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsStock, ADODB.LockTypeEnum.adLockReadOnly)

        mRecordCount = 0

        mEmpCode = Trim(txtEmpCode.Text)
        mHDRUpdate = False

        If RsStock.EOF = False Then
            Do While Not RsStock.EOF

                mRecordCount = mRecordCount + 1

                mDivisionCode = CDbl(Trim(IIf(IsDBNull(RsStock.Fields("DIV_CODE").Value), "", RsStock.Fields("DIV_CODE").Value)))
                mDeptCode = Trim(IIf(IsDBNull(RsStock.Fields("DEPT_CODE").Value), "", RsStock.Fields("DEPT_CODE").Value))
                mItemCode = Trim(IIf(IsDBNull(RsStock.Fields("ITEM_CODE").Value), "", RsStock.Fields("ITEM_CODE").Value))
                mUOM = Trim(IIf(IsDBNull(RsStock.Fields("ISSUE_UOM").Value), "", RsStock.Fields("ISSUE_UOM").Value))
                mStkType = Trim(IIf(IsDBNull(RsStock.Fields("STOCK_TYPE").Value), "", RsStock.Fields("STOCK_TYPE").Value))
                mAdjQty = Val(IIf(IsDBNull(RsStock.Fields("QTY_DIFF").Value), 0, RsStock.Fields("QTY_DIFF").Value))
                mIO = IIf(Val(CStr(mAdjQty)) >= 0, "I", "O")

                mBatchNo = Trim(IIf(IsDBNull(RsStock.Fields("BATCH_NO").Value), "", RsStock.Fields("BATCH_NO").Value))
                mHeatNo = Trim(IIf(IsDBNull(RsStock.Fields("HEAT_NO").Value), "", RsStock.Fields("HEAT_NO").Value))


                If mItemCode <> "" Then
                    If mHDRUpdate = False Then
                        mVNoSeq = CDbl(AutoGenSeqNo())


                        mNarration = "PHYSICAL INVENTORY"

                        SqlStr = "INSERT INTO INV_ADJ_HDR (" & vbCrLf _
                            & " AUTO_KEY_ADJ, " & vbCrLf _
                            & " COMPANY_CODE, " & vbCrLf _
                            & " ADJ_DATE, " & vbCrLf _
                            & " DEPT_CODE, " & vbCrLf _
                            & " EMP_CODE, COST_CENTER_CODE, REMARKS,   " & vbCrLf _
                            & " ADDUSER,ADDDATE,MODUSER,MODDATE, BOOKTYPE,DIV_CODE)" & vbCrLf _
                            & " VALUES( " & vbCrLf & " " & Val(CStr(mVNoSeq)) & "," & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                            & " TO_DATE('" & VB6.Format(mAdjDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mDeptCode) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mEmpCode) & "', " & vbCrLf _
                            & " '001', " & vbCrLf & " '" & mNarration & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " '','','" & mBookType & "'," & mDivisionCode & ")"

                        OpenLocalConnection()
                        LocalPubDBCn.Errors.Clear()
                        LocalPubDBCn.BeginTrans()

                        LocalPubDBCn.Execute(SqlStr)

                        LocalPubDBCn.CommitTrans()
                        CloseLocalConnection()
                    End If

                    '                  MsgBox "ok5"						
                    If UpdateDetail1(Val(CStr(mVNoSeq)), mAdjDate, mDeptCode, mItemCode, mUOM, mStkType, System.Math.Abs(mAdjQty), mIO, mHDRUpdate, mRecordCount, mDivisionCode, mBatchNo, mHeatNo) = False Then GoTo ErrPart

                    mHDRUpdate = True
                End If

                RsStock.MoveNext()
                If RsStock.EOF = False Then
                    If mDivisionCode <> CDbl(Trim(IIf(IsDBNull(RsStock.Fields("DIV_CODE").Value), -1, RsStock.Fields("DIV_CODE").Value))) Then
                        mHDRUpdate = False
                    End If
                    lblCount.Text = mRecordCount & " - " & mItemCode
                End If
                System.Windows.Forms.Application.DoEvents()
            Loop
        End If

        UpdateMain1 = True

        RsStock.Close()
        RsStock = Nothing




        Exit Function
ErrPart:
        UpdateMain1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        LocalPubDBCn.RollbackTrans() ''		

        CloseLocalConnection()
        'If Err.Description = "" Then Exit Function
        'ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

        ''Resume						
    End Function

    Private Function UpdateTemp() As Boolean
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim SqlStr As String
        Dim RsStock As ADODB.Recordset
        Dim mVNoSeq As Double

        Dim mAdjDate As String
        Dim mDeptCode As String
        Dim mEmpCode As String
        Dim mStkType As String
        Dim mItemCode As String
        Dim mUOM As String
        Dim mAdjQty As Double
        Dim mRecordCount As Double
        Dim mIO As String
        Dim mDivisionCode As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        '    lblStockID.text = "WH"						

        mSqlStr = "SELECT EMP_CODE, ID.ITEM_CODE,IH.AUTO_KEY_ADJ,ID.STOCK_TYPE,ID.ITEM_UOM,IH.ADJ_DATE," & vbCrLf & " ADJ_QTY, ITEM_IO,DIV_CODE" & vbCrLf & " FROM  INV_ADJ_HDR IH, INV_ADJ_DET ID" & vbCrLf & " WHERE IH.AUTO_KEY_ADJ=ID.AUTO_KEY_ADJ AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_ADJ,LENGTH(IH.AUTO_KEY_ADJ)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.DEPT_CODE='PRS'"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsStock, ADODB.LockTypeEnum.adLockReadOnly)

        mRecordCount = 0


        If RsStock.EOF = False Then
            Do While Not RsStock.EOF
                mRecordCount = mRecordCount + 1
                mVNoSeq = Val(IIf(IsDBNull(RsStock.Fields("AUTO_KEY_ADJ").Value), 0, RsStock.Fields("AUTO_KEY_ADJ").Value))
                mAdjDate = Trim(IIf(IsDBNull(RsStock.Fields("ADJ_DATE").Value), "", RsStock.Fields("ADJ_DATE").Value))
                mDeptCode = "PRS" '''"STR"         '''"ASY"						
                mEmpCode = Trim(IIf(IsDBNull(RsStock.Fields("EMP_CODE").Value), "", RsStock.Fields("EMP_CODE").Value))
                mItemCode = Trim(IIf(IsDBNull(RsStock.Fields("ITEM_CODE").Value), "", RsStock.Fields("ITEM_CODE").Value))
                mUOM = Trim(IIf(IsDBNull(RsStock.Fields("ITEM_UOM").Value), "", RsStock.Fields("ITEM_UOM").Value))
                mStkType = Trim(IIf(IsDBNull(RsStock.Fields("STOCK_TYPE").Value), "", RsStock.Fields("STOCK_TYPE").Value))
                mAdjQty = Val(IIf(IsDBNull(RsStock.Fields("ADJ_QTY").Value), 0, RsStock.Fields("ADJ_QTY").Value))
                mIO = Trim(IIf(IsDBNull(RsStock.Fields("ITEM_IO").Value), "", RsStock.Fields("ITEM_IO").Value))
                mDivisionCode = Val(IIf(IsDBNull(RsStock.Fields("DIV_CODE").Value), 0, RsStock.Fields("DIV_CODE").Value))

                If UpdateStockTRN(PubDBCn, ConStockRefType_ADJ, Str(mVNoSeq), 1, mAdjDate, mAdjDate, mStkType, mItemCode, mUOM, CStr(-1), System.Math.Abs(mAdjQty), 0, mIO, 0, 0, "", "", mDeptCode, mDeptCode, "", "N", "STOCK ADJUSTMENT AFTER PHYSICAL INVENTORY", "", mStockID, mDivisionCode, "", "") = False Then GoTo ErrPart


                RsStock.MoveNext()
                lblCount.Text = CStr(mRecordCount)
                System.Windows.Forms.Application.DoEvents()
            Loop
        End If


        UpdateTemp = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateTemp = False
        PubDBCn.RollbackTrans() ''						
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Item Consumption Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume						
    End Function
    Private Function UpdateDetail1(ByRef pRefNo As Double, ByRef mAdjDate As String, ByRef pDeptCode As String, ByRef mItemCode As String,
                                   ByRef mUOM As String, ByRef mStkType As String, ByRef mAdjQty As Double, ByRef mIO As String,
                                   ByRef pHDRUPdate As Boolean, ByRef pSNo As Double, ByRef mDivisionCode As Double, ByRef mBatchNo As String, ByRef mHeatNo As String) As Boolean
        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim mRemarks As String

        OpenLocalConnection()
        LocalPubDBCn.Errors.Clear()
        LocalPubDBCn.BeginTrans()

        If pHDRUPdate = False Then
            SqlStr = " Delete From INV_ADJ_DET " & vbCrLf & " WHERE AUTO_KEY_ADJ=" & Val(CStr(pRefNo)) & ""
            LocalPubDBCn.Execute(SqlStr)

            If DeleteStockTRN(LocalPubDBCn, ConStockRefType_ADJ, Str(pRefNo)) = False Then GoTo UpdateDetail1
        End If

        mRemarks = "PHYSICAL INVENTORY"


        If mItemCode <> "" And System.Math.Abs(mAdjQty) > 0 Then

            SqlStr = " INSERT INTO INV_ADJ_DET ( " & vbCrLf _
                & " AUTO_KEY_ADJ,SERIAL_NO,ITEM_CODE,ITEM_UOM,ADJ_QTY," & vbCrLf _
                & " ITEM_IO,STOCK_TYPE,REMARKS,COMPANY_CODE, BATCH_NO, HEAT_NO) "


            SqlStr = SqlStr & vbCrLf _
                & " VALUES (" & Val(pRefNo) & ", " & pSNo & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mUOM) & "', " & vbCrLf _
                & " " & Math.Abs(mAdjQty) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mIO) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mStkType) & "', " & vbCrLf _
                & " '" & mRemarks & "'," & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mBatchNo & "','" & mHeatNo & "') "

            LocalPubDBCn.Execute(SqlStr)

            If UpdateStockTRN(LocalPubDBCn, ConStockRefType_ADJ, Str(pRefNo), 1, mAdjDate, mAdjDate, mStkType, mItemCode, mUOM, mBatchNo, mAdjQty, 0, mIO, 0, 0, "", "", pDeptCode, pDeptCode, "", "N", mRemarks, "", mStockID, mDivisionCode, "", "",, mHeatNo) = False Then GoTo UpdateDetail1

        End If
        UpdateDetail1 = True

        LocalPubDBCn.CommitTrans()
        CloseLocalConnection()

        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        LocalPubDBCn.RollbackTrans()
        CloseLocalConnection()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume						
    End Function
    Public Sub frmStoreAdjustmentProcess_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim SqlStr As String


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Stock Adjustment Process"

        '    txtAdjDate.Enabled = True						

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume						
    End Sub
    Private Sub frmStoreAdjustmentProcess_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub frmStoreAdjustmentProcess_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode						
    End Sub
    Public Sub frmStoreAdjustmentProcess_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Integer

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection						
        'PvtDBCn.Open StrConn						

        MainClass.SetControlsColor(Me)

        Me.Text = "Stock Adjustment Process"
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(6045)
        'Me.Width = VB6.TwipsToPixelsX(4560)

        optWareHouse(0).Checked = True
        mStockID = ConWH
        optUpdate(0).Checked = True

        TxtItemName.Enabled = False
        txtItemCode.Enabled = False
        cmdSearch.Enabled = False
        chkAllItem.Enabled = True
        chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked

        '    txtCategory.Enabled = False						
        '    cmdsearchCategory.Enabled = False						
        '    chkAllCategory.Value = vbChecked						
        '    txtAdjDate.Text = Format(PubCurrDate, "DD/MM/YYYY")						
        Call MainClass.FillCombo(CboSType, "INV_TYPE_MST", "STOCK_TYPE_CODE", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        CboSType.SelectedIndex = 0

        Call MainClass.FillCombo(cboDivision, "INV_DIVISION_MST", "DIV_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        cboDivision.SelectedIndex = 0


        lstMaterialType.Items.Clear()
        SqlStr = "SELECT GEN_DESC FROM INV_GENERAL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GEN_TYPE='C' ORDER BY GEN_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            Do While RS.EOF = False
                lstMaterialType.Items.Add(RS.Fields("GEN_DESC").Value)
                lstMaterialType.SetItemChecked(CntLst, False)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstMaterialType.SelectedIndex = 0

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub optWareHouse_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optWareHouse.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optWareHouse.GetIndex(eventSender)
            If Index = 0 Then
                optWareHouse(0).Checked = True
                mStockID = ConWH
            ElseIf Index = 1 Then
                optWareHouse(1).Checked = True
                mStockID = ConPH
            Else
                optWareHouse(2).Checked = True
                mStockID = ConSH
            End If
        End If
    End Sub


    Private Sub txtADJDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAdjDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If TxtAdjDate.Text = "__/__/____" Then GoTo EventExitSub
        If IsDate(TxtAdjDate.Text) = False Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            TxtAdjDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((TxtAdjDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub TxtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then TxtDept_DoubleClick(txtDept, New System.EventArgs())
    End Sub

    Private Sub TxtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtDept.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Depatment Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtEmpCode.Text = AcName1
            If txtEmpCode.Enabled = True Then txtEmpCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub

        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")

        If MainClass.ValidateWithMasterTable(txtEmpCode.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Emp Code")
            Cancel = True
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.DoubleClick
        SearchItem()
    End Sub
    Private Sub txtItemCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub
    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub
    Private Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If txtItemCode.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            TxtItemName.Text = MasterNo
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
    Private Sub SearchItem()
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(TxtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE",  ,  , SqlStr)
        If AcName <> "" Then
            TxtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
End Class
