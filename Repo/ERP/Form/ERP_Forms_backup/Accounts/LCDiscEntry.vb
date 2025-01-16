Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmLCDiscEntry
    Inherits System.Windows.Forms.Form
    Dim RsPurchMain As ADODB.Recordset ''Recordset
    Dim RsPurchDetail As ADODB.Recordset ''Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    'Dim SqlStr As String=""
    Dim mSupplierCode As String

    Private Const ConRowHeight As Short = 12
    Dim pShowCalc As Boolean
    Dim mBookType As String
    Dim pProcessKey As Double

    Private Const ColParticulars As Short = 1
    Private Const ColHSN As Short = 2
    Private Const ColCreditApplicable As Short = 3
    Private Const ColAmount As Short = 4
    Private Const ColCGSTPer As Short = 5
    Private Const ColCGSTAmount As Short = 6
    Private Const ColSGSTPer As Short = 7
    Private Const ColSGSTAmount As Short = 8
    Private Const ColIGSTPer As Short = 9
    Private Const ColIGSTAmount As Short = 10

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True

            txtVNo.Enabled = False


            pShowCalc = True
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart


        Exit Sub
DelErrPart:

    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPurchMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True

            txtVNo.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            pShowCalc = True
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonAdvanceReceipt(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonAdvanceReceipt(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        Call CalcTots()

        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
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

    Private Sub CmdSearchLC_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchLC.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "SELECT DISTINCT IH.VNO, IH.LC_NO , IH.LC_DATE, IH.LC_AMOUNT, CMST.SUPP_CUST_NAME " & vbCrLf & " FROM FIN_LCOPEN_HDR IH, FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.LC_STATUS='O'" & vbCrLf & " ORDER BY IH.VNO DESC"

        If MainClass.SearchGridMasterBySQL2((txtLCVNo.Text), SqlStr) = True Then
            txtLCVNo.Text = AcName
            txtLCNo.Text = AcName1
            txtLCVNo_Validating(txtLCVNo, New System.ComponentModel.CancelEventArgs(False))
        End If

        '    Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND LC_STATUS='O' "
        '
        '    If MainClass.SearchGridMaster(txtLCVNo.Text, "FIN_LCOPEN_HDR", "VNO", "LC_NO", "LC_DATE", "LC_AMOUNT", Sqlstr) = True Then
        '        txtLCVNo.Text = AcName
        '        txtLCNo.Text = AcName1
        '        txtLCVNo_Validate False
        '    End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xHSNCode As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mGSTApp As String
        'Dim mCGSTPer As Double
        'Dim mSGSTPer As Double
        'Dim mIGSTPer As Double
        'Dim mHSNCode As String

        If eventArgs.row = 0 And eventArgs.col = ColHSN Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColHSN
                '            mHSNCode = Trim(.Text)

                '


                If MainClass.SearchGridMaster(.Text, "GEN_HSN_MST", "HSN_CODE", "GST_APP", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                    .Row = .ActiveRow
                    .Col = ColHSN
                    .Text = AcName
                    xHSNCode = Trim(.Text)
                    mGSTApp = AcName1

                    .Col = ColCreditApplicable
                    .Text = mGSTApp

                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColHSN)
                End If

            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColParticulars Then
            If eventArgs.row = 0 Then NameSearch(eventArgs.col, (SprdMain.ActiveRow))
        End If

        '    If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
        '        With SprdMain
        '            .Row = .ActiveRow
        '            .Col = ColItemCode
        ''            If mainclass.SearchMaster(.Text, "vwITEM", "ITEMCODE", SqlStr) = True Then
        ''                .Row = .ActiveRow
        ''                .Col = ColItemCode
        ''                .Text = AcName
        ''            End If
        '            MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColItemCode
        '        End With
        '    End If
        '
        '    If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
        '        With SprdMain
        '            .Row = .ActiveRow
        '            .Col = ColItemDesc
        '            xIName = .Text
        '            .Text = ""
        ''            If mainclass.SearchMaster(.Text, "vwITEM", "Name", SqlStr) = True Then
        ''                .Row = .ActiveRow
        ''                .Col = ColItemDesc
        ''                .Text = AcName
        ''            Else
        ''                .Row = .ActiveRow
        ''                .Col = ColItemDesc
        ''                .Text = xIName
        ''            End If
        '            MainClass.ValidateWithMasterTable .Text, "Name", "ItemCode", "Item", PubDBCn, MasterNo
        '            .Row = .ActiveRow
        '            .Col = ColItemCode
        '            .Text = MasterNo
        '            MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColItemCode
        '        End With
        '    End If


        If eventArgs.col = 0 And eventArgs.row > 0 Then '***ROW DEL. OPTION NOT REQ IN INVOICE
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColHSN
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColHSN)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                FormatSprdMain(eventArgs.row)
            End If
        End If

        Call CalcTots()
    End Sub

    Private Sub NameSearch(ByRef Col As Integer, ByRef Row As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mString As String
        Dim mTableName As String
        Dim mFieldName1 As String
        Dim mFieldName2 As String
        Dim mDeptCode As String


        SprdMain.Row = Row
        SprdMain.Col = Col
        mString = SprdMain.Text
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'"

        mTableName = "FIN_SUPP_CUST_MST"
        mFieldName1 = "SUPP_CUST_NAME"
        mFieldName2 = "SUPP_CUST_CODE"


        MainClass.SearchGridMaster(mString, mTableName, mFieldName1, mFieldName2, , , SqlStr)

        If AcName <> "" Then
            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = Col
            SprdMain.Text = AcName
        End If


        SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(Col, SprdMain.ActiveRow, Col, SprdMain.ActiveRow, False))

        SprdMain.Refresh()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent

        On Error GoTo ERR1

        If SprdMain.ActiveRow <= 0 Then Exit Sub

        Select Case SprdMain.ActiveCol
            Case ColParticulars
                If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Then NameSearch((SprdMain.ActiveCol), (SprdMain.ActiveRow))

            Case ColAmount
                If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Or eventArgs.KeyCode = System.Windows.Forms.Keys.Tab Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColParticulars, ConRowHeight)
                        'FormatSprdMain -1
                    End If
                End If
        End Select
        eventArgs.KeyCode = 9999
        Exit Sub
ERR1:
        ErrorMsg(Err.Description)
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim RIBBONSGroup As Boolean
        Dim xHSNCode As String
        Dim mGSTApp As String
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mLocal As String
        Dim mPartyGSTNo As String = ""
        Dim pAccountName As String

        If eventArgs.NewRow = -1 Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow


        Select Case eventArgs.col
            Case ColParticulars

                SprdMain.Col = ColParticulars
                pAccountName = Trim(SprdMain.Text)
                If pAccountName = "" Then Exit Sub


                If MainClass.ValidateWithMasterTable(pAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgBox("Account Name Does Not Exist In Master", MsgBoxStyle.Information)
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColParticulars)
                    eventArgs.cancel = True
                    Exit Sub
                End If


            Case ColAmount
                If SprdMain.Row = 1 Then
                    Call PayDetailForm((SprdMain.ActiveRow))
                Else
                    SprdMain.Col = ColAmount
                    If Val(SprdMain.Text) > 0 Then
                        MainClass.AddBlankSprdRow(SprdMain, ColParticulars, ConRowHeight)
                        FormatSprdMain((SprdMain.MaxRows))
                    End If
                End If
            Case ColHSN
                SprdMain.Col = ColHSN
                mLocal = "N"
                If Trim(txtSupplier.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(Trim(txtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mLocal = Trim(MasterNo)
                    End If


                    mPartyGSTNo = ""
                    If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mPartyGSTNo = MasterNo
                    End If

                End If

                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColHSN
                xHSNCode = Trim(SprdMain.Text)

                If xHSNCode = "" Then
                    SprdMain.Col = ColCreditApplicable
                    SprdMain.Text = ""
                    '
                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = "0.00"

                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = "0.00"

                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = "0.00"
                Else
                    mGSTApp = ""
                    mCGSTPer = 0
                    mSGSTPer = 0
                    mIGSTPer = 0

                    If MainClass.ValidateWithMasterTable(Trim(xHSNCode), "HSN_CODE", "GST_APP", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                        mGSTApp = Trim(MasterNo)
                    Else
                        MsgBox("SAC Code Does Not Exist In Master", MsgBoxStyle.Information)
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColHSN)
                        eventArgs.cancel = True
                        Exit Sub
                    End If

                    If GetSACDetails(xHSNCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, mPartyGSTNo, "G") = False Then GoTo ErrPart

                    SprdMain.Col = ColCreditApplicable
                    SprdMain.Text = mGSTApp
                    '
                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = VB6.Format(mCGSTPer, "0.00")

                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = VB6.Format(mSGSTPer, "0.00")

                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = VB6.Format(mIGSTPer, "0.00")
                End If

        End Select

        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub PayDetailForm(ByRef mActiveRow As Integer)

        ConLCDiscPaymentDetail = False
        If ShowDetailForm = "S" Then 'When Account is bill by bill
            If SprdMain.MaxRows = mActiveRow Then
                MainClass.AddBlankSprdRow(SprdMain, ColParticulars, ConRowHeight)
                '                FormatSprdMain -1
            End If
        Else
            If ConLCDiscPaymentDetail = True Then
                SprdMain.Row = mActiveRow
                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(frmPaymentDetail.LblNetAmt.Text))
                If SprdMain.MaxRows = mActiveRow Then
                    MainClass.AddBlankSprdRow(SprdMain, ColParticulars, ConRowHeight)
                    '                    FormatSprdMain -1
                End If
            End If
            frmPaymentDetail.Close()
        End If
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            If eventArgs.Row = 0 Then Exit Sub

            .Row = eventArgs.Row

            .Col = 1
            txtVNoPrefix.Text = .Text

            .Col = 2
            txtVNo.Text = VB6.Format(.Text, "00000")

            .Col = 3
            TxtVDate.Text = VB6.Format(.Text, "DD/MM/YYYY")

            txtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))

            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub

    Private Function ShowDetailForm() As String
        Dim mAccountName As String
        Dim mAmount As Double
        Dim mDC As String
        Dim mNarration As String = ""
        'Dim mEmpCode As String
        Dim mCostCName As String
        Dim mPRRowNo As Integer
        Dim mCostCode As String
        'Dim mAccountCode As String
        'Dim mPartyName As String
        'Dim mCurrRow As Integer
        'Dim cntRow As Integer
        'Dim mSectionCode As Double
        'Dim mBillAmount As Double
        Dim mDivisionCode As Double

        ShowDetailForm = "S"

        With SprdMain
            .Row = .ActiveRow
            mPRRowNo = Val(CStr(.Row))

            .Col = ColParticulars
            mAccountName = SprdMain.Text

            mDC = "DR"

            mCostCode = "001"
            If MainClass.ValidateWithMasterTable(mCostCode, "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCostCName = MasterNo
            Else
                mCostCName = ""
            End If

            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = Val(MasterNo)
            Else
                Exit Function
            End If

            .Col = ColAmount
            mAmount = Val(.Text)
        End With


        If GetAccountBalancingMethod(mAccountName, False) = "D" Then
            ShowDetailForm = "D"
            With frmPaymentDetail
                .lblAccountName.Text = mAccountName
                .lblAmount.Text = CStr(mAmount)
                .lblADDMode.Text = CStr(ADDMode)
                .lblTempProcessKey.Text = CStr(pProcessKey)
                .lblModifyMode.Text = CStr(MODIFYMode)
                .lblDC.Text = mDC
                .lblVDate.Text = TxtVDate.Text
                .lblNarration.Text = mNarration
                .lblBookType.Text = "JV" ''lblBookType.text
                .lblCostCName.Text = mCostCName
                .lblCostCCode.Text = mCostCode
                .lblTrnRowNo.Text = CStr(mPRRowNo)
                .lblDivisionCode.Text = CStr(mDivisionCode)
                .LblMKey.Text = LblMKey.Text
                .cmdPopulate.Enabled = True
                If ADDMode = True Then
                    .cmdAppendDetail.Enabled = False
                Else
                    .cmdAppendDetail.Enabled = True
                End If
                .ShowDialog()
                If ADDMode = True Or MODIFYMode = True Then CmdSave.Enabled = True
            End With
        End If


    End Function

    Private Sub CopyToTempPRDetail()

        On Error GoTo ERR1
        Dim SqlStr As String = ""


        SqlStr = "Insert Into FIN_TEMPBILL_TRN  ( " & vbCrLf & " UserId, TRNDTLSUBROWNO, SUBROWNO, " & vbCrLf & " ACCOUNTCODE, BILLNO, BILLDATE, " & vbCrLf & " BILLAMOUNT, BILLDC, TRNTYPE, " & vbCrLf & " Amount, DC, BOOKTYPE, REMARKS,  " & vbCrLf & " OldAmount, OldDC, OldBillNo, " & vbCrLf & " OldPayType,DUEDATE, " & vbCrLf & " STTYPE, STFORMNAME, STFORMNO, " & vbCrLf & " STFORMDATE, STFORMAMT, STDUEFORMNAME, " & vbCrLf & " STDUEFORMNO, STDUEFORMDATE, STDUEFORMAMT, " & vbCrLf & " ISREGDNO, STFORMCODE, STDUEFORMCODE,TAXABLE_AMOUNT,PONO,DIV_CODE,TEMPMKEY" & vbCrLf & " )"


        SqlStr = SqlStr & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "' , " & vbCrLf & " TRNDTLSUBROWNO, SUBROWNO, " & vbCrLf & " ACCOUNTCODE, BILLNO, BILLDATE, " & vbCrLf & " BILLAMOUNT, BILLDC,TRNTYPE,Amount,DC, " & vbCrLf & " 'JV', " & vbCrLf & " REMARKS, AMOUNT, DC, BILLNO, TRNTYPE,DUEDATE, " & vbCrLf & " STTYPE, STFORMNAME, STFORMNO, " & vbCrLf & " STFORMDATE, STFORMAMT, STDUEFORMNAME, " & vbCrLf & " STDUEFORMNO, STDUEFORMDATE, STDUEFORMAMT, " & vbCrLf & " ISREGDNO, STFORMCODE, STDUEFORMCODE,TAXABLE_AMOUNT,PONO,DIV_CODE," & pProcessKey & " " & vbCrLf & " FROM FIN_BILLDETAILS_TRN Where MKey='" & lblBankMKey.Text & "'"

        PubDBCn.Execute(SqlStr)

        Exit Sub
ERR1:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtAdvBankName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvBankName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvBankName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvBankName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim RsCheckName As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mOPBal As Double
        Dim mBankBookType As String

        If Trim(txtAdvBankName.Text) = "" Then GoTo EventExitSub


        SqlStr = " Select SUPP_CUST_NAME,SUPP_CUST_CODE,STATUS FROM FIN_SUPP_CUST_MST Where " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUPP_CUST_NAME = '" & MainClass.AllowSingleQuote(Trim(txtAdvBankName.Text)) & "'"

        '    SqlStr = SqlStr & vbCrLf & "AND SUPP_CUST_TYPE='2'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCheckName, ADODB.LockTypeEnum.adLockReadOnly)

        If RsCheckName.EOF = True Then
            MsgBox("Invaild Adv. Bank Name. ", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        RsCheckName.Close()
        RsCheckName = Nothing

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchAdvBankName()

        On Error GoTo SearchErr
        Dim SqlStr As String = ""

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND STATUS='O'"

        MainClass.SearchMaster(txtAdvBankName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", SqlStr)
        If AcName <> "" Then
            txtAdvBankName.Text = AcName
            txtAdvBankName_Validating(txtAdvBankName, New System.ComponentModel.CancelEventArgs(True))
        End If
        Exit Sub

SearchErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtAdvBankName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvBankName.DoubleClick
        Call SearchAdvBankName()
    End Sub
    Private Sub txtAdvBankName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvBankName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAdvBankName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAdvBankName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAdvBankName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAdvBankName()
    End Sub

    Private Sub txtBankName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBankName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBankName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim RsCheckName As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mOPBal As Double
        Dim mBankBookType As String

        If Trim(txtBankName.Text) = "" Then GoTo EventExitSub

        mBankBookType = ConBankPayment

        SqlStr = " Select SUPP_CUST_NAME,SUPP_CUST_CODE,STATUS FROM FIN_SUPP_CUST_MST Where " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUPP_CUST_NAME = '" & MainClass.AllowSingleQuote(Trim(txtBankName.Text)) & "'"

        SqlStr = SqlStr & vbCrLf & "AND SUPP_CUST_TYPE='2'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCheckName, ADODB.LockTypeEnum.adLockReadOnly)

        If RsCheckName.EOF = True Then
            MsgBox("Invaild Bank Name. ", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If mBankBookType = ConBankReceipt Or mBankBookType = ConBankPayment Then
            If CheckPendingPDC(mBankBookType) = True Then
                Cancel = True
                GoTo EventExitSub
            End If
        End If

        '    If MainClass.ValidateWithMasterTable(txtBankName.Text, "VNAME", "VTYPE", "FIN_VOUCHERTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BOOKTYPE='" & vb.Left(mBankBookType, 1) & "'") = True Then
        '        txtVType.Text = MasterNo
        '    End If

        If IsDate(TxtVDate.Text) Then
            mOPBal = GetOpeningBal((RsCheckName.Fields("SUPP_CUST_CODE").Value), (TxtVDate.Text))
        End If
        txtBookBalAmt.Text = VB6.Format(System.Math.Abs(mOPBal), "0.00") & IIf(mOPBal >= 0, "Dr", "Cr")
        RsCheckName.Close()
        RsCheckName = Nothing

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchName()

        On Error GoTo SearchErr
        Dim SqlStr As String = ""

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE = '2' AND STATUS='O'"

        MainClass.SearchMaster(txtBankName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", SqlStr)
        If AcName <> "" Then
            txtBankName.Text = AcName
            txtBankName_Validating(txtBankName, New System.ComponentModel.CancelEventArgs(True))
        End If
        Exit Sub

SearchErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckPendingPDC(ByRef pBankBookType As Object) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsPDC As ADODB.Recordset = Nothing
        Dim xBookType As String = ""
        Dim xBookSubType As String = ""
        Dim xAccountCode As String
        Dim mChq As String = ""

        If VB.Right(pBankBookType, 1) = "R" Then
            xBookType = VB.Left(ConPDCReceipt, 1)
            xBookSubType = VB.Right(ConPDCReceipt, 1)
        ElseIf VB.Right(pBankBookType, 1) = "P" Then
            xBookType = VB.Left(ConPDCPayment, 1)
            xBookSubType = VB.Right(ConPDCPayment, 1)
        End If

        If MainClass.ValidateWithMasterTable((txtBankName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = True Then
            xAccountCode = IIf(IsDbNull(MasterNo), -1, MasterNo)
        Else
            xAccountCode = CStr(-1)
        End If

        SqlStr = "SELECT 'VNO : ' || FIN_VOUCHER_HDR.VNO || ':' || 'CHQ NO : ' || CHEQUENO AS VNO FROM FIN_VOUCHER_HDR,FIN_VOUCHER_DET" & vbCrLf & " WHERE FIN_VOUCHER_HDR.MKEY=FIN_VOUCHER_DET.MKEY " & vbCrLf & " AND COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & " AND CHQDATE<=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND BOOKTYPE='" & xBookType & "'" & vbCrLf & " AND BOOKSUBTYPE='" & xBookSubType & "' AND BOOKCODE='" & xAccountCode & "' AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPDC, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPDC.EOF = False Then
            Do While Not RsPDC.EOF
                mChq = IIf(mChq = "", "", mChq & vbNewLine) & IIf(IsDbNull(RsPDC.Fields("VNO").Value), "", RsPDC.Fields("VNO").Value)
                RsPDC.MoveNext()
            Loop
            MsgBox("Following PDC are pending for Normalization " & vbNewLine & mChq, MsgBoxStyle.Information)
            CheckPendingPDC = True
        Else
            CheckPendingPDC = False
        End If
        RsPDC.Close()
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        RsPDC.Close()
    End Function
    Private Sub txtBankName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.DoubleClick
        Call SearchName()
    End Sub
    Private Sub txtBankName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBankName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBankName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchName()
    End Sub

    Private Sub txtBankVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBankVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtBankVDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtBankVDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtBankVDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBankVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankVNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankVNoSuffix_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankVNoSuffix.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtBookBalAmt_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBookBalAmt.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChqDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChqDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChqDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtChqDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtChqDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtChqDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtChqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChqNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChqNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtChqNo.Text) <> "" Then
            If GetChequeStatus(Trim(txtChqNo.Text)) = False Then
                Cancel = True
                GoTo EventExitSub
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Function GetChequeStatus(ByRef mChequeNo As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim mBankCode As String
        Dim mChequeStatus As String
        Dim mVMkey As String

        If Trim(TxtVDate.Text) = "" Then GetChequeStatus = True : Exit Function

        If MainClass.ValidateWithMasterTable((txtBankName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = True Then
            mBankCode = MasterNo
        Else
            MsgBox("Bank Does Not Exist In Master.")
            GetChequeStatus = False
            Exit Function
        End If

        SqlStr = "SELECT CHEQUE_STATUS,VMKEY FROM FIN_CHEQUE_MST  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND BANKCODE='" & mBankCode & "'" & vbCrLf & " AND CHEQUE_NO='" & MainClass.AllowSingleQuote(Trim(mChequeNo)) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            mChequeStatus = IIf(IsDbNull(RS.Fields("CHEQUE_STATUS").Value), "C", RS.Fields("CHEQUE_STATUS").Value)
            mVMkey = IIf(IsDbNull(RS.Fields("VMkey").Value), "", RS.Fields("VMkey").Value)

            If mChequeStatus = "O" Then
                GetChequeStatus = True
            Else
                If mVMkey = Trim(LblMKey.Text) Then
                    GetChequeStatus = True
                Else
                    MsgBox("Cheque No for such Bank Already Issue.")
                    GetChequeStatus = False
                End If
            End If
        Else
            MsgBox("No Cheque Allocated for such Bank.")
            GetChequeStatus = False
        End If
        Exit Function
ErrPart:
        GetChequeStatus = False
    End Function

    Private Sub txtLCAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLCAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLCAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLCAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtLCDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLCDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLCDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLCDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtLCDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtLCDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtLCDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtLCNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLCNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLCNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLCNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLCNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtLCVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLCVNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLCVNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLCVNo.DoubleClick
        CmdSearchLC_Click(CmdSearchLC, New System.EventArgs())
    End Sub

    Private Sub txtLCVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLCVNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLCVNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLCVNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtLCVNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then CmdSearchLC_Click(CmdSearchLC, New System.EventArgs())
    End Sub

    Private Sub txtLCVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLCVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If Trim(txtLCVNo.Text) = "" Then GoTo EventExitSub
        If txtLCVNo.Enabled = False Then GoTo EventExitSub

        SqlStr = " SELECT * FROM FIN_LCOPEN_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND VNO='" & txtLCVNo.Text & "' AND LC_STATUS='O'" '' AND ISFINALPOST='N'"

        If Trim(txtLCNo.Text) = "" Then
            SqlStr = SqlStr & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "  "
        Else
            SqlStr = SqlStr & vbCrLf & " AND LC_NO='" & Trim(txtLCNo.Text) & "'  "
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Clear1()
            If ShowFromLCMain(RsTemp) = False Then
                Cancel = True
                GoTo EventExitSub
            End If
        Else
            ErrorMsg("Either InValid LC No. OR Closed.", "", MsgBoxStyle.Critical)
            Cancel = True
        End If
        FormatSprdMain(-1)
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function ShowFromLCMain(ByRef mRsDC As ADODB.Recordset) As Boolean
        On Error GoTo ErrPart
        Dim mFormCode As Integer
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mBankCode As String
        Dim mAdvBankCode As String
        Dim mLCAmount As Double
        Dim mLCDiscountedAmt As Double

        With mRsDC
            lblLCMkey.Text = IIf(IsDbNull(.Fields("mKey").Value), "", .Fields("mKey").Value)
            mBankCode = IIf(IsDbNull(.Fields("BANK_CODE").Value), "", .Fields("BANK_CODE").Value)

            If MainClass.ValidateWithMasterTable(mBankCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtBankName.Text = MasterNo
            End If

            mAdvBankCode = IIf(IsDbNull(.Fields("ADV_BANK_CODE").Value), "", .Fields("ADV_BANK_CODE").Value)

            If MainClass.ValidateWithMasterTable(mAdvBankCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtAdvBankName.Text = MasterNo
            End If

            txtLCVNo.Text = IIf(IsDbNull(.Fields("VNO").Value), "", .Fields("VNO").Value)
            txtLCVDate.Text = VB6.Format(IIf(IsDbNull(.Fields("VDATE").Value), "", .Fields("VDATE").Value), "DD/MM/YYYY")

            If MainClass.ValidateWithMasterTable(.Fields("SUPP_CUST_CODE"), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtSupplier.Text = MasterNo
            End If

            mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

            If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionDesc = Trim(MasterNo)
                cboDivision.Text = mDivisionDesc
            End If

            txtRemarks.Text = IIf(IsDbNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)

            txtRefNo.Text = IIf(IsDbNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)
            txtRefDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")

            txtLCNo.Text = IIf(IsDbNull(.Fields("LC_NO").Value), "", .Fields("LC_NO").Value)
            txtLCDate.Text = VB6.Format(IIf(IsDbNull(.Fields("LC_DATE").Value), "", .Fields("LC_DATE").Value), "DD/MM/YYYY")
            txtLCAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("LC_AMOUNT").Value), "", .Fields("LC_AMOUNT").Value), "0.00")

            mLCAmount = GetLCOpeningAmount((lblLCMkey.Text))
            mLCDiscountedAmt = GetLCDiscountingAmount((lblLCMkey.Text))

            txtDiscAmount.Text = VB6.Format(mLCAmount - mLCDiscountedAmt, "0.00") ''Format(IIf(IsNull(!LC_AMOUNT), "", !LC_AMOUNT), "0.00")

            '     txtRefNo.Text = IIf(IsNull(!REF_NO), "", !REF_NO)
            '     txtRefDate.Text = Format(IIf(IsNull(!REF_DATE), "", !REF_DATE), "DD/MM/YYYY")


            cboDivision.Enabled = False
            Call txtBankName_Validating(txtBankName, New System.ComponentModel.CancelEventArgs(True))
            '     Call ShowDetail1(LblMKey.text)

        End With
        Call CalcTots()
        ShowFromLCMain = True
        Exit Function
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ShowFromLCMain = False
    End Function

    Private Sub txtRefNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRefNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRefNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRefDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRefDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtRefDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtRefDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(TxtVDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(TxtVDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((TxtVDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Public Sub txtVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mVNo As String
        Dim SqlStr As String = ""
        If Trim(txtVNo.Text) = "" Then GoTo EventExitSub

        txtVNo.Text = VB6.Format(Val(txtVNo.Text), "00000")

        If MODIFYMode = True And RsPurchMain.EOF = False Then xMKey = RsPurchMain.Fields("mKey").Value
        mVNo = Trim(Trim(txtVNoPrefix.Text) & Trim(txtVNo.Text))

        SqlStr = " SELECT * FROM FIN_LCDISC_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND VNo='" & MainClass.AllowSingleQuote(mVNo) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsPurchMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Voucher, Use Generate Voucher Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM FIN_LCDISC_HDR " & " WHERE Mkey='" & MainClass.AllowSingleQuote(xMKey) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim I As Short
        Dim SqlStr As String = ""
        Dim nMkey As String
        Dim mTRNType As String
        Dim mVNoSeq As Integer
        Dim mVNo As String
        Dim mSuppCustCode As String
        Dim mItemValue As Double
        Dim mNETVALUE As Double

        Dim cntRow As Integer

        Dim mItemCode As String
        Dim mLocal As String
        Dim mDivisionCode As Double

        Dim mTotGSTAmount As Double

        Dim pBankVoucherMkey As String
        Dim mBankCode As String
        Dim mAdvBankCode As String
        Dim mGSTNo As Double
        Dim mNewGSTNo As Boolean
        Dim mLCAmount As Double
        Dim mLCDiscountedAmt As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        mBankCode = CStr(-1)
        If MainClass.ValidateWithMasterTable((txtBankName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mBankCode = MasterNo
        Else
            mBankCode = CStr(-1)
            MsgBox("Bank Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mAdvBankCode = CStr(-1)
        If MainClass.ValidateWithMasterTable((txtAdvBankName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAdvBankCode = MasterNo
        Else
            mAdvBankCode = CStr(-1)
            MsgBox("Adv. Bank Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mSuppCustCode = CStr(-1)
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = CStr(-1)
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        If MainClass.ValidateWithMasterTable(mSuppCustCode, "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mLocal = Trim(MasterNo)
        Else
            mLocal = "N"
        End If

        mItemValue = Val(lblTotItemValue.Text)
        mNETVALUE = Val(lblNetAmount.Text)


        mTotGSTAmount = Val(lblTotCGSTAmount.Text) + Val(lblTotSGSTAmount.Text) + Val(lblTotIGSTAmount.Text)

        If Trim(txtVNo.Text) = "" Then
            mVNoSeq = CInt(AutoGenSeqBillNo("VNOSEQ"))
        Else
            mVNoSeq = Val(txtVNo.Text)
        End If

        txtVNo.Text = IIf(mVNoSeq = -1 Or mVNoSeq = 0, "", VB6.Format(Val(CStr(mVNoSeq)), "00000"))

        If mVNoSeq = -1 Or mVNoSeq = 0 Then
            mVNo = "-1"
        Else
            mVNo = Trim(Trim(txtVNoPrefix.Text) & VB6.Format(Val(CStr(mVNoSeq)), "00000"))
        End If

        '    If Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text) > 0 Then
        '        If Trim(txtModvatNo.Text) = "" Or Val(txtModvatNo.Text) = 0 Then
        '            mGSTNo = AutoGenSeqGSTNo()
        '            txtModvatDate.Text = Format(txtVDate.Text, "DD/MM/YYYY")
        '            mNewGSTNo = True
        '        Else
        '            mGSTNo = Val(txtModvatNo.Text)
        '        End If
        '    End If

        SqlStr = ""

        mGSTNo = Val(txtModvatNo.Text)

        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("FIN_LCDISC_HDR", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo
            LblMKey.Text = nMkey

            SqlStr = "INSERT INTO FIN_LCDISC_HDR( " & vbCrLf & " MKEY, COMPANY_CODE, FYEAR, ROWNO," & vbCrLf & " BOOKTYPE, VNOPREFIX, VNOSEQ, VNO, VDATE, " & vbCrLf & " LCMKEY, LCVNO, LCVDATE, " & vbCrLf & " SUPP_CUST_CODE,  " & vbCrLf & " DIV_CODE, REMARKS, ITEMVALUE," & vbCrLf & " NETVALUE,  " & vbCrLf & " TOTALGSTVALUE, " & vbCrLf & " TOTCGST_AMOUNT, TOTSGST_AMOUNT, TOTIGST_AMOUNT," & vbCrLf & " TOTCGST_CREDITAMT, TOTSGST_CREDITAMT, TOTIGST_CREDITAMT," & vbCrLf & " BANKVOUCHERMKEY," & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE, " & vbCrLf & " CHEQUENO, CHQDATE, BANK_CODE, LC_NO, LC_DATE, LC_AMOUNT, ADV_BANK_CODE, " & vbCrLf & " REF_NO, REF_DATE, GST_CLAIM_NO, GST_CLAIM_DATE, DISC_AMOUNT)"


            SqlStr = SqlStr & vbCrLf & " VALUES('" & nMkey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & mCurRowNo & ", " & vbCrLf & " '" & mBookType & "',  '" & MainClass.AllowSingleQuote(txtVNoPrefix.Text) & "', " & mVNoSeq & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(mVNo) & "', TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(lblLCMkey.Text) & "','" & MainClass.AllowSingleQuote(txtLCVNo.Text) & "', TO_DATE('" & VB6.Format(txtLCVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mSuppCustCode & "',  " & vbCrLf & " " & mDivisionCode & ", '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & mItemValue & ", " & vbCrLf & " " & mNETVALUE & ",  " & vbCrLf & " " & Val(CStr(mTotGSTAmount)) & ", " & vbCrLf & " " & Val(lblTotCGSTAmount.Text) & ", " & Val(lblTotSGSTAmount.Text) & ", " & Val(lblTotIGSTAmount.Text) & ", " & vbCrLf & " " & Val(lblCGSTRefundAmount.Text) & ", " & Val(lblSGSTRefundAmount.Text) & ", " & Val(lblIGSTRefundAmount.Text) & ", " & vbCrLf & " '', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf & " '', '', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtChqNo.Text) & "', TO_DATE('" & VB6.Format(txtChqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mBankCode & "', '" & MainClass.AllowSingleQuote(txtLCNo.Text) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtLCDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtLCAmount.Text) & ", '" & mAdvBankCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtRefNo.Text) & "', TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(CStr(mGSTNo)) & ", TO_DATE('" & VB6.Format(txtModvatDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(txtDiscAmount.Text) & ")"


        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE FIN_LCDISC_HDR SET " & vbCrLf & " VNOPREFIX = '" & MainClass.AllowSingleQuote(txtVNoPrefix.Text) & "'," & vbCrLf & " VNOSEQ= " & mVNoSeq & ", " & vbCrLf & " VNO= '" & MainClass.AllowSingleQuote(mVNo) & "'," & vbCrLf & " VDATE= TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " LCMKEY='" & MainClass.AllowSingleQuote(lblLCMkey.Text) & "'," & vbCrLf & " LCVNO='" & MainClass.AllowSingleQuote(txtLCVNo.Text) & "', " & vbCrLf & " LCVDATE=TO_DATE('" & VB6.Format(txtLCVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),"

            SqlStr = SqlStr & vbCrLf & " REF_NO='" & MainClass.AllowSingleQuote(txtRefNo.Text) & "', REF_DATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " BOOKTYPE='" & mBookType & "', " & vbCrLf & " SUPP_CUST_CODE='" & mSuppCustCode & "', " & vbCrLf & " DIV_CODE=" & mDivisionCode & ", " & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf & " ITEMVALUE=" & mItemValue & "," & vbCrLf & " NETVALUE=" & mNETVALUE & ", DISC_AMOUNT=" & Val(txtDiscAmount.Text) & "," & vbCrLf & " TOTALGSTVALUE=" & Val(CStr(mTotGSTAmount)) & ", " & vbCrLf & " TOTCGST_AMOUNT=" & Val(lblTotCGSTAmount.Text) & ", " & vbCrLf & " TOTSGST_AMOUNT=" & Val(lblTotSGSTAmount.Text) & ", " & vbCrLf & " TOTIGST_AMOUNT=" & Val(lblTotIGSTAmount.Text) & ", " & vbCrLf & " TOTCGST_CREDITAMT = " & Val(lblCGSTRefundAmount.Text) & ", TOTSGST_CREDITAMT = " & Val(lblSGSTRefundAmount.Text) & ", " & vbCrLf & " TOTIGST_CREDITAMT = " & Val(lblIGSTRefundAmount.Text) & ", " & vbCrLf & " CHEQUENO = '" & MainClass.AllowSingleQuote(txtChqNo.Text) & "', " & vbCrLf & " CHQDATE = TO_DATE('" & VB6.Format(txtChqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " BANK_CODE='" & mBankCode & "', " & vbCrLf & " LC_NO = '" & MainClass.AllowSingleQuote(txtLCNo.Text) & "', " & vbCrLf & " LC_DATE = TO_DATE('" & VB6.Format(txtLCDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " LC_AMOUNT=" & Val(txtLCAmount.Text) & ", " & vbCrLf & " ADV_BANK_CODE='" & mAdvBankCode & "', GST_CLAIM_NO=" & Val(CStr(mGSTNo)) & ", GST_CLAIM_DATE= TO_DATE('" & VB6.Format(txtModvatDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),"

            SqlStr = SqlStr & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " WHERE Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)

        '    If mNewGSTNo = True And (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text) > 0) Then
        '        If UpdateGSTSeqMaster(PubDBCn, LblMKey.text, ConLDBookCode, Left(lblBookType.text, 1), Right(lblBookType.text, 1), _
        ''                mGSTNo, Format(txtModvatDate.Text, "DD-MMM-YYYY"), "N", "N", "S" _
        ''                ) = False Then GoTo ErrPart:
        '    End If

        If UpdateDetail1(mVNo, mSuppCustCode, mDivisionCode) = False Then GoTo ErrPart

        If ADDMode = True Then
            pBankVoucherMkey = ""
            If GenerateBankVoucher(pBankVoucherMkey, mDivisionCode, True) = False Then
                GoTo ErrPart
            End If
            SqlStr = "UPDATE FIN_LCDISC_HDR SET BANKVOUCHERMKEY='" & pBankVoucherMkey & "' WHERE MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"
            PubDBCn.Execute(SqlStr)
        Else
            pBankVoucherMkey = lblBankMKey.Text
            If GenerateBankVoucher(pBankVoucherMkey, mDivisionCode, False) = False Then
                GoTo ErrPart
            End If
        End If

        If Trim(txtVType.Text & txtBankVNo.Text) <> "" And ADDMode = True Then
            MsgBox(" Voucher No. " & Trim(txtVType.Text & txtBankVNo.Text) & " Created. ", MsgBoxStyle.Information)
        End If

        mLCAmount = GetLCOpeningAmount((lblLCMkey.Text))
        mLCDiscountedAmt = GetLCDiscountingAmount((lblLCMkey.Text))

        If Val(CStr(mLCAmount)) = Val(CStr(mLCDiscountedAmt)) Then
            SqlStr = "UPDATE FIN_LCOPEN_HDR SET LC_STATUS='C' WHERE MKEY='" & MainClass.AllowSingleQuote(lblLCMkey.Text) & "'"
        Else
            SqlStr = "UPDATE FIN_LCOPEN_HDR SET LC_STATUS='O' WHERE MKEY='" & MainClass.AllowSingleQuote(lblLCMkey.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        UpdateMain1 = True

        Exit Function
ErrPart:
        '    Resume
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsPurchMain.Requery() ''.Refresh
        RsPurchDetail.Requery() ''.Refresh
        If ADDMode = True Then
            txtVNo.Text = ""
        End If

        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '    Resume
    End Function

    Private Function GenerateBankVoucher(ByRef pBankVoucherMkey As String, ByRef mDivCode As Double, ByRef pAddMode As Boolean) As Boolean

        On Error GoTo ErrPart
        Dim mBookCode As String
        Dim SqlStr As String = ""
        Dim mAccountCode As String
        Dim mDrCr As String
        Dim mVAmount As Double

        Dim mVnoStr As String
        Dim mVType As String

        Dim mVNoPrefix As String
        Dim mVNoSuffix As String

        Dim mBookType As String
        Dim mBookSubType As String
        Dim mVNo As String
        Dim mCancelled As String
        Dim pRowNo As Integer
        Dim CurBankMKey As String
        Dim pBankBookType As String

        '    If Right(lblBookType.text, 1) = "R" Then
        '        pBankBookType = ConBankReceipt
        '    Else
        '        pBankBookType = ConBankPayment
        '    End If

        pBankBookType = ConJournal

        mBookType = VB.Left(pBankBookType, 1)
        mBookSubType = VB.Right(pBankBookType, 1)

        If pAddMode = True Then
            mVNo = GenBankVno(pBankBookType)
        Else
            mVNo = VB6.Format(txtBankVNo.Text, "00000")
        End If
        mVNoPrefix = GenPrefixVNo(txtVDate.Text)
        mVNoSuffix = ""
        mVType = Trim(txtVType.Text)
        mVnoStr = mVType & mVNoPrefix & mVNo & mVNoSuffix
        txtBankVNo.Text = mVNo

        mCancelled = "N"

        '    If MainClass.ValidateWithMasterTable(txtBankName, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mBookCode = MasterNo
        '    End If

        mBookCode = CStr(ConJournalBookCode)

        If pAddMode = True Then
            pRowNo = MainClass.AutoGenRowNo("FIN_VOUCHER_HDR", "RowNo", PubDBCn)
            CurBankMKey = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value) & VB6.Format(RsCompany.Fields("FYEAR").Value) & VB6.Format(pRowNo)
            pBankVoucherMkey = CurBankMKey

            SqlStr = " INSERT INTO FIN_VOUCHER_HDR ( " & vbCrLf & " Mkey, COMPANY_CODE, " & vbCrLf & " FYEAR,RowNo, VType, VNoPrefix, VNoSeq, VNoSuffix, " & vbCrLf & " Vno, Vdate, BookType,BookSubType, " & vbCrLf & " BookCode, Narration, CANCELLED, " & vbCrLf & " AddUser, AddDate, ModUser, ModDate,UPDATE_FROM, EXPDATE,IS_REVERSAL_MADE, IS_REVERSAL_VOUCHER, REVERSAL_MKEY) VALUES ( " & vbCrLf & " '" & CurBankMKey & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & pRowNo & ", " & vbCrLf & " '" & mVType & "', '" & mVNoPrefix & "', " & vbCrLf & " " & Val(mVNo) & ", '" & mVNoSuffix & "', '" & mVnoStr & "', " & vbCrLf & " TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mBookType & "', '" & mBookSubType & "', " & vbCrLf & " '" & mBookCode & "', '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', '" & mCancelled & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','N', " & vbCrLf & " TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'N','N','')"

        Else
            CurBankMKey = lblBankMKey.Text
            pBankVoucherMkey = CurBankMKey
            SqlStr = "UPDATE FIN_VOUCHER_HDR SET " & vbCrLf & " Vdate=TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " EXPDATE=TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " VType= '" & mVType & "'," & vbCrLf & " VnoPrefix='" & mVNoPrefix & "', " & vbCrLf & " VnoSeq=" & Val(mVNo) & ", " & vbCrLf & " VnoSuffix='" & mVNoSuffix & "', " & vbCrLf & " Vno='" & mVnoStr & "', " & vbCrLf & " BookCode='" & mBookCode & "', " & vbCrLf & " Narration='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf & " CANCELLED='" & mCancelled & "', " & vbCrLf & " BookType='" & mBookType & "', " & vbCrLf & " BookSubType='" & mBookSubType & "', " & vbCrLf & " UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " Where Mkey='" & CurBankMKey & "'"
        End If

        PubDBCn.Execute(SqlStr)
        If GenerateBankDetail(CurBankMKey, pRowNo, mBookCode, pBankBookType, mVType, mVnoStr, (TxtVDate.Text), (txtRemarks.Text), mDivCode, PubDBCn) = False Then GoTo ErrPart


        '    mVAmount = Val(CDbl(lblNetAmount.text))
        '    mDrCr = "C"
        '
        '    If UpdateTRN(PubDBCn, CurBankMKey, pRowNo, -1, mBookCode, mVType, mBookType, _
        ''            mBookSubType, mBookCode, mVnoStr, txtVDate.Text, mVnoStr, txtVDate.Text, _
        ''            mVAmount, mDrCr, "P", "", "", -1, -1, -1, -1, "", _
        ''            "", "P", "", "", txtRemarks.Text, "", txtVDate.Text, ADDMode, PubUserID, Format(PubCurrDate, "DD-MMM-YYYY"), mDivCode, "N") = False Then GoTo ErrPart
        '
        '    If (pBankBookType = ConBankPayment) And Trim(txtChqNo.Text) <> "" Then
        '        If UpdateChequeDetail(Trim(txtChqNo.Text), CurBankMKey, "C") = False Then GoTo ErrPart
        '    End If

        GenerateBankVoucher = True
        Exit Function
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GenerateBankVoucher = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function GetLCOpeningAmount(ByRef pLCMKey As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetLCOpeningAmount = 0

        SqlStr = " SELECT SUM(LC_AMOUNT) AS LC_AMOUNT" & vbCrLf & " FROM FIN_LCOPEN_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MKEY='" & pLCMKey & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetLCOpeningAmount = IIf(IsDbNull(RsTemp.Fields("LC_AMOUNT").Value), 0, RsTemp.Fields("LC_AMOUNT").Value)
        End If
        Exit Function
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetLCOpeningAmount = 0
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function GetLCDiscountingAmount(ByRef pLCMKey As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetLCDiscountingAmount = 0

        SqlStr = " SELECT SUM(DISC_AMOUNT) AS NETVALUE" & vbCrLf & " FROM FIN_LCDISC_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND LCMKEY='" & pLCMKey & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetLCDiscountingAmount = IIf(IsDbNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
        End If
        Exit Function
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetLCDiscountingAmount = 0
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function UpdateChequeDetail(ByRef mChequeNo As String, ByRef VMkey As String, ByRef mChqStatus As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim mBankCode As String
        Dim pVMkey As String

        If MainClass.ValidateWithMasterTable(txtBankName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = True Then
            mBankCode = MasterNo
        Else
            MsgBox("Bank Does Not Exist In Master.")
            UpdateChequeDetail = False
            Exit Function
        End If

        SqlStr = "UPDATE FIN_CHEQUE_MST SET " & vbCrLf & " CHEQUE_STATUS='" & mChqStatus & "'," & vbCrLf & " VMKEY='" & VMkey & "'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND BANKCODE='" & mBankCode & "'" & vbCrLf & " AND CHEQUE_NO='" & MainClass.AllowSingleQuote(Trim(mChequeNo)) & "'"
        PubDBCn.Execute(SqlStr)
        UpdateChequeDetail = True
        Exit Function
ErrPart:
        UpdateChequeDetail = False
    End Function
    Private Function GenerateBankDetail(ByRef mMkey As String, ByRef mRowNo As Integer, ByRef mBookCode As String, ByRef pBankBookType As String, ByRef mVType As String, ByRef mVNo As String, ByRef mVDate As String, ByRef pNarration As String, ByRef mDivCode As Double, ByRef pDBCn As ADODB.Connection) As Boolean


        On Error GoTo ErrDetail

        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mAccountName As String
        Dim mAccountCode As String = ""
        Dim mChequeNo As String
        Dim mChqDate As String
        Dim mAmount As Double
        Dim mCCCode As String
        Dim mDeptCode As String
        Dim mEmpCode As String
        Dim mExpCode As String
        Dim mIBRNo As String
        Dim mDC As String
        Dim mRemarks As String = ""
        Dim mPRRowNo As Integer
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mClearDate As String
        Dim mParticulars As String
        Dim cntRow As Integer
        Dim mCreditApplicable As String


        mBookType = VB.Left(pBankBookType, 1)
        mBookSubType = VB.Right(pBankBookType, 1)

        '    SqlStr = "Delete From FIN_TEMPBILL_TRN Where UserId='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        '    pDBCn.Execute SqlStr

        SqlStr = "Delete From FIN_BILLDETAILS_TRN Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)

        SqlStr = "Delete From FIN_VOUCHER_DET Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)


        SqlStr = "DELETE FROM FIN_POSTED_TRN  WHERE " & vbCrLf & " MKEY ='" & mMkey & "' " & vbCrLf & " AND BookType='" & mBookType & "'" & vbCrLf & " AND BooksubType='" & mBookSubType & "'"
        pDBCn.Execute(SqlStr)

        If (pBankBookType = ConBankPayment) Then
            SqlStr = "UPDATE FIN_CHEQUE_MST SET " & vbCrLf & " CHEQUE_STATUS='O'," & vbCrLf & " VMKEY=''," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND VMKEY='" & MainClass.AllowSingleQuote(Trim(mMkey)) & "'"
            PubDBCn.Execute(SqlStr)
        End If


        cntRow = 1
        For cntRow = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = cntRow
            SprdMain.Col = ColParticulars

            mAccountName = Trim(SprdMain.Text)
            If mAccountName <> "" Then
                mPRRowNo = cntRow
                mDC = "D"
                mAccountCode = IIf(MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", pDBCn, mAccountCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mAccountCode, -1)

                SprdMain.Col = ColAmount
                mAmount = Val(SprdMain.Text)

                SprdMain.Col = ColCreditApplicable
                mCreditApplicable = Trim(SprdMain.Text)

                SprdMain.Col = ColCGSTAmount
                mAmount = mAmount + IIf(mCreditApplicable = "N", Val(SprdMain.Text), 0)

                SprdMain.Col = ColSGSTAmount
                mAmount = mAmount + IIf(mCreditApplicable = "N", Val(SprdMain.Text), 0)

                SprdMain.Col = ColIGSTAmount
                mAmount = mAmount + IIf(mCreditApplicable = "N", Val(SprdMain.Text), 0)

                mParticulars = pNarration

                mChequeNo = txtChqNo.Text
                mChqDate = VB6.Format(txtChqDate.Text, "DD/MM/YYYY")
                mCCCode = "-1"
                mDeptCode = "-1"
                mEmpCode = "-1"
                mExpCode = "-1"
                mIBRNo = "-1"
                mClearDate = ""
                I = cntRow + 1

                SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivCode & ")"

                PubDBCn.Execute(SqlStr)

                If GetAccountBalancingMethod(mAccountCode, True) = "S" Or cntRow = 1 Then
                    If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", "N", mBookType, mBookSubType, VB6.Format(mVDate, "DD/MM/YYYY"), ADDMode, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivCode, pProcessKey, "N") = False Then GoTo ErrDetail
                Else
                    If UpdateSuppPRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, mAmount, mRemarks, mDivCode) = False Then GoTo ErrDetail
                End If
            End If
        Next

        '************************** GST (RECOVERY ACCOUNT)
        'CGST ACCOUNT POSTING(RECOVERY ACCOUNT)

        mPRRowNo = cntRow + 2
        mDC = "D"

        mAccountCode = IIf(IsDbNull(RsCompany.Fields("CGST_REFUNDCODE").Value), "-1", RsCompany.Fields("CGST_REFUNDCODE").Value)
        mAmount = Val(lblCGSTRefundAmount.Text)

        mParticulars = pNarration


        mChequeNo = txtChqNo.Text
        mChqDate = VB6.Format(txtChqDate.Text, "DD/MM/YYYY")
        mCCCode = "-1"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "-1"
        mIBRNo = "-1"
        mClearDate = ""
        I = cntRow + 2

        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivCode & " )"

            PubDBCn.Execute(SqlStr)

            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", "N", mBookType, mBookSubType, VB6.Format(mVDate, "DD/MM/YYYY"), ADDMode, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivCode, pProcessKey, "N") = False Then GoTo ErrDetail
        End If

        'SGST ACCOUNT POSTING(RECOVERY ACCOUNT)

        mPRRowNo = cntRow + 3
        mDC = "D"

        mAccountCode = IIf(IsDbNull(RsCompany.Fields("SGST_REFUNDCODE").Value), "-1", RsCompany.Fields("SGST_REFUNDCODE").Value)
        mAmount = Val(lblSGSTRefundAmount.Text)


        mParticulars = pNarration


        mChequeNo = txtChqNo.Text
        mChqDate = VB6.Format(txtChqDate.Text, "DD/MM/YYYY")
        mCCCode = "-1"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "-1"
        mIBRNo = "-1"
        mClearDate = ""
        I = cntRow + 3

        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivCode & " )"

            PubDBCn.Execute(SqlStr)

            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", "N", mBookType, mBookSubType, VB6.Format(mVDate, "DD/MM/YYYY"), ADDMode, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivCode, pProcessKey, "N") = False Then GoTo ErrDetail
        End If

        'IGST ACCOUNT POSTING(RECOVERY ACCOUNT)

        mPRRowNo = cntRow + 4
        mDC = "D"

        mAccountCode = IIf(IsDbNull(RsCompany.Fields("IGST_REFUNDCODE").Value), "-1", RsCompany.Fields("IGST_REFUNDCODE").Value)
        mAmount = Val(lblIGSTRefundAmount.Text)


        mParticulars = pNarration


        mChequeNo = txtChqNo.Text
        mChqDate = VB6.Format(txtChqDate.Text, "DD/MM/YYYY")
        mCCCode = "-1"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "-1"
        mIBRNo = "-1"
        mClearDate = ""
        I = cntRow + 4

        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivCode & " )"

            PubDBCn.Execute(SqlStr)

            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", "N", mBookType, mBookSubType, VB6.Format(mVDate, "DD/MM/YYYY"), ADDMode, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivCode, pProcessKey, "N") = False Then GoTo ErrDetail
        End If

        '******SUPPLIER ACCOUNT POSTING


        mAccountName = Trim(txtAdvBankName.Text)
        If mAccountName <> "" Then
            mPRRowNo = cntRow + 5
            mDC = "C"
            mAccountCode = IIf(MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", pDBCn, mAccountCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mAccountCode, -1)
            mAmount = Val(txtDiscAmount.Text)

            mParticulars = pNarration

            mChequeNo = txtChqNo.Text
            mChqDate = VB6.Format(txtChqDate.Text, "DD/MM/YYYY")
            mCCCode = "-1"
            mDeptCode = "-1"
            mEmpCode = "-1"
            mExpCode = "-1"
            mIBRNo = "-1"
            mClearDate = ""
            I = cntRow + 5

            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivCode & ")"

            PubDBCn.Execute(SqlStr)

            If GetAccountBalancingMethod(mAccountCode, True) = "S" Then
                If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", "N", mBookType, mBookSubType, VB6.Format(mVDate, "DD/MM/YYYY"), ADDMode, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivCode, pProcessKey, "N") = False Then GoTo ErrDetail
            Else
                If UpdateSuppPRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, mAmount, mRemarks, mDivCode) = False Then GoTo ErrDetail
            End If
        End If


        GenerateBankDetail = True
        Exit Function
ErrDetail:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GenerateBankDetail = False
        ''Resume
    End Function

    Public Function UpdateSuppPRDetail(ByRef pDBCn As ADODB.Connection, ByRef pMKey As String, ByRef pRowNo As Integer, ByRef pTRNDtlSubRow As Integer, ByRef pAccountCode As String, ByRef pBookCode As String, ByRef pVType As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pVNo As String, ByRef pVDate As String, ByRef pTrnDC As String, ByRef pTrnAmount As Double, ByRef pChequeNo As String, ByRef pChqDate As String, ByRef pCostCCode As String, ByRef pDeptCode As String, ByRef pEmpCode As String, ByRef pExpCode As String, ByRef pIBRNo As String, ByRef pClearDate As String, ByRef pLocked As String, ByRef pNarration As String, ByRef xAmount As Double, ByRef xRemarks As String, ByRef mDivisionCode As Double) As Boolean


        On Error GoTo ErrDetail
        Dim RsTempPRDetail As ADODB.Recordset
        Dim RsCntPRDetail As ADODB.Recordset
        Dim mCountBill As Integer

        Dim SqlStr As String = ""
        Dim pTRNType As String
        Dim pBillNo As String
        Dim pBillDate As String
        Dim pBillAmount As Double
        Dim pBillDC As String
        Dim pAmount As Double
        Dim pDC As String
        Dim pBillType As String
        Dim pSubRowNo As Integer
        Dim pRemarks As String
        Dim pDueDate As String
        Dim mRowCount As Integer
        'Dim mTDSAmount As Long
        Dim pSTTYPE As String
        Dim pSTFORMNAME As String
        Dim pSTFORMNO As String
        Dim pSTFORMDATE As String
        Dim pSTFORMAMT As Double
        Dim pSTDUEFORMNAME As String
        Dim pSTDUEFORMNO As String
        Dim pSTDUEFORMDATE As String
        Dim pSTDUEFORMAMT As Double
        Dim pISREGDNO As String
        Dim pSTFORMCODE As Integer
        Dim pSTDUEFORMCODE As Integer
        Dim pTaxableAmount As Double
        Dim pPONO As String
        Dim mDivCode As Double


        pSubRowNo = pRowNo

        pTRNType = "N" 'IIf(IsNull(RsTempPRDetail!TRNTYPE), "B", RsTempPRDetail!TRNTYPE)
        pBillType = "B"
        pBillNo = Trim(txtRefNo.Text)
        pBillDate = VB6.Format(txtRefDate.Text, "DD-MMM-YYYY")
        pBillAmount = pTrnAmount
        pTaxableAmount = pTrnAmount
        pPONO = ""
        mDivCode = mDivisionCode

        pBillDC = pTrnDC
        pDC = pTrnDC
        pAmount = pTrnAmount ''Round(IIf(IsNull(RsTempPRDetail!Amount), 0, RsTempPRDetail!Amount) * Val(txtJVTDSRate.Text) * 0.01, 0)


        pSTTYPE = "0" ' IIf(IsNull(RsTempPRDetail!STTYPE), "", RsTempPRDetail!STTYPE)
        pSTFORMNAME = "" 'IIf(IsNull(RsTempPRDetail!STFORMNAME), "", RsTempPRDetail!STFORMNAME)
        pSTFORMNO = "" 'IIf(IsNull(RsTempPRDetail!STFORMNO), "", RsTempPRDetail!STFORMNO)
        pSTFORMDATE = "" 'IIf(IsNull(RsTempPRDetail!STFORMDATE), "", RsTempPRDetail!STFORMDATE)
        pSTFORMAMT = 0 'IIf(IsNull(RsTempPRDetail!STFORMAMT), 0, RsTempPRDetail!STFORMAMT)
        pSTDUEFORMNAME = "" ' IIf(IsNull(RsTempPRDetail!STDUEFORMNAME), "", RsTempPRDetail!STDUEFORMNAME)
        pSTDUEFORMNO = "" '  IIf(IsNull(RsTempPRDetail!STDUEFORMNO), "", RsTempPRDetail!STDUEFORMNO)
        pSTDUEFORMDATE = "" 'IIf(IsNull(RsTempPRDetail!STDUEFORMDATE), "", RsTempPRDetail!STDUEFORMDATE)
        pSTDUEFORMAMT = 0 '   IIf(IsNull(RsTempPRDetail!STDUEFORMAMT), 0, RsTempPRDetail!STDUEFORMAMT)
        pISREGDNO = "N" 'IIf(IsNull(RsTempPRDetail!ISREGDNO), "", RsTempPRDetail!ISREGDNO)
        pSTFORMCODE = CInt("-1") 'IIf(IsNull(RsTempPRDetail!STFORMCODE), Null, RsTempPRDetail!STFORMCODE)
        pSTDUEFORMCODE = CInt("-1") ' IIf(IsNull(RsTempPRDetail!STDUEFORMCODE), Null, RsTempPRDetail!STDUEFORMCODE)


        pRemarks = xRemarks
        pDueDate = pVDate

        SqlStr = "INSERT INTO FIN_BILLDETAILS_TRN ( " & vbCrLf & " MKey, TRNDtlSubRowNo ,SubRowNo," & vbCrLf & " AccountCode, TrnType, BillNo, BillDate," & vbCrLf & " BillAmount,BillDc, Amount,Dc,REMARKS,DUEDATE, " & vbCrLf & " STTYPE, STFORMNAME, STFORMNO, " & vbCrLf & " STFORMDATE, STFORMAMT, STDUEFORMNAME, " & vbCrLf & " STDUEFORMNO, STDUEFORMDATE, STDUEFORMAMT, " & vbCrLf & " ISREGDNO, STFORMCODE, STDUEFORMCODE,TAXABLE_AMOUNT,PONO,DIV_CODE " & vbCrLf & " ) VALUES ( " & vbCrLf & " '" & pMKey & "', " & pTRNDtlSubRow & "," & pSubRowNo & ", " & vbCrLf & " '" & pAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(UCase(pTRNType)) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(pBillNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & pBillAmount & ", '" & pBillDC & "', " & pAmount & ", '" & pDC & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(pRemarks) & "',TO_DATE('" & VB6.Format(pDueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & pSTTYPE & "', '" & MainClass.AllowSingleQuote(pSTFORMNAME) & "', '" & MainClass.AllowSingleQuote(pSTFORMNO) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(pSTFORMDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(CStr(pSTFORMAMT)) & ", '" & MainClass.AllowSingleQuote(pSTDUEFORMNAME) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(pSTDUEFORMNO) & "', TO_DATE('" & VB6.Format(pSTDUEFORMDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(CStr(pSTDUEFORMAMT)) & ", " & vbCrLf & " '" & pISREGDNO & "', " & Val(CStr(pSTFORMCODE)) & ", " & Val(CStr(pSTDUEFORMCODE)) & ", " & Val(CStr(pTaxableAmount)) & ", '" & pPONO & "'," & mDivCode & " " & vbCrLf & " ) "

        pDBCn.Execute(SqlStr)

        If UpdateTRN(pDBCn, pMKey, pTRNDtlSubRow, pSubRowNo, pBookCode, pVType, pBookType, pBookSubType, pAccountCode, pVNo, pVDate, pBillNo, pBillDate, pAmount, pDC, pTRNType, pChequeNo, pChqDate, pCostCCode, pDeptCode, pEmpCode, pExpCode, pDueDate, pIBRNo, pBillType, pClearDate, pLocked, pNarration, pRemarks, VB6.Format(pVDate, "DD/MM/YYYY"), ADDMode, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivCode, "N") = False Then GoTo ErrDetail


        UpdateSuppPRDetail = True
        Exit Function
ErrDetail:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateSuppPRDetail = False
        'Resume
    End Function
    Private Function GenBankVno(ByRef pBankBookType As String) As String

        On Error GoTo ERR1
        Dim mVNo1 As String
        Dim SqlStr2 As String
        Dim SqlStr As String = ""
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mVType As String

        GenBankVno = ""
        mBookType = VB.Left(pBankBookType, 1)
        mBookSubType = VB.Right(pBankBookType, 1)
        mVType = Trim(txtVType.Text)

        If ADDMode = True Or txtBankVNo.Text = "" Then
            SqlStr = "SELECT MAX(VNOSeq) From FIN_VOUCHER_HDR WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND BookType='" & mBookType & "'" & vbCrLf _
                & " AND BookSubType='" & mBookSubType & "'" & vbCrLf _
                & " AND VTYPE='" & MainClass.AllowSingleQuote(mVType) & "'"

            If RsCompany.Fields("CBJVoucherSeq").Value = "D" Then
                SqlStr = SqlStr & vbCrLf & " AND VDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "M" Then
                SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(VDATE,'MMMYYYY')=TO_CHAR('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','MMMYYYY')"
            ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "Y" Then

            End If

            GenBankVno = VB6.Format(MainClass.AutoGenVNo(SqlStr, PubDBCn), "00000")
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description)
        'Resume
    End Function

    Private Function AutoGenSeqBillNo(ByRef mFieldName As String) As String

        On Error GoTo AutoGenSeqBillNoErr
        Dim RsPurchMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqBillNo As Double
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMaxNo As Double

        SqlStr = ""

        SqlStr = ""
        SqlStr = "SELECT Max(" & mFieldName & ")  FROM FIN_LCDISC_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPurchMainGen
            If .EOF = False Then
                mMAxNo = IIf(IsDBNull(.Fields(0).Value), -1, .Fields(0).Value)
                If mMAxNo = -1 Then
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


    Private Function UpdateDetail1(ByRef pVNo As String, ByRef pSuppCustCode As String, ByRef pDivCode As Double) As Boolean
        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer

        Dim mAmount As Double
        Dim mPONo As String
        Dim mHSNCode As String
        Dim mParticulars As String
        Dim mPODate As String
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double

        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mPOS As String
        Dim mState As String
        Dim mAccountCode As String
        Dim mCreditApp As String

        PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BOOKCODE=" & ConLDBookCode & " AND BookType='" & UCase(mBookType) & "'")
        PubDBCn.Execute("Delete From FIN_LCDISC_DET Where Mkey='" & LblMKey.Text & "'")

        mPOS = ""

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColHSN
                mHSNCode = Trim(.Text)

                .Col = ColCreditApplicable
                mCreditApp = Trim(.Text)

                .Col = ColParticulars
                mParticulars = Trim(.Text)

                mAccountCode = ""
                If MainClass.ValidateWithMasterTable(mParticulars, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountCode = MasterNo
                End If

                .Col = ColAmount
                mAmount = Val(.Text)

                .Col = ColCGSTPer
                mCGSTPer = Val(.Text)

                .Col = ColSGSTPer
                mSGSTPer = Val(.Text)

                .Col = ColIGSTPer
                mIGSTPer = Val(.Text)

                .Col = ColCGSTAmount
                mCGSTAmount = Val(.Text)

                .Col = ColSGSTAmount
                mSGSTAmount = Val(.Text)

                .Col = ColIGSTAmount
                mIGSTAmount = Val(.Text)


                SqlStr = ""

                If mAccountCode <> "" And mAmount > 0 Then
                    SqlStr = " INSERT INTO FIN_LCDISC_DET ( " & vbCrLf & " MKEY , COMPANY_CODE, SUBROWNO, " & vbCrLf & " ACCOUNTCODE, HSN_CODE ,  AMOUNT, " & vbCrLf & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT " & vbCrLf & " ) "

                    SqlStr = SqlStr & vbCrLf & " VALUES ('" & LblMKey.Text & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & I & ", " & vbCrLf & " '" & mAccountCode & "', '" & mHSNCode & "',  " & mAmount & ", " & vbCrLf & " " & mCGSTPer & "," & mSGSTPer & "," & mIGSTPer & "," & vbCrLf & " " & mCGSTAmount & "," & mSGSTAmount & "," & mIGSTAmount & " " & vbCrLf & " )"

                    PubDBCn.Execute(SqlStr)

                    If (mCGSTAmount + mSGSTAmount + mIGSTAmount > 0) And mCreditApp = "Y" Then
                        If UpdateGSTTRN(PubDBCn, (LblMKey.Text), CStr(ConLDBookCode), mBookType, "S", pVNo, VB6.Format(TxtVDate.Text, "DD-MMM-YYYY"), pVNo, VB6.Format(TxtVDate.Text, "DD-MMM-YYYY"), "", "", pSuppCustCode, "-1", "Y", pSuppCustCode, I, "-1", 0, "-", 0, mAmount, mAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, pDivCode, mHSNCode, Trim(mParticulars), mPOS, "N", VB.Right(lblBookType.Text, 1), "S", "N", "C", VB6.Format(TxtVDate.Text, "DD-MMM-YYYY"), "N") = False Then GoTo UpdateDetail1
                    End If

                End If
            Next
        End With
        UpdateDetail1 = True

        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim cntRow As Integer
        Dim xPoNo As String
        Dim mIsItemCapital As String
        Dim xSuppCode As String
        Dim xHSNCode As String
        Dim mLocal As String
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mGSTApp As String
        Dim mGSTRegd As String
        Dim mPartyGSTNo As String
        Dim mLCAmount As Double
        Dim mLCDiscountedAmt As Double

        FieldsVarification = True


        If ValidateAccountLocking(PubDBCn, TxtVDate.Text, (txtSupplier.Text), mSupplierCode) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsPurchMain.EOF = True Then Exit Function

        If MainClass.GetUserCanModify((TxtVDate.Text)) = False Then
            MsgBox("You Have Not Rights to Add or Modify back Voucher", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If PubUserID <> "G0416" Then
            If chkGSTClaim.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("GST Claim is Taken, So that cann't be Save.")
                FieldsVarification = False
                Exit Function
            End If

            '        If CheckVoucherDateLock(txtVDate.Text, pMaxDate) = True Then
            '             MsgInformation "Working Company has been locked till date : " & pMaxDate & vbCrLf _
            ''                        & "So Unable to Save or Delete. Contact your system administrator."
            '            FieldsVarification = False
            '            Exit Function
            '        End If
        End If

        If MODIFYMode = True And txtVNo.Text = "" Then
            MsgInformation("Voucher No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If TxtVDate.Text = "" Then
            MsgBox("VDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            TxtVDate.Focus()
            Exit Function
        ElseIf FYChk((TxtVDate.Text)) = False Then
            FieldsVarification = False
            If TxtVDate.Enabled = True Then TxtVDate.Focus()
            Exit Function
        End If

        If Trim(txtSupplier.Text) = "" Then
            MsgBox("Supplier Cannot Be Blank", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If


        If Trim(cboDivision.Text) = "" Then
            MsgBox("Please select the division", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If


        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Supplier Does Not Exist In Master", MsgBoxStyle.Information)
            'txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        Else
            xSuppCode = MasterNo
        End If

        '    mWithInState = "Y"
        '    If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mWithInState = IIf(IsNull(MasterNo), "Y", MasterNo)
        '    End If

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mGSTRegd = IIf(IsDbNull(MasterNo), "N", MasterNo)
        End If
        '
        '    If cboGSTStatus.ListIndex = -1 Then
        '        MsgBox "Please select GST Status", vbInformation
        '        If cboGSTStatus.Enabled = True Then cboGSTStatus.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        '
        '    If mGSTRegd = "Y" And Left(cboGSTStatus.Text, 1) <> "G" Then
        '        MsgBox "Supplier is registered, please select the GST Refund.", vbInformation
        '       ' txtSupplier.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        '
        '    If mGSTRegd = "N" And Left(cboGSTStatus.Text, 1) <> "R" Then
        '        MsgBox "Supplier is not registered, please select the Reverse Charge.", vbInformation
        '       ' txtSupplier.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        '
        '    If mGSTRegd = "E" And Left(cboGSTStatus.Text, 1) <> "E" Then
        '        MsgBox "GST Exempted Supplier, please select the GST Exempted.", vbInformation
        '       ' txtSupplier.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        '    If Left(cboGSTStatus.Text, 1) <> "E" And (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) = 0 Then
        '        MsgBox "GST Amount Cann't be Zero.", vbInformation
        '        FieldsVarification = False
        '        Exit Function
        '    End If


        '    If Left(cboGSTStatus.Text, 1) = "E" And (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) <> 0 Then
        '        MsgBox "You have not Check in GST. You Want to Continue ...", vbInformation
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        mLocal = "N"
        If Trim(txtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(Trim(txtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLocal = IIf(IsDbNull(MasterNo), "N", MasterNo)
            End If
        End If

        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If

        If Trim(txtLCNo.Text) = "" Then
            MsgBox("LC No cann't be Blank. Cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtLCDate.Text) = "" Then
            MsgBox("LC date cann't be Blank. Cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If IsDate(txtLCDate.Text) = False Then
            MsgBox("Invalid LC date. Cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtLCDate.Enabled = True Then txtLCDate.Focus()
            Exit Function
        End If

        If Trim(txtLCVNo.Text) = "" Then
            MsgBox("LC Ref No cann't be Blank. Cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtLCVDate.Text) = "" Then
            MsgBox("LC Ref date cann't be Blank. Cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If IsDate(txtRefDate.Text) = False Then
            MsgBox("Invalid Ref date. Cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtRefDate.Enabled = True Then txtRefDate.Focus()
            Exit Function
        End If

        If Val(txtLCAmount.Text) < 0 Then
            MsgBox("LC Amount cann't be Blank. Cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtAdvBankName.Text) = "" Then
            MsgBox("Adv. Bank cann't be Blank. Cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(Trim(txtAdvBankName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Invaild Ad. Bank Name. Cann't be Save.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        '    If Trim(txtAdvBankName.Text) = Trim(txtBankName.Text) Then
        '        MsgBox "Adv. Bank cann't same Bank Name. Cann't be Save.", vbInformation
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        mLCAmount = GetLCOpeningAmount((lblLCMkey.Text))
        mLCDiscountedAmt = Val(txtDiscAmount.Text) 'GetLCDiscountingAmount(lblLCMkey.text) '29/05/2019

        If Val(CStr(mLCDiscountedAmt)) > Val(CStr(mLCAmount)) Then
            MsgBox("Discounting Amount cann't be Greater than LC Amount. Cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If Val(lblNetAmount.Text) <> Val(txtDiscAmount.Text) Then
            MsgBox("Dr. & Cr. Amount Not Match. Cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If CDate(txtLCVDate.Text) > CDate(TxtVDate.Text) Then
            MsgBox("LC Ref date cann't be greater than LC Discount Date. Cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColHSN
                xHSNCode = Trim(.Text)


                If xHSNCode <> "" Then
                    mGSTApp = ""
                    mCGSTPer = 0
                    mSGSTPer = 0
                    mIGSTPer = 0


                    If MainClass.ValidateWithMasterTable(Trim(xHSNCode), "HSN_CODE", "GST_APP", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                        mGSTApp = Trim(MasterNo)
                    Else
                        MsgBox("SAC Code Does Not Exist In Master", MsgBoxStyle.Information)
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColHSN)
                        FieldsVarification = False
                        Exit Function
                    End If

                    If GetSACDetails(xHSNCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, mPartyGSTNo, "G") = False Then GoTo err_Renamed

                    SprdMain.Col = ColCreditApplicable
                    SprdMain.Text = mGSTApp

                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = VB6.Format(mCGSTPer, "0.00")

                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = VB6.Format(mSGSTPer, "0.00")

                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = VB6.Format(mIGSTPer, "0.00")
                End If

            Next
        End With

        If SprdMain.MaxRows > 1 Then
            '        If MainClass.ValidDataInGrid(SprdMain, ColHSN, "S", "HSN Code Is Blank.") = False Then FieldsVarification = False: Exit Function
            If MainClass.ValidDataInGrid(SprdMain, ColParticulars, "S", "Particulars Is Blank.") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(SprdMain, ColAmount, "N", "Please Check Amount.") = False Then FieldsVarification = False : Exit Function
        End If


        CalcTots()
        Exit Function
err_Renamed:
        FieldsVarification = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmLCDiscEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mBookType = Trim(lblBookType.Text)

        SqlStr = ""
        SqlStr = "Select * from FIN_LCDISC_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from FIN_LCDISC_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        mSupplierCode = CStr(-1)



        txtVNoPrefix.Text = mBookType & VB6.Format(IIf(IsDbNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00")

        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""
        SqlStr = ""

        MainClass.ClearGrid(SprdView)

        SqlStr = "SELECT VNOPREFIX, TO_CHAR(VNOSEQ), " & vbCrLf & " VNO,VDATE, REF_NO, REF_DATE, LC_NO, LC_DATE, "

        SqlStr = SqlStr & vbCrLf & " CMST.SUPP_CUST_NAME AS SUPPLIER, " & vbCrLf & " ITEMVALUE, " & vbCrLf & " TOTALGSTVALUE, TOTCGST_AMOUNT, TOTSGST_AMOUNT, TOTIGST_AMOUNT, NETVALUE "


        SqlStr = SqlStr & vbCrLf & " FROM " & vbCrLf & " FIN_LCDISC_HDR IH, FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " And IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.BOOKTYPE='" & mBookType & "'"

        '    SqlStr = SqlStr & vbCrLf & " AND VDATE >= '" & vb6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "'"
        SqlStr = SqlStr & vbCrLf & " Order by IH.VDATE, IH.VNO"


        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume
    End Sub
    Private Sub FormatSprdView()

        'Dim cntCol As Integer

        With SprdView
            .Row = -1

            .set_RowHeight(0, 600)

            .set_ColWidth(0, 600)


            .set_ColWidth(1, 1200)
            .ColHidden = False
            .set_ColWidth(2, 1200)
            .set_ColWidth(3, 1200)
            .set_ColWidth(4, 1200)
            .set_ColWidth(5, 1200)
            .set_ColWidth(6, 1200)
            .set_ColWidth(7, 1200)
            .set_ColWidth(8, 1300)
            .set_ColWidth(9, 1200)
            '
            '        For cntCol = 17 To 20
            '            .Col = cntCol
            '            .TypeHAlign = TypeHAlignRight
            '        Next
            .ColsFrozen = 3
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1

        pShowCalc = False
        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColHSN
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("HSN_CODE", "GEN_HSN_MST", PubDBCn) ''
            .set_ColWidth(ColHSN, 8)


            .Col = ColCreditApplicable
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("GST_APP", "GEN_HSN_MST", PubDBCn) ''
            .set_ColWidth(ColCreditApplicable, 6)

            .Col = ColParticulars
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn) ''
            .set_ColWidth(ColParticulars, 22)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColAmount, 9)

            .Col = ColCGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .set_ColWidth(ColCGSTPer, 4.5)

            .Col = ColCGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColCGSTAmount, 9)

            .Col = ColSGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .set_ColWidth(ColSGSTPer, 4.5)

            .Col = ColSGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColSGSTAmount, 9)

            .Col = ColIGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .set_ColWidth(ColIGSTPer, 4.5)

            .Col = ColIGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColIGSTAmount, 9)


        End With


        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCreditApplicable, ColCreditApplicable)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCGSTPer, ColIGSTAmount)
        '    MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColPONo, ColPONo

        pShowCalc = True
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsPurchDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsPurchMain

            txtVNo.Maxlength = .Fields("Vno").DefinedSize ''
            txtVNoPrefix.Maxlength = .Fields("VNoPrefix").DefinedSize ''
            TxtVDate.Maxlength = 10
            txtBankName.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtSupplier.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtAdvBankName.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

            txtBankVNo.Maxlength = MainClass.SetMaxLength("VNO", "FIN_VOUCHER_HDR", PubDBCn)
            txtBankVDate.Maxlength = 10


            txtRemarks.Maxlength = .Fields("Remarks").DefinedSize ''

            txtChqNo.Maxlength = .Fields("CHEQUENO").DefinedSize
            txtChqDate.Maxlength = 10
            txtLCNo.Maxlength = .Fields("LC_NO").Precision
            txtLCDate.Maxlength = 10

            txtRefNo.Maxlength = .Fields("REF_NO").Precision
            txtRefDate.Maxlength = 10

        End With

        Exit Sub
ERR1:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mSqlStr As String
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mServiceCode As Double
        Dim mBankMKEY As String
        Dim mPaymentType As String
        Dim mBankCode As String
        Dim mOPBal As Double
        Dim mGSTStatus As String
        Dim mAdvBankCode As String

        Clear1()

        With RsPurchMain
            If Not .EOF Then
                LblMKey.Text = .Fields("MKey").Value
                mBankCode = IIf(IsDbNull(.Fields("BANK_CODE").Value), "", .Fields("BANK_CODE").Value)

                If MainClass.ValidateWithMasterTable(mBankCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtBankName.Text = MasterNo
                End If

                mAdvBankCode = IIf(IsDbNull(.Fields("ADV_BANK_CODE").Value), "", .Fields("ADV_BANK_CODE").Value)

                If MainClass.ValidateWithMasterTable(mAdvBankCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtAdvBankName.Text = MasterNo
                End If

                txtDiscAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("DISC_AMOUNT").Value), "", .Fields("DISC_AMOUNT").Value), "0.00")

                txtVNoPrefix.Text = IIf(IsDbNull(.Fields("VNOPREFIX").Value), "", .Fields("VNOPREFIX").Value)
                txtVNo.Text = VB6.Format(IIf(IsDbNull(.Fields("VNOSEQ").Value), "", .Fields("VNOSEQ").Value), "00000")
                TxtVDate.Text = VB6.Format(IIf(IsDbNull(.Fields("VDate").Value), "", .Fields("VDate").Value), "DD/MM/YYYY")

                txtModvatNo.Text = IIf(IsDbNull(.Fields("GST_CLAIM_NO").Value), "", .Fields("GST_CLAIM_NO").Value)
                txtModvatDate.Text = VB6.Format(IIf(IsDbNull(.Fields("GST_CLAIM_DATE").Value), "", .Fields("GST_CLAIM_DATE").Value), "DD/MM/YYYY")
                chkGSTClaim.CheckState = IIf(.Fields("GST_CLAIM").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


                lblLCMkey.Text = IIf(IsDbNull(.Fields("LCMKey").Value), "", .Fields("LCMKey").Value)
                txtLCVNo.Text = IIf(IsDbNull(.Fields("LCVNO").Value), "", .Fields("LCVNO").Value)
                txtLCVDate.Text = VB6.Format(IIf(IsDbNull(.Fields("LCVDATE").Value), "", .Fields("LCVDATE").Value), "DD/MM/YYYY")

                txtLCVNo.Enabled = False
                txtLCVDate.Enabled = False
                CmdSearchLC.Enabled = False

                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSupplier.Text = MasterNo
                End If


                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If

                txtRemarks.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                txtLCNo.Text = IIf(IsDbNull(.Fields("LC_NO").Value), "", .Fields("LC_NO").Value)
                txtLCDate.Text = VB6.Format(IIf(IsDbNull(.Fields("LC_DATE").Value), "", .Fields("LC_DATE").Value), "DD/MM/YYYY")
                txtLCAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("LC_AMOUNT").Value), "", .Fields("LC_AMOUNT").Value), "0.00")

                txtRefNo.Text = IIf(IsDbNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)
                txtRefDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")

                lblTotItemValue.Text = VB6.Format(IIf(IsDbNull(.Fields("ITEMVALUE").Value), 0, .Fields("ITEMVALUE").Value), "0.00")
                lblNetAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("NETVALUE").Value), 0, .Fields("NETVALUE").Value), "0.00")
                lblTotCGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTCGST_AMOUNT").Value), "", .Fields("TOTCGST_AMOUNT").Value), "0.00")
                lblTotSGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTSGST_AMOUNT").Value), "", .Fields("TOTSGST_AMOUNT").Value), "0.00")
                lblTotIGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTIGST_AMOUNT").Value), "", .Fields("TOTIGST_AMOUNT").Value), "0.00")

                lblCGSTRefundAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTCGST_CREDITAMT").Value), "", .Fields("TOTCGST_CREDITAMT").Value), "0.00")
                lblSGSTRefundAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTSGST_CREDITAMT").Value), "", .Fields("TOTSGST_CREDITAMT").Value), "0.00")
                lblIGSTRefundAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTIGST_CREDITAMT").Value), "", .Fields("TOTIGST_CREDITAMT").Value), "0.00")



                txtChqNo.Text = IIf(IsDbNull(.Fields("CHEQUENO").Value), "", .Fields("CHEQUENO").Value)
                txtChqDate.Text = VB6.Format(IIf(IsDbNull(.Fields("CHQDATE").Value), "", .Fields("CHQDATE").Value), "DD/MM/YYYY")


                mBankMKEY = IIf(IsDbNull(.Fields("BANKVOUCHERMKEY").Value), "", .Fields("BANKVOUCHERMKEY").Value)
                lblBankMKey.Text = mBankMKEY

                mSqlStr = "SELECT IH.VNO, VTYPE,VNOSEQ,VNOSUFFIX, IH.VDATE, CMST.SUPP_CUST_NAME  " & vbCrLf & " FROM FIN_VOUCHER_HDR IH, FIN_SUPP_CUST_MST CMST" & vbCrLf & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY='" & mBankMKEY & "'" & vbCrLf & " AND IH.COMPANY_CODE = CMST.COMPANY_CODE " '' & vbCrLf |                    & " AND IH.BOOKCODE = CMST.SUPP_CUST_CODE "

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)

                If RsMisc.EOF = False Then
                    txtVType.Text = IIf(IsDbNull(RsMisc.Fields("VTYPE").Value), "", RsMisc.Fields("VTYPE").Value)
                    txtBankVNo.Text = VB6.Format(IIf(IsDbNull(RsMisc.Fields("VNOSEQ").Value), 0, RsMisc.Fields("VNOSEQ").Value), "00000")
                    txtBankVNoSuffix.Text = IIf(IsDbNull(RsMisc.Fields("VNOSUFFIX").Value), "", RsMisc.Fields("VNOSUFFIX").Value)
                    txtBankVDate.Text = VB6.Format(IIf(IsDbNull(RsMisc.Fields("VDATE").Value), "", RsMisc.Fields("VDATE").Value), "DD/MM/YYYY")

                End If

                mOPBal = GetOpeningBal(mBankCode, (TxtVDate.Text))
                txtBookBalAmt.Text = VB6.Format(System.Math.Abs(mOPBal), "0.00") & IIf(mOPBal >= 0, "Dr", "Cr")

                cboDivision.Enabled = False

                Call ShowDetail1((LblMKey.Text))
                Call CopyToTempPRDetail()
            End If
        End With
        txtVNo.Enabled = True

        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsPurchMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        FormatSprdMain(-1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub ShowDetail1(ByRef mMkey As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mHSNCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mHSNDesc As String
        Dim mAccountCode As String
        Dim mAccountDesc As String = ""
        Dim mGSTApp As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM FIN_LCDISC_DET " & vbCrLf & " Where Mkey='" & mMkey & "'" & vbCrLf & " Order By SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPurchDetail
            If .EOF = True Then GoTo NextStep
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColHSN
                mHSNCode = IIf(IsDbNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value)
                SprdMain.Text = mHSNCode

                SprdMain.Col = ColCreditApplicable
                mGSTApp = "N"
                If MainClass.ValidateWithMasterTable(Trim(mHSNCode), "HSN_CODE", "GST_APP", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                    mGSTApp = Trim(MasterNo)
                End If

                SprdMain.Text = mGSTApp


                mAccountCode = IIf(IsDbNull(.Fields("ACCOUNTCODE").Value), "", .Fields("ACCOUNTCODE").Value)

                SprdMain.Col = ColParticulars
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountDesc = MasterNo
                End If

                SprdMain.Text = mAccountDesc

                '            SprdMain.Col = ColParticulars
                '            SprdMain.Text = IIf(IsNull(.Fields("PARTICULARS").Value), "", .Fields("PARTICULARS").Value)

                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("AMOUNT").Value), 0, .Fields("AMOUNT").Value)))

                '            SprdMain.Col = ColPONo
                '            SprdMain.Text = CStr(IIf(IsNull(.Fields("PO_REF_NO").Value), "", .Fields("PO_REF_NO").Value))

                SprdMain.Col = ColCGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("CGST_PER").Value), 0, .Fields("CGST_PER").Value)))

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("SGST_PER").Value), 0, .Fields("SGST_PER").Value)))

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)))

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
NextStep:
        CalcTots()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        AdoDCMain.Refresh
            FormatSprdView()
            SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsPurchMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1
        Dim mItemAmount As Double
        Dim j As Integer
        Dim I As Integer
        Dim mHSNCode As String
        Dim xStr As String
        Dim mTotItemAmount As Double
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim pTCSPer As Double

        Dim mGSTableAmount As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim pTotCGSTAmount As Double
        Dim pTotSGSTAmount As Double
        Dim pTotIGSTAmount As Double

        Dim pCGSTRefundAmount As Double
        Dim pSGSTRefundAmount As Double
        Dim pIGSTRefundAmount As Double

        Dim mCreditApp As String

        mItemAmount = 0
        mTotItemAmount = 0

        pTotCGSTAmount = 0
        pTotSGSTAmount = 0
        pTotIGSTAmount = 0

        pCGSTRefundAmount = 0
        pSGSTRefundAmount = 0
        pIGSTRefundAmount = 0

        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I

                .Col = ColHSN
                '            If .Text = "" Then GoTo DontCalc
                mHSNCode = .Text

                .Col = ColCreditApplicable
                mCreditApp = Trim(.Text)

                .Col = ColAmount
                mItemAmount = Val(.Text)

                .Col = ColCGSTPer
                pCGSTPer = Val(.Text)

                .Col = ColSGSTPer
                pSGSTPer = Val(.Text)

                .Col = ColIGSTPer
                pIGSTPer = Val(.Text)

                mCGSTAmount = CDbl(VB6.Format(mItemAmount * pCGSTPer * 0.01, "0.00"))
                mSGSTAmount = CDbl(VB6.Format(mItemAmount * pSGSTPer * 0.01, "0.00"))
                mIGSTAmount = CDbl(VB6.Format(mItemAmount * pIGSTPer * 0.01, "0.00"))

                .Col = ColCGSTAmount
                .Text = VB6.Format(mCGSTAmount, "0.00")

                .Col = ColSGSTAmount
                .Text = VB6.Format(mSGSTAmount, "0.00")

                .Col = ColIGSTAmount
                .Text = VB6.Format(mIGSTAmount, "0.00")

                mTotItemAmount = mTotItemAmount + mItemAmount

                pTotCGSTAmount = pTotCGSTAmount + mCGSTAmount
                pTotSGSTAmount = pTotSGSTAmount + mSGSTAmount
                pTotIGSTAmount = pTotIGSTAmount + mIGSTAmount

                If mCreditApp = "Y" Then
                    pCGSTRefundAmount = pCGSTRefundAmount + mCGSTAmount
                    pSGSTRefundAmount = pSGSTRefundAmount + mSGSTAmount
                    pIGSTRefundAmount = pIGSTRefundAmount + mIGSTAmount
                End If

DontCalc:
            Next I
        End With



        lblTotItemValue.Text = VB6.Format(mTotItemAmount, "#0.00")
        lblTotCGSTAmount.Text = VB6.Format(pTotCGSTAmount, "#0.00")
        lblTotSGSTAmount.Text = VB6.Format(pTotSGSTAmount, "#0.00")
        lblTotIGSTAmount.Text = VB6.Format(pTotIGSTAmount, "#0.00")

        lblCGSTRefundAmount.Text = VB6.Format(pCGSTRefundAmount, "#0.00")
        lblSGSTRefundAmount.Text = VB6.Format(pSGSTRefundAmount, "#0.00")
        lblIGSTRefundAmount.Text = VB6.Format(pIGSTRefundAmount, "#0.00")

        '    txtDiscAmount.Text = Val(txtLCAmount.Text) - Val(mTotItemAmount + pTotCGSTAmount + pTotSGSTAmount + pTotIGSTAmount)

        lblNetAmount.Text = VB6.Format(mTotItemAmount + pTotCGSTAmount + pTotSGSTAmount + pTotIGSTAmount, "#0.00")


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub Clear1()

        Dim SqlStr As String = ""

        pShowCalc = False
        LblMKey.Text = ""
        lblBankMKey.Text = ""
        mSupplierCode = CStr(-1)
        txtSupplier.Text = ""

        txtVNo.Text = ""
        txtVNoPrefix.Text = mBookType & VB6.Format(IIf(IsDbNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00")


        TxtVDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")


        txtVType.Text = "JV"
        txtBankVNo.Text = ""
        txtBankVNoSuffix.Text = ""
        txtBankVDate.Text = ""
        txtBankName.Text = ""
        txtBookBalAmt.Text = ""
        txtChqNo.Text = ""
        txtChqDate.Text = ""
        txtLCNo.Text = ""
        txtLCDate.Text = ""
        txtLCAmount.Text = ""
        txtAdvBankName.Text = ""

        txtLCNo.Enabled = False
        txtLCDate.Enabled = False
        txtLCAmount.Enabled = False
        txtSupplier.Enabled = False

        txtModvatNo.Text = ""
        txtModvatDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtModvatNo.Enabled = False
        txtModvatDate.Enabled = False
        chkGSTClaim.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkGSTClaim.Enabled = False

        txtRefNo.Text = ""
        txtRefDate.Text = ""
        txtRefNo.Enabled = True
        txtRefDate.Enabled = True

        txtVType.Enabled = False
        txtBankVNo.Enabled = False
        txtBankVNoSuffix.Enabled = False
        txtBankVDate.Enabled = False

        txtBookBalAmt.Enabled = False
        txtChqNo.Enabled = True
        txtChqDate.Enabled = True


        txtBankName.Text = ""
        txtBankName.Enabled = False

        txtRemarks.Text = ""

        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        cboDivision.Enabled = False

        txtDiscAmount.Text = "0.00"
        txtDiscAmount.Enabled = True

        txtLCVNo.Text = ""
        txtLCVDate.Text = ""
        lblLCMkey.Text = ""

        txtLCVNo.Enabled = True
        txtLCVDate.Enabled = True
        CmdSearchLC.Enabled = True

        lblTotItemValue.Text = VB6.Format(0, "#0.00")

        lblTotCGSTAmount.Text = "0.00"
        lblTotSGSTAmount.Text = "0.00"
        lblTotIGSTAmount.Text = "0.00"

        lblCGSTRefundAmount.Text = "0.00"
        lblSGSTRefundAmount.Text = "0.00"
        lblIGSTRefundAmount.Text = "0.00"

        lblNetAmount.Text = VB6.Format(0, "#0.00")
        pProcessKey = MainClass.AutoGenRowNo("TEMP_DSP_DESPATCH", "RowNo", PubDBCn)

        SqlStr = "Delete from FIN_TEMPBILL_TRN  Where UserID='" & PubUserID & "' AND TEMPMKEY=" & pProcessKey & ""
        PubDBCn.Execute(SqlStr)

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsPurchMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        pShowCalc = True
    End Sub

    Private Sub FrmLCDiscEntry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmLCDiscEntry_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub FrmLCDiscEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim x As Boolean
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        mBookType = Trim(lblBookType.Text)

        txtVNoPrefix.Text = mBookType & VB6.Format(IIf(IsDbNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00")
        SprdMain.Enabled = True


        txtVNo.Enabled = True


        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7755) '8000
        'Me.Width = VB6.TwipsToPixelsX(11355) '11900

        AdoDCMain.Visible = False

        txtSupplier.Enabled = True
        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1



        ' Control displays text tips aligned to pointer with focus
        SprdMain.TextTip = FPSpreadADO.TextTipConstants.TextTipFloatingFocusOnly
        ' Control displays text tips after 250 milliseconds
        SprdMain.TextTipDelay = 250
        ' Text tip displays custom font and colors
        ' Background is yellow, RGB(255, 255, 0)
        ' Foreground is dark blue, RGB(0, 0, 128)
        x = SprdMain.SetTextTipAppearance("Arial", CShort("10"), False, False, &HFFFF, &H800000)

        Call FrmLCDiscEntry_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColHSN Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColHSN, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        '    With SprdMain
        '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False
        '    End With
    End Sub


    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.DoubleClick
        On Error GoTo ErrPart

        If MainClass.SearchGridMaster((txtSupplier.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C') AND STATUS='O'") = True Then
            txtSupplier.Text = AcName
            txtSupplier.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtSupplier_DoubleClick(txtSupplier, New System.EventArgs())
    End Sub

    Private Sub txtsupplier_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSupplier.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtSupplier.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            Cancel = True
            ErrorMsg("Please Enter the Valid Supplier / Customer.", "", MsgBoxStyle.Critical)
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub ReportonAdvanceReceipt(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mRptFileName As String



        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""

        mSubTitle = ""
        Call MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""

        Call SelectQry(SqlStr)


        mTitle = "LC Open"
        mSubTitle = ""
        mRptFileName = "LCOpen.rpt" ''mRptFileName = "PO.rpt"


        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, "Y")

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQry(ByRef mSqlStr As String) As String
        On Error GoTo ErrPart
        Dim mSuppCode As String
        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*, CMST.*, BANKMST.* "

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM FIN_LCDISC_HDR IH, FIN_LCDISC_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST BANKMST"

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=BANKMST.COMPANY_CODE" & vbCrLf & " AND IH.BANK_CODE=BANKMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        mSqlStr = mSqlStr & vbCrLf & " AND IH.MKEY=" & Val(LblMKey.Text) & ""

        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY IH.MKEY, ID.SUBROWNO"


        SelectQry = mSqlStr
        Exit Function
        SelectQry = ""
ErrPart:

    End Function
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PO " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY AUTO_KEY_PO,AMEND_NO"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef pIsPO As String)

        On Error GoTo ErrPart
        Dim RsTempShip As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mShipToSameParty As String
        Dim mShipToCode As String

        Dim mShipToName As String = ""
        Dim mShipToAddress As String = ""
        Dim mShipToCity As String = ""
        Dim mShipToGSTN As String = ""
        Dim mCompanyDetail As String = ""
        Dim mCompanyeMail As String = ""
        Dim mCompanyWebSite As String = ""
        Dim mShipToState As String = ""
        Dim mShipToStateCode As String = ""
        Dim mStateName As String = ""
        Dim mStateCode As String = ""
        Dim mWithInState As String = ""
        Dim mWithInCountry As String = ""
        Dim mPlaceofSupply As String = ""
        Dim mExpHeading As String = ""

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        mStateName = ""
        mStateCode = ""

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mStateName = MasterNo
            mStateCode = GetStateCode(mStateName)
        End If

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mWithInState = MasterNo
        End If

        If mWithInState = "N" Then
            If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithInCountry = MasterNo
            End If
        End If

        mPlaceofSupply = IIf(mWithInState = "Y", "INTRA STATE", IIf(mWithInCountry = "Y", "INTER STATE", "EXPORT"))



        '    MainClass.AssignCRptFormulas Report1, "CompanyCity=""" & IIf(IsNull(RsCompany!COMPANY_CITY), "", RsCompany!COMPANY_CITY) & """"
        MainClass.AssignCRptFormulas(Report1, "CompanyGSTIN=""" & IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "COMPANYCINNo=""" & IIf(IsDbNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & """")

        '    mCompanyeMail = IIf(IsNull(RsCompany!COMPANY_MAILID), "", "e-mail : " & RsCompany!COMPANY_MAILID)
        '    mCompanyWebSite = IIf(IsNull(RsCompany!WEBSITE), "", "WebSite : " & RsCompany!WEBSITE)
        '    mCompanyDetail = mCompanyeMail & ", " & mCompanyWebSite
        '    MainClass.AssignCRptFormulas Report1, "COMPANYDETAIL=""" & mCompanyDetail & """"

        MainClass.AssignCRptFormulas(Report1, "mShipToName=""" & mShipToName & """")
        MainClass.AssignCRptFormulas(Report1, "mShipToAddress=""" & mShipToAddress & """")
        MainClass.AssignCRptFormulas(Report1, "mShipToCity=""" & mShipToCity & """")
        MainClass.AssignCRptFormulas(Report1, "mShipToGSTN=""" & mShipToGSTN & """")

        MainClass.AssignCRptFormulas(Report1, "mShipToState=""" & mShipToState & """")
        MainClass.AssignCRptFormulas(Report1, "mShipToStateCode=""" & mShipToStateCode & """")

        '    MainClass.AssignCRptFormulas Report1, "mStateName=""" & mStateName & """"
        MainClass.AssignCRptFormulas(Report1, "mStateCode=""" & mStateCode & """")
        MainClass.AssignCRptFormulas(Report1, "mPlaceofSupply=""" & mPlaceofSupply & """")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.ReportFileName = ""
        Report1.Reset()

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub


    Private Sub txtVType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
End Class
