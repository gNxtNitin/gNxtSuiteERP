Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmGaugeFixInsp
    Inherits System.Windows.Forms.Form
    Dim RsGaugeCalibHdr As ADODB.Recordset
    Dim RsGaugeCalibDet As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean
    Private Const ConRowHeight As Short = 14

    Dim xColorOrig As String

    Private Const ColParamDesc As Short = 1
    Private Const ColSpecification As Short = 2
    Private Const ColSpecPlus As Short = 3
    Private Const ColSpecMinus As Short = 4
    Private Const ColWearLimit As Short = 5
    Private Const ColInspMth As Short = 6
    Private Const ColBeforeCorrection As Short = 7
    Private Const ColObservation As Short = 8


    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtSlipNo.Enabled = False
            cmdSearchSlipNo.Enabled = False
            SprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsGaugeCalibHdr.EOF = False Then RsGaugeCalibHdr.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtSlipNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsGaugeCalibHdr.EOF Then
            If RsGaugeCalibHdr.Fields("APPROVED_BY").Value <> "" Then MsgBox("Number been approved, So cann't be Deleted ") : Exit Sub
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_GAUGE_CALIB_HDR", (txtSlipNo.Text), RsGaugeCalibHdr) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_GAUGE_CALIB_DET WHERE AUTO_KEY_CALIB=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_GAUGE_CALIB_HDR WHERE AUTO_KEY_CALIB=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsGaugeCalibHdr.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsGaugeCalibHdr.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            If RsGaugeCalibHdr.Fields("APPROVED_BY").Value <> "" Then MsgBox("Number been approved, So cann't be Modified ") : Exit Sub
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsGaugeCalibHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtSlipNo.Enabled = False
            cmdSearchSlipNo.Enabled = False
            Call MakeEnableDesableField(True)
            SprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then Exit Sub
        If IsRecordExist = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
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
        SqlStr = " SELECT AUTO_KEY_CALIB " & vbCrLf & " From QAL_GAUGE_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND CALIB_DATE =TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND DOCNO = " & Val(lblDocNo.Text) & " "
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

    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSlipNo As Double
        Dim mRsTemp As ADODB.Recordset

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""
        mSlipNo = Val(txtSlipNo.Text)
        If Val(txtSlipNo.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtSlipNo.Text = CStr(mSlipNo)
        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_GAUGE_CALIB_HDR " & vbCrLf _
                            & " (AUTO_KEY_CALIB,COMPANY_CODE," & vbCrLf _
                            & " CALIB_DATE,DOCNO," & vbCrLf _
                            & " REMARKS,INSPECTED_BY,APPROVED_BY, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " " & Val(lblDocNo.text) & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','" & MainClass.AllowSingleQuote(txtInspBy.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtAppBy.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_GAUGE_CALIB_HDR SET " & vbCrLf _
                    & " AUTO_KEY_CALIB=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                    & " CALIB_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " DOCNO=" & Val(lblDocNo.text) & ", " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " INSPECTED_BY='" & MainClass.AllowSingleQuote(txtInspBy.Text) & "', " & vbCrLf _
                    & " APPROVED_BY='" & MainClass.AllowSingleQuote(txtAppBy.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND AUTO_KEY_CALIB =" & Val(lblMkey.text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail = False Then GoTo ErrPart

        SqlStr = ""
        SqlStr = " SELECT AUTO_KEY_CALIB " & vbCrLf & " FROM QAL_GAUGE_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND DOCNO=" & Val(lblDocNo.Text) & " " & vbCrLf & " AND CALIB_DATE=" & vbCrLf & " (SELECT Max(CALIB_DATE) " & vbCrLf & " FROM QAL_GAUGE_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND DOCNO=" & Val(lblDocNo.Text) & ")"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not mRsTemp.EOF Then
            If mRsTemp.Fields("AUTO_KEY_CALIB").Value = Val(lblMkey.Text) Then
                SqlStr = ""
                SqlStr = " UPDATE QAL_GAUGEFIX_MST SET " & vbCrLf & " VDONEON=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " VDUEON=TO_DATE('" & VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Month, Val(lblFrequency.Text), CDate(txtDate.Text)), "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO =" & Val(lblDocNo.Text) & ""

                PubDBCn.Execute(SqlStr)

                SqlStr = " UPDATE QAL_IMTE_SCHD_DET SET " & vbCrLf & " PM_DONE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE DOCNO ='" & MainClass.AllowSingleQuote(lblDocNo.Text) & "' " & vbCrLf & " AND CHECK_TYPE ='PM' " & vbCrLf & " AND AUTO_KEY_SCHD=" & vbCrLf & " (SELECT AUTO_KEY_SCHD FROM QAL_IMTE_SCHD_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DOC_TYPE='G'" & vbCrLf & " AND SCHD_MONTH=" & Val(VB6.Format(txtDate.Text, "MM")) & " " & vbCrLf & " AND SCHD_YEAR=" & Val(VB6.Format(txtDate.Text, "YYYY")) & ") "

                PubDBCn.Execute(SqlStr)
            End If
        End If

        Update1 = True
        PubDBCn.CommitTrans()
        txtSlipNo.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsGaugeCalibHdr.Requery()
        RsGaugeCalibDet.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_CALIB)  " & vbCrLf & " FROM QAL_GAUGE_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & " "

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

    Private Function UpdateDetail() As Boolean

        On Error GoTo UpdateDetailERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mParamDesc As String
        Dim mSpecification As Double
        Dim mSpecPlus As Double
        Dim mSpecMinus As Double
        Dim mWearLimit As Double
        Dim mInspMth As String
        Dim mObservation As Double
        Dim mBeforeCorrection As Double

        PubDBCn.Execute("DELETE FROM QAL_GAUGE_CALIB_DET WHERE AUTO_KEY_CALIB=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColParamDesc
                mParamDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColSpecification
                mSpecification = Val(.Text)

                .Col = ColSpecPlus
                mSpecPlus = Val(.Text)

                .Col = ColSpecMinus
                mSpecMinus = Val(.Text)

                .Col = ColWearLimit
                mWearLimit = Val(.Text)

                .Col = ColInspMth
                mInspMth = MainClass.AllowSingleQuote(.Text)

                .Col = ColBeforeCorrection
                mBeforeCorrection = Val(.Text)

                .Col = ColObservation
                mObservation = Val(.Text)

                SqlStr = ""

                If mParamDesc <> "" Then
                    SqlStr = " INSERT INTO  QAL_GAUGE_CALIB_DET ( " & vbCrLf & " AUTO_KEY_CALIB,SERIAL_NO,PARAM_DESC,SPECIFICATION, " & vbCrLf & " SPEC_PLUS,SPEC_MINUS,WEAR_LIMIT,INSP_MTH,BEFORE_OBSERVATION,OBSERVATION ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mParamDesc & "'," & mSpecification & "," & vbCrLf & " " & mSpecPlus & "," & mSpecMinus & "," & mWearLimit & "," & vbCrLf & " '" & mInspMth & "'," & mBeforeCorrection & ", " & mObservation & " ) "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail = True
        Exit Function
UpdateDetailERR:
        UpdateDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub cmdSearchAppBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAppBy.Click
        Call SearchEmp(txtAppBy, lblAppBy)
    End Sub

    Private Sub cmdSearchInspBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchInspBy.Click
        Call SearchEmp(txtInspBy, lblInspBy)
    End Sub

    Private Sub SearchEmp(ByRef pTextBax As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label)
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            pTextBax.Text = AcName1
            pLable.text = AcName
            If pTextBax.Enabled = True Then pTextBax.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchTypeNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchTypeNo.Click
        Dim SqlStr As String
        SqlStr = "SELECT DISTINCT B.TYPENO, A.DOCNO " & vbCrLf & " FROM QAL_GAUGE_CALIB_STD A, QAL_GAUGEFIX_MST B " & vbCrLf & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf & " AND LTRIM(RTRIM(A.DOCNO)) = LTRIM(RTRIM(B.DOCNO)) " & vbCrLf & " AND A.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY B.TYPENO "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtTypeNo.Text = AcName
            lblDocNo.text = AcName1
            If txtTypeNo.Enabled = True Then txtTypeNo.Focus()
        End If
    End Sub

    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_GAUGE_CALIB_HDR", "AUTO_KEY_CALIB", "CALIB_DATE", "DOCNO", "", SqlStr) = True Then
            txtSlipNo.Text = AcName
            Call txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validating(XXXX, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
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
        MainClass.ButtonStatus(Me, XRIGHT, RsGaugeCalibHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub frmGaugeFixInsp_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Gauge Fixture Inspection (Calibration)"

        SqlStr = "Select * From QAL_GAUGE_CALIB_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGaugeCalibHdr, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_GAUGE_CALIB_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGaugeCalibDet, ADODB.LockTypeEnum.adLockReadOnly)

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

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_CALIB AS SLIP_NUMBER,TO_CHAR(CALIB_DATE,'DD/MM/YYYY') AS CALIB_DATE, " & vbCrLf & " DOCNO,REMARKS,INSPECTED_BY,APPROVED_BY " & vbCrLf & " FROM QAL_GAUGE_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_CALIB"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmGaugeFixInsp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmGaugeFixInsp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(8010)
        Me.Width = VB6.TwipsToPixelsX(10755)
        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear1()

        On Error GoTo ClearErr

        lblMkey.Text = ""
        txtSlipNo.Text = ""
        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtTypeNo.Text = ""
        lblDocNo.Text = ""
        lblDescription.Text = ""
        lblModel.Text = ""
        lblCustomer.Text = ""
        lblLocation.Text = ""
        lblDRGNo.Text = ""
        lblFrequency.Text = ""
        txtRemarks.Text = ""
        txtInspBy.Text = ""
        lblInspBy.Text = ""
        txtAppBy.Text = ""
        lblAppBy.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsGaugeCalibHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColParamDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsGaugeCalibDet.Fields("PARAM_DESC").DefinedSize
            .set_ColWidth(.Col, 22)

            .Col = ColSpecification
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.999")
            .TypeFloatMin = CDbl("-99999.999")
            .TypeEditLen = RsGaugeCalibDet.Fields("SPECIFICATION").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 3
            .set_ColWidth(.Col, 8)

            .Col = ColSpecPlus
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.999")
            .TypeFloatMin = CDbl("-99999.999")
            .TypeEditLen = RsGaugeCalibDet.Fields("SPEC_PLUS").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 3
            .set_ColWidth(.Col, 8)

            .Col = ColSpecMinus
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.999")
            .TypeFloatMin = CDbl("-99999.999")
            .TypeEditLen = RsGaugeCalibDet.Fields("SPEC_MINUS").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 3
            .set_ColWidth(.Col, 8)

            .Col = ColWearLimit
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.999")
            .TypeFloatMin = CDbl("-99999.999")
            .TypeEditLen = RsGaugeCalibDet.Fields("WEAR_LIMIT").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 3
            .set_ColWidth(.Col, 8)

            .Col = ColInspMth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsGaugeCalibDet.Fields("INSP_MTH").DefinedSize
            .set_ColWidth(.Col, 8)

            .Col = ColBeforeCorrection
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.999")
            .TypeFloatMin = CDbl("-99999.999")
            .TypeEditLen = RsGaugeCalibDet.Fields("BEFORE_OBSERVATION").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 3
            .set_ColWidth(.Col, 8)

            .Col = ColObservation
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.999")
            .TypeFloatMin = CDbl("-99999.999")
            .TypeEditLen = RsGaugeCalibDet.Fields("OBSERVATION").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 3
            .set_ColWidth(.Col, 8)

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColParamDesc, ColInspMth)
            MainClass.SetSpreadColor(SprdMain, Arow)
            .Col = ColObservation
            xColorOrig = System.Drawing.ColorTranslator.ToOle(.ForeColor).ToString
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500 * 1)
            .set_ColWidth(1, 500 * 4)
            .set_ColWidth(2, 500 * 3)
            .set_ColWidth(3, 500 * 3)
            .set_ColWidth(4, 500 * 4)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 3)
            .set_ColWidth(7, 500 * 5)
            .set_ColWidth(8, 500 * 3)
            .set_ColWidth(9, 500 * 3)
            .set_ColWidth(10, 500 * 4)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.Maxlength = RsGaugeCalibHdr.Fields("AUTO_KEY_CALIB").Precision
        txtDate.Maxlength = RsGaugeCalibHdr.Fields("CALIB_DATE").DefinedSize - 6
        txtTypeNo.Maxlength = 255
        txtRemarks.Maxlength = RsGaugeCalibHdr.Fields("REMARKS").DefinedSize
        txtInspBy.Maxlength = RsGaugeCalibHdr.Fields("INSPECTED_BY").DefinedSize
        txtAppBy.Maxlength = RsGaugeCalibHdr.Fields("APPROVED_BY").DefinedSize

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

        If MODIFYMode = True And RsGaugeCalibHdr.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtTypeNo.Text) = "" Then
            MsgInformation("Part No. empty, So unable to save.")
            txtTypeNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtInspBy.Text) = "" Then
            MsgInformation("Inspection Employee Code is empty, So unable to save.")
            txtInspBy.Focus()
            FieldsVarification = False
            Exit Function
        End If
        '    If MainClass.ValidDataInGrid(SprdMain, ColParamDesc, "S", "Please Check Parameter.") = False Then FieldsVarification = False: Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColObservation, "N", "Please Check Observation.") = False Then FieldsVarification = False: Exit Function
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmGaugeFixInsp_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        RsGaugeCalibHdr.Close()
        RsGaugeCalibHdr = Nothing
        RsGaugeCalibDet.Close()
        RsGaugeCalibDet = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        'Dim SqlStr As String
        '    If Col = 0 And Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
        '        MainClass.DeleteSprdRow SprdMain, Row, ColParamDesc
        '        MainClass.SaveStatus Me, ADDMode, MODIFYMode
        '    End If
        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim xParamDesc As String
        Dim xInspMth As String
        If eventArgs.NewRow = -1 Then Exit Sub

        With SprdMain
            .Row = .ActiveRow
            .Col = ColParamDesc
            xParamDesc = Trim(.Text)
            If xParamDesc = "" Then Exit Sub
            .Col = ColInspMth
            xInspMth = Trim(.Text)
            If xInspMth = "VISUAL" Then Exit Sub
            .Col = ColObservation
            If Trim(.Text) = "" Then Exit Sub

            If .Col = ColObservation Then
                Call SetObsCol(eventArgs.row, eventArgs.col, True)
            End If
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SetObsCol(ByRef Row As Integer, ByRef Col As Integer, ByRef Ask As Boolean)

        Dim xParamDesc As String
        Dim xSpecification As Double
        Dim xSpecPlus As Double
        Dim xSpecMinus As Double
        Dim xWearLimit As Double
        Dim xObservation As Double
        Dim xMinSpec As Double
        Dim xMaxspec As Double
        Dim xMinWear As Double
        Dim xMaxWear As Double
        Dim xMinBlack As Double
        Dim xMaxBlack As Double
        Dim xWearBlack As Double
        Dim xMinAsk As Double
        Dim xMaxAsk As Double
        Dim xColorBlue As String
        Dim xColorRed As String
        Dim xResponse As String
        Dim A As Double
        Dim B As Double

        xColorBlue = CStr(&HFF0000)
        xColorRed = CStr(&HFF)

        With SprdMain
            .Col = ColParamDesc
            xParamDesc = Trim(.Text)
            .Col = ColSpecification
            xSpecification = Val(.Text)
            .Col = ColSpecPlus
            xSpecPlus = Val(.Text)
            .Col = ColSpecMinus
            xSpecMinus = Val(.Text)
            .Col = ColWearLimit
            xWearLimit = Val(.Text)
            .Col = ColObservation
            xObservation = Val(.Text)
            If InStr(1, xParamDesc, "ANGLE") > 0 Then
                A = CDbl(VB6.Format(xSpecification, CStr(0)))
                B = xSpecification - A
                xSpecification = A + B * 10 / 6
                A = CDbl(VB6.Format(xSpecPlus, CStr(0)))
                B = xSpecPlus - A
                xSpecPlus = A + B * 10 / 6
                A = CDbl(VB6.Format(xSpecMinus, CStr(0)))
                B = xSpecMinus - A
                xSpecMinus = A + B * 10 / 6
                A = CDbl(VB6.Format(xWearLimit, CStr(0)))
                B = xWearLimit - A
                xWearLimit = A + B * 10 / 6
                A = CDbl(VB6.Format(xObservation, CStr(0)))
                B = xObservation - A
                xObservation = A + B * 10 / 6
            End If
            xMinSpec = xSpecification + xSpecMinus
            xMaxspec = xSpecification + xSpecPlus

            If xObservation = xSpecification Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(xColorOrig))
            Else
                If (xObservation >= xMinSpec And xObservation <= xMaxspec) Then
                    .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(xColorOrig))
                Else
                    If xWearLimit = 0 Then
                        xMinWear = xMinSpec
                        xMaxWear = xMaxspec
                    ElseIf (xWearLimit >= xMinSpec And xWearLimit <= xMaxspec) Then
                        xMinWear = xMinSpec
                        xMaxWear = xMaxspec
                    ElseIf (xWearLimit < xMinSpec) Then
                        xMinWear = xWearLimit
                        xMaxWear = xMaxspec
                    ElseIf (xWearLimit > xMinSpec) Then
                        xMinWear = xMinSpec
                        xMaxWear = xWearLimit
                    End If
                    If (xObservation >= xMinWear And xObservation <= xMaxWear) Then
                        xWearBlack = xWearLimit + ((xSpecification - xWearLimit) * 0.2)
                        If xSpecification <= xWearBlack Then
                            xMinBlack = xSpecification
                            xMaxBlack = xWearBlack
                        ElseIf xSpecification > xWearBlack Then
                            xMinBlack = xWearBlack
                            xMaxBlack = xSpecification
                        End If
                        If (xObservation >= xMinBlack And xObservation <= xMaxBlack) Then
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(xColorOrig))
                        Else
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(xColorBlue))
                        End If
                    Else
                        xMinAsk = xMinWear + (xSpecMinus * 10)
                        xMaxAsk = xMaxWear + (xSpecPlus * 10)
                        If (xObservation >= xMinAsk And xObservation <= xMaxAsk) Then
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(xColorRed))
                        Else
                            If Ask = True Then
                                xResponse = MsgQuestion("Observation Values seems to be Wrong. Do you want to accept it?")
                                If xResponse = CStr(MsgBoxResult.Yes) Then
                                    .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(xColorRed))
                                    MainClass.SetFocusToCell(SprdMain, Row + 1, ColObservation)
                                Else
                                    MainClass.SetFocusToCell(SprdMain, Row, ColObservation)
                                End If
                            Else
                                .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(xColorRed))
                            End If
                        End If
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        txtSlipNo.Text = SprdView.Text

        txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Function ValidateEMP(ByRef pTextBox As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label) As Boolean
        On Error GoTo ValEMP
        Dim SqlStr As String
        ValidateEMP = True
        If Trim(pTextBox.Text) = "" Then Exit Function
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(pTextBox.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Employee Does Not Exist In Master.")
            ValidateEMP = False
        Else
            pLable.text = MasterNo
        End If
        Exit Function
ValEMP:
        MsgBox(Err.Description)
    End Function

    Private Sub txtAppBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAppBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppBy.DoubleClick
        Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtAppBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAppBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtAppBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAppBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtAppBy, lblAppBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTypeNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTypeNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTypeNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTypeNo.DoubleClick
        Call cmdSearchTypeNo_Click(cmdSearchTypeNo, New System.EventArgs())
    End Sub

    Private Sub txtTypeNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtTypeNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchTypeNo_Click(cmdSearchTypeNo, New System.EventArgs())
    End Sub

    Public Sub txtTypeNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTypeNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtTypeNo.Text) = "" Then GoTo EventExitSub

        SqlStr = "SELECT DISTINCT B.TYPENO, A.DOCNO " & vbCrLf _
                    & " FROM QAL_GAUGE_CALIB_STD A, QAL_GAUGEFIX_MST B " & vbCrLf _
                    & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _
                    & " AND LTRIM(RTRIM(A.DOCNO)) = LTRIM(RTRIM(B.DOCNO)) " & vbCrLf _
                    & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND LTRIM(RTRIM(B.TYPENO)) ='" & MainClass.AllowSingleQuote(txtTypeNo.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                lblDocNo.Text = IIf(IsDbNull(mRsTemp.Fields("DOCNO").Value), "", .Fields("DOCNO").Value)
                ShowGauge()
                FillStd()
            Else
                MsgBox("Not a valid Type No.")
                lblDocNo.Text = ""
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInspBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInspBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspBy.DoubleClick
        Call cmdSearchInspBy_Click(cmdSearchInspBy, New System.EventArgs())
    End Sub

    Private Sub txtInspBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtInspBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchInspBy_Click(cmdSearchInspBy, New System.EventArgs())
    End Sub

    Private Sub txtInspBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInspBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtInspBy, lblInspBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
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
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart

        If Not RsGaugeCalibHdr.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsGaugeCalibHdr.Fields("AUTO_KEY_CALIB").Value), "", RsGaugeCalibHdr.Fields("AUTO_KEY_CALIB").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsGaugeCalibHdr.Fields("AUTO_KEY_CALIB").Value), "", RsGaugeCalibHdr.Fields("AUTO_KEY_CALIB").Value)
            txtDate.Text = IIf(IsDbNull(RsGaugeCalibHdr.Fields("CALIB_DATE").Value), "", RsGaugeCalibHdr.Fields("CALIB_DATE").Value)
            lblDocNo.Text = IIf(IsDbNull(RsGaugeCalibHdr.Fields("DOCNO").Value), "", RsGaugeCalibHdr.Fields("DOCNO").Value)
            ShowGauge()
            txtRemarks.Text = IIf(IsDbNull(RsGaugeCalibHdr.Fields("REMARKS").Value), "", RsGaugeCalibHdr.Fields("REMARKS").Value)
            txtInspBy.Text = IIf(IsDbNull(RsGaugeCalibHdr.Fields("INSPECTED_BY").Value), "", RsGaugeCalibHdr.Fields("INSPECTED_BY").Value)
            txtInspBy_Validating(txtInspBy, New System.ComponentModel.CancelEventArgs(False))
            txtAppBy.Text = IIf(IsDbNull(RsGaugeCalibHdr.Fields("APPROVED_BY").Value), "", RsGaugeCalibHdr.Fields("APPROVED_BY").Value)
            txtAppBy_Validating(txtAppBy, New System.ComponentModel.CancelEventArgs(False))
            Call ShowDetail1()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsGaugeCalibHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub ShowGauge()

        On Error GoTo ShowErrPart
        Dim RsGaugeFix As ADODB.Recordset
        Dim SqlStr As String

        If Trim(lblDocNo.Text) = "" Then Exit Sub
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_GAUGEFIX_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO=" & Val(lblDocNo.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGaugeFix, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsGaugeFix.EOF Then
            txtTypeNo.Text = IIf(IsDbNull(RsGaugeFix.Fields("TypeNo").Value), "", RsGaugeFix.Fields("TypeNo").Value)
            lblDescription.Text = IIf(IsDbNull(RsGaugeFix.Fields("Description").Value), "", RsGaugeFix.Fields("Description").Value)
            lblCustomer.Text = IIf(IsDbNull(RsGaugeFix.Fields("Customer").Value), "", RsGaugeFix.Fields("Customer").Value)
            lblLocation.Text = IIf(IsDbNull(RsGaugeFix.Fields("Location").Value), "", RsGaugeFix.Fields("Location").Value)
            lblModel.Text = IIf(IsDbNull(RsGaugeFix.Fields("MODEL").Value), "", RsGaugeFix.Fields("MODEL").Value)
            lblDRGNo.Text = IIf(IsDbNull(RsGaugeFix.Fields("DrgNo").Value), "", RsGaugeFix.Fields("VALFREQUENCY").Value)
            lblFrequency.Text = IIf(IsDbNull(RsGaugeFix.Fields("VALFREQUENCY").Value), "", RsGaugeFix.Fields("VALFREQUENCY").Value)
        Else
            MsgBox("Doc No Does Not Exist", MsgBoxStyle.Information)
        End If
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub FillStd()

        On Error GoTo FillERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mRsTemp As ADODB.Recordset
        If Trim(lblDocNo.Text) = "" Then Exit Sub
        SqlStr = "SELECT SERIAL_NO,PARAM_DESC, SPECIFICATION, SPEC_PLUS, SPEC_MINUS, WEAR_LIMIT, INSP_MTH " & vbCrLf & " From QAL_GAUGE_CALIB_STD " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO =" & Val(lblDocNo.Text) & " ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColParamDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdMain.Col = ColSpecification
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPECIFICATION").Value), "", .Fields("SPECIFICATION").Value))

                SprdMain.Col = ColSpecPlus
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPEC_PLUS").Value), "", .Fields("SPEC_PLUS").Value))

                SprdMain.Col = ColSpecMinus
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPEC_MINUS").Value), "", .Fields("SPEC_MINUS").Value))

                SprdMain.Col = ColWearLimit
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("WEAR_LIMIT").Value), "", .Fields("WEAR_LIMIT").Value))

                SprdMain.Col = ColInspMth
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("INSP_MTH").Value), "", .Fields("INSP_MTH").Value))

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim mInspMth As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_GAUGE_CALIB_DET " & vbCrLf & " WHERE AUTO_KEY_CALIB=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGaugeCalibDet, ADODB.LockTypeEnum.adLockReadOnly)
        With RsGaugeCalibDet
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColParamDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdMain.Col = ColSpecification
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPECIFICATION").Value), "", .Fields("SPECIFICATION").Value))

                SprdMain.Col = ColSpecPlus
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPEC_PLUS").Value), "", .Fields("SPEC_PLUS").Value))

                SprdMain.Col = ColSpecMinus
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPEC_MINUS").Value), "", .Fields("SPEC_MINUS").Value))

                SprdMain.Col = ColWearLimit
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("WEAR_LIMIT").Value), "", .Fields("WEAR_LIMIT").Value))

                SprdMain.Col = ColInspMth
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("INSP_MTH").Value), "", .Fields("INSP_MTH").Value))
                mInspMth = SprdMain.Text

                SprdMain.Col = ColBeforeCorrection
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("BEFORE_OBSERVATION").Value), "", .Fields("BEFORE_OBSERVATION").Value))

                SprdMain.Col = ColObservation
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERVATION").Value), "", .Fields("OBSERVATION").Value))
                If mInspMth <> "VISUAL" Then
                    Call SetObsCol((SprdMain.Row), (SprdMain.Col), False)
                End If

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColParamDesc, ColInspMth)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtSlipNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.DoubleClick
        Call cmdSearchSlipNo_Click(cmdSearchSlipNo, New System.EventArgs())
    End Sub

    Private Sub txtSlipNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSlipNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchSlipNo_Click(cmdSearchSlipNo, New System.EventArgs())
    End Sub

    Public Sub txtSlipNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSlipNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String

        If Trim(txtSlipNo.Text) = "" Then GoTo EventExitSub


        If Len(Trim(txtSlipNo.Text)) < 6 Then
            txtSlipNo.Text = Trim(txtSlipNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mSlipNo = Val(txtSlipNo.Text)
        If MODIFYMode = True And RsGaugeCalibHdr.BOF = False Then xMKey = RsGaugeCalibHdr.Fields("AUTO_KEY_CALIB").Value

        SqlStr = "SELECT * FROM QAL_GAUGE_CALIB_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_CALIB=" & mSlipNo & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGaugeCalibHdr, ADODB.LockTypeEnum.adLockReadOnly)
        If RsGaugeCalibHdr.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_GAUGE_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_CALIB=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGaugeCalibHdr, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtDate.Enabled = mMode
        txtTypeNo.Enabled = mMode
        cmdSearchTypeNo.Enabled = mMode
        txtRemarks.Enabled = mMode
        txtInspBy.Enabled = mMode
        cmdSearchInspBy.Enabled = mMode
        txtAppBy.Enabled = mMode
        cmdSearchAppBy.Enabled = mMode
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

    Private Sub ReportOnCalibCertFix(ByRef Mode As Crystal.DestinationConstants)

    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnCalibCertFix(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnCalibCertFix(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
