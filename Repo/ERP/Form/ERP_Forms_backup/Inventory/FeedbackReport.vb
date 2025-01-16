Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmFeedbackReport
    Inherits System.Windows.Forms.Form
    Dim RsFeedBackMain As ADODB.Recordset
    Dim RsFeedBackDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Private Const ConRowHeight As Short = 14

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Private Sub cboShift_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShift.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboShift_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShift.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboShift2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShift2.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboShift2_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShift2.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtDocNo.Enabled = False
        Else
            CmdAdd.Text = ConCmdAddCaption
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
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart

        Dim mItemCode As String

        If Trim(txtDocNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If OptStatus(1).Checked = True Then
            MsgInformation("Status Closed, Cann't be Deleted.")
            Exit Sub
        End If

        If Not RsFeedBackMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_FEEDBACK_HDR", (lblMKey.Text), RsFeedBackMain, "MKEY") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_FEEDBACK_HDR", "MKEY", (lblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("Delete from INV_FEEDBACK_DET Where MKEY='" & Val(lblMKey.Text) & "'")
                PubDBCn.Execute("Delete from INV_FEEDBACK_HDR Where MKEY='" & Val(lblMKey.Text) & "'")

                PubDBCn.CommitTrans()
                RsFeedBackMain.Requery()
                RsFeedBackDetail.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsFeedBackMain.Requery()
        RsFeedBackDetail.Requery()
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub cmdDept2Search_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDept2Search.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept2Code.Text = AcName1
            txtDept2Name.Text = AcName
            txtDept2Code_Validating(txtDept2Code, New System.ComponentModel.CancelEventArgs(False))
            If txtDept2Code.Enabled = True Then txtDept2Code.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdItemCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdItemCode.Click
        Call SearchCode()
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr
        If OptStatus(1).Checked = True Then
            MsgInformation("Status Closed, Cann't be Modified")
            Exit Sub
        End If

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsFeedBackMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtDocNo.Enabled = False
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

    Private Sub cmdRaisedSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRaisedSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtRaisedBy.Text), "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , SqlStr) = True Then
            txtRaisedBy.Text = AcName
            txtRaisedName.Text = AcName1
            txtRaisedBy_Validating(txtRaisedBy, New System.ComponentModel.CancelEventArgs(False))
            If txtRaisedBy.Enabled = True Then txtRaisedBy.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONFeedBack(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONFeedBack(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportONFeedBack(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""

        Call MainClass.ClearCRptFormulas(Report1)

        Call SelectQryForReport(SqlStr)


        mTitle = "FEEDBACK REPORT"
        mSubTitle = ""
        mRptFileName = "FeedBackReport.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function SelectQryForReport(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*, INVMST.ITEM_SHORT_DESC"

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_FEEDBACK_HDR IH, " & vbCrLf & " INV_FEEDBACK_DET ID, INV_ITEM_MST INVMST, " & vbCrLf & " PAY_EMPLOYEE_MST A, PAY_EMPLOYEE_MST b  "

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IH.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=A.COMPANY_CODE" & vbCrLf & " AND IH.EMP_RAISED_CODE=A.EMP_CODE" & vbCrLf & " AND IH.COMPANY_CODE=B.COMPANY_CODE(+)" & vbCrLf & " AND IH.EMP_RECD_CODE=B.EMP_CODE(+)" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.MKEY =" & Val(lblMKey.Text) & ""


        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY IH.REF_NO,ID.TYPE,ID.SUBROWNO"

        SelectQryForReport = mSqlStr
    End Function
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtDocno_Validating(txtDocno, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
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


    Private Sub cmdDeptSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDeptSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDeptCode.Text = AcName1
            txtDeptName.Text = AcName
            txtDeptCode_Validating(txtDeptCode, New System.ComponentModel.CancelEventArgs(False))
            If txtDeptCode.Enabled = True Then txtDeptCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdRecdSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRecdSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtRecdBy.Text), "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , SqlStr) = True Then
            txtRecdBy.Text = AcName
            txtRecdName.Text = AcName1
            txtRecdBy_Validating(txtRecdBy, New System.ComponentModel.CancelEventArgs(False))
            If txtRecdBy.Enabled = True Then txtRecdBy.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub FrmFeedbackReport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub OptStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptStatus.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        On Error GoTo ErrPart

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, 1)
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent

        SprdMain.Col = SprdMain.ActiveCol
        SprdMain.Row = SprdMain.ActiveRow
        If Trim(SprdMain.Text) <> "" Then
            MainClass.AddBlankSprdRow(SprdMain, 1, ConRowHeight)
            '       FormatSprdMainII SprdMainII.MaxRows
        End If
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Select Case eventArgs.Col
            Case 1
                If Trim(SprdMain.Text) <> "" Then
                    MainClass.AddBlankSprdRow(SprdMain, 1, ConRowHeight)
                    FormatSprdMain(-1)
                End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMainII_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMainII.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMainII_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMainII.ClickEvent

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMainII, eventArgs.Row, 1)
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMainII_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMainII.KeyUpEvent

        SprdMainII.Col = SprdMainII.ActiveCol
        SprdMainII.Row = SprdMainII.ActiveRow
        If Trim(SprdMainII.Text) <> "" Then
            MainClass.AddBlankSprdRow(SprdMainII, 1, ConRowHeight)
            '       FormatSprdMainII SprdMainII.MaxRows
        End If
    End Sub

    Private Sub SprdMainII_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMainII.LeaveCell

        On Error GoTo ErrPart
        Select Case eventArgs.Col
            Case 1
                If Trim(SprdMainII.Text) <> "" Then
                    MainClass.AddBlankSprdRow(SprdMainII, 1, ConRowHeight)
                    FormatSprdMainII(-1)
                End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdView.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.row
            .Col = 1
            txtDocNo.Text = .Text
            txtDocno_Validating(txtDocno, New System.ComponentModel.CancelEventArgs(True))
            If txtDocNo.Enabled = True Then txtDocNo.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function AutoGenSeqNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = "SELECT Max(REF_NO)  " & vbCrLf & " FROM INV_FEEDBACK_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDbNull(.Fields(0).Value) Then
                    mNewSeqNo = .Fields(0).Value
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = 1
                End If
            Else
                mNewSeqNo = 1
            End If
        End With
        AutoGenSeqNo = CStr(mNewSeqNo)
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mVNoSeq As Double
        Dim mStatus As String
        Dim mShift1 As String
        Dim mShift2 As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mStatus = IIf(OptStatus(0).Checked = True, "O", "C")

        If Val(txtDocNo.Text) = 0 Then
            mVNoSeq = CDbl(AutoGenSeqNo())
        Else
            mVNoSeq = Val(txtDocNo.Text)
        End If

        txtDocNo.Text = CStr(Val(CStr(mVNoSeq)))

        mShift1 = VB.Left(cboShift.Text, 1)
        mShift2 = VB.Left(cboShift2.Text, 1)

        SqlStr = ""
        If ADDMode = True Then
            lblMKey.Text = mVNoSeq & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")

            SqlStr = "INSERT INTO INV_FEEDBACK_HDR (" & vbCrLf & " MKEY, COMPANY_CODE, " & vbCrLf & " FYEAR, " & vbCrLf & " REF_NO, REF_DATE, " & vbCrLf & " EMP_RAISED_CODE, DEPT_CODE, SHIFT_CODE, " & vbCrLf & " EMP_RECD_CODE, DEPT_CODE2, SHIFT_CODE2, " & vbCrLf & " ITEM_CODE, QTY, " & vbCrLf & " STATUS, STATUSDATE, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE)" & vbCrLf
            SqlStr = SqlStr & vbCrLf & " VALUES( " & vbCrLf & " '" & lblMKey.Text & "'," & RsCompany.Fields("Company_Code").Value & "," & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf & " " & Val(CStr(mVNoSeq)) & ", TO_DATE('" & VB6.Format(Trim(txtDate.Text), "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtRaisedBy.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtDeptCode.Text)) & "', " & vbCrLf & " '" & mShift1 & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtRecdBy.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtDept2Code.Text)) & "', " & vbCrLf & " '" & mShift2 & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtItemCode.Text)) & "'," & vbCrLf & " " & Val(txtQtyInvolved.Text) & ", " & vbCrLf & " '" & mStatus & "', " & vbCrLf & " TO_DATE('" & VB6.Format(Trim(txtStatusDate.Text), "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""

            SqlStr = "UPDATE INV_FEEDBACK_HDR SET " & vbCrLf & " REF_DATE=TO_DATE('" & VB6.Format(Trim(txtDate.Text), "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " EMP_RAISED_CODE='" & MainClass.AllowSingleQuote((txtRaisedBy.Text)) & "', " & vbCrLf & " DEPT_CODE='" & MainClass.AllowSingleQuote((txtDeptCode.Text)) & "', " & vbCrLf & " SHIFT_CODE='" & mShift1 & "', " & vbCrLf & " EMP_RECD_CODE='" & MainClass.AllowSingleQuote((txtRecdBy.Text)) & "', " & vbCrLf & " DEPT_CODE2='" & MainClass.AllowSingleQuote((txtDept2Code.Text)) & "', " & vbCrLf & " SHIFT_CODE2='" & mShift2 & "', " & vbCrLf & " ITEM_CODE='" & MainClass.AllowSingleQuote((txtItemCode.Text)) & "', " & vbCrLf & " QTY=" & Val(txtQtyInvolved.Text) & ", " & vbCrLf & " STATUS='" & mStatus & "', " & vbCrLf & " STATUSDATE=TO_DATE('" & VB6.Format(Trim(txtStatusDate.Text), "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND MKEY =" & Val(lblMKey.Text) & ""

        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1 = False Then GoTo ErrPart

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsFeedBackMain.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function

    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mDesc As String


        SqlStr = "Delete From  INV_FEEDBACK_DET " & vbCrLf & " Where " & vbCrLf & " MKEY=" & Val(lblMKey.Text) & "" & vbCrLf

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = 1
                mDesc = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mDesc <> "" Then
                    SqlStr = " INSERT INTO INV_FEEDBACK_DET ( " & vbCrLf & " MKEY, SUBROWNO, DESPRIPTION, " & vbCrLf & " TYPE) "

                    SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMKey.Text) & "," & I & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mDesc) & "',1) "

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        With SprdMainII
            For I = 1 To .MaxRows
                .Row = I

                .Col = 1
                mDesc = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mDesc <> "" Then
                    SqlStr = " INSERT INTO INV_FEEDBACK_DET ( " & vbCrLf & " MKEY, SUBROWNO, DESPRIPTION, " & vbCrLf & " TYPE) "

                    SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMKey.Text) & "," & I & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mDesc) & "',2) "

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer

        FieldsVarification = True
        If ValidateBranchLocking((txtDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsFeedBackMain.EOF = True Then Exit Function

        If MODIFYMode = True And txtDocNo.Text = "" Then
            MsgInformation("Ref No. Cann't Blank")
            FieldsVarification = False
            Exit Function
        End If

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

        If Trim(txtStatusDate.Text) <> "" Then
            If Not IsDate(txtStatusDate.Text) Then
                MsgBox("Date is not valid", MsgBoxStyle.Information)
                FieldsVarification = False
                txtStatusDate.Focus()
                Exit Function
            End If
        End If

        If Trim(txtDeptCode.Text) = "" Then
            MsgBox("Dept code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDeptCode.Focus()
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable((txtDeptCode.Text), "DEPT_CODE", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Invalid Dept code.", MsgBoxStyle.Information)
                FieldsVarification = False
                txtDeptCode.Focus()
                Exit Function
            End If
        End If

        If Trim(txtItemCode.Text) = "" Then
            MsgBox("Item code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtItemCode.Focus()
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable((txtItemCode.Text), "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Invalid Item code.", MsgBoxStyle.Information)
                FieldsVarification = False
                txtItemCode.Focus()
                Exit Function
            End If
        End If


        If Trim(txtRaisedBy.Text) = "" Then
            MsgBox("Prepared code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtRaisedBy.Focus()
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable((txtRaisedBy.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Invalid Emp code.", MsgBoxStyle.Information)
                FieldsVarification = False
                txtRaisedBy.Focus()
                Exit Function
            End If
        End If

        If Trim(txtRecdBy.Text) = "" Then
            MsgBox("Authority code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtRecdBy.Focus()
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable((txtRecdBy.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Invalid Emp code.", MsgBoxStyle.Information)
                FieldsVarification = False
                txtRecdBy.Focus()
                Exit Function
            End If
        End If

        If MainClass.ValidDataInGrid(SprdMain, 1, "S", "Problem Statement is must.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmFeedbackReport_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        SqlStr = ""
        SqlStr = "Select * from INV_FEEDBACK_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFeedBackMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from INV_FEEDBACK_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFeedBackDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
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
        Dim SqlStr As String = ""

        SqlStr = ""

        ''SELECT CLAUSE...

        SqlStr = " SELECT TO_CHAR(REF_NO) AS REF_NO, REF_DATE, IH.ITEM_CODE, INVMST.ITEM_SHORT_DESC,DECODE(STATUS,'O','OPEN','CLOSED') AS STATUS " & vbCrLf & " FROM INV_FEEDBACK_HDR IH, INV_ITEM_MST INVMST "

        ''WHERE CLAUSE...

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND IH.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        ''ORDER BY CLAUSE...

        SqlStr = SqlStr & vbCrLf & " Order by REF_NO, REF_DATE"

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

            .set_ColWidth(0, 500)
            .set_ColWidth(1, 1000)
            .set_ColWidth(2, 1000)
            .set_ColWidth(3, 900)
            .set_ColWidth(4, 4000)
            .set_ColWidth(5, 3000)



            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsFeedBackMain

            txtDocNo.Maxlength = .Fields("REF_NO").Precision
            txtDate.Maxlength = 10
            txtItemCode.Maxlength = .Fields("ITEM_CODE").DefinedSize
            txtDeptCode.Maxlength = .Fields("DEPT_CODE").DefinedSize
            txtRaisedBy.Maxlength = .Fields("EMP_Raised_CODE").DefinedSize
            txtRecdBy.Maxlength = .Fields("EMP_RECD_CODE").DefinedSize

            txtDept2Code.Maxlength = .Fields("DEPT_CODE2").DefinedSize
            txtQtyInvolved.Maxlength = .Fields("QTY").Precision
            txtStatusDate.Maxlength = 10

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing

        With RsFeedBackMain
            If Not .EOF Then
                txtDocNo.Enabled = False
                lblMKey.Text = .Fields("MKEY").Value

                txtDocNo.Text = IIf(IsDbNull(.Fields("REF_NO").Value), 0, .Fields("REF_NO").Value)
                txtDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")

                txtItemCode.Text = IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                txtQtyInvolved.Text = IIf(IsDbNull(.Fields("QTY").Value), "", .Fields("QTY").Value)
                OptStatus(0).Checked = IIf(.Fields("Status").Value = "O", True, False)
                OptStatus(1).Checked = IIf(.Fields("Status").Value = "C", True, False)

                If OptStatus(1).Checked = True Then
                    fraStatus.Enabled = False
                End If

                txtStatusDate.Text = VB6.Format(IIf(IsDbNull(.Fields("STATUSDATE").Value), "", .Fields("STATUSDATE").Value), "DD/MM/YYYY")

                If MainClass.ValidateWithMasterTable((txtItemCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtItemDesc.Text = MasterNo
                End If
                txtItemCode.Enabled = False
                cmdItemCode.Enabled = False

                txtDeptCode.Text = IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)
                If MainClass.ValidateWithMasterTable((txtDeptCode.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtDeptName.Text = MasterNo
                End If

                txtDept2Code.Text = IIf(IsDbNull(.Fields("DEPT_CODE2").Value), "", .Fields("DEPT_CODE2").Value)
                If MainClass.ValidateWithMasterTable((txtDept2Code.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtDept2Name.Text = MasterNo
                End If

                txtDeptCode.Enabled = True
                cmdDeptSearch.Enabled = True


                txtRaisedBy.Text = IIf(IsDbNull(.Fields("EMP_RAISED_CODE").Value), "", .Fields("EMP_RAISED_CODE").Value)
                If MainClass.ValidateWithMasterTable((txtRaisedBy.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtRaisedName.Text = MasterNo
                End If

                txtRecdBy.Text = IIf(IsDbNull(.Fields("EMP_RECD_CODE").Value), "", .Fields("EMP_RECD_CODE").Value)
                If MainClass.ValidateWithMasterTable((txtRecdBy.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtRecdName.Text = MasterNo
                End If

                If IsDbNull(.Fields("SHIFT_CODE").Value) Then
                    cboShift.SelectedIndex = -1
                Else
                    If .Fields("SHIFT_CODE").Value = "G" Then
                        cboShift.Text = "General"
                    ElseIf .Fields("SHIFT_CODE").Value = "A" Then
                        cboShift.Text = "A-Shift"
                    ElseIf .Fields("SHIFT_CODE").Value = "B" Then
                        cboShift.Text = "B-Shift"
                    ElseIf .Fields("SHIFT_CODE").Value = "C" Then
                        cboShift.Text = "C-Shift"
                    End If
                End If

                If IsDbNull(.Fields("SHIFT_CODE2").Value) Then
                    cboShift2.SelectedIndex = -1
                Else
                    If .Fields("SHIFT_CODE2").Value = "G" Then
                        cboShift2.Text = "General"
                    ElseIf .Fields("SHIFT_CODE2").Value = "A" Then
                        cboShift2.Text = "A-Shift"
                    ElseIf .Fields("SHIFT_CODE2").Value = "B" Then
                        cboShift2.Text = "B-Shift"
                    ElseIf .Fields("SHIFT_CODE2").Value = "C" Then
                        cboShift2.Text = "C-Shift"
                    End If
                End If

                Call ShowDetail1()
            End If


        End With
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsFeedBackMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        '    FormatSprdMain -1

        txtDocNo.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim j As Integer
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_FEEDBACK_DET " & vbCrLf & " Where " & vbCrLf & " MKEY=" & Val(lblMKey.Text) & "" & vbCrLf & " Order By TYPE, SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFeedBackDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsFeedBackDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1
            I = 1
            j = 1
            '        .MoveFirst

            Do While Not .EOF

                If .Fields("TYPE").Value = 1 Then
                    SprdMain.Row = I

                    SprdMain.Col = 1
                    SprdMain.Text = Trim(IIf(IsDbNull(.Fields("DESPRIPTION").Value), "", .Fields("DESPRIPTION").Value))
                    I = I + 1
                    SprdMain.MaxRows = I
                Else
                    SprdMainII.Row = j

                    SprdMainII.Col = 1
                    SprdMainII.Text = Trim(IIf(IsDbNull(.Fields("DESPRIPTION").Value), "", .Fields("DESPRIPTION").Value))
                    j = j + 1
                    SprdMainII.MaxRows = j
                End If

                .MoveNext()
            Loop
        End With
        FormatSprdMain(-1)
        FormatSprdMainII(-1)
        Exit Sub
ERR1:
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
        MainClass.ButtonStatus(Me, XRIGHT, RsFeedBackMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()


        lblMKey.Text = ""

        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDocNo.Text = ""

        txtItemCode.Text = ""
        txtItemDesc.Text = ""
        txtDeptCode.Text = ""
        txtDeptName.Text = ""
        txtRaisedBy.Text = ""
        txtRaisedName.Text = ""
        txtRecdBy.Text = ""
        txtRecdName.Text = ""

        txtDept2Code.Text = ""
        txtDept2Name.Text = ""
        txtQtyInvolved.Text = ""
        OptStatus(0).Checked = True
        txtStatusDate.Text = ""
        cboShift.SelectedIndex = 0
        cboShift2.SelectedIndex = 0

        cmdItemCode.Enabled = True
        txtItemCode.Enabled = True

        cmdDeptSearch.Enabled = True
        txtDeptCode.Enabled = True

        cmdRaisedSearch.Enabled = True
        txtRaisedBy.Enabled = True

        cmdRecdSearch.Enabled = True
        txtRecdBy.Enabled = True

        txtDate.Enabled = False
        fraStatus.Enabled = True

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        MainClass.ClearGrid(SprdMainII, ConRowHeight)
        FormatSprdMainII(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsFeedBackMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsFeedBackDetail.Fields("DESPRIPTION").DefinedSize ''
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 39)

            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdMainII(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdMainII
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsFeedBackDetail.Fields("DESPRIPTION").DefinedSize ''
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 39)

            MainClass.SetSpreadColor(SprdMainII, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FrmFeedbackReport_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmFeedbackReport_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Public Sub FrmFeedbackReport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Call SetMainFormCordinate(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        FillComboMst()

        AdoDCMain.Visible = False
        txtDocNo.Enabled = True
        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtDept2Code_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept2Code.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDeptName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeptName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtQtyInvolved_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtQtyInvolved.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtQtyInvolved_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtQtyInvolved.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRecdBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRecdBy.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRecdBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRecdBy.DoubleClick
        Call cmdRecdSearch_Click(cmdRecdSearch, New System.EventArgs())
    End Sub

    Private Sub txtRecdBy_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRecdBy.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRecdBy.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRecdBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRecdBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtRecdBy_DoubleClick(txtRecdBy, New System.EventArgs())
    End Sub

    Private Sub txtRecdBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRecdBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtRecdBy.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtRecdBy.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtRecdName.Text = MasterNo
        Else
            MsgInformation("Invalid Employee Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.DoubleClick
        Call SearchCode()
    End Sub
    Private Sub SearchCode()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'"

        If MainClass.SearchGridMaster((txtItemCode.Text), "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , SqlStr) = True Then
            txtItemCode.Text = Trim(AcName)
            txtItemDesc.Text = Trim(AcName1)
            txtItemCode_Validating(txtItemCode, New System.ComponentModel.CancelEventArgs(False))

        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCode()
    End Sub

    Private Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsItemMast As ADODB.Recordset = Nothing
        If Trim(txtItemCode.Text) = "" Then GoTo EventExitSub

        SqlStr = "Select ITEM_SHORT_DESC From INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote((txtItemCode.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemMast, ADODB.LockTypeEnum.adLockReadOnly)

        If RsItemMast.EOF = False Then
            txtItemDesc.Text = IIf(IsDbNull(RsItemMast.Fields("ITEM_SHORT_DESC").Value), "", RsItemMast.Fields("ITEM_SHORT_DESC").Value)
        Else
            MsgBox("Invalid Item Code", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemDesc.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRaisedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRaisedBy.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRaisedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRaisedBy.DoubleClick
        Call cmdRaisedSearch_Click(cmdRaisedSearch, New System.EventArgs())
    End Sub

    Private Sub txtRaisedBy_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRaisedBy.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRaisedBy.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRaisedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRaisedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtRaisedBy_DoubleClick(txtRaisedBy, New System.EventArgs())
    End Sub

    Private Sub txtRaisedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRaisedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtRaisedBy.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtRaisedBy.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtRaisedName.Text = MasterNo
        Else
            MsgInformation("Invalid Employee Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDeptCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeptCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDeptCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeptCode.DoubleClick
        Call cmdDeptSearch_Click(cmdDeptSearch, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtDeptCode_DoubleClick(txtDeptCode, New System.EventArgs())
    End Sub

    Private Sub txtDeptCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeptCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtDeptCode.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtDeptCode.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtDeptName.Text = MasterNo
        Else
            MsgInformation("Invalid Dept Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDept2Code_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept2Code.DoubleClick
        Call cmdDept2Search_Click(cmdDept2Search, New System.EventArgs())
    End Sub

    Private Sub txtDept2Code_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept2Code.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept2Code.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDept2Code_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept2Code.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtDept2Code_DoubleClick(txtDept2Code, New System.EventArgs())
    End Sub

    Private Sub txtDept2Code_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept2Code.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtDept2Code.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtDept2Code.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtDept2Name.Text = MasterNo
        Else
            MsgInformation("Invalid Dept Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDocno_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDocNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDocno_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDocNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDocNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDocno_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDocNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mMKEY As String = ""

        If Trim(txtDocNo.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsFeedBackMain.EOF = False Then mMKEY = RsFeedBackMain.Fields("MKEY").Value

        SqlStr = "Select * From INV_FEEDBACK_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND REF_NO=" & Val(txtDocNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFeedBackMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsFeedBackMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Ref No, Use Generate New Doc Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_FEEDBACK_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " MKEY='" & Val(mMKEY) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFeedBackMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtStatusDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStatusDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub FillComboMst()

        cboShift.Items.Clear()
        cboShift.Items.Add("General")
        cboShift.Items.Add("A-Shift")
        cboShift.Items.Add("B-Shift")
        cboShift.Items.Add("C-Shift")
        cboShift.SelectedIndex = 0

        cboShift2.Items.Clear()
        cboShift2.Items.Add("General")
        cboShift2.Items.Add("A-Shift")
        cboShift2.Items.Add("B-Shift")
        cboShift2.Items.Add("C-Shift")
        cboShift2.SelectedIndex = 0

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtStatusDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtStatusDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtStatusDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtStatusDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
