Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPayMachineMst
    Inherits System.Windows.Forms.Form
    Dim RsProjectMst As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Private PvtDBCn As ADODB.Connection	

    Dim xCode As Integer
    Dim FormActive As Boolean
    Dim Shw As Boolean
    Dim MasterNo As Object
    Dim SqlStr As String = ""
    Private Sub ViewGrid()

        On Error GoTo ErrorPart
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh	
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            FraGridView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraView.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsProjectMst, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        txtName.Text = ""

        MainClass.ButtonStatus(Me, XRIGHT, RsProjectMst, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsProjectMst, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
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
        ShowReportOld(Crystal.DestinationConstants.crptToWindow)
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
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "PAY_MACHINE_MST", (txtName.Text), RsProjectMst, "NAME") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "PAY_MACHINE_MST", "NAME", (txtName.Text)) = False Then GoTo DeleteErr

        SqlStr = "DELETE FROM PAY_MACHINE_MST " & vbCrLf _
            & "WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & "AND Name='" & MainClass.AllowSingleQuote(UCase((txtName.Text))) & "'"

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsProjectMst.Requery() ''.Refresh	
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans() ''	
        RsProjectMst.Requery() ''.Refresh	
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This Expense Head.", MsgBoxStyle.Information)
            Exit Function
        End If
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsProjectMst.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.	
                If Delete1() = False Then GoTo DelErrPart
                If RsProjectMst.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        ErrorMsg("Record Not Deleted", "DELETE", MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo SearchError
        If MainClass.SearchGridMaster(txtName.Text, "PAY_MACHINE_MST", "NAME", "CODE", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtName.Text = AcName
            TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
SearchError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmPayMachineMst_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmPayMachineMst_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtName.Text = Trim(SprdView.Text)
        TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub TxtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        SqlStr = ""
        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsProjectMst.EOF = False Then xCode = RsProjectMst.Fields("CODE").Value

        SqlStr = "SELECT * FROM PAY_MACHINE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND NAME='" & MainClass.AllowSingleQuote(UCase((Trim(txtName.Text)))) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProjectMst, ADODB.LockTypeEnum.adLockReadOnly)

        If RsProjectMst.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Machine Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "SELECT * FROM PAY_MACHINE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CODE=" & xCode & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProjectMst, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub frmPayMachineMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From PAY_MACHINE_MST Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProjectMst, ADODB.LockTypeEnum.adLockReadOnly)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        AssignGrid(False)
        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmPayMachineMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ''Set PvtDBCn = New ADODB.Connection	
        ''PvtDBCn.Open StrConn	

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CmdView.Text = ConCmdGridViewCaption
        ADDMode = False
        MODIFYMode = False
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(5220)
        'Me.Width = VB6.TwipsToPixelsX(8265)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmPayMachineMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsProjectMst = Nothing
        RsProjectMst.Close()
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        If Not RsProjectMst.EOF Then

            txtName.Text = IIf(IsDBNull(RsProjectMst.Fields("Name").Value), "", RsProjectMst.Fields("Name").Value)

            xCode = RsProjectMst.Fields("Code").Value
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsProjectMst, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        Resume
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
        Dim mCode As Integer
        Dim mSalesPostCode As String
        Dim Identification As String
        Dim mSalesTaxCode As Integer
        Dim mStatus As String '***	
        Dim mWef As Date '**	

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xCompanyCode As Long


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        Else
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                If ADDMode = True Then
                    mCode = AutoGenSeqNo()        ''MainClass.AutoGenRowNo("PAY_MACHINE_MST", "CODE", PubDBCn)
                    SqlStr = "INSERT INTO PAY_MACHINE_MST (" & vbCrLf _
                        & " COMPANY_CODE, CODE, NAME) VALUES ( " & vbCrLf _
                        & " " & xCompanyCode & ", " & mCode & ", '" & MainClass.AllowSingleQuote(txtName.Text) & "'" & vbCrLf _
                        & " )"

                Else
                    SqlStr = " UPDATE PAY_MACHINE_MST  SET " & vbCrLf _
                        & " NAME='" & MainClass.AllowSingleQuote(txtName.Text) & "'" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & xCompanyCode & "" & vbCrLf _
                        & " AND CODE= " & xCode & ""
                End If
UpdatePart:
                PubDBCn.Execute(SqlStr)
                RsTemp.MoveNext()
            Loop
        End If

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans() ''	
        RsProjectMst.Requery() ''.Refresh	
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function AutoGenSeqNo() As Double

        On Error GoTo AutoGenSeqNoErr
        Dim RsMRRMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(CODE)  " & vbCrLf _
            & " FROM PAY_MACHINE_MST " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMRRMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mNewSeqNo = .Fields(0).Value
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With
        AutoGenSeqNo = CDbl(mNewSeqNo)
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtName.MaxLength = RsProjectMst.Fields("Name").DefinedSize ''	
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If Trim(txtName.Text) = "" Then
            MsgInformation(" Name is empty. Cannot Save")
            txtName.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsProjectMst.EOF = True Then Exit Function
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

        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT NAME " & vbCrLf _
            & " FROM PAY_MACHINE_MST" & vbCrLf _
            & " WHERE PAY_MACHINE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 20)
            .set_ColWidth(2, 12)
            .set_ColWidth(3, 12)
            .set_ColWidth(4, 12)
            .set_ColWidth(5, 12)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub ShowReportOld(ByRef Mode As Crystal.DestinationConstants)
        Dim mTitle As String
        On Error GoTo ERR1
        mTitle = ""
        Report1.Reset()
        mTitle = "Machine Master"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PayMachineMaster.rpt"
        SetCrpt(Report1, Mode, 1, mTitle)

        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.ReportFileName = ""

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & "PayMachineMaster.rpt"
        ' Name from label on sample form	

        ' Discard saved data?	
        '    If MsgBox("Do you wish to discard any saved data?", vbYesNo + vbQuestion, "Discard Saved Data?") = vbYes Then	
        Report1.DiscardSavedData = 1
        '    End If	

        ' Display progress dialog?	
        If MsgBox("Do you want to see the progress dialog?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Display Progress Dialog?") = MsgBoxResult.Yes Then
            Report1.ProgressDialog = True
        Else
            Report1.ProgressDialog = False
        End If

        Report1.Destination = Mode

        ' Display Windows printer selection dialog	
        Report1.PrinterSelect()

        ' Print	
        Report1.Action = 1

        MsgBox("Print Complete!", MsgBoxStyle.OKOnly, "Operation Completed")


        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
End Class
