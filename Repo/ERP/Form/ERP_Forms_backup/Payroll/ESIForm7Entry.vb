Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmESIForm7Entry
    Inherits System.Windows.Forms.Form
    Dim RsPayTRn As ADODB.Recordset ''Recordset

    Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim mESIRate As Double
    Dim mESICeiling As Double

    Private Const ConRowHeight As Short = 12
    Private Const ColSNo As Short = 0
    Private Const ColSalType As Short = 1
    Private Const ColMonth As Short = 2
    Private Const ColDate As Short = 3
    Private Const ColWDays As Short = 4
    Private Const ColTotWages As Short = 5
    Private Const ColESIAmount As Short = 6

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        If MainClass.SearchGridMaster((TxtEmpName.Text), "PAY_CONTESI_TRN", "EMP_NAME", "ESIAC_CODE", "EMP_FNAME", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtEmpName.Text = AcName
            txtESINo.Text = AcName1
            txtESINo_Validating(txtESINo, New System.ComponentModel.CancelEventArgs(False))
            If txtESINo.Enabled = True Then txtESINo.Focus()
        End If

        Exit Sub
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColMonth)
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdMain.KeyPressEvent
        If (eventArgs.keyAscii = System.Windows.Forms.Keys.Tab Or eventArgs.keyAscii = System.Windows.Forms.Keys.Enter) And SprdMain.ActiveCol = ColTotWages Then
            SprdMain.Row = SprdMain.ActiveRow
            If Val(SprdMain.Text) <> 0 Then
                Call sprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(SprdMain.ActiveCol, SprdMain.ActiveRow, ColSalType, SprdMain.ActiveRow + 1, False))
            End If
        End If
    End Sub

    Private Sub txtContractorName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtContractorName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtContractorName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtContractorName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtContractorName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDateLeave_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateLeave.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDateLeave_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateLeave.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDateLeave.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDateLeave.Text) = True Then
            MsgBox("Not a valid date")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDesg_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDesg.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDesg_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDesg.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDesg.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDispensary_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDispensary.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDispensary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDispensary.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDispensary.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDOJ_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDOJ.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDOJ_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDOJ.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDOJ.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDOJ.Text) = True Then
            MsgBox("Not a valid date")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmpFName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpFName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpFName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpFName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpFName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpName.Click

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mESINo As String
        'Dim mEDATE As String
        'Dim mIsArrear As String
        'Dim xTotWages As Double
        'Dim mESIAmount As Double
        '    PubDBCn.Errors.Clear
        '    PubDBCn.BeginTrans
        '
        '    SqlStr = "SELECT * FROM PAY_CONTESI_TRN WHERE COMPANY_CODE=1"
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '    Do While Not RsTemp.EOF
        '            mESINo = RsTemp!ESIAC_CODE
        '            mEDATE = RsTemp!EDATE
        '            mIsArrear = RsTemp!ISARREAR
        '            xTotWages = RsTemp!TOT_WAGES
        '            mESIAmount = PaiseRound(Format(xTotWages * 0.0175, "0.00"), 0.05)
        '
        '            SqlStr = "Update PAY_CONTESI_TRN SET ESI_AMT=" & Val(mESIAmount) & "" & vbCrLf _
        ''                    & " WHERE COMPANY_CODE=1 " & vbCrLf _
        ''                    & " AND ESIAC_CODE='" & mESINo & "'" & vbCrLf _
        ''                    & " AND EDATE='" & VB6.Format(mEDATE, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''                    & " AND ISARREAR='" & mIsArrear & "'"
        '            PubDBCn.Execute SqlStr
        '
        '            RsTemp.MoveNext
        '    Loop
        '
        '    PubDBCn.CommitTrans
        '
        '    Exit Sub
        mESINo = Trim(txtESIPreFixNo.Text) & Trim(txtESINo.Text)

        If Trim(mESINo) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsPayTRn.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PAY_CONTESI_TRN", mESINo, RsPayTRn, "ESIAC_CODE") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PAY_CONTESI_TRN", "ESIAC_CODE", mESINo) = False Then GoTo DelErrPart

                SqlStr = " DELETE FROM PAY_CONTESI_TRN " & vbCrLf & " WHERE " & vbCrLf & " ESIAC_CODE='" & mESINo & "'" & vbCrLf & " AND EDATE BETWEEN TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                PubDBCn.Execute(SqlStr)
                PubDBCn.CommitTrans()
                RsPayTRn.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsPayTRn.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPayTRn, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True

        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


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
            txtESINo_Validating(txtESINo, New System.ComponentModel.CancelEventArgs(False))
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

    Private Sub sprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mWDays As Double
        Dim xDate As String
        Dim xTotWages As Double
        Dim xESI As Double
        Dim mMonthValue As Integer
        Dim mType As String
        If eventArgs.NewRow = -1 Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow

        Select Case eventArgs.col
            Case ColMonth
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColMonth
                mMonthValue = MonthValue((SprdMain.Text))

                If mMonthValue <= 3 Then
                    xDate = "01/" & VB6.Format(mMonthValue, "00") & "/" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")
                Else
                    xDate = "01/" & VB6.Format(mMonthValue, "00") & "/" & VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY")
                End If

                SprdMain.Col = ColDate
                SprdMain.Text = xDate

                If CheckDuplicateDate() = True Then
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColMonth)
                End If

                '            If Trim(xDate) <> "" Then
                '               MainClass.AddBlankSprdRow SprdMain, ColDate, ConRowHeight
                '               FormatSprdMain -1
                '            End If
            Case ColDate
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColDate
                xDate = SprdMain.Text
                If Not IsDate(xDate) Then Exit Sub

                If CheckDuplicateDate() = True Then
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColMonth)
                End If

                If VB6.Format(xDate, "YYYY/MM/DD") < VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY/MM/DD") Then
                    MsgInformation("Date Cann't be Less Than FY Start Date")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColDate)
                End If

                If VB6.Format(xDate, "YYYY/MM/DD") > VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY/MM/DD") Then
                    MsgInformation("Date Cann't be Greater Than FY END Date")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColDate)
                End If

                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColMonth
                SprdMain.Text = UCase(MonthName(CInt(VB6.Format(xDate, "MM"))))

                If Trim(xDate) <> "" Then
                    MainClass.AddBlankSprdRow(SprdMain, ColESIAmount, ConRowHeight)
                    FormatSprdMain(-1)
                End If

            Case ColWDays
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColSalType
                mType = VB.Left(SprdMain.Text, 1)

                SprdMain.Col = ColDate
                xDate = SprdMain.Text

                SprdMain.Col = ColWDays
                mWDays = Val(SprdMain.Text)

                If Val(CStr(mWDays)) <= 0 And mType = "S" Then
                    MsgInformation("Please Enter the Working Days.")
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColWDays)
                End If

            Case ColTotWages
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColSalType
                mType = VB.Left(SprdMain.Text, 1)

                SprdMain.Col = ColDate
                xDate = SprdMain.Text

                If Trim(SprdMain.Text) = "" Then Exit Sub

                SprdMain.Col = ColTotWages
                xTotWages = Val(SprdMain.Text)

                If Val(CStr(xTotWages)) <= 0 And mType = "S" Then
                    MsgInformation("Please Enter the Wages.")
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColTotWages)
                End If

                If IsDate(xDate) Then
                    Call CheckESIRates(CDate(xDate))
                End If

                If CDate(xDate) >= CDate("01/10/2004") Then
                    xESI = System.Math.Round(CDbl(VB6.Format(xTotWages * mESIRate * 0.01, "0.00")), 0)

                    If xESI < CDbl(VB6.Format(xTotWages * mESIRate * 0.01, "0.00")) Then
                        xESI = xESI + 1
                    End If
                Else
                    xESI = PaiseRound(CDbl(VB6.Format(xTotWages * mESIRate * 0.01, "0.00")), 0.05)
                    '                xESI = PaiseRound(Format(xTotWages * 0.0175, "0.00"), 0.05)
                End If




                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColESIAmount
                SprdMain.Text = CStr(Val(CStr(xESI)))

                If Val(CStr(xESI)) <> 0 Then
                    MainClass.AddBlankSprdRow(SprdMain, ColESIAmount, ConRowHeight)
                    FormatSprdMain(-1)
                End If

        End Select

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub

    Private Function CheckDuplicateDate() As Boolean

        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckMonth As String
        Dim mSALType As String
        Dim mMonth As String

        With SprdMain
            .Row = .ActiveRow
            .Col = ColSalType
            mSALType = VB.Left(.Text, 1)

            .Col = ColMonth
            If Trim(UCase(.Text)) = "" Then
                CheckDuplicateDate = False
                Exit Function
            End If
            mMonth = mSALType & Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColWDays
                If Val(.Text) <> 0 Then
                    .Col = ColSalType
                    mSALType = VB.Left(.Text, 1)

                    .Col = ColMonth
                    mCheckMonth = mSALType & Trim(UCase(.Text))

                    If (mCheckMonth = mMonth And Trim(UCase(.Text)) <> "") Then
                        mCount = mCount + 1
                    End If

                    If mCount > 1 Then
                        CheckDuplicateDate = True
                        MsgInformation("Duplicate Month : " & Trim(UCase(.Text)))
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColMonth)
                        Exit Function
                    End If
                End If
            Next
        End With
    End Function
    Private Sub CheckESIRates(ByRef mDate As Date)

        Dim RsCeiling As ADODB.Recordset
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mSqlStr As String
        mSqlStr = ""
        mSqlStr = " SELECT MAX(WEF) FROM PAY_PFESICEILING_MST" & vbCrLf & " WHERE " & vbCrLf & " CODE=" & ConESI & "" & vbCrLf & " AND " & vbCrLf & " WEF<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        SqlStr = " SELECT * FROM PAY_PFESICEILING_MST WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConESI & " AND WEF=(" & mSqlStr & ") "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeiling, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCeiling.EOF = False Then
            mESICeiling = IIf(IsDbNull(RsCeiling.Fields("ceiling").Value), 0, RsCeiling.Fields("ceiling").Value)
            mESIRate = IIf(IsDbNull(RsCeiling.Fields("Rate").Value), 0, RsCeiling.Fields("Rate").Value)
        Else
            mESICeiling = 6500
            mESIRate = 1.75
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        Dim mPFPrefixNo As String
        Dim mESINoStr As String
        Dim mESINo As String

        With SprdView
            If eventArgs.Row = 0 Then Exit Sub

            .Row = eventArgs.Row

            .Col = 1

            mESINoStr = .Text

            If Len(mESINoStr) < 4 Then
                MsgBox("Invaild PF NO.")
                Exit Sub
            End If

            mPFPrefixNo = Mid(mESINoStr, 1, Len(mESINoStr) - 4)
            mESINo = Mid(mESINoStr, Len(mESINoStr) - 3)
            mESINo = VB6.Format(mESINo, "0000")
            txtESIPreFixNo.Text = mPFPrefixNo
            txtESINo.Text = mESINo
            Call txtESINo_Validating(txtESINo, New System.ComponentModel.CancelEventArgs(True))

            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub


    Private Sub txtEmpName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub TxtEmpName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtEmpName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdsearch_Click(cmdsearch, New System.EventArgs())
        End If
    End Sub

    Public Sub txtESINo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtESINo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mESINo As String
        Dim SqlStr As String = ""
        Dim xMkey As String = ""

        If Trim(txtESINo.Text) = "" Then GoTo EventExitSub

        txtESINo.Text = VB6.Format(txtESINo.Text, "0000")

        If MODIFYMode = True And RsPayTRn.EOF = False Then xMkey = RsPayTRn.Fields("ESIAC_CODE").Value
        mESINo = Trim(Trim(txtESIPreFixNo.Text) & Trim(txtESINo.Text))



        SqlStr = " SELECT * FROM PAY_CONTESI_TRN " & vbCrLf & " WHERE " & vbCrLf & " ESIAC_CODE='" & MainClass.AllowSingleQuote(mESINo) & "'" & vbCrLf & " AND EDATE BETWEEN TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " ORDER BY SALTYPE DESC,EDATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPayTRn, ADODB.LockTypeEnum.adLockReadOnly)

        If RsPayTRn.EOF = False Then
            Clear1()
            Show1()
        Else
            If FillData(mESINo) = False Then
                Cancel = True
                GoTo EventExitSub
            End If

            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such PF No, Use Generate Voucher Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PAY_CONTESI_TRN " & " WHERE ESIAC_CODE='" & MainClass.AllowSingleQuote(xMkey) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPayTRn, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function FillData(ByRef pESINo As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT " & vbCrLf & " CONT_NAME, EMP_NAME, EMP_FNAME," & vbCrLf & " DISPENSARY, DESG_DESC," & vbCrLf & " DEPT_DESC, DOJ, LEAVEDATE " & vbCrLf & " FROM PAY_CONTESI_TRN " & vbCrLf & " WHERE " & vbCrLf & " ESIAC_CODE='" & MainClass.AllowSingleQuote(pESINo) & "'" & vbCrLf & " ORDER BY EDATE DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            With RsTemp
                txtContractorName.Text = IIf(IsDbNull(.Fields("CONT_NAME").Value), "", .Fields("CONT_NAME").Value)
                TxtEmpName.Text = IIf(IsDbNull(.Fields("EMP_NAME").Value), "", .Fields("EMP_NAME").Value)
                txtEmpFName.Text = IIf(IsDbNull(.Fields("EMP_FNAME").Value), "", .Fields("EMP_FNAME").Value)
                txtDispensary.Text = IIf(IsDbNull(.Fields("DISPENSARY").Value), "", .Fields("DISPENSARY").Value)
                txtDesg.Text = IIf(IsDbNull(.Fields("DESG_DESC").Value), "", .Fields("DESG_DESC").Value)
                txtDept.Text = IIf(IsDbNull(.Fields("DEPT_DESC").Value), "", .Fields("DEPT_DESC").Value)
                txtDateLeave.Text = IIf(IsDbNull(.Fields("LEAVEDATE").Value), "", .Fields("LEAVEDATE").Value)
                txtDOJ.Text = IIf(IsDbNull(.Fields("DOJ").Value), "", .Fields("DOJ").Value)
            End With
        End If

        FillData = True
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FillData = False
    End Function
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim I As Short
        Dim SqlStr As String = ""

        Dim mEDate As String
        Dim mESINo As String
        Dim mTotWages As Double
        Dim mESIAmount As Double
        Dim mIsArrear As String
        Dim mWDays As Double
        Dim mSALType As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mESINo = Trim(Trim(txtESIPreFixNo.Text) & Trim(txtESINo.Text))

        ''COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "

        SqlStr = " DELETE FROM PAY_CONTESI_TRN WHERE " & vbCrLf & " ESIAC_CODE='" & MainClass.AllowSingleQuote(mESINo) & "' AND EDATE BETWEEN TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        SqlStr = ""



        For I = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = I

            SprdMain.Col = ColDate
            mEDate = Trim(SprdMain.Text)

            SprdMain.Col = ColTotWages
            mTotWages = Val(SprdMain.Text)

            SprdMain.Col = ColESIAmount
            mESIAmount = Val(SprdMain.Text)

            SprdMain.Col = ColSalType
            mSALType = VB.Left(SprdMain.Text, 1)

            mIsArrear = IIf(mSALType = "A" Or mSALType = "I", "Y", "N")

            SprdMain.Col = ColWDays
            mWDays = Val(SprdMain.Text)


            If Trim(mEDate) <> "" Then
                SqlStr = "INSERT INTO PAY_CONTESI_TRN( " & vbCrLf & " COMPANY_CODE, CONT_NAME, " & vbCrLf & " EDATE, ESIAC_CODE, " & vbCrLf & " EMP_NAME, EMP_FNAME, " & vbCrLf & " DISPENSARY, DESG_DESC, " & vbCrLf & " DEPT_DESC, DOJ, " & vbCrLf & " WDAYS, TOT_WAGES, " & vbCrLf & " ESI_AMT, LEAVEDATE," & vbCrLf & " ISARREAR, SALTYPE,ESIAC_CODE_NUM) "

                SqlStr = SqlStr & vbCrLf & " VALUES( " & vbCrLf & " " & RsCompany.Fields("Company_Code").Value & ", '" & MainClass.AllowSingleQuote(txtContractorName.Text) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(mEDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mESINo) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtEmpName.Text) & "', '" & MainClass.AllowSingleQuote(txtEmpFName.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtDispensary.Text) & "', '" & MainClass.AllowSingleQuote(txtDesg.Text) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtDept.Text) & "', TO_DATE('" & VB6.Format(txtDOJ.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & Val(CStr(mWDays)) & ", " & Val(CStr(mTotWages)) & ", " & vbCrLf & " " & Val(CStr(mESIAmount)) & ", TO_DATE('" & VB6.Format(txtDateLeave.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mIsArrear & "','" & mSALType & "'," & Val(mESINo) & " )"

                PubDBCn.Execute(SqlStr)
            End If

        Next


        UpdateMain1 = True

        PubDBCn.CommitTrans()


        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsPayTRn.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mRow As Integer

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsPayTRn.EOF = True Then Exit Function

        If Trim(TxtEmpName.Text) = "" Then
            MsgInformation("Employee Name is Must")
            TxtEmpName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtContractorName.Text) = "" Then
            MsgInformation("Contractor Name is Must")
            txtContractorName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDOJ.Text) = "" Then
            MsgInformation("DOJ is Must")
            txtDOJ.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtDOJ.Text) Then
            MsgInformation("Invalid Joining Date.")
            txtDOJ.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColSalType, "S", "Salary Type Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColMonth, "S", "Month Is Blank.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub frmESIForm7Entry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = ""
        SqlStr = "Select * from PAY_CONTESI_TRN Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPayTRn, ADODB.LockTypeEnum.adLockReadOnly)

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

        MainClass.ClearGrid(SprdView)

        SqlStr = "SELECT DISTINCT ESIAC_CODE, EMP_NAME, EMP_FNAME, CONT_NAME "
        SqlStr = SqlStr & vbCrLf & " FROM " & vbCrLf & " PAY_CONTESI_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND EDATE BETWEEN TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY EMP_NAME, ESIAC_CODE "

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 600)

            .set_ColWidth(0, 600)

            .set_ColWidth(1, 1200)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
            .set_ColWidth(2, 2500)
            .set_ColWidth(3, 2500)
            .set_ColWidth(4, 2000)
            '        .ColWidth(5) = 2500
            '        .ColWidth(6) = 1200
            '        .ColWidth(7) = 1200
            '        .TypeHAlign = TypeHAlignRight
            '        .ColWidth(8) = 1200
            '        .TypeHAlign = TypeHAlignRight
            '        .ColWidth(9) = 1200
            '        .TypeHAlign = TypeHAlignRight
            '        .ColWidth(10) = 1200
            '        .TypeHAlign = TypeHAlignRight
            '

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            '        .Col = ColSalType
            '        .CellType = SS_CELL_TYPE_CHECKBOX
            '        .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            '        .ColWidth(ColSalType) = 7
            ''        .Value = vbUnchecked

            .Col = ColSalType
            If FormActive = False Then
                .CellType = SS_CELL_TYPE_COMBOBOX
                .TypeComboBoxList = "SALARY" & Chr(9) & "ARREAR(W)" & Chr(9) & "OT" & Chr(9) & "INCENTIVE(A)" & Chr(9) & "F & F" & Chr(9) & "Voucher (S)"
                .TypeComboBoxCurSel = 0
            End If
            .set_ColWidth(ColSalType, 12)

            .Col = ColMonth
            If FormActive = False Then
                .CellType = SS_CELL_TYPE_COMBOBOX
                .TypeComboBoxList = "" & Chr(9) & "APRIL" & Chr(9) & "MAY" & Chr(9) & "JUNE" & Chr(9) & "JULY" & Chr(9) & "AUGUST" & Chr(9) & "SEPTEMBER" & Chr(9) & "OCTOBER" & Chr(9) & "NOVEMBER" & Chr(9) & "DECEMBER" & Chr(9) & "JANUARY" & Chr(9) & "FEBRUARY" & Chr(9) & "MARCH"
                .TypeComboBoxCurSel = 0
            End If
            .set_ColWidth(ColMonth, 19)


            .Col = ColDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColDate, 10)

            For cntCol = ColWDays To ColESIAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMin = CDbl("-99999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 15)
            Next

        End With

        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDate, ColTotWages)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColESIAmount, ColESIAmount)

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsPayTRn

            txtESINo.Maxlength = .Fields("ESIAC_CODE").DefinedSize ''
            txtContractorName.Text = CStr(.Fields("CONT_NAME").DefinedSize)
            TxtEmpName.Text = CStr(.Fields("EMP_NAME").DefinedSize)
            txtEmpFName.Text = CStr(.Fields("EMP_FNAME").DefinedSize)
            txtDateLeave.Text = CStr(.Fields("LEAVEDATE").DefinedSize - 6)

            txtDispensary.Text = CStr(.Fields("DISPENSARY").DefinedSize)
            txtDesg.Text = CStr(.Fields("DESG_DESC").DefinedSize)
            txtDept.Text = CStr(.Fields("DEPT_DESC").DefinedSize)
            txtDOJ.Text = CStr(.Fields("DOJ").DefinedSize - 6)

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mESIPrefixNo As String
        Dim mESINo As String

        Clear1()

        With RsPayTRn
            If Not .EOF Then


                mESINo = IIf(IsDbNull(.Fields("ESIAC_CODE").Value), "", .Fields("ESIAC_CODE").Value)


                mESIPrefixNo = "" ''Mid(mESINoStr, 1, Len(mESINoStr) - 4)
                txtESIPreFixNo.Text = mESIPrefixNo
                txtESINo.Text = mESINo

                txtContractorName.Text = IIf(IsDbNull(.Fields("CONT_NAME").Value), "", .Fields("CONT_NAME").Value)
                TxtEmpName.Text = IIf(IsDbNull(.Fields("EMP_NAME").Value), "", .Fields("EMP_NAME").Value)
                txtEmpFName.Text = IIf(IsDbNull(.Fields("EMP_FNAME").Value), "", .Fields("EMP_FNAME").Value)

                txtDateLeave.Text = IIf(IsDbNull(.Fields("LEAVEDATE").Value), "", .Fields("LEAVEDATE").Value)
                txtDOJ.Text = IIf(IsDbNull(.Fields("DOJ").Value), "", .Fields("DOJ").Value)

                txtDesg.Text = IIf(IsDbNull(.Fields("DESG_DESC").Value), "", .Fields("DESG_DESC").Value)
                txtDept.Text = IIf(IsDbNull(.Fields("DEPT_DESC").Value), "", .Fields("DEPT_DESC").Value)
                txtDispensary.Text = IIf(IsDbNull(.Fields("DISPENSARY").Value), "", .Fields("DISPENSARY").Value)


                Call ShowDetail1(RsPayTRn)

                RsPayTRn.MoveFirst()
            End If
        End With

        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsPayTRn, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)



        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub ShowDetail1(ByRef mRsPayTrn As ADODB.Recordset)
        On Error GoTo ERR1
        Dim I As Integer
        Dim mEPF_8 As Double
        Dim mEPF_3 As Double
        Dim mDate As String
        Dim mSALType As String

        With mRsPayTrn
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColSalType
                mSALType = IIf(IsDbNull(.Fields("SALTYPE").Value), "", .Fields("SALTYPE").Value)

                If mSALType = "S" Then
                    SprdMain.Text = "SALARY"
                ElseIf mSALType = "A" Then
                    SprdMain.Text = "ARREAR(W)"
                ElseIf mSALType = "O" Then
                    SprdMain.Text = "OT"
                ElseIf mSALType = "F" Then
                    SprdMain.Text = "F & F"
                ElseIf mSALType = "V" Then
                    SprdMain.Text = "Voucher (S)"
                Else
                    SprdMain.Text = "INCENTIVE(A)"
                End If



                SprdMain.Col = ColDate
                SprdMain.Text = IIf(IsDbNull(.Fields("EDATE").Value), "", .Fields("EDATE").Value)
                mDate = IIf(IsDbNull(.Fields("EDATE").Value), "", .Fields("EDATE").Value)

                SprdMain.Col = ColMonth
                SprdMain.Text = UCase(MonthName(Month(CDate(mDate))))


                SprdMain.Col = ColWDays
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("WDAYS").Value), "", .Fields("WDAYS").Value), "0.00")

                SprdMain.Col = ColTotWages
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("TOT_WAGES").Value), 0, .Fields("TOT_WAGES").Value)))

                SprdMain.Col = ColESIAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ESI_AMT").Value), 0, .Fields("ESI_AMT").Value)))

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With

        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            AssignGrid(True)
            '        AdoDCMain.Refresh
            FormatSprdView()
            SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsPayTRn, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()


        txtESIPreFixNo.Text = ""
        txtESINo.Text = ""
        txtContractorName.Text = ""
        TxtEmpName.Text = ""
        txtEmpFName.Text = ""
        txtDateLeave.Text = ""
        txtFYear.Text = RsCompany.Fields("FYEAR").Value


        txtDesg.Text = ""
        txtDept.Text = ""
        txtDOJ.Text = ""
        txtDispensary.Text = ""

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsPayTRn, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmESIForm7Entry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmESIForm7Entry_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmESIForm7Entry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7755) '8000
        Me.Width = VB6.TwipsToPixelsX(11355) '11900

        'AdoDCMain.Visible = False

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol

        If (eventArgs.keyCode = System.Windows.Forms.Keys.Tab Or eventArgs.keyCode = System.Windows.Forms.Keys.Enter) And SprdMain.ActiveCol = ColTotWages Then
            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = ColTotWages
            If Val(SprdMain.Text) <> 0 Then
                Call sprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColTotWages, SprdMain.ActiveRow, ColSalType, SprdMain.ActiveRow + 1, True))
            End If
        End If
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            sprdMain_LeaveCell(sprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        PubDBCn.Errors.Clear()

        Exit Sub
        'Insert Data from Grid to PrintDummyData Table...


        If FillPrintDummyData(SprdMain, 1, SprdMain.MaxRows - 1, ColMonth, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1

        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = "From " & MonthName(Month(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, RsCompany.Fields("START_DATE").Value))) & ", " & Year(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, RsCompany.Fields("START_DATE").Value)) & " To : " & MonthName(Month(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, RsCompany.Fields("END_DATE").Value))) & ", " & Year(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, RsCompany.Fields("END_DATE").Value))
        mTitle = "Form 3A (Revised)"
        Call ShowReport(SqlStr, "PFFORM3A_Cont.Rpt", Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
End Class
