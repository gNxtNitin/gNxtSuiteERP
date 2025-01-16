Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmControlPlan
    Inherits System.Windows.Forms.Form
    Dim RsControlPlanMain As ADODB.Recordset
    Dim RsControlPlanDetail As ADODB.Recordset
    Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean
    Private Const ConRowHeight As Short = 14

    Private Const ColStage As Short = 1
    Private Const ColStageDesc As Short = 2
    Private Const ColMachine As Short = 3
    Private Const ColMachineDesc As Short = 4
    Private Const ColProduct As Short = 5
    Private Const ColProcess As Short = 6
    Private Const ColClass As Short = 7
    Private Const ColSpecification As Short = 8
    Private Const ColInspection As Short = 9
    Private Const ColSampleSize As Short = 10
    Private Const ColSampleFreq As Short = 11
    Private Const ColControlMeth As Short = 12
    Private Const ColReaction As Short = 13


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
            If RsControlPlanMain.EOF = False Then RsControlPlanMain.MoveFirst()
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
        If Not RsControlPlanMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_CONTROLPLAN_HDR", (txtSlipNo.Text), RsControlPlanMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "QAL_CONTROLPLAN_HDR", "AUTO_KEY_CTRLPLAN", (lblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM QAL_CONTROLPLAN_DET WHERE AUTO_KEY_CTRLPLAN=" & Val(lblMKey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_CONTROLPLAN_HDR WHERE AUTO_KEY_CTRLPLAN=" & Val(lblMKey.Text) & "")
                PubDBCn.CommitTrans()
                RsControlPlanMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        RsControlPlanMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsControlPlanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

        SqlStr = " SELECT AUTO_KEY_CTRLPLAN " & vbCrLf _
                & " From QAL_CONTROLPLAN_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE = " & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND LTRIM(RTRIM(PRODUCT_CODE)) ='" & MainClass.AllowSingleQuote(UCase(txtPartNo.Text)) & "' "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_CTRLPLAN").Value)
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
        Dim mPlanFlag As String
        Dim mSlipNo As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""
        If optPlanFlag(0).Checked = True Then
            mPlanFlag = "0"
        ElseIf optPlanFlag(1).Checked = True Then
            mPlanFlag = "1"
        ElseIf optPlanFlag(2).Checked = True Then
            mPlanFlag = "2"
        End If

        mSlipNo = Val(txtSlipNo.Text)
        If Val(txtSlipNo.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtSlipNo.Text = CStr(mSlipNo)
        If ADDMode = True Then
            lblMKey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_CONTROLPLAN_HDR " & vbCrLf _
                            & " (AUTO_KEY_CTRLPLAN,COMPANY_CODE," & vbCrLf _
                            & " PLAN_DATE,PLAN_FLAG,PRODUCT_CODE,SUPPLIER_CODE,SUPP_PLANT_APP_DATE, " & vbCrLf _
                            & " KEY_CONT_DETAIL,CORE_TEAM,OTH_APP_DATE,CUST_QAL_APP_ORG_DATE,CUST_QAL_APP_REV_DATE, " & vbCrLf _
                            & " ORG_DATE,REV_DATE,OTH_APP_DATE1," & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mPlanFlag & "','" & MainClass.AllowSingleQuote(txtPartNo.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtSuppCode.Text) & "',TO_DATE('" & VB6.Format(txtPlanAppDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtKet.Text) & "','" & MainClass.AllowSingleQuote(txtCoreTeam.Text) & "', " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(txtOtherAppDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtCustAppDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(txtCustAppDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateOrig.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(txtDateRev.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtIfOtherAppDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_CONTROLPLAN_HDR SET " & vbCrLf _
                    & " AUTO_KEY_CTRLPLAN=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " PLAN_DATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),PLAN_FLAG='" & mPlanFlag & "', " & vbCrLf _
                    & " PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "', " & vbCrLf _
                    & " SUPPLIER_CODE='" & MainClass.AllowSingleQuote(txtSuppCode.Text) & "', " & vbCrLf _
                    & " SUPP_PLANT_APP_DATE=TO_DATE('" & VB6.Format(txtPlanAppDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " KEY_CONT_DETAIL='" & MainClass.AllowSingleQuote(txtKet.Text) & "', " & vbCrLf _
                    & " CORE_TEAM='" & MainClass.AllowSingleQuote(txtCoreTeam.Text) & "', " & vbCrLf _
                    & " OTH_APP_DATE=TO_DATE('" & VB6.Format(txtOtherAppDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " CUST_QAL_APP_ORG_DATE=TO_DATE('" & VB6.Format(txtCustAppDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " CUST_QAL_APP_REV_DATE=TO_DATE('" & VB6.Format(txtCustAppDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " ORG_DATE=TO_DATE('" & VB6.Format(txtDateOrig.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " REV_DATE=TO_DATE('" & VB6.Format(txtDateRev.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " OTH_APP_DATE1=TO_DATE('" & VB6.Format(txtIfOtherAppDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND AUTO_KEY_CTRLPLAN =" & Val(lblMkey.Text) & ""
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
        RsControlPlanMain.Requery()
        RsControlPlanDetail.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_CTRLPLAN)  " & vbCrLf & " FROM QAL_CONTROLPLAN_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CTRLPLAN,LENGTH(AUTO_KEY_CTRLPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & " "

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
        Dim mStage As String
        Dim mStageDesc As String
        Dim mMachine As String
        Dim mMachineDesc As String
        Dim mProduct As String
        Dim mProcess As String
        Dim mClass As String
        Dim mSpecification As String
        Dim mInspection As String
        Dim mSampleSize As String
        Dim mSampleFreq As String
        Dim mControlMeth As String
        Dim mReaction As String

        PubDBCn.Execute("DELETE FROM QAL_CONTROLPLAN_DET WHERE AUTO_KEY_CTRLPLAN=" & Val(lblMKey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                SprdMain.Col = ColStage
                mStage = MainClass.AllowSingleQuote(.Text)

                SprdMain.Col = ColMachine
                mMachine = MainClass.AllowSingleQuote(.Text)

                SprdMain.Col = ColProduct
                mProduct = MainClass.AllowSingleQuote(.Text)

                SprdMain.Col = ColProcess
                mProcess = MainClass.AllowSingleQuote(.Text)

                SprdMain.Col = ColClass
                Select Case Trim(.Text)
                    Case "Minor"
                        mClass = "Mi"
                    Case "Major"
                        mClass = "Ma"
                    Case "Critical"
                        mClass = "Cr"
                    Case Else
                        mClass = ""
                End Select

                SprdMain.Col = ColSpecification
                mSpecification = MainClass.AllowSingleQuote(.Text)

                SprdMain.Col = ColInspection
                mInspection = MainClass.AllowSingleQuote(.Text)

                SprdMain.Col = ColSampleSize
                mSampleSize = MainClass.AllowSingleQuote(.Text)

                SprdMain.Col = ColSampleFreq
                mSampleFreq = MainClass.AllowSingleQuote(.Text)

                SprdMain.Col = ColControlMeth
                mControlMeth = MainClass.AllowSingleQuote(.Text)

                SprdMain.Col = ColReaction
                mReaction = MainClass.AllowSingleQuote(.Text)


                SqlStr = ""

                If mStage <> "" And mMachine <> "" Then
                    SqlStr = " INSERT INTO  QAL_CONTROLPLAN_DET ( " & vbCrLf & " AUTO_KEY_CTRLPLAN,SERIAL_NO,OPR_CODE,MACHINE_NO,PROD_DESC,PROC_DESC, " & vbCrLf & " SPL_CHAR_CLASS,SPECIFICATION,INSP_METHOD,SAMPLE_SIZE,SAMPLE_FREQ,CONTROL_METHOD,REACTION_PLAN ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMKey.Text) & "," & I & ",'" & mStage & "','" & mMachine & "','" & mProduct & "','" & mProcess & "', " & vbCrLf & " '" & mClass & "','" & mSpecification & "','" & mInspection & "','" & mSampleSize & "','" & mSampleFreq & "','" & mControlMeth & "','" & mReaction & "') "
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





    Private Sub cmdSearchPartNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPartNo.Click
        Dim SqlStr As String

        SqlStr = " SELECT INV_ITEM_MST.ITEM_CODE, INV_ITEM_MST.ITEM_SHORT_DESC " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY ITEM_SHORT_DESC"
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtPartNo.Text = AcName
            lblPartNo.text = AcName1
            If txtPartNo.Enabled = True Then txtPartNo.Focus()
        End If
    End Sub


    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CTRLPLAN,LENGTH(AUTO_KEY_CTRLPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_CONTROLPLAN_HDR", "AUTO_KEY_CTRLPLAN", "PLAN_DATE", "PRODUCT_CODE", "SUPPLIER_CODE", SqlStr) = True Then
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
        MainClass.ButtonStatus(Me, XRIGHT, RsControlPlanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmControlPlan_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Control Plan"

        SqlStr = "Select * From QAL_CONTROLPLAN_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsControlPlanMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_CONTROLPLAN_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsControlPlanDetail, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)
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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_CTRLPLAN AS SLIP_NUMBER,TO_CHAR(PLAN_DATE,'DD/MM/YYYY') AS PLAN_DATE, " & vbCrLf & " PRODUCT_CODE,SUPPLIER_CODE " & vbCrLf & " FROM QAL_CONTROLPLAN_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CTRLPLAN,LENGTH(AUTO_KEY_CTRLPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_CTRLPLAN"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmControlPlan_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmControlPlan_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        lblMKey.Text = ""
        txtSlipNo.Text = ""
        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        optPlanFlag(0).Checked = True
        txtPartNo.Text = ""
        lblPartNo.Text = ""
        txtSuppCode.Text = ""
        txtPlanAppDate.Text = ""
        txtKet.Text = ""
        txtCoreTeam.Text = ""
        txtOtherAppDate.Text = ""
        txtCustAppDate1.Text = ""
        txtDateOrig.Text = ""
        txtCustAppDate2.Text = ""
        txtDateRev.Text = ""
        txtIfOtherAppDate.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsControlPlanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

            .Col = ColStage
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsControlPlanDetail.Fields("OPR_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColStageDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColMachine
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsControlPlanDetail.Fields("MACHINE_NO").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColMachineDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColProduct
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsControlPlanDetail.Fields("PROD_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColProcess
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsControlPlanDetail.Fields("PROC_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColClass
            .CellType = SS_CELL_TYPE_COMBOBOX
            .TypeComboBoxList = "Minor" & Chr(9) & "Major" & Chr(9) & "Critical" & Chr(9) & " "
            .TypeComboBoxCurSel = 3

            .Col = ColSpecification
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsControlPlanDetail.Fields("SPECIFICATION").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColInspection
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsControlPlanDetail.Fields("INSP_METHOD").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColSampleSize
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsControlPlanDetail.Fields("SAMPLE_SIZE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColSampleFreq
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsControlPlanDetail.Fields("SAMPLE_FREQ").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColControlMeth
            .CellType = SS_CELL_TYPE_COMBOBOX
            .TypeComboBoxList = "QAD-F-14" & Chr(9) & "QAD-F-25" & Chr(9) & "QAD-F-26A" & Chr(9) & "QAD-F-26B" & Chr(9) & " "
            .TypeComboBoxCurSel = 4

            .Col = ColReaction
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsControlPlanDetail.Fields("REACTION_PLAN").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColStageDesc, ColStageDesc)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColMachineDesc, ColMachineDesc)
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
            .set_ColWidth(4, 500 * 5)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.Maxlength = RsControlPlanMain.Fields("AUTO_KEY_CTRLPLAN").Precision
        txtDate.Maxlength = RsControlPlanMain.Fields("PLAN_DATE").DefinedSize - 6
        txtPartNo.Maxlength = RsControlPlanMain.Fields("PRODUCT_CODE").DefinedSize
        txtSuppCode.Maxlength = RsControlPlanMain.Fields("SUPPLIER_CODE").DefinedSize
        txtPlanAppDate.Maxlength = RsControlPlanMain.Fields("SUPP_PLANT_APP_DATE").DefinedSize - 6
        txtKet.Maxlength = RsControlPlanMain.Fields("KEY_CONT_DETAIL").DefinedSize
        txtCoreTeam.Maxlength = RsControlPlanMain.Fields("CORE_TEAM").DefinedSize
        txtOtherAppDate.Maxlength = RsControlPlanMain.Fields("OTH_APP_DATE").DefinedSize - 6
        txtCustAppDate1.Maxlength = RsControlPlanMain.Fields("CUST_QAL_APP_ORG_DATE").DefinedSize - 6
        txtCustAppDate2.Maxlength = RsControlPlanMain.Fields("CUST_QAL_APP_REV_DATE").DefinedSize - 6
        txtDateOrig.Maxlength = RsControlPlanMain.Fields("ORG_DATE").DefinedSize - 6
        txtDateRev.Maxlength = RsControlPlanMain.Fields("REV_DATE").DefinedSize - 6
        txtIfOtherAppDate.Maxlength = RsControlPlanMain.Fields("OTH_APP_DATE1").DefinedSize - 6
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
        If MODIFYMode = True And RsControlPlanMain.EOF = True Then Exit Function

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
        If Trim(txtSuppCode.Text) = "" Then
            MsgInformation("Supplier Code is empty, So unable to save.")
            txtSuppCode.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtPlanAppDate.Text) = "" Then
            MsgInformation("Plant Approval Date Code is empty, So unable to save.")
            txtPlanAppDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtKet.Text) = "" Then
            MsgInformation("Key Contact Details is empty, So unable to save.")
            txtKet.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtCoreTeam.Text) = "" Then
            MsgInformation("Core Team Details is empty, So unable to save.")
            txtCoreTeam.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtDateOrig.Text) = "" Then
            MsgInformation("Document Original Date is empty, So unable to save.")
            txtDateOrig.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColStage, "S", "Please Check Stage.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColMachine, "S", "Please Check Machine.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmControlPlan_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsControlPlanMain.Close()
        RsControlPlanMain = Nothing
        RsControlPlanDetail.Close()
        RsControlPlanDetail = Nothing
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

        If eventArgs.Row = 0 And eventArgs.Col = ColStage Then
            With SprdMain
                .Row = .ActiveRow

                SqlStr = " SELECT A.OPR_CODE, B.OPR_DESC " & vbCrLf _
                                & " FROM MKT_BOMOPERATIONS_DET A, PRD_OPR_MST B " & vbCrLf _
                                & " WHERE B.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                                & " AND B.OPR_CODE = A.OPR_CODE " & vbCrLf _
                                & " AND TO_NUMBER(SUBSTR(AUTO_KEY_BOM,-2)) =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                                & " AND LTRIM(RTRIM(A.PRODUCT_CODE)) ='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "' "

                .Col = ColStage
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColStage
                    .Text = Trim(AcName)

                    .Col = ColStageDesc
                    .Text = Trim(AcName1)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColStage, .ActiveRow, ColStage, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColStageDesc Then
            With SprdMain
                .Row = .ActiveRow

                SqlStr = " SELECT B.OPR_DESC,A.OPR_CODE  " & vbCrLf _
                                & " FROM MKT_BOMOPERATIONS_DET A, PRD_OPR_MST B " & vbCrLf _
                                & " WHERE B.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                                & " AND B.OPR_CODE = A.OPR_CODE " & vbCrLf _
                                & " AND TO_NUMBER(SUBSTR(AUTO_KEY_BOM,-2)) =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                                & " AND LTRIM(RTRIM(A.PRODUCT_CODE)) ='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "' "

                If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColStage
                    .Text = Trim(AcName1)

                    .Col = ColStageDesc
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColStage, .ActiveRow, ColStage, .ActiveRow, False))
            End With
        End If
        If eventArgs.row = 0 And eventArgs.col = ColMachine Then
            With SprdMain
                .Row = .ActiveRow

                SqlStr = " SELECT MAN_MACHINE_MST.MACHINE_NO, MAN_MACHINE_MST.MACHINE_DESC " & vbCrLf & " From MAN_MACHINE_MST " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "

                .Col = ColMachine
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColMachine
                    .Text = Trim(AcName)

                    .Col = ColMachineDesc
                    .Text = Trim(AcName1)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColMachine, .ActiveRow, ColMachine, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColMachineDesc Then
            With SprdMain
                .Row = .ActiveRow

                SqlStr = " SELECT MAN_MACHINE_MST.MACHINE_DESC,MAN_MACHINE_MST.MACHINE_NO " & vbCrLf & " From MAN_MACHINE_MST " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "

                If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColMachine
                    .Text = Trim(AcName1)

                    .Col = ColMachineDesc
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColMachine, .ActiveRow, ColMachine, .ActiveRow, False))
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColStage)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColStage Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColStage, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColStageDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColStageDesc, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColMachine Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColMachine, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColMachineDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColMachineDesc, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xStage As String
        Dim xMachine As String

        If eventArgs.NewRow = -1 Then Exit Sub


        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColStage
        xStage = Trim(SprdMain.Text)
        If xStage = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColStage
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColStage
                xStage = Trim(SprdMain.Text)
                If xStage = "" Then Exit Sub
                Call CheckStage()

            Case ColMachine
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColMachine
                xMachine = Trim(SprdMain.Text)
                If xMachine = "" Then Exit Sub
                If CheckMachine() = True Then
                    MainClass.AddBlankSprdRow(SprdMain, ColStage, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckDate(ByRef pText As System.Windows.Forms.TextBox) As Boolean
        CheckDate = True
        If pText.Text = "" Then Exit Function
        If Not IsDate(pText.Text) Then
            MsgBox("Not a valid date.")
            CheckDate = False
        End If
    End Function

    Private Function CheckStage() As Boolean

        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String

        On Error GoTo CheckERR
        With SprdMain
            .Row = .ActiveRow
            .Col = ColStage

            SqlStr = " SELECT A.OPR_CODE, B.OPR_DESC " & vbCrLf _
                        & " FROM MKT_BOMOPERATIONS_DET A, PRD_OPR_MST B " & vbCrLf _
                        & " WHERE B.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                        & " AND B.OPR_CODE = A.OPR_CODE " & vbCrLf _
                        & " AND TO_NUMBER(SUBSTR(AUTO_KEY_BOM,-2)) =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                        & " AND LTRIM(RTRIM(A.PRODUCT_CODE)) ='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "' " & vbCrLf _
                        & " AND LTRIM(RTRIM(A.OPR_CODE)) ='" & MainClass.AllowSingleQuote(.Text) & "' "


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsTemp.EOF Then
                .Col = ColStageDesc
                .Text = IIf(IsDbNull(RsTemp.Fields("OPR_DESC").Value), "", RsTemp.Fields("OPR_DESC").Value)
                CheckStage = True
            Else
                .Col = ColStageDesc
                .Text = ""
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColStage)
            End If
        End With
        Exit Function
CheckERR:
        MsgBox(Err.Description)
    End Function
    Private Function CheckMachine() As Boolean

        On Error GoTo CheckERR
        With SprdMain
            .Row = .ActiveRow
            .Col = ColMachine
            If MainClass.ValidateWithMasterTable(.Text, "MACHINE_NO", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ") = True Then
                .Col = ColMachineDesc
                .Text = MasterNo
                CheckMachine = True
            Else
                .Col = ColMachineDesc
                .Text = ""
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColMachine)
            End If
        End With
        Exit Function
CheckERR:
        MsgBox(Err.Description)
    End Function

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


    Private Sub txtCoreTeam_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCoreTeam.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustAppDate1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustAppDate1.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustAppDate1_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustAppDate1.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtCustAppDate1) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCustAppDate2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustAppDate2.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustAppDate2_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustAppDate2.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtCustAppDate2) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDateOrig_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateOrig.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDateOrig_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateOrig.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtDateOrig) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDateRev_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateRev.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDateRev_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateRev.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtDateRev) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtIfOtherAppDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIfOtherAppDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIfOtherAppDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIfOtherAppDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtIfOtherAppDate) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtKet_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtKet.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOtherAppDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOtherAppDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOtherAppDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOtherAppDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtOtherAppDate) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPartNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPartNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartNo.DoubleClick
        Call cmdSearchPartNo_Click(cmdSearchPartNo, New System.EventArgs())
    End Sub

    Private Sub txtPartNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchPartNo_Click(cmdSearchPartNo, New System.EventArgs())
    End Sub

    Private Sub txtPartNo_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartNo.Leave
        If Trim(txtPartNo.Text) = "" Then Exit Sub

    End Sub

    Private Sub txtPartNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtPartNo.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT ITEM_SHORT_DESC " & vbCrLf _
                & " FROM INV_ITEM_MST " & vbCrLf _
                & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND LTRIM(RTRIM(ITEM_CODE)) = '" & MainClass.AllowSingleQuote(txtPartNo.Text) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                lblPartNo.Text = IIf(IsDbNull(mRsTemp.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)
            Else
                MsgBox("Not a valid Part No.")
                lblPartNo.Text = ""
                Cancel = True
            End If
        End With


EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub



    Private Sub txtPlanAppDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPlanAppDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPlanAppDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPlanAppDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtPlanAppDate) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSuppCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuppCode.TextChanged

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


    Private Sub txtSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart

        If Not RsControlPlanMain.EOF Then
            IsShowing = True
            lblMKey.Text = IIf(IsDbNull(RsControlPlanMain.Fields("AUTO_KEY_CTRLPLAN").Value), "", RsControlPlanMain.Fields("AUTO_KEY_CTRLPLAN").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsControlPlanMain.Fields("AUTO_KEY_CTRLPLAN").Value), "", RsControlPlanMain.Fields("AUTO_KEY_CTRLPLAN").Value)
            txtDate.Text = IIf(IsDbNull(RsControlPlanMain.Fields("PLAN_DATE").Value), "", RsControlPlanMain.Fields("PLAN_DATE").Value)
            Select Case IIf(IsDbNull(RsControlPlanMain.Fields("PLAN_FLAG").Value), "1", RsControlPlanMain.Fields("PLAN_FLAG").Value)
                Case "0"
                    optPlanFlag(0).Checked = True
                Case "1"
                    optPlanFlag(1).Checked = True
                Case "2"
                    optPlanFlag(2).Checked = True
            End Select

            txtPartNo.Text = IIf(IsDbNull(RsControlPlanMain.Fields("PRODUCT_CODE").Value), "", RsControlPlanMain.Fields("PRODUCT_CODE").Value)
            txtPartNo_Validating(txtPartNo, New System.ComponentModel.CancelEventArgs(False))
            txtSuppCode.Text = IIf(IsDbNull(RsControlPlanMain.Fields("SUPPLIER_CODE").Value), "", RsControlPlanMain.Fields("SUPPLIER_CODE").Value)
            txtPlanAppDate.Text = IIf(IsDbNull(RsControlPlanMain.Fields("SUPP_PLANT_APP_DATE").Value), "", RsControlPlanMain.Fields("SUPP_PLANT_APP_DATE").Value)
            txtKet.Text = IIf(IsDbNull(RsControlPlanMain.Fields("KEY_CONT_DETAIL").Value), "", RsControlPlanMain.Fields("KEY_CONT_DETAIL").Value)
            txtCoreTeam.Text = IIf(IsDbNull(RsControlPlanMain.Fields("CORE_TEAM").Value), "", RsControlPlanMain.Fields("CORE_TEAM").Value)
            txtOtherAppDate.Text = IIf(IsDbNull(RsControlPlanMain.Fields("OTH_APP_DATE").Value), "", RsControlPlanMain.Fields("OTH_APP_DATE").Value)
            txtCustAppDate1.Text = IIf(IsDbNull(RsControlPlanMain.Fields("CUST_QAL_APP_ORG_DATE").Value), "", RsControlPlanMain.Fields("CUST_QAL_APP_ORG_DATE").Value)
            txtCustAppDate2.Text = IIf(IsDbNull(RsControlPlanMain.Fields("CUST_QAL_APP_REV_DATE").Value), "", RsControlPlanMain.Fields("CUST_QAL_APP_REV_DATE").Value)
            txtDateOrig.Text = IIf(IsDbNull(RsControlPlanMain.Fields("ORG_DATE").Value), "", RsControlPlanMain.Fields("ORG_DATE").Value)
            txtDateRev.Text = IIf(IsDbNull(RsControlPlanMain.Fields("REV_DATE").Value), "", RsControlPlanMain.Fields("REV_DATE").Value)
            txtIfOtherAppDate.Text = IIf(IsDbNull(RsControlPlanMain.Fields("OTH_APP_DATE1").Value), "", RsControlPlanMain.Fields("OTH_APP_DATE1").Value)
            Call ShowDetail1()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsControlPlanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim mStage As String
        Dim mMachine As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_CONTROLPLAN_DET " & vbCrLf & " WHERE AUTO_KEY_CTRLPLAN=" & Val(lblMKey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsControlPlanDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsControlPlanDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColStage
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OPR_CODE").Value), "", .Fields("OPR_CODE").Value))
                mStage = SprdMain.Text

                SprdMain.Col = ColStageDesc
                If MainClass.ValidateWithMasterTable(mStage, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ") = True Then
                    SprdMain.Text = MasterNo
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColMachine
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("MACHINE_NO").Value), "", .Fields("MACHINE_NO").Value))
                mMachine = SprdMain.Text

                SprdMain.Col = ColMachineDesc
                If MainClass.ValidateWithMasterTable(mMachine, "MACHINE_NO", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ") = True Then
                    SprdMain.Text = MasterNo
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColProduct
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PROD_DESC").Value), "", .Fields("PROD_DESC").Value))

                SprdMain.Col = ColProcess
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PROC_DESC").Value), "", .Fields("PROC_DESC").Value))

                SprdMain.Col = ColClass
                Select Case Trim(IIf(IsDbNull(.Fields("SPL_CHAR_CLASS").Value), "", .Fields("SPL_CHAR_CLASS").Value))
                    Case "Mi"
                        SprdMain.Text = "Minor"
                    Case "Ma"
                        SprdMain.Text = "Major"
                    Case "Cr"
                        SprdMain.Text = "Critical"
                    Case Else
                        SprdMain.Text = " "
                End Select

                SprdMain.Col = ColSpecification
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPECIFICATION").Value), "", .Fields("SPECIFICATION").Value))

                SprdMain.Col = ColInspection
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("INSP_METHOD").Value), "", .Fields("INSP_METHOD").Value))

                SprdMain.Col = ColSampleSize
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SAMPLE_SIZE").Value), "", .Fields("SAMPLE_SIZE").Value))

                SprdMain.Col = ColSampleFreq
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SAMPLE_FREQ").Value), "", .Fields("SAMPLE_FREQ").Value))

                SprdMain.Col = ColControlMeth
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("CONTROL_METHOD").Value), "", .Fields("CONTROL_METHOD").Value))

                SprdMain.Col = ColReaction
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("REACTION_PLAN").Value), "", .Fields("REACTION_PLAN").Value))

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

        If MODIFYMode = True And RsControlPlanMain.BOF = False Then xMkey = RsControlPlanMain.Fields("AUTO_KEY_CTRLPLAN").Value

        SqlStr = "SELECT * FROM QAL_CONTROLPLAN_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CTRLPLAN,LENGTH(AUTO_KEY_CTRLPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_CTRLPLAN=" & mSlipNo & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsControlPlanMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsControlPlanMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_CONTROLPLAN_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CTRLPLAN,LENGTH(AUTO_KEY_CTRLPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_CTRLPLAN=" & Val(CStr(xMkey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsControlPlanMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        optPlanFlag(0).Enabled = mMode
        optPlanFlag(1).Enabled = mMode
        optPlanFlag(2).Enabled = mMode
        txtDate.Enabled = mMode
        txtPartNo.Enabled = mMode
        cmdSearchPartNo.Enabled = mMode

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
    Private Sub ReportOnControlPlan(ByRef Mode As Crystal.DestinationConstants)

    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnControlPlan(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnControlPlan(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
