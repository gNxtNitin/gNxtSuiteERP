Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmGaugeFixCal
    Inherits System.Windows.Forms.Form
    Dim RsCalibCertGauge As ADODB.Recordset
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
            If RsCalibCertGauge.EOF = False Then RsCalibCertGauge.MoveFirst()
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
        Dim SqlStr As String

        If txtNumber.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsCalibCertGauge.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_GAUGE_CALIB_TRN", (txtNumber.Text), RsCalibCertGauge) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_GAUGE_CALIB_TRN WHERE AUTO_KEY_CALIB=" & Val(lblMkey.Text) & "")


                SqlStr = " UPDATE QAL_IMTE_SCHD_DET SET " & vbCrLf & " PM_DONE='' " & vbCrLf & " WHERE DOCNO ='" & MainClass.AllowSingleQuote(lblDocNo.Text) & "' " & vbCrLf & " AND CHECK_TYPE ='PM' " & vbCrLf & " AND AUTO_KEY_SCHD=" & vbCrLf & " (SELECT AUTO_KEY_SCHD FROM QAL_IMTE_SCHD_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DOC_TYPE='G'" & vbCrLf & " AND SCHD_MONTH=" & Val(VB6.Format(txtDate.Text, "MM")) & " " & vbCrLf & " AND SCHD_YEAR=TO_DATE(" & Val(VB6.Format(txtDate.Text, "YYYY")) & ",'DD-MON-YYYY')) "

                PubDBCn.Execute(SqlStr)


                PubDBCn.CommitTrans()
                RsCalibCertGauge.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsCalibCertGauge.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsCalibCertGauge, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        SqlStr = " SELECT AUTO_KEY_CALIB " & vbCrLf _
                    & " From QAL_GAUGE_CALIB_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND CALIB_DATE =TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                    & " AND GAUGE_NO = '" & MainClass.AllowSingleQuote(txtGaugeNo.Text) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_CALIB").Value)
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
        Call ReportOnCalibCartGauge(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnCalibCartGauge(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnCalibCartGauge(ByRef Mode As Crystal.DestinationConstants)

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
        SqlStr = " DELETE FROM QAL_GAUGE_CALIB_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND AUTO_KEY_CALIB =" & Val(txtNumber.Text) & ""

        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        SqlStr = " INSERT INTO QAL_GAUGE_CALIB_TRN " & vbCrLf _
                    & " (AUTO_KEY_CALIB,COMPANY_CODE,FYEAR," & vbCrLf _
                    & " CALIB_DATE,GAUGE_NO,PartName," & vbCrLf _
                    & " ACTUAL_GOSIZE,ACTUAL_NOGOSIZE,CALIBDUE_DATE,CALIB_REMARKS, " & vbCrLf _
                    & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                    & " VALUES ( " & vbCrLf _
                    & " " & Val(txtNumber.Text) & "," & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                    & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtGaugeNo.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtPartName.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtActualGoSize.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtActualNoGoSize.Text) & "', " & vbCrLf _
                    & " TO_DATE('" & vb6.Format(lblDueDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"


        PubDBCn.Execute(SqlStr)

        SqlStr1 = ""
        SqlStr1 = " SELECT AUTO_KEY_CALIB " & vbCrLf _
                    & " FROM QAL_GAUGE_CALIB_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND  FYEAR=" & RsCompany.Fields("FYEAR").Value & vbCrLf _
                    & " AND GAUGE_NO='" & MainClass.AllowSingleQuote(txtGaugeNo.Text) & "' " & vbCrLf _
                    & " AND CALIB_DATE=" & vbCrLf _
                    & " (SELECT Max(CALIB_DATE) " & vbCrLf _
                    & " FROM QAL_GAUGE_CALIB_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND  FYEAR=" & RsCompany.Fields("FYEAR").Value & vbCrLf _
                    & " AND GAUGE_NO='" & MainClass.AllowSingleQuote(txtGaugeNo.Text) & "')"

        MainClass.UOpenRecordSet(SqlStr1, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not mRsTemp.EOF Then
            If mRsTemp.Fields("AUTO_KEY_CALIB").Value = Val(txtNumber.Text) Then
                SqlStr = ""
                SqlStr = " UPDATE QAL_GAUGEFIX_MST SET " & vbCrLf & " VDONEON=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " VDUEON=TO_DATE('" & VB6.Format(lblDueDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO =" & Val(lblDocNo.Text) & ""

                PubDBCn.Execute(SqlStr)

                SqlStr = " UPDATE QAL_IMTE_SCHD_DET SET " & vbCrLf & " PM_DONE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE DOCNO ='" & MainClass.AllowSingleQuote(lblDocNo.Text) & "' " & vbCrLf & " AND CHECK_TYPE ='PM' " & vbCrLf & " AND AUTO_KEY_SCHD=" & vbCrLf & " (SELECT AUTO_KEY_SCHD FROM QAL_IMTE_SCHD_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DOC_TYPE='G'" & vbCrLf & " AND SCHD_MONTH=" & Val(VB6.Format(txtDate.Text, "MM")) & " " & vbCrLf & " AND SCHD_YEAR=TO_DATE(" & Val(VB6.Format(txtDate.Text, "YYYY")) & ",'DD-MON-YYYY')) "

                PubDBCn.Execute(SqlStr)

            End If
        End If

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsCalibCertGauge.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_CALIB)  " & vbCrLf & " FROM QAL_GAUGE_CALIB_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

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
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtNumber.Text, "QAL_GAUGE_CALIB_TRN", "AUTO_KEY_CALIB", "CALIB_DATE", "GAUGE_NO", "", SqlStr) = True Then
            txtNumber.Text = AcName
            Call txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False)) ''_Validating(XXXX, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub CmdSearchGauge_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchGauge.Click
        On Error GoTo CompERR
        Dim SqlStr As String
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND Type IN ('GAUGE','PGG')"
        If MainClass.SearchGridMaster("", "QAL_GAUGEFIX_MST", "TypeNo", "Description", "Type", "Model", SqlStr) = True Then
            txtGaugeNo.Text = AcName
            If txtGaugeNo.Enabled = True Then txtGaugeNo.Focus()
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
        MainClass.ButtonStatus(Me, XRIGHT, RsCalibCertGauge, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub frmGaugeFixCal_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Gauge Fixture Calibration"

        SqlStr = "Select * From QAL_GAUGE_CALIB_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCalibCertGauge, ADODB.LockTypeEnum.adLockReadOnly)


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

    Private Sub frmGaugeFixCal_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmGaugeFixCal_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(6180)
        Me.Width = VB6.TwipsToPixelsX(9285)
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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_CALIB AS SLIP_NUMBER,TO_CHAR(CALIB_DATE,'DD/MM/YYYY') AS CALIB_DATE, " & vbCrLf & " GAUGE_NO,ACTUAL_GOSIZE,ACTUAL_NOGOSIZE,TO_CHAR(CALIBDUE_DATE,'DD/MM/YYYY') AS CALIBDUE_DATE,PARTNAME AS INSTRUMENT_USED " & vbCrLf & " FROM QAL_GAUGE_CALIB_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_CALIB"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtDate.Maxlength = RsCalibCertGauge.Fields("CALIB_DATE").DefinedSize - 6
        txtActualGoSize.Maxlength = RsCalibCertGauge.Fields("ACTUAL_GOSIZE").DefinedSize
        txtActualNoGoSize.Maxlength = RsCalibCertGauge.Fields("ACTUAL_NOGOSIZE").DefinedSize
        txtPartName.Maxlength = RsCalibCertGauge.Fields("PARTNAME").DefinedSize
        txtRemarks.Maxlength = RsCalibCertGauge.Fields("CALIB_REMARKS").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
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

        If MODIFYMode = True And RsCalibCertGauge.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtGaugeNo.Text) = "" Then
            MsgInformation("Gauge No. is empty, So unable to save.")
            txtGaugeNo.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtActualGoSize.Text) = "" Then
            MsgInformation("Actual GoSize is empty, So unable to save.")
            txtActualGoSize.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtActualNoGoSize.Text) = "" Then
            MsgInformation("Actual NoGoSize is empty, So unable to save.")
            txtActualNoGoSize.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = True Then
            If CheckGauge_IMTEPMSchd((lblDocNo.Text), CDate(txtDate.Text), "G", "PM") = False Then
                FieldsVarification = False
                Exit Function
            End If
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

        If Not RsCalibCertGauge.EOF Then
            mIsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsCalibCertGauge.Fields("AUTO_KEY_CALIB").Value), "", RsCalibCertGauge.Fields("AUTO_KEY_CALIB").Value)
            txtNumber.Text = IIf(IsDbNull(RsCalibCertGauge.Fields("AUTO_KEY_CALIB").Value), "", RsCalibCertGauge.Fields("AUTO_KEY_CALIB").Value)
            txtDate.Text = IIf(IsDbNull(RsCalibCertGauge.Fields("CALIB_DATE").Value), "", RsCalibCertGauge.Fields("CALIB_DATE").Value)
            txtGaugeNo.Text = IIf(IsDbNull(RsCalibCertGauge.Fields("GAUGE_NO").Value), "", RsCalibCertGauge.Fields("GAUGE_NO").Value)
            txtGaugeNo_Validating(txtGaugeNo, New System.ComponentModel.CancelEventArgs(False))
            txtPartName.Text = IIf(IsDbNull(RsCalibCertGauge.Fields("PartName").Value), "", RsCalibCertGauge.Fields("PartName").Value)
            txtActualGoSize.Text = IIf(IsDbNull(RsCalibCertGauge.Fields("ACTUAL_GOSIZE").Value), "", RsCalibCertGauge.Fields("ACTUAL_GOSIZE").Value)
            txtActualNoGoSize.Text = IIf(IsDbNull(RsCalibCertGauge.Fields("ACTUAL_NOGOSIZE").Value), "", RsCalibCertGauge.Fields("ACTUAL_NOGOSIZE").Value)
            lblDueDate.Text = IIf(IsDbNull(RsCalibCertGauge.Fields("CALIBDUE_DATE").Value), "", RsCalibCertGauge.Fields("CALIBDUE_DATE").Value)
            txtRemarks.Text = IIf(IsDbNull(RsCalibCertGauge.Fields("CALIB_REMARKS").Value), "", RsCalibCertGauge.Fields("CALIB_REMARKS").Value)
            Call MakeEnableDesableField(False)
            mIsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        txtNumber.Enabled = True
        cmdSearchNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsCalibCertGauge, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        'txtDate.Enabled = mMode
        txtGaugeNo.Enabled = mMode
        CmdSearchGauge.Enabled = mMode
        'lblDueDate.Enabled = mMode
    End Sub

    Private Sub Clear1()

        On Error GoTo ClearErr
        lblMkey.Text = ""
        txtNumber.Text = ""
        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtGaugeNo.Text = ""
        lblGaugeDesc.Text = ""
        lblCustomer.Text = ""
        txtPartName.Text = ""
        lblModel.Text = ""
        lblDepartment.Text = ""
        lblGoSize.Text = ""
        lblNoGoSize.Text = ""
        lblWearSize.Text = ""
        lblCompSize.Text = ""
        lblFrequency.Text = ""
        txtActualGoSize.Text = ""
        txtActualNoGoSize.Text = ""
        lblDueDate.Text = ""
        txtRemarks.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsCalibCertGauge, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

    Private Sub frmGaugeFixCal_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        RsCalibCertGauge.Close()
        RsCalibCertGauge = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub txtActualGoSize_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtActualGoSize.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtActualNoGoSize_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtActualNoGoSize.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        Else
            lblDueDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, Val(lblFrequency.Text), CDate(txtDate.Text)))
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtGaugeNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGaugeNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGaugeNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGaugeNo.DoubleClick
        Call CmdSearchGauge_Click(CmdSearchGauge, New System.EventArgs())
    End Sub

    Private Sub txtGaugeNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtGaugeNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchGauge_Click(CmdSearchGauge, New System.EventArgs())
    End Sub

    Public Sub txtGaugeNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGaugeNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtGaugeNo.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT DocNo,TypeNo,Description,Customer,Model,Location," & vbCrLf _
                    & " ReqGoSize,ReqNoGoSize,CompSize,WearSize,ValFrequency " & vbCrLf _
                    & " FROM QAL_GAUGEFIX_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND Type IN ('GAUGE','PGG') " & vbCrLf _
                    & " AND TypeNo='" & MainClass.AllowSingleQuote(txtGaugeNo.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                lblDocNo.Text = IIf(IsDbNull(mRsTemp.Fields("DOCNO").Value), "", .Fields("DOCNO").Value)
                lblGaugeDesc.Text = IIf(IsDbNull(mRsTemp.Fields("Description").Value), "", .Fields("Description").Value)
                lblCustomer.Text = IIf(IsDbNull(mRsTemp.Fields("Customer").Value), "", .Fields("Customer").Value)
                lblModel.Text = IIf(IsDbNull(mRsTemp.Fields("MODEL").Value), "", .Fields("MODEL").Value)
                lblDepartment.Text = IIf(IsDbNull(mRsTemp.Fields("Location").Value), "", .Fields("Location").Value)
                lblGoSize.Text = IIf(IsDbNull(mRsTemp.Fields("ReqGoSize").Value), "", .Fields("ReqGoSize").Value)
                lblNoGoSize.Text = IIf(IsDbNull(mRsTemp.Fields("ReqNoGoSize").Value), "", .Fields("ReqNoGoSize").Value)
                lblWearSize.Text = IIf(IsDbNull(mRsTemp.Fields("WearSize").Value), "", .Fields("WearSize").Value)
                lblCompSize.Text = IIf(IsDbNull(mRsTemp.Fields("CompSize").Value), "", .Fields("CompSize").Value)
                lblFrequency.Text = IIf(IsDbNull(mRsTemp.Fields("ValFrequency").Value), "", .Fields("ValFrequency").Value)
            Else
                MsgBox("Not a valid Gauge No.")
                lblDocNo.Text = ""
                lblGaugeDesc.Text = ""
                lblCustomer.Text = ""
                lblModel.Text = ""
                lblDepartment.Text = ""
                lblGoSize.Text = ""
                lblNoGoSize.Text = ""
                lblWearSize.Text = ""
                lblCompSize.Text = ""
                lblFrequency.Text = ""
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPartName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartName.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

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

        If MODIFYMode = True And RsCalibCertGauge.BOF = False Then xMKey = RsCalibCertGauge.Fields("AUTO_KEY_CALIB").Value

        SqlStr = "SELECT * FROM QAL_GAUGE_CALIB_TRN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_CALIB=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCalibCertGauge, ADODB.LockTypeEnum.adLockReadOnly)
        If RsCalibCertGauge.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_GAUGE_CALIB_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_CALIB=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCalibCertGauge, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
