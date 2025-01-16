Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmITPersonal
    Inherits System.Windows.Forms.Form
    Dim RsITRate As ADODB.Recordset

    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection	

    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const mITType As String = "IT"

    Private Const ColMin As Short = 1
    Private Const ColMax As Short = 2
    Private Const ColTax As Short = 3
    Private Const ColFixedAmt As Short = 4
    Private Sub Clear1()

        MainClass.ClearGrid(sprdITRate)
    End Sub

    Private Sub cboSex_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSex.TextChanged
        Show1()
    End Sub

    Private Sub cboSex_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSex.SelectedIndexChanged
        Show1()
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
            If Delete1 = False Then GoTo DelErrPart
            Clear1()
        End If

        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub frmITPersonal_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub sprdITRate_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdITRate.Change
        CmdSave.Enabled = True
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

    Private Sub sprdITRate_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdITRate.LeaveCell

        On Error GoTo ErrPart
        Dim mMaxAmount As Double
        Dim mMinAmount As Double
        Dim mTax As Double

        If eventArgs.NewRow = -1 Then Exit Sub
        sprdITRate.Row = eventArgs.row
        If eventArgs.col = ColMax Or eventArgs.col = ColMin Then
            If eventArgs.row > 1 Then
                sprdITRate.Row = eventArgs.row - 1
                sprdITRate.Col = ColMin
                mMinAmount = Val(sprdITRate.Text)
                sprdITRate.Col = ColMax
                mMaxAmount = Val(sprdITRate.Text)
                sprdITRate.Col = ColTax
                mTax = Val(sprdITRate.Text)
                sprdITRate.Row = eventArgs.row
                sprdITRate.Col = ColMin
                If Val(sprdITRate.Text) <= mMaxAmount And Val(sprdITRate.Text) <> 0 Then
                    MsgInformation("Please enter the vaild Value.")
                    MainClass.SetFocusToCell(sprdITRate, eventArgs.row, ColMin)
                    Exit Sub
                End If
            End If

            If CheckMinMaxLimit(eventArgs.row, eventArgs.col) = True Then
                CalcTaxAmount(mMinAmount, mTax, eventArgs.row)
                MainClass.AddBlankSprdRow(sprdITRate, ColMin)
            End If
        End If
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        'Resume	
    End Sub
    Private Sub frmITPersonal_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        SqlStr = ""
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        txtAYear.Text = Year(RsCompany.Fields("START_DATE").Value) & "-" & Year(RsCompany.Fields("END_DATE").Value)
        Clear1()
        Show1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmITPersonal_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        'Me.Height = VB6.TwipsToPixelsY(4695)
        'Me.Width = VB6.TwipsToPixelsX(6315)
        'Me.Left = VB6.TwipsToPixelsX(20)
        'Me.Top = VB6.TwipsToPixelsY(20)

        cboSex.Items.Clear()
        cboSex.Items.Add("Male")
        cboSex.Items.Add("Female")
        cboSex.Items.Add("Sr. Citizen")

        cboSex.SelectedIndex = Val(lblType.Text)
        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmITPersonal_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Cancel	
        'PvtDBCn.Close	
        RsITRate = Nothing
        'Set PvtDBCn = Nothing	
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim cntRow As Integer

        If Trim(cboSex.Text) = "" Then Exit Sub

        SqlStr = " SELECT *  FROM PAY_ITRATE_MST WHERE " & vbCrLf & " Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR = " & RsCompany.Fields("FYEAR").Value & " AND ITTYPE = '" & mITType & "'"

        SqlStr = SqlStr & vbCrLf & " AND SEX='" & VB.Left(cboSex.Text, 1) & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY SUBROWNO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITRate, ADODB.LockTypeEnum.adLockOptimistic)

        If RsITRate.EOF = False Then
            With RsITRate
                txtAYear.Text = .Fields("AYEAR").Value
                cboSex.SelectedIndex = IIf(.Fields("SEX").Value = "M", 0, IIf(.Fields("SEX").Value = "F", 1, 2))
                cntRow = 1
                Do While Not RsITRate.EOF
                    sprdITRate.Row = cntRow

                    sprdITRate.Col = ColMin
                    sprdITRate.Text = CStr(.Fields("MINLIMIT").Value)

                    sprdITRate.Col = ColMax
                    sprdITRate.Text = CStr(.Fields("MAXLIMIT").Value)

                    sprdITRate.Col = ColTax
                    sprdITRate.Text = CStr(.Fields("TAXPER").Value)

                    sprdITRate.Col = ColFixedAmt
                    sprdITRate.Text = CStr(.Fields("FixedAmt").Value)

                    cntRow = cntRow + 1
                    RsITRate.MoveNext()
                    sprdITRate.MaxRows = cntRow
                Loop
            End With
        End If
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If Update1 = True Then
            CmdSave.Enabled = False
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
        Dim cntRow As Integer
        Dim mMax As Double
        Dim mMin As Double
        Dim mTax As Double
        Dim mSurcharge As Double
        Dim mFixedAmt As Double
        Dim mCompanyCode As Integer
        Dim RsTemp As ADODB.Recordset = Nothing

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mCompanyCode = IIf(IsDbNull(RsTemp.Fields("COMPANY_CODE").Value), -1, RsTemp.Fields("COMPANY_CODE").Value)

                SqlStr = " DELETE FROM PAY_ITRATE_MST WHERE " & vbCrLf & " Company_Code=" & mCompanyCode & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND ITTYPE='" & mITType & "' " & vbCrLf & " AND SEX='" & VB.Left(cboSex.Text, 1) & "'"

                PubDBCn.Execute(SqlStr)

                With sprdITRate
                    For cntRow = 1 To .MaxRows - 1
                        .Row = cntRow

                        .Col = ColMin
                        mMin = IIf(IsNumeric(.Text), .Text, 0)

                        .Col = ColMax
                        mMax = IIf(IsNumeric(.Text), .Text, 0)

                        .Col = ColTax
                        mTax = IIf(IsNumeric(.Text), .Text, 0)

                        .Col = ColFixedAmt
                        mFixedAmt = IIf(IsNumeric(.Text), .Text, 0)


                        SqlStr = " INSERT INTO PAY_ITRATE_MST " & vbCrLf & " ( COMPANY_CODE , FYEAR, SUBROWNO, AYEAR, " & vbCrLf & " MINLIMIT , MAXLIMIT, TAXPER, FIXEDAMT, ITTYPE, SEX, " & vbCrLf & " ADDUSER, ADDDATE )  VALUES " & vbCrLf & " (" & mCompanyCode & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & cntRow & ", '" & MainClass.AllowSingleQuote(txtAYear.Text) & "', " & vbCrLf & " " & mMin & "," & mMax & ", " & vbCrLf & " " & mTax & "," & mFixedAmt & ", '" & mITType & "', '" & VB.Left(cboSex.Text, 1) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                        PubDBCn.Execute(SqlStr)
                    Next
                End With

                RsTemp.MoveNext()
            Loop
        End If
        PubDBCn.CommitTrans()
        RsITRate.Requery()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsITRate.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Modify Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        'Resume	
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr

        SqlStr = ""
        SqlStr = " DELETE FROM PAY_ITRATE_MST WHERE " & vbCrLf & " Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR = " & RsCompany.Fields("FYEAR").Value & " AND ITTYPE='" & mITType & "' " & vbCrLf & " AND SEX='" & VB.Left(cboSex.Text, 1) & "'"

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsITRate.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsITRate.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This Employee.")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        With sprdITRate
            .set_RowHeight(0, ConRowHeight * 1.5)
            .Row = mRow
            .MaxCols = ColFixedAmt
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColMin
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColMin, 16)


            .Col = ColMax
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColMin, 16)

            .Col = ColTax
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColTax, 15)

            .Col = ColFixedAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColFixedAmt, 10)
            .ColHidden = True

        End With
        MainClass.UnProtectCell(sprdITRate, 1, sprdITRate.MaxRows, ColMin, ColFixedAmt)
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

    Private Sub CalcTaxAmount(ByRef mMinAmount As Double, ByRef mTax As Double, ByRef mRow As Integer)

        Dim mFixedAmount As Double
        Dim mAddFixedAmount As Double
        Dim cntRow As Integer
        With sprdITRate
            .Row = mRow
            .Col = ColMin
            mFixedAmount = Val(.Text) - mMinAmount
            For cntRow = 1 To mRow - 1
                .Row = cntRow
                .Col = ColFixedAmt
                mAddFixedAmount = mAddFixedAmount + Val(.Text)
            Next
            .Row = mRow
            .Col = ColFixedAmt
            .Text = CStr((mFixedAmount * mTax / 100) + mAddFixedAmount)
        End With
    End Sub
End Class
