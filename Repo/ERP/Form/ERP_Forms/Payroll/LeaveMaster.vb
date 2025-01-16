Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLeaveMaster
    Inherits System.Windows.Forms.Form
    Dim RsLeave As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Private Const ConRowHeight As Short = 12
    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Const ColCode As Short = 1
    Private Const ColDesc As Short = 2
    Private Const ColTotEntitle As Short = 3
    Private Const ColTotEntitle_Wrks As Short = 4
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub frmLeaveMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From PAY_LEAVEDTL_MST Where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FormActive = True
        sprdLeaves.Enabled = True
        Show1()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub frmLeaveMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmLeaveMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Height = VB6.TwipsToPixelsY(4215)
        Me.Width = VB6.TwipsToPixelsX(6030)
        Me.Left = 0
        Me.Top = 0

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If UpdateLeave = False Then GoTo UpdateError
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Update1 = False
    End Function

    Private Sub FormatSprd(ByRef mRow As Integer)

        Dim cntCol As Integer

        On Error GoTo ERR1
        With sprdLeaves
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.3)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCode, 5)
            .ColHidden = True

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesc, 15)

            .Col = ColTotEntitle
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 1
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.9")
            .TypeFloatMin = CDbl("-9999999.9")
            .set_ColWidth(ColTotEntitle, 12)

            .Col = ColTotEntitle_Wrks
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 1
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.9")
            .TypeFloatMin = CDbl("-9999999.9")
            .set_ColWidth(ColTotEntitle_Wrks, 12)

            FillLeave()
            '        MainClass.UnProtectCell sprdLeaves, 1, .MaxRows, 1, .MaxCols
            MainClass.ProtectCell(sprdLeaves, 1, sprdLeaves.MaxRows, ColCode, ColDesc)
        End With

        MainClass.SetSpreadColor(sprdLeaves, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Function UpdateLeave() As Boolean
        On Error GoTo UpdateLoanErr

        Dim SqlStr As String = ""
        Dim xOpCode As Integer
        Dim xOpening As Double
        Dim xEntitle As Double
        Dim xTOTENTITLE As Double
        Dim xTOTENTITLE_WRKS As Double
        Dim xMon1 As Double
        Dim xMon2 As Double
        Dim xMon3 As Double
        Dim xMon4 As Double
        Dim xMon5 As Double
        Dim xMon6 As Double
        Dim xMon7 As Double
        Dim xMon8 As Double
        Dim xMon9 As Double
        Dim xMon10 As Double
        Dim xMon11 As Double
        Dim xMon12 As Double
        Dim cntRow As Integer

        UpdateLeave = True

        SqlStr = " DELETE FROM PAY_LEAVEDTL_MST WHERE " & vbCrLf & " PAYYEAR='" & PubPAYYEAR & "' AND " & vbCrLf & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        With sprdLeaves
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColCode
                xOpCode = Val(.Text)

                .Col = ColTotEntitle
                xTOTENTITLE = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColTotEntitle_Wrks
                xTOTENTITLE_WRKS = IIf(IsNumeric(.Text), .Text, 0)

                SqlStr = " Insert Into PAY_LEAVEDTL_MST (COMPANY_CODE, " & vbCrLf _
                    & " PAYYEAR, LEAVECODE,  " & vbCrLf & " TOTENTITLE, TOTENTITLE_WRKS ) VALUES " & vbCrLf _
                    & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " '" & PubPAYYEAR & "', " & vbCrLf _
                    & " " & xOpCode & "," & xTOTENTITLE & ", " & xTOTENTITLE_WRKS & ") "
                PubDBCn.Execute(SqlStr)
            Next
        End With

        Exit Function
UpdateLoanErr:
        UpdateLeave = False
    End Function
    Private Sub Show1()

        Dim SqlStr As String = ""
        Dim cntRow As Integer

        MainClass.ClearGrid(sprdLeaves, -1)
        FormatSprd(-1)

        SqlStr = " SELECT * from PAY_LEAVEDTL_MST " & vbCrLf & " WHERE " & vbCrLf & " PAYYEAR='" & PubPAYYEAR & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

        With sprdLeaves
            If RsLeave.EOF = False Then
                For cntRow = 1 To .MaxRows
                    RsLeave.MoveFirst()
                    Do While Not RsLeave.EOF
                        .Row = cntRow
                        .Col = ColCode
                        If Val(.Text) = RsLeave.Fields("LeaveCode").Value Then
                            .Col = ColTotEntitle
                            .Text = CStr(RsLeave.Fields("TOTENTITLE").Value)

                            .Col = ColTotEntitle_Wrks
                            .Text = CStr(RsLeave.Fields("TOTENTITLE_WRKS").Value)
                            GoTo NextRow1
                        End If
                        RsLeave.MoveNext()
                    Loop
NextRow1:
                Next
            Else
                '            MainClass.UnProtectCell sprdLeaves, 1, .MaxRows, 1, .MaxCols
            End If
        End With
    End Sub
    Private Sub FillLeave()
        Dim cntCol As Integer
        Dim mDate As Date


        With sprdLeaves
            .MaxRows = 4
            .Row = 0

            .Row = 1
            .Col = ColCode
            .Text = CStr(EARN)

            .Col = ColDesc
            .Text = "EARN"

            .Row = 2
            .Col = ColCode
            .Text = CStr(SICK)

            .Col = ColDesc
            .Text = "SICK"

            .Row = 3
            .Col = ColCode
            .Text = CStr(CASUAL)

            .Col = ColDesc
            .Text = "CASUAL"

            .Row = 4
            .Col = ColCode
            .Text = CStr(CPLEARN)

            .Col = ColDesc
            .Text = "CPL EARN"
        End With
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If Update1 = True Then
            Show1()
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
End Class
