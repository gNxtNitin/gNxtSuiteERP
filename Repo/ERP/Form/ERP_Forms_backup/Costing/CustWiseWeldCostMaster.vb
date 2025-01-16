Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCustWiseWeldCostMaster
    Inherits System.Windows.Forms.Form
    Dim RsCustWeldCost As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mIsShowing As Boolean

    Dim mcntRow As Integer


    Dim mAmendStatus As Boolean

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String
        SqlStr = ""

        SqlStr = "SELECT IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME , TO_CHAR(WEF,'DD/MM/YYYY') AS WEF " & vbCrLf & " FROM PRD_CUST_WELD_COST_MST IH, FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " ORDER BY CMST.SUPP_CUST_NAME "

        MainClass.AssignDataInSprd(SqlStr, ADataGrid, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 24)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 10)
            .set_ColWidth(2, 25)
            .set_ColWidth(3, 8)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle			
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsCustWeldCost.EOF = True Then Exit Function

        If MODIFYMode = True And chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed Costing Cann't be Modified")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtWEF.Text) = "" Then
            MsgBox("W.E.F is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtWEF.Enabled = True Then txtWEF.Focus()
            Exit Function
        End If

        If Trim(txtSuppCustCode.Text) = "" Then
            MsgBox("Customer is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtSuppCustCode.Enabled = True Then txtSuppCustCode.Focus()
            Exit Function
        End If

        If Trim(txtPrepBy.Text) = "" Then
            MsgBox("Prepared By is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPrepBy.Focus()
            Exit Function
        End If

        If ADDMode = True And Val(txtAmendNo.Text) > 0 Then
            If CheckWEFDate(Trim(txtWEF.Text)) = False Then
                MsgBox("WEF. Date Cann't be Less or Equal Than Current WEF Date.", MsgBoxStyle.Information)
                FieldsVarification = False
                txtWEF.Focus()
                Exit Function
            End If
        End If
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Function CheckWEFDate(ByRef pWEFDate As String) As Boolean

        On Error GoTo ErrorPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mCheckWEFDate As String

        CheckWEFDate = True

        SqlStr = " SELECT MAX(WEF) AS WEF " & vbCrLf _
        & " FROM PRD_CUST_WELD_COST_MST " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "' " & vbCrLf _
        & " AND AMEND_NO < " & Val(txtAmendNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mCheckWEFDate = IIf(IsDBNull(RsTemp.Fields("WEF").Value), "", RsTemp.Fields("WEF").Value)
            If mCheckWEFDate <> "" Then
                If CDate(mCheckWEFDate) >= CDate(pWEFDate) Then
                    CheckWEFDate = False
                End If
            End If
        End If

        Exit Function
ErrorPart:
        CheckWEFDate = False
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
            cmdSearchWEF.Enabled = True
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

        Dim mItemCode As String
        Dim I As Integer

        txtAmendNo.Text = CStr(GetMaxAmendNo())
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        mAmendStatus = True
        cmdAmend.Enabled = False
        txtWEF.Enabled = True


        txtPrepBy.Text = ""
        lblPrepBy.Text = ""
        txtAppBy.Text = ""
        lblAppBy.Text = ""

        txtPrepBy.Enabled = True
        cmdSearchPrepBy.Enabled = True

        ADDMode = True
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsCustWeldCost, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Function GetMaxAmendNo() As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = " SELECT MAX(AMEND_NO) AS AMEND_NO" & vbCrLf _
        & " FROM PRD_CUST_WELD_COST_MST" & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "'"

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

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click

        On Error GoTo DelErrPart

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed Costing Cann't be Deleted")
            Exit Sub
        End If

        If Not RsCustWeldCost.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PRD_CUST_WELD_COST_MST", (txtSuppCustCode.Text), RsCustWeldCost) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_CUST_WELD_COST_MST", "MKEY", (lblMKey.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM PRD_CUST_WELD_COST_MST WHERE UPPER(LTRIM(RTRIM(Mkey)))='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")



                If Val(txtAmendNo.Text) > 0 Then
                    If UpdatePreviousCost(Val(txtAmendNo.Text), "O") = False Then GoTo DelErrPart
                End If

                PubDBCn.CommitTrans()
                RsCustWeldCost.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsCustWeldCost.Requery()

        PubDBCn.Cancel()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsCustWeldCost, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
        '    Resume			
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnCosting((lblMKey.Text), Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnCosting((lblMKey.Text), Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnCosting(ByRef nMkey As String, ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mSuppCustCode As String


        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "Customer Wise Welding Cost Master"

        SqlStr = " SELECT IH.*, CMST.SUPP_CUST_NAME " & vbCrLf & " FROM PRD_CUST_WELD_COST_MST IH, FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.MKEY='" & MainClass.AllowSingleQuote(nMkey) & "' "

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\CustWeldCost.rpt"


        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)



        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume			
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Dim ii As Integer

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1() = True Then
            mIsShowing = False
            ADDMode = False
            MODIFYMode = False

            txtSuppCustCode_Validating(txtSuppCustCode, New System.ComponentModel.CancelEventArgs(False))
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
        '    Resume			
    End Sub

    Private Sub cmdSearchAppBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAppBy.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtAppBy.Text = AcName1
            lblAppBy.Text = AcName
        End If
    End Sub
    Private Sub cmdSearchCust_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCust.Click
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='C' "
        If MainClass.SearchGridMaster("", "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtSuppCustCode.Text = AcName1
            txtSuppCustName.Text = AcName
            If txtSuppCustCode.Enabled = True Then txtSuppCustCode.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdSearchPrepBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPrepBy.Click
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtPrepBy.Text = AcName1
            lblPrepBy.Text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchWEF_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchWEF.Click
        Dim mSqlStr As String

        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtSuppCustCode.Text) <> "" Then
            mSqlStr = mSqlStr & " AND SUPP_CUST_CODE='" & Trim(txtSuppCustCode.Text) & "'"
        End If

        If MainClass.SearchGridMaster("", "PRD_CUST_WELD_COST_MST", "WEF", "SUPP_CUST_CODE", "", "", mSqlStr) = True Then
            txtWEF.Text = Format(AcName, "DD/MM/YYYY")
            txtSuppCustCode.Text = AcName1
            If txtWEF.Enabled = True Then txtWEF.Focus()
        End If
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub frmCustWiseWeldCostMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String
        If FormActive = True Then Exit Sub
        Me.Text = "Customer Wise Welding Cost Master"

        SqlStr = ""
        SqlStr = "Select * from PRD_CUST_WELD_COST_MST Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCustWeldCost, ADODB.LockTypeEnum.adLockReadOnly)

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

    Private Sub frmCustWiseWeldCostMaster_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmCustWiseWeldCostMaster_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmCustWiseWeldCostMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim mDocNo As String
        Dim mDateOrg As String
        Dim mRevNo As String
        Dim mDateRev As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
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
        With RsCustWeldCost
            txtSuppCustCode.MaxLength = .Fields("SUPP_CUST_CODE").DefinedSize
            txtWEF.MaxLength = .Fields("WEF").DefinedSize - 6

        End With
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume			
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtWEF.Enabled = mMode
        txtSuppCustCode.Enabled = mMode
        cmdSearchCust.Enabled = mMode
        txtPrepBy.Enabled = mMode
        cmdSearchPrepBy.Enabled = mMode
    End Sub

    Private Sub frmCustWiseWeldCostMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False

        RsCustWeldCost.Close()

        RsCustWeldCost = Nothing

    End Sub

    Private Sub Clear1()

        Dim I As Integer

        lblMKey.Text = ""
        txtWEF.Text = ""
        txtSuppCustCode.Text = ""
        txtSuppCustName.Text = ""
        txtAmendNo.Text = "0"


        txtRemarks.Text = ""
        txtPrepBy.Text = ""
        lblPrepBy.Text = ""
        txtAppBy.Text = ""
        lblAppBy.Text = ""



        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        mIsShowing = False

        mAmendStatus = False
        cmdAmend.Enabled = True

        SSTab1.SelectedIndex = 0

        FormatSprd(-1)

        Call MakeEnableDesableField(True)

        MainClass.ButtonStatus(Me, XRIGHT, RsCustWeldCost, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub FormatSprd(ByRef mRow As Integer)
        On Error GoTo ERR1

        Call FormatSprdCell(SprdWeld, mRow, "1.,2.")
        Call FormatSprdCell(SprdCO2, mRow, "1.,2.")
        Call FormatSprdCell(SprdMC, mRow, "1.,4.,5.,7.,9.")
        Call FormatSprdCell(SprdPower, mRow, "1.,2.,3.,5.,6.,11.")
        Call FormatSprdCell(SprdLabour, mRow, "1.,2.")
        Call FormatSprdCell(SprdCons, mRow, "1.")
        Exit Sub
ERR1:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub FormatSprdCell(ByRef pSprd As AxFPSpreadADO.AxfpSpread, ByRef mRow As Integer, ByRef mUnProtectRow As String)

        On Error GoTo ERR1
        Dim cntRow As Integer
        Dim mCheckRow As String

        With pSprd
            .Row = mRow
            .set_RowHeight(mRow, 16)
            .set_RowHeight(0, 12)

            .Col = 1
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER

            .Col = 2
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            .Col = 3
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("9999999.999")
            .TypeFloatMin = CDbl("-9999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            MainClass.SetSpreadColor(pSprd, mRow)
            MainClass.ProtectCell(pSprd, 1, .MaxRows, 1, 3)

            .Row = 1
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Lock = True
            .BlockMode = False

            For cntRow = 1 To .MaxRows
                mCheckRow = cntRow & "."
                If InStr(1, mUnProtectRow, mCheckRow) > 0 Then
                    .Row = cntRow
                    .Row2 = cntRow
                    .Col = 4
                    .Col2 = 4
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                    '                .Protect = False			
                    .Lock = False
                    .BlockMode = False
                End If
            Next

        End With

        Exit Sub
ERR1:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1

        With RsCustWeldCost
            If Not .EOF Then
                ADDMode = False
                MODIFYMode = False
                mIsShowing = True

                lblMKey.Text = .Fields("MKey").Value

                txtWEF.Text = IIf(IsDBNull(.Fields("WEF").Value), "", .Fields("WEF").Value)
                txtSuppCustCode.Text = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)
                If MainClass.ValidateWithMasterTable(txtSuppCustCode.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSuppCustName.Text = MasterNo
                End If
                txtAmendNo.Text = IIf(IsDBNull(.Fields("AMEND_NO").Value), "", .Fields("AMEND_NO").Value)



                txtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtPrepBy.Text = IIf(IsDBNull(.Fields("PREP_BY").Value), "", .Fields("PREP_BY").Value)
                txtPrepBy_Validating(txtPrepBy, New System.ComponentModel.CancelEventArgs(False))
                txtAppBy.Text = IIf(IsDBNull(.Fields("APP_BY").Value), "", .Fields("APP_BY").Value)
                txtAppBy_Validating(txtAppBy, New System.ComponentModel.CancelEventArgs(False))

                chkStatus.CheckState = IIf(.Fields("Status").Value = "O", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                With SprdWeld
                    .Col = 4
                    .Row = 1
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("MIG_INCH_PER_KG").Value), 0, RsCustWeldCost.Fields("MIG_INCH_PER_KG").Value), "0.00")

                    .Row = 2
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("MIG_COST_PER_KG").Value), 0, RsCustWeldCost.Fields("MIG_COST_PER_KG").Value), "0.00")
                End With

                With SprdCO2
                    .Col = 4
                    .Row = 1
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("CO2_INCH_PER_KG").Value), 0, RsCustWeldCost.Fields("CO2_INCH_PER_KG").Value), "0.00")

                    .Row = 2
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("CO2_COST_PER_KG").Value), 0, RsCustWeldCost.Fields("CO2_COST_PER_KG").Value), "0.00")
                End With

                With SprdMC
                    .Col = 4
                    .Row = 1
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("PROD_CAP_INCH_PER_SHIFT").Value), 0, RsCustWeldCost.Fields("PROD_CAP_INCH_PER_SHIFT").Value), "0.00")

                    .Row = 4
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("COST_OF_MC").Value), 0, RsCustWeldCost.Fields("COST_OF_MC").Value), "0.00")

                    .Row = 5
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("INTEREST_PER").Value), 0, RsCustWeldCost.Fields("INTEREST_PER").Value), "0.00")

                    .Row = 7
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("DEP_PER").Value), 0, RsCustWeldCost.Fields("DEP_PER").Value), "0.00")

                    .Row = 9
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("MAINT_PER").Value), 0, RsCustWeldCost.Fields("MAINT_PER").Value), "0.00")

                End With

                With SprdPower
                    .Col = 4
                    .Row = 1
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("POWER_CONS_KW").Value), 0, RsCustWeldCost.Fields("POWER_CONS_KW").Value), "0.00")

                    .Row = 2
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("POWER_EFFICIENCY").Value), 0, RsCustWeldCost.Fields("POWER_EFFICIENCY").Value), "0.00")

                    .Row = 3
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("HSEB_PER").Value), 0, RsCustWeldCost.Fields("HSEB_PER").Value), "0.00")

                    .Row = 5
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("HSBC_RATE").Value), 0, RsCustWeldCost.Fields("HSBC_RATE").Value), "0.00")

                    .Row = 6
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("DG_RATE").Value), 0, RsCustWeldCost.Fields("DG_RATE").Value), "0.00")

                    .Row = 11
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("NO_HOUR_PER_DAY").Value), 0, RsCustWeldCost.Fields("NO_HOUR_PER_DAY").Value), "0.00")
                End With
                With SprdLabour
                    .Col = 4
                    .Row = 1
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("WELDER_RATE").Value), 0, RsCustWeldCost.Fields("WELDER_RATE").Value), "0.00")

                    .Row = 2
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("HELPER_RATE").Value), 0, RsCustWeldCost.Fields("HELPER_RATE").Value), "0.00")
                End With

                With SprdCons
                    .Col = 4
                    .Row = 1
                    .Text = VB6.Format(IIf(IsDBNull(RsCustWeldCost.Fields("CONS_COST_PER_MONTH").Value), 0, RsCustWeldCost.Fields("CONS_COST_PER_MONTH").Value), "0.00")
                End With

                txtSmokeCost.Text = IIf(IsDBNull(.Fields("SMOKE_COST_PER_INCH").Value), 0, .Fields("SMOKE_COST_PER_INCH").Value)
                txtNetWeldCost.Text = IIf(IsDBNull(.Fields("TOT_WELD_COST_PER_INCH").Value), 0, .Fields("TOT_WELD_COST_PER_INCH").Value)

                Call AutoCalc()

                Call MakeEnableDesableField(False)
                mIsShowing = False

            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsCustWeldCost, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        cmdSearchWEF.Enabled = False
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
        '    Resume			
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim nMkey As String
        Dim mRowNo As Integer
        Dim mStatus As String

        Dim mTOT_WELD_COST_PER_INCH As Double
        Dim mMIG_COST_PER_INCH As Double
        Dim mCO2_COST_PER_INCH As Double
        Dim mMC_COST_PER_INCH As Double
        Dim mPOWER_COST_PER_INCH As Double
        Dim mLABOUR_COST_PER_INCH As Double
        Dim mCONS_COST_PER_INCH As Double
        Dim mSMOKE_COST_PER_INCH As Double
        Dim mPROD_CAP_INCH_PER_SHIFT As Double
        Dim mPROD_CAP_INCH_PER_DAY As Double
        Dim mPROD_CAP_INCH_PER_MONTH As Double
        Dim mMIG_COST_PER_KG As Double
        Dim mMIG_INCH_PER_KG As Double
        Dim mCO2_COST_PER_KG As Double
        Dim mCO2_INCH_PER_KG As Double
        Dim mCOST_OF_MC As Double
        Dim mINTEREST_PER As Double
        Dim mINTEREST_AMOUNT As Double
        Dim mDEP_PER As Double
        Dim mDEP_AMOUNT As Double
        Dim mMAINT_PER As Double
        Dim mMAINT_AMOUNT As Double
        Dim mMC_COST_PER_MONTH As Double
        Dim mPOWER_CONS_KW As Double
        Dim mPOWER_EFFICIENCY As Double
        Dim mHSEB_PER As Double
        Dim mHSBC_RATE As Double
        Dim mNET_HSBC_RATE As Double
        Dim mDG_PER As Double
        Dim mDG_RATE As Double
        Dim mNET_DG_RATE As Double
        Dim mNET_POWER_RATE As Double
        Dim mELEC_LOAD_HOUR As Double
        Dim mNO_HOUR_PER_DAY As Double
        Dim mPOWER_COST_PER_MONTH As Double
        Dim mWELDER_RATE As Double
        Dim mHELPER_RATE As Double
        Dim mNET_LABOUR_COST As Double
        Dim mCONS_COST_PER_MONTH As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()



        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked, "O", "C")

        With SprdWeld
            .Col = 4
            .Row = 1
            mMIG_INCH_PER_KG = Val(.Text)

            .Row = 2
            mMIG_COST_PER_KG = Val(.Text)

            mMIG_COST_PER_INCH = Val(txtTotMIGCost.Text)

        End With

        With SprdCO2
            .Col = 4
            .Row = 1
            mCO2_INCH_PER_KG = Val(.Text)

            .Row = 2
            mCO2_COST_PER_KG = Val(.Text)

            mCO2_COST_PER_INCH = Val(txtTotCO2Cost.Text)

        End With

        With SprdMC
            .Col = 4
            .Row = 1
            mPROD_CAP_INCH_PER_SHIFT = Val(.Text)

            .Row = 2
            mPROD_CAP_INCH_PER_DAY = Val(.Text)

            .Row = 3
            mPROD_CAP_INCH_PER_MONTH = Val(.Text)

            .Row = 4
            mCOST_OF_MC = Val(.Text)

            .Row = 5
            mINTEREST_PER = Val(.Text)

            .Row = 6
            mINTEREST_AMOUNT = Val(.Text)

            .Row = 7
            mDEP_PER = Val(.Text)

            .Row = 8
            mDEP_AMOUNT = Val(.Text)

            .Row = 9
            mMAINT_PER = Val(.Text)

            .Row = 10
            mMAINT_AMOUNT = Val(.Text)

            .Row = 12
            mMC_COST_PER_MONTH = Val(.Text)

            mMC_COST_PER_INCH = Val(txtTotMCCost.Text)

        End With

        With SprdPower
            .Col = 4
            .Row = 1
            mPOWER_CONS_KW = Val(.Text)

            .Row = 2
            mPOWER_EFFICIENCY = Val(.Text)

            .Row = 3
            mHSEB_PER = Val(.Text)

            .Row = 4
            mDG_PER = Val(.Text)

            .Row = 5
            mHSBC_RATE = Val(.Text)

            .Row = 6
            mDG_RATE = Val(.Text)


            .Row = 7
            mNET_HSBC_RATE = Val(.Text)

            .Row = 8
            mNET_DG_RATE = Val(.Text)


            .Row = 9
            mNET_POWER_RATE = Val(.Text)

            .Row = 10
            mELEC_LOAD_HOUR = Val(.Text)

            .Row = 11
            mNO_HOUR_PER_DAY = Val(.Text)

            .Row = 12
            mPOWER_COST_PER_MONTH = Val(.Text)

            mPOWER_COST_PER_INCH = Val(txtTotPowerCost.Text)

        End With

        With SprdLabour
            .Col = 4
            .Row = 1
            mWELDER_RATE = Val(.Text)

            .Row = 2
            mHELPER_RATE = Val(.Text)

            .Row = 3
            mNET_LABOUR_COST = Val(.Text)

            mLABOUR_COST_PER_INCH = Val(txtTotLabourCost.Text)

        End With

        With SprdCons
            .Col = 4
            .Row = 1
            mCONS_COST_PER_MONTH = Val(.Text)

            mCONS_COST_PER_INCH = Val(txtTotLabourCost.Text)
        End With

        mSMOKE_COST_PER_INCH = Val(txtSmokeCost.Text)
        mTOT_WELD_COST_PER_INCH = Val(txtNetWeldCost.Text)

        SqlStr = ""
        If ADDMode = True Then
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & UCase(Trim(txtSuppCustCode.Text)) & VB6.Format(txtWEF.Text, "YYYYMMDD") & VB6.Format(txtAmendNo.Text, "000")

            lblMKey.Text = nMkey
            SqlStr = " INSERT INTO PRD_CUST_WELD_COST_MST ( " & vbCrLf & " MKEY, COMPANY_CODE, SUPP_CUST_CODE,  " & vbCrLf & " WEF, AMEND_NO, TOT_WELD_COST_PER_INCH,  " & vbCrLf & " MIG_COST_PER_INCH, CO2_COST_PER_INCH, MC_COST_PER_INCH,  " & vbCrLf & " POWER_COST_PER_INCH, LABOUR_COST_PER_INCH, CONS_COST_PER_INCH,  " & vbCrLf & " SMOKE_COST_PER_INCH, PROD_CAP_INCH_PER_SHIFT, PROD_CAP_INCH_PER_DAY,  " & vbCrLf & " PROD_CAP_INCH_PER_MONTH, MIG_COST_PER_KG, MIG_INCH_PER_KG,  " & vbCrLf & " CO2_COST_PER_KG, CO2_INCH_PER_KG, COST_OF_MC,  " & vbCrLf & " INTEREST_PER, INTEREST_AMOUNT, DEP_PER,  " & vbCrLf & " DEP_AMOUNT, MAINT_PER, MAINT_AMOUNT,  " & vbCrLf & " MC_COST_PER_MONTH, POWER_CONS_KW, POWER_EFFICIENCY,  " & vbCrLf & " HSEB_PER, HSBC_RATE, NET_HSBC_RATE,  " & vbCrLf & " DG_PER, DG_RATE, NET_DG_RATE,  " & vbCrLf & " NET_POWER_RATE, ELEC_LOAD_HOUR, NO_HOUR_PER_DAY,  " & vbCrLf & " POWER_COST_PER_MONTH, WELDER_RATE, HELPER_RATE,  " & vbCrLf & " NET_LABOUR_COST, CONS_COST_PER_MONTH, STATUS,  " & vbCrLf & " REMARKS, PREP_BY, APP_BY,  " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE " & vbCrLf & " ) VALUES (  "

            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(nMkey) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "', " & vbCrLf & "TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & Val(txtAmendNo.Text) & ", " & vbCrLf & "" & Val(CStr(mTOT_WELD_COST_PER_INCH)) & "," & Val(CStr(mMIG_COST_PER_INCH)) & "," & Val(CStr(mCO2_COST_PER_INCH)) & "," & vbCrLf & "" & Val(CStr(mMC_COST_PER_INCH)) & "," & Val(CStr(mPOWER_COST_PER_INCH)) & "," & Val(CStr(mLABOUR_COST_PER_INCH)) & "," & vbCrLf & "" & Val(CStr(mCONS_COST_PER_INCH)) & "," & Val(CStr(mSMOKE_COST_PER_INCH)) & "," & Val(CStr(mPROD_CAP_INCH_PER_SHIFT)) & "," & vbCrLf & "" & Val(CStr(mPROD_CAP_INCH_PER_DAY)) & "," & Val(CStr(mPROD_CAP_INCH_PER_MONTH)) & "," & Val(CStr(mMIG_COST_PER_KG)) & "," & vbCrLf & "" & Val(CStr(mMIG_INCH_PER_KG)) & "," & Val(CStr(mCO2_COST_PER_KG)) & "," & Val(CStr(mCO2_INCH_PER_KG)) & "," & vbCrLf & "" & Val(CStr(mCOST_OF_MC)) & "," & Val(CStr(mINTEREST_PER)) & "," & Val(CStr(mINTEREST_AMOUNT)) & "," & vbCrLf & "" & Val(CStr(mDEP_PER)) & "," & Val(CStr(mDEP_AMOUNT)) & "," & Val(CStr(mMAINT_PER)) & "," & vbCrLf & "" & Val(CStr(mMAINT_AMOUNT)) & "," & Val(CStr(mMC_COST_PER_MONTH)) & "," & Val(CStr(mPOWER_CONS_KW)) & "," & vbCrLf & "" & Val(CStr(mPOWER_EFFICIENCY)) & "," & Val(CStr(mHSEB_PER)) & "," & Val(CStr(mHSBC_RATE)) & "," & vbCrLf & "" & Val(CStr(mNET_HSBC_RATE)) & "," & Val(CStr(mDG_PER)) & "," & Val(CStr(mDG_RATE)) & "," & vbCrLf & "" & Val(CStr(mNET_DG_RATE)) & "," & Val(CStr(mNET_POWER_RATE)) & "," & Val(CStr(mELEC_LOAD_HOUR)) & "," & vbCrLf & "" & Val(CStr(mNO_HOUR_PER_DAY)) & "," & Val(CStr(mPOWER_COST_PER_MONTH)) & "," & Val(CStr(mWELDER_RATE)) & "," & vbCrLf & "" & Val(CStr(mHELPER_RATE)) & "," & Val(CStr(mNET_LABOUR_COST)) & "," & Val(CStr(mCONS_COST_PER_MONTH)) & ","

            SqlStr = SqlStr & vbCrLf & " '" & mStatus & "', '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtPrepBy.Text) & "', '" & MainClass.AllowSingleQuote(txtAppBy.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = " UPDATE PRD_CUST_WELD_COST_MST  SET " & vbCrLf & " TOT_WELD_COST_PER_INCH = " & Val(CStr(mTOT_WELD_COST_PER_INCH)) & ", " & vbCrLf & " MIG_COST_PER_INCH = " & Val(CStr(mMIG_COST_PER_INCH)) & ", " & vbCrLf & " CO2_COST_PER_INCH = " & Val(CStr(mCO2_COST_PER_INCH)) & ", " & vbCrLf & " MC_COST_PER_INCH = " & Val(CStr(mMC_COST_PER_INCH)) & ", " & vbCrLf & " POWER_COST_PER_INCH = " & Val(CStr(mPOWER_COST_PER_INCH)) & ", " & vbCrLf & " LABOUR_COST_PER_INCH = " & Val(CStr(mLABOUR_COST_PER_INCH)) & ", " & vbCrLf & " CONS_COST_PER_INCH = " & Val(CStr(mCONS_COST_PER_INCH)) & ", " & vbCrLf & " SMOKE_COST_PER_INCH = " & Val(CStr(mSMOKE_COST_PER_INCH)) & ", " & vbCrLf & " PROD_CAP_INCH_PER_SHIFT = " & Val(CStr(mPROD_CAP_INCH_PER_SHIFT)) & ", " & vbCrLf & " PROD_CAP_INCH_PER_DAY = " & Val(CStr(mPROD_CAP_INCH_PER_DAY)) & ", " & vbCrLf & " PROD_CAP_INCH_PER_MONTH = " & Val(CStr(mPROD_CAP_INCH_PER_MONTH)) & ", " & vbCrLf & " MIG_COST_PER_KG = " & Val(CStr(mMIG_COST_PER_KG)) & ", " & vbCrLf & " MIG_INCH_PER_KG = " & Val(CStr(mMIG_INCH_PER_KG)) & ", " & vbCrLf & " CO2_COST_PER_KG = " & Val(CStr(mCO2_COST_PER_KG)) & ", " & vbCrLf & " CO2_INCH_PER_KG = " & Val(CStr(mCO2_INCH_PER_KG)) & ", "


            SqlStr = SqlStr & vbCrLf & " COST_OF_MC = " & Val(CStr(mCOST_OF_MC)) & ", " & vbCrLf & " INTEREST_PER = " & Val(CStr(mINTEREST_PER)) & ", " & vbCrLf & " INTEREST_AMOUNT = " & Val(CStr(mINTEREST_AMOUNT)) & ", " & vbCrLf & " DEP_PER = " & Val(CStr(mDEP_PER)) & ", " & vbCrLf & " DEP_AMOUNT = " & Val(CStr(mDEP_AMOUNT)) & ", " & vbCrLf & " MAINT_PER = " & Val(CStr(mMAINT_PER)) & ", " & vbCrLf & " MAINT_AMOUNT = " & Val(CStr(mMAINT_AMOUNT)) & ", " & vbCrLf & " MC_COST_PER_MONTH = " & Val(CStr(mMC_COST_PER_MONTH)) & ", " & vbCrLf & " POWER_CONS_KW = " & Val(CStr(mPOWER_CONS_KW)) & ", " & vbCrLf & " POWER_EFFICIENCY = " & Val(CStr(mPOWER_EFFICIENCY)) & ", " & vbCrLf & " HSEB_PER = " & Val(CStr(mHSEB_PER)) & ", " & vbCrLf & " HSBC_RATE = " & Val(CStr(mHSBC_RATE)) & ", " & vbCrLf & " NET_HSBC_RATE = " & Val(CStr(mNET_HSBC_RATE)) & ", " & vbCrLf & " DG_PER = " & Val(CStr(mDG_PER)) & ", " & vbCrLf & " DG_RATE = " & Val(CStr(mDG_RATE)) & ", " & vbCrLf & " NET_DG_RATE = " & Val(CStr(mNET_DG_RATE)) & ", " & vbCrLf & " NET_POWER_RATE = " & Val(CStr(mNET_POWER_RATE)) & ", " & vbCrLf & " ELEC_LOAD_HOUR = " & Val(CStr(mELEC_LOAD_HOUR)) & ", " & vbCrLf & " NO_HOUR_PER_DAY = " & Val(CStr(mNO_HOUR_PER_DAY)) & ", " & vbCrLf & " POWER_COST_PER_MONTH = " & Val(CStr(mPOWER_COST_PER_MONTH)) & ", " & vbCrLf & " WELDER_RATE = " & Val(CStr(mWELDER_RATE)) & ", " & vbCrLf & " HELPER_RATE = " & Val(CStr(mHELPER_RATE)) & ", " & vbCrLf & " NET_LABOUR_COST = " & Val(CStr(mNET_LABOUR_COST)) & ", " & vbCrLf & " CONS_COST_PER_MONTH = " & Val(CStr(mCONS_COST_PER_MONTH)) & ", "

            SqlStr = SqlStr & vbCrLf & " STATUS = '" & mStatus & "', " & vbCrLf & " REMARKS = '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "',  " & vbCrLf & " PREP_BY = '" & MainClass.AllowSingleQuote(txtPrepBy.Text) & "',  " & vbCrLf & " APP_BY = '" & MainClass.AllowSingleQuote(txtAppBy.Text) & "', " & vbCrLf & " ModUser = '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " ModDate = TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & " WHERE Mkey = '" & MainClass.AllowSingleQuote(lblMKey.Text) & "'"
        End If
        PubDBCn.Execute(SqlStr)


        If Val(txtAmendNo.Text) > 0 Then
            If UpdatePreviousCost(Val(txtAmendNo.Text), "C") = False Then GoTo ErrPart
        End If

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsCustWeldCost.Requery()

        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
        '    Resume			
    End Function

    Private Function UpdatePreviousCost(ByRef pAmendNo As Integer, ByRef pPreviousStatus As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " UPDATE PRD_CUST_WELD_COST_MST SET " & vbCrLf & " STATUS = '" & pPreviousStatus & "', " & vbCrLf & " MODUSER = '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE = TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AMEND_NO = " & pAmendNo - 1 & "" & vbCrLf & " AND SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "'"

        PubDBCn.Execute(SqlStr)

        UpdatePreviousCost = True

        Exit Function
ErrPart:
        UpdatePreviousCost = False
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
        '    Resume			
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
        MainClass.ButtonStatus(Me, XRIGHT, RsCustWeldCost, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub AutoCalc()
        On Error GoTo AuERR
        Dim I As Integer
        Dim mWeldLength As Double
        Dim mWeldRate As Double
        Dim mTotWeldCost As Double

        Dim mCO2Length As Double
        Dim mCo2Rate As Double
        Dim mTotCo2Cost As Double

        Dim mNetCustWeldCost As Double

        Dim mProdPerShift As Double
        Dim mProdPerMonth As Double

        Dim mCostofMachine As Double
        Dim mIntRate As Double
        Dim mIntAmount As Double
        Dim mDepRate As Double
        Dim mDepAmount As Double
        Dim mMaintRate As Double
        Dim mMaintAmount As Double

        Dim mMCCostPerYear As Double
        Dim mMCCostPerMonth As Double
        Dim mNetMCCost As Double

        Dim mPowerCons As Double
        Dim mPowerEff As Double
        Dim mHSEBPer As Double
        Dim mDGPer As Double
        Dim mHSEBRate As Double
        Dim mDGRate As Double
        Dim mHSEBShareRate As Double
        Dim mDGShareRate As Double
        Dim mNetPowerCostPerUnit As Double
        Dim mElecLoadPerHour As Double
        Dim mWorkingHour As Double
        Dim mPowerCostperMonth As Double
        Dim mNetPowerCost As Double
        Dim mWelderCostPerMonth As Double
        Dim mHelperCostPerMonth As Double
        Dim mLabourCostPerMonth As Double
        Dim mNetLabourCost As Double
        Dim mConsPerMonth As Double
        Dim mNetConsCost As Double

        With SprdWeld
            .Col = 4
            .Row = 1
            mWeldLength = Val(.Text)

            .Row = 2
            mWeldRate = Val(.Text)

            If mWeldLength <= 0 Then
                mTotWeldCost = 0
            Else
                mTotWeldCost = CDbl(VB6.Format(mWeldRate / mWeldLength, "0.000"))
            End If

            .Row = 3
            .Text = VB6.Format(mTotWeldCost, "0.000")

            txtTotMIGCost.Text = VB6.Format(mTotWeldCost, "0.000")

        End With

        With SprdCO2
            .Col = 4
            .Row = 1
            mCO2Length = Val(.Text)

            .Row = 2
            mCo2Rate = Val(.Text)

            If mCO2Length <= 0 Then
                mTotCo2Cost = 0
            Else
                mTotCo2Cost = CDbl(VB6.Format(mCo2Rate / mCO2Length, "0.000"))
            End If

            .Row = 3
            .Text = VB6.Format(mTotCo2Cost, "0.000")

            txtTotCO2Cost.Text = VB6.Format(mTotCo2Cost, "0.000")

        End With



        With SprdMC
            .Col = 4
            .Row = 1
            mProdPerShift = Val(.Text)
            mProdPerMonth = mProdPerShift * 2 * 26

            .Row = 2
            .Text = CStr(mProdPerShift * 2)

            .Row = 3
            .Text = VB6.Format(mProdPerMonth, "0.000")

            .Row = 4
            mCostofMachine = Val(.Text)

            .Row = 5
            mIntRate = Val(.Text)

            .Row = 6
            mIntAmount = mCostofMachine * mIntRate * 0.01
            .Text = VB6.Format(mIntAmount, "0.000")

            .Row = 7
            mDepRate = Val(.Text)

            .Row = 8
            mDepAmount = mCostofMachine * mDepRate * 0.01
            .Text = VB6.Format(mDepAmount, "0.000")

            .Row = 9
            mMaintRate = Val(.Text)

            .Row = 10
            mMaintAmount = mCostofMachine * mMaintRate * 0.01
            .Text = VB6.Format(mMaintAmount, "0.000")

            mMCCostPerYear = mIntAmount + mDepAmount + mMaintAmount
            mMCCostPerMonth = mMCCostPerYear / 12
            If mProdPerMonth = 0 Then
                mNetMCCost = 0
            Else
                mNetMCCost = mMCCostPerMonth / mProdPerMonth
            End If

            .Row = 11
            .Text = VB6.Format(mMCCostPerYear, "0.000")

            .Row = 12
            .Text = VB6.Format(mMCCostPerMonth, "0.000")

            .Row = 13
            .Text = VB6.Format(mNetMCCost, "0.000")

            txtTotMCCost.Text = VB6.Format(mNetMCCost, "0.000")

        End With



        With SprdPower
            .Col = 4
            .Row = 1
            mPowerCons = Val(.Text)

            .Row = 2
            mPowerEff = Val(.Text)

            .Row = 3
            mHSEBPer = Val(.Text)
            mDGPer = 100 - mHSEBPer

            .Row = 4
            .Text = CStr(mDGPer)

            .Row = 5
            mHSEBRate = Val(.Text)

            .Row = 6
            mDGRate = Val(.Text)

            mHSEBShareRate = mHSEBPer * mHSEBRate * 0.01
            mDGShareRate = mDGPer * mDGRate * 0.01
            mNetPowerCostPerUnit = mHSEBShareRate + mDGShareRate

            .Row = 7
            .Text = CStr(mHSEBShareRate)

            .Row = 8
            .Text = CStr(mDGShareRate)

            .Row = 9
            .Text = CStr(mNetPowerCostPerUnit)

            .Row = 10
            mElecLoadPerHour = mPowerCons * mPowerEff * 0.01
            .Text = CStr(mElecLoadPerHour)

            .Row = 11
            mWorkingHour = Val(.Text)

            mPowerCostperMonth = mElecLoadPerHour * mNetPowerCostPerUnit * mWorkingHour * 26

            .Row = 12
            .Text = CStr(mPowerCostperMonth)

            If mProdPerMonth = 0 Then
                mNetPowerCost = 0
            Else
                mNetPowerCost = mPowerCostperMonth / mProdPerMonth
            End If

            .Row = 13
            .Text = VB6.Format(mNetPowerCost, "0.000")

            txtTotPowerCost.Text = VB6.Format(mNetPowerCost, "0.000")

        End With

        With SprdLabour
            .Col = 4
            .Row = 1
            mWelderCostPerMonth = Val(.Text)

            .Row = 2
            mHelperCostPerMonth = Val(.Text)

            .Row = 3
            mLabourCostPerMonth = mWelderCostPerMonth + mHelperCostPerMonth
            .Text = CStr(mLabourCostPerMonth)

            .Row = 4
            .Text = CStr(mProdPerMonth)

            If mProdPerMonth = 0 Then
                mNetLabourCost = 0
            Else
                mNetLabourCost = mLabourCostPerMonth / mProdPerMonth
            End If

            .Row = 5
            .Text = VB6.Format(mNetLabourCost, "0.000")

            txtTotLabourCost.Text = VB6.Format(mNetLabourCost, "0.000")

        End With

        With SprdCons
            .Col = 4
            .Row = 1
            mConsPerMonth = Val(.Text)

            .Row = 2
            .Text = CStr(mProdPerMonth)

            If mProdPerMonth = 0 Then
                mNetConsCost = 0
            Else
                mNetConsCost = mConsPerMonth / mProdPerMonth
            End If

            .Row = 3
            .Text = VB6.Format(mNetConsCost, "0.000")

            txtTotConsCost.Text = VB6.Format(mNetConsCost, "0.000")

        End With

        mNetCustWeldCost = CDbl(VB6.Format(mTotWeldCost + mTotCo2Cost + mNetMCCost + mNetPowerCost + mNetLabourCost + mNetConsCost + Val(txtSmokeCost.Text), "0.000"))

        txtNetWeldCost.Text = VB6.Format(mNetCustWeldCost, "0.000")
        Exit Sub
AuERR:
        '    Resume			
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdCO2_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdCO2.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdCO2_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdCO2.ClickEvent

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdCO2_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdCO2.LeaveCell
        On Error GoTo ErrPart

        Call AutoCalc()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdCO2_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdCO2.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        Call AutoCalc()

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SprdCons_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdCons.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdCons_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdCons.ClickEvent

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdCons_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdCons.LeaveCell
        On Error GoTo ErrPart

        Call AutoCalc()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdCons_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdCons.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        Call AutoCalc()

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SprdLabour_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdLabour.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdLabour_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdLabour.ClickEvent

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdLabour_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdLabour.LeaveCell
        On Error GoTo ErrPart

        Call AutoCalc()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdLabour_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdLabour.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        Call AutoCalc()

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SprdMC_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMC.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdMC_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMC.ClickEvent

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdMC_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMC.LeaveCell
        On Error GoTo ErrPart

        Call AutoCalc()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMC_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdMC.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        Call AutoCalc()

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SprdPower_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdPower.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdPower_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdPower.ClickEvent

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdPower_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdPower.LeaveCell
        On Error GoTo ErrPart

        Call AutoCalc()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdPower_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdPower.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        Call AutoCalc()

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        txtSuppCustCode.Text = SprdView.Text

        SprdView.Col = 3
        txtWEF.Text = VB6.Format(SprdView.Text, "DD/MM/YYYY")


        txtSuppCustCode_Validating(txtSuppCustCode, New System.ComponentModel.CancelEventArgs(False))
        ViewGrid()
    End Sub

    Private Sub SprdWeld_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdWeld.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdWeld_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdWeld.ClickEvent

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdWeld_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdWeld.LeaveCell
        On Error GoTo ErrPart

        Call AutoCalc()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAppBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppBy.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAppBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppBy.DoubleClick
        Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtAppBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAppBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtAppBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAppBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtAppBy.Text) = "" Then GoTo EventExitSub
        txtAppBy.Text = VB6.Format(Trim(txtAppBy.Text), "000000")

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.ValidateWithMasterTable(txtAppBy.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("EMPLOYEE Does Not Exist In Master.")
            Cancel = True
        Else
            lblAppBy.Text = MasterNo
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

    Private Sub txtSuppCustCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuppCustCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSuppCustCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuppCustCode.DoubleClick
        Call cmdSearchCust_Click(cmdSearchCust, New System.EventArgs())
    End Sub

    Private Sub txtSuppCustCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSuppCustCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchCust_Click(cmdSearchCust, New System.EventArgs())
    End Sub

    Private Sub txtSuppCustCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSuppCustCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String
        If Trim(txtSuppCustCode.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtSuppCustCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Customer Does Not Exist In Master.")
            Cancel = True
        Else
            txtSuppCustName.Text = MasterNo
        End If
        Call ShowRecord()
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtPrepBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrepBy.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPrepBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrepBy.DoubleClick
        Call cmdSearchPrepBy_Click(cmdSearchPrepBy, New System.EventArgs())
    End Sub

    Private Sub txtPrepBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPrepBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchPrepBy_Click(cmdSearchPrepBy, New System.EventArgs())
    End Sub

    Private Sub txtPrepBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPrepBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String
        If Trim(txtPrepBy.Text) = "" Then GoTo EventExitSub
        txtPrepBy.Text = VB6.Format(Trim(txtPrepBy.Text), "000000")
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.ValidateWithMasterTable(txtPrepBy, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Employee Does Not Exist In Master.")
            Cancel = True
        Else
            lblPrepBy.Text = MasterNo
        End If
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtWEF_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWEF_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtWEF.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchWEF_Click(cmdSearchWEF, New System.EventArgs())
    End Sub

    Private Sub txtWEF_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtWEF.Text) = "" Then GoTo EventExitSub

        If IsDate(txtWEF.Text) = False Then
            MsgBox("Invalid Date")
            Cancel = True
        Else
            Call ShowRecord()
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function ShowRecord() As Boolean

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String
        Dim xMkey As String

        ShowRecord = True

        If Trim(txtSuppCustCode.Text) = "" Then Exit Function

        If MODIFYMode = True And RsCustWeldCost.EOF = False Then xMkey = RsCustWeldCost.Fields("mKey").Value

        SqlStr = " SELECT * FROM PRD_CUST_WELD_COST_MST " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "' "

        If Trim(txtWEF.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf _
            & " AND WEF = (" & vbCrLf _
            & " SELECT MAX(WEF) AS WEF " & vbCrLf _
            & " FROM PRD_CUST_WELD_COST_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "') "

        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCustWeldCost, ADODB.LockTypeEnum.adLockReadOnly)

        If RsCustWeldCost.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Costing Not Made For This Item. Click Add For New.", MsgBoxStyle.Information)
                ShowRecord = False
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PRD_CUST_WELD_COST_MST " & vbCrLf & " WHERE UPPER(LTRIM(RTRIM(MKey)))='" & MainClass.AllowSingleQuote(UCase(xMkey)) & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCustWeldCost, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
End Class
