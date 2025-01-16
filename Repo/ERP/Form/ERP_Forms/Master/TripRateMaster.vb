Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTripRateMaster
    Inherits System.Windows.Forms.Form
    Dim RsTripRateMain As ADODB.Recordset
    Dim RsTripRateDetail As ADODB.Recordset
    Dim RsTripTPRateDetail As ADODB.Recordset

    Dim xMyMenu As String

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColTransportName As Short = 1

    Private Const ColVehicleNo As Short = 1
    Private Const ColVehicleType As Short = 2
    Private Const ColPerKGRate As Short = 3
    Private Const ColTripRate As Short = 4
    Private Const ColPickupRate As Short = 5
    Private Const ColBackRate As Short = 6
    Private Const ColPointRate As Short = 7
    Private Const ColOTRate As Short = 8
    Dim mAmendStatus As Boolean

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""

        SqlStr = " SELECT IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, IH.BILL_TO_LOC_ID, AMEND_NO, TO_CHAR(WEF,'DD/MM/YYYY') AS WEF, DEFAULT_TRIP_RATE,DEFAULT_PER_KG_RATE,DEFAULT_PICKUP_RATE " & vbCrLf _
            & " FROM FIN_VEHICLE_RATE_HDR IH, FIN_SUPP_CUST_MST CMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " ORDER BY IH.SUPP_CUST_CODE, AMEND_NO "

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 10)
            .set_ColWidth(2, 40)
            .set_ColWidth(3, 10)
            .set_ColWidth(4, 15)
            .set_ColWidth(5, 10)
            .set_ColWidth(6, 10)

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle	
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mOldAmendNo As Integer
        Dim mLastestWEF As String

        FieldsVarification = True
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsTripRateMain.EOF = True Then Exit Function

        If MODIFYMode = True And chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed Operation Rates Cann't be Modified")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtCustomerCode.Text) = "" Then
            MsgBox("Customer Code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtCustomerCode.Enabled = True Then txtCustomerCode.Focus()
            Exit Function
        End If

        If Trim(txtBillTo.Text) = "" Then
            MsgInformation("Bill To is blank. Cannot Save")
            txtBillTo.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'") = False Then
                MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
                txtBillTo.Focus()
                FieldsVarification = False
            End If
        End If

        If Trim(txtWEF.Text) = "" Then
            MsgBox("W.E.F is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtWEF.Enabled = True Then txtWEF.Focus()
            Exit Function
        End If

        If Val(txtAmendNo.Text) > 0 Then
            mOldAmendNo = Val(txtAmendNo.Text) - 1
            If MainClass.ValidateWithMasterTable(txtCustomerCode.Text, "SUPP_CUST_CODE", "WEF", "FIN_VEHICLE_RATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AMEND_NO=" & Val(mOldAmendNo) & "") = True Then
                mLastestWEF = MasterNo

                If CDate(txtWEF.Text) <= CDate(mLastestWEF) Then
                    MsgInformation("W.E.F Cann't be less than or equal to Last WEF.")
                    FieldsVarification = False
                    If txtWEF.Enabled = True Then txtWEF.Focus()
                    Exit Function
                End If
            End If
        End If

        '    If MainClass.ValidDataInGrid(SprdMain, ColVehicleNo, "S", "Vehicle No Is Blank") = False Then FieldsVarification = False: Exit Function	
        '    If MainClass.ValidDataInGrid(SprdMain, ColTripRate, "N", "Trip Rate Is Blank") = False Then FieldsVarification = False: Exit Function	


        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtCustomerCode.Enabled = True
            cmdSearchCustCode.Enabled = True
            cmdSearchWEF.Enabled = True
            SprdMain.Enabled = True
            SprdMain2.Enabled = True
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdAmend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAmend.Click

        On Error GoTo ERR1
        Dim mCustomerCode As String
        Dim i As Integer

        mCustomerCode = Trim(txtCustomerCode.Text)

        If mCustomerCode = "" Then
            MsgInformation("Please Select Item")
            Exit Sub
        End If

        Call txtCustomerCode_Validating(txtCustomerCode, New System.ComponentModel.CancelEventArgs(True))

        txtAmendNo.Text = CStr(GetMaxAmendNo(mCustomerCode))
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        mAmendStatus = True
        cmdAmend.Enabled = False
        txtWEF.Enabled = True
        SprdMain.Enabled = True
        SprdMain2.Enabled = True
        ADDMode = True
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsTripRateMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click

        On Error GoTo DelErrPart

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed Operation Rates Cann't be Deleted")
            Exit Sub
        End If

        If Trim(txtCustomerCode.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        PubDBCn.Cancel()
        PubDBCn.BeginTrans()

        If Not RsTripRateMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then

                If InsertIntoDelAudit(PubDBCn, "FIN_VEHICLE_RATE_HDR", (lblMKey.Text), RsTripRateMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "FIN_VEHICLE_RATE_HDR", "MKEY", (lblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM FIN_VEHICLE_RATE_DET WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM FIN_VEHICLE_TP_RATE_DET WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM FIN_VEHICLE_RATE_HDR  WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

                If Val(txtAmendNo.Text) > 0 Then
                    If UpdatePreviousRate((txtCustomerCode.Text), Val(txtAmendNo.Text), "O") = False Then GoTo DelErrPart
                End If

                PubDBCn.CommitTrans()
                RsTripRateMain.Requery()
                RsTripRateDetail.Requery()
                RsTripTPRateDetail.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsTripRateMain.Requery()
        RsTripRateDetail.Requery()
        RsTripTPRateDetail.Requery()
        PubDBCn.Cancel()
        MsgBox(Err.Description)
    End Sub

    Private Function UpdatePreviousRate(ByRef pCustomerCode As String, ByRef pAmendNo As Integer, ByRef pPreviousStatus As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = " UPDATE FIN_VEHICLE_RATE_HDR SET " & vbCrLf _
            & " STATUS = '" & pPreviousStatus & "', " & vbCrLf _
            & " MODUSER = '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
            & " MODDATE = TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND AMEND_NO = " & pAmendNo - 1 & "" & vbCrLf _
            & " AND SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(pCustomerCode) & "'"

        PubDBCn.Execute(SqlStr)

        UpdatePreviousRate = True

        Exit Function
ErrPart:
        UpdatePreviousRate = False
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
    End Function

    Private Function GetMaxAmendNo(ByRef pCustomerCode As String) As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT MAX(AMEND_NO) AS AMEND_NO" & vbCrLf _
        & " FROM FIN_VEHICLE_RATE_HDR" & vbCrLf _
        & " WHERE " & vbCrLf _
        & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustomerCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDBNull(RsTemp.Fields("AMEND_NO").Value) Then
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

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsTripRateMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtCustomerCode.Enabled = False
            cmdSearchCustCode.Enabled = False
            SprdMain.Enabled = True
            SprdMain2.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintOprRate(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintOprRate(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub PrintOprRate(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRsTemp As ADODB.Recordset = Nothing

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "Product Wise Operation Rate Master"

        SqlStr = " SELECT FIN_VEHICLE_RATE_HDR.*,FIN_VEHICLE_RATE_DET.*,FIN_SUPP_CUST_MST.*,PRD_OPR_MST.* " & vbCrLf & " FROM FIN_VEHICLE_RATE_HDR, FIN_VEHICLE_RATE_DET, FIN_SUPP_CUST_MST, PRD_OPR_MST " & vbCrLf & " WHERE FIN_VEHICLE_RATE_HDR.MKEY=FIN_VEHICLE_RATE_DET.MKEY " & vbCrLf & " AND FIN_VEHICLE_RATE_HDR.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE(+) " & vbCrLf & " AND FIN_VEHICLE_RATE_HDR.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE(+) " & vbCrLf & " AND FIN_VEHICLE_RATE_DET.COMPANY_CODE=PRD_OPR_MST.COMPANY_CODE(+) " & vbCrLf & " AND FIN_VEHICLE_RATE_DET.OPR_CODE=PRD_OPR_MST.OPR_CODE(+) " & vbCrLf & " AND FIN_VEHICLE_RATE_HDR.MKEY='" & MainClass.AllowSingleQuote(lblMKey.Text) & "' ORDER BY SERIAL_NO"

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\VehicleRate.rpt"

        SetCrpt(Report1, Mode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub



    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtCustomerCode_Validating(txtCustomerCode, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True And CmdAdd.Visible = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            GoTo ErrorHandler
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        MsgBox(Err.Description)
        Resume
    End Sub

    Private Sub cmdSearchCustCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCustCode.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtCustomerName.Text, "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", , SqlStr) = True Then
            txtCustomerName.Text = AcName
            txtCustomerCode.Text = AcName1
            txtBillTo.Text = AcName2
            txtCustomerCode_Validating(txtCustomerCode, New System.ComponentModel.CancelEventArgs(False))
            If txtCustomerCode.Enabled = True Then txtCustomerCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchWEF_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchWEF.Click
        On Error GoTo SrchERR
        Dim SqlStr As String = ""

        SqlStr = " SELECT IH.WEF, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME" & vbCrLf & " FROM FIN_VEHICLE_RATE_HDR IH, FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtCustomerCode.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "'"
        End If

        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtWEF.Text = Format(AcName, "DD/MM/YYYY")
            txtCustomerCode.Text = AcName1
            If txtWEF.Enabled = True Then txtWEF.Focus()

            txtCustomerCode_Validating(txtCustomerCode, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub frmTripRateMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub

        Me.Text = "Customer Wise Trip Rate Master"

        SqlStr = "Select * from FIN_VEHICLE_RATE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTripRateMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from FIN_VEHICLE_RATE_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTripRateDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from FIN_VEHICLE_TP_RATE_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTripTPRateDetail, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SetTextLengths()

        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmTripRateMaster_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmTripRateMaster_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmTripRateMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7590)
        Me.Width = VB6.TwipsToPixelsX(11385)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsTripRateMain
            txtCustomerCode.MaxLength = .Fields("SUPP_CUST_CODE").DefinedSize
            txtCustomerName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtWEF.MaxLength = .Fields("WEF").DefinedSize - 6
            txtRemarks.MaxLength = .Fields("Remarks").DefinedSize
            txtTripRate.MaxLength = .Fields("DEFAULT_TRIP_RATE").Precision
            txtPremiumRate.MaxLength = .Fields("PREMIUM_RATE").Precision
            txtBackRate.MaxLength = .Fields("DEFAULT_BACK_RATE").Precision
            txtOTRate.MaxLength = .Fields("DEFAULT_OT_RATE").Precision
            txtPointRate.MaxLength = .Fields("DEFAULT_POINT_RATE").Precision
            txtBillTo.MaxLength = .Fields("BILL_TO_LOC_ID").DefinedSize
            txtDefaultRatePerKG.MaxLength = .Fields("DEFAULT_PER_KG_RATE").Precision
            txtDefaultPickupRate.MaxLength = .Fields("DEFAULT_PICKUP_RATE").Precision

        End With
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume	
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtCustomerCode.Enabled = mMode
        cmdSearchCustCode.Enabled = mMode
        txtWEF.Enabled = mMode
        cmdSearchWEF.Enabled = True '' mMode	

        txtCustomerName.Enabled = False
        txtAmendNo.Enabled = False
    End Sub

    Private Sub frmTripRateMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsTripRateMain.Close()
        RsTripRateDetail.Close()
        RsTripTPRateDetail.Close()

        RsTripRateMain = Nothing
        RsTripRateDetail = Nothing
        RsTripTPRateDetail = Nothing

    End Sub

    Private Sub Clear1()

        lblMKey.Text = ""
        txtCustomerCode.Text = ""
        txtCustomerName.Text = ""
        txtWEF.Text = ""
        lblWEF.Text = ""
        txtRemarks.Text = ""
        txtAmendNo.Text = "0"
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtTripRate.Text = ""
        txtBackRate.Text = ""
        txtPointRate.Text = ""
        txtOTRate.Text = ""
        txtPremiumRate.Text = ""
        txtBillTo.Text = ""
        txtDefaultRatePerKG.Text = ""
        txtDefaultPickupRate.Text = ""

        chkStatus.Enabled = False
        mAmendStatus = False
        cmdAmend.Enabled = True

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)

        MainClass.ClearGrid(SprdMain2)
        FormatSprdMain2(-1)

        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsTripRateMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FormatSprdMain(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2)
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColVehicleNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsTripRateDetail.Fields("VEHICLE_NO").DefinedSize
            .set_ColWidth(.Col, 25)

            .Col = ColVehicleType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("VEHICLE_TYPE", "FIN_VEHICLE_MST", PubDBCn)
            .set_ColWidth(.Col, 12)

            For cntCol = ColPerKGRate To ColOTRate
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMax = CDbl("99999.99")
                .TypeFloatMin = CDbl("-99999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 12)
            Next


        End With
        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColVehicleType, ColVehicleType)

        '    MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColVehicleNo, ColBackRate	
        '    MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColTotalOldRate, ColTotalOldRate	

        MainClass.SetSpreadColor(SprdMain, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsTripRateDetail.Requery()
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdMain2(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain2
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2)
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColTransportName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsTripTPRateDetail.Fields("TRANSPORT_NAME").DefinedSize
            .set_ColWidth(.Col, 25)

            .Col = ColVehicleType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("NAME", "FIN_VEHICLETYPE_MST", PubDBCn)
            .set_ColWidth(.Col, 12)

            For cntCol = ColPerKGRate To ColOTRate
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMax = CDbl("99999.99")
                .TypeFloatMin = CDbl("-99999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 12)
            Next


        End With
        MainClass.UnProtectCell(SprdMain2, 1, SprdMain2.MaxRows, 1, SprdMain2.MaxCols)
        '    MainClass.ProtectCell SprdMain2, 1, SprdMain2.MaxRows, ColVehicleType, ColVehicleType	

        '    MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColVehicleNo, ColBackRate	
        '    MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColTotalOldRate, ColTotalOldRate	

        MainClass.SetSpreadColor(SprdMain2, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsTripTPRateDetail.Requery()
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1

        With RsTripRateMain
            If Not .EOF Then
                ADDMode = False
                MODIFYMode = False
                lblMKey.Text = .Fields("MKey").Value

                txtCustomerCode.Text = Trim(IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value))
                If MainClass.ValidateWithMasterTable(txtCustomerCode.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCustomerName.Text = MasterNo
                End If
                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)
                txtWEF.Text = IIf(IsDBNull(.Fields("WEF").Value), "", .Fields("WEF").Value)
                lblWEF.Text = IIf(IsDBNull(.Fields("WEF").Value), "", .Fields("WEF").Value)
                txtAmendNo.Text = IIf(IsDBNull(.Fields("AMEND_NO").Value), "", .Fields("AMEND_NO").Value)
                chkStatus.CheckState = IIf(.Fields("Status").Value = "O", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                txtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtTripRate.Text = IIf(IsDBNull(.Fields("DEFAULT_TRIP_RATE").Value), "", VB6.Format(.Fields("DEFAULT_TRIP_RATE").Value, "0.00"))
                txtPremiumRate.Text = IIf(IsDBNull(.Fields("PREMIUM_RATE").Value), "", VB6.Format(.Fields("PREMIUM_RATE").Value, "0.00"))
                txtBackRate.Text = IIf(IsDBNull(.Fields("DEFAULT_BACK_RATE").Value), "", VB6.Format(.Fields("DEFAULT_BACK_RATE").Value, "0.00"))
                txtPointRate.Text = IIf(IsDBNull(.Fields("DEFAULT_POINT_RATE").Value), "", VB6.Format(.Fields("DEFAULT_POINT_RATE").Value, "0.00"))
                txtOTRate.Text = IIf(IsDBNull(.Fields("DEFAULT_OT_RATE").Value), "", VB6.Format(.Fields("DEFAULT_OT_RATE").Value, "0.00"))

                txtDefaultRatePerKG.Text = IIf(IsDBNull(.Fields("DEFAULT_PER_KG_RATE").Value), "", VB6.Format(.Fields("DEFAULT_PER_KG_RATE").Value, "0.00"))
                txtDefaultPickupRate.Text = IIf(IsDBNull(.Fields("DEFAULT_PICKUP_RATE").Value), "", VB6.Format(.Fields("DEFAULT_PICKUP_RATE").Value, "0.00"))


                Call ShowDetail()
                Call ShowTPDetail()
                Call MakeEnableDesableField(False)
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsTripRateMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        SprdMain2.Enabled = False
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowDetail()

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mVehicleCode As Double
        Dim mVehicleNo As String
        Dim mVehicleType As String

        SqlStr = ""

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM FIN_VEHICLE_RATE_DET " & vbCrLf & " WHERE " & vbCrLf & " MKEY='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTripRateDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTripRateDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1	
            i = 1
            '        .MoveFirst	

            Do While Not .EOF

                SprdMain.Row = i

                mVehicleCode = IIf(IsDBNull(.Fields("VEHICLE_CODE").Value), "", .Fields("VEHICLE_CODE").Value)

                If MainClass.ValidateWithMasterTable(mVehicleCode, "CODE", "NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mVehicleNo = Trim(MasterNo)
                Else
                    mVehicleNo = ""
                End If

                If MainClass.ValidateWithMasterTable(mVehicleCode, "CODE", "VEHICLE_TYPE", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mVehicleType = Trim(MasterNo)
                Else
                    mVehicleType = ""
                End If

                SprdMain.Col = ColVehicleNo
                SprdMain.Text = mVehicleNo

                SprdMain.Col = ColVehicleType
                SprdMain.Text = mVehicleType

                SprdMain.Col = ColPerKGRate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("PER_KG_RATE").Value), 0, .Fields("PER_KG_RATE").Value), "0.00")

                SprdMain.Col = ColTripRate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("TRIP_RATE").Value), 0, .Fields("TRIP_RATE").Value), "0.00")

                SprdMain.Col = ColPickupRate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("PICKUP_RATE").Value), 0, .Fields("PICKUP_RATE").Value), "0.00")

                SprdMain.Col = ColBackRate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("BACK_RATE").Value), 0, .Fields("BACK_RATE").Value), "0.00")

                SprdMain.Col = ColPointRate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("POINT_RATE").Value), 0, .Fields("POINT_RATE").Value), "0.00")

                SprdMain.Col = ColOTRate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("OT_RATE").Value), 0, .Fields("OT_RATE").Value), "0.00")

                .MoveNext()

                i = i + 1
                SprdMain.MaxRows = i
            Loop
        End With

        FormatSprdMain(-1)
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowTPDetail()

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""

        Dim mTransporterCode As Double
        Dim mTransporterName As String

        Dim mVTCode As Double
        Dim mVTName As String

        SqlStr = ""

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM FIN_VEHICLE_TP_RATE_DET " & vbCrLf & " WHERE " & vbCrLf & " MKEY='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTripTPRateDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTripTPRateDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1	
            i = 1
            '        .MoveFirst	

            Do While Not .EOF

                SprdMain2.Row = i

                mTransporterCode = IIf(IsDBNull(.Fields("TRANSPORT_CODE").Value), "", .Fields("TRANSPORT_CODE").Value)

                If MainClass.ValidateWithMasterTable(mTransporterCode, "TRANSPORTER_CODE", "TRANSPORTER_NAME", "FIN_TRANSPORTER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTransporterName = Trim(MasterNo)
                Else
                    mTransporterName = ""
                End If

                mVTCode = IIf(IsDBNull(.Fields("VT_CODE").Value), "", .Fields("VT_CODE").Value)

                If MainClass.ValidateWithMasterTable(mVTCode, "CODE", "NAME", "FIN_VEHICLETYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mVTName = Trim(MasterNo)
                Else
                    mVTName = ""
                End If

                SprdMain2.Col = ColTransportName
                SprdMain2.Text = mTransporterName

                SprdMain2.Col = ColVehicleType
                SprdMain2.Text = mVTName

                SprdMain2.Col = ColPerKGRate
                SprdMain2.Text = VB6.Format(IIf(IsDBNull(.Fields("PER_KG_RATE").Value), 0, .Fields("PER_KG_RATE").Value), "0.00")


                SprdMain2.Col = ColTripRate
                SprdMain2.Text = VB6.Format(IIf(IsDBNull(.Fields("TRIP_RATE").Value), 0, .Fields("TRIP_RATE").Value), "0.00")

                SprdMain2.Col = ColPickupRate
                SprdMain2.Text = VB6.Format(IIf(IsDBNull(.Fields("PICKUP_RATE").Value), 0, .Fields("PICKUP_RATE").Value), "0.00")


                SprdMain2.Col = ColBackRate
                SprdMain2.Text = VB6.Format(IIf(IsDBNull(.Fields("BACK_RATE").Value), 0, .Fields("BACK_RATE").Value), "0.00")

                SprdMain2.Col = ColPointRate
                SprdMain2.Text = VB6.Format(IIf(IsDBNull(.Fields("POINT_RATE").Value), 0, .Fields("POINT_RATE").Value), "0.00")

                SprdMain2.Col = ColOTRate
                SprdMain2.Text = VB6.Format(IIf(IsDBNull(.Fields("OT_RATE").Value), 0, .Fields("OT_RATE").Value), "0.00")

                .MoveNext()

                i = i + 1
                SprdMain2.MaxRows = i
            Loop
        End With

        FormatSprdMain2(-1)
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mMKey As String
        Dim mStatus As String

        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked, "O", "C")

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If ADDMode = True Then
            mMKey = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") & "/" & UCase(Trim(txtCustomerCode.Text)) & "/" & VB6.Format(txtAmendNo.Text, "000")
            lblMKey.Text = mMKey


            SqlStr = " INSERT INTO FIN_VEHICLE_RATE_HDR (" & vbCrLf _
                & " MKEY, COMPANY_CODE, SUPP_CUST_CODE, " & vbCrLf _
                & " WEF, AMEND_NO, DEFAULT_TRIP_RATE, PREMIUM_RATE, " & vbCrLf _
                & " DEFAULT_BACK_RATE, DEFAULT_POINT_RATE, DEFAULT_OT_RATE," & vbCrLf _
                & " REMARKS, STATUS, " & vbCrLf _
                & " ADDUSER, ADDDATE, " & vbCrLf _
                & " MODUSER, MODDATE,DEFAULT_PER_KG_RATE, DEFAULT_PICKUP_RATE,BILL_TO_LOC_ID) VALUES ( "

            SqlStr = SqlStr & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mMKey) & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')," & Val(txtAmendNo.Text) & ", " & vbCrLf _
                & " " & Val(txtTripRate.Text) & ", " & Val(txtPremiumRate.Text) & ", " & vbCrLf _
                & " " & Val(txtBackRate.Text) & ", " & Val(txtPointRate.Text) & ", " & Val(txtOTRate.Text) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', '" & mStatus & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'',''," & Val(txtDefaultRatePerKG.Text) & "," & Val(txtDefaultPickupRate.Text) & ",'" & MainClass.AllowSingleQuote(txtBillTo.Text) & "')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = " UPDATE FIN_VEHICLE_RATE_HDR  SET " & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "', " & vbCrLf _
                & " WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " AMEND_NO=" & Val(txtAmendNo.Text) & ", BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'," & vbCrLf _
                & " DEFAULT_TRIP_RATE=" & Val(txtTripRate.Text) & ", PREMIUM_RATE=" & Val(txtPremiumRate.Text) & "," & vbCrLf _
                & " DEFAULT_BACK_RATE=" & Val(txtBackRate.Text) & ", " & vbCrLf _
                & " DEFAULT_POINT_RATE=" & Val(txtPointRate.Text) & ", " & vbCrLf _
                & " DEFAULT_OT_RATE=" & Val(txtOTRate.Text) & ", " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " STATUS='" & mStatus & "', DEFAULT_PER_KG_RATE=" & Val(txtDefaultRatePerKG.Text) & ",DEFAULT_PICKUP_RATE=" & Val(txtDefaultPickupRate.Text) & "," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE Mkey ='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'"

        End If



        PubDBCn.Execute(SqlStr)
        If UpdateDetail1() = False Then GoTo ErrPart
        If UpdateTPDetail1() = False Then GoTo ErrPart

        If Val(txtAmendNo.Text) > 0 Then
            If UpdatePreviousRate((txtCustomerCode.Text), Val(txtAmendNo.Text), "C") = False Then GoTo ErrPart
        End If

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsTripRateMain.Requery()
        RsTripRateDetail.Requery()
        RsTripTPRateDetail.Requery()
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
    End Function

    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mVehicleCode As Double
        Dim mVehicleNo As String
        Dim mTripRate As Double
        Dim mPointRate As Double
        Dim mOTRate As Double
        Dim mBackRate As Double
        Dim mPerKGRate As Double
        Dim mPickupRate As Double

        PubDBCn.Execute("DELETE FROM FIN_VEHICLE_RATE_DET  " & vbCrLf _
                        & " WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColVehicleNo
                mVehicleNo = MainClass.AllowSingleQuote(.Text)

                If MainClass.ValidateWithMasterTable(mVehicleNo, "NAME", "CODE", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mVehicleCode = Val(MasterNo)
                End If

                .Col = ColTripRate
                mTripRate = Val(.Text)

                .Col = ColBackRate
                mBackRate = Val(.Text)

                .Col = ColPointRate
                mPointRate = Val(.Text)

                .Col = ColOTRate
                mOTRate = Val(.Text)

                .Col = ColPerKGRate
                mPerKGRate = Val(.Text)

                .Col = ColPickupRate
                mPickupRate = Val(.Text)


                SqlStr = ""
                If Trim(mVehicleNo) <> "" Then
                    SqlStr = " INSERT INTO  FIN_VEHICLE_RATE_DET ( " & vbCrLf _
                        & " MKEY, COMPANY_CODE, " & vbCrLf _
                        & " SUPP_CUST_CODE, WEF, AMEND_NO, SERIAL_NO, " & vbCrLf _
                        & " VEHICLE_CODE, VEHICLE_NO, " & vbCrLf _
                        & " TRIP_RATE, BACK_RATE, POINT_RATE, OT_RATE,PER_KG_RATE, PICKUP_RATE " & vbCrLf _
                        & " ) VALUES ( " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "', TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " " & Val(txtAmendNo.Text) & "," & i & ", " & vbCrLf _
                        & " '" & mVehicleCode & "', '" & mVehicleNo & "', " & vbCrLf _
                        & " " & mTripRate & ", " & mBackRate & ", " & mPointRate & ", " & mOTRate & "," & mPerKGRate & ", " & mPickupRate & ")"

                    PubDBCn.Execute(SqlStr)
                End If

            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        MsgBox(Err.Description)
        UpdateDetail1 = False
    End Function
    Private Function UpdateTPDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mTransportorCode As Double
        Dim mTransportorName As String

        Dim mVTCode As Double
        Dim mVTName As String
        Dim mTripRate As Double
        Dim mPointRate As Double
        Dim mOTRate As Double
        Dim mBackRate As Double
        Dim mPerKGRate As Double
        Dim mPickupRate As Double

        PubDBCn.Execute("DELETE FROM FIN_VEHICLE_TP_RATE_DET  " & vbCrLf _
                        & " WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")


        With SprdMain2
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColTransportName
                mTransportorName = MainClass.AllowSingleQuote(.Text)

                If MainClass.ValidateWithMasterTable(mTransportorName, "TRANSPORTER_NAME", "TRANSPORTER_CODE", "FIN_TRANSPORTER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTransportorCode = Val(MasterNo)
                End If

                .Col = ColVehicleType
                mVTName = MainClass.AllowSingleQuote(.Text)

                If MainClass.ValidateWithMasterTable(mVTName, "NAME", "CODE", "FIN_VEHICLETYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mVTCode = Val(MasterNo)
                End If

                .Col = ColTripRate
                mTripRate = Val(.Text)

                .Col = ColBackRate
                mBackRate = Val(.Text)

                .Col = ColPointRate
                mPointRate = Val(.Text)

                .Col = ColOTRate
                mOTRate = Val(.Text)

                .Col = ColPerKGRate
                mPerKGRate = Val(.Text)

                .Col = ColPickupRate
                mPickupRate = Val(.Text)

                SqlStr = ""
                If Trim(mTransportorName) <> "" Then
                    SqlStr = " INSERT INTO  FIN_VEHICLE_TP_RATE_DET ( " & vbCrLf _
                        & " MKEY, COMPANY_CODE, " & vbCrLf _
                        & " SUPP_CUST_CODE, WEF, AMEND_NO, SERIAL_NO, " & vbCrLf _
                        & " TRANSPORT_CODE, TRANSPORT_NAME, " & vbCrLf _
                        & " VT_CODE, VT_NAME, " & vbCrLf _
                        & " TRIP_RATE, BACK_RATE, POINT_RATE, OT_RATE, PER_KG_RATE, PICKUP_RATE " & vbCrLf _
                        & " ) VALUES ( " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "', TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " " & Val(txtAmendNo.Text) & "," & i & ", " & vbCrLf _
                        & " '" & mTransportorCode & "', '" & mTransportorName & "', " & vbCrLf _
                        & " '" & mVTCode & "', '" & mVTName & "', " & vbCrLf _
                        & " " & mTripRate & ", " & mBackRate & ", " & mPointRate & ", " & mOTRate & "," & mPerKGRate & ", " & mPickupRate & ")"

                    PubDBCn.Execute(SqlStr)
                End If

            Next
        End With
        UpdateTPDetail1 = True
        Exit Function
UpdateDetail1:
        MsgBox(Err.Description)
        UpdateTPDetail1 = False
    End Function
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsTripRateMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        txtCustomerCode.Text = Trim(SprdView.Text)

        SprdView.Col = 3
        txtBillTo.Text = Trim(SprdView.Text)

        SprdView.Col = 5
        txtWEF.Text = VB6.Format(SprdView.Text, "DD/MM/YYYY")

        txtCustomerCode_Validating(txtCustomerCode, New System.ComponentModel.CancelEventArgs(False))
        ViewGrid()
    End Sub
    Private Sub SprdMain2_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain2.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""
        Dim mRMName As String
        Dim mDeleted As Boolean

        If eventArgs.row = 0 And eventArgs.col = ColVehicleNo Then
            With SprdMain
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "FIN_VEHICLE_MST", "NAME", "VEHICLE_TYPE", "TRANSPORTER_NAME", , SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColVehicleNo
                    .Text = AcName
                    .Col = ColVehicleType
                    .Text = AcName1
                End If
            End With
        End If


        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColVehicleNo)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain2_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain2.ClickEvent

        Dim SqlStr As String = ""
        Dim mRMName As String
        Dim mDeleted As Boolean

        If eventArgs.row = 0 And eventArgs.col = ColTransportName Then
            With SprdMain2
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "FIN_TRANSPORTER_MST", "TRANSPORTER_NAME", , , , SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColTransportName
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColVehicleType Then
            With SprdMain2
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "FIN_VEHICLETYPE_MST", "NAME", , , , SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColVehicleType
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain2, eventArgs.row, ColTransportName)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColVehicleNo Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColVehicleNo, 0))
        '    If KeyCode = vbKeyF1 And mCol = ColRMCode Then SprdMain_Click ColRMCode, 0	
        '    If KeyCode = vbKeyF1 And mCol = ColRMDesc Then SprdMain_Click ColRMDesc, 0	
        '    If KeyCode = vbKeyF1 And mCol = ColStockType Then SprdMain_Click ColStockType, 0	
    End Sub
    Private Sub SprdMain2_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain2.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain2.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColTransportName Then SprdMain2_ClickEvent(SprdMain2, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColTransportName, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColVehicleType Then SprdMain2_ClickEvent(SprdMain2, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColVehicleType, 0))


        '    If KeyCode = vbKeyF1 And mCol = ColRMCode Then SprdMain_Click ColRMCode, 0	
        '    If KeyCode = vbKeyF1 And mCol = ColRMDesc Then SprdMain_Click ColRMDesc, 0	
        '    If KeyCode = vbKeyF1 And mCol = ColStockType Then SprdMain_Click ColStockType, 0	
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xVehicleNo As String
        Dim mVehicleType As String

        If eventArgs.newRow = -1 Then Exit Sub
        SprdMain.Row = eventArgs.row
        If Trim(SprdMain.Text) = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColVehicleNo
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColVehicleNo
                xVehicleNo = SprdMain.Text
                If xVehicleNo = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(xVehicleNo, "NAME", "CODE", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If CheckDuplicate(xVehicleNo) = False Then
                        If MainClass.ValidateWithMasterTable(xVehicleNo, "NAME", "VEHICLE_TYPE", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mVehicleType = Trim(MasterNo)
                        Else
                            mVehicleType = ""
                        End If
                        SprdMain.Col = ColVehicleType
                        SprdMain.Text = mVehicleType

                        MainClass.AddBlankSprdRow(SprdMain, ColVehicleNo, ConRowHeight)
                        FormatSprdMain(eventArgs.row)
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColVehicleNo)
                End If
        End Select

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub SprdMain2_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain2.LeaveCell

        On Error GoTo ErrPart
        Dim xTransporterName As String
        Dim mVehicleType As String

        If eventArgs.newRow = -1 Then Exit Sub
        SprdMain2.Row = eventArgs.row
        If Trim(SprdMain2.Text) = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColTransportName
                SprdMain2.Row = SprdMain2.ActiveRow

                SprdMain2.Col = ColTransportName
                xTransporterName = SprdMain2.Text

                SprdMain2.Col = ColVehicleType
                mVehicleType = SprdMain2.Text

                If xTransporterName = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(xTransporterName, "TRANSPORTER_NAME", "TRANSPORTER_CODE", "FIN_TRANSPORTER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If CheckTPDuplicate(xTransporterName, mVehicleType, ColTransportName) = False Then
                        MainClass.AddBlankSprdRow(SprdMain2, ColTransportName, ConRowHeight)
                        FormatSprdMain2(eventArgs.row)
                    End If
                Else
                    MsgInformation("Invalid Transporter Name.")
                    MainClass.SetFocusToCell(SprdMain2, eventArgs.row, ColTransportName)
                End If

            Case ColVehicleType
                SprdMain2.Row = SprdMain2.ActiveRow

                SprdMain2.Col = ColTransportName
                xTransporterName = SprdMain2.Text

                SprdMain2.Col = ColVehicleType
                mVehicleType = SprdMain2.Text

                If xTransporterName = "" Then Exit Sub
                '            If mVehicleType = "" Then Exit Sub	

                If MainClass.ValidateWithMasterTable(mVehicleType, "NAME", "CODE", "FIN_VEHICLETYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If CheckTPDuplicate(xTransporterName, mVehicleType, ColVehicleType) = False Then
                        MainClass.AddBlankSprdRow(SprdMain2, ColTransportName, ConRowHeight)
                        FormatSprdMain2(eventArgs.row)
                    End If
                Else
                    MsgInformation("Invalid Vehicle Type.")
                    MainClass.SetFocusToCell(SprdMain2, eventArgs.row, ColVehicleType)
                End If
        End Select


        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function CheckDuplicate(ByRef mVehicleCode As String) As Boolean

        On Error GoTo ERR1
        Dim i As Integer
        Dim mVehicleRept As Integer

        If mVehicleCode = "" Then CheckDuplicate = True : Exit Function
        With SprdMain
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColVehicleNo
                If UCase(.Text) = UCase(mVehicleCode) Then
                    mVehicleRept = mVehicleRept + 1
                    If mVehicleRept > 1 Then
                        CheckDuplicate = True
                        MsgInformation("Duplicate Vehicle No")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColVehicleNo)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function CheckTPDuplicate(ByRef mTransporterName As String, ByRef mVT As String, ByRef pCol As Integer) As Boolean

        On Error GoTo ERR1
        Dim i As Integer
        Dim mVehicleRept As Integer
        Dim xTransporterName As String
        Dim xVT As String
        If mTransporterName = "" Or mVT = "" Then CheckTPDuplicate = True : Exit Function
        With SprdMain2
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColTransportName
                xTransporterName = Trim(.Text)

                .Col = ColVehicleType
                xVT = Trim(.Text)

                If UCase(xTransporterName & xVT) = UCase(Trim(mTransporterName) & Trim(mVT)) Then
                    mVehicleRept = mVehicleRept + 1
                    If mVehicleRept > 1 Then
                        CheckTPDuplicate = True
                        MsgInformation("Duplicate Transporter Name & Vehicle Type")
                        MainClass.SetFocusToCell(SprdMain2, .ActiveRow, pCol)
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
    Private Sub SprdMain2_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain2.Leave
        With SprdMain2
            SprdMain2_LeaveCell(SprdMain2, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub txtAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBackRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBackRate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBackRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBackRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDefaultRatePerKG_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDefaultRatePerKG.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDefaultRatePerKG_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDefaultRatePerKG.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDefaultPickupRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDefaultPickupRate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDefaultPickupRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDefaultPickupRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCustomerCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomerCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustomerCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomerCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = Asc(UCase(Chr(KeyAscii)))
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCustomerCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomerCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchCustCode_Click(cmdSearchCustCode, New System.EventArgs())
    End Sub

    Private Sub txtCustomerCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomerCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim xMkey As String = ""
        Dim mCustomerName As String

        If Trim(txtCustomerCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtCustomerCode.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Invaild Customer Code")
            Cancel = True
            Exit Sub
        Else
            mCustomerName = MasterNo
        End If

        txtCustomerName.Text = mCustomerName

        If ShowRecord() = False Then Cancel = True
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function ShowRecord() As Boolean

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mWef As String
        Dim xMkey As String = ""

        ShowRecord = True

        If Trim(txtCustomerCode.Text) = "" Then Exit Function
        If Trim(txtBillTo.Text) = "" Then Exit Function

        If Trim(txtWEF.Text) = "" Then
            SqlStr = " SELECT MAX(WEF) AS WEF " & vbCrLf & " FROM FIN_VEHICLE_RATE_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "' AND BILL_TO_LOC_ID='" & Trim(txtBillTo.Text) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTripRateMain, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTripRateMain.EOF = True Then
                Exit Function
            End If
        End If

        If MODIFYMode = True And RsTripRateMain.EOF = False Then xMkey = RsTripRateMain.Fields("mKey").Value
        SqlStr = " SELECT * FROM FIN_VEHICLE_RATE_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'  AND BILL_TO_LOC_ID='" & Trim(txtBillTo.Text) & "'"

        If Trim(txtWEF.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            '    Else	
            '        SqlStr = SqlStr & vbCrLf _	
            ''            & " AND WEF = (" & vbCrLf _	
            ''            & " SELECT MAX(WEF) AS WEF " & vbCrLf _	
            ''            & " FROM FIN_VEHICLE_RATE_HDR " & vbCrLf _	
            ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _	
            ''            & " AND SUPP_CUST_CODE='" & Trim(txtCustomerCode.Text) & "')"	
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTripRateMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTripRateMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Vehicle Rate Not Entered For This Customer. Click Add For New.", MsgBoxStyle.Information)
                ShowRecord = False
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM FIN_VEHICLE_RATE_HDR" & vbCrLf & " WHERE UPPER(LTRIM(RTRIM(MKey)))='" & MainClass.AllowSingleQuote(UCase(xMkey)) & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTripRateMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function



    Private Sub txtCustomerName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomerName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOTRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOTRate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOTRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOTRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPointRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPointRate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPointRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPointRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPremiumRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPremiumRate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPremiumRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPremiumRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTripRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTripRate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTripRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTripRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtWef_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWEF_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtWEF.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchWEF_Click(cmdSearchWEF, New System.EventArgs())
    End Sub

    Private Sub TxtWef_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        If Trim(txtWEF.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtWEF.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(txtCustomerCode.Text) = "" Then GoTo EventExitSub

        If mAmendStatus = True Then
            If CDate(txtWEF.Text) <= CDate(lblWEF.Text) Then
                MsgBox("W.E.F. Date Should be greater than Previous Date")
                Cancel = True
                GoTo EventExitSub
            End If
        End If

        If ShowRecord = False Then Cancel = True


        GoTo EventExitSub
ERR1:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtBillTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillTo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillTo.DoubleClick
        cmdBillToSearch_Click(cmdBillToSearch, New System.EventArgs())
    End Sub
    Private Sub txtBillTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBillTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBillTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdBillToSearch_Click(cmdBillToSearch, New System.EventArgs())
    End Sub
    Private Sub txtBillTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(txtCustomerCode.Text) = "" Then GoTo EventExitSub
        If Trim(txtBillTo.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(txtCustomerCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        Else
            MsgBox("Invalid Customer Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'") = False Then
            MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If ShowRecord() = False Then Cancel = True

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdBillToSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBillToSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim xAcctCode As String = ""

        If Trim(txtCustomerCode.Text) = "" Then
            MsgInformation("Please select the Customer First")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtCustomerCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        End If

        SqlStr = "SELECT LOCATION_ID, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE, GST_RGN_NO " & vbCrLf _
                & " FROM FIN_SUPP_CUST_BUSINESS_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'"

        If MainClass.SearchGridMasterBySQL2((txtBillTo.Text), SqlStr) = True Then
            txtBillTo.Text = AcName
            txtBillTo_Validating(txtBillTo, New System.ComponentModel.CancelEventArgs(False))
            If txtBillTo.Enabled = True Then txtBillTo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
