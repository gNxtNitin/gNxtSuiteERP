Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmQualityFlash
    Inherits System.Windows.Forms.Form
    Dim RsFlashMain As ADODB.Recordset
    Dim RsFlashDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim xMenuID As String
    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 14

    Private Const ColProblem As Short = 1
    Private Const ColDefect As Short = 2

    Private Sub chkCall_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCall.CheckStateChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkDeviation_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDeviation.CheckStateChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRejected_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRejected.CheckStateChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkReplyRecv_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkReplyRecv.CheckStateChanged

        If chkReplyRecv.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtReplyDate.Enabled = True
        Else
            txtReplyDate.Enabled = False
            txtReplyDate.Text = ""
        End If
        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRework_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRework.CheckStateChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkSatisfy_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSatisfy.CheckStateChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkSegregation_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSegregation.CheckStateChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
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
            If RsFlashMain.EOF = False Then RsFlashMain.MoveFirst()
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
        If Not RsFlashMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_FLASH_HDR", (txtSlipNo.Text), RsFlashMain) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_FLASH_DET WHERE AUTO_KEY_FLASH=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_FLASH_HDR WHERE AUTO_KEY_FLASH=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsFlashMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsFlashMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsFlashMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

        SqlStr = " SELECT AUTO_KEY_FLASH " & vbCrLf _
                & " FROM QAL_FLASH_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND UPPER(LTRIM(RTRIM(ITEM_CODE))) ='" & MainClass.AllowSingleQuote(UCase(txtItemCode.Text)) & "'  " & vbCrLf _
                & " AND UPPER(LTRIM(RTRIM(AUTO_KEY_MRR))) = '" & MainClass.AllowSingleQuote(UCase(txtMRRNo.Text)) & "'  "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_FLASH").Value)
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
        Dim mCall As String
        Dim mStatus As String
        Dim mDeviation As String
        Dim mSegregation As String
        Dim mRejected As String
        Dim mRework As String
        Dim mUnderstood As String
        Dim mCause As String
        Dim mAdded As String
        Dim mInspection As String
        Dim mSupplierCode As String
        Dim mReplyRecd As String
        Dim mSatisfy As String
        Dim mReworkBy As String




        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If MainClass.ValidateWithMasterTable(lblSupplier.text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            mSupplierCode = MasterNo
        Else
            mSupplierCode = "-1"
        End If

        mCall = IIf(chkCall.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        Select Case cboStatus.Text
            Case "New"
                mStatus = "N"
            Case "Repeated"
                mStatus = "R"
            Case "Repeated After Improvement"
                mStatus = "I"
        End Select
        mDeviation = IIf(chkDeviation.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mSegregation = IIf(chkSegregation.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mRejected = IIf(chkRejected.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mRework = IIf(chkRework.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mUnderstood = VB.Left(cboUnderstood.Text, 1)
        mCause = VB.Left(cboCause.Text, 1)
        mAdded = VB.Left(cboAdded.Text, 1)
        mInspection = VB.Left(cboInspection.Text, 1)

        mReplyRecd = IIf(chkReplyRecv.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mSatisfy = IIf(chkSatisfy.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        Select Case cboReworkBy.Text
            Case "Own Company"
                mReworkBy = "C"
            Case "Other"
                mReworkBy = "O"
        End Select

        SqlStr = ""
        mSlipNo = Val(txtSlipNo.Text)
        If Val(txtSlipNo.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtSlipNo.Text = CStr(mSlipNo)
        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_FLASH_HDR " & vbCrLf _
                            & " (AUTO_KEY_FLASH,COMPANY_CODE," & vbCrLf _
                            & " FLASH_RPT_DATE,AUTO_KEY_MRR,MRR_DATE,ITEM_CODE,SUPP_CUST_CODE,BILL_NO,BILL_DATE," & vbCrLf _
                            & " CALL_FLAG,PROBLEM_STATUS,REC_QTY,ACP_QTY,REJ_QTY,ACP_DEV_FLAG, " & vbCrLf _
                            & " SEG_FLAG,REJ_FLAG,REWORK_FLAG,REPLY_RECV,REPLY_RECV_DATE,SATISFY_REPLY,REWORK_BY, " & vbCrLf _
                            & " PROBLEM_DESC , UNDERSTOOD_FLAG, " & vbCrLf _
                            & " CAUSE_FLAG,MAN_DEFECT,METHOD_DEFECT,MATERIAL_DEFECT,MACHINE_DEFECT, " & vbCrLf _
                            & " MEASURE_DEFECT,CORR_ACTION,ADDED_FLAG,INSP_RPT_START_FLAG,CUST_CORR_DESC,REMARKS, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtMRRNo.Text) & "',TO_DATE('" & vb6.Format(lblMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "','" & MainClass.AllowSingleQuote(mSupplierCode) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(lblBillNo.Text) & "',TO_DATE('" & vb6.Format(lblBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & mCall & "','" & mStatus & "'," & Val(txtReceivedQty.Text) & "," & Val(txtAcceptedQty.Text) & "," & Val(txtRejectedQty.Text) & ",  " & vbCrLf _
                            & " '" & mDeviation & "','" & mSegregation & "','" & mRejected & "','" & mRework & "','" & mReplyRecd & "',TO_DATE('" & vb6.Format(txtReplyDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mSatisfy & "','" & mReworkBy & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtDescription.Text) & "','" & mUnderstood & "','" & mCause & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtMan.Text) & "','" & MainClass.AllowSingleQuote(txtMethod.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtMaterial.Text) & "','" & MainClass.AllowSingleQuote(txtMachine.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtMeasure.Text) & "','" & MainClass.AllowSingleQuote(txtCorrAction.Text) & "', " & vbCrLf _
                            & " '" & mAdded & "','" & mInspection & "','" & MainClass.AllowSingleQuote(txtCorrMeasure.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_FLASH_HDR SET " & vbCrLf _
                    & " AUTO_KEY_FLASH=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " FLASH_RPT_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),AUTO_KEY_MRR='" & MainClass.AllowSingleQuote(txtMRRNo.Text) & "', " & vbCrLf _
                    & " MRR_DATE=TO_DATE('" & vb6.Format(lblMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),   " & vbCrLf _
                    & " ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "',SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "', " & vbCrLf _
                    & " BILL_NO='" & MainClass.AllowSingleQuote(lblBillNo.Text) & "',BILL_DATE=TO_DATE('" & vb6.Format(lblBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " CALL_FLAG='" & mCall & "',PROBLEM_STATUS='" & mStatus & "',REC_QTY=" & Val(txtReceivedQty.Text) & ", " & vbCrLf _
                    & " ACP_QTY=" & Val(txtAcceptedQty.Text) & ",REJ_QTY=" & Val(txtRejectedQty.Text) & ",ACP_DEV_FLAG='" & mDeviation & "', " & vbCrLf _
                    & " SEG_FLAG='" & mSegregation & "',REJ_FLAG='" & mRejected & "',REWORK_FLAG='" & mRework & "', " & vbCrLf _
                    & " REPLY_RECV='" & mReplyRecd & "',REPLY_RECV_DATE=TO_DATE('" & vb6.Format(txtReplyDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),SATISFY_REPLY='" & mSatisfy & "',REWORK_BY='" & mReworkBy & "'," & vbCrLf _
                    & " PROBLEM_DESC='" & MainClass.AllowSingleQuote(txtDescription.Text) & "',UNDERSTOOD_FLAG='" & mUnderstood & "', " & vbCrLf _
                    & " CAUSE_FLAG='" & mCause & "',MAN_DEFECT='" & MainClass.AllowSingleQuote(txtMan.Text) & "',METHOD_DEFECT='" & MainClass.AllowSingleQuote(txtMethod.Text) & "', " & vbCrLf _
                    & " MATERIAL_DEFECT='" & MainClass.AllowSingleQuote(txtMaterial.Text) & "',MACHINE_DEFECT='" & MainClass.AllowSingleQuote(txtMachine.Text) & "', " & vbCrLf _
                    & " MEASURE_DEFECT='" & MainClass.AllowSingleQuote(txtMeasure.Text) & "',CORR_ACTION='" & MainClass.AllowSingleQuote(txtCorrAction.Text) & "', " & vbCrLf _
                    & " ADDED_FLAG='" & mAdded & "',INSP_RPT_START_FLAG='" & mInspection & "',CUST_CORR_DESC='" & MainClass.AllowSingleQuote(txtCorrMeasure.Text) & "', " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND AUTO_KEY_FLASH =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        txtSlipNo.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsFlashMain.Requery()
        RsFlashDetail.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_FLASH)  " & vbCrLf & " FROM QAL_FLASH_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_FLASH,LENGTH(AUTO_KEY_FLASH)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
        Dim mProblem As String
        Dim mDefect As String


        PubDBCn.Execute("DELETE FROM QAL_FLASH_DET WHERE AUTO_KEY_FLASH=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColProblem
                mProblem = MainClass.AllowSingleQuote(.Text)

                .Col = ColDefect
                mDefect = MainClass.AllowSingleQuote(.Text)



                SqlStr = ""

                If Trim(mProblem) <> "" Then
                    SqlStr = " INSERT INTO  QAL_FLASH_DET ( " & vbCrLf & " AUTO_KEY_FLASH,SERIAL_NO,PROBLEM_OBSR,DEFECT_PER) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mProblem & "','" & mDefect & "') "
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

    Private Sub CmdSearchItemCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItemCode.Click
        On Error GoTo CompERR
        Dim SqlStr As String
        SqlStr = "SELECT G.ITEM_CODE,  I.ITEM_SHORT_DESC " & vbCrLf _
                    & " FROM INV_GATE_DET G,INV_ITEM_MST I  " & vbCrLf _
                    & " WHERE G.COMPANY_CODE=I.COMPANY_CODE " & vbCrLf _
                    & " AND G.ITEM_CODE = I.ITEM_CODE " & vbCrLf _
                    & " AND G.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND G.AUTO_KEY_MRR ='" & MainClass.AllowSingleQuote(txtMRRNo.Text) & "'" & vbCrLf _
                    & " ORDER BY I.ITEM_SHORT_DESC "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtItemCode.Text = AcName
            lblItemCode.text = AcName1
            txtItemCode_Validating(txtItemCode, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
        Exit Sub
CompERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdSearchMRRNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchMRRNo.Click
        Dim SqlStr As String

        SqlStr = "SELECT  G.AUTO_KEY_MRR, F.SUPP_CUST_NAME,G.BILL_NO,G.BILL_DATE " & vbCrLf & " FROM INV_GATE_HDR G, FIN_SUPP_CUST_MST F " & vbCrLf & " WHERE F.COMPANY_CODE = G.COMPANY_CODE " & vbCrLf & " AND F.SUPP_CUST_CODE =  G.SUPP_CUST_CODE " & vbCrLf & " AND G.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY G.AUTO_KEY_MRR "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtMRRNo.Text = AcName
            lblSupplier.text = AcName1
            txtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub
    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_FLASH,LENGTH(AUTO_KEY_FLASH)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_FLASH_HDR", "AUTO_KEY_FLASH", "AUTO_KEY_MRR", "ITEM_CODE", "SUPP_CUST_CODE", SqlStr) = True Then
            txtSlipNo.Text = AcName
            Call txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
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
        MainClass.ButtonStatus(Me, XRIGHT, RsFlashMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmQualityFlash_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Quality Flash Report"

        SqlStr = "Select * From QAL_FLASH_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFlashMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_FLASH_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFlashDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_FLASH AS SLIP_NUMBER,TO_CHAR(FLASH_RPT_DATE,'DD/MM/YYYY') AS DOC_DATE, " & vbCrLf & " AUTO_KEY_MRR,ITEM_CODE,CMST.SUPP_CUST_NAME,BILL_NO,BILL_DATE,REPLY_RECV, " & vbCrLf & " TO_CHAR(REPLY_RECV_DATE,'DD/MM/YYYY') AS REPLY_RECV_DATE, " & vbCrLf & " TO_CHAR(TO_DATE(REPLY_RECV_DATE)-TO_DATE(FLASH_RPT_DATE)) AS DAYS_TAKEN,SATISFY_REPLY,REWORK_BY  " & vbCrLf & " FROM QAL_FLASH_HDR IH, FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_FLASH,LENGTH(AUTO_KEY_FLASH)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " ORDER BY AUTO_KEY_FLASH"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmQualityFlash_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmQualityFlash_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(10755)
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

        cboStatus.Items.Add("New")
        cboStatus.Items.Add("Repeated")
        cboStatus.Items.Add("Repeated After Improvement")
        cboStatus.SelectedIndex = 0

        cboReworkBy.Items.Add("Own Company")
        cboReworkBy.Items.Add("Other")

        cboUnderstood.Items.Add("Yes")
        cboUnderstood.Items.Add("No")
        cboUnderstood.SelectedIndex = 0

        cboCause.Items.Add("Yes")
        cboCause.Items.Add("No")
        cboCause.SelectedIndex = 0

        cboAdded.Items.Add("Yes")
        cboAdded.Items.Add("No")
        cboAdded.SelectedIndex = 0

        cboInspection.Items.Add("Yes")
        cboInspection.Items.Add("No")
        cboInspection.SelectedIndex = 0

    End Sub
    Private Sub Clear1()

        On Error GoTo ClearErr

        lblMkey.Text = ""
        txtSlipNo.Text = ""
        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtMRRNo.Text = ""
        lblMRRDate.Text = ""
        txtItemCode.Text = ""
        lblItemCode.Text = ""
        lblSupplier.Text = ""
        lblBillNo.Text = ""
        lblBillDate.Text = ""

        lblHeatNo.Text = ""
        chkCall.CheckState = System.Windows.Forms.CheckState.Unchecked
        cboStatus.SelectedIndex = 0
        txtReceivedQty.Text = ""
        txtAcceptedQty.Text = ""
        txtRejectedQty.Text = ""
        chkDeviation.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSegregation.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkRejected.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkRework.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkReplyRecv.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkReplyRecv_CheckStateChanged(chkReplyRecv, New System.EventArgs())
        txtReplyDate.Text = ""
        chkSatisfy.CheckState = System.Windows.Forms.CheckState.Unchecked
        cboReworkBy.SelectedIndex = 0
        txtDescription.Text = ""
        cboUnderstood.SelectedIndex = 0
        cboCause.SelectedIndex = 0
        txtMan.Text = ""
        txtMethod.Text = ""
        txtMaterial.Text = ""
        txtMachine.Text = ""
        txtMeasure.Text = ""
        txtCorrAction.Text = ""
        cboAdded.SelectedIndex = 0
        cboInspection.SelectedIndex = 0
        txtCorrMeasure.Text = ""
        txtRemarks.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsFlashMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

            .Col = ColProblem
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsFlashDetail.Fields("PROBLEM_OBSR").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColDefect
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsFlashDetail.Fields("DEFECT_PER").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True


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
            .set_ColWidth(3, 500 * 3)
            .set_ColWidth(4, 500 * 3)
            .set_ColWidth(5, 500 * 5)
            .set_ColWidth(6, 500 * 3)
            .set_ColWidth(7, 500 * 3)
            .set_ColWidth(8, 500 * 3)
            .set_ColWidth(9, 500 * 3)
            .set_ColWidth(10, 500 * 3)
            .set_ColWidth(11, 500 * 3)
            .set_ColWidth(12, 500 * 3)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.Maxlength = RsFlashMain.Fields("AUTO_KEY_FLASH").Precision
        txtDate.Maxlength = RsFlashMain.Fields("FLASH_RPT_DATE").DefinedSize - 6
        txtMRRNo.Maxlength = RsFlashMain.Fields("AUTO_KEY_MRR").DefinedSize
        txtItemCode.Maxlength = RsFlashMain.Fields("ITEM_CODE").DefinedSize
        txtReceivedQty.Maxlength = RsFlashMain.Fields("REC_QTY").Precision
        txtAcceptedQty.Maxlength = RsFlashMain.Fields("ACP_QTY").Precision
        txtRejectedQty.Maxlength = RsFlashMain.Fields("REJ_QTY").Precision
        txtDescription.Maxlength = RsFlashMain.Fields("PROBLEM_DESC").DefinedSize
        txtMan.Maxlength = RsFlashMain.Fields("MAN_DEFECT").DefinedSize
        txtMethod.Maxlength = RsFlashMain.Fields("METHOD_DEFECT").DefinedSize
        txtMaterial.Maxlength = RsFlashMain.Fields("MATERIAL_DEFECT").DefinedSize
        txtMachine.Maxlength = RsFlashMain.Fields("MACHINE_DEFECT").DefinedSize
        txtMeasure.Maxlength = RsFlashMain.Fields("MEASURE_DEFECT").DefinedSize
        txtCorrAction.Maxlength = RsFlashMain.Fields("CORR_ACTION").DefinedSize
        txtCorrMeasure.Maxlength = RsFlashMain.Fields("CUST_CORR_DESC").DefinedSize
        txtRemarks.Maxlength = RsFlashMain.Fields("REMARKS").DefinedSize
        txtReplyDate.Text = CStr(RsFlashMain.Fields("REPLY_RECV_DATE").DefinedSize - 6)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim ii As Integer
        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsFlashMain.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Report Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtMRRNo.Text) = "" Then
            MsgInformation("MRR Number is empty, So unable to save.")
            txtMRRNo.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtItemCode.Text) = "" Then
            MsgInformation("Item Code is empty, So unable to save.")
            txtItemCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtReceivedQty.Text) = 0 Then
            MsgInformation("Received Quantity is empty, So unable to save.")
            txtReceivedQty.Focus()
            FieldsVarification = False
            Exit Function
        End If
        '    If Val(txtAcceptedQty.Text) = 0 Then
        '        MsgInformation "Accepted Quantity is empty, So unable to save."
        '        txtAcceptedQty.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        '    If Val(txtRejectedQty.Text) = 0 Then
        '        MsgInformation "Rejected Quantity is empty, So unable to save."
        '        txtRejectedQty.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        If chkReplyRecv.CheckState = System.Windows.Forms.CheckState.Checked Then
            If Trim(txtReplyDate.Text) = "" Then
                MsgInformation("Reply date is empty, So unable to save.")
                txtReplyDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColProblem, "S", "Please Check Problem Observation.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume
    End Function

    Private Sub frmQualityFlash_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsFlashMain.Close()
        RsFlashMain = Nothing
        RsFlashDetail.Close()
        RsFlashDetail = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String
        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColProblem)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart

        If eventArgs.NewRow = -1 Then Exit Sub


        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColProblem
        If Trim(SprdMain.Text) = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColProblem
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColProblem
                If Trim(SprdMain.Text) = "" Then Exit Sub
                MainClass.AddBlankSprdRow(SprdMain, ColProblem, ConRowHeight)
                FormatSprdMain((SprdMain.MaxRows))
        End Select
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
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
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

    Private Sub txtCorrAction_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCorrAction.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCorrMeasure_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCorrMeasure.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDescription_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDescription.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.DoubleClick
        Call CmdSearchItemCode_Click(CmdSearchItemCode, New System.EventArgs())
    End Sub

    Private Sub txtItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchItemCode_Click(CmdSearchItemCode, New System.EventArgs())
    End Sub

    Private Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtItemCode.Text) = "" Then GoTo EventExitSub

        SqlStr = "SELECT A.ITEM_SHORT_DESC,B.ITEM_CODE,B.RECEIVED_QTY,B.APPROVED_QTY,B.REJECTED_QTY, " & vbCrLf _
                    & " DECODE(LOT_ACCEPT_DEV,0,'No','Yes') AS DEV,DECODE(LOT_ACC_SEG,0,'No','Yes') AS SEG, " & vbCrLf _
                    & " DECODE(REJECTED_QTY,0,'No','Yes') AS REJ,DECODE(LOT_ACC_RWK,0,'No','Yes') AS REW " & vbCrLf _
                    & " FROM INV_GATE_DET B, INV_ITEM_MST A " & vbCrLf _
                    & " WHERE B.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf _
                    & " AND B.ITEM_CODE = A.ITEM_CODE " & vbCrLf _
                    & " AND B.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND B.ITEM_CODE = '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "' " & vbCrLf _
                    & " AND B.AUTO_KEY_MRR ='" & MainClass.AllowSingleQuote(txtMRRNo.Text) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                txtItemCode.Text = IIf(IsDbNull(mRsTemp.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                lblItemCode.Text = IIf(IsDbNull(mRsTemp.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)
                txtReceivedQty.Text = IIf(IsDbNull(mRsTemp.Fields("RECEIVED_QTY").Value), "", .Fields("RECEIVED_QTY").Value)
                txtAcceptedQty.Text = IIf(IsDbNull(mRsTemp.Fields("APPROVED_QTY").Value), "", .Fields("APPROVED_QTY").Value)
                txtRejectedQty.Text = IIf(IsDbNull(mRsTemp.Fields("REJECTED_QTY").Value), "", .Fields("REJECTED_QTY").Value)
                chkDeviation.CheckState = IIf(IsDbNull(mRsTemp.Fields("DEV").Value) Or mRsTemp.Fields("DEV").Value = "No", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                chkSegregation.CheckState = IIf(IsDbNull(mRsTemp.Fields("SEG").Value) Or mRsTemp.Fields("SEG").Value = "No", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                chkRejected.CheckState = IIf(IsDbNull(mRsTemp.Fields("REJ").Value) Or mRsTemp.Fields("REJ").Value = "No", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                chkRework.CheckState = IIf(IsDbNull(mRsTemp.Fields("REW").Value) Or mRsTemp.Fields("REW").Value = "No", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            Else
                MsgBox("Not a valid MRRNo's Item.")
                lblItemCode.Text = ""
                txtReceivedQty.Text = ""
                txtAcceptedQty.Text = ""
                txtRejectedQty.Text = ""
                chkDeviation.CheckState = System.Windows.Forms.CheckState.Unchecked
                chkSegregation.CheckState = System.Windows.Forms.CheckState.Unchecked
                chkRejected.CheckState = System.Windows.Forms.CheckState.Unchecked
                chkRework.CheckState = System.Windows.Forms.CheckState.Unchecked
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtmachine_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachine.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMaterial_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMaterial.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMeasure_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMeasure.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMethod_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMethod.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
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
        txtItemCode.Focus()
    End Sub

    Private Sub txtMRRNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtMRRNo.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT A.AUTO_KEY_MRR,A.MRR_DATE,A.BILL_NO, A.BILL_DATE,B.SUPP_CUST_NAME " & vbCrLf _
                    & " FROM INV_GATE_HDR A, FIN_SUPP_CUST_MST B " & vbCrLf _
                    & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _
                    & " AND A.SUPP_CUST_CODE = B.SUPP_CUST_CODE " & vbCrLf _
                    & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND A.AUTO_KEY_MRR = '" & MainClass.AllowSingleQuote(txtMRRNo.Text) & "'  "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                txtMRRNo.Text = IIf(IsDbNull(mRsTemp.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                lblMRRDate.Text = IIf(IsDbNull(mRsTemp.Fields("MRR_DATE").Value), "", .Fields("MRR_DATE").Value)
                lblSupplier.Text = IIf(IsDbNull(mRsTemp.Fields("SUPP_CUST_NAME").Value), "", .Fields("SUPP_CUST_NAME").Value)
                lblBillNo.Text = IIf(IsDbNull(mRsTemp.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                lblBillDate.Text = IIf(IsDbNull(mRsTemp.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value)
            Else
                MsgBox("Not a valid Customer")
                lblMRRDate.Text = ""
                lblSupplier.Text = ""
                lblBillNo.Text = ""
                lblBillDate.Text = ""
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

    Private Sub txtReplyDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReplyDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReplyDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReplyDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtReplyDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtReplyDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        Else
            If CDate(txtReplyDate.Text) < CDate(txtDate.Text) Then
                MsgBox("Reply Date Cann't Be Less Than Date ")
                Cancel = True
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mItemCode As String
        Dim mSuppCode As String


        If Not RsFlashMain.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsFlashMain.Fields("AUTO_KEY_FLASH").Value), "", RsFlashMain.Fields("AUTO_KEY_FLASH").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsFlashMain.Fields("AUTO_KEY_FLASH").Value), "", RsFlashMain.Fields("AUTO_KEY_FLASH").Value)
            txtDate.Text = IIf(IsDbNull(RsFlashMain.Fields("FLASH_RPT_DATE").Value), "", RsFlashMain.Fields("FLASH_RPT_DATE").Value)
            txtMRRNo.Text = IIf(IsDbNull(RsFlashMain.Fields("AUTO_KEY_MRR").Value), "", RsFlashMain.Fields("AUTO_KEY_MRR").Value)
            lblMRRDate.Text = IIf(IsDbNull(RsFlashMain.Fields("MRR_DATE").Value), "", RsFlashMain.Fields("MRR_DATE").Value)
            txtItemCode.Text = IIf(IsDbNull(RsFlashMain.Fields("ITEM_CODE").Value), "", RsFlashMain.Fields("ITEM_CODE").Value)

            mItemCode = txtItemCode.Text
            If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                lblItemCode.text = MasterNo
            Else
                lblItemCode.text = ""
            End If

            If MainClass.ValidateWithMasterTable(txtMRRNo.Text, "AUTO_KEY_MRR", "HEAT_NO", "INV_GATE_DET", PubDBCn, MasterNo, , "COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'") = True Then
                lblHeatNo.text = MasterNo
            Else
                lblHeatNo.text = ""
            End If


            mSuppCode = IIf(IsDbNull(RsFlashMain.Fields("SUPP_CUST_CODE").Value), "", RsFlashMain.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mSuppCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                lblSupplier.text = MasterNo
            Else
                lblSupplier.text = ""
            End If

            lblBillNo.Text = IIf(IsDbNull(RsFlashMain.Fields("BILL_NO").Value), "", RsFlashMain.Fields("BILL_NO").Value)
            lblBillDate.Text = IIf(IsDbNull(RsFlashMain.Fields("BILL_DATE").Value), "", RsFlashMain.Fields("BILL_DATE").Value)
            chkCall.CheckState = IIf(IsDbNull(RsFlashMain.Fields("CALL_FLAG").Value) Or RsFlashMain.Fields("CALL_FLAG").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            Select Case IIf(IsDbNull(RsFlashMain.Fields("PROBLEM_STATUS").Value), "", RsFlashMain.Fields("PROBLEM_STATUS").Value)
                Case "N"
                    cboStatus.Text = "New"
                Case "R"
                    cboStatus.Text = "Repeated"
                Case "I"
                    cboStatus.Text = "Repeated After Improvement"
            End Select
            txtReceivedQty.Text = IIf(IsDbNull(RsFlashMain.Fields("REC_QTY").Value), "", RsFlashMain.Fields("REC_QTY").Value)
            txtAcceptedQty.Text = IIf(IsDbNull(RsFlashMain.Fields("ACP_QTY").Value), "", RsFlashMain.Fields("ACP_QTY").Value)
            txtRejectedQty.Text = IIf(IsDbNull(RsFlashMain.Fields("REJ_QTY").Value), "", RsFlashMain.Fields("REJ_QTY").Value)
            chkDeviation.CheckState = IIf(IsDbNull(RsFlashMain.Fields("ACP_DEV_FLAG").Value) Or RsFlashMain.Fields("ACP_DEV_FLAG").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            chkSegregation.CheckState = IIf(IsDbNull(RsFlashMain.Fields("SEG_FLAG").Value) Or RsFlashMain.Fields("SEG_FLAG").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            chkRejected.CheckState = IIf(IsDbNull(RsFlashMain.Fields("REJ_FLAG").Value) Or RsFlashMain.Fields("REJ_FLAG").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            chkRework.CheckState = IIf(IsDbNull(RsFlashMain.Fields("REWORK_FLAG").Value) Or RsFlashMain.Fields("REWORK_FLAG").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            txtDescription.Text = IIf(IsDbNull(RsFlashMain.Fields("PROBLEM_DESC").Value), "", RsFlashMain.Fields("PROBLEM_DESC").Value)
            cboUnderstood.Text = IIf(IsDbNull(RsFlashMain.Fields("UNDERSTOOD_FLAG").Value) Or RsFlashMain.Fields("UNDERSTOOD_FLAG").Value = "N", "No", "Yes")
            cboCause.Text = IIf(IsDbNull(RsFlashMain.Fields("CAUSE_FLAG").Value) Or RsFlashMain.Fields("CAUSE_FLAG").Value = "N", "No", "Yes")
            txtMan.Text = IIf(IsDbNull(RsFlashMain.Fields("MAN_DEFECT").Value), "", RsFlashMain.Fields("MAN_DEFECT").Value)
            txtMethod.Text = IIf(IsDbNull(RsFlashMain.Fields("METHOD_DEFECT").Value), "", RsFlashMain.Fields("METHOD_DEFECT").Value)
            txtMaterial.Text = IIf(IsDbNull(RsFlashMain.Fields("MATERIAL_DEFECT").Value), "", RsFlashMain.Fields("MATERIAL_DEFECT").Value)
            txtMachine.Text = IIf(IsDbNull(RsFlashMain.Fields("MACHINE_DEFECT").Value), "", RsFlashMain.Fields("MACHINE_DEFECT").Value)
            txtMeasure.Text = IIf(IsDbNull(RsFlashMain.Fields("MEASURE_DEFECT").Value), "", RsFlashMain.Fields("MEASURE_DEFECT").Value)
            txtCorrAction.Text = IIf(IsDbNull(RsFlashMain.Fields("CORR_ACTION").Value), "", RsFlashMain.Fields("CORR_ACTION").Value)
            cboAdded.Text = IIf(IsDbNull(RsFlashMain.Fields("ADDED_FLAG").Value) Or RsFlashMain.Fields("ADDED_FLAG").Value = "N", "No", "Yes")
            cboInspection.Text = IIf(IsDbNull(RsFlashMain.Fields("INSP_RPT_START_FLAG").Value) Or RsFlashMain.Fields("INSP_RPT_START_FLAG").Value = "N", "No", "Yes")
            txtCorrMeasure.Text = IIf(IsDbNull(RsFlashMain.Fields("CUST_CORR_DESC").Value), "", RsFlashMain.Fields("CUST_CORR_DESC").Value)
            txtRemarks.Text = IIf(IsDbNull(RsFlashMain.Fields("REMARKS").Value), "", RsFlashMain.Fields("REMARKS").Value)

            chkReplyRecv.CheckState = IIf(IsDbNull(RsFlashMain.Fields("REPLY_RECV").Value) Or RsFlashMain.Fields("REPLY_RECV").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            txtReplyDate.Text = IIf(IsDbNull(RsFlashMain.Fields("REPLY_RECV_DATE").Value), "", RsFlashMain.Fields("REPLY_RECV_DATE").Value)
            chkSatisfy.CheckState = IIf(IsDbNull(RsFlashMain.Fields("SATISFY_REPLY").Value) Or RsFlashMain.Fields("SATISFY_REPLY").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            Select Case IIf(IsDbNull(RsFlashMain.Fields("REWORK_BY").Value), "", RsFlashMain.Fields("REWORK_BY").Value)
                Case "C"
                    cboReworkBy.Text = "Own Company"
                Case "O"
                    cboReworkBy.Text = "Other"
            End Select

            Call txtItemCode_Validating(txtItemCode, New System.ComponentModel.CancelEventArgs(False))
            Call ShowDetail1()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsFlashMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_FLASH_DET " & vbCrLf & " WHERE AUTO_KEY_FLASH=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFlashDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsFlashDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColProblem
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PROBLEM_OBSR").Value), "", .Fields("PROBLEM_OBSR").Value))

                SprdMain.Col = ColDefect
                SprdMain.Text = IIf(IsDbNull(.Fields("DEFECT_PER").Value), "", .Fields("DEFECT_PER").Value)

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
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
    Private Sub txtSlipNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSlipNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String


        If Trim(txtSlipNo.Text) = "" Then GoTo EventExitSub
        mSlipNo = Val(txtSlipNo.Text)

        If MODIFYMode = True And RsFlashMain.BOF = False Then xMkey = RsFlashMain.Fields("AUTO_KEY_FLASH").Value

        SqlStr = "SELECT * FROM QAL_FLASH_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_FLASH,LENGTH(AUTO_KEY_FLASH)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_FLASH=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFlashMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsFlashMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_FLASH_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_FLASH,LENGTH(AUTO_KEY_FLASH)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_FLASH=" & Val(CStr(xMkey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFlashMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        txtMRRNo.Enabled = mMode
        CmdSearchMRRNo.Enabled = mMode
        txtItemCode.Enabled = mMode
        cmdSearchItemCode.Enabled = mMode
        txtReceivedQty.Enabled = False ' mMode
        txtAcceptedQty.Enabled = False 'mMode
        txtRejectedQty.Enabled = False 'mMode
        chkDeviation.Enabled = False ' mMode
        chkSegregation.Enabled = False 'mMode
        chkRejected.Enabled = False 'mMode
        chkRework.Enabled = False 'mMode
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
    Private Sub ReportOnFlash(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "QUALITY FLASH REPORT"
        SqlStr = "SELECT QAL_FLASH_HDR.*,QAL_FLASH_DET.*, " & vbCrLf & " INV_ITEM_MST.*,FIN_SUPP_CUST_MST.* " & vbCrLf & " FROM QAL_FLASH_HDR,QAL_FLASH_DET,  " & vbCrLf & " INV_ITEM_MST ,FIN_SUPP_CUST_MST  " & vbCrLf & " WHERE QAL_FLASH_HDR.AUTO_KEY_FLASH=QAL_FLASH_DET.AUTO_KEY_FLASH (+)" & vbCrLf & " AND QAL_FLASH_HDR.COMPANY_CODE=INV_ITEM_MST.COMPANY_CODE (+) " & vbCrLf & " AND QAL_FLASH_HDR.ITEM_CODE=INV_ITEM_MST.ITEM_CODE (+) " & vbCrLf & " AND QAL_FLASH_HDR.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE (+) " & vbCrLf & " AND QAL_FLASH_HDR.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE (+) " & vbCrLf & " AND QAL_FLASH_HDR.AUTO_KEY_FLASH=" & Val(lblMkey.Text) & ""

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\QualityFlash.rpt"

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
        Call ReportOnFlash(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnFlash(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
