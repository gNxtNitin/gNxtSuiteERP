Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports AxFPSpreadADO
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
Imports System.Data.OleDb
'Imports Infragistics.Win.UltraWinTabControl

Friend Class frmParamChqClearing
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection					
    Dim mAccountCode As Integer
    Private Const RowHeight As Short = 15

    Private Const ColMKEY As Short = 1
    Private Const ColCompanyCode As Short = 2
    Private Const colType As Short = 3
    Private Const ColVNo As Short = 4
    Private Const ColVDate As Short = 5
    Private Const ColChqNo As Short = 6
    Private Const ColChqDate As Short = 7
    Private Const ColPartyName As Short = 8
    Private Const ColAmount As Short = 9
    Private Const ColOrderBy As Short = 10
    Private Const ColNarration As Short = 11
    Private Const ColPostStatus As Short = 12

    Dim mSearchKey As String
    Dim cntSearchRow As Long
    Dim cntSearchCol As Long

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        TxtAccount.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
        cmdsearch.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
        Call ShowStatus(True)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONChq(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONChq(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportONChq(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String
        Dim mRptFileName As String

        Exit Sub
        '    Report1.Reset					
        '    MainClass.ClearCRptFormulas Report1					
        '					
        '    SqlStr = "SEND CHEQUE TO HO"					
        '    mSubTitle = "Send Date : " & vb6.Format(txtSendDate.Text, "DD/MM/YYYY")					
        '    Call MainClass.ClearCRptFormulas(Report1)					
        '					
        '    SqlStr = MakeSQL					
        '    mRptFileName = "SENDCHQ_PRN.rpt"					
        '					
        '    Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)					

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart

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

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String
        Dim mMkey As String
        Dim mChqNo As String
        Dim mUpdateCount As Integer
        Dim mRow As UltraGridRow
        Dim mMaxRow As Long
        Dim mPostFlag As String
        Dim mCompanyCode As Integer


        If FieldsVerification() = False Then Exit Sub

        mMaxRow = UltraGrid1.Rows.Count
        If Not IsDate(txtSendDate.Text) Then
            MsgBox("Invalid Date")
            txtSendDate.Focus()
            Exit Sub
        End If

        If cboList.SelectedIndex = 1 Then
            If MsgQuestion("Are you sure to Unclear Clearing Date?") = CStr(MsgBoxResult.No) Then
                Exit Sub
            End If
        End If
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0
        With UltraGrid1
            For cntRow = 0 To mMaxRow - 1

                mRow = Me.UltraGrid1.Rows(cntRow)
                mCompanyCode = CInt(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCompanyCode - 1)))

                mMkey = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1))
                mChqNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColChqNo - 1))

                mPostFlag = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColPostStatus - 1))

                If UCase(mPostFlag) = "TRUE" And cboList.SelectedIndex = 0 Then

                    SqlStr = "UPDATE FIN_VOUCHER_DET " & vbCrLf _
                            & " SET CLEARDATE=TO_DATE('" & VB6.Format(txtSendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                            & " WHERE MKEY='" & mMkey & "'" '' AND CHEQUENO='" & mChqNo & "'					

                    PubDBCn.Execute(SqlStr)

                    SqlStr = "UPDATE FIN_POSTED_TRN " & vbCrLf _
                            & " SET CLEARDATE=TO_DATE('" & VB6.Format(txtSendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & mCompanyCode & " AND MKEY='" & mMkey & "'" '' AND CHEQUENO='" & mChqNo & "'					

                    PubDBCn.Execute(SqlStr)

                    SqlStr = "UPDATE FIN_VOUCHER_HDR " & vbCrLf _
                            & " SET UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                            & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                            & " WHERE MKEY='" & mMkey & "'" & vbCrLf & " AND COMPANY_CODE=" & mCompanyCode & ""

                    PubDBCn.Execute(SqlStr)
                    mUpdateCount = mUpdateCount + 1
                ElseIf UCase(mPostFlag) = "FALSE" And cboList.SelectedIndex = 1 Then

                    SqlStr = "UPDATE FIN_VOUCHER_DET " & vbCrLf _
                            & " SET CLEARDATE=''" & vbCrLf _
                            & " WHERE MKEY='" & mMkey & "'" '' AND CHEQUENO='" & mChqNo & "'					

                    PubDBCn.Execute(SqlStr)

                    SqlStr = "UPDATE FIN_POSTED_TRN " & vbCrLf _
                            & " SET CLEARDATE=''" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & mCompanyCode & " AND MKEY='" & mMkey & "'" '' AND CHEQUENO='" & mChqNo & "'					

                    PubDBCn.Execute(SqlStr)

                    SqlStr = "UPDATE FIN_VOUCHER_HDR " & vbCrLf _
                            & " SET UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                            & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                            & " WHERE MKEY='" & mMkey & "'" & vbCrLf & " AND COMPANY_CODE=" & mCompanyCode & ""

                    PubDBCn.Execute(SqlStr)
                    mUpdateCount = mUpdateCount + 1
                End If



            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " Cheques Cleared.", MsgBoxStyle.Information)

        Show1("S")
        'FormatSprdMain()
        Call ShowStatus(True)
        Exit Sub
ErrPart:
        ''Resume					
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
    End Sub
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        'If FieldsVerification() = False Then Exit Sub
        'MainClass.ClearGrid(SprdMain, RowHeight)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) = "" Then
            MsgInformation("Please select the Account Name.")
            Exit Sub
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(Trim(TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                Exit Sub
            End If
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtBankName.Text), "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = False Then
            Exit Sub
        End If

        OptSelection(1).Checked = True
        Show1("S")

        'FormatSprdMain()

        Call ShowStatus(False)
    End Sub
    Private Sub ShowStatus(ByRef pPrintEnable As Object)
        cmdShow.Enabled = True ''pPrintEnable					
        CmdPreview.Enabled = True ''pPrintEnable					
        cmdPrint.Enabled = True '' pPrintEnable					

        'If VB.Left(XRIGHT, 1) = "A" Then
        CmdSave.Enabled = Not pPrintEnable
        'Else
        '    CmdSave.Enabled = Not pPrintEnable '' False					
        'End If
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.SetFocus: Exit Function					
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus					

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) = "" Then
            MsgInformation("Please select the Account Name.")
            FieldsVerification = False
            Exit Function
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(Trim(TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                FieldsVerification = False
                Exit Function
            End If
        End If

        '    If chkAllBank.Value = vbUnchecked And Trim(txtBankName.Text) = "" Then					
        '        MsgInformation "Please select the Bank Name."					
        '        FieldsVerification = False					
        '        Exit Function					
        '    End If					

        '    If chkAllBank.Value = vbUnchecked Then					
        If MainClass.ValidateWithMasterTable(Trim(txtBankName.Text), "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = False Then
            FieldsVerification = False
            Exit Function
        End If
        '    End If					


        Dim cntRow As Integer
        Dim mChqDate As String
        Dim mRow As UltraGridRow
        Dim mMaxRow As Long
        Dim mPostFlag As String
        Dim mCompanyCode As Integer

        mMaxRow = UltraGrid1.Rows.Count

        If cboList.SelectedIndex = 0 Then
            With UltraGrid1
                For cntRow = 0 To mMaxRow - 1

                    mRow = Me.UltraGrid1.Rows(cntRow)
                    mCompanyCode = CInt(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCompanyCode - 1)))


                    mChqDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColChqDate - 1))

                    mPostFlag = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColPostStatus - 1))

                    If UCase(mPostFlag) = "TRUE" Then
                        If IsDate(mChqDate) Then
                            If CDate(mChqDate) > CDate(txtSendDate.Text) Then
                                MsgInformation("Clear date cann't be less than Chq Date. Line No : " & cntRow + 1)
                                FieldsVerification = False
                                Exit Function
                            End If
                        End If
                    End If
                Next
            End With
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmParamChqClearing_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamChqClearing_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection					
        'PvtDBCn.Open StrConn					
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(12765)

        TxtAccount.Enabled = False
        cmdsearch.Enabled = False
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked

        txtBankName.Enabled = True
        cmdsearchBank.Enabled = True

        txtSendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        Call FillComboBox()

        'FormatSprdMain()
        Show1("L")
        cmdShow.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillComboBox()
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RS As ADODB.Recordset


        cboUnit.Items.Clear()
        SqlStr = "SELECT COMPANY_NAME FROM GEN_COMPANY_MST WHERE STATUS='O' ORDER BY COMPANY_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboUnit.Items.Add("ALL")
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboUnit.Items.Add(RS.Fields("COMPANY_NAME").Value)
                RS.MoveNext()
            Loop
        End If

        cboUnit.Text = RsCompany.Fields("COMPANY_NAME").Value

        cboShow.Items.Clear()
        cboShow.Items.Add("ALL")
        cboShow.Items.Add("Without Reversal Voucher")
        cboShow.Items.Add("Only Reversal Voucher")
        cboShow.SelectedIndex = 0

        cboList.Items.Clear()
        cboList.Items.Add("Pending")
        cboList.Items.Add("Completed")
        cboList.SelectedIndex = 0

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1(pShowType As String)
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)



        SqlStr = MakeSQL(pShowType)


        'MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        ClearGroupFromUltraGrid(UltraGrid1)
        ClearFilterFromUltraGrid(UltraGrid1)

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        Me.UltraGrid1.DataSource = ds
        Me.UltraGrid1.DataMember = ""

        CreateGridHeader()


        oledbAdapter.Dispose()
        oledbCnn.Close()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub

    Private Function MakeSQL(pShowType As String) As String
        On Error GoTo ErrPart
        Dim mBankCode As String
        Dim xAccountCode As String


        If MainClass.ValidateWithMasterTable(txtBankName.Text, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAccountCode = MasterNo
        Else
            xAccountCode = "-1"
        End If

        MakeSQL = "SELECT IH.MKEY, IH.COMPANY_CODE, IH.BOOKSUBTYPE, " & vbCrLf _
            & "IH.VNO, IH.VDATE AS VDATE, ID.CHEQUENO, ID.CHQDATE AS CHQDATE,  " & vbCrLf _
            & " CMST.SUPP_CUST_NAME, DECODE(ID.DC,'D',1,-1) * ID.AMOUNT, IH.VDATE, ID.PARTICULARS, CASE WHEN CLEARDATE IS NOT NULL THEN 'True' ELSE 'False' END AS PostStatus " & vbCrLf _
            & " FROM FIN_VOUCHER_HDR IH, FIN_VOUCHER_DET ID, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST CCMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=CCMST.COMPANY_CODE AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.Company_Code=CMST.Company_Code " & vbCrLf _
            & " AND ID.ACCOUNTCODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.BOOKTYPE IN ('" & ConBankBook & "') AND IH.CANCELLED='N'" '','" & ConPDCBook & "'					

        If cboUnit.SelectedIndex = 0 Then

        Else
            MakeSQL = MakeSQL & vbCrLf & " AND CCMST.COMPANY_NAME ='" & cboUnit.Text & "'"
        End If

        If OptType(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.BOOKSUBTYPE ='P'"
        ElseIf OptType(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.BOOKSUBTYPE ='R'"
        Else
            MakeSQL = MakeSQL & vbCrLf & " AND IH.BOOKSUBTYPE IN ('P','R')"
        End If


        If cboList.SelectedIndex = 0 Then
            MakeSQL = MakeSQL & vbCrLf & " AND (CLEARDATE='' OR CLEARDATE IS NULL OR CLEARDATE=TO_DATE('" & VB6.Format(txtSendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        ElseIf cboList.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & " AND CLEARDATE IS NOT NULL"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & " AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND IH.BOOKCODE='" & MainClass.AllowSingleQuote(xAccountCode) & "'"

        If cboShow.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & " AND IS_REVERSAL_MADE='N' AND IS_REVERSAL_VOUCHER='N'"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & " AND (IS_REVERSAL_MADE='Y' OR IS_REVERSAL_VOUCHER='Y')"
        End If



        If pShowType = "L" Then
            MakeSQL = MakeSQL & " AND 1=2"
        End If

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY  ID.CHQDATE, ID.CHEQUENO, IH.VDATE, CMST.SUPP_CUST_NAME"

        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function
    'Private Sub FormatSprdMain()
    '    With SprdMain
    '        .MaxCols = ColPostStatus
    '        .set_RowHeight(0, RowHeight * 1.5)
    '        .set_ColWidth(0, 4.5)

    '        .set_RowHeight(-1, RowHeight)
    '        .Row = -1

    '        .Col = ColMKEY
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
    '        .set_ColWidth(ColMKEY, 11)
    '        .ColHidden = True

    '        .Col = ColCompanyCode
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
    '        .set_ColWidth(ColCompanyCode, 11)
    '        .ColHidden = True

    '        .Col = colType
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
    '        .set_ColWidth(colType, 11)
    '        .ColHidden = True

    '        .Col = ColVNo
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .set_ColWidth(ColVNo, 14)

    '        .Col = ColVDate
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .set_ColWidth(ColVDate, 9)

    '        .Col = ColOrderBy
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .set_ColWidth(ColOrderBy, 9)
    '        .ColHidden = True

    '        .Col = ColChqNo
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
    '        .set_ColWidth(ColChqNo, 10)

    '        .Col = ColChqDate
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .set_ColWidth(ColChqDate, 9)

    '        .Col = ColPartyName
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColPartyName, 42)

    '        .Col = ColAmount
    '        .CellType = SS_CELL_TYPE_FLOAT
    '        .TypeFloatDecimalPlaces = 2
    '        .TypeFloatMin = CDbl("-99999999999")
    '        .TypeFloatMax = CDbl("99999999999")
    '        .TypeFloatMoney = False
    '        .TypeFloatSeparator = False
    '        .TypeFloatDecimalChar = Asc(".")
    '        .TypeFloatSepChar = Asc(",")
    '        .set_ColWidth(ColAmount, 12)

    '        .Col = ColPostStatus
    '        .CellType = SS_CELL_TYPE_CHECKBOX
    '        .TypeHAlign = SS_CELL_H_ALIGN_CENTER
    '        .set_ColWidth(ColPostStatus, 8)
    '        '    .Value = vbUnchecked					

    '        MainClass.SetSpreadColor(SprdMain, -1)
    '        MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColPartyName)
    '        '    SprdMain.OperationMode = OperationModeSingle					
    '        '    SprdMain.DAutoCellTypes = True					
    '        '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH					
    '        '    SprdMain.GridColor = &HC00000					
    '    End With
    '    WriteColHeadings()
    'End Sub
    'Private Sub WriteColHeadings()
    '    With SprdMain
    '        .Row = 0

    '        .Col = ColMKEY
    '        .Text = "MKey"

    '        .Col = ColCompanyCode
    '        .Text = "Company Code"

    '        .Col = colType
    '        .Text = "Type"

    '        .Col = ColVNo
    '        .Text = "Voucher No."

    '        .Col = ColVDate
    '        .Text = "Voucher Date"

    '        .Col = ColOrderBy
    '        .Text = "Order By Date"

    '        .Col = ColChqNo
    '        .Text = "Cheque No."

    '        .Col = ColChqDate
    '        .Text = "Cheque Date"

    '        .Col = ColPartyName
    '        .Text = "Supplier Name"

    '        .Col = ColAmount
    '        .Text = "Amount (in Rs.)"

    '        .Col = ColPostStatus
    '        .Text = "Post Status"
    '    End With
    'End Sub
    Private Sub frmParamChqClearing_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer

            For inti = 0 To UltraGrid1.Rows.Count - 1
                UltraGrid1.Rows(inti).Cells(ColPostStatus - 1).Value = IIf(Index = 0, True, False)
            Next

            'Call ShowStatus(True)

        End If
    End Sub
    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent)
        '    Call ShowStatus(True)					
    End Sub

    'Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent)
    '    SprdMain.Row = -1
    '    SprdMain.Col = eventArgs.col
    '    SprdMain.DAutoCellTypes = True
    '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
    '    SprdMain.TypeEditLen = 1000
    'End Sub

    'Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
    '    Dim SqlStr As String
    '    Dim xVDate As String
    '    Dim xMKey As String
    '    Dim xVNo As String
    '    Dim xBookType As String
    '    Dim xBookSubType As String
    '    Dim pIndex As Integer
    '    Dim xVTYPE As String
    '    Dim xCompanyCode As Long


    '    SprdMain.Row = SprdMain.ActiveRow

    '    SprdMain.Col = ColCompanyCode
    '    xCompanyCode = Me.SprdMain.Text

    '    If xCompanyCode <> RsCompany.Fields("COMPANY_CODE").Value Then
    '        Exit Sub
    '    End If
    '    SprdMain.Col = ColVDate
    '    xVDate = Me.SprdMain.Text

    '    SprdMain.Col = ColMKEY
    '    xMKey = Me.SprdMain.Text

    '    SprdMain.Col = ColVNo
    '    xVNo = Me.SprdMain.Text


    '    xBookType = ConBankBook '' Me.SprdMain.Text					
    '    xBookSubType = IIf(OptType(0).Checked = True, "P", "R")

    '    If xBookType = "B" Or xBookType = "F" Or xBookType = "C" Or xBookType = "J" Or xBookType = "R" Or xBookType = "E" Then
    '        '            xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)					
    '        '            xVNo = Right(xVNo, 5)					
    '        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    '            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
    '            & " AND MKEY='" & xMKey & "'" & vbCrLf & " AND BOOKTYPE='" & xBookType & "'" & vbCrLf _
    '            & " AND BOOKSUBTYPE='" & xBookSubType & "'" & vbCrLf _
    '            & " AND VDATE=TO_DATE('" & VB6.Format(xVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

    '        If MainClass.ValidateWithMasterTable(xVNo, "VNO", "VTYPE", "FIN_POSTED_TRN", PubDBCn, MasterNo,  , SqlStr) = True Then
    '            xVTYPE = MasterNo
    '            xVNo = Mid(xVNo, Len(xVTYPE) + 1)
    '        Else
    '            Exit Sub
    '        End If
    '    End If

    '    Call ShowTrn(xMKey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType, frmAtrn)
    'End Sub
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xVDate As String
        Dim xMKey As String
        Dim xVNo As String
        Dim xBookType As String
        Dim xBookSubType As String
        Dim pIndex As Integer
        Dim xVTYPE As String
        Dim xCompanyCode As Integer

        Dim mRow As UltraGridRow

        Exit Sub

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        xCompanyCode = CInt(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCompanyCode - 1)))


        If xCompanyCode <> RsCompany.Fields("COMPANY_CODE").Value Then
            Exit Sub
        End If

        xVDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVDate - 1))
        xMKey = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1))
        xVNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVNo - 1))

        xBookType = ConBankBook '' Me.SprdMain.Text					
        xBookSubType = IIf(OptType(0).Checked = True, "P", "R")

        If xBookType = "B" Or xBookType = "F" Or xBookType = "C" Or xBookType = "J" Or xBookType = "R" Or xBookType = "E" Then
            '            xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)					
            '            xVNo = Right(xVNo, 5)					
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND MKEY='" & xMKey & "'" & vbCrLf & " AND BOOKTYPE='" & xBookType & "'" & vbCrLf _
                & " AND BOOKSUBTYPE='" & xBookSubType & "'" & vbCrLf _
                & " AND VDATE=TO_DATE('" & VB6.Format(xVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            If MainClass.ValidateWithMasterTable(xVNo, "VNO", "VTYPE", "FIN_POSTED_TRN", PubDBCn, MasterNo,  , SqlStr) = True Then
                xVTYPE = MasterNo
                xVNo = Mid(xVNo, Len(xVTYPE) + 1)
            Else
                Exit Sub
            End If
        End If

        Call ShowTrn(xMKey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType, frmAtrn)
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call ShowStatus(True)
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

        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "SELECT SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST Where " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'" & vbCrLf & " AND (SUPP_CUST_TYPE='C' OR SUPP_CUST_TYPE='S')"

        SqlStr = SqlStr & " ORDER BY SUPP_CUST_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If Not RsACM.EOF = False Then
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Private Sub SearchAccounts()
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_TYPE='C' OR SUPP_CUST_TYPE='S')"

        MainClass.SearchMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_Name", SqlStr)

        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub SearchBankName()
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_TYPE='2')"

        MainClass.SearchMaster(txtBankName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_Name", SqlStr)

        If AcName <> "" Then
            txtBankName.Text = AcName
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub txtBankName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.TextChanged
        Call ShowStatus(True)
    End Sub

    Private Sub txtBankName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.DoubleClick
        SearchBankName()
    End Sub

    Private Sub txtBankName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtBankName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBankName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchBankName()
    End Sub

    Private Sub txtBankName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBankName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        Dim RsACM As ADODB.Recordset

        On Error GoTo ERR1

        If txtBankName.Text = "" Then GoTo EventExitSub

        SqlStr = "SELECT SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST Where " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(txtBankName.Text)) & "'" & vbCrLf & " AND (SUPP_CUST_TYPE='2')"

        SqlStr = SqlStr & " ORDER BY SUPP_CUST_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If Not RsACM.EOF = False Then
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSendDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSendDate.TextChanged
        Call ShowStatus(True)
        'CmdSave.Enabled = True
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

    Private Sub cmdsearchBank_Click(sender As Object, e As EventArgs) Handles cmdsearchBank.Click
        SearchBankName()
    End Sub

    'Private Sub frmParamChqClearing_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
    '    Dim KeyAscii As Short = Asc(e.KeyChar)

    '    If KeyAscii = 6 Then
    '        SprdMain.Row = 1
    '        SprdMain.Row2 = SprdMain.MaxRows
    '        SprdMain.Col = 1
    '        SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
    '        SprdMain.BlockMode = True
    '        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
    '        SprdMain.BlockMode = False

    '        mSearchKey = ""
    '        cntSearchRow = 1
    '        cntSearchCol = 1
    '        mSearchKey = InputBox("Search :", "Search", mSearchKey)
    '        If MainClass.SearchIntoFullGrid(SprdMain, ColVNo, mSearchKey, cntSearchRow, cntSearchCol) = True Then

    '            SprdMain.Row = cntSearchRow
    '            SprdMain.Row2 = cntSearchRow
    '            SprdMain.Col = 1
    '            SprdMain.Col2 = SprdMain.MaxCols
    '            SprdMain.BlockMode = True
    '            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
    '            SprdMain.BlockMode = False

    '            MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColVNo)
    '            cntSearchRow = cntSearchRow + 1
    '            cntSearchCol = cntSearchCol + 1
    '        End If
    '    End If
    'End Sub

    'Private Sub SprdMain_KeyPressEvent(sender As Object, e As _DSpreadEvents_KeyPressEvent)
    '    'Dim KeyAscii As Short = Asc(e.keyAscii)

    '    'KeyAscii = MainClass.SetNumericField(KeyAscii)
    '    'EventArgs.KeyChar = Chr(KeyAscii)
    '    'If KeyAscii = 67 Then
    '    '    EventArgs.Handled = True
    '    'End If

    '    If e.keyAscii = 6 Then
    '        SprdMain.Row = 1
    '        SprdMain.Row2 = SprdMain.MaxRows
    '        SprdMain.Col = 1
    '        SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
    '        SprdMain.BlockMode = True
    '        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
    '        SprdMain.BlockMode = False

    '        mSearchKey = ""
    '        cntSearchRow = 1
    '        cntSearchCol = 1
    '        mSearchKey = InputBox("Search :", "Search", mSearchKey)
    '        If MainClass.SearchIntoFullGrid(SprdMain, ColVNo, mSearchKey, cntSearchRow, cntSearchCol) = True Then

    '            SprdMain.Row = cntSearchRow
    '            SprdMain.Row2 = cntSearchRow
    '            SprdMain.Col = 1
    '            SprdMain.Col2 = SprdMain.MaxCols
    '            SprdMain.BlockMode = True
    '            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
    '            SprdMain.BlockMode = False

    '            MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColVNo)
    '            cntSearchRow = cntSearchRow + 1
    '            cntSearchCol = cntSearchCol + 1
    '        End If
    '    End If
    'End Sub

    'Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent)
    '    Dim mCol As Short
    '    mCol = SprdMain.ActiveCol
    '    If eventArgs.keyCode = System.Windows.Forms.Keys.F3 And mSearchKey <> "" Then
    '        If MainClass.SearchIntoFullGrid(SprdMain, ColVNo, mSearchKey, cntSearchRow, cntSearchCol) = True Then

    '            SprdMain.Row = cntSearchRow
    '            SprdMain.Row2 = cntSearchRow
    '            SprdMain.Col = 1
    '            SprdMain.Col2 = SprdMain.MaxCols
    '            SprdMain.BlockMode = True
    '            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
    '            SprdMain.BlockMode = False

    '            MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColVNo)
    '            cntSearchRow = cntSearchRow + 1
    '            cntSearchCol = cntSearchCol + 1
    '        End If
    '    End If

    '    SprdMain.Refresh()
    'End Sub
    Private Sub CreateGridHeader()
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   to create the grid header
        'Comments       :   Nil
        '----------------------------------------------------------------------------
        Try
            Dim inti As Integer
            'create column header


            UltraGrid1.DisplayLayout.Bands(0).Columns(0).RowLayoutColumnInfo.PreferredLabelSize = New System.Drawing.Size(0, 40)
            UltraGrid1.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True

            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Key = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "MKey"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Company Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Voucher No."
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "Voucher Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "Cheque No."
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Cheque Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "Supplier Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "Amount (in Rs.)"
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Header.Caption = "Order By"
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Header.Caption = "Narration"

            If cboList.SelectedIndex = 0 Then
                UltraGrid1.DisplayLayout.Bands(0).Columns(11).Header.Caption = "Post Status"
            Else
                UltraGrid1.DisplayLayout.Bands(0).Columns(11).Header.Caption = "UnPost Status"
            End If

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPostStatus - 1).Style = UltraWinGrid.ColumnStyle.CheckBox
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmount - 1).Style = UltraWinGrid.ColumnStyle.Double

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmount - 1).CellAppearance.TextHAlign = HAlign.Right

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center
            Next

            'For inti = 0 To UltraGrid1.Rows.Count - 1
            '    UltraGrid1.Rows(inti).Cells(ColPostStatus - 1).Value = False
            'Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPostStatus - 1).CellActivation = Activation.AllowEdit
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Hidden = True

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCompanyCode - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(colType - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOrderBy - 1).Hidden = True

            'col = Me.UltraGrid1.DisplayLayout.Bands(0).Columns(intLoop + 1)
            'strCelltype = col.Style

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCompanyCode - 1).Width = 70
            UltraGrid1.DisplayLayout.Bands(0).Columns(colType - 1).Width = 0
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVNo - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVDate - 1).Width = 70
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColChqNo - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColChqDate - 1).Width = 70
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyName - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmount - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOrderBy - 1).Width = 50
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNarration - 1).Width = 150
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPostStatus - 1).Width = 80



            'UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.MFGQty).MaskInput = "99999"

            'Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            'Me.UltraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")

            'fill labels 
            'FillLabelsFromResFile(Me)
            'Catch sqlex As SqlException
            '    ErrorTrap(sqlex.Message, "frmRMReturn.vb", "CreateHeader", "", "", "Sql Exception")
            '    Me.Cursor = Windows.Forms.Cursors.Default
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub frmParamChqClearing_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(UltraGrid1, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click

        Dim lngLoop As Integer  'loop variable

        Dim objMode As Object 'to store the mode of the row
        Dim objChk As Object 'to get the check status of the first column
        Dim strSplit() As String 'split variable
        Dim intAns As Integer ' to store the result from msgbox
        Dim lngRow As Long
        Try


            'If m_blnChangeInData = True Then
            '    MessageFromResFile(7284, MessageType.Information)
            '    GridSetFocus(UltraGrid1.ActiveRow.Tag.ToString, UltraGrid1.ActiveCell.Column.Index)
            '    Exit Sub
            'End If

            ''Please provide a location where you whould like to export the data to
            'MessageFromResFile(7304, MessageType.Information, GetLabelDes("7305"))

            Try
                SaveFileDialog1.FileName = Me.Text
            Catch
                SaveFileDialog1.FileName = "File1"
            End Try

            Dim strAction As String = ""
            Try
                strAction = SaveFileDialog1.ShowDialog()
            Catch
                SaveFileDialog1.FileName = "File1"
                strAction = SaveFileDialog1.ShowDialog()
            End Try

            If strAction = "1" Then
                ExportToExcel(SaveFileDialog1.FileName)
            End If
            'Me.Cursor = Cursors.Default

        Catch ex As Exception
            ErrorMsg(Err.Description, Err.Number)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ExportToExcel(ByVal strFileName As String)
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   To export the report to excel/csv/text file
        'Comments       :   This function will be called from clickinvoked and enterpressed events
        '                   THIS FUNCTION HAS TO BE OVERRIDED IN THE DERIVED FORM   
        '----------------------------------------------------------------------------
        Me.Cursor = Cursors.WaitCursor
        Dim start As DateTime
        'Dim timespan As TimeSpan
        start = DateTime.Now
        Try
            Me.UltraGridExcelExporter1.FileLimitBehaviour = ExcelExport.FileLimitBehaviour.TruncateData
            Me.UltraGridExcelExporter1.ExportAsync(Me.UltraGrid1, strFileName & ".xls")
            ' timespan = DateTime.Now.Subtract(start)
            'Exported To File : 
            '  MessageFromResFile(7292, MessageType.Information, strFileName)
        Catch
            'Specified Path Does Not Exist,Invalid File Name
            ErrorMsg(Err.Description, Err.Number)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
End Class
