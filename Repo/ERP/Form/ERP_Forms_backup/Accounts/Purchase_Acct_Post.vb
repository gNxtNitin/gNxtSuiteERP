Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPurchase_Acct_Post
    Inherits System.Windows.Forms.Form
    Dim RsPurMain As ADODB.Recordset ''ADODB.Recordset
    Dim RsPurDetail As ADODB.Recordset ''ADODB.Recordset
    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String
    Dim pRound As Double
    Private Const ConRowHeight As Short = 14
    Dim pShowCalc As Boolean
    Dim pmyMenu As String
    Private Const ColWoDesc As Short = 1
    Private Const ColItemCode As Short = 2
    Private Const ColItemName As Short = 3
    Private Const ColAcctPostName As Short = 4
    Private Const ColAcctPostNameNew As Short = 5
    Dim mAmendStatus As Boolean
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtPONO_Validating(txtPONO, New System.ComponentModel.CancelEventArgs(False))
            CmdSave.Enabled = False
        Else
            MsgInformation("Record not saved")
            GoTo ErrorHandler
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim Sqlstr As String
        Dim mPONo As Double
        Dim mPurType As String
        Dim mOrderType As String
        Dim mStatus As String
        Dim mActivate As String
        Dim mAmendNo As Integer
        Dim mRecdAcct As String
        Dim mModvatable As String
        Dim mSTRefundable As String
        Dim mCapital As String
        Dim mServiceCode As Double
        Dim mOwnerCode As String
        Dim mPostingDetail As Integer
        Dim mGSTApplicable As String
        Dim mShippedToSame As String
        Dim mShippedToCode As String
        Dim mReverseCharge As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If UpdateDetail1 = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        '    txtPONo.Text = mPONo
        Exit Function
ErrPart:
        Update1 = False
        MsgInformation(Err.Description)
        PubDBCn.RollbackTrans()
        RsPurMain.Requery()
        RsPurDetail.Requery()
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function UpdateDetail1() As Boolean
        On Error GoTo UpdateDetail1
        Dim Sqlstr As String
        Dim i As Integer
        Dim mItemCode As String
        Dim mItemUOM As String
        Dim mQty As Double
        Dim mRate As Double
        Dim mDisc As Double
        Dim mGross As Double
        Dim mQtyRecd As Double
        Dim mRemarks As String
        Dim mWODesc As String
        Dim mStatus As String
        Dim xUpdate As Boolean
        Dim mPOWEFDate As String
        Dim mIsTentativeRate As String
        Dim mFreightCost As Double
        Dim mVolumeDiscount As Double
        Dim mCGSTPer As String
        Dim mSGSTPer As String
        Dim mIGSTPer As String
        Dim mCGSTAmount As String
        Dim mSGSTAmount As String
        Dim mIGSTAmount As String
        Dim mAcctPostCode As String
        Dim mAcctPostName As String
        Dim mLandedCost As Double
        Dim pInvTypeName As String
        Dim pInvType As String
        Dim pInvTypeFirst As String
        Dim mAcctPostCodeFirst As String
        Dim mNetExpAmount As Double
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim mLocationID As String
        Dim mAccountCode As String = "-1"
        mAccountCode = IIf(MainClass.ValidateWithMasterTable(txtSupplierName.Text, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, mAccountCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mAccountCode, "-1")

        mLocationID = GetDefaultLocation(mAccountCode)

        mCompanyGSTNo = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If

        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i
                xUpdate = False
                .Col = ColItemName
                mWODesc = MainClass.AllowSingleQuote(.Text)
                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)
                .Col = ColAcctPostNameNew
                pInvTypeName = Trim(.Text)
                pInvType = ""
                If MainClass.ValidateWithMasterTable(pInvTypeName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                    pInvType = MasterNo
                End If
                mAcctPostCode = GetDebitNameOfInvType(pInvTypeName, "N")
                If pInvTypeFirst = "" Then
                    pInvTypeFirst = pInvType
                    mAcctPostCodeFirst = mAcctPostCode
                End If
                If (mItemCode <> "" Or mWODesc <> "") Then
                    Sqlstr = " UPDATE FIN_PURCHASE_DET SET " & vbCrLf & " PUR_ACCOUNT_CODE ='" & MainClass.AllowSingleQuote(mAcctPostCode) & "'," & vbCrLf & " ITEM_TRNTYPE =" & Val(pInvType) & "" & vbCrLf & " WHERE  COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MKEY=" & Val(lblMkey.Text) & " AND SUBROWNO=" & i & ""
                    '                If mItemCode <> "" Then
                    '                    SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                    '                Else
                    '                    SqlStr = SqlStr & vbCrLf & " AND ITEM_DESC='" & MainClass.AllowSingleQuote(mWODesc) & "'"
                    '                End If
                    PubDBCn.Execute(Sqlstr)
                End If
            Next
        End With
        Sqlstr = " UPDATE FIN_PURCHASE_HDR SET " & vbCrLf & " TRNTYPE=" & Val(pInvTypeFirst) & ",ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAcctPostCodeFirst) & "', " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MKEY =" & Val(lblMkey.Text) & ""
        PubDBCn.Execute(Sqlstr)
        If mCompanyGSTNo = mPartyGSTNo Then
            mNetExpAmount = Val(lblNetExpAmount.Text)
        Else
            If VB.Left(lblIsGSTRefund.Text, 1) = "G" Or VB.Left(lblIsGSTRefund.Text, 1) = "I" Then
                mNetExpAmount = Val(lblNetExpAmount.Text) + Val(lblTotCGSTAmount.Text) + Val(lblTotIGSTAmount.Text) + Val(lblTotSGSTAmount.Text)
            Else
                mNetExpAmount = Val(lblNetExpAmount.Text)
            End If
        End If
        If PurchasePostTRNGST(PubDBCn, (lblMkey.Text), Val(lblCurRowNo.Text), (LblBookCode.Text), (lblBookType.Text), (lblBookSubType.Text), (txtPONo.Text), (txtPODate.Text), (lblBillNo.Text), (lblBillDate.Text), pInvTypeFirst, (lblSuppCustCode.Text), mAcctPostCodeFirst, Val(lblItemValue.Text), Val(lblNetAmount.Text), IIf(lblCancelled.Text = "Y", True, False), IIf(LblFOC.Text = "Y", True, False), (lblDueDate.Text), VB.Left(lblNarration.Text, 254), (lblRemarks.Text), mNetExpAmount, IIf(lblIsGSTRefund.Text = "G", "Y", "N"), Val(lblTotCGSTRefund.Text), Val(lblTotSGSTRefund.Text), Val(lblTotIGSTRefund.Text), (lblMRRDate.Text), True, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), Val(lblDivisionCode.Text), IIf(lblIsGSTRefund.Text = "R", "Y", "N"), Val(lblTotCGSTAmount.Text) + Val(lblTotIGSTAmount.Text) + Val(lblTotSGSTAmount.Text), Val(lblTotCGSTAmount.Text), Val(lblTotSGSTAmount.Text), Val(lblTotIGSTAmount.Text), (lblSaleBillNo.Text), (lblSaleBillDate.Text), mLocationID) = False Then GoTo UpdateDetail1
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        'Resume
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Sub cmdsearchPO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPO.Click
        On Error GoTo ErrPart
        Dim Sqlstr As String
        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND PURCHASE_TYPE='G' AND REJECTION='N' AND CANCELLED='N'"

        'Sqlstr = "AND PURCHASE_TYPE='G' AND REJECTION='N' AND CANCELLED='N'"


        'Sqlstr = Sqlstr & vbCrLf _
        '    & " AND PUR_TYPE='" & vb.Left(lblBookType.text, 1) & "'" & vbCrLf _
        '    & " AND ORDER_TYPE='" & Right(lblBookType.text, 1) & "'"

        If MainClass.SearchGridMaster((txtPONo.Text), "FIN_PURCHASE_HDR", "VNO", "VDATE", "", "", Sqlstr) = True Then
            txtPONo.Text = AcName
            txtPONO_Validating(txtPONO, New System.ComponentModel.CancelEventArgs(False)) '' txtPONO_Validate False
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
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
        '    MainClass.ButtonStatus Me, XRIGHT, RsPurMain, ADDMode, MODIFYMode, True
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmPurchase_Acct_Post_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim Sqlstr As String
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Sqlstr = "Select * From FIN_PURCHASE_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurMain, ADODB.LockTypeEnum.adLockReadOnly)
        Sqlstr = "Select * From FIN_PURCHASE_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurDetail, ADODB.LockTypeEnum.adLockReadOnly)
        Call AssignGrid(False)
        SetTextLengths()
        Clear1()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        On Error GoTo ERR1
        Dim Sqlstr As String
        Sqlstr = ""
        Sqlstr = " SELECT " & vbCrLf & " A.MKEY AS MKEY, A.VNO, A.VDATE, " & vbCrLf & " B.SUPP_CUST_NAME AS NAME, " & vbCrLf & " ID.ITEM_CODE, IMST.NAME " & vbCrLf
        Sqlstr = Sqlstr & vbCrLf & " FROM FIN_PURCHASE_HDR A, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST B, FIN_INVTYPE_MST IMST " & vbCrLf & " WHERE A.MKEY=ID.MKEY" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE " & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf & " AND ID.PUR_ACCOUNT_CODE=IMST.CODE AND IMST.CATEGORY='P'"
        Sqlstr = Sqlstr & " ORDER BY A.VNO, A.VDATE"
        MainClass.AssignDataInSprd8(Sqlstr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmPurchase_Acct_Post_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub frmPurchase_Acct_Post_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        Call SetMainFormCordinate(Me)
        pmyMenu = myMenu
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, pmyMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Call frmPurchase_Acct_Post_Activated(eventSender, eventArgs)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()
        On Error GoTo ClearErr
        mAccountCode = CStr(-1)
        lblMkey.Text = ""
        txtPONo.Text = ""
        txtPODate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        LblBookCode.Text = ""
        lblBillNo.Text = ""
        lblBillDate.Text = ""
        lblBookType.Text = ""
        lblBookSubType.Text = ""
        lblTRNType.Text = ""
        lblCurRowNo.Text = ""
        lblSuppCustCode.Text = ""
        lblAccountCode.Text = ""
        lblItemValue.Text = ""
        lblNetAmount.Text = ""
        lblCancelled.Text = ""
        LblFOC.Text = ""
        lblDueDate.Text = ""
        lblNarration.Text = ""
        lblRemarks.Text = ""
        lblNetExpAmount.Text = ""
        lblIsGSTRefund.Text = ""
        lblTotCGSTRefund.Text = ""
        lblTotSGSTRefund.Text = ""
        lblTotIGSTRefund.Text = ""
        lblMRRDate.Text = ""
        lblTotCGSTAmount.Text = ""
        lblTotSGSTAmount.Text = ""
        lblTotIGSTAmount.Text = ""
        lblSaleBillDate.Text = ""
        lblSaleBillNo.Text = ""
        lblDivisionCode.Text = ""
        txtSupplierName.Text = ""
        txtCode.Text = ""
        txtCode.Enabled = True
        txtSupplierName.Enabled = True
        SprdMain.Enabled = True
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1, "")
        pShowCalc = False
        '    MainClass.ButtonStatus Me, XRIGHT, RsPurMain, ADDMode, MODIFYMode, True
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer, ByRef mPurType As String)
        On Error GoTo ERR1
        Dim Sqlstr As String
        Dim cntCol As Integer
        With SprdMain
            .set_RowHeight(-1, ConRowHeight * 1)
            .Row = Arow
            .Col = ColWoDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPurDetail.Fields("ITEM_DESC").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 25)
            If mPurType = "W" Or mPurType = "R" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If
            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPurDetail.Fields("ITEM_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 6)
            If mPurType = "W" Or mPurType = "R" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If
            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("Item_Short_Desc", "INV_ITEM_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            If VB.Right(lblBookType.Text, 1) = "O" Then
                .set_ColWidth(.Col, 22)
            Else
                .set_ColWidth(.Col, 22)
            End If
            If mPurType = "W" Or mPurType = "R" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If
            .ColsFrozen = ColItemName
            .Col = ColAcctPostName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("NAME", "FIN_INVTYPE_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 22)
            .Col = ColAcctPostNameNew
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("NAME", "FIN_INVTYPE_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 22)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColWoDesc, ColAcctPostName)
            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub FormatSprdView()
        With SprdView
            .Row = -1
            .set_RowHeight(0, 300)
            .set_ColWidth(0, 500)
            .set_ColWidth(1, 1400)
            .set_ColWidth(2, 1200)
            .set_ColWidth(3, 1000)
            .set_ColWidth(4, 800)
            .set_ColWidth(5, 1000)
            .set_ColWidth(6, 1000)
            .set_ColWidth(7, 3500)
            .set_ColWidth(8, 800)
            .set_ColWidth(9, 2000)
            .set_ColWidth(10, 2000)
            .set_ColWidth(11, 2000)
            .set_ColWidth(12, 1200)
            .ColsFrozen = 2
            .Col = 1
            .ColHidden = True
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtPONo.Maxlength = RsPurMain.Fields("VNO").DefinedSize
        txtPODate.Maxlength = RsPurMain.Fields("VDATE").DefinedSize - 6
        txtSupplierName.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mItemCode As String
        'Dim mQty As Double
        'Dim mPOWEFCheck As String
        'Dim mPOWEF As String
        'Dim mCheckPOWEF As Boolean
        '
        'Dim pPervRate As Double
        'Dim pCurrRate As Double
        'Dim mPrice As Double
        'Dim mDisc As Double
        '
        Dim i As Integer
        'Dim mIsApproved As String
        'Dim pPONO As Double
        'Dim mItemCategory As String
        'Dim mItemUOM As String
        'Dim mItemStock As Double
        'Dim mIsCapitalCheck As String
        'Dim mIsItemCapital As String
        Dim mAcctPostName As String
        Dim mFirstAcctPostName As String
        'Dim pISGSTRegd As String
        'Dim mLocal As String
        'Dim mPartyGSTNo As String
        'Dim mHSNCode As String
        'Dim mSAC As String
        'Dim mServCode As String
        'Dim pCGSTPer As Double
        'Dim pSGSTPer As Double
        'Dim pIGSTPer As Double
        'Dim mGSTClass As String
        FieldsVarification = True
        If CDate(lblBillDate.Text) < CDate(PubGSTApplicableDate) Then
            MsgInformation("Pre GST Voucher Cann't be Save. Cannot Save")
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtPODate.Text) = "" Then
            MsgInformation(" Voucher Date is empty. Cannot Save")
            txtPODate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtPODate.Text) <> "" Then
            If IsDate(txtPODate.Text) = False Then
                MsgInformation(" Invalid Voucher Date. Cannot Save")
                If txtPODate.Enabled = True Then txtPODate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If
        If Trim(txtSupplierName.Text) = "" Then
            MsgInformation("Supplier Name is Blank. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCode.Text = MasterNo
        Else
            MsgInformation("Invalid Supplier Name. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            FieldsVarification = False
            Exit Function
        End If
        SprdMain.Row = 1
        SprdMain.Col = ColAcctPostNameNew
        mFirstAcctPostName = Trim(UCase(SprdMain.Text))
        For i = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = i
            SprdMain.Col = ColItemCode
            mItemCode = Trim(UCase(SprdMain.Text))
            SprdMain.Row = i
            SprdMain.Col = ColAcctPostNameNew
            If Trim(UCase(SprdMain.Text)) = "" Then
                SprdMain.Text = mFirstAcctPostName
            End If
            mAcctPostName = Trim(UCase(SprdMain.Text))
            If mAcctPostName = "" Then
                MsgInformation("Account Post Name Cann't be Blank.")
                MainClass.SetFocusToCell(SprdMain, i, ColAcctPostNameNew)
                FieldsVarification = False
                Exit Function
            Else
                If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = False Then
                    MsgInformation("Invaild Account Post Name.")
                    MainClass.SetFocusToCell(SprdMain, i, ColAcctPostNameNew)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        Next
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume
    End Function
    Private Sub frmPurchase_Acct_Post_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()
        RsPurMain.Close()
        'RsOpOuts.Close
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        'Dim Sqlstr As String
        If eventArgs.Row = 0 And eventArgs.Col = ColAcctPostNameNew Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColAcctPostNameNew
                MainClass.SearchGridMaster(.Text, "FIN_INVTYPE_MST", "NAME", , , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'")
                .Row = .ActiveRow
                .Col = ColAcctPostNameNew
                .Text = AcName
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColAcctPostNameNew)
            End With
        End If
        '    If mAmendStatus = True Or (txtAmendNo.Text) > 0 Then
        '        Exit Sub
        '    End If
        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColItemName)
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F2 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F2 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))
        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim xICode As String
        Dim xIDesc As String
        Dim xAcctPostName As String
        Dim xWoDesc As String
        If eventArgs.NewRow = -1 Then Exit Sub
        Select Case eventArgs.col
            Case ColAcctPostNameNew
                SprdMain.Row = SprdMain.ActiveRow
                '            SprdMain.Col = ColItemCode
                '            xICode = SprdMain.Text
                '            If xICode = "" Then GoTo CalcPart
                SprdMain.Col = ColAcctPostNameNew
                xAcctPostName = SprdMain.Text
                If xAcctPostName <> "" Then
                    If MainClass.ValidateWithMasterTable(xAcctPostName, "NAME", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = False Then
                        MsgInformation("Invaild Account Post Name.")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColAcctPostNameNew)
                        Exit Sub
                    End If
                End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Row = SprdView.ActiveRow
        SprdView.Col = 2
        txtPONo.Text = SprdView.Text
        txtPONO_Validating(txtPONO, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    End Sub
    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim Sqlstr As String
        If Trim(txtCode.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_Name", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            txtSupplierName.Text = MasterNo
            txtCode.Enabled = False
        Else
            MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtPODate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPODate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPODate.Text) = "" Then GoTo EventExitSub
        If IsDate(txtPODate.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtPONO_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.DoubleClick
        cmdsearchPO_Click(cmdsearchPO, New System.EventArgs())
    End Sub
    Private Sub txtPONO_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPONo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearchPO_Click(cmdsearchPO, New System.EventArgs())
    End Sub
    Private Sub txtSupplierName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierName.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
        Dim Sqlstr As String
        Dim xAcctCode As String
        Dim mIsApproved As String
        If Trim(txtSupplierName.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
            txtCode.Text = xAcctCode
            txtCode.Enabled = False
        Else
            MsgBox("Invalid Supplier Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub Show1()
        On Error GoTo ShowErrPart
        Dim mAccountName As String
        Dim mAddMode As Boolean
        Dim mServiceCode As Double
        Dim mOwnerName As String
        Dim mOwnerCode As String
        Dim mPostDetail As Integer
        Dim mShippedToCode As String
        Dim mShippedToName As String
        Dim mGSTStatus As String
        Dim mPurType As String
        Clear1()
        pShowCalc = False
        If Not RsPurMain.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsPurMain.Fields("MKEY").Value), "", RsPurMain.Fields("MKEY").Value)
            txtPONo.Text = IIf(IsDbNull(RsPurMain.Fields("VNO").Value), "", RsPurMain.Fields("VNO").Value)
            txtPODate.Text = VB6.Format(IIf(IsDbNull(RsPurMain.Fields("VDATE").Value), "", RsPurMain.Fields("VDATE").Value), "DD/MM/YYYY")
            LblBookCode.Text = IIf(IsDbNull(RsPurMain.Fields("BOOKCODE").Value), "", RsPurMain.Fields("BOOKCODE").Value)
            lblBillNo.Text = IIf(IsDbNull(RsPurMain.Fields("BILLNO").Value), "", RsPurMain.Fields("BILLNO").Value)
            lblBillDate.Text = IIf(IsDbNull(RsPurMain.Fields("INVOICE_DATE").Value), "", RsPurMain.Fields("INVOICE_DATE").Value)
            lblBookType.Text = IIf(IsDbNull(RsPurMain.Fields("BOOKTYPE").Value), "", RsPurMain.Fields("BOOKTYPE").Value)
            lblBookSubType.Text = IIf(IsDbNull(RsPurMain.Fields("BOOKSUBTYPE").Value), "", RsPurMain.Fields("BOOKSUBTYPE").Value)
            lblTRNType.Text = IIf(IsDbNull(RsPurMain.Fields("TRNTYPE").Value), "", RsPurMain.Fields("TRNTYPE").Value)
            lblCurRowNo.Text = IIf(IsDbNull(RsPurMain.Fields("ROWNO").Value), "", RsPurMain.Fields("ROWNO").Value)
            lblSuppCustCode.Text = IIf(IsDbNull(RsPurMain.Fields("SUPP_CUST_CODE").Value), "", RsPurMain.Fields("SUPP_CUST_CODE").Value)
            lblAccountCode.Text = IIf(IsDbNull(RsPurMain.Fields("ACCOUNTCODE").Value), "", RsPurMain.Fields("ACCOUNTCODE").Value)
            lblItemValue.Text = IIf(IsDbNull(RsPurMain.Fields("ITEMVALUE").Value), "", RsPurMain.Fields("ITEMVALUE").Value)
            lblNetAmount.Text = IIf(IsDbNull(RsPurMain.Fields("NETVALUE").Value), "", RsPurMain.Fields("NETVALUE").Value)
            lblCancelled.Text = IIf(IsDbNull(RsPurMain.Fields("CANCELLED").Value), "", RsPurMain.Fields("CANCELLED").Value)
            LblFOC.Text = IIf(IsDbNull(RsPurMain.Fields("ISFOC").Value), "", RsPurMain.Fields("ISFOC").Value)
            lblDueDate.Text = VB6.Format(IIf(IsDbNull(RsPurMain.Fields("PAYMENTDATE").Value), "", RsPurMain.Fields("PAYMENTDATE").Value), "DD/MM/YYYY")
            lblNarration.Text = IIf(IsDbNull(RsPurMain.Fields("NARRATION").Value), "", RsPurMain.Fields("NARRATION").Value)
            lblRemarks.Text = IIf(IsDbNull(RsPurMain.Fields("REMARKS").Value), "", RsPurMain.Fields("REMARKS").Value)
            lblNetExpAmount.Text = IIf(IsDbNull(RsPurMain.Fields("TOTEXPAMT").Value), 0, RsPurMain.Fields("TOTEXPAMT").Value) '
            '        + IIf(IsNull(RsPurMain.Fields("TOTALGSTVALUE").Value), 0, RsPurMain.Fields("TOTALGSTVALUE").Value)
            lblIsGSTRefund.Text = IIf(IsDbNull(RsPurMain.Fields("ISGSTAPPLICABLE").Value), "", RsPurMain.Fields("ISGSTAPPLICABLE").Value)
            lblTotCGSTRefund.Text = IIf(IsDbNull(RsPurMain.Fields("TOTCGST_REFUNDAMT").Value), "", RsPurMain.Fields("TOTCGST_REFUNDAMT").Value)
            lblTotSGSTRefund.Text = IIf(IsDbNull(RsPurMain.Fields("TOTSGST_REFUNDAMT").Value), "", RsPurMain.Fields("TOTSGST_REFUNDAMT").Value)
            lblTotIGSTRefund.Text = IIf(IsDbNull(RsPurMain.Fields("TOTIGST_REFUNDAMT").Value), "", RsPurMain.Fields("TOTIGST_REFUNDAMT").Value)
            lblMRRDate.Text = IIf(IsDbNull(RsPurMain.Fields("MRRDATE").Value), "", RsPurMain.Fields("MRRDATE").Value)
            lblTotCGSTAmount.Text = IIf(IsDbNull(RsPurMain.Fields("TOTCGST_AMOUNT").Value), "", RsPurMain.Fields("TOTCGST_AMOUNT").Value)
            lblTotSGSTAmount.Text = IIf(IsDbNull(RsPurMain.Fields("TOTSGST_AMOUNT").Value), "", RsPurMain.Fields("TOTSGST_AMOUNT").Value)
            lblTotIGSTAmount.Text = IIf(IsDbNull(RsPurMain.Fields("TOTIGST_AMOUNT").Value), "", RsPurMain.Fields("TOTIGST_AMOUNT").Value)
            lblDivisionCode.Text = IIf(IsDbNull(RsPurMain.Fields("DIV_CODE").Value), "", RsPurMain.Fields("DIV_CODE").Value)
            lblSaleBillDate.Text = IIf(IsDbNull(RsPurMain.Fields("SALEBILL_NO").Value), "", RsPurMain.Fields("SALEBILL_NO").Value)
            lblSaleBillNo.Text = IIf(IsDbNull(RsPurMain.Fields("SALEBILLDATE").Value), "", RsPurMain.Fields("SALEBILLDATE").Value)
            mAccountCode = IIf(IsDbNull(RsPurMain.Fields("SUPP_CUST_CODE").Value), -1, RsPurMain.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = MasterNo
            End If
            txtSupplierName.Text = mAccountName
            txtCode.Text = Trim(IIf(IsDbNull(RsPurMain.Fields("SUPP_CUST_CODE").Value), "", RsPurMain.Fields("SUPP_CUST_CODE").Value))
            txtCode.Enabled = False
            txtSupplierName.Enabled = True
            '        mPurType = Trim(IIf(IsNull(RsPurMain.Fields("PUR_TYPE").Value), "", RsPurMain.Fields("PUR_TYPE").Value))
            Call ShowDetail1(mPurType)
            CmdSave.Enabled = True
        End If
        '    FormatSprdMain -1
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True
        txtPONo.Enabled = True
        cmdSearchPO.Enabled = True
        '    MainClass.ButtonStatus Me, XRIGHT, RsPurMain, ADDMode, MODIFYMode, True
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub ShowDetail1(ByRef mPurType As String)
        On Error GoTo ERR1
        Dim i As Integer
        Dim Sqlstr As String
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mIdenty As String
        Dim mWODesc As String
        Dim mCurrValue As Double
        Dim mPrevValue As Double
        Dim mPrice As Double
        Dim mDisc As Double
        Dim mPOWEFDate As String
        Dim mPrevPOWEFDate As String
        Dim mInvTypeCode As String
        Dim mInvTypeDesc As String
        Dim mHSNCode As String
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        Sqlstr = ""
        Sqlstr = " SELECT * " & vbCrLf & " FROM FIN_PURCHASE_DET " & vbCrLf & " Where " & vbCrLf & " MKEY=" & Val(lblMkey.Text) & "" & vbCrLf & " Order By SUBROWNO"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPurDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1
            i = 1
            '        .MoveFirst
            Do While Not .EOF
                SprdMain.Row = i
                '            SprdMain.Col = ColWoDesc
                '            SprdMain.Text = Trim(IIf(IsNull(.Fields("WO_DESCRIPTION").Value), "", .Fields("WO_DESCRIPTION").Value))
                '            mWODesc = Trim(IIf(IsNull(.Fields("WO_DESCRIPTION").Value), "", .Fields("WO_DESCRIPTION").Value))
                SprdMain.Col = ColItemCode
                If mWODesc = "" Then
                    mItemCode = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                Else
                    mItemCode = ""
                End If
                '            If mItemCode = "C00010" Then MsgBox "OK"
                SprdMain.Text = mItemCode
                SprdMain.Col = ColItemName
                If mWODesc = "" Then
                    MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                    mItemDesc = MasterNo
                Else
                    mItemDesc = ""
                End If
                SprdMain.Text = mItemDesc
                mInvTypeCode = Trim(IIf(IsDbNull(.Fields("ITEM_TRNTYPE").Value), "", .Fields("ITEM_TRNTYPE").Value))
                mInvTypeDesc = ""
                If MainClass.ValidateWithMasterTable(mInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                    mInvTypeDesc = MasterNo
                End If
                SprdMain.Col = ColAcctPostName
                SprdMain.Value = mInvTypeDesc
                SprdMain.Col = ColAcctPostNameNew
                SprdMain.Value = mInvTypeDesc
                .MoveNext()
                i = i + 1
                SprdMain.MaxRows = i
            Loop
        End With
        Call FormatSprdMain(-1, mPurType)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''   Resume
    End Sub
    Private Sub txtPODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPODate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPONo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtPONO_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPONo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim xMKey As String
        Dim mPONo As Double
        Dim Sqlstr As String
        If Trim(txtPONo.Text) = "" Then GoTo EventExitSub
        '    mPONo = Val(txtPONo.Text)
        Sqlstr = "SELECT * FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE VNO='" & MainClass.AllowSingleQuote(UCase(txtPONo.Text)) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND PURCHASE_TYPE='G' AND REJECTION='N' AND CANCELLED='N'"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPurMain.EOF = False Then
            Clear1()
            Show1()
        Else
            MsgInformation("Invalid Purchase Order No.")
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
