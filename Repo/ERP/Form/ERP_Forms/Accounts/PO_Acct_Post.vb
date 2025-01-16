Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPO_Acct_Post
    Inherits System.Windows.Forms.Form
    Dim RsPOMain As ADODB.Recordset ''ADODB.Recordset
    Dim RsPODetail As ADODB.Recordset ''ADODB.Recordset

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
            txtAmendNo_Validating(txtAmendNo, New System.ComponentModel.CancelEventArgs(False)) '' txtPONO_Validate False
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
        Dim SqlStr As String = ""
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

        SqlStr = " UPDATE PUR_PURCHASE_HDR SET " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MKEY =" & Val(lblMkey.Text) & ""


        PubDBCn.Execute(SqlStr)
        If UpdateDetail1 = False Then GoTo ErrPart

        Update1 = True
        PubDBCn.CommitTrans()
        '    txtPONo.Text = mPONo
        Exit Function
ErrPart:
        Update1 = False
        MsgInformation(Err.Description)
        PubDBCn.RollbackTrans()
        RsPOMain.Requery()
        RsPODetail.Requery()
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemUOM As String = ""
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

        Dim mAcctPostCode As String = ""
        Dim mAcctPostName As String
        Dim mLandedCost As Double


        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I
                xUpdate = False

                .Col = ColWoDesc
                mWODesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColAcctPostName
                mAcctPostName = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                    mAcctPostCode = MasterNo
                End If

                If (mItemCode <> "" Or mWODesc <> "") Then
                    SqlStr = " UPDATE PUR_PURCHASE_DET SET ACCOUNT_POSTING_CODE ='" & MainClass.AllowSingleQuote(mAcctPostCode) & "'" & vbCrLf & " WHERE  COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MKEY=" & Val(lblMkey.Text) & ""

                    If mItemCode <> "" Then
                        SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND WO_DESCRIPTION='" & MainClass.AllowSingleQuote(mWODesc) & "'"
                    End If

                    PubDBCn.Execute(SqlStr)
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

    Private Sub cmdSearchAmend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAmend.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtPONo.Text) = "" Then
            Exit Sub
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PO=" & Val(txtPONo.Text) & "" ''& vbCrLf |            & " AND ORDER_TYPE='" & Right(lblBookType.text, 1) & "'" & vbCrLf |            & " "

        If MainClass.SearchGridMaster("", "PUR_PURCHASE_HDR", "trim(TO_CHAR(AMEND_NO,'000'))", "AMEND_DATE", , , SqlStr) = True Then
            txtAmendNo.Text = AcName
            txtAmendDate.Text = AcName1
            txtAmendNo_Validating(txtAmendNo, New System.ComponentModel.CancelEventArgs(False)) ''txtPONO_Validate False
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchPO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPO.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y'"

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " AND PUR_TYPE='" & vb.Left(lblBookType.text, 1) & "'" & vbCrLf _
        ''            & " AND ORDER_TYPE='" & Right(lblBookType.text, 1) & "'"

        If MainClass.SearchGridMaster((txtPONo.Text), "PUR_PURCHASE_HDR", "AUTO_KEY_PO", "AMEND_NO", "PUR_ORD_DATE", "SUPP_CUST_CODE", SqlStr) = True Then
            txtPONo.Text = AcName
            txtAmendNo.Text = AcName1
            txtAmendNo_Validating(txtAmendNo, New System.ComponentModel.CancelEventArgs(False)) '' txtPONO_Validate False
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
        '    MainClass.ButtonStatus Me, XRIGHT, RsPOMain, ADDMode, MODIFYMode, True
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmPO_Acct_Post_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        SqlStr = "Select * From PUR_PURCHASE_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PUR_PURCHASE_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPODetail, ADODB.LockTypeEnum.adLockReadOnly)


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
        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf & " A.MKEY AS MKEY, A.AUTO_KEY_PO AS PO_NO, A.PUR_ORD_DATE AS PO_DATE, " & vbCrLf & " A.AMEND_NO, A.AMEND_DATE,  " & vbCrLf & " A.AMEND_WEF_DATE AS WEF, B.SUPP_CUST_NAME AS NAME, " & vbCrLf & " ID.ITEM_CODE, IMST.NAME " & vbCrLf
        SqlStr = SqlStr & vbCrLf & " FROM PUR_PURCHASE_HDR A, PUR_PURCHASE_DET ID, FIN_SUPP_CUST_MST B, FIN_INVTYPE_MST IMST " & vbCrLf & " WHERE A.MKEY=ID.MKEY" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE " & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf & " AND ID.ACCOUNT_POSTING_CODE=IMST.CODE AND IMST.CATEGORY='P'"

        SqlStr = SqlStr & " ORDER BY SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4),A.AUTO_KEY_PO,A.AMEND_NO"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmPO_Acct_Post_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmPO_Acct_Post_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Call frmPO_Acct_Post_Activated(eventSender, eventArgs)
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

        txtAmendNo.Text = CStr(0)
        txtAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtSupplierName.Text = ""


        txtCode.Text = ""
        txtCode.Enabled = True
        txtSupplierName.Enabled = True


        SprdMain.Enabled = True

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1, "")

        pShowCalc = False
        '    MainClass.ButtonStatus Me, XRIGHT, RsPOMain, ADDMode, MODIFYMode, True
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer, ByRef mPurType As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim cntCol As Integer

        With SprdMain
            .set_RowHeight(-1, ConRowHeight * 2)
            .Row = Arow

            .Col = ColWoDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPODetail.Fields("WO_DESCRIPTION").DefinedSize
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
            .TypeEditLen = RsPODetail.Fields("ITEM_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)
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
                .set_ColWidth(.Col, 40)
            Else
                .set_ColWidth(.Col, 40)
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
            .set_ColWidth(.Col, 40)
            .TypeEditMultiLine = True

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColWoDesc, ColItemName)
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

        txtPONo.Maxlength = RsPOMain.Fields("AUTO_KEY_PO").Precision
        txtPODate.Maxlength = RsPOMain.Fields("PUR_ORD_DATE").DefinedSize - 6


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
        Dim I As Integer
        'Dim mIsApproved As String
        'Dim pPONO As Double
        'Dim mItemCategory As String
        'Dim mItemUOM As String = ""
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


        If Trim(txtPODate.Text) = "" Then
            MsgInformation(" PO Date is empty. Cannot Save")
            txtPODate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtPODate.Text) <> "" Then
            If IsDate(txtPODate.Text) = False Then
                MsgInformation(" Invalid PO Date. Cannot Save")
                If txtPODate.Enabled = True Then txtPODate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtAmendDate.Text) <> "" Then
            If IsDate(txtAmendDate.Text) = False Then
                MsgInformation(" Invalid PO Amend Date. Cannot Save")
                If txtAmendDate.Enabled = True Then txtAmendDate.Focus()
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
        SprdMain.Col = ColAcctPostName
        mFirstAcctPostName = Trim(UCase(SprdMain.Text))


        For I = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = I
            SprdMain.Col = ColItemCode
            mItemCode = Trim(UCase(SprdMain.Text))

            SprdMain.Row = I
            SprdMain.Col = ColAcctPostName
            If Trim(UCase(SprdMain.Text)) = "" Then
                SprdMain.Text = mFirstAcctPostName
            End If
            mAcctPostName = Trim(UCase(SprdMain.Text))

            If mAcctPostName = "" Then
                MsgInformation("Account Post Name Cann't be Blank.")
                MainClass.SetFocusToCell(SprdMain, I, ColAcctPostName)
                FieldsVarification = False
                Exit Function
            Else
                If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = False Then
                    MsgInformation("Invaild Account Post Name.")
                    MainClass.SetFocusToCell(SprdMain, I, ColAcctPostName)
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

    Private Sub frmPO_Acct_Post_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()
        RsPOMain.Close()
        'RsOpOuts.Close
    End Sub


    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        'Dim SqlStr As String = ""

        If eventArgs.Row = 0 And eventArgs.Col = ColAcctPostName Then
            With SprdMain
                eventArgs.row = .ActiveRow
                eventArgs.col = ColAcctPostName
                MainClass.SearchGridMaster(.Text, "FIN_INVTYPE_MST", "NAME", , , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'")
                eventArgs.row = .ActiveRow
                eventArgs.col = ColAcctPostName
                .Text = AcName

                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColAcctPostName)
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

            Case ColAcctPostName
                SprdMain.Row = SprdMain.ActiveRow
                '            SprdMain.Col = ColItemCode
                '            xICode = SprdMain.Text
                '            If xICode = "" Then GoTo CalcPart
                SprdMain.Col = ColAcctPostName
                xAcctPostName = SprdMain.Text

                If MainClass.ValidateWithMasterTable(xAcctPostName, "NAME", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = False Then
                    MsgInformation("Invaild Account Post Name.")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColAcctPostName)
                    Exit Sub
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

        SprdView.Col = 4
        txtAmendNo.Text = SprdView.Text

        txtAmendNo_Validating(txtAmendNo, New System.ComponentModel.CancelEventArgs(False)) ''txtPONO_Validate False
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    End Sub

    Private Sub txtAmendDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmendDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAmendDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAmendDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtAmendDate.Text) = "" Then GoTo EventExitSub

        If IsDate(txtAmendDate.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmendNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Public Sub txtAmendNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAmendNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As String = ""
        Dim mPONo As Double
        Dim SqlStr As String = ""


        If Trim(txtPONo.Text) = "" Then GoTo EventExitSub

        If Len(txtPONo.Text) < 6 Then
            txtPONo.Text = Val(txtPONo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mPONo = Val(txtPONo.Text)

        If MODIFYMode = True And RsPOMain.BOF = False Then xMKey = RsPOMain.Fields("mKey").Value

        SqlStr = "SELECT * FROM PUR_PURCHASE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y'"

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " AND PUR_TYPE='" & vb.Left(lblBookType.text, 1) & "'" & vbCrLf _
        ''            & " AND ORDER_TYPE='" & Right(lblBookType.text, 1) & "'"


        SqlStr = SqlStr & vbCrLf & " AND AMEND_NO=" & Val(txtAmendNo.Text) & ""


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPOMain.EOF = False Then
            Clear1()
            Show1()
        Else
            SqlStr = "SELECT * FROM PUR_PURCHASE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y'"

            '        SqlStr = SqlStr & vbCrLf _
            ''            & " AND PUR_TYPE='" & vb.Left(lblBookType.text, 1) & "'" & vbCrLf _
            ''            & " AND ORDER_TYPE='" & Right(lblBookType.text, 1) & "'"

            SqlStr = SqlStr & vbCrLf & " AND AMEND_NO IN (" & vbCrLf & " SELECT MAX(AMEND_NO) FROM PUR_PURCHASE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y'"

            '        SqlStr = SqlStr & vbCrLf _
            ''            & " AND PUR_TYPE='" & vb.Left(lblBookType.text, 1) & "'" & vbCrLf _
            ''            & " AND ORDER_TYPE='" & Right(lblBookType.text, 1) & "'"

            SqlStr = SqlStr & vbCrLf & ")"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
            If RsPOMain.EOF = False Then
                Clear1()
                Show1()
            Else

                If ADDMode = False And MODIFYMode = False Then
                    MsgBox("No Such PO No. Click, Add for New", MsgBoxStyle.Information)
                    txtAmendNo.Text = CStr(0)
                    '                Cancel = True
                ElseIf MODIFYMode = True Then
                    SqlStr = "SELECT * FROM PUR_PURCHASE_HDR WHERE MKEY=" & Val(xMKey) & " AND ISGSTENABLE_PO='Y'"

                    '                SqlStr = SqlStr & vbCrLf _
                    ''                    & " AND PUR_TYPE='" & vb.Left(lblBookType.text, 1) & "'" & vbCrLf _
                    ''                    & " AND ORDER_TYPE='" & Right(lblBookType.text, 1) & "'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
                End If
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
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
        Dim SqlStr As String = ""


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
        ErrorMsg(Err.Description, err.NUmber, MsgBoxStyle.Critical)
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

    Private Sub txtPONo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.DoubleClick
        cmdSearchPO_Click(cmdSearchPO, New System.EventArgs())
    End Sub

    Private Sub txtPONo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPONo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchPO_Click(cmdSearchPO, New System.EventArgs())
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
        Dim SqlStr As String = ""
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
        ErrorMsg(Err.Description, err.NUmber, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub Show1()
        On Error GoTo ShowErrPart
        Dim mAccountName As String = ""
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
        If Not RsPOMain.EOF Then

            lblMkey.Text = IIf(IsDbNull(RsPOMain.Fields("MKEY").Value), "", RsPOMain.Fields("MKEY").Value)
            txtPONo.Text = IIf(IsDbNull(RsPOMain.Fields("AUTO_KEY_PO").Value), "", RsPOMain.Fields("AUTO_KEY_PO").Value)
            txtPODate.Text = VB6.Format(IIf(IsDbNull(RsPOMain.Fields("PUR_ORD_DATE").Value), "", RsPOMain.Fields("PUR_ORD_DATE").Value), "DD/MM/YYYY")

            txtAmendNo.Text = IIf(IsDbNull(RsPOMain.Fields("AMEND_NO").Value), 0, RsPOMain.Fields("AMEND_NO").Value)
            txtAmendDate.Text = VB6.Format(IIf(IsDbNull(RsPOMain.Fields("AMEND_DATE").Value), "", RsPOMain.Fields("AMEND_DATE").Value), "DD/MM/YYYY")

            mAccountCode = IIf(IsDbNull(RsPOMain.Fields("SUPP_CUST_CODE").Value), -1, RsPOMain.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = MasterNo
            End If

            txtSupplierName.Text = mAccountName
            txtCode.Text = Trim(IIf(IsDbNull(RsPOMain.Fields("SUPP_CUST_CODE").Value), "", RsPOMain.Fields("SUPP_CUST_CODE").Value))
            txtCode.Enabled = False
            txtSupplierName.Enabled = True
            mPurType = Trim(IIf(IsDbNull(RsPOMain.Fields("PUR_TYPE").Value), "", RsPOMain.Fields("PUR_TYPE").Value))

            Call ShowDetail1(mPurType)
            CmdSave.Enabled = True
        End If
        '    FormatSprdMain -1

        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True
        txtPONo.Enabled = True
        cmdSearchPO.Enabled = True
        cmdSearchAmend.Enabled = True
        '    MainClass.ButtonStatus Me, XRIGHT, RsPOMain, ADDMode, MODIFYMode, True
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub


    Private Sub ShowDetail1(ByRef mPurType As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
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
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM PUR_PURCHASE_DET " & vbCrLf & " Where " & vbCrLf & " MKEY=" & Val(lblMkey.Text) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPODetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsPODetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1
            I = 1
            '        .MoveFirst

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColWoDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("WO_DESCRIPTION").Value), "", .Fields("WO_DESCRIPTION").Value))
                mWODesc = Trim(IIf(IsDbNull(.Fields("WO_DESCRIPTION").Value), "", .Fields("WO_DESCRIPTION").Value))

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

                mInvTypeCode = Trim(IIf(IsDbNull(.Fields("ACCOUNT_POSTING_CODE").Value), "", .Fields("ACCOUNT_POSTING_CODE").Value))
                mInvTypeDesc = ""

                If MainClass.ValidateWithMasterTable(mInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                    mInvTypeDesc = MasterNo
                End If

                SprdMain.Col = ColAcctPostName
                SprdMain.Value = mInvTypeDesc

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
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

        KeyAscii = MainClass.SetNumericField(KeyAscii)
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
        Dim SqlStr As String = ""


        If Trim(txtPONo.Text) = "" Then GoTo EventExitSub

        If Len(txtPONo.Text) < 6 Then
            txtPONo.Text = Val(txtPONo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mPONo = Val(txtPONo.Text)

        SqlStr = "SELECT * FROM PUR_PURCHASE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y' "

        SqlStr = SqlStr & vbCrLf & " AND AMEND_NO = (" & vbCrLf & " SELECT MAX(AMEND_NO) AS AMEND_NO FROM PUR_PURCHASE_HDR" & vbCrLf & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "' AND ISGSTENABLE_PO='Y'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPOMain.EOF = False Then
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


    Private Function GetMaxAmendNo(ByRef pPONO As Double) As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT MAX(AMEND_NO) AS AMEND_NO" & vbCrLf & " FROM PUR_PURCHASE_HDR" & vbCrLf & " WHERE AUTO_KEY_PO=" & Val(CStr(pPONO)) & "" '& vbCrLf |        & " AND PO_STATUS='Y' " & vbCrLf |        & " AND PO_CLOSED='N' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDbNull(RsTemp.Fields("AMEND_NO").Value) Then
                GetMaxAmendNo = 0
            Else
                GetMaxAmendNo = Val(RsTemp.Fields("AMEND_NO").Value) + 1
            End If
        Else
            GetMaxAmendNo = 0
        End If

        Exit Function
ErrPart:
        GetMaxAmendNo = Val(txtAmendNo.Text)
    End Function
End Class
