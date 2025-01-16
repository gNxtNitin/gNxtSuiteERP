Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmProcessInsp
    Inherits System.Windows.Forms.Form
    Dim RsProcessInspMain As ADODB.Recordset
    Dim RsProcessInspDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean
    Private Const ConRowHeight As Short = 14

    Private Const ColOPR As Short = 1
    Private Const ColParameter As Short = 2
    Private Const ColSpecification As Short = 3
    Private Const ColInspection As Short = 4
    Private Const ColObservation As Short = 5


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
            If RsProcessInspMain.EOF = False Then RsProcessInspMain.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtSlipNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsProcessInspMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_PROCESS_HDR", (txtSlipNo.Text), RsProcessInspMain) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_PROCESS_DET WHERE AUTO_KEY_PROCESS=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_PROCESS_HDR WHERE AUTO_KEY_PROCESS=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsProcessInspMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsProcessInspMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsProcessInspMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtSlipNo.Enabled = False
            cmdSearchSlipNo.Enabled = False
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
    Private Function CheckDuplicateParam_Specfic(ByRef pParameter As String, ByRef pSpecification As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer
        Dim xParameter As String
        Dim xSpecification As String

        If pParameter = "" Or pSpecification = "" Then CheckDuplicateParam_Specfic = False : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColParameter
                xParameter = UCase(Trim(.Text))

                .Col = ColSpecification
                xSpecification = UCase(Trim(.Text))

                If xParameter = UCase(Trim(pParameter)) And xSpecification = UCase(Trim(pSpecification)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateParam_Specfic = True
                        MsgInformation("Duplicate Entry.")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, .ActiveCol)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
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
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String

        IsRecordExist = False
        If MODIFYMode = True Then Exit Function

        SqlStr = " SELECT AUTO_KEY_PROCESS " & vbCrLf _
                & " From QAL_PROCESS_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND INSP_SLOT =" & Val(VB.Left(cboInspSlot.Text, 1)) & "  " & vbCrLf _
                & " AND LTRIM(RTRIM(SHIFT_CODE)) ='" & MainClass.AllowSingleQuote(UCase(txtShift.Text)) & "' " & vbCrLf _
                & " AND LTRIM(RTRIM(OPR_CODE)) = '" & MainClass.AllowSingleQuote(UCase(txtOperation.Text)) & "' " & vbCrLf _
                & " AND LTRIM(RTRIM(ITEM_CODE)) ='" & MainClass.AllowSingleQuote(UCase(txtPartNo.Text)) & "' " & vbCrLf _
                & " AND INSP_DATE =TO_DATE('" & vb6.Format(txtDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')  "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_PROCESS").Value)
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
            SqlStr = " INSERT INTO QAL_PROCESS_HDR " & vbCrLf _
                            & " (AUTO_KEY_PROCESS,COMPANY_CODE," & vbCrLf _
                            & " INSP_DATE,ITEM_CODE,INSP_SLOT,AUTO_KEY_STD,OPR_CODE, " & vbCrLf _
                            & " SHIFT_CODE,INSPECTED_BY, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtPartNo.Text) & "', " & vbCrLf _
                            & " " & Val(VB.Left(cboInspSlot.Text, 1)) & ", " & Val(txtInspectionSTD.Text) & ",'" & MainClass.AllowSingleQuote(txtOperation.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtShift.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtInspBy.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_PROCESS_HDR SET " & vbCrLf _
                    & " AUTO_KEY_PROCESS=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                    & " INSP_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " ITEM_CODE='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "', " & vbCrLf _
                    & " INSP_SLOT=" & Val(VB.Left(cboInspSlot.Text, 1)) & ",AUTO_KEY_STD=" & Val(txtInspectionSTD.Text) & ", " & vbCrLf _
                    & " OPR_CODE='" & MainClass.AllowSingleQuote(txtOperation.Text) & "', " & vbCrLf _
                    & " SHIFT_CODE='" & MainClass.AllowSingleQuote(txtShift.Text) & "', " & vbCrLf _
                    & " INSPECTED_BY='" & MainClass.AllowSingleQuote(txtInspBy.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND AUTO_KEY_PROCESS =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        txtSlipNo.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsProcessInspMain.Requery()
        RsProcessInspDetail.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_PROCESS)  " & vbCrLf & " FROM QAL_PROCESS_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PROCESS,LENGTH(AUTO_KEY_PROCESS)-5,4)=" & RsCompany.Fields("FYEAR").Value & " "

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
        Dim mParameter As String
        Dim mSpecification As String
        Dim mInspection As String
        Dim mObservation As String

        Dim pOPRDesc As String
        Dim pOPRCode As String
        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset

        PubDBCn.Execute("DELETE FROM QAL_PROCESS_DET WHERE AUTO_KEY_PROCESS=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColParameter
                mParameter = MainClass.AllowSingleQuote(.Text)

                .Col = ColSpecification
                mSpecification = MainClass.AllowSingleQuote(.Text)

                .Col = ColInspection
                mInspection = MainClass.AllowSingleQuote(.Text)

                .Col = ColObservation
                mObservation = MainClass.AllowSingleQuote(.Text)

                .Col = ColOPR
                pOPRDesc = Trim(.Text)
                If Trim(pOPRDesc) = "" Then
                    pOPRCode = ""
                Else
                    pSqlStr = " SELECT IMST.OPR_CODE " & vbCrLf & " FROM PRD_OPR_MST IMST, PRD_OPR_TRN TRN" & vbCrLf & " WHERE IMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IMST.COMPANY_CODE=TRN.COMPANY_CODE" & vbCrLf & " AND IMST.OPR_CODE=TRN.OPR_CODE" & vbCrLf & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "'" & vbCrLf & " AND IMST.OPR_DESC='" & MainClass.AllowSingleQuote(pOPRDesc) & "'"

                    MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = False Then
                        pOPRCode = IIf(IsDbNull(RsTemp.Fields("OPR_CODE").Value), "", RsTemp.Fields("OPR_CODE").Value)
                    Else
                        pOPRCode = ""
                    End If
                End If

                SqlStr = ""

                If mParameter <> "" And mSpecification <> "" Then
                    SqlStr = " INSERT INTO  QAL_PROCESS_DET ( " & vbCrLf & " AUTO_KEY_PROCESS,SERIAL_NO,PARAM_DESC,SPECIFICATION,INSP_MTH, " & vbCrLf & " OBSERVATION,OPR_CODE ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mParameter & "','" & mSpecification & "', " & vbCrLf & " '" & mInspection & "','" & mObservation & "','" & pOPRCode & "') "
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

    Private Sub cmdSearchOperation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchOperation.Click
        Dim SqlStr As String


        SqlStr = OperationQuery(Trim(txtPartNo.Text), "", Trim(txtOperation.Text), "", Trim(txtDate.Text), "TRN.OPR_CODE", "OPR_DESC", "TRN.DEPT_CODE")

        '    SqlStr = " SELECT DISTINCT A.OPR_CODE, B.OPR_DESC " & vbCrLf _
        ''            & " FROM PRD_OPR_TRN A, PRD_OPR_MST B " & vbCrLf _
        ''            & " WHERE B.OPR_CODE = A.OPR_CODE " & vbCrLf _
        ''            & " AND LTRIM(RTRIM(B.OPR_CODE)) = LTRIM(RTRIM(A.OPR_CODE)) " & vbCrLf _
        ''            & " AND B.COMPANY_CODE = " & RsCompany.fields("COMPANY_CODE").value & " AND A.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "'" & vbCrLf _
        ''            & " ORDER BY B.OPR_DESC "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtOperation.Text = AcName
            lblOperation.text = AcName1
            '        If txtOperation.Enabled = True Then txtOperation.SetFocus
        End If

    End Sub

    Private Sub cmdSearchPartNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPartNo.Click
        Dim SqlStr As String

        SqlStr = " SELECT A.PRODUCT_CODE, B.ITEM_SHORT_DESC" & vbCrLf & " FROM PRD_NEWBOM_HDR A, INV_ITEM_MST B " & vbCrLf & " WHERE B.COMPANY_CODE = A.COMPANY_CODE " & vbCrLf & " AND LTRIM(RTRIM(B.ITEM_CODE)) = LTRIM(RTRIM(A.PRODUCT_CODE)) " & vbCrLf & " AND A.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "  AND A.STATUS='O'" & vbCrLf & " ORDER BY B.ITEM_SHORT_DESC"
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtPartNo.Text = AcName
            lblPartNo.text = AcName1
            If txtPartNo.Enabled = True Then txtPartNo.Focus()
        End If
    End Sub

    Private Sub cmdSearchShift_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchShift.Click
        Dim SqlStr As String

        SqlStr = " COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster("", "PAY_SHIFT_MST", "SHIFT_CODE", "SHIFT_DESC", , , SqlStr) = True Then
            txtShift.Text = AcName
            lblShift.text = AcName1
            If txtShift.Enabled = True Then txtShift.Focus()
        End If
    End Sub

    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PROCESS,LENGTH(AUTO_KEY_PROCESS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_PROCESS_HDR", "AUTO_KEY_PROCESS", "INSP_DATE", "ITEM_CODE", "AUTO_KEY_STD", SqlStr) = True Then
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
        MainClass.ButtonStatus(Me, XRIGHT, RsProcessInspMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmProcessInsp_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Process Inspection"

        SqlStr = "Select * From QAL_PROCESS_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcessInspMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_PROCESS_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcessInspDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_PROCESS AS SLIP_NUMBER,TO_CHAR(INSP_DATE,'DD/MM/YYYY') AS INSP_DATE, " & vbCrLf & " ITEM_CODE,INSP_SLOT,AUTO_KEY_STD,OPR_CODE,SHIFT_CODE " & vbCrLf & " FROM QAL_PROCESS_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PROCESS,LENGTH(AUTO_KEY_PROCESS)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_PROCESS"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmProcessInsp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmProcessInsp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        cboInspSlot.Items.Add("1st Inspection")
        cboInspSlot.Items.Add("2nd Inspection")
        '    cboInspSlot.AddItem "3rd Inspection"
        '    cboInspSlot.AddItem "4th Inspection"
        cboInspSlot.SelectedIndex = 0

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
        txtPartNo.Text = ""
        lblPartNo.Text = ""
        cboInspSlot.SelectedIndex = 0
        txtInspectionSTD.Text = ""
        lblDocNo.Text = ""
        lblIssueNo.Text = ""
        txtOperation.Text = ""
        lblOperation.Text = ""
        txtShift.Text = ""
        lblShift.Text = ""
        lblModel.Text = ""
        txtInspBy.Text = ""
        lblInspBy.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsProcessInspMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

            .Col = ColParameter
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsProcessInspDetail.Fields("PARAM_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 14)

            .Col = ColSpecification
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsProcessInspDetail.Fields("SPECIFICATION").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 17)

            .Col = ColInspection
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsProcessInspDetail.Fields("INSP_MTH").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 17)

            .Col = ColObservation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsProcessInspDetail.Fields("OBSERVATION").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 17)

            .Col = ColOPR
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("OPR_DESC", "PRD_OPR_MST", PubDBCn)
            .set_ColWidth(.Col, 14) '' 7.5

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColParameter, ColInspection)
            MainClass.SetSpreadColor(SprdMain, Arow)
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
            .set_ColWidth(5, 500 * 4)
            .set_ColWidth(6, 500 * 3)
            .set_ColWidth(7, 500 * 3)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.Maxlength = RsProcessInspMain.Fields("AUTO_KEY_PROCESS").Precision
        txtDate.Maxlength = RsProcessInspMain.Fields("INSP_DATE").DefinedSize - 6
        txtPartNo.Maxlength = RsProcessInspMain.Fields("ITEM_CODE").DefinedSize
        txtInspectionSTD.Maxlength = RsProcessInspMain.Fields("AUTO_KEY_STD").Precision
        txtOperation.Maxlength = RsProcessInspMain.Fields("OPR_CODE").DefinedSize
        txtShift.Maxlength = RsProcessInspMain.Fields("SHIFT_CODE").DefinedSize
        txtInspBy.Maxlength = RsProcessInspMain.Fields("INSPECTED_BY").DefinedSize

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim ii As Integer
        Dim mOprDesc As String
        Dim pOPRCode As String
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim cntRow As Integer

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsProcessInspMain.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtPartNo.Text) = "" Then
            MsgInformation("Part No. empty, So unable to save.")
            txtPartNo.Focus()
            FieldsVarification = False
            Exit Function
        End If
        '    If Trim(txtOperation.Text) = "" Then
        '        MsgInformation "Operation Code empty, So unable to save."
        '        txtOperation.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        If Trim(txtShift.Text) = "" Then
            MsgInformation("Shift Code empty, So unable to save.")
            txtShift.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtInspBy.Text) = "" Then
            MsgInformation("Inspection Employee Code is empty, So unable to save.")
            txtInspBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColOPR
                mOprDesc = Trim(.Text)
                pOPRCode = ""
                If mOprDesc <> "" Then
                    '        SqlStr = OperationQuery(Trim(txtItemCode.Text), "", "", "", Format(PubCurrDate, "DD/MM/YYYY"), "TRN.OPR_CODE")
                    '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
                    '
                    '        If RsTemp.EOF = False Then
                    '            MsgInformation "Operation Defined for Item Code : " & mProductCode & ". Cann't Be Saved"
                    '            FieldsVarification = False
                    '            MainClass.SetFocusToCell SprdMain, cntRow, ColOPR
                    '            Exit Function
                    '        End If
                    '    Else
                    SqlStr = OperationQuery(Trim(txtPartNo.Text), "", "", Trim(mOprDesc), VB6.Format(PubCurrDate, "DD/MM/YYYY"), "TRN.OPR_CODE")
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = True Then
                        MsgInformation("Invalid Operation for Item Code : " & txtPartNo.Text & ". Cann't Be Saved")
                        FieldsVarification = False
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColOPR)
                        Exit Function
                    Else
                        pOPRCode = IIf(IsDbNull(RsTemp.Fields("OPR_CODE").Value), "", RsTemp.Fields("OPR_CODE").Value)
                    End If
                End If
            Next
        End With

        If MainClass.ValidDataInGrid(SprdMain, ColParameter, "S", "Please Check Parameter Description.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColSpecification, "S", "Please Check Specification Details.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColInspection, "S", "Please Check Inspection Method.") = False Then FieldsVarification = False : Exit Function
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmProcessInsp_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsProcessInspMain.Close()
        RsProcessInspMain = Nothing
        RsProcessInspDetail.Close()
        RsProcessInspDetail = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String
        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColParameter)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim xParameter As String
        Dim xSpecification As String

        If eventArgs.NewRow = -1 Then Exit Sub


        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColParameter
        xParameter = Trim(SprdMain.Text)
        If xParameter = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColParameter 'GRID IS FILLING ON THE BASIS OF txtInspectionSTD.TEXT, SO MARKED
                '            SprdMain.Row = SprdMain.ActiveRow
                '
                '            SprdMain.Col = ColParameter
                '            xParameter = Trim(SprdMain.Text)
                '            If xParameter = "" Then Exit Sub
                '
                '            SprdMain.Col = ColSpecification
                '            xSpecification = Trim(SprdMain.Text)
                '
                '            If CheckDuplicateParam_Specfic(xParameter, xSpecification) = False Then
                '                MainClass.AddBlankSprdRow SprdMain, ColParameter, ConRowHeight
                '                FormatSprdMain SprdMain.MaxRows
                '            End If
            Case ColSpecification 'GRID IS FILLING ON THE BASIS OF txtInspectionSTD.TEXT, SO MARKED
                '            SprdMain.Row = SprdMain.ActiveRow
                '
                '            SprdMain.Col = ColParameter
                '            xParameter = Trim(SprdMain.Text)
                '            If xParameter = "" Then Exit Sub
                '
                '            SprdMain.Col = ColSpecification
                '            xSpecification = Trim(SprdMain.Text)
                '
                '            Call CheckDuplicateParam_Specfic(xParameter, xSpecification)
        End Select
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


    Private Sub txtInspBy_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInspBy.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInspBy.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtInspectionSTD_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInspectionSTD.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInspectionSTD.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOperation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOperation.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOperation_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOperation.DoubleClick
        Call cmdSearchOperation_Click(cmdSearchOperation, New System.EventArgs())
    End Sub

    Private Sub txtOperation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOperation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOperation.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOperation_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtOperation.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchOperation_Click(cmdSearchOperation, New System.EventArgs())
    End Sub

    Private Sub txtOperation_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOperation.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtOperation.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT DISTINCT A.OPR_CODE, B.OPR_DESC " & vbCrLf _
                    & " FROM PRD_OPR_TRN A, PRD_OPR_MST B " & vbCrLf _
                    & " WHERE B.OPR_CODE = A.OPR_CODE " & vbCrLf _
                    & " AND LTRIM(RTRIM(B.OPR_CODE)) = LTRIM(RTRIM(A.OPR_CODE)) " & vbCrLf _
                    & " AND B.COMPANY_CODE = " & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND LTRIM(RTRIM(A.OPR_CODE)) ='" & MainClass.AllowSingleQuote(LTrim(RTrim(txtOperation.Text))) & "'  AND LTRIM(RTRIM(A.PRODUCT_CODE)) ='" & MainClass.AllowSingleQuote(LTrim(RTrim(txtPartNo.Text))) & "'" & vbCrLf _
                    & " ORDER BY B.OPR_DESC "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                lblOperation.Text = IIf(IsDbNull(mRsTemp.Fields("OPR_DESC").Value), "", .Fields("OPR_DESC").Value)
                Call FillGrid()
            Else
                MsgBox("Not a valid Operation.")
                lblOperation.Text = "'"
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub FillGrid()

        On Error GoTo FillERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mRsTemp As ADODB.Recordset
        SqlStr = " SELECT DECODE(PROD_DESC, NULL,PROC_DESC,PROD_DESC) PARAM_DESC, " & vbCrLf _
                    & " SPECIFICATION, INSP_METHOD " & vbCrLf _
                    & " From QAL_CONTROLPLAN_DET " & vbCrLf _
                    & " WHERE AUTO_KEY_CTRLPLAN =" & Val(txtInspectionSTD.Text) & " " & vbCrLf _
                    & " AND LTRIM(RTRIM(OPR_CODE)) ='" & MainClass.AllowSingleQuote(LTrim(RTrim(txtOperation.Text))) & "' " & vbCrLf _
                    & " AND LTRIM(RTRIM(CONTROL_METHOD)) = 'QAD-F-14'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColParameter
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdMain.Col = ColSpecification
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPECIFICATION").Value), "", .Fields("SPECIFICATION").Value))

                SprdMain.Col = ColInspection
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("INSP_METHOD").Value), "", .Fields("INSP_METHOD").Value))

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

    Private Sub txtPartNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPartNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartNo.DoubleClick
        Call cmdSearchPartNo_Click(cmdSearchPartNo, New System.EventArgs())
    End Sub

    Private Sub txtPartNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPartNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchPartNo_Click(cmdSearchPartNo, New System.EventArgs())
    End Sub

    Private Sub txtPartNo_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartNo.Leave
        If Trim(txtPartNo.Text) = "" Then Exit Sub
        '    If txtOperation.Enabled = True Then txtOperation.SetFocus
    End Sub

    Private Sub txtPartNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtPartNo.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT A.ITEM_SHORT_DESC, A.ITEM_MODEL " & vbCrLf _
                & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B " & vbCrLf _
                & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _
                & " AND LTRIM(RTRIM(A.ITEM_CODE)) = LTRIM(RTRIM(B.ITEM_CODE)) " & vbCrLf _
                & " AND B.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND LTRIM(RTRIM(B.ITEM_CODE)) = '" & MainClass.AllowSingleQuote(txtPartNo.Text) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                lblPartNo.Text = Trim(IIf(IsDbNull(mRsTemp.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value))
                lblModel.Text = IIf(IsDbNull(mRsTemp.Fields("ITEM_MODEL").Value), "", .Fields("ITEM_MODEL").Value)

                Call FillInspectionSTD()
            Else
                MsgBox("Not a valid Part No.")
                lblPartNo.Text = ""
                lblModel.Text = ""
                Cancel = True
            End If
        End With
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub FillInspectionSTD()

        On Error GoTo FillERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mRsTemp As ADODB.Recordset
        Dim mOprCode As String
        Dim mOprDesc As String

        If Trim(txtPartNo.Text) = "" Then Exit Sub

        SqlStr = "SELECT ID.SERIAL_NO, ID.OPR_CODE, ID.PARAM_DESC, ID.SPECIFICATION , ID.INSP_MTH" & vbCrLf _
                        & " From QAL_INSPECTION_STD_HDR IH, QAL_INSPECTION_STD_DET ID" & vbCrLf _
                        & " WHERE IH.AUTO_KEY_STD = ID.AUTO_KEY_STD AND IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                        & " AND IH.ITEM_CODE='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "'" & vbCrLf _
                        & " AND IH.INSP_TYPE='P' ORDER BY ID.OPR_CODE, ID.SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                mOprCode = Trim(IIf(IsDbNull(.Fields("OPR_CODE").Value), "", .Fields("OPR_CODE").Value))

                If MainClass.ValidateWithMasterTable(mOprCode, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mOprDesc = MasterNo
                Else
                    mOprDesc = ""
                End If

                SprdMain.Col = ColOPR
                SprdMain.Text = mOprDesc

                SprdMain.Col = ColParameter
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdMain.Col = ColSpecification
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPECIFICATION").Value), "", .Fields("SPECIFICATION").Value))

                SprdMain.Col = ColInspection
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
        If Trim(txtInspBy.Text) = "" Then GoTo EventExitSub
        txtInspBy.Text = VB6.Format(txtInspBy.Text, "000000")
        If ValidateEMP(txtInspBy, lblInspBy) = False Then Cancel = True
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtInspectionSTD_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspectionSTD.TextChanged

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
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtShift_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShift.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtShift_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShift.DoubleClick
        Call cmdSearchShift_Click(cmdSearchShift, New System.EventArgs())
    End Sub

    Private Sub txtShift_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtShift.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtShift.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtShift_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtShift.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchShift_Click(cmdSearchShift, New System.EventArgs())
    End Sub

    Private Sub txtShift_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtShift.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtShift.Text) = "" Then GoTo EventExitSub
        SqlStr = " COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.ValidateWithMasterTable(txtShift.Text, "SHIFT_CODE", "SHIFT_DESC", "PAY_SHIFT_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblShift.text = MasterNo
            If txtShift.Enabled = True Then txtShift.Focus()
        Else
            MsgBox("Not a valid Shift Code.")
            lblShift.text = ""
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

        If Not RsProcessInspMain.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsProcessInspMain.Fields("AUTO_KEY_PROCESS").Value), "", RsProcessInspMain.Fields("AUTO_KEY_PROCESS").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsProcessInspMain.Fields("AUTO_KEY_PROCESS").Value), "", RsProcessInspMain.Fields("AUTO_KEY_PROCESS").Value)
            txtDate.Text = IIf(IsDbNull(RsProcessInspMain.Fields("INSP_DATE").Value), "", RsProcessInspMain.Fields("INSP_DATE").Value)
            txtPartNo.Text = IIf(IsDbNull(RsProcessInspMain.Fields("ITEM_CODE").Value), "", RsProcessInspMain.Fields("ITEM_CODE").Value)
            txtPartNo_Validating(txtPartNo, New System.ComponentModel.CancelEventArgs(False))
            Select Case Val(IIf(IsDbNull(RsProcessInspMain.Fields("INSP_SLOT").Value), "", RsProcessInspMain.Fields("INSP_SLOT").Value))
                Case 1
                    cboInspSlot.Text = "1st Inspection"
                Case 2
                    cboInspSlot.Text = "2nd Inspection"
                Case 3
                    cboInspSlot.Text = "3rd Inspection"
                Case 4
                    cboInspSlot.Text = "4th Inspection"
            End Select
            txtInspectionSTD.Text = IIf(IsDbNull(RsProcessInspMain.Fields("AUTO_KEY_STD").Value), "", RsProcessInspMain.Fields("AUTO_KEY_STD").Value)
            txtOperation.Text = IIf(IsDbNull(RsProcessInspMain.Fields("OPR_CODE").Value), "", RsProcessInspMain.Fields("OPR_CODE").Value)
            txtOperation_Validating(txtOperation, New System.ComponentModel.CancelEventArgs(False))
            txtShift.Text = IIf(IsDbNull(RsProcessInspMain.Fields("SHIFT_CODE").Value), "", RsProcessInspMain.Fields("SHIFT_CODE").Value)
            txtShift_Validating(txtShift, New System.ComponentModel.CancelEventArgs(False))
            txtInspBy.Text = IIf(IsDbNull(RsProcessInspMain.Fields("INSPECTED_BY").Value), "", RsProcessInspMain.Fields("INSPECTED_BY").Value)
            txtInspBy_Validating(txtInspBy, New System.ComponentModel.CancelEventArgs(False))
            Call ShowDetail1()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsProcessInspMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim mOprCode As String
        Dim mOprDesc As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_PROCESS_DET " & vbCrLf & " WHERE AUTO_KEY_PROCESS=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcessInspDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsProcessInspDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColParameter
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdMain.Col = ColSpecification
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPECIFICATION").Value), "", .Fields("SPECIFICATION").Value))

                SprdMain.Col = ColInspection
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("INSP_MTH").Value), "", .Fields("INSP_MTH").Value))

                SprdMain.Col = ColObservation
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERVATION").Value), "", .Fields("OBSERVATION").Value))

                mOprCode = IIf(IsDbNull(.Fields("OPR_CODE").Value), "", .Fields("OPR_CODE").Value)

                If MainClass.ValidateWithMasterTable(mOprCode, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mOprDesc = MasterNo
                Else
                    mOprDesc = ""
                End If
                SprdMain.Col = ColOPR
                SprdMain.Text = mOprDesc

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
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
    Private Sub txtSlipNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSlipNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String


        If Trim(txtSlipNo.Text) = "" Then GoTo EventExitSub
        mSlipNo = Val(txtSlipNo.Text)

        If MODIFYMode = True And RsProcessInspMain.BOF = False Then xMkey = RsProcessInspMain.Fields("AUTO_KEY_PROCESS").Value

        SqlStr = "SELECT * FROM QAL_PROCESS_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PROCESS,LENGTH(AUTO_KEY_PROCESS)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PROCESS=" & mSlipNo & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcessInspMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsProcessInspMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_PROCESS_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PROCESS,LENGTH(AUTO_KEY_PROCESS)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PROCESS=" & Val(CStr(xMkey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcessInspMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        txtPartNo.Enabled = mMode
        cmdSearchPartNo.Enabled = mMode
        txtInspectionSTD.Enabled = False
        txtOperation.Enabled = mMode
        cmdsearchOperation.Enabled = mMode
        txtShift.Enabled = mMode
        cmdSearchShift.Enabled = mMode


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
    Private Sub ReportOnProcessInsp(ByRef Mode As Crystal.DestinationConstants)

    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnProcessInsp(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnProcessInsp(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
