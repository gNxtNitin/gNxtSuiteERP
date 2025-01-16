Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmIMTEInsp
    Inherits System.Windows.Forms.Form
    Dim RsIMTECalibHdr As ADODB.Recordset
    Dim RsIMTECalibDet As ADODB.Recordset
    Dim RsIMTECalibInst As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean
    Private Const ConRowHeight As Short = 14

    Dim xColorOrig As String
    Dim xMyMenu As String

    Private Const ColParamDesc As Short = 1
    Private Const ColReadingStep As Short = 2
    Private Const ColPerError As Short = 3
    Private Const ColObservation As Short = 4

    Private Const ColDocNo As Short = 1
    Private Const ColDescription As Short = 2
    Private Const ColModel As Short = 3
    Private Const ColMake As Short = 4
    Private Const ColCalibBy As Short = 5
    Private Const ColCertNo As Short = 6
    Private Const ColCalibValid As Short = 7

    Private Sub chkCalibOK_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCalibOK.CheckStateChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click

        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtSlipNo.Enabled = False
            cmdSearchSlipNo.Enabled = False
            MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols)
            MainClass.UnProtectCell(SprdInst, 1, SprdInst.MaxRows, 1, SprdInst.MaxCols)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColParamDesc, ColPerError)
            MainClass.ProtectCell(SprdInst, 1, SprdInst.MaxRows, ColDescription, ColCalibValid)
            '        SprdMain.Enabled = True
            '        SprdInst.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsIMTECalibHdr.EOF = False Then RsIMTECalibHdr.MoveFirst()
            Clear1()
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
        Dim SqlStr As String

        If txtSlipNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsIMTECalibHdr.EOF Then
            If PubSuperUser = "U" Then
                If RsIMTECalibHdr.Fields("APPROVED_BY").Value <> "" Then MsgBox("Number been approved, So cann't be Deleted ") : Exit Sub
            End If
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_IMTE_CALIB_HDR", (txtSlipNo.Text), RsIMTECalibHdr) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_IMTE_CALIB_INST WHERE AUTO_KEY_CALIB=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_IMTE_CALIB_DET WHERE AUTO_KEY_CALIB=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_IMTE_CALIB_HDR WHERE AUTO_KEY_CALIB=" & Val(lblMkey.Text) & "")

                SqlStr = " UPDATE QAL_IMTE_SCHD_DET SET " & vbCrLf & " PM_DONE='' " & vbCrLf & " WHERE DOCNO ='" & MainClass.AllowSingleQuote(txtDocNo.Text) & "' " & vbCrLf & " AND CHECK_TYPE ='PM' " & vbCrLf & " AND AUTO_KEY_SCHD=" & vbCrLf & " (SELECT AUTO_KEY_SCHD FROM QAL_IMTE_SCHD_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DOC_TYPE='I'" & vbCrLf & " AND SCHD_MONTH=" & Val(VB6.Format(txtDate.Text, "MM")) & " " & vbCrLf & " AND SCHD_YEAR=" & Val(VB6.Format(txtDate.Text, "YYYY")) & ") "

                PubDBCn.Execute(SqlStr)

                PubDBCn.CommitTrans()
                RsIMTECalibHdr.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsIMTECalibHdr.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            If PubSuperUser = "U" Then
                If RsIMTECalibHdr.Fields("APPROVED_BY").Value <> "" Then MsgBox("Number been approved, So cann't be Modified ") : Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsIMTECalibHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtSlipNo.Enabled = False
            cmdSearchSlipNo.Enabled = False
            Call MakeEnableDesableField(True)
            MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols)
            MainClass.UnProtectCell(SprdInst, 1, SprdInst.MaxRows, 1, SprdInst.MaxCols)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColParamDesc, ColPerError)
            If lblCaliFacil.Text <> "OUTSIDE" Then
                MainClass.ProtectCell(SprdInst, 1, SprdInst.MaxRows, ColDescription, ColCalibValid)
            End If
            '        SprdMain.Enabled = True
            '        SprdInst.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
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
        SqlStr = " SELECT AUTO_KEY_CALIB " & vbCrLf & " From QAL_IMTE_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND CALIB_DATE =TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND DOCNO = " & Val(txtDocNo.Text) & " "
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
        Dim mCalibOK As String
        Dim mNABLLogo As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        mSlipNo = Val(txtSlipNo.Text)
        If Val(txtSlipNo.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtSlipNo.Text = CStr(mSlipNo)

        If ADDMode = True And RsCompany.Fields("COMPANY_CODE").Value = 1 Then
            txtCretificateNo.Text = AutoCretificationNo()
        End If

        If Trim(txtCretificateNo.Text) = "" Then
            txtCretificateNo.Text = CStr(mSlipNo)
        End If

        mNABLLogo = IIf(chkNABLLogo.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mCalibOK = IIf(chkCalibOK.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_IMTE_CALIB_HDR " & vbCrLf _
                            & " (AUTO_KEY_CALIB,COMPANY_CODE," & vbCrLf _
                            & " CALIB_DATE,RECEIPT_DATE,DOCNO," & vbCrLf _
                            & " AMB_TEMP,HUMIDITY,SOAKING_TIME,CALIB_OK,NABL_LOGO," & vbCrLf _
                            & " CALIB_PROC,VISUAL_INSP,ZERO_ERROR,UNCERTAINTY," & vbCrLf _
                            & " REMARKS,INSPECTED_BY,INSPECTED_NAME,APPROVED_BY, " & vbCrLf _
                            & " ACTUAL_GOSIZE,ACTUAL_NOGOSIZE, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE,CALIB_CERT_NO,CERT_ISSUE_DATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtReceiptDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " " & Val(txtDocNo.Text) & ", " & vbCrLf _
                            & " " & Val(txtAmbTemp.Text) & ", " & Val(txtHumidity.Text) & ", " & Val(txtSoakingTime.Text) & ",'" & mCalibOK & "','" & mNABLLogo & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCalibProc.Text) & "','" & MainClass.AllowSingleQuote(txtVisualInsp.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtZeroError.Text) & "','" & MainClass.AllowSingleQuote(txtUncertainty.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','" & MainClass.AllowSingleQuote(txtInspBy.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtInspName.Text) & "','" & MainClass.AllowSingleQuote(txtAppBy.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtActualGoSize.Text) & "','" & MainClass.AllowSingleQuote(txtActualNoGoSize.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'',''," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCretificateNo.Text) & "', TO_DATE('" & vb6.Format(txtCretificateIssueDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_IMTE_CALIB_HDR SET " & vbCrLf _
                    & " AUTO_KEY_CALIB=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " CALIB_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " RECEIPT_DATE=TO_DATE('" & vb6.Format(txtReceiptDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " DOCNO=" & Val(txtDocNo.Text) & ", " & vbCrLf _
                    & " AMB_TEMP=" & Val(txtAmbTemp.Text) & ", " & vbCrLf _
                    & " HUMIDITY=" & Val(txtHumidity.Text) & ", " & vbCrLf _
                    & " SOAKING_TIME=" & Val(txtSoakingTime.Text) & ", " & vbCrLf _
                    & " CALIB_OK='" & mCalibOK & "', " & vbCrLf _
                    & " NABL_LOGO='" & mNABLLogo & "', " & vbCrLf _
                    & " CALIB_CERT_NO='" & MainClass.AllowSingleQuote(txtCretificateNo.Text) & "'," & vbCrLf _
                    & " CERT_ISSUE_DATE=TO_DATE('" & vb6.Format(txtCretificateIssueDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " CALIB_PROC='" & MainClass.AllowSingleQuote(txtCalibProc.Text) & "', " & vbCrLf _
                    & " VISUAL_INSP='" & MainClass.AllowSingleQuote(txtVisualInsp.Text) & "', " & vbCrLf _
                    & " ZERO_ERROR='" & MainClass.AllowSingleQuote(txtZeroError.Text) & "', " & vbCrLf _
                    & " UNCERTAINTY='" & MainClass.AllowSingleQuote(txtUncertainty.Text) & "', " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " INSPECTED_BY='" & MainClass.AllowSingleQuote(txtInspBy.Text) & "', " & vbCrLf _
                    & " INSPECTED_NAME='" & MainClass.AllowSingleQuote(txtInspName.Text) & "', " & vbCrLf _
                    & " APPROVED_BY='" & MainClass.AllowSingleQuote(txtAppBy.Text) & "', " & vbCrLf _
                    & " ACTUAL_GOSIZE='" & MainClass.AllowSingleQuote(txtActualGoSize.Text) & "', " & vbCrLf _
                    & " ACTUAL_NOGOSIZE='" & MainClass.AllowSingleQuote(txtActualNoGoSize.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_CALIB =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail = False Then GoTo ErrPart
        If UpdateInst = False Then GoTo ErrPart

        SqlStr = ""
        SqlStr = " SELECT AUTO_KEY_CALIB " & vbCrLf & " FROM QAL_IMTE_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND DOCNO=" & Val(txtDocNo.Text) & " " & vbCrLf & " AND CALIB_DATE=" & vbCrLf & " (SELECT Max(CALIB_DATE) " & vbCrLf & " FROM QAL_IMTE_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND DOCNO=" & Val(txtDocNo.Text) & ")"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not mRsTemp.EOF Then
            If mRsTemp.Fields("AUTO_KEY_CALIB").Value = Val(lblMkey.Text) Then
                SqlStr = ""
                SqlStr = " UPDATE QAL_IMTE_MST SET " & vbCrLf & " LCDATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " CDATE=TO_DATE('" & VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Month, Val(lblFrequency.Text), CDate(txtDate.Text)), "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " CALIB_OK='" & mCalibOK & "', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO =" & Val(txtDocNo.Text) & ""

                PubDBCn.Execute(SqlStr)

                SqlStr = " UPDATE QAL_IMTE_SCHD_DET SET " & vbCrLf & " PM_DONE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE DOCNO ='" & MainClass.AllowSingleQuote(Val(txtDocNo.Text)) & "' " & vbCrLf & " AND CHECK_TYPE ='PM' " & vbCrLf & " AND AUTO_KEY_SCHD=" & vbCrLf & " (SELECT AUTO_KEY_SCHD FROM QAL_IMTE_SCHD_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DOC_TYPE='I'" & vbCrLf & " AND SCHD_MONTH=" & Val(VB6.Format(txtDate.Text, "MM")) & " " & vbCrLf & " AND SCHD_YEAR=" & Val(VB6.Format(txtDate.Text, "YYYY")) & ") "

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
        RsIMTECalibHdr.Requery()
        RsIMTECalibDet.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_CALIB)  " & vbCrLf & " FROM QAL_IMTE_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & " "

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

    Private Function AutoCretificationNo() As String

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mCalYear As String
        Dim mNablNo As String
        Dim pCheckDate As String

        mAutoGen = 1

        mNablNo = "CC2063"
        pCheckDate = "01/09/2018"
        mCalYear = VB6.Format(txtDate.Text, "YY")

        If CDate(txtDate.Text) < CDate(pCheckDate) Then
            AutoCretificationNo = ""
            Exit Function
        End If

        SqlStr = ""
        SqlStr = "SELECT Max(SUBSTR(CALIB_CERT_NO,10,8))  " & vbCrLf & " FROM QAL_IMTE_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(CALIB_CERT_NO,1,6)='" & mNablNo & "' " & vbCrLf & " AND SUBSTR(CALIB_CERT_NO,7,2)='" & mCalYear & "'"

        If chkNABLLogo.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(CALIB_CERT_NO,LENGTH(CALIB_CERT_NO),1)='F'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(CALIB_CERT_NO,LENGTH(CALIB_CERT_NO),1)='P'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND CALIB_DATE >= '" & VB6.Format(pCheckDate, "DD-MMM-YYYY") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDbNull(.Fields(0).Value) Then
                    mAutoGen = Val(.Fields(0).Value)
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With

        AutoCretificationNo = mNablNo & mCalYear & "0" & VB6.Format(mAutoGen, "00000000") & IIf(chkNABLLogo.CheckState = System.Windows.Forms.CheckState.Checked, "F", "P")

        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Function UpdateDetail() As Boolean

        On Error GoTo UpdateDetailERR
        Dim SqlStr As String
        Dim i As Integer
        Dim mParamDesc As String
        Dim mReadingStep As Double
        Dim mPerError As Double
        Dim mObservation As Double

        PubDBCn.Execute("DELETE FROM QAL_IMTE_CALIB_DET WHERE AUTO_KEY_CALIB=" & Val(lblMkey.Text) & "")

        With SprdMain
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColParamDesc
                mParamDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColReadingStep
                mReadingStep = Val(.Text)

                .Col = ColPerError
                mPerError = Val(.Text)

                .Col = ColObservation
                mObservation = Val(.Text)

                SqlStr = ""

                If mParamDesc <> "" Then
                    SqlStr = " INSERT INTO  QAL_IMTE_CALIB_DET ( " & vbCrLf & " AUTO_KEY_CALIB,SERIAL_NO,PARAM_DESC, " & vbCrLf & " READING_STEP,PER_ERROR,OBSERVATION ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & i & ",'" & mParamDesc & "'," & mReadingStep & "," & vbCrLf & " '" & mPerError & "'," & mObservation & " ) "
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

    Private Function UpdateInst() As Boolean

        On Error GoTo UpdateInstERR
        Dim SqlStr As String
        Dim i As Integer
        Dim mDocNo As Integer
        Dim mDescription As String
        Dim mModel As String
        Dim mMake As String
        Dim mCalibBy As String
        Dim mCertNo As String
        Dim mCalibValid As String

        PubDBCn.Execute("DELETE FROM QAL_IMTE_CALIB_INST WHERE AUTO_KEY_CALIB=" & Val(lblMkey.Text) & "")

        With SprdInst
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColDocNo
                mDocNo = Val(.Text)

                .Col = ColDescription
                mDescription = MainClass.AllowSingleQuote(.Text)

                .Col = ColModel
                mModel = MainClass.AllowSingleQuote(.Text)

                .Col = ColMake
                mMake = MainClass.AllowSingleQuote(.Text)

                .Col = ColCalibBy
                mCalibBy = MainClass.AllowSingleQuote(.Text)

                .Col = ColCertNo
                mCertNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColCalibValid
                mCalibValid = VB6.Format(.Text, "DD/MM/YYYY")

                SqlStr = ""

                If mDocNo <> 0 Then
                    SqlStr = " INSERT INTO  QAL_IMTE_CALIB_INST ( " & vbCrLf & " AUTO_KEY_CALIB,SERIALNO,DOCNO,DESCRIPTION,MODEL, " & vbCrLf & " MAKE_NAME,CALIB_BY,CERT_NO,CALIB_VALID ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & i & "," & mDocNo & ",'" & mDescription & "','" & mModel & "'," & vbCrLf & " '" & mMake & "','" & mCalibBy & "','" & mCertNo & "',TO_DATE('" & VB6.Format(mCalibValid, "DD-MMMM-YYYY") & "','DD-MON-YYYY')) "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateInst = True
        Exit Function
UpdateInstERR:
        UpdateInst = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub cmdSearchAppBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAppBy.Click
        Call SearchEmp(txtAppBy, txtAppName)
    End Sub

    Private Sub cmdSearchInspBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchInspBy.Click
        Call SearchEmp(txtInspBy, txtInspName)
    End Sub

    Private Sub SearchEmp(ByRef pCode As System.Windows.Forms.TextBox, ByRef pName As System.Windows.Forms.TextBox)
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            pCode.Text = AcName1
            pName.Text = AcName
            If pCode.Enabled = True Then pCode.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchDocNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDocNo.Click
        Dim SqlStr As String
        SqlStr = " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster("", "QAL_IMTE_MST", "DOCNO", "DESCRIPTION", "E_NO", "L_C", SqlStr) = True Then
            txtDocNo.Text = AcName
            If txtDocNo.Enabled = True Then txtDocNo.Focus()
        End If
    End Sub

    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_IMTE_CALIB_HDR", "AUTO_KEY_CALIB", "CALIB_DATE", "DOCNO", "", SqlStr) = True Then
            txtSlipNo.Text = AcName
            Call txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
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
        MainClass.ButtonStatus(Me, XRIGHT, RsIMTECalibHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub frmIMTEInsp_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "IMTE Inspection (Calibration)"

        SqlStr = "Select * From QAL_IMTE_CALIB_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTECalibHdr, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_IMTE_CALIB_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTECalibDet, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_IMTE_CALIB_INST WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTECalibInst, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_CALIB AS SLIP_NUMBER,TO_CHAR(CALIB_DATE,'DD/MM/YYYY') AS CALIB_DATE, " & vbCrLf & " DOCNO,REMARKS,INSPECTED_BY,APPROVED_BY " & vbCrLf & " FROM QAL_IMTE_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_CALIB"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmIMTEInsp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmIMTEInsp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        xMyMenu = myMenu
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(8010)
        'Me.Width = VB6.TwipsToPixelsX(11595)
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
        txtCretificateNo.Text = ""
        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtCretificateIssueDate.Text = ""
        txtReceiptDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDocNo.Text = ""
        lblType.Text = ""
        lblCaliFacil.Text = ""
        lblDescription.Text = ""
        lblENo.Text = ""
        lblLC.Text = ""
        lblMakersNo.Text = ""
        lblMake.Text = ""
        lblLocation.Text = ""
        lblRange.Text = ""
        lblFrequency.Text = ""
        lblMinRange.Text = ""
        lblMaxRange.Text = ""
        lblUnitRange.Text = ""
        lblGoSize.Text = ""
        lblNoGoSize.Text = ""
        lblBasicSize.Text = ""
        lblWearSize.Text = ""
        txtAmbTemp.Text = ""
        txtHumidity.Text = ""
        txtSoakingTime.Text = ""
        txtCalibProc.Text = ""
        txtVisualInsp.Text = ""
        txtZeroError.Text = ""
        txtUncertainty.Text = ""
        txtRemarks.Text = ""
        txtInspBy.Text = ""
        txtInspName.Text = ""
        txtAppBy.Text = ""
        txtAppName.Text = ""
        txtActualGoSize.Text = ""
        txtActualNoGoSize.Text = ""
        chkNABLLogo.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCalibOK.CheckState = System.Windows.Forms.CheckState.Checked

        txtInspBy.Enabled = True
        cmdSearchInspBy.Enabled = True
        txtInspName.Enabled = False

        fraGoNoGo.Visible = False
        fraRange.Visible = True
        fraActualSize.Enabled = False
        fraActualSize.Visible = False
        fraCalibResult.Enabled = True
        fraCalibResult.Visible = True

        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        MainClass.ClearGrid(SprdInst, ConRowHeight)
        FormatSprdMain(-1)
        FormatSprdInst(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsIMTECalibHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            .TypeEditLen = RsIMTECalibDet.Fields("PARAM_DESC").DefinedSize

            .Col = ColReadingStep
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.9999")
            .TypeFloatMin = CDbl("-99999.9999")
            .TypeEditLen = RsIMTECalibDet.Fields("READING_STEP").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 4

            .Col = ColPerError
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.99999")
            .TypeFloatMin = CDbl("-99999.99999")
            .TypeEditLen = RsIMTECalibDet.Fields("PER_ERROR").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 5

            .Col = ColObservation
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.99999")
            .TypeFloatMin = CDbl("-99999.99999")
            .TypeEditLen = RsIMTECalibDet.Fields("OBSERVATION").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 5

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColParamDesc, ColPerError)
            MainClass.SetSpreadColor(SprdMain, Arow)
            .Col = ColObservation
            xColorOrig = System.Drawing.ColorTranslator.ToOle(.ForeColor).ToString
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub FormatSprdInst(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String

        With SprdInst
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColDocNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeEditLen = RsIMTECalibInst.Fields("DOCNO").Precision
            .ColsFrozen = ColDocNo

            .Col = ColDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsIMTECalibInst.Fields("DESCRIPTION").DefinedSize

            .Col = ColModel
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsIMTECalibInst.Fields("MODEL").DefinedSize

            .Col = ColMake
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsIMTECalibInst.Fields("MAKE_NAME").DefinedSize

            .Col = ColCalibBy
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsIMTECalibInst.Fields("CALIB_BY").DefinedSize

            .Col = ColCertNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsIMTECalibInst.Fields("CERT_NO").DefinedSize

            .Col = ColCalibValid
            .CellType = SS_CELL_TYPE_DATE
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY

            MainClass.ProtectCell(SprdInst, 1, SprdMain.MaxRows, ColDescription, ColCalibValid)
            MainClass.SetSpreadColor(SprdInst, Arow)
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

        txtSlipNo.Maxlength = RsIMTECalibHdr.Fields("AUTO_KEY_CALIB").Precision
        txtDate.Maxlength = RsIMTECalibHdr.Fields("CALIB_DATE").DefinedSize - 6
        txtCretificateIssueDate.Maxlength = RsIMTECalibHdr.Fields("CERT_ISSUE_DATE").DefinedSize - 6
        txtReceiptDate.Maxlength = RsIMTECalibHdr.Fields("RECEIPT_DATE").DefinedSize - 6
        txtDocNo.Maxlength = RsIMTECalibHdr.Fields("DOCNO").Precision
        txtAmbTemp.Maxlength = RsIMTECalibHdr.Fields("AMB_TEMP").Precision
        txtHumidity.Maxlength = RsIMTECalibHdr.Fields("HUMIDITY").Precision
        txtSoakingTime.Maxlength = RsIMTECalibHdr.Fields("SOAKING_TIME").Precision
        txtCalibProc.Maxlength = RsIMTECalibHdr.Fields("CALIB_PROC").Precision
        txtVisualInsp.Maxlength = RsIMTECalibHdr.Fields("VISUAL_INSP").Precision
        txtZeroError.Maxlength = RsIMTECalibHdr.Fields("ZERO_ERROR").Precision
        txtUncertainty.Maxlength = RsIMTECalibHdr.Fields("UNCERTAINTY").Precision
        txtRemarks.Maxlength = RsIMTECalibHdr.Fields("REMARKS").DefinedSize
        txtInspBy.Maxlength = RsIMTECalibHdr.Fields("INSPECTED_BY").DefinedSize
        txtInspName.Maxlength = RsIMTECalibHdr.Fields("INSPECTED_NAME").DefinedSize
        txtAppBy.Maxlength = RsIMTECalibHdr.Fields("APPROVED_BY").DefinedSize
        txtAppName.Maxlength = 255
        txtActualGoSize.Maxlength = RsIMTECalibHdr.Fields("ACTUAL_GOSIZE").DefinedSize
        txtActualNoGoSize.Maxlength = RsIMTECalibHdr.Fields("ACTUAL_NOGOSIZE").DefinedSize

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

        If MODIFYMode = True And RsIMTECalibHdr.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Calibration Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtCretificateIssueDate.Text) <> "" Then
            If Not IsDate(txtCretificateIssueDate.Text) Then
                MsgInformation("Invalid Calibration Date, So unable to save.")
                txtCretificateIssueDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtReceiptDate.Text) = "" Then
            MsgInformation("Receipt Date is empty, So unable to save.")
            txtReceiptDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDocNo.Text) = "" Then
            MsgInformation("Doc No. empty, So unable to save.")
            txtDocNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If lblCaliFacil.Text = "OUTSIDE" Then
            If Trim(txtInspName.Text) = "" Then
                MsgInformation("Inspected By is empty, So unable to save.")
                txtInspName.Focus()
                FieldsVarification = False
                Exit Function
            End If
        Else
            If Trim(txtInspBy.Text) = "" Then
                MsgInformation("Inspection Employee Code is empty, So unable to save.")
                txtInspBy.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MainClass.ValidateWithMasterTable(txtDocNo.Text, "DOCNO", "DOCNO", "QAL_IMTE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND STATUS='O'") = True Then
            If ADDMode = True Then
                If CheckGauge_IMTEPMSchd(txtDocNo.Text, txtDate.Text, "I", "PM") = False Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        '    If MainClass.ValidDataInGrid(SprdMain, ColParamDesc, "S", "Please Check Parameter.") = False Then FieldsVarification = False: Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColObservation, "N", "Please Check Observation.") = False Then FieldsVarification = False: Exit Function
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmIMTEInsp_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        RsIMTECalibHdr.Close()
        RsIMTECalibHdr = Nothing
        RsIMTECalibDet.Close()
        RsIMTECalibDet = Nothing
        RsIMTECalibInst.Close()
        RsIMTECalibInst = Nothing
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
        If eventArgs.NewRow = -1 Then Exit Sub

        With SprdMain
            .Row = .ActiveRow
            .Col = ColParamDesc
            xParamDesc = Trim(.Text)
            If xParamDesc = "" Then Exit Sub
            .Col = ColObservation
            If Trim(.Text) = "" Then Exit Sub

            If .Col = ColObservation Then
                Call SetObsCol(.Row, .Col, True)
            End If
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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

    Private Sub SprdInst_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdInst.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdInst_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdInst.ClickEvent

        On Error GoTo ErrPart
        Dim SqlStr As String

        If ADDMode = False And MODIFYMode = False Then Exit Sub

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)

        With SprdInst
            If eventArgs.Row = 0 And eventArgs.Col = ColDocNo And lblCaliFacil.Text <> "OUTSIDE" Then
                .Row = .ActiveRow
                .Col = ColDocNo
                If MainClass.SearchGridMaster(.Text, "QAL_IMTE_MST", "DOCNO", "DESCRIPTION", "L_C", "RANGE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MASTER_INST='Y' ") = True Then
                    .Row = .ActiveRow
                    .Col = ColDocNo
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdInst, SprdInst.ActiveRow, ColDocNo)
            End If
            If eventArgs.Col = 0 And eventArgs.Row > 0 Then
                .Row = eventArgs.row
                .Col = ColDocNo
                If eventArgs.Row < .MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                    MainClass.DeleteSprdRow(SprdInst, eventArgs.Row, ColDocNo)
                    FormatSprdInst(eventArgs.Row)
                End If
            End If
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdInst_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdInst.KeyDownEvent

        Dim mActiveCol As Integer

        mActiveCol = SprdInst.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Or eventArgs.KeyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColDocNo Then
                SprdInst.Row = SprdInst.ActiveRow
                SprdInst.Col = ColDocNo
                If Val(SprdInst.Text) <> 0 Then
                    If SprdInst.MaxRows = SprdInst.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdInst, ColDocNo, ConRowHeight)
                    End If
                End If
            End If
        ElseIf eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Then
            If mActiveCol = ColDocNo Then SprdInst_ClickEvent(SprdInst, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDocNo, 0))
        End If
        eventArgs.KeyCode = 9999
    End Sub

    Private Sub SprdInst_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdInst.KeyUpEvent
        Dim mCol As Short
        mCol = SprdInst.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDocNo Then SprdInst_ClickEvent(SprdInst, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDocNo, 0))
        SprdInst.Refresh()
    End Sub

    Private Sub SprdInst_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdInst.LeaveCell
        On Error GoTo ErrPart
        Dim xDocNo As Integer
        If eventArgs.NewRow = -1 Then Exit Sub
        If lblCaliFacil.Text = "OUTSIDE" Then Exit Sub

        With SprdInst
            .Row = .ActiveRow
            If eventArgs.Col = ColDocNo Then
                .Col = ColDocNo
                If Trim(.Text) <> "" Then
                    Call FillMasterInst(.Text)
                    If DuplicateDoc = False Then
                        FormatSprdInst(-1)
                    End If
                    .Row = .ActiveRow
                End If
            End If
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdInst_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdInst.Leave
        With SprdInst
            SprdInst_LeaveCell(SprdInst, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub FillMasterInst(ByRef pDocNo As String)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        If Trim(pDocNo) = "" Then Exit Sub
        With SprdInst
            SqlStr = "SELECT DESCRIPTION,E_NO,MAKE_NAME,CALIB_BY,CERT_NO,CALIB_VALID " & vbCrLf & " FROM QAL_IMTE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DOCNO=" & Val(pDocNo) & " AND MASTER_INST='Y' "
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                .Row = .ActiveRow
                .Col = ColDescription
                .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                .Col = ColModel
                .Text = IIf(IsDbNull(RsTemp.Fields("E_NO").Value), "", VB.Left(RsTemp.Fields("E_NO").Value, 20))

                .Col = ColMake
                .Text = IIf(IsDbNull(RsTemp.Fields("Make_Name").Value), "", RsTemp.Fields("Make_Name").Value)

                .Col = ColCalibBy
                .Text = IIf(IsDbNull(RsTemp.Fields("CALIB_BY").Value), "", RsTemp.Fields("CALIB_BY").Value)

                .Col = ColCertNo
                .Text = IIf(IsDbNull(RsTemp.Fields("CERT_NO").Value), "", RsTemp.Fields("CERT_NO").Value)

                .Col = ColCalibValid
                .Text = IIf(IsDbNull(RsTemp.Fields("Calib_Valid").Value), "", RsTemp.Fields("Calib_Valid").Value)
            Else
                MsgInformation("Invaild Doc No")
                MainClass.SetFocusToCell(SprdInst, .ActiveRow, ColDocNo)
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Function DuplicateDoc() As Boolean

        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckDocNo As Integer
        Dim mDocNo As Integer

        With SprdInst
            .Row = .ActiveRow
            .Col = ColDocNo

            mCheckDocNo = Val(.Text)
            mCount = 0

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDocNo
                mDocNo = Val(.Text)

                If (mDocNo = mCheckDocNo And mCheckDocNo <> 0) Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateDoc = True
                    MsgInformation("Duplicate Doc No : " & mCheckDocNo)
                    MainClass.SetFocusToCell(SprdInst, .ActiveRow, ColDocNo)
                    Exit Function
                End If
            Next
        End With
    End Function

    Private Sub SetObsCol(ByRef Row As Integer, ByRef Col As Integer, ByRef Ask As Boolean)

        Dim xPerError As Double
        Dim xObservation As Double
        Dim xReadingStep As Double
        Dim xMinPer As Double
        Dim xMaxPer As Double
        Dim xMinError As Double
        Dim xMaxError As Double
        Dim xMinAsk As Double
        Dim xMaxAsk As Double
        Dim xColorBlue As String
        Dim xColorRed As String
        Dim xResponse As String

        xColorBlue = CStr(&HFF0000)
        xColorRed = CStr(&HFF)

        With SprdMain
            .Col = ColPerError
            xPerError = Val(.Text)
            .Col = ColReadingStep
            xReadingStep = Val(.Text)
            xMinPer = xReadingStep - xPerError
            xMaxPer = xReadingStep + xPerError
            .Col = ColObservation
            xObservation = Val(.Text)

            If xObservation = 0 Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(xColorOrig))
            Else
                If (xObservation >= xMinPer And xObservation <= xMaxPer) Then
                    .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(xColorOrig))
                Else
                    xMinError = xReadingStep - xPerError * 2
                    xMaxError = xReadingStep + xPerError * 2
                    If (xObservation >= xMinError And xObservation <= xMaxError) Then
                        .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(xColorBlue))
                    Else
                        xMinAsk = xReadingStep - xPerError * 5
                        xMaxAsk = xReadingStep + xPerError * 5
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

    Private Function ValidateEMP(ByRef pCode As System.Windows.Forms.TextBox, ByRef pName As System.Windows.Forms.TextBox) As Boolean
        On Error GoTo ValEMP
        Dim SqlStr As String
        ValidateEMP = True
        If Trim(pCode.Text) = "" Then Exit Function
        pCode.Text = VB6.Format(pCode.Text, "000000")
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(pCode.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Employee Does Not Exist In Master.")
            ValidateEMP = False
        Else
            pName.Text = MasterNo
        End If
        Exit Function
ValEMP:
        MsgBox(Err.Description)
    End Function

    Private Sub txtAmbTemp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmbTemp.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAmbTemp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmbTemp.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAmbTemp_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAmbTemp.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtAmbTemp.Text) = "" Then GoTo EventExitSub
        txtAmbTemp.Text = VB6.Format(txtAmbTemp.Text, "0.00")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

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
        If ValidateEMP(txtAppBy, txtAppName) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCalibProc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCalibProc.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCretificateIssueDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCretificateIssueDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCretificateIssueDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCretificateIssueDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtCretificateIssueDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtCretificateIssueDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtCretificateNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCretificateNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtHumidity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHumidity.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtHumidity_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtHumidity.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtHumidity_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtHumidity.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtHumidity.Text) = "" Then GoTo EventExitSub
        txtHumidity.Text = VB6.Format(txtHumidity.Text, "0.00")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInspName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspName.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.ValidateWithMasterTable(txtDocNo.Text, "DOCNO", "TYPE", "QAL_IMTE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            ShowIMTE()
            If lblCalibOK.Text = "N" Then
                If MsgQuestion("Last Calibration of the Instrument was Not OK." & Chr(13) & "        Want to Enter Repair Details ? ") = CStr(MsgBoxResult.Yes) Then ' User choose Yes.
                    frmIMTERpr.MdiParent = Me.MdiParent
                    frmIMTERpr.frmIMTERpr_Activated(Nothing, New System.EventArgs())
                    frmIMTERpr.Show()
                    frmIMTERpr.txtDocNo.Text = txtDocNo.Text
                    frmIMTERpr.txtDocNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
                End If
                Cancel = True
            Else
                If lblType.Text = "VARIABLE" Then
                    SqlStr = "SELECT AUTO_KEY_PE,CALIB_PROC " & vbCrLf _
                                        & " FROM QAL_IMTE_PE_HDR " & vbCrLf _
                                        & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                                        & " AND DESCRIPTION ='" & MainClass.AllowSingleQuote(lblDescription.text) & "' " & vbCrLf _
                                        & " AND L_C ='" & MainClass.AllowSingleQuote(lblLC.text) & "' "

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If Not mRsTemp.EOF Then
                        fraGoNoGo.Visible = False
                        fraRange.Visible = True
                        fraActualSize.Enabled = False
                        fraActualSize.Visible = False
                        fraCalibResult.Enabled = True
                        fraCalibResult.Visible = True
                        txtCalibProc.Text = IIf(IsDbNull(mRsTemp.Fields("CALIB_PROC").Value), "", mRsTemp.Fields("CALIB_PROC").Value)
                        FillStd(IIf(IsDbNull(mRsTemp.Fields("AUTO_KEY_PE").Value), "", mRsTemp.Fields("AUTO_KEY_PE").Value))
                        If lblCaliFacil.Text = "OUTSIDE" Then MainClass.UnProtectCell(SprdInst, 1, SprdInst.MaxRows, 1, SprdInst.MaxCols)
                    Else
                        MsgBox("Permissible Errors not defined.")
                        Cancel = True
                    End If
                Else
                    fraRange.Visible = False
                    fraGoNoGo.Visible = True
                    fraCalibResult.Enabled = False
                    fraCalibResult.Visible = False
                    fraActualSize.Enabled = True
                    fraActualSize.Visible = True
                End If
            End If
        Else
            MsgBox("Not a valid Doc No.")
            Cancel = True
        End If
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
        If ValidateEMP(txtInspBy, txtInspName) = False Then Cancel = True
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

    Private Sub txtReceiptDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReceiptDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReceiptDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReceiptDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtReceiptDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtReceiptDate.Text) Then
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

        If Not RsIMTECalibHdr.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("AUTO_KEY_CALIB").Value), "", RsIMTECalibHdr.Fields("AUTO_KEY_CALIB").Value)
            txtCretificateNo.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("CALIB_CERT_NO").Value), "", RsIMTECalibHdr.Fields("CALIB_CERT_NO").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("AUTO_KEY_CALIB").Value), "", RsIMTECalibHdr.Fields("AUTO_KEY_CALIB").Value)
            txtDate.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("CALIB_DATE").Value), "", RsIMTECalibHdr.Fields("CALIB_DATE").Value)
            txtReceiptDate.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("RECEIPT_DATE").Value), "", RsIMTECalibHdr.Fields("RECEIPT_DATE").Value)
            txtDocNo.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("DOCNO").Value), "", RsIMTECalibHdr.Fields("DOCNO").Value)
            ShowIMTE()
            txtAmbTemp.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("AMB_TEMP").Value), "", VB6.Format(RsIMTECalibHdr.Fields("AMB_TEMP").Value, "0.00"))
            txtHumidity.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("HUMIDITY").Value), "", VB6.Format(RsIMTECalibHdr.Fields("HUMIDITY").Value, "0.00"))
            txtSoakingTime.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("SOAKING_TIME").Value), "", VB6.Format(RsIMTECalibHdr.Fields("SOAKING_TIME").Value, "0.00"))
            txtCalibProc.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("CALIB_PROC").Value), "", RsIMTECalibHdr.Fields("CALIB_PROC").Value)
            txtVisualInsp.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("VISUAL_INSP").Value), "", RsIMTECalibHdr.Fields("VISUAL_INSP").Value)
            txtZeroError.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("ZERO_ERROR").Value), "", RsIMTECalibHdr.Fields("ZERO_ERROR").Value)
            txtUncertainty.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("UNCERTAINTY").Value), "", RsIMTECalibHdr.Fields("UNCERTAINTY").Value)
            txtRemarks.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("REMARKS").Value), "", RsIMTECalibHdr.Fields("REMARKS").Value)
            txtInspBy.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("INSPECTED_BY").Value), "", RsIMTECalibHdr.Fields("INSPECTED_BY").Value)
            txtInspName.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("INSPECTED_NAME").Value), "", RsIMTECalibHdr.Fields("INSPECTED_NAME").Value)
            txtInspBy_Validating(txtInspBy, New System.ComponentModel.CancelEventArgs(False))
            txtAppBy.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("APPROVED_BY").Value), "", RsIMTECalibHdr.Fields("APPROVED_BY").Value)
            txtAppBy_Validating(txtAppBy, New System.ComponentModel.CancelEventArgs(False))
            txtActualGoSize.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("ACTUAL_GOSIZE").Value), "", RsIMTECalibHdr.Fields("ACTUAL_GOSIZE").Value)
            txtActualNoGoSize.Text = IIf(IsDbNull(RsIMTECalibHdr.Fields("ACTUAL_NOGOSIZE").Value), "", RsIMTECalibHdr.Fields("ACTUAL_NOGOSIZE").Value)

            txtCretificateIssueDate.Text = VB6.Format(IIf(IsDbNull(RsIMTECalibHdr.Fields("CERT_ISSUE_DATE").Value), "", RsIMTECalibHdr.Fields("CERT_ISSUE_DATE").Value), "DD/MM/YYYY")

            If RsIMTECalibHdr.Fields("NABL_LOGO").Value = "Y" Then
                chkNABLLogo.CheckState = System.Windows.Forms.CheckState.Checked
            Else
                chkNABLLogo.CheckState = System.Windows.Forms.CheckState.Unchecked
            End If

            If RsIMTECalibHdr.Fields("CALIB_OK").Value = "Y" Then
                chkCalibOK.CheckState = System.Windows.Forms.CheckState.Checked
            Else
                chkCalibOK.CheckState = System.Windows.Forms.CheckState.Unchecked
            End If

            If lblType.Text = "VARIABLE" Then
                fraGoNoGo.Visible = False
                fraRange.Visible = True
                fraActualSize.Enabled = False
                fraActualSize.Visible = False
                fraCalibResult.Enabled = True
                fraCalibResult.Visible = True
            Else
                fraRange.Visible = False
                fraGoNoGo.Visible = True
                fraCalibResult.Enabled = False
                fraCalibResult.Visible = False
                fraActualSize.Enabled = True
                fraActualSize.Visible = True
            End If

            Call MakeEnableDesableField(False)
            Call ShowDetail1()
            Call ShowInst1()
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols)
        MainClass.ProtectCell(SprdInst, 1, SprdInst.MaxRows, 1, SprdInst.MaxCols)
        '    SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        '    SprdInst.Enabled = False
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsIMTECalibHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub ShowIMTE()

        On Error GoTo ShowErrPart
        Dim RsIMTE As ADODB.Recordset
        Dim SqlStr As String

        If Trim(txtDocNo.Text) = "" Then Exit Sub
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_IMTE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO=" & Val(txtDocNo.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTE, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsIMTE.EOF Then
            lblCalibOK.Text = IIf(IsDbNull(RsIMTE.Fields("CALIB_OK").Value), "", RsIMTE.Fields("CALIB_OK").Value)
            lblType.Text = IIf(IsDbNull(RsIMTE.Fields("Type").Value), "", RsIMTE.Fields("Type").Value)
            lblCaliFacil.Text = IIf(IsDbNull(RsIMTE.Fields("CALIFACIL").Value), "", RsIMTE.Fields("CALIFACIL").Value)
            lblDescription.Text = IIf(IsDbNull(RsIMTE.Fields("Description").Value), "", RsIMTE.Fields("Description").Value)
            lblENo.Text = IIf(IsDbNull(RsIMTE.Fields("E_NO").Value), "", RsIMTE.Fields("E_NO").Value)
            lblLC.Text = IIf(IsDbNull(RsIMTE.Fields("L_C").Value), "", RsIMTE.Fields("L_C").Value)
            lblMakersNo.Text = IIf(IsDbNull(RsIMTE.Fields("Markers_No").Value), "", RsIMTE.Fields("Markers_No").Value)
            lblMake.Text = IIf(IsDbNull(RsIMTE.Fields("Make_Name").Value), "", RsIMTE.Fields("Make_Name").Value)
            lblLocation.Text = IIf(IsDbNull(RsIMTE.Fields("Location").Value), "", RsIMTE.Fields("Location").Value)
            lblRange.Text = IIf(IsDbNull(RsIMTE.Fields("Range").Value), "", RsIMTE.Fields("Range").Value)
            lblMinRange.Text = IIf(IsDbNull(RsIMTE.Fields("Min_Range").Value), "", RsIMTE.Fields("Min_Range").Value)
            lblMaxRange.Text = IIf(IsDbNull(RsIMTE.Fields("Max_Range").Value), "", RsIMTE.Fields("Max_Range").Value)
            lblUnitRange.Text = IIf(IsDbNull(RsIMTE.Fields("Unit_Range").Value), "", RsIMTE.Fields("Unit_Range").Value)
            lblGoSize.Text = IIf(IsDbNull(RsIMTE.Fields("GOSIZE").Value), "", RsIMTE.Fields("GOSIZE").Value)
            lblNoGoSize.Text = IIf(IsDbNull(RsIMTE.Fields("NOGOSIZE").Value), "", RsIMTE.Fields("NOGOSIZE").Value)
            lblBasicSize.Text = IIf(IsDbNull(RsIMTE.Fields("BASICSIZE").Value), "", RsIMTE.Fields("BASICSIZE").Value)
            lblWearSize.Text = IIf(IsDbNull(RsIMTE.Fields("WearSize").Value), "", RsIMTE.Fields("WearSize").Value)
            lblFrequency.Text = IIf(IsDbNull(RsIMTE.Fields("ValFrequency").Value), "", RsIMTE.Fields("ValFrequency").Value)

            If lblCaliFacil.Text = "OUTSIDE" Then
                txtInspBy.Enabled = False
                cmdSearchInspBy.Enabled = False
                txtInspName.Enabled = True
            Else
                txtInspBy.Enabled = True
                cmdSearchInspBy.Enabled = True
                txtInspName.Enabled = False
            End If

        Else
            MsgBox("Doc No Does Not Exist", MsgBoxStyle.Information)
        End If
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub FillStd(ByRef pPENo As Double)

        On Error GoTo FillERR
        Dim SqlStr As String
        Dim i As Integer
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtDocNo.Text) = "" Then Exit Sub
        SqlStr = "SELECT SERIAL_NO,PARAM_DESC, READING_STEP, PER_ERROR " & vbCrLf _
                        & " From QAL_IMTE_PE_STD A, QAL_IMTE_PE_HDR B " & vbCrLf _
                        & " WHERE A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                        & " AND A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _
                        & " AND A.AUTO_KEY_PE = B.AUTO_KEY_PE " & vbCrLf _
                        & " AND LTRIM(RTRIM(DESCRIPTION)) = '" & MainClass.AllowSingleQuote(lblDescription.text) & "' " & vbCrLf _
                        & " AND LTRIM(RTRIM(L_C)) = '" & MainClass.AllowSingleQuote(lblLC.text) & "' "


        If pPENo > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND A.AUTO_KEY_PE=" & pPENo & ""
        End If

        If lblMinRange.Text <> "" And lblMaxRange.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND READING_STEP BETWEEN " & Val(lblMinRange.Text) & " AND " & Val(lblMaxRange.Text) & " "
        ElseIf lblMinRange.Text <> "" And lblMaxRange.Text = "" Then
            SqlStr = SqlStr & vbCrLf & " AND READING_STEP >= " & Val(lblMinRange.Text) & " "
        ElseIf lblMinRange.Text = "" And lblMaxRange.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND READING_STEP <= " & Val(lblMaxRange.Text) & " "
        End If

        SqlStr = SqlStr & " ORDER BY SERIAL_NO "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            i = 1
            Do While Not .EOF
                SprdMain.Row = i

                SprdMain.Col = ColParamDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdMain.Col = ColReadingStep
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("READING_STEP").Value), "", .Fields("READING_STEP").Value))

                SprdMain.Col = ColPerError
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PER_ERROR").Value), "", .Fields("PER_ERROR").Value))

                .MoveNext()
                i = i + 1
                SprdMain.MaxRows = i
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim mInspMth As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_IMTE_CALIB_DET " & vbCrLf & " WHERE AUTO_KEY_CALIB=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTECalibDet, ADODB.LockTypeEnum.adLockReadOnly)
        With RsIMTECalibDet
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            i = 1
            Do While Not .EOF
                SprdMain.Row = i

                SprdMain.Col = ColParamDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdMain.Col = ColReadingStep
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("READING_STEP").Value), "", .Fields("READING_STEP").Value))

                SprdMain.Col = ColPerError
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PER_ERROR").Value), "", .Fields("PER_ERROR").Value))

                SprdMain.Col = ColObservation
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERVATION").Value), "", .Fields("OBSERVATION").Value))
                Call SetObsCol((SprdMain.Row), (SprdMain.Col), False)

                .MoveNext()
                i = i + 1
                SprdMain.MaxRows = i
            Loop
        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColParamDesc, ColPerError)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub ShowInst1()

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_IMTE_CALIB_INST " & vbCrLf & " WHERE AUTO_KEY_CALIB=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY DOCNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTECalibInst, ADODB.LockTypeEnum.adLockReadOnly)
        With RsIMTECalibInst
            If .EOF = True Then Exit Sub
            FormatSprdInst(-1)
            i = 1
            Do While Not .EOF
                SprdInst.Row = i

                SprdInst.Col = ColDocNo
                SprdInst.Text = Trim(IIf(IsDbNull(.Fields("DOCNO").Value), "", .Fields("DOCNO").Value))

                SprdInst.Col = ColDescription
                SprdInst.Text = Trim(IIf(IsDbNull(.Fields("Description").Value), "", .Fields("Description").Value))

                SprdInst.Col = ColModel
                SprdInst.Text = Trim(IIf(IsDbNull(.Fields("Model").Value), "", .Fields("Model").Value))

                SprdInst.Col = ColMake
                SprdInst.Text = Trim(IIf(IsDbNull(.Fields("Make_Name").Value), "", .Fields("Make_Name").Value))

                SprdInst.Col = ColCalibBy
                SprdInst.Text = Trim(IIf(IsDbNull(.Fields("CALIB_BY").Value), "", .Fields("CALIB_BY").Value))

                SprdInst.Col = ColCertNo
                SprdInst.Text = Trim(IIf(IsDbNull(.Fields("CERT_NO").Value), "", .Fields("CERT_NO").Value))

                SprdInst.Col = ColCalibValid
                SprdInst.Text = Trim(IIf(IsDbNull(.Fields("Calib_Valid").Value), "", .Fields("Calib_Valid").Value))

                .MoveNext()
                i = i + 1
                SprdInst.MaxRows = i
            Loop
        End With
        MainClass.ProtectCell(SprdInst, 1, SprdInst.MaxRows, ColDescription, ColCalibValid)
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

        If MODIFYMode = True And RsIMTECalibHdr.BOF = False Then xMKey = RsIMTECalibHdr.Fields("AUTO_KEY_CALIB").Value

        SqlStr = "SELECT * FROM QAL_IMTE_CALIB_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_CALIB=" & mSlipNo & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTECalibHdr, ADODB.LockTypeEnum.adLockReadOnly)
        If RsIMTECalibHdr.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_IMTE_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_CALIB=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTECalibHdr, ADODB.LockTypeEnum.adLockReadOnly)
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
        txtReceiptDate.Enabled = mMode
        txtDocNo.Enabled = mMode
        cmdSearchDocNo.Enabled = mMode
        txtAmbTemp.Enabled = mMode
        txtHumidity.Enabled = mMode
        txtSoakingTime.Enabled = mMode
        txtCalibProc.Enabled = mMode
        txtVisualInsp.Enabled = mMode
        txtZeroError.Enabled = mMode
        txtUncertainty.Enabled = mMode
        txtRemarks.Enabled = mMode
        txtInspBy.Enabled = mMode
        cmdSearchInspBy.Enabled = mMode
        txtAppBy.Enabled = mMode
        cmdSearchAppBy.Enabled = mMode
        chkNABLLogo.Enabled = mMode
        chkCalibOK.Enabled = mMode
        txtCretificateIssueDate.Enabled = mMode
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

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnCalibCertIMTE(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnCalibCertIMTE(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnCalibCertIMTE(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRsTemp As ADODB.Recordset
        Dim mDec1 As Short
        Dim mDec2 As Short

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "CALIBRATION CERTIFICATE"

        SqlStr = " SELECT QAL_IMTE_CALIB_HDR.*,QAL_IMTE_CALIB_DET.*,QAL_IMTE_MST.*, " & vbCrLf & " INSP.EMP_NAME,APP.EMP_NAME,FIN_SUPP_CUST_MST.SUPP_CUST_NAME " & vbCrLf & " FROM QAL_IMTE_CALIB_HDR, QAL_IMTE_CALIB_DET, QAL_IMTE_MST, " & vbCrLf & " PAY_EMPLOYEE_MST INSP, PAY_EMPLOYEE_MST APP, FIN_SUPP_CUST_MST " & vbCrLf & " WHERE QAL_IMTE_CALIB_HDR.AUTO_KEY_CALIB=QAL_IMTE_CALIB_DET.AUTO_KEY_CALIB(+) " & vbCrLf & " AND QAL_IMTE_CALIB_HDR.COMPANY_CODE=QAL_IMTE_MST.COMPANY_CODE " & vbCrLf & " AND QAL_IMTE_CALIB_HDR.DOCNO=QAL_IMTE_MST.DOCNO " & vbCrLf & " AND QAL_IMTE_CALIB_HDR.COMPANY_CODE=INSP.COMPANY_CODE(+) " & vbCrLf & " AND QAL_IMTE_CALIB_HDR.INSPECTED_BY=INSP.EMP_CODE(+) " & vbCrLf & " AND QAL_IMTE_CALIB_HDR.COMPANY_CODE=APP.COMPANY_CODE(+) " & vbCrLf & " AND QAL_IMTE_CALIB_HDR.APPROVED_BY=APP.EMP_CODE(+) " & vbCrLf & " AND QAL_IMTE_MST.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE(+) " & vbCrLf & " AND QAL_IMTE_MST.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE(+) " & vbCrLf & " AND QAL_IMTE_CALIB_HDR.AUTO_KEY_CALIB=" & Val(txtSlipNo.Text) & ""

        If lblType.Text = "VARIABLE" Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\IMTEInsp.rpt"
        Else
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\IMTECal.rpt"
        End If

        SetCrpt(Report1, Mode, 1, mTitle, mSubTitle, True, lblMenu.Text)

        If lblType.Text = "VARIABLE" Then
            SqlStr2 = "SELECT * " & vbCrLf & " FROM QAL_IMTE_PE_HDR " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DESCRIPTION ='" & MainClass.AllowSingleQuote(lblDescription.Text) & "' " & vbCrLf & " AND L_C ='" & MainClass.AllowSingleQuote(lblLC.Text) & "' "

            MainClass.UOpenRecordSet(SqlStr2, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If Not mRsTemp.EOF Then
                mDec1 = IIf(IsDbNull(mRsTemp.Fields("READING_STEP_DEC").Value), "", mRsTemp.Fields("READING_STEP_DEC").Value)
                mDec2 = IIf(IsDbNull(mRsTemp.Fields("OBSERVATION_DEC").Value), "", mRsTemp.Fields("OBSERVATION_DEC").Value)
            End If

            MainClass.AssignCRptFormulas(Report1, "Dec1=""" & mDec1 & """")
            MainClass.AssignCRptFormulas(Report1, "Dec2=""" & mDec2 & """")
        End If

        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False

        SqlStr1 = " SELECT * FROM QAL_IMTE_CALIB_INST " & vbCrLf & " WHERE AUTO_KEY_CALIB =" & Val(txtSlipNo.Text) & "" & vbCrLf & " ORDER BY SERIALNO "

        Report1.SubreportToChange = Report1.GetNthSubreportName(0)
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStr1

        Report1.SubreportToChange = ""

        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtSoakingTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSoakingTime.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSoakingTime_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSoakingTime.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSoakingTime_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSoakingTime.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtSoakingTime.Text) = "" Then GoTo EventExitSub
        txtSoakingTime.Text = VB6.Format(txtSoakingTime.Text, "0.00")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtVisualInsp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVisualInsp.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtZeroError_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtZeroError.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
End Class
