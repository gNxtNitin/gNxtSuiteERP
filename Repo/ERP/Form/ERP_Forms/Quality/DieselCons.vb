Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmDieselCons
    Inherits System.Windows.Forms.Form
    Dim RsPower As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtNumber.Enabled = False
            cmdSearchNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsPower.EOF = False Then RsPower.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtNumber.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsPower.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "MAN_DIESELCOSUMP_TRN", (txtNumber.Text), RsPower) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_SUBISS, CStr(Val(txtIssueNo.Text))) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM INV_SUB_ISSUE_DET WHERE AUTO_KEY_ISS=" & Val(txtIssueNo.Text) & "")
                PubDBCn.Execute("DELETE FROM INV_SUB_ISSUE_HDR WHERE AUTO_KEY_ISS=" & Val(txtIssueNo.Text) & "")
                PubDBCn.Execute("DELETE FROM MAN_DIESELCOSUMP_TRN WHERE AUTO_KEY_NO=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsPower.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        '    PubDBCn.Errors.Clear
        RsPower.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPower, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtNumber.Enabled = False
            cmdSearchNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            GoTo ErrorHandler
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSlipNo As Double
        Dim mIssueNoteNo As Double
        Dim mDivisionCode As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""

        If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Val(MasterNo)
        Else
            mDivisionCode = -1
            MsgBox("Division Does Not Exist In Master", vbInformation)
            GoTo ErrPart
        End If

        mSlipNo = Val(txtNumber.Text)
        If Val(txtNumber.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtNumber.Text = CStr(mSlipNo)
        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO MAN_DIESELCOSUMP_TRN " & vbCrLf _
                            & " (AUTO_KEY_NO,COMPANY_CODE,FYEAR," & vbCrLf _
                            & " GEN_TYPE,DOC_DATE,TOT_UNIT,UNIT_RATE,TOT_UNIT_COST," & vbCrLf _
                            & " WORK_HOUR, REMARKS,SIGN_EMP_CODE,DEPT_CODE, ITEM_CODE, " & vbCrLf _
                            & " DIESEL_CONS, DIESEL_RATE, DIV_CODE, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                            & " '" & VB.Left(cboType.Text, 1) & "', " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(txtTotUnit.Text) & ", " & vbCrLf _
                            & " " & Val(txtRate.Text) & "," & vbCrLf _
                            & " " & Val(lblTotalCost.Text) & ", " & vbCrLf _
                            & " " & Val(txtHour.Text) & ", '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "','" & MainClass.AllowSingleQuote(txtDept.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'," & vbCrLf _
                            & " " & Val(txtDieselConsumed.Text) & ", " & Val(txtDieselRate.Text) & ", " & mDivisionCode & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE MAN_DIESELCOSUMP_TRN SET " & vbCrLf _
                    & " AUTO_KEY_NO=" & mSlipNo & "," & vbCrLf _
                    & " DOC_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " TOT_UNIT=" & Val(txtTotUnit.Text) & "," & vbCrLf _
                    & " UNIT_RATE=" & Val(txtRate.Text) & "," & vbCrLf _
                    & " WORK_HOUR=" & Val(txtHour.Text) & "," & vbCrLf _
                    & " TOT_UNIT_COST=" & Val(lblTotalCost.Text) & ", " & vbCrLf _
                    & " DIESEL_CONS=" & Val(txtDieselConsumed.Text) & ", " & vbCrLf _
                    & " DIESEL_RATE=" & Val(txtDieselRate.Text) & ", " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " DIV_CODE=" & mDivisionCode & "," & vbCrLf _
                    & " SIGN_EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', " & vbCrLf _
                    & " DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                    & " ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf _
                    & " GEN_TYPE='" & VB.Left(cboType.Text, 1) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE AUTO_KEY_NO =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)


        If UpdateIssueNoteMain(mIssueNoteNo, mDivisionCode, IIf(MODIFYMode = True, False, True)) = False Then GoTo ErrPart

        SqlStr = " UPDATE MAN_DIESELCOSUMP_TRN SET " & vbCrLf & " AUTO_KEY_ISSUE=" & mIssueNoteNo & "" & vbCrLf & " WHERE AUTO_KEY_NO =" & Val(lblMkey.Text) & ""

        PubDBCn.Execute(SqlStr)

        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsPower.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function UpdateIssueNoteMain(ByRef pIssueNoteNoStr As Double, ByRef mDivisionCode As Double, ByRef xAddMode As Boolean) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mIssueSeq As Double
        Dim mStatus As String
        Dim mEntryDate As String
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mStockType As String
        Dim mQCEmpCode As String
        Dim mPurchaseQty As Double
        Dim mIssueQty As Double
        Dim mPurchaseUOM As String
        Dim mUOM As String
        Dim mDeptCode As String
        Dim mFactor As Double
        Dim mLotNoRequied As String
        Dim mDeptDesc As String
        Dim mCostC As String
        Dim mPONo As String
        Dim mProd_Type As String
        Dim mIssueFor As String
        Dim mCommonDivision As Double
        Dim mCommonStockQty As Double
        Dim mStockQty As Double

        mItemCode = Trim(txtItemCode.Text)
        mStockType = "ST"
        mDeptCode = Trim(txtDept.Text)
        mQCEmpCode = Trim(txtEmpCode.Text)

        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND ITEM_CLASSIFICATION='3'") = True Then
            mUOM = MasterNo
        End If

        mIssueQty = Val(txtDieselConsumed.Text)
        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime
        mCostC = GetCostC(mDeptCode)
        mIssueFor = "G"

        If xAddMode = True Then
            mIssueSeq = AutoGenIssueSeqNo()
        Else
            mIssueSeq = Val(txtIssueNo.Text)
            If mIssueSeq = 0 Then
                UpdateIssueNoteMain = True
                Exit Function
            End If
        End If

        If xAddMode = True Then
            SqlStr = "INSERT INTO INV_SUB_ISSUE_HDR (" & vbCrLf _
                & " AUTO_KEY_ISS, " & vbCrLf _
                & " COMPANY_CODE, " & vbCrLf _
                & " ISSUE_DATE, " & vbCrLf _
                & " DEPT_CODE, " & vbCrLf _
                & " EMP_CODE, REMARKS, COST_CENTER_CODE,  " & vbCrLf _
                & " ISSUE_STATUS, SUB_STORE_DEPT, " & vbCrLf _
                & " ADDUSER,ADDDATE,MODUSER,MODDATE,DIV_CODE)" & vbCrLf _
                & " VALUES( "

            SqlStr = SqlStr & vbCrLf _
                            & " " & Val(mIssueSeq) & "," & vbCrLf _
                            & " " & RsCompany.Fields("Company_Code").Value & "," & vbCrLf _
                            & " TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mDeptCode) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mQCEmpCode) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                            & " '" & mCostC & "', " & vbCrLf _
                            & " 'Y', '" & MainClass.AllowSingleQuote(mDeptCode) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                            & " TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'',''," & mDivisionCode & ")"

            PubDBCn.Execute(SqlStr)
        Else
            SqlStr = "UPDATE INV_SUB_ISSUE_HDR SET ISSUE_DATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "', " & vbCrLf _
                            & " EMP_CODE ='" & MainClass.AllowSingleQuote(mQCEmpCode) & "', " & vbCrLf _
                            & " REMARKS ='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf _
                            & " COST_CENTER_CODE ='" & mCostC & "'," & vbCrLf _
                            & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                            & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),DIV_CODE=" & mDivisionCode & " " & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                            & " AND AUTO_KEY_ISS =" & Val(mIssueSeq) & ""
            PubDBCn.Execute(SqlStr)
        End If

        SqlStr = "DELETE FROM INV_SUB_ISSUE_DET WHERE AUTO_KEY_ISS=" & Val(CStr(mIssueSeq)) & ""
        PubDBCn.Execute(SqlStr)

        If DeleteStockTRN(PubDBCn, ConStockRefType_SUBISS, CStr(Val(CStr(mIssueSeq)))) = False Then GoTo ErrPart

        SqlStr = " INSERT INTO INV_SUB_ISSUE_DET ( " & vbCrLf _
            & " AUTO_KEY_ISS,SERIAL_NO,ITEM_CODE,ITEM_UOM,REMARKS," & vbCrLf _
            & " STOCK_TYPE,DEMAND_QTY,ISSUE_QTY, COMPANY_CODE,DEPT_CODE,COST_CENTER_CODE,ISSUE_STATUS,RATE) "


        SqlStr = SqlStr & vbCrLf _
                    & " VALUES (" & Val(mIssueSeq) & ",1," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mUOM) & "', " & vbCrLf _
                    & " '', '" & MainClass.AllowSingleQuote(mStockType) & "', " & vbCrLf _
                    & " " & mIssueQty & "," & mIssueQty & ", " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(mDeptCode) & "','" & mCostC & "','Y'," & Val(txtRate.Text) & ") "

        PubDBCn.Execute(SqlStr)

        mStockQty = GetBalanceStockQty(Trim(mItemCode), (txtDate.Text), mUOM, "STR", "ST", "", ConSH, mDivisionCode, ConStockRefType_SUBISS, Val(txtIssueNo.Text))

        'If mIssueQty < mStockQty Then
        If UpdateStockTRN(PubDBCn, ConStockRefType_SUBISS, Str(mIssueSeq), 1, VB6.Format(txtDate.Text, "DD/MM/YYYY"), VB6.Format(txtDate.Text, "DD/MM/YYYY"), mStockType, mItemCode, mUOM, CStr(-1), mIssueQty, 0, "O", 0, 0, "", "", "STR", mDeptCode, "", "N", "To : " & mDeptDesc, "-1", ConSH, mDivisionCode, "", "") = False Then GoTo ErrPart

            'Else

            '    If UpdateStockTRN(PubDBCn, ConStockRefType_SUBISS, Str(mIssueSeq), 1, VB6.Format(txtDate.Text, "DD/MM/YYYY"), VB6.Format(txtDate.Text, "DD/MM/YYYY"), mStockType, mItemCode, mUOM, CStr(-1), mStockQty, 0, "O", 0, 0, "", "", "STR", mDeptCode, "", "N", "To : " & mDeptDesc, "-1", ConSH, mDivisionCode, "", "") = False Then GoTo ErrPart

            '    mCommonDivision = GetCommonDivCode
            '    If mCommonDivision > 0 Then
            '        mCommonStockQty = GetBalanceStockQty(Trim(mItemCode), (txtDate.Text), mUOM, "STR", "ST", "", ConSH, mCommonDivision, ConStockRefType_SUBISS, Val(txtIssueNo.Text))
            '    End If


            '    If UpdateStockTRN(PubDBCn, ConStockRefType_SUBISS, Str(mIssueSeq), 2, VB6.Format(txtDate.Text, "DD/MM/YYYY"), VB6.Format(txtDate.Text, "DD/MM/YYYY"), mStockType, mItemCode, mUOM, CStr(-1), mIssueQty - mStockQty, 0, "O", 0, 0, "", "", "STR", mDeptCode, "", "N", "To : " & mDeptDesc, "-1", ConSH, mCommonDivision, "", "") = False Then GoTo ErrPart

            'End If

            pIssueNoteNoStr = mIssueSeq

        UpdateIssueNoteMain = True
        Exit Function
ErrPart:
        UpdateIssueNoteMain = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function AutoGenIssueSeqNo() As Double

        On Error GoTo AutoGenIssueSeqNoErr
        Dim RsMainGen As ADODB.Recordset
        Dim mNewSeqNo As Integer
        Dim SqlStr As String
        Dim mMaxValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_ISS)  " & vbCrLf _
            & " FROM INV_SUB_ISSUE_DET " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_ISS,LENGTH(AUTO_KEY_ISS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMainGen
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
        AutoGenIssueSeqNo = CDbl(mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        Exit Function
AutoGenIssueSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function



    Private Function GetCostC(ByRef pDeptCode As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        GetCostC = "001"

        SqlStr = " SELECT IH.CC_CODE " & vbCrLf _
                    & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                    & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf _
                    & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetCostC = IIf(IsDbNull(RsTemp.Fields("CC_CODE").Value), "001", RsTemp.Fields("CC_CODE").Value)
        End If
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_NO)  " & vbCrLf & " FROM MAN_DIESELCOSUMP_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = CDbl(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With
        AutoGenKeyNo = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Sub SearchEmp(ByRef pTextBax As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label)
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" ''  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "ATH_PASSWORD_MST", "EMP_NAME", "USER_ID", , , SqlStr) = True Then
            pTextBax.Text = AcName1
            pLable.Text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            lblDept.text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchEmpCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEmpCode.Click
        Call SearchEmp(txtEmpCode, lblEmpCode)
    End Sub


    Private Sub cmdSearchNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtNumber.Text, "MAN_DIESELCOSUMP_TRN", "AUTO_KEY_NO", "DEPT_CODE", , , SqlStr) = True Then
            txtNumber.Text = AcName
            Call txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            Call AssignGrid(True)
            '        ADataGrid.Refresh
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsPower, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmDieselCons_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Dept Wise Diesel Consumption"

        SqlStr = "Select * From MAN_DIESELCOSUMP_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPower, ADODB.LockTypeEnum.adLockReadOnly)


        Call AssignGrid(False)
        Call SetTextLengths()
        Call Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_NO AS REF_NUM,TO_CHAR(DOC_DATE,'DD/MM/YYYY') AS DOCDATE, " & vbCrLf & " GEN_TYPE, DEPT_CODE AS CC,DIESEL_CONS, DIESEL_RATE,TOT_UNIT AS UNITS_GENERATE,DIESEL_CONS*DIESEL_RATE AS COST  " & vbCrLf & " FROM MAN_DIESELCOSUMP_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_NO"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmDieselCons_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmDieselCons_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(5235)
        Me.Width = VB6.TwipsToPixelsX(9285)

        Call FillCombo()
        '    cboType.Clear
        '    cboType.AddItem "A:Hot Water Generator-I"
        '    cboType.AddItem "B:Burner-1"
        '    cboType.AddItem "C:Burner-2"
        '    cboType.AddItem "D:Burner-3"
        '
        '    cboType.AddItem "E:Hot Water Generator-II"
        '    cboType.AddItem "F:Burner-4"
        '    cboType.AddItem "G:800 KVA DG"
        '    cboType.AddItem "H:500 KVA DG"
        '    cboType.AddItem "I:380 KVA DG"
        '    cboType.AddItem "J:100 KVA DG"
        '    cboType.AddItem "K:OTHERS"
        '
        '    cboType.ListIndex = 0

        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub FillCombo()

        On Error GoTo FillCErr
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset ''Recordset
        Dim RS As ADODB.Recordset
        cboType.Items.Clear()
        MainClass.UOpenRecordSet("SELECT TYPE_CODE,TYPE_DESC  FROM MAN_DIESELTYPE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY TYPE_CODE", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If Not RsTemp.EOF Then
            RsTemp.MoveFirst()
            Do While Not RsTemp.EOF
                cboType.Items.Add(RsTemp.Fields("TYPE_CODE").Value & ":" & IIf(IsDbNull(RsTemp.Fields("TYPE_DESC").Value), "", RsTemp.Fields("TYPE_DESC").Value))
                RsTemp.MoveNext()
            Loop
        End If

        cboType.SelectedIndex = 0

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = -1


        Exit Sub
FillCErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub Clear1()

        On Error GoTo ClearErr

        lblMkey.Text = ""
        txtNumber.Text = ""
        txtIssueNo.Text = ""
        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtTotUnit.Text = ""
        txtRate.Text = ""
        lblTotalCost.Text = ""
        txtRemarks.Text = ""
        txtEmpCode.Text = ""
        lblEmpCode.Text = ""
        txtDept.Text = ""
        txtItemCode.Text = ""
        lblDept.Text = ""
        txtHour.Text = "1.00"
        txtHour.Enabled = False
        lblTotalUnit.Text = CStr(0)
        txtDieselConsumed.Text = CStr(0)
        txtDieselRate.Text = CStr(0)
        lblDieselCost.Text = CStr(0)
        txtDieselRate.Enabled = False

        cboDivision.Enabled = True
        cboDivision.SelectedIndex = IIf(RsCompany.Fields("COMPANY_CODE").Value = 1, 0, -1)

        cboType.SelectedIndex = 0
        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsPower, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500 * 1)
            .set_ColWidth(1, 500 * 2)
            .set_ColWidth(2, 500 * 2)
            .set_ColWidth(3, 500 * 2)
            .set_ColWidth(4, 500 * 2)
            .set_ColWidth(5, 500 * 2)
            .set_ColWidth(6, 500 * 2)
            .set_ColWidth(7, 500 * 2)
            '        .ColWidth(8) = 500 * 2
            '        .ColWidth(9) = 500 * 4
            '        .ColWidth(10) = 500 * 4
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtNumber.Maxlength = RsPower.Fields("AUTO_KEY_NO").Precision
        txtDate.Maxlength = RsPower.Fields("DOC_DATE").DefinedSize - 6
        txtTotUnit.Maxlength = RsPower.Fields("TOT_UNIT").Precision - RsPower.Fields("TOT_UNIT").NumericScale
        txtRate.Maxlength = RsPower.Fields("UNIT_RATE").Precision - RsPower.Fields("UNIT_RATE").NumericScale
        txtHour.Maxlength = RsPower.Fields("WORK_HOUR").Precision - RsPower.Fields("WORK_HOUR").NumericScale
        txtRemarks.Maxlength = RsPower.Fields("REMARKS").DefinedSize
        txtEmpCode.Maxlength = RsPower.Fields("SIGN_EMP_CODE").DefinedSize
        txtDept.Maxlength = RsPower.Fields("DEPT_CODE").DefinedSize
        txtItemCode.Maxlength = RsPower.Fields("ITEM_CODE").DefinedSize

        txtDieselConsumed.Maxlength = RsPower.Fields("DIESEL_CONS").Precision
        txtDieselRate.Maxlength = RsPower.Fields("DIESEL_RATE").Precision
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim ii As Integer
        Dim mDivisionCode As Double
        Dim mCheckLastEntryDate As String
        Dim mStockQty As Double
        Dim mItemUOM As String
        Dim mCommonDivision As Double

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsPower.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtDept.Text) = "" Then
            MsgInformation("Dept is empty, So unable to save.")
            txtDept.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtItemCode.Text) = "" Then
            MsgInformation("Diesel Item Code is empty, So unable to save.")
            txtDept.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtDieselConsumed.Text) <= 0 Then
            MsgInformation("Diesel Consumption is empty, So unable to save.")
            txtDieselConsumed.Focus()
            FieldsVarification = False
            Exit Function
        End If

        '    If Val(lblTotalCost.text) <= 0 Then
        '        MsgBox "Can not be saved for zero consumption"
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        '
        If Trim(txtEmpCode.Text) = "" Then
            MsgInformation("Sign Emp is empty, So unable to save.")
            txtEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Cannot Be Blank", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Division Does Not Exist In Master", vbInformation)
            'txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        Else
            mDivisionCode = Val(MasterNo)
        End If

        If PubSuperUser <> "S" Then
            mCheckLastEntryDate = GetLastEntryDate
            If mCheckLastEntryDate <> "" Then
                If CDate(txtDate.Text) < CDate(mCheckLastEntryDate) Then
                    MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtItemCode.Text), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND ITEM_CLASSIFICATION='3'") = True Then
            mItemUOM = MasterNo
        End If

        mStockQty = GetBalanceStockQty(Trim(txtItemCode.Text), (txtDate.Text), mItemUOM, "STR", "ST", "", ConSH, mDivisionCode, ConStockRefType_SUBISS, Val(txtIssueNo.Text))
        mCommonDivision = GetCommonDivCode
        If mCommonDivision > 0 Then
            mStockQty = mStockQty + GetBalanceStockQty(Trim(txtItemCode.Text), (txtDate.Text), mItemUOM, "STR", "ST", "", ConSH, mCommonDivision, ConStockRefType_SUBISS, Val(txtIssueNo.Text))
        End If

        If mStockQty < Val(txtDieselConsumed.Text) Then
            MsgBox("You have Only Balance Stock is " & mStockQty & " " & mItemUOM, MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function GetLastEntryDate() As String

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = ""
        SqlStr = "SELECT Max(DOC_DATE) AS  ISSUE_DATE " & vbCrLf _
                    & " FROM MAN_DIESELCOSUMP_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetLastEntryDate = IIf(IsDbNull(RsTemp.Fields("ISSUE_DATE").Value), "", RsTemp.Fields("ISSUE_DATE").Value)
        End If

        Exit Function
ErrPart:

    End Function

    Private Sub CalcTotUnit()
        Dim mRate As Double
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mDieselConsumption As Double

        '    mSqlStr = "SELECT GETDIESELRATE(" & RsCompany.fields("COMPANY_CODE").value & ",TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS ERATE FROM DUAL"
        '    MainClass.UOpenRecordSet mSqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '    If RsTemp.EOF = False Then
        '        txtDieselRate.Text = Format(IIf(isdbnull(RsTemp!ERATE), 0, RsTemp!ERATE), "0.00")
        '    Else
        '        txtDieselRate.Text = 0
        '    End If

        mDieselConsumption = Val(txtDieselConsumed.Text) * Val(txtDieselRate.Text)
        lblTotalUnit.Text = VB6.Format(Val(txtTotUnit.Text) * Val(txtHour.Text), "#0.000") ' Format(Val(txtTotUnit.Text) * Val(txtHour.Text), "#0.000")

        If Val(txtTotUnit.Text) = 0 Then
            txtRate.Text = CStr(0)
        Else
            txtRate.Text = VB6.Format(mDieselConsumption / Val(txtTotUnit.Text), "0.00")
        End If

        lblDieselCost.Text = VB6.Format(mDieselConsumption, "#0.00")
        lblTotalCost.Text = VB6.Format(Val(txtTotUnit.Text) * Val(txtRate.Text), "#0.00") ''* Val(txtHour.Text)
    End Sub
    Private Sub frmDieselCons_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsPower.Close()
        RsPower = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtNumber.Text = SprdView.Text
        txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Function ValidateEMP(ByRef pTextBox As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label) As Boolean
        On Error GoTo ValEMP
        Dim SqlStr As String
        ValidateEMP = True
        If Trim(pTextBox.Text) = "" Then Exit Function
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""      ''  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(pTextBox.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Employee Does Not Exist In Master.")
            ValidateEMP = False
        Else
            pLable.text = MasterNo
        End If
        Exit Function
ValEMP:
        MsgBox(Err.Description)
    End Function


    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDieselConsumed_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDieselConsumed.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDieselConsumed_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDieselConsumed.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDieselConsumed_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDieselConsumed.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTotUnit()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDieselRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDieselRate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDieselRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDieselRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtHour_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHour.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtHour_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtHour.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtHour_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtHour.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        '    If Val(txtHour.Text) > 24 Then
        '        MsgInformation "Hour Cann't be Greater than 24."
        '        Cancel = False
        '        Exit Sub
        '    End If
        Call CalcTotUnit()
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtIssueNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIssueNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIssueNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIssueNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.DoubleClick
        Call cmdSearchItem_Click(cmdSearchItem, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchItem_Click(cmdSearchItem, New System.EventArgs())
    End Sub

    Private Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ValEMP
        Dim SqlStr As String
        Dim mRate As Double
        Dim mUOM As String

        If Trim(txtItemCode.Text) = "" Then GoTo EventExitSub
        txtItemCode.Text = Trim(txtItemCode.Text)
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CLASSIFICATION='3'"
        If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Item Does Not Exist In Master.")
            Cancel = True
        Else
            lblItemName.Text = MasterNo
            If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                mUOM = MasterNo
            End If
        End If

        mRate = GetLatestItemCostFromMRR((txtItemCode.Text), mUOM, 1, (txtDate.Text), "L", "ST", "STR")
        txtDieselRate.Text = VB6.Format(mRate, "0.00")

        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItem.Click
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CLASSIFICATION='3'"
        If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItemCode.Text = AcName1
            lblItemName.text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotUnit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotUnit.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotUnit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotUnit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Dept Does Not Exist In Master.")
            Cancel = True
        Else
            lblDept.text = MasterNo
        End If
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTotUnit()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
            GoTo EventExitSub
        End If
        If CDate(txtDate.Text) > CDate(PubCurrDate) Then
            MsgBox("Date Cann't be Greater than Current Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mType As String
        Dim mDivision As String
        Dim mGenType As String

        If Not RsPower.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsPower.Fields("AUTO_KEY_NO").Value), "", RsPower.Fields("AUTO_KEY_NO").Value)
            txtNumber.Text = IIf(IsDbNull(RsPower.Fields("AUTO_KEY_NO").Value), "", RsPower.Fields("AUTO_KEY_NO").Value)
            txtIssueNo.Text = IIf(IsDbNull(RsPower.Fields("AUTO_KEY_ISSUE").Value), "", RsPower.Fields("AUTO_KEY_ISSUE").Value)


            txtDate.Text = IIf(IsDbNull(RsPower.Fields("DOC_DATE").Value), "", RsPower.Fields("DOC_DATE").Value)
            txtTotUnit.Text = IIf(IsDbNull(RsPower.Fields("TOT_UNIT").Value), "", RsPower.Fields("TOT_UNIT").Value)
            txtHour.Text = IIf(IsDbNull(RsPower.Fields("WORK_HOUR").Value), "", RsPower.Fields("WORK_HOUR").Value)
            txtRate.Text = IIf(IsDbNull(RsPower.Fields("UNIT_RATE").Value), "", RsPower.Fields("UNIT_RATE").Value)

            txtDieselConsumed.Text = IIf(IsDbNull(RsPower.Fields("DIESEL_CONS").Value), "", RsPower.Fields("DIESEL_CONS").Value)
            txtDieselRate.Text = IIf(IsDbNull(RsPower.Fields("DIESEL_RATE").Value), "", RsPower.Fields("DIESEL_RATE").Value)

            lblTotalCost.Text = IIf(IsDbNull(RsPower.Fields("TOT_UNIT_COST").Value), "", RsPower.Fields("TOT_UNIT_COST").Value)
            txtRemarks.Text = IIf(IsDbNull(RsPower.Fields("REMARKS").Value), "", RsPower.Fields("REMARKS").Value)
            txtEmpCode.Text = IIf(IsDbNull(RsPower.Fields("SIGN_EMP_CODE").Value), "", RsPower.Fields("SIGN_EMP_CODE").Value)
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
            txtDept.Text = IIf(IsDbNull(RsPower.Fields("DEPT_CODE").Value), "", RsPower.Fields("DEPT_CODE").Value)
            txtDept_Validating(txtDept, New System.ComponentModel.CancelEventArgs(False))

            txtItemCode.Text = IIf(IsDbNull(RsPower.Fields("ITEM_CODE").Value), "", RsPower.Fields("ITEM_CODE").Value)
            txtItemCode_Validating(txtItemCode, New System.ComponentModel.CancelEventArgs(False))

            mGenType = IIf(IsDBNull(RsPower.Fields("GEN_TYPE").Value), "", RsPower.Fields("GEN_TYPE").Value)
            mType = GetGenType(mGenType)
            If mType = "" Then
                cboType.SelectedIndex = -1
            Else
                cboType.Text = mType
            End If

            mDivision = ""
            If MainClass.ValidateWithMasterTable(RsPower.Fields("DIV_CODE").Value, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
            End If
            cboDivision.Text = mDivision
            cboDivision.Enabled = IIf(PubSuperUser = "S", True, False)

            Call CalcTotUnit()
            Call MakeEnableDesableField(False)
        End If
        ADDMode = False
        MODIFYMode = False
        txtNumber.Enabled = True
        cmdSearchNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsPower, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Function GetGenType(ByRef pType As String) As String

        On Error GoTo ShowErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        GetGenType = ""
        SqlStr = "SELECT TYPE_CODE,TYPE_DESC  FROM MAN_DIESELTYPE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE_CODE='" & pType & "' ORDER BY TYPE_CODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If Not RsTemp.EOF Then
            GetGenType = RsTemp.Fields("TYPE_CODE").Value & ":" & IIf(IsDbNull(RsTemp.Fields("TYPE_DESC").Value), "", RsTemp.Fields("TYPE_DESC").Value)
        End If

        Exit Function
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Sub txtNumber_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.DoubleClick
        Call cmdSearchNo_Click(cmdSearchNo, New System.EventArgs())
    End Sub

    Private Sub txtNumber_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtNumber.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchNo_Click(cmdSearchNo, New System.EventArgs())
    End Sub
    Private Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String


        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub
        txtNumber.Text = txtNumber.Text & IIf(Len(txtNumber.Text) <= 6, RsCompany.Fields("FYEAR").Value & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"), "")
        mSlipNo = Val(txtNumber.Text)

        If MODIFYMode = True And RsPower.BOF = False Then xMKey = RsPower.Fields("AUTO_KEY_NO").Value

        SqlStr = "SELECT * FROM MAN_DIESELCOSUMP_TRN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_NO=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPower, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPower.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM MAN_DIESELCOSUMP_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_NO=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPower, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        txtTotUnit.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        txtRate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        '    txtHour.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        txtEmpCode.Enabled = mMode
        cmdSearchEmpCode.Enabled = mMode
        txtDept.Enabled = True '' mMode
        cmdSearchDept.Enabled = True '' mMode

        txtItemCode.Enabled = mMode
        cmdSearchItem.Enabled = mMode
        cboType.Enabled = True '' mMode
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub ReportOnConsump(ByRef Mode As Crystal.DestinationConstants)

    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnConsump(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnConsump(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.DoubleClick
        Call cmdSearchEmpCode_Click(cmdSearchEmpCode, New System.EventArgs())
    End Sub
    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchEmpCode_Click(cmdSearchEmpCode, New System.EventArgs())
    End Sub

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        If ValidateEMP(txtEmpCode, lblEmpCode) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTotUnit_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTotUnit.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTotUnit()
        eventArgs.Cancel = Cancel
    End Sub
End Class
