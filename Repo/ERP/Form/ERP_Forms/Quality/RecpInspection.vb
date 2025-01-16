Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmRecpInspection
    Inherits System.Windows.Forms.Form
    Dim RsRecpInsMain As ADODB.Recordset
    Dim RsRecpInsDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim xMenuID As String

    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 14

    Private Const ColParameter As Short = 1
    Private Const ColSpecification As Short = 2
    Private Const ColInspection As Short = 3
    Private Const ColCheck As Short = 4
    Private Const ColObserv1 As Short = 5
    Private Const ColObserv2 As Short = 6
    Private Const ColObserv3 As Short = 7
    Private Const ColObserv4 As Short = 8
    Private Const ColObserv5 As Short = 9
    Private Const ColObserv6 As Short = 10
    Private Const ColObserv7 As Short = 11
    Private Const ColObserv8 As Short = 12
    Private Const ColObserv9 As Short = 13
    Private Const ColObserv10 As Short = 14
    Private Const ColObserv11 As Short = 15
    Private Const ColObserv12 As Short = 16
    Private Const ColObserv13 As Short = 17

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
            If RsRecpInsMain.EOF = False Then RsRecpInsMain.MoveFirst()
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

        If txtSlipNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsRecpInsMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                ''Not Req. 21-10-2004 'sk
                '            If UpdateInvGate(False) = False Then GoTo DelErrPart:
                If InsertIntoDelAudit(PubDBCn, "QAL_RECEIPT_HDR", (txtSlipNo.Text), RsRecpInsMain) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_RECEIPT_DET WHERE AUTO_KEY_RECEIPT=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_RECEIPT_HDR WHERE AUTO_KEY_RECEIPT=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsRecpInsMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsRecpInsMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsRecpInsMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then Exit Sub
        If IsRecordExist = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
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
    Private Function IsRecordExist() As Boolean

        On Error GoTo IsRecordExistERR
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String

        IsRecordExist = False
        If MODIFYMode = True Then Exit Function
        SqlStr = " SELECT AUTO_KEY_RECEIPT " & vbCrLf _
                & " FROM QAL_RECEIPT_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND UPPER(LTRIM(RTRIM(ITEM_CODE))) ='" & MainClass.AllowSingleQuote(UCase(txtPartNo.Text)) & "'  " & vbCrLf _
                & " AND UPPER(LTRIM(RTRIM(MRR_NO))) = " & Val(txtMRRNo.Text) & "  "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_RECEIPT").Value)
                IsRecordExist = True
            End If
        End With
        Exit Function
IsRecordExistERR:
        IsRecordExist = True
        MsgBox(Err.Description)
    End Function

    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSlipNo As Double
        Dim mPDIRRecv As String
        Dim mDisposition As String


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mPDIRRecv = IIf(chkPDIR.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        Select Case cboDisposition.Text
            Case "Direct Pass"
                mDisposition = "D"
            Case "Under Deviation"
                mDisposition = "U"
            Case "Segregation"
                mDisposition = "S"
            Case "Rework"
                mDisposition = "R"
            Case "Rejected"
                mDisposition = "J"
        End Select

        SqlStr = ""
        mSlipNo = Val(txtSlipNo.Text)
        If Val(txtSlipNo.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtSlipNo.Text = CStr(mSlipNo)
        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_RECEIPT_HDR " & vbCrLf _
                            & " (AUTO_KEY_RECEIPT,COMPANY_CODE," & vbCrLf _
                            & " INSP_DATE,PROJ_DESC,SUPP_CUST_CODE,MRR_NO,ITEM_CODE,REMARKS," & vbCrLf _
                            & " INSPECTED_BY,AUTH_EMP,RECEIVED_QTY,LOT_ACCEPT,LOT_ACCEPT_DEV,LOT_ACC_SEG, " & vbCrLf _
                            & " LOT_ACC_RWK,REJECTED_QTY,PDIR_FLAG,DISPOSITION,AUTO_KEY_STD, " & vbCrLf _
                            & " STAGE,APPROVED_QTY,STOCK_TYPE, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtProject.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtSource.Text) & "','" & MainClass.AllowSingleQuote(txtMRRNo.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtPartNo.Text) & "','" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtInspectedBy.Text) & "','" & MainClass.AllowSingleQuote(txtAuthorisedBy.Text) & "', " & vbCrLf _
                            & " " & Val(txtReceivedQty.Text) & "," & Val(txtAcceptedQty.Text) & "," & Val(txtUnderDev.Text) & "," & Val(txtSegregated.Text) & ", " & vbCrLf _
                            & " " & Val(txtRework.Text) & "," & Val(txtRejectedQty.Text) & ",'" & mPDIRRecv & "','" & mDisposition & "', " & vbCrLf _
                            & " " & Val(lblAuto_Key_Std.text) & ",'Receipt Inspection'," & Val(lblApprovedQty.text) & ",'" & ConWH & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_RECEIPT_HDR SET " & vbCrLf _
                    & " AUTO_KEY_RECEIPT=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                    & " INSP_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),PROJ_DESC='" & MainClass.AllowSingleQuote(txtProject.Text) & "', " & vbCrLf _
                    & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSource.Text) & "',MRR_NO='" & MainClass.AllowSingleQuote(txtMRRNo.Text) & "', " & vbCrLf _
                    & " ITEM_CODE='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "',REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " INSPECTED_BY='" & MainClass.AllowSingleQuote(txtInspectedBy.Text) & "',AUTH_EMP='" & MainClass.AllowSingleQuote(txtAuthorisedBy.Text) & "', " & vbCrLf _
                    & " RECEIVED_QTY=" & Val(txtReceivedQty.Text) & ",LOT_ACCEPT=" & Val(txtAcceptedQty.Text) & ", " & vbCrLf _
                    & " LOT_ACCEPT_DEV=" & Val(txtUnderDev.Text) & ",LOT_ACC_SEG=" & Val(txtSegregated.Text) & ", " & vbCrLf _
                    & " LOT_ACC_RWK=" & Val(txtRework.Text) & ",REJECTED_QTY=" & Val(txtRejectedQty.Text) & ", " & vbCrLf _
                    & " PDIR_FLAG='" & mPDIRRecv & "',DISPOSITION='" & mDisposition & "',AUTO_KEY_STD=" & Val(lblAuto_Key_Std.text) & ", " & vbCrLf _
                    & " STAGE='Receipt Inspection',APPROVED_QTY=" & Val(lblApprovedQty.text) & ",STOCK_TYPE='" & ConWH & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND AUTO_KEY_RECEIPT =" & Val(lblMkey.text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail = False Then GoTo ErrPart
        ''Not Req. 21-10-2004 'sk
        '    If UpdateInvGate(True) = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        txtSlipNo.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsRecpInsMain.Requery()
        RsRecpInsDetail.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function UpdateInvGate(ByRef pUpdating As Boolean) As Boolean

        On Error GoTo UpdateInvERR
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        If pUpdating = True Then
            SqlStr = " UPDATE INV_GATE_DET  " & vbCrLf _
                            & " SET APPROVED_QTY =" & Val(lblApprovedQty.Text) & " , " & vbCrLf _
                            & " LOT_ACCEPT = " & Val(txtAcceptedQty.Text) & " , " & vbCrLf _
                            & " LOT_ACCEPT_DEV =" & Val(txtUnderDev.Text) & " , " & vbCrLf _
                            & " LOT_ACC_SEG =" & Val(txtSegregated.Text) & " ,  " & vbCrLf _
                            & " LOT_ACC_RWK =" & Val(txtRework.Text) & " , " & vbCrLf _
                            & " REJECTED_QTY =" & Val(txtRejectedQty.Text) & " , " & vbCrLf _
                            & " STOCK_TYPE ='ST', " & vbCrLf _
                            & " CONV_QTY = 0, " & vbCrLf _
                            & " PDIR_FLAG ='" & IIf(chkPDIR.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
                            & " MRR_QCDATE =TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                            & " WHERE LTRIM(RTRIM(ITEM_CODE)) ='" & MainClass.AllowSingleQuote(LTrim(RTrim(txtPartNo.Text))) & "' " & vbCrLf _
                            & " AND AUTO_KEY_MRR =" & Val(txtMRRNo.Text) & " "
            PubDBCn.Execute(SqlStr)

            SqlStr = " SELECT COUNT(1) ACT, SUM(DECODE(STOCK_TYPE, NULL,0, " & vbCrLf & " DECODE(RECEIVED_QTY,(APPROVED_QTY+REJECTED_QTY),1,0))) QC " & vbCrLf & " From INV_GATE_DET " & vbCrLf & " WHERE AUTO_KEY_MRR =" & Val(txtMRRNo.Text) & " " & vbCrLf & " GROUP BY AUTO_KEY_MRR"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            With RsTemp
                If Not .EOF Then
                    If Val(IIf(IsDbNull(.Fields("ACT").Value), "", .Fields("ACT").Value)) = Val(IIf(IsDbNull(.Fields("QC").Value), "", .Fields("QC").Value)) Then
                        SqlStr = "UPDATE INV_GATE_HDR SET QC_STATUS = 'Y', UPDATE_FROM='H'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE AUTO_KEY_MRR = " & Val(txtMRRNo.Text) & " "
                        PubDBCn.Execute(SqlStr)
                    End If
                End If
            End With
        Else
            SqlStr = " UPDATE INV_GATE_DET SET APPROVED_QTY = 0, LOT_ACCEPT = 0, " & vbCrLf & " LOT_ACCEPT_DEV = 0, LOT_ACC_SEG = 0, LOT_ACC_RWK = 0, " & vbCrLf & " REJECTED_QTY = 0, STOCK_TYPE = 'QC', " & vbCrLf & " CONV_QTY = 0, PDIR_FLAG = 'Y',MRR_QCDATE = NULL " & vbCrLf & " WHERE LTRIM(RTRIM(ITEM_CODE)) = '" & MainClass.AllowSingleQuote(RTrim(txtPartNo.Text)) & "' " & vbCrLf & " AND AUTO_KEY_MRR =" & Val(txtMRRNo.Text) & " "
            PubDBCn.Execute(SqlStr)

            SqlStr = " SELECT COUNT(1) ACT, SUM(DECODE(STOCK_TYPE, NULL,0, " & vbCrLf & " DECODE(RECEIVED_QTY,(APPROVED_QTY+REJECTED_QTY),1,0))) QC " & vbCrLf & " From INV_GATE_DET " & vbCrLf & " WHERE AUTO_KEY_MRR =" & Val(txtMRRNo.Text) & " " & vbCrLf & " GROUP BY AUTO_KEY_MRR"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            With RsTemp
                If Not .EOF Then
                    If Val(IIf(IsDbNull(.Fields("ACT").Value), "", .Fields("ACT").Value)) <> Val(IIf(IsDbNull(.Fields("QC").Value), "", .Fields("QC").Value)) Then
                        SqlStr = "UPDATE INV_GATE_HDR SET QC_STATUS = 'N', UPDATE_FROM='H'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE AUTO_KEY_MRR = " & Val(txtMRRNo.Text) & " "
                        PubDBCn.Execute(SqlStr)
                    End If
                End If
            End With

            '        SqlStr = " DELETE FROM QAL_FLASH_HDR WHERE ITEM_CODE ='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "' AND AUTO_KEY_MRR = " & Val(txtMRRNo.Text) & " "
            '        PubDBCn.Execute SqlStr

            '        If UpdateStockTRN(PubDBCn, ConStockRefType_MRR, txtMRRNo.Text, I, mQCDate, _
            ''                                    mStockType, mItemCode, mUnit, mBatchNo, mInvQty, mRejQty, "I", mItemRate, mItemCost, "", "", "STR", "", "", IIf(chkCancelled.Value = vbChecked, "Y", "N"), "From : " & TxtSupplier.Text, pSupplierCode, ConWH) = False Then GoTo UpdateInvERR

        End If
        UpdateInvGate = True
        Exit Function
UpdateInvERR:
        UpdateInvGate = False
        MsgBox(Err.Description)
    End Function
    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_RECEIPT)  " & vbCrLf & " FROM QAL_RECEIPT_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_RECEIPT,LENGTH(AUTO_KEY_RECEIPT)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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

    Private Function UpdateDetail() As Boolean

        On Error GoTo UpdateDetailERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mParameter As String
        Dim mSpecification As String
        Dim mInspection As String
        Dim mCheck As String
        Dim mObserv1 As String
        Dim mObserv2 As String
        Dim mObserv3 As String
        Dim mObserv4 As String
        Dim mObserv5 As String
        Dim mObserv6 As String
        Dim mObserv7 As String
        Dim mObserv8 As String
        Dim mObserv9 As String
        Dim mObserv10 As String
        Dim mObserv11 As String
        Dim mObserv12 As String
        Dim mObserv13 As String

        PubDBCn.Execute("DELETE FROM QAL_RECEIPT_DET WHERE AUTO_KEY_RECEIPT=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColParameter
                mParameter = MainClass.AllowSingleQuote(.Text)

                .Col = ColSpecification
                mSpecification = MainClass.AllowSingleQuote(.Text)

                .Col = ColInspection
                mInspection = MainClass.AllowSingleQuote(.Text)


                .Col = ColCheck
                mCheck = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColObserv1
                mObserv1 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv2
                mObserv2 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv3
                mObserv3 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv4
                mObserv4 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv5
                mObserv5 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv6
                mObserv6 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv7
                mObserv7 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv8
                mObserv8 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv9
                mObserv9 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv10
                mObserv10 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv11
                mObserv11 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv12
                mObserv12 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv13
                mObserv13 = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mParameter <> "" And mSpecification <> "" Then
                    SqlStr = " INSERT INTO  QAL_RECEIPT_DET ( " & vbCrLf & " COMPANY_CODE,AUTO_KEY_RECEIPT,SERIAL_NO,PARAM_DESC,SPECIFICATION,INSP_MTH,CHECK_FLAG, " & vbCrLf & " OBSERV_1,OBSERV_2,OBSERV_3,OBSERV_4,OBSERV_5,OBSERV_6,OBSERV_7,OBSERV_8, " & vbCrLf & " OBSERV_9,OBSERV_10,OBSERV_11,OBSERV_12,OBSERV_13,AUTH_EMP) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMkey.Text) & "," & I & ",'" & mParameter & "','" & mSpecification & "', " & vbCrLf & " '" & mInspection & "','" & mCheck & "','" & mObserv1 & "','" & mObserv2 & "', " & vbCrLf & " '" & mObserv3 & "','" & mObserv4 & "','" & mObserv5 & "','" & mObserv6 & "', " & vbCrLf & " '" & mObserv7 & "','" & mObserv8 & "','" & mObserv9 & "','" & mObserv10 & "', " & vbCrLf & " '" & mObserv11 & "','" & mObserv12 & "','" & mObserv13 & "','" & MainClass.AllowSingleQuote(txtAuthorisedBy.Text) & "' )"
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail = True
        Exit Function
UpdateDetailERR:
        UpdateDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Function

    Private Sub cmdSearchAuthorised_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAuthorised.Click
        Call SearchEmp(txtAuthorisedBy, lblAuthorisedBy)
    End Sub

    Private Sub cmdSearchInspected_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchInspected.Click
        Call SearchEmp(txtInspectedBy, lblInspectedBy)
    End Sub
    Private Sub SearchEmp(ByRef pTextBax As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label)
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            pTextBax.Text = AcName1
            pLable.text = AcName
            If pTextBax.Enabled = True Then pTextBax.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdSearchPartNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPartNo.Click
        On Error GoTo CompERR
        Dim SqlStr As String
        SqlStr = " SELECT A.ITEM_CODE, B.ITEM_SHORT_DESC, B.ITEM_MODEL, A.RECEIVED_QTY " & vbCrLf & " FROM INV_GATE_DET A, INV_ITEM_MST B " & vbCrLf & " WHERE A.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE =B.ITEM_CODE " & vbCrLf & " AND A.AUTO_KEY_MRR =" & Val(txtMRRNo.Text) & " " & vbCrLf & " ORDER BY B.ITEM_SHORT_DESC "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtPartNo.Text = AcName
            lblPartNo.text = AcName1
            txtPartNo_Validating(txtPartNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
        Exit Sub
CompERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdSearchMRRNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchMRRNo.Click

        Dim SqlStr As String
        SqlStr = "SELECT  AUTO_KEY_MRR,QC_STATUS,MRR_DATE,BILL_NO,BILL_DATE " & vbCrLf & " FROM INV_GATE_HDR " & vbCrLf & " WHERE SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtSource.Text) & "' " & vbCrLf & " AND COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY AUTO_KEY_MRR, MRR_DATE "

        '            & " AND QC_STATUS = 'N' " & vbCrLf _
        '
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtMRRNo.Text = AcName
            lblQCStatus.Text = IIf(IsDbNull(AcName1), "N", AcName1)
            txtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs((False)))
        End If
    End Sub
    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_RECEIPT,LENGTH(AUTO_KEY_RECEIPT)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_RECEIPT_HDR", "AUTO_KEY_RECEIPT", "INSP_DATE", "PROJ_DESC", "SUPP_CUST_CODE", SqlStr) = True Then
            txtSlipNo.Text = AcName
            Call txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub cmdSearchSource_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSource.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtSource.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "", "", SqlStr) = True Then
            txtSource.Text = AcName1
            lblSource.text = AcName
            If txtSource.Enabled = True Then txtSource.Focus()
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
        MainClass.ButtonStatus(Me, XRIGHT, RsRecpInsMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmRecpInspection_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Receipt Inspection"

        SqlStr = "Select * From QAL_RECEIPT_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRecpInsMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_RECEIPT_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRecpInsDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_RECEIPT AS SLIP_NUMBER,TO_CHAR(INSP_DATE,'DD/MM/YYYY') AS INSP_DATE, " & vbCrLf & " PROJ_DESC,SUPP_CUST_CODE,MRR_NO,ITEM_CODE  " & vbCrLf & " FROM QAL_RECEIPT_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_RECEIPT,LENGTH(AUTO_KEY_RECEIPT)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_RECEIPT"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmRecpInspection_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmRecpInspection_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMenuID = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(10755)
        Call FillCombo()
        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub FillCombo()

        cboDisposition.Items.Add("Direct Pass")
        cboDisposition.Items.Add("Under Deviation")
        cboDisposition.Items.Add("Segregation")
        cboDisposition.Items.Add("Rework")
        cboDisposition.Items.Add("Rejected")
        cboDisposition.SelectedIndex = 0

    End Sub
    Private Sub Clear1()

        On Error GoTo ClearErr

        lblMkey.Text = ""
        txtSlipNo.Text = ""
        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtProject.Text = ""
        txtSource.Text = ""
        lblSource.Text = ""
        txtMRRNo.Text = ""
        lblQCStatus.Text = ""
        lblBillNo.Text = ""
        lblBillDate.Text = ""
        txtPartNo.Text = ""
        lblPartNo.Text = ""
        txtRemarks.Text = ""
        txtInspectedBy.Text = ""
        txtAuthorisedBy.Text = ""
        txtReceivedQty.Text = ""
        txtAcceptedQty.Text = ""
        txtUnderDev.Text = ""
        txtSegregated.Text = ""
        txtRework.Text = ""
        txtRejectedQty.Text = ""
        chkPDIR.CheckState = System.Windows.Forms.CheckState.Unchecked
        cboDisposition.SelectedIndex = 0
        lblAuto_Key_Std.Text = ""
        lblApprovedQty.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsRecpInsMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String


        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColParameter
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecpInsDetail.Fields("PARAM_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColSpecification
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecpInsDetail.Fields("SPECIFICATION").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColInspection
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecpInsDetail.Fields("INSP_MTH").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColCheck
            .CellType = SS_CELL_TYPE_CHECKBOX

            .Col = ColObserv1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecpInsDetail.Fields("OBSERV_1").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColObserv2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecpInsDetail.Fields("OBSERV_2").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColObserv3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecpInsDetail.Fields("OBSERV_3").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColObserv4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecpInsDetail.Fields("OBSERV_4").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColObserv5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecpInsDetail.Fields("OBSERV_5").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            '        .ColHidden = True

            .Col = ColObserv6
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecpInsDetail.Fields("OBSERV_6").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            '        .ColHidden = True

            .Col = ColObserv7
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecpInsDetail.Fields("OBSERV_7").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            '        .ColHidden = True

            .Col = ColObserv8
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecpInsDetail.Fields("OBSERV_8").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            '        .ColHidden = True

            .Col = ColObserv9
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecpInsDetail.Fields("OBSERV_9").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            '        .ColHidden = True

            .Col = ColObserv10
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecpInsDetail.Fields("OBSERV_10").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            '        .ColHidden = True

            .Col = ColObserv11
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecpInsDetail.Fields("OBSERV_11").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            '        .ColHidden = True

            .Col = ColObserv12
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecpInsDetail.Fields("OBSERV_12").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            '        .ColHidden = True

            .Col = ColObserv13
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecpInsDetail.Fields("OBSERV_13").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            '        .ColHidden = True

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColParameter, ColInspection)
            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500 * 1)
            .set_ColWidth(1, 500 * 4)
            .set_ColWidth(2, 500 * 3)
            .set_ColWidth(3, 500 * 5)
            .set_ColWidth(4, 500 * 3)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 3)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.Maxlength = RsRecpInsMain.Fields("AUTO_KEY_RECEIPT").Precision
        txtDate.Maxlength = RsRecpInsMain.Fields("INSP_DATE").DefinedSize - 6
        txtProject.Maxlength = RsRecpInsMain.Fields("PROJ_DESC").DefinedSize
        txtSource.Maxlength = RsRecpInsMain.Fields("SUPP_CUST_CODE").DefinedSize
        txtMRRNo.Maxlength = RsRecpInsMain.Fields("MRR_NO").Precision
        txtPartNo.Maxlength = RsRecpInsMain.Fields("ITEM_CODE").DefinedSize
        txtRemarks.Maxlength = RsRecpInsMain.Fields("REMARKS").DefinedSize
        txtInspectedBy.Maxlength = RsRecpInsMain.Fields("INSPECTED_BY").DefinedSize
        txtAuthorisedBy.Maxlength = RsRecpInsMain.Fields("AUTH_EMP").DefinedSize
        txtReceivedQty.Maxlength = RsRecpInsMain.Fields("RECEIVED_QTY").Precision
        txtAcceptedQty.Maxlength = RsRecpInsMain.Fields("LOT_ACCEPT").Precision
        txtUnderDev.Maxlength = RsRecpInsMain.Fields("LOT_ACCEPT_DEV").Precision
        txtSegregated.Maxlength = RsRecpInsMain.Fields("LOT_ACC_SEG").Precision
        txtRework.Maxlength = RsRecpInsMain.Fields("LOT_ACC_RWK").Precision
        txtRejectedQty.Maxlength = RsRecpInsMain.Fields("LOT_ACC_RWK").Precision
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim ii As Integer
        Dim mSampleCount As Double
        Dim mCntRow As Integer
        Dim mCntCol As Integer

        Dim mActualCount As Double

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsRecpInsMain.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Report Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtProject.Text) = "" Then
            MsgInformation("Project Description is empty, So unable to save.")
            txtProject.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtSource.Text) = "" Then
            MsgInformation("Supplier Code is empty, So unable to save.")
            txtSource.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtMRRNo.Text) = "" Then
            MsgInformation("MRR Number is empty, So unable to save.")
            txtMRRNo.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtPartNo.Text) = "" Then
            MsgInformation("Part No. is empty, So unable to save.")
            txtPartNo.Focus()
            FieldsVarification = False
            Exit Function
        End If



        mActualCount = 0
        mSampleCount = GetSamplePlan(CDate(VB6.Format(txtDate.Text, "DD/MM/YYYY")), Val(txtReceivedQty.Text))
        If mSampleCount > 0 Then
            For mCntCol = ColObserv1 To ColObserv13
                SprdMain.Col = mCntCol
                For mCntRow = 1 To SprdMain.MaxRows - 1
                    SprdMain.Row = mCntRow
                    If Trim(SprdMain.Text) <> "" Then
                        mActualCount = mActualCount + 1
                        Exit For
                    End If
                Next
            Next
            If mActualCount < mSampleCount Then
                MsgInformation("Min. " & mSampleCount & " Should be checked as per Standard, So unable to save.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColParameter, "S", "Please Check Parameter.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColSpecification, "S", "Please Check Specification.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColInspection, "S", "Please Check Inspection.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        Resume
    End Function

    Private Sub frmRecpInspection_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error GoTo ErrPart
        RsRecpInsMain.Close()
        RsRecpInsMain = Nothing
        RsRecpInsDetail.Close()
        RsRecpInsDetail = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        FormActive = False
        Me.Hide()
        Me.Close()
        Exit Sub
ErrPart:
        '    Exit Sub
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String
        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColParameter)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim xParamDesc As String
        Dim mSpecification As Double
        Dim mMinMargin As Double
        Dim mMaxMargin As Double
        Dim mLoc1 As Integer
        Dim mLoc2 As Integer
        Dim mLoc3 As Integer
        Dim mLoc4 As Integer
        Dim mLoc5 As Integer
        Dim cntCol As Integer
        Dim mValue As Double
        Dim mLoc6 As Integer

        If eventArgs.NewRow = -1 Then Exit Sub


        SprdMain.Row = eventArgs.row 'SprdMain.ActiveRow
        SprdMain.Col = ColParameter
        xParamDesc = Trim(SprdMain.Text)
        If xParamDesc = "" Then Exit Sub

        SprdMain.Row = eventArgs.row '' SprdMain.ActiveRow
        SprdMain.Col = ColSpecification
        mLoc1 = InStr(1, SprdMain.Text, "+/-")
        mLoc2 = InStr(1, SprdMain.Text, "-/+")
        mLoc3 = InStr(1, SprdMain.Text, "+")
        mLoc4 = InStr(1, SprdMain.Text, "-")
        mLoc5 = InStr(1, SprdMain.Text, "±")

        If mLoc3 <> 0 Then
            mLoc6 = InStr(1, SprdMain.Text, "/-")
        End If

        If mLoc1 > 0 Then
            mLoc3 = InStr(1, SprdMain.Text, "-")
            mSpecification = Val(Mid(SprdMain.Text, 1, mLoc1 - 1))
            mMinMargin = mSpecification - Val(Mid(SprdMain.Text, mLoc3 + 1))
            mMaxMargin = mSpecification + Val(Mid(SprdMain.Text, mLoc3 + 1))
        ElseIf mLoc2 > 0 Then
            mLoc3 = InStr(1, SprdMain.Text, "+")
            mSpecification = Val(Mid(SprdMain.Text, 1, mLoc2 - 1))
            mMinMargin = mSpecification - Val(Mid(SprdMain.Text, mLoc3 + 1))
            mMaxMargin = mSpecification + Val(Mid(SprdMain.Text, mLoc3 + 1))
        ElseIf mLoc3 > 0 Then
            mSpecification = Val(Mid(SprdMain.Text, 1, mLoc3 - 1))
            If mLoc6 = 0 Then
                mMinMargin = mSpecification - Val(Mid(SprdMain.Text, mLoc3 + 1))
            Else
                mMinMargin = mSpecification - Val(Mid(SprdMain.Text, mLoc6 + 2))
            End If
            mMaxMargin = mSpecification + Val(Mid(SprdMain.Text, mLoc3 + 1))
        ElseIf mLoc4 > 0 Then
            mSpecification = Val(Mid(SprdMain.Text, 1, mLoc4 - 1))
            mMinMargin = mSpecification - Val(Mid(SprdMain.Text, mLoc4 + 1))
            mMaxMargin = mSpecification + Val(Mid(SprdMain.Text, mLoc4 + 1))
        ElseIf mLoc5 > 0 Then
            mSpecification = Val(Mid(SprdMain.Text, 1, mLoc5 - 1))
            mMinMargin = mSpecification - Val(Mid(SprdMain.Text, mLoc5 + 1))
            mMaxMargin = mSpecification + Val(Mid(SprdMain.Text, mLoc5 + 1))
        Else
            mSpecification = Val(SprdMain.Text)
            mMinMargin = mSpecification
            mMaxMargin = mSpecification
        End If

        If mSpecification <> 0 Then
            For cntCol = ColObserv1 To ColObserv13
                SprdMain.Row = eventArgs.row ''SprdMain.ActiveRow
                SprdMain.Col = cntCol
                mValue = Val(SprdMain.Text)
                If mValue <> 0 Then
                    If mValue < mMinMargin Or mValue > mMaxMargin Then
                        SprdMain.BackColor = System.Drawing.Color.Red
                    Else
                        SprdMain.BackColor = System.Drawing.Color.White
                    End If
                End If
            Next
        End If

        '    Select Case Col
        ''         Case ColParameter
        ''
        ''            SprdMain.Row = SprdMain.ActiveRow
        ''
        ''            SprdMain.Col = ColParameter
        ''            xParamDesc = Trim(SprdMain.Text)
        ''            If xParamDesc = "" Then Exit Sub
        ''            MainClass.AddBlankSprdRow SprdMain, ColParameter, ConRowHeight
        ''            FormatSprdMain SprdMain.MaxRows
        '        Case ColSpecification
        '
        '
        '    End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtSlipNo.Text = SprdView.Text
        txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
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

        If Len(pTextBox.Text) < 6 Then pTextBox.Text = VB6.Format(pTextBox.Text, "000000")

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If
        If MainClass.ValidateWithMasterTable(pTextBox.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Employee Does Not Exist In Master.")
            ValidateEMP = False
        Else
            pLable.text = MasterNo
        End If
        Exit Function
ValEMP:
        MsgBox(Err.Description)
    End Function

    Private Sub txtAcceptedQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAcceptedQty.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAcceptedQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAcceptedQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Function CalcRejQty() As Boolean
        CalcRejQty = True
        lblApprovedQty.Text = CStr(Val(txtAcceptedQty.Text) + Val(txtUnderDev.Text) + Val(txtSegregated.Text) + Val(txtRework.Text))
        txtRejectedQty.Text = VB6.Format(Val(txtReceivedQty.Text) - Val(lblApprovedQty.Text), "#0.00")
        If Val(txtRejectedQty.Text) < 0 Then
            MsgBox("Input Quantity is Invalid, Rejected Qty is < 0! Pls Verify Input")
            CalcRejQty = False
        End If
    End Function

    Private Sub txtAcceptedQty_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAcceptedQty.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CalcRejQty = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtAuthorisedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthorisedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAuthorisedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthorisedBy.DoubleClick
        Call cmdSearchAuthorised_Click(cmdSearchAuthorised, New System.EventArgs())
    End Sub

    Private Sub txtAuthorisedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAuthorisedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchAuthorised_Click(cmdSearchAuthorised, New System.EventArgs())
    End Sub

    Private Sub txtAuthorisedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAuthorisedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtAuthorisedBy, lblAuthorisedBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInspectedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspectedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInspectedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspectedBy.DoubleClick
        Call cmdSearchInspected_Click(cmdSearchInspected, New System.EventArgs())
    End Sub

    Private Sub txtInspectedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtInspectedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchInspected_Click(cmdSearchInspected, New System.EventArgs())
    End Sub

    Private Sub txtInspectedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInspectedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtInspectedBy, lblInspectedBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPartNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPartNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartNo.DoubleClick
        Call cmdSearchPartNo_Click(cmdSearchPartNo, New System.EventArgs())
    End Sub

    Private Sub txtPartNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchPartNo_Click(cmdSearchPartNo, New System.EventArgs())
    End Sub

    Private Sub txtPartNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtPartNo.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT INV_ITEM_MST.ITEM_SHORT_DESC,INV_GATE_DET.RECEIVED_QTY,QAL_INSPECTION_STD_HDR.AUTO_KEY_STD " & vbCrLf _
                    & " FROM INV_GATE_DET,QAL_INSPECTION_STD_HDR, INV_ITEM_MST " & vbCrLf _
                    & " WHERE INV_GATE_DET.COMPANY_CODE =INV_ITEM_MST.COMPANY_CODE " & vbCrLf _
                    & " AND INV_GATE_DET.COMPANY_CODE =QAL_INSPECTION_STD_HDR.COMPANY_CODE(+) " & vbCrLf _
                    & " AND INV_GATE_DET.ITEM_CODE = INV_ITEM_MST.ITEM_CODE " & vbCrLf _
                    & " AND INV_GATE_DET.ITEM_CODE = QAL_INSPECTION_STD_HDR.ITEM_CODE(+) " & vbCrLf _
                    & " AND INV_GATE_DET.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND LTRIM(RTRIM(INV_GATE_DET.ITEM_CODE)) ='" & MainClass.AllowSingleQuote(LTrim(RTrim(txtPartNo.Text))) & "' " & vbCrLf _
                    & " AND INV_GATE_DET.AUTO_KEY_MRR =" & Val(txtMRRNo.Text) & " " & vbCrLf _
                    & " AND QAL_INSPECTION_STD_HDR.INSP_TYPE(+) = 'R'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                lblPartNo.Text = IIf(IsDbNull(mRsTemp.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)
                txtReceivedQty.Text = IIf(IsDbNull(mRsTemp.Fields("RECEIVED_QTY").Value), "", .Fields("RECEIVED_QTY").Value)
                lblAuto_Key_Std.Text = IIf(IsDbNull(mRsTemp.Fields("AUTO_KEY_STD").Value), "", .Fields("AUTO_KEY_STD").Value)
                Call FillInspectionSTD()
            Else
                MsgBox("Not a valid MRRNo's Item.")
                lblPartNo.Text = ""
                txtReceivedQty.Text = ""
                lblAuto_Key_Std.Text = ""
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FillInspectionSTD()

        On Error GoTo FillERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mRsTemp As ADODB.Recordset
        If Trim(lblAuto_Key_Std.Text) = "" Then Exit Sub

        SqlStr = "SELECT SERIAL_NO,PARAM_DESC, SPECIFICATION ,INSP_MTH" & vbCrLf & " From QAL_INSPECTION_STD_DET " & vbCrLf & " WHERE AUTO_KEY_STD =" & Val(lblAuto_Key_Std.Text) & " ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColParameter
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdMain.Col = ColSpecification
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPECIFICATION").Value), "", .Fields("SPECIFICATION").Value))

                SprdMain.Col = ColInspection
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("INSP_MTH").Value), "", .Fields("INSP_MTH").Value))

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub


    Private Sub txtMRRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMRRNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.DoubleClick
        Call CmdSearchMRRNo_Click(CmdSearchMRRNo, New System.EventArgs())
    End Sub

    Private Sub txtMRRNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMRRNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchMRRNo_Click(CmdSearchMRRNo, New System.EventArgs())
    End Sub

    Private Sub txtMRRNo_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.Leave
        If Trim(txtMRRNo.Text) = "" Then Exit Sub
        txtPartNo.Focus()
    End Sub

    Private Sub txtMRRNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtMRRNo.Text) = "" Then GoTo EventExitSub
        SqlStr = "SELECT  AUTO_KEY_MRR,QC_STATUS,MRR_DATE,BILL_NO,BILL_DATE " & vbCrLf & " FROM INV_GATE_HDR " & vbCrLf & " WHERE SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtSource.Text) & "' " & vbCrLf & " AND COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_MRR = " & Val(txtMRRNo.Text) & "  "
        '    If IsShowing = False Then
        '        SqlStr = SqlStr & vbCrLf _
        ''                & " AND QC_STATUS = 'N' "
        '    End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                txtMRRNo.Text = IIf(IsDbNull(mRsTemp.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                lblQCStatus.Text = IIf(IsDbNull(mRsTemp.Fields("QC_STATUS").Value), "N", .Fields("QC_STATUS").Value)
                lblBillNo.Text = IIf(IsDbNull(mRsTemp.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                lblBillDate.Text = IIf(IsDbNull(mRsTemp.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value)
            Else
                MsgBox("Not a valid Source'MRRNo")
                lblBillNo.Text = ""
                lblBillDate.Text = ""
                lblQCStatus.Text = ""
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
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
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProject_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProject.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProject_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProject.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProject.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtReceivedQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReceivedQty.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReceivedQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReceivedQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRejectedQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRejectedQty.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRejectedQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRejectedQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRework_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRework.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRework_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRework.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRework_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRework.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CalcRejQty = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSegregated_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSegregated.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSegregated_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSegregated.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSegregated_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSegregated.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CalcRejQty = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mItemCode As String
        Dim mSuppCode As String


        If Not RsRecpInsMain.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsRecpInsMain.Fields("AUTO_KEY_RECEIPT").Value), "", RsRecpInsMain.Fields("AUTO_KEY_RECEIPT").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsRecpInsMain.Fields("AUTO_KEY_RECEIPT").Value), "", RsRecpInsMain.Fields("AUTO_KEY_RECEIPT").Value)
            txtDate.Text = IIf(IsDbNull(RsRecpInsMain.Fields("INSP_DATE").Value), "", RsRecpInsMain.Fields("INSP_DATE").Value)
            txtProject.Text = IIf(IsDbNull(RsRecpInsMain.Fields("PROJ_DESC").Value), "", RsRecpInsMain.Fields("PROJ_DESC").Value)
            txtSource.Text = IIf(IsDbNull(RsRecpInsMain.Fields("SUPP_CUST_CODE").Value), "", RsRecpInsMain.Fields("SUPP_CUST_CODE").Value)
            txtSource.Text = IIf(IsDbNull(RsRecpInsMain.Fields("SUPP_CUST_CODE").Value), "", RsRecpInsMain.Fields("SUPP_CUST_CODE").Value)
            txtSource_Validating(txtSource, New System.ComponentModel.CancelEventArgs(False))
            txtMRRNo.Text = IIf(IsDbNull(RsRecpInsMain.Fields("MRR_NO").Value), "", RsRecpInsMain.Fields("MRR_NO").Value)
            txtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
            txtPartNo.Text = IIf(IsDbNull(RsRecpInsMain.Fields("ITEM_CODE").Value), "", RsRecpInsMain.Fields("ITEM_CODE").Value)
            txtPartNo_Validating(txtPartNo, New System.ComponentModel.CancelEventArgs(False))
            txtRemarks.Text = IIf(IsDbNull(RsRecpInsMain.Fields("REMARKS").Value), "", RsRecpInsMain.Fields("REMARKS").Value)
            txtInspectedBy.Text = IIf(IsDbNull(RsRecpInsMain.Fields("INSPECTED_BY").Value), "", RsRecpInsMain.Fields("INSPECTED_BY").Value)
            txtInspectedBy_Validating(txtInspectedBy, New System.ComponentModel.CancelEventArgs(False))
            txtAuthorisedBy.Text = IIf(IsDbNull(RsRecpInsMain.Fields("AUTH_EMP").Value), "", RsRecpInsMain.Fields("AUTH_EMP").Value)
            txtAuthorisedBy_Validating(txtAuthorisedBy, New System.ComponentModel.CancelEventArgs(False))
            txtReceivedQty.Text = IIf(IsDbNull(RsRecpInsMain.Fields("RECEIVED_QTY").Value), "", RsRecpInsMain.Fields("RECEIVED_QTY").Value)
            txtAcceptedQty.Text = IIf(IsDbNull(RsRecpInsMain.Fields("LOT_ACCEPT").Value), "", RsRecpInsMain.Fields("LOT_ACCEPT").Value)
            txtUnderDev.Text = IIf(IsDbNull(RsRecpInsMain.Fields("LOT_ACCEPT_DEV").Value), "", RsRecpInsMain.Fields("LOT_ACCEPT_DEV").Value)
            txtSegregated.Text = IIf(IsDbNull(RsRecpInsMain.Fields("LOT_ACC_SEG").Value), "", RsRecpInsMain.Fields("LOT_ACC_SEG").Value)
            txtRework.Text = IIf(IsDbNull(RsRecpInsMain.Fields("LOT_ACC_RWK").Value), "", RsRecpInsMain.Fields("LOT_ACC_RWK").Value)
            txtRework.Text = IIf(IsDbNull(RsRecpInsMain.Fields("LOT_ACC_RWK").Value), "", RsRecpInsMain.Fields("LOT_ACC_RWK").Value)
            txtRejectedQty.Text = IIf(IsDbNull(RsRecpInsMain.Fields("REJECTED_QTY").Value), "", RsRecpInsMain.Fields("REJECTED_QTY").Value)
            chkPDIR.CheckState = IIf(IsDbNull(RsRecpInsMain.Fields("PDIR_FLAG").Value) Or RsRecpInsMain.Fields("PDIR_FLAG").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

            Select Case IIf(IsDbNull(RsRecpInsMain.Fields("DISPOSITION").Value), "", RsRecpInsMain.Fields("DISPOSITION").Value)
                Case "D"
                    cboDisposition.Text = "Direct Pass"
                Case "U"
                    cboDisposition.Text = "Under Deviation"
                Case "S"
                    cboDisposition.Text = "Segregation"
                Case "R"
                    cboDisposition.Text = "Rework"
                Case "J"
                    cboDisposition.Text = "Rejected"
            End Select
            lblAuto_Key_Std.Text = IIf(IsDbNull(RsRecpInsMain.Fields("AUTO_KEY_STD").Value), "", RsRecpInsMain.Fields("AUTO_KEY_STD").Value)
            Call ShowDetail1()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsRecpInsMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_RECEIPT_DET " & vbCrLf & " WHERE AUTO_KEY_RECEIPT=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRecpInsDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsRecpInsDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColParameter
                SprdMain.Text = IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value)

                SprdMain.Col = ColSpecification
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPECIFICATION").Value), "", .Fields("SPECIFICATION").Value))

                SprdMain.Col = ColInspection
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("INSP_MTH").Value), "", .Fields("INSP_MTH").Value))

                SprdMain.Col = ColCheck
                SprdMain.Value = IIf(IsDbNull(.Fields("CHECK_FLAG").Value) Or .Fields("CHECK_FLAG").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                SprdMain.Col = ColObserv1
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_1").Value), "", .Fields("OBSERV_1").Value))

                SprdMain.Col = ColObserv2
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_2").Value), "", .Fields("OBSERV_2").Value))

                SprdMain.Col = ColObserv3
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_3").Value), "", .Fields("OBSERV_3").Value))

                SprdMain.Col = ColObserv4
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_4").Value), "", .Fields("OBSERV_4").Value))

                SprdMain.Col = ColObserv5
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_5").Value), "", .Fields("OBSERV_5").Value))

                SprdMain.Col = ColObserv6
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_6").Value), "", .Fields("OBSERV_6").Value))

                SprdMain.Col = ColObserv7
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_7").Value), "", .Fields("OBSERV_7").Value))

                SprdMain.Col = ColObserv8
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_8").Value), "", .Fields("OBSERV_8").Value))

                SprdMain.Col = ColObserv9
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_9").Value), "", .Fields("OBSERV_9").Value))

                SprdMain.Col = ColObserv10
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_10").Value), "", .Fields("OBSERV_10").Value))

                SprdMain.Col = ColObserv11
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_11").Value), "", .Fields("OBSERV_11").Value))

                SprdMain.Col = ColObserv12
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_12").Value), "", .Fields("OBSERV_12").Value))

                SprdMain.Col = ColObserv13
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_13").Value), "", .Fields("OBSERV_13").Value))

                SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColObserv1, I, ColObserv1, I, False))

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
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
    Public Sub txtSlipNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSlipNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String


        If Trim(txtSlipNo.Text) = "" Then GoTo EventExitSub

        If Len(txtSlipNo.Text) < 6 Then
            txtSlipNo.Text = Val(txtSlipNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mSlipNo = Val(txtSlipNo.Text)

        If MODIFYMode = True And RsRecpInsMain.BOF = False Then xMkey = RsRecpInsMain.Fields("AUTO_KEY_RECEIPT").Value

        SqlStr = "SELECT * FROM QAL_RECEIPT_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_RECEIPT,LENGTH(AUTO_KEY_RECEIPT)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_RECEIPT=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRecpInsMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsRecpInsMain.EOF = False Then
            Clear1()
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_RECEIPT_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_RECEIPT,LENGTH(AUTO_KEY_RECEIPT)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_RECEIPT=" & Val(CStr(xMkey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRecpInsMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtDate.Enabled = mMode
        txtSource.Enabled = mMode
        cmdSearchSource.Enabled = mMode
        txtMRRNo.Enabled = mMode
        CmdSearchMRRNo.Enabled = mMode
        txtPartNo.Enabled = mMode
        cmdSearchPartNo.Enabled = mMode
        txtInspectedBy.Enabled = mMode
        cmdSearchInspected.Enabled = mMode
        txtReceivedQty.Enabled = False ' mMode
        txtRejectedQty.Enabled = False 'mMode
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
    Private Sub ReportOnRecpInsp(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "INSPECTION REPORT OF RECEIPT INSPECTION"
        SqlStr = "SELECT QAL_RECEIPT_HDR.*,QAL_RECEIPT_DET.*, " & vbCrLf & " INV_GATE_HDR.*,INV_ITEM_MST.*,FIN_SUPP_CUST_MST.*, " & vbCrLf & " PAY_EMPLOYEE_MST.EMP_NAME,EMP2.EMP_NAME " & vbCrLf & " FROM QAL_RECEIPT_HDR,QAL_RECEIPT_DET,INV_GATE_HDR,  " & vbCrLf & " INV_ITEM_MST,FIN_SUPP_CUST_MST,PAY_EMPLOYEE_MST ,PAY_EMPLOYEE_MST EMP2 " & vbCrLf & " WHERE QAL_RECEIPT_HDR.AUTO_KEY_RECEIPT=QAL_RECEIPT_DET.AUTO_KEY_RECEIPT " & vbCrLf & " AND QAL_RECEIPT_HDR.MRR_NO=INV_GATE_HDR.AUTO_KEY_MRR " & vbCrLf & " AND QAL_RECEIPT_HDR.COMPANY_CODE=INV_ITEM_MST.COMPANY_CODE " & vbCrLf & " AND QAL_RECEIPT_HDR.ITEM_CODE=INV_ITEM_MST.ITEM_CODE " & vbCrLf & " AND QAL_RECEIPT_HDR.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE " & vbCrLf & " AND QAL_RECEIPT_HDR.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE " & vbCrLf & " AND QAL_RECEIPT_HDR.COMPANY_CODE=PAY_EMPLOYEE_MST.COMPANY_CODE (+) " & vbCrLf & " AND QAL_RECEIPT_HDR.INSPECTED_BY=PAY_EMPLOYEE_MST.EMP_CODE (+) " & vbCrLf & " AND QAL_RECEIPT_HDR.COMPANY_CODE=EMP2.COMPANY_CODE (+) " & vbCrLf & " AND QAL_RECEIPT_HDR.AUTH_EMP=EMP2.EMP_CODE (+) " & vbCrLf & " AND QAL_RECEIPT_HDR.AUTO_KEY_RECEIPT=" & Val(lblMkey.Text) & " ORDER BY SERIAL_NO"

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\InspecRepReceipt.rpt"

        SetCrpt(Report1, Mode, 1, mTitle, , True, xMenuID)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnRecpInsp(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnRecpInsp(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtSource_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSource.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSource_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSource.DoubleClick
        Call cmdSearchSource_Click(cmdSearchSource, New System.EventArgs())
    End Sub

    Private Sub txtSource_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSource.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchSource_Click(cmdSearchSource, New System.EventArgs())
    End Sub

    Private Sub txtSource_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSource.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValSource
        Dim SqlStr As String
        If Trim(txtSource.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtSource.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Source Does Not Exist In Master.")
            Cancel = True
        Else
            lblSource.text = MasterNo
        End If
        GoTo EventExitSub
ValSource:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtUnderDev_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUnderDev.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtUnderDev_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtUnderDev.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CalcRejQty = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub
End Class
