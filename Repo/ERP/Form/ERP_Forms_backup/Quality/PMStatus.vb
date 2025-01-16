Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPMStatus
    Inherits System.Windows.Forms.Form
    Dim RsMachinePMHdr As ADODB.Recordset
    Dim RsMachinePMDet As ADODB.Recordset
    Dim RsMachinePMItem As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean
    Private Const ConRowHeight As Short = 14

    Dim xColorOrig As String

    Private Const ColCategory As Short = 1
    Private Const ColCheckPoint As Short = 2
    Private Const ColRequirment As Short = 3
    Private Const ColCheckMethod As Short = 4
    Private Const ColObservation As Short = 5
    Private Const ColActionPlan As Short = 6
    Private Const ColRemarks As Short = 7
    Private Const ColStatus As Short = 8

    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColStockQty As Short = 3
    Private Const ColUom As Short = 4
    Private Const ColQty As Short = 5
    Private Const ColRate As Short = 6
    Private Const ColAmount As Short = 7
    Private Const ColSavedItemCode As Short = 8
    Private Const ColSavedQty As Short = 9

    Dim CurrFormHeight As Integer
    Dim CurrFormWidth As Integer


    Public Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtSlipNo.Enabled = False
            cmdSearchSlipNo.Enabled = False
            SprdMain.Enabled = True
            SprdItem.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsMachinePMHdr.EOF = False Then RsMachinePMHdr.MoveFirst()
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
        If Not RsMachinePMHdr.EOF Then
            If RsMachinePMHdr.Fields("APP_BY").Value <> "" Then MsgBox("Number been approved, So cann't be Deleted ") : Exit Sub
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "MAN_MACHINE_PM_HDR", (txtSlipNo.Text), RsMachinePMHdr) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM MAN_MACHINE_PM_ITEM WHERE AUTO_KEY_PM=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM MAN_MACHINE_PM_DET WHERE AUTO_KEY_PM=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM MAN_MACHINE_PM_HDR WHERE AUTO_KEY_PM=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsMachinePMHdr.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsMachinePMHdr.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            If PubSuperUser = "U" Then
                If RsMachinePMHdr.Fields("APP_BY").Value <> "" Then MsgBox("Number been approved, So cann't be Modified ") : Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsMachinePMHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtSlipNo.Enabled = False
            cmdSearchSlipNo.Enabled = False
            '        txtMachineNo.Enabled = False
            '        cmdSearchMachineNo.Enabled = False
            Call MakeEnableDesableField(True)
            SprdMain.Enabled = True
            SprdItem.Enabled = True
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
        SqlStr = " SELECT AUTO_KEY_PM " & vbCrLf _
            & " From MAN_MACHINE_PM_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND PM_DATE =TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND MACHINE_NO ='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' " & vbCrLf _
            & " AND CHECK_TYPE ='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_PM").Value)
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
            SqlStr = " INSERT INTO MAN_MACHINE_PM_HDR " & vbCrLf _
                            & " (AUTO_KEY_PM,COMPANY_CODE, " & vbCrLf _
                            & " PM_DATE,MACHINE_NO,CHECK_TYPE, " & vbCrLf _
                            & " REMARKS,DONE_BY,APP_BY, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCheckType.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','" & MainClass.AllowSingleQuote(txtDoneBy.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtAppBy.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE MAN_MACHINE_PM_HDR SET " & vbCrLf _
                    & " AUTO_KEY_PM=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                    & " PM_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "', " & vbCrLf _
                    & " CHECK_TYPE='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "', " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " DONE_BY='" & MainClass.AllowSingleQuote(txtDoneBy.Text) & "', " & vbCrLf _
                    & " APP_BY='" & MainClass.AllowSingleQuote(txtAppBy.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND AUTO_KEY_PM =" & Val(lblMkey.text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail = False Then GoTo ErrPart
        If UpdateItem = False Then GoTo ErrPart

        SqlStr = ""
        SqlStr = " SELECT AUTO_KEY_PM " & vbCrLf _
                    & " FROM MAN_MACHINE_PM_HDR " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' " & vbCrLf _
                    & " AND CHECK_TYPE ='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "' " & vbCrLf _
                    & " AND PM_DATE=" & vbCrLf _
                    & " (SELECT Max(PM_DATE) " & vbCrLf _
                    & " FROM MAN_MACHINE_PM_HDR " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND SUBSTR(AUTO_KEY_PM,LENGTH(AUTO_KEY_PM)-5,4)=" & RsCompany.fields("FYEAR").value & " " & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'" & vbCrLf _
                    & " AND CHECK_TYPE ='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not mRsTemp.EOF Then
            If mRsTemp.Fields("AUTO_KEY_PM").Value = Val(lblMkey.Text) Then
                SqlStr = " UPDATE MAN_MACHINE_MAINT_TRN SET " & vbCrLf & " LAST_PM=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " DUE_PM=TO_DATE('" & VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Month, Val(lblFrequency.Text), CDate(txtDate.Text)), "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MACHINE_NO ='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' " & vbCrLf & " AND CHECK_TYPE ='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "' "

                PubDBCn.Execute(SqlStr)

                SqlStr = " UPDATE MAN_MACHINE_SCHD_DET SET " & vbCrLf & " PM_DONE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE MACHINE_NO ='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' " & vbCrLf & " AND CHECK_TYPE ='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "' " & vbCrLf & " AND AUTO_KEY_SCHD=" & vbCrLf & " (SELECT AUTO_KEY_SCHD FROM MAN_MACHINE_SCHD_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SCHD_MONTH=" & Val(VB6.Format(txtDate.Text, "MM")) & " " & vbCrLf & " AND SCHD_YEAR=" & Val(VB6.Format(txtDate.Text, "YYYY")) & ") "

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
        RsMachinePMHdr.Requery()
        RsMachinePMDet.Requery()
        RsMachinePMItem.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_PM)  " & vbCrLf & " FROM MAN_MACHINE_PM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PM,LENGTH(AUTO_KEY_PM)-5,4)=" & RsCompany.Fields("FYEAR").Value & " "

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
        Dim mCategory As String
        Dim mCheckPoint As String
        Dim mStatus As String
        Dim mRemarks As String
        Dim mObservation As String
        Dim mActionPlan As String
        Dim mRequirment As String
        Dim mCheckingMethod As String

        PubDBCn.Execute("DELETE FROM MAN_MACHINE_PM_DET WHERE AUTO_KEY_PM=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColCategory
                mCategory = MainClass.AllowSingleQuote(.Text)

                .Col = ColCheckPoint
                mCheckPoint = MainClass.AllowSingleQuote(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                .Col = ColStatus
                mStatus = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColRequirment
                mRequirment = MainClass.AllowSingleQuote(.Text)

                .Col = ColCheckMethod
                mCheckingMethod = MainClass.AllowSingleQuote(.Text)

                .Col = ColObservation
                mObservation = MainClass.AllowSingleQuote(.Text)

                .Col = ColActionPlan
                mActionPlan = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If mCategory <> "" Then
                    SqlStr = " INSERT INTO  MAN_MACHINE_PM_DET ( " & vbCrLf _
                        & " AUTO_KEY_PM,SERIAL_NO,CATEGORY, " & vbCrLf _
                        & " CHECK_POINT, ROW_REMARKS, STATUS, CHECK_REQUIRMENT,CHECK_METHOD,OBSERVATION,ACTION_PLAN ) " & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & Val(lblMkey.Text) & "," & I & ",'" & mCategory & "'," & vbCrLf _
                        & " '" & mCheckPoint & "','" & mRemarks & "', '" & mStatus & "', '" & mRequirment & "', '" & mCheckingMethod & "', '" & mObservation & "', '" & mActionPlan & "') "

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

    Private Function UpdateItem() As Boolean

        On Error GoTo UpdateItemERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mItemCode As String
        Dim mUOM As String
        Dim mQty As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mDivisionCode As Double

        '    If DeleteStockTRN(PubDBCn, ConStockRefType_BDM, lblMkey.text) = False Then GoTo UpdateItemERR
        If DeleteStockTRN(PubDBCn, ConStockRefType_PMS, (lblMkey.Text)) = False Then GoTo UpdateItemERR
        PubDBCn.Execute("DELETE FROM MAN_MACHINE_PM_ITEM WHERE AUTO_KEY_PM=" & Val(lblMkey.Text) & "")

        With SprdItem
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUom
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColAmount
                mAmount = Val(.Text)

                SqlStr = ""

                If mQty > 0 Then
                    SqlStr = " INSERT INTO  MAN_MACHINE_PM_ITEM ( " & vbCrLf & " COMPANY_CODE,AUTO_KEY_PM,SERIAL_NO,ITEM_CODE,ITEM_UOM,STOCK_TYPE,ITEM_QTY,ITEM_RATE,ITEM_AMOUNT ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMkey.Text) & "," & I & ",'" & mItemCode & "','" & mUOM & "', " & vbCrLf & " 'ST'," & mQty & "," & mRate & "," & mAmount & ") "
                    PubDBCn.Execute(SqlStr)

                    If MainClass.ValidateWithMasterTable(txtMachineNo.Text, "MACHINE_NO", "DIV_CODE", "MAN_MACHINE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDivisionCode = Val(MasterNo)
                    End If
                    If UpdateStockTRN(PubDBCn, ConStockRefType_PMS, CStr(Val(lblMkey.Text)), I, (txtDate.Text), (txtDate.Text), "ST", mItemCode, mUOM, CStr(-1), mQty, 0, "O", 0, 0, "", "", "MWS", "MNT", "", "N", "Prev. Maint. of Machine : " & txtMachineNo.Text, "-1", ConSH, mDivisionCode, "", "") = False Then GoTo UpdateItemERR
                End If
            Next
        End With
        UpdateItem = True
        Exit Function
UpdateItemERR:
        UpdateItem = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub cmdSearchAppBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAppBy.Click
        Call SearchEmp(txtAppBy, lblAppBy)
    End Sub

    Private Sub cmdSearchCheckType_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCheckType.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = " SELECT DISTINCT CHECK_TYPE FROM MAN_MACHINE_MAINT_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & vbCrLf _
                    & " AND MACHINE_NO ='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "

        If MainClass.SearchGridMasterBySQL2(txtCheckType.Text, SqlStr) = True Then
            txtCheckType.Text = AcName
        End If
        If txtCheckType.Enabled = True Then txtCheckType.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchDoneBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDoneBy.Click
        Call SearchEmp(txtDoneBy, lblDoneBy)
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

    Private Sub cmdSearchMachineNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMachineNo.Click
        Dim SqlStr As String
        SqlStr = " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' "
        If MainClass.SearchGridMaster("", "MAN_MACHINE_MST", "MACHINE_DESC", "MACHINE_NO", "", "", SqlStr) = True Then
            txtMachineNo.Text = AcName1
            lblDescription.text = AcName
            If txtMachineNo.Enabled = True Then txtMachineNo.Focus()
        End If
    End Sub

    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PM,LENGTH(AUTO_KEY_PM)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "MAN_MACHINE_PM_HDR", "AUTO_KEY_PM", "PM_DATE", "MACHINE_NO", "", SqlStr) = True Then
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
        MainClass.ButtonStatus(Me, XRIGHT, RsMachinePMHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub frmPMStatus_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = "Preventive Maintenance Status"

        SqlStr = "Select * From MAN_MACHINE_PM_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachinePMHdr, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From MAN_MACHINE_PM_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachinePMDet, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From MAN_MACHINE_PM_ITEM WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachinePMItem, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_PM AS SLIP_NUMBER,TO_CHAR(PM_DATE,'DD/MM/YYYY') AS PM_DATE, " & vbCrLf & " MACHINE_NO,CHECK_TYPE,REMARKS,DONE_BY,APP_BY " & vbCrLf & " FROM MAN_MACHINE_PM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PM,LENGTH(AUTO_KEY_PM)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_PM"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmPMStatus_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmPMStatus_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7245
        CurrFormWidth = 11355


        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(8010)
        Me.Width = VB6.TwipsToPixelsX(10635)
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
        txtMachineNo.Text = ""
        txtCheckType.Text = ""
        lblDescription.Text = ""
        lblSpec.Text = ""
        lblFrequency.Text = "0"
        txtRemarks.Text = ""
        txtDoneBy.Text = ""
        lblDoneBy.Text = ""
        txtAppBy.Text = ""
        lblAppBy.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        MainClass.ClearGrid(SprdItem, ConRowHeight)
        FormatSprdMain(-1)
        FormatSprdItem(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsMachinePMHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

            .Col = ColCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMachinePMDet.Fields("CATEGORY").DefinedSize
            .set_ColWidth(ColCategory, 20)

            .Col = ColCheckPoint
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMachinePMDet.Fields("CHECK_POINT").DefinedSize
            .set_ColWidth(ColCheckPoint, 30)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMachinePMDet.Fields("ROW_REMARKS").DefinedSize
            .set_ColWidth(ColRemarks, 15)

            .Col = ColObservation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMachinePMDet.Fields("OBSERVATION").DefinedSize
            .set_ColWidth(ColObservation, 15)

            .Col = ColActionPlan
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMachinePMDet.Fields("ACTION_PLAN").DefinedSize
            .set_ColWidth(ColActionPlan, 15)

            .Col = ColRequirment
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMachinePMDet.Fields("CHECK_REQUIRMENT").DefinedSize
            .set_ColWidth(ColRequirment, 20)

            .Col = ColCheckMethod
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMachinePMDet.Fields("CHECK_METHOD").DefinedSize
            .set_ColWidth(ColCheckMethod, 20)

            .Col = ColStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditMultiLine = False
            .set_ColWidth(ColStatus, 6)



            .Col = ColCategory
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCategory, ColCheckMethod)
            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub FormatSprdItem(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String

        With SprdItem
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMachinePMItem.Fields("ITEM_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 55)


            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColStockQty, 12)

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsMachinePMItem.Fields("ITEM_UOM").DefinedSize
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColUom, 6)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQty, 12)

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRate, 12)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAmount, 12)

            .Col = ColSavedItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMachinePMItem.Fields("ITEM_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .ColHidden = True

            .Col = ColSavedQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = True

            MainClass.ProtectCell(SprdItem, 1, .MaxRows, ColItemName, ColUom)
            MainClass.ProtectCell(SprdItem, 1, .MaxRows, ColRate, ColAmount)
            MainClass.SetSpreadColor(SprdItem, Arow)
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

        txtSlipNo.Maxlength = RsMachinePMHdr.Fields("AUTO_KEY_PM").Precision
        txtDate.Maxlength = RsMachinePMHdr.Fields("PM_DATE").DefinedSize - 6
        txtMachineNo.Maxlength = RsMachinePMHdr.Fields("MACHINE_NO").DefinedSize
        txtCheckType.Maxlength = RsMachinePMHdr.Fields("CHECK_TYPE").DefinedSize
        txtRemarks.Maxlength = RsMachinePMHdr.Fields("REMARKS").DefinedSize
        txtDoneBy.Maxlength = RsMachinePMHdr.Fields("DONE_BY").DefinedSize
        txtAppBy.Maxlength = RsMachinePMHdr.Fields("APP_BY").DefinedSize

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

        If MODIFYMode = True And RsMachinePMHdr.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtMachineNo.Text) = "" Then
            MsgInformation("Part No. empty, So unable to save.")
            txtMachineNo.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtCheckType.Text) = "" Then
            MsgInformation("Check Type empty, So unable to save.")
            txtCheckType.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtDoneBy.Text) = "" Then
            MsgInformation("Done By Employee Code is empty, So unable to save.")
            txtDoneBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtMachineNo.Text, "MACHINE_NO", "DIV_CODE", "MAN_MACHINE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = False Then
            MsgInformation("Division not defined for such Machine in Master, So unable to save.")
            FieldsVarification = False
            Exit Function
        End If

        '    If MainClass.ValidDataInGrid(SprdMain, ColParamDesc, "S", "Please Check Parameter.") = False Then FieldsVarification = False: Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColObservation, "N", "Please Check Observation.") = False Then FieldsVarification = False: Exit Function

        If CheckStockQty(SprdItem, ColStockQty, ColQty, ColItemCode, -1, True) = False Then
            FieldsVarification = False
            Exit Function
        End If
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmPMStatus_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        RsMachinePMHdr.Close()
        RsMachinePMHdr = Nothing
        RsMachinePMDet.Close()
        RsMachinePMDet = Nothing
        RsMachinePMItem.Close()
        RsMachinePMItem = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdItem_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdItem.Change

        With SprdItem
            SprdItem_LeaveCell(SprdItem, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdItem_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdItem.ClickEvent

        Dim SqlStr As String

        If eventArgs.Row = 0 And eventArgs.Col = ColItemCode Then
            With SprdItem
                .Row = .ActiveRow
                .Col = ColItemCode
                '            If RsCompany.fields("COMPANY_CODE").value = 12 Then
                SqlStr = GetStockItemQry(.Text, "Y", VB6.Format(txtDate.Text, "DD/MM/YYYY"), ConSH)
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr, "Y", "1") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName)

                    .Col = ColItemName
                    .Text = Trim(AcName1)
                End If
                '            Else
                '                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                '                    .Row = .ActiveRow
                '
                '                    .Col = ColItemCode
                '                    .Text = Trim(AcName)
                '
                '                    .Col = ColItemName
                '                    .Text = Trim(AcName1)
                '                End If
                '            End If
                Call SprdItem_LeaveCell(SprdItem, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColItemName Then
            With SprdItem
                .Row = .ActiveRow
                .Col = ColItemName
                '            If RsCompany.fields("COMPANY_CODE").value = 12 Then
                SqlStr = GetStockItemQry(.Text, "N", VB6.Format(txtDate.Text, "DD/MM/YYYY"), ConSH)
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr, "Y", "2") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName1)

                    .Col = ColItemName
                    .Text = Trim(AcName)
                End If
                '            Else
                '                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                '                    .Row = .ActiveRow
                '
                '                    .Col = ColItemCode
                '                    .Text = Trim(AcName1)
                '
                '                    .Col = ColItemName
                '                    .Text = Trim(AcName)
                '                End If
                '            End If
                Call SprdItem_LeaveCell(SprdItem, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdItem, eventArgs.Row, ColItemCode)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdItem_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdItem.KeyUpEvent
        Dim mCol As Short
        mCol = SprdItem.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdItem_ClickEvent(SprdItem, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemName Then SprdItem_ClickEvent(SprdItem, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))
        SprdItem.Refresh()
    End Sub

    Private Sub SprdItem_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdItem.LeaveCell

        On Error GoTo ErrPart
        Dim xICode As String

        If eventArgs.NewRow = -1 Then Exit Sub

        SprdItem.Row = SprdItem.ActiveRow
        SprdItem.Col = ColItemCode
        xICode = Trim(SprdItem.Text)
        If xICode = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColItemCode
                SprdItem.Row = SprdItem.ActiveRow

                SprdItem.Col = ColItemCode
                xICode = Trim(SprdItem.Text)
                If xICode = "" Then Exit Sub
                If CheckDuplicateItem(xICode) = False Then
                    If FillGridRow(xICode) = False Then Exit Sub
                    Call CalcAmount()
                End If
            Case ColQty
                If CheckQty() = True Then
                    Call CalcAmount()
                End If
                MainClass.AddBlankSprdRow(SprdItem, ColItemCode, ConRowHeight)
                FormatSprdItem((SprdItem.MaxRows))
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CalcAmount()
        On Error GoTo ERR1
        Dim mQty As Double
        Dim mRate As Double

        With SprdItem
            .Row = .ActiveRow

            .Col = ColQty
            mQty = Val(.Text)

            .Col = ColRate
            mRate = Val(.Text)

            .Col = ColAmount
            .Text = CStr(mQty * mRate)
        End With

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdItem_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdItem.Leave
        With SprdItem
            SprdItem_LeaveCell(SprdItem, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Function CheckQty() As Boolean

        On Error GoTo ERR1
        Dim mQty As Double
        Dim mStockQty As Double
        Dim mItemCode As String
        Dim mUOM As String

        CheckQty = False
        With SprdItem
            .Row = .ActiveRow
            .Col = ColItemCode
            mItemCode = Trim(.Text)

            .Col = ColUom
            mUOM = Trim(.Text)

            .Col = ColQty
            mQty = Val(.Text)
            If Val(.Text) > 0 Then
                mStockQty = GetBalanceStockQty(mItemCode, (txtDate.Text), mUOM, "", "ST", "", ConSH, -1, ConStockRefType_PMS, Val(txtSlipNo.Text)) '''+ GetSavedQty(pItemCode)  ''GetBalanceStockQty(pItemCode, txtDate.Text, mUnit, "", "ST", "", ConSH) + GetSavedQty(pItemCode)
                If mStockQty < mQty Then
                    MsgInformation("Stock Qty is Less than Enter Qty. Please Check Qty.")
                    MainClass.SetFocusToCell(SprdItem, .ActiveRow, ColQty)
                    Exit Function
                End If
                CheckQty = True
            Else
                '            MainClass.SetFocusToCell SprdItem, .ActiveRow, ColQty
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FillGridRow(ByRef pItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String
        Dim mItemCode As String
        Dim mUnit As String
        Dim mStockQty As Double

        If pItemCode = "" Then Exit Function

        SqlStr = ""
        SqlStr = " Select ITEM_CODE,ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf _
                    & " FROM INV_ITEM_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                    & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdItem.Row = SprdItem.ActiveRow
            With RsMisc

                SprdItem.Col = ColItemCode
                SprdItem.Text = IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                mItemCode = Trim(SprdItem.Text)

                SprdItem.Col = ColItemName
                SprdItem.Text = IIf(IsDbNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)

                SprdItem.Col = ColUom
                SprdItem.Text = IIf(IsDbNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)
                mUnit = Trim(SprdItem.Text)

                mStockQty = GetBalanceStockQty(mItemCode, (txtDate.Text), Trim(IIf(IsDbNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)), "", "ST", "", ConSH, -1, ConStockRefType_PMS, Val(txtSlipNo.Text)) '''+ GetSavedQty(pItemCode)  ''GetBalanceStockQty(pItemCode, txtDate.Text, mUnit, "", "ST", "", ConSH) + GetSavedQty(pItemCode)
                SprdItem.Col = ColStockQty
                SprdItem.Text = CStr(mStockQty)

                SprdItem.Col = ColRate
                SprdItem.Text = CStr(GetLatestItemCostFromMRR(mItemCode, mUnit, 1, VB6.Format(IIf((txtDate.Text = "" Or txtDate.Text = "__/__/____"), RunDate, txtDate.Text), "DD/MM/YYYY"), "L"))
            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdItem, SprdItem.ActiveRow, ColItemCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function

    Private Function GetSavedQty(ByRef pItemCode As String) As Double
        On Error GoTo GetERR
        Dim mSavedItemCode As String
        Dim mSavedQty As Double

        With SprdItem
            .Row = .ActiveRow

            .Col = ColSavedItemCode
            mSavedItemCode = .Text

            .Col = ColSavedQty
            mSavedQty = Val(.Text)

            If UCase(Trim(pItemCode)) = UCase(Trim(mSavedItemCode)) Then
                GetSavedQty = mSavedQty
            Else
                GetSavedQty = 0
            End If
        End With
        Exit Function
GetERR:
        GetSavedQty = 0
        MsgBox(Err.Description)
    End Function

    Private Function CheckDuplicateItem(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

        If mItemCode = "" Then CheckDuplicateItem = False : Exit Function
        With SprdItem
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                If UCase(Trim(.Text)) = UCase(Trim(mItemCode)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Item Code")
                        MainClass.SetFocusToCell(SprdItem, .ActiveRow, ColItemCode)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
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
        pTextBox.Text = VB6.Format(Trim(pTextBox.Text), "000000")
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

    Private Sub txtCheckType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCheckType.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCheckType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCheckType.DoubleClick
        Call cmdSearchCheckType_Click(cmdSearchCheckType, New System.EventArgs())
    End Sub

    Private Sub txtCheckType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCheckType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchCheckType_Click(cmdSearchCheckType, New System.EventArgs())
    End Sub

    Private Sub txtCheckType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCheckType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ValProbERR
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mRsTemp As ADODB.Recordset

        If Trim(txtCheckType.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT CHECK_TYPE,FREQUENCY FROM MAN_MACHINE_MAINT_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & vbCrLf _
                    & " AND CHECK_TYPE='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "' " & vbCrLf _
                    & " AND MACHINE_NO ='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            lblFrequency.Text = IIf(IsDbNull(RsTemp.Fields("FREQUENCY").Value), "", RsTemp.Fields("FREQUENCY").Value)

            If ADDMode = True Then
                SqlStr = " SELECT AUTO_KEY_CP " & vbCrLf & " FROM MAN_MACHINE_CP_HDR " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MACHINE_DESC ='" & MainClass.AllowSingleQuote(lblDescription.Text) & "' " & vbCrLf & " AND MACHINE_SPEC ='" & MainClass.AllowSingleQuote(lblSpec.Text) & "' " & vbCrLf & " AND CHECK_TYPE ='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "' "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If Not mRsTemp.EOF Then
                    If CheckMachinePMSchd((txtMachineNo.Text), (txtCheckType.Text), CDate(txtDate.Text)) = True Then
                        FillCP()
                    Else
                        Cancel = True
                    End If
                Else
                    MsgBox("Check Points not defined.")
                    Cancel = True
                End If
            End If
        Else
            MsgBox("Not a valid Check Type", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMachineNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMachineNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.DoubleClick
        Call cmdSearchMachineNo_Click(cmdSearchMachineNo, New System.EventArgs())
    End Sub

    Private Sub txtMachineNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMachineNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMachineNo_Click(cmdSearchMachineNo, New System.EventArgs())
    End Sub

    Public Sub txtMachineNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachineNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtMachineNo.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' "
        If MainClass.ValidateWithMasterTable(txtMachineNo.Text, "MACHINE_NO", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            ShowMachine()
            If Trim(txtCheckType.Text) <> "" Then
                If ADDMode = True Then
                    SqlStr = " SELECT AUTO_KEY_CP " & vbCrLf _
                                        & " FROM MAN_MACHINE_CP_HDR " & vbCrLf _
                                        & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                                        & " AND MACHINE_DESC ='" & MainClass.AllowSingleQuote(lblDescription.text) & "' " & vbCrLf _
                                        & " AND MACHINE_SPEC ='" & MainClass.AllowSingleQuote(lblSpec.text) & "' " & vbCrLf _
                                        & " AND CHECK_TYPE ='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "' "

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If Not mRsTemp.EOF Then
                        If CheckMachinePMSchd((txtMachineNo.Text), (txtCheckType.Text), CDate(txtDate.Text)) = True Then
                            FillCP()
                        Else
                            Cancel = True
                        End If
                    Else
                        MsgBox("Check Points not defined.")
                        Cancel = True
                    End If
                End If
            End If
        Else
            MsgBox("Not a valid Machine No.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDoneBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDoneBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDoneBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDoneBy.DoubleClick
        Call cmdSearchDoneBy_Click(cmdSearchDoneBy, New System.EventArgs())
    End Sub

    Private Sub txtDoneBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDoneBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDoneBy_Click(cmdSearchDoneBy, New System.EventArgs())
    End Sub

    Private Sub txtDoneBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDoneBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtDoneBy, lblDoneBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDate.Text) = "" Then GoTo EventExitSub
        '    txtDate.Text = Format(txtDate.Text, "DD/MM/YYYY")
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

        Clear1()

        If Not RsMachinePMHdr.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsMachinePMHdr.Fields("AUTO_KEY_PM").Value), "", RsMachinePMHdr.Fields("AUTO_KEY_PM").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsMachinePMHdr.Fields("AUTO_KEY_PM").Value), "", RsMachinePMHdr.Fields("AUTO_KEY_PM").Value)
            txtDate.Text = IIf(IsDbNull(RsMachinePMHdr.Fields("PM_DATE").Value), "", RsMachinePMHdr.Fields("PM_DATE").Value)
            txtMachineNo.Text = IIf(IsDbNull(RsMachinePMHdr.Fields("MACHINE_NO").Value), "", RsMachinePMHdr.Fields("MACHINE_NO").Value)
            ShowMachine()
            txtCheckType.Text = IIf(IsDbNull(RsMachinePMHdr.Fields("CHECK_TYPE").Value), "", RsMachinePMHdr.Fields("CHECK_TYPE").Value)
            txtRemarks.Text = IIf(IsDbNull(RsMachinePMHdr.Fields("REMARKS").Value), "", RsMachinePMHdr.Fields("REMARKS").Value)
            txtDoneBy.Text = IIf(IsDbNull(RsMachinePMHdr.Fields("DONE_BY").Value), "", RsMachinePMHdr.Fields("DONE_BY").Value)
            txtDoneBy_Validating(txtDoneBy, New System.ComponentModel.CancelEventArgs(False))
            txtAppBy.Text = IIf(IsDbNull(RsMachinePMHdr.Fields("APP_BY").Value), "", RsMachinePMHdr.Fields("APP_BY").Value)
            txtAppBy_Validating(txtAppBy, New System.ComponentModel.CancelEventArgs(False))
            Call ShowDetail1()
            Call ShowItem()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        SprdItem.Enabled = False
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsMachinePMHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub ShowMachine()

        On Error GoTo ShowErrPart
        Dim RsMachineMst As ADODB.Recordset
        Dim RsTemp As ADODB.Recordset
        Dim RsTemp1 As ADODB.Recordset
        Dim SqlStr As String

        If Trim(txtMachineNo.Text) = "" Then Exit Sub
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
                    & " FROM MAN_MACHINE_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachineMst, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsMachineMst.EOF Then
            lblDescription.Text = IIf(IsDbNull(RsMachineMst.Fields("MACHINE_DESC").Value), "", RsMachineMst.Fields("MACHINE_DESC").Value)
            lblSpec.Text = IIf(IsDbNull(RsMachineMst.Fields("MACHINE_SPEC").Value), "", RsMachineMst.Fields("MACHINE_SPEC").Value)

            '        SqlStr = " SELECT COUNT(*) AS REC_CNT " & vbCrLf _
            ''                & " FROM MAN_MACHINE_MAINT_TRN " & vbCrLf _
            ''                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            ''                & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "
            '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
            '
            '        If RsTemp!REC_CNT = 1 Then
            SqlStr = " SELECT * " & vbCrLf & " FROM MAN_MACHINE_MAINT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp1.EOF = False Then
                lblFrequency.Text = IIf(IsDbNull(RsTemp1.Fields("FREQUENCY").Value), "", RsTemp1.Fields("FREQUENCY").Value)
                txtCheckType.Text = IIf(IsDbNull(RsTemp1.Fields("CHECK_TYPE").Value), "", RsTemp1.Fields("CHECK_TYPE").Value)
            Else
                lblFrequency.Text = ""
                txtCheckType.Text = ""
            End If
        Else
            MsgBox("Machine No Does Not Exist", MsgBoxStyle.Information)
        End If
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub FillCP()

        On Error GoTo FillERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtMachineNo.Text) = "" Then Exit Sub
        SqlStr = " SELECT SERIAL_NO,CATEGORY,CHECK_POINT,CHECK_REQUIRMENT,CHECK_METHOD  " & vbCrLf _
                    & " From MAN_MACHINE_CP_DET " & vbCrLf _
                    & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND AUTO_KEY_CP =" & vbCrLf _
                    & " (SELECT AUTO_KEY_CP FROM MAN_MACHINE_CP_HDR " & vbCrLf _
                    & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND MACHINE_DESC='" & MainClass.AllowSingleQuote(lblDescription.Text) & "' " & vbCrLf _
                    & " AND MACHINE_SPEC='" & MainClass.AllowSingleQuote(lblSpec.Text) & "' " & vbCrLf _
                    & " AND CHECK_TYPE='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "') " & vbCrLf _
                    & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If .EOF = True Then Exit Sub
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColCategory
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("CATEGORY").Value), "", .Fields("CATEGORY").Value))

                SprdMain.Col = ColCheckPoint
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("CHECK_POINT").Value), "", .Fields("CHECK_POINT").Value))

                SprdMain.Col = ColRequirment
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("CHECK_REQUIRMENT").Value), "", .Fields("CHECK_REQUIRMENT").Value))

                SprdMain.Col = ColCheckMethod
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("CHECK_METHOD").Value), "", .Fields("CHECK_METHOD").Value))



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

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM MAN_MACHINE_PM_DET " & vbCrLf _
            & " WHERE AUTO_KEY_PM=" & Val(lblMkey.Text) & "" & vbCrLf _
            & " ORDER BY CATEGORY,SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachinePMDet, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMachinePMDet
            If .EOF = True Then Exit Sub
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColCategory
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("CATEGORY").Value), "", .Fields("CATEGORY").Value))

                SprdMain.Col = ColCheckPoint
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("CHECK_POINT").Value), "", .Fields("CHECK_POINT").Value))

                SprdMain.Col = ColRequirment
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("CHECK_REQUIRMENT").Value), "", .Fields("CHECK_REQUIRMENT").Value))

                SprdMain.Col = ColCheckMethod
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("CHECK_METHOD").Value), "", .Fields("CHECK_METHOD").Value))


                SprdMain.Col = ColRemarks
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ROW_REMARKS").Value), "", .Fields("ROW_REMARKS").Value))

                SprdMain.Col = ColObservation
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("OBSERVATION").Value), "", .Fields("OBSERVATION").Value))

                SprdMain.Col = ColActionPlan
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ACTION_PLAN").Value), "", .Fields("ACTION_PLAN").Value))

                ''  '',
                SprdMain.Col = ColStatus
                SprdMain.Value = IIf(.Fields("Status").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCategory, ColCheckMethod)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub ShowItem()

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemName As String
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM MAN_MACHINE_PM_ITEM " & vbCrLf & " WHERE AUTO_KEY_PM=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachinePMItem, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMachinePMItem
            If .EOF = True Then Exit Sub
            FormatSprdItem(-1)
            I = 1
            Do While Not .EOF
                SprdItem.Row = I

                SprdItem.Col = ColItemCode
                mItemCode = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdItem.Text = mItemCode

                SprdItem.Col = ColItemName
                MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemName = MasterNo
                SprdItem.Text = mItemName

                SprdItem.Col = ColStockQty
                SprdItem.Text = CStr(GetBalanceStockQty(mItemCode, (txtDate.Text), Trim(IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)), "", "ST", "", ConSH, -1, ConStockRefType_PMS, Val(txtSlipNo.Text))) '''+ GetSavedQty(pItemCode)

                SprdItem.Col = ColUom
                SprdItem.Text = Trim(IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value))

                SprdItem.Col = ColQty
                SprdItem.Text = CStr(Val(Trim(IIf(IsDbNull(.Fields("ITEM_QTY").Value), "", .Fields("ITEM_QTY").Value))))

                SprdItem.Col = ColRate
                SprdItem.Text = CStr(Val(Trim(IIf(IsDbNull(.Fields("ITEM_RATE").Value), "", .Fields("ITEM_RATE").Value))))

                SprdItem.Col = ColAmount
                SprdItem.Text = CStr(Val(Trim(IIf(IsDbNull(.Fields("ITEM_AMOUNT").Value), "", .Fields("ITEM_AMOUNT").Value))))

                SprdItem.Col = ColSavedItemCode
                SprdItem.Text = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                SprdItem.Col = ColSavedQty
                SprdItem.Text = CStr(Val(Trim(IIf(IsDbNull(.Fields("ITEM_QTY").Value), "", .Fields("ITEM_QTY").Value))))

                .MoveNext()
                I = I + 1
                SprdItem.MaxRows = I
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

        If MODIFYMode = True And RsMachinePMHdr.BOF = False Then xMKey = RsMachinePMHdr.Fields("AUTO_KEY_PM").Value

        SqlStr = "SELECT * FROM MAN_MACHINE_PM_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PM,LENGTH(AUTO_KEY_PM)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PM=" & mSlipNo & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachinePMHdr, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMachinePMHdr.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM MAN_MACHINE_PM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PM,LENGTH(AUTO_KEY_PM)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PM=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachinePMHdr, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        '    txtDate.Enabled = mMode
        '    txtCheckType.Enabled = mMode
        '    cmdSearchCheckType.Enabled = mMode
        '    txtRemarks.Enabled = mMode
        '    txtDoneBy.Enabled = mMode
        '    cmdSearchDoneBy.Enabled = mMode
        '    txtAppBy.Enabled = mMode
        '    cmdSearchAppBy.Enabled = mMode
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)

        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        MainClass.AssignCRptFormulas(Report1, "PMFrequency=""" & lblFrequency.Text & """")
        MainClass.AssignCRptFormulas(Report1, "Desc=""" & lblDescription.Text & """")
        MainClass.AssignCRptFormulas(Report1, "Spec=""" & lblSpec.Text & """")
        '    Report1.ReportFileName = App.path & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub ReportOnMachinePM(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()

        mTitle = "Preventive Maintenance Status"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MachWisePMStatus.rpt"

        mSubTitle = ""

        SqlStr = MakeSQL

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, "")
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function MakeSQL() As String
        On Error GoTo ERR1

        MakeSQL = " SELECT * " & vbCrLf _
            & " FROM MAN_MACHINE_PM_HDR IH, MAN_MACHINE_PM_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND  SUBSTR(IH.AUTO_KEY_PM,LENGTH(IH.AUTO_KEY_PM)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND IH.AUTO_KEY_PM=ID.AUTO_KEY_PM"


        MakeSQL = MakeSQL & vbCrLf & " AND IH.AUTO_KEY_PM=" & Val(txtSlipNo.Text) & " "

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY IH.AUTO_KEY_PM"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnMachinePM(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnMachinePM(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmPMStatus_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 165, mReFormWidth - 165, mReFormWidth))
        SprdItem.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 165, mReFormWidth - 165, mReFormWidth))
        fraTop1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 105, mReFormWidth - 105, mReFormWidth))
        fraItem.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 105, mReFormWidth - 105, mReFormWidth))
        CurrFormWidth = mReFormWidth

        ''MainClass.SetSpreadColor(SprdMain, -1)
    End Sub
End Class
