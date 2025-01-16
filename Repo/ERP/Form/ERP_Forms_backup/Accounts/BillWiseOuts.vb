Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmBillWiseOuts
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 12

    Private Const ColUnitName As Short = 1
    Private Const ColName As Short = 2
    Private Const ColBill As Short = 3
    Private Const ColDate As Short = 4
    Private Const ColVNo As Short = 5
    Private Const ColVDate As Short = 6
    Private Const ColDebitAmount As Short = 7
    Private Const ColCreditAmount As Short = 8
    Private Const ColBal As Short = 9
    Private Const ColDrCr As Short = 10
    Private Const ColBookType As Short = 11
    Private Const ColBookSubType As Short = 12
    Private Const ColMKEY As Short = 13

    Dim mClickProcess As Boolean

    Dim mActiveRow As Integer
    Dim PrintFlag As Boolean
    Dim CurrFormHeight As Integer
    Dim CurrFormWidth As Integer

    Private Sub cmdBillSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBillSearch.Click
        BillSearch()
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_TYPE='C' OR SUPP_CUST_TYPE='S')"

        If MainClass.SearchGridMaster((TxtAccount.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                TxtAccount.Text = AcName
            End If
        End If

        'MainClass.SearchMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", SqlStr)

        'If AcName <> "" Then
        '    TxtAccount.Text = AcName
        'End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForAgeingAnly(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        PrintFlag = False
        PrintStatus()

        MainClass.ClearGrid(SprdAgeing, RowHeight)
        If FieldsVerification = False Then Exit Sub

        AgeingInfo()
        DisplayTotal()

        FormatSprdAgeing()
        FillHeading()

        SprdAgeing.Focus()
        PrintFlag = True
        PrintStatus()
        MainClass.SetFocusToCell(SprdAgeing, mActiveRow, 4)
    End Sub
    Function FieldsVerification() As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        If MainClass.ChkIsdateF(txtFromDate) = False Then Exit Function

        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Exit Function
        End If

        If optParticulars.Checked = True Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                TxtAccount.Focus()
                MsgInformation("Please Select Account")
                Exit Function
            End If
        End If

        If optBill(0).Checked = True Then
            If optParticulars.Checked = True Then
                SqlStr = "SELECT DISTINCT BILLNO FROM FIN_POSTED_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ACCOUNTCODE='" & mAccountCode & "'" & vbCrLf & " AND BILLNO='" & MainClass.AllowSingleQuote(UCase(Trim(txtBillNo.Text))) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
                If RsTemp.EOF = True Then
                    txtBillNo.Focus()
                    MsgInformation("Invaild Bill No")
                    Exit Function
                End If
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub frmBillWiseOuts_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmBillWiseOuts_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String
        Dim Rs As ADODB.Recordset
        Dim CntLst As Long

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        PrintFlag = False
        txtFromDate.Text = RsCompany.Fields("START_DATE").Value
        txtDateTo.Text = CStr(RunDate)

        FormatSprdAgeing()
        FillHeading()

        lstCompanyName.Items.Clear()
        Sqlstr = "SELECT COMPANY_SHORTNAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_SHORTNAME"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If Rs.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While Rs.EOF = False
                lstCompanyName.Items.Add(Rs.Fields("COMPANY_SHORTNAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(Rs.Fields("COMPANY_SHORTNAME").Value = RsCompany.Fields("COMPANY_SHORTNAME").Value, True, False))
                Rs.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCompanyName.SelectedIndex = 0

        PrintStatus()
        Call frmBillWiseOuts_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub lstCompanyName_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstCompanyName.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstCompanyName.GetItemChecked(0) = True Then
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstCompanyName.GetItemChecked(e.Index - 1) = False Then
                    lstCompanyName.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub AgeingInfo()

        On Error GoTo LedgError
        Dim SqlStr As String
        Dim mSuppCustCode As String
        Dim mAgeingDays As String
        Dim mSql As String
        Dim mSqlStr As String
        Dim mBillDate As String
        Dim RsTemp As ADODB.Recordset
        Dim mCompanyName As String = ""
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String = ""

        If optParticulars.Checked = True Then
            If MainClass.ValidateWithMasterTable(Trim(TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCustCode = MasterNo
            End If
        End If

        If optParticulars.Checked = True And optBill(0).Checked = True Then
            mBillDate = ""


            SqlStr = " SELECT BILLDATE FROM FIN_POSTED_TRN " & vbCrLf _
                & " WHERE FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(Trim(UCase(mSuppCustCode))) & "'" & vbCrLf _
                & " AND BillNo='" & MainClass.AllowSingleQuote(UCase(Trim(txtBillNo.Text))) & "'" & vbCrLf _
                & " AND TRNTYPE IN ('N','O', DECODE(BOOKTYPE,'J','',DECODE(BOOKTYPE,'B','','B'))) AND BOOKTYPE<>'O' "

            If lstCompanyName.GetItemChecked(0) = True Then
                mCompanyCodeStr = ""
            Else
                For CntLst = 1 To lstCompanyName.Items.Count - 1
                    If lstCompanyName.GetItemChecked(CntLst) = True Then
                        mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                        If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                            mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                        End If
                        mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                    End If
                Next
            End If

            If mCompanyCodeStr <> "" Then
                mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            SqlStr = SqlStr & vbCrLf & " ORDER BY BILLDATE "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mBillDate = IIf(IsDbNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value)
            End If

        End If

        mCompanyCodeStr = ""

        mSql = " Sum(AMOUNT*DECODE(DC,'D',1,-1))"
        mSqlStr = " TO_CHAR(ABS(SUM(AMOUNT*DECODE(DC,'D',1,-1))),'999999999.99')"

        SqlStr = "SELECT CC.COMPANY_SHORTNAME, ACM.SUPP_CUST_NAME AS Name,OUTS.BillNo AS BillNo, " & vbCrLf _
            & " OUTS.BillDate AS BillDate, " & vbCrLf & " DECODE(OUTS.VNo,NULL,'',OUTS.VNo) as VNo," & vbCrLf _
            & " OUTS.VDate AS VDate,"

        SqlStr = SqlStr & vbCrLf _
            & " CASE WHEN " & mSql & ">=0 THEN " & vbCrLf _
            & " " & mSqlStr & " ELSE '' END As Debit, " & vbCrLf _
            & " CASE WHEN " & mSql & "<= 0 THEN " & vbCrLf _
            & " " & mSqlStr & " ELSE '' END as Credit," & vbCrLf _
            & " '' AS Balance, '' AS DrCr, " & vbCrLf _
            & " OUTS.BOOKTYPE, OUTS.BOOKSUBTYPE, OUTS.MKEY "

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_POSTED_TRN OUTS,FIN_SUPP_CUST_MST ACM, GEN_COMPANY_MST CC "

        SqlStr = SqlStr & vbCrLf _
            & " WHERE OUTS.COMPANY_CODE=CC.COMPANY_CODE"        ''OUTS.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If optParticulars.Checked = True And optBill(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf _
                & " AND OUTS.BILLDATE=TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf _
                & " AND OUTS.BOOKTYPE<>'O' AND  OUTS.BOOKSUBTYPE<>'O'"
        Else
            SqlStr = SqlStr & vbCrLf _
                & " AND OUTS.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND OUTS.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND OUTS.AccountCode=ACM.SUPP_CUST_CODE "

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND CC.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        If OptAll.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND (SUPP_CUST_TYPE='C' OR SUPP_CUST_TYPE='S')"
        ElseIf optParticulars.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND  OUTS.ACCOUNTCODE='" & MainClass.AllowSingleQuote(Trim(UCase(mSuppCustCode))) & "'"
        End If

        If optBill(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND OUTS.BillNo='" & MainClass.AllowSingleQuote(UCase(Trim(txtBillNo.Text))) & "'"
        End If

        If optParticulars.Checked = True And optBill(0).Checked = True Then

        Else
            SqlStr = SqlStr & vbCrLf & " AND OUTS.VDate>=TO_DATE('" & VB6.Format(txtFromDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf & " AND OUTS.VDate<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY ACM.SUPP_CUST_NAME, CC.COMPANY_SHORTNAME,OUTS.BillNo,OUTS.BillDate,OUTS.VDate, " & vbCrLf _
            & " DECODE(OUTS.VNo,NULL,'',OUTS.VNo),OUTS.MKEY, OUTS.BOOKTYPE, OUTS.BOOKSUBTYPE " & vbCrLf _
            & " HAVING " & mSql & " <> 0 " & vbCrLf _
            & " ORDER BY ACM.SUPP_CUST_NAME,OUTS.BillDate,OUTS.BillNo,OUTS.VDATE "

        MainClass.AssignDataInSprd8(SqlStr, SprdAgeing, StrConn, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LedgError:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdAgeing()

        With SprdAgeing
            .set_RowHeight(-1, RowHeight)
            .Row = -1
            .MaxCols = ColMKEY

            .Col = ColUnitName
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColName, 10)

            .Col = ColName
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColName, 19)

            .Col = ColBill
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY

            .set_ColWidth(ColBill, 9)
            .ColsFrozen = ColBill

            .Col = ColDate
            .set_ColWidth(ColDate, 8)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            .Col = ColVNo
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColVNo, 8)

            .Col = ColVDate
            .set_ColWidth(ColVDate, 8)
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            .Col = ColDebitAmount
            .set_ColWidth(ColDebitAmount, 10)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            .Col = ColCreditAmount
            .set_ColWidth(ColCreditAmount, 10)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            .Col = ColBal
            .set_ColWidth(ColBal, 10)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            .Col = ColDrCr
            .set_ColWidth(ColDrCr, 3)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT


            If optParticulars.Checked = True Then
                .Col = ColName
                .ColHidden = True
            Else
                .Col = ColName
                .ColHidden = False
            End If

            .Col = ColBookType
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBookType, 8)
            .ColHidden = True

            .Col = ColBookSubType
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBookSubType, 8)
            .ColHidden = True

            .Col = ColMKEY
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True

            .Col = ColBill
            .ColHidden = False
            .Col = ColDate
            .ColHidden = False
            .Col = ColVDate
            .ColHidden = False

            MainClass.SetSpreadColor(SprdAgeing, -1)
            MainClass.ProtectCell(SprdAgeing, 1, .MaxRows, 1, .MaxCols)
            SprdAgeing.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub FillHeading()
        With SprdAgeing
            .Row = 0

            .Col = ColUnitName
            .Text = "Unit Name"

            .Col = ColName
            .Text = "Account Name"

            .Col = ColBill
            .Text = "Bill No."

            .Col = ColDate
            .Text = "Bill Date"

            .Col = ColVNo
            .Text = "VNo"

            .Col = ColVDate
            .Text = "VDate"

            .Col = ColDebitAmount
            .Text = "Debit"

            .Col = ColCreditAmount
            .Text = "Credit"

            .Col = ColBookType
            .Text = "Book Type"

            .Col = ColBookSubType
            .Text = "Book Sub Type"

            .Col = ColMKEY
            .Text = "MKey"

        End With
    End Sub

    Private Sub frmBillWiseOuts_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdAgeing.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 150, mReFormWidth - 150, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 90, mReFormWidth - 90, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdAgeing, -1)
    End Sub

    Private Sub OptAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptAll.CheckedChanged
        If eventSender.Checked Then
            TxtAccount.Enabled = False
            cmdsearch.Enabled = False
            PrintFlag = False
            PrintStatus()
        End If
    End Sub

    Private Sub optBill_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optBill.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optBill.GetIndex(eventSender)
            txtBillNo.Enabled = IIf(Index = 1, False, True)
            cmdBillSearch.Enabled = IIf(Index = 1, False, True)
            PrintFlag = False
            PrintStatus()

        End If
    End Sub


    Private Sub optParticulars_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optParticulars.CheckedChanged
        If eventSender.Checked Then
            TxtAccount.Enabled = True
            cmdsearch.Enabled = True
            PrintFlag = False
            PrintStatus()
        End If
    End Sub

    Private Sub PrintStatus()
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
    End Sub
    Private Sub SprdAgeing_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdAgeing.DblClick
        Dim xVDate As String
        Dim xMKey As String
        Dim xVNo As String
        Dim xBookType As String
        Dim xBookSubType As String
        Dim pIndex As Integer
        Dim xVTYPE As String
        Dim mGetFY As Integer

        '    Call ViewAccountLedger
        With SprdAgeing
            .Row = .ActiveRow

            .Col = ColVDate
            xVDate = .Text

            .Col = ColMKEY
            xMKey = .Text

            .Col = ColVNo
            xVNo = .Text

            .Col = ColBookType
            xBookType = .Text

            .Col = ColBookSubType
            xBookSubType = .Text
        End With

        mGetFY = GetCurrentFYNo(PubDBCn, xVDate)

        If mGetFY <> RsCompany.Fields("FYEAR").Value Then
            MsgInformation("Not a current Year Voucher, So cann't be Open.")
            Exit Sub
        End If

        If xBookType = "B" Or xBookType = "F" Or xBookType = "C" Or xBookType = "J" Then
            xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)
            xVNo = VB.Right(xVNo, 5)
        ElseIf xBookType = "R" Or xBookType = "E" Then
            If RsCompany.Fields("FYEAR").Value >= 2020 Then
                xVTYPE = Mid(xVNo, 1, Len(xVNo) - 8)
                xVNo = VB.Right(xVNo, 8)
            Else
                xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)
                xVNo = VB.Right(xVNo, 5)
            End If
        End If

        Call ShowTrn(xMKey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType, Me)
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub

    Private Sub TxtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub
    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim RsACM As ADODB.Recordset

        On Error GoTo ERR1
        'lblAcCode.text = ""
        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "SELECT SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'"
        SqlStr = SqlStr & "AND (SUPP_CUST_TYPE IN ('C','S','2'))"
        SqlStr = SqlStr & "ORDER BY SUPP_CUST_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF Then
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForAgeingAnly(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForAgeingAnly(ByRef Mode As Crystal.DestinationConstants)


        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String
        PubDBCn.Errors.Clear()

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""


        Call FillPrintDummy()

        '''''Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)


        mSubTitle = "From : " & VB6.Format(txtFromDate.Text, "DD MMM, YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")


        mRPTName = "BillOutstanding.Rpt"
        mTitle = "Bill Wise Outstanding"

        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)
        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        '    Resume
    End Sub
    Private Sub FillPrintDummy()


        Dim mName As String
        Dim mBill As String
        Dim mDate As String
        Dim mVNo As String
        Dim mVdate As String
        Dim mDAmount As String
        Dim mCAmount As String
        Dim mBal As String
        Dim mDrCr As String
        Dim SqlStr As String
        Dim cntRow As Integer

        On Error GoTo ERR1

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdAgeing

            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColName
                If Trim(.Text) <> "" Then
                    mName = Trim(.Text)
                End If

                .Col = ColBill
                mBill = IIf(Trim(.Text) = "", ".", Trim(.Text))

                .Col = ColDate
                mDate = .Text

                .Col = ColVNo
                mVNo = Trim(.Text)

                .Col = ColVDate
                mVdate = .Text

                .Col = ColDebitAmount
                mDAmount = .Text

                .Col = ColCreditAmount
                mCAmount = .Text

                .Col = ColBal
                mBal = .Text

                .Col = ColDrCr
                mDrCr = .Text


                SqlStr = "Insert into TEMP_PrintDummyData (UserID,SubRow,Field1," & vbCrLf & " Field2,Field3,Field4,Field5,Field6,Field7,Field8," & vbCrLf & " Field9) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(mName)) & "', " & vbCrLf & " '" & Trim(mBill) & "', " & vbCrLf & " '" & Trim(mDate) & "', " & vbCrLf & " '" & Trim(mVNo) & "', " & vbCrLf & " '" & Trim(mVdate) & "', " & vbCrLf & " '" & Trim(mDAmount) & "', " & vbCrLf & " '" & Trim(mCAmount) & "', " & vbCrLf & " '" & Trim(mBal) & "', " & vbCrLf & " '" & Trim(mDrCr) & "') "

                PubDBCn.Execute(SqlStr)

NextRow:
            Next
        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        'Resume
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM TEMP_PrintDummyData PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW,Field1"

        FetchRecordForReport = mSqlStr

    End Function

    Private Sub txtBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub

    Private Sub txtBillNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.DoubleClick
        BillSearch()
    End Sub

    Private Sub txtBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBillNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBillNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then BillSearch()
    End Sub

    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub txtFromDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFromDate.TextChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub DisplayTotal()
        On Error GoTo DisplayErr
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mNextPartyName As String
        Dim mPartyName As String
        Dim mDAmount As Double
        Dim mCAmount As Double

        cntRow = 1
        With SprdAgeing
            Do While cntRow <= .MaxRows
                .Row = cntRow
                .Col = ColName
                mPartyName = .Text

                .Col = ColDebitAmount
                mDAmount = mDAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColCreditAmount
                mCAmount = mCAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                cntRow = cntRow + 1
                .Row = cntRow
                .Col = ColName
                mNextPartyName = .Text
                If mPartyName <> mNextPartyName Then
                    .Row = cntRow
                    .MaxRows = .MaxRows + 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow
                    For cntCol = ColDebitAmount To ColDrCr
                        .Col = cntCol
                        .Text = New String("_", 254)
                    Next

                    cntRow = cntRow + 1

                    .MaxRows = .MaxRows + 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow
                    Call GridTotal(mDAmount, mCAmount, cntRow - 1)

                    mDAmount = 0
                    mCAmount = 0

                    cntRow = cntRow + 1

                    .MaxRows = .MaxRows + 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow
                    For cntCol = ColDebitAmount To ColDrCr
                        .Col = cntCol
                        .Text = New String("_", 254)
                    Next
                    cntRow = cntRow + 1
                End If
            Loop

            .MaxRows = .MaxRows + 1
            For cntCol = ColDebitAmount To ColDrCr
                .Row = .MaxRows
                .Col = cntCol
                .Text = New String("_", 254)
            Next

            .MaxRows = .MaxRows + 1
            Call GridTotal(mDAmount, mCAmount, .MaxRows)

            .MaxRows = .MaxRows + 1
            For cntCol = ColDebitAmount To ColDrCr
                .Row = .MaxRows
                .Col = cntCol
                .Text = New String("_", 254)
            Next

        End With


        Exit Sub
DisplayErr:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel 'txtFromDate
    End Sub
    Private Sub txtFromDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtFromDate) = False Then
            txtFromDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtFromDate.Text))) = False Then
            txtFromDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel '
    End Sub
    Private Sub ViewAccountLedger()

        On Error GoTo ErrPart
        If SprdAgeing.ActiveRow <= 0 Then Exit Sub

        frmViewLedger.lblBookType.Text = "LEDG"

        SprdAgeing.Row = SprdAgeing.ActiveRow
        SprdAgeing.Col = ColName
        If LTrim(RTrim(SprdAgeing.Text)) = "" Then Exit Sub
        frmViewLedger.cboAccount.Text = LTrim(RTrim(SprdAgeing.Text))

        MainClass.ValidateWithMasterTable(SprdAgeing.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        frmViewLedger.lblAcCode.Text = MasterNo
        If LTrim(RTrim(frmViewLedger.lblAcCode.Text)) = "" Then Exit Sub

        SprdAgeing.Col = ColVDate
        frmViewLedger.txtDateFrom.Text = txtFromDate.Text     ' RsCompany.Fields("START_DATE").Value
        frmViewLedger.txtDateTo.Text = txtDateTo.Text
        frmViewLedger.OptSumDet(2).Checked = True
        '    frmViewLedger.cboDivision.Text = cboDivision.Text
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        frmViewLedger.MdiParent = Me.MdiParent
        frmViewLedger.Show()
        frmViewLedger.frmViewLedger_Activated(Nothing, New System.EventArgs())
        frmViewLedger.cmdShow_Click(Nothing, New System.EventArgs())

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub


    Private Sub BillSearch()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = ""


        If optParticulars.Checked = True Then
            If TxtAccount.Text <> "" Then
                If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SqlStr = SqlStr & " ACCOUNTCODE='" & MasterNo & "'"
                End If
            End If
        End If

        SqlStr = IIf(SqlStr = "", "", SqlStr & " AND ") & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "  "

        If MainClass.SearchGridMaster((txtBillNo.Text), "FIN_POSTED_TRN", "BILLNO", "BILLDATE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtBillNo.Text = AcName
            End If
        End If

        'MainClass.SearchMaster(txtBillNo.Text, "FIN_POSTED_TRN", "BILLNO", SqlStr)

        'If AcName <> "" Then
        '    txtBillNo.Text = AcName
        'End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub GridTotal(ByRef mDAmount As Double, ByRef mCAmount As Double, ByRef mRow As Integer)

        With SprdAgeing
            .Row = mRow
            .Col = ColBill
            .Text = "TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColDebitAmount
            .Text = VB6.Format(mDAmount, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColCreditAmount
            .Text = VB6.Format(mCAmount, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColBal
            .Text = VB6.Format(System.Math.Abs(mDAmount - mCAmount), "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColDrCr
            .Text = CStr(IIf((mDAmount - mCAmount) >= 0, "Dr", "Cr"))
            .Font = VB6.FontChangeBold(.Font, True)
        End With
    End Sub
End Class
