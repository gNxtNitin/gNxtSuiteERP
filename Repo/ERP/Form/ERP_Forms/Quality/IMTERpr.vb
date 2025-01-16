Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmIMTERpr
    Inherits System.Windows.Forms.Form
    Dim RsIMTECalibRpr As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim mIsShowing As Boolean

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtNumber.Enabled = False
            cmdSearchNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsIMTECalibRpr.EOF = False Then RsIMTECalibRpr.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtNumber.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsIMTECalibRpr.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_IMTE_REPAIR", (txtNumber.Text), RsIMTECalibRpr) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_IMTE_REPAIR WHERE AUTO_KEY_REPAIR=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsIMTECalibRpr.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsIMTECalibRpr.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsIMTECalibRpr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtNumber.Enabled = False
            cmdSearchNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
        Resume
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then Exit Sub
        If IsRecordExist = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
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
        SqlStr = " SELECT AUTO_KEY_REPAIR " & vbCrLf & " From QAL_IMTE_REPAIR " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SEND_DATE =TO_DATE('" & VB6.Format(txtSendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND DOCNO = " & Val(txtDocNo.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_REPAIR").Value)
                IsRecordExist = True
            End If
        End With
        Exit Function
IsRecordExistERR:
        IsRecordExist = True
        MsgBox(Err.Description)
    End Function

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnIMTERpr(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnIMTERpr(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnIMTERpr(ByRef Mode As Crystal.DestinationConstants)

    End Sub

    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim mRsTemp As ADODB.Recordset

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If Val(txtNumber.Text) = 0 Then
            txtNumber.Text = CStr(AutoGenKeyNo())
        End If

        SqlStr = ""
        SqlStr = " DELETE FROM QAL_IMTE_REPAIR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_REPAIR =" & Val(txtNumber.Text) & ""

        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        SqlStr = " INSERT INTO QAL_IMTE_REPAIR " & vbCrLf _
                    & " (AUTO_KEY_REPAIR,COMPANY_CODE," & vbCrLf _
                    & " DOCNO,SEND_DATE,RECD_DATE," & vbCrLf _
                    & " REPAIR_AGENCY,REPAIR_DETAIL,REPAIR_AMT, " & vbCrLf _
                    & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                    & " VALUES ( " & vbCrLf _
                    & " " & Val(txtNumber.Text) & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " " & Val(txtDocNo.Text) & ", " & vbCrLf _
                    & " TO_DATE('" & vb6.Format(txtSendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " TO_DATE('" & vb6.Format(txtRecdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtRepairAgency.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtRepairDetail.Text) & "', " & vbCrLf _
                    & " " & Val(txtRepairAmt.Text) & ", " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"


        PubDBCn.Execute(SqlStr)

        SqlStr1 = ""
        SqlStr1 = " SELECT AUTO_KEY_REPAIR " & vbCrLf & " FROM QAL_IMTE_REPAIR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REPAIR,LENGTH(AUTO_KEY_REPAIR)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND DOCNO=" & Val(txtDocNo.Text) & " " & vbCrLf & " AND SEND_DATE=" & vbCrLf & " (SELECT Max(SEND_DATE) " & vbCrLf & " FROM QAL_IMTE_REPAIR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REPAIR,LENGTH(AUTO_KEY_REPAIR)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND DOCNO=" & Val(txtDocNo.Text) & ")"

        MainClass.UOpenRecordSet(SqlStr1, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not mRsTemp.EOF Then
            If mRsTemp.Fields("AUTO_KEY_REPAIR").Value = Val(txtNumber.Text) Then
                SqlStr = ""
                SqlStr = " UPDATE QAL_IMTE_MST SET " & vbCrLf & " CALIB_OK='R', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO =" & Val(txtDocNo.Text) & " " & vbCrLf & " AND LCDATE <=TO_DATE('" & VB6.Format(txtSendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND CALIB_OK='N' "

                PubDBCn.Execute(SqlStr)
            End If
        End If

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsIMTECalibRpr.Requery()
        MsgBox(Err.Description)
        'Resume
    End Function

    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_REPAIR)  " & vbCrLf & " FROM QAL_IMTE_REPAIR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

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

    Private Sub cmdSearchNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REPAIR,LENGTH(AUTO_KEY_REPAIR)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtNumber.Text, "QAL_IMTE_REPAIR", "AUTO_KEY_REPAIR", "SEND_DATE", "RECD_DATE", "DOCNO", SqlStr) = True Then
            txtNumber.Text = AcName
            Call txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub cmdSearchDocNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchDocNo.Click
        On Error GoTo CompERR
        Dim SqlStr As String
        '    If PubSuperUser = "Y" Then
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
        '    Else
        '        SqlStr = " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND CALIB_OK IN('N','R') "
        '    End If
        If MainClass.SearchGridMaster("", "QAL_IMTE_MST", "DocNo", "Description", "E_NO", "L_C", SqlStr) = True Then
            txtDocNo.Text = AcName
            If txtDocNo.Enabled = True Then txtDocNo.Focus()
        End If
        Exit Sub
CompERR:
        MsgBox(Err.Description)
    End Sub

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
        MainClass.ButtonStatus(Me, XRIGHT, RsIMTECalibRpr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub frmIMTERpr_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "IMTE Repair"

        SqlStr = "Select * From QAL_IMTE_REPAIR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTECalibRpr, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        Call Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub frmIMTERpr_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmIMTERpr_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(6180)
        'Me.Width = VB6.TwipsToPixelsX(9285)
        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_REPAIR AS SLIP_NUMBER,TO_CHAR(SEND_DATE,'DD/MM/YYYY') AS SEND_DATE,TO_CHAR(RECD_DATE,'DD/MM/YYYY') AS RECEIVED_DATE, " & vbCrLf & " DOCNO,REPAIR_AGENCY,REPAIR_DETAIL,REPAIR_AMT " & vbCrLf & " FROM QAL_IMTE_REPAIR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REPAIR,LENGTH(AUTO_KEY_REPAIR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_REPAIR"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSendDate.Maxlength = RsIMTECalibRpr.Fields("SEND_DATE").DefinedSize - 6
        txtRecdDate.Maxlength = RsIMTECalibRpr.Fields("RECD_DATE").DefinedSize - 6
        txtRepairAgency.Maxlength = RsIMTECalibRpr.Fields("REPAIR_AGENCY").DefinedSize
        txtRepairDetail.Maxlength = RsIMTECalibRpr.Fields("REPAIR_DETAIL").DefinedSize
        txtRepairAmt.Maxlength = RsIMTECalibRpr.Fields("REPAIR_AMT").Precision
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
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

        If MODIFYMode = True And RsIMTECalibRpr.EOF = True Then Exit Function

        If Trim(txtDocNo.Text) = "" Then
            MsgInformation("Doc No. is empty, So unable to save.")
            txtDocNo.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtSendDate.Text) = "" Then
            MsgInformation("Send Date is empty, So unable to save.")
            txtSendDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtRecdDate.Text) = "" Then
            MsgInformation("Received Date is empty, So unable to save.")
            txtRecdDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtRepairAgency.Text) = "" Then
            MsgInformation("Repairing Agency is empty, So unable to save.")
            txtRepairAgency.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtRepairDetail.Text) = "" Then
            MsgInformation("Repairing Detail is empty, So unable to save.")
            txtRepairDetail.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtRepairAmt.Text) = "" Then
            MsgInformation("Repairing Amount is empty, So unable to save.")
            txtRepairAmt.Focus()
            FieldsVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtNumber.Text = SprdView.Text
        txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart

        If Not RsIMTECalibRpr.EOF Then
            mIsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsIMTECalibRpr.Fields("AUTO_KEY_REPAIR").Value), "", RsIMTECalibRpr.Fields("AUTO_KEY_REPAIR").Value)
            txtNumber.Text = IIf(IsDbNull(RsIMTECalibRpr.Fields("AUTO_KEY_REPAIR").Value), "", RsIMTECalibRpr.Fields("AUTO_KEY_REPAIR").Value)
            txtDocNo.Text = IIf(IsDbNull(RsIMTECalibRpr.Fields("DOCNO").Value), "", RsIMTECalibRpr.Fields("DOCNO").Value)
            txtDocNo_Validating(txtDocNo, New System.ComponentModel.CancelEventArgs(False))
            txtSendDate.Text = IIf(IsDbNull(RsIMTECalibRpr.Fields("SEND_DATE").Value), "", RsIMTECalibRpr.Fields("SEND_DATE").Value)
            txtRecdDate.Text = IIf(IsDbNull(RsIMTECalibRpr.Fields("RECD_DATE").Value), "", RsIMTECalibRpr.Fields("RECD_DATE").Value)
            txtRepairAgency.Text = IIf(IsDbNull(RsIMTECalibRpr.Fields("REPAIR_AGENCY").Value), "", RsIMTECalibRpr.Fields("REPAIR_AGENCY").Value)
            txtRepairDetail.Text = IIf(IsDbNull(RsIMTECalibRpr.Fields("REPAIR_DETAIL").Value), "", RsIMTECalibRpr.Fields("REPAIR_DETAIL").Value)
            txtRepairAmt.Text = IIf(IsDbNull(RsIMTECalibRpr.Fields("REPAIR_AMT").Value), "", RsIMTECalibRpr.Fields("REPAIR_AMT").Value)
            Call MakeEnableDesableField(False)
            mIsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        txtNumber.Enabled = True
        cmdSearchNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsIMTECalibRpr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        'txtDate.Enabled = mMode
        txtDocNo.Enabled = mMode
        cmdSearchDocNo.Enabled = mMode
        'lblDueDate.Enabled = mMode
    End Sub

    Private Sub Clear1()

        On Error GoTo ClearErr
        lblMkey.Text = ""
        txtNumber.Text = ""
        txtDocNo.Text = ""
        txtSendDate.Text = ""
        txtRecdDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        lblDescription.Text = ""
        lblENo.Text = ""
        lblMakersNo.Text = ""
        lblLC.Text = ""
        lblMake.Text = ""
        lblLocation.Text = ""
        lblFrequency.Text = ""
        txtRepairAgency.Text = ""
        txtRepairDetail.Text = ""
        txtRepairAmt.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsIMTECalibRpr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
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

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500 * 2)
            .set_ColWidth(1, 500 * 4)
            .set_ColWidth(2, 500 * 3)
            .set_ColWidth(3, 500 * 3)
            .set_ColWidth(4, 500 * 3)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 3)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub frmIMTERpr_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        RsIMTECalibRpr.Close()
        RsIMTECalibRpr = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub txtRepairAgency_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRepairAgency.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRepairAmt_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRepairAmt.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRepairDetail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRepairDetail.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSendDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSendDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSendDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSendDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtSendDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtSendDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRecdDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRecdDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRecdDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRecdDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRecdDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtRecdDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDocNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDocNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDocNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDocNo.DoubleClick
        Call cmdSearchDocNo_Click(cmdSearchDocNo, New System.EventArgs())
    End Sub

    Private Sub txtDocNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDocNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDocNo_Click(cmdSearchDocNo, New System.EventArgs())
    End Sub

    Public Sub txtDocNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDocNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtDocNo.Text) = "" Then GoTo EventExitSub
        '    If PubSuperUser = "Y" Then
        SqlStr = " SELECT DocNo,Description,E_NO,MaRkers_No,L_C,MAKE_NAME,Location," & vbCrLf & " ValFrequency " & vbCrLf & " FROM QAL_IMTE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DocNo=" & Val(txtDocNo.Text) & ""
        '    Else
        '        SqlStr = " SELECT DocNo,Description,E_NO,MaRkers_No,L_C,MAKE_NAME,Location," & vbCrLf _
        ''                & " ValFrequency " & vbCrLf _
        ''                & " FROM QAL_IMTE_MST " & vbCrLf _
        ''                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
        ''                & " AND Calib_OK IN ('N','R') " & vbCrLf _
        ''                & " AND DocNo=" & Val(txtDocNo.Text) & ""
        '    End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                txtDocNo.Text = IIf(IsDbNull(mRsTemp.Fields("DOCNO").Value), "", .Fields("DOCNO").Value)
                lblDescription.Text = IIf(IsDbNull(mRsTemp.Fields("Description").Value), "", .Fields("Description").Value)
                lblENo.Text = IIf(IsDbNull(mRsTemp.Fields("E_NO").Value), "", .Fields("E_NO").Value)
                lblMakersNo.Text = IIf(IsDbNull(mRsTemp.Fields("Markers_No").Value), "", .Fields("Markers_No").Value)
                lblLC.Text = IIf(IsDbNull(mRsTemp.Fields("L_C").Value), "", .Fields("L_C").Value)
                lblMake.Text = IIf(IsDbNull(mRsTemp.Fields("Make_Name").Value), "", .Fields("Make_Name").Value)
                lblLocation.Text = IIf(IsDbNull(mRsTemp.Fields("Location").Value), "", .Fields("Location").Value)
                lblFrequency.Text = IIf(IsDbNull(mRsTemp.Fields("ValFrequency").Value), "", .Fields("ValFrequency").Value)
            Else
                MsgBox("Not a valid Doc No.")
                txtDocNo.Text = ""
                lblDescription.Text = ""
                lblENo.Text = ""
                lblMakersNo.Text = ""
                lblLC.Text = ""
                lblMake.Text = ""
                lblLocation.Text = ""
                lblFrequency.Text = ""
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRepairAmt_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRepairAmt.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNumber_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.DoubleClick
        Call cmdSearchNo_Click(cmdSearchNo, New System.EventArgs())
    End Sub

    Private Sub txtNumber_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtNumber.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchNo_Click(cmdSearchNo, New System.EventArgs())
    End Sub

    Public Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String


        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub
        mSlipNo = Val(txtNumber.Text)

        If MODIFYMode = True And RsIMTECalibRpr.BOF = False Then xMKey = RsIMTECalibRpr.Fields("AUTO_KEY_REPAIR").Value

        SqlStr = "SELECT * FROM QAL_IMTE_REPAIR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REPAIR,LENGTH(AUTO_KEY_REPAIR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_REPAIR=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTECalibRpr, ADODB.LockTypeEnum.adLockReadOnly)
        If RsIMTECalibRpr.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_IMTE_REPAIR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REPAIR,LENGTH(AUTO_KEY_REPAIR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_REPAIR=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTECalibRpr, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
