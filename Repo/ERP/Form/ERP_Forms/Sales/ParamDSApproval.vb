Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamDSApproval
    Inherits System.Windows.Forms.Form
    Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColMKEY As Short = 1
    Private Const ColRefNo As Short = 2
    Private Const ColRefDate As Short = 3
    Private Const ColAmendNo As Short = 4
    Private Const ColCustomerCode As Short = 5
    Private Const ColCustomerName As Short = 6
    Private Const ColOurSONo As Short = 7
    Private Const ColCustPONo As Short = 8
    Private Const ColScheduleDate As Short = 9
    Private Const ColRemarks As Short = 10
    Private Const ColPreSchdValue As Short = 11
    Private Const ColSchdValue As Short = 12
    Private Const ColAmnendSchdValue As Short = 13
    Private Const ColAppStatus As Short = 14


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

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColRefNo, 10)

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColRefDate, 10)

            .Col = ColAmendNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColAmendNo, 5)

            .Col = ColCustomerCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustomerCode, 8)
            .ColHidden = False

            .Col = ColCustomerName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustomerName, 30)

            .Col = ColOurSONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColOurSONo, 12)
            .ColHidden = True

            .Col = ColCustPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustPONo, 18)

            .Col = ColScheduleDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColScheduleDate, 10)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColRemarks, 9)

            .ColsFrozen = ColCustomerName


            For cntCol = ColPreSchdValue To ColAmnendSchdValue
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 9)
                .ColHidden = False
            Next


            .Col = ColAppStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColAppStatus, 6)


            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMKEY, ColAmnendSchdValue)
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

            .Col = ColRefNo
            .Text = "Ref No"

            .Col = ColRefDate
            .Text = "Ref Date"

            .Col = ColAmendNo
            .Text = "Amend No"

            .Col = ColCustomerCode
            .Text = "Customer Code"

            .Col = ColCustomerName
            .Text = "Customer Name"

            .Col = ColOurSONo
            .Text = "Our SO. No."

            .Col = ColCustPONo
            .Text = "Customer PO no"

            .Col = ColScheduleDate
            .Text = "Schedule For the Month"

            .Col = ColRemarks
            .Text = "Remarks"

            .Col = ColPreSchdValue
            .Text = "Previous Schedule Value"

            .Col = ColSchdValue
            .Text = "Schedule Value"

            .Col = ColAmnendSchdValue
            .Text = "Amended Schedule Value"

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
        Dim mDate As String
        Clear1()
        mDate = lblYear.Text
        SetDate(CDate(mDate))
        Show1()
        Call FormatSprdMain(-1)

        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldsVerification() = False Then GoTo NoValidate

        If Update1() = False Then GoTo ErrPart

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
    Private Sub frmParamDSApproval_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

        If FormActive = True Then Exit Sub

        'Me.Text = "Delivery Schedule Approval Register" & IIf(lblBookType.Text = "P", "-(Plant Head Approval)", "-(Bussiness Head Approval)")
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
    Private Sub frmParamDSApproval_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmParamDSApproval_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        'Set PvtDBCn = New ADODB.Connection						
        'PvtDBCn.Open StrConn						

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)


        lblRunDate.Text = CStr(RunDate)
        SetDate(CDate(lblRunDate.Text))


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
    Private Sub frmParamDSApproval_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
        Dim SqlStr As String
        Dim CntRow As Integer
        Dim mSchdDate As String
        Dim mSONo As Double
        Dim mCustomerCode As String
        Dim mSchdValue As Double
        Dim mPreSchdValue As Double
        Dim mTotalSchdValue As Double
        Dim pRefNo As Double
        Dim mAmendNo As Double

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        mTotalSchdValue = 0

        '''********************************						
        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow

                .Col = ColRefNo
                pRefNo = Val(.Text)

                .Col = ColAmendNo
                mAmendNo = Val(.Text)

                .Col = ColCustomerCode
                mCustomerCode = Trim(.Text)

                .Col = ColOurSONo
                mSONo = Val(.Text)

                .Col = ColScheduleDate
                mSchdDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColPreSchdValue
                If mAmendNo = 0 Then
                    mPreSchdValue = 0
                Else
                    mPreSchdValue = GetPreSchdValue(mCustomerCode, mSONo, mSchdDate, pRefNo)
                End If
                .Text = VB6.Format(mPreSchdValue, "0.00")

                .Col = ColSchdValue
                mSchdValue = GetSchdValue(mCustomerCode, mSONo, mSchdDate, pRefNo)
                .Text = VB6.Format(mSchdValue, "0.00")

                .Col = ColAmnendSchdValue
                .Text = VB6.Format(mSchdValue - mPreSchdValue, "0.00")

                mTotalSchdValue = mTotalSchdValue + mSchdValue

            Next
        End With
        lblAmount.Text = VB6.Format(mTotalSchdValue, "0.00")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Function GetSchdValue(ByRef mCustomerCode As String, ByRef mSONo As Double, ByRef mSchdDate As String, ByRef pRefNo As Double) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mFieldName As String
        Dim mWOPO As Boolean
        Dim mPurchaseUOM As String
        Dim mFactor As Double

        GetSchdValue = 0
        SqlStr = " SELECT ID.ITEM_CODE,"

        SqlStr = SqlStr & vbCrLf _
            & "  (SELECT ITEM_QTY * ITEM_PRICE AS ITEM_PRICE FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf _
            & " WHERE SIH.COMPANY_CODE=IH.COMPANY_CODE" & vbCrLf _
            & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & mCustomerCode & "'" & vbCrLf _
            & " AND SID.ITEM_CODE=ID.ITEM_CODE" & vbCrLf _
            & " AND NVL(SID.CUST_STORE_LOC,' ')=NVL(ID.LOC_CODE,' ')" & vbCrLf _
            & " AND SIH.AUTO_KEY_SO=" & Val(CStr(mSONo)) & " AND SIH.SO_APPROVED='Y'" & vbCrLf _
            & " AND SIH.MKEY = ("

        SqlStr = SqlStr & vbCrLf _
            & "SELECT MAX(SSIH.MKEY) FROM  DSP_SALEORDER_HDR SSIH, DSP_SALEORDER_DET SSID" & vbCrLf _
            & " WHERE SSIH.COMPANY_CODE=SIH.COMPANY_CODE" & vbCrLf _
            & " AND SSIH.MKEY=SSID.MKEY AND SSIH.SUPP_CUST_CODE=SIH.SUPP_CUST_CODE" & vbCrLf _
            & " AND SSID.ITEM_CODE=SID.ITEM_CODE" & vbCrLf _
            & " AND NVL(SSID.CUST_STORE_LOC,' ')=NVL(SID.CUST_STORE_LOC,' ')" & vbCrLf _
            & " AND SSIH.AUTO_KEY_SO=SIH.AUTO_KEY_SO AND SSIH.AUTO_KEY_SO=" & Val(CStr(mSONo)) & " AND SSIH.SO_APPROVED='Y'" & vbCrLf _
            & " AND SSID.AMEND_WEF <=IH.SCHLD_DATE)) AS RATE"

        SqlStr = SqlStr & vbCrLf _
            & " FROM DSP_DELV_SCHLD_REQ_HDR IH, DSP_DELV_SCHLD_REQ_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf _
            & " AND IH.AUTO_KEY_DELV=" & Val(CStr(pRefNo)) & ""

        '

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                GetSchdValue = GetSchdValue + Val(IIf(IsDBNull(RsTemp.Fields("RATE").Value), 0, RsTemp.Fields("RATE").Value))
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

    Private Function GetPreSchdValue(ByRef mCustomerCode As String, ByRef mSONo As Double, ByRef mSchdDate As String, ByRef pRefNo As Double) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mFieldName As String
        Dim mWOPO As Boolean
        Dim mPurchaseUOM As String
        Dim mFactor As Double

        GetPreSchdValue = 0
        SqlStr = " SELECT ID.ITEM_CODE,"

        SqlStr = SqlStr & vbCrLf _
            & "  (SELECT ITEM_QTY * ITEM_PRICE AS ITEM_PRICE FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf _
            & " WHERE SIH.COMPANY_CODE=IH.COMPANY_CODE" & vbCrLf _
            & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & mCustomerCode & "'" & vbCrLf _
            & " AND SID.ITEM_CODE=ID.ITEM_CODE" & vbCrLf _
            & " AND NVL(SID.CUST_STORE_LOC,' ')=NVL(ID.LOC_CODE,' ')" & vbCrLf _
            & " AND SIH.AUTO_KEY_SO=" & Val(CStr(mSONo)) & " AND SIH.SO_APPROVED='Y'" & vbCrLf _
            & " AND SIH.MKEY = ("

        SqlStr = SqlStr & vbCrLf _
            & "SELECT MAX(SSIH.MKEY) FROM  DSP_SALEORDER_HDR SSIH, DSP_SALEORDER_DET SSID" & vbCrLf _
            & " WHERE SSIH.COMPANY_CODE=SIH.COMPANY_CODE" & vbCrLf _
            & " AND SSIH.MKEY=SSID.MKEY AND SSIH.SUPP_CUST_CODE=SIH.SUPP_CUST_CODE" & vbCrLf _
            & " AND SSID.ITEM_CODE=SID.ITEM_CODE" & vbCrLf _
            & " AND NVL(SSID.CUST_STORE_LOC,' ')=NVL(SID.CUST_STORE_LOC,' ')" & vbCrLf _
            & " AND SSIH.AUTO_KEY_SO=SIH.AUTO_KEY_SO AND SSIH.AUTO_KEY_SO=" & Val(CStr(mSONo)) & " AND SSIH.SO_APPROVED='Y'" & vbCrLf _
            & " AND SSID.AMEND_WEF <=IH.SCHLD_DATE)) AS RATE"

        SqlStr = SqlStr & vbCrLf _
            & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DELV_SCHLD_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & mCustomerCode & "'" & vbCrLf _
            & " AND IH.AUTO_KEY_SO=" & Val(CStr(mSONo)) & "" & vbCrLf _
            & " AND IH.SCHLD_DATE=TO_DATE('" & VB6.Format(mSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                GetPreSchdValue = GetPreSchdValue + Val(IIf(IsDBNull(RsTemp.Fields("RATE").Value), 0, RsTemp.Fields("RATE").Value))
                RsTemp.MoveNext()
            Loop
        Else
            GetPreSchdValue = 0
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetPreSchdValue = 0
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

        MakeSQL = " SELECT '', " & vbCrLf _
            & " TO_CHAR(IH.AUTO_KEY_DELV), TO_CHAR(IH.DELV_SCHLD_DATE,'DD/MM/YYYY') AS DELV_SCHLD_DATE, " & vbCrLf _
            & " IH.DELV_AMEND_NO,  IH.SUPP_CUST_CODE,  CMST.SUPP_CUST_NAME, IH.AUTO_KEY_SO, IH.CUST_SO_NO, TO_CHAR(IH.SCHLD_DATE,'DD/MM/YYYY') AS SCHLD_DATE, " & vbCrLf _
            & " IH.REMARKS, 0 AS SCHD_VALUE,0 AS AMEND_SCHD_VALUE," & vbCrLf _
            & " 0 AS APPROVED"

        ''''FROM CLAUSE...						
        MakeSQL = MakeSQL & vbCrLf _
            & " FROM DSP_DELV_SCHLD_REQ_HDR IH, FIN_SUPP_CUST_MST CMST"

        ''''WHERE CLAUSE...						
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        If lblBookType.Text = "P" Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.APPROVAL_PH ='N' AND IH.APPROVAL_BH ='N'"
        Else
            MakeSQL = MakeSQL & vbCrLf & "AND IH.APPROVAL_PH ='Y' AND IH.APPROVAL_BH ='N'"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & "AND TO_CHAR(IH.SCHLD_DATE,'MMYYYY')='" & VB6.Format(lblRunDate.Text, "MMYYYY") & "'"



        ''''ORDER CLAUSE...						

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,TO_CHAR(IH.AUTO_KEY_DELV)"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
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
    Private Function Update1() As Boolean
        On Error GoTo UpdateErr
        Dim SqlStr As String
        Dim mRefNo As String
        Dim mAppStatus As String
        Dim I As Integer
        Dim pSONo As Double
        Dim pCustomerCode As String
        Dim pScheduleDate As String


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColRefNo
                mRefNo = CStr(Val(.Text))

                .Col = ColCustomerCode
                pCustomerCode = Trim(.Text)

                .Col = ColOurSONo
                pSONo = Val(.Text)

                .Col = ColScheduleDate
                pScheduleDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColAppStatus
                mAppStatus = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                If mAppStatus = "Y" And Val(mRefNo) > 0 Then

                    If lblBookType.Text = "P" Then
                        SqlStr = "UPDATE DSP_DELV_SCHLD_REQ_HDR SET " & vbCrLf _
                            & " PH_USERID='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                            & " PH_USERDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " APPROVAL_PH='" & mAppStatus & "', " & vbCrLf _
                            & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                            & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND AUTO_KEY_DELV =" & Val(mRefNo) & ""

                        PubDBCn.Execute(SqlStr)
                    Else
                        SqlStr = "UPDATE DSP_DELV_SCHLD_REQ_HDR SET " & vbCrLf _
                            & " BH_USERID='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                            & " BH_USERDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " APPROVAL_BH='" & mAppStatus & "', " & vbCrLf _
                            & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                            & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND AUTO_KEY_DELV =" & Val(mRefNo) & ""

                        PubDBCn.Execute(SqlStr)
                        If UpdateDeliverySchedule(Val(mRefNo), Val(CStr(pSONo)), pCustomerCode, pScheduleDate) = False Then GoTo UpdateErr

                    End If
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


    Private Sub frmParamDSApproval_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Close						
        'Set PvtDBCn = Nothing						
    End Sub
    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim CntRow As Integer
            With SprdMain
                For CntRow = 1 To .MaxRows
                    .Row = CntRow
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
        Dim xRefNo As String
        Dim mCustomerCode As String
        Dim mSONo As Double
        Dim mDate As String
        Dim mAmendNo As Double

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColRefNo
        xRefNo = Me.SprdMain.Text

        SprdMain.Col = ColCustomerCode
        mCustomerCode = Me.SprdMain.Text

        SprdMain.Col = ColOurSONo
        mSONo = CDbl(Me.SprdMain.Text)

        SprdMain.Col = ColScheduleDate
        mDate = Me.SprdMain.Text

        SprdMain.Col = ColAmendNo
        mAmendNo = CDbl(Me.SprdMain.Text)

        frmParamSalesDSView.lblMkey.Text = xRefNo
        frmParamSalesDSView.lblCustomerCode.Text = mCustomerCode
        frmParamSalesDSView.lblSONo.Text = CStr(mSONo)
        frmParamSalesDSView.lblDate.Text = mDate
        frmParamSalesDSView.lblAmendNo.Text = CStr(mAmendNo)
        frmParamSalesDSView.ShowDialog()
        frmParamSalesDSView.frmParamSalesDSView_Activated(Nothing, New System.EventArgs())

    End Sub

    Private Sub txtCustomer_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.DoubleClick
        SearchCustomer()
    End Sub
    Private Sub SearchCustomer()
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        ''MainClass.SearchMaster txtCustomer, "FIN_SUPP_CUST_MST", "NAME", SqlStr						
        MainClass.SearchGridMaster(txtCustomer.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE",  ,  , SqlStr)
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
        Dim SqlStr As String

        If txtCustomer.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((txtCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
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
    Private Function UpdateDeliverySchedule(ByRef pRefNo As Double, ByRef pSONo As Double, ByRef pCustomerCode As String, ByRef pScheduleDate As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mDSNo As Double
        Dim pAddMode As Boolean
        Dim pAmendNo As Long

        'SqlStr = "SELECT AUTO_KEY_DELV FROM "

        SqlStr = "SELECT AUTO_KEY_DELV " & vbCrLf _
            & " FROM DSP_DELV_SCHLD_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & pCustomerCode & "'" & vbCrLf _
            & " AND AUTO_KEY_SO=" & Val(CStr(pSONo)) & "" & vbCrLf _
            & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(pScheduleDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenKeyset, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = True Then
            pAddMode = True
            mDSNo = AutoGenPONoSeq()
            'pAmendNo = 0
        Else
            pAddMode = False
            mDSNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_DELV").Value), -1, RsTemp.Fields("AUTO_KEY_DELV").Value)
            'pAmendNo = IIf(IsDBNull(RsTemp.Fields("DELV_AMEND_NO").Value), 0, RsTemp.Fields("DELV_AMEND_NO").Value)
        End If


        If pAddMode = True Then
            SqlStr = " INSERT INTO DSP_DELV_SCHLD_HDR ( " & vbCrLf _
                & " COMPANY_CODE , AUTO_KEY_DELV," & vbCrLf _
                & " DELV_SCHLD_DATE ,  CUST_DELV_NO," & vbCrLf _
                & " CUST_DELV_DATE , AUTO_KEY_SO," & vbCrLf _
                & " SO_DATE , CUST_SO_NO," & vbCrLf _
                & " CUST_SO_DATE , SO_AMEND_NO," & vbCrLf _
                & " AMEND_DATE , AMEND_WEF_DATE," & vbCrLf _
                & " SUPP_CUST_CODE , SCHLD_DATE," & vbCrLf _
                & " DELV_AMEND_NO , DELV_AMEND_DATE, " & vbCrLf _
                & " SCHLD_STATUS , REMARKS, IS_MAIL, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE, " & vbCrLf _
                & " APPROVAL_BH, APPROVAL_PH, PH_USERID, PH_USERDATE, BH_USERID, BH_USERDATE) "

            SqlStr = SqlStr & vbCrLf _
                & " SELECT " & vbCrLf _
                & " COMPANY_CODE, " & mDSNo & ", DELV_SCHLD_DATE, " & vbCrLf _
                & " CUST_DELV_NO, CUST_DELV_DATE, AUTO_KEY_SO, " & vbCrLf _
                & " SO_DATE, CUST_SO_NO, CUST_SO_DATE, " & vbCrLf _
                & " SO_AMEND_NO, AMEND_DATE, AMEND_WEF_DATE, " & vbCrLf _
                & " SUPP_CUST_CODE, SCHLD_DATE, DELV_AMEND_NO, DELV_AMEND_DATE, SCHLD_STATUS, " & vbCrLf _
                & " REMARKS, 'N', ADDUSER, ADDDATE, " & vbCrLf _
                & " MODUSER, MODDATE,  " & vbCrLf _
                & " APPROVAL_BH, APPROVAL_PH, PH_USERID, " & vbCrLf _
                & " PH_USERDATE, BH_USERID, BH_USERDATE " & vbCrLf _
                & " FROM DSP_DELV_SCHLD_REQ_HDR" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND AUTO_KEY_DELV=" & Val(CStr(pRefNo)) & ""
        Else


            SqlStr = " UPDATE DSP_DELV_SCHLD_HDR SET ( " & vbCrLf _
                & " CUST_DELV_NO, CUST_DELV_DATE, DELV_AMEND_NO, DELV_AMEND_DATE, " & vbCrLf _
                & " AUTO_KEY_SO, SO_DATE, CUST_SO_NO, CUST_SO_DATE, SO_AMEND_NO, " & vbCrLf _
                & " AMEND_DATE, AMEND_WEF_DATE, SUPP_CUST_CODE, " & vbCrLf _
                & " SCHLD_DATE, SCHLD_STATUS, REMARKS, APPROVAL_BH, " & vbCrLf _
                & " APPROVAL_PH, PH_USERID, PH_USERDATE, " & vbCrLf _
                & " BH_USERID, MODUSER, MODDATE ) = "

            SqlStr = SqlStr & vbCrLf _
                & " ( SELECT CUST_DELV_NO, CUST_DELV_DATE, DELV_AMEND_NO, DELV_AMEND_DATE, " & vbCrLf _
                & " AUTO_KEY_SO, SO_DATE, CUST_SO_NO, CUST_SO_DATE, SO_AMEND_NO, " & vbCrLf _
                & " AMEND_DATE, AMEND_WEF_DATE, SUPP_CUST_CODE, " & vbCrLf _
                & " SCHLD_DATE, SCHLD_STATUS, REMARKS, APPROVAL_BH, " & vbCrLf _
                & " APPROVAL_PH, PH_USERID, PH_USERDATE, " & vbCrLf _
                & " BH_USERID, MODUSER, MODDATE " & vbCrLf _
                & " FROM DSP_DELV_SCHLD_REQ_HDR " & vbCrLf _
                & " WHERE AUTO_KEY_DELV=" & Val(CStr(pRefNo)) & "" & vbCrLf _
                & " )"

            SqlStr = SqlStr & vbCrLf _
                & " WHERE AUTO_KEY_DELV=" & mDSNo & ""

        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(pRefNo, mDSNo) = False Then GoTo ErrPart
        '    If UpdateDailyDSDetail(pRefNo, mDSNo) = False Then GoTo ErrPart						

        UpdateDeliverySchedule = True

        Exit Function
ErrPart:
        UpdateDeliverySchedule = False
        MsgBox(Err.Description)
        ''Resume						
    End Function

    Private Function UpdateDetail1(ByRef pRefNo As Double, ByRef mDSNo As Double) As Boolean
        On Error GoTo UpdateDetail1
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = "DELETE FROM DSP_DAILY_SCHLD_DET  " & vbCrLf _
            & " WHERE AUTO_KEY_DELV=" & Val(CStr(mDSNo)) & " "

        PubDBCn.Execute(SqlStr)


        SqlStr = "DELETE FROM DSP_DELV_SCHLD_DET " & vbCrLf _
            & " Where " & vbCrLf _
            & " AUTO_KEY_DELV=" & Val(CStr(mDSNo)) & ""

        PubDBCn.Execute(SqlStr)


        SqlStr = " INSERT INTO DSP_DELV_SCHLD_DET ( " & vbCrLf _
            & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf _
            & " ITEM_UOM, " & vbCrLf _
            & " WEEK1_QTY, WEEK2_QTY, " & vbCrLf _
            & " WEEK3_QTY, WEEK4_QTY, " & vbCrLf _
            & " WEEK5_QTY, " & vbCrLf _
            & " ITEM_QTY, AMEND_NO, COMPANY_CODE,AMEND_REASON, LOC_CODE) "

        SqlStr = SqlStr & vbCrLf _
            & " SELECT " & vbCrLf _
            & " " & Val(CStr(mDSNo)) & ", SERIAL_NO, ITEM_CODE, " & vbCrLf _
            & " ITEM_UOM, " & vbCrLf & " WEEK1_QTY, WEEK2_QTY, " & vbCrLf _
            & " WEEK3_QTY, WEEK4_QTY, " & vbCrLf _
            & " WEEK5_QTY, " & vbCrLf _
            & " ITEM_QTY, AMEND_NO, COMPANY_CODE,AMEND_REASON, LOC_CODE" & vbCrLf _
            & " FROM DSP_DELV_SCHLD_REQ_DET" & vbCrLf _
            & " WHERE AUTO_KEY_DELV=" & Val(CStr(pRefNo)) & "" & vbCrLf
        PubDBCn.Execute(SqlStr)


        SqlStr = "INSERT INTO DSP_DAILY_SCHLD_DET (" & vbCrLf _
            & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf _
            & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
            & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE,REQ_DATE,LOC_CODE, OD_NO, BOOKTYPE )" & vbCrLf _
            & " SELECT " & vbCrLf _
            & " " & Val(CStr(mDSNo)) & ", SERIAL_NO, ITEM_CODE, " & vbCrLf _
            & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
            & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE,REQ_DATE,LOC_CODE, OD_NO, BOOKTYPE " & vbCrLf _
            & " FROM DSP_DAILY_SCHLD_REQ_DET " & vbCrLf _
            & " WHERE AUTO_KEY_DELV=" & Val(CStr(pRefNo)) & ""

        PubDBCn.Execute(SqlStr)

        SqlStr = "INSERT INTO DSP_DAILY_SCHLD_LOG_DET (" & vbCrLf _
                    & " AUTO_KEY_DELV, AMEND_NO, SERIAL_NO, ITEM_CODE, " & vbCrLf _
                    & " SERIAL_DATE, PLANNED_QTY, LOC_CODE, OD_NO,BOOKTYPE,MODUSER, MODDATE)" & vbCrLf _
                    & " SELECT " & Val(CStr(mDSNo)) & ", B.DELV_AMEND_NO," & vbCrLf _
                    & " A.SERIAL_NO, A.ITEM_CODE, A.SERIAL_DATE, A.PLANNED_QTY," & vbCrLf _
                    & " A.LOC_CODE, A.OD_NO, A.BOOKTYPE, '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " FROM DSP_DAILY_SCHLD_REQ_DET A,  DSP_DELV_SCHLD_REQ_HDR B" & vbCrLf _
                    & " WHERE A.AUTO_KEY_DELV = B.AUTO_KEY_DELV AND A.AUTO_KEY_DELV=" & Val(CStr(pRefNo)) & ""


        PubDBCn.Execute(SqlStr)

        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume						
    End Function
    Private Function AutoGenPONoSeq() As Double
        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Integer
        Dim SqlStr As String
        Dim mMaxValue As String

        mAutoGen = 1

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_DELV)  " & vbCrLf _
            & " FROM DSP_DELV_SCHLD_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_DELV,LENGTH(AUTO_KEY_DELV)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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

        AutoGenPONoSeq = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function
End Class
