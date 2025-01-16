Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json.Serialization
Friend Class FrmeWayCancellation
    Inherits System.Windows.Forms.Form
    Dim RsSaleGRMain As ADODB.Recordset ''Recordset					
    '''''Private PvtDBCn As ADODB.Connection					

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim SqlStr As String
    Private Const ConRowHeight As Short = 12
    Private JB As JsonBag
    Dim XRIGHT As String
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        If MsgQuestion("Are you sure cancel the e-Way Bill No? ") = CStr(MsgBoxResult.No) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If


        If UpdateMain1() = True Then
            MsgInformation("Bill No & IRN has been cancelled.")
            cmdSave.Enabled = False
        Else
            MsgInformation("Record not saved")
            cmdSave.Enabled = True
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume					
    End Sub

    Private Sub FrmeWayCancellation_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub


    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim CntBillNo As Integer
        Dim mBillNo As String
        Dim RsTemp As ADODB.Recordset
        Dim pStatus As Boolean = False
        mBillNo = txtBillNo.Text

        If WebRequestCanceleWayBill(pStatus) = False Then Exit Function

        If pStatus = True Then
            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()


            SqlStr = ""
            SqlStr = "INSERT INTO FIN_EWAY_BILL_CANCEL_HIS (" & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf _
                & " BILLNO, INVOICE_DATE, VEHICLENO, CARRIERS, " & vbCrLf _
                & " E_BILLWAYNO, EWAY_CANCEL_REASON, " & vbCrLf _
                & " EWAY_CANCEL_REMARK, EWAY_CANCEL_DATE, " & vbCrLf _
                & " MODUSER, MODDATE, CHANGE_TYPE) "

            SqlStr = SqlStr & vbCrLf & " VALUES('" & lblMkey.Text & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mBillNo) & "', TO_DATE('" & VB6.Format(TxtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtVehicle.Text)) & "', '" & MainClass.AllowSingleQuote((txtTransporter.Text)) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtEWayBillNo.Text)) & "', '" & MainClass.AllowSingleQuote((txtCancelReason.Text)) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtCancelRemark.Text)) & "', TO_DATE('" & VB6.Format(txtCancelDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), 'CANCELLED'" & vbCrLf & " )"

            PubDBCn.Execute(SqlStr)


            'SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf _
            '    & " E_BILLWAYNO= '', " & vbCrLf & " E_BILLWAYDATE= '', " & vbCrLf & " E_BILLWAYVAILDUPTO= '', " & vbCrLf & " E_BILLWAYFILEPATH= '', " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR= '" & RsCompany.Fields("FYEAR").Value & "' AND BILLNo='" & mBillNo & "'"

            'PubDBCn.Execute(SqlStr)

            'PubDBCn.Execute "DELETE FROM FIN_POSTED_TRN WHERE MKey='" & UCase(lblMkey.Caption) & "' AND BookType='" & UCase(lblBookType.Caption) & "' AND BOOKCODE='" & lblBookCode.Caption & "'"                   
            'PubDBCn.Execute "Delete From TCS_TRN Where Mkey='" & lblMkey.Caption & "'"                  

            PubDBCn.CommitTrans()
        End If

        UpdateMain1 = True


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

    Public Function WebRequestCanceleWayBill(ByRef pStatus As Boolean) As Boolean
        On Error GoTo ErrPart
        Dim url As String

        Dim mGSTIN As String
        Dim pCDKey As String
        Dim pEWBUserName As String
        Dim pEWBPassword As String
        Dim pEFUserName As String
        Dim pEFPassword As String

        'Dim mSqlStr As String					
        'Dim RsTemp As ADODB.Recordset					

        Dim mBody As String
        Dim mResponseId As String
        Dim mResponseIdStr As String
        Dim url1 As String
        Dim WebRequestGen As String
        Dim pStaus As String

        Dim mIRNNo As String

        Dim mCancelDate As String
        Dim pDocDate As String

        Dim pError As String
        Dim pResponseText As String

        pStatus = False
        'If GetWebTeleWaySetupContents(url, "X", pCDKey, pEFUserName, pEFPassword, pEWBUserName, pEWBPassword) = False Then GoTo ErrPart
        If GetWebTeleWaySetupContents(url, "X", pCDKey, pEFUserName, pEFPassword, pEWBUserName, pEWBPassword, "N") = False Then GoTo ErrPart

        'Dim http As MSXML2.XMLHTTP60 '' MSXML.xmlhttp					
        'http = CreateObject("MSXML2.ServerXMLHTTP")

        Dim http As Object  ' MSXML2.XMLHTTP60 '' MSXML.xmlhttp
        http = CreateObject("MSXML2.ServerXMLHTTP")

        mGSTIN = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)

        http.Open("POST", url, False)

        http.setRequestHeader("Content-Type", "application/json")

        pDocDate = VB6.Format(TxtBillDate.Text, "DD/MM/YYYY")

        mBody = "{""Push_Data_List"":["
        'mBody = mBody & """Data"": ["

        mBody = mBody & "{"


        mBody = mBody & """Gstin"":""" & mGSTIN & ""","
        mBody = mBody & """EWBNumber"":" & Trim(txtEWayBillNo.Text) & ","
        mBody = mBody & """CancelReasonCode"":""" & Trim(txtCancelReason.Text) & ""","
        mBody = mBody & """CancelRemark"":""" & Trim(txtCancelRemark.Text) & ""","


        mBody = mBody & """EWBUserName"":""" & pEWBUserName & ""","
        mBody = mBody & """EWBPassword"":""" & pEWBPassword & """"

        mBody = mBody & "}"

        mBody = mBody & "],"

        mBody = mBody & """Year"":" & Year(CDate(pDocDate)) & ","
        mBody = mBody & """Month"":" & Month(CDate(pDocDate)) & ","
        mBody = mBody & """EFUserName"":""" & pEFUserName & ""","
        mBody = mBody & """EFPassword"":""" & pEFPassword & ""","
        mBody = mBody & """CDKey"":""" & pCDKey & """"


        mBody = mBody & "}"

        'mBody = mBody & "]"
        'mBody = mBody & "}"
        'mBody = mBody & "}"


        http.send(mBody)

        pResponseText = http.responseText

        pResponseText = Replace(pResponseText, "[", "")
        pResponseText = Replace(pResponseText, "]", "")
        pResponseText = Replace(pResponseText, "\", "")
        pResponseText = Replace(pResponseText, """", "'")
        pResponseText = Replace(pResponseText, "'{", "{")
        pResponseText = Replace(pResponseText, "}'", "}")

        Dim post As Object
        pStaus = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .IsSuccess = ""})).IsSuccess  '\'IsSuccess

        If UCase(pStaus) = "TRUE" Then
            mCancelDate = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .CancelDate = ""})).CancelDate   'JsonTest.Item("Irn")
            txtCancelDate.Text = VB6.Format(mCancelDate, "DD/MM/YYYY HH:MM")
            pStatus = True
        End If


        If UCase(pStaus) = "FALSE" Then
            pError = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .ErrorMessage = ""})).ErrorMessage ' JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
            MsgInformation(pError)
            WebRequestCanceleWayBill = False
            http = Nothing
            Exit Function
        End If

        ''Dim JsonTest As Object
        ''JsonTest = JSON.parse(pResponseText)

        ''pStaus = JsonTest.Item("IsSuccess")

        'If UCase(pStaus) = UCase("True") Then
        '    mCancelDate = JsonTest.Item("CancelDate")
        '    txtCancelDate.Text = VB6.Format(mCancelDate, "DD/MM/YYYY HH:MM")
        'End If

        'If UCase(pStaus) = "FALSE" Then
        '    pError = JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")					
        '    MsgInformation(pError)
        '    WebRequestCanceleWayBill = False
        '    http = Nothing
        '    Exit Function
        'End If


        '    pResponseText = http.responseText					
        '					
        '    pResponseText = Replace(pResponseText, "[", "")					
        '    pResponseText = Replace(pResponseText, "]", "")					
        '					
        '    Dim JsonTest As Object					
        '    Dim SB As New cStringBuilder					
        '					
        '    Dim c As Object					
        '    Dim I As Long					
        '					
        '    Set JsonTest = JSON.parse(pResponseText)					
        '					
        '    pStaus = JsonTest.Item("Status")					
        '					
        '					
        '    If pStaus = "1" Then					
        '        mIRNNo = JsonTest.Item("Irn")					
        '        mCancelDate = JsonTest.Item("CancelDate") 'JsonTest.Item("elements").Item(mResponseId).Item("ewayBillDate")					
        '					
        '        txtCancelDate.Text = Format(mCancelDate, "DD/MM/YYYY HH:MM")					
        '    End If					
        '					
        '    If pStaus = "0" Then					
        '        pError = JsonTest.Item("ErrorMessage")  ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")					
        '        MsgInformation pError					
        '        WebRequestCanceleWayBill = False					
        '        Set http = Nothing					
        '        Exit Function					
        '    End If					

        WebRequestCanceleWayBill = True
        http = Nothing

        Exit Function
ErrPart:
        '    Resume					
        WebRequestCanceleWayBill = False
        http = Nothing
        MsgBox(Err.Description)
        PubDBCn.RollbackTrans()
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True

        If txtBillNo.Text = "" Then
            MsgInformation("Bill No. is Blank")
            txtBillNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If TxtBillDate.Text = "" Then
            MsgInformation("Bill date is Blank")
            TxtBillDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtEWayBillNo.Text) = "" Then
            MsgBox("eWay Bill No is blank.", MsgBoxStyle.Critical)
            FieldsVarification = False
            If txtEWayBillNo.Enabled = True Then txtEWayBillNo.Focus()
            Exit Function
        End If

        If Trim(txtCancelReason.Text) = "" Then
            MsgBox("Cancel Reason is blank.", MsgBoxStyle.Critical)
            FieldsVarification = False
            txtCancelReason.Focus()
            Exit Function
        End If

        If Trim(txtCancelRemark.Text) = "" Then
            MsgBox("Cancel Remark is blank.", MsgBoxStyle.Critical)
            FieldsVarification = False
            txtCancelRemark.Focus()
            Exit Function
        End If

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume					
    End Function
    Public Sub FrmeWayCancellation_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = ""
        SqlStr = "Select * from FIN_INVOICE_HDR Where 1<>1"
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
            'txtBillNoPrefix.MaxLength = .Fields("BillNoPrefix").DefinedSize ''					
            txtBillNo.MaxLength = .Fields("BILLNO").DefinedSize ''					
            txtCustomerName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

            txtEWayBillNo.MaxLength = .Fields("E_BILLWAYNO").DefinedSize ''					

            txtVehicle.MaxLength = .Fields("VEHICLENO").DefinedSize
            txtTransporter.MaxLength = .Fields("CARRIERS").DefinedSize

            txtCancelReason.MaxLength = MainClass.SetMaxLength("EWAY_CANCEL_REASON", "FIN_EWAY_BILL_CANCEL_HIS", PubDBCn)
            txtCancelRemark.MaxLength = MainClass.SetMaxLength("EWAY_CANCEL_REMARK", "FIN_EWAY_BILL_CANCEL_HIS", PubDBCn) ''					


        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Clear1()
        Dim mFyear1 As String
        Dim mFyear2 As String

        mFyear1 = VB.Right(VB6.Format(RsCompany.Fields("FYEAR").Value, "0000"), 2)
        mFyear2 = CStr(Val(mFyear1) + 1)




        'txtBillNoPrefix.Text = ""


        '    txtBillNoPrefix.Text = "S"					

        txtBillNo.Text = ""
        txtBillNo.Enabled = True
        TxtBillDate.Text = "__/__/____"
        txtCustomerName.Text = ""
        txtEWayBillNo.Text = ""
        txtVehicle.Text = ""
        txtTransporter.Text = ""
        txtCancelReason.Text = ""
        txtCancelRemark.Text = ""
        txtCancelDate.Text = ""

        lblBookCode.Text = ""
        lblBookType.Text = ""
        lblMkey.Text = ""


    End Sub

    Private Sub FrmeWayCancellation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Public Sub FrmeWayCancellation_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ''''Set PvtDBCn = New ADODB.Connection					
        ''''PvtDBCn.Open StrConn					

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)

        cmdSave.Enabled = IIf(InStr(1, XRIGHT, "S") > 0, True, False)

        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(4665)
        'Me.Width = VB6.TwipsToPixelsX(5910)

        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNo.KeyPress
        'Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        'KeyAscii = MainClass.SetNumericField(KeyAscii)
        'eventArgs.KeyChar = Chr(KeyAscii)
        'If KeyAscii = 0 Then
        '    eventArgs.Handled = True
        'End If
    End Sub

    Private Sub txtBillNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim xMkey As String
        Dim mBillNo As String
        Dim mCustomerCode As String
        Dim mCustomerName As String
        If Trim(txtBillNo.Text) = "" Then GoTo EventExitSub

        'txtBillNo.Text = VB6.Format(Val(txtBillNo.Text), "00000")
        mBillNo = Trim(txtBillNo.Text)

        SqlStr = " SELECT * FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR='" & RsCompany.Fields("FYEAR").Value & "' " & vbCrLf & " AND BillNo='" & MainClass.AllowSingleQuote(mBillNo) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Clear1()

            'txtBillNoPrefix.Text = IIf(IsDBNull(RsTemp.Fields("BILLNOPREFIX").Value), "", RsTemp.Fields("BILLNOPREFIX").Value)
            txtBillNo.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value), ConBillFormat)

            'txtBillNo.Text = IIf(IsDBNull(RsTemp.Fields("BILLNOSEQ").Value), "", RsTemp.Fields("BILLNOSEQ").Value) ''IIf(IsNull(RsTemp!BILLNO), "", RsTemp!BILLNO)					
            TxtBillDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
            mCustomerCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)

            If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtCustomerName.Text = MasterNo
            End If




            txtTransporter.Text = IIf(IsDBNull(RsTemp.Fields("CARRIERS").Value), "", RsTemp.Fields("CARRIERS").Value)

            txtVehicle.Text = IIf(IsDBNull(RsTemp.Fields("VEHICLENO").Value), "", RsTemp.Fields("VEHICLENO").Value)

            txtEWayBillNo.Text = IIf(IsDBNull(RsTemp.Fields("E_BILLWAYNO").Value), "", RsTemp.Fields("E_BILLWAYNO").Value)

            lblBookCode.Text = IIf(IsDBNull(RsTemp.Fields("BOOKCODE").Value), "", RsTemp.Fields("BOOKCODE").Value)
            lblBookType.Text = IIf(IsDBNull(RsTemp.Fields("BookType").Value), "", RsTemp.Fields("BookType").Value)
            lblMkey.Text = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)

        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCancelReason_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCancelReason.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtCancelReason.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBillDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtBillDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If TxtBillDate.Text = "__/__/____" Then GoTo EventExitSub
        If IsDate(TxtBillDate.Text) = False Then
            ErrorMsg("Invalid Date", "", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtCancelRemark_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCancelRemark.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtCancelRemark.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
