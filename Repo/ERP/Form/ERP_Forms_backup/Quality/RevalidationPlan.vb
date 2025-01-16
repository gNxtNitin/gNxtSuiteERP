Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmRevalidationPlan
    Inherits System.Windows.Forms.Form
    Dim RsRevalidation As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim PvtDBCn As ADODB.Connection
    Dim FormActive As Boolean
    Dim SqlStr As String
    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh
            FormatSprdView()
            SprdView.Refresh()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsRevalidation, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()

        lblMkey.Text = ""
        txtNumber.Text = ""
        txtYear.Text = RsCompany.Fields("FYEAR").Value
        txtProcess.Text = ""
        lblProcess.Text = ""
        txtMachine.Text = ""
        lblMachine.Text = ""
        txtJanPlan.Text = ""
        txtFebPlan.Text = ""
        txtMarPlan.Text = ""
        txtAprPlan.Text = ""
        txtMayPlan.Text = ""
        txtJunPlan.Text = ""
        txtJulPlan.Text = ""
        txtAugPlan.Text = ""
        txtSepPlan.Text = ""
        txtOctPlan.Text = ""
        txtNovPlan.Text = ""
        txtDecPlan.Text = ""
        Call MakeEnableDeField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsRevalidation, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub MakeEnableDeField(ByRef mMode As Boolean)
        txtYear.Enabled = False
        txtMachine.Enabled = mMode
        cmdSearchMachine.Enabled = mMode
        txtProcess.Enabled = mMode
        CmdSearchProcess.Enabled = mMode
    End Sub
    Private Function CheckDate(ByRef pTxtDate As System.Windows.Forms.TextBox) As Boolean
        CheckDate = True
        If Trim(pTxtDate.Text) = "" Then Exit Function
        If Not IsDate(pTxtDate.Text) Then
            MsgBox("Not a Valid Date")
            CheckDate = False
        Else
            Select Case pTxtDate.Name
                Case txtJanPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/01/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("31/01/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtFebPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/02/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("28/02/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtMarPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/03/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("31/03/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtAprPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/04/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("30/04/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtMayPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/05/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("31/05/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtJunPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/06/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("30/06/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtJulPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/07/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("31/07/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtAugPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/08/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("30/08/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtSepPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/09/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("31/09/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtOctPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/10/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("30/10/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtNovPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/11/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("31/11/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtDecPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/12/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("30/12/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
            End Select
            pTxtDate.Text = VB6.Format(pTxtDate.Text, "DD/MM/YYYY")
        End If
    End Function
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsRevalidation, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Call ShowReport(Crystal.DestinationConstants.crptToWindow)
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdSearchMachine_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMachine.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "MAN_MACHINE_MST", "MACHINE_DESC", "MACHINE_NO", , , SqlStr) = True Then
            txtMachine.Text = AcName1
            lblMachine.text = AcName
        End If

    End Sub

    Private Sub cmdSearchNumber_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNumber.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REVAL,LENGTH(AUTO_KEY_REVAL)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster("", "QAL_REVALIDATIONPLAN_TRN", "AUTO_KEY_REVAL", "CAL_YEAR", "OPR_CODE", "MACHINE_NO", SqlStr) = True Then
            txtNumber.Text = AcName
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub cmdSearchProcess_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchProcess.Click
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", , , SqlStr) = True Then
            txtProcess.Text = AcName1
            lblProcess.text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        Call ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsRevalidation.EOF = False Then RsRevalidation.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        ''Resume
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtNumber.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsRevalidation.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_REVALIDATIONPLAN_TRN", (txtNumber.Text), RsRevalidation) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_REVALIDATIONPLAN_TRN WHERE AUTO_KEY_REVAL=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsRevalidation.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        '    PubDBCn.Errors.Clear
        RsRevalidation.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub frmRevalidationPlan_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmRevalidationPlan_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        txtNumber.Text = SprdView.Text
        txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub frmRevalidationPlan_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "Annual Calibration Schedule"
        SqlStr = " Select * From QAL_REVALIDATIONPLAN_TRN Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRevalidation, ADODB.LockTypeEnum.adLockReadOnly)
        Call SetTextLengths()
        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        Clear1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '   Resume
    End Sub
    Private Sub frmRevalidationPlan_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(4935)
        Me.Width = VB6.TwipsToPixelsX(8295)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmRevalidationPlan_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsRevalidation.Close()
        RsRevalidation = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart

        If Not RsRevalidation.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsRevalidation.Fields("AUTO_KEY_REVAL").Value), "", RsRevalidation.Fields("AUTO_KEY_REVAL").Value)
            txtNumber.Text = IIf(IsDbNull(RsRevalidation.Fields("AUTO_KEY_REVAL").Value), "", RsRevalidation.Fields("AUTO_KEY_REVAL").Value)
            txtYear.Text = IIf(IsDbNull(RsRevalidation.Fields("CAL_YEAR").Value), "", RsRevalidation.Fields("CAL_YEAR").Value)
            txtProcess.Text = IIf(IsDbNull(RsRevalidation.Fields("OPR_CODE").Value), "", RsRevalidation.Fields("OPR_CODE").Value)
            TxtProcess_Validating(TxtProcess, New System.ComponentModel.CancelEventArgs(False))
            txtMachine.Text = IIf(IsDbNull(RsRevalidation.Fields("MACHINE_NO").Value), "", RsRevalidation.Fields("MACHINE_NO").Value)
            txtmachine_Validating(txtmachine, New System.ComponentModel.CancelEventArgs(False))
            txtJanPlan.Text = IIf(IsDbNull(RsRevalidation.Fields("JAN_DATE").Value), "", RsRevalidation.Fields("JAN_DATE").Value)
            txtFebPlan.Text = IIf(IsDbNull(RsRevalidation.Fields("FEB_DATE").Value), "", RsRevalidation.Fields("FEB_DATE").Value)
            txtMarPlan.Text = IIf(IsDbNull(RsRevalidation.Fields("MAR_DATE").Value), "", RsRevalidation.Fields("MAR_DATE").Value)
            txtAprPlan.Text = IIf(IsDbNull(RsRevalidation.Fields("APR_DATE").Value), "", RsRevalidation.Fields("APR_DATE").Value)
            txtMayPlan.Text = IIf(IsDbNull(RsRevalidation.Fields("MAY_DATE").Value), "", RsRevalidation.Fields("MAY_DATE").Value)
            txtJunPlan.Text = IIf(IsDbNull(RsRevalidation.Fields("JUN_DATE").Value), "", RsRevalidation.Fields("JUN_DATE").Value)
            txtJulPlan.Text = IIf(IsDbNull(RsRevalidation.Fields("JUL_DATE").Value), "", RsRevalidation.Fields("JUL_DATE").Value)
            txtAugPlan.Text = IIf(IsDbNull(RsRevalidation.Fields("AUG_DATE").Value), "", RsRevalidation.Fields("AUG_DATE").Value)
            txtSepPlan.Text = IIf(IsDbNull(RsRevalidation.Fields("SEP_DATE").Value), "", RsRevalidation.Fields("SEP_DATE").Value)
            txtOctPlan.Text = IIf(IsDbNull(RsRevalidation.Fields("OCT_DATE").Value), "", RsRevalidation.Fields("OCT_DATE").Value)
            txtNovPlan.Text = IIf(IsDbNull(RsRevalidation.Fields("NOV_DATE").Value), "", RsRevalidation.Fields("NOV_DATE").Value)
            txtDecPlan.Text = IIf(IsDbNull(RsRevalidation.Fields("DEC_DATE").Value), "", RsRevalidation.Fields("DEC_DATE").Value)
            Call MakeEnableDeField(False)
        End If
        ADDMode = False
        MODIFYMode = False
        txtNumber.Enabled = True
        cmdSearchNumber.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsRevalidation, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If IsRecordExist = True Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default : Exit Sub
        End If
        If Update1 = True Then
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Function IsRecordExist() As Boolean

        On Error GoTo IsRecordExistERR
        Dim RsTemp As ADODB.Recordset


        IsRecordExist = False
        If MODIFYMode = True Then Exit Function
        SqlStr = " SELECT AUTO_KEY_REVAL  " & vbCrLf _
                & " FROM QAL_REVALIDATIONPLAN_TRN " & vbCrLf _
                & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND CAL_YEAR =" & Val(txtYear.Text) & " " & vbCrLf _
                & " AND UPPER(LTRIM(RTRIM(OPR_CODE))) ='" & MainClass.AllowSingleQuote(UCase(txtProcess.Text)) & "'  " & vbCrLf _
                & " AND UPPER(LTRIM(RTRIM(MACHINE_NO))) = '" & MainClass.AllowSingleQuote(UCase(txtMachine.Text)) & "'  "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_REVAL").Value)
                IsRecordExist = True
            End If
        End With
        Exit Function
IsRecordExistERR:
        IsRecordExist = True
        MsgBox(Err.Description)
    End Function

    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_REVAL)  " & vbCrLf & " FROM QAL_REVALIDATIONPLAN_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REVAL,LENGTH(AUTO_KEY_REVAL)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSlipNo As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""
        mSlipNo = Val(txtNumber.Text)
        If Val(txtNumber.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtNumber.Text = CStr(mSlipNo)
        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_REVALIDATIONPLAN_TRN " & vbCrLf _
                            & " (COMPANY_CODE,AUTO_KEY_REVAL,CAL_YEAR," & vbCrLf _
                            & " OPR_CODE,MACHINE_NO," & vbCrLf _
                            & " JAN_DATE,FEB_DATE,MAR_DATE," & vbCrLf _
                            & " APR_DATE,MAY_DATE,JUN_DATE, " & vbCrLf _
                            & " JUL_DATE,AUG_DATE,SEP_DATE, " & vbCrLf _
                            & " OCT_DATE,NOV_DATE,DEC_DATE, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & mSlipNo & "," & Val(txtYear.Text) & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtProcess.Text) & "','" & MainClass.AllowSingleQuote(txtMachine.Text) & "'," & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtJanPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtFebPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtMarPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtAprPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtMayPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtJunPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtJulPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtAugPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtSepPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtOctPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtNovPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDecPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_REVALIDATIONPLAN_TRN SET " & vbCrLf _
                    & " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ",AUTO_KEY_REVAL=" & mSlipNo & ", " & vbCrLf _
                    & " CAL_YEAR=" & Val(txtYear.Text) & ",OPR_CODE='" & MainClass.AllowSingleQuote(txtProcess.Text) & "', " & vbCrLf _
                    & " MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachine.Text) & "', " & vbCrLf _
                    & " JAN_DATE=TO_DATE('" & vb6.Format(txtJanPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " FEB_DATE=TO_DATE('" & vb6.Format(txtFebPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " MAR_DATE=TO_DATE('" & vb6.Format(txtMarPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " APR_DATE=TO_DATE('" & vb6.Format(txtAprPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " MAY_DATE=TO_DATE('" & vb6.Format(txtMayPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " JUN_DATE=TO_DATE('" & vb6.Format(txtJunPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), "
            SqlStr = SqlStr & vbCrLf _
                    & " JUL_DATE=TO_DATE('" & vb6.Format(txtJulPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " AUG_DATE=TO_DATE('" & vb6.Format(txtAugPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " SEP_DATE=TO_DATE('" & vb6.Format(txtSepPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " OCT_DATE=TO_DATE('" & vb6.Format(txtOctPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " NOV_DATE=TO_DATE('" & vb6.Format(txtNovPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " DEC_DATE=TO_DATE('" & vb6.Format(txtDecPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND AUTO_KEY_REVAL =" & Val(lblMkey.text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsRevalidation.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo VarificationErr
        FieldsVarification = True

        If Trim(txtYear.Text) = "" Then
            MsgInformation("Cal Year is empty, So unable to Save")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtProcess.Text) = "" Then
            MsgInformation("Process By is empty, So unable to Save")
            txtProcess.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtMachine.Text) = "" Then
            MsgInformation("machine is empty, So unable to Save")
            txtMachine.Focus()
            FieldsVarification = False
            Exit Function
        End If



        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Machine Details or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsRevalidation.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = " SELECT AUTO_KEY_REVAL,CAL_YEAR,OPR_CODE, " & vbCrLf & " MACHINE_NO " & vbCrLf & " FROM QAL_REVALIDATIONPLAN_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REVAL,LENGTH(AUTO_KEY_REVAL)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_REVAL"
        Call MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()

    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "Revalidation Plan"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\RevalidationPlan.rpt"

        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub


    Private Sub txtAprPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAprPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAprPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAprPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtAprPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtAugPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAugPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAugPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAugPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtAugPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtDecPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDecPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDecPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDecPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtDecPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtFebPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFebPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFebPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFebPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtFebPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtmachine_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachine.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtmachine_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachine.DoubleClick
        Call cmdSearchMachine_Click(cmdSearchMachine, New System.EventArgs())
    End Sub

    Private Sub txtmachine_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMachine.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMachine_Click(cmdSearchMachine, New System.EventArgs())
    End Sub

    Private Sub txtmachine_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachine.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String
        If Trim(txtMachine.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtMachine, "MACHINE_NO", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Machine Does Not Exist In Master.")
            Cancel = True
        Else
            lblMachine.text = MasterNo
        End If
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtJanPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtJanPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtJanPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtJanPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtJanPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtJulPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtJulPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtJulPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtJulPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtJulPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtJunPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtJunPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtJunPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtJunPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtJunPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtMarPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMarPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMarPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMarPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtMarPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtMayPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMayPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMayPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMayPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtMayPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNovPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNovPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNovPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNovPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtNovPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNumber_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.DoubleClick
        Call cmdSearchNumber_Click(cmdSearchNumber, New System.EventArgs())
    End Sub

    Private Sub txtNumber_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtNumber.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchNumber_Click(cmdSearchNumber, New System.EventArgs())
    End Sub

    Private Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsRevalidation.EOF = False Then xMKey = RsRevalidation.Fields("AUTO_KEY_REVAL").Value
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_REVALIDATIONPLAN_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REVAL,LENGTH(AUTO_KEY_REVAL)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_REVAL=" & Val(txtNumber.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRevalidation, ADODB.LockTypeEnum.adLockReadOnly)
        If RsRevalidation.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            xMKey = RsRevalidation.Fields("AUTO_KEY_REVAL").Value
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Such Number Does Not Exist" & vbCrLf & "Click Add To Add for New.", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = " SELECT * " & vbCrLf & " FROM QAL_REVALIDATIONPLAN_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REVAL,LENGTH(AUTO_KEY_REVAL)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_REVAL=" & xMKey & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRevalidation, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtProcess.Maxlength = RsRevalidation.Fields("OPR_CODE").DefinedSize
        txtMachine.Maxlength = RsRevalidation.Fields("MACHINE_NO").DefinedSize
        txtJanPlan.Maxlength = RsRevalidation.Fields("JAN_DATE").DefinedSize - 6
        txtFebPlan.Maxlength = RsRevalidation.Fields("FEB_DATE").DefinedSize - 6
        txtMarPlan.Maxlength = RsRevalidation.Fields("MAR_DATE").DefinedSize - 6
        txtAprPlan.Maxlength = RsRevalidation.Fields("APR_DATE").DefinedSize - 6
        txtMayPlan.Maxlength = RsRevalidation.Fields("MAY_DATE").DefinedSize - 6
        txtJunPlan.Maxlength = RsRevalidation.Fields("JUN_DATE").DefinedSize - 6
        txtJulPlan.Maxlength = RsRevalidation.Fields("JUL_DATE").DefinedSize - 6
        txtAugPlan.Maxlength = RsRevalidation.Fields("AUG_DATE").DefinedSize - 6
        txtSepPlan.Maxlength = RsRevalidation.Fields("SEP_DATE").DefinedSize - 6
        txtOctPlan.Maxlength = RsRevalidation.Fields("OCT_DATE").DefinedSize - 6
        txtNovPlan.Maxlength = RsRevalidation.Fields("NOV_DATE").DefinedSize - 6
        txtDecPlan.Maxlength = RsRevalidation.Fields("DEC_DATE").DefinedSize - 6
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_ColWidth(0, 0)
            .set_ColWidth(1, 1500 * 2)
            .set_ColWidth(2, 1500 * 1)
            .set_ColWidth(3, 1500 * 1)
            .set_ColWidth(4, 1500 * 1)
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtNumber_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNumber.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNumber.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOctPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOctPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOctPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOctPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtOctPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtProcess_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProcess.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtProcess_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProcess.DoubleClick
        Call cmdSearchProcess_Click(cmdSearchProcess, New System.EventArgs())
    End Sub

    Private Sub TxtProcess_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProcess.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchProcess_Click(cmdSearchProcess, New System.EventArgs())
    End Sub

    Private Sub TxtProcess_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProcess.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String
        If Trim(txtProcess.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtProcess, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Process Does Not Exist In Master.")
            Cancel = True
        Else
            lblProcess.text = MasterNo
        End If
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSepPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSepPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSepPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSepPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtSepPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtYear_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtYear.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
End Class
