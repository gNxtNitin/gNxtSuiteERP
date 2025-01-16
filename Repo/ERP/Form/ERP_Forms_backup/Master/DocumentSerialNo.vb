Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmDocumentSerialNo
   Inherits System.Windows.Forms.Form
   Dim RsLocking As ADODB.Recordset
    'Dim PvtDBCN As ADODB.Connection
    Private Const ColBookType As Short = 1
    Private Const ColBookSubType As Short = 2
    Private Const ColBookName As Short = 3
    Private Const ColPrefixNo As Short = 4
    Private Const ColSuffixNo As Short = 5
    Private Const ColNumberDigit As Short = 6
    Private Const ConRowHeight As Short = 13
    Dim mFormLoad As Boolean
    Private Sub Show1()

        On Error GoTo Errshow1
        Dim cntCol As Short
        Dim SqlStr As String = ""
        Dim mCheckBookType As String
        Dim mBookType As String

        Dim mCheckBookSubType As String
        Dim mBookSubType As String


        For cntCol = 1 To SprdMain.MaxRows
            SprdMain.Row = cntCol
            SprdMain.Col = ColBookType
            mBookType = Trim(SprdMain.Text)

            SprdMain.Col = ColBookSubType
            mBookSubType = Trim(SprdMain.Text)

            SqlStr = " SELECT * FROM GEN_DOCUMENTNO_MST " & vbCrLf _
               & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
               & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
               & " AND BOOKTYPE='" & mBookType & "' AND BOOKSUBTYPE='" & mBookSubType & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLocking, ADODB.LockTypeEnum.adLockReadOnly)



            If RsLocking.EOF = False Then
                txtInvoiceDigit.Text = CStr(Val(IIf(IsDBNull(RsLocking.Fields("INVOICE_DIGIT").Value), 0, RsLocking.Fields("INVOICE_DIGIT").Value)))

                SprdMain.Col = ColPrefixNo
                SprdMain.Text = IIf(IsDBNull(RsLocking.Fields("PREFIX_NO").Value), "", RsLocking.Fields("PREFIX_NO").Value)

                SprdMain.Col = ColSuffixNo
                SprdMain.Text = IIf(IsDBNull(RsLocking.Fields("SUFFIX_NO").Value), "", RsLocking.Fields("SUFFIX_NO").Value)

                SprdMain.Col = ColNumberDigit
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsLocking.Fields("INVOICE_DIGIT").Value), 0, RsLocking.Fields("INVOICE_DIGIT").Value)))




            Else
                SprdMain.Col = ColPrefixNo
                SprdMain.Text = ""

                SprdMain.Col = ColSuffixNo
                SprdMain.Text = ""

                SprdMain.Col = ColNumberDigit
                SprdMain.Text = 0



            End If
        Next

        mFormLoad = True
        Exit Sub
Errshow1:
        MsgBox(Err.Description)
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)


        On Error GoTo ErrPart

        With SprdMain
            .set_RowHeight(Arow, ConRowHeight * 1.5)
            .Row = Arow

            .Col = ColBookType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = MainClass.SetMaxLength("BOOKTYPE", "GEN_DOCUMENTNO_MST", PubDBCn)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .ColHidden = True

            .Col = ColBookName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(.Col, 35)



            .Col = ColBookSubType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = MainClass.SetMaxLength("BOOKSUBTYPE", "GEN_DOCUMENTNO_MST", PubDBCn)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .ColHidden = True

            .Col = ColPrefixNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = MainClass.SetMaxLength("PREFIX_NO", "GEN_DOCUMENTNO_MST", PubDBCn)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(.Col, 12)

            .Col = ColSuffixNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = MainClass.SetMaxLength("SUFFIX_NO", "GEN_DOCUMENTNO_MST", PubDBCn)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(.Col, 12)

            .Col = ColNumberDigit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 1
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 12)
            .ColHidden = True

            MainClass.ProtectCell(SprdMain, Arow, .MaxRows, ColBookType, ColBookName)

            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Clear1()
        FormatSprdMain(-1)
    End Sub
    Private Sub FillSprdMain()
        On Error GoTo ErrPart

        SprdMain.MaxRows = 21

        If GetInsertRow(1, "S", "0", "SALE - BILL OF SUPPLY") = False Then GoTo ErrPart
        If GetInsertRow(2, "S", "1", "SALE - TAX INVOICE") = False Then GoTo ErrPart
        If GetInsertRow(3, "S", "3", "DELIVERY CHALLAN INTER UNIT") = False Then GoTo ErrPart
        If GetInsertRow(4, "S", "4", "TAX INVOICE SERVICE") = False Then GoTo ErrPart
        If GetInsertRow(5, "S", "6", "TAX INVOICE EXPORT") = False Then GoTo ErrPart
        If GetInsertRow(6, "S", "9", "SUPPLIMENTRY INVOICE") = False Then GoTo ErrPart

        If GetInsertRow(7, "E", "I", "EXPORT INVOICE") = False Then GoTo ErrPart
        If GetInsertRow(8, "8", "8", "RCM INVOICE") = False Then GoTo ErrPart

        If GetInsertRow(9, "P", "R", "CUSTOMER DEBIT NOTE") = False Then GoTo ErrPart

        If GetInsertRow(10, "J", "C", "GATE PASS - INTER UNIT") = False Then GoTo ErrPart
        If GetInsertRow(11, "J", "N", "GATE PASS - NON RETURNABLE") = False Then GoTo ErrPart
        If GetInsertRow(12, "J", "R", "GATE PASS - RETURNABLE") = False Then GoTo ErrPart

        If GetInsertRow(13, "S", "2", "SALE - JOB WORK INVOICE") = False Then GoTo ErrPart
        If GetInsertRow(14, "S", "7", "RCM GOODS") = False Then GoTo ErrPart
        If GetInsertRow(15, "S", "8", "RCM SERVICE") = False Then GoTo ErrPart
        If GetInsertRow(16, "S", "5", "DELIVERY CHALLAN INTER UNIT SUPPLIMENTRY") = False Then GoTo ErrPart
        If GetInsertRow(17, "J", "U", "GATE PASS - NON RETURNABLE INTER UNIT") = False Then GoTo ErrPart
        If GetInsertRow(18, "P", "M", "CUSTOMER CREDIT NOTE") = False Then GoTo ErrPart
        If GetInsertRow(19, "S", "R", "SALE RETURN") = False Then GoTo ErrPart

        If GetInsertRow(20, "S", "D", "CUSTOMER DEBIT NOTE VNO NO") = False Then GoTo ErrPart
        If GetInsertRow(21, "S", "C", "CUSTOMER CREDIT NOTE VNO NO") = False Then GoTo ErrPart

        FormatSprdMain(-1)

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function GetInsertRow(ByRef pRow As Integer, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pBookName As String) As Boolean
        On Error GoTo ErrPart

        With SprdMain
            .Row = pRow
            .Col = ColBookType
            .Text = pBookType

            .Col = ColBookSubType
            .Text = pBookSubType

            .Col = ColBookName
            .Text = pBookName
        End With

        GetInsertRow = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        GetInsertRow = False
    End Function

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrSave
        Dim mSqlStr As String = ""
        Dim cntCol As Short

        Dim SqlStr As String = ""
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mBookName As String
        Dim mPrefixNo As String
        Dim mLockingRights As String
        Dim mSuffixNo As String
        Dim mNumberDigit As Long

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mLockingRights = GetUserPermission("BOOK_LOCKING", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)

        'If mLockingRights = "N" Then
        '    MsgInformation("You Have no enough Rights.")
        '    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Exit Sub
        'End If

        mSqlStr = " Delete From GEN_DOCUMENTNO_MST " & vbCrLf _
           & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
           & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        PubDBCn.Execute(mSqlStr)


        ''**************************
        For cntCol = 1 To SprdMain.MaxRows
            SprdMain.Row = cntCol

            SprdMain.Col = ColBookType
            mBookType = Trim(SprdMain.Text)

            SprdMain.Col = ColBookSubType
            mBookSubType = Trim(SprdMain.Text)

            SprdMain.Col = ColPrefixNo
            mPrefixNo = Trim(SprdMain.Text)

            SprdMain.Col = ColSuffixNo
            mSuffixNo = Trim(SprdMain.Text)

            SprdMain.Col = ColNumberDigit
            mNumberDigit = Val(SprdMain.Text)
            mNumberDigit = IIf(mNumberDigit = 0, Val(txtInvoiceDigit.Text), mNumberDigit)

            If mBookType <> "" Then
                SqlStr = ""
                SqlStr = " INSERT INTO GEN_DOCUMENTNO_MST (" & vbCrLf _
                        & " COMPANY_CODE, FYEAR, " & vbCrLf _
                        & " BOOKTYPE, BOOKSUBTYPE, PREFIX_NO, SUFFIX_NO, INVOICE_DIGIT " & vbCrLf _
                        & " ) VALUES (" & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf _
                        & " '" & mBookType & "', '" & mBookSubType & "','" & mPrefixNo & "','" & mSuffixNo & "'," & mNumberDigit & "" & vbCrLf _
                        & " )"

                PubDBCn.Execute(SqlStr)
            End If
LabelSave:
        Next

        Show1()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        mFormLoad = True
        CmdSave.Enabled = False
        Exit Sub
ErrSave:
        MsgBox(Err.Description)
        ''Resume
    End Sub

    Private Sub frmDocumentSerialNo_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim SqlStr As String = ""

        Call SetMainFormCordinate(Me)

        'Set PvtDBCN = New ADODB.Connection
        'PvtDBCN.Open StrConn
        'Me.Top = 0
        'Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(6075) '8000
        ''Me.Width = VB6.TwipsToPixelsX(7935) '11900

        Clear1()

        '    SqlStr = " Select * From GEN_DOCUMENTNO_MST " & vbCrLf _
        ''            & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsLocking, adLockOptimistic
        MainClass.SetControlsColor(Me)

        FillSprdMain()
        Show1()
        mFormLoad = True

    End Sub

    Private Sub frmDocumentSerialNo_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        RsLocking.Close()
        RsLocking = Nothing
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub frmDocumentSerialNo_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        CmdSave.Enabled = True
    End Sub
End Class
