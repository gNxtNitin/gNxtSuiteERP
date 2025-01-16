Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamExcessDSApproval
    Inherits System.Windows.Forms.Form
    Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColMKEY As Short = 1
    Private Const ColCustomerCode As Short = 2
    Private Const ColCustomerName As Short = 3
    Private Const ColScheduleDate As Short = 4
    Private Const ColIndentBy As Short = 5
    Private Const ColSchdValue As Short = 6
    Private Const ColAppStatus As Short = 7


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .MaxCols = ColAppStatus

            .Row = 0
            SetColHeadings()
            .Row = Arow

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColMKEY, 12)
            .ColHidden = True

            .Col = ColCustomerCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustomerCode, 10)
            .ColHidden = True

            .Col = ColCustomerName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustomerName, 30)


            .Col = ColScheduleDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColScheduleDate, 11)

            .Col = ColIndentBy
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColIndentBy, 15)

            .ColsFrozen = ColCustomerName


            For cntCol = ColSchdValue To ColSchdValue
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 12)
                .ColHidden = False
            Next


            .Col = ColAppStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColAppStatus, 6)


            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMKEY, ColSchdValue)
        End With
        MainClass.SetSpreadColor(SprdMain, Arow)
        '    SprdMain.SetOddEvenRowColor &HC0FFFF, vbBlack, &HFFFFC0, vbBlack
        SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
    End Sub
    Private Sub SetColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColMKEY
            .Text = "MKEY"

            .Col = ColCustomerCode
            .Text = "Customer Code"

            .Col = ColCustomerName
            .Text = "Customer Name"

            .Col = ColScheduleDate
            .Text = "Schedule For the Month"

            .Col = ColIndentBy
            .Text = "Requisition By"

            .Col = ColSchdValue
            .Text = "Schedule Value"


            .Col = ColAppStatus
            .Text = "Approved"

            .set_RowHeight(0, 20)
        End With
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        txtCustomer.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo AddErr

        Clear1()
        SetDate(CDate(lblRunDate.Text))
        Show1()
        Call FormatSprdMain(-1)

        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldsVerification = False Then GoTo NoValidate

        If Update1 = False Then GoTo ErrPart

        cmdSave.Enabled = False


NoValidate:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        If Err.Number <> 0 Then
            ErrorMsg(CStr(Err.Number), Err.Description, MsgBoxStyle.Critical)
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmParamExcessDSApproval_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

        If FormActive = True Then Exit Sub

        Me.Text = "Excess Delivery Schedule Approval"
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtCustomer.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmParamExcessDSApproval_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmParamExcessDSApproval_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)


        lblRunDate.Text = CStr(RunDate)
        SetDate(CDate(lblRunDate.Text))
        UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)

        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False


        txtCustomer.Enabled = True
        chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmParamExcessDSApproval_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame2.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        FraFront.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))

        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        On Error GoTo ClearErr

        cmdSave.Enabled = True
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        Exit Sub
ClearErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub Show1()

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mSchdDate As String
        Dim mCustomerCode As String
        Dim mSchdValue As Double
        Dim mTotalSchdValue As Double
        Dim mMKey As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        mTotalSchdValue = 0

        '********************************
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColMKEY
                mMKey = Trim(.Text)

                .Col = ColCustomerCode
                mCustomerCode = Trim(.Text)

                .Col = ColAppStatus
                .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

                If Trim(mCustomerCode) <> "" Then
                    .Col = ColScheduleDate
                    mSchdDate = VB6.Format(.Text, "DD/MM/YYYY")

                    .Col = ColSchdValue
                    mSchdValue = GetSchdValue(mCustomerCode, mSchdDate, mMKey)
                    .Text = VB6.Format(mSchdValue, "0.00")

                    mTotalSchdValue = mTotalSchdValue + mSchdValue
                End If

            Next
        End With
        lblAmount.Text = VB6.Format(mTotalSchdValue, "0.00")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Function GetSchdValue(ByRef mCustomerCode As String, ByRef mSchdDate As String, ByRef mMKey As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFieldName As String
        Dim mWOPO As Boolean
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim mLastDate As String

        mLastDate = MainClass.LastDay(Month(CDate(mSchdDate)), Year(CDate(mSchdDate))) & "/" & VB6.Format(mSchdDate, "MM/YYYY")


        GetSchdValue = 0

        SqlStr = " SELECT ID.ITEM_CODE, " & vbCrLf & " ID.APP_QTY, " & vbCrLf & " (SELECT (((NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,4))) * EXCHANGERATE) * ID.APP_QTY" & vbCrLf & " FROM PUR_PURCHASE_HDR PH, PUR_PURCHASE_DET PD " & vbCrLf & " WHERE PH.COMPANY_CODE = PD.COMPANY_CODE AND PH.MKEY = PD.MKEY And PD.ITEM_CODE = ID.ITEM_CODE" & vbCrLf & " AND PH.SUPP_CUST_CODE= ID.SUPP_CUST_CODE" & vbCrLf & " AND PD.MKEY =  (SELECT MAX(SPH.MKEY) " & vbCrLf & " FROM PUR_PURCHASE_HDR SPH, PUR_PURCHASE_DET SPD" & vbCrLf & " WHERE SPH.MKEY = SPD.MKEY " & vbCrLf & " AND SPH.SUPP_CUST_CODE= ID.SUPP_CUST_CODE" & vbCrLf & " AND SPD.ITEM_CODE= ID.ITEM_CODE" & vbCrLf & " AND SPD.PO_WEF_DATE <= TO_DATE('" & VB6.Format(mLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND PO_STATUS='Y' AND ISGSTENABLE_PO='Y' AND ORDER_TYPE='O')) AS PORATE" & vbCrLf & " FROM INV_EXCESS_DS_APP_DET ID " & vbCrLf & " WHERE ID.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.MKEY = '" & Val(mMKey) & "'" & vbCrLf & " AND ID.BOOKTYPE='D'" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                GetSchdValue = GetSchdValue + Val(IIf(IsDbNull(RsTemp.Fields("PORATE").Value), 0, RsTemp.Fields("PORATE").Value))
                RsTemp.MoveNext()
            Loop
        Else
            GetSchdValue = 0
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetSchdValue = 0
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        Dim I As Integer
        Dim mAppStatus As String



        FieldsVerification = True
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number))
        FieldsVerification = False
        '    Resume
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1


        MakeSQL = " SELECT TO_CHAR(IH.MKEY) AS MKEY, " & vbCrLf & " IH.SUPP_CUST_CODE,  CMST.SUPP_CUST_NAME, TO_CHAR(IH.SCHD_DATE,'DD/MM/YYYY') AS SCHD_DATE, " & vbCrLf & " IH.INDENT_BY || '-' || AMST.EMP_NAME, 0 AS SCHD_VALUE," & vbCrLf & " 0 AS APPROVED"

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM INV_EXCESS_DS_APP_HDR IH, FIN_SUPP_CUST_MST CMST, ATH_PASSWORD_MST AMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=AMST.COMPANY_CODE" & vbCrLf & " AND IH.INDENT_BY=AMST.USER_ID"

        MakeSQL = MakeSQL & vbCrLf & "AND IH.IS_APPROVED ='N'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & "AND TO_CHAR(IH.SCHD_DATE,'MMYYYY')='" & VB6.Format(lblRunDate.Text, "MMYYYY") & "'"



        ''ORDER CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY TO_CHAR(IH.MKEY), CMST.SUPP_CUST_NAME"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub UpDYear_DownClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        '    Call PrintCommand(False)
        'RefreshScreen
    End Sub
    Private Sub SetDate(ByRef xDate As Date)

        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        Tempdate = "01/" & Month(xDate) & "/" & Year(xDate)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

        Daysinmonth = MainClass.LastDay(VB6.Format(xDate, "mm"), VB6.Format(xDate, "yyyy"))
    End Sub
    Private Sub UpDYear_UpClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        '    Call PrintCommand(False)
        'RefreshScreen
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo UpdateErr
        Dim SqlStr As String = ""
        Dim mRefNo As String
        Dim mAppStatus As String
        Dim I As Integer
        Dim pSONo As Double
        Dim pCustomerCode As String
        Dim pScheduleDate As String
        Dim mMKey As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColMKEY
                mMKey = Trim(.Text)

                .Col = ColCustomerCode
                pCustomerCode = Trim(.Text)

                .Col = ColScheduleDate
                pScheduleDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColAppStatus
                mAppStatus = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                If mAppStatus = "Y" And Trim(pCustomerCode) <> "" Then

                    SqlStr = "UPDATE INV_EXCESS_DS_APP_HDR SET " & vbCrLf & " AUTH_GIVEN_BY='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " IS_APPROVED='" & mAppStatus & "', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY='" & MainClass.AllowSingleQuote(mMKey) & "'" & vbCrLf & " AND BOOKTYPE='D'"

                    PubDBCn.Execute(SqlStr)

                End If

            Next
        End With

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
UpdateErr:
        Update1 = False
        PubDBCn.RollbackTrans()
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
        '    Resume
    End Function


    Private Sub frmParamExcessDSApproval_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColAppStatus
                    .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Next
            End With

        End If
    End Sub
    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim mCustomerCode As String
        Dim mDate As String
        Dim mMKey As String

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColMKEY
        mMKey = Me.SprdMain.Text

        SprdMain.Col = ColCustomerCode
        mCustomerCode = Me.SprdMain.Text

        SprdMain.Col = ColScheduleDate
        mDate = Me.SprdMain.Text

        frmParamExcessDSView.lblMkey.Text = mMKey
        frmParamExcessDSView.lblCustomerCode.Text = mCustomerCode
        frmParamExcessDSView.lblDate.Text = mDate
        frmParamExcessDSView.ShowDialog()
        frmParamExcessDSView.frmParamExcessDSView_Activated(Nothing, New System.EventArgs())

    End Sub

    Private Sub txtCustomer_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.DoubleClick
        SearchCustomer()
    End Sub
    Private Sub SearchCustomer()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        ''MainClass.SearchMaster txtCustomer, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(txtCustomer.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            txtCustomer.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtCustomer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomer.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCustomer_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomer.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCustomer()
    End Sub
    Private Sub txtCustomer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtCustomer.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtCustomer.Text = UCase(Trim(txtCustomer.Text))
        Else
            MsgInformation("No Such Customer in Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
