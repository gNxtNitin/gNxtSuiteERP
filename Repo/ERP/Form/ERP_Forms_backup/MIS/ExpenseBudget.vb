Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmExpenseBudget
    Inherits System.Windows.Forms.Form
    Dim RsSBHdr As ADODB.Recordset ''ADODB.Recordset	
    Dim RsSBDet As ADODB.Recordset ''ADODB.Recordset	
    'Private PvtDBCn As ADODB.Connection	

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String

    Private Const ConRowHeight As Short = 14

    Private Const ColDate As Short = 1
    Private Const ColMonth As Short = 2
    Private Const ColBudgetAmount As Short = 3
    Private Const ColRemarks As Short = 4

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtName.Enabled = True
            cmdsearch.Enabled = True
            SprdMain.Enabled = True
            SprdMain.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsSBHdr.EOF = False Then RsSBHdr.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume	
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.hide()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click

        On Error GoTo DelErrPart

        If Trim(txtName.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsSBHdr.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.	
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "MIS_EXPBUDGET_HDR", Trim(lblMkey.Text), RsSBHdr) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM MIS_EXPBUDGET_DET WHERE SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblMkey.Text) & "' ")
                PubDBCn.Execute("DELETE FROM MIS_EXPBUDGET_HDR WHERE SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblMkey.Text) & "' ")
                PubDBCn.CommitTrans()
                RsSBHdr.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsSBHdr.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsSBHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtName.Enabled = False
            cmdsearch.Enabled = False
            SprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdMonthly_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdMonthly.Click

        On Error GoTo ErrPart
        Dim cntRow As Integer
        'Dim mAmount As Double	

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColBudgetAmount
                .Text = VB6.Format(Val(txtMonthAmount.Text), "0.00")
            Next
        End With

        Call CalcTots()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
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

    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""

        If ADDMode = True Then
            SqlStr = " INSERT INTO MIS_EXPBUDGET_HDR ( " & vbCrLf _
                & " COMPANY_CODE, FYEAR, SUPP_CUST_CODE, TOT_BUDGET_AMOUNT, REMARKS, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE) VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(lblMkey.Text) & "', " & Val(lblTotalBudget.Text) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "',  " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE MIS_EXPBUDGET_HDR SET " & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', TOT_BUDGET_AMOUNT=" & Val(lblTotalBudget.Text) & "," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblMkey.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1() = False Then GoTo ErrPart

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsSBHdr.Requery()
        RsSBDet.Requery()
        MsgBox(Err.Description)
        '    Resume	
    End Function
    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim I As Integer

        Dim mAccountName As String
        Dim mRemarks As String
        Dim mDate As String
        Dim mMonth As String
        Dim mTotal As Double

        SqlStr = " Delete From  MIS_EXPBUDGET_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblMkey.Text) & "' "

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColDate
                mDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColMonth
                mMonth = Trim(.Text)

                .Col = ColBudgetAmount
                mTotal = Val(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If Trim(lblMkey.Text) <> "" Then
                    SqlStr = " INSERT INTO MIS_EXPBUDGET_DET ( " & vbCrLf & " COMPANY_CODE, FYEAR, SUPP_CUST_CODE, " & vbCrLf & " SERIAL_NO, BUDGET_DATE, BUDGET_MONTH, BUDGET_AMOUNT, " & vbCrLf & " REMARKS) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMkey.Text) & "', " & vbCrLf _
                        & " " & I & ", TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mMonth & "'," & vbCrLf _
                        & " " & mTotal & ",'" & mRemarks & "') "

                    PubDBCn.Execute(SqlStr)
                End If
NextRow:
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume	
    End Function
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtName.Text = AcName
            If txtName.Enabled = True Then txtName.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh	
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsSBHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmExpenseBudget_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Expense Budget Entry"

        SqlStr = "Select * From MIS_EXPBUDGET_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSBHdr, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From MIS_EXPBUDGET_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSBDet, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)

        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf & " B.SUPP_CUST_NAME AS NAME, A.SUPP_CUST_CODE AS CODE, A.TOT_BUDGET_AMOUNT," & vbCrLf & " A.REMARKS " & vbCrLf & " FROM MIS_EXPBUDGET_HDR A, FIN_SUPP_CUST_MST B " & vbCrLf & " WHERE A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE " & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY B.SUPP_CUST_NAME"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmExpenseBudget_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	
        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear1()

        On Error GoTo ClearErr

        lblMkey.Text = ""
        txtName.Text = ""
        cmdsearch.Enabled = True
        txtName.Enabled = True
        SprdMain.Enabled = True
        lblTotalBudget.Text = "0.00"
        txtMonthAmount.Text = "0.00"
        txtRemarks.Text = ""

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FillSprdMain()
        FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsSBHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume	
    End Sub

    Private Sub FillSprdMain()
        On Error GoTo ERR1
        Dim cntRow As Integer
        Dim mDate As String
        Dim mMonth As Integer


        With SprdMain
            .MaxRows = 12

            For cntRow = 1 To 12
                .Row = cntRow

                If cntRow <= 9 Then
                    mMonth = cntRow + 3
                    mDate = VB6.Format("01/" & mMonth & "/" & RsCompany.Fields("FYEAR").Value, "DD/MM/YYYY")

                    .Col = ColDate
                    .Text = VB6.Format(mDate, "DD/MM/YYYY")

                    .Col = ColMonth
                    .Text = MonthName(mMonth)
                Else
                    mMonth = cntRow - 9
                    mDate = VB6.Format("01/" & mMonth & "/" & RsCompany.Fields("FYEAR").Value + 1, "DD/MM/YYYY")

                    .Col = ColDate
                    .Text = VB6.Format(mDate, "DD/MM/YYYY")

                    .Col = ColMonth
                    .Text = MonthName(mMonth)
                End If

            Next
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume	
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim cntCol As Integer

        With SprdMain
            .set_RowHeight(-1, ConRowHeight) ''* 1.3	
            .Row = Arow

            .Col = ColDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 10
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 12)
            .TypeEditMultiLine = False

            .Col = ColMonth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsSBDet.Fields("BUDGET_MONTH").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(.Col, 15)

            .Col = ColBudgetAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 15)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsSBDet.Fields("REMARKS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(.Col, 25)

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDate, ColMonth)
            MainClass.SetSpreadColor(SprdMain, Arow)
            '        .Col = ColAccountName	
            '        .UserColAction = UserColActionSort	
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 300)
            .set_ColWidth(0, 500)
            .set_ColWidth(1, 5000)
            .set_ColWidth(2, 1200)
            .set_ColWidth(3, 1500)
            .set_ColWidth(4, 2500)

            '        .ColsFrozen = 2	
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SetTextLengths()

        On Error GoTo ERR1

        txtName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtRemarks.MaxLength = RsSBHdr.Fields("REMARKS").DefinedSize

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume	
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mTotQty As Double
        Dim I As Integer
        Dim mItemCode As String

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsSBHdr.EOF = True Then Exit Function

        If Trim(txtName.Text) = "" Then
            MsgInformation("Account Name is Blank. Cannot Save")
            If txtName.Enabled = True Then txtName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblMkey.Text = Trim(MasterNo)
        Else
            MsgInformation("Invalid Expense Account Name. Cannot Save")
            If txtName.Enabled = True Then txtName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColMonth, "S", "Please Check Month Description.") = False Then FieldsVarification = False
        '    If MainClass.ValidDataInGrid(SprdMain, ColTotal, "N", "Please Check Amount.") = False Then FieldsVarification = False	

        CalcTots()
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume	
    End Function

    Private Sub frmExpenseBudget_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        RsSBHdr.Close()
        'RsOpOuts.Close	
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        Dim SqlStr As String = ""

        '    If eventArgs.row = 0 And eventArgs.col = ColAccountName Then	
        '        With SprdMain	
        '            .Row = .ActiveRow	
        '            .Col = ColAccountName	
        '	
        '            If MainClass.SearchGridMaster("", "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then	
        '                .Row = .ActiveRow	
        '	
        '                .Col = ColAccountName	
        '                .Text = Trim(AcName)	
        '            End If	
        '            Call SprdMain_LeaveCell(ColItemCode, .ActiveRow, ColAccountName, .ActiveRow, False)	
        '        End With	
        '    End If	
        '	
        '    If Col = 0 And Row > 0 And (ADDMode = True Or MODIFYMode = True) Then	
        '        MainClass.DeleteSprdRow SprdMain, Row, ColAccountName	
        '        MainClass.SaveStatus Me, ADDMode, MODIFYMode	
        '    End If	
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        'Dim mCol As Integer	
        '    mCol = SprdMain.ActiveCol	
        '    If KeyCode = vbKeyF1 And mCol = ColAccountName Then SprdMain_Click ColAccountName, 0	
        '    SprdMain.Refresh	
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim xAccountName As String

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            '         Case ColAccountName	
            '            SprdMain.Row = SprdMain.ActiveRow	
            '	
            '            SprdMain.Col = ColAccountName	
            '            xAccountName = SprdMain.Text	
            '            If xAccountName = "" Then Exit Sub	
            '	
            '            If GetValidAcount(xAccountName) = True Then	
            '                If CheckDuplicateItem(xAccountName) = False Then	
            '                    FormatSprdMain Row	
            '                    MainClass.AddBlankSprdRow SprdMain, ColAccountName, ConRowHeight	
            '                End If	
            '            Else	
            '                MainClass.SetFocusToCell SprdMain, Row, ColAccountName	
            '            End If	

        End Select
        CalcTots()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtName.Text = SprdView.Text

        txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    End Sub
    Private Sub CalcTots()
        On Error GoTo ErrSprdTotal
        Dim mTotBudgetAmount As Double
        Dim cntRow As Integer


        mTotBudgetAmount = 0
        For cntRow = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = cntRow

            SprdMain.Col = ColBudgetAmount
            mTotBudgetAmount = mTotBudgetAmount + Val(SprdMain.Value)

        Next
        lblTotalBudget.Text = VB6.Format(mTotBudgetAmount, "0.00")
        Exit Sub

ErrSprdTotal:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mAccountName As String

        Clear1()
        If Not RsSBHdr.EOF Then

            lblMkey.Text = Trim(IIf(IsDBNull(RsSBHdr.Fields("SUPP_CUST_CODE").Value), "", RsSBHdr.Fields("SUPP_CUST_CODE").Value))
            txtRemarks.Text = IIf(IsDBNull(RsSBHdr.Fields("REMARKS").Value), "", RsSBHdr.Fields("REMARKS").Value)

            If MainClass.ValidateWithMasterTable(lblMkey.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtName.Text = MasterNo
            End If

            Call ShowDetail1()
        End If

        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = False
        txtName.Enabled = False
        cmdsearch.Enabled = False
        MainClass.ButtonStatus(Me, XRIGHT, RsSBHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        '    Resume	
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mDate As String
        Dim mBudgetDate As String
        Dim mBudgetAmount As Double
        Dim mRemarks As String
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM MIS_EXPBUDGET_DET " & vbCrLf _
            & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR =" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblMkey.Text) & "'" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSBDet, ADODB.LockTypeEnum.adLockReadOnly)

        With RsSBDet
            If .EOF = True Then Exit Sub

            Do While Not .EOF
                mBudgetDate = IIf(IsDBNull(RsSBDet.Fields("BUDGET_DATE").Value), "", RsSBDet.Fields("BUDGET_DATE").Value)
                mBudgetAmount = IIf(IsDBNull(RsSBDet.Fields("BUDGET_AMOUNT").Value), 0, RsSBDet.Fields("BUDGET_AMOUNT").Value)
                mRemarks = Trim(IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value))

                With SprdMain
                    For I = 1 To .MaxRows
                        .Row = I
                        .Col = ColDate
                        mDate = VB6.Format(.Text, "DD/MM/YYYY")

                        If VB6.Format(mBudgetDate, "YYYYMM") = VB6.Format(mDate, "YYYYMM") Then
                            .Col = ColBudgetAmount
                            .Text = VB6.Format(mBudgetAmount, "0.00")

                            .Col = ColRemarks
                            .Text = Trim(mRemarks)
                            Exit For
                        End If
                    Next
                End With

                .MoveNext()
            Loop
        End With
        CalcTots()
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Sub

    Private Sub txtMonthAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMonthAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE NOT IN ('S','C')") = True Then
            lblMkey.Text = MasterNo
        Else
            MsgBox("Invalid Account Name.", vbInformation)
            Cancel = True
            Exit Sub
        End If

        If MODIFYMode = True And RsSBHdr.EOF = False Then lblMkey.Text = RsSBHdr.Fields("SUPP_CUST_CODE").Value

        SqlStr = "Select * From MIS_EXPBUDGET_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblMkey.Text) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSBHdr, ADODB.LockTypeEnum.adLockReadOnly)

        If RsSBHdr.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist, Click Add For New Entry", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From MIS_EXPBUDGET_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SUPP_CUST_CODE=" & lblMkey.Text & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSBHdr, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        '    mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(lblNetAmount.text) = 0, 0, lblNetAmount.text)))	
        '	
        '    MainClass.AssignCRptFormulas Report1, "AmountInWord=""" & mAmountInword & """"	
        '    MainClass.AssignCRptFormulas Report1, "NetAmount=""" & lblNetAmount.text & """"	

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub ReportOnSB(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String
        Dim Response As String
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""
        mSubTitle = ""
        Call MainClass.ClearCRptFormulas(Report1)

        mTitle = "Delivery Schedule"
        mRptFileName = "ExpBudget.rpt"

        ''    Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)	

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnSB(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnSB(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
