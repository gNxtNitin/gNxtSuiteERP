Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmQuotation
    Inherits System.Windows.Forms.Form
    Dim RsQuotationMain As ADODB.Recordset
    Dim RsQuotationDetail As ADODB.Recordset
    'Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Dim CurMKey As String
    Dim SqlStr As String = ""

    Private Const ConRowHeight As Short = 22

    Private Const ColIndentNo As Short = 1
    Private Const ColIndentSNo As Short = 2
    Private Const ColItemCode As Short = 3
    Private Const ColItemDesc As Short = 4
    Private Const ColUnit As Short = 5
    Private Const ColPrice As Short = 6
    Private Const ColDiscount As Short = 7
    Private Const ColDelivery As Short = 8
    Private Const ColCredibility As Short = 9
    Private Const ColRemarks As Short = 10
    Private Const ColAppStatus As Short = 11

    Private Sub cboStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtQuotationNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsQuotationMain.EOF = False Then RsQuotationMain.MoveFirst()
            Show1()
            txtQuotationNo.Enabled = True
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If ValidateBranchLocking((txtQuotationDate.Text)) = True Then
            Exit Sub
        End If

        If txtQuotationNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsQuotationMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PUR_QUOTATION_HDR", (txtQuotationNo.Text), RsQuotationMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PUR_QUOTATION_HDR", "AUTO_KEY_QUOT", (LblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM PUR_QUOTATION_DET WHERE AUTO_KEY_QUOT=" & Val(LblMKey.Text) & "")
                PubDBCn.Execute("DELETE FROM PUR_QUOTATION_HDR WHERE AUTO_KEY_QUOT=" & Val(LblMKey.Text) & "")
                PubDBCn.CommitTrans()
                RsQuotationMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        RsQuotationMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='S'"

        If MainClass.SearchGridMaster((txtSupplierName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtSupplierName.Text = AcName
            txtSupplierName_Validating(txtSupplierName, New System.ComponentModel.CancelEventArgs(False))
            txtSupplierName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsQuotationMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtQuotationNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonIndent(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonIndent(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonIndent(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim mBranchCode As Integer
        Dim mCategoryCode As Integer
        Dim mRPTName As String = ""

        '    Report1.Reset
        '    SqlStr = ""
        '    Screen.MousePointer = 11
        '
        '    Call SelectQry(SqlStr)
        '    Screen.MousePointer = 0
        '
        '    mSubTitle = ""
        '
        '    mRPTName = "\reports\PrintPPO.rpt"
        '    mTitle = "Pre-Purchase Order (Indigineious)"
        '
        '    Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle, "P")

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQry(ByRef mSqlStr As String) As String
        mSqlStr = " SELECT " & vbCrLf & " PUR_QUOTATION_HDR.PONO, PUR_QUOTATION_HDR.PODATE, PUR_QUOTATION_HDR.PARTYREF, " & vbCrLf & " PUR_QUOTATION_HDR.REFDATE, PUR_QUOTATION_HDR.EXCISEDUTY, PUR_QUOTATION_HDR.SALESTAX, " & vbCrLf & " PUR_QUOTATION_HDR.INSURANCE, PUR_QUOTATION_HDR.FREIGHT, PUR_QUOTATION_HDR.PACKINGFORWARDING, " & vbCrLf & " PUR_QUOTATION_HDR.ADVANCE, PUR_QUOTATION_HDR.DESPATCHMODE, PUR_QUOTATION_HDR.PAYMENTTERMS, " & vbCrLf & " PUR_QUOTATION_HDR.WARRANTY, PUR_QUOTATION_HDR.DLVDATE, PUR_QUOTATION_HDR.DLVSCHDL, PUR_QUOTATION_HDR.REMARKS, " & vbCrLf & " ACM.NAME, ACM.ADDRESS1, ACM.ADDRESS2, ACM.CITY, ACM.PINCODE, " & vbCrLf & " PUR_QUOTATION_DET.QTY, PUR_QUOTATION_DET.UNIT, PUR_QUOTATION_DET.RATE, PUR_QUOTATION_DET.DISCOUNT,PUR_QUOTATION_DET.DLVPOINT, " & vbCrLf & " SUPPLIERRATES.SUPPLIERITEMNAME " & vbCrLf & " FROM PUR_QUOTATION_HDR,PUR_QUOTATION_DET,ACM ,SUPPLIERRATES " & vbCrLf & " WHERE PUR_QUOTATION_HDR.MKEY=PPODETAIL.MKEY " & vbCrLf & " AND PUR_QUOTATION_HDR.SUPPLIERCODE=ACM.CODE(+) " & vbCrLf & " AND PUR_QUOTATION_HDR.CompanyCode = " & RsCompany.Fields("CompanyCode").Value & "" & vbCrLf & " AND PUR_QUOTATION_HDR.BranchCode=" & RsCompany.Fields("BranchCode").Value & "" & vbCrLf & " AND PUR_QUOTATION_HDR.FYNo=" & RsCompany.Fields("FYNO").Value & "" & vbCrLf & " AND PUR_QUOTATION_HDR.MKey='" & RsQuotationMain.Fields("mKey").Value & "' " & vbCrLf & " AND PUR_QUOTATION_HDR.SUPPLIERCODE=SUPPLIERRATES.SUPPLIERCODE " & vbCrLf & " AND PUR_QUOTATION_DET.ITEMCODE=SUPPLIERRATES.ITEMCODE(+) " & vbCrLf & " ORDER BY PUR_QUOTATION_DET.SubRowNo"
        SelectQry = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mFlag As String)
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & mRPTName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1

    End Sub

    Private Sub ShowTermsReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & mRPTName
        'Report1.SQLQuery = mSqlStr
        'Report1.WindowShowGroupTree = False
        Report1.Action = 1

    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtQuotationNo_Validating(txtQuotationNo, New System.ComponentModel.CancelEventArgs(False))
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
    Public Sub FrmQuotation_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "Quotation - Purchase"

        SqlStr = ""
        SqlStr = "Select * from PUR_QUOTATION_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsQuotationMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from PUR_QUOTATION_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsQuotationDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call SetTextLengths()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub FrmQuotation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmQuotation_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        txtQuotationDate.Enabled = False
        ADDMode = False
        MODIFYMode = False
        Call SetMainFormCordinate(Me)
        Me.Top = 0
        Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        cboStatus.Items.Clear()
        cboStatus.Items.Add("No")
        cboStatus.Items.Add("Yes")
        cboStatus.SelectedIndex = 0

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetTextLengths()

        On Error GoTo SetTextLengthsErr
        txtQuotationNo.Maxlength = RsQuotationMain.Fields("AUTO_KEY_QUOT").DefinedSize
        txtQuotationDate.Maxlength = RsQuotationMain.Fields("QUOTATION_DATE").DefinedSize - 6
        txtSupplierName.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtRemarks.Maxlength = RsQuotationMain.Fields("REMARKS").DefinedSize

        Exit Sub
SetTextLengthsErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub Clear1()

        SqlStr = ""
        LblMKey.Text = ""
        txtQuotationNo.Text = ""
        txtQuotationDate.Text = VB6.Format(RunDate, "DD-MM-YYYY")
        txtSupplierName.Text = ""

        txtRemarks.Text = ""
        cboStatus.SelectedIndex = 0
        cboStatus.Enabled = True
        lblCode.Text = ""
        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "", txtSupplierName)

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsQuotationMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mDate As String

        FraCmd.Enabled = True
        If Not RsQuotationMain.EOF Then
            LblMKey.Text = RsQuotationMain.Fields("AUTO_KEY_QUOT").Value
            mDate = RsQuotationMain.Fields("QUOTATION_DATE").Value  '' IIf(IsDBNull(RsQuotationMain.Fields("QUOTATION_DATE").Value), "", RsQuotationMain.Fields("QUOTATION_DATE").Value)

            txtQuotationNo.Text = VB6.Format(IIf(IsDbNull(RsQuotationMain.Fields("AUTO_KEY_QUOT").Value), "", RsQuotationMain.Fields("AUTO_KEY_QUOT").Value), "000000")

            txtQuotationDate.Text = VB6.Format(mDate, "DD-MM-YYYY")

            lblCode.Text = IIf(IsDbNull(RsQuotationMain.Fields("SUPP_CUST_CODE").Value), "", RsQuotationMain.Fields("SUPP_CUST_CODE").Value)

            If MainClass.ValidateWithMasterTable((lblCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtSupplierName.Text = MasterNo
            End If

            txtRemarks.Text = IIf(IsDbNull(RsQuotationMain.Fields("REMARKS").Value), "", RsQuotationMain.Fields("REMARKS").Value)
            cboStatus.SelectedIndex = IIf(RsQuotationMain.Fields("QUOTATION_STATUS").Value = "N", 0, 1)

            If VB.Left(cboStatus.Text, 1) = "Y" Then
                cboStatus.Enabled = False
            End If

            Call ShowDetail1()

        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsQuotationMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        txtQuotationNo.Enabled = True
        SprdMain.Enabled = True
        Exit Sub
ShowErrPart:

        If Err.Number = -2147418113 Then
            RsQuotationMain.Requery()
            Resume
        End If
        MsgBox(Err.Description, Err.Number)

    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If ValidateBranchLocking((txtQuotationDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsQuotationMain.EOF = True Then Exit Function

        If MODIFYMode = True And Trim(txtQuotationNo.Text) = "" Then
            MsgInformation("Quotation No. is Blank")
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtQuotationDate.Text) = "" Then
            MsgInformation(" Quotation Date is empty. Cannot Save")
            txtQuotationDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtQuotationDate.Text) <> "" Then
            If IsDate(txtQuotationDate.Text) = False Then
                MsgInformation(" Invalid Quotation Date. Cannot Save")
                txtQuotationDate.Focus()
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
            lblCode.Text = MasterNo
        Else
            MsgInformation("Invalid Supplier Name. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If VB.Left(cboStatus.Text, 1) = "Y" And cboStatus.Enabled = False Then
            MsgInformation("Quoation Already Freezed")
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColIndentNo, "S", "Please Check Indent No.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColIndentSNo, "N", "Please Check Indent Serial No.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColUnit, "S", "Please Check Unit.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Please Check Item Code.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColItemDesc, "S", "Please Check Item Description.") = False Then FieldsVarification = False
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim mQuotNo As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        mQuotNo = Val(txtQuotationNo.Text)
        If Val(txtQuotationNo.Text) = 0 Then
            mQuotNo = AutoGenQuotationNoSeq()
        End If

        If ADDMode = True Then
            LblMKey.Text = CStr(mQuotNo)
            SqlStr = " INSERT INTO PUR_QUOTATION_HDR ( " & vbCrLf _
                & " AUTO_KEY_QUOT, COMPANY_CODE, QUOTATION_DATE, " & vbCrLf _
                & " SUPP_CUST_CODE, QUOTATION_STATUS, REMARKS) "

            SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf _
                & " " & mQuotNo & ", " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtQuotationDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((lblCode.Text)) & "', " & vbCrLf _
                & " '" & VB.Left(cboStatus.Text, 1) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "') "
        End If

        If MODIFYMode = True Then
            SqlStr = " UPDATE PUR_QUOTATION_HDR SET " & vbCrLf _
                & " AUTO_KEY_QUOT=" & mQuotNo & ", " & vbCrLf _
                & " QUOTATION_DATE=TO_DATE('" & VB6.Format(txtQuotationDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((lblCode.Text)) & "', " & vbCrLf _
                & " QUOTATION_STATUS='" & VB.Left(cboStatus.Text, 1) & "', " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "' " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND AUTO_KEY_QUOT =" & Val(LblMKey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1 = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        txtQuotationNo.Text = CStr(mQuotNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsQuotationMain.Requery()
        RsQuotationDetail.Requery()
        MsgBox(Err.Description)
        ''Resume
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
        MainClass.ButtonStatus(Me, XRIGHT, RsQuotationMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FrmQuotation_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsQuotationDetail.Close()
        RsQuotationMain.Close()
        'PvtDBCn.Close
        RsQuotationDetail = Nothing
        RsQuotationMain = Nothing
        'Set PvtDBCn = Nothing
    End Sub


    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtQuotationNo.Text = SprdView.Text

        txtQuotationNo_Validating(txtQuotationNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        Try
            If eventArgs.row = 0 And eventArgs.col = ColIndentNo Then
                Dim mSqlStr As String = ""

                SqlStr = " SELECT IH.AUTO_KEY_INDENT, IH.INDENT_DATE, IH.DEPT_CODE, ID.SERIAL_NO, ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, ID.ITEM_UOM, ID.REQ_QTY " & vbCrLf _
                        & " FROM PUR_INDENT_HDR IH, PUR_INDENT_DET ID, INV_ITEM_MST IMST " & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND IH.AUTO_KEY_INDENT=ID.AUTO_KEY_INDENT " & vbCrLf _
                        & " AND IH.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf _
                        & " AND ID.ITEM_CODE=IMST.ITEM_CODE " & vbCrLf _
                        & " AND IH.HOD_EMP_CODE Is Not NULL"

                SqlStr = SqlStr & vbCrLf & " And IH.APPROVAL_STATUS = 'Y' AND ID.IS_REJECTED='N'"

                SqlStr = SqlStr & vbCrLf & " AND GETPOIndentQty(IH.COMPANY_CODE,IH.AUTO_KEY_INDENT,ID.ITEM_CODE) = 0"
                ' & " ID.REQ_QTY, GETPOIndentQty(IH.COMPANY_CODE,IH.AUTO_KEY_INDENT,ID.ITEM_CODE) AS POQTY, IH.IND_EMP_CODE, IH.DIV_CODE" & vbCrLf _

                With SprdMain
                    .Row = .ActiveRow
                    .Col = ColIndentNo
                    If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                        'If MainClass.SearchGridMaster(.Text, "PUR_INDENT_DET", "AUTO_KEY_INDENT", "SERIAL_NO", "ITEM_CODE", , "SUBSTR(AUTO_KEY_INDENT,LENGTH(AUTO_KEY_INDENT)-1,2)=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Row = .ActiveRow
                        .Col = ColIndentNo
                        .Text = AcName

                        .Col = ColIndentSNo
                        .Text = AcName3

                        .Col = ColItemCode
                        .Text = AcName4

                        .Col = ColItemDesc
                        .Text = AcName5

                        .Col = ColUnit
                        .Text = AcName6

                    End If
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColIndentNo)
                End With
            End If

            Dim mItemCode As String
            Dim DelStatus As Boolean
            If eventArgs.col = 0 And eventArgs.row > 0 Then

                SprdMain.Row = eventArgs.row

                SprdMain.Col = ColIndentNo
                mItemCode = SprdMain.Text

                If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                    MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColIndentNo, DelStatus)
                    MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColIndentNo Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColIndentNo, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColIndentSNo Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColIndentSNo, 0))
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ERR1
        Dim mIndentNo As Double
        Dim mIndentSNo As Integer


        If eventArgs.NewRow = -1 Then Exit Sub
        With SprdMain
            .Row = .ActiveRow
            .Col = ColIndentNo
            If Trim(.Text) = "" Then Exit Sub
            Select Case eventArgs.Col
                Case ColIndentNo
                    .Row = .ActiveRow
                    .Col = ColIndentNo
                    mIndentNo = CDbl(.Text)

                    .Col = ColIndentSNo
                    mIndentSNo = CInt(.Text)

                    If CheckDuplicateItem(mIndentNo, mIndentSNo) = False Then
                        Call InsertItemDetIntoGrid(mIndentNo, mIndentSNo)
                    End If
                Case ColPrice
                    If CheckQty(eventArgs.Col, eventArgs.Row) = True Then
                        MainClass.AddBlankSprdRow(SprdMain, ColIndentNo, ConRowHeight)
                        FormatSprdMain((SprdMain.MaxRows))
                    End If
                Case ColDiscount
                    .Row = .ActiveRow
                    .Col = ColDiscount
                    If CDbl(.Text) > 100 Then
                        MsgInformation("Discount Cann't be Greater Than 100%")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColDiscount)
                        Exit Sub
                    End If
            End Select
        End With

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSupplierName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSupplierName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub


    Private Sub txtSupplierName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplierName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplierName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSupplierName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplierName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub


    Private Sub txtSupplierName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSupplierName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtSupplierName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='S'") = True Then
            lblCode.Text = MasterNo
        Else
            MsgBox("Invalid Supplier Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub



    Private Sub txtQuotationDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtQuotationDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtQuotationNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtQuotationNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtQuotationNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtQuotationNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtQuotationNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtQuotationNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mIndentNo As Double

        If Trim(txtQuotationNo.Text) = "" Then GoTo EventExitSub


        If Len(txtQuotationNo.Text) < 6 Then
            txtQuotationNo.Text = Val(txtQuotationNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mIndentNo = Val(txtQuotationNo.Text)

        If MODIFYMode = True And RsQuotationMain.BOF = False Then xMkey = RsQuotationMain.Fields("AUTO_KEY_QUOT").Value

        SqlStr = "SELECT * FROM PUR_QUOTATION_HDR " & vbCrLf _
           & " WHERE AUTO_KEY_QUOT='" & MainClass.AllowSingleQuote(UCase(CStr(mIndentNo))) & "'" & vbCrLf _
           & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
           & " AND SUBSTR(AUTO_KEY_QUOT,LENGTH(AUTO_KEY_QUOT)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsQuotationMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsQuotationMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Indent No. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PUR_QUOTATION_HDR WHERE AUTO_KEY_QUOT=" & Val(xMkey) & "" & vbCrLf _
                    & " AND SUBSTR(AUTO_KEY_QUOT,LENGTH(AUTO_KEY_QUOT)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsQuotationMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FrmQuotation_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf _
            & " IH.AUTO_KEY_QUOT, IH.QUOTATION_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, " & vbCrLf _
            & " ID.ITEM_PRICE, ID.DISCOUNT, ID.DELIVERY_TIME, ID.CREDIBILITY, ID.REMARKS ITEM_REMARKS, DECODE(ID.QUOTATION_APP,'Y','APPROVED','NOT APPROVED') AS QUOTATION_APP," & vbCrLf _
            & " DECODE(QUOTATION_STATUS,'Y','YES','NO') AS STATUS, IH.REMARKS " & vbCrLf _
            & " FROM PUR_QUOTATION_HDR IH, PUR_QUOTATION_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST " & vbCrLf _
            & " WHERE IH.AUTO_KEY_QUOT=ID.AUTO_KEY_QUOT" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " And IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
            & " And ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
            & " And IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " And SUBSTR(IH.AUTO_KEY_QUOT,LENGTH(IH.AUTO_KEY_QUOT)-5,4)=" & RsCompany.Fields("FYEAR").Value & " ORDER BY IH.AUTO_KEY_QUOT"


        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 20)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 14)
            .set_ColWidth(2, 14)
            .set_ColWidth(3, 26)
            .set_ColWidth(4, 10)
            .set_ColWidth(5, 20)


            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' .OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub


    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColIndentNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsQuotationDetail.Fields("AUTO_KEY_INDENT").DefinedSize
            .set_ColWidth(ColIndentNo, 10)

            .Col = ColIndentSNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsQuotationDetail.Fields("SERIAL_NO").DefinedSize
            .set_ColWidth(ColIndentSNo, 5)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColItemCode, 9)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemDesc, 25)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColUnit, 4)

            .Col = ColPrice
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999.99")
            .TypeFloatMin = CDbl("-999999.99")
            .set_ColWidth(ColPrice, 7)
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .ColsFrozen = True
            .ColsFrozen = ColPrice

            .Col = ColDiscount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("99999.99")
            .TypeFloatMin = CDbl("-999999.99")
            .set_ColWidth(ColDiscount, 7)
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC


            .Col = ColDelivery
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColDelivery, 12)

            .Col = ColCredibility
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColCredibility, 12)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 12)

            .Col = ColAppStatus
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColAppStatus, 4)

            .ColHidden = True

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIndentSNo, ColUnit)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAppStatus, ColAppStatus)
        End With
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
    End Sub


    Private Function UpdateDetail1() As Boolean
        On Error GoTo UpdateDetail1
        Dim I As Short
        Dim mRow As Short
        Dim mIdentNo As Double
        Dim mIdentSNo As Integer
        Dim mPrice As Double
        Dim mDiscount As String
        Dim mDelivery As String
        Dim mRemarks As String
        Dim mCredibility As String
        Dim mAppStatus As String
        Dim mItemCode As String

        SqlStr = "DELETE FROM PUR_QUOTATION_DET WHERE AUTO_KEY_QUOT=" & Val(LblMKey.Text) & ""
        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColIndentNo
                mIdentNo = Val(.Text)

                .Col = ColIndentSNo
                mIdentSNo = Val(.Text)

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColPrice
                mPrice = Val(.Text)

                .Col = ColDiscount
                mDiscount = Val(.Text)

                .Col = ColDelivery
                mDelivery = .Text

                .Col = ColCredibility
                mCredibility = .Text

                .Col = ColRemarks
                mRemarks = .Text

                .Col = ColAppStatus
                mAppStatus = IIf(Trim(.Text) = "", "N", Trim(.Text))

                SqlStr = ""

                SqlStr = " INSERT INTO PUR_QUOTATION_DET ( " & vbCrLf _
                    & " AUTO_KEY_QUOT, AUTO_KEY_INDENT, SERIAL_NO, " & vbCrLf _
                    & " ITEM_PRICE, DISCOUNT, " & vbCrLf _
                    & " DELIVERY_TIME, CREDIBILITY, " & vbCrLf _
                    & " REMARKS, QUOTATION_APP, COMPANY_CODE, ITEM_CODE) VALUES ( "

                SqlStr = SqlStr & vbCrLf _
                    & " " & Val(LblMKey.Text) & ", " & vbCrLf _
                    & " " & Val(CStr(mIdentNo)) & "," & vbCrLf _
                    & " " & Val(CStr(mIdentSNo)) & "," & vbCrLf _
                    & " " & Val(CStr(mPrice)) & "," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mDiscount) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mDelivery) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mCredibility) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mRemarks) & "','" & mAppStatus & "'," & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(mItemCode) & "')"

                PubDBCn.Execute(SqlStr)
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        MsgBox(Err.Description)
        UpdateDetail1 = False
    End Function
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim I As Integer
        Dim mItemCode As String
        Dim mIndentNo As Double
        Dim mIndentSNo As Integer

        SqlStr = ""
        MainClass.ClearGrid(SprdMain)
        SqlStr = "SELECT * " & vbCrLf _
            & " FROM PUR_QUOTATION_DET " & vbCrLf _
            & " WHERE AUTO_KEY_QUOT=" & LblMKey.Text & "" & vbCrLf _
            & " ORDER BY AUTO_KEY_INDENT,SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsQuotationDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsQuotationDetail

            If .EOF = True Then Exit Sub
            I = 0
            .MoveFirst()
            Do While Not .EOF
                I = I + 1
                SprdMain.MaxRows = SprdMain.MaxRows + 1
                SprdMain.Row = I

                SprdMain.Col = ColIndentNo
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("AUTO_KEY_INDENT").Value), 0, .Fields("AUTO_KEY_INDENT").Value)))
                mIndentNo = IIf(IsDbNull(.Fields("AUTO_KEY_INDENT").Value), "", .Fields("AUTO_KEY_INDENT").Value)

                SprdMain.Col = ColIndentSNo
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("SERIAL_NO").Value), 0, .Fields("SERIAL_NO").Value)))
                mIndentSNo = IIf(IsDbNull(.Fields("SERIAL_NO").Value), "", .Fields("SERIAL_NO").Value)

                SprdMain.Col = ColPrice
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_PRICE").Value), 0, .Fields("ITEM_PRICE").Value)))

                SprdMain.Col = ColDiscount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("DISCOUNT").Value), 0, .Fields("DISCOUNT").Value)))

                SprdMain.Col = ColDelivery
                SprdMain.Text = IIf(IsDbNull(.Fields("DELIVERY_TIME").Value), "", .Fields("DELIVERY_TIME").Value)

                SprdMain.Col = ColCredibility
                SprdMain.Text = IIf(IsDbNull(.Fields("CREDIBILITY").Value), "", .Fields("CREDIBILITY").Value)

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                SprdMain.Col = ColAppStatus
                SprdMain.Text = IIf(IsDBNull(.Fields("QUOTATION_APP").Value), "N", .Fields("QUOTATION_APP").Value)

                SprdMain.Col = ColItemCode
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_CODE").Value), 0, .Fields("ITEM_CODE").Value)
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), 0, .Fields("ITEM_CODE").Value)

                'SqlStr = "SELECT A.ITEM_CODE,B.ITEM_SHORT_DESC,A.ITEM_UOM " & vbCrLf _
                '    & " FROM PUR_INDENT_DET A, INV_ITEM_MST B " & vbCrLf _
                '    & " WHERE B.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                '    & " AND A.ITEM_CODE=B.ITEM_CODE" & vbCrLf _
                '    & " AND A.AUTO_KEY_INDENT=" & mIndentNo & "" & vbCrLf _
                '    & " AND A.SERIAL_NO=" & mIndentSNo & ""

                SqlStr = "SELECT ITEM_CODE,ITEM_SHORT_DESC,PURCHASE_UOM AS ITEM_UOM " & vbCrLf _
                    & " FROM INV_ITEM_MST B " & vbCrLf _
                    & " WHERE B.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND ITEM_CODE='" & mItemCode & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    'SprdMain.Col = ColItemCode
                    'SprdMain.Text = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), 0, RsTemp.Fields("ITEM_CODE").Value)

                    SprdMain.Col = ColItemDesc
                    SprdMain.Text = IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                    SprdMain.Col = ColUnit
                    SprdMain.Text = IIf(IsDbNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                End If

                .MoveNext()
            Loop

        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Function AutoGenQuotationNoSeq() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Integer
        Dim mMaxValue As String
        mAutoGen = 1

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_QUOT)  " & vbCrLf _
            & " FROM PUR_QUOTATION_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_QUOT,LENGTH(AUTO_KEY_QUOT)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With

        AutoGenQuotationNoSeq = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function
    Private Sub InsertItemDetIntoGrid(ByRef pIndentNo As Double, ByRef pIndentSno As Integer)

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing

        If Val(CStr(pIndentNo)) = 0 Then Exit Sub
        SqlStr = ""
        SqlStr = "SELECT A.AUTO_KEY_INDENT, SERIAL_NO," & vbCrLf _
            & " A.ITEM_CODE, B.ITEM_SHORT_DESC, A.ITEM_UOM" & vbCrLf _
            & " FROM PUR_INDENT_DET A, INV_ITEM_MST B" & vbCrLf _
            & " WHERE " & vbCrLf & " A.ITEM_CODE=B.ITEM_CODE " & vbCrLf _
            & " AND B.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND A.AUTO_KEY_INDENT=" & Val(pIndentNo) & "" & vbCrLf _
            & " AND A.SERIAL_NO=" & Val(pIndentSno) & ""


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsTemp
                '            SprdMain.Col = ColIndentNo
                '            SprdMain.Text = IIf(IsNull(!AUTO_KEY_INDENT), "", !AUTO_KEY_INDENT)
                '
                '            SprdMain.Col = ColIndentSNo
                '            SprdMain.Text = IIf(IsNull(!SERIAL_NO), "", !SERIAL_NO)

                SprdMain.Col = ColItemCode
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)

                SprdMain.Col = ColItemDesc
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value)

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
            End With
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColIndentNo)
        End If
        RsTemp.Close()
        RsTemp = Nothing
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Function CheckDuplicateItem(ByRef mIndentNo As Double, ByRef mIndentSNo As Integer) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mCheckCode As Double
        Dim mCheckSNo As Integer
        Dim mItemRept As Integer

        If Val(CStr(mIndentNo)) = 0 Then CheckDuplicateItem = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColIndentNo
                mCheckCode = Val(.Text)

                .Col = ColIndentSNo
                mCheckSNo = Val(.Text)

                If mCheckCode = Val(CStr(mIndentNo)) And mCheckSNo = Val(CStr(mIndentSNo)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColIndentNo)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function CheckQty(ByRef pCol As Integer, ByRef pRow As Integer) As Boolean

        On Error GoTo ERR1
        CheckQty = True
        With SprdMain
            .Row = pRow
            .Col = ColPrice
            If Val(.Text) = 0 Then
                CheckQty = False
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColPrice)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
End Class
