Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmITForm16Print
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

    Private Sub frmITForm16Print_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmITForm16Print_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
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
    Private Sub frmITForm16Print_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        Me.Height = VB6.TwipsToPixelsY(3000)
        Me.Width = VB6.TwipsToPixelsX(6480)
        Me.Left = 0
        Me.Top = 0

        txtEmpCode.Enabled = True
        TxtName.Enabled = True
        cmdSearch.Enabled = True
        optParticular.Checked = True

        txtTo.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")
        'CellFormat
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub frmITForm16Print_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
        On Error GoTo ErrPart
        Dim mPrintType As String

        If Trim(txtTo.Text) = "" Then
            MsgInformation("Please Enter the Print Date.")
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        frmPrintForm16.ShowDialog()

        If frmPrintForm16.OptPrint(0).Checked = True Then
            mPrintType = "1"
        ElseIf frmPrintForm16.OptPrint(1).Checked = True Then
            mPrintType = "2"
        ElseIf frmPrintForm16.OptPrint(2).Checked = True Then
            mPrintType = "3"
        Else
            mPrintType = "4"
        End If

        If G_PrintLedg = False Then
            Exit Sub
        End If

        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow, mPrintType)

        frmPrintForm16.Close()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        frmPrintForm16.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        On Error GoTo ErrPart
        Dim mPrintType As String

        If Trim(txtTo.Text) = "" Then
            MsgInformation("Please Enter the Print Date.")
            Exit Sub
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        frmPrintForm16.ShowDialog()

        If frmPrintForm16.OptPrint(0).Checked = True Then
            mPrintType = "1"
        ElseIf frmPrintForm16.OptPrint(1).Checked = True Then
            mPrintType = "2"
        ElseIf frmPrintForm16.OptPrint(2).Checked = True Then
            mPrintType = "3"
        Else
            mPrintType = "4"
        End If

        If G_PrintLedg = False Then
            Exit Sub
        End If

        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter, mPrintType)

        frmPrintForm16.Close()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        frmPrintForm16.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants, ByRef mPrintType As String)


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mCode As Integer
        Dim mUpdate As Boolean
        Dim mRptFileName As String

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

        SqlStr = "INSERT INTO TEMP_PrintDummyData ( " & vbCrLf & " USERID, SUBROW, FIELD1, FIELD2, " & vbCrLf & " FIELD3, FIELD4, FIELD5, FIELD6, FIELD8, FIELD9," & vbCrLf & " FIELD10, FIELD25)" & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', TRN.SUBROW, " & vbCrLf & " '', TRN.DESCRIPTION, DECODE(TRN.AMOUNT1,0,'',TRN.AMOUNT1), DECODE(TRN.AMOUNT2,0,'',TRN.AMOUNT2), DECODE(TRN.AMOUNT3,0,'',TRN.AMOUNT3), " & vbCrLf & " DECODE(TRN.TOTALAMOUNT,0,'',TRN.TOTALAMOUNT), EMP.EMP_CODE || ' - ' || EMP.EMP_NAME,EMP.EMP_CODE," & vbCrLf & " 'M', '" & PubUserID & "' || EMP.EMP_CODE " & vbCrLf & " FROM PAY_ITFORM16_DET TRN, PAY_EMPLOYEE_MST EMP" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE"

        If optParticular.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE ='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)


        SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " FIELD1,FIELD2,FIELD4,FIELD8,FIELD9, " & vbCrLf & " FIELD10, FIELD25)" & vbCrLf & " SELECT " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',1.1, " & vbCrLf & " '', " & vbCrLf & " '*  Salary Recd from Previous Employer (as per Form 16 summitted by the employee ) : ', " & vbCrLf & " DECODE(TRN.PRESALARY,0,'',TRN.PRESALARY), EMP.EMP_CODE || ' - ' || EMP.EMP_NAME,EMP.EMP_CODE, " & vbCrLf & " 'M', '" & PubUserID & "' || EMP.EMP_CODE " & vbCrLf & " FROM PAY_ITFORM16_HDR TRN, PAY_EMPLOYEE_MST EMP" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE AND TRN.PRESALARY>0"

        If optParticular.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE ='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()


        'Select Record for print...

        mSubTitle = ""
        mTitle = "Form No. 16"
        If RsCompany.Fields("FYEAR").Value >= 2010 Then
            If FillITChallan("Q1") = False Then GoTo ERR1
            If FillITChallan("Q2") = False Then GoTo ERR1
            If FillITChallan("Q3") = False Then GoTo ERR1
            If FillITChallan("Q4") = False Then GoTo ERR1
            If FillITChallan("S") = False Then GoTo ERR1
            If UpdateOtherInfo = False Then GoTo ERR1
            If mPrintType = "1" Then
                mRptFileName = "ITForm16All_2010.Rpt"
            ElseIf mPrintType = "2" Then
                mRptFileName = "ITForm16All_PartA.Rpt"
            ElseIf mPrintType = "3" Then
                mRptFileName = "ITForm16All_PartB.Rpt"
            Else
                mRptFileName = "ITForm16All_AnnxB.Rpt"
            End If
        Else
            If FillITChallan("S") = False Then GoTo ERR1
            mRptFileName = "ITForm16All.Rpt"
        End If

        SqlStr = ""

        SqlStr = " SELECT * " & " FROM TEMP_PRINTDUMMYDATA PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY FIELD9,SUBROW"

        Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)


        '    If UpdateOtherInfo = False Then GoTo ERR1
        '
        '    If FillITChallan = False Then GoTo ERR1
        '
        '
        '    mUpdate = True
        '    'Select Record for print...
        '
        '    SqlStr = ""
        '
        '    SqlStr = " SELECT * " _
        ''            & " FROM TEMP_PRINTDUMMYDATA PrintDummyData " & vbCrLf _
        ''            & " WHERE  " & vbCrLf _
        ''            & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' AND FIELD10 ='M'" & vbCrLf _
        ''            & " ORDER BY FIELD8,SUBROW"

        '    mSubTitle = ""
        '    mTitle = "Form No. 16"
        ''    If mPrintType = "1" Then
        '        mRptFileName = "ITForm16All_2010.Rpt"
        ''    ElseIf mPrintType = "2" Then
        ''        mRptFileName = "ITForm16New_PartA.Rpt"
        ''    ElseIf mPrintType = "3" Then
        ''        mRptFileName = "ITForm16All_PartB.Rpt"
        ''    Else
        ''        mRptFileName = "ITForm16New_AnnxB.Rpt"
        ''    End If
        '
        '    Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        '    Resume
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

        Dim mCode As Integer
        Dim SqlStrSub As String
        Dim mRegdAddress As String
        Dim mAuthoName As String
        Dim mAuthoDesg As String
        Dim mAuthoFName As String
        Dim mAuthoSign As String
        Dim xSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAckNo1QTR As String
        Dim mAckNo2QTR As String
        Dim mAckNo3QTR As String
        Dim mAckNo4QTR As String

        Dim mAckNo1QTRDate As String
        Dim mAckNo2QTRDate As String
        Dim mAckNo3QTRDate As String
        Dim mAckNo4QTRDate As String
        Dim mQtrNo As Integer
        Dim mCITName As String
        Dim mCITAddress As String
        Dim mCITCity As String
        Dim mCITPincode As String
        Dim mCompanyPan As String
        Dim mCompanyTan As String
        Dim mCompanyTDS As String

        Report1.SQLQuery = mSqlStr

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)


        xSqlStr = " SELECT * FROM PAY_RTN_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            mAckNo1QTRDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("I_QTR_DATE").Value), "", RsTemp.Fields("I_QTR_DATE").Value), "DD/MM/YYYY")
            mAckNo2QTRDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("II_QTR_DATE").Value), "", RsTemp.Fields("II_QTR_DATE").Value), "DD/MM/YYYY")
            mAckNo3QTRDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("III_QTR_DATE").Value), "", RsTemp.Fields("III_QTR_DATE").Value), "DD/MM/YYYY")
            mAckNo4QTRDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("IV_QTR_DATE").Value), "", RsTemp.Fields("IV_QTR_DATE").Value), "DD/MM/YYYY")

            mAckNo1QTR = IIf(IsDbNull(RsTemp.Fields("I_QTR_NO").Value), "", RsTemp.Fields("I_QTR_NO").Value)
            mAckNo2QTR = IIf(IsDbNull(RsTemp.Fields("II_QTR_NO").Value), "", RsTemp.Fields("II_QTR_NO").Value)
            mAckNo3QTR = IIf(IsDbNull(RsTemp.Fields("III_QTR_NO").Value), "", RsTemp.Fields("III_QTR_NO").Value)
            mAckNo4QTR = IIf(IsDbNull(RsTemp.Fields("IV_QTR_NO").Value), "", RsTemp.Fields("IV_QTR_NO").Value)


            If RsCompany.Fields("FYEAR").Value < 2010 Then
                If mAckNo1QTRDate <> "" Then
                    If CDate(txtTo.Text) < CDate(mAckNo1QTRDate) Then
                        mAckNo1QTR = ""
                        mAckNo2QTR = ""
                        mAckNo3QTR = ""
                        mAckNo4QTR = ""
                    End If
                End If

                If mAckNo2QTRDate <> "" Then
                    If CDate(txtTo.Text) < CDate(mAckNo2QTRDate) Then
                        mAckNo2QTR = ""
                        mAckNo3QTR = ""
                        mAckNo4QTR = ""
                    End If
                End If

                If mAckNo3QTRDate <> "" Then
                    If CDate(txtTo.Text) < CDate(mAckNo3QTRDate) Then
                        mAckNo3QTR = ""
                        mAckNo4QTR = ""
                    End If
                End If

                If mAckNo4QTRDate <> "" Then
                    If CDate(txtTo.Text) < CDate(mAckNo4QTRDate) Then
                        mAckNo4QTR = ""
                    End If
                End If
            End If
        End If

        MainClass.AssignCRptFormulas(Report1, "AckNo1QTR='" & mAckNo1QTR & "'")
        MainClass.AssignCRptFormulas(Report1, "AckNo2QTR='" & mAckNo2QTR & "'")
        MainClass.AssignCRptFormulas(Report1, "AckNo3QTR='" & mAckNo3QTR & "'")
        MainClass.AssignCRptFormulas(Report1, "AckNo4QTR='" & mAckNo4QTR & "'")


        mCompanyPan = IIf(IsDbNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
        mCompanyTan = IIf(IsDbNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
        mCompanyTDS = IIf(IsDbNull(RsCompany.Fields("TDSCIRCLE").Value), "", RsCompany.Fields("TDSCIRCLE").Value)


        MainClass.AssignCRptFormulas(Report1, "Com_Pan='" & mCompanyPan & "'")
        MainClass.AssignCRptFormulas(Report1, "Com_Tan='" & mCompanyTan & "'")
        MainClass.AssignCRptFormulas(Report1, "TDSCircle='" & mCompanyTDS & "'")

        mRegdAddress = IIf(IsDbNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        mRegdAddress = mRegdAddress & " " & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
        mRegdAddress = mRegdAddress & " " & IIf(IsDbNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
        mRegdAddress = mRegdAddress & " - " & IIf(IsDbNull(RsCompany.Fields("REGD_PIN").Value), "", RsCompany.Fields("REGD_PIN").Value)

        MainClass.AssignCRptFormulas(Report1, "Companyaddress=""" & mRegdAddress & """")

        mAuthoName = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED").Value), "", RsCompany.Fields("TDSAUTHORIZED").Value)
        mAuthoFName = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_FNAME").Value), "", RsCompany.Fields("TDSAUTHORIZED_FNAME").Value)
        mAuthoDesg = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_DESIG").Value), "", RsCompany.Fields("TDSAUTHORIZED_DESIG").Value)
        mAuthoSign = mAuthoName & " (" & mAuthoDesg & ")"


        MainClass.AssignCRptFormulas(Report1, "mAuthoName='" & mAuthoName & "'")
        MainClass.AssignCRptFormulas(Report1, "mAuthoFName='" & mAuthoFName & "'")
        MainClass.AssignCRptFormulas(Report1, "mAuthoDesg='" & mAuthoDesg & "'")
        MainClass.AssignCRptFormulas(Report1, "mAuthoSign='" & mAuthoSign & "'")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False

        If RsCompany.Fields("FYEAR").Value >= 2010 Then
            xSqlStr = " SELECT CIRCLE_NAME,CIRCLE_ADDRESS,CIRCLE_CITY,CIRCLE_PINCODE " & vbCrLf & " FROM FIN_PRINT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
            MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

            If RsTemp.EOF = False Then
                mCITName = IIf(IsDbNull(RsTemp.Fields("CIRCLE_NAME").Value), "", RsTemp.Fields("CIRCLE_NAME").Value)
                mCITAddress = IIf(IsDbNull(RsTemp.Fields("CIRCLE_ADDRESS").Value), "", RsTemp.Fields("CIRCLE_ADDRESS").Value)
                mCITCity = IIf(IsDbNull(RsTemp.Fields("CIRCLE_CITY").Value), "", RsTemp.Fields("CIRCLE_CITY").Value)
                mCITPincode = IIf(IsDbNull(RsTemp.Fields("CIRCLE_PINCODE").Value), "", RsTemp.Fields("CIRCLE_PINCODE").Value)
            End If

            MainClass.AssignCRptFormulas(Report1, "mCITName='" & mCITName & "'")
            MainClass.AssignCRptFormulas(Report1, "mCITAddress='" & mCITAddress & "'")
            MainClass.AssignCRptFormulas(Report1, "mCITCity='" & mCITCity & "'")
            MainClass.AssignCRptFormulas(Report1, "mCITPincode='" & mCITPincode & "'")

            SqlStrSub = " SELECT * FROM TEMP_PRINTDUMMYDATA PRINTDUMMYDATA " & vbCrLf & " WHERE " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " AND FIELD10 IN ('Q1','Q2','Q3','Q4')" & vbCrLf & " ORDER BY FIELD9,FIELD7,FIELD10"

            Report1.SubreportToChange = ""

            Report1.SubreportToChange = Report1.GetNthSubreportName(0)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub

            MainClass.AssignCRptFormulas(Report1, "AckNo1QTR='" & mAckNo1QTR & "'")
            MainClass.AssignCRptFormulas(Report1, "AckNo2QTR='" & mAckNo2QTR & "'")
            MainClass.AssignCRptFormulas(Report1, "AckNo3QTR='" & mAckNo3QTR & "'")
            MainClass.AssignCRptFormulas(Report1, "AckNo4QTR='" & mAckNo4QTR & "'")

            Report1.SubreportToChange = ""

            SqlStrSub = " SELECT * FROM TEMP_PRINTDUMMYDATA PRINTDUMMYDATA " & vbCrLf & " WHERE " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " AND FIELD10 ='S'" & vbCrLf & " ORDER BY FIELD25,FIELD9,SUBROW"

            Report1.SubreportToChange = Report1.GetNthSubreportName(1)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub

            Report1.SubreportToChange = ""

        Else
            SqlStrSub = " SELECT * FROM TEMP_PRINTDUMMYDATA PRINTDUMMYDATA " & vbCrLf & " WHERE " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " AND FIELD10 ='S'" & vbCrLf & " ORDER BY FIELD25,FIELD9,SUBROW"

            Report1.SubreportToChange = Report1.GetNthSubreportName(0)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub

            Report1.SubreportToChange = ""
        End If



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

    Public Function FillITChallan(ByRef pChallanType As String) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mCode As String
        Dim RowNum As Integer

        Dim mTotPaidAmount As Double
        Dim mTotPaidAmountStr As String
        Dim mTDSAmount As Double
        Dim mSurchargeAmount As Double
        Dim mCESSAmount As Double
        Dim mTotalAmount As Double
        Dim mChequeNo As String
        Dim mBSRCode As String
        Dim mPAYMENTDATE As String
        Dim mChallanNo As String
        Dim mAuthoName As String
        Dim mAuthoFName As String
        Dim mAuthoDesg As String

        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String
        Dim mFromEmpLeaveDate As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIsInsert As Boolean
        Dim mBookType As String
        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String
        Dim mFromDate As String
        Dim mToDate As String
        Dim RsEmp As ADODB.Recordset = Nothing

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If pChallanType = "Q1" Then
            mFromDate = "01/04/" & VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY")
            mToDate = "30/06/" & VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY")
        ElseIf pChallanType = "Q2" Then
            mFromDate = "01/07/" & VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY")
            mToDate = "30/09/" & VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY")
        ElseIf pChallanType = "Q3" Then
            mFromDate = "01/10/" & VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY")
            mToDate = "31/12/" & VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY")
        ElseIf pChallanType = "Q4" Then
            mFromDate = "01/01/" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")
            mToDate = "31/03/" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")
        Else
            mFromDate = RsCompany.Fields("START_DATE").Value
            mToDate = RsCompany.Fields("END_DATE").Value
        End If

        mIsInsert = False
        '    If MainClass.ValidateWithMasterTable(txtEmpCode.Text, "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mCode = MasterNo
        '    End If

        mAuthoName = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED").Value), "", RsCompany.Fields("TDSAUTHORIZED").Value)
        mAuthoFName = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_FNAME").Value), "", RsCompany.Fields("TDSAUTHORIZED_FNAME").Value)
        mAuthoDesg = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_DESIG").Value), "", RsCompany.Fields("TDSAUTHORIZED_DESIG").Value)

        SqlStr = " SELECT  DISTINCT EMP.EMP_CODE , EMP.EMP_NAME " & vbCrLf & " FROM PAY_ITFORM16_HDR TRN, PAY_EMPLOYEE_MST EMP" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE"

        If optParticular.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE ='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsEmp.EOF = False Then
            Do While RsEmp.EOF = False
                mCode = IIf(IsDbNull(RsEmp.Fields("EMP_CODE").Value), "", RsEmp.Fields("EMP_CODE").Value)
                SqlStr = " SELECT  SUM(ID.AMOUNT) AS TOT_AMOUNT " & vbCrLf & " FROM PAY_ITChallan_HDR IH, PAY_ITChallan_DET ID WHERE " & vbCrLf & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO" & vbCrLf & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ID.EMP_CODE='" & mCode & "'"

                SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.VDATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                SqlStr = SqlStr & vbCrLf & "ORDER BY IH.VDate "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSPrintDummy, ADODB.LockTypeEnum.adLockReadOnly)
                If RSPrintDummy.EOF = False Then
                    mTotPaidAmount = IIf(IsDbNull(RSPrintDummy.Fields("TOT_AMOUNT").Value), 0, RSPrintDummy.Fields("TOT_AMOUNT").Value)
                End If

                'Transfer Emp Data ...........

                '    SqlStr = " SELECT * " & vbCrLf _
                ''            & " FROM PAY_EMP_TRF_MST" & vbCrLf _
                ''            & " WHERE " & vbCrLf _
                ''            & " TO_COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                ''            & " AND TO_EMP_CODE = '" & mCode & "'"

                mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
                mToEmpCode = mCode

SearchRow:
                SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

                If RsTemp.EOF = False Then
                    mFromEmpCompany = IIf(IsDbNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
                    mFromEmpCode = IIf(IsDbNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

                    If MainClass.ValidateWithMasterTable(mFromEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mFromEmpCompany & "") = True Then
                        mFromEmpLeaveDate = VB6.Format(Trim(MasterNo), "DD/MM/YYYY")
                    End If

                    SqlStr = " SELECT  SUM(ID.AMOUNT) AS TOT_AMOUNT " & vbCrLf & " FROM PAY_ITChallan_HDR IH, PAY_ITChallan_DET ID WHERE " & vbCrLf & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO" & vbCrLf & " AND IH.COMPANY_CODE = " & mFromEmpCompany & " AND " & vbCrLf & " IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " ID.EMP_CODE = '" & mFromEmpCode & "' "

                    SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.VDATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    SqlStr = SqlStr & vbCrLf & " ORDER BY IH.VDate "

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSPrintDummy, ADODB.LockTypeEnum.adLockReadOnly)
                    If RSPrintDummy.EOF = False Then
                        mTotPaidAmount = mTotPaidAmount + IIf(IsDBNull(RSPrintDummy.Fields("TOT_AMOUNT").Value), 0, RSPrintDummy.Fields("TOT_AMOUNT").Value)
                    End If

                    SqlStr = " SELECT  ID.TDS_AMOUNT, ID.SURCHARGE_AMT, ID.CESS_AMT, " & vbCrLf & " ID.AMOUNT, IH.CHQ_NO, IH.BSRCODE, IH.CHALLANDATE, " & vbCrLf & " IH.CHALLANNO, IH.BOOKTYPE " & vbCrLf & " FROM PAY_ITChallan_HDR IH, PAY_ITChallan_DET ID WHERE " & vbCrLf & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO" & vbCrLf & " AND IH.COMPANY_CODE = " & mFromEmpCompany & " AND " & vbCrLf & " IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " ID.EMP_CODE = '" & mFromEmpCode & "' "

                    SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.VDATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    SqlStr = SqlStr & vbCrLf & " ORDER BY IH.VDate "

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSPrintDummy, ADODB.LockTypeEnum.adLockReadOnly)

                    If RSPrintDummy.EOF = False Then

                        Do While Not RSPrintDummy.EOF
                            mTDSAmount = IIf(IsDBNull(RSPrintDummy.Fields("TDS_AMOUNT").Value), 0, RSPrintDummy.Fields("TDS_AMOUNT").Value)
                            mSurchargeAmount = IIf(IsDBNull(RSPrintDummy.Fields("SURCHARGE_AMT").Value), 0, RSPrintDummy.Fields("SURCHARGE_AMT").Value)
                            mCESSAmount = IIf(IsDBNull(RSPrintDummy.Fields("CESS_AMT").Value), 0, RSPrintDummy.Fields("CESS_AMT").Value)
                            mTotalAmount = IIf(IsDBNull(RSPrintDummy.Fields("Amount").Value), 0, RSPrintDummy.Fields("Amount").Value)

                            mChequeNo = IIf(IsDBNull(RSPrintDummy.Fields("CHQ_NO").Value), "", RSPrintDummy.Fields("CHQ_NO").Value)
                            mBSRCode = IIf(IsDBNull(RSPrintDummy.Fields("BSRCODE").Value), "", RSPrintDummy.Fields("BSRCODE").Value)
                            mPAYMENTDATE = IIf(IsDBNull(RSPrintDummy.Fields("CHALLANDATE").Value), "", RSPrintDummy.Fields("CHALLANDATE").Value)
                            mChallanNo = IIf(IsDBNull(RSPrintDummy.Fields("CHALLANNO").Value), "", RSPrintDummy.Fields("CHALLANNO").Value)
                            mBookType = IIf(IsDBNull(RSPrintDummy.Fields("BOOKTYPE").Value), "", RSPrintDummy.Fields("BOOKTYPE").Value)

                            If Val(CStr(mTotalAmount)) = 0 Then GoTo NextRec2
                            RowNum = RowNum + 100

                            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA ( " & vbCrLf & " USERID, SUBROW, " & vbCrLf & " FIELD1, FIELD2, FIELD3, FIELD4," & vbCrLf & " FIELD5, FIELD6, FIELD7, FIELD8," & vbCrLf & " FIELD10,FIELD31,FIELD32,FIELD33,FIELD34, FIELD35,FIELD9,FIELD25) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " '" & mTDSAmount & "', '" & mSurchargeAmount & "', '" & mCESSAmount & "', " & vbCrLf & " '" & mTotalAmount & "', '" & mChequeNo & "', '" & mBSRCode & "', " & vbCrLf & " '" & mPAYMENTDATE & "', '" & mChallanNo & "', '" & pChallanType & "', " & vbCrLf & " '" & mAuthoName & "','" & mAuthoFName & "','" & mAuthoDesg & "','" & mTotPaidAmountStr & "','" & mBookType & "','" & mCode & "','" & PubUserID & mCode & "') "
                            PubDBCn.Execute(SqlStr)
NextRec2:
                            RSPrintDummy.MoveNext()
                            mIsInsert = True
                        Loop

                    End If
                    mToEmpCompany = mFromEmpCompany
                    mToEmpCode = mFromEmpCode
                    GoTo SearchRow
                End If

                mTotPaidAmountStr = MainClass.RupeesConversion(CDbl(mTotPaidAmount))

                SqlStr = " SELECT  ID.TDS_AMOUNT, ID.SURCHARGE_AMT, ID.CESS_AMT, " & vbCrLf & " ID.AMOUNT, IH.CHQ_NO, IH.BSRCODE, IH.CHALLANDATE, " & vbCrLf & " IH.CHALLANNO,BOOKTYPE " & vbCrLf & " FROM PAY_ITChallan_HDR IH, PAY_ITChallan_DET ID WHERE " & vbCrLf & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO" & vbCrLf & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " ID.EMP_CODE = '" & mCode & "' "

                SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.VDATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                SqlStr = SqlStr & vbCrLf & " ORDER BY IH.VDate "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSPrintDummy, ADODB.LockTypeEnum.adLockReadOnly)

                If RSPrintDummy.EOF = False Then

                    Do While Not RSPrintDummy.EOF
                        mTDSAmount = IIf(IsDbNull(RSPrintDummy.Fields("TDS_AMOUNT").Value), 0, RSPrintDummy.Fields("TDS_AMOUNT").Value)
                        mSurchargeAmount = IIf(IsDbNull(RSPrintDummy.Fields("SURCHARGE_AMT").Value), 0, RSPrintDummy.Fields("SURCHARGE_AMT").Value)
                        mCESSAmount = IIf(IsDbNull(RSPrintDummy.Fields("CESS_AMT").Value), 0, RSPrintDummy.Fields("CESS_AMT").Value)
                        mTotalAmount = IIf(IsDbNull(RSPrintDummy.Fields("Amount").Value), 0, RSPrintDummy.Fields("Amount").Value)

                        mChequeNo = IIf(IsDbNull(RSPrintDummy.Fields("CHQ_NO").Value), "", RSPrintDummy.Fields("CHQ_NO").Value)
                        mBSRCode = IIf(IsDbNull(RSPrintDummy.Fields("BSRCODE").Value), "", RSPrintDummy.Fields("BSRCODE").Value)
                        mPAYMENTDATE = IIf(IsDbNull(RSPrintDummy.Fields("CHALLANDATE").Value), "", RSPrintDummy.Fields("CHALLANDATE").Value)
                        mChallanNo = IIf(IsDbNull(RSPrintDummy.Fields("CHALLANNO").Value), "", RSPrintDummy.Fields("CHALLANNO").Value)
                        mBookType = IIf(IsDbNull(RSPrintDummy.Fields("BOOKTYPE").Value), "", RSPrintDummy.Fields("BOOKTYPE").Value)

                        If Val(CStr(mTotalAmount)) = 0 Then GoTo NextRec
                        RowNum = RowNum + 100

                        SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA ( " & vbCrLf & " USERID, SUBROW, " & vbCrLf & " FIELD1, FIELD2, FIELD3, FIELD4," & vbCrLf & " FIELD5, FIELD6, FIELD7, FIELD8," & vbCrLf & " FIELD10,FIELD31,FIELD32,FIELD33,FIELD34, FIELD35,FIELD9,FIELD25) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " '" & mTDSAmount & "', '" & mSurchargeAmount & "', '" & mCESSAmount & "', " & vbCrLf & " '" & mTotalAmount & "', '" & mChequeNo & "', '" & mBSRCode & "', " & vbCrLf & " '" & mPAYMENTDATE & "', '" & mChallanNo & "', '" & pChallanType & "', " & vbCrLf & " '" & mAuthoName & "','" & mAuthoFName & "','" & mAuthoDesg & "','" & mTotPaidAmountStr & "','" & mBookType & "','" & mCode & "','" & PubUserID & mCode & "') "
                        PubDBCn.Execute(SqlStr)
NextRec:
                        RSPrintDummy.MoveNext()
                        mIsInsert = True
                    Loop

                End If

                If mIsInsert = False Then

                    SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA ( " & vbCrLf & " USERID, SUBROW, " & vbCrLf & " FIELD1, FIELD2, FIELD3, FIELD4," & vbCrLf & " FIELD5, FIELD6, FIELD7, FIELD8," & vbCrLf & " FIELD10,FIELD31,FIELD32,FIELD33,FIELD34, FIELD35,FIELD9,FIELD25) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',1, " & vbCrLf & " '0.00', '0.00', '0.00', " & vbCrLf & " '0.00', '', '', " & vbCrLf & " '', '', '" & pChallanType & "', " & vbCrLf & " '" & mAuthoName & "','" & mAuthoFName & "','" & mAuthoDesg & "','Zero','R','" & mCode & "','" & PubUserID & mCode & "') "
                    PubDBCn.Execute(SqlStr)
                End If

                RsEmp.MoveNext()
            Loop
        End If
        PubDBCn.CommitTrans()
        FillITChallan = True
        Exit Function
PrintDummyErr:
        FillITChallan = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateOtherInfo() As Boolean

        On Error GoTo ErrPart
        'Dim mCode As Long
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim xSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAckNo1QTR As String
        Dim mAckNo2QTR As String
        Dim mAckNo3QTR As String
        Dim mAckNo4QTR As String

        Dim mAckNo1QTRDate As String
        Dim mAckNo2QTRDate As String
        Dim mAckNo3QTRDate As String
        Dim mAckNo4QTRDate As String
        Dim mQtrNo As Integer


        Dim mToDate As String
        Dim mFromDate As String
        Dim mEmpCode As String
        Dim mEmpName As String
        Dim mEmpDesg As String
        Dim mEmpPan As String
        Dim mTDSCircle As String
        Dim mAYEAR As String
        Dim mSignDate As String
        Dim mNotFilled As String
        Dim mCheckQtrFilled As String

        mNotFilled = "Not Available as the last qtrly statement is yet to be furnished"
        mAckNo1QTRDate = ""
        mAckNo2QTRDate = ""
        mAckNo3QTRDate = ""
        mAckNo4QTRDate = ""

        mAckNo1QTR = ""
        mAckNo2QTR = ""
        mAckNo3QTR = ""
        mAckNo4QTR = ""

        xSqlStr = " SELECT * FROM PAY_RTN_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""


        SqlStr = " SELECT EMP.EMP_CODE , EMP.EMP_NAME, TODATE, FROMDATE, EMPPANNO, TDSCIRCLE, AYEAR " & vbCrLf & " FROM PAY_ITFORM16_HDR TRN, PAY_EMPLOYEE_MST EMP" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE"

        If optParticular.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE ='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsEmp.EOF = False Then
            Do While Not RsEmp.EOF
                '            MainClass.UOpenRecordSet xSqlStr, PubDBCn, adOpenStatic, RsTemp

                mToDate = IIf(IsDbNull(RsEmp.Fields("TODATE").Value), "", RsEmp.Fields("TODATE").Value)
                mFromDate = IIf(IsDbNull(RsEmp.Fields("FROMDATE").Value), "", RsEmp.Fields("FROMDATE").Value)
                mEmpCode = IIf(IsDbNull(RsEmp.Fields("EMP_CODE").Value), "", RsEmp.Fields("EMP_CODE").Value)
                mEmpName = IIf(IsDbNull(RsEmp.Fields("EMP_NAME").Value), "", RsEmp.Fields("EMP_NAME").Value)


                mEmpDesg = GetEmpCurrentDesg(mEmpCode, mToDate)
                mSignDate = VB6.Format(txtTo.Text, "DD/MM/YYYY")

                If MainClass.ValidateWithMasterTable(Trim(mEmpDesg), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mEmpDesg = MasterNo
                End If
                mEmpPan = IIf(IsDbNull(RsEmp.Fields("EMPPANNO").Value), "", RsEmp.Fields("EMPPANNO").Value)
                mTDSCircle = IIf(IsDbNull(RsEmp.Fields("TDSCIRCLE").Value), "", RsEmp.Fields("TDSCIRCLE").Value)
                mAYEAR = IIf(IsDbNull(RsEmp.Fields("AYEAR").Value), "", RsEmp.Fields("AYEAR").Value)

                SqlStr = " UPDATE TEMP_PrintDummyData set FIELD11='" & mToDate & "'," & vbCrLf & " FIELD12='" & mFromDate & "'," & vbCrLf & " FIELD13='" & Trim(mEmpCode) & "'," & vbCrLf & " FIELD14='" & Trim(mEmpName) & "'," & vbCrLf & " FIELD15='" & Trim(mEmpDesg) & "'," & vbCrLf & " FIELD16='" & Trim(mEmpPan) & "'," & vbCrLf & " FIELD17='" & Trim(mTDSCircle) & "'," & vbCrLf & " FIELD18='" & Trim(mAYEAR) & "'," & vbCrLf & " FIELD19='" & Trim(mAckNo1QTR) & "'," & vbCrLf & " FIELD20='" & Trim(mAckNo2QTR) & "'," & vbCrLf & " FIELD21='" & Trim(mAckNo3QTR) & "'," & vbCrLf & " FIELD22='" & Trim(mAckNo4QTR) & "'," & vbCrLf & " FIELD23='" & VB6.Format(mSignDate, "DD/MM/YYYY") & "'," & vbCrLf & " FIELD25='" & PubUserID & mEmpCode & "'" & vbCrLf & " WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " AND FIELD9='" & Trim(mEmpCode) & "'" '' AND FIELD10='M'
                PubDBCn.Execute(SqlStr)

                SqlStr = " UPDATE PAY_ITFORM12BA_HDR SET SIGN_DATE=TO_DATE('" & VB6.Format(mSignDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " ADDUSER='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'," & vbCrLf & " ADDDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND EMP_CODE='" & Trim(mEmpCode) & "'"
                PubDBCn.Execute(SqlStr)

                RsEmp.MoveNext()
            Loop
        End If
        UpdateOtherInfo = True
        Exit Function
ErrPart:
        UpdateOtherInfo = False
    End Function

    Private Sub txtTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtTo.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtTo.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
