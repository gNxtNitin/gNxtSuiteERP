Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmDailyProdUpdate
    Inherits System.Windows.Forms.Form
    Dim RsSaleGRMain As ADODB.Recordset ''Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim SqlStr As String = ""
    Private Const ConRowHeight As Short = 12
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If


        If UpdateMain1 = True Then
            Me.Close()
        Else
            MsgInformation("Record not saved")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchItem()
    End Sub

    Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
        SearchTarrifCode()
    End Sub
    Private Sub SearchTarrifCode()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        MainClass.SearchGridMaster(txtTarrifCode.Text, "FIN_TARRIF_MST", "TARRIF_CODE", "TARRIF_DESC", , , SqlStr)
        If AcName <> "" Then
            txtTarrifCode.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub frmDailyProdUpdate_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Close()
    End Sub
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            txtItemName.Text = AcName
            txtItemCode.Text = AcName1
            txtItemName_Validating(txtItemName, New System.ComponentModel.CancelEventArgs(True))
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim mBillNoSeq As Integer
        Dim mBillNo As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mBillNoSeq = CInt(AutoGenSeqBillNo())
        mBillNo = "P" & VB6.Format(mBillNoSeq, "00000")

        SqlStr = ""
        SqlStr = "INSERT INTO FIN_RGDAILYMANU_HDR (" & vbCrLf & " MKEY,COMPANY_CODE,FYEAR,BOOKTYPE,BILLNO,MDATE," & vbCrLf & " ITEM_CODE,ITEM_QTY,TARIFF_CODE,UPDATEFLAG ) VALUES (  " & vbCrLf & " '-1', " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", 'S'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mBillNo) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtItemCode.Text)) & "', " & vbCrLf & " " & Val(txtQty.Text) & ",'" & MainClass.AllowSingleQuote((txtTarrifCode.Text)) & "','Y')"

        PubDBCn.Execute(SqlStr)
        UpdateMain1 = True

        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume
    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True

        '    If txtBillNo.Text = "" Then
        '        MsgInformation "Bill No. is Blank"
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        If txtBillDate.Text = "" Then
            MsgBox("BillDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtBillDate.Focus()
            Exit Function
        ElseIf FYChk((txtBillDate.Text)) = False Then
            FieldsVarification = False
            txtBillDate.Focus()
            Exit Function
        End If

        If Val(txtQty.Text) = 0 Then
            MsgBox("Qty is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtQty.Focus()
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Item Does Not Exist In Master", MsgBoxStyle.Information)
            'txtCustomer.SetFocus
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtTarrifCode.Text), "TARRIF_CODE", "TARRIF_CODE", "FIN_TARRIF_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Tarrif Does Not Exist In Master", MsgBoxStyle.Information)
            'txtCustomer.SetFocus
            FieldsVarification = False
            txtTarrifCode.Focus()
            Exit Function
        End If

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Public Sub frmDailyProdUpdate_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = ""
        SqlStr = "Select * from FIN_RGDAILYMANU_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleGRMain, ADODB.LockTypeEnum.adLockReadOnly)

        Call SetTextLengths()
        Call Clear1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsSaleGRMain

            txtBillNo.Maxlength = .Fields("BillNo").DefinedSize ''
            txtBillDate.Text = CStr(10)
            txtItemName.Maxlength = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            txtTarrifCode.Maxlength = .Fields("TARIFF_CODE").DefinedSize
            txtQty.Maxlength = .Fields("ITEM_QTY").Precision ''

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()
        '    LblMKey.text = ""
        '

        txtBillNo.Text = ""
        txtBillDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtItemName.Text = ""
        txtItemCode.Text = ""
        txtTarrifCode.Text = ""
        txtQty.Text = ""
    End Sub

    Private Sub frmDailyProdUpdate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Public Sub frmDailyProdUpdate_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn


        MainClass.SetControlsColor(Me)
        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
        'Me.Height = VB6.TwipsToPixelsY(3600)
        ''Me.Width = VB6.TwipsToPixelsX(5910)

        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        txtItemCode.Text = ""
        If txtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtItemCode.Text = MasterNo
            txtItemName.Text = UCase(Trim(txtItemName.Text))
        Else
            txtItemCode.Text = ""
            MsgInformation("No Such Item in Item Master")
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(txtTarrifCode.Text) = "" Then
            If MainClass.ValidateWithMasterTable((txtItemCode.Text), "ITEM_CODE", "TARIFF_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                txtTarrifCode.Text = MasterNo
            End If
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub

    Private Sub txtQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Function AutoGenSeqBillNo() As String

        On Error GoTo AutoGenSeqBillNoErr
        Dim RsSaleMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqBillNo As Integer
        SqlStr = ""


        SqlStr = "SELECT Max(SUBSTR(BILLNO,2))  FROM FIN_RGDAILYMANU_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "  AND BOOKTYPE='S'" & vbCrLf & " AND MKEY='-1' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSaleMainGen
            If .EOF = False Then
                If Not IsDbNull(.Fields(0).Value) Then
                    mNewSeqBillNo = .Fields(0).Value + 1
                Else
                    mNewSeqBillNo = 1
                End If
            Else
                mNewSeqBillNo = 1
            End If
        End With
        AutoGenSeqBillNo = CStr(mNewSeqBillNo)
        Exit Function
AutoGenSeqBillNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub txtTarrifCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtTarrifCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchTarrifCode()
    End Sub

    Private Sub txtTarrifCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTarrifCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtTarrifCode.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable((txtTarrifCode.Text), "TARRIF_CODE", "TARRIF_CODE", "FIN_TARRIF_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtTarrifCode.Text = MasterNo
        Else
            MsgInformation("No Such Tarrif in Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
