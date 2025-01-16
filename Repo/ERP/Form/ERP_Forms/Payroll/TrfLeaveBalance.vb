Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTrfLeaveBalance
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection

    Dim mLastFYNo As Integer
    Dim mCurrFYNo As Integer
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
        'Set PvtDBCn = Nothing
    End Sub
    Sub TopDisplayTransfer(ByRef MsgStr As String)
        TxtDisplayTransfer(0).SelectedText = MsgStr & vbCrLf
        TxtDisplayTransfer(0).SelectionLength = Len(MsgStr)
    End Sub
    Sub BottomDisplayTransfer(ByRef MsgStr As String)
        TxtDisplayTransfer(1).SelectedText = MsgStr & vbCrLf
        TxtDisplayTransfer(1).SelectionLength = Len(MsgStr)
    End Sub
    Sub MakeTxtDisplayTransferVisible()
        TxtDisplayTransfer(0).Width = VB6.TwipsToPixelsX(5085)
        TxtDisplayTransfer(1).Width = VB6.TwipsToPixelsX(5025)
        TxtDisplayTransfer(0).Height = VB6.TwipsToPixelsY(2835)
        TxtDisplayTransfer(1).Height = VB6.TwipsToPixelsY(1725)
        TxtDisplayTransfer(0).Top = VB6.TwipsToPixelsY(1710)
        TxtDisplayTransfer(1).Top = VB6.TwipsToPixelsY(2790)
        TxtDisplayTransfer(0).Left = 0
        TxtDisplayTransfer(1).Left = VB6.TwipsToPixelsX(30)
        TxtDisplayTransfer(0).Visible = True
        TxtDisplayTransfer(1).Visible = True
        TxtDisplayTransfer(0).Text = ""
        TxtDisplayTransfer(1).Text = ""
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo SearchErr
        Dim SqlStr As String = ""
        SqlStr = ""
        If MainClass.SearchGridMaster((TxtName.Text), "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtName.Text = AcName
            lblName.Text = AcName1
            TxtName.Focus()
        End If
        Exit Sub
SearchErr:
        MsgBox(Err.Description)
    End Sub
    Private Function FieldVarification() As Boolean

        On Error GoTo FieldErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(CboFYearFrom.Text) = "" Then
            MsgBox("Year From Not Selected....")
            Exit Function
        End If
        If Trim(CboFYearTo.Text) = "" Then
            MsgBox("Year To Not Selected....")
            Exit Function
        End If

        mLastFYNo = CInt(CboFYearFrom.Text)
        mCurrFYNo = CInt(CboFYearTo.Text)

        If mCurrFYNo = 2010 And RsCompany.Fields("COMPANY_CODE").Value = 1 Then
            MsgInformation("Carry Forward Already Done . So Cann't be Process Again.")
            Exit Function
        End If

        If mLastFYNo + 1 <> mCurrFYNo Then
            MsgBox("Invalid Year From & Year To ....")
            Exit Function
        End If

        If OptParticularAccount.Checked = True Then
            If MainClass.ValidateWithMasterTable((TxtName.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Employee Code Does Not Exist In Master.")
                Exit Function
            Else
                lblName.Text = AcName
            End If
        End If

        SqlStr = " SELECT * from PAY_LEAVEDTL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & mCurrFYNo & " AND LEAVECODE =" & SICK & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            SqlStr = "INSERT INTO PAY_LEAVEDTL_MST (" & vbCrLf & " COMPANY_CODE, PAYYEAR, LEAVECODE, TOTENTITLE, TOTENTITLE_WRKS ) " & vbCrLf & " SELECT COMPANY_CODE, " & mCurrFYNo & ", LEAVECODE, TOTENTITLE, TOTENTITLE_WRKS " & vbCrLf & " FROM PAY_LEAVEDTL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & mCurrFYNo - 1 & " AND LEAVECODE =" & SICK & ""

            PubDBCn.Execute(SqlStr)
        End If

        SqlStr = " SELECT * from PAY_LEAVEDTL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & mCurrFYNo & " AND LEAVECODE =" & CASUAL & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            SqlStr = "INSERT INTO PAY_LEAVEDTL_MST (" & vbCrLf & " COMPANY_CODE, PAYYEAR, LEAVECODE, TOTENTITLE, TOTENTITLE_WRKS ) " & vbCrLf & " SELECT COMPANY_CODE, " & mCurrFYNo & ", LEAVECODE, TOTENTITLE, TOTENTITLE_WRKS " & vbCrLf & " FROM PAY_LEAVEDTL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & mCurrFYNo - 1 & " AND LEAVECODE =" & CASUAL & ""

            PubDBCn.Execute(SqlStr)
        End If


        SqlStr = " SELECT Count(1) CNTREC From PAY_OPLeave_TRN WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND PAYYEAR=" & mCurrFYNo + 1 & ""

        If OptParticularAccount.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtName.Text & "' "
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If RsTemp.Fields("CNTREC").Value > 0 Then
                MsgInformation("You Cann't Process Back Opening Balance.")
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Function
            End If
        End If


        FieldVarification = True
        Exit Function
FieldErr:
        FieldVarification = False
        MsgBox(Err.Description)
    End Function

    Private Sub cmdStart_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdStart.Click

        On Error GoTo ERR1
        Dim mEmpCode As String

        Dim xEmpCode As String = ""
        Dim xEmpName As String = ""

        Dim SqlStr As String = ""
        Dim RsOPBal As ADODB.Recordset
        Dim mDOJ As String
        Dim mDOL As String

        If FieldVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mLastFYNo = CInt(CboFYearFrom.Text)
        mCurrFYNo = CInt(CboFYearTo.Text)

        mDOJ = "01/01/" & mLastFYNo
        mDOL = "31/12/" & mLastFYNo

        MakeTxtDisplayTransferVisible()
        TopDisplayTransfer(New String("=", 37))
        TopDisplayTransfer("Transferring Leave Balance From Year " & mLastFYNo & " To Year " & mCurrFYNo)
        TopDisplayTransfer("Please Wait........")
        TopDisplayTransfer(New String("=", 37))


        If OptParticularAccount.Checked = True Then
            If MainClass.ValidateWithMasterTable(txtName.Text, "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmpCode = MasterNo
            Else
                MsgInformation("Account Name Does Not Exist In Master.")
                Exit Sub
            End If
        End If

        SqlStr = "SELECT *  " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "


        If OptParticularAccount.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & mEmpCode & "' "
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY " & vbCrLf & " EMP_NAME "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOPBal, ADODB.LockTypeEnum.adLockReadOnly)

        If RsOPBal.EOF = False Then
            Do While Not RsOPBal.EOF
                xEmpCode = IIf(IsDBNull(RsOPBal.Fields("EMP_CODE").Value), "", RsOPBal.Fields("EMP_CODE").Value)
                xEmpName = IIf(IsDBNull(RsOPBal.Fields("EMP_NAME").Value), "", RsOPBal.Fields("EMP_NAME").Value)
                BottomDisplayTransfer(xEmpCode & " - " & xEmpName)
                If TransferBalance(xEmpCode) = False Then GoTo ERR1
                RsOPBal.MoveNext()
            Loop
        End If

        TxtDisplayTransfer(1).Text = ""
        TxtDisplayTransfer(0).Text = ""
        TopDisplayTransfer(New String("=", 37))
        TopDisplayTransfer("Transferring Leaves Balances From Year " & mLastFYNo & " To Year " & mCurrFYNo)
        TopDisplayTransfer("Leaves Balances Transfer Done Successfully.")
        TopDisplayTransfer(New String("=", 37))

        cmdStart.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        If Err.Number = 7 Then
            TxtDisplayTransfer(1).Text = ""
            Resume Next
        End If
        TxtDisplayTransfer(0).Text = ""
        TopDisplayTransfer(New String("=", 37))
        TopDisplayTransfer("Transferring Leaves Balances From Year " & mLastFYNo & " To Year " & mCurrFYNo)
        TopDisplayTransfer("Leaves Balances Transfer Failed.........")
        TopDisplayTransfer(New String("=", 37))
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Number <> 0 Then
            MsgInformation(Err.Description)
        End If
        '   Resume
    End Sub
    Private Function TransferBalance(ByRef xEmpCode As String) As Boolean
        On Error GoTo UpdateErr
        Dim xOpCode As Integer
        Dim xOpening As Double
        Dim xTOTENTITLE As Double
        Dim SqlStr As String = ""
        Dim mDOJ As String
        Dim xSickOPLeave As Double

        TransferBalance = False
        PubDBCn.BeginTrans()

        SqlStr = " DELETE From PAY_OPLeave_TRN WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
            & " AND PAYYEAR=" & mCurrFYNo & "" & vbCrLf & " AND EMP_CODE='" & xEmpCode & "'"
        PubDBCn.Execute(SqlStr)

        xOpCode = EARN

        '    If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
        '        xOpening = CalcBalEL(xEmpCode, SICK)
        '        xOpening = IIf(xOpening > 21, xOpening - 21, 0)
        '    Else
        '        xOpening = 0
        '    End If
        xOpening = xOpening + CalcBalEL(xEmpCode, xOpCode)



        xTOTENTITLE = 0

        SqlStr = " INSERT INTO PAY_OPLeave_TRN (COMPANY_CODE, PAYYEAR, " & vbCrLf _
            & " EMP_CODE, LEAVECODE, OPENING, " & vbCrLf _
            & " TOTENTITLE) VALUES " & vbCrLf _
            & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
            & " " & mCurrFYNo & ", " & vbCrLf _
            & " '" & xEmpCode & "', " & vbCrLf _
            & " " & xOpCode & "," & xOpening & "," & xTOTENTITLE & ") "
        PubDBCn.Execute(SqlStr)


        xOpCode = CASUAL
        xOpening = 0
        xTOTENTITLE = GetCurrYearEntitle(CASUAL, xEmpCode)

        SqlStr = " INSERT INTO PAY_OPLeave_TRN (COMPANY_CODE, PAYYEAR, " & vbCrLf _
            & " EMP_CODE, LEAVECODE, OPENING, " & vbCrLf _
            & " TOTENTITLE) VALUES " & vbCrLf _
            & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
            & " " & mCurrFYNo & ", " & vbCrLf & " '" & xEmpCode & "', " & vbCrLf _
            & " " & xOpCode & "," & xOpening & "," & xTOTENTITLE & ") "
        PubDBCn.Execute(SqlStr)

        xOpCode = SICK

        If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
            If mCurrFYNo = 2016 Then
                xOpCode = CASUAL
                xOpening = CalcBalEL(xEmpCode, xOpCode)
            Else
                xOpening = 0
            End If

            xOpCode = SICK

            xSickOPLeave = CalcBalEL(xEmpCode, xOpCode)
            xSickOPLeave = IIf(xSickOPLeave <= 21, xSickOPLeave, 21)

            xOpening = xOpening + xSickOPLeave
        Else
            xOpening = 0
        End If
        xTOTENTITLE = GetCurrYearEntitle(SICK, xEmpCode)

        SqlStr = " INSERT INTO PAY_OPLeave_TRN (COMPANY_CODE, PAYYEAR, " & vbCrLf & " EMP_CODE, LEAVECODE, OPENING, " & vbCrLf & " TOTENTITLE) VALUES " & vbCrLf & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & mCurrFYNo & ", " & vbCrLf & " '" & xEmpCode & "', " & vbCrLf & " " & xOpCode & "," & xOpening & "," & xTOTENTITLE & ") "
        PubDBCn.Execute(SqlStr)


        xOpCode = CPLEARN
        If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
            xOpening = CalcBalEL(xEmpCode, xOpCode)
        Else
            xOpening = 0
        End If
        xTOTENTITLE = 0

        SqlStr = " INSERT INTO PAY_OPLeave_TRN (COMPANY_CODE, PAYYEAR, " & vbCrLf & " EMP_CODE, LEAVECODE, OPENING, " & vbCrLf & " TOTENTITLE) VALUES " & vbCrLf & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & mCurrFYNo & ", " & vbCrLf & " '" & xEmpCode & "', " & vbCrLf & " " & xOpCode & "," & xOpening & "," & xTOTENTITLE & ") "
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        TransferBalance = True
        Exit Function
UpdateErr:
        If Err.Number = 7 Then
            TxtDisplayTransfer(1).Text = ""
            Resume Next
        End If
        BottomDisplayTransfer("Employee Code ..." & xEmpCode & " Transfer Failed...")
        PubDBCn.RollbackTrans()
        TransferBalance = False
        If Err.Number <> 0 Then
            MsgInformation(Err.Description)
        End If
        '    Resume
    End Function

    Private Function CalcBalEL(ByRef mCode As String, ByRef xLeaveCode As Integer) As Double
        On Error GoTo ErrPart
        Dim mDepositLeave As Double
        Dim mDate As String
        Dim mTotalLeavesBal As Double
        Dim pBalEL As Double
        Dim pBalCL As Double
        Dim pBalSL As Double
        Dim pBalCPL As Double
        Dim xRunDate As String
        Dim mIsStaff As Boolean



        CalcBalEL = 0
        pBalEL = 0
        pBalCL = 0
        pBalSL = 0
        pBalCPL = 0

        mIsStaff = True
        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_CAT_TYPE ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE =2") = True Then
            mIsStaff = False
        End If

        mDate = "31/12/" & mLastFYNo
        xRunDate = VB6.Format(mDate, "DD/MM/YYYY")

        mTotalLeavesBal = CalcBalLeaves(mCode, mDate, PubDBCn, pBalEL, pBalCL, pBalSL, pBalCPL)

        If RsCompany.Fields("COMPANY_CODE").Value = 16 And xLeaveCode = EARN Then
            pBalEL = pBalEL + IIf(pBalSL > 21, System.Math.Round(pBalSL - 21, 0), 0)
        End If

        If xLeaveCode = EARN Then
            If (RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 16) Then
                If mIsStaff = True Then
                    mDepositLeave = IIf(IsDbNull(RsCompany.Fields("DEPOSITLEAVE").Value), 0, RsCompany.Fields("DEPOSITLEAVE").Value)
                Else
                    mDepositLeave = IIf(IsDbNull(RsCompany.Fields("DEPOSITLEAVE_WK").Value), 0, RsCompany.Fields("DEPOSITLEAVE_WK").Value)
                End If
            Else
                mDepositLeave = IIf(IsDbNull(RsCompany.Fields("DEPOSITLEAVE").Value), 0, RsCompany.Fields("DEPOSITLEAVE").Value)
            End If
        ElseIf xLeaveCode = CPLEARN Then
            mDepositLeave = 0 ''mTotalLeavesBal - GetCPLPaid(mCode, mDate, PubDBCn)
        End If

        If xLeaveCode = EARN Then
            If (RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12) And Year(CDate(xRunDate)) < 2009 Then
                If pBalEL > mDepositLeave Then
                    CalcBalEL = mDepositLeave
                Else
                    CalcBalEL = pBalEL
                End If
            ElseIf (RsCompany.Fields("COMPANY_CODE").Value = 16) Then
                If pBalEL > mDepositLeave Then
                    CalcBalEL = mDepositLeave
                Else
                    CalcBalEL = pBalEL
                End If
            Else
                If mTotalLeavesBal > mDepositLeave Then
                    CalcBalEL = mDepositLeave
                Else
                    If RsCompany.Fields("COMPANY_CODE").Value = 15 Then
                        If Val(CboFYearTo.Text) <= 2013 Then
                            CalcBalEL = mTotalLeavesBal
                        Else
                            CalcBalEL = mTotalLeavesBal ''- pBalCPL ''13/01/2018
                        End If
                    Else
                        CalcBalEL = mTotalLeavesBal ''- pBalCPL''13/01/2018
                    End If
                End If
            End If
        ElseIf xLeaveCode = CPLEARN Then
            If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
                '            If mTotalLeavesBal > mDepositLeave Then
                '                CalcBalEL = 0
                '            Else
                CalcBalEL = pBalCPL
                '            End If
            Else
                '            If mTotalLeavesBal > mDepositLeave Then
                '                CalcBalEL = 0
                '            Else
                pBalCPL = 0
                CalcBalEL = pBalCPL
                '            End If
            End If
        End If
        If RsCompany.Fields("COMPANY_CODE").Value = 16 And xLeaveCode = SICK Then
            CalcBalEL = pBalSL ' IIf(pBalSL <= 21, pBalSL, 21)
        ElseIf RsCompany.Fields("COMPANY_CODE").Value = 16 And xLeaveCode = CASUAL And xRunDate = "31/12/2015" Then
            CalcBalEL = pBalCL
        End If
        Exit Function
ErrPart:
        CalcBalEL = 0
    End Function
    Private Function GetCurrYearEntitle(ByRef xLeaveCode As Integer, ByRef pEmpCode As String) As Double

        On Error GoTo UpdateErr
        Dim RsEntitle As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mCategory As String

        GetCurrYearEntitle = 0

        SqlStr = " SELECT * from PAY_LEAVEDTL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & mCurrFYNo & " AND LEAVECODE=" & xLeaveCode & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEntitle, ADODB.LockTypeEnum.adLockReadOnly)

        If RsEntitle.EOF = False Then

            If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "EMP_CAT_TYPE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCategory = MasterNo
            Else
                mCategory = "-1"
            End If

            If mCategory = "2" Then
                If IsDbNull(RsEntitle.Fields("TOTENTITLE_WRKS").Value) Or RsEntitle.Fields("TOTENTITLE_WRKS").Value = 0 Then
                    GetCurrYearEntitle = IIf(IsDbNull(RsEntitle.Fields("TOTENTITLE").Value), 0, RsEntitle.Fields("TOTENTITLE").Value)
                Else
                    GetCurrYearEntitle = IIf(IsDbNull(RsEntitle.Fields("TOTENTITLE_WRKS").Value), 0, RsEntitle.Fields("TOTENTITLE_WRKS").Value)
                End If
            Else
                GetCurrYearEntitle = IIf(IsDbNull(RsEntitle.Fields("TOTENTITLE").Value), 0, RsEntitle.Fields("TOTENTITLE").Value)
            End If
        End If
        Exit Function
UpdateErr:
        GetCurrYearEntitle = 0
    End Function
    Private Sub frmTrfLeaveBalance_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ERR1
        'Set PvtDBCn = New ADODB.Connection
        '    PvtDBCn.CommandTimeout = 0
        '    PvtDBCn.ConnectionTimeout = 0
        'PvtDBCn.Open StrConn

        'TxtDisplayTransfer(0).Visible = False
        'TxtDisplayTransfer(1).Visible = False
        OptAllAccount.Checked = True
        Me.Height = VB6.TwipsToPixelsY(5595)
        Me.Width = VB6.TwipsToPixelsX(5220)
        Me.Left = VB6.TwipsToPixelsX(20)
        Me.Top = VB6.TwipsToPixelsY(20)
        Call FillFYear()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub FillFYear()

        Dim SqlStr As String = ""
        Dim mRsFYear As ADODB.Recordset
        CboFYearFrom.Items.Clear()
        CboFYearTo.Items.Clear()
        SqlStr = "SELECT FYEAR FROM GEN_CMPYRDTL_TRN  " & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY FYEAR"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsFYear, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsFYear.EOF = False Then
            Do While Not mRsFYear.EOF
                CboFYearFrom.Items.Add(mRsFYear.Fields("FYEAR").Value)
                CboFYearTo.Items.Add(mRsFYear.Fields("FYEAR").Value)
                mRsFYear.MoveNext()
            Loop
        End If

        '
        '    CboFYearFrom.AddItem RsCompany.Fields("FYEAR").Value + 1
        '    CboFYearTo.AddItem RsCompany.Fields("FYEAR").Value + 1

    End Sub

    Private Sub OptAllAccount_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptAllAccount.CheckedChanged
        If eventSender.Checked Then
            TxtName.Enabled = False
            cmdSearch.Enabled = False
            cmdStart.Enabled = True
        End If
    End Sub
    Private Sub OptParticularAccount_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptParticularAccount.CheckedChanged
        If eventSender.Checked Then
            TxtName.Enabled = True
            cmdSearch.Enabled = True
            cmdStart.Enabled = True
        End If
    End Sub
    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.DoubleClick
        Call cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo SearchErr
        Dim RsItem As ADODB.Recordset
        Dim SqlStr As String = ""
        If Trim(TxtName.Text) = "" Then GoTo EventExitSub
        TxtName.Text = VB6.Format(TxtName.Text, "000000")

        SqlStr = "SELECT EMP_CODE,EMP_NAME FROM PAY_EMPLOYEE_MST where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & MainClass.AllowSingleQuote(UCase(TxtName.Text)) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItem, ADODB.LockTypeEnum.adLockReadOnly)

        If RsItem.EOF = True Then
            MsgBox("Employee Name Not Exist In Master", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        Else
            lblName.Text = IIf(IsDbNull(RsItem.Fields("EMP_NAME").Value), "", RsItem.Fields("EMP_NAME").Value)
        End If

        cmdStart.Enabled = True

        GoTo EventExitSub
SearchErr:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged

    End Sub

    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        e.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
End Class
