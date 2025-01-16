Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmParamVoucherChk
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection
    Dim SqlStr As String
    Private Const ColLocked As Short = 1
    Private Const ColBookType As Short = 2
    Private Const ColBookSubType As Short = 3
    Private Const ColVDate As Short = 4
    Private Const ColVNo As Short = 5
    Private Const ColDAmount As Short = 6
    Private Const ColCAmount As Short = 7
    Private Const ColMKEY As Short = 8

    Private Const RowHeight As Short = 12
    Dim mActiveRow As Integer
    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Function FieldVarification() As Boolean
        FieldVarification = True
        If FYChk((TxtDtFrom.Text)) = False Then
            FieldVarification = False
            TxtDtFrom.Focus()
            Exit Function
        End If
        If FYChk((TxtDtTo.Text)) = False Then
            FieldVarification = False
            TxtDtTo.Focus()
            Exit Function
        End If
    End Function
    Private Function SelectQuery(ByRef mSqlStr As String) As String
        Dim mCode As Integer
        On Error GoTo InsertErr

        mSqlStr = " Select '' ,Trn.BookType,Trn.BookSubType, "

        If chkExpenseDate.CheckState = System.Windows.Forms.CheckState.UnChecked Then
            mSqlStr = mSqlStr & vbCrLf & "TO_CHAR(Trn.Vdate,'DD/MM/YYYY') AS VDate, Trn.Vno as V_No,"
        Else
            mSqlStr = mSqlStr & vbCrLf & "TO_CHAR(Trn.EXPDate,'DD/MM/YYYY') AS VDate, CASE WHEN BOOKTYPE='O' THEN Trn.Vno ||'- ACCOUNT CODE : ' || ACCOUNTCODE ELSE Trn.Vno END as V_No,"
        End If

        mSqlStr = mSqlStr & vbCrLf & "  " & vbCrLf & " TO_CHAR(SUM(DECODE(DC,'D',1,0)*TRN.AMOUNT),'9,99,99,99,999.99') AS DEBIT, " & vbCrLf & " TO_CHAR(SUM(DECODE(DC,'D',0,1)*TRN.AMOUNT),'9,99,99,99,999.99') AS CREDIT, " & vbCrLf & " TRN.MKEY " & vbCrLf & " FROM FIN_POSTED_TRN Trn " & vbCrLf & " WHERE TRN.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        If chkExpenseDate.CheckState = System.Windows.Forms.CheckState.UnChecked Then
            If TxtDtFrom.Text <> "" Then
                mSqlStr = mSqlStr & vbCrLf & " AND TRN.VDate>=TO_DATE('" & VB6.Format(TxtDtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If TxtDtTo.Text <> "" Then
                mSqlStr = mSqlStr & vbCrLf & " AND TRN.VDate<=TO_DATE('" & VB6.Format(TxtDtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        Else
            If TxtDtFrom.Text <> "" Then
                mSqlStr = mSqlStr & vbCrLf & " AND TRN.EXPDate>=TO_DATE('" & VB6.Format(TxtDtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If TxtDtTo.Text <> "" Then
                mSqlStr = mSqlStr & vbCrLf & " AND TRN.EXPDate<=TO_DATE('" & VB6.Format(TxtDtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

        End If


        If chkExpenseDate.CheckState = System.Windows.Forms.CheckState.UnChecked Then
            mSqlStr = mSqlStr & vbCrLf & " GROUP BY Trn.BookType,Trn.BookSubType,Trn.Vdate, Trn.Vno, Trn.Mkey "
        Else
            mSqlStr = mSqlStr & vbCrLf & " GROUP BY Trn.BookType,Trn.BookSubType,Trn.EXPDate, CASE WHEN BOOKTYPE='O' THEN Trn.Vno ||'- ACCOUNT CODE : ' || ACCOUNTCODE ELSE Trn.Vno END, Trn.Mkey "
        End If

        mSqlStr = mSqlStr & vbCrLf & " Having SUM(DECODE(DC,'D',1,-1)*TRN.AMOUNT) <> 0"

        SelectQuery = mSqlStr
        Exit Function
InsertErr:
        MsgBox(Err.Description)
    End Function
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        If FieldVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Show1()
        FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub FrmParamVoucherChk_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        '    PvtDBCn.Close
        '    Set PvtDBCn = Nothing
    End Sub

    Private Sub FrmParamVoucherChk_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmParamVoucherChk_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo LErr
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        MainClass.SetControlsColor(Me)
        TxtDtFrom.Text = RsCompany.Fields("START_DATE").Value
        TxtDtTo.Text = CStr(RunDate)
        'Me.Height = VB6.TwipsToPixelsY(6210)
        'Me.Width = VB6.TwipsToPixelsX(9540)
        Me.Top = 0
        Me.Left = 0
        FormatSprdMain(-1)
        'Call FrmParamVoucherChk_Activated(eventSender, eventArgs)
        Exit Sub
LErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim xVDate As String
        Dim xMKey As String
        Dim xVNo As String
        Dim xBookType As String
        Dim xBookSubType As String
        Dim pIndex As Integer
        Dim xVTYPE As String

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColVDate
        xVDate = Me.SprdMain.Text

        SprdMain.Col = ColMKEY
        xMKey = Me.SprdMain.Text

        SprdMain.Col = ColVNo
        xVNo = Me.SprdMain.Text

        SprdMain.Col = ColBookType
        xBookType = Me.SprdMain.Text

        SprdMain.Col = ColBookSubType
        xBookSubType = Me.SprdMain.Text

        If xBookType = "B" Or xBookType = "F" Or xBookType = "C" Or xBookType = "J" Then
            xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)
            xVNo = VB.Right(xVNo, 5)
        ElseIf xBookType = "R" Or xBookType = "E" Then
            If RsCompany.Fields("FYEAR").Value >= 2020 Then
                xVTYPE = Mid(xVNo, 1, Len(xVNo) - 8)
                xVNo = VB.Right(xVNo, 8)
            Else
                xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)
                xVNo = VB.Right(xVNo, 5)
            End If
        End If

        Call ShowTrn(xMKey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType, Me)

    End Sub
    Private Sub TxtDtFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtDtFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If TxtDtFrom.Text = "" Then
            MsgBox("Date From Cannot Be Blank", MsgBoxStyle.Critical)
            TxtDtFrom.Focus()
            Cancel = True
        ElseIf TxtDtFrom.Text <> "" Then
            If Not IsDate(TxtDtFrom.Text) Then
                MsgBox("Invalid Date Pl. Check", MsgBoxStyle.Critical)
                TxtDtFrom.Focus()
                Cancel = True
            ElseIf FYChk((TxtDtFrom.Text)) = False Then
                TxtDtFrom.Focus()
                Cancel = True
            End If
        End If
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub TxtDtTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtDtTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If TxtDtTo.Text = "" Then
            MsgBox("Date To. Cannot Be Blank", MsgBoxStyle.Critical)
            TxtDtTo.Focus()
            Cancel = True
        ElseIf TxtDtTo.Text <> "" Then
            If Not IsDate(TxtDtTo.Text) Then
                MsgBox("Invalid Date Pl. Check", MsgBoxStyle.Critical)
                TxtDtTo.Focus()
                Cancel = True
            ElseIf FYChk((TxtDtTo.Text)) = False Then
                TxtDtTo.Focus()
                Cancel = True
            End If
        End If
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub Show1()

        On Error GoTo Show1Err

        Dim SqlStr As String
        Dim mRow As Integer
        Dim mName As String

        MainClass.ClearGrid(SprdMain)
        SqlStr = SelectQuery(SqlStr)

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        Exit Sub
Show1Err:
        MsgBox(Err.Description)
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        With SprdMain
            .set_RowHeight(-1, RowHeight * 1.5)
            .Row = -1

            .MaxCols = ColMKEY
            .set_ColWidth(0, 6)

            .Col = ColLocked
            .ColHidden = True
            .set_ColWidth(ColLocked, 1)

            .Col = ColBookType
            .ColHidden = True
            .set_ColWidth(ColBookType, 1)

            .Col = ColBookSubType
            .ColHidden = True
            .set_ColWidth(ColBookSubType, 1)

            .set_ColWidth(ColVDate, 12)
            .set_ColWidth(ColVNo, 20)

            .Col = ColDAmount
            .set_ColWidth(ColDAmount, 12)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            .Col = ColCAmount
            .set_ColWidth(ColCAmount, 12)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            .Col = ColMKEY
            .ColHidden = True

            '    .Col = ColAccountCode2
            '    .ColHidden = True


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '''OperationModeSingle
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))

            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH


        End With
        Call WriteColHeadings()
    End Sub
    Sub WriteColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColLocked
            .Text = " "

            .Col = ColVDate
            .Text = "Date"

            .Col = ColVNo
            .Text = "V.No."

            .Col = ColDAmount
            .Text = "Debit"

            .Col = ColCAmount
            .Text = "Credit"

        End With
    End Sub
End Class
