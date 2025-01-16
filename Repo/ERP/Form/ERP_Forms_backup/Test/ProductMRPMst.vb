Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Friend Class frmProductMRPMst
    Inherits System.Windows.Forms.Form
    Dim RsMRPMain As ADODB.Recordset ''ADODB.Recordset
    Dim RsMRPDetail As ADODB.Recordset ''ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection
    Dim FileDBCn As ADODB.Connection
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean


    Private Const ConRowHeight As Short = 14

    Private Const ColProdCode As Short = 1
    Private Const ColProdDesc As Short = 2
    Private Const ColProdPartNo As Short = 3
    Private Const ColProdUOM As Short = 4
    Private Const ColPrevRate As Short = 5
    Private Const ColPrevRateDisc As Short = 6
    Private Const ColCurrRate As Short = 7
    Private Const ColCurrRateDisc As Short = 8
    Private Const ColRateAftAbatement As Short = 9
    Private Const ColDiff As Short = 10
    Private Const ColOEMRate As Short = 11

    Dim mAmendStatus As Boolean
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            CmdAdd.Text = ConCmdCancelCaption
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtWEF.Focus()
            SprdMain.Enabled = True
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub
    Private Sub cmdAmend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAmend.Click
        Dim mCustName As String
        Dim I As Integer

        txtWEF.Text = ""


        txtWEF.Enabled = True
        cmdAmend.Enabled = False

        ADDMode = True
        MODIFYMode = False
        SprdMain.Enabled = True

        cmdSearchAmend.Enabled = False
        MainClass.ButtonStatus(Me, XRIGHT, RsMRPMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        Dim SqlStr As String
        Dim mMaxWEF As String

        If txtWEF.Text = "" Then Call ErrorMsg("Nothing to Delete", "", MsgBoxStyle.Critical) : Exit Sub

        mMaxWEF = GetMaxWEF()

        If CDate(txtWEF.Text) < CDate(mMaxWEF) Then
            MsgInformation("Cann't be deleted old WEF MRP.")
            Exit Sub
        End If
        If Not RsMRPMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "INV_ITEM_MRP_HDR", (txtWEF.Text), RsMRPMain, "WEF_DATE") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_ITEM_MRP_HDR", "WEF_DATE", VB6.Format(txtWEF.Text, "DD-MMM-YYYY")) = False Then GoTo DelErrPart

                SqlStr = " DELETE From INV_ITEM_MRP_DET WHERE " & vbCrLf _
                    & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
                    & " AND WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                PubDBCn.Execute(SqlStr)

                SqlStr = " DELETE From INV_ITEM_MRP_HDR WHERE " & vbCrLf _
                    & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
                    & " AND WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                PubDBCn.Execute(SqlStr)

                PubDBCn.CommitTrans()
                RsMRPMain.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        ''Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''
        RsMRPMain.Requery() ''.Refresh
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            SprdMain.Enabled = True
            MainClass.ButtonStatus(Me, XRIGHT, RsMRPMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            '        SprdMain.Enabled = False
            Show1()
        End If
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String
        Dim mRptFileName As String


        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "MRP Rate List- W.E.F. From" & VB6.Format(txtWEF.Text, "DD/MM/YYYY")
        mSubTitle = IIf(chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, txtCategory.Text, "")

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows - 1, 1, ColOEMRate, PubDBCn) = False Then GoTo ERR1
        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        mRptFileName = "MRPRATELIST.rpt"

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
        Report1.ReportFileName = ""

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        If FieldVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            TxtWef_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xCompanyCode As Long
        Dim SqlStr As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        'If UpdateMain1() = False Then GoTo UpdateError


        If CheckConsolidatedMaster("INV_ITEM_MRP_HDR") = True Then
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        Else
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                If UpdateMain1(xCompanyCode) = False Then GoTo UpdateError
                RsTemp.MoveNext()
            Loop
        End If


        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''
        Update1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateMain1(xCompanyCode As Long) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String

        If ADDMode = True Then

            SqlStr = ""
            SqlStr = " INSERT INTO INV_ITEM_MRP_HDR ( " & vbCrLf _
                & " COMPANY_CODE, WEF_DATE, REMARKS, ABATEMENT_PER, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE) VALUES ( "


            SqlStr = SqlStr & vbCrLf _
                & " " & xCompanyCode & ", TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "'," & vbCrLf & " " & Val(txtAbatementPer.Text) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

        End If

        If MODIFYMode = True Then
            SqlStr = ""
            SqlStr = " UPDATE INV_ITEM_MRP_HDR SET  " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " ABATEMENT_PER=" & Val(txtAbatementPer.Text) & "," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & xCompanyCode & " " & vbCrLf _
                & " AND WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        End If
        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(xCompanyCode) = False Then GoTo ErrPart
        UpdateMain1 = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateMain1 = False
        'Resume
    End Function
    Private Function UpdateDetail1(xCompanyCode As Long) As Boolean
        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim I As Integer
        Dim mProdCode As String
        Dim mRate As Double
        Dim mRateAfterDisc As Double
        Dim mRateOEM As Double
        Dim mRateAfterAbate As Double
        Dim mRateDisc As Double

        SqlStr = "DELETE FROM  INV_ITEM_MRP_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & xCompanyCode & "" & vbCrLf _
            & " AND WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColProdCode
                mProdCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColCurrRate
                mRate = Val(.Text)

                .Col = ColCurrRateDisc
                mRateDisc = Val(.Text)

                .Col = ColRateAftAbatement
                mRateAfterAbate = Val(.Text)

                mRateAfterDisc = mRate - (mRate * mRateDisc * 0.01)

                .Col = ColOEMRate
                mRateOEM = Val(.Text)

                SqlStr = ""

                If mProdCode <> "" Then
                    SqlStr = " INSERT INTO INV_ITEM_MRP_DET ( " & vbCrLf _
                        & " COMPANY_CODE , WEF_DATE, " & vbCrLf _
                        & " ITEM_CODE, RATE, RATE_DISC, RATE_OEM, RATE_AFTER_ABATE) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " '" & xCompanyCode & "', TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " '" & mProdCode & "', " & mRate & ",  " & mRateDisc & ", " & mRateOEM & ", " & mRateAfterAbate & ") "

                    PubDBCn.Execute(SqlStr)

                    If mRate <> 0 Then
                        SqlStr = " UPDATE INV_ITEM_MST SET  " & vbCrLf _
                            & " ITEM_STD_COST=" & mRateAfterDisc & ", " & vbCrLf _
                            & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                            & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                            & " WHERE COMPANY_CODE=" & xCompanyCode & " " & vbCrLf _
                            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProdCode) & "'"

                        PubDBCn.Execute(SqlStr)

                        'SqlStr = " UPDATE FIN_SUPP_CUST_DET SET  " & vbCrLf _
                        '    & " ITEM_MRP=" & mRate & " " & vbCrLf _
                        '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                        '    & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProdCode) & "'"

                        'PubDBCn.Execute(SqlStr)

                        'SqlStr = " UPDATE FIN_SUPP_CUST_HDR SET  " & vbCrLf _
                        '    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                        '    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                        '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                        '    & " AND SUPP_CUST_CODE IN (" & vbCrLf _
                        '    & " SELECT DISTINCT SUPP_CUST_CODE FROM FIN_SUPP_CUST_DET" & vbCrLf _
                        '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                        '    & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProdCode) & "')"

                        'PubDBCn.Execute(SqlStr)
                    End If
                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Sub cmdSearchAmend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAmend.Click
        Dim SqlStr As String

        SqlStr = "SELECT " & vbCrLf _
            & " DISTINCT TO_CHAR(WEF_DATE,'DD/MM/YYYY') AS WEF" & vbCrLf _
            & " FROM INV_ITEM_MRP_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchBySQL(SqlStr, "WEF") = True Then
            txtWEF.Text = AcName
            txtWEF.Text = VB6.Format(txtWEF.Text, "DD/MM/YYYY")
            TxtWef_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(True))
        End If
        Exit Sub
    End Sub

    Private Sub cmdSubCatsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSubCatsearch.Click
        SearchSubCategory()
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
        MainClass.ButtonStatus(Me, XRIGHT, RsMRPMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmProductMRPMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = "Select * From INV_ITEM_MRP_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRPMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From INV_ITEM_MRP_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRPDetail, ADODB.LockTypeEnum.adLockReadOnly)


        AssignGrid(False)

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

        SqlStr = " SELECT IH.WEF_DATE" & vbCrLf _
            & " FROM INV_ITEM_MRP_HDR IH " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & " ORDER BY IH.WEF_DATE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmProductMRPMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        '    Call SetMainFormCordinate(Me)

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Left = 0
        Me.Top = 0
        'Me.Width = VB6.TwipsToPixelsX(11355)
        'Me.Height = VB6.TwipsToPixelsY(7245)

        chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        txtCategory.Enabled = False
        cmdsearchCategory.Enabled = False

        chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked
        txtSubCategory.Enabled = False
        cmdSubCatsearch.Enabled = False


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


        txtWEF.Text = ""
        txtRemarks.Text = ""
        txtAbatementPer.Text = "0"
        mAmendStatus = False

        txtWEF.Enabled = True

        SprdMain.Enabled = True

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsMRPMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '''Resume
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim cntCol As Integer

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColProdCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMRPDetail.Fields("ITEM_CODE").DefinedSize ''
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 9)

            .Col = ColProdDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 32)

            .Col = ColProdPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = MainClass.SetMaxLength("CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 25)

            .Col = ColProdUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = MainClass.SetMaxLength("ISSUE_UOM", "INV_ITEM_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 4)

            For cntCol = ColPrevRate To ColOEMRate
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 9)
            Next

            For cntCol = ColPrevRateDisc To ColPrevRateDisc
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99.99")
                .TypeFloatMin = CDbl("-99.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 9)
            Next

            For cntCol = ColCurrRateDisc To ColCurrRateDisc
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99.99")
                .TypeFloatMin = CDbl("-99.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 9)
            Next

            .Col = ColRateAftAbatement
            .ColHidden = True

            .Col = ColOEMRate
            .ColHidden = True

            MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColProdCode, ColDiff)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColProdCode, ColPrevRateDisc)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDiff, ColDiff)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRateAftAbatement, ColRateAftAbatement)
            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub FormatSprdView()
        With SprdView
            .Row = -1
            .set_RowHeight(0, 300)
            .set_ColWidth(0, 500)
            .set_ColWidth(1, 900)
            .set_ColWidth(2, 3500)
            .set_ColWidth(3, 2000)
            .set_ColWidth(4, 2000)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtWEF.MaxLength = 10
        txtRemarks.MaxLength = RsMRPMain.Fields("REMARKS").DefinedSize

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Function FieldVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mMaxWEF As String

        FieldVarification = True
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtWEF.Text) = "" Then
            MsgInformation("WEF Date is empty. Cannot Save")
            If txtWEF.Enabled = True Then txtWEF.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Not IsDate(txtWEF.Text) Then
            MsgInformation("Invalid WEF Date.")
            If txtWEF.Enabled = True Then txtWEF.Focus()
            FieldVarification = False
            Exit Function
        End If

        mMaxWEF = GetMaxWEF()

        If Trim(mMaxWEF) <> "" Then
            If CDate(txtWEF.Text) < CDate(mMaxWEF) Then
                MsgInformation("Cann't be Add old WEF MRP.")
                FieldVarification = False
                Exit Function
            End If
        End If
        If MainClass.ValidDataInGrid(SprdMain, ColProdCode, "S", "Item Code is must") = False Then FieldVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColProdDesc, "S", "Item Desc is must") = False Then FieldVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColDensity, "S", "Item UOM is must") = False Then FieldVarification = False: Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColCurrRate, "N", "Item Rate is must") = False Then FieldVarification = False: Exit Function


        Exit Function
err_Renamed:
        '    Resume
        MsgBox(Err.Description)
    End Function

    Private Sub frmProductMRPMst_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        fraAccounts.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        FraMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))

        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmProductMRPMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
        RsMRPMain.Close()
        'RsOpOuts.Close
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function CheckDuplicateItem(ByRef mGradeCode As String) As Boolean
        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

        If mGradeCode = "" Then CheckDuplicateItem = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColProdCode
                If UCase(.Text) = UCase(mGradeCode) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Grade Code")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColProdCode)
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


    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        Dim SqlStr As String

        Exit Sub

        If eventArgs.row = 0 And eventArgs.col = ColProdCode And (ADDMode = True Or MODIFYMode = True) Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColProdCode
                SqlStr = GetSearchItem("Y")
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColProdCode
                    .Text = Trim(AcName)
                    .Col = ColProdDesc
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColProdCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColProdDesc And (ADDMode = True Or MODIFYMode = True) Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColProdDesc
                SqlStr = GetSearchItem("N")
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColProdDesc
                    .Text = Trim(AcName)
                    .Col = ColProdCode
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColProdCode)
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColProdDesc)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Function GetSearchItem(ByRef mByCode As String) As String
        On Error GoTo ErrPart
        Dim mSqlStr As String

        If mByCode = "Y" Then
            mSqlStr = "SELECT A.ITEM_CODE,A.ITEM_SHORT_DESC, A.MTRL_TYPE "
        Else
            mSqlStr = "SELECT A.ITEM_SHORT_DESC,A.ITEM_CODE, A.MTRL_TYPE "
        End If


        mSqlStr = mSqlStr & vbCrLf _
            & " FROM INV_ITEM_MST A" & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If mByCode = "Y" Then
            mSqlStr = mSqlStr & vbCrLf & "ORDER BY A.ITEM_CODE "
        Else
            mSqlStr = mSqlStr & vbCrLf & "ORDER BY A.ITEM_SHORT_DESC"
        End If

        GetSearchItem = mSqlStr
        Exit Function
ErrPart:
        GetSearchItem = ""

    End Function



    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColProdCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColProdCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColProdDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColProdDesc, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        'Dim xICode As String
        Dim mPrevRate As Double
        Dim mCurrRate As Double
        Dim mDiff As Double
        Dim mPrevRateDisc As Double
        Dim mCurrRateDisc As Double

        If eventArgs.newRow = -1 Then Exit Sub



        Select Case eventArgs.col
            Case ColProdCode
                '            SprdMain.Row = SprdMain.ActiveRow
                '
                '            SprdMain.Col = ColProdCode
                '            xICode = SprdMain.Text
                '            If xICode = "" Then Exit Sub
                '
                '            If GetValidItem(xICode) = True Then
                '                If CheckDuplicateItem(xICode) = False Then
                '                    If FillGridRow(xICode) = False Then Exit Sub
                ''                    FormatSprdMain Row
                '    '                MainClass.SetFocusToCell SprdMain, Row, ColItemRate
                '                End If
                '            Else
                '                MainClass.SetFocusToCell SprdMain, Row, ColProdCode
                '            End If

            Case ColCurrRate, ColCurrRateDisc

                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColProdCode
                If Trim(SprdMain.Text) <> "" Then

                    SprdMain.Col = ColPrevRate
                    mPrevRate = Val(SprdMain.Text)

                    SprdMain.Col = ColPrevRateDisc
                    mPrevRateDisc = Val(SprdMain.Text)


                    SprdMain.Col = ColCurrRate
                    mCurrRate = Val(SprdMain.Text)


                    SprdMain.Col = ColCurrRateDisc
                    mCurrRateDisc = Val(SprdMain.Text)


                    SprdMain.Col = ColRateAftAbatement
                    SprdMain.Text = VB6.Format(mCurrRate - (mCurrRate * CDbl(txtAbatementPer.Text) * 0.01), "0.00")

                    SprdMain.Col = ColRateAftAbatement
                    SprdMain.Text = VB6.Format(mCurrRate - (mCurrRate * CDbl(txtAbatementPer.Text) * 0.01), "0.00")

                    SprdMain.Col = ColDiff
                    mDiff = (mCurrRate - (mCurrRate * mCurrRateDisc * 0.01)) - (mPrevRate - (mPrevRate * mPrevRateDisc * 0.01))
                    SprdMain.Text = CStr(mDiff)

                End If
                '            If CheckItemRate() = True Then
                '                MainClass.AddBlankSprdRow SprdMain, ColProdCode, ConRowHeight
                '                FormatSprdMain SprdMain.MaxRows
                '            End If
        End Select


        '    Call CalcGrid
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetValidItem(ByRef pItemCode As String) As Boolean
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset

        mSqlStr = "SELECT ITEM_CODE " & vbCrLf & " FROM INV_ITEM_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & pItemCode & "'"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetValidItem = True
        Else
            MsgInformation("Please Check RM Grade Code in Master.")
            GetValidItem = False
        End If

        Exit Function
ErrPart:
        GetValidItem = False
    End Function

    Private Function CheckItemRate() As Boolean
        On Error GoTo ERR1
        With SprdMain
            .Row = .ActiveRow
            .Col = ColProdCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColCurrRate
            If Val(.Text) > 0 Then
                CheckItemRate = True
                '        Else
                '            MsgInformation "Please Enter the Rate."
                '            MainClass.SetFocusToCell SprdMain, .ActiveRow, ColCurrRate
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FillGridRow(ByRef mItemCode As String) As Boolean
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String

        If mItemCode = "" Then Exit Function


        SqlStr = ""
        SqlStr = " Select ITEM_CODE,ITEM_SHORT_DESC,CUSTOMER_PART_NO,  ISSUE_UOM" & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & Trim(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc

                SprdMain.Col = ColProdDesc
                SprdMain.Text = IIf(IsDBNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)

                SprdMain.Col = ColProdUOM
                SprdMain.Text = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)

                SprdMain.Col = ColProdPartNo
                SprdMain.Text = IIf(IsDBNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)


            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColProdCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        If eventArgs.row < 1 Then Exit Sub

        SprdView.Row = eventArgs.row
        SprdView.Col = 1
        txtWEF.Text = VB6.Format(SprdView.Text, "DD/MM/YYYY")

        TxtWef_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(True))

        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    End Sub

    Private Sub txtAbatementPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAbatementPer.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAbatementPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAbatementPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtAbatementPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAbatementPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        'Call CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.DoubleClick
        SearchCategory()
    End Sub
    Private Sub txtCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCategory()
    End Sub
    Private Sub txtCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If txtCategory.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C' AND STOCKTYPE='FG'"

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
            txtCategory.Text = UCase(Trim(txtCategory.Text))
        Else
            MsgInformation("No Such Category in Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchCategory()
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C' AND STOCKTYPE='FG'"

        If MainClass.SearchGridMaster((txtCategory.Text), "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE",  ,  , SqlStr) = True Then
            If AcName <> "" Then
                txtCategory.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCategory.Click
        SearchCategory()
    End Sub
    Private Sub chkAllCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCategory.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCategory.Enabled = False
            cmdsearchCategory.Enabled = False
        Else
            txtCategory.Enabled = True
            cmdsearchCategory.Enabled = True
        End If
    End Sub

    Private Sub txtSubCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSubCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.DoubleClick
        SearchSubCategory()
    End Sub


    Private Sub txtSubCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSubCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSubCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSubCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSubCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSubCategory()
    End Sub

    Private Sub txtSubCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSubCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCatCode As String


        If txtSubCategory.Text = "" Then GoTo EventExitSub


        If txtCategory.Text = "" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"
            If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
                mCatCode = MasterNo
            End If
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"
        End If

        If MainClass.ValidateWithMasterTable((txtSubCategory.Text), "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo,  , SqlStr) = False Then
            MsgInformation("No Such Sub Category ")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub chkAllSubCat_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSubCat.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSubCategory.Enabled = False
            cmdSubCatsearch.Enabled = False
        Else
            txtSubCategory.Enabled = True
            cmdSubCatsearch.Enabled = True
        End If
    End Sub


    Private Sub SearchSubCategory()
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCatCode As String

        If txtCategory.Text = "" Then
            MsgInformation("Please Select category .")
            If txtCategory.Enabled = True Then txtCategory.Focus()
            Exit Sub
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
            mCatCode = MasterNo
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"


        If MainClass.SearchGridMaster((txtSubCategory.Text), "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE",  ,  , SqlStr) = True Then
            If AcName <> "" Then
                txtSubCategory.Text = AcName
            End If
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub



    Private Sub TxtWef_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        Dim SqlStr As String
        Dim mWEF As String

        If Trim(txtWEF.Text) = "" Then
            '        MsgInformation "WEF Date Is Blank"
            '        txtWEF.SetFocus
            GoTo EventExitSub
        End If

        If MODIFYMode = True And RsMRPMain.EOF = False Then mWEF = RsMRPMain.Fields("WEF").Value

        If ADDMode = True Then
            SqlStr = " SELECT * FROM INV_ITEM_MRP_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRPMain, ADODB.LockTypeEnum.adLockReadOnly)

            If RsMRPMain.EOF = True Then
                mWEF = txtWEF.Text
                Clear1()
                txtWEF.Text = VB6.Format(mWEF, "DD/MM/YYYY")
                Call ShowDetail1()
                GoTo EventExitSub
            End If
        End If

        SqlStr = " SELECT * FROM INV_ITEM_MRP_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRPMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsMRPMain.EOF = False Then
            Clear1()
            Call Show1()
            If txtWEF.Enabled = True Then txtWEF.Focus()
        Else

            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Month, Use add Button to Generate New Costing.", MsgBoxStyle.Information)
                Cancel = True
                GoTo EventExitSub
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM INV_ITEM_MRP_HDR " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND WEF_DATE=TO_DATE('" & VB6.Format(mWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRPMain, ADODB.LockTypeEnum.adLockReadOnly)
                GoTo EventExitSub
            End If
        End If

        CalcTots()
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub Show1()
        On Error GoTo ShowErrPart
        Dim mAccountName As String

        Clear1()
        If Not RsMRPMain.EOF Then

            txtWEF.Text = VB6.Format(IIf(IsDBNull(RsMRPMain.Fields("WEF_DATE").Value), "", RsMRPMain.Fields("WEF_DATE").Value), "DD/MM/YYYY")
            txtAbatementPer.Text = VB6.Format(IIf(IsDBNull(RsMRPMain.Fields("ABATEMENT_PER").Value), "", RsMRPMain.Fields("ABATEMENT_PER").Value), "0.00")
            txtWEF.Enabled = False
            cmdAmend.Enabled = True
            cmdSearchAmend.Enabled = True
            Call ShowDetail1()
        End If
        ADDMode = False
        MODIFYMode = False
        '    SprdMain.Enabled = False
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColProdCode, ColPrevRate)
        MainClass.ButtonStatus(Me, XRIGHT, RsMRPMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Function GetMaxWEF() As String
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = " SELECT MAX(WEF_DATE) AS WEF" & vbCrLf & " FROM INV_ITEM_MRP_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDBNull(RsTemp.Fields("WEF").Value) Then
                GetMaxWEF = ""
            Else
                GetMaxWEF = RsTemp.Fields("WEF").Value
            End If
        Else
            GetMaxWEF = ""
        End If

        Exit Function
ErrPart:
        GetMaxWEF = ""
    End Function
    Private Sub ShowDetail1()
        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim mItemCode As String
        Dim mItemShortDesc As String
        Dim mPartNo As String
        Dim mIssueUOM As String
        Dim mPrevRate As String
        Dim mCurrRate As String
        Dim mDiff As String
        Dim mDiffPer As String
        Dim mCatCode As String
        Dim mSubCatCode As String

        Dim mPrevRateDisc As Double
        Dim mCurrRateDisc As Double

        SqlStr = ""

        SqlStr = " SELECT ITEM.ITEM_CODE, ITEM.ITEM_SHORT_DESC, " & vbCrLf _
            & " ITEM.CUSTOMER_PART_NO, ITEM.ISSUE_UOM, "

        SqlStr = SqlStr & vbCrLf & "GetMRPRate(ITEM.COMPANY_CODE,TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),ITEM.ITEM_CODE,'P') AS PREVIOUS_RATE,"
        SqlStr = SqlStr & vbCrLf & "GetMRPRateDiscount(ITEM.COMPANY_CODE,TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),ITEM.ITEM_CODE,'P') AS PREVIOUS_RATE_DISC,"

        SqlStr = SqlStr & vbCrLf & "GetMRPRate(ITEM.COMPANY_CODE,TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),ITEM.ITEM_CODE,'C') AS CURRENT_RATE,"
        SqlStr = SqlStr & vbCrLf & "GetMRPRateDiscount(ITEM.COMPANY_CODE,TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),ITEM.ITEM_CODE,'C') AS CURRENT_RATE_DISC,"

        SqlStr = SqlStr & vbCrLf & "0, 0,0 "

        SqlStr = SqlStr & vbCrLf _
            & " FROM INV_ITEM_MST ITEM, INV_GENERAL_MST GEN " & vbCrLf _
            & " WHERE ITEM.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ITEM.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
            & " AND ITEM.CATEGORY_CODE=GEN.GEN_CODE" & vbCrLf _
            & " AND GEN.GEN_TYPE='C' AND STOCKTYPE='FG' "

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND GEN.GEN_DESC='" & MainClass.AllowSingleQuote(txtCategory.Text) & "'"
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mCatCode = "-1"
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
                mSubCatCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND ITEM.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " Order By ITEM.ITEM_CODE"

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        'MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        'SprdMain.MaxRows = SprdMain.MaxRows + 1

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRPDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsMRPDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1
            I = 1
            '        .MoveFirst

            Do While Not .EOF

                SprdMain.Row = I
                '            FormatSprdMain I

                SprdMain.Col = ColProdCode
                mItemCode = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = mItemCode

                SprdMain.Col = ColProdDesc
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value))

                SprdMain.Col = ColProdUOM
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value))

                SprdMain.Col = ColProdPartNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value))

                SprdMain.Col = ColPrevRate
                mPrevRate = CStr(IIf(IsDBNull(.Fields("PREVIOUS_RATE").Value), 0, .Fields("PREVIOUS_RATE").Value)) 'CStr(GetMRPRate((txtWEF.Text), "RATE", mItemCode, "P"))
                SprdMain.Text = mPrevRate

                SprdMain.Col = ColPrevRateDisc
                mPrevRateDisc = CStr(IIf(IsDBNull(.Fields("PREVIOUS_RATE_DISC").Value), 0, .Fields("PREVIOUS_RATE_DISC").Value)) 'CStr(GetMRPRate((txtWEF.Text), "RATE", mItemCode, "P"))
                SprdMain.Text = mPrevRateDisc

                SprdMain.Col = ColCurrRate
                mCurrRate = CStr(IIf(IsDBNull(.Fields("CURRENT_RATE").Value), 0, .Fields("CURRENT_RATE").Value)) ' CStr(GetMRPRate((txtWEF.Text), "RATE", mItemCode, "C"))
                SprdMain.Text = mCurrRate

                SprdMain.Col = ColCurrRateDisc
                mCurrRateDisc = CStr(IIf(IsDBNull(.Fields("CURRENT_RATE_DISC").Value), 0, .Fields("CURRENT_RATE_DISC").Value)) 'CStr(GetMRPRate((txtWEF.Text), "RATE", mItemCode, "P"))
                SprdMain.Text = mCurrRateDisc

                SprdMain.Col = ColRateAftAbatement
                SprdMain.Text = VB6.Format(CDbl(mCurrRate) - (CDbl(mCurrRate) * CDbl(Val(txtAbatementPer.Text)) * 0.01), "0.00")

                SprdMain.Col = ColOEMRate
                SprdMain.Text = "0.00" '' CStr(GetMRPRate((txtWEF.Text), "RATE_OEM", mItemCode, "C"))

                SprdMain.Col = ColDiff
                mDiff = CStr(CDbl(mCurrRate) - CDbl(mPrevRate))
                SprdMain.Text = mDiff

                SprdMain.Col = ColDiff
                mDiff = (mCurrRate - (mCurrRate * mCurrRateDisc * 0.01)) - (mPrevRate - (mPrevRate * mPrevRateDisc * 0.01))
                SprdMain.Text = CStr(mDiff)

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)

        'Call CalcGrid()


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
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
    Private Sub TxtWef_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtWEF_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.DoubleClick
        Call cmdSearchAmend_Click(cmdSearchAmend, New System.EventArgs())
    End Sub
    Private Sub txtWEF_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtWEF.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            Call cmdSearchAmend_Click(cmdSearchAmend, New System.EventArgs())
        End If
    End Sub


    Private Sub CalcTots()
        On Error GoTo CalcERR
        Dim I As Integer
        Dim mItemCode As String
        Dim mPrevRate As Double
        Dim mPrevRateDisc As Double
        Dim mCurrRate As Double
        Dim mCurrRateDisc As Double
        Dim mDiff As Double
        Dim ColDiffPer As Double

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColProdCode
                mItemCode = Trim(.Text)
                If Trim(mItemCode) <> "" Then

                    SprdMain.Col = ColPrevRate
                    mPrevRate = GetMRPRate((txtWEF.Text), "RATE", mItemCode, "P")
                    SprdMain.Text = CStr(mPrevRate)

                    SprdMain.Col = ColPrevRateDisc
                    mPrevRateDisc = GetMRPRate((txtWEF.Text), "RATE_DISC", mItemCode, "P")
                    SprdMain.Text = CStr(mPrevRateDisc)

                    SprdMain.Col = ColCurrRate
                    'SprdMain.Text = IIf(Val(SprdMain.Text) = 0, mPrevRate, Val(SprdMain.Text))
                    mCurrRate = Val(SprdMain.Text)

                    SprdMain.Col = ColCurrRateDisc
                    'SprdMain.Text = IIf(Val(SprdMain.Text) = 0, mPrevRateDisc, Val(SprdMain.Text))
                    mCurrRateDisc = Val(SprdMain.Text)

                    SprdMain.Col = ColDiff
                    mDiff = (mCurrRate - (mCurrRate * mCurrRateDisc * 0.01)) - (mPrevRate - (mPrevRate * mPrevRateDisc * 0.01))
                    SprdMain.Text = CStr(mDiff)

                End If
            Next
        End With

        'Call CalcGrid()

        Exit Sub
CalcERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub CalcGrid()
        On Error GoTo CalcERR
        Dim I As Integer
        Dim mProdCode As String
        Dim mPrevRate As Double
        Dim mCurrRate As Double

        Dim mPrevRateDisc As Double
        Dim mCurrRateDisc As Double

        Dim mDiff As Double
        Dim mDiffPer As Double

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColProdCode
                mProdCode = Trim(.Text)
                If Trim(mProdCode) <> "" Then

                    SprdMain.Col = ColPrevRate
                    mPrevRate = Val(.Text)

                    SprdMain.Col = ColPrevRateDisc
                    mPrevRateDisc = Val(.Text)


                    SprdMain.Col = ColCurrRate
                    mCurrRate = Val(.Text)


                    SprdMain.Col = ColCurrRateDisc
                    mCurrRateDisc = Val(.Text)

                    SprdMain.Col = ColRateAftAbatement
                    SprdMain.Text = VB6.Format(mCurrRate - (mCurrRate * CDbl(txtAbatementPer.Text) * 0.01), "0.00")

                    SprdMain.Col = ColDiff
                    mDiff = (mCurrRate - (mCurrRate * mCurrRateDisc * 0.01)) - (mPrevRate - (mPrevRate * mPrevRateDisc * 0.01))
                    SprdMain.Text = CStr(mDiff)

                End If
            Next
        End With
        Exit Sub
CalcERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdPopFromFile_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPopFromFile.Click
        On Error GoTo ErrPart
        Dim strFilePath As String


        strFilePath = My.Application.Info.DirectoryPath

        ''Commit on convert to .net
        If Not fOpenFile(strFilePath, "*.xlsx", "Excel Data", CommonDialogOpen) Then
            GoTo NormalExit
        End If

        Call PopulateFromXLSFile(strFilePath)

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:
    End Sub
    Private Sub PopulateFromXLSFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String = ""
        Dim mPartNo As String = ""
        Dim mRate As Double
        Dim mDiff As Double
        Dim xSqlStr As String
        Dim mSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""
        Dim mDivisionCode As Double
        Dim mPreviousRate As Double
        Dim mPreviousRateDisc As Double
        Dim mRateDisc As Double

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)


        FileConnStr = "Provider=MSDASQL.1;Connect Timeout=15;Extended Properties='DSN=Excel Files;DBQ=XXLSFILEX;DefaultDir=XXLSDIRX;DriverId=790;FIL=excel 8.0;MaxBufferSize=2048;PageTimeout=5;UID=admin;';Locale Identifier=1033"
        FileConnStr = Replace(FileConnStr, "XXLSFILEX", strXLSFile)
        strTemp = Mid(strXLSFile, 1, InStrRev(strXLSFile, "\") - 1)
        FileConnStr = Replace(FileConnStr, "XXLSDIRX", strTemp)

        If Not XLSConnect(Trim(FileConnStr), FileDBCn) Then
            GoTo ErrPart
        End If

        RsFile = FileDBCn.OpenSchema(ADODB.SchemaEnum.adSchemaTables)
        strWkShName = RsFile.Fields("Table_Name").Value

        mSqlStr = "SELECT * FROM ""XWKSHTX"" " ''WHERE F1 <> NULL"	
        mSqlStr = Replace(mSqlStr, "XWKSHTX", strWkShName)

        If OpenExcelRecordSet(mSqlStr, RsFile, strError, FileDBCn, False) = 0 Then

            If RsFile.EOF = False Then
                Do While Not RsFile.EOF
                    mItemCode = Trim(IIf(IsDBNull(RsFile.Fields(0).Value), "", RsFile.Fields(0).Value))
                    mRate = Val(IIf(IsDBNull(RsFile.Fields(4).Value), 0, RsFile.Fields(4).Value))
                    mRateDisc = Val(IIf(IsDBNull(RsFile.Fields(5).Value), 0, RsFile.Fields(5).Value))

                    xSqlStr = " SELECT ITEM_SHORT_DESC,ISSUE_UOM,CUSTOMER_PART_NO, "

                    xSqlStr = xSqlStr & vbCrLf & "GetMRPRate(ITEM.COMPANY_CODE,TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),ITEM.ITEM_CODE,'P') AS PREVIOUS_RATE,"
                    xSqlStr = xSqlStr & vbCrLf & "GetMRPRateDiscount(ITEM.COMPANY_CODE,TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),ITEM.ITEM_CODE,'P') AS PREVIOUS_RATE_DISC,"


                    xSqlStr = xSqlStr & vbCrLf _
                        & " FROM INV_ITEM_MST " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(mItemCode) & "'"

                    MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        mItemDesc = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))
                        mUOM = Trim(IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                        mPartNo = Trim(IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value))
                        mPreviousRate = Val(IIf(IsDBNull(RsTemp.Fields("PREVIOUS_RATE").Value), 0, RsTemp.Fields("PREVIOUS_RATE").Value))
                        mPreviousRateDisc = Val(IIf(IsDBNull(RsTemp.Fields("PREVIOUS_RATE_DISC").Value), 0, RsTemp.Fields("PREVIOUS_RATE_DISC").Value))
                    Else
                        GoTo NextRecord
                    End If
                    'If DuplicateItem = True Then GoTo NextRecord

                    SprdMain.Row = SprdMain.MaxRows

                    SprdMain.Col = ColProdCode
                    SprdMain.Text = mItemCode

                    SprdMain.Col = ColProdDesc
                    SprdMain.Text = mItemDesc

                    SprdMain.Col = ColProdUOM
                    SprdMain.Text = mUOM

                    SprdMain.Col = ColProdPartNo
                    SprdMain.Text = mPartNo

                    SprdMain.Col = ColPrevRate
                    SprdMain.Text = CStr(mPreviousRate)

                    SprdMain.Col = ColPrevRateDisc
                    SprdMain.Text = CStr(mPreviousRateDisc)


                    SprdMain.Col = ColCurrRate
                    SprdMain.Text = CStr(mRate)

                    SprdMain.Col = ColCurrRateDisc
                    SprdMain.Text = CStr(mRateDisc)

                    SprdMain.Col = ColDiff
                    mDiff = (mRate - (mRate * mRateDisc * 0.01)) - (mPreviousRate - (mPreviousRate * mPreviousRateDisc * 0.01))
                    SprdMain.Text = CStr(mDiff)

                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    '               FormatSprdMain -1, False	

NextRecord:
                    RsFile.MoveNext()
                Loop
            End If
        End If
        'CalcTots()

        If RsFile.State = ADODB.ObjectStateEnum.adStateOpen Then RsFile.Close()
        RsFile = Nothing

        If FileDBCn.State = ADODB.ObjectStateEnum.adStateOpen Then
            FileDBCn.Close()
            FileDBCn = Nothing
        End If

        '    CmdPopFromFile.Enabled = False	
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume	
    End Sub
End Class
