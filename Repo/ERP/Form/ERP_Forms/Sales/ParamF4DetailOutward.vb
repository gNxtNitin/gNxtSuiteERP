Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class frmParamF4DetailOutward
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 12
    ''Private PvtDBCn As ADODB.Connection

    Dim mPartyC4 As String
    Private Const ColLocked As Short = 1
    Private Const ColPartyC4No As Short = 2
    Private Const ColChallanNo As Short = 3
    Private Const ColPartyC4Date As Short = 4
    Private Const ColPartyName As Short = 5
    Private Const ColMTRLCode As Short = 6
    Private Const ColMtrlName As Short = 7
    Private Const ColOpening As Short = 8
    Private Const ColIssued As Short = 9
    Private Const ColRecd As Short = 10
    Private Const ColBillNo As Short = 11
    Private Const ColBillDate As Short = 12
    Private Const ColItemName As Short = 13
    Private Const ColBalQty As Short = 14
    Private Const ColExpDate As Short = 15
    Private Const ColRate As Short = 16
    Private Const ColBalValue As Short = 17
    Private Const ColDutyForgone As Short = 18

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim mClickProcess As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboShow_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtC4No.Enabled = False
            cmdSearch.Enabled = False
        Else
            TxtC4No.Enabled = True
            cmdSearch.Enabled = True
        End If
    End Sub

    Private Sub chkEmp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkEmp.CheckStateChanged
        Call PrintStatus(False)
        If chkEmp.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtName.Enabled = False
            cmdEmp.Enabled = False
        Else
            txtName.Enabled = True
            cmdEmp.Enabled = True
        End If
    End Sub

    Private Sub chkItemAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkItemAll.CheckStateChanged
        Call PrintStatus(False)
        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItemDesc.Enabled = False
            cmdItemDesc.Enabled = False
        Else
            txtItemDesc.Enabled = True
            cmdItemDesc.Enabled = True
        End If
    End Sub

    Private Sub ChkPartyAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkPartyAll.CheckStateChanged
        Call PrintStatus(False)
        If ChkPartyAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtPartyName.Enabled = False
            cmdPartySearch.Enabled = False
        Else
            txtPartyName.Enabled = True
            cmdPartySearch.Enabled = True
        End If
    End Sub


    Private Sub chkPrepareBy_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPrepareBy.CheckStateChanged
        Call PrintStatus(False)
        If chkPrepareBy.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtPrepareBy.Enabled = False
            cmdPrepareBy.Enabled = False
        Else
            txtPrepareBy.Enabled = True
            cmdPrepareBy.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub cmdEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEmp.Click
        SearchEmp()
    End Sub

    Private Sub cmdItemDesc_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdItemDesc.Click
        SearchItem()
    End Sub

    Private Sub cmdPartySearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPartySearch.Click
        SearchParty()
    End Sub


    Private Sub cmdPrepareBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrepareBy.Click
        SearchPrepareBy()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '    If mDOSPRINTING = True Then
        '        Screen.MousePointer = 0
        '        Exit Sub
        '        Call DOSC4Report("V")
        '    Else
        ReportonC4(Crystal.DestinationConstants.crptToWindow)
        '    End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '    If mDOSPRINTING = True Then
        '        Call DOSC4Report("P")
        '    Else
        ReportonC4(Crystal.DestinationConstants.crptToPrinter)
        '    End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonC4(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""

        SqlStr = ""

        If InsertPrintDummy = False Then GoTo ReportErr

        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        Report1.Reset()

        mSubTitle = "FROM : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " TO : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        If optShow(0).Checked Then
            mTitle = "Outward C4 Details"
            If chkList.CheckState = System.Windows.Forms.CheckState.Checked Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\C4OutwardList.RPT"
            Else
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\C4Outward.RPT"
            End If

        Else
            mTitle = "Outward C4 Summary"
            If chkList.CheckState = System.Windows.Forms.CheckState.Checked Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\C4OutwardSummList.RPT"
            Else
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\C4OutwardSumm.RPT"
            End If

        End If

        If ChkPartyAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTitle = mTitle & "( Party : " & txtPartyName.Text & ")"
        End If

        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTitle = mTitle & "( Item : " & txtItemDesc.Text & ")"
        End If

        If cboDivision.Text <> "ALL" Then
            mSubTitle = mSubTitle & " ( Division : " & cboDivision.Text & ")"
        End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = "SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function

    Private Function InsertPrintDummy() As Boolean


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mLocked As String
        Dim mPartyC4No As String
        Dim mPartyC4Date As String
        Dim mMTRLCode As String
        Dim mMtrlName As String
        Dim mRecd As String
        Dim mIssued As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mItemName As String
        Dim mBalQty As String
        Dim mPartyName As String
        Dim mExpDate As String
        Dim mRate As Double
        Dim mBalValue As Double
        Dim mDutyForgone As Double
        Dim mChallanNo As String
        Dim mOpeningBal As Double

        'Dim PvtDBCn As ADODB.Connection

        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColPartyC4No
                mPartyC4No = .Text

                .Col = ColChallanNo
                mChallanNo = .Text

                .Col = ColPartyC4Date
                mPartyC4Date = .Text

                .Col = ColMTRLCode
                mMTRLCode = .Text

                .Col = ColPartyName
                mPartyName = Replace(.Text, "'", "''")

                .Col = ColMtrlName
                mMtrlName = Replace(.Text, "'", "''")

                .Col = ColBillNo
                mBillNo = .Text

                .Col = ColBillDate
                mBillDate = .Text

                .Col = ColItemName
                mItemName = Replace(.Text, "'", "''")

                .Col = ColOpening
                mOpeningBal = Val(.Text)

                .Col = ColIssued
                mIssued = .Text

                .Col = ColRecd
                mRecd = .Text

                .Col = ColBalQty
                mBalQty = .Text

                .Col = ColExpDate
                mExpDate = .Text

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColBalValue
                mBalValue = Val(.Text)

                .Col = ColDutyForgone
                mDutyForgone = Val(.Text)

                SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow, " & vbCrLf _
                    & " Field1,Field2,Field3,Field4,Field5," & vbCrLf _
                    & " Field6,Field7,Field8,Field9,Field10,Field11, Field12,Field13, Field14,Field15, Field16) Values (" & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " " & cntRow & ", " & vbCrLf _
                    & " '" & mPartyC4No & "', " & vbCrLf _
                    & " TO_DATE('" & mPartyC4Date & "','DD/MM/YYYY'), " & vbCrLf _
                    & " '" & mMTRLCode & "', " & vbCrLf _
                    & " '" & mMtrlName & "', " & vbCrLf _
                    & " '" & mRecd & "', " & vbCrLf _
                    & " '" & mIssued & "', " & vbCrLf _
                    & " '" & mBillNo & "', " & vbCrLf _
                    & " TO_DATE('" & mBillDate & "','DD/MM/YYYY'),'" & mItemName & "','" & mBalQty & "','" & mPartyName & "', " & vbCrLf _
                    & " TO_DATE('" & mExpDate & "','DD/MM/YYYY'),'" & mRate & "', '" & mBalValue & "','" & mDutyForgone & "','" & mChallanNo & "') "

                PubDBCn.Execute(SqlStr)
NextRow:
            Next
        End With
        PubDBCn.CommitTrans()
        InsertPrintDummy = True
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        InsertPrintDummy = False
        PubDBCn.RollbackTrans()
    End Function

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchC4()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)
        If optShow(0).Checked = True Then
            CalcSprdTotal()  ''TEmp Sandeep
        Else
            If chkValue.CheckState = System.Windows.Forms.CheckState.Checked Then
                CalcRate()
            End If
        End If

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CalcRate()
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mUOM As String = ""
        Dim mRate As Double
        Dim mBalQty As Double
        Dim mValue As Double
        Dim mAsOn As String
        Dim mRefNo As String
        Dim mRefDate As String

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColPartyC4No
                mRefNo = Trim(.Text)

                .Col = ColPartyC4Date
                mRefDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColMTRLCode
                mItemCode = Trim(.Text)

                .Col = ColPartyC4Date
                mAsOn = VB6.Format(.Text, "DD/MM/YYYY")

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mUOM = MasterNo
                End If


                .Col = ColBalQty
                mBalQty = Val(.Text)

                'If mBalQty > 0 Then
                '    mValue = GetLatestItemCostFromMRR(mItemCode, mUOM, mBalQty, (txtAsOn.Text), "P")
                '    '                mRate = GetLatestItemCostForOW(mItemCode, mUOM, mAsOn)
                '    mRate = mValue / mBalQty
                '    If mRate = 0 Then
                '        mRate = GetRateFromRGP((lblBookType.Text), mItemCode, mRefNo, mRefDate)
                '        mValue = mRate * mBalQty
                '    End If
                'Else
                '    mValue = 0
                '    mRate = 0
                'End If
                '            mValue = VB6.Format(mRate * mBalQty, "0.00")

                .Row = cntRow
                .Col = ColRate
                mRate = VB6.Format(.Text, "0.00")

                mValue = VB6.Format(mRate * mBalQty, "0.00")

                .Col = ColBalValue
                .Text = VB6.Format(mValue, "0.00")


            Next
        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function GetRateFromRGP(ByRef pBookType As String, ByRef pItemCode As String, ByRef pRefNo As String, ByRef pRefDate As String) As Double

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        mSqlStr = "SELECT ITEM_RATE FROM INV_GATEPASS_HDR IH,  INV_GATEPASS_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_PASSNO=ID.AUTO_KEY_PASSNO"

        If pBookType = "OW" Then
            mSqlStr = mSqlStr & vbCrLf & " AND  IH.OUTWARD_57F4NO='" & pRefNo & "'" & vbCrLf & " AND IH.GATEPASS_DATE=TO_DATE('" & VB6.Format(pRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        Else
            mSqlStr = mSqlStr & vbCrLf & " AND  IH.AUTO_KEY_PASSNO='" & pRefNo & "'" & vbCrLf & " AND IH.GATEPASS_DATE=TO_DATE('" & VB6.Format(pRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
        End If


        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            GetRateFromRGP = IIf(IsDbNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)
        Else
            GetRateFromRGP = 0
        End If
        Exit Function
ErrPart:
        GetRateFromRGP = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub frmParamF4DetailOutward_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblBookType.Text = "OW" Then
            Me.Text = " F4 Details (Outward)"
        Else
            Me.Text = "Pending RGP Register"
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamF4DetailOutward_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        txtDateFrom.Text = VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
        txtAsOn.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtC4No.Enabled = False
        cmdSearch.Enabled = False

        cboShow.Items.Clear()
        cboShow.Items.Add("BOTH")
        cboShow.Items.Add("COMPLETE")
        cboShow.Items.Add("PENDING")
        cboShow.Items.Add("PENDING AFTER EXP. DATE")
        cboShow.Items.Add("PENDING AFTER ONE YEAR")
        cboShow.SelectedIndex = 0

        Call FillCategory()
        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillCategory()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer

        lstCategory.Items.Clear()
        SqlStr = "SELECT GEN_DESC FROM INV_GENERAL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GEN_TYPE='C' ORDER BY GEN_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        'If RS.EOF = False Then
        '    Do While RS.EOF = False
        '        lstCategory.Items.Add(RS.Fields("GEN_DESC").Value)
        '        lstCategory.SetItemChecked(CntLst, True)
        '        RS.MoveNext()
        '        CntLst = CntLst + 1
        '    Loop
        'End If

        If RS.EOF = False Then
            lstCategory.Items.Add("ALL")
            lstCategory.SetItemChecked(CntLst, True)
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstCategory.Items.Add(RS.Fields("GEN_DESC").Value)
                lstCategory.SetItemChecked(CntLst, True)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCategory.SelectedIndex = 0


        lstPurpose.Items.Clear()
        lstPurpose.Items.Add("A : None")
        lstPurpose.Items.Add("B : Jobwork")
        lstPurpose.Items.Add("C : Repair / Refill / Work Order")
        lstPurpose.Items.Add("D : Tool Trial")
        lstPurpose.Items.Add("E : Preparation of Tool/Die/Jigs/Fixture")
        lstPurpose.Items.Add("F : Testing / Trial")
        lstPurpose.Items.Add("G : Trolley / Bins")
        lstPurpose.Items.Add("H : FOC - Under Warranty / Re-Repair")
        lstPurpose.Items.Add("I : Fitting into any M/c coming to the company")
        lstPurpose.SelectedIndex = 0

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDivision.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub lstCategory_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstCategory.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstCategory.GetItemChecked(0) = True Then
                    For I = 1 To lstCategory.Items.Count - 1
                        lstCategory.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstCategory.Items.Count - 1
                        lstCategory.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstCategory.GetItemChecked(e.Index - 1) = False Then
                    lstCategory.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmParamF4DetailOutward_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamF4DetailOutward_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub



    Private Sub optOrder_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optOrder.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optOrder.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
    End Sub

    Private Sub optShow_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optShow.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optShow.GetIndex(eventSender)
            Call PrintStatus(False)
            chkValue.Enabled = IIf(Index = 0, False, True)
            If Index = 0 Then
                chkValue.CheckState = System.Windows.Forms.CheckState.Unchecked
            End If
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub


    Private Sub txtAsOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAsOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Not IsDate(txtAsOn.Text) Then
            MsgInformation("Invalid date")
            Cancel = True
        End If
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtC4No_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtC4No.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtC4No_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtC4No.DoubleClick
        SearchC4()
    End Sub
    Private Sub SearchC4()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If OptShowNo(0).Checked = True Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND OUTWARD_57F4NO>0 "
            MainClass.SearchGridMaster(TxtC4No.Text, "INV_GATEPASS_HDR", "OUTWARD_57F4NO", "GATEPASS_DATE", "CHALLAN_PREFIX || GATEPASS_NO", , SqlStr)
            If AcName <> "" Then
                TxtC4No.Text = AcName
            End If
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            MainClass.SearchGridMaster(TxtC4No.Text, "INV_GATEPASS_HDR", "AUTO_KEY_PASSNO", "GATEPASS_DATE", "CHALLAN_PREFIX || GATEPASS_NO", , SqlStr)
            If AcName <> "" Then
                TxtC4No.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtItemDesc.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            txtItemDesc.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchEmp()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtName.Text, "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr)
        If AcName <> "" Then
            txtName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchPrepareBy()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtPrepareBy.Text, "ATH_PASSWORD_MST", "USER_ID", "EMP_NAME", , , SqlStr)
        If AcName <> "" Then
            txtPrepareBy.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchParty()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtPartyName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            txtPartyName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub TxtC4No_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtC4No.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtC4No.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtC4No_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtC4No.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchC4()
    End Sub
    Private Sub TxtC4No_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtC4No.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If TxtC4No.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If OptShowNo(0).Checked = True Then
            If MainClass.ValidateWithMasterTable((TxtC4No.Text), "OUTWARD_57F4NO", "OUTWARD_57F4NO", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , SqlStr) = False Then
                MsgInformation("No Such C4.")
                Cancel = True
            End If
        Else
            If MainClass.ValidateWithMasterTable((TxtC4No.Text), "AUTO_KEY_PASSNO", "OUTWARD_57F4NO", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , SqlStr) = False Then
                MsgInformation("No Such RGP No.")
                Cancel = True
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        'Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColDutyForgone
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 10)
            .ColHidden = IIf(lblBookType.Text = "OW", True, IIf(optShow(2).Checked = True, True, False))

            .Col = ColPartyC4No
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyC4No, 10)
            .ColHidden = IIf(optShow(2).Checked = True, True, False)

            .Col = ColChallanNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColChallanNo, 10)
            .ColHidden = IIf(optShow(2).Checked = True, True, False)

            .Col = ColPartyC4Date
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyC4Date, 8)
            .ColHidden = IIf(optShow(2).Checked = True, True, False)

            .Col = ColPartyName
            '.CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 35)

            .Col = ColMTRLCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMTRLCode, 10)

            .Col = ColMtrlName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMtrlName, 35)

            .Col = ColOpening
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColOpening, 11)
            If optShow(2).Checked Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColIssued
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColIssued, 11)

            .Col = ColRecd
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColRecd, 11)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 11)
            If optShow(0).Checked Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 8)
            If optShow(0).Checked Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 25)
            If optShow(0).Checked Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColBalQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColBalQty, 11)
            '        If optShow(0) Then
            '            .ColHidden = True
            '        Else
            '            .ColHidden = False
            '        End If

            .Col = ColExpDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColExpDate, 8)
            .ColHidden = IIf(optShow(2).Checked = True, True, False)

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColRate, 8)
            If optShow(0).Checked Then
                .ColHidden = True
            Else
                .ColHidden = IIf(chkValue.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
            End If


            .Col = ColBalValue
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColBalValue, 11)
            If optShow(0).Checked Then
                .ColHidden = True
            Else
                .ColHidden = IIf(chkValue.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
            End If

            .Col = ColDutyForgone
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColDutyForgone, 8)
            'If optShow(0).Checked Then
            .ColHidden = True
            'Else
            '    .ColHidden = IIf(chkValue.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
            'End If

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim SqlStr As String = ""
        Dim SqlStr1 As String
        Dim SqlStr2 As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If optShow(0).Checked Then
            SqlStr = MakeSQL
        Else
            SqlStr = MakeSQLSumm
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mSuppCode As String
        Dim xAlterItemCode As String

        Dim mCategoryCode As String = ""
        Dim mCategoryStr As String = ""
        Dim mPurpose As String
        Dim mAllPurpose As String

        Dim CntLst As Integer
        Dim mCategory As String
        Dim mAllTrnType As Boolean
        Dim mDivision As Double
        Dim mEmpCode As String
        Dim mPrepareBy As String
        Dim mRGPDateFrom As String

        ''SELECT CLAUSE...

        If lblBookType.Text = "OW" Then
            MakeSQL = " SELECT '',F4NO, CHALLAN_PREFIX || GATEPASS_NO AS CHALLANO, TO_CHAR(F4DATE,'DD/MM/YYYY') AS RGP_DATE ,CMST.SUPP_CUST_NAME, "
        Else
            If OptWise(0).Checked = True Then
                MakeSQL = " SELECT GETRGPDEPT(TRN.COMPANY_CODE, RGP_NO, TRN.RGP_DATE) AS DEPT,RGP_NO, CHALLAN_PREFIX || GATEPASS_NO AS CHALLANO,TO_CHAR(TRN.RGP_DATE,'DD/MM/YYYY') AS RGP_DATE ,CMST.SUPP_CUST_NAME, "
            ElseIf OptWise(1).Checked = True Then
                MakeSQL = " SELECT GETRGPEMP(TRN.COMPANY_CODE, RGP_NO, TRN.RGP_DATE) AS DEPT,RGP_NO, CHALLAN_PREFIX || GATEPASS_NO AS CHALLANO,TO_CHAR(TRN.RGP_DATE,'DD/MM/YYYY') AS RGP_DATE ,CMST.SUPP_CUST_NAME, "
            Else
                MakeSQL = " SELECT GETRGPEMPWithPREID (TRN.COMPANY_CODE, RGP_NO, TRN.RGP_DATE) AS DEPT,RGP_NO, CHALLAN_PREFIX || GATEPASS_NO AS CHALLANO,TO_CHAR(TRN.RGP_DATE,'DD/MM/YYYY') AS RGP_DATE ,CMST.SUPP_CUST_NAME, "
            End If
        End If

        MakeSQL = MakeSQL & vbCrLf _
            & " TRN.OUTWARD_ITEM_CODE, A.ITEM_SHORT_DESC, " & vbCrLf _
            & " 0 AS Opening, " & vbCrLf _
            & " TO_CHAR(DECODE(ITEM_IO,'O',TRN.RGP_QTY,0),'9999999.9999') AS Issued, " & vbCrLf _
            & " TO_CHAR(DECODE(ITEM_IO,'I',TRN.RGP_QTY,0),'9999999.9999') AS Received,  " & vbCrLf _
            & " TRN.BILL_NO, " & vbCrLf & " TO_CHAR(TRN.BILL_DATE,'DD/MM/YYYY'),GETItemName(TRN.COMPANY_CODE,TRN.INWARD_ITEM_CODE), "

        If lblBookType.Text = "OW" Then
            MakeSQL = MakeSQL & vbCrLf & " 0, F4DATE+90,GETRGPRATE (RGP_NO,TRN.OUTWARD_ITEM_CODE,TRN.COMPANY_CODE),0,0 "
        Else
            MakeSQL = MakeSQL & vbCrLf & " 0, TRN.EXP_RTN_DATE,GETRGPRATE (RGP_NO,TRN.OUTWARD_ITEM_CODE,TRN.COMPANY_CODE),0,0 "
        End If

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM INV_GATEPASS_HDR IH, INV_RGP_REG_TRN TRN, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST A "

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MakeSQL = MakeSQL & vbCrLf & " AND IH.AUTO_KEY_PASSNO=TRN.RGP_NO"

        MakeSQL = MakeSQL & vbCrLf & " AND TRN.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND TRN.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND TRN.COMPANY_CODE=A.COMPANY_CODE" & vbCrLf & " AND TRN.OUTWARD_ITEM_CODE=A.ITEM_CODE "

        ''& " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        '
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If OptShowNo(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & "AND TRN.F4NO='" & MainClass.AllowSingleQuote(TxtC4No.Text) & "'"
            Else
                MakeSQL = MakeSQL & vbCrLf & "AND TRN.RGP_NO='" & MainClass.AllowSingleQuote(TxtC4No.Text) & "'"
            End If
        End If

        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemDesc.Text, "Item_Short_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                xAlterItemCode = GetAlterMainItemCode(mItemCode) ' GetAlterItemCode(mItemCode)
                If xAlterItemCode = "" Then
                    MakeSQL = MakeSQL & vbCrLf & "AND TRN.OUTWARD_ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                Else
                    MakeSQL = MakeSQL & vbCrLf & "AND TRN.OUTWARD_ITEM_CODE IN " & xAlterItemCode & ""
                End If
            End If

        End If

        mSuppCode = ""
        If ChkPartyAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND TRN.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'"
            End If
        End If

        mEmpCode = ""
        If chkEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtName.Text, "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmpCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND GETRGPRESPONSIBLEEMP(TRN.COMPANY_CODE, RGP_NO, TRN.RGP_DATE)='" & MainClass.AllowSingleQuote(mEmpCode) & "'"
            End If
        End If

        mPrepareBy = ""
        If chkPrepareBy.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtPrepareBy.Text, "USER_ID", "USER_ID", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPrepareBy = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND GETRGPPREPAREBY(TRN.COMPANY_CODE, RGP_NO, TRN.RGP_DATE)='" & MainClass.AllowSingleQuote(mPrepareBy) & "'"
            End If
        End If

        mAllTrnType = True

        If lstCategory.GetItemChecked(0) = True Then
            mCategoryStr = ""
        Else
            For CntLst = 1 To lstCategory.Items.Count - 1
                If lstCategory.GetItemChecked(CntLst) = True Then
                    mCategory = VB6.GetItemString(lstCategory, CntLst)
                    If MainClass.ValidateWithMasterTable(mCategory, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                        mCategoryCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCategoryStr = IIf(mCategoryStr = "", mCategoryCode, mCategoryStr & "," & mCategoryCode)
                Else
                    mAllTrnType = False
                End If
            Next
        End If
        If mCategoryStr <> "" And mAllTrnType = False Then
            mCategoryStr = "(" & mCategoryStr & ")"
            MakeSQL = MakeSQL & vbCrLf & " AND A.CATEGORY_CODE IN " & mCategoryStr & ""
        End If

        mAllPurpose = CStr(True)
        mPurpose = ""
        For CntLst = 0 To lstPurpose.Items.Count - 1
            If lstPurpose.GetItemChecked(CntLst) = True Then
                mPurpose = "'" & VB.Left(VB6.GetItemString(lstPurpose, CntLst), 1) & "'"
            Else
                mAllPurpose = CStr(False)
            End If
        Next

        If mPurpose <> "" And CBool(mAllPurpose) = False Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.PURPOSE IN (" & mPurpose & ")"
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo

                MakeSQL = MakeSQL & vbCrLf & " AND IH.DIV_CODE=" & mDivision & ""

                '            MakeSQL = MakeSQL & vbCrLf & "AND TRN.RGP_NO IN (SELECT AUTO_KEY_PASSNO FROM " & vbCrLf _
                ''                                        & " INV_GATEPASS_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                ''                                        & " AND GATEPASS_DATE>='" & VB6.Format(txtDateFrom, "DD-MMM-YYYY") & "'" & vbCrLf _
                ''                                        & " AND GATEPASS_DATE<='" & VB6.Format(txtDateTo, "DD-MMM-YYYY") & "'" & vbCrLf _
                ''                                        & " AND DIV_CODE=" & mDivision & ")"
            End If
        End If


        If cboShow.SelectedIndex <> 0 Then
            If lblBookType.Text = "OW" Then
                MakeSQL = MakeSQL & vbCrLf & "AND F4NO IN (" & F4Query & ")"
            Else
                MakeSQL = MakeSQL & vbCrLf & "AND RGP_NO IN (" & F4Query & ")"
            End If
        End If

        If cboShow.SelectedIndex = 4 Then
            MakeSQL = MakeSQL & vbCrLf & " AND TRN.REF_DATE<=TRN.RGP_DATE+365"
        Else
            MakeSQL = MakeSQL & vbCrLf & " AND TRN.REF_DATE<=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If


        If cboShow.SelectedIndex = 4 Then
            mRGPDateFrom = CStr(System.Date.FromOADate(CDate(txtAsOn.Text).ToOADate - 365))

            MakeSQL = MakeSQL & vbCrLf & " AND TRN.RGP_DATE<=TO_DATE('" & VB6.Format(mRGPDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQL = MakeSQL & vbCrLf & " AND TRN.RGP_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TRN.RGP_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        End If

        If cboShow.SelectedIndex = 3 Then
            MakeSQL = MakeSQL & vbCrLf & " AND TRN.EXP_RTN_DATE<=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If lblBookType.Text = "OW" Then
            MakeSQL = MakeSQL & vbCrLf & "AND (TRN.F4NO IS NOT  NULL OR TRN.F4NO<>0)"
        End If

        ''ORDER CLAUSE...

        If optOrder(0).Checked = True Then
            If lblBookType.Text = "OW" Then
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY  F4NO, F4DATE, TRN.OUTWARD_ITEM_CODE,TRN.BILL_DATE,ITEM_IO"
            Else
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY TRN.RGP_DATE,RGP_NO, TRN.OUTWARD_ITEM_CODE,TRN.BILL_DATE,ITEM_IO"
            End If
        ElseIf optOrder(1).Checked = True Then
            If lblBookType.Text = "OW" Then
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,F4DATE, F4NO, TRN.OUTWARD_ITEM_CODE,TRN.BILL_DATE"
            Else
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,TRN.RGP_DATE ,RGP_NO, TRN.OUTWARD_ITEM_CODE,TRN.BILL_DATE"
            End If
        Else
            If lblBookType.Text = "OW" Then
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY  TRN.OUTWARD_ITEM_CODE, F4NO, F4DATE, TRN.BILL_DATE,ITEM_IO"
            Else
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY TRN.OUTWARD_ITEM_CODE, TRN.RGP_DATE,RGP_NO, TRN.BILL_DATE,ITEM_IO"
            End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
        '    Resume
    End Function

    Private Function GetAlterItemCode(ByRef mItemCode As String) As String

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim mSubItemCode As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTemp1 As ADODB.Recordset = Nothing
        Dim RSTemp2 As ADODB.Recordset = Nothing

        Dim xInItemCode As String = ""
        Dim mKey As String
        Dim mRMItemCode As String



        mSubItemCode = ""
        SqlStr = "SELECT IH.PRODUCT_CODE " & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=ID.PRODUCT_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.ITEM_CODE='" & mItemCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xInItemCode = Trim(IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))

                SqlStr = "SELECT IH.MKEY, IH.PRODUCT_CODE, ID.ITEM_CODE,ITEM_QTY " & vbCrLf & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_DET ID " & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & xInItemCode & "'"

                SqlStr = SqlStr & vbCrLf & " AND IH.WEF = (" & vbCrLf & " SELECT MAX(WEF) " & vbCrLf & " FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & xInItemCode & "'" & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp1.EOF = False Then
                    mKey = Trim(IIf(IsDbNull(RsTemp1.Fields("mKey").Value), "", RsTemp1.Fields("mKey").Value))
                    mRMItemCode = Trim(IIf(IsDbNull(RsTemp1.Fields("ITEM_CODE").Value), "", RsTemp1.Fields("ITEM_CODE").Value))

                    Do While Not RsTemp1.EOF
                        If mSubItemCode = "" Then
                            mSubItemCode = "'" & Trim(IIf(IsDbNull(RsTemp1.Fields("ITEM_CODE").Value), "", RsTemp1.Fields("ITEM_CODE").Value)) & "'"
                        Else
                            mSubItemCode = mSubItemCode & "," & "'" & Trim(IIf(IsDbNull(RsTemp1.Fields("ITEM_CODE").Value), "", RsTemp1.Fields("ITEM_CODE").Value)) & "'"
                        End If
                        RsTemp1.MoveNext()
                    Loop
                    SqlStr = "SELECT ALTER_ITEM_CODE " & vbCrLf & " FROM PRD_OUTBOM_ALTER_DET " & vbCrLf & " WHERE " & vbCrLf & " MKEY='" & mKey & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & mRMItemCode & "'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSTemp2, ADODB.LockTypeEnum.adLockReadOnly)
                    If RSTemp2.EOF = False Then
                        Do While Not RSTemp2.EOF
                            If mSubItemCode = "" Then
                                mSubItemCode = "'" & Trim(IIf(IsDbNull(RSTemp2.Fields("ALTER_ITEM_CODE").Value), "", RSTemp2.Fields("ALTER_ITEM_CODE").Value)) & "'"
                            Else
                                mSubItemCode = mSubItemCode & "," & "'" & Trim(IIf(IsDbNull(RSTemp2.Fields("ALTER_ITEM_CODE").Value), "", RSTemp2.Fields("ALTER_ITEM_CODE").Value)) & "'"
                            End If
                            RSTemp2.MoveNext()
                        Loop
                    End If
                End If

                RsTemp.MoveNext()
            Loop
            If mSubItemCode = "" Then
                mSubItemCode = "('" & mItemCode & "')"
            Else
                mSubItemCode = "(" & mSubItemCode & ",'" & xInItemCode & "')"
            End If
        Else
            mSubItemCode = "('" & mItemCode & "')"
        End If

        GetAlterItemCode = mSubItemCode
        Exit Function
LedgError:
        GetAlterItemCode = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function MakeSQLSumm() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mSuppCode As String
        Dim xAlterItemCode As String
        Dim mCategoryCode As String = ""
        Dim mCategoryStr As String
        Dim CntLst As Integer
        Dim mCategory As String
        Dim mAllTrnType As Boolean
        Dim mDivision As Double
        Dim mEmpCode As String
        Dim mPrepareBy As String
        Dim mPurpose As String
        Dim mAllPurpose As String
        Dim mRGPDateFrom As String

        ''SELECT CLAUSE...
        ''
        mCategoryStr = ""
        If optShow(1).Checked = True Then
            If lblBookType.Text = "OW" Then
                MakeSQLSumm = " SELECT '',F4NO, CHALLAN_PREFIX || GATEPASS_NO AS CHALLANO, TO_CHAR(TRN.F4DATE,'DD/MM/YYYY') AS F4DATE ,CMST.SUPP_CUST_NAME, "
            Else
                If OptWise(0).Checked = True Then
                    MakeSQLSumm = " SELECT GETRGPDEPT(TRN.COMPANY_CODE, RGP_NO, TRN.RGP_DATE) AS DEPT,RGP_NO,CHALLAN_PREFIX || GATEPASS_NO AS CHALLANO, TO_CHAR(TRN.RGP_DATE,'DD/MM/YYYY') AS RGP_DATE  ,CMST.SUPP_CUST_NAME, "
                ElseIf OptWise(1).Checked = True Then
                    MakeSQLSumm = " SELECT GETRGPEMP(TRN.COMPANY_CODE, RGP_NO, TRN.RGP_DATE) AS DEPT,RGP_NO,CHALLAN_PREFIX || GATEPASS_NO AS CHALLANO, TO_CHAR(TRN.RGP_DATE,'DD/MM/YYYY') AS RGP_DATE ,CMST.SUPP_CUST_NAME, "
                Else
                    MakeSQLSumm = " SELECT GETRGPEMPWithPREID(TRN.COMPANY_CODE, RGP_NO, TRN.RGP_DATE) AS DEPT,RGP_NO, CHALLAN_PREFIX || GATEPASS_NO AS CHALLANO,TO_CHAR(TRN.RGP_DATE,'DD/MM/YYYY') AS RGP_DATE ,CMST.SUPP_CUST_NAME, "
                End If
            End If
        Else
            If lblBookType.Text = "OW" Then
                MakeSQLSumm = " SELECT '','', '','' ,CMST.SUPP_CUST_NAME, "
            Else
                MakeSQLSumm = " SELECT '' AS DEPT,'','', '' ,CMST.SUPP_CUST_NAME, "
            End If

        End If
        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " TRN.OUTWARD_ITEM_CODE, A.ITEM_SHORT_DESC, "

        If optShow(1).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " 0 AS Opening, "
        Else
            MakeSQLSumm = MakeSQLSumm & vbCrLf _
                & " TO_CHAR(SUM(DECODE(ITEM_IO,'I',-1,1) * TRN.RGP_QTY * CASE WHEN TRN.REF_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END ),'9999999.9999') AS Opening, "
        End If
        ''TRN.RGP_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND 

        'MakeSQLSumm = MakeSQLSumm & vbCrLf _
        '    & " TO_CHAR(SUM(DECODE(ITEM_IO,'O',TRN.RGP_QTY,0)),'9999999.9999') AS Issued, " & vbCrLf _
        '    & " TO_CHAR(SUM(DECODE(ITEM_IO,'I',TRN.RGP_QTY,0)),'9999999.9999') AS Received,    "


        MakeSQLSumm = MakeSQLSumm & vbCrLf _
                & " TO_CHAR(SUM(DECODE(ITEM_IO,'O',1,0) * TRN.RGP_QTY * CASE WHEN TRN.REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND  TRN.REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END ),'9999999.9999') AS Issued, " & vbCrLf _
                & " TO_CHAR(SUM(DECODE(ITEM_IO,'I',1,0) * TRN.RGP_QTY * CASE WHEN TRN.REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND  TRN.REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END ),'9999999.9999') AS Received, "


        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " '',    " & vbCrLf _
            & " '','', " & vbCrLf _
            & " TO_CHAR(SUM(DECODE(ITEM_IO,'I',-1,1)*TRN.RGP_QTY),'9999999.9999') AS Balance,"

        If optShow(1).Checked = True Then
            If lblBookType.Text = "OW" Then
                MakeSQLSumm = MakeSQLSumm & vbCrLf & " F4DATE+90,MAX(GETRGPRATE (GATEPASS_NO,TRN.OUTWARD_ITEM_CODE,TRN.COMPANY_CODE)),0,0 "
            Else
                MakeSQLSumm = MakeSQLSumm & vbCrLf & " TRN.EXP_RTN_DATE,MAX(GETRGPRATE (RGP_NO,TRN.OUTWARD_ITEM_CODE,TRN.COMPANY_CODE)),0,0 "
            End If
        Else
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " '',MAX(GETRGPRATE (RGP_NO,TRN.OUTWARD_ITEM_CODE,TRN.COMPANY_CODE)),0,0 "
        End If
        ''FROM CLAUSE...
        MakeSQLSumm = MakeSQLSumm & vbCrLf & " FROM INV_GATEPASS_HDR IH, INV_RGP_REG_TRN TRN, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST A "

        ''WHERE CLAUSE...
        MakeSQLSumm = MakeSQLSumm & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TRN.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND TRN.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND TRN.COMPANY_CODE=A.COMPANY_CODE" & vbCrLf & " AND TRN.OUTWARD_ITEM_CODE=A.ITEM_CODE "

        MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND IH.AUTO_KEY_PASSNO=TRN.RGP_NO"

        ''& " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        '
        If cboShow.SelectedIndex = 4 Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND TRN.REF_DATE<=TRN.RGP_DATE+365"
        Else
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND TRN.REF_DATE<=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If


        If cboShow.SelectedIndex = 4 Then
            mRGPDateFrom = CStr(System.Date.FromOADate(CDate(txtDateTo.Text).ToOADate - 365))

            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND TRN.RGP_DATE<=TO_DATE('" & VB6.Format(mRGPDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            If optShow(1).Checked = True Then
                MakeSQLSumm = MakeSQLSumm & vbCrLf _
                    & "AND TRN.RGP_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND TRN.RGP_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                MakeSQLSumm = MakeSQLSumm & vbCrLf _
                    & "AND TRN.RGP_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        End If
        If cboShow.SelectedIndex = 3 Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND TRN.EXP_RTN_DATE<=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then

            If OptShowNo(0).Checked = True Then
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND TRN.F4NO='" & MainClass.AllowSingleQuote(TxtC4No.Text) & "'"
            Else
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND TRN.RGP_NO='" & MainClass.AllowSingleQuote(TxtC4No.Text) & "'"
            End If
        End If

        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemDesc.Text, "Item_Short_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                xAlterItemCode = GetAlterMainItemCode(mItemCode) ' GetAlterItemCode(mItemCode)
                If xAlterItemCode = "" Then
                    MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND TRN.OUTWARD_ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                Else
                    MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND TRN.OUTWARD_ITEM_CODE IN " & xAlterItemCode & ""
                End If
            End If

        End If

        If ChkPartyAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCode = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND TRN.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'"
            End If
        End If

        mEmpCode = ""
        If chkEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtName.Text, "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmpCode = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND GETRGPRESPONSIBLEEMP(TRN.COMPANY_CODE, RGP_NO, TRN.RGP_DATE)='" & MainClass.AllowSingleQuote(mEmpCode) & "'"
            End If
        End If

        mPrepareBy = ""
        If chkPrepareBy.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtPrepareBy.Text, "USER_ID", "USER_ID", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPrepareBy = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND GETRGPPREPAREBY(TRN.COMPANY_CODE, RGP_NO, TRN.RGP_DATE)='" & MainClass.AllowSingleQuote(mPrepareBy) & "'"
            End If
        End If

        mAllTrnType = True
        If lstCategory.GetItemChecked(0) = True Then
            mCategoryStr = ""
        Else
            For CntLst = 1 To lstCategory.Items.Count - 1
                If lstCategory.GetItemChecked(CntLst) = True Then
                    mCategory = VB6.GetItemString(lstCategory, CntLst)
                    If MainClass.ValidateWithMasterTable(mCategory, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                        mCategoryCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCategoryStr = IIf(mCategoryStr = "", "'" & mCategoryCode & "'", mCategoryStr & "," & "'" & mCategoryCode & "'")
                Else
                    mAllTrnType = False
                End If
            Next
        End If
        If mCategoryStr <> "" And mAllTrnType = False Then
            mCategoryStr = "(" & mCategoryStr & ")"
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND A.CATEGORY_CODE IN " & mCategoryStr & ""
        End If


        mAllPurpose = CStr(True)
        mPurpose = ""
        For CntLst = 0 To lstPurpose.Items.Count - 1
            If lstPurpose.GetItemChecked(CntLst) = True Then
                mPurpose = "'" & VB.Left(VB6.GetItemString(lstPurpose, CntLst), 1) & "'"
            Else
                mAllPurpose = CStr(False)
            End If
        Next

        If mPurpose <> "" And CBool(mAllPurpose) = False Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND IH.PURPOSE IN (" & mPurpose & ")"
        End If


        If optShow(1).Checked = True Then
            If lblBookType.Text = "OW" Then
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND (TRN.F4NO IS NOT NULL OR TRN.F4NO<>0)"
            End If
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""

                '            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND TRN.RGP_NO IN (SELECT AUTO_KEY_PASSNO FROM " & vbCrLf _
                ''                                        & " INV_GATEPASS_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                ''                                        & " AND GATEPASS_DATE>='" & VB6.Format(txtDateFrom, "DD-MMM-YYYY") & "'" & vbCrLf _
                ''                                        & " AND GATEPASS_DATE<='" & VB6.Format(txtDateTo, "DD-MMM-YYYY") & "'" & vbCrLf _
                ''                                        & " AND DIV_CODE=" & mDivision & ")"
            End If
        End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "HAVING SUM(DECODE(ITEM_IO,'I',-1,1)*TRN.RGP_QTY)=0"
        ElseIf (cboShow.SelectedIndex = 2 Or cboShow.SelectedIndex = 3 Or cboShow.SelectedIndex = 4) Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "HAVING SUM(DECODE(ITEM_IO,'I',-1,1)*TRN.RGP_QTY)<>0"
        End If


        'Group CLAUSE...
        If optShow(1).Checked = True Then
            If lblBookType.Text = "OW" Then
                MakeSQLSumm = MakeSQLSumm & vbCrLf & " GROUP BY F4NO , CHALLAN_PREFIX || GATEPASS_NO , TO_CHAR(F4DATE,'DD/MM/YYYY'), " & vbCrLf _
                    & " TRN.OUTWARD_ITEM_CODE, A.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME,TRN.EXP_RTN_DATE"      '',GETRGPRATE (GATEPASS_NO,TRN.OUTWARD_ITEM_CODE,TRN.COMPANY_CODE) "
            Else
                If OptWise(0).Checked = True Then
                    MakeSQLSumm = MakeSQLSumm & vbCrLf _
                        & " GROUP BY GETRGPDEPT(TRN.COMPANY_CODE, RGP_NO, TRN.RGP_DATE), RGP_NO , CHALLAN_PREFIX || GATEPASS_NO ,TRN.RGP_DATE, TO_CHAR(TRN.RGP_DATE,'DD/MM/YYYY'), " & vbCrLf & " TRN.OUTWARD_ITEM_CODE, A.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME,TRN.EXP_RTN_DATE"  '',GETRGPRATE (RGP_NO,TRN.OUTWARD_ITEM_CODE,TRN.COMPANY_CODE) "
                ElseIf OptWise(1).Checked = True Then
                    MakeSQLSumm = MakeSQLSumm & vbCrLf _
                        & " GROUP BY GETRGPEMP(TRN.COMPANY_CODE, RGP_NO, TRN.RGP_DATE), RGP_NO , CHALLAN_PREFIX || GATEPASS_NO ,TRN.RGP_DATE,TO_CHAR(TRN.RGP_DATE,'DD/MM/YYYY'), " & vbCrLf & " TRN.OUTWARD_ITEM_CODE, A.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME,TRN.EXP_RTN_DATE"  '',GETRGPRATE (RGP_NO,TRN.OUTWARD_ITEM_CODE,TRN.COMPANY_CODE) "
                Else
                    MakeSQLSumm = MakeSQLSumm & vbCrLf _
                        & " GROUP BY GETRGPEMPWithPREID(TRN.COMPANY_CODE, RGP_NO, TRN.RGP_DATE), RGP_NO , CHALLAN_PREFIX || GATEPASS_NO ,TRN.RGP_DATE, TO_CHAR(TRN.RGP_DATE,'DD/MM/YYYY')," & vbCrLf & " TRN.OUTWARD_ITEM_CODE, A.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME,TRN.EXP_RTN_DATE"  '',GETRGPRATE (RGP_NO,TRN.OUTWARD_ITEM_CODE,TRN.COMPANY_CODE) "
                End If
            End If
        Else
            If lblBookType.Text = "OW" Then
                MakeSQLSumm = MakeSQLSumm & vbCrLf & " GROUP BY TRN.OUTWARD_ITEM_CODE, A.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME"       '',GETRGPRATE (RGP_NO,TRN.OUTWARD_ITEM_CODE,TRN.COMPANY_CODE)"
            Else
                MakeSQLSumm = MakeSQLSumm & vbCrLf & " GROUP BY TRN.OUTWARD_ITEM_CODE, A.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME"       '',GETRGPRATE (RGP_NO,TRN.OUTWARD_ITEM_CODE,TRN.COMPANY_CODE)"
            End If
        End If
        ''ORDER CLAUSE...

        If optShow(1).Checked = True Then
            If optOrder(0).Checked = True Then
                If lblBookType.Text = "OW" Then
                    MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY F4NO, F4DATE, TRN.OUTWARD_ITEM_CODE"
                Else
                    MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY TRN.RGP_DATE,RGP_NO, TRN.OUTWARD_ITEM_CODE"
                End If
            ElseIf optOrder(1).Checked = True Then
                If lblBookType.Text = "OW" Then
                    MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,F4DATE ,F4NO, TRN.OUTWARD_ITEM_CODE"
                Else
                    MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,TRN.RGP_DATE ,RGP_NO, TRN.OUTWARD_ITEM_CODE"
                End If
            ElseIf optOrder(2).Checked = True Then
                If lblBookType.Text = "OW" Then
                    MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY TRN.OUTWARD_ITEM_CODE, F4NO, F4DATE"
                Else
                    MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY TRN.OUTWARD_ITEM_CODE, TRN.RGP_DATE,RGP_NO"
                End If
            Else
                If lblBookType.Text = "OW" Then
                    MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY F4NO, F4DATE, TRN.OUTWARD_ITEM_CODE"
                Else
                    If OptWise(0).Checked = True Then
                        MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY GETRGPDEPT(TRN.COMPANY_CODE, RGP_NO, TRN.RGP_DATE),TRN.RGP_DATE,RGP_NO, TRN.OUTWARD_ITEM_CODE"
                    ElseIf OptWise(1).Checked = True Then
                        MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY GETRGPEMP(TRN.COMPANY_CODE, RGP_NO, TRN.RGP_DATE),TRN.RGP_DATE,RGP_NO, TRN.OUTWARD_ITEM_CODE"
                    Else
                        MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY GETRGPEMPWithPREID(TRN.COMPANY_CODE, RGP_NO, TRN.RGP_DATE),TRN.RGP_DATE,RGP_NO, TRN.OUTWARD_ITEM_CODE"
                    End If
                End If ''
            End If
        Else
            If optOrder(1).Checked = True Then
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME, TRN.OUTWARD_ITEM_CODE"
            Else
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY TRN.OUTWARD_ITEM_CODE, CMST.SUPP_CUST_NAME"
            End If
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function


    Private Function F4Query() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mSuppCode As String
        Dim mSqlStr As String
        Dim mRGPDateFrom As String

        ''SELECT CLAUSE...

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mSqlStr = "DELETE FROM Temp_F4Detail NOLOGGING " & vbCrLf & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(mSqlStr)

        mSqlStr = ""
        mSqlStr = " INSERT INTO Temp_F4Detail (USERID, COMPANY_CODE, " & vbCrLf & " FYEAR, PARTY_F4DATE, ITEM_CODE, MKEY, PARTY_F4NO)  "

        mSqlStr = mSqlStr & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ","

        mSqlStr = mSqlStr & vbCrLf & " TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'A','A', "

        If lblBookType.Text = "OW" Then
            mSqlStr = mSqlStr & vbCrLf & " F4NO "
        Else
            mSqlStr = mSqlStr & vbCrLf & " RGP_NO "
        End If


        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_RGP_REG_TRN TRN" ', FIN_SUPP_CUST_MST CMST, INV_ITEM_MST A "

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" '& vbCrLf |            & " AND TRN.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf |            & " AND TRN.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf |            & " AND TRN.COMPANY_CODE=A.COMPANY_CODE" & vbCrLf |            & " AND TRN.OUTWARD_ITEM_CODE=A.ITEM_CODE "


        ''& " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        '
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then

            If OptShowNo(0).Checked = True Then
                mSqlStr = mSqlStr & vbCrLf & "AND TRN.F4NO='" & MainClass.AllowSingleQuote(TxtC4No.Text) & "'"
            Else
                mSqlStr = mSqlStr & vbCrLf & "AND TRN.RGP_NO='" & MainClass.AllowSingleQuote(TxtC4No.Text) & "'"
            End If
        End If

        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemDesc.Text, "Item_Short_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                mSqlStr = mSqlStr & vbCrLf & "AND TRN.OUTWARD_ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If

        End If

        If ChkPartyAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCode = MasterNo
                mSqlStr = mSqlStr & vbCrLf & "AND TRN.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'"
            End If
        End If

        If cboShow.SelectedIndex = 4 Then
            mSqlStr = mSqlStr & vbCrLf & " AND TRN.REF_DATE<=TRN.RGP_DATE+365"
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND TRN.REF_DATE<=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If cboShow.SelectedIndex = 4 Then
            mRGPDateFrom = CStr(System.Date.FromOADate(CDate(txtAsOn.Text).ToOADate - 365))

            mSqlStr = mSqlStr & vbCrLf & " AND TRN.RGP_DATE<=TO_DATE('" & VB6.Format(mRGPDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            mSqlStr = mSqlStr & vbCrLf & "AND TRN.RGP_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TRN.RGP_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" ''& vbCrLf |                & " AND TRN.REF_DATE<='" & VB6.Format(txtAsOn, "DD-MMM-YYYY") & "'"
        End If

        If cboShow.SelectedIndex = 3 Then
            mSqlStr = mSqlStr & vbCrLf & " AND TRN.EXP_RTN_DATE<=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If cboShow.SelectedIndex = 1 Then
            mSqlStr = mSqlStr & vbCrLf & "HAVING SUM(DECODE(ITEM_IO,'I',-1,1)*TRN.RGP_QTY)=0"
        ElseIf (cboShow.SelectedIndex = 2 Or cboShow.SelectedIndex = 3 Or cboShow.SelectedIndex = 4) Then
            mSqlStr = mSqlStr & vbCrLf & "HAVING SUM(DECODE(ITEM_IO,'I',-1,1)*TRN.RGP_QTY)<>0"
        End If

        If lblBookType.Text = "OW" Then
            mSqlStr = mSqlStr & vbCrLf & "AND (TRN.F4NO IS NOT NULL OR TRN.F4NO<>0)"
        End If

        'Group CLAUSE...
        If lblBookType.Text = "OW" Then
            mSqlStr = mSqlStr & vbCrLf & " GROUP BY F4NO "
        Else
            mSqlStr = mSqlStr & vbCrLf & " GROUP BY RGP_NO "
        End If
        PubDBCn.Execute(mSqlStr)

        PubDBCn.CommitTrans()
        F4Query = "SELECT DISTINCT PARTY_F4NO FROM Temp_F4Detail " & vbCrLf & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"


        Exit Function
ERR1:
        MsgInformation(Err.Description)
        PubDBCn.RollbackTrans()
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If chkAll.Value = vbUnchecked Then
        '        If Trim(TxtC4No.Text) = "" Then
        '            MsgInformation "Invaild C4."
        '            TxtC4No.SetFocus
        '            FieldsVerification = False
        '            Exit Function
        '        End If
        '        If MainClass.ValidateWithMasterTable(TxtC4No.Text, "PARTY_F4NO", "PARTY_F4NO", "DSP_PAINT57F4_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            mPartyC4 = MasterNo
        '        Else
        '            MsgInformation "Invaild C4"
        '            TxtC4No.SetFocus
        '            FieldsVerification = False
        '            Exit Function
        '        End If
        '    End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub CalcSprdTotal()

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim StartRow As Integer
        Dim EndRow As Integer
        Dim mIssued As Double
        Dim mRecd As Double
        Dim mPreviousItemCode As String = ""

        Dim mPartyC4 As String
        Dim mItemCode As String
        Dim mCheckCode As String

        Dim mPartyC4No As String
        Dim mPartyC4Date As String
        Dim mPartyName As String
        Dim mMTRLCode As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mItemRate As Double

        Dim mBalQty As Double
        Dim mRate As Double
        Dim mValue As Double

        Dim mTotIssued As Double
        Dim mTotRecd As Double
        Dim mOpening As Double

        Call MainClass.AddBlankfpSprdRow(SprdMain, ColPartyC4No)
        cntRow = 1
        StartRow = 1
        With SprdMain
            Do While cntRow <= .MaxRows
                .Row = cntRow
                .Col = ColPartyC4No

                mPartyC4 = Trim(.Text)

                .Col = ColMTRLCode
                mItemCode = Trim(.Text)

                '.Col = ColIssued
                'mTotIssued = mTotIssued + Val(.Text)

                '.Col = ColRecd
                'mTotRecd = mTotRecd + Val(.Text)

                mCheckCode = mPartyC4 & mItemCode

                If mPreviousItemCode <> mCheckCode And cntRow <> 1 Then
                    .MaxRows = .MaxRows + 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow


                    EndRow = cntRow
                    .Row = cntRow
                    .Col = ColMTRLCode
                    .Font = VB6.FontChangeBold(.Font, True)
                    .Text = "TOTAL"

                    Call CalcRowTotal(SprdMain, ColOpening, StartRow, ColOpening, EndRow - 1, EndRow, ColOpening)
                    Call CalcRowTotal(SprdMain, ColIssued, StartRow, ColIssued, EndRow - 1, EndRow, ColIssued)
                    Call CalcRowTotal(SprdMain, ColRecd, StartRow, ColRecd, EndRow - 1, EndRow, ColRecd)

                    'FormatSprdMain(cntRow)
                    '                DoEvents
                    .Row = cntRow

                    .Col = ColOpening
                    '.Text = mTotIssued
                    mOpening = Val(.Text)
                    '.Font = VB6.FontChangeBold(.Font, True)

                    .Col = ColIssued
                    '.Text = mTotIssued
                    mIssued = Val(.Text)
                    '.Font = VB6.FontChangeBold(.Font, True)

                    .Col = ColRecd
                    '.Text = mTotRecd
                    mRecd = Val(.Text)
                    '.Font = VB6.FontChangeBold(.Font, True)

                    .Col = ColBalQty
                    .Text = VB6.Format(mOpening + mIssued - mRecd, "0.00")
                    mBalQty = CDbl(VB6.Format(mOpening + mIssued - mRecd, "0.00"))
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Row = cntRow
                    .Row2 = cntRow
                    .Col = 1
                    .Col2 = .MaxCols
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F)
                    .BlockMode = False

                    mTotRecd = 0
                    mTotIssued = 0

                    cntRow = cntRow + 1
                    .Row = cntRow
                    StartRow = cntRow
                End If
                .Col = ColPartyC4No
                mPartyC4 = Trim(.Text)


                .Col = ColMTRLCode
                mItemCode = Trim(.Text)
                mPreviousItemCode = mPartyC4 & mItemCode


                cntRow = cntRow + 1
            Loop
            .Row = .MaxRows
            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow

        End With

        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Function GetF4Rate(ByRef mPartyC4No As String, ByRef mPartyC4Date As String, ByRef mPartyName As String, ByRef mMTRLCode As String, ByRef mBillNo As String, ByRef mBillDate As String) As Double

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPartyCode As String


        If mPartyC4No = "" Then GetF4Rate = 0 : Exit Function
        If mPartyC4Date = "" Then GetF4Rate = 0 : Exit Function

        If mPartyName = "" Then GetF4Rate = 0 : Exit Function
        If mMTRLCode = "" Then GetF4Rate = 0 : Exit Function

        If mBillNo = "" Then GetF4Rate = 0 : Exit Function
        If mBillDate = "" Then GetF4Rate = 0 : Exit Function
        mPartyCode = "-1"

        If MainClass.ValidateWithMasterTable(mPartyName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyCode = MasterNo
        End If

        mSqlStr = "SELECT ITEM_RATE FROM INV_GATEPASS_HDR IH, INV_GATEPASS_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_PASSNO=ID.AUTO_KEY_PASSNO" & vbCrLf & " AND IH.AUTO_KEY_PASSNO=" & Val(mBillNo) & "" & vbCrLf & " AND IH.GATEPASS_DATE=TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.OUTWARD_57F4NO=" & Val(mPartyC4No) & "" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mMTRLCode) & "'"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetF4Rate = IIf(IsDbNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Not IsDate(txtDateFrom.Text) Then
            MsgInformation("Invalid date")
            Cancel = True
        End If
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Not IsDate(txtDateTo.Text) Then
            MsgInformation("Invalid date")
            Cancel = True
        End If
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDays_Change()
        Call PrintStatus(False)
    End Sub

    Private Sub txtItemDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemDesc.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtItemDesc_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemDesc.DoubleClick
        SearchItem()
    End Sub


    Private Sub txtItemDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemDesc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtItemDesc_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemDesc.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub


    Private Sub txtItemDesc_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemDesc.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If Trim(txtItemDesc.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable((txtItemDesc.Text), "ITEM_SHORT_DESC", "ITEm_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            '        txtItemDesc.Text = UCase(Trim(MasterNo))
        Else
            MsgInformation("Invalid Item Code.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.DoubleClick
        SearchEmp()
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchEmp()
    End Sub

    Private Sub TxtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If Trim(txtName.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable((txtName.Text), "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            '        txtItemDesc.Text = UCase(Trim(MasterNo))
        Else
            MsgInformation("Invalid Responsibile Emp Name.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPartyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtPartyName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.DoubleClick
        SearchParty()
    End Sub


    Private Sub txtPartyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartyName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPartyName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartyName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchParty()
    End Sub


    Private Sub txtPartyName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartyName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If Trim(txtPartyName.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable((txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Party Name.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtPrepareBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrepareBy.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtPrepareBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrepareBy.DoubleClick
        SearchPrepareBy()
    End Sub


    Private Sub txtPrepareBy_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPrepareBy.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPrepareBy.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPrepareBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPrepareBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchPrepareBy()
    End Sub


    Private Sub txtPrepareBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPrepareBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If Trim(txtPrepareBy.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable((txtPrepareBy.Text), "USER_ID", "USER_ID", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            '        txtItemDesc.Text = UCase(Trim(MasterNo))
        Else
            MsgInformation("Invalid User ID.")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
