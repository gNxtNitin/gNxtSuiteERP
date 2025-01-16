Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSaleBudgetMonthlyDetail
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection
    Dim FormLoaded As Boolean
    Private Const ConRowHeight As Short = 11

    Private Const ColMonthName As Short = 1
    Private Const ColQty As Short = 2
    Private Const ColRate As Short = 3
    Private Const ColValue As Short = 4

    Public Sub FormatSprdDlv(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdDlv
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColMonthName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999.999")
            .TypeFloatMin = CDbl("-99999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.9999")
            .TypeFloatMin = CDbl("-9999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColValue
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            MainClass.ProtectCell(SprdDlv, 1, .MaxRows, ColMonthName, ColMonthName)
            MainClass.ProtectCell(SprdDlv, 1, .MaxRows, ColValue, ColValue)

        End With
        MainClass.SetSpreadColor(SprdDlv, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        ConBudgetDailyDetail = False
        Me.hide()
    End Sub

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        If UCase(LblAddMode.Text) = UCase("False") And UCase(LblModifyMode.Text) = UCase("False") Then Exit Sub
        If InsertIntoTemp_Table = True Then
            ConBudgetDailyDetail = True
            Me.Hide()
            '' Unload Me
        Else
            ConBudgetDailyDetail = False
            MsgBox("Can Not Save Sale Budget Monthly Detail", MsgBoxStyle.Critical)
            cmdOk.Enabled = True
        End If
    End Sub

    Private Sub frmSaleBudgetMonthlyDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If FormLoaded = True Then Exit Sub
        If UCase(LblAddMode.Text) = UCase("False") And UCase(LblModifyMode.Text) = UCase("False") Then
            cmdOk.Enabled = False
        Else
            cmdOk.Enabled = True
        End If

        Call ShowSBMonthlyDetail()
        FormLoaded = True

    End Sub

    Private Sub frmSaleBudgetMonthlyDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo LoadPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        MainClass.SetControlsColor(Me)

        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
        MainClass.ClearGrid(SprdDlv)
        FormatSprdDlv(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LoadPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Sub frmSaleBudgetMonthlyDetail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormLoaded = False
        LblAddMode.Text = ""
        LblModifyMode.Text = ""
        lblNumber.Text = ""
        lblSuppCode.Text = ""
        lblItemCode.Text = ""
        lblMainActiveRow.Text = ""
        lblRate.Text = ""
        lblUOM.Text = ""
    End Sub

    Private Sub ShowSBMonthlyDetail()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mLastDay As Integer
        Dim pSDate As String
        Dim mQty As Double
        Dim mRate As Double

        MainClass.ClearGrid(SprdDlv)
        FormatSprdDlv(-1)
        SqlStr = "SELECT * FROM TEMP_MIS_SALEBUDGET_TRN " & vbCrLf & " WHERE USER_ID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND AUTO_KEY_NO =" & Val(lblNumber.Text) & "" & vbCrLf & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(lblItemCode.Text) & "'" & vbCrLf & " ORDER BY SUB_SERIAL_NO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                I = I + 1

                With SprdDlv
                    .Row = I

                    .Col = ColMonthName
                    .Text = IIf(IsDbNull(RsTemp.Fields("MONTH_NAME").Value), lblItemCode.Text, RsTemp.Fields("MONTH_NAME").Value)

                    .Col = ColQty
                    .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("QTY").Value), "", RsTemp.Fields("QTY").Value)))
                    mQty = Val(IIf(IsDbNull(RsTemp.Fields("QTY").Value), "", RsTemp.Fields("QTY").Value))

                    .Col = ColRate
                    .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("RATE").Value), "", RsTemp.Fields("RATE").Value)))
                    If Val(.Text) = 0 Then
                        .Text = VB6.Format(lblRate.Text, "0.0000")
                    End If
                    mRate = Val(.Text)

                    .Col = ColValue
                    .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("VALUE").Value), "", RsTemp.Fields("VALUE").Value)))
                    If Val(.Text) = 0 Then
                        .Text = VB6.Format(mQty * mRate, "0.0000")
                    End If
                End With

                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    SprdDlv.MaxRows = I + 1
                End If
            Loop
        Else
            With SprdDlv
                .MaxRows = 12

                For I = 1 To .MaxRows
                    .Row = I

                    .Col = 1
                    .Text = MonthName(IIf(I <= 9, I + 3, I + 3 - 12))

                    .Col = 2
                    .Text = "0.000"

                    .Col = 3
                    .Text = VB6.Format(lblRate.Text, "0.0000")

                    .Col = 4
                    .Text = "0.00"
                Next
            End With
        End If
        FormatSprdDlv(-1)
    End Sub

    Private Function InsertIntoTemp_Table() As Boolean

        On Error GoTo InsertErr
        Dim I As Integer
        Dim mMonthName As String
        Dim mQty As Double
        Dim mRate As Double
        Dim mValue As Double
        Dim SqlStr As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_MIS_SALEBUDGET_TRN " & vbCrLf & " WHERE USER_ID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND AUTO_KEY_NO =" & Val(lblNumber.Text) & "" & vbCrLf & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(lblItemCode.Text) & "' "

        PubDBCn.Execute(SqlStr)

        With SprdDlv
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColMonthName
                mMonthName = MainClass.AllowSingleQuote(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColValue
                mValue = Val(.Text)

                SqlStr = ""
                If mMonthName <> "" Then
                    SqlStr = "INSERT INTO TEMP_MIS_SALEBUDGET_TRN " & vbCrLf & " (USER_ID, AUTO_KEY_NO, COMPANY_CODE, SUPP_CUST_CODE, SERIAL_NO, " & vbCrLf & " ITEM_CODE, ITEM_UOM, SUB_SERIAL_NO, " & vbCrLf & " MONTH_NAME, QTY, RATE, VALUE) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & Val(lblNumber.Text) & ", " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(lblSuppCode.Text) & "', " & vbCrLf & " " & Val(lblMainActiveRow.Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(lblItemCode.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(lblUOM.Text) & "', " & vbCrLf & " " & I & ", " & vbCrLf & " '" & mMonthName & "', " & vbCrLf & " " & mQty & ", " & mRate & ", " & mValue & ")"
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        InsertIntoTemp_Table = True
        Exit Function
InsertErr:
        'Resume
        PubDBCn.RollbackTrans()
        InsertIntoTemp_Table = False
        MsgBox(Err.Description)
    End Function

    Private Sub SprdDlv_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdDlv.KeyPressEvent
        With SprdDlv
            If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then
                SprdDlv_LeaveCell(SprdDlv, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow + 1, False))
            End If

        End With
    End Sub

    Private Sub SprdDlv_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdDlv.LeaveCell
        On Error GoTo ErrPart
        Dim mQty As Double
        Dim mRate As Double

        If eventArgs.NewRow = -1 Then Exit Sub

        With SprdDlv
            .Row = eventArgs.Row
            .Col = ColQty
            mQty = Val(.Text)

            .Col = ColRate
            mRate = Val(.Text)

            .Col = ColValue
            .Text = VB6.Format(mQty * mRate, "0.00")
        End With

        '    MainClass.SetFocusToCell SprdDlv, NewRow, NewCol
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
End Class
