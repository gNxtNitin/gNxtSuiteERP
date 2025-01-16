Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmProdIssuRecvNote
    Inherits System.Windows.Forms.Form
    Dim RsIssRecMain As ADODB.Recordset ''Recordset	
    Dim RsIssRecDetail As ADODB.Recordset ''Recordset	
    Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 16

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColUom As Short = 3
    Private Const ColStockType As Short = 4
    Private Const ColBatchNo As Short = 5
    Private Const ColStockQty As Short = 6
    Private Const ColIssueQty As Short = 7
    Private Const ColReceiveQty As Short = 8
    Private Const ColReceivedQty As Short = 9
    Private Const ColOPRCode As Short = 10
    Private Const ColOprDesc As Short = 11
    Private Const ColNextOprCode As Short = 12
    Private Const ColNextOprDesc As Short = 13
    Private Const ColBalIssueQty As Short = 14
    Private Const ColRemarks As Short = 15

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        Dim cntRow As Integer
        Dim mDivisionCode As Double
        Dim xItemCode As String
        Dim xItemUOM As String
        Dim xStockType As String
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Col = ColItemCode
                xItemCode = Trim(.Text)

                SprdMain.Col = ColUom
                xItemUOM = Trim(.Text)

                .Col = ColBatchNo
                If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND DSP_RPT_FLAG='Y'") = True Then
                    mBatchNo = Trim(.Text)
                    xFGBatchNoReq = "Y"
                Else
                    mBatchNo = ""
                    xFGBatchNoReq = "N"
                End If

                SprdMain.Col = ColStockType
                xStockType = Trim(.Text)
                If xStockType = "" Then GoTo NextRow

                .Col = ColStockQty
                .Text = CStr(GetBalanceStockQty(xItemCode, (txtDate.Text), xItemUOM, Trim(txtFromDept.Text), xStockType, mBatchNo, ConPH, mDivisionCode, ConStockRefType_PISS, Val(lblMKey.Text), xFGBatchNoReq))

NextRow:
            Next
        End With

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboShiftcd_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShiftcd.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboShiftcd_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShiftcd.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub



    Private Sub chkReceive_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkReceive.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtIssueNo.Enabled = False
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
    Private Sub FillCboFormType()

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

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
        cboShiftcd.Items.Clear()
        cboShiftcd.Items.Add(("A"))
        cboShiftcd.Items.Add(("B"))
        cboShiftcd.Items.Add(("C"))
        cboShiftcd.SelectedIndex = -1
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
        If ValidateBranchLocking((txtDate.Text)) = True Then
            Exit Sub
        End If
        If Trim(txtIssueNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If chkReceive.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Issue Completed, Cann't be Deleted")
            Exit Sub
        End If
        If Not RsIssRecMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PRD_ISSREC_HDR", (txtIssueNo.Text), RsIssRecMain, "AUTO_KEY_ISSREC") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_ISSREC_HDR", "AUTO_KEY_ISSREC", (lblMKey.Text)) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_PISS, (txtIssueNo.Text)) = False Then GoTo DelErrPart
                If lblBookType.Text = "R" Then
                    PubDBCn.Execute("UPDATE PRD_ISSREC_DET SET RECV_QTY=0 WHERE AUTO_KEY_ISSREC=" & Val(lblMKey.Text) & "")
                    PubDBCn.Execute("UPDATE PRD_ISSREC_HDR SET RECV_STATUS='N',ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') WHERE AUTO_KEY_ISSREC=" & Val(lblMKey.Text) & "")
                Else
                    PubDBCn.Execute("DELETE FROM PRD_ISSREC_DET WHERE AUTO_KEY_ISSREC=" & Val(lblMKey.Text) & "")
                    PubDBCn.Execute("DELETE FROM PRD_ISSREC_HDR WHERE AUTO_KEY_ISSREC=" & Val(lblMKey.Text) & "")
                End If
                PubDBCn.CommitTrans()
                RsIssRecMain.Requery() ''.Refresh	
                RsIssRecDetail.Requery() ''.Refresh	
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''	
        RsIssRecMain.Requery() ''.Refresh	
        RsIssRecDetail.Requery() ''.Refresh	
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr
        If PubSuperUser <> "S" Then
            If chkReceive.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Issue Completed, Cann't be Modified")
                Exit Sub
            End If
        End If
        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsIssRecMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtIssueNo.Enabled = False
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

    Private Sub cmdPopulate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulate.Click

        On Error GoTo ErrorHandler
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim i As Integer
        Dim xItemCode As String
        Dim xItemUOM As String
        Dim mDivisionCode As Double
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        If Trim(txtFromDept.Text) = "" Then Exit Sub
        If Trim(txtDate.Text) = "" Then Exit Sub
        If Not IsDate(txtDate.Text) Then Exit Sub


        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If



        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = " SELECT DISTINCT " & vbCrLf _
            & " IH.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC, INVMST.ISSUE_UOM " & vbCrLf _
            & " FROM PRD_PRODPLAN_MONTH_DET IH, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODe=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND IH.COMPANY_CODe=INVMST.COMPANY_CODe " & vbCrLf _
            & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtFromDept.Text) & "'" & vbCrLf _
            & " AND IH.SERIAL_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        i = 1

        If RsTemp.EOF = False Then
            With SprdMain
                Do While RsTemp.EOF = False
                    .Row = i
                    .Col = ColItemCode
                    xItemCode = Trim(IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                    .Text = xItemCode

                    .Col = ColItemDesc
                    .Text = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))

                    .Col = ColUom
                    xItemUOM = Trim(IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                    .Text = xItemUOM

                    .Col = ColBatchNo
                    If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                        mBatchNo = Trim(.Text)
                        xFGBatchNoReq = "Y"
                    Else
                        mBatchNo = ""
                        xFGBatchNoReq = "N"
                    End If

                    .Col = ColStockType
                    .Text = "ST"

                    .Col = ColStockQty
                    SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtDate.Text), xItemUOM, Trim(txtFromDept.Text), "ST", mBatchNo, ConPH, mDivisionCode, ConStockRefType_PISS, Val(lblMKey.Text), xFGBatchNoReq))

                    .Col = ColIssueQty
                    .Text = "0.00"

                    i = i + 1
                    .MaxRows = i
                    RsTemp.MoveNext()
                Loop
                Call FormatSprdMain(-1)
            End With
        Else
            MsgInformation("No Plan Enter For Such Dept. &  Date")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        cmdPopulate.Enabled = False
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If PubUserID <> "G0416" Then
            If FieldsVarification = False Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
        End If

        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtIssueNo_Validating(txtIssueNo, New System.ComponentModel.CancelEventArgs(False))
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
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND RECV_STATUS='N' AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtIssueNo.Text, "PRD_ISSREC_HDR", "AUTO_KEY_ISSREC", "ISSREC_DATE", "FROM_DEPT", "TO_DEPT || DECODE(COST_CENTER_CODE,NULL,'','-' || COST_CENTER_CODE) AS TO_DEPT", SqlStr) = True Then
            txtIssueNo.Text = AcName
            'txtIssueNo_Validate(False)
            txtIssueNo_Validating(txtIssueNo, New System.ComponentModel.CancelEventArgs(False))
            If txtIssueNo.Enabled = True Then txtIssueNo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdSearchFromDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchFromDept.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", "CCCODE", , SqlStr) = True Then
            txtFromDept.Text = AcName1
            lblFromDept.text = AcName
            If txtFromDept.Enabled = True Then txtFromDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdSearchCC_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCC.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtCost.Text, "FIN_CCENTER_HDR", "CC_CODE", "CC_DESC", , , SqlStr) = True Then
            txtCost.Text = AcName
            lblCostctr.text = AcName1
            If txtCost.Enabled = True Then txtCost.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchIssueEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchIssueEmp.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtIssueEmp.Text = AcName1
            lblIssueEmp.text = AcName
            If txtIssueEmp.Enabled = True Then txtIssueEmp.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchRecvEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchRecvEmp.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtRecvEmp.Text = AcName1
            lblRecvEmp.text = AcName
            If txtRecvEmp.Enabled = True Then txtRecvEmp.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchToDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchToDept.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", "CCCODE", , SqlStr) = True Then
            txtToDept.Text = AcName1
            lblToDept.text = AcName
            If txtToDept.Enabled = True Then txtToDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub FrmProdIssuRecvNote_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub



    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        'Dim xIName As String
        Dim SqlStr As String = ""
        Dim xICode As String
        Dim mUOM As String
        Dim mStockType As String
        Dim mBatchNo As String

        If lblBookType.Text = "R" Then Exit Sub
        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "", "", "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = Trim(AcName)

                    .Col = ColItemDesc
                    .Text = Trim(AcName1)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemDesc
                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "", "", "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = Trim(AcName1)

                    .Col = ColItemDesc
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColBatchNo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                xICode = Trim(.Text)

                .Col = ColUom
                mUOM = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                .Col = ColBatchNo
                mBatchNo = Trim(.Text)

                SqlStr = GetItemBatchWiseQry(xICode, (txtDate.Text), mUOM, Trim(txtFromDept.Text), mStockType, mBatchNo, ConPH, ConStockRefType_PISS, Val(lblMKey.Text))
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColBatchNo
                    .Text = Trim(AcName1)
                End If

                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColBatchNo)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColStockType Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColStockType
                If MainClass.SearchGridMaster(.Text, "INV_TYPE_MST", "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColStockType
                    .Text = AcName
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColStockType, .ActiveRow, ColStockType, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColOPRCode Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColOPRCode
                If MainClass.SearchGridMaster(.Text, "PRD_OPR_MST", "OPR_CODE", "OPR_DESC", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColOPRCode
                    .Text = Trim(AcName)

                    .Col = ColOprDesc
                    .Text = Trim(AcName1)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColOPRCode, .ActiveRow, ColOPRCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColOprDesc Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColOprDesc
                If MainClass.SearchGridMaster("", "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColOPRCode
                    .Text = Trim(AcName1)

                    .Col = ColOprDesc
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColOPRCode, .ActiveRow, ColOPRCode, .ActiveRow, False))
            End With
        End If
        If eventArgs.row = 0 And eventArgs.col = ColNextOprCode Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColNextOprCode
                If MainClass.SearchGridMaster(.Text, "PRD_OPR_MST", "OPR_CODE", "OPR_DESC", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColNextOprCode
                    .Text = Trim(AcName)

                    .Col = ColNextOprDesc
                    .Text = Trim(AcName1)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColNextOprCode, .ActiveRow, ColNextOprCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColNextOprDesc Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColNextOprDesc
                If MainClass.SearchGridMaster("", "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColNextOprCode
                    .Text = Trim(AcName1)

                    .Col = ColNextOprDesc
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColNextOprCode, .ActiveRow, ColNextOprCode, .ActiveRow, False))
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

    Private Function GetItemBatchWiseQry(ByRef pItemCode As String, ByRef pDateTo As String, ByRef pPackUnit As String, ByRef pDeptCode As String, ByRef pStockType As String, ByRef pLotNo As String, ByRef pStock_ID As String, Optional ByRef pRefType As String = "", Optional ByRef pRefNo As Double = 0) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim mBalQty As Double

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIssueUOM As String
        Dim mPurchaseUOM As String
        Dim mFactor As Double
        Dim mTableName As String
        Dim xItemCode As String


        SqlStr = ""

        SqlStr = "SELECT ITEM_CODE, TO_CHAR(CASE WHEN BATCH_NO<='0' OR BATCH_NO=NULL OR BATCH_NO='' THEN '-1' ELSE BATCH_NO END) BATCH_NO, SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY"

        mTableName = ConInventoryTable

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " "

        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & "AND STOCK_ID='" & pStock_ID & "'"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(pItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='" & pDeptCode & "'" ''pDeptCode	

        If pRefType <> "" And Val(CStr(pRefNo)) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE || REF_NO <> '" & pRefType & pRefNo & "'"
        End If

        If pStockType = "QC" Then
            SqlStr = SqlStr & vbCrLf & " AND (STOCK_TYPE='" & pStockType & "' OR E_DATE>TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY'))"
        Else
            If pStockType = "" Then
                SqlStr = SqlStr & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY')"
            Else
                '            SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='" & pStockType & "'"	

                SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='ST' AND E_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY')"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1))<>0"

        SqlStr = SqlStr & vbCrLf & " GROUP BY ITEM_CODE,TO_CHAR(CASE WHEN BATCH_NO<='0' OR BATCH_NO=NULL OR BATCH_NO='' THEN '-1' ELSE BATCH_NO END)"



        GetItemBatchWiseQry = SqlStr

        Exit Function
ErrPart:
        GetItemBatchWiseQry = ""
    End Function



    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mBalQty As Double
        Dim mRecvQty As Double
        Dim xItemCode As String
        Dim xItemDesc As String
        Dim xItemUOM As String
        Dim xStockType As String
        Dim mRecvedQty As Double
        Dim xStockQty As Double
        Dim mIssuedQty As Double
        Dim mDivisionCode As Double
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        If eventArgs.NewRow = -1 Then Exit Sub

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

        If Trim(txtFromDept.Text) = "" Then
            txtFromDept.Focus()
            MsgInformation("From Dept is Blank.")
            Exit Sub
        End If

        If Trim(txtToDept.Text) = "" Then
            txtToDept.Focus()
            MsgInformation("To Dept is Blank.")
            Exit Sub
        End If

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                xItemCode = Trim(SprdMain.Text)
                If FillItemDescPart(xItemCode, True) = True Then
                    If CheckDuplicateItem(xItemCode) = False Then
                        FormatSprdMain(-1)
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColStockType)
                    End If
                End If
            Case ColItemDesc
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                xItemDesc = Trim(SprdMain.Text)
                If FillItemDescPart(xItemDesc, False) = True Then
                    If CheckDuplicateItem(xItemDesc) = False Then
                    End If
                End If
            Case ColIssueQty
                If CheckQty() = True Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColStockQty
                    xStockQty = Val(SprdMain.Text)

                    SprdMain.Col = ColIssueQty
                    If Val(SprdMain.Text) <> 0 Then
                        If xStockQty < Val(SprdMain.Text) Then
                            MsgInformation("You have not enough Stock.")
                            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColIssueQty)
                            eventArgs.cancel = True
                            Exit Sub
                        Else
                            If SprdMain.MaxRows = SprdMain.ActiveRow Then
                                MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                                FormatSprdMain(-1)
                            End If
                        End If
                    End If
                End If
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColReceiveQty
                mRecvQty = Val(SprdMain.Text)

                SprdMain.Col = ColIssueQty
                If Val(SprdMain.Text) <> 0 Then
                    If Val(SprdMain.Text) < mRecvQty Then
                        MsgInformation("Issue Qty Cann't Be Less Than Recveid Qty : " & mRecvQty)
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColIssueQty)
                    End If
                End If
            Case ColReceiveQty
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColBalIssueQty
                mBalQty = Val(SprdMain.Text)

                SprdMain.Col = ColReceivedQty
                mBalQty = mBalQty + Val(SprdMain.Text)


                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColReceiveQty
                mRecvQty = Val(SprdMain.Text)

                If mRecvQty > mBalQty Then
                    MsgInformation("Received Qty Cann't Be Greater Than Issue Qty.")
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColReceiveQty)
                    eventArgs.cancel = True
                    Exit Sub
                End If

            Case ColStockType, ColBatchNo
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xItemCode = Trim(SprdMain.Text)

                SprdMain.Col = ColUom
                xItemUOM = Trim(SprdMain.Text)

                SprdMain.Col = ColBatchNo
                If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    mBatchNo = Trim(SprdMain.Text)
                    xFGBatchNoReq = "Y"
                Else
                    mBatchNo = ""
                    xFGBatchNoReq = "N"
                End If

                SprdMain.Col = ColStockType
                xStockType = Trim(SprdMain.Text)
                If xStockType = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(xStockType, "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("InValid Stock Type")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                    eventArgs.cancel = True
                    Exit Sub
                Else
                    If xStockType = "FG" Then
                        MsgInformation("Can't be Selected FG Stock Type")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                        eventArgs.cancel = True
                        Exit Sub
                    End If

                    SprdMain.Col = ColReceiveQty
                    mIssuedQty = Val(SprdMain.Text)

                    SprdMain.Col = ColStockQty
                    SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtDate.Text), xItemUOM, Trim(txtFromDept.Text), xStockType, mBatchNo, ConPH, mDivisionCode, ConStockRefType_PISS, Val(lblMKey.Text), xFGBatchNoReq))
                End If
            Case ColOPRCode
                Call CheckOperation((SprdMain.ActiveRow), ColOPRCode, ColOprDesc)
            Case ColNextOprCode
                Call CheckOperation((SprdMain.ActiveRow), ColNextOprCode, ColNextOprDesc)
        End Select
        Exit Sub
ErrPart:
        ' Resume	
        MsgBox(Err.Description)
    End Sub
    Private Sub CheckOperation(ByRef pRow As Integer, ByRef pCol As Integer, ByRef pCol1 As Integer)

        With SprdMain
            .Row = pRow
            .Col = pCol
            If Trim(.Text) = "" Then Exit Sub
            If MainClass.ValidateWithMasterTable(.Text, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                .Row = pRow
                .Col = pCol1
                .Text = MasterNo
            Else
                MainClass.SetFocusToCell(SprdMain, pRow, pCol)
            End If
        End With
    End Sub
    Private Function CheckDuplicateItem(ByRef pProdCode As String) As Boolean

        On Error GoTo ERR1
        Dim i As Integer
        Dim xProdCode As String
        Dim mItemRept As Integer

        If pProdCode = "" Then CheckDuplicateItem = False : Exit Function
        With SprdMain
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColItemCode
                xProdCode = .Text

                If UCase(Trim(xProdCode)) = UCase(Trim(pProdCode)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Item's Entry")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, .ActiveCol)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        CheckDuplicateItem = False
        MsgInformation(Err.Description)
    End Function

    Private Function CheckQty() As Boolean

        On Error GoTo ERR1

        CheckQty = True
        Exit Function

        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColIssueQty
            If Val(.Text) > 0 Then
                CheckQty = True
            Else
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColIssueQty)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FillItemDescPart(ByRef pItemCode As String, ByRef pIsItemCode As Boolean) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(pItemCode) = "" Then Exit Function
        SqlStr = "SELECT ITEM_CODE, ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If pIsItemCode = True Then
            SqlStr = SqlStr & vbCrLf & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND LTRIM(RTRIM(ITEM_SHORT_DESC))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                .Text = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                .Col = ColItemDesc
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColUom
                .Text = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

                .Col = ColStockType
                .Text = IIf(Trim(.Text) = "", "ST", Trim(.Text))

                FillItemDescPart = True
            End With
        Else
            FillItemDescPart = False
            If pIsItemCode = True Then
                MsgInformation("Invalid Item Code")
            Else
                MsgInformation("Invalid Item Description")
            End If
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
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
            .Row = eventArgs.Row
            .Col = 1
            txtIssueNo.Text = .Text
            txtIssueNo_Validating(txtIssueNo, New System.ComponentModel.CancelEventArgs(False))
            If txtIssueNo.Enabled = True Then txtIssueNo.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Double
        Dim SqlStr As String = ""
        Dim mMaxValue As String
        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_ISSREC)  " & vbCrLf & " FROM PRD_ISSREC_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

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
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mIssueNo As Double
        Dim mStatus As String
        Dim mEntryDate As String
        Dim mDivisionCode As Double


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime
        mStatus = IIf(chkReceive.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIssueNo = Val(txtIssueNo.Text)
        If Val(txtIssueNo.Text) = 0 Then
            mIssueNo = AutoGenKeyNo()
        End If
        txtIssueNo.Text = CStr(mIssueNo)
        SqlStr = ""
        If ADDMode = True Then
            lblMKey.Text = CStr(mIssueNo)
            SqlStr = "INSERT INTO PRD_ISSREC_HDR (" & vbCrLf _
                & " COMPANY_CODE,FYEAR,AUTO_KEY_ISSREC, " & vbCrLf _
                & " ISSREC_DATE, PREP_TIME, FROM_DEPT,TO_DEPT, " & vbCrLf _
                & " ISS_EMP_CODE,RECV_EMP_CODE, " & vbCrLf _
                & " COST_CENTER_CODE,SHIFT_CODE,RECV_STATUS, " & vbCrLf _
                & " REMARKS,AUTO_KEY_ISS, AUTO_KEY_CUT," & vbCrLf _
                & " ADDUSER,ADDDATE,MODUSER,MODDATE, DIV_CODE)" & vbCrLf _
                & " VALUES( " & vbCrLf _
                & " " & RsCompany.Fields("Company_Code").Value & "," & vbCrLf _
                & " " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf _
                & " " & Val(mIssueNo) & ",TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & txtRefTM.Text & "','HH24:MI'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtFromDept.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtToDept.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtIssueEmp.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRecvEmp.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtCost.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(cboShiftcd.Text) & "', " & vbCrLf _
                & " '" & mStatus & "','" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & Val(txtPSNo.Text) & ",  " & Val(txtCPNo.Text) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'','', " & mDivisionCode & ")"
        ElseIf MODIFYMode = True Then
            SqlStr = "UPDATE PRD_ISSREC_HDR SET " & vbCrLf _
                & " AUTO_KEY_ISSREC=" & Val(mIssueNo) & ", " & vbCrLf _
                & " ISSREC_DATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " PREP_TIME=TO_DATE('" & txtRefTM.Text & "','HH24:MI')," & vbCrLf _
                & " FROM_DEPT='" & MainClass.AllowSingleQuote(txtFromDept.Text) & "', " & vbCrLf _
                & " TO_DEPT='" & MainClass.AllowSingleQuote(txtToDept.Text) & "', " & vbCrLf _
                & " ISS_EMP_CODE='" & MainClass.AllowSingleQuote(txtIssueEmp.Text) & "', " & vbCrLf _
                & " RECV_EMP_CODE='" & MainClass.AllowSingleQuote(txtRecvEmp.Text) & "', " & vbCrLf _
                & " COST_CENTER_CODE='" & MainClass.AllowSingleQuote(txtCost.Text) & "', " & vbCrLf _
                & " SHIFT_CODE='" & MainClass.AllowSingleQuote(cboShiftcd.Text) & "', " & vbCrLf _
                & " RECV_STATUS='" & mStatus & "', DIV_CODE=" & mDivisionCode & "," & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "',AUTO_KEY_ISS=" & Val(txtPSNo.Text) & ", AUTO_KEY_CUT=" & Val(txtCPNo.Text) & "," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " Moddate=TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                & " AND AUTO_KEY_ISSREC =" & Val(lblMKey.Text) & ""
        End If
        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(mDivisionCode) = False Then GoTo ErrPart

        ''mStatus
        Dim mCloseDate As String

        mCloseDate = DateAdd(DateInterval.Day, -2, PubCurrDate)

        SqlStr = "UPDATE PRD_ISSREC_HDR SET " & vbCrLf _
                & " RECV_STATUS='Y'" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                & " AND RECV_STATUS='N' AND ISSREC_DATE<TO_DATE('" & VB6.Format(mCloseDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND FROM_DEPT='" & MainClass.AllowSingleQuote(txtFromDept.Text) & "'"

        PubDBCn.Execute(SqlStr)

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''	
        If ADDMode = True Then
            txtIssueNo.Text = ""
        End If
        RsIssRecMain.Requery() ''.Refresh	
        RsIssRecDetail.Requery() ''.Refresh	
        If Err.Description = "" Then Exit Function
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateDetail1(ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mStockSerialNo As Integer
        Dim mItemCode As String
        Dim mUOM As String
        Dim mStockType As String
        Dim mIssueQty As Double
        Dim mRecvQty As Double
        Dim mOPRCode As String
        Dim mNextOprCode As String
        Dim mRemarks As String
        Dim mOutCCCode As String
        Dim mInCCCode As String
        Dim mER1CategoryCode As String
        Dim mItemCategoryCode As String

        Dim mBOMSql As String
        Dim RsBOM As ADODB.Recordset
        Dim mProductType As String
        Dim xFGBatchNo As String
        Dim mProductSeqNo As Long
        Dim mFinalProdSeq As Long

        If MainClass.ValidateWithMasterTable(txtFromDept.Text, "DEPT_CODE", "CCCODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            mOutCCCode = IIf(IsDBNull(MasterNo), "-1", MasterNo)
        Else
            mOutCCCode = "-1"
        End If

        If MainClass.ValidateWithMasterTable(txtToDept.Text, "DEPT_CODE", "CCCODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            mInCCCode = IIf(IsDBNull(MasterNo), "-1", MasterNo)
        Else
            mInCCCode = "-1"
        End If


        SqlStr = " DELETE FROM PRD_ISSREC_DET " & vbCrLf & " WHERE AUTO_KEY_ISSREC=" & Val(lblMKey.Text) & ""
        PubDBCn.Execute(SqlStr)

        '    If lblBookType.text = "R" Then	
        If DeleteStockTRN(PubDBCn, ConStockRefType_PISS, CStr(Val(lblMKey.Text))) = False Then GoTo UpdateDetail1Err
        '    End If	

        mStockSerialNo = 0
        mER1CategoryCode = GetER1CategoryCode
        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUom
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColStockType
                mStockType = MainClass.AllowSingleQuote(.Text)

                .Col = ColIssueQty
                mIssueQty = Val(.Text)

                .Col = ColReceiveQty
                mRecvQty = Val(.Text)

                .Col = ColOPRCode
                mOPRCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColNextOprCode
                mNextOprCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)
                SqlStr = ""

                If mItemCode <> "" And mIssueQty > 0 Then

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And DSP_RPT_FLAG='Y'") = True Then
                        .Col = ColBatchNo
                        If Trim(.Text) = "0" Then
                            xFGBatchNo = ""
                        Else
                            xFGBatchNo = Trim(.Text)
                        End If
                    Else
                        xFGBatchNo = ""
                    End If

                    SqlStr = " INSERT INTO PRD_ISSREC_DET ( " & vbCrLf _
                        & " COMPANY_CODE,AUTO_KEY_ISSREC,SERIAL_NO,ITEM_CODE,ITEM_UOM,FROM_STOCK_TYPE, BATCH_NO," & vbCrLf _
                        & " ISSUE_QTY,RECV_QTY,OPR_CODE,NEXTOPR_CODE,REMARKS,AUTO_KEY_ISS,AUTO_KEY_CUT) " & vbCrLf _
                        & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMKey.Text) & ", " & i & "," & vbCrLf _
                        & " '" & mItemCode & "','" & mUOM & "','" & mStockType & "', '" & xFGBatchNo & "'," & vbCrLf _
                        & " " & mIssueQty & "," & mRecvQty & ",'" & mOPRCode & "', " & vbCrLf _
                        & " '" & mNextOprCode & "','" & mRemarks & "'," & Val(txtPSNo.Text) & "," & Val(txtCPNo.Text) & ") "

                    PubDBCn.Execute(SqlStr)

                    '                If lblBookType.text = "R" And mRecvQty > 0 Then	
                    If mRecvQty > 0 Then
                        mStockSerialNo = mStockSerialNo + 1

                        If UpdateStockTRN(PubDBCn, ConStockRefType_PISS, (txtIssueNo.Text), mStockSerialNo, (txtDate.Text), (txtDate.Text), mStockType, mItemCode, mUOM, xFGBatchNo, mRecvQty, 0, "O", 0, 0, mOPRCode, mNextOprCode, (txtFromDept.Text), (txtFromDept.Text), mOutCCCode, "N", "From : " & lblFromDept.Text & " To : " & lblToDept.Text & " -" & ConStockRefType_PISS, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err


                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CATEGORY_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mItemCategoryCode = MasterNo
                        Else
                            mItemCategoryCode = ""
                        End If

                        If lblBookType.Text = "J" Then    '' GetProductionType(mItemCode) = "J" Then
                            mStockType = "CS"
                        Else
                            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                                mProductSeqNo = GetProductSeqNo(mItemCode, Trim(txtFromDept.Text), (txtDate.Text))
                            Else
                                mProductSeqNo = GetProductSeqNo(mItemCode, Trim(txtToDept.Text), (txtDate.Text))
                            End If

                            If mProductSeqNo <= 1 Then
                                mStockType = "ST"
                            Else
                                mFinalProdSeq = GetMaxProductSeqNo(mItemCode, (txtDate.Text))
                                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then

                                    If mProductSeqNo = mFinalProdSeq Then
                                        'mStockType = "ST"
                                        If InStr(1, mER1CategoryCode, mItemCategoryCode) > 0 Then
                                            If Trim(txtToDept.Text) = "STR" Then
                                                mStockType = "ST"
                                            Else
                                                mStockType = "WP"
                                            End If
                                        Else
                                            mStockType = "ST"
                                        End If
                                    ElseIf mProductSeqNo > 1 Then
                                        mStockType = "WP"
                                        'Else
                                        '    If InStr(1, mER1CategoryCode, mItemCategoryCode) > 0 Then
                                        '        If Trim(txtToDept.Text) = "STR" Then
                                        '            mStockType = "ST"
                                        '        Else
                                        '            mStockType = "WP"
                                        '        End If
                                        '    Else
                                        '        mStockType = "ST"
                                        '    End If
                                    End If
                                Else
                                    If InStr(1, mER1CategoryCode, mItemCategoryCode) > 0 Then
                                        If Trim(txtToDept.Text) = "STR" Or mProductSeqNo = mFinalProdSeq Then
                                            mStockType = "ST"
                                        Else
                                            mStockType = "WP"
                                        End If
                                    Else
                                        mStockType = "ST"
                                    End If
                                End If

                            End If
                            'If mItemCategoryCode = "" Then
                            '    mStockType = "ST"
                            'Else
                            '    If InStr(1, mER1CategoryCode, mItemCategoryCode) > 0 Then
                            '        If Trim(txtToDept.Text) = "STR" Then
                            '            mStockType = "ST"
                            '        Else
                            '            mStockType = "WP"
                            '        End If
                            '    Else
                            '        If MainClass.ValidateWithMasterTable(mItemCategoryCode, "GEN_CODE", "STOCKTYPE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C' AND PRD_TYPE='J'") = True Then
                            '            If Trim(txtToDept.Text) = "STR" Or Trim(txtToDept.Text) = "PAD" Then
                            '                mStockType = "CS"
                            '                ''22-02-2010	
                            '                '                                    If MasterNo = "CS" Then	
                            '                '                                        mStockType = "CS"	
                            '                '                                    Else	
                            '                '                                        mStockType = "WP"	
                            '                '                                    End If	
                            '            Else
                            '                mStockType = "WP"
                            '            End If
                            '        Else
                            '            mStockType = "ST"
                            '        End If
                            '    End If
                            'End If
                        End If

                        mStockSerialNo = mStockSerialNo + 1
                        If UpdateStockTRN(PubDBCn, ConStockRefType_PISS, (txtIssueNo.Text), mStockSerialNo, (txtDate.Text), (txtDate.Text), mStockType, mItemCode, mUOM, xFGBatchNo, mRecvQty, 0, "I", 0, 0, mOPRCode, mNextOprCode, (txtToDept.Text), (txtToDept.Text), mInCCCode, "N", "From : " & lblFromDept.Text & " To : " & lblToDept.Text & " -" & ConStockRefType_PISS, "-1", IIf(Trim(txtToDept.Text) = "STR" Or Trim(txtToDept.Text) = "PAD", ConWH, ConPH), mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
                    End If
                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Function
    Private Function UpdateBOMStock(ByRef pRsBOM As ADODB.Recordset, ByRef mSFQty As Double, ByRef pRefNo As String, ByRef pRefDate As String, ByRef pOperationCode As String, ByRef pNextOperationCode As String, ByRef mStockSerialNo As Integer) As Boolean
        On Error GoTo BOMStockErr
        'Dim SqlStr As String=""=""	
        'Dim mStdQty As Double	
        'Dim mRMGrossQtyGram As Double	
        'Dim mRMGrossQtyKg As Double	
        'Dim mRMCostKg As Double	
        'Dim mScrpGrossQtyGram As Double	
        'Dim mScrpGrossQtyKg As Double	
        'Dim mScrpCostKg As Double	
        'Dim mSUOM As String	
        'Dim mRetItemCost As Double	
        '	
        '    With pRsBOM	
        '        If Not .EOF Then	
        '            mRetItemCost = Val(IIf(IsNull(!FINAL_COST), "", !FINAL_COST))	
        '            Do While Not .EOF	
        '                mStdQty = Val(IIf(IsNull(!STD_QTY), "", !STD_QTY))	
        '	
        '                If UCase(Trim(!RM_TYPE)) = "RECTANGLE" Or UCase(Trim(!RM_TYPE)) = "ROUND" Then	
        '                    mRMGrossQtyGram = Val(IIf(IsNull(!GROSS_WT_PCS), "", !GROSS_WT_PCS))	
        '                    mRMGrossQtyKg = Val(mRMGrossQtyGram / 1000)	
        '                    mRMGrossQtyKg = Val(mRMGrossQtyKg * mStdQty * mSFQty)	
        '                    mRMCostKg = Val(IIf(IsNull(!COST_PCS), "", !COST_PCS))	
        '	
        '                    mScrpGrossQtyGram = Val(IIf(IsNull(!GROSS_WT_SCRAP), "", !GROSS_WT_SCRAP))	
        '                    mScrpGrossQtyKg = Val(mScrpGrossQtyGram / 1000)	
        '                    mScrpGrossQtyKg = Val(mScrpGrossQtyKg * mStdQty * mSFQty)	
        '                    mScrpCostKg = Val(IIf(IsNull(!COST_SCRAP), "", !COST_SCRAP))	
        '                ElseIf UCase(Trim(!RM_TYPE)) = "OTHER" Then	
        '                    mRMGrossQtyGram = mStdQty	
        '                    mRMGrossQtyKg = mStdQty	
        '                    mRMGrossQtyKg = Val(mStdQty * mSFQty)	
        '                    mRMCostKg = Val(IIf(IsNull(!COST_PCS), "", !COST_PCS))	
        '	
        '                    mScrpGrossQtyGram = Val(IIf(IsNull(!GROSS_WT_SCRAP), "", !GROSS_WT_SCRAP))	
        '                    mScrpGrossQtyKg = Val(mScrpGrossQtyGram)	
        '                    mScrpGrossQtyKg = Val(mScrpGrossQtyKg * mSFQty)	
        '                    mScrpCostKg = Val(IIf(IsNull(!COST_SCRAP), "", !COST_SCRAP))	
        '                End If	
        '	
        '                If mRMGrossQtyKg > 0 Then   'UPDATING RAW MATERIAL INVENTORY	
        '                    mStockSerialNo = mStockSerialNo + 1	
        '                    If UpdateStockTRN(PubDBCn, ConStockRefType_PISS, pRefNo, mStockSerialNo, pRefDate, pRefDate, _	
        ''                                    "ST", !RM_CODE, IIf(IsNull(!IUOM), "", !IUOM), -1, mRMGrossQtyKg, 0, "O", mRMCostKg, mRMCostKg, pOperationCode, pNextOperationCode, txtFromDept.Text, txtFromDept.Text, txtCost.Text, "N", "From : " & lblFromDept.text & " To : " & lblToDept.text & " -" & ConStockRefType_PISS, "-1", ConPH) = False Then GoTo BOMStockErr	
        '                End If	
        '	
        '                If mScrpGrossQtyKg > 0 Then     'UPDATING SCRAP INVENTORY	
        '                    If Not IsNull(!SCRAP_ITEM_CODE) Then	
        '                        If MainClass.ValidateWithMasterTable(!SCRAP_ITEM_CODE, "SCRAP_ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " ") = True Then	
        '                            mSUOM = IIf(IsNull(MasterNo), "", MasterNo)	
        '                            mStockSerialNo = mStockSerialNo + 1	
        '                            If UpdateStockTRN(PubDBCn, ConStockRefType_PISS, pRefNo, mStockSerialNo, pRefDate, pRefDate, _	
        ''                                            "SC", !SCRAP_ITEM_CODE, mSUOM, -1, mScrpGrossQtyKg, 0, "I", mScrpCostKg, mScrpCostKg, "", "", txtToDept.Text, txtFromDept.Text, txtCost.Text, "N", "From : " & lblFromDept.text & " To : " & lblToDept.text & " -" & ConStockRefType_PISS, "-1", ConWH) = False Then GoTo BOMStockErr	
        '                        End If	
        '                    End If	
        '                End If	
        '                pRsBOM.MoveNext	
        '            Loop	
        '        End If	
        '    End With	
        UpdateBOMStock = True
        Exit Function
BOMStockErr:
        UpdateBOMStock = False
        If Err.Description <> "" Then MsgBox(Err.Description)
        'Resume	
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim mDeptCode As String
        Dim mCheckLastEntryDate As String
        Dim xAutoProdIssue As Boolean
        Dim mItemCode As String
        Dim mCheckProdType As String
        Dim xFGBatchNoReq As String

        FieldsVarification = True
        If ValidateBranchLocking((txtDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsIssRecMain.EOF = True Then Exit Function

        If lblBookType.Text = "R" Then
            If txtIssueNo.Text = "" Then
                MsgInformation("Issue No. Cann't Be Blank")
                FieldsVarification = False
                Exit Function
            End If
        Else
            If MODIFYMode = True And txtIssueNo.Text = "" Then
                MsgInformation("Issue No. Cann't Be Blank")
                FieldsVarification = False
                Exit Function
            End If
        End If
        If txtDate.Text = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDate.Focus()
            Exit Function
        ElseIf FYChk((txtDate.Text)) = False Then
            FieldsVarification = False
            If txtDate.Enabled = True Then txtDate.Focus()
            Exit Function
        End If

        If CDate(txtDate.Text) > CDate(PubCurrDate) Then
            MsgBox("Issue Date Cann't be Greater than Current Date", MsgBoxStyle.Information)
            FieldsVarification = False
            'txtProdDate.Focus()
            Exit Function
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If

        If lblBookType.Text = "I" Then
            If Trim(txtIssueEmp.Text) = "" Then
                MsgBox("Issued Emp Is Blank", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtIssueEmp.Enabled = True Then txtIssueEmp.Focus()
                Exit Function
            End If
        Else
            If Trim(txtRecvEmp.Text) = "" Then
                MsgBox("Received Emp Is Blank", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtRecvEmp.Enabled = True Then txtRecvEmp.Focus()
                Exit Function
            End If
        End If

        If Trim(txtFromDept.Text) = Trim(txtToDept.Text) Then
            MsgBox("'FROM DEPT' & 'TO DEPT' Cann't be same.", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtFromDept.Enabled = True Then txtFromDept.Focus()
            Exit Function
        End If

        If lblBookType.Text = "I" Then
            If chkReceive.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgBox("Issue Completed. Cann't be Change.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        '    If lblBookType.text = "I" Then	
        If PubSuperUser <> "S" Then
            mCheckLastEntryDate = GetLastEntryDate()
            mCheckLastEntryDate = DateAdd("d", -1, mCheckLastEntryDate)
            If mCheckLastEntryDate <> "" Then
                If CDate(txtDate.Text) < CDate(mCheckLastEntryDate) Then
                    MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If
        '    End If	

        If lblBookType.Text = "R" Then
            If CheckBalIssueQty() = True Then
                chkReceive.CheckState = System.Windows.Forms.CheckState.Checked
            Else
                chkReceive.CheckState = System.Windows.Forms.CheckState.Unchecked
            End If

            If CheckStockQty(SprdMain, ColStockQty, ColReceiveQty, ColItemCode, ColStockType, True, True) = False Then
                FieldsVarification = False
                Exit Function
            End If

            If ValidateDeptRight(PubUserID, Trim(txtToDept.Text), UCase(Trim(lblToDept.Text))) = False Then
                FieldsVarification = False
                Exit Function
            End If

            '        If PubSuperUser = "U" Then	
            '            If MainClass.ValidateWithMasterTable(PubUserEMPCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then	
            '                mDeptCode = MasterNo	
            '                If UCase(Trim(txtToDept.Text)) <> UCase(Trim(mDeptCode)) Then	
            '                    MsgBox "You Are Not in 'TO Dept'.", vbInformation	
            '                    FieldsVarification = False	
            '                End If	
            '            Else	
            '                MsgBox "Invalid Recd. Emp Code.", vbInformation	
            '                FieldsVarification = False	
            '            End If	
            '        End If	
        End If

        If lblBookType.Text = "I" Then
            If CheckStockQty(SprdMain, ColStockQty, ColIssueQty, ColItemCode, ColStockType, True, True) = False Then
                FieldsVarification = False
                Exit Function
            End If

            If ValidateDeptRight(PubUserID, Trim(txtFromDept.Text), UCase(Trim(lblFromDept.Text))) = False Then
                FieldsVarification = False
                Exit Function
            End If
            '	
            '        If PubSuperUser = "U" Then	
            '            If MainClass.ValidateWithMasterTable(PubUserEMPCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then	
            '                mDeptCode = MasterNo	
            '                If UCase(Trim(txtFromDept.Text)) <> UCase(Trim(mDeptCode)) Then	
            '                    MsgBox "You Are Not in 'From Dept'.", vbInformation	
            '                    FieldsVarification = False	
            '                End If	
            '            Else	
            '                MsgBox "Invalid Issue Emp Code.", vbInformation	
            '                FieldsVarification = False	
            '            End If	
            '        End If	

            If CheckRowCount() = False Then
                FieldsVarification = False
                Exit Function
            End If
        End If

        With SprdMain
            For mRow = 1 To .MaxRows
                .Row = mRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND DSP_RPT_FLAG='Y'") = True Then
                    xFGBatchNoReq = "Y"
                Else
                    xFGBatchNoReq = "N"
                End If

                .Col = ColBatchNo
                If xFGBatchNoReq = "Y" Then
                    If Trim(.Text) = "" Or Trim(.Text) = "0" Then
                        MsgBox("Invalid or Blank Batch No.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            Next
        End With

        xAutoProdIssue = CheckAutoIssueProd((txtDate.Text), "")

        If xAutoProdIssue = True Then
            With SprdMain
                For mRow = 1 To .MaxRows
                    .Row = mRow
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)

                    mCheckProdType = GetProductionType(mItemCode)
                    ''23-04-2011	
                    '                If mCheckProdType = "P" Then        ''And IsProductionItem(mItemCode) = True	
                    If IsProductionItem(mItemCode) = True Then
                        If Trim(txtToDept.Text) <> "STR" Then
                            MsgBox("Auto Issue is Defined, so Cann't be make Issue Note.", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                Next
            End With


        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColIssueQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False: Exit Function	
        '    If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False: Exit Function	

        If lblBookType.Text = "I" Then
            '        If MainClass.ValidDataInGrid(SprdMain, ColOPRCode, "S", "Operation Is Blank.") = False Then FieldsVarification = False: Exit Function	
            '        If MainClass.ValidDataInGrid(SprdMain, ColNextOprCode, "S", "Next Operation Is Blank.") = False Then FieldsVarification = False: Exit Function	
        Else
            '        If MainClass.ValidDataInGrid(SprdMain, ColReceiveQty, "N", "Please Check Receive Qty.") = False Then FieldsVarification = False: Exit Function	
        End If
        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Function
    Private Function GetLastEntryDate() As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = ""
        GetLastEntryDate = ""
        SqlStr = "SELECT Max(ISSREC_DATE) AS  ISSREC_DATE " & vbCrLf & " FROM PRD_ISSREC_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " and RECV_STATUS='Y'"

        '    If lblBookType.text = "I" Then	
        SqlStr = SqlStr & vbCrLf & " AND FROM_DEPT='" & VB6.Format(txtFromDept.Text, "DD-MMM-YYYY") & "'"
        SqlStr = SqlStr & vbCrLf & " AND TO_DEPT='" & VB6.Format(txtToDept.Text, "DD-MMM-YYYY") & "'"
        '    End If	

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetLastEntryDate = IIf(IsDbNull(RsTemp.Fields("ISSREC_DATE").Value), "", RsTemp.Fields("ISSREC_DATE").Value)
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
        Dim mProductSeqNo As Integer
        Dim mFromProductSeqNo As Integer


        mRowCount = 0
        mTotQty = 0
        CheckRowCount = True
        With SprdMain
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColIssueQty
                mQty = Val(.Text)

                If mItemCode <> "" And mQty > 0 Then
                    mFromProductSeqNo = GetProductSeqNo(mItemCode, Trim(txtFromDept.Text), (txtDate.Text))
                    mProductSeqNo = GetProductSeqNo(mItemCode, Trim(txtToDept.Text), (txtDate.Text))
                    ''Temp Lock
                    'If mProductSeqNo = 0 Then
                    '    '                    If MsgQuestion("Either Production Sequence not defined Or not in " & Trim(txtToDept.Text) & " Dept, for Item Code : " & mItemCode & ". Are You Want to Continue ...") = vbNo Then	
                    '    MsgInformation("Either Production Sequence not defined Or not in " & Trim(txtToDept.Text) & " Dept." & vbCrLf & "Item Code : " & mItemCode & ". Cann't be Saved ...")
                    '    CheckRowCount = False
                    '    txtFromDept.Focus()
                    '    Exit Function
                    '    '                    End If	
                    'End If

                    'If mFromProductSeqNo + 1 <> mProductSeqNo Then
                    '    MsgInformation(Trim(txtToDept.Text) & " Dept is Not a Next Production Sequence of " & vbCrLf & "Item Code : " & mItemCode & ". Cann't be Saved ...")
                    '    CheckRowCount = False
                    '    txtToDept.Focus()
                    '    Exit Function
                    'End If

                    .Col = ColIssueQty
                    mQty = Val(.Text)

                    mTotQty = mTotQty + mQty

                    If mItemCode <> "" And mQty > 0 Then
                        mRowCount = mRowCount + 1
                    End If
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

    Public Function CheckBalIssueQty() As Boolean
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mIssueQty As Double
        Dim mRecvQty As Double

        CheckBalIssueQty = True
        If chkReceive.CheckState = System.Windows.Forms.CheckState.Checked Then Exit Function

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColIssueQty
                mIssueQty = Val(.Text)

                .Col = ColReceiveQty
                mRecvQty = Val(.Text)

                If mIssueQty <> mRecvQty Then
                    CheckBalIssueQty = False
                    Exit Function
                End If

            Next
        End With
        Exit Function
ErrPart:
        CheckBalIssueQty = False
    End Function

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmProdIssuRecvNote_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If lblBookType.Text = "I" Then
            Me.Text = "Material Movement Issue Note"
        Else
            Me.Text = "Material Movement Received Note"
        End If

        SqlStr = ""
        SqlStr = "Select * from PRD_ISSREC_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIssRecMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_ISSREC_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIssRecDetail, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)
        Call SetTextLengths()

        If lblBookType.Text = "I" Then
            If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        Else
            Clear1()
        End If
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume	
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = " SELECT  AUTO_KEY_ISSREC ISSUE_NO,ISSREC_DATE ,FROM_DEPT , " & vbCrLf & " TO_DEPT,ISS_EMP_CODE,RECV_EMP_CODE, " & vbCrLf & " DECODE(RECV_STATUS,'Y','COMPLETE','PENDING') AS RECV_STATUS, " & vbCrLf & " REMARKS " & vbCrLf & " FROM PRD_ISSREC_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " ORDER BY ISSREC_DATE ,AUTO_KEY_ISSREC,FROM_DEPT , TO_DEPT"

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
            .set_ColWidth(0, 500 * 1)
            .set_ColWidth(1, 500 * 2)
            .set_ColWidth(2, 500 * 2)
            .set_ColWidth(3, 500 * 2)
            .set_ColWidth(4, 500 * 2)
            .set_ColWidth(5, 500 * 2)
            .set_ColWidth(6, 500 * 3)
            .set_ColWidth(7, 500 * 3)
            .set_ColWidth(8, 500 * 4)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .set_RowHeight(0, ConRowHeight * 1.5)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsIssRecDetail.Fields("ITEM_CODE").DefinedSize ''	
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemDesc, 28)

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsIssRecDetail.Fields("ITEM_UOM").DefinedSize
            .set_ColWidth(ColUom, 4)

            .Col = ColBatchNo
            '        .CellType = SS_CELL_TYPE_FLOAT	
            '        .TypeFloatDecimalPlaces = 0	
            '        .TypeFloatDecimalChar = Asc(".")	
            '        .TypeFloatMax = "9999999999"	
            '        .TypeFloatMin = "-9999999999"	
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC	
            '        .ColHidden = False      '' IIf(lblBookType.text = "I", False, True)	
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsIssRecDetail.Fields("BATCH_NO").DefinedSize
            '        .TypeEditMultiLine = False	
            .set_ColWidth(ColBatchNo, 10)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsIssRecDetail.Fields("FROM_STOCK_TYPE").DefinedSize
            .set_ColWidth(ColStockType, 5)

            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999999.9999")
            .TypeFloatMin = CDbl("-9999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = False '' IIf(lblBookType.text = "I", False, True)	
            .set_ColWidth(ColStockQty, 10)

            .Col = ColIssueQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999999.9999")
            .TypeFloatMin = CDbl("-9999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColIssueQty, 10)

            .Col = ColReceiveQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999999.9999")
            .TypeFloatMin = CDbl("-9999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            If lblBookType.Text = "I" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If
            .set_ColWidth(ColReceiveQty, 10)

            .Col = ColReceivedQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999999.9999")
            .TypeFloatMin = CDbl("-9999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = True

            .Col = ColOPRCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsIssRecDetail.Fields("OPR_CODE").DefinedSize ''	
            .set_ColWidth(ColOPRCode, 7)

            .Row = 0
            If lblBookType.Text = "R" Then
                .Text = "Operation Done"
            Else
                .Text = "Operation"
            End If
            .ColHidden = True

            .Row = Arow
            .Col = ColOprDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 255
            .set_ColWidth(ColOprDesc, 7)

            .Row = 0
            If lblBookType.Text = "R" Then
                .Text = "Operation Done"
            Else
                .Text = "Operation"
            End If
            .ColHidden = True

            .Row = Arow
            .Col = ColNextOprCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsIssRecDetail.Fields("NEXTOPR_CODE").DefinedSize ''	
            .set_ColWidth(ColNextOprCode, 7)

            .Row = 0
            If lblBookType.Text = "R" Then
                .Text = "Operation"
            Else
                .Text = "Next Operation"
            End If
            .ColHidden = True

            .Row = Arow
            .Col = ColNextOprDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 255
            .set_ColWidth(ColNextOprDesc, 7)

            .Row = 0
            If lblBookType.Text = "R" Then
                .Text = "Operation"
            Else
                .Text = "Next Operation"
            End If
            .ColHidden = True

            .Row = Arow
            .Col = ColBalIssueQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999999.9999")
            .TypeFloatMin = CDbl("-9999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = True


            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsIssRecDetail.Fields("REMARKS").DefinedSize
            .set_ColWidth(ColRemarks, 12)

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUom)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockQty, ColStockQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColReceivedQty, ColReceivedQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColOprDesc, ColOprDesc)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColNextOprDesc, ColNextOprDesc)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColBalIssueQty, ColBalIssueQty)
        '    MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColStockQty, ColStockQty	

        If lblBookType.Text = "R" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColItemCode)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIssueQty, ColIssueQty)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColOPRCode, ColNextOprDesc)
        End If
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsIssRecDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsIssRecMain
            txtIssueNo.Maxlength = .Fields("AUTO_KEY_ISSREC").Precision
            txtDate.Maxlength = .Fields("ISSREC_DATE").Precision - 6
            txtRefTM.Maxlength = 5
            txtFromDept.Maxlength = .Fields("FROM_DEPT").DefinedSize
            txtToDept.Maxlength = .Fields("TO_DEPT").DefinedSize
            txtIssueEmp.Maxlength = .Fields("ISS_EMP_CODE").DefinedSize
            txtRecvEmp.Maxlength = .Fields("RECV_EMP_CODE").DefinedSize
            txtCost.Maxlength = .Fields("COST_CENTER_CODE").DefinedSize
            txtRemarks.Maxlength = .Fields("REMARKS").DefinedSize
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mEntryDate As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        With RsIssRecMain
            If Not .EOF Then
                lblMKey.Text = .Fields("AUTO_KEY_ISSREC").Value
                txtIssueNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_ISSREC").Value), "", .Fields("AUTO_KEY_ISSREC").Value)
                txtDate.Text = VB6.Format(IIf(IsDbNull(.Fields("ISSREC_DATE").Value), "", .Fields("ISSREC_DATE").Value), "DD/MM/YYYY")
                txtRefTM.Text = VB6.Format(IIf(IsDbNull(.Fields("PREP_TIME").Value), "", .Fields("PREP_TIME").Value), "HH:MM")
                '            txtEntryDate.Text = Format(IIf(IsNull(!ADDDATE), "", !ADDDATE), "DD/MM/YYYY HH:MM")	


                mEntryDate = IIf(IsDbNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value) & " - " & VB6.Format(IIf(IsDbNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY HH:MM")
                mEntryDate = mEntryDate & vbCrLf & IIf(IsDbNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value) & " - " & VB6.Format(IIf(IsDbNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY HH:MM")
                txtEntryDate.Text = mEntryDate

                txtFromDept.Text = IIf(IsDbNull(.Fields("FROM_DEPT").Value), "", .Fields("FROM_DEPT").Value)
                TxtFromDept_Validating(TxtFromDept, New System.ComponentModel.CancelEventArgs(False))
                txtToDept.Text = IIf(IsDbNull(.Fields("TO_DEPT").Value), "", .Fields("TO_DEPT").Value)
                txtToDept_Validating(txtToDept, New System.ComponentModel.CancelEventArgs(False))
                txtIssueEmp.Text = IIf(IsDbNull(.Fields("ISS_EMP_CODE").Value), "", .Fields("ISS_EMP_CODE").Value)
                txtIssueEmp_Validating(txtIssueEmp, New System.ComponentModel.CancelEventArgs(False))
                txtRecvEmp.Text = IIf(IsDbNull(.Fields("RECV_EMP_CODE").Value), "", .Fields("RECV_EMP_CODE").Value)
                txtRecvEmp_Validating(txtRecvEmp, New System.ComponentModel.CancelEventArgs(False))
                txtCost.Text = IIf(IsDbNull(.Fields("COST_CENTER_CODE").Value), "", .Fields("COST_CENTER_CODE").Value)
                txtCost_Validating(txtCost, New System.ComponentModel.CancelEventArgs(False))
                cboShiftcd.Text = IIf(IsDbNull(.Fields("SHIFT_CODE").Value), "", .Fields("SHIFT_CODE").Value)
                chkReceive.CheckState = IIf(.Fields("RECV_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                txtRemarks.Text = IIf(IsDbNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtPSNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_ISS").Value), "", .Fields("AUTO_KEY_ISS").Value)

                txtCPNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_CUT").Value), "", .Fields("AUTO_KEY_CUT").Value)

                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If
                cboDivision.Enabled = False

                Call ShowDetail1(mDivisionCode)
                cmdPopulate.Enabled = False
                txtPSNo.Enabled = False
                txtCPNo.Enabled = False


                Call MakeEnableDesableField(False)


            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsIssRecMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        txtIssueNo.Enabled = True
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
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String
        Dim mIssueQty As Double
        Dim mRecvQty As Double
        Dim mOPRCode As String
        Dim mOPRDesc As String
        Dim mNextOprCode As String
        Dim mNextOprDesc As String
        Dim mStkType As String
        Dim mRemarks As String
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM PRD_ISSREC_DET  " & vbCrLf & " WHERE AUTO_KEY_ISSREC = " & Val(lblMKey.Text) & " " & vbCrLf & " ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIssRecDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsIssRecDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            i = 1
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = i

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemDesc
                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemDesc = MasterNo
                    SprdMain.Text = mItemDesc
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColUom
                mItemUOM = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                SprdMain.Text = mItemUOM

                SprdMain.Col = ColBatchNo
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    mBatchNo = Trim(IIf(IsDbNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value))
                    SprdMain.Text = IIf(mBatchNo > "0", mBatchNo, IIf(mBatchNo = "-1", mBatchNo, ""))
                    xFGBatchNoReq = "Y"
                Else
                    mBatchNo = "X"
                    SprdMain.Text = ""
                    xFGBatchNoReq = "N"
                End If

                SprdMain.Col = ColStockType
                mStkType = IIf(IsDbNull(.Fields("FROM_STOCK_TYPE").Value), "", .Fields("FROM_STOCK_TYPE").Value)
                SprdMain.Text = mStkType

                SprdMain.Col = ColIssueQty
                mIssueQty = IIf(IsDbNull(.Fields("ISSUE_QTY").Value), 0, .Fields("ISSUE_QTY").Value)
                SprdMain.Text = CStr(mIssueQty)

                SprdMain.Col = ColReceiveQty
                mRecvQty = IIf(IsDbNull(.Fields("RECV_QTY").Value), 0, .Fields("RECV_QTY").Value)
                SprdMain.Text = CStr(mRecvQty)

                SprdMain.Col = ColReceivedQty
                SprdMain.Text = CStr(mRecvQty)

                SprdMain.Col = ColStockQty ''mRecvQty +	
                SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtDate.Text), mItemUOM, Trim(txtFromDept.Text), mStkType, mBatchNo, ConPH, mDivisionCode, ConStockRefType_PISS, Val(lblMKey.Text), xFGBatchNoReq))

                SprdMain.Col = ColOPRCode
                mOPRCode = IIf(IsDbNull(.Fields("OPR_CODE").Value), "", .Fields("OPR_CODE").Value)
                SprdMain.Text = Trim(mOPRCode)

                SprdMain.Col = ColOprDesc
                If MainClass.ValidateWithMasterTable(mOPRCode, "OPR_Code", "OPR_Desc", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mOPRDesc = MasterNo
                    SprdMain.Text = mOPRDesc
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColNextOprCode
                mNextOprCode = IIf(IsDbNull(.Fields("NEXTOPR_CODE").Value), "", .Fields("NEXTOPR_CODE").Value)
                SprdMain.Text = Trim(mNextOprCode)

                SprdMain.Col = ColNextOprDesc
                If MainClass.ValidateWithMasterTable(mNextOprCode, "OPR_Code", "OPR_Desc", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mNextOprDesc = MasterNo
                    SprdMain.Text = mNextOprDesc
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColBalIssueQty
                SprdMain.Text = CStr(Val(CStr(mIssueQty - mRecvQty)))

                SprdMain.Col = ColRemarks
                mRemarks = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                SprdMain.Text = mRemarks

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


    Private Function ShowFromProdDetail1(ByRef pDeptCode As String) As Boolean

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemCode As String

        ShowFromProdDetail1 = False

        SqlStr = " SELECT * From PRD_COST_MAIN_DET WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND DEPT_CODE='" & pDeptCode & "'" & vbCrLf & " ORDER BY SERIAL_NO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTemp
            If .EOF = True Then Exit Function
            FormatSprdMain(-1)
            i = 1
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = i

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemDesc
                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Text = MasterNo
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColUom
                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "ISSUE_UOM", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Text = MasterNo
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColStockType
                SprdMain.Text = "ST"

                .MoveNext()

                i = i + 1
                SprdMain.MaxRows = i
            Loop
        End With
        FormatSprdMain(-1)
        ShowFromProdDetail1 = True
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ShowFromProdDetail1 = False
        '   Resume	
    End Function
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
        MainClass.ButtonStatus(Me, XRIGHT, RsIssRecMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        lblMKey.Text = ""
        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtEntryDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime
        txtRefTM.Text = GetServerTime
        txtIssueNo.Text = ""
        txtFromDept.Text = ""
        lblFromDept.Text = ""
        txtToDept.Text = ""
        lblToDept.Text = ""
        txtIssueEmp.Text = ""
        lblIssueEmp.Text = ""
        txtRecvEmp.Text = ""
        lblRecvEmp.Text = ""
        txtCost.Text = ""
        lblCostctr.Text = ""
        cboShiftcd.SelectedIndex = 0
        chkReceive.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtRemarks.Text = ""
        txtPSNo.Text = ""
        txtPSNo.Enabled = IIf(lblBookType.Text = "I", True, False)

        txtCPNo.Text = ""
        txtCPNo.Enabled = IIf(lblBookType.Text = "I", True, False)

        cmdPopulate.Enabled = IIf(lblBookType.Text = "I", True, False)

        cboDivision.Text = GetDefaultDivision()             '' -1
        cboDivision.Enabled = True

        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsIssRecMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtDate.Enabled = True  ''IIf(chkReceive.CheckState = System.Windows.Forms.CheckState.Unchecked Or PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        txtFromDept.Enabled = mMode
        cmdSearchFromDept.Enabled = mMode
        txtToDept.Enabled = mMode
        cmdSearchToDept.Enabled = mMode
        If lblBookType.Text = "I" Then
            txtIssueEmp.Enabled = mMode
            cmdSearchIssueEmp.Enabled = mMode
            txtRecvEmp.Enabled = False
            cmdSearchRecvEmp.Enabled = mMode
            chkReceive.Enabled = False
        Else
            txtRecvEmp.Enabled = True
            cmdSearchRecvEmp.Enabled = True
            txtIssueEmp.Enabled = mMode
            cmdSearchIssueEmp.Enabled = mMode
            chkReceive.Enabled = IIf(chkReceive.CheckState = System.Windows.Forms.CheckState.Checked, False, True) ''   mMode	
        End If
        txtCost.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        cmdSearchCC.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
    End Sub
    Private Sub FrmProdIssuRecvNote_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmProdIssuRecvNote_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Public Sub FrmProdIssuRecvNote_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        AdoDCMain.Visible = False
        FillCboFormType()
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
        mCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColStockType Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColStockType, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColOPRCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColOPRCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColOprDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColOprDesc, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColNextOprCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColNextOprCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColNextOprDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColNextOprDesc, 0))
        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        'With SprdMain
        '    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With

    End Sub


    Private Sub txtCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCost_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.DoubleClick
        Call CmdSearchCC_Click(CmdSearchCC, New System.EventArgs())
    End Sub

    Private Sub txtCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCost.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCost_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCost.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtCost_DoubleClick(txtCost, New System.EventArgs())
    End Sub

    Private Sub txtCost_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCost.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtCost.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtCost.Text, "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            lblCostctr.text = MasterNo
        Else
            MsgInformation("Invalid CostC Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtFromDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFromDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then TxtFromDept_DoubleClick(TxtFromDept, New System.EventArgs())
    End Sub

    Private Sub TxtFromDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFromDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtFromDept.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtFromDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            lblFromDept.text = MasterNo

        Else
            MsgInformation("Invalid Depatment Code")
            Cancel = True
            Exit Sub
        End If

        '    If ADDMode = True Then	
        '        If MsgQuestion("Populate Data From Production Master ...") = vbYes Then	
        '            MainClass.ClearGrid SprdMain	
        '            Call FormatSprdMain(-1)	
        '            If ShowFromProdDetail1(txtFromDept.Text) = False Then GoTo ErrPart	
        '            If txtToDept.Enabled = True Then txtToDept.SetFocus	
        '        End If	
        '    End If	
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtIssueEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIssueEmp.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIssueEmp_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIssueEmp.DoubleClick
        Call cmdSearchIssueEmp_Click(cmdSearchIssueEmp, New System.EventArgs())
    End Sub

    Private Sub txtIssueEmp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIssueEmp.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtIssueEmp.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtIssueEmp_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtIssueEmp.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtIssueEmp_DoubleClick(txtIssueEmp, New System.EventArgs())
    End Sub

    Private Sub txtIssueEmp_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIssueEmp.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtIssueEmp.Text) = "" Then GoTo EventExitSub
        txtIssueEmp.Text = VB6.Format(txtIssueEmp.Text, "000000")
        If MainClass.ValidateWithMasterTable(txtIssueEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            lblIssueEmp.text = MasterNo
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


    Private Sub txtIssueNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIssueNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtIssueNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIssueNo.DoubleClick
        Call cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtIssueNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIssueNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtIssueNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtIssueNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtIssueNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Public Sub txtIssueNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIssueNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mIssueNo As Double
        If Trim(txtIssueNo.Text) = "" Then GoTo EventExitSub
        If Len(txtIssueNo.Text) < 6 Then
            txtIssueNo.Text = Val(txtIssueNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If
        If MODIFYMode = True And RsIssRecMain.EOF = False Then mIssueNo = RsIssRecMain.Fields("AUTO_KEY_ISSREC").Value

        SqlStr = "SELECT * FROM PRD_ISSREC_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_ISSREC=" & Val(txtIssueNo.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIssRecMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsIssRecMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Issue No,Click Add For New.", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PRD_ISSREC_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_ISSREC=" & mIssueNo & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIssRecMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPSNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPSNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPSNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPSNo.DoubleClick
        Call SearchPSNo()
    End Sub

    Private Sub txtPSNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPSNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPSNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPSNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchPSNo()
    End Sub
    Private Sub SearchPSNo()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If Trim(txtFromDept.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtFromDept.Text) & "'"
        End If

        If MainClass.SearchGridMaster("", "PRD_PMEMODEPT_HDR", "AUTO_KEY_REF", "REF_DATE", "DEPT_CODE", , SqlStr) = True Then
            txtPSNo.Text = AcName
            If txtPSNo.Enabled = True Then txtPSNo.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtPSNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPSNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrorHandler
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim i As Integer
        Dim xItemCode As String
        Dim xItemUOM As String
        Dim mDivisionCode As Double
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        If Trim(txtPSNo.Text) = "" Then GoTo EventExitSub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        If Len(txtPSNo.Text) < 6 Then
            txtPSNo.Text = Val(txtPSNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = " SELECT DEPT_CODE, " & vbCrLf & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.ISSUE_UOM " & vbCrLf & " FROM PRD_PMEMODEPT_HDR IH, PRD_PMEMODEPT_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODe=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf & " AND ID.COMPANY_CODe=INVMST.COMPANY_CODe " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.AUTO_KEY_REF=" & Val(txtPSNo.Text) & ""

        SqlStr = SqlStr & vbCrLf & " AND (GETFINALOPRNEW(IH.COMPANY_CODE, IH.DEPT_CODE, ID.ITEM_CODE,ID.OPR_CODE,IH.REF_DATE)='Y' OR ID.OPR_CODE IS NULL)"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        i = 1

        If RsTemp.EOF = False Then
            txtFromDept.Text = Trim(IIf(IsDbNull(RsTemp.Fields("DEPT_CODE").Value), "", RsTemp.Fields("DEPT_CODE").Value))
            If MainClass.ValidateWithMasterTable(txtFromDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblFromDept.Text = MasterNo
            End If

            With SprdMain
                Do While RsTemp.EOF = False
                    .Row = i
                    .Col = ColItemCode
                    xItemCode = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                    .Text = xItemCode

                    .Col = ColItemDesc
                    .Text = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))

                    .Col = ColUom
                    xItemUOM = Trim(IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                    .Text = xItemUOM

                    .Col = ColBatchNo
                    If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                        mBatchNo = Trim(.Text)
                        xFGBatchNoReq = "Y"
                    Else
                        mBatchNo = ""
                        xFGBatchNoReq = "N"
                    End If

                    .Col = ColStockType
                    .Text = "ST"

                    .Col = ColStockQty
                    SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtDate.Text), xItemUOM, Trim(txtFromDept.Text), "ST", mBatchNo, ConPH, mDivisionCode, ConStockRefType_PISS, Val(lblMKey.Text), xFGBatchNoReq))

                    .Col = ColIssueQty
                    .Text = "0.00"

                    i = i + 1
                    .MaxRows = i
                    RsTemp.MoveNext()
                Loop
                Call FormatSprdMain(-1)
            End With
            txtPSNo.Enabled = False
        Else
            MsgInformation("Invalid Production Slip No")
            Cancel = True
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        GoTo EventExitSub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then GoTo EventExitSub
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRecvEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRecvEmp.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRecvEmp_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRecvEmp.DoubleClick
        Call cmdSearchRecvEmp_Click(cmdSearchRecvEmp, New System.EventArgs())
    End Sub

    Private Sub txtRecvEmp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRecvEmp.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRecvEmp.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRecvEmp_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRecvEmp.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtRecvEmp_DoubleClick(txtRecvEmp, New System.EventArgs())
    End Sub

    Private Sub txtRecvEmp_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRecvEmp.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtRecvEmp.Text) = "" Then GoTo EventExitSub
        txtRecvEmp.Text = VB6.Format(txtRecvEmp.Text, "000000")
        If MainClass.ValidateWithMasterTable(txtRecvEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            lblRecvEmp.text = MasterNo
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

    Private Sub txtRefTM_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefTM.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtToDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtToDept_DoubleClick(txtToDept, New System.EventArgs())
    End Sub

    Private Sub txtToDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtToDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtToDept.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtToDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            lblToDept.text = MasterNo
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        On Error GoTo ErrorHandler
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim i As Integer
        Dim xItemCode As String
        Dim xItemUOM As String
        Dim mDivisionCode As Double
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String
        Dim mProdSeq As Long
        Dim mNextDept As String

        If Trim(txtFromDept.Text) = "" Then Exit Sub
        If Trim(txtDate.Text) = "" Then Exit Sub
        If Not IsDate(txtDate.Text) Then Exit Sub


        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = " SELECT DISTINCT " & vbCrLf _
            & " IH.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC, INVMST.ISSUE_UOM " & vbCrLf _
            & " FROM PRD_PRODSEQUENCE_DET IH, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODe=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODe=INVMST.COMPANY_CODe " & vbCrLf _
            & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtFromDept.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        i = 1

        If RsTemp.EOF = False Then
            With SprdMain
                Do While RsTemp.EOF = False
                    .Row = i
                    .Col = ColItemCode
                    xItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                    'mProdSeq = GetProductSeqNo(xItemCode, txtFromDept.Text, txtDate.Text)

                    'mNextDept = GetProductDept(xItemCode, mProdSeq + 1, txtDate.Text)

                    'If Trim(mNextDept) = Trim(txtToDept.Text) Then
                    .Text = xItemCode

                    .Col = ColItemDesc
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))

                    .Col = ColUom
                    xItemUOM = Trim(IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                    .Text = xItemUOM

                    .Col = ColBatchNo
                    If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                        mBatchNo = Trim(.Text)
                        xFGBatchNoReq = "Y"
                    Else
                        mBatchNo = ""
                        xFGBatchNoReq = "N"
                    End If

                    .Col = ColStockType
                    .Text = "ST"

                    .Col = ColStockQty
                    SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtDate.Text), xItemUOM, Trim(txtFromDept.Text), "ST", mBatchNo, ConPH, mDivisionCode, ConStockRefType_PISS, Val(lblMKey.Text), xFGBatchNoReq))

                    .Col = ColIssueQty
                    .Text = "0.00"
                    'End If

                    i = i + 1
                    .MaxRows = i
                    RsTemp.MoveNext()
                Loop
                Call FormatSprdMain(-1)
            End With
        Else
            MsgInformation("No Plan Enter For Such Dept. &  Date")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        cmdPopulate.Enabled = False
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
    End Sub
    Private Sub txtCPNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCPNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCPNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCPNo.DoubleClick
        Call SearchCPNo()
    End Sub

    Private Sub txtCPNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCPNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCPNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCPNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCPNo()
    End Sub
    Private Sub SearchCPNo()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If Trim(txtFromDept.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtFromDept.Text) & "'"
        End If

        'SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_REF NOT IN (SELECT AUTO_KEY_CUT FROM PRD_ISSREC_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")"

        If MainClass.SearchGridMaster("", "PRD_CUTTINGPLAN_HDR", "AUTO_KEY_REF", "REF_DATE", "DEPT_CODE", , SqlStr) = True Then
            txtCPNo.Text = AcName
            If txtCPNo.Enabled = True Then txtCPNo.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtCPNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCPNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrorHandler
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim i As Integer
        Dim xItemCode As String
        Dim xItemUOM As String
        Dim mDivisionCode As Double
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        If Trim(txtCPNo.Text) = "" Then GoTo EventExitSub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        If Len(txtCPNo.Text) < 6 Then
            txtCPNo.Text = Val(txtCPNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = " SELECT DEPT_CODE, " & vbCrLf _
            & " ID.SF_CODE ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.ISSUE_UOM, ID.SF_QTY " & vbCrLf _
            & " FROM PRD_CUTTINGPLAN_HDR IH, PRD_CUTTINGPLAN_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODe=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf _
            & " AND ID.COMPANY_CODe=INVMST.COMPANY_CODe " & vbCrLf _
            & " AND ID.SF_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.AUTO_KEY_REF=" & Val(txtCPNo.Text) & ""


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        i = 1

        If RsTemp.EOF = False Then
            txtFromDept.Text = Trim(IIf(IsDBNull(RsTemp.Fields("DEPT_CODE").Value), "", RsTemp.Fields("DEPT_CODE").Value))
            If MainClass.ValidateWithMasterTable(txtFromDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblFromDept.Text = MasterNo
            End If

            With SprdMain
                Do While RsTemp.EOF = False
                    .Row = i
                    .Col = ColItemCode
                    xItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                    .Text = xItemCode

                    .Col = ColItemDesc
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))

                    .Col = ColUom
                    xItemUOM = Trim(IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                    .Text = xItemUOM

                    .Col = ColBatchNo
                    If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                        mBatchNo = Trim(.Text)
                        xFGBatchNoReq = "Y"
                    Else
                        mBatchNo = ""
                        xFGBatchNoReq = "N"
                    End If

                    .Col = ColStockType
                    .Text = "ST"

                    .Col = ColStockQty
                    SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtDate.Text), xItemUOM, Trim(txtFromDept.Text), "ST", mBatchNo, ConPH, mDivisionCode, ConStockRefType_PISS, Val(lblMKey.Text), xFGBatchNoReq))

                    .Col = ColIssueQty
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SF_QTY").Value), 0, RsTemp.Fields("SF_QTY").Value), "0.00")

                    i = i + 1
                    .MaxRows = i
                    RsTemp.MoveNext()
                Loop
                Call FormatSprdMain(-1)
            End With
            txtCPNo.Enabled = False
        Else
            MsgInformation("Invalid Production Slip No")
            Cancel = True
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        GoTo EventExitSub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then GoTo EventExitSub
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
