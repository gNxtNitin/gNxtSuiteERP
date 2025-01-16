Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmBreakDownTools
    Inherits System.Windows.Forms.Form
    Dim RsBreakDownMain As ADODB.Recordset
    Dim RsBreakDownDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection					

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 12
    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColStockQty As Short = 3
    Private Const ColUom As Short = 4
    Private Const ColQty As Short = 5
    Private Const ColRate As Short = 6
    Private Const ColAmount As Short = 7
    Private Const ColSavedItemCode As Short = 8
    Private Const ColSavedQty As Short = 9

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Dim cntRow As Double
        Dim mItemCode As String
        Dim mUnit As String
        Dim mDivisionCode As Double

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Col = ColItemCode
                mItemCode = Trim(SprdMain.Text)

                .Col = ColUom
                mUnit = Trim(.Text)

                .Col = ColStockQty
                .Text = CStr(GetBalanceStockQty(mItemCode, (txtSlipDate.Text), mUnit, (txtToDept.Text), "ST", "", ConWH, mDivisionCode, ConStockRefType_BDT, Val(txtSlipNo.Text))) '''+ GetSavedQty(pItemCode)					
            Next
        End With


        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub chkItemConsumed_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkItemConsumed.CheckStateChanged
        On Error GoTo ERR1
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        If chkItemConsumed.CheckState = System.Windows.Forms.CheckState.Checked Then
            fraItem.Enabled = True
        Else
            fraItem.Enabled = False
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

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
            If RsBreakDownMain.EOF = False Then RsBreakDownMain.MoveFirst()
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
        Me.Dispose()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtSlipNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsBreakDownMain.EOF Then
            If PubSuperUser <> "S" Then
                If RsBreakDownMain.Fields("CCOMPLET_DATE").Value <> "" Then MsgBox("Number has been completed, So cann't be deleted") : Exit Sub
            End If
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.					
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "TOL_BREAKDOWN_HDR", (txtSlipNo.Text), RsBreakDownMain, "", "D") = False Then GoTo DelErrPart
                If UpdateToolMaster(True) = False Then GoTo DelErrPart
                If DeleteStockTRN(PubDBCn, ConStockRefType_BDT, CStr(Val(lblMkey.Text))) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM TOL_BREAKDOWN_DET WHERE AUTO_KEY_BDSLIP=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM TOL_BREAKDOWN_HDR WHERE AUTO_KEY_BDSLIP=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsBreakDownMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsBreakDownMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click
        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            If PubSuperUser <> "S" Then
                If RsBreakDownMain.Fields("CCOMPLET_DATE").Value <> "" Then MsgBox("Number has been completed, So cann't be modified") : Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsBreakDownMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtSlipNo.Enabled = False
            cmdSearchSlipNo.Enabled = False
            SprdMain.Enabled = True
            If PubSuperUser = "S" Or PubSuperUser = "A" Then txtSlipDate.Enabled = True
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
        If FieldsVarification() = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1() = True Then
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

    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSlipNo As Double
        Dim mItemConsumed As String
        Dim mDivisionCode As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        mSlipNo = Val(txtSlipNo.Text)
        If Val(txtSlipNo.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtSlipNo.Text = CStr(mSlipNo)

        If chkItemConsumed.CheckState = System.Windows.Forms.CheckState.Checked Then
            mItemConsumed = "Y"
        Else
            mItemConsumed = "N"
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO TOL_BREAKDOWN_HDR " & vbCrLf & " (AUTO_KEY_BDSLIP,COMPANY_CODE,FYEAR," & vbCrLf & " SLIP_DATE,BRK_DWN_TIME,FROM_DEPT_CODE,TO_DEPT_CODE," & vbCrLf & " TOOL_NO,COMPLAINT_BY,SUSPECTED_REASON,PROBLEM_FACED," & vbCrLf & " DEPU_EMP_CODE,DEPU_DATE,DEPU_TIME,ITEM_CONSUMED,DEPU_REMARKS,SLIP_RECEIVED_BY, " & vbCrLf & " COMPLETION_DATE,COMPLETION_TIME,COMPLETION_REMARKS,AUTO_KEY_DLYRPT, " & vbCrLf & " CCOMPLET_DATE,CCOMPLET_TIME,CCOMPLAINT_BY,TOTHOUR,TOTCHOUR, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE, DIV_CODE) "

            SqlStr = SqlStr & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                & " TO_DATE('" & IIf(Trim(txtSlipDate.Text) = "/  /", "", VB6.Format(txtSlipDate.Text, "DD-MMM-YYYY")) & "','DD-MON-YYYY'), " & vbCrLf _
                & " TO_DATE('" & IIf(Trim(txtSlipTime.Text) = ":", "", txtSlipTime.Text) & "', 'HH24:MI')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtFromDept.Text) & "','" & MainClass.AllowSingleQuote(txtToDept.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtToolNo.Text) & "','" & MainClass.AllowSingleQuote(txtCompldBy.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtReason.Text) & "','" & MainClass.AllowSingleQuote(txtProblem.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDeputPerson.Text) & "'," & vbCrLf _
                & " TO_DATE('" & IIf(Trim(txtDeputDate.Text) = "/  /", "", VB6.Format(txtDeputDate.Text, "DD-MMM-YYYY")) & "','DD-MON-YYYY'), " & vbCrLf _
                & " TO_DATE('" & IIf(Trim(txtDeputTime.Text) = ":", "", txtDeputTime.Text) & "', 'HH24:MI')," & vbCrLf _
                & " '" & mItemConsumed & "','" & MainClass.AllowSingleQuote(txtDeputRemarks.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtSlipRecvdBy.Text) & "'," & vbCrLf _
                & " TO_DATE('" & IIf(Trim(txtComptDate.Text) = "/  /", "", VB6.Format(txtComptDate.Text, "DD-MMM-YYYY")) & "','DD-MON-YYYY'), " & vbCrLf _
                & " TO_DATE('" & IIf(Trim(txtComptTime.Text) = ":", "", txtComptTime.Text) & "', 'HH24:MI')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "',''," & vbCrLf _
                & " TO_DATE('" & IIf(Trim(txtCComptDate.Text) = "/  /", "", VB6.Format(txtCComptDate.Text, "DD-MMM-YYYY")) & "','DD-MON-YYYY'), " & vbCrLf _
                & " TO_DATE('" & IIf(Trim(txtCComptTime.Text) = ":", "", txtCComptTime.Text) & "', 'HH24:MI'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtCCompldBy.Text) & "'," & Val(txtTotalTime.Text) & ", " & vbCrLf _
                & " " & Val(txtCTotalTime.Text) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'',''," & mDivisionCode & ")"

        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE TOL_BREAKDOWN_HDR SET " & vbCrLf _
                & " AUTO_KEY_BDSLIP=" & mSlipNo & ", " & vbCrLf _
                & " SLIP_DATE=TO_DATE('" & IIf(Trim(txtSlipDate.Text) = "/  /", "", VB6.Format(txtSlipDate.Text, "DD-MMM-YYYY")) & "','DD-MON-YYYY'), " & vbCrLf _
                & " BRK_DWN_TIME=TO_DATE('" & IIf(Trim(txtSlipTime.Text) = ":", "", txtSlipTime.Text) & "', 'HH24:MI')," & vbCrLf _
                & " FROM_DEPT_CODE='" & MainClass.AllowSingleQuote(txtFromDept.Text) & "',TO_DEPT_CODE='" & MainClass.AllowSingleQuote(txtToDept.Text) & "'," & vbCrLf _
                & " TOOL_NO='" & MainClass.AllowSingleQuote(txtToolNo.Text) & "',COMPLAINT_BY='" & MainClass.AllowSingleQuote(txtCompldBy.Text) & "'," & vbCrLf _
                & " SUSPECTED_REASON='" & MainClass.AllowSingleQuote(txtReason.Text) & "',PROBLEM_FACED='" & MainClass.AllowSingleQuote(txtProblem.Text) & "'," & vbCrLf _
                & " DEPU_EMP_CODE='" & MainClass.AllowSingleQuote(txtDeputPerson.Text) & "', " & vbCrLf _
                & " DEPU_DATE=TO_DATE('" & IIf(Trim(txtDeputDate.Text) = "/  /", "", VB6.Format(txtDeputDate.Text, "DD-MMM-YYYY")) & "','DD-MON-YYYY'), " & vbCrLf _
                & " DEPU_TIME=TO_DATE('" & IIf(Trim(txtDeputTime.Text) = ":", "", txtDeputTime.Text) & "', 'HH24:MI')," & vbCrLf _
                & " ITEM_CONSUMED='" & mItemConsumed & "',DEPU_REMARKS='" & MainClass.AllowSingleQuote(txtDeputRemarks.Text) & "', " & vbCrLf _
                & " SLIP_RECEIVED_BY='" & MainClass.AllowSingleQuote(txtSlipRecvdBy.Text) & "'," & vbCrLf _
                & " COMPLETION_DATE=TO_DATE('" & IIf(Trim(txtComptDate.Text) = "/  /", "", VB6.Format(txtComptDate.Text, "DD-MMM-YYYY")) & "','DD-MON-YYYY'), " & vbCrLf _
                & " COMPLETION_TIME=TO_DATE('" & IIf(Trim(txtComptTime.Text) = ":", "", txtComptTime.Text) & "', 'HH24:MI')," & vbCrLf _
                & " COMPLETION_REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "',AUTO_KEY_DLYRPT=''," & vbCrLf _
                & " CCOMPLET_DATE=TO_DATE('" & IIf(Trim(txtCComptDate.Text) = "/  /", "", VB6.Format(txtCComptDate.Text, "DD-MMM-YYYY")) & "','DD-MON-YYYY'), " & vbCrLf _
                & " CCOMPLET_TIME=TO_DATE('" & IIf(Trim(txtCComptTime.Text) = ":", "", txtCComptTime.Text) & "', 'HH24:MI')," & vbCrLf _
                & " CCOMPLAINT_BY='" & MainClass.AllowSingleQuote(txtCCompldBy.Text) & "'," & vbCrLf _
                & " TOTHOUR=" & Val(txtTotalTime.Text) & ",TOTCHOUR=" & Val(txtCTotalTime.Text) & ", " & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), DIV_CODE=" & mDivisionCode & "" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND AUTO_KEY_BDSLIP =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail(mDivisionCode) = False Then GoTo ErrPart
        If UpdateToolMaster(False) = False Then GoTo ErrPart

        If ADDMode = True And lblFormType.Text = "REG" Then
            If SendMail((lblFormType.Text)) = False Then GoTo ErrPart
        End If

        If lblFormType.Text = "MAN" Then
            If SendMail((lblFormType.Text)) = False Then GoTo ErrPart
        End If

        Update1 = True
        PubDBCn.CommitTrans()
        txtSlipNo.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsBreakDownMain.Requery()
        RsBreakDownDetail.Requery()
        MsgBox(Err.Description)
        '    Resume					
    End Function
    Private Function SendMail(ByRef pFlag As Object) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String

        Dim mTo As String
        Dim mCC As String
        Dim mFrom As String
        Dim mAttachmentFile As String
        Dim mDateTime As String
        Dim pAccountCode As String
        Dim mSubject As String
        Dim mBodyText As String


        SendMail = False


        mFrom = ""
        If MainClass.ValidateWithMasterTable(PubUserID, "USER_ID", "EMAIL", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mFrom = MasterNo
        Else
            mFrom = ""
        End If

        mFrom = If(Len(mFrom) < 5, "", Trim(mFrom))

        If mFrom = "" Then
            mFrom = GetEMailID("MAIL_ACCOUNT")  ''strAccount
        End If

        mTo = GetEMailID("TOOL_MAIL_TO")

        'If MainClass.ValidateWithMasterTable(txtCompldBy.Text, "EMP_CODE", "EMP_EMAILID_OFF", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '    mCC = MasterNo
        'Else
        '    mCC = ""
        'End If

        If MainClass.ValidateWithMasterTable(txtCompldBy.Text, "USER_ID", "EMAIL", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCC = MasterNo
        Else
            mCC = ""
        End If

        mCC = If(Len(mCC) < 5, "", Trim(mCC))



        mAttachmentFile = ""

        mSubject = ""

        mSubject = "Tool Break Down - Department :" & Trim(lblFromDept.Text) & " From Dated : " & VB6.Format(txtSlipDate.Text) & ""

        If pFlag = "REG" Then
            mBodyText = "<html><body><b><font size=11, color=Red>Tool Break Down</font></b><br />" & "<b>Slip No       : </b>" & Trim(txtSlipNo.Text) & "<br />" & "<b>Department    : </b>" & Trim(lblFromDept.Text) & "<br />" & "<b>Dated         : </b>" & VB6.Format(txtSlipDate.Text) & "<br />" & "<b>Time          : </b>" & Trim(txtSlipTime.Text) & "<br />" & "<b>Tool No      : </b>" & Trim(txtToolNo.Text) & "<br />" & "<br />" & "<b>Tool Name      : </b>" & Trim(lblToolNo.Text) & "<br />" & "<br />" & "<b>Part Name      : </b>" & Trim(txtPartName.Text) & "<br />" & "<b>Complained By : </b>" & Trim(lblCompldBy.Text) & "<br />" & "<b>Problem       : </b>" & Trim(txtReason.Text) & "<br />" & "</body></html>"
        Else
            mBodyText = "<html><body><b><font size=11, color=Blue>Tool Break Down (Completed)</font></b><br />" & "<b>Slip No               : </b>" & Trim(txtSlipNo.Text) & "<br />" & "<b>Department            : </b>" & Trim(lblFromDept.Text) & "<br />" & "<b>Completion Dated      : </b>" & VB6.Format(txtComptDate.Text) & "<br />" & "<b>Completion Time       : </b>" & Trim(txtComptTime.Text) & "<br />" & "<b>Total Break Down Time : </b>" & Trim(txtTotalTime.Text) & "<br />" & "<b>Tool No            : </b>" & Trim(txtToolNo.Text) & "<br />" & "<br />" & "<b>Tool Desc      : </b>" & Trim(lblToolNo.Text) & "<br />" & "<br />" & "<b>Part Name      : </b>" & Trim(txtPartName.Text) & "<br />" & "<b>Deputed Person        : </b>" & Trim(lblDeputPerson.Text) & "<br />" & "<b>Action Taken          : </b>" & Trim(txtDeputRemarks.Text) & "<br />" & "</body></html>"
        End If


        If Trim(mTo) <> "" Then
            If SendMailProcess(mFrom, mTo, mCC, "", mAttachmentFile, mSubject, mBodyText) = False Then
                SendMail = True
                Exit Function
            End If
        End If

        SendMail = True

        Exit Function
ErrPart:
        SendMail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateToolMaster(ByRef pIsSlipDeleted As Boolean) As Boolean
        On Error GoTo UpMacERR
        Dim SqlStr As String
        Dim mTool_UB As String

        If pIsSlipDeleted = True Then
            mTool_UB = "N"
        Else
            If Trim(txtSlipRecvdBy.Text) = "" And Not IsDate(txtComptDate.Text) Then       ''Not IsDate(txtComptDate.Text)
                mTool_UB = "Y"
            Else
                mTool_UB = "N"
            End If
        End If
        SqlStr = " UPDATE TOL_TOOLINFO_MST SET " & vbCrLf _
            & " TOOL_UB='" & mTool_UB & "' " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND TOOL_NO='" & MainClass.AllowSingleQuote(txtToolNo.Text) & "'"
        PubDBCn.Execute(SqlStr)
        UpdateToolMaster = True
        Exit Function
UpMacERR:
        UpdateToolMaster = False
        MsgBox(Err.Description)
    End Function

    Private Function AutoGenKeyNo() As Double
        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_BDSLIP)  " & vbCrLf & " FROM TOL_BREAKDOWN_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mValue = IIf(IsDBNull(.Fields(0).Value), 0, .Fields(0).Value)
                    mAutoGen = CDbl(Mid(mValue, 1, Len(mValue) - 6))
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

    Private Function UpdateDetail(ByRef mDivisionCode As Double) As Boolean
        On Error GoTo UpdateDetailERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mItemCode As String
        Dim mUOM As String
        Dim mQty As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mCompDate As String

        If DeleteStockTRN(PubDBCn, ConStockRefType_BDT, (lblMkey.Text)) = False Then GoTo UpdateDetailERR
        PubDBCn.Execute("DELETE FROM TOL_BREAKDOWN_DET WHERE AUTO_KEY_BDSLIP=" & Val(lblMkey.Text) & "")

        With SprdMain
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

                If mQty > 0 And chkItemConsumed.CheckState = System.Windows.Forms.CheckState.Checked Then
                    '                If CDate(txtSlipDate.Text) < CDate(RsCompany!START_DATE) Then					
                    '                    mCompDate = Format(RsCompany!START_DATE, "DD/MM/YYYY")					
                    '                Else					
                    '                    mCompDate = Format(txtSlipDate.Text, "DD/MM/YYYY")					
                    '                End If					

                    SqlStr = " INSERT INTO  TOL_BREAKDOWN_DET ( " & vbCrLf _
                        & " COMPANY_CODE,AUTO_KEY_BDSLIP,SERIAL_NO,SLIP_DATE,BRK_DWN_TIME,FROM_DEPT_CODE, " & vbCrLf _
                        & " TOOL_NO,PROBLEM_FACED,ITEM_CODE,ITEM_UOM,STOCK_TYPE,ITEM_QTY,ITEM_RATE,ITEM_AMOUNT ) " & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMkey.Text) & "," & I & ",TO_DATE('" & VB6.Format(txtSlipDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & txtSlipTime.Text & "', 'HH24:MI'), " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtFromDept.Text) & "','" & MainClass.AllowSingleQuote(txtToolNo.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtProblem.Text) & "','" & mItemCode & "','" & mUOM & "', " & vbCrLf _
                        & " 'ST'," & mQty & "," & mRate & "," & mAmount & ") "

                    PubDBCn.Execute(SqlStr)
                    If UpdateStockTRN(PubDBCn, ConStockRefType_BDT, CStr(Val(lblMkey.Text)), I, (txtSlipDate.Text), (txtSlipDate.Text), "ST", mItemCode, mUOM, CStr(-1), mQty, 0, "O", 0, 0, "", "", (txtToDept.Text), (txtFromDept.Text), "", "N", " From : " & txtToDept.Text & " To : " & txtFromDept.Text & "-" & ConStockRefType_BDT, "-1", ConWH, mDivisionCode, "", "") = False Then GoTo UpdateDetailERR
                End If
            Next
        End With
        UpdateDetail = True
        Exit Function
UpdateDetailERR:
        UpdateDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub cmdSearchCCompldBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCCompldBy.Click
        Call SearchEmp(txtCCompldBy, lblCCompldBy)
    End Sub

    Private Sub cmdSearchCompldBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCompldBy.Click
        Call SearchEmp(txtCompldBy, lblCompldBy)
    End Sub

    Private Sub SearchEmp(ByRef pTextBax As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label)
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
        If ADDMode = True Then
            SqlStr = SqlStr & vbCrLf & "  AND EMP_LEAVE_DATE IS NULL "
        Else
            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR (EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND EMP_LEAVE_DATE <= TO_DATE('" & VB6.Format(txtSlipDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')))"
        End If

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            pTextBax.Text = AcName1
            pLable.Text = AcName
        End If

        If pTextBax.Enabled = True Then pTextBax.Focus()
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchDeputPerson_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDeputPerson.Click
        Call SearchEmp(txtDeputPerson, lblDeputPerson)
    End Sub

    Private Sub cmdSearchFromDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchFromDept.Click
        Call SearchDept(txtFromDept, lblFromDept)
    End Sub

    Private Sub SearchDept(ByRef pTextBax As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label)
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            pTextBax.Text = AcName1
            pLable.Text = AcName
            If pTextBax.Enabled = True Then pTextBax.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchMacNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMacNo.Click
        Dim SqlStr As String


        SqlStr = " SELECT  B.ITEM_SHORT_DESC,A.TOOL_NO, C.OPR_DESC, D.ITEM_SHORT_DESC AS PART_NAME " & vbCrLf _
            & " FROM TOL_TOOLINFO_MST A, INV_ITEM_MST B, PRD_OPR_MST C, INV_ITEM_MST D" & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
            & " AND A.TOOL_ITEM_CODE=B.ITEM_CODE " & vbCrLf _
            & " AND A.COMPANY_CODE=C.COMPANY_CODE " & vbCrLf _
            & " AND A.OPR_CODE=C.OPR_CODE " & vbCrLf _
            & " AND A.COMPANY_CODE=D.COMPANY_CODE " & vbCrLf _
            & " AND A.ITEM_CODE=D.ITEM_CODE " & vbCrLf

        SqlStr = SqlStr & vbCrLf & " AND B.ITEM_CLASSIFICATION='T'"
        SqlStr = SqlStr & vbCrLf & " AND A.TOOL_UB='N' AND A.TOOL_STATUS='O'"

        If MainClass.SearchGridMasterBySQL2(txtToolNo.Text, SqlStr, "N", "1") = True Then
            txtToolNo.Text = AcName1
            lblToolNo.Text = AcName
            txtPartName.Text = AcName3
            If txtToolNo.Enabled = True Then txtToolNo.Focus()
        End If
    End Sub

    Private Sub cmdSearchProblem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProblem.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "TOL_BDPROBLEMS_MST", "PROB_DESC", "PROB_CODE", , , SqlStr) = True Then
            txtProblem.Text = AcName1
            lblProblem.Text = AcName
        End If
        If txtProblem.Enabled = True Then txtProblem.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        If MainClass.SearchGridMaster(txtSlipNo.Text, "TOL_BREAKDOWN_HDR", "AUTO_KEY_BDSLIP", "TOOL_NO", "FROM_DEPT_CODE", "TO_DEPT_CODE", SqlStr) = True Then
            txtSlipNo.Text = AcName
            Call txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
        End If

    End Sub

    Private Sub cmdSearchSlipRecvdBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipRecvdBy.Click
        Call SearchEmp(txtSlipRecvdBy, lblSlipRecvdBy)
    End Sub

    Private Sub cmdSearchToDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchToDept.Click
        Call SearchDept(txtToDept, lblToDept)
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
        MainClass.ButtonStatus(Me, XRIGHT, RsBreakDownMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmBreakDownTools_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If lblFormType.Text = "REG" Then
            Me.Text = "Tools Break Down Complaint (Registration)"
            fraComplainer.Enabled = True
            fraComplainee.Enabled = False
            fraComplainerEnd.Enabled = False
            fraItem.Enabled = False
        ElseIf lblFormType.Text = "MAN" Then
            Me.Text = "Tools Break Down Complaint (Tool Room)"
            fraComplainer.Enabled = True ''False					
            fraComplainee.Enabled = True
            fraComplainerEnd.Enabled = True ''False					
            fraItem.Enabled = True
        ElseIf lblFormType.Text = "DEL" Then
            Me.Text = "Tools Break Down Complaint (Delivery)"
            fraComplainer.Enabled = False
            fraComplainee.Enabled = False
            fraComplainerEnd.Enabled = True
            fraItem.Enabled = False
        End If

        SqlStr = "Select * From TOL_BREAKDOWN_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBreakDownMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From TOL_BREAKDOWN_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBreakDownDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        Call Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_BDSLIP AS BREAKDOWN_NUMBER,TO_CHAR(SLIP_DATE,'DD/MM/YYYY') AS SLIP_DATE,TO_CHAR(BRK_DWN_TIME,'HH24:MM') AS BRK_DWN_TIME, " & vbCrLf & " TOOL_NO,FROM_DEPT_CODE,TO_DEPT_CODE,COMPLAINT_BY,SUSPECTED_REASON,  " & vbCrLf _
            & " PROBLEM_FACED,DEPU_EMP_CODE,TO_CHAR(DEPU_DATE,'DD/MM/YYYY') AS DEPU_DATE,TO_CHAR(DEPU_TIME,'HH24:MM') AS DEPU_TIME, " & vbCrLf _
            & " DEPU_REMARKS,SLIP_RECEIVED_BY,TO_CHAR(COMPLETION_DATE,'DD/MM/YYYY') AS COMPLETION_DATE,TO_CHAR(COMPLETION_TIME,'HH24:MM') AS COMPLETION_TIME, " & vbCrLf _
            & " COMPLETION_REMARKS " & vbCrLf _
            & " FROM TOL_BREAKDOWN_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " ORDER BY AUTO_KEY_BDSLIP"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmBreakDownTools_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmBreakDownTools_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RS As ADODB.Recordset

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection					
        'PvtDBCn.Open StrConn					
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(8010)
        Me.Width = VB6.TwipsToPixelsX(11370)
        ADDMode = False
        MODIFYMode = False
        FormActive = False



        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = -1

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear1()
        On Error GoTo ClearErr

        lblMkey.Text = ""
        txtSlipNo.Text = ""
        txtSlipDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtSlipTime.Text = GetServerTime()

        txtSlipDate.Enabled = False
        txtSlipTime.Enabled = False

        txtFromDept.Text = ""
        lblFromDept.Text = ""
        txtToDept.Text = ""
        lblToDept.Text = ""
        txtToolNo.Text = ""
        lblToolNo.Text = ""
        txtPartName.Text = ""
        txtCompldBy.Text = ""
        lblCompldBy.Text = ""
        txtReason.Text = ""
        txtProblem.Text = ""
        lblProblem.Text = ""
        txtDeputPerson.Text = ""
        lblDeputPerson.Text = ""
        txtDeputDate.Text = "__/__/____"
        txtDeputTime.Text = "__:__"
        chkItemConsumed.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtDeputRemarks.Text = ""
        txtSlipRecvdBy.Text = ""
        lblSlipRecvdBy.Text = ""
        txtComptDate.Text = "__/__/____"
        txtComptTime.Text = "__:__"
        txtTotalTime.Text = ""
        txtCComptDate.Text = "__/__/____"
        txtCComptTime.Text = "__:__"
        txtCCompldBy.Text = ""
        lblCCompldBy.Text = ""
        txtCTotalTime.Text = ""
        txtRemarks.Text = ""

        cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        fraItem.Enabled = False
        MainClass.ButtonStatus(Me, XRIGHT, RsBreakDownMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsBreakDownDetail.Fields("ITEM_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsBreakDownDetail.Fields("ITEM_UOM").DefinedSize
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColSavedItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsBreakDownDetail.Fields("ITEM_CODE").DefinedSize
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

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColUom)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRate, ColAmount)
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
            .set_ColWidth(2, 500 * 2)
            .set_ColWidth(3, 500 * 3)
            .set_ColWidth(4, 500 * 3)
            .set_ColWidth(5, 500 * 4)
            .set_ColWidth(6, 500 * 4)
            .set_ColWidth(7, 500 * 3)
            .set_ColWidth(8, 500 * 4)
            .set_ColWidth(9, 500 * 4)
            .set_ColWidth(10, 500 * 3)
            .set_ColWidth(11, 500 * 3)
            .set_ColWidth(12, 500 * 2)
            .set_ColWidth(13, 500 * 5)
            .set_ColWidth(14, 500 * 4)
            .set_ColWidth(15, 500 * 4)
            .set_ColWidth(16, 500 * 4)
            .set_ColWidth(17, 500 * 5)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.MaxLength = RsBreakDownMain.Fields("AUTO_KEY_BDSLIP").Precision
        txtSlipDate.MaxLength = RsBreakDownMain.Fields("SLIP_DATE").DefinedSize - 6
        txtSlipTime.MaxLength = RsBreakDownMain.Fields("BRK_DWN_TIME").DefinedSize - 11
        txtReason.MaxLength = RsBreakDownMain.Fields("SUSPECTED_REASON").DefinedSize
        txtDeputDate.MaxLength = RsBreakDownMain.Fields("DEPU_DATE").DefinedSize - 6
        txtDeputTime.MaxLength = RsBreakDownMain.Fields("DEPU_TIME").DefinedSize - 11
        txtDeputRemarks.MaxLength = RsBreakDownMain.Fields("DEPU_REMARKS").DefinedSize
        txtComptDate.MaxLength = RsBreakDownMain.Fields("COMPLETION_DATE").DefinedSize - 6
        txtComptTime.MaxLength = RsBreakDownMain.Fields("COMPLETION_TIME").DefinedSize - 11
        txtCComptDate.MaxLength = RsBreakDownMain.Fields("CCOMPLET_DATE").DefinedSize - 6
        txtCComptTime.MaxLength = RsBreakDownMain.Fields("CCOMPLET_TIME").DefinedSize - 11
        txtRemarks.MaxLength = RsBreakDownMain.Fields("COMPLETION_REMARKS").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume					
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim ii As Integer
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsBreakDownMain.EOF = True Then Exit Function

        If Trim(txtSlipDate.Text) = "" Or Trim(txtSlipDate.Text) = "__/__/____" Then
            MsgInformation("Slip Date is empty, So unable to save.")
            txtSlipDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If FYChk((txtSlipDate.Text)) = False Then
            FieldsVarification = False
            If txtSlipDate.Enabled = True Then txtSlipDate.Focus()
            Exit Function
        End If

        If Val(Replace(txtSlipTime.Text, ":", ".")) = 0 Then
            MsgInformation("Slip Time is empty, So unable to save.")
            txtSlipTime.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If

        If lblFormType.Text = "REG" Then


            If ADDMode = True Then
                SqlStr = " SELECT A.TOOL_NO, B.ITEM_SHORT_DESC, C.OPR_DESC,A.TOOL_UB, A.TOOL_STATUS " & vbCrLf & " FROM TOL_TOOLINFO_MST A, INV_ITEM_MST B, PRD_OPR_MST C" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.TOOL_ITEM_CODE=B.ITEM_CODE " & vbCrLf & " AND A.COMPANY_CODE=C.COMPANY_CODE " & vbCrLf & " AND A.OPR_CODE=C.OPR_CODE "

                SqlStr = SqlStr & vbCrLf & " AND B.ITEM_CLASSIFICATION='T'"
                '            SqlStr = SqlStr & vbCrLf & " AND A.TOOL_UB='N' AND A.TOOL_STATUS='O'"					
                SqlStr = SqlStr & vbCrLf & " AND A.TOOL_NO='" & MainClass.AllowSingleQuote(txtToolNo.Text) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = True Then
                    MsgInformation("Invalid Tool No, Cann't be Save.")
                    If txtToolNo.Enabled = True Then txtToolNo.Focus()
                    FieldsVarification = False
                    Exit Function
                Else
                    If RsTemp.Fields("TOOL_STATUS").Value = "C" Then
                        MsgInformation("Tool No is Closed, Cann't be Save.")
                        If txtToolNo.Enabled = True Then txtToolNo.Focus()
                        FieldsVarification = False
                        Exit Function
                    End If
                    If RsTemp.Fields("TOOL_UB").Value = "Y" Then
                        MsgInformation("Already Under Break Down, Cann't be Save.")
                        If txtToolNo.Enabled = True Then txtToolNo.Focus()
                        FieldsVarification = False
                        Exit Function
                    End If

                End If
            End If


            If Trim(txtFromDept.Text) = "" Then
                MsgInformation("From Dept. is empty, So unable to save.")
                txtFromDept.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If Trim(txtToDept.Text) = "" Then
                MsgInformation("To Dept. is empty, So unable to save.")
                txtToDept.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If Trim(txtToolNo.Text) = "" Then
                MsgInformation("Tool No is empty, So unable to save.")
                txtToolNo.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtCompldBy.Text) = "" Then
                MsgInformation("Complained By is empty, So unable to save.")
                txtCompldBy.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtReason.Text) = "" Then
                MsgInformation("Reason is empty, So unable to save.")
                txtReason.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If
        If lblFormType.Text = "MAN" Then
            If Trim(txtDeputPerson.Text) = "" Then
                MsgInformation("Deputed Person is empty, So unable to save.")
                txtDeputPerson.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtDeputDate.Text) = "" Then
                MsgInformation("Deputed Date is empty, So unable to save.")
                txtDeputDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtDeputTime.Text) = "" Then
                MsgInformation("Deputed Time is empty, So unable to save.")
                txtDeputTime.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtProblem.Text) <> "" Then
                If Trim(txtDeputRemarks.Text) = "" Then
                    MsgInformation("Deputed Remarks is empty, So unable to save.")
                    txtDeputRemarks.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
                If Trim(txtComptDate.Text) = "" Then
                    MsgInformation("Completion Date is empty, So unable to save.")
                    txtComptDate.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
                If Trim(txtComptTime.Text) = "" Then
                    MsgInformation("Completion Time is empty, So unable to save.")
                    txtComptTime.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If
        If lblFormType.Text = "DEL" Then
            If Trim(txtDeputPerson.Text) = "" Then
                MsgInformation("Complaint has not been Attended, So unable to save.")
                '            txtDeputPerson.SetFocus					
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtProblem.Text) = "" Then
                MsgInformation("Complaint has not been Completed, So unable to save.")
                '            txtProblem.SetFocus					
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtCComptDate.Text) = "" Then
                MsgInformation("Completion Date At Complainer Site is empty, So unable to save.")
                txtCComptDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtCComptTime.Text) = "" Then
                MsgInformation("Completion Time At Complainer Site  is empty, So unable to save.")
                txtCComptTime.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtCCompldBy.Text) = "" Then
                MsgInformation("Complained By At Complainer Site is empty, So unable to save.")
                txtCCompldBy.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Val(txtCTotalTime.Text) < 0 Then
            MsgInformation("Deputed cann't be less than Complaint regestered., So unable to save.")
            txtCTotalTime.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtTotalTime.Text) < 0 Then
            MsgInformation("Completion cann't be less than Complaint regestered., So unable to save.")
            txtTotalTime.Focus()
            FieldsVarification = False
            Exit Function
        End If



        If chkItemConsumed.CheckState = System.Windows.Forms.CheckState.Checked Then
            If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Please Check Item Consumed Detail.") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Item Consumed Detail.") = False Then FieldsVarification = False : Exit Function

            If CheckStockQty(SprdMain, ColStockQty, ColQty, ColItemCode, -1, True) = False Then
                FieldsVarification = False
                Exit Function
            End If
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume					
    End Function

    Private Sub frmBreakDownTools_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsBreakDownMain.Close()
        RsBreakDownMain = Nothing
        RsBreakDownDetail.Close()
        RsBreakDownDetail = Nothing
        'PvtDBCn.Close					
        'Set PvtDBCn = Nothing					
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        Dim SqlStr As String

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                '            If RsCompany.Fields("COMPANY_CODE").Value = 12 Then					
                SqlStr = GetStockItemQry(.Text, "Y", VB6.Format(txtSlipDate.Text, "DD/MM/YYYY"), ConWH)

                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr, "Y", "1") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName)
                End If
                '            Else					
                '                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then					
                '                    .Row = .ActiveRow					
                '					
                '                    .Col = ColItemCode					
                '                    .Text = Trim(AcName)					
                '					
                '                    .Col = ColItemName					
                '                    .Text = Trim(AcName1)					
                '                End If					
                '            End If					
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemName Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemName
                '            If RsCompany.Fields("COMPANY_CODE").Value = 12 Then					
                SqlStr = GetStockItemQry(.Text, "N", VB6.Format(txtSlipDate.Text, "DD/MM/YYYY"), ConWH)
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr, "Y", "2") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName1)
                End If

                '            Else					
                '                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then					
                '                    .Row = .ActiveRow					
                '					
                '                    .Col = ColItemCode					
                '                    .Text = Trim(AcName1)					
                '					
                '                    .Col = ColItemName					
                '                    .Text = Trim(AcName)					
                '                End If					
                '            End If					
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim xICode As String
        Dim mDivisionCode As Double

        If eventArgs.newRow = -1 Then Exit Sub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColItemCode
        xICode = Trim(SprdMain.Text)
        If xICode = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = Trim(SprdMain.Text)
                If xICode = "" Then Exit Sub
                If CheckDuplicateItem(xICode) = False Then
                    If FillGridRow(xICode, mDivisionCode) = False Then Exit Sub
                    Call CalcAmount()
                End If
            Case ColQty
                If CheckQty() = True Then
                    Call CalcAmount()
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CalcAmount()
        On Error GoTo ERR1
        Dim mQty As Double
        Dim mRate As Double

        With SprdMain
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

    Private Function CheckQty() As Boolean
        On Error GoTo ERR1
        With SprdMain
            .Col = ColQty
            If Val(.Text) > 0 Then
                CheckQty = True
            Else
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColQty)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FillGridRow(ByRef pItemCode As String, ByRef mDivisionCode As Double) As Boolean
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String
        Dim mItemCode As String
        Dim mUnit As String
        Dim mStockQty As Double

        If pItemCode = "" Then Exit Function

        If Trim(txtSlipDate.Text) = "" Then
            MsgInformation("Please Select Slip Date.")
            If txtSlipDate.Enabled = True Then txtSlipDate.Focus()
            FillGridRow = True
            Exit Function
        End If

        If Trim(txtToDept.Text) = "" Then
            MsgInformation("Please Select Dept Code.")
            If txtToDept.Enabled = True Then txtToDept.Focus()
            FillGridRow = True
            Exit Function
        End If

        SqlStr = ""
        SqlStr = " Select ITEM_CODE,ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf _
            & " FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc

                SprdMain.Col = ColItemCode
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                mItemCode = Trim(SprdMain.Text)

                SprdMain.Col = ColItemName
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value)

                SprdMain.Col = ColUom
                SprdMain.Text = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)
                mUnit = Trim(SprdMain.Text)

                mStockQty = GetBalanceStockQty(pItemCode, (txtSlipDate.Text), mUnit, (txtToDept.Text), "ST", "", ConWH, mDivisionCode, ConStockRefType_BDT, Val(txtSlipNo.Text)) '''+ GetSavedQty(pItemCode)					
                SprdMain.Col = ColStockQty
                SprdMain.Text = CStr(mStockQty)

                SprdMain.Col = ColRate
                SprdMain.Text = CStr(GetLatestItemCostFromMRR(mItemCode, mUnit, 1, VB6.Format(IIf((txtComptDate.Text = "" Or txtComptDate.Text = "__/__/____"), RunDate, txtComptDate.Text), "DD/MM/YYYY"), "L"))
            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        '    Resume					
        FillGridRow = False
        MsgBox(Err.Description)
    End Function

    Private Function GetSavedQty(ByRef pItemCode As String) As Double
        On Error GoTo GetERR
        Dim mSavedItemCode As String
        Dim mSavedQty As Double

        With SprdMain
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
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                If UCase(Trim(.Text)) = UCase(Trim(mItemCode)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Item Code")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

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
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtCCompldBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCCompldBy.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCCompldBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCCompldBy.DoubleClick
        Call cmdSearchCCompldBy_Click(cmdSearchCCompldBy, New System.EventArgs())
    End Sub

    Private Sub txtCCompldBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCCompldBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchCCompldBy_Click(cmdSearchCCompldBy, New System.EventArgs())
    End Sub

    Private Sub txtCCompldBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCCompldBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtCCompldBy, lblCCompldBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCompldBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompldBy.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCompldBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompldBy.DoubleClick
        Call cmdSearchCompldBy_Click(cmdSearchCompldBy, New System.EventArgs())
    End Sub

    Private Sub txtCompldBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCompldBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchCompldBy_Click(cmdSearchCompldBy, New System.EventArgs())
    End Sub

    Private Sub txtCompldBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCompldBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtCompldBy, lblCompldBy) = False Then
            Cancel = True
        Else
            txtCCompldBy.Text = txtCompldBy.Text
            lblCCompldBy.Text = lblCompldBy.Text
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Function ValidateEMP(ByRef pTextBox As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label) As Boolean
        On Error GoTo ValEMP
        Dim SqlStr As String
        ValidateEMP = True
        If Trim(pTextBox.Text) = "" Then pLable.Text = "" : Exit Function

        pTextBox.Text = VB6.Format(pTextBox.Text, "000000")
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
        If ADDMode = True Then
            SqlStr = SqlStr & vbCrLf & "  AND EMP_LEAVE_DATE IS NULL "
        Else
            SqlStr = SqlStr & vbCrLf _
                & " AND (EMP_LEAVE_DATE IS NULL OR (EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND EMP_LEAVE_DATE <= TO_DATE('" & VB6.Format(txtSlipDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')))"
        End If

        If MainClass.ValidateWithMasterTable(pTextBox.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Employee Does Not Exist In Master.")
            ValidateEMP = False
        Else
            pLable.Text = MasterNo
        End If

        Exit Function
ValEMP:
        MsgBox(Err.Description)
    End Function

    Private Sub txtComptDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtComptDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCComptDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCComptDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtComptDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtComptDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtComptDate.Text) = "" Or Trim(txtComptDate.Text) = "__/__/____" Then GoTo EventExitSub
        If Not IsDate(txtComptDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
            GoTo EventExitSub
        Else
            If CheckDate(txtComptDate) = False Then
                Cancel = True
            Else
                txtCComptDate.Text = txtComptDate.Text
                Call CalcTot()
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCComptDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCComptDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtCComptDate.Text) = "" Or Trim(txtCComptDate.Text) = "__/__/____" Then GoTo EventExitSub
        If Not IsDate(txtCComptDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
            GoTo EventExitSub
        Else
            If CheckDate(txtCComptDate) = False Then
                Cancel = True
            Else
                Call CalcTot()
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtComptTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtComptTime.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCComptTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCComptTime.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtComptTime_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtComptTime.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtComptTime.Text) = "" Or Trim(txtComptTime.Text) = "__:__" Then GoTo EventExitSub
        If CheckTimeFormat(txtComptTime) = False Then Cancel = True : GoTo EventExitSub
        txtComptTime.Text = VB6.Format(txtComptTime.Text, "HH:MM")
        If CheckTime(txtComptTime) = False Then Cancel = True : GoTo EventExitSub
        txtCComptTime.Text = txtComptTime.Text
        Call CalcTot()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCComptTime_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCComptTime.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtCComptTime.Text) = "" Or Trim(txtCComptTime.Text) = "__:__" Then GoTo EventExitSub
        If CheckTimeFormat(txtCComptTime) = False Then Cancel = True : GoTo EventExitSub
        txtCComptTime.Text = VB6.Format(txtCComptTime.Text, "HH:MM")
        If CheckTime(txtCComptTime) = False Then Cancel = True : GoTo EventExitSub
        Call CalcTot()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDeputDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeputDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDeputDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeputDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDeputDate.Text) = "" Or Trim(txtDeputDate.Text) = "__/__/____" Then GoTo EventExitSub
        If Not IsDate(txtDeputDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        Else
            If CheckDate(txtDeputDate) = False Then Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function CheckDate(ByRef pTextDate As System.Windows.Forms.MaskedTextBox) As Boolean
        On Error GoTo ERR1
        CheckDate = True
        If pTextDate.Name = txtSlipDate.Name Then
            If Trim(txtSlipDate.Text) <> "" And Not IsDate(txtSlipDate.Text) And Trim(txtDeputDate.Text) <> "" And Not IsDate(txtDeputDate.Text) Then
                If CDate(txtSlipDate.Text) > CDate(txtDeputDate.Text) Then
                    MsgBox("Slip Date cann't be greater than Deputed Date")
                    CheckDate = False
                    Exit Function
                End If
            End If

            If Trim(txtSlipDate.Text) <> "" And Not IsDate(txtSlipDate.Text) And Trim(txtComptDate.Text) <> "" And Not IsDate(txtComptDate.Text) Then
                If CDate(txtSlipDate.Text) > CDate(txtComptDate.Text) Then
                    MsgBox("Slip Date cann't be greater than Completion Date")
                    CheckDate = False
                    Exit Function
                End If
            End If

            If Trim(txtSlipDate.Text) <> "" And Not IsDate(txtSlipDate.Text) And Trim(txtCComptDate.Text) <> "" And Not IsDate(txtCComptDate.Text) Then
                If CDate(txtSlipDate.Text) > CDate(txtCComptDate.Text) Then
                    MsgBox("Slip Date cann't be greater than Completion Date At Complainer Site.")
                    CheckDate = False
                    Exit Function
                End If
            End If

        ElseIf pTextDate.Name = txtDeputDate.Name Then
            If Trim(txtDeputDate.Text) <> "" And Not IsDate(txtDeputDate.Text) And Trim(txtSlipDate.Text) <> "" And Not IsDate(txtSlipDate.Text) Then
                If CDate(txtDeputDate.Text) < CDate(txtSlipDate.Text) Then
                    MsgBox("Deputed Date cann't be less than Slip Date")
                    CheckDate = False
                    Exit Function
                End If
            End If

            If Trim(txtDeputDate.Text) <> "" And Not IsDate(txtDeputDate.Text) And Trim(txtComptDate.Text) <> "" And Not IsDate(txtComptDate.Text) Then
                If CDate(txtDeputDate.Text) > CDate(txtComptDate.Text) Then
                    MsgBox("Deputed Date cann't be greater than Completion Date")
                    CheckDate = False
                    Exit Function
                End If
            End If

            If Trim(txtDeputDate.Text) <> "" And Not IsDate(txtDeputDate.Text) And Trim(txtCComptDate.Text) <> "" And Not IsDate(txtCComptDate.Text) Then
                If CDate(txtDeputDate.Text) > CDate(txtCComptDate.Text) Then
                    MsgBox("Deputed Date cann't be greater than Completion Date At Complainer Site.")
                    CheckDate = False
                    Exit Function
                End If
            End If

        ElseIf pTextDate.Name = txtComptDate.Name Then
            If Trim(txtComptDate.Text) <> "" And Not IsDate(txtComptDate.Text) And Trim(txtSlipDate.Text) <> "" And Not IsDate(txtSlipDate.Text) Then
                If CDate(txtComptDate.Text) < CDate(txtSlipDate.Text) Then
                    MsgBox("Completion Date cann't be less than Slip Date")
                    CheckDate = False
                    Exit Function
                End If
            End If

            If Trim(txtComptDate.Text) <> "" And Not IsDate(txtComptDate.Text) And Trim(txtDeputDate.Text) <> "" And Not IsDate(txtDeputDate.Text) Then
                If CDate(txtComptDate.Text) < CDate(txtDeputDate.Text) Then
                    MsgBox("Completion Date cann't be less than Deputed Date")
                    CheckDate = False
                    Exit Function
                End If
            End If

            If Trim(txtComptDate.Text) <> "" And Not IsDate(txtComptDate.Text) And Trim(txtCComptDate.Text) <> "" And Not IsDate(txtCComptDate.Text) Then
                If CDate(txtComptDate.Text) < CDate(txtCComptDate.Text) Then
                    MsgBox("Completion Date cann't be less than Completion Date At Complainer Site.")
                    CheckDate = False
                    Exit Function
                End If
            End If
        ElseIf pTextDate.Name = txtCComptDate.Name Then
            If Trim(txtCComptDate.Text) <> "" And Not IsDate(txtCComptDate.Text) And Trim(txtSlipDate.Text) <> "" And Not IsDate(txtSlipDate.Text) Then
                If CDate(txtCComptDate.Text) < CDate(txtSlipDate.Text) Then
                    MsgBox("Completion Date At Complainer Site cann't be less than Slip Date")
                    CheckDate = False
                    Exit Function
                End If
            End If

            If Trim(txtCComptDate.Text) <> "" And Not IsDate(txtCComptDate.Text) And Trim(txtDeputDate.Text) <> "" And Not IsDate(txtDeputDate.Text) Then
                If CDate(txtCComptDate.Text) < CDate(txtDeputDate.Text) Then
                    MsgBox("Completion Date At Complainer Site cann't be less than Deputed Date")
                    CheckDate = False
                    Exit Function
                End If
            End If

            If Trim(txtCComptDate.Text) <> "" And Not IsDate(txtCComptDate.Text) And Trim(txtComptDate.Text) <> "" And Not IsDate(txtComptDate.Text) Then
                If CDate(txtCComptDate.Text) < CDate(txtComptDate.Text) Then
                    MsgBox("Completion Date At Complainer Site cann't be less than Completion Date.")
                    CheckDate = False
                    Exit Function
                End If
            End If
        End If
        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function
    Private Function CheckTime(ByRef pTextTime As System.Windows.Forms.MaskedTextBox) As Boolean
        CheckTime = True
        If pTextTime.Text = txtSlipTime.Text Then
            If (Trim(txtSlipDate.Text) <> "" And Not IsDate(txtSlipDate.Text)) And (Trim(txtDeputDate.Text) <> "" And Not IsDate(txtDeputDate.Text)) Then
                If CDate(txtSlipDate.Text) = CDate(txtDeputDate.Text) Then
                    If Val(Replace(txtDeputTime.Text, ":", ".")) > 0 Then
                        If Val(Replace(txtSlipTime.Text, ":", ".")) > Val(Replace(txtDeputTime.Text, ":", ".")) Then
                            MsgBox("Slip Time cann't be greater than Deputed Time")
                            CheckTime = False
                            Exit Function
                        End If
                    End If
                End If
            End If

            If (Trim(txtSlipDate.Text) <> "" And Not IsDate(txtSlipDate.Text)) And (Trim(txtComptDate.Text) <> "" And Not IsDate(txtComptDate.Text)) Then
                If CDate(txtSlipDate.Text) = CDate(txtComptDate.Text) Then
                    If Val(Replace(txtComptTime.Text, ":", ".")) > 0 Then
                        If Val(Replace(txtSlipTime.Text, ":", ".")) > Val(Replace(txtComptTime.Text, ":", ".")) Then
                            MsgBox("Slip Time cann't be greater than Completion Time")
                            CheckTime = False
                            Exit Function
                        End If
                    End If
                End If
            End If

            If (Trim(txtSlipDate.Text) <> "" And Not IsDate(txtSlipDate.Text)) And (Trim(txtCComptDate.Text) <> "" And Not IsDate(txtCComptDate.Text)) Then
                If CDate(txtSlipDate.Text) = CDate(txtCComptDate.Text) Then
                    If Val(Replace(txtCComptTime.Text, ":", ".")) > 0 Then
                        If Val(Replace(txtSlipTime.Text, ":", ".")) > Val(Replace(txtCComptTime.Text, ":", ".")) Then
                            MsgBox("Slip Time cann't be greater than Completion Time At Complainer Site.")
                            CheckTime = False
                            Exit Function
                        End If
                    End If
                End If
            End If

        ElseIf pTextTime.Text = txtDeputTime.Text Then
            If (Trim(txtDeputDate.Text) <> "" And Not IsDate(txtDeputDate.Text)) And (Trim(txtSlipDate.Text) <> "" And Not IsDate(txtSlipDate.Text)) Then
                If CDate(txtDeputDate.Text) = CDate(txtSlipDate.Text) Then
                    If Val(Replace(txtDeputTime.Text, ":", ".")) < Val(Replace(txtSlipTime.Text, ":", ".")) Then
                        MsgBox("Deputed Time cann't be less than Slip Time")
                        CheckTime = False
                        Exit Function
                    End If
                End If
            End If

            If (Trim(txtDeputDate.Text) <> "" And Not IsDate(txtDeputDate.Text)) And (Trim(txtComptDate.Text) <> "" And Not IsDate(txtComptDate.Text)) Then
                If CDate(txtDeputDate.Text) = CDate(txtComptDate.Text) Then
                    If Val(Replace(txtComptTime.Text, ":", ".")) > 0 Then
                        If Val(Replace(txtDeputTime.Text, ":", ".")) > Val(Replace(txtComptTime.Text, ":", ".")) Then
                            MsgBox("Deputed Time cann't be greater than Completion Time")
                            CheckTime = False
                            Exit Function
                        End If
                    End If
                End If
            End If

            If (Trim(txtDeputDate.Text) <> "" And Not IsDate(txtDeputDate.Text)) And (Trim(txtCComptDate.Text) <> "" And Not IsDate(txtCComptDate.Text)) Then
                If CDate(txtDeputDate.Text) = CDate(txtCComptDate.Text) Then
                    If Val(Replace(txtCComptTime.Text, ":", ".")) > 0 Then
                        If Val(Replace(txtDeputTime.Text, ":", ".")) > Val(Replace(txtCComptTime.Text, ":", ".")) Then
                            MsgBox("Deputed Time cann't be greater than Completion Time At Complainer Site.")
                            CheckTime = False
                            Exit Function
                        End If
                    End If
                End If
            End If

        ElseIf pTextTime.Text = txtComptTime.Text Then
            If (Trim(txtComptDate.Text) <> "" And Not IsDate(txtComptDate.Text)) And (Trim(txtSlipDate.Text) <> "" And Not IsDate(txtSlipDate.Text)) Then
                If CDate(txtComptDate.Text) = CDate(txtSlipDate.Text) Then
                    If Val(Replace(txtComptTime.Text, ":", ".")) < Val(Replace(txtSlipTime.Text, ":", ".")) Then
                        MsgBox("Completion Time cann't be less than Slip Time")
                        CheckTime = False
                        Exit Function
                    End If
                End If
            End If

            If (Trim(txtComptDate.Text) <> "" And Not IsDate(txtComptDate.Text)) And (Trim(txtDeputDate.Text) <> "" And Not IsDate(txtDeputDate.Text)) Then
                If CDate(txtComptDate.Text) = CDate(txtDeputDate.Text) Then
                    If Val(Replace(txtComptTime.Text, ":", ".")) < Val(Replace(txtDeputTime.Text, ":", ".")) Then
                        MsgBox("Completion Time cann't be less than Deputed Time")
                        CheckTime = False
                        Exit Function
                    End If
                End If
            End If
            '        If Trim(txtComptDate.Text) <> "" And Trim(txtCComptDate.Text) <> "" Then					
            '            If CDate(txtComptDate.Text) = CDate(txtCComptDate.Text) Then					
            '                If Val(Replace(txtComptTime.Text, ":", ".")) > Val(Replace(txtCComptTime.Text, ":", ".")) Then					
            '                    MsgBox "Completion Time cann't be Greater than Completion Time At Complainer Site."					
            '                    CheckTime = False					
            '                    Exit Function					
            '                End If					
            '            End If					
            '        End If					
        ElseIf pTextTime.Text = txtCComptTime.Text Then
            If (Trim(txtCComptDate.Text) <> "" And Not IsDate(txtCComptDate.Text)) And (Trim(txtSlipDate.Text) <> "" And Not IsDate(txtSlipDate.Text)) Then
                If CDate(txtCComptDate.Text) = CDate(txtSlipDate.Text) Then
                    If Val(Replace(txtCComptTime.Text, ":", ".")) < Val(Replace(txtSlipTime.Text, ":", ".")) Then
                        MsgBox("Completion Time At Complainer Site cann't be less than Slip Time")
                        CheckTime = False
                        Exit Function
                    End If
                End If
            End If

            If (Trim(txtCComptDate.Text) <> "" And Not IsDate(txtCComptDate.Text)) And (Trim(txtDeputDate.Text) <> "" And Not IsDate(txtDeputDate.Text)) Then
                If CDate(txtCComptDate.Text) = CDate(txtDeputDate.Text) Then
                    If Val(Replace(txtCComptTime.Text, ":", ".")) < Val(Replace(txtDeputTime.Text, ":", ".")) Then
                        MsgBox("Completion Time  At Complainer Site cann't be less than Deputed Time")
                        CheckTime = False
                        Exit Function
                    End If
                End If
            End If
            If (Trim(txtCComptDate.Text) <> "" And Not IsDate(txtCComptDate.Text)) And (Trim(txtComptDate.Text) <> "" And Not IsDate(txtComptDate.Text)) Then
                If CDate(txtCComptDate.Text) = CDate(txtComptDate.Text) Then
                    If Val(Replace(txtCComptTime.Text, ":", ".")) < Val(Replace(txtComptTime.Text, ":", ".")) Then
                        MsgBox("Completion Time  At Complainer Site cann't be less than Completion Time .")
                        CheckTime = False
                        Exit Function
                    End If
                End If
            End If
        End If

    End Function
    Private Sub CalcTot()
        Dim mStartDate As String
        Dim mStartTime As String
        Dim mStartDateTime As String
        Dim mEndDate As String
        Dim mEndTime As String
        Dim mEndDateTime As String
        Dim mTotHour As Double
        Dim mTotMin As Double
        Dim mTotTime As Double

        '************* FOR TOTAL TIME AT COMPLAINEE SITE*******************					
        mStartDate = Trim(txtSlipDate.Text)
        mStartTime = Trim(txtSlipTime.Text)
        mEndDate = Trim(txtComptDate.Text)
        mEndTime = Trim(txtComptTime.Text)

        If mStartDate = "" Or mStartDate = "__/__/____" Or Trim(mStartDate) = "/  /" Or Not IsDate(mStartDate) Then GoTo ForComplainerSite
        If mStartTime = "" Or mStartTime = "__:__" Or Trim(mStartTime) = ":" Or Not IsDate(mStartTime) Then GoTo ForComplainerSite
        If mEndDate = "" Or mEndDate = "__/__/____" Or Trim(mEndDate) = "/  /" Or Not IsDate(mEndDate) Then GoTo ForComplainerSite
        If mEndTime = "" Or mEndTime = "__:__" Or Trim(mEndTime) = ":" Or Not IsDate(mEndTime) Then GoTo ForComplainerSite
        mStartDateTime = mStartDate & " " & mStartTime
        mEndDateTime = mEndDate & " " & mEndTime

        mTotHour = Int(DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mStartDateTime), CDate(mEndDateTime)) / 60)
        mTotMin = DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mStartDateTime), CDate(mEndDateTime)) Mod 60
        If mTotMin < 10 Then
            mTotMin = mTotMin / 100
            mTotTime = mTotHour + mTotMin
        Else
            mTotTime = CDbl(mTotHour & "." & mTotMin)
        End If

        txtTotalTime.Text = CStr(Val(CStr(mTotTime)))


        '************* FOR TOTAL TIME AT COMPLAINER SITE *******************					
ForComplainerSite:
        mStartDate = Trim(txtSlipDate.Text)
        mStartTime = Trim(txtSlipTime.Text)
        mEndDate = Trim(txtCComptDate.Text)
        mEndTime = Trim(txtCComptTime.Text)

        If mStartDate = "" Or mStartDate = "__/__/____" Or Trim(mStartDate) = "/  /" Or Not IsDate(mStartDate) Then Exit Sub
        If mStartTime = "" Or mStartTime = "__:__" Or Trim(mStartTime) = ":" Or Not IsDate(mStartTime) Then Exit Sub
        If mEndDate = "" Or mEndDate = "__/__/____" Or Trim(mEndDate) = "/  /" Or Not IsDate(mEndDate) Then Exit Sub
        If mEndTime = "" Or mEndTime = "__:__" Or Trim(mEndTime) = ":" Or Not IsDate(mEndTime) Then Exit Sub
        mStartDateTime = mStartDate & " " & mStartTime
        mEndDateTime = mEndDate & " " & mEndTime

        mTotHour = Int(DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mStartDateTime), CDate(mEndDateTime)) / 60)
        mTotMin = DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mStartDateTime), CDate(mEndDateTime)) Mod 60
        If mTotMin < 10 Then
            mTotMin = mTotMin / 100
            mTotTime = mTotHour + mTotMin
        Else
            mTotTime = CDbl(mTotHour & "." & mTotMin)
        End If

        txtCTotalTime.Text = CStr(Val(CStr(mTotTime)))

    End Sub

    Private Sub txtDeputPerson_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeputPerson.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDeputPerson_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeputPerson.DoubleClick
        Call cmdSearchDeputPerson_Click(cmdSearchDeputPerson, New System.EventArgs())
    End Sub

    Private Sub txtDeputPerson_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDeputPerson.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDeputPerson_Click(cmdSearchDeputPerson, New System.EventArgs())
    End Sub

    Private Sub txtDeputPerson_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeputPerson.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtDeputPerson, lblDeputPerson) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDeputRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeputRemarks.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDeputRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDeputRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtDeputRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDeputTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeputTime.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDeputTime_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeputTime.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDeputTime.Text) = "" Or Trim(txtDeputTime.Text) = "__:__" Then GoTo EventExitSub
        If CheckTimeFormat(txtDeputTime) = False Then Cancel = True : GoTo EventExitSub
        txtDeputTime.Text = VB6.Format(txtDeputTime.Text, "HH:MM")
        If CheckTime(txtDeputTime) = False Then Cancel = True
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtFromDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFromDept.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtFromDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFromDept.DoubleClick
        Call cmdSearchFromDept_Click(cmdSearchFromDept, New System.EventArgs())
    End Sub

    Private Sub TxtFromDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFromDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtFromDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtFromDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtFromDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchFromDept_Click(cmdSearchFromDept, New System.EventArgs())
    End Sub

    Private Sub TxtFromDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFromDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateDept(txtFromDept, lblFromDept) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub
    Private Function ValidateDept(ByRef pTextBox As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label) As Boolean
        On Error GoTo ValERR
        Dim SqlStr As String
        ValidateDept = True
        If Trim(pTextBox.Text) = "" Then Exit Function
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(pTextBox.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Deparment Does Not Exist In Master.")
            ValidateDept = False
        Else
            pLable.Text = MasterNo
        End If
        Exit Function
ValERR:
        MsgBox(Err.Description)
    End Function

    Private Sub txtToolNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToolNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToolNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToolNo.DoubleClick
        Call cmdSearchMacNo_Click(cmdSearchMacNo, New System.EventArgs())
    End Sub

    Private Sub txtToolNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtToolNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtToolNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtToolNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtToolNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMacNo_Click(cmdSearchMacNo, New System.EventArgs())
    End Sub
    Private Sub txtToolNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtToolNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValMacERR
        Dim SqlStr As String
        Dim mToolItemCode As String
        Dim mItemCode As String

        If Trim(txtToolNo.Text) = "" Then GoTo EventExitSub
        '					
        '					
        '    SqlStr = " SELECT A.TOOL_NO, B.ITEM_SHORT_DESC, C.OPR_DESC " & vbCrLf _					
        ''            & " FROM TOL_TOOLINFO_MST A, INV_ITEM_MST B, PRD_OPR_MST C" & vbCrLf _					
        ''            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _					
        ''            & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _					
        ''            & " AND A.TOOL_ITEM_CODE=B.ITEM_CODE " & vbCrLf _					
        ''            & " AND A.COMPANY_CODE=C.COMPANY_CODE " & vbCrLf _					
        ''            & " AND A.OPR_CODE=C.OPR_CODE "					
        '					
        '    SqlStr = SqlStr & vbCrLf & " AND B.ITEM_CLASSIFICATION='T'"					
        '    SqlStr = SqlStr & vbCrLf & " AND A.TOOL_UB='N' AND A.TOOL_STATUS='O'"					
        '					

        If ADDMode = True Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TOOL_STATUS='O'"
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        If MainClass.ValidateWithMasterTable(txtToolNo.Text, "TOOL_NO", "TOOL_ITEM_CODE", "TOL_TOOLINFO_MST", PubDBCn, MasterNo,  , SqlStr) = False Then
            MsgBox("Tool Does Not Exist In Master.")
            Cancel = True
        Else
            mToolItemCode = MasterNo
            If MainClass.ValidateWithMasterTable(Trim(mToolItemCode), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CLASSIFICATION='T'") = True Then
                lblToolNo.Text = MasterNo
            Else
                MsgBox("Invalid Tool Item Code.")
                Cancel = True
            End If
        End If

        If MainClass.ValidateWithMasterTable(txtToolNo.Text, "TOOL_NO", "ITEM_CODE", "TOL_TOOLINFO_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
            mItemCode = MasterNo
            If MainClass.ValidateWithMasterTable(Trim(mItemCode), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CLASSIFICATION='T'") = True Then
                txtPartName.Text = MasterNo
            Else
                txtPartName.Text = ""
            End If
        End If

        GoTo EventExitSub
ValMacERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtProblem_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProblem.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProblem_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProblem.DoubleClick
        Call cmdSearchProblem_Click(cmdSearchProblem, New System.EventArgs())
    End Sub

    Private Sub txtProblem_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProblem.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtProblem.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProblem_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProblem.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchProblem_Click(cmdSearchProblem, New System.EventArgs())
    End Sub

    Private Sub txtProblem_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProblem.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtProblem.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtProblem.Text, "PROB_CODE", "PROB_DESC", "TOL_BDPROBLEMS_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Break Down Problem Does Not Exist In Master.")
            Cancel = True
        Else
            lblProblem.Text = MasterNo
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtReason_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReason.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReason_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReason.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtReason.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSlipDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSlipDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSlipDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtSlipDate.Text) = "" Or Trim(txtSlipDate.Text) = "__/__/____" Then GoTo EventExitSub
        If Not IsDate(txtSlipDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
            GoTo EventExitSub
        Else
            If CheckDate(txtSlipDate) = False Then Cancel = True : GoTo EventExitSub
        End If
        If FYChk((txtSlipDate.Text)) = False Then
            If txtSlipDate.Enabled = True Then txtSlipDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        Call CalcTot()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1()
        On Error GoTo ShowErrPart
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        Clear1()
        If Not RsBreakDownMain.EOF Then
            IsShowing = True

            lblMkey.Text = IIf(IsDBNull(RsBreakDownMain.Fields("AUTO_KEY_BDSLIP").Value), "", RsBreakDownMain.Fields("AUTO_KEY_BDSLIP").Value)
            txtSlipNo.Text = IIf(IsDBNull(RsBreakDownMain.Fields("AUTO_KEY_BDSLIP").Value), "", RsBreakDownMain.Fields("AUTO_KEY_BDSLIP").Value)
            txtSlipDate.Text = IIf(IsDBNull(RsBreakDownMain.Fields("SLIP_DATE").Value), "__/__/____", RsBreakDownMain.Fields("SLIP_DATE").Value)
            txtSlipTime.Text = VB6.Format(IIf(IsDBNull(RsBreakDownMain.Fields("BRK_DWN_TIME").Value), "__:__", RsBreakDownMain.Fields("BRK_DWN_TIME").Value), "HH:MM")
            txtFromDept.Text = IIf(IsDBNull(RsBreakDownMain.Fields("FROM_DEPT_CODE").Value), "", RsBreakDownMain.Fields("FROM_DEPT_CODE").Value)
            TxtFromDept_Validating(txtFromDept, New System.ComponentModel.CancelEventArgs(False))
            txtToDept.Text = IIf(IsDBNull(RsBreakDownMain.Fields("TO_DEPT_CODE").Value), "", RsBreakDownMain.Fields("TO_DEPT_CODE").Value)
            txtToDept_Validating(txtToDept, New System.ComponentModel.CancelEventArgs(False))
            txtToolNo.Text = IIf(IsDBNull(RsBreakDownMain.Fields("TOOL_NO").Value), "", RsBreakDownMain.Fields("TOOL_NO").Value)
            txtToolNo_Validating(txtToolNo, New System.ComponentModel.CancelEventArgs(False))
            txtCompldBy.Text = IIf(IsDBNull(RsBreakDownMain.Fields("COMPLAINT_BY").Value), "", RsBreakDownMain.Fields("COMPLAINT_BY").Value)
            txtCompldBy_Validating(txtCompldBy, New System.ComponentModel.CancelEventArgs(False))
            txtReason.Text = IIf(IsDBNull(RsBreakDownMain.Fields("SUSPECTED_REASON").Value), "", RsBreakDownMain.Fields("SUSPECTED_REASON").Value)
            txtProblem.Text = IIf(IsDBNull(RsBreakDownMain.Fields("PROBLEM_FACED").Value), "", RsBreakDownMain.Fields("PROBLEM_FACED").Value)
            txtProblem_Validating(txtProblem, New System.ComponentModel.CancelEventArgs(False))
            txtDeputPerson.Text = IIf(IsDBNull(RsBreakDownMain.Fields("DEPU_EMP_CODE").Value), "", RsBreakDownMain.Fields("DEPU_EMP_CODE").Value)
            txtDeputPerson_Validating(txtDeputPerson, New System.ComponentModel.CancelEventArgs(False))
            txtDeputDate.Text = IIf(IsDBNull(RsBreakDownMain.Fields("DEPU_DATE").Value), "__/__/____", RsBreakDownMain.Fields("DEPU_DATE").Value)
            txtDeputTime.Text = VB6.Format(IIf(IsDBNull(RsBreakDownMain.Fields("DEPU_TIME").Value), "__:__", RsBreakDownMain.Fields("DEPU_TIME").Value), "HH:MM")
            If RsBreakDownMain.Fields("ITEM_CONSUMED").Value = "Y" Then
                chkItemConsumed.CheckState = System.Windows.Forms.CheckState.Checked
            ElseIf RsBreakDownMain.Fields("ITEM_CONSUMED").Value = "N" Then
                chkItemConsumed.CheckState = System.Windows.Forms.CheckState.Unchecked
            End If
            txtDeputRemarks.Text = IIf(IsDBNull(RsBreakDownMain.Fields("DEPU_REMARKS").Value), "", RsBreakDownMain.Fields("DEPU_REMARKS").Value)
            txtSlipRecvdBy.Text = IIf(IsDBNull(RsBreakDownMain.Fields("SLIP_RECEIVED_BY").Value), "", RsBreakDownMain.Fields("SLIP_RECEIVED_BY").Value)
            txtSlipRecvdBy_Validating(txtSlipRecvdBy, New System.ComponentModel.CancelEventArgs(False))
            txtComptDate.Text = IIf(IsDBNull(RsBreakDownMain.Fields("COMPLETION_DATE").Value), "__/__/____", RsBreakDownMain.Fields("COMPLETION_DATE").Value)
            txtComptTime.Text = VB6.Format(IIf(IsDBNull(RsBreakDownMain.Fields("COMPLETION_TIME").Value), "__:__", RsBreakDownMain.Fields("COMPLETION_TIME").Value), "HH:MM")
            txtTotalTime.Text = IIf(IsDBNull(RsBreakDownMain.Fields("TOTHOUR").Value), "", RsBreakDownMain.Fields("TOTHOUR").Value)
            txtCComptDate.Text = IIf(IsDBNull(RsBreakDownMain.Fields("CCOMPLET_DATE").Value), "__/__/____", RsBreakDownMain.Fields("CCOMPLET_DATE").Value)
            txtCComptTime.Text = VB6.Format(IIf(IsDBNull(RsBreakDownMain.Fields("CCOMPLET_TIME").Value), "__:__", RsBreakDownMain.Fields("CCOMPLET_TIME").Value), "HH:MM")
            txtCCompldBy.Text = IIf(IsDBNull(RsBreakDownMain.Fields("CCOMPLAINT_BY").Value), "", RsBreakDownMain.Fields("CCOMPLAINT_BY").Value)
            txtCCompldBy_Validating(txtCCompldBy, New System.ComponentModel.CancelEventArgs(False))
            txtCTotalTime.Text = IIf(IsDBNull(RsBreakDownMain.Fields("TOTCHOUR").Value), "", RsBreakDownMain.Fields("TOTCHOUR").Value)
            txtRemarks.Text = IIf(IsDBNull(RsBreakDownMain.Fields("COMPLETION_REMARKS").Value), "", RsBreakDownMain.Fields("COMPLETION_REMARKS").Value)



            mDivisionCode = IIf(IsDBNull(RsBreakDownMain.Fields("DIV_CODE").Value), -1, RsBreakDownMain.Fields("DIV_CODE").Value)

            If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionDesc = Trim(MasterNo)
                cboDivision.Text = mDivisionDesc
            End If
            cboDivision.Enabled = False

            Call ShowDetail1(mDivisionCode)
            Call MakeEnableDesableField(False)
            Call CalcTot()
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = False
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsBreakDownMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume					
    End Sub
    Private Sub ShowDetail1(ByRef mDivisionCode As Double)
        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemName As String
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM TOL_BREAKDOWN_DET " & vbCrLf & " WHERE AUTO_KEY_BDSLIP=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBreakDownDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsBreakDownDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = mItemCode

                SprdMain.Col = ColItemName
                MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemName = MasterNo
                SprdMain.Text = mItemName

                SprdMain.Col = ColStockQty
                SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtSlipDate.Text), Trim(IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)), (txtToDept.Text), "ST", "", ConWH, mDivisionCode, ConStockRefType_BDT, CDbl(txtSlipNo.Text))) '''+ GetSavedQty(pItemCode)					

                SprdMain.Col = ColUom
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value))

                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(Trim(IIf(IsDBNull(.Fields("ITEM_QTY").Value), "", .Fields("ITEM_QTY").Value))))

                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(Trim(IIf(IsDBNull(.Fields("ITEM_RATE").Value), "", .Fields("ITEM_RATE").Value))))

                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(Trim(IIf(IsDBNull(.Fields("ITEM_AMOUNT").Value), "", .Fields("ITEM_AMOUNT").Value))))

                SprdMain.Col = ColSavedItemCode
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                SprdMain.Col = ColSavedQty
                SprdMain.Text = CStr(Val(Trim(IIf(IsDBNull(.Fields("ITEM_QTY").Value), "", .Fields("ITEM_QTY").Value))))

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

        If Len(txtSlipNo.Text) < 6 Then
            txtSlipNo.Text = txtSlipNo.Text & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mSlipNo = Val(txtSlipNo.Text)

        If MODIFYMode = True And RsBreakDownMain.BOF = False Then xMkey = RsBreakDownMain.Fields("AUTO_KEY_BDSLIP").Value

        SqlStr = "SELECT * FROM TOL_BREAKDOWN_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_BDSLIP=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBreakDownMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsBreakDownMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM TOL_BREAKDOWN_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND AUTO_KEY_BDSLIP=" & Val(CStr(xMkey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBreakDownMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtSlipDate.Enabled = mMode
        txtSlipTime.Enabled = mMode
        txtFromDept.Enabled = mMode
        cmdSearchFromDept.Enabled = mMode
        txtToDept.Enabled = mMode
        cmdSearchToDept.Enabled = mMode
        txtToolNo.Enabled = mMode
        cmdSearchMacNo.Enabled = mMode
        txtCompldBy.Enabled = mMode
        cmdSearchCompldBy.Enabled = mMode
        '    txtProblem.Enabled = mMode					
        '    cmdSearchProblem.Enabled = mMode					
        txtTotalTime.Enabled = False
        txtCTotalTime.Enabled = False

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
    Private Sub ReportOnBreakDownMain(ByRef Mode As Crystal.DestinationConstants)

    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnBreakDownMain(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnBreakDownMain(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtSlipRecvdBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipRecvdBy.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSlipRecvdBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipRecvdBy.DoubleClick
        Call cmdSearchSlipRecvdBy_Click(cmdSearchSlipRecvdBy, New System.EventArgs())
    End Sub
    Private Sub txtSlipRecvdBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSlipRecvdBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchSlipRecvdBy_Click(cmdSearchSlipRecvdBy, New System.EventArgs())
    End Sub

    Private Sub txtSlipRecvdBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSlipRecvdBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtSlipRecvdBy, lblSlipRecvdBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSlipTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipTime.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSlipTime_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSlipTime.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtSlipTime.Text) = "" Or Trim(txtSlipTime.Text) = "__:__" Then GoTo EventExitSub
        If CheckTimeFormat(txtSlipTime) = False Then Cancel = True : GoTo EventExitSub
        txtSlipTime.Text = VB6.Format(txtSlipTime.Text, "HH:MM")
        If CheckTime(txtSlipTime) = False Then Cancel = True : GoTo EventExitSub
        Call CalcTot()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function CheckTimeFormat(ByRef pTextTime As System.Windows.Forms.MaskedTextBox) As Boolean
        On Error GoTo ERR1
        CheckTimeFormat = True
        If InStr(1, pTextTime.Text, ":", CompareMethod.Text) <= 0 Then
            MsgBox("Time should be in format of HH24:MI with numeric value")
            CheckTimeFormat = False
        ElseIf InStr(1, pTextTime.Text, ":", CompareMethod.Text) > 0 Then
            If Not IsNumeric(VB.Left(pTextTime.Text, InStr(1, pTextTime.Text, ":", CompareMethod.Text) - 1)) = True Or Not IsNumeric(Mid(pTextTime.Text, InStr(1, pTextTime.Text, ":", CompareMethod.Text) + 1)) = True Then
                MsgBox("Time should be in format of HH24:MI with numeric value")
                CheckTimeFormat = False
            ElseIf Val(VB.Left(pTextTime.Text, InStr(1, pTextTime.Text, ":", CompareMethod.Text) - 1)) > 23 Then
                MsgBox("HH cann't be greater than 23")
                CheckTimeFormat = False
            ElseIf Val(Mid(pTextTime.Text, InStr(1, pTextTime.Text, ":", CompareMethod.Text) + 1)) > 59 Then
                MsgBox("MM cann't be greater than 59")
                CheckTimeFormat = False
            End If
        End If
        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function

    Private Sub txtToDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToDept.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToDept.DoubleClick
        Call cmdSearchToDept_Click(cmdSearchToDept, New System.EventArgs())
    End Sub

    Private Sub txtToDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtToDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtToDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtToDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtToDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchToDept_Click(cmdSearchToDept, New System.EventArgs())
    End Sub

    Private Sub txtToDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtToDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateDept(txtToDept, lblToDept) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub
End Class
