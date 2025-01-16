Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmDeptAllocationMaster
    Inherits System.Windows.Forms.Form
    Dim RsDeptAllocationHdr As ADODB.Recordset
    Dim RsDeptAllocationDet As ADODB.Recordset

    Dim xMyMenu As String

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColDeptCode As Short = 1
    Private Const ColDeptName As Short = 2
    Private Const ColWeld As Short = 3
    Private Const ColPPS As Short = 4
    Private Const ColNPC As Short = 5
    Private Const ColPRS As Short = 6
    Private Const ColASY As Short = 7
    Private Const ColPLT As Short = 8
    Private Const ColZNC As Short = 9
    Private Const ColFRM As Short = 10
    Private Const ColHDL As Short = 11
    Private Const ColCCD As Short = 12
    Private Const ColNPD As Short = 13
    Dim mAmendStatus As Boolean

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""

        SqlStr = " SELECT IH.WEF " & vbCrLf & " FROM PAY_DEPT_ALLOCATION_HDR IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 10)

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mOldAmendNo As Integer
        Dim mLastestWEF As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim CntCol As Integer
        Dim CntRow As Integer
        Dim mPerCent As Double
        Dim mTotPercent As Double

        FieldsVarification = True
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsDeptAllocationHdr.EOF = True Then Exit Function

        If MODIFYMode = True And chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed Master Cann't be Modified")
            FieldsVarification = False
            Exit Function
        End If

        '    If PubPayCorpUser = "N" Then
        '        MsgInformation "You have not Rights to change Sanction Manpower Master."
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        If Trim(txtWEF.Text) = "" Then
            MsgBox("W.E.F is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtWEF.Enabled = True Then txtWEF.Focus()
            Exit Function
        End If

        With sprdMain
            For CntRow = 1 To .MaxRows

                mPerCent = 0
                mTotPercent = 0
                For CntCol = ColWeld To ColNPD
                    .Row = CntRow
                    .Col = CntCol
                    mPerCent = Val(.Text)
                    mTotPercent = mTotPercent + mPerCent

                    If mTotPercent > 100 Then
                        MsgInformation("Total Percent is cann't be Greater than 100%")
                        FieldsVarification = False
                        Exit Function
                    End If
                Next
            Next
        End With

        If Val(txtAmendNo.Text) > 0 Then
            mOldAmendNo = Val(txtAmendNo.Text) - 1
            mSqlStr = " SELECT WEF FROM PAY_DEPT_ALLOCATION_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AMEND_NO=" & Val(CStr(mOldAmendNo)) & ""

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mLastestWEF = IIf(IsDbNull(RsTemp.Fields("WEF").Value), "", RsTemp.Fields("WEF").Value)
                If CDate(txtWEF.Text) <= CDate(mLastestWEF) Then
                    MsgBox("W.E.F Cann't be less than or equal to Last WEF.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    If txtWEF.Enabled = True Then txtWEF.Focus()
                    Exit Function
                End If
            End If
        End If

        If MainClass.ValidDataInGrid(sprdMain, ColDeptCode, "S", "Dept Code Is Blank") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            cmdSearchWEF.Enabled = True
            sprdMain.Enabled = True
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdAmend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAmend.Click

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim I As Integer


        txtAmendNo.Text = CStr(GetMaxAmendNo())
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        mAmendStatus = True
        cmdAmend.Enabled = False
        txtWEF.Enabled = True
        sprdMain.Enabled = True
        ADDMode = True
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsDeptAllocationHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click

        On Error GoTo DelErrPart

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed Master Cann't be Deleted")
            Exit Sub
        End If

        PubDBCn.Cancel()
        PubDBCn.BeginTrans()

        If Not RsDeptAllocationHdr.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then

                If InsertIntoDelAudit(PubDBCn, "PAY_DEPT_ALLOCATION_HDR ", (lblMKey.Text), RsDeptAllocationHdr) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PAY_DEPT_ALLOCATION_HDR ", "MKEY", (lblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM PAY_DEPT_ALLOCATION_DET WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PAY_DEPT_ALLOCATION_HDR WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

                If Val(txtAmendNo.Text) > 0 Then
                    If UpdatePreviousRate(Val(txtAmendNo.Text), "O") = False Then GoTo DelErrPart
                End If

                PubDBCn.CommitTrans()
                RsDeptAllocationHdr.Requery()
                RsDeptAllocationDet.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsDeptAllocationHdr.Requery()
        RsDeptAllocationDet.Requery()
        PubDBCn.Cancel()
        MsgBox(Err.Description)
    End Sub

    Private Function UpdatePreviousRate(ByRef pAmendNo As Integer, ByRef pPreviousStatus As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = " UPDATE PAY_DEPT_ALLOCATION_HDR  SET " & vbCrLf & " STATUS = '" & pPreviousStatus & "', " & vbCrLf & " MODUSER = '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE =TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AMEND_NO = " & pAmendNo - 1 & ""

        PubDBCn.Execute(SqlStr)

        UpdatePreviousRate = True

        Exit Function
ErrPart:
        UpdatePreviousRate = False
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
    End Function

    Private Function GetMaxAmendNo() As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT MAX(AMEND_NO) AS AMEND_NO" & vbCrLf & " FROM PAY_DEPT_ALLOCATION_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDbNull(RsTemp.Fields("AMEND_NO").Value) Then
                GetMaxAmendNo = 0
            Else
                GetMaxAmendNo = Val(RsTemp.Fields("AMEND_NO").Value) + 1
            End If
        Else
            GetMaxAmendNo = 0
        End If

        Exit Function
ErrPart:
        GetMaxAmendNo = Val(txtAmendNo.Text)
    End Function

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsDeptAllocationHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            Call MakeEnableDesableField(False)
            sprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()

        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintOprRate(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintOprRate(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub PrintOprRate(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRsTemp As ADODB.Recordset = Nothing

        '        Report1.Reset
        '        MainClass.ClearCRptFormulas Report1
        '
        '        mTitle = "Manpower Sanctioned & Budget Master"
        '
        '        SqlStr = " SELECT IH.*, ID.*, DEPT.* " & vbCrLf _
        ''                & " FROM PAY_DEPT_ALLOCATION_HDR IH, PAY_DEPT_ALLOCATION_DET ID, PAY_DEPT_MST DEPT " & vbCrLf _
        ''                & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
        ''                & " AND IH.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf _
        ''                & " AND ID.DEPT_CODE=DEPT.DEPT_CODE " & vbCrLf _
        ''                & " AND IH.MKEY='" & MainClass.AllowSingleQuote(lblMKey.Caption) & "' ORDER BY ID.DEPT_CODE"
        '
        '        Report1.ReportFileName = App.path & "\reports\PayManPowerBudget.rpt"
        '
        '        SetCrpt Report1, Mode, 1, mTitle, mSubTitle, True, xMyMenu
        '        Report1.SQLQuery = SqlStr
        '        Report1.WindowShowGroupTree = False
        '
        '        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtWEF_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True And CmdAdd.Visible = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            GoTo ErrorHandler
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Sub cmdSearchWEF_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchWEF.Click
        On Error GoTo SrchERR
        Dim SqlStr As String = ""

        SqlStr = " SELECT IH.WEF" & vbCrLf & " FROM PAY_DEPT_ALLOCATION_HDR IH " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtWEF.Text = VB6.Format(AcName, "DD/MM/YYYY")
            If txtWEF.Enabled = True Then txtWEF.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub frmDeptAllocationMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub

        Me.Text = "Department Wise Allocation Master"

        SqlStr = "Select * from PAY_DEPT_ALLOCATION_HDR  Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDeptAllocationHdr, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from PAY_DEPT_ALLOCATION_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDeptAllocationDet, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SetTextLengths()

        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmDeptAllocationMaster_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmDeptAllocationMaster_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmDeptAllocationMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7590)
        Me.Width = VB6.TwipsToPixelsX(11385)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsDeptAllocationHdr
            txtWEF.Maxlength = .Fields("WEF").DefinedSize - 6
            txtRemarks.Maxlength = .Fields("Remarks").DefinedSize
        End With
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtWEF.Enabled = mMode
        cmdSearchWEF.Enabled = mMode
        txtAmendNo.Enabled = False
    End Sub

    Private Sub frmDeptAllocationMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsDeptAllocationHdr.Close()
        RsDeptAllocationDet.Close()

        RsDeptAllocationHdr = Nothing
        RsDeptAllocationDet = Nothing
    End Sub

    Private Sub Clear1()

        lblMKey.Text = ""
        txtWEF.Text = ""
        lblWEF.Text = ""
        txtRemarks.Text = ""
        txtAmendNo.Text = "0"
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkStatus.Enabled = False
        mAmendStatus = False
        cmdAmend.Enabled = True

        MainClass.ClearGrid(sprdMain)
        FormatSprdMain(-1)
        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsDeptAllocationHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FormatSprdMain(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim CntCol As Integer

        With sprdMain
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2)
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsDeptAllocationDet.Fields("DEPT_CODE").DefinedSize
            .set_ColWidth(.Col, 8)

            .Col = ColDeptName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("DEPT_DESC", "PAY_DEPT_MST", PubDBCn)
            .set_ColWidth(.Col, 35)

            For CntCol = ColWeld To ColNPD
                .Col = CntCol
                .CellType = SS_CELL_TYPE_INTEGER
                .TypeNumberMax = CDbl("99")
                .TypeNumberMin = CDbl("-99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(CntCol, 7)
            Next


            If RsCompany.Fields("COMPANY_CODE").Value = 1 Then
                .Col = ColPLT
                .ColHidden = True

                .Col = ColZNC
                .ColHidden = True

                .Col = ColFRM
                .ColHidden = True

                .Col = ColHDL
                .ColHidden = True

                .Col = ColCCD
                .ColHidden = True
            ElseIf RsCompany.Fields("COMPANY_CODE").Value = 3 Then
                .Col = ColNPC
                .ColHidden = True

                .Col = ColZNC
                .ColHidden = True

                .Col = ColFRM
                .ColHidden = True

                .Col = ColHDL
                .ColHidden = True

                .Col = ColCCD
                .ColHidden = True
            ElseIf RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                .Col = ColPLT
                .ColHidden = True

                .Col = ColPPS
                .ColHidden = True

                .Col = ColZNC
                .ColHidden = True

                .Col = ColFRM
                .ColHidden = True

                .Col = ColHDL
                .ColHidden = True

                .Col = ColCCD
                .ColHidden = True
            ElseIf RsCompany.Fields("COMPANY_CODE").Value = 12 Then
                .Col = ColNPC
                .ColHidden = True

                .Col = ColPPS
                .ColHidden = True

                .Col = ColCCD
                .ColHidden = True
            ElseIf RsCompany.Fields("COMPANY_CODE").Value = 15 Then
                .Col = ColPPS
                .ColHidden = True

                .Col = ColPRS
                .ColHidden = True

                .Col = ColZNC
                .ColHidden = True

                .Col = ColHDL
                .ColHidden = True

                .Col = ColCCD
                .ColHidden = True
            ElseIf RsCompany.Fields("COMPANY_CODE").Value = 25 Then
                .Col = ColPLT
                .ColHidden = True

                .Col = ColPPS
                .ColHidden = True

                .Col = ColZNC
                .ColHidden = True

                .Col = ColFRM
                .ColHidden = True

                .Col = ColHDL
                .ColHidden = True
            End If

            '
            '          Private Const ColWeld = 3
            'Private Const ColPPS = 4
            'Private Const ColNPC = 5
            'Private Const ColPRS = 6
            'Private Const ColASY = 7
            'Private Const ColPLT = 8
            'Private Const ColZNC = 9
            'Private Const ColFRM = 10
            'Private Const ColHDL = 11
            'Private Const ColCCD = 12
            'Private Const ColNPD = 13


        End With
        MainClass.UnProtectCell(sprdMain, 1, sprdMain.MaxRows, 1, sprdMain.MaxCols)

        MainClass.ProtectCell(sprdMain, 1, sprdMain.MaxRows, ColDeptName, ColDeptName)

        MainClass.SetSpreadColor(sprdMain, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsDeptAllocationDet.Requery()
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub Show1()

        On Error GoTo ERR1

        With RsDeptAllocationHdr
            If Not .EOF Then
                ADDMode = False
                MODIFYMode = False
                lblMKey.Text = .Fields("MKey").Value

                txtWEF.Text = IIf(IsDbNull(.Fields("WEF").Value), "", .Fields("WEF").Value)
                lblWEF.Text = IIf(IsDbNull(.Fields("WEF").Value), "", .Fields("WEF").Value)
                txtAmendNo.Text = IIf(IsDbNull(.Fields("AMEND_NO").Value), "", .Fields("AMEND_NO").Value)
                chkStatus.CheckState = IIf(.Fields("Status").Value = "O", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                txtRemarks.Text = IIf(IsDbNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                Call ShowDetail()

                Call MakeEnableDesableField(True)

            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsDeptAllocationHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowDetail()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemDesc As String
        Dim CntRow As Integer
        Dim mDeptCode As String

        SqlStr = ""
        SqlStr = " SELECT * FROM PAY_DEPT_ALLOCATION_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'" & vbCrLf & " ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDeptAllocationDet, ADODB.LockTypeEnum.adLockReadOnly)

        With RsDeptAllocationDet
            If .EOF = False Then
                I = 1
                Do While Not .EOF
                    sprdMain.Row = I

                    sprdMain.Col = ColDeptCode
                    sprdMain.Text = IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)

                    sprdMain.Col = ColDeptName
                    If MainClass.ValidateWithMasterTable(.Fields("DEPT_CODE"), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND DEPT_TYPE IN ('N','1','2')") = True Then
                        sprdMain.Text = MasterNo
                    End If

                    sprdMain.Col = ColWeld
                    sprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("DEPT_MWS").Value), "", .Fields("DEPT_MWS").Value)))

                    sprdMain.Col = ColPPS
                    sprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("DEPT_PPS").Value), "", .Fields("DEPT_PPS").Value)))

                    sprdMain.Col = ColNPC
                    sprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("DEPT_NPC").Value), "", .Fields("DEPT_NPC").Value)))

                    sprdMain.Col = ColPRS
                    sprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("DEPT_PRS").Value), "", .Fields("DEPT_PRS").Value)))

                    sprdMain.Col = ColASY
                    sprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("DEPT_ASY").Value), "", .Fields("DEPT_ASY").Value)))

                    sprdMain.Col = ColPLT
                    sprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("DEPT_PLT").Value), "", .Fields("DEPT_PLT").Value)))

                    sprdMain.Col = ColZNC
                    sprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("DEPT_ZNC").Value), "", .Fields("DEPT_ZNC").Value)))

                    sprdMain.Col = ColFRM
                    sprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("DEPT_FRM").Value), "", .Fields("DEPT_FRM").Value)))

                    sprdMain.Col = ColHDL
                    sprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("DEPT_HDL").Value), "", .Fields("DEPT_HDL").Value)))

                    sprdMain.Col = ColCCD
                    sprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("DEPT_CCD").Value), "", .Fields("DEPT_CCD").Value)))

                    sprdMain.Col = ColNPD
                    sprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("DEPT_NPD").Value), "", .Fields("DEPT_NPD").Value)))


                    .MoveNext()
                    I = I + 1
                    sprdMain.MaxRows = I
                Loop
            End If
        End With

        FormatSprdMain(-1)
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mMKEY As String
        Dim mStatus As String

        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked, "O", "C")

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If ADDMode = True Then
            mMKEY = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") & VB6.Format(txtAmendNo.Text, "000")
            lblMKey.Text = mMKEY

            SqlStr = " INSERT INTO PAY_DEPT_ALLOCATION_HDR  (" & vbCrLf & " MKEY, COMPANY_CODE, " & vbCrLf & " WEF, AMEND_NO, REMARKS, STATUS," & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE ) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(mMKEY) & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')," & Val(txtAmendNo.Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', '" & mStatus & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = " UPDATE PAY_DEPT_ALLOCATION_HDR   SET " & vbCrLf & " WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " AMEND_NO=" & Val(txtAmendNo.Text) & ", " & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf & " STATUS='" & mStatus & "', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE Mkey ='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'"

        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1 = False Then GoTo ErrPart

        If Val(txtAmendNo.Text) > 0 Then
            If UpdatePreviousRate(Val(txtAmendNo.Text), "C") = False Then GoTo ErrPart
        End If

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsDeptAllocationHdr.Requery()
        RsDeptAllocationDet.Requery()
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
    End Function

    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mDeptCode As String
        Dim mDeptMWS As Short
        Dim mDeptPPS As Short
        Dim mDeptNPC As Short
        Dim mDeptPRS As Short
        Dim mDeptASY As Short
        Dim mDeptPLT As Short
        Dim mDeptZNC As Short
        Dim mDeptFRM As Short
        Dim mDeptHDL As Short
        Dim mDeptCCD As Short
        Dim mDeptNPD As Short


        PubDBCn.Execute("DELETE FROM PAY_DEPT_ALLOCATION_DET  " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        With sprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColDeptCode
                mDeptCode = MainClass.AllowSingleQuote(.Text)

                sprdMain.Col = ColWeld
                mDeptMWS = Val(.Text)

                sprdMain.Col = ColPPS
                mDeptPPS = Val(.Text)

                sprdMain.Col = ColNPC
                mDeptNPC = Val(.Text)

                sprdMain.Col = ColPRS
                mDeptPRS = Val(.Text)

                sprdMain.Col = ColASY
                mDeptASY = Val(.Text)

                sprdMain.Col = ColPLT
                mDeptPLT = Val(.Text)

                sprdMain.Col = ColZNC
                mDeptZNC = Val(.Text)

                sprdMain.Col = ColFRM
                mDeptFRM = Val(.Text)

                sprdMain.Col = ColHDL
                mDeptHDL = Val(.Text)

                sprdMain.Col = ColCCD
                mDeptCCD = Val(.Text)

                sprdMain.Col = ColNPD
                mDeptNPD = Val(.Text)

                SqlStr = ""
                If Trim(mDeptCode) <> "" Then
                    SqlStr = " INSERT INTO  PAY_DEPT_ALLOCATION_DET ( " & vbCrLf & " MKEY, COMPANY_CODE, " & vbCrLf & " WEF, AMEND_NO, SERIAL_NO, " & vbCrLf & " DEPT_CODE, DEPT_MWS, DEPT_PPS, DEPT_NPC, " & vbCrLf & " DEPT_PRS, DEPT_ASY, DEPT_PLT, DEPT_ZNC, " & vbCrLf & " DEPT_FRM, DEPT_HDL, DEPT_CCD, DEPT_NPD " & vbCrLf & " ) VALUES ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtAmendNo.Text) & "," & I & ", " & vbCrLf & " '" & mDeptCode & "', " & vbCrLf & " " & mDeptMWS & ", " & mDeptPPS & ", " & mDeptNPC & ", " & mDeptPRS & ", " & vbCrLf & " " & mDeptASY & ", " & mDeptPLT & ", " & mDeptZNC & ", " & mDeptFRM & ", " & vbCrLf & " " & mDeptHDL & ", " & mDeptCCD & ", " & mDeptNPD & " " & vbCrLf & " )"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        MsgBox(Err.Description)
        UpdateDetail1 = False
    End Function

    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsDeptAllocationHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        txtWEF.Text = VB6.Format(SprdView.Text, "DD/MM/YYYY")

        ViewGrid()
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles sprdMain.ClickEvent

        On Error GoTo ERR1
        Dim mDeptCode As String
        Dim SqlStr As String = ""
        Dim mCCDesc As String

        If eventArgs.Row = 0 And eventArgs.Col = ColDeptCode Then
            With sprdMain
                .Row = .ActiveRow
                .Col = ColDeptCode
                If MainClass.SearchGridMaster(.Text, "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND DEPT_TYPE IN ('N','1','2')") = True Then
                    .Row = .ActiveRow
                    .Col = ColDeptCode
                    .Text = AcName
                    .Col = ColDeptName
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColDeptName Then
            With sprdMain
                .Row = .ActiveRow
                .Col = ColDeptName
                If MainClass.SearchGridMaster(.Text, "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND DEPT_TYPE IN ('N','1','2')") = True Then
                    .Row = .ActiveRow
                    .Col = ColDeptCode
                    .Text = AcName1
                    .Col = ColDeptName
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(sprdMain, eventArgs.Row, ColDeptCode)
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles sprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = sprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptName, 0))
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mDeptCode As String
        Dim mRow As Integer

        If eventArgs.NewRow = -1 Then Exit Sub
        mRow = sprdMain.ActiveRow
        '    SprdMain.Row = Row
        '    If Trim(SprdMain.Text) = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColDeptCode
                sprdMain.Row = mRow
                sprdMain.Col = ColDeptCode
                If Trim(sprdMain.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(sprdMain.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_TYPE IN ('N','1','2')") = False Then
                        MainClass.SetFocusToCell(sprdMain, eventArgs.row, ColDeptCode)
                        eventArgs.cancel = True
                    Else
                        sprdMain.Col = ColDeptName
                        sprdMain.Text = MasterNo
                    End If
                    MainClass.AddBlankSprdRow(sprdMain, ColDeptCode, ConRowHeight)
                    FormatSprdMain(eventArgs.row)
                End If
                If DuplicateRow() = True Then
                    MainClass.SetFocusToCell(sprdMain, sprdMain.ActiveRow, ColDeptCode)
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColDeptName
                sprdMain.Row = mRow
                sprdMain.Col = ColDeptName
                If Trim(sprdMain.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(sprdMain.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND DEPT_TYPE IN ('N','1','2')") = False Then
                        MainClass.SetFocusToCell(sprdMain, eventArgs.row, ColDeptCode)
                        eventArgs.cancel = True
                    End If
                End If
        End Select

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function DuplicateRow() As Boolean
        Dim CntRow As Integer
        Dim mCount As Byte
        Dim mCheckString As String
        Dim mRowString As String
        Dim mDeptCode As String

        DuplicateRow = False

        With sprdMain

            .Col = ColDeptCode
            mCheckString = UCase(Trim(.Text))

            For CntRow = 1 To .MaxRows
                .Row = CntRow

                .Col = ColDeptCode
                mDeptCode = UCase(Trim(.Text))
                mRowString = UCase(Trim(.Text))

                If mCheckString = mRowString Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateRow = True
                    MsgInformation("Duplicate Dept : " & mDeptCode)
                    Exit Function
                End If
            Next
        End With
    End Function
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles sprdMain.Leave
        With sprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub txtAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Function ShowRecord() As Boolean

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mWef As String
        Dim xMkey As String = ""

        ShowRecord = True

        If Trim(txtWEF.Text) = "" Then
            SqlStr = " SELECT MAX(WEF) AS WEF " & vbCrLf & " FROM PAY_DEPT_ALLOCATION_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDeptAllocationHdr, ADODB.LockTypeEnum.adLockReadOnly)
            If RsDeptAllocationHdr.EOF = True Then
                Exit Function
            End If
        End If

        If MODIFYMode = True And RsDeptAllocationHdr.EOF = False Then xMkey = RsDeptAllocationHdr.Fields("mKey").Value
        SqlStr = " SELECT * FROM PAY_DEPT_ALLOCATION_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtWEF.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) AS WEF " & vbCrLf & " FROM PAY_DEPT_ALLOCATION_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDeptAllocationHdr, ADODB.LockTypeEnum.adLockReadOnly)
        If RsDeptAllocationHdr.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Operation Rate Not Entered For This Item. Click Add For New.", MsgBoxStyle.Information)
                ShowRecord = False
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PAY_DEPT_ALLOCATION_HDR " & vbCrLf & " WHERE UPPER(LTRIM(RTRIM(MKey)))='" & MainClass.AllowSingleQuote(UCase(xMkey)) & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDeptAllocationHdr, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtWEF_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtWEF_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtWEF.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchWEF_Click(cmdSearchWEF, New System.EventArgs())
    End Sub
    Private Sub txtWEF_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        If Trim(txtWEF.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtWEF.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If mAmendStatus = True Then
            If CDate(txtWEF.Text) <= CDate(lblWEF.Text) Then
                MsgBox("W.E.F. Date Should be greater than Previous Date")
                Cancel = True
                GoTo EventExitSub
            End If
        End If

        If ShowRecord = False Then Cancel = True


        GoTo EventExitSub
ERR1:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
