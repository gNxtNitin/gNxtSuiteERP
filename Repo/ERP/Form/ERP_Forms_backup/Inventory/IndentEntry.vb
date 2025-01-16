Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Imports System.ComponentModel
Friend Class FrmIndentEntry
    Inherits System.Windows.Forms.Form
    Dim RsIndentMain As ADODB.Recordset
    Dim RsIndentDetail As ADODB.Recordset
    'Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Dim CurMKey As String
    Dim SqlStr As String = ""

    Dim mBookType As String
    Dim mBookSubType As String

    Dim pmyMenu As String

    Private Const ConRowHeight As Short = 15

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColAddItemDesc As Short = 3
    Private Const ColMake As Short = 4
    Private Const ColCategory As Short = 5
    Private Const ColUnit As Short = 6
    Private Const ColLastPurDate As Short = 7
    Private Const ColLastSupplier As Short = 8
    Private Const ColLastPORate As Short = 9
    Private Const ColMaxLevel As Short = 10
    Private Const ColReOderLevel As Short = 11
    Private Const ColStock As Short = 12
    Private Const ColQtyReqd As Short = 13
    Private Const ColItemPriority As Short = 14
    Private Const ColPurpose As Short = 15
    Private Const ColRemarks As Short = 16
    Private Const ColReqDate As Short = 17
    Private Const ColIndentStatus As Short = 18
    Private Const ColIndentRejected As Short = 19
    Private Const ColQuotationApproved As Short = 20
    Private Const ColAPPRemarks As Short = 21
    Private Const ColConsiderQty As Short = 22

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Dim mIsAuthorisedUser As Boolean
    Private Sub chkHODApproval_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkHODApproval.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkApproval_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkApproval.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAutoIssue_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAutoIssue.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If chkAutoIssueToSS.CheckState = System.Windows.Forms.CheckState.Checked Then
            chkAutoIssue.CheckState = System.Windows.Forms.CheckState.Checked
        End If
    End Sub


    Private Sub chkAutoIssueToSS_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAutoIssueToSS.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If chkAutoIssueToSS.CheckState = System.Windows.Forms.CheckState.Checked Then
            chkAutoIssue.CheckState = System.Windows.Forms.CheckState.Checked
        End If
    End Sub


    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkSendBack_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSendBack.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtIndentBy.Text = PubUserID
            lblIndentBy.Text = PubUserName

            SprdMain.Enabled = True
            txtIndentNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsIndentMain.EOF = False Then RsIndentMain.MoveFirst()
            Show1()
            txtIndentNo.Enabled = True
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
        If ValidateBranchLocking((txtIndentDate.Text)) = True Then
            Exit Sub
        End If

        If ValidateBookLocking(PubDBCn, CInt(ConLockINDENT), txtIndentDate.Text) = True Then
            Exit Sub
        End If


        If txtIndentNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsIndentMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PUR_INDENT_HDR", (txtIndentNo.Text), RsIndentMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PUR_INDENT_HDR", "AUTO_KEY_INDENT", (lblmKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM PUR_INDENT_DET WHERE AUTO_KEY_INDENT=" & Val(lblmKey.Text) & "")
                PubDBCn.Execute("DELETE FROM PUR_INDENT_HDR WHERE AUTO_KEY_INDENT=" & Val(lblmKey.Text) & "")
                PubDBCn.CommitTrans()
                RsIndentMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        RsIndentMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdDeptSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDeptSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtDept.Text), "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", , , SqlStr) = True Then
            txtDept.Text = AcName
            txtDept_Validating(txtDept, New System.ComponentModel.CancelEventArgs(False))
            txtDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub TxtBillTm_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtBillTm.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCCentre_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCCentre.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCCentre_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCCentre.DoubleClick
        cmdCCSearch_Click(cmdCCSearch, New System.EventArgs())
    End Sub


    Private Sub txtCCentre_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCCentre.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCCentre.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCCentre_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCCentre.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdCCSearch_Click(cmdCCSearch, New System.EventArgs())
    End Sub


    Private Sub txtCCentre_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCCentre.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If Trim(txtCCentre.Text) = "" Then GoTo EventExitSub
        'txtCCentre.Text = VB6.Format(txtCCentre.Text, "000")

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then

        Else
            If Trim(txtDept.Text) = "" Then
                MsgInformation("Please Select Dept. First.")
                txtDept.Focus()
                GoTo EventExitSub
            End If
        End If




        SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf _
            & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf _
            & " AND IH.CC_CODE='" & MainClass.AllowSingleQuote((txtCCentre.Text)) & "'"

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
            If txtDept.Text <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"
            End If
        Else
            SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            lblCCentre.Text = IIf(IsDBNull(RsTemp.Fields("CC_DESC").Value), "", RsTemp.Fields("CC_DESC").Value)

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                txtDept.Text = IIf(IsDBNull(RsTemp.Fields("DEPT_CODE").Value), "", RsTemp.Fields("DEPT_CODE").Value)
                txtDept_Validating(txtDept, New System.ComponentModel.CancelEventArgs(False))
            End If

        Else
            MsgInformation("Invalid Cost Center Code for Department : " & Trim(txtDept.Text))
            Cancel = True
        End If

        '    If MainClass.ValidateWithMasterTable(txtCCentre.Text, "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '        lblCCentre.text = MasterNo
        '    Else
        '        MsgInformation "Invalid CostC Code"
        '        Cancel = True
        '    End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdCCSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCCSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""


        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then

        Else
            If Trim(txtDept.Text) = "" Then
                MsgInformation("Please Select Dept. First.")
                If txtDept.Enabled = True Then txtDept.Focus()
                Exit Sub
            End If
        End If



        SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf _
            & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE"

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
            If txtDept.Text <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"
            End If
        Else
            SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"
        End If

        '    If MainClass.SearchGridMaster(txtCCentre.Text, "FIN_CCENTER_HDR", "CC_CODE", "CC_DESC", , , SqlStr) = True Then
        If MainClass.SearchGridMasterBySQL2((txtCCentre.Text), SqlStr) = True Then
            txtCCentre.Text = AcName
            lblCCentre.Text = AcName1
            txtDept.Text = AcName2
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                txtDept_Validating(txtDept, New System.ComponentModel.CancelEventArgs(False))
            End If

            txtCCentre_Validating(txtCCentre, New System.ComponentModel.CancelEventArgs(False))
            If txtCCentre.Enabled = True Then txtCCentre.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDivision_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDivision.DoubleClick
        cmdDivSearch_Click(cmdDivSearch, New System.EventArgs())
    End Sub


    Private Sub txtDivision_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDivision.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDivision.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDivision_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDivision.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdDivSearch_Click(cmdDivSearch, New System.EventArgs())
    End Sub


    Private Sub txtDivision_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDivision.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtDivision.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtDivision.Text), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblDivision.Text = MasterNo
        Else
            MsgBox("Invalid Division Code.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdDivSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDivSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtDivision.Text), "INV_DIVISION_MST", "DIV_CODE", "DIV_DESC", , , SqlStr) = True Then
            txtDivision.Text = AcName
            txtDivision_Validating(txtDivision, New System.ComponentModel.CancelEventArgs(False))
            txtDivision.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtFinalAppDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFinalAppDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtHODAppDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHODAppDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIndentBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIndentBy.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtIndentBy_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIndentBy.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtIndentBy.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        SprdMain.Enabled = True
        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            If ApprovalStatus = True Then Exit Sub
            MainClass.ButtonStatus(Me, XRIGHT, RsIndentMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtIndentNo.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            txtIndentDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Function ApprovalStatus() As Boolean
        ApprovalStatus = False
        Dim mAppEMPCode As String = ""

        If MODIFYMode = True Then
            RsIndentMain.Requery()

            If PubSuperUser = "U" Then
                If VB.Right(lblBookType.Text, 1) <> "C" Then
                    mAppEMPCode = IIf(IsDBNull(RsIndentMain.Fields("APP_EMP_CODE").Value), "", RsIndentMain.Fields("APP_EMP_CODE").Value)

                    If Trim(mAppEMPCode) <> "" Then
                        MsgBox("This Indent Has Approved. So Can Not Be Modified.", MsgBoxStyle.Information)
                        ApprovalStatus = True
                        Exit Function
                    End If
                End If

                If Trim(RsIndentMain.Fields("APPROVAL_STATUS").Value) = "N" Then
                    MsgBox("This Indent Has Cancelled. So Can Not Be Modified.", MsgBoxStyle.Information)
                    ApprovalStatus = True
                    Exit Function
                End If
            End If
        End If
    End Function
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonIndent(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonIndent(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonIndent(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim mRPTName As String = ""

        Report1.Reset()
        SqlStr = ""
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Call SelectQry(SqlStr)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        mSubTitle = Trim(lblDivision.Text)
        mTitle = "I N D E N T"

        mRPTName = "\reports\Indent.rpt"

        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub SelectQry(ByRef mSqlStr As String)

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mItemRate As Double
        Dim mItemCode As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mStockQty As Double
        Dim mItemUOM As String = ""
        Dim mDivisionCode As Double
        Dim mLastSuppCustName As String = ""
        Dim mLastMrrDate As String = ""
        Dim mMaxLevel As Double

        '
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If MainClass.ValidateWithMasterTable(Trim(lblDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        SqlStr = " DELETE FROM TEMP_INDENT_PRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = " INSERT INTO TEMP_INDENT_PRN (" & vbCrLf _
            & " USERID, AUTO_KEY_INDENT, INDENT_DATE," & vbCrLf _
            & " SERIAL_NO, ITEM_CODE, ITEM_SHORT_DESC," & vbCrLf _
            & " ITEM_UOM, REQ_QTY, PRIORITY_LEVEL," & vbCrLf _
            & " ITEM_PURPOSE, REMARKS,CONSIDER_QTY," & vbCrLf _
            & " REQ_DATE, ITEM_RATE, CATEGORY_NAME, ADD_DESCRIPTION, MAKE) "

        SqlStr = SqlStr & vbCrLf _
            & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', IH.AUTO_KEY_INDENT, IH.INDENT_DATE," & vbCrLf _
            & " ID.SERIAL_NO, ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
            & " ID.ITEM_UOM, ID.REQ_QTY, ID.PRIORITY_LEVEL," & vbCrLf _
            & " ID.ITEM_PURPOSE, ID.REMARKS, ID.CONSIDER_QTY," & vbCrLf _
            & " ID.REQ_DATE, 0, GMST.GEN_DESC, ADD_DESCRIPTION, MAKE"


        '    SqlStr = " SELECT " & vbCrLf _
        ''            & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC"

        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf _
            & " FROM PUR_INDENT_HDR IH, PUR_INDENT_DET ID, " & vbCrLf _
            & "INV_ITEM_MST INVMST, INV_GENERAL_MST GMST"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " IH.AUTO_KEY_INDENT=ID.AUTO_KEY_INDENT" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND INVMST.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
            & " AND INVMST.CATEGORY_CODE=GMST.GEN_CODE AND GMST.GEN_TYPE='C'" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_INDENT=" & Val(txtIndentNo.Text) & ""


        ''ORDER CLAUSE...

        SqlStr = SqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        PubDBCn.Execute(SqlStr)


        SqlStr = " SELECT * FROM TEMP_INDENT_PRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False

                mItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                mItemUOM = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value))
                mItemRate = CDbl(GetLastPORate(mItemCode, mLastSuppCustName, mLastMrrDate))
                mStockQty = GetBalanceStockQty(mItemCode, (txtIndentDate.Text), mItemUOM, "STR", "ST", "", ConWH, mDivisionCode) + GetBalanceStockQty(mItemCode, (txtIndentDate.Text), mItemUOM, "", "ST", "", ConSH, mDivisionCode)

                mMaxLevel = 0
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "MAXIMUM_QTY", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mMaxLevel = MasterNo
                End If

                'If mItemRate <> 0 Then
                SqlStr = "UPDATE TEMP_INDENT_PRN SET ITEM_RATE=" & mItemRate & ", " & vbCrLf _
                    & " STOCK_QTY= " & mStockQty & "," & vbCrLf _
                    & " SUPP_CUST_NAME= '" & MainClass.AllowSingleQuote(mLastSuppCustName) & "'," & vbCrLf _
                    & " LAST_MRRDATE= TO_DATE('" & VB6.Format(mLastMrrDate, "dd/MMM/yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                    & " MAX_LEVEL= " & mMaxLevel & "" & vbCrLf _
                    & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
                    & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                PubDBCn.Execute(SqlStr)
                'End If
                RsTemp.MoveNext()
            Loop
        End If



        PubDBCn.CommitTrans()

        mSqlStr = " SELECT * FROM TEMP_INDENT_PRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' ORDER BY SERIAL_NO"
        Exit Sub
ErrPart:
        mSqlStr = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        'Dim Printer As New Printer

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, pmyMenu)
        MainClass.AssignCRptFormulas(Report1, "Department=""" & lblDept.Text & """")
        MainClass.AssignCRptFormulas(Report1, "EmpName=""" & lblIndentBy.Text & """")
        MainClass.AssignCRptFormulas(Report1, "EmpHoD=""" & lblHOD.Text & """")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & mRPTName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        'Dim prt As Printer
        'If PubUniversalPrinter = "Y" And mMode = Crystal.DestinationConstants.crptToPrinter Then
        '    For Each prt In Printers
        '        If UCase(prt.DeviceName) = UCase("Universal Printer") Then
        '            Printer = prt

        '            Report1.PrinterName = prt.DeviceName
        '            Report1.PrinterDriver = prt.DriverName
        '            Report1.PrinterPort = prt.Port
        '            Report1.PrinterSelect()
        '            Exit For
        '        End If
        '    Next prt
        'End If

        Report1.Action = 1

    End Sub

    Private Sub ShowTermsReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & mRPTName
        'Report1.SQLQuery = mSqlStr
        'Report1.WindowShowGroupTree = False
        Report1.Action = 1

    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtIndentNo_Validating(txtIndentNo, New System.ComponentModel.CancelEventArgs(False))
            If cmdAdd.Enabled = True Then cmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            GoTo ErrorHandler
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ViewGrid()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub FrmIndentEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        mBookType = VB.Left(lblBookType.Text, 1)
        mBookSubType = VB.Right(lblBookType.Text, 1)
        If mBookSubType = "I" Then
            Me.Text = "Indent Entry"
        ElseIf mBookSubType = "A" Then
            Me.Text = "Indent Approval"
        ElseIf mBookSubType = "H" Then
            Me.Text = "Indent HOD Approval"
        ElseIf mBookSubType = "C" Then
            Me.Text = "Indent Cancellation"
        End If

        SqlStr = ""
        SqlStr = "Select * from PUR_INDENT_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIndentMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from PUR_INDENT_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIndentDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call SetTextLengths()
        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        Clear1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub FrmIndentEntry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmIndentEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        pmyMenu = myMenu
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        mIsAuthorisedUser = False
        If InStr(1, XRIGHT, "S") > 0 Then
            mIsAuthorisedUser = True
        End If

        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        txtIndentDate.Enabled = False
        ADDMode = False
        MODIFYMode = False
        Call SetMainFormCordinate(Me)
        Me.Top = 0
        Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo SetTextLengthsErr
        txtIndentNo.Maxlength = RsIndentMain.Fields("AUTO_KEY_INDENT").DefinedSize
        txtIndentDate.Maxlength = RsIndentMain.Fields("INDENT_DATE").DefinedSize - 6


        txtDept.MaxLength = RsIndentMain.Fields("DEPT_CODE").DefinedSize
        txtRequestBy.MaxLength = RsIndentMain.Fields("REQUEST_BY").DefinedSize
        '    txtIndentBy.MaxLength = RsIndentMain.Fields("IND_EMP_CODE").DefinedSize
        '    txtHOD.MaxLength = RsIndentMain.Fields("HOD_EMP_CODE").DefinedSize
        '    txtStatus.MaxLength = RsIndentMain.Fields("APPROVAL_STATUS").DefinedSize
        txtHODAppDate.Maxlength = 10
        txtFinalAppDate.Maxlength = 10
        '    txtAppBy.MaxLength = RsIndentMain.Fields("APP_EMP_CODE").DefinedSize
        TxtBillTm.Maxlength = 5
        txtRemarks.Maxlength = RsIndentMain.Fields("REMARKS").DefinedSize

        txtDivision.Maxlength = RsIndentMain.Fields("DIV_CODE").DefinedSize
        txtCCentre.Maxlength = RsIndentMain.Fields("CC_CODE").DefinedSize
        Exit Sub
SetTextLengthsErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub Clear1()

        SqlStr = ""
        lblmKey.Text = ""
        txtIndentNo.Text = ""
        txtIndentDate.Text = VB6.Format(RunDate, "dd/MM/yyyy")
        txtDept.Text = ""
        txtIndentBy.Text = ""
        lblIndentBy.Text = ""
        txtIndentBy.Enabled = False
        txtRequestBy.Text = ""
        txtHOD.Text = ""
        txtHOD.Enabled = False

        lblAppBy.Text = ""
        txtStatus.Text = ""
        txtDivision.Text = ""
        txtCCentre.Text = ""
        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAutoIssue.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAutoIssueToSS.CheckState = System.Windows.Forms.CheckState.Unchecked

        lblAppBy.Text = ""
        lblHOD.Text = ""
        lblDivision.Text = ""
        lblCCentre.Text = ""


        lblDept.Text = ""
        txtRemarks.Text = ""
        txtStatus.Enabled = False

        chkHODApproval.Enabled = False
        chkHODApproval.CheckState = System.Windows.Forms.CheckState.Unchecked


        chkApproval.Enabled = False
        chkApproval.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSendBack.CheckState = System.Windows.Forms.CheckState.Unchecked
        TxtBillTm.Text = GetServerTime()
        FraApproved.Enabled = False


        If RsCompany.Fields("SALEORDER_WISE_INDENT").Value = "Y" Then
            fraOrder.Visible = True
        Else
            fraOrder.Visible = False
        End If

        txtSONo.Text = ""
        lblCustomerName.Text = ""
        txtProductCode.Text = ""
        lblProductName.Text = ""
        txtSONo.Enabled = True
        cmdGetData.Enabled = True
        cmdSearchSONo.Enabled = True
        cmdReOrderLevel.Enabled = True
        txtPlanQty.Text = ""

        If VB.Right(lblBookType.Text, 1) = "I" Then
            txtDept.Enabled = True
            cmdDeptSearch.Enabled = True

            txtDivision.Enabled = True
            cmdDivSearch.Enabled = True

            txtCCentre.Enabled = True
            cmdCCSearch.Enabled = True

            '        txtIndentBy.Enabled = True
            '        cmdIndentBySearch.Enabled = True

            FraHODApp.Enabled = False
            chkAutoIssue.Enabled = False
            chkAutoIssueToSS.Enabled = False

            txtHOD.Enabled = False
            '        cmdHODSearch.Enabled = False
            txtHODAppDate.Enabled = False


            txtFinalAppDate.Enabled = False

            txtHODAppDate.Text = ""
            txtFinalAppDate.Text = ""



        ElseIf VB.Right(lblBookType.Text, 1) = "H" Then
            txtDept.Enabled = True
            cmdDeptSearch.Enabled = True

            txtDivision.Enabled = True
            cmdDivSearch.Enabled = True

            txtCCentre.Enabled = True
            cmdCCSearch.Enabled = True

            txtIndentBy.Enabled = False
            '        cmdIndentBySearch.Enabled = False

            FraHODApp.Enabled = True
            chkAutoIssue.Enabled = True
            chkAutoIssueToSS.Enabled = True

            '        txtHOD.Enabled = True
            '        cmdHODSearch.Enabled = True
            txtHODAppDate.Enabled = False

            txtFinalAppDate.Enabled = False

            txtHODAppDate.Text = VB6.Format(RunDate, "dd/MM/yyyy")
            txtFinalAppDate.Text = ""

            chkHODApproval.Enabled = True
        ElseIf VB.Right(lblBookType.Text, 1) = "A" Then
            txtDept.Enabled = False
            cmdDeptSearch.Enabled = False

            txtDivision.Enabled = False
            cmdDivSearch.Enabled = False

            txtCCentre.Enabled = False
            cmdCCSearch.Enabled = False

            txtIndentBy.Enabled = False
            '        cmdIndentBySearch.Enabled = False

            FraHODApp.Enabled = False
            chkAutoIssue.Enabled = False
            chkAutoIssueToSS.Enabled = False

            txtHOD.Enabled = False
            '        cmdHODSearch.Enabled = False
            txtHODAppDate.Enabled = False

            lblAppBy.Text = PubUserID
            txtFinalAppDate.Enabled = False

            txtHODAppDate.Text = ""
            txtFinalAppDate.Text = VB6.Format(RunDate, "dd/MM/yyyy")
            chkApproval.Enabled = True

            FraApproved.Enabled = True
        ElseIf VB.Right(lblBookType.Text, 1) = "C" Then
            txtDept.Enabled = False
            cmdDeptSearch.Enabled = False

            txtDivision.Enabled = False
            cmdDivSearch.Enabled = False

            txtCCentre.Enabled = False
            cmdCCSearch.Enabled = False

            txtIndentBy.Enabled = False
            '        cmdIndentBySearch.Enabled = False

            FraHODApp.Enabled = False
            chkAutoIssue.Enabled = False
            chkAutoIssueToSS.Enabled = False

            txtHOD.Enabled = False
            '        cmdHODSearch.Enabled = False
            txtHODAppDate.Enabled = False

            txtFinalAppDate.Enabled = False

            txtHODAppDate.Text = ""
            txtFinalAppDate.Text = ""
        End If

        txtIndentBy.Text = PubUserID
        lblIndentBy.Text = PubUserName
        'If MainClass.ValidateWithMasterTable((txtIndentBy.Text), "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    lblIndentBy.Text = MasterNo
        'End If

        Call AutoCompleteSearch("PAY_DEPT_MST", "DEPT_CODE", "", txtDept)
        Call AutoCompleteSearch("INV_DIVISION_MST", "TO_CHAR(DIV_CODE)", "", txtDivision)

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsIndentMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mValue As String

        FraCmd.Enabled = True
        If Not RsIndentMain.EOF Then
            LblMKey.Text = RsIndentMain.Fields("AUTO_KEY_INDENT").Value
            txtIndentNo.Text = VB6.Format(IIf(IsDBNull(RsIndentMain.Fields("AUTO_KEY_INDENT").Value), "", RsIndentMain.Fields("AUTO_KEY_INDENT").Value), "000000")
            txtIndentDate.Text = IIf(IsDBNull(RsIndentMain.Fields("INDENT_DATE").Value), ConBlankDate, VB6.Format(RsIndentMain.Fields("INDENT_DATE").Value, "dd/MM/yyyy"))

            TxtBillTm.Text = VB6.Format(IIf(IsDBNull(RsIndentMain.Fields("IND_PREP_TIME").Value), "", RsIndentMain.Fields("IND_PREP_TIME").Value), "hh:mm")

            txtDept.Text = IIf(IsDBNull(RsIndentMain.Fields("DEPT_CODE").Value), "", RsIndentMain.Fields("DEPT_CODE").Value)
            txtIndentBy.Text = IIf(IsDBNull(RsIndentMain.Fields("IND_EMP_CODE").Value), "", RsIndentMain.Fields("IND_EMP_CODE").Value)
            txtHOD.Text = IIf(IsDBNull(RsIndentMain.Fields("HOD_EMP_CODE").Value), "", RsIndentMain.Fields("HOD_EMP_CODE").Value)

            txtRequestBy.Text = IIf(IsDBNull(RsIndentMain.Fields("REQUEST_BY").Value), "", RsIndentMain.Fields("REQUEST_BY").Value)

            mValue = IIf(IsDBNull(RsIndentMain.Fields("HOD_EMP_CODE").Value), "", RsIndentMain.Fields("HOD_EMP_CODE").Value)

            chkHODApproval.CheckState = IIf(mValue = "", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)


            If VB.Right(lblBookType.Text, 1) = "H" Then
                If Trim(txtHOD.Text) = "" Then
                    txtHOD.Text = PubUserID
                End If
                chkHODApproval.Enabled = IIf(mValue = "", True, False)

            End If

            lblAppBy.Text = IIf(IsDBNull(RsIndentMain.Fields("APP_EMP_CODE").Value), "", RsIndentMain.Fields("APP_EMP_CODE").Value)

            mValue = IIf(IsDBNull(RsIndentMain.Fields("APP_EMP_CODE").Value), "", RsIndentMain.Fields("APP_EMP_CODE").Value)

            chkApproval.CheckState = IIf(mValue = "", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            chkApproval.Enabled = IIf(mValue = "", True, False)

            txtHODAppDate.Text = IIf(IsDBNull(RsIndentMain.Fields("HOD_APP_DATE").Value), ConBlankDate, VB6.Format(RsIndentMain.Fields("HOD_APP_DATE").Value, "dd/MM/yyyy"))
            txtFinalAppDate.Text = IIf(IsDBNull(RsIndentMain.Fields("PUR_APP_DATE").Value), ConBlankDate, VB6.Format(RsIndentMain.Fields("PUR_APP_DATE").Value, "dd/MM/yyyy"))
            chkCancelled.CheckState = IIf(RsIndentMain.Fields("APPROVAL_STATUS").Value = "Y", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            chkSendBack.CheckState = IIf(RsIndentMain.Fields("SENDBACK_TO_HOD").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkSendBack.Enabled = chkApproval.Enabled
            txtRemarks.Text = IIf(IsDBNull(RsIndentMain.Fields("Remarks").Value), "", RsIndentMain.Fields("Remarks").Value)

            chkAutoIssue.CheckState = IIf(RsIndentMain.Fields("AUTO_ISSUE").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            chkAutoIssueToSS.CheckState = IIf(RsIndentMain.Fields("AUTO_SS_ISSUE").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

            txtDivision.Text = IIf(IsDBNull(RsIndentMain.Fields("DIV_CODE").Value), "", RsIndentMain.Fields("DIV_CODE").Value)
            txtCCentre.Text = IIf(IsDBNull(RsIndentMain.Fields("CC_CODE").Value), "", RsIndentMain.Fields("CC_CODE").Value)

            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
                txtStatus.Text = "INDENT CANCELLED"
            Else
                If Trim(lblAppBy.Text) = "" Then
                    If RsIndentMain.Fields("SENDBACK_TO_HOD").Value = "Y" Then
                        txtStatus.Text = "INDENT PENDING (SEND BACK)"
                    Else
                        txtStatus.Text = "INDENT PENDING FOR APPROVAL"
                    End If
                Else
                    txtStatus.Text = "INDENT APPROVED"
                End If
            End If

            If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblDept.Text = MasterNo
            End If

            If MainClass.ValidateWithMasterTable((txtIndentBy.Text), "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblIndentBy.Text = MasterNo
            End If

            If MainClass.ValidateWithMasterTable((txtHOD.Text), "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblHOD.Text = MasterNo
            End If

            If MainClass.ValidateWithMasterTable((txtDivision.Text), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblDivision.Text = MasterNo
            End If

            If MainClass.ValidateWithMasterTable((txtCCentre.Text), "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblCCentre.Text = MasterNo
            Else
                lblCCentre.Text = ""
            End If

            If VB.Right(lblBookType.Text, 1) = "H" Then
                If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_CODE", "ISSUBSTORE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISSUBSTORE='Y'") = True Then
                    chkAutoIssueToSS.Enabled = True
                Else
                    chkAutoIssueToSS.Enabled = False
                End If
            End If

            txtSONo.Text = IIf(IsDBNull(RsIndentMain.Fields("AUTO_KEY_SO").Value), "", RsIndentMain.Fields("AUTO_KEY_SO").Value)
            lblCustomerName.Text = ""
            If MainClass.ValidateWithMasterTable((txtSONo.Text), "AUTO_KEY_SO", "SUPP_CUST_CODE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                Dim mCustomerCode As String = MasterNo
                If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblCustomerName.Text = MasterNo
                End If
            End If

            txtProductCode.Text = IIf(IsDBNull(RsIndentMain.Fields("PRODUCT_CODE").Value), "", RsIndentMain.Fields("PRODUCT_CODE").Value)

            If MainClass.ValidateWithMasterTable((txtProductCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblProductName.Text = MasterNo
            Else
                lblProductName.Text = ""
            End If


            txtPlanQty.Text = IIf(IsDBNull(RsIndentMain.Fields("PACK_QTY").Value), "", RsIndentMain.Fields("PACK_QTY").Value)

            txtSONo.Enabled = False
            cmdSearchSONo.Enabled = False
            cmdGetData.Enabled = False

            '        If MainClass.ValidateWithMasterTable(txtAppBy.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            '            lblAppBy.text = MasterNo
            '        End If

            Call ShowDetail1(Val(txtDivision.Text))


        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsIndentMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        txtIndentNo.Enabled = True
        SprdMain.Enabled = True
        Exit Sub
ShowErrPart:

        If Err.Number = -2147418113 Then
            RsIndentMain.Requery()
            Resume
        End If
        MsgBox(Err.Description, Err.Number)

    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mDeptCode As String
        Dim mCnt As Integer
        Dim mItemCode As String
        Dim mMaxLevel As Double
        Dim mStock As Double
        Dim mReqQty As Double
        Dim mItemCategory As String
        Dim mIsApproved As String

        FieldsVarification = True

        If ValidateBranchLocking((txtIndentDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ValidateBookLocking(PubDBCn, CInt(ConLockINDENT), txtIndentDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsIndentMain.EOF = True Then Exit Function

        If MODIFYMode = True And Trim(txtIndentNo.Text) = "" Then
            MsgInformation("Indent No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtIndentDate.Text) = "" Then
            MsgInformation(" Indent Date is empty. Cannot Save")
            txtIndentDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtIndentDate.Text) <> "" Then
            If IsDate(txtIndentDate.Text) = False Then
                MsgInformation(" Invalid Indent Date. Cannot Save")
                txtIndentDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If FYChk((txtIndentDate.Text)) = False Then
            FieldsVarification = False
            If txtIndentDate.Enabled = True Then txtIndentDate.Focus()
            Exit Function
        End If


        If Trim(txtDept.Text) = "" Then
            MsgInformation("Department Name is Blank. Cannot Save")
            If txtDept.Enabled = True Then txtDept.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDivision.Text) = "" Then
            MsgInformation("Division is Blank. Cannot Save")
            If txtDivision.Enabled = True Then txtDivision.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtCCentre.Text) = "" Then
            MsgInformation("Cost Centre is Blank. Cannot Save")
            If txtCCentre.Enabled = True Then txtCCentre.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If VB.Right(lblBookType.Text, 1) = "I" Then
            If Trim(txtIndentBy.Text) = "" Then
                MsgInformation("Indent By Name is Blank. Cannot Save")
                If txtIndentBy.Enabled = True Then txtIndentBy.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If MODIFYMode = True Then
                If MainClass.ValidateWithMasterTable((txtIndentNo.Text), "AUTO_KEY_INDENT", "HOD_EMP_CODE", "PUR_INDENT_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mIsApproved = MasterNo
                    If Trim(mIsApproved) <> "" Then
                        MsgInformation("Indent already approved, so can't be save.")
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If

        ElseIf VB.Right(lblBookType.Text, 1) = "H" Then
            If Trim(txtHOD.Text) = "" Then
                MsgInformation("HOD Name is Blank. Cannot Save")
                If txtHOD.Enabled = True Then txtHOD.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If MODIFYMode = True Then
                If MainClass.ValidateWithMasterTable((txtIndentNo.Text), "AUTO_KEY_INDENT", "APP_EMP_CODE", "PUR_INDENT_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mIsApproved = MasterNo
                    If Trim(mIsApproved) <> "" Then
                        MsgInformation("Indent already approved, so can't be save.")
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If
        ElseIf VB.Right(lblBookType.Text, 1) = "A" Then

            '        If Trim(txtAppBy.Text) = "" Then
            '            MsgInformation "Approval Name is Blank. Cannot Save"
            '            If txtAppBy.Enabled = True Then txtHOD.SetFocus
            '            FieldsVarification = False
            '            Exit Function
            '        End If
        End If

        If VB.Right(lblBookType.Text, 1) = "A" Then
            '        If Trim(txtHOD.Text) = "" Then
            '            MsgInformation "HOD Not Approved This Indent. Cannot Save"
            '            FieldsVarification = False
            '            Exit Function
            '        End If

            If chkApproval.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                MsgInformation("Please Approved This Indent. Cannot Save")
                If chkApproval.Enabled = True Then chkApproval.Focus()
                FieldsVarification = False
                Exit Function
            End If
        Else
            If chkApproval.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Approved Indent Cannot be Save.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If lblBookType.Text = "II" Or lblBookType.Text = "IH" Then
            If ValidateDeptRight(PubUserID, Trim(txtDept.Text), UCase(Trim(lblDept.Text))) = False Then
                FieldsVarification = False
                Exit Function
            End If

            If ValidateDivisionRight(PubUserID, CDbl(Trim(txtDivision.Text)), UCase(Trim(lblDivision.Text))) = False Then
                FieldsVarification = False
                Exit Function
            End If
        End If

        With SprdMain
            For mCnt = 1 To .MaxRows - 1
                .Row = mCnt
                .Col = ColItemCode
                mItemCode = Trim(.Text)
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_STATUS", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='I'") = True Then
                    MsgBox("Item Code " & mItemCode & " is Inactive, So cann't be save.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If

                .Col = ColMaxLevel
                mMaxLevel = CDbl(Trim(.Text))

                .Col = ColStock
                mStock = CDbl(Trim(.Text))

                .Col = ColQtyReqd
                mReqQty = CDbl(Trim(.Text))

                If CheckMaxLevel(mItemCode) = True Then
                    If mReqQty > (mMaxLevel - mStock) Then
                        MsgBox("Required Qty of Item Code " & mItemCode & " is greater than Max Level Minus Stock.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If

                Dim mReqDate As String

                .Col = ColReqDate
                mReqDate = VB6.Format(SprdMain.Text, "dd/MM/yyyy")

                If VB6.Format(.Text, "YYYYMMDD") < VB6.Format(txtIndentDate.Text, "YYYYMMDD") Then
                    MsgBox("Required Date cann't be less than Indent Date.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            Next
        End With

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Please Check ItemCode.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColQtyReqd, "N", "Please Check Quantity ") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColUnit, "S", "Please Check Unit.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColItemPriority, "S", "Please Check Item Priority.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColReqDate, "S", "Please Check Required Date.") = False Then FieldsVarification = False
        Exit Function
err_Renamed:
        FieldsVarification = False
        MsgBox(Err.Description)
    End Function
    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim mRowNo As Integer
        Dim mIndentNo As Double
        Dim mAppStatus As String
        Dim mSendBack As String
        Dim mAutoIssue As String
        Dim mAutoIssueSS As String


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mAppStatus = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "N", "Y")
        mAutoIssue = IIf(chkAutoIssue.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAutoIssueSS = IIf(chkAutoIssueToSS.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If VB.Right(lblBookType.Text, 1) = "C" Then
            mAppStatus = "N"
        End If
        mSendBack = IIf(chkSendBack.CheckState = System.Windows.Forms.CheckState.Unchecked, "N", "Y")

        If VB.Right(lblBookType.Text, 1) = "H" Then
            txtHODAppDate.Text = VB6.Format(IIf(IsDate(txtHODAppDate.Text), txtHODAppDate.Text, RunDate), "dd/MM/yyyy")
        ElseIf VB.Right(lblBookType.Text, 1) = "A" Then

            If Trim(txtHOD.Text) = "" Then
                txtHOD.Text = PubUserID
            End If

            If Trim(txtHODAppDate.Text) = "" Then
                txtHODAppDate.Text = VB6.Format(IIf(IsDate(txtHODAppDate.Text), txtHODAppDate.Text, RunDate), "dd/MM/yyyy")
            End If

            txtFinalAppDate.Text = VB6.Format(IIf(IsDate(txtFinalAppDate.Text), txtFinalAppDate.Text, RunDate), "dd/MM/yyyy")
            lblAppBy.Text = PubUserID
        End If

        SqlStr = ""
        mIndentNo = Val(txtIndentNo.Text)
        If Val(txtIndentNo.Text) = 0 Then
            mIndentNo = AutoGenIndentNoSeq()
        End If

        If ADDMode = True Then
            LblMKey.Text = CStr(mIndentNo)
            SqlStr = " INSERT INTO PUR_INDENT_HDR ( " & vbCrLf _
                & " AUTO_KEY_INDENT, COMPANY_CODE, INDENT_DATE, " & vbCrLf _
                & " DEPT_CODE, IND_EMP_CODE, HOD_EMP_CODE, " & vbCrLf _
                & " APP_EMP_CODE, APPROVAL_STATUS, IND_PREP_TIME, HOD_APP_DATE, " & vbCrLf _
                & " PUR_APP_DATE," & vbCrLf & " SENDBACK_TO_HOD, REMARKS, " & vbCrLf _
                & " AUTO_ISSUE, AUTO_SS_ISSUE, " & vbCrLf _
                & " DIV_CODE, CC_CODE, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE,REQUEST_BY, PRODUCT_CODE , AUTO_KEY_SO, PACK_QTY) "

            ''

            SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf _
                & " " & mIndentNo & ", " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtIndentDate.Text, "dd/MMM/yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtDept.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtIndentBy.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtHOD.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((lblAppBy.Text)) & "', " & vbCrLf _
                & " '" & mAppStatus & "', " & vbCrLf _
                & " TO_DATE('" & TxtBillTm.Text & "','HH24:MI')," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtHODAppDate.Text, "dd/MMM/yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtFinalAppDate.Text, "dd/MMM/yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & mSendBack & "', '" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', " & vbCrLf _
                & " '" & mAutoIssue & "', '" & mAutoIssueSS & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtDivision.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtCCentre.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd/MMM/yyyy") & "','DD-MON-YYYY'),'','','" & MainClass.AllowSingleQuote((txtRequestBy.Text)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'," & IIf(Val(txtSONo.Text) = 0, "NULL", Val(txtSONo.Text)) & "," & IIf(Val(txtPlanQty.Text) = 0, "NULL", Val(txtPlanQty.Text)) & ")"
        End If

        If MODIFYMode = True Then
            SqlStr = " UPDATE PUR_INDENT_HDR SET " & vbCrLf _
                & " AUTO_KEY_INDENT=" & mIndentNo & ", REQUEST_BY='" & MainClass.AllowSingleQuote((txtRequestBy.Text)) & "'," & vbCrLf _
                & " INDENT_DATE=TO_DATE('" & VB6.Format(txtIndentDate.Text, "dd/MMM/yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                & " DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "', " & vbCrLf _
                & " IND_EMP_CODE='" & MainClass.AllowSingleQuote((txtIndentBy.Text)) & "', " & vbCrLf _
                & " HOD_EMP_CODE='" & MainClass.AllowSingleQuote((txtHOD.Text)) & "', " & vbCrLf _
                & " APP_EMP_CODE='" & MainClass.AllowSingleQuote((lblAppBy.Text)) & "', " & vbCrLf _
                & " APPROVAL_STATUS='" & mAppStatus & "', " & vbCrLf _
                & " IND_PREP_TIME= TO_DATE('" & TxtBillTm.Text & "','HH24:MI')," & vbCrLf _
                & " HOD_APP_DATE=TO_DATE('" & VB6.Format(txtHODAppDate.Text, "dd/MMM/yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                & " PUR_APP_DATE=TO_DATE('" & VB6.Format(txtFinalAppDate.Text, "dd/MMM/yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                & " SENDBACK_TO_HOD='" & mSendBack & "', " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', " & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd/MMM/yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                & " PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', AUTO_KEY_SO = " & IIf(Val(txtSONo.Text) = 0, "NULL", Val(txtSONo.Text)) & ",PACK_QTY=" & IIf(Val(txtPlanQty.Text) = 0, "NULL", Val(txtPlanQty.Text)) & "," & vbCrLf _
                & " AUTO_ISSUE='" & mAutoIssue & "', " & vbCrLf _
                & " AUTO_SS_ISSUE='" & mAutoIssueSS & "', " & vbCrLf _
                & " DIV_CODE='" & MainClass.AllowSingleQuote((txtDivision.Text)) & "', " & vbCrLf _
                & " CC_CODE='" & MainClass.AllowSingleQuote((txtCCentre.Text)) & "' " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND AUTO_KEY_INDENT =" & Val(LblMKey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        Update1 = True
        If UpdateDetail1(mIndentNo) = False Then GoTo ErrPart


        '    If SendMail(mIndentNo) = False Then GoTo ErrPart


        PubDBCn.CommitTrans()

        If chkHODApproval.CheckState = System.Windows.Forms.CheckState.Checked And chkHODApproval.Enabled = True Then
            MsgInformation("Indent Approved.")
        End If


        txtIndentNo.Text = CStr(mIndentNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsIndentMain.Requery()
        RsIndentDetail.Requery()
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub txtRequestBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRequestBy.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRequestBy_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRequestBy.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRequestBy.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Function SendMail(ByRef mIndentNo As Double) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim mTo As String
        Dim mCC As String
        Dim mFrom As String
        Dim mAttachmentFile As String
        Dim mDateTime As String
        Dim pAccountCode As String
        Dim mSubject As String
        Dim mBodyText As String
        Dim mHODCode As String
        Dim mStatus As String
        Dim mBodyTextDetail As String
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String = ""
        Dim mQty As String
        Dim mPurpose As String
        Dim CntRow As Integer


        SendMail = False

        strServerPop3 = GetEMailID("POP_ID") ''ReadInIFromServer("InternetInfo", "POP3", "InternetInfo.INI")
        strServerSmtp = GetEMailID("SMTP_ID") ''ReadInIFromServer("InternetInfo", "SMTP", "InternetInfo.INI")
        strAccount = GetEMailID("MAIL_ACCOUNT") ''ReadInIFromServer("InternetInfo", "Account", "InternetInfo.INI")
        strPassword = GetEMailID("PASSWORD") ''ReadInIFromServer("InternetInfo", "Password", "InternetInfo.INI")

        mFrom = GetEMailID("MAIL_FROM")

        '    If MainClass.ValidateWithMasterTable(txtIndentBy.Text, "EMP_CODE", "EMP_EMAILID_OFF", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '        mFrom = MasterNo
        '    Else
        '        mFrom = strAccount
        '    End If


        If Trim(txtHOD.Text) = "" Then
            If MainClass.ValidateWithMasterTable((txtIndentBy.Text), "EMP_CODE", "EMP_HOD_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mHODCode = MasterNo
                If MainClass.ValidateWithMasterTable(mHODCode, "EMP_CODE", "EMP_EMAILID_OFF", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTo = MasterNo
                Else
                    mTo = ""
                End If
            Else
                mTo = ""
            End If
        ElseIf Trim(txtHOD.Text) <> "" And chkApproval.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTo = GetEMailID("INDENT_MAIL_TO")
        Else
            mTo = GetEMailID("PUR_MAIL_TO")
        End If


        If MainClass.ValidateWithMasterTable((txtIndentBy.Text), "EMP_CODE", "EMP_EMAILID_OFF", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCC = MasterNo
        Else
            mCC = ""
        End If

        mCC = Trim(mCC)

        mAttachmentFile = ""
        mStatus = ""
        mSubject = ""

        If Trim(txtHOD.Text) = "" Then
            mStatus = "Pending for HOD Approval"
        ElseIf Trim(txtHOD.Text) <> "" And chkApproval.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mStatus = "Pending for Final Approval"
        Else
            mStatus = "Approved"
        End If
        mSubject = "Indent - Department :" & Trim(lblDept.Text) & " (" & mStatus & ")" ''" Indent No & Dated : " & Trim(mIndentNo) & " & " & VB6.Format(txtIndentDate.Text) & ""



        mBodyTextDetail = "<table align=center border=1 cellPadding=2 cellSpacing=0>" & "<tr>" & "<td width=50><b>SNo</b></td>" & "<td width=100><b>Item Code</b></td>" & "<td width=100><b>Item Description</b></td>" & "<td width=100><b>UOM</b></td>" & "<td width=100><b>Qty</b></td>" & "<td width=100><b>Item Purpose</b></td>" & "</tr>"

        With SprdMain
            For CntRow = 1 To .MaxRows - 1
                .Row = CntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColItemDesc
                mItemDesc = Trim(.Text)

                .Col = ColUnit
                mUOM = Trim(.Text)

                .Col = ColQtyReqd
                mQty = Trim(.Text)

                .Col = ColPurpose
                mPurpose = Trim(.Text)


                mBodyTextDetail = mBodyTextDetail & "<tr>" & "<td align=Right>" & CntRow & "</td>" & "<td>" & mItemCode & "</td>" & "<td>" & mItemDesc & "</td>" & "<td>" & mUOM & "</td>" & "<td align=Right>" & VB6.Format(mQty, "0.00") & "</td>" & "<td>" & mPurpose & "</td>" & "</tr>"

            Next
        End With

        mBodyTextDetail = mBodyTextDetail & "</table>"



        mBodyText = "<html><body><b><font size=11, color=Red>INDENT</font></b><br />" & "<b>Indent No       : </b>" & Trim(CStr(mIndentNo)) & "<br />" & "<b>Department    : </b>" & Trim(lblDept.Text) & "<br />" & "<b>Dated         : </b>" & VB6.Format(txtIndentDate.Text) & "<br />" & "<b>Indent By : </b>" & Trim(lblIndentBy.Text) & "<br />" & "<b>Status       : </b>" & Trim(mStatus) & "<br />" & "<br />" & "<br />" & mBodyTextDetail & "<br />" & "<br />" & "</body></html>"




        Call SendMailProcess(mFrom, mTo, mCC, "", mAttachmentFile, mSubject, mBodyText)

        SendMail = True

        Exit Function
ErrPart:
        SendMail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            AssignGrid(True)
            '        ADataPPOMain.Refresh
            UltraGrid1.Refresh()
            UltraGrid1.Focus()
            FraTop.Visible = False
            Frabot.Visible = False
            UltraGrid1.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraTop.Visible = True
            Frabot.Visible = True
            UltraGrid1.SendToBack()
        End If
        'Call FormatSprdView()
        MainClass.ButtonStatus(Me, XRIGHT, RsIndentMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FrmIndentEntry_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsIndentDetail.Close()
        RsIndentMain.Close()
        'PvtDBCn.Close
        RsIndentDetail = Nothing
        RsIndentMain = Nothing
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mIndentNo As String

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        mIndentNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(0))

        txtIndentNo.Text = CStr(Val(mIndentNo))

        txtIndentNo_Validating(txtIndentNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())

    End Sub

    'Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
    '    SprdView.Col = 1
    '    SprdView.Row = SprdView.ActiveRow
    '    txtIndentNo.Text = SprdView.Text

    '    txtIndentNo_Validating(txtIndentNo, New System.ComponentModel.CancelEventArgs(False))
    '    CmdView_Click(CmdView, New System.EventArgs())
    'End Sub

    'Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent)
    '    If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    'End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xIDesc As String
        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "CUSTOMER_PART_NO", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If
        If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemDesc
                xIDesc = .Text
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "CUSTOMER_PART_NO", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND ITEM_STATUS='A'") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = AcName
                    .Col = ColItemCode
                    .Text = AcName1
                Else
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = xIDesc
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        Dim mItemCode As String
        Dim DelStatus As Boolean
        If eventArgs.col = 0 And eventArgs.row > 0 Then

            SprdMain.Row = eventArgs.row

            SprdMain.Col = ColItemCode
            mItemCode = SprdMain.Text

            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode, DelStatus)
                MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
            End If
        End If
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mQty As Double
        Dim mSubRowNo As Integer
        Dim mAmount As Double

        If eventArgs.newRow = -1 Then Exit Sub

        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Sub

            Select Case eventArgs.col
                Case ColItemCode
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    mItemCode = Trim(SprdMain.Text)

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = False Then
                        MsgBox("Either Item Code in Invalid or not Active.", MsgBoxStyle.Information)
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                        Exit Sub
                    End If

                    If CheckPendingItemForPO(mItemCode) = False Then
                        If CheckDuplicateItem(mItemCode) = False Then
                            .Row = .ActiveRow
                            .Col = ColItemCode
                            Call InsertItemDetIntoGrid((SprdMain.Text))
                        End If
                    Else
                        '                    MsgInformation "Such Item Pending For PO First. You Made PO for Such Item."
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                    End If
                Case ColQtyReqd
                    If CheckQty(eventArgs.col, eventArgs.row) = True Then
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        FormatSprdMain((SprdMain.MaxRows))
                    End If

                Case ColReqDate
                    Dim mReqDate As String

                    .Row = .ActiveRow
                    .Col = ColReqDate
                    mReqDate = VB6.Format(SprdMain.Text, "dd/MM/yyyy")

                    If VB6.Format(.Text, "YYYYMMDD") < VB6.Format(txtIndentDate.Text, "YYYYMMDD") Then
                        MsgBox("Required Date cann't be less than Indent Date.", MsgBoxStyle.Information)
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColReqDate)
                        Exit Sub
                    End If
            End Select
        End With

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        'With SprdMain
        '    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With
    End Sub
    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        cmdDeptSearch_Click(cmdDeptSearch, New System.EventArgs())
    End Sub


    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdDeptSearch_Click(cmdDeptSearch, New System.EventArgs())
    End Sub


    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtDept.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblDept.Text = MasterNo
        Else
            MsgBox("Invalid Department Code.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub



    Private Sub txtIndentDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIndentDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtIndentNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIndentNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtIndentNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIndentNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtIndentNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIndentNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mIndentNo As Double

        If Trim(txtIndentNo.Text) = "" Then GoTo EventExitSub


        If Len(txtIndentNo.Text) < 6 Then
            txtIndentNo.Text = Val(txtIndentNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mIndentNo = Val(txtIndentNo.Text)

        If MODIFYMode = True And RsIndentMain.BOF = False Then xMkey = RsIndentMain.Fields("AUTO_KEY_INDENT").Value

        SqlStr = "SELECT * FROM PUR_INDENT_HDR " & " WHERE AUTO_KEY_INDENT='" & MainClass.AllowSingleQuote(UCase(CStr(mIndentNo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_INDENT,LENGTH(AUTO_KEY_INDENT)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIndentMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsIndentMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Indent No. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PUR_INDENT_HDR WHERE AUTO_KEY_INDENT=" & Val(xMkey) & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_INDENT,LENGTH(AUTO_KEY_INDENT)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIndentMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FrmIndentEntry_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)
        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf _
            & "  IH.AUTO_KEY_INDENT, TO_CHAR(IH.INDENT_DATE,'DD/MM/YYYY') AS INDENT_DATE, IH.DEPT_CODE, " & vbCrLf _
            & " EM.EMP_NAME AS EMP_CODE, HM.EMP_NAME AS HOD_CODE , AM.EMP_NAME AS APPROVED_BY," & vbCrLf _
            & " DECODE(IH.APPROVAL_STATUS,'Y','YES','NO') AS STATUS, TO_CHAR(IH.HOD_APP_DATE,'DD/MM/YYYY') AS HOD_APP_DATE , TO_CHAR(IH.PUR_APP_DATE,'DD/MM/YYYY') AS PUR_APP_DATE" & vbCrLf _
            & " FROM PUR_INDENT_HDR IH, ATH_PASSWORD_MST EM, ATH_PASSWORD_MST HM, ATH_PASSWORD_MST AM" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_INDENT,LENGTH(AUTO_KEY_INDENT)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        ''IND_EMP_CODE  HOD_EMP_CODE APP_EMP_CODE

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.COMPANY_CODE=EM.COMPANY_CODE (+)" & vbCrLf _
            & " AND IH.IND_EMP_CODE=EM.USER_ID (+)" & vbCrLf _
            & " AND IH.COMPANY_CODE=HM.COMPANY_CODE (+)" & vbCrLf _
            & " AND IH.HOD_EMP_CODE=HM.USER_ID (+)" & vbCrLf _
            & " AND IH.COMPANY_CODE=AM.COMPANY_CODE (+)" & vbCrLf _
            & " AND IH.APP_EMP_CODE=AM.USER_ID (+)"


        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.AUTO_KEY_INDENT"

        'If MainClass.ValidateWithMasterTable((txtIndentBy.Text), "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    lblIndentBy.Text = MasterNo
        'End If


        'MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        'Call FormatSprdView()
        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        Me.UltraGrid1.DataSource = ds
        Me.UltraGrid1.DataMember = ""

        CreateGridHeader("S")

        MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")
        oledbAdapter.Dispose()
        oledbCnn.Close()
    End Sub
    Private Sub CreateGridHeader(pShowType As String)
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   to create the grid header
        'Comments       :   Nil
        '----------------------------------------------------------------------------
        Try
            Dim inti As Integer
            'create column header

            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Key = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Indent No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "INdent Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Dept Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Indent Emp Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "HOD Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "Approved By"
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Status"
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "HOD App Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "Final App Date"

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment
            Next

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 100


            'UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.MFGQty).MaskInput = "99999"

            Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            Me.UltraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

            'fill labels 
            'FillLabelsFromResFile(Me)
            'Catch sqlex As SqlException
            '    ErrorTrap(sqlex.Message, "frmRMReturn.vb", "CreateHeader", "", "", "Sql Exception")
            '    Me.Cursor = Windows.Forms.Cursors.Default
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub
    'Private Sub FormatSprdView()

    '    With SprdView
    '        .Row = -1
    '        .set_ColWidth(0, 5)
    '        .set_ColWidth(1, 10)
    '        .set_ColWidth(2, 10)
    '        .set_ColWidth(3, 10)
    '        .set_ColWidth(4, 10)
    '        .set_ColWidth(5, 10)
    '        .set_ColWidth(6, 10)
    '        .set_ColWidth(7, 8)
    '        .set_ColWidth(8, 10)
    '        .set_ColWidth(9, 10)


    '        MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
    '        MainClass.SetSpreadColor(SprdView, -1)
    '        .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
    '        MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
    '    End With
    'End Sub


    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.25)
            .set_RowHeight(0, ConRowHeight * 2)
            '        .RowHeight(Arow) = ConRowHeight * 1.5

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsIndentDetail.Fields("ITEM_CODE").DefinedSize
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemDesc, 25)
            .ColsFrozen = True
            .ColsFrozen = ColItemDesc

            .Col = ColAddItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsIndentDetail.Fields("ADD_DESCRIPTION").DefinedSize '
            .set_ColWidth(ColAddItemDesc, 25)

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColMake
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsIndentDetail.Fields("MAKE").DefinedSize '
            .set_ColWidth(ColMake, 12)
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = MainClass.SetMaxLength("GEN_DESC", "INV_GENERAL_MST", PubDBCn)
            .set_ColWidth(ColCategory, 15)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsIndentDetail.Fields("ITEM_UOM").DefinedSize 'ADD_DESCRIPTION
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColUnit, 4)

            .Col = ColLastPurDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = 10
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColLastPurDate, 8)


            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                If VB.Right(lblBookType.Text, 1) = "A" Then
                    .ColHidden = False
                Else
                    .ColHidden = True
                End If
            Else
                .ColHidden = False
            End If

            .Col = ColLastSupplier
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColLastSupplier, 12)
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                If VB.Right(lblBookType.Text, 1) = "A" Then
                    .ColHidden = False
                Else
                    .ColHidden = True
                End If
            Else
                .ColHidden = False
            End If

            .Col = ColLastPORate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999.99")
            .TypeFloatMin = CDbl("-999999.99")
            .set_ColWidth(ColLastPORate, 6)
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                If VB.Right(lblBookType.Text, 1) = "A" Then
                    .ColHidden = False
                Else
                    .ColHidden = True
                End If
            Else
                .ColHidden = False
            End If

            .Col = ColMaxLevel
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999.99")
            .TypeFloatMin = CDbl("-999999.99")
            .set_ColWidth(ColMaxLevel, 6)
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColReOderLevel
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999.99")
            .TypeFloatMin = CDbl("-999999.99")
            .set_ColWidth(ColReOderLevel, 6)
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColStock
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999.99")
            .TypeFloatMin = CDbl("-999999.99")
            .set_ColWidth(ColStock, 6)
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColQtyReqd
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                .TypeFloatDecimalPlaces = 4
                .TypeFloatMax = CDbl("9999999.9999")
                .TypeFloatMin = CDbl("-9999999.9999")
            Else
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMax = CDbl("9999999.999")
                .TypeFloatMin = CDbl("-9999999.999")
            End If

            .set_ColWidth(ColQtyReqd, 8)
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC


            .Col = ColItemPriority
            If FormActive = False Then
                .CellType = SS_CELL_TYPE_COMBOBOX
                .TypeComboBoxList = "Regular" & Chr(9) & "Urgent" & Chr(9) & "Most Urgent"
                .TypeComboBoxCurSel = 0
            End If

            .set_ColWidth(ColItemPriority, 9)


            .Col = ColPurpose
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsIndentDetail.Fields("ITEM_PURPOSE").DefinedSize
            .set_ColWidth(ColPurpose, 11)
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                .ColHidden = True
            End If

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsIndentDetail.Fields("REMARKS").DefinedSize
            .set_ColWidth(ColRemarks, 11)

            .Col = ColIndentStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColIndentStatus, 3)

            .Col = ColIndentRejected
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColIndentRejected, 3)

            .Col = ColQuotationApproved
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsIndentDetail.Fields("APPROVAL_REMARKS").DefinedSize
            .set_ColWidth(ColQuotationApproved, 4)
            .ColHidden = True

            .Col = ColAPPRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsIndentDetail.Fields("APPROVAL_REMARKS").DefinedSize
            .set_ColWidth(ColAPPRemarks, 11)

            .Col = ColConsiderQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("9999999.999")
            .TypeFloatMin = CDbl("-9999999.999")
            .set_ColWidth(ColConsiderQty, 7)
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = True

            .Col = ColReqDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(.Col, 8)

            '        If lblBookType.text = "IA" Then
            '            MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColReqDate
            '        End If

            If lblBookType.Text = "IA" Then
                If chkApproval.CheckState = System.Windows.Forms.CheckState.Unchecked And mIsAuthorisedUser = True Then
                    MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColConsiderQty)
                    MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColStock)
                    MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemPriority, ColConsiderQty)
                Else
                    MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColReqDate)
                End If
            Else
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColItemDesc)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCategory, ColStock)

                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIndentStatus, ColConsiderQty)
            End If

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIndentRejected, ColAPPRemarks)

        End With
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
    End Sub


    Private Function UpdateDetail1(ByRef pIndentNo As Double) As Boolean
        On Error GoTo UpdateDetail1Err
        Dim I As Short
        Dim mRow As Short
        Dim mItemCode As String
        Dim mItemUnit As String
        Dim mReqdQty As Double
        Dim mPriority As String
        Dim mPurpose As String
        Dim mRemarks As String
        Dim mStatus As String
        Dim mConsiderQty As Integer
        Dim mReqDate As String
        Dim mRJStatus As String
        Dim mAPPRemarks As String
        Dim mAddDesc As String
        Dim mMake As String
        Dim mQuotationApproved As String

        SqlStr = "DELETE FROM PUR_INDENT_DET WHERE AUTO_KEY_INDENT=" & Val(LblMKey.Text) & ""
        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = .Text

                .Col = ColUnit
                mItemUnit = .Text

                .Col = ColQtyReqd
                mReqdQty = Val(.Text)

                .Col = ColItemPriority
                mPriority = VB.Left(.Text, 1)

                .Col = ColPurpose
                mPurpose = .Text

                .Col = ColRemarks
                mRemarks = .Text

                .Col = ColIndentStatus
                mStatus = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColIndentRejected
                mRJStatus = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColQuotationApproved
                mQuotationApproved = Trim(.Text)

                .Col = ColAPPRemarks
                mAPPRemarks = .Text

                .Col = ColConsiderQty
                mConsiderQty = Val(.Text)

                .Col = ColReqDate
                mReqDate = .Text

                .Col = ColAddItemDesc
                mAddDesc = .Text

                SprdMain.Col = ColMake
                mMake = .Text


                SqlStr = ""

                SqlStr = " INSERT INTO PUR_INDENT_DET ( " & vbCrLf _
                    & " AUTO_KEY_INDENT, SERIAL_NO, " & vbCrLf _
                    & " ITEM_CODE, ITEM_UOM, " & vbCrLf _
                    & " REQ_QTY, PRIORITY_LEVEL, " & vbCrLf _
                    & " ITEM_PURPOSE, REMARKS, " & vbCrLf _
                    & " INDENT_STATUS, CONSIDER_QTY, " & vbCrLf _
                    & " REQ_DATE, COMPANY_CODE, IS_REJECTED, APPROVAL_REMARKS,ADD_DESCRIPTION,MAKE,QUOTATION_APP) VALUES ( "


                SqlStr = SqlStr & vbCrLf _
                    & " " & Val(CStr(pIndentNo)) & "," & I & ", " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mItemCode) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mItemUnit) & "'," & vbCrLf _
                    & " " & mReqdQty & ", '" & MainClass.AllowSingleQuote(mPriority) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mPurpose) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mRemarks) & "'," & vbCrLf _
                    & " '" & mStatus & "', " & mConsiderQty & "," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(mReqDate, "dd/MMM/yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                    & " " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mRJStatus & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mAPPRemarks) & "', '" & MainClass.AllowSingleQuote(mAddDesc) & "', '" & MainClass.AllowSingleQuote(mMake) & "','" & mQuotationApproved & "')"

                PubDBCn.Execute(SqlStr)
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        MsgBox(Err.Description)
        UpdateDetail1 = False
    End Function

    Private Sub ShowDetail1(ByRef mDivisionCode As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemCode As String
        Dim mPriority As String
        Dim mItemUOM As String = ""
        Dim mLastSuppCustName As String
        Dim mLastMrrDate As String
        Dim mCategoryCode As String
        Dim mCategoryDesc As String

        SqlStr = ""
        MainClass.ClearGrid(SprdMain)
        SqlStr = "SELECT * " & vbCrLf & " FROM PUR_INDENT_DET " & vbCrLf & " WHERE AUTO_KEY_INDENT=" & LblMKey.Text & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIndentDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsIndentDetail

            If .EOF = True Then Exit Sub
            I = 0
            .MoveFirst()
            Do While Not .EOF
                I = I + 1
                SprdMain.MaxRows = SprdMain.MaxRows + 1
                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)

                SprdMain.Col = ColItemDesc
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If

                mCategoryCode = ""
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CATEGORY_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCategoryCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If

                mCategoryDesc = ""
                If MainClass.ValidateWithMasterTable(mCategoryCode, "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCategoryDesc = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If

                SprdMain.Col = ColCategory
                SprdMain.Text = mCategoryDesc

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                mItemUOM = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColAddItemDesc
                SprdMain.Text = IIf(IsDBNull(.Fields("ADD_DESCRIPTION").Value), "", .Fields("ADD_DESCRIPTION").Value)

                SprdMain.Col = ColMake
                SprdMain.Text = IIf(IsDBNull(.Fields("MAKE").Value), "", .Fields("MAKE").Value)

                '' ''

                mLastSuppCustName = ""
                mLastMrrDate = ""

                SprdMain.Col = ColLastPORate
                SprdMain.Text = GetLastPORate(mItemCode, mLastSuppCustName, mLastMrrDate)

                SprdMain.Col = ColLastPurDate
                SprdMain.Text = VB6.Format(mLastMrrDate, "dd/MM/yyyy")          ''= CDate(mLastMrrDate).ToString("dd/MM/yyyy")

                SprdMain.Col = ColLastSupplier
                SprdMain.Text = MainClass.AllowSingleQuote(mLastSuppCustName)

                SprdMain.Col = ColMaxLevel
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "MAXIMUM_QTY", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Text = IIf(IsDBNull(MasterNo), 0, MasterNo)
                End If

                SprdMain.Col = ColReOderLevel
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "REORDER_QTY", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Text = IIf(IsDBNull(MasterNo), 0, MasterNo)
                End If

                SprdMain.Col = ColStock
                SprdMain.Text = GetBalanceStockQty(mItemCode, (txtIndentDate.Text), mItemUOM, "STR", "ST", "", ConWH, mDivisionCode) + GetBalanceStockQty(mItemCode, (txtIndentDate.Text), mItemUOM, "", "ST", "", ConSH, mDivisionCode)


                SprdMain.Col = ColQtyReqd
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("REQ_QTY").Value), 0, .Fields("REQ_QTY").Value)))

                SprdMain.Col = ColItemPriority
                mPriority = IIf(IsDBNull(.Fields("PRIORITY_LEVEL").Value), "", .Fields("PRIORITY_LEVEL").Value)

                Select Case mPriority
                    Case "R"
                        SprdMain.TypeComboBoxCurSel = 0
                    Case "U"
                        SprdMain.TypeComboBoxCurSel = 1
                    Case "M"
                        SprdMain.TypeComboBoxCurSel = 2
                End Select

                SprdMain.Col = ColPurpose
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_PURPOSE").Value), "", .Fields("ITEM_PURPOSE").Value)

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)

                SprdMain.Col = ColIndentStatus
                SprdMain.Value = IIf(.Fields("INDENT_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                SprdMain.Col = ColIndentRejected
                SprdMain.Value = IIf(.Fields("IS_REJECTED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                SprdMain.Col = ColQuotationApproved
                SprdMain.Text = IIf(IsDBNull(.Fields("QUOTATION_APP").Value), "", .Fields("QUOTATION_APP").Value)

                SprdMain.Col = ColAPPRemarks
                SprdMain.Text = IIf(IsDBNull(.Fields("APPROVAL_REMARKS").Value), "", .Fields("APPROVAL_REMARKS").Value)


                SprdMain.Col = ColConsiderQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CONSIDER_QTY").Value), 0, .Fields("CONSIDER_QTY").Value)))

                SprdMain.Col = ColReqDate
                SprdMain.Text = IIf(IsDBNull(.Fields("REQ_DATE").Value), "", VB6.Format(.Fields("REQ_DATE").Value, "dd/MM/yyyy"))

                .MoveNext()
            Loop

        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Function AutoGenIndentNoSeq() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Double
        Dim mStartingChk As Double
        Dim mFieldValue As String

        mAutoGen = 1


        'mStartingChk = CDbl(VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_INDENT)  AS AUTO_KEY_INDENT" & vbCrLf _
            & " FROM PUR_INDENT_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_INDENT,LENGTH(AUTO_KEY_INDENT)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                mFieldValue = IIf(IsDBNull(RsAutoGen.Fields("AUTO_KEY_INDENT").Value), 0, RsAutoGen.Fields("AUTO_KEY_INDENT").Value)
                If mFieldValue > 0 Then
                    mAutoGen = Mid(mFieldValue, 1, Len(mFieldValue) - 6)
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With

        AutoGenIndentNoSeq = mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function
    Private Sub InsertItemDetIntoGrid(ByRef mItemCode As String)

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemUOM As String = ""
        Dim mLastSuppCustName As String
        Dim mLastMrrDate As String
        Dim mCategoryCode As String
        Dim mCategoryDesc As String

        If Trim(mItemCode) = "" Then Exit Sub
        SqlStr = ""
        SqlStr = "SELECT ITEM_CODE,ITEM_SHORT_DESC,PURCHASE_UOM,MAXIMUM_QTY, REORDER_QTY,CATEGORY_CODE" & vbCrLf _
            & " FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsTemp
                SprdMain.Col = ColItemCode
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                SprdMain.Col = ColItemDesc
                SprdMain.Text = IIf(IsDBNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)

                mCategoryCode = IIf(IsDBNull(.Fields("CATEGORY_CODE").Value), "", .Fields("CATEGORY_CODE").Value)

                mCategoryDesc = ""
                If MainClass.ValidateWithMasterTable(mCategoryCode, "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCategoryDesc = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If

                SprdMain.Col = ColCategory
                SprdMain.Text = mCategoryDesc

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)
                mItemUOM = IIf(IsDBNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)

                SprdMain.Col = ColStock
                SprdMain.Text = GetBalanceStockQty(mItemCode, (txtIndentDate.Text), mItemUOM, "STR", "ST", "", ConWH, Val(txtDivision.Text)) + GetBalanceStockQty(mItemCode, (txtIndentDate.Text), mItemUOM, "", "ST", "", ConSH, Val(txtDivision.Text))
                mLastSuppCustName = ""
                mLastMrrDate = ""

                SprdMain.Col = ColLastPORate
                SprdMain.Text = GetLastPORate(Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)), mLastSuppCustName, mLastMrrDate)

                SprdMain.Col = ColLastPurDate
                SprdMain.Text = VB6.Format(mLastMrrDate, "dd/MM/yyyy")

                SprdMain.Col = ColLastSupplier
                SprdMain.Text = MainClass.AllowSingleQuote(mLastSuppCustName)

                SprdMain.Col = ColMaxLevel
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("MAXIMUM_QTY").Value), 0, .Fields("MAXIMUM_QTY").Value), "0.00")

                SprdMain.Col = ColReOderLevel
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("REORDER_QTY").Value), 0, .Fields("REORDER_QTY").Value), "0.00")



            End With
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
        End If
        RsTemp.Close()
        RsTemp = Nothing
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Function CheckDuplicateItem(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer
        '12/09/2001 duplicate item check not requied...
        '    Exit Function
        If mItemCode = "" Then CheckDuplicateItem = True : Exit Function
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
    Private Function CheckPendingItemForPO(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsPO As ADODB.Recordset = Nothing
        Dim mIndentNo As Double
        Dim mIndentQty As Double
        Dim mPOQty As Double
        Dim mDivisionCode As Double


        If Trim(txtDept.Text) = "" Then
            MsgBox("Please Enter Department First")
            CheckPendingItemForPO = True
            Exit Function
        End If

        If Val(txtDivision.Text) = 0 Then
            MsgBox("Please Enter Division First")
            CheckPendingItemForPO = True
            Exit Function
        End If
        mDivisionCode = Val(txtDivision.Text)

        CheckPendingItemForPO = False

        If IIf(IsDBNull(RsCompany.Fields("MAX_PENDING_INDENT").Value), 0, RsCompany.Fields("MAX_PENDING_INDENT").Value) = 0 Then
            CheckPendingItemForPO = False
            Exit Function
        End If

        mSqlStr = "SELECT IH.AUTO_KEY_INDENT, SUM(ID.REQ_QTY) As REQ_QTY " & vbCrLf _
            & " FROM PUR_INDENT_HDR IH, PUR_INDENT_DET ID" & vbCrLf _
            & " WHERE IH.AUTO_KEY_INDENT=ID.AUTO_KEY_INDENT AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(IH.AUTO_KEY_INDENT,LENGTH(IH.AUTO_KEY_INDENT)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
            & " AND APPROVAL_STATUS='Y' AND HOD_EMP_CODE IS NOT NULL AND ID.INDENT_STATUS='N'"

        mSqlStr = mSqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""

        mSqlStr = mSqlStr & vbCrLf & " AND IH.AUTO_KEY_INDENT<>" & Val(txtIndentNo.Text) & ""

        mSqlStr = mSqlStr & vbCrLf & "GROUP BY IH.AUTO_KEY_INDENT"
        mSqlStr = mSqlStr & vbCrLf & "ORDER BY IH.AUTO_KEY_INDENT DESC"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                mIndentNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_INDENT").Value), -1, RsTemp.Fields("AUTO_KEY_INDENT").Value)
                mIndentQty = IIf(IsDBNull(RsTemp.Fields("REQ_QTY").Value), 0, RsTemp.Fields("REQ_QTY").Value)

                mSqlStr = "SELECT SUM(IT.INDENT_QTY) AS ITEM_QTY" & vbCrLf _
                    & " FROM " & vbCrLf _
                    & " PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, PUR_POCONS_IND_TRN IT " & vbCrLf _
                    & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
                    & " AND ID.MKEY=IT.MKEY AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND ID.ITEM_CODE=IT.ITEM_CODE AND IH.PO_STATUS='Y' AND IH.PO_CLOSED='N'" & vbCrLf _
                    & " AND IT.AUTO_KEY_INDENT=" & mIndentNo & " " & vbCrLf _
                    & " AND IT.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' "

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPO, ADODB.LockTypeEnum.adLockReadOnly)

                If RsPO.EOF = False Then
                    mPOQty = IIf(IsDBNull(RsPO.Fields("ITEM_QTY").Value), 0, RsPO.Fields("ITEM_QTY").Value)
                    If mIndentQty > mPOQty Then
                        MsgInformation("Such Item Indent Already exsits & P.O. Not Complete." & vbCrLf & " Please Complete PO First." & vbCrLf & "Indent No : " & mIndentNo)
                        CheckPendingItemForPO = True
                        Exit Function
                    End If
                Else
                    MsgInformation("Such Item Indent Already exsits & P.O. Not Complete." & vbCrLf & " Please Complete PO First." & vbCrLf & "Indent No : " & mIndentNo)
                    CheckPendingItemForPO = True
                    Exit Function
                End If

                RsTemp.MoveNext()
            Loop
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function CheckQty(ByRef pCol As Integer, ByRef pRow As Integer) As Boolean

        On Error GoTo ERR1
        CheckQty = True
        With SprdMain
            .Row = pRow
            .Col = ColQtyReqd
            If Val(.Text) = 0 Then
                CheckQty = False
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColQtyReqd)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub cmdHODSearch_Click()
        On Error GoTo ErrPart
        'Dim SqlStr As String = ""
        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        'If ADDMode = True Then
        '    SqlStr = SqlStr & " AND (EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE IS NULL)"
        'Else
        '    SqlStr = SqlStr & " AND (EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE>TO_DATE('" & VB6.Format(txtIndentDate.Text, "dd/MM/yyyy") & "','DD-MON-YYYY'))"
        'End If

        'If MainClass.SearchGridMaster((txtHOD.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
        '    txtHOD.Text = AcName1
        '    txtHOD_Validating(txtHOD, New System.ComponentModel.CancelEventArgs(False))
        '    txtHOD.Focus()
        'End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtHOD_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHOD.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtHOD_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHOD.DoubleClick
        cmdHODSearch_Click()
    End Sub


    Private Sub txtHOD_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtHOD.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtHOD.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtHOD_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtHOD.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdHODSearch_Click()
    End Sub


    Private Sub txtHOD_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtHOD.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        'Dim SqlStr As String = ""

        'If Trim(txtHOD.Text) = "" Then GoTo EventExitSub

        ''txtHOD.Text = VB6.Format(txtHOD.Text, "000000")
        'If MainClass.ValidateWithMasterTable((txtHOD.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    lblHOD.Text = MasterNo
        'Else
        '    MsgBox("Invalid HOD Employee Code.", MsgBoxStyle.Information)
        '    Cancel = True
        '    GoTo EventExitSub
        'End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStatus.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Function GetLastPORate(ByRef pItemCode As String, ByRef mLastSuppCustName As String, ByRef mLastMrrDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPONo As Double
        Dim mLastSuppCustCode As String

        GetLastPORate = CStr(0)
        mLastSuppCustCode = ""
        mLastSuppCustName = ""
        mLastMrrDate = ""

        SqlStr = " SELECT IH.MRR_DATE, ID.REF_PO_NO, IH.SUPP_CUST_CODE" & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND IH.REF_TYPE='P'" & vbCrLf & " ORDER BY IH.MRR_DATE DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            mLastSuppCustCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mLastSuppCustCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLastSuppCustName = MasterNo
            End If


            mLastMrrDate = IIf(IsDBNull(RsTemp.Fields("MRR_DATE").Value), "", RsTemp.Fields("MRR_DATE").Value)
            mPONo = IIf(IsDBNull(RsTemp.Fields("REF_PO_NO").Value), -1, RsTemp.Fields("REF_PO_NO").Value)
        Else
            GetLastPORate = 0
            Exit Function
        End If

        SqlStr = " SELECT NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,2) AS PO_RATE" & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.PO_STATUS='Y' AND IH.PUR_TYPE='P' AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mLastSuppCustCode) & "'" & vbCrLf & " AND IH.AUTO_KEY_PO=" & mPONo & "" & vbCrLf & " AND AMEND_WEF_DATE=(" & vbCrLf & " SELECT MAX(AMEND_WEF_DATE) " & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.AUTO_KEY_PO=" & mPONo & "" & vbCrLf & " AND IH.PO_STATUS='Y' AND IH.PUR_TYPE='P' AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mLastSuppCustCode) & "'" & vbCrLf & " )"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            GetLastPORate = IIf(IsDBNull(RsTemp.Fields("PO_RATE").Value), 0, RsTemp.Fields("PO_RATE").Value)
        End If
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Function

    Private Sub FrmIndentEntry_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        Frame2.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        Frabot.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    'Private Sub UltraGrid1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles UltraGrid1.KeyPress
    '    If e.keyAscii = System.Windows.Forms.Keys.Return Then UltraGrid1_DoubleClick(UltraGrid1, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    'End Sub

    Private Sub txtProductCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProductCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.DoubleClick
        Call cmdSearchProduct_Click(cmdSearchProduct, New System.EventArgs())
    End Sub

    Private Sub txtProductCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProductCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProductCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProductCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProductCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchProduct_Click(cmdSearchProduct, New System.EventArgs())
    End Sub

    Private Sub txtProductCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProductCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub


        SqlStr = " SELECT ITEM_SHORT_DESC  " & vbCrLf _
            & " FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsTemp.EOF Then
            lblProductName.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)
        Else
            MsgBox("Not a valid Product Code")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdSearchProduct_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProduct.Click
        Dim SqlStr As String = ""
        SqlStr = " SELECT B.ITEM_CODE, B.ITEM_SHORT_DESC, B.CUSTOMER_PART_NO " & vbCrLf _
            & " FROM INV_ITEM_MST B, INV_GENERAL_MST C " & vbCrLf _
            & " WHERE B.COMPANY_CODE =C.COMPANY_CODE " & vbCrLf _
            & " AND B.CATEGORY_CODE = C.GEN_CODE " & vbCrLf _
            & " AND C.GEN_TYPE='C' AND C.PRD_TYPE IN ('P','I')" & vbCrLf _
            & " AND B.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & vbCrLf _
            & " ORDER BY B.ITEM_SHORT_DESC "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtProductCode.Text = AcName
            lblProductName.Text = AcName1
            If txtProductCode.Enabled = True Then txtProductCode.Focus()
        End If
    End Sub

    Private Sub cmdGetData_Click(sender As Object, e As EventArgs) Handles cmdGetData.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTempBOM As ADODB.Recordset
        Dim mProductCode As String = ""
        Dim mOrderQty As Double
        Dim cntRow As Integer
        Dim mItemCode As String
        'Dim mItemUOM As String = ""
        'Dim mIsChild As Boolean
        'Dim xAutoIssue As Boolean
        'Dim mProd_Type As Boolean
        Dim mDivisionCode As Double
        'Dim mStockQty As Double
        'Dim xItemUOM As String
        'Dim mDemandQty As Double
        'Dim mInvoiceDate As String
        Dim mMainItemCode As String
        'Dim mIssueNo As Double

        If Trim(txtSONo.Text) = "" Then Exit Sub
        If Trim(txtProductCode.Text) = "" Then Exit Sub
        If Trim(txtDept.Text) = "" Then Exit Sub
        If Trim(txtDivision.Text) = "" Then Exit Sub



        mDivisionCode = Val(txtDivision.Text)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(SprdMain)


        mProductCode = Trim(txtProductCode.Text)
        mMainItemCode = GetMainItemCode(mProductCode)

        mOrderQty = CDbl(VB6.Format(Val(txtPlanQty.Text), "0.00"))

        If mOrderQty > 0 Then
            Call ShowNewBOM(mMainItemCode, mOrderQty, mDivisionCode)
        End If

        txtProductCode.Enabled = False
        cmdSearchProduct.Enabled = False
        cmdGetData.Enabled = False
        FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowNewBOM(ByRef mProductCode As String, ByRef mProductPlanQty As Double, ByRef mDivisionCode As Double)

        On Error GoTo LedgError
        Dim RsMain As ADODB.Recordset = Nothing
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim pWEF As String
        Dim mTableName As String

        'Dim mRMCode As String
        'Dim mItemDesc As String
        'Dim mItemUOM As String = ""
        'Dim mStockType As String = ""
        'Dim mStockQty As Double
        'Dim mDeptQty As Double
        'Dim mStdQty As Double
        'Dim mRefNo As String
        'Dim pWIPLockQty As Double
        'Dim pFGQty As Double

        mTableName = ConInventoryTable

        SqlStr = " SELECT " & vbCrLf _
            & " IH.PRODUCT_CODE, ID.RM_CODE, INVMST.ITEM_SHORT_DESC AS RM_NAME, INVMST.PURCHASE_UOM,  INVMST.CATEGORY_CODE, INVMST.MAXIMUM_QTY, INVMST.REORDER_QTY," & vbCrLf _
            & " ((ID.STD_QTY +  ID.GROSS_WT_SCRAP) * DECODE(INVMST.PURCHASE_UOM,'KGS',0.001,DECODE(INVMST.PURCHASE_UOM,'TON',0.001* 0.001,1))) AS STD_QTY, " & vbCrLf _
            & " ID.DEPT_CODE, 'ST' AS STOCK_TYPE, "


        SqlStr = SqlStr & vbCrLf _
            & " (SELECT " & vbCrLf _
            & " --+ ORDERED INDEX (INV_STOCK_REC_TRN, IND_STK_REC_TRN_17) " & vbCrLf _
            & " SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY" & vbCrLf _
            & " FROM " & mTableName & "" & vbCrLf _
            & " WHERE COMPANY_CODE = IH.COMPANY_CODE AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND ITEM_CODE= ID.RM_CODE AND STOCK_ID='WH'" & vbCrLf _
            & " AND DIV_CODE=" & mDivisionCode & " AND STATUS='O'" & vbCrLf _
            & " AND STOCK_TYPE IN ('ST','QC') " & vbCrLf _
            & " AND E_DATE<=TO_DATE('" & VB6.Format(txtIndentDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtIndentDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " ) AS STR_STOCK_QTY "

        SqlStr = SqlStr & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND TRIM(ID.RM_CODE)=TRIM(INVMST.ITEM_CODE)" & vbCrLf _
            & " --AND  TRIM(IH.PRODUCT_CODE) = '" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _
            & " AND IH.STATUS='O'"


        SqlStr = SqlStr & vbCrLf _
            & " START WITH  TRIM(IH.PRODUCT_CODE) || '-' || IH.COMPANY_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf _
            & " CONNECT BY PRIOR TRIM(ID.RM_CODE) || IH.COMPANY_CODE || ' ' = (TRIM(IH.PRODUCT_CODE) || IH.COMPANY_CODE) || ' '"


        '& vbCrLf _
        '    & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"

        'SqlStr = SqlStr & vbCrLf _
        '    & " ORDER BY IH.PRODUCT_CODE, ID.RM_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            'Do While Not RsShow.EOF
            Call FillGridCol(RsShow, mProductCode, mProductCode, mProductPlanQty, mDivisionCode)
            '    RsShow.MoveNext()
            'Loop
        End If

        RsShow = Nothing
        Exit Sub
LedgError:
        ''    Resume
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillGridCol(ByRef pRs As ADODB.Recordset, ByRef pProductCode As String, ByRef pParentCode As String, ByRef mProductPlanQty As Double, ByRef mDivisionCode As Double)
        On Error GoTo FillGERR
        Dim mRMCode As String
        Dim mItemUOM As String = ""
        Dim mStockQty As Double
        Dim mDeptCode As String
        Dim mWIPStock As Double
        Dim mProd_Type As Boolean
        Dim xAutoIssue As Boolean
        Dim pRow As Integer

        Dim mItemDesc As String
        Dim mIsNewLine As Boolean
        Dim mcntRow As Long
        Dim mSTDQty As Double
        Dim mCategoryCode As String
        Dim mCategoryDesc As String
        Dim mLastSuppCustName As String = ""
        Dim mLastMrrDate As String = ""
        Dim mCategoryType As String = ""

        mIsNewLine = False

        MainClass.ClearGrid(SprdMain)

        mcntRow = 0
        Do While Not pRs.EOF
            With SprdMain

                mDeptCode = Trim(IIf(IsDBNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value))
                mRMCode = Trim(IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))

                mCategoryCode = IIf(IsDBNull(pRs.Fields("CATEGORY_CODE").Value), "", pRs.Fields("CATEGORY_CODE").Value)

                mCategoryDesc = ""
                If MainClass.ValidateWithMasterTable(mCategoryCode, "GEN_CODE", "PRD_TYPE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCategoryType = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If

                If mCategoryType = "I" Or mCategoryType = "P" Then
                    '.Row = pRow
                    GoTo NextRec
                End If

                pRow = 0
                If GetItemCodeAlreadyExists(IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value), pRow) = True Then
                    .Row = pRow
                    GoTo NextRec
                Else
                    mIsNewLine = True
                    mcntRow = mcntRow + 1
                    .Row = mcntRow
                End If


                .Col = ColItemCode
                .Text = IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)

                .Col = ColItemDesc
                .Text = IIf(IsDBNull(pRs.Fields("RM_NAME").Value), "", pRs.Fields("RM_NAME").Value) 'mItemDesc

                .Col = ColUnit
                .Text = IIf(IsDBNull(pRs.Fields("PURCHASE_UOM").Value), "", pRs.Fields("PURCHASE_UOM").Value) 'mItemUOM 'IIf(IsNull(pRs!ISSUE_UOM), "", pRs!ISSUE_UOM)

                '.Col = ColAddItemDesc
                '.Col = ColMake

                mCategoryCode = IIf(IsDBNull(pRs.Fields("CATEGORY_CODE").Value), "", pRs.Fields("CATEGORY_CODE").Value)

                mCategoryDesc = ""
                If MainClass.ValidateWithMasterTable(mCategoryCode, "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCategoryDesc = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If

                SprdMain.Col = ColCategory
                SprdMain.Text = mCategoryDesc

                mLastSuppCustName = ""
                mLastMrrDate = ""

                SprdMain.Col = ColLastPORate
                SprdMain.Text = GetLastPORate(Trim(IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)), mLastSuppCustName, mLastMrrDate)

                SprdMain.Col = ColLastPurDate
                SprdMain.Text = VB6.Format(mLastMrrDate, "dd/MM/yyyy")

                SprdMain.Col = ColLastSupplier
                SprdMain.Text = MainClass.AllowSingleQuote(mLastSuppCustName)

                SprdMain.Col = ColMaxLevel
                SprdMain.Text = VB6.Format(IIf(IsDBNull(pRs.Fields("MAXIMUM_QTY").Value), 0, pRs.Fields("MAXIMUM_QTY").Value), "0.00")

                SprdMain.Col = ColReOderLevel
                SprdMain.Text = VB6.Format(IIf(IsDBNull(pRs.Fields("REORDER_QTY").Value), 0, pRs.Fields("REORDER_QTY").Value), "0.00")



                .Col = ColStock
                mStockQty = IIf(IsDBNull(pRs.Fields("STR_STOCK_QTY").Value), 0, pRs.Fields("STR_STOCK_QTY").Value) ''19/12/2018
                .Text = CStr(mStockQty)

                'SprdMain.Col = ColStock
                'SprdMain.Text = GetBalanceStockQty(mItemCode, (txtIndentDate.Text), mItemUOM, "STR", "ST", "", ConWH, Val(txtDivision.Text)) + GetBalanceStockQty(mItemCode, (txtIndentDate.Text), mItemUOM, "", "ST", "", ConSH, Val(txtDivision.Text))


                '.Col = ColItemPriority
                '.Col = ColPurpose
                '.Col = ColRemarks
                .Col = ColReqDate
                .Text = txtIndentDate.Text

                '.Col = ColIndentStatus
                '.Col = ColIndentRejected
                '.Col = ColAPPRemarks
                '.Col = ColConsiderQty

NextRec:

                mSTDQty = Val(IIf(IsDBNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value))

                .Col = ColQtyReqd
                .Text = Val(.Text) + (mSTDQty * Val(txtPlanQty.Text))

                If mIsNewLine = True Then
                    .MaxRows = .MaxRows + 1
                End If
                mIsNewLine = False
            End With
            pRs.MoveNext()
        Loop


NextRecd:

        Exit Sub
FillGERR:
        '    Resume
        MsgBox(Err.Description)
    End Sub
    Private Function GetItemCodeAlreadyExists(ByRef pItemCode As Object, ByRef pRow As Object) As Boolean
        On Error GoTo FillGERR
        Dim xRow As Integer

        pRow = 0
        GetItemCodeAlreadyExists = False
        With SprdMain
            For xRow = 1 To .MaxRows
                .Row = xRow
                .Col = ColItemCode
                If Trim(.Text) <> "" Then
                    If Trim(.Text) = Trim(pItemCode) Then
                        pRow = xRow
                        GetItemCodeAlreadyExists = True
                    End If
                End If
            Next
        End With
        Exit Function
FillGERR:
        GetItemCodeAlreadyExists = False
    End Function

    Private Sub cmdSearchSONo_Click(sender As Object, e As EventArgs) Handles cmdSearchSONo.Click
        Dim SqlStr As String = ""
        SqlStr = " SELECT IH.AUTO_KEY_SO, IH.SO_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO,SO_QTY " & vbCrLf _
            & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE =CMST.COMPANY_CODE " & vbCrLf _
            & " And IH.SUPP_CUST_CODE = CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE =INVMST.COMPANY_CODE " & vbCrLf _
            & " And ID.ITEM_CODE = INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND IH.SO_STATUS='O' AND IH.SO_APPROVED='Y' "

        SqlStr = SqlStr & vbCrLf _
            & " AND NOT EXISTS (SELECT A.AUTO_KEY_SO " & vbCrLf _
            & " FROM PUR_INDENT_HDR A" & vbCrLf _
            & " WHERE A.AUTO_KEY_SO = IH.AUTO_KEY_SO " & vbCrLf _
            & " AND A.PRODUCT_CODE=ID.ITEM_CODE)"

        SqlStr = SqlStr & vbCrLf _
            & " ORDER BY IH.AUTO_KEY_SO, INVMST.ITEM_SHORT_DESC  "

        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtSONo.Text = AcName
            lblCustomerName.Text = AcName2
            txtProductCode.Text = AcName3
            lblProductName.Text = AcName4
            txtPlanQty.Text = AcName6

            If txtProductCode.Enabled = True Then txtProductCode.Focus()
        End If
    End Sub
    Private Sub txtSONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSONo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSONo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSONo.DoubleClick
        Call cmdSearchSONo_Click(cmdSearchSONo, New System.EventArgs())
    End Sub

    Private Sub txtSONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSONo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSONo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSONo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchSONo_Click(cmdSearchSONo, New System.EventArgs())
    End Sub

    Private Sub txtSONo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSONo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        If Trim(txtSONo.Text) = "" Then GoTo EventExitSub


        SqlStr = " SELECT *  " & vbCrLf _
            & " FROM DSP_SALEORDER_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND AUTO_KEY_SO = '" & MainClass.AllowSingleQuote(txtSONo.Text) & "' " & vbCrLf _
            & " AND SO_STATUS='O' AND SO_APPROVED='Y' "

        'SqlStr = " SELECT IH.AUTO_KEY_SO, IH.SO_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO " & vbCrLf _
        '    & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST" & vbCrLf _
        '    & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
        '    & " AND IH.COMPANY_CODE =CMST.COMPANY_CODE " & vbCrLf _
        '    & " And IH.SUPP_CUST_CODE = CMST.SUPP_CUST_CODE " & vbCrLf _
        '    & " AND IH.COMPANY_CODE =INVMST.COMPANY_CODE " & vbCrLf _
        '    & " And ID.ITEM_CODE = INVMST.ITEM_CODE " & vbCrLf _
        '    & " AND IH.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        '    & " AND IH.SO_STATUS='O' AND IH.SO_APPROVED='Y' "

        'SqlStr = SqlStr & vbCrLf _
        '    & " NOT EXISTS ( SELECT MKEY, PRODUCT_CODE " & vbCrLf _
        '    & " FROM DSP_SALEORDER_HDR A, DSP_SALEORDER_DET B" & vbCrLf _
        '    & " WHERE A.MKEY=B.MKEY " & vbCrLf _
        '    & " AND A.COMPANY_CODE =IH.COMPANY_CODE " & vbCrLf _
        '    & " And A.AUTO_KEY_SO = IH.AUTO_KEY_SO " & vbCrLf _
        '    & " AND B.ITEM_CODE = ID.ITEM_CODE )"

        'SqlStr = SqlStr & vbCrLf _
        '    & " ORDER BY IH.AUTO_KEY_SO, INVMST.ITEM_SHORT_DESC  "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsTemp.EOF Then
            'lblProductName.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)
        Else
            MsgBox("Not a valid Sales Order No")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdReOrderLevel_Click(sender As Object, e As EventArgs) Handles cmdReOrderLevel.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTempBOM As ADODB.Recordset
        Dim mProductCode As String = ""
        Dim mOrderQty As Double
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mTableName As String
        Dim mDivisionCode As Double
        Dim mProductPlanQty As Double
        Dim mMainItemCode As String
        Dim RsShow As ADODB.Recordset

        If Trim(txtDept.Text) = "" Then Exit Sub
        If Trim(txtDivision.Text) = "" Then Exit Sub
        mDivisionCode = Val(txtDivision.Text)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(SprdMain)

        mTableName = ConInventoryTable

        SqlStr = " SELECT " & vbCrLf _
            & " INVMST.ITEM_CODE PRODUCT_CODE, INVMST.ITEM_CODE RM_CODE, INVMST.ITEM_SHORT_DESC AS RM_NAME, " & vbCrLf _
            & " INVMST.PURCHASE_UOM,  INVMST.CATEGORY_CODE, INVMST.MAXIMUM_QTY,INVMST.REORDER_QTY," & vbCrLf _
            & " 1 AS STD_QTY, " & vbCrLf _
            & " 'STR' AS DEPT_CODE, 'ST' AS STOCK_TYPE, " & vbCrLf _
            & " SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS STR_STOCK_QTY" & vbCrLf _
            & " FROM " & mTableName & " TRN, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE TRN.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TRN.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND TRN.COMPANY_CODE = INVMST.COMPANY_CODE " & vbCrLf _
            & " And TRN.ITEM_CODE= INVMST.ITEM_CODE And TRN.STOCK_ID='WH'" & vbCrLf _
            & " AND TRN.DIV_CODE=" & mDivisionCode & " AND TRN.STATUS='O' AND INVMST.ITEM_STATUS='A'" & vbCrLf _
            & " AND STOCK_TYPE IN ('ST','QC') " & vbCrLf _
            & " AND E_DATE<=TO_DATE('" & VB6.Format(txtIndentDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtIndentDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf _
            & " AND INVMST.REORDER_QTY>0"

        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY INVMST.ITEM_CODE, INVMST.ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.PURCHASE_UOM," & vbCrLf _
            & " INVMST.CATEGORY_CODE, INVMST.MAXIMUM_QTY,INVMST.REORDER_QTY"

        SqlStr = SqlStr & vbCrLf _
            & " HAVING  SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1))<= INVMST.REORDER_QTY"


        SqlStr = SqlStr & vbCrLf _
            & " ORDER BY INVMST.ITEM_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)
        mProductPlanQty=1
        If Not RsShow.EOF Then
            'Do While Not RsShow.EOF
            Call FillGridCol(RsShow, mProductCode, mProductCode, mProductPlanQty, mDivisionCode)
            '    RsShow.MoveNext()
            'Loop
        End If

        cmdReOrderLevel.Enabled = False
        FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
