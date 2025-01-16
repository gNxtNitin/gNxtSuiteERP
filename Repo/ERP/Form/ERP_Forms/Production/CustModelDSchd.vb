Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCustModelDSchd
    Inherits System.Windows.Forms.Form
    Dim RsDSMain As ADODB.Recordset ''ADODB.Recordset					
    Dim RsDSDetail As ADODB.Recordset ''ADODB.Recordset					
    'Private PvtDBCn As ADODB.Connection					
    Dim mSearchStartRow As Integer

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String


    Private Const ConRowHeight As Short = 14
    Dim mAmendSchd As Boolean
    Dim mSerialDate As String

    Private Const ColModelCode As Short = 1
    Private Const ColModelDesc As Short = 2
    Private Const ColMonthPlanQty As Short = 3
    Private Const ColTodayPlanQty As Short = 4
    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtDSNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsDSMain.EOF = False Then RsDSMain.MoveFirst()
            Show1()
            txtDSNo.Enabled = True
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume					
    End Sub

    Private Sub cmdAmendSchd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAmendSchd.Click

        On Error GoTo ModifyErr

        '    If CDate(PubCurrDate) > CDate(txtScheduleDate.Text) Then					
        '        MsgInformation "MOnth Closed so Cann't be Modified."					
        '        Exit Sub					
        '    End If					

        ADDMode = False
        MODIFYMode = True
        MainClass.ButtonStatus(Me, XRIGHT, RsDSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        txtDSNo.Enabled = False
        txtDSAmendNo.Text = CStr(Val(txtDSAmendNo.Text) + 1)
        txtDSAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtScheduleDate.Enabled = False
        cmdAmendSchd.Enabled = False
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        mAmendSchd = True
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        MsgInformation("Cann't be Delete")
        Exit Sub

        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        RsDSMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Posted DS Cann't be Modified")
                Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsDSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtDSNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
            txtDSNo.Enabled = True

        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtDSNo_Validating(txtDSNo, New System.ComponentModel.CancelEventArgs(False))
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
    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim I As Integer
        Dim mTodayPlanQty As Double
        Dim mModelCode As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I
                .Col = ColModelCode
                mModelCode = Trim(.Text)

                .Col = ColTodayPlanQty
                mTodayPlanQty = Val(.Text)

                SqlStr = " UPDATE PPC_MODELWISE_MON_SCHD_TRN " & vbCrLf _
                & " SET ACTUAL_PLAN_QTY=" & mTodayPlanQty & "" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND AUTO_KEY_REF=" & Val(txtDSNo.Text) & "" & vbCrLf _
                & " AND MODEL_CODE='" & MainClass.AllowSingleQuote(mModelCode) & "'" & vbCrLf _
                & " AND SERIAL_DATE=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                PubDBCn.Execute(SqlStr)
            Next
        End With

        If UpdateProductWise() = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsDSMain.Requery()
        RsDSDetail.Requery()
        MsgBox(Err.Description)
        ''Resume					
    End Function
    Private Function UpdateProductWise() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim I As Integer
        'Dim SqlStr As String					
        Dim RsTemp As ADODB.Recordset
        Dim mModelCode As String
        Dim mProductCode As String
        Dim mPlanningQty As Double
        Dim mProdPlanNo As Double

        Dim SqlStr1 As String
        Dim RsTempDet As ADODB.Recordset

        Dim SqlStr2 As String
        Dim RsTempMonthly As ADODB.Recordset

        Dim mDeptCode As String
        Dim mDeptDesc As String

        Dim mSerialDate As String
        Dim mIPlanQty As Double
        Dim mActQty As Double
        Dim mItemSharing As Double

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColModelCode
                mModelCode = Trim(.Text)

                .Col = ColTodayPlanQty
                mPlanningQty = Val(.Text)

                SqlStr = "SELECT ITEM_CODE FROM INV_MODELWISE_PROD_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MODEL_CODE='" & mModelCode & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                Do While Not RsTemp.EOF
                    mProductCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                    If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "OP_QTY", "FIN_SUPP_CUST_DET", PubDBCn, MasterNo,  , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'") = True Then
                        mItemSharing = Val(MasterNo)
                    Else
                        GoTo NextRec
                    End If

                    mProdPlanNo = GetProdPlanNo(mProductCode)
                    SqlStr = " UPDATE PRD_PRODPLAN_HDR SET " & vbCrLf _
                        & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                        & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                        & " AND AUTO_KEY_PRODPLAN =" & Val(CStr(mProdPlanNo)) & ""

                    PubDBCn.Execute(SqlStr)

                    SqlStr = " UPDATE PRD_PRODPLAN_MONTH_DET SET " & vbCrLf _
                        & " DPLAN_QTY=" & mPlanningQty * mItemSharing / 100 & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                        & " AND SUBSTR(AUTO_KEY_PRODPLAN,LENGTH(AUTO_KEY_PRODPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
                        & " AND AUTO_KEY_PRODPLAN =" & Val(CStr(mProdPlanNo)) & "" & vbCrLf _
                        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
                        & " AND SERIAL_DATE=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"

                    PubDBCn.Execute(SqlStr)
NextRec:
                    RsTemp.MoveNext()
                Loop
            Next
        End With



        UpdateProductWise = True
        Exit Function
UpdateDetail1:
        UpdateProductWise = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume					
    End Function
    Private Function GetProdPlanNo(ByRef mProductCode As String) As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = "SELECT AUTO_KEY_PRODPLAN  " & vbCrLf _
        & " FROM PRD_PRODPLAN_HDR " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "' AND TO_CHAR(SCHLD_DATE,'YYYYMM')='" & VB6.Format(txtScheduleDate.Text, "YYYYMM") & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                GetProdPlanNo = IIf(IsDBNull(RsAutoGen.Fields("AUTO_KEY_PRODPLAN").Value), -1, RsAutoGen.Fields("AUTO_KEY_PRODPLAN").Value)
            Else
                GetProdPlanNo = -1
            End If
        End With

        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster(txtSupplierName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtSupplierName.Text = AcName
            txtSupplierName_Validating(txtSupplierName, New System.ComponentModel.CancelEventArgs(False))
            If txtCode.Enabled = True Then txtCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SearchCode()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster(txtCode.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", "SUPP_CUST_NAME", , , SqlStr) = True Then
            txtCode.Text = AcName
            txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
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
        MainClass.ButtonStatus(Me, XRIGHT, RsDSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmCustModelDSchd_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Customer Wise Model Wise Daily Schedule"

        SqlStr = "Select * From PPC_MODELWISE_MON_SCHD_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PPC_MODELWISE_MON_SCHD_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSDetail, ADODB.LockTypeEnum.adLockReadOnly)

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
        Dim SqlStr As String

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf & " A.AUTO_KEY_REF AS RefNo, A.REF_DATE As DS_DATE, " & vbCrLf & " A.AMEND_NO AS Amendno, A.AMEND_DATE AS AmendDate,  " & vbCrLf & " B.SUPP_CUST_NAME AS NAME, " & vbCrLf & " A.PLAN_MONTH, DECODE(A.PLAN_STATUS,'N','OPEN','CLOSE') AS Status, " & vbCrLf & " A.REMARKS, DECODE(A.POST_FLAG,'Y','YES','NO') AS Posted " & vbCrLf & " FROM PPC_MODELWISE_MON_SCHD_HDR A, FIN_SUPP_CUST_MST B " & vbCrLf & " WHERE A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE " & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & " ORDER BY A.AUTO_KEY_REF"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmCustModelDSchd_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmCustModelDSchd_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        mAccountCode = CStr(-1)
        lblMkey.Text = ""
        txtDSNo.Text = ""
        txtDSDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDSAmendNo.Text = CStr(0)
        txtDSAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtSupplierName.Text = ""
        txtCode.Text = ""
        txtCode.Enabled = True
        txtSupplierName.Enabled = True
        cmdsearch.Enabled = True
        SprdMain.Enabled = True

        txtScheduleDate.Text = "01/" & VB6.Format(Month(RunDate), "00") & "/" & VB6.Format(Year(RunDate), "0000")
        txtRemarks.Text = ""

        txtDSAmendNo.Enabled = False
        txtDSAmendDate.Enabled = False

        chkStatus.Enabled = False

        cmdAmendSchd.Enabled = False
        '    cboStatus.Enabled = False					
        txtDSDate.Enabled = False
        txtScheduleDate.Enabled = True

        mAmendSchd = False
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsDSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume					
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String


        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColModelCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsDSDetail.Fields("MODEL_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(.Col, 10)

            .Col = ColModelDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("MODEL_DESC", "GEN_MODEl_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 35)
            .TypeEditMultiLine = True


            .Col = ColMonthPlanQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSDetail.Fields("WEEK1_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColMonthPlanQty, 15)


            .Col = ColTodayPlanQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSDetail.Fields("WEEK2_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColTodayPlanQty, 15)


            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColModelCode, ColMonthPlanQty)
            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume					
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500)
            .set_ColWidth(1, 1000)
            .set_ColWidth(2, 1000)
            .set_ColWidth(3, 1000)
            .set_ColWidth(4, 1200)
            .set_ColWidth(5, 3500)
            .set_ColWidth(6, 1000)
            .set_ColWidth(7, 1000)
            .set_ColWidth(8, 1000)
            .set_ColWidth(9, 2000)
            .set_ColWidth(10, 1000)
            .set_ColWidth(11, 1200)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1

        txtDSNo.MaxLength = RsDSMain.Fields("AUTO_KEY_REF").Precision
        txtDSDate.MaxLength = RsDSMain.Fields("REF_DATE").DefinedSize - 6
        txtSupplierName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtRemarks.MaxLength = RsDSMain.Fields("REMARKS").DefinedSize

        txtDSAmendNo.MaxLength = RsDSMain.Fields("AMEND_NO").Precision
        txtDSAmendDate.MaxLength = RsDSMain.Fields("AMEND_DATE").DefinedSize - 6

        txtSupplierName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtCode.MaxLength = RsDSMain.Fields("SUPP_CUST_CODE").DefinedSize

        txtScheduleDate.MaxLength = RsDSMain.Fields("PLAN_MONTH").DefinedSize - 6



        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume					
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mModelCode As String
        Dim mTotQty As Double
        Dim I As Integer
        Dim pDSNo As Double

        FieldsVarification = True
        If ValidateBranchLocking((txtDSDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        '    If ValidateBookLocking(PubDBCn, ConLockPO_DS, txtDSDate) = True Then					
        '        FieldsVarification = False					
        '        Exit Function					
        '    End If					
        '					
        '    If ValidateAccountLocking(PubDBCn, txtScheduleDate.Text, txtSupplierName.Text) = True Then					
        '        FieldsVarification = False					
        '        Exit Function					
        '    End If					

        If MODIFYMode = True Then
            If mAmendSchd = False Then
                If RsDSMain.Fields("POST_FLAG").Value = "Y" Then
                    MsgInformation("Posted DS Cann't be Modified")
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            If RsDSMain.Fields("PLAN_STATUS").Value = "Y" Then
                MsgInformation("Closed DS Cann't be Modified")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsDSMain.EOF = True Then Exit Function

        If MODIFYMode = True And Trim(txtDSNo.Text) = "" Then
            MsgInformation("PO No. is Blank")
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtDSDate.Text) = "" Then
            MsgInformation(" PO Date is empty. Cannot Save")
            txtDSDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtDSDate.Text) <> "" Then
            If IsDate(txtDSDate.Text) = False Then
                MsgInformation(" Invalid PO Date. Cannot Save")
                txtDSDate.Focus()
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

        If MainClass.ValidateWithMasterTable(txtSupplierName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCode.Text = MasterNo
        Else
            MsgInformation("Invalid Supplier Name. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If VB6.Format(txtScheduleDate.Text, "YYYYMM") < VB6.Format(txtDSDate.Text, "YYYYMM") Then
            MsgInformation("Schedule Date Cann't be Less Than Delivery Schedule Date")
            txtScheduleDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        '    If DSExsistInCurrSchdMon(txtCode.Text, Trim(txtScheduleDate.Text)) = True Then					
        '        FieldsVarification = False					
        '        Exit Function					
        '    End If					

        ''28-09-2005					

        '    For I = 1 To SprdMain.MaxRows - 1					
        '        SprdMain.Row = I					
        '        SprdMain.Col = ColModelCode					
        '        mModelcode = Trim(UCase(SprdMain.Text))					
        '					
        '        SprdMain.Col = ColTotQty					
        '        mTotQty = Val(SprdMain.Text)					
        '					
        '        If mModelcode <> "" And mTotQty > 0 Then					
        '            If CheckDSDetailExists(mModelcode, I, mTotQty) = False Then					
        '                MsgInformation "Please Check Delivery Detail Qty. For Item Code :" & mModelcode					
        '                MainClass.SetFocusToCell SprdMain, I, ColItemDetail					
        '                FieldsVarification = False					
        '                Exit Function					
        '            End If					
        '        End If					
        '					
        '    Next					
        '					

        If MainClass.ValidDataInGrid(SprdMain, ColModelCode, "S", "Please Check Model Code.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColModelDesc, "S", "Please Check Model Description.") = False Then FieldsVarification = False


        '    If MainClass.ValidDataInGrid(SprdMain, ColTotQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False					

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume					
    End Function
    Private Sub frmCustModelDSchd_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        RsDSMain.Close()
        'RsOpOuts.Close					
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function CheckDuplicateItem(ByRef mModelCode As String, ByRef mCol As Integer) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

        If mModelCode = "" Then CheckDuplicateItem = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = mCol
                If UCase(Trim(.Text)) = UCase(Trim(mModelCode)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Model Code")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColModelCode)
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
            '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False					
        End With
    End Sub


    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        Dim SqlStr As String



        '    If Row = 0 And Col = ColModelCode And SprdMain.Enabled = True Then					
        '        With SprdMain					
        '            .Row = .ActiveRow					
        '            .Col = ColModelCode					
        '            If MainClass.SearchGridMaster(.Text, "GEN_MODEL_MST", "MODEL_CODE", "MODEL_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "MODEL_CODE") = True Then					
        '                .Row = .ActiveRow					
        '                .Col = ColModelCode					
        '                .Text = Trim(AcName)					
        '                .Col = ColModelDesc					
        '                .Text = Trim(AcName1)					
        '            End If					
        '            MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColModelCode					
        '        End With					
        '    End If					
        '					
        '    If Row = 0 And Col = ColModelDesc And SprdMain.Enabled = True Then					
        '        With SprdMain					
        '            .Row = .ActiveRow					
        '            .Col = ColModelDesc					
        '            If MainClass.SearchGridMaster(.Text, "GEN_MODEL_MST", "MODEL_DESC", "MODEL_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "MODEL_DESC") = True Then					
        '                .Row = .ActiveRow					
        '                .Col = ColModelDesc					
        '                .Text = Trim(AcName)					
        '                .Col = ColModelCode					
        '                .Text = Trim(AcName1)					
        '            End If					
        '            MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColModelCode					
        '        End With					
        '    End If					
        '					
        '    If Col = 0 And Row > 0 And (ADDMode = True Or MODIFYMode = True) Then					
        '        MainClass.DeleteSprdRow SprdMain, Row, ColModelDesc					
        '        MainClass.SaveStatus Me, ADDMode, MODIFYMode					
        '    End If					
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColModelCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColModelCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColModelDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColModelDesc, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xModelDesc As String

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColModelCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColModelCode
                xModelDesc = SprdMain.Text
                If xModelDesc = "" Then Exit Sub


                If GetValidModel(xModelDesc, True) = True Then
                    If CheckDuplicateItem(xModelDesc, ColModelCode) = False Then
                        If FillGridRow(xModelDesc, True) = False Then Exit Sub
                        MainClass.AddBlankSprdRow(SprdMain, ColModelDesc, ConRowHeight)
                        FormatSprdMain(eventArgs.row)
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColModelDesc)
                End If
            Case ColModelDesc
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColModelDesc
                xModelDesc = SprdMain.Text
                If xModelDesc = "" Then Exit Sub


                If GetValidModel(xModelDesc, False) = True Then
                    If CheckDuplicateItem(xModelDesc, ColModelDesc) = False Then
                        If FillGridRow(xModelDesc, False) = False Then Exit Sub
                        MainClass.AddBlankSprdRow(SprdMain, ColModelDesc, ConRowHeight)
                        FormatSprdMain(eventArgs.row)
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColModelDesc)
                End If

                '        Case ColTotQty					
                '            If CheckItemRate() = True Then					
                '                MainClass.AddBlankSprdRow SprdMain, ColModelDesc, ConRowHeight					
                '                FormatSprdMain -1					
                '            End If					
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FillGridRow(ByRef mModelDesc As String, ByRef mIsModelCode As Boolean) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String

        If mModelDesc = "" Then Exit Function

        SqlStr = ""
        SqlStr = " Select MODEL_CODE,MODEL_DESC" & vbCrLf & " FROM GEN_MODEL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If mIsModelCode = True Then
            SqlStr = SqlStr & vbCrLf & " AND MODEL_CODE='" & Trim(mModelDesc) & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND MODEL_DESC='" & Trim(mModelDesc) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc

                SprdMain.Col = ColModelCode
                SprdMain.Text = IIf(IsDBNull(.Fields("MODEL_CODE").Value), "", .Fields("MODEL_CODE").Value)

                SprdMain.Col = ColModelDesc
                SprdMain.Text = IIf(IsDBNull(.Fields("MODEL_DESC").Value), "", .Fields("MODEL_DESC").Value)
            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColModelCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function

    Private Sub SprdMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdMain.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        '    SprdView.Col = 1					
        '    SprdView.Row = SprdView.ActiveRow					
        '    txtDSNo.Text = SprdView.Text					
        '					
        '    txtDSNo_Validate False					
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    End Sub

    Private Sub txtDSAmendDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSAmendDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDSAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSAmendNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDSAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDSAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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
    Private Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String


        If Trim(txtCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtCode.Text, "SUPP_CUST_CODE", "SUPP_CUST_Name", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            txtSupplierName.Text = MasterNo
            txtCode.Enabled = False
        Else
            MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", vbInformation)
            Cancel = True
            Exit Sub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtScheduleDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtScheduleDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtScheduleDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtScheduleDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mCustomerCode As String

        If Trim(txtScheduleDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtScheduleDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(txtSupplierName.Text) = "" Then
            MsgInformation("Please Select Customer Name")
            '        Cancel = True					
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = MasterNo
        Else
            MsgInformation("Invalid Customer Name")
            Cancel = True
            Exit Sub
        End If


        mSerialDate = Trim(txtScheduleDate.Text)

        SqlStr = "SELECT * " & vbCrLf _
            & " FROM PPC_MODELWISE_MON_SCHD_HDR IH " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & mCustomerCode & "'" & vbCrLf _
            & " AND TO_CHAR(PLAN_MONTH,'YYYYMM')='" & VB6.Format(txtScheduleDate.Text, "YYYYMM") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsDSMain.EOF = False Then
            Clear1()
            Show1()
        Else
            MsgInformation("Monthly Plan is not Updated. Please entered Month Plan first.")
            Cancel = True
            GoTo EventExitSub
        End If

        '    If Format((txtScheduleDate), "YYYYMM") < Format(txtDSDate, "YYYYMM") Then					
        '        MsgInformation "Schedule Date Cann't be Less Than Delivery Schedule Date"					
        '        Cancel = True					
        '    End If					


        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtSupplierName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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
        Dim SqlStr As String
        Dim mCustomerCode As String

        If Trim(txtSupplierName.Text) = "" Then GoTo EventExitSub
        If Trim(txtScheduleDate.Text) = "" Then GoTo EventExitSub

        mSerialDate = VB6.Format(txtScheduleDate.Text, "DD/MM/YYYY")
        If MainClass.ValidateWithMasterTable(txtSupplierName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            mCustomerCode = MasterNo
            txtCode.Text = mCustomerCode
            txtCode.Enabled = False
        Else
            MsgBox("Invalid Supplier Name.", vbInformation)
            Cancel = True
            Exit Sub
        End If



        If Trim(txtScheduleDate.Text) = "" Then GoTo EventExitSub

        SqlStr = "SELECT * " & vbCrLf _
            & " FROM PPC_MODELWISE_MON_SCHD_HDR IH " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & mCustomerCode & "'" & vbCrLf _
            & " AND TO_CHAR(PLAN_MONTH,'YYYYMM')='" & VB6.Format(txtScheduleDate.Text, "YYYYMM") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsDSMain.EOF = False Then
            Clear1()
            Show1()
        Else
            MsgInformation("Monthly Plan is not Updated. Please entered Month Plan first.")
            '        Cancel = True					
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

        Clear1()
        If Not RsDSMain.EOF Then

            lblMkey.Text = IIf(IsDBNull(RsDSMain.Fields("AUTO_KEY_REF").Value), "", RsDSMain.Fields("AUTO_KEY_REF").Value)
            txtDSNo.Text = IIf(IsDBNull(RsDSMain.Fields("AUTO_KEY_REF").Value), "", RsDSMain.Fields("AUTO_KEY_REF").Value)
            txtDSDate.Text = VB6.Format(IIf(IsDBNull(RsDSMain.Fields("REF_DATE").Value), "", RsDSMain.Fields("REF_DATE").Value), "DD/MM/YYYY")
            txtDSAmendNo.Text = IIf(IsDBNull(RsDSMain.Fields("AMEND_NO").Value), 0, RsDSMain.Fields("AMEND_NO").Value)
            txtDSAmendDate.Text = VB6.Format(IIf(IsDBNull(RsDSMain.Fields("AMEND_DATE").Value), "", RsDSMain.Fields("AMEND_DATE").Value), "DD/MM/YYYY")

            txtRemarks.Text = IIf(IsDBNull(RsDSMain.Fields("REMARKS").Value), "", RsDSMain.Fields("REMARKS").Value)

            chkStatus.CheckState = IIf(RsDSMain.Fields("POST_FLAG").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            cmdAmendSchd.Enabled = IIf(RsDSMain.Fields("POST_FLAG").Value = "Y", True, False)

            If RsDSMain.Fields("PLAN_STATUS").Value = "Y" Then
                cmdAmendSchd.Enabled = False
            End If

            txtScheduleDate.Text = VB6.Format(mSerialDate, "DD/MM/YYYY") '' Format(IIf(IsNull(RsDSMain.Fields("PLAN_MONTH").Value), "", RsDSMain.Fields("PLAN_MONTH").Value), "DD/MM/YYYY")					
            '        txtScheduleDate.Enabled = IIf(RsDSMain!POST_FLAG = "Y", False, True)					


            mAccountCode = IIf(IsDBNull(RsDSMain.Fields("SUPP_CUST_CODE").Value), -1, RsDSMain.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = MasterNo
            End If
            txtSupplierName.Text = mAccountName
            txtCode.Text = Trim(IIf(IsDBNull(RsDSMain.Fields("SUPP_CUST_CODE").Value), "", RsDSMain.Fields("SUPP_CUST_CODE").Value))
            txtCode.Enabled = False
            txtSupplierName.Enabled = False
            cmdsearch.Enabled = False
            mAmendSchd = False
            Call ShowDetail1()

        End If

        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColModelDesc, ColMonthPlanQty)
        MainClass.ButtonStatus(Me, XRIGHT, RsDSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume					
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim mModelCode As String
        Dim mItemDesc As String
        Dim RsDSDailyDetail As ADODB.Recordset

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PPC_MODELWISE_MON_SCHD_DET " & vbCrLf _
            & " Where AUTO_KEY_REF=" & Val(lblMkey.Text) & "" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsDSDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1					
            I = 1
            '        .MoveFirst					

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColModelCode
                mModelCode = Trim(IIf(IsDBNull(.Fields("MODEL_CODE").Value), "", .Fields("MODEL_CODE").Value))
                SprdMain.Text = mModelCode

                SprdMain.Col = ColModelDesc
                MainClass.ValidateWithMasterTable(mModelCode, "MODEL_CODE", "MODEL_DESC", "GEN_MODEL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SqlStr = " SELECT * " & vbCrLf _
                    & " FROM PPC_MODELWISE_MON_SCHD_TRN " & vbCrLf _
                    & " Where AUTO_KEY_REF=" & Val(lblMkey.Text) & "" & vbCrLf _
                    & " AND MODEL_CODE='" & mModelCode & "'" & vbCrLf _
                    & " AND SERIAL_DATE=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSDailyDetail, ADODB.LockTypeEnum.adLockReadOnly)

                If RsDSDailyDetail.EOF = False Then
                    SprdMain.Col = ColMonthPlanQty
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(RsDSDailyDetail.Fields("PLANNED_QTY").Value), 0, RsDSDailyDetail.Fields("PLANNED_QTY").Value)))

                    SprdMain.Col = ColTodayPlanQty
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(RsDSDailyDetail.Fields("ACTUAL_PLAN_QTY").Value), 0, RsDSDailyDetail.Fields("ACTUAL_PLAN_QTY").Value)))
                Else
                    SprdMain.Col = ColMonthPlanQty
                    SprdMain.Text = CStr(0)

                    SprdMain.Col = ColTodayPlanQty
                    SprdMain.Text = CStr(0)
                End If

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        mSerialDate = txtScheduleDate.Text
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume					
    End Sub
    Private Sub txtDSDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDSNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDSNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDSNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtDSNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDSNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String
        Dim mDSNo As Double
        Dim SqlStr As String

        If Trim(txtDSNo.Text) = "" Then GoTo EventExitSub

        If Len(txtDSNo.Text) < 6 Then
            txtDSNo.Text = Val(txtDSNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If


        mDSNo = Val(txtDSNo.Text)

        If MODIFYMode = True And RsDSMain.BOF = False Then xMkey = RsDSMain.Fields("AUTO_KEY_REF").Value

        SqlStr = "SELECT * FROM PPC_MODELWISE_MON_SCHD_HDR " & " WHERE AUTO_KEY_REF='" & MainClass.AllowSingleQuote(UCase(CStr(mDSNo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsDSMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such PO No. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PPC_MODELWISE_MON_SCHD_HDR WHERE AUTO_KEY_REF=" & Val(xMkey) & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub





    Private Function GetValidModel(ByRef pModelDesc As String, ByRef mIsCode As Boolean) As Boolean
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset


        If mIsCode = True Then
            If MainClass.ValidateWithMasterTable(pModelDesc, "MODEL_CODE", "MODEL_DESC", "GEN_MODEL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                GetValidModel = True
                Exit Function
            Else
                MsgInformation("Invalid Model.")
                GetValidModel = False
                Exit Function
            End If
        Else
            If MainClass.ValidateWithMasterTable(pModelDesc, "MODEL_DESC", "MODEL_CODE", "GEN_MODEL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                GetValidModel = True
                Exit Function
            Else
                MsgInformation("Invalid Model.")
                GetValidModel = False
                Exit Function
            End If
        End If

        Exit Function
ErrPart:
        GetValidModel = False
    End Function
    Private Function SelectQryForDS(ByRef mSqlStr As String) As String

        ''''SELECT CLAUSE...					

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC,"

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, " & vbCrLf & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE"

        ''''FROM CLAUSE...					
        mSqlStr = mSqlStr & vbCrLf & " FROM PPC_MODELWISE_MON_SCHD_HDR IH, PPC_MODELWISE_MON_SCHD_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''''WHERE CLAUSE...					
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REF=" & Val(txtDSNo.Text) & ""

        ''''ORDER CLAUSE...					

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForDS = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        '    mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(lblNetAmount.text) = 0, 0, lblNetAmount.text)))					
        '					
        '    MainClass.AssignCRptFormulas Report1, "AmountInWord=""" & mAmountInword & """"					
        '    MainClass.AssignCRptFormulas Report1, "NetAmount=""" & lblNetAmount.text & """"					

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub ReportOnDS(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String
        Dim mRptFileName As String
        Dim mVNo As String
        Dim Response As String
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""
        mSubTitle = ""
        Call MainClass.ClearCRptFormulas(Report1)

        '    Call SelectQryForDS(SqlStr)					
        '    mTitle = "Delivery Schedule"					
        '    mRptFileName = "DS.rpt"					
        '					
        '    Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)					
        '					
        '    Response = MsgQuestion("Do You Want to Print Detail Delivery Schedule?")					
        '					
        '    If Response = vbYes Then					
        '        Call MainClass.ClearCRptFormulas(Report1)					
        '					
        '        Call SelectQryForDailyDS(SqlStr)					
        '        mTitle = "Shortage Follow-up register for the month of " & vb6.Format(txtScheduleDate, "MMMM , YYYY")					
        '        mRptFileName = "DSDetail.rpt"					
        '					
        '        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)					
        '    End If					

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryForDailyDS(ByRef mSqlStr As String) As String

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME"

        ''''FROM CLAUSE...					
        mSqlStr = mSqlStr & vbCrLf & " FROM PPC_MODELWISE_MON_SCHD_HDR IH, PPC_MODELWISE_MON_SCHD_TRN ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''''WHERE CLAUSE...					
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REF=" & Val(txtDSNo.Text) & "" & vbCrLf

        ''''ORDER CLAUSE...					

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_DATE"

        SelectQryForDailyDS = mSqlStr

    End Function
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnDS(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnDS(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
