Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSamplePlanAtRI
    Inherits System.Windows.Forms.Form
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As Connection
    Dim RsSample As ADODB.Recordset

    Dim SqlStr As String
    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColMin As Short = 1
    Private Const ColMax As Short = 2
    Private Const ColDC As Short = 3
    Private Const ColVC As Short = 4

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchMaster(txtWEF.Text, "QAL_SAMPLE_MST", "WEFDATE", "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            txtWEF.Text = AcName
            txtWEF_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtWEF.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsSample.EOF = False Then RsSample.MoveFirst()
            If RsSample.EOF = False Then
                txtWEF.Text = VB6.Format(RsSample.Fields("WEFDATE").Value, "DD/MM/YYYY")
                Show1()
            End If
        End If
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsSample, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        'ShowReport crptToPrinter
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtWEF.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsSample.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1 = False Then GoTo DelErrPart
                If RsSample.EOF = 0 Then
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
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtWEF_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(False))
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

    Private Sub frmSamplePlanAtRI_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        If FormActive = True Then Exit Sub

        SqlStr = "Select * From QAL_SAMPLE_MST WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSample, ADODB.LockTypeEnum.adLockReadOnly)
        FormActive = True
    End Sub
    Private Sub frmSamplePlanAtRI_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmSamplePlanAtRI_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Height = VB6.TwipsToPixelsY(4170)
        Me.Width = VB6.TwipsToPixelsX(7080)
        Me.Left = 0
        Me.Top = 0
        FormatSprd(-1)
        AssignGrid(False)
        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim cntRow As Integer
        Dim mMax As Double
        Dim mMin As Double
        Dim mDC As Double
        Dim mVC As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = " DELETE FROM QAL_SAMPLE_MST WHERE " & vbCrLf & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " WEFDATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        With sprdITRate
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColMin
                mMin = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColMax
                mMax = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColDC
                mDC = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColVC
                mVC = IIf(IsNumeric(.Text), .Text, 0)

                SqlStr = " INSERT INTO QAL_SAMPLE_MST " & vbCrLf _
                                    & " ( COMPANY_CODE , WEFDATE, SUBROWNO, " & vbCrLf _
                                    & " MINLIMIT , MAXLIMIT, DIM_CHECK, VISUAL_CHECK, " & vbCrLf _
                                    & " ADDUSER, ADDDATE )  VALUES " & vbCrLf _
                                    & " (" & RsCompany.Fields("COMPANY_CODE").Value & ",TO_DATE('" & vb6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                                    & " " & cntRow & ", " & vbCrLf _
                                    & " " & mMin & "," & mMax & ", " & vbCrLf _
                                    & " " & mDC & "," & mVC & ", " & vbCrLf _
                                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


                PubDBCn.Execute(SqlStr)
            Next
        End With


        PubDBCn.CommitTrans()
        RsSample.Requery()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsSample.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Modify Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        'Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Function

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim cntRow As Integer

        If Trim(txtWEF.Text) = "" Then Exit Sub

        '    SqlStr = " SELECT *  FROM QAL_SAMPLE_MST WHERE " & vbCrLf _
        ''            & " Company_Code = " & RsCompany.fields("COMPANY_CODE").value & " AND " & vbCrLf _
        ''            & " WEFDATE = '" & vb6.Format(txtWEF.Text, "DD-MMM-YYYY") & "'"
        '
        '    SqlStr = SqlStr & vbCrLf & " ORDER BY SUBROWNO "
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsSample, adLockOptimistic

        If RsSample.EOF = False Then
            With RsSample
                txtWEF.Text = VB6.Format(.Fields("WEFDATE").Value, "DD/MM/YYYY")
                cntRow = 1
                Do While Not RsSample.EOF
                    sprdITRate.Row = cntRow

                    sprdITRate.Col = ColMin
                    sprdITRate.Text = CStr(.Fields("MINLIMIT").Value)

                    sprdITRate.Col = ColMax
                    sprdITRate.Text = CStr(.Fields("MAXLIMIT").Value)

                    sprdITRate.Col = ColDC
                    sprdITRate.Text = CStr(.Fields("DIM_CHECK").Value)

                    sprdITRate.Col = ColVC
                    sprdITRate.Text = CStr(.Fields("VISUAL_CHECK").Value)

                    cntRow = cntRow + 1
                    RsSample.MoveNext()
                    sprdITRate.MaxRows = cntRow
                Loop
            End With
            RsSample.MoveFirst()
        End If
        FormatSprd(-1)

        ADDMode = False
        MODIFYMode = False
        txtWEF.Enabled = False
        MainClass.ButtonStatus(Me, XRIGHT, RsSample, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        FieldsVarification = True
        If Not IsDate(txtWEF.Text) Then
            MsgInformation("Please enter the Vaild Date.")
            txtWEF.Focus()
            FieldsVarification = False
            Exit Function
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
    End Function

    Private Sub sprdITRate_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdITRate.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub sprdITRate_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles sprdITRate.ClickEvent
        sprdITRate.Row = eventArgs.row
        If eventArgs.Col = 0 Then
            If MsgQuestion("Are sure to delete the row? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                sprdITRate.Action = SS_ACTION_DELETE_ROW
                sprdITRate.MaxRows = sprdITRate.MaxRows - 1
            End If
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        SqlStr = ""
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtWEF.Text = VB6.Format(SprdView.Text, "DD/MM/YYYY")
        SqlStr = " SELECT * from QAL_SAMPLE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND WEFDATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSample, ADODB.LockTypeEnum.adLockOptimistic)
        If RsSample.EOF = False Then
            '        Show1
            txtWEF_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(False))
            CmdView_Click(CmdView, New System.EventArgs())
        End If
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub sprdITRate_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdITRate.LeaveCell

        On Error GoTo ErrPart
        Dim mMaxAmount As Double
        Dim mMinAmount As Double


        If eventArgs.NewRow = -1 Then Exit Sub
        sprdITRate.Row = eventArgs.row
        If eventArgs.col = ColMax Or eventArgs.col = ColMin Then
            If eventArgs.row > 1 Then
                sprdITRate.Row = eventArgs.row - 1
                sprdITRate.Col = ColMin
                mMinAmount = Val(sprdITRate.Text)
                sprdITRate.Col = ColMax
                mMaxAmount = Val(sprdITRate.Text)

                sprdITRate.Row = eventArgs.row
                sprdITRate.Col = ColMin

                If Val(sprdITRate.Text) <= mMaxAmount And Val(sprdITRate.Text) <> 0 Then
                    MsgInformation("Please enter the vaild Value.")
                    MainClass.SetFocusToCell(sprdITRate, eventArgs.row, ColMin)
                    Exit Sub
                End If
            End If

            If CheckMinMaxLimit(eventArgs.row, eventArgs.col) = True Then
                MainClass.AddBlankSprdRow(sprdITRate, ColMin)
            End If
        End If
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        With sprdITRate
            .set_RowHeight(0, ConRowHeight * 1.5)
            .Row = mRow
            .MaxCols = ColVC
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColMin
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999.99")
            .TypeFloatMin = CDbl("-99999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColMin, 12)


            .Col = ColMax
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999.99")
            .TypeFloatMin = CDbl("-99999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColMin, 12)

            .Col = ColDC
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDC, 12)

            .Col = ColVC
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColVC, 12)

        End With
        MainClass.UnProtectCell(sprdITRate, 1, sprdITRate.MaxRows, ColMin, ColVC)
        MainClass.SetSpreadColor(sprdITRate, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Function CheckMinMaxLimit(ByRef mRow As Integer, ByRef mCol As Integer) As Boolean
        CheckMinMaxLimit = False
        With sprdITRate
            .Row = mRow
            .Col = ColMin
            If Val(.Text) > 0 Then
                .Col = ColMax
                If Val(.Text) > 0 Then
                    CheckMinMaxLimit = True
                End If
            End If
        End With
    End Function
    Private Sub txtWEF_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWEF_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim mDate As String
        Dim xDate As String

        Dim SqlStr As String
        If txtWEF.Text = "" Then GoTo EventExitSub
        If Not IsDate(txtWEF.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        End If
        txtWEF.Text = VB6.Format(txtWEF.Text, "dd/mm/yyyy")
        mDate = txtWEF.Text

        If MODIFYMode = True And RsSample.EOF = False Then xDate = RsSample.Fields("WEFDATE").Value

        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_SAMPLE_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND  WEFDATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "

        SqlStr = SqlStr & vbCrLf & " ORDER BY SUBROWNO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSample, ADODB.LockTypeEnum.adLockOptimistic)

        If RsSample.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            txtWEF.Text = VB6.Format(mDate, "dd/mm/yyyy")
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("WEF Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM  QAL_SAMPLE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND  WEFDATE=TO_DATE('" & VB6.Format(xDate, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSample, ADODB.LockTypeEnum.adLockOptimistic)
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub Clear1()

        txtWEF.Text = ""
        txtWEF.Enabled = True
        MainClass.ClearGrid(sprdITRate)
        MainClass.ButtonStatus(Me, XRIGHT, RsSample, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub ViewGrid()

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
            FraGridView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsSample, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)


        SqlStr = "Select DISTINCT TO_CHAR(WEFDATE,'DD/MM/YYYY') AS WEF from QAL_SAMPLE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by wef"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "QAL_SAMPLE_MST", (txtWEF.Text), RsSample) = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "QAL_SAMPLE_MST", "WEFDATE", VB6.Format(txtWEF.Text, "DD-MMM-YYYY")) = False Then GoTo DeleteErr

        SqlStr = "Delete from QAL_SAMPLE_MST where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND WEFDATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsSample.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        MsgBox(Err.Description)
        '     Resume
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsSample.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against this Code")
            Exit Function
        End If

    End Function
End Class
