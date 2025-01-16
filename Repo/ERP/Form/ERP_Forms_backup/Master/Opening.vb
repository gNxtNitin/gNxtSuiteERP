Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmOpening
   Inherits System.Windows.Forms.Form
   Dim RsOpening As ADODB.Recordset
   'Dim PvtDBCn As ADODB.Connection

   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String

   Dim FormActive As Boolean

   Dim CurMKey As String
    Dim SqlStr As String = ""

    Private Const ConRowHeight As Short = 15
    Private Const ColStatus As Short = 1
    Private Const ColDivision As Short = 2
    Private Const ColDeptCode As Short = 3
    Private Const ColDeptName As Short = 4
    Private Const ColLotNo As Short = 5
    Private Const ColStockType As Short = 6
    Private Const ColQty As Short = 7
    Private Const ColPartyCode As Short = 8


    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            '        txtItemCode.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsOpening.EOF = False Then RsOpening.MoveFirst()
            Show1()
            txtItemcode.Enabled = True
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If ValidateBranchLocking(RsCompany.Fields("Start_Date").Value) = True Then
            Exit Sub
        End If

        If txtItemcode.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsOpening.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_OPENING_BAL", (txtItemcode.Text), RsOpening) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_OPENING_BAL", "ITEM_CODE", (txtItemcode.Text)) = False Then GoTo DelErrPart

                If DeleteOpeningStockTRN(PubDBCn, ConStockRefType_OPN, (txtItemcode.Text), (lblStockID.Text), "O") = False Then GoTo DelErrPart
                If lblStockID.Text = ConWH Then
                    If DeletePaintStockTRN(PubDBCn, ConStockRefType_OPN, (LblMKey.Text)) = False Then GoTo DelErrPart
                End If

                PubDBCn.CommitTrans()
                RsOpening.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        RsOpening.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        '    SqlStr = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItemName.Text = AcName
            txtItemcode.Text = AcName1
            TxtItemName_Validating(txtItemName, New System.ComponentModel.CancelEventArgs(True))
            If txtItemcode.Enabled = True Then txtItemcode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsOpening, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtItemcode.Enabled = False
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
        '    Call ReportonIndent(crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '    Call ReportonIndent(crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonIndent(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim mBranchCode As Integer
        Dim mCategoryCode As Integer
        Dim mRPTName As String = ""

        '    Report1.Reset
        '    SqlStr = ""
        '    Screen.MousePointer = 11
        '
        '    Call SelectQry(SqlStr)
        '    Screen.MousePointer = 0
        '
        '    mSubTitle = ""
        '
        '    mRPTName = "\reports\PrintPPO.rpt"
        '    mTitle = "Pre-Purchase Order (Indigineious)"
        '
        '    Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle, "P")
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mFlag As String)
        'Dim mAmtinWord As String
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & mRPTName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1

    End Sub

    Private Sub ShowTermsReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & mRPTName
        'Report1.SQLQuery = mSqlStr
        'Report1.WindowShowGroupTree = False
        Report1.Action = 1

    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtItemCode_Validating(txtItemcode, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            GoTo ErrorHandler
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ViewGrid()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub FrmOpening_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        If lblStockID.Text = ConWH Then
            Me.Text = "Opening Balance - Store"
        ElseIf lblStockID.Text = ConPH Then
            Me.Text = "Opening Balance - Production"
        ElseIf lblStockID.Text = ConJW Then
            Me.Text = "Opening Balance - Job Work"
        ElseIf lblStockID.Text = ConSH Then
            Me.Text = "Opening Balance - Sub Store"
        End If

        SqlStr = ""
        SqlStr = "Select * from INV_ITEM_MST Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpening, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call SetTextLengths()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub FrmOpening_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmOpening_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call SetMainFormCordinate(Me)
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        'Me.Top = 0
        'Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo SetTextLengthsErr
        txtItemcode.MaxLength = RsOpening.Fields("ITEM_CODE").DefinedSize

        Exit Sub
SetTextLengthsErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub Clear1()

        SqlStr = ""

        txtItemcode.Text = ""
        txtItemName.Text = ""
        txtItemUOM.Text = ""
        txtItemUOM.Text = ""
        txtPurchaseCost.Text = ""
        txtLandedCost.Text = ""
        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)
        Call AutoCompleteSearch("INV_ITEM_MST", "ITEM_SHORT_DESC", "", txtItemName)
        Call AutoCompleteSearch("INV_ITEM_MST", "ITEM_CODE", "", txtItemcode)
        MainClass.ButtonStatus(Me, XRIGHT, RsOpening, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        FraCmd.Enabled = True
        If Not RsOpening.EOF Then

            txtItemcode.Text = IIf(IsDBNull(RsOpening.Fields("ITEM_CODE").Value), "", RsOpening.Fields("ITEM_CODE").Value)

            MainClass.ValidateWithMasterTable(txtItemcode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            txtItemName.Text = MasterNo


            If MainClass.ValidateWithMasterTable(txtItemcode.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtItemUOM.Text = MasterNo
            End If

            '        txtItemUOM.Text = IIf(IsNull(RsOpening!ISSUE_UOM), "", RsOpening!ISSUE_UOM)

            Call ShowDetail1(Trim(txtItemcode.Text))
            RsOpening.MoveFirst()
        End If
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsOpening, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        txtItemcode.Enabled = True
        SprdMain.Enabled = True
        Exit Sub
ShowErrPart:

        If Err.Number = -2147418113 Then
            RsOpening.Requery()
            Resume
        End If
        MsgBox(Err.Description, Err.Number)

    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mLotNoRequied As String

        FieldsVarification = True
        If ValidateBranchLocking(RsCompany.Fields("Start_Date").Value) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsOpening.EOF = True Then Exit Function

        If Trim(txtItemcode.Text) = "" Then
            MsgInformation("Item Code is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If PubSuperUser <> "S" Then
            MsgInformation("Cann't be Change Opening Balance.")
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColDeptCode, "S", "DeptCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColDivision, "S", "Division Is Blank.") = False Then FieldsVarification = False : Exit Function

        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Stock Type Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False : Exit Function

        mLotNoRequied = "N"
        If MainClass.ValidateWithMasterTable(txtItemcode.Text, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mLotNoRequied = MasterNo
        End If
        If mLotNoRequied = "Y" Then
            If MainClass.ValidDataInGrid(SprdMain, ColLotNo, "N", "Please Check LotNo.") = False Then FieldsVarification = False : Exit Function
        End If


        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim i As Integer
        Dim mItemCode As String
        Dim mDeptCode As String
        Dim mStockType As String = ""
        Dim mQty As Double
        Dim nMkey As String
        Dim mCurRowNo As Integer
        Dim mLotNo As String
        Dim mBatchNo As String
        Dim mPartyCode As String
        Dim mDivisionDesc As String
        Dim mDivisionCode As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mItemCode = RTrim(txtItemcode.Text)

        nMkey = ""
        If GetValidateRefNo(mItemCode, nMkey) = False Then
            mCurRowNo = MainClass.AutoGenRowNo("INV_OPN_BAL", "RowNo", PubDBCn)
            nMkey = mCurRowNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If


        '    SqlStr = "DELETE FROM INV_OPENING_BAL WHERE " ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUBSTR(MKEY,LENGTH(MKEY)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        '    PubDBCn.Execute SqlStr
        '
        If DeleteOpeningStockTRN(PubDBCn, ConStockRefType_OPN, mItemCode, (lblStockID.Text), "O") = False Then GoTo ErrPart
        If lblStockID.Text = ConWH Then
            If DeletePaintStockTRN(PubDBCn, ConStockRefType_OPN, nMkey) = False Then GoTo ErrPart
        End If

        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColDeptCode
                mDeptCode = .Text

                .Col = ColDivision
                mDivisionDesc = Trim(.Text)

                If MainClass.ValidateWithMasterTable(Trim(mDivisionDesc), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionCode = CDbl(Trim(MasterNo))
                End If

                .Col = ColStockType
                mStockType = .Text

                .Col = ColLotNo
                mLotNo = Trim(.Text)
                mBatchNo = CStr(Val(.Text))

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColPartyCode
                mPartyCode = Trim(.Text)

                SqlStr = ""

                '            SqlStr = " INSERT INTO INV_OPENING_BAL ( MKEY," & vbCrLf _
                ''                    & " COMPANY_CODE, OPENING_DATE, ITEM_CODE,ITEM_UOM, " & vbCrLf _
                ''                    & " DEPT_CODE, STOCK_TYPE, " & vbCrLf _
                ''                    & " STK_QTY, LOT_NO, ADDUSER,ADDDATE, " & vbCrLf _
                ''                    & " MODUSER,MODDATE) VALUES ( "
                '
                '            SqlStr = SqlStr & vbCrLf _
                ''                    & " " & Val(nMkey) & ", " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                ''                    & " '" & VB6.Format(RsCompany!Start_Date - 1, "dd-MMM-yyyy") & "'," & vbCrLf _
                ''                    & " '" & MainClass.AllowSingleQuote(mItemCode) & "'," & vbCrLf _
                ''                    & " '" & MainClass.AllowSingleQuote(txtItemUOM.Text) & "'," & vbCrLf _
                ''                    & " '" & MainClass.AllowSingleQuote(mDeptCode) & "'," & vbCrLf _
                ''                    & " '" & MainClass.AllowSingleQuote(mStockType) & "'," & vbCrLf _
                ''                    & " " & Val(mQty) & ", '" & MainClass.AllowSingleQuote(mLotNo) & "'," & vbCrLf _
                ''                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                ''                    & " TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                ''                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                ''                    & " TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))"
                '            PubDBCn.Execute SqlStr

                If UpdateStockTRN(PubDBCn, ConStockRefType_OPN, nMkey, i, DateAdd("d", -1, CDate(RsCompany.Fields("Start_Date").Value).ToString("dd/MM/yyyy")),
                                  DateAdd("d", -1, CDate(RsCompany.Fields("Start_Date").Value).ToString("dd/MM/yyyy")), mStockType, mItemCode, (txtItemUOM.Text),
                                  mBatchNo, mQty, 0, "I", Val(txtPurchaseCost.Text), Val(txtLandedCost.Text), "", "", mDeptCode, mDeptCode, "", "N", "Opening",
                                  mPartyCode, (lblStockID.Text), mDivisionCode, "", "") = False Then GoTo ErrPart

                'If Trim(mLotNo) <> "" And lblStockID.Text = ConWH Then
                '    If UpdatePaintStockTRN(PubDBCn, ConStockRefType_OPN, nMkey, i, CStr(RsCompany.Fields("Start_Date").Value - 1), mStockType, mItemCode, (txtItemUOM.Text), CStr(-1), mLotNo, mQty, 0, "I", "N", "Opening") = False Then GoTo ErrPart
                'End If

            Next
        End With
        Update1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsOpening.Requery()

        MsgBox(Err.Description)
        ''Resume
    End Function


    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataPPOMain.Refresh
            SprdView.Refresh()
            SprdView.Focus()
            FraTop.Visible = False
            Frabot.Visible = False
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraTop.Visible = True
            Frabot.Visible = True
            SprdView.SendToBack()
        End If
        Call FormatSprdView()
        MainClass.ButtonStatus(Me, XRIGHT, RsOpening, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FrmOpening_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False

        RsOpening.Close()
        'PvtDBCn.Close

        RsOpening = Nothing
        'Set PvtDBCn = Nothing
    End Sub



    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent

        Dim mActiveCol As Integer

        mActiveCol = SprdMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColQty Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColQty
                If CheckQty(ColQty, (SprdMain.ActiveRow)) = True Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColQty, ConRowHeight)
                        FormatSprdMain((SprdMain.MaxRows))
                    End If
                End If
            End If
        End If
        eventArgs.keyCode = 9999
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtItemcode.Text = SprdView.Text

        txtItemCode_Validating(txtItemcode, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xIDesc As String = ""
        Dim xICode As String = ""

        If eventArgs.row = 0 And eventArgs.col = ColDivision Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColDivision
                If MainClass.SearchGridMaster(.Text, "INV_DIVISION_MST", "DIV_DESC", "DIV_CODE", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColDivision
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColDivision)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColDeptCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColDeptCode
                If MainClass.SearchGridMaster(.Text, "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColDeptCode
                    .Text = AcName

                    .Col = ColDeptName
                    .Text = AcName1

                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColDeptCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColStockType Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColStockType
                If MainClass.SearchGridMaster(.Text, "INV_TYPE_MST", "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColStockType
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColStockType)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColPartyCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColPartyCode
                If MainClass.SearchGridMaster(.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", "SUPP_CUST_NAME", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColPartyCode
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColPartyCode)
            End With
        End If

        Dim mItemCode As String
        'Dim mOSNo As Integer
        Dim DelStatus As Boolean
        If eventArgs.col = 0 And eventArgs.row > 0 Then

            SprdMain.Row = eventArgs.row

            SprdMain.Col = ColDeptCode
            mItemCode = SprdMain.Text

            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColDeptCode, DelStatus)
                MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
            End If
        End If
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColDivision Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDivision, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptName, 0))

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColStockType Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColStockType, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColPartyCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColPartyCode, 0))

    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ERR1
        Dim mQty As Double
        Dim mIndentSNo As Integer
        Dim mDeptCode As String
        Dim mLotNoRequied As String
        Dim mDivision As String
        Dim mCheckString As String

        If eventArgs.newRow = -1 Then Exit Sub
        With SprdMain
            .Row = .ActiveRow

            .Col = ColDeptCode
            If Trim(.Text) = "" Then Exit Sub

            .Col = ColDivision
            If Trim(.Text) = "" Then Exit Sub

            Select Case eventArgs.col
                Case ColDivision
                    .Row = .ActiveRow

                    .Col = ColDivision
                    mCheckString = Trim(.Text)

                    .Col = ColDeptCode
                    mCheckString = mCheckString & Trim(.Text)

                    .Col = ColLotNo
                    mCheckString = mCheckString & Trim(.Text)

                    .Col = ColStockType
                    mCheckString = mCheckString & Trim(.Text)



                    If CheckDuplicate(mCheckString, ColDivision) = False Then
                        eventArgs.row = .ActiveRow
                        eventArgs.col = ColDivision
                        If MainClass.ValidateWithMasterTable(.Text, "DIV_DESC", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            eventArgs.row = .ActiveRow
                            eventArgs.col = ColDivision
                            .Text = MasterNo
                        Else
                            '                        MsgInformation "Invalid Dept Code."
                            MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColDivision)
                        End If
                    End If

                Case ColDeptCode
                    .Row = .ActiveRow

                    .Col = ColDivision
                    mCheckString = Trim(.Text)

                    .Col = ColDeptCode
                    mCheckString = mCheckString & Trim(.Text)

                    .Col = ColLotNo
                    mCheckString = mCheckString & Trim(.Text)

                    .Col = ColStockType
                    mCheckString = mCheckString & Trim(.Text)

                    If CheckDuplicate(mCheckString, ColDeptCode) = False Then
                        .Row = .ActiveRow
                        .Col = ColDeptCode
                        If MainClass.ValidateWithMasterTable(.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            .Row = .ActiveRow
                            .Col = ColDeptName
                            .Text = MasterNo
                        Else
                            '                        MsgInformation "Invalid Dept Code."
                            MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColDeptCode)
                        End If
                    End If

                Case ColStockType
                    .Row = .ActiveRow

                    .Col = ColDivision
                    mCheckString = Trim(.Text)

                    .Col = ColDeptCode
                    mCheckString = mCheckString & Trim(.Text)

                    .Col = ColLotNo
                    mCheckString = mCheckString & Trim(.Text)

                    .Col = ColStockType
                    mCheckString = mCheckString & Trim(.Text)

                    If CheckDuplicate(mCheckString, ColStockType) = False Then
                        .Row = .ActiveRow
                        .Col = ColStockType
                        If Trim(.Text) <> "" Then
                            If MainClass.ValidateWithMasterTable(.Text, "STOCK_TYPE_CODE", "STOCK_TYPE_CODE", "INV_TYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                                '                     MsgInformation "Invalid Stock Type."
                                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColStockType)
                            End If
                        End If
                    End If
                Case ColPartyCode
                    .Row = .ActiveRow

                    .Col = ColDivision
                    mCheckString = Trim(.Text)

                    .Col = ColDeptCode
                    mCheckString = mCheckString & Trim(.Text)

                    .Col = ColLotNo
                    mCheckString = mCheckString & Trim(.Text)

                    .Col = ColStockType
                    mCheckString = mCheckString & Trim(.Text)

                    If CheckDuplicate(mCheckString, ColStockType) = False Then
                        .Row = .ActiveRow
                        .Col = ColPartyCode
                        If Trim(.Text) <> "" Then
                            If MainClass.ValidateWithMasterTable(.Text, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColPartyCode)
                            End If
                        End If
                    End If
                Case ColLotNo
                    .Row = .ActiveRow
                    mLotNoRequied = "N"
                    If MainClass.ValidateWithMasterTable(txtItemcode.Text, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mLotNoRequied = MasterNo
                    End If
                    If mLotNoRequied = "Y" Then
                        .Col = ColLotNo
                        If Trim(.Text) = "" Then
                            MsgInformation("Lot No. Must For Such Item.")
                            MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColLotNo)
                        End If
                    End If
                Case ColQty
                    .Row = .ActiveRow
                    .Col = ColQty
                    mQty = Val(.Text)

                    If CheckQty(ColDeptCode, eventArgs.row) = False Then
                        MainClass.AddBlankSprdRow(SprdMain, ColDeptCode, ConRowHeight)
                    End If


            End Select
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        '    Resume
        MsgInformation(Err.Description)
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemcode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemcode.DoubleClick
        Call SearchItemCode()
    End Sub

    Private Sub txtItemCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemcode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemcode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemcode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchItemCode()
    End Sub

    Public Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemcode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtItemcode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtItemcode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtItemName.Text = MasterNo
        Else
            MsgInformation("Invalid Item Code.")
            Cancel = True
            Exit Sub
        End If
        TxtItemName_Validating(txtItemName, New System.ComponentModel.CancelEventArgs(True))

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FrmOpening_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf _
           & " OP.ITEM_CODE,  INVMST.ITEM_SHORT_DESC, " & vbCrLf _
           & " OP.ITEM_UOM, OP.DEPT_CODE,OP. STOCK_TYPE, OP.STK_QTY " & vbCrLf _
           & " FROM INV_OPENING_BAL OP, INV_ITEM_MST INVMST " & vbCrLf _
           & " WHERE OP.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
           & " AND OP.ITEM_CODE =INVMST.ITEM_CODE" & vbCrLf _
           & " AND OP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
           & " AND SUBSTR(MKEY,LENGTH(MKEY)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " ORDER BY OP.ITEM_CODE,OP.DEPT_CODE"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 20)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)
            .set_ColWidth(2, 35)
            .set_ColWidth(3, 8)
            .set_ColWidth(4, 10)
            .set_ColWidth(5, 10)
            .set_ColWidth(6, 10)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub


    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColStatus
            .ColHidden = True

            .Col = ColDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColDeptCode, 6)

            .Col = ColDivision
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColDivision, 15)

            .Col = ColDeptName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColDeptName, 22)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditMultiLine = False
            .set_ColWidth(ColStockType, 10)

            .Col = ColLotNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColLotNo, 6)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("9999999999.9999")
            .TypeFloatMin = CDbl("-9999999999.9999")
            .set_ColWidth(ColQty, 15)
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColPartyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColPartyCode, 10)

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDeptName, ColDeptName)
        End With
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
    End Sub


    Private Sub ShowDetail1(ByRef pItemCode As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim i As Integer
        Dim mItemCode As String
        Dim mDeptCode As String
        Dim mDeptDesc As String
        Dim mTableName As String
        Dim mDivisionCode As Double

        mTableName = ConInventoryTable

        '    If RsCompany.Fields("COMPANY_CODE").Value = 1 Then
        '        mTableName = "INV_STOCK_REC_TRN" & RsCompany.Fields("FYEAR").Value
        '    ElseIf RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
        '        mTableName = "INV_STOCK_REC_TRN" & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") & RsCompany.Fields("FYEAR").Value
        '    Else
        '        mTableName = "INV_STOCK_REC_TRN"
        '    End If
        '
        SqlStr = " SELECT * " & vbCrLf & " FROM " & mTableName & "" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND REF_TYPE='" & ConStockRefType_OPN & "'" & vbCrLf & " AND STOCK_ID='" & lblStockID.Text & "'" & vbCrLf & " AND ITEM_CODE='" & pItemCode & "'"
        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTemp

            If .EOF = True Then Exit Sub
            i = 0
            .MoveFirst()
            '        txtItemUOM.Text = IIf(IsNull(RsTemp!ITEM_UOM), "", RsTemp!ITEM_UOM)
            txtPurchaseCost.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PURCHASE_COST").Value), 0, RsTemp.Fields("PURCHASE_COST").Value), "0.00")
            txtLandedCost.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("LANDED_COST").Value), "", RsTemp.Fields("LANDED_COST").Value), "0.00")
            LblMKey.Text = IIf(IsDBNull(RsTemp.Fields("REF_NO").Value), "", RsTemp.Fields("REF_NO").Value)

            Do While Not .EOF
                i = i + 1
                SprdMain.MaxRows = SprdMain.MaxRows + 1
                SprdMain.Row = i

                SprdMain.Col = ColDeptCode
                SprdMain.Text = IIf(IsDBNull(.Fields("DEPT_CODE_TO").Value), "STR", .Fields("DEPT_CODE_TO").Value)
                mDeptCode = IIf(IsDBNull(.Fields("DEPT_CODE_TO").Value), "STR", .Fields("DEPT_CODE_TO").Value)



                SprdMain.Col = ColDeptName
                MainClass.ValidateWithMasterTable(mDeptCode, "DEPT_CODE", "DEPT_DESC", "pay_dept_mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mDeptDesc = MasterNo
                SprdMain.Text = mDeptDesc

                mDivisionCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                SprdMain.Col = ColDivision
                MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                SprdMain.Text = Trim(MasterNo)


                SprdMain.Col = ColStockType
                SprdMain.Text = IIf(IsDBNull(.Fields("STOCK_TYPE").Value), 0, .Fields("STOCK_TYPE").Value)

                SprdMain.Col = ColLotNo
                SprdMain.Text = Trim(Str(IIf(IsDBNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value)))
                SprdMain.Text = IIf(SprdMain.Text = "-1" Or SprdMain.Text = "0", "", SprdMain.Text)

                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), "", .Fields("ITEM_QTY").Value)))

                SprdMain.Col = ColPartyCode
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("PARTYCODE").Value), "", .Fields("PARTYCODE").Value))
                SprdMain.Text = IIf(SprdMain.Text = "-1" Or SprdMain.Text = "0", "", SprdMain.Text)
                .MoveNext()
            Loop

        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        MsgBox(Err.Description)

    End Sub
    Private Function GetValidateRefNo(ByRef pItemCode As String, ByRef nMkey As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTableName As String

        GetValidateRefNo = False

        mTableName = ConInventoryTable
        '
        '    If RsCompany.Fields("COMPANY_CODE").Value = 1 Then
        '        mTableName = "INV_STOCK_REC_TRN" & RsCompany.Fields("FYEAR").Value
        '    ElseIf RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
        '        mTableName = "INV_STOCK_REC_TRN" & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") & RsCompany.Fields("FYEAR").Value
        '    Else
        '        mTableName = "INV_STOCK_REC_TRN"
        '    End If

        SqlStr = " SELECT DISTINCT REF_NO " & vbCrLf & " FROM " & mTableName & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND REF_TYPE='" & ConStockRefType_OPN & "'" & vbCrLf & " AND STOCK_ID='" & lblStockID.Text & "'" & vbCrLf & " AND ITEM_CODE='" & pItemCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTemp
            If .EOF = False Then
                nMkey = IIf(IsDBNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)
                GetValidateRefNo = True
            End If
        End With
        Exit Function
ERR1:
        MsgBox(Err.Description)
        GetValidateRefNo = False
    End Function

    Private Function CheckDuplicate(ByRef mCheckString As String, ByRef mCol As Integer) As Boolean

        On Error GoTo ERR1
        Dim i As Integer
        Dim mCheckCode As String
        Dim mCheckSNo As Integer
        Dim mItemRept As Integer

        '    CheckDuplicateDept = True
        '    Exit Function

        If Trim(mCheckString) = "" Then CheckDuplicate = True : Exit Function

        With SprdMain
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColDivision
                mCheckCode = Trim(.Text)

                .Col = ColDeptCode
                mCheckCode = mCheckCode & Trim(.Text)

                .Col = ColLotNo
                mCheckCode = mCheckCode & Trim(.Text)

                .Col = ColStockType
                mCheckCode = mCheckCode & Trim(.Text)

                If mCheckCode = mCheckString Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicate = True
                        MsgInformation("Duplicate Lot No,  Division & Stock Type of Dept Code.")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, mCol)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function CheckQty(ByRef pCol As Integer, ByRef pRow As Integer) As Boolean

        On Error GoTo ERR1
        CheckQty = True
        With SprdMain
            .Row = pRow
            .Col = ColDeptCode
            If Trim(.Text) = "" Then CheckQty = False : Exit Function
            .Col = ColQty
            If Val(.Text) = 0 Then
                CheckQty = False
                MsgInformation("Please Check Quantity")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColQty)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.DoubleClick
        Call cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mItemCode As String

        If Trim(txtItemName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mItemCode = MasterNo
        Else
            MsgInformation("Invalid Item Name.")
            Cancel = True
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtItemUOM.Text = MasterNo
        End If


        If MODIFYMode = True And RsOpening.BOF = False Then mItemCode = RsOpening.Fields("ITEM_CODE").Value

        SqlStr = "SELECT * FROM INV_ITEM_MST " & vbCrLf _
              & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
              & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpening, ADODB.LockTypeEnum.adLockReadOnly)
        If RsOpening.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No data found Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM INV_ITEM_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpening, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Public Sub SearchItemCode()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        '    SqlStr = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.SearchGridMaster(txtItemcode.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , SqlStr) = True Then
            txtItemcode.Text = AcName
            txtItemName.Text = AcName1
            TxtItemName_Validating(txtItemName, New System.ComponentModel.CancelEventArgs(True))
            If txtItemcode.Enabled = True Then txtItemcode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

   Private Sub txtLandedCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLandedCost.TextChanged

      MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txtLandedCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLandedCost.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

      KeyAscii = MainClass.SetNumericField(KeyAscii)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub txtPurchaseCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurchaseCost.TextChanged

      MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txtPurchaseCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPurchaseCost.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

      KeyAscii = MainClass.SetNumericField(KeyAscii)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
End Class
