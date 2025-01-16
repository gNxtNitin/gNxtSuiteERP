Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmStoreReqSub
    Inherits System.Windows.Forms.Form
    Dim RsReqMain As ADODB.Recordset ''Recordset
    Dim RsReqDetail As ADODB.Recordset ''Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColUOM As Short = 3
    Private Const ColStockType As Short = 4
    Private Const ColStockQty As Short = 5
    Private Const ColDemandQty As Short = 6
    Private Const ColIssueQty As Short = 7
    Private Const ColIssuedQty As Short = 8
    Private Const ColBalQty As Short = 9
    Private Const ColPurpose As Short = 14
    Private Const ColRemarks As Short = 10

    Private Sub chkissue_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkIssue.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtReqNo.Enabled = False
            cmdsearch.Enabled = False

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
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click

        On Error GoTo DelErrPart

        Dim mItemCode As String
        Dim mLockBookCode As Integer

        If ValidateBranchLocking((txtReqDate.Text)) = True Then
            Exit Sub
        End If

        If lblBookType.Text = "R" Then
            mLockBookCode = CInt(ConLockStoreReq)
        Else
            mLockBookCode = CInt(ConLockIssueNote)
        End If

        If ValidateBookLocking(PubDBCn, mLockBookCode, txtReqDate.Text) = True Then
            Exit Sub
        End If

        If Trim(txtReqNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If chkIssue.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Issue Completed, Cann't be Deleted")
            Exit Sub
        End If

        If Not RsReqMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_SUB_ISSUE_HDR", (txtReqNo.Text), RsReqMain, "AUTO_KEY_ISS") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_SUB_ISSUE_HDR", "AUTO_KEY_ISS", (txtReqNo.Text)) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_SUBISS, (txtReqNo.Text)) = False Then GoTo DelErrPart

                If lblBookType.Text = "I" Then
                    PubDBCn.Execute("UPDATE INV_SUB_ISSUE_DET SET ISSUE_QTY=0 Where AUTO_KEY_ISS=" & Val(txtReqNo.Text) & "")
                    PubDBCn.Execute("UPDATE INV_SUB_ISSUE_HDR SET ISSUE_STATUS='N',ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') Where AUTO_KEY_ISS=" & Val(txtReqNo.Text) & "")
                Else
                    PubDBCn.Execute("Delete from INV_SUB_ISSUE_DET Where AUTO_KEY_ISS=" & Val(txtReqNo.Text) & "")
                    PubDBCn.Execute("Delete from INV_SUB_ISSUE_HDR Where AUTO_KEY_ISS=" & Val(txtReqNo.Text) & "")
                End If

                PubDBCn.CommitTrans()
                RsReqMain.Requery() ''.Refresh
                RsReqDetail.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsReqMain.Requery() ''.Refresh
        RsReqDetail.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        'If PubSuperUser = "U" Then
        If chkIssue.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Issue Completed, Cann't be Modified")
            Exit Sub
        End If
        'End If

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtReqNo.Enabled = False
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
            txtReqNo_Validating(txtReqNo, New System.ComponentModel.CancelEventArgs(False))
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISSUE_STATUS='N' AND SUBSTR(AUTO_KEY_ISS,LENGTH(AUTO_KEY_ISS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        If MainClass.SearchGridMaster((txtReqNo.Text), "INV_SUB_ISSUE_HDR", "AUTO_KEY_ISS", "ISSUE_DATE", , , SqlStr) = True Then
            txtReqNo.Text = AcName
            txtReqNo_Validating(txtReqNo, New System.ComponentModel.CancelEventArgs(False))
            If txtReqNo.Enabled = True Then txtReqNo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        Call TxtDept_DoubleClick(TxtDept, New System.EventArgs())
    End Sub

    Private Sub cmdSearchCC_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCC.Click
        Call txtCost_DoubleClick(txtCost, New System.EventArgs())
    End Sub

    Private Sub cmdSearchEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEmp.Click
        Call txtEmp_DoubleClick(txtEmp, New System.EventArgs())
    End Sub

    Private Sub cmdSearchSSDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSSDept.Click
        Call txtSubStoreDept_DoubleClick(txtSubStoreDept, New System.EventArgs())
    End Sub

    Private Sub FrmStoreReqSub_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xIName As String
        Dim mItemCode As String
        Dim mItemClass As String
        'Dim SqlStr As String = ""

        If lblBookType.Text = "I" Then Exit Sub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColItemDesc Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemDesc
                xIName = .Text
                .Text = ""
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = Trim(AcName)
                Else
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = xIName
                End If
                If MainClass.ValidateWithMasterTable(.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(MasterNo)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColPurpose Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_CLASSIFICATION", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemClass = MasterNo
                End If

                If mItemClass = "3" Then ''Diesel
                    .Col = ColPurpose
                    If MainClass.SearchGridMaster(.Text, "FIN_VEHICLE_MST", "NAME", "VEHICLE_TYPE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND VEHICLE_OWNER='1'") = True Then
                        .Row = .ActiveRow
                        .Col = ColPurpose
                        .Text = AcName
                    End If
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColPurpose)
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
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColStockType)
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 Then '***ROW DEL. OPTION NOT REQ IN INVOICE
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColItemCode
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                FormatSprdMain(eventArgs.row)
            End If
        End If

    End Sub



    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent

        Dim mActiveCol As Integer

        mActiveCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Or eventArgs.KeyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColDemandQty Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColDemandQty
                If Val(SprdMain.Text) <> 0 Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        '                    FormatSprdMain SprdMain.MaxRows
                    End If
                End If
                '            SprdMain.Row = SprdMain.MaxRows
            End If
        ElseIf eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Then
            If mActiveCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
            If mActiveCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))

        End If
        eventArgs.KeyCode = 9999
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mBalQty As Double
        Dim mIssueQty As Double
        Dim xItemCode As String = ""
        Dim xItemDesc As String
        Dim xItemUOM As String
        Dim xStockType As String
        Dim xStockQty As Double
        Dim mIssuedQty As Double
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

        Select Case eventArgs.Col
            Case ColItemCode
                SprdMain.Col = ColItemCode
                xItemCode = Trim(SprdMain.Text)
                If FillItemDescPart(xItemCode, True) = True Then
                    If DuplicateItem = False Then
                        FormatSprdMain(-1)
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColStockType)
                    End If
                End If
            Case ColItemDesc
                SprdMain.Col = ColItemCode
                xItemDesc = Trim(SprdMain.Text)
                If FillItemDescPart(xItemDesc, False) = True Then
                    If DuplicateItem = False Then
                    End If
                End If
            Case ColDemandQty
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColDemandQty
                If Val(SprdMain.Text) <> 0 Then
                    If CheckQty() = True Then
                        SprdMain.Row = SprdMain.ActiveRow
                        SprdMain.Col = ColStockQty
                        xStockQty = Val(SprdMain.Text)



                        '                    If RsCompany!StockBalCheck = "Y" Then
                        If xStockQty < Val(SprdMain.Text) Then
                            MsgInformation("You have not enough Stock.")
                            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColDemandQty)
                        Else
                            If SprdMain.MaxRows = SprdMain.ActiveRow Then
                                MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                                '                        FormatSprdMain SprdMain.MaxRows
                                FormatSprdMain(-1)
                            End If
                        End If
                        '                    Else
                        '                        If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        '                            MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight
                        '    '                        FormatSprdMain SprdMain.MaxRows
                        '                            FormatSprdMain -1
                        '                        End If
                        '                    End If
                    End If
                End If
            Case ColIssueQty

                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColBalQty
                mBalQty = Val(SprdMain.Text)

                SprdMain.Col = ColIssuedQty
                mBalQty = mBalQty + Val(SprdMain.Text)


                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColIssueQty
                mIssueQty = Val(SprdMain.Text)

                If mIssueQty > mBalQty Then
                    MsgInformation("Issued Qty Cann't Be Greater Than Bal Qty.")
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColIssueQty)
                End If

            Case ColStockType
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xItemCode = Trim(SprdMain.Text)

                SprdMain.Col = ColUOM
                xItemUOM = Trim(SprdMain.Text)

                SprdMain.Col = ColStockType
                xStockType = Trim(SprdMain.Text)
                If xStockType = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(xStockType, "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("InValid Stock Type")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                Else
                    SprdMain.Col = ColIssuedQty
                    mIssuedQty = Val(SprdMain.Text)

                    SprdMain.Col = ColStockQty
                    SprdMain.Text = CStr(mIssuedQty + GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, (txtSubStoreDept.Text), xStockType, "", ConSH, mDivisionCode))
                End If
            Case ColPurpose
                Dim mItemClass As String = ""
                Dim mPurpose As String
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xItemCode = Trim(SprdMain.Text)

                If xItemCode = "" Then Exit Sub

                SprdMain.Col = ColPurpose
                mPurpose = Trim(SprdMain.Text)

                If mPurpose = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "ITEM_CLASSIFICATION", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemClass = MasterNo
                End If

                If mItemClass = "3" Then ''Diesel
                    If MainClass.ValidateWithMasterTable(mPurpose, "NAME", "NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And VEHICLE_OWNER='1'") = False Then
                        MsgInformation("Vehicle No is Must for Diesel.")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPurpose)
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                End If

        End Select
        '    FormatSprdMain -1
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

                If (mItemCode = mCheckItemCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItem = True
                    MsgInformation("Duplicate Item : " & mCheckItemCode)
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                    Exit Function
                End If
            Next
        End With
    End Function
    Private Function CheckQty() As Boolean

        On Error GoTo ERR1
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColDemandQty
            If Val(.Text) > 0 Then
                CheckQty = True
            Else
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColDemandQty)
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
        With SprdMain
            SqlStr = "SELECT ITEM_CODE, ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            If pIsItemCode = True Then
                SqlStr = SqlStr & vbCrLf & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND LTRIM(RTRIM(ITEM_SHORT_DESC))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                .Row = .ActiveRow
                .Col = ColItemCode
                .Text = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                .Col = ColItemDesc
                .Text = IIf(IsDbNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)

                .Col = ColUOM
                .Text = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

                .Col = ColStockType
                .Text = IIf(Trim(.Text) = "", "ST", Trim(.Text))

                FillItemDescPart = True
            Else
                FillItemDescPart = False
                If pIsItemCode = True Then
                    MsgInformation("Invalid Item Code")
                Else
                    MsgInformation("Invalid Item Description")
                End If
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
            End If
        End With
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
            txtReqNo.Text = .Text
            txtReqNo_Validating(txtReqNo, New System.ComponentModel.CancelEventArgs(False))
            If txtReqNo.Enabled = True Then txtReqNo.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function AutoGenSeqNo() As Double

        On Error GoTo AutoGenSeqNoErr
        Dim RsMRRMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_ISS)  " & vbCrLf & " FROM INV_SUB_ISSUE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_ISS,LENGTH(AUTO_KEY_ISS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMRRMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With
        AutoGenSeqNo = CDbl(mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim mReqnum As String = ""
        Dim SqlStr As String = ""
        Dim mVNoSeq As Double
        Dim mStatus As String
        Dim mDivisionCode As Double


        If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Val(MasterNo)
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mStatus = IIf(chkIssue.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If Val(txtReqNo.Text) = 0 Then
            mVNoSeq = AutoGenSeqNo()
        Else
            mVNoSeq = Val(txtReqNo.Text)
        End If

        txtReqNo.Text = CStr(Val(CStr(mVNoSeq)))

        SqlStr = ""
        If ADDMode = True Then
            LblMkey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO INV_SUB_ISSUE_HDR (" & vbCrLf & " AUTO_KEY_ISS, " & vbCrLf & " COMPANY_CODE, " & vbCrLf & " ISSUE_DATE, SUB_STORE_DEPT, " & vbCrLf & " DEPT_CODE, " & vbCrLf & " EMP_CODE, REMARKS, COST_CENTER_CODE, " & vbCrLf & " ISSUE_STATUS, " & vbCrLf & " ADDUSER,ADDDATE,MODUSER,MODDATE, DIV_CODE)" & vbCrLf & " VALUES( " & vbCrLf & " " & Val(CStr(mVNoSeq)) & "," & vbCrLf & " " & RsCompany.Fields("Company_Code").Value & "," & vbCrLf & " TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtSubStoreDept.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtDept.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtEmp.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtsubdept.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtCost.Text)) & "', " & vbCrLf & " '" & mStatus & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'',''," & mDivisionCode & ")"
        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE INV_SUB_ISSUE_HDR SET ISSUE_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " SUB_STORE_DEPT='" & txtSubStoreDept.Text & "', " & vbCrLf & " DEPT_CODE='" & txtDept.Text & "', " & vbCrLf & " EMP_CODE ='" & txtEmp.Text & "', " & vbCrLf & " REMARKS ='" & txtsubdept.Text & "'," & vbCrLf & " COST_CENTER_CODE ='" & txtCost.Text & "'," & vbCrLf & " ISSUE_STATUS ='" & mStatus & "'," & vbCrLf & " DIV_CODE ='" & mDivisionCode & "'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND AUTO_KEY_ISS =" & Val(lblMKey.Text) & ""
        End If
        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(CStr(Val(CStr(mVNoSeq))), mDivisionCode) = False Then GoTo ErrPart

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''

        If ADDMode = True Then
            txtReqNo.Text = ""
        End If

        RsReqMain.Requery() ''.Refresh
        RsReqDetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        '    If err.Number = -2147217900 Then
        '        ErrorMsg "Duplicate Item Consumption Generated, Save Again", "Duplicate", vbCritical
        '    Else
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    End If
        ''Resume
    End Function
    Private Function GetLastEntryDate() As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetLastEntryDate = ""
        SqlStr = ""
        SqlStr = "SELECT Max(ISSUE_DATE) AS  ISSUE_DATE " & vbCrLf _
            & " FROM INV_SUB_ISSUE_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_ISS,LENGTH(AUTO_KEY_ISS)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND ISSUE_STATUS='Y'"

        ''AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' 

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetLastEntryDate = IIf(IsDBNull(RsTemp.Fields("ISSUE_DATE").Value), "", RsTemp.Fields("ISSUE_DATE").Value)
        End If

        Exit Function
ErrPart:

    End Function

    Private Function CheckMaterialIssue() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mQty As Double

        CheckMaterialIssue = False
        SqlStr = ""
        mQty = 0

        SqlStr = "SELECT SUM(ISSUE_QTY) AS ISSUE_QTY " & vbCrLf _
            & " FROM INV_SUB_ISSUE_HDR IH, INV_SUB_ISSUE_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND IH.AUTO_KEY_ISS=ID.AUTO_KEY_ISS " & vbCrLf _
            & " AND IH.AUTO_KEY_ISS=" & Val(txtReqNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            mQty = IIf(IsDBNull(RsTemp.Fields("ISSUE_QTY").Value), 0, RsTemp.Fields("ISSUE_QTY").Value)
        End If

        If mQty > 0 Then
            CheckMaterialIssue = True
        End If

        Exit Function
ErrPart:
        CheckMaterialIssue = False
    End Function
    Private Function UpdateDetail1(ByRef pVnoseq As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mUOM As String = ""
        Dim mStockType As String = ""
        Dim mQty As Double
        Dim mRemarks As String
        Dim mIssueQty As Double
        Dim mLotNoRequied As String
        Dim mStatus As String
        Dim mPurpose As String

        SqlStr = " Delete From INV_SUB_ISSUE_DET " & vbCrLf & " WHERE AUTO_KEY_ISS=" & Val(LblMkey.Text) & ""
        PubDBCn.Execute(SqlStr)

        mStatus = IIf(chkIssue.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If lblBookType.Text = "I" Then
            If DeleteStockTRN(PubDBCn, ConStockRefType_SUBISS, (txtReqNo.Text)) = False Then GoTo UpdateDetail1Err
        End If

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUOM
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColStockType
                mStockType = MainClass.AllowSingleQuote(.Text)

                .Col = ColDemandQty
                mQty = Val(.Text)

                .Col = ColIssueQty
                mIssueQty = Val(.Text)

                .Col = ColPurpose
                mPurpose = MainClass.AllowSingleQuote(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)
                SqlStr = ""

                If mItemCode <> "" And mQty > 0 Then
                    SqlStr = " INSERT INTO INV_SUB_ISSUE_DET ( " & vbCrLf _
                        & " AUTO_KEY_ISS,SERIAL_NO,ITEM_CODE,ITEM_UOM,REMARKS," & vbCrLf _
                        & " STOCK_TYPE,DEMAND_QTY,ISSUE_QTY,COMPANY_CODE,DEPT_CODE,COST_CENTER_CODE,ISSUE_STATUS,ISSUE_PURPOSE) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES (" & Val(lblMKey.Text) & ", " & I & "," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mUOM) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mRemarks) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mStockType) & "', " & vbCrLf _
                        & " " & mQty & "," & mIssueQty & "," & RsCompany.Fields("COMPANY_CODE").Value & ",'" & txtDept.Text & "','" & txtCost.Text & "','" & mStatus & "','" & MainClass.AllowSingleQuote(mPurpose) & "' ) "
                    PubDBCn.Execute(SqlStr)

                    If lblBookType.Text = "I" Then
                        If UpdateStockTRN(PubDBCn, ConStockRefType_SUBISS, (txtReqNo.Text), I, (txtReqDate.Text), (txtReqDate.Text), "ST", mItemCode, mUOM, CStr(-1), mIssueQty, 0, "O", 0, 0, "", "", (txtSubStoreDept.Text), (txtDept.Text), "", "N", "To : " & lblDeptname.Text, "-1", ConSH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
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
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim mLockBookCode As Integer

        FieldsVarification = True
        If ValidateBranchLocking((txtReqDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If lblBookType.Text = "R" Then
            mLockBookCode = CInt(ConLockStoreReq)
        Else
            mLockBookCode = CInt(ConLockIssueNote)
        End If

        If ValidateBookLocking(PubDBCn, mLockBookCode, txtReqDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If


        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsReqMain.EOF = True Then Exit Function

        If lblBookType.Text = "I" Then
            If txtReqNo.Text = "" Then
                MsgInformation("Requisition No. Cann't Blank")
                FieldsVarification = False
                Exit Function
            End If
        Else
            If MODIFYMode = True And txtReqNo.Text = "" Then
                MsgInformation("Requisition No. Cann't Blank")
                FieldsVarification = False
                Exit Function
            End If
        End If
        If txtReqDate.Text = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtReqDate.Focus()
            Exit Function
        ElseIf FYChk((txtReqDate.Text)) = False Then
            FieldsVarification = False
            If txtReqDate.Enabled = True Then txtReqDate.Focus()
            Exit Function
        End If

        If lblBookType.Text = "R" And MODIFYMode = True Then
            If CheckMaterialIssue() = True Then
                MsgBox("Material Issue Against this Store Requisition, So cann't be Save.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        If lblBookType.Text = "R" Then
            If CheckStockQty(SprdMain, ColStockQty, ColDemandQty, ColItemCode, ColStockType, True) = False Then
                FieldsVarification = False
                Exit Function
            End If
        Else
            If CheckStockQty(SprdMain, ColStockQty, ColIssueQty, ColItemCode, ColStockType, True) = False Then
                FieldsVarification = False
                Exit Function
            End If

            If CheckBalDemandedQty(SprdMain, ColDemandQty, ColIssueQty) = True Then
                chkIssue.CheckState = System.Windows.Forms.CheckState.Checked
            End If
        End If

        If Trim(txtSubStoreDept.Text) = "" Then
            MsgBox("Sub Store Dept Code is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            txtSubStoreDept.Focus()
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable((txtSubStoreDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISSUBSTORE='Y'") = False Then
                MsgBox("Invalid Sub Store Dept Code. Cann't Save", MsgBoxStyle.Information)
                FieldsVarification = False
                txtSubStoreDept.Focus()
                Exit Function
            End If
        End If

        If Trim(txtDept.Text) = "" Then
            MsgBox("Dept Code is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDept.Focus()
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Invalid Dept Code. Cann't Save", MsgBoxStyle.Information)
                FieldsVarification = False
                txtDept.Focus()
                Exit Function
            End If
        End If

        If Trim(txtEmp.Text) = "" Then
            MsgBox("Employee Code is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            txtEmp.Focus()
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Invalid Employee Code. Cann't Save", MsgBoxStyle.Information)
                FieldsVarification = False
                txtEmp.Focus()
                Exit Function
            End If
        End If

        If PubSuperUser <> "S" Then
            'If mCheckLastEntryDate <> "" Then
            If CDate(txtReqDate.Text) < CDate(PubCurrDate) Then
                    MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            'End If
        End If

        With SprdMain
            Dim mPurpose As String
            Dim mItemCode As String
            Dim mItemClass As String

            For CntRow = 1 To .MaxRows - 1
                .Row = CntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If mItemCode <> "" Then

                    .Row = CntRow
                    SprdMain.Col = ColPurpose
                    mPurpose = Trim(SprdMain.Text)

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_CLASSIFICATION", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemClass = MasterNo
                    End If

                    If mItemClass = "3" Then ''Diesel
                        If MainClass.ValidateWithMasterTable(mPurpose, "NAME", "NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And VEHICLE_OWNER='1'") = False Then
                            MsgInformation("Vehicle No is Must for Diesel.")
                            MainClass.SetFocusToCell(SprdMain, CntRow, ColPurpose)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                End If
            Next

        End With

        Dim mCheckLastEntryDate As String

        mCheckLastEntryDate = GetLastEntryDate()
        If mCheckLastEntryDate <> "" Then
            mCheckLastEntryDate = DateAdd("d", -1, mCheckLastEntryDate)
            If mCheckLastEntryDate <> "" Then
                If CDate(txtReqDate.Text) < CDate(mCheckLastEntryDate) Then
                    MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColDemandQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmStoreReqSub_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblBookType.Text = "I" Then
            Me.Text = "Material Issue Note (Sub-Store)"
        Else
            Me.Text = "Store Requisition Note (Sub-Store)"
        End If

        SqlStr = ""
        SqlStr = "Select * from INV_SUB_ISSUE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_SUB_ISSUE_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        '    Call SetTextLengths

        Clear1()
        If lblBookType.Text = "R" Then
            If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
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

        ''SELECT CLAUSE...

        SqlStr = " SELECT  AUTO_KEY_ISS ISSUE_NO, ISSUE_DATE,SUB_STORE_DEPT, DEPT_CODE DEPT, " & vbCrLf & " EMP_CODE EMP,DECODE(ISSUE_STATUS,'Y','COMPLETE','PENDING') AS STATUS, " & vbCrLf & " REMARKS "

        ''FROM CLAUSE...

        SqlStr = SqlStr & vbCrLf & " FROM INV_SUB_ISSUE_HDR "

        ''WHERE CLAUSE...

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND SUBSTR(AUTO_KEY_ISS,LENGTH(AUTO_KEY_ISS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        ''ORDER BY CLAUSE...

        SqlStr = SqlStr & vbCrLf & " Order by AUTO_KEY_ISS"

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
            .set_ColWidth(6, 3000)

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
            .TypeEditLen = RsReqDetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            If lblBookType.Text = "R" Then
                .set_ColWidth(ColItemDesc, 30)
            Else
                .set_ColWidth(ColItemDesc, 23)
            End If

            .Col = ColUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_UOM", "INV_SUB_ISSUE_DET", PubDBCn)
            .set_ColWidth(ColUOM, 4)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("STOCK_TYPE", "INV_SUB_ISSUE_DET", PubDBCn)
            .set_ColWidth(ColStockType, 4.5)

            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColStockQty, 9)

            .Col = ColDemandQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDemandQty, 9)

            .Col = ColIssueQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColIssueQty, 9)
            If lblBookType.Text = "R" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColIssuedQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColIssuedQty, 8)
            .ColHidden = True

            .Col = ColBalQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBalQty, 8)
            If lblBookType.Text = "R" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("REMARKS", "INV_SUB_ISSUE_DET", PubDBCn)
            If lblBookType.Text = "R" Then
                .set_ColWidth(ColRemarks, 20)
            Else
                .set_ColWidth(ColRemarks, 13)
            End If

            .Col = ColPurpose
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("ISSUE_PURPOSE", "INV_SUB_ISSUE_DET", PubDBCn)
            If lblBookType.Text = "R" Then
                .set_ColWidth(ColPurpose, 11)
            Else
                .set_ColWidth(ColPurpose, 6)
            End If

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUOM)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockQty, ColStockQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIssuedQty, ColBalQty)
        If lblBookType.Text = "I" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColItemCode)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDemandQty, ColDemandQty)
        End If

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsReqDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsReqMain
            txtReqDate.Maxlength = 10
            txtReqNo.Maxlength = .Fields("AUTO_KEY_ISS").Precision
            txtSubStoreDept.Maxlength = .Fields("SUB_STORE_DEPT").DefinedSize
            txtDept.Maxlength = .Fields("DEPT_CODE").DefinedSize
            txtEmp.Maxlength = .Fields("EMP_CODE").DefinedSize
            txtCost.Maxlength = .Fields("COST_CENTER_CODE").DefinedSize
            txtsubdept.Maxlength = .Fields("REMARKS").DefinedSize

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        With RsReqMain
            If Not .EOF Then
                txtReqNo.Enabled = False
                LblMkey.Text = .Fields("AUTO_KEY_ISS").Value


                txtReqNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_ISS").Value), 0, .Fields("AUTO_KEY_ISS").Value)
                txtReqDate.Text = VB6.Format(IIf(IsDbNull(.Fields("ISSUE_DATE").Value), "", .Fields("ISSUE_DATE").Value), "DD/MM/YYYY")
                txtSubStoreDept.Text = IIf(IsDbNull(.Fields("SUB_STORE_DEPT").Value), "", .Fields("SUB_STORE_DEPT").Value)
                txtDept.Text = IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)
                txtEmp.Text = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtCost.Text = IIf(IsDbNull(.Fields("COST_CENTER_CODE").Value), "", .Fields("COST_CENTER_CODE").Value)
                txtsubdept.Text = IIf(IsDbNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                chkIssue.CheckState = IIf(.Fields("ISSUE_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkIssue.Enabled = IIf(.Fields("ISSUE_STATUS").Value = "Y", False, True)

                If MainClass.ValidateWithMasterTable((txtSubStoreDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISSUBSTORE='Y'") = True Then
                    lblSubStore.Text = MasterNo
                Else
                    lblSubStore.Text = ""
                End If

                If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblDeptname.Text = MasterNo
                Else
                    lblDeptname.Text = ""
                End If

                If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblEmpName.Text = MasterNo
                Else
                    lblEmpName.Text = ""
                End If

                If MainClass.ValidateWithMasterTable((txtCost.Text), "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblCostctr.Text = MasterNo
                Else
                    lblCostctr.Text = ""
                End If

                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), "", .Fields("DIV_CODE").Value)
                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If

                If lblBookType.Text = "I" Then
                    txtSubStoreDept.Enabled = False
                    cmdSearchSSDept.Enabled = False
                    txtDept.Enabled = False

                    txtEmp.Enabled = False
                    txtCost.Enabled = False
                    cmdSearchEmp.Enabled = False
                    cmdSearchDept.Enabled = False
                    cmdSearchCC.Enabled = False
                End If

                cboDivision.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
                Call ShowDetail1(Val(lblMKey.Text), mDivisionCode)

            End If
        End With
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        '    FormatSprdMain -1

        SprdMain.Enabled = True
        txtReqNo.Enabled = True
        cmdsearch.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub ShowDetail1(ByRef pReqNum As Double, ByRef mDivisionCode As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        Dim mDemandQty As String
        Dim mIssueQty As String
        Dim mStkType As String
        Dim mRemarks As String


        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_SUB_ISSUE_DET  " & vbCrLf & " Where AUTO_KEY_ISS = " & Val(CStr(pReqNum)) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsReqDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColUOM
                mItemUOM = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                SprdMain.Text = mItemUOM

                SprdMain.Col = ColStockType
                mStkType = IIf(IsDbNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)
                SprdMain.Text = mStkType

                mIssueQty = IIf(IsDbNull(.Fields("ISSUE_QTY").Value), 0, .Fields("ISSUE_QTY").Value)

                SprdMain.Col = ColStockQty
                SprdMain.Text = CStr(CDbl(mIssueQty) + GetBalanceStockQty(mItemCode, (txtReqDate.Text), mItemUOM, (txtSubStoreDept.Text), mStkType, "", ConSH, mDivisionCode))

                SprdMain.Col = ColDemandQty
                mDemandQty = IIf(IsDbNull(.Fields("DEMAND_QTY").Value), 0, .Fields("DEMAND_QTY").Value)
                SprdMain.Text = mDemandQty

                SprdMain.Col = ColIssueQty
                SprdMain.Text = mIssueQty

                SprdMain.Col = ColIssuedQty
                SprdMain.Text = mIssueQty

                SprdMain.Col = ColBalQty
                SprdMain.Text = CStr(Val(CStr(CDbl(mDemandQty) - CDbl(mIssueQty))))

                SprdMain.Col = ColRemarks
                mRemarks = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                SprdMain.Text = mRemarks

                SprdMain.Col = ColPurpose
                SprdMain.Text = IIf(IsDBNull(.Fields("ISSUE_PURPOSE").Value), "", .Fields("ISSUE_PURPOSE").Value)


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
        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()


        LblMkey.Text = ""

        txtReqDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtReqNo.Text = ""
        txtSubStoreDept.Text = ""
        txtDept.Text = ""
        txtEmp.Text = ""
        txtCost.Text = ""
        txtsubdept.Text = ""

        lblCostctr.Text = ""
        lblDeptname.Text = ""
        lblSubStore.Text = ""
        lblEmpName.Text = ""
        chkIssue.CheckState = System.Windows.Forms.CheckState.Unchecked
        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        txtReqDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)

        txtSubStoreDept.Enabled = True
        txtDept.Enabled = True
        txtEmp.Enabled = True
        txtCost.Enabled = True
        cmdSearchEmp.Enabled = True
        cmdSearchDept.Enabled = True
        cmdSearchCC.Enabled = True
        cmdSearchSSDept.Enabled = True
        chkIssue.Enabled = IIf(lblBookType.Text = "I", True, False)

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub FrmStoreReqSub_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmStoreReqSub_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Public Sub FrmStoreReqSub_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

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
        ''Me.Width = VB6.TwipsToPixelsX(11355)



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


        'AdoDCMain.Visible = False

        txtReqNo.Enabled = True
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
        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With

    End Sub


    Private Sub txtCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCost_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtDept.Text) = "" Then
            MsgInformation("Please Select Dept. First.")
            txtDept.Focus()
            Exit Sub
        End If
        SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf _
            & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf _
            & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"

        '    If MainClass.SearchGridMaster(txtCost.Text, "FIN_CCENTER_HDR", "CC_CODE", "CC_DESC", , , SqlStr) = True Then
        If MainClass.SearchGridMasterBySQL2((txtCost.Text), SqlStr) = True Then
            txtCost.Text = AcName
            lblCostctr.Text = AcName1
            txtCost_Validating(txtCost, New System.ComponentModel.CancelEventArgs(False))
            If txtCost.Enabled = True Then txtCost.Focus()
        End If

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        'If MainClass.SearchGridMaster((txtCost.Text), "FIN_CCENTER_HDR", "CC_CODE", "CC_DESC", , , SqlStr) = True Then
        '    txtCost.Text = AcName
        '    lblCostctr.Text = AcName1
        '    txtCost_Validating(txtCost, New System.ComponentModel.CancelEventArgs(False))
        '    If txtCost.Enabled = True Then txtCost.Focus()
        'End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""


        If Trim(txtCost.Text) = "" Then GoTo EventExitSub
        txtCost.Text = VB6.Format(txtCost.Text, "000")
        If Trim(txtDept.Text) = "" Then
            MsgInformation("Please Select Dept. First.")
            txtDept.Focus()
            GoTo EventExitSub
        End If

        SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf _
            & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf _
            & " AND IH.CC_CODE='" & MainClass.AllowSingleQuote((txtCost.Text)) & "'"


        If ADDMode = True Then
            SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            lblCostctr.Text = IIf(IsDBNull(RsTemp.Fields("CC_DESC").Value), "", RsTemp.Fields("CC_DESC").Value)
        Else
            MsgInformation("Invalid Cost Center Code for Department : " & Trim(txtDept.Text))
            Cancel = True
        End If

        'If Trim(txtCost.Text) = "" Then GoTo EventExitSub

        'If MainClass.ValidateWithMasterTable((txtCost.Text), "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    lblCostctr.Text = MasterNo
        'Else
        '    MsgInformation("Invalid CostC Code")
        '    Cancel = True
        'End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtReqDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReqDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReqDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReqDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtReqDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtReqDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            lblDeptname.Text = AcName
            '            txtDept_Validate False
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub TxtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then TxtDept_DoubleClick(TxtDept, New System.EventArgs())
    End Sub

    Private Sub TxtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtDept.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblDeptname.Text = MasterNo
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
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtEmp.Text = AcName1
            lblEmpName.Text = AcName
            txtEmp_Validating(txtEmp, New System.ComponentModel.CancelEventArgs(False))
            If txtEmp.Enabled = True Then txtEmp.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtEmp_DoubleClick(txtEmp, New System.EventArgs())
    End Sub

    Private Sub txtEmp_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmp.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtEmp.Text) = "" Then GoTo EventExitSub

        txtEmp.Text = VB6.Format(txtEmp.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblEmpName.Text = MasterNo
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

    Private Sub txtReqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReqNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtReqNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReqNo.DoubleClick
        Call cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtReqNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReqNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtReqNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtReqNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtReqNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Public Sub txtReqNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReqNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mReqnum As String = ""

        If Trim(txtReqNo.Text) = "" Then GoTo EventExitSub

        If Len(txtReqNo.Text) < 6 Then
            txtReqNo.Text = Val(txtReqNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsReqMain.EOF = False Then mReqnum = RsReqMain.Fields("AUTO_KEY_ISS").Value

        SqlStr = "Select * From INV_SUB_ISSUE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(AUTO_KEY_ISS))=" & Val(txtReqNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsReqMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Item Consumption, Use Generate Item Consumption Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_SUB_ISSUE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " LTRIM(RTRIM(AUTO_KEY_ISS))=" & Val(mReqnum) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtsubdept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtsubdept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSubStoreDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubStoreDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSubStoreDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubStoreDept.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISSUBSTORE='Y'"

        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtSubStoreDept.Text = AcName1
            lblSubStore.Text = AcName
            '            txtSubStoreDept_Validate False
            If txtSubStoreDept.Enabled = True Then txtSubStoreDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtSubStoreDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSubStoreDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSubStoreDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSubStoreDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSubStoreDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtSubStoreDept_DoubleClick(txtSubStoreDept, New System.EventArgs())
    End Sub

    Private Sub txtSubStoreDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSubStoreDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtSubStoreDept.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtSubStoreDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISSUBSTORE='Y'") = True Then
            lblSubStore.Text = MasterNo
        Else
            MsgInformation("Invalid Sub Store Depatment Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub Label4_Click(sender As System.Object, e As System.EventArgs) Handles Label4.Click

    End Sub

End Class
