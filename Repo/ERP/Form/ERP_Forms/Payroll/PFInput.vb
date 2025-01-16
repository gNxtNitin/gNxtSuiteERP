Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPFInput
    Inherits System.Windows.Forms.Form
    Dim RsPayTRn As ADODB.Recordset ''Recordset

    Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim mPFRate As Double
    Dim mPFEPFRate As Double
    Dim mPFPensionRate As Double
    Dim mPFCeiling As Double
    Dim mEmplerPFCont As String
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 0
    Private Const ColDate As Short = 1
    Private Const ColSalType As Short = 2
    Private Const ColMonth As Short = 3
    Private Const ColAcctNo As Short = 4
    Private Const ColName As Short = 5
    Private Const ColFName As Short = 6
    Private Const ColContRate As Short = 7
    Private Const ColHigherRate As Short = 8
    Private Const ColTotWages As Short = 9
    Private Const ColEPF_12 As Short = 10
    Private Const ColEPF_3 As Short = 11
    Private Const ColEPF_8 As Short = 12
    Private Const ColVPFRATE As Short = 13
    Private Const ColVPFAMT As Short = 14
    Private Const ColDateLeave As Short = 15
    Private Const ColNCP As Short = 16

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        Dim mPFPrefixNo As String
        Dim mPFNoStr As String
        Dim mPFNo As String

        If MainClass.SearchGridMaster((txtEmpName.Text), "PAY_PF_MST", "EMP_NAME", "PFNO", "FNAME", "CONT_NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpName.Text = AcName
            mPFNoStr = AcName1
            mPFPrefixNo = Mid(mPFNoStr, 1, Len(mPFNoStr) - 4)
            mPFNo = Mid(mPFNoStr, Len(mPFNoStr) - 3)
            mPFNo = VB6.Format(mPFNo, "0000")
            txtPFPreFixNo.Text = mPFPrefixNo
            txtPFNo.Text = mPFNo

            txtPFNo_Validating(txtPFNo, New System.ComponentModel.CancelEventArgs(False))
            If txtPFNo.Enabled = True Then txtPFNo.Focus()
        End If

        Exit Sub
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        On Error GoTo ERR1
        Select Case eventArgs.Col
            Case 0
                If eventArgs.Row > 0 And SprdMain.Enabled = True Then
                    MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColDate)
                    MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                    CalcTots()
                End If
        End Select
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        Dim mPFNo As String


        mPFNo = Trim(txtPFPreFixNo.Text) & Trim(txtPFNo.Text)

        If Trim(mPFNo) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsPayTRn.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PAY_CONTSALARY_TRN", mPFNo, RsPayTRn, "PFAC_CODE") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PAY_CONTSALARY_TRN", "PFAC_CODE", mPFNo) = False Then GoTo DelErrPart

                SqlStr = " DELETE FROM PAY_CONTSALARY_TRN " & vbCrLf & " WHERE PFAC_CODE='" & mPFNo & "'" & vbCrLf & " AND EDATE BETWEEN TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

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


    Private Sub cmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call CalcTots()

        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtPFNo_Validating(txtPFNo, New System.ComponentModel.CancelEventArgs(False))
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

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim RIBBONSGroup As Boolean
        Dim xDate As String
        Dim xTotWages As Double
        Dim xEPF As Double
        Dim xEPF8 As Double
        Dim xEPF8New As Double
        Dim xEPF3 As Double
        Dim mSALType As String
        Dim xVPFRate As Double
        Dim mEmployerPF As Double
        Dim pEmpCode As String
        Dim mEmpContOn As String
        Dim mPayablePFCeiling As Double
        Dim mMonthDays As Integer
        Dim xNCP As Double
        Dim mWDays As Double
        Dim xDateLeave As String
        Dim xSalDate As String

        If eventArgs.NewRow = -1 Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow

        Select Case eventArgs.col
            Case ColSalType
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColSalType
                mSALType = SprdMain.Text

                SprdMain.Col = ColDate
                xDate = SprdMain.Text
                If Not IsDate(xDate) Then Exit Sub
                If VB6.Format(xDate, "YYYY/MM/DD") < VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY/MM/DD") Then
                    MsgInformation("Date Cann't be Less Than FY Start Date")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColDate)
                End If

                If VB6.Format(xDate, "YYYY/MM/DD") > VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY/MM/DD") Then
                    MsgInformation("Date Cann't be Greater Than FY END Date")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColDate)
                End If

                If CheckDuplicateDate() = True Then
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColMonth)
                End If

                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColMonth
                If VB6.Format(xDate, "MM") = "04" Then
                    SprdMain.Text = "March (Paid In April) " & IIf(VB.Left(mSALType, 1) = "S" Or VB.Left(mSALType, 1) = "F", "", mSALType)
                ElseIf VB6.Format(xDate, "MM") = "03" Then
                    SprdMain.Text = "February (Paid In March) " & IIf(VB.Left(mSALType, 1) = "S" Or VB.Left(mSALType, 1) = "F", "", mSALType)
                Else
                    '                xDate = DateAdd("m", -1, xDate)
                    SprdMain.Text = VB6.Format(xDate, "MMMM") & " " & IIf(VB.Left(mSALType, 1) = "S" Or VB.Left(mSALType, 1) = "F", "", mSALType)
                End If

                If Trim(xDate) <> "" Then
                    MainClass.AddBlankSprdRow(SprdMain, ColDate, ConRowHeight)
                    FormatSprdMain(-1)
                End If
            Case ColDate
                SprdMain.Row = SprdMain.ActiveRow


                SprdMain.Col = ColDate
                xDate = SprdMain.Text

                If Not IsDate(xDate) Then Exit Sub

                SprdMain.Col = ColSalType
                mSALType = SprdMain.Text

                If VB6.Format(xDate, "YYYY/MM/DD") < VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY/MM/DD") Then
                    MsgInformation("Date Cann't be Less Than FY Start Date")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColDate)
                End If

                If VB6.Format(xDate, "YYYY/MM/DD") > VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY/MM/DD") Then
                    MsgInformation("Date Cann't be Greater Than FY END Date")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColDate)
                End If

                If CheckDuplicateDate() = True Then
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColMonth)
                End If

                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColMonth

                'February (Paid In March)
                If VB6.Format(xDate, "MM") = "04" Then
                    SprdMain.Text = "March (Paid In April) " & IIf(VB.Left(mSALType, 1) = "S" Or VB.Left(mSALType, 1) = "F", "", mSALType)
                ElseIf VB6.Format(xDate, "MM") = "03" Then
                    SprdMain.Text = "February (Paid In March) " & IIf(VB.Left(mSALType, 1) = "S" Or VB.Left(mSALType, 1) = "F", "", mSALType)
                Else
                    '                xDate = DateAdd("m", -1, xDate)
                    SprdMain.Text = VB6.Format(xDate, "MMMM") & " " & IIf(VB.Left(mSALType, 1) = "S" Or VB.Left(mSALType, 1) = "F", "", mSALType)
                End If

                If Trim(xDate) <> "" Then
                    MainClass.AddBlankSprdRow(SprdMain, ColDate, ConRowHeight)
                    FormatSprdMain(-1)
                End If

            Case ColVPFRATE
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColTotWages
                xTotWages = Val(SprdMain.Text)

                SprdMain.Col = ColVPFRATE
                xVPFRate = Val(SprdMain.Text)

                SprdMain.Col = ColVPFAMT
                SprdMain.Text = CStr(System.Math.Round(xTotWages * xVPFRate * 0.01))

            Case ColTotWages
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColSalType
                mSALType = VB.Left(SprdMain.Text, 1)
                mSALType = IIf(mSALType = "S", "N", "Y")
                SprdMain.Col = ColDate
                xDate = SprdMain.Text

                If Not IsDate(xDate) Then Exit Sub

                SprdMain.Col = ColTotWages
                xTotWages = Val(SprdMain.Text)

                SprdMain.Col = ColNCP
                xNCP = Val(SprdMain.Text)

                SprdMain.Col = ColDateLeave
                xDateLeave = Trim(SprdMain.Text)

                mEmpContOn = GetEmployeePFContOn("", xDate, txtPFPreFixNo.Text & txtPFNo.Text)

                Call CheckPFRates(CDate(xDate))

                If mEmplerPFCont = "C" Then
                    xSalDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(xDate)))
                    mMonthDays = MainClass.LastDay(Month(CDate(xSalDate)), Year(CDate(xSalDate)))

                    If xDateLeave <> "" Then
                        If VB6.Format(CDate(xDateLeave), "YYYYMM") = VB6.Format(CDate(xSalDate), "YYYYMM") Then
                            mWDays = VB.Day(CDate(xDateLeave)) - xNCP
                        Else
                            mWDays = mMonthDays - xNCP
                        End If
                    Else
                        mWDays = mMonthDays - xNCP
                    End If

                    '                If Format(CDate(mDOJ), "YYYYMM") = Format(CDate(xSalDate), "YYYYMM") Then
                    '                    mWDays = mWDays - Day(mDOJ)
                    '                End If
                    mPayablePFCeiling = System.Math.Round(mPFCeiling * mWDays / mMonthDays, 0)
                Else
                    mPayablePFCeiling = mPFCeiling
                End If
                If mSALType = "Y" And RsCompany.Fields("COMPANY_CODE").Value = 5 Then '
                    xEPF = System.Math.Round(xTotWages * 0.12)
                    ''5-04-2010
                    '                xEPF8 = Round(xTotWages * 0.0833)
                    '                xEPF3 = xEPF - xEPF8        ''Round(xTotWages * 0.0367)
                    ''5-04-2010
                    If mEmplerPFCont = "C" Then
                        mEmployerPF = System.Math.Round(mPayablePFCeiling * 0.12)
                        xEPF8 = System.Math.Round(mPayablePFCeiling * 0.0833)
                        xEPF3 = mEmployerPF - xEPF8
                    End If
                Else
                    If mPFCeiling > xTotWages Then
                        xEPF = System.Math.Round(xTotWages * 0.12)
                        If mEmplerPFCont = "B" Then
                            xEPF8 = System.Math.Round(xTotWages * 0.0833)
                            '                   xEPF3 = Round(xTotWages * 0.0367)
                            xEPF3 = xEPF - xEPF8
                        Else
                            mEmployerPF = System.Math.Round(mPayablePFCeiling * 0.12)
                            xEPF8 = System.Math.Round(mPayablePFCeiling * 0.0833)
                            xEPF3 = mEmployerPF - xEPF8
                        End If
                    Else
                        If mEmpContOn = "B" Then
                            xEPF = System.Math.Round(xTotWages * 0.12)
                        Else
                            xEPF = System.Math.Round(mPFCeiling * 0.12)
                        End If
                        If mEmplerPFCont = "B" Then
                            mEmployerPF = System.Math.Round(xTotWages * 0.12)
                            xEPF8 = System.Math.Round(mPFCeiling * 0.0833)
                            xEPF3 = mEmployerPF - xEPF8 ''Round(xTotWages * 0.0367)
                        Else
                            mEmployerPF = System.Math.Round(mPayablePFCeiling * 0.12)
                            xEPF8 = System.Math.Round(mPayablePFCeiling * 0.0833)
                            xEPF3 = mEmployerPF - xEPF8
                        End If
                    End If
                End If

                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColEPF_12
                SprdMain.Text = CStr(Val(CStr(xEPF)))

                SprdMain.Col = ColEPF_8
                SprdMain.Text = CStr(Val(CStr(xEPF8)))

                SprdMain.Col = ColEPF_3
                SprdMain.Text = CStr(Val(CStr(xEPF3)))

                SprdMain.Col = ColVPFRATE
                xVPFRate = Val(SprdMain.Text)

                SprdMain.Col = ColVPFAMT
                SprdMain.Text = CStr(System.Math.Round(xTotWages * xVPFRate * 0.01))

            Case ColEPF_8

                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColSalType
                mSALType = VB.Left(SprdMain.Text, 1)
                mSALType = IIf(mSALType = "S", "N", "Y")

                SprdMain.Col = ColDate
                xDate = SprdMain.Text

                If Not IsDate(xDate) Then Exit Sub

                SprdMain.Col = ColTotWages
                xTotWages = Val(SprdMain.Text)

                SprdMain.Col = ColNCP
                xNCP = Val(SprdMain.Text)

                SprdMain.Col = ColDateLeave
                xDateLeave = Trim(SprdMain.Text)

                mEmpContOn = GetEmployeePFContOn("", xDate, txtPFPreFixNo.Text & txtPFNo.Text)
                Call CheckPFRates(CDate(xDate))


                If mEmplerPFCont = "C" Then
                    xSalDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(xDate)))
                    mMonthDays = MainClass.LastDay(Month(CDate(xSalDate)), Year(CDate(xSalDate)))

                    If xDateLeave <> "" Then
                        If VB6.Format(CDate(xDateLeave), "YYYYMM") = VB6.Format(CDate(xSalDate), "YYYYMM") Then
                            mWDays = VB.Day(CDate(xDateLeave)) - xNCP
                        Else
                            mWDays = mMonthDays - xNCP
                        End If
                    Else
                        mWDays = mMonthDays - xNCP
                    End If

                    '                If Format(CDate(mDOJ), "YYYYMM") = Format(CDate(xSalDate), "YYYYMM") Then
                    '                    mWDays = mWDays - Day(mDOJ)
                    '                End If
                    mPayablePFCeiling = System.Math.Round(mPFCeiling * mWDays / mMonthDays, 0)
                    mEmployerPF = System.Math.Round(mPayablePFCeiling * 0.12)
                Else
                    mPayablePFCeiling = mPFCeiling
                    mEmployerPF = System.Math.Round(xTotWages * 0.12)
                End If

                If mSALType = "Y" And RsCompany.Fields("COMPANY_CODE").Value = 5 Then
                    xEPF = System.Math.Round(xTotWages * 0.12)
                    ''5-04-2010
                    '                xEPF8 = Round(xTotWages * 0.0833)
                    '                xEPF3 = xEPF - xEPF8        ''Round(xTotWages * 0.0367)

                    ''5-04-2010
                    If mEmplerPFCont = "C" Then
                        mEmployerPF = System.Math.Round(mPayablePFCeiling * 0.12)
                        xEPF8 = System.Math.Round(mPayablePFCeiling * 0.0833)
                        xEPF3 = mEmployerPF - xEPF8
                    End If


                Else
                    If mPFCeiling > xTotWages Then
                        xEPF = System.Math.Round(xTotWages * 0.12)
                        '                    xEPF8 = Round(xTotWages * 0.0833)
                        ''                    xEPF3 = Round(xTotWages * 0.0367)
                        '                    xEPF3 = xEPF - xEPF8

                        If mEmplerPFCont = "B" Then
                            xEPF8 = System.Math.Round(xTotWages * 0.0833)
                            '                   xEPF3 = Round(xTotWages * 0.0367)
                            xEPF3 = xEPF - xEPF8
                        Else
                            mEmployerPF = System.Math.Round(mPayablePFCeiling * 0.12)
                            xEPF8 = System.Math.Round(mPayablePFCeiling * 0.0833)
                            xEPF3 = mEmployerPF - xEPF8
                        End If

                    Else
                        If mEmpContOn = "B" Then
                            xEPF = System.Math.Round(xTotWages * 0.12)
                        Else
                            xEPF = System.Math.Round(mPayablePFCeiling * 0.12)
                        End If
                        If mEmplerPFCont = "B" Then
                            mEmployerPF = System.Math.Round(xTotWages * 0.12)
                            xEPF8 = System.Math.Round(mPayablePFCeiling * 0.0833)
                            xEPF3 = mEmployerPF - xEPF8 ''Round(xTotWages * 0.0367)
                        Else
                            mEmployerPF = System.Math.Round(mPayablePFCeiling * 0.12)
                            xEPF8 = System.Math.Round(mPayablePFCeiling * 0.0833)
                            xEPF3 = mEmployerPF - xEPF8
                        End If
                    End If
                End If

                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColEPF_12
                SprdMain.Text = CStr(Val(CStr(xEPF)))
                xEPF = Val(SprdMain.Text)

                SprdMain.Col = ColEPF_8
                xEPF8New = Val(SprdMain.Text)

                If xEPF8New > xEPF8 Then
                    MsgInformation("8.33% Share Cannot be Greater than Rs. " & xEPF8)
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColEPF_8)
                    eventArgs.cancel = True
                    Exit Sub
                End If

                SprdMain.Col = ColEPF_3
                xEPF3 = mEmployerPF - xEPF8New
                SprdMain.Text = CStr(Val(CStr(xEPF3)))

                SprdMain.Col = ColVPFRATE
                xVPFRate = Val(SprdMain.Text)

                SprdMain.Col = ColVPFAMT
                SprdMain.Text = CStr(System.Math.Round(xTotWages * xVPFRate * 0.01))
        End Select
        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        ' Resume
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

            Next
        End With
    End Function
    Private Sub CheckPFRates(ByRef mDate As Date)

        Dim RsCeiling As ADODB.Recordset
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mSqlStr As String
        mSqlStr = ""
        mSqlStr = " SELECT MAX(WEF) FROM PAY_PFESICEILING_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CODE=" & ConPF & "" & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        SqlStr = " SELECT * FROM PAY_PFESICEILING_MST WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConPF & " AND WEF=(" & mSqlStr & ") "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeiling, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCeiling.EOF = False Then
            mPFCeiling = IIf(IsDbNull(RsCeiling.Fields("ceiling").Value), 0, RsCeiling.Fields("ceiling").Value)
            mPFRate = IIf(IsDbNull(RsCeiling.Fields("Rate").Value), 0, RsCeiling.Fields("Rate").Value)
            mPFEPFRate = IIf(IsDbNull(RsCeiling.Fields("EPF").Value), 0, RsCeiling.Fields("EPF").Value)
            mPFPensionRate = IIf(IsDbNull(RsCeiling.Fields("PFUND").Value), 0, RsCeiling.Fields("PFUND").Value)
            mEmplerPFCont = IIf(IsDbNull(RsCeiling.Fields("EMPER_CONT").Value), "B", RsCeiling.Fields("EMPER_CONT").Value)
        Else
            mPFCeiling = 6500
            mPFRate = 12
            mPFEPFRate = 3.67
            mPFPensionRate = 8.33
            mEmplerPFCont = "B"
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        Dim mPFPrefixNo As String
        Dim mPFNoStr As String
        Dim mPFNo As String

        With SprdView
            If eventArgs.Row = 0 Then Exit Sub

            .Row = eventArgs.Row

            .Col = 1

            mPFNoStr = .Text

            If Len(mPFNoStr) < 4 Then
                MsgBox("Invaild PF NO.")
                Exit Sub
            End If

            If RsCompany.Fields("COMPANY_CODE").Value = 1 Or RsCompany.Fields("COMPANY_CODE").Value = 11 Or RsCompany.Fields("COMPANY_CODE").Value = 15 Or RsCompany.Fields("COMPANY_CODE").Value = 25 Then
                mPFPrefixNo = Mid(mPFNoStr, 1, Len(mPFNoStr) - 4)
                mPFNo = Mid(mPFNoStr, Len(mPFNoStr) - 3)
                mPFNo = VB6.Format(mPFNo, "0000")
                txtPFPreFixNo.Text = mPFPrefixNo
                txtPFNo.Text = mPFNo
            Else
                txtPFPreFixNo.Text = ""
                txtPFNo.Text = mPFNoStr
            End If


            Call txtPFNo_Validating(txtPFNo, New System.ComponentModel.CancelEventArgs(True))

            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub


    Private Sub txtEmpName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtEmpName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpName.Text)
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

    Public Sub txtPFNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPFNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPFNo As String
        Dim SqlStr As String = ""
        Dim xMkey As String = ""

        If Trim(txtPFNo.Text) = "" Then GoTo EventExitSub

        txtPFNo.Text = VB6.Format(txtPFNo.Text, "0000")

        '' & vbCrLf             & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " "


        If MODIFYMode = True And RsPayTRn.EOF = False Then xMkey = RsPayTRn.Fields("PFAC_CODE").Value
        mPFNo = Trim(Trim(txtPFPreFixNo.Text) & Trim(txtPFNo.Text))

        SqlStr = " SELECT * FROM PAY_CONTSALARY_TRN "

        SqlStr = SqlStr & vbCrLf & " WHERE PFAC_CODE='" & MainClass.AllowSingleQuote(mPFNo) & "'" & vbCrLf & " AND EDATE BETWEEN TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " ORDER BY EDATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPayTRn, ADODB.LockTypeEnum.adLockReadOnly)

        If RsPayTRn.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such PF No, Use Generate Voucher Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PAY_CONTSALARY_TRN " & " WHERE PFAC_CODE='" & MainClass.AllowSingleQuote(xMkey) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPayTRn, ADODB.LockTypeEnum.adLockReadOnly)
            Else
                SqlStr = "SELECT * FROM PAY_CONTSALARY_TRN " & " WHERE PFAC_CODE='" & MainClass.AllowSingleQuote(mPFNo) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    txtContractorName.Text = IIf(IsDbNull(RsTemp.Fields("CONT_NAME").Value), "", RsTemp.Fields("CONT_NAME").Value)
                    txtEmpName.Text = IIf(IsDbNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)
                    txtEmpFName.Text = IIf(IsDbNull(RsTemp.Fields("EMP_FNAME").Value), "", RsTemp.Fields("EMP_FNAME").Value)
                Else
                    txtContractorName.Text = ""
                    txtEmpName.Text = ""
                    txtEmpFName.Text = ""
                End If
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim I As Short
        Dim SqlStr As String = ""
        Dim mVNo As String

        Dim mMonthName As String
        Dim mEDate As String
        Dim mPFNo As String
        Dim mTotWages As Double
        Dim mEPFAmount As Double
        Dim mEPF_367 As Double
        Dim mEPF_833 As Double
        Dim mSALType As String
        Dim mVPFAmt As Double
        Dim mVPFRate As Double
        Dim mDOB As String
        Dim mAge As String
        Dim mNCP As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mPFNo = Trim(Trim(txtPFPreFixNo.Text) & Trim(txtPFNo.Text))

        SqlStr = " DELETE FROM PAY_CONTSALARY_TRN WHERE " & vbCrLf & " PFAC_CODE='" & MainClass.AllowSingleQuote(mPFNo) & "' AND EDATE BETWEEN TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        If txtContractorName.Text = "EMPLOYEE" Then
            If MainClass.ValidateWithMasterTable(mPFNo, "EMP_PF_ACNO", "EMP_DOB", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, "", "") = True Then
                mDOB = MasterNo
            Else
                mDOB = ""
            End If
        Else
            If MainClass.ValidateWithMasterTable(mPFNo, "EMP_PF_ACNO", "EMP_DOB", "PAY_CONT_EMPLOYEE_MST", PubDBCn, MasterNo, "", "") = True Then
                mDOB = MasterNo
            Else
                mDOB = ""
            End If
        End If


        For I = 1 To SprdMain.MaxRows - 1



            SprdMain.Row = I

            SprdMain.Col = ColMonth
            mMonthName = Trim(SprdMain.Text)

            SprdMain.Col = ColDate
            mEDate = Trim(SprdMain.Text)

            If Trim(mDOB) = "" Then
                mAge = CStr(0)
            Else
                '            mAge = DateDiff("yyyy", mDOB, mEDate)
                mAge = CStr(DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(mDOB), DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(mEDate))) / 12)
            End If

            SprdMain.Col = ColTotWages
            mTotWages = Val(SprdMain.Text)

            SprdMain.Col = ColEPF_12
            mEPFAmount = Val(SprdMain.Text)

            SprdMain.Col = ColEPF_3
            mEPF_367 = Val(SprdMain.Text)

            SprdMain.Col = ColEPF_8
            mEPF_833 = Val(SprdMain.Text)

            SprdMain.Col = ColVPFAMT
            mVPFAmt = Val(SprdMain.Text)

            SprdMain.Col = ColVPFRATE
            mVPFRate = Val(SprdMain.Text)

            SprdMain.Col = ColNCP
            mNCP = Val(SprdMain.Text)

            SprdMain.Col = ColSalType
            mSALType = VB.Left(SprdMain.Text, 1)
            mSALType = IIf(mSALType = "S", "N", mSALType)
            mSALType = IIf(mSALType = "A", "Y", mSALType)

            If CDbl(mAge) > 58 Then
                mEPF_367 = mEPF_833 + mEPF_367
                mEPF_833 = 0
            End If

            If Trim(mEDate) <> "" Then
                SqlStr = "INSERT INTO PAY_CONTSALARY_TRN( " & vbCrLf & " COMPANY_CODE, CONT_NAME,LEAVEDATE," & vbCrLf & " MONTH_DESC, EDATE, " & vbCrLf & " PFAC_CODE," & vbCrLf & " EMP_NAME," & vbCrLf & " EMP_FNAME," & vbCrLf & " TOT_WAGES," & vbCrLf & " EPF_AMT," & vbCrLf & " EPF_367, " & vbCrLf & " EPF_833,ISARREAR, VPFAMT, VPFRATE,WDAYS) "

                SqlStr = SqlStr & vbCrLf & " VALUES(" & RsCompany.Fields("Company_Code").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtContractorName.Text) & "',TO_DATE('" & VB6.Format(txtDateLeave.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(mMonthName) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(mEDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(mPFNo) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtEmpName.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtEmpFName.Text) & "', " & vbCrLf & " " & Val(CStr(mTotWages)) & ", " & vbCrLf & " " & Val(CStr(mEPFAmount)) & ", " & vbCrLf & " " & Val(CStr(mEPF_367)) & ", " & vbCrLf & " " & Val(CStr(mEPF_833)) & ",'" & mSALType & "','" & mVPFAmt & "', '" & mVPFRate & "'," & mNCP & ")"

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

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsPayTRn.EOF = True Then Exit Function

        If MainClass.ValidDataInGrid(SprdMain, ColDate, "S", "Date Is Blank.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub frmPFInput_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = ""
        SqlStr = "Select * from PAY_CONTSALARY_TRN Where 1<>1"
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

        SqlStr = "SELECT DISTINCT PFAC_CODE, EMP_NAME, EMP_FNAME, CONT_NAME "


        '    & vbCrLf _
        ''            & " MONTH_DESC, EDATE, " & vbCrLf _
        ''            & " TO_CHAR(TOT_WAGES) AS TOT_WAGES,TO_CHAR(EPF_AMT) AS EPF_AMT, " & vbCrLf _
        ''            & " TO_CHAR(EPF_367) AS EPF_367,TO_CHAR(EPF_833) AS EPF_833 "

        SqlStr = SqlStr & vbCrLf & " FROM " & vbCrLf & " PAY_CONTSALARY_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND EDATE BETWEEN TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY PFAC_CODE,EMP_NAME "

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

            .set_ColWidth(1, 2000)
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
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColMonth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPayTRn.Fields("MONTH_DESC").DefinedSize ''
            .set_ColWidth(ColMonth, 16)

            .Col = ColSalType
            If FormActive = False Then
                .CellType = SS_CELL_TYPE_COMBOBOX
                .TypeComboBoxList = "SALARY" & Chr(9) & "ARREAR" & Chr(9) & "ENCASH" & Chr(9) & "OT" & Chr(9) & "EL ARREAR" & Chr(9) & "CPL" & Chr(9) & "F&F" & Chr(9) & "1. PREV. ARREAR" & Chr(9) & "2. PREV. ARREAR"
                .TypeComboBoxCurSel = 0
            End If
            .set_ColWidth(ColSalType, 12)

            '        .CellType = SS_CELL_TYPE_CHECKBOX
            '        .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            '        .ColWidth(ColSalType) = 7
            '        .Value = vbUnchecked

            .Col = ColAcctNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColAcctNo, 20)
            .ColHidden = True

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColName, 20)
            .ColHidden = True

            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColFName, 20)
            .ColHidden = True

            .Col = ColContRate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColContRate, 20)
            .ColHidden = True

            .Col = ColHigherRate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColHigherRate, 20)
            .ColHidden = True

            .Col = ColDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColDate, 10)

            .Col = ColTotWages
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColTotWages, 9)

            .Col = ColEPF_12
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColEPF_12, 8)

            .Col = ColEPF_3
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColEPF_3, 8)

            .Col = ColEPF_8
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColEPF_8, 8)

            .Col = ColVPFAMT
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColVPFAMT, 8)

            .Col = ColVPFRATE
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColVPFRATE, 5)

            .Col = ColDateLeave
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColDate, 10)
            .ColHidden = True

            .Col = ColNCP
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 1
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColNCP, 5)
            .ColHidden = False
        End With

        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDate, ColTotWages)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMonth, ColMonth)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColEPF_12, ColEPF_12)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColEPF_3, ColEPF_3)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColVPFAMT, ColVPFAMT)

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsPayTRn

            txtPFNo.Maxlength = .Fields("PFAC_CODE").DefinedSize ''
            txtContractorName.Text = CStr(.Fields("CONT_NAME").DefinedSize)
            txtEmpName.Text = CStr(.Fields("EMP_NAME").DefinedSize)
            txtEmpFName.Text = CStr(.Fields("EMP_FNAME").DefinedSize)
            txtDateLeave.Text = CStr(.Fields("LEAVEDATE").DefinedSize - 6)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPFPrefixNo As String
        Dim mPFNoStr As String
        Dim mPFNo As String
        Dim mCompanyPFNo As String
        Dim SqlStr As String = ""
        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String

        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String

        Clear1()
        mCompanyPFNo = IIf(IsDbNull(RsCompany.Fields("PFEST").Value), "", RsCompany.Fields("PFEST").Value)
        With RsPayTRn
            If Not .EOF Then


                mPFNoStr = IIf(IsDbNull(.Fields("PFAC_CODE").Value), "", .Fields("PFAC_CODE").Value)

                If Len(mPFNoStr) < 4 Then
                    MsgBox("Invaild PF NO.")
                    Exit Sub
                End If
                If RsCompany.Fields("COMPANY_CODE").Value = 1 Or RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 5 Or RsCompany.Fields("COMPANY_CODE").Value = 15 Or RsCompany.Fields("COMPANY_CODE").Value = 11 Or RsCompany.Fields("COMPANY_CODE").Value = 25 Then
                    If mCompanyPFNo <> "" Then
                        txtPFPreFixNo.Text = mCompanyPFNo
                        txtPFNo.Text = Mid(mPFNoStr, Len(mCompanyPFNo) + 1)
                    Else
                        mPFPrefixNo = Mid(mPFNoStr, 1, Len(mPFNoStr) - 4)
                        mPFNo = Mid(mPFNoStr, Len(mPFNoStr) - 3)
                        mPFNo = VB6.Format(mPFNo, "0000")
                        txtPFPreFixNo.Text = mCompanyPFNo
                        txtPFNo.Text = mPFNo
                    End If
                Else
                    txtPFPreFixNo.Text = ""
                    txtPFNo.Text = mPFNoStr
                End If

                txtContractorName.Text = IIf(IsDbNull(.Fields("CONT_NAME").Value), "", .Fields("CONT_NAME").Value)
                txtEmpName.Text = IIf(IsDbNull(.Fields("EMP_NAME").Value), "", .Fields("EMP_NAME").Value)
                txtEmpFName.Text = IIf(IsDbNull(.Fields("EMP_FNAME").Value), "", .Fields("EMP_FNAME").Value)

                '            txtDateLeave = IIf(IsNull(.Fields("LEAVEDATE").Value), "", .Fields("LEAVEDATE").Value)


                If MainClass.ValidateWithMasterTable(txtEmpName, "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mFromEmpCode = Trim(MasterNo)
                End If

                SqlStr = GetEmpTransferSQL(mFromEmpCode, RsCompany.Fields("COMPANY_CODE").Value, "Y")
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

                If RsTemp.EOF = False Then

                    mToEmpCompany = IIf(IsDbNull(RsTemp.Fields("TO_COMPANY_CODE").Value), "", RsTemp.Fields("TO_COMPANY_CODE").Value)
                    mToEmpCode = IIf(IsDbNull(RsTemp.Fields("TO_EMP_CODE").Value), "", RsTemp.Fields("TO_EMP_CODE").Value)

                    If MainClass.ValidateWithMasterTable(mPFNoStr, "EMP_PF_ACNO", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mToEmpCompany & "") = True Then
                        txtDateLeave.Text = VB6.Format(Trim(MasterNo), "DD/MM/YYYY")
                    End If
                Else
                    If MainClass.ValidateWithMasterTable(mPFNoStr, "EMP_PF_ACNO", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtDateLeave.Text = Trim(MasterNo)
                    Else
                        If MainClass.ValidateWithMasterTable(mPFNoStr, "EMP_PF_ACNO", "EMP_LEAVE_DATE", "PAY_CONT_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            txtDateLeave.Text = Trim(MasterNo)
                        Else
                            txtDateLeave.Text = IIf(IsDbNull(.Fields("LEAVEDATE").Value), "", .Fields("LEAVEDATE").Value)
                        End If
                    End If
                End If

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
        Dim mSALType As String
        Dim mRtnDate As String
        Dim mLeaveDate As String

        With mRsPayTrn
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColSalType
                mSALType = VB.Left(IIf(IsDbNull(.Fields("ISARREAR").Value), "N", .Fields("ISARREAR").Value), 1)

                If mSALType = "N" Then
                    SprdMain.Text = "SALARY"
                ElseIf mSALType = "Y" Then
                    SprdMain.Text = "ARREAR"
                ElseIf mSALType = "E" Then
                    SprdMain.Text = "ENCASH"
                ElseIf mSALType = "P" Then
                    SprdMain.Text = "EL ARREAR"
                ElseIf mSALType = "C" Then
                    SprdMain.Text = "CPL"
                ElseIf mSALType = "F" Then
                    SprdMain.Text = "F&F"
                ElseIf mSALType = "1" Then
                    SprdMain.Text = "1. PREV. ARREAR"
                ElseIf mSALType = "2" Then
                    SprdMain.Text = "2. PREV. ARREAR"
                Else
                    SprdMain.Text = "OT"
                End If

                SprdMain.Col = ColMonth
                SprdMain.Text = IIf(IsDbNull(.Fields("MONTH_DESC").Value), "", .Fields("MONTH_DESC").Value)

                SprdMain.Col = ColAcctNo
                SprdMain.Text = IIf(IsDbNull(.Fields("PFAC_CODE").Value), "", .Fields("PFAC_CODE").Value)

                SprdMain.Col = ColName
                SprdMain.Text = Trim(txtEmpName.Text)

                SprdMain.Col = ColFName
                SprdMain.Text = Trim(txtEmpFName.Text)

                SprdMain.Col = ColContRate
                SprdMain.Text = ""

                SprdMain.Col = ColHigherRate
                SprdMain.Text = ""

                SprdMain.Col = ColDate
                SprdMain.Text = IIf(IsDbNull(.Fields("EDATE").Value), "", .Fields("EDATE").Value)

                SprdMain.Col = ColEPF_8
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("EPF_833").Value), 0, .Fields("EPF_833").Value)))
                mEPF_8 = Val(IIf(IsDbNull(.Fields("EPF_833").Value), 0, .Fields("EPF_833").Value))

                SprdMain.Col = ColTotWages
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("TOT_WAGES").Value), 0, .Fields("TOT_WAGES").Value)))

                SprdMain.Col = ColEPF_3
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("EPF_367").Value), 0, .Fields("EPF_367").Value)))
                mEPF_3 = Val(IIf(IsDbNull(.Fields("EPF_367").Value), 0, .Fields("EPF_367").Value))

                SprdMain.Col = ColEPF_12
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("EPF_AMT").Value), 0, .Fields("EPF_AMT").Value)))

                SprdMain.Col = ColVPFAMT
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("VPFAMT").Value), 0, .Fields("VPFAMT").Value)))

                SprdMain.Col = ColVPFRATE
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("VPFRATE").Value), 0, .Fields("VPFRATE").Value)))

                If Trim(txtDateLeave.Text) = "" Then
                    mLeaveDate = ""
                Else
                    mLeaveDate = Trim(txtDateLeave.Text)
                    mRtnDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, RsCompany.Fields("END_DATE").Value))
                    If CDate(mLeaveDate) > CDate(mRtnDate) Then
                        mLeaveDate = ""
                    End If
                End If

                SprdMain.Col = ColDateLeave
                SprdMain.Text = mLeaveDate

                SprdMain.Col = ColNCP
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("WDAYS").Value), 0, .Fields("WDAYS").Value)))

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        Call CalcTots()
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Sub CalcTots()
        On Error GoTo ErrSprdTotal
        Dim cntRow As Integer
        Dim CntMonth As Integer
        Dim mCheckMonth As String
        Dim mMonth As String
        Dim mEPF_8_S As Double
        Dim mEPF_8_A As Double
        Dim mEPF_8_E As Double
        Dim mEPF_8_O As Double
        Dim mEPF_3_S As Double
        Dim mEPF_3_A As Double
        Dim mEPF_3_E As Double
        Dim mEPF_3_O As Double
        Dim mSALType As String
        Dim mEPF_8_APP As Double


        For CntMonth = 1 To 12
            mEPF_8_S = 0
            mEPF_8_A = 0
            mEPF_8_E = 0
            mEPF_8_O = 0
            mEPF_3_S = 0
            mEPF_3_A = 0
            mEPF_3_E = 0
            mEPF_3_O = 0
            Select Case CntMonth
                Case 1, 2, 3
                    mCheckMonth = VB6.Format(CntMonth, "00") & "/" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")
                Case 4, 5, 6, 7, 8, 9, 10, 11, 12
                    mCheckMonth = VB6.Format(CntMonth, "00") & "/" & VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY")
            End Select

            Call CheckPFRates(CDate(VB6.Format("01/" & mCheckMonth, "DD/MM/YYYY")))

            For cntRow = 1 To SprdMain.MaxRows
                SprdMain.Row = cntRow
                SprdMain.Col = ColDate
                mMonth = VB6.Format(SprdMain.Text, "MM/YYYY")
                If mCheckMonth = mMonth Then
                    SprdMain.Col = ColSalType
                    mSALType = VB.Left(SprdMain.Text, 1)

                    SprdMain.Col = ColEPF_8
                    If mSALType = "S" Then
                        mEPF_8_S = mEPF_8_S + Val(SprdMain.Text)
                    ElseIf mSALType = "A" Or mSALType = "1" Or mSALType = "2" Then
                        mEPF_8_A = mEPF_8_A + Val(SprdMain.Text)
                    ElseIf mSALType = "E" Then
                        mEPF_8_E = mEPF_8_E + Val(SprdMain.Text)
                    Else
                        mEPF_8_O = mEPF_8_O + Val(SprdMain.Text)
                    End If
                End If
            Next



            mEPF_8_APP = System.Math.Round(mPFCeiling * 8.33 * 0.01, 0) '' 541 ''mPFCeiling
            If mEPF_8_S + mEPF_8_A + mEPF_8_E + mEPF_8_O > mEPF_8_APP Then
                mEPF_8_APP = mEPF_8_APP - mEPF_8_S
                If mEPF_8_APP = 0 Then
                    mEPF_3_A = mEPF_8_A
                    mEPF_3_E = mEPF_8_E
                    mEPF_3_O = mEPF_8_O
                    mEPF_8_A = 0
                    mEPF_8_E = 0
                    mEPF_8_O = 0
                Else
                    If mEPF_8_APP > mEPF_8_A Then
                        mEPF_8_APP = mEPF_8_APP - mEPF_8_A
                        mEPF_3_A = 0
                        If mEPF_8_APP > mEPF_8_E Then
                            mEPF_8_APP = mEPF_8_APP - mEPF_8_E
                            mEPF_3_E = 0
                            If mEPF_8_APP > mEPF_8_O Then
                                mEPF_8_APP = mEPF_8_APP - mEPF_8_O
                                mEPF_3_O = 0
                            Else
                                mEPF_3_O = mEPF_8_O - mEPF_8_APP
                                mEPF_8_O = mEPF_8_APP
                                mEPF_8_APP = 0
                            End If
                        Else
                            mEPF_3_E = mEPF_8_E - mEPF_8_APP
                            mEPF_8_E = mEPF_8_APP
                            mEPF_3_O = mEPF_8_O
                            mEPF_8_O = 0
                            mEPF_8_APP = 0
                        End If
                    Else
                        mEPF_3_A = mEPF_8_A - mEPF_8_APP
                        mEPF_8_A = mEPF_8_APP
                        mEPF_3_E = mEPF_8_E
                        mEPF_8_E = 0
                        mEPF_3_O = mEPF_8_O
                        mEPF_8_O = 0
                        mEPF_8_APP = 0
                    End If
                End If

                For cntRow = 1 To SprdMain.MaxRows
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColDate
                    mMonth = VB6.Format(SprdMain.Text, "MM/YYYY")
                    If mCheckMonth = mMonth Then
                        SprdMain.Col = ColSalType
                        mSALType = VB.Left(SprdMain.Text, 1)

                        If mSALType = "A" Or mSALType = "1" Or mSALType = "2" Then
                            SprdMain.Col = ColEPF_3
                            SprdMain.Text = VB6.Format(Val(SprdMain.Text) + mEPF_3_A, "0.00")
                            SprdMain.Col = ColEPF_8
                            SprdMain.Text = VB6.Format(mEPF_8_A, "0.00")

                        ElseIf mSALType = "E" Then
                            SprdMain.Col = ColEPF_3
                            SprdMain.Text = VB6.Format(Val(SprdMain.Text) + mEPF_3_E, "0.00")
                            SprdMain.Col = ColEPF_8
                            SprdMain.Text = VB6.Format(mEPF_8_E, "0.00")
                        ElseIf mSALType = "O" Then
                            SprdMain.Col = ColEPF_3
                            SprdMain.Text = VB6.Format(Val(SprdMain.Text) + mEPF_3_O, "0.00")
                            SprdMain.Col = ColEPF_8
                            SprdMain.Text = VB6.Format(mEPF_8_O, "0.00")
                        End If
                    End If
                Next
            End If
        Next

        Exit Sub

ErrSprdTotal:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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


        If RsCompany.Fields("COMPANY_CODE").Value = 1 Then
            txtPFPreFixNo.Text = IIf(IsDbNull(RsCompany.Fields("PFEST").Value), "", RsCompany.Fields("PFEST").Value) ''"HR/5415/"
            txtPFPreFixNo.Enabled = True
            '    ElseIf RsCompany.Fields("COMPANY_CODE").Value = 6 Then
            '        txtPFPreFixNo.Text = "HR/26166/"
        Else
            txtPFPreFixNo.Text = IIf(IsDbNull(RsCompany.Fields("PFEST").Value), "", RsCompany.Fields("PFEST").Value)
            '        txtPFPreFixNo.Text = ""
            txtPFPreFixNo.Enabled = False
        End If

        txtPFNo.Text = ""
        txtContractorName.Text = ""
        txtEmpName.Text = ""
        txtEmpFName.Text = ""
        txtDateLeave.Text = ""
        txtFYear.Text = RsCompany.Fields("FYEAR").Value


        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsPayTRn, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmPFInput_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmPFInput_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmPFInput_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        '    If KeyCode = vbKeyF1 And mCol = ColMonth Then SprdMain_Click ColMonth, 0
        '    If KeyCode = vbKeyF1 And mCol = ColItemDesc Then SprdMain_Click ColItemDesc, 0
        '
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        PubDBCn.Errors.Clear()


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

        Dim mREASON As String
        Dim mPFNoStr As String
        Dim mLeaveDate As String
        Dim mRtnDate As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)


        If Trim(txtDateLeave.Text) = "" Then
            mLeaveDate = ""
        Else
            mLeaveDate = Trim(txtDateLeave.Text)
            mRtnDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, RsCompany.Fields("END_DATE").Value))
            If CDate(mLeaveDate) > CDate(mRtnDate) Then
                mLeaveDate = ""
            End If
        End If


        'COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "
        '
        mPFNoStr = Trim(txtPFPreFixNo.Text) & Trim(txtPFNo.Text)
        If Trim(mLeaveDate) = "" Then
            mREASON = ""
        Else
            If MainClass.ValidateWithMasterTable(mPFNoStr, "EMP_PF_ACNO", "EMP_LEAVE_REASON", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "") = True Then
                mREASON = Trim(MasterNo)
            Else
                If MainClass.ValidateWithMasterTable(mPFNoStr, "EMP_PF_ACNO", "EMP_LEAVE_REASON", "PAY_CONT_EMPLOYEE_MST", PubDBCn, MasterNo, , "") = True Then
                    mREASON = Trim(MasterNo)
                End If
            End If
        End If

        If mREASON = "" Then mREASON = "N.A."

        MainClass.AssignCRptFormulas(Report1, "LeaveReason='" & MainClass.AllowSingleQuote(mREASON) & "'")

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
End Class
