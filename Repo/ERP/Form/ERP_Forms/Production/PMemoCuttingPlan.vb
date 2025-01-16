Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports System.ComponentModel

Friend Class FrmPMemoCuttingPlan
    Inherits System.Windows.Forms.Form
    Dim RsPMemoMain As ADODB.Recordset ''Recordset	
    Dim RsPMemoDetail As ADODB.Recordset ''Recordset	
    Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColUom As Short = 3
    Private Const ColThickness As Short = 4
    Private Const ColWidth As Short = 5
    Private Const ColLength As Short = 6
    Private Const ColWt_Per_No As Short = 7
    Private Const ColProdQty As Short = 8
    Private Const ColScrapQty As Short = 9
    Private Const ColOkQty As Short = 10
    Private Const ColTotalWeight As Short = 11
    Private Const ColRemarks As Short = 12

    Dim CurrFormHeight As Integer
    Dim CurrFormWidth As Integer

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboShiftcd_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShiftcd.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboShiftcd_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShiftcd.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboShiftcd_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboShiftcd.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(cboShiftcd.Text) = "C" Then
            If Trim(txtPMemoDate.Text) <> "" Then
                txtProdDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"))))
                txtProdDate.Text = VB6.Format(txtProdDate.Text, "DD/MM/YYYY")
            End If
        Else
            txtProdDate.Text = VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cboType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtPMemoNo.Enabled = False
            cmdSearch.Enabled = False
        Else
            cmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillCbo()

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " ORDER BY DIV_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = -1

        cboShiftcd.Items.Clear()
        cboShiftcd.Items.Add(("A"))
        cboShiftcd.Items.Add(("B"))
        cboShiftcd.Items.Add(("C"))

        cboShiftcd.SelectedIndex = 0

        cboType.Items.Clear()
        cboType.Items.Add(("Production"))
        cboType.Items.Add(("Jobwork"))

        cboType.SelectedIndex = 0

        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart
        Dim mItemCode As String

        If ValidateBranchLocking((txtPMemoDate.Text)) = True Then
            Exit Sub
        End If
        If Trim(txtPMemoNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsPMemoMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PRD_CUTTINGPLAN_HDR ", (txtPMemoNo.Text), RsPMemoMain, "AUTO_KEY_REF") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_CUTTINGPLAN_HDR", "AUTO_KEY_REF", (lblMKey.Text)) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM PRD_CUTTINGPLAN_DET WHERE AUTO_KEY_REF=" & Val(lblMKey.Text) & "")
                PubDBCn.Execute("DELETE FROM PRD_CUTTINGPLAN_HDR  WHERE AUTO_KEY_REF=" & Val(lblMKey.Text) & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'")
                PubDBCn.CommitTrans()
                RsPMemoMain.Requery()
                RsPMemoDetail.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsPMemoMain.Requery()
        RsPMemoDetail.Requery()
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr
        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtPMemoNo.Enabled = False
            cmdSearch.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub



    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtPMemoNo_Validating(txtPMemoNo, New System.ComponentModel.CancelEventArgs(False))
            If cmdAdd.Enabled = True Then cmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume	
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'"

        If MainClass.SearchGridMaster(txtPMemoNo.Text, "PRD_CUTTINGPLAN_HDR ", "AUTO_KEY_REF", "REF_DATE", , , SqlStr) = True Then
            txtPMemoNo.Text = AcName
            If txtPMemoNo.Enabled = True Then txtPMemoNo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchDept.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            lblDept.Text = AcName
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub cmdSearchEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEmp.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtEmp.Text = AcName1
            lblEmp.Text = AcName
            If txtEmp.Enabled = True Then txtEmp.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub



    Private Sub txtCTLWt_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCTLWt.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCTLWt_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCTLWt.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtLength_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLength.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLength_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLength.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtLength_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLength.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mWeightPerStrip As Double
        mWeightPerStrip = GetWeightPerStrip(Val(txtThickness.Text), Val(txtLength.Text), Val(txtWidth.Text), Val(lblDensity.Text))

        txtNetRMWt.Text = VB6.Format(mWeightPerStrip * Val(txtRMQty.Text), "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNetRMWt_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNetRMWt.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNetRMWt_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNetRMWt.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtNetScrapWt_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNetScrapWt.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNetScrapWt_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNetScrapWt.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtRMCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRMCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRMCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRMCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRMCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRMCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRMCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchRMCode_Click(cmdSearchRMCode, New System.EventArgs())
    End Sub

    Private Sub txtRMCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRMCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim mAvailable As Double
        Dim mItemUOM As String

        Dim mDivisionCode As Double
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        If Trim(txtRMCode.Text) = "" Then lblRMCode.Text = "" : GoTo EventExitSub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            txtRMCode.Text = ""
            lblRMCode.Text = ""
            GoTo EventExitSub
        End If

        If txtDept.Text = "" Then
            If txtDept.Enabled = True Then txtDept.Focus()
            MsgInformation("Please Enter Department.")
            txtRMCode.Text = ""
            lblRMCode.Text = ""
            '        Cancel = True	
            GoTo EventExitSub
        End If

        If txtPMemoDate.Text = "" Then
            If txtPMemoDate.Enabled = True Then txtPMemoDate.Focus()
            MsgInformation("Please Enter Date.")
            txtRMCode.Text = ""
            lblRMCode.Text = ""
            GoTo EventExitSub
        End If

        SqlStr = "SELECT ITEM_CODE, ITEM_SHORT_DESC, ISSUE_UOM, MAT_LEN, MAT_WIDTH, MAT_THICHNESS, MAT_DENSITY  " & vbCrLf _
            & " FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(txtRMCode.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            lblRMCode.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)
            lblRMUOM.Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
            mItemUOM = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

            txtThickness.Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("MAT_THICHNESS").Value), 0, RsTemp.Fields("MAT_THICHNESS").Value)))
            txtWidth.Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("MAT_WIDTH").Value), 0, RsTemp.Fields("MAT_WIDTH").Value)))
            txtLength.Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("MAT_LEN").Value), 0, RsTemp.Fields("MAT_LEN").Value)))
            lblDensity.Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("MAT_DENSITY").Value), 0, RsTemp.Fields("MAT_DENSITY").Value)))
        Else
            MsgInformation("Invalid RM Code")
            Cancel = True
        End If

        '    If cboDivision.Text = "" Then	
        ''        If cboDivision.Enabled = True Then cboDivision.SetFocus	
        ''        MsgInformation "Please Select Division."	
        '        Exit Sub	
        '    End If	

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        mAvailable = GetBalanceStockQty(Trim(txtRMCode.Text), (txtPMemoDate.Text), mItemUOM, (txtDept.Text), "ST", "", ConPH, mDivisionCode, ConStockRefType_PMEMODEPT, Val(txtPMemoNo.Text))
        txtStockQty.Text = VB6.Format(mAvailable, "0.00")

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdSearchRMCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchRMCode.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = " SELECT DISTINCT INVMST.ITEM_SHORT_DESC, IH.RM_CODE " & vbCrLf _
            & " FROM PRD_NEWBOM_DET IH, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.RM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.DEPT_CODE='" & Trim(txtDept.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf & " SELECT DISTINCT INVMST.ITEM_SHORT_DESC, IH.ALTER_RM_CODE " & vbCrLf _
            & " FROM PRD_BOM_ALTER_DET IH, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.ALTER_RM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.DEPT_CODE='" & Trim(txtDept.Text) & "'"

        'If MainClass.SearchGridMasterBySQL2(txtRMCode.Text, SqlStr) = True Then
        If MainClass.SearchGridMaster(txtRMCode.Text, "vw_BOMRMSearch", "ITEM_SHORT_DESC", "RM_CODE", "CUSTOMER_PART_NO", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And DEPT_CODE='" & Trim(txtDept.Text) & "'") = True Then
            txtRMCode.Text = AcName1
            lblRMCode.Text = AcName
            If txtRMCode.Enabled = True Then txtRMCode.Focus()
        End If

        '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""	
        '    If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then	
        '        txtRMCode.Text = AcName1	
        '        lblRMCode.text = AcName	
        '        If txtRMCode.Enabled = True Then txtRMCode.SetFocus	
        '    End If	
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtBlockCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBlockCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBlockCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBlockCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBlockCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBlockCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBlockCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdBlockSearch_Click(cmdBlockSearch, New System.EventArgs())
    End Sub
    Private Sub cmdBlockSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBlockSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""


        SqlStr = " SELECT DISTINCT ITEM_SHORT_DESC,ITEM_CODE,ISSUE_UOM " & vbCrLf _
            & " FROM INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " ORDER BY ITEM_SHORT_DESC"

        If MainClass.SearchGridMasterBySQL2(txtBlockCode.Text, SqlStr) = True Then
            txtBlockCode.Text = AcName1
            lblBlockDesc.Text = AcName
            lblBlockUOM.Text = AcName2
            If txtBlockCode.Enabled = True Then txtBlockCode.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtBlockCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBlockCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim mAvailable As Double
        Dim mItemUOM As String


        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        If Trim(txtBlockCode.Text) = "" Then lblBlockDesc.Text = "" : GoTo EventExitSub


        SqlStr = "SELECT ITEM_CODE, ITEM_SHORT_DESC, ISSUE_UOM, MAT_LEN, MAT_WIDTH, MAT_THICHNESS, MAT_DENSITY  " & vbCrLf _
            & " FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(txtBlockCode.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            lblBlockDesc.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)
            lblBlockUOM.Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

        Else
            lblBlockDesc.Text = ""
            lblBlockUOM.Text = ""
            MsgInformation("Invalid Block Code")
            Cancel = True
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub FrmPMemoCuttingPlan_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xIName As String
        Dim SqlStr As String = ""
        Dim pOPRCode As String
        Dim mProductCode As String
        Dim pOPRDesc As String
        Dim RsTemp As ADODB.Recordset = Nothing

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode

                SqlStr = " Select PRODUCT_CODE, ITEM_SHORT_DESC, NVL(CUSTOMER_PART_NO,' ') As CUSTOMER_PART_NO" & vbCrLf _
                    & " FROM vw_BOMSearch" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " And DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf _
                    & " AND RM_CODE='" & Trim(txtRMCode.Text) & "'"


                'SqlStr = " Select DISTINCT IH.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC, NVL(INVMST.CUSTOMER_PART_NO,' ') As CUSTOMER_PART_NO" & vbCrLf _
                '    & " FROM PRD_NEWBOM_DET IH, INV_ITEM_MST INVMST" & vbCrLf _
                '    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                '    & " And IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                '    & " And IH.PRODUCT_CODE=INVMST.ITEM_CODE" & vbCrLf _
                '    & " And IH.DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf _
                '    & " AND IH.RM_CODE='" & Trim(txtRMCode.Text) & "'"

                'SqlStr = SqlStr & vbCrLf & " UNION ALL"

                'SqlStr = SqlStr & vbCrLf _
                '    & " Select DISTINCT IH.PRODUCT_CODE PRODUCT_CODE, INVMST.ITEM_SHORT_DESC, NVL(INVMST.CUSTOMER_PART_NO,' ') As CUSTOMER_PART_NO" & vbCrLf _
                '    & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                '    & " WHERE IH.MKEY=ID.MKEY AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                '    & " And IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                '    & " And IH.PRODUCT_CODE=INVMST.ITEM_CODE" & vbCrLf _
                '    & " And ID.DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf _
                '    & " AND ID.ALTER_RM_CODE='" & Trim(txtRMCode.Text) & "'"

                'If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then

                If MainClass.SearchGridMaster(.Text, "vw_BOMSearch", "PRODUCT_CODE", "ITEM_SHORT_DESC", "CUSTOMER_PART_NO", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And DEPT_CODE='" & Trim(txtDept.Text) & "' AND RM_CODE='" & Trim(txtRMCode.Text) & "'") = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = Trim(AcName)

                    .Col = ColItemDesc
                    .Text = Trim(AcName1)
                    'Call SprdMain_LeaveCell(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False)
                    Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
                End If

            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemDesc

                SqlStr = " Select ITEM_SHORT_DESC, PRODUCT_CODE, NVL(CUSTOMER_PART_NO,' ') As CUSTOMER_PART_NO" & vbCrLf _
                    & " FROM vw_BOMSearch" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " And DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf _
                    & " AND RM_CODE='" & Trim(txtRMCode.Text) & "'"


                'SqlStr = " SELECT DISTINCT INVMST.ITEM_SHORT_DESC, IH.PRODUCT_CODE, NVL(INVMST.CUSTOMER_PART_NO,' ') As CUSTOMER_PART_NO" & vbCrLf _
                '    & " FROM PRD_NEWBOM_DET IH, INV_ITEM_MST INVMST" & vbCrLf _
                '    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                '    & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                '    & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE" & vbCrLf _
                '    & " AND IH.DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf _
                '    & " AND IH.RM_CODE='" & Trim(txtRMCode.Text) & "'"

                'SqlStr = SqlStr & vbCrLf & " UNION ALL"

                'SqlStr = SqlStr & vbCrLf _
                '    & " Select DISTINCT INVMST.ITEM_SHORT_DESC,  IH.PRODUCT_CODE PRODUCT_CODE, NVL(INVMST.CUSTOMER_PART_NO,' ') As CUSTOMER_PART_NO" & vbCrLf _
                '    & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                '    & " WHERE IH.MKEY=ID.MKEY AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                '    & " And IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                '    & " And IH.PRODUCT_CODE=INVMST.ITEM_CODE" & vbCrLf _
                '    & " And ID.DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf _
                '    & " AND ID.ALTER_RM_CODE='" & Trim(txtRMCode.Text) & "'"

                'If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then

                If MainClass.SearchGridMaster(.Text, "vw_BOMSearch", "ITEM_SHORT_DESC", "PRODUCT_CODE", "CUSTOMER_PART_NO", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And DEPT_CODE='" & Trim(txtDept.Text) & "' AND RM_CODE='" & Trim(txtRMCode.Text) & "'") = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = Trim(AcName1)

                    .Col = ColItemDesc
                    .Text = Trim(AcName)
                    'Call SprdMain_LeaveCell(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False)
                    Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
                End If

            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColItemCode
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                FormatSprdMain(eventArgs.row)
            End If
        End If

    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mDivisionCode As Double
        Dim mWeightPerStrip As Double
        Dim mRMThick As Double
        Dim mRMLenth As Double
        Dim mRMWidth As Double
        Dim mDensity As Double
        Dim mProdQty As Double
        Dim mNetWt As Double
        Dim mScrapQty As Double

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
        If Trim(SprdMain.Text) = "" Then Exit Sub
        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                If DuplicateItem() = False Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColItemCode
                    If FillItemDescPart(Trim(SprdMain.Text), mDivisionCode) = False Then
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                        eventArgs.cancel = True
                        Exit Sub
                    Else
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight * 2)
                        FormatSprdMain((SprdMain.MaxRows))
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColThickness, ColWidth, ColLength

            Case ColProdQty
                If CheckQty() = True Then
                    '                MainClass.SetFocusToCell SprdMain, .ActiveRow, ColProdQty	
                    '                MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight	
                    '                FormatSprdMain SprdMain.MaxRows	
                End If
        End Select

        With SprdMain
            .Row = SprdMain.ActiveRow
            .Col = ColThickness
            .Text = CStr(Val(txtThickness.Text))
            mRMThick = Val(.Text)

            .Col = ColWidth
            mRMWidth = Val(.Text)

            .Col = ColLength
            mRMLenth = Val(.Text)

            mDensity = Val(lblDensity.Text)

            mWeightPerStrip = GetWeightPerStrip(mRMThick, mRMLenth, mRMWidth, mDensity)

            .Col = ColWt_Per_No
            .Text = VB6.Format(mWeightPerStrip, "0.00")

            .Col = ColProdQty
            mProdQty = Val(.Text)

            .Col = ColScrapQty
            mScrapQty = Val(.Text)

            .Col = ColOkQty
            .Text = VB6.Format(mProdQty - mScrapQty, "0.00")

            mNetWt = mWeightPerStrip * mProdQty

            .Col = ColTotalWeight
            .Text = VB6.Format(mNetWt, "0.00")

        End With

        CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1
        ''Dim RsMisc As ADODB.Recordset = Nothing	
        Dim mNetWt As Double
        Dim mTotNetWt As Double
        Dim i As Integer
        Dim mCTLArea As Double
        Dim mTotalCTLArea As Double
        Dim mWidth As Double
        Dim mLength As Double
        Dim mProdQty As Double
        Dim mScrapQty As Double

        mNetWt = 0
        mTotNetWt = 0
        mTotalCTLArea = 0

        With SprdMain
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColItemCode
                If Trim(.Text) <> "" Then
                    .Col = ColTotalWeight
                    mNetWt = Val(.Text)
                    mTotNetWt = mTotNetWt + mNetWt

                    .Col = ColLength
                    mLength = Val(.Text)

                    .Col = ColWidth
                    mWidth = Val(.Text)

                    .Col = ColProdQty
                    mProdQty = Val(.Text)

                    .Col = ColScrapQty
                    mScrapQty = Val(.Text)

                    .Col = ColOkQty
                    .Text = VB6.Format(mProdQty - mScrapQty, "0.00")

                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                        mCTLArea = CDbl(VB6.Format(Val((mWidth)) * Val((mLength)) * Val((mProdQty) * 0.000001), "0.00"))
                    Else
                        mCTLArea = CDbl(VB6.Format(Val(CStr(mWidth)) * Val(CStr(mLength)) * Val(CStr(mProdQty)), "0.00"))
                    End If

                    mTotalCTLArea = mTotalCTLArea + mCTLArea
                End If
            Next i
        End With

        txtCTLWt.Text = VB6.Format(mTotNetWt, "#0.00")
        txtCTLArea.Text = VB6.Format(mTotalCTLArea + Val(txtBlockQty.Text), "0.00")
        txtNetScrapWt.Text = VB6.Format(Val(txtNetRMWt.Text) - mTotNetWt, "#0.00")

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            txtRMSheetArea.Text = VB6.Format(Val(txtWidth.Text) * Val(txtLength.Text) * Val(txtRMQty.Text) * 0.000001, "0.00")
        Else
            txtRMSheetArea.Text = VB6.Format(Val(txtWidth.Text) * Val(txtLength.Text) * Val(txtRMQty.Text), "0.00")
            txtNetScrapWt.Text = VB6.Format(Val(txtNetRMWt.Text) - mTotNetWt - Val(txtBlockQty.Text), "#0.00")
        End If



        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Sub
    Private Function DuplicateItem() As Boolean
        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mItemCode As String

        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            mCheckItemCode = Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                If (mCheckItemCode = mItemCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItem = True
                    MsgInformation("Duplicate Item : " & mCheckItemCode)
                    Exit Function
                End If
            Next
        End With
    End Function
    Private Function CheckQty() As Boolean
        On Error GoTo ERR1
        Dim mProdQty As Double

        CheckQty = True
        Exit Function

        With SprdMain
            .Row = .ActiveRow
            '        .Col = ColProdQty	
            '        mProdQty = Val(.Text)	
            '	
            '        .Col = ColOKQty	
            '        mOKQty = Val(.Text)	
            '	
            '        If mProdQty < mOKQty Then	
            '            CheckQty = False	
            '        Else	
            '            CheckQty = True	
            '        End If	
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FillItemDescPart(ByRef pItemCode As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCategoryCode As String
        Dim mStockType As String
        Dim mProdItemCode As String
        Dim mItemUOM As String
        Dim mWeightPerStrip As Double
        Dim mRMThick As Double
        Dim mRMLenth As Double
        Dim mRMWidth As Double
        Dim mDensity As Double

        FillItemDescPart = False
        If Trim(pItemCode) = "" Then Exit Function

        SqlStr = "SELECT ITEM_CODE, ITEM_SHORT_DESC,ISSUE_UOM,CATEGORY_CODE, MAT_LEN, MAT_WIDTH, MAT_THICHNESS, MAT_DENSITY " & vbCrLf _
            & " FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mCategoryCode = Trim(IIf(IsDBNull(RsTemp.Fields("CATEGORY_CODE").Value), "", RsTemp.Fields("CATEGORY_CODE").Value))

            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                mProdItemCode = Trim(.Text)

                .Col = ColItemDesc
                .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColUom
                .Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                mItemUOM = Trim(.Text)

                .Col = ColThickness
                .Text = CStr(Val(txtThickness.Text)) ''Val(IIf(IsNull(RsTemp!MAT_THICHNESS), 0, RsTemp!MAT_THICHNESS))	
                mRMThick = Val(txtThickness.Text) ''Val(IIf(IsNull(RsTemp!MAT_THICHNESS), 0, RsTemp!MAT_THICHNESS))	

                .Col = ColWidth
                If Val(.Text) = 0 Then
                    .Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("MAT_WIDTH").Value), 0, RsTemp.Fields("MAT_WIDTH").Value)))
                End If
                mRMWidth = Val(.Text)

                .Col = ColLength
                If Val(.Text) = 0 Then
                    .Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("MAT_LEN").Value), 0, RsTemp.Fields("MAT_LEN").Value)))
                End If
                mRMLenth = Val(.Text)

                '            .Col = ColDensity	
                '            .Text = Val(IIf(IsNull(RsTemp!MAT_DENSITY), 0, RsTemp!MAT_DENSITY))	
                mDensity = Val(lblDensity.Text)

                mWeightPerStrip = GetWeightPerStrip(mRMThick, mRMLenth, mRMWidth, mDensity)

                .Col = ColWt_Per_No
                .Text = VB6.Format(mWeightPerStrip, "0.00")


                '            .Col = ColStockQty	
                '            .Text = GetBalanceStockQty(mProdItemCode, txtPMemoDate.Text, mItemUOM, Trim(txtDept.Text), "ST", "", ConPH, mDivisionCode, ConStockRefType_PMEMODEPT, Val(txtPMemoNo.Text))	

                FillItemDescPart = True
            End With
        Else
            MsgInformation("Invalid Item Code.")
            FillItemDescPart = False
        End If
        Exit Function
ERR1:
        FillItemDescPart = False
        MsgInformation(Err.Description)
    End Function

    Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdView.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.row
            .Col = 1
            txtPMemoNo.Text = .Text
            txtPMemoNo_Validating(txtPMemoNo, New System.ComponentModel.CancelEventArgs(False))
            If txtPMemoNo.Enabled = True Then txtPMemoNo.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function AutoGenMemoNo() As String

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Double
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        mAutoGen = 90000
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_REF)  " & vbCrLf & " FROM PRD_CUTTINGPLAN_HDR  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = Mid(mMaxValue, 1, Len(mMaxValue) - 6)
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = mAutoGen + 1
                End If
            End If
        End With
        AutoGenMemoNo = mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mPMemoNo As Double
        Dim mEntryDate As String
        Dim pErrorDesc As String
        'Dim RsTemp As ADODB.Recordset = Nothing	
        Dim mDivisionCode As Double
        Dim pBlockArea As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime()

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        SqlStr = ""
        mPMemoNo = Val(txtPMemoNo.Text)
        If Val(txtPMemoNo.Text) = 0 Then
            mPMemoNo = CDbl(AutoGenMemoNo())
        End If

        pBlockArea = Val(txtBlockQty.Text)

        txtPMemoNo.Text = CStr(mPMemoNo)
        If ADDMode = True Then
            lblMKey.Text = CStr(mPMemoNo)
            SqlStr = " INSERT INTO PRD_CUTTINGPLAN_HDR  " & vbCrLf _
                & " (COMPANY_CODE,FYEAR,AUTO_KEY_REF," & vbCrLf _
                & " REF_DATE, PREP_TIME, PROD_DATE, DEPT_CODE, SHIFT_CODE,PROD_TYPE," & vbCrLf _
                & " EMP_CODE, REMARKS, BOOKTYPE,  " & vbCrLf _
                & " ADDUSER,ADDDATE,MODUSER,MODDATE, DIV_CODE," & vbCrLf _
                & " RM_CODE, RM_UOM, " & vbCrLf _
                & " RM_QTY, RM_THICKNESS, RM_WIDTH, " & vbCrLf _
                & " RM_LENGTH, RM_NET_WT, CTL_NET_WT, SCRAP_NET_WT, RM_BLOCK_CODE, RM_BLOCK_QTY, RM_AREA, CTL_AREA, BLOCK_AREA" & vbCrLf _
                & " ) VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & "," & mPMemoNo & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & txtRefTM.Text & "','HH24:MI'), " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                & " '" & cboShiftcd.Text & "', " & vbCrLf _
                & " '" & VB.Left(cboType.Text, 1) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtEmp.Text) & "','" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " '" & VB.Left(lblBookType.Text, 1) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'',''," & vbCrLf _
                & " " & mDivisionCode & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRMCode.Text) & "', '" & MainClass.AllowSingleQuote(lblRMUOM.Text) & "'," & vbCrLf _
                & " " & Val(txtRMQty.Text) & ", " & Val(txtThickness.Text) & ", " & Val(txtWidth.Text) & "," & vbCrLf _
                & " " & Val(txtLength.Text) & ", " & Val(txtNetRMWt.Text) & ", " & Val(txtCTLWt.Text) & ", " & Val(txtNetScrapWt.Text) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtBlockCode.Text) & "', " & Val(txtBlockQty.Text) & ", " & Val(txtRMSheetArea.Text) & "," & Val(txtCTLArea.Text) & "," & Val(pBlockArea) & ")"

        ElseIf MODIFYMode = True Then
            mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime()
            SqlStr = " UPDATE PRD_CUTTINGPLAN_HDR  SET " & vbCrLf _
                & " AUTO_KEY_REF=" & mPMemoNo & ", " & vbCrLf _
                & " REF_DATE=TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " PROD_DATE=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " PREP_TIME=TO_DATE('" & txtRefTM.Text & "','HH24:MI')," & vbCrLf _
                & " DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                & " SHIFT_CODE='" & cboShiftcd.Text & "', " & vbCrLf _
                & " PROD_TYPE= '" & VB.Left(cboType.Text, 1) & "'," & vbCrLf _
                & " EMP_CODE='" & MainClass.AllowSingleQuote(txtEmp.Text) & "', " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', RM_BLOCK_CODE='" & MainClass.AllowSingleQuote(txtBlockCode.Text) & "', RM_BLOCK_QTY=" & Val(txtBlockQty.Text) & "," & vbCrLf _
                & " BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'), DIV_CODE=" & mDivisionCode & "," & vbCrLf _
                & " RM_CODE='" & MainClass.AllowSingleQuote(txtRMCode.Text) & "', RM_UOM='" & MainClass.AllowSingleQuote(lblRMUOM.Text) & "', " & vbCrLf _
                & " RM_QTY=" & Val(txtRMQty.Text) & ", RM_THICKNESS=" & Val(txtThickness.Text) & ", RM_WIDTH= " & Val(txtWidth.Text) & ", " & vbCrLf _
                & " RM_LENGTH=" & Val(txtLength.Text) & ", RM_NET_WT=" & Val(txtNetRMWt.Text) & ",  CTL_NET_WT=" & Val(txtCTLWt.Text) & ", SCRAP_NET_WT=" & Val(txtNetScrapWt.Text) & "," & vbCrLf _
                & " RM_AREA=" & Val(txtRMSheetArea.Text) & ", CTL_AREA=" & Val(txtCTLArea.Text) & ", BLOCK_AREA=" & Val(pBlockArea) & "" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND AUTO_KEY_REF=" & Val(lblMKey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(pErrorDesc, mDivisionCode) = False Then GoTo ErrPart
        UpdateMain1 = True
        PubDBCn.CommitTrans()
        txtPMemoNo.Text = CStr(mPMemoNo)
        Exit Function
ErrPart:

        UpdateMain1 = False
        PubDBCn.RollbackTrans()
        If pErrorDesc <> "" Then
            MsgInformation(pErrorDesc)
        End If
        RsPMemoMain.Requery()
        RsPMemoDetail.Requery()
        If Trim(Err.Description) <> "" Then
            MsgBox(Err.Description)
        End If
        If ADDMode = True Then
            lblMKey.Text = ""
            txtPMemoNo.Text = ""
        End If
        '    Resume	
    End Function
    Private Function UpdateDetail1(ByRef pErrorDesc As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String
        'Dim mStockType As String	
        Dim mProdQty As Double
        Dim xStockRowNo As Integer
        Dim xItemCost As Double
        Dim mInCCCode As String

        Dim mSFThickNess As Double
        Dim mSFWidth As Double
        Dim mSFLength As Double
        Dim mSFWtPerUnit As Double
        Dim mSFQty As Double
        Dim mSFNetWt As Double
        Dim mRemarks As String
        Dim mSFDensity As Double
        Dim mScrapQty As Double

        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "CCCODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInCCCode = IIf(IsDBNull(MasterNo), "-1", MasterNo)
        Else
            mInCCCode = "-1"
        End If
        '	
        '    mProductionDate = Format(txtPMemoDate.Text, "DD/MM/YYYY")	
        '    If Left(cboShiftcd.Text, 1) = "C" Then	
        '        mEntryDate = DateAdd("d", 1, mProductionDate)	
        '    Else	
        '        mEntryDate = mProductionDate	
        '    End If	

        SqlStr = " DELETE FROM PRD_CUTTINGPLAN_DET " & vbCrLf & " WHERE AUTO_KEY_REF=" & Val(lblMKey.Text) & " "
        PubDBCn.Execute(SqlStr)

        If DeleteStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text)) = False Then GoTo UpdateDetail1Err
        xStockRowNo = 1

        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUom
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColThickness
                mSFThickNess = Val(.Text)

                .Col = ColThickness
                mSFThickNess = Val(.Text)

                .Col = ColWidth
                mSFWidth = Val(.Text)

                .Col = ColLength
                mSFLength = Val(.Text)

                '            .Col = ColDensity	
                '            mSFDensity = Val(.Text)	

                .Col = ColWt_Per_No
                mSFWtPerUnit = Val(.Text)

                .Col = ColProdQty
                mProdQty = Val(.Text)

                .Col = ColScrapQty
                mScrapQty = Val(.Text)

                .Col = ColTotalWeight
                mSFNetWt = Val(.Text)

                .Col = ColRemarks
                mRemarks = Trim(.Text)

                '            .Col = ColStockType	
                '            mStockType = IIf(Left(cboType.Text, 1) = "P", "ST", "CS")       ''MainClass.AllowSingleQuote(.Text)	
                '	
                '            .Col = ColReason	
                '            mReason = Trim(.Text)	


                If mItemCode <> "" And mProdQty > 0 Then
                    SqlStr = " INSERT INTO PRD_CUTTINGPLAN_DET ( " & vbCrLf _
                        & " COMPANY_CODE, AUTO_KEY_REF, SERIAL_NO, " & vbCrLf _
                        & " SF_CODE, SF_UOM, SF_THICKNESS,  " & vbCrLf _
                        & " SF_WIDTH, SFLENGTH, SF_WT_PER_UNIT, " & vbCrLf _
                        & " SF_QTY, SF_NET_WT, REMARKS, SCRAP_QTY) " & vbCrLf _
                        & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMKey.Text) & ", " & i & "," & vbCrLf _
                        & " '" & mItemCode & "','" & mUOM & "', " & mSFThickNess & ", " & vbCrLf _
                        & " " & mSFWidth & ", " & mSFLength & ", " & mSFWtPerUnit & "," & vbCrLf _
                        & " " & mProdQty & ", " & mSFNetWt & ", '" & MainClass.AllowSingleQuote(mRemarks) & "'," & mScrapQty & "" & vbCrLf _
                        & " )"

                    PubDBCn.Execute(SqlStr)

                    If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "ST", mItemCode, mUOM, CStr(-1), mProdQty - mScrapQty, 0, "I", xItemCost, xItemCost, "-1", "", (txtDept.Text), (txtDept.Text), mInCCCode, "N", "From : " & lblDept.Text & "  : (Production Cutting Plan) -" & ConStockRefType_PMEMODEPT, "-1", ConPH, mDivisionCode, "", (txtRMCode.Text)) = False Then GoTo UpdateDetail1Err

                    xStockRowNo = xStockRowNo + 1
                    If mScrapQty > 0 Then
                        If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "SC", mItemCode, mUOM, CStr(-1), mScrapQty, 0, "I", xItemCost, xItemCost, "-1", "", (txtDept.Text), (txtDept.Text), mInCCCode, "N", "From : " & lblDept.Text & "  : (Production Cutting Scrap) -" & ConStockRefType_PMEMODEPT, "-1", ConPH, mDivisionCode, "", (txtRMCode.Text)) = False Then GoTo UpdateDetail1Err
                    End If

                    xStockRowNo = xStockRowNo + 1
                End If
NextRec:
            Next
        End With

        xStockRowNo = xStockRowNo + 1
        Dim mItemQty As Double = 0

        If lblRMUOM.Text = "SQM" Then
            mItemQty = Val(txtRMSheetArea.Text)
        ElseIf lblRMUOM.Text = "KGS" Then
            mItemQty = Val(txtNetRMWt.Text)
        Else
            mItemQty = Val(txtRMQty.Text)
        End If

        If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "ST", (txtRMCode.Text), (lblRMUOM.Text), CStr(-1), Val(mItemQty), 0, "O", xItemCost, xItemCost, "-1", "", (txtDept.Text), (txtDept.Text), mInCCCode, "N", "From : " & lblDept.Text & "  : (Production Cutting Plan) -" & ConStockRefType_PMEMODEPT, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err

        xStockRowNo = xStockRowNo + 1

        If lblRMUOM.Text = "SQM" Then
            mItemQty = Val(txtRMSheetArea.Text) - Val(txtCTLArea.Text)
        ElseIf lblRMUOM.Text = "KGS" Then
            mItemQty = Val(txtNetScrapWt.Text)
        Else
            mItemQty = 0
        End If

        If Val(mItemQty) > 0 Then
            If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "SC", (txtRMCode.Text), (lblRMUOM.Text), CStr(-1), Val(mItemQty), 0, "I", xItemCost, xItemCost, "-1", "", (txtDept.Text), (txtDept.Text), mInCCCode, "N", "From : " & lblDept.Text & "  : (Production Cutting Plan) -" & ConStockRefType_PMEMODEPT, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
        End If

        xStockRowNo = xStockRowNo + 1
        If Val(txtBlockQty.Text) > 0 Then
            If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "ST", (txtBlockCode.Text), (lblBlockUOM.Text), CStr(-1), Val(txtBlockQty.Text), 0, "I", xItemCost, xItemCost, "-1", "", (txtDept.Text), (txtDept.Text), mInCCCode, "N", "From : " & lblDept.Text & "  : (Production Cutting Plan) -" & ConStockRefType_PMEMODEPT, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
        End If

        pErrorDesc = ""
        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        '    Resume	
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Function
    'Private Function UpdateBOMStock(pErrorDesc As String, pRsBOM As ADODB.Recordset, mFICode As String, mFQty As Double, mStockRowNo As Long, mRetStockRowNo As Long, mRetItemCost As Double, pInCCCode As String, pOutCCCode As String, mDivisionCode As Double) As Boolean	
    'On Error GoTo BOMStockErr	
    'Dim SqlStr As String=""=""	
    'Dim mStdQty As Double	
    'Dim mRMGrossQtyGram As Double	
    'Dim mRMGrossQtyKg As Double	
    'Dim mRMCostKg As Double	
    'Dim mScrpGrossQtyGram As Double	
    'Dim mScrpGrossQtyKg As Double	
    'Dim mScrpCostKg As Double	
    'Dim mSUOM As String	
    'Dim mProductionQty As Double	
    'Dim xProductionQty As Double	
    'Dim mStockQty As Double	
    'Dim mTotStockQty As Double	
    'Dim mScrapCode As String	
    'Dim mRMCode As String	
    'Dim mRMCodeStr As String	
    'Dim mRMUOM As String	
    '	
    'Dim pSqlStr As String	
    'Dim RsTemp As ADODB.Recordset = Nothing	
    'Dim mMKEY As String	
    'Dim mBalFQty As Double	
    'Dim mUsedRMQty As Double	
    'Dim mUsedSFQty As Double	
    'Dim mStockType As String	
    'Dim mFromScrap As String	
    'Dim mUsedScrap As Double	
    'Dim xWareHouse As String	
    'Dim mISProd As Boolean	
    '	
    '	
    '    With pRsBOM	
    '        Do While Not .EOF	
    '            mRMCode = Trim(IIf(IsNull(!RM_CODE), "", !RM_CODE))	
    '            If IsFGItem(mRMCode) = True Then	
    '                xWareHouse = ConPH	
    '            Else	
    '                If CheckAutoIssue(Format(txtPMemoDate.Text, "DD/MM/YYYY"), mRMCode) = False Then '' RsCompany!AUTO_ISSUE = "N"	
    '                    xWareHouse = "PH"	
    '                Else	
    '                    mISProd = IsProductionItem(mRMCode)	
    '                    If mISProd = True Then	
    '                        If CDate(Format(txtPMemoDate.Text, "DD/MM/YYYY")) < CDate(Format(RsCompany!AUTO_ISSUE_DATE, "DD/MM/YYYY")) Then	
    '                            xWareHouse = "PH"	
    '                        Else	
    '                            xWareHouse = "WH"	
    '                        End If	
    '                    Else	
    '                        xWareHouse = "PH"	
    '                    End If	
    '                End If	
    '            End If	
    '	
    '            mMKEY = IIf(IsNull(!mKey), "", !mKey)	
    '            mFromScrap = IIf(IsNull(!FROM_SCRAP), "N", !FROM_SCRAP)	
    '            mStockType = IIf(IsNull(!STOCK_TYPE), "ST", !STOCK_TYPE)	
    '	
    '            mStdQty = Val(IIf(IsNull(!STD_QTY), "", !STD_QTY)) + Val(IIf(IsNull(!GROSS_WT_SCRAP), "", !GROSS_WT_SCRAP))	
    '            mRMCodeStr = mRMCode	
    '            mRMUOM = IIf(IsNull(!ISSUE_UOM), "", !ISSUE_UOM)	
    '            mScrapCode = IIf(IsNull(!SCRAP_ITEM_CODE), "", !SCRAP_ITEM_CODE)	
    '	
    '            If UCase(Trim(mRMUOM)) = "TON" Then	
    '                mProductionQty = Val((mStdQty * mFQty) / 1000)	
    '                mProductionQty = Val(mProductionQty / 1000)	
    '            ElseIf UCase(Trim(mRMUOM)) = "KGS" Then	
    '                mProductionQty = Val(mStdQty / 1000) * mFQty	
    '            Else	
    '                mProductionQty = mStdQty * mFQty	
    '            End If	
    '	
    '	
    '            mScrpGrossQtyGram = Val(IIf(IsNull(!GROSS_WT_SCRAP), "", !GROSS_WT_SCRAP))	
    '            mScrpGrossQtyKg = Val(mScrpGrossQtyGram / 1000)	
    '            mScrpGrossQtyKg = Val(mScrpGrossQtyKg * mFQty)	
    '	
    '            If mProductionQty > 0 Then	
    '                mStockRowNo = mStockRowNo + 1	
    '                If chkProduction.Value = vbUnchecked Then	
    '                    If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, txtPMemoNo.Text, mStockRowNo, txtPMemoDate.Text, txtPMemoDate.Text, _	
    ''                            mStockType, mRMCode, mRMUOM, -1, mProductionQty + mScrpGrossQtyKg, 0, "I", mRMCostKg, mRMCostKg, "", "", txtDept.Text, txtDept.Text, pOutCCCode, "N", "TO : " & lblDept.text & " (Production Break-up) -" & ConStockRefType_PMEMODEPT & "-" & mFICode, "-1", xWareHouse, mDivisionCode, "", mFICode) = False Then GoTo BOMStockErr	
    '                Else	
    '                    If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, txtPMemoNo.Text, mStockRowNo, txtPMemoDate.Text, txtPMemoDate.Text, _	
    ''                            mStockType, mRMCode, mRMUOM, -1, mProductionQty + mScrpGrossQtyKg, 0, "O", mRMCostKg, mRMCostKg, "", "", txtDept.Text, txtDept.Text, pOutCCCode, "N", "TO : " & lblDept.text & " (Production after Physical) -" & ConStockRefType_PMEMODEPT & "-" & mFICode, "-1", xWareHouse, mDivisionCode, "", mFICode) = False Then GoTo BOMStockErr	
    '	
    '                End If	
    '            End If	
    '	
    '            If mScrpGrossQtyKg > 0 Then	
    '                mStockRowNo = mStockRowNo + 1	
    '                If chkProduction.Value = vbUnchecked Then	
    '                    If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, txtPMemoNo.Text, mStockRowNo, txtPMemoDate.Text, txtPMemoDate.Text, _	
    ''                            "SC", mRMCode, mRMUOM, -1, mScrpGrossQtyKg, 0, "O", mRMCostKg, mRMCostKg, "", "", txtDept.Text, txtDept.Text, pOutCCCode, "N", "TO : " & lblDept.text & " (Production Break-up) -" & ConStockRefType_PMEMODEPT & "-" & mFICode, "-1", xWareHouse, mDivisionCode, "", mFICode) = False Then GoTo BOMStockErr	
    '                Else	
    '                    If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, txtPMemoNo.Text, mStockRowNo, txtPMemoDate.Text, txtPMemoDate.Text, _	
    ''                            "SC", mRMCode, mRMUOM, -1, mScrpGrossQtyKg, 0, "I", mRMCostKg, mRMCostKg, "", "", txtDept.Text, txtDept.Text, pOutCCCode, "N", "TO : " & lblDept.text & " (Production after Physical) -" & ConStockRefType_PMEMODEPT & "-" & mFICode, "-1", xWareHouse, mDivisionCode, "", mFICode) = False Then GoTo BOMStockErr	
    '                End If	
    '            End If	
    '	
    '            pRsBOM.MoveNext	
    '        Loop	
    '    End With	
    '    mRetStockRowNo = mStockRowNo	
    '    UpdateBOMStock = True	
    '    Exit Function	
    'BOMStockErr:	
    '    UpdateBOMStock = False	
    '    If err.Description <> "" Then MsgBox err.Description	
    ''    Resume	
    'End Function	

    Private Function CheckRMStock(ByRef pErrorDesc As String, ByRef mMKEY As String, ByRef mFICode As String, ByRef mRMCode As String, ByRef mRMUOM As String, ByRef mDeptCode As String, ByRef pFQty As Double, ByRef pStdQty As Double, ByRef mReqQty As Double, ByRef pStockType As String, ByRef xWareHouse As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo BOMStockErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAlterRMCode As String
        Dim mAlterRMUOM As String
        Dim mAlterStdQty As Double
        Dim mStockQty As Double
        Dim mReqStockQty As Double
        Dim mTotStockQty As Double
        Dim pFQtyUsed As Double
        Dim pBalFQty As Double
        Dim mRMCodeStr As String
        Dim mFGUOM As String
        Dim mProd_Type As String


        mStockQty = GetBalanceStockQty(mRMCode, (txtPMemoDate.Text), mRMUOM, mDeptCode, pStockType, "", xWareHouse, mDivisionCode)
        mTotStockQty = mStockQty
        mRMCodeStr = mRMCode
        If UCase(Trim(mRMUOM)) = "TON" Then
            mStockQty = mStockQty * 1000 * 1000
        ElseIf UCase(Trim(mRMUOM)) = "KGS" Then
            mStockQty = mStockQty * 1000
        End If

        If MainClass.ValidateWithMasterTable(mFICode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mFGUOM = MasterNo
        End If

        If mFGUOM = "KGS" Or mFGUOM = "TON" Or mFGUOM = "LTR" Then
            pFQtyUsed = mStockQty / pStdQty
        Else
            pFQtyUsed = Int(mStockQty / pStdQty)
        End If

        pBalFQty = pFQty - pFQtyUsed

        If pBalFQty <= 0 Then
            CheckRMStock = True
            Exit Function
        End If

        SqlStr = " SELECT ID.ALTER_RM_CODE, ALTER_STD_QTY ,ALETRSCRAP, INVMST.ISSUE_UOM " & vbCrLf _
            & " FROM PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST " & vbCrLf _
            & " WHERE ID.MKEY='" & mMKEY & "'" & vbCrLf _
            & " AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(UCase(mDeptCode)) & "' " & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.ALTER_RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND ID.MAINITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mRMCode)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                mAlterRMCode = Trim(IIf(IsDBNull(RsTemp.Fields("ALTER_RM_CODE").Value), "", RsTemp.Fields("ALTER_RM_CODE").Value))
                mAlterRMUOM = Trim(IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                mAlterStdQty = Val(IIf(IsDBNull(RsTemp.Fields("ALTER_STD_QTY").Value), 0, RsTemp.Fields("ALTER_STD_QTY").Value)) + Val(IIf(IsDBNull(RsTemp.Fields("ALETRSCRAP").Value), 0, RsTemp.Fields("ALETRSCRAP").Value))
                mRMCodeStr = mRMCodeStr & "," & mAlterRMCode

                mStockQty = GetBalanceStockQty(mAlterRMCode, (txtPMemoDate.Text), mAlterRMUOM, mDeptCode, pStockType, "", xWareHouse, mDivisionCode)
                mTotStockQty = mTotStockQty + mStockQty

                If UCase(Trim(mRMUOM)) = "TON" Then
                    mStockQty = mStockQty * 1000 * 1000
                ElseIf UCase(Trim(mRMUOM)) = "KGS" Then
                    mStockQty = mStockQty * 1000
                End If
                pFQtyUsed = Int(mStockQty / mAlterStdQty)
                pBalFQty = pBalFQty - pFQtyUsed

                RsTemp.MoveNext()
                If pBalFQty <= 0 Then Exit Do
            Loop
        End If

        If pBalFQty <= 0 Then
            CheckRMStock = True
        Else
            '        MsgInformation "You have Not Enough Stock. For Finished Goods " & mFICode & vbNewLine & "(Item Code : " & mRMCodeStr & "( Req. Qty : " & mReqQty & " And Bal. Qty : " & mTotStockQty & "))." & vbNewLine & " Cann't Save."	
            pErrorDesc = "You have Not Enough Stock. For Finished Goods " & mFICode & vbNewLine & "(Item Code : " & mRMCodeStr & "( Req. Qty : " & mReqQty & " And Bal. Qty : " & mTotStockQty & "))." & vbNewLine & " Cann't Save."
            CheckRMStock = False
        End If
        Exit Function
BOMStockErr:
        CheckRMStock = False
        If Err.Description <> "" Then MsgBox(Err.Description)
        'Resume	
    End Function
    Private Function MakeBOMStockQty(ByRef mSFICode As String, ByRef mDeptCode As String, ByRef pDeptSeq As Integer) As String
        On Error GoTo BOMStockErr
        Dim SqlStr As String = ""


        SqlStr = "SELECT  IH.MKEY, IH.PRODUCT_CODE, " & vbCrLf _
            & " ID.RM_CODE, ID.STD_QTY, ID.DEPT_CODE," & vbCrLf _
            & " ID.GROSS_WT_SCRAP, INVMST.ISSUE_UOM, INVMST.SCRAP_ITEM_CODE, FROM_SCRAP, ID.STOCK_TYPE "

        SqlStr = SqlStr & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST"

        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(mSFICode)) & "' "

        '    If pDeptSeq = 1 Then
        '        SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(UCase(mDeptCode)) & "' "
        '    Else
        SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE IN ( " & vbCrLf _
                & " SELECT DEPT_CODE FROM PRD_PRODSEQUENCE_DET" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mSFICode) & "'" & vbCrLf _
                & " AND WEF = (" & vbCrLf _
                & " SELECT MAX(WEF) FROM PRD_PRODSEQUENCE_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mSFICode) & "'" & vbCrLf _
                & " AND WEF<=TO_DATE('" & VB6.Format(txtPMemoDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
                & " AND SERIAL_NO<=" & Val(pDeptSeq) & ")"
        '    End If

        SqlStr = SqlStr & vbCrLf _
                & " AND IH.WEF=( " & vbCrLf _
                & " SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(mSFICode)) & "' " & vbCrLf _
                & " AND WEF<= TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"



        MakeBOMStockQty = SqlStr
        Exit Function
BOMStockErr:
        If Err.Description <> "" Then MsgBox(Err.Description)
        'Resume	
    End Function

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mDeptCode As String
        Dim mCheckLastEntryDate As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mProductCode As String
        Dim mOPRCode As String
        Dim mOPRDesc As String
        Dim mTotalProduction As Double
        Dim mProdQty As Double
        Dim mStockQty As Double

        FieldsVarification = True
        If ValidateBranchLocking((txtPMemoDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Memo No or modify an existing Memo No")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsPMemoMain.EOF = True Then Exit Function

        If txtPMemoDate.Text = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPMemoDate.Focus()
            Exit Function
        End If

        If Trim(cboShiftcd.Text) = "" Then
            MsgBox("Shift is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            cboShiftcd.Focus()
            Exit Function
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If

        If Trim(cboShiftcd.Text) = "C" Then
            txtProdDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"))))
            txtProdDate.Text = VB6.Format(txtProdDate.Text, "DD/MM/YYYY")
        Else
            txtProdDate.Text = VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")
        End If

        If CDate(txtProdDate.Text) > CDate(PubCurrDate) Then
            MsgBox("Production Date Cann't be Greater than Current Date", MsgBoxStyle.Information)
            FieldsVarification = False
            'txtProdDate.Focus()
            Exit Function
        End If

        If txtDept.Text = "" Then
            MsgBox("From Deptt is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDept.Focus()
            Exit Function
        End If

        If cboType.Text = "" Then
            MsgBox("Production Type is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboType.Enabled = True Then cboType.Focus()
            Exit Function
        End If

        If txtEmp.Text = "" Then
            MsgBox("Employee is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtEmp.Focus()
            Exit Function
        End If

        Call CalcTots()


        Dim mScrapQty As Double

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mProductCode = Trim(.Text)

                If mProductCode <> "" Then
                    If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = False Then
                        If MsgQuestion("Product Code : " & mProductCode & " is Inactive. Want to Proceed ?") = CStr(MsgBoxResult.No) Then
                            FieldsVarification = False
                            MainClass.SetFocusToCell(SprdMain, cntRow, ColItemCode)
                            Exit Function
                        End If
                    End If
                End If

                If mProductCode <> "" Then
                    SqlStr = " SELECT PRODUCT_CODE " & vbCrLf _
                        & " FROM PRD_NEWBOM_HDR" & vbCrLf _
                        & " WHERE " & vbCrLf _
                        & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = True Then
                        MsgInformation("Please Defined B.O.M. For Product Code : " & mProductCode & ". Cann't Be Saved")
                        FieldsVarification = False
                        '                MainClass.SetFocusToCell SprdMain, CntRow, ColItemCode	
                        Exit Function
                    End If
                End If

                .Col = ColProdQty
                mProdQty = Val(.Text)

                .Col = ColScrapQty
                mScrapQty = Val(.Text)

                If mScrapQty > mProdQty Then
                    MsgInformation("Scrap Qty Cann't be Greater Than Production Qty, Item Code : " & mProductCode & ". Cann't Be Saved")
                    FieldsVarification = False
                    MainClass.SetFocusToCell(SprdMain, cntRow, ColScrapQty)
                    Exit Function
                End If

                '           .Col = ColProdQty	
                '           mProdQty = Val(.Text)	
                '	
                '           If mProdQty > mStockQty And chkProduction.Value = vbUnchecked Then	
                '                MsgInformation "Product Qty Cann't be Greater Than Stock Qty, Item Code : " & mProductCode & ". Cann't Be Saved"	
                '                FieldsVarification = False	
                '                MainClass.SetFocusToCell SprdMain, cntRow, ColProdQty	
                '                Exit Function	
                '           End If	
            Next
        End With

        If CheckRowCount() = False Then
            FieldsVarification = False
            Exit Function
        End If

        If ValidateDeptRight(PubUserID, Trim(txtDept.Text), UCase(Trim(lblDept.Text))) = False Then
            FieldsVarification = False
            Exit Function
        End If

        mCheckLastEntryDate = GetLastEntryDate()
        mCheckLastEntryDate = DateAdd("d", -1, mCheckLastEntryDate)
        If PubSuperUser <> "S" Then
            If mCheckLastEntryDate <> "" Then
                If CDate(txtPMemoDate.Text) < CDate(mCheckLastEntryDate) Then
                    MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If


        If Val(txtRMSheetArea.Text) < Val(txtCTLArea.Text) Then
            MsgBox("CTL Area cann't be Greater than Sheet Area", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If


        '     If PubSuperUser = "U" Then	
        '        If MainClass.ValidateWithMasterTable(PubUserEMPCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then	
        '            mDeptCode = MasterNo	
        '            If UCase(Trim(txtDept.Text)) <> UCase(Trim(mDeptCode)) Then	
        '                MsgBox "You Are Not in This Dept.", vbInformation	
        '                FieldsVarification = False	
        '            End If	
        '        Else	
        '            MsgBox "Invalid Emp Code.", vbInformation	
        '            FieldsVarification = False	
        '        End If	
        '    End If	

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function

        If MainClass.ValidDataInGrid(SprdMain, ColProdQty, "N", "Production Qty Is Blank.") = False Then FieldsVarification = False : Exit Function

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
        Else
            If MainClass.ValidDataInGrid(SprdMain, ColTotalWeight, "N", "Total Weight Qty Is Blank.") = False Then FieldsVarification = False : Exit Function
        End If

        '    If MainClass.ValidDataInGrid(SprdMain, ColprodQty, "N", "Please Check Produce Quantity.") = False Then FieldsVarification = False: Exit Function	
        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Function

    Private Function GetDeptSeq(ByRef mProductCode As String, ByRef pDeptCode As String) As Integer
        'On Error GoTo err	
        'Dim SqlStr As String=""=""	
        'Dim RsTemp As ADODB.Recordset = Nothing	
        '	
        '    SqlStr = " SELECT SERIAL_NO " & vbCrLf _	
        ''            & " FROM PRD_PRODSEQUENCE_DET TRN" & vbCrLf _	
        ''            & " WHERE " & vbCrLf _	
        ''            & " TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
        ''            & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _	
        ''            & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf _	
        ''            & " AND WEF = ( " & vbCrLf _	
        ''            & " SELECT MAX(WEF) AS WEF FROM PRD_PRODSEQUENCE_DET " & vbCrLf _	
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
        ''            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _	
        ''            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf _	
        ''            & " AND WEF<='" & vb6.Format(txtPMemoDate, "DD-MMM-YYYY") & "')"	
        '	
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly	
        '	
        '    If RsTemp.EOF = False Then	
        '        GetDeptSeq = IIf(IsNull(RsTemp!SERIAL_NO), 0, RsTemp!SERIAL_NO)	
        '    Else	
        '        GetDeptSeq = 0	
        '    End If	
        '	
        '    If chkSPD.Value = vbChecked Then	
        '        GetDeptSeq = GetDeptSeq - 1	
        '    End If	
        '	
        '    Exit Function	
        'err:	
        '    ErrorMsg err.Description, err.Number, vbCritical	
        ''Resume	
    End Function
    Private Function GetLastEntryDate() As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = ""

        SqlStr = "SELECT Max(REF_DATE) AS  REF_DATE " & vbCrLf _
            & " FROM PRD_CUTTINGPLAN_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _
            & " AND PROD_TYPE='" & VB.Left(cboType.Text, 1) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetLastEntryDate = IIf(IsDBNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value)
        End If

        Exit Function
ErrPart:

    End Function
    Private Function CheckRowCount() As Boolean
        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemCode As String
        Dim mQty As Double
        Dim mRowCount As Integer
        Dim mTotQty As Double
        Dim mMainItemCode As String

        mRowCount = 0
        mTotQty = 0
        CheckRowCount = True
        With SprdMain
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColProdQty
                mQty = Val(.Text)
                mTotQty = mTotQty + mQty

                mMainItemCode = GetMainItemCode(mItemCode)

                If mMainItemCode <> mItemCode And mQty > 0 Then
                    CheckRowCount = False
                    MsgInformation("Relationship made for Item : " & mItemCode & " with " & mMainItemCode & ". Cann't be save")
                    Exit Function
                End If
                If mItemCode <> "" And mQty > 0 Then
                    mRowCount = mRowCount + 1
                End If
            Next
        End With

        If mTotQty = 0 Then
            CheckRowCount = False
            MsgInformation("Nothing To Save.")
            Exit Function
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
        CheckRowCount = False
    End Function

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmPMemoCuttingPlan_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Production - Cutting Plan"


        SqlStr = ""
        SqlStr = "Select * from PRD_CUTTINGPLAN_HDR  Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_CUTTINGPLAN_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoDetail, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)
        Call SetTextLengths()
        Clear1()
        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = " SELECT  AUTO_KEY_REF MEMO_NO, TO_CHAR(REF_DATE,'DD/MM/YYYY') MEMO_DATE, " & vbCrLf & " DEPT_CODE FROM_DEPT,SHIFT_CODE,DECODE(PROD_TYPE,'P','Production','Jobwork') AS Prod_Type,REMARKS " & vbCrLf & " FROM PRD_CUTTINGPLAN_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "

        SqlStr = SqlStr & vbCrLf & " ORDER BY REF_DATE,AUTO_KEY_REF"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 400)

            .set_ColWidth(0, 600)
            .set_ColWidth(1, 1200)
            .Col = 1
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            .set_ColWidth(2, 1200)
            .Col = 2
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

            .set_ColWidth(3, 1000)
            .set_ColWidth(4, 1000)
            .set_ColWidth(5, 1000)
            .set_ColWidth(6, 2500)


            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .set_RowHeight(0, ConRowHeight * 2)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.75)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPMemoDetail.Fields("SF_CODE").DefinedSize
            .set_ColWidth(.Col, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 36)

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsPMemoDetail.Fields("SF_UOM").DefinedSize
            .set_ColWidth(.Col, 4)

            For cntCol = ColThickness To ColWt_Per_No
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 7)
            Next

            .Col = ColThickness
            .ColHidden = True

            .Col = ColProdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            .Col = ColScrapQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            .Col = ColOkQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            .Col = ColTotalWeight
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)
            '.ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, True, False)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsPMemoDetail.Fields("REMARKS").DefinedSize
            .set_ColWidth(.Col, 20)

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColThickness)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColWt_Per_No, ColWt_Per_No)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColOkQty, ColOkQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColTotalWeight, ColTotalWeight)

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        '    Resume	
        If Err.Number = -2147418113 Then RsPMemoDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsPMemoMain
            txtPMemoNo.MaxLength = .Fields("AUTO_KEY_REF").Precision
            txtPMemoDate.MaxLength = 10
            txtDept.MaxLength = .Fields("DEPT_CODE").DefinedSize
            txtEmp.MaxLength = .Fields("EMP_CODE").DefinedSize
            txtRemarks.MaxLength = .Fields("REMARKS").DefinedSize

            txtRMCode.MaxLength = .Fields("RM_CODE").DefinedSize
            txtBlockCode.MaxLength = .Fields("RM_BLOCK_CODE").DefinedSize
            txtBlockQty.MaxLength = .Fields("RM_BLOCK_QTY").Precision

            '        txtRMQty.MaxLength = .Fields("AUTO_KEY_REF").Precision	
            '        txtThickness.MaxLength = .Fields("AUTO_KEY_REF").Precision	
            '        txtWidth.MaxLength = .Fields("AUTO_KEY_REF").Precision	
            '        txtLength.MaxLength = .Fields("AUTO_KEY_REF").Precision	

            txtProdDate.MaxLength = 10
            txtRefTM.MaxLength = 5
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetWeightPerStrip(ByRef mRMThick As Double, ByRef mRMLenth As Double, ByRef mRMWidth As Double, ByRef mDensity As Double) As Double
        On Error GoTo ErrPart

        'Dim mRMDiaMeter As Double	
        Dim mWtPerStrip As Double

        GetWeightPerStrip = 0


        If mRMThick <> 0 And mRMLenth <> 0 And mRMWidth <> 0 Then ''Sheet	
            mWtPerStrip = CDbl(VB6.Format(mRMThick * mRMLenth * mRMWidth * mDensity / (1000), "0.000")) ''IN Grams	
        End If

        GetWeightPerStrip = mWtPerStrip * 0.001

        Exit Function
ErrPart:
        GetWeightPerStrip = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub Show1()

        On Error GoTo ERR1
        Dim mProdType As String
        Dim mEntryDate As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mAvailable As Double

        With RsPMemoMain
            If Not .EOF Then
                lblMKey.Text = .Fields("AUTO_KEY_REF").Value
                txtPMemoNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_REF").Value), "", .Fields("AUTO_KEY_REF").Value)
                txtPMemoDate.Text = VB6.Format(IIf(IsDBNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")
                txtProdDate.Text = VB6.Format(IIf(IsDBNull(.Fields("PROD_DATE").Value), "", .Fields("PROD_DATE").Value), "DD/MM/YYYY")

                txtRefTM.Text = VB6.Format(IIf(IsDBNull(.Fields("PREP_TIME").Value), "", .Fields("PREP_TIME").Value), "HH:MM")

                '            txtEntryDate.Text = Format(IIf(IsNull(!ADDDATE), "", !ADDDATE), "DD/MM/YYYY HH:MM")	
                mEntryDate = VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY HH:MM")
                mEntryDate = mEntryDate & vbCrLf & VB6.Format(IIf(IsDBNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY HH:MM")
                txtEntryDate.Text = mEntryDate

                txtDept.Text = IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)
                txtDept_Validating(txtDept, New System.ComponentModel.CancelEventArgs(False))
                cboShiftcd.Text = IIf(IsDBNull(.Fields("SHIFT_CODE").Value), "", .Fields("SHIFT_CODE").Value)

                mProdType = IIf(IsDBNull(.Fields("PROD_TYPE").Value), "P", .Fields("PROD_TYPE").Value)
                If mProdType = "P" Then
                    cboType.SelectedIndex = 0
                Else
                    cboType.SelectedIndex = 1
                End If

                txtEmp.Text = IIf(IsDBNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtEmp_Validating(txtEmp, New System.ComponentModel.CancelEventArgs(False))
                txtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)

                mDivisionCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If
                cboDivision.Enabled = False


                txtRMCode.Text = IIf(IsDBNull(.Fields("RM_CODE").Value), "", .Fields("RM_CODE").Value)
                If MainClass.ValidateWithMasterTable(Trim(txtRMCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblRMCode.Text = Trim(MasterNo)
                End If

                If MainClass.ValidateWithMasterTable(Trim(txtRMCode.Text), "ITEM_CODE", "MAT_DENSITY", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblDensity.Text = Val(MasterNo)
                End If


                lblRMUOM.Text = IIf(IsDBNull(.Fields("RM_UOM").Value), "", .Fields("RM_UOM").Value)
                txtRMQty.Text = VB6.Format(IIf(IsDBNull(.Fields("RM_QTY").Value), "", .Fields("RM_QTY").Value), "0.00")
                txtThickness.Text = VB6.Format(IIf(IsDBNull(.Fields("RM_THICKNESS").Value), "", .Fields("RM_THICKNESS").Value), "0.00")
                txtWidth.Text = VB6.Format(IIf(IsDBNull(.Fields("RM_WIDTH").Value), "", .Fields("RM_WIDTH").Value), "0.00")
                txtLength.Text = VB6.Format(IIf(IsDBNull(.Fields("RM_LENGTH").Value), "", .Fields("RM_LENGTH").Value), "0.00")
                txtNetRMWt.Text = VB6.Format(IIf(IsDBNull(.Fields("RM_NET_WT").Value), "", .Fields("RM_NET_WT").Value), "0.00")
                txtCTLWt.Text = VB6.Format(IIf(IsDBNull(.Fields("CTL_NET_WT").Value), "", .Fields("CTL_NET_WT").Value), "0.00")
                txtNetScrapWt.Text = VB6.Format(IIf(IsDBNull(.Fields("SCRAP_NET_WT").Value), "", .Fields("SCRAP_NET_WT").Value), "0.00")

                mAvailable = GetBalanceStockQty(Trim(txtRMCode.Text), (txtPMemoDate.Text), (lblRMUOM.Text), (txtDept.Text), "ST", "", ConPH, mDivisionCode, ConStockRefType_PMEMODEPT, Val(txtPMemoNo.Text))
                txtStockQty.Text = VB6.Format(mAvailable, "0.00")

                txtBlockCode.Text = IIf(IsDBNull(.Fields("RM_BLOCK_CODE").Value), "", .Fields("RM_BLOCK_CODE").Value)
                If MainClass.ValidateWithMasterTable(Trim(txtBlockCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblBlockDesc.Text = Trim(MasterNo)
                End If

                If MainClass.ValidateWithMasterTable(Trim(txtBlockCode.Text), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblBlockUOM.Text = Trim(MasterNo)
                End If

                txtBlockQty.Text = VB6.Format(IIf(IsDBNull(.Fields("RM_BLOCK_QTY").Value), "", .Fields("RM_BLOCK_QTY").Value), "0.00")

                Call ShowDetail1(mDivisionCode)
                Call CalcTots()
                Call MakeEnableDesableField(False)
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        txtPMemoNo.Enabled = True
        cmdSearch.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Sub
    Private Sub ShowDetail1(ByRef mDivisionCode As Double)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mProdItemCode As String
        Dim mItemUOM As String
        Dim mOKQty As Double

        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PRD_CUTTINGPLAN_DET  " & vbCrLf _
            & " WHERE AUTO_KEY_REF = " & Val(lblMKey.Text) & " " & vbCrLf _
            & " ORDER BY  SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPMemoDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            i = 1
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = i

                SprdMain.Col = ColItemCode
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("SF_CODE").Value), "", .Fields("SF_CODE").Value))
                mProdItemCode = Trim(SprdMain.Text)

                SprdMain.Col = ColItemDesc
                If MainClass.ValidateWithMasterTable(Trim(mProdItemCode), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Text = Trim(MasterNo)
                End If

                SprdMain.Col = ColUom
                SprdMain.Text = IIf(IsDBNull(.Fields("SF_UOM").Value), "", .Fields("SF_UOM").Value)
                mItemUOM = Trim(SprdMain.Text)


                SprdMain.Col = ColThickness
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SF_THICKNESS").Value), "", .Fields("SF_THICKNESS").Value)))

                SprdMain.Col = ColWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SF_WIDTH").Value), "", .Fields("SF_WIDTH").Value)))

                SprdMain.Col = ColLength
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SFLENGTH").Value), "", .Fields("SFLENGTH").Value)))

                '            SprdMain.Col = ColDensity	
                '            If MainClass.ValidateWithMasterTable(Trim(mProdItemCode), "ITEM_CODE", "MAT_DENSITY", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then	
                '                SprdMain.Text = Val(MasterNo)	
                '            End If	

                SprdMain.Col = ColWt_Per_No
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SF_WT_PER_UNIT").Value), "", .Fields("SF_WT_PER_UNIT").Value)))

                SprdMain.Col = ColProdQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SF_QTY").Value), "", .Fields("SF_QTY").Value)))

                SprdMain.Col = ColScrapQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SCRAP_QTY").Value), "", .Fields("SCRAP_QTY").Value)))

                mOKQty = Val(IIf(IsDBNull(.Fields("SF_QTY").Value), 0, .Fields("SF_QTY").Value)) - Val(IIf(IsDBNull(.Fields("SCRAP_QTY").Value), 0, .Fields("SCRAP_QTY").Value))

                SprdMain.Col = ColOkQty
                SprdMain.Text = CStr(Val(mOKQty))

                SprdMain.Col = ColTotalWeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SF_NET_WT").Value), "", .Fields("SF_NET_WT").Value)))

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)


                .MoveNext()

                i = i + 1
                SprdMain.MaxRows = i
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume	
    End Sub
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        AdoDCMain.Refresh	
            FormatSprdView()
            SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        lblMKey.Text = ""
        txtPMemoNo.Text = ""
        txtRefTM.Text = GetServerTime()


        txtPMemoDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")
        txtProdDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")

        txtDept.Text = ""
        lblDept.Text = ""
        cboShiftcd.SelectedIndex = 0
        cboType.SelectedIndex = 0
        txtEmp.Text = ""
        lblEmp.Text = ""
        txtRemarks.Text = ""

        txtRMCode.Text = ""
        lblRMCode.Text = ""
        lblRMUOM.Text = ""
        txtRMQty.Text = ""
        txtThickness.Text = ""
        txtWidth.Text = ""
        txtLength.Text = ""
        txtNetRMWt.Text = ""
        txtCTLWt.Text = ""
        txtNetScrapWt.Text = ""
        txtStockQty.Text = ""
        lblDensity.Text = ""

        txtBlockCode.Text = ""
        lblBlockDesc.Text = ""
        lblBlockUOM.Text = ""
        txtBlockQty.Text = ""

        cmdSearchRMCode.Enabled = True
        txtRMCode.Enabled = True

        txtBlockCode.Enabled = True
        cmdBlockSearch.Enabled = True

        cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        txtRMSheetArea.Text = "0.00"
        txtCTLArea.Text = "0.00"

        txtEntryDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime()
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtPMemoDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        txtDept.Enabled = mMode
        CmdSearchDept.Enabled = mMode

        txtRMCode.Enabled = mMode
        cmdSearchRMCode.Enabled = mMode
        txtRMQty.Enabled = mMode
        txtThickness.Enabled = mMode
        txtWidth.Enabled = mMode
        txtLength.Enabled = mMode


    End Sub
    Private Sub FrmPMemoCuttingPlan_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmPMemoCuttingPlan_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Public Sub FrmPMemoCuttingPlan_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(10935)
        Call FillCbo()
        AdoDCMain.Visible = False
        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        Dim mRow As Short
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String

        mCol = SprdMain.ActiveCol
        mRow = SprdMain.ActiveRow

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))


    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        '    With SprdMain	
        '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False	
        '    End With	

    End Sub

    Private Sub txtEntryDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEntryDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtPMemoDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPMemoDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPMemoDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPMemoDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtPMemoDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtPMemoDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If

        If Trim(cboShiftcd.Text) = "C" Then
            txtProdDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"))))
            txtProdDate.Text = VB6.Format(txtProdDate.Text, "DD/MM/YYYY")
        Else
            txtProdDate.Text = VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDept_Click(CmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblDept.Text = MasterNo
        Else
            MsgInformation("Invalid Depatment Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmp.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmp_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmp.DoubleClick
        Call cmdSearchEmp_Click(cmdSearchEmp, New System.EventArgs())
    End Sub

    Private Sub txtEmp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmp.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmp.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmp_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmp.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchEmp_Click(cmdSearchEmp, New System.EventArgs())
    End Sub

    Private Sub txtEmp_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmp.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtEmp.Text) = "" Then GoTo EventExitSub

        If ADDMode = True Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_LEAVE_DATE IS NULL "
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        txtEmp.Text = VB6.Format(txtEmp.Text, "000000")
        If MainClass.ValidateWithMasterTable(txtEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblEmp.Text = MasterNo
        Else
            MsgInformation("Invalid Employee Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPMemoNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPMemoNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPMemoNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPMemoNo.DoubleClick
        Call cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Private Sub txtPMemoNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPMemoNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPMemoNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPMemoNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPMemoNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Public Sub txtPMemoNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPMemoNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mPMemoNo As Double

        If Trim(txtPMemoNo.Text) = "" Then GoTo EventExitSub

        If Len(txtPMemoNo.Text) < 6 Then
            txtPMemoNo.Text = Val(txtPMemoNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsPMemoMain.EOF = False Then mPMemoNo = RsPMemoMain.Fields("AUTO_KEY_REF").Value

        SqlStr = "Select * From PRD_CUTTINGPLAN_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_REF=" & Val(txtPMemoNo.Text) & "" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsPMemoMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such P.Memo.", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From PRD_CUTTINGPLAN_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_REF=" & Val(CStr(mPMemoNo)) & "" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProdDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProdDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProdDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProdDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtProdDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtProdDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRefTM_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefTM.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRMQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRMQty.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRMQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRMQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRMQty_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRMQty.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mWeightPerStrip As Double
        mWeightPerStrip = GetWeightPerStrip(Val(txtThickness.Text), Val(txtLength.Text), Val(txtWidth.Text), Val(lblDensity.Text))

        txtNetRMWt.Text = VB6.Format(mWeightPerStrip * Val(txtRMQty.Text), "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtStockQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStockQty.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtStockQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtStockQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtThickness_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtThickness.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtThickness_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtThickness.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtThickness_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtThickness.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mWeightPerStrip As Double
        mWeightPerStrip = GetWeightPerStrip(Val(txtThickness.Text), Val(txtLength.Text), Val(txtWidth.Text), Val(lblDensity.Text))

        txtNetRMWt.Text = VB6.Format(mWeightPerStrip * Val(txtRMQty.Text), "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtWidth_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWidth.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtWidth_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWidth.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtWidth_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWidth.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mWeightPerStrip As Double
        mWeightPerStrip = GetWeightPerStrip(Val(txtThickness.Text), Val(txtLength.Text), Val(txtWidth.Text), Val(lblDensity.Text))

        txtNetRMWt.Text = VB6.Format(mWeightPerStrip * Val(txtRMQty.Text), "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBlockQty_Validating(sender As Object, e As CancelEventArgs) Handles txtBlockQty.Validating
        CalcTots()
    End Sub

    Private Sub FrmPMemoCuttingPlan_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame1.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
