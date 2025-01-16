Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTrfOpStock
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCN As Connection					

    Dim mLastFYDateFrom As String
    Dim mLastFYDateTo As String
    Dim mLastFYNo As Integer

    Dim mCurrFYDateFrom As String
    Dim mCurrFYDateTo As String
    Dim mCurrFYNo As Integer

    Private Sub cboStockID_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStockID.TextChanged
        cmdStart.Enabled = True
    End Sub
    Private Sub cboStockID_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStockID.SelectedIndexChanged
        If cboStockID.SelectedIndex = 1 Or cboStockID.SelectedIndex = 3 Then
            cboDept.Enabled = True
        Else
            cboDept.Enabled = False
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
        '    'Set PvtDBCN = Nothing					
    End Sub
    Sub TopDisplayTransfer(ByRef MsgStr As String)
        TxtDisplayTransfer(0).SelectedText = MsgStr & vbCrLf
        TxtDisplayTransfer(0).SelectionLength = Len(MsgStr)
    End Sub
    Sub BottomDisplayTransfer(ByRef MsgStr As String)
        TxtDisplayTransfer(1).SelectedText = MsgStr & vbCrLf
        TxtDisplayTransfer(1).SelectionLength = Len(MsgStr)
    End Sub
    Sub MakeTxtDisplayTransferVisible()
        TxtDisplayTransfer(0).Width = VB6.TwipsToPixelsX(5085)
        TxtDisplayTransfer(0).Height = VB6.TwipsToPixelsY(2775)
        TxtDisplayTransfer(0).Top = VB6.TwipsToPixelsY(3300)
        TxtDisplayTransfer(0).Left = 0

        TxtDisplayTransfer(1).Width = VB6.TwipsToPixelsX(5025)
        TxtDisplayTransfer(1).Height = VB6.TwipsToPixelsY(1725)
        TxtDisplayTransfer(1).Top = VB6.TwipsToPixelsY(4320)
        TxtDisplayTransfer(1).Left = 0 ''30					

        TxtDisplayTransfer(0).Visible = True
        TxtDisplayTransfer(1).Visible = True
        TxtDisplayTransfer(0).Text = ""
        TxtDisplayTransfer(1).Text = ""

        '    TxtDisplayTransfer(0).ForeColor = black					
        '    TxtDisplayTransfer(1).ForeColor = blue					

    End Sub

    Private Sub chkAllCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCategory.CheckStateChanged
        '    Call PrintStatus(False)					
        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCategory.Enabled = False
            cmdsearchCategory.Enabled = False
        Else
            txtCategory.Enabled = True
            cmdsearchCategory.Enabled = True
        End If
    End Sub
    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCategory.Click
        SearchCategory()
    End Sub

    Private Sub txtCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If txtCategory.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtCategory.Text = UCase(Trim(txtCategory.Text))
        Else
            MsgInformation("No Such Category in Master")
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
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.SearchGridMaster(txtCategory.Text, "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtCategory.Text = AcName
            End If
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
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

    Private Sub cmdSearchCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCode.Click
        On Error GoTo SearchErr
        Dim SqlStr As String
        If MainClass.SearchMaster(txtItemCode.Text, "INV_ITEM_MST", "ITEM_CODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtItemCode.Text = AcName
            txtItemCode.Focus()
        End If
        Exit Sub
SearchErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchName.Click
        On Error GoTo SearchErr
        Dim SqlStr As String
        SqlStr = ""
        If MainClass.SearchMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtItemName.Text = AcName
            txtItemName.Focus()
        End If
        Exit Sub
SearchErr:
        MsgBox(Err.Description)
    End Sub
    Private Function FieldVarification() As Boolean
        On Error GoTo FieldErr

        FieldVarification = False

        If Trim(CboFYearFrom.Text) = "" Then
            MsgBox("FYearFrom Not Selected....")
            Exit Function
        End If
        If Trim(CboFYearTo.Text) = "" Then
            MsgBox("FYearTo Not Selected....")
            Exit Function
        End If

        mLastFYNo = CInt(VB.Left(CboFYearFrom.Text, 4))
        mCurrFYNo = CInt(VB.Left(CboFYearTo.Text, 4))


        If mLastFYNo = 2021 Then
            FieldVarification = False
            Exit Function
        End If

        If mLastFYNo + 1 <> mCurrFYNo Then
            MsgBox("Invalid FYearFrom & FYearTo ....")
            Exit Function
        End If
        If OptParticularItem.Checked = True Then
            If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Item Code Does Not Exist In Item Master.")
                Exit Function
            End If
        End If

        If cboStockID.SelectedIndex = -1 Or Trim(cboStockID.Text) = "" Then
            MsgInformation("Please Select Stock ID.")
            Exit Function
        End If

        FieldVarification = True
        Exit Function
FieldErr:
        FieldVarification = False
        MsgBox(Err.Description)
    End Function
    Private Sub cmdStartoLD1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        On Error GoTo ERR1
        Dim mITEM_CODE As String
        Dim SqlStr As String
        Dim RsItem As ADODB.Recordset
        Dim mDeptCode As String
        Dim mInvTable As String
        Dim mAllDept As String

        'Dim mInvTable As String					
        Dim mCatCode As String

        If FieldVarification() = False Then Exit Sub

        If cboStockID.SelectedIndex = 3 Or cboStockID.SelectedIndex = 1 Then
            If cboDept.Text = "ALL" Then
                mAllDept = "Y"
            Else
                mAllDept = "N"
                If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDeptCode = Trim(MasterNo)
                Else
                    MsgInformation("Please Select Valid Dept")
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Arrow ''Screen.MousePointer = 0
                    Exit Sub
                End If
            End If

        End If

        If OptParticularItem.Checked = True Then
            If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mITEM_CODE = MasterNo
            Else
                MsgInformation("Item Code Does Not Exist In Item Master.")
                Exit Sub
            End If
        End If

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
            Else
                mCatCode = "-1"
            End If
        Else
            mCatCode = "-1"
        End If

        mInvTable = "INV_STOCK_REC_TRN" & IIf(RsCompany.Fields("INV_TAB_CC").Value = "Y", VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"), "") & IIf(RsCompany.Fields("INV_TAB_FY").Value = "Y", mCurrFYNo, "")


        If cboStockID.SelectedIndex = 1 Or cboStockID.SelectedIndex = 3 Then
            SqlStr = " DELETE FROM " & mInvTable & " " & vbCrLf _
                & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND FYEAR = " & mCurrFYNo & " " & vbCrLf _
                & " AND REF_TYPE='" & ConStockRefType_OPN & "' "

            If cboStockID.SelectedIndex = 1 Then
                SqlStr = SqlStr & vbCrLf & " AND STOCK_ID='" & ConPH & "'"
                SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE_TO='" & mDeptCode & "'"
            ElseIf cboStockID.SelectedIndex = 3 Then
                SqlStr = SqlStr & vbCrLf & " AND STOCK_ID='" & ConSH & "'"
                '            SqlStr = SqlStr & vbCrLf & " AND ( DEPT_CODE_TO='" & mDeptCode & "' OR DEPT_CODE_FROM='" & mDeptCode & "')"					
            End If

            If OptParticularItem.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mITEM_CODE)) & "'"
            End If

            If optStatus(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND STATUS='C'"
            End If

            If mCatCode <> "-1" Then
                SqlStr = SqlStr & vbCrLf _
                    & " AND ITEM_CODE IN ( " & vbCrLf _
                    & " SELECT ITEM_CODE FROm INV_ITEM_MST" & vbCrLf _
                    & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND CATEGORY_CODE = '" & MainClass.AllowSingleQuote(mCatCode) & "')"
            End If
            PubDBCn.Execute(SqlStr)
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mLastFYNo = CInt(VB.Left(CboFYearFrom.Text, 4))
        mLastFYDateFrom = GetFYStartEndDate(PubDBCn, "START_DATE", CStr(mLastFYNo)) 'Trim(Mid(CboFYearFrom.Text, 8, 12))
        mLastFYDateTo = GetFYStartEndDate(PubDBCn, "END_DATE", CStr(mLastFYNo)) 'Trim(Mid(CboFYearFrom.Text, 21, 30))

        mCurrFYNo = CInt(VB.Left(CboFYearTo.Text, 4))
        mCurrFYDateFrom = GetFYStartEndDate(PubDBCn, "START_DATE", CStr(mCurrFYNo)) ' Trim(Mid(CboFYearTo.Text, 8, 12))
        mCurrFYDateTo = GetFYStartEndDate(PubDBCn, "END_DATE", CStr(mCurrFYNo)) ' Trim(Mid(CboFYearTo.Text, 21, 30))

        MsgBox(mLastFYDateFrom & mLastFYDateTo & mCurrFYDateFrom & mCurrFYDateTo)

        MakeTxtDisplayTransferVisible()
        TopDisplayTransfer(New String("=", 37))
        TopDisplayTransfer("Transferring Stock From Financial Year " & mLastFYDateFrom & " To Financial Year " & mCurrFYDateFrom)
        TopDisplayTransfer("Please Wait........")
        TopDisplayTransfer(New String("=", 37))



        'If cboStockID.SelectedIndex = 1 Or cboStockID.SelectedIndex = 3 Then
        '    SqlStr = "SELECT DISTINCT ITEM_CODE "

        '    mInvTable = "INV_STOCK_REC_TRN" & IIf(RsCompany.Fields("INV_TAB_CC").Value = "Y", VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"), "") & IIf(RsCompany.Fields("INV_TAB_FY").Value = "Y", mLastFYNo, "")

        '    SqlStr = SqlStr & vbCrLf _
        '        & " FROM " & mInvTable & "" & vbCrLf _
        '        & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '        & " AND FYEAR=" & mLastFYNo & ""

        '    If cboStockID.SelectedIndex = 1 Then
        '        SqlStr = SqlStr & vbCrLf & " AND STOCK_ID='" & ConPH & "'"
        '        SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE_TO='" & mDeptCode & "'"
        '    ElseIf cboStockID.SelectedIndex = 3 Then
        '        SqlStr = SqlStr & vbCrLf & " AND STOCK_ID='" & ConSH & "'"
        '    End If

        '    If OptParticularItem.Checked = True Then
        '        SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mITEM_CODE)) & "'"
        '    End If

        '    If optStatus(0).Checked = True Then
        '        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND STATUS='C'"
        '    End If

        '    If mCatCode <> "-1" Then
        '        SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE IN ( " & vbCrLf & " SELECT ITEM_CODE FROm INV_ITEM_MST" & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY_CODE = '" & MainClass.AllowSingleQuote(mCatCode) & "')"
        '    End If
        'Else
        '    SqlStr = " SELECT ITEM_CODE " & vbCrLf _
        '        & " FROM INV_ITEM_MST" & vbCrLf _
        '        & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" ''& vbCrLf |                & " AND  ITEM_STATUS='A'"   ''26-07-2011 ''SANDEEP NOT REQUIRED					

        '    If OptParticularItem.Checked = True Then
        '        SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mITEM_CODE)) & "'"
        '    End If

        '    If mCatCode <> "-1" Then
        '        SqlStr = SqlStr & vbCrLf & " AND CATEGORY_CODE = '" & MainClass.AllowSingleQuote(mCatCode) & "'"
        '    End If

        '    If optExportItem(1).Checked = True Then
        '        SqlStr = SqlStr & vbCrLf & " AND IS_EXPORT_ITEM='Y'"
        '    ElseIf optExportItem(2).Checked = True Then
        '        SqlStr = SqlStr & vbCrLf & " AND IS_EXPORT_ITEM='N'"
        '    End If
        'End If

        'SqlStr = SqlStr & vbCrLf & " ORDER BY ITEM_CODE"

        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItem, ADODB.LockTypeEnum.adLockReadOnly)

        'If Not RsItem.EOF Then
        '    Do While Not RsItem.EOF
        '        mITEM_CODE = Trim(IIf(IsDBNull(RsItem.Fields("ITEM_CODE").Value), "", RsItem.Fields("ITEM_CODE").Value))

        SqlStr = "SELECT INV.ITEM_CODE, INV.ITEM_UOM, INV.STOCK_TYPE, INV.STOCK_ID, INV.DIV_CODE, " & vbCrLf _
            & " CASE WHEN BATCH_NO='-1' OR BATCH_NO='0' THEN '' ELSE BATCH_NO END AS BATCH_NO, HEAT_NO, "

        If cboStockID.SelectedIndex = 0 Then
            SqlStr = SqlStr & "DECODE(INV.STOCK_TYPE,'FG','PAD','STR') AS DEPT_CODE_TO, "
        ElseIf cboStockID.SelectedIndex = 3 Or cboStockID.SelectedIndex = 1 Then
            SqlStr = SqlStr & " '" & mDeptCode & "' AS DEPT_CODE_TO, "
        Else
            SqlStr = SqlStr & " DEPT_CODE_TO, "
        End If


        mInvTable = "INV_STOCK_REC_TRN" & IIf(RsCompany.Fields("INV_TAB_CC").Value = "Y", VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"), "") & IIf(RsCompany.Fields("INV_TAB_FY").Value = "Y", mLastFYNo, "")


        SqlStr = SqlStr & vbCrLf _
            & " SUM(INV.ITEM_QTY * DECODE(INV.ITEM_IO,'I',1,-1)) AS ITEM_QTY, " & vbCrLf _
            & " AVG(INV.PURCHASE_COST) As PURCHASE_COST, " & vbCrLf _
            & " AVG(INV.LANDED_COST) AS LANDED_COST " & vbCrLf _
            & " FROM " & mInvTable & " INV, INV_ITEM_MST IMST" & vbCrLf _
            & " WHERE INV.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INV.COMPANY_CODE=IMST.COMPANY_CODE AND INV.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
            & " AND INV.FYEAR=" & mLastFYNo & " "

        If OptParticularItem.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " And ITEM_CODE = '" & MainClass.AllowSingleQuote(UCase(mITEM_CODE)) & "'"
        End If

        If mCatCode <> "-1" Then
            SqlStr = SqlStr & vbCrLf _
                    & " AND ITEM_CODE IN ( " & vbCrLf _
                    & " SELECT ITEM_CODE FROm INV_ITEM_MST" & vbCrLf _
                    & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND CATEGORY_CODE = '" & MainClass.AllowSingleQuote(mCatCode) & "')"
        End If

        If cboStockID.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConWH & "'"
        ElseIf cboStockID.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConPH & "'"
            SqlStr = SqlStr & vbCrLf & " AND INV.DEPT_CODE_TO='" & mDeptCode & "'"
        ElseIf cboStockID.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConJW & "'"
        ElseIf cboStockID.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConSH & "'"
        End If


        If optStatus(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='C'"
        End If

        If optStatus(0).Checked = True Then
            If cboStockID.SelectedIndex = 0 Then
                SqlStr = SqlStr & vbCrLf & " HAVING SUM(INV.ITEM_QTY* DECODE(INV.ITEM_IO,'I',1,-1))>0 "
            End If
        End If

        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY INV.ITEM_CODE, INV.ITEM_UOM, INV.STOCK_TYPE, INV.STOCK_ID,INV.DIV_CODE, CASE WHEN BATCH_NO='-1' OR BATCH_NO='0' THEN '' ELSE BATCH_NO END, HEAT_NO " ',DECODE(BATCH_NO,NULL,0,DECODE(BATCH_NO,-1,0,BATCH_NO))  ''CASE WHEN IMST.DSP_RPT_FLAG='Y' THEN 'OP' ELSE '-1' END 					

        If cboStockID.SelectedIndex = 2 Then
            SqlStr = SqlStr & ", INV.DEPT_CODE_TO"
        End If

        If TransferStock(SqlStr, mITEM_CODE, mDeptCode, IIf(optStatus(0).Checked = True, "O", "C"), mAllDept) = False Then GoTo ERR1

        '        RsItem.MoveNext()
        '    Loop
        'End If



        TxtDisplayTransfer(1).Text = ""
        TxtDisplayTransfer(0).Text = ""
        TopDisplayTransfer(New String("=", 37))
        TopDisplayTransfer("Transferring Stock From Financial Year " & mLastFYDateFrom & " To Financial Year " & mCurrFYDateFrom)
        TopDisplayTransfer("Stock Transfer Done Successfully.")
        TopDisplayTransfer(New String("=", 37))

        cmdStart.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        If Err.Number = 7 Then
            TxtDisplayTransfer(1).Text = ""
            Resume Next
        End If
        TxtDisplayTransfer(0).Text = ""
        TopDisplayTransfer(New String("=", 37))
        TopDisplayTransfer("Transferring Stock From Financial Year " & mLastFYDateFrom & " To Financial Year " & mCurrFYDateFrom)
        TopDisplayTransfer("Stock Transfer Failed.........")
        TopDisplayTransfer(New String("=", 37))
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Number <> 0 Then
            MsgInformation(Err.Description)
        End If
        'Resume					
    End Sub
    Private Sub cmdStart_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdStart.Click
        On Error GoTo ERR1
        Dim mITEM_CODE As String
        Dim SqlStr As String
        Dim RsItem As ADODB.Recordset
        Dim mDeptCode As String
        Dim mInvTable As String
        'Dim mInvTable As String					
        Dim mCatCode As String
        Dim mAllDept As String

        If FieldVarification() = False Then Exit Sub

        If cboStockID.SelectedIndex = 3 Or cboStockID.SelectedIndex = 1 Then
            If cboDept.Text = "ALL" Then
                mAllDept = "Y"
            Else
                mAllDept = "N"

                If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDeptCode = Trim(MasterNo)
                Else
                    MsgInformation("Please Select Valid Dept")
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Arrow ''Screen.MousePointer = 0
                    Exit Sub
                End If
            End If

        End If

        If OptParticularItem.Checked = True Then
            If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mITEM_CODE = MasterNo
            Else
                MsgInformation("Item Code Does Not Exist In Item Master.")
                Exit Sub
            End If
        End If

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
            Else
                mCatCode = "-1"
            End If
        Else
            mCatCode = "-1"
        End If

        mInvTable = "INV_STOCK_REC_TRN" & IIf(RsCompany.Fields("INV_TAB_CC").Value = "Y", VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"), "") & IIf(RsCompany.Fields("INV_TAB_FY").Value = "Y", mCurrFYNo, "")


        If cboStockID.SelectedIndex = 1 Or cboStockID.SelectedIndex = 3 Then
            SqlStr = " DELETE FROM " & mInvTable & " " & vbCrLf _
                & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND FYEAR = " & mCurrFYNo & " " & vbCrLf _
                & " AND REF_TYPE='" & ConStockRefType_OPN & "' "

            If cboStockID.SelectedIndex = 1 Then
                SqlStr = SqlStr & vbCrLf & " AND STOCK_ID='" & ConPH & "'"
                If mAllDept = "N" Then
                    SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE_TO='" & mDeptCode & "'"
                End If
            ElseIf cboStockID.SelectedIndex = 3 Then
                    SqlStr = SqlStr & vbCrLf & " AND STOCK_ID='" & ConSH & "'"
                '            SqlStr = SqlStr & vbCrLf & " AND ( DEPT_CODE_TO='" & mDeptCode & "' OR DEPT_CODE_FROM='" & mDeptCode & "')"					
            End If

            If OptParticularItem.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mITEM_CODE)) & "'"
            End If

            If optStatus(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND STATUS='C'"
            End If

            If mCatCode <> "-1" Then
                SqlStr = SqlStr & vbCrLf _
                    & " AND ITEM_CODE IN ( " & vbCrLf _
                    & " SELECT ITEM_CODE FROm INV_ITEM_MST" & vbCrLf _
                    & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND CATEGORY_CODE = '" & MainClass.AllowSingleQuote(mCatCode) & "')"
            End If
            PubDBCn.Execute(SqlStr)
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'mLastFYDateFrom = Trim(Mid(CboFYearFrom.Text, 8, 12))
        'mLastFYDateTo = Trim(Mid(CboFYearFrom.Text, 21, 30))
        'mLastFYNo = CInt(VB.Left(CboFYearFrom.Text, 4))

        'mCurrFYDateFrom = Trim(Mid(CboFYearTo.Text, 8, 12))
        'mCurrFYDateTo = Trim(Mid(CboFYearTo.Text, 21, 30))
        'mCurrFYNo = CInt(VB.Left(CboFYearTo.Text, 4))

        mLastFYNo = CInt(VB.Left(CboFYearFrom.Text, 4))
        mLastFYDateFrom = GetFYStartEndDate(PubDBCn, "START_DATE", CStr(mLastFYNo)) 'Trim(Mid(CboFYearFrom.Text, 8, 12))
        mLastFYDateTo = GetFYStartEndDate(PubDBCn, "END_DATE", CStr(mLastFYNo)) 'Trim(Mid(CboFYearFrom.Text, 21, 30))

        mCurrFYNo = CInt(VB.Left(CboFYearTo.Text, 4))
        mCurrFYDateFrom = GetFYStartEndDate(PubDBCn, "START_DATE", CStr(mCurrFYNo)) ' Trim(Mid(CboFYearTo.Text, 8, 12))
        mCurrFYDateTo = GetFYStartEndDate(PubDBCn, "END_DATE", CStr(mCurrFYNo)) ' Trim(Mid(CboFYearTo.Text, 21, 30))

        'MsgBox(mLastFYDateFrom & mLastFYDateTo & mCurrFYDateFrom & mCurrFYDateTo)

        MakeTxtDisplayTransferVisible()
        TopDisplayTransfer(New String("=", 37))
        TopDisplayTransfer("Transferring Stock From Financial Year " & mLastFYDateFrom & " To Financial Year " & mCurrFYDateFrom)
        TopDisplayTransfer("Please Wait........")
        TopDisplayTransfer(New String("=", 37))


        If cboStockID.SelectedIndex = 1 Or cboStockID.SelectedIndex = 3 Then
            SqlStr = "SELECT DISTINCT ITEM_CODE "

            mInvTable = "INV_STOCK_REC_TRN" & IIf(RsCompany.Fields("INV_TAB_CC").Value = "Y", VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"), "") & IIf(RsCompany.Fields("INV_TAB_FY").Value = "Y", mLastFYNo, "")

            SqlStr = SqlStr & vbCrLf _
                & " FROM " & mInvTable & "" & vbCrLf _
                & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND FYEAR=" & mLastFYNo & ""

            If cboStockID.SelectedIndex = 1 Then
                SqlStr = SqlStr & vbCrLf & " AND STOCK_ID='" & ConPH & "'"
                If mAllDept = "N" Then
                    SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE_TO='" & mDeptCode & "'"
                End If
            ElseIf cboStockID.SelectedIndex = 3 Then
                    SqlStr = SqlStr & vbCrLf & " AND STOCK_ID='" & ConSH & "'"
            End If

            If OptParticularItem.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mITEM_CODE)) & "'"
            End If

            If optStatus(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND STATUS='C'"
            End If

            If mCatCode <> "-1" Then
                SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE IN ( " & vbCrLf _
                    & " SELECT ITEM_CODE FROm INV_ITEM_MST" & vbCrLf _
                    & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND CATEGORY_CODE = '" & MainClass.AllowSingleQuote(mCatCode) & "')"
            End If
        Else
            SqlStr = " SELECT ITEM_CODE " & vbCrLf _
                & " FROM INV_ITEM_MST" & vbCrLf _
                & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" ''& vbCrLf |                & " AND  ITEM_STATUS='A'"   ''26-07-2011 ''SANDEEP NOT REQUIRED					

            If OptParticularItem.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mITEM_CODE)) & "'"
            End If

            If mCatCode <> "-1" Then
                SqlStr = SqlStr & vbCrLf & " AND CATEGORY_CODE = '" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If

            If optExportItem(1).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND IS_EXPORT_ITEM='Y'"
            ElseIf optExportItem(2).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND IS_EXPORT_ITEM='N'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY ITEM_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItem, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsItem.EOF Then
            Do While Not RsItem.EOF
                mITEM_CODE = Trim(IIf(IsDBNull(RsItem.Fields("ITEM_CODE").Value), "", RsItem.Fields("ITEM_CODE").Value))

                ''DECODE(BATCH_NO,NULL,0,DECODE(BATCH_NO,-1,0,BATCH_NO))					
                ''CASE WHEN IMST.DSP_RPT_FLAG='Y' THEN DECODE(BATCH_NO,NULL,'-1',DECODE(BATCH_NO,'0','-1',BATCH_NO)) ELSE '-1' END '', IMST.DSP_RPT_FLAG AS BATCH_REQ					
                ''CASE WHEN IMST.DSP_RPT_FLAG='Y' THEN 'OP' ELSE '-1' END AS

                SqlStr = "SELECT INV.ITEM_CODE, INV.ITEM_UOM, INV.STOCK_TYPE, INV.STOCK_ID, INV.DIV_CODE, " & vbCrLf _
                    & " CASE WHEN BATCH_NO='-1' OR BATCH_NO='0' THEN '' ELSE BATCH_NO END AS BATCH_NO, HEAT_NO, IS_CAPITAL, 'N' AS ITEM_TYPE,"

                If cboStockID.SelectedIndex = 0 Then
                    SqlStr = SqlStr & "DECODE(INV.STOCK_TYPE,'FG','PAD','STR') AS DEPT_CODE_TO, "
                ElseIf cboStockID.SelectedIndex = 3 Or cboStockID.SelectedIndex = 1 Then
                    If mAllDept = "N" Then
                        SqlStr = SqlStr & " '" & mDeptCode & "' AS DEPT_CODE_TO, "
                    Else
                        SqlStr = SqlStr & " DECODE(INV.DEPT_CODE_TO,NULL,'PAD',INV.DEPT_CODE_TO)  AS DEPT_CODE_TO,"
                    End If

                Else
                        SqlStr = SqlStr & " DEPT_CODE_TO, "
                End If


                mInvTable = "INV_STOCK_REC_TRN" & IIf(RsCompany.Fields("INV_TAB_CC").Value = "Y", VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"), "") & IIf(RsCompany.Fields("INV_TAB_FY").Value = "Y", mLastFYNo, "")


                SqlStr = SqlStr & vbCrLf _
                    & " SUM(INV.ITEM_QTY * DECODE(INV.ITEM_IO,'I',1,-1)) AS ITEM_QTY, " & vbCrLf _
                    & " AVG(INV.PURCHASE_COST) As PURCHASE_COST, " & vbCrLf _
                    & " AVG(INV.LANDED_COST) AS LANDED_COST " & vbCrLf _
                    & " FROM " & mInvTable & " INV, INV_ITEM_MST IMST" & vbCrLf _
                    & " WHERE INV.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INV.COMPANY_CODE=IMST.COMPANY_CODE AND INV.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
                    & " AND INV.FYEAR=" & mLastFYNo & " AND INV.ITEM_CODE='" & mITEM_CODE & "'"

                If cboStockID.SelectedIndex = 0 Then
                    SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConWH & "'"
                ElseIf cboStockID.SelectedIndex = 1 Then
                    SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConPH & "'"
                    If mAllDept = "N" Then
                        SqlStr = SqlStr & vbCrLf & " AND INV.DEPT_CODE_TO='" & mDeptCode & "'"
                    End If
                ElseIf cboStockID.SelectedIndex = 2 Then
                        SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConJW & "'"
                    ElseIf cboStockID.SelectedIndex = 3 Then
                        SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID='" & ConSH & "'"
                    '                SqlStr = SqlStr & vbCrLf & " AND ( DEPT_CODE_TO='" & mDeptCode & "' OR DEPT_CODE_FROM='" & mDeptCode & "')"					
                End If

                '            SqlStr = SqlStr & vbCrLf & " AND STATUS = 'O'"					
                If optStatus(0).Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='C'"
                End If

                If optStatus(0).Checked = True Then
                    If cboStockID.SelectedIndex = 0 Then
                        SqlStr = SqlStr & vbCrLf & " HAVING SUM(INV.ITEM_QTY* DECODE(INV.ITEM_IO,'I',1,-1))>0 "
                    End If
                End If

                SqlStr = SqlStr & vbCrLf _
                    & " GROUP BY INV.ITEM_CODE, INV.ITEM_UOM, INV.STOCK_TYPE, INV.STOCK_ID,INV.DIV_CODE, IS_CAPITAL, CASE WHEN BATCH_NO='-1' OR BATCH_NO='0' THEN '' ELSE BATCH_NO END, HEAT_NO " ',DECODE(BATCH_NO,NULL,0,DECODE(BATCH_NO,-1,0,BATCH_NO))  ''CASE WHEN IMST.DSP_RPT_FLAG='Y' THEN 'OP' ELSE '-1' END 					

                If cboStockID.SelectedIndex = 3 Or cboStockID.SelectedIndex = 1 Then
                    SqlStr = SqlStr & ",  DECODE(INV.DEPT_CODE_TO,NULL,'PAD',INV.DEPT_CODE_TO)"
                ElseIf cboStockID.SelectedIndex = 2 Then
                    SqlStr = SqlStr & ", INV.DEPT_CODE_TO" ''DECODE(DEPT_CODE_TO,NULL,'STR',DEPT_CODE_TO) "					
                End If

                If TransferStock(SqlStr, mITEM_CODE, mDeptCode, IIf(optStatus(0).Checked = True, "O", "C"), mAllDept) = False Then GoTo ERR1

                'MsgBox("15")
                RsItem.MoveNext()

            Loop
        End If



        TxtDisplayTransfer(1).Text = ""
        TxtDisplayTransfer(0).Text = ""
        TopDisplayTransfer(New String("=", 37))
        TopDisplayTransfer("Transferring Stock From Financial Year " & mLastFYDateFrom & " To Financial Year " & mCurrFYDateFrom)
        TopDisplayTransfer("Stock Transfer Done Successfully.")
        TopDisplayTransfer(New String("=", 37))

        cmdStart.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        'If Err.Number = 7 Then
        '    TxtDisplayTransfer(1).Text = ""
        '    Resume Next
        'End If
        TxtDisplayTransfer(0).Text = ""
        TopDisplayTransfer(New String("=", 37))
        TopDisplayTransfer("Transferring Stock From Financial Year " & mLastFYDateFrom & " To Financial Year " & mCurrFYDateFrom)
        TopDisplayTransfer("Stock Transfer Failed.........")
        TopDisplayTransfer(New String("=", 37))
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Number <> 0 Then
            MsgInformation(Err.Description)
        End If
        'Resume					
    End Sub
    Private Function TransferStock(ByRef RsSqlStr As String, ByRef xITEM_CODE As String, ByRef pDeptCode As String, ByRef pStatus As String, ByRef pAllDept As String) As Boolean
        On Error GoTo UpdateErr
        Dim RSFinalStock As ADODB.Recordset
        Dim i As Long
        Dim SqlStr As String
        Dim pSerailNo As Long
        Dim pRefType As String
        Dim pStockID As String
        Dim pRefNo As Double
        Dim pRefDate As String
        Dim pQCDate As String
        Dim xStockType As String
        Dim pItemCode As String
        Dim pItemUOM As String
        Dim pBatchNo As String
        Dim pItemQty As Double
        Dim pIO As String
        Dim pPurchaseCost As Double
        Dim pLandedCost As Double
        Dim pOperationCode As String
        Dim pNextOperationCode As String
        Dim pDeptCodeTo As String
        Dim pDeptCodeFrom As String
        Dim pCCCode As String
        Dim pDescription As String
        Dim pPartyCode As String
        Dim mCurRowNo As Integer
        Dim nMkey As String
        Dim mError As Boolean
        Dim mInvTable As String
        Dim mDivisionCode As Double
        Dim mNegativeCheck As Boolean
        Dim mBatchReq As String
        Dim mHeatNo As String
        Dim mDate As String

        Dim mCapital As String
        Dim mItemType As String

        TransferStock = False

        'MsgBox("1")
        OpenLocalConnection()

        'MsgBox("2")
        LocalPubDBCn.Errors.Clear()
        LocalPubDBCn.BeginTrans()

        mDate = VB6.Format(mLastFYDateTo, "DD/MM/YYYY")       ''DateAdd("d", -1, mCurrFYDateFrom)
        'MsgBox(mDate)
        '    mInvTable = ConInventoryTable					

        '    If RsCompany.Fields("COMPANY_CODE").Value = 1 Then					
        '        mInvTable = "INV_STOCK_REC_TRN" & mCurrFYNo					
        '    ElseIf RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then					
        '        mInvTable = "INV_STOCK_REC_TRN" & vb6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") & mCurrFYNo					
        '    Else					
        '        mInvTable = "INV_STOCK_REC_TRN"					
        '    End If					

        'If RsCompany.Fields("COMPANY_CODE").Value = 1 Then
        '    mInvTable = "INV_STOCK_REC_TRN" & mCurrFYNo
        'ElseIf RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
        '    mInvTable = "INV_STOCK_REC_TRN" & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") & mCurrFYNo
        'Else
        mInvTable = "INV_STOCK_REC_TRN" ''& IIf(RsCompany.Fields("INV_TAB_CC").Value = "Y", VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"), "") & IIf(RsCompany.Fields("INV_TAB_FY").Value = "Y", mCurrFYNo, "")
        'End If

        mError = False

        'MsgBox("3")
        MainClass.UOpenRecordSet(RsSqlStr, LocalPubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSFinalStock, ADODB.LockTypeEnum.adLockReadOnly)

        'MsgBox("4")
        If cboStockID.SelectedIndex = 1 Or cboStockID.SelectedIndex = 3 Then

        Else
            SqlStr = " DELETE FROM " & mInvTable & " " & vbCrLf _
                & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND FYEAR = " & mCurrFYNo & " " & vbCrLf _
                & " AND REF_TYPE='" & ConStockRefType_OPN & "' " & vbCrLf _
                & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(xITEM_CODE)) & "' "

            If cboStockID.SelectedIndex = 0 Then
                SqlStr = SqlStr & vbCrLf & " AND STOCK_ID='" & ConWH & "'"
            ElseIf cboStockID.SelectedIndex = 1 Then
                SqlStr = SqlStr & vbCrLf & " AND STOCK_ID='" & ConPH & "'"
                If pAllDept = "N" Then
                    SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE_TO='" & pDeptCode & "'"
                End If
            ElseIf cboStockID.SelectedIndex = 2 Then
                    SqlStr = SqlStr & vbCrLf & " AND STOCK_ID='" & ConJW & "'"
                ElseIf cboStockID.SelectedIndex = 3 Then
                    SqlStr = SqlStr & vbCrLf & " AND STOCK_ID='" & ConSH & "'"
                '            SqlStr = SqlStr & vbCrLf & " AND ( DEPT_CODE_TO='" & pDeptCode & "' OR DEPT_CODE_FROM='" & pDeptCode & "')"					
            End If

            '        If optStatus(0).Value = True Then					
            '            SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"					
            '        Else					
            SqlStr = SqlStr & vbCrLf & " AND STATUS='" & pStatus & "'"
            '        End If					

            'MsgBox("5")
            LocalPubDBCn.Execute(SqlStr)
        End If

        'MsgBox("6")
        If Not RSFinalStock.EOF Then
            RSFinalStock.MoveFirst()
            pItemCode = Trim(IIf(IsDBNull(RSFinalStock.Fields("ITEM_CODE").Value), "", RSFinalStock.Fields("ITEM_CODE").Value))


            BottomDisplayTransfer("ITEM_CODE..." & pItemCode)

            mCurRowNo = MainClass.AutoGenRowNo("INV_OPN_BAL", "RowNo", PubDBCn)
            'MsgBox(mCurRowNo)
            nMkey = mCurRowNo & VB6.Format(mCurrFYNo, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
            'MsgBox(nMkey)
            Do While Not RSFinalStock.EOF = True
                i = i + 1

                pSerailNo = i
                pRefType = ConStockRefType_OPN
                pStockID = IIf(IsDBNull(RSFinalStock.Fields("STOCK_ID").Value), "WH", RSFinalStock.Fields("STOCK_ID").Value)

                pRefNo = Val(nMkey)
                'MsgBox(nMkey)
                pRefDate = mDate  ''CStr(System.DateTime.FromOADate(CDate(mCurrFYDateFrom).ToOADate - 1))
                pQCDate = mDate ''CStr(System.DateTime.FromOADate(CDate(mCurrFYDateFrom).ToOADate - 1))
                'MsgBox(pQCDate)
                xStockType = IIf(IsDBNull(RSFinalStock.Fields("STOCK_TYPE").Value), "", RSFinalStock.Fields("STOCK_TYPE").Value)

                'MsgBox(xStockType)
                pItemCode = IIf(IsDBNull(RSFinalStock.Fields("ITEM_CODE").Value), "", RSFinalStock.Fields("ITEM_CODE").Value)
                pItemUOM = IIf(IsDBNull(RSFinalStock.Fields("ITEM_UOM").Value), "", RSFinalStock.Fields("ITEM_UOM").Value)
                pBatchNo = IIf(IsDBNull(RSFinalStock.Fields("BATCH_NO").Value), "", RSFinalStock.Fields("BATCH_NO").Value)
                pBatchNo = IIf(pBatchNo = "-1" Or pBatchNo = "0", "", pBatchNo)
                pItemQty = IIf(IsDBNull(RSFinalStock.Fields("ITEM_QTY").Value), 0, RSFinalStock.Fields("ITEM_QTY").Value)
                pIO = "I"
                pPurchaseCost = 0 ''VB.Format(IIf(IsDBNull(RSFinalStock.Fields("PURCHASE_COST").Value), 0, RSFinalStock.Fields("PURCHASE_COST").Value), "0.00")
                pLandedCost = 0 ''VB.Format(IIf(IsDBNull(RSFinalStock.Fields("LANDED_COST").Value), 0, RSFinalStock.Fields("LANDED_COST").Value), "0.00")        ''      pOperationCode = ""
                pNextOperationCode = ""
                pDeptCodeTo = IIf(IsDBNull(RSFinalStock.Fields("DEPT_CODE_TO").Value), "", RSFinalStock.Fields("DEPT_CODE_TO").Value)
                pDeptCodeFrom = IIf(IsDBNull(RSFinalStock.Fields("DEPT_CODE_TO").Value), "", RSFinalStock.Fields("DEPT_CODE_TO").Value) ''IIf(IsNull(RSFinalStock!DEPT_CODE_TO), "", RSFinalStock!DEPT_CODE_TO)					
                pCCCode = ""
                pDescription = "Opening"
                pPartyCode = "-1"
                mDivisionCode = IIf(IsDBNull(RSFinalStock.Fields("DIV_CODE").Value), -1, RSFinalStock.Fields("DIV_CODE").Value)
                mHeatNo = IIf(IsDBNull(RSFinalStock.Fields("HEAT_NO").Value), "", RSFinalStock.Fields("HEAT_NO").Value)

                mCapital = IIf(IsDBNull(RSFinalStock.Fields("IS_CAPITAL").Value), "N", RSFinalStock.Fields("IS_CAPITAL").Value)
                mItemType = IIf(IsDBNull(RSFinalStock.Fields("ITEM_TYPE").Value), "N", RSFinalStock.Fields("ITEM_TYPE").Value)



                'MsgBox(mHeatNo)

                If pItemCode <> "-1" Then
                    'MsgBox("7")
                    SqlStr = ""
                    SqlStr = "INSERT INTO " & mInvTable & " ( " & vbCrLf _
                        & " COMPANY_CODE, FYEAR, " & vbCrLf _
                        & " SERIAL_NO, REF_TYPE, " & vbCrLf _
                        & " STOCK_ID, REF_NO, " & vbCrLf _
                        & " REF_DATE, E_DATE, " & vbCrLf _
                        & " STOCK_TYPE, ITEM_CODE, " & vbCrLf _
                        & " ITEM_UOM, BATCH_NO, " & vbCrLf _
                        & " ITEM_QTY, " & vbCrLf _
                        & " ITEM_IO, PURCHASE_COST, " & vbCrLf _
                        & " LANDED_COST, OPR_CODE," & vbCrLf _
                        & " NEXT_OPR_CODE, DEPT_CODE_TO, " & vbCrLf _
                        & " DEPT_CODE_FROM, CC_CODE, " & vbCrLf _
                        & " REMARKS, PARTYCODE,STATUS,DIV_CODE,HEAT_NO,IS_CAPITAL, ITEM_TYPE )"

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & mCurrFYNo & ", " & vbCrLf _
                        & " " & pSerailNo & ",'" & pRefType & "', " & vbCrLf & " '" & pStockID & "', " & Val(CStr(pRefNo)) & ", " & vbCrLf _
                        & " TO_DATE('" & VB6.Format(pRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(pQCDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(xStockType) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(pItemCode) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(pItemUOM) & "', '" & Trim(pBatchNo) & "', " & vbCrLf _
                        & " " & Val(CStr(pItemQty)) & ",'" & pIO & "', " & vbCrLf _
                        & " " & Val(CStr(pPurchaseCost)) & "," & Val(CStr(pLandedCost)) & "," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(pOperationCode) & "', '" & MainClass.AllowSingleQuote(pNextOperationCode) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(pDeptCodeTo) & "', '" & MainClass.AllowSingleQuote(pDeptCodeFrom) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(pCCCode) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(pDescription) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(pPartyCode) & "','" & pStatus & "'," & mDivisionCode & "," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mHeatNo) & "','" & MainClass.AllowSingleQuote(mCapital) & "','" & MainClass.AllowSingleQuote(mItemType) & "')"

                    'MsgBox(SqlStr)
                    LocalPubDBCn.Execute(SqlStr)
                    'MsgBox("OK")
                End If
NextRecd:
                RSFinalStock.MoveNext()
                'MsgBox("9")
            Loop
            'MsgBox("10")
        End If
NextItem:
        If mError = True Then
            MsgBox("Item Code..." & pItemCode & " Transfer Failded...")
            mError = False
        End If
        'MsgBox("11")
        LocalPubDBCn.CommitTrans()
        'MsgBox("12")
        CloseLocalConnection()
        'MsgBox("13")
        TransferStock = True
        'MsgBox("14")
        Exit Function
UpdateErr:
        BottomDisplayTransfer("Item Code..." & pItemCode & " Transfer Failded..." & Err.Description)
        'mError = True
        'GoTo NextItem
        LocalPubDBCn.RollbackTrans()
        CloseLocalConnection()
        TransferStock = False
        'If Err.Number <> 0 Then
        '    MsgInformation(Err.Description)
        'End If
        ' Resume					
    End Function
    Private Function TransferStockOld(ByRef RsSqlStr As String) As Boolean
        On Error GoTo UpdateErr
        Dim RSFinalStock As ADODB.Recordset
        Dim i As Integer
        Dim mITEM_CODE As String
        Dim mLotNo As String
        Dim mStockID As String
        Dim mStockFlag As String
        Dim mMKey As String
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mRefNo As String
        Dim mIO As String
        Dim mVDate As String
        Dim mDescription As String

        Dim mTotWidth As Double
        Dim mTotLength As Double
        Dim mTotQty As Double
        Dim mTotPack As Double
        Dim mTotCost As Double

        Dim ProcStat As Short
        Dim SqlStr As String

        TransferStockOld = False
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mBookType = "O"
        mBookSubType = "S"
        mRefNo = "OPS"
        mIO = "I"
        mVDate = CStr(System.DateTime.FromOADate(CDate(mCurrFYDateFrom).ToOADate - 1))
        mDescription = "Opening Balance"

        MainClass.UOpenRecordSet(RsSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSFinalStock, ADODB.LockTypeEnum.adLockReadOnly)
        If RSFinalStock.RecordCount > 0 Then
            Do While Not RSFinalStock.EOF = True
                i = i + 1
                mITEM_CODE = IIf(IsDBNull(RSFinalStock.Fields("ITEM_CODE").Value), "-1", RSFinalStock.Fields("ITEM_CODE").Value)
                mLotNo = IIf(IsDBNull(RSFinalStock.Fields("LotNo").Value), "-1", RSFinalStock.Fields("LotNo").Value)
                mTotWidth = IIf(IsDBNull(RSFinalStock.Fields("TotWidth").Value), 0, RSFinalStock.Fields("TotWidth").Value)
                mTotLength = IIf(IsDBNull(RSFinalStock.Fields("TotLength").Value), 0, RSFinalStock.Fields("TotLength").Value)
                mTotQty = IIf(IsDBNull(RSFinalStock.Fields("TOTQTY").Value), 0, RSFinalStock.Fields("TOTQTY").Value)
                mTotPack = IIf(IsDBNull(RSFinalStock.Fields("TotPack").Value), 0, RSFinalStock.Fields("TotPack").Value)
                mTotCost = IIf(IsDBNull(RSFinalStock.Fields("TotCost").Value), 0, RSFinalStock.Fields("TotCost").Value)
                mStockID = IIf(IsDBNull(RSFinalStock.Fields("StockID").Value), "-1", RSFinalStock.Fields("StockID").Value)
                mStockFlag = IIf(IsDBNull(RSFinalStock.Fields("STOCKFLAG").Value), "-1", RSFinalStock.Fields("STOCKFLAG").Value)

                BottomDisplayTransfer("ITEM_CODE..." & mITEM_CODE & " : " & "LotNo..." & mLotNo)
                If mITEM_CODE <> "-1" And mLotNo <> "-1" And mStockID <> "-1" And mStockFlag <> "-1" Then
                    mMKey = RsCompany.Fields("CompanyCode").Value & RsCompany.Fields("BranchCode").Value & mCurrFYNo & mITEM_CODE
                    SqlStr = " DELETE FROM Stock  " & " WHERE CompanyCode=" & RsCompany.Fields("CompanyCode").Value & " " & " AND BranchCode=" & RsCompany.Fields("BranchCode").Value & " " & " AND FYNo =" & mCurrFYNo & " " & " AND BookType='" & mBookType & "' " & " AND BookSubType='" & mBookSubType & "' " & " AND UPPER(LTRIM(RTRIM(ITEM_CODE)))='" & MainClass.AllowSingleQuote(UCase(mITEM_CODE)) & "' " & " AND UPPER(LTRIM(RTRIM(LotNo)))='" & MainClass.AllowSingleQuote(UCase(mLotNo)) & "' " & " AND UPPER(LTRIM(RTRIM(StockID)))='" & MainClass.AllowSingleQuote(UCase(mStockID)) & "' " & " AND UPPER(LTRIM(RTRIM(StockFlag)))='" & MainClass.AllowSingleQuote(UCase(mStockFlag)) & "' "
                    PubDBCn.Execute(SqlStr)

                    SqlStr = ""
                    SqlStr = "INSERT INTO Stock (MKey,CompanyCode,BranchCode, " & vbCrLf _
                        & " FyNo,BookType,BookSubtype, " & vbCrLf _
                        & " SubRowNo,RefNo,Vdate,ITEM_CODE,LotNo,Description, " & vbCrLf _
                        & " InWidth,OutWidth,InLength,OutLength,InPack,OutPack,InQty, " & vbCrLf _
                        & " OutQty,InCost,OutCost,IO,Stockflag,STOCKID, " & vbCrLf _
                        & " PARTYCODE,TRNBRANCHCODE,BarPrintQty) " & vbCrLf _
                        & " Values ( " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mMKey) & "'," & RsCompany.Fields("CompanyCode").Value & ", " & vbCrLf _
                        & " " & RsCompany.Fields("BranchCode").Value & "," & vbCrLf _
                        & " " & mCurrFYNo & ",'" & mBookType & "','" & mBookSubType & "', " & vbCrLf _
                        & " " & i & ",'" & mRefNo & "',TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mITEM_CODE) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mLotNo) & "','" & mDescription & "', " & vbCrLf _
                        & " " & mTotWidth & ",0," & mTotLength & ",0," & vbCrLf _
                        & " " & mTotPack & ",0," & mTotQty & ",0," & mTotCost & ", " & vbCrLf _
                        & " 0,'" & mIO & "','" & mStockFlag & "','" & mStockID & "','-1','-1',0)"

                    PubDBCn.Execute(SqlStr)
                End If
                RSFinalStock.MoveNext()
            Loop
        End If
        PubDBCn.CommitTrans()
        TransferStockOld = True
        Exit Function
UpdateErr:
        If Err.Number = 7 Then
            TxtDisplayTransfer(1).Text = ""
            Resume Next
        End If
        BottomDisplayTransfer("ProductCode..." & mITEM_CODE & " From LotNo..." & mLotNo & " Transfer Failded...")
        PubDBCn.RollbackTrans()
        TransferStockOld = False
        If Err.Number <> 0 Then
            MsgInformation(Err.Description)
        End If
        ' Resume					
    End Function
    Private Sub frmTrfOpStock_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ERR1
        Dim XRIGHT As String

        'Set PvtDBCN = New Connection					
        '    'PvtDBCN.CommandTimeout = 0					
        '    'PvtDBCN.ConnectionTimeout = 0					
        'PvtDBCN.Open StrConn					

        'TxtDisplayTransfer(0).Visible = False					
        'TxtDisplayTransfer(1).Visible = False					

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        OptAllItem.Checked = True
        Me.Height = VB6.TwipsToPixelsY(7170)
        Me.Width = VB6.TwipsToPixelsX(5220)
        Me.Left = VB6.TwipsToPixelsX(20)
        Me.Top = VB6.TwipsToPixelsY(20)
        txtCategory.Enabled = False
        cmdsearchCategory.Enabled = False
        chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        Call FillFYear()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume					
    End Sub
    Private Sub FillFYear()
        Dim SqlStr As String
        Dim mRsFYear As ADODB.Recordset
        CboFYearFrom.Items.Clear()
        CboFYearTo.Items.Clear()

        SqlStr = "SELECT FYEAR,START_DATE,END_DATE FROM GEN_CMPYRDTL_TRN  " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "  ORDER BY FYEAR"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsFYear, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsFYear.EOF = False Then
            Do While Not mRsFYear.EOF
                CboFYearFrom.Items.Add(VB6.Format(mRsFYear.Fields("FYEAR").Value, "00") & "  :  " & VB6.Format(mRsFYear.Fields("Start_Date").Value, "DD-MM-YYYY") & "_" & VB6.Format(mRsFYear.Fields("END_DATE").Value, "DD-MM-YYYY"))
                CboFYearTo.Items.Add(VB6.Format(mRsFYear.Fields("FYEAR").Value, "00") & "  :  " & VB6.Format(mRsFYear.Fields("Start_Date").Value, "DD-MM-YYYY") & "_" & VB6.Format(mRsFYear.Fields("END_DATE").Value, "DD-MM-YYYY"))
                mRsFYear.MoveNext()
            Loop
        End If

        cboDept.Items.Clear()
        cboDept.Items.Clear()
        SqlStr = "SELECT DEPT_DESC FROM PAY_DEPT_MST  " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsFYear, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsFYear.EOF = False Then
            cboDept.Items.Add("ALL")
            Do While Not mRsFYear.EOF
                cboDept.Items.Add(IIf(IsDBNull(mRsFYear.Fields("DEPT_DESC").Value), "", mRsFYear.Fields("DEPT_DESC").Value))
                mRsFYear.MoveNext()
            Loop
        End If

        cboStockID.Items.Clear()
        cboStockID.Items.Add("STORE")
        cboStockID.Items.Add("PRODUCTION")
        cboStockID.Items.Add("JOBWORK")
        cboStockID.Items.Add("SUB-STORE")
        '    cboStockID.AddItem "ALL"					
        cboStockID.SelectedIndex = 0
    End Sub

    Private Sub OptAllItem_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptAllItem.CheckedChanged
        If eventSender.Checked Then
            txtItemCode.Enabled = False
            cmdSearchCode.Enabled = False
            txtItemName.Enabled = False
            cmdSearchName.Enabled = False
        End If
    End Sub
    Private Sub OptParticularItem_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptParticularItem.CheckedChanged
        If eventSender.Checked Then
            txtItemCode.Enabled = True
            cmdSearchCode.Enabled = True
            txtItemName.Enabled = True
            cmdSearchName.Enabled = True
            cmdStart.Enabled = True
        End If
    End Sub

    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged
        cmdStart.Enabled = True
    End Sub

    Private Sub txtItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.DoubleClick
        Call cmdSearchCode_Click(cmdSearchCode, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchCode_Click(cmdSearchCode, New System.EventArgs())
    End Sub

    Private Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo SearchErr
        Dim RsItem As ADODB.Recordset
        Dim SqlStr As String
        If Trim(txtItemCode.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT ITEM_SHORT_DESC FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(txtItemCode.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItem, ADODB.LockTypeEnum.adLockReadOnly)

        If RsItem.EOF = False Then
            txtItemName.Text = RsItem.Fields("ITEM_SHORT_DESC").Value
        Else
            MsgBox("Item Code Does Not Exist In Master", MsgBoxStyle.Information)
            txtItemName.Text = ""
            Cancel = True
        End If
        GoTo EventExitSub
SearchErr:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.DoubleClick
        Call cmdSearchName_Click(cmdSearchName, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchName_Click(cmdSearchName, New System.EventArgs())
    End Sub

    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo SearchErr
        Dim RsItem As ADODB.Recordset
        Dim SqlStr As String
        If Trim(txtItemName.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT ITEM_CODE FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(UCase(txtItemName.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItem, ADODB.LockTypeEnum.adLockReadOnly)

        If RsItem.EOF = False Then
            txtItemCode.Text = RsItem.Fields("ITEM_CODE").Value
        Else
            MsgBox("Item Name Not Exist In Master", MsgBoxStyle.Information)
            txtItemCode.Text = ""
            Cancel = True
        End If
        GoTo EventExitSub
SearchErr:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
