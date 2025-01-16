Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Imports AxFPSpreadADO

Friend Class frmDS
    Inherits System.Windows.Forms.Form
    Dim RsDSMain As ADODB.Recordset ''ADODB.Recordset
    Dim RsDSDetail As ADODB.Recordset ''ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection
    Dim mSearchStartRow As Integer

    Dim mSearchKey As String
    Dim cntSearchRow As Long
    Dim cntSearchCol As Long

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String


    Private Const ConRowHeight As Short = 14
    Dim mAmendSchd As Boolean
    Dim pmyMenu As String

    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColItemUOM As Short = 3
    Private Const ColPlanningQty As Short = 4
    Private Const ColItemDetail As Short = 5
    Private Const ColWeek1Qty As Short = 6
    Private Const ColWeek2Qty As Short = 7
    Private Const ColWeek3Qty As Short = 8
    Private Const ColWeek4Qty As Short = 9
    Private Const ColWeek5Qty As Short = 10
    Private Const ColTotQty As Short = 11
    Private Const ColRecdQty As Short = 12
    Private Const ColShortQty As Short = 13

    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtDSNo.Enabled = False
            cmdPopulate.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsDSMain.EOF = False Then RsDSMain.MoveFirst()
            Show1()
            txtDSNo.Enabled = True
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub cmdAmendSchd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAmendSchd.Click

        On Error GoTo ModifyErr

        '    If CDate(PubCurrDate) > CDate(txtScheduleDate.Text) Then
        '        MsgInformation "MOnth Closed so Cann't be Modified."
        '        Exit Sub
        '    End If

        Call Refresh1()
        ADDMode = False
        MODIFYMode = True
        MainClass.ButtonStatus(Me, XRIGHT, RsDSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        txtDSNo.Enabled = False
        txtDSAmendNo.Text = CStr(Val(txtDSAmendNo.Text) + 1)
        txtDSAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtScheduleDate.Enabled = False
        cmdAmendSchd.Enabled = False
        cmdRefresh.Enabled = False
        txtPONo.Enabled = False
        txtPODate.Enabled = False
        cmdPoSearch.Enabled = False
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        mAmendSchd = True
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If ValidateBranchLocking((txtDSDate.Text)) = True Then
            Exit Sub
        End If

        If ValidateBookLocking(PubDBCn, CInt(ConLockPO_DS), txtDSDate.Text) = True Then
            Exit Sub
        End If

        If ValidateAccountLocking(PubDBCn, (txtDSDate.Text), (txtSupplierName.Text)) = True Then
            Exit Sub
        End If
        '
        '    If chkStatus.Value = vbChecked Then
        '        MsgInformation "Posted DS Cann't be Deleted"
        '        Exit Sub
        '    End If


        If Val(txtDSAmendNo.Text) > 0 Then
            MsgInformation("Amend DS Cann't be Deleted")
            Exit Sub
        End If

        If txtDSNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsDSMain.EOF Then
            If MainClass.ValidateWithMasterTable(Val(lblMkey.Text), "AUTO_KEY_DELV", "AUTO_KEY_DELV", "PUR_DELV_SCHLD_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND POST_FLAG='Y'") = True Then
                MsgInformation("Posted DS Cann't be Deleted")
                Exit Sub
            End If
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PUR_DELV_SCHLD_HDR", (txtDSNo.Text), RsDSMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PUR_DELV_SCHLD_HDR", "AUTO_KEY_DELV", (lblMkey.Text)) = False Then GoTo DelErrPart

                If DeleteDSDailyDetail(PubDBCn, Val(lblMkey.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM PUR_DELV_SCHLD_DET WHERE AUTO_KEY_DELV=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM PUR_DELV_SCHLD_HDR WHERE AUTO_KEY_DELV=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsDSMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        RsDSMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub txtDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDivision.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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

        If MainClass.ValidateWithMasterTable((txtDivision.Text), "DIV_CODE", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
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

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Posted DS Cann't be Modified")
                Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsDSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtDSNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
            txtDSNo.Enabled = True

        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdPopulate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulate.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim xAcctCode As String
        Dim mPlanningQty As Double

        If Val(txtPONo.Text) = 0 Then
            MsgInformation("Please Select PO NO.")
            Exit Sub
        End If

        If Not IsDate(txtScheduleDate.Text) Then
            MsgInformation("Please Select valid Schedule Date")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "Supp_Cust_Name", "Supp_Cust_Code", "Fin_Supp_Cust_MSt", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            MsgInformation("InValid Supplier Name.")
            Exit Sub
        End If

        If DSExsistInCurrSchdMon(xAcctCode, Val(txtPONo.Text), Trim(txtScheduleDate.Text)) = True Then
            Exit Sub
        End If

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        SqlStr = " SELECT ID.ITEM_CODE,  ID.ITEM_UOM, INVMST.ITEM_SHORT_DESC " & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_PO=" & Val(txtPONo.Text) & "" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & xAcctCode & "'" & vbCrLf _
            & " AND IH.PUR_TYPE||IH.ORDER_TYPE IN ('PO','JC') AND IH.PO_STATUS='Y' AND AMEND_NO=" & Val(txtPOAmendNo.Text) & ""

        If CDate(txtScheduleDate.Text) < CDate(PubGSTApplicableDate) Then
            SqlStr = SqlStr & vbCrLf & " AND ISGSTENABLE_PO='N'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND ISGSTENABLE_PO='Y'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        I = 1
        If RsTemp.EOF = False Then
            MainClass.ClearGrid(SprdMain, ConRowHeight)
            With SprdMain
                Do While Not RsTemp.EOF
                    .Row = I
                    .Col = ColItemCode
                    .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                    .Col = ColItemName
                    .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                    .Col = ColItemUOM
                    .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)

                    .Col = ColPlanningQty
                    mPlanningQty = GetPlanningFromCustomerDS(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value), Trim(txtScheduleDate.Text))
                    .Text = VB6.Format(mPlanningQty, "0")

                    I = I + 1
                    .MaxRows = I
                    RsTemp.MoveNext()
                Loop
            End With
        End If

        FormatSprdMain(-1)
        txtCode.Enabled = False
        txtSupplierName.Enabled = False
        cmdsearch.Enabled = False
        txtPONo.Enabled = False
        txtPODate.Enabled = False
        Exit Sub

ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPoSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPoSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim pSuppCode As String

        If (txtSupplierName.Text) = "" Then
            MsgInformation("Please Enter Valid Supplier Name")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "Supp_Cust_Name", "Supp_Cust_Code", "Fin_Supp_Cust_MSt", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            pSuppCode = MasterNo
        Else
            MsgInformation("InValid Supplier Name.")
            Exit Sub
        End If

        SqlStr = "SELECT DISTINCT POMain.AUTO_KEY_PO, POMain.PUR_ORD_DATE, POMain.AMEND_NO, POMain.AMEND_WEF_DATE, PODetail.ITEM_CODE, INV.ITEM_SHORT_DESC " & vbCrLf _
                        & " FROM PUR_PURCHASE_HDR POMain,PUR_PURCHASE_DET PODetail, INV_ITEM_MST INV" & vbCrLf _
                        & " WHERE POMain.MKEY=PODetail.MKEY " & vbCrLf _
                        & " And POMain.Company_Code=INV.Company_Code And PODetail.ITEM_CODE=INV.ITEM_CODE" & vbCrLf _
                        & " And POMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " And  POMain.PUR_TYPE||POMain.ORDER_TYPE IN ('PO','JC') AND ISGSTENABLE_PO='Y'"

        SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & pSuppCode & "'" & vbCrLf & " AND PO_STATUS='Y' AND PO_CLOSED='N'"

        If IsDate(txtDSDate.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND PODetail.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtDSDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If Val(txtDivision.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND DIV_CODE='" & Val(txtDivision.Text) & "'"
        End If

        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtPONo.Text = AcName
            txtPONO_Validating(txtPONo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Refresh1()

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xAcctCode As String

        If Trim(txtPONo.Text) = "" Then Exit Sub

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "Supp_Cust_Name", "Supp_Cust_Code", "Fin_Supp_Cust_MSt", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            MsgInformation("InValid Supplier Name.")
            Exit Sub
        End If

        SqlStr = " SELECT PUR_ORD_DATE , AMEND_NO, AMEND_DATE, AMEND_WEF_DATE " & vbCrLf & " FROM PUR_PURCHASE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If RsCompany.Fields("FYEAR").Value < ConOPENPO_CONTINOUS_YEAR Then
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        Else
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & ""
        End If

        If CDate(txtScheduleDate.Text) < CDate(PubGSTApplicableDate) Then
            SqlStr = SqlStr & vbCrLf & " AND ISGSTENABLE_PO='N'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND ISGSTENABLE_PO='Y'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_PO=" & Val(txtPONo.Text) & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & xAcctCode & "'" & vbCrLf _
            & " AND PUR_TYPE||ORDER_TYPE IN ('PO','JC') AND PO_STATUS='Y' AND PO_CLOSED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtPODate.Text = IIf(IsDBNull(RsTemp.Fields("PUR_ORD_DATE").Value), "", RsTemp.Fields("PUR_ORD_DATE").Value)
            txtPOAmendNo.Text = IIf(IsDBNull(RsTemp.Fields("AMEND_NO").Value), "", RsTemp.Fields("AMEND_NO").Value)
            txtPOAmendDate.Text = IIf(IsDBNull(RsTemp.Fields("AMEND_DATE").Value), "", RsTemp.Fields("AMEND_DATE").Value)
            txtWEF.Text = IIf(IsDBNull(RsTemp.Fields("AMEND_WEF_DATE").Value), "", RsTemp.Fields("AMEND_WEF_DATE").Value)
        Else
            MsgBox("Invalid PO NO.", MsgBoxStyle.Information)
            Exit Sub
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        Call Refresh1()
    End Sub


    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtDSNo_Validating(txtDSNo, New System.ComponentModel.CancelEventArgs(False))
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
        Dim SqlStr As String = ""
        Dim mDSNo As Double
        Dim mPostFlag As String
        Dim mScheduleStatus As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mPostFlag = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If VB.Left(cboStatus.Text, 1) = "O" Then
            mScheduleStatus = "N"
        Else
            mScheduleStatus = "Y"
        End If

        SqlStr = ""
        mDSNo = Val(txtDSNo.Text)
        If Val(txtDSNo.Text) = 0 Then
            mDSNo = AutoGenPONoSeq()
        End If
        txtDSNo.Text = CStr(mDSNo)


        If ADDMode = True Then
            lblMkey.Text = CStr(mDSNo)
            SqlStr = " INSERT INTO PUR_DELV_SCHLD_HDR ( " & vbCrLf & "  COMPANY_CODE , AUTO_KEY_DELV," & vbCrLf & "  DELV_SCHLD_DATE , DELV_AMEND_NO," & vbCrLf & "  DELV_AMEND_DATE , AUTO_KEY_PO," & vbCrLf & "  SUPP_CUST_CODE , SCHLD_DATE," & vbCrLf & "  EMP_CODE , SCHLD_STATUS," & vbCrLf & "  REMARKS , POST_FLAG," & vbCrLf & "  PO_DATE , PO_AMEND_NO," & vbCrLf & "  AMEND_DATE , AMEND_WEF_DATE, IS_MAIL, " & vbCrLf & "  ADDUSER, ADDDATE, MODUSER, MODDATE) "

            SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mDSNo & "," & vbCrLf & " TO_DATE('" & VB6.Format(txtDSDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtDSAmendNo.Text) & ", " & vbCrLf & " TO_DATE('" & VB6.Format(txtDSAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtPONo.Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtCode.Text)) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " ''," & vbCrLf & " '" & mScheduleStatus & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', " & vbCrLf & " '" & mPostFlag & "'," & vbCrLf & " TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtPOAmendNo.Text) & ", " & vbCrLf & " TO_DATE('" & VB6.Format(txtPOAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), 'N', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','')"
        End If

        If MODIFYMode = True Then
            SqlStr = " UPDATE PUR_DELV_SCHLD_HDR SET " & vbCrLf & " AUTO_KEY_DELV= " & mDSNo & "," & vbCrLf & " DELV_SCHLD_DATE=TO_DATE('" & VB6.Format(txtDSDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " DELV_AMEND_NO=" & Val(txtDSAmendNo.Text) & ", " & vbCrLf & " DELV_AMEND_DATE=TO_DATE('" & VB6.Format(txtDSAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " AUTO_KEY_PO=" & Val(txtPONo.Text) & ", " & vbCrLf & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtCode.Text)) & "', " & vbCrLf & " SCHLD_DATE=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " EMP_CODE=''," & vbCrLf & " SCHLD_STATUS='" & mScheduleStatus & "'," & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', " & vbCrLf & " POST_FLAG='" & mPostFlag & "'," & vbCrLf & " PO_DATE=TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " PO_AMEND_NO=" & Val(txtPOAmendNo.Text) & ", " & vbCrLf & " AMEND_DATE=TO_DATE('" & VB6.Format(txtPOAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " AMEND_WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), IS_MAIL='N', " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_DELV =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1() = False Then GoTo ErrPart
        If UpdateDailyDSDetail() = False Then GoTo ErrPart

        Update1 = True
        PubDBCn.CommitTrans()
        txtDSNo.Text = CStr(mDSNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsDSMain.Requery()
        RsDSDetail.Requery()
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function UpdateDailyDSDetail() As Boolean
        On Error GoTo UpdateErr1
        Dim RsTemp_SRLNo As ADODB.Recordset
        Dim ii As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String


        With SprdMain
            For ii = 1 To .MaxRows - 1
                .Row = ii
                .Col = ColItemCode
                mItemCode = Trim(.Text)
                SqlStr = "INSERT INTO PUR_DAILY_SCHLD_DET (" & vbCrLf _
                    & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf _
                    & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
                    & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE )" & vbCrLf _
                    & " SELECT " & vbCrLf & " " & Val(txtDSNo.Text) & ", " & ii & ", ITEM_CODE, " & vbCrLf _
                    & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
                    & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE " & vbCrLf _
                    & " FROM TEMP_PUR_DAILY_SCHLD_DET " & vbCrLf _
                    & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                    & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
                    & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
                    & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                PubDBCn.Execute(SqlStr)


                SqlStr = "INSERT INTO PUR_DAILY_SCHLD_HIS_DET (" & vbCrLf _
                    & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf _
                    & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
                    & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE,DELV_AMEND_NO )" & vbCrLf _
                    & " SELECT " & vbCrLf _
                    & " " & Val(txtDSNo.Text) & ", " & ii & ", ITEM_CODE, " & vbCrLf _
                    & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
                    & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE," & Val(txtDSAmendNo.Text) & " " & vbCrLf _
                    & " FROM TEMP_PUR_DAILY_SCHLD_DET " & vbCrLf _
                    & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                    & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtCode.Text)) & "'" & vbCrLf _
                    & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
                    & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                PubDBCn.Execute(SqlStr)


            Next
        End With

        UpdateDailyDSDetail = True
        Exit Function
UpdateErr1:
        UpdateDailyDSDetail = False
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
    End Function
    Public Function DeleteDSDailyDetail(ByRef pDBCn As ADODB.Connection, ByRef pMkey As Double) As Boolean
        Dim SqlStr As String = ""
        On Error GoTo DeleteDSDailyDetailErr
        SqlStr = ""
        SqlStr = "DELETE FROM PUR_DAILY_SCHLD_DET  " & vbCrLf & " WHERE AUTO_KEY_DELV=" & Val(CStr(pMkey)) & " "
        pDBCn.Execute(SqlStr)

        SqlStr = "DELETE FROM PUR_DAILY_SCHLD_HIS_DET  " & vbCrLf & " WHERE AUTO_KEY_DELV=" & Val(CStr(pMkey)) & " AND DELV_AMEND_NO=" & Val(txtDSAmendNo.Text) & ""
        pDBCn.Execute(SqlStr)

        DeleteDSDailyDetail = True
        Exit Function
DeleteDSDailyDetailErr:
        MsgInformation(Err.Description)
        DeleteDSDailyDetail = False
    End Function
    Private Function AutoGenPONoSeq() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Integer
        Dim SqlStr As String = ""
        Dim mStartingChk As Double
        Dim mMaxValue As String
        mAutoGen = 1

        'mStartingChk = CDbl(50000 & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_DELV)  " & vbCrLf _
            & " FROM PUR_DELV_SCHLD_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_DELV,LENGTH(AUTO_KEY_DELV)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(RsAutoGen.Fields(0).Value) Then
                    mMaxValue = RsAutoGen.Fields(0).Value
                    mAutoGen = Mid(mMaxValue, 1, Len(mMaxValue) - 6)
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With

        AutoGenPONoSeq = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemUOM As String = ""
        Dim mWeek1Qty As Double
        Dim mWeek2Qty As Double
        Dim mWeek3Qty As Double
        Dim mWeek4Qty As Double
        Dim mWeek5Qty As Double
        Dim mTotQty As Double
        Dim mRecdQty As Double
        Dim mShortQty As Double

        If DeleteDSDailyDetail(PubDBCn, Val(lblMkey.Text)) = False Then GoTo UpdateDetail1

        SqlStr = "Delete From  PUR_DELV_SCHLD_DET " & vbCrLf & " Where " & vbCrLf & " AUTO_KEY_DELV=" & Val(lblMkey.Text) & "" & vbCrLf

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemUOM
                mItemUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColWeek1Qty
                mWeek1Qty = Val(.Text)

                .Col = ColWeek2Qty
                mWeek2Qty = Val(.Text)

                .Col = ColWeek3Qty
                mWeek3Qty = Val(.Text)

                .Col = ColWeek4Qty
                mWeek4Qty = Val(.Text)

                .Col = ColWeek5Qty
                mWeek5Qty = Val(.Text)

                .Col = ColTotQty
                mTotQty = Val(.Text)

                .Col = ColRecdQty
                mRecdQty = Val(.Text)

                .Col = ColShortQty
                mShortQty = Val(.Text)

                SqlStr = ""

                If mItemCode <> "" Then 'And mTotQty > 0 'If DS Amend Then Print ...
                    SqlStr = " INSERT INTO PUR_DELV_SCHLD_DET ( " & vbCrLf & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf & " ITEM_UOM, WEEK1_QTY, WEEK2_QTY, " & vbCrLf & " WEEK3_QTY, WEEK4_QTY, " & vbCrLf & " WEEK5_QTY, TOTAL_QTY, " & vbCrLf & " REC_QTY, SHORT_QTY, COMPANY_CODE) "

                    SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ", " & vbCrLf & " '" & mItemCode & "','" & mItemUOM & "', " & vbCrLf & " " & mWeek1Qty & ", " & mWeek2Qty & ", " & vbCrLf & " " & mWeek3Qty & "," & mWeek4Qty & "," & mWeek5Qty & ", " & vbCrLf & " " & mTotQty & "," & vbCrLf & " " & mRecdQty & "," & mShortQty & "," & RsCompany.Fields("COMPANY_CODE").Value & ") "

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtSupplierName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtSupplierName.Text = AcName
            txtSupplierName_Validating(txtSupplierName, New System.ComponentModel.CancelEventArgs(False))
            If txtCode.Enabled = True Then txtCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SearchCode()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtCode.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", "SUPP_CUST_NAME", , , SqlStr) = True Then
            txtCode.Text = AcName
            txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
            txtCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItem.Click

        On Error GoTo ErrPart
        Dim mSearchItem As String
        Dim mFindItemName As String
        Dim I As Integer

        mSearchItem = Trim(txtSearchItem.Text)
        Dim counter As Short
        With SprdMain
            counter = mSearchStartRow
            For I = counter To .MaxRows
                .Row = I

                .Col = ColItemCode
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                    mSearchStartRow = I + 1
                    GoTo NextRec
                End If

                .Col = ColItemName
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                    mSearchStartRow = I + 1
                    GoTo NextRec
                End If
            Next
            mSearchStartRow = 1
NextRec:
        End With




        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            'MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh
            FormatSprdView()
            UltraGrid1.Refresh()
            UltraGrid1.Focus()
            UltraGrid1.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            UltraGrid1.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsDSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmDS_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Purchase Delivery Schedule"

        SqlStr = "Select * From PUR_DELV_SCHLD_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PUR_DELV_SCHLD_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)

        SetTextLengths()
        cboStatus.Items.Clear()
        cboStatus.Items.Add("Open")
        cboStatus.Items.Add("Close")
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim SqlStr As String
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)
        ClearGroupFromUltraGrid(UltraGrid1)
        ClearFilterFromUltraGrid(UltraGrid1)

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf & " A.AUTO_KEY_DELV AS DSNo, A.DELV_SCHLD_DATE As DS_DATE, " & vbCrLf & " A.DELV_AMEND_NO AS Amendno, A.DELV_AMEND_DATE AS AmendDate,  " & vbCrLf & " B.SUPP_CUST_NAME AS NAME, A.AUTO_KEY_PO AS PO_NO, " & vbCrLf & " A.SCHLD_DATE, DECODE(A.SCHLD_STATUS,'N','OPEN','CLOSE') AS Status, " & vbCrLf & " A.REMARKS, DECODE(A.POST_FLAG,'Y','YES','NO') AS Posted " & vbCrLf & " FROM PUR_DELV_SCHLD_HDR A, FIN_SUPP_CUST_MST B " & vbCrLf & " WHERE A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE " & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DELV,LENGTH(AUTO_KEY_DELV)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & " ORDER BY A.AUTO_KEY_DELV"

        'MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        'FormatSprdView()

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        Me.UltraGrid1.DataSource = ds
        Me.UltraGrid1.DataMember = ""

        CreateGridHeader("S")


        oledbAdapter.Dispose()
        oledbCnn.Close()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "DS No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "DS Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "DS Amend No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "DS Amend Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "Customer Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "Customer PO No"

            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Schedule Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "Status"
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "Remarks"
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Header.Caption = "Posted"




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
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Width = 100


            'UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.MFGQty).MaskInput = "99999"

            Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            Me.UltraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")
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

    Private Sub frmDS_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If

        If KeyAscii = 6 Then
            SprdMain.Row = 1
            SprdMain.Row2 = SprdMain.MaxRows
            SprdMain.Col = 1
            SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
            SprdMain.BlockMode = True
            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            SprdMain.BlockMode = False

            mSearchKey = ""
            cntSearchRow = 1
            cntSearchCol = 1
            mSearchKey = InputBox("Search :", "Search", mSearchKey)
            If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColShortQty)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If

    End Sub

    Private Sub frmDS_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        Call SetMainFormCordinate(Me)
        pmyMenu = myMenu
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

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

        mAccountCode = CStr(-1)
        lblMkey.Text = ""
        txtDSNo.Text = ""
        txtDSDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDSAmendNo.Text = CStr(0)
        txtDSAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtWEF.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtSupplierName.Text = ""
        txtCode.Text = ""
        txtCode.Enabled = True
        txtSupplierName.Enabled = True
        cmdsearch.Enabled = True
        SprdMain.Enabled = True

        txtDivision.Text = ""
        txtDivision.Enabled = True

        txtPONo.Text = ""
        txtPODate.Text = ""
        txtPOAmendNo.Text = ""
        txtPOAmendDate.Text = ""
        txtScheduleDate.Text = "01/" & VB6.Format(Month(RunDate), "00") & "/" & VB6.Format(Year(RunDate), "0000")
        cboStatus.SelectedIndex = 0
        txtRemarks.Text = ""

        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""

        txtDSAmendNo.Enabled = False
        txtDSAmendDate.Enabled = False

        chkStatus.Enabled = False

        cmdAmendSchd.Enabled = False
        cmdRefresh.Enabled = False
        cmdPopulate.Enabled = False

        cboStatus.Enabled = False
        txtDSDate.Enabled = False
        txtScheduleDate.Enabled = True
        Call DelTemp_DailyDetail()
        txtPONo.Enabled = True
        txtPODate.Enabled = True
        cmdPoSearch.Enabled = True
        mAmendSchd = False
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsDSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""


        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsDSDetail.Fields("ITEM_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(.Col, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("Item_Short_Desc", "INV_ITEM_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 20)
            .TypeEditMultiLine = True

            .Col = ColItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsDSDetail.Fields("ITEM_UOM").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 4)

            .Col = ColItemDetail
            .CellType = SS_CELL_TYPE_BUTTON
            '.Lock = False
            .TypeButtonText = "Details"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColItemDetail, 6)

            .Col = ColPlanningQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColWeek1Qty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSDetail.Fields("WEEK1_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .set_ColWidth(ColWeek1Qty, 8)


            .Col = ColWeek2Qty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSDetail.Fields("WEEK2_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColWeek2Qty, 8)

            .Col = ColWeek3Qty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSDetail.Fields("WEEK3_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColWeek3Qty, 8)

            .Col = ColWeek4Qty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSDetail.Fields("WEEK4_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColWeek4Qty, 8)

            .Col = ColWeek5Qty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSDetail.Fields("WEEK5_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColWeek5Qty, 8)

            .Col = ColTotQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSDetail.Fields("TOTAL_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColTotQty, 9)

            .Col = ColRecdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSDetail.Fields("REC_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRecdQty, 10)

            .Col = ColShortQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSDetail.Fields("SHORT_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColShortQty, 8)

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColPlanningQty)
            '        MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColPlanningQty, ColPlanningQty
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColWeek1Qty, ColShortQty)
            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub



    Private Sub FormatSprdView()

        'With SprdView
        '    .Row = -1
        '    .set_RowHeight(0, 600)
        '    .set_ColWidth(0, 500)
        '    .set_ColWidth(1, 1000)
        '    .set_ColWidth(2, 1000)
        '    .set_ColWidth(3, 1000)
        '    .set_ColWidth(4, 1200)
        '    .set_ColWidth(5, 3500)
        '    .set_ColWidth(6, 1000)
        '    .set_ColWidth(7, 1000)
        '    .set_ColWidth(8, 1000)
        '    .set_ColWidth(9, 2000)
        '    .set_ColWidth(10, 1000)
        '    .set_ColWidth(11, 1200)
        '    .ColsFrozen = 2
        '    MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
        '    MainClass.SetSpreadColor(SprdView, -1)
        '    .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
        '    MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        'End With
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1

        txtDSNo.MaxLength = RsDSMain.Fields("AUTO_KEY_DELV").Precision
        txtDSDate.MaxLength = RsDSMain.Fields("DELV_SCHLD_DATE").DefinedSize - 6
        txtSupplierName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtRemarks.MaxLength = RsDSMain.Fields("REMARKS").DefinedSize

        txtDSAmendNo.MaxLength = RsDSMain.Fields("DELV_AMEND_NO").Precision
        txtDSAmendDate.MaxLength = RsDSMain.Fields("DELV_AMEND_DATE").DefinedSize - 6
        txtWEF.MaxLength = RsDSMain.Fields("AMEND_WEF_DATE").DefinedSize - 6

        txtSupplierName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtCode.MaxLength = RsDSMain.Fields("SUPP_CUST_CODE").DefinedSize

        txtPONo.MaxLength = RsDSMain.Fields("AUTO_KEY_PO").Precision
        txtPODate.MaxLength = RsDSMain.Fields("PO_DATE").DefinedSize - 6
        txtPOAmendNo.MaxLength = RsDSMain.Fields("PO_AMEND_NO").Precision
        txtPOAmendDate.MaxLength = RsDSMain.Fields("AMEND_DATE").DefinedSize - 6
        txtScheduleDate.MaxLength = RsDSMain.Fields("SCHLD_DATE").DefinedSize - 6



        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mItemCode As String
        Dim mTotQty As Double
        Dim mWeek1Qty As Double
        Dim mWeek2Qty As Double
        Dim mWeek3Qty As Double
        Dim mWeek4Qty As Double
        Dim mWeek5Qty As Double

        Dim mRecdQty As Double


        Dim I As Integer
        Dim pDSNo As Double
        Dim mTotSchdQty As Double
        Dim mPlanningQty As Double
        Dim mCategoryType As String

        FieldsVarification = True
        If ValidateBranchLocking((txtScheduleDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ValidateBookLocking(PubDBCn, CInt(ConLockPO_DS), txtScheduleDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ValidateAccountLocking(PubDBCn, (txtScheduleDate.Text), (txtSupplierName.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If MODIFYMode = True Then
            If mAmendSchd = False Then
                If RsDSMain.Fields("POST_FLAG").Value = "Y" Then
                    MsgInformation("Posted DS Cann't be Modified")
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            If RsDSMain.Fields("SCHLD_STATUS").Value = "Y" Then
                MsgInformation("Closed DS Cann't be Modified")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsDSMain.EOF = True Then Exit Function

        If MODIFYMode = True And Trim(txtDSNo.Text) = "" Then
            MsgInformation("PO No. is Blank")
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtDSDate.Text) = "" Then
            MsgInformation(" PO Date is empty. Cannot Save")
            txtDSDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtDSDate.Text) <> "" Then
            If IsDate(txtDSDate.Text) = False Then
                MsgInformation(" Invalid PO Date. Cannot Save")
                txtDSDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtSupplierName.Text) = "" Then
            MsgInformation("Supplier Name is Blank. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCode.Text = MasterNo
        Else
            MsgInformation("Invalid Supplier Name. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STOP_PO='Y'") = True Then
            MsgBox("PO/Delivery Schedule Lock for Such Supplier, So cann't be saved", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            Exit Function
        End If

        If VB6.Format(txtScheduleDate.Text, "YYYYMM") < VB6.Format(txtDSDate.Text, "YYYYMM") Then
            MsgInformation("Schedule Date Cann't be Less Than Delivery Schedule Date")
            txtScheduleDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If DSExsistInCurrSchdMon((txtCode.Text), Val(txtPONo.Text), Trim(txtScheduleDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        ''28-09-2005

        'If PubSuperUser = "U" Then
        '    If ConOnlineData = False Then
        '        If Val(txtDSNo.Text) <> 0 Then
        '            pDSNo = CDbl(Mid(txtDSNo.Text, 1, Len(txtDSNo.Text) - 6))


        '            If Val(CStr(pDSNo)) < 50000 Then
        '                MsgInformation("Not in Your series. Cannot Save")
        '                FieldsVarification = False
        '                Exit Function
        '            End If

        '        End If
        '    End If
        'End If

        For I = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = I
            SprdMain.Col = ColItemCode
            mItemCode = Trim(UCase(SprdMain.Text))

            If CheckItemLock(mItemCode, "S", "") = True Then
                MsgInformation("Delivery Schedule Lock For Item Code : " & mItemCode & ". So cann't be made MRR for this Item.")
                FieldsVarification = False
                Exit Function
            End If

            SprdMain.Col = ColPlanningQty
            mPlanningQty = Val(SprdMain.Text)

            SprdMain.Col = ColTotQty
            mTotQty = Val(SprdMain.Text)

            SprdMain.Col = ColWeek1Qty
            mWeek1Qty = Val(SprdMain.Text)

            SprdMain.Col = ColWeek2Qty
            mWeek2Qty = Val(SprdMain.Text)

            SprdMain.Col = ColWeek3Qty
            mWeek3Qty = Val(SprdMain.Text)

            SprdMain.Col = ColWeek4Qty
            mWeek4Qty = Val(SprdMain.Text)

            SprdMain.Col = ColWeek5Qty
            mWeek5Qty = Val(SprdMain.Text)

            '        If mItemCode = "R04493" Then MsgBox "ok"

            If mItemCode <> "" Then
                If mTotQty > 0 Then
                    If CheckDSDetailExists(mItemCode, I, mTotQty) = False Then
                        MsgInformation("Please Check Delivery Detail Qty. For Item Code :" & mItemCode)
                        MainClass.SetFocusToCell(SprdMain, I, ColItemDetail)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If

                If IIf(IsDBNull(RsCompany.Fields("PUR_PLANNING").Value), "N", RsCompany.Fields("PUR_PLANNING").Value) = "Y" Then
                    mTotSchdQty = 0
                    If CheckPlanningQty(mItemCode, mTotQty, mTotSchdQty, mPlanningQty) = False Then
                        MsgInformation("Delivery Schedule Qty Cann't be Greater than Planning Qty." & vbNewLine & "For Item Code :" & mItemCode & " Plan Qty : " & mPlanningQty & IIf(mTotSchdQty - mTotQty > 0, " Total Given Schedule : " & mTotSchdQty - mTotQty, ""))
                        MainClass.SetFocusToCell(SprdMain, I, ColItemDetail)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If

                mRecdQty = CalcRecvQty(Val(txtPONo.Text), mItemCode, Trim(txtCode.Text), "M")
                If mTotQty < mRecdQty Then
                    MsgInformation("Already Recd Qty is " & mRecdQty & " Agt Schedule " & mTotQty & " for Item Code :" & mItemCode & ", So Cann't be Saved.")
                    MainClass.SetFocusToCell(SprdMain, I, ColItemDetail)
                    FieldsVarification = False
                    Exit Function
                End If

                If RsCompany.Fields("WEEKLY_SCHD").Value = "Y" Then
                    mRecdQty = CalcRecvQty(Val(txtPONo.Text), mItemCode, Trim(txtCode.Text), "W1")
                    If mWeek1Qty < mRecdQty Then
                        MsgInformation("Already Recd Qty for Week 1 is " & mRecdQty & " Agt Schedule " & mWeek1Qty & " for Item Code :" & mItemCode & ", So Cann't be Saved.")
                        MainClass.SetFocusToCell(SprdMain, I, ColItemDetail)
                        FieldsVarification = False
                        Exit Function
                    End If

                    mRecdQty = CalcRecvQty(Val(txtPONo.Text), mItemCode, Trim(txtCode.Text), "W2")
                    If mWeek2Qty < mRecdQty Then
                        MsgInformation("Already Recd Qty for Week 2 is " & mRecdQty & " Agt Schedule " & mWeek2Qty & " for Item Code :" & mItemCode & ", So Cann't be Saved.")
                        MainClass.SetFocusToCell(SprdMain, I, ColItemDetail)
                        FieldsVarification = False
                        Exit Function
                    End If

                    mRecdQty = CalcRecvQty(Val(txtPONo.Text), mItemCode, Trim(txtCode.Text), "W3")
                    If mWeek3Qty < mRecdQty Then
                        MsgInformation("Already Recd Qty for Week 3 is " & mRecdQty & " Agt Schedule " & mWeek3Qty & " for Item Code :" & mItemCode & ", So Cann't be Saved.")
                        MainClass.SetFocusToCell(SprdMain, I, ColItemDetail)
                        FieldsVarification = False
                        Exit Function
                    End If

                    mRecdQty = CalcRecvQty(Val(txtPONo.Text), mItemCode, Trim(txtCode.Text), "W4")
                    If mWeek4Qty < mRecdQty Then
                        MsgInformation("Already Recd Qty for Week 4 is " & mRecdQty & " Agt Schedule " & mWeek4Qty & " for Item Code :" & mItemCode & ", So Cann't be Saved.")
                        MainClass.SetFocusToCell(SprdMain, I, ColItemDetail)
                        FieldsVarification = False
                        Exit Function
                    End If

                    mRecdQty = CalcRecvQty(Val(txtPONo.Text), mItemCode, Trim(txtCode.Text), "W5")
                    If mWeek5Qty < mRecdQty Then
                        MsgInformation("Already Recd Qty for Week 5 is " & mRecdQty & " Agt Schedule " & mWeek5Qty & " for Item Code :" & mItemCode & ", So Cann't be Saved.")
                        MainClass.SetFocusToCell(SprdMain, I, ColItemDetail)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If

        Next


        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Please Check Item Code.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColItemName, "S", "Please Check Item Description.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColItemUOM, "S", "Please Check Unit.") = False Then FieldsVarification = False

        '    If MainClass.ValidDataInGrid(SprdMain, ColTotQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function CalcRecvQty(ByRef CurrPONo As Double, ByRef CurrItemCode As String, ByRef pSupplierCode As String, ByRef pType As String) As Double

        On Error GoTo CalcRecvQtyErr
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim xPoNo As String
        Dim xFYNo As Integer
        Dim SqlStr As String = ""
        Dim CurrMrrNo As Double
        Dim xSchldDate As String
        Dim mLastDayOfMonth As String

        If pType = "M" Then
            xSchldDate = "01/" & VB6.Format(txtScheduleDate.Text, "MM") & "/" & VB6.Format(txtScheduleDate.Text, "YYYY")
            mLastDayOfMonth = MainClass.LastDay(Month(CDate(txtScheduleDate.Text)), Year(CDate(txtScheduleDate.Text))) & "/" & VB6.Format(txtScheduleDate.Text, "MM") & "/" & VB6.Format(txtScheduleDate.Text, "YYYY")
        ElseIf pType = "W1" Then
            xSchldDate = "01/" & VB6.Format(txtScheduleDate.Text, "MM") & "/" & VB6.Format(txtScheduleDate.Text, "YYYY")
            mLastDayOfMonth = "07/" & VB6.Format(txtScheduleDate.Text, "MM") & "/" & VB6.Format(txtScheduleDate.Text, "YYYY")
        ElseIf pType = "W2" Then
            xSchldDate = "08/" & VB6.Format(txtScheduleDate.Text, "MM") & "/" & VB6.Format(txtScheduleDate.Text, "YYYY")
            mLastDayOfMonth = "14/" & VB6.Format(txtScheduleDate.Text, "MM") & "/" & VB6.Format(txtScheduleDate.Text, "YYYY")
        ElseIf pType = "W3" Then
            xSchldDate = "15/" & VB6.Format(txtScheduleDate.Text, "MM") & "/" & VB6.Format(txtScheduleDate.Text, "YYYY")
            mLastDayOfMonth = "21/" & VB6.Format(txtScheduleDate.Text, "MM") & "/" & VB6.Format(txtScheduleDate.Text, "YYYY")
        ElseIf pType = "W4" Then
            xSchldDate = "22/" & VB6.Format(txtScheduleDate.Text, "MM") & "/" & VB6.Format(txtScheduleDate.Text, "YYYY")
            mLastDayOfMonth = "28/" & VB6.Format(txtScheduleDate.Text, "MM") & "/" & VB6.Format(txtScheduleDate.Text, "YYYY")
        Else
            xSchldDate = "29/" & VB6.Format(txtScheduleDate.Text, "MM") & "/" & VB6.Format(txtScheduleDate.Text, "YYYY")
            mLastDayOfMonth = MainClass.LastDay(Month(CDate(txtScheduleDate.Text)), Year(CDate(txtScheduleDate.Text))) & "/" & VB6.Format(txtScheduleDate.Text, "MM") & "/" & VB6.Format(txtScheduleDate.Text, "YYYY")
        End If

        SqlStr = ""

        SqlStr = "SELECT SUM(ID.RECEIVED_QTY) AS RECDQTY " & vbCrLf & " FROM INV_GATE_DET ID WHERE " & vbCrLf & " ID.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUBSTR(ID.AUTO_KEY_MRR,LENGTH(ID.AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " --AND TRIM(ID.SUPP_CUST_CODE)='" & MainClass.AllowSingleQuote(UCase(pSupplierCode)) & "' " & vbCrLf & " AND ID.REF_AUTO_KEY_NO=" & Val(CStr(CurrPONo)) & " "

        ''
        SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(CurrItemCode) & "' "

        SqlStr = SqlStr & vbCrLf & " AND ID.REF_TYPE='P'"
        SqlStr = SqlStr & vbCrLf & " AND ID.MRR_DATE>=TO_DATE('" & VB6.Format(xSchldDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ID.MRR_DATE<=TO_DATE('" & VB6.Format(mLastDayOfMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            CalcRecvQty = Val(IIf(IsDBNull(RsMisc.Fields(0).Value), 0, RsMisc.Fields(0).Value))
        Else
            CalcRecvQty = 0.0#
        End If
        Exit Function
CalcRecvQtyErr:
        CalcRecvQty = 0.0#
        MsgBox(Err.Description)
    End Function
    Private Function CheckDSDetailExists(ByRef nItemCode As String, ByRef mSerialNo As Integer, ByRef mDSQty As Double) As Boolean

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing '' ADODB.Recordset

        SqlStr = "SELECT SUM(PLANNED_QTY) AS PLANNED_QTY" & vbCrLf & " FROM TEMP_PUR_DAILY_SCHLD_DET " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND ITEM_CODE='" & Trim(nItemCode) & "'" & vbCrLf & " GROUP BY ITEM_CODE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If Val(RsTemp.Fields("PLANNED_QTY").Value) = mDSQty Then
                CheckDSDetailExists = True
            Else
                CheckDSDetailExists = False
            End If
        Else
            CheckDSDetailExists = False
        End If
    End Function

    Private Function CheckPlanningQty(ByRef nItemCode As String, ByRef mDSQty As Double, ByRef mTotSchdQty As Double, ByRef mPlanningQty As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing '' ADODB.Recordset
        Dim mRMUOM As String
        Dim mPURUOM As String
        Dim mFactor As String
        Dim mPackStd As Double
        Dim mSchdQty As Double
        Dim mPackQty As Double
        Dim mExcessAprovalQty As Double
        Dim mRecdQty As Double

        '    mPlanningQty = 0

        CheckPlanningQty = False
        mTotSchdQty = 0
        If mDSQty = 0 Then
            CheckPlanningQty = True
            Exit Function
        End If

        mExcessAprovalQty = CheckExcessDSApprovalQty(nItemCode, VB6.Format(txtScheduleDate.Text, "DD/MM/YYYY"), Trim(txtCode.Text), mDSQty)
        mExcessAprovalQty = mExcessAprovalQty + CheckInterChangeDSApprovalQty(nItemCode, VB6.Format(txtScheduleDate.Text, "DD/MM/YYYY"), Trim(txtCode.Text))

        SqlStr = " SELECT PRD_TYPE " & vbCrLf & " FROM INV_ITEM_MST INVMST, INV_GENERAL_MST GMST" & vbCrLf & " WHERE INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INVMST.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND INVMST.CATEGORY_CODE=GMST.GEN_CODE" & vbCrLf & " AND INVMST.ITEM_CODE='" & Trim(nItemCode) & "'" & vbCrLf & " AND GMST.GEN_TYPE='C' AND GMST.PRD_TYPE IN ('B','I','R','3')" ''R',  ''3 : Raw material Tube

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            CheckPlanningQty = True
            Exit Function
        End If


        '    SqlStr = "SELECT IH.RM_CODE, IH.RM_UOM, INVMST.PURCHASE_UOM,INVMST.UOM_FACTOR, INVMST.PACK_STD," & vbCrLf _
        ''            & " SUM(IH.RM_QTY) + MAX(INVMST.MINIMUM_QTY) - MAX(IH.STOCK_QTY) AS PLANNED_QTY" & vbCrLf _
        ''            & " FROM INV_PROCESS_MONTHLY_SCHLD IH, INV_ITEM_MST INVMST " & vbCrLf _
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND IH.COMPANY_CODE = INVMST.COMPANY_CODE " & vbCrLf _
        ''            & " AND IH.RM_CODE = INVMST.ITEM_CODE" & vbCrLf _
        ''            & " AND IH.RM_CODE='" & Trim(nItemCode) & "'" & vbCrLf _
        ''            & " AND TO_CHAR(IH.PROCESS_DATE,'YYYYMM')='" & VB6.Format(txtScheduleDate, "YYYYMM") & "'" & vbCrLf _
        ''            & " GROUP BY IH.RM_CODE, IH.RM_UOM, INVMST.PURCHASE_UOM,INVMST.UOM_FACTOR, INVMST.PACK_STD"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '    If RsTemp.EOF = False Then
        '        mPlanningQty = IIf(IsNull(RsTemp!PLANNED_QTY), 0, RsTemp!PLANNED_QTY)
        '        mPlanningQty = IIf(mPlanningQty < 0, 0, mPlanningQty)
        '        mRMUOM = IIf(IsNull(RsTemp!RM_UOM), "", RsTemp!RM_UOM)
        '        mPURUOM = IIf(IsNull(RsTemp!PURCHASE_UOM), "", RsTemp!PURCHASE_UOM)
        '        mFactor = IIf(IsNull(RsTemp!UOM_FACTOR), 1, RsTemp!UOM_FACTOR)
        '
        '        If mRMUOM <> mPURUOM Then
        '            mPlanningQty = mPlanningQty / mFactor
        '        End If
        '
        '        mPackStd = IIf(IsNull(RsTemp!PACK_STD), 0, RsTemp!PACK_STD)
        '        If mPackStd > 0 Then
        '            mPlanningQty = mPlanningQty / mPackStd
        '            mPlanningQty = IIf(Int(mPlanningQty) = mPlanningQty, mPlanningQty, Int(mPlanningQty) + 1) * mPackStd
        '        End If
        '
        '    End If

        '    mPlanningQty = 0
        '    mPlanningQty = GetPlanningFromCustomerDS(nItemCode, txtScheduleDate)

        SqlStr = " SELECT SUM(ID.TOTAL_QTY) AS TOTAL_QTY, ID.ITEM_CODE, ID.ITEM_UOM, INVMST.PURCHASE_UOM, INVMST.UOM_FACTOR,PACK_STD " & vbCrLf & " FROM PUR_DELV_SCHLD_HDR IH, PUR_DELV_SCHLD_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf & " AND ID.COMPANY_CODE = INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE = INVMST.ITEM_CODE" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(nItemCode) & "'"

        If Val(txtDSNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_DELV <> " & Val(txtDSNo.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'YYYYMM')='" & VB6.Format(txtScheduleDate.Text, "YYYYMM") & "'" & vbCrLf & " GROUP BY ID.ITEM_CODE, ID.ITEM_UOM, INVMST.PURCHASE_UOM,INVMST.UOM_FACTOR,PACK_STD"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mSchdQty = 0
        mTotSchdQty = 0
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mSchdQty = IIf(IsDBNull(RsTemp.Fields("TOTAL_QTY").Value), 0, RsTemp.Fields("TOTAL_QTY").Value)
                mRMUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                mPURUOM = IIf(IsDBNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                mFactor = IIf(IsDBNull(RsTemp.Fields("UOM_FACTOR").Value), 1, RsTemp.Fields("UOM_FACTOR").Value)
                mPackQty = IIf(IsDBNull(RsTemp.Fields("PACK_STD").Value), 1, RsTemp.Fields("PACK_STD").Value)

                If mRMUOM <> mPURUOM Then
                    mSchdQty = mSchdQty / CDbl(mFactor)
                End If


                mTotSchdQty = mTotSchdQty + mSchdQty

                RsTemp.MoveNext()
            Loop
        End If

        mTotSchdQty = mTotSchdQty + mDSQty

        If Val(CStr(mPlanningQty + mExcessAprovalQty)) >= mTotSchdQty Then
            CheckPlanningQty = True
        Else
            CheckPlanningQty = False
            '        mRecdQty = CheckRecdQty(nItemCode)
            '        If mRecdQty >= mTotSchdQty Then
            '            CheckPlanningQty = True
            '        Else
            '            CheckPlanningQty = False
            '        End If
            '        If CheckRecdQty(nItemCode, mPlanningQty, mDSQty) = True Then
            '            CheckPlanningQty = True
            '        Else
            '            CheckPlanningQty = False
            '        End If
        End If
        Exit Function
ErrPart:
        CheckPlanningQty = False
    End Function
    Private Function CheckRecdQty(ByRef nItemCode As String, ByRef mPlanningQty As Double, ByRef mDSQty As Double) As Double



        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing '' ADODB.Recordset
        Dim mRMUOM As String
        Dim mPURUOM As String
        Dim mFactor As String
        Dim mPackStd As Double
        Dim mMRRQty As Double
        Dim mAmendNo As Integer

        If Val(txtDSAmendNo.Text) = 0 Then
            mAmendNo = 0 ''Val(txtDSAmendNo.Text) - 1
            '        CheckRecdQty = False
            '        Exit Function
        Else
            mAmendNo = Val(txtDSAmendNo.Text) - 1
        End If


        SqlStr = " SELECT SUM(ID.RECEIVED_QTY) AS RECEIVED_QTY" & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf & " AND DECODE(SHIPPED_TO_SAMEPARTY,'Y',IH.SUPP_CUST_CODE,SHIPPED_TO_PARTY_CODE)='" & MainClass.AllowSingleQuote((txtCode.Text)) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(nItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(IH.MRR_DATE,'YYYYMM')='" & VB6.Format(txtScheduleDate.Text, "YYYYMM") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mMRRQty = 0
        If RsTemp.EOF = False Then
            mMRRQty = IIf(IsDBNull(RsTemp.Fields("RECEIVED_QTY").Value), 0, RsTemp.Fields("RECEIVED_QTY").Value)
        End If

        CheckRecdQty = mMRRQty
        '    If Val(VB6.Format(mMRRQty, "0.00")) <= Val(VB6.Format(mPlanningQty, "0.00")) Then
        '        CheckRecdQty = True
        '        If Val(VB6.Format(mDSQty, "0.00")) < Val(VB6.Format(mMRRQty, "0.00")) Then
        '            CheckRecdQty = False
        '        End If
        '    Else
        '        CheckRecdQty = True
        '    End If

        Exit Function
ErrPart:
        CheckRecdQty = 0
    End Function
    Private Function GetPlanningFromCustomerDSOld(ByRef nItemCode As String, ByRef mDSDate As String, Optional ByRef mMsg As String = "") As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing '' ADODB.Recordset
        Dim RsTempDS As ADODB.Recordset = Nothing '' ADODB.Recordset
        Dim mRMUOM As String
        Dim mPURUOM As String
        Dim mFactor As String = "1"
        Dim mPackStd As Double
        Dim mSchdQty As Double
        Dim mFromDate As String
        Dim mToDate As String
        Dim mProductCode As String = ""
        Dim mLastDate As String
        Dim mPlanningQty As Double
        Dim mStdQty As Double
        Dim RsItem As ADODB.Recordset = Nothing
        Dim mDSQty As Double
        Dim PreLevelStdQty As Double
        Dim mPreLevel As Double

        GetPlanningFromCustomerDSOld = 0
        mPlanningQty = 0
        mStdQty = 0


        mMsg = ""
        mFromDate = VB6.Format("01/" & VB6.Format(mDSDate, "MM/YYYY"), "DD/MM/YYYY")
        mLastDate = MainClass.LastDay(Month(CDate(mDSDate)), Year(CDate(mDSDate)))
        mToDate = VB6.Format(mLastDate & "/" & VB6.Format(mDSDate, "MM/YYYY"), "DD/MM/YYYY")

        SqlStr = " SELECT UOM_FACTOR, PACK_STD" & vbCrLf & " FROM INV_ITEM_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & nItemCode & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItem, ADODB.LockTypeEnum.adLockReadOnly)

        If RsItem.EOF = False Then
            mFactor = IIf(IsDBNull(RsItem.Fields("UOM_FACTOR").Value), 1, RsItem.Fields("UOM_FACTOR").Value)
            mPackStd = IIf(IsDBNull(RsItem.Fields("PACK_STD").Value), 0, RsItem.Fields("PACK_STD").Value)
        End If

        SqlStr = " SELECT  " & vbCrLf & " LEVEL,TRN.PRODUCT_CODE, TRN.STD_QTY+  GROSS_WT_SCRAP AS STD_QTY, DEPT_CODE" & vbCrLf & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " START WITH  TRIM(RM_CODE) || '-' || TRN.COMPANY_CODE='" & MainClass.AllowSingleQuote(nItemCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE) || ' '=TRIM(RM_CODE) || COMPANY_CODE || ' '"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mProductCode = IIf(IsDBNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value)

                If RsTemp.Fields("Level").Value = 1 Then
                    mStdQty = IIf(IsDBNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value)
                Else
                    If RsTemp.Fields("Level").Value > mPreLevel Then
                        mStdQty = mStdQty * IIf(IsDBNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value)
                    Else
                        mStdQty = PreLevelStdQty
                    End If
                End If

                SqlStr = " SELECT  SUM(PLANNED_QTY) AS PLANNED_QTY" & vbCrLf & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_DELV = ID.AUTO_KEY_DELV" & vbCrLf & " AND TRIM(ID.ITEM_CODE) IN (" & vbCrLf & " SELECT TRIM(REF_ITEM_CODE) FROM INV_ITEM_RELATIONSHIP_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " UNION " & vbCrLf & " SELECT '" & mProductCode & "' FROM DUAL" & vbCrLf & " )" & vbCrLf & " AND ID.SERIAL_DATE >=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ID.SERIAL_DATE <=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDS, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTempDS.EOF = False Then
                    mDSQty = IIf(IsDBNull(RsTempDS.Fields("PLANNED_QTY").Value), 0, RsTempDS.Fields("PLANNED_QTY").Value) ''* 0.9  ''90% of Customer Delivery Schedule..
                    mPlanningQty = mPlanningQty + (mDSQty * mStdQty)
                    If IIf(IsDBNull(RsTempDS.Fields("PLANNED_QTY").Value), 0, RsTempDS.Fields("PLANNED_QTY").Value) > 0 Then
                        mMsg = mMsg & IIf(mMsg = "", "", vbCrLf) & mProductCode & " : " & IIf(IsDBNull(RsTempDS.Fields("PLANNED_QTY").Value), 0, RsTempDS.Fields("PLANNED_QTY").Value)
                    End If
                End If
                mPreLevel = RsTemp.Fields("Level").Value
                PreLevelStdQty = mStdQty 'IIf(IsNull(RsTemp!STD_QTY), 0, RsTemp!STD_QTY)
                RsTemp.MoveNext()
            Loop
        End If

        SqlStr = " SELECT  SUM(PLANNED_QTY) AS PLANNED_QTY" & vbCrLf & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_DELV = ID.AUTO_KEY_DELV" & vbCrLf & " AND TRIM(ID.ITEM_CODE) ='" & MainClass.AllowSingleQuote(nItemCode) & "'" & vbCrLf & " AND ID.SERIAL_DATE >=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ID.SERIAL_DATE <=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDS, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempDS.EOF = False Then
            mPlanningQty = mPlanningQty + IIf(IsDBNull(RsTempDS.Fields("PLANNED_QTY").Value), 0, RsTempDS.Fields("PLANNED_QTY").Value)
            If IIf(IsDBNull(RsTempDS.Fields("PLANNED_QTY").Value), 0, RsTempDS.Fields("PLANNED_QTY").Value) > 0 Then
                mMsg = mMsg & IIf(mMsg = "", "", vbCrLf) & nItemCode & " : " & IIf(IsDBNull(RsTempDS.Fields("PLANNED_QTY").Value), 0, RsTempDS.Fields("PLANNED_QTY").Value)
            End If
        End If

        mPlanningQty = mPlanningQty / IIf(CDbl(mFactor) = 0, 1, mFactor)

        If mPackStd > 0 Then
            mPlanningQty = mPlanningQty / mPackStd
            mPlanningQty = IIf(Int(mPlanningQty) = mPlanningQty, mPlanningQty, Int(mPlanningQty) + 1) * mPackStd
        End If

        GetPlanningFromCustomerDSOld = Int(mPlanningQty) ''+ 1

        Exit Function
ErrPart:
        GetPlanningFromCustomerDSOld = False
    End Function
    Private Function GetPlanningFromCustomerDS(ByRef nItemCode As String, ByRef mDSDate As String, Optional ByRef mMsg As String = "") As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing '' ADODB.Recordset
        Dim RsTempDS As ADODB.Recordset = Nothing '' ADODB.Recordset
        Dim mRMUOM As String
        Dim mPURUOM As String
        Dim mFactor As String = 1
        Dim mPackStd As Double
        Dim mSchdQty As Double
        Dim mFromDate As String
        Dim mToDate As String
        Dim mProductCode As String = ""
        Dim mLastDate As String
        Dim mPlanningQty As Double
        Dim mStdQty As Double
        Dim RsItem As ADODB.Recordset = Nothing
        Dim mDSQty As Double
        Dim PreLevelStdQty As Double
        Dim mPreLevel As Double
        Dim mOpeningBal As Double
        Dim mMinBal As Double

        GetPlanningFromCustomerDS = 0
        mPlanningQty = 0
        mStdQty = 0


        mMsg = ""
        mFromDate = VB6.Format("01/" & VB6.Format(mDSDate, "MM/YYYY"), "DD/MM/YYYY")
        mLastDate = MainClass.LastDay(Month(CDate(mDSDate)), Year(CDate(mDSDate)))
        mToDate = VB6.Format(mLastDate & "/" & VB6.Format(mDSDate, "MM/YYYY"), "DD/MM/YYYY")

        SqlStr = " SELECT UOM_FACTOR, PACK_STD, MINIMUM_QTY" & vbCrLf & " FROM INV_ITEM_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & nItemCode & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItem, ADODB.LockTypeEnum.adLockReadOnly)

        If RsItem.EOF = False Then
            mFactor = IIf(IsDBNull(RsItem.Fields("UOM_FACTOR").Value), 1, RsItem.Fields("UOM_FACTOR").Value)
            mPackStd = IIf(IsDBNull(RsItem.Fields("PACK_STD").Value), 0, RsItem.Fields("PACK_STD").Value)
            mMinBal = IIf(IsDBNull(RsItem.Fields("MINIMUM_QTY").Value), 0, RsItem.Fields("MINIMUM_QTY").Value)
        End If



        SqlStr = " SELECT  PRODUCT_CODE, SUM(DPLAN_QTY) AS PLANNED_QTY, SUM(RM_QTY) AS RM_QTY, MAX(STOCK_QTY) AS STOCK_QTY" & vbCrLf & " FROM INV_PROCESS_MONTHLY_SCHLD IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRIM(IH.RM_CODE) ='" & MainClass.AllowSingleQuote(nItemCode) & "'" & vbCrLf & " AND IH.PROCESS_DATE >=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.PROCESS_DATE <=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE='P' AND BOOKSUBTYPE='D'"

        SqlStr = SqlStr & vbCrLf & " GROUP BY PRODUCT_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDS, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempDS.EOF = False Then
            mOpeningBal = IIf(IsDBNull(RsTempDS.Fields("STOCK_QTY").Value), 0, RsTempDS.Fields("STOCK_QTY").Value)
            mMsg = mMsg & IIf(mMsg = "", "", vbCrLf) & nItemCode & " : (OP) " & mOpeningBal
            mMsg = mMsg & IIf(mMsg = "", "", vbCrLf) & nItemCode & " : (Min) " & mMinBal
            Do While RsTempDS.EOF = False
                mPlanningQty = mPlanningQty + IIf(IsDBNull(RsTempDS.Fields("PLANNED_QTY").Value), 0, RsTempDS.Fields("PLANNED_QTY").Value)
                mProductCode = IIf(IsDBNull(RsTempDS.Fields("PRODUCT_CODE").Value), "", RsTempDS.Fields("PRODUCT_CODE").Value)
                If IIf(IsDBNull(RsTempDS.Fields("PLANNED_QTY").Value), 0, RsTempDS.Fields("PLANNED_QTY").Value) > 0 Then
                    mMsg = mMsg & IIf(mMsg = "", "", vbCrLf) & mProductCode & " : " & IIf(IsDBNull(RsTempDS.Fields("PLANNED_QTY").Value), 0, RsTempDS.Fields("PLANNED_QTY").Value)
                End If

                RsTempDS.MoveNext()
            Loop

        End If

        mPlanningQty = IIf(mPlanningQty = 0, 0, mPlanningQty - mOpeningBal + mMinBal)


        ''Detail Method..

        '    SqlStr = " SELECT  " & vbCrLf _
        ''            & " LEVEL,TRN.PRODUCT_CODE, TRN.STD_QTY+  GROSS_WT_SCRAP AS STD_QTY, DEPT_CODE" & vbCrLf _
        ''            & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf _
        ''            & " WHERE TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND STATUS='O'"
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " START WITH  TRIM(RM_CODE) || '-' || TRN.COMPANY_CODE='" & MainClass.AllowSingleQuote(nItemCode) & "-" & RsCompany.fields("COMPANY_CODE").value & "'" & vbCrLf _
        ''            & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE) || ' '=TRIM(RM_CODE) || COMPANY_CODE || ' '"
        '
        '
        '     MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '
        '    If RsTemp.EOF = False Then
        '        Do While RsTemp.EOF = False
        '            mProductCode = IIf(IsNull(RsTemp!PRODUCT_CODE), "", RsTemp!PRODUCT_CODE)
        '
        '            If RsTemp!Level = 1 Then
        '                mStdQty = IIf(IsNull(RsTemp!STD_QTY), 0, RsTemp!STD_QTY)
        '            Else
        '                If RsTemp!Level > mPreLevel Then
        '                    mStdQty = mStdQty * IIf(IsNull(RsTemp!STD_QTY), 0, RsTemp!STD_QTY)
        '                Else
        '                    mStdQty = PreLevelStdQty
        '                End If
        '            End If
        '
        '            SqlStr = " SELECT  SUM(PLANNED_QTY) AS PLANNED_QTY" & vbCrLf _
        ''                    & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID" & vbCrLf _
        ''                    & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''                    & " AND IH.AUTO_KEY_DELV = ID.AUTO_KEY_DELV" & vbCrLf _
        ''                    & " AND TRIM(ID.ITEM_CODE) IN (" & vbCrLf _
        ''                    & " SELECT TRIM(REF_ITEM_CODE) FROM INV_ITEM_RELATIONSHIP_DET " & vbCrLf _
        ''                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
        ''                    & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _
        ''                    & " UNION " & vbCrLf _
        ''                    & " SELECT '" & mProductCode & "' FROM DUAL" & vbCrLf _
        ''                    & " )" & vbCrLf _
        ''                    & " AND ID.SERIAL_DATE >='" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''                    & " AND ID.SERIAL_DATE <='" & VB6.Format(mToDate, "DD-MMM-YYYY") & "'"
        '
        '            MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTempDS, adLockReadOnly
        '
        '            If RsTempDS.EOF = False Then
        '                mDSQty = IIf(IsNull(RsTempDS!PLANNED_QTY), 0, RsTempDS!PLANNED_QTY) ''* 0.9  ''90% of Customer Delivery Schedule..
        '                mPlanningQty = mPlanningQty + (mDSQty * mStdQty)
        '                If IIf(IsNull(RsTempDS!PLANNED_QTY), 0, RsTempDS!PLANNED_QTY) > 0 Then
        '                    mMsg = mMsg & IIf(mMsg = "", "", vbCrLf) & mProductCode & " : " & IIf(IsNull(RsTempDS!PLANNED_QTY), 0, RsTempDS!PLANNED_QTY)
        '                End If
        '            End If
        '            mPreLevel = RsTemp!Level
        '            PreLevelStdQty = mStdQty  'IIf(IsNull(RsTemp!STD_QTY), 0, RsTemp!STD_QTY)
        '            RsTemp.MoveNext
        '        Loop
        '    End If
        '
        '    SqlStr = " SELECT  SUM(PLANNED_QTY) AS PLANNED_QTY" & vbCrLf _
        ''            & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID" & vbCrLf _
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND IH.AUTO_KEY_DELV = ID.AUTO_KEY_DELV" & vbCrLf _
        ''            & " AND TRIM(ID.ITEM_CODE) ='" & MainClass.AllowSingleQuote(nItemCode) & "'" & vbCrLf _
        ''            & " AND ID.SERIAL_DATE >='" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''            & " AND ID.SERIAL_DATE <='" & VB6.Format(mToDate, "DD-MMM-YYYY") & "'"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTempDS, adLockReadOnly
        '
        '    If RsTempDS.EOF = False Then
        '        mPlanningQty = mPlanningQty + IIf(IsNull(RsTempDS!PLANNED_QTY), 0, RsTempDS!PLANNED_QTY)
        '        If IIf(IsNull(RsTempDS!PLANNED_QTY), 0, RsTempDS!PLANNED_QTY) > 0 Then
        '            mMsg = mMsg & IIf(mMsg = "", "", vbCrLf) & nItemCode & " : " & IIf(IsNull(RsTempDS!PLANNED_QTY), 0, RsTempDS!PLANNED_QTY)
        '        End If
        '    End If

        mPlanningQty = mPlanningQty / IIf(CDbl(mFactor) = 0, 1, mFactor)

        If mPackStd > 0 Then
            mPlanningQty = mPlanningQty / mPackStd
            mPlanningQty = IIf(Int(mPlanningQty) = mPlanningQty, mPlanningQty, Int(mPlanningQty) + 1) * mPackStd
        End If

        GetPlanningFromCustomerDS = Int(mPlanningQty) ''+ 1

        Exit Function
ErrPart:
        GetPlanningFromCustomerDS = False
    End Function
    Private Sub frmDS_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()
        RsDSMain.Close()
        'RsOpOuts.Close
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked

        Call ShowFormDSDailyDetail(eventArgs.col, eventArgs.row)

    End Sub

    Private Sub ShowFormDSDailyDetail(ByRef pCol As Integer, ByRef pRow As Integer)
        'Dim I As Integer
        'Dim SqlStr As String = ""
        'Dim RsTemp As ADODB.Recordset=Nothing= Nothing
        'Dim pDate As String
        Dim mItemCode As String
        'Dim mItemName As String
        'Dim mQty As String

        With SprdMain
            .Row = pRow

            .Col = ColItemCode
            mItemCode = .Text
        End With
        If mItemCode = "" Then Exit Sub

        If Trim(txtScheduleDate.Text) = "" Then
            MsgInformation("Please Enter Valid Schedule Date")
            txtScheduleDate.Focus()
            Exit Sub
        End If
        'Me.lblDetail.Text = "True"

        ConDSDetail = False

        With FrmDSDailyDetail
            .LblAddMode.Text = CStr(ADDMode)
            .LblModifyMode.Text = CStr(MODIFYMode)
            .lblPoNo.Text = CStr(Val(txtDSNo.Text))
            .lblItemCode.Text = mItemCode
            .lblSuppCode.Text = txtCode.Text
            .LblPODate.Text = VB6.Format(txtScheduleDate.Text, "DD/MM/YYYY")
            .lblMainActiveRow.Text = CStr(pRow)
            .ShowDialog()
        End With

        If ConDSDetail = True Then        ''If Me.lblDetail.Text = "True" Then
            With SprdMain
                .Row = pRow
                .Col = ColWeek1Qty
                .Text = CStr(Val(FrmDSDailyDetail.lblWeek1.Text))
                .Col = ColWeek2Qty
                .Text = CStr(Val(FrmDSDailyDetail.lblWeek2.Text))
                .Col = ColWeek3Qty
                .Text = CStr(Val(FrmDSDailyDetail.lblWeek3.Text))
                .Col = ColWeek4Qty
                .Text = CStr(Val(FrmDSDailyDetail.lblWeek4.Text))
                .Col = ColWeek5Qty
                .Text = CStr(Val(FrmDSDailyDetail.lblWeek5.Text))

            End With
        End If
        FrmDSDailyDetail.Hide()
        FrmDSDailyDetail.Close()
        Call CalcTots()
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function CheckDuplicateItem(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

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

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False
        End With
    End Sub


    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""

        Dim nItemCode As String
        Dim mPlanningQty As Double
        Dim mMsg As String = ""


        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)

        If eventArgs.row > 0 And eventArgs.col = ColPlanningQty And SprdMain.Enabled = True Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColItemCode
            nItemCode = Trim(SprdMain.Text)
            mPlanningQty = GetPlanningFromCustomerDS(nItemCode, Trim(txtScheduleDate.Text), mMsg)
            If Trim(mMsg) <> "" Then
                MsgInformation(mMsg)
            End If
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                SqlStr = GetSearchItem("Y")
                ''If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "ITEM_CODE") = True Then
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName)
                    .Col = ColItemName
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemName And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemName
                SqlStr = GetSearchItem("N")
                ''If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "ITEM_CODE") = True Then
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemName
                    .Text = Trim(AcName)
                    .Col = ColItemCode
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemName)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))


        If eventArgs.keyCode = System.Windows.Forms.Keys.F3 And mSearchKey <> "" Then
            If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColShortQty)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If

        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xICode As String

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub


                If GetValidItem(xICode) = True Then
                    If CheckDuplicateItem(xICode) = False Then
                        If FillGridRow(xICode) = False Then Exit Sub
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        FormatSprdMain(eventArgs.row)
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                End If

                '        Case ColTotQty
                '            If CheckItemRate() = True Then
                '                MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight
                '                FormatSprdMain -1
                '            End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckQty() As Boolean

        On Error GoTo ERR1
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColTotQty
            If Val(.Text) > 0 Then
                CheckQty = True
            Else
                MsgInformation("Please Enter the Qty.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColTotQty)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FillGridRow(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mPlanningQty As Double

        If mItemCode = "" Then Exit Function

        SqlStr = ""
        SqlStr = " Select ITEM_CODE,ITEM_SHORT_DESC,PURCHASE_UOM" & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & Trim(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc

                SprdMain.Col = ColItemName
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value)

                SprdMain.Col = ColItemUOM
                SprdMain.Text = IIf(IsDBNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)

                SprdMain.Col = ColPlanningQty
                mPlanningQty = GetPlanningFromCustomerDS(mItemCode, Trim(txtScheduleDate.Text))
                SprdMain.Text = VB6.Format(mPlanningQty, "0")

            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function

    Private Sub SprdMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdMain.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mDSNo As String

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        mDSNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(0))

        txtDSNo.Text = CStr(Val(mDSNo))

        txtDSNo_Validating(txtDSNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())

    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
        'SprdView.Col = 1
        'SprdView.Row = SprdView.ActiveRow
        'txtDSNo.Text = SprdView.Text

        'txtDSNo_Validating(txtDSNo, New System.ComponentModel.CancelEventArgs(False))
        'CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent)
        'If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    End Sub

    Private Sub txtDSAmendDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSAmendDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDSAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSAmendNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDSAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDSAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.DoubleClick
        Call SearchCode()
    End Sub
    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCode()
    End Sub
    Private Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If Trim(txtCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_Name", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            txtSupplierName.Text = MasterNo
            txtCode.Enabled = False
        Else
            MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPOAmendDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPOAmendDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPOAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPOAmendNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPODate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtScheduleDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtScheduleDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
    End Sub

    Private Sub txtScheduleDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtScheduleDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Not IsDate(txtScheduleDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
            GoTo EventExitSub
        End If

        If VB6.Format(txtScheduleDate.Text, "YYYYMM") < VB6.Format(txtDSDate.Text, "YYYYMM") Then
            MsgInformation("Schedule Date Cann't be Less Than Delivery Schedule Date")
            Cancel = True
        End If


        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtSearchItem_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSearchItem.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        mSearchStartRow = 1
    End Sub

    Private Sub txtSearchItem_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchItem.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSearchItem.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSupplierName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSupplierName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtSupplierName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplierName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplierName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplierName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplierName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtSupplierName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSupplierName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(txtSupplierName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
            txtCode.Text = xAcctCode
            txtCode.Enabled = False
        Else
            MsgBox("Invalid Supplier Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPONO_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPONo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xAcctCode As String

        If Trim(txtPONo.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "Supp_Cust_Name", "Supp_Cust_Code", "Fin_Supp_Cust_MSt", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            MsgInformation("InValid Supplier Name.")
            GoTo EventExitSub
        End If


        SqlStr = " SELECT PUR_ORD_DATE , AMEND_NO, AMEND_DATE, AMEND_WEF_DATE " & vbCrLf & " FROM PUR_PURCHASE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If RsCompany.Fields("FYEAR").Value < ConOPENPO_CONTINOUS_YEAR Then
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        Else
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & ""
        End If

        If CDate(txtScheduleDate.Text) < CDate(PubGSTApplicableDate) Then
            SqlStr = SqlStr & vbCrLf & " AND ISGSTENABLE_PO='N'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND ISGSTENABLE_PO='Y'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_PO=" & Val(txtPONo.Text) & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & xAcctCode & "'" & vbCrLf _
            & " AND PUR_TYPE||ORDER_TYPE IN ('PO','JC') AND PO_STATUS='Y' AND PO_CLOSED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtPODate.Text = IIf(IsDBNull(RsTemp.Fields("PUR_ORD_DATE").Value), "", RsTemp.Fields("PUR_ORD_DATE").Value)
            txtPOAmendNo.Text = IIf(IsDBNull(RsTemp.Fields("AMEND_NO").Value), "", RsTemp.Fields("AMEND_NO").Value)
            txtPOAmendDate.Text = IIf(IsDBNull(RsTemp.Fields("AMEND_DATE").Value), "", RsTemp.Fields("AMEND_DATE").Value)
            txtWEF.Text = IIf(IsDBNull(RsTemp.Fields("AMEND_WEF_DATE").Value), "", RsTemp.Fields("AMEND_WEF_DATE").Value)
        Else
            MsgBox("Invalid PO NO.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mAccountName As String = ""

        Clear1()
        If Not RsDSMain.EOF Then

            lblMkey.Text = IIf(IsDBNull(RsDSMain.Fields("AUTO_KEY_DELV").Value), "", RsDSMain.Fields("AUTO_KEY_DELV").Value)
            txtDSNo.Text = IIf(IsDBNull(RsDSMain.Fields("AUTO_KEY_DELV").Value), "", RsDSMain.Fields("AUTO_KEY_DELV").Value)
            txtDSDate.Text = VB6.Format(IIf(IsDBNull(RsDSMain.Fields("DELV_SCHLD_DATE").Value), "", RsDSMain.Fields("DELV_SCHLD_DATE").Value), "DD/MM/YYYY")
            txtDSAmendNo.Text = IIf(IsDBNull(RsDSMain.Fields("DELV_AMEND_NO").Value), 0, RsDSMain.Fields("DELV_AMEND_NO").Value)
            txtDSAmendDate.Text = VB6.Format(IIf(IsDBNull(RsDSMain.Fields("DELV_AMEND_DATE").Value), "", RsDSMain.Fields("DELV_AMEND_DATE").Value), "DD/MM/YYYY")

            txtPONo.Text = IIf(IsDBNull(RsDSMain.Fields("AUTO_KEY_PO").Value), "", RsDSMain.Fields("AUTO_KEY_PO").Value)
            txtPODate.Text = VB6.Format(IIf(IsDBNull(RsDSMain.Fields("PO_DATE").Value), "", RsDSMain.Fields("PO_DATE").Value), "DD/MM/YYYY")
            txtPOAmendNo.Text = IIf(IsDBNull(RsDSMain.Fields("PO_AMEND_NO").Value), "", RsDSMain.Fields("PO_AMEND_NO").Value)
            txtPOAmendDate.Text = VB6.Format(IIf(IsDBNull(RsDSMain.Fields("AMEND_DATE").Value), "", RsDSMain.Fields("AMEND_DATE").Value), "DD/MM/YYYY")


            cboStatus.SelectedIndex = IIf(RsDSMain.Fields("SCHLD_STATUS").Value = "N", 0, 1)
            txtRemarks.Text = IIf(IsDBNull(RsDSMain.Fields("REMARKS").Value), "", RsDSMain.Fields("REMARKS").Value)

            txtWEF.Text = VB6.Format(IIf(IsDBNull(RsDSMain.Fields("AMEND_WEF_DATE").Value), "", RsDSMain.Fields("AMEND_WEF_DATE").Value), "DD/MM/YYYY")
            chkStatus.CheckState = IIf(RsDSMain.Fields("POST_FLAG").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            cmdAmendSchd.Enabled = IIf(RsDSMain.Fields("POST_FLAG").Value = "Y", True, False)

            If RsDSMain.Fields("SCHLD_STATUS").Value = "Y" Then
                cmdAmendSchd.Enabled = False
            End If

            lblAddUser.Text = IIf(IsDBNull(RsDSMain.Fields("ADDUSER").Value), "", RsDSMain.Fields("ADDUSER").Value)
            lblAddDate.Text = VB6.Format(IIf(IsDBNull(RsDSMain.Fields("ADDDATE").Value), "", RsDSMain.Fields("ADDDATE").Value), "DD/MM/YYYY")
            lblModUser.Text = IIf(IsDBNull(RsDSMain.Fields("MODUSER").Value), "", RsDSMain.Fields("MODUSER").Value)
            lblModDate.Text = VB6.Format(IIf(IsDBNull(RsDSMain.Fields("MODDATE").Value), "", RsDSMain.Fields("MODDATE").Value), "DD/MM/YYYY")

            txtScheduleDate.Text = VB6.Format(IIf(IsDBNull(RsDSMain.Fields("SCHLD_DATE").Value), "", RsDSMain.Fields("SCHLD_DATE").Value), "DD/MM/YYYY")
            txtScheduleDate.Enabled = IIf(RsDSMain.Fields("POST_FLAG").Value = "Y", False, True)

            cmdRefresh.Enabled = cmdAmendSchd.Enabled

            mAccountCode = IIf(IsDBNull(RsDSMain.Fields("SUPP_CUST_CODE").Value), -1, RsDSMain.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = MasterNo
            End If
            txtSupplierName.Text = mAccountName
            txtCode.Text = Trim(IIf(IsDBNull(RsDSMain.Fields("SUPP_CUST_CODE").Value), "", RsDSMain.Fields("SUPP_CUST_CODE").Value))
            txtCode.Enabled = False
            txtSupplierName.Enabled = False
            cmdsearch.Enabled = False
            mAmendSchd = False
            Call ShowDetail1()
            Call ShowDSDailyDetail()
        End If
        Call CalcTots()
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColPlanningQty)
        MainClass.ButtonStatus(Me, XRIGHT, RsDSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub ShowDSDailyDetail()

        On Error GoTo ShowSerialNoErr
        Dim RsSRLNo As ADODB.Recordset
        Dim SqlStr As String = ""

        Call DelTemp_DailyDetail()

        SqlStr = ""
        SqlStr = "INSERT INTO TEMP_PUR_DAILY_SCHLD_DET ( " & vbCrLf & " UserId, AUTO_KEY_DELV, ITEM_CODE, " & vbCrLf & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY," & vbCrLf & " DELV_CNT, SUPP_CUST_CODE,SCHLD_DATE)" & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " AUTO_KEY_DELV, ITEM_CODE," & vbCrLf & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf & " DELV_CNT , SUPP_CUST_CODE, SCHLD_DATE " & vbCrLf & " FROM PUR_DAILY_SCHLD_DET " & vbCrLf & " WHERE AUTO_KEY_DELV=" & Val(lblMkey.Text) & " " & vbCrLf & " ORDER BY SERIAL_NO, SERIAL_DATE"

        PubDBCn.Execute(SqlStr)

        Exit Sub
ShowSerialNoErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub DelTemp_DailyDetail(Optional ByRef mRefNo As String = "", Optional ByRef mItemCode As String = "")

        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = "DELETE FROM TEMP_PUR_DAILY_SCHLD_DET " & "WHERE UserId='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        If mRefNo <> "" And mItemCode <> "" Then
            SqlStr = SqlStr & "AND AUTO_KEY_DELV=" & Val(mRefNo) & "' " & "AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "
        End If
        PubDBCn.Execute(SqlStr)
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPlanningQty As Double

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM PUR_DELV_SCHLD_DET " & vbCrLf & " Where " & vbCrLf & " AUTO_KEY_DELV=" & Val(lblMkey.Text) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsDSDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1
            I = 1
            '        .MoveFirst

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = mItemCode

                SprdMain.Col = ColItemName
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColItemUOM
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value))

                SprdMain.Col = ColPlanningQty
                mPlanningQty = GetPlanningFromCustomerDS(mItemCode, Trim(txtScheduleDate.Text))
                SprdMain.Text = VB6.Format(mPlanningQty, "0")

                SprdMain.Col = ColWeek1Qty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("WEEK1_QTY").Value), 0, .Fields("WEEK1_QTY").Value)))

                SprdMain.Col = ColWeek2Qty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("WEEK2_QTY").Value), 0, .Fields("WEEK2_QTY").Value)))

                SprdMain.Col = ColWeek3Qty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("WEEK3_QTY").Value), 0, .Fields("WEEK3_QTY").Value)))

                SprdMain.Col = ColWeek4Qty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("WEEK4_QTY").Value), 0, .Fields("WEEK4_QTY").Value)))

                SprdMain.Col = ColWeek5Qty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("WEEK5_QTY").Value), 0, .Fields("WEEK5_QTY").Value)))

                SprdMain.Col = ColTotQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("TOTAL_QTY").Value), 0, .Fields("TOTAL_QTY").Value)))

                SprdMain.Col = ColRecdQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("REC_QTY").Value), 0, .Fields("REC_QTY").Value)))

                SprdMain.Col = ColShortQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SHORT_QTY").Value), 0, .Fields("SHORT_QTY").Value)))

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub txtDSDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDSNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDSNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDSNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtDSNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDSNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mDSNo As Double
        Dim SqlStr As String = ""

        If Trim(txtDSNo.Text) = "" Then GoTo EventExitSub

        If Len(txtDSNo.Text) < 6 Then
            txtDSNo.Text = Val(txtDSNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If


        mDSNo = Val(txtDSNo.Text)

        If MODIFYMode = True And RsDSMain.BOF = False Then xMkey = RsDSMain.Fields("AUTO_KEY_DELV").Value

        SqlStr = "SELECT * FROM PUR_DELV_SCHLD_HDR " & " WHERE AUTO_KEY_DELV='" & MainClass.AllowSingleQuote(UCase(CStr(mDSNo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " ''& vbCrLf |            & " AND SUBSTR(AUTO_KEY_DELV,LENGTH(AUTO_KEY_DELV)-5,4)=" & RsCompany.fields("FYEAR").value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsDSMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such PO No. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PUR_DELV_SCHLD_HDR WHERE AUTO_KEY_DELV=" & Val(xMkey) & "" ''& vbCrLf |                & " AND SUBSTR(AUTO_KEY_DELV,LENGTH(AUTO_KEY_DELV)-5,4)=" & RsCompany.fields("FYEAR").value & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
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


    Private Sub TxtWef_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub



    Private Function GetSearchItem(ByRef mByCode As String) As String
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String

        xSuppCode = IIf(Trim(txtCode.Text) = "", "-1", Trim(txtCode.Text))

        If mByCode = "Y" Then
            mSqlStr = "SELECT DISTINCT ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC "
        Else
            mSqlStr = "SELECT DISTINCT INVMST.ITEM_SHORT_DESC,ID.ITEM_CODE "
        End If

        '    mSqlStr = mSqlStr & vbCrLf _
        ''        & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf _
        ''        & " WHERE A.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''        & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
        ''        & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf _
        ''        & " AND B.SUPP_CUST_CODE='" & xSuppCode & "'"

        mSqlStr = mSqlStr & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & xSuppCode & "'" & vbCrLf & " AND IH.AUTO_KEY_PO='" & txtPONo.Text & "'" & vbCrLf & " AND IH.PUR_TYPE||IH.ORDER_TYPE IN ('PO','JC') " '& vbCrLf |        & " AND AMEND_NO=" & Val(txtPOAmendNo) & ""

        If CDate(txtScheduleDate.Text) < CDate(PubGSTApplicableDate) Then
            mSqlStr = mSqlStr & vbCrLf & " AND ISGSTENABLE_PO='N'"
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND ISGSTENABLE_PO='Y'"
        End If

        GetSearchItem = mSqlStr
        Exit Function
ErrPart:
        GetSearchItem = ""

    End Function

    Private Function GetValidItem(ByRef pItemCode As String) As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim Response As String

        '    If ADDMode = False Then
        '        GetValidItem = True
        '        Exit Function
        '    End If

        xSuppCode = IIf(Trim(txtCode.Text) = "", "-1", Trim(txtCode.Text))

        mSqlStr = "SELECT ID.ITEM_CODE,PO_WEF_DATE,PO_STATUS,PO_CLOSED " & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & xSuppCode & "'" & vbCrLf & " AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.AUTO_KEY_PO='" & Val(txtPONo.Text) & "'" & vbCrLf & " AND IH.PUR_TYPE||IH.ORDER_TYPE IN ('PO','JC') AND IH.PO_STATUS='Y' AND IH.PO_CLOSED='N' AND INVMST.ITEM_STATUS='A'" ''& vbCrLf |        & " AND AMEND_NO=" & Val(txtPOAmendNo) & ""

        If CDate(txtScheduleDate.Text) < CDate(PubGSTApplicableDate) Then
            mSqlStr = mSqlStr & vbCrLf & " AND ISGSTENABLE_PO='N'"
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND ISGSTENABLE_PO='Y'"
        End If

        If ADDMode = True Then
            mSqlStr = mSqlStr & vbCrLf & " AND AMEND_NO=" & Val(txtPOAmendNo.Text) & ""
        End If

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If RsTemp.Fields("PO_STATUS").Value = "Y" Then
                GetValidItem = True
                Exit Function
            Else
                MsgInformation("Purchase Order not posted for this Item")
                GetValidItem = False
                Exit Function
            End If
        Else
            MsgInformation("Invalid Item For this PO.")
            GetValidItem = False
            Exit Function
        End If

        Exit Function
ErrPart:
        GetValidItem = False
    End Function
    Private Function SelectQryForDS(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC,"

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, " & vbCrLf & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE"

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM PUR_DELV_SCHLD_HDR IH, PUR_DELV_SCHLD_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_DELV=" & Val(txtDSNo.Text) & ""

        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForDS = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        'Dim Printer As New Printer
        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mCompanyAdd1 As String = ""
        Dim mCompanyCity As String = ""
        Dim mCompanyPhone As String = ""
        Dim meMail As String = ""
        Dim mCompanyPAN As String = ""

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, pmyMenu)
        '    mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(lblNetAmount.text) = 0, 0, lblNetAmount.text)))
        '
        '    MainClass.AssignCRptFormulas Report1, "AmountInWord=""" & mAmountInword & """"
        '    MainClass.AssignCRptFormulas Report1, "NetAmount=""" & lblNetAmount.text & """"

        If mRptFileName = "DS.rpt" Then


            mCompanyPhone = IIf(IsDBNull(RsCompany.Fields("COMPANY_PHONE").Value), "", "Phone : " & RsCompany.Fields("COMPANY_PHONE").Value)
            mCompanyPhone = mCompanyPhone & IIf(IsDBNull(RsCompany.Fields("COMPANY_FAXNO").Value), "", " Fax : " & RsCompany.Fields("COMPANY_FAXNO").Value)

            meMail = IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", "EMail : " & RsCompany.Fields("COMPANY_MAILID").Value)
            meMail = meMail & IIf(IsDBNull(RsCompany.Fields("WEBSITE").Value), "", "  Web : " & RsCompany.Fields("WEBSITE").Value)

            mCompanyPAN = IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)

            MainClass.AssignCRptFormulas(Report1, "CompanyPAN=""" & mCompanyPAN & """")
            MainClass.AssignCRptFormulas(Report1, "CompanyeMail=""" & meMail & """")
            MainClass.AssignCRptFormulas(Report1, "CompanyPhone=""" & mCompanyPhone & """")
        End If

        Report1.ReportFileName = PubReportFolderPath & mRptFileName
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
        '            Exit For
        '        End If
        '    Next prt
        'End If

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub ReportOnDS(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String
        Dim Response As String
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""
        mSubTitle = ""
        Call MainClass.ClearCRptFormulas(Report1)

        Call SelectQryForDS(SqlStr)
        mTitle = "Delivery Schedule"
        mRptFileName = "DS.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Response = MsgQuestion("Do You Want to Print Detail Delivery Schedule?")

        If Response = CStr(MsgBoxResult.Yes) Then
            Call MainClass.ClearCRptFormulas(Report1)

            Call SelectQryForDailyDS(SqlStr, "")
            mTitle = "Shortage Follow-up register for the month of " & VB6.Format(txtScheduleDate.Text, "MMMM , YYYY")
            mRptFileName = "DSDetail.rpt"

            Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
        End If

        Response = MsgQuestion("Do You Want to Print Day wise Delivery Schedule?")

        Dim mDay As String
        If Response = CStr(MsgBoxResult.Yes) Then
            mDay = InputBox("Date :", "Date", VB6.Format(Now(), "DD/MM/YYYY"))
            If IsDate(mDay) = False Then
                MsgInformation("Invalid Date")
                Exit Sub
            End If
            Call MainClass.ClearCRptFormulas(Report1)

            Call SelectQryForDailyDS(SqlStr, mDay)
            mTitle = "Shortage Follow-up register for the Day of " & VB6.Format(mDay, "DD-MMM-YYYY")
            mRptFileName = "DSDayWise.rpt"

            Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryForDailyDS(ByRef mSqlStr As String, ByRef mDay As String) As String

        mSqlStr = " SELECT " & vbCrLf _
            & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME"

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf _
            & " FROM PUR_DELV_SCHLD_HDR IH, PUR_DAILY_SCHLD_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_DELV=" & Val(txtDSNo.Text) & ""

        If IsDate(mDay) = True Then
            mSqlStr = mSqlStr & vbCrLf & " AND SERIAL_DATE=TO_DATE('" & VB6.Format(mDay, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If
        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_DATE"

        SelectQryForDailyDS = mSqlStr

    End Function
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnDS(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnDS(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


    Private Function DSExsistInCurrSchdMon(ByRef pSuppCustCode As String, ByRef pPONO As Double, ByRef pSchdDate As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xDSNo As Double
        Dim pDSNo As Double

        pDSNo = Val(txtDSNo.Text)

        SqlStr = "SELECT AUTO_KEY_DELV " & vbCrLf _
            & " FROM PUR_DELV_SCHLD_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & pSuppCustCode & "'" & vbCrLf _
            & " AND AUTO_KEY_PO=" & pPONO & "" & vbCrLf _
            & " AND TO_CHAR(SCHLD_DATE,'MM-YYYY')=TO_CHAR('" & VB6.Format(pSchdDate, "MM-YYYY") & "')"

        SqlStr = SqlStr & vbCrLf & "AND AUTO_KEY_DELV<>" & pDSNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            xDSNo = RsTemp.Fields("AUTO_KEY_DELV").Value
            MsgInformation("Delivery Schedule (" & xDSNo & ") Already Made in this Month for Such Supplier.")
            DSExsistInCurrSchdMon = True
        Else
            DSExsistInCurrSchdMon = False
        End If

        Exit Function
ErrPart:
        DSExsistInCurrSchdMon = True
    End Function
    Private Sub CalcTots()
        On Error GoTo ERR1
        ''Dim RsMisc As ADODB.Recordset=Nothing
        Dim mGrossQty As Double

        Dim I As Integer
        Dim j As Integer


        mGrossQty = 0

        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                mGrossQty = 0

                .Col = ColWeek1Qty
                mGrossQty = mGrossQty + Val(.Text)

                .Col = ColWeek2Qty
                mGrossQty = mGrossQty + Val(.Text)

                .Col = ColWeek3Qty
                mGrossQty = mGrossQty + Val(.Text)

                .Col = ColWeek4Qty
                mGrossQty = mGrossQty + Val(.Text)

                .Col = ColWeek5Qty
                mGrossQty = mGrossQty + Val(.Text)

                .Col = ColTotQty
                .Text = CStr(Val(CStr(mGrossQty)))

            Next I
        End With

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub SprdMain_KeyPressEvent(sender As Object, e As _DSpreadEvents_KeyPressEvent) Handles SprdMain.KeyPressEvent
        'Dim KeyAscii As Short = Asc(e.keyAscii)

        'KeyAscii = MainClass.SetNumericField(KeyAscii)
        'EventArgs.KeyChar = Chr(KeyAscii)
        'If KeyAscii = 67 Then
        '    EventArgs.Handled = True
        'End If

        If e.keyAscii = 6 Then
            SprdMain.Row = 1
            SprdMain.Row2 = SprdMain.MaxRows
            SprdMain.Col = 1
            SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
            SprdMain.BlockMode = True
            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            SprdMain.BlockMode = False

            mSearchKey = ""
            cntSearchRow = 1
            cntSearchCol = 1
            mSearchKey = InputBox("Search :", "Search", mSearchKey)
            If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColShortQty)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub
End Class
