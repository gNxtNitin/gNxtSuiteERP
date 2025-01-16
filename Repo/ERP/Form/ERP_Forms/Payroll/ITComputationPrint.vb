Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmITComputationPrint
    Inherits System.Windows.Forms.Form
    'Dim RsITEmp As ADODB.Recordset
    'Dim RsITTRN As ADODB.Recordset

    Dim XRIGHT As String
    Dim Shw As Boolean
    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        SqlStr = ""

        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtName.Text = AcName
            txtEmpCode.Text = AcName1
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub

    End Sub

    Private Sub frmITComputationPrint_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmITComputationPrint_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub

        '    SqlStr = " SELECT * FROM PAY_ITCOMP_HDR WHERE 1<>1"
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsITEmp
        '
        '    SqlStr = " SELECT * FROM PAY_ITCOMP_TRN WHERE 1<>1"
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsITTRN


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume
    End Sub
    Private Sub frmITComputationPrint_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        Me.Height = VB6.TwipsToPixelsY(2580)
        Me.Width = VB6.TwipsToPixelsX(6480)
        Me.Left = 0
        Me.Top = 0

        txtEmpCode.Enabled = True
        TxtName.Enabled = True
        cmdSearch.Enabled = True
        optParticular.Checked = True

        'CellFormat
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub frmITComputationPrint_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        '    'PvtDBCn.Cancel
        '    'PvtDBCn.Close
        '    'Set PvtDBCn = Nothing
    End Sub

    Private Sub opAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles opAll.CheckedChanged
        If eventSender.Checked Then
            txtEmpCode.Enabled = False
            TxtName.Enabled = False
            cmdSearch.Enabled = False
        End If
    End Sub

    Private Sub optParticular_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optParticular.CheckedChanged
        If eventSender.Checked Then
            txtEmpCode.Enabled = True
            TxtName.Enabled = True
            cmdSearch.Enabled = True
        End If
    End Sub

    Private Sub txtEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdsearch_Click(cmdsearch, New System.EventArgs())
        End If
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        Dim xAmount As Decimal

        FieldsVarification = True

        If optParticular.Checked = True Then
            If Trim(txtEmpCode.Text) = "" Then
                MsgInformation("Code is empty. Cannot Save")
                txtEmpCode.Focus()
                FieldsVarification = False
                Exit Function
            End If
            txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")

            If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Employee Code Does Not Exist In Master.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If

        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
        'Resume
    End Function
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mCode As Integer
        Dim mUpdate As Boolean

        'Insert Data from Grid to PrintDummyData Table...

        If FieldsVarification = False Then
            Exit Sub
        End If

        mUpdate = False
        '    If FillPrintDummyData(sprdIT, 1, sprdIT.MaxRows, 0, sprdIT.MaxCols, PubDBCn) = False Then GoTo ERR1

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = "INSERT INTO TEMP_PrintDummyData ( " & vbCrLf & " USERID, SUBROW, FIELD1, FIELD2, " & vbCrLf & " FIELD3, FIELD4, FIELD5, FIELD6, FIELD7, FIELD8 )" & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', TRN.SUBROWNO, " & vbCrLf & " '', TRN.DESCRIPTION, DECODE(TRN.AMOUNT1,0,'',TRN.AMOUNT1), DECODE(TRN.AMOUNT2,0,'',TRN.AMOUNT2), DECODE(TRN.AMOUNT3,0,'',TRN.AMOUNT3), " & vbCrLf & " DECODE(TRN.AMOUNT4,0,'',TRN.AMOUNT4), DECODE(TRN.TOTALAMOUNT,0,'',TRN.TOTALAMOUNT), EMP.EMP_CODE || ' - ' || EMP.EMP_NAME " & vbCrLf & " FROM PAY_ITCOMP_TRN TRN, PAY_EMPLOYEE_MST EMP" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE"

        If optParticular.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE ='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)

        SqlStr = " UPDATE TEMP_PrintDummyData set FIELD10='M' WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        mUpdate = True
        'Select Record for print...

        SqlStr = ""

        SqlStr = " SELECT * " & " FROM TEMP_PRINTDUMMYDATA PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY FIELD8,SUBROW"

        mSubTitle = ""
        mTitle = "Computation of Income Tax for the F.Y. " & Year(RsCompany.Fields("START_DATE").Value) & "-" & Year(RsCompany.Fields("END_DATE").Value) & " A.Y. " & Year(RsCompany.Fields("END_DATE").Value) & "-" & Year(RsCompany.Fields("END_DATE").Value) + 1

        Call ShowReport(SqlStr, "ITCompAll.rpt", Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If mUpdate = False Then
            PubDBCn.RollbackTrans()
        End If

        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Dim mCode As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        '    mCode = txtEmpCode.Text & " - " & Trim(TxtName.Text)
        '    MainClass.AssignCRptFormulas Report1, "Name='" & mCode & "'"
        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim mName As String
        Dim mEmpCode As String

        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        mEmpCode = txtEmpCode.Text

        If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtName.Text = MasterNo
        Else
            MsgBox("Employee Code Does Not Exist In Master.", MsgBoxStyle.Information)
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
