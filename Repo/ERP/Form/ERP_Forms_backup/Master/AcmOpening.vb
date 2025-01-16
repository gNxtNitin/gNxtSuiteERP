Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports System.ComponentModel

Friend Class frmAcmOpening
    Inherits System.Windows.Forms.Form
    Dim RsACM As ADODB.Recordset ''ADODB.Recordset				
    'Private PvtDBCn As ADODB.Connection				

    ''Dim RsOpOuts As ADODB.Recordset				

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String


    Private Const ConRowHeight As Short = 14

    Private Const ColLocation As Short = 1
    Private Const ColTRNType As Short = 2
    Private Const ColBillNo As Short = 3
    Private Const ColDate As Short = 4
    Private Const ColDivision As Short = 5
    Private Const ColCostC As Short = 6
    Private Const ColAmount As Short = 7
    Private Const ColDrCr As Short = 8
    Private Const ColDueDays As Short = 9
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            CmdAdd.Text = ConCmdCancelCaption
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtName.Focus()
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        '    If PvtDBCn.State = adStateOpen Then				
        '        PvtDBCn.Close				
        '        Set PvtDBCn = Nothing				
        '    End If				
        RsACM.Close()
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        Dim SqlStr As String = ""

        If txtName.Text = "" Then Call ErrorMsg("Nothing to Delete", "", MsgBoxStyle.Critical) : Exit Sub

        If PubSuperUser <> "S" Then
            MsgInformation("You have not Rights. Cannot Save")
            Exit Sub
        End If

        If Not RsACM.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "OPENING_BAL", (txtName.Text), RsACM, "SUPP_CUST_NAME") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "OPENING_BAL", "ACCOUNTCODE", (txtName.Text)) = False Then GoTo DelErrPart

                SqlStr = " DELETE From FIN_POSTED_TRN WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKTYPE='" & VB.Left(ConOpening, 1) & "'" & vbCrLf & " AND BOOKSUBTYPE='" & VB.Right(ConOpening, 1) & "'" & vbCrLf & " AND ACCOUNTCODE='" & RsACM.Fields("SUPP_CUST_CODE").Value & "'"
                PubDBCn.Execute(SqlStr)

                PubDBCn.CommitTrans()
                RsACM.Requery() ''.Refresh				
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        ''Resume				
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''				
        RsACM.Requery() ''.Refresh				
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsACM, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        If FieldVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        Call CalcTots()

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
        If UpdateOPOuts() = False Then GoTo UpdateError

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''				
        Update1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function UpdateOPOuts() As Boolean
        On Error GoTo ErrPart
        Dim RS As ADODB.Recordset = Nothing
        Dim mTRNType As String
        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim mMKey As String
        Dim mCostCCode As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mVNo As String
        Dim mVDate As String = ""
        Dim mAmount As Double
        Dim mDC As String
        Dim mDueDate As String
        Dim mVType As String
        Dim mBillType As String
        Dim mDivisionDesc As String
        Dim mDivisionCode As Double
        Dim mSuppType As String
        Dim mLocation As String

        '    SqlStr = " SELECT VTYPE From VOUCHERTYPE WHERE " & vbCrLf _				
        ''            & " BOOKTYPE='" & vb.Left(ConOpening, 1) & "' ORDER BY VTYPE"				
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, Rs, adLockReadOnly				
        '				
        '    If Rs.EOF = False Then				
        '        mVType = IIf(IsNull(Rs.Fields("VType").Value), "", Rs.Fields("VType").Value)				
        '    Else				
        '        UpdateOPOuts = False				
        '        ErrorMsg "Voucher Type Not Defined in the Master", "", vbInformation				
        '        Exit Function				
        '    End If				

        mVType = "OO"
        mMKey = mAccountCode

        SqlStr = " DELETE From FIN_POSTED_TRN WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BOOKTYPE='" & VB.Left(ConOpening, 1) & "'" & vbCrLf _
            & " AND BOOKSUBTYPE='" & VB.Right(ConOpening, 1) & "'" & vbCrLf _
            & " AND ACCOUNTCODE='" & mAccountCode & "'"

        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_TYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppType = IIf(IsDBNull(MasterNo), "", MasterNo)
        Else
            mSuppType = ""
        End If

        'If mSuppType = "S" Or mSuppType = "C" Then
        '    SqlStr = SqlStr & vbCrLf & " AND LOCATION_ID='" & MainClass.AllowSingleQuote(txtLocation.Text) & "'"
        'Else
        '    txtLocation.Text = "-"
        'End If

        PubDBCn.Execute(SqlStr)

        mVNo = "OP"
        mVDate = CDate(RsCompany.Fields("START_DATE").Value).ToString("dd/MM/yyyy") ''- 1				
        mVDate = DateAdd(DateInterval.Day, -1, CDate(mVDate))

        '    If optBalMethod(0).Value = True Then        'IF Summarised.......				
        '        mBillNo = "OP"				
        '        mBillDate = RsCompany.Fields("START_DATE").Value				
        '        mAmount = Val(MskOpbal.Text)				
        '        mDC = IIf(optDrCr(0).Value = True, "D", "C")				
        '        mTRNType = "B"				
        '        mCostCCode = "-1"				
        '				
        '        mBillType = "B"				
        '				
        '        If mAmount <> 0 Then				
        '            'If Left(Trim(cboCategory.Text), 1) <> "O" Then				
        '                If UpdateTRN(PubDBCn, mMkey, 1, 1, ConOpeningBookCode, mVType, Left(ConOpening, 1), Right(ConOpening, 1), mAccountCode, mVNo, mVDate, mBillNo, mBillDate, mAmount, mDC, mTRNType, "", "", mCostCCode, "-1", "-1", "-1", mDueDate, "", mBillType, "", "", "Opening :", "", mBillDate, True, PubUserID, VB6.Format(PubCurrDate, "dd/MM/yyyy")) = False Then GoTo ErrPart				
        '            'End If				
        '        End If				
        '    Else				
        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColLocation
                mLocation = Trim(.Text)

                .Col = ColTRNType
                mTRNType = IIf(.Text = "", "B", VB.Left(.Text, 1))

                .Col = ColBillNo
                mBillNo = .Text

                .Col = ColDate
                mBillDate = IIf(IsDBNull(.Text), ConBlankDate, VB6.Format(.Text, "dd/MM/yyyy"))

                .Col = ColDivision
                mDivisionCode = Trim(.Text)

                If MainClass.ValidateWithMasterTable(Trim(mDivisionCode), "DIV_Code", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionCode = Trim(MasterNo)
                End If


                .Col = ColCostC
                If MainClass.ValidateWithMasterTable(.Text, "CC_CODE", "CC_CODE", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCostCCode = MasterNo
                Else
                    mCostCCode = -1
                End If

                .Col = ColAmount
                mAmount = Val(.Text)

                .Col = ColDrCr
                mDC = VB.Left(.Text, 1)

                .Col = ColDueDays
                mDueDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, Val(.Text), CDate(mBillDate))) '' Val(.Text)				

                mBillType = "B"

                If mAmount <> 0 Then
                    If UpdateTRN(PubDBCn, mMKey, cntRow, cntRow, CStr(ConOpeningBookCode), mVType, VB.Left(ConOpening, 1), VB.Right(ConOpening, 1), mAccountCode, mVNo, mVDate, mBillNo, mBillDate, mAmount, mDC, mTRNType, "", "", mCostCCode, "-1", "-1", "-1", mDueDate, "", mBillType, "", "", "Opening :", "", mBillDate, True, PubUserID, VB6.Format(PubCurrDate, "dd/MM/yyyy"), mDivisionCode, mLocation) = False Then GoTo ErrPart
                End If
            Next
        End With
        '    End If				

        UpdateOPOuts = True
        Exit Function
ErrPart:
        'Resume				
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateOPOuts = False
    End Function


    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "SUPP_CUST_ADDR", "", SqlStr) = True Then
            txtName.Text = AcName
            txtCode.Text = AcName1

            'TxtName_Validate(False)				
            If txtCode.Enabled = True Then txtCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    '    Private Sub SearchFormName(ByRef mTextField As System.Windows.Forms.TextBox)
    '        On Error GoTo ErrPart
    '        Dim SqlStr As String = ""

    '        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

    '        If MainClass.SearchGridMaster(mTextField.Text, "FIN_STFORM_MST", "NAME", , , , SqlStr) = True Then
    '            mTextField.Text = AcName
    '        End If
    '        Exit Sub
    'ErrPart:
    '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    '    End Sub

    Private Sub SearchCode()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtCode.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", "SUPP_CUST_NAME", "SUPP_CUST_ADDR", "", SqlStr) = True Then
            txtCode.Text = AcName
            txtName.Text = AcName1
            'txtCode_Validate(False)				
            txtCode.Focus()
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
        MainClass.ButtonStatus(Me, XRIGHT, RsACM, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmAcmOpening_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = "Select * From FIN_SUPP_CUST_MST WHERE 1<>1 Order by SUPP_CUST_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockReadOnly)

        '    SqlStr = "Select * From OpOuts Where 1<>1"				
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsOpOuts, adLockReadOnly				

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

        SqlStr = " SELECT ACM.SUPP_CUST_CODE AS CODE, ACM.SUPP_CUST_NAME AS NAME, BACM.LOCATION_ID," & vbCrLf _
            & " SUM(AMOUNT) AS BAL_AMOUNT" & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN, FIN_SUPP_CUST_MST ACM, FIN_SUPP_CUST_BUSINESS_MST BACM" & vbCrLf _
            & " WHERE ACM.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE" & vbCrLf _
            & " AND TRN.COMPANY_CODE=BACM.COMPANY_CODE(+) AND TRN.ACCOUNTCODE=BACM.SUPP_CUST_CODE (+)" & vbCrLf _
            & " AND TRN.LOCATION_ID=BACM.LOCATION_ID (+)" & vbCrLf _
            & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND TRN.BOOKTYPE='" & VB.Left(ConOpening, 1) & "'" & vbCrLf _
            & " AND TRN.BOOKSUBTYPE='" & VB.Right(ConOpening, 1) & "'"


        SqlStr = SqlStr & " GROUP BY ACM.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME, BACM.LOCATION_ID"

        SqlStr = SqlStr & " HAVING SUM(AMOUNT)<>0"

        SqlStr = SqlStr & " ORDER BY ACM.SUPP_CUST_NAME, BACM.LOCATION_ID"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmAcmOpening_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmAcmOpening_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection				
        ''PvtDBCn.Open StrConn				
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

        mAccountCode = "-1"
        txtName.Text = ""
        txtCode.Text = ""

        txtCode.Enabled = True

        optBalMethod(0).Checked = True
        MskOpbal.Text = "0.00"
        optDrCr(0).Checked = True

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "", txtName)
        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", "", txtCode)


        MainClass.ButtonStatus(Me, XRIGHT, RsACM, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume				
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsOpOuts As ADODB.Recordset = Nothing

        SqlStr = " SELECT * From FIN_POSTED_TRN WHERE 1=2"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpOuts, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColTRNType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            ''.TypeEditLen = RsOpOuts.Fields("TrnType").DefinedSize           ''				
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 12)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsOpOuts.Fields("BillNo").DefinedSize ''				
            .set_ColWidth(.Col, 12)
            .ColsFrozen = ColBillNo

            .Col = ColDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(.Col, 12)

            .Col = ColDivision
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColDivision, 10)

            .Col = ColCostC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '.TypeEditLen = MainClass.SetMaxLength("Alias", "CostC", PubDBCn)				
            .set_ColWidth(ColCostC, 8)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999999.99")
            .TypeFloatMin = CDbl("-999999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 12)

            .Col = ColDrCr
            '.CellType = SS_CELL_TYPE_COMBOBOX
            '.TypeComboBoxList = "Cr" & Chr(9) & "Dr"
            '.TypeComboBoxCurSel = 0

            If FormActive = False Then
                .CellType = SS_CELL_TYPE_COMBOBOX
                .TypeComboBoxList = "Cr" & Chr(9) & "Dr"
                .TypeComboBoxCurSel = 0
            End If

            .Col = ColDueDays
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColDueDays, 8)

            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 300)

            .set_ColWidth(0, 500)
            .set_ColWidth(1, 900)
            .set_ColWidth(2, 6000)
            .set_ColWidth(3, 2500)
            .Col = 3
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle				
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtName.MaxLength = RsACM.Fields("SUPP_CUST_NAME").DefinedSize ''				
        txtCode.MaxLength = RsACM.Fields("SUPP_CUST_CODE").DefinedSize ''				
        'txtCode.MaxLength = RsACM.Fields("SUPP_CUST_CODE").DefinedSize

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume				
    End Sub

    Function FieldVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mSuppType As String
        Dim cntRow As Long
        Dim mLocation As String

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

        Dim mAuthorised As Boolean
        mAuthorised = IIf(InStr(1, XRIGHT, "S") > 0, True, False)

        If mAuthorised = False Then
            MsgInformation("You have not Rights. Cannot Save")
            FieldVarification = False
            Exit Function
        End If

        Dim xAccountCode As String = ""

        If MainClass.ValidateWithMasterTable(txtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAccountCode = Trim(MasterNo)
        Else
            MsgInformation("Please select the Valid Account. Cannot Save")
            FieldVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_TYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppType = IIf(IsDBNull(MasterNo), "", MasterNo)
        Else
            mSuppType = ""
        End If

        For cntRow = 1 To SprdMain.MaxRows - 1
            SprdMain.Col = ColLocation
            SprdMain.Row = cntRow
            mLocation = Trim(SprdMain.Text)

            If mSuppType = "S" Or mSuppType = "C" Then

                If MainClass.ValidateWithMasterTable(mLocation, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAccountCode) & "'") = False Then
                    MsgInformation("Please select the Valid Location. Cannot Save")
                    FieldVarification = False
                    Exit Function
                End If
            End If
        Next

        'If optBalMethod(1).Checked = True Then
        If SprdMain.MaxRows > 1 Then
            If MainClass.ValidDataInGrid(SprdMain, ColBillNo, "S", "Bill No is must") = False Then FieldVarification = False : Exit Function
            If MainClass.ValidDataInGrid(SprdMain, ColDate, "S", "Bill Date is must") = False Then FieldVarification = False : Exit Function
            If MainClass.ValidDataInGrid(SprdMain, ColDivision, "S", "Division Is Blank.") = False Then FieldVarification = False : Exit Function

            'If MainClass.ValidDataInGrid(SprdMain, ColCostC, "S", "Cost Centre is must") = False Then FieldVarification = False: Exit Function				
            '            If MainClass.ValidDataInGrid(SprdMain, ColDueDays, "N", "Due Days is must") = False Then FieldVarification = False: Exit Function				
        End If
        'End If
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmAcmOpening_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        '    If PvtDBCn.State = adStateOpen Then				
        '        PvtDBCn.Close				
        '        Set PvtDBCn = Nothing				
        '    End If				
        RsACM.Close()
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub MskOpbal_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MskOpbal.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub MskOpbal_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MskOpbal.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub optBalMethod_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optBalMethod.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optBalMethod.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
            SprdMain.Enabled = True '' IIf(optBalMethod(0).Checked = True, False, True)
            MskOpbal.Enabled = False ' IIf(optBalMethod(0).Checked = True, True, False)
            optDrCr(0).Enabled = False 'IIf(optBalMethod(0).Checked = True, True, False)
            optDrCr(1).Enabled = False 'IIf(optBalMethod(0).Checked = True, True, False)
        End If
    End Sub

    Private Sub optDrCr_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optDrCr.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optDrCr.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xAccountCode As String
        Dim SqlStr As String

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)

        If eventArgs.row = 0 And eventArgs.col = ColLocation Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColLocation

                xAccountCode = "-1"

                If MainClass.ValidateWithMasterTable(txtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    xAccountCode = Trim(MasterNo)
                End If

                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & xAccountCode & "'"


                If MainClass.SearchGridMaster(.Text, "FIN_SUPP_CUST_BUSINESS_MST", "LOCATION_ID", "SUPP_CUST_NAME", "SUPP_CUST_ADDR", "SUPP_CUST_CITY", SqlStr,,, "SUPP_CUST_STATE") = True Then
                    .Row = .ActiveRow
                    .Col = ColLocation
                    .Text = AcName
                End If

                'If MainClass.SearchGridMaster(.Text, "INV_DIVISION_MST", "DIV_DESC", "DIV_CODE", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '    .Row = .ActiveRow
                '    .Col = ColDivision
                '    .Text = AcName1
                'End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColLocation)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColDivision Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColDivision
                If MainClass.SearchGridMaster(.Text, "INV_DIVISION_MST", "DIV_DESC", "DIV_CODE", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColDivision
                    .Text = AcName1
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColDivision)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColCostC Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColCostC
                ''If MainClass.SearchMaster("", "CostC", "Name", "Status='O'", "Alias") = True Then				
                If MainClass.SearchGridMaster("", "FIN_CCENTER_HDR", "CC_CODE", "CC_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "CC_CODE") = True Then
                    .Row = .ActiveRow
                    .Col = ColCostC
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColCostC)
            End With
        End If
        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColBillNo)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
            CalcTots()
        End If
    End Sub

    Private Sub CalcTots()
        On Error GoTo ERR1
        Dim cntRow As Integer
        Dim TotOpBal As Double
        Dim mDC As String


        ''if Balanceing method is summerized then exit				
        'If optBalMethod(0).Checked = True Then Exit Sub
        TotOpBal = 0

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColBillNo
                If .Text = "" Then GoTo DontCalc
                .Col = ColDrCr
                mDC = .Text
                .Col = ColAmount
                TotOpBal = TotOpBal + (Val(.Text) * IIf(mDC = "Dr", 1, -1))
DontCalc:
            Next
        End With

        MskOpbal.Text = VB6.Format(System.Math.Abs(TotOpBal), "0.00")
        If TotOpBal > 0 Then
            optDrCr(0).Checked = True
        Else
            optDrCr(1).Checked = True
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Information)
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColDivision Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDivision, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColCostC Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColCostC, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xAccountCode As String
        Dim SqlStr As String
        Dim mSuppType As String

        Select Case eventArgs.col
            Case ColLocation
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColLocation
                If Trim(SprdMain.Text) = "" Then Exit Sub


                If MainClass.ValidateWithMasterTable(txtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_TYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSuppType = IIf(IsDBNull(MasterNo), "", MasterNo)
                Else
                    mSuppType = ""
                End If

                If mSuppType = "S" Or mSuppType = "C" Then
                    If MainClass.ValidateWithMasterTable(txtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        xAccountCode = Trim(MasterNo)
                    End If

                    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & xAccountCode & "'"


                    If MainClass.ValidateWithMasterTable(SprdMain.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                        SprdMain.Row = SprdMain.ActiveRow
                        SprdMain.Col = ColLocation
                        SprdMain.Text = MasterNo
                    Else
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColDivision)
                    End If
                End If


            Case ColDivision
                SprdMain.Row = eventArgs.row

                SprdMain.Col = ColDivision
                If Trim(SprdMain.Text) = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(SprdMain.Text, "DIV_CODE", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColDivision
                    SprdMain.Text = MasterNo
                Else
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColDivision)
                End If


            Case ColTRNType
                SprdMain.Col = ColTRNType
                SprdMain.Row = eventArgs.row
                If InStr(1, "BOADC", UCase(VB.Left(SprdMain.Text, 1))) = 0 Then SprdMain.Text = "B"
                Select Case UCase(VB.Left(SprdMain.Text, 1))
                    Case "B"
                        SprdMain.Text = "BILL"
                    Case "O"
                        SprdMain.Text = "ON ACCOUNT"
                    Case "A"
                        SprdMain.Text = "ADVANCE"
                    Case "D"
                        SprdMain.Text = "D/N"
                    Case "C"
                        SprdMain.Text = "C/N"
                End Select
            Case ColCostC
                '            If CheckCostC(Col, Row) = False Then				
                '               MainClass.SetFocusToCell SprdMain, Row, Col				
                '           End If				
            Case ColAmount
                SprdMain.Col = ColAmount
                SprdMain.Row = eventArgs.row
                If Val(SprdMain.Text) <> 0 Then
                    MainClass.AddBlankSprdRow(SprdMain, ColBillNo, ConRowHeight)
                    FormatSprdMain(-1)
                End If
        End Select
        CalcTots()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function CheckCostC(ByVal Col As Integer, ByVal Row As Integer) As Boolean
        On Error GoTo ERR1
        CheckCostC = False
        With SprdMain
            .Row = Row
            .Col = ColCostC
            If MainClass.ValidateWithMasterTable(.Text, "Alias", "Alias", "CostC", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                CheckCostC = True
                .Text = IIf(IsDBNull(MasterNo), "", MasterNo)
            End If
        End With
        Exit Function
ERR1:
        CheckCostC = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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

    Private Sub txtCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.DoubleClick
        Call SearchCode()
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCode()
    End Sub
    Public Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mSuppType As String

        If Trim(txtCode.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsACM.EOF = False Then
            mAccountCode = RsACM.Fields("SUPP_CUST_CODE").Value
            'mLocationCode = IIf(IsDBNull(RsACM.Fields("LOCATION_ID").Value), "", RsACM.Fields("LOCATION_ID").Value)
        End If
        'SqlStr = "Select * From FIN_SUPP_CUST_BUSINESS_MST " & vbCrLf _
        '        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
        '        & " AND LOCATION_ID='" & MainClass.AllowSingleQuote(txtLocation.Text) & "'"

        SqlStr = "Select * From FIN_SUPP_CUST_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"

        If MainClass.ValidateWithMasterTable(txtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_TYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppType = IIf(IsDBNull(MasterNo), "", MasterNo)
        Else
            mSuppType = ""
        End If

        'If mSuppType = "S" Or mSuppType = "C" Then
        '    SqlStr = SqlStr & vbCrLf & " AND LOCATION_ID='" & MainClass.AllowSingleQuote(txtLocation.Text) & "'"
        'End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockReadOnly)

        If RsACM.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From FIN_SUPP_CUST_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & mAccountCode & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub TxtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mSuppType As String

        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsACM.EOF = False Then mAccountCode = RsACM.Fields("SUPP_CUST_CODE").Value

        'If MainClass.ValidateWithMasterTable(txtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mAccountCode = IIf(IsDBNull(MasterNo), "-1", MasterNo)
        '    txtCode.Text = mAccountCode
        'Else
        '    mAccountCode = "-1"
        'End If

        SqlStr = "Select * From FIN_SUPP_CUST_MST " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtName.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockReadOnly)

        If RsACM.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From FIN_SUPP_CUST_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & mAccountCode & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mBalOpt As Integer
        Dim mControlBranchCode As Integer
        Dim mBuyerCode As String
        Dim mBalMethod As String
        Dim mSupCustType As String

        Clear1()
        If Not RsACM.EOF Then

            mAccountCode = IIf(IsDBNull(RsACM.Fields("SUPP_CUST_CODE").Value), -1, RsACM.Fields("SUPP_CUST_CODE").Value)
            txtName.Text = Trim(IIf(IsDBNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value))
            txtCode.Text = Trim(IIf(IsDBNull(RsACM.Fields("SUPP_CUST_CODE").Value), "", RsACM.Fields("SUPP_CUST_CODE").Value))
            mSupCustType = Trim(IIf(IsDBNull(RsACM.Fields("SUPP_CUST_TYPE").Value), "", RsACM.Fields("SUPP_CUST_TYPE").Value))

            txtCode.Enabled = False

            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "BALANCINGMETHOD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mBalMethod = IIf(IsDBNull(MasterNo), "S", MasterNo)
            Else
                mBalMethod = "S"
            End If

            If mBalMethod = "S" Then
                optBalMethod(0).Checked = True
            Else
                optBalMethod(1).Checked = True
            End If


            ShowOpOuts(mSupCustType)
            CalcTots()

            'Field Disable...				
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsACM, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume				
    End Sub
    Private Sub ShowOpOuts(pSuppType As String)

        On Error GoTo ShowError
        Dim RsOpOuts As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mDivisionCode As Double

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        'If Trim(txtLocation.Text) = "" Then Exit Sub

        MskOpbal.Text = "0.00"

        SqlStr = " SELECT TRNTYPE,BILLNO,BILLDATE,COSTCCODE,AMOUNT,DUEDAYS,DC,DIV_CODE,LOCATION_ID From FIN_POSTED_TRN WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BOOKTYPE='" & VB.Left(ConOpening, 1) & "'" & vbCrLf _
            & " AND BOOKSUBTYPE='" & VB.Right(ConOpening, 1) & "'" & vbCrLf _
            & " AND ACCOUNTCODE='" & mAccountCode & "'"

        'If pSuppType = "S" Or pSuppType = "C" Then
        '    SqlStr = SqlStr & vbCrLf & " AND LOCATION_ID='" & txtLocation.Text & "'"
        'End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpOuts, ADODB.LockTypeEnum.adLockReadOnly)

        If RsOpOuts.EOF = True Then
            Exit Sub
        End If

        '    If optBalMethod(1).Value = True Then				
        'FormatSprdMain -1				
        With SprdMain
            Do While Not RsOpOuts.EOF
                .Row = SprdMain.MaxRows

                .Col = ColLocation
                .Text = IIf(IsDBNull(RsOpOuts.Fields("LOCATION_ID").Value), "", RsOpOuts.Fields("LOCATION_ID").Value)

                .Col = ColTRNType
                .Text = IIf(IsDBNull(RsOpOuts.Fields("TRNTYPE").Value), "", GetPayType((RsOpOuts.Fields("TRNTYPE").Value)))

                .Col = ColBillNo
                .Text = IIf(IsDBNull(RsOpOuts.Fields("BILLNO").Value), "", RsOpOuts.Fields("BILLNO").Value)

                .Col = ColDate
                .Text = IIf(IsDBNull(RsOpOuts.Fields("BILLDATE").Value), "", VB6.Format(RsOpOuts.Fields("BILLDATE").Value, "dd/MM/yyyy"))

                mDivisionCode = IIf(IsDBNull(RsOpOuts.Fields("DIV_CODE").Value), -1, RsOpOuts.Fields("DIV_CODE").Value)
                SprdMain.Col = ColDivision
                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Text = Trim(MasterNo)
                Else
                    .Text = ""
                End If

                .Col = ColCostC
                If MainClass.ValidateWithMasterTable(RsOpOuts.Fields("COSTCCODE").Value, "CC_CODE", "CC_CODE", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Text = IIf(IsDBNull(MasterNo), "", MasterNo)
                Else
                    .Text = ""
                End If

                .Col = ColAmount
                If RsOpOuts.Fields("Amount").Value <> 0 Then
                    .Text = VB6.Format(RsOpOuts.Fields("Amount").Value, "0.00")
                End If

                .Col = ColDrCr
                .Text = IIf(IsDBNull(RsOpOuts.Fields("DC").Value), "D", RsOpOuts.Fields("DC").Value) & "r"

                .Col = ColDueDays
                .Text = Str(IIf(IsDBNull(RsOpOuts.Fields("DUEDAYS").Value), 0, RsOpOuts.Fields("DUEDAYS").Value))

                .MaxRows = .MaxRows + 1
                RsOpOuts.MoveNext()
            Loop
        End With
        '    Else				
        '        MskOpbal.Text = VB6.Format(Abs(RsOpOuts.Fields("Amount").Value), "0.00")				
        '        If RsOpOuts.Fields("DC").Value = "D" Then				
        '            optDrCr(0).Value = True				
        '        Else				
        '            optDrCr(1).Value = True				
        '        End If				
        '    End If				
        Exit Sub
ShowError:
        MsgInformation(Err.Description)
        '    Resume				
    End Sub
    Private Function GetPayType(ByRef pPayType As Object) As String
        Select Case UCase(pPayType)
            Case "B"
                GetPayType = "BILL"
            Case "N"
                GetPayType = "NEW BILL"
            Case "D"
                GetPayType = "D/N"
            Case "C"
                GetPayType = "C/N"
            Case "O"
                GetPayType = "ON ACCOUNT"
            Case "A"
                GetPayType = "ADVANCE"
            Case Else
                GetPayType = "ON ACCOUNT"
        End Select
    End Function
    Private Sub SearchFormName(ByRef mTextField As System.Windows.Forms.TextBox)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(mTextField.Text, "FIN_STFORM_MST", "NAME", , , , SqlStr) = True Then
            mTextField.Text = AcName
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
