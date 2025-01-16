Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamLeaveRegHOD
    Inherits System.Windows.Forms.Form
    Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColMKEY As Short = 1
    Private Const ColRefNo As Short = 2
    Private Const ColRefDate As Short = 3
    Private Const ColEmpCode As Short = 4
    Private Const ColEmpName As Short = 5
    Private Const ColFromDate As Short = 6
    Private Const ColToDate As Short = 7
    Private Const ColLDays As Short = 8
    Private Const ColRecName As Short = 9
    Private Const ColAppCode As Short = 10
    Private Const ColAppEmpName As Short = 11
    Private Const ColReason As Short = 12
    Private Const ColAppStatus As Short = 13
    Private Const ColRejStatus As Short = 14

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .MaxCols = ColRejStatus

            .Row = 0
            SetColHeadings()
            .Row = Arow

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColMKEY, 12)
            .ColHidden = True

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColRefNo, 6)
            .ColHidden = False

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColRefDate, 9)
            ''.ColHidden = True

            .Col = ColEmpCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColEmpCode, 9)
            .ColHidden = False

            .Col = ColEmpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColEmpName, 25)
            .ColHidden = False

            .Col = ColFromDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColFromDate, 11)
            .ColHidden = False

            .Col = ColToDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColToDate, 11)
            .ColHidden = False

            .ColsFrozen = ColFromDate

            .Col = ColLDays
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColLDays, 5)
            .ColHidden = False

            .Col = ColRecName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColRecName, 20)
            .ColHidden = False

            .Col = ColAppCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColAppCode, 9)
            .ColHidden = True

            .Col = ColAppEmpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColAppEmpName, 25)
            .ColHidden = True

            .Col = ColReason
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColReason, 15)
            .ColHidden = False

            .Col = ColAppStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColAppStatus, 6)

            .Col = ColRejStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColRejStatus, 6)

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMKEY, ColReason)
        End With
        MainClass.SetSpreadColor(SprdMain, Arow)
        '    SprdMain.SetOddEvenRowColor &HC0FFFF, vbBlack, &HFFFFC0, vbBlack
        SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
    End Sub
    Private Sub SetColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColMKEY
            .Text = "MKEY"

            .Col = ColRefNo
            .Text = "Ref No"

            .Col = ColRefDate
            .Text = "Ref Date"

            .Col = ColEmpCode
            .Text = "Emp Code"

            .Col = ColEmpName
            .Text = "Emp Name"

            .Col = ColFromDate
            .Text = "Leave From Date"

            .Col = ColToDate
            .Text = "Leave To Date"

            .Col = ColLDays
            .Text = "Total Leave Days"

            .Col = ColRecName
            .Text = "Recommended By"

            .Col = ColAppCode
            .Text = "Approved By"

            .Col = ColAppEmpName
            .Text = "Approved By"

            .Col = ColReason
            .Text = "Reason"

            .Col = ColAppStatus
            .Text = "Approved"

            .Col = ColRejStatus
            .Text = "Rejected"

            .set_RowHeight(0, 20)
        End With
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        txtAppEmpName.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo AddErr

        Clear1()

        Show1()
        Call FormatSprdMain(-1)

        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldsVerification = False Then GoTo NoValidate

        If Update1 = False Then GoTo ErrPart

        cmdSave.Enabled = False


NoValidate:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        If Err.Number <> 0 Then
            ErrorMsg(CStr(Err.Number), Err.Description, MsgBoxStyle.Critical)
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmParamLeaveRegHOD_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

        If FormActive = True Then Exit Sub
        txtAppEmpName.Text = ""
        If MainClass.ValidateWithMasterTable(PubUserEMPCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtAppEmpName.Text = Trim(MasterNo)
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmParamLeaveRegHOD_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmParamLeaveRegHOD_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False


        txtAppEmpName.Enabled = True
        chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmParamLeaveRegHOD_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame2.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        FraFront.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))

        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        On Error GoTo ClearErr

        cmdSave.Enabled = True
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        Exit Sub
ClearErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub Show1()

        On Error GoTo LedgError
        Dim SqlStr As String = ""

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVerification() As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mAppStatus As String
        Dim mRejStatus As String
        Dim mAppCode As String

        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then

        Else
            If Trim(txtAppEmpName.Text) = "" Then
                MsgInformation("Invaild Employee Name")
                If txtAppEmpName.Enabled = True Then txtAppEmpName.Focus()
                FieldsVerification = False
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable((txtAppEmpName.Text), "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Employee Name")
                If txtAppEmpName.Enabled = True Then txtAppEmpName.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColAppStatus
                mAppStatus = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "A", "O")

                .Col = ColRejStatus
                mRejStatus = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "R", "O")

                .Col = ColAppCode
                mAppCode = Trim(.Text)



                If mAppStatus = "A" And mRejStatus = "R" Then
                    MainClass.SetFocusToCell(SprdMain, I, ColRejStatus, "Select Either Approved or Reject.")
                    FieldsVerification = False
                    Exit Function
                End If

                If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then

                Else
                    If mAppStatus = "A" Or mRejStatus = "R" Then
                        If Trim(mAppCode) <> Trim(PubUserEMPCode) Then
                            MainClass.SetFocusToCell(SprdMain, I, ColAppStatus, "You are not a Approval Employee.")
                            FieldsVerification = False
                            Exit Function
                        End If
                    End If
                End If
            Next
        End With

        FieldsVerification = True
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number))
        FieldsVerification = False
        '    Resume
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1

        MakeSQL = " SELECT '', " & vbCrLf _
            & " TO_CHAR(IH.AUTO_KEY_REF), IH.REF_DATE, " & vbCrLf _
            & " IH.EMP_CODE, CMST.EMP_NAME, IH.FROM_DATE,  IH.TO_DATE, " & vbCrLf _
            & " IH.LDAYS, RMST.EMP_NAME, IH.APP_EMP_CODE, AMST.EMP_NAME," & vbCrLf _
            & " REASON," & vbCrLf _
            & " 0 AS APPROVED," & vbCrLf _
            & " 0 AS REJECTED"

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " FROM PAY_LEAVE_APP_TRN IH, " & vbCrLf _
            & " PAY_EMPLOYEE_MST CMST, PAY_EMPLOYEE_MST RMST, PAY_EMPLOYEE_MST AMST"
        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.EMP_CODE=CMST.EMP_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=RMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.REC_EMP_CODE=RMST.EMP_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=AMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.APP_EMP_CODE=AMST.EMP_CODE"


        MakeSQL = MakeSQL & vbCrLf & "AND IH.APP_STATUS ='O'"



        '    MakeSQL = MakeSQL & vbCrLf & "AND IH.APP_EMP_CODE='" & MainClass.AllowSingleQuote(PubUserEMPCode) & "'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND AMST.EMP_NAME='" & MainClass.AllowSingleQuote(txtAppEmpName.Text) & "'"
        End If

        ''ORDER CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.FROM_DATE,IH.TO_DATE,IH.EMP_CODE,IH.AUTO_KEY_REF"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function Update1() As Boolean

        On Error GoTo UpdateErr
        Dim SqlStr As String = ""
        Dim mRefNo As String
        Dim mAppStatus As String
        Dim mRejStatus As String
        Dim mStatus As String
        Dim I As Integer
        Dim mEmpName As String
        Dim mDesgName As String
        Dim mDeptName As String
        Dim mDateFrom As String
        Dim mDateTo As String
        Dim mDays As Double
        Dim mREASON As String
        Dim mApprovedName As String
        Dim mFromeMailId As String
        Dim mEmpCode As String
        Dim mApprovedCode As String
        Dim mStatusStr As String
        Dim RsEmpDesg As ADODB.Recordset

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColAppStatus
                mAppStatus = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "A", "O")

                .Col = ColRejStatus
                mRejStatus = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "R", "O")

                mStatus = IIf(mAppStatus = "A", mAppStatus, IIf(mRejStatus = "R", mRejStatus, "O"))

                .Col = ColRefNo
                mRefNo = CStr(Val(.Text))

                If mStatus <> "O" And Val(mRefNo) > 0 Then

                    SqlStr = "UPDATE PAY_LEAVE_APP_TRN SET " & vbCrLf & " APP_STATUS='" & mStatus & "', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_REF=" & Val(mRefNo) & ""

                    PubDBCn.Execute(SqlStr)
                End If

                .Col = ColEmpCode
                mEmpCode = Trim(.Text)

                .Col = ColEmpName
                mEmpName = Trim(.Text)

                .Col = ColFromDate
                mDateFrom = Trim(.Text)

                .Col = ColToDate
                mDateTo = Trim(.Text)

                .Col = ColLDays
                mDays = Val(.Text)

                .Col = ColReason
                mREASON = Trim(.Text)

                .Col = ColAppCode
                mApprovedCode = Trim(.Text)

                .Col = ColAppEmpName
                mApprovedName = Trim(.Text)

                If mStatus = "A" Then
                    mStatusStr = "Approved"
                ElseIf mStatus = "R" Then
                    mStatusStr = "Rejected"
                Else
                    mStatusStr = "Pending for Approval"
                End If

                SqlStr = " Select EMP_EMAILID_OFF, EMP_DEPT_CODE, GETEMPDESG(" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & Trim(mEmpCode) & "',TO_DATE('" & VB6.Format(mDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpDesg, ADODB.LockTypeEnum.adLockReadOnly)

                If RsEmpDesg.EOF = False Then
                    mDesgName = IIf(IsDbNull(RsEmpDesg.Fields("DESG_DESC").Value), "", RsEmpDesg.Fields("DESG_DESC").Value)

                    If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        If MainClass.ValidateWithMasterTable(MasterNo, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mDeptName = MasterNo
                        End If
                    End If
                End If

                SqlStr = " Select EMP_EMAILID_OFF, EMP_DEPT_CODE, GETEMPDESG(" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & Trim(mApprovedCode) & "',TO_DATE('" & VB6.Format(mDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mApprovedCode) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpDesg, ADODB.LockTypeEnum.adLockReadOnly)

                If RsEmpDesg.EOF = False Then
                    mFromeMailId = IIf(IsDbNull(RsEmpDesg.Fields("EMP_EMAILID_OFF").Value), "", RsEmpDesg.Fields("EMP_EMAILID_OFF").Value)
                End If
                If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then

                Else
                    If SendMail(mEmpName, mDesgName, mDeptName, mDateFrom, mDateTo, mDays, mREASON, mApprovedName, mFromeMailId, mRefNo, mStatusStr) = False Then GoTo UpdateErr
                End If
            Next
        End With

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
UpdateErr:
        Update1 = False
        PubDBCn.RollbackTrans()
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
        '    Resume
    End Function


    Private Function SendMail(ByRef mEmpName As String, ByRef mDesgName As String, ByRef mDeptName As String, ByRef mDateFrom As String, ByRef mDateTo As String, ByRef mDays As Double, ByRef mREASON As String, ByRef mApprovedName As String, ByRef mFromeMailId As String, ByRef mRefNo As String, ByRef mStatusStr As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim mTo As String
        Dim mCC As String
        Dim mFrom As String
        Dim mAttachmentFile As String
        Dim mDateTime As String
        Dim pAccountCode As String
        Dim mSubject As String
        Dim mBodyText As String

        SendMail = False


        mTo = GetEMailID("HRD_MAIL_TO") ''


        '    mTo = Trim(lblToeMailID.Caption)    '' ReadInI("InternetInfo", "TO", "InternetInfo.INI")
        '    mCC = ReadInI("InternetInfo", "CC", "InternetInfo.INI")
        mFrom = GetEMailID("MAIL_FROM") ''mFrom = Trim(mFromeMailId)  ''ReadInI("InternetInfo", "FROM", "InternetInfo.INI")
        mCC = Trim(mFromeMailId)

        mAttachmentFile = ""

        mSubject = ""

        mSubject = "Leave Requisition of " & Trim(mEmpName) & " From Dated : " & mDateFrom & " To Dated : " & mDateTo

        mBodyText = "<html><body><b><font size=11, color=Red>Leave Requisition</font></b><br />" & "<b>Employee Name : </b>" & Trim(mEmpName) & "<br />" & "<b>Designation : </b>" & Trim(mDesgName) & "(" & mDeptName & ") <br />" & "<b>From Dated : </b>" & Trim(mDateFrom) & "<br />" & "<b>To Dated : </b>" & Trim(mDateTo) & "<br />" & "<b>Total Working Days Applied: </b>" & Trim(CStr(mDays)) & "<br />" & "<b>Reason : </b>" & Trim(mREASON) & "<br />" & "<b>Approved By : </b>" & Trim(mApprovedName) & "<br />" & "<b>Ref No :" & mRefNo & "<br />" & "<b>Status :" & mStatusStr & "<br />" & "</body></html>"

        Call SendMailProcess(mFrom, mTo, mCC, "", mAttachmentFile, mSubject, mBodyText)

        SendMail = True

        Exit Function
ErrPart:
        SendMail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub frmParamLeaveRegHOD_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColAppStatus
                    .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Next
            End With

        End If
    End Sub
    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        'Dim xVDate As String
        'Dim xMkey As String=""
        'Dim xVNo As String
        'Dim xBookType As String=""
        'Dim xBookSubType As String
        '
        '
        '    SprdMain.Row = SprdMain.ActiveRow
        '
        '    SprdMain.Col = ColEmpName
        '    xVDate = Me.SprdMain.Text
        '
        '
        '
        '    SprdMain.Col = ColMKEY
        '    xMkey = Me.SprdMain.Text
        '
        '    SprdMain.Col = ColEmpCode
        '    xVNo = Me.SprdMain.Text
        '
        '    If lblBookType.Caption = "M" Then
        '        If Not IsDate(xVDate) Then Exit Sub
        '        If CDate(xVDate) >= CDate(RsCompany!Start_Date) And CDate(xVDate) <= CDate(RsCompany!END_DATE) Then
        '            Call ShowTrn(xMkey, xVDate, "", xVNo, "P", "")
        '        End If
        ''    Else
        ''        Call ShowTrn(xMkey, xVDate, "", xVNo, "J", "")
        '    End If
    End Sub

    Private Sub txtAppEmpName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppEmpName.DoubleClick
        SearchAppEmpName()
    End Sub
    Private Sub SearchAppEmpName()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        ''MainClass.SearchMaster txtAppEmpName, "PAY_EMPLOYEE_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(txtAppEmpName.Text, "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr)
        If AcName <> "" Then
            txtAppEmpName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtAppEmpName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAppEmpName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAppEmpName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAppEmpName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAppEmpName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAppEmpName()
    End Sub
    Private Sub txtAppEmpName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAppEmpName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtAppEmpName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtAppEmpName.Text), "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtAppEmpName.Text = UCase(Trim(txtAppEmpName.Text))
        Else
            MsgInformation("No Such Employee in Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
