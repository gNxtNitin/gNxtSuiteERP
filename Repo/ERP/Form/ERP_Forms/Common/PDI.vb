Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmPDI
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
    Private Const ColProdItemCode As Short = 2
    Private Const ColItemDesc As Short = 3
    Private Const ColPartNo As Short = 4
    Private Const ColUom As Short = 5
    Private Const ColBatchNo As Short = 6
    Private Const ColStockFGQty As Short = 7
    Private Const ColStockQty As Short = 8
    Private Const ColProdQty As Short = 9
    Private Const ColOKQty As Short = 10
    Private Const ColPrevOkQty As Short = 11
    Private Const ColFaultQty As Short = 12
    Private Const ColStockType As Short = 13
    Private Const ColFaultType As Short = 14
    Private Const ColFaultName As Short = 15
    Private Const ColPackDetail As Short = 16
    Private Const ColCostPcs As Short = 17
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Function GetLastEntryDate() As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetLastEntryDate = ""
        SqlStr = ""
        SqlStr = "SELECT Max(PMO_DATE) AS  PMO_DATE " & vbCrLf & " FROM PRD_PMEMO_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetLastEntryDate = IIf(IsDbNull(RsTemp.Fields("PMO_DATE").Value), "", RsTemp.Fields("PMO_DATE").Value)
        End If

        Exit Function
ErrPart:

    End Function

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        Dim cntRow As Integer
        Dim mDivisionCode As Double
        Dim mProdItemCode As String
        Dim pItemUOM As String = ""
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColProdItemCode
                mProdItemCode = Trim(.Text)

                .Col = ColBatchNo
                If MainClass.ValidateWithMasterTable(mProdItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    mBatchNo = Trim(.Text)
                    xFGBatchNoReq = "Y"
                Else
                    mBatchNo = ""
                    xFGBatchNoReq = "N"
                End If

                .Col = ColUom
                pItemUOM = Trim(.Text)

                .Col = ColStockFGQty
                .Text = CStr(GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), pItemUOM, Trim(txtFromDept.Text), "FG", "", ConWH, mDivisionCode) + GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), pItemUOM, Trim(txtFromDept.Text), "CS", "", ConWH, mDivisionCode))


                .Col = ColStockQty
                .Text = CStr(GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), pItemUOM, Trim(txtFromDept.Text), "ST", mBatchNo, IIf(txtFromDept.Text = "STR", ConWH, ConPH), mDivisionCode, ConStockRefType_PMEMO, Val(txtPMemoNo.Text), xFGBatchNoReq))

                If GetProductionType(mProdItemCode) = "J" Then
                    SprdMain.Text = CStr(Val(SprdMain.Text) + GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), pItemUOM, Trim(txtFromDept.Text), "CS", "", IIf(txtFromDept.Text = "STR", ConWH, ConPH), mDivisionCode, ConStockRefType_PMEMO, Val(txtPMemoNo.Text)))
                End If
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

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1

        cboShiftcd.Items.Clear()
        cboShiftcd.Items.Add(("A"))
        cboShiftcd.Items.Add(("B"))
        cboShiftcd.Items.Add(("C"))

        cboShiftcd.SelectedIndex = 0



        cboType.Items.Clear()

        If lblBookType.Text = "S" Then
            cboType.Items.Add(("Scrap"))
        Else
            cboType.Items.Add(("Production"))
            cboType.Items.Add(("Jobwork"))
        End If

        cboType.SelectedIndex = 0

        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
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
                If InsertIntoDelAudit(PubDBCn, "PRD_PMEMO_HDR", (txtPMemoNo.Text), RsPMemoMain, "AUTO_KEY_PMO") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_PMEMO_HDR", "AUTO_KEY_PMO", (lblMKey.Text)) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_PMEMO, (txtPMemoNo.Text)) = False Then GoTo DelErrPart


                PubDBCn.Execute("DELETE FROM FIN_RGDAILYMANU_HDR WHERE Mkey='" & Val(lblMKey.Text) & "' AND BOOKTYPE='P'")
                PubDBCn.Execute("DELETE FROM PRD_PACKING_TRN WHERE AUTO_KEY_PMO=" & Val(lblMKey.Text) & "")
                PubDBCn.Execute("DELETE FROM PRD_PMEMO_DET WHERE AUTO_KEY_PMO=" & Val(lblMKey.Text) & "")
                PubDBCn.Execute("DELETE FROM PRD_PMEMO_HDR WHERE AUTO_KEY_PMO=" & Val(lblMKey.Text) & "")
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
            MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

    Private Sub cmdPopulate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulate.Click

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        Dim mTRNType As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mStockType As String = ""
        Dim mOKQty As Double
        Dim mProdItemCode As String
        Dim mDivisionCode As Double
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        SqlStr = ""
        '    mTRNType = IIf(Left(cboType.Text, 1) = "P", "S", "J")

        If VB.Left(cboType.Text, 1) = "P" Then
            mStockType = "FG"
        ElseIf VB.Left(cboType.Text, 1) = "J" Then
            mStockType = "CS"
        ElseIf VB.Left(cboType.Text, 1) = "S" Then
            mStockType = "SC"
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        SqlStr = " SELECT ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.ISSUE_UOM, INVMST.CUSTOMER_PART_NO " & vbCrLf _
            & " FROM DSP_SALEORDER_HDR IH,DSP_SALEORDER_DET ID, INV_ITEM_MST INVMST, INV_GENERAL_MST GEN " & vbCrLf _
            & " WHERE IH.MKEY = ID.MKEY " & vbCrLf _
            & " AND ID.Company_Code = INVMST.Company_Code " & vbCrLf _
            & " AND ID.ITEM_CODE = INVMST.ITEM_CODE " & vbCrLf _
            & " AND INVMST.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
            & " AND INVMST.CATEGORY_CODE=GEN.GEN_CODE" & vbCrLf _
            & " AND GEN.GEN_TYPE='C'" & vbCrLf _
            & " AND GEN.STOCKTYPE='" & mStockType & "'" & vbCrLf _
            & " AND IH.AUTO_KEY_SO=" & Val(txtSONo.Text) & " "

        SqlStr = SqlStr & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.SO_STATUS='O' " & vbCrLf & " ORDER BY ID.SERIAL_NO"

        '
        '    SqlStr = " SELECT ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.ISSUE_UOM " & vbCrLf _
        ''            & " FROM FIN_SUPP_CUST_DET ID, INV_ITEM_MST INVMST, INV_GENERAL_MST GEN" & vbCrLf _
        ''            & " Where ID.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
        ''            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
        ''            & " AND INVMST.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
        ''            & " AND INVMST.CATEGORY_CODE=GEN.GEN_CODE" & vbCrLf _
        ''            & " AND ID.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSONo.Text) & "'" & vbCrLf _
        ''            & " AND ID.TRN_TYPE='" & mTRNType & "'" & vbCrLf _
        ''            & " AND GEN.GEN_TYPE='C'" & vbCrLf _
        ''            & " AND GEN.STOCKTYPE='" & mStockType & "'" & vbCrLf _
        ''            & " Order By ID.ITEM_CODE"
        '
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTemp
            If .EOF = True Then Exit Sub
            i = 1
            Do While Not .EOF

                SprdMain.Row = i

                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = mItemCode

                mProdItemCode = GetMainItemCode(mItemCode)

                SprdMain.Row = i
                SprdMain.Col = ColProdItemCode
                SprdMain.Text = mProdItemCode

                SprdMain.Col = ColItemDesc
                mItemDesc = Trim(IIf(IsDbNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value))
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColPartNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value))


                SprdMain.Col = ColUom
                mItemUOM = Trim(IIf(IsDbNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value))
                SprdMain.Text = mItemUOM

                SprdMain.Col = ColStockType
                SprdMain.Text = mStockType

                SprdMain.Col = ColPrevOkQty
                mOKQty = Val(SprdMain.Text)

                SprdMain.Col = ColBatchNo
                If MainClass.ValidateWithMasterTable(mProdItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    mBatchNo = Trim(SprdMain.Text)
                    xFGBatchNoReq = "Y"
                Else
                    mBatchNo = ""
                    xFGBatchNoReq = "N"
                End If

                SprdMain.Col = ColStockFGQty
                SprdMain.Text = CStr(GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtFromDept.Text), "FG", "", ConWH, mDivisionCode) + GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtFromDept.Text), "CS", "", ConWH, mDivisionCode))

                SprdMain.Col = ColStockQty
                If Trim(txtFromDept.Text) = "" Then
                    SprdMain.Text = "0.00"
                Else
                    SprdMain.Text = CStr(GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtFromDept.Text), "ST", mBatchNo, IIf(txtFromDept.Text = "STR", ConWH, ConPH), mDivisionCode, ConStockRefType_PMEMO, Val(txtPMemoNo.Text), xFGBatchNoReq))
                End If

                If GetProductionType(mProdItemCode) = "J" Then
                    SprdMain.Text = CStr(Val(SprdMain.Text) + GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtFromDept.Text), "CS", "", IIf(txtFromDept.Text = "STR", ConWH, ConPH), mDivisionCode, ConStockRefType_PMEMO, Val(txtPMemoNo.Text)))
                End If

                .MoveNext()

                i = i + 1
                SprdMain.MaxRows = i
            Loop
        End With

        FormatSprdMain(-1)
        cmdPopulate.Enabled = False
        cmdSearchSO.Enabled = False
        txtSONo.Enabled = False
        cboType.Enabled = False
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1 = True Then
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        If MainClass.SearchGridMaster((txtPMemoNo.Text), "PRD_PMEMO_HDR", "AUTO_KEY_PMO", "PMO_DATE", , , SqlStr) = True Then
            txtPMemoNo.Text = AcName
            If txtPMemoNo.Enabled = True Then txtPMemoNo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchSO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSO.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mStockType As String = ""

        SqlStr = ""

        If VB.Left(cboType.Text, 1) = "P" Then
            mStockType = "FG"
        ElseIf VB.Left(cboType.Text, 1) = "J" Then
            mStockType = "CS"
        ElseIf VB.Left(cboType.Text, 1) = "S" Then
            mStockType = "SC"
        End If


        SqlStr = " SELECT DISTINCT IH.AUTO_KEY_SO, CMST.SUPP_CUST_NAME, IH.CUST_PO_NO, IH.CUST_PO_DATE " & vbCrLf & " FROM DSP_SALEORDER_HDR IH,DSP_SALEORDER_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST, INV_GENERAL_MST GEN " & vbCrLf & " WHERE IH.MKEY = ID.MKEY " & vbCrLf & " AND IH.Company_Code = CMST.Company_Code " & vbCrLf & " AND IH.SUPP_CUST_CODE = CMST.SUPP_CUST_CODE " & vbCrLf & " AND ID.Company_Code = INVMST.Company_Code " & vbCrLf & " AND ID.ITEM_CODE = INVMST.ITEM_CODE " & vbCrLf & " AND INVMST.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf & " AND INVMST.CATEGORY_CODE=GEN.GEN_CODE" & vbCrLf & " AND GEN.GEN_TYPE='C'" & vbCrLf & " AND GEN.STOCKTYPE='" & mStockType & "'"

        If Val(txtSONo.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_SO LIKE " & Val(txtSONo.Text) & "% "
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.SO_STATUS='O' "

        If MainClass.SearchGridMasterBySQL2((txtSONo.Text), SqlStr) = True Then
            txtSONo.Text = AcName
        End If

        '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_STATUS='O'"
        '    If MainClass.SearchGridMaster(txtSONo.Text, "DSP_SALEORDER_HDR", "AUTO_KEY_SO", "SUPP_CUST_CODE", "CUST_PO_NO", "CUST_PO_DATE", SqlStr) = True Then
        '        txtSONo.Text = AcName
        '    End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchFromDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchFromDept.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtFromDept.Text = AcName1
            lblFromDept.Text = AcName
            If txtFromDept.Enabled = True Then txtFromDept.Focus()
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

    Private Sub cmdSearchToDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchToDept.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_CODE  IN ('STR','PAD') "
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtToDept.Text = AcName1
            lblToDept.Text = AcName
            If txtToDept.Enabled = True Then txtToDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub FrmPDI_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        Call ShowFormPackDetail(eventArgs.Col, eventArgs.Row)
    End Sub
    Private Sub ShowFormPackDetail(ByRef pCol As Integer, ByRef pRow As Integer)

        On Error GoTo ErrPart
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemCode As String
        Dim mMainItemCode As String
        Dim pPDIQty As Double

        With SprdMain
            .Row = pRow

            .Col = ColItemCode
            mItemCode = .Text

            .Col = ColProdItemCode
            mMainItemCode = .Text

            .Col = ColOKQty
            pPDIQty = Val(.Text)
        End With
        If mItemCode = "" Then Exit Sub
        If Trim(txtToDept.Text) = "" Then Exit Sub

        Me.lblDetail.Text = "False"

        With FrmPackingDetail
            .LblAddMode.Text = CStr(ADDMode)
            .LblModifyMode.Text = CStr(MODIFYMode)
            .lblProductCode.Text = mItemCode
            .lblMainProductCode.Text = mMainItemCode
            .lblDeptCode.Text = Trim(txtToDept.Text)
            .lblPDIQty.Text = Trim(CStr(pPDIQty))
            .lblRefDate.Text = VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")
            .ShowDialog()
        End With

        If Me.lblDetail.Text = "True" Then
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
            FrmPackingDetail.Close()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        'Dim xIName As String
        Dim SqlStr As String = ""
        Dim xICode As String
        Dim mUOM As String = ""
        Dim mStockType As String = ""
        Dim mBatchNo As String

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "CUSTOMER_PART_NO", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = Trim(AcName)

                    .Col = ColItemDesc
                    .Text = Trim(AcName1)

                    .Col = ColPartNo
                    .Text = Trim(AcName2)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemDesc
                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "CUSTOMER_PART_NO", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = Trim(AcName1)

                    .Col = ColItemDesc
                    .Text = Trim(AcName)

                    .Col = ColPartNo
                    .Text = Trim(AcName2)
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

                SqlStr = GetItemBatchWiseQry(xICode, (txtPMemoDate.Text), mUOM, Trim(txtFromDept.Text), mStockType, mBatchNo, ConPH, ConStockRefType_PMEMO, Val(txtPMemoNo.Text))
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
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColStockType, .ActiveRow, ColStockType, .ActiveRow, False))
            End With
        End If
        If eventArgs.row = 0 And eventArgs.col = ColFaultName Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColFaultName
                If MainClass.SearchGridMaster(.Text, "PRD_FAULT_MST", "NAME", "", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColFaultName
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColStockType, .ActiveRow, ColFaultName, .ActiveRow, False))
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
        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim mTableName As String
        Dim xItemCode As String = ""
        Dim mMainItemCode As String

        mMainItemCode = GetMainItemCode(pItemCode)

        SqlStr = ""

        SqlStr = "SELECT ITEM_CODE, TO_CHAR(CASE WHEN BATCH_NO<='0' OR BATCH_NO=NULL OR BATCH_NO='' THEN '-1' ELSE BATCH_NO END) BATCH_NO, SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY"

        mTableName = ConInventoryTable

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " "

        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & "AND STOCK_ID='" & pStock_ID & "'"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(mMainItemCode) & "'"

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

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1))<>0"

        SqlStr = SqlStr & vbCrLf & " GROUP BY ITEM_CODE,TO_CHAR(CASE WHEN BATCH_NO<='0' OR BATCH_NO=NULL OR BATCH_NO='' THEN '-1' ELSE BATCH_NO END)"



        GetItemBatchWiseQry = SqlStr

        Exit Function
ErrPart:
        GetItemBatchWiseQry = ""
    End Function
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mDivisionCode As Double
        Dim mItemCode As String
        Dim mBatchNo As String
        Dim mProdQty As Double

        If eventArgs.NewRow = -1 Then Exit Sub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColItemCode
        If Trim(SprdMain.Text) = "" Then Exit Sub
        Select Case eventArgs.Col
            Case ColItemCode, ColBatchNo
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                mItemCode = Trim(SprdMain.Text)

                SprdMain.Col = ColBatchNo
                mBatchNo = Trim(SprdMain.Text)

                If CheckDuplicateItem(mItemCode, mBatchNo) = False Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColItemCode
                    If FillItemDescPart(Trim(SprdMain.Text), mDivisionCode) = False Then
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                        eventArgs.Cancel = True
                        Exit Sub
                    End If
                End If
            Case ColProdQty
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                mItemCode = Trim(SprdMain.Text)
                If Trim(SprdMain.Text) = "" Then Exit Sub

                SprdMain.Col = ColProdQty
                If Val(SprdMain.Text) = 0 Then Exit Sub
                mProdQty = Val(SprdMain.Text)

                SprdMain.Col = ColOKQty
                SprdMain.Text = mProdQty

                If CheckQty(ColProdQty) = False Then

                    eventArgs.Cancel = True
                    Exit Sub
                Else
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
            Case ColOKQty
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                mItemCode = Trim(SprdMain.Text)
                If Trim(SprdMain.Text) = "" Then Exit Sub

                SprdMain.Col = ColOKQty
                If Val(SprdMain.Text) = 0 Then Exit Sub

                If CheckQty(ColOKQty) = False Then
                    eventArgs.Cancel = True
                    Exit Sub
                Else
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
            Case ColStockType
                Call CheckStockType()
            Case ColFaultName
                Call CheckFaultName()
        End Select
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function CheckDuplicateItem(ByRef mItemCode As String, ByRef mBatchNo As String) As Boolean

        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemRept As Integer
        Dim mChkItemCode As String
        Dim mChkBatchNo As String

        '12/09/2001 duplicate item check not requied...
        '    Exit Function
        If mItemCode = "" Then CheckDuplicateItem = True : Exit Function
        With SprdMain
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColItemCode
                mChkItemCode = Trim(.Text)

                .Col = ColBatchNo
                mChkBatchNo = Trim(.Text)

                If UCase(mChkItemCode) & "-" & mChkBatchNo = UCase(Trim(mItemCode)) & "-" & mBatchNo Then
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
    Private Sub CheckStockType()

        On Error GoTo ChkERR
        Dim mStockType As String = ""
        Dim mCheckStockType As String = ""

        If VB.Left(cboType.Text, 1) = "P" Then
            mCheckStockType = "FG"
        ElseIf VB.Left(cboType.Text, 1) = "J" Then
            mCheckStockType = "CS"
        ElseIf VB.Left(cboType.Text, 1) = "S" Then
            mCheckStockType = "SC"
        End If

        With SprdMain
            .Row = .ActiveRow
            .Col = ColStockType
            If Trim(.Text) = "" Then Exit Sub
            If MainClass.ValidateWithMasterTable(Trim(.Text), "STOCK_TYPE_CODE", "STOCK_TYPE_CODE", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mStockType = MasterNo
                If Trim(mStockType) <> mCheckStockType Then
                    If VB.Left(cboType.Text, 1) = "P" Then ''If mCheckStockType = "FG" Then
                        MsgInformation("Please Select 'FG' Stock Type.")
                    ElseIf VB.Left(cboType.Text, 1) = "J" Then
                        MsgInformation("Please Select 'CS' Stock Type.")
                    ElseIf VB.Left(cboType.Text, 1) = "S" Then
                        MsgInformation("Please Select 'SC' Stock Type.")
                    End If
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColStockType)
                    Exit Sub
                End If
            Else
                MsgInformation("Invalid Stock Type.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColStockType)
                Exit Sub
            End If
        End With
        Exit Sub
ChkERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub CheckFaultName()

        On Error GoTo ChkERR

        With SprdMain
            .Row = .ActiveRow
            .Col = ColFaultName
            If Trim(.Text) = "" Then Exit Sub
            If MainClass.ValidateWithMasterTable(Trim(.Text), "NAME", "NAME", "PRD_FAULT_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Fault name.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColFaultName)
                Exit Sub
            End If
        End With
        Exit Sub
ChkERR:
        MsgBox(Err.Description)
    End Sub
    Private Function CheckQty(ByRef pColQty As Integer) As Boolean

        On Error GoTo ERR1
        Dim mProductQty As Double
        Dim mOKQty As Double
        Dim mFaultQty As Double

        CheckQty = False

        '    CheckQty = True
        '    Exit Function

        With SprdMain
            .Row = .ActiveRow
            .Col = pColQty
            If Val(.Text) > 0 Then
                .Col = ColProdQty
                mProductQty = Val(.Text)

                .Col = ColOKQty
                .Text = mProductQty
                'mOKQty = Val(.Text)

                'If mProductQty - mOKQty < 0 Then
                '    CheckQty = False
                '    MsgInformation("Ok Quantity Cann't be Greater Than Production Qty.")
                '    MainClass.SetFocusToCell(SprdMain, .ActiveRow, pColQty)
                '    Exit Function
                'End If

                .Col = ColFaultQty
                .Text = 0 '' CStr(mProductQty - mOKQty)
                CheckQty = True
            Else
                CheckQty = False
                MsgInformation("Qty Cann't be Zero.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, pColQty)
            End If


        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FillItemDescPart(ByRef pItemCode As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCategoryCode As String = ""
        Dim mStockType As String = ""
        Dim mCheckStockType As String = ""
        Dim mOKQty As Double
        Dim pItemUOM As String = ""
        Dim mProdItemCode As String
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        FillItemDescPart = False
        If Trim(pItemCode) = "" Then Exit Function

        If VB.Left(cboType.Text, 1) = "P" Then
            mCheckStockType = "FG"
        ElseIf VB.Left(cboType.Text, 1) = "J" Then
            mCheckStockType = "CS"
        ElseIf VB.Left(cboType.Text, 1) = "S" Then
            mCheckStockType = "SC"
        End If


        SqlStr = "SELECT ITEM_CODE, ITEM_SHORT_DESC,ISSUE_UOM,CATEGORY_CODE,CUSTOMER_PART_NO " & vbCrLf _
            & " FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mCategoryCode = Trim(IIf(IsDbNull(RsTemp.Fields("CATEGORY_CODE").Value), "", RsTemp.Fields("CATEGORY_CODE").Value))

            If MainClass.ValidateWithMasterTable(mCategoryCode, "GEN_CODE", "STOCKTYPE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mStockType = MasterNo
                If mStockType <> mCheckStockType Then
                    If mCheckStockType = "FG" Then
                        MsgInformation("Not a Finish Good Item.")
                    ElseIf mCheckStockType = "CS" Then
                        'MsgInformation("Not a JobWork Item.")
                        GoTo NextRecd
                    ElseIf mCheckStockType = "SC" Then
                        MsgInformation("Not a Scrap Item.")
                    End If

                    FillItemDescPart = False
                    Exit Function
                End If
            Else
                MsgInformation("Invalid Stock Type. Please Check Category For This Item")
                FillItemDescPart = False
                Exit Function
            End If
NextRecd:
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                .Text = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                mProdItemCode = GetMainItemCode(pItemCode)

                SprdMain.Row = .ActiveRow
                SprdMain.Col = ColProdItemCode
                SprdMain.Text = mProdItemCode

                .Col = ColItemDesc
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColPartNo
                .Text = IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value)

                .Col = ColUom
                .Text = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                pItemUOM = Trim(IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))

                .Col = ColBatchNo
                If MainClass.ValidateWithMasterTable(mProdItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    mBatchNo = Trim(.Text)
                    xFGBatchNoReq = "Y"
                Else
                    mBatchNo = ""
                    xFGBatchNoReq = "N"
                End If

                .Col = ColStockType
                .Text = IIf(Trim(.Text) = "", mStockType, Trim(.Text))

                .Col = ColPrevOkQty
                mOKQty = Val(.Text)

                .Col = ColStockFGQty
                .Text = CStr(GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), pItemUOM, Trim(txtFromDept.Text), "FG", "", ConWH, mDivisionCode) + GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), pItemUOM, Trim(txtFromDept.Text), "CS", "", ConWH, mDivisionCode))


                .Col = ColStockQty
                If VB.Left(cboType.Text, 1) = "J" Then
                    .Text = CStr(GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), pItemUOM, Trim(txtFromDept.Text), "CS", mBatchNo, IIf(txtFromDept.Text = "STR", ConWH, ConPH), mDivisionCode, ConStockRefType_PMEMO, Val(txtPMemoNo.Text), xFGBatchNoReq))
                Else
                    .Text = CStr(GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), pItemUOM, Trim(txtFromDept.Text), "ST", mBatchNo, IIf(txtFromDept.Text = "STR", ConWH, ConPH), mDivisionCode, ConStockRefType_PMEMO, Val(txtPMemoNo.Text), xFGBatchNoReq))
                End If
                'If mStockType = "CS" Then
                '    .Text = CStr(Val(.Text) + GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), pItemUOM, Trim(txtFromDept.Text), "CS", "", IIf(txtFromDept.Text = "STR", ConWH, ConPH), mDivisionCode, ConStockRefType_PMEMO, Val(txtPMemoNo.Text)))
                'End If

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

    Private Function ValidateStockQty() As Boolean

        On Error GoTo ERR1
        Dim cntRow As Integer
        Dim CntItemRow As Integer
        Dim mStockQty As Double
        Dim mPDIQty As Double
        Dim mItemCode As String
        Dim mCheckItemCode As String
        Dim pItemUOM As String = ""
        Dim mDivisionCode As Double
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows
                mStockQty = 0
                mPDIQty = 0
                .Row = cntRow

                .Col = ColProdItemCode
                mItemCode = Trim(.Text)

                .Col = ColUom
                pItemUOM = Trim(.Text)

                .Col = ColBatchNo
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    mBatchNo = Trim(.Text)
                    xFGBatchNoReq = "Y"
                Else
                    mBatchNo = ""
                    xFGBatchNoReq = "N"
                End If

                '                mMainItemCode = GetMainItemCode(mItemCode)

                If Mid(cboType.Text, 1, 1) = "J" Then
                    mStockQty = GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), pItemUOM, Trim(txtFromDept.Text), "CS", "", IIf(txtFromDept.Text = "STR", ConWH, ConPH), mDivisionCode, ConStockRefType_PMEMO, Val(txtPMemoNo.Text))
                Else
                    mStockQty = GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), pItemUOM, Trim(txtFromDept.Text), "ST", mBatchNo, IIf(txtFromDept.Text = "STR", ConWH, ConPH), mDivisionCode, ConStockRefType_PMEMO, Val(txtPMemoNo.Text), xFGBatchNoReq)
                End If

                For CntItemRow = 1 To .MaxRows
                    .Row = CntItemRow

                    .Col = ColProdItemCode
                    mCheckItemCode = Trim(.Text)
                    If mCheckItemCode = mItemCode Then
                        .Col = ColProdQty
                        mPDIQty = mPDIQty + Val(.Text)
                    End If
                Next
                If mPDIQty > mStockQty Then
                    MsgInformation("Item Code : " & mItemCode & " PDI Qty Cann't be Greater than Lot Stock Qty, so cann't be saved.")
                    MainClass.SetFocusToCell(SprdMain, cntRow, ColProdQty)
                    ValidateStockQty = False
                    Exit Function
                End If

                If xFGBatchNoReq = "Y" Then

                    mStockQty = GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), pItemUOM, Trim(txtFromDept.Text), "ST", "", IIf(txtFromDept.Text = "STR", ConWH, ConPH), mDivisionCode, ConStockRefType_PMEMO, Val(txtPMemoNo.Text), "N")

                    If mPDIQty > mStockQty Then
                        MsgInformation("Item Code : " & mItemCode & " PDI Qty Cann't be Greater than Stock Qty, so cann't be saved.")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColProdQty)
                        ValidateStockQty = False
                        Exit Function
                    End If

                End If

            Next
        End With
        ValidateStockQty = True
        Exit Function
ERR1:
        ValidateStockQty = False
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

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_PMO)  " & vbCrLf & " FROM PRD_PMEMO_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = CDbl(Mid(mMaxValue, 1, Len(mMaxValue) - 6)) ''Mid(.Fields(0), 1, Len(.Fields(0)) - 4)
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
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
        Dim mDivisionCode As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        mPMemoNo = Val(txtPMemoNo.Text)
        If Val(txtPMemoNo.Text) = 0 Then
            mPMemoNo = CDbl(AutoGenMemoNo())
        End If
        txtPMemoNo.Text = CStr(mPMemoNo)

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        If ADDMode = True Then
            lblMKey.Text = CStr(mPMemoNo)
            SqlStr = " INSERT INTO PRD_PMEMO_HDR " & vbCrLf & " (COMPANY_CODE,FYEAR,AUTO_KEY_PMO," & vbCrLf & " PMO_DATE,PREP_TIME,FROMDEPT_CODE,TODEPT_CODE,SHIFT_CODE," & vbCrLf & " EMP_CODE, REMARKS, PROD_TYPE, ADDUSER, ADDDATE, MODUSER, MODDATE, DIV_CODE) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & "," & mPMemoNo & ", " & vbCrLf & " TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & txtRefTM.Text & "','HH24:MI')," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtFromDept.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtToDept.Text)) & "','" & cboShiftcd.Text & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtEmp.Text)) & "','" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', " & vbCrLf & " '" & VB.Left(cboType.Text, 1) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'',''," & mDivisionCode & ")"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE PRD_PMEMO_HDR SET " & vbCrLf & " AUTO_KEY_PMO=" & mPMemoNo & ", " & vbCrLf & " PMO_DATE=TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " PREP_TIME=TO_DATE('" & txtRefTM.Text & "','HH24:MI')," & vbCrLf & " FROMDEPT_CODE='" & MainClass.AllowSingleQuote((txtFromDept.Text)) & "', " & vbCrLf & " TODEPT_CODE='" & MainClass.AllowSingleQuote((txtToDept.Text)) & "', " & vbCrLf & " SHIFT_CODE='" & cboShiftcd.Text & "', " & vbCrLf & " EMP_CODE='" & MainClass.AllowSingleQuote((txtEmp.Text)) & "', " & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', " & vbCrLf & " PROD_TYPE= '" & VB.Left(cboType.Text, 1) & "', DIV_CODE=" & mDivisionCode & "," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_PMO=" & Val(lblMKey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(mDivisionCode) = False Then GoTo ErrPart
        If UpdatePackingDetail(Val(lblMKey.Text)) = False Then GoTo ErrPart
        UpdateMain1 = True
        PubDBCn.CommitTrans()
        txtPMemoNo.Text = CStr(mPMemoNo)
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans()
        RsPMemoMain.Requery()
        RsPMemoDetail.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function UpdateDetail1(ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String = ""
        Dim mStockType As String = ""
        Dim mOutStockType As String
        Dim mProdQty As Double
        Dim mCostPcs As Double
        Dim xStockRowNo As Integer
        Dim xItemCost As Double
        Dim mOutCCCode As String
        Dim mInCCCode As String
        Dim mOKQty As Double
        Dim mFaultQty As Double
        Dim mFaultType As String
        Dim mSqlStr As String
        Dim RsBOM As ADODB.Recordset = Nothing
        Dim mMainItemCode As String
        Dim mFaultName As String
        Dim xFGBatchNo As String
        Dim xFGBatchNoReq As String

        If MainClass.ValidateWithMasterTable((txtFromDept.Text), "DEPT_CODE", "CCCODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mOutCCCode = IIf(IsDbNull(MasterNo), "-1", MasterNo)
        Else
            mOutCCCode = "-1"
        End If

        If MainClass.ValidateWithMasterTable((txtToDept.Text), "DEPT_CODE", "CCCODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInCCCode = IIf(IsDbNull(MasterNo), "-1", MasterNo)
        Else
            mInCCCode = "-1"
        End If


        PubDBCn.Execute("DELETE FROM PRD_PACKING_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_PMO=" & Val(lblMKey.Text) & "")

        SqlStr = " DELETE FROM PRD_PMEMO_DET " & vbCrLf & " WHERE AUTO_KEY_PMO=" & Val(lblMKey.Text) & " "
        PubDBCn.Execute(SqlStr)

        PubDBCn.Execute("Delete From FIN_RGDAILYMANU_HDR Where Mkey='" & Val(lblMKey.Text) & "' AND BOOKTYPE='P'")



        If DeleteStockTRN(PubDBCn, ConStockRefType_PMEMO, (txtPMemoNo.Text)) = False Then GoTo UpdateDetail1Err
        xStockRowNo = 1
        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColUom
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColBatchNo
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    xFGBatchNo = Trim(.Text)
                    xFGBatchNoReq = "Y"
                Else
                    xFGBatchNo = ""
                    xFGBatchNoReq = "N"
                End If

                .Col = ColProdQty
                mProdQty = Val(.Text)

                .Col = ColOKQty
                mOKQty = mProdQty        '' Val(.Text)

                mFaultQty = 0 ''  Not required '' 22-09-2015 mProdQty - mOKQty

                .Col = ColStockType
                '            mStockType = MainClass.AllowSingleQuote(.Text)

                If VB.Left(cboType.Text, 1) = "P" Then
                    mStockType = "FG"
                ElseIf VB.Left(cboType.Text, 1) = "J" Then
                    mStockType = "CS"
                ElseIf VB.Left(cboType.Text, 1) = "S" Then
                    mStockType = "SC"
                End If

                .Col = ColFaultType
                mFaultType = VB.Left(MainClass.AllowSingleQuote(.Text), 1)

                .Col = ColFaultName
                mFaultName = MainClass.AllowSingleQuote(.Text)

                .Col = ColCostPcs
                mCostPcs = Val(.Text)

                SqlStr = ""

                If mItemCode <> "" And mProdQty > 0 Then

                    SqlStr = " INSERT INTO PRD_PMEMO_DET ( " & vbCrLf & " COMPANY_CODE, AUTO_KEY_PMO, SERIAL_NO, ITEM_CODE, ITEM_DESC, " & vbCrLf & " ITEM_UOM, BATCH_NO, STOCK_TYPE, PROD_QTY, " & vbCrLf & " OK_QTY, FAULT_QTY, FAULT_TYPE, " & vbCrLf & " COST_PCS, FAULT_NAME) " & vbCrLf & " VALUES (" & RsCompany.Fields("Company_Code").Value & "," & Val(lblMKey.Text) & ", " & i & "," & vbCrLf & " '" & mItemCode & "','" & mItemDesc & "', " & vbCrLf & " '" & mUOM & "', '" & xFGBatchNo & "', '" & mStockType & "', " & vbCrLf & " " & mProdQty & ", " & mOKQty & ", " & mFaultQty & ", '" & mFaultType & "', " & vbCrLf & " " & mCostPcs & ", '" & MainClass.AllowSingleQuote(mFaultName) & "' ) "
                    PubDBCn.Execute(SqlStr)

                    ''SK28-11-2005

                    mMainItemCode = GetMainItemCode(mItemCode)


                    If lblBookType.Text <> "S" Then
                        If Mid(cboType.Text, 1, 1) = "J" Then          '' If GetProductionType(mItemCode) = "J" Then
                            mOutStockType = "CS"
                        Else
                            mOutStockType = "ST"
                        End If
                        If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMO, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), mOutStockType, mMainItemCode, mUOM, xFGBatchNo, mOKQty + mFaultQty, 0, "O", xItemCost, xItemCost, "", "", (txtFromDept.Text), (txtFromDept.Text), mInCCCode, "N", "From : " & lblFromDept.Text & " To : " & lblToDept.Text & " -" & ConStockRefType_PMEMO, "-1", IIf(txtFromDept.Text = "STR", ConWH, ConPH), mDivisionCode, "", mItemCode) = False Then GoTo UpdateDetail1Err

                        xStockRowNo = xStockRowNo + 1
                        If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMO, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "WR", mItemCode, mUOM, xFGBatchNo, mFaultQty, 0, "I", xItemCost, xItemCost, "", "", (txtFromDept.Text), (txtFromDept.Text), mInCCCode, "N", "From : " & lblToDept.Text & " To : " & lblFromDept.Text & " (Faulty) -" & ConStockRefType_PMEMO, "-1", IIf(txtFromDept.Text = "STR", ConWH, ConPH), mDivisionCode, "", mItemCode) = False Then GoTo UpdateDetail1Err

                    End If

                    xStockRowNo = xStockRowNo + 1
                    If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMO, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), mStockType, mItemCode, mUOM, xFGBatchNo, mOKQty, 0, "I", xItemCost, xItemCost, "", "", (txtToDept.Text), (txtToDept.Text), mInCCCode, "N", "From : " & lblFromDept.Text & " To : " & lblToDept.Text & " -" & ConStockRefType_PMEMO, "-1", ConWH, mDivisionCode, "", mItemCode) = False Then GoTo UpdateDetail1Err


                    'Temp Mark '01-12-2005
                    If Trim(mStockType) = "FG" Or Trim(mStockType) = "SC" Then
                        If UpdateProductionData(mItemCode, mOKQty) = False Then GoTo UpdateDetail1Err
                    End If


                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Function UpdateProductionData(ByRef mItemCode As String, ByRef mItemQty As Double) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mTariff As String

        If mItemQty <= 0 Then UpdateProductionData = True : Exit Function

        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "TARIFF_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mTariff = Trim(MasterNo)
        Else
            mTariff = "-1"
        End If

        SqlStr = " INSERT INTO FIN_RGDAILYMANU_HDR ( " & vbCrLf & " MKEY , COMPANY_CODE, FYEAR, BOOKTYPE, " & vbCrLf & " BILLNO , INV_PREP_TM, MDATE, " & vbCrLf & " ITEM_CODE, ITEM_QTY, " & vbCrLf & " TARIFF_CODE, UPDATEFLAG) "

        SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " '" & lblMKey.Text & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", 'P', " & vbCrLf & " '" & lblMKey.Text & "', TO_DATE('" & txtRefTM.Text & "','HH24:MI'), TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mItemCode & "'," & mItemQty & ",'" & mTariff & "','Y' ) "
        PubDBCn.Execute(SqlStr)
        ''TO_DATE('" & txtPMemoDate.Text & "','HH24:MI')
        UpdateProductionData = True
        Exit Function
ErrPart:
        UpdateProductionData = False
        If Err.Description <> "" Then MsgBox(Err.Description)
        'Resume
    End Function
    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mCheckLastEntryDate As String

        Dim i As Integer
        Dim pItemCode As String
        Dim mStockType As String = ""
        Dim mTariffCode As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pProdQty As Double
        Dim pOKQty As Double
        Dim mTotOKQty As Double
        Dim mDivisionCode As Double


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

        If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Val(MasterNo)
        Else
            MsgInformation("Invalid Division Name.")
            FieldsVarification = False
            Exit Function
        End If


        If ValidateDivisionRight(PubUserID, mDivisionCode, UCase(Trim(cboDivision.Text))) = False Then
            FieldsVarification = False
            Exit Function
        End If

        If MODIFYMode = True And RsPMemoMain.EOF = True Then Exit Function

        '
        '    If txtPMemoDate.Text = "" Then
        '        MsgBox "Date is Blank", vbInformation
        '        FieldsVarification = False
        '        txtPMemoDate.SetFocus
        '        Exit Function
        '    End If

        If txtPMemoDate.Text = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPMemoDate.Focus()
            Exit Function
        ElseIf FYChk((txtPMemoDate.Text)) = False Then
            FieldsVarification = False
            If txtPMemoDate.Enabled = True Then txtPMemoDate.Focus()
            Exit Function
        End If

        If cboType.Text = "" Then
            MsgBox("Production Type is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboType.Enabled = True Then cboType.Focus()
            Exit Function
        End If

        If cboDivision.Text = "" Then
            MsgBox("Division is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If

        mCheckLastEntryDate = GetLastEntryDate

        If PubSuperUser <> "S" Then
            If mCheckLastEntryDate <> "" Then
                If CDate(txtPMemoDate.Text) < CDate(mCheckLastEntryDate) Then
                    MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If txtFromDept.Text = "" Then
            MsgBox("From Deptt is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtFromDept.Focus()
            Exit Function
        End If
        If txtToDept.Text = "" Then
            MsgBox("To Deptt is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtToDept.Focus()
            Exit Function
        End If
        If txtEmp.Text = "" Then
            MsgBox("Employee is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtEmp.Focus()
            Exit Function
        End If

        If CheckRowCount() = False Then
            FieldsVarification = False
            Exit Function
        End If

        If lblBookType.Text <> "S" Then
            'If RsCompany.Fields("StockBalCheck").Value = "Y" Then
            '            If CheckStockQty(SprdMain, ColStockQty, ColProdQty, ColItemCode, ColStockType, True) = False Then
            If ValidateStockQty = False Then
                    FieldsVarification = False
                    Exit Function
                End If
            'End If
        End If




        For i = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = i
            SprdMain.Col = ColItemCode
            pItemCode = Trim(SprdMain.Text)

            SprdMain.Col = ColProdQty
            pProdQty = Val(SprdMain.Text)

            SprdMain.Col = ColOKQty
            pOKQty = CDbl(Trim(SprdMain.Text))
            mTotOKQty = mTotOKQty + CDbl(Trim(SprdMain.Text))

            If lblPDIType.Text = "B" Then
                If pProdQty <> pOKQty Then
                    MsgInformation("OK Qty must be equal to Production Qty.")
                    FieldsVarification = False
                    Exit Function

                    'SprdMain.Col = ColFaultName
                    'If Trim(SprdMain.Text) = "" Then
                    '    MsgInformation("Please enter the Fault.") : FieldsVarification = False : Exit Function
                    'End If
                End If
            End If

            SqlStr = "SELECT STOCKTYPE, PRD_TYPE, TARIFF_CODE " & vbCrLf & " FROM INV_ITEM_MST IMST, INV_GENERAL_MST CMST" & vbCrLf & " WHERE IMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IMST.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IMST.CATEGORY_CODE=CMST.GEN_CODE " & vbCrLf & " AND CMST.GEN_TYPE='C'" & vbCrLf & " AND IMST.ITEM_CODE='" & Trim(pItemCode) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mStockType = Trim(IIf(IsDbNull(RsTemp.Fields("STOCKTYPE").Value), "", RsTemp.Fields("STOCKTYPE").Value))
                mTariffCode = Trim(IIf(IsDbNull(RsTemp.Fields("TARIFF_CODE").Value), "", RsTemp.Fields("TARIFF_CODE").Value))

                If VB.Left(cboType.Text, 1) = "P" Then
                    If mStockType <> "FG" Then MsgInformation("Not a Finish Good Item.") : FieldsVarification = False : Exit Function
                    'If Trim(mTariffCode) = "" Then MsgInformation("Tariff Code Not Define for Item Code : " & pItemCode) : FieldsVarification = False : Exit Function
                ElseIf VB.Left(cboType.Text, 1) = "J" Then
                    'If mStockType <> "CS" Then MsgInformation("Not a JobWork Item.") : FieldsVarification = False : Exit Function
                ElseIf VB.Left(cboType.Text, 1) = "S" Then
                    If mStockType <> "SC" Then MsgInformation("Not a Scrap Item.") : FieldsVarification = False : Exit Function
                End If
            End If
        Next

        If lblPDIType.Text = "S" Then
            If mTotOKQty > 0 Then
                MsgInformation("Material Already Received, So Cann't be Changed any Qty.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColProdQty, "N", "Please Check Produce Quantity.") = False Then FieldsVarification = False: Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False: Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Function CheckRowCount() As Boolean
        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemCode As String
        Dim mQty As Double
        Dim mRowCount As Integer
        Dim mTotQty As Double
        Dim mProductSeqNo As Integer
        Dim mMaxProductSeqNo As Integer
        Dim mMainItemCode As String
        Dim mCancel As Boolean

        mCancel = False
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

                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColProdQty, i, ColFaultName, i, mCancel))
                If mCancel = True Then
                    CheckRowCount = False
                    Exit Function
                End If
                .Row = i
                If mItemCode <> "" And mQty > 0 Then
                    If lblBookType.Text <> "S" Then
                        mMainItemCode = GetMainItemCode(mItemCode)
                        mProductSeqNo = GetProductSeqNo(mMainItemCode, Trim(txtFromDept.Text), (txtPMemoDate.Text))
                        If mProductSeqNo = 0 Then
                            If MsgQuestion("Either Production Sequence not defined Or not in this Dept." & vbCrLf & "Item Code : " & mMainItemCode & ". Are You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                                CheckRowCount = False
                                txtFromDept.Focus()
                                Exit Function
                            End If
                        End If

                        mMaxProductSeqNo = GetMaxProductSeqNo(mMainItemCode, (txtPMemoDate.Text))
                        If mProductSeqNo <> mMaxProductSeqNo Then
                            '                    If MsgQuestion("Not a Final dept for Item Code : " & mItemCode & ". Are You Want to Continue ...") = vbNo Then
                            MsgInformation("Not a Final dept for Item Code : " & mMainItemCode & ". Cann't be Saved ...")
                            CheckRowCount = False
                            txtFromDept.Focus()
                            Exit Function
                            '                    End If
                        End If
                    End If

                    .Row = i
                    .Col = ColProdQty
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
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmPDI_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblBookType.Text = "S" Then
            Me.Text = "Scrap Note"
        Else
            If lblPDIType.Text = "B" Then
                Me.Text = "Pre Despatch Inspection"
            ElseIf lblPDIType.Text = "S" Then
                Me.Text = "Pre Despatch Inspection - Send"
            ElseIf lblPDIType.Text = "R" Then
                Me.Text = "Pre Despatch Inspection - Received"
            End If
        End If

        SqlStr = ""
        SqlStr = "Select * from PRD_PMEMO_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_PMEMO_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoDetail, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)
        Call SetTextLengths()
        Call FillCbo()
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
        SqlStr = " SELECT  AUTO_KEY_PMO MEMO_NO,TO_CHAR(PMO_DATE,'DD/MM/YYYY') MEMO_DATE, " & vbCrLf & " TO_CHAR(PREP_TIME, 'HH24:MI') REF_TM, "

        If lblBookType.Text <> "S" Then
            SqlStr = SqlStr & vbCrLf & " DECODE(PROD_TYPE,'P','PRODUCTION','JOBWORK') AS PROD_TYPE,"
        Else
            SqlStr = SqlStr & vbCrLf & " DECODE(PROD_TYPE,'S','SCRAP','') AS PROD_TYPE,"
        End If

        SqlStr = SqlStr & vbCrLf & " FROMDEPT_CODE FROM_DEPT,TODEPT_CODE TO_DEPT,SHIFT_CODE,REMARKS " & vbCrLf & " FROM PRD_PMEMO_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "


        If lblBookType.Text = "S" Then
            SqlStr = SqlStr & vbCrLf & " AND PROD_TYPE='S'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND PROD_TYPE<>'S'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY AUTO_KEY_PMO"



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
            .set_ColWidth(4, 1200)
            .set_ColWidth(5, 1000)
            .set_ColWidth(6, 1000)
            .set_ColWidth(7, 1000)
            .set_ColWidth(8, 1800)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim mStr As String

        With SprdMain
            .set_RowHeight(0, ConRowHeight * 2)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.5)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPMemoDetail.Fields("ITEM_CODE").DefinedSize
            .set_ColWidth(.Col, 8)


            .Col = ColProdItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPMemoDetail.Fields("ITEM_CODE").DefinedSize
            .set_ColWidth(.Col, 8)
            .ColHidden = True

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsPMemoDetail.Fields("ITEM_DESC").DefinedSize
            .set_ColWidth(.Col, 35)

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            '.TypeEditLen = RsPMemoDetail.Fields("ITEM_DESC").DefinedSize
            .set_ColWidth(.Col, 10)

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsPMemoDetail.Fields("ITEM_UOM").DefinedSize
            .set_ColWidth(.Col, 4)

            .Col = ColBatchNo
            '        .CellType = SS_CELL_TYPE_FLOAT
            '        .TypeFloatDecimalPlaces = 0
            '        .TypeFloatDecimalChar = Asc(".")
            '        .TypeFloatMax = "9999999999"
            '        .TypeFloatMin = "-999999999"
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '        .TypeEditMultiLine = False
            .TypeEditLen = RsPMemoDetail.Fields("BATCH_NO").DefinedSize
            .set_ColWidth(.Col, 6)

            .Col = ColStockFGQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            .Col = ColProdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)

            .Col = ColOKQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)
            .ColHidden = True

            .Col = ColPrevOkQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)
            .ColHidden = True

            .Col = ColFaultQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 7)
            .ColHidden = True

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsPMemoDetail.Fields("STOCK_TYPE").DefinedSize
            .set_ColWidth(.Col, 4)

            .Col = ColFaultType
            If FormActive = False Then
                .CellType = SS_CELL_TYPE_COMBOBOX
                mStr = "X-N/A" & Chr(9) & "A-Welding U/Cut" & Chr(9) & "B-Welding Missing" & Chr(9) & "C-Spatters"
                mStr = mStr & Chr(9) & "D-Blow Holes" & Chr(9) & "E-Grindm" & Chr(9) & "H-Burr"
                mStr = mStr & Chr(9) & "I-NG With R/G" & Chr(9) & "J-Paint/Plating" & Chr(9) & "K-Scratches"
                mStr = mStr & Chr(9) & "L-Alling Ment of Bush N/G" & Chr(9) & "M-Thread Missing" & Chr(9) & "N-Threading NG"
                mStr = mStr & Chr(9) & "O-Torque of Handle Holder" & Chr(9) & "P-Paintdry" & Chr(9) & "Q-paint Dust"
                mStr = mStr & Chr(9) & "R-Paint OverFlow" & Chr(9) & "S-PitMarks" & Chr(9) & "T-Twisting"
                mStr = mStr & Chr(9) & "U-Plating Dull"


                .TypeComboBoxList = mStr
                .TypeComboBoxCurSel = 0
                .TypeComboBoxEditable = False
            End If

            .set_ColWidth(ColFaultType, 10)
            .ColHidden = True

            .Col = ColFaultName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsPMemoDetail.Fields("FAULT_NAME").DefinedSize
            .set_ColWidth(.Col, 12)
            .ColHidden = True

            .Col = ColPackDetail
            .CellType = SS_CELL_TYPE_BUTTON
            '.Lock = False
            .TypeButtonText = "Details"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColPackDetail, 8)

            .Col = ColCostPcs
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = True
            .set_ColWidth(.Col, 6)

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUom)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColProdItemCode, ColProdItemCode)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockFGQty, ColStockQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPrevOkQty, ColPrevOkQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCostPcs, ColCostPcs)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColFaultQty, ColFaultQty)

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockType, ColStockType)

        If lblPDIType.Text = "S" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColOKQty, ColOKQty)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColFaultType, ColPackDetail)
        ElseIf lblPDIType.Text = "R" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColProdQty)
        End If

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsPMemoDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsPMemoMain
            txtPMemoNo.Maxlength = .Fields("AUTO_KEY_PMO").Precision
            txtPMemoDate.Maxlength = .Fields("PMO_DATE").DefinedSize - 6
            txtFromDept.Maxlength = .Fields("FROMDEPT_CODE").DefinedSize
            txtToDept.Maxlength = .Fields("TODEPT_CODE").DefinedSize
            txtEmp.Maxlength = .Fields("EMP_CODE").DefinedSize
            txtRemarks.Maxlength = .Fields("REMARKS").DefinedSize
            txtRefTM.Maxlength = 5
            txtSONo.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mProdType As String
        Dim mEntryDate As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        With RsPMemoMain
            If Not .EOF Then
                lblMKey.Text = .Fields("AUTO_KEY_PMO").Value
                txtPMemoNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_PMO").Value), "", .Fields("AUTO_KEY_PMO").Value)
                txtPMemoDate.Text = VB6.Format(IIf(IsDbNull(.Fields("PMO_DATE").Value), "", .Fields("PMO_DATE").Value), "DD/MM/YYYY")
                txtFromDept.Text = IIf(IsDbNull(.Fields("FROMDEPT_CODE").Value), "", .Fields("FROMDEPT_CODE").Value)
                TxtFromDept_Validating(TxtFromDept, New System.ComponentModel.CancelEventArgs(False))
                txtToDept.Text = IIf(IsDbNull(.Fields("TODEPT_CODE").Value), "", .Fields("TODEPT_CODE").Value)
                txtToDept_Validating(txtToDept, New System.ComponentModel.CancelEventArgs(False))
                cboShiftcd.Text = IIf(IsDbNull(.Fields("SHIFT_CODE").Value), "", .Fields("SHIFT_CODE").Value)
                txtRefTM.Text = VB6.Format(IIf(IsDbNull(.Fields("PREP_TIME").Value), "", .Fields("PREP_TIME").Value), "HH:MM")

                mEntryDate = IIf(IsDbNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value) & " - " & VB6.Format(IIf(IsDbNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY HH:MM")
                mEntryDate = mEntryDate & vbCrLf & IIf(IsDbNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value) & " - " & VB6.Format(IIf(IsDbNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY HH:MM")
                txtEntryDate.Text = mEntryDate

                mProdType = IIf(IsDbNull(.Fields("PROD_TYPE").Value), "P", .Fields("PROD_TYPE").Value)

                If lblBookType.Text = "S" Then
                    cboType.SelectedIndex = 0
                Else
                    If mProdType = "P" Then
                        cboType.SelectedIndex = 0
                    Else
                        cboType.SelectedIndex = 1
                    End If
                End If

                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If

                txtEmp.Text = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtEmp_Validating(txtEmp, New System.ComponentModel.CancelEventArgs(False))
                txtRemarks.Text = IIf(IsDbNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                cmdPopulate.Enabled = False
                cmdSearchSO.Enabled = False
                txtSONo.Enabled = False
                cboType.Enabled = False
                Call ShowDetail1(mDivisionCode)
                Call ShowPackingDetail(Val(lblMKey.Text))
                Call MakeEnableDesableField(False)
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
    Private Sub ShowPackingDetail(ByRef pRefNo As Double)

        On Error GoTo ShowSerialNoErr
        Dim RsSRLNo As ADODB.Recordset
        Dim SqlStr As String = ""

        Call DelTemp_PackingDetail()

        SqlStr = ""

        SqlStr = "INSERT INTO TEMP_PRD_PACKING_TRN ( " & vbCrLf & " USERID, COMPANY_CODE, PRODUCT_CODE, MAIN_PRODUCT_CODE," & vbCrLf & " DEPT_CODE, SERIAL_NO, EMP_CODE, PACK_QTY)" & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " COMPANY_CODE, PRODUCT_CODE,MAIN_PRODUCT_CODE," & vbCrLf & " DEPT_CODE, SERIAL_NO, EMP_CODE,PACK_QTY " & vbCrLf & " FROM PRD_PACKING_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PMO=" & Val(CStr(pRefNo)) & " " & vbCrLf & " ORDER BY DEPT_CODE,SERIAL_NO"

        PubDBCn.Execute(SqlStr)

        Exit Sub
ShowSerialNoErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub DelTemp_PackingDetail(Optional ByRef mDeptCode As String = "")

        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = "DELETE FROM TEMP_PRD_PACKING_TRN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND UserId='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        If mDeptCode <> "" Then
            SqlStr = SqlStr & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(UCase(mDeptCode)) & "' "
        End If
        PubDBCn.Execute(SqlStr)
    End Sub
    Private Function UpdatePackingDetail(ByRef pMKey As Double) As Boolean
        On Error GoTo UpdateErr1
        Dim RsTemp_SRLNo As ADODB.Recordset
        Dim ii As Integer
        Dim SqlStr As String = ""
        Dim mProductCode As String = ""


        With SprdMain
            For ii = 1 To .MaxRows - 1
                .Row = ii
                .Col = ColItemCode
                mProductCode = Trim(.Text)

                SqlStr = "INSERT INTO PRD_PACKING_TRN (" & vbCrLf & " COMPANY_CODE, AUTO_KEY_PMO, PRODUCT_CODE, MAIN_PRODUCT_CODE, DEPT_CODE, " & vbCrLf & " SERIAL_NO, EMP_CODE,PACK_QTY )" & vbCrLf & " SELECT " & vbCrLf & " COMPANY_CODE, " & Val(CStr(pMKey)) & ", PRODUCT_CODE, MAIN_PRODUCT_CODE, DEPT_CODE, " & vbCrLf & " SERIAL_NO, EMP_CODE, PACK_QTY " & vbCrLf & " FROM TEMP_PRD_PACKING_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(mProductCode)) & "' " & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote((txtToDept.Text)) & "'"

                PubDBCn.Execute(SqlStr)
            Next
        End With

        UpdatePackingDetail = True
        Exit Function
UpdateErr1:
        UpdatePackingDetail = False
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
    End Function
    Private Sub ShowDetail1(ByRef mDivisionCode As Double)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mFaultType As String
        Dim mOKQty As Double
        Dim mItemCode As String
        Dim mItemUOM As String = ""
        Dim mProdItemCode As String
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PRD_PMEMO_DET  " & vbCrLf _
            & " WHERE AUTO_KEY_PMO = " & Val(lblMKey.Text) & " " & vbCrLf _
            & " ORDER BY  SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPMemoDetail
            If .EOF = True Then Exit Sub
            cboDivision.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            '        FormatSprdMain -1
            i = 1
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = i

                SprdMain.Col = ColItemCode
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                mItemCode = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                mProdItemCode = GetMainItemCode(mItemCode)

                SprdMain.Row = i
                SprdMain.Col = ColProdItemCode
                SprdMain.Text = mProdItemCode

                SprdMain.Col = ColItemDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value))

                SprdMain.Col = ColPartNo
                If MainClass.ValidateWithMasterTable(mProdItemCode, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Text = MasterNo
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColUom
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                mItemUOM = Trim(IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value))

                SprdMain.Col = ColBatchNo
                If MainClass.ValidateWithMasterTable(mProdItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    SprdMain.Text = IIf(IsDbNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value)
                    mBatchNo = IIf(IsDbNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value)
                    xFGBatchNoReq = "Y"
                Else
                    SprdMain.Text = ""
                    mBatchNo = ""
                    xFGBatchNoReq = "N"
                End If

                SprdMain.Col = ColProdQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("PROD_QTY").Value), "", .Fields("PROD_QTY").Value)))

                SprdMain.Col = ColOKQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("OK_QTY").Value), "", .Fields("OK_QTY").Value)))
                mOKQty = Val(IIf(IsDbNull(.Fields("OK_QTY").Value), "", .Fields("OK_QTY").Value))

                SprdMain.Col = ColPrevOkQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("OK_QTY").Value), "", .Fields("OK_QTY").Value)))

                SprdMain.Col = ColStockFGQty
                SprdMain.Text = CStr(GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtFromDept.Text), "FG", "", ConWH, mDivisionCode) + GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtFromDept.Text), "CS", "", ConWH, mDivisionCode))

                SprdMain.Col = ColStockQty
                SprdMain.Text = CStr(GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtFromDept.Text), "ST", mBatchNo, IIf(txtFromDept.Text = "STR", ConWH, ConPH), mDivisionCode, ConStockRefType_PMEMO, Val(txtPMemoNo.Text), xFGBatchNoReq))

                If GetProductionType(mProdItemCode) = "J" Then
                    SprdMain.Text = CStr(Val(SprdMain.Text) + GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtFromDept.Text), "CS", "", IIf(txtFromDept.Text = "STR", ConWH, ConPH), mDivisionCode, ConStockRefType_PMEMO, Val(txtPMemoNo.Text)))
                End If

                SprdMain.Row = i
                SprdMain.Col = ColFaultQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("FAULT_QTY").Value), "", .Fields("FAULT_QTY").Value)))

                SprdMain.Col = ColStockType
                SprdMain.Text = IIf(IsDbNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)

                SprdMain.Col = ColFaultType
                mFaultType = IIf(IsDbNull(.Fields("FAULT_TYPE").Value), "", .Fields("FAULT_TYPE").Value)

                Select Case mFaultType
                    Case "X"
                        SprdMain.Text = "X-N/A"
                    Case "A"
                        SprdMain.Text = "A-Welding U/Cut"
                    Case "B"
                        SprdMain.Text = "B-Welding Missing"
                    Case "C"
                        SprdMain.Text = "C-Spatters"
                    Case "D"
                        SprdMain.Text = "D-Blow Holes"
                    Case "E"
                        SprdMain.Text = "E-Grindm"
                    Case "H"
                        SprdMain.Text = "H-Burr"
                    Case "I"
                        SprdMain.Text = "I-NG With R/G"
                    Case "J"
                        SprdMain.Text = "J-Paint/Plating"
                    Case "K"
                        SprdMain.Text = "K-Scratches"
                    Case "L"
                        SprdMain.Text = "L-Alling Ment of Bush N/G"
                    Case "M"
                        SprdMain.Text = "M-Thread Missing"
                    Case "N"
                        SprdMain.Text = "N-Threading NG"
                    Case "O"
                        SprdMain.Text = "O-Torque of Handle Holder"
                    Case "P"
                        SprdMain.Text = "P-Paintdry"
                    Case "Q"
                        SprdMain.Text = "Q-paint Dust"
                    Case "R"
                        SprdMain.Text = "R-Paint OverFlow"
                    Case "S"
                        SprdMain.Text = "S-PitMarks"
                    Case "T"
                        SprdMain.Text = "T-Twisting"
                    Case "U"
                        SprdMain.Text = "U-Plating Dull"
                End Select

                SprdMain.Col = ColFaultName
                SprdMain.Text = IIf(IsDbNull(.Fields("FAULT_NAME").Value), "", .Fields("FAULT_NAME").Value)


                SprdMain.Col = ColCostPcs
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("COST_PCS").Value), "", .Fields("COST_PCS").Value)))

                .MoveNext()

                i = i + 1
                SprdMain.MaxRows = i
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
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
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        lblMKey.Text = ""
        txtPMemoNo.Text = ""
        txtPMemoDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtEntryDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime
        txtFromDept.Text = ""
        lblFromDept.Text = ""
        txtToDept.Text = ""
        lblToDept.Text = ""
        cboShiftcd.SelectedIndex = 0
        cboType.SelectedIndex = 0
        txtEmp.Text = ""
        lblEmp.Text = ""
        txtRemarks.Text = ""
        txtRefTM.Text = GetServerTime
        txtSONo.Text = ""

        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        cmdPopulate.Enabled = True
        cmdSearchSO.Enabled = True
        txtSONo.Enabled = True
        cboType.Enabled = True
        Call MakeEnableDesableField(True)
        cboDivision.Enabled = True
        txtEmp.Enabled = True
        cboShiftcd.Enabled = True
        If lblPDIType.Text = "R" Then
            txtPMemoDate.Enabled = False
            txtFromDept.Enabled = False ''IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            cmdSearchFromDept.Enabled = False
            txtToDept.Enabled = False ''IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            cmdSearchToDept.Enabled = False
            cboDivision.Enabled = False
            txtEmp.Enabled = False
            cboShiftcd.Enabled = False
        End If

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.lblDetail.Text = "False"
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtPMemoDate.Enabled = mMode
        txtFromDept.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        cmdSearchFromDept.Enabled = mMode
        txtToDept.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        cmdSearchToDept.Enabled = mMode
        If lblPDIType.Text = "R" Then
            txtPMemoDate.Enabled = False
            txtFromDept.Enabled = False ''IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            cmdSearchFromDept.Enabled = False
            txtToDept.Enabled = False ''IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            cmdSearchToDept.Enabled = False
            cboDivision.Enabled = False
            txtEmp.Enabled = False
            cboShiftcd.Enabled = False
        End If
    End Sub
    Private Sub FrmPDI_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmPDI_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Public Sub FrmPDI_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        ''Me.Width = VB6.TwipsToPixelsX(10560)
        '    Call FillCbo
        'AdoDCMain.Visible = False

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
        If SprdMain.ActiveRow <= 0 Then Exit Sub
        mCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColFaultName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColFaultName, 0))
        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        '    With SprdMain
        '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False
        '    End With

    End Sub

    Private Sub txtSONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSONo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSONo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSONo.DoubleClick
        Call cmdSearchSO_Click(cmdSearchSO, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchSO_Click(cmdSearchSO, New System.EventArgs())
    End Sub

    Private Sub txtSONo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSONo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtSONo.Text) = "" Then GoTo EventExitSub
        '    If MainClass.ValidateWithMasterTable(txtSONo.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = False Then
        '        MsgInformation "Invalid Customer Code"
        '        Cancel = True
        '    End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtFromDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFromDept.DoubleClick
        Call cmdSearchFromDept_Click(cmdSearchFromDept, New System.EventArgs())
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

        If FYChk((txtPMemoDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
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
        On Error GoTo ErrPart
        Dim i As Integer
        Dim mItemCode As String
        Dim mItemUOM As String = ""
        Dim a As String
        Dim mOKQty As Double
        Dim mProdItemCode As String
        Dim mDivisionCode As Double
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        If Trim(txtFromDept.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtFromDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblFromDept.Text = MasterNo
        Else
            MsgInformation("Invalid Depatment Code")
            Cancel = True
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        For i = 1 To SprdMain.MaxRows
            SprdMain.Row = i
            SprdMain.Col = ColItemCode
            mItemCode = Trim(SprdMain.Text)

            mProdItemCode = GetMainItemCode(mItemCode)

            If mItemCode <> "" Then
                SprdMain.Col = ColUom
                mItemUOM = Trim(SprdMain.Text)

                SprdMain.Col = ColBatchNo
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    mBatchNo = Trim(SprdMain.Text)
                    xFGBatchNoReq = "Y"
                Else
                    mBatchNo = ""
                    xFGBatchNoReq = "N"
                End If

                SprdMain.Col = ColPrevOkQty
                mOKQty = Val(SprdMain.Text)

                SprdMain.Col = ColStockFGQty
                SprdMain.Text = CStr(GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtFromDept.Text), "FG", "", ConWH, mDivisionCode) + GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtFromDept.Text), "CS", "", ConWH, mDivisionCode))

                SprdMain.Col = ColStockQty
                SprdMain.Text = CStr(GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtFromDept.Text), "ST", mBatchNo, IIf(txtFromDept.Text = "STR", ConWH, ConPH), mDivisionCode, ConStockRefType_PMEMO, Val(txtPMemoNo.Text), xFGBatchNoReq))

                If GetProductionType(mProdItemCode) = "J" Then
                    SprdMain.Text = CStr(Val(SprdMain.Text) + GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtFromDept.Text), "CS", "", IIf(txtFromDept.Text = "STR", ConWH, ConPH), mDivisionCode, ConStockRefType_PMEMO, Val(txtPMemoNo.Text)))
                End If
            End If
        Next
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
        Dim mCond As String

        If Trim(txtEmp.Text) = "" Then GoTo EventExitSub


        If ADDMode = True Then
            mCond = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_LEAVE_DATE IS NULL "
        Else
            mCond = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        txtEmp.Text = VB6.Format(txtEmp.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , mCond) = True Then
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
        Call cmdsearch_Click(cmdsearch, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
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

        If MODIFYMode = True And RsPMemoMain.EOF = False Then mPMemoNo = RsPMemoMain.Fields("AUTO_KEY_PMO").Value

        SqlStr = "Select * From PRD_PMEMO_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PMO=" & Val(txtPMemoNo.Text) & " AND SUBSTR(AUTO_KEY_PMO,LENGTH(AUTO_KEY_PMO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
                SqlStr = "Select * From PRD_PMEMO_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AUTO_KEY_PMO=" & Val(CStr(mPMemoNo)) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchToDept_Click(cmdSearchToDept, New System.EventArgs())
    End Sub

    Private Sub txtToDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtToDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtToDept.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtToDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_CODE  IN ('STR','PAD') ") = True Then
            lblToDept.Text = MasterNo
        Else
            MsgInformation("To Dept Should Be STR OR PAD Only.")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub FrmPDI_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
