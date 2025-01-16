Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmServiceTaxDetail
    Inherits System.Windows.Forms.Form
    Private XRIGHT As String
    Private ADDMode As Boolean
    Private MODIFYMode As Boolean
    Private FormLoaded As Boolean
    'Private PvtDBCn As ADODB.Connection

    Private Const ColRO As Short = 1
    Private Const ColBillNo As Short = 2
    Private Const ColBillDate As Short = 3
    Private Const ColBillAmount As Short = 4
    Private Const ColServiceProv As Short = 5
    Private Const ColServiceTaxOn As Short = 6
    Private Const ColServicePerProv As Short = 7
    Private Const ColServicePerRec As Short = 8
    Private Const ColServiceTax As Short = 9
    Private Const ColCessAmount As Short = 10
    Private Const ColSHECessAmount As Short = 11
    Private Const ColSBCessAmount As Short = 12
    Private Const ColKKCessAmount As Short = 13
    Private Const ColServiceTaxAmount_Rec As Short = 14
    Private Const ColCessAmount_Rec As Short = 15
    Private Const ColSHECessAmount_Rec As Short = 16
    Private Const ColSBCessAmount_Rec As Short = 17
    Private Const ColKKCessAmount_Rec As Short = 18
    Private Const ColIsClaim As Short = 19
    Private Const ColClaimNo As Short = 20
    Private Const ColClaimDate As Short = 21

    Private Const ConRowHeight As Short = 14

    Private Sub cmdCalc_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCalc.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mBillAmount As Double
        Dim mServiceTaxOn As Double

        'Private Const ColBillAmount = 4
        'Private Const ColServiceTaxOn = 5
        'Private Const ColServiceTax = 6

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColBillAmount
                mBillAmount = Val(.Text)

                mServiceTaxOn = mBillAmount - (mBillAmount * Val(txtSTPer.Text) * 0.01)

                .Col = ColServiceTaxOn
                .Text = VB6.Format(mServiceTaxOn, "0.00")
            Next
        End With
        Call CalcTots()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        frmAtrn.lblServiceTaxDetail.Text = "False"
        Me.Hide()
        Me.Close()
        FormLoaded = False
        frmAtrn.Refresh()
    End Sub
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        CheckForEqualAmount()
    End Sub
    Private Sub CheckForEqualAmount()
        On Error GoTo ERR1
        CalcTots()


        If MainClass.ValidDataInGrid(SprdMain, ColBillNo, "S", "Bill No is must.") = False Then Exit Sub
        If MainClass.ValidDataInGrid(SprdMain, ColBillDate, "S", "Bill Date is must.") = False Then Exit Sub
        If MainClass.ValidDataInGrid(SprdMain, ColBillAmount, "S", "Bill Amount is must.") = False Then Exit Sub
        If MainClass.ValidDataInGrid(SprdMain, ColServiceProv, "S", "Service Provided is must.") = False Then Exit Sub
        '
        '    If Trim(txtServProvided.Text) = "" Then
        '        MsgInformation "Service Provided is must."
        '        Exit Sub
        '    End If

        '    If MainClass.ValidateWithMasterTable(txtServProvided.Text, "NAME", "CODE", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = False Then
        '        MsgInformation "Please Select Valid Service Provided"
        '        Exit Sub
        '    End If

        '    If Val(txtServPer.Text) = 0 Then
        '        MsgInformation "Service Tax Percent is must."
        '        Exit Sub
        '    End If

        '    If Val(txtCESSPer.Text) = 0 Then
        '        MsgInformation "Cess Percent is must."
        '        Exit Sub
        '    End If

        '    If MainClass.ValidDataInGrid(SprdMain, ColServiceTaxOn, "N", "Service Tax On is must.") = False Then Exit Sub
        '    If MainClass.ValidDataInGrid(SprdMain, ColServiceTax, "N", "Service Tax On is must.") = False Then Exit Sub
        '    If MainClass.ValidDataInGrid(SprdMain, ColCessAmount, "N", "Service Tax On is must.") = False Then Exit Sub

        If Val(lblServiceTaxAmt.Text) = Val(lblAmount.Text) Then
            frmAtrn.lblServiceTaxDetail.Text = "True"

            UpdateTempSTDetail()
            Me.Hide()
            '        Unload Me
            FormLoaded = False
            frmAtrn.Refresh()
        Else
            MsgInformation("Paid Service Tax Amount Not Match.")
            Exit Sub
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        On Error GoTo ERR1
        Dim cntRow As Integer
        Dim mServiceName As String
        Dim mReverseChargeApp As String
        Dim mReverseChargePer As Double

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColServiceProv
                mServiceName = Trim(.Text)
                mReverseChargePer = 0


                If mServiceName <> "" Then
                    If MainClass.ValidateWithMasterTable(mServiceName, "NAME", "REVERSE_CHARGE_APP", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mReverseChargeApp = MasterNo
                        If Trim(mReverseChargeApp) = "Y" Then
                            If MainClass.ValidateWithMasterTable(mServiceName, "NAME", "REVERSE_CHARGE_PER", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mReverseChargePer = Val(MasterNo)
                            End If
                        Else
                            mReverseChargePer = 0
                        End If
                    End If

                    .Row = cntRow

                    .Col = ColServicePerProv
                    .Text = CStr(100 - mReverseChargePer)

                    .Col = ColServicePerRec
                    .Text = CStr(mReverseChargePer)
                End If
            Next
        End With
        CalcTots()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        If FormLoaded = True Then
            CalcTots()
        End If
    End Sub

    Private Sub txtCESSPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCessPer.TextChanged

        'MainClass.SaveStatus(frmAtrn, lblADDMode.Text, lblModifyMode.Text)
    End Sub

    Private Sub txtCESSPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCessPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCESSPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCessPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Val(txtCESSPer.Text) = 0 Then GoTo EventExitSub
        Call CalcTots()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtKKCessPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtKKCessPer.TextChanged

        'MainClass.SaveStatus(frmAtrn, lblADDMode.Text, lblModifyMode.Text)
    End Sub
    Private Sub txtKKCessPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtKKCessPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtKKCessPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtKKCessPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Val(txtKKCessPer.Text) = 0 Then GoTo EventExitSub
        Call CalcTots()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSBCessPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSBCessPer.TextChanged

        'MainClass.SaveStatus(frmAtrn, lblADDMode.Text, lblModifyMode.Text)
    End Sub

    Private Sub txtSBCessPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSBCessPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSBCessPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSBCessPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Val(txtSBCessPer.Text) = 0 Then GoTo EventExitSub
        Call CalcTots()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtServPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServPer.TextChanged

        'MainClass.SaveStatus(frmAtrn, lblADDMode.Text, lblModifyMode.Text)
    End Sub


    Private Sub txtServPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtServPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtServPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Val(txtServPer.Text) = 0 Then GoTo EventExitSub
        Call CalcTots()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SearchProvidedMaster(ByRef Col As Integer, ByRef Row As Integer)

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mString As String

        SprdMain.Row = Row
        SprdMain.Col = Col
        mString = SprdMain.Text
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.SearchGridMaster(mString, "FIN_SERVPROV_MST", "NAME", , , , SqlStr)

        If AcName <> "" Then
            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = Col
            SprdMain.Text = AcName
        End If

        SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(Col, SprdMain.ActiveRow, Col, SprdMain.ActiveRow, False))


        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function UpdateTempSTDetail() As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTempPRDetail As ADODB.Recordset
        Dim cntRow As Short
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillAmount As Double
        Dim mServiceTaxOn As Double
        Dim mServiceTax As Double
        Dim mCessAmount As Double
        Dim mSHECESSAmount As Double
        Dim mISClaim As String
        Dim mClaimNo As String
        Dim mClaimDate As String
        Dim mRO As String
        Dim mServProvided As String

        Dim mServicePerProv As Double
        Dim mServicePerRec As Double
        Dim mServiceTaxOn_Rec As Double
        Dim mCessAmount_Rec As Double
        Dim mSHECessAmount_Rec As Double

        Dim mSBCessAmount As Double
        Dim mSBCessAmount_Rec As Double
        Dim mSBCessPer_Rec As Double

        Dim mKKCessAmount As Double
        Dim mKKCessAmount_Rec As Double
        Dim mKKCessPer_Rec As Double

        UpdateTempSTDetail = False
        SqlStr = "DELETE FIN_TEMP_SERVICE_TRN  Where UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND AccountCode='" & MainClass.AllowSingleQuote(lblAccountCode.Text) & "'" '' & vbCrLf |            & " AND BookType='" & UCase(Trim(lblBookType.text)) & "'  "

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColRO
                mRO = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColBillNo
                mBillNo = Trim(.Text)

                .Col = ColBillDate
                mBillDate = Trim(.Text)

                .Col = ColBillAmount
                mBillAmount = Val(.Text)

                .Col = ColServiceTaxOn
                mServiceTaxOn = Val(.Text)

                .Col = ColServicePerProv
                mServicePerProv = Val(.Text)

                .Col = ColServicePerRec
                mServicePerRec = Val(.Text)

                .Col = ColServiceTaxAmount_Rec
                mServiceTaxOn_Rec = Val(.Text)

                .Col = ColCessAmount_Rec
                mCessAmount_Rec = Val(.Text)

                .Col = ColSHECessAmount_Rec
                mSHECessAmount_Rec = Val(.Text)

                .Col = ColSBCessAmount
                mSBCessAmount = Val(.Text)

                .Col = ColSBCessAmount_Rec
                mSBCessAmount_Rec = Val(.Text)

                .Col = ColKKCessAmount
                mKKCessAmount = Val(.Text)

                .Col = ColKKCessAmount_Rec
                mKKCessAmount_Rec = Val(.Text)

                .Col = ColServiceTax
                mServiceTax = Val(.Text)

                .Col = ColCessAmount
                mCessAmount = Val(.Text)

                .Col = ColSHECessAmount
                mSHECESSAmount = Val(.Text)

                .Col = ColIsClaim
                mISClaim = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColClaimNo
                mClaimNo = Trim(.Text)

                .Col = ColClaimDate
                mClaimDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColServiceProv
                mServProvided = Trim(.Text)

                If mServiceTaxOn = 0 Then GoTo NextRow

                SqlStr = "INSERT INTO FIN_TEMP_SERVICE_TRN  ( " & vbCrLf & " USERID, SUBROWNO, ACCOUNTCODE, " & vbCrLf & " RO, BILLNO, BILLDATE, BILLAMOUNT, " & vbCrLf & " TAX_ON, SERVICETAX_AMT, CESS_AMT, " & vbCrLf & " SERV_PROV, ISSERVICECLAIM, SERVNO, " & vbCrLf & " SERVDATE, SERVICE_PER, CESS_PER, SHE_CESS_PER, SHE_CESS_AMT, " & vbCrLf & " SERVICE_PER_PROV, SERVICE_PER_REC, " & vbCrLf & " SERVICETAX_REC_AMT, CESS_REC_AMT, SHE_CESS_REC_AMT,  " & vbCrLf & " SWACHH_CESS_PER, SWACHH_CESS_AMOUNT, SWACHH_CESS_AMOUNT_REC, " & vbCrLf & " KK_CESS_PER, KK_CESS_AMOUNT, KK_CESS_AMOUNT_REC " & vbCrLf & " ) VALUES ( "

                SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & cntRow & ", '" & MainClass.AllowSingleQuote(lblAccountCode.Text) & "', " & vbCrLf & " '" & mRO & "', '" & MainClass.AllowSingleQuote(mBillNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "')," & vbCrLf & " " & mBillAmount & ", " & vbCrLf & " " & mServiceTaxOn & ", " & mServiceTax & ", " & mCessAmount & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mServProvided) & "', '" & mISClaim & "', " & Val(mClaimNo) & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClaimDate, "DD-MMM-YYYY") & "')," & vbCrLf & " " & Val(txtServPer.Text) & "," & vbCrLf & " " & Val(txtCessPer.Text) & ", " & Val(txtSHECessPer.Text) & "," & Val(CStr(mSHECESSAmount)) & "," & vbCrLf & " " & Val(CStr(mServicePerProv)) & ", " & Val(CStr(mServicePerRec)) & ", " & vbCrLf & " " & Val(CStr(mServiceTaxOn_Rec)) & ", " & Val(CStr(mCessAmount_Rec)) & "," & Val(CStr(mSHECessAmount_Rec)) & "," & vbCrLf & " " & Val(txtSBCessPer.Text) & ", " & Val(CStr(mSBCessAmount)) & "," & Val(CStr(mSBCessAmount_Rec)) & "," & vbCrLf & " " & Val(txtKKCessPer.Text) & ", " & Val(CStr(mKKCessAmount)) & "," & Val(CStr(mKKCessAmount_Rec)) & "" & vbCrLf & " )"

                PubDBCn.Execute(SqlStr)

NextRow:
            Next
        End With
        UpdateTempSTDetail = True
        Exit Function
ERR1:
        UpdateTempSTDetail = False
        MsgInformation(Err.Description)
        '    Resume
    End Function



    Private Sub frmServiceTaxDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If MainClass.ValidateWithMasterTable((lblAccountName.Text), "SUPP_CUST_NAME", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblAccountCode.Text = MasterNo
        Else
            ErrorMsg("Invalid Account Name", "", MsgBoxStyle.Information)
        End If

        If FormLoaded = False Then
            FormatSprdMain(-1, False)
            Show1()
            FormLoaded = True
        End If
    End Sub

    Private Sub frmServiceTaxDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ERR1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        Call SetChildFormCordinate(Me)
        ADDMode = False
        MODIFYMode = False
        FormLoaded = False


        '    txtServProvided.Text = ""

        XRIGHT = "AMD"
        FormatSprdMain(-1, False)
        MainClass.SetControlsColor(Me)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub Show1()

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTempPRDetail As ADODB.Recordset = Nothing
        Dim RsTempSTDetail As ADODB.Recordset = Nothing
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBillAmount As Double
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mISClaim As String
        Dim mServiceProviderCode As Double
        Dim mServProvidedName As String

        Dim mRO As String
        Dim mSERVNo As Double

        '    txtServProvided.Text = ""
        txtServPer.Text = ""
        txtCESSPer.Text = ""
        txtSHECessPer.Text = ""
        txtSBCessPer.Text = ""
        txtKKCessPer.Text = ""
        txtSTPer.Text = CStr(0)

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        If lblBookType.Text = ConBankPayment Then
            With SprdMain
                .Row = 1
                .Col = ColBillNo
                mBillNo = UCase(VB6.Format(lblVDate.Text, "MMMYYYYDD"))

                .Text = mBillNo

                .Col = ColBillDate
                mBillDate = lblVDate.Text
                .Text = mBillDate

                .Col = ColBillAmount
                mBillAmount = CDbl(lblBillAmount.Text)
                .Text = VB6.Format(mBillAmount, "0.00")

                Call FillServiceTaxDetail(.MaxRows, mBillNo, mBillDate, mBillAmount)
                .MaxRows = .MaxRows + 1
            End With
            CalcTots()
            FormatSprdMain(-1, True)
            Exit Sub
        End If

        SqlStr = "Select * From FIN_TEMPBILL_TRN " & vbCrLf & " Where UserID='" & PubUserID & "'" & vbCrLf & " AND AccountCode = '" & MainClass.AllowSingleQuote(lblAccountCode.Text) & "' AND BookType='" & Trim(lblBookType.Text) & "' " & vbCrLf & " ORDER BY BILLDATE,BILLNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPRDetail, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempPRDetail.EOF = True Then Exit Sub

        FormatSprdMain(-1, False)
        With SprdMain
            If MainClass.ValidateWithMasterTable(lblAccountCode.Text, "SUPP_CUST_CODE", "SERVPROV_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mServiceProviderCode = Val(IIf(IsDbNull(MasterNo) Or Trim(MasterNo) = "", -1, MasterNo))
            End If
            If MainClass.ValidateWithMasterTable(mServiceProviderCode, "CODE", "NAME", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mServProvidedName = MasterNo
            End If

            Do While RsTempPRDetail.EOF = False
                .Row = .MaxRows
                .Col = ColBillNo
                mBillNo = IIf(IsDbNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value)
                .Text = mBillNo

                .Col = ColBillDate
                mBillDate = IIf(IsDbNull(RsTempPRDetail.Fields("BILLDATE").Value), "", VB6.Format(RsTempPRDetail.Fields("BILLDATE").Value, "dd/mm/yyyy"))
                .Text = mBillDate

                .Col = ColBillAmount
                mBillAmount = IIf(IsDbNull(RsTempPRDetail.Fields("Amount").Value), 0, RsTempPRDetail.Fields("Amount").Value)
                .Text = VB6.Format(mBillAmount, "0.00")

                Call FillServiceTaxDetail(.MaxRows, mBillNo, mBillDate, mBillAmount)

                '            SqlStr = "Select * From  FIN_TEMP_SERVICE_TRN " & vbCrLf _
                ''                & " Where UserID='" & PubUserID & "'" & vbCrLf _
                ''                & " AND ACCOUNTCODE = '" & MainClass.AllowSingleQuote(lblAccountCode.text) & "' " & vbCrLf _
                ''                & " AND BILLNO = '" & MainClass.AllowSingleQuote(mBillNo) & "' " & vbCrLf _
                ''                & " AND BILLDATE = '" & vb6.Format(mBillDate, "DD-MMM-YYYY") & "' " & vbCrLf _
                ''                & " ORDER BY BILLDATE,BILLNO"
                '            MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTempSTDetail, adLockReadOnly
                '
                '            If RsTempSTDetail.EOF = False Then
                '                txtServPer.Text = IIf(IsNull(RsTempSTDetail!SERVICE_PER), "0.00", RsTempSTDetail!SERVICE_PER)
                '                txtCessPer.Text = IIf(IsNull(RsTempSTDetail!CESS_PER), "0.00", RsTempSTDetail!CESS_PER)
                '                txtSHECessPer.Text = IIf(IsNull(RsTempSTDetail!SHE_CESS_PER), "0.00", RsTempSTDetail!SHE_CESS_PER)
                '                Do While RsTempSTDetail.EOF = False
                '                    .Row = .MaxRows
                '
                '
                '                    .Col = ColBillNo
                '                    .Text = mBillNo     '' Format(IIf(IsNull(RsTempSTDetail!BILLNO), 0, RsTempSTDetail!BILLNO))
                '
                '                    .Col = ColBillDate
                '                    .Text = mBillDate       ''Format(IIf(IsNull(RsTempSTDetail!BILLDATE), 0, RsTempSTDetail!BILLDATE), "dd/mm/yyyy")
                '
                '                    .Col = ColBillAmount
                '                    .Text = mBillAmount         ''IIf(IsNull(RsTempPRDetail!BILLAMOUNT), 0, RsTempPRDetail!BILLAMOUNT)
                '
                '                    .Col = ColRO
                '                    mRO = IIf(IsNull(RsTempSTDetail!RO), "N", RsTempSTDetail!RO)
                '                    .Value = IIf(mRO = "N", vbUnchecked, vbChecked)
                '
                '                    .Col = ColServiceTaxOn
                '                    .Text = Format(IIf(IsNull(RsTempSTDetail!TAX_ON), 0, RsTempSTDetail!TAX_ON), "0.00")
                '
                '                    .Col = ColServiceTax
                '                    .Text = Format(IIf(IsNull(RsTempSTDetail!SERVICETAX_AMT), 0, RsTempSTDetail!SERVICETAX_AMT), "0.00")
                '
                '                    .Col = ColCessAmount
                '                    .Text = Format(IIf(IsNull(RsTempSTDetail!CESS_AMT), 0, RsTempSTDetail!CESS_AMT), "0.00")
                '
                '                    .Col = ColIsClaim
                '                    mISClaim = IIf(IsNull(RsTempSTDetail!ISSERVICECLAIM), "N", RsTempSTDetail!ISSERVICECLAIM)
                '                    .Value = IIf(mISClaim = "N", vbUnchecked, vbChecked)
                '
                '                    .Col = ColClaimNo
                '                    mServNo = IIf(IsNull(RsTempSTDetail!SERVNO), 0, RsTempSTDetail!SERVNO)
                '                    If mServNo > 0 Then
                '                        .Text = Str(mServNo)
                '                    End If
                '
                '                    .Col = ColClaimDate
                '                    .Text = Format(IIf(IsNull(RsTempSTDetail!SERVDATE), "", RsTempSTDetail!SERVDATE), "DD/MM/YYYY")
                '
                '                    .Col = ColServiceProv
                '                    .Text = IIf(IsNull(RsTempSTDetail!SERV_PROV), "", RsTempSTDetail!SERV_PROV)
                '                    RsTempSTDetail.MoveNext
                '                    If RsTempSTDetail.EOF = False Then
                '                        .MaxRows = .MaxRows + 1
                '                    End If
                '                Loop
                ''                txtServProvided.Text = IIf(IsNull(RsTempSTDetail!SERV_PROV), "", RsTempSTDetail!SERV_PROV)
                '
                '            Else
                '                .Col = ColServiceProv
                '                .Text = mServProvidedName
                '
                '                .Col = ColServiceTaxOn
                '                .Text = Format(mBillAmount, "0.00")
                '            End If

                .MaxRows = .MaxRows + 1

                RsTempPRDetail.MoveNext()
            Loop
            '        ProtectUnProtectCell Left(mPayType, 1), -1
            CalcTots()
            FormatSprdMain(-1, True)
            '        If SprdMain.Visible = True Then MainClass.SetFocusToCell SprdMain, 1, ColBillNo
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub FillServiceTaxDetail(ByRef pRow As Integer, ByRef pBillNo As String, ByRef pBillDate As String, ByRef pBillAmount As Double)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTempSTDetail As ADODB.Recordset = Nothing
        Dim mISClaim As String
        Dim mServProvidedName As String = ""
        Dim mRO As String
        Dim mSERVNo As Double


        SqlStr = "Select * From  FIN_TEMP_SERVICE_TRN " & vbCrLf & " Where UserID='" & PubUserID & "'" & vbCrLf & " AND ACCOUNTCODE = '" & MainClass.AllowSingleQuote(lblAccountCode.Text) & "' " & vbCrLf & " AND BILLNO = '" & MainClass.AllowSingleQuote(pBillNo) & "' " & vbCrLf & " AND BILLDATE = '" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "' " & vbCrLf & " ORDER BY BILLDATE,BILLNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempSTDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdMain
            If RsTempSTDetail.EOF = False Then
                txtServPer.Text = IIf(IsDbNull(RsTempSTDetail.Fields("SERVICE_PER").Value), "0.00", RsTempSTDetail.Fields("SERVICE_PER").Value)
                txtCESSPer.Text = IIf(IsDbNull(RsTempSTDetail.Fields("CESS_PER").Value), "0.00", RsTempSTDetail.Fields("CESS_PER").Value)
                txtSHECessPer.Text = IIf(IsDbNull(RsTempSTDetail.Fields("SHE_CESS_PER").Value), "0.00", RsTempSTDetail.Fields("SHE_CESS_PER").Value)
                txtSBCessPer.Text = IIf(IsDbNull(RsTempSTDetail.Fields("SWACHH_CESS_PER").Value), "0.00", RsTempSTDetail.Fields("SWACHH_CESS_PER").Value)
                txtKKCessPer.Text = IIf(IsDbNull(RsTempSTDetail.Fields("KK_CESS_PER").Value), "0.00", RsTempSTDetail.Fields("KK_CESS_PER").Value)
                Do While RsTempSTDetail.EOF = False
                    .Row = pRow


                    .Col = ColBillNo
                    .Text = pBillNo '' Format(IIf(IsNull(RsTempSTDetail!BILLNO), 0, RsTempSTDetail!BILLNO))

                    .Col = ColBillDate
                    .Text = pBillDate ''Format(IIf(IsNull(RsTempSTDetail!BILLDATE), 0, RsTempSTDetail!BILLDATE), "dd/mm/yyyy")

                    .Col = ColBillAmount
                    .Text = CStr(pBillAmount) ''IIf(IsNull(RsTempPRDetail!BILLAMOUNT), 0, RsTempPRDetail!BILLAMOUNT)

                    .Col = ColRO
                    mRO = IIf(IsDbNull(RsTempSTDetail.Fields("RO").Value), "N", RsTempSTDetail.Fields("RO").Value)
                    .Value = IIf(mRO = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                    .Col = ColServiceTaxOn
                    .Text = VB6.Format(IIf(IsDbNull(RsTempSTDetail.Fields("TAX_ON").Value), 0, RsTempSTDetail.Fields("TAX_ON").Value), "0.00")

                    .Col = ColServicePerProv
                    .Text = VB6.Format(IIf(IsDbNull(RsTempSTDetail.Fields("SERVICE_PER_PROV").Value), 0, RsTempSTDetail.Fields("SERVICE_PER_PROV").Value), "0.00")

                    .Col = ColServicePerRec
                    .Text = VB6.Format(IIf(IsDbNull(RsTempSTDetail.Fields("SERVICE_PER_REC").Value), 0, RsTempSTDetail.Fields("SERVICE_PER_REC").Value), "0.00")

                    .Col = ColServiceTax
                    .Text = VB6.Format(IIf(IsDbNull(RsTempSTDetail.Fields("SERVICETAX_AMT").Value), 0, RsTempSTDetail.Fields("SERVICETAX_AMT").Value), "0.00")

                    .Col = ColCessAmount
                    .Text = VB6.Format(IIf(IsDbNull(RsTempSTDetail.Fields("CESS_AMT").Value), 0, RsTempSTDetail.Fields("CESS_AMT").Value), "0.00")

                    .Col = ColSHECessAmount
                    .Text = VB6.Format(IIf(IsDbNull(RsTempSTDetail.Fields("SHE_CESS_AMT").Value), 0, RsTempSTDetail.Fields("SHE_CESS_AMT").Value), "0.00")

                    .Col = ColSBCessAmount
                    .Text = VB6.Format(IIf(IsDbNull(RsTempSTDetail.Fields("SWACHH_CESS_AMOUNT").Value), 0, RsTempSTDetail.Fields("SWACHH_CESS_AMOUNT").Value), "0.00")

                    .Col = ColKKCessAmount
                    .Text = VB6.Format(IIf(IsDbNull(RsTempSTDetail.Fields("KK_CESS_AMOUNT").Value), 0, RsTempSTDetail.Fields("KK_CESS_AMOUNT").Value), "0.00")

                    .Col = ColIsClaim
                    mISClaim = IIf(IsDbNull(RsTempSTDetail.Fields("ISSERVICECLAIM").Value), "N", RsTempSTDetail.Fields("ISSERVICECLAIM").Value)
                    .Value = IIf(mISClaim = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                    .Col = ColClaimNo
                    mSERVNo = IIf(IsDbNull(RsTempSTDetail.Fields("SERVNO").Value), 0, RsTempSTDetail.Fields("SERVNO").Value)
                    If mSERVNo > 0 Then
                        .Text = Str(mSERVNo)
                    End If

                    .Col = ColClaimDate
                    .Text = VB6.Format(IIf(IsDbNull(RsTempSTDetail.Fields("SERVDATE").Value), "", RsTempSTDetail.Fields("SERVDATE").Value), "DD/MM/YYYY")

                    .Col = ColServiceProv
                    .Text = IIf(IsDbNull(RsTempSTDetail.Fields("SERV_PROV").Value), "", RsTempSTDetail.Fields("SERV_PROV").Value)

                    .Col = ColServiceTaxAmount_Rec
                    .Text = VB6.Format(IIf(IsDbNull(RsTempSTDetail.Fields("SERVICETAX_REC_AMT").Value), 0, RsTempSTDetail.Fields("SERVICETAX_REC_AMT").Value), "0.00")

                    .Col = ColCessAmount_Rec
                    .Text = VB6.Format(IIf(IsDbNull(RsTempSTDetail.Fields("CESS_REC_AMT").Value), 0, RsTempSTDetail.Fields("CESS_REC_AMT").Value), "0.00")

                    .Col = ColSHECessAmount_Rec
                    .Text = VB6.Format(IIf(IsDbNull(RsTempSTDetail.Fields("SHE_CESS_REC_AMT").Value), 0, RsTempSTDetail.Fields("SHE_CESS_REC_AMT").Value), "0.00")

                    .Col = ColSBCessAmount_Rec
                    .Text = VB6.Format(IIf(IsDbNull(RsTempSTDetail.Fields("SWACHH_CESS_AMOUNT_REC").Value), 0, RsTempSTDetail.Fields("SWACHH_CESS_AMOUNT_REC").Value), "0.00")

                    .Col = ColKKCessAmount_Rec
                    .Text = VB6.Format(IIf(IsDbNull(RsTempSTDetail.Fields("KK_CESS_AMOUNT_REC").Value), 0, RsTempSTDetail.Fields("KK_CESS_AMOUNT_REC").Value), "0.00")

                    RsTempSTDetail.MoveNext()
                    If RsTempSTDetail.EOF = False Then
                        .MaxRows = .MaxRows + 1
                        pRow = pRow + 1
                    End If
                Loop
                '                txtServProvided.Text = IIf(IsNull(RsTempSTDetail!SERV_PROV), "", RsTempSTDetail!SERV_PROV)

            Else
                .Row = pRow
                .Col = ColServiceProv
                .Text = mServProvidedName

                .Col = ColServiceTaxOn
                .Text = VB6.Format(pBillAmount, "0.00")
            End If
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer, ByRef mFromPopulate As Boolean)

        On Error GoTo ErrPart
        Dim cntCol As Integer

        With SprdMain
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = 0
            .set_ColWidth(0, 3)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(.Col, 7)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(.Col, 8)

            For cntCol = ColBillAmount To ColBillAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 7.5)
            Next

            .Col = ColServiceProv
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(.Col, 14)

            For cntCol = ColServiceTaxOn To ColKKCessAmount_Rec
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 7.4)
            Next

            .Col = ColClaimNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColClaimNo, 7)

            .Col = ColClaimDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(ColClaimDate, 8)

            .Row = Arow
            .Col = ColRO
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColRO, 2)
            If mFromPopulate = False Then
                .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            End If

            .Row = Arow
            .Col = ColIsClaim
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColIsClaim, 2)
            If mFromPopulate = False Then
                .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            End If

            .set_ColWidth(ColServicePerProv, 5.4)
            .set_ColWidth(ColServicePerRec, 6)

            .Col = ColServiceTaxAmount_Rec
            .ColHidden = True

            .Col = ColCessAmount_Rec
            .ColHidden = True

            .Col = ColSHECessAmount_Rec
            .ColHidden = True

            .Col = ColSBCessAmount_Rec
            .ColHidden = True

            .Col = ColKKCessAmount_Rec
            .ColHidden = True

            '        .Row = Arow

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColBillDate, ColBillAmount)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColServiceTax, ColSBCessAmount)

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColServicePerProv, ColServicePerRec)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColServiceTaxAmount_Rec, ColKKCessAmount_Rec)

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColIsClaim, ColClaimDate)
            MainClass.SetSpreadColor(SprdMain, Arow)


        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub


    Private Sub CalcTots()
        On Error GoTo ErrSprdTotal
        Dim mRO As String
        Dim mServiceOn As Double
        Dim mServiceTax As Double
        Dim mCessAmount As Double
        Dim mSHECESSAmount As Double
        Dim mTotServiceTaxAmt As Double
        Dim mTotCessAmt As Double
        Dim mTotSHECessAmt As Double
        Dim mSBCessAmount As Double
        Dim mNetAmt As Double
        Dim cntRow As Integer
        Dim mServicePerProv As Double
        Dim mServicePerRec As Double
        Dim mServiceOnRec As Double
        Dim mSBCessAmount_Rec As Double
        Dim mTotSBCessAmount As Double
        Dim mKKCessAmount As Double
        Dim mKKCessAmount_Rec As Double
        Dim mTotKKCessAmount As Double

        With SprdMain
            For cntRow = 1 To .MaxRows - 1 Step 1
                .Row = cntRow
                .Col = ColRO
                mRO = .Value

                .Col = ColServicePerProv
                mServicePerProv = Val(.Text)

                .Col = ColServicePerRec
                mServicePerRec = Val(.Text)

                .Col = ColServiceTaxOn
                mServiceOn = Val(.Text) * mServicePerProv * 0.01
                mServiceOnRec = Val(.Text) * mServicePerRec * 0.01

                .Col = ColServiceTax
                mServiceTax = CDbl(VB6.Format(mServiceOn * Val(txtServPer.Text) * 0.01, "0.00"))
                If mRO = "1" Then
                    mServiceTax = System.Math.Round(mServiceTax, 0)
                End If
                .Text = VB6.Format(mServiceTax, "0.00")
                mTotServiceTaxAmt = mTotServiceTaxAmt + mServiceTax

                .Col = ColCessAmount
                mCessAmount = CDbl(VB6.Format(mServiceTax * Val(txtCESSPer.Text) * 0.01, "0.00"))
                If mRO = "1" Then
                    mCessAmount = System.Math.Round(mCessAmount, 0)
                End If
                .Text = VB6.Format(mCessAmount, "0.00")
                mTotCessAmt = mTotCessAmt + mCessAmount

                .Col = ColSHECessAmount
                mSHECESSAmount = CDbl(VB6.Format(mServiceTax * Val(txtSHECessPer.Text) * 0.01, "0.00"))
                If mRO = "1" Then
                    mSHECESSAmount = System.Math.Round(mSHECESSAmount, 0)
                End If
                .Text = VB6.Format(mSHECESSAmount, "0.00")
                mTotSHECessAmt = mTotSHECessAmt + mSHECESSAmount

                .Col = ColSBCessAmount
                mSBCessAmount = CDbl(VB6.Format(mServiceOn * Val(txtSBCessPer.Text) * 0.01, "0.00"))
                If mRO = "1" Then
                    mSBCessAmount = System.Math.Round(mSBCessAmount, 0)
                End If
                .Text = VB6.Format(mSBCessAmount, "0.00")
                mTotSBCessAmount = mTotSBCessAmount + mSBCessAmount

                .Col = ColKKCessAmount
                mKKCessAmount = CDbl(VB6.Format(mServiceOn * Val(txtKKCessPer.Text) * 0.01, "0.00"))
                If mRO = "1" Then
                    mKKCessAmount = System.Math.Round(mKKCessAmount, 0)
                End If
                .Text = VB6.Format(mKKCessAmount, "0.00")
                mTotKKCessAmount = mTotKKCessAmount + mKKCessAmount

                .Col = ColServiceTaxAmount_Rec
                mServiceTax = CDbl(VB6.Format(mServiceOnRec * Val(txtServPer.Text) * 0.01, "0.00"))
                If mRO = "1" Then
                    mServiceTax = System.Math.Round(mServiceTax, 0)
                End If
                .Text = VB6.Format(mServiceTax, "0.00")

                .Col = ColCessAmount_Rec
                mCessAmount = CDbl(VB6.Format(mServiceTax * Val(txtCESSPer.Text) * 0.01, "0.00"))
                If mRO = "1" Then
                    mCessAmount = System.Math.Round(mCessAmount, 0)
                End If
                .Text = VB6.Format(mCessAmount, "0.00")

                .Col = ColSHECessAmount_Rec
                mSHECESSAmount = CDbl(VB6.Format(mServiceTax * Val(txtSHECessPer.Text) * 0.01, "0.00"))
                If mRO = "1" Then
                    mSHECESSAmount = System.Math.Round(mSHECESSAmount, 0)
                End If
                .Text = VB6.Format(mSHECESSAmount, "0.00")

                .Col = ColSBCessAmount_Rec
                mSBCessAmount_Rec = CDbl(VB6.Format(mServiceOnRec * Val(txtSBCessPer.Text) * 0.01, "0.00"))
                If mRO = "1" Then
                    mSBCessAmount_Rec = System.Math.Round(mSBCessAmount_Rec, 0)
                End If
                .Text = VB6.Format(mSBCessAmount_Rec, "0.00")

                .Col = ColKKCessAmount_Rec
                mKKCessAmount_Rec = CDbl(VB6.Format(mServiceOnRec * Val(txtKKCessPer.Text) * 0.01, "0.00"))
                If mRO = "1" Then
                    mKKCessAmount_Rec = System.Math.Round(mKKCessAmount_Rec, 0)
                End If
                .Text = VB6.Format(mKKCessAmount_Rec, "0.00")

NextRow:
            Next cntRow
        End With
        '    Call FormatSprdMain(-1, False)
        mNetAmt = mTotServiceTaxAmt + mTotCessAmt + mTotSHECessAmt + mTotSBCessAmount + mTotKKCessAmount

        lblServiceTaxAmt.Text = VB6.Format(mTotServiceTaxAmt, "0.00")
        lblCessAmt.Text = VB6.Format(mTotCessAmt, "0.00")
        lblSHECessAmt.Text = VB6.Format(mTotSHECessAmt, "0.00")
        lblSBCessAmount.Text = VB6.Format(mTotSBCessAmount, "0.00")
        lblKKCessAmount.Text = VB6.Format(mTotKKCessAmount, "0.00")
        LblNetAmt.Text = VB6.Format(mNetAmt, "0.00")
        lblDiffAmt.Text = VB6.Format(Val(lblAmount.Text) - Val(CStr(mTotServiceTaxAmt)), "0.00")
        Exit Sub
ErrSprdTotal:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        'MainClass.SaveStatus(frmAtrn, lblADDMode.Text, lblModifyMode.Text)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        On Error GoTo ERR1
        Select Case eventArgs.Col
            Case 0
                If eventArgs.Row > 0 Then
                    MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColBillNo)
                    'MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                    FormatSprdMain(-1, True)
                    CalcTots()
                End If
            Case ColServiceProv
                If eventArgs.Row = 0 Then

                    If eventArgs.Row = 0 Then SearchProvidedMaster(eventArgs.Col, (SprdMain.ActiveRow))
                End If
        End Select


        Exit Sub
ERR1:
        ''Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchBill()

        'Dim I As Integer
        Dim SqlStr As String = ""
        Dim mBillAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mPayAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String

        mBillAmtStr = "SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
        mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        mPayAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        ''mBalAmtStr = "ABS(" & mBillAmtStr & ")-ABS(" & mPayAmtStr & ")"
        mBalAmtStr = "" & mBillAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " +" & mCNAmtStr & " + " & mPayAmtStr & ""


        mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"

        SqlStr = " Select BillNo, BillDate, " & vbCrLf & " TO_CHAR(ABS(" & mBillAmtStr & ")) AS BillAMT, " & vbCrLf & " CASE WHEN " & mBillAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS DC , " & vbCrLf & " TO_CHAR(ABS(" & mADVAmtStr & ")) AS ADV, " & vbCrLf & " TO_CHAR(ABS(" & mDNAmtStr & ")) AS DNOTE, " & vbCrLf & " TO_CHAR(ABS(" & mCNAmtStr & ")) AS CNOTE, " & vbCrLf & " TO_CHAR(ABS(" & mTDSAmtStr & ")) AS TDS, " & vbCrLf & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS BALANCE, " & vbCrLf & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS DC, MAX(DUEDATE) AS DUEDATE  " & vbCrLf & " FROM FIN_POSTED_TRN  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=2004" & vbCrLf & " AND AccountCode = '" & lblAccountCode.Text & "'"


        SqlStr = SqlStr & vbCrLf & " GROUP BY BillNo, BillDate" & vbCrLf & " HAVING " & mBalAmtStr & " <>0 " & vbCrLf & " ORDER BY BillDate, BillNo "

        MainClass.SearchGridMasterBySQL("", SqlStr)

        If AcName <> "" Then
            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = ColBillNo
            SprdMain.Text = AcName
            SprdMain.Col = ColBillDate
            SprdMain.Text = AcName1

            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColBillNo)
        End If
        Exit Sub

        '        lblBillNo.text = ""
        '        frmViewOuts.txtName.Text = lblAccountName.text
        '        frmViewOuts.lblFromMenu.text = "No"
        '        frmViewOuts.txtDateTo = RunDate      ''lblVDate.text
        '
        '        frmViewOuts.CboCostC.Text = UCase(IIf(lblCostCName.text = "", "ALL", lblCostCName.text))
        '
        '        If RsCompany.Fields("Type").Value = "R" Then
        '            frmViewOuts.cboConsolidated.ListIndex = 1
        '        ElseIf RsCompany.Fields("Type").Value = "B" Then
        '            frmViewOuts.cboConsolidated.ListIndex = 2
        '        ElseIf RsCompany.Fields("Type").Value = "D" Then
        '            frmViewOuts.cboConsolidated.ListIndex = 3
        '        End If
        '        frmViewOuts.cmdShow = True
        '        frmViewOuts.Show 1
        '        frmViewOuts.lblBillNo.text = IIf(frmViewOuts.lblBillNo.text = "lblBillNo", "", frmViewOuts.lblBillNo.text)
        '
        '        If frmViewOuts.lblBillNo.text <> "" Then
        '            SprdMain.Col = ColBillNo
        '            SprdMain.Row = SprdMain.ActiveRow
        '            SprdMain.Text = frmViewOuts.lblBillNo.text
        '
        '            SprdMain.Col = ColBillDate
        '            SprdMain.Text = frmViewOuts.lblBillDate.text
        '
        '            MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColAmount
        '            Call SprdMain_LeaveCell(ColBillNo, SprdMain.ActiveRow, ColAmount, SprdMain.ActiveRow, False)
        '        End If
    End Sub
    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent

        'Dim mPayType As String
        Dim mActiveCol As Integer

        mActiveCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Or eventArgs.KeyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColServiceTax Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColServiceTax
                If Val(SprdMain.Text) <> 0 Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColServiceTax, ConRowHeight)
                        FormatSprdMain((SprdMain.MaxRows), False)
                    End If
                End If

            End If
        ElseIf eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Then
            If SprdMain.ActiveCol = ColBillNo Then SearchBill()
        End If
        eventArgs.KeyCode = 9999
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ERR1
        Dim mServiceTaxOn As Double
        Dim mServiceTax As Double
        Dim mCessAmount As Double
        Dim mSHECESSAmount As Double
        Dim mBillNo As String = ""
        Dim mBillDate As String = ""
        Dim mBillAmount As Double
        Dim mReverseChargePer As Double
        Dim mReverseChargeApp As String = ""
        Dim mServiceTaxRecOn As Double
        Dim mSBCessAmount As Double
        Dim mKKCessAmount As Double
        If eventArgs.NewRow = -1 Then Exit Sub

        With SprdMain
            Select Case eventArgs.Col
                Case ColBillNo
                    .Row = eventArgs.Row
                    .Col = ColBillNo
                    mBillNo = Trim(.Text)

                    If mBillNo = "" Then Exit Sub

                    If lblBookType.Text = ConBankPayment Then

                    Else
                        If ValidatePartyBillNo(mBillNo, mBillDate, mBillAmount) = True Then
                            .Col = ColBillDate
                            .Text = VB6.Format(mBillDate, "DD/MM/YYYY")

                            .Col = ColBillAmount
                            .Text = VB6.Format(mBillAmount, "0.00")
                        Else
                            '                    MsgInformation "Invaild Bill No for Such Supplier"
                            MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColBillNo, "Invalid Bill No for Such Supplier.")
                        End If
                    End If
                    If DuplicateBillNo(eventArgs.Row) = True Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColBillNo, "Duplicate Bill No. : " & mBillNo)
                    End If

                    If .MaxRows = eventArgs.Row Then
                        MainClass.AddBlankSprdRow(SprdMain, ColBillNo, ConRowHeight)
                        FormatSprdMain(eventArgs.Row, False)
                    End If
                Case ColServiceProv
                    .Row = eventArgs.Row

                    .Col = ColServiceProv
                    mReverseChargePer = 0
                    If Trim(.Text) <> "" Then
                        If MainClass.ValidateWithMasterTable(.Text, "NAME", "REVERSE_CHARGE_APP", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                            MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColServiceProv, "Invalid Service Provided Name.")
                        Else
                            mReverseChargeApp = MasterNo
                            If Trim(mReverseChargeApp) = "Y" Then
                                If MainClass.ValidateWithMasterTable(.Text, "NAME", "REVERSE_CHARGE_PER", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                    mReverseChargePer = Val(MasterNo)
                                End If
                            Else
                                mReverseChargePer = 0
                            End If
                        End If
                    End If

                    .Row = eventArgs.Row
                    If CBool(lblADDMode.Text) = True Then
                        .Col = ColServicePerProv
                        .Text = CStr(100 - mReverseChargePer)

                        .Col = ColServicePerRec
                        .Text = CStr(mReverseChargePer)
                    End If

                    .Col = ColBillNo
                    mBillNo = Trim(.Text)

                    If DuplicateBillNo(eventArgs.Row) = True Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColServiceProv, "Duplicate Service Provided for Bill No. : " & mBillNo)
                    End If

                Case ColServiceTaxOn
                    .Row = eventArgs.Row
                    .Col = ColBillNo

                    .Col = ColServicePerProv
                    mReverseChargePer = Val(.Text)

                    .Col = ColServiceTaxOn
                    mServiceTaxOn = Val(.Text) * mReverseChargePer * 0.01
                    mServiceTaxRecOn = Val(.Text) * (100 - mReverseChargePer) * 0.01

                    .Col = ColServiceTax
                    If Val(txtServPer.Text) > 0 Then
                        mServiceTax = mServiceTaxOn * Val(txtServPer.Text) * 0.01
                    End If
                    .Text = VB6.Format(mServiceTax, "0.00")

                    .Col = ColCessAmount
                    If Val(txtCESSPer.Text) > 0 Then
                        mCessAmount = mServiceTax * Val(txtCESSPer.Text) * 0.01
                    End If
                    .Text = VB6.Format(mCessAmount, "0.00")

                    .Col = ColSHECessAmount
                    If Val(txtSHECessPer.Text) > 0 Then
                        mSHECESSAmount = mServiceTax * Val(txtSHECessPer.Text) * 0.01
                    End If
                    .Text = VB6.Format(mSHECESSAmount, "0.00")

                    .Col = ColSBCessAmount
                    If Val(txtSBCessPer.Text) > 0 Then
                        mSBCessAmount = mServiceTaxOn * Val(txtSBCessPer.Text) * 0.01
                    End If
                    .Text = VB6.Format(mSBCessAmount, "0.00")

                    .Col = ColKKCessAmount
                    If Val(txtKKCessPer.Text) > 0 Then
                        mKKCessAmount = mServiceTaxOn * Val(txtKKCessPer.Text) * 0.01
                    End If
                    .Text = VB6.Format(mKKCessAmount, "0.00")

                    .Col = ColServiceTaxAmount_Rec
                    If Val(txtServPer.Text) > 0 Then
                        mServiceTax = mServiceTaxRecOn * Val(txtServPer.Text) * 0.01
                    End If
                    .Text = VB6.Format(mServiceTax, "0.00")

                    .Col = ColCessAmount_Rec
                    If Val(txtCESSPer.Text) > 0 Then
                        mCessAmount = mServiceTax * Val(txtCESSPer.Text) * 0.01
                    End If
                    .Text = VB6.Format(mCessAmount, "0.00")

                    .Col = ColSHECessAmount_Rec
                    If Val(txtSHECessPer.Text) > 0 Then
                        mSHECESSAmount = mServiceTax * Val(txtSHECessPer.Text) * 0.01
                    End If
                    .Text = VB6.Format(mSHECESSAmount, "0.00")

                    .Col = ColSBCessAmount_Rec
                    If Val(txtSBCessPer.Text) > 0 Then
                        mSBCessAmount = mServiceTaxRecOn * Val(txtSBCessPer.Text) * 0.01
                    End If
                    .Text = VB6.Format(mSBCessAmount, "0.00")

                    .Col = ColKKCessAmount_Rec
                    If Val(txtKKCessPer.Text) > 0 Then
                        mKKCessAmount = mServiceTaxRecOn * Val(txtKKCessPer.Text) * 0.01
                    End If
                    .Text = VB6.Format(mKKCessAmount, "0.00")
            End Select
        End With
        CalcTots()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub

    Private Function ValidatePartyBillNo(ByRef mBillNo As String, ByRef mBillDate As String, ByRef mBillAmount As Double) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        ValidatePartyBillNo = False
        mBillDate = ""
        mBillAmount = 0

        SqlStr = "Select BILLNO,BILLDATE,AMOUNT From FIN_TEMPBILL_TRN " & vbCrLf & " Where UserID='" & PubUserID & "'" & vbCrLf & " AND AccountCode = '" & MainClass.AllowSingleQuote(lblAccountCode.Text) & "'" & vbCrLf & " AND BookType='" & Trim(lblBookType.Text) & "' " & vbCrLf & " AND BILLNO='" & mBillNo & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mBillDate = IIf(IsDbNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value)
            mBillAmount = IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
            ValidatePartyBillNo = True
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Function

    Private Function CheckAmount() As Boolean
        'Dim mDC As String
        'Dim mBalance As Double
        'Dim mBalanceDC As String
        'Dim mOldAmount As Double
        'Dim mOldDC As String
        'Dim mNetBalance As Double
        'Dim mCurrAmount As Double
        '
        '    With SprdMain
        '        .Col = ColOldDC
        '        mOldDC = Left(.Text, 1)
        '
        '        .Col = ColOldAmount
        '        mOldAmount = Val(.Text) * IIf(mOldDC = "D", -1, 1)
        '
        '
        '        .Col = ColBalanceDC
        '        mBalanceDC = Left(.Text, 1)
        '
        '        .Col = ColBalance
        '        mBalance = Val(.Text) * IIf(mBalanceDC = "D", 1, -1)
        '
        '        mNetBalance = mBalance + mOldAmount
        '
        '        .Col = ColDC
        '        mDC = Left(.Text, 1)
        '
        '        .Col = ColAmount
        '        mCurrAmount = Val(.Text) * IIf(mDC = "D", 1, -1)
        '
        ''        If mBalanceDC = mDC And mCurrAmount <> 0 Then
        ''            ErrorMsg "Dr. / Cr. Mismatch.", "", vbCritical
        ''            CheckAmount = False
        ''        Else
        '
        '        If Abs(mCurrAmount) > Abs(mNetBalance) Then
        '            ErrorMsg "Amount Exceeds", "", vbCritical
        '            CheckAmount = False
        '        Else
        '            CheckAmount = True
        '        End If
        '
        '
        '   End With
    End Function
    Private Function CheckBillNo() As Boolean
        On Error GoTo ERR1
        'Dim RS As ADODB.Recordset = Nothing
        'Dim SqlStr As String=""
        '
        'Dim mPayType As String
        'Dim mBillNo As String
        'Dim mBillDate As String
        'Dim mBillAmount As Double
        'Dim mDC As String
        'Dim mPaymentAmt As Double
        '
        'Dim mBalance As Double
        'Dim mRow As Long
        'Dim cntRow As Long
        'Dim mOldAmount As Double
        '
        '    With SprdMain
        '        mRow = .ActiveRow
        '        .Row = mRow
        '        .Col = ColBillNo
        '        mBillNo = Trim(.Text)
        '
        '        If mBillNo = "" Then
        '             CheckBillNo = True
        '             Exit Function
        '        End If
        '
        '        SprdMain.Col = ColPayType
        '        mPayType = Left(.Text, 1)
        '
        '        .Col = ColBillDate
        '        mBillDate = .Text
        '
        '        Call GetBalanceAmount(mRow, lblAccountCode.text, mBillNo, mBillDate, mPayType)
        '        Call PickUpBillPayment(mPayType, mBillNo, mOldAmount, "D")
        '
        '    End With
        '    CheckBillNo = True
        Exit Function
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function DuplicateBillNo(ByRef xRow As Integer) As Boolean
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckBillNo As String
        Dim mBillNo As String

        DuplicateBillNo = False
        With SprdMain
            .Row = xRow
            .Col = ColBillNo
            mCheckBillNo = Trim(UCase(.Text))

            .Col = ColServiceProv
            mCheckBillNo = mCheckBillNo & "-" & Trim(UCase(.Text))

            If mCheckBillNo = "" Then Exit Function

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColBillNo
                mBillNo = Trim(UCase(.Text))

                .Col = ColServiceProv
                mBillNo = mBillNo & "-" & Trim(UCase(.Text))

                If (mBillNo = mCheckBillNo) Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateBillNo = True
                    Exit Function
                End If
            Next
        End With
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub txtSHECessPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSHECessPer.TextChanged

        'MainClass.SaveStatus(frmAtrn, lblADDMode.Text, lblModifyMode.Text)
    End Sub

    Private Sub txtSHECessPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSHECessPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSHECessPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSHECessPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Val(txtSHECessPer.Text) = 0 Then GoTo EventExitSub
        Call CalcTots()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSTPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTPer.TextChanged

        'MainClass.SaveStatus(frmAtrn, lblADDMode.Text, lblModifyMode.Text)
    End Sub

    Private Sub txtSTPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSTPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
