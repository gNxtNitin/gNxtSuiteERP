Option Strict Off
Option Explicit On
Imports System.ComponentModel
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmProductPackingMaster
    Inherits System.Windows.Forms.Form
    Dim RsPackingMain As ADODB.Recordset
    Dim RsPackingDetail As ADODB.Recordset

    Dim xMyMenu As String

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColCustomerName As Short = 1
    Private Const ColLocation As Short = 2
    Private Const ColInnerBoxStd As Short = 3
    Private Const ColInnerBoxCode As Short = 4
    Private Const ColOuterBoxStd_IB As Short = 5
    Private Const ColOuterBoxStd_Pcs As Short = 6
    Private Const ColOuterBoxCode As Short = 7

    Dim mAmendStatus As Boolean

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""

        SqlStr = " SELECT ITEM_CODE, ITEM_SHORT_DESC, CUSTOMER_PART_NO, PACK_STD, PACK_ITEM_CODE, " & vbCrLf _
            & " OUTER_PACK_STD_PER_INNER, OUTER_PACK_STD_PER_UOM, OUTER_PACK_ITEM_CODE " & vbCrLf _
            & " FROM INV_ITEM_MST" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " ORDER BY ITEM_CODE"

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
        Dim CntRow As Long
        Dim mCustomerName As String
        Dim mLocation As String

        FieldsVarification = True
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a New Account Or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsPackingMain.EOF = True Then Exit Function


        If Trim(txtProductCode.Text) = "" Then
            MsgBox("Product Code Is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtProductCode.Enabled = True Then txtProductCode.Focus()
            Exit Function
        End If

        With SprdMain
            If .MaxRows <= 1 Then Exit Function
            For CntRow = 1 To .MaxRows - 1
                .Row = CntRow
                .Col = ColCustomerName
                mCustomerName = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mCustomerName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("Invalid Customer Name.")
                    FieldsVarification = False
                    Exit Function
                End If

                .Col = ColLocation
                mLocation = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mLocation, "LOCATION_ID", "SUPP_CUST_CODE", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(mCustomerName) & "'") = False Then
                    MsgInformation("Invalid Location ID.")
                    FieldsVarification = False
                    Exit Function
                End If

            Next
        End With

        '    If MainClass.ValidDataInGrid(SprdMain, ColVehicleNo, "S", "Vehicle No Is Blank") = False Then FieldsVarification = False: Exit Function	
        '    If MainClass.ValidDataInGrid(SprdMain, ColTripRate, "N", "Trip Rate Is Blank") = False Then FieldsVarification = False: Exit Function	


        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtProductCode.Enabled = True
            cmdSearchProductCode.Enabled = True
            SprdMain.Enabled = True
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
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click

        On Error GoTo DelErrPart
        Exit Sub

        If Trim(txtProductCode.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        PubDBCn.Cancel()
        PubDBCn.BeginTrans()

        If Not RsPackingMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then

                'PubDBCn.Execute("DELETE FROM FIN_VEHICLE_RATE_DET WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                'PubDBCn.Execute("DELETE FROM FIN_VEHICLE_TP_RATE_DET WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                'PubDBCn.Execute("DELETE FROM FIN_VEHICLE_RATE_HDR  WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

                PubDBCn.CommitTrans()
                RsPackingMain.Requery()
                RsPackingDetail.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsPackingMain.Requery()
        RsPackingDetail.Requery()
        PubDBCn.Cancel()
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPackingMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtProductCode.Enabled = False
            cmdSearchProductCode.Enabled = False
            SprdMain.Enabled = True
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
        Call PrintStandard(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintStandard(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub PrintStandard(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim mRsTemp As ADODB.Recordset = Nothing

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "Customer Wise Packing Standard Master"

        'SqlStr = " SELECT FIN_VEHICLE_RATE_HDR.*,FIN_VEHICLE_RATE_DET.*,FIN_SUPP_CUST_MST.*,PRD_OPR_MST.* " & vbCrLf & " FROM FIN_VEHICLE_RATE_HDR, FIN_VEHICLE_RATE_DET, FIN_SUPP_CUST_MST, PRD_OPR_MST " & vbCrLf & " WHERE FIN_VEHICLE_RATE_HDR.MKEY=FIN_VEHICLE_RATE_DET.MKEY " & vbCrLf & " AND FIN_VEHICLE_RATE_HDR.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE(+) " & vbCrLf & " AND FIN_VEHICLE_RATE_HDR.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE(+) " & vbCrLf & " AND FIN_VEHICLE_RATE_DET.COMPANY_CODE=PRD_OPR_MST.COMPANY_CODE(+) " & vbCrLf & " AND FIN_VEHICLE_RATE_DET.OPR_CODE=PRD_OPR_MST.OPR_CODE(+) " & vbCrLf & " AND FIN_VEHICLE_RATE_HDR.MKEY='" & MainClass.AllowSingleQuote(lblMKey.Text) & "' ORDER BY SERIAL_NO"

        'Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\VehicleRate.rpt"

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
            txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(False))
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

    Private Sub cmdSearchProductCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProductCode.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtProductCode.Text, "INV_ITEM_MST", "ITEM_CODE", "CUSTOMER_PART_NO", "ITEM_SHORT_DESC", , SqlStr) = True Then
            txtProductName.Text = AcName2
            txtProductCode.Text = AcName
            txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(False))
            If txtProductCode.Enabled = True Then txtProductCode.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub frmProductPackingMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub

        SqlStr = "Select * from INV_ITEM_MST Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPackingMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from INV_CUSTOMER_PACKING_MST Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPackingDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

    Private Sub frmProductPackingMaster_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmProductPackingMaster_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmProductPackingMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        'Me.Height = VB6.TwipsToPixelsY(7590)
        'Me.Width = VB6.TwipsToPixelsX(11385)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsPackingMain
            txtProductCode.MaxLength = MainClass.SetMaxLength("ITEM_CODE", "INV_ITEM_MST", PubDBCn)
            txtProductName.MaxLength = MainClass.SetMaxLength("ITEM_CODE", "INV_ITEM_MST", PubDBCn)
            txtUOM.MaxLength = MainClass.SetMaxLength("ISSUE_UOM", "INV_ITEM_MST", PubDBCn)

            txtInnerBoxCode.MaxLength = MainClass.SetMaxLength("ITEM_CODE", "INV_ITEM_MST", PubDBCn)
            txtInnerName.MaxLength = MainClass.SetMaxLength("ITEM_CODE", "INV_ITEM_MST", PubDBCn)
            txtInnerStdQty.MaxLength = .Fields("PACK_STD").Precision
            txtInnerUOM.MaxLength = MainClass.SetMaxLength("ISSUE_UOM", "INV_ITEM_MST", PubDBCn)

            txtOuterBoxCode.MaxLength = MainClass.SetMaxLength("ITEM_CODE", "INV_ITEM_MST", PubDBCn)
            txtOuterName.MaxLength = MainClass.SetMaxLength("ITEM_CODE", "INV_ITEM_MST", PubDBCn)
            txtOuterUOM.MaxLength = MainClass.SetMaxLength("ISSUE_UOM", "INV_ITEM_MST", PubDBCn)
            txtOuter_IB_StdQty.MaxLength = .Fields("OUTER_PACK_STD_PER_INNER").Precision
            txtOuter_UOM_StdQty.MaxLength = .Fields("OUTER_PACK_STD_PER_UOM").Precision

        End With
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume	
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtProductCode.Enabled = mMode
        cmdSearchProductCode.Enabled = mMode
        txtProductName.Enabled = False
        txtUOM.Enabled = False
        txtOuter_UOM_StdQty.Enabled = False
    End Sub

    Private Sub frmProductPackingMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsPackingMain.Close()
        RsPackingDetail.Close()

        RsPackingMain = Nothing
        RsPackingDetail = Nothing
        Me.Hide()


    End Sub

    Private Sub Clear1()


        txtProductCode.Text = ""
        txtProductName.Text = ""
        txtUOM.Text = ""

        txtInnerBoxCode.Text = ""
        txtInnerName.Text = ""
        txtInnerStdQty.Text = ""
        txtInnerUOM.Text = ""

        txtOuterBoxCode.Text = ""
        txtOuterName.Text = ""
        txtOuterUOM.Text = ""
        txtOuter_IB_StdQty.Text = ""
        txtOuter_UOM_StdQty.Text = ""

        txtProductName.Enabled = False
        txtInnerName.Enabled = False
        txtOuterName.Enabled = False
        txtInnerUOM.Enabled = False
        txtOuterUOM.Enabled = False

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)

        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsPackingMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FormatSprdMain(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2)
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColCustomerName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            .set_ColWidth(.Col, 25)

            .Col = ColLocation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn)
            .set_ColWidth(.Col, 25)

            .Col = ColInnerBoxStd
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("99999.99")
            .TypeFloatMin = CDbl("-99999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColInnerBoxStd, 8)

            .Col = ColInnerBoxCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 10)


            .Col = ColOuterBoxStd_IB
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("99999.99")
            .TypeFloatMin = CDbl("-99999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            .Col = ColOuterBoxStd_Pcs
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("99999.99")
            .TypeFloatMin = CDbl("-99999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            .Col = ColOuterBoxCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 10)


        End With
        'MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColOuterBoxStd_Pcs, ColOuterBoxStd_Pcs)

        MainClass.SetSpreadColor(SprdMain, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsPackingDetail.Requery()
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1

        With RsPackingMain
            If Not .EOF Then
                ADDMode = False
                MODIFYMode = False

                txtProductCode.Text = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                txtProductName.Text = Trim(IIf(IsDBNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value))
                txtUOM.Text = Trim(IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value))

                txtInnerBoxCode.Text = Trim(IIf(IsDBNull(.Fields("PACK_ITEM_CODE").Value), "", .Fields("PACK_ITEM_CODE").Value))
                If MainClass.ValidateWithMasterTable(txtInnerBoxCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtInnerName.Text = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(txtInnerBoxCode.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtInnerUOM.Text = MasterNo
                End If

                txtInnerStdQty.Text = VB6.Format(IIf(IsDBNull(.Fields("PACK_STD").Value), 0, .Fields("PACK_STD").Value), "0.00")

                txtOuterBoxCode.Text = Trim(IIf(IsDBNull(.Fields("OUTER_PACK_ITEM_CODE").Value), "", .Fields("OUTER_PACK_ITEM_CODE").Value))
                If MainClass.ValidateWithMasterTable(txtOuterBoxCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtOuterName.Text = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(txtOuterBoxCode.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtOuterUOM.Text = MasterNo
                End If

                txtOuter_IB_StdQty.Text = VB6.Format(IIf(IsDBNull(.Fields("OUTER_PACK_STD_PER_INNER").Value), 0, .Fields("OUTER_PACK_STD_PER_INNER").Value), "0.00")
                txtOuter_UOM_StdQty.Text = VB6.Format(IIf(IsDBNull(.Fields("OUTER_PACK_STD_PER_UOM").Value), 0, .Fields("OUTER_PACK_STD_PER_UOM").Value), "0.00")

                Call ShowDetail()
                Call MakeEnableDesableField(False)
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsPackingMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
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
        Dim mCustomerCode As String
        Dim mCustomerName As String
        Dim mInnerItemCode As String
        Dim mOuterItemCode As String

        SqlStr = ""

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM INV_CUSTOMER_PACKING_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPackingDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsPackingDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1	
            i = 1
            '        .MoveFirst	

            Do While Not .EOF

                SprdMain.Row = i

                mCustomerCode = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)

                If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCustomerName = Trim(MasterNo)
                Else
                    mCustomerName = ""
                End If

                SprdMain.Col = ColCustomerName
                SprdMain.Text = mCustomerName

                SprdMain.Col = ColLocation
                SprdMain.Text = IIf(IsDBNull(.Fields("LOC_ID").Value), "", .Fields("LOC_ID").Value)

                SprdMain.Col = ColInnerBoxStd
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("INNER_PACK_STD").Value), 0, .Fields("INNER_PACK_STD").Value), "0.00")

                SprdMain.Col = ColOuterBoxStd_IB
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("OUTER_PACK_STD_PER_INNER").Value), 0, .Fields("OUTER_PACK_STD_PER_INNER").Value), "0.00")

                SprdMain.Col = ColOuterBoxStd_Pcs
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("OUTER_PACK_STD_PER_UOM").Value), 0, .Fields("OUTER_PACK_STD_PER_UOM").Value), "0.00")

                SprdMain.Col = ColInnerBoxCode
                SprdMain.Text = IIf(IsDBNull(.Fields("INNER_PACK_ITEM_CODE").Value), "", .Fields("INNER_PACK_ITEM_CODE").Value)

                SprdMain.Col = ColOuterBoxCode
                SprdMain.Text = IIf(IsDBNull(.Fields("OUTER_PACK_ITEM_CODE").Value), "", .Fields("OUTER_PACK_ITEM_CODE").Value)

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
    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mMKey As String
        Dim mStatus As String


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""
        SqlStr = " UPDATE INV_ITEM_MST SET " & vbCrLf _
                & " PACK_STD=" & VB6.Format(txtInnerStdQty.Text) & "," & vbCrLf _
                & " PACK_ITEM_CODE= '" & MainClass.AllowSingleQuote(txtInnerBoxCode.Text) & "'," & vbCrLf _
                & " OUTER_PACK_STD_PER_INNER=" & VB6.Format(txtOuter_IB_StdQty.Text) & ", " & vbCrLf _
                & " OUTER_PACK_STD_PER_UOM=" & VB6.Format(txtOuter_UOM_StdQty.Text) & ", " & vbCrLf _
                & " OUTER_PACK_ITEM_CODE='" & MainClass.AllowSingleQuote(txtOuterBoxCode.Text) & "', " & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'"


        PubDBCn.Execute(SqlStr)

        If UpdateDetail1() = False Then GoTo ErrPart

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsPackingMain.Requery()
        RsPackingDetail.Requery()
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
    End Function

    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mCustomerCode As String
        Dim mCustomerName As String
        Dim mInnerBoxStd As Double
        Dim mOuterBoxStd_IB As Double
        Dim mOuterBoxStd_Pcs As Double
        Dim mOuterBoxCode As String
        Dim mInnerBoxCode As String
        Dim mLocationID As String

        PubDBCn.Execute("DELETE FROM INV_CUSTOMER_PACKING_MST  " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(txtProductCode.Text)) & "'")

        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColCustomerName
                mCustomerName = MainClass.AllowSingleQuote(.Text)

                If MainClass.ValidateWithMasterTable(mCustomerName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCustomerCode = MainClass.AllowSingleQuote(MasterNo)
                End If

                .Col = ColLocation
                mLocationID = Trim(.Text)

                .Col = ColInnerBoxStd
                mInnerBoxStd = Val(.Text)

                .Col = ColOuterBoxStd_IB
                mOuterBoxStd_IB = Val(.Text)

                .Col = ColOuterBoxStd_Pcs
                mOuterBoxStd_Pcs = Val(.Text)

                .Col = ColInnerBoxCode
                mInnerBoxCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColOuterBoxCode
                mOuterBoxCode = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If Trim(mCustomerName) <> "" Then
                    SqlStr = " INSERT INTO  INV_CUSTOMER_PACKING_MST ( " & vbCrLf _
                        & " COMPANY_CODE, ITEM_CODE, SERIAL_NO, " & vbCrLf _
                        & " SUPP_CUST_CODE, LOC_ID, INNER_PACK_STD, INNER_PACK_ITEM_CODE, " & vbCrLf _
                        & " OUTER_PACK_STD_PER_INNER, OUTER_PACK_STD_PER_UOM, OUTER_PACK_ITEM_CODE " & vbCrLf _
                        & " ) VALUES ( " & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & i & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mCustomerCode) & "', '" & MainClass.AllowSingleQuote(mLocationID) & "', " & Val(mInnerBoxStd) & ", '" & MainClass.AllowSingleQuote(mInnerBoxCode) & "'," & vbCrLf _
                        & " '" & mOuterBoxStd_IB & "', '" & mOuterBoxStd_Pcs & "', '" & MainClass.AllowSingleQuote(mOuterBoxCode) & "')"

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
        MainClass.ButtonStatus(Me, XRIGHT, RsPackingMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        txtProductCode.Text = Trim(SprdView.Text)

        txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(False))
        ViewGrid()
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""
        Dim mDeleted As Boolean
        Dim mSuppCustCode As String = ""

        If eventArgs.row = 0 And eventArgs.col = ColCustomerName Then
            With SprdMain

                SqlStr = " SELECT SUPP_CUST_CODE, SUPP_CUST_NAME, LOCATION_ID, SUPP_CUST_ADDR,SUPP_CUST_CITY,SUPP_CUST_STATE" & vbCrLf _
                        & " FROM FIN_SUPP_CUST_BUSINESS_MST " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


                If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColCustomerName
                    .Text = AcName1

                    .Col = ColLocation
                    .Text = AcName2

                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColLocation Then
            With SprdMain

                .Col = ColCustomerName
                mSuppCustCode = Trim(.Text)

                SqlStr = " SELECT SUPP_CUST_CODE, SUPP_CUST_NAME, LOCATION_ID, SUPP_CUST_ADDR,SUPP_CUST_CITY,SUPP_CUST_STATE" & vbCrLf _
                        & " FROM FIN_SUPP_CUST_BUSINESS_MST " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(mSuppCustCode) & "'"

                If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColLocation
                    .Text = AcName2

                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColInnerBoxCode Then
            With SprdMain
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "", , SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColInnerBoxCode
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColOuterBoxCode Then
            With SprdMain
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "", , SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColOuterBoxCode
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColCustomerName)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColCustomerName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColCustomerName, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColInnerBoxCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColInnerBoxCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColOuterBoxCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColOuterBoxCode, 0))

    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xCustomerName As String
        Dim xInnerCode As String
        Dim xOuterCode As String

        If eventArgs.newRow = -1 Then Exit Sub
        SprdMain.Row = eventArgs.row
        If Trim(SprdMain.Text) = "" Then Exit Sub
        Dim mTotalPcsinOuter As Double

        Select Case eventArgs.col
            Case ColCustomerName
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColCustomerName
                xCustomerName = SprdMain.Text
                If xCustomerName = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(xCustomerName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If CheckDuplicate(xCustomerName) = False Then
                        MainClass.AddBlankSprdRow(SprdMain, ColCustomerName, ConRowHeight)
                        FormatSprdMain(eventArgs.row)
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCustomerName)
                End If
            Case ColInnerBoxCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColInnerBoxCode
                xInnerCode = SprdMain.Text
                If xInnerCode <> "" Then
                    If MainClass.ValidateWithMasterTable(xInnerCode, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColInnerBoxCode)
                    End If
                End If
            Case ColOuterBoxCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColOuterBoxCode
                xOuterCode = SprdMain.Text
                If xOuterCode <> "" Then
                    If MainClass.ValidateWithMasterTable(xOuterCode, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColOuterBoxCode)
                    End If
                End If

        End Select

        Dim mOuterBoxStd_IB As Double = 0
        Dim mInnerStd As Double = 0

        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColInnerBoxStd
        mInnerStd = Val(SprdMain.Text)

        SprdMain.Col = ColOuterBoxStd_IB
        mOuterBoxStd_IB = Val(SprdMain.Text)

        mTotalPcsinOuter = mInnerStd * mOuterBoxStd_IB
        SprdMain.Col = ColOuterBoxStd_Pcs
        SprdMain.Text = Val(mTotalPcsinOuter)

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function CheckDuplicate(ByRef mCustomerName As String) As Boolean

        On Error GoTo ERR1
        Dim i As Integer
        Dim mCustomerNameRept As Integer

        If mCustomerName = "" Then CheckDuplicate = True : Exit Function
        With SprdMain
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColCustomerName
                If UCase(.Text) = UCase(mCustomerName) Then
                    mCustomerNameRept = mCustomerNameRept + 1
                    If mCustomerNameRept > 1 Then
                        CheckDuplicate = True
                        MsgInformation("Duplicate Customer Name")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColCustomerName)
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
    Private Sub txtProductCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProductCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProductCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = Asc(UCase(Chr(KeyAscii)))
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProductCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProductCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchProductCode_Click(cmdSearchProductCode, New System.EventArgs())
    End Sub

    Private Sub txtProductCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProductCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim xProductCode As String = ""

        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsPackingMain.EOF = False Then xProductCode = RsPackingMain.Fields("ITEM_CODE").Value
        SqlStr = " SELECT * FROM INV_ITEM_MST " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ITEM_CODE='" & Trim(txtProductCode.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPackingMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPackingMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Please Enter Valid Product code. Click Add For New.", MsgBoxStyle.Information)
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM INV_ITEM_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & Trim(xProductCode) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPackingMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtProductName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtInnerBoxCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInnerBoxCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInnerBoxCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInnerBoxCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = Asc(UCase(Chr(KeyAscii)))
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtInnerBoxCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtInnerBoxCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchInnerCode_Click(cmdSearchInnerCode, New System.EventArgs())
    End Sub

    Private Sub txtInnerBoxCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInnerBoxCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtInnerBoxCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtInnerBoxCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Invaild Inner Code")
            Cancel = True
            Exit Sub
        Else
            txtInnerName.Text = MasterNo
            If MainClass.ValidateWithMasterTable(txtInnerBoxCode.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtInnerUOM.Text = MasterNo
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtInnerName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInnerName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdSearchInnerCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchInnerCode.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtInnerBoxCode.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "ISSUE_UOM", , SqlStr) = True Then
            txtInnerName.Text = AcName
            txtInnerBoxCode.Text = AcName1
            txtInnerBoxCode_Validating(txtInnerBoxCode, New System.ComponentModel.CancelEventArgs(False))
            If txtInnerBoxCode.Enabled = True Then txtInnerBoxCode.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtOuterBoxCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOuterBoxCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOuterBoxCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOuterBoxCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = Asc(UCase(Chr(KeyAscii)))
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOuterBoxCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtOuterBoxCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchOuterCode_Click(cmdSearchOuterCode, New System.EventArgs())
    End Sub

    Private Sub txtOuterBoxCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOuterBoxCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtOuterBoxCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtOuterBoxCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Invaild Outer Code")
            Cancel = True
            Exit Sub
        Else
            txtOuterName.Text = MasterNo
            If MainClass.ValidateWithMasterTable(txtOuterBoxCode.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtOuterUOM.Text = MasterNo
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
        '    Resume	
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtOuterName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOuterName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdSearchOuterCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchOuterCode.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtOuterBoxCode.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtOuterName.Text = AcName
            txtOuterBoxCode.Text = AcName1
            txtOuterBoxCode_Validating(txtOuterBoxCode, New System.ComponentModel.CancelEventArgs(False))
            If txtOuterBoxCode.Enabled = True Then txtOuterBoxCode.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtOuter_IB_StdQty_Validating(sender As Object, e As CancelEventArgs) Handles txtOuter_IB_StdQty.Validating
        Try
            txtOuter_UOM_StdQty.Text = Val(txtInnerStdQty.Text) * Val(txtOuter_IB_StdQty.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtInnerStdQty_Validating(sender As Object, e As CancelEventArgs) Handles txtInnerStdQty.Validating
        Try
            txtOuter_UOM_StdQty.Text = Val(txtInnerStdQty.Text) * Val(txtOuter_IB_StdQty.Text)
        Catch ex As Exception

        End Try
    End Sub
End Class
