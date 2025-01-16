Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTCSDetail
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection				
    Dim RsTCSDetail As ADODB.Recordset

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
        MainClass.ButtonStatus(Me, XRIGHT, RsTCSDetail, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub Clear1()

        txtVNo.Text = ""
        txtPartyName.Text = ""
        txtVDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtAmountPaid.Text = "0.00"
        txtTCSRate.Text = "0.00"
        txtTCSAmount.Text = "0.00"
        txtRemarks.Text = ""
        txtVNo.Enabled = True
        chkAdditional.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCancelled.Enabled = True
        lblBookType.Text = ConBookType
        lblBookSubType.Text = ConBookSubType
        MainClass.ButtonStatus(Me, XRIGHT, RsTCSDetail, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub chkAdditional_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAdditional.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged
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
            MainClass.ButtonStatus(Me, XRIGHT, RsTCSDetail, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo err_Renamed
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtVNo.Enabled = False
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Show1()
            MainClass.ButtonStatus(Me, XRIGHT, RsTCSDetail, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        End If
        Exit Sub
err_Renamed:
        MsgInformation(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr

        SqlStr = ""
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "TCS_TRN", (lblMKey.Text), RsTCSDetail, "", "D") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "TCS_TRN", "MKEY", (lblMKey.Text)) = False Then GoTo DeleteErr

        SqlStr = "DELETE FROM TCS_TRN WHERE MKEY='" & lblMKey.Text & "'"
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsTCSDetail.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsTCSDetail.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This Company.")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If lblMKey.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsTCSDetail.EOF Then
            If CheckChallanMade() = True Then
                MsgInformation("TCS Challan Made Agt. this Entry, So Cann't Modify.")
                Exit Sub
            End If
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.				
                If Delete1() = False Then GoTo DelErrPart
                If RsTCSDetail.EOF = True Then
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
    Public Sub frmTCSDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.UOpenRecordSet("Select * From TCS_TRN where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTCSDetail, ADODB.LockTypeEnum.adLockReadOnly)
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
    Private Sub frmTCSDetail_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub frmTCSDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(5085)
        Me.Width = VB6.TwipsToPixelsX(8355)
        ''Set PvtDBCn = New ADODB.Connection				
        ''PvtDBCn.Open StrConn				

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmTCSDetail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsTCSDetail = Nothing
        frmTDSDetail = Nothing
        '    PubDBCn.Cancel				
        '    PvtDBCn.Close				
        '    Set PvtDBCn = Nothing				
    End Sub
    Private Sub Show1()
        On Error GoTo ShowErrPart
        Dim mCTYPE As String

        If RsTCSDetail.EOF = False Then
            With RsTCSDetail

                txtVNo.Text = IIf(IsDBNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)

                If MainClass.ValidateWithMasterTable(.Fields("SUPP_CUST_CODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtPartyName.Text = MasterNo
                Else
                    txtPartyName.Text = ""
                End If


                txtVDate.Text = VB6.Format(IIf(IsDBNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                txtAmountPaid.Text = VB6.Format(IIf(IsDBNull(.Fields("NETVALUE").Value), "", .Fields("NETVALUE").Value), "0.00")
                txtTCSRate.Text = VB6.Format(IIf(IsDBNull(.Fields("TCSPER").Value), "", .Fields("TCSPER").Value), "0.00")
                txtTCSAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("TCSAMOUNT").Value), "", .Fields("TCSAMOUNT").Value), "0.00")
                txtRemarks.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                chkAdditional.CheckState = IIf(.Fields("ADDITIONAL_TAX").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkCancelled.CheckState = IIf(.Fields("Cancelled").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkCancelled.Enabled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

                lblMKey.Text = .Fields("mKey").Value
                lblBookType.Text = IIf(IsDBNull(.Fields("BookType").Value), "", .Fields("BookType").Value)
                lblBookSubType.Text = IIf(IsDBNull(.Fields("BOOKSUBTYPE").Value), "", .Fields("BOOKSUBTYPE").Value)
                txtVNo.Enabled = True
                xMkey = .Fields("mKey").Value
            End With
        End If
        ADDMode = False
        If lblBookType.Text = ConBookType And lblBookSubType.Text = ConBookSubType Then
            MODIFYMode = False
        Else
            MODIFYMode = True
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsTCSDetail, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

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

            SqlStr = "SELECT * FROM TCS_TRN WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf & " AND BOOKSUBTYPE='" & lblBookSubType.Text & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTCSDetail, ADODB.LockTypeEnum.adLockReadOnly)
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
        Dim mCancelled As String
        Dim mAdditionTax As String
        Dim mTCSAMOUNT As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = ""

        If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        Else
            mAccountCode = -1
        End If

        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAdditionTax = IIf(chkAdditional.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mTCSAMOUNT = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, 0, Val(txtTCSAmount.Text))

        If Trim(txtVNo.Text) = "" Then
            txtVNo.Text = AutoGenSeqBillNo()
        End If

        If ADDMode = True Then
            mRowNo = MainClass.AutoGenRowNo("TCSTRN", "RowNo", PubDBCn)
            CurMKey = ConBookType & ConBookSubType & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value) & VB6.Format(RsCompany.Fields("FYEAR").Value) & VB6.Format(mRowNo)

            SqlStr = "INSERT INTO TCS_TRN ( " & vbCrLf & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf & " BILLNO, SUBROWNO, INVOICE_DATE, " & vbCrLf & " SUPP_CUST_CODE, BOOKCODE, BOOKTYPE, " & vbCrLf & " BOOKSUBTYPE, NETVALUE, NETTAXAMOUNT, TCSPER, " & vbCrLf & " TCSAMOUNT, REMARKS, CANCELLED, " & vbCrLf & " ADDITIONAL_TAX,  " & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE,UPDATE_FROM " & vbCrLf & " ) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(CurMKey)) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                & " " & txtVNo.Text & ", " & mRowNo & ", TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mAccountCode & "', " & Val(lblBookCode.Text) & ",'" & lblBookType.Text & "', " & vbCrLf & " '" & lblBookSubType.Text & "', " & Val(txtAmountPaid.Text) & ",  " & Val(CStr(mTCSAMOUNT)) & ", " & Val(txtTCSRate.Text) & ", " & vbCrLf & " " & Val(CStr(mTCSAMOUNT)) & ", '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', '" & mCancelled & "', " & vbCrLf & " '" & mAdditionTax & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','H')"

        Else

            SqlStr = " UPDATE TCS_TRN SET " & vbCrLf & " BILLNO='" & txtVNo.Text & "', " & vbCrLf _
                & " INVOICE_DATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " SUPP_CUST_CODE='" & mAccountCode & "', " & vbCrLf & " NETVALUE=" & Val(txtAmountPaid.Text) & ", NETTAXAMOUNT=" & Val(CStr(mTCSAMOUNT)) & ", " & vbCrLf & " TCSPER=" & Val(txtTCSRate.Text) & ", " & vbCrLf & " TCSAMOUNT=" & Val(CStr(mTCSAMOUNT)) & ", " & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf & " CANCELLED='" & mCancelled & "', " & vbCrLf & " ADDITIONAL_TAX='" & mAdditionTax & "',  UPDATE_FROM='H'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE MKey= '" & xMkey & "'"

            CurMKey = xMkey
        End If

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        lblMKey.Text = CurMKey
        Update1 = True
        RsTCSDetail.Requery()
        Exit Function
UpdateError:
        Update1 = False

        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        RsTCSDetail.Requery()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        PubDBCn.RollbackTrans()
        '    Resume				
    End Function
    Private Function AutoGenSeqBillNo() As String
        On Error GoTo AutoGenSeqBillNoErr
        Dim RsMainGen As ADODB.Recordset
        Dim mNewSeqBillNo As Integer
        Dim SqlStr As String

        SqlStr = ""


        SqlStr = "SELECT Max(BILLNO)  FROM TCS_TRN " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BookCODE='" & lblBookCode.Text & "'" & vbCrLf & " AND BookType='" & lblBookType.Text & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMainGen
            If .EOF = False Then
                If .Fields(0).Value = -1 Then
                    mNewSeqBillNo = 1
                ElseIf Not IsDBNull(.Fields(0).Value) Then
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


    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtVNo.MaxLength = RsTCSDetail.Fields("BILLNO").DefinedSize
        txtVDate.MaxLength = 10
        txtAmountPaid.MaxLength = RsTCSDetail.Fields("NETVALUE").Precision
        txtPartyName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtTCSAmount.MaxLength = RsTCSDetail.Fields("TCSAMOUNT").Precision
        txtTCSRate.MaxLength = RsTCSDetail.Fields("TCSPER").Precision
        txtRemarks.MaxLength = RsTCSDetail.Fields("REMARKS").DefinedSize

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
        If MODIFYMode = True And RsTCSDetail.EOF = True Then
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtPartyName.Text) = "" Then
            MsgBox("Party Name is empty.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtPartyName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Invalid Party Name", vbInformation)
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

        If Val(txtTCSRate.Text) > 100 Then
            MsgBox("Deducted Rate Cann't be Greater Than 100.", MsgBoxStyle.Information)
            txtTCSRate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        FieldsVarification = False
        MsgBox(Err.Description)
    End Function
    Private Sub frmTCSDetail_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        On Error GoTo AssignErr
        SqlStr = ""
        SqlStr = "SELECT MKEY, BILLNO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') AS V_DATE, BOOKTYPE, BOOKSUBTYPE, " & vbCrLf & " ACM.SUPP_CUST_NAME AS PARTYNAME,  " & vbCrLf & " TO_CHAR(NETVALUE,'99,99,99,999.99') AS AMOUNT_PAID, " & vbCrLf & " TO_CHAR(TCSPER,'99,99,99,999.99') AS RATE, " & vbCrLf & " TO_CHAR(TCSAMOUNT,'99,99,99,999.99') AS TCS_AMOUNT, " & vbCrLf & " DECODE(TCSTRN.CANCELLED,'Y','YES','NO') AS CANCELLED  " & vbCrLf & " FROM TCS_TRN TCSTRN, FIN_SUPP_CUST_MST ACM " & vbCrLf & " WHERE " & vbCrLf & " TCSTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TCSTRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TCSTRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf & " AND TCSTRN.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf & " AND TCSTRN.BOOKCODE='" & lblBookCode.Text & "' ORDER BY INVOICE_DATE"

        MainClass.AssignDataInSprd(SqlStr, ADataGrid, StrConn, IIf(mRefresh = True, "Y", "N"))
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
            .set_ColWidth(.Col, 9)

            .Col = 4
            .set_ColWidth(.Col, 0)

            .Col = 5
            .set_ColWidth(.Col, 0)

            .Col = 6
            .set_ColWidth(.Col, 25)

            .Col = 7
            .set_ColWidth(.Col, 8)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            .Col = 8
            .set_ColWidth(.Col, 8)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            .Col = 9
            .set_ColWidth(.Col, 8)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            .Col = 11
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

        SprdView.Col = 2
        txtVNo.Text = SprdView.Text


        TxtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())

    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
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

        If Trim(txtPartyName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtPartyName, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Party Name.")
            Cancel = True
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
        If txtTCSAmount.Enabled = False Then GoTo EventExitSub
        If chkAdditional.CheckState = System.Windows.Forms.CheckState.Checked Then
            GoTo EventExitSub
        End If

        txtTCSAmount.Text = CStr(Val(txtAmountPaid.Text) * Val(txtTCSRate.Text) / 100)
        txtTCSAmount.Text = VB6.Format(System.Math.Round(CDbl(txtTCSAmount.Text), 0), "0.00")

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtTCSAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTCSAmount.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtTCSAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTCSAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTCSAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTCSAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Val(txtTCSAmount.Text) = 0 Then
            GoTo EventExitSub
        End If
        If chkAdditional.CheckState = System.Windows.Forms.CheckState.Checked Then
            GoTo EventExitSub
        End If

        If txtTCSAmount.Enabled = True Then
            If Val(txtAmountPaid.Text) = 0 And Val(txtTCSRate.Text) Then txtTCSAmount.Text = CStr(0) : GoTo EventExitSub
        End If

        If Val(txtAmountPaid.Text) = 0 Then
            txtAmountPaid.Text = CStr(Val(txtTCSAmount.Text) * 100 / IIf(Val(txtTCSRate.Text) = 0, 1, Val(txtTCSRate.Text)))
        ElseIf Val(txtTCSRate.Text) = 0 Then
            txtTCSRate.Text = CStr(Val(txtTCSAmount.Text) * 100 / Val(txtAmountPaid.Text))
        End If
        txtTCSAmount.Text = VB6.Format(System.Math.Round(CDbl(txtTCSAmount.Text), 0), "0.00")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTCSRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTCSRate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtTCSRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTCSRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTCSRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTCSRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtTCSAmount.Enabled = False Then GoTo EventExitSub
        If chkAdditional.CheckState = System.Windows.Forms.CheckState.Checked Then
            GoTo EventExitSub
        End If

        txtTCSAmount.Text = CStr(Val(txtAmountPaid.Text) * Val(txtTCSRate.Text) / 100)
        txtTCSAmount.Text = VB6.Format(System.Math.Round(CDbl(txtTCSAmount.Text), 0), "0.00")

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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
    Private Function CheckChallanMade() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RS As ADODB.Recordset

        SqlStr = "Select CHALLAN_MADE FROM TCS_TRN " & vbCrLf & " WHERE MKEY='" & lblMKey.Text & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            If IsDBNull(RS.Fields("CHALLAN_MADE").Value) Then
                CheckChallanMade = False
            Else
                CheckChallanMade = IIf(RS.Fields("CHALLAN_MADE").Value = "N", False, True)
            End If
        Else
            CheckChallanMade = False
        End If

        If CheckChallanMade = True Then
            MsgInformation("TCS Challan Made Agt. this Entry, So Cann't Modify.")
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


    Public Sub TxtVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtVNo.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsTCSDetail.EOF = False Then xMkey = RsTCSDetail.Fields("mKey").Value

        SqlStr = "SELECT * FROM TCS_TRN WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf _
            & " AND BOOKSUBTYPE='" & lblBookSubType.Text & "'" & vbCrLf _
            & " AND BILLNO='" & MainClass.AllowSingleQuote(UCase(txtVNo.Text)) & "' "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTCSDetail, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTCSDetail.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Does Not Exist." & vbCrLf & "Click Add For New.")
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "Select * from TCS_TRN Where Mkey='" & lblMKey.Text & "' AND BOOKTYPE='" & lblBookType.Text & "' AND BOOKSUBTYPE='" & lblBookSubType.Text & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTCSDetail, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
