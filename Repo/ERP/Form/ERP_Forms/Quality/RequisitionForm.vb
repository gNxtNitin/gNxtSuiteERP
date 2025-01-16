Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmRequisitionForm
    Inherits System.Windows.Forms.Form
    Dim RsRequisitionHdr As ADODB.Recordset
    Dim RsRequisitionNewDet As ADODB.Recordset
    Dim RsRequisitionRepairDet As ADODB.Recordset
    Dim RsRequisitionSampleDet As ADODB.Recordset
    Dim RsRequisitionActionDet As ADODB.Recordset

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean
    Private Const ConRowHeight As Short = 14

    Dim xMyMenu As String

    Private Const ColNewName As Short = 1
    Private Const ColNewMake As Short = 2
    Private Const ColNewRange As Short = 3
    Private Const ColNewLeastCount As Short = 4
    Private Const ColNewLocation As Short = 5
    Private Const ColNewUsageFreq As Short = 6
    Private Const ColNewQty As Short = 7
    Private Const ColNewCalibFreq As Short = 8

    Private Const ColRepairProblem As Short = 1
    Private Const ColRepairRootCause As Short = 2
    Private Const ColRepairPermanentAction As Short = 3
    Private Const ColRepairTargetDate As Short = 4

    Private Const ColSampleDescription As Short = 1
    Private Const ColSampleQty As Short = 2
    Private Const ColSampleTestStandard As Short = 3
    Private Const ColSampleReference As Short = 4
    Private Const ColSampleRemarks As Short = 5

    Private Const ColActionInstNo As Short = 1
    Private Const ColActionDescription As Short = 2
    Private Const ColActionDueDate As Short = 3
    Private Const ColActionStatus As Short = 4
    Private Const ColActionCalibFrom As Short = 5
    Private Const ColActionHandoverDept As Short = 6

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtReqNo.Enabled = False
            cmdSearchReqNo.Enabled = False
            Call ShowFrames()
            If fraNew.Visible = True Then
                SprdNew.Enabled = True
            ElseIf fraRepair.Visible = True Then
                SprdRepair.Enabled = True
            ElseIf fraSample.Visible = True Then
                SprdSample.Enabled = True
            End If
            SprdAction.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsRequisitionHdr.EOF = False Then RsRequisitionHdr.MoveFirst()
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub ShowFrames()
        If cboReqType.Text = "New Instrument" Then
            fraNew.Visible = True
            If ADDMode = True Or MODIFYMode = True Then SprdNew.Enabled = True
            fraRepair.Visible = False
            fraSample.Visible = False
            fraAction1.Visible = True
            If lblFormType.Text = "A" Then fraAction1.Enabled = True
            fraRequest.Height = VB6.TwipsToPixelsY(3960)
        ElseIf cboReqType.Text = "Repair" Then
            fraNew.Visible = False
            fraRepair.Visible = True
            If ADDMode = True Or MODIFYMode = True Then SprdRepair.Enabled = True
            fraSample.Visible = False
            fraAction1.Visible = True
            If lblFormType.Text = "A" Then fraAction1.Enabled = True
            fraRepair.Text = "Request for Reapir of Instruments"
            fraRepair.Height = VB6.TwipsToPixelsY(2055)
            SprdRepair.Height = VB6.TwipsToPixelsY(1725)
            fraRepair.Top = VB6.TwipsToPixelsY(1920)
            fraRequest.Height = VB6.TwipsToPixelsY(3960)
        ElseIf cboReqType.Text = "Sample" Then
            fraNew.Visible = False
            fraRepair.Visible = False
            fraSample.Visible = True
            If ADDMode = True Or MODIFYMode = True Then SprdSample.Enabled = True
            If cboPreviousFailure.Text = "Yes" Then
                fraAction1.Visible = False
                If lblFormType.Text = "R" And ADDMode = True Or MODIFYMode = True Then SprdRepair.Enabled = True
                fraRepair.Visible = True
                fraRepair.Text = "In case of Previous Sample Failure"
                SprdRepair.Height = VB6.TwipsToPixelsY(1245)
                fraRepair.Top = VB6.TwipsToPixelsY(3930)
                fraRepair.Height = VB6.TwipsToPixelsY(1545)
                fraRequest.Height = VB6.TwipsToPixelsY(5520)
            ElseIf cboPreviousFailure.Text = "No" Then
                fraAction1.Visible = True
                fraRepair.Visible = False
                fraRequest.Height = VB6.TwipsToPixelsY(3960)
            End If
            fraAction1.Enabled = False
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtReqNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsRequisitionHdr.EOF Then
            If PubSuperUser = "U" Then
                If RsRequisitionHdr.Fields("REQ_STATUS").Value = "H" Or RsRequisitionHdr.Fields("REQ_STATUS").Value = "N" Then MsgBox("Requisition has been Completed, So cann't be Deleted ") : Exit Sub
            End If
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_REQUISITION_HDR", (txtReqNo.Text), RsRequisitionHdr) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_REQUISITION_ACTION_DET WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_REQUISITION_SAMPLE_DET WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_REQUISITION_REPAIR_DET WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_REQUISITION_NEW_DET WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_REQUISITION_HDR WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsRequisitionHdr.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsRequisitionHdr.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            If PubSuperUser = "U" Then
                If RsRequisitionHdr.Fields("REQ_STATUS").Value = "H" Or RsRequisitionHdr.Fields("REQ_STATUS").Value = "N" Then MsgBox("Requisition has been Completed, So cann't be Modified ") : Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsRequisitionHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtReqNo.Enabled = False
            cmdSearchReqNo.Enabled = False
            Call ShowFrames()
            If fraNew.Visible = True Then
                SprdNew.Enabled = True
            ElseIf fraRepair.Visible = True Then
                SprdRepair.Enabled = True
            ElseIf fraSample.Visible = True Then
                SprdSample.Enabled = True
            End If
            SprdAction.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
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
            txtReqNo_Validating(txtReqNo, New System.ComponentModel.CancelEventArgs(False))
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
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        IsRecordExist = False
        If MODIFYMode = True Then Exit Function
        SqlStr = " SELECT AUTO_KEY_REQ " & vbCrLf & " From QAL_REQUISITION_HDR " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_REQ = " & Val(lblMkey.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_REQ").Value)
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
        Dim mRsTemp As ADODB.Recordset
        Dim mReqType As String
        Dim mUrgency As String
        Dim mPreviousFailure As String
        Dim mReqStatus As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        PubDBCn.Execute("DELETE FROM QAL_REQUISITION_ACTION_DET WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & "")
        PubDBCn.Execute("DELETE FROM QAL_REQUISITION_SAMPLE_DET WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & "")
        PubDBCn.Execute("DELETE FROM QAL_REQUISITION_REPAIR_DET WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & "")
        PubDBCn.Execute("DELETE FROM QAL_REQUISITION_NEW_DET WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & "")

        SqlStr = ""
        mSlipNo = Val(txtReqNo.Text)
        If Val(txtReqNo.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtReqNo.Text = CStr(mSlipNo)

        mReqType = VB.Left(cboReqType.Text, 1)
        mUrgency = VB.Left(cboUrgency.Text, 1)
        mPreviousFailure = VB.Left(cboPreviousFailure.Text, 1)
        mReqStatus = VB.Left(cboReqStatus.Text, 1)

        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_REQUISITION_HDR " & vbCrLf _
                            & " (AUTO_KEY_REQ,COMPANY_CODE, " & vbCrLf _
                            & " REQ_DATE,REQ_TYPE,DEPT_CODE, " & vbCrLf _
                            & " CUSTOMER,URGENCY,URGENCY_REASON,PREVIOUS_FAILURE, " & vbCrLf _
                            & " ACTION_DATE,REQ_STATUS,STATUS_REASON, " & vbCrLf _
                            & " REMARKS,REQ_BY,APP_BY , " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mReqType & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "','" & MainClass.AllowSingleQuote(txtCustomer.Text) & "', " & vbCrLf _
                            & " '" & mUrgency & "','" & MainClass.AllowSingleQuote(txtUrgencyReason.Text) & "','" & mPreviousFailure & "', " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtActionDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mReqStatus & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtStatusReason.Text) & "','" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtReqBy.Text) & "','" & MainClass.AllowSingleQuote(txtAppBy.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_REQUISITION_HDR SET " & vbCrLf _
                    & " AUTO_KEY_REQ=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                    & " REQ_DATE=TO_DATE('" & vb6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " REQ_TYPE='" & mReqType & "', " & vbCrLf _
                    & " DEPT_CODE='" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "', " & vbCrLf _
                    & " CUSTOMER='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "', " & vbCrLf _
                    & " URGENCY='" & mUrgency & "', " & vbCrLf _
                    & " PREVIOUS_FAILURE='" & mPreviousFailure & "', " & vbCrLf _
                    & " URGENCY_REASON='" & MainClass.AllowSingleQuote(txtUrgencyReason.Text) & "', " & vbCrLf _
                    & " ACTION_DATE=TO_DATE('" & vb6.Format(txtActionDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " REQ_STATUS='" & mReqStatus & "', " & vbCrLf _
                    & " STATUS_REASON='" & MainClass.AllowSingleQuote(txtStatusReason.Text) & "', " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " REQ_BY='" & MainClass.AllowSingleQuote(txtReqBy.Text) & "', " & vbCrLf _
                    & " APP_BY='" & MainClass.AllowSingleQuote(txtAppBy.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND AUTO_KEY_REQ =" & Val(lblMkey.text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail = False Then GoTo ErrPart

        Update1 = True
        PubDBCn.CommitTrans()
        txtReqNo.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsRequisitionHdr.Requery()
        RsRequisitionNewDet.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_REQ)  " & vbCrLf & " FROM QAL_REQUISITION_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REQ,LENGTH(AUTO_KEY_REQ)-5,4)=" & RsCompany.Fields("FYEAR").Value & " "

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
        On Error GoTo ErrPart
        If cboReqType.Text = "New Instrument" Then
            If UpdateNewDetail = False Then GoTo ErrPart
            If UpdateActionDetail = False Then GoTo ErrPart
        ElseIf cboReqType.Text = "Repair" Then
            If UpdateRepairDetail = False Then GoTo ErrPart
            If UpdateActionDetail = False Then GoTo ErrPart
        ElseIf cboReqType.Text = "Sample" Then
            If UpdateSampleDetail = False Then GoTo ErrPart
            If cboPreviousFailure.Text = "Yes" Then
                If UpdateRepairDetail = False Then GoTo ErrPart
            End If
        End If
        UpdateDetail = True
        Exit Function
ErrPart:
        UpdateDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateNewDetail() As Boolean

        On Error GoTo UpdateNewDetailERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mName As String
        Dim mMake As String
        Dim mRange As String
        Dim mLeastCount As String
        Dim mLocation As String
        Dim mUsageFreq As Integer
        Dim mQty As Integer
        Dim mCalibFreq As Integer

        PubDBCn.Execute("DELETE FROM QAL_REQUISITION_NEW_DET WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & "")

        With SprdNew
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColNewName
                mName = MainClass.AllowSingleQuote(.Text)

                .Col = ColNewMake
                mMake = MainClass.AllowSingleQuote(.Text)

                .Col = ColNewRange
                mRange = MainClass.AllowSingleQuote(.Text)

                .Col = ColNewLeastCount
                mLeastCount = MainClass.AllowSingleQuote(.Text)

                .Col = ColNewLocation
                mLocation = MainClass.AllowSingleQuote(.Text)

                .Col = ColNewUsageFreq
                mUsageFreq = Val(.Text)

                .Col = ColNewQty
                mQty = Val(.Text)

                .Col = ColNewCalibFreq
                mCalibFreq = Val(.Text)

                SqlStr = ""

                If mName <> "" Then
                    SqlStr = " INSERT INTO  QAL_REQUISITION_NEW_DET ( " & vbCrLf & " AUTO_KEY_REQ,SERIAL_NO,NAME,MAKE,RANGE, " & vbCrLf & " LEAST_COUNT,LOCATION,USAGE_FREQ,QTY,CALIB_FREQ " & vbCrLf & " ) VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mName & "','" & mMake & "', " & vbCrLf & " '" & mRange & "','" & mLeastCount & "','" & mLocation & "'," & mUsageFreq & ", " & vbCrLf & " " & mQty & "," & mCalibFreq & ") "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateNewDetail = True
        Exit Function
UpdateNewDetailERR:
        UpdateNewDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateRepairDetail() As Boolean

        On Error GoTo UpdateRepairDetailERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mProblem As String
        Dim mRootCause As String
        Dim mPermanentAction As String
        Dim mTargetDate As String

        PubDBCn.Execute("DELETE FROM QAL_REQUISITION_REPAIR_DET WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & "")

        With SprdRepair
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColRepairProblem
                mProblem = MainClass.AllowSingleQuote(.Text)

                .Col = ColRepairRootCause
                mRootCause = MainClass.AllowSingleQuote(.Text)

                .Col = ColRepairPermanentAction
                mPermanentAction = MainClass.AllowSingleQuote(.Text)

                .Col = ColRepairTargetDate
                mTargetDate = VB6.Format(.Text, "DD/MM/YYYY")

                SqlStr = ""

                If mProblem <> "" Then
                    SqlStr = " INSERT INTO  QAL_REQUISITION_REPAIR_DET ( " & vbCrLf & " AUTO_KEY_REQ,SERIAL_NO,PROBLEM, " & vbCrLf & " ROOT_CAUSE,PERMANENT_ACTION,TARGET_DATE " & vbCrLf & " ) VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mProblem & "','" & mRootCause & "', " & vbCrLf & " '" & mPermanentAction & "',TO_DATE('" & VB6.Format(mTargetDate, "DD-MMMM-YYYY") & "','DD-MON-YYYY')) "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateRepairDetail = True
        Exit Function
UpdateRepairDetailERR:
        UpdateRepairDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateSampleDetail() As Boolean

        On Error GoTo UpdateSampleDetailERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mDescription As String
        Dim mQty As Integer
        Dim mTestStandard As String
        Dim mReference As String
        Dim mRemarks As String

        PubDBCn.Execute("DELETE FROM QAL_REQUISITION_SAMPLE_DET WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & "")

        With SprdSample
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColSampleDescription
                mDescription = MainClass.AllowSingleQuote(.Text)

                .Col = ColSampleQty
                mQty = Val(.Text)

                .Col = ColSampleTestStandard
                mTestStandard = MainClass.AllowSingleQuote(.Text)

                .Col = ColSampleReference
                mReference = MainClass.AllowSingleQuote(.Text)

                .Col = ColSampleRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mDescription <> "" Then
                    SqlStr = " INSERT INTO  QAL_REQUISITION_SAMPLE_DET ( " & vbCrLf & " AUTO_KEY_REQ,SERIAL_NO,DESCRIPTION,QTY, " & vbCrLf & " TEST_STANDARD,REFERENCE,REMARKS " & vbCrLf & " ) VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mDescription & "'," & mQty & ", " & vbCrLf & " '" & mTestStandard & "','" & mReference & "','" & mRemarks & "') "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateSampleDetail = True
        Exit Function
UpdateSampleDetailERR:
        UpdateSampleDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateActionDetail() As Boolean

        On Error GoTo UpdateActionDetailDetailERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mInstNo As String
        Dim mDescription As String
        Dim mDueDate As String
        Dim mStatus As String
        Dim mCalibFrom As String
        Dim mHandoverDept As String

        PubDBCn.Execute("DELETE FROM QAL_REQUISITION_ACTION_DET WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & "")

        With SprdAction
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColActionInstNo
                mInstNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColActionDescription
                mDescription = MainClass.AllowSingleQuote(.Text)

                .Col = ColActionDueDate
                mDueDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColActionStatus
                mStatus = MainClass.AllowSingleQuote(.Text)

                .Col = ColActionCalibFrom
                mCalibFrom = VB.Left(.Text, 1)

                .Col = ColActionHandoverDept
                mHandoverDept = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mInstNo <> "" Then
                    SqlStr = " INSERT INTO  QAL_REQUISITION_ACTION_DET ( " & vbCrLf & " COMPANY_CODE,AUTO_KEY_REQ,SERIAL_NO,INST_NO,DESCRIPTION, " & vbCrLf & " DUE_DATE,STATUS,CALIB_FROM,HANDOVER_DEPT " & vbCrLf & " ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMkey.Text) & "," & I & ",'" & mInstNo & "','" & mDescription & "', " & vbCrLf & " TO_DATE('" & VB6.Format(mDueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mStatus & "','" & mCalibFrom & "','" & mHandoverDept & "') "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateActionDetail = True
        Exit Function
UpdateActionDetailDetailERR:
        UpdateActionDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

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
        MainClass.ButtonStatus(Me, XRIGHT, RsRequisitionHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub frmRequisitionForm_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblFormType.Text = "R" Then
            Me.Text = "Requisition Form for Laboratory (Standard Room) - Request Entry"
            fraRequest.Enabled = True
            fraAction1.Enabled = False
            fraAction2.Enabled = False
            CmdAdd.Visible = True
        ElseIf lblFormType.Text = "A" Then
            Me.Text = "Requisition Form for Laboratory (Standard Room) - Action Entry"
            fraRequest.Enabled = False
            fraAction1.Enabled = True
            fraAction2.Enabled = True
            CmdAdd.Visible = False
        End If

        SqlStr = "Select * From QAL_REQUISITION_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRequisitionHdr, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_REQUISITION_NEW_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRequisitionNewDet, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_REQUISITION_REPAIR_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRequisitionRepairDet, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_REQUISITION_SAMPLE_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRequisitionSampleDet, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_REQUISITION_ACTION_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRequisitionActionDet, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        Call Clear1()
        If lblFormType.Text = "R" Then
            If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        End If

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_REQ AS SLIP_NUMBER,TO_CHAR(REQ_DATE,'DD/MM/YYYY') AS REQ_DATE, " & vbCrLf & " DECODE(REQ_TYPE,'N','NEW','R','REPAIR','S','SAMPLE') AS REQ_TYPE,DEPT_CODE, " & vbCrLf & " DECODE(REQ_STATUS,'O','OPEN','H','HONOURED','N','NOT HONOURED') AS REQ_STATUS, " & vbCrLf & " TO_CHAR(ACTION_DATE,'DD/MM/YYYY') AS ACTION_DATE,REQ_BY,APP_BY " & vbCrLf & " FROM QAL_REQUISITION_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REQ,LENGTH(AUTO_KEY_REQ)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_REQ"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmRequisitionForm_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmRequisitionForm_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        xMyMenu = myMenu
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(8010)
        Me.Width = VB6.TwipsToPixelsX(11595)

        cboReqType.Items.Clear()
        cboReqType.Items.Add("New Instrument")
        cboReqType.Items.Add("Repair")
        cboReqType.Items.Add("Sample")
        cboReqType.SelectedIndex = 0

        cboUrgency.Items.Clear()
        cboUrgency.Items.Add("Yes")
        cboUrgency.Items.Add("No")
        cboUrgency.SelectedIndex = 0

        cboPreviousFailure.Items.Clear()
        cboPreviousFailure.Items.Add("No")
        cboPreviousFailure.Items.Add("Yes")
        cboPreviousFailure.SelectedIndex = 0

        cboReqStatus.Items.Clear()
        cboReqStatus.Items.Add("Open")
        cboReqStatus.Items.Add("Honoured")
        cboReqStatus.Items.Add("Not Honoured")
        cboReqStatus.SelectedIndex = 0

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
        txtReqNo.Text = ""
        txtReqDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        cboReqType.SelectedIndex = 0
        txtDeptCode.Text = ""
        txtDeptName.Text = ""
        txtRemarks.Text = ""
        txtReqBy.Text = ""
        txtReqName.Text = ""
        txtAppBy.Text = ""
        txtAppName.Text = ""
        txtCustomer.Text = ""
        cboUrgency.SelectedIndex = 0
        txtUrgencyReason.Text = ""
        cboPreviousFailure.SelectedIndex = 0
        txtActionDate.Text = ""
        cboReqStatus.SelectedIndex = 0
        txtStatusReason.Text = ""

        MainClass.ClearGrid(SprdNew, ConRowHeight)
        MainClass.ClearGrid(SprdRepair, ConRowHeight)
        MainClass.ClearGrid(SprdSample, ConRowHeight)
        MainClass.ClearGrid(SprdAction, ConRowHeight)
        FormatSprdNew(-1)
        FormatSprdRepair(-1)
        FormatSprdSample(-1)
        FormatSprdAction(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsRequisitionHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub FormatSprdNew(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String

        With SprdNew
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColNewName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRequisitionNewDet.Fields("NAME").DefinedSize

            .Col = ColNewMake
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRequisitionNewDet.Fields("MAKE").DefinedSize

            .Col = ColNewRange
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRequisitionNewDet.Fields("RANGE").DefinedSize

            .Col = ColNewLeastCount
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRequisitionNewDet.Fields("LEAST_COUNT").DefinedSize

            .Col = ColNewLocation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRequisitionNewDet.Fields("LOCATION").DefinedSize

            .Col = ColNewUsageFreq
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeNumberMax = CDbl("999")
            .TypeNumberMin = CDbl("-999")
            .TypeEditLen = RsRequisitionNewDet.Fields("USAGE_FREQ").Precision

            .Col = ColNewQty
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeNumberMax = CDbl("9999")
            .TypeNumberMin = CDbl("-9999")
            .TypeEditLen = RsRequisitionNewDet.Fields("QTY").Precision

            .Col = ColNewCalibFreq
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeNumberMax = CDbl("999")
            .TypeNumberMin = CDbl("-999")
            .TypeEditLen = RsRequisitionNewDet.Fields("CALIB_FREQ").Precision

            MainClass.SetSpreadColor(SprdNew, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub FormatSprdRepair(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String

        With SprdRepair
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColRepairProblem
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRequisitionRepairDet.Fields("PROBLEM").Precision

            .Col = ColRepairRootCause
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRequisitionRepairDet.Fields("ROOT_CAUSE").DefinedSize

            .Col = ColRepairPermanentAction
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRequisitionRepairDet.Fields("PERMANENT_ACTION").DefinedSize

            .Col = ColRepairTargetDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY

            MainClass.SetSpreadColor(SprdRepair, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub FormatSprdSample(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String

        With SprdSample
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColSampleDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRequisitionSampleDet.Fields("DESCRIPTION").DefinedSize

            .Col = ColSampleQty
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeNumberMax = CDbl("9999")
            .TypeNumberMin = CDbl("-9999")
            .TypeEditLen = RsRequisitionSampleDet.Fields("QTY").Precision

            .Col = ColSampleTestStandard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRequisitionSampleDet.Fields("TEST_STANDARD").DefinedSize

            .Col = ColSampleReference
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRequisitionSampleDet.Fields("REFERENCE").DefinedSize

            .Col = ColSampleRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRequisitionSampleDet.Fields("REMARKS").DefinedSize

            MainClass.SetSpreadColor(SprdSample, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub FormatSprdAction(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String

        With SprdAction
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColActionInstNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRequisitionActionDet.Fields("INST_NO").Precision

            .Col = ColActionDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRequisitionActionDet.Fields("DESCRIPTION").DefinedSize

            .Col = ColActionDueDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY

            .Col = ColActionStatus
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRequisitionActionDet.Fields("STATUS").DefinedSize

            .Col = ColActionCalibFrom
            .CellType = SS_CELL_TYPE_COMBOBOX
            .TypeComboBoxList = "Inside" & Chr(9) & "Outside" & Chr(9) & " "

            .Col = ColActionHandoverDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRequisitionActionDet.Fields("HANDOVER_DEPT").DefinedSize

            MainClass.SetSpreadColor(SprdAction, Arow)
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
            .set_ColWidth(4, 500 * 4)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 3)
            .set_ColWidth(7, 500 * 5)
            .set_ColWidth(8, 500 * 3)
            .set_ColWidth(9, 500 * 3)
            .set_ColWidth(10, 500 * 4)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtReqNo.Maxlength = RsRequisitionHdr.Fields("AUTO_KEY_REQ").Precision
        txtReqDate.Maxlength = RsRequisitionHdr.Fields("REQ_DATE").DefinedSize - 6
        txtDeptCode.Maxlength = RsRequisitionHdr.Fields("DEPT_CODE").Precision
        txtDeptName.Maxlength = 255
        txtRemarks.Maxlength = RsRequisitionHdr.Fields("REMARKS").Precision
        txtReqBy.Maxlength = RsRequisitionHdr.Fields("REQ_BY").Precision
        txtReqName.Maxlength = 255
        txtAppBy.Maxlength = RsRequisitionHdr.Fields("APP_BY").Precision
        txtAppName.Maxlength = 255
        txtCustomer.Maxlength = RsRequisitionHdr.Fields("CUSTOMER").Precision
        txtUrgencyReason.Maxlength = RsRequisitionHdr.Fields("URGENCY_REASON").DefinedSize
        txtActionDate.Maxlength = RsRequisitionHdr.Fields("ACTION_DATE").DefinedSize - 6
        txtStatusReason.Maxlength = RsRequisitionHdr.Fields("STATUS_REASON").DefinedSize

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

        If MODIFYMode = True And RsRequisitionHdr.EOF = True Then Exit Function

        If lblFormType.Text = "R" Then
            If Trim(txtReqDate.Text) = "" Then
                MsgInformation("Requisition Date is empty, So unable to save.")
                txtReqDate.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If Trim(txtDeptCode.Text) = "" Then
                MsgInformation("Requisition Department is empty, So unable to save.")
                txtDeptCode.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If cboReqType.Text = "Sample" And cboUrgency.Text = "Yes" Then
                If Trim(txtUrgencyReason.Text) = "" Then
                    MsgInformation("Reason of Urgency is empty, So unable to save.")
                    txtUrgencyReason.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
            End If

            If Trim(txtReqBy.Text) = "" Then
                MsgInformation("Requested By is empty, So unable to save.")
                txtReqBy.Focus()
                FieldsVarification = False
                Exit Function
            End If
        ElseIf lblFormType.Text = "A" Then
            If Trim(txtActionDate.Text) = "" Then
                MsgInformation("Action Date is empty, So unable to save.")
                txtActionDate.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If cboReqStatus.Text = "Not Honoured" Then
                If Trim(txtStatusReason.Text) = "" Then
                    MsgInformation("Status Reason is empty, So unable to save.")
                    txtStatusReason.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmRequisitionForm_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        RsRequisitionHdr.Close()
        RsRequisitionHdr = Nothing
        RsRequisitionNewDet.Close()
        RsRequisitionNewDet = Nothing
        RsRequisitionRepairDet.Close()
        RsRequisitionRepairDet = Nothing
        RsRequisitionSampleDet.Close()
        RsRequisitionSampleDet = Nothing
        RsRequisitionActionDet.Close()
        RsRequisitionActionDet = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdNew_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdNew.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdNew_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdNew.ClickEvent

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdNew, eventArgs.Row, ColNewName)
        End If
        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdNew_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdNew.LeaveCell

        On Error GoTo ErrPart

        If eventArgs.NewRow = -1 Then Exit Sub
        SprdNew.Row = eventArgs.row
        SprdNew.Col = ColNewName
        If Trim(SprdNew.Text) = "" Then Exit Sub
        Select Case eventArgs.col
            Case ColNewName
                SprdNew.Row = SprdNew.ActiveRow
                SprdNew.Col = ColNewName
                If Trim(SprdNew.Text) <> "" Then
                    MainClass.AddBlankSprdRow(SprdNew, ColNewName, ConRowHeight)
                    FormatSprdNew((SprdNew.MaxRows))
                End If
        End Select

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdNew_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdNew.Leave
        With SprdNew
            SprdNew_LeaveCell(SprdNew, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub SprdRepair_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdRepair.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdRepair_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdRepair.ClickEvent

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdRepair, eventArgs.Row, ColRepairProblem)
        End If
        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdRepair_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdRepair.LeaveCell

        On Error GoTo ErrPart

        If eventArgs.NewRow = -1 Then Exit Sub
        SprdRepair.Row = eventArgs.row
        SprdRepair.Col = ColRepairProblem
        If Trim(SprdRepair.Text) = "" Then Exit Sub
        Select Case eventArgs.col
            Case ColRepairProblem
                SprdRepair.Row = SprdRepair.ActiveRow
                SprdRepair.Col = ColRepairProblem
                If Trim(SprdRepair.Text) <> "" Then
                    MainClass.AddBlankSprdRow(SprdRepair, ColRepairProblem, ConRowHeight)
                    FormatSprdRepair((SprdRepair.MaxRows))
                End If
        End Select

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdRepair_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdRepair.Leave
        With SprdRepair
            SprdRepair_LeaveCell(SprdRepair, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub SprdSample_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdSample.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdSample_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdSample.ClickEvent

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdSample, eventArgs.Row, ColSampleDescription)
        End If
        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdSample_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdSample.LeaveCell

        On Error GoTo ErrPart

        If eventArgs.NewRow = -1 Then Exit Sub
        SprdSample.Row = eventArgs.row
        SprdSample.Col = ColSampleDescription
        If Trim(SprdSample.Text) = "" Then Exit Sub
        Select Case eventArgs.col
            Case ColSampleDescription
                SprdSample.Row = SprdSample.ActiveRow
                SprdSample.Col = ColSampleDescription
                If Trim(SprdSample.Text) <> "" Then
                    MainClass.AddBlankSprdRow(SprdSample, ColSampleDescription, ConRowHeight)
                    FormatSprdSample((SprdSample.MaxRows))
                End If
        End Select

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdSample_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdSample.Leave
        With SprdSample
            SprdSample_LeaveCell(SprdSample, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub SprdAction_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdAction.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdAction_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdAction.ClickEvent

        Dim SqlStr As String

        If eventArgs.Row = 0 And eventArgs.Col = ColActionHandoverDept Then
            With SprdAction
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColActionHandoverDept
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdAction, eventArgs.Row, ColActionInstNo)
        End If
        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdAction_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdAction.KeyUpEvent
        Dim mCol As Short
        mCol = SprdAction.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColActionHandoverDept Then SprdAction_ClickEvent(SprdAction, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColActionHandoverDept, 0))
    End Sub

    Private Sub SprdAction_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdAction.LeaveCell

        On Error GoTo ErrPart
        Dim mDeptCode As String

        If eventArgs.NewRow = -1 Then Exit Sub
        SprdAction.Row = eventArgs.row
        SprdAction.Col = ColActionInstNo
        If Trim(SprdAction.Text) = "" Then Exit Sub
        Select Case eventArgs.col
            Case ColActionInstNo
                SprdAction.Row = SprdAction.ActiveRow
                SprdAction.Col = ColActionInstNo
                If Trim(SprdAction.Text) <> "" Then
                    MainClass.AddBlankSprdRow(SprdAction, ColActionInstNo, ConRowHeight)
                    FormatSprdAction((SprdAction.MaxRows))
                End If
            Case ColActionHandoverDept
                SprdAction.Row = SprdAction.ActiveRow
                SprdAction.Col = ColActionHandoverDept
                mDeptCode = Trim(SprdAction.Text)
                If mDeptCode <> "" Then
                    If MainClass.ValidateWithMasterTable(mDeptCode, "DEPT_CODE", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MainClass.SetFocusToCell(SprdAction, SprdAction.ActiveRow, ColActionHandoverDept)
                    End If
                End If
        End Select

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdAction_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdAction.Leave
        With SprdAction
            SprdAction_LeaveCell(SprdAction, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        txtReqNo.Text = SprdView.Text

        txtReqNo_Validating(txtReqNo, New System.ComponentModel.CancelEventArgs(False))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart

        If Not RsRequisitionHdr.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsRequisitionHdr.Fields("AUTO_KEY_REQ").Value), "", RsRequisitionHdr.Fields("AUTO_KEY_REQ").Value)
            txtReqNo.Text = IIf(IsDbNull(RsRequisitionHdr.Fields("AUTO_KEY_REQ").Value), "", RsRequisitionHdr.Fields("AUTO_KEY_REQ").Value)
            txtReqDate.Text = IIf(IsDbNull(RsRequisitionHdr.Fields("REQ_DATE").Value), "", RsRequisitionHdr.Fields("REQ_DATE").Value)
            If RsRequisitionHdr.Fields("REQ_TYPE").Value = "N" Then
                cboReqType.Text = "New Instrument"
            ElseIf RsRequisitionHdr.Fields("REQ_TYPE").Value = "R" Then
                cboReqType.Text = "Repair"
            ElseIf RsRequisitionHdr.Fields("REQ_TYPE").Value = "S" Then
                cboReqType.Text = "Sample"
            End If
            txtDeptCode.Text = IIf(IsDbNull(RsRequisitionHdr.Fields("DEPT_CODE").Value), "", RsRequisitionHdr.Fields("DEPT_CODE").Value)
            txtDeptCode_Validating(txtDeptCode, New System.ComponentModel.CancelEventArgs(False))
            txtRemarks.Text = IIf(IsDbNull(RsRequisitionHdr.Fields("REMARKS").Value), "", RsRequisitionHdr.Fields("REMARKS").Value)
            txtReqBy.Text = IIf(IsDbNull(RsRequisitionHdr.Fields("REQ_BY").Value), "", RsRequisitionHdr.Fields("REQ_BY").Value)
            txtReqBy_Validating(txtReqBy, New System.ComponentModel.CancelEventArgs(False))
            txtAppBy.Text = IIf(IsDbNull(RsRequisitionHdr.Fields("APP_BY").Value), "", RsRequisitionHdr.Fields("APP_BY").Value)
            txtAppBy_Validating(txtAppBy, New System.ComponentModel.CancelEventArgs(False))
            txtCustomer.Text = IIf(IsDbNull(RsRequisitionHdr.Fields("Customer").Value), "", RsRequisitionHdr.Fields("Customer").Value)
            If RsRequisitionHdr.Fields("URGENCY").Value = "Y" Then
                cboUrgency.Text = "Yes"
            ElseIf RsRequisitionHdr.Fields("URGENCY").Value = "R" Then
                cboUrgency.Text = "No"
            End If
            txtUrgencyReason.Text = IIf(IsDbNull(RsRequisitionHdr.Fields("URGENCY_REASON").Value), "", RsRequisitionHdr.Fields("URGENCY_REASON").Value)
            If RsRequisitionHdr.Fields("PREVIOUS_FAILURE").Value = "Y" Then
                cboPreviousFailure.Text = "Yes"
            ElseIf RsRequisitionHdr.Fields("PREVIOUS_FAILURE").Value = "R" Then
                cboPreviousFailure.Text = "No"
            End If
            txtActionDate.Text = IIf(IsDbNull(RsRequisitionHdr.Fields("ACTION_DATE").Value), "", RsRequisitionHdr.Fields("ACTION_DATE").Value)
            If RsRequisitionHdr.Fields("REQ_STATUS").Value = "O" Then
                cboReqStatus.Text = "Open"
            ElseIf RsRequisitionHdr.Fields("REQ_STATUS").Value = "H" Then
                cboReqStatus.Text = "Honoured"
            ElseIf RsRequisitionHdr.Fields("REQ_STATUS").Value = "N" Then
                cboReqStatus.Text = "Not Honoured"
            End If
            txtStatusReason.Text = IIf(IsDbNull(RsRequisitionHdr.Fields("STATUS_REASON").Value), "", RsRequisitionHdr.Fields("STATUS_REASON").Value)
            Call ShowFrames()

            Call ShowNew()
            Call ShowRepair()
            Call ShowSample()
            Call ShowAction()
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdNew.Enabled = False
        SprdRepair.Enabled = False
        SprdSample.Enabled = False
        SprdAction.Enabled = False
        txtReqNo.Enabled = True
        cmdSearchReqNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsRequisitionHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub ShowNew()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_REQUISITION_NEW_DET " & vbCrLf & " WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & " " & vbCrLf & " ORDER BY SERIAL_NO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRequisitionNewDet, ADODB.LockTypeEnum.adLockReadOnly)
        With RsRequisitionNewDet
            If .EOF = True Then Exit Sub
            FormatSprdNew(-1)
            I = 1
            Do While Not .EOF
                SprdNew.Row = I

                SprdNew.Col = ColNewName
                SprdNew.Text = Trim(IIf(IsDbNull(.Fields("Name").Value), "", .Fields("Name").Value))

                SprdNew.Col = ColNewMake
                SprdNew.Text = Trim(IIf(IsDbNull(.Fields("MAKE").Value), "", .Fields("MAKE").Value))

                SprdNew.Col = ColNewRange
                SprdNew.Text = Trim(IIf(IsDbNull(.Fields("Range").Value), "", .Fields("Range").Value))

                SprdNew.Col = ColNewLeastCount
                SprdNew.Text = Trim(IIf(IsDbNull(.Fields("LEAST_COUNT").Value), "", .Fields("LEAST_COUNT").Value))

                SprdNew.Col = ColNewLocation
                SprdNew.Text = Trim(IIf(IsDbNull(.Fields("Location").Value), "", .Fields("Location").Value))

                SprdNew.Col = ColNewUsageFreq
                SprdNew.Text = Trim(IIf(IsDbNull(.Fields("USAGE_FREQ").Value), "", .Fields("USAGE_FREQ").Value))

                SprdNew.Col = ColNewQty
                SprdNew.Text = Trim(IIf(IsDbNull(.Fields("QTY").Value), "", .Fields("QTY").Value))

                SprdNew.Col = ColNewCalibFreq
                SprdNew.Text = Trim(IIf(IsDbNull(.Fields("CALIB_FREQ").Value), "", .Fields("CALIB_FREQ").Value))

                .MoveNext()
                I = I + 1
                SprdNew.MaxRows = I
            Loop
        End With
        FormatSprdNew(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub ShowRepair()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_REQUISITION_REPAIR_DET " & vbCrLf & " WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRequisitionRepairDet, ADODB.LockTypeEnum.adLockReadOnly)
        With RsRequisitionRepairDet
            If .EOF = True Then Exit Sub
            FormatSprdRepair(-1)
            I = 1
            Do While Not .EOF
                SprdRepair.Row = I

                SprdRepair.Col = ColRepairProblem
                SprdRepair.Text = Trim(IIf(IsDbNull(.Fields("PROBLEM").Value), "", .Fields("PROBLEM").Value))

                SprdRepair.Col = ColRepairRootCause
                SprdRepair.Text = Trim(IIf(IsDbNull(.Fields("ROOT_CAUSE").Value), "", .Fields("ROOT_CAUSE").Value))

                SprdRepair.Col = ColRepairPermanentAction
                SprdRepair.Text = Trim(IIf(IsDbNull(.Fields("PERMANENT_ACTION").Value), "", .Fields("PERMANENT_ACTION").Value))

                SprdRepair.Col = ColRepairTargetDate
                SprdRepair.Text = Trim(IIf(IsDbNull(.Fields("TARGET_DATE").Value), "", .Fields("TARGET_DATE").Value))

                .MoveNext()
                I = I + 1
                SprdRepair.MaxRows = I
            Loop
        End With
        FormatSprdRepair(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub ShowSample()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_REQUISITION_SAMPLE_DET " & vbCrLf & " WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & " " & vbCrLf & " ORDER BY SERIAL_NO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRequisitionSampleDet, ADODB.LockTypeEnum.adLockReadOnly)
        With RsRequisitionSampleDet
            If .EOF = True Then Exit Sub
            FormatSprdSample(-1)
            I = 1
            Do While Not .EOF
                SprdSample.Row = I

                SprdSample.Col = ColSampleDescription
                SprdSample.Text = Trim(IIf(IsDbNull(.Fields("Description").Value), "", .Fields("Description").Value))

                SprdSample.Col = ColSampleQty
                SprdSample.Text = Trim(IIf(IsDbNull(.Fields("QTY").Value), "", .Fields("QTY").Value))

                SprdSample.Col = ColSampleTestStandard
                SprdSample.Text = Trim(IIf(IsDbNull(.Fields("TEST_STANDARD").Value), "", .Fields("TEST_STANDARD").Value))

                SprdSample.Col = ColSampleReference
                SprdSample.Text = Trim(IIf(IsDbNull(.Fields("REFERENCE").Value), "", .Fields("REFERENCE").Value))

                SprdSample.Col = ColSampleRemarks
                SprdSample.Text = Trim(IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value))

                .MoveNext()
                I = I + 1
                SprdSample.MaxRows = I
            Loop
        End With
        FormatSprdSample(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub ShowAction()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_REQUISITION_ACTION_DET " & vbCrLf & " WHERE AUTO_KEY_REQ=" & Val(lblMkey.Text) & " " & vbCrLf & " ORDER BY SERIAL_NO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRequisitionActionDet, ADODB.LockTypeEnum.adLockReadOnly)
        With RsRequisitionActionDet
            If .EOF = True Then Exit Sub
            FormatSprdAction(-1)
            I = 1
            Do While Not .EOF
                SprdAction.Row = I

                SprdAction.Col = ColActionInstNo
                SprdAction.Text = Trim(IIf(IsDbNull(.Fields("INST_NO").Value), "", .Fields("INST_NO").Value))

                SprdAction.Col = ColActionDescription
                SprdAction.Text = Trim(IIf(IsDbNull(.Fields("Description").Value), "", .Fields("Description").Value))

                SprdAction.Col = ColActionDueDate
                SprdAction.Text = Trim(IIf(IsDbNull(.Fields("DUE_DATE").Value), "", .Fields("DUE_DATE").Value))

                SprdAction.Col = ColActionStatus
                SprdAction.Text = Trim(IIf(IsDbNull(.Fields("Status").Value), "", .Fields("Status").Value))

                SprdAction.Col = ColActionCalibFrom
                If .Fields("CALIB_FROM").Value = "I" Then
                    SprdAction.Text = "Inside"
                ElseIf .Fields("CALIB_FROM").Value = "O" Then
                    SprdAction.Text = "Outside"
                End If

                SprdAction.Col = ColActionHandoverDept
                SprdAction.Text = Trim(IIf(IsDbNull(.Fields("HANDOVER_DEPT").Value), "", .Fields("HANDOVER_DEPT").Value))

                .MoveNext()
                I = I + 1
                SprdAction.MaxRows = I
            Loop
        End With
        '    FormatSprdAction -1
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnRequisition(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnRequisition(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnRequisition(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "Requisition Form for Laboratory (Standard Room)"

        '        If cboReqType.Text = "New Instrument" Then
        '            mSubTitle = "Requisition For New Instruments"
        '
        '            SqlStr = " SELECT QAL_REQUISITION_HDR.*,QAL_REQUISITION_NEW_DET.*, " & vbCrLf _
        ''                    & " PAY_DEPT_MST.DEPT_DESC, REQ.EMP_NAME,APP.EMP_NAME " & vbCrLf _
        ''                    & " FROM QAL_REQUISITION_HDR, QAL_REQUISITION_NEW_DET, " & vbCrLf _
        ''                    & " PAY_DEPT_MST, PAY_EMPLOYEE_MST REQ, PAY_EMPLOYEE_MST APP " & vbCrLf _
        ''                    & " WHERE QAL_REQUISITION_HDR.AUTO_KEY_REQ=QAL_REQUISITION_NEW_DET.AUTO_KEY_REQ(+) " & vbCrLf _
        ''                    & " AND QAL_REQUISITION_HDR.COMPANY_CODE=PAY_DEPT_MST.COMPANY_CODE(+) " & vbCrLf _
        ''                    & " AND QAL_REQUISITION_HDR.DEPT_CODE=PAY_DEPT_MST.DEPT_CODE(+) " & vbCrLf _
        ''                    & " AND QAL_REQUISITION_HDR.COMPANY_CODE=REQ.COMPANY_CODE(+) " & vbCrLf _
        ''                    & " AND QAL_REQUISITION_HDR.REQ_BY=REQ.EMP_CODE(+) " & vbCrLf _
        ''                    & " AND QAL_REQUISITION_HDR.COMPANY_CODE=APP.COMPANY_CODE(+) " & vbCrLf _
        ''                    & " AND QAL_REQUISITION_HDR.APP_BY=APP.EMP_CODE(+) " & vbCrLf _
        ''                    & " AND QAL_REQUISITION_HDR.AUTO_KEY_REQ=" & Val(txtReqNo.Text) & ""
        '
        '            Report1.ReportFileName = App.path & "\reports\RequisitionNew.rpt"
        '        ElseIf cboReqType.Text = "Repair" Then
        '            mSubTitle = "Requisition For Reapir of Items"
        '
        '            SqlStr = " SELECT QAL_REQUISITION_HDR.*,QAL_REQUISITION_REPAIR_DET.*, " & vbCrLf _
        ''                    & " PAY_DEPT_MST.DEPT_DESC, REQ.EMP_NAME,APP.EMP_NAME " & vbCrLf _
        ''                    & " FROM QAL_REQUISITION_HDR, QAL_REQUISITION_REPAIR_DET, " & vbCrLf _
        ''                    & " PAY_DEPT_MST, PAY_EMPLOYEE_MST REQ, PAY_EMPLOYEE_MST APP " & vbCrLf _
        ''                    & " WHERE QAL_REQUISITION_HDR.AUTO_KEY_REQ=QAL_REQUISITION_REPAIR_DET.AUTO_KEY_REQ(+) " & vbCrLf _
        ''                    & " AND QAL_REQUISITION_HDR.COMPANY_CODE=PAY_DEPT_MST.COMPANY_CODE(+) " & vbCrLf _
        ''                    & " AND QAL_REQUISITION_HDR.DEPT_CODE=PAY_DEPT_MST.DEPT_CODE(+) " & vbCrLf _
        ''                    & " AND QAL_REQUISITION_HDR.COMPANY_CODE=REQ.COMPANY_CODE(+) " & vbCrLf _
        ''                    & " AND QAL_REQUISITION_HDR.REQ_BY=REQ.EMP_CODE(+) " & vbCrLf _
        ''                    & " AND QAL_REQUISITION_HDR.COMPANY_CODE=APP.COMPANY_CODE(+) " & vbCrLf _
        ''                    & " AND QAL_REQUISITION_HDR.APP_BY=APP.EMP_CODE(+) " & vbCrLf _
        ''                    & " AND QAL_REQUISITION_HDR.AUTO_KEY_REQ=" & Val(txtReqNo.Text) & ""
        '
        '            Report1.ReportFileName = App.path & "\reports\RequisitionRepair.rpt"
        '        ElseIf cboReqType.Text = "Sample" Then
        '            mSubTitle = "Requisition For Inspection of Samples"

        SqlStr = " SELECT QAL_REQUISITION_HDR.*,QAL_REQUISITION_SAMPLE_DET.*, " & vbCrLf & " PAY_DEPT_MST.DEPT_DESC, REQ.EMP_NAME,APP.EMP_NAME " & vbCrLf & " FROM QAL_REQUISITION_HDR, QAL_REQUISITION_SAMPLE_DET, " & vbCrLf & " PAY_DEPT_MST, PAY_EMPLOYEE_MST REQ, PAY_EMPLOYEE_MST APP " & vbCrLf & " WHERE QAL_REQUISITION_HDR.AUTO_KEY_REQ=QAL_REQUISITION_SAMPLE_DET.AUTO_KEY_REQ(+) " & vbCrLf & " AND QAL_REQUISITION_HDR.COMPANY_CODE=PAY_DEPT_MST.COMPANY_CODE(+) " & vbCrLf & " AND QAL_REQUISITION_HDR.DEPT_CODE=PAY_DEPT_MST.DEPT_CODE(+) " & vbCrLf & " AND QAL_REQUISITION_HDR.COMPANY_CODE=REQ.COMPANY_CODE(+) " & vbCrLf & " AND QAL_REQUISITION_HDR.REQ_BY=REQ.EMP_CODE(+) " & vbCrLf & " AND QAL_REQUISITION_HDR.COMPANY_CODE=APP.COMPANY_CODE(+) " & vbCrLf & " AND QAL_REQUISITION_HDR.APP_BY=APP.EMP_CODE(+) " & vbCrLf & " AND QAL_REQUISITION_HDR.AUTO_KEY_REQ=" & Val(txtReqNo.Text) & " "

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\Requisition.rpt"
        '        End If

        SetCrpt(Report1, Mode, 1, mTitle, mSubTitle, True, xMyMenu)

        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False

        '        If cboReqType.Text = "Sample" Then
        '            SqlStr1 = " SELECT * FROM QAL_REQUISITION_REPAIR_DET " & vbCrLf _
        ''                    & " WHERE AUTO_KEY_REQ =" & Val(txtReqNo.Text) & "" & vbCrLf _
        ''                    & " ORDER BY SERIAL_NO "
        '        Else
        '            SqlStr1 = " SELECT * FROM QAL_REQUISITION_ACTION_DET " & vbCrLf _
        ''                    & " WHERE AUTO_KEY_REQ =" & Val(txtReqNo.Text) & "" & vbCrLf _
        ''                    & " ORDER BY SERIAL_NO "
        '        End If

        SqlStr1 = " SELECT * FROM QAL_REQUISITION_REPAIR_DET " & vbCrLf & " WHERE AUTO_KEY_REQ =" & Val(txtReqNo.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO "

        Report1.SubreportToChange = Report1.GetNthSubreportName(0)
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStr1

        SqlStr1 = " SELECT * FROM QAL_REQUISITION_NEW_DET " & vbCrLf & " WHERE AUTO_KEY_REQ =" & Val(txtReqNo.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO "

        Report1.SubreportToChange = Report1.GetNthSubreportName(1)
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStr1

        SqlStr1 = " SELECT * FROM QAL_REQUISITION_ACTION_DET " & vbCrLf & " WHERE AUTO_KEY_REQ =" & Val(txtReqNo.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO "

        Report1.SubreportToChange = Report1.GetNthSubreportName(2)
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStr1

        Report1.SubreportToChange = ""

        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
    End Sub

    Private Sub txtReqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReqNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReqNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReqNo.DoubleClick
        Call cmdSearchReqNo_Click(cmdSearchReqNo, New System.EventArgs())
    End Sub

    Private Sub txtReqNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReqNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtReqNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtReqNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchReqNo_Click(cmdSearchReqNo, New System.EventArgs())
    End Sub

    Public Sub txtReqNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReqNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String

        If Trim(txtReqNo.Text) = "" Then GoTo EventExitSub

        If Len(Trim(txtReqNo.Text)) < 6 Then
            txtReqNo.Text = Trim(txtReqNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If
        mSlipNo = Val(txtReqNo.Text)

        If MODIFYMode = True And RsRequisitionHdr.BOF = False Then xMKey = RsRequisitionHdr.Fields("AUTO_KEY_REQ").Value

        SqlStr = "SELECT * FROM QAL_REQUISITION_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REQ,LENGTH(AUTO_KEY_REQ)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_REQ=" & mSlipNo & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRequisitionHdr, ADODB.LockTypeEnum.adLockReadOnly)
        If RsRequisitionHdr.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_REQUISITION_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REQ,LENGTH(AUTO_KEY_REQ)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_REQ=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRequisitionHdr, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchReqNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchReqNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REQ,LENGTH(AUTO_KEY_REQ)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtReqNo.Text, "QAL_REQUISITION_HDR", "AUTO_KEY_REQ", "REQ_DATE", "REQ_TYPE", "DEPT_CODE", SqlStr) = True Then
            txtReqNo.Text = AcName
            Call txtReqNo_Validating(txtReqNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub txtReqDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReqDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReqDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReqDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtReqDate.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtReqDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReqDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtReqDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtReqDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        Else
            If Trim(txtReqDate.Text) <> "" And Trim(txtActionDate.Text) <> "" Then
                If CDate(txtActionDate.Text) < CDate(txtReqDate.Text) Then
                    MsgBox("Action Date cann't be Less than Requested Date")
                    Cancel = True
                End If
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cboReqType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboReqType.TextChanged

        Call ShowFrames()
        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboReqType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboReqType.SelectedIndexChanged

        Call ShowFrames()
        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDeptCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeptCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDeptCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeptCode.DoubleClick
        Call CmdSearchDeptCode_Click(CmdSearchDeptCode, New System.EventArgs())
    End Sub

    Private Sub txtDeptCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDeptCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDeptCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDeptCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDeptCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchDeptCode_Click(CmdSearchDeptCode, New System.EventArgs())
    End Sub

    Public Sub txtDeptCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeptCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtDeptCode.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.ValidateWithMasterTable(txtDeptCode.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Department Does Not Exist In Master.")
            Cancel = True
        Else
            txtDeptName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub CmdSearchDeptCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDeptCode.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDeptCode.Text = AcName1
            txtDeptName.Text = AcName
            txtDeptCode.Focus()
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

    Private Sub txtReqBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReqBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReqBy_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReqBy.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtReqBy.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtReqBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReqBy.DoubleClick
        Call cmdSearchReqBy_Click(cmdSearchReqBy, New System.EventArgs())
    End Sub

    Private Sub txtReqBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtReqBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchReqBy_Click(cmdSearchReqBy, New System.EventArgs())
    End Sub

    Private Sub txtReqBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReqBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtReqBy, txtReqName) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Function ValidateEMP(ByRef pCode As System.Windows.Forms.TextBox, ByRef pName As System.Windows.Forms.TextBox) As Boolean
        On Error GoTo ValEMP
        Dim SqlStr As String
        ValidateEMP = True
        If Trim(pCode.Text) = "" Then Exit Function
        pCode.Text = VB6.Format(pCode.Text, "000000")
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(pCode.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Employee Does Not Exist In Master.")
            ValidateEMP = False
        Else
            pName.Text = MasterNo
        End If
        Exit Function
ValEMP:
        MsgBox(Err.Description)
    End Function

    Private Sub cmdSearchReqBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchReqBy.Click
        Call SearchEmp(txtReqBy, txtReqName)
    End Sub

    Private Sub SearchEmp(ByRef pCode As System.Windows.Forms.TextBox, ByRef pName As System.Windows.Forms.TextBox)
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            pCode.Text = AcName1
            pName.Text = AcName
            If pCode.Enabled = True Then pCode.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtAppBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAppBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppBy.DoubleClick
        Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtAppBy_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAppBy.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAppBy.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAppBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAppBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtAppBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAppBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtAppBy, txtAppName) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchAppBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAppBy.Click
        Call SearchEmp(txtAppBy, txtAppName)
    End Sub

    Private Sub txtCustomer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustomer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomer.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub cboUrgency_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboUrgency.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboUrgency_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboUrgency.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtUrgencyReason_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUrgencyReason.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtUrgencyReason_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUrgencyReason.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtUrgencyReason.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub cboPreviousFailure_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPreviousFailure.TextChanged

        Call ShowFrames()
        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboPreviousFailure_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPreviousFailure.SelectedIndexChanged

        Call ShowFrames()
        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtActionDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtActionDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtActionDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtActionDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtActionDate.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtActionDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtActionDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtActionDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtActionDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        Else
            If Trim(txtReqDate.Text) <> "" And Trim(txtActionDate.Text) <> "" Then
                If CDate(txtActionDate.Text) < CDate(txtReqDate.Text) Then
                    MsgBox("Action Date cann't be Less than Requested Date")
                    Cancel = True
                End If
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cboReqStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboReqStatus.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboReqStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboReqStatus.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtStatusReason_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStatusReason.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtStatusReason_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtStatusReason.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtStatusReason.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
