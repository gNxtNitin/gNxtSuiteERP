Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTDSDetail
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection				
    Dim RsTDSDetail As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Private Const ConBookType As String = "D"
    Private Const ConBookSubType As String = "D"

    Dim xMkey As String
    Dim FormActive As Boolean
    Dim SqlStr As String
    Private Sub ViewGrid()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh				
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsTDSDetail, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub Clear1()
        txtVNo.Text = ""
        TxtAccount.Text = ""
        chkExepted.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkLowerDed.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtAmountPaid.Text = ""
        txtPartyName.Text = ""
        txtVDate.Text = ""
        txtSection.Text = ""
        txtTDSAmount.Text = ""
        txtTdsRate.Text = ""
        txtExepted.Text = ""
        lblMKey.Text = ""
        lblBookType.Text = ConBookType
        lblBookSubType.Text = ConBookSubType
        txtPANNo.Text = ""
        cboCType.SelectedIndex = 0
        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        cboCType.Enabled = False
        txtPANNo.ReadOnly = True
        chkCancelled.Enabled = True
        chkAdditional.CheckState = System.Windows.Forms.CheckState.Unchecked
        MainClass.ButtonStatus(Me, XRIGHT, RsTDSDetail, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cboCType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCType.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboCType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCType.SelectedIndexChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub chkAdditional_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAdditional.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkExepted_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkExepted.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)

        If chkExepted.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtPartyName_Validating(txtPartyName, New System.ComponentModel.CancelEventArgs(False))
            txtTDSAmount.Text = "0.00"
            txtTdsRate.Text = "0.000"
        Else
            txtExepted.Text = ""
        End If
    End Sub

    Private Sub chkLowerDed_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkLowerDed.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Public Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click
        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            If CheckChallanMade() = True Then
                Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsTDSDetail, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdPartySearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPartySearch.Click
        SearchPartyName()
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearch.Click
        SearchAccounts()
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo err_Renamed
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Show1()
            MainClass.ButtonStatus(Me, XRIGHT, RsTDSDetail, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        End If
        Exit Sub
err_Renamed:
        MsgInformation(Err.Description)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr

        SqlStr = ""
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "TDS_TRN", (lblMKey.Text), RsTDSDetail, "", "D") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "TDS_TRN", "MKEY", (lblMKey.Text)) = False Then GoTo DeleteErr

        SqlStr = "DELETE FROM TDS_TRN WHERE MKEY='" & lblMKey.Text & "'"
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsTDSDetail.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsTDSDetail.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This Company.")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If lblMKey.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsTDSDetail.EOF Then
            If CheckChallanMade() = True Then
                MsgInformation("TDS Challan Made Agt. this Entry, So Cann't Modify.")
                Exit Sub
            End If
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.				
                If Delete1() = False Then GoTo DelErrPart
                If RsTDSDetail.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Public Sub frmTDSDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.UOpenRecordSet("Select * From TDS_TRN where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDSDetail, ADODB.LockTypeEnum.adLockReadOnly)
        Call AssignGrid(False)
        Call SetTextLengths()
        Call Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmTDSDetail_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub frmTDSDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(5085)				
        'Me.Width = VB6.TwipsToPixelsX(8355)				
        ''Set PvtDBCn = New ADODB.Connection				
        ''PvtDBCn.Open StrConn				

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        FillCboCType()

        ADDMode = False
        MODIFYMode = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmTDSDetail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsTDSDetail = Nothing
        Me.Dispose()

        Me.Close()
        '    PubDBCn.Cancel				
        '    PvtDBCn.Close				
        '    Set PvtDBCn = Nothing				
    End Sub
    Private Sub Show1()
        On Error GoTo ShowErrPart
        Dim mCTYPE As String

        If RsTDSDetail.EOF = False Then
            With RsTDSDetail

                If MainClass.ValidateWithMasterTable(.Fields("ACCOUNTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtAccount.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
                Else
                    TxtAccount.Text = ""
                End If


                chkExepted.CheckState = IIf(.Fields("ISEXEPTED").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                chkLowerDed.CheckState = IIf(.Fields("ISLOWERDED").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                chkCancelled.CheckState = IIf(.Fields("Cancelled").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkCancelled.Enabled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
                txtVNo.Text = IIf(IsDBNull(.Fields("VNO").Value), "", .Fields("VNO").Value)

                txtAmountPaid.Text = VB6.Format(IIf(IsDBNull(.Fields("AmountPaid").Value), "", .Fields("AmountPaid").Value), "0.00")

                If MainClass.ValidateWithMasterTable(.Fields("PARTYCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtPartyName.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
                Else
                    txtPartyName.Text = ""
                End If

                '            txtPartyName.Text = IIf(IsNull(!PARTYNAME), "", IIf(!PARTYNAME = "-1", "", !PARTYNAME))				
                txtVDate.Text = IIf(IsDBNull(.Fields("VDATE").Value), "", .Fields("VDATE").Value)

                If MainClass.ValidateWithMasterTable(.Fields("SECTIONCODE").Value, "Code", "Name", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSection.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
                Else
                    txtSection.Text = ""
                End If

                txtTDSAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("TDSAMOUNT").Value), "", .Fields("TDSAMOUNT").Value), "0.00")
                txtTdsRate.Text = VB6.Format(IIf(IsDBNull(.Fields("TDSRATE").Value), "", .Fields("TDSRATE").Value), "0.000")
                txtExepted.Text = IIf(IsDBNull(.Fields("EXEPTIONCNO").Value), "", .Fields("EXEPTIONCNO").Value)

                If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "CTYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCTYPE = IIf(IsDBNull(MasterNo), "C", MasterNo)
                Else
                    mCTYPE = "C"
                End If

                cboCType.SelectedIndex = IIf(mCTYPE = "C", 0, 1)
                txtPANNo.Text = IIf(IsDBNull(.Fields("PANNO").Value), "", .Fields("PANNO").Value)


                lblMKey.Text = .Fields("mKey").Value
                lblBookType.Text = IIf(IsDBNull(.Fields("BookType").Value), "", .Fields("BookType").Value)
                lblBookSubType.Text = IIf(IsDBNull(.Fields("BOOKSUBTYPE").Value), "", .Fields("BOOKSUBTYPE").Value)

                xMkey = .Fields("mKey").Value
            End With
        End If
        ADDMode = False
        If lblBookType.Text = ConBookType And lblBookSubType.Text = ConBookSubType Then
            MODIFYMode = False
        Else
            MODIFYMode = True
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsTDSDetail, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            SqlStr = "SELECT * FROM TDS_TRN WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "' AND BOOKTYPE='" & lblBookType.Text & "' AND BOOKSUBTYPE='" & lblBookSubType.Text & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDSDetail, ADODB.LockTypeEnum.adLockReadOnly)
            Show1()
            If CmdAdd.Enabled = True And CmdAdd.Visible = True Then
                CmdAdd.Focus()
            Else
                If lblBookType.Text = ConBookType And lblBookSubType.Text = ConBookSubType Then
                    ''				
                Else
                    Me.Close()
                End If
            End If
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
        ''Resume				
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim mMkey As Integer
        Dim mRowNo As Integer
        Dim CurMKey As String
        Dim mAccountCode As String
        Dim mExepted As String
        Dim mSectionCode As Integer
        Dim mCTYPE As String
        Dim mCancelled As String
        Dim mTDSAmount As Double
        Dim mPartyCode As String
        Dim mLowerDed As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = ""
        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        Else
            mAccountCode = -1
        End If

        If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyCode = MasterNo
        Else
            mPartyCode = "-1"
        End If

        If MainClass.ValidateWithMasterTable(txtSection.Text, "Name", "Code", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSectionCode = MasterNo
        Else
            mSectionCode = -1
        End If

        mCTYPE = VB.Left(cboCType.Text, 1)
        mExepted = IIf(chkExepted.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mLowerDed = IIf(chkLowerDed.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mTDSAmount = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, 0, Val(txtTDSAmount.Text))
        If chkExepted.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTDSAmount.Text = "0.00"
            txtTdsRate.Text = "0.000"
        End If

        If MODIFYMode = True Then
            If InsertIntoDelAudit(PubDBCn, "TDS_TRN", (lblMKey.Text), RsTDSDetail, "MKEY", "M") = False Then GoTo UpdateError
        End If


        If ADDMode = True Then
            mRowNo = MainClass.AutoGenRowNo("TDS_TRN", "RowNo", PubDBCn)
            CurMKey = ConBookType & ConBookSubType & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value) & VB6.Format(RsCompany.Fields("FYEAR").Value) & VB6.Format(mRowNo)

            SqlStr = "INSERT INTO TDS_TRN ( MKEY, COMPANY_CODE,  " & vbCrLf & " FYEAR, ROWNO, SUBROWNO, VNO, VDATE, " & vbCrLf & " BOOKCODE, BOOKTYPE, BOOKSUBTYPE, ACCOUNTCODE, " & vbCrLf & " PARTYCODE, PARTYNAME, AMOUNTPAID, SECTIONCODE, " & vbCrLf & " TDSRATE, ISEXEPTED, EXEPTIONCNO, " & vbCrLf & " TDSAMOUNT, CTYPE, PANNo,CANCELLED, ADDUSER, ADDDATE, MODUSER, MODDATE,UPDATE_FROM,ISLOWERDED ) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(CurMKey)) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & mRowNo & ",1,'" & mRowNo & "', TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(lblBookCode.Text) & ",'" & lblBookType.Text & "', '" & lblBookSubType.Text & "', " & vbCrLf & " '" & mAccountCode & "','" & MainClass.AllowSingleQuote(mPartyCode) & "', '" & MainClass.AllowSingleQuote(txtPartyName.Text) & "', " & vbCrLf & " " & Val(txtAmountPaid.Text) & "," & mSectionCode & "," & Val(txtTdsRate.Text) & ", " & vbCrLf & " '" & mExepted & "','" & MainClass.AllowSingleQuote(txtExepted.Text) & "', " & vbCrLf & " " & Val(CStr(mTDSAmount)) & ", " & vbCrLf & " '" & mCTYPE & "', '" & MainClass.AllowSingleQuote(txtPANNo.Text) & "', " & vbCrLf & " '" & mCancelled & "','" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','H','" & mLowerDed & "')"
        Else
            SqlStr = " UPDATE TDS_TRN SET " & vbCrLf & " VDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " ACCOUNTCODE='" & mAccountCode & "', " & vbCrLf & " PARTYCODE='" & MainClass.AllowSingleQuote(mPartyCode) & "', PARTYNAME='" & MainClass.AllowSingleQuote(txtPartyName.Text) & "', " & vbCrLf & " AMOUNTPAID=" & Val(txtAmountPaid.Text) & ", " & vbCrLf & " SECTIONCODE=" & mSectionCode & "," & vbCrLf & " TDSRATE=" & Val(txtTdsRate.Text) & ", " & vbCrLf & " ISEXEPTED='" & mExepted & "', " & vbCrLf & " ISLOWERDED='" & mLowerDed & "', " & vbCrLf & " EXEPTIONCNO='" & MainClass.AllowSingleQuote(txtExepted.Text) & "', " & vbCrLf & " TDSAMOUNT=" & Val(CStr(mTDSAmount)) & ", " & vbCrLf & " CTYPE='" & mCTYPE & "', " & vbCrLf & " PANNO='" & MainClass.AllowSingleQuote(txtPANNo.Text) & "', " & vbCrLf & " CANCELLED='" & mCancelled & "', UPDATE_FROM='H'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE MKey= '" & xMkey & "'"

            CurMKey = xMkey
        End If

        PubDBCn.Execute(SqlStr)

        SqlStr = " UPDATE FIN_VOUCHER_HDR SET " & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE MKey= '" & xMkey & "'" & vbCrLf & " AND BookType<>'D' " & vbCrLf & " AND BookSubType<>'D'"

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        lblMKey.Text = CurMKey
        Update1 = True
        RsTDSDetail.Requery()
        Exit Function
UpdateError:
        Update1 = False

        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        RsTDSDetail.Requery()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        PubDBCn.RollbackTrans()
        '    Resume				
    End Function
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtVNo.MaxLength = RsTDSDetail.Fields("VNO").DefinedSize
        txtAmountPaid.MaxLength = RsTDSDetail.Fields("AMOUNTPAID").DefinedSize
        txtPartyName.MaxLength = RsTDSDetail.Fields("PARTYNAME").DefinedSize
        txtVDate.MaxLength = RsTDSDetail.Fields("VDate").DefinedSize
        txtSection.MaxLength = MainClass.SetMaxLength("Name", "TDS_Section_MST", PubDBCn)
        txtTDSAmount.MaxLength = RsTDSDetail.Fields("TDSAMOUNT").Precision
        txtTdsRate.MaxLength = RsTDSDetail.Fields("TDSRATE").Precision
        txtExepted.MaxLength = RsTDSDetail.Fields("EXEPTIONCNO").DefinedSize
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Or Modify To Add a New Voucher.")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsTDSDetail.EOF = True Then
            FieldsVarification = False
            Exit Function
        End If

        If Trim(TxtAccount.Text) = "" Then
            MsgBox("TDS Account Name is empty.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Invalid TDS Account Name", vbInformation)
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtSection.Text) = "" Then
            MsgBox("Section Name is empty.", MsgBoxStyle.Information)
            txtSection.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtSection.Text, "Name", "Code", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Invalid Section Name", vbInformation)
                txtSection.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtVDate.Text) = "" Then
            MsgBox("Payment Date is empty.", MsgBoxStyle.Information)
            txtVDate.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If MainClass.ChkIsdateF(txtVDate.Text) = False Then Exit Function
        If FYChk(CStr(CDate(txtVDate.Text))) = False Then txtVDate.Focus()

        If Val(txtAmountPaid.Text) = 0 And chkAdditional.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MsgBox("Amount Paid/Credited Cann't Be Zero.", MsgBoxStyle.Information)
            txtAmountPaid.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If chkExepted.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Val(txtTDSAmount.Text) = 0 And chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                MsgBox("TDS Amount Cann't Be Zero.", MsgBoxStyle.Information)
                txtTDSAmount.Focus()
                FieldsVarification = False
                Exit Function
            End If
        Else
            If Trim(txtExepted.Text) = "" Then
                MsgBox("Exeption Certificate Cann't Be Blank.", MsgBoxStyle.Information)
                txtExepted.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Val(txtTdsRate.Text) > 100 Then
            MsgBox("Deducted Rate Cann't be Greater Than 100.", MsgBoxStyle.Information)
            txtTdsRate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            If MsgQuestion("Want to save Cancelled voucher?") = CStr(MsgBoxResult.No) Then
                If chkCancelled.Enabled = True Then chkCancelled.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        Exit Function
err_Renamed:
        FieldsVarification = False
        MsgBox(Err.Description)
    End Function
    Private Sub frmTDSDetail_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        On Error GoTo AssignErr
        SqlStr = ""
        SqlStr = "SELECT MKEY, TO_CHAR(VDATE,'DD/MM/YYYY') AS V_DATE, BOOKTYPE, BOOKSUBTYPE, " & vbCrLf & " ACM.SUPP_CUST_NAME AS TDS_ACCOUNT, PARTYNAME,  " & vbCrLf & " TDSSECTION.NAME AS SECTION, " & vbCrLf & " TO_CHAR(AMOUNTPAID,'99,99,99,999.99') AS AMOUNT_PAID, TO_CHAR(TDSRATE,'99,99,99,999.99') AS RATE, TO_CHAR(TDSAMOUNT,'99,99,99,999.99') AS TDS_AMOUNT, " & vbCrLf & " ISEXEPTED, EXEPTIONCNO ,DECODE(TDSTRN.CANCELLED,'Y','YES','NO') AS CANCELLED  " & vbCrLf & " FROM TDS_TRN TDSTRN, FIN_SUPP_CUST_MST ACM, TDS_Section_MST TDSSECTION " & vbCrLf & " WHERE " & vbCrLf & " TDSTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TDSTRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf & " AND TDSTRN.COMPANY_CODE=TDSSECTION.COMPANY_CODE " & vbCrLf & " AND TDSTRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE " & vbCrLf & " AND TDSTRN.SECTIONCODE=TDSSECTION.CODE " & vbCrLf & " AND TDSTRN.BOOKCODE=-2 ORDER BY VDATE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

        Exit Sub
AssignErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdView()
        With SprdView
            .Row = -1
            .set_RowHeight(0, 15)

            .Col = 0
            .set_ColWidth(.Col, 5)

            .Col = 1
            .set_ColWidth(.Col, 0)

            .Col = 2
            .set_ColWidth(.Col, 9)

            .Col = 3
            .set_ColWidth(.Col, 0)

            .Col = 4
            .set_ColWidth(.Col, 0)

            .Col = 5
            .set_ColWidth(.Col, 25)

            .Col = 6
            .set_ColWidth(.Col, 25)

            .Col = 7
            .set_ColWidth(.Col, 8)

            .Col = 8
            .set_ColWidth(.Col, 12)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            .Col = 9
            .set_ColWidth(.Col, 10)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            .Col = 10
            .set_ColWidth(.Col, 12)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            .Col = 11
            .set_ColWidth(.Col, 12)

            .Col = 12
            .set_ColWidth(.Col, 12)

            .Col = 13
            .set_ColWidth(.Col, 12)

            ''.ColsFrozen = 5				
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Row = eventArgs.row

        SprdView.Col = 1
        lblMKey.Text = SprdView.Text

        SprdView.Col = 5
        TxtAccount.Text = SprdView.Text


        txtAccount_Validating(TxtAccount, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())

    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub SearchAccounts()
        On Error GoTo ERR1
        MainClass.SearchMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'")
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub TxtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub


    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(TxtAccount.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'") = False Then
            MsgInformation("Invalid TDS Head.")
            Cancel = True
        End If


        SqlStr = ""
        If Trim(lblMKey.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsTDSDetail.EOF = False Then xMkey = RsTDSDetail.Fields("mKey").Value

        SqlStr = "SELECT * FROM TDS_TRN WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "' AND BOOKTYPE='" & lblBookType.Text & "' AND BOOKSUBTYPE='" & lblBookSubType.Text & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDSDetail, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTDSDetail.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Does Not Exist." & vbCrLf & "Click Add For New.")
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "Select * from TDS_TRN Where Mkey='" & lblMKey.Text & "' AND BOOKTYPE='" & lblBookType.Text & "' AND BOOKSUBTYPE='" & lblBookSubType.Text & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDSDetail, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtAmountPaid_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmountPaid.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtAmountPaid_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmountPaid.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAmountPaid_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAmountPaid.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtTDSAmount.Enabled = False Then GoTo EventExitSub
        If chkAdditional.CheckState = System.Windows.Forms.CheckState.Checked Then
            GoTo EventExitSub
        End If

        If chkExepted.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTDSAmount.Text = "0.00"
            GoTo EventExitSub
        End If

        txtTDSAmount.Text = CStr(Val(txtAmountPaid.Text) * Val(txtTdsRate.Text) / 100)
        txtTDSAmount.Text = VB6.Format(System.Math.Round(CDbl(txtTDSAmount.Text), 0), "0.00")

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtExepted_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExepted.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtExepted_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExepted.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtExepted.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPANNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPANNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPANNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPANNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPANNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPartyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPartyName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.DoubleClick
        SearchPartyName()
    End Sub

    Private Sub txtPartyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartyName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPartyName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartyName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchPartyName()
    End Sub

    Private Sub txtPartyName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartyName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim mPartyCode As String
        Dim SqlStr As String
        Dim RsSec As ADODB.Recordset

        If txtPartyName.Text = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyCode = MasterNo
            SqlStr = "Select TDSSECTION.NAME AS SECTIONNAME, EXPTIONCNO, " & vbCrLf & " TDS_PER AS TDSRATE,TDSMASTER.CTYPE,PAN_NO " & vbCrLf & " FROM FIN_SUPP_CUST_MST TDSMASTER,TDS_Section_MST TDSSECTION " & vbCrLf & " WHERE TDSMASTER.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TDSMASTER.COMPANY_CODE=TDSSECTION.COMPANY_CODE " & vbCrLf & " AND TDSMASTER.SECTIONCODE=TDSSECTION.Code " & vbCrLf & " AND TDSMASTER.SUPP_CUST_CODE='" & mPartyCode & "' "
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSec, ADODB.LockTypeEnum.adLockReadOnly)

            If RsSec.EOF = False Then
                txtSection.Text = IIf(Trim(txtSection.Text) = "", IIf(IsDBNull(RsSec.Fields("SECTIONNAME").Value), "", RsSec.Fields("SECTIONNAME").Value), txtSection.Text)
                txtTdsRate.Text = IIf(Val(txtTdsRate.Text) = 0, IIf(IsDBNull(RsSec.Fields("TDSRATE").Value), 0, RsSec.Fields("TDSRATE").Value), txtTdsRate.Text)
                txtExepted.Text = IIf(Trim(txtExepted.Text) = "", IIf(IsDBNull(RsSec.Fields("EXPTIONCNO").Value), "", RsSec.Fields("EXPTIONCNO").Value), txtExepted.Text)
                cboCType.SelectedIndex = IIf(RsSec.Fields("CType").Value = "C", 0, 1)
                txtPANNo.Text = IIf(IsDBNull(RsSec.Fields("PAN_NO").Value), "", RsSec.Fields("PAN_NO").Value)
            End If

            cboCType.Enabled = False
            txtPANNo.ReadOnly = True

            RsSec.Close()
            RsSec = Nothing
        Else
            MsgInformation("Invaild Account Name")
            Cancel = True
        End If

        GoTo EventExitSub
ErrPart:
        MsgBox(Err.Description)
        'Resume				
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSection_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSection.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtSection_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSection.DoubleClick
        SearchSection()
    End Sub

    Private Sub txtSection_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSection.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSection.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSection_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSection.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSection()
    End Sub

    Private Sub txtSection_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSection.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim mSectionCode As Integer
        Dim mTdsRate As Double

        If Trim(txtSection.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtSection.Text, "Name", "Code", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Secion Name Does Not Exist In Master", vbInformation)
            Cancel = True
            Exit Sub
        Else
            mSectionCode = MasterNo
        End If


        mTdsRate = CalcTDSRate(mSectionCode, VB.Left(cboCType.Text, 1), PubDBCn, Trim(txtVDate.Text))

        txtTdsRate.Text = VB6.Format(IIf(Val(txtTdsRate.Text) = 0, mTdsRate, txtTdsRate.Text), "0.000")

        GoTo EventExitSub
ERR1:
        MsgBox(Err.Description, MsgBoxStyle.Information)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTDSAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSAmount.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtTDSAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTDSAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTDSAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Val(txtTDSAmount.Text) = 0 Then
            GoTo EventExitSub
        End If
        If chkAdditional.CheckState = System.Windows.Forms.CheckState.Checked Then
            GoTo EventExitSub
        End If

        If chkExepted.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTDSAmount.Text = "0.00"
            txtTdsRate.Text = "0.000"
            GoTo EventExitSub
        End If

        If txtTDSAmount.Enabled = True Then
            If Val(txtAmountPaid.Text) = 0 And Val(txtTdsRate.Text) Then txtTDSAmount.Text = CStr(0) : GoTo EventExitSub
        End If

        If Val(txtAmountPaid.Text) = 0 Then
            txtAmountPaid.Text = CStr(Val(txtTDSAmount.Text) * 100 / IIf(Val(txtTdsRate.Text) = 0, 1, Val(txtTdsRate.Text)))
        ElseIf Val(txtTdsRate.Text) = 0 Then
            txtTdsRate.Text = CStr(Val(txtTDSAmount.Text) * 100 / Val(txtAmountPaid.Text))
        End If
        txtTDSAmount.Text = VB6.Format(System.Math.Round(CDbl(txtTDSAmount.Text), 0), "0.00")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTdsRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTdsRate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtTdsRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTdsRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTdsRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTdsRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtTDSAmount.Enabled = False Then GoTo EventExitSub
        If chkAdditional.CheckState = System.Windows.Forms.CheckState.Checked Then
            GoTo EventExitSub
        End If

        If chkExepted.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTDSAmount.Text = "0.00"
            txtTdsRate.Text = "0.000"
            GoTo EventExitSub
        End If

        txtTDSAmount.Text = CStr(Val(txtAmountPaid.Text) * Val(txtTdsRate.Text) / 100)
        txtTDSAmount.Text = VB6.Format(System.Math.Round(CDbl(txtTDSAmount.Text), 0), "0.00")

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub






    Private Sub SearchSection()
        On Error GoTo ERR1
        MainClass.SearchMaster(txtSection.Text, "TDS_Section_MST", "NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        If AcName <> "" Then
            txtSection.Text = AcName
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtVDate.Text) = "" Then GoTo EventExitSub
        If MainClass.ChkIsdateF(txtVDate.Text) = False Then
            Cancel = True
            Exit Sub
        End If

        If FYChk(CStr(CDate(txtVDate.Text))) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
        txtSection_Validating(txtSection, New System.ComponentModel.CancelEventArgs(False))
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SearchPartyName()
        On Error GoTo SearchErr
        Dim SqlStr As String
        MainClass.SearchMaster(txtPartyName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')")
        If AcName <> "" Then
            txtPartyName.Text = AcName
            txtPartyName_Validating(txtPartyName, New System.ComponentModel.CancelEventArgs(False))
            txtPartyName.Focus()
        End If
        Exit Sub

SearchErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub FillCboCType()
        cboCType.Items.Clear()
        cboCType.Items.Add("COMPANY")
        cboCType.Items.Add("NON-COMPANY")
        cboCType.SelectedIndex = 0
    End Sub

    Private Function CheckChallanMade() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim mChallanNo As String

        SqlStr = "Select CHALLANNO FROM TDS_TRN " & vbCrLf & " WHERE MKEY='" & lblMKey.Text & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            mChallanNo = IIf(IsDBNull(RS.Fields("CHALLANNO").Value), "", RS.Fields("CHALLANNO").Value)
            If mChallanNo = "" Then
                CheckChallanMade = False
            Else
                CheckChallanMade = True
                MsgInformation("TDS Challan Made Agt. this Entry, So Cann't Modify.")
            End If
        Else
            CheckChallanMade = False
        End If
        Exit Function
ErrPart:
        MsgInformation(Err.Description)
        CheckChallanMade = False
    End Function

    Private Sub txtVno_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub TxtVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtVNo.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsTDSDetail.EOF = False Then xMkey = RsTDSDetail.Fields("mKey").Value

        SqlStr = "SELECT * FROM TDS_TRN WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf _
            & " AND BOOKSUBTYPE='" & lblBookSubType.Text & "'" & vbCrLf _
            & " AND VNO='" & MainClass.AllowSingleQuote(UCase(txtVNo.Text)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDSDetail, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTDSDetail.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Does Not Exist." & vbCrLf & "Click Add For New.")
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "Select * from TDS_TRN Where Mkey='" & lblMKey.Text & "' AND BOOKTYPE='" & lblBookType.Text & "' AND BOOKSUBTYPE='" & lblBookSubType.Text & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDSDetail, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
