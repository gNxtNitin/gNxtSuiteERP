Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCostingExp
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection	
    Dim RsCosExpMst As ADODB.Recordset
    Dim RsCosExpDetail As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Private Const ConRowHeight As Short = 14

    Dim FormActive As Boolean
    Dim Shw As Boolean
    Dim MasterNo As Object

    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh	
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsCosExpMst, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        Dim I As Integer

        txtName.Text = ""
        TxtDefaultPer.Text = ""
        txtSeq.Text = ""
        optAdd_Deduct(0).Checked = True

        For I = 0 To 9
            chkCalcOn(I).CheckState = System.Windows.Forms.CheckState.Unchecked
        Next

        MainClass.ButtonStatus(Me, XRIGHT, RsCosExpMst, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub chkCalcOn_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCalcOn.CheckStateChanged
        Dim Index As Short = chkCalcOn.GetIndex(eventSender)

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsCosExpMst, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo ERR1
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtName.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        Dim SqlStr As String
        SqlStr = ""
        If txtName.Text = "" Then MsgInformation("Nothing to Delete") : Exit Function
        If Not RsCosExpMst.EOF Then
            '        If MsgQuestion("Want to Delete ? ") = vbYes Then	
            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()
            If InsertIntoDelAudit(PubDBCn, "PRD_COSTINGEXP_MST", (txtName.Text), RsCosExpMst) = False Then GoTo DeleteErr
            If InsertIntoDeleteTrn(PubDBCn, "PRD_COSTINGEXP_MST", "CODE", (TxtCode.Text)) = False Then GoTo DeleteErr

            SqlStr = "DELETE FROM PRD_COSTINGEXP_MST " & vbCrLf & " WHERE Code=" & Val(TxtCode.Text) & "" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='" & lblCategory.Text & "'"
            PubDBCn.Execute(SqlStr)

            PubDBCn.CommitTrans()
            RsCosExpMst.Requery()
            Clear1()
            '        End If	
        End If
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsCosExpMst.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This Expense Head.", MsgBoxStyle.Information)
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsCosExpMst.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                If Delete1() = False Then GoTo DelErrPart
                If RsCosExpMst.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo SearchError
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='" & lblCategory.Text & "'"
        If MainClass.SearchGridMaster(txtName.Text, "PRD_COSTINGEXP_MST", "NAME", "", "", "", SqlStr) = True Then
            txtName.Text = AcName
            TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
SearchError:
        MsgBox(Err.Description)
    End Sub
    Private Sub frmCostingExp_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmCostingExp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub optAdd_Deduct_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optAdd_Deduct.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optAdd_Deduct.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        On Error GoTo ErrPart

        SprdView.Row = SprdView.ActiveRow
        SprdView.Col = 1
        txtName.Text = SprdView.Text
        Call TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
        ViewGrid()
        Exit Sub

ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub TxtDefaultPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtDefaultPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub TxtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = Asc(UCase(Chr(KeyAscii)))
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim xCode As Integer

        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsCosExpMst.EOF = False Then xCode = RsCosExpMst.Fields("Code").Value
        SqlStr = "Select * from PRD_COSTINGEXP_MST " & vbCrLf & " Where Upper(Name)='" & MainClass.AllowSingleQuote(UCase(RTrim(LTrim(txtName.Text)))) & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='" & lblCategory.Text & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCosExpMst, ADODB.LockTypeEnum.adLockReadOnly)
        If RsCosExpMst.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Costing Exp. Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * from PRD_COSTINGEXP_MST " & vbCrLf & " Where Code=" & xCode & " " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='" & lblCategory.Text & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCosExpMst, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub frmCostingExp_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.UOpenRecordSet("Select * From PRD_COSTINGEXP_MST Where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCosExpMst, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)
        Call SetTextLengths()
        Call Clear1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmCostingExp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Left = VB6.TwipsToPixelsX(25)
        Me.Top = VB6.TwipsToPixelsY(25)
        Me.Height = VB6.TwipsToPixelsY(4245)
        Me.Width = VB6.TwipsToPixelsX(8220)

        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CmdView.Text = ConCmdGridViewCaption

        ADDMode = False
        MODIFYMode = False

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmCostingExp_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsCosExpMst = Nothing

        'PvtDBCn.Close	
        'Set PvtDBCn = Nothing	
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mStrFound As String
        Dim mCalculation As String
        Dim I As Integer

        If Not RsCosExpMst.EOF Then
            TxtCode.Text = RsCosExpMst.Fields("Code").Value
            txtName.Text = IIf(IsDBNull(RsCosExpMst.Fields("Name").Value), "", RsCosExpMst.Fields("Name").Value)
            TxtDefaultPer.Text = IIf(IsDBNull(RsCosExpMst.Fields("DefaultPercent").Value), "", RsCosExpMst.Fields("DefaultPercent").Value)
            mCalculation = IIf(IsDBNull(RsCosExpMst.Fields("CALCULATION").Value), "", RsCosExpMst.Fields("CALCULATION").Value)

            txtSeq.Text = IIf(IsDBNull(RsCosExpMst.Fields("PRINTSEQUENCE").Value), "", RsCosExpMst.Fields("PRINTSEQUENCE").Value)
            optAdd_Deduct(0).Checked = IIf(RsCosExpMst.Fields("Add_Ded").Value = "A", True, False)

            For I = 0 To 9
                '            mStrFound = InStr(1, mCalculation, I & ",")	
                If InStr(1, mCalculation, I & ",") > 0 Then
                    chkCalcOn(I).CheckState = System.Windows.Forms.CheckState.Checked
                End If
            Next

        End If
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsCosExpMst, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
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
            TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo UpdateError
        Dim SqlStr As String
        Dim mCode As Double
        Dim mCalculation As String
        Dim I As Integer
        Dim mAdd_Deduct As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        mAdd_Deduct = IIf(optAdd_Deduct(0).Checked = True, "A", "D")

        mCalculation = ""

        For I = 0 To 9
            If chkCalcOn(I).CheckState = System.Windows.Forms.CheckState.Checked Then
                mCalculation = mCalculation & Str(I) & ","
            End If
        Next
        If ADDMode = True Then
            mCode = MainClass.AutoGenRowNo("PRD_COSTINGEXP_MST", "CODE", PubDBCn)
            TxtCode.Text = CStr(mCode)
            SqlStr = "Insert Into PRD_COSTINGEXP_MST (COMPANY_CODE,Code,Name, CATEGORY," & vbCrLf _
                & " DefaultPercent,Calculation,PRINTSEQUENCE,Add_Ded," & vbCrLf _
                & " AddUser,AddDate,ModUser,ModDate,IDENTIFICATION)" & vbCrLf _
                & " Values (" & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(mCode) & ", '" & MainClass.AllowSingleQuote(txtName.Text) & "', '" & Trim(lblCategory.Text) & "'," & vbCrLf _
                & " " & Val(TxtDefaultPer.Text) & ",'" & mCalculation & "', " & Val(txtSeq.Text) & ", '" & Trim(mAdd_Deduct) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','') "
        Else
            SqlStr = "Update PRD_COSTINGEXP_MST Set " & vbCrLf & " Name='" & MainClass.AllowSingleQuote(txtName.Text) & "',CATEGORY='" & lblCategory.Text & "'," & " DefaultPercent=" & Val(TxtDefaultPer.Text) & "," & " Calculation='" & mCalculation & "',PRINTSEQUENCE=" & Val(txtSeq.Text) & ",Add_Ded='" & mAdd_Deduct & "'," & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',ModDate=TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & " WHERE Code= " & Val(TxtCode.Text) & " " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        End If
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsCosExpMst.Requery()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtName.MaxLength = RsCosExpMst.Fields("Name").DefinedSize
        TxtDefaultPer.MaxLength = RsCosExpMst.Fields("DefaultPercent").Precision - 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub TxtDefaultPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtDefaultPer.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsCosExpMst.EOF = True Then FieldsVarification = False : Exit Function
        If Trim(txtName.Text) = "" Then
            MsgInformation(" Name is empty. Cannot Save")
            txtName.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(TxtDefaultPer.Text) <> "" Then
            If InStr(1, Trim(TxtDefaultPer.Text), ".", CompareMethod.Text) <> 3 Then
                If Len(Trim(TxtDefaultPer.Text)) > 2 And InStr(1, Trim(TxtDefaultPer.Text), ".", CompareMethod.Text) = 0 Or Len(Trim(TxtDefaultPer.Text)) > 2 And InStr(1, Trim(TxtDefaultPer.Text), ".", CompareMethod.Text) > 2 Then
                    MsgInformation("Default can not be more than 99.99%. ")
                    TxtDefaultPer.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub TxtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String

        SqlStr = "SELECT Name,DefaultPercent " & vbCrLf & " FROM PRD_COSTINGEXP_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='" & lblCategory.Text & "'" & vbCrLf & " ORDER BY Name"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 28)
            .set_ColWidth(2, 10)
            .set_ColWidth(3, 12)
            .set_ColWidth(4, 12)
            .set_ColWidth(5, 12)

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        Dim mTitle As String
        On Error GoTo ERR1
        mTitle = ""
        Report1.Reset()
        mTitle = "Costing Exp"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\CostingExp.rpt"
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtSeq_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSeq.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
End Class
