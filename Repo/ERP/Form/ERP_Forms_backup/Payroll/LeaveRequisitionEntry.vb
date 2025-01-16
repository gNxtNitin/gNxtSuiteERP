Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class FrmLeaveRequisitionEntry
    Inherits System.Windows.Forms.Form
    Dim RsReqMain As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection					

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12
    Dim xMyMenu As String

    Dim pDataShow As Boolean

    Dim mIsAuthorisedUser As Boolean

    Private Const ColDate As Short = 1
    Private Const ColDay As Short = 2
    Private Const ColFH As Short = 3
    Private Const ColSH As Short = 4

    Private Sub chkHalfDay_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkHalfDay.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)

        If txtDateFrom.Text = "" Then Exit Sub
        If txtDateTo.Text = "" Then Exit Sub
        txtDays.Text = CStr(DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(txtDateFrom.Text), CDate(txtDateTo.Text)) + IIf(chkHalfDay.CheckState = System.Windows.Forms.CheckState.Checked, 0.5, 1))

    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtReqNo.Enabled = False
            cmdSearch.Enabled = False

        Else
            cmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart

        If Trim(txtReqNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If lblBookType.Text = "E" Then
            If optStatus(0).Checked = True Or optStatus(1).Checked = True Then
                MsgInformation("Requisition Completed, Cann't be Deleted")
                Exit Sub
            End If
        Else
            MsgInformation("Cann't be Deleted")
            Exit Sub
        End If

        If Not RsReqMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.					
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PAY_LEAVE_APP_TRN ", (txtReqNo.Text), RsReqMain, "AUTO_KEY_REF") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PAY_LEAVE_APP_TRN ", "AUTO_KEY_REF", (txtReqNo.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("Delete from PAY_LEAVE_APP_TRN  Where AUTO_KEY_REF=" & Val(txtReqNo.Text) & "")

                PubDBCn.CommitTrans()
                RsReqMain.Requery() ''.Refresh					
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''					
        RsReqMain.Requery() ''.Refresh					
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click
        On Error GoTo ModifyErr

        '    If PubSuperUser = "U" Then					
        If optStatus(0).Checked = True Or optStatus(1).Checked = True Then
            MsgInformation("Requisition Completed, Cann't be Modified")
            Exit Sub
        End If
        '    End If					

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtReqNo.Enabled = False
            cmdSearch.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForLeave(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForLeave(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportForLeave(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "Leave Application Form"


        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP, PAY_LEAVE_APP_TRN TRN,PAY_DEPT_MST DEPT" & vbCrLf & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP.COMPANY_CODE=TRN.COMPANY_CODE " & vbCrLf & " AND EMP.EMP_CODE=TRN.EMP_CODE" & vbCrLf & " AND EMP.COMPANY_CODE=DEPT.COMPANY_CODE " & vbCrLf & " AND EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE" & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmp.Text) & "'"


        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\LeaveAppForm.rpt"

        SetCrpt(Report1, Mode, 1, mTitle) '', , fa, xMyMenu					

        '    MainClass.AssignCRptFormulas Report1, "mDesignation='" & MainClass.AllowSingleQuote(lblDesgName.Caption) & "'"					
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtReqNo_Validating(txtReqNo, New System.ComponentModel.CancelEventArgs(False))
            If cmdAdd.Enabled = True Then cmdAdd.Focus()
        Else
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

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND APP_STATUS='O'"

        If MainClass.SearchGridMaster((txtReqNo.Text), "PAY_LEAVE_APP_TRN ", "TRIM(To_CHAR(AUTO_KEY_REF,'000000'))", "REF_DATE", "EMP_CODE", "APP_EMP_CODE", SqlStr) = True Then
            txtReqNo.Text = AcName
            txtReqNo_Validating(txtReqNo, New System.ComponentModel.CancelEventArgs(False))
            If txtReqNo.Enabled = True Then txtReqNo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEmp.Click
        Call txtEmp_DoubleClick(txtEmp, New System.EventArgs())
    End Sub

    Private Sub cmdSearchRecEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchRecEmp.Click
        Call txtRecEmpCode_DoubleClick(txtRecEmpCode, New System.EventArgs())
    End Sub


    Private Sub FrmLeaveRequisitionEntry_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub optHRStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optHRStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optHRStatus.GetIndex(eventSender)
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub OptStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optStatus.GetIndex(eventSender)
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdView.Change
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.row
            .Col = 1
            txtReqNo.Text = .Text
            txtReqNo_Validating(txtReqNo, New System.ComponentModel.CancelEventArgs(False))
            If txtReqNo.Enabled = True Then txtReqNo.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function AutoGenSeqNo(ByRef mFieldName As String, ByRef mTableName As String) As Double
        On Error GoTo AutoGenSeqNoErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mNewSeqNo As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = "SELECT Max(TO_NUMBER(substr(" & mFieldName & ",1,length(" & mFieldName & ")-2)))  AS AUTO_KEY " & vbCrLf & " FROM " & mTableName & " " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields.Item("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)

        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(RsAutoGen.Fields.Item("AUTO_KEY").Value) Then
                    mNewSeqNo = RsAutoGen.Fields.Item("AUTO_KEY").Value + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With

        AutoGenSeqNo = CDbl(mNewSeqNo & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetAppEmpID(ByRef pAppUserCode As String) As String
        On Error GoTo ErrPart
        Dim RsAutoGen As ADODB.Recordset
        Dim SqlStr As String

        SqlStr = ""
        GetAppEmpID = ""
        SqlStr = "SELECT USER_ID " & vbCrLf & " FROM ATH_PASSWORD_MST " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields.Item("COMPANY_CODE").Value & " " & vbCrLf & " AND STATUS='O' AND (USER_CODE='" & MainClass.AllowSingleQuote(pAppUserCode) & "')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)

        If RsAutoGen.EOF = False Then
            GetAppEmpID = RsAutoGen.Fields.Item("USER_ID").Value
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim mReqnum As String
        Dim SqlStr As String
        Dim mVNoSeq As Double
        Dim mStatus As String
        Dim mEntryDate As String
        Dim mHRStatus As String
        Dim mHalfDay As String

        Dim cntRow As Integer
        Dim mFHalf As Integer
        Dim mSHalf As Integer
        Dim mDate As String
        Dim mAgtLate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()




        mStatus = IIf(optStatus(0).Checked = True, "A", IIf(optStatus(1).Checked = True, "R", "O"))
        mHRStatus = IIf(optHRStatus(1).Checked = True, "C", "O")
        mHalfDay = IIf(chkHalfDay.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If Val(txtReqNo.Text) = 0 Then
            mVNoSeq = AutoGenSeqNo("AUTO_KEY_REF", "PAY_LEAVE_APP_TRN")
        Else
            mVNoSeq = Val(txtReqNo.Text)
        End If

        txtReqNo.Text = CStr(Val(CStr(mVNoSeq)))

        SqlStr = ""
        If ADDMode = True Then
            lblMKey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO PAY_LEAVE_APP_TRN  (" & vbCrLf _
                & " AUTO_KEY_REF,REF_DATE,COMPANY_CODE," & vbCrLf _
                & " EMP_CODE,FROM_DATE,TO_DATE," & vbCrLf _
                & " LDAYS,REC_EMP_CODE,APP_EMP_CODE," & vbCrLf _
                & " REASON,APP_STATUS,HR_STATUS, HALF_DAY, " & vbCrLf _
                & " ADDUSER,ADDDATE,MODUSER,MODDATE)" & vbCrLf _
                & " VALUES( " & vbCrLf _
                & " " & Val(CStr(mVNoSeq)) & ",TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & RsCompany.Fields("Company_Code").Value & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtEmp.Text)) & "', TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " " & Val(txtDays.Text) & ", '" & MainClass.AllowSingleQuote((txtRecEmpCode.Text)) & "', '" & MainClass.AllowSingleQuote((txtAppEmpCode.Text)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtReason.Text)) & "', '" & mStatus & "', '" & mHRStatus & "','" & mHalfDay & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

            ''Format(PubCurrDate, "DD-MMM-YYYY")					
        ElseIf MODIFYMode = True Then
            SqlStr = ""

            SqlStr = "UPDATE PAY_LEAVE_APP_TRN  SET REF_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " EMP_CODE='" & MainClass.AllowSingleQuote((txtEmp.Text)) & "', " & vbCrLf _
                & " FROM_DATE=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ," & vbCrLf _
                & " TO_DATE=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ," & vbCrLf _
                & " LDAYS=" & Val(txtDays.Text) & " ," & vbCrLf _
                & " REC_EMP_CODE='" & MainClass.AllowSingleQuote((txtRecEmpCode.Text)) & "' ," & vbCrLf _
                & " APP_EMP_CODE='" & MainClass.AllowSingleQuote((txtAppEmpCode.Text)) & "' ," & vbCrLf _
                & " REASON='" & MainClass.AllowSingleQuote((txtReason.Text)) & "' ," & vbCrLf _
                & " APP_STATUS='" & mStatus & "' ," & vbCrLf _
                & " HR_STATUS='" & mHRStatus & "' , HALF_DAY='" & mHalfDay & "'," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                & " AND AUTO_KEY_REF =" & Val(lblMKey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)

        SqlStr = "DELETE FROM PAY_REQ_ATTN_MST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND AUTO_KEY_REF=" & Val(lblMKey.Text) & " AND EMP_CODE='" & txtEmp.Text & "'" & vbCrLf _
            & " AND ATTN_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND ATTN_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDate
                mDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColFH
                mFHalf = IIf(.Text = "", -1, VB.Left(.Text, 1))

                .Col = ColSH
                mSHalf = IIf(.Text = "", -1, VB.Left(.Text, 1))

                mAgtLate = "N"

                If mFHalf <> -1 Or mSHalf <> -1 Then
                    SqlStr = "INSERT INTO PAY_REQ_ATTN_MST (" & vbCrLf _
                        & " COMPANY_CODE, PAYYEAR, AUTO_KEY_REF, EMP_CODE," & vbCrLf _
                        & " ATTN_DATE, FIRSTHALF, SECONDHALF, " & vbCrLf _
                        & " ADDUSER, ADDDATE) VALUES (" & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(mDate)) & ", " & Val(lblMKey.Text) & "," & vbCrLf _
                        & " '" & txtEmp.Text & "', TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & "  " & mFHalf & ", " & mSHalf & "," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With



        If SendMail() = False Then GoTo ErrPart

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''					

        If ADDMode = True Then
            txtReqNo.Text = ""
        End If

        RsReqMain.Requery() ''.Refresh					

        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217873 Then
            ErrorMsg("Leave Already Applied for such Date, Cann't be save.", vbCritical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume					
    End Function
    Private Function SendMail() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String

        Dim mTo As String
        Dim mCC As String
        Dim mFrom As String
        Dim mAttachmentFile As String
        Dim mDateTime As String
        Dim pAccountCode As String
        Dim mSubject As String
        Dim mBodyText As String
        Dim mHReMailID As String
        Dim mPublishPath As String
        Dim mApprovedEmpID As String

        SendMail = False

        '    strServerPop3 = ReadInIFromServer("InternetInfo", "POP3", "InternetInfo.INI")					
        '    strServerSmtp = ReadInIFromServer("InternetInfo", "SMTP", "InternetInfo.INI")					
        '    strAccount = ReadInIFromServer("InternetInfo", "Account", "InternetInfo.INI")					
        '    strPassword = ReadInIFromServer("InternetInfo", "Password", "InternetInfo.INI")					

        strServerPop3 = GetEMailID("POP_ID") ''ReadInIFromServer("InternetInfo", "POP3", "InternetInfo.INI")					
        strServerSmtp = GetEMailID("SMTP_ID") ''ReadInIFromServer("InternetInfo", "SMTP", "InternetInfo.INI")					
        strAccount = GetEMailID("MAIL_ACCOUNT") ''ReadInIFromServer("InternetInfo", "Account", "InternetInfo.INI")					
        strPassword = GetEMailID("PASSWORD")


        mHReMailID = GetEMailID("HRD_MAIL_TO") '' ReadInIFromServer("InternetInfo", "HR_eMail", "InternetInfo.INI")					

        mTo = Trim(lblToeMailID.Text) '' ReadInI("InternetInfo", "TO", "InternetInfo.INI")					
        '    mCC = ReadInI("InternetInfo", "CC", "InternetInfo.INI")					
        mFrom = GetEMailID("MAIL_FROM") ''mFrom = Trim(lblFromeMailID.Caption)  ''ReadInI("InternetInfo", "FROM", "InternetInfo.INI")					
        mCC = Trim(lblFromeMailID.Text)

        mAttachmentFile = ""

        mSubject = ""

        mSubject = "Leave Requisition of " & Trim(lblEmpname.Text) & " From Dated : " & txtDateFrom.Text & " To Dated : " & txtDateTo.Text

        mApprovedEmpID = GetAppEmpID((txtAppEmpCode.Text))

        'If RsCompany.Fields("COMPANY_CODE").Value = 1 Then
        '    mPublishPath = PubPublishPath1 & mApprovedEmpID
        'Else
        mPublishPath = PubPublishPath
        'End If

        mBodyText = "<html><body><b><font size=11, color=Red>Leave Requisition</font></b><br />" & "<b>Employee Name : </b>" & Trim(lblEmpname.Text) & "<br />" & "<b>Designation : </b>" & Trim(lblDesgName.Text) & "(" & lblDeptname.Text & ") <br />" & "<b>From Dated : </b>" & Trim(txtDateFrom.Text) & "<br />" & "<b>To Dated : </b>" & Trim(txtDateTo.Text) & "<br />" & "<b>Total Working Days Applied: </b>" & Trim(txtDays.Text) & "<br />" & "<b>Reason : </b>" & Trim(txtReason.Text) & "<br />" & "<br />" & "<br />" & "<a href='mailto:" & lblFromeMailID.Text & ", " & mHReMailID & "?subject=Your Leave Sanction.&body=Dear " & lblEmpname.Text & ",%0AYour Leave Sanction From " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " TO: " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & " agt Your Ref. No " & txtReqNo.Text & ".%0A%0ARegards, %0A%0A" & lblAppEmpName.Text & "'>Approved</a> " & "<a href='mailto:" & lblFromeMailID.Text & "?subject=Your Leave Rejected.&body=Your Leave Rejected From " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " TO " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & " agt Your Ref. No " & txtReqNo.Text & ".%0A%0ARegards, %0A%0A" & lblAppEmpName.Text & "'>Rejected</a> " & "<br />" & "<br />" & "<a href=" & mPublishPath & ">" & "On Line Approval" & "</a> " & "</body></html>"

        'If strServerPop3 = "" And strServerSmtp = "" And strAccount = "" And strPassword = "" Then
        '    MsgBox("Please Check Email Configuration", MsgBoxStyle.Information)
        '    SendMail = False
        '    Exit Function
        'End If

        'Call SendMailProcess(mFrom, mTo, mCC, "", strAccount, strPassword, mAttachmentFile, mSubject, mBodyText)

        'If SendMailProcess(mFrom, mTo, mCC, "", mAttachmentFile, mSubject, mBodyText) = False Then

        'End If
        SendMail = True

        Exit Function
ErrPart:
        SendMail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim SqlStr As String
        Dim mCheckLastReq As Boolean

        Dim mCPLEarn As Double
        Dim mTotCPLEarn As Double
        Dim mCPLAvail As Double
        Dim mCurrCPLAvail As Double

        Dim mBalCPLEarn As Double
        Dim mCPLFH As String
        Dim mCPLSH As String

        Dim mCheckCPLEarn As Double
        Dim mTotalCPLAvail As Double
        Dim mCurrentDate As String
        'Dim mWorkingHours As Double
        Dim mShortLeave As Integer
        'Dim mIsRoundClock As String
        'Dim mAttnCheck As Boolean
        'Dim mSalaryCheck As Boolean
        'Dim mLastDay As String
        Dim mMLAvail As Double
        Dim mRefDate As String
        'Dim mAgtESI As String
        'Dim mUnderESI As String

        Dim mCLBalance As Double
        Dim mSLBalance As Double
        Dim mELBalance As Double

        Dim mCLApplied As Double
        Dim mSLApplied As Double
        Dim mELApplied As Double

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsReqMain.EOF = True Then Exit Function

        If lblBookType.Text = "A" Then
            If txtReqNo.Text = "" Then
                MsgInformation("Requisition No. Cann't Blank")
                FieldsVarification = False
                Exit Function
            End If
        Else
            If MODIFYMode = True And txtReqNo.Text = "" Then
                MsgInformation("Requisition No. Cann't Blank")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If txtReqDate.Text = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtReqDate.Focus()
            Exit Function
        End If

        If txtDateFrom.Text = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDateFrom.Focus()
            Exit Function
        End If

        If txtDateTo.Text = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDateTo.Focus()
            Exit Function
        End If

        '    If CDate(txtDateFrom.Text) < CDate(PubCurrDate) Then					
        '        MsgBox "Please From Date Cann't be less than Current Date.", vbInformation					
        '        txtDateFrom.SetFocus					
        '        FieldsVarification = False					
        '        Exit Function					
        '    End If					




        'If Year(CDate(txtReqDate.Text)) = Year(CDate(txtDateFrom.Text)) Then
        '    MsgBox("Request date and Leave Applied Date Should be Same Year.", MsgBoxStyle.Information)
        '    FieldsVarification = False
        '    txtDateFrom.Focus()
        '    Exit Function
        'End If

        If Year(CDate(txtDateFrom.Text)) <> Year(CDate(txtDateTo.Text)) Then
            MsgBox("Leave Applied From Date & To Date Should be Same Year.", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDateFrom.Focus()
            Exit Function
        End If

        If Month(CDate(txtDateFrom.Text)) <> Month(CDate(txtDateTo.Text)) Then
            MsgBox("Leave Applied From Date & To Date Should be Same Month.", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDateFrom.Focus()
            Exit Function
        End If

        If chkHalfDay.CheckState = System.Windows.Forms.CheckState.Checked Then
            If CDate(txtDateFrom.Text) <> CDate(txtDateTo.Text) Then
                MsgBox("Please To Date must be same From Date.", MsgBoxStyle.Information)
                txtDateTo.Focus()
                FieldsVarification = False
                Exit Function
            End If
        Else
            If CDate(txtDateFrom.Text) > CDate(txtDateTo.Text) Then
                MsgBox("Please To Date Cann't be less than From Date.", MsgBoxStyle.Information)
                txtDateTo.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        txtDays.Text = CStr(DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(txtDateFrom.Text), CDate(txtDateTo.Text)) + IIf(chkHalfDay.CheckState = System.Windows.Forms.CheckState.Checked, 0.5, 1))

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        SqlStr = SqlStr & vbCrLf _
            & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


        If Trim(txtEmp.Text) = "" Then
            MsgBox("Employee Code is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            txtEmp.Focus()
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , SqlStr) = False Then
                MsgBox("Invalid Employee Code. Cann't Save", MsgBoxStyle.Information)
                FieldsVarification = False
                txtEmp.Focus()
                Exit Function
            End If
        End If

        If mIsAuthorisedUser = False Then
            If VB6.Format(txtEmp.Text, "000000") <> VB6.Format(PubUserEMPCode, "000000") Then
                MsgInformation("You are not a Valid User for Such ID.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtRecEmpCode.Text) = "" Then
            MsgBox("Recommended By is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            txtRecEmpCode.Focus()
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable((txtRecEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , SqlStr) = False Then
                MsgBox("Invalid Recommended By Code. Cann't Save", MsgBoxStyle.Information)
                FieldsVarification = False
                txtRecEmpCode.Focus()
                Exit Function
            End If
        End If

        If Trim(txtAppEmpCode.Text) = "" Then
            MsgBox("Approved By is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            txtRecEmpCode.Focus()
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable((txtAppEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , SqlStr) = False Then
                MsgBox("Invalid Approved By Code. Cann't Save", MsgBoxStyle.Information)
                FieldsVarification = False
                txtAppEmpCode.Focus()
                Exit Function
            End If
        End If


        mCPLEarn = 0
        mCPLAvail = 0
        mCurrCPLAvail = 0
        mTotCPLEarn = 0
        mShortLeave = 0

        mMLAvail = 0
        mRefDate = VB6.Format(txtReqDate.Text, "DD/MM/YYYY") ' DateAdd("d", -1, txtRefDate.Text)
        mMLAvail = GetOpeningLeaves((txtEmp.Text), mRefDate, MATERNITY, "Y", "Y", "")
        mCLBalance = Val(CStr(1))


        Dim mEmpESPApp As Boolean

        mEmpESPApp = GetEmployeeESIApp(txtEmp.Text, mRefDate)
        mSLBalance = GetOpeningLeaves((txtEmp.Text), mRefDate, SICK, "Y", "Y", "")
        mCLBalance = GetOpeningLeaves((txtEmp.Text), mRefDate, CASUAL, "Y", "Y", "")
        mELBalance = GetOpeningLeaves((txtEmp.Text), mRefDate, EARN, "Y", "Y", "")




        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDate
                mCurrentDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColFH
                If Trim(.Text) = "" Then
                    MsgInformation("Please Select Leave.")
                    FieldsVarification = False
                    Exit Function
                End If

                .Col = ColSH
                If Trim(.Text) = "" Then
                    MsgInformation("Please Select Leave.")
                    FieldsVarification = False
                    Exit Function
                End If

                .Col = ColFH
                If mEmpESPApp = True And Val(VB.Left(.Text, 2)) = 3 Then
                    MsgInformation("ESI Applicable For this Emp, so cann't be avail Sick Leave.")
                    FieldsVarification = False
                    Exit Function
                End If

                mMLAvail = mMLAvail - IIf(Val(VB.Left(.Text, 2)) = 4, 0.5, 0)


                mCLBalance = mCLBalance - IIf(Val(VB.Left(.Text, 2)) = 1, 0.5, 0)
                mELBalance = mELBalance - IIf(Val(VB.Left(.Text, 2)) = 2, 0.5, 0)
                mSLBalance = mSLBalance - IIf(Val(VB.Left(.Text, 2)) = 3, 0.5, 0)

                mCLApplied = mCLApplied + IIf(Val(VB.Left(.Text, 2)) = 1, 0.5, 0)
                mELApplied = mELApplied + IIf(Val(VB.Left(.Text, 2)) = 2, 0.5, 0)
                mSLApplied = mSLApplied + IIf(Val(VB.Left(.Text, 2)) = 3, 0.5, 0)

                .Col = ColSH
                If mEmpESPApp = True And Val(VB.Left(.Text, 2)) = 3 Then
                    MsgInformation("ESI Applicable For this Emp, so cann't be avail Sick Leave.")
                    FieldsVarification = False
                    Exit Function
                End If
                mMLAvail = mMLAvail - IIf(Val(VB.Left(.Text, 2)) = 4, 0.5, 0)

                mCLBalance = mCLBalance - IIf(Val(VB.Left(.Text, 2)) = 1, 0.5, 0)
                mELBalance = mELBalance - IIf(Val(VB.Left(.Text, 2)) = 2, 0.5, 0)
                mSLBalance = mSLBalance - IIf(Val(VB.Left(.Text, 2)) = 3, 0.5, 0)

                mCLApplied = mCLApplied + IIf(Val(VB.Left(.Text, 2)) = 1, 0.5, 0)
                mELApplied = mELApplied + IIf(Val(VB.Left(.Text, 2)) = 2, 0.5, 0)
                mSLApplied = mSLApplied + IIf(Val(VB.Left(.Text, 2)) = 3, 0.5, 0)


            Next
        End With

        'If PubUserID = "G0416" Then
        'Else
        If mMLAvail < 0 Then
                MsgInformation("No Balance in Maternity Leave. So Cann't be Save.")
                FieldsVarification = False
                Exit Function
            End If

            If mCLBalance < 0 And mCLApplied > 0 Then
                MsgInformation("No Balance in Casual Leave. So Cann't be Save.")
                FieldsVarification = False
                Exit Function
            End If

            If mELBalance < 0 And mELApplied > 0 Then
                MsgInformation("No Balance in Earn Leave. So Cann't be Save.")
                FieldsVarification = False
                Exit Function
            End If

            If mSLBalance < 0 And mSLApplied > 0 Then
                MsgInformation("No Balance in Sick Leave. So Cann't be Save.")
                FieldsVarification = False
                Exit Function
            End If
        'End If


        If CheckSalaryMade((txtEmp.Text), VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")) = True Then
            MsgInformation("Salary Made Againt This Month. So Cann't be Entered Back Leave.")
            FieldsVarification = False
            Exit Function
        End If

        '    If CheckAlreadyReqMade = True Then					
        '        MsgBox "You Already given Requisition for Such Date. Cann't be Save.", vbInformation					
        '        FieldsVarification = False					
        '        Exit Function					
        '    End If					

        FieldsVarification = True

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume					
    End Function
    Private Function CheckAlreadyReqMade() As Boolean
        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String

        CheckAlreadyReqMade = False

        SqlStr = " SELECT * FROM PAY_LEAVE_APP_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmp.Text)) & "'" & vbCrLf _
            & " AND FROM_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND FROM_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If Val(txtReqNo.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_REF<>" & Val(txtReqNo.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        '    If RsTemp.EOF = False Then					
        CheckAlreadyReqMade = True
        '    End If					

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmLeaveRequisitionEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblBookType.Text = "E" Then
            Me.Text = "Leave Requisition Entry"
        Else
            Me.Text = "Leave Requisition (HOD Approval)"
        End If

        SqlStr = ""
        SqlStr = "Select * from PAY_LEAVE_APP_TRN  Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        '    Call SetTextLengths					

        '    Clear1					
        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume					
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        On Error GoTo AssignGridErr
        Dim SqlStr As String
        Dim mRunYear As Long
        Dim mStartDate As String
        Dim mEndDate As String

        SqlStr = ""

        mRunYear = Year(RunDate)
        mStartDate = "01/01/" & mRunYear
        mEndDate = "31/12/" & mRunYear

        ''SELECT CLAUSE...					

        SqlStr = " SELECT  TO_CHAR(AUTO_KEY_REF,'0000000') AS AUTO_KEY_REF, REF_DATE,IH.EMP_CODE,EMP.EMP_NAME,FROM_DATE,TO_DATE,LDAYS,REC_EMP_CODE,APP_EMP_CODE, " & vbCrLf _
            & " DECODE(APP_STATUS,'O','OPEN',DECODE(APP_STATUS,'A','APPROVED',DECODE(APP_STATUS,'R','REJECTED','CLOSED'))) AS STATUS, " & vbCrLf _
            & " DECODE(HR_STATUS,'O','OPEN','CLOSED') AS HR_STATUS, " & vbCrLf _
            & " REASON"

        ''FROM CLAUSE...					

        SqlStr = SqlStr & vbCrLf & " FROM PAY_LEAVE_APP_TRN IH, PAY_EMPLOYEE_MST EMP  "

        ''WHERE CLAUSE...					

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf _
            & " AND IH.EMP_CODE=EMP.EMP_CODE"

        ''ORDER BY CLAUSE...					
        'If mIsAuthorisedUser = False Then
        '    SqlStr = SqlStr & vbCrLf _
        '        & " AND IH.EMP_CODE ='" & VB6.Format(PubUserEMPCode, "000000") & "'"
        'End If

        'If PubUserLevel = 1 Then

        'ElseIf PubUserLevel = 2 Then
        '    SqlStr = SqlStr & vbCrLf & " AND (EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP_CODE = '" & PubUserEMPCode & "')"
        'Else
        SqlStr = SqlStr & vbCrLf & " AND IH.EMP_CODE='" & PubUserEMPCode & "'"
        'End If

        SqlStr = SqlStr & vbCrLf & " AND FROM_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD/MMM/YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND FROM_DATE<=TO_DATE('" & VB6.Format(mEndDate, "DD/MMM/YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " Order by AUTO_KEY_REF"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdView()
        With SprdView
            .Row = -1

            .set_RowHeight(0, 400)

            .set_ColWidth(0, 600)
            .set_ColWidth(1, 1000)
            .Col = 1
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            .set_ColWidth(2, 1000)
            .Col = 2
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

            .set_ColWidth(3, 1000)
            .set_ColWidth(4, 3500)
            .set_ColWidth(5, 1000)
            .set_ColWidth(6, 1000)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsReqMain
            txtReqDate.MaxLength = 10
            txtReqNo.MaxLength = .Fields("AUTO_KEY_REF").Precision

            txtEmp.MaxLength = .Fields("EMP_CODE").DefinedSize

            txtDateFrom.MaxLength = 10
            txtDateTo.MaxLength = 10
            txtDays.MaxLength = .Fields("LDAYS").Precision
            txtRecEmpCode.MaxLength = .Fields("REC_EMP_CODE").DefinedSize
            txtAppEmpCode.MaxLength = .Fields("APP_EMP_CODE").DefinedSize
            txtReason.MaxLength = .Fields("REASON").DefinedSize

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim mStatus As String
        Dim mHRStatus As String
        Dim mDeptCode As String

        With RsReqMain
            If Not .EOF Then
                txtReqNo.Enabled = False
                lblMKey.Text = .Fields("AUTO_KEY_REF").Value

                txtReqNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_REF").Value), 0, .Fields("AUTO_KEY_REF").Value)
                txtReqDate.Text = VB6.Format(IIf(IsDBNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")
                txtEmp.Text = IIf(IsDBNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtDateFrom.Text = VB6.Format(IIf(IsDBNull(.Fields("FROM_DATE").Value), "", .Fields("FROM_DATE").Value), "DD/MM/YYYY")
                txtDateTo.Text = VB6.Format(IIf(IsDBNull(.Fields("TO_DATE").Value), "", .Fields("TO_DATE").Value), "DD/MM/YYYY")
                txtDays.Text = IIf(IsDBNull(.Fields("LDAYS").Value), "", .Fields("LDAYS").Value)
                txtRecEmpCode.Text = IIf(IsDBNull(.Fields("REC_EMP_CODE").Value), "", .Fields("REC_EMP_CODE").Value)
                txtAppEmpCode.Text = IIf(IsDBNull(.Fields("APP_EMP_CODE").Value), "", .Fields("APP_EMP_CODE").Value)
                txtReason.Text = IIf(IsDBNull(.Fields("REASON").Value), "", .Fields("REASON").Value)
                chkHalfDay.CheckState = IIf(.Fields("HALF_DAY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                mStatus = IIf(IsDBNull(.Fields("APP_STATUS").Value), "", .Fields("APP_STATUS").Value)
                mHRStatus = IIf(IsDBNull(.Fields("HR_STATUS").Value), "", .Fields("HR_STATUS").Value)

                optStatus(0).Checked = False
                optStatus(1).Checked = False

                If mStatus = "A" Then
                    optStatus(0).Checked = True
                End If

                If mStatus = "R" Then
                    optStatus(1).Checked = True
                End If

                optHRStatus(0).Checked = False
                optHRStatus(1).Checked = False

                If mHRStatus = "O" Then
                    optHRStatus(0).Checked = True
                ElseIf mHRStatus = "C" Then
                    optHRStatus(1).Checked = True
                End If

                If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblEmpname.Text = MasterNo
                Else
                    lblEmpname.Text = ""
                End If

                If MainClass.ValidateWithMasterTable((txtRecEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblRecEmpName.Text = MasterNo
                Else
                    lblRecEmpName.Text = ""
                End If

                If MainClass.ValidateWithMasterTable((txtAppEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblAppEmpName.Text = MasterNo
                Else
                    lblAppEmpName.Text = ""
                End If

                If MainClass.ValidateWithMasterTable((txtAppEmpCode.Text), "EMP_CODE", "EMP_EMAILID_OFF", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblToeMailID.Text = MasterNo
                Else
                    lblToeMailID.Text = ""
                End If

                SqlStr = "SELECT EMP_NAME,EMP_EMAILID_OFF,EMP_DEPT_CODE, " & vbCrLf _
                    & " GETEMPDESG (" & RsCompany.Fields.Item("COMPANY_CODE").Value & ",'" & Trim(txtEmp.Text) & "'," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC " & vbCrLf _
                    & " FROM PAY_EMPLOYEE_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND EMP_CODE='" & Trim(UCase(txtEmp.Text)) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    lblEmpname.Text = IIf(IsDBNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)
                    lblFromeMailID.Text = IIf(IsDBNull(RsTemp.Fields("EMP_EMAILID_OFF").Value), "", RsTemp.Fields("EMP_EMAILID_OFF").Value)
                    mDeptCode = IIf(IsDBNull(RsTemp.Fields("EMP_DEPT_CODE").Value), "", RsTemp.Fields("EMP_DEPT_CODE").Value)
                    lblDesgName.Text = IIf(IsDBNull(RsTemp.Fields("DESG_DESC").Value), "", RsTemp.Fields("DESG_DESC").Value)

                    If MainClass.ValidateWithMasterTable(mDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        lblDeptname.Text = MasterNo
                    End If
                End If

                txtEmp.Enabled = False
                txtRecEmpCode.Enabled = False
                txtAppEmpCode.Enabled = False
                cmdSearchEmp.Enabled = False
                cmdSearchAppEmp.Enabled = False
                cmdSearchRecEmp.Enabled = False

                Call FillDate()
                If ShowDetail1() = False Then GoTo ERR1
                Call FillLeaves((txtEmp.Text))

            End If
        End With
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        txtReqNo.Enabled = True
        cmdSearch.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume					
    End Sub
    Private Sub ViewGrid()
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        AdoDCMain.Refresh					
            FormatSprdView()
            SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        Dim SqlStr As String
        Dim mHODCode As String = ""
        Dim mHRHODCode As String
        Dim mDeptHOD As String = "N"
        Dim RsTemp As ADODB.Recordset = Nothing


        lblMKey.Text = ""
        txtReqNo.Text = ""
        txtReqDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")
        txtReqDate.Enabled = False
        txtEmp.Text = PubUserEMPCode     '' ""
        lblEmpname.Text = ""
        lblFromeMailID.Text = ""
        lblDeptname.Text = ""
        lblDesgName.Text = ""
        lblRecEmpName.Text = ""
        lblAppEmpName.Text = ""
        lblToeMailID.Text = ""

        If PubUserEMPCode <> "" Then
            If MainClass.ValidateWithMasterTable(PubUserEMPCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblEmpname.Text = MasterNo
                txtEmp_Validating(txtEmp, New System.ComponentModel.CancelEventArgs(False))
            Else
                txtEmp.Text = ""
            End If
        End If

        txtEmp.Enabled = False
        cmdSearchEmp.Enabled = False

        txtDateFrom.Text = ""
        txtDateTo.Text = ""
        txtDays.Text = ""
        txtRecEmpCode.Text = ""
        txtAppEmpCode.Text = ""
        txtReason.Text = ""

        optStatus(0).Checked = False
        optStatus(1).Checked = False
        optHRStatus(1).Checked = False
        optHRStatus(0).Checked = False



        txtDays.Enabled = False

        chkHalfDay.CheckState = System.Windows.Forms.CheckState.Unchecked

        FraAppStatus.Enabled = IIf(lblBookType.Text = "E", False, True)
        FraHRStatus.Enabled = False


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        mHRHODCode = GetHRHODCode(txtReqDate.Text)
        If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "IS_DEPT_HOD", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
            mDeptHOD = MasterNo
        End If
        If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_HOD_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
            mHODCode = MasterNo
        Else
            mHODCode = ""
        End If

        If mDeptHOD = "N" Then
            SqlStr = "SELECT EMP_CODE, EMP_NAME FROM PAY_EMPLOYEE_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_HOD_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & txtEmp.Text & "')"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                txtRecEmpCode.Text = IIf(IsDBNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)
                lblRecEmpName.Text = IIf(IsDBNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)
            End If
        Else
            txtRecEmpCode.Text = mHRHODCode
            If mHRHODCode = "" Then
                lblRecEmpName.Text = ""
            Else
                If MainClass.ValidateWithMasterTable(mHRHODCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
                    lblRecEmpName.Text = MasterNo
                End If
            End If

        End If

        txtAppEmpCode.Text = mHRHODCode
        If mHRHODCode = "" Then
            lblAppEmpName.Text = ""
        Else
            txtAppEmpCode_Validating(txtAppEmpCode, New System.ComponentModel.CancelEventArgs(False))
            'If MainClass.ValidateWithMasterTable(mHRHODCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    lblAppEmpName.Text = MasterNo
            'End If
        End If

        txtEmp.Enabled = False
        txtRecEmpCode.Enabled = False
        txtAppEmpCode.Enabled = False
        cmdSearchEmp.Enabled = False
        cmdSearchAppEmp.Enabled = False
        cmdSearchRecEmp.Enabled = False

        MainClass.ClearGrid(SprdMain)
        Call FormatMain()

        pDataShow = False
        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub FrmLeaveRequisitionEntry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    'Private Sub Form_KeyUp(KeyCode As Integer, Shift As Integer)					
    ''    MainClass.DoFunctionKey Me, KeyCode					
    'End Sub					
    Public Sub FrmLeaveRequisitionEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection					
        'PvtDBCn.Open StrConn					

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        xMyMenu = myMenu

        If InStr(1, XRIGHT, "S") = 0 Then
            mIsAuthorisedUser = False
        Else
            mIsAuthorisedUser = True
        End If

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(6375)
        Me.Width = VB6.TwipsToPixelsX(8280)

        AdoDCMain.Visible = False

        txtReqNo.Enabled = True
        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtAppEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppEmpCode.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAppEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAppEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mHRHODCode As String

        If Trim(txtAppEmpCode.Text) = "" Then GoTo EventExitSub

        mHRHODCode = GetHRHODCode(txtReqDate.Text)
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & mHRHODCode & "'"

        'txtAppEmpCode.Text = VB6.Format(txtAppEmpCode.Text, "000000")

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE=1"
        'SqlStr = SqlStr & vbCrLf _
        '    & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        'If PubUserLevel = 1 Then

        'ElseIf PubUserLevel = 2 Then
        '    'SqlStr = SqlStr & vbCrLf & " AND (EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP_CODE = '" & PubUserEMPCode & "')"
        'Else
        '    SqlStr = SqlStr & vbCrLf & " AND (EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP_CODE = '" & PubUserEMPCode & "')"
        '    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE<>'" & PubUserEMPCode & "'"
        'End If

        If MainClass.ValidateWithMasterTable((txtAppEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
            lblAppEmpName.Text = MasterNo
        Else
            MsgInformation("Invalid Employee Code")
            Cancel = True
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable((txtAppEmpCode.Text), "EMP_CODE", "EMP_EMAILID_OFF", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblToeMailID.Text = MasterNo
        Else
            lblToeMailID.Text = ""
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtAppEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppEmpCode.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mHRHODCode As String

        mHRHODCode = GetHRHODCode(txtReqDate.Text)

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & mHRHODCode & "'"
        'SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        'If PubUserLevel = 1 Then

        'ElseIf PubUserLevel = 2 Then
        '    'SqlStr = SqlStr & vbCrLf & " AND (EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP_CODE = '" & PubUserEMPCode & "')"
        '    SqlStr = SqlStr & vbCrLf & " AND EMP_DEPT_CODE IN ('HR','HRD')"
        '    'SqlStr = SqlStr & vbCrLf & " AND EMP_CODE<>'" & PubUserEMPCode & "'"
        'Else
        '    'SqlStr = SqlStr & vbCrLf & " AND (EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP_CODE = '" & PubUserEMPCode & "')"
        '    SqlStr = SqlStr & vbCrLf & " AND EMP_DEPT_CODE IN ('HR','HRD')"
        '    'SqlStr = SqlStr & vbCrLf & " AND EMP_CODE<>'" & PubUserEMPCode & "'"
        'End If

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE",  ,  , SqlStr) = True Then
            txtAppEmpCode.Text = AcName1
            lblAppEmpName.Text = AcName
            txtAppEmpCode_Validating(txtAppEmpCode, New System.ComponentModel.CancelEventArgs(False))
            If txtAppEmpCode.Enabled = True Then txtAppEmpCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtAppEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAppEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtAppEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAppEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAppEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtAppEmpCode_DoubleClick(txtAppEmpCode, New System.EventArgs())
    End Sub



    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtDateFrom.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDateFrom.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
            GoTo EventExitSub
        End If

        '    If CDate(txtDateFrom.Text) < CDate(PubCurrDate) Then					
        '        MsgBox "Please From Date Cann't be less than Current Date.", vbInformation					
        '        Cancel = True					
        '        Exit Sub					
        '    End If					


        If txtDateTo.Text = "" Then GoTo EventExitSub

        If chkHalfDay.CheckState = System.Windows.Forms.CheckState.Checked Then
            If CDate(txtDateFrom.Text) <> CDate(txtDateTo.Text) Then
                MsgBox("Please To Date must be same From Date.", MsgBoxStyle.Information)
                Cancel = True
                GoTo EventExitSub
            End If
        Else
            If CDate(txtDateFrom.Text) > CDate(txtDateTo.Text) Then
                MsgBox("Please To Date Cann't be less than From Date.", MsgBoxStyle.Information)
                Cancel = True
                GoTo EventExitSub
            End If
        End If

        If Year(CDate(txtDateFrom.Text)) <> Year(CDate(txtDateTo.Text)) Then
            MsgBox("To Date & From Date Should be Same Year.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If


        If Month(CDate(txtDateFrom.Text)) <> Month(CDate(txtDateTo.Text)) Then
            MsgBox("To Date & From Date Should be Same Month.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If


        txtDays.Text = CStr(DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(txtDateFrom.Text), CDate(txtDateTo.Text)) + IIf(chkHalfDay.CheckState = System.Windows.Forms.CheckState.Checked, 0.5, 1))

        Call FillDate()

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtDateTo.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDateTo.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
            GoTo EventExitSub
        End If

        '    If CDate(txtDateTo.Text) < CDate(PubCurrDate) Then					
        '        MsgBox "Please To Date Cann't be less than Current Date.", vbInformation					
        '        Cancel = True					
        '        Exit Sub					
        '    End If					


        If txtDateFrom.Text = "" Then GoTo EventExitSub

        If chkHalfDay.CheckState = System.Windows.Forms.CheckState.Checked Then
            If CDate(txtDateFrom.Text) <> CDate(txtDateTo.Text) Then
                MsgBox("Please To Date must be same From Date.", MsgBoxStyle.Information)
                Cancel = True
                GoTo EventExitSub
            End If
        Else
            If CDate(txtDateFrom.Text) > CDate(txtDateTo.Text) Then
                MsgBox("Please To Date Cann't be less than From Date.", MsgBoxStyle.Information)
                Cancel = True
                GoTo EventExitSub
            End If
        End If


        If Year(CDate(txtDateFrom.Text)) <> Year(CDate(txtDateTo.Text)) Then
            MsgBox("To Date & From Date Should be Same Year.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If


        If Month(CDate(txtDateFrom.Text)) <> Month(CDate(txtDateTo.Text)) Then
            MsgBox("To Date & From Date Should be Same Month.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If


        txtDays.Text = CStr(DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(txtDateFrom.Text), CDate(txtDateTo.Text)) + IIf(chkHalfDay.CheckState = System.Windows.Forms.CheckState.Checked, 0.5, 1))

        Call FillDate()

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDays.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtReason_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReason.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRecEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRecEmpCode.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtRecEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRecEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim SqlStr As String

        Dim mHODCode As String = ""
        Dim mHRHODCode As String
        Dim mDeptHOD As String = "N"
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(txtRecEmpCode.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        mHRHODCode = GetHRHODCode(txtReqDate.Text)

        If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "IS_DEPT_HOD", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
            mDeptHOD = MasterNo
        End If

        If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_HOD_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
            mHODCode = MasterNo
        Else
            mHODCode = ""
        End If

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE=1"
        SqlStr = SqlStr & vbCrLf _
            & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_HOD_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & txtEmp.Text & "')"

        If mDeptHOD = "N" Then
            If MainClass.ValidateWithMasterTable((txtRecEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
                lblRecEmpName.Text = MasterNo
            Else
                MsgInformation("Invalid Employee Code")
                Cancel = True
            End If
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & mHRHODCode & "'"
            If MainClass.ValidateWithMasterTable((txtRecEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
                lblRecEmpName.Text = MasterNo
            Else
                MsgInformation("Invalid Employee Code")
                Cancel = True
            End If
        End If





        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE=1"
        'SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        'If PubUserLevel = 1 Then

        'ElseIf PubUserLevel = 2 Then
        '    SqlStr = SqlStr & vbCrLf & " AND (EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP_CODE = '" & PubUserEMPCode & "')"
        'Else
        '    SqlStr = SqlStr & vbCrLf & " AND (EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP_CODE = '" & PubUserEMPCode & "')"
        '    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE<>'" & PubUserEMPCode & "'"
        'End If


        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRecEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRecEmpCode.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mHODCode As String = ""
        Dim mHRHODCode As String
        Dim mDeptHOD As String = "N"
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        mHRHODCode = GetHRHODCode(txtReqDate.Text)

        If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "IS_DEPT_HOD", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
            mDeptHOD = MasterNo
        End If

        If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_HOD_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
            mHODCode = MasterNo
        Else
            mHODCode = ""
        End If

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE=1"
        SqlStr = SqlStr & vbCrLf _
            & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_HOD_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & txtEmp.Text & "')"

        If mDeptHOD = "N" Then
            If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE",  ,  , SqlStr) = True Then
                txtRecEmpCode.Text = AcName1
                lblRecEmpName.Text = AcName
                txtRecEmpCode_Validating(txtRecEmpCode, New System.ComponentModel.CancelEventArgs(False))
                If txtRecEmpCode.Enabled = True Then txtRecEmpCode.Focus()
            End If
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & mHRHODCode & "'"
            If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE",  ,  , SqlStr) = True Then
                txtRecEmpCode.Text = AcName1
                lblRecEmpName.Text = AcName
                txtRecEmpCode_Validating(txtRecEmpCode, New System.ComponentModel.CancelEventArgs(False))
                If txtRecEmpCode.Enabled = True Then txtRecEmpCode.Focus()
            End If
        End If




        'If PubUserLevel = 1 Then

        'ElseIf PubUserLevel = 2 Then
        '    SqlStr = SqlStr & vbCrLf & " And (EMP_DEPT_CODE In (Select DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP_CODE = '" & PubUserEMPCode & "')"
        'Else
        '    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_HOD_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & txtEmp.Text & "')"
        '    'SqlStr = SqlStr & vbCrLf & " AND (EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP_CODE = '" & PubUserEMPCode & "')"
        '    'SqlStr = SqlStr & vbCrLf & " AND EMP_CODE<>'" & PubUserEMPCode & "'"
        'End If


        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtRecEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRecEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtRecEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRecEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRecEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtRecEmpCode_DoubleClick(txtRecEmpCode, New System.EventArgs())
    End Sub



    Private Sub txtReqDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReqDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReqDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReqDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtReqDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtReqDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmp.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmp_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmp.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mDept As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        SqlStr = SqlStr & vbCrLf _
            & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        If PubUserLevel = 1 Then

        ElseIf PubUserLevel = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND (EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP_CODE = '" & PubUserEMPCode & "')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & PubUserEMPCode & "'"
        End If

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE",  ,  , SqlStr) = True Then
            txtEmp.Text = AcName1
            lblEmpname.Text = AcName
            txtEmp_Validating(txtEmp, New System.ComponentModel.CancelEventArgs(False))
            If txtEmp.Enabled = True Then txtEmp.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtEmp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmp.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmp.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmp_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmp.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtEmp_DoubleClick(txtEmp, New System.EventArgs())
    End Sub

    Private Sub txtEmp_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmp.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mDeptCode As String
        Dim RsTemp As ADODB.Recordset

        If Trim(txtEmp.Text) = "" Then GoTo EventExitSub
        txtEmp.Text = VB6.Format(txtEmp.Text, "000000")

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        If PubUserLevel = 1 Then

        ElseIf PubUserLevel = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND (EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP_CODE = '" & PubUserEMPCode & "')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & PubUserEMPCode & "'"
        End If

        If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
            lblEmpname.Text = MasterNo
        Else
            MsgInformation("Invalid Employee Code")
            Cancel = True
            GoTo EventExitSub
        End If

        SqlStr = "SELECT EMP_NAME,EMP_EMAILID_OFF,EMP_DEPT_CODE, " & vbCrLf _
            & " GETEMPDESG (" & RsCompany.Fields.Item("COMPANY_CODE").Value & ",'" & Trim(txtEmp.Text) & "'," & vbCrLf _
            & " TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & Trim(UCase(txtEmp.Text)) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            lblEmpname.Text = IIf(IsDBNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)
            lblFromeMailID.Text = IIf(IsDBNull(RsTemp.Fields("EMP_EMAILID_OFF").Value), "", RsTemp.Fields("EMP_EMAILID_OFF").Value)
            mDeptCode = IIf(IsDBNull(RsTemp.Fields("EMP_DEPT_CODE").Value), "", RsTemp.Fields("EMP_DEPT_CODE").Value)
            lblDesgName.Text = IIf(IsDBNull(RsTemp.Fields("DESG_DESC").Value), "", RsTemp.Fields("DESG_DESC").Value)

            If MainClass.ValidateWithMasterTable(mDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblDeptname.Text = MasterNo
            End If
        End If


        Call FillLeaves((txtEmp.Text))

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtReqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReqNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtReqNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReqNo.DoubleClick
        Call cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Private Sub txtReqNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReqNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtReqNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtReqNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtReqNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Public Sub txtReqNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReqNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mReqnum As String

        If Trim(txtReqNo.Text) = "" Then GoTo EventExitSub

        If Len(txtReqNo.Text) < 2 Then
            txtReqNo.Text = Val(txtReqNo.Text) & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        txtReqNo.Text = VB6.Format(txtReqNo.Text, "000000")

        If MODIFYMode = True And RsReqMain.EOF = False Then mReqnum = RsReqMain.Fields("AUTO_KEY_REF").Value

        SqlStr = "Select * From PAY_LEAVE_APP_TRN  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(AUTO_KEY_REF))=" & Val(txtReqNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsReqMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Issue Note, Use Generate Issue Note Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From PAY_LEAVE_APP_TRN  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " LTRIM(RTRIM(AUTO_KEY_REF))=" & Val(mReqnum) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchAppEmp_Click(sender As Object, e As EventArgs) Handles cmdSearchAppEmp.Click
        Call txtAppEmpCode_DoubleClick(txtAppEmpCode, New System.EventArgs())
    End Sub
    Private Sub FormatMain()

        Dim cntCol As Integer
        '    MainClass.ClearGrid sprdHoliday

        Call FillDate()

        With SprdMain
            .MaxCols = ColSH

            .set_RowHeight(0, ConRowHeight * 1.5)
            .set_RowHeight(-1, ConRowHeight * 1.5)
            .Row = -1

            .Col = ColDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColDate, 10)

            For cntCol = ColFH To ColSH
                .Col = cntCol
                .CellType = SS_CELL_TYPE_COMBOBOX
                .TypeComboBoxList = "" & Chr(9) & "0-ABSENT" & Chr(9) & "1-CASUAL" & Chr(9) & "2-EARN"
                .TypeComboBoxList = .TypeComboBoxList & "3-SICK" & Chr(9) & "4-MATERNITY" & Chr(9) & "6-WOPAY"
                .TypeComboBoxList = .TypeComboBoxList & "7-CPLAVAIL" ''& Chr(9) & "8-SUNDAY" & Chr(9) & "9-HOLIDAY"
                .TypeComboBoxCurSel = 0
                .set_ColWidth(cntCol, 13)
            Next

            ''& "5-CPLEARN" & Chr(9) 

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColDate, ColDay)
            MainClass.SetSpreadColor(SprdMain, -1)
            '        sprdHoliday.OperationMode = OperationModeSingle
        End With
    End Sub
    Private Sub FillDate()
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mLastDate As Integer
        Dim mDate As String
        Dim mCurrDate As Date

        '    mLastDate = MainClass.LastDay(Month(txtRefDate.Text), Year(txtRefDate.Text))

        If Trim(txtDateFrom.Text) = "" Then Exit Sub
        If Trim(txtDateTo.Text) = "" Then Exit Sub

        With SprdMain
            .MaxRows = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(txtDateFrom.Text), CDate(txtDateTo.Text)) + 1
            cntRow = 1
            mCurrDate = CDate(txtDateFrom.Text)
            Do While mCurrDate <= CDate(txtDateTo.Text)
                .Row = cntRow
                .Col = ColDate
                .Text = VB6.Format(mCurrDate, "DD/MM/YYYY")

                .Col = ColDay
                .Text = WeekdayName(Weekday(mCurrDate, FirstDayOfWeek.System))

                mCurrDate = DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mCurrDate))
                cntRow = cntRow + 1
            Loop
            '        For cntRow = 1 To mLastDate
            '            .Row = cntRow
            '            .Col = ColDate
            '            mDate = Format(cntRow, "00") & "/" & vb6.Format(txtRefDate.Text, "MM/YYYY")
            '            .Text = Format(mDate, "DD/MM/YYYY")
            '
            '            .Col = ColDay
            '            .Text = WeekdayName(Weekday(mDate, vbUseSystemDayOfWeek))
            '        Next
        End With

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDate, ColDay)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        Dim mListIndex As Integer

        If eventArgs.newRow = -1 Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow
        If eventArgs.col = ColFH Then
            SprdMain.Col = ColFH
            mListIndex = Val(SprdMain.Value)

            SprdMain.Col = ColSH
            If Val(SprdMain.Value) <= 0 Then
                SprdMain.Value = CStr(mListIndex)
            End If
        End If

        '    MainClass.SetFocusToCell SprdMain, SprdMain.Row + 1, ColFH
    End Sub
    Private Function ShowDetail1() As Boolean

        On Error GoTo ShowErrPart
        Dim SqlStr As String = ""
        Dim mEmpCode As String
        Dim mMoveType As String
        Dim cntRow As Integer
        Dim mCode As String
        Dim mRowDate As String
        Dim mAttnDate As String
        Dim mFH As Integer
        Dim mSH As Integer
        Dim RsELeaveDetail As ADODB.Recordset = Nothing

        ShowDetail1 = False
        SqlStr = ""
        SqlStr = " SELECT EMP_CODE,ATTN_DATE, FIRSTHALF , SECONDHALF " & vbCrLf _
            & " FROM PAY_REQ_ATTN_MST WHERE " & vbCrLf _
            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND AUTO_KEY_REF = '" & MainClass.AllowSingleQuote((lblMKey.Text)) & "' AND EMP_CODE ='" & MainClass.AllowSingleQuote((txtEmp.Text)) & "'" & vbCrLf _
            & " AND ATTN_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND ATTN_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY ATTN_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsELeaveDetail, ADODB.LockTypeEnum.adLockReadOnly)
        If RsELeaveDetail.EOF = False Then
            Do While Not RsELeaveDetail.EOF
                mAttnDate = VB6.Format(IIf(IsDBNull(RsELeaveDetail.Fields("ATTN_DATE").Value), "", RsELeaveDetail.Fields("ATTN_DATE").Value), "DD/MM/YYYY")
                mFH = IIf(IsDBNull(RsELeaveDetail.Fields("FIRSTHALF").Value), -1, RsELeaveDetail.Fields("FIRSTHALF").Value)
                mSH = IIf(IsDBNull(RsELeaveDetail.Fields("SECONDHALF").Value), -1, RsELeaveDetail.Fields("SECONDHALF").Value)

                For cntRow = 1 To SprdMain.MaxRows
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColDate
                    mRowDate = VB6.Format(SprdMain.Text, "DD/MM/YYYY")
                    If mAttnDate = mRowDate Then
                        SprdMain.Col = ColFH
                        SprdMain.TypeComboBoxCurSel = mFH + 1

                        SprdMain.Col = ColSH
                        SprdMain.TypeComboBoxCurSel = mSH + 1

                        Exit For
                    End If
                Next
                RsELeaveDetail.MoveNext()
            Loop
            RsELeaveDetail.MoveFirst()
            Call FillLeaves((txtEmp.Text))
        End If
        ShowDetail1 = True
        Exit Function
ShowErrPart:
        MsgBox(Err.Description)
        ShowDetail1 = False
        '    Resume
    End Function
    Private Sub FillLeaves(ByRef mCode As String)

        Dim RsOpLeave As ADODB.Recordset = Nothing
        Dim RsLeave As ADODB.Recordset = Nothing
        Dim mOpSick As Double
        Dim mOpCasual As Double
        Dim mOpEL As Double

        Dim mSick As Double
        Dim mCasual As Double
        Dim mEL As Double
        Dim mCPL As Double
        Dim mCPL_A As Double
        Dim mDOJ As String = ""

        'Dim mMonth As Short
        'Dim mYear As Short
        Dim SqlStr As String
        'Dim I As Integer
        'Dim mMonField As Object
        'Dim mon As String
        Dim mPeriod As Double

        Dim mStartDate As String
        Dim mYearDays As Integer
        Dim xSalDate As String
        Dim mRefDate As String

        If Trim(txtReqDate.Text) = "" Then Exit Sub

        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDOJ = MasterNo
        End If
        xSalDate = MainClass.LastDay(Month(CDate(txtReqDate.Text)), Year(CDate(txtReqDate.Text))) & "/" & VB6.Format(txtReqDate.Text, "MM/YYYY")

        mOpEL = GETEntitleEarnLeave(PubDBCn, mCode, EARN, xSalDate)
        '    mCPL = GETCPL(PubDBCn, mCode, xSalDate)


        If Year(CDate(xSalDate)) = Year(CDate(mDOJ)) Then
            mStartDate = mDOJ
        Else
            mStartDate = "01/01/" & VB6.Format(xSalDate, "YYYY")
        End If

        mYearDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate("01/01/" & VB6.Format(xSalDate, "YYYY")), CDate("31/12/" & VB6.Format(xSalDate, "YYYY"))) + 1

        mPeriod = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(xSalDate)) + 1
        mPeriod = CDbl(VB6.Format(mPeriod / mYearDays, "0.00000"))

        mRefDate = txtReqDate.Text

        mOpSick = GetOpeningLeaves(mCode, mRefDate, SICK, "Y", "Y", "")
        mOpCasual = GetOpeningLeaves(mCode, mRefDate, CASUAL, "Y", "Y", "")
        'mOpML = GetOpeningLeaves(mCode, mRefDate, MATERNITY, "Y", "Y", "")

        mOpEL = GetOpeningLeaves(mCode, mRefDate, EARN, "Y", "Y", "")
        mCPL = GetOpeningLeaves(mCode, mRefDate, CPLEARN, "Y", "Y", "")

        '    mPeriod = Round(Month(lblDate.Caption) / 12, 2)

        'SqlStr = " SELECT NVL(OPENING,0) AS OPENING, NVL(TOTENTITLE,0) AS  TOTENTITLE, LEAVECODE " & vbCrLf _
        '    & " FROM PAY_OPLEAVE_TRN " & vbCrLf _
        '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND PAYYEAR =" & Year(CDate(txtReqDate.Text)) & "" & vbCrLf _
        '    & " AND EMP_CODE ='" & mCode & "'"

        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpLeave, ADODB.LockTypeEnum.adLockOptimistic)

        'If RsOpLeave.EOF = False Then
        '    Do While Not RsOpLeave.EOF
        '        If RsOpLeave.Fields("LeaveCode").Value = SICK Then
        '            mOpSick = IIf(IsDBNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
        '            mOpSick = mOpSick + IIf(IsDBNull(RsOpLeave.Fields("TOTENTITLE").Value), 0, RsOpLeave.Fields("TOTENTITLE").Value) * mPeriod ''(GetLeaveEntitle(Val(RsOpLeave!LeaveCode)) * mPeriod)
        '            mOpSick = System.Math.Round(mOpSick * 2, 0) / 2
        '        ElseIf RsOpLeave.Fields("LeaveCode").Value = CASUAL Then
        '            mOpCasual = IIf(IsDBNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
        '            mOpCasual = mOpCasual + IIf(IsDBNull(RsOpLeave.Fields("TOTENTITLE").Value), 0, RsOpLeave.Fields("TOTENTITLE").Value) * mPeriod
        '            mOpCasual = System.Math.Round(mOpCasual * 2, 0) / 2
        '        ElseIf RsOpLeave.Fields("LeaveCode").Value = EARN Then
        '            mOpEL = mOpEL + IIf(IsDBNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
        '        ElseIf RsOpLeave.Fields("LeaveCode").Value = CPLEARN Then
        '            mCPL = mCPL + IIf(IsDBNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
        '        End If

        '        RsOpLeave.MoveNext()
        '    Loop
        'End If

        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PAY_ATTN_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PAYYEAR =" & Year(CDate(txtReqDate.Text)) & "" & vbCrLf & " AND EMP_CODE ='" & mCode & "'"

        SqlStr = SqlStr & vbCrLf & "AND ATTN_DATE<=TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsLeave.EOF = False Then
            Do While Not RsLeave.EOF
                If RsLeave.Fields("FIRSTHALF").Value = SICK And RsLeave.Fields("SECONDHALF").Value = SICK Then
                    mSick = mSick + 1
                ElseIf RsLeave.Fields("FIRSTHALF").Value = SICK Or RsLeave.Fields("SECONDHALF").Value = SICK Then
                    mSick = mSick + 0.5
                End If

                If RsLeave.Fields("FIRSTHALF").Value = CASUAL And RsLeave.Fields("SECONDHALF").Value = CASUAL Then
                    mCasual = mCasual + 1
                ElseIf RsLeave.Fields("FIRSTHALF").Value = CASUAL Or RsLeave.Fields("SECONDHALF").Value = CASUAL Then
                    mCasual = mCasual + 0.5
                End If

                If RsLeave.Fields("FIRSTHALF").Value = EARN And RsLeave.Fields("SECONDHALF").Value = EARN Then
                    mEL = mEL + 1
                ElseIf RsLeave.Fields("FIRSTHALF").Value = EARN Or RsLeave.Fields("SECONDHALF").Value = EARN Then
                    mEL = mEL + 0.5
                End If

                '            If RsLeave!FIRSTHALF = CPLEARN And RsLeave!SECONDHALF = CPLEARN Then
                '                mCPL = mCPL + 1
                '            ElseIf RsLeave!FIRSTHALF = CPLEARN Or RsLeave!SECONDHALF = CPLEARN Then
                '                mCPL = mCPL + 0.5
                '            End If

                If RsLeave.Fields("FIRSTHALF").Value = CPLAVAIL And RsLeave.Fields("SECONDHALF").Value = CPLAVAIL Then
                    mCPL_A = mCPL_A + 1
                ElseIf RsLeave.Fields("FIRSTHALF").Value = CPLAVAIL Or RsLeave.Fields("SECONDHALF").Value = CPLAVAIL Then
                    mCPL_A = mCPL_A + 0.5
                End If

                mCPL = mCPL + (IIf(IsDBNull(RsLeave.Fields("CPL_EARN").Value), 0, RsLeave.Fields("CPL_EARN").Value) * 0.5)
                RsLeave.MoveNext()
            Loop
        End If

        lblBalSL.Text = VB6.Format(mOpSick - mSick, "0.0")
        lblBalCL.Text = VB6.Format(mOpCasual - mCasual, "0.0")
        lblBalEL.Text = VB6.Format(mOpEL - mEL, "0.0")
        lblBalCPL.Text = VB6.Format(mCPL - mCPL_A, "0.0")

        lblAvlSL.Text = VB6.Format(mSick, "0.0")
        lblAvlCL.Text = VB6.Format(mCasual, "0.0")
        lblAvlEL.Text = VB6.Format(mEL, "0.0")
        lblAvlCPL.Text = VB6.Format(mCPL_A, "0.0")

    End Sub
    Private Function ValidLeaveInGrid(ByRef CheckCol As Integer, Optional ByRef InvalidMsg As String = "") As Boolean

        On Error GoTo ERR1
        Static I As Object
        Static j As Integer
        With SprdMain
            j = .MaxRows
            If j = 0 Then MsgBox(InvalidMsg) : ValidLeaveInGrid = False : Exit Function
            For I = 1 To j
                .Row = I
                .Col = 0

                .Col = CheckCol

                If .Text <> "" Then
                    ValidLeaveInGrid = True
                Else
                    ValidLeaveInGrid = False
                    GoTo DspMsg
                End If
            Next I
        End With
        ValidLeaveInGrid = True
        Exit Function
DspMsg:
        'Resume
        If InvalidMsg = "" Then
            MsgInformation("Not a valid Leave")
            MainClass.SetFocusToCell(SprdMain, I, CheckCol)
        Else
            '    Resume
            MsgInformation(InvalidMsg)
            MainClass.SetFocusToCell(SprdMain, I, CheckCol)
        End If
        'Resume
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Class
