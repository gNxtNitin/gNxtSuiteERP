Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPlatingRateMst
    Inherits System.Windows.Forms.Form
    Dim RsOprRateHdr As ADODB.Recordset
    Dim RsOprRateDet As ADODB.Recordset

    Dim xMyMenu As String

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColBuffingRate As Short = 3
    Private Const ColDeptCode As Short = 4
    Dim mAmendStatus As Boolean

    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        On Error GoTo AssignGridErr
        Dim SqlStr As String

        SqlStr = " SELECT TO_CHAR(WEF_DATE,'DD/MM/YYYY') AS WEF, IH.ITEM_CODE, INVMST.ITEM_SHORT_DESC, AMEND_NO, BUFFING_RATE, DEPT_CODE" & vbCrLf & " FROM INV_BUFFINGITEM_TRN IH, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IH.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " ORDER BY TO_CHAR(WEF_DATE,'DD/MM/YYYY'), AMEND_NO, IH.ITEM_CODE "

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
            .set_ColWidth(2, 10)
            .set_ColWidth(3, 38)
            .set_ColWidth(4, 10)
            .set_ColWidth(5, 10)
            .set_ColWidth(6, 10)
            .set_ColWidth(7, 10)


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

        FieldsVarification = True
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsOprRateHdr.EOF = True Then Exit Function

        If MODIFYMode = True And chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed Operation Rates Cann't be Modified")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtWEF.Text) = "" Then
            MsgBox("W.E.F is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtWEF.Enabled = True Then txtWEF.Focus()
            Exit Function
        End If

        If mAmendStatus = False Then
            Dim mCompanyCode As Long
            mCompanyCode = RsCompany.Fields("COMPANY_CODE").Value
            If MainClass.ValidateWithMasterTable(mCompanyCode, "COMPANY_CODE", "WEF_DATE", "INV_BUFFINGITEM_TRN", PubDBCn, MasterNo, , " WEF_DATE<>TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = True Then
                MsgBox("Already Enter. Please select previous data.", vbInformation)
                FieldsVarification = False
                If txtWEF.Enabled = True Then txtWEF.Focus()
                Exit Function
            End If
        End If

        If Val(txtAmendNo.Text) > 0 Then
            mOldAmendNo = Val(txtAmendNo.Text) - 1
            If MainClass.ValidateWithMasterTable(RsCompany.Fields("COMPANY_CODE").Value, "COMPANY_CODE", "WEF_DATE", "INV_BUFFINGITEM_TRN", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AMEND_NO=" & Val(mOldAmendNo) & "") = True Then
                mLastestWEF = MasterNo

                If CDate(txtWEF.Text) <= CDate(mLastestWEF) Then
                    MsgBox("W.E.F Cann't be less than or equal to Last WEF.", vbInformation)
                    FieldsVarification = False
                    If txtWEF.Enabled = True Then txtWEF.Focus()
                    Exit Function
                End If
            End If
        End If
        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Product Code Is Blank") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColBuffingRate, "N", "Buffing Rate Is Blank") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColDeptCode, "S", "Department Code Is Blank") = False Then FieldsVarification = False : Exit Function



        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            cmdSearchWEF.Enabled = True
            SprdMain.Enabled = True
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
        Dim I As Integer



        txtAmendNo.Text = CStr(GetMaxAmendNo())
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        mAmendStatus = True
        cmdAmend.Enabled = False
        txtWEF.Enabled = True
        SprdMain.Enabled = True
        ADDMode = True
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsOprRateHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed Rates Cann't be Deleted")
            Exit Sub
        End If

        If Trim(txtWEF.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        PubDBCn.Cancel()
        PubDBCn.BeginTrans()

        If Not RsOprRateHdr.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then

                If InsertIntoDelAudit(PubDBCn, "INV_BUFFINGITEM_TRN", (lblMKey.Text), RsOprRateHdr) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_BUFFINGITEM_TRN", "MKEY", (lblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM PRD_OPR_RATE_DET WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM INV_BUFFINGITEM_TRN  WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

                If Val(txtAmendNo.Text) > 0 Then
                    If UpdatePreviousRate(Val(txtAmendNo.Text), "O") = False Then GoTo DelErrPart
                End If

                PubDBCn.CommitTrans()
                RsOprRateHdr.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsOprRateHdr.Requery()
        PubDBCn.Cancel()
        MsgBox(Err.Description)
    End Sub

    Private Function UpdatePreviousRate(ByRef pAmendNo As Integer, ByRef pPreviousStatus As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " UPDATE INV_BUFFINGITEM_TRN SET " & vbCrLf & " STATUS = '" & pPreviousStatus & "', " & vbCrLf & " MODUSER = '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
            & " MODDATE = TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AMEND_NO = " & pAmendNo - 1 & ""

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
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = " SELECT MAX(AMEND_NO) AS AMEND_NO" & vbCrLf & " FROM INV_BUFFINGITEM_TRN" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDBNull(RsTemp.Fields("AMEND_NO").Value) Then
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
            MainClass.ButtonStatus(Me, XRIGHT, RsOprRateHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
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
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRsTemp As ADODB.Recordset

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "Product Wise Operation Rate Master"

        SqlStr = " SELECT * " & vbCrLf & " FROM INV_BUFFINGITEM_TRN, INV_ITEM_MST " & vbCrLf & " WHERE INV_BUFFINGITEM_TRN.COMPANY_CODE=INV_ITEM_MST.COMPANY_CODE " & vbCrLf & " AND INV_BUFFINGITEM_TRN.ITEM_CODE=INV_ITEM_MST.ITEM_CODE " & vbCrLf & " AND INV_BUFFINGITEM_TRN.MKEY='" & MainClass.AllowSingleQuote(lblMKey.Text) & "' ORDER BY SERIAL_NO"

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\BuffingRate.rpt"

        SetCrpt(Report1, Mode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub



    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1() = True Then
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
        ''Resume					
    End Sub
    Private Sub cmdSearchWEF_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchWEF.Click
        On Error GoTo SrchERR
        Dim SqlStr As String

        SqlStr = " SELECT IH.WEF_DATE" & vbCrLf & " FROM INV_BUFFINGITEM_TRN IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""



        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtWEF.Text = VB6.Format(AcName, "DD/MM/YYYY")
            If txtWEF.Enabled = True Then txtWEF.Focus()

            txtWEF_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(False)) ''

        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub frmPlatingRateMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim SqlStr As String
        If FormActive = True Then Exit Sub

        'Me.Text = "Buffing Rate Master"

        SqlStr = "Select * from INV_BUFFINGITEM_TRN Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOprRateHdr, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SetTextLengths()

        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmPlatingRateMst_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmPlatingRateMst_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmPlatingRateMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        With RsOprRateHdr
            txtWEF.MaxLength = .Fields("WEF_DATE").DefinedSize - 6
            txtRemarks.MaxLength = .Fields("Remarks").DefinedSize
        End With
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume					
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtWEF.Enabled = mMode
        cmdSearchWEF.Enabled = True '' mMode					
        txtAmendNo.Enabled = False
    End Sub

    Private Sub frmPlatingRateMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsOprRateHdr.Close()

        RsOprRateHdr = Nothing

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

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)
        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsOprRateHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FormatSprdMain(ByRef mRow As Integer)
        On Error GoTo ERR1
        With SprdMain
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 3.2)
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsOprRateHdr.Fields("ITEM_CODE").DefinedSize
            .set_ColWidth(.Col, 15)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 40)

            .Col = ColBuffingRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("99999.999")
            .TypeFloatMin = CDbl("-99999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 12)

            .Col = ColDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsOprRateHdr.Fields("DEPT_CODE").DefinedSize
            .set_ColWidth(.Col, 8)

        End With
        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols)

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemName)
        '    MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColTotalOldRate, ColTotalOldRate					

        MainClass.SetSpreadColor(SprdMain, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsOprRateDet.Requery()
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub Show1()
        On Error GoTo ERR1

        With RsOprRateHdr
            If Not .EOF Then
                ADDMode = False
                MODIFYMode = False
                lblMKey.Text = .Fields("MKey").Value

                txtWEF.Text = IIf(IsDBNull(.Fields("WEF_DATE").Value), "", .Fields("WEF_DATE").Value)
                lblWEF.Text = IIf(IsDBNull(.Fields("WEF_DATE").Value), "", .Fields("WEF_DATE").Value)
                txtAmendNo.Text = IIf(IsDBNull(.Fields("AMEND_NO").Value), "", .Fields("AMEND_NO").Value)
                chkStatus.CheckState = IIf(.Fields("Status").Value = "O", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                txtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                Call ShowDetail(RsOprRateHdr)
                RsOprRateHdr.MoveFirst()
                Call MakeEnableDesableField(False)
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsOprRateHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = False
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowDetail(ByRef pRsOprRateHdr As ADODB.Recordset)
        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim mItemDesc As String
        Dim mItemCode As String
        Dim cntRow As Integer


        SqlStr = ""
        I = 1
        Do While pRsOprRateHdr.EOF = False
            SprdMain.Row = I

            SprdMain.Col = ColItemCode
            SprdMain.Text = Trim(IIf(IsDBNull(pRsOprRateHdr.Fields("ITEM_CODE").Value), "", pRsOprRateHdr.Fields("ITEM_CODE").Value))
            mItemCode = Trim(SprdMain.Text)

            mItemDesc = ""
            If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemDesc = MasterNo
            End If

            SprdMain.Col = ColItemName
            SprdMain.Text = Trim(mItemDesc)

            SprdMain.Col = ColBuffingRate
            SprdMain.Text = VB6.Format(IIf(IsDBNull(pRsOprRateHdr.Fields("BUFFING_RATE").Value), "0", pRsOprRateHdr.Fields("BUFFING_RATE").Value), "0.000")

            SprdMain.Col = ColDeptCode
            SprdMain.Text = Trim(IIf(IsDBNull(pRsOprRateHdr.Fields("DEPT_CODE").Value), "", pRsOprRateHdr.Fields("DEPT_CODE").Value))

            pRsOprRateHdr.MoveNext()

            I = I + 1
            SprdMain.MaxRows = I
        Loop
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mMKEY As String


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If ADDMode = True Then
            mMKEY = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") & VB6.Format(txtAmendNo.Text, "000")
            lblMKey.Text = mMKEY
        End If

        If UpdateDetail1((lblMKey.Text)) = False Then GoTo ErrPart

        If Val(txtAmendNo.Text) > 0 Then
            If UpdatePreviousRate(Val(txtAmendNo.Text), "C") = False Then GoTo ErrPart
        End If

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsOprRateHdr.Requery()
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
    End Function

    Private Function UpdateDetail1(ByRef mMKEY As String) As Boolean
        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim I As Integer
        Dim mStatus As String
        Dim mItemCode As String
        Dim mBuffingRate As Double
        Dim mDeptCode As String

        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked, "O", "C")

        PubDBCn.Execute("DELETE FROM INV_BUFFINGITEM_TRN  " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")


        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColBuffingRate
                mBuffingRate = CDbl(VB6.Format(.Text, "0.000"))

                .Col = ColDeptCode
                mDeptCode = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If Trim(mItemCode) <> "" Then
                    SqlStr = " INSERT INTO INV_BUFFINGITEM_TRN (" & vbCrLf & " MKEY, COMPANY_CODE, AMEND_NO, WEF_DATE, " & vbCrLf & " ITEM_CODE, BUFFING_RATE, STATUS, DEPT_CODE, " & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE ) VALUES ( "

                    SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(mMKEY) & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(txtAmendNo.Text) & "," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & Val(CStr(mBuffingRate)) & ", '" & mStatus & "','" & MainClass.AllowSingleQuote(mDeptCode) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

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
        MainClass.ButtonStatus(Me, XRIGHT, RsOprRateHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        txtWEF.Text = VB6.Format(SprdView.Text, "DD/MM/YYYY")

        txtWEF_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(False))
        ViewGrid()
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        Dim SqlStr As String
        Dim mRMName As String
        Dim mDeleted As Boolean


        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                SqlStr = "SELECT ITEM_CODE,ITEM_SHORT_DESC " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' " & vbCrLf & " ORDER BY ITEM_CODE "
                .Row = .ActiveRow
                .Col = ColItemCode
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = AcName

                    .Col = ColItemName
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemName Then
            With SprdMain
                SqlStr = "SELECT ITEM_SHORT_DESC,ITEM_CODE " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' " & vbCrLf & " ORDER BY ITEM_SHORT_DESC "

                .Row = .ActiveRow

                .Col = ColItemName
                mRMName = .Text

                .Text = ""
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColItemName
                    .Text = AcName

                    .Col = ColItemCode
                    .Text = AcName1
                Else
                    .Row = .ActiveRow

                    .Col = ColItemName
                    .Text = mRMName
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColDeptCode Then
            With SprdMain
                SqlStr = "SELECT DEPT_CODE, DEPT_DESC " & vbCrLf & " FROM PAY_DEPT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY DEPT_CODE "
                .Row = .ActiveRow
                .Col = ColDeptCode
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColDeptCode
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptCode, 0))
        '    If KeyCode = vbKeyF1 And mCol = ColRMDesc Then SprdMain_Click ColRMDesc, 0					
        '    If KeyCode = vbKeyF1 And mCol = ColStockType Then SprdMain_Click ColStockType, 0					
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim xItemCode As String
        Dim xDeptCode As String

        If eventArgs.newRow = -1 Then Exit Sub
        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColItemCode
        If Trim(SprdMain.Text) = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Col = ColItemCode
                xItemCode = Trim(SprdMain.Text)
                If xItemCode = "" Then Exit Sub
                If FillItemDescPart(xItemCode, True) = True Then
                    If DuplicateItem() = False Then
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        FormatSprdMain(-1)
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColBuffingRate)
                    Else
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                Else
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColDeptCode
                SprdMain.Col = ColDeptCode
                xDeptCode = Trim(SprdMain.Text)
                If xDeptCode = "" Then Exit Sub
                If MainClass.ValidateWithMasterTable(xDeptCode, "DEPT_CODE", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("Invalid Department Code.")
                    eventArgs.cancel = True
                    Exit Sub
                Else
                    FormatSprdMain(-1)
                End If
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
    Private Function FillItemDescPart(ByRef pItemCode As String, ByRef pIsItemCode As Boolean) As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

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
                .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                .Col = ColItemName
                .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                FillItemDescPart = True
            Else
                FillItemDescPart = False

                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
            End If
        End With
        Exit Function
ERR1:
        FillItemDescPart = False
        MsgInformation(Err.Description)
    End Function

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
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
        Dim SqlStr As String
        Dim mWef As String
        Dim xMkey As String

        ShowRecord = True

        If Trim(txtWEF.Text) = "" Then Exit Function

        If MODIFYMode = True And RsOprRateHdr.EOF = False Then xMkey = RsOprRateHdr.Fields("mKey").Value

        SqlStr = " SELECT * FROM INV_BUFFINGITEM_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtWEF.Text) = "" Then
            SqlStr = SqlStr & vbCrLf & " AND WEF_DATE = (" & vbCrLf & " SELECT MAX(WEF_DATE) AS WEF_DATE " & vbCrLf & " FROM INV_BUFFINGITEM_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")"
        Else
            SqlStr = SqlStr & vbCrLf & " AND WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOprRateHdr, ADODB.LockTypeEnum.adLockReadOnly)
        If RsOprRateHdr.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Click Add For New.", MsgBoxStyle.Information)
                ShowRecord = False
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM INV_BUFFINGITEM_TRN" & vbCrLf & " WHERE UPPER(LTRIM(RTRIM(MKey)))='" & MainClass.AllowSingleQuote(UCase(xMkey)) & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOprRateHdr, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtWEF_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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

        If ShowRecord() = False Then Cancel = True


        GoTo EventExitSub
ERR1:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
