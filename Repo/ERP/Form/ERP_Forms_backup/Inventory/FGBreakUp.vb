Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmFGBreakup
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
    Private Const ColUOM As Short = 3
    Private Const ColStockType As Short = 4
    Private Const ColStdQty As Short = 5
    Private Const ColQty As Short = 6
    Private Const ColScrapQty As Short = 7
    Private Const ColReason As Short = 8

    Dim mcntRow As Integer
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtPMemoNo.Enabled = False
            cmdsearch.Enabled = False
            cmdPopulate.Enabled = True
        Else
            CmdAdd.Text = ConCmdAddCaption
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
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
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
                If InsertIntoDelAudit(PubDBCn, "PRD_FGBREAKUP_HDR ", (txtPMemoNo.Text), RsPMemoMain, "AUTO_KEY_REF") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_FGBREAKUP_HDR", "AUTO_KEY_REF", (lblMkey.Text)) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_FGBREAKUP, (txtPMemoNo.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM PRD_FGBREAKUP_DET WHERE AUTO_KEY_REF=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM PRD_FGBREAKUP_HDR  WHERE AUTO_KEY_REF=" & Val(lblMkey.Text) & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'")
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
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtPMemoNo.Enabled = False
            cmdsearch.Enabled = False
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
        Dim RsBOM As ADODB.Recordset = Nothing
        Dim mItemDesc As String
        Dim mItemCode As String
        Dim mItemUOM As String = ""
        Dim mFactor As Double
        Dim mStdQty As Double
        Dim I As Integer
        Dim mSrn As String
        Dim mLevel As Integer
        Dim mProductCode As String = ""


        If Trim(txtProductCode.Text) = "" Then
            MsgBox("Please Enter Product Code.")
            Exit Sub
        End If

        If Val(txtDismantleQty.Text) = 0 Then
            MsgBox("Please Enter Dismantle Qty.")
            Exit Sub
        End If

        If Val(txtDismantleQty.Text) > Val(txtAvailableQty.Text) Then
            MsgBox("Dismantle Qty cann't be greater than Available Qty.")
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

        I = 0
        If RsBOM.EOF = False Then
            With SprdMain
                Do While RsBOM.EOF = False
                    I = I + 1

                    mSrn = Str(I)

                    '                mStdQty = (Val(IIf(IsNull(RsBOM!STD_QTY), 0, RsBOM!STD_QTY)))
                    '
                    '                Call FillGridCol(RsBOM, mSrn, mLevel, mProductCode, mProductCode, mStdQty)


                    .Row = .MaxRows
                    .Col = ColItemCode
                    .Text = Trim(IIf(IsDbNull(RsBOM.Fields("RM_CODE").Value), "", RsBOM.Fields("RM_CODE").Value))
                    mItemCode = Trim(IIf(IsDbNull(RsBOM.Fields("RM_CODE").Value), "", RsBOM.Fields("RM_CODE").Value))

                    .Col = ColItemDesc
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Text = Trim(MasterNo)
                    Else
                        .Text = ""
                    End If

                    .Col = ColUOM
                    .Text = Trim(IIf(IsDbNull(RsBOM.Fields("ISSUE_UOM").Value), "", RsBOM.Fields("ISSUE_UOM").Value))
                    mItemUOM = Trim(IIf(IsDbNull(RsBOM.Fields("ISSUE_UOM").Value), "", RsBOM.Fields("ISSUE_UOM").Value))

                    If UCase(Trim(mItemUOM)) = "TON" Then
                        mFactor = (1000 * 1000)
                    ElseIf UCase(Trim(mItemUOM)) = "KGS" Then
                        mFactor = (1000)
                    Else
                        mFactor = 1
                    End If

                    .Col = ColStockType
                    .Text = Trim(IIf(IsDbNull(RsBOM.Fields("STOCK_TYPE").Value), "", RsBOM.Fields("STOCK_TYPE").Value))

                    .Col = ColStdQty
                    mStdQty = Val(CStr(IIf(IsDbNull(RsBOM.Fields("STD_QTY").Value), 0, RsBOM.Fields("STD_QTY").Value) + IIf(IsDbNull(RsBOM.Fields("GROSS_WT_SCRAP").Value), 0, RsBOM.Fields("GROSS_WT_SCRAP").Value))) / mFactor
                    .Text = CStr(mStdQty * Val(txtDismantleQty.Text))

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

    Private Function GetStdQty(ByRef pRMCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemDesc As String
        Dim mStdQty As Double
        Dim mItemUOM As String = ""
        Dim mFactor As Double
        Dim mDeptCode As String
        Dim mDeptSeq As Integer

        GetStdQty = 0

        mDeptSeq = GetMaxProductSeqNo(Trim(txtProductCode.Text), (txtPMemoDate.Text))
        mDeptCode = GetProductDept(Trim(txtProductCode.Text), mDeptSeq, (txtPMemoDate.Text))

        SqlStr = "SELECT  SUM(ID.STD_QTY + ID.GROSS_WT_SCRAP) AS STD_QTY, INVMST.ISSUE_UOM " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST"

        SqlStr = SqlStr & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(txtProductCode.Text)) & "' " & vbCrLf & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(UCase(pRMCode)) & "' "


        SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(UCase(mDeptCode)) & "' "

        SqlStr = SqlStr & vbCrLf & " AND IH.WEF=( " & vbCrLf & " SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(txtProductCode.Text)) & "' " & vbCrLf & " AND WEF<= TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf & " GROUP BY INVMST.ISSUE_UOM"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            mStdQty = IIf(IsDbNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value)
            mItemUOM = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

            If UCase(Trim(mItemUOM)) = "TON" Then
                mFactor = (1000 * 1000)
            ElseIf UCase(Trim(mItemUOM)) = "KGS" Then
                mFactor = (1000)
            Else
                mFactor = 1
            End If
            mStdQty = mStdQty / mFactor
            GetStdQty = mStdQty * Val(txtDismantleQty.Text)
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        If PubSuperUser <> "S" Then
            If lblBookType.Text = "C" Or lblBookType.Text = "F" Then
                Exit Sub
            End If
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtPMemoNo_Validating(txtPMemoNo, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
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

        If MainClass.SearchGridMaster((txtPMemoNo.Text), "PRD_FGBREAKUP_HDR ", "AUTO_KEY_REF", "REF_DATE", , , SqlStr) = True Then
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

    Private Sub FrmFGBreakup_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        'Dim xIName As String
        'Dim SqlStr As String = ""
        'Dim pOPRCode As String
        'Dim mProductCode As String = ""
        'Dim pOPRDesc As String
        'Dim RsTemp As ADODB.Recordset = Nothing

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColItemCode Then
            With SprdMain
                eventArgs.Row = .ActiveRow

                eventArgs.Col = ColItemCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    eventArgs.Row = .ActiveRow

                    eventArgs.Col = ColItemCode
                    .Text = Trim(AcName)

                    eventArgs.Col = ColItemDesc
                    .Text = Trim(AcName1)
                    Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
                End If

            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColItemDesc Then
            With SprdMain
                eventArgs.Row = .ActiveRow

                eventArgs.Col = ColItemDesc
                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    eventArgs.Row = .ActiveRow

                    eventArgs.Col = ColItemCode
                    .Text = Trim(AcName1)

                    eventArgs.Col = ColItemDesc
                    .Text = Trim(AcName)
                    Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
                End If

            End With
        End If
        If eventArgs.Row = 0 And eventArgs.Col = ColStockType Then
            With SprdMain
                eventArgs.Row = .ActiveRow

                eventArgs.Col = ColStockType
                If MainClass.SearchGridMaster(.Text, "INV_TYPE_MST", "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    eventArgs.Row = .ActiveRow

                    eventArgs.Col = ColStockType
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColStockType, .ActiveRow, ColStockType, .ActiveRow, False))
            End With
        End If

        '    If eventArgs.Col = 0 And eventArgs.Row > 0 Then
        '        SprdMain.Row=eventArgs.Row
        '        SprdMain.Col = ColItemCode
        '        If eventArgs.Row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
        '            MainClass.DeleteSprdRow SprdMain, Row, ColItemCode
        '            MainClass.SaveStatus Me, ADDMode, MODIFYMode
        '            FormatSprdMain Row
        '        End If
        '    End If

    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim mDivisionCode As Double

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
        Dim mStockType As String = ""

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
        Dim mCategoryCode As String = ""
        Dim mStockType As String = ""
        Dim mProdItemCode As String
        Dim mItemUOM As String = ""

        FillItemDescPart = False
        If Trim(pItemCode) = "" Then Exit Function

        SqlStr = "SELECT ITEM_CODE, ITEM_SHORT_DESC,ISSUE_UOM,CATEGORY_CODE " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mCategoryCode = Trim(IIf(IsDbNull(RsTemp.Fields("CATEGORY_CODE").Value), "", RsTemp.Fields("CATEGORY_CODE").Value))

            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                .Text = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                mProdItemCode = Trim(.Text)

                .Col = ColItemDesc
                .Text = IIf(IsDbNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)

                .Col = ColUOM
                .Text = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                mItemUOM = Trim(.Text)

                '            .Col = ColStockType
                '            .Text = "FG"
                '
                '            .Col = ColStdQty
                '            .Text = GetBalanceStockQty(mProdItemCode, txtPMemoDate.Text, mItemUOM, "STR", "FG", "", ConWH, mDivisionCode, ConStockRefType_FGBREAKUP, Val(txtPMemoNo.Text))

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

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_REF)  " & vbCrLf & " FROM PRD_FGBREAKUP_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDbNull(.Fields(0).Value) Then
                    mAutoGen = CDbl(Mid(.Fields(0).Value, 1, Len(.Fields(0).Value) - 6))
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

        Dim pErrorDesc As String = ""
        'Dim RsTemp As ADODB.Recordset=Nothing

        Dim mDivisionCode As Double


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        SqlStr = ""
        mPMemoNo = Val(txtPMemoNo.Text)
        If Val(txtPMemoNo.Text) = 0 Then
            mPMemoNo = CDbl(AutoGenMemoNo())
        End If
        txtPMemoNo.Text = CStr(mPMemoNo)
        If ADDMode = True Then
            lblMkey.Text = CStr(mPMemoNo)
            SqlStr = " INSERT INTO PRD_FGBREAKUP_HDR  " & vbCrLf & " (COMPANY_CODE, FYEAR, AUTO_KEY_REF," & vbCrLf & " REF_DATE, EMP_CODE, PRODUCT_CODE, PROD_QTY, REMARKS, " & vbCrLf & " BOOKTYPE, ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE, DIV_CODE) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & "," & mPMemoNo & ", " & vbCrLf & " TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote((txtEmp.Text)) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtProductCode.Text)) & "'," & Val(txtDismantleQty.Text) & ", '" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', " & vbCrLf & " '" & VB.Left(lblBookType.Text, 1) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'',''," & mDivisionCode & ")"

        ElseIf MODIFYMode = True Then
            mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime
            SqlStr = " UPDATE PRD_FGBREAKUP_HDR  SET " & vbCrLf & " AUTO_KEY_REF=" & mPMemoNo & ", " & vbCrLf & " REF_DATE=TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " EMP_CODE='" & MainClass.AllowSingleQuote((txtEmp.Text)) & "', " & vbCrLf & " PRODUCT_CODE='" & MainClass.AllowSingleQuote((txtProductCode.Text)) & "', " & vbCrLf & " PROD_QTY=" & Val(txtDismantleQty.Text) & ", " & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', " & vbCrLf & " BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'), DIV_CODE=" & mDivisionCode & "" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_REF=" & Val(lblMKey.Text) & ""
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
            lblMkey.Text = ""
            txtPMemoNo.Text = ""
        End If
        '    Resume
    End Function
    Private Function UpdateDetail1(ByRef pErrorDesc As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String = ""
        Dim mStockType As String = ""
        Dim mProdQty As Double
        Dim xStockRowNo As Integer
        Dim mREASON As String
        Dim mDeptCode As String

        Dim xItemCost As Double
        Dim mInCCCode As String
        Dim mWIPStock As Double
        Dim mWIPReworkStock As Double
        Dim mSqlStr As String
        Dim RsBOM As ADODB.Recordset = Nothing
        Dim mProductSeqNo As Integer
        Dim mProductionDate As String
        'Dim mEntryDate As String

        Dim mToolNo As String
        Dim mTotalOpr As Integer
        Dim mDeptSeq As Integer
        Dim xOPStockType As String
        Dim pOPRCode As String
        Dim pOPRDesc As String
        Dim pSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mOperatorCode As String
        Dim mScrapQty As Double
        Dim xWareHouse As String

        If MainClass.ValidateWithMasterTable("STR", "DEPT_CODE", "CCCODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInCCCode = IIf(IsDbNull(MasterNo), "-1", MasterNo)
        Else
            mInCCCode = "-1"
        End If

        SqlStr = " DELETE FROM PRD_FGBREAKUP_DET " & vbCrLf & " WHERE AUTO_KEY_REF=" & Val(lblMkey.Text) & " "
        PubDBCn.Execute(SqlStr)

        If DeleteStockTRN(PubDBCn, ConStockRefType_FGBREAKUP, (txtPMemoNo.Text)) = False Then GoTo UpdateDetail1Err
        xStockRowNo = 1


        '    mDeptSeq = GetMaxProductSeqNo(Trim(txtProductCode.Text), txtPMemoDate.Text)
        mDeptCode = "STR" ''GetProductDept(Trim(txtProductCode.Text), mDeptSeq, txtPMemoDate.Text)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColUOM
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColQty
                mProdQty = Val(.Text)

                .Col = ColScrapQty
                mScrapQty = Val(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                .Col = ColReason
                mREASON = Trim(.Text)

                If mItemCode <> "" And (mProdQty + mScrapQty) > 0 Then
                    SqlStr = " INSERT INTO PRD_FGBREAKUP_DET ( " & vbCrLf & " COMPANY_CODE,AUTO_KEY_REF,SERIAL_NO,ITEM_CODE,ITEM_DESC, " & vbCrLf & " ITEM_UOM,STOCK_TYPE, ITEM_QTY, SCRAP_QTY, REASON) " & vbCrLf & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMkey.Text) & ", " & I & "," & vbCrLf & " '" & mItemCode & "','" & mItemDesc & "', " & vbCrLf & " '" & mUOM & "','" & mStockType & "', " & vbCrLf & " " & mProdQty & ", " & mScrapQty & ", '" & MainClass.AllowSingleQuote(mREASON) & "')"

                    PubDBCn.Execute(SqlStr)

                    If I = 1 Then
                        If UpdateStockTRN(PubDBCn, ConStockRefType_FGBREAKUP, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), IIf(lblBookType.Text = "F", "FG", "CR"), Trim(txtProductCode.Text), Trim(lblProductionUOM.Text), CStr(-1), Val(txtDismantleQty.Text), 0, "O", xItemCost, xItemCost, "-1", "", mDeptCode, IIf(lblBookType.Text = "F", "PAD", "STR"), mInCCCode, "N", "From : " & IIf(lblBookType.Text = "F", "FG", "CR") & " (Dismantle) -" & ConStockRefType_FGBREAKUP, "-1", ConWH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err

                    End If

                    '                If mDeptSeq > 1 Then
                    '                    xStockRowNo = xStockRowNo + 1
                    '                    If UpdateStockTRN(PubDBCn, ConStockRefType_FGBREAKUP, txtPMemoNo.Text, xStockRowNo, txtPMemoDate.Text, txtPMemoDate.Text, _
                    ''                            "ST", Trim(txtProductCode.Text), Trim(lblProductionUOM.text), -1, Val(txtDismantleQty.Text), 0, "I", xItemCost, xItemCost, "-1", "", mDeptCode, "PAD", mInCCCode, "N", "From : (" & IIf(lblBookType.text = "F", "FG", "CR") & " Dismantle) -" & ConStockRefType_FGBREAKUP, "-1", ConPH, mDivisionCode) = False Then GoTo UpdateDetail1Err
                    '                End If

                    '                If UpdateStockTRN(PubDBCn, ConStockRefType_FGBREAKUP, txtPMemoNo.Text, xStockRowNo, txtPMemoDate.Text, txtPMemoDate.Text, _
                    ''                            "ST", Trim(txtProductCode.Text), Trim(lblProductionUOM.text), -1, Val(txtDismantleQty.Text), 0, "I", xItemCost, xItemCost, "-1", "", mDeptCode, "PAD", mInCCCode, "N", "From : (" & IIf(lblBookType.text = "F", "FG","CR") & " Dismantle) -" & ConStockRefType_FGBREAKUP, "-1", ConPH, mDivisionCode) = False Then GoTo UpdateDetail1Err

                    '                If IsFGItem(mItemCode) = True Then
                    '                    xWareHouse = ConPH
                    '                Else
                    '                    If RsCompany!AUTO_ISSUE = "N" Then
                    '                        xWareHouse = "PH"
                    '                    Else
                    '                        If IsProductionItem(mItemCode) = True Then
                    '                            If CDate(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")) < CDate(VB6.Format(RsCompany!AUTO_ISSUE_DATE, "DD/MM/YYYY")) Then
                    '                                xWareHouse = "PH"
                    '                            Else
                    '                                xWareHouse = "WH"
                    '                            End If
                    '                        Else
                    '                            xWareHouse = "PH"
                    '                        End If
                    '                    End If
                    '                End If

                    xWareHouse = "WH"
                    If mProdQty > 0 Then
                        xStockRowNo = xStockRowNo + 1
                        If UpdateStockTRN(PubDBCn, ConStockRefType_FGBREAKUP, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), mStockType, mItemCode, mUOM, CStr(-1), mProdQty, 0, "I", xItemCost, xItemCost, "", "", mDeptCode, "PAD", mInCCCode, "N", "TO : " & mDeptCode & " (" & IIf(lblBookType.Text = "F", "FG", "CR") & " Dismantle) -" & ConStockRefType_FGBREAKUP & "-" & Trim(txtProductCode.Text), "-1", xWareHouse, mDivisionCode, "", Trim(txtProductCode.Text)) = False Then GoTo UpdateDetail1Err

                    End If

                    If mScrapQty > 0 Then
                        xStockRowNo = xStockRowNo + 1
                        If UpdateStockTRN(PubDBCn, ConStockRefType_FGBREAKUP, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "SC", mItemCode, mUOM, CStr(-1), mScrapQty, 0, "I", xItemCost, xItemCost, "", "", mDeptCode, "PAD", mInCCCode, "N", "TO : " & mDeptCode & " (" & IIf(lblBookType.Text = "F", "FG", "CR") & " Dismantle) -" & ConStockRefType_FGBREAKUP & "-" & Trim(txtProductCode.Text), "-1", xWareHouse, mDivisionCode, "", Trim(txtProductCode.Text)) = False Then GoTo UpdateDetail1Err

                    End If

                End If
NextRec:
            Next
        End With
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

        SqlStr = SqlStr & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(mSFICode)) & "' "

        '    If pDeptSeq = 1 Then
        '        SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(UCase(mDeptCode)) & "' "
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE IN ( " & vbCrLf _
        ''                & " SELECT DEPT_CODE FROM PRD_PRODSEQUENCE_DET" & vbCrLf _
        ''                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mSFICode) & "'" & vbCrLf _
        ''                & " AND SERIAL_NO<=" & Val(pDeptSeq) & ")"
        '    End If

        SqlStr = SqlStr & vbCrLf & " AND IH.WEF=( " & vbCrLf & " SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(mSFICode)) & "' " & vbCrLf & " AND WEF<= TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


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
        Dim mItemUOM As String = ""
        Dim mStockQty As Double
        Dim mDeptCode As String
        Dim mRate As Double
        Dim mFactorQty As Double
        Dim mChildRMCode As String
        'Dim mStqQty As Double
        Dim mTotValue As Double
        Dim mUOM As String = ""
        Dim mTotClosing As Double

        With SprdMain

            mRMCode = Trim(IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
            If CheckSubRecord(mRMCode) = True Then
                pLevel = pLevel + 1
                Call FillSubRecord(mRMCode, "", pSRNo, pLevel, pProductCode, mStqQty)

            Else
                .Row = .MaxRows

                mDeptCode = IIf(IsDbNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)
                mItemUOM = IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)

                .Col = ColItemCode
                .Text = Trim(IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
                mRMCode = Trim(IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColItemDesc
                .Text = IIf(IsDBNull(pRs.Fields("Item_Short_Desc").Value), "", pRs.Fields("Item_Short_Desc").Value)
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColUOM
                .Text = Trim(mItemUOM)

                .Col = ColStockType
                .Text = "ST" ''Trim(IIf(IsNull(pRs!STOCK_TYPE), "", pRs!STOCK_TYPE))


                .Col = ColStdQty
                '            If optCalcOn(0).Value = True Then
                mStqQty = mStqQty * (Val(IIf(IsDBNull(pRs.Fields("STD_QTY").Value), 0, pRs.Fields("STD_QTY").Value))) * Val(txtDismantleQty.Text)
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

        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, ID.RM_CODE " & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O' AND IH.IS_BOP='N'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O' ) " '& vbCrLf |            & " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf |
        '    SqlStr = SqlStr & vbCrLf & " AND ID.RM_CODE NOT "

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            '        Do While Not RsShow.EOF
            mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
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
                mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
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

        SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O') " '& vbCrLf |            & " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf |
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

                mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                '            If optCalcOn(0).Value = True Then
                mStqQty = (Val(IIf(IsDbNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)))
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
                    mStqQty = (Val(IIf(IsDbNull(RsShow.Fields("STD_QTY").Value), 0, RsShow.Fields("STD_QTY").Value)))
                    '                Else
                    '                    mStqQty = ((Val(IIf(IsNull(RsShow!STD_QTY), 0, RsShow!STD_QTY)))) '' + Val(IIf(IsNull(RsShow!GROSS_WT_SCRAP), 0, RsShow!GROSS_WT_SCRAP))))
                    '                End If
                    mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
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
        Dim mProductCode As String = ""
        Dim mOPRCode As String
        Dim mOPRDesc As String
        Dim mTotalProduction As Double
        Dim mProdQty As Double
        Dim mStockQty As Double
        Dim mScrapQty As Double

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

                .Col = ColStdQty
                mStockQty = Val(.Text)

                .Col = ColScrapQty
                mScrapQty = Val(.Text)

                If mStockQty <> mProdQty + mScrapQty Then
                    MsgInformation("Standard Qty should be equal to OK Qty Plus Scrap Qty. Item Code : " & mProductCode & " Qty not match. Cann't Be Saved")
                    FieldsVarification = False
                    MainClass.SetFocusToCell(SprdMain, cntRow, ColQty)
                    Exit Function
                End If
            Next
        End With

        If CheckRowCount() = False Then
            FieldsVarification = False
            Exit Function
        End If

        If ValidateDeptRight(PubUserID, "PAD", "PAD") = False Then
            FieldsVarification = False
            Exit Function
        End If

        mCheckLastEntryDate = GetLastEntryDate

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

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Produce Quantity.") = False Then FieldsVarification = False: Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function

    Private Function GetLastEntryDate() As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetLastEntryDate = ""
        SqlStr = ""

        SqlStr = "SELECT Max(REF_DATE) AS  REF_DATE " & vbCrLf & " FROM PRD_FGBREAKUP_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetLastEntryDate = IIf(IsDbNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value)
        End If

        Exit Function
ErrPart:

    End Function
    Private Function CheckRowCount() As Boolean
        On Error GoTo ERR1
        Dim I As Integer
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
            For I = 1 To .MaxRows
                .Row = I
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

        If System.Math.Abs(mTotQty) + System.Math.Abs(mTotScrapQty) = 0 Then
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
    Public Sub FrmFGBreakup_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblBookType.Text = "F" Then
            Me.Text = "Finished Goods Dismantle / Scrap"
        Else
            Me.Text = "Customer Rejection Dismantle / Scrap"
        End If

        SqlStr = ""
        SqlStr = "Select * from PRD_FGBREAKUP_HDR  Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_FGBREAKUP_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoDetail, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)
        Call SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
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
        SqlStr = " SELECT  AUTO_KEY_REF REF_NO, TO_CHAR(REF_DATE,'DD/MM/YYYY') REF_DATE, " & vbCrLf & " PRODUCT_CODE PROD_QTY,DIV_CODE,REMARKS " & vbCrLf & " FROM PRD_FGBREAKUP_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "


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

            .Col = ColUOM
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

            .Col = ColStdQty
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

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColUOM)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockType, ColStdQty)
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsPMemoDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsPMemoMain
            txtPMemoNo.Maxlength = .Fields("AUTO_KEY_REF").Precision
            txtPMemoDate.Maxlength = 10

            txtEmp.Maxlength = .Fields("EMP_CODE").DefinedSize
            txtRemarks.Maxlength = .Fields("REMARKS").DefinedSize
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
        Dim mItemUOM As String = ""
        Dim mAvailable As Double

        With RsPMemoMain
            If Not .EOF Then
                lblMkey.Text = .Fields("AUTO_KEY_REF").Value
                txtPMemoNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_REF").Value), "", .Fields("AUTO_KEY_REF").Value)
                txtPMemoDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")

                txtEmp.Text = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtEmp_Validating(txtEmp, New System.ComponentModel.CancelEventArgs(False))
                txtRemarks.Text = IIf(IsDbNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)

                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If

                txtProductCode.Text = Trim(IIf(IsDbNull(.Fields("PRODUCT_CODE").Value), "", .Fields("PRODUCT_CODE").Value))
                txtDismantleQty.Text = VB6.Format(IIf(IsDbNull(.Fields("PROD_QTY").Value), "0.00", .Fields("PROD_QTY").Value), "0.00")

                If MainClass.ValidateWithMasterTable((txtProductCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblProductCode.Text = MasterNo
                End If

                If MainClass.ValidateWithMasterTable((txtProductCode.Text), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblProductionUOM.Text = MasterNo
                    mItemUOM = MasterNo
                End If


                mAvailable = GetBalanceStockQty(Trim(txtProductCode.Text), (txtPMemoDate.Text), mItemUOM, "PAD", "FG", "", ConWH, mDivisionCode, ConStockRefType_FGBREAKUP, Val(txtPMemoNo.Text))

                txtAvailableQty.Text = VB6.Format(mAvailable, "0.00")

                cboDivision.Enabled = False

                Call ShowDetail1(mDivisionCode)
                Call MakeEnableDesableField(False)
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        txtPMemoNo.Enabled = True
        cmdsearch.Enabled = True

        txtProductCode.Enabled = False
        txtDismantleQty.Enabled = False
        cmdSearchProductCode.Enabled = False
        cmdPopulate.Enabled = False

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub ShowDetail1(ByRef mDivisionCode As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mProdItemCode As String
        Dim mItemUOM As String = ""


        SqlStr = " SELECT * " & vbCrLf & " FROM PRD_FGBREAKUP_DET  " & vbCrLf & " WHERE AUTO_KEY_REF = " & Val(lblMkey.Text) & " " & vbCrLf & " ORDER BY  SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPMemoDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                mProdItemCode = Trim(SprdMain.Text)

                SprdMain.Col = ColItemDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value))

                SprdMain.Col = ColUOM
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                mItemUOM = Trim(SprdMain.Text)

                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_QTY").Value), "", .Fields("ITEM_QTY").Value)))

                SprdMain.Col = ColScrapQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("SCRAP_QTY").Value), "", .Fields("SCRAP_QTY").Value)))

                SprdMain.Col = ColStockType
                SprdMain.Text = IIf(IsDbNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)

                SprdMain.Col = ColStdQty
                SprdMain.Text = CStr(GetStdQty(mProdItemCode))

                SprdMain.Col = ColReason
                SprdMain.Text = IIf(IsDbNull(.Fields("REASON").Value), "", .Fields("REASON").Value)

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
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
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        lblMkey.Text = ""
        txtPMemoNo.Text = ""


        '    If CDate(txtRefTM.Text) < CDate("09:00") Then
        '        txtPMemoDate.Text = VB6.Format(RunDate - 1, "DD/MM/YYYY")
        '    Else
        txtPMemoDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")

        '    End If


        txtEmp.Text = ""
        lblEmp.Text = ""
        txtRemarks.Text = ""

        txtProductCode.Text = ""
        txtDismantleQty.Text = CStr(0)
        txtAvailableQty.Text = CStr(0)

        txtProductCode.Enabled = True
        txtDismantleQty.Enabled = True
        cmdSearchProductCode.Enabled = True

        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtPMemoDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)

    End Sub
    Private Sub FrmFGBreakup_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmFGBreakup_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Public Sub FrmFGBreakup_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Call SetMainFormCordinate(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(10935)
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
        'Dim mItemCode As String
        'Dim mItemDesc As String
        Dim mUOM As String = ""

        mCol = SprdMain.ActiveCol
        mRow = SprdMain.ActiveRow

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))


    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        '    With SprdMain
        '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False
        '    End With

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
        If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
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

        If MODIFYMode = True And RsPMemoMain.EOF = False Then mPMemoNo = RsPMemoMain.Fields("AUTO_KEY_REF").Value

        SqlStr = "Select * From PRD_FGBREAKUP_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_REF=" & Val(txtPMemoNo.Text) & "" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "

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
                SqlStr = "Select * From PRD_FGBREAKUP_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_REF=" & Val(CStr(mPMemoNo)) & "" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRefTM_Change()

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
        Dim mItemUOM As String = ""

        Dim mDivisionCode As Double


        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtProductCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        If MainClass.ValidateWithMasterTable((txtProductCode.Text), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblProductionUOM.Text = MasterNo
            mItemUOM = MasterNo
        End If

        mAvailable = GetBalanceStockQty(Trim(txtProductCode.Text), (txtPMemoDate.Text), mItemUOM, "PAD", IIf(lblBookType.Text = "F", "FG", "CR"), "", ConWH, mDivisionCode, ConStockRefType_FGBREAKUP, Val(txtPMemoNo.Text))

        txtAvailableQty.Text = VB6.Format(mAvailable, "0.00")
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
End Class