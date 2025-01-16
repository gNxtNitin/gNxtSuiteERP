Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Friend Class frmeWayBillWebtel
    Inherits System.Windows.Forms.Form
    Dim RsInvoice As ADODB.Recordset
    'Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Dim CurMKey As String
    Dim SqlStr As String

    Private Const ConRowHeight As Short = 15
    Private GapHorizontal As Single
    Private GapVertical As Single

    Private JB As JsonBag

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColProductDesc As Short = 3
    Private Const ColHSNCode As Short = 4
    Private Const ColQty As Short = 5
    Private Const ColUnit As Short = 6
    Private Const ColTaxableAmount As Short = 7
    Private Const ColSGSTRate As Short = 8
    Private Const ColSGSTValue As Short = 9
    Private Const ColCGSTRate As Short = 10
    Private Const ColCGSTValue As Short = 11
    Private Const ColIGSTRate As Short = 12
    Private Const ColIGSTValue As Short = 13
    Private Const ColCessRate As Short = 14

    Private Sub cboTransmode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboTransmode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboVehicleType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboVehicleType.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            '        SprdDetail.Enabled = True
            '        txtInvoiceNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsInvoice.EOF = False Then RsInvoice.MoveFirst()
            Show1()
            '        ShowInvoiceData
            '        txtInvoiceNo.Enabled = True
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        '    RsInvoice.Requery
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdDistance_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDistance.Click
        On Error GoTo ErrPart
        Dim url As String
        Dim pUserGSTin As String
        Dim pFromPincode As Double
        Dim pToGstin As String
        Dim pToPincode As Double

        Dim pTransDistance As Double
        Dim cntRow As Integer

        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim pStaus As String

        Dim mBody As String
        Dim mResponseId As String



        Dim pResponseText As String
        Dim pError As String

        Dim pCDKey As String = ""
        Dim pEFUserName As String = ""
        Dim pEFPassword As String = ""
        Dim pEWBUserName As String = ""
        Dim pEWBPassword As String = ""
        Dim pIsTesing As String = "Y"

        If CDbl(lblInvoiceSeqType.Text) = 6 Then Exit Sub

        If GetWebTeleWaySetupContents(url, "D", pCDKey, pEFUserName, pEFPassword, pEWBUserName, pEWBPassword, pIsTesing) = False Then GoTo ErrPart

        Dim http As Object '' MSXML2.XMLHTTP60 '' MSXML.xmlhttp							
        http = CreateObject("MSXML2.ServerXMLHTTP")



        pUserGSTin = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        pFromPincode = IIf(IsDBNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value)


        pToGstin = GetPartyBusinessDetail(Trim(txtSuppCustCode.Text), Trim(txtBillToLocation.Text), "GST_RGN_NO")
        pToPincode = GetPartyBusinessDetail(Trim(txtSuppCustCode.Text), Trim(txtBillToLocation.Text), "SUPP_CUST_PIN")

        If pToGstin = "" Or pToPincode = "" Then
            MsgInformation("Invalid GSTN No or PIN Code.")
            http = Nothing
            Exit Sub
        End If

        http.Open("POST", url, False)

        http.setRequestHeader("Content-Type", "application/json")

        ''     Public Class EwData
        ''    Public Property Irn As String
        ''    Public Property GSTIN As String
        ''    Public Property CDKey As String
        ''    Public Property EInvUserName As String
        ''    Public Property EInvPassword As String
        ''    Public Property EFUserName As String
        ''    Public Property EFPassword As String
        ''End Class

        'Dim details As New List(Of IRNQRData)()

        'details.Add(New IRNQRData() With {
        '    .Irn = mIRNNo,
        '    .GSTIN = mGSTIN,
        '    .CDKey = mCDKey,
        '    .EInvUserName = mEInvUserName,
        '    .EInvPassword = mEInvPassword,
        '    .EFUserName = mEFUserName,
        '    .EFPassword = mEFPassword
        '       })


        'Dim mBodyDetail As String = JsonConvert.SerializeObject(details)


        'mBody = "{""Push_Data_List"":{"
        'mBody = mBody & """Data"": "
        'mBody = mBody & mBodyDetail
        'mBody = mBody & "}"
        'mBody = mBody & "}"


        'With JB
        '    .Clear()
        '    .IsArray_Renamed = False 'Actually the default after Clear.

        '    With .AddNewArray("Push_Data_List")
        '        For cntRow = 1 To SprdMain.MaxRows - 1
        '            With .AddNewObject()
        '                .Item("GSTIN") = pUserGSTin
        '                .Item("SourcePincode") = pFromPincode
        '                .Item("DestinationPincode") = pToPincode
        '                .Item("EWBUserName") = pEWBUserName
        '                .Item("EWBPassword") = pEWBPassword
        '            End With
        '        Next
        '    End With
        '    .Item("EFUserName") = pEFUserName
        '    .Item("EFPassword") = pEFPassword
        '    .Item("CDKey") = pCDKey
        '    mBody = .JSON
        'End With

        http.Send(mBody)

        pResponseText = http.responseText
        pResponseText = Replace(pResponseText, "\", "")
        pResponseText = Replace(pResponseText, "[", "")
        pResponseText = Replace(pResponseText, "]", "")
        pResponseText = Mid(pResponseText, 2, Len(pResponseText) - 2)
        Dim JsonTest As Object
        JsonTest = JSON.parse(pResponseText)


        pStaus = JsonTest.Item("IsSuccess")

        If UCase(pStaus) = UCase("True") Then
            pTransDistance = JsonTest.Item("Distance")
            MsgInformation("Net Distance is : " & pTransDistance & " KM")
            '        txtDistance.Text = Val(pTransDistance)

            '        PubDBCn.Errors.Clear
            '        PubDBCn.BeginTrans
            '
            '        SqlStr = ""
            '
            '        SqlStr = "UPDATE FIN_SUPP_CUST_MST SET " & vbCrLf _
            ''                & " LOC_DISTANCE ='" & Val(pTransDistance) & "'" & vbCrLf _
            ''                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            ''                & " AND MKEY ='" & pMKey & "'"
            '
            '
            '        PubDBCn.Execute SqlStr
            '
            '        PubDBCn.CommitTrans
        End If



        http = Nothing
        Exit Sub
ErrPart:
        http = Nothing
        MsgBox(Err.Description)
        '     PubDBCn.RollbackTrans
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click
        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then

            If Trim(txtResponseId.Text) <> "" Then
                MsgInformation("Response ID is generated, So cann't be change.")
                Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsInvoice, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

            txtInvoiceNo.Enabled = False

            cboTransmode.Enabled = True
            txtDistance.Enabled = True
            txtTransName.Enabled = True
            txtTransportCode.Enabled = True
            txtTransportDocNo.Enabled = True
            txtTransDocDate.Enabled = True
            txtVehicleNo.Enabled = True
            cboVehicleType.Enabled = True

        Else
            ADDMode = False
            MODIFYMode = False
            txtInvoiceNo.Enabled = True
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        On Error GoTo ErrPart
        Dim mFilePath As String
        Dim pCDKey As String
        Dim pEFUserName As String
        Dim pEFPassword As String
        Dim pEWBUserName As String
        Dim pEWBPassword As String
        Dim url As String
        Dim pResponseIdText As String
        Dim mBody As String
        Dim pStatus As String
        Dim pIsTesing As String = "Y"

        Dim http As Object  ''MSXML2.XMLHTTP60 '' MSXML.xmlhttp
        http = CreateObject("MSXML2.ServerXMLHTTP")

        If GetWebTeleWaySetupContents(url, "P", pCDKey, pEFUserName, pEFPassword, pEWBUserName, pEWBPassword, pIsTesing) = False Then GoTo ErrPart


        http.Open("POST", url, False)
        http.setRequestHeader("Content-Type", "application/json")

        'With JB
        '    .Clear()
        '    .IsArray_Renamed = False 'Actually the default after Clear.

        '    .Item("GSTIN") = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        '    .Item("ewbNo") = Trim(txteWayBillNo.Text)
        '    .Item("Year") = Year(CDate(txtInvoiceDate.Text))
        '    .Item("Month") = Month(CDate(txtInvoiceDate.Text))
        '    .Item("EFUserName") = pEFUserName
        '    .Item("EFPassword") = pEFPassword
        '    .Item("CDKey") = pCDKey
        '    .Item("EWBUserName") = pEWBUserName
        '    .Item("EWBPassword") = pEWBPassword
        '    mBody = .JSON
        'End With

        http.Send(mBody)

        mFilePath = http.responseText


        'If mFilePath <> "" Then
        '    ShellExecute(Me.Handle.ToInt32, "open", mFilePath, vbNullString, vbNullString, 0)
        'End If

        If FILEExists(mFilePath) Then
            Process.Start("explorer.exe", mFilePath)
        End If

        Exit Sub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        On Error GoTo ErrPart
        Dim mMKey As String
        Dim result As String

        Dim RsTemp As ADODB.Recordset
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mSuppCustName As String
        Dim mSubRow As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim mItemUOM As String
        Dim mItemQty As Double
        Dim mItemRate As Double
        Dim mItemAmount As Double
        Dim mBodyTextDetail As String
        Dim mBodyText As String
        Dim mSubject As String
        Dim mTo As String
        Dim mCC As String
        Dim mFrom As String
        Dim mVehicleNo As String

        mMKey = Trim(lblMKey.Text)

        If Val(txtDistance.Text) = 0 Then
            MsgInformation("Please Select Distance.")
            Exit Sub
        End If

        If Trim(txteWayBillNo.Text) = "" Then
            If WebRequestCreate(mMKey) = False Then Exit Sub
        End If

        '    If Trim(txtResponseId.Text) <> "" And Trim(txteWayBillNo.Text) = "" Then
        '        If WebRequestGenerate(mMKey) = False Then Exit Sub
        '    End If

        '    cmdResetID.Enabled = IIf(txteWayBillNo.Text = "", True, False)

        Exit Sub
ErrPart:
        cmdResetID.Enabled = IIf(txteWayBillNo.Text = "", True, False)
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mFlag As String)

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & mRPTName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1

    End Sub
    Private Sub cmdResetID_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdResetID.Click
        On Error GoTo ErrPart
        Dim SqlStr As String

        If Trim(txteWayBillNo.Text) = "" Then

            If MsgQuestion("Want to reset Response Id.? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                SqlStr = ""

                If lbleWayType.Text = "I" Then
                    SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf & " eWayResponseID =''" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY ='" & lblMKey.Text & "'"

                ElseIf lbleWayType.Text = "R" Then
                    SqlStr = "UPDATE INV_GATEPASS_HDR SET " & vbCrLf & " eWayResponseID =''" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PASSNO =" & Val(lblMKey.Text) & ""
                End If

                PubDBCn.Execute(SqlStr)
                txtResponseId.Text = ""
                PubDBCn.CommitTrans()
            End If
        End If

        Exit Sub
ErrPart:
        PubDBCn.RollbackTrans()
        Exit Sub
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtInvoiceNo_Validating(txtInvoiceNo, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            GoTo ErrorHandler
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ViewGrid()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmeWayBillWebtel_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "e-Way Bill" & IIf(lbleWayType.Text = "I", "(Invoice)", "(RGP)")

        SqlStr = ""

        If lbleWayType.Text = "I" Then
            SqlStr = "Select * from FIN_INVOICE_HDR Where 1<>1"
        ElseIf lbleWayType.Text = "R" Then
            SqlStr = "Select * from INV_GATEPASS_HDR Where 1<>1"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInvoice, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call SetTextLengths()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub frmeWayBillWebtel_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmeWayBillWebtel_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        txtInvoiceDate.Enabled = True
        txtInvoiceDate.ReadOnly = True
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        cboSubType.Items.Clear()
        cboSubType.Items.Add("01. Supply")
        cboSubType.Items.Add("02. Import")
        cboSubType.Items.Add("03. Export")
        cboSubType.Items.Add("04. Jobwork")
        cboSubType.Items.Add("05. For Own Use")
        cboSubType.Items.Add("06. JobWork Returns")
        cboSubType.Items.Add("07. Sales Return")
        cboSubType.Items.Add("08. Others")
        cboSubType.Items.Add("09. SKD/CKD")
        cboSubType.Items.Add("10. Line Sales")
        cboSubType.Items.Add("11. Recipient Not Know")
        cboSubType.Items.Add("12. Exhibition or Fairs")
        cboSubType.SelectedIndex = 0

        cboDocType.Items.Clear()
        cboDocType.Items.Add("INV - Tax Invoice")
        cboDocType.Items.Add("BIL - Bill of Supply")
        cboDocType.Items.Add("BOE - Bill of Entry")
        cboDocType.Items.Add("CHL - Challan")
        cboDocType.Items.Add("CNT - Credit Note")
        cboDocType.Items.Add("OTH - Others")
        cboDocType.SelectedIndex = 0
        '
        '
        cboTransmode.Items.Clear()
        cboTransmode.Items.Add("1. Road")
        cboTransmode.Items.Add("2. Rail")
        cboTransmode.Items.Add("3. Air")
        cboTransmode.Items.Add("4. Ship")
        cboTransmode.SelectedIndex = 0

        cboVehicleType.Items.Clear()
        cboVehicleType.Items.Add("Regular")
        cboVehicleType.Items.Add("Over Dimensional Cargo")
        cboVehicleType.SelectedIndex = 0

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo SetTextLengthsErr
        txtInvoiceDate.MaxLength = 10
        txtSupplierName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

        txtDistance.MaxLength = RsInvoice.Fields("TRANS_DISTANCE").Precision
        txtTransName.MaxLength = RsInvoice.Fields("CARRIERS").DefinedSize
        txtTransportCode.MaxLength = RsInvoice.Fields("TRANSPORTER_GSTNO").DefinedSize
        '    txtTransportDocNo
        '    txtTransportDocDate
        If lbleWayType.Text = "I" Then
            txtVehicleNo.MaxLength = RsInvoice.Fields("VEHICLENO").DefinedSize
        ElseIf lbleWayType.Text = "R" Then
            txtVehicleNo.MaxLength = RsInvoice.Fields("VEHICLE_NO").DefinedSize
        End If

        '    txtVehicleType

        '    txtCWBNo.MaxLength = RsInvoice.Fields("CWBNO").DefinedSize
        '    txtTransportCode.MaxLength = RsInvoice.Fields("TRANSPORTCODE").DefinedSize

        Exit Sub
SetTextLengthsErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub Clear1()
        SqlStr = ""
        txtPreInvoice.Text = IIf(lbleWayType.Text = "I", "S", "")
        lblMKey.Text = ""

        txtInvoiceNo.Enabled = True
        txtInvoiceNo.Text = ""
        txtInvoiceDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtSupplierName.Text = ""
        cboTransmode.SelectedIndex = 0
        txtDistance.Text = ""
        txtTransName.Text = ""
        txtTransportCode.Text = ""
        txtTransportDocNo.Text = ""
        txtTransDocDate.Text = ""
        txtVehicleNo.Text = ""
        cboVehicleType.SelectedIndex = 0
        txtResponseId.Text = ""
        txteWayBillNo.Text = ""

        txteWayBillDate.Text = ""
        txteWayValidupto.Text = ""
        lblFilepath.Text = ""


        cboTransmode.Enabled = True
        txtDistance.Enabled = True
        txtTransName.Enabled = True
        txtTransportCode.Enabled = True
        txtTransportDocNo.Enabled = True
        txtTransDocDate.Enabled = True
        txtVehicleNo.Enabled = True
        cboVehicleType.Enabled = True

        txtResponseId.Enabled = False
        cmdResetID.Enabled = True
        txteWayBillNo.Enabled = True
        txteWayBillNo.ReadOnly = True

        txteWayBillDate.Enabled = False
        txteWayValidupto.Enabled = False

        lblDespatchFrom.Text = "N"
        lblShippedFromCode.Text = ""


        lblInvoiceSeqType.Text = CStr(-1)
        cboSubType.SelectedIndex = 0
        cboDocType.SelectedIndex = 0

        lblShippedCode.Text = ""
        lblShippedToSameParty.Text = ""

        lblIRNNo.Text = ""

        MainClass.ClearGrid(SprdMain)
        '    MainClass.ClearGrid SprdDetail
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsInvoice, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub ShowInvoiceData()
        On Error GoTo ShowErrPart
        Dim mSuppCustCode As String
        Dim mShippedToCode As String
        Dim mShippedFromCode As String
        Dim mItemCode As String
        Dim pMKey As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim I As Integer
        Dim mTransMode As Integer
        Dim mVehicleType As String
        Dim pUOM As String
        Dim pProductType As String
        Dim pProductDesc As String
        Dim mInvoiceSeqType As Integer
        Dim mHSNCode As String
        Dim m57F4 As String

        Dim mItemRate As Double


        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double

        Dim mItemCGSTValue As Double
        Dim mItemSGSTValue As Double
        Dim mItemIGSTValue As Double

        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mQty As Double
        Dim mDespatchNo As Double
        Dim mTaxableValue As Double
        Dim mNetInvoiceValue As Double
        Dim mSameShippTo As String
        Dim mTotTaxableValue As Double


        Dim mCGSTValue As Double
        Dim mSGSTValue As Double
        Dim mIGSTValue As Double


        lblCGSTAmt.Text = CStr(0)
        lblSGSTAmt.Text = CStr(0)
        lblIGSTAmt.Text = CStr(0)
        mCGSTValue = 0
        mSGSTValue = 0
        mIGSTValue = 0

        mItemCGSTValue = 0
        mItemSGSTValue = 0
        mItemIGSTValue = 0

        If Not RsInvoice.EOF Then
            pMKey = IIf(IsDBNull(RsInvoice.Fields("mKey").Value), "", RsInvoice.Fields("mKey").Value)
            txtInvoiceNo.Text = VB6.Format(IIf(IsDBNull(RsInvoice.Fields("BILLNOSEQ").Value), "", RsInvoice.Fields("BILLNOSEQ").Value), "00000000")
            txtInvoiceDate.Text = VB6.Format(IIf(IsDBNull(RsInvoice.Fields("INVOICE_DATE").Value), "", RsInvoice.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
            mInvoiceSeqType = IIf(IsDBNull(RsInvoice.Fields("INVOICESEQTYPE").Value), -1, RsInvoice.Fields("INVOICESEQTYPE").Value)
            lblInvoiceSeqType.Text = CStr(mInvoiceSeqType)
            mDespatchNo = IIf(IsDBNull(RsInvoice.Fields("AUTO_KEY_DESP").Value), "", RsInvoice.Fields("AUTO_KEY_DESP").Value)

            If mInvoiceSeqType = 1 Then
                cboSubType.SelectedIndex = 0
                cboDocType.SelectedIndex = 0
            ElseIf mInvoiceSeqType = 2 Then
                cboSubType.SelectedIndex = 3
                cboDocType.SelectedIndex = 3
            ElseIf mInvoiceSeqType = 3 Then
                cboSubType.SelectedIndex = 4
                cboDocType.SelectedIndex = 3
            ElseIf mInvoiceSeqType = 6 Then
                cboSubType.SelectedIndex = 2
                cboDocType.SelectedIndex = 0
            End If

            mSameShippTo = IIf(IsDBNull(RsInvoice.Fields("SHIPPED_TO_SAMEPARTY").Value), "Y", RsInvoice.Fields("SHIPPED_TO_SAMEPARTY").Value)
            mSuppCustCode = IIf(IsDBNull(RsInvoice.Fields("SUPP_CUST_CODE").Value), "", RsInvoice.Fields("SUPP_CUST_CODE").Value)
            mShippedToCode = IIf(IsDBNull(RsInvoice.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsInvoice.Fields("SHIPPED_TO_PARTY_CODE").Value)

            '        If mSameShippTo = "Y" Then
            '            mSuppCustCode = IIf(IsNull(RsInvoice!SUPP_CUST_CODE), "", RsInvoice!SUPP_CUST_CODE)
            '        Else
            '            mSuppCustCode = IIf(IsNull(RsInvoice!SHIPPED_TO_PARTY_CODE), "", RsInvoice!SHIPPED_TO_PARTY_CODE)
            '        End If

            txtSuppCustCode.Text = mSuppCustCode

            If MainClass.ValidateWithMasterTable(mSuppCustCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtSupplierName.Text = MasterNo
            End If

            lblShippedToSameParty.Text = mSameShippTo
            lblShippedCode.Text = mShippedToCode

            lblDespatchFrom.Text = IIf(IsDBNull(RsInvoice.Fields("IS_DESP_OTHERTHAN_BILL").Value), "N", RsInvoice.Fields("IS_DESP_OTHERTHAN_BILL").Value)
            lblShippedFromCode.Text = IIf(IsDBNull(RsInvoice.Fields("SHIPPED_FROM_PARTY_CODE").Value), "", RsInvoice.Fields("SHIPPED_FROM_PARTY_CODE").Value)


            mTransMode = IIf(IsDBNull(RsInvoice.Fields("TRANSPORT_MODE").Value), 0, VB.Left(RsInvoice.Fields("TRANSPORT_MODE").Value, 1))

            cboTransmode.SelectedIndex = mTransMode - 1
            txtDistance.Text = IIf(IsDBNull(RsInvoice.Fields("TRANS_DISTANCE").Value), 0, RsInvoice.Fields("TRANS_DISTANCE").Value)
            txtTransName.Text = IIf(IsDBNull(RsInvoice.Fields("CARRIERS").Value), "", RsInvoice.Fields("CARRIERS").Value)
            txtTransportCode.Text = IIf(IsDBNull(RsInvoice.Fields("TRANSPORTER_GSTNO").Value), "", RsInvoice.Fields("TRANSPORTER_GSTNO").Value)
            txtTransportDocNo.Text = IIf(IsDBNull(RsInvoice.Fields("GRNO").Value), "", RsInvoice.Fields("GRNO").Value)
            txtTransDocDate.Text = VB6.Format(IIf(IsDBNull(RsInvoice.Fields("GRDATE").Value), "", RsInvoice.Fields("GRDATE").Value), "DD/MM/YYYY")
            txtVehicleNo.Text = IIf(IsDBNull(RsInvoice.Fields("VEHICLENO").Value), "", RsInvoice.Fields("VEHICLENO").Value)
            txtResponseId.Text = IIf(IsDBNull(RsInvoice.Fields("EWAYRESPONSEID").Value), "", RsInvoice.Fields("EWAYRESPONSEID").Value)
            txteWayBillNo.Text = IIf(IsDBNull(RsInvoice.Fields("E_BILLWAYNO").Value), "", RsInvoice.Fields("E_BILLWAYNO").Value)

            txteWayBillDate.Text = VB6.Format(IIf(IsDBNull(RsInvoice.Fields("E_BILLWAYDATE").Value), "", RsInvoice.Fields("E_BILLWAYDATE").Value), "DD/MM/YYYY HH:MM")
            txteWayValidupto.Text = VB6.Format(IIf(IsDBNull(RsInvoice.Fields("E_BILLWAYVAILDUPTO").Value), "", RsInvoice.Fields("E_BILLWAYVAILDUPTO").Value), "DD/MM/YYYY HH:MM")
            lblFilepath.Text = Trim(IIf(IsDBNull(RsInvoice.Fields("E_BILLWAYFILEPATH").Value), "", RsInvoice.Fields("E_BILLWAYFILEPATH").Value))

            lblIRNNo.Text = Trim(IIf(IsDBNull(RsInvoice.Fields("IRN_NO").Value), "", RsInvoice.Fields("IRN_NO").Value)) ''01-12-2020

            If mInvoiceSeqType = 2 Or mInvoiceSeqType = 3 Then
            Else
                lblNetAmount.Text = IIf(IsDBNull(RsInvoice.Fields("NETVALUE").Value), "0.00", RsInvoice.Fields("NETVALUE").Value)
                lblTaxableAmount.Text = IIf(IsDBNull(RsInvoice.Fields("TOTTAXABLEAMOUNT").Value), "0.00", RsInvoice.Fields("TOTTAXABLEAMOUNT").Value)
            End If

            cmdResetID.Enabled = IIf(txteWayBillNo.Text = "", True, False)
            mVehicleType = IIf(IsDBNull(RsInvoice.Fields("VEHICLE_TYPE").Value), "", RsInvoice.Fields("VEHICLE_TYPE").Value)
            cboVehicleType.SelectedIndex = IIf(mVehicleType = "R", 0, 1)

            cboTransmode.Enabled = False
            txtDistance.Enabled = False
            txtTransName.Enabled = False
            txtTransportCode.Enabled = False
            txtTransportDocNo.Enabled = False
            txtTransDocDate.Enabled = False
            txtVehicleNo.Enabled = False
            cboVehicleType.Enabled = False

            MainClass.ClearGrid(SprdMain)

            If mInvoiceSeqType = 2 Then
                mSqlStr = "SELECT * FROM FIN_INVOICE_DET WHERE MKEY='" & MainClass.AllowSingleQuote(pMKey) & "'"
            Else
                mSqlStr = " SELECT ITEM_CODE, ITEM_DESC, ITEM_UOM, ITEM_RATE, " & vbCrLf & " CGST_PER, SGST_PER , IGST_PER, " & vbCrLf & " HSNCODE, SUM(CGST_AMOUNT) AS CGST_AMOUNT,SUM(SGST_AMOUNT) AS SGST_AMOUNT,  SUM(IGST_AMOUNT) AS IGST_AMOUNT,  SUM(GSTABLE_AMT) As GSTABLE_AMT, " & vbCrLf & " SUM(ITEM_QTY) AS ITEM_QTY, SUM(ITEM_AMT) AS ITEM_AMT " & vbCrLf & " FROM FIN_INVOICE_DET WHERE MKEY='" & MainClass.AllowSingleQuote(pMKey) & "'" & vbCrLf & " GROUP BY ITEM_CODE, ITEM_DESC, ITEM_UOM, ITEM_RATE, " & vbCrLf & " CGST_PER, SGST_PER , IGST_PER, HSNCODE"
            End If
            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            I = 0
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False

                    I = I + 1
                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    SprdMain.Row = I

                    SprdMain.Col = ColItemCode
                    SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                    mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                    SprdMain.Col = ColItemDesc
                    SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_DESC").Value), "", RsTemp.Fields("ITEM_DESC").Value)
                    pProductDesc = IIf(IsDBNull(RsTemp.Fields("ITEM_DESC").Value), "", RsTemp.Fields("ITEM_DESC").Value)

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "PRODTYPE_DESC", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        pProductType = MasterNo
                    End If
                    pProductType = IIf(Trim(pProductType) = "", pProductDesc, pProductType)

                    SprdMain.Col = ColProductDesc
                    SprdMain.Text = VB.Left(pProductType, 80)

                    If mInvoiceSeqType = 2 Then

                        mLocal = "N"
                        If Trim(txtSupplierName.Text) <> "" Then
                            If MainClass.ValidateWithMasterTable(txtSupplierName.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mLocal = Trim(MasterNo)
                            End If
                        End If

                        mPartyGSTNo = ""
                        If MainClass.ValidateWithMasterTable(txtSupplierName.Text, "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mPartyGSTNo = MasterNo
                        End If

                        mHSNCode = GetHSNCode(mItemCode)
                        SprdMain.Col = ColHSNCode
                        SprdMain.Text = mHSNCode

                        SprdMain.Col = ColQty
                        SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00")
                        mQty = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00"))

                        SprdMain.Col = ColUnit
                        pUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                        SprdMain.Text = IIf(pUOM = "PCS", "NOS", pUOM)

                        m57F4 = Get57F4(mDespatchNo, Trim(mItemCode), I)

                        mItemRate = GetChallanRate(mItemCode, Str(mDespatchNo), m57F4)

                        mCGSTPer = 0
                        mSGSTPer = 0
                        mIGSTPer = 0

                        mItemCGSTValue = 0
                        mItemSGSTValue = 0
                        mItemIGSTValue = 0

                        mCGSTValue = 0
                        mSGSTValue = 0
                        mIGSTValue = 0

                        '                    If GetHSNDetails(mHSNCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, "G", mPartyGSTNo) = False Then GoTo ShowErrPart

                        mTaxableValue = CDbl(VB6.Format(mItemRate * mQty, "0.00"))
                        mTotTaxableValue = mTotTaxableValue + mTaxableValue

                        SprdMain.Col = ColTaxableAmount
                        SprdMain.Text = VB6.Format(mTaxableValue, "0.00")

                        SprdMain.Col = ColSGSTRate
                        SprdMain.Text = VB6.Format(mSGSTPer, "0.00")

                        SprdMain.Col = ColSGSTValue
                        SprdMain.Text = VB6.Format(mItemSGSTValue, "0.00")

                        SprdMain.Col = ColCGSTRate
                        SprdMain.Text = VB6.Format(mCGSTPer, "0.00")

                        SprdMain.Col = ColCGSTValue
                        SprdMain.Text = VB6.Format(mItemCGSTValue, "0.00")

                        SprdMain.Col = ColIGSTRate
                        SprdMain.Text = VB6.Format(mIGSTPer, "0.00")

                        SprdMain.Col = ColIGSTValue
                        SprdMain.Text = VB6.Format(mItemIGSTValue, "0.00")

                        SprdMain.Col = ColCessRate
                        SprdMain.Text = "0.00"

                        mNetInvoiceValue = mNetInvoiceValue + (mTaxableValue + mCGSTValue + mSGSTValue + mIGSTValue)
                        '                    mCGSTAmount = Format(mItemRate * mQty * mCGSTPer * 0.01, "0.00")
                        '                    mSGSTAmount = Format(mItemRate * mQty * mSGSTPer * 0.01, "0.00")
                        '                    mIGSTAmount = Format(mItemRate * mQty * mIGSTPer * 0.01, "0.00")

                    Else
                        SprdMain.Col = ColHSNCode
                        SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("HSNCODE").Value), "", RsTemp.Fields("HSNCODE").Value)

                        SprdMain.Col = ColQty
                        SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00")

                        SprdMain.Col = ColUnit
                        pUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                        SprdMain.Text = IIf(pUOM = "PCS", "NOS", pUOM)

                        SprdMain.Col = ColTaxableAmount
                        SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("GSTABLE_AMT").Value), 0, RsTemp.Fields("GSTABLE_AMT").Value), "0.00")
                        mTotTaxableValue = mTotTaxableValue + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("GSTABLE_AMT").Value), 0, RsTemp.Fields("GSTABLE_AMT").Value), "0.00"))

                        If mInvoiceSeqType = 3 Then
                            SprdMain.Col = ColSGSTRate
                            SprdMain.Text = "0.00"

                            SprdMain.Col = ColCGSTRate
                            SprdMain.Text = "0.00"

                            SprdMain.Col = ColIGSTRate
                            SprdMain.Text = "0.00"

                            SprdMain.Col = ColSGSTValue
                            SprdMain.Text = "0.00"

                            SprdMain.Col = ColCGSTValue
                            SprdMain.Text = "0.00"

                            SprdMain.Col = ColIGSTValue
                            SprdMain.Text = "0.00"


                            mCGSTValue = 0
                            mSGSTValue = 0
                            mIGSTValue = 0

                        Else
                            SprdMain.Col = ColSGSTRate
                            SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value), "0.00")

                            SprdMain.Col = ColSGSTValue
                            SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SGST_AMOUNT").Value), 0, RsTemp.Fields("SGST_AMOUNT").Value), "0.00")

                            SprdMain.Col = ColCGSTRate
                            SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value), "0.00")

                            SprdMain.Col = ColCGSTValue
                            SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("CGST_AMOUNT").Value), 0, RsTemp.Fields("CGST_AMOUNT").Value), "0.00")

                            SprdMain.Col = ColIGSTRate
                            SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value), "0.00")

                            SprdMain.Col = ColIGSTValue
                            SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("IGST_AMOUNT").Value), 0, RsTemp.Fields("IGST_AMOUNT").Value), "0.00")

                            mCGSTValue = mCGSTValue + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("CGST_AMOUNT").Value), 0, RsTemp.Fields("CGST_AMOUNT").Value), "0.00"))
                            mSGSTValue = mSGSTValue + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SGST_AMOUNT").Value), 0, RsTemp.Fields("SGST_AMOUNT").Value), "0.00"))
                            mIGSTValue = mIGSTValue + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("IGST_AMOUNT").Value), 0, RsTemp.Fields("IGST_AMOUNT").Value), "0.00"))

                        End If

                        mNetInvoiceValue = mNetInvoiceValue + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("GSTABLE_AMT").Value), 0, RsTemp.Fields("GSTABLE_AMT").Value), "0.00"))

                        SprdMain.Col = ColCessRate
                        SprdMain.Text = "0.00"
                    End If

                    RsTemp.MoveNext()
                Loop
            End If
        End If

        If mInvoiceSeqType = 2 Or mInvoiceSeqType = 3 Then
            lblNetAmount.Text = VB6.Format(mNetInvoiceValue, "0.00")
            lblTaxableAmount.Text = VB6.Format(mTotTaxableValue, "0.00")
        End If

        lblCGSTAmt.Text = VB6.Format(mCGSTValue, "0.00")
        lblSGSTAmt.Text = VB6.Format(mSGSTValue, "0.00")
        lblIGSTAmt.Text = VB6.Format(mIGSTValue, "0.00")


        ADDMode = False
        MODIFYMode = False

        'MainClass.ButtonStatus(Me, XRIGHT, RsInvoice, ADDMode, MODIFYMode, True,  , False)

        MainClass.ButtonStatus(Me, XRIGHT, RsInvoice, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        FormatSprdMain(-1)


        Exit Sub
ShowErrPart:
        MsgBox(Err.Description, Err.Number)
    End Sub
    Private Sub ShowRGPData()
        On Error GoTo ShowErrPart
        Dim mSuppCustCode As String
        Dim mItemCode As String
        Dim pMKey As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim I As Integer
        Dim mTransMode As Integer
        Dim mVehicleType As String
        Dim pUOM As String
        Dim pProductType As String
        Dim pProductDesc As String
        Dim mInvoiceSeqType As Integer
        Dim mHSNCode As String
        Dim m57F4 As String

        Dim mItemRate As Double


        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mQty As Double
        Dim mDespatchNo As Double
        Dim mTaxableValue As Double
        Dim mTotInvoiceValue As Double
        Dim mItemValue As Double
        Dim mCGSTValue As Double
        Dim mSGSTValue As Double
        Dim mIGSTValue As Double

        Dim mTotCGSTValue As Double
        Dim mTotSGSTValue As Double
        Dim mTotIGSTValue As Double
        Dim mCompanyGSTNo As String
        'Dim mPartyGSTNo As String

        'Dim mQty As Double



        mCGSTValue = 0
        mSGSTValue = 0
        mIGSTValue = 0

        mTotInvoiceValue = CDbl("0.00")

        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""


        If Not RsInvoice.EOF Then
            pMKey = IIf(IsDBNull(RsInvoice.Fields("AUTO_KEY_PASSNO").Value), "", RsInvoice.Fields("AUTO_KEY_PASSNO").Value)
            txtInvoiceNo.Text = IIf(IsDBNull(RsInvoice.Fields("AUTO_KEY_PASSNO").Value), "", RsInvoice.Fields("AUTO_KEY_PASSNO").Value)
            txtInvoiceDate.Text = VB6.Format(IIf(IsDBNull(RsInvoice.Fields("GATEPASS_DATE").Value), "", RsInvoice.Fields("GATEPASS_DATE").Value), "DD/MM/YYYY")
            lblInvoiceSeqType.Text = CStr(1)




            mSuppCustCode = IIf(IsDBNull(RsInvoice.Fields("SUPP_CUST_CODE").Value), "", RsInvoice.Fields("SUPP_CUST_CODE").Value)
            txtSuppCustCode.Text = IIf(IsDBNull(RsInvoice.Fields("SUPP_CUST_CODE").Value), "", RsInvoice.Fields("SUPP_CUST_CODE").Value)

            If MainClass.ValidateWithMasterTable(mSuppCustCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtSupplierName.Text = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(mSuppCustCode, "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyGSTNo = MasterNo
            End If

            If mCompanyGSTNo = mPartyGSTNo Then
                cboSubType.SelectedIndex = 7 'lbleWayType.Caption = "R"
                cboDocType.SelectedIndex = 3
            Else
                cboSubType.SelectedIndex = 3 'lbleWayType.Caption = "R"
                cboDocType.SelectedIndex = 3
            End If

            lblShippedCode.Text = ""
            lblShippedToSameParty.Text = "Y"

            mTransMode = IIf(IsDBNull(RsInvoice.Fields("TRANSPORT_MODE").Value), 1, VB.Left(RsInvoice.Fields("TRANSPORT_MODE").Value, 1))

            cboTransmode.SelectedIndex = mTransMode - 1
            txtDistance.Text = IIf(IsDBNull(RsInvoice.Fields("TRANS_DISTANCE").Value), 0, RsInvoice.Fields("TRANS_DISTANCE").Value)
            txtTransName.Text = IIf(IsDBNull(RsInvoice.Fields("CARRIERS").Value), "", RsInvoice.Fields("CARRIERS").Value)
            txtTransportCode.Text = IIf(IsDBNull(RsInvoice.Fields("TRANSPORTER_GSTNO").Value), "", RsInvoice.Fields("TRANSPORTER_GSTNO").Value)
            txtTransportDocNo.Text = IIf(IsDBNull(RsInvoice.Fields("TRANSPORTERBILLNO").Value), "", RsInvoice.Fields("TRANSPORTERBILLNO").Value)
            txtTransDocDate.Text = VB6.Format(IIf(IsDBNull(RsInvoice.Fields("TRANSPORTERBILLDATE").Value), "", RsInvoice.Fields("TRANSPORTERBILLDATE").Value), "DD/MM/YYYY")
            txtVehicleNo.Text = IIf(IsDBNull(RsInvoice.Fields("VEHICLE_NO").Value), "", RsInvoice.Fields("VEHICLE_NO").Value)
            txtResponseId.Text = IIf(IsDBNull(RsInvoice.Fields("EWAYRESPONSEID").Value), "", RsInvoice.Fields("EWAYRESPONSEID").Value)
            txteWayBillNo.Text = IIf(IsDBNull(RsInvoice.Fields("E_BILLWAYNO").Value), "", RsInvoice.Fields("E_BILLWAYNO").Value)

            txteWayBillDate.Text = VB6.Format(IIf(IsDBNull(RsInvoice.Fields("E_BILLWAYDATE").Value), "", RsInvoice.Fields("E_BILLWAYDATE").Value), "DD/MM/YYYY HH:MM")
            txteWayValidupto.Text = VB6.Format(IIf(IsDBNull(RsInvoice.Fields("E_BILLWAYVAILDUPTO").Value), "", RsInvoice.Fields("E_BILLWAYVAILDUPTO").Value), "DD/MM/YYYY HH:MM")
            lblFilepath.Text = Trim(IIf(IsDBNull(RsInvoice.Fields("E_BILLWAYFILEPATH").Value), "", RsInvoice.Fields("E_BILLWAYFILEPATH").Value))
            'lblIRNNo.Caption = Trim(IIf(IsNull(RsInvoice!IRN_NO), "", RsInvoice!IRN_NO))

            cmdResetID.Enabled = IIf(txteWayBillNo.Text = "", True, False)
            mVehicleType = IIf(IsDBNull(RsInvoice.Fields("VEHICLE_TYPE").Value), "R", RsInvoice.Fields("VEHICLE_TYPE").Value)
            cboVehicleType.SelectedIndex = IIf(mVehicleType = "R", 0, 1)

            cboTransmode.Enabled = False
            txtDistance.Enabled = False
            txtTransName.Enabled = False
            txtTransportCode.Enabled = False
            txtTransportDocNo.Enabled = False
            txtTransDocDate.Enabled = False
            txtVehicleNo.Enabled = False
            cboVehicleType.Enabled = False

            MainClass.ClearGrid(SprdMain)
            mSqlStr = "SELECT * FROM INV_GATEPASS_DET WHERE AUTO_KEY_PASSNO=" & Val(pMKey) & ""
            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            I = 0
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False

                    I = I + 1
                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    SprdMain.Row = I

                    SprdMain.Col = ColItemCode
                    SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                    mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                    SprdMain.Col = ColItemDesc
                    pProductDesc = ""
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        pProductDesc = MasterNo
                    End If

                    SprdMain.Text = pProductDesc

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "PRODTYPE_DESC", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        pProductType = MasterNo
                    End If
                    pProductType = IIf(pProductType = "", pProductDesc, pProductType)

                    SprdMain.Col = ColProductDesc
                    SprdMain.Text = VB.Left(pProductType, 80)

                    SprdMain.Col = ColHSNCode
                    SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("HSN_CODE").Value), "", RsTemp.Fields("HSN_CODE").Value)

                    SprdMain.Col = ColQty
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00")
                    mQty = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.00"))

                    SprdMain.Col = ColUnit
                    pUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                    SprdMain.Text = IIf(pUOM = "PCS", "NOS", pUOM)

                    SprdMain.Col = ColTaxableAmount
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value), "0.00")
                    mItemValue = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value), "0.00"))
                    mTotInvoiceValue = mTotInvoiceValue + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value), "0.00"))

                    SprdMain.Col = ColSGSTRate
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value), "0.00")
                    mSGSTPer = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value), "0.00"))

                    mSGSTValue = CDbl(VB6.Format(mItemValue * mSGSTPer * 0.01, "0.00"))
                    mTotSGSTValue = mTotSGSTValue + mSGSTValue

                    SprdMain.Col = ColSGSTValue
                    SprdMain.Text = VB6.Format(mSGSTValue, "0.00")


                    SprdMain.Col = ColCGSTRate
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value), "0.00")
                    mCGSTPer = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value), "0.00"))

                    mCGSTValue = CDbl(VB6.Format(mItemValue * mCGSTPer * 0.01, "0.00"))
                    mTotCGSTValue = mTotCGSTValue + mCGSTValue

                    SprdMain.Col = ColCGSTValue
                    SprdMain.Text = VB6.Format(mCGSTValue, "0.00")

                    SprdMain.Col = ColIGSTRate
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value), "0.00")
                    mIGSTPer = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value), "0.00"))
                    mIGSTValue = CDbl(VB6.Format(mItemValue * mIGSTPer * 0.01, "0.00"))
                    mTotIGSTValue = mTotIGSTValue + mIGSTValue

                    SprdMain.Col = ColIGSTValue
                    SprdMain.Text = VB6.Format(mIGSTValue, "0.00")

                    SprdMain.Col = ColCessRate
                    SprdMain.Text = "0.00"


                    RsTemp.MoveNext()
                Loop
            End If
        End If

        lblTaxableAmount.Text = VB6.Format(mTotInvoiceValue, "0.00")

        lblNetAmount.Text = VB6.Format(mTotInvoiceValue + mTotCGSTValue + mTotSGSTValue + mTotIGSTValue, "0.00")

        lblCGSTAmt.Text = VB6.Format(mTotCGSTValue, "0.00")
        lblSGSTAmt.Text = VB6.Format(mTotSGSTValue, "0.00")
        lblIGSTAmt.Text = VB6.Format(mTotIGSTValue, "0.00")


        ADDMode = False
        MODIFYMode = False

        'MainClass.ButtonStatus(Me, XRIGHT, RsInvoice, ADDMode, MODIFYMode, True,  , False)
        MainClass.ButtonStatus(Me, XRIGHT, RsInvoice, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        FormatSprdMain(-1)


        Exit Sub
ShowErrPart:
        MsgBox(Err.Description, Err.Number)
    End Sub
    Public Function GetChallanRate(ByRef mItemCode As String, ByRef pDespatchNoteNo As String, ByRef m57F4 As String) As Double
        On Error GoTo ErrPart

        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim mCustCode As String

        mCustCode = ""
        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustCode = MasterNo
        End If

        '    SqlStr = " SELECT MAX(ID.ITEM_RATE) AS  ITEM_RATE" & vbCrLf _
        ''            & " FROM DSP_PAINT57F4_HDR IH, DSP_PAINT57F4_DET ID, DSP_PAINT57F4_TRN TRN" & vbCrLf _
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        ''            & " AND IH.MKEY=ID.MKEY AND ID.MKEY = TRN.MKEY AND ID.ITEM_CODE=TRN.ITEM_CODE" & vbCrLf _
        ''            & " AND IH.PARTY_F4NO='" & MainClass.AllowSingleQuote(m57F4) & "' " & vbCrLf _
        ''            & " AND TRN.SUB_ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' " & vbCrLf _
        ''            & " AND IH.BookType='D' "

        SqlStr = " SELECT MAX(ID.ITEM_RATE) AS  ITEM_RATE" & vbCrLf & " FROM DSP_PAINT57F4_HDR IH, DSP_PAINT57F4_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.PARTY_F4NO='" & MainClass.AllowSingleQuote(m57F4) & "' " & vbCrLf & " AND IH.BookType='D' " & vbCrLf & " AND ID.ITEM_CODE IN ( " & vbCrLf & " SELECT ITEM_CODE FROM DSP_PAINT57F4_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.MKEY='" & Trim(pDespatchNoteNo) & "'" & vbCrLf & " AND TRN.SUB_ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf & " AND TRN.SUPP_CUST_CODE=IH.SUPP_CUST_CODE" & vbCrLf & " AND TRN.PARTY_F4NO=IH.PARTY_F4NO)" & vbCrLf


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetChallanRate = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value), "0.00"))
        End If

        Exit Function
ErrPart:
        GetChallanRate = 0
    End Function

    Private Function Get57F4(ByRef pDespatchNote As Double, ByRef pItemCode As String, ByRef xSubRow As Integer) As Object
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = "SELECT REF_NO,REF_DATE FROM DSP_DESPATCH_DET " & vbCrLf & " WHERE AUTO_KEY_DESP=" & pDespatchNote & "" & vbCrLf & " AND ITEM_CODE='" & pItemCode & "' AND SERIAL_NO=" & xSubRow & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Get57F4 = IIf(IsDBNull(RsTemp.Fields("REF_NO").Value), "", RsTemp.Fields("REF_NO").Value) ''& " " & IIf(IsNull(RsTemp!REF_DATE), "", RsTemp!REF_DATE)
        End If
        Exit Function
ErrPart:
        Get57F4 = ""
    End Function


    Private Sub Show1()
        On Error GoTo ShowErrPart
        Dim xMkey As String
        'Dim RsInvoice As ADODB.Recordset

        FraCmd.Enabled = True
        If Not RsInvoice.EOF Then

            If lbleWayType.Text = "I" Then
                xMkey = IIf(IsDBNull(RsInvoice.Fields("mKey").Value), "", RsInvoice.Fields("mKey").Value)

                SqlStr = "SELECT * FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MKEY='" & MainClass.AllowSingleQuote(UCase(xMkey)) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInvoice, ADODB.LockTypeEnum.adLockReadOnly)

                If RsInvoice.EOF = False Then
                    Clear1()
                    lblMKey.Text = xMkey
                    Call ShowInvoiceData()
                End If
            ElseIf lbleWayType.Text = "R" Then
                xMkey = IIf(IsDBNull(RsInvoice.Fields("AUTO_KEY_PASSNO").Value), "", RsInvoice.Fields("AUTO_KEY_PASSNO").Value)

                SqlStr = "SELECT * FROM INV_GATEPASS_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PASSNO='" & MainClass.AllowSingleQuote(UCase(xMkey)) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInvoice, ADODB.LockTypeEnum.adLockReadOnly)

                If RsInvoice.EOF = False Then
                    Clear1()
                    lblMKey.Text = xMkey
                    Call ShowRGPData()
                End If

            End If
            RsInvoice.MoveFirst()
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsInvoice, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        txtInvoiceNo.Enabled = True

        Exit Sub
ShowErrPart:

        If Err.Number = -2147418113 Then
            RsInvoice.Requery()
            '        Resume
        End If
        MsgBox(Err.Description, Err.Number)

    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If ValidateBranchLocking((txtInvoiceDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsInvoice.EOF = True Then Exit Function

        If MODIFYMode = True And Trim(txtInvoiceNo.Text) = "" Then
            MsgInformation("Invoice No. is Blank")
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtInvoiceDate.Text) = "" Then
            MsgInformation(" Invoice Date is empty. Cannot Save")
            txtInvoiceDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtInvoiceDate.Text) <> "" Then
            If IsDate(txtInvoiceDate.Text) = False Then
                MsgInformation(" Invalid Invoice Date. Cannot Save")
                txtInvoiceDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtSupplierName.Text) = "" Then
            MsgInformation("Customer Name is Blank. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboTransmode.Text) = "" Then
            MsgInformation("Trans Mode is Blank. Cannot Save")
            If cboTransmode.Enabled = True Then cboTransmode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboVehicleType.Text) = "" Then
            MsgInformation("Vehicle Type is Blank. Cannot Save")
            If cboVehicleType.Enabled = True Then cboVehicleType.Focus()
            FieldsVarification = False
            Exit Function
        End If

        '    If Trim(txtTransName.Text) = "" Then
        '        MsgInformation "Transporter Name is Blank. Cannot Save"
        '        If txtTransName.Enabled = True Then txtTransName.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        '    If Trim(txtTransportDocNo.Text) = "" Then
        '        MsgInformation "Transporter Doc No is Blank. Cannot Save"
        '        If txtTransportDocNo.Enabled = True Then txtTransportDocNo.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        '
        '    If Trim(txtTransDocDate.Text) = "" Then
        '        MsgInformation "Transporter Doc date is Blank. Cannot Save"
        '        If txtTransDocDate.Enabled = True Then txtTransDocDate.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        '
        '    If IsDate(txtTransDocDate.Text) = False Then
        '        MsgInformation "Invalid Transporter Doc date is Blank. Cannot Save"
        '        If txtTransDocDate.Enabled = True Then txtTransDocDate.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If


        If Val(txtDistance.Text) = 0 Then
            MsgInformation("Distance is empty. Cannot Save")
            If txtDistance.Enabled = True Then txtDistance.Focus()
            FieldsVarification = False
            Exit Function
        End If

        '    If MainClass.ValidDataInGrid(SprdMain, ColInvQty, "N", "Please Check Invoice Qty") = False Then FieldsVarification = False
        '    If MainClass.ValidDataInGrid(SprdDetail, ColQty, "N", "Please Check JIT Call Qty") = False Then FieldsVarification = False
        '    If MainClass.ValidDataInGrid(SprdDetail, ColJITCallNo, "N", "Please Check JIT Call No.") = False Then FieldsVarification = False
        '    If MainClass.ValidDataInGrid(SprdDetail, ColItemCode, "S", "Please Check Item Code.") = False Then FieldsVarification = False
        '    If MainClass.ValidDataInGrid(SprdDetail, ColItemDesc, "S", "Please Check Item Description.") = False Then FieldsVarification = False
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim mMKey As String
        Dim mTransMode As String
        Dim mVehicleType As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        mMKey = Trim(lblMKey.Text)

        mTransMode = VB.Left(cboTransmode.Text, 1)
        mVehicleType = VB.Left(cboVehicleType.Text, 1)

        If lbleWayType.Text = "I" Then
            SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf & " TRANSPORT_MODE='" & mTransMode & "'," & vbCrLf & " TRANS_DISTANCE=" & Val(txtDistance.Text) & "," & vbCrLf & " CARRIERS='" & MainClass.AllowSingleQuote((txtTransName.Text)) & "', VEHICLENO='" & MainClass.AllowSingleQuote((txtVehicleNo.Text)) & "'," & vbCrLf & " TRANSPORTER_GSTNO='" & MainClass.AllowSingleQuote((txtTransportCode.Text)) & "'," & vbCrLf & " VEHICLE_TYPE='" & mVehicleType & "'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY ='" & mMKey & "'"
        ElseIf lbleWayType.Text = "R" Then
            SqlStr = "UPDATE INV_GATEPASS_HDR SET " & vbCrLf & " TRANSPORT_MODE='" & mTransMode & "'," & vbCrLf & " TRANS_DISTANCE=" & Val(txtDistance.Text) & "," & vbCrLf & " CARRIERS='" & MainClass.AllowSingleQuote((txtTransName.Text)) & "', VEHICLE_NO ='" & MainClass.AllowSingleQuote((txtVehicleNo.Text)) & "'," & vbCrLf & " TRANSPORTER_GSTNO='" & MainClass.AllowSingleQuote((txtTransportCode.Text)) & "'," & vbCrLf & " VEHICLE_TYPE='" & mVehicleType & "'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PASSNO ='" & mMKey & "'"

        End If


        PubDBCn.Execute(SqlStr)


        PubDBCn.CommitTrans()

        '    If eWayBillCreateAPI(mMKey) = False Then GoTo ErrPart

        Update1 = True
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsInvoice.Requery()
        MsgBox(Err.Description)
        ''Resume
    End Function



    Public Function WebRequestCreate(ByRef pMKey As String) As Boolean
        On Error GoTo ErrPart
        Dim url As String
        Dim pUserGSTin As String
        Dim pSupplyType As String
        Dim pSubSupplyType As Integer
        Dim pDocType As String
        Dim pDocNo As String
        Dim pDocDate As String
        Dim pFromGSTin As String
        Dim pFromTrdName As String
        Dim pFromAddr1 As String
        Dim pfromAddr2 As String
        Dim pFromPlace As String
        Dim pFromPincode As Double
        Dim pFromStateCode As String
        Dim pToGstin As String
        Dim pToTrdName As String
        Dim pToAddr1 As String
        Dim pToAddr2 As String
        Dim pToPlace As String
        Dim pToCity As String
        Dim pToPincode As Double
        Dim pToStateCode As String
        Dim pTransMode As String
        Dim pTransDistance As Double
        Dim pTransporterName As String
        Dim pTransporterId As String
        Dim pTransDocNo As String
        Dim pTransDocDate As String
        Dim pVehicleNo As String
        Dim pVehicleType As String
        Dim pItemNo As Double
        Dim pProductName As String
        Dim pProductDesc As String
        Dim pHSNCode As Double
        Dim pQuantity As Double
        Dim pQtyUnit As String
        Dim pTaxableAmount As Double
        Dim pSgstRate As Double
        Dim pCgstRate As Double
        Dim pIgstRate As Double
        Dim pCessRate As Double
        Dim pcessAdvol As Double
        Dim pStateName As String
        Dim pStateCode As String
        Dim cntRow As Integer

        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim pStaus As String
        'Dim meWayResponseID  As String
        Dim mBody As String
        Dim mResponseId As String
        Dim mResponseIdStr As String
        Dim url1 As String
        Dim WebRequestGen As String

        'Dim pCompanyId As String
        'Dim pBranchId As String
        'Dim pTokenId As String
        'Dim pUserId As String



        Dim pResponseText As String
        Dim pError As String
        Dim pInvoiceValue As Double
        Dim pTaxableValue As Double
        Dim pCGSTValue As Double
        Dim pSGSTValue As Double
        Dim pIGSTValue As Double
        Dim pItemCessValue As Double

        Dim pItemCGSTValue As Double
        Dim pItemSGSTValue As Double
        Dim pItemIGSTValue As Double

        Dim mIsBillToShipToSame As String
        Dim mDispatchFromGSTIN As String
        Dim mDispatchFromTradeName As String
        Dim mShipToGSTIN As String
        Dim mShipToTradeName As String
        Dim pShipToStateCode As String
        Dim pOtherValue As Double
        Dim mIsBillFromShipFromSame As String

        Dim meWayResponseID As String
        Dim meWayBillDate As String
        Dim meWayBillUpto As String
        Dim meWayFilePath As String

        Dim pCDKey As String
        Dim pEFUserName As String
        Dim pEFPassword As String
        Dim pEWBUserName As String
        Dim pEWBPassword As String
        Dim pIRNNo As String
        Dim pIsTesing As String = "Y"

        If GetWebTeleWaySetupContents(url, "C", pCDKey, pEFUserName, pEFPassword, pEWBUserName, pEWBPassword, pIsTesing) = False Then GoTo ErrPart


        Dim http As Object  ' MSXML2.XMLHTTP60 '' MSXML.xmlhttp
        http = CreateObject("MSXML2.ServerXMLHTTP")



        pSupplyType = "O"
        pSubSupplyType = Val(VB.Left(cboSubType.Text, 2))
        pDocType = Trim(VB.Left(cboDocType.Text, 3)) '"INV"
        pDocNo = Trim(txtPreInvoice.Text) & Trim(txtInvoiceNo.Text)
        pDocDate = VB6.Format(txtInvoiceDate.Text, "DD/MM/YYYY")
        pUserGSTin = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) ''"05AAAAU3306Q1ZC" ''
        pFromGSTin = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) '"05AAAAU3306Q1ZC" ''
        pFromTrdName = IIf(IsDBNull(RsCompany.Fields("Company_Name").Value), "", RsCompany.Fields("Company_Name").Value)
        pFromAddr1 = IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value)
        pfromAddr2 = IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        pFromPlace = IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value)
        pFromPincode = IIf(IsDBNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value)
        pStateName = IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value)
        pStateCode = GetStateCode(pStateName)
        pFromStateCode = pStateCode


        mSqlStr = " SELECT SUPP_CUST_ADDR, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf & " FROM FIN_SUPP_CUST_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtSuppCustCode.Text)) & "'"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pToTrdName = Trim(txtSupplierName.Text)
            pToAddr1 = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
            pToAddr2 = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
            pToCity = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value) ''
            pToPlace = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)

            If CDbl(lblInvoiceSeqType.Text) = 6 Then
                pToGstin = "URP"
                pToPincode = CDbl("999999")
                pToStateCode = CStr(99)
            Else
                pToGstin = IIf(IsDBNull(RsTemp.Fields("GST_RGN_NO").Value), "", RsTemp.Fields("GST_RGN_NO").Value)
                pToPincode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
                pToStateCode = GetStateCode(pToPlace)
            End If


        Else
            MsgInformation("Invalid Customer Name, Please Select Valid Customer Name.")
            WebRequestCreate = False
            http = Nothing
            Exit Function
        End If

        mIsBillToShipToSame = IIf(lblShippedToSameParty.Text = "Y", "1", "0")
        mIsBillFromShipFromSame = "1"
        mDispatchFromGSTIN = ""
        mDispatchFromTradeName = ""
        mShipToGSTIN = ""
        mShipToTradeName = ""

        If mIsBillToShipToSame = "0" Then
            mSqlStr = " SELECT SUPP_CUST_ADDR,SUPP_CUST_NAME, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf & " FROM FIN_SUPP_CUST_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblShippedCode.Text) & "'"

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mShipToTradeName = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)) '' Dated 04/03/2019 Trim(txtSupplierName.Text)

                ''Ship to  Address
                pToAddr1 = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                pToAddr2 = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
                pToCity = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value) ''
                pToPlace = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)

                '
                If CDbl(lblInvoiceSeqType.Text) = 6 Then
                    mShipToGSTIN = "URP"
                    '                pToPincode = "999999"
                    pShipToStateCode = CStr(99)
                Else
                    mShipToGSTIN = IIf(IsDBNull(RsTemp.Fields("GST_RGN_NO").Value), "", RsTemp.Fields("GST_RGN_NO").Value)
                    pToPincode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
                    pShipToStateCode = GetStateCode(pToPlace)
                End If
            Else
                MsgInformation("Invalid Shipped to Customer Name, Please Select Valid Shipped To Customer Name.")
                WebRequestCreate = False
                http = Nothing
                Exit Function
            End If
        Else
            mShipToTradeName = Trim(txtSupplierName.Text)
            mShipToGSTIN = pToGstin
            pShipToStateCode = pToStateCode
        End If

        If lblDespatchFrom.Text = "Y" Then
            mSqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf & " FROM FIN_SUPP_CUST_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblShippedFromCode.Text) & "'"

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mDispatchFromTradeName = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value))
                mDispatchFromGSTIN = IIf(IsDBNull(RsTemp.Fields("GST_RGN_NO").Value), "", RsTemp.Fields("GST_RGN_NO").Value)

                ''Ship From  Address
                pFromAddr1 = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                pfromAddr2 = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
                pFromPlace = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)

                pFromPincode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
                pStateName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)
                pStateCode = GetStateCode(pStateName)
                pFromStateCode = pStateCode

                mIsBillFromShipFromSame = "0"
            Else
                MsgInformation("Invalid Shipped From Customer Name, Please Select Valid Shipped From Customer Name.")
                WebRequestCreate = False
                http = Nothing
                Exit Function
            End If
        End If


        pTransMode = VB.Left(cboTransmode.Text, 1)
        pTransDistance = Val(txtDistance.Text)
        pTransporterName = Trim(txtTransName.Text)
        pTransporterId = Trim(txtTransportCode.Text)
        pTransDocNo = Trim(txtTransportDocNo.Text)
        pTransDocDate = VB6.Format(txtTransDocDate.Text, "DD/MM/YYYY") ''IIf(pTransDocNo = "", "", Format(txtTransDocDate.Text, "DD/MM/YYYY"))
        pVehicleNo = Trim(txtVehicleNo.Text)
        pVehicleType = VB.Left(cboVehicleType.Text, 1)
        pInvoiceValue = CDbl(VB6.Format(lblNetAmount.Text, "0.00"))
        pTaxableValue = CDbl(VB6.Format(lblTaxableAmount.Text, "0.00"))

        pCGSTValue = CDbl(VB6.Format(lblCGSTAmt.Text, "0.00"))
        pSGSTValue = CDbl(VB6.Format(lblSGSTAmt.Text, "0.00"))
        pIGSTValue = CDbl(VB6.Format(lblIGSTAmt.Text, "0.00"))

        pOtherValue = CDbl(VB6.Format(pInvoiceValue - (pTaxableValue + pCGSTValue + pSGSTValue + pIGSTValue), "0.00"))

        pIRNNo = lblIRNNo.Text

        ''url = "http://ip.webtel.in/eWayGSP2/Sandbox/EWayBill/v1.3/GenEWB" ''"http://ip.webtel.in/ewaygsp2/Help/Api/POST-Sandbox-EWayBill-v1.3-GenEWB"  '' http://ip.webtel.in/eWayGSP2/Sandbox/EWayBill/v1.3/GenEWB

        '    url = "http://ewayasp.webtel.in/Ewaybill/v1.3/GENEWB"
        http.Open("POST", url, False)

        http.setRequestHeader("Content-Type", "application/json")



        'With JB
        '    .Clear()
        '    .IsArray_Renamed = False 'Actually the default after Clear.

        '    With .AddNewArray("Push_Data_List")
        '        For cntRow = 1 To SprdMain.MaxRows - 1
        '            With .AddNewObject()
        '                .Item("GSTIN") = pUserGSTin
        '                .Item("Year") = Year(CDate(pDocDate))
        '                .Item("Month") = Month(CDate(pDocDate))
        '                .Item("SupplyType") = pSupplyType
        '                .Item("SubType") = pSubSupplyType
        '                '
        '                .Item("DocType") = pDocType
        '                .Item("DocNo") = pDocNo
        '                .Item("DocDate") = VB6.Format(pDocDate, "YYYYMMDD")
        '                .Item("SupGSTIN") = pFromGSTin
        '                .Item("SupName") = pFromTrdName
        '                .Item("SupAdd1") = pFromAddr1
        '                .Item("SupAdd2") = pfromAddr2
        '                .Item("SupCity") = pfromAddr2 ''pFromPlace 'pStateName
        '                .Item("SupState") = pStateCode
        '                .Item("SupPincode") = pFromPincode

        '                .Item("RecGSTIN") = pToGstin

        '                .Item("RecName") = pToTrdName
        '                .Item("RecAdd1") = pToAddr1
        '                .Item("RecAdd2") = pToAddr2
        '                .Item("Reccity") = pToCity ''& "," & pToPlace
        '                .Item("RecState") = pToStateCode 'pToPlace
        '                .Item("Recpincode") = pToPincode



        '                .Item("TransMode") = pTransMode
        '                .Item("TransporterId") = pTransporterId
        '                .Item("TransporterName") = pTransporterName
        '                .Item("TransDistance") = pTransDistance


        '                .Item("TransDocNo") = pTransDocNo
        '                .Item("TransDocDate") = VB6.Format(pTransDocDate, "YYYYMMDD")
        '                .Item("VehicleType") = pVehicleType
        '                .Item("VehicleNo") = pVehicleNo

        '                '                .Item("reasonCode") = ""
        '                '                .Item("reasonRem") = ""
        '                '                .Item("tripshtNo") = ""

        '                SprdMain.Row = cntRow
        '                SprdMain.Col = ColItemCode
        '                pItemNo = cntRow ' Trim(.Text)

        '                SprdMain.Col = ColItemDesc
        '                pProductName = Trim(SprdMain.Text)

        '                SprdMain.Col = ColProductDesc
        '                pProductDesc = Trim(SprdMain.Text)

        '                SprdMain.Col = ColHSNCode
        '                pHSNCode = CDbl(Trim(SprdMain.Text))

        '                SprdMain.Col = ColQty
        '                pQuantity = Val(SprdMain.Text)

        '                SprdMain.Col = ColUnit
        '                pQtyUnit = Trim(SprdMain.Text)

        '                SprdMain.Col = ColTaxableAmount
        '                pTaxableAmount = Val(SprdMain.Text)

        '                SprdMain.Col = ColSGSTRate
        '                pSgstRate = Val(SprdMain.Text)

        '                SprdMain.Col = ColCGSTRate
        '                pCgstRate = Val(SprdMain.Text)

        '                SprdMain.Col = ColIGSTRate
        '                pIgstRate = Val(SprdMain.Text)

        '                SprdMain.Col = ColSGSTValue
        '                pItemSGSTValue = Val(SprdMain.Text)

        '                SprdMain.Col = ColCGSTValue
        '                pItemCGSTValue = Val(SprdMain.Text)

        '                SprdMain.Col = ColIGSTValue
        '                pItemIGSTValue = Val(SprdMain.Text)

        '                SprdMain.Col = ColCessRate
        '                pCessRate = Val(SprdMain.Text)

        '                pItemCessValue = 0
        '                pcessAdvol = CDbl("0.0")


        '                '                    .Item("ItemNo") = pItemNo
        '                .Item("ProductName") = pProductName
        '                .Item("ProductDesc") = pProductDesc
        '                .Item("HSNCode") = pHSNCode
        '                .Item("Quantity") = pQuantity
        '                .Item("QtyUnit") = pQtyUnit
        '                .Item("TaxableValue") = pTaxableAmount
        '                .Item("TotalValue") = pTaxableAmount
        '                .Item("SGSTRate") = pSgstRate
        '                .Item("SGSTValue") = pItemSGSTValue
        '                .Item("CGSTRate") = pCgstRate
        '                .Item("CGSTValue") = pItemCGSTValue
        '                .Item("IGSTRate") = pIgstRate
        '                .Item("IGSTValue") = pItemIGSTValue
        '                .Item("CessRate") = pCessRate
        '                .Item("CessValue") = pItemCessValue

        '                .Item("EWBUserName") = pEWBUserName
        '                .Item("EWBPassword") = pEWBPassword
        '                .Item("CessNonAdvol") = pcessAdvol

        '                If cboSubType.SelectedIndex = 7 Then
        '                    .Item("SubSupplyDesc") = "Others"
        '                Else
        '                    .Item("SubSupplyDesc") = ""
        '                End If

        '                .Item("ShipFromStateCode") = pFromStateCode
        '                .Item("ShipToStateCode") = pShipToStateCode
        '                .Item("TotalInvoiceValue") = pInvoiceValue
        '                .Item("CessNonAdvolValue") = 0
        '                .Item("OtherValue") = pOtherValue
        '                .Item("dispatchFromGSTIN") = mDispatchFromGSTIN
        '                .Item("dispatchFromTradeName") = mDispatchFromTradeName
        '                .Item("ShipToGSTIN") = mShipToGSTIN
        '                .Item("ShipToTradeName") = mShipToTradeName
        '                .Item("IsBillFromShipFromSame") = mIsBillFromShipFromSame
        '                .Item("IsBillToShipToSame") = mIsBillToShipToSame
        '                .Item("IsGSTINSEZ") = "0"

        '                ''                    .Item("Irnno") = pIRNNo

        '            End With
        '        Next
        '    End With
        '    .Item("Year") = Year(CDate(pDocDate))
        '    .Item("Month") = Month(CDate(pDocDate))
        '    .Item("EFUserName") = pEFUserName
        '    .Item("EFPassword") = pEFPassword
        '    .Item("CDKey") = pCDKey
        '    mBody = .JSON
        'End With

        ' shipToGSTIN String GSTIN of  Ship-To shipToTradeName String Trade Name of  Ship-To dispatchFromGSTIN String GSTIN of Dispatch-From dispatchFromTradeName String Trade Name of Dispatch-From IsBillFromShipFromSame String Required, 0 for Different BillFrom and ShipFrom, 1 for Same BillFrom and ShipFrom IsBillToShipToSame String Required, 0 for Different BillTo and ShipTo, 1 for Same BillTo and ShipTo IsGSTINSEZ

        http.Send(mBody)

        pResponseText = http.responseText
        pResponseText = Replace(pResponseText, "\", "")
        pResponseText = Replace(pResponseText, "[", "")
        pResponseText = Replace(pResponseText, "]", "")
        pResponseText = Mid(pResponseText, 2, Len(pResponseText) - 2)

        Dim JsonTest As Object
        JsonTest = JSON.parse(pResponseText)

        pStaus = JsonTest.Item("IsSuccess")

        If UCase(pStaus) = UCase("True") Then
            meWayResponseID = JsonTest.Item("EWayBill")
            meWayBillDate = JsonTest.Item("Date") 'JsonTest.Item("elements").Item(mResponseId).Item("ewayBillDate")
            meWayBillUpto = JsonTest.Item("ValidUpTo") 'JsonTest.Item("elements").Item(mResponseId).Item("validUpto")


            txteWayBillNo.Text = meWayResponseID

            txteWayBillDate.Text = VB6.Format(meWayBillDate, "DD/MM/YYYY HH:MM")
            txteWayValidupto.Text = VB6.Format(meWayBillUpto, "DD/MM/YYYY HH:MM")
            lblFilepath.Text = "" 'Trim(meWayFilePath)


            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()

            SqlStr = ""

            If lbleWayType.Text = "I" Then
                SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf & " E_BILLWAYNO ='" & Val(txteWayBillNo.Text) & "'," & vbCrLf & " E_BILLWAYDATE =TO_DATE('" & VB6.Format(txteWayBillDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf & " E_BILLWAYVAILDUPTO =TO_DATE('" & VB6.Format(txteWayValidupto.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf & " E_BILLWAYFILEPATH ='" & lblFilepath.Text & "'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY ='" & pMKey & "'"
            ElseIf lbleWayType.Text = "R" Then
                SqlStr = "UPDATE INV_GATEPASS_HDR SET " & vbCrLf & " E_BILLWAYNO ='" & Val(txteWayBillNo.Text) & "'," & vbCrLf & " E_BILLWAYDATE =TO_DATE('" & VB6.Format(txteWayBillDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf & " E_BILLWAYVAILDUPTO =TO_DATE('" & VB6.Format(txteWayValidupto.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf & " E_BILLWAYFILEPATH ='" & lblFilepath.Text & "'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PASSNO ='" & pMKey & "'"
            End If

            PubDBCn.Execute(SqlStr)
            PubDBCn.CommitTrans()
        End If

        If UCase(pStaus) = "FALSE" Then
            pError = JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")
            MsgInformation(pError)
            WebRequestCreate = False
            http = Nothing
            Exit Function
        End If

        WebRequestCreate = True
        http = Nothing
        '    Set httpGen = Nothing
        Exit Function
ErrPart:
        Resume
        WebRequestCreate = False
        http = Nothing
        MsgBox(Err.Description)
        PubDBCn.RollbackTrans()
    End Function
    Public Function WebRequestGenerate(ByRef pMKey As String) As Boolean
        On Error GoTo ErrPart
        Dim url As String
        Dim pUserGSTin As String

        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim pStaus As String

        Dim mBody As String
        Dim mResponseId As String
        Dim mResponseIdStr As String
        Dim url1 As String

        Dim pCompanyId As String
        Dim pBranchId As String
        Dim pTokenId As String
        Dim pUserId As String
        Dim pResponseIdText As String
        Dim pError As String
        Dim meWayResponseID As String
        Dim meWayBillDate As String
        Dim meWayBillUpto As String
        Dim meWayFilePath As String


        Dim http As Object '' MSXML2.XMLHTTP60 '' MSXML.xmlhttp
        http = CreateObject("MSXML2.ServerXMLHTTP")

        mResponseId = txtResponseId.Text
        mResponseIdStr = "{""ids"":[""" & mResponseId & """]}"

        If GeteWaySetupContents(url, pCompanyId, pBranchId, pTokenId, pUserId, "G") = False Then GoTo ErrPart

        http.Open("POST", url, False)
        http.setRequestHeader("Content-Type", "application/json")
        http.setRequestHeader("idCompany", pCompanyId) ''84
        http.setRequestHeader("idBranch", pBranchId) ''102
        http.setRequestHeader("token", pTokenId) '' "b9979d7e-28be-4ba1-b07e-ac5557ef3499"
        http.setRequestHeader("idUser", pUserId) ''9230
        http.Send(mResponseIdStr)

        pResponseIdText = http.responseText

        Dim JsonTest As Object
        JsonTest = JSON.parse(pResponseIdText)
        pStaus = JsonTest.Item("success") ' JsonTest.Item("status")


        If UCase(pStaus) = "0" Then 'If UCase(pStaus) = "FALSE" Then
            pError = JsonTest.Item("elements").Item(mResponseId).Item("error") & ". " & JsonTest.Item("message") ''JsonTest.Item("error") ' JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")
            MsgInformation(pError)
            WebRequestGenerate = False
            http = Nothing
            Exit Function
        End If


        meWayResponseID = JsonTest.Item("elements").Item(mResponseId).Item("ewayBillNo") '' JsonTest.Item("message")
        meWayBillDate = JsonTest.Item("elements").Item(mResponseId).Item("ewayBillDate")
        meWayBillUpto = JsonTest.Item("elements").Item(mResponseId).Item("validUpto")
        meWayFilePath = JsonTest.Item("elements").Item(mResponseId).Item("filePath")

        If meWayResponseID <> "" Then
            txteWayBillNo.Text = meWayResponseID

            txteWayBillDate.Text = VB6.Format(meWayBillDate, "DD/MM/YYYY HH:MM")
            txteWayValidupto.Text = VB6.Format(meWayBillUpto, "DD/MM/YYYY HH:MM")
            lblFilepath.Text = Trim(meWayFilePath)


            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()

            SqlStr = ""

            If lbleWayType.Text = "I" Then
                SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf & " E_BILLWAYNO ='" & Val(txteWayBillNo.Text) & "'," & vbCrLf & " E_BILLWAYDATE =TO_DATE('" & VB6.Format(txteWayBillDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf & " E_BILLWAYVAILDUPTO =TO_DATE('" & VB6.Format(txteWayValidupto.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf & " E_BILLWAYFILEPATH ='" & lblFilepath.Text & "'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY ='" & pMKey & "'"
            ElseIf lbleWayType.Text = "R" Then
                SqlStr = "UPDATE INV_GATEPASS_HDR SET " & vbCrLf & " E_BILLWAYNO ='" & Val(txteWayBillNo.Text) & "'," & vbCrLf & " E_BILLWAYDATE =TO_DATE('" & VB6.Format(txteWayBillDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf & " E_BILLWAYVAILDUPTO =TO_DATE('" & VB6.Format(txteWayValidupto.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf & " E_BILLWAYFILEPATH ='" & lblFilepath.Text & "'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PASSNO ='" & pMKey & "'"
            End If

            PubDBCn.Execute(SqlStr)
            PubDBCn.CommitTrans()
        End If
        WebRequestGenerate = True
        http = Nothing

        Exit Function
ErrPart:
        Resume

        WebRequestGenerate = False
        MsgBox(Err.Description)
        PubDBCn.RollbackTrans()
    End Function
    Private Sub ViewGrid()
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            AssignGrid(True)
            '        ADataPPOMain.Refresh
            SprdView.Refresh()
            SprdView.Focus()
            FraTop.Visible = False
            Frabot.Visible = False
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraTop.Visible = True
            Frabot.Visible = True
            SprdView.SendToBack()
        End If
        Call FormatSprdView()
        MainClass.ButtonStatus(Me, XRIGHT, RsInvoice, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmeWayBillWebtel_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsInvoice.Close()
        'PvtDBCn.Close
        RsInvoice = Nothing
        'Set PvtDBCn = Nothing
    End Sub


    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 2
        SprdView.Row = SprdView.ActiveRow
        txtInvoiceNo.Text = SprdView.Text

        txtInvoiceNo_Validating(txtInvoiceNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txtDistance_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDistance.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDistance_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDistance.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtInvoiceDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvoiceDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSupplierName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub



    Private Sub txtSupplierName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplierName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplierName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSupplierName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSupplierName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If Trim(txtSupplierName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Invalid Customer Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtInvoiceNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvoiceNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtInvoiceNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInvoiceNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtInvoiceNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInvoiceNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        'Dim RsInvoice As ADODB.Recordset
        Dim xMkey As String
        Dim pMKey As String
        Dim mInvoiceNo As String

        If Trim(txtInvoiceNo.Text) = "" Then GoTo EventExitSub

        If lbleWayType.Text = "I" Then
            mInvoiceNo = txtPreInvoice.Text & VB6.Format(txtInvoiceNo.Text, "00000000")


            SqlStr = "SELECT * FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

            SqlStr = SqlStr & " AND BILLNO='" & MainClass.AllowSingleQuote(UCase(mInvoiceNo)) & "'"

            SqlStr = SqlStr & " AND INVOICESEQTYPE IN (1,2,3,6)"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInvoice, ADODB.LockTypeEnum.adLockReadOnly)

            If MODIFYMode = True And RsInvoice.BOF = False Then xMkey = RsInvoice.Fields("mKey").Value

            If RsInvoice.EOF = False Then
                pMKey = IIf(IsDBNull(RsInvoice.Fields("mKey").Value), "", RsInvoice.Fields("mKey").Value)
                Clear1()
                lblMKey.Text = pMKey
                Call ShowInvoiceData()
            Else

                If ADDMode = False And MODIFYMode = False Then
                    MsgBox("No Such Invoice No. Click, Add for New", MsgBoxStyle.Information)
                    Cancel = True
                ElseIf MODIFYMode = True Then
                    SqlStr = "SELECT * FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MKEY='" & MainClass.AllowSingleQuote(UCase(xMkey)) & "'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInvoice, ADODB.LockTypeEnum.adLockReadOnly)
                End If
            End If
        ElseIf lbleWayType.Text = "R" Then

            If Len(txtInvoiceNo.Text) < 6 Then
                txtInvoiceNo.Text = Val(txtInvoiceNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
            End If

            mInvoiceNo = txtInvoiceNo.Text


            SqlStr = "SELECT * FROM INV_GATEPASS_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            SqlStr = SqlStr & " AND AUTO_KEY_PASSNO=" & Val(mInvoiceNo) & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInvoice, ADODB.LockTypeEnum.adLockReadOnly)

            If MODIFYMode = True And RsInvoice.BOF = False Then xMkey = RsInvoice.Fields("AUTO_KEY_PASSNO").Value

            If RsInvoice.EOF = False Then
                pMKey = IIf(IsDBNull(RsInvoice.Fields("AUTO_KEY_PASSNO").Value), "", RsInvoice.Fields("AUTO_KEY_PASSNO").Value)
                Clear1()
                lblMKey.Text = pMKey
                Call ShowRGPData()
            Else

                If ADDMode = False And MODIFYMode = False Then
                    MsgBox("No Such RGP No. Click, Add for New", MsgBoxStyle.Information)
                    Cancel = True
                ElseIf MODIFYMode = True Then
                    SqlStr = "SELECT * FROM INV_GATEPASS_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PASSNO=" & Val(xMkey) & ""

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInvoice, ADODB.LockTypeEnum.adLockReadOnly)
                End If
            End If

        End If



        cmdDistance.Enabled = True
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub frmeWayBillWebtel_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        SqlStr = ""
        MainClass.ClearGrid(SprdView)

        If lbleWayType.Text = "I" Then
            SqlStr = " SELECT DISTINCT " & vbCrLf & " IH.MKEY,IH.BILLNOSEQ, IH.BILLNO, IH.INVOICE_DATE, CMST.SUPP_CUST_NAME, IH.NETVALUE," & vbCrLf & " IH.E_BILLWAYNO, DECODE(IH.TRANSPORT_MODE,1,'ROAD',DECODE(IH.TRANSPORT_MODE,2,'RAIL',DECODE(IH.TRANSPORT_MODE,3,'AIR','SHIP'))) AS TRANS_MODE,  " & vbCrLf & " IH.TRANS_DISTANCE, IH.CARRIERS, IH.TRANSPORTER_GSTNO, IH.VEHICLENO,  DECODE(IH.VEHICLE_TYPE,'R','REGULAR','OVER DIMENSIONAL CARGO') AS VEHICLE_TYPE, " & vbCrLf & " IH.EWAYRESPONSEID " & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " --AND (IH.EWAYRESPONSEID IS NOT NULL OR EWAYRESPONSEID<>'') " & vbCrLf & " ORDER BY IH.INVOICE_DATE,IH.BILLNO"
        ElseIf lbleWayType.Text = "R" Then
            SqlStr = " SELECT DISTINCT " & vbCrLf & " IH.AUTO_KEY_PASSNO,IH.AUTO_KEY_PASSNO, IH.AUTO_KEY_PASSNO, IH.GATEPASS_DATE, CMST.SUPP_CUST_NAME, 0 AS NETVALUE, " & vbCrLf & " IH.E_BILLWAYNO, DECODE(IH.TRANSPORT_MODE,1,'ROAD',DECODE(IH.TRANSPORT_MODE,2,'RAIL',DECODE(IH.TRANSPORT_MODE,3,'AIR','SHIP'))) AS TRANS_MODE,  " & vbCrLf & " IH.TRANS_DISTANCE, IH.CARRIERS, IH.TRANSPORTER_GSTNO, IH.VEHICLE_NO,  DECODE(IH.VEHICLE_TYPE,'R','REGULAR','OVER DIMENSIONAL CARGO') AS VEHICLE_TYPE, " & vbCrLf & " IH.EWAYRESPONSEID " & vbCrLf & " FROM INV_GATEPASS_HDR IH, FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_PASSNO,LENGTH(IH.AUTO_KEY_PASSNO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " --AND (IH.EWAYRESPONSEID IS NOT NULL OR EWAYRESPONSEID<>'') " & vbCrLf & " ORDER BY IH.AUTO_KEY_PASSNO"

        End If

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()
    End Sub
    Private Sub FormatSprdView()
        With SprdView
            .Row = -1
            .set_RowHeight(0, 20)
            .set_ColWidth(0, 5)
            .Col = 1
            .set_ColWidth(1, 0)
            .ColHidden = True

            .Col = 2
            .set_ColWidth(2, 0)
            .ColHidden = True

            .set_ColWidth(3, 10)
            .set_ColWidth(4, 10)
            .set_ColWidth(5, 30)


            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub


    Private Sub FormatSprdMain(ByRef Arow As Integer)
        On Error GoTo ERR1
        Dim cntCol As Integer


        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.5)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColItemCode, 7)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 15)

            .Col = ColProductDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColProductDesc, 15)

            .Col = ColHSNCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColHSNCode, 8)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999.9999")
            .TypeFloatMin = CDbl("-999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQty, 8)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 6)

            .Col = ColTaxableAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999999999.99")
            .TypeFloatMin = CDbl("-999999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColTaxableAmount, 8)

            For cntCol = ColSGSTRate To ColIGSTValue
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMax = CDbl("999999999999.99")
                .TypeFloatMin = CDbl("-999999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 8)
            Next
            '
            '        .Col = ColIGSTRate
            '        .CellType = SS_CELL_TYPE_FLOAT
            '        .TypeFloatDecimalChar = Asc(".")
            '        .TypeFloatDecimalPlaces = 2
            '        .TypeFloatMax = "999999999999.99"
            '        .TypeFloatMin = "-999999999999.99"
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            '        .ColWidth(ColIGSTRate) = 8
            '
            '        .Col = ColCGSTRate
            '        .CellType = SS_CELL_TYPE_FLOAT
            '        .TypeFloatDecimalChar = Asc(".")
            '        .TypeFloatDecimalPlaces = 2
            '        .TypeFloatMax = "999999999999.99"
            '        .TypeFloatMin = "-999999999999.99"
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCGSTRate, 8)

            .Col = ColCessRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999999999.99")
            .TypeFloatMin = CDbl("-999999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCessRate, 8)

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColCessRate)

        End With
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
    End Sub



    Private Sub txtTransDocDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTransDocDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTransName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTransName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTransName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTransName.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtTransName.Text), "FIN_TRANSPORTER_MST", "TRANSPORTER_NAME", "TRANSPORTER_ID",  ,  , SqlStr) = True Then
            txtTransName.Text = AcName
            txtTransportCode.Text = AcName1
            If txtTransName.Enabled = True Then txtTransName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtTransName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTransName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtTransName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTransName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtTransName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtTransName_DoubleClick(txtTransName, New System.EventArgs())
    End Sub

    Private Sub txtTransportCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTransportCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtTransportCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTransportCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtTransportCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTransportDocNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTransportDocNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtVehicleNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicleNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtVehicleNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicleNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicleNo.Text, "N")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
