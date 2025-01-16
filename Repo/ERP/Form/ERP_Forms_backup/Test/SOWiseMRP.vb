Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSOWiseMRP
    Inherits System.Windows.Forms.Form
    Dim RsMRPMain As ADODB.Recordset
    Dim RsMRPDetail As ADODB.Recordset
    Private PvtDBCn As ADODB.Connection

    'Dim IsShowingRecord As Boolean
    'Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 18

    Private Const ColItemCode As Short = 1
    Private Const ColItemPartNo As Short = 2
    Private Const ColItemName As Short = 3
    Private Const ColReorderQty As Short = 4
    Private Const ColStockQty As Short = 5
    Private Const ColSOQty As Short = 6
    Private Const ColLastPrice As Short = 7
    Private Const ColLastSupplier As Short = 8
    Private Const ColRequiredQty As Short = 9

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtPlanNo.Enabled = False
            cmdSearchPlanNo.Enabled = False
            SprdMain.Enabled = True
            PopulateMode((True))
        Else
            ADDMode = False
            MODIFYMode = False
            If RsMRPMain.EOF = False Then RsMRPMain.MoveFirst()
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume	
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtPlanNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsMRPMain.EOF Then
            If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then MsgBox("Production Data has been Input For This Schedule Date, So Cann't be deleted") : Exit Sub
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PRD_PRODPLAN_HDR", (txtPlanNo.Text), RsMRPMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_PRODPLAN_HDR", "AUTO_KEY_PRODPLAN", (lblMkey.Text)) = False Then GoTo DelErrPart

                '            If DelProdPlanMonthlyDetail = False Then GoTo DelErrPart:	
                PubDBCn.Execute("DELETE FROM PRD_PRODPLAN_MONTH_DET WHERE AUTO_KEY_PRODPLAN=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM PRD_PRODPLAN_DET WHERE AUTO_KEY_PRODPLAN=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM PRD_PRODPLAN_HDR WHERE AUTO_KEY_PRODPLAN=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsMRPMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        RsMRPMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsMRPMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtPlanNo.Enabled = False
            cmdSearchPlanNo.Enabled = False
            SprdMain.Enabled = True
            PopulateMode((True))
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
        Resume
    End Sub

    Private Sub cmdOnceWeek_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call FillQty(5, 6, , True)
        'txtPlanningQty.Enabled = False
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtPlanNo_Validating(txtPlanNo, New System.ComponentModel.CancelEventArgs(False))
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
        Dim SqlStr As String = ""
        Dim mProdPlanNo As Double
        Dim mStatus As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mProdPlanNo = Val(txtPlanNo.Text)
        If Val(txtPlanNo.Text) = 0 Then
            mProdPlanNo = AutoGenKeyNo()
        End If

        'If ADDMode = True Then
        '    lblMkey.Text = CStr(mProdPlanNo)
        '    SqlStr = " INSERT INTO PRD_PRODPLAN_HDR " & vbCrLf _
        '        & " (AUTO_KEY_PRODPLAN ,COMPANY_CODE," & vbCrLf _
        '        & " PRODUCT_CODE,SCHLD_DATE," & vbCrLf _
        '        & " CUST_ORD_QTY,REMARKS,PLAN_STATUS," & vbCrLf _
        '        & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
        '        & " VALUES ( " & vbCrLf _
        '        & " " & mProdPlanNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
        '        & " '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'," & vbCrLf _
        '        & " TO_DATE('" & VB6.Format(txtPlanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(txtPlanningQty.Text) & ", " & vbCrLf _
        '        & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','" & mStatus & "', " & vbCrLf _
        '        & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        'ElseIf MODIFYMode = True Then
        '    SqlStr = " UPDATE PRD_PRODPLAN_HDR SET " & vbCrLf _
        '        & " AUTO_KEY_PRODPLAN=" & mProdPlanNo & ",COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
        '        & " PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf _
        '        & " SCHLD_DATE=TO_DATE('" & VB6.Format(txtPlanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
        '        & " CUST_ORD_QTY=" & Val(txtPlanningQty.Text) & ", " & vbCrLf _
        '        & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
        '        & " PLAN_STATUS='" & mStatus & "', " & vbCrLf _
        '        & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
        '        & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
        '        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        '        & " AND AUTO_KEY_PRODPLAN =" & Val(lblMkey.Text) & ""
        'End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail() = False Then GoTo ErrPart


        PubDBCn.CommitTrans()
        Update1 = True

        txtPlanNo.Text = CStr(mProdPlanNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsMRPMain.Requery()
        RsMRPDetail.Requery()
        MsgBox(Err.Description)
        ''Resume	
    End Function

    Private Function UpdateDetail() As Boolean

        On Error GoTo UpdateDetailERR
        Dim SqlStr As String = ""
        Dim mRow As Integer
        Dim mCol As Integer
        Dim mDate As String
        Dim mDept As String
        Dim mDeptDesc As String
        Dim mIPlanQty As Integer
        Dim mDPlanQty As Integer
        Dim mProdLoss As String
        Dim mPlanStart As String
        Dim InHouseCode As String
        Dim mModDate As String

        mModDate = VB6.Format(GetServerDate, "DD-MMM-YYYY") & " " & GetServerTime()

        SqlStr = " INSERT INTO PRD_PRODPLAN_MONTH_DET_HIS ( " & vbCrLf _
            & " USERID, MODDATE, AUTO_KEY_PRODPLAN, " & vbCrLf _
            & " COMPANY_CODE, PRODUCT_CODE," & vbCrLf _
            & " SCHLD_DATE, DEPT_CODE, SERIAL_DATE," & vbCrLf _
            & " IPLAN_QTY, DPLAN_QTY, " & vbCrLf _
            & " PROD_LOSS, PLAN_START, INHOUSE_CODE) " & vbCrLf _
            & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & mModDate & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
            & " AUTO_KEY_PRODPLAN, " & vbCrLf _
            & " COMPANY_CODE, PRODUCT_CODE," & vbCrLf _
            & " SCHLD_DATE, DEPT_CODE, SERIAL_DATE," & vbCrLf _
            & " IPLAN_QTY, DPLAN_QTY, " & vbCrLf _
            & " PROD_LOSS, PLAN_START, INHOUSE_CODE" & vbCrLf _
            & " FROM PRD_PRODPLAN_MONTH_DET " & vbCrLf _
            & " WHERE AUTO_KEY_PRODPLAN=" & Val(lblMkey.Text) & ""

        PubDBCn.Execute(SqlStr)

        PubDBCn.Execute("DELETE FROM PRD_PRODPLAN_MONTH_DET WHERE AUTO_KEY_PRODPLAN=" & Val(lblMkey.Text) & "")
        PubDBCn.Execute("DELETE FROM PRD_PRODPLAN_DET WHERE AUTO_KEY_PRODPLAN=" & Val(lblMkey.Text) & "")

        With SprdMain
            For mCol = 2 To .MaxCols
                .Row = FPSpreadADO.CoordConstants.SpreadHeader    '' 0

                .Col = mCol
                InHouseCode = Trim(.Text)        '' Mid(Trim(.Text), 1, InStr(Trim(.Text), "-") - 1)

                .Row = FPSpreadADO.CoordConstants.SpreadHeader + 2
                mDept = Trim(.Text)        ''Mid(Trim(.Text), InStr(Trim(.Text), "-") + 1)



                If MainClass.ValidateWithMasterTable(mDept, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                    mDeptDesc = MasterNo

                    SqlStr = " INSERT INTO PRD_PRODPLAN_DET ( " & vbCrLf _
                        & " AUTO_KEY_PRODPLAN,COMPANY_CODE, " & vbCrLf _
                        & " PRODUCT_CODE,SCHLD_DATE,DEPT_CODE,DEPT_DESC,INHOUSE_CODE) " & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & Val(lblMkey.Text) & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf _
                        & " TO_DATE('" & VB6.Format(txtPlanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " '" & mDept & "', " & vbCrLf & " '" & mDeptDesc & "','" & MainClass.AllowSingleQuote(InHouseCode) & "') "

                    PubDBCn.Execute(SqlStr)

                    For mRow = 1 To .MaxRows
                        .Row = mRow

                        .Col = ColDate
                        mDate = Trim(.Text)

                        .Col = mCol
                        mIPlanQty = Val(.Text)

                        .Col = mCol + 1
                        mDPlanQty = Val(.Text)

                        .Col = mCol + 2
                        mProdLoss = MainClass.AllowSingleQuote(.Text)

                        .Col = mCol + 3
                        mPlanStart = MainClass.AllowSingleQuote(.Text)
                        If mPlanStart = "" Then
                            mPlanStart = "N"
                        End If

                        If mDate <> "" Then
                            SqlStr = "INSERT INTO PRD_PRODPLAN_MONTH_DET (" & vbCrLf _
                                & " AUTO_KEY_PRODPLAN,COMPANY_CODE, " & vbCrLf _
                                & " PRODUCT_CODE,SCHLD_DATE,DEPT_CODE,SERIAL_DATE, " & vbCrLf _
                                & " IPLAN_QTY,DPLAN_QTY," & vbCrLf _
                                & " PROD_LOSS,PLAN_START,INHOUSE_CODE )" & vbCrLf _
                                & " VALUES (" & vbCrLf _
                                & " " & Val(lblMkey.Text) & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                                & " '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf _
                                & " TO_DATE('" & VB6.Format(txtPlanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                                & " '" & mDept & "',TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                                & " " & mIPlanQty & "," & mDPlanQty & "," & vbCrLf _
                                & " '" & mProdLoss & "','" & mPlanStart & "','" & MainClass.AllowSingleQuote(InHouseCode) & "') "
                            PubDBCn.Execute(SqlStr)
                        End If
                    Next
                End If
                mCol = mCol + 3
            Next
        End With
        UpdateDetail = True
        Exit Function
UpdateDetailERR:
        UpdateDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Function

    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Double
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_PRODPLAN)  " & vbCrLf _
            & " FROM PRD_PRODPLAN_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_PRODPLAN,LENGTH(AUTO_KEY_PRODPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = Mid(mMaxValue, 1, Len(mMaxValue) - 6)
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With
        AutoGenKeyNo = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Sub cmdSched_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mRow As Integer
        Dim mCol As Integer
        Dim mSchdQty As Double
        Dim mSerialDate As String
        Dim mDSSerialDate As String
        Dim mDSSchdQty As Double

        If Trim(txtProductCode.Text) = "" Then MsgInformation("Please Select Product Name") : Exit Sub
        If Trim(txtPlanDate.Text) = "" Then MsgInformation("Please Select Planning Date Name") : Exit Sub

        If Val(txtScheduleQty.Text) = 0 Then MsgInformation("Schedule Qty is Zero. Please Check Customer Schedule") : Exit Sub
        mSchdQty = 0

        SqlStr = " SELECT SERIAL_DATE, SUM(PLANNED_QTY) AS ITEM_QTY " & vbCrLf _
            & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID" & vbCrLf _
            & " WHERE IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
            & " AND To_CHAR(IH.SCHLD_DATE,'MON-YYYY')='" & UCase(Format(txtPlanDate.Text, "MMM-YYYY")) & "'" & vbCrLf _
            & " GROUP BY SERIAL_DATE" & vbCrLf _
            & " ORDER BY SERIAL_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdMain
            If Not RsTemp.EOF Then
                Do While Not RsTemp.EOF
                    mDSSerialDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SERIAL_DATE").Value), "", RsTemp.Fields("SERIAL_DATE").Value), "DD/MM/YYYY")
                    mDSSchdQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), "", RsTemp.Fields("ITEM_QTY").Value)
                    mSchdQty = mSchdQty + mDSSchdQty
                    For mRow = 1 To .MaxRows
                        .Row = mRow

                        .Col = ColDate
                        mSerialDate = VB6.Format(.Text, "DD/MM/YYYY")

                        If mSerialDate = mDSSerialDate Then
                            For mCol = 2 To .MaxCols
                                .Col = mCol
                                .Text = VB6.Format(mDSSchdQty, "0.00")

                                mCol = mCol + 3
                            Next
                        End If
                    Next
                    RsTemp.MoveNext()
                Loop
            End If
        End With
        'txtPlanningQty.Text = VB6.Format(mSchdQty, "0.00")
        'txtPlanningQty.Enabled = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchProduct_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProduct.Click
        Dim SqlStr As String = ""
        SqlStr = " SELECT B.ITEM_CODE, B.ITEM_SHORT_DESC, B.CUSTOMER_PART_NO " & vbCrLf _
            & " FROM INV_ITEM_MST B, INV_GENERAL_MST C " & vbCrLf _
            & " WHERE B.COMPANY_CODE =C.COMPANY_CODE " & vbCrLf _
            & " AND B.CATEGORY_CODE = C.GEN_CODE " & vbCrLf _
            & " AND C.GEN_TYPE='C' AND C.PRD_TYPE IN ('P','I')" & vbCrLf _
            & " AND B.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "

        'C.STOCKTYPE='FG'

        'SqlStr = SqlStr & vbCrLf _
        '    & " AND B.ITEM_CODE IN (SELECT DISTINCT ITEM_CODE" & vbCrLf _
        '    & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DAILY_SCHLD_DET ID" & vbCrLf _
        '    & " WHERE IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf _
        '    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")"

        SqlStr = SqlStr & vbCrLf _
            & " ORDER BY B.ITEM_SHORT_DESC "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtProductCode.Text = AcName
            lblProductCode.Text = AcName1
            If txtProductCode.Enabled = True Then txtProductCode.Focus()
        End If
    End Sub

    Private Sub cmdSearchPlanNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPlanNo.Click
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUBSTR(AUTO_KEY_PRODPLAN,LENGTH(AUTO_KEY_PRODPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & " "
        If MainClass.SearchGridMaster(txtPlanNo.Text, "PRD_PRODPLAN_HDR", "AUTO_KEY_PRODPLAN", "PRODUCT_CODE", "SCHLD_DATE", SqlStr) = True Then
            txtPlanNo.Text = AcName
            'Call txtPlanNo_Validate(False)
            txtPlanNo_Validating(txtPlanNo, New System.ComponentModel.CancelEventArgs(False))
        End If
    End Sub

    Private Sub FillQty(ByRef pWorkingDays As Integer, ByRef pGapDays As Integer, Optional ByRef pGapDays2 As Integer = 0, Optional ByRef pCheckWorkingDays As Boolean = False)
        On Error GoTo ErrPart
        Dim I As Integer
        Dim mRow As Integer
        Dim mCol As Integer
        Dim mHolidays As Integer
        Dim mWorkingdays As Integer
        Dim mActualDays As Integer
        Dim mGapDays As Integer
        Dim mSchdQty As Integer
        Dim mSerialDate As String

        If Val(txtPlanningQty.Text) = 0 Then MsgInformation("Please Enter Order Qty.") : Exit Sub

        mActualDays = 0

        mWorkingdays = pWorkingDays

ReStartFilling:

        mGapDays = pGapDays
        mSchdQty = Val(txtPlanningQty.Text) / mWorkingdays
        mHolidays = 0

        With SprdMain
            For mRow = 1 To .MaxRows
                .Row = mRow

                .Col = ColDate
                mSerialDate = Trim(.Text)

                If IsHoliday(mSerialDate) = False Then
                    For mCol = 2 To .MaxCols
                        .Col = mCol
                        .Text = CStr(mSchdQty)

                        mCol = mCol + 3
                    Next
                    For I = mRow + 1 To mRow + mGapDays - mHolidays
                        .Row = I
                        For mCol = 2 To .MaxCols
                            .Col = mCol
                            .Text = CStr(0)

                            mCol = mCol + 3
                        Next
                    Next
                    mRow = mRow + mGapDays - mHolidays
                    mHolidays = 0
                    mActualDays = mActualDays + 1
                    If pGapDays2 <> 0 Then
                        If mGapDays = pGapDays Then
                            mGapDays = pGapDays2
                        Else
                            mGapDays = pGapDays
                        End If
                    End If
                Else
                    For mCol = 2 To .MaxCols
                        .Col = mCol
                        .Text = CStr(0)

                        mCol = mCol + 3
                    Next
                    mHolidays = mHolidays + 1
                End If
            Next
        End With

        If pCheckWorkingDays = True Then
            If mActualDays <> pWorkingDays Then
                mWorkingdays = mActualDays
                mActualDays = 0
                pCheckWorkingDays = False
                GoTo ReStartFilling
            End If
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdTwiceMonth_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call FillQty(2, 14, , False)
        'txtPlanningQty.Enabled = False
    End Sub

    Private Sub cmdTwiceWeek_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call FillQty(9, 3, 2, True)
        'txtPlanningQty.Enabled = False
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
        MainClass.ButtonStatus(Me, XRIGHT, RsMRPMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmSOWiseMRP_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Production Plan (Monthly Schedule)"

        SqlStr = "Select * From PRD_PRODPLAN_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRPMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PRD_PRODPLAN_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRPDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PRD_PRODPLAN_MONTH_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRPMonDetail, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)
        Call SetTextLengths()
        Call Clear1()
        FormatSprdMain(-1)
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf _
            & " AUTO_KEY_PRODPLAN AS PLAN_NUMBER,PRODUCT_CODE,TO_CHAR(SCHLD_DATE,'DD-MM-YYYY') AS SCHLD_DATE, " & vbCrLf _
            & " CUST_ORD_QTY,REMARKS,PLAN_STATUS " & vbCrLf _
            & " FROM PRD_PRODPLAN_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_PRODPLAN,LENGTH(AUTO_KEY_PRODPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " ORDER BY AUTO_KEY_PRODPLAN"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmSOWiseMRP_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmSOWiseMRP_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(7710)
        'Me.Width = VB6.TwipsToPixelsX(11385)
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
        lblMkey.Text = ""
        txtPlanNo.Text = ""
        txtProductCode.Text = ""
        lblProductCode.Text = ""
        txtPlanDate.Text = ""
        txtPlanningQty.Text = ""
        txtCapacity.Text = ""
        txtRemarks.Text = ""
        txtScheduleQty.Text = ""
        txtScheduleQty.Enabled = False
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        ClearSprdHeader()
        txtPlanningQty.Enabled = True
        '    FormatSprdMain -1	
        MainClass.ButtonStatus(Me, XRIGHT, RsMRPMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume	
    End Sub

    Private Sub ClearSprdHeader()
        On Error GoTo ClearErr
        Dim I As Integer
        With SprdMain
            .MaxRows = 1
            .MaxCols = 12
            For I = 2 To .MaxCols
                .Col = I

                .Row = FPSpreadADO.CoordConstants.SpreadHeader
                .Text = ""

                .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1
                .Text = " "

                .Row = FPSpreadADO.CoordConstants.SpreadHeader + 2
                .Text = " "

                .Row = FPSpreadADO.CoordConstants.SpreadHeader + 3
                .Text = " "

                .Row = FPSpreadADO.CoordConstants.SpreadHeader + 4
                .Text = " "

                .Row = FPSpreadADO.CoordConstants.SpreadHeader + 5
                .Text = " "

            Next
        End With
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume	
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer
        Dim j As Integer

        With SprdMain
            SprdMain.ColHeaderRows = 6
            .set_RowHeight(-1, ConRowHeight)


            .Row = Arow

            .Col = ColDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY

            For I = 2 To .MaxCols
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeFloatDecimalPlaces = 3
                .set_ColWidth(.Col, 12)
                For j = 1 To 1
                    .Col = I + j
                    .CellType = SS_CELL_TYPE_FLOAT
                    .TypeFloatDecimalChar = Asc(".")
                    .TypeFloatMax = CDbl("999999999.999")
                    .TypeFloatMin = CDbl("-999999999.999")
                    .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                    .TypeFloatDecimalPlaces = 3
                    .ColHidden = True
                    .set_ColWidth(.Col, 12)
                Next

                .Col = I + 2
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditLen = RsMRPMonDetail.Fields("PROD_LOSS").DefinedSize
                .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
                .TypeEditMultiLine = True
                .ColHidden = True

                .Col = I + 3
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditLen = RsMRPMonDetail.Fields("PLAN_START").DefinedSize
                .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
                .TypeEditMultiLine = True
                .ColHidden = True

                I = I + 3
            Next

            MainClass.UnProtectCell(SprdMain, 1, .MaxRows, ColDate, SprdMain.MaxCols)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColDate, ColDate)
        End With
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500)
            .set_ColWidth(1, 500 * 4)
            .set_ColWidth(2, 500 * 3)
            .set_ColWidth(3, 500 * 3)
            .set_ColWidth(4, 500 * 3)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 5)
            .set_ColWidth(5, 500 * 3)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtPlanNo.MaxLength = RsMRPMain.Fields("AUTO_KEY_PRODPLAN").Precision
        txtProductCode.MaxLength = RsMRPMain.Fields("PRODUCT_CODE").DefinedSize
        txtPlanDate.MaxLength = RsMRPMain.Fields("SCHLD_DATE").Precision - 6
        txtPlanningQty.MaxLength = RsMRPMain.Fields("CUST_ORD_QTY").Precision
        txtRemarks.MaxLength = RsMRPMain.Fields("REMARKS").DefinedSize

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub

    Private Function GetProdSeq() As Boolean

        On Error GoTo GetERR
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim RsGet As ADODB.Recordset = Nothing
        Dim mFinalDept As String
        Dim mRunDate As String
        Dim mFinalDeptCheck As Boolean
        Dim mSFCode As String
        Dim mDeptCode As String
        Dim mProductName As String
        Dim mLastDate As String
        Dim mTotalPlanningQty As Double = 0

        '    SqlStr = " SELECT DEPT_CODE,SERIAL_NO " & vbCrLf _	
        ''            & " FROM PRD_PRODSEQUENCE_DET " & vbCrLf _	
        ''            & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _	
        ''            & " AND PRODUCT_CODE ='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _	
        ''            & " ORDER BY SERIAL_NO "	

        mRunDate = IIf(Trim(txtPlanDate.Text) = "", VB6.Format(PubCurrDate, "DD/MM/YYYY"), VB6.Format(txtPlanDate.Text, "DD/MM/YYYY"))

        mFinalDept = GetProductFinalDept((txtProductCode.Text), mRunDate)
        mFinalDeptCheck = False

        Call InsertTempTable((txtProductCode.Text))
        SqlStr = " SELECT DISTINCT FG_CODE, DEPT_CODE " & vbCrLf _
            & " FROM TEMP_DESPVSISSUE " & vbCrLf _
            & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " AND DEPT_CODE <>'J/W'"

        SqlStr = SqlStr & vbCrLf _
            & " UNION " & vbCrLf _
            & " SELECT PRODUCT_CODE, DEPT_CODE " & vbCrLf _
            & " FROM PRD_PRODSEQUENCE_DET " & vbCrLf _
            & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND PRODUCT_CODE ='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
            & " AND WEF = (SELECT MAX(WEF) FROM PRD_PRODSEQUENCE_DET " & vbCrLf _
            & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND PRODUCT_CODE ='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGet, ADODB.LockTypeEnum.adLockReadOnly)
        MainClass.ClearGrid(SprdMain)
        ClearSprdHeader()
        With RsGet
            If Not .EOF Then
                FormatSprdMain(-1)
                I = 2
                Do While Not .EOF

                    SprdMain.MaxCols = I + 3

                    SprdMain.Row = FPSpreadADO.CoordConstants.SpreadHeader
                    SprdMain.Col = I
                    SprdMain.Text = Trim(IIf(IsDBNull(.Fields("FG_CODE").Value), "", .Fields("FG_CODE").Value)) ''& "-" & Trim(IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value))
                    mSFCode = Trim(IIf(IsDBNull(.Fields("FG_CODE").Value), "", .Fields("FG_CODE").Value))
                    mDeptCode = Trim(IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value))

                    mProductName = ""
                    If MainClass.ValidateWithMasterTable(mSFCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                        mProductName = MasterNo
                    End If

                    SprdMain.Row = FPSpreadADO.CoordConstants.SpreadHeader + 1  ''Product name
                    SprdMain.Col = I
                    SprdMain.Text = mProductName

                    SprdMain.Row = FPSpreadADO.CoordConstants.SpreadHeader + 2  ''Dept Code
                    SprdMain.Col = I
                    SprdMain.Text = mDeptCode

                    SprdMain.Row = FPSpreadADO.CoordConstants.SpreadHeader + 3  ''Capacity
                    SprdMain.Col = I
                    mLastDate = GetWorkingDays(mRunDate)
                    SprdMain.Text = GetLineCapacityQty(mSFCode, mDeptCode, mRunDate) * mLastDate

                    SprdMain.Row = FPSpreadADO.CoordConstants.SpreadHeader + 4  ''Get Planning Qty
                    SprdMain.Col = I
                    mTotalPlanningQty = GetTotalPlanningQty(mSFCode, mDeptCode, mRunDate)
                    SprdMain.Text = mTotalPlanningQty

                    SprdMain.Row = FPSpreadADO.CoordConstants.SpreadHeader + 5  ''Total
                    SprdMain.Col = I
                    SprdMain.Text = " "

                    'SprdMain.Text = Trim(IIf(IsDBNull(.Fields("FG_CODE").Value), "", .Fields("FG_CODE").Value))

                    If Trim(IIf(IsDBNull(.Fields("FG_CODE").Value), "", .Fields("FG_CODE").Value)) & "-" & Trim(IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)) = Trim(txtProductCode.Text) & "-" & Trim(mFinalDept) Then
                        mFinalDeptCheck = True
                    End If
                    .MoveNext()
                    I = I + 4
                Loop
                GetProdSeq = True
            Else
                MsgBox(" Product Sequence Master Not Made For This Product")
                GetProdSeq = False
            End If
        End With

        If mFinalDeptCheck = False And mFinalDept <> "" Then
            SprdMain.Row = 0
            SprdMain.MaxCols = I + 3

            SprdMain.Col = I
            SprdMain.Text = Trim(txtProductCode.Text) ''& "-" & Trim(mFinalDept)

            mProductName = ""
            If MainClass.ValidateWithMasterTable(Trim(txtProductCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                mProductName = MasterNo
            End If

            SprdMain.Row = FPSpreadADO.CoordConstants.SpreadHeader + 1  ''Product name
            SprdMain.Col = I
            SprdMain.Text = mProductName

            SprdMain.Row = FPSpreadADO.CoordConstants.SpreadHeader + 2  ''Dept Code
            SprdMain.Col = I
            SprdMain.Text = Trim(mFinalDept)

            SprdMain.Row = FPSpreadADO.CoordConstants.SpreadHeader + 3  ''Capacity
            SprdMain.Col = I
            mLastDate = GetWorkingDays(mRunDate)
            SprdMain.Text = GetLineCapacityQty(Trim(txtProductCode.Text), Trim(mFinalDept), mRunDate) * mLastDate

            SprdMain.Row = FPSpreadADO.CoordConstants.SpreadHeader + 4  ''Get Planning Qty
            SprdMain.Col = I
            mTotalPlanningQty = GetTotalPlanningQty(Trim(txtProductCode.Text), Trim(mFinalDept), mRunDate)
            SprdMain.Text = mTotalPlanningQty

            SprdMain.Row = FPSpreadADO.CoordConstants.SpreadHeader + 5  ''Total
            SprdMain.Col = I
            SprdMain.Text = " "

        End If
        FormatSprdMain(-1)
        Exit Function
GetERR:
        GetProdSeq = False
        MsgBox(Err.Description)
    End Function
    Private Function GetTotalPlanningQty(ByVal pProductCode As String, ByVal pDeptCode As String, ByVal pDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mQty As Double
        Dim pFromDate As String
        Dim pToDate As String
        Dim mLastDay As Long

        GetTotalPlanningQty = 0
        mLastDay = MainClass.LastDay(Month(pDate), Year(pDate))

        pFromDate = "01/" & VB6.Format(pDate, "MM/YYYY")

        pToDate = VB6.Format(mLastDay & "/" & VB6.Format(pDate, "MM/YYYY"), "DD/MM/YYYY")

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf _
            & " SUM(IPLAN_QTY) AS IPLAN_QTY" & vbCrLf _
            & " FROM PRD_PRODPLAN_MONTH_DET" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND INHOUSE_CODE = '" & MainClass.AllowSingleQuote(pProductCode) & "'"

        If pDeptCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE = '" & MainClass.AllowSingleQuote(pDeptCode) & "'"
        End If

        If Val(txtPlanNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_PRODPLAN <> " & Val(txtPlanNo.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND SCHLD_DATE>=TO_DATE('" & VB6.Format(pFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SCHLD_DATE<=TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetTotalPlanningQty = IIf(IsDBNull(RsTemp.Fields("IPLAN_QTY").Value), 0, RsTemp.Fields("IPLAN_QTY").Value)
        End If

        Exit Function
ErrPart:
        GetTotalPlanningQty = 0
    End Function
    Private Function InsertTempTable(ByRef xProductCode As String) As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim xItemCode As String
        Dim mLevel As Integer

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " DELETE FROM TEMP_DESPVSISSUE WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        '    SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf _	
        ''            & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _	
        ''            & " '" & xProductcode & "', ID.RM_CODE, IH.PRODUCT_CODE, 0 ,ID.DEPT_CODE, 1 " & vbCrLf _	
        ''            & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID" & vbCrLf _	
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
        ''            & " AND IH.MKEY=ID.MKEY " & vbCrLf _	
        ''            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(xProductcode) & "' AND STATUS='O'"	
        '	
        '    PubDBCn.Execute SqlStr	

        SqlStr = " SELECT " & vbCrLf _
            & " IH.PRODUCT_CODE, ID.RM_CODE, ID.DEPT_CODE " & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(xProductCode) & "' AND IH.STATUS='O'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If RsShow.EOF = False Then
            Do While RsShow.EOF = False
                Call FillGridCol(RsShow, 1, Trim(xProductCode), Trim(xProductCode))
                RsShow.MoveNext()
            Loop
        End If

        PubDBCn.CommitTrans()
        Exit Function
LedgError:
        '    Resume	
        PubDBCn.RollbackTrans()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub FillGridCol(ByRef pRs As ADODB.Recordset, ByRef pLevel As Integer, ByRef pProductCode As String, ByRef pParentCode As String)

        On Error GoTo FillGERR
        Dim SqlStr As String = ""
        Dim mRMCode As String
        Dim mDeptCode As String
        '	
        'Dim mRM_PURCHASE_COST As Double	
        'Dim mRM_LANDED_COST As Double	
        'Dim mRMUOM As String	
        'Dim mFactor As Double	
        '	


        mDeptCode = IIf(IsDBNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)
        mRMCode = IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)

        SqlStr = " INSERT INTO TEMP_DESPVSISSUE ( " & vbCrLf _
            & " USERID, CHILD_CODE, RM_CODE, FG_CODE, STD_QTY, DEPT_CODE, FG_LEVEL ) VALUES (" & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(PubUserID) & "', '" & pProductCode & "', '" & pProductCode & "', '" & pParentCode & "', " & vbCrLf _
            & " 1, '" & mDeptCode & "', " & pLevel & ")"

        PubDBCn.Execute(SqlStr)


        '    Call FillSubAlterRecord(mRMCode, "", pSRNo, pLevel, pProductCode, mDeptCode, pParentCode)	
        Call FillSubRecord(mRMCode, VB6.Format(PubCurrDate, "DD/MM/YYYY"), pLevel, pProductCode)

        Exit Sub
FillGERR:
        '    Resume	
        MsgBox(Err.Description)
    End Sub
    Private Sub FillSubRecord(ByRef pProductCode As String, ByRef pWEF As String, ByRef pLevel As Integer, ByRef pMainProductCode As String)

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String

        pLevel = pLevel + 1
        SqlStr = " SELECT " & vbCrLf _
            & " IH.PRODUCT_CODE, " & vbCrLf _
            & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf _
            & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf _
            & " ID.STD_QTY, ID.GROSS_WT_SCRAP," & vbCrLf _
            & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM "


        SqlStr = SqlStr & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O'" & vbCrLf _
            & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O') "       '& vbCrLf _

        ''& " AND WEF<= '" & vb6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf _

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                Call FillGridCol(RsShow, pLevel, pMainProductCode, pProductCode)
                RsShow.MoveNext()
            Loop
        Else
            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ITEM_CODE AS RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ITEM_QTY AS STD_QTY, ID.SCRAP_QTY AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM "

            SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND IH.STATUS='O' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' AND STATUS='O') "

            '        SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'"	

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                Do While Not RsShow.EOF
                    Call FillGridCol(RsShow, pLevel, pMainProductCode, pProductCode)
                    RsShow.MoveNext()
                Loop
            End If
        End If
        Exit Sub
FillERR:
        MsgBox(Err.Description)
        '    Resume	
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mTotQty As Double
        Dim mString As String
        Dim mProductCode As String
        Dim mDept As String
        Dim mCapacity As Double
        Dim mTotalPlanningQty As Double
        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsMRPMain.EOF = True Then Exit Function

        If Trim(txtProductCode.Text) = "" Then
            MsgInformation("Product Code is empty, So unable to save.")
            txtProductCode.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtPlanDate.Text) = "" Then
            MsgInformation("Plan Date is empty, So unable to save.")
            txtPlanDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        'If PubSuperUser <> "S" Then
        'If Val(txtPlanningQty.Text) > Val(txtCapacity.Text) Then
        '    MsgInformation("Planning Qty Cann't be Greater than Capacity Qty.")
        '    FieldsVarification = False
        '    Exit Function
        'End If
        'End If

        Call CalcTotal()

        With SprdMain
            For cntCol = 2 To .MaxCols
                mCapacity = 0
                mTotalPlanningQty = 0

                .Col = cntCol
                .Row = FPSpreadADO.CoordConstants.SpreadHeader
                mProductCode = Trim(.Text)

                .Row = FPSpreadADO.CoordConstants.SpreadHeader + 3
                mCapacity = Val(.Text)

                .Row = FPSpreadADO.CoordConstants.SpreadHeader + 4
                mTotalPlanningQty = Val(.Text)

                .Row = FPSpreadADO.CoordConstants.SpreadHeader + 5
                mTotalPlanningQty = mTotalPlanningQty + Val(.Text)

                If PubSuperUser <> "S" Then
                    If Val(CStr(mTotalPlanningQty)) > Val(mCapacity) Then
                        MsgInformation("Planning Qty (" & mTotalPlanningQty & ") Cann't be Greater than Capacity Qty (" & mCapacity & ") For Product Code - " & mProductCode & ".")
                        FieldsVarification = False
                        Exit Function
                    End If
                End If

            Next
        End With
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume	
    End Function

    Private Sub frmSOWiseMRP_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsMRPMain.Close()
        RsMRPMain = Nothing
        RsMRPDetail.Close()
        RsMRPDetail = Nothing
        RsMRPMonDetail.Close()
        RsMRPMonDetail = Nothing
        'PvtDBCn.Close	
        'Set PvtDBCn = Nothing	
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        CalcTotal()
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtPlanNo.Text = SprdView.Text
        txtPlanNo_Validating(txtPlanNo, New System.ComponentModel.CancelEventArgs(False))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtPlanningQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPlanningQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPlanDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPlanDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs)
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mLastDate As Long
        If txtPlanDate.Text = "" Then GoTo EventExitSub
        If IsDate(txtPlanDate.Text) = False Then
            MsgBox("Not a valid Date")
            Cancel = True
        Else
            If FYChk((txtPlanDate.Text)) = False Then
                Cancel = True
            Else
                If ShowRecord(False) = False Then Cancel = True
            End If
        End If
        If Trim(txtPlanDate.Text) <> "" Then
            mLastDate = GetWorkingDays(txtPlanDate.Text)  ''MainClass.LastDay(Month(txtPlanDate.Text), Year(txtPlanDate.Text))
            txtCapacity.Text = GetLineCapacityQty(txtProductCode.Text, "", txtPlanDate.Text) * mLastDate
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPlanningQty_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs)
        Dim Cancel As Boolean = eventArgs.Cancel
        'If PubSuperUser <> "S" Then
        'If Val(txtPlanningQty.Text) > Val(txtScheduleQty.Text) Then
        '        MsgInformation("Planning Qty Cann't be Greater than Schedule Qty.")
        '        Cancel = True
        '        GoTo EventExitSub
        '    End If
        'End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPlanNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPlanNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtScheduleQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtScheduleQty.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtScheduleQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtScheduleQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtProductCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProductCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.DoubleClick
        Call cmdSearchProduct_Click(cmdSearchProduct, New System.EventArgs())
    End Sub

    Private Sub txtProductCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProductCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProductCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProductCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProductCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchProduct_Click(cmdSearchProduct, New System.EventArgs())
    End Sub

    Private Sub txtProductCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProductCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mLastDate As Long

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub


        SqlStr = " SELECT ITEM_SHORT_DESC  " & vbCrLf _
            & " FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsTemp.EOF Then


            lblProductCode.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)
            If Trim(txtPlanDate.Text) <> "" Then
                mLastDate = GetWorkingDays(txtPlanDate.Text)  ''MainClass.LastDay(Month(txtPlanDate.Text), Year(txtPlanDate.Text))
                txtCapacity.Text = GetLineCapacityQty(txtProductCode.Text, "", txtPlanDate.Text) * mLastDate
            End If

            If IsShowingRecord = False Then
                If ShowRecord(False) = False Then
                    Cancel = True
                Else
                    If GetProdSeq() = False Then Cancel = True
                End If
            End If
        Else
            MsgBox("Not a valid Customer's Product Code")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPlanNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPlanNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mScheduleQty As Double
        Dim mLastDate As Long

        If Not RsMRPMain.EOF Then
            IsShowingRecord = True
            lblMkey.Text = IIf(IsDBNull(RsMRPMain.Fields("AUTO_KEY_PRODPLAN").Value), "", RsMRPMain.Fields("AUTO_KEY_PRODPLAN").Value)
            txtPlanNo.Text = IIf(IsDBNull(RsMRPMain.Fields("AUTO_KEY_PRODPLAN").Value), "", RsMRPMain.Fields("AUTO_KEY_PRODPLAN").Value)
            txtProductCode.Text = IIf(IsDBNull(RsMRPMain.Fields("PRODUCT_CODE").Value), "", RsMRPMain.Fields("PRODUCT_CODE").Value)
            txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(False))
            txtPlanDate.Text = IIf(IsDBNull(RsMRPMain.Fields("SCHLD_DATE").Value), "", RsMRPMain.Fields("SCHLD_DATE").Value)
            txtPlanningQty.Text = IIf(IsDBNull(RsMRPMain.Fields("CUST_ORD_QTY").Value), "", RsMRPMain.Fields("CUST_ORD_QTY").Value)

            mLastDate = GetWorkingDays(txtPlanDate.Text)  ''MainClass.LastDay(Month(txtPlanDate.Text), Year(txtPlanDate.Text))
            txtCapacity.Text = GetLineCapacityQty(txtProductCode.Text, "", txtPlanDate.Text) * mLastDate

            txtRemarks.Text = IIf(IsDBNull(RsMRPMain.Fields("REMARKS").Value), "", RsMRPMain.Fields("REMARKS").Value)
            chkStatus.CheckState = IIf(IsDBNull(RsMRPMain.Fields("PLAN_STATUS").Value) Or RsMRPMain.Fields("PLAN_STATUS").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            mScheduleQty = GetSchdQty(Trim(txtProductCode.Text), (txtPlanDate.Text))

            txtScheduleQty.Text = VB6.Format(mScheduleQty, "0.00")
            Call ShowDetail1()
            Call MakeEnableDesableField(False)
            IsShowingRecord = False
        End If
        CalcTotal()
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        PopulateMode((False))
        txtPlanNo.Enabled = True
        cmdSearchPlanNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsMRPMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub


    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim mRow As Integer
        Dim mCol As Integer
        Dim mSerialDate As String
        Dim mCurDate As String
        Dim SqlStr As String = ""
        Dim mDeptCode As String
        Dim mCheckDate As String
        Dim mDays As Integer

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM PRD_PRODPLAN_MONTH_DET " & vbCrLf _
            & " WHERE AUTO_KEY_PRODPLAN=" & Val(lblMkey.Text) & " " & vbCrLf _
            & " ORDER BY SERIAL_DATE,DEPT_CODE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRPMonDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMRPMonDetail
            If .EOF = True Then Exit Sub
            If GetProdSeq() = False Then GoTo ERR1
            mRow = 1
            Do While Not .EOF
                SprdMain.MaxRows = mRow
                SprdMain.Row = mRow

                SprdMain.Col = ColDate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("SERIAL_DATE").Value), "", .Fields("SERIAL_DATE").Value), "DD/MM/YYYY")

                mSerialDate = Trim(SprdMain.Text)
                Do While Not .EOF
                    mCurDate = VB6.Format(IIf(IsDBNull(.Fields("SERIAL_DATE").Value), "", .Fields("SERIAL_DATE").Value), "DD/MM/YYYY")
                    If mCurDate <> mSerialDate Then GoTo NextRow

                    For mCol = 2 To SprdMain.MaxCols
                        'SprdMain.Row = 0
                        SprdMain.Row = FPSpreadADO.CoordConstants.SpreadHeader + 2
                        SprdMain.Col = mCol

                        mDeptCode = Trim(SprdMain.Text)      '' Mid(Trim(SprdMain.Text), InStr(Trim(SprdMain.Text), "-") + 1)

                        If mDeptCode = Trim(.Fields("DEPT_CODE").Value) Then
                            SprdMain.Row = mRow

                            SprdMain.Col = mCol
                            SprdMain.Text = IIf(IsDBNull(.Fields("IPLAN_QTY").Value), "", CStr(.Fields("IPLAN_QTY").Value))

                            SprdMain.Col = mCol + 1
                            SprdMain.Text = IIf(IsDBNull(.Fields("DPLAN_QTY").Value), "", CStr(.Fields("DPLAN_QTY").Value))

                            SprdMain.Col = mCol + 2
                            SprdMain.Text = IIf(IsDBNull(.Fields("PROD_LOSS").Value), "", .Fields("PROD_LOSS").Value)

                            SprdMain.Col = mCol + 3
                            SprdMain.Text = IIf(IsDBNull(.Fields("PLAN_START").Value), "", .Fields("PLAN_START").Value)
                        End If

                        mCol = mCol + 3
                    Next
                    .MoveNext()
                Loop
NextRow:
                mRow = mRow + 1
            Loop
        End With
        FormatSprdMain(-1)


        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then	
        '        mCheckDate = DateAdd("d", 1, PubCurrDate)	
        '    Else	
        '        mCheckDate = PubCurrDate	
        '    End If	
        '	
        If RsCompany.Fields("PROD_PALN_LOCK").Value = "Y" Then
            mDays = IIf(IsDBNull(RsCompany.Fields("PROD_PALN_LOCK_DAY").Value), 0, RsCompany.Fields("PROD_PALN_LOCK_DAY").Value)
            mDays = mDays - 1
            mCheckDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, mDays, PubCurrDate))
            If PubSuperUser <> "S" Then
                For mRow = 1 To SprdMain.MaxRows
                    SprdMain.Row = mRow
                    SprdMain.Col = ColDate
                    mSerialDate = Trim(SprdMain.Text)
                    If CDate(mSerialDate) <= CDate(mCheckDate) Then
                        MainClass.ProtectCell(SprdMain, mRow, mRow, ColDate, SprdMain.MaxCols)
                    End If
                Next
            End If
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub

    Private Sub txtPlanNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPlanNo.DoubleClick
        Call cmdSearchPlanNo_Click(cmdSearchPlanNo, New System.EventArgs())
    End Sub

    Private Sub txtPlanNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPlanNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchPlanNo_Click(cmdSearchPlanNo, New System.EventArgs())
    End Sub

    Private Function ShowRecord(ByRef pByKey As Boolean) As Boolean

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mProdPlanNo As Double
        Dim mProdCode As String
        Dim mSchldDate As String
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mScheduleQty As Double

        ShowRecord = True
        If pByKey = True Then
            If Trim(txtPlanNo.Text) = "" Then Exit Function
            mProdPlanNo = Val(txtPlanNo.Text)
        Else
            If Trim(txtProductCode.Text) = "" Then Exit Function
            mProdCode = txtProductCode.Text
            If Trim(txtPlanDate.Text) = "" Then Exit Function
            mSchldDate = txtPlanDate.Text
        End If

        mScheduleQty = GetSchdQty(Trim(txtProductCode.Text), (txtPlanDate.Text))
        txtScheduleQty.Text = VB6.Format(mScheduleQty, "0.00")

        If MODIFYMode = True And RsMRPMain.BOF = False Then xMkey = RsMRPMain.Fields("AUTO_KEY_PRODPLAN").Value

        SqlStr = "SELECT * FROM PRD_PRODPLAN_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_PRODPLAN,LENGTH(AUTO_KEY_PRODPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        If pByKey = True Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_PRODPLAN=" & mProdPlanNo & ""
        Else
            SqlStr = SqlStr & vbCrLf _
                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProdCode) & "'" & vbCrLf _
                & " AND TO_CHAR(SCHLD_DATE,'MMYYYY')='" & VB6.Format(mSchldDate, "MMYYYY") & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRPMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMRPMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                If pByKey = True Then
                    MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Else
                    MsgBox("Schedule not made for these parameter. Click, Add for New", MsgBoxStyle.Information)
                End If
                ShowRecord = False
            ElseIf ADDMode = True Then
                MainClass.ClearGrid(SprdMain)
                For I = 1 To MainClass.LastDay(Month(CDate(mSchldDate)), Year(CDate(mSchldDate)))
                    With SprdMain
                        .MaxRows = I
                        .Row = I
                        .Col = ColDate
                        .Text = VB6.Format(I, "00") & "/" & VB6.Format(Month(CDate(mSchldDate)), "00") & "/" & VB6.Format(Year(CDate(mSchldDate)), "0000")
                    End With
                Next
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PRD_PRODPLAN_HDR " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND SUBSTR(AUTO_KEY_PRODPLAN,LENGTH(AUTO_KEY_PRODPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
                    & " AND AUTO_KEY_PRODPLAN=" & Val(xMkey) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRPMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub txtPlanNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPlanNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPlanNo.Text) = "" Then GoTo EventExitSub
        If Len(Trim(txtPlanNo.Text)) < 6 Then
            txtPlanNo.Text = Trim(txtPlanNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If
        If ShowRecord(True) = False Then Cancel = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtProductCode.Enabled = mMode
        cmdSearchProduct.Enabled = mMode
        txtPlanDate.Enabled = mMode
        '    txtPlanningQty.Enabled = mMode	
        chkStatus.Enabled = False
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub ReportOnProdPlan(ByRef Mode As Crystal.DestinationConstants)

    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnProdPlan(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnProdPlan(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class
