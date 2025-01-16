Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports System.ComponentModel

Friend Class frmCashDiscountSlab
    Inherits System.Windows.Forms.Form
    Dim RsCDSlab As ADODB.Recordset

    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection	

    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColDays As Short = 1
    Private Const ColCDPer As Short = 2
    Private Sub Clear1()

        MainClass.ClearGrid(sprdMain)
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If Trim(txtWEF.Text) = "" Then Exit Sub

        If Not IsDate(txtWEF.Text) Then
            Exit Sub
        End If

        If Trim(txtSupplier.Text) = "" Then Exit Sub


        If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
            If Delete1() = False Then GoTo DelErrPart
            Clear1()
        End If

        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub frmCashDiscountSlab_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub sprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdMain.Change
        CmdSave.Enabled = True
    End Sub

    Private Sub sprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles sprdMain.ClickEvent
        sprdMain.Row = eventArgs.row
        If eventArgs.col = 0 Then
            If MsgQuestion("Are sure to delete the row? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.	
                sprdMain.Action = SS_ACTION_DELETE_ROW
                sprdMain.MaxRows = sprdMain.MaxRows - 1
            End If
        End If
    End Sub

    Private Sub sprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mPreviousDays As Double
        Dim mNextDays As Double
        Dim mCurrentDays As Double

        If eventArgs.newRow = -1 Then Exit Sub
        sprdMain.Row = eventArgs.row
        If eventArgs.col = ColDays Then
            If eventArgs.row > 1 Then
                sprdMain.Row = eventArgs.row - 1
                sprdMain.Col = ColDays
                mPreviousDays = Val(sprdMain.Text)

                sprdMain.Row = eventArgs.row
                sprdMain.Col = ColDays
                If Val(sprdMain.Text) <= mPreviousDays And Val(sprdMain.Text) <> 0 Then
                    MsgInformation("Please enter the vaild Value.")
                    MainClass.SetFocusToCell(sprdMain, eventArgs.row, ColDays)
                    Exit Sub
                Else
                    MainClass.AddBlankSprdRow(sprdMain, ColDays, ConRowHeight * 1.5)
                    FormatSprd(eventArgs.row)
                End If
            Else
                sprdMain.Row = eventArgs.row
                sprdMain.Col = ColDays
                If Val(sprdMain.Text) >= 0 Then
                    MainClass.AddBlankSprdRow(sprdMain, ColDays, ConRowHeight * 1.5)
                    FormatSprd(eventArgs.row)
                End If
            End If
        End If


        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        'Resume	
    End Sub
    Private Sub frmCashDiscountSlab_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        SqlStr = ""
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        txtWEF.Text = RsCompany.Fields("START_DATE").Value
        Clear1()
        Show1("")
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmCashDiscountSlab_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Height = VB6.TwipsToPixelsY(4695)
        Me.Width = VB6.TwipsToPixelsX(6315)
        Me.Left = VB6.TwipsToPixelsX(20)
        Me.Top = VB6.TwipsToPixelsY(20)

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmCashDiscountSlab_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Cancel	
        'PvtDBCn.Close	
        RsCDSlab = Nothing
        'Set PvtDBCn = Nothing	
    End Sub
    Private Sub Show1(ByRef pCustomerCode As String)

        On Error GoTo ShowErrPart
        Dim cntRow As Integer

        SqlStr = " SELECT *  FROM FIN_CDSLAB_MST WHERE " & vbCrLf _
            & " COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & vbCrLf & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustomerCode) & "'"



        SqlStr = SqlStr & vbCrLf & " ORDER BY SUBROWNO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCDSlab, ADODB.LockTypeEnum.adLockOptimistic)

        If RsCDSlab.EOF = False Then
            With RsCDSlab
                txtWEF.Text = .Fields("WEF").Value
                cntRow = 1
                Do While Not RsCDSlab.EOF
                    sprdMain.Row = cntRow

                    sprdMain.Col = ColDays
                    sprdMain.Text = CStr(.Fields("CD_DAYS").Value)

                    sprdMain.Col = ColCDPer
                    sprdMain.Text = CStr(.Fields("CD_PER").Value)

                    cntRow = cntRow + 1
                    RsCDSlab.MoveNext()
                    sprdMain.MaxRows = cntRow
                Loop
            End With
        End If
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        Dim mCustomerCode As String = ""
        If Trim(txtWEF.Text) = "" Then
            MsgInformation("WEF Can not be blank")
            txtWEF.Focus()
            Exit Sub
        End If
        If Not IsDate(txtWEF.Text) Then
            MsgInformation("Invaild WEF Date")
            txtWEF.Focus()
            Exit Sub
        End If

        If Trim(txtSupplier.Text) = "" Then
            MsgInformation("Invaild Supplier Name")
            txtSupplier.Focus()
            Exit Sub
        End If
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invaild Supplier Name")
            txtSupplier.Focus()
            Exit Sub
        Else
            mCustomerCode = MasterNo
        End If

        If Update1(mCustomerCode) = True Then
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
    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.TextChanged
        CmdSave.Enabled = True
    End Sub
    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.DoubleClick
        SearchSupplier()
    End Sub
    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSupplier()
    End Sub
    Private Sub cmdsearchSupp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchSupp.Click
        SearchSupplier()
    End Sub
    Private Function Update1(ByRef mCustomerCode As String) As Boolean

        On Error GoTo UpdateError
        Dim cntRow As Integer
        Dim mDays As Double
        Dim mPer As Double

        Dim mCompanyCode As Integer
        Dim RsTemp As ADODB.Recordset = Nothing

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mCompanyCode = IIf(IsDBNull(RsTemp.Fields("COMPANY_CODE").Value), -1, RsTemp.Fields("COMPANY_CODE").Value)

                SqlStr = " DELETE FROM FIN_CDSLAB_MST WHERE " & vbCrLf _
                    & " Company_Code=" & mCompanyCode & " " & vbCrLf _
                    & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustomerCode) & "'"

                PubDBCn.Execute(SqlStr)

                With sprdMain
                    For cntRow = 1 To .MaxRows - 1
                        .Row = cntRow

                        .Col = ColDays
                        mDays = IIf(IsNumeric(.Text), .Text, 0)

                        .Col = ColCDPer
                        mPer = IIf(IsNumeric(.Text), .Text, 0)


                        SqlStr = " INSERT INTO FIN_CDSLAB_MST " & vbCrLf _
                            & " ( COMPANY_CODE , SUBROWNO, WEF, SUPP_CUST_CODE, " & vbCrLf _
                            & " CD_DAYS , CD_PER," & vbCrLf _
                            & " ADDUSER, ADDDATE )  VALUES " & vbCrLf _
                            & " (" & mCompanyCode & ", " & vbCrLf _
                            & " " & cntRow & ", TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mCustomerCode) & "'," & mDays & "," & mPer & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                        PubDBCn.Execute(SqlStr)
                    Next
                End With

                RsTemp.MoveNext()
            Loop
        End If
        PubDBCn.CommitTrans()
        RsCDSlab.Requery()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsCDSlab.Requery()
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
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCompanyCode As Integer
        Dim mCustomerCode As String


        If Trim(txtSupplier.Text) = "" Then Exit Function

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invaild Supplier Name")
            txtSupplier.Focus()
            Exit Function
        Else
            mCustomerCode = MasterNo
        End If

        SqlStr = ""


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mCompanyCode = IIf(IsDBNull(RsTemp.Fields("COMPANY_CODE").Value), -1, RsTemp.Fields("COMPANY_CODE").Value)

                SqlStr = " DELETE FROM FIN_CDSLAB_MST WHERE " & vbCrLf _
                    & " Company_Code=" & mCompanyCode & " " & vbCrLf _
                    & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustomerCode) & "'"

                PubDBCn.Execute(SqlStr)

                RsTemp.MoveNext()
            Loop
        End If


        PubDBCn.CommitTrans()
        RsCDSlab.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsCDSlab.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This Employee.")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        With sprdMain
            .set_RowHeight(0, ConRowHeight * 1.5)
            .Row = mRow
            .MaxCols = ColCDPer
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColDays
            .CellType = SS_CELL_TYPE_INTEGER

            .TypeIntegerMax = CDbl("999")
            .TypeIntegerMin = 0
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDays, 16)



            .Col = ColCDPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCDPer, 15)


        End With
        MainClass.UnProtectCell(sprdMain, 1, sprdMain.MaxRows, ColDays, ColCDPer)
        MainClass.SetSpreadColor(sprdMain, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub SearchSupplier()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        MainClass.SearchGridMaster(txtSupplier.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            txtSupplier.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtWEF_TextChanged(sender As Object, e As EventArgs) Handles txtWEF.TextChanged

    End Sub

    Private Sub txtWEF_Validating(sender As Object, e As CancelEventArgs) Handles txtWEF.Validating
        On Error GoTo ErrPart
        Dim mCustomerCode As String = ""

        If Trim(txtWEF.Text) = "" Then Exit Sub

        If Not IsDate(txtWEF.Text) Then
            MsgInformation("Invalid Date")
            Exit Sub
        End If



        If Trim(txtSupplier.Text) = "" Then Exit Sub

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invaild Supplier Name")
            txtSupplier.Focus()
            Exit Sub
        Else
            mCustomerCode = MasterNo
        End If

        Clear1()
        Show1(mCustomerCode)

        Exit Sub
ErrPart:

    End Sub

    Private Sub txtSupplier_Validating(sender As Object, e As CancelEventArgs) Handles txtSupplier.Validating
        On Error GoTo ErrPart
        Dim mCustomerCode As String = ""

        If Trim(txtWEF.Text) = "" Then Exit Sub

        If Not IsDate(txtWEF.Text) Then
            MsgInformation("Invalid Date")
            Exit Sub
        End If



        If Trim(txtSupplier.Text) = "" Then Exit Sub

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invaild Supplier Name")
            txtSupplier.Focus()
            Exit Sub
        Else
            mCustomerCode = MasterNo
        End If

        Clear1()
        Show1(mCustomerCode)

        Exit Sub
ErrPart:

    End Sub
End Class
