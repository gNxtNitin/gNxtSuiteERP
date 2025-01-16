Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmDieselOnVechile
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection	
    Private Const RowHeight As Short = 22

    Private Const ColVechileNo As Short = 1
    Private Const ColCompanyName1 As Short = 2

    Dim ColFlag As Short
    Dim mcntRow As Integer

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        Try
            Dim mSqlStr As String
            Dim RsTemp As ADODB.Recordset = Nothing
            Dim mCol As Long

            mSqlStr = "SELECT COMPANY_SHORTNAME, COMPANY_GROUPING FROM GEN_COMPANY_MST WHERE STATUS='O' ORDER BY COMPANY_GROUPING,COMPANY_SHORTNAME"
            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            With SprdMain
                .Row = 0
                mCol = ColCompanyName1
                If Not RsTemp.EOF Then
                    Do While Not RsTemp.EOF
                        .Col = mCol
                        .Text = IIf(IsDBNull(RsTemp.Fields("COMPANY_SHORTNAME").Value), "", RsTemp.Fields("COMPANY_SHORTNAME").Value)

                        RsTemp.MoveNext()

                        mCol = mCol + 1
                        .MaxCols = .MaxCols + 1

                        If RsTemp.EOF = True Then
                            .Row = 0
                            ColFlag = mCol
                            .Col = ColFlag
                            .Text = "Flag"
                        End If
                    Loop
                End If
            End With

            MainClass.ClearGrid(SprdMain, RowHeight)
            FormatSprdMain()

            If Trim(txtDateFrom.Text) = "" Then
                MsgInformation("Please Enter Date.")
                txtDateFrom.Focus()
                Exit Sub
            End If

            If Trim(txtDateTo.Text) = "" Then
                MsgInformation("Please Enter Date.")
                txtDateTo.Focus()
                Exit Sub
            End If

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Show1()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            SprdMain.Focus()
            Call PrintStatus(True)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Show1()

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim mCompanyAlais As String = ""
        Dim CntCol As Long

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        If Trim(txtDateFrom.Text) = "" Or Trim(txtDateTo.Text) = "" Then Exit Sub
        If IsDate(txtDateFrom.Text) = False Then
            MsgBox("Invalid Date")
            Exit Sub
        End If

        If IsDate(txtDateTo.Text) = False Then
            MsgBox("Invalid Date")
            Exit Sub
        End If

        With SprdMain
            For CntCol = ColCompanyName1 To ColFlag - 1
                .Row = 0
                .Col = CntCol
                mCompanyAlais = IIf(mCompanyAlais = "", "", mCompanyAlais & ",") & "'" & Trim(.Text) & "'"
            Next
        End With

        SqlStr = " SELECT * FROM (" & vbCrLf _
                & " SELECT IGD.ISSUE_PURPOSE, NVL(IGD.ISSUE_QTY,0) ISSUE_QTY, GMST.COMPANY_SHORTNAME" & vbCrLf _
                & " FROM INV_ISSUE_HDR IGH, INV_ISSUE_DET IGD, INV_ITEM_MST INVMST, GEN_COMPANY_MST GMST"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IGH.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
            & " AND IGH.AUTO_KEY_ISS=IGD.AUTO_KEY_ISS" & vbCrLf _
            & " AND IGH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND IGD.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND INVMST.ITEM_CLASSIFICATION='3' AND ISSUE_FOR<>'S'"

        SqlStr = SqlStr & vbCrLf _
            & " AND IGH.ISSUE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IGH.ISSUE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " UNION ALL "

        SqlStr = SqlStr & vbCrLf _
                & " SELECT IGD.ISSUE_PURPOSE, IGD.ISSUE_QTY, GMST.COMPANY_SHORTNAME" & vbCrLf _
                & " FROM INV_SUB_ISSUE_HDR IGH, INV_SUB_ISSUE_DET IGD, INV_ITEM_MST INVMST, GEN_COMPANY_MST GMST"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IGH.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
            & " AND IGH.AUTO_KEY_ISS=IGD.AUTO_KEY_ISS" & vbCrLf _
            & " AND IGH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND IGD.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND INVMST.ITEM_CLASSIFICATION='3' AND (IGD.ISSUE_PURPOSE IS NOT NULL OR IGD.ISSUE_PURPOSE<>'')"

        SqlStr = SqlStr & vbCrLf _
            & " AND IGH.ISSUE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IGH.ISSUE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf _
                & " ) " & vbCrLf _
                & " PIVOT " & vbCrLf _
                & " ( " & vbCrLf _
                & " SUM(ISSUE_QTY)" & vbCrLf _
                & " FOR COMPANY_SHORTNAME IN (" & mCompanyAlais & "))"


        SqlStr = SqlStr & vbCrLf _
            & "ORDER BY ISSUE_PURPOSE"

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        'Call CalcSprdTotal()

        SprdMain.MaxCols = SprdMain.MaxCols + 1
        SprdMain.MaxRows = SprdMain.MaxRows + 1
        ColFlag = ColFlag + 1

        Call FormatSprdMain()

        SprdMain.Row = 0
        SprdMain.Col = ColFlag - 1
        SprdMain.Text = "Total"

        Dim J As Long

        For J = 1 To SprdMain.MaxRows - 1
            Call CalcRowTotal(SprdMain, ColCompanyName1, J, ColFlag - 2, J, J, ColFlag - 1)

            With SprdMain
                .Row = J
                .Col = ColFlag - 1
                '.SetCellBorder(j, i, j, i, SS_BORDER_TYPE_LEFT, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)
                .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 'Gray	
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue	
                .Font = VB6.FontChangeBold(.Font, True)
            End With
        Next

        For j = ColCompanyName1 To ColFlag - 1
            Call CalcRowTotal(SprdMain, J, 1, J, SprdMain.MaxRows - 1, SprdMain.MaxRows, J)

            With SprdMain
                .Row = SprdMain.MaxRows
                .Col = J
                '.SetCellBorder(j, i, j, i, SS_BORDER_TYPE_LEFT, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)
                .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 'Gray	
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue	
                .Font = VB6.FontChangeBold(.Font, True)
            End With
        Next

        Call FormatSprdMain()



        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
LedgError:
        MsgInformation(Err.Description)
    End Sub

    Public Sub frmDieselOnVechile_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim mCol As Integer
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset

        If FormActive = True Then Exit Sub

        mSqlStr = "SELECT COMPANY_SHORTNAME, COMPANY_GROUPING FROM GEN_COMPANY_MST WHERE STATUS='O' ORDER BY COMPANY_GROUPING,COMPANY_SHORTNAME"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdMain
            .Row = 0
            mCol = ColCompanyName1
            If Not RsTemp.EOF Then
                Do While Not RsTemp.EOF
                    .Col = mCol
                    .Text = IIf(IsDBNull(RsTemp.Fields("COMPANY_SHORTNAME").Value), "", RsTemp.Fields("COMPANY_SHORTNAME").Value)

                    RsTemp.MoveNext()

                    mCol = mCol + 1
                    .MaxCols = .MaxCols + 1
                    If RsTemp.EOF = True Then
                        .Row = 0
                        ColFlag = mCol
                        .Col = ColFlag
                        .Text = "Flag"
                    End If
                Loop
            End If
        End With

        FormatSprdMain()
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmDieselOnVechile_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7440
        CurrFormWidth = 11625

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7440)
        'Me.Width = VB6.TwipsToPixelsX(11625)

        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain()

        Dim cntCol As Integer

        With SprdMain
            .MaxCols = ColFlag
            .set_RowHeight(0, RowHeight * 2)

            .Row = -1
            .set_RowHeight(-1, RowHeight)


            .Col = ColVechileNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColVechileNo, 18)
            .TypeEditMultiLine = True
            .ColsFrozen = ColVechileNo

            For cntCol = ColCompanyName1 To ColFlag - 1
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("0")
                .TypeFloatMax = CDbl("9999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 10)
                .TypeEditMultiLine = True
            Next

            .Col = ColFlag
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColFlag, 5)
            .ColHidden = True

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal

        End With

    End Sub

    Private Sub frmDieselOnVechile_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1	
        '    MainClass.SetSpreadColor SprdOption, -1	
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmDieselOnVechile_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintBOM(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub InsertIntoPrintdummyData()

        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mInsertSQL As String
        Dim mValueSQL As String
        Dim mFieldStr As String
        Dim mValueStr As String

        On Error GoTo ERR1

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                mInsertSQL = ""
                mValueSQL = ""
                SqlStr = ""

                mInsertSQL = "Insert into TEMP_PrintDummyData (UserID,SubRow,"
                mValueSQL = " Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", "


                For cntCol = 1 To .MaxCols
                    .Col = cntCol

                    If cntCol = .MaxCols Then
                        mFieldStr = "FIELD" & cntCol
                        mValueStr = "'" & MainClass.AllowSingleQuote(VB.Left(.Text, 255)) & "'"
                    Else
                        mFieldStr = "FIELD" & cntCol & ","
                        mValueStr = "'" & MainClass.AllowSingleQuote(VB.Left(.Text, 255)) & "'" & ","
                    End If

                    mInsertSQL = mInsertSQL & mFieldStr
                    mValueSQL = mValueSQL & mValueStr


                Next
                mInsertSQL = mInsertSQL & ")"
                mValueSQL = mValueSQL & ")"

                SqlStr = mInsertSQL & vbCrLf & mValueSQL
                PubDBCn.Execute(SqlStr)
            Next
        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        'Resume	
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Sub PrintBOM(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mCustDealer As String
        Dim SqlStr As String = ""

        Report1.Reset()
        SqlStr = ""
        PubDBCn.Execute("DELETE FROM TEMP_PRINTDUMMYDATA WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'")

        MainClass.ClearCRptFormulas(Report1)

        Call InsertIntoPrintdummyData()

        '*************** Fetching Record For Report ***************************	
        SqlStr = ""
        SqlStr = "SELECT * " & vbCrLf & " FROM TEMP_PRINTDUMMYDATA " & vbCrLf & " WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " ORDER BY SUBROW"


        mTitle = "Vechile Wise Diesel Consumption"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\DieselOnVechile.rpt"
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim ii As Integer
        Dim mHeadStr As String


        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, "")
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintBOM(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

End Class
