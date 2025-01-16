Option Strict Off
Option Explicit On
Imports AxFPSpreadADO
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamSaleMIS
    Inherits System.Windows.Forms.Form
    Dim FormLoaded As Boolean

    Dim GroupOnItem As Boolean
    'Dim PvtDBCn As ADODB.Connection

    Private Const RowHeight As Short = 12

    Private Const ColPicMain As Short = 1
    Private Const ColPicSub As Short = 2

    Private Const ColCompanyCode As Short = 3
    Private Const ColGrouping As Short = 4
    Private Const ColBillNo As Short = 5
    Private Const ColBillDate As Short = 6
    Private Const ColCustomerCode As Short = 7
    Private Const ColCustomerName As Short = 8
    Private Const ColCustomerLocation As Short = 9

    Private Const ColPANNo As Short = 10
    Private Const ColCode As Short = 11
    Private Const ColItemName As Short = 12
    Private Const ColPartNo As Short = 13
    Private Const ColUnit As Short = 14
    Private Const ColSALERate As Short = 15
    Private Const ColSALESQMQTY As Short = 16
    Private Const ColSALEQTY As Short = 17
    Private Const ColSALEVALUE As Short = 18
    Private Const ColCGST As Short = 19
    Private Const ColSGST As Short = 20
    Private Const ColIGST As Short = 21
    Private Const ColTCS As Short = 22
    Private Const ColOthers As Short = 23
    Private Const ColNetAmount As Short = 24
    Private Const ColISGroup As Short = 25
    Private Const ColFlag As Short = 26


    Private Const ColCustomer1 As Short = 1
    Private Const ColDivision1 As Short = 2
    Private Const ColItemCode1 As Short = 3
    Private Const ColCategory1 As Short = 4
    Private Const ColSubCategory1 As Short = 5
    Private Const ColHSNCode1 As Short = 6
    Private Const ColItemDesc1 As Short = 7
    Private Const ColBillNo1 As Short = 8
    Private Const ColAgtD31 As Short = 9
    Private Const ColRejection1 As Short = 10
    Private Const ColFOC1 As Short = 11
    Private Const ColInvoiceType1 As Short = 12
    Private Const ColAccountHead1 As Short = 13
    Private Const ColAgtCT31 As Short = 14
    Private Const ColAgtCT11 As Short = 15
    Private Const ColDutyForegone1 As Short = 16
    Private Const ColHSNPer As Short = 17
    Private Const ColModel As Short = 18
    Private Const ColPANNo1 As Short = 19
    Private Const ColSalePerson1 As Short = 20
    Private Const ColThickness1 As Short = 21
    Private Const ColColor1 As Short = 22
    Private Const ColProductType1 As Short = 23
    Private Const ColCustomerGroup1 As Short = 24

    Dim mClickProcess As Boolean

    Dim mActiveRow As Integer
    Dim PrintFlag As Boolean

    Dim GetSubTitle As String
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Dim minuspict As System.Drawing.Image
    Dim pluspict As System.Drawing.Image

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboItemType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboItemType.SelectedIndexChanged, cboReversalInvoice.SelectedIndexChanged
        PrintStatus(False)
    End Sub
    Private Sub cboWise_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboWise.SelectedIndexChanged
        PrintStatus(False)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub FillSprdMain()
        On Error GoTo ERR1
        Dim I As Integer
        Dim mField As String

        With SprdMain
            .MaxCols = ColFlag
            .Row = 0

            .Col = 0
            .Text = "S.No."

            .Col = ColCompanyCode
            .Text = "Company Code"

            .Col = ColGrouping
            For I = 1 To SprdOption.MaxCols
                SprdOption.Row = 1
                SprdOption.Col = I
                If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    .Text = FillFieldName(I)
                    Exit For
                End If
            Next

            .Col = ColPicMain
            .Text = "Pic Main"

            .Col = ColPicSub
            .Text = "Pic Sub"

            .Col = ColFlag
            .Text = "Flag"

            .Col = ColISGroup
            .Text = "Is Group"

            .Col = ColBillNo
            .Text = "Bill No"

            .Col = ColBillDate
            .Text = "Bill Date"

            .Col = ColCustomerCode
            .Text = "Customer Code"

            .Col = ColCustomerName
            .Text = "Customer Name"

            .Col = ColCustomerLocation
            .Text = "Location"

            .Col = ColPANNo
            .Text = "PAN No"

            .Col = ColCode
            .Text = "Item Code"

            .Col = ColItemName
            .Text = "Item Short Name"

            .Col = ColPartNo
            .Text = "Part No"

            .Col = ColUnit
            .Text = "Unit"

            .Col = ColSALESQMQTY
            .Text = "Sale Qty (In SQM)"

            .Col = ColSALEQTY
            .Text = "Sale Qty"

            .Col = ColSALERate
            .Text = "Rate"

            .Col = ColSALEVALUE
            .Text = "Item Value"

            .Col = ColCGST
            .Text = "CGST Amount"

            .Col = ColSGST
            .Text = "SGST Amount"

            .Col = ColIGST
            .Text = "IGST Amount"

            .Col = ColTCS
            .Text = "TCS Amount"

            .Col = ColOthers
            .Text = "Other Amt"

            .Col = ColNetAmount
            .Text = "Net Amount"

        End With

        Call HideUnHide()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub

    Private Sub FormatSprdOption(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer

        With SprdOption

            .MaxCols = ColCustomerGroup1
            .Row = 0
            .Col = ColCustomer1
            .Text = "Customer"

            '        .Col = ColInvoiceType1
            '        .Text = "Invoice Type"

            .set_RowHeight(0, 1.2 * RowHeight)

            .Row = 0
            .Col = 0
            .Text = "Grouping"



            For I = 1 To .MaxCols
                .Col = I

                .Row = 1
                .CellType = SS_CELL_TYPE_CHECKBOX
                .TypeHAlign = SS_CELL_H_ALIGN_CENTER
                .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                .set_RowHeight(1, RowHeight)

                .Row = 2
                .CellType = SS_CELL_TYPE_CHECKBOX
                .TypeHAlign = SS_CELL_H_ALIGN_CENTER
                .Value = CStr(System.Windows.Forms.CheckState.Checked)
                .set_RowHeight(2, RowHeight)

                .Col = I
                .Row = 3
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .set_RowHeight(3, RowHeight)
                MainClass.ProtectCell(SprdOption, 3, 3, I, I)

                .set_ColWidth(I, 13)
            Next

            .Col = ColThickness1
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColColor1
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            MainClass.SetSpreadColor(SprdOption, -1)
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer
        Dim j As Integer
        Dim mColWidth As Integer

        With SprdMain
            .set_RowHeight(0, 2.5 * RowHeight)
            .Row = Arow
            .set_ColWidth(0, 7)


            .Col = ColPicMain
            .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
            .TypePictCenter = True
            .TypePictMaintainScale = False
            .TypePictStretch = False
            .set_ColWidth(ColPicMain, 2)
            '.ColHidden = IIf(OptGroup(1).Checked = True, False, True)
            .ColHidden = False

            .Col = ColPicSub
            '.CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
            '.TypePictCenter = True
            '.TypePictMaintainScale = False
            '.TypePictStretch = False
            .ColHidden = True

            .Col = ColFlag
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColISGroup
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColCompanyCode

            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColCompanyCode, 15)

            For I = ColGrouping To ColUnit
                .Col = I

                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
                .set_RowHeight(Arow, RowHeight)
                Select Case I
                    Case ColGrouping
                        For j = 1 To SprdOption.MaxCols
                            SprdOption.Row = 1
                            SprdOption.Col = j
                            If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                                If j = ColCustomer1 Or j = ColItemCode1 Then
                                    mColWidth = 25
                                    Exit For
                                Else
                                    mColWidth = 9
                                End If
                            End If
                        Next
                    Case ColBillNo, ColCustomerCode, ColBillDate, ColCode, ColCustomerLocation, ColPANNo
                        mColWidth = 10
                    Case ColItemName, ColCustomerName
                        mColWidth = 25
                    Case ColUnit
                        mColWidth = 6
                    Case ColPartNo
                        mColWidth = 15
                End Select
                .set_ColWidth(I, mColWidth)
                .ColsFrozen = ColGrouping
                .ColHidden = False
            Next

            .Col = ColSALESQMQTY
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColSALESQMQTY, 10)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColSALEQTY
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColSALEQTY, 10)

            .Col = ColSALERate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColSALERate, 10)

            .Col = ColSALEVALUE
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColSALEVALUE, 10)

            .Col = ColCGST
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColCGST, 10)

            .Col = ColSGST
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColSGST, 10)

            .Col = ColIGST
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColIGST, 10)

            .Col = ColTCS
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColTCS, 10)

            .Col = ColOthers
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColOthers, 10)

            .Col = ColNetAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColNetAmount, 10)

            MainClass.SetSpreadColor(SprdMain, -1, False)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal

        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        ''Resume
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForMIS(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function InsertIntoPrintTable() As Boolean

        On Error GoTo PrintDummyErr

        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim FieldCnt As Short
        Dim RowNum As Short
        Dim FieldNum As Short
        Dim GetData As String
        Dim SetData As String
        Dim SqlStr As String = ""
        Dim prmStartGridRow As Long
        Dim prmEndGridRow As Long

        Dim prmStartGridCol As Long
        Dim prmEndGridCol As Long

        Dim cntCol As Integer

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        prmStartGridRow = 0
        prmEndGridRow = SprdMain.MaxRows

        prmStartGridCol = 1
        prmEndGridCol = SprdMain.MaxCols

        For RowNum = prmStartGridRow To prmEndGridRow
            FieldCnt = 1
            SetData = ""
            GetData = ""
            SprdMain.Row = RowNum
            For FieldNum = prmStartGridCol To prmEndGridCol
                SprdMain.Col = FieldNum
                If FieldNum = prmStartGridCol Then
                    SetData = "FIELD" & FieldCnt
                    GetData = "'" & MainClass.AllowSingleQuote(Mid(SprdMain.Text, 1, 255)) & "'"
                    FieldCnt = FieldCnt + 1
                ElseIf FieldNum = ColGrouping Then
                    SetData = SetData & ", " & "FIELD" & FieldCnt
                    GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(Mid(SprdMain.Text, 1, 255)) & "'"
                    FieldCnt = FieldCnt + 1
                ElseIf FieldNum >= ColBillNo And FieldNum <= ColSALERate Then
                    'For cntCol = 1 To 10
                    If lstFieldName.GetItemChecked(FieldNum - 2) = True Then
                        SetData = SetData & ", " & "FIELD" & FieldCnt
                        GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(Mid(SprdMain.Text, 1, 255)) & "'"
                        FieldCnt = FieldCnt + 1
                    End If
                    'Next
                Else
                    SetData = SetData & ", " & "FIELD" & FieldCnt
                    GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(Mid(SprdMain.Text, 1, 255)) & "'"
                    FieldCnt = FieldCnt + 1
                End If

            Next
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " " & SetData & ") " & vbCrLf _
                & " VALUES (" & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf _
                & " " & GetData & ") "
            PubDBCn.Execute(SqlStr)
NextRec:
        Next

        PubDBCn.CommitTrans()
        InsertIntoPrintTable = True

        Exit Function
PrintDummyErr:
        InsertIntoPrintTable = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub ReportForMIS(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String
        Dim mWiseField As String

        Dim I As Integer

        If InsertIntoPrintTable() = False Then GoTo ERR1

        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)


        If optType(0).Checked Then
            mRPTName = "SalesMIS.RPT"
            mTitle = "Sales MIS"
        Else
            mRPTName = "SalesMISSumm.RPT"
            mTitle = "Sales MIS Summary"
        End If

        SprdMain.Row = 0
        SprdMain.Col = ColGrouping
        mTitle = mTitle & " ( Group By " & SprdMain.Text & " )"

        mSubTitle = "FROM : " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY")
        mSubTitle = mSubTitle & " TO : " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")
        mSubTitle = mSubTitle & " " & IIf(GetSubTitle = "", "", "[ ") & GetSubTitle & IIf(GetSubTitle = "", "", "]")


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
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String


        mSqlStr = mSqlStr & "SELECT * " & " FROM TEMP_PrintDummyData PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " ORDER BY SubRow,Field1,Field3"


        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Dim mFieldHeading As String = ""
        MainClass.ClearCRptFormulas(Report1)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        If optType(0).Checked = True Then
            MainClass.AssignCRptFormulas(Report1, "Wise=""" & cboWise.Text & """")
            If cboWise.SelectedIndex < 2 Then
                MainClass.AssignCRptFormulas(Report1, "ItemDesc='Item Short Description'")
                MainClass.AssignCRptFormulas(Report1, "Unit='UNIT'")
            Else
                MainClass.AssignCRptFormulas(Report1, "ItemDesc=''")
                MainClass.AssignCRptFormulas(Report1, "Unit=''")
            End If
        Else
            SprdMain.Row = 0
            SprdMain.Col = ColGrouping
            mFieldHeading = SprdMain.Text
            MainClass.AssignCRptFormulas(Report1, "Wise=""" & mFieldHeading & """")
        End If
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Report1.Action = 1

    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForMIS(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click


        Dim SqlStr As String = ""
        PrintStatus(False)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(SprdMain)

        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        ShowSaleMIS()

        SprdMain.DataSource = Nothing

        SprdMain.Refresh()
        FormatSprdMain(-1)
        FillSprdMain()
        'GroupByColor()

        If optSubTotalYes.Checked = True Then
            Call GroupBySpread(ColGrouping)
            Call SubTotal
        End If
        Call PrintStatus(True)
        SprdMain.Focus()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub SubTotal()
        On Error GoTo ERR1
        Dim mSALESQMQTY As Double
        Dim mSALEQTY As Double
        Dim mSALEVALUE As Double
        Dim mCGST As Double
        Dim mSGST As Double
        Dim mIGST As Double
        Dim mTCS As Double
        Dim mOthers As Double
        Dim mNetAmount As Double
        Dim cntRow As Integer
        Dim mGroupName As String
        Dim mCompanyName As String

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColISGroup
                If Trim(.Text) = "0" Then

                    .Col = ColCompanyCode
                    mCompanyName = SprdMain.Text

                    .Col = ColGrouping
                    mGroupName = SprdMain.Text

                    mSALESQMQTY = 0
                    mSALEQTY = 0
                    mSALEVALUE = 0
                    mCGST = 0
                    mSGST = 0
                    mIGST = 0
                    mTCS = 0
                    mOthers = 0
                    mNetAmount = 0

                    If GroupSumQry(cntRow, mCompanyName, mGroupName, mSALESQMQTY, mSALEQTY, mSALEVALUE, mCGST, mSGST, mIGST, mTCS, mOthers, mNetAmount) = True Then
                        .Row = cntRow

                        .Col = ColGrouping
                        .Text = "SUB TOTAL : ( " & mGroupName & " )"
                        .Col = ColSALESQMQTY
                        .Text = mSALESQMQTY

                        .Col = ColSALEQTY
                        .Text = mSALEQTY

                        .Col = ColSALEVALUE
                        .Text = mSALEVALUE

                        .Col = ColCGST
                        .Text = mCGST

                        .Col = ColSGST
                        .Text = mSGST

                        .Col = ColIGST
                        .Text = mIGST

                        .Col = ColTCS
                        .Text = mTCS

                        .Col = ColOthers
                        .Text = mOthers

                        .Col = ColNetAmount
                        .Text = mNetAmount
                    End If


                End If
            Next
        End With

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Function GroupSumQry(ByRef mLineNo As Long, ByRef mCompanyName As String, ByRef mGroupName As String, ByRef mSALESQMQTY As Double, ByRef mSALEQTY As Double, ByRef mSALEVALUE As Double, ByRef mCGST As Double,
        ByRef mSGST As Double, ByRef mIGST As Double, ByRef mTCS As Double, ByRef mOthers As Double, ByRef mNetAmount As Double) As Boolean
        On Error GoTo ViewTrialErr
        Dim cntRow As Long
        'Dim mCheckCompanyName As String
        'Dim mCheckGroupName As String


        With SprdMain
            For cntRow = mLineNo + 1 To .MaxRows
                .Row = cntRow
                .Col = ColCompanyCode
                If .Text = mCompanyName Then
                    .Col = ColGrouping
                    If .Text = mGroupName Then
                        .Col = ColSALESQMQTY
                        mSALESQMQTY = mSALESQMQTY + Val(.Text)

                        .Col = ColSALEQTY
                        mSALEQTY = mSALEQTY + Val(.Text)

                        .Col = ColSALEVALUE
                        mSALEVALUE = mSALEVALUE + Val(.Text)

                        .Col = ColCGST
                        mCGST = mCGST + Val(.Text)

                        .Col = ColSGST
                        mSGST = mSGST + Val(.Text)

                        .Col = ColIGST
                        mIGST = mIGST + Val(.Text)

                        .Col = ColTCS
                        mTCS = mTCS + Val(.Text)

                        .Col = ColOthers
                        mOthers = mOthers + Val(.Text)

                        .Col = ColNetAmount
                        mNetAmount = mNetAmount + Val(.Text)
                    End If
                End If
            Next

        End With

        GroupSumQry = True
        Exit Function
ViewTrialErr:
        GroupSumQry = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim I As Integer

        FieldsVarification = True
        If Not IsDate(txtDateFrom.Text) Then
            FieldsVarification = False
            MsgInformation("Invaild Date")
            txtDateFrom.Focus()
            Exit Function
            'ElseIf FYChk((txtDateFrom.Text)) = False Then
            '    FieldsVarification = False
            '    txtDateFrom.Focus()
            '    Exit Function
        End If

        If Not IsDate(txtDateTo.Text) Then
            FieldsVarification = False
            MsgInformation("Invaild Date")
            txtDateTo.Focus()
            '    Exit Function
            'ElseIf FYChk((txtDateTo.Text)) = False Then
            '    FieldsVarification = False
            '    txtDateTo.Focus()
            '    Exit Function
        End If

        For I = 1 To SprdOption.MaxCols
            SprdOption.Row = 2
            SprdOption.Col = I
            If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                SprdOption.Row = 3
                SprdOption.Col = I
                If Trim(SprdOption.Text) = "" Then
                    FieldsVarification = False
                    MsgInformation("Blank Field.")
                    MainClass.SetFocusToCell(SprdOption, 3, I)
                    Exit Function
                End If
            End If
        Next

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function MakeSql(ByRef pIsGroup As String) As String

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mGroupBy As String
        Dim mGroupBy2 As String
        Dim mSqlStr As String
        Dim mOptionalTable As String
        Dim mOptionalJoining As String
        Dim mCATEGORY_CODE As String
        Dim CntLst As Integer
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim mInvoiceType As String
        Dim mDivisionCode As Double

        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String = ""

        SqlStr = " SELECT '' AS GROUP1,'' AS GROUP2, CC.COMPANY_NAME,"

        ''Collect the Group Field...

        For I = 1 To SprdOption.MaxCols
            SprdOption.Row = 1
            SprdOption.Col = I
            If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                mGroupBy = GetGroupBy(I, "1")
                If mGroupBy <> "" Then
                    SqlStr = SqlStr & vbCrLf & " " & mGroupBy & " AS GROUP_NAME,"
                    Exit For
                End If
            End If
        Next

        If lstFieldName.GetItemChecked(1) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " IH.BILLNO,"
        Else
            SqlStr = SqlStr & vbCrLf & " '' AS BILLNO,"
        End If

        If lstFieldName.GetItemChecked(2) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(IH.INVOICE_DATE,'DD/MM/YYYY') AS INVOICE_DATE,"
        Else
            SqlStr = SqlStr & vbCrLf & " '' AS INVOICE_DATE,"
        End If

        If lstFieldName.GetItemChecked(3) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " CMST.SUPP_CUST_CODE,"
        Else
            SqlStr = SqlStr & vbCrLf & " '' AS SUPP_CUST_CODE,"
        End If

        If lstFieldName.GetItemChecked(4) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " CMST.SUPP_CUST_NAME,"
        Else
            SqlStr = SqlStr & vbCrLf & " '' As SUPP_CUST_NAME,"
        End If

        If lstFieldName.GetItemChecked(5) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " CMST.SUPP_CUST_CITY,"
        Else
            SqlStr = SqlStr & vbCrLf & " '' AS SUPP_CUST_CITY,"
        End If

        If lstFieldName.GetItemChecked(11) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " AMST.PAN_NO,"
        Else
            SqlStr = SqlStr & vbCrLf & " '' AS PAN_NO,"
        End If

        If lstFieldName.GetItemChecked(6) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " ID.ITEM_CODE,"
        Else
            SqlStr = SqlStr & vbCrLf & " '' AS ITEM_CODE,"
        End If

        If lstFieldName.GetItemChecked(7) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " IM.ITEM_SHORT_DESC,"
        Else
            SqlStr = SqlStr & vbCrLf & " '' AS ITEM_SHORT_DESC,"
        End If

        If lstFieldName.GetItemChecked(8) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " ID.CUSTOMER_PART_NO,"
        Else
            SqlStr = SqlStr & vbCrLf & " '' AS CUSTOMER_PART_NO,"
        End If

        If lstFieldName.GetItemChecked(9) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " ID.ITEM_UOM,"
        Else
            SqlStr = SqlStr & vbCrLf & " '' AS ITEM_UOM,"
        End If

        If lstFieldName.GetItemChecked(10) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " ID.ITEM_RATE,"
        Else
            SqlStr = SqlStr & vbCrLf & " 0 AS ITEM_RATE,"
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(ID.CHARGEABLEGLASS_AREA * ID.ITEM_QTY)) AS SALEQTYSQM, "
        Else
            SqlStr = SqlStr & vbCrLf & " '' AS SALEQTYSQM, "
        End If

        If pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN INVOICESEQTYPE IN (0,4) THEN TOTQTY ELSE ID.ITEM_QTY END)) AS SALEQTY, "

            SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN INVOICESEQTYPE IN (0,4) THEN IH.ITEMVALUE ELSE ID.ITEM_AMT END)) AS SALEVALUE,"

            SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN CMST.GST_RGN_NO='" & RsCompany.Fields("COMPANY_GST_RGN_NO").Value & "' THEN 0 ELSE CASE WHEN INVOICESEQTYPE IN (0,4) THEN IH.NETCGST_AMOUNT ELSE ID.CGST_AMOUNT END END)) AS CGST_AMOUNT,"
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN CMST.GST_RGN_NO='" & RsCompany.Fields("COMPANY_GST_RGN_NO").Value & "' THEN 0 ELSE CASE WHEN INVOICESEQTYPE IN (0,4) THEN IH.NETSGST_AMOUNT ELSE ID.SGST_AMOUNT END END)) AS SGST_AMOUNT,"
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN CMST.GST_RGN_NO='" & RsCompany.Fields("COMPANY_GST_RGN_NO").Value & "' THEN 0 ELSE CASE WHEN INVOICESEQTYPE IN (0,4) THEN IH.NETIGST_AMOUNT ELSE ID.IGST_AMOUNT END END)) AS IGST_AMOUNT,"

            SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN INVOICESEQTYPE IN (0,4) THEN IH.TCSAMOUNT ELSE DECODE(IH.ITEMVALUE,0,0,ID.ITEM_AMT*IH.TCSAMOUNT/IH.ITEMVALUE) END)) AS TCS_AMOUNT,"

            SqlStr = SqlStr & vbCrLf & " TO_CHAR(0) AS OTHER_AMOUNT,"

            SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN CMST.GST_RGN_NO='" & RsCompany.Fields("COMPANY_GST_RGN_NO").Value & "' THEN CASE WHEN INVOICESEQTYPE IN (0,4) THEN IH.ITEMVALUE ELSE ID.ITEM_AMT END ELSE CASE WHEN INVOICESEQTYPE IN (0,4) THEN IH.ITEMVALUE+IH.NETCGST_AMOUNT+IH.NETSGST_AMOUNT+ IH.NETIGST_AMOUNT ELSE ID.ITEM_AMT+ID.CGST_AMOUNT+ID.SGST_AMOUNT+ ID.IGST_AMOUNT END END)) AS NET_AMOUNT,'1' AS IS_GROUP, '0' AS GROUP_FLAG"
        Else
            SqlStr = SqlStr & vbCrLf & " '' AS SALEQTY, "

            SqlStr = SqlStr & vbCrLf & " '' AS SALEVALUE,"

            SqlStr = SqlStr & vbCrLf & " '' AS CGST_AMOUNT,"
            SqlStr = SqlStr & vbCrLf & " '' AS SGST_AMOUNT,"
            SqlStr = SqlStr & vbCrLf & " '' AS IGST_AMOUNT,"

            SqlStr = SqlStr & vbCrLf & " '' AS TCS_AMOUNT,"

            SqlStr = SqlStr & vbCrLf & " '' AS OTHER_AMOUNT,"

            SqlStr = SqlStr & vbCrLf & " '' AS NET_AMOUNT,'0' AS IS_GROUP, '0' AS GROUP_FLAG"

        End If



        Call GetOptionTable(mOptionalTable, mOptionalJoining)

        SqlStr = SqlStr & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_BUSINESS_MST CMST, FIN_SUPP_CUST_MST AMST, FIN_SUPP_CUST_MST ACM," & vbCrLf _
            & " INV_DIVISION_MST IDIV, FIN_INVTYPE_MST IMST, GEN_COMPANY_MST CC"

        SqlStr = SqlStr & " ,INV_ITEM_MST IM"

        SprdOption.Row = 1
        SprdOption.Col = ColModel
        If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
            SqlStr = SqlStr & " ,INV_MODELWISE_PROD_DET MMST, GEN_MODEL_MST MODELMST"
        End If

        SprdOption.Row = 1
        SprdOption.Col = ColSalePerson1
        If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                SqlStr = SqlStr & " , FIN_SALESPERSON_MST SPMST"
            Else
                SqlStr = SqlStr & " , PAY_EMPLOYEE_MST SPMST"
            End If
        End If


        SqlStr = SqlStr & IIf(mOptionalTable = "", "", vbCrLf & mOptionalTable)

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE= CC.COMPANY_CODE " & vbCrLf _
            & " And IH.MKEY=ID.MKEY(+) And IH.CANCELLED='N'"

        ''            & " And IH.FYEAR='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf _

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID"

        SqlStr = SqlStr & vbCrLf _
            & " AND CMST.COMPANY_CODE=AMST.COMPANY_CODE " & vbCrLf _
            & " AND CMST.SUPP_CUST_CODE=AMST.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND IH.ACCOUNTCODE=ACM.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.COMPANY_CODE=IDIV.COMPANY_CODE " & vbCrLf _
            & " AND IH.DIV_CODE=IDIV.DIV_CODE"

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.TRNTYPE=IMST.CODE"

        SqlStr = SqlStr & vbCrLf _
            & " AND ID.COMPANY_CODE=IM.COMPANY_CODE(+) " & vbCrLf _
            & " AND ID.ITEM_CODE=IM.ITEM_CODE(+)"

        If cboReversalInvoice.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.BILLNO IN (" & vbCrLf _
                & " SELECT BILLNO FROM FIN_SUPP_SALE_HDR" & vbCrLf _
                & " WHERE COMPANY_CODE=IH.COMPANY_CODE" & vbCrLf _
                & " AND FYEAR=IH.FYEAR" & vbCrLf _
                & " AND SUPP_CUST_CODE=IH.SUPP_CUST_CODE" & vbCrLf _
                & " AND CANCELLED='N' AND ISFINALPOST='Y'" & vbCrLf _
                & " AND REASON = '6'" & vbCrLf _
                & " )"
        ElseIf cboReversalInvoice.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.BILLNO NOT IN (" & vbCrLf _
                & " SELECT BILLNO FROM FIN_SUPP_SALE_HDR" & vbCrLf _
                & " WHERE COMPANY_CODE=IH.COMPANY_CODE" & vbCrLf _
                & " AND FYEAR=IH.FYEAR" & vbCrLf _
                & " AND SUPP_CUST_CODE=IH.SUPP_CUST_CODE" & vbCrLf _
                & " AND CANCELLED='N' AND ISFINALPOST='Y'" & vbCrLf _
                & " AND REASON = '6'" & vbCrLf _
                & " )"
        End If


        SprdOption.Row = 1
        SprdOption.Col = ColModel
        If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
            ''SqlStr = SqlStr & " ,INV_MODELWISE_PROD_DET MMST, GEN_MODEL_MST MODELMST"
            SqlStr = SqlStr & vbCrLf & " AND ID.COMPANY_CODE=MMST.COMPANY_CODE(+) " & vbCrLf & " AND ID.ITEM_CODE=MMST.ITEM_CODE (+)"
            SqlStr = SqlStr & vbCrLf & " AND MMST.COMPANY_CODE=MODELMST.COMPANY_CODE(+) " & vbCrLf & " AND MMST.MODEL_CODE=MODELMST.MODEL_CODE(+)"
        End If

        SprdOption.Row = 1
        SprdOption.Col = ColSalePerson1
        If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                SqlStr = SqlStr & vbCrLf & " AND ''||IH.SALE_PERSON_CODE=''||SPMST.CODE(+)"
            Else
                SqlStr = SqlStr & vbCrLf & " AND ''||IH.SALE_PERSON_CODE=''||SPMST.EMP_CODE(+)"
            End If
        End If

        SqlStr = SqlStr & IIf(GetAttributeCode() = "", "", vbCrLf & GetAttributeCode())

        SqlStr = SqlStr & IIf(mOptionalJoining = "", "", vbCrLf & mOptionalJoining)

        'If CboItemType.SelectedIndex > 0 Then
        '    If MainClass.ValidateWithMasterTable((CboItemType.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
        '        mCATEGORY_CODE = IIf(IsDbNull(MasterNo), "", MasterNo)
        '    End If

        '    If Trim(mCATEGORY_CODE) <> "" Then
        '        SqlStr = SqlStr & vbCrLf & "AND IM.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCATEGORY_CODE) & "'"
        '    End If
        'End If

        'If cboDivision.SelectedIndex > 0 Then
        '    If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mDivisionCode = CDbl(Trim(MasterNo))
        '    End If
        '    SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        'End If

        'If CboItemType.SelectedIndex > 0 Then
        '    If MainClass.ValidateWithMasterTable(CboItemType.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
        '        mCATEGORY_CODE = IIf(IsDbNull(MasterNo), "", MasterNo)
        '    End If

        '    If Trim(mCATEGORY_CODE) <> "" Then
        '        SqlStr = SqlStr & vbCrLf & "AND IM.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCATEGORY_CODE) & "'"
        '    End If
        'End If

        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                '            lstInvoiceType.ListIndex = CntLst
                If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            End If
        Next

        If mTrnTypeStr <> "" Then
            mTrnTypeStr = "(" & mTrnTypeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND IH.TRNTYPE IN " & mTrnTypeStr & ""
        End If


        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
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


        If IsDate(txtDateFrom.Text) And IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        ''set Particular Condition...
        GetSubTitle = ""
        For I = 1 To SprdOption.MaxCols
            SprdOption.Row = 2
            SprdOption.Col = I
            If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                SprdOption.Row = 3
                SqlStr = GetConditionalQry(SqlStr, I, (SprdOption.Text), GetSubTitle)
            End If
        Next

        SqlStr = SqlStr & vbCrLf & "GROUP BY "

        If mGroupBy <> "" Then
            SqlStr = SqlStr & vbCrLf & "" & mGroupBy & ","
        End If

        If lstFieldName.GetItemChecked(1) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " IH.BILLNO,"
        End If

        If lstFieldName.GetItemChecked(2) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " IH.INVOICE_DATE,"
        End If

        If lstFieldName.GetItemChecked(3) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " CMST.SUPP_CUST_CODE,"
        End If

        If lstFieldName.GetItemChecked(4) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " CMST.SUPP_CUST_NAME,"
        End If

        If lstFieldName.GetItemChecked(5) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " CMST.SUPP_CUST_CITY,"
        End If

        ''
        If lstFieldName.GetItemChecked(11) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " AMST.PAN_NO,"
        End If

        If lstFieldName.GetItemChecked(6) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " ID.ITEM_CODE,"
        End If

        If lstFieldName.GetItemChecked(7) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " IM.ITEM_SHORT_DESC,"
        End If

        If lstFieldName.GetItemChecked(8) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " ID.CUSTOMER_PART_NO,"
        End If

        If lstFieldName.GetItemChecked(9) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " ID.ITEM_UOM,"
        End If

        If lstFieldName.GetItemChecked(10) = True And pIsGroup = "N" Then
            SqlStr = SqlStr & vbCrLf & " ID.ITEM_RATE,"
        End If

        SqlStr = SqlStr & vbCrLf & "CC.COMPANY_NAME"

        'MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        'SprdColTotal(ColSALESQMQTY, ColNetAmount)

        MakeSql = SqlStr
        Exit Function
InsertErr:
        MakeSql = ""
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function ShowSaleMIS() As Boolean

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mGroupBy As String
        Dim mGroupBy2 As String
        Dim mSqlStr As String
        Dim mOptionalTable As String
        Dim mOptionalJoining As String
        Dim mCATEGORY_CODE As String
        Dim CntLst As Integer
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim mInvoiceType As String
        Dim mDivisionCode As Double

        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String = ""

        Dim mSqlStr1 As String
        Dim mSqlStr2 As String

        SqlStr = " SELECT GROUP1,GROUP2,COMPANY_NAME, GROUP_NAME," & vbCrLf _
                & " BILLNO, INVOICE_DATE, SUPP_CUST_CODE, SUPP_CUST_NAME, SUPP_CUST_CITY, " & vbCrLf _
                & " PAN_NO, ITEM_CODE, ITEM_SHORT_DESC, CUSTOMER_PART_NO, " & vbCrLf _
                & " ITEM_UOM, ITEM_RATE, SALEQTYSQM, SALEQTY, SALEVALUE, " & vbCrLf _
                & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, TCS_AMOUNT, OTHER_AMOUNT, NET_AMOUNT,IS_GROUP, GROUP_FLAG FROM ( "

        If optSubTotalYes.Checked = True Then
            mSqlStr1 = MakeSql("Y")
            SqlStr = SqlStr & vbCrLf & mSqlStr1

            SqlStr = SqlStr & vbCrLf & " UNION ALL "
        End If

        mSqlStr2 = MakeSql("N")

        SqlStr = SqlStr & vbCrLf & mSqlStr2

        SqlStr = SqlStr & vbCrLf & ")"

        SqlStr = SqlStr & vbCrLf & "ORDER BY COMPANY_NAME, GROUP_NAME, IS_GROUP"


        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        SprdColTotal(ColSALESQMQTY, ColNetAmount)

        ShowSaleMIS = True
        Exit Function
InsertErr:
        ShowSaleMIS = False
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function GetOptionTable(ByRef pOptionalTable As String, ByRef pOptionJoining As String) As Object
        On Error GoTo ERR1
        Dim pSqlStr As String

        With SprdOption

            '        .Col = ColCustomer1
            '        .Row = 1
            '        If .Value = vbChecked Or cboWise.ListIndex = 0 Then
            '            pOptionalTable = pOptionalTable & ", FIN_SUPP_CUST_MST"
            '            pOptionJoining = pOptionJoining & " AND IH.COMPANY_CODE= FIN_SUPP_CUST_MST.COMPANY_CODE AND IH.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE"
            '        End If
            '
            '        .Col = ColInvoiceType1
            '        .Row = 1
            '        If .Value = vbChecked Or cboWise.ListIndex = 0 Then
            '            pOptionalTable = pOptionalTable & ", FIN_INVTYPE_MST INVTYPE"
            '            pOptionJoining = pOptionJoining & " AND IH.COMPANY_CODE= INVTYPE.COMPANY_CODE AND IH.TRNTYPE=INVTYPE.CODE"
            '        End If

            .Col = ColCategory1
            .Row = 1
            If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                pOptionalTable = ", INV_GENERAL_MST ITEMCAT"
                pOptionJoining = " AND IM.COMPANY_CODE=ITEMCAT.COMPANY_CODE AND IM.CATEGORY_CODE=ITEMCAT.GEN_CODE AND ITEMCAT.GEN_TYPE='C'"
            End If

            .Col = ColSubCategory1
            .Row = 1
            If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                pOptionalTable = ", INV_SUBCATEGORY_MST ITEMSUBCAT"
                pOptionJoining = " AND IM.COMPANY_CODE=ITEMSUBCAT.COMPANY_CODE AND IM.SUBCATEGORY_CODE=ITEMSUBCAT.SUBCATEGORY_CODE AND IM.CATEGORY_CODE=ITEMSUBCAT.CATEGORY_CODE"
            End If

        End With

        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function
    Private Sub frmParamSaleMIS_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormLoaded = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = " MIS Sales Reports"


        FormLoaded = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
        'Resume
    End Sub

    Private Sub frmParamSaleMIS_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0

        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)
        MainClass.SetControlsColor(Me)

        optType(0).Checked = True

        txtDateFrom.Text = RsCompany.Fields("START_DATE").Value
        txtDateTo.Text = CStr(RunDate)
        Call PrintStatus(True)

        Call FillCboWise()
        Call FillCboItemType()
        Call FillInvoiceType()

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

        cboReversalInvoice.Items.Clear()
        cboReversalInvoice.Items.Add("ALL")
        cboReversalInvoice.Items.Add("YES")
        cboReversalInvoice.Items.Add("NO")
        cboReversalInvoice.SelectedIndex = 0

        'SprdMain.DataSource = AData1
        FormatSprdOption(-1)
        FormatSprdMain(-1)
        FillSprdMain()
        FormLoaded = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
err_Renamed:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
        ''Resume
    End Sub
    Private Sub FillCboWise()
        On Error GoTo FillERR
        cboWise.Items.Clear()
        cboWise.Items.Add("Customer")
        cboWise.Items.Add("Item Code")
        cboWise.Items.Add("HSN Code")
        cboWise.Items.Add("Item Desc")
        cboWise.Items.Add("Bill No")
        cboWise.Items.Add("Division")
        cboWise.Items.Add("Invoice Type")
        cboWise.Items.Add("Company Code")
        cboWise.SelectedIndex = 0
        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub FillCboItemType()

        On Error GoTo FillErr2
        CboItemType.Items.Clear()
        CboItemType.Items.Add("All")

        MainClass.FillCombo(CboItemType, "INV_GENERAL_MST", "GEN_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'")

        CboItemType.SelectedIndex = 0
        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub

    Private Sub FillInvoiceType()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer

        lstInvoiceType.Items.Clear()
        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='S' ORDER BY NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstInvoiceType.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstInvoiceType.Items.Add(RS.Fields("Name").Value)
                lstInvoiceType.SetItemChecked(CntLst, False)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstInvoiceType.SelectedIndex = 0

        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_NAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstCompanyName.Items.Add(RS.Fields("COMPANY_NAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(RS.Fields("COMPANY_NAME").Value = RsCompany.Fields("COMPANY_NAME").Value, True, False))
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCompanyName.SelectedIndex = 0


        lstFieldName.Items.Clear()

        lstFieldName.Items.Add("ALL")
        lstFieldName.SetItemChecked(0, True)

        lstFieldName.Items.Add("Bill No")
        lstFieldName.SetItemChecked(1, True)

        lstFieldName.Items.Add("Bill Date")
        lstFieldName.SetItemChecked(2, True)

        lstFieldName.Items.Add("Customer Code")
        lstFieldName.SetItemChecked(3, True)

        lstFieldName.Items.Add("Customer Name")
        lstFieldName.SetItemChecked(4, True)

        lstFieldName.Items.Add("Customer Location")
        lstFieldName.SetItemChecked(5, True)

        lstFieldName.Items.Add("Item Code")
        lstFieldName.SetItemChecked(6, True)

        lstFieldName.Items.Add("Item Name")
        lstFieldName.SetItemChecked(7, True)

        lstFieldName.Items.Add("Part No")
        lstFieldName.SetItemChecked(8, True)

        lstFieldName.Items.Add("Item Unit")
        lstFieldName.SetItemChecked(9, True)

        lstFieldName.Items.Add("Item Rate")
        lstFieldName.SetItemChecked(10, True)

        lstFieldName.Items.Add("PAN No")
        lstFieldName.SetItemChecked(11, True)


        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub lstInvoiceType_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstInvoiceType.ItemCheck
        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstInvoiceType.GetItemChecked(0) = True Then
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstInvoiceType.GetItemChecked(e.Index - 1) = False Then
                    lstInvoiceType.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
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
    Private Sub lstFieldName_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstFieldName.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstFieldName.GetItemChecked(0) = True Then
                    For I = 1 To lstFieldName.Items.Count - 1
                        lstFieldName.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstFieldName.Items.Count - 1
                        lstFieldName.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstFieldName.GetItemChecked(e.Index - 1) = False Then
                    lstFieldName.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmParamSaleMIS_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub optType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optType.GetIndex(eventSender)
            cboWise.Enabled = IIf(Index = 0, True, False)
            PrintStatus(False)
        End If
    End Sub

    Private Sub SprdOption_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdOption.ButtonClicked

        If eventArgs.row = 1 Then
            If eventArgs.buttonDown = System.Windows.Forms.CheckState.Checked Then Exit Sub

            If eventArgs.col = SprdOption.ActiveCol Then
                SprdOption.Row = 1
                SprdOption.Col = SprdOption.ActiveCol
                If SprdOption.Col = ColItemCode1 Then
                    GroupOnItem = True
                Else
                    GroupOnItem = False
                End If
                SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked)
            End If
        End If

        If eventArgs.row = 2 Then
            If eventArgs.buttonDown = System.Windows.Forms.CheckState.Checked Then
                SprdOption.Row = 2
                SprdOption.Col = SprdOption.ActiveCol
                MainClass.ProtectCell(SprdOption, 3, 3, SprdOption.ActiveCol, SprdOption.ActiveCol)
                Exit Sub
            End If
            SprdOption.Row = 2
            SprdOption.Col = SprdOption.ActiveCol
            MainClass.UnProtectCell(SprdOption, 3, 3, SprdOption.ActiveCol, SprdOption.ActiveCol)
            MainClass.ProtectCell(SprdOption, 3, 3, IIf(SprdOption.MaxCols = SprdOption.ActiveCol, 1, SprdOption.ActiveCol + 1), IIf(SprdOption.MaxCols = SprdOption.ActiveCol, SprdOption.MaxCols - 1, SprdOption.MaxCols))
        End If
    End Sub

    Private Sub SprdOption_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdOption.Change

        SprdOption.Row = 2
        SprdOption.Col = eventArgs.col
        If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
            SprdOption.Row = 3
            SprdOption.Col = eventArgs.col
            SprdOption.Text = ""
            Exit Sub
        End If
        PrintStatus(False)
    End Sub

    Private Sub SprdOption_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdOption.ClickEvent
        Dim mColValue As String
        Dim I As Integer
        Dim mCategoryCode As String
        Dim mCategory As String

        If eventArgs.col = 0 Then Exit Sub

        If eventArgs.row = 1 Then
            SprdOption.Row = 1
            SprdOption.Col = eventArgs.col
            mColValue = SprdOption.Value
            For I = 1 To SprdOption.MaxCols
                SprdOption.Col = I
                SprdOption.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            Next
        End If

        SprdOption.Row = 2
        SprdOption.Col = eventArgs.col
        If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then Exit Sub

        If eventArgs.row = 0 Then
            Select Case eventArgs.col
                Case ColCustomer1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                Case ColItemCode1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "CUSTOMER_PART_NO")
                Case ColHSNCode1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "GEN_HSN_MST", "HSN_CODE", "HSN_DESC", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                Case ColItemDesc1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "FIN_ITEMTYPE_MST", "NAME", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                Case ColBillNo1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "FIN_INVOICE_HDR", "BILLNO", "INVOICE_DATE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                Case ColInvoiceType1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "FIN_INVTYPE_MST", "NAME", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'")
                Case ColAccountHead1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='O'")
                Case ColDivision1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "INV_DIVISION_MST", "DIV_DESC", "DIV_CODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                Case ColCategory1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'")
                Case ColSubCategory1
                    SprdOption.Row = 3
                    SprdOption.Col = ColCategory1
                    mCategory = Trim(SprdOption.Text)
                    If mCategory = "" Then
                        MsgInformation("Please First Select Category.")
                        Exit Sub
                    End If
                    If MainClass.ValidateWithMasterTable(mCategory, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                        mCategoryCode = MasterNo
                        Call SearchColMaster(eventArgs.row, eventArgs.col, "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCategoryCode & "' ")
                    End If
                Case ColSalePerson1
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                        Call SearchColMaster(eventArgs.row, eventArgs.col, "FIN_SALESPERSON_MST", "NAME", "CODE", "")
                    Else
                        Call SearchColMaster(eventArgs.row, eventArgs.col, "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                    End If
                    ' SPMST"  SPMST"
            End Select
        End If
        PrintStatus(False)
    End Sub
    Private Sub PrintStatus(ByRef PrintFlag As Boolean)
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
    End Sub
    Private Sub SearchColMaster(ByRef mRow As Integer, ByRef mCol As Integer, ByRef mTable As String, ByRef mField As String, ByRef mField1 As String, Optional ByRef mConditional As String = "", Optional ByRef mField2 As String = "", Optional ByRef mField3 As String = "")

        With SprdOption
            SprdOption.Row = 3
            SprdOption.Col = mCol

            If MainClass.SearchGridMaster((SprdOption.Text), mTable, mField, mField1, mField2, mField3, mConditional) = True Then
                '        If MainClass.SearchMaster(SprdOption.Text, mTable, mField, mConditional) = True Then
                .Row = 3
                .Col = mCol
                .Text = AcName
            End If
            MainClass.SetFocusToCell(SprdOption, SprdOption.ActiveRow, IIf(SprdOption.MaxCols > mCol, mCol + 1, 1))
        End With
    End Sub
    Private Function GetConditionalQry(ByRef mSqlStr As String, ByRef ColCheck As Integer, ByRef DataFieldName As String, ByRef GetSubTitle As String) As String
        On Error GoTo ERR1
        Dim FieldName As String
        GetConditionalQry = mSqlStr
        FieldName = GetGroupBy(ColCheck, "1")

        If Mid(FieldName, 1, InStr(1, FieldName, ".") - 1) = "FIN_SUPP_CUST_MST" Then
            If MainClass.ValidateWithMasterTable(DataFieldName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                GetConditionalQry = GetConditionalQry & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MasterNo & "'"
            Else
                GetConditionalQry = GetConditionalQry & vbCrLf & "AND IH.SUPP_CUST_CODE=''"
            End If
        ElseIf InStr(FieldName, "SUPP_CUST_NAME", CompareMethod.Text) > 0 Then
            GetConditionalQry = GetConditionalQry & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & DataFieldName & "'"
        Else
            GetConditionalQry = GetConditionalQry & vbCrLf & "AND " & FieldName & "='" & DataFieldName & "'"
        End If

        SprdOption.Row = 0
        GetSubTitle = GetSubTitle & IIf(GetSubTitle <> "", " AND ", "") & SprdOption.Text & " : " & DataFieldName
        'GetConditionalQry = GetConditionalQry & vbCrLf & " AND " & FieldName & "='" & mainclass.AllowSingleQuote(Trim(DataFieldName)) & "' "
        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function
    Private Function GetGroupBy(ByRef ColGroup As Integer, ByRef pFieldSeq As Integer) As String
        On Error GoTo ERR1
        Dim mFieldName As String
        If ColGroup <> ColCustomer1 And pFieldSeq > 1 Then Exit Function

        Select Case ColGroup
            Case ColThickness1
                mFieldName = "IM.MAT_THICHNESS"
            Case ColColor1
                mFieldName = "IM.ITEM_COLOR"
            Case ColProductType1
                mFieldName = "IM.PRODTYPE_DESC"
            Case ColCustomerGroup1
                mFieldName = "AMST.CUSTOMER_GROUP"
            Case ColSalePerson1
                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                    mFieldName = "SPMST.NAME"
                Else
                    mFieldName = "SPMST.EMP_NAME"
                End If
            Case ColPANNo1
                mFieldName = "AMST.PAN_NO"
            Case ColCustomer1
                mFieldName = "CMST.SUPP_CUST_NAME"
            Case ColItemCode1
                mFieldName = "IM.ITEM_CODE"
            Case ColDivision1
                mFieldName = "IDIV.DIV_DESC"
            Case ColCategory1
                mFieldName = "ITEMCAT.GEN_DESC"
            Case ColSubCategory1
                mFieldName = "ITEMSUBCAT.SUBCATEGORY_DESC"
            Case ColHSNCode1
                mFieldName = "ID.HSNCODE"
            Case ColItemDesc1
                mFieldName = "IH.ITEMDESC"
            Case ColBillNo1
                mFieldName = "IH.BILLNO"
            Case ColAgtD31
                mFieldName = "IH.AGTD3"
            Case ColRejection1
                mFieldName = "IH.REJECTION"
            Case ColFOC1
                mFieldName = "IH.FOC"
            Case ColInvoiceType1
                mFieldName = "IMST.NAME" ''"IH.CANCELLED"
            Case ColAccountHead1
                mFieldName = "ACM.SUPP_CUST_NAME" ''"IH.CANCELLED"
            Case ColAgtCT31
                mFieldName = "IH.AGTCT3"
            Case ColAgtCT11
                mFieldName = "IH.AGTCT1"
            Case ColDutyForegone1
                mFieldName = "IH.ISDUTY_FORGONE"
            Case ColHSNPer
                mFieldName = "ID.CGST_PER + ID.SGST_PER + ID.IGST_PER"
            Case ColModel
                mFieldName = "MODELMST.MODEL_DESC"
        End Select
        GetGroupBy = mFieldName
        Exit Function
ERR1:
        GetGroupBy = ""
    End Function
    Private Function FillFieldName(ByRef ColGroup As Integer) As Object
        On Error GoTo ERR1
        Dim mFieldName As String
        Select Case ColGroup
            Case ColCustomer1
                mFieldName = "Customer"
            Case ColItemCode1
                mFieldName = "Item Code"
            Case ColHSNCode1
                mFieldName = "HSN Code"
            Case ColItemDesc1
                mFieldName = "Item Desc"
            Case ColBillNo1
                mFieldName = "Bill No"
            Case ColAgtD31
                mFieldName = "Agt D3"
            Case ColRejection1
                mFieldName = "Rejection"
            Case ColFOC1
                mFieldName = "FOC"
            Case ColInvoiceType1
                mFieldName = "Invoice Type" ''"Cancelled"
            Case ColAccountHead1
                mFieldName = "Account Head" ''"Cancelled"
            Case ColDivision1
                mFieldName = "Division"
            Case ColCategory1
                mFieldName = "Category"
            Case ColSubCategory1
                mFieldName = "Sub Category"
            Case ColAgtCT31
                mFieldName = "Agt CT3"
            Case ColAgtCT11
                mFieldName = "Agt CT1"
            Case ColDutyForegone1
                mFieldName = "Duty Foregone"
            Case ColHSNPer
                mFieldName = "GST Percentage"
            Case ColModel
                mFieldName = "Model"
            Case ColPANNo1
                mFieldName = "PAN No"
            Case ColSalePerson1
                mFieldName = "Sale Person Name"
            Case ColThickness1
                mFieldName = "Thickness"
            Case ColColor1
                mFieldName = "Color"
            Case ColProductType1
                mFieldName = "Product Type"
            Case ColCustomerGroup1
                mFieldName = "Customer Group"
        End Select




        FillFieldName = mFieldName
        Exit Function
ERR1:
        FillFieldName = ""
    End Function

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        'If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then
        '    txtDateFrom.Focus()
        '    Cancel = True
        '    GoTo EventExitSub
        'End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        'If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
        '    txtDateTo.Focus()
        '    Cancel = True
        '    GoTo EventExitSub
        'End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub GroupByColor()
        Dim mGroup As String
        Dim cntRow As Integer
        Dim mBlackColor As Color ' Integer
        Dim mOpening As Double
        Dim mReceipt As Double
        Dim mIssue As Double
        Dim mTotClosing As Double
        Dim mClosing As Double

        mBlackColor = PubSpdMainColor '' &H80FF80

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColGrouping
                If mGroup <> Trim(.Text) Then
                    If mBlackColor = PubSpdMainColor Then ''&HFFFF00 Then
                        mBlackColor = PubSpdAlterColor '' &H80FF80
                    Else
                        mBlackColor = PubSpdMainColor ' &HFFFF00
                    End If
                    mGroup = Trim(.Text)
                    mTotClosing = 0
                End If

                .Row = cntRow
                .Row2 = cntRow
                .Col = 1
                .Col2 = .MaxCols
                .BlockMode = True
                .BackColor = mBlackColor 'System.Drawing.ColorTranslator.FromOle(mBlackColor) ''&HFFFF00
                .BlockMode = False
            Next
        End With
    End Sub

    Private Sub HideUnHide()

        SprdMain.Col = ColBillNo
        If lstFieldName.GetItemChecked(1) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

        SprdMain.Col = ColBillDate
        If lstFieldName.GetItemChecked(2) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

        SprdMain.Col = ColCustomerCode
        If lstFieldName.GetItemChecked(3) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

        SprdMain.Col = ColCustomerName
        If lstFieldName.GetItemChecked(4) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

        SprdMain.Col = ColCustomerLocation
        If lstFieldName.GetItemChecked(5) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

        SprdMain.Col = ColCode
        If lstFieldName.GetItemChecked(6) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

        SprdMain.Col = ColItemName
        If lstFieldName.GetItemChecked(7) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

        SprdMain.Col = ColPartNo
        If lstFieldName.GetItemChecked(8) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

        SprdMain.Col = ColUnit
        If lstFieldName.GetItemChecked(9) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

        SprdMain.Col = ColSALERate
        If lstFieldName.GetItemChecked(10) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

        SprdMain.Col = ColPANNo
        If lstFieldName.GetItemChecked(11) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

    End Sub

    Private Sub SprdColTotal(ByRef Col As Integer, ByRef col2 As Integer)
        Dim cntCol As Integer
        Dim cntRow As Integer
        Dim TotCol As Double

        With SprdMain
            .MaxRows = .MaxRows + 1
            SprdMain.Col = ColGrouping
            SprdMain.Row = SprdMain.MaxRows
            SprdMain.Text = "TOTAL :"
            SprdMain.Font = VB6.FontChangeBold(SprdMain.Font, True)

            For cntCol = Col To col2
                .Col = cntCol
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    TotCol = TotCol + IIf(IsNumeric(.Text), .Text, 0)
                Next
                .Row = .MaxRows
                .Text = VB6.Format(TotCol, "0.00")
                .Font = VB6.FontChangeBold(.Font, True)
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                TotCol = 0
            Next
        End With
    End Sub
    Private Function GetAttributeCode() As String

        On Error GoTo ERR1
        Dim pSqlStr As String
        Dim mCategoryCode As String

        With SprdOption
            .Col = ColItemCode1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = "IM.ITEM_CODE='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColCustomer1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColPANNo1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "AMST.PAN_NO='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColThickness1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "IM.MAT_THICHNESS='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColColor1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "IM.ITEM_COLOR='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If


            .Col = ColProductType1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "IM.PRODTYPE_DESC='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColCustomerGroup1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "AMST.CUSTOMER_GROUP='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColSalePerson1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "SPMST.NAME='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColDivision1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "IDIV.DIV_DESC='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColInvoiceType1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "IMST.NAME='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColAccountHead1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColCategory1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                If MainClass.ValidateWithMasterTable(.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "IM.CATEGORY_CODE='" & MasterNo & "'"
                End If
            End If

            .Col = ColSubCategory1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then

                .Col = ColCategory1
                .Row = 3
                If MainClass.ValidateWithMasterTable(.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mCategoryCode = MasterNo
                End If

                .Col = ColSubCategory1
                .Row = 3

                If MainClass.ValidateWithMasterTable(.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCategoryCode & "'") = True Then
                    pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "IM.SUBCATEGORY_CODE='" & MasterNo & "'"
                End If
            End If

            '        .Col = ColModel1
            '        .Row = 2
            '        If .Value = vbUnchecked Then
            '            .Row = 3
            '            pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "IM.ITEM_MODEL='" & MainClass.AllowSingleQuote(.Text) & "'"
            '        End If

            '        .Col = ColMake1
            '        .Row = 2
            '        If .Value = vbUnchecked Then
            '            .Row = 3
            '            pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "IM.ITEM_MAKE='" & MainClass.AllowSingleQuote(.Text) & "'"
            '        End If

            '        .Col = ColColor1
            '        .Row = 2
            '        If .Value = vbUnchecked Then
            '            .Row = 3
            '            pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "IM.ITEM_COLOR='" & MainClass.AllowSingleQuote(.Text) & "'"
            '        End If

            '        .Col = ColDept1
            '        .Row = 2
            '        If .Value = vbUnchecked Then
            '            .Row = 3
            '            pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "( STOCK.DEPT_CODE_TO='" & MainClass.AllowSingleQuote(.Text) & "' OR STOCK.DEPT_CODE_FROM='" & MainClass.AllowSingleQuote(.Text) & "')"
            '            'SqlStr = SqlStr & vbCrLf & " AND ( STOCK.DEPT_CODE_TO='" & MasterNo & "' OR STOCK.DEPT_CODE_FROM='" & MasterNo & "')"
            '        End If

            '.Col = ColTariffHeading1
            '.Row = 2
            'If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
            '    .Row = 3
            '    If MainClass.ValidateWithMasterTable(.Text, "TARRIF_DESC", "TARRIF_CODE", "FIN_TARRIF_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '        pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "IM.TARIFF_CODE='" & MasterNo & "'"
            '    End If
            'End If

        End With
        GetAttributeCode = IIf(pSqlStr = "", "", " AND ") & pSqlStr
        Exit Function
ERR1:
        '    Resume
        MsgBox(Err.Description)
        GetAttributeCode = ""
    End Function

    Private Sub frmParamSaleMIS_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdOption.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        'Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1, False)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_KeyPressEvent(sender As Object, e As _DSpreadEvents_KeyPressEvent) Handles SprdMain.KeyPressEvent
        Try
            If e.keyAscii = 18 Then
                Dim mFieldValue As String
                Dim I As Long
                Dim mDelRow As Long


                mDelRow = SprdMain.ActiveRow

                SprdMain.Row = mDelRow
                SprdMain.Col = ColGrouping
                mFieldValue = Trim(SprdMain.Text)

                If mFieldValue = "TOTAL :" Then
                    Exit Sub
                End If

                SprdMain.DeleteRows(mDelRow, 1)
                'SprdMain.Action = SS_ACTION_DELETE_ROW
                If SprdMain.MaxRows > 1 Then SprdMain.MaxRows = SprdMain.MaxRows - 1

                For I = SprdMain.MaxRows To 1 Step -1
                    SprdMain.Row = I
                    SprdMain.Col = ColGrouping
                    mFieldValue = Trim(SprdMain.Text)
                    '"TOTAL :"
                    If mFieldValue = "TOTAL :" Then
                        SprdMain.Action = SS_ACTION_DELETE_ROW
                        If SprdMain.MaxRows > 1 Then SprdMain.MaxRows = SprdMain.MaxRows - 1
                    End If
                Next

                SprdColTotal(ColSALESQMQTY, ColNetAmount)

                'FormatSprdMain(-1)

                'FillSprdMain()
                'GroupByColor()

                'Call PrintStatus(True)
                'SprdMain.Focus()

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GroupBySpread(ByRef Col As Long)
        'Group the data by the specified column
        Dim i As Integer
        Dim currentrow As Long
        Dim lastid As String
        Dim prevtext As Object = Nothing
        Dim lastheaderrow As Long
        Dim ret As Boolean
        Dim Currentid As String

        'Turn off the redraw
        SprdMain.ReDraw = False

        'Reset the header bolds and make the sort col bold
        BoldHeader(Col)

        'Sort the data on the specified column
        'SortData(Col)

        'Reset the max columns to allow for the inserted "gouping" picture columns
        'SprdMain.MaxCols = SprdMain.MaxCols + 2
        'Insert 2 columns at beginning
        For i = 1 To 2
            'SprdMain.InsertCols(i, 1)

            'Change col width
            'SprdMain.colwidth(i) = 2
            SprdMain.set_ColWidth(i, 2)
        Next i

        'Change background color of the first inserted column
        SprdMain.Col = 1
        SprdMain.Row = -1
        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ' &H8000000F    'Gray

        'Init variables	
        lastheaderrow = 0
        currentrow = 1
        lastid = ""

        While currentrow <= SprdMain.DataRowCnt

            SprdMain.Row = currentrow
            SprdMain.Col = ColGrouping
            Currentid = UCase(Trim(SprdMain.Text))
            If Currentid <> lastid Then
                If lastheaderrow <> 0 Then
                    SprdMain.SetRowItemData(lastheaderrow, (SprdMain.Row - lastheaderrow - 1))
                    '                mRecordCount = SprdMain.GetRowItemData(lastheaderrow)	
                End If

                lastid = UCase(Trim(SprdMain.Text))

                lastheaderrow = currentrow

                'Insert a new header row	
                InsertHeaderRow(currentrow, 0)
            Else
                MakePictureCellType((SprdMain.Row), ColPicSub)
                SprdMain.Col = ColPicSub
                SprdMain.TypePictPicture = minuspict
                SprdMain.SetCellBorder(ColPicSub, SprdMain.Row, ColPicSub, SprdMain.Row, SS_BORDER_TYPE_LEFT, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)
            End If
            SprdMain.Row = SprdMain.Row + 1
            currentrow = currentrow + 1

        End While

        'Display last read data	
        SprdMain.SetRowItemData(lastheaderrow, (SprdMain.Row - lastheaderrow))
        SprdMain.MaxRows = SprdMain.DataRowCnt
        SprdMain.SetActiveCell(1, 1)

        'Paint Spread	
        SprdMain.ReDraw = True

        'Update displays	
        System.Windows.Forms.Application.DoEvents()

        ''Init variables
        'lastheaderrow = 0
        'currentrow = 1
        'lastid = " "


        ''Loop through all rows
        'While currentrow <= SprdMain.DataRowCnt

        '    SprdMain.Row = currentrow
        '    SprdMain.Col = Col   'adjust for 2 inserted cols
        '    'Compare Ids to see if new
        '    If UCase(Trim(SprdMain.Text)) <> lastid Then
        '        'New ID
        '        'Set the number of rows "associated" with the previous group
        '        If lastheaderrow <> 0 Then
        '            'Set the item data with the number of rows for this grouping
        '            SprdMain.SetRowItemData(lastheaderrow, (SprdMain.Row - lastheaderrow - 1))
        '            prevtext = Nothing
        '            ret = SprdMain.GetText(ColPicSub, lastheaderrow, prevtext)
        '            'Set the header row text
        '            'SprdMain.SetText(ColAccountCode, lastheaderrow, prevtext & "     " & SprdMain.GetRowItemData(lastheaderrow) & " item(s)")
        '        End If

        '        'Init new variables
        '        SprdMain.Col = Col
        '        lastid = UCase(Trim(SprdMain.Text))
        '        lastheaderrow = currentrow

        '        'Insert a new header row
        '        InsertHeaderRow(currentrow, SprdMain.Text)

        '        'Update counters
        '        SprdMain.Row = SprdMain.Row + 1
        '        currentrow = currentrow + 1
        '        'Label4.Caption = currentrow
        '    End If

        '    'Add the picture for expanding/collapsing
        '    MakePictureCellType(SprdMain.Row, ColPicMain)
        '    SprdMain.Col = ColPicMain
        '    SprdMain.TypePictPicture = minuspict

        '    'Add left border

        '    SprdMain.SetCellBorder(ColAccountCode, SprdMain.Row, ColAccountCode, SprdMain.Row, SS_BORDER_TYPE_LEFT, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)

        '    currentrow = currentrow + 1

        'End While

        ''Display last read data
        'SprdMain.SetRowItemData(lastheaderrow, (SprdMain.Row - lastheaderrow))
        'prevtext = Nothing
        'ret = SprdMain.GetText(ColPicSub, lastheaderrow, prevtext)
        ''SprdMain.SetText(ColPicSub, lastheaderrow, prevtext & "     " & SprdMain.GetRowItemData(lastheaderrow) & " item(s)")

        ''Set the max rows = number or records
        'SprdMain.MaxRows = SprdMain.DataRowCnt

        ''Make the first cell active
        'SprdMain.SetActiveCell(1, 1)


        ''Paint Spread
        'SprdMain.ReDraw = True

        ''Update displays
        ''pb1.Value = 0
        ''Label4.Caption = "0"
        ''DoEvents

        ''Screen.MousePointer = 0
    End Sub
    Private Sub MakePictureCellType(Row As Long, Col As Integer)
        'Define specified cell as type PICTURE

        SprdMain.Col = Col
        SprdMain.Row = Row

        SprdMain.CellType = FPSpreadADO.CellTypeConstants.CellTypePicture ' CellTypePicture
        SprdMain.TypePictCenter = True
        SprdMain.TypePictMaintainScale = False
        SprdMain.TypePictStretch = False

    End Sub


    Private Sub InsertHeaderRow(rownum As Long, coltext As String)
        'Insert a header row at the specifed location

        'SprdMain.InsertRows(rownum, 1)
        'SprdMain.MaxRows = SprdMain.MaxRows + 1

        SprdMain.Col = -1
        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ' &H8000000F   'Gray
        SprdMain.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) ' &HC00000     'Blue
        SprdMain.FontBold = True

        MakePictureCellType(rownum, 1)

        SprdMain.Col = 1
        SprdMain.TypePictPicture = minuspict
        SprdMain.Col = ColPicSub
        SprdMain.Text = coltext

        'Add picture state values
        SprdMain.Col = ColFlag
        SprdMain.Text = "0"

        'Add Border

        SprdMain.SetCellBorder(1, rownum, SprdMain.MaxCols, rownum, SS_BORDER_TYPE_OUTLINE, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)

    End Sub
    Private Sub BoldHeader(Col As Long)
        'Reset the header bolds and make the sort col bold

        'Change font for visual cue to what column sorting on
        'Reset all header fonts
        SprdMain.Row = 0
        SprdMain.Col = -1
        SprdMain.FontBold = False

        'Bold the specified column
        SprdMain.Row = 0
        SprdMain.Col = Col
        SprdMain.FontBold = True

    End Sub
    Private Sub SortData(Col As Long)
        'Sort the data on the specified column

        SprdMain.Sort(1, 1, SprdMain.MaxCols, SprdMain.DataRowCnt, FPSpreadADO.SortByConstants.SortByRow, Col, SS_SORT_ORDER_ASCENDING)

    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        'Sort on specified column or show/collapse rows	

        'Show Summary/Detail info.	
        'If clicked on a "+" or "-" grouping	

        If eventArgs.col = ColPicMain Then
            SprdMain.Col = ColPicMain
            SprdMain.Row = eventArgs.row
            If SprdMain.CellType = FPSpreadADO.CellTypeConstants.CellTypePicture Then
                'Show or hide the specified rows	
                ShowHideRows(ColPicMain, eventArgs.row)
            End If
        End If
    End Sub
    Private Sub ShowHideRows(ByRef Col As Integer, ByRef Row As Integer)
        'Collapse or uncollape the specified rows	
        Dim i As Short
        Dim collapsetype As Short

        SprdMain.Row = Row
        SprdMain.Col = ColFlag

        If SprdMain.Text = "0" Then
            collapsetype = 0 'collape/hide rows : minus picture	
            SprdMain.Col = ColPicMain
            SprdMain.TypePictPicture = pluspict
            SprdMain.Col = ColFlag
            SprdMain.Text = "1"
        Else
            collapsetype = 1 'uncollapse / show rows: plus picture	
            SprdMain.Col = ColPicMain
            SprdMain.TypePictPicture = minuspict
            SprdMain.Col = ColFlag
            SprdMain.Text = "0"
        End If

        SprdMain.ReDraw = False
        For i = 1 To SprdMain.GetRowItemData(Row)
            SprdMain.Row = SprdMain.Row + 1
            If collapsetype = 0 Then
                SprdMain.RowHidden = True
            Else
                SprdMain.RowHidden = False
            End If
        Next i
        SprdMain.ReDraw = True

    End Sub
End Class
