Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamSendMRRMannual
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColFOC As Short = 1
    Private Const ColMRRNo As Short = 2
    Private Const ColMKEY As Short = 3
    Private Const ColMRRDate As Short = 4
    Private Const ColPartyName As Short = 5
    Private Const ColBillNo As Short = 6
    Private Const ColBillAmount As Short = 7
    Private Const ColPostFlag As Short = 8

    Dim mAddMode As Boolean

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        mAddMode = True
        CmdAdd.Enabled = False
        OptSend(0).Checked = True
        Call FormatSprdMain()
        Call ShowStatus(False)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONMRR(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONMRR(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportONMRR(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String


        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = "SEND MRR TO ACCOUNT"
        mSubTitle = "Send Date : " & VB6.Format(txtSendDate.Text, "DD/MM/YYYY")
        Call MainClass.ClearCRptFormulas(Report1)

        SqlStr = MakeSQL
        mRptFileName = "SENDMRR_PRN.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrPart
        Dim CntRow As Integer
        Dim SqlStr As String = ""
        Dim mMKEY As Double
        Dim mMRRNO As Double
        Dim mUpdateCount As Integer
        Dim mFocFlag As String
        Dim mMRRType As String
        Dim mTableName As String
        Dim mMRRDATE As String

        If Not IsDate(txtSendDate.Text) Then
            MsgBox("Invalid Date")
            txtSendDate.Focus()
            Exit Sub
        End If

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow

                .Col = ColMKEY
                mMRRNO = Val(.Text)

                If mMRRNO > 0 Then
                    .Col = ColMRRDate
                    mMRRDATE = Trim(.Text)

                    .Col = ColPostFlag
                    If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                        If CDate(mMRRDATE) > CDate(txtSendDate.Text) Then
                            MsgBox("MRR date is Greater Than Send Date. MRR No. " & mMRRNO)
                            Exit Sub
                        End If
                    End If
                End If
            Next
        End With


        mAddMode = False

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0
        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow

                .Col = ColMKEY
                mMRRNO = Val(.Text)

                .Col = ColMRRDate
                mMRRDATE = Trim(.Text)

                If mMRRNO = 0 Then GoTo NextRec

                .Col = ColFOC
                mFocFlag = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                mMRRType = GetMRRType(mMRRNO)

                .Col = ColPostFlag
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    If mFocFlag = "Y" Then
                        SqlStr = "UPDATE INV_GATE_HDR " & vbCrLf & " SET SEND_AC_DATE=TO_DATE('" & VB6.Format(txtSendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " QC_STATUS='Y', " & vbCrLf & " QC_DATE=TO_DATE('" & VB6.Format(txtSendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " MRR_FINAL_FLAG='" & mFocFlag & "', UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE AUTO_KEY_MRR=" & mMRRNO & "" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                        PubDBCn.Execute(SqlStr)

                        If mMRRType = "J" Or mMRRType = "1" Then
                            SqlStr = " UPDATE INV_GATE_DET SET STOCK_TYPE='CS', MRR_QCDATE=TO_DATE('" & VB6.Format(mMRRDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE AUTO_KEY_MRR=" & mMRRNO & "" & vbCrLf & " AND STOCK_TYPE='QC'"

                            PubDBCn.Execute(SqlStr)

                            mTableName = ConInventoryTable

                            SqlStr = " UPDATE " & mTableName & " SET STOCK_TYPE='CS' " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO=" & mMRRNO & " " & vbCrLf & " AND REF_TYPE='MRR' AND STOCK_TYPE='QC'"

                            PubDBCn.Execute(SqlStr)

                        Else
                            SqlStr = " UPDATE INV_GATE_DET SET STOCK_TYPE='ST', MRR_QCDATE=TO_DATE('" & VB6.Format(mMRRDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE AUTO_KEY_MRR=" & mMRRNO & "" & vbCrLf & " AND STOCK_TYPE='QC'"

                            PubDBCn.Execute(SqlStr)

                            mTableName = ConInventoryTable

                            SqlStr = " UPDATE " & mTableName & " SET STOCK_TYPE='ST' " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO=" & mMRRNO & " " & vbCrLf & " AND REF_TYPE='MRR' AND STOCK_TYPE='QC'"

                            PubDBCn.Execute(SqlStr)

                        End If

                    Else
                        SqlStr = "UPDATE INV_GATE_HDR " & vbCrLf & " SET SEND_AC_DATE=TO_DATE('" & VB6.Format(txtSendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " MRR_FINAL_FLAG='" & mFocFlag & "', UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE AUTO_KEY_MRR=" & mMRRNO & "" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                        PubDBCn.Execute(SqlStr)

                    End If


                    mUpdateCount = mUpdateCount + 1
                End If
NextRec:
            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " MRR Send.", MsgBoxStyle.Information)
        Call ShowStatus(True)
        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
    End Sub

    Private Function GetMRRType(ByRef pMRRNo As Double) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetMRRType = ""
        SqlStr = "SELECT REF_TYPE FROM INV_GATE_HDR " & vbCrLf & " WHERE AUTO_KEY_MRR=" & pMRRNo & "" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            GetMRRType = IIf(IsDbNull(RsTemp.Fields("REF_TYPE").Value), "", RsTemp.Fields("REF_TYPE").Value)
        End If
        Exit Function
ErrPart:
        GetMRRType = ""
    End Function
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        OptSelection(1).Checked = True
        Show1()

        FormatSprdMain()

        Call ShowStatus(IIf(OptSend(2).Checked = True, True, False))
        '    Call ShowStatus(False)
    End Sub
    Private Sub ShowStatus(ByRef pPrintEnable As Object)
        cmdShow.Enabled = pPrintEnable
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
        cmdSave.Enabled = Not pPrintEnable
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.SetFocus: Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmParamSendMRRMannual_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamSendMRRMannual_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        txtSendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        mAddMode = False
        FormatSprdMain()
        cmdShow.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = MakeSQL

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub

    Private Function MakeSQL() As String
        On Error GoTo ErrPart

        MakeSQL = "SELECT CASE WHEN MRR_FINAL_FLAG='Y' THEN '1' ELSE '0' END AS PrintStatus,IH.AUTO_KEY_MRR, IH.AUTO_KEY_MRR,IH.MRR_DATE,  " & vbCrLf _
            & " CMST.SUPP_CUST_NAME, BILL_NO, INVOICE_AMT, CASE WHEN SEND_AC_DATE IS NOT NULL THEN '1' ELSE '0' END AS PostStatus " & vbCrLf _
            & " FROM INV_GATE_HDR IH,FIN_SUPP_CUST_MST CMST" & vbCrLf _
            & " WHERE IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.Company_Code=CMST.Company_Code " & vbCrLf _
            & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        If OptSend(2).Checked = False Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.QC_STATUS='Y' " & vbCrLf & " AND IH.MRR_FINAL_FLAG='N' " & vbCrLf & " AND IH.SEND_AC_FLAG='N' "
        End If

        If OptSend(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND (SEND_AC_DATE='' OR SEND_AC_DATE IS NULL)"
        ElseIf OptSend(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND SEND_AC_DATE IS NOT NULL"
        Else
            MakeSQL = MakeSQL & vbCrLf & " AND SEND_AC_DATE=TO_DATE('" & VB6.Format(txtSendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND IH.MRR_DATE<=TO_DATE('" & VB6.Format(txtSendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY IH.MRR_DATE,IH.AUTO_KEY_MRR"


        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function
    Private Sub FormatSprdMain()

        With SprdMain
            .MaxCols = ColPostFlag
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMKEY, 11)
            '    .ColHidden = True

            .Col = ColMRRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditMultiLine = False
            .set_ColWidth(ColMRRNo, 9)

            .Col = ColMRRDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMRRDate, 9)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 33)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColBillNo, 12)

            .Col = ColBillAmount
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillAmount, 10)

            .Col = ColPostFlag
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditMultiLine = False
            .set_ColWidth(ColFOC, 8)
            .Value = CStr(System.Windows.Forms.CheckState.Checked)

            .Col = ColFOC
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditMultiLine = False
            .set_ColWidth(ColFOC, 8)
            '    .Value = vbUnchecked

            MainClass.SetSpreadColor(SprdMain, -1)
            If mAddMode = True Then
                MainClass.UnProtectCell(SprdMain, 1, .MaxRows, 1, 1)
                MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColMKEY, ColBillAmount)
            Else
                MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColBillAmount)
            End If

            '    SprdMain.OperationMode = OperationModeSingle
            '    SprdMain.DAutoCellTypes = True
            '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            '    SprdMain.GridColor = &HC00000
        End With
        WriteColHeadings()
    End Sub
    Private Sub WriteColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColMKEY
            .Text = "MKey"

            .Col = ColMRRNo
            .Text = "MRR No."

            .Col = ColMRRDate
            .Text = "MRR Date"

            .Col = ColPartyName
            .Text = "Supplier Name"

            .Col = ColBillNo
            .Text = "Bill No"

            .Col = ColBillAmount
            .Text = "Bill Amount"

            .Col = ColFOC
            .Text = "FOC Status"

            .Col = ColPostFlag
            .Text = "Send Status"
        End With
    End Sub
    Private Sub frmParamSendMRRMannual_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim CntRow As Integer
            Call ShowStatus(True)
            With SprdMain
                For CntRow = 1 To .MaxRows
                    .Row = CntRow
                    .Col = ColFOC
                    .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Next
            End With
        End If
    End Sub

    Private Sub OptSend_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSend.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSend.GetIndex(eventSender)

            MainClass.ClearGrid(SprdMain, RowHeight)
            Call ShowStatus(True)
        End If
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""


        If mAddMode = False Then Exit Sub

        If eventArgs.Row = 0 And eventArgs.Col = ColMRRNo And SprdMain.Enabled = True Then
            With SprdMain
                eventArgs.Row = .ActiveRow
                eventArgs.Col = ColMRRNo
                '            SqlStr = " SELECT AUTO_KEY_MRR, MRR_DATE " & vbCrLf _
                ''                    & " FROM INV_GATE_HDR " & vbCrLf _
                '
                SqlStr = MakeSQL
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    eventArgs.Row = .ActiveRow
                    eventArgs.Col = ColMRRNo
                    .Text = Trim(AcName)
                    '                .Col = ColMRRDate
                    '                .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColMRRNo)
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (mAddMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColMRRNo)
            '        MainClass.SaveStatus Me, mAddMode, mAddMode
        End If
    End Sub



    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColMRRNo Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColMRRNo, 0))
        SprdMain.Refresh()
    End Sub


    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xMRRNo As Double
        Dim xMkey As Double
        Dim xIsFOC As Boolean

        If eventArgs.NewRow = -1 Then Exit Sub

        Select Case eventArgs.Col
            Case ColFOC
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColFOC
                xIsFOC = IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), True, False)

                SprdMain.Col = ColMRRNo
                xMRRNo = Val(SprdMain.Text)
                If Val(CStr(xMRRNo)) = 0 Then Exit Sub

                If Len("" & xMRRNo & "") < 6 Then
                    xMRRNo = CDbl(xMRRNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
                Else
                    xMRRNo = xMRRNo
                End If

                If GetValidMRR1(xMRRNo, xIsFOC) = False Then
                    MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColFOC)
                End If
            Case ColMRRNo
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColFOC
                xIsFOC = IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), True, False)

                SprdMain.Col = ColMRRNo
                xMRRNo = Val(SprdMain.Text)
                If Val(CStr(xMRRNo)) = 0 Then Exit Sub

                If Len("" & xMRRNo & "") < 6 Then
                    xMkey = CDbl(xMRRNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
                    SprdMain.Col = ColMKEY
                    SprdMain.Text = CStr(xMkey)
                Else
                    SprdMain.Col = ColMKEY
                    SprdMain.Text = CStr(xMRRNo)
                    xMkey = xMRRNo
                End If


                If GetValidMRR1(xMkey, xIsFOC) = True Then
                    If CheckDuplicateMRR(xMkey) = False Then
                        If FillGridRow(xMkey) = False Then Exit Sub
                        MainClass.AddBlankSprdRow(SprdMain, ColMRRNo, RowHeight)
                        Call FormatSprdMain()
                    Else
                        MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColMRRNo)
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColMRRNo)
                End If
        End Select
CalcPart:
        Exit Sub
ErrPart:
        'Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function FillGridRow(ByRef xMRRNo As Double) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If Val(CStr(xMRRNo)) = 0 Then Exit Function

        SqlStr = ""
        SqlStr = " Select IH.MRR_DATE, CMST.SUPP_CUST_NAME, BILL_NO, INVOICE_AMT " & vbCrLf _
            & " FROM INV_GATE_HDR IH, FIN_SUPP_CUST_MST CMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.AUTO_KEY_MRR=" & xMRRNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc
                SprdMain.Col = ColMRRDate
                SprdMain.Text = IIf(IsDbNull(.Fields("MRR_DATE").Value), "", .Fields("MRR_DATE").Value)

                SprdMain.Col = ColPartyName
                SprdMain.Text = IIf(IsDbNull(.Fields("SUPP_CUST_NAME").Value), "", .Fields("SUPP_CUST_NAME").Value)

                SprdMain.Col = ColBillNo
                SprdMain.Text = IIf(IsDBNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)

                SprdMain.Col = ColBillAmount
                SprdMain.Text = IIf(IsDBNull(.Fields("INVOICE_AMT").Value), "", .Fields("INVOICE_AMT").Value)

            End With
            FillGridRow = True
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function

    Private Function CheckDuplicateMRR(ByRef mMRRNO As Double) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

        If Val(CStr(mMRRNO)) = 0 Then CheckDuplicateMRR = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColMKEY
                If UCase(.Text) = UCase(CStr(mMRRNO)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateMRR = True
                        MsgInformation("Duplicate MRR No.")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColMRRNo)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function GetValidMRR1(ByRef pMRRNo As Double, ByRef mISFOC As Boolean) As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        mSqlStr = "SELECT IH.REF_TYPE, IH.AUTO_KEY_MRR, IH.AUTO_KEY_MRR,IH.MRR_DATE,EXCISE_STATUS,SALETAX_STATUS,SERV_STATUS " & vbCrLf & " FROM INV_GATE_HDR IH" & vbCrLf & " WHERE " & vbCrLf & " IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_MRR=" & pMRRNo & "" & vbCrLf & " AND IH.MRR_FINAL_FLAG='N' " & vbCrLf & " AND IH.SEND_AC_FLAG='N' " & vbCrLf & " AND (SEND_AC_DATE='' OR SEND_AC_DATE IS NULL)"

        If mISFOC = False Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.QC_STATUS='Y' "
        End If

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If mISFOC = True Then
                If RsTemp.Fields("REF_TYPE").Value = "P" Then
                    MsgInformation("Against Purchase Order Ref. MRR Cann't be FOC.")
                    GetValidMRR1 = False
                    Exit Function
                End If

                If RsTemp.Fields("EXCISE_STATUS").Value = "Y" Then
                    MsgInformation("Excise Cenvat taken Against This MRR")
                    GetValidMRR1 = False
                    Exit Function
                End If
                If RsTemp.Fields("SALETAX_STATUS").Value = "Y" Then
                    MsgInformation("Sales Tax Refund taken Against This MRR")
                    GetValidMRR1 = False
                    Exit Function
                End If
                If RsTemp.Fields("SERV_STATUS").Value = "Y" Then
                    MsgInformation("Service Tax Refund taken Against This MRR")
                    GetValidMRR1 = False
                    Exit Function
                End If
                GetValidMRR1 = True
            Else
                GetValidMRR1 = True
            End If
        Else
            If mISFOC = True Then
                MsgInformation("Already Send to Account")
            Else
                MsgInformation("QC Not Done")
            End If
            GetValidMRR1 = False
        End If

        Exit Function
ErrPart:
        GetValidMRR1 = False
    End Function

    Private Function GetValidRGPPurpose(ByRef pMRRNo As Double) As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPurpose As String

        GetValidRGPPurpose = True
        mSqlStr = "SELECT DISTINCT GH.PURPOSE " & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, INV_GATEPASS_HDR GH" & vbCrLf & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR " & vbCrLf & " AND ID.Company_Code=GH.Company_Code " & vbCrLf & " AND ID.REF_AUTO_KEY_NO=GH.AUTO_KEY_PASSNO " & vbCrLf & " AND IH.AUTO_KEY_MRR=" & pMRRNo & ""

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mPurpose = IIf(IsDbNull(RsTemp.Fields("PURPOSE").Value), "", RsTemp.Fields("PURPOSE").Value)
                If mPurpose = "D" Or mPurpose = "F" Or mPurpose = "G" Or mPurpose = "H" Then
                    GetValidRGPPurpose = False
                    Exit Function
                End If
                RsTemp.MoveNext()
            Loop
        End If

        Exit Function
ErrPart:
        GetValidRGPPurpose = True
    End Function
    Private Sub txtSendDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSendDate.TextChanged
        Call ShowStatus(True)
    End Sub

    Private Sub txtSendDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSendDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtSendDate.Text = "" Then GoTo EventExitSub
        If Not IsDate(txtSendDate.Text) Then
            MsgBox("Invalid Date")
            Cancel = True
            '    ElseIf FYChk(txtSendDate.Text) = False Then
            '        Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
