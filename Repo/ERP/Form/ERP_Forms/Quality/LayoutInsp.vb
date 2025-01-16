Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLayoutInsp
    Inherits System.Windows.Forms.Form
    Dim RsLayoutInspMain As ADODB.Recordset
    Dim RsLayoutInspDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim xMenuID As String

    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 14

    Private Const ColParameter As Short = 1
    Private Const ColSpecification As Short = 2
    Private Const ColInspection As Short = 3
    Private Const ColObserv1 As Short = 4
    Private Const ColObserv2 As Short = 5
    Private Const ColObserv3 As Short = 6
    Private Const ColObserv4 As Short = 7
    Private Const ColObserv5 As Short = 8
    Private Const ColRemarks As Short = 9

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
            If RsLayoutInspMain.EOF = False Then RsLayoutInspMain.MoveFirst()
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
        If Not RsLayoutInspMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If UpdateLayoutTrn(False) = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "QAL_LAYOUT_HDR", (txtSlipNo.Text), RsLayoutInspMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "QAL_LAYOUT_HDR", "AUTO_KEY_LAYOUT", (txtSlipNo.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_LAYOUT_DET WHERE AUTO_KEY_LAYOUT=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_LAYOUT_HDR WHERE AUTO_KEY_LAYOUT=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsLayoutInspMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsLayoutInspMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsLayoutInspMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then Exit Sub
        'If IsRecordExist = True Then Exit Sub STANDARD COULD BE MADE MULTY TIMES
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
        SqlStr = " SELECT AUTO_KEY_LAYOUT " & vbCrLf _
                & " FROM QAL_LAYOUT_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND UPPER(LTRIM(RTRIM(ITEM_CODE))) ='" & MainClass.AllowSingleQuote(UCase(txtPartNo.Text)) & "'  " & vbCrLf _
                & " AND UPPER(LTRIM(RTRIM(AUTO_KEY_LIP))) = " & Val(txtPlanNo.Text) & "  "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_LAYOUT").Value)
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
            SqlStr = " INSERT INTO QAL_LAYOUT_HDR " & vbCrLf _
                            & " (AUTO_KEY_LAYOUT,COMPANY_CODE," & vbCrLf _
                            & " INSP_DATE,STAGE,AUTO_KEY_LIP,ITEM_CODE,SUPP_CUST_CODE,PROJ_DESC, " & vbCrLf _
                            & " REMARKS,INSPECTED_BY,AUTH_EMP,AUTO_KEY_STD, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(lblStage.text) & "', " & vbCrLf _
                            & " " & Val(txtPlanNo.Text) & ",'" & MainClass.AllowSingleQuote(txtPartNo.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtSource.Text) & "','" & MainClass.AllowSingleQuote(txtProject.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','" & MainClass.AllowSingleQuote(txtInspectedBy.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtAuthorisedBy.Text) & "', " & vbCrLf _
                            & " " & Val(txtInspectionSTD.Text) & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_LAYOUT_HDR SET " & vbCrLf _
                    & " AUTO_KEY_LAYOUT=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                    & " INSP_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),STAGE='" & MainClass.AllowSingleQuote(lblStage.text) & "', " & vbCrLf _
                    & " AUTO_KEY_LIP=" & Val(txtPlanNo.Text) & ",ITEM_CODE='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "', " & vbCrLf _
                    & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSource.Text) & "',PROJ_DESC='" & MainClass.AllowSingleQuote(txtProject.Text) & "', " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "',INSPECTED_BY='" & MainClass.AllowSingleQuote(txtInspectedBy.Text) & "', " & vbCrLf _
                    & " AUTH_EMP='" & MainClass.AllowSingleQuote(txtAuthorisedBy.Text) & "',AUTO_KEY_STD=" & Val(txtInspectionSTD.Text) & ", " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND AUTO_KEY_LAYOUT =" & Val(lblMkey.text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail = False Then GoTo ErrPart
        If UpdateLayoutTrn(True) = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        txtSlipNo.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsLayoutInspMain.Requery()
        RsLayoutInspDetail.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function UpdateLayoutTrn(ByRef pUpdation As Boolean) As Boolean
        On Error GoTo UpdateERR
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mFieldName As String

        If Trim(txtDate.Text) = "" Then UpdateLayoutTrn = True : Exit Function
        If Month(CDate(txtDate.Text)) = 1 Then
            mFieldName = "JAN_ACTUAL"
        ElseIf Month(CDate(txtDate.Text)) = 2 Then
            mFieldName = "FEB_ACTUAL"
        ElseIf Month(CDate(txtDate.Text)) = 3 Then
            mFieldName = "MAR_ACTUAL"
        ElseIf Month(CDate(txtDate.Text)) = 4 Then
            mFieldName = "APR_ACTUAL"
        ElseIf Month(CDate(txtDate.Text)) = 5 Then
            mFieldName = "MAY_ACTUAL"
        ElseIf Month(CDate(txtDate.Text)) = 6 Then
            mFieldName = "JUN_ACTUAL"
        ElseIf Month(CDate(txtDate.Text)) = 7 Then
            mFieldName = "JUL_ACTUAL"
        ElseIf Month(CDate(txtDate.Text)) = 8 Then
            mFieldName = "AUG_ACTUAL"
        ElseIf Month(CDate(txtDate.Text)) = 9 Then
            mFieldName = "SEP_ACTUAL"
        ElseIf Month(CDate(txtDate.Text)) = 10 Then
            mFieldName = "OCT_ACTUAL"
        ElseIf Month(CDate(txtDate.Text)) = 11 Then
            mFieldName = "NOV_ACTUAL"
        ElseIf Month(CDate(txtDate.Text)) = 12 Then
            mFieldName = "DEC_ACTUAL"
        End If

        If pUpdation = True Then
            SqlStr = " UPDATE QAL_LAYOUT_PLAN_TRN " & vbCrLf & " SET " & mFieldName & " =TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_NEW_LIP = " & Val(txtPlanNo.Text) & "  "
        ElseIf pUpdation = False Then
            SqlStr = " UPDATE QAL_LAYOUT_PLAN_TRN " & vbCrLf & " SET " & mFieldName & " ='' " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_NEW_LIP = " & Val(txtPlanNo.Text) & "  "
        End If
        PubDBCn.Execute(SqlStr)
        UpdateLayoutTrn = True
        Exit Function
UpdateERR:
        UpdateLayoutTrn = False
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
        SqlStr = "SELECT Max(AUTO_KEY_LAYOUT)  " & vbCrLf & " FROM QAL_LAYOUT_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_LAYOUT,LENGTH(AUTO_KEY_LAYOUT)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
        Dim mObserv1 As String
        Dim mObserv2 As String
        Dim mObserv3 As String
        Dim mObserv4 As String
        Dim mObserv5 As String
        Dim mRemarks As String

        PubDBCn.Execute("DELETE FROM QAL_LAYOUT_DET WHERE AUTO_KEY_LAYOUT=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColParameter
                mParameter = MainClass.AllowSingleQuote(.Text)

                .Col = ColSpecification
                mSpecification = MainClass.AllowSingleQuote(.Text)

                .Col = ColInspection
                mInspection = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv1
                mObserv1 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv2
                mObserv2 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv3
                mObserv3 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv4
                mObserv4 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv5
                mObserv5 = MainClass.AllowSingleQuote(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mParameter <> "" And mSpecification <> "" Then
                    SqlStr = " INSERT INTO  QAL_LAYOUT_DET ( " & vbCrLf & " AUTO_KEY_LAYOUT,SERIAL_NO,PARAM_DESC,SPECIFICATION,INSP_MTH, " & vbCrLf & " OBSERV_1,OBSERV_2,OBSERV_3,OBSERV_4,OBSERV_5,REMARKS) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mParameter & "','" & mSpecification & "', " & vbCrLf & " '" & mInspection & "','" & mObserv1 & "','" & mObserv2 & "', " & vbCrLf & " '" & mObserv3 & "','" & mObserv4 & "','" & mObserv5 & "','" & mRemarks & "' )"
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail = True
        Exit Function
UpdateDetailERR:
        UpdateDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Function

    Private Sub cmdSearchAuthorised_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAuthorised.Click
        Call SearchEmp(txtAuthorisedBy, lblAuthorisedBy)
    End Sub

    Private Sub cmdSearchInspected_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchInspected.Click
        Call SearchEmp(txtInspectedBy, lblInspectedBy)
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


    Private Sub cmdSearchInspSTD_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchInspSTD.Click
        Dim SqlStr As String
        SqlStr = " SELECT QAL_INSPECTION_STD_HDR.AUTO_KEY_STD, QAL_INSPECTION_STD_HDR.ITEM_CODE, INV_ITEM_MST.ITEM_SHORT_DESC " & vbCrLf & " FROM QAL_INSPECTION_STD_HDR, INV_ITEM_MST " & vbCrLf & " WHERE QAL_INSPECTION_STD_HDR.COMPANY_CODE = INV_ITEM_MST.COMPANY_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.ITEM_CODE = INV_ITEM_MST.ITEM_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.INSP_TYPE='L' "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtInspectionSTD.Text = AcName
            txtPartNo.Text = AcName1
            If txtInspectionSTD.Enabled = True Then txtInspectionSTD.Focus()
        End If
    End Sub

    Private Sub CmdSearchPlanNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchPlanNo.Click
        Dim SqlStr As String

        SqlStr = " SELECT TO_CHAR(SCHLD_NO),DT, SUPP_CUST_CODE,PRODUCT_CODE  " & vbCrLf _
                    & " FROM VW_LAYOUT_PLAN " & vbCrLf _
                    & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND CAL_YEAR =" & RsCompany.fields("FYEAR").value & "" & vbCrLf _
                    & " AND DT <= TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                    & " AND ACDT IS NULL AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "' "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtPlanNo.Text = AcName
            txtSource.Text = AcName1
            If txtPlanNo.Enabled = True Then txtPlanNo.Focus()
        End If
    End Sub
    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_LAYOUT,LENGTH(AUTO_KEY_LAYOUT)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_LAYOUT_HDR", "AUTO_KEY_LAYOUT", "INSP_DATE", "PROJ_DESC", "SUPP_CUST_CODE", SqlStr) = True Then
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
        MainClass.ButtonStatus(Me, XRIGHT, RsLayoutInspMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmLayoutInsp_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Layout Inspection"

        SqlStr = "Select * From QAL_LAYOUT_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLayoutInspMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_LAYOUT_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLayoutInspDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_LAYOUT AS SLIP_NUMBER,TO_CHAR(INSP_DATE,'DD/MM/YYYY') AS INSP_DATE, " & vbCrLf & " AUTO_KEY_LIP,ITEM_CODE,SUPP_CUST_CODE,PROJ_DESC  " & vbCrLf & " FROM QAL_LAYOUT_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_LAYOUT,LENGTH(AUTO_KEY_LAYOUT)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_LAYOUT"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmLayoutInsp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmLayoutInsp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMenuID = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(7350)
        'Me.Width = VB6.TwipsToPixelsX(10755)
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
        lblStage.Text = "Layout Inspection"
        txtPlanNo.Text = ""
        lblPlanDate.Text = ""
        txtPartNo.Text = ""
        lblPartNo.Text = ""
        txtSource.Text = ""
        lblSource.Text = ""
        txtProject.Text = ""
        txtRemarks.Text = ""
        txtInspectedBy.Text = ""
        lblInspectedBy.Text = ""
        txtAuthorisedBy.Text = ""
        lblAuthorisedBy.Text = ""
        txtInspectionSTD.Text = ""
        lblNote.Text = "** NOTE : " & vbNewLine & "A :- TO BE RECTIFIED URGENTLY" & vbNewLine & "B :- CAN BE RECTIFIED BASED ON ACTION PLAN" & vbNewLine & "C :- CAN BE RECTIIED AT THE TIME OF NEW /SPARE DEVLOPMENT" & vbNewLine & "X :- NO NEED OF RECTIFICATION" & vbNewLine & "O :- OK"
        lblNote.Font = VB6.FontChangeSize(lblNote.Font, 7)

        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsLayoutInspMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            .set_RowHeight(0, ConRowHeight * 2)
            .Row = Arow

            .Col = ColParameter
            .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsLayoutInspDetail.Fields("PARAM_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)
            .TypeEditMultiLine = True


            .Col = ColSpecification
            .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsLayoutInspDetail.Fields("SPECIFICATION").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)
            .TypeEditMultiLine = True
            .ColsFrozen = ColSpecification

            .Col = ColInspection
            .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsLayoutInspDetail.Fields("INSP_MTH").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)
            .TypeEditMultiLine = True

            .Col = ColObserv1
            .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsLayoutInspDetail.Fields("OBSERV_1").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)
            .TypeEditMultiLine = True

            .Col = ColObserv2
            .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsLayoutInspDetail.Fields("OBSERV_2").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)
            .TypeEditMultiLine = True

            .Col = ColObserv3
            .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsLayoutInspDetail.Fields("OBSERV_3").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)
            .TypeEditMultiLine = True

            .Col = ColObserv4
            .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsLayoutInspDetail.Fields("OBSERV_4").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)
            .TypeEditMultiLine = True

            .Col = ColObserv5
            .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsLayoutInspDetail.Fields("OBSERV_5").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)
            .TypeEditMultiLine = True

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_COMBOBOX
            ''.TypeComboBoxList = "A :- TO BE RECTIFIED URGENTLY" & Chr(9) & "B :- CAN BE RECTIFIED BASED ON ACTION PLAN" & Chr(9) & "C :- CAN BE RECTIIED AT THE TIME OF NEW /SPARE DEVLOPMENT" & Chr(9) & "X :- NO NEED OF RECTIFICATION" & Chr(9) & " "
            .TypeComboBoxList = "A" & Chr(9) & "B" & Chr(9) & "C" & Chr(9) & "X" & Chr(9) & "O" & Chr(9) & " "

            .set_ColWidth(.Col, 8)
            .TypeComboBoxCurSel = 5

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
            .set_ColWidth(3, 500 * 5)
            .set_ColWidth(4, 500 * 3)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 3)
            .set_ColWidth(7, 500 * 5)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.Maxlength = RsLayoutInspMain.Fields("AUTO_KEY_LAYOUT").Precision
        txtDate.Maxlength = RsLayoutInspMain.Fields("INSP_DATE").DefinedSize - 6
        txtPlanNo.Maxlength = RsLayoutInspMain.Fields("AUTO_KEY_LIP").Precision
        txtPartNo.Maxlength = RsLayoutInspMain.Fields("ITEM_CODE").DefinedSize
        txtSource.Maxlength = RsLayoutInspMain.Fields("SUPP_CUST_CODE").DefinedSize
        txtProject.Maxlength = RsLayoutInspMain.Fields("PROJ_DESC").DefinedSize
        txtRemarks.Maxlength = RsLayoutInspMain.Fields("REMARKS").DefinedSize
        txtInspectedBy.Maxlength = RsLayoutInspMain.Fields("INSPECTED_BY").DefinedSize
        txtAuthorisedBy.Maxlength = RsLayoutInspMain.Fields("AUTH_EMP").DefinedSize
        txtInspectionSTD.Maxlength = RsLayoutInspMain.Fields("AUTO_KEY_STD").DefinedSize

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
        If MODIFYMode = True And RsLayoutInspMain.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Report Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtPartNo.Text) = "" Then
            MsgInformation("Part No is empty, So unable to save.")
            txtPartNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        '    If Trim(txtSource.Text) = "" Then
        '        MsgInformation "Source Code is empty, So unable to save."
        '        If txtSource.Enabled = True Then txtSource.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        '    If Trim(txtPlanNo.Text) = "" Then
        '        MsgInformation "Layout Plan Number is empty, So unable to save."
        '        txtPlanNo.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        If Trim(txtProject.Text) = "" Then
            MsgInformation("Project Description is empty, So unable to save.")
            txtProject.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtInspectedBy.Text) = "" Then
            MsgInformation("Inspected By is empty, So unable to save.")
            txtInspectedBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColParameter, "S", "Please Check Parameter.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColSpecification, "S", "Please Check Specification.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColInspection, "S", "Please Check Inspection.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        Resume
    End Function

    Private Sub frmLayoutInsp_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsLayoutInspMain.Close()
        RsLayoutInspMain = Nothing
        RsLayoutInspDetail.Close()
        RsLayoutInspDetail = Nothing
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
        'On Error GoTo ErrPart
        'Dim xParamDesc As String
        '
        '    If NewRow = -1 Then Exit Sub
        '
        '
        '    SprdMain.Row = SprdMain.ActiveRow
        '    SprdMain.Col = ColParameter
        '    xParamDesc = Trim(SprdMain.Text)
        '    If xParamDesc = "" Then Exit Sub
        '
        '    Select Case eventArgs.Col
        '         Case ColParameter
        '
        '            SprdMain.Row = SprdMain.ActiveRow
        '
        '            SprdMain.Col = ColParameter
        '            xParamDesc = Trim(SprdMain.Text)
        '            If xParamDesc = "" Then Exit Sub
        '            MainClass.AddBlankSprdRow SprdMain, ColParameter, ConRowHeight
        '            FormatSprdMain SprdMain.MaxRows
        '    End Select
        '    Exit Sub
        'ErrPart:
        '    ErrorMsg err.Description, err.Number, vbCritical
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
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

        If Len(pTextBox.Text) < 6 Then
            pTextBox.Text = VB6.Format(pTextBox.Text, "000000")
        End If

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


    Private Sub txtAuthorisedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthorisedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAuthorisedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthorisedBy.DoubleClick
        Call cmdSearchAuthorised_Click(cmdSearchAuthorised, New System.EventArgs())
    End Sub

    Private Sub txtAuthorisedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAuthorisedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchAuthorised_Click(cmdSearchAuthorised, New System.EventArgs())
    End Sub

    Private Sub txtAuthorisedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAuthorisedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtAuthorisedBy, lblAuthorisedBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInspectedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspectedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInspectedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspectedBy.DoubleClick
        Call cmdSearchInspected_Click(cmdSearchInspected, New System.EventArgs())
    End Sub

    Private Sub txtInspectedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtInspectedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchInspected_Click(cmdSearchInspected, New System.EventArgs())
    End Sub

    Private Sub txtInspectedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInspectedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtInspectedBy, lblInspectedBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInspectionSTD_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspectionSTD.DoubleClick
        Call cmdSearchInspSTD_Click(cmdSearchInspSTD, New System.EventArgs())
    End Sub

    Private Sub txtInspectionSTD_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtInspectionSTD.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchInspSTD_Click(cmdSearchInspSTD, New System.EventArgs())
    End Sub

    Private Sub txtInspectionSTD_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInspectionSTD.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call FillInspectionSTD()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPartNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub


    Private Sub FillInspectionSTD()

        On Error GoTo FillERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtInspectionSTD.Text) = "" Then Exit Sub
        SqlStr = " SELECT QAL_INSPECTION_STD_HDR.ITEM_CODE,QAL_INSPECTION_STD_DET.PARAM_DESC, QAL_INSPECTION_STD_DET.SPECIFICATION, QAL_INSPECTION_STD_DET.INSP_MTH ,INV_ITEM_MST.ITEM_SHORT_DESC " & vbCrLf & " FROM QAL_INSPECTION_STD_HDR,QAL_INSPECTION_STD_DET,INV_ITEM_MST " & vbCrLf & " WHERE QAL_INSPECTION_STD_HDR.AUTO_KEY_STD=QAL_INSPECTION_STD_DET.AUTO_KEY_STD " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.COMPANY_CODE=INV_ITEM_MST.COMPANY_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.ITEM_CODE=INV_ITEM_MST.ITEM_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.INSP_TYPE='L' " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.AUTO_KEY_STD =" & Val(txtInspectionSTD.Text) & " ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1

            txtPartNo.Text = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
            lblPartNo.Text = Trim(IIf(IsDbNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value))

            Do While Not .EOF
                SprdMain.Row = I

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


    Private Sub txtPlanNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPlanNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPlanNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPlanNo.DoubleClick
        Call CmdSearchPlanNo_Click(CmdSearchPlanNo, New System.EventArgs())
    End Sub

    Private Sub txtPlanNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPlanNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchPlanNo_Click(CmdSearchPlanNo, New System.EventArgs())
    End Sub

    Private Sub txtPlanNo_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPlanNo.Leave
        '    If Trim(txtPlanNo.Text) = "" Then Exit Sub
        '    txtPartNo.SetFocus
    End Sub

    Private Sub txtPlanNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPlanNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtPlanNo.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT VW_LAYOUT_PLAN.SCHLD_NO,VW_LAYOUT_PLAN.DT, VW_LAYOUT_PLAN.SUPP_CUST_CODE,VW_LAYOUT_PLAN.PRODUCT_CODE,  " & vbCrLf _
                    & " FIN_SUPP_CUST_MST.SUPP_CUST_NAME,INV_ITEM_MST.ITEM_SHORT_DESC  " & vbCrLf _
                    & " FROM VW_LAYOUT_PLAN ,INV_ITEM_MST,FIN_SUPP_CUST_MST" & vbCrLf _
                    & " WHERE VW_LAYOUT_PLAN.COMPANY_CODE =INV_ITEM_MST.COMPANY_CODE   " & vbCrLf _
                    & " AND VW_LAYOUT_PLAN.COMPANY_CODE =FIN_SUPP_CUST_MST.COMPANY_CODE   " & vbCrLf _
                    & " AND VW_LAYOUT_PLAN.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND VW_LAYOUT_PLAN.SUPP_CUST_CODE =FIN_SUPP_CUST_MST.SUPP_CUST_CODE   " & vbCrLf _
                    & " AND VW_LAYOUT_PLAN.PRODUCT_CODE =INV_ITEM_MST.ITEM_CODE   " & vbCrLf _
                    & " AND VW_LAYOUT_PLAN.CAL_YEAR =" & RsCompany.fields("FYEAR").value & "" & vbCrLf _
                    & " AND VW_LAYOUT_PLAN.DT <= TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                    & " AND VW_LAYOUT_PLAN.ACDT IS NULL " & vbCrLf _
                    & " AND VW_LAYOUT_PLAN.SCHLD_NO =" & Val(txtPlanNo.Text) & " " & vbCrLf _
                    & " AND VW_LAYOUT_PLAN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                txtPlanNo.Text = IIf(IsDbNull(mRsTemp.Fields("SCHLD_NO").Value), "", .Fields("SCHLD_NO").Value)
                lblPlanDate.Text = IIf(IsDbNull(mRsTemp.Fields("DT").Value), "", .Fields("DT").Value)
                txtPartNo.Text = IIf(IsDbNull(mRsTemp.Fields("PRODUCT_CODE").Value), "", .Fields("PRODUCT_CODE").Value)
                lblPartNo.Text = IIf(IsDbNull(mRsTemp.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)
                txtSource.Text = IIf(IsDbNull(mRsTemp.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)
                lblSource.Text = IIf(IsDbNull(mRsTemp.Fields("SUPP_CUST_NAME").Value), "", .Fields("SUPP_CUST_NAME").Value)
            Else
                MsgBox("Not a valid Plan No.")
                lblPlanDate.Text = ""
                '            txtPartNo.Text = ""
                '            lblPartNo.text = ""
                txtSource.Text = ""
                lblSource.Text = ""
                Cancel = True
            End If
        End With
EventExitSub:
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

    Private Sub txtProject_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProject.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mItemCode As String
        Dim mSuppCode As String

        If Not RsLayoutInspMain.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsLayoutInspMain.Fields("AUTO_KEY_LAYOUT").Value), "", RsLayoutInspMain.Fields("AUTO_KEY_LAYOUT").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsLayoutInspMain.Fields("AUTO_KEY_LAYOUT").Value), "", RsLayoutInspMain.Fields("AUTO_KEY_LAYOUT").Value)
            txtDate.Text = IIf(IsDbNull(RsLayoutInspMain.Fields("INSP_DATE").Value), "", RsLayoutInspMain.Fields("INSP_DATE").Value)
            lblStage.Text = IIf(IsDbNull(RsLayoutInspMain.Fields("STAGE").Value), "", RsLayoutInspMain.Fields("STAGE").Value)
            txtInspectionSTD.Text = IIf(IsDbNull(RsLayoutInspMain.Fields("AUTO_KEY_STD").Value), "", RsLayoutInspMain.Fields("AUTO_KEY_STD").Value)
            If MainClass.ValidateWithMasterTable(RsLayoutInspMain.Fields("ITEM_CODE").Value, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblPartNo.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
            Else
                lblPartNo.Text = ""
            End If

            txtPlanNo.Text = IIf(IsDbNull(RsLayoutInspMain.Fields("AUTO_KEY_LIP").Value), "", RsLayoutInspMain.Fields("AUTO_KEY_LIP").Value)
            '        txtPlanNo_Validate False
            txtPartNo.Text = IIf(IsDbNull(RsLayoutInspMain.Fields("ITEM_CODE").Value), "", RsLayoutInspMain.Fields("ITEM_CODE").Value)
            '        txtPartNo_Validate False
            txtSource.Text = IIf(IsDbNull(RsLayoutInspMain.Fields("SUPP_CUST_CODE").Value), "", RsLayoutInspMain.Fields("SUPP_CUST_CODE").Value)
            txtSource_Validating(txtSource, New System.ComponentModel.CancelEventArgs(False))
            txtProject.Text = IIf(IsDbNull(RsLayoutInspMain.Fields("PROJ_DESC").Value), "", RsLayoutInspMain.Fields("PROJ_DESC").Value)
            txtRemarks.Text = IIf(IsDbNull(RsLayoutInspMain.Fields("REMARKS").Value), "", RsLayoutInspMain.Fields("REMARKS").Value)
            txtInspectedBy.Text = IIf(IsDbNull(RsLayoutInspMain.Fields("INSPECTED_BY").Value), "", RsLayoutInspMain.Fields("INSPECTED_BY").Value)
            txtInspectedBy_Validating(txtInspectedBy, New System.ComponentModel.CancelEventArgs(False))
            txtAuthorisedBy.Text = IIf(IsDbNull(RsLayoutInspMain.Fields("AUTH_EMP").Value), "", RsLayoutInspMain.Fields("AUTH_EMP").Value)
            txtAuthorisedBy_Validating(txtAuthorisedBy, New System.ComponentModel.CancelEventArgs(False))
            Call MakeEnableDesableField(False)
            Call ShowDetail1()

            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsLayoutInspMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        '    FormatSprdMain -1
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_LAYOUT_DET " & vbCrLf & " WHERE AUTO_KEY_LAYOUT=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLayoutInspDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsLayoutInspDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                FormatSprdMain(I)

                SprdMain.Col = ColParameter
                SprdMain.Text = IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value)

                SprdMain.Col = ColSpecification
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPECIFICATION").Value), "", .Fields("SPECIFICATION").Value))

                SprdMain.Col = ColInspection
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("INSP_MTH").Value), "", .Fields("INSP_MTH").Value))

                SprdMain.Col = ColObserv1
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_1").Value), "", .Fields("OBSERV_1").Value))

                SprdMain.Col = ColObserv2
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_2").Value), "", .Fields("OBSERV_2").Value))

                SprdMain.Col = ColObserv3
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_3").Value), "", .Fields("OBSERV_3").Value))

                SprdMain.Col = ColObserv4
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_4").Value), "", .Fields("OBSERV_4").Value))

                SprdMain.Col = ColObserv5
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_5").Value), "", .Fields("OBSERV_5").Value))

                SprdMain.Col = ColRemarks
                SprdMain.Text = VB.Left(Trim(IIf(IsDbNull(.Fields("REMARKS").Value), " ", .Fields("REMARKS").Value)), 1)


                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        '    Resume
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
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String


        If Trim(txtSlipNo.Text) = "" Then GoTo EventExitSub
        If Len(Trim(txtSlipNo.Text)) < 6 Then
            txtSlipNo.Text = Trim(txtSlipNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mSlipNo = Val(txtSlipNo.Text)

        If MODIFYMode = True And RsLayoutInspMain.BOF = False Then xMKey = RsLayoutInspMain.Fields("AUTO_KEY_LAYOUT").Value

        SqlStr = "SELECT * FROM QAL_LAYOUT_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_LAYOUT,LENGTH(AUTO_KEY_LAYOUT)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_LAYOUT=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLayoutInspMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsLayoutInspMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_LAYOUT_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_LAYOUT,LENGTH(AUTO_KEY_LAYOUT)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_LAYOUT=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLayoutInspMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        txtPlanNo.Enabled = mMode
        CmdSearchPlanNo.Enabled = mMode
        txtPartNo.Enabled = False 'mMode
        txtSource.Enabled = True '' mMode
        txtInspectedBy.Enabled = mMode
        cmdSearchInspected.Enabled = mMode
        txtInspectionSTD.Enabled = mMode

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
    Private Sub ReportOnLayoutInsp(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "INSPECTION REPORT OF LAYOUT INSPECTION"
        SqlStr = "SELECT QAL_LAYOUT_HDR.*,QAL_LAYOUT_DET.*, " & vbCrLf & " INV_ITEM_MST.*,FIN_SUPP_CUST_MST.*, " & vbCrLf & " PAY_EMPLOYEE_MST.EMP_NAME,EMP2.EMP_NAME " & vbCrLf & " FROM QAL_LAYOUT_HDR,QAL_LAYOUT_DET,  " & vbCrLf & " INV_ITEM_MST,FIN_SUPP_CUST_MST,PAY_EMPLOYEE_MST ,PAY_EMPLOYEE_MST EMP2 " & vbCrLf & " WHERE QAL_LAYOUT_HDR.AUTO_KEY_LAYOUT=QAL_LAYOUT_DET.AUTO_KEY_LAYOUT " & vbCrLf & " AND QAL_LAYOUT_HDR.COMPANY_CODE=INV_ITEM_MST.COMPANY_CODE(+) " & vbCrLf & " AND QAL_LAYOUT_HDR.ITEM_CODE=INV_ITEM_MST.ITEM_CODE(+) " & vbCrLf & " AND QAL_LAYOUT_HDR.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE(+) " & vbCrLf & " AND QAL_LAYOUT_HDR.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE(+) " & vbCrLf & " AND QAL_LAYOUT_HDR.COMPANY_CODE=PAY_EMPLOYEE_MST.COMPANY_CODE (+) " & vbCrLf & " AND QAL_LAYOUT_HDR.INSPECTED_BY=PAY_EMPLOYEE_MST.EMP_CODE (+) " & vbCrLf & " AND QAL_LAYOUT_HDR.COMPANY_CODE=EMP2.COMPANY_CODE (+) " & vbCrLf & " AND QAL_LAYOUT_HDR.AUTH_EMP=EMP2.EMP_CODE (+) " & vbCrLf & " AND QAL_LAYOUT_HDR.AUTO_KEY_LAYOUT=" & Val(lblMkey.Text) & " ORDER BY SERIAL_NO "

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\InspecRepLayout.rpt"

        SetCrpt(Report1, Mode, 1, mTitle, , True, xMenuID)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnLayoutInsp(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnLayoutInsp(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtSource_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSource.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSource_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSource.DoubleClick
        Call SearchSource()
    End Sub

    Private Sub txtSource_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSource.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchSource()
    End Sub

    Private Sub txtSource_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSource.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String

        If Trim(txtSource.Text) = "" Then lblSource.Text = "" : GoTo EventExitSub


        If MainClass.ValidateWithMasterTable(Trim(txtSource.Text), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgBox("Customer Not Exist In Master.")
            Cancel = False
        Else
            lblSource.text = MasterNo
        End If
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SearchSource()
        On Error GoTo SrchERR
        Dim SqlStr As String

        If MainClass.SearchGridMaster(Trim(txtSource.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            txtSource.Text = AcName1
            lblSource.text = AcName
            If txtSource.Enabled = True Then txtSource.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub
End Class
