Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports AxFPSpreadADO

Friend Class FrmReworkBreakup
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
    Private Const ColStockType As Short = 4
    Private Const colStdQty As Short = 5
    Private Const ColQty As Short = 6
    Private Const ColScrapQty As Short = 7
    Private Const ColReason As Short = 8
    Private Const ColFlag As Short = 9
    Private Const ColRePopulate As Short = 10

    Dim mcntRow As Integer
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkApproved_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkApproved.CheckStateChanged

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
            cmdPopulate.Enabled = True
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

        If PubUserID <> "G416" Then
            If chkApproved.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Voucher Already Approved, So cann't be Delete.")
                Exit Sub
            End If
        End If

        If Not RsPMemoMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PRD_WRBREAKUP_HDR ", (txtPMemoNo.Text), RsPMemoMain, "AUTO_KEY_REF") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_WRBREAKUP_HDR", "AUTO_KEY_REF", (lblMKey.Text)) = False Then GoTo DelErrPart

                If DeleteReworkTRN(PubDBCn, ConStockRefType_WRBREAKUP, (lblMKey.Text)) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_WRBREAKUP, (txtPMemoNo.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM PRD_WRBREAKUP_DET WHERE AUTO_KEY_REF=" & Val(lblMKey.Text) & "")
                PubDBCn.Execute("DELETE FROM PRD_WRBREAKUP_HDR  WHERE AUTO_KEY_REF=" & Val(lblMKey.Text) & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'")
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
            If PubUserID <> "G0416" Then
                If chkApproved.CheckState = System.Windows.Forms.CheckState.Checked Then
                    MsgInformation("Voucher Already Approved, So cann't be Modify.")
                    Exit Sub
                End If
            End If
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
    Private Sub cmdPopulate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulate.Click

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim mDeptSeq As Integer
        Dim mProdDept As String
        Dim RsBOM As ADODB.Recordset
        Dim mItemDesc As String
        Dim mItemCode As String
        Dim mItemUOM As String
        Dim mFactor As Double
        Dim mStdQty As Double
        Dim i As Integer
        Dim mSrn As String
        Dim mLevel As Integer
        Dim mProductCode As String


        If Trim(txtProductCode.Text) = "" Then
            MsgBox("Please Enter Product Code.")
            Exit Sub
        End If

        If Val(txtDismantleQty.Text) = 0 Then
            MsgBox("Please Enter Dismantle Qty.")
            Exit Sub
        End If

        If Val(txtDismantleQty.Text) + Val(txtDirectScrapWR.Text) > Val(txtAvailableQty.Text) Then
            MsgBox("Dismantle & Direct Scrap Qty cann't be greater than Available Qty.")
            Exit Sub
        End If

        mDeptSeq = GetMaxProductSeqNo(Trim(txtProductCode.Text), txtPMemoDate.Text)
        mProdDept = GetProductDept(Trim(txtProductCode.Text), mDeptSeq, txtPMemoDate.Text)

        mSqlStr = MakeBOMStockQty(Trim(txtProductCode.Text), mProdDept, mDeptSeq)

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)

        If mSqlStr = "" Then
            Exit Sub
        Else
            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOM, ADODB.LockTypeEnum.adLockReadOnly)
        End If

        mLevel = 1

        i = 0
        If RsBOM.EOF = False Then
            With SprdMain
                Do While RsBOM.EOF = False
                    i = i + 1

                    mSrn = Str(i)

                    '                mStdQty = (Val(IIf(IsNull(RsBOM!STD_QTY), 0, RsBOM!STD_QTY)))	
                    '	
                    '                Call FillGridCol(RsBOM, mSrn, mLevel, mProductCode, mProductCode, mStdQty)	


                    .Row = .MaxRows
                    .Col = ColItemCode
                    .Text = Trim(IIf(IsDBNull(RsBOM.Fields("RM_CODE").Value), "", RsBOM.Fields("RM_CODE").Value))
                    mItemCode = Trim(IIf(IsDBNull(RsBOM.Fields("RM_CODE").Value), "", RsBOM.Fields("RM_CODE").Value))

                    .Col = ColItemDesc
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Text = Trim(MasterNo)
                    Else
                        .Text = ""
                    End If

                    .Col = ColUom
                    .Text = Trim(IIf(IsDBNull(RsBOM.Fields("ISSUE_UOM").Value), "", RsBOM.Fields("ISSUE_UOM").Value))
                    mItemUOM = Trim(IIf(IsDBNull(RsBOM.Fields("ISSUE_UOM").Value), "", RsBOM.Fields("ISSUE_UOM").Value))

                    If UCase(Trim(mItemUOM)) = "TON" Then
                        mFactor = (1000 * 1000)
                    ElseIf UCase(Trim(mItemUOM)) = "KGS" Then
                        mFactor = (1000)
                    Else
                        mFactor = 1
                    End If

                    .Col = ColStockType
                    .Text = Trim(IIf(IsDBNull(RsBOM.Fields("STOCK_TYPE").Value), "", RsBOM.Fields("STOCK_TYPE").Value))

                    .Col = colStdQty
                    mStdQty = Val(CStr(IIf(IsDBNull(RsBOM.Fields("STD_QTY").Value), 0, RsBOM.Fields("STD_QTY").Value) + IIf(IsDBNull(RsBOM.Fields("GROSS_WT_SCRAP").Value), 0, RsBOM.Fields("GROSS_WT_SCRAP").Value))) / mFactor
                    .Text = CStr(mStdQty * (Val(txtDismantleQty.Text)))

                    .Col = ColQty
                    .Text = CStr(0)

                    .Col = ColScrapQty
                    .Text = CStr(0)

                    RsBOM.MoveNext()

                    .MaxRows = .MaxRows + 1

                Loop
            End With
        End If

        '    txtProductCode.Enabled = False	
        '    txtDismantleQty.Enabled = False	
        '    cmdSearchProductCode.Enabled = False	
        '    cmdPopulate.Enabled = False	
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub

    Private Function GetSTDQty(ByRef pRMCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemDesc As String
        Dim mStdQty As Double
        Dim mItemUOM As String
        Dim mFactor As Double
        Dim mDeptCode As String
        Dim mDeptSeq As Integer

        GetSTDQty = 0

        mDeptSeq = GetMaxProductSeqNo(Trim(txtProductCode.Text), (txtPMemoDate.Text))
        mDeptCode = GetProductDept(Trim(txtProductCode.Text), mDeptSeq, (txtPMemoDate.Text))

        SqlStr = "SELECT  SUM(ID.STD_QTY + ID.GROSS_WT_SCRAP) AS STD_QTY, INVMST.ISSUE_UOM " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST"

        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(txtProductCode.Text)) & "' " & vbCrLf _
            & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(UCase(pRMCode)) & "' "


        SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(UCase(mDeptCode)) & "' "

        SqlStr = SqlStr & vbCrLf & " AND IH.WEF=( " & vbCrLf & " SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(txtProductCode.Text)) & "' " & vbCrLf & " AND WEF<= TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf & " GROUP BY INVMST.ISSUE_UOM"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            mStdQty = IIf(IsDBNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value)
            mItemUOM = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

            If UCase(Trim(mItemUOM)) = "TON" Then
                mFactor = (1000 * 1000)
            ElseIf UCase(Trim(mItemUOM)) = "KGS" Then
                mFactor = (1000)
            Else
                mFactor = 1
            End If
            mStdQty = mStdQty / mFactor
            GetSTDQty = mStdQty * (Val(txtDismantleQty.Text))
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Function

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mSlipNo As String
        Dim mRPTName As String
        mSlipNo = CStr(Val(txtPMemoNo.Text))

        Report1.Reset()
        '    SqlStr = "SELECT * " & vbCrLf _	
        ''            & " FROM PRD_SENDBACKFORRWK_HDR IH, PRD_SENDBACKFORRWK_DET ID, INV_ITEM_MST INVMST, PAY_EMPLOYEE_MST EMP" & vbCrLf _	
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
        ''            & " AND IH.AUTO_KEY_SBRWK=ID.AUTO_KEY_SBRWK" & vbCrLf _	
        ''            & " AND IH.COMPANY_CODE=EMP.COMPANY_CODE(+)" & vbCrLf _	
        ''            & " AND IH.SHIFT_EMP_CODE=EMP.EMP_CODE(+)" & vbCrLf _	
        ''            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _	
        ''            & " AND ID.PRODUCT_CODE=INVMST.ITEM_CODE" & vbCrLf _	
        ''            & " AND IH.AUTO_KEY_SBRWK=" & mSlipNo & ""	

        mTitle = "Material Scrap Note"
        mSubTitle = ""
        mRPTName = "ReworkScrap.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRPTName)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume	

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

        If MainClass.SearchGridMaster(txtPMemoNo.Text, "PRD_WRBREAKUP_HDR ", "AUTO_KEY_REF", "REF_DATE", , , SqlStr) = True Then
            txtPMemoNo.Text = AcName
            If txtPMemoNo.Enabled = True Then txtPMemoNo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEmp.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtEmp.Text = AcName1
            lblEmp.Text = AcName
            If txtEmp.Enabled = True Then txtEmp.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdSearchProductCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProductCode.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtProductCode.Text = AcName1
            lblProductCode.Text = AcName
            If txtProductCode.Enabled = True Then txtProductCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub FrmReworkBreakup_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        'Dim xIName As String
        Dim SqlStr As String = ""
        'Dim pOPRCode As String
        'Dim mProductCode As String
        'Dim pOPRDesc As String
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
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = Trim(AcName)

                    .Col = ColItemDesc
                    .Text = Trim(AcName1)
                    Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
                End If

            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemDesc
                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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

        '    If eventArgs.Col = 0 And eventArgs.Row > 0 Then	
        '        SprdMain.Row = Row	
        '        SprdMain.Col = ColItemCode	
        '        If Row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then	
        '            MainClass.DeleteSprdRow SprdMain, Row, ColItemCode	
        '            MainClass.SaveStatus Me, ADDMode, MODIFYMode	
        '            FormatSprdMain Row	
        '        End If	
        '    End If	

    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
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
        If Trim(SprdMain.Text) = "" Then Exit Sub
        Select Case eventArgs.col
            '        Case ColItemCode	
            '            SprdMain.Row = SprdMain.ActiveRow	
            '            SprdMain.Col = ColItemCode	
            '            If DuplicateItem = False Then	
            '                SprdMain.Row = SprdMain.ActiveRow	
            '                SprdMain.Col = ColItemCode	
            '                If FillItemDescPart(Trim(SprdMain.Text), mDivisionCode) = False Then	
            '                    MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColItemCode	
            '                    Cancel = True	
            '                    Exit Sub	
            '                Else	
            '                    MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight	
            '                    FormatSprdMain SprdMain.MaxRows	
            '                End If	
            '            Else	
            '                MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColItemCode	
            '                Cancel = True	
            '                Exit Sub	
            '            End If	
            Case ColQty
                If CheckQty() = True Then
                    '                MainClass.SetFocusToCell SprdMain, .ActiveRow, ColQty	
                    '                MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight	
                    '                FormatSprdMain SprdMain.MaxRows	
                End If
            Case ColStockType
                Call CheckStockType()
        End Select
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
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

    Private Sub CheckStockType()

        On Error GoTo ChkERR
        Dim mStockType As String

        With SprdMain
            .Row = .ActiveRow
            .Col = ColStockType
            If Trim(.Text) = "" Then Exit Sub
            If MainClass.ValidateWithMasterTable(Trim(.Text), "STOCK_TYPE_CODE", "STOCK_TYPE_CODE", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mStockType = MasterNo
                '            If Trim(mStockType) <> "FG" Then	
                '                MsgInformation "Please Select 'FG' Stock Type."	
                '                MainClass.SetFocusToCell SprdMain, .ActiveRow, ColStockType	
                '                Exit Sub	
                '            End If	
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
    Private Function CheckPendingSlipforApproval(ByRef mPendingRefNo As String) As Boolean

        On Error GoTo ChkERR
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        mPendingRefNo = ""
        CheckPendingSlipforApproval = False

        SqlStr = "SELECT AUTO_KEY_REF FROM PRD_WRBREAKUP_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_SBRWK=" & Val(txtSBSlipNo.Text) & " AND APPROVED='N'"

        SqlStr = SqlStr & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'"

        If Val(txtPMemoNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_REF<>" & Val(txtPMemoNo.Text) & ""
        End If



        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPendingRefNo = Trim(IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_REF").Value), "", RsTemp.Fields("AUTO_KEY_REF").Value))
            CheckPendingSlipforApproval = True
        End If

        Exit Function
ChkERR:
        MsgBox(Err.Description)
    End Function
    Private Function CheckQty() As Boolean
        On Error GoTo ERR1
        Dim mOKQty As Double
        Dim mScrapQty As Double
        Dim mStdQty As Double

        CheckQty = True
        Exit Function

        '    With SprdMain	
        '        .Row = .ActiveRow	
        '        .Col = ColQty	
        '        mOKQty = Val(.Text)	
        '	
        '        .Col = ColScrapQty	
        '        mScrapQty = Val(.Text)	
        '	
        '        .Col = ColStdQty	
        '        mStdQty = Val(.Text)	
        '	
        '        If mProdQty < mOKQty Then	
        '            CheckQty = False	
        '        Else	
        '            CheckQty = True	
        '        End If	
        '    End With	
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

        FillItemDescPart = False
        If Trim(pItemCode) = "" Then Exit Function

        SqlStr = "SELECT ITEM_CODE, ITEM_SHORT_DESC,ISSUE_UOM,CATEGORY_CODE " & vbCrLf _
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

                '            .Col = ColStockType	
                '            .Text = "FG"	
                '	
                '            .Col = ColStdQty	
                '            .Text = GetBalanceStockQty(mProdItemCode, txtPMemoDate.Text, mItemUOM, "STR", "FG", "", ConWH, mDivisionCode, ConStockRefType_WRBREAKUP, Val(txtPMemoNo.Text))	

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

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_REF)  " & vbCrLf & " FROM PRD_WRBREAKUP_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

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
        Dim mApproved As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime()

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If


        mApproved = IIf(chkApproved.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")



        SqlStr = ""
        mPMemoNo = Val(txtPMemoNo.Text)
        If Val(txtPMemoNo.Text) = 0 Then
            mPMemoNo = CDbl(AutoGenMemoNo())
        End If
        txtPMemoNo.Text = CStr(mPMemoNo)
        If ADDMode = True Then
            lblMKey.Text = CStr(mPMemoNo)
            SqlStr = " INSERT INTO PRD_WRBREAKUP_HDR  " & vbCrLf _
                & " (COMPANY_CODE, FYEAR, AUTO_KEY_REF," & vbCrLf _
                & " REF_DATE, EMP_CODE, DEPT_CODE,PRODUCT_CODE, PROD_QTY,  REMARKS, " & vbCrLf _
                & " BOOKTYPE, ADDUSER, ADDDATE, " & vbCrLf _
                & " MODUSER, MODDATE, DIV_CODE, AUTO_KEY_SBRWK,SB_DATE, MATERIAL_COST,APPROVED,WR_SCRAP_QTY) " & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & "," & mPMemoNo & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtEmp.Text) & "','" & MainClass.AllowSingleQuote(txtDept.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'," & Val(txtDismantleQty.Text) & ", '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " '" & VB.Left(lblBookType.Text, 1) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'',''," & mDivisionCode & ", " & vbCrLf _
                & " " & Val(txtSBSlipNo.Text) & ",TO_DATE('" & VB6.Format(txtSBSlipDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(lblMaterialCost.Text) & ",'" & mApproved & "'," & Val(txtDirectScrapWR.Text) & ")"


        ElseIf MODIFYMode = True Then
            mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime()
            SqlStr = " UPDATE PRD_WRBREAKUP_HDR  SET " & vbCrLf _
                & " AUTO_KEY_REF=" & mPMemoNo & ", APPROVED='" & mApproved & "'," & vbCrLf _
                & " REF_DATE=TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " EMP_CODE='" & MainClass.AllowSingleQuote(txtEmp.Text) & "', DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'," & vbCrLf _
                & " PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf _
                & " PROD_QTY=" & Val(txtDismantleQty.Text) & ", WR_SCRAP_QTY=" & Val(txtDirectScrapWR.Text) & "," & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'," & vbCrLf _
                & " AUTO_KEY_SBRWK=" & Val(txtSBSlipNo.Text) & "," & vbCrLf _
                & " SB_DATE=TO_DATE('" & VB6.Format(txtSBSlipDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " MATERIAL_COST=" & Val(lblMaterialCost.Text) & "," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'), DIV_CODE=" & mDivisionCode & "" & vbCrLf _
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
        Dim mStockType As String
        Dim mProdQty As Double
        Dim xStockRowNo As Integer
        Dim mReason As String
        Dim mDeptCode As String

        Dim xItemCost As Double
        Dim mInCCCode As String
        Dim mWIPStock As Double
        Dim mWIPReworkStock As Double
        Dim mSqlStr As String
        Dim RsBOM As ADODB.Recordset
        Dim mProductSeqNo As Integer
        Dim mProductionDate As String
        'Dim mEntryDate As String	

        Dim mToolNo As String
        Dim mTotalOpr As Integer
        Dim mDeptSeq As Integer
        Dim xOPStockType As String
        Dim pOPRCode As String
        Dim pOPRDesc As String
        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mOperatorCode As String
        Dim mScrapQty As Double
        Dim xWareHouse As String
        Dim mSupplierCode As String
        Dim mOrgBillNO As Double
        Dim mOrdBillDate As String
        Dim mItemRate As Double
        Dim xFGBatchNo As String
        Dim xFGBatchNoReq As String

        If MainClass.ValidateWithMasterTable("STR", "DEPT_CODE", "CCCODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInCCCode = IIf(IsDBNull(MasterNo), "-1", MasterNo)
        Else
            mInCCCode = "-1"
        End If

        SqlStr = " DELETE FROM PRD_WRBREAKUP_DET " & vbCrLf & " WHERE AUTO_KEY_REF=" & Val(lblMKey.Text) & " "
        PubDBCn.Execute(SqlStr)

        If DeleteReworkTRN(PubDBCn, ConStockRefType_WRBREAKUP, (txtPMemoNo.Text)) = False Then GoTo UpdateDetail1Err

        If DeleteStockTRN(PubDBCn, ConStockRefType_WRBREAKUP, (txtPMemoNo.Text)) = False Then GoTo UpdateDetail1Err
        xStockRowNo = 1


        '    mDeptSeq = GetMaxProductSeqNo(Trim(txtProductCode.Text), txtPMemoDate.Text)	
        mDeptCode = Trim(txtDept.Text) ''GetProductDept(Trim(txtProductCode.Text), mDeptSeq, txtPMemoDate.Text)	

        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColUom
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColQty
                mProdQty = Val(.Text)

                .Col = ColScrapQty
                mScrapQty = Val(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                .Col = ColReason
                mReason = Trim(.Text)

                If mItemCode <> "" And (mProdQty + mScrapQty) > 0 Then
                    SqlStr = " INSERT INTO PRD_WRBREAKUP_DET ( " & vbCrLf & " COMPANY_CODE,AUTO_KEY_REF,SERIAL_NO,ITEM_CODE,ITEM_DESC, " & vbCrLf & " ITEM_UOM,STOCK_TYPE, ITEM_QTY, SCRAP_QTY, REASON) " & vbCrLf & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMKey.Text) & ", " & i & "," & vbCrLf & " '" & mItemCode & "','" & mItemDesc & "', " & vbCrLf & " '" & mUOM & "','" & mStockType & "', " & vbCrLf & " " & mProdQty & ", " & mScrapQty & ", '" & MainClass.AllowSingleQuote(mReason) & "')"

                    PubDBCn.Execute(SqlStr)

                    If lblApproval.Text = "Y" Then
                        xWareHouse = "PH"
                        If mProdQty > 0 Then
                            xStockRowNo = xStockRowNo + 1
                            If UpdateStockTRN(PubDBCn, ConStockRefType_WRBREAKUP, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "ST", mItemCode, mUOM, CStr(-1), mProdQty, 0, "I", xItemCost, xItemCost, "", "", mDeptCode, mDeptCode, mInCCCode, "N", "TO : " & mDeptCode & " (Rework Dismantle) -" & ConStockRefType_WRBREAKUP & "-" & Trim(txtProductCode.Text), "-1", xWareHouse, mDivisionCode, "", Trim(txtProductCode.Text)) = False Then GoTo UpdateDetail1Err

                        End If

                        If mScrapQty > 0 Then
                            xStockRowNo = xStockRowNo + 1
                            If UpdateStockTRN(PubDBCn, ConStockRefType_WRBREAKUP, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "RS", mItemCode, mUOM, CStr(-1), mScrapQty, 0, "I", xItemCost, xItemCost, "", "", mDeptCode, "PAD", mInCCCode, "N", "TO : " & mDeptCode & " (" & IIf(lblBookType.Text = "F", "FG", "CR") & " Dismantle) -" & ConStockRefType_WRBREAKUP & "-" & Trim(txtProductCode.Text), "-1", xWareHouse, mDivisionCode, "", Trim(txtProductCode.Text)) = False Then GoTo UpdateDetail1Err

                        End If
                    End If
                End If
NextRec:
            Next
        End With

        If lblApproval.Text = "Y" Then

            If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                If Val(txtBatchNo.Text) > 0 Then
                    xFGBatchNo = CStr(Val(txtBatchNo.Text))
                Else
                    If Val(txtBatchNo.Text) = -1 Then
                        xFGBatchNo = CStr(Val(txtBatchNo.Text))
                    End If
                End If
                xFGBatchNoReq = "Y"
            Else
                xFGBatchNo = CStr(-1)
                xFGBatchNoReq = "N"
            End If

            If GetSBData(CDbl(txtSBSlipNo.Text), Trim(txtProductCode.Text), mItemRate) = False Then GoTo UpdateDetail1Err

            If Val(txtDismantleQty.Text) > 0 Then
                xStockRowNo = xStockRowNo + 1
                If UpdateStockTRN(PubDBCn, ConStockRefType_WRBREAKUP, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "WR", Trim(txtProductCode.Text), Trim(lblProductionUOM.Text), xFGBatchNo, Val(txtDismantleQty.Text), 0, "O", xItemCost, xItemCost, "-1", "", mDeptCode, mDeptCode, mInCCCode, "N", "From : Rework (Dismantle) -" & ConStockRefType_WRBREAKUP, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err

                If UpdateReworkTRN(PubDBCn, Val(txtPMemoNo.Text), (txtPMemoDate.Text), ConStockRefType_WRBREAKUP, (txtSBSlipNo.Text), (txtSBSlipDate.Text), Trim(txtProductCode.Text), Val(txtDismantleQty.Text) + Val(txtDirectScrapWR.Text), lblProductionUOM.Text, mItemRate, "WR", "O", (txtPMemoDate.Text), Val(CStr(mDivisionCode)), (txtDept.Text), xFGBatchNo) = False Then GoTo UpdateDetail1Err
            End If

            If Val(txtDirectScrapWR.Text) > 0 Then
                xStockRowNo = xStockRowNo + 1
                If UpdateStockTRN(PubDBCn, ConStockRefType_WRBREAKUP, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "WR", Trim(txtProductCode.Text), Trim(lblProductionUOM.Text), xFGBatchNo, Val(txtDirectScrapWR.Text), 0, "O", xItemCost, xItemCost, "-1", "", mDeptCode, mDeptCode, mInCCCode, "N", "From : Rework (Dismantle) -" & ConStockRefType_WRBREAKUP, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err

                xStockRowNo = xStockRowNo + 1
                If UpdateStockTRN(PubDBCn, ConStockRefType_WRBREAKUP, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "RS", Trim(txtProductCode.Text), Trim(lblProductionUOM.Text), xFGBatchNo, Val(txtDirectScrapWR.Text), 0, "I", xItemCost, xItemCost, "-1", "", mDeptCode, mDeptCode, mInCCCode, "N", "From : Rework (Dismantle) -" & ConStockRefType_WRBREAKUP, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err

                If UpdateReworkTRN(PubDBCn, Val(txtPMemoNo.Text), (txtPMemoDate.Text), ConStockRefType_WRBREAKUP, (txtSBSlipNo.Text), (txtSBSlipDate.Text), Trim(txtProductCode.Text), Val(txtDirectScrapWR.Text), lblProductionUOM.Text, mItemRate, "WR", "O", (txtPMemoDate.Text), Val(CStr(mDivisionCode)), (txtDept.Text), xFGBatchNo) = False Then GoTo UpdateDetail1Err

                If UpdateReworkTRN(PubDBCn, Val(txtPMemoNo.Text), (txtPMemoDate.Text), ConStockRefType_WRBREAKUP, (txtSBSlipNo.Text), (txtSBSlipDate.Text), Trim(txtProductCode.Text), Val(txtDirectScrapWR.Text), lblProductionUOM.Text, mItemRate, "RS", "I", (txtPMemoDate.Text), Val(CStr(mDivisionCode)), (txtDept.Text), xFGBatchNo) = False Then GoTo UpdateDetail1Err

            End If
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
    Private Function MakeBOMStockQty(ByRef mSFICode As String, ByRef mDeptCode As String, ByRef pDeptSeq As Integer) As String
        On Error GoTo BOMStockErr
        Dim SqlStr As String = ""


        SqlStr = "SELECT  IH.MKEY, IH.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC, " & vbCrLf & " ID.RM_CODE, ID.STD_QTY, ID.DEPT_CODE," & vbCrLf & " ID.GROSS_WT_SCRAP, INVMST.ISSUE_UOM, INVMST.SCRAP_ITEM_CODE, FROM_SCRAP, ID.STOCK_TYPE "

        SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST"

        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(mSFICode)) & "' "

        '    If pDeptSeq = 1 Then	
        '        SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(UCase(mDeptCode)) & "' "	
        '    Else	
        '        SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE IN ( " & vbCrLf _	
        ''                & " SELECT DEPT_CODE FROM PRD_PRODSEQUENCE_DET" & vbCrLf _	
        ''                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
        ''                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mSFICode) & "'" & vbCrLf _	
        ''                & " AND SERIAL_NO<=" & Val(pDeptSeq) & ")"	
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

    Private Sub FillGridCol(ByRef pRs As ADODB.Recordset, ByRef pSRNo As String, ByRef pLevel As Integer, ByRef pProductCode As String, ByRef pParentCode As String, ByRef mStqQty As Double)

        On Error GoTo FillGERR
        Dim mDrgRevNo As String
        Dim mRMCode As String
        Dim mItemUOM As String
        Dim mStockQty As Double
        Dim mDeptCode As String
        Dim mRate As Double
        Dim mFactorQty As Double
        Dim mChildRMCode As String
        'Dim mStqQty As Double	
        Dim mTotValue As Double
        Dim mUOM As String
        Dim mTotClosing As Double

        With SprdMain

            mRMCode = Trim(IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
            If CheckSubRecord(mRMCode) = True Then
                pLevel = pLevel + 1
                Call FillSubRecord(mRMCode, "", pSRNo, pLevel, pProductCode, mStqQty)

            Else
                .Row = .MaxRows

                mDeptCode = IIf(IsDBNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)
                mItemUOM = IIf(IsDBNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)

                .Col = ColItemCode
                .Text = Trim(IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
                mRMCode = Trim(IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColItemDesc
                .Text = IIf(IsDBNull(pRs.Fields("ITEM_SHORT_DESC").Value), "", pRs.Fields("ITEM_SHORT_DESC").Value)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColUom
                .Text = Trim(mItemUOM)

                .Col = ColStockType
                .Text = "ST" ''Trim(IIf(IsNull(pRs!STOCK_TYPE), "", pRs!STOCK_TYPE))	


                .Col = colStdQty
                '            If optCalcOn(0).Value = True Then	
                mStqQty = mStqQty * (Val(IIf(IsDBNull(pRs.Fields("STD_QTY").Value), 0, pRs.Fields("STD_QTY").Value))) * (Val(txtDismantleQty.Text))
                '            Else	
                '                mStqQty = mStqQty * ((Val(IIf(IsNull(pRs!STD_QTY), 0, pRs!STD_QTY)) + Val(IIf(IsNull(pRs!GROSS_WT_SCRAP), 0, pRs!GROSS_WT_SCRAP))))	
                '            End If	

                mFactorQty = 1
                If mDeptCode = "J/W" Then
                    If mItemUOM = "TON" Then
                        mFactorQty = 1 / 1000
                    End If
                Else
                    If mItemUOM = "KGS" Then
                        mFactorQty = 1 / 1000
                    ElseIf mItemUOM = "TON" Then
                        mFactorQty = 1 / 1000
                        mFactorQty = mFactorQty / 1000
                    End If
                End If

                .Text = CStr(mStqQty * mFactorQty)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                mStqQty = 1

                SprdMain.MaxRows = SprdMain.MaxRows + 1
                mcntRow = mcntRow + 1
            End If
        End With

        Exit Sub
FillGERR:
        Resume
        MsgBox(Err.Description)
    End Sub
    Private Function CheckSubRecord(ByRef pProductCode As String) As Boolean


        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String
        'Dim mSrn As String	
        'Dim xSrn As String	
        'Dim j As Long	
        '	
        CheckSubRecord = False
        '    If GetProductionType(pProductCode) = "B" Then	
        '        Exit Function	
        '    End If	

        SqlStr = " SELECT " & vbCrLf _
            & " IH.PRODUCT_CODE, ID.RM_CODE " & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O' AND IH.IS_BOP='N'" & vbCrLf _
            & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O' ) "  '& vbCrLf |            & " AND WEF<= '" & vb6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf |	
        '    SqlStr = SqlStr & vbCrLf & " AND ID.RM_CODE NOT "	

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            '        Do While Not RsShow.EOF	
            mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
            If GetProductionType(mRMCode) = "B" Then
                CheckSubRecord = False
                Exit Function
            Else
                CheckSubRecord = True
            End If
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, ID.ITEM_CODE AS RM_CODE " & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'  AND STATUS='O' AND IS_INHOUSE='N' AND IH.IS_BOP='N'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O') "

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                '            Do While Not RsShow.EOF	
                mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                If GetProductionType(mRMCode) = "B" Then
                    CheckSubRecord = False
                    Exit Function
                Else
                    CheckSubRecord = True
                End If
                RsShow.MoveNext()
                '            Loop	
            End If
        End If
        RsShow = Nothing
        '        RsShow.Close	

        Exit Function
FillERR:
        CheckSubRecord = False
        MsgBox(Err.Description)
        '    Resume	
    End Function


    Private Sub FillSubRecord(ByRef pProductCode As String, ByRef pWEF As String, ByVal pSrn As String, ByRef pLevel As Integer, ByRef pMainProductCode As String, ByRef mStqQty As Double)


        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String
        Dim mSrn As String
        Dim xSrn As String
        Dim j As Integer

        mSrn = pSrn
        pLevel = pLevel + 1
        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " " & mStqQty & " * ID.STD_QTY * (CASE WHEN INVMST.ISSUE_UOM='KGS' THEN 0.001 WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 * 0.001 ELSE 1 END) AS STD_QTY, " & mStqQty & " * ID.GROSS_WT_SCRAP * (CASE WHEN INVMST.ISSUE_UOM='KGS' THEN 0.001 WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 * 0.001 ELSE 1 END) AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

        SqlStr = SqlStr & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O'" & vbCrLf _
            & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O') "  '& vbCrLf |            & " AND WEF<= '" & vb6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf |	

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                '            mcntRow = mcntRow + 1	
                '            SprdMain.MaxRows = SprdMain.MaxRows + 1	
                SprdMain.Row = mcntRow

                j = j + 1
                xSrn = mSrn & "." & j
                pSrn = pSrn & "." & j

                mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                '            If optCalcOn(0).Value = True Then	
                mStqQty = (Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)))
                '            Else	
                '                mStqQty = ((Val(IIf(IsNull(RsShow!STD_QTY), 0, RsShow!STD_QTY)))) '' + Val(IIf(IsNull(RsShow!GROSS_WT_SCRAP), 0, RsShow!GROSS_WT_SCRAP))))	
                '            End If	
                Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pProductCode, mStqQty)
                RsShow.MoveNext()
            Loop
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ITEM_CODE AS RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " " & mStqQty & " * ID.ITEM_QTY * (CASE WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 ELSE 1 END) AS STD_QTY, " & mStqQty & " * ID.SCRAP_QTY * (CASE WHEN INVMST.ISSUE_UOM='TON' THEN 0.001 ELSE 1 END) AS GROSS_WT_SCRAP ," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

            SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O' AND IS_INHOUSE='N'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O') "

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                Do While Not RsShow.EOF
                    '                mcntRow = mcntRow + 1	
                    '                SprdMain.MaxRows = SprdMain.MaxRows + 1	
                    SprdMain.Row = mcntRow

                    j = j + 1
                    xSrn = mSrn & "." & j
                    pSrn = pSrn & "." & j

                    '                If optCalcOn(0).Value = True Then	
                    mStqQty = (Val(IIf(IsDBNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)))
                    '                Else	
                    '                    mStqQty = ((Val(IIf(IsNull(RsShow!STD_QTY), 0, RsShow!STD_QTY)))) '' + Val(IIf(IsNull(RsShow!GROSS_WT_SCRAP), 0, RsShow!GROSS_WT_SCRAP))))	
                    '                End If	
                    mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                    Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pProductCode, mStqQty)
                    RsShow.MoveNext()
                Loop
            End If
        End If
        RsShow = Nothing
        '        RsShow.Close	

        Exit Sub
FillERR:
        MsgBox(Err.Description)
        '    Resume	
    End Sub
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
        Dim mScrapQty As Double
        Dim mPendingRefNo As String

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

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If

        If txtEmp.Text = "" Then
            MsgBox("Employee is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtEmp.Focus()
            Exit Function
        End If

        If txtDept.Text = "" Then
            MsgBox("Department is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDept.Focus()
            Exit Function
        End If

        If lblApproval.Text = "Y" And ADDMode = True Then
            MsgBox("Cann't be Add New Record in Approval Form.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtDismantleQty.Text) + Val(txtDirectScrapWR.Text) > Val(txtAvailableQty.Text) Then
            MsgBox("Dismantle & Direct Scrap Qty cann't be greater than Available Qty.")
            FieldsVarification = False
            Exit Function
        End If


        If PubUserID <> "G0416" Then
            If CheckPendingSlipforApproval(mPendingRefNo) = True Then
                MsgBox("Ref No : " & mPendingRefNo & " is Pending agt this Slip No, Please approve this Ref no. first. ", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        Dim mScrapCode As String = ""
        Dim mWeight As Double = 0

        If Val(txtDirectScrapWR.Text) > 0 Then
            If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "SCRAP_ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mScrapCode = MasterNo
            End If

            If Trim(mScrapCode) = "" Then
                MsgInformation("Scrap Item Code not Defined for the Item. " & txtProductCode.Text)
                FieldsVarification = False
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ITEM_WEIGHT", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWeight = Val(MasterNo)
            End If

            If mWeight <= 0 Then
                MsgInformation("Item Weight not Defined for the Item. " & txtProductCode.Text)
                FieldsVarification = False
                Exit Function
            End If
        End If

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

                '            If mProductCode <> "" Then	
                '                SqlStr = " SELECT PRODUCT_CODE " & vbCrLf _	
                ''                        & " FROM PRD_NEWBOM_HDR" & vbCrLf _	
                ''                        & " WHERE " & vbCrLf _	
                ''                        & " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
                ''                        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"	
                '	
                '                MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly	
                '	
                '                If RsTemp.EOF = True Then	
                '                    MsgInformation "Please Defined B.O.M. For Product Code : " & mProductCode & ". Cann't Be Saved"	
                '                    FieldsVarification = False	
                '    '                MainClass.SetFocusToCell SprdMain, CntRow, ColItemCode	
                '                    Exit Function	
                '                End If	
                '           End If	

                .Col = ColQty
                mProdQty = Val(.Text)

                .Col = colStdQty
                mStockQty = Val(.Text)

                .Col = ColScrapQty
                mScrapQty = Val(.Text)

                'If mStockQty <> mProdQty + mScrapQty Then
                '    MsgInformation("Standard Qty should be equal to OK Qty Plus Scrap Qty. Item Code : " & mProductCode & " Qty not match. Cann't Be Saved")
                '    FieldsVarification = False
                '    MainClass.SetFocusToCell(SprdMain, cntRow, ColQty)
                '    Exit Function
                'End If
                .Col = ColFlag
                If Trim(.Text) <> "1" Then
                    If mStockQty <> mProdQty + mScrapQty Then
                        MsgInformation("Standard Qty should be equal to OK Qty Plus Scrap Qty. Item Code : " & mProductCode & " Qty not match. Cann't Be Saved")
                        FieldsVarification = False
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColQty)
                        Exit Function
                    End If
                End If

                .Col = ColScrapQty

                If Val(.Text) > 0 Then
                    If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "SCRAP_ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mScrapCode = MasterNo
                    End If

                    If Trim(mScrapCode) = "" Then
                        MsgInformation("Scrap Item Code not Defined for the Item. " & mProductCode)
                        FieldsVarification = False
                        Exit Function
                    End If

                    If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "ITEM_WEIGHT", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mWeight = Val(MasterNo)
                    End If

                    If mWeight <= 0 Then
                        MsgInformation("Item Weight not Defined for the Item. " & mProductCode)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If

            Next
        End With

        If CheckRowCount() = False Then
            FieldsVarification = False
            Exit Function
        End If

        '    If ValidateDeptRight(PubUserID, "PAD", "PAD") = False Then	
        '        FieldsVarification = False	
        '        Exit Function	
        '    End If	

        mCheckLastEntryDate = GetLastEntryDate()

        If PubSuperUser = "U" Then
            If mCheckLastEntryDate <> "" Then
                If CDate(txtPMemoDate.Text) < CDate(mCheckLastEntryDate) Then
                    MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
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

        If Val(txtDismantleQty.Text) > 0 Then
            If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
            '    If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Produce Quantity.") = False Then FieldsVarification = False: Exit Function	
            If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function
        End If

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Function

    Private Function GetLastEntryDate() As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = ""

        SqlStr = "SELECT Max(REF_DATE) AS  REF_DATE " & vbCrLf & " FROM PRD_WRBREAKUP_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

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
        Dim mScrapQty As Double
        Dim mTotScrapQty As Double

        mRowCount = 0
        mTotQty = 0
        CheckRowCount = True
        With SprdMain
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColQty
                mQty = Val(.Text)
                mTotQty = mTotQty + mQty

                .Col = ColScrapQty
                mScrapQty = Val(.Text)
                mTotScrapQty = mTotScrapQty + mScrapQty

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

        If Val(txtDismantleQty.Text) > 0 Then
            If System.Math.Abs(mTotQty) + System.Math.Abs(mTotScrapQty) = 0 Then
                CheckRowCount = False
                MsgInformation("Nothing To Save.")
                Exit Function
            End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
        CheckRowCount = False
    End Function

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmReworkBreakup_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Rework Scrap Note" & IIf(lblApproval.Text = "Y", " (Approval)", "")

        SqlStr = ""
        SqlStr = "Select * from PRD_WRBREAKUP_HDR  Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_WRBREAKUP_DET Where 1<>1"
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
        SqlStr = " SELECT  AUTO_KEY_REF REF_NO, REF_DATE, " & vbCrLf _
            & " PRODUCT_CODE PROD_QTY,DIV_CODE,REMARKS " & vbCrLf & " FROM PRD_WRBREAKUP_HDR  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "


        SqlStr = SqlStr & vbCrLf & " ORDER BY REF_DATE, AUTO_KEY_REF"
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
        With SprdMain
            .set_RowHeight(0, ConRowHeight * 2)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.25)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPMemoDetail.Fields("ITEM_CODE").DefinedSize
            .set_ColWidth(.Col, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsPMemoDetail.Fields("ITEM_DESC").DefinedSize
            .set_ColWidth(.Col, 30)

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsPMemoDetail.Fields("ITEM_UOM").DefinedSize
            .set_ColWidth(.Col, 6)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)

            .Col = ColScrapQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)

            .Col = colStdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsPMemoDetail.Fields("STOCK_TYPE").DefinedSize
            .set_ColWidth(.Col, 4)

            .Col = ColReason
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsPMemoDetail.Fields("REASON").DefinedSize
            .set_ColWidth(.Col, 14)
            .ColHidden = True

            .Col = ColFlag
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(.Col, 5)
            .ColHidden = True

            .Col = ColRePopulate
            .CellType = SS_CELL_TYPE_BUTTON
            '.Lock = False
            .TypeButtonText = "Re-Populate"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColRePopulate, 8)
            .ColHidden = False

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColUom)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockType, colStdQty)
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsPMemoDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsPMemoMain
            txtPMemoNo.MaxLength = .Fields("AUTO_KEY_REF").Precision
            txtPMemoDate.MaxLength = 10

            txtEmp.MaxLength = .Fields("EMP_CODE").DefinedSize
            txtDept.MaxLength = .Fields("DEPT_CODE").DefinedSize
            txtRemarks.MaxLength = .Fields("REMARKS").DefinedSize
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mProdType As String
        Dim mEntryDate As String
        Dim mDivisionCode As Integer
        Dim mDivisionDesc As String
        Dim mItemUOM As String
        Dim mAvailable As Double
        Dim mApproved As String
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        With RsPMemoMain
            If Not .EOF Then
                lblMKey.Text = .Fields("AUTO_KEY_REF").Value
                txtPMemoNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_REF").Value), "", .Fields("AUTO_KEY_REF").Value)
                txtPMemoDate.Text = VB6.Format(IIf(IsDBNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")

                txtSBSlipNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_SBRWK").Value), "", .Fields("AUTO_KEY_SBRWK").Value)
                txtSBSlipDate.Text = VB6.Format(IIf(IsDBNull(.Fields("SB_DATE").Value), "", .Fields("SB_DATE").Value), "DD/MM/YYYY")
                lblMaterialCost.Text = VB6.Format(IIf(IsDBNull(.Fields("MATERIAL_COST").Value), 0, .Fields("MATERIAL_COST").Value), "0.00")

                txtEmp.Text = IIf(IsDBNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtEmp_Validating(txtEmp, New System.ComponentModel.CancelEventArgs(False))

                txtDept.Text = IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)
                txtDept_Validating(txtDept, New System.ComponentModel.CancelEventArgs(False))

                txtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)

                mDivisionCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If


                mApproved = IIf(IsDBNull(.Fields("APPROVED").Value), "N", .Fields("APPROVED").Value)
                chkApproved.CheckState = IIf(mApproved = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkApproved.Enabled = IIf(mApproved = "Y", False, IIf(lblApproval.Text = "N", False, True))



                txtProductCode.Text = Trim(IIf(IsDBNull(.Fields("PRODUCT_CODE").Value), "", .Fields("PRODUCT_CODE").Value))
                txtDismantleQty.Text = VB6.Format(IIf(IsDBNull(.Fields("PROD_QTY").Value), "0.00", .Fields("PROD_QTY").Value), "0.00")
                txtDirectScrapWR.Text = VB6.Format(IIf(IsDBNull(.Fields("WR_SCRAP_QTY").Value), "0.00", .Fields("WR_SCRAP_QTY").Value), "0.00")

                If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblProductCode.Text = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblProductionUOM.Text = MasterNo
                    mItemUOM = MasterNo
                End If

                If Val(txtBatchNo.Text) <> 0 Then
                    If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                        mBatchNo = Trim(txtBatchNo.Text)
                        xFGBatchNoReq = "Y"
                    Else
                        mBatchNo = ""
                        xFGBatchNoReq = "N"
                    End If
                End If

                mAvailable = GetReworkStockQty(CDbl(Trim(txtSBSlipNo.Text)), Trim(txtProductCode.Text), (txtDept.Text), mDivisionCode, "WR", ConStockRefType_WRBREAKUP, Val(txtPMemoNo.Text), mBatchNo, xFGBatchNoReq)
                txtAvailableQty.Text = VB6.Format(mAvailable, "0.00")

                txtSBSlipNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_SBRWK").Value), "", .Fields("AUTO_KEY_SBRWK").Value)
                txtSBSlipDate.Text = VB6.Format(IIf(IsDBNull(.Fields("SB_DATE").Value), "", .Fields("SB_DATE").Value), "DD/MM/YYYY")
                lblMaterialCost.Text = VB6.Format(IIf(IsDBNull(.Fields("MATERIAL_COST").Value), 0, .Fields("MATERIAL_COST").Value), "0.00")

                cboDivision.Enabled = False

                Call ShowDetail1(mDivisionCode)
                Call MakeEnableDesableField(False)
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        txtPMemoNo.Enabled = True
        cmdSearch.Enabled = True

        txtProductCode.Enabled = False
        txtDismantleQty.Enabled = False
        txtDirectScrapWR.Enabled = False
        cmdSearchProductCode.Enabled = False
        cmdPopulate.Enabled = False

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub
    Private Sub ShowDetail1(ByRef mDivisionCode As Integer)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mProdItemCode As String
        Dim mItemUOM As String


        SqlStr = " SELECT * " & vbCrLf & " FROM PRD_WRBREAKUP_DET  " & vbCrLf & " WHERE AUTO_KEY_REF = " & Val(lblMKey.Text) & " " & vbCrLf & " ORDER BY  SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPMemoDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            i = 1
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = i

                SprdMain.Col = ColItemCode
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                mProdItemCode = Trim(SprdMain.Text)

                SprdMain.Col = ColItemDesc
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value))

                SprdMain.Col = ColUom
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                mItemUOM = Trim(SprdMain.Text)

                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), "", .Fields("ITEM_QTY").Value)))

                SprdMain.Col = ColScrapQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SCRAP_QTY").Value), "", .Fields("SCRAP_QTY").Value)))

                SprdMain.Col = ColStockType
                SprdMain.Text = IIf(IsDBNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)

                SprdMain.Col = colStdQty
                SprdMain.Text = CStr(GetSTDQty(mProdItemCode))

                SprdMain.Col = ColReason
                SprdMain.Text = IIf(IsDBNull(.Fields("REASON").Value), "", .Fields("REASON").Value)

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
    'Private Sub CalcTots()	
    'On Error GoTo ERR1	
    'Dim RsMisc As ADODB.Recordset = Nothing	
    'Dim mQty As Double	
    'Dim mRate As Double	
    'Dim mAmount As Double	
    'Dim mNetAmount	
    '	
    'Dim i As Long	
    'Dim j As Long	
    '	
    '    mNetAmount = 0	
    '	
    '    With SprdMain	
    '        j = .MaxRows	
    '        For i = 1 To j	
    '            .Row = i	
    '	
    '            .Col = ColItemCode	
    '            If Trim(.Text) <> "" Then	
    '                .Col = ColRate	
    '                mRate = Val(.Text)	
    '	
    '                .Col = ColReWorkQty	
    '                mQty = Val(.Text)	
    '	
    '                mAmount = Format(mRate * mQty, "0.00")	
    '	
    '                .Col = ColAmount	
    '                .Text = mAmount	
    '	
    '                mNetAmount = mNetAmount + mAmount	
    '            End If	
    '         Next i	
    '    End With	
    '	
    '    lblMaterialCost.text = Format(mNetAmount, "#0.00")	
    '	
    '    Exit Sub	
    'ERR1:	
    '    ErrorMsg err.Description, err.Number, vbCritical	
    '    ''Resume	
    'End Sub	
    '	
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

        chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkApproved.Enabled = IIf(lblApproval.Text = "N", False, True)

        '    If CDate(txtRefTM.Text) < CDate("09:00") Then	
        '        txtPMemoDate.Text = Format(RunDate - 1, "DD/MM/YYYY")	
        '    Else	
        txtPMemoDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")

        '    End If	

        txtDept.Text = ""
        txtEmp.Text = ""
        lblEmp.Text = ""
        txtRemarks.Text = ""

        txtProductCode.Text = ""
        txtDismantleQty.Text = CStr(0)
        txtDirectScrapWR.Text = CStr(0)
        txtAvailableQty.Text = CStr(0)

        lblProductionUOM.Text = ""

        txtProductCode.Enabled = False
        txtDismantleQty.Enabled = True
        txtDirectScrapWR.Enabled = True
        cmdSearchProductCode.Enabled = True

        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        txtSBSlipNo.Text = ""
        txtSBSlipDate.Text = ""
        lblMaterialCost.Text = "0.00"

        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtPMemoDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)

    End Sub
    Private Sub FrmReworkBreakup_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmReworkBreakup_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Public Sub FrmReworkBreakup_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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


    Private Sub txtBatchNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBatchNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBatchNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBatchNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBatchNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBatchNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBatchNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchProductCode_Click(cmdSearchProductCode, New System.EventArgs())
    End Sub

    Private Sub cmdSearchBatchNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchBatchNo.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = GetItemBatchWiseQry((txtProductCode.Text), (txtPMemoDate.Text), Trim(lblProductionUOM.Text), Trim(txtDept.Text), "WR", Trim(txtBatchNo.Text), ConPH, ConStockRefType_REWORK, Val(txtPMemoNo.Text))
        If MainClass.SearchGridMasterBySQL2(txtBatchNo.Text, SqlStr) = True Then
            txtBatchNo.Text = Trim(AcName1)
        End If

        '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""	
        '    If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then	
        '        txtProductCode.Text = AcName1	
        '        lblProductCode.text = AcName	
        '        If txtProductCode.Enabled = True Then txtProductCode.SetFocus	
        '    End If	
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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

        Dim xSqlStr As String
        Dim RsTemp1 As ADODB.Recordset
        Dim mProdSeq As Integer
        Dim mPrevDept As String


        mProdSeq = GetProductSeqNo(pItemCode, Trim(txtDept.Text), (txtPMemoDate.Text))
        If mProdSeq = 1 Then
            mPrevDept = ""
        Else
            mPrevDept = GetProductDept(pItemCode, mProdSeq - 1, (txtPMemoDate.Text))
        End If

        SqlStr = ""

        SqlStr = "SELECT ITEM_CODE, TO_CHAR(CASE WHEN BATCH_NO<='0' OR BATCH_NO=NULL OR BATCH_NO='' THEN '-1' ELSE BATCH_NO END) BATCH_NO, SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY"

        mTableName = ConInventoryTable

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " "

        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & "AND STOCK_ID='" & pStock_ID & "'"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(pItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='" & mPrevDept & "'" ''pDeptCode	

        If pRefType <> "" And Val(CStr(pRefNo)) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE || REF_NO <> '" & pRefType & pRefNo & "'"
        End If

        If pStockType = "QC" Then
            SqlStr = SqlStr & vbCrLf & " AND (STOCK_TYPE='" & pStockType & "' OR E_DATE>TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY'))"
        Else
            If pStockType = "" Then
                SqlStr = SqlStr & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='" & pStockType & "'"

                SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='" & pStockType & "' AND E_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY')"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1))<>0"

        SqlStr = SqlStr & vbCrLf & " GROUP BY ITEM_CODE,TO_CHAR(CASE WHEN BATCH_NO<='0' OR BATCH_NO=NULL OR BATCH_NO='' THEN '-1' ELSE BATCH_NO END)"



        xSqlStr = " SELECT RM_CODE " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST, INV_GENERAL_MST GMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND INVMST.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf & " AND INVMST.CATEGORY_CODE=GMST.GEN_CODE" & vbCrLf & " AND GMST.GEN_TYPE='C' AND GMST.PRD_TYPE IN ('P','I') AND DSP_RPT_FLAG='Y' AND STATUS='O'" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'"

        MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)

        xItemCode = ""
        If RsTemp1.EOF = False Then
            '        Do While RsTemp1.EOF = False	
            xItemCode = IIf(IsDBNull(RsTemp1.Fields("RM_CODE").Value), "", Trim(RsTemp1.Fields("RM_CODE").Value))

            mProdSeq = 1 ''GetProductSeqNo(xItemCode, Trim(txtDept.Text), txtPMemoDate.Text)	
            mPrevDept = GetProductDept(xItemCode, mProdSeq, (txtPMemoDate.Text))
            '            RsTemp1.MoveNext	
            '        Loop	


            SqlStr = SqlStr & " UNION ALL"

            SqlStr = SqlStr & " SELECT ITEM_CODE, TO_CHAR(CASE WHEN BATCH_NO<='0' OR BATCH_NO=NULL OR BATCH_NO='' THEN '-1' ELSE BATCH_NO END) BATCH_NO, SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY"

            mTableName = ConInventoryTable

            SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " "

            SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

            SqlStr = SqlStr & vbCrLf & "AND STOCK_ID='" & pStock_ID & "'"

            SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

            SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(xItemCode) & "'"

            SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='" & mPrevDept & "'"


            '    SqlStr = SqlStr & vbCrLf & " AND (BATCH_NO>=0 OR BATCH_NO=" & Val(pLotNo) & ")"	

            If pRefType <> "" And Val(CStr(pRefNo)) <> 0 Then
                SqlStr = SqlStr & vbCrLf & " AND REF_TYPE || REF_NO <> '" & pRefType & pRefNo & "'"
            End If

            If pStockType = "QC" Then
                SqlStr = SqlStr & vbCrLf & " AND (STOCK_TYPE='" & pStockType & "' OR E_DATE>TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY'))"
            Else
                If pStockType = "" Then
                    SqlStr = SqlStr & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY')"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='" & pStockType & "'"

                    '            If PubUserID <> "G0416" Then	
                    SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='" & pStockType & "' AND E_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY')"
                    '            End If	
                End If
            End If

            '    If PubUserID <> "G0416" Then	
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            '    End If	

            SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1))<>0"

            SqlStr = SqlStr & vbCrLf & " GROUP BY ITEM_CODE,TO_CHAR(CASE WHEN BATCH_NO<='0' OR BATCH_NO=NULL OR BATCH_NO='' THEN '-1' ELSE BATCH_NO END)"
        End If

        GetItemBatchWiseQry = SqlStr

        Exit Function
ErrPart:
        GetItemBatchWiseQry = ""
    End Function

    Private Sub txtBatchNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBatchNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim mAvailable As Double
        Dim mItemUOM As String

        Dim mDivisionCode As Double
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String


        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblProductCode.Text = MasterNo
        Else
            MsgInformation("Invalid Product Code")
            Cancel = True
        End If

        If cboDivision.Text = "" Then
            '        If cboDivision.Enabled = True Then cboDivision.SetFocus	
            '        MsgInformation "Please Select Division."	
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblProductionUOM.Text = MasterNo
            mItemUOM = MasterNo
        End If

        If Val(txtBatchNo.Text) <> 0 Then
            If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                mBatchNo = Trim(txtBatchNo.Text)
                xFGBatchNoReq = "Y"
            Else
                mBatchNo = ""
                xFGBatchNoReq = "N"
            End If
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
    Private Sub txtDirectScrapWR_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDirectScrapWR.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDirectScrapWR_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDirectScrapWR.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDismantleQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDismantleQty.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDismantleQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDismantleQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtSBSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSBSlipNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtSBSlipNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSBSlipNo.DoubleClick
        cmdSBSlipSearch_Click(cmdSBSlipSearch, New System.EventArgs())
    End Sub
    Private Sub TxtSBSlipNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSBSlipNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtSBSlipNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSBSlipNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSBSlipSearch_Click(cmdSBSlipSearch, New System.EventArgs())
    End Sub

    Public Sub TxtSBSlipNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSBSlipNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mSBSlipNo As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDivisionCode As Integer

        If Trim(txtSBSlipNo.Text) = "" Then GoTo EventExitSub
        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub
        If Trim(cboDivision.Text) = "" Then GoTo EventExitSub
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub

        txtAvailableQty.Text = "0.00"
        lblProductionUOM.Text = ""


        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If



        If Len(txtSBSlipNo.Text) < 6 Then
            txtSBSlipNo.Text = Val(txtSBSlipNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mSBSlipNo = Trim(txtSBSlipNo.Text)

        SqlStr = "SELECT AUTO_KEY_SBRWK, ITEM_CODE, ITEM_UOM, SB_DATE, " & vbCrLf _
            & " SUM(DECODE(ITEM_IO,'I',1,-1) * DECODE(STOCK_TYPE,'WR',1,0) * ITEM_QTY) As ITEM_QTY" & vbCrLf _
            & " FROM PRD_REWORK_TRN" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND AUTO_KEY_SBRWK=" & mSBSlipNo & "" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
            & " AND DEPT_CODE='" & txtDept.Text & "'" & vbCrLf _
            & " AND DIV_CODE=" & mDivisionCode & " AND STOCK_TYPE IN ('WR')"

        If Val(txtPMemoNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE||AUTO_KEY_REF<>'" & "WBU" & Val(txtPMemoNo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY AUTO_KEY_SBRWK, SB_DATE, ITEM_CODE,ITEM_UOM "
        SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY)>0"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtSBSlipDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SB_DATE").Value), "", RsTemp.Fields("SB_DATE").Value), "DD/MM/YYYY")
            txtAvailableQty.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)

            If MainClass.ValidateWithMasterTable(Trim(txtProductCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblProductCode.Text = Trim(MasterNo)
            End If

            lblProductionUOM.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)

        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSBSlipSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSBSlipSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mDivisionCode As Double


        If Trim(cboDivision.Text) = "" Then MsgInformation("Please select the Division Code") : Exit Sub
        If Trim(txtDept.Text) = "" Then MsgInformation("Please select the Dept Code") : Exit Sub

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        SqlStr = "SELECT AUTO_KEY_SBRWK, ITEM_CODE, SB_DATE, SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY) As ITEM_QTY" & vbCrLf _
            & " FROM PRD_REWORK_TRN" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND DEPT_CODE='" & txtDept.Text & "'" & vbCrLf _
            & " AND DIV_CODE=" & mDivisionCode & " AND STOCK_TYPE='WR'" & vbCrLf _
            & " GROUP BY AUTO_KEY_SBRWK, SB_DATE, ITEM_CODE " & vbCrLf _
            & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY)>0"

        If MainClass.SearchGridMasterBySQL2(txtSBSlipNo.Text, SqlStr) = True Then
            txtSBSlipNo.Text = AcName
            txtProductCode.Text = AcName1
            'TxtSBSlipNo_Validate(False)
            TxtSBSlipNo_Validating(txtSBSlipNo, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtSBSlipDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSBSlipDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSBSlipDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSBSlipDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Not IsDate(txtSBSlipDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
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

        SqlStr = "Select * From PRD_WRBREAKUP_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_REF=" & Val(txtPMemoNo.Text) & "" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "

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
                SqlStr = "Select * From PRD_WRBREAKUP_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_REF=" & Val(CStr(mPMemoNo)) & "" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProductCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchProductCode_Click(cmdSearchProductCode, New System.EventArgs())
    End Sub

    Private Sub txtProductCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProductCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim mAvailable As Double
        Dim mItemUOM As String

        Dim mDivisionCode As Double
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String


        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblProductCode.Text = MasterNo
        Else
            MsgInformation("Invalid Product Code")
            Cancel = True
        End If

        If cboDivision.Text = "" Then
            '        If cboDivision.Enabled = True Then cboDivision.SetFocus	
            '        MsgInformation "Please Select Division."	
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblProductionUOM.Text = MasterNo
            mItemUOM = MasterNo
        End If

        If Val(txtBatchNo.Text) <> 0 Then
            If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                mBatchNo = Trim(txtBatchNo.Text)
                xFGBatchNoReq = "Y"
            Else
                mBatchNo = ""
                xFGBatchNoReq = "N"
            End If
        End If


        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim mDeptSeq As Integer
        Dim mProdDept As String
        Dim RsBOM As ADODB.Recordset = Nothing
        Dim mItemDesc As String
        Dim mItemCode As String
        Dim mItemUOM As String = ""
        Dim mFactor As Double
        Dim mStdQty As Double
        Dim i As Integer
        Dim mSrn As String
        Dim mLevel As Integer
        Dim mFlag As String

        'Dim mProductCode As String = ""

        Dim mProductCode As String = ""
        Dim mQty As Double

        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColItemCode
        mProductCode = Trim(SprdMain.Text)

        SprdMain.Col = colStdQty
        mQty = Val(SprdMain.Text)


        mDeptSeq = GetMaxProductSeqNo(Trim(mProductCode), txtPMemoDate.Text)
        mProdDept = GetProductDept(Trim(mProductCode), mDeptSeq, txtPMemoDate.Text)

        mSqlStr = MakeBOMStockQty(Trim(mProductCode), mProdDept, mDeptSeq)

        '    MainClass.ClearGrid SprdMain
        '    Call FormatSprdMain(-1)

        If mSqlStr = "" Then
            Exit Sub
        Else
            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOM, ADODB.LockTypeEnum.adLockReadOnly)
        End If

        'If RsBOM.EOF = True Then
        '    mSqlStr = MakeOutBOMStockQty(Trim(mProductCode), mProdDept, mDeptSeq)
        '    MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOM, ADODB.LockTypeEnum.adLockReadOnly)
        'End If

        mLevel = 1

        i = eventArgs.row
        If RsBOM.EOF = False Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColQty
            SprdMain.Text = "0.00"

            SprdMain.Col = ColScrapQty
            SprdMain.Text = "0.00"

            SprdMain.Col = ColFlag
            SprdMain.Text = "1"

            MainClass.ProtectCell(SprdMain, eventArgs.row, eventArgs.row, ColItemCode, ColRePopulate)

            With SprdMain
                Do While RsBOM.EOF = False
                    i = i + 1
                    .Row = i
                    .Action = SS_ACTION_INSERT_ROW
                    .MaxRows = .MaxRows + 1


                    .Row = i
                    .Col = ColItemCode
                    .Text = Trim(IIf(IsDBNull(RsBOM.Fields("RM_CODE").Value), "", RsBOM.Fields("RM_CODE").Value))
                    mItemCode = Trim(IIf(IsDBNull(RsBOM.Fields("RM_CODE").Value), "", RsBOM.Fields("RM_CODE").Value))

                    .Col = ColItemDesc
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Text = Trim(MasterNo)
                    Else
                        .Text = ""
                    End If

                    .Col = ColUom
                    .Text = Trim(IIf(IsDBNull(RsBOM.Fields("ISSUE_UOM").Value), "", RsBOM.Fields("ISSUE_UOM").Value))
                    mItemUOM = Trim(IIf(IsDBNull(RsBOM.Fields("ISSUE_UOM").Value), "", RsBOM.Fields("ISSUE_UOM").Value))

                    If UCase(Trim(mItemUOM)) = "TON" Then
                        mFactor = (1000 * 1000)
                    ElseIf UCase(Trim(mItemUOM)) = "KGS" Then
                        mFactor = (1000)
                    Else
                        mFactor = 1
                    End If

                    .Col = ColStockType
                    .Text = Trim(IIf(IsDBNull(RsBOM.Fields("STOCK_TYPE").Value), "", RsBOM.Fields("STOCK_TYPE").Value))

                    .Col = colStdQty
                    mStdQty = Val(CStr(IIf(IsDBNull(RsBOM.Fields("STD_QTY").Value), 0, RsBOM.Fields("STD_QTY").Value) + IIf(IsDBNull(RsBOM.Fields("GROSS_WT_SCRAP").Value), 0, RsBOM.Fields("GROSS_WT_SCRAP").Value))) / mFactor
                    .Text = CStr(mStdQty * (Val(CStr(mQty))))

                    .Col = ColQty
                    .Text = CStr(0)

                    .Col = ColScrapQty
                    .Text = CStr(0)

                    RsBOM.MoveNext()
                Loop
            End With
        End If

        Call FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
End Class
