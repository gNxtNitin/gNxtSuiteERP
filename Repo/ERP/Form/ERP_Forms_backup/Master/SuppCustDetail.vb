Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports AxFPSpreadADO

Friend Class frmSuppCustDetail
    Inherits System.Windows.Forms.Form
    Dim RsACMMain As ADODB.Recordset ''ADODB.Recordset			
    Dim RsACMDetail As ADODB.Recordset ''ADODB.Recordset			
    'Private PvtDBCn As ADODB.Connection			

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String
    Dim mDataShow As Boolean

    Dim mSearchKey As String
    Dim cntSearchRow As Long
    Dim cntSearchCol As Long

    Dim mCurrencyFactor As Double
    Private Const ConRowHeight As Short = 14

    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColItemUOM As Short = 3
    Private Const ColItemPartNo As Short = 4
    Private Const ColItemType As Short = 5
    Private Const ColItemRateINR As Short = 6
    Private Const ColItemRate As Short = 7
    Private Const ColItemMRP As Short = 8
    Private Const ColItemDisc As Short = 9
    Private Const ColItemApproved As Short = 10
    Private Const ColItemLock As Short = 11
    Private Const ColCostingReq As Short = 12
    Private Const ColPartyPer As Short = 13

    Private Sub SetCombo(ByRef ComboName As System.Windows.Forms.ComboBox, ByRef mMasterType As String)
        Dim CntCount As Integer

        For CntCount = 0 To ComboName.Items.Count - 1
            ComboName.SelectedIndex = CntCount
            If mMasterType = VB.Left(ComboName.Text, 1) Then
                Exit Sub
            End If
        Next
        ComboName.SelectedIndex = -1
    End Sub

    Private Sub chkApproved_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkApproved.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            CmdAdd.Text = ConCmdCancelCaption
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtName.Focus()
            SprdMain.Enabled = True
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        Dim SqlStr As String = ""

        If txtName.Text = "" Then Call ErrorMsg("Nothing to Delete", "", MsgBoxStyle.Critical) : Exit Sub

        If Not RsACMMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_CUST_HDR", (txtName.Text), RsACMMain, "SUPP_CUST_Code") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "FIN_SUPP_CUST_HDR", "SUPP_CUST_CODE", RsACMMain.Fields("SUPP_CUST_CODE").Value) = False Then GoTo DelErrPart

                SqlStr = " DELETE From FIN_SUPP_CUST_HDR WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & RsACMMain.Fields("SUPP_CUST_CODE").Value & "'"
                PubDBCn.Execute(SqlStr)

                SqlStr = " DELETE From FIN_SUPP_CUST_DET WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & RsACMMain.Fields("SUPP_CUST_CODE").Value & "'"

                PubDBCn.Execute(SqlStr)

                PubDBCn.CommitTrans()
                RsACMMain.Requery() ''.Refresh				
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        ''Resume				
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''				
        RsACMMain.Requery() ''.Refresh				
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            SprdMain.Enabled = True
            MainClass.ButtonStatus(Me, XRIGHT, RsACMMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            SprdMain.Enabled = True    '' False Sandeep 15/05/2022
            Show1()
        End If
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String


        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "Supplier/Customer Item Detail"
        mSubTitle = ""


        SqlStr = MakeSQL()

        mRptFileName = "SuppCustDtl.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        'Report1.ReportFileName = PubReportFolderPath & mRptFileName				

        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Report1.ReportFileName = ""

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        If FieldVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If UpdateMain1() = False Then GoTo UpdateError

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''				
        Update1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mVendorApproved As String

        mVendorApproved = IIf(chkApproved.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If ADDMode = True Then
            mAccountCode = MainClass.AllowSingleQuote(txtCode.Text) ''MainClass.AutoGenRowNo("ACM", "Code", PubDBCn)				

            SqlStr = ""
            SqlStr = " INSERT INTO FIN_SUPP_CUST_HDR ( " & vbCrLf _
                & " COMPANY_CODE,  " & vbCrLf _
                & " SUPP_CUST_CODE,  " & vbCrLf _
                & " PAYMENT_CODE,  " & vbCrLf _
                & " DELIVERY,  " & vbCrLf _
                & " EXCISE_OTHERS,  " & vbCrLf _
                & " MODE_DESPATCH,  " & vbCrLf _
                & " INSPECTION,  " & vbCrLf _
                & " PACKING_FORWARDING,  " & vbCrLf _
                & " INSURANCE,  " & vbCrLf & " OTHERS_COND1,  " & vbCrLf _
                & " OTHERS_COND2, IS_APPROVED, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE, INVOICE_LINEITEM) VALUES ( "


            SqlStr = SqlStr & vbCrLf _
                & " " & RsCompany.Fields("Company_Code").Value & ", " & vbCrLf _
                & " '" & mAccountCode & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(lblPaymentTerms.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDelivery.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtExcise.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDespMode.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtInspection.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtPacking.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtInsurance.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtOthCond1.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtOthCond2.Text) & "', " & vbCrLf _
                & " '" & mVendorApproved & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'',''," & Val(txtInvoiceLineNo.Text) & ")"

        End If

        If MODIFYMode = True Then
            SqlStr = ""
            SqlStr = " UPDATE FIN_SUPP_CUST_HDR SET  " & vbCrLf _
                & " PAYMENT_CODE='" & MainClass.AllowSingleQuote(lblPaymentTerms.Text) & "', INVOICE_LINEITEM=" & Val(txtInvoiceLineNo.Text) & ", " & vbCrLf _
                & " DELIVERY='" & MainClass.AllowSingleQuote(txtDelivery.Text) & "', " & vbCrLf _
                & " EXCISE_OTHERS='" & MainClass.AllowSingleQuote(txtExcise.Text) & "', " & vbCrLf _
                & " MODE_DESPATCH='" & MainClass.AllowSingleQuote(txtDespMode.Text) & "', " & vbCrLf _
                & " INSPECTION='" & MainClass.AllowSingleQuote(txtInspection.Text) & "', " & vbCrLf _
                & " PACKING_FORWARDING='" & MainClass.AllowSingleQuote(txtPacking.Text) & "', " & vbCrLf _
                & " INSURANCE='" & MainClass.AllowSingleQuote(txtInsurance.Text) & "', " & vbCrLf _
                & " OTHERS_COND1='" & MainClass.AllowSingleQuote(txtOthCond1.Text) & "', " & vbCrLf _
                & " OTHERS_COND2='" & MainClass.AllowSingleQuote(txtOthCond2.Text) & "'," & vbCrLf _
                & " IS_APPROVED='" & mVendorApproved & "', " & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                & " AND SUPP_CUST_CODE = '" & mAccountCode & "'"

        End If
        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(mAccountCode) = False Then GoTo ErrPart

        '    SqlStr = "UPDATE FIN_SUPP_CUST_MST SET " & vbCrLf _				
        ''            & " PAYMENT_CODE='" & MainClass.AllowSingleQuote(txtPayment.Text) & "'," & vbCrLf _				
        ''            & " PAYMENT_DESC='" & MainClass.AllowSingleQuote(lblPaymentTerms.text) & "'" & vbCrLf _				
        ''            & " Where COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _				
        ''            & " AND SUPP_CUST_CODE = '" & mAccountCode & "'"				
        '				
        '    PubDBCn.Execute SqlStr				

        UpdateMain1 = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateMain1 = False
        'Resume				
    End Function
    Private Function UpdateDetail1(ByRef pAccountCode As String) As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mRateINR As Double
        Dim mRate As Double
        Dim mDisc As Double
        Dim mType As String
        Dim mOpQty As Double
        Dim mItemApproved As String
        Dim mCostingReq As String
        Dim mMRPRate As Double
        Dim mItemPartNo As String
        Dim mItemLock As String

        SqlStr = "Delete From  FIN_SUPP_CUST_DET " & vbCrLf _
            & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pAccountCode) & "'"

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemPartNo
                mItemPartNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemRateINR
                mRateINR = Val(.Text)

                .Col = ColItemRate
                If mCurrencyFactor = 1 Then
                    mRate = mRateINR
                Else
                    mRate = Val(.Text)
                End If


                .Col = ColItemMRP
                mMRPRate = Val(.Text)

                .Col = ColItemDisc
                mDisc = Val(.Text)

                .Col = ColItemApproved
                mItemApproved = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColItemLock
                mItemLock = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColCostingReq
                mCostingReq = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColPartyPer
                mOpQty = Val(.Text)

                .Col = ColItemType
                mType = VB.Left(.Text, 1)

                SqlStr = ""

                If mItemCode <> "" And mRateINR > 0 Then
                    SqlStr = " INSERT INTO FIN_SUPP_CUST_DET ( " & vbCrLf _
                        & " COMPANY_CODE , SUPP_CUST_CODE, " & vbCrLf _
                        & " ITEM_CODE, CUSTOMER_ITEM_NO, ITEM_RATE, ITEM_RATE_F," & vbCrLf _
                        & " DISC_PER, TRN_TYPE, ITEM_APPROVED, OP_QTY, " & vbCrLf _
                        & " COSTING_REQ,ITEM_MRP,ITEM_LOCK) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " '" & RsCompany.Fields("COMPANY_CODE").Value & "','" & MainClass.AllowSingleQuote(pAccountCode) & "', " & vbCrLf _
                        & " '" & mItemCode & "', '" & MainClass.AllowSingleQuote(mItemPartNo) & "', " & mRateINR & ", " & mRate & "," & vbCrLf _
                        & " " & mDisc & ",'" & mType & "','" & mItemApproved & "', " & mOpQty & ",'" & mCostingReq & "'," & mMRPRate & ",'" & mItemLock & "') "

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
        MainClass.ButtonStatus(Me, XRIGHT, RsACMMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmSuppCustDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = "Select * From FIN_SUPP_CUST_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACMMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From FIN_SUPP_CUST_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACMDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)

        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
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

        SqlStr = " SELECT A.SUPP_CUST_CODE AS CODE, A.SUPP_CUST_NAME AS NAME, " & vbCrLf & " B.DELIVERY, B.EXCISE_OTHERS, " & vbCrLf & " B.MODE_DESPATCH, B.INSPECTION, B.PACKING_FORWARDING, " & vbCrLf & " B.INSURANCE, B.OTHERS_COND1, B.OTHERS_COND2,B.PAYMENT_CODE " & vbCrLf & " FROM FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_HDR B " & vbCrLf & " WHERE A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE " & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & " ORDER BY A.SUPP_CUST_NAME"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmSuppCustDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection				
        'PvtDBCn.Open StrConn				
        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()

        On Error GoTo ClearErr

        mDataShow = False
        mAccountCode = CStr(-1)
        txtName.Text = ""
        txtCode.Text = ""
        txtCode.Enabled = True
        txtName.Enabled = True
        SprdMain.Enabled = True

        txtExcise.Text = "Extra As Applicable"
        txtDespMode.Text = "By Road"
        txtPacking.Text = "For At Our Works"
        txtOthCond1.Text = "N.A."
        txtPayment.Text = ""
        txtPayment.Enabled = False
        txtDelivery.Text = "Urgent"
        txtInspection.Text = "At Our Works"
        txtInsurance.Text = "N.A."
        txtOthCond2.Text = "N.A."
        lblPaymentTerms.Text = ""
        chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkApproved.Enabled = True
        mCurrencyFactor = 1
        txtInvoiceLineNo.Text = "0"

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)


        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_TYPE IN ('S','C')", txtName)
        Call AutoCompleteSearch("FIN_PAYTERM_MST", "PAY_TERM_DESC", "", txtPayment)


        MainClass.ButtonStatus(Me, XRIGHT, RsACMMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume				
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsACMDetail.Fields("ITEM_CODE").DefinedSize ''				
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("Item_Short_Desc", "INV_ITEM_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, IIf(mCurrencyFactor = 1, 34, 27))

            .Col = ColItemPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsACMDetail.Fields("CUSTOMER_ITEM_NO").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)

            .Col = ColItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = MainClass.SetMaxLength("PURCHASE_UOM", "INV_ITEM_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 4)


            .Col = ColItemType
            If mDataShow = False Then
                .CellType = SS_CELL_TYPE_COMBOBOX

                .TypeComboBoxEditable = False
                .TypeComboBoxList = ""
                .TypeComboBoxList = "Purchases" & Chr(9) & "Sales" & Chr(9) & "JobWork" & Chr(9) & "Others"
                .TypeComboBoxCurSel = 0
            End If

            .set_ColWidth(ColItemType, 8)

            .Col = ColItemRateINR
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8.5)

            .Col = ColItemRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8.5)
            .ColHidden = IIf(mCurrencyFactor = 1, True, False)

            .Col = ColItemMRP
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8.5)


            .Col = ColItemDisc
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColItemDisc, 5)

            .Col = ColItemApproved
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            '        .Value = vbUnchecked				
            .set_ColWidth(ColItemApproved, 5)
            .ColHidden = IIf(lblBookType.Text = "G", False, True)

            .Col = ColItemLock
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            '        .Value = vbUnchecked				
            .set_ColWidth(ColItemLock, 5)
            '.ColHidden = IIf(lblBookType.Text = "G", False, True)

            .Col = ColCostingReq
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            '        .Value = vbUnchecked				
            .set_ColWidth(ColCostingReq, 5)
            .ColHidden = IIf(lblBookType.Text = "G", True, False)

            .Col = ColPartyPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999.99")
            .TypeFloatMin = CDbl("-999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPartyPer, 6)
            .ColHidden = IIf(lblBookType.Text = "G", False, True)

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemUOM)


            If lblBookType.Text = "G" Then
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCostingReq, ColCostingReq)
                If PubSuperUser = "S" Then
                Else
                    'MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPartyPer, ColPartyPer)
                End If
            Else
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemApproved, ColItemApproved)
                'MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPartyPer, ColPartyPer)
            End If

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
            .set_ColWidth(1, 900)
            .set_ColWidth(2, 3500)
            .set_ColWidth(3, 2000)
            .set_ColWidth(4, 2000)
            .set_ColWidth(5, 2000)
            .set_ColWidth(6, 2000)
            .set_ColWidth(7, 2000)
            .set_ColWidth(8, 2000)
            .set_ColWidth(9, 2000)
            .set_ColWidth(10, 2000)
            .set_ColWidth(11, 1200)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1

        txtName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtCode.MaxLength = RsACMMain.Fields("SUPP_CUST_CODE").DefinedSize ''				

        txtExcise.MaxLength = RsACMMain.Fields("EXCISE_OTHERS").DefinedSize ''				
        txtDespMode.MaxLength = RsACMMain.Fields("MODE_DESPATCH").DefinedSize ''				
        txtPacking.MaxLength = RsACMMain.Fields("PACKING_FORWARDING").DefinedSize ''				
        txtOthCond1.MaxLength = RsACMMain.Fields("OTHERS_COND1").DefinedSize ''				
        txtPayment.MaxLength = MainClass.SetMaxLength("PAY_TERM_DESC", "FIN_PAYTERM_MST", PubDBCn)


        txtDelivery.MaxLength = RsACMMain.Fields("DELIVERY").DefinedSize ''				
        txtInspection.MaxLength = RsACMMain.Fields("INSPECTION").DefinedSize ''				
        txtInsurance.MaxLength = RsACMMain.Fields("INSURANCE").DefinedSize ''				
        txtOthCond2.MaxLength = RsACMMain.Fields("OTHERS_COND2").DefinedSize ''				
        txtInvoiceLineNo.MaxLength = RsACMMain.Fields("INVOICE_LINEITEM").Precision

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Function FieldVarification() As Boolean

        On Error GoTo err_Renamed
        Dim I As Integer
        Dim xICode As String
        Dim xPartyPer As Double

        FieldVarification = True
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldVarification = False
            Exit Function
        End If
        If txtName.Text = "" Then
            MsgInformation("Account Name is empty. Cannot Save")
            txtName.Focus()
            FieldVarification = False
            Exit Function
        End If

        If txtCode.Text = "" Then
            MsgInformation("Account Code is empty. Cannot Save")
            txtCode.Focus()
            FieldVarification = False
            Exit Function
        End If

        'If Trim(txtPayment.Text) = "" Then
        '    MsgInformation("Please Check Payment Terms.")
        '    txtPayment.Focus()
        '    FieldVarification = False
        '    Exit Function
        'End If

        If lblBookType.Text = "G" Then
            For I = 1 To SprdMain.MaxRows
                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                xICode = Trim(SprdMain.Text)

                SprdMain.Col = ColPartyPer
                xPartyPer = Val(SprdMain.Text)

                If GetProductionType(xICode) = "P" Then

                Else
                    If xICode <> "" And xPartyPer < 0 Then
                        MsgInformation("Please Check S.O.B. Cann't be Less Than Zero.")
                        MainClass.SetFocusToCell(SprdMain, I, ColPartyPer)
                        FieldVarification = False
                        Exit Function
                    End If

                    If xICode <> "" And xPartyPer > 0 Then
                        If CheckSOB(xICode, xPartyPer) = False Then
                            MainClass.SetFocusToCell(SprdMain, I, ColPartyPer)
                            FieldVarification = False
                            Exit Function
                        End If
                    End If
                End If
            Next
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Item Code is must") = False Then FieldVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColItemName, "S", "Item Name is must") = False Then FieldVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColItemUOM, "S", "Item UOM is must") = False Then FieldVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColItemType, "S", "Item Type is must") = False Then FieldVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColItemRateINR, "N", "Item Rate is must") = False Then FieldVarification = False : Exit Function

        Exit Function
err_Renamed:
        'Resume				
        MsgBox(Err.Description)
    End Function

    Private Sub frmSuppCustDetail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        RsACMMain.Close()
        'RsOpOuts.Close				
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function CheckDuplicateItem(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

        If mItemCode = "" Then CheckDuplicateItem = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                If UCase(Trim(.Text)) = UCase(Trim(mItemCode)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Item Code")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub


    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        If eventArgs.row = 0 And eventArgs.col = ColItemCode And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'", "ITEM_CODE") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName)
                    .Col = ColItemName
                    .Text = Trim(AcName1)
                End If

                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemName And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemName
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "ITEM_CODE") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemName
                    .Text = Trim(AcName)
                    .Col = ColItemCode
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemName)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F3 And mSearchKey <> "" Then
            If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColItemRateINR)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xICode As String
        Dim xPartyPer As Double

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(xICode, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If CheckDuplicateItem(xICode) = False Then
                        If FillGridRow(xICode) = False Then Exit Sub
                        '                    FormatSprdMain Row				
                        '                MainClass.SetFocusToCell SprdMain, Row, ColItemRateINR				
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                End If

            Case ColItemRateINR
                If CheckItemRate() = True Then

                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
            Case ColPartyPer
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = Trim(SprdMain.Text)
                If xICode = "" Then Exit Sub

                SprdMain.Col = ColPartyPer
                xPartyPer = Val(SprdMain.Text)


                If xICode <> "" And xPartyPer < 0 Then
                    MsgInformation("Please Check S.O.B. Cann't be Less Than Zero.")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPartyPer)
                    Exit Sub
                End If
                If xICode <> "" And xPartyPer > 0 Then
                    If CheckSOB(xICode, xPartyPer) = False Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPartyPer)
                        Exit Sub
                    End If
                End If
        End Select
        Exit Sub
ErrPart:
        '    Resume				
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckItemRate() As Boolean

        On Error GoTo ERR1
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColItemRateINR
            If Val(.Text) > 0 Then
                CheckItemRate = True
            Else
                MsgInformation("Please Enter the Rate.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemRateINR)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FillGridRow(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If mItemCode = "" Then Exit Function

        SqlStr = ""
        SqlStr = " Select ITEM_CODE,ITEM_SHORT_DESC,PURCHASE_UOM,ITEM_STD_COST,CUSTOMER_PART_NO" & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & Trim(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc

                SprdMain.Col = ColItemName
                SprdMain.Text = IIf(IsDBNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)

                SprdMain.Col = ColItemUOM
                SprdMain.Text = IIf(IsDBNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)

                If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
                    SprdMain.Col = ColItemMRP
                    SprdMain.Text = CStr(GetMRPRate("", "RATE", mItemCode, "L"))
                End If
            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function


    Private Function CheckSOB(ByRef mItemCode As String, ByRef pSuppSOB As Double) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mSupplierName As String = ""
        Dim mSOB As Double
        Dim mSOBPer As Double
        Dim mStr As String = ""
        Dim mSupplierCode As String = ""

        CheckSOB = False

        If GetProductionType(mItemCode) = "P" Then
            CheckSOB = True
            Exit Function
        End If
        mSupplierCode = ""
        If MainClass.ValidateWithMasterTable(txtCode.Text, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And SUPP_CUST_TYPE In ('S','C')") = True Then
            mSupplierCode = MasterNo
        End If

        If mSupplierCode = "" Then Exit Function

        SqlStr = ""
        SqlStr = " Select SUM(OP_QTY) AS OP_QTY" & vbCrLf _
            & " FROM FIN_SUPP_CUST_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE<>'" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            mSOB = IIf(IsDBNull(RsMisc.Fields("OP_QTY").Value), 0, RsMisc.Fields("OP_QTY").Value)
        End If

        mSOB = mSOB + pSuppSOB

        If mSOB > 105 Then
            SqlStr = " Select SUPP_CUST_NAME, OP_QTY" & vbCrLf & " FROM FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_DET CID" & vbCrLf & " WHERE CMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CMST.COMPANY_CODE=CID.COMPANY_CODE" & vbCrLf & " AND CMST.SUPP_CUST_CODE=CID.SUPP_CUST_CODE" & vbCrLf & " AND CMST.SUPP_CUST_CODE<>'" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf & " AND CID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf & " AND OP_QTY<>0"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
            If RsMisc.EOF = False Then
                Do While RsMisc.EOF = False
                    mSupplierName = IIf(IsDBNull(RsMisc.Fields("SUPP_CUST_NAME").Value), "", RsMisc.Fields("SUPP_CUST_NAME").Value)
                    mSOBPer = IIf(IsDBNull(RsMisc.Fields("OP_QTY").Value), 0, RsMisc.Fields("OP_QTY").Value)
                    mStr = IIf(mStr = "", "", mStr & ", ") & vbCrLf & mSupplierName & " - " & mSOBPer & "%"
                    RsMisc.MoveNext()
                Loop
            End If
            mStr = IIf(mStr = "", "", mStr & ", ") & vbCrLf & Trim(txtName.Text) & " - " & pSuppSOB & "%"

            MsgInformation("Total S.O.B. of Item Code : " & mItemCode & " Cann't be Greater than 105 %." & mStr)
            CheckSOB = False
        Else
            CheckSOB = True
        End If
        Exit Function
ERR1:
        CheckSOB = False
        MsgBox(Err.Description)
    End Function
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        If eventArgs.row < 1 Then Exit Sub

        SprdView.Col = 2
        SprdView.Row = eventArgs.row
        txtName.Text = Trim(SprdView.Text)
        TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(True))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    End Sub
    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub
    Private Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCurrency As String = ""

        If Trim(txtCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtCode.Text, "SUPP_CUST_CODE", "SUPP_CUST_Name", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            txtName.Text = MasterNo
            txtCode.Enabled = False
        Else
            MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", vbInformation)
            Cancel = True
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "CURRENCYNAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCurrency = Trim(MasterNo)
        End If

        If MainClass.ValidateWithMasterTable(Trim(mCurrency), "CURR_DESC", "CON_FACTOR", "FIN_CURRENCY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCurrencyFactor = Trim(MasterNo)
        End If

        SprdMain.Row = 0
        SprdMain.Col = ColItemRate
        SprdMain.Text = "Item Rate (" & mCurrency & ")"


        If MODIFYMode = True And RsACMMain.EOF = False Then mAccountCode = RsACMMain.Fields("SUPP_CUST_CODE").Value
        SqlStr = "Select * From FIN_SUPP_CUST_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACMMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsACMMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From FIN_SUPP_CUST_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & mAccountCode & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACMMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function MakeSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        MakeSQL = "Select * From " & vbCrLf _
            & " FIN_SUPP_CUST_HDR IH, FIN_SUPP_CUST_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=ID.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY ID.ITEM_CODE"

        Exit Function
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
    End Function
    Private Sub txtDelivery_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDelivery.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDelivery_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDelivery.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDelivery.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDespMode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDespMode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDespMode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDespMode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDespMode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtExcise_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExcise.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExcise_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExcise.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtExcise.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtInspection_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspection.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInspection_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInspection.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInspection.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtInsurance_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInsurance.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInsurance_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInsurance.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInsurance.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtInvoiceLineNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvoiceLineNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInvoiceLineNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInvoiceLineNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub
    Private Sub TxtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        On Error GoTo ShowErrPart
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String = ""
        Dim mCurrency As String = ""

        If Trim(txtName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
            txtCode.Text = xAcctCode
            txtCode.Enabled = False
        Else
            MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", vbInformation)
            Cancel = True
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(xAcctCode, "SUPP_CUST_CODE", "CURRENCYNAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCurrency = Trim(MasterNo)
        End If

        If MainClass.ValidateWithMasterTable(Trim(mCurrency), "CURR_DESC", "CON_FACTOR", "FIN_CURRENCY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCurrencyFactor = Trim(MasterNo)
        End If

        SprdMain.Row = 0
        SprdMain.Col = ColItemRate
        SprdMain.Text = "Item Rate (" & mCurrency & ")"

        If MODIFYMode = True And RsACMMain.EOF = False Then mAccountCode = RsACMMain.Fields("SUPP_CUST_CODE").Value
        SqlStr = "Select * From FIN_SUPP_CUST_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND LTRIM(RTRIM(SUPP_CUST_CODE))='" & MainClass.AllowSingleQuote(xAcctCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACMMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsACMMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From FIN_SUPP_CUST_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & mAccountCode & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACMMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mAccountName As String = ""

        Clear1()
        If Not RsACMMain.EOF Then

            mAccountCode = IIf(IsDBNull(RsACMMain.Fields("SUPP_CUST_CODE").Value), -1, RsACMMain.Fields("SUPP_CUST_CODE").Value)

            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = MasterNo
            End If

            txtName.Text = mAccountName
            txtCode.Text = Trim(IIf(IsDBNull(RsACMMain.Fields("SUPP_CUST_CODE").Value), "", RsACMMain.Fields("SUPP_CUST_CODE").Value))
            txtCode.Enabled = False
            txtName.Enabled = False

            txtExcise.Text = IIf(IsDBNull(RsACMMain.Fields("EXCISE_OTHERS").Value), "", RsACMMain.Fields("EXCISE_OTHERS").Value)
            txtInvoiceLineNo.Text = IIf(IsDBNull(RsACMMain.Fields("INVOICE_LINEITEM").Value), 0, RsACMMain.Fields("INVOICE_LINEITEM").Value)
            txtDespMode.Text = IIf(IsDBNull(RsACMMain.Fields("MODE_DESPATCH").Value), "", RsACMMain.Fields("MODE_DESPATCH").Value)
            txtPacking.Text = IIf(IsDBNull(RsACMMain.Fields("PACKING_FORWARDING").Value), "", RsACMMain.Fields("PACKING_FORWARDING").Value)
            txtOthCond1.Text = IIf(IsDBNull(RsACMMain.Fields("OTHERS_COND1").Value), "", RsACMMain.Fields("OTHERS_COND1").Value)
            txtDelivery.Text = IIf(IsDBNull(RsACMMain.Fields("DELIVERY").Value), "", RsACMMain.Fields("DELIVERY").Value)
            txtInspection.Text = IIf(IsDBNull(RsACMMain.Fields("INSPECTION").Value), "", RsACMMain.Fields("INSPECTION").Value)
            txtInsurance.Text = IIf(IsDBNull(RsACMMain.Fields("INSURANCE").Value), "", RsACMMain.Fields("INSURANCE").Value)
            txtOthCond2.Text = IIf(IsDBNull(RsACMMain.Fields("OTHERS_COND2").Value), "", RsACMMain.Fields("OTHERS_COND2").Value)

            chkApproved.CheckState = IIf(RsACMMain.Fields("IS_APPROVED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            If chkApproved.CheckState = System.Windows.Forms.CheckState.Checked Then chkApproved.Enabled = False

            If MainClass.ValidateWithMasterTable(txtCode.Text, "SUPP_CUST_CODE", "PAYMENT_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblPaymentTerms.Text = MasterNo
            Else
                lblPaymentTerms.Text = ""
            End If

            If MainClass.ValidateWithMasterTable(lblPaymentTerms.Text, "PAY_TERM_CODE", "PAY_TERM_DESC", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtPayment.Text = MasterNo
            End If

            Call ShowDetail1((RsACMMain.Fields("SUPP_CUST_CODE").Value))

        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemUOM)
        MainClass.ButtonStatus(Me, XRIGHT, RsACMMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        mDataShow = False
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume				
    End Sub
    Private Sub ShowDetail1(ByRef mSuppCode As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String = ""
        Dim mItemDesc As String = ""
        Dim mItemUOM As String = ""
        Dim mItemType As String = ""
        Dim mCurrency As String = ""

        If MainClass.ValidateWithMasterTable(mSuppCode, "SUPP_CUST_CODE", "CURRENCYNAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCurrency = Trim(MasterNo)
        End If

        If MainClass.ValidateWithMasterTable(Trim(mCurrency), "CURR_DESC", "CON_FACTOR", "FIN_CURRENCY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCurrencyFactor = Trim(MasterNo)
        End If

        SprdMain.Row = 0
        SprdMain.Col = ColItemRate
        SprdMain.Text = "Item Rate (" & mCurrency & ")"


        SqlStr = ""
        SqlStr = " Select * " & vbCrLf & " FROM FIN_SUPP_CUST_DET " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " And SUPP_CUST_CODE='" & mSuppCode & "'" & vbCrLf & " Order By ITEM_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACMDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsACMDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1				
            I = 1
            '        .MoveFirst				

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = mItemCode

                '            If mItemCode = "EXP703" Then MsgBox mItemCode				

                SprdMain.Col = ColItemName
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColItemUOM
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "PURCHASE_UOM", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemUOM = MasterNo
                SprdMain.Text = mItemUOM

                SprdMain.Col = ColItemPartNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("CUSTOMER_ITEM_NO").Value), "", .Fields("CUSTOMER_ITEM_NO").Value))

                SprdMain.Col = ColItemType
                SprdMain.CellType = SS_CELL_TYPE_COMBOBOX
                SprdMain.TypeComboBoxEditable = False
                SprdMain.TypeComboBoxList = ""
                SprdMain.TypeComboBoxList = "Purchases" & Chr(9) & "Sales" & Chr(9) & "JobWork" & Chr(9) & "Others"

                mItemType = IIf(IsDBNull(.Fields("TRN_TYPE").Value), "", .Fields("TRN_TYPE").Value)

                Select Case mItemType
                    Case "P"
                        SprdMain.TypeComboBoxCurSel = 0
                    Case "S"
                        SprdMain.TypeComboBoxCurSel = 1
                    Case "J"
                        SprdMain.TypeComboBoxCurSel = 2
                    Case "O"
                        SprdMain.TypeComboBoxCurSel = 3
                End Select


                SprdMain.Col = ColItemRateINR
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))

                SprdMain.Col = ColItemRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE_F").Value), 0, .Fields("ITEM_RATE_F").Value)))

                SprdMain.Col = ColItemMRP
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_MRP").Value), 0, .Fields("ITEM_MRP").Value)))

                SprdMain.Col = ColItemDisc
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("DISC_PER").Value), 0, .Fields("DISC_PER").Value)))

                SprdMain.Col = ColItemApproved
                SprdMain.Value = IIf(.Fields("Item_Approved").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                SprdMain.Col = ColItemLock
                SprdMain.Value = IIf(.Fields("ITEM_LOCK").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


                SprdMain.Col = ColCostingReq
                SprdMain.Value = IIf(.Fields("COSTING_REQ").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                SprdMain.Col = ColPartyPer
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("OP_QTY").Value), 0, .Fields("OP_QTY").Value)))

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        mDataShow = True
        FormatSprdMain(-1)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume				
    End Sub
    Private Sub txtOthCond1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOthCond1.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOthCond1_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOthCond1.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOthCond1.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOthCond2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOthCond2.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOthCond2_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOthCond2.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOthCond2.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPacking_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPacking.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPacking_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPacking.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPacking.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPayment_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPayment.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPayment_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPayment.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPayment.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPayment_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPayment.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

    End Sub


    Private Sub txtPayment_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPayment.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(txtPayment.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtPayment.Text, "PAY_TERM_DESC", "PAY_TERM_CODE", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblPaymentTerms.Text = MasterNo
        Else
            MsgBox("Invalid Payment Desc.", vbInformation)
            Cancel = True
            Exit Sub
        End If


        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdsearch_Click(sender As Object, e As EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster(txtName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "SUPP_CUST_CITY", "SUPP_CUST_STATE", SqlStr) = True Then
            txtName.Text = AcName
            TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
            If txtCode.Enabled = True Then txtCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub

    Private Sub cmdPaySearch_Click(sender As Object, e As EventArgs) Handles cmdPaySearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtPayment.Text, "FIN_PAYTERM_MST", "PAY_TERM_DESC", "PAY_TERM_CODE", , , SqlStr) = True Then
            txtPayment.Text = AcName
            txtPayment_Validating(txtPayment, New System.ComponentModel.CancelEventArgs(False))
            txtPayment.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub

    Private Sub frmSuppCustDetail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        If KeyAscii = 6 Then
            SprdMain.Row = 1
            SprdMain.Row2 = SprdMain.MaxRows
            SprdMain.Col = 1
            SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
            SprdMain.BlockMode = True
            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            SprdMain.BlockMode = False

            mSearchKey = ""
            cntSearchRow = 1
            cntSearchCol = 1
            mSearchKey = InputBox("Search :", "Search", mSearchKey)
            If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColItemRate)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub

    Private Sub SprdMain_KeyPressEvent(sender As Object, e As _DSpreadEvents_KeyPressEvent) Handles SprdMain.KeyPressEvent
        'Dim KeyAscii As Short = Asc(e.keyAscii)

        'KeyAscii = MainClass.SetNumericField(KeyAscii)
        'EventArgs.KeyChar = Chr(KeyAscii)
        'If KeyAscii = 67 Then
        '    EventArgs.Handled = True
        'End If

        If e.keyAscii = 6 Then
            SprdMain.Row = 1
            SprdMain.Row2 = SprdMain.MaxRows
            SprdMain.Col = 1
            SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
            SprdMain.BlockMode = True
            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            SprdMain.BlockMode = False

            mSearchKey = ""
            cntSearchRow = 1
            cntSearchCol = 1
            mSearchKey = InputBox("Search :", "Search", mSearchKey)
            If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColItemRate)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub
End Class
