Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSuppResponesMaster
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSuppCode As Short = 1
    Private Const ColSuppName As Short = 2
    Private Const ColResponesPoint As Short = 3

    Private Sub FillHeading()

        'Dim RsTemp As ADODB.Recordset=Nothing
        'Dim cntCol As Integer

        '    MainClass.ClearGrid SprdMain

        With SprdMain
            .MaxCols = ColResponesPoint

            .set_RowHeight(0, ConRowHeight * 2)
            .set_RowHeight(-1, ConRowHeight * 1.5)
            .Row = -1

            .Col = ColSuppCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColSuppCode, 8)

            .Col = ColSuppName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColSuppName, 37)

            .Col = ColResponesPoint
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColResponesPoint, 8)


            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColSuppCode, ColSuppName)
            MainClass.SetSpreadColor(SprdMain, -1)
            '        SprdMain.OperationMode = OperationModeSingle
        End With
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim mSuppCode As String
        Dim mResPoint As Double
        Dim mUpdateCount As Integer

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM PUR_SUPP_CUST_RES WHERE" & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(RES_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY")) & "'"

        PubDBCn.Execute(SqlStr)

        mUpdateCount = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColSuppCode
                mSuppCode = Trim(.Text)

                .Col = ColResponesPoint
                mResPoint = Val(.Text)

                If Trim(mSuppCode) <> "" And mResPoint > 0 Then

                    SqlStr = "INSERT INTO PUR_SUPP_CUST_RES ( " & vbCrLf & " COMPANY_CODE, FYEAR, SUPP_CUST_CODE, " & vbCrLf & " RES_DATE, RES_POINT, " & vbCrLf & " ADDUSER, ADDDATE) VALUES (" & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mSuppCode) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(CStr(mResPoint)) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))"

                    PubDBCn.Execute(SqlStr)


                    mUpdateCount = mUpdateCount + 1
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " Supplier Respones Master.", MsgBoxStyle.Information)

        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
    End Sub
    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(SprdMain)
        RefreshScreen()
    End Sub
    Private Sub frmSuppResponesMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(7515)

        lblRunDate.Text = CStr(RunDate)

        SetDate(CDate(lblRunDate.Text))
        FillHeading()
        UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub UpDYear_DownClick()

        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(SprdMain)
    End Sub
    Private Sub UpDYear_UpClick()

        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(SprdMain)
    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mResPoint As Double
        Dim mSuppCode As String

        MainClass.ClearGrid(SprdMain)


        SqlStr = " SELECT DISTINCT IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME " & vbCrLf & " FROM FIN_SUPP_CUST_HDR IH, FIN_SUPP_CUST_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST, " & vbCrLf & " INV_SUBCATEGORY_MST SUBCATMST WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE= ID.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE= ID.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE= CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE= CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE= INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE= INVMST.ITEM_CODE" & vbCrLf & " AND INVMST.COMPANY_CODE= SUBCATMST.COMPANY_CODE" & vbCrLf & " AND INVMST.CATEGORY_CODE= SUBCATMST.CATEGORY_CODE" & vbCrLf & " AND INVMST.SUBCATEGORY_CODE= SUBCATMST.SUBCATEGORY_CODE" & vbCrLf & " AND CMST.SUPP_CUST_TYPE='S' AND IH.IS_APPROVED='Y' AND ID.ITEM_APPROVED='Y' AND SUBCATMST.IS_APPROVAL='Y'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY CMST.SUPP_CUST_NAME "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        cntRow = 1
        With SprdMain
            If RsTemp.EOF = False Then
                Do While Not RsTemp.EOF
                    .Row = cntRow
                    .Col = ColSuppCode
                    mSuppCode = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                    .Text = mSuppCode

                    .Col = ColSuppName
                    .Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)

                    mResPoint = GetResponesPoint(mSuppCode)
                    .Col = ColResponesPoint
                    .Text = CStr(mResPoint)

                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        cntRow = cntRow + 1
                        .MaxRows = cntRow
                    End If
                Loop
            End If
        End With

        FillHeading()
        Exit Sub
refreshErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub
    Private Sub SetDate(ByRef xDate As Date)

        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        Tempdate = "01/" & Month(xDate) & "/" & Year(xDate)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

        Daysinmonth = MainClass.LastDay(VB6.Format(xDate, "mm"), VB6.Format(xDate, "yyyy"))
    End Sub

    Private Function GetResponesPoint(ByRef pSuppCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsSupp As ADODB.Recordset = Nothing

        GetResponesPoint = 0

        SqlStr = "SELECT RES_POINT " & vbCrLf & " FROM PUR_SUPP_CUST_RES " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pSuppCode & "'" & vbCrLf & " AND RES_DATE=TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSupp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsSupp.EOF = False Then
            GetResponesPoint = IIf(IsDbNull(RsSupp.Fields("RES_POINT").Value), 0, RsSupp.Fields("RES_POINT").Value)
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Class
