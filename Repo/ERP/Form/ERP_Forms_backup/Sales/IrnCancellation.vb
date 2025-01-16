Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json.Serialization
Friend Class FrmIRNCancellation
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

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If MsgQuestion("Are you sure cancel the Bill No & IRN No ? ") = CStr(MsgBoxResult.No) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

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

    Private Sub FrmIRNCancellation_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
        Dim pDespNoteNo As String

        mBillNo = txtBillNo.Text

        If WebRequestCancelIRN(pStatus) = False Then Exit Function


        If pStatus = True Then
            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()

            SqlStr = ""
            If optInvoice.Checked = True Then
                SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf _
                        & " CANCELLED= 'Y', " & vbCrLf _
                        & " IRN_CANCEL_REASON= '" & MainClass.AllowSingleQuote(txtCancelReason.Text) & "', " & vbCrLf _
                        & " IRN_CANCEL_REMARK=  '" & MainClass.AllowSingleQuote(txtCancelRemark.Text) & "'," & vbCrLf _
                        & " IRN_CANCEL_DATE=  TO_DATE('" & VB6.Format(txtCancelDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                        & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                        & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND FYEAR= '" & RsCompany.Fields("FYEAR").Value & "' AND BILLNo='" & mBillNo & "'"

                PubDBCn.Execute(SqlStr)

                pDespNoteNo = ""
                If MainClass.ValidateWithMasterTable(lblMkey.Text, "MKEY", "AUTO_KEY_DESP", "FIN_INVOICE_HDR", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    pDespNoteNo = MasterNo
                End If

                If DeleteCRTRN(PubDBCn, ConStockRefType_DSP, pDespNoteNo) = False Then GoTo ErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_DSP, pDespNoteNo) = False Then GoTo ErrPart


                PubDBCn.Execute("DELETE FROM DSP_PAINT57F4_TRN WHERE MKey='" & pDespNoteNo & "' AND BookType='D' AND BookSubType='O' AND TRNTYPE='D'")
                PubDBCn.Execute("UPDATE DSP_DESPATCH_HDR SET DESP_STATUS=2 WHERE AUTO_KEY_DESP='" & pDespNoteNo & "'")


                PubDBCn.Execute("DELETE FROM FIN_POSTED_TRN WHERE MKey='" & UCase(lblMkey.Text) & "' AND BookType='" & UCase(lblBookType.Text) & "' AND BOOKCODE='" & lblBookCode.Text & "'")
                PubDBCn.Execute("Delete From TCS_TRN Where Mkey='" & lblMkey.Text & "'")
            ElseIf optCreditNote.Checked = True Then
                SqlStr = "UPDATE FIN_SUPP_SALE_HDR SET " & vbCrLf _
                       & " CANCELLED= 'Y', " & vbCrLf _
                       & " IRN_CANCEL_REASON= '" & MainClass.AllowSingleQuote(txtCancelReason.Text) & "', " & vbCrLf _
                       & " IRN_CANCEL_REMARK=  '" & MainClass.AllowSingleQuote(txtCancelRemark.Text) & "'," & vbCrLf _
                       & " IRN_CANCEL_DATE=  TO_DATE('" & VB6.Format(txtCancelDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                       & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                       & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                       & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                       & " AND FYEAR= '" & RsCompany.Fields("FYEAR").Value & "' AND MKEY='" & lblMkey.Text & "'"

                PubDBCn.Execute(SqlStr)

                If DeleteStockTRN(PubDBCn, ConStockRefType_DSP, lblMkey.Text) = False Then GoTo ErrPart

                PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & lblMkey.Text & "' AND BookType='" & UCase(lblBookType.Text) & "' AND BOOKCODE='" & lblBookCode.Text & "'")
                PubDBCn.Execute("DELETE FROM FIN_POSTED_TRN WHERE MKey='" & UCase(lblMkey.Text) & "' AND BookType='" & UCase(lblBookType.Text) & "' AND BOOKCODE='" & lblBookCode.Text & "'")

            Else
                SqlStr = "UPDATE FIN_PURCHASE_HDR SET " & vbCrLf _
                       & " CANCELLED= 'Y', " & vbCrLf _
                       & " IRN_CANCEL_REASON= '" & MainClass.AllowSingleQuote(txtCancelReason.Text) & "', " & vbCrLf _
                       & " IRN_CANCEL_REMARK=  '" & MainClass.AllowSingleQuote(txtCancelRemark.Text) & "'," & vbCrLf _
                       & " IRN_CANCEL_DATE=  TO_DATE('" & VB6.Format(txtCancelDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                       & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                       & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                       & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                       & " AND FYEAR= '" & RsCompany.Fields("FYEAR").Value & "' AND MKEY='" & lblMkey.Text & "'"

                PubDBCn.Execute(SqlStr)

                PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & lblMkey.Text & "' AND BookType='" & UCase(lblBookType.Text) & "' AND BOOKCODE='" & lblBookCode.Text & "'")
                PubDBCn.Execute("DELETE FROM FIN_POSTED_TRN WHERE MKey='" & UCase(lblMkey.Text) & "' AND BookType='" & UCase(lblBookType.Text) & "' AND BOOKCODE='" & lblBookCode.Text & "'")

            End If



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

    Public Function WebRequestCancelIRN(ByRef pStatus As Boolean) As Boolean
        On Error GoTo ErrPart
        Dim url As String

        Dim mGSTIN As String
        Dim mCDKey As String
        Dim mEInvUserName As String
        Dim mEInvPassword As String
        Dim mEFUserName As String
        Dim mEFPassword As String

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

        Dim pError As String
        Dim pResponseText As String

        pStatus = False
        'If GeteInvoiceSetupContents(url, "C", mCDKey, mEFUserName, mEFPassword, mEInvUserName, mEInvPassword) = False Then GoTo ErrPart
        If GeteInvoiceSetupContents(url, "C", mCDKey, mEFUserName, mEFPassword, mEInvUserName, mEInvPassword, "N") = False Then GoTo ErrPart

        'Dim http As MSXML2.XMLHTTP60 '' MSXML.xmlhttp						
        'http = CreateObject("MSXML2.ServerXMLHTTP")

        Dim HTTP As Object
        HTTP = CreateObject("MSXML2.ServerXMLHTTP")

        mGSTIN = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)

        HTTP.Open("POST", url, False)

        HTTP.setRequestHeader("Content-Type", "application/json")
        mBody = "{""Push_Data_List"":{"
        mBody = mBody & """Data"": ["

        mBody = mBody & "{"

        mBody = mBody & """Irn"":""" & txtIRNNo.Text & ""","
        mBody = mBody & """Gstin"":""" & mGSTIN & ""","
        mBody = mBody & """CnlRsn"":""" & txtCancelReason.Text & ""","
        mBody = mBody & """CnlRem"":""" & txtCancelRemark.Text & ""","

        mBody = mBody & """CDKey"":""" & mCDKey & ""","
        mBody = mBody & """EInvUserName"":""" & mEInvUserName & ""","
        mBody = mBody & """EInvPassword"":""" & mEInvPassword & ""","
        mBody = mBody & """EFUserName"":""" & mEFUserName & ""","
        mBody = mBody & """EFPassword"":""" & mEFPassword & """"

        mBody = mBody & "}"

        mBody = mBody & "]"
        mBody = mBody & "}"
        mBody = mBody & "}"

        HTTP.Send(mBody)

        pResponseText = HTTP.responseText

        pResponseText = Replace(pResponseText, "[", "")
        pResponseText = Replace(pResponseText, "]", "")
        pResponseText = Replace(pResponseText, """", "'")

        If pResponseText = "''" Then
            WebRequestCancelIRN = True
            HTTP = Nothing
            Exit Function
        End If

        Dim post As Object
        pStaus = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .Status = ""})).Status

        If pStaus = "1" Then
            mIRNNo = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .Irn = ""})).Irn   ' JsonTest.Item("Irn")
            mCancelDate = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .CancelDate = ""})).CancelDate   'JsonTest.Item("CancelDate") 'JsonTest.Item("elements").Item(mResponseId).Item("ewayBillDate")						
            txtCancelDate.Text = VB6.Format(mCancelDate, "DD/MM/YYYY HH:MM")
            pStatus = True
        End If

        If pStaus = "0" Then
            pError = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .ErrorMessage = ""})).ErrorMessage ' JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
            MsgInformation(pError)
            WebRequestCancelIRN = False
            HTTP = Nothing
            Exit Function
        End If

        ''With JB
        ''    .Clear()
        ''    .IsArray_Renamed = False 'Actually the default after Clear.						

        ''    With .AddNewObject("Push_Data_List")
        ''        With .AddNewArray("Data") ''With .AddNewArray("Push_Data_List")						
        ''            With .AddNewObject()
        ''                .Item("Irn") = txtIRNNo.Text
        ''                .Item("Gstin") = mGSTIN
        ''                .Item("CnlRsn") = txtCancelReason.Text
        ''                .Item("CnlRem") = txtCancelRemark.Text

        ''                .Item("CDKey") = mCDKey
        ''                .Item("EInvUserName") = mEInvUserName
        ''                .Item("EInvPassword") = mEInvPassword
        ''                .Item("EFUserName") = mEFUserName
        ''                .Item("EFPassword") = mEFPassword

        ''            End With
        ''        End With
        ''    End With
        ''    mBody = .JSON
        ''End With

        'HTTP.send(mBody)

        'pResponseText = http.responseText

        'pResponseText = Replace(pResponseText, "[", "")
        'pResponseText = Replace(pResponseText, "]", "")

        'Dim JsonTest As Object
        'Dim SB As New cStringBuilder

        'Dim c As Object
        'Dim I As Integer

        'JsonTest = JSON.parse(pResponseText)

        'pStaus = JsonTest.Item("Status")


        'If pStaus = "1" Then
        '    mIRNNo = JsonTest.Item("Irn")
        '    mCancelDate = JsonTest.Item("CancelDate") 'JsonTest.Item("elements").Item(mResponseId).Item("ewayBillDate")						

        '    txtCancelDate.Text = VB6.Format(mCancelDate, "DD/MM/YYYY HH:MM")
        'End If

        'If pStaus = "0" Then
        '    pError = JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
        '    MsgInformation(pError)
        '    WebRequestCancelIRN = False
        '    http = Nothing
        '    Exit Function
        'End If

        WebRequestCancelIRN = True
        HTTP = Nothing

        Exit Function
ErrPart:
        '    Resume						
        WebRequestCancelIRN = False
        HTTP = Nothing
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

        If Trim(txtIRNNo.Text) = "" Then
            MsgBox("IRN No is blank.", MsgBoxStyle.Critical)
            FieldsVarification = False
            If txtIRNNo.Enabled = True Then txtIRNNo.Focus()
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
    Public Sub FrmIRNCancellation_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

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
        '   Resume						
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsSaleGRMain

            txtBillNo.MaxLength = .Fields("BILLNO").DefinedSize ''						
            txtCustomerName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

            txtIRNNo.MaxLength = .Fields("IRN_NO").DefinedSize ''						

            txtCancelReason.MaxLength = .Fields("IRN_CANCEL_REASON").DefinedSize ''						
            txtCancelRemark.MaxLength = .Fields("IRN_CANCEL_REMARK").DefinedSize ''						

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







        '    txtBillNoPrefix.Text = "S"						

        txtBillNo.Text = ""
        txtBillNo.Enabled = True
        TxtBillDate.Text = "__/__/____"
        txtCustomerName.Text = ""
        txtIRNNo.Text = ""
        txtCancelReason.Text = ""
        txtCancelRemark.Text = ""
        txtCancelDate.Text = ""

        lblBookCode.Text = ""
        lblBookType.Text = ""
        lblMkey.Text = ""
    End Sub

    Private Sub FrmIRNCancellation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Public Sub FrmIRNCancellation_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        'txtBillNo.Text = VB6.Format(txtBillNo.Text, ConBillFormat)

        mBillNo = Trim(txtBillNo.Text)

        If optInvoice.Checked = True Then
            SqlStr = " SELECT * FROM FIN_INVOICE_HDR " & vbCrLf _
                & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                & " AND FYEAR='" & RsCompany.Fields("FYEAR").Value & "' " & vbCrLf _
                & " AND BillNo='" & MainClass.AllowSingleQuote(mBillNo) & "'" ''& vbCrLf |            & " AND CANCELLED='N'"						

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                Clear1()
                ''txtBillNoPrefix.Text = IIf(IsDBNull(RsTemp.Fields("BILLNOPREFIX").Value), "", RsTemp.Fields("BILLNOPREFIX").Value)
                txtBillNo.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value), ConBillFormat)
                'txtBillNo.Text = IIf(IsDBNull(RsTemp.Fields("BILLNOSEQ").Value), "", RsTemp.Fields("BILLNOSEQ").Value) ''IIf(IsNull(RsTemp!BILLNO), "", RsTemp!BILLNO)						
                TxtBillDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                mCustomerCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)

                If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCustomerName.Text = MasterNo
                End If

                txtIRNNo.Text = IIf(IsDBNull(RsTemp.Fields("IRN_NO").Value), "", RsTemp.Fields("IRN_NO").Value)

                lblBookCode.Text = IIf(IsDBNull(RsTemp.Fields("BOOKCODE").Value), "", RsTemp.Fields("BOOKCODE").Value)
                lblBookType.Text = IIf(IsDBNull(RsTemp.Fields("BookType").Value), "", RsTemp.Fields("BookType").Value)
                lblMkey.Text = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)

            End If
        ElseIf optCreditNote.Checked = True Then
            SqlStr = " SELECT * FROM FIN_SUPP_SALE_HDR " & vbCrLf _
                & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                & " AND FYEAR='" & RsCompany.Fields("FYEAR").Value & "' " & vbCrLf _
                & " AND VNO='" & MainClass.AllowSingleQuote(mBillNo) & "' AND IRN_NO IS NOT NULL" ''& vbCrLf |            & " AND CANCELLED='N'"						

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                Clear1()
                'txtBillNoPrefix.Text = IIf(IsDBNull(RsTemp.Fields("BILLNOPREFIX").Value), "", RsTemp.Fields("BILLNOPREFIX").Value)
                'txtBillNo.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BILLNOSEQ").Value), "", RsTemp.Fields("BILLNOSEQ").Value), ConBillFormat)
                'txtBillNo.Text = IIf(IsDBNull(RsTemp.Fields("BILLNOSEQ").Value), "", RsTemp.Fields("BILLNOSEQ").Value) ''IIf(IsNull(RsTemp!BILLNO), "", RsTemp!BILLNO)						
                txtBillNo.Text = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                TxtBillDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")
                mCustomerCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)

                If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCustomerName.Text = MasterNo
                End If

                txtIRNNo.Text = IIf(IsDBNull(RsTemp.Fields("IRN_NO").Value), "", RsTemp.Fields("IRN_NO").Value)

                lblBookCode.Text = ConSaleDebitBookCode
                lblBookType.Text = IIf(IsDBNull(RsTemp.Fields("BookType").Value), "", RsTemp.Fields("BookType").Value)
                lblMkey.Text = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)

            End If
        Else
            SqlStr = " SELECT * FROM FIN_PURCHASE_HDR " & vbCrLf _
                & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                & " AND FYEAR='" & RsCompany.Fields("FYEAR").Value & "' " & vbCrLf _
                & " AND VNo='" & MainClass.AllowSingleQuote(mBillNo) & "' AND IRN_NO IS NOT NULL" ''& vbCrLf |            & " AND CANCELLED='N'"						

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                Clear1()
                'txtBillNoPrefix.Text = IIf(IsDBNull(RsTemp.Fields("BILLNOPREFIX").Value), "", RsTemp.Fields("BILLNOPREFIX").Value)
                'txtBillNo.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BILLNOSEQ").Value), "", RsTemp.Fields("BILLNOSEQ").Value), ConBillFormat)
                'txtBillNo.Text = IIf(IsDBNull(RsTemp.Fields("BILLNOSEQ").Value), "", RsTemp.Fields("BILLNOSEQ").Value) ''IIf(IsNull(RsTemp!BILLNO), "", RsTemp!BILLNO)						
                TxtBillDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")
                mCustomerCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)

                If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCustomerName.Text = MasterNo
                End If

                txtIRNNo.Text = IIf(IsDBNull(RsTemp.Fields("IRN_NO").Value), "", RsTemp.Fields("IRN_NO").Value)

                lblBookCode.Text = IIf(IsDBNull(RsTemp.Fields("BOOKCODE").Value), "", RsTemp.Fields("BOOKCODE").Value)
                lblBookType.Text = IIf(IsDBNull(RsTemp.Fields("BookType").Value), "", RsTemp.Fields("BookType").Value)
                lblMkey.Text = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)

            End If

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
