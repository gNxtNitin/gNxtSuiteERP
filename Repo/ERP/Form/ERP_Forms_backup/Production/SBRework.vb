Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports System.ComponentModel

Friend Class frmSBRework
    Inherits System.Windows.Forms.Form
    Dim RsSBReworkMain As ADODB.Recordset
    Dim RsSBReworkDetail As ADODB.Recordset
    Private PvtDBCn As ADODB.Connection

    Dim IsShowingRecord As Boolean

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 14

    Private Const ColProdCode As Short = 1
    Private Const ColProdDesc As Short = 2
    Private Const ColPartNo As Short = 3
    Private Const ColStockType As Short = 4
    Private Const ColBatchNo As Short = 5
    Private Const ColStockBal As Short = 6
    Private Const ColQuantity As Short = 7
    Private Const ColRecdQuantity As Short = 8
    Private Const ColFaultType As Short = 9
    Private Const ColReason As Short = 10
    Private Const ColWorkerCode As Short = 11
    Private Const ColRemarks As Short = 12
    Private Const ColOldQty As Short = 13
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboShiftcd_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShiftcd.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboShiftcd_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShiftcd.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.SelectedIndexChanged

        On Error GoTo CheckERR
        Dim xItemCode As String
        Dim xItemUOM As String

        Dim mRecdQty As Double
        Dim mStockType As String
        Dim PreviousDept As String
        Dim mRow As Integer
        Dim mProductSeqNo As Integer
        Dim xAutoProductionIssue As Boolean
        Dim mDivisionCode As Double
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        If FormActive = False Then Exit Sub
        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        With SprdMain
            For mRow = 1 To .MaxRows - 1

                .Row = mRow

                .Col = ColProdCode
                xItemCode = Trim(.Text)
                If Trim(xItemCode) = "" Then GoTo NextRecd
                If Trim(txtFromDept.Text) = "" Then GoTo NextRecd

                If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ") = True Then

                    If CheckBOMItem(xItemCode, (txtFromDept.Text)) = False Then
                        MsgInformation("Product Code is Not defined for 'FROM DEPT' : " & xItemCode)
                        MainClass.SetFocusToCell(SprdMain, mRow, ColProdCode)
                        Exit Sub
                    End If

                    .Row = mRow

                    .Col = ColProdDesc
                    .Text = CStr(MasterNo)

                    If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        xItemUOM = MasterNo
                    End If

                    .Row = mRow

                    .Col = ColBatchNo
                    If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                        mBatchNo = Trim(.Text)
                        xFGBatchNoReq = "Y"
                    Else
                        mBatchNo = ""
                        xFGBatchNoReq = "N"
                    End If

                    .Col = ColRecdQuantity
                    mRecdQty = Val(.Text)

                    If cboType.SelectedIndex = 0 Then
                        mProductSeqNo = GetProductSeqNo(xItemCode, Trim(txtFromDept.Text), (txtDate.Text))
                        If mProductSeqNo <= 1 Then
                            MsgInformation("Please Select Correct Dept : " & xItemCode)
                            MainClass.SetFocusToCell(SprdMain, mRow, ColProdCode)
                            Exit Sub
                        Else
                            xAutoProductionIssue = CheckAutoIssueProd((txtDate.Text), xItemCode)
                            If xAutoProductionIssue = False Then
                                PreviousDept = GetProductDept(xItemCode, mProductSeqNo, (txtDate.Text))
                            Else
                                mProductSeqNo = mProductSeqNo - 1
                                PreviousDept = GetProductDept(xItemCode, mProductSeqNo, (txtDate.Text))
                                If Trim(PreviousDept) = "" Then
                                    PreviousDept = Trim(txtFromDept.Text)
                                End If
                            End If
                        End If
                        .Col = ColStockType
                        .Text = "ST"
                        mStockType = Trim(.Text)

                        .Col = ColStockBal
                        If Trim(txtFromDept.Text) = "" Then
                            .Text = "0.00"
                        Else
                            .Text = CStr(GetBalanceStockQty(xItemCode, (txtDate.Text), xItemUOM, Trim(PreviousDept), mStockType, mBatchNo, ConPH, mDivisionCode, ConStockRefType_RWK, Val(txtSlipNo.Text), xFGBatchNoReq) - GetUnApprovedQty(xItemCode, PreviousDept, mDivisionCode))
                        End If
                    Else
                        .Col = ColStockType
                        .Text = "WR"
                        mStockType = Trim(.Text)

                        .Col = ColStockBal
                        If Trim(txtFromDept.Text) = "" Then
                            .Text = "0.00"
                        Else
                            .Text = CStr(GetBalanceStockQty(xItemCode, (txtDate.Text), xItemUOM, Trim(txtFromDept.Text), mStockType, mBatchNo, ConPH, mDivisionCode, ConStockRefType_RWK, Val(txtSlipNo.Text), xFGBatchNoReq) - GetUnApprovedQty(xItemCode, Trim(txtFromDept.Text), mDivisionCode))
                        End If
                    End If
                Else
ErrPart:
                    .Row = mRow

                    .Col = ColProdDesc
                    .Text = ""
                    '                MainClass.SetFocusToCell SprdMain, .ActiveRow, ColProdCode	
                End If
NextRecd:
            Next
        End With
        Exit Sub
CheckERR:
        MsgBox(Err.Description)
    End Sub


    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtSlipNo.Enabled = False
            cmdSearchSlipNo.Enabled = False
            SprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsSBReworkMain.EOF = False Then RsSBReworkMain.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume	
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtSlipNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsSBReworkMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PRD_SENDBACKFORRWK_HDR", (txtSlipNo.Text), RsSBReworkMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_SENDBACKFORRWK_HDR", "AUTO_KEY_SBRWK", (lblMkey.Text)) = False Then GoTo DelErrPart

                If DeleteProdStockTRN(PubDBCn, "2", (txtFromDept.Text), (txtToDept.Text), (txtDate.Text), "") = False Then GoTo DelErrPart
                If DeleteReworkTRN(PubDBCn, ConStockRefType_RWK, CStr(Val(lblMkey.Text))) = False Then GoTo DelErrPart
                If DeleteStockTRN(PubDBCn, ConStockRefType_RWK, CStr(Val(lblMkey.Text))) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM PRD_SENDBACKFORRWK_DET WHERE AUTO_KEY_SBRWK=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM PRD_SENDBACKFORRWK_HDR WHERE AUTO_KEY_SBRWK=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsSBReworkMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        RsSBReworkMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsSBReworkMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtSlipNo.Enabled = False
            cmdSearchSlipNo.Enabled = False
            SprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
        Resume
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
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
        Dim SqlStr As String = ""
        Dim mSlipNo As Double
        Dim mStatus As String
        Dim pErrorDesc As String
        Dim mType As Integer
        Dim mEntryDate As String
        Dim mDivisionCode As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If


        SqlStr = ""
        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime()
        mSlipNo = Val(txtSlipNo.Text)
        If Val(txtSlipNo.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtSlipNo.Text = CStr(mSlipNo)
        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Checked, "C", "P")
        mType = CInt(Trim(VB.Left(cboType.Text, 1)))


        If Trim(txtFromDept.Text) = Trim(txtToDept.Text) And lblBookType.Text = "I" And txtRecdBy.Text = "" Then
            txtRecdBy.Text = txtIssuedBy.Text
            mStatus = "C"
        End If

        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO PRD_SENDBACKFORRWK_HDR " & vbCrLf _
                & " (AUTO_KEY_SBRWK ,COMPANY_CODE," & vbCrLf _
                & " FROM_DEPT,TO_DEPT,SB_DATE, PREP_TIME, EMP_CODE, RECDEMP_CODE," & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE, " & vbCrLf _
                & " STATUS,REWORK_TYPE, DIV_CODE,SHIFT_CODE, SHIFT_EMP_CODE,PROD_DATE )" & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtFromDept.Text) & "','" & MainClass.AllowSingleQuote(txtToDept.Text) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & txtRefTM.Text & "','HH24:MI'), '" & MainClass.AllowSingleQuote(txtIssuedBy.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRecdBy.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'','','" & mStatus & "'," & mType & "," & mDivisionCode & "," & vbCrLf _
                & " '" & cboShiftcd.Text & "', '" & MainClass.AllowSingleQuote(txtEngineerCode.Text) & "', TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE PRD_SENDBACKFORRWK_HDR SET " & vbCrLf _
                    & " AUTO_KEY_SBRWK =" & mSlipNo & ",COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " FROM_DEPT='" & MainClass.AllowSingleQuote(txtFromDept.Text) & "', " & vbCrLf _
                    & " TO_DEPT='" & MainClass.AllowSingleQuote(txtToDept.Text) & "', " & vbCrLf _
                    & " SB_DATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " PROD_DATE=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " PREP_TIME=TO_DATE('" & txtRefTM.Text & "','HH24:MI')," & vbCrLf _
                    & " STATUS='" & mStatus & "', REWORK_TYPE=" & mType & ", " & vbCrLf _
                    & " SHIFT_CODE='" & cboShiftcd.Text & "', " & vbCrLf _
                    & " SHIFT_EMP_CODE='" & MainClass.AllowSingleQuote(txtEngineerCode.Text) & "', " & vbCrLf _
                    & " EMP_CODE='" & MainClass.AllowSingleQuote(txtIssuedBy.Text) & "', " & vbCrLf _
                    & " RECDEMP_CODE='" & MainClass.AllowSingleQuote(txtRecdBy.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " MODDATE=TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),DIV_CODE=" & mDivisionCode & "" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND AUTO_KEY_SBRWK =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail(pErrorDesc, mDivisionCode) = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        txtSlipNo.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        If pErrorDesc = "" Then
            MsgBox(Err.Description)
        Else
            MsgInformation(pErrorDesc)
        End If

        RsSBReworkMain.Requery()
        RsSBReworkDetail.Requery()

        ''Resume	
    End Function
    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Double
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_SBRWK)  " & vbCrLf & " FROM PRD_SENDBACKFORRWK_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_SBRWK,LENGTH(AUTO_KEY_SBRWK)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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

    Private Function UpdateDetail(ByRef pErrorDesc As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetailERR
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mProdCode As String
        Dim mQuantity As Double
        Dim mRecdQuantity As Double
        Dim mRemarks As String
        Dim mOldQty As Double
        Dim mUOM As String
        Dim xStockRowNo As Integer
        Dim mFaultType As String
        Dim mStockType As String
        Dim mProductSeqNo As Integer
        Dim mSqlStr As String

        Dim mOutDeptCode As String
        Dim xOutStockType As String
        Dim mOutQty As Double
        Dim mStockID As String
        Dim xAutoProductionIssue As Boolean
        Dim mReason As String
        Dim mWorkerCode As String
        Dim mCompletionDate As String
        Dim pProdCost As Double
        Dim xFGBatchNo As String

        PubDBCn.Execute("DELETE FROM PRD_SENDBACKFORRWK_DET WHERE AUTO_KEY_SBRWK=" & Val(lblMkey.Text) & "")
        If DeleteReworkTRN(PubDBCn, ConStockRefType_RWK, CStr(Val(lblMkey.Text))) = False Then GoTo UpdateDetailERR
        If DeleteStockTRN(PubDBCn, ConStockRefType_RWK, CStr(Val(lblMkey.Text))) = False Then GoTo UpdateDetailERR

        With SprdMain
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColProdCode
                mProdCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColStockType
                mStockType = MainClass.AllowSingleQuote(.Text)

                .Col = ColQuantity
                mQuantity = Val(.Text)

                .Col = ColRecdQuantity
                mRecdQuantity = Val(.Text)

                If Trim(txtFromDept.Text) = Trim(txtToDept.Text) And lblBookType.Text = "I" And mRecdQuantity = 0 Then
                    mRecdQuantity = mQuantity
                End If

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                .Col = ColOldQty
                mOldQty = Val(.Text)

                .Col = ColFaultType
                mFaultType = VB.Left(.Text, 1)

                .Col = ColReason
                mReason = Trim(.Text)

                .Col = ColWorkerCode
                mWorkerCode = Trim(.Text)


                SqlStr = ""
                If mProdCode <> "" And mRecdQuantity + mQuantity <> 0 Then
                    If MainClass.ValidateWithMasterTable(mProdCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                        .Col = ColBatchNo
                        If Trim(.Text) = "0" Or Trim(.Text) = "" Then
                            xFGBatchNo = ""
                        Else
                            xFGBatchNo = Trim(.Text)
                        End If
                    Else
                        xFGBatchNo = ""
                    End If

                    SqlStr = " INSERT INTO PRD_SENDBACKFORRWK_DET ( " & vbCrLf & " AUTO_KEY_SBRWK, COMPANY_CODE, FROM_DEPT, " & vbCrLf & " TO_DEPT, SB_DATE, PRODUCT_CODE, STOCK_TYPE, SB_QTY, " & vbCrLf & " REMARKS, RECD_QTY,FAULT_TYPE,REASON,WORKER_CODE, BATCH_NO) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtFromDept.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtToDept.Text) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mProdCode & "', '" & mStockType & "', " & vbCrLf & " " & mQuantity & ", '" & mRemarks & "', " & mRecdQuantity & ", " & vbCrLf & " '" & mFaultType & "','" & MainClass.AllowSingleQuote(mReason) & "','" & MainClass.AllowSingleQuote(mWorkerCode) & "','" & Trim(xFGBatchNo) & "') "

                    PubDBCn.Execute(SqlStr)

                    mProductSeqNo = GetProductSeqNo(mProdCode, Trim(txtFromDept.Text), (txtDate.Text))

                    '                If mProductSeqNo = 0 Then	
                    '                    pErrorDesc = Trim(txtFromDept.Text) & " : Department is not in Product Sequence for Product Code :" & mProdCode	
                    '                    UpdateDetail = False	
                    '                    Exit Function	
                    '                End If	
                    '	
                    '                If Trim(txtToDept.Text) <> "STR" Then	
                    '                    mProductSeqNo = GetProductSeqNo(mProdCode, Trim(txtToDept.Text))	
                    '	
                    '                    If mProductSeqNo = 0 Then	
                    '                        pErrorDesc = Trim(txtToDept.Text) & " : Department is not in Product Sequence for Product Code :" & mProdCode	
                    '                        UpdateDetail = False	
                    '                        Exit Function	
                    '                    End If	
                    '                End If	


                    If MainClass.ValidateWithMasterTable(mProdCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mUOM = MasterNo
                    End If

                    If cboType.SelectedIndex = 0 Then
                        mProductSeqNo = GetProductSeqNo(mProdCode, Trim(txtFromDept.Text), (txtDate.Text))
                        '                    If mProductSeqNo <= 1 Then	
                        '                            MsgInformation "Please Select Correct Dept : " & xItemCode	
                        '                            MainClass.SetFocusToCell SprdMain, .ActiveRow, ColProdCode	
                        '                            CheckProd = False	
                        '                            Exit Function	
                        '                    Else	
                        xAutoProductionIssue = CheckAutoIssueProd((txtDate.Text), mProdCode)
                        If xAutoProductionIssue = False Then
                            If mProductSeqNo = 0 Then
                                mOutDeptCode = Trim(txtFromDept.Text)
                            Else
                                mOutDeptCode = GetProductDept(mProdCode, mProductSeqNo, (txtDate.Text))
                            End If
                        Else
                            If mProductSeqNo = 0 Then
                                mOutDeptCode = Trim(txtToDept.Text)
                            Else
                                mProductSeqNo = mProductSeqNo - 1
                                mOutDeptCode = GetProductDept(mProdCode, mProductSeqNo, (txtDate.Text))
                                If mOutDeptCode = "" Then
                                    mOutDeptCode = txtFromDept.Text
                                End If
                            End If
                        End If
                        '                    End If	
                        xOutStockType = "ST"
                    Else
                        mOutDeptCode = txtFromDept.Text
                        xOutStockType = mStockType
                    End If

                    If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
                        mOutQty = mRecdQuantity
                    Else
                        If mRecdQuantity = 0 Then
                            mOutQty = mQuantity
                        Else
                            mOutQty = mRecdQuantity
                        End If
                    End If

                    If mRecdQuantity > 0 Then
                        xStockRowNo = xStockRowNo + 1
                        If UpdateStockTRN(PubDBCn, ConStockRefType_RWK, CStr(Val(lblMkey.Text)), xStockRowNo, (txtDate.Text), (txtDate.Text), xOutStockType, mProdCode, mUOM, xFGBatchNo, mRecdQuantity, 0, "O", 0, 0, "", "", mOutDeptCode, mOutDeptCode, "", "N", "From : " & txtFromDept.Text & "  TO : " & txtToDept.Text & " (For Rework)", "-1", ConPH, mDivisionCode, VB.Left(cboType.Text, 1), "") = False Then GoTo UpdateDetailERR

                        If txtToDept.Text = "STR" Then
                            mStockID = ConWH
                        Else
                            mStockID = ConPH
                        End If

                        xStockRowNo = xStockRowNo + 1
                        If UpdateStockTRN(PubDBCn, ConStockRefType_RWK, CStr(Val(lblMkey.Text)), xStockRowNo, (txtDate.Text), (txtDate.Text), "WR", mProdCode, mUOM, xFGBatchNo, mRecdQuantity, 0, "I", 0, 0, "", "", (txtToDept.Text), (txtToDept.Text), "", "N", "From : " & txtFromDept.Text & "  TO : " & txtToDept.Text & " (For Rework)", "-1", mStockID, mDivisionCode, VB.Left(cboType.Text, 1), "") = False Then GoTo UpdateDetailERR

                        mCompletionDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 15, CDate(txtDate.Text)))
                        pProdCost = 0 ' GetLatestItemCostFromMRR(mProdCode, mUOM, 1, txtDate, "L", "ST", mOutDeptCode)  ''Slow	
                        If UpdateReworkTRN(PubDBCn, CDbl(txtSlipNo.Text), (txtDate.Text), ConStockRefType_RWK, (txtSlipNo.Text), (txtDate.Text), mProdCode, mRecdQuantity, mUOM, pProdCost, "WR", "I", mCompletionDate, Val(CStr(mDivisionCode)), (txtToDept.Text), xFGBatchNo) = False Then GoTo UpdateDetailERR

                    End If
                End If
            Next
        End With
        UpdateDetail = True
        Exit Function
UpdateDetailERR:
        UpdateDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Function

    Private Sub cmdSearchIssue_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchIssue.Click
        On Error GoTo SrchERR
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""      '' AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "ATH_PASSWORD_MST", "EMP_NAME", "USER_ID", , , SqlStr) = True Then
            txtIssuedBy.Text = AcName1
            lblIssuedBy.Text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchRecd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchRecd.Click
        On Error GoTo SrchERR
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""      '' AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "ATH_PASSWORD_MST", "EMP_NAME", "USER_ID", , , SqlStr) = True Then
            txtRecdBy.Text = AcName1
            lblRecdBy.Text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchToDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchToDept.Click
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtToDept.Text = AcName1
            lblToDept.Text = AcName
            If txtToDept.Enabled = True Then txtToDept.Focus()
        End If
    End Sub

    Private Sub cmdSearchFromDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchFromDept.Click
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtFromDept.Text = AcName1
            lblFromDept.Text = AcName
            If txtFromDept.Enabled = True Then txtFromDept.Focus()
        End If
    End Sub

    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_SBRWK,LENGTH(AUTO_KEY_SBRWK)-5,4)=" & RsCompany.Fields("FYEAR").Value & " AND STATUS='P'"
        If MainClass.SearchGridMaster(txtSlipNo.Text, "PRD_SENDBACKFORRWK_HDR", "AUTO_KEY_SBRWK", "FROM_DEPT", "TO_DEPT", "SB_DATE", SqlStr) = True Then
            txtSlipNo.Text = AcName
            'Call txtSlipNo_Validate(False)
            txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
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
            AssignGrid(True)
            '        ADataGrid.Refresh	
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsSBReworkMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmSBRework_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Material Send Back For Rework - Input"

        SqlStr = "Select * From PRD_SENDBACKFORRWK_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSBReworkMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PRD_SENDBACKFORRWK_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSBReworkDetail, ADODB.LockTypeEnum.adLockReadOnly)

        cboType.Items.Clear()
        cboType.Items.Add("1. INSPECTION IN NEXT DEPT")
        cboType.Items.Add("2. INSPECTION IN PDI")

        cboShiftcd.Items.Clear()
        cboShiftcd.Items.Add(("A"))
        cboShiftcd.Items.Add(("B"))
        cboShiftcd.Items.Add(("C"))

        cboShiftcd.SelectedIndex = 0


        AssignGrid(False)
        Call SetTextLengths()
        Call Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf _
            & " AUTO_KEY_SBRWK AS SLIP_NUMBER,FROM_DEPT,TO_DEPT,TO_CHAR(SB_DATE,'DD-MM-YYYY') AS SB_DATE, " & vbCrLf _
            & " EMP_CODE, " & vbCrLf _
            & " DECODE(STATUS,'C','COMPLETE','PENDING') AS STATUS " & vbCrLf _
            & " FROM PRD_SENDBACKFORRWK_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_SBRWK,LENGTH(AUTO_KEY_SBRWK)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " ORDER BY AUTO_KEY_SBRWK"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmSBRework_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmSBRework_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(7710)
        'Me.Width = VB6.TwipsToPixelsX(11370)

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.Text = GetDefaultDivision()             ''cboDivision.SelectedIndex = -1

        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()

        On Error GoTo ClearErr

        lblMkey.Text = ""
        txtSlipNo.Text = ""
        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtProdDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtRefTM.Text = GetServerTime()
        txtEntryDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime()
        txtFromDept.Text = ""
        lblFromDept.Text = ""
        txtToDept.Text = ""
        lblToDept.Text = ""
        txtIssuedBy.Text = PubUserID
        lblIssuedBy.Text = PubUserName
        txtRecdBy.Text = IIf(lblBookType.Text = "R", PubUserID, "")
        lblRecdBy.Text = IIf(lblBookType.Text = "R", PubUserName, "")

        txtEngineerCode.Text = ""
        lblEngineerName.Text = ""

        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStatus.Enabled = IIf(lblBookType.Text = "R", True, False)

        cboShiftcd.SelectedIndex = 0

        txtIssuedBy.Enabled = IIf(lblBookType.Text = "I", True, False)
        cmdSearchIssue.Enabled = IIf(lblBookType.Text = "I", True, False)
        txtRecdBy.Enabled = IIf(lblBookType.Text = "R", True, False)
        cmdSearchRecd.Enabled = IIf(lblBookType.Text = "R", True, False)

        txtEngineerCode.Enabled = IIf(lblBookType.Text = "R", True, False)
        cmdSearchEngineer.Enabled = IIf(lblBookType.Text = "R", True, False)

        cboDivision.Text = GetDefaultDivision()             ''cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        cboType.SelectedIndex = 0
        cboType.Enabled = IIf(lblBookType.Text = "I", True, False)

        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        cboType.SelectedIndex = 0
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsSBReworkMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume	
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim mStr As String

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColProdCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSBReworkDetail.Fields("PRODUCT_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 5.5)

            .Col = ColProdDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 22)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 5)

            .Col = ColBatchNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSBReworkDetail.Fields("BATCH_NO").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '        .CellType = SS_CELL_TYPE_EDIT	
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC	
            '        .TypeEditLen = 255	
            '        .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE	
            ''        .TypeEditMultiLine = True	
            .set_ColWidth(.Col, 5)

            .Col = ColStockBal
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(.Col, 8)
            .ColHidden = False

            .Col = ColQuantity
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(.Col, 8)

            .Col = ColRecdQuantity
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(.Col, 8)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSBReworkDetail.Fields("REMARKS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 22)
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 15)

            .Col = ColOldQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(.Col, 10)
            .ColHidden = True

            '.Col = ColRemarks
            '.CellType = SS_CELL_TYPE_EDIT
            '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '.TypeEditLen = RsSBReworkDetail.Fields("REMARKS").DefinedSize
            '.TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '.TypeEditMultiLine = True
            '.set_ColWidth(.Col, 15)

            .Col = ColReason
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSBReworkDetail.Fields("REASON").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 15)

            .Col = ColWorkerCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSBReworkDetail.Fields("WORKER_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 8)


            .Col = ColFaultType
            If FormActive = False Then
                .CellType = SS_CELL_TYPE_COMBOBOX
                mStr = "0-N/A" & Chr(9) & "1-Paint Dry" & Chr(9) & "2-Dust" & Chr(9) & "3-Blister"
                mStr = mStr & Chr(9) & "4-Over Flow" & Chr(9) & "5-Scratch" & Chr(9) & "6-Damage"
                mStr = mStr & Chr(9) & "7-Thickness" & Chr(9) & "8-Shade Not Match" & Chr(9) & "9-Others"

                .TypeComboBoxList = mStr
                .TypeComboBoxCurSel = 0
                .TypeComboBoxEditable = False
            End If

            .set_ColWidth(ColFaultType, 14)

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColProdDesc, ColProdDesc)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockBal, ColStockBal)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockType, ColStockType)
            If lblBookType.Text = "I" Then
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRecdQuantity, ColRecdQuantity)
            Else
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColQuantity, ColQuantity)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColReason, ColReason)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColWorkerCode, ColWorkerCode)
            End If

        End With
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500)
            .set_ColWidth(1, 500 * 2)
            .set_ColWidth(2, 500 * 2)
            .set_ColWidth(3, 500 * 2)
            .set_ColWidth(4, 500 * 3)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 4)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle	
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.MaxLength = RsSBReworkMain.Fields("AUTO_KEY_SBRWK").Precision
        txtDate.MaxLength = RsSBReworkMain.Fields("SB_DATE").Precision - 6
        txtRefTM.MaxLength = 5
        txtFromDept.MaxLength = RsSBReworkMain.Fields("FROM_DEPT").DefinedSize
        txtToDept.MaxLength = RsSBReworkMain.Fields("TO_DEPT").DefinedSize
        txtIssuedBy.MaxLength = RsSBReworkMain.Fields("EMP_CODE").DefinedSize
        txtEngineerCode.MaxLength = RsSBReworkMain.Fields("EMP_CODE").DefinedSize
        txtProdDate.MaxLength = 10

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub
    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim i As Integer
        Dim mReworkType As String
        Dim mCheckDept As String
        Dim mCheckDeptName As String
        Dim mDeptCode As String
        Dim mIssueQty As Double
        Dim mRecdQty As Double
        Dim mStockQty As Double
        Dim mProdCode As String
        Dim mCheckLastEntryDate As String
        Dim mProductSeqNo As Integer
        Dim mProductSeqNoTo As Integer

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsSBReworkMain.EOF = True Then Exit Function

        '    If Trim(txtDate.Text) = "" Then	
        '        MsgInformation "Date is empty, So unable to save."	
        '        txtDate.SetFocus	
        '        FieldsVarification = False	
        '        Exit Function	
        '    End If	

        If txtDate.Text = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDate.Focus()
            Exit Function
        ElseIf FYChk((txtDate.Text)) = False Then
            FieldsVarification = False
            If txtDate.Enabled = True Then txtDate.Focus()
            Exit Function
        End If

        If Trim(txtFromDept.Text) = "" Then
            MsgInformation("From Dept. is empty, So unable to save.")
            txtFromDept.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtToDept.Text) = "" Then
            MsgInformation("To Dept. is empty, So unable to save.")
            txtToDept.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If
        ''	
        ''    If Trim(txtFromDept.Text) = Trim(txtToDept.Text) Then	
        ''        MsgInformation "From Dept. & To Dept could not be same, So unable to save."	
        ''        txtFromDept.SetFocus	
        ''        FieldsVarification = False	
        ''        Exit Function	
        ''    End If	

        If Trim(cboShiftcd.Text) = "" Then
            MsgBox("Shift is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            cboShiftcd.Focus()
            Exit Function
        End If

        If Trim(txtIssuedBy.Text) = "" Then
            MsgInformation("Issued By is empty, So unable to save.")
            txtIssuedBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If lblBookType.Text = "R" And Trim(txtEngineerCode.Text) = "" Then
            MsgInformation("Shift Engineer is empty, So unable to save.")
            If txtEngineerCode.Enabled = True Then txtEngineerCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If lblBookType.Text = "R" And Trim(txtRecdBy.Text) = "" Then
            MsgInformation("Recd By is empty, So unable to save.")
            If txtRecdBy.Enabled = True Then txtRecdBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If CheckBalRecdQty() = True Then
            chkStatus.CheckState = System.Windows.Forms.CheckState.Checked
        Else
            chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        End If

        If Trim(cboShiftcd.Text) = "C" Then
            txtProdDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(txtDate.Text, "DD/MM/YYYY"))))
            txtProdDate.Text = VB6.Format(txtProdDate.Text, "DD/MM/YYYY")
        Else
            txtProdDate.Text = VB6.Format(txtDate.Text, "DD/MM/YYYY")
        End If

        If PubSuperUser <> "S" Then
            If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked And chkStatus.Enabled = False Then
                MsgInformation("Slip is completed, So unable to save.")
                FieldsVarification = False
                Exit Function
            End If

            If lblBookType.Text = "I" Then
                mCheckDept = Trim(txtFromDept.Text)
                mCheckDeptName = Trim(lblFromDept.Text)
            Else
                mCheckDept = Trim(txtToDept.Text)
                mCheckDeptName = Trim(lblToDept.Text)
            End If

            If ValidateDeptRight(PubUserID, mCheckDept, mCheckDeptName) = False Then
                FieldsVarification = False
                Exit Function
            End If

            '        mCheckLastEntryDate = GetLastEntryDate	
            '	
            '        If mCheckLastEntryDate <> "" Then	
            '            If CDate(txtDate.Text) < CDate(mCheckLastEntryDate) Then	
            '                MsgBox "Cann't be Add or Modify Back Entry", vbInformation	
            '                FieldsVarification = False	
            '                Exit Function	
            '            End If	
            '        End If	


        End If

        With SprdMain
            For i = 1 To .MaxRows

                .Row = i

                .Col = ColProdCode
                mProdCode = Trim(.Text)

                If mProdCode <> "" Then
                    .Col = ColQuantity
                    mIssueQty = Val(.Text)

                    .Col = ColRecdQuantity
                    mRecdQty = Val(.Text)

                    .Col = ColStockBal
                    mStockQty = Val(.Text)

                    .Col = ColReason
                    If Trim(.Text) = "" Then
                        MsgInformation("Please enter the Reason of Item Code :  " & mProdCode)
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColReason)
                        FieldsVarification = False
                        Exit Function
                    End If

                    .Col = ColWorkerCode
                    If lblBookType.Text = "R" And Trim(.Text) = "" Then
                        MsgInformation("Please enter the Code of Worker:  " & mProdCode)
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColWorkerCode)
                        FieldsVarification = False
                        Exit Function
                    End If


                    mProductSeqNo = GetProductSeqNo(mProdCode, Trim(txtFromDept.Text), (txtDate.Text))
                    If mProductSeqNo < 1 And Trim(txtFromDept.Text) = Trim(txtToDept.Text) Then
                        If CheckJobWorkItem(mProdCode) = True Then
                            GoTo NextCheck
                        Else
                            MsgInformation(txtFromDept.Text & " Department is not in Seq of Item Code :  " & mProdCode)
                            '                    If MsgQuestion(txtFromDept.Text & " Department is not in Seq of Item Code :  " & mProdCode & ", Are you want to continue..") = vbNo Then	
                            MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColProdCode)
                            FieldsVarification = False
                            Exit Function
                            '                    End If	
                        End If
                    ElseIf mProductSeqNo < 1 Then
                        MsgInformation(txtFromDept.Text & " Department is not in Seq of Item Code :  " & mProdCode)
                        '                    If MsgQuestion(txtFromDept.Text & " Department is not in Seq of Item Code :  " & mProdCode & ", Are you want to continue..") = vbNo Then	
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColProdCode)
                        FieldsVarification = False
                        Exit Function
                        '                    End If	
                    End If

                    mProductSeqNoTo = GetProductSeqNo(mProdCode, Trim(txtToDept.Text), (txtDate.Text))
                    If mProductSeqNoTo < 1 Then
                        '                  MsgInformation txtToDept.Text & " Department is not in Seq of Item Code :  " & mProdCode	
                        If MsgQuestion(txtToDept.Text & " Department is not in Seq of Item Code :  " & mProdCode & ", Are you want to continue..") = CStr(MsgBoxResult.No) Then
                            MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColProdCode)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    If mProductSeqNoTo > 0 Then
                        If mProductSeqNo < mProductSeqNoTo Then
                            MsgInformation("Please Select Correct Seq of Item Code :  " & mProdCode)
                            MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColProdCode)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
NextCheck:
                    If mIssueQty <> 0 And lblBookType.Text = "I" Then
                        If mStockQty < mIssueQty Then
                            MsgInformation("Issue Qty Cann't be Greater Than Stock Qty.")
                            MainClass.SetFocusToCell(SprdMain, i, ColQuantity)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    If mRecdQty <> 0 And lblBookType.Text = "R" Then
                        If mStockQty < mRecdQty Then
                            MsgInformation("Recd Qty Cann't be Greater Than Stock Qty.")
                            MainClass.SetFocusToCell(SprdMain, i, ColRecdQuantity)
                            FieldsVarification = False
                            Exit Function
                        End If

                        If mIssueQty < mRecdQty Then
                            MsgInformation("Recd Qty Cann't be Greater Than Issue Qty")
                            MainClass.SetFocusToCell(SprdMain, i, ColRecdQuantity)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If
            Next
        End With


        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume	
    End Function
    Private Function CheckJobWorkItem(ByRef pItemCode As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = ""
        CheckJobWorkItem = False
        SqlStr = " SELECT PRODUCT_CODE, WEF FROM PRD_OUTBOM_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' "


        SqlStr = SqlStr & vbCrLf & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) AS WEF " & vbCrLf & " FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & Trim(pItemCode) & "' AND WEF<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckJobWorkItem = True
        End If



        Exit Function
ErrPart:

    End Function

    Private Function GetLastEntryDate() As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = ""

        SqlStr = "SELECT Max(SB_DATE) AS  REF_DATE " & vbCrLf & " FROM PRD_SENDBACKFORRWK_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        If lblBookType.Text = "I" Then
            SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtFromDept.Text) & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtToDept.Text) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetLastEntryDate = IIf(IsDBNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value)
        End If

        Exit Function
ErrPart:

    End Function
    Private Function CheckBalRecdQty() As Boolean
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mIssueQty As Double
        Dim mRecvQty As Double

        CheckBalRecdQty = True
        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then Exit Function

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColQuantity
                mIssueQty = Val(.Text)

                .Col = ColRecdQuantity
                mRecvQty = Val(.Text)

                If mIssueQty <> mRecvQty Then
                    CheckBalRecdQty = False
                    Exit Function
                End If

            Next
        End With
        Exit Function
ErrPart:
        CheckBalRecdQty = False
    End Function
    Private Sub frmSBRework_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsSBReworkMain.Close()
        RsSBReworkMain = Nothing
        RsSBReworkDetail.Close()
        RsSBReworkDetail = Nothing
        'PvtDBCn.Close	
        'Set PvtDBCn = Nothing	
        Me.Hide()
        Me.Close()
    End Sub



    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""
        Dim xICode As String
        Dim mUOM As String
        Dim mStockType As String
        Dim mBatchNo As String

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If eventArgs.row = 0 And eventArgs.col = ColProdCode Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColProdCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "", "", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColProdCode
                    .Text = Trim(AcName)

                    .Col = ColProdDesc
                    .Text = Trim(AcName1)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColProdCode, .ActiveRow, ColProdCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColProdDesc Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColProdDesc
                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "", "", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColProdCode
                    .Text = Trim(AcName1)

                    .Col = ColProdDesc
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColProdCode, .ActiveRow, ColProdCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColBatchNo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColProdCode
                xICode = Trim(.Text)

                '            .Col = ColUom	
                '            mUOM = Trim(.Text)	

                If MainClass.ValidateWithMasterTable(Trim(xICode), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mUOM = Trim(MasterNo)
                End If
                .Col = ColStockType
                mStockType = Trim(.Text)

                .Col = ColBatchNo
                mBatchNo = Trim(.Text)

                SqlStr = GetItemBatchWiseQry(xICode, (txtDate.Text), mUOM, Trim(txtFromDept.Text), mStockType, mBatchNo, ConPH, ConStockRefType_PISS, Val(txtSlipNo.Text))
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColBatchNo
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColBatchNo)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColStockType Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColStockType
                If MainClass.SearchGridMaster(.Text, "INV_TYPE_MST", "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColStockType
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColStockType, .ActiveRow, ColStockType, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColWorkerCode Then
            With SprdMain
                .Row = .ActiveRow


                SqlStr = " SELECT EMP_NAME , EMP_CODE,  EMP_TYPE FROM ("
                SqlStr = SqlStr & vbCrLf & " SELECT EMP.EMP_NAME, EMP.EMP_CODE, 'REGULAR' AS EMP_TYPE" & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND EMP_DEPT_CODE = '" & txtFromDept.Text & "' " & vbCrLf & " AND EMP_CAT_TYPE=2 " & vbCrLf & " AND (EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf & " SELECT EMP.EMP_NAME, EMP.EMP_CODE,  'CASUAL' AS EMP_TYPE" & vbCrLf & " FROM PAY_CONT_EMPLOYEE_MST EMP " & vbCrLf & " WHERE EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND EMP_DEPT_CODE = '" & txtFromDept.Text & "' " & vbCrLf & " AND  (EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
                SqlStr = SqlStr & vbCrLf & " ) ORDER BY EMP_NAME"

                .Col = ColWorkerCode
                If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColWorkerCode
                    .Text = Trim(AcName1)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColProdCode, .ActiveRow, ColWorkerCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColReason Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColReason
                If MainClass.SearchGridMaster(.Text, "PRD_FAULT_MST", "NAME", "", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColReason
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColStockType, .ActiveRow, ColReason, .ActiveRow, False))
            End With
        End If

        '    If eventArgs.row = 0 And eventArgs.col = ColPartyCode Then	
        '        With SprdMain	
        '            .Row = .ActiveRow	
        '	
        '            .Col = ColProdCode	
        '            SqlStr = " SELECT B.SUPP_CUST_CODE, A.SUPP_CUST_NAME " & vbCrLf _	
        ''                    & " FROM FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_DET B " & vbCrLf _	
        ''                    & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _	
        ''                    & " AND A.SUPP_CUST_CODE = B.SUPP_CUST_CODE " & vbCrLf _	
        ''                    & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _	
        ''                    & " AND LTRIM(RTRIM(B.ITEM_CODE)) ='" & MainClass.AllowSingleQuote(.Text) & "' " & vbCrLf _	
        ''                    & " ORDER BY 1"	
        '	
        '            .Col = ColPartyCode	
        '            If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then	
        '                .Row = .ActiveRow	
        '	
        '                .Col = ColPartyCode	
        '                .Text = Trim(AcName)	
        '	
        '                .Col = ColPartyDesc	
        '                .Text = Trim(AcName1)	
        '            End If	
        '            Call SprdMain_LeaveCell(ColPartyCode, .ActiveRow, ColPartyCode, .ActiveRow, False)	
        '        End With	
        '    End If	

        '    If eventArgs.row = 0 And eventArgs.col = ColPartyDesc Then	
        '        With SprdMain	
        '            .Row = .ActiveRow	
        '	
        '            .Col = ColProdCode	
        '            SqlStr = " SELECT  A.SUPP_CUST_NAME,B.SUPP_CUST_CODE " & vbCrLf _	
        ''                    & " FROM FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_DET B " & vbCrLf _	
        ''                    & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _	
        ''                    & " AND A.SUPP_CUST_CODE = B.SUPP_CUST_CODE " & vbCrLf _	
        ''                    & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _	
        ''                    & " AND LTRIM(RTRIM(B.ITEM_CODE)) ='" & MainClass.AllowSingleQuote(.Text) & "' " & vbCrLf _	
        ''                    & " ORDER BY 1"	
        '	
        '            .Col = ColPartyDesc	
        '            If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then	
        '                .Row = .ActiveRow	
        '	
        '                .Col = ColPartyCode	
        '                .Text = Trim(AcName1)	
        '	
        '                .Col = ColPartyDesc	
        '                .Text = Trim(AcName)	
        '            End If	
        '            Call SprdMain_LeaveCell(ColPartyCode, .ActiveRow, ColPartyCode, .ActiveRow, False)	
        '        End With	
        '    End If	

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColProdCode)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Function GetItemBatchWiseQry(ByRef pItemCode As String, ByRef pDateTo As String, ByRef pPackUnit As String, ByRef pDeptCode As String, ByRef pStockType As String, ByRef pLotNo As String, ByRef pStock_ID As String, Optional ByRef pRefType As String = "", Optional ByRef pRefNo As Double = 0) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim mBalQty As Double

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIssueUOM As String
        Dim mPurchaseUOM As String
        Dim mFactor As Double
        Dim mTableName As String
        Dim xItemCode As String


        SqlStr = ""

        SqlStr = "SELECT ITEM_CODE, TO_CHAR(CASE WHEN BATCH_NO<='0' OR BATCH_NO=NULL OR BATCH_NO='' THEN '-1' ELSE BATCH_NO END) BATCH_NO, SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY"

        mTableName = ConInventoryTable

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " "

        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & "AND STOCK_ID='" & pStock_ID & "'"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(pItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='" & pDeptCode & "'" ''pDeptCode	

        If pRefType <> "" And Val(CStr(pRefNo)) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE || REF_NO <> '" & pRefType & pRefNo & "'"
        End If

        If pStockType = "QC" Then
            SqlStr = SqlStr & vbCrLf & " AND (STOCK_TYPE='" & pStockType & "' OR E_DATE>TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY'))"
        Else
            If pStockType = "" Then
                SqlStr = SqlStr & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY')"
            Else
                '            SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='" & pStockType & "'"	

                SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='ST' AND E_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY')"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1))<>0"

        SqlStr = SqlStr & vbCrLf & " GROUP BY ITEM_CODE,TO_CHAR(CASE WHEN BATCH_NO<='0' OR BATCH_NO=NULL OR BATCH_NO='' THEN '-1' ELSE BATCH_NO END)"



        GetItemBatchWiseQry = SqlStr

        Exit Function
ErrPart:
        GetItemBatchWiseQry = ""
    End Function

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColProdCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColProdCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColProdDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColProdDesc, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColReason Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColReason, 0))
        '    If KeyCode = vbKeyF1 And mCol = ColPartyCode Then SprdMain_Click ColPartyCode, 0	
        '    If KeyCode = vbKeyF1 And mCol = ColPartyDesc Then SprdMain_Click ColPartyDesc, 0	
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xProdCode As String
        'Dim xPartyCode As String	
        Dim xStockType As String
        Dim mDivisionCode As Double
        Dim xWorkerCode As String
        Dim xBatchNo As String


        If eventArgs.newRow = -1 Then Exit Sub
        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If


        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColProdCode
        xProdCode = Trim(SprdMain.Text)

        SprdMain.Col = ColStockType
        xBatchNo = Trim(SprdMain.Text)

        SprdMain.Col = ColStockType
        xStockType = Trim(SprdMain.Text)

        SprdMain.Col = ColWorkerCode
        xWorkerCode = Trim(SprdMain.Text)

        If xProdCode = "" Then Exit Sub

        '    SprdMain.Col = ColPartyCode	
        '    xPartyCode = Trim(SprdMain.Text)	

        Select Case eventArgs.col
            Case ColProdCode, ColBatchNo
                If xProdCode = "" Then Exit Sub
                If CheckProd(mDivisionCode) = True Then
                    If CheckDuplicasy(xProdCode, xStockType, xBatchNo) = True Then
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, eventArgs.col) 'ColProdCode	
                    End If
                End If
                '        Case ColPartyCode	
                '            If xPartyCode = "" Then Exit Sub	
                '            If CheckParty = True Then	
                '                Call CheckDuplicasy(xProdCode, xPartyCode)	
                '            End If	
            Case ColQuantity
                If CheckQty() = True Then
                    MainClass.AddBlankSprdRow(SprdMain, ColProdCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
            Case ColRecdQuantity
                If CheckRecdQty() = False Then
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColRecdQuantity)
                    eventArgs.cancel = True
                End If
            Case ColWorkerCode
                If xWorkerCode = "" Then Exit Sub
                ''Temp not required
                'If CheckWorker(xWorkerCode) = False Then
                '    MsgInformation("Please select the Vaild Worker Code.")
                '    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColWorkerCode)
                '    eventArgs.cancel = True
                'End If
            Case ColStockType
                Call CheckStockType()
                If CheckDuplicasy(xProdCode, xStockType, xBatchNo) = True Then
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColStockType)
                End If
            Case ColReason
                Call CheckFaultName()
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CheckFaultName()

        On Error GoTo ChkERR

        With SprdMain
            .Row = .ActiveRow
            .Col = ColReason
            If Trim(.Text) = "" Then Exit Sub
            If MainClass.ValidateWithMasterTable(Trim(.Text), "NAME", "NAME", "PRD_FAULT_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Fault name.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColReason)
                Exit Sub
            End If
        End With
        Exit Sub
ChkERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub CheckStockType()

        On Error GoTo ChkERR
        Dim mStockType As String
        Dim xItemCode As String
        Dim xItemUOM As String
        Dim mRecdQty As Double
        Dim mDivisionCode As Double
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If


        With SprdMain
            .Row = .ActiveRow
            .Col = ColStockType
            If Trim(.Text) = "" Then Exit Sub

            If MainClass.ValidateWithMasterTable(Trim(.Text), "STOCK_TYPE_CODE", "STOCK_TYPE_CODE", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mStockType = MasterNo
                '            If Trim(mStockType) <> "FG" Then	
                '                MsgInformation "Please Select 'FG' Stock Type."	
                '                MainClass.SetFocusToCell SprdMain, .ActiveRow, ColStockType	
                '                Exit Sub	
                '            End If	
            Else
                MsgInformation("Invalid Stock Type.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColStockType)
                Exit Sub
            End If

            .Row = .ActiveRow
            .Col = ColProdCode
            xItemCode = Trim(.Text)

            .Col = ColBatchNo
            If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                mBatchNo = Trim(.Text)
                xFGBatchNoReq = "Y"
            Else
                mBatchNo = ""
                xFGBatchNoReq = "N"
            End If

            If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xItemUOM = MasterNo
            End If

            .Col = ColRecdQuantity
            mRecdQty = Val(.Text)

            .Col = ColStockBal
            If Trim(txtFromDept.Text) = "" Then
                .Text = "0.00"
            Else
                .Text = CStr(GetBalanceStockQty(xItemCode, (txtDate.Text), xItemUOM, Trim(txtFromDept.Text), mStockType, mBatchNo, ConPH, mDivisionCode, ConStockRefType_RWK, Val(txtSlipNo.Text), xFGBatchNoReq) - GetUnApprovedQty(xItemCode, Trim(txtFromDept.Text), mDivisionCode))
            End If

        End With
        Exit Sub
ChkERR:
        MsgBox(Err.Description)
    End Sub
    Private Function CheckQty() As Boolean

        On Error GoTo ERR1
        Dim mStockQty As Double
        Dim mFillQty As Double

        With SprdMain
            .Row = .ActiveRow

            .Col = ColQuantity
            mFillQty = Val(.Text)

            If mFillQty = 0 Then
                CheckQty = True
                Exit Function
            End If
            .Col = ColStockBal
            mStockQty = Val(.Text)


            If mStockQty >= mFillQty Then
                CheckQty = True
            Else
                MsgInformation("You have not enough Stock.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColQuantity)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function CheckRecdQty() As Boolean
        On Error GoTo ERR1
        Dim mIssueQty As Double
        Dim mRecdQty As Double
        Dim mStockQty As Double

        CheckRecdQty = True
        If PubSuperUser = "S" Then Exit Function
        With SprdMain
            .Row = .ActiveRow

            .Col = ColQuantity
            mIssueQty = Val(.Text)

            .Col = ColRecdQuantity
            mRecdQty = Val(.Text)

            .Col = ColStockBal
            mStockQty = Val(.Text)

            If mRecdQty = 0 Then
                CheckRecdQty = True
                Exit Function
            End If

            If mStockQty < mRecdQty Then
                MsgInformation("Recd Qty Cann't be Greater Than Stock Qty.")
                CheckRecdQty = False
                Exit Function
            End If

            If mIssueQty >= mRecdQty Then
                CheckRecdQty = True
            Else
                '            MainClass.SetFocusToCell SprdMain, .ActiveRow, ColRecdQuantity	
                MsgInformation("Recd Qty Cann't be Greater Than Issue Qty")
                CheckRecdQty = False
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function CheckWorker(ByRef xWorkerCode As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckWorker = False

        SqlStr = " SELECT EMP_NAME , EMP_CODE,  EMP_TYPE FROM ("

        SqlStr = SqlStr & vbCrLf _
            & " SELECT EMP.EMP_NAME, EMP.EMP_CODE, 'REGULAR' AS EMP_TYPE" & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP " & vbCrLf _
            & " WHERE EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND EMP_DEPT_CODE = '" & txtFromDept.Text & "' " & vbCrLf _
            & " AND EMP_CAT_TYPE=2 " & vbCrLf _
            & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(xWorkerCode) & "'" & vbCrLf _
            & " AND (EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf _
                & " UNION " & vbCrLf _
                & " SELECT EMP.EMP_NAME, EMP.EMP_CODE,  'CASUAL' AS EMP_TYPE" & vbCrLf _
                & " FROM PAY_CONT_EMPLOYEE_MST EMP " & vbCrLf _
                & " WHERE EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND EMP_DEPT_CODE = '" & txtFromDept.Text & "' " & vbCrLf _
                & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(xWorkerCode) & "'" & vbCrLf _
                & " AND  (EMP.EMP_LEAVE_DATE IS NULL OR EMP.EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf & " ) ORDER BY EMP_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            CheckWorker = True
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function CheckParty() As Boolean
        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim mProdCode As String
        Dim mRsTemp As ADODB.Recordset = Nothing

        '    With SprdMain	
        '        .Row = .ActiveRow	
        '	
        '        .Col = ColProdCode	
        '        mProdCode = .Text	
        '	
        '        .Col = ColPartyCode	
        '        SqlStr = " SELECT B.SUPP_CUST_CODE,A.SUPP_CUST_NAME " & vbCrLf _	
        ''                    & " FROM FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_DET B " & vbCrLf _	
        ''                    & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _	
        ''                    & " AND A.SUPP_CUST_CODE = B.SUPP_CUST_CODE " & vbCrLf _	
        ''                    & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _	
        ''                    & " AND LTRIM(RTRIM(B.ITEM_CODE)) ='" & MainClass.AllowSingleQuote(mProdCode) & "' " & vbCrLf _	
        ''                    & " AND LTRIM(RTRIM(A.SUPP_CUST_CODE)) ='" & MainClass.AllowSingleQuote(.Text) & "' "	
        '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, mRsTemp, adLockReadOnly	
        '        If Not mRsTemp.EOF Then	
        '            .Row = .ActiveRow	
        '	
        '            .Col = ColPartyDesc	
        '            .Text = IIf(IsNull(mRsTemp!SUPP_CUST_NAME), "", mRsTemp!SUPP_CUST_NAME)	
        '	
        '            CheckParty = True	
        '        Else	
        '            .Row = .ActiveRow	
        '	
        '            .Col = ColPartyDesc	
        '            .Text = ""	
        '	
        '            MainClass.SetFocusToCell SprdMain, .ActiveRow, ColPartyCode	
        '        End If	
        '	
        '    End With	
        Exit Function
CheckERR:
        MsgBox(Err.Description)
    End Function

    Private Function CheckDuplicasy(ByRef pProdCode As String, ByRef pStockType As String, ByRef pBatchNo As String) As Boolean
        On Error GoTo ERR1
        Dim i As Integer
        Dim xProdCode As String
        Dim xStockType As String
        Dim xBatchNo As String
        Dim mItemRept As Integer

        If pProdCode = "" Then CheckDuplicasy = False : Exit Function
        If pStockType = "" Then CheckDuplicasy = False : Exit Function

        With SprdMain
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColProdCode
                xProdCode = .Text

                .Col = ColStockType
                xStockType = .Text

                .Col = ColBatchNo
                xBatchNo = .Text

                If UCase(Trim(xProdCode)) & "-" & UCase(Trim(xStockType)) & "-" & UCase(Trim(xBatchNo)) = UCase(Trim(pProdCode)) & "-" & UCase(Trim(pStockType)) & "-" & UCase(Trim(pBatchNo)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicasy = True
                        MsgInformation("Duplicate Record")
                        '                    MainClass.SetFocusToCell SprdMain, .ActiveRow, .ActiveCol	
                        Exit Function
                    End If
                End If
            Next
        End With

        Exit Function
ERR1:
        CheckDuplicasy = False
        MsgInformation(Err.Description)
    End Function

    Private Function CheckProd(ByRef mDivisionCode As Double) As Boolean

        On Error GoTo CheckERR
        Dim xItemCode As String
        Dim xItemUOM As String

        Dim mRecdQty As Double
        Dim mStockType As String
        Dim PreviousDept As String
        Dim mProductSeqNo As Integer
        Dim xAutoProductionIssue As Boolean
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        With SprdMain
            .Row = .ActiveRow

            .Col = ColProdCode
            xItemCode = Trim(.Text)
            If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ") = True Then

                '            If CheckBOMItem(xItemCode, txtFromDept.Text) = False Then	
                '                MsgInformation "Product Code is Not defined for 'FROM DEPT'"	
                '                MainClass.SetFocusToCell SprdMain, .ActiveRow, ColProdCode	
                '                CheckProd = False	
                '                Exit Function	
                '            End If	

                .Row = .ActiveRow

                .Col = ColProdDesc
                .Text = CStr(MasterNo)

                .Col = ColBatchNo
                If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    mBatchNo = Trim(.Text)
                    xFGBatchNoReq = "Y"
                Else
                    mBatchNo = ""
                    xFGBatchNoReq = "N"
                End If

                If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    xItemUOM = MasterNo
                End If
                .Row = .ActiveRow
                .Col = ColRecdQuantity
                mRecdQty = Val(.Text)

                If cboType.SelectedIndex = 0 Then
                    mProductSeqNo = GetProductSeqNo(xItemCode, Trim(txtFromDept.Text), (txtDate.Text))
                    '                If mProductSeqNo <= 1 Then	
                    '                    MsgInformation "Please Select Correct Dept : " & xItemCode	
                    '                    MainClass.SetFocusToCell SprdMain, .ActiveRow, ColProdCode	
                    '                    CheckProd = False	
                    '                    Exit Function	
                    '                Else	
                    xAutoProductionIssue = CheckAutoIssueProd((txtDate.Text), xItemCode)
                    If xAutoProductionIssue = False Then
                        PreviousDept = GetProductDept(xItemCode, mProductSeqNo, (txtDate.Text))
                    Else
                        mProductSeqNo = mProductSeqNo - 1
                        PreviousDept = GetProductDept(xItemCode, mProductSeqNo, (txtDate.Text))
                        If Trim(PreviousDept) = "" Then
                            PreviousDept = Trim(txtFromDept.Text)
                        End If
                    End If
                    '                End If	


                    .Col = ColStockType
                    .Text = "ST"
                    mStockType = Trim(.Text)

                    .Col = ColStockBal
                    If Trim(txtFromDept.Text) = "" Then
                        .Text = "0.00"
                    Else
                        .Text = CStr(GetBalanceStockQty(xItemCode, (txtDate.Text), xItemUOM, Trim(PreviousDept), mStockType, mBatchNo, ConPH, mDivisionCode, ConStockRefType_RWK, Val(txtSlipNo.Text), xFGBatchNoReq) - GetUnApprovedQty(xItemCode, PreviousDept, mDivisionCode))
                    End If
                Else
                    .Col = ColStockType
                    .Text = "WR"
                    mStockType = Trim(.Text)

                    .Col = ColStockBal
                    If Trim(txtFromDept.Text) = "" Then
                        .Text = "0.00"
                    Else
                        .Text = CStr(GetBalanceStockQty(xItemCode, (txtDate.Text), xItemUOM, Trim(txtFromDept.Text), mStockType, mBatchNo, ConPH, mDivisionCode, ConStockRefType_RWK, Val(txtSlipNo.Text), xFGBatchNoReq) - GetUnApprovedQty(xItemCode, Trim(txtFromDept.Text), mDivisionCode))
                    End If
                End If

                CheckProd = True
            Else
ErrPart:
                .Row = .ActiveRow

                .Col = ColProdDesc
                .Text = ""
                CheckProd = False
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColProdCode)
            End If


        End With
        Exit Function
CheckERR:
        MsgBox(Err.Description)
        ''Resume	
    End Function

    Private Function CheckBOMItem(ByRef xItemCode As String, ByRef mDeptCode As String) As Boolean

        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckBOMItem = False
        If xItemCode = "" Then CheckBOMItem = True : Exit Function

        SqlStr = " SELECT PRODUCT_CODE FROM PRD_PRODSEQUENCE_DET" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "'" & vbCrLf _
            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'"

        SqlStr = SqlStr & vbCrLf _
                & " AND WEF = (" & vbCrLf _
                & " SELECT MAX(WEF) AS WEF FROM PRD_PRODSEQUENCE_DET" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "'" & vbCrLf _
                & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'" & vbCrLf _
                & " AND WEF <=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            CheckBOMItem = True
        End If

        Exit Function
CheckERR:
        CheckBOMItem = False
        MsgBox(Err.Description)
    End Function


    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        'With SprdMain
        '    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtSlipNo.Text = SprdView.Text
        txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDate.Text) = True Then
            MsgBox("Not a valid Date")
            Cancel = True
            GoTo EventExitSub
            '    Else	
            '        If ShowRecord(False) = False Then Cancel = True	
        End If

        If Trim(cboShiftcd.Text) = "C" Then
            txtProdDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(txtDate.Text, "DD/MM/YYYY"))))
            txtProdDate.Text = VB6.Format(txtProdDate.Text, "DD/MM/YYYY")
        Else
            txtProdDate.Text = VB6.Format(txtDate.Text, "DD/MM/YYYY")
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEngineerCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEngineerCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEngineerCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEngineerCode.DoubleClick
        Call cmdSearchEngineer_Click(cmdSearchEngineer, New System.EventArgs())
    End Sub

    Private Sub txtEngineerCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEngineerCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchEngineer_Click(cmdSearchEngineer, New System.EventArgs())
    End Sub
    Private Sub txtEngineerCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEngineerCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String = ""
        If Trim(txtEngineerCode.Text) = "" Then GoTo EventExitSub
        txtEngineerCode.Text = VB6.Format(txtEngineerCode.Text, "000000")
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If


        If MainClass.ValidateWithMasterTable(txtEngineerCode.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Employee Does Not Exist In Master.")
            Cancel = True
        Else
            lblEngineerName.Text = MasterNo
        End If
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchEngineer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEngineer.Click
        On Error GoTo SrchERR
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtEngineerCode.Text = AcName1
            lblEngineerName.Text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtIssuedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIssuedBy.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIssuedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIssuedBy.DoubleClick
        Call cmdSearchIssue_Click(cmdSearchIssue, New System.EventArgs())
    End Sub

    Private Sub txtIssuedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtIssuedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchIssue_Click(cmdSearchIssue, New System.EventArgs())
    End Sub

    Private Sub txtIssuedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIssuedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String = ""
        If Trim(txtIssuedBy.Text) = "" Then GoTo EventExitSub
        txtIssuedBy.Text = VB6.Format(txtIssuedBy.Text, "000000")
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        'If ADDMode = True Then
        '    SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        'End If
        If MainClass.ValidateWithMasterTable(txtIssuedBy.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("USer ID Does Not Exist In Master.")
            Cancel = True
        Else
            lblIssuedBy.Text = MasterNo
        End If

        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRecdBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRecdBy.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRecdBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRecdBy.DoubleClick
        Call cmdSearchRecd_Click(cmdSearchRecd, New System.EventArgs())
    End Sub

    Private Sub txtRecdBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRecdBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchRecd_Click(cmdSearchRecd, New System.EventArgs())
    End Sub

    Private Sub txtRecdBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRecdBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String = ""
        If Trim(txtRecdBy.Text) = "" Then GoTo EventExitSub
        'txtRecdBy.Text = VB6.Format(txtRecdBy.Text, "000000")
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        'If ADDMode = True Then
        '    SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        'End If

        If MainClass.ValidateWithMasterTable(txtRecdBy.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("User ID Does Not Exist In Master.")
            Cancel = True
        Else
            lblRecdBy.Text = MasterNo
        End If
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRefTM_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefTM.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToDept.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToDept.DoubleClick
        Call cmdSearchToDept_Click(CmdSearchToDept, New System.EventArgs())
    End Sub

    Private Sub txtToDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtToDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtToDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtToDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtToDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchToDept_Click(CmdSearchToDept, New System.EventArgs())
    End Sub

    Private Sub txtToDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtToDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String = ""

        If Trim(txtToDept.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtToDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblToDept.Text = MasterNo
        Else
            Cancel = True
        End If



EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtFromDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFromDept.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtFromDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFromDept.DoubleClick
        Call cmdSearchFromDept_Click(CmdSearchFromDept, New System.EventArgs())
    End Sub

    Private Sub TxtFromDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFromDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFromDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtFromDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtFromDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchFromDept_Click(CmdSearchFromDept, New System.EventArgs())
    End Sub

    Private Sub TxtFromDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFromDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String = ""

        If Trim(txtFromDept.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtFromDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblFromDept.Text = MasterNo
        Else
            Cancel = True
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mStatus As String
        Dim mCheckFromDept As String
        Dim mCheckToDept As String
        Dim mDeptCode As String
        Dim mEntryDate As String
        Dim mType As Integer
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        If Not RsSBReworkMain.EOF Then
            IsShowingRecord = True
            lblMkey.Text = IIf(IsDBNull(RsSBReworkMain.Fields("AUTO_KEY_SBRWK").Value), "", RsSBReworkMain.Fields("AUTO_KEY_SBRWK").Value)
            txtSlipNo.Text = IIf(IsDBNull(RsSBReworkMain.Fields("AUTO_KEY_SBRWK").Value), "", RsSBReworkMain.Fields("AUTO_KEY_SBRWK").Value)
            txtDate.Text = IIf(IsDBNull(RsSBReworkMain.Fields("SB_DATE").Value), "", RsSBReworkMain.Fields("SB_DATE").Value)

            txtProdDate.Text = VB6.Format(IIf(IsDBNull(RsSBReworkMain.Fields("PROD_DATE").Value), "", RsSBReworkMain.Fields("PROD_DATE").Value), "DD/MM/YYYY")

            txtRefTM.Text = VB6.Format(IIf(IsDBNull(RsSBReworkMain.Fields("PREP_TIME").Value), "", RsSBReworkMain.Fields("PREP_TIME").Value), "HH:MM")
            txtFromDept.Text = IIf(IsDBNull(RsSBReworkMain.Fields("FROM_DEPT").Value), "", RsSBReworkMain.Fields("FROM_DEPT").Value)
            TxtFromDept_Validating(txtFromDept, New System.ComponentModel.CancelEventArgs(False))
            txtToDept.Text = IIf(IsDBNull(RsSBReworkMain.Fields("TO_DEPT").Value), "", RsSBReworkMain.Fields("TO_DEPT").Value)
            txtToDept_Validating(txtToDept, New System.ComponentModel.CancelEventArgs(False))
            txtIssuedBy.Text = IIf(IsDBNull(RsSBReworkMain.Fields("EMP_CODE").Value), "", RsSBReworkMain.Fields("EMP_CODE").Value)

            txtIssuedBy_Validating(txtIssuedBy.Text, New System.ComponentModel.CancelEventArgs(False))
            mType = IIf(IsDBNull(RsSBReworkMain.Fields("REWORK_TYPE").Value), 1, RsSBReworkMain.Fields("REWORK_TYPE").Value)
            cboType.SelectedIndex = mType - 1

            mEntryDate = IIf(IsDBNull(RsSBReworkMain.Fields("ADDUSER").Value), "", RsSBReworkMain.Fields("ADDUSER").Value) & " - " & VB6.Format(IIf(IsDBNull(RsSBReworkMain.Fields("ADDDATE").Value), "", RsSBReworkMain.Fields("ADDDATE").Value), "DD/MM/YYYY HH:MM")
            mEntryDate = mEntryDate & vbCrLf & IIf(IsDBNull(RsSBReworkMain.Fields("MODUSER").Value), "", RsSBReworkMain.Fields("MODUSER").Value) & " - " & VB6.Format(IIf(IsDBNull(RsSBReworkMain.Fields("MODDATE").Value), "", RsSBReworkMain.Fields("MODDATE").Value), "DD/MM/YYYY HH:MM")
            txtEntryDate.Text = mEntryDate

            txtRecdBy.Text = IIf(IsDBNull(RsSBReworkMain.Fields("RECDEMP_CODE").Value), "", RsSBReworkMain.Fields("RECDEMP_CODE").Value)

            txtRecdBy.Text = IIf(lblBookType.Text = "R" And txtRecdBy.Text = "", PubUserID, txtRecdBy.Text)
            txtRecdBy_Validating(txtRecdBy.Text, New System.ComponentModel.CancelEventArgs(False))

            cboShiftcd.Text = IIf(IsDBNull(RsSBReworkMain.Fields("SHIFT_CODE").Value), "", RsSBReworkMain.Fields("SHIFT_CODE").Value)

            txtEngineerCode.Text = IIf(IsDBNull(RsSBReworkMain.Fields("SHIFT_EMP_CODE").Value), "", RsSBReworkMain.Fields("SHIFT_EMP_CODE").Value)
            txtEngineerCode_Validating(txtEngineerCode.Text, New System.ComponentModel.CancelEventArgs(False))

            mStatus = IIf(IsDBNull(RsSBReworkMain.Fields("Status").Value), "C", RsSBReworkMain.Fields("Status").Value)
            chkStatus.CheckState = IIf(mStatus = "C", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkStatus.Enabled = IIf(mStatus = "C", False, True)
            If lblBookType.Text = "I" Then
                cboType.Enabled = IIf(mStatus = "C", False, True)
            End If

            mDivisionCode = IIf(IsDBNull(RsSBReworkMain.Fields("DIV_CODE").Value), -1, RsSBReworkMain.Fields("DIV_CODE").Value)

            If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionDesc = Trim(MasterNo)
                cboDivision.Text = mDivisionDesc
            End If
            cboDivision.Enabled = False

            Call ShowDetail1(mDivisionCode)
            Call MakeEnableDesableField(False)
            IsShowingRecord = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsSBReworkMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub
    Private Sub ShowDetail1(ByRef mDivisionCode As Double)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim xItemCode As String
        Dim xItemUOM As String
        Dim mFaultType As String
        Dim mStockType As String
        Dim mProductSeqNo As Integer
        Dim PreviousDept As String
        Dim xAutoProductionIssue As Boolean
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        SqlStr = ""
        SqlStr = " SELECT PRD_SENDBACKFORRWK_DET.*,INV_ITEM_MST.ITEM_SHORT_DESC " & vbCrLf & " FROM PRD_SENDBACKFORRWK_DET,INV_ITEM_MST " & vbCrLf & " WHERE PRD_SENDBACKFORRWK_DET.COMPANY_CODE=INV_ITEM_MST.COMPANY_CODE " & vbCrLf & " AND PRD_SENDBACKFORRWK_DET.PRODUCT_CODE=INV_ITEM_MST.ITEM_CODE " & vbCrLf & " AND AUTO_KEY_SBRWK=" & Val(lblMkey.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSBReworkDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSBReworkDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            i = 1
            Do While Not .EOF
                SprdMain.Row = i

                SprdMain.Col = ColProdCode
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("PRODUCT_CODE").Value), "", .Fields("PRODUCT_CODE").Value))
                xItemCode = Trim(IIf(IsDBNull(.Fields("PRODUCT_CODE").Value), "", .Fields("PRODUCT_CODE").Value))

                If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    xItemUOM = MasterNo
                End If

                SprdMain.Col = ColProdDesc
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value))

                SprdMain.Col = ColStockType
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value))
                mStockType = Trim(IIf(IsDBNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value))

                SprdMain.Col = ColBatchNo
                If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    mBatchNo = CStr(Val(IIf(IsDBNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value)))
                    SprdMain.Text = IIf(mBatchNo > "0", mBatchNo, IIf(mBatchNo = "-1", mBatchNo, ""))
                    xFGBatchNoReq = "Y"
                Else
                    mBatchNo = "X"
                    SprdMain.Text = ""
                    xFGBatchNoReq = "N"
                End If

                If cboType.SelectedIndex = 0 Then
                    mProductSeqNo = GetProductSeqNo(xItemCode, Trim(txtFromDept.Text), (txtDate.Text))
                    If mProductSeqNo <= 1 Then

                        '                    MsgInformation "Please Select Correct Dept : " & xItemCode	
                        '                    MainClass.SetFocusToCell SprdMain, .ActiveRow, ColProdCode	
                        '                    CheckProd = False	
                        '                    Exit Function	
                    Else
                        xAutoProductionIssue = CheckAutoIssueProd((txtDate.Text), xItemCode)
                        If xAutoProductionIssue = False Then
                            PreviousDept = GetProductDept(xItemCode, mProductSeqNo, (txtDate.Text))
                        Else
                            mProductSeqNo = mProductSeqNo - 1
                            PreviousDept = GetProductDept(xItemCode, mProductSeqNo, (txtDate.Text))
                            If Trim(PreviousDept) = "" Then
                                PreviousDept = Trim(txtFromDept.Text)
                            End If
                        End If
                    End If
                    SprdMain.Col = ColStockBal
                    SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtDate.Text), xItemUOM, Trim(PreviousDept), mStockType, mBatchNo, ConPH, mDivisionCode, ConStockRefType_RWK, Val(txtSlipNo.Text), xFGBatchNoReq) - GetUnApprovedQty(xItemCode, PreviousDept, mDivisionCode))
                Else
                    SprdMain.Col = ColStockBal
                    '            SprdMain.Text = Val(Trim(IIf(IsNull(!RECD_QTY), 0, !RECD_QTY))) + GetBalanceStockQty(xItemCode, txtDate.Text, xItemUOM, Trim(txtFromDept.Text), mStockType, "", ConPH)	
                    SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtDate.Text), xItemUOM, Trim(txtFromDept.Text), mStockType, mBatchNo, ConPH, mDivisionCode, ConStockRefType_RWK, Val(txtSlipNo.Text), xFGBatchNoReq) - GetUnApprovedQty(xItemCode, Trim(txtFromDept.Text), mDivisionCode))
                End If

                SprdMain.Col = ColQuantity
                SprdMain.Text = CStr(Val(Trim(IIf(IsDBNull(.Fields("SB_QTY").Value), "", .Fields("SB_QTY").Value))))

                SprdMain.Col = ColRecdQuantity
                SprdMain.Text = CStr(Val(Trim(IIf(IsDBNull(.Fields("RECD_QTY").Value), "", .Fields("RECD_QTY").Value))))

                SprdMain.Col = ColRemarks
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value))

                SprdMain.Col = ColReason
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("reason").Value), "", .Fields("reason").Value))

                SprdMain.Col = ColWorkerCode
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("WORKER_CODE").Value), "", .Fields("WORKER_CODE").Value))


                SprdMain.Col = ColOldQty
                SprdMain.Text = CStr(Val(Trim(IIf(IsDBNull(.Fields("SB_QTY").Value), "", .Fields("SB_QTY").Value))))

                SprdMain.Col = ColFaultType
                mFaultType = IIf(IsDBNull(.Fields("FAULT_TYPE").Value), "", .Fields("FAULT_TYPE").Value)

                Select Case mFaultType
                    Case "0"
                        SprdMain.Text = "0-N/A"
                    Case "1"
                        SprdMain.Text = "1-Paint Dry"
                    Case "2"
                        SprdMain.Text = "2-Dust"
                    Case "3"
                        SprdMain.Text = "3-Blister"
                    Case "4"
                        SprdMain.Text = "4-Over Flow"
                    Case "5"
                        SprdMain.Text = "5-Scratch"
                    Case "6"
                        SprdMain.Text = "6-Damage"
                    Case "7"
                        SprdMain.Text = "7-Thickness"
                    Case "8"
                        SprdMain.Text = "8-Shade Not Match"
                    Case "9"
                        SprdMain.Text = "9-Others"
                End Select

                .MoveNext()
                i = i + 1
                SprdMain.MaxRows = i
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtSlipNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.DoubleClick
        Call cmdSearchSlipNo_Click(cmdSearchSlipNo, New System.EventArgs())
    End Sub

    Private Sub txtSlipNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSlipNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchSlipNo_Click(cmdSearchSlipNo, New System.EventArgs())
    End Sub
    Private Function ShowRecord(ByRef pByKey As Boolean) As Boolean

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mSlipNo As Double
        Dim mFromDeptCode As String
        Dim mToDeptCode As String
        Dim mSchldDate As String
        Dim SqlStr As String = ""

        ShowRecord = True
        If pByKey = True Then
            If Trim(txtSlipNo.Text) = "" Then Exit Function
            mSlipNo = Val(txtSlipNo.Text)
        Else
            If Trim(txtFromDept.Text) = "" Then Exit Function
            mFromDeptCode = txtFromDept.Text

            If Trim(txtToDept.Text) = "" Then Exit Function
            mToDeptCode = txtToDept.Text

            If Trim(txtDate.Text) = "" Then Exit Function
            mSchldDate = txtDate.Text
        End If

        If MODIFYMode = True And RsSBReworkMain.BOF = False Then xMkey = RsSBReworkMain.Fields("AUTO_KEY_SBRWK").Value

        SqlStr = "SELECT * FROM PRD_SENDBACKFORRWK_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_SBRWK,LENGTH(AUTO_KEY_SBRWK)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        If pByKey = True Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_SBRWK=" & mSlipNo & ""
        Else
            SqlStr = SqlStr & vbCrLf & " AND FROM_DEPT='" & MainClass.AllowSingleQuote(mFromDeptCode) & "'" & vbCrLf & " AND TO_DEPT='" & MainClass.AllowSingleQuote(mToDeptCode) & "'" & vbCrLf & " AND SB_DATE=TO_DATE('" & VB6.Format(mSchldDate, "DD/MMM/YYYY") & "','DD-MON-YYYY')"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSBReworkMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsSBReworkMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                If pByKey = True Then
                    MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Else
                    MsgBox("SB Rework not made for these parameter. Click, Add for New", MsgBoxStyle.Information)
                End If
                ShowRecord = False
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PRD_SENDBACKFORRWK_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_SBRWK,LENGTH(AUTO_KEY_SBRWK)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND AUTO_KEY_SBRWK=" & Val(xMkey) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSBReworkMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Public Sub txtSlipNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSlipNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtSlipNo.Text) = "" Then GoTo EventExitSub
        If Len(txtSlipNo.Text) < 6 Then
            txtSlipNo.Text = Val(txtSlipNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If ShowRecord(True) = False Then Cancel = True
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)


        txtDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False) 'mMode	
        txtFromDept.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        CmdSearchFromDept.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        txtToDept.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        CmdSearchToDept.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)

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
    Private Sub ReportOnSBRework(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mSlipNo As String
        Dim mRPTName As String
        mSlipNo = CStr(Val(txtSlipNo.Text))

        Report1.Reset()
        SqlStr = "SELECT * " & vbCrLf & " FROM PRD_SENDBACKFORRWK_HDR IH, PRD_SENDBACKFORRWK_DET ID, INV_ITEM_MST INVMST, PAY_EMPLOYEE_MST EMP" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_SBRWK=ID.AUTO_KEY_SBRWK" & vbCrLf _
            & " AND IH.COMPANY_CODE=EMP.COMPANY_CODE(+)" & vbCrLf & " AND IH.SHIFT_EMP_CODE=EMP.EMP_CODE(+)" & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.PRODUCT_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.AUTO_KEY_SBRWK=" & mSlipNo & ""

        mTitle = "Material Send Back For Rework"
        mSubTitle = ""
        mRPTName = "SBRework.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRPTName)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume	

    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnSBRework(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnSBRework(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cboShiftcd_Validating(sender As Object, e As CancelEventArgs) Handles cboShiftcd.Validating
        Dim Cancel As Boolean = e.Cancel
        If Trim(cboShiftcd.Text) = "C" Then
            If Trim(txtDate.Text) <> "" Then
                txtProdDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(txtDate.Text, "DD/MM/YYYY"))))
                txtProdDate.Text = VB6.Format(txtProdDate.Text, "DD/MM/YYYY")
            End If
        Else
            txtProdDate.Text = VB6.Format(txtDate.Text, "DD/MM/YYYY")
        End If
        e.Cancel = Cancel
    End Sub
    Private Sub txtProdDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProdDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtProdDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProdDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtProdDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtProdDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Public Function GetUnApprovedQty(ByVal pItemCode As String, ByVal pDeptCode As String, ByVal pDivision As Double) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsBalStock As ADODB.Recordset = Nothing
        Dim mBalQty As Double


        SqlStr = ""
        SqlStr = "SELECT SUM(SB_QTY-RECD_QTY) AS BALQTY" & vbCrLf _
            & " FROM PRD_SENDBACKFORRWK_HDR IH, PRD_SENDBACKFORRWK_DET ID " & vbCrLf _
            & " WHERE IH.AUTO_KEY_SBRWK=ID.AUTO_KEY_SBRWK AND  IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(IH.AUTO_KEY_SBRWK,LENGTH(IH.AUTO_KEY_SBRWK)-5,4) = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND ID.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        If pDivision <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.DIV_CODE=" & pDivision & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='P'"

        SqlStr = SqlStr & vbCrLf & "AND IH.FROM_DEPT='" & MainClass.AllowSingleQuote(txtFromDept.Text) & "'"

        If Val(txtSlipNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND  IH.AUTO_KEY_SBRWK<>" & Val(txtSlipNo.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.SB_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalStock, ADODB.LockTypeEnum.adLockReadOnly)
        If RsBalStock.EOF = False Then
            If IsDBNull(RsBalStock.Fields(0).Value) Then
                mBalQty = 0
            Else
                mBalQty = RsBalStock.Fields(0).Value
            End If
        Else
            mBalQty = 0
        End If
        RsBalStock = Nothing

        GetUnApprovedQty = mBalQty
        Exit Function
ErrPart:
        GetUnApprovedQty = 0
    End Function

    Private Sub frmSBRework_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 300, mReFormWidth))
        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 300, mReFormWidth))
        fraTop1.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth
        'MainClass.SetSpreadColor(UltraGrid1, -1)

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
