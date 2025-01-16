Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmPMemoBreakup
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
    Private Const ColStockQty As Short = 5
    Private Const ColProdQty As Short = 6
    Private Const ColReason As Short = 7

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

    Private Sub chkProduction_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkProduction.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkSPD_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSPD.CheckStateChanged

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
                If InsertIntoDelAudit(PubDBCn, "PRD_BREAKUP_HDR ", (txtPMemoNo.Text), RsPMemoMain, "AUTO_KEY_REF") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_BREAKUP_HDR", "AUTO_KEY_REF", (lblMKey.Text)) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_PBREAKUP, (txtPMemoNo.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM PRD_BREAKUP_DET WHERE AUTO_KEY_REF=" & Val(lblMKey.Text) & "")
                PubDBCn.Execute("DELETE FROM PRD_BREAKUP_HDR  WHERE AUTO_KEY_REF=" & Val(lblMKey.Text) & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'")
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'"

        If MainClass.SearchGridMaster(txtPMemoNo.Text, "PRD_BREAKUP_HDR ", "AUTO_KEY_REF", "REF_DATE", , , SqlStr) = True Then
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
            lblDept.text = AcName
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
            lblEmp.text = AcName
            If txtEmp.Enabled = True Then txtEmp.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub



    Private Sub FrmPMemoBreakup_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "", "", "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
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
                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "", "", "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
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
                If MainClass.SearchGridMaster(.Text, "INV_TYPE_MST", "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColStockType
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColStockType, .ActiveRow, ColStockType, .ActiveRow, False))
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
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        FormatSprdMain((SprdMain.MaxRows))
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColProdQty
                If CheckQty() = True Then
                    '                MainClass.SetFocusToCell SprdMain, .ActiveRow, ColProdQty	
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

        FillItemDescPart = False
        If Trim(pItemCode) = "" Then Exit Function

        SqlStr = "SELECT ITEM_CODE, ITEM_SHORT_DESC,ISSUE_UOM,CATEGORY_CODE " & vbCrLf _
            & " FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
            & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mCategoryCode = Trim(IIf(IsDbNull(RsTemp.Fields("CATEGORY_CODE").Value), "", RsTemp.Fields("CATEGORY_CODE").Value))

            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                .Text = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                mProdItemCode = Trim(.Text)

                .Col = ColItemDesc
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColUom
                .Text = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                mItemUOM = Trim(.Text)

                .Col = ColStockType
                .Text = IIf(Trim(.Text) = "", IIf(VB.Left(cboType.Text, 1) = "P", "ST", "CS"), Trim(.Text))

                .Col = ColStockQty
                .Text = CStr(GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtDept.Text), "ST", "", ConPH, mDivisionCode, ConStockRefType_PBREAKUP, Val(txtPMemoNo.Text)))

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
            .Row = eventArgs.Row
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
        SqlStr = "SELECT Max(AUTO_KEY_REF)  " & vbCrLf & " FROM PRD_BREAKUP_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

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
        Dim mIsSPD As String
        Dim pErrorDesc As String
        'Dim RsTemp As ADODB.Recordset = Nothing	
        Dim mIsProduction As String
        Dim mDivisionCode As Double


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime
        mIsSPD = IIf(chkSPD.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsProduction = IIf(chkProduction.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        SqlStr = ""
        mPMemoNo = Val(txtPMemoNo.Text)
        If Val(txtPMemoNo.Text) = 0 Then
            mPMemoNo = CDbl(AutoGenMemoNo())
        End If
        txtPMemoNo.Text = CStr(mPMemoNo)
        If ADDMode = True Then
            lblMKey.Text = CStr(mPMemoNo)
            SqlStr = " INSERT INTO PRD_BREAKUP_HDR  " & vbCrLf _
                & " (COMPANY_CODE,FYEAR,AUTO_KEY_REF," & vbCrLf _
                & " REF_DATE, PREP_TIME, PROD_DATE, DEPT_CODE, SHIFT_CODE,PROD_TYPE," & vbCrLf _
                & " EMP_CODE, REMARKS, IS_SPD, BOOKTYPE,  " & vbCrLf _
                & " ADDUSER,ADDDATE,MODUSER,MODDATE, IS_PRODUCTION, DIV_CODE) " & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " " & RsCompany.fields("COMPANY_CODE").value & "," & RsCompany.fields("FYEAR").value & "," & mPMemoNo & ", " & vbCrLf _
                & " '" & vb6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "',TO_DATE('" & txtRefTM.Text & "','HH24:MI'), " & vbCrLf _
                & " '" & vb6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                & " '" & cboShiftcd.Text & "', " & vbCrLf _
                & " '" & vb.Left(cboType.Text, 1) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtEmp.Text) & "','" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " '" & mIsSPD & "', '" & vb.Left(lblBookType.text, 1) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'','','" & mIsProduction & "'," & mDivisionCode & ")"


        ElseIf MODIFYMode = True Then
            mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime
            SqlStr = " UPDATE PRD_BREAKUP_HDR  SET " & vbCrLf _
                & " AUTO_KEY_REF=" & mPMemoNo & ", " & vbCrLf _
                & " REF_DATE='" & vb6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "', " & vbCrLf _
                & " PROD_DATE='" & vb6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "', " & vbCrLf _
                & " PREP_TIME=TO_DATE('" & txtRefTM.Text & "','HH24:MI')," & vbCrLf _
                & " DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                & " SHIFT_CODE='" & cboShiftcd.Text & "', " & vbCrLf _
                & " PROD_TYPE= '" & vb.Left(cboType.Text, 1) & "'," & vbCrLf _
                & " EMP_CODE='" & MainClass.AllowSingleQuote(txtEmp.Text) & "', " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " IS_SPD='" & mIsSPD & "',IS_PRODUCTION='" & mIsProduction & "'," & vbCrLf _
                & " BOOKTYPE='" & vb.Left(lblBookType.text, 1) & "'," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'), DIV_CODE=" & mDivisionCode & "" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND AUTO_KEY_REF=" & Val(lblMKey.text) & ""
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
        Dim xItemCost As Double
        Dim mInCCCode As String
        Dim mWIPStock As Double
        Dim mWIPReworkStock As Double
        Dim mSqlStr As String
        Dim RsBOM As ADODB.Recordset
        Dim mProductSeqNo As Integer
        Dim mProductionDate As String
        'Dim mEntryDate As String	
        Dim mReason As String
        Dim mToolNo As String
        Dim mTotalOpr As Integer
        Dim mDeptSeq As Integer
        Dim xOPStockType As String
        Dim pOPRCode As String
        Dim pOPRDesc As String
        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mOperatorCode As String

        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "CCCODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
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

        SqlStr = " DELETE FROM PRD_BREAKUP_DET " & vbCrLf & " WHERE AUTO_KEY_REF=" & Val(lblMKey.Text) & " "
        PubDBCn.Execute(SqlStr)

        If DeleteStockTRN(PubDBCn, ConStockRefType_PBREAKUP, (txtPMemoNo.Text)) = False Then GoTo UpdateDetail1Err
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

                .Col = ColProdQty
                mProdQty = Val(.Text)

                .Col = ColStockType
                mStockType = IIf(VB.Left(cboType.Text, 1) = "P", "ST", "CS") ''MainClass.AllowSingleQuote(.Text)	

                .Col = ColReason
                mReason = Trim(.Text)

                If mItemCode <> "" And mProdQty > 0 Then
                    SqlStr = " INSERT INTO PRD_BREAKUP_DET ( " & vbCrLf & " COMPANY_CODE,AUTO_KEY_REF,SERIAL_NO,ITEM_CODE,ITEM_DESC, " & vbCrLf & " ITEM_UOM,STOCK_TYPE, PROD_QTY, REASON) " & vbCrLf & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMKey.Text) & ", " & i & "," & vbCrLf & " '" & mItemCode & "','" & mItemDesc & "', " & vbCrLf & " '" & mUOM & "','" & mStockType & "', " & vbCrLf & " " & mProdQty & ", '" & MainClass.AllowSingleQuote(mReason) & "')"

                    PubDBCn.Execute(SqlStr)

                    If chkProduction.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        If UpdateStockTRN(PubDBCn, ConStockRefType_PBREAKUP, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), mStockType, mItemCode, mUOM, CStr(-1), mProdQty, 0, "O", xItemCost, xItemCost, "-1", "", (txtDept.Text), (txtDept.Text), mInCCCode, "N", "From : " & lblDept.Text & "  : (Production Break-up) -" & ConStockRefType_PBREAKUP, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
                    Else
                        If UpdateStockTRN(PubDBCn, ConStockRefType_PBREAKUP, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), mStockType, mItemCode, mUOM, CStr(-1), mProdQty, 0, "I", xItemCost, xItemCost, "-1", "", (txtDept.Text), (txtDept.Text), mInCCCode, "N", "From : " & lblDept.Text & "  : (Production after Physical) -" & ConStockRefType_PBREAKUP, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
                    End If

                    mDeptSeq = GetDeptSeq(mItemCode, Trim(txtDept.Text))

                    mSqlStr = MakeBOMStockQty(mItemCode, (txtDept.Text), mDeptSeq)

                    If mSqlStr = "" Then
                        pErrorDesc = "Cann't Saved"
                        UpdateDetail1 = False
                        Exit Function
                    Else
                        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOM, ADODB.LockTypeEnum.adLockReadOnly)
                    End If
                    If RsBOM.EOF = False Then
                        If UpdateBOMStock(pErrorDesc, RsBOM, mItemCode, mProdQty, xStockRowNo, xStockRowNo, xItemCost, mInCCCode, mInCCCode, mDivisionCode) = False Then GoTo UpdateDetail1Err
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
    Private Function UpdateBOMStock(ByRef pErrorDesc As String, ByRef pRsBOM As ADODB.Recordset, ByRef mFICode As String, ByRef mFQty As Double, ByRef mStockRowNo As Integer, ByRef mRetStockRowNo As Integer, ByRef mRetItemCost As Double, ByRef pInCCCode As String, ByRef pOutCCCode As String, ByRef mDivisionCode As Double) As Boolean
        On Error GoTo BOMStockErr
        Dim SqlStr As String = ""
        Dim mStdQty As Double
        Dim mRMGrossQtyGram As Double
        Dim mRMGrossQtyKg As Double
        Dim mRMCostKg As Double
        Dim mScrpGrossQtyGram As Double
        Dim mScrpGrossQtyKg As Double
        Dim mScrpCostKg As Double
        Dim mSUOM As String
        Dim mProductionQty As Double
        Dim xProductionQty As Double
        Dim mStockQty As Double
        Dim mTotStockQty As Double
        Dim mScrapCode As String
        Dim mRMCode As String
        Dim mRMCodeStr As String
        Dim mRMUOM As String

        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMKEY As String
        Dim mBalFQty As Double
        Dim mUsedRMQty As Double
        Dim mUsedSFQty As Double
        Dim mStockType As String
        Dim mFromScrap As String
        Dim mUsedScrap As Double
        Dim xWareHouse As String
        Dim mISProd As Boolean


        With pRsBOM
            Do While Not .EOF
                mRMCode = Trim(IIf(IsDbNull(.Fields("RM_CODE").Value), "", .Fields("RM_CODE").Value))
                If IsFGItem(mRMCode) = True Then
                    xWareHouse = ConPH
                Else
                    If CheckAutoIssue(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"), mRMCode) = False Then '' RsCompany!AUTO_ISSUE = "N"	
                        xWareHouse = "PH"
                    Else
                        mISProd = IsProductionItem(mRMCode)
                        If mISProd = True Then
                            If CDate(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")) < CDate(VB6.Format(RsCompany.Fields("AUTO_ISSUE_DATE").Value, "DD/MM/YYYY")) Then
                                xWareHouse = "PH"
                            Else
                                xWareHouse = "WH"
                            End If
                        Else
                            xWareHouse = "PH"
                        End If
                    End If
                End If

                mMKEY = IIf(IsDbNull(.Fields("mKey").Value), "", .Fields("mKey").Value)
                mFromScrap = IIf(IsDbNull(.Fields("FROM_SCRAP").Value), "N", .Fields("FROM_SCRAP").Value)
                mStockType = IIf(IsDbNull(.Fields("STOCK_TYPE").Value), "ST", .Fields("STOCK_TYPE").Value)

                mStdQty = Val(IIf(IsDbNull(.Fields("STD_QTY").Value), "", .Fields("STD_QTY").Value)) + Val(IIf(IsDbNull(.Fields("GROSS_WT_SCRAP").Value), "", .Fields("GROSS_WT_SCRAP").Value))
                mRMCodeStr = mRMCode
                mRMUOM = IIf(IsDbNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)
                mScrapCode = IIf(IsDbNull(.Fields("SCRAP_ITEM_CODE").Value), "", .Fields("SCRAP_ITEM_CODE").Value)

                If UCase(Trim(mRMUOM)) = "TON" Then
                    mProductionQty = Val(CStr((mStdQty * mFQty) / 1000))
                    mProductionQty = Val(CStr(mProductionQty / 1000))
                ElseIf UCase(Trim(mRMUOM)) = "KGS" Then
                    mProductionQty = Val(CStr(mStdQty / 1000)) * mFQty
                Else
                    mProductionQty = mStdQty * mFQty
                End If


                mScrpGrossQtyGram = Val(IIf(IsDbNull(.Fields("GROSS_WT_SCRAP").Value), "", .Fields("GROSS_WT_SCRAP").Value))
                mScrpGrossQtyKg = Val(CStr(mScrpGrossQtyGram / 1000))
                mScrpGrossQtyKg = Val(CStr(mScrpGrossQtyKg * mFQty))

                If mProductionQty > 0 Then
                    mStockRowNo = mStockRowNo + 1
                    If chkProduction.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        If UpdateStockTRN(PubDBCn, ConStockRefType_PBREAKUP, (txtPMemoNo.Text), mStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), mStockType, mRMCode, mRMUOM, CStr(-1), mProductionQty + mScrpGrossQtyKg, 0, "I", mRMCostKg, mRMCostKg, "", "", (txtDept.Text), (txtDept.Text), pOutCCCode, "N", "TO : " & lblDept.Text & " (Production Break-up) -" & ConStockRefType_PBREAKUP & "-" & mFICode, "-1", xWareHouse, mDivisionCode, "", mFICode) = False Then GoTo BOMStockErr
                    Else
                        If UpdateStockTRN(PubDBCn, ConStockRefType_PBREAKUP, (txtPMemoNo.Text), mStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), mStockType, mRMCode, mRMUOM, CStr(-1), mProductionQty + mScrpGrossQtyKg, 0, "O", mRMCostKg, mRMCostKg, "", "", (txtDept.Text), (txtDept.Text), pOutCCCode, "N", "TO : " & lblDept.Text & " (Production after Physical) -" & ConStockRefType_PBREAKUP & "-" & mFICode, "-1", xWareHouse, mDivisionCode, "", mFICode) = False Then GoTo BOMStockErr

                    End If
                End If

                If mScrpGrossQtyKg > 0 Then
                    mStockRowNo = mStockRowNo + 1
                    If chkProduction.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        If UpdateStockTRN(PubDBCn, ConStockRefType_PBREAKUP, (txtPMemoNo.Text), mStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "SC", mRMCode, mRMUOM, CStr(-1), mScrpGrossQtyKg, 0, "O", mRMCostKg, mRMCostKg, "", "", (txtDept.Text), (txtDept.Text), pOutCCCode, "N", "TO : " & lblDept.Text & " (Production Break-up) -" & ConStockRefType_PBREAKUP & "-" & mFICode, "-1", xWareHouse, mDivisionCode, "", mFICode) = False Then GoTo BOMStockErr
                    Else
                        If UpdateStockTRN(PubDBCn, ConStockRefType_PBREAKUP, (txtPMemoNo.Text), mStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "SC", mRMCode, mRMUOM, CStr(-1), mScrpGrossQtyKg, 0, "I", mRMCostKg, mRMCostKg, "", "", (txtDept.Text), (txtDept.Text), pOutCCCode, "N", "TO : " & lblDept.Text & " (Production after Physical) -" & ConStockRefType_PBREAKUP & "-" & mFICode, "-1", xWareHouse, mDivisionCode, "", mFICode) = False Then GoTo BOMStockErr
                    End If
                End If

                pRsBOM.MoveNext()
            Loop
        End With
        mRetStockRowNo = mStockRowNo
        UpdateBOMStock = True
        Exit Function
BOMStockErr:
        UpdateBOMStock = False
        If Err.Description <> "" Then MsgBox(Err.Description)
        '    Resume	
    End Function

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

        If MainClass.ValidateWithMasterTable(mFICode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
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
            & " AND ID.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(UCase(mDeptCode)) & "' " & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.ALTER_RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND ID.MAINITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mRMCode)) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                mAlterRMCode = Trim(IIf(IsDbNull(RsTemp.Fields("ALTER_RM_CODE").Value), "", RsTemp.Fields("ALTER_RM_CODE").Value))
                mAlterRMUOM = Trim(IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                mAlterStdQty = Val(IIf(IsDbNull(RsTemp.Fields("ALTER_STD_QTY").Value), 0, RsTemp.Fields("ALTER_STD_QTY").Value)) + Val(IIf(IsDbNull(RsTemp.Fields("ALETRSCRAP").Value), 0, RsTemp.Fields("ALETRSCRAP").Value))
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


        SqlStr = "SELECT  IH.MKEY, IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, ID.STD_QTY, ID.DEPT_CODE," & vbCrLf & " ID.GROSS_WT_SCRAP, INVMST.ISSUE_UOM, INVMST.SCRAP_ITEM_CODE, FROM_SCRAP, ID.STOCK_TYPE "

        SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST"

        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(mSFICode)) & "' "

        '    If pDeptSeq = 1 Then	
        '        SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(UCase(mDeptCode)) & "' "	
        '    Else	
        SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE IN ( " & vbCrLf _
                & " SELECT DEPT_CODE FROM PRD_PRODSEQUENCE_DET" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mSFICode) & "'" & vbCrLf _
                & " AND WEF = (" & vbCrLf _
                & " SELECT MAX(WEF) FROM PRD_PRODSEQUENCE_DET WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mSFICode) & "'" & vbCrLf _
                & " AND WEF<='" & vb6.Format(txtPMemoDate, "DD-MMM-YYYY") & "')" & vbCrLf _
                & " AND SERIAL_NO<=" & Val(pDeptSeq) & ")"
        '    End If

        SqlStr = SqlStr & vbCrLf _
                & " AND IH.WEF=( " & vbCrLf _
                & " SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(mSFICode)) & "' " & vbCrLf _
                & " AND WEF<= '" & vb6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "')"

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

        If chkSPD.CheckState = System.Windows.Forms.CheckState.Checked And Trim(txtDept.Text) <> "ASY" Then
            MsgBox("Please Check in SPD", MsgBoxStyle.Information)
            FieldsVarification = False
            chkSPD.Focus()
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

                If mProductCode <> "" Then
                    SqlStr = " SELECT PRODUCT_CODE " & vbCrLf & " FROM PRD_NEWBOM_HDR" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"

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

                .Col = ColStockQty
                mStockQty = Val(.Text)

                If mProdQty > mStockQty And chkProduction.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    MsgInformation("Product Qty Cann't be Greater Than Stock Qty, Item Code : " & mProductCode & ". Cann't Be Saved")
                    FieldsVarification = False
                    MainClass.SetFocusToCell(SprdMain, cntRow, ColProdQty)
                    Exit Function
                End If
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
        '    If MainClass.ValidDataInGrid(SprdMain, ColprodQty, "N", "Please Check Produce Quantity.") = False Then FieldsVarification = False: Exit Function	
        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Function

    Private Function GetDeptSeq(ByRef mProductCode As String, ByRef pDeptCode As String) As Integer

        On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT SERIAL_NO " & vbCrLf _
            & " FROM PRD_PRODSEQUENCE_DET TRN" & vbCrLf _
            & " WHERE " & vbCrLf _
            & " TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _
            & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf _
            & " AND WEF = ( " & vbCrLf _
            & " SELECT MAX(WEF) AS WEF FROM PRD_PRODSEQUENCE_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _
            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf _
            & " AND WEF<='" & vb6.Format(txtPMemoDate, "DD-MMM-YYYY") & "')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetDeptSeq = IIf(IsDbNull(RsTemp.Fields("SERIAL_NO").Value), 0, RsTemp.Fields("SERIAL_NO").Value)
        Else
            GetDeptSeq = 0
        End If

        If chkSPD.CheckState = System.Windows.Forms.CheckState.Checked Then
            GetDeptSeq = GetDeptSeq - 1
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

        SqlStr = "SELECT Max(REF_DATE) AS  REF_DATE " & vbCrLf _
            & " FROM PRD_BREAKUP_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.fields("FYEAR").value & " " & vbCrLf _
            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _
            & " AND PROD_TYPE='" & vb.Left(cboType.Text, 1) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetLastEntryDate = IIf(IsDbNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value)
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
    Public Sub FrmPMemoBreakup_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Production Break-up"


        SqlStr = ""
        SqlStr = "Select * from PRD_BREAKUP_HDR  Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_BREAKUP_DET Where 1<>1"
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
        SqlStr = " SELECT  AUTO_KEY_REF MEMO_NO, TO_CHAR(REF_DATE,'DD/MM/YYYY') MEMO_DATE, " & vbCrLf & " DEPT_CODE FROM_DEPT,SHIFT_CODE,DECODE(PROD_TYPE,'P','Production','Jobwork') AS Prod_Type,REMARKS " & vbCrLf & " FROM PRD_BREAKUP_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "

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
            .set_ColWidth(.Col, 6)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsPMemoDetail.Fields("ITEM_DESC").DefinedSize
            .set_ColWidth(.Col, 32)

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsPMemoDetail.Fields("ITEM_UOM").DefinedSize
            .set_ColWidth(.Col, 6)

            .Col = ColProdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)

            .Col = ColStockQty
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

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUom)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockType, ColStockQty)
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
            txtDept.Maxlength = .Fields("DEPT_CODE").DefinedSize
            txtEmp.Maxlength = .Fields("EMP_CODE").DefinedSize
            txtRemarks.Maxlength = .Fields("REMARKS").DefinedSize
            txtProdDate.Maxlength = 10
            txtRefTM.Maxlength = 5
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
                lblMKey.Text = .Fields("AUTO_KEY_REF").Value
                txtPMemoNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_REF").Value), "", .Fields("AUTO_KEY_REF").Value)
                txtPMemoDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")
                txtProdDate.Text = VB6.Format(IIf(IsDbNull(.Fields("PROD_DATE").Value), "", .Fields("PROD_DATE").Value), "DD/MM/YYYY")

                txtRefTM.Text = VB6.Format(IIf(IsDbNull(.Fields("PREP_TIME").Value), "", .Fields("PREP_TIME").Value), "HH:MM")

                '            txtEntryDate.Text = Format(IIf(IsNull(!ADDDATE), "", !ADDDATE), "DD/MM/YYYY HH:MM")	
                mEntryDate = VB6.Format(IIf(IsDbNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY HH:MM")
                mEntryDate = mEntryDate & vbCrLf & VB6.Format(IIf(IsDbNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY HH:MM")
                txtEntryDate.Text = mEntryDate

                txtDept.Text = IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)
                txtDept_Validating(txtDept, New System.ComponentModel.CancelEventArgs(False))
                cboShiftcd.Text = IIf(IsDbNull(.Fields("SHIFT_CODE").Value), "", .Fields("SHIFT_CODE").Value)

                mProdType = IIf(IsDbNull(.Fields("PROD_TYPE").Value), "P", .Fields("PROD_TYPE").Value)
                If mProdType = "P" Then
                    cboType.SelectedIndex = 0
                Else
                    cboType.SelectedIndex = 1
                End If

                chkSPD.CheckState = IIf(.Fields("IS_SPD").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkProduction.CheckState = IIf(.Fields("IS_PRODUCTION").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                txtEmp.Text = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtEmp_Validating(txtEmp, New System.ComponentModel.CancelEventArgs(False))
                txtRemarks.Text = IIf(IsDbNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)

                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If
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


        SqlStr = " SELECT * " & vbCrLf & " FROM PRD_BREAKUP_DET  " & vbCrLf & " WHERE AUTO_KEY_REF = " & Val(lblMKey.Text) & " " & vbCrLf & " ORDER BY  SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPMemoDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            i = 1
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = i

                SprdMain.Col = ColItemCode
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                mProdItemCode = Trim(SprdMain.Text)

                SprdMain.Col = ColItemDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value))

                SprdMain.Col = ColUom
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                mItemUOM = Trim(SprdMain.Text)

                SprdMain.Col = ColProdQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("PROD_QTY").Value), "", .Fields("PROD_QTY").Value)))

                SprdMain.Col = ColStockType
                SprdMain.Text = IIf(IsDbNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)

                SprdMain.Col = ColStockQty
                SprdMain.Text = CStr(GetBalanceStockQty(mProdItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtDept.Text), "ST", "", ConPH, mDivisionCode, ConStockRefType_PBREAKUP, Val(txtPMemoNo.Text)))

                SprdMain.Col = ColReason
                SprdMain.Text = IIf(IsDbNull(.Fields("REASON").Value), "", .Fields("REASON").Value)

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
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        lblMKey.Text = ""
        txtPMemoNo.Text = ""
        txtRefTM.Text = GetServerTime

        '    If CDate(txtRefTM.Text) < CDate("09:00") Then	
        '        txtPMemoDate.Text = Format(RunDate - 1, "DD/MM/YYYY")	
        '    Else	
        txtPMemoDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")
        txtProdDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")
        '    End If	

        txtDept.Text = ""
        lblDept.Text = ""
        cboShiftcd.SelectedIndex = 0
        cboType.SelectedIndex = 0
        txtEmp.Text = ""
        lblEmp.Text = ""
        txtRemarks.Text = ""

        cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        chkSPD.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkProduction.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSPD.Visible = IIf(RsCompany.Fields("COMPANY_CODE").Value = 1, True, False)

        txtEntryDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtPMemoDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        txtDept.Enabled = mMode
        CmdSearchDept.Enabled = mMode

    End Sub
    Private Sub FrmPMemoBreakup_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmPMemoBreakup_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Public Sub FrmPMemoBreakup_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Width = VB6.TwipsToPixelsX(10935)
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

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))


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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            lblDept.text = MasterNo
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
            lblEmp.text = MasterNo
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

        SqlStr = "Select * From PRD_BREAKUP_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_REF=" & Val(txtPMemoNo.Text) & "" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "

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
                SqlStr = "Select * From PRD_BREAKUP_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_REF=" & Val(CStr(mPMemoNo)) & "" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "

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
End Class
