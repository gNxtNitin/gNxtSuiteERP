Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmConsumption
    Inherits System.Windows.Forms.Form
    Dim RsConsumptionHdr As ADODB.Recordset ''Recordset	
    Dim RsConsumptionDet As ADODB.Recordset ''Recordset	
    'Private PvtDBCn As ADODB.Connection	

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12
    Dim xMyMenu As String

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColUom As Short = 3
    Private Const ColStockType As Short = 4
    Private Const ColStockQty As Short = 5
    Private Const ColConsQty As Short = 6
    Private Const ColRemarks As Short = 7


    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        Dim cntRow As Integer
        Dim mDivisionCode As Double
        Dim xItemCode As String
        Dim xItemUOM As String
        Dim xStockType As String

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If
        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColItemCode
                xItemCode = Trim(sprdMain.Text)

                .Col = ColUom
                xItemUOM = Trim(sprdMain.Text)

                .Col = ColStockType
                xStockType = Trim(sprdMain.Text)
                If xStockType = "" Then GoTo NextRec

                .Col = ColStockQty
                .Text = CStr(GetBalanceStockQty(xItemCode, (txtConsDate.Text), xItemUOM, (txtDept.Text), xStockType, "", "PH", mDivisionCode, ConStockRefType_CON, Val(txtNumber.Text)))
NextRec:
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

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            sprdMain.Enabled = True
            txtNumber.Enabled = False
            cmdsearch.Enabled = False
        Else
            cmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(sprdMain)
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

        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
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
        Dim mLockBookCode As Integer

        If ValidateBranchLocking((txtConsDate.Text)) = True Then
            Exit Sub
        End If

        If Trim(txtNumber.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsConsumptionHdr.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.	
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PRD_CONSUMPTION_HDR", (txtNumber.Text), RsConsumptionHdr, "AUTO_KEY_CONS") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_CONSUMPTION_HDR", "AUTO_KEY_CONS", (txtNumber.Text)) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_CON, (txtNumber.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("Delete from PRD_CONSUMPTION_DET Where AUTO_KEY_CONS=" & Val(txtNumber.Text) & "")
                PubDBCn.Execute("Delete from PRD_CONSUMPTION_HDR Where AUTO_KEY_CONS=" & Val(txtNumber.Text) & "")

                PubDBCn.CommitTrans()
                RsConsumptionHdr.Requery() ''.Refresh	
                RsConsumptionDet.Requery() ''.Refresh	
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''	
        RsConsumptionHdr.Requery() ''.Refresh	
        RsConsumptionDet.Requery() ''.Refresh	
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsConsumptionHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            sprdMain.Enabled = True
            txtNumber.Enabled = False
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

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnConsumption(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnConsumption(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnConsumption(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "Daily Consumption"

        SqlStr = " SELECT PRD_CONSUMPTION_HDR.*,PRD_CONSUMPTION_DET.*,INV_ITEM_MST.ITEM_SHORT_DESC, " & vbCrLf & " PAY_EMPLOYEE_MST.EMP_NAME,PAY_DEPT_MST.DEPT_DESC " & vbCrLf & " FROM PRD_CONSUMPTION_HDR,PRD_CONSUMPTION_DET,INV_ITEM_MST, " & vbCrLf & " PAY_EMPLOYEE_MST,PAY_DEPT_MST " & vbCrLf & " WHERE PRD_CONSUMPTION_HDR.AUTO_KEY_CONS=PRD_CONSUMPTION_DET.AUTO_KEY_CONS(+) " & vbCrLf & " AND PRD_CONSUMPTION_DET.COMPANY_CODE=INV_ITEM_MST.COMPANY_CODE(+) " & vbCrLf & " AND PRD_CONSUMPTION_DET.ITEM_CODE=INV_ITEM_MST.ITEM_CODE(+) " & vbCrLf & " AND PRD_CONSUMPTION_HDR.COMPANY_CODE=PAY_EMPLOYEE_MST.COMPANY_CODE(+) " & vbCrLf & " AND PRD_CONSUMPTION_HDR.EMP_CODE=PAY_EMPLOYEE_MST.EMP_CODE(+) " & vbCrLf & " AND PRD_CONSUMPTION_HDR.COMPANY_CODE=PAY_DEPT_MST.COMPANY_CODE(+) " & vbCrLf & " AND PRD_CONSUMPTION_HDR.DEPT_CODE=PAY_DEPT_MST.DEPT_CODE(+) " & vbCrLf & " AND PRD_CONSUMPTION_HDR.AUTO_KEY_CONS=" & Val(txtNumber.Text) & ""

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\Consumption.rpt"

        SetCrpt(Report1, Mode, 1, mTitle, , True, xMyMenu)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
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
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUBSTR(AUTO_KEY_CONS,LENGTH(AUTO_KEY_CONS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        If MainClass.SearchGridMaster(txtNumber.Text, "PRD_CONSUMPTION_HDR", "AUTO_KEY_CONS", "CONS_DATE", "DEPT_CODE", , SqlStr) = True Then
            txtNumber.Text = AcName
            'txtNumber_Validate(False)
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
            If txtNumber.Enabled = True Then txtNumber.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        Call txtDept_DoubleClick(txtDept, New System.EventArgs())
    End Sub

    Private Sub CmdSearchCC_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCC.Click
        Call txtCost_DoubleClick(txtCost, New System.EventArgs())
    End Sub

    Private Sub cmdSearchEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEmp.Click
        Call txtEmp_DoubleClick(txtEmp, New System.EventArgs())
    End Sub

    Private Sub frmConsumption_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xIName As String
        Dim SqlStr As String = ""

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND ITEM_STATUS='A'") = True Then
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

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mBalQty As Double
        Dim mConsQty As Double
        Dim xItemCode As String
        Dim xItemDesc As String
        Dim xItemUOM As String
        Dim xStockType As String
        Dim xStockQty As Double

        'Dim xAutoIssue As Boolean
        Dim mWareHouse As String
        Dim mDivisionCode As Double

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If


        '    xAutoIssue = CheckAutoIssue(txtConsDate.Text)	
        '	
        '    If xAutoIssue = True Then	
        '        mWareHouse = "WH"	
        '    Else	
        '        mWareHouse = "PH"	
        '    End If	

        mWareHouse = "PH"

        If eventArgs.NewRow = -1 Then Exit Sub

        sprdMain.Row = sprdMain.ActiveRow

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Col = ColItemCode
                xItemCode = Trim(SprdMain.Text)
                If xItemCode = "" Then Exit Sub
                If FillItemDescPart(xItemCode, True) = True Then
                    If DuplicateItem() = False Then
                        FormatSprdMain(-1)
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColStockType)
                    Else
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                Else
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColItemDesc
                SprdMain.Col = ColItemCode
                xItemDesc = Trim(SprdMain.Text)
                If xItemDesc = "" Then Exit Sub
                If FillItemDescPart(xItemDesc, False) = True Then
                    If DuplicateItem() = True Then
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                Else
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColStockType
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xItemCode = Trim(SprdMain.Text)

                SprdMain.Col = ColUom
                xItemUOM = Trim(SprdMain.Text)

                SprdMain.Col = ColStockType
                xStockType = Trim(SprdMain.Text)
                If xStockType = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(xStockType, "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    '                MsgInformation "InValid Stock Type"	
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                    eventArgs.cancel = True
                    Exit Sub
                Else
                    SprdMain.Col = ColConsQty
                    mConsQty = Val(SprdMain.Text)

                    SprdMain.Col = ColStockQty
                    SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtConsDate.Text), xItemUOM, (txtDept.Text), xStockType, "", mWareHouse, mDivisionCode, ConStockRefType_CON, Val(txtNumber.Text)))
                End If
            Case ColConsQty
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColConsQty
                If Val(SprdMain.Text) = 0 Then Exit Sub

                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColStockQty
                xStockQty = Val(SprdMain.Text)

                SprdMain.Col = ColConsQty
                If Val(SprdMain.Text) <> 0 Then
                    If xStockQty < Val(SprdMain.Text) Then
                        MsgInformation("You have not enough Stock.")
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColConsQty)
                        eventArgs.cancel = True
                        Exit Sub
                    Else
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        FormatSprdMain(-1)
                    End If
                End If
        End Select

        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1

        Dim mItemCode As String

        Dim I As Integer
        Dim j As Integer
        Dim mScrapWt As Double = 0

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value <> 101 Then
            With SprdMain
                j = .MaxRows
                For I = 1 To j
                    .Row = I

                    .Col = ColItemCode
                    mItemCode = Trim(.Text)

                    .Col = ColConsQty
                    mScrapWt = mScrapWt + Val(.Text)


                Next I
            End With

            txtScrapItemWeight.Text = VB6.Format(mScrapWt, "0.000")
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

        With sprdMain
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
                    MainClass.SetFocusToCell(sprdMain, .ActiveRow, ColItemCode)
                    Exit Function
                End If
            Next
        End With
    End Function

    Private Function FillItemDescPart(ByRef pItemCode As String, ByRef pIsItemCode As Boolean) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(pItemCode) = "" Then Exit Function
        With sprdMain
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
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColUom
                .Text = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

                .Col = ColStockType
                .Text = IIf(Trim(.Text) = "", "ST", Trim(.Text))

                FillItemDescPart = True
            Else
                FillItemDescPart = False

                MainClass.SetFocusToCell(sprdMain, .ActiveRow, ColItemCode)
            End If
        End With
        Exit Function
ERR1:
        FillItemDescPart = False
        MsgInformation(Err.Description)
    End Function

    Private Sub SprdMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdMain.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mCancel As Boolean
        mCancel = False
        With sprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, mCancel))
            Cancel = mCancel
        End With
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdView.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.Row
            .Col = 1
            txtNumber.Text = .Text
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
            If txtNumber.Enabled = True Then txtNumber.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub

    Private Function AutoGenSeqNo() As Double

        On Error GoTo AutoGenSeqNoErr
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_CONS)  " & vbCrLf & " FROM PRD_CONSUMPTION_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CONS,LENGTH(AUTO_KEY_CONS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
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
        Dim mReqnum As String
        Dim SqlStr As String = ""
        Dim mVNoSeq As Double
        Dim mEntryDate As String
        Dim mDivisionCode As Double



        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If Val(txtNumber.Text) = 0 Then
            mVNoSeq = AutoGenSeqNo()
        Else
            mVNoSeq = Val(txtNumber.Text)
        End If

        txtNumber.Text = CStr(Val(CStr(mVNoSeq)))

        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        SqlStr = ""
        If ADDMode = True Then
            LblMkey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO PRD_CONSUMPTION_HDR (" & vbCrLf _
                & " AUTO_KEY_CONS, " & vbCrLf _
                & " COMPANY_CODE, " & vbCrLf _
                & " CONS_DATE, " & vbCrLf _
                & " DEPT_CODE, " & vbCrLf _
                & " EMP_CODE, COST_CENTER_CODE, SHIFT_CODE, " & vbCrLf _
                & " REMARKS, " & vbCrLf _
                & " ADDUSER,ADDDATE,MODUSER,MODDATE, DIV_CODE, SCRAP_ITEM_CODE, SCRAP_QTY)" & vbCrLf _
                & " VALUES( " & vbCrLf _
                & " " & Val(mVNoSeq) & "," & vbCrLf _
                & " " & RsCompany.Fields("Company_Code").Value & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtConsDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtEmp.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtCost.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(cboShiftcd.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'',''," & mDivisionCode & ",'" & MainClass.AllowSingleQuote(txtScrapItemCode.Text) & "', " & Val(txtScrapItemWeight.Text) & ")"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE PRD_CONSUMPTION_HDR SET CONS_DATE=TO_DATE('" & VB6.Format(txtConsDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " DEPT_CODE='" & txtDept.Text & "', " & vbCrLf _
                & " EMP_CODE ='" & txtEmp.Text & "', " & vbCrLf _
                & " COST_CENTER_CODE ='" & txtCost.Text & "'," & vbCrLf _
                & " SHIFT_CODE ='" & MainClass.AllowSingleQuote(cboShiftcd.Text) & "', " & vbCrLf _
                & " REMARKS ='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf _
                & " SCRAP_ITEM_CODE='" & MainClass.AllowSingleQuote(txtScrapItemCode.Text) & "', SCRAP_QTY=" & Val(txtScrapItemWeight.Text) & "," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),DIV_CODE=" & mDivisionCode & " " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                & " AND AUTO_KEY_CONS =" & Val(lblMKey.Text) & ""
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
            txtNumber.Text = ""
        End If

        RsConsumptionHdr.Requery() ''.Refresh	
        RsConsumptionDet.Requery() ''.Refresh	
        If Err.Description = "" Then Exit Function
        '    If err.Number = -2147217900 Then	
        '        ErrorMsg "Duplicate Item Consumption Generated, Save Again", "Duplicate", vbCritical	
        '    Else	
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    End If	
        ''Resume	
    End Function

    Private Function AutoGenKeyIssRec() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Double
        Dim SqlStr As String = ""
        Dim mMaxValue As String
        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_CONSREC)  " & vbCrLf & " FROM PRD_ISSREC_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

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
        AutoGenKeyIssRec = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Function UpdateDetail1(ByRef pVnoseq As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim RsBOM As ADODB.Recordset
        Dim mSqlStr As String
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mUOM As String
        Dim mStockType As String
        Dim mConsQty As Double
        Dim mRemarks As String
        Dim mProd_Type As String
        Dim cntRow As Integer
        'Dim xAutoIssue As Boolean
        Dim mWareHouse As String

        '    xAutoIssue = CheckAutoIssue(txtConsDate.Text)	
        '	
        '    If xAutoIssue = True Then	
        '        mWareHouse = "WH"	
        '    Else	
        '        mWareHouse = "PH"	
        '    End If	

        mWareHouse = "PH"

        SqlStr = " Delete From PRD_CONSUMPTION_DET " & vbCrLf & " WHERE AUTO_KEY_CONS=" & Val(LblMkey.Text) & ""

        PubDBCn.Execute(SqlStr)

        If DeleteStockTRN(PubDBCn, ConStockRefType_CON, (txtNumber.Text)) = False Then GoTo UpdateDetail1Err

        With sprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUom
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColStockType
                mStockType = MainClass.AllowSingleQuote(.Text)

                .Col = ColConsQty
                mConsQty = Val(.Text)

                '            mProd_Type = GetProductionType(mItemCode)	

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mItemCode <> "" And mConsQty > 0 Then
                    SqlStr = " INSERT INTO PRD_CONSUMPTION_DET ( " & vbCrLf _
                        & " AUTO_KEY_CONS,SERIAL_NO,COMPANY_CODE,ITEM_CODE,ITEM_UOM," & vbCrLf _
                        & " STOCK_TYPE,CONS_QTY,REMARKS) " & vbCrLf _
                        & " VALUES (" & Val(lblMKey.Text) & ", " & I & ", " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mUOM) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mStockType) & "', " & vbCrLf _
                        & " " & mConsQty & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mRemarks) & "') "

                    PubDBCn.Execute(SqlStr)

                    '                If mProd_Type = "P" Or mProd_Type = "J" Then	
                    If UpdateStockTRN(PubDBCn, ConStockRefType_CON, (txtNumber.Text), I, (txtConsDate.Text), (txtConsDate.Text), mStockType, mItemCode, mUOM, CStr(-1), mConsQty, 0, "O", 0, 0, "", "", (txtDept.Text), (txtDept.Text), (txtCost.Text), "N", "From : " & lblDeptName.Text & "  : (Consumption) -" & ConStockRefType_CON, "-1", mWareHouse, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
                    '                End If	
                End If


            Next
        End With

        I = I + 1
        If Trim(txtScrapItemCode.Text) <> "" And Val(txtScrapItemWeight.Text) > 0 Then
            If UpdateStockTRN(PubDBCn, ConStockRefType_CON, (txtNumber.Text), I, (txtConsDate.Text), (txtConsDate.Text), "SC", Trim(txtScrapItemCode.Text), Trim(lblScrapItemUOM.Text), CStr(-1), Val(txtScrapItemWeight.Text), 0, "I", 0, 0, "", "", (txtDept.Text), (txtDept.Text), (txtCost.Text), "N", "From : " & lblDeptname.Text & "  : (Consumption - Scrap) -" & ConStockRefType_CON, "-1", mWareHouse, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
            '                End If	
        End If

        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Function

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim mLockBookCode As Integer
        Dim mCheckLastEntryDate As String
        Dim mDeptCode As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mProd_Type As String
        Dim cntRow As Integer
        Dim mItemCode As String

        FieldsVarification = True
        If ValidateBranchLocking((txtConsDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsConsumptionHdr.EOF = True Then Exit Function

        If txtConsDate.Text = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtConsDate.Focus()
            Exit Function
        ElseIf FYChk((txtConsDate.Text)) = False Then
            FieldsVarification = False
            If txtConsDate.Enabled = True Then txtConsDate.Focus()
            Exit Function
        End If

        If CheckStockQty(sprdMain, ColStockQty, ColConsQty, ColItemCode, ColStockType, True) = False Then
            FieldsVarification = False
            Exit Function
        End If

        If ValidateDeptRight(PubUserID, Trim(txtDept.Text), UCase(Trim(lblDeptName.Text))) = False Then
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDept.Text) = "" Then
            MsgBox("Dept Code is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDept.Focus()
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = False Then
                MsgBox("Invalid Dept Code. Cann't Save", vbInformation)
                FieldsVarification = False
                txtDept.Focus()
                Exit Function
            End If
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If

        If Trim(txtCost.Text) = "" Then
            MsgBox("Cost Center Code is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtCost.Enabled Then txtCost.Focus()
            Exit Function
        Else
            SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf _
                & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf _
                & " AND IH.CC_CODE='" & MainClass.AllowSingleQuote(txtCost.Text) & "'" & vbCrLf _
                & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Invalid Cost Center Code for Department : " & Trim(txtDept.Text))
                FieldsVarification = False
                If txtCost.Enabled Then txtCost.Focus()
                Exit Function
            End If
        End If

        If Trim(txtEmp.Text) = "" Then
            MsgBox("Employee Code is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            txtEmp.Focus()
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Invalid Employee Code. Cann't Save", MsgBoxStyle.Information)
                FieldsVarification = False
                txtEmp.Focus()
                Exit Function
            End If
        End If

        If PubSuperUser <> "S" Then
            mCheckLastEntryDate = GetLastEntryDate
            If mCheckLastEntryDate <> "" Then
                If CDate(txtConsDate.Text) < CDate(mCheckLastEntryDate) Then
                    MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)
                    If mItemCode <> "" Then
                        mProd_Type = GetProductionType(mItemCode)

                        If mProd_Type = "C" Or mProd_Type = "T" Then
                        Else
                            FieldsVarification = False
                            MsgInformation("Only Main Consumable / Tools Items Can be Saved.")
                            MainClass.SetFocusToCell(SprdMain, cntRow, ColItemCode)
                            Exit Function
                        End If
                    End If
                Next
            End With
        End If

        If Trim(txtScrapItemCode.Text) <> "" Then
            Dim mStockType As String
            mStockType = GetStockType(PubDBCn, Trim(txtScrapItemCode.Text), 1)

            If mStockType <> "SC" Then
                FieldsVarification = False
                MsgInformation("Please Select Scrap Item Code.")
                txtScrapItemCode.Focus()
                Exit Function
            End If
        End If
        CalcTots()

        If MainClass.ValidDataInGrid(sprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(sprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function
        'If MainClass.ValidDataInGrid(sprdMain, ColConsQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False : Exit Function

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
        SqlStr = "SELECT Max(CONS_DATE) AS  CONS_DATE " & vbCrLf _
            & " FROM PRD_CONSUMPTION_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_CONS,LENGTH(AUTO_KEY_CONS)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetLastEntryDate = IIf(IsDbNull(RsTemp.Fields("CONS_DATE").Value), "", RsTemp.Fields("CONS_DATE").Value)
        End If

        Exit Function
ErrPart:

    End Function

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub frmConsumption_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Daily Consumption Entry"

        SqlStr = ""
        SqlStr = "Select * from PRD_CONSUMPTION_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsConsumptionHdr, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_CONSUMPTION_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsConsumptionDet, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        '    Call SetTextLengths	

        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())

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

        SqlStr = " SELECT  AUTO_KEY_CONS CONS_NO,CONS_DATE,DEPT_CODE DEPT, " & vbCrLf & " EMP_CODE EMP, " & vbCrLf & " REMARKS "

        ''FROM CLAUSE...	

        SqlStr = SqlStr & vbCrLf & " FROM PRD_CONSUMPTION_HDR "

        ''WHERE CLAUSE...	

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND SUBSTR(AUTO_KEY_CONS,LENGTH(AUTO_KEY_CONS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        ''ORDER BY CLAUSE...	

        SqlStr = SqlStr & vbCrLf & " Order by AUTO_KEY_CONS"

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
        With sprdMain
            .set_RowHeight(0, ConRowHeight * 2)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.25)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsConsumptionDet.Fields("ITEM_CODE").DefinedSize ''	
            .set_ColWidth(ColItemCode, 9)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemDesc, 34)

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_UOM", "PRD_CONSUMPTION_DET", PubDBCn)
            .set_ColWidth(ColUom, 4)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("STOCK_TYPE", "PRD_CONSUMPTION_DET", PubDBCn)
            .set_ColWidth(ColStockType, 4.5)

            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColStockQty, 11)

            .Col = ColConsQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColConsQty, 11)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("REMARKS", "PRD_CONSUMPTION_DET", PubDBCn)
            .set_ColWidth(ColRemarks, 20)

        End With

        MainClass.ProtectCell(sprdMain, 1, sprdMain.MaxRows, ColItemDesc, ColUom)
        MainClass.ProtectCell(sprdMain, 1, sprdMain.MaxRows, ColStockQty, ColStockQty)

        MainClass.SetSpreadColor(sprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsConsumptionDet.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsConsumptionHdr
            txtConsDate.Maxlength = 10
            txtNumber.Maxlength = .Fields("AUTO_KEY_CONS").Precision
            txtDept.Maxlength = .Fields("DEPT_CODE").DefinedSize
            txtEmp.Maxlength = .Fields("EMP_CODE").DefinedSize
            txtCost.Maxlength = .Fields("COST_CENTER_CODE").DefinedSize
            txtRemarks.MaxLength = .Fields("REMARKS").DefinedSize

            txtScrapItemCode.MaxLength = .Fields("SCRAP_ITEM_CODE").DefinedSize
            txtScrapItemWeight.MaxLength = .Fields("SCRAP_QTY").Precision

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mEntryDate As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        With RsConsumptionHdr
            If Not .EOF Then
                txtNumber.Enabled = False
                LblMkey.Text = .Fields("AUTO_KEY_CONS").Value

                txtNumber.Text = IIf(IsDbNull(.Fields("AUTO_KEY_CONS").Value), 0, .Fields("AUTO_KEY_CONS").Value)
                txtConsDate.Text = VB6.Format(IIf(IsDbNull(.Fields("CONS_DATE").Value), "", .Fields("CONS_DATE").Value), "DD/MM/YYYY")


                mEntryDate = IIf(IsDbNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value) & " - " & VB6.Format(IIf(IsDbNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY HH:MM")
                mEntryDate = mEntryDate & vbCrLf & IIf(IsDbNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value) & " - " & VB6.Format(IIf(IsDbNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY HH:MM")
                txtEntryDate.Text = mEntryDate

                txtDept.Text = IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)
                txtEmp.Text = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtCost.Text = IIf(IsDbNull(.Fields("COST_CENTER_CODE").Value), "", .Fields("COST_CENTER_CODE").Value)
                TxtRemarks.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                cboShiftcd.Text = IIf(IsDbNull(.Fields("SHIFT_CODE").Value), "", .Fields("SHIFT_CODE").Value)

                txtScrapItemCode.Text = IIf(IsDBNull(.Fields("SCRAP_ITEM_CODE").Value), "", .Fields("SCRAP_ITEM_CODE").Value) ' .Fields("SCRAP_ITEM_CODE").DefinedSize
                If MainClass.ValidateWithMasterTable(txtScrapItemCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblScrapItemCode.Text = MasterNo
                Else
                    lblScrapItemCode.Text = ""
                End If

                If MainClass.ValidateWithMasterTable(txtScrapItemCode.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblScrapItemUOM.Text = MasterNo
                Else
                    lblScrapItemUOM.Text = ""
                End If

                txtScrapItemWeight.Text = IIf(IsDBNull(.Fields("SCRAP_QTY").Value), "", .Fields("SCRAP_QTY").Value) ' .Fields("SCRAP_QTY").Precision


                If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    lblDeptname.text = MasterNo
                Else
                    lblDeptname.text = ""
                End If

                If MainClass.ValidateWithMasterTable(txtEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    lblEmpname.text = MasterNo
                Else
                    lblEmpname.text = ""
                End If

                If MainClass.ValidateWithMasterTable(txtCost.Text, "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    lblCostctr.text = MasterNo
                Else
                    lblCostctr.text = ""
                End If

                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If

                Call ShowDetail1(lblMKey.Text, mDivisionCode)
            End If
        End With
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsConsumptionHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        sprdMain.Enabled = True
        txtNumber.Enabled = True
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
        Dim mItemUOM As String
        Dim mConsQty As String
        Dim mStkType As String
        Dim mRemarks As String
        Dim mDate As String
        'Dim xAutoIssue As Boolean
        Dim mWareHouse As String

        '    xAutoIssue = CheckAutoIssue(txtConsDate.Text)	
        '	
        '    If xAutoIssue = True Then	
        '        mWareHouse = "WH"	
        '    Else	
        '        mWareHouse = "PH"	
        '    End If	

        mWareHouse = "PH"

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM PRD_CONSUMPTION_DET  " & vbCrLf & " Where AUTO_KEY_CONS = " & Val(CStr(pReqNum)) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsConsumptionDet, ADODB.LockTypeEnum.adLockReadOnly)
        With RsConsumptionDet
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1	
            I = 1
            .MoveFirst()

            Do While Not .EOF

                sprdMain.Row = I

                sprdMain.Col = ColItemCode
                mItemCode = IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                sprdMain.Text = Trim(mItemCode)

                sprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                sprdMain.Text = mItemDesc

                sprdMain.Col = ColUom
                mItemUOM = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                sprdMain.Text = mItemUOM

                sprdMain.Col = ColStockType
                mStkType = IIf(IsDbNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)
                sprdMain.Text = mStkType

                mConsQty = IIf(IsDbNull(.Fields("CONS_QTY").Value), 0, .Fields("CONS_QTY").Value)

                sprdMain.Col = ColStockQty
                sprdMain.Text = CStr(CDbl(mConsQty) + GetBalanceStockQty(mItemCode, (txtConsDate.Text), mItemUOM, (txtDept.Text), mStkType, "", mWareHouse, mDivisionCode))

                sprdMain.Col = ColConsQty
                sprdMain.Text = mConsQty

                sprdMain.Col = ColRemarks
                mRemarks = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                sprdMain.Text = mRemarks

                .MoveNext()

                I = I + 1
                sprdMain.MaxRows = I
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
        MainClass.ButtonStatus(Me, XRIGHT, RsConsumptionHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub Clear1()


        LblMkey.Text = ""

        txtConsDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtNumber.Text = ""
        txtDept.Text = ""
        txtEmp.Text = ""
        txtCost.Text = ""
        TxtRemarks.Text = ""
        cboShiftcd.SelectedIndex = 0
        lblCostctr.Text = ""
        lblDeptName.Text = ""
        lblEmpName.Text = ""
        txtEntryDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime()
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            txtConsDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
        Else
            txtConsDate.Enabled = True
        End If

        txtScrapItemCode.Text = ""
        lblScrapItemCode.Text = ""
        lblScrapItemUOM.Text = ""
        txtScrapItemWeight.Text = ""

        txtScrapItemWeight.Enabled = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101, True, False)


        txtDept.Enabled = True
        txtEmp.Enabled = True
        txtCost.Enabled = True
        cmdSearchEmp.Enabled = True
        cmdSearchDept.Enabled = True
        cmdSearchCC.Enabled = True
        cmdSearchScrapItemCode.Enabled = True
        txtScrapItemCode.Enabled = True

        cboShiftcd.Enabled = True

        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        MainClass.ClearGrid(sprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsConsumptionHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmConsumption_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Public Sub frmConsumption_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        xMyMenu = myMenu

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        AdoDCMain.Visible = False
        FillCboFormType()
        txtNumber.Enabled = True
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
        mCol = sprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Or eventArgs.KeyCode = System.Windows.Forms.Keys.Tab Then
            If mCol = ColConsQty Then
                sprdMain.Row = sprdMain.ActiveRow
                sprdMain.Col = ColConsQty
                If Val(sprdMain.Text) <> 0 Then
                    If sprdMain.MaxRows = sprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(sprdMain, ColItemCode, ConRowHeight)
                        '                    FormatSprdMain SprdMain.MaxRows	
                    End If
                End If
                '            SprdMain.Row = SprdMain.MaxRows	
            End If
        End If
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
        sprdMain.Refresh()
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        'With sprdMain
        '    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With

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
            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf _
            & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

        '    If MainClass.SearchGridMaster(txtCost.Text, "FIN_CCENTER_HDR", "CC_CODE", "CC_DESC", , , SqlStr) = True Then
        If MainClass.SearchGridMasterBySQL2(txtCost.Text, SqlStr) = True Then
            txtCost.Text = AcName
            lblCostctr.text = AcName1
            'txtCost_Validate(False)
            txtCost_Validating(txtCost, New System.ComponentModel.CancelEventArgs(False))
            If txtCost.Enabled = True Then txtCost.Focus()
        End If
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

        If Trim(txtDept.Text) = "" Then
            MsgInformation("Please Select Dept. First.")
            txtDept.Focus()
            GoTo EventExitSub
        End If

        SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf _
            & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf _
            & " AND IH.CC_CODE='" & MainClass.AllowSingleQuote(txtCost.Text) & "'" & vbCrLf _
            & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            lblCostctr.Text = IIf(IsDbNull(RsTemp.Fields("CC_DESC").Value), "", RsTemp.Fields("CC_DESC").Value)
        Else
            MsgInformation("Invalid Cost Center Code for Department : " & Trim(txtDept.Text))
            Cancel = True
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtConsDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtConsDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtConsDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtConsDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mItemUOM As String
        Dim mStkType As String
        'Dim xAutoIssue As Boolean

        Dim mDivisionCode As Double

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        If Trim(txtConsDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtConsDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If

        If FYChk((txtConsDate.Text)) = False Then
            If txtConsDate.Enabled = True Then txtConsDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(txtDept.Text) = "" Then GoTo EventExitSub

        'xAutoIssue = CheckAutoIssue((txtConsDate.Text), "")

        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColUom
                mItemUOM = Trim(.Text)

                .Col = ColStockType
                mStkType = Trim(.Text)

                If mItemCode <> "" Then
                    .Col = ColStockQty
                    .Text = CStr(GetBalanceStockQty(mItemCode, (txtConsDate.Text), mItemUOM, Trim(txtDept.Text), mStkType, "", ConPH, mDivisionCode, ConStockRefType_CON, Val(txtNumber.Text)))
                    ''SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtConsDate.Text), xItemUOM, (txtDept.Text), xStockType, "", mWareHouse, mDivisionCode, ConStockRefType_CON, Val(txtNumber.Text)))
                End If
            Next
        End With

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            lblDeptname.text = AcName
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtDept_DoubleClick(txtDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mItemUOM As String
        Dim mStkType As String
        'Dim xAutoIssue As Boolean
        Dim mDivisionCode As Double

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        If Trim(txtDept.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            lblDeptname.text = MasterNo
        Else
            MsgInformation("Invalid Depatment Code")
            Cancel = True
            Exit Sub
        End If

        If Trim(txtConsDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtConsDate.Text) Then
            MsgInformation("Invalid Date")
            If txtConsDate.Enabled = True Then txtConsDate.Focus()
            GoTo EventExitSub
        End If

        If FYChk((txtConsDate.Text)) = False Then
            If txtConsDate.Enabled = True Then txtConsDate.Focus()
            GoTo EventExitSub
        End If

        'xAutoIssue = CheckAutoIssue((txtConsDate.Text), "")

        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColUom
                mItemUOM = Trim(.Text)

                .Col = ColStockType
                mStkType = Trim(.Text)

                If mItemCode <> "" Then

                    .Col = ColStockQty
                    .Text = CStr(GetBalanceStockQty(mItemCode, (txtConsDate.Text), mItemUOM, Trim(txtDept.Text), mStkType, "", ConPH, mDivisionCode, ConStockRefType_CON, Val(txtNumber.Text)))
                End If
            Next
        End With

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
            lblEmpname.text = AcName
            'txtEmp_Validate(False)
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

        If Len(Trim(txtEmp.Text)) < 6 Then
            txtEmp.Text = VB6.Format(txtEmp.Text, "000000")
        End If

        If MainClass.ValidateWithMasterTable(txtEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            lblEmpname.text = MasterNo
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

    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNumber_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.DoubleClick
        Call cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtNumber_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNumber.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNumber.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtNumber_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtNumber.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Public Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mReqnum As String

        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub

        If Len(txtNumber.Text) < 6 Then
            txtNumber.Text = Val(txtNumber.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsConsumptionHdr.EOF = False Then mReqnum = RsConsumptionHdr.Fields("AUTO_KEY_CONS").Value

        SqlStr = "Select * From PRD_CONSUMPTION_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(AUTO_KEY_CONS))=" & Val(txtNumber.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsConsumptionHdr, ADODB.LockTypeEnum.adLockReadOnly)

        If RsConsumptionHdr.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Issue Note, Use Generate Issue Note Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From PRD_CONSUMPTION_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " LTRIM(RTRIM(AUTO_KEY_CONS))=" & Val(mReqnum) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsConsumptionHdr, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = " SELECT GEN_DESC, GEN_CODE " & vbCrLf _
            & " FROM INV_GENERAL_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND GEN_TYPE='C'"

        '    If MainClass.SearchGridMaster(txtCategory.Text, "FIN_CCENTER_HDR", "CC_CODE", "CC_DESC", , , SqlStr) = True Then
        If MainClass.SearchGridMasterBySQL2(txtCategory.Text, SqlStr) = True Then
            txtCategory.Text = AcName
            'txtCategory_Validate(False)
            txtCategory_Validating(txtCategory, New System.ComponentModel.CancelEventArgs(False))
            If txtCategory.Enabled = True Then txtCategory.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtCategory_DoubleClick(txtCategory, New System.EventArgs())
    End Sub

    Private Sub txtCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If Trim(txtCategory.Text) = "" Then GoTo EventExitSub

        'SqlStr = " SELECT GEN_DESC, GEN_CODE " & vbCrLf _
        '    & " FROM INV_GENERAL_MST " & vbCrLf _
        '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND GEN_TYPE='C'"

        SqlStr = " SELECT GEN_DESC " & vbCrLf _
            & " FROM INV_GENERAL_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND GEN_DESC='" & MainClass.AllowSingleQuote(txtCategory.Text) & "' AND GEN_TYPE='C'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            txtCategory.Text = IIf(IsDBNull(RsTemp.Fields("GEN_DESC").Value), "", RsTemp.Fields("GEN_DESC").Value)
        Else
            MsgInformation("Invalid category : ")
            Cancel = True
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim CntRow As Integer
        Dim pDeptCode As String
        Dim mTableName As String
        'Dim xAutoIssue As Boolean
        Dim mItemCode As String
        Dim mDivisionCode As Double

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If


        MainClass.ClearGrid(SprdMain)

        If Trim(txtDept.Text) = "" Then
            MsgInformation("Please Enter Dept Code.")
            Exit Sub
        End If

        mTableName = ConInventoryTable

        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then
        '        mTableName = "INV_STOCK_REC_TRN" & RsCompany.fields("FYEAR").value
        '    ElseIf RsCompany.fields("COMPANY_CODE").value = 3 Or RsCompany.fields("COMPANY_CODE").value = 10 Or RsCompany.fields("COMPANY_CODE").value = 12 Then
        '        mTableName = "INV_STOCK_REC_TRN" & VB6.Format(RsCompany.fields("COMPANY_CODE").value, "00") & RsCompany.fields("FYEAR").value
        '    Else
        '        mTableName = "INV_STOCK_REC_TRN"
        '    End If

        pDeptCode = Trim(txtDept.Text)

        If Trim(txtConsDate.Text) = "" Then
            MsgInformation("Please Enter Date.")
            Exit Sub
        End If

        SqlStr = " SELECT STOCK.ITEM_CODE, ITEM.ITEM_SHORT_DESC, STOCK.ITEM_UOM, " & vbCrLf _
            & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) AS STOCKQTY, " & vbCrLf _
            & " ITEM_WEIGHT "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " STOCK, " & vbCrLf _
            & " INV_ITEM_MST ITEM "

        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf _
            & " Where " & vbCrLf _
            & " STOCK.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND STOCK.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND STOCK.COMPANY_CODE=ITEM.COMPANY_CODE " & vbCrLf _
            & " AND STOCK.ITEM_CODE=ITEM.ITEM_CODE " & vbCrLf _
            & " AND STOCK.STOCK_ID='" & ConPH & "'" & vbCrLf _
            & " AND DEPT_CODE_FROM='" & pDeptCode & "' AND STOCK.DIV_CODE=" & mDivisionCode & "" & vbCrLf _
            & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtConsDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        Dim mCategoryCode As String = ""

        If Trim(txtCategory.Text) <> "" Then

            If MainClass.ValidateWithMasterTable(Trim(txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCategoryCode = Trim(MasterNo)
            End If

            SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE='" & mCategoryCode & "'"

        End If

        SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_TYPE='ST'"


        SqlStr = SqlStr & vbCrLf & " AND STOCK.STATUS='O'"

        SqlStr = SqlStr & vbCrLf & "GROUP BY STOCK.ITEM_CODE, ITEM.ITEM_SHORT_DESC, STOCK.ITEM_UOM, ITEM_WEIGHT"

        SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>0"

        SqlStr = SqlStr & vbCrLf & "ORDER BY STOCK.ITEM_CODE, ITEM.ITEM_SHORT_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        CntRow = 1
        With SprdMain
            If RsTemp.EOF = False Then
                Do While Not RsTemp.EOF

                    mItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                    .Row = CntRow
                    .Col = ColItemCode
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                    .Col = ColItemDesc
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))

                    .Col = ColUom
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value))

                    .Col = ColStockType
                    .Text = "ST"

                    .Col = ColStockQty
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("STOCKQTY").Value), 0, RsTemp.Fields("STOCKQTY").Value), "0.000")

                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                        .Col = ColConsQty
                        .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("STOCKQTY").Value), 0, RsTemp.Fields("STOCKQTY").Value), "0.000")
                    Else
                        .Col = ColConsQty
                        .Text = 0
                    End If

                    .Col = ColRemarks
                    .Text = ""

                    CntRow = CntRow + 1
                    .MaxRows = CntRow


                    RsTemp.MoveNext()
                Loop
            End If
        End With

        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtScrapItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtScrapItemCode.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtScrapItemCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtScrapItemCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtScrapItemCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtScrapItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtScrapItemCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchScrapItemCode_Click(cmdSearchScrapItemCode, New System.EventArgs())
    End Sub

    Private Sub txtScrapItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtScrapItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtScrapItemCode.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtScrapItemCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblScrapItemCode.Text = MasterNo
        Else
            MsgInformation("Invalid Product Code")
            Cancel = True
        End If

        If MainClass.ValidateWithMasterTable(txtScrapItemCode.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblScrapItemUOM.Text = MasterNo
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtScrapItemWeight_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtScrapItemWeight.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtScrapItemWeight_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtScrapItemWeight.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub cmdSearchScrapItemCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchScrapItemCode.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtScrapItemCode.Text = AcName1
            lblScrapItemCode.Text = AcName
            If txtScrapItemCode.Enabled = True Then txtScrapItemCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

End Class
