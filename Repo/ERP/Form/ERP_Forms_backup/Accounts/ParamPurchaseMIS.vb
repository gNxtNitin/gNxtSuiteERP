Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Friend Class frmParamPurchaseMIS
    Inherits System.Windows.Forms.Form
    Dim FormLoaded As Boolean

    Dim GroupOnItem As Boolean
    'Dim PvtDBCn As ADODB.Connection

    Private Const RowHeight As Short = 12

    Private Const ColCompanyCode As Short = 1
    Private Const ColGrouping As Short = 2
    Private Const ColBillNo As Short = 3
    Private Const ColBillDate As Short = 4
    Private Const ColCustomerCode As Short = 5
    Private Const ColCustomerName As Short = 6
    Private Const ColCustomerLocation As Short = 7
    Private Const ColCustomerPANNo As Short = 8
    Private Const ColCode As Short = 9
    Private Const ColItemName As Short = 10
    Private Const ColPartNo As Short = 11
    Private Const ColCategory As Short = 12
    Private Const ColUnit As Short = 13
    Private Const ColSALERate As Short = 14
    Private Const ColSALEQTY As Short = 15
    Private Const ColSALEVALUE As Short = 16
    Private Const ColCGST As Short = 17
    Private Const ColSGST As Short = 18
    Private Const ColIGST As Short = 19
    Private Const ColTCS As Short = 20
    Private Const ColOthers As Short = 21
    Private Const ColNetAmount As Short = 22

    'Private Const ColGrouping As Short = 1
    'Private Const ColCode As Short = 2
    'Private Const ColItemName As Short = 3
    'Private Const ColUnit As Short = 4
    'Private Const ColSALEQTY As Short = 5
    'Private Const ColSALEVALUE As Short = 6
    'Private Const ColCGST As Short = 7
    'Private Const ColSGST As Short = 8
    'Private Const ColIGST As Short = 9
    'Private Const ColExciseDuty As Short = 10
    'Private Const ColCessTax As Short = 11
    'Private Const ColSHCessTax As Short = 12
    'Private Const ColVAT As Short = 13
    'Private Const ColCST As Short = 14
    'Private Const ColOthers As Short = 15

    Private Const ColCustomer1 As Short = 1
    Private Const ColDivision1 As Short = 2
    Private Const ColItemCode1 As Short = 3
    Private Const ColCategory1 As Short = 4
    Private Const ColSubCategory1 As Short = 5
    Private Const ColTariffHeading1 As Short = 6
    Private Const ColItemDesc1 As Short = 7
    Private Const ColSalesTax1 As Short = 8
    Private Const ColBillNo1 As Short = 9
    Private Const ColAgtD31 As Short = 10
    Private Const ColCancelled1 As Short = 11
    Private Const ColInvType1 As Short = 12
    Private Const ColThickness1 As Short = 13

    Dim mActiveRow As Integer
    Dim PrintFlag As Boolean
    Dim mClickProcess As Boolean
    Dim GetSubTitle As String

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboWise_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboWise.SelectedIndexChanged
        PrintStatus(False)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub FillSprdMain()
        On Error GoTo ERR1
        Dim I As Integer
        Dim mField As String


        With SprdMain
            .MaxCols = ColNetAmount
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

            .Col = ColBillNo
            .Text = "V No"

            .Col = ColBillDate
            .Text = "V Date"

            .Col = ColCustomerCode
            .Text = "Customer Code"

            .Col = ColCustomerName
            .Text = "Customer Name"

            .Col = ColCustomerLocation
            .Text = "Location"

            .Col = ColCustomerPANNo
            .Text = "PAN No"

            .Col = ColCode
            .Text = "Item Code"

            .Col = ColItemName
            .Text = "Item Short Name"

            .Col = ColPartNo
            .Text = "Part No"

            .Col = ColCategory
            .Text = "Item Category"

            .Col = ColUnit
            .Text = "Unit"

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
            .Text = "TDS Amount"

            .Col = ColOthers
            .Text = "Other Amt"

            .Col = ColNetAmount
            .Text = "Net Amount"

        End With

        'With SprdMain
        '    .Row = 0

        '    .Col = 0
        '    .Text = "S.No."

        '    .Col = ColCode
        '    If cboWise.SelectedIndex = 0 Then
        '        .Text = "Customer"
        '    ElseIf cboWise.SelectedIndex = 1 Then
        '        .Text = "Item Code"
        '    ElseIf cboWise.SelectedIndex = 2 Then
        '        .Text = "Tariff Heading"
        '    ElseIf cboWise.SelectedIndex = 3 Then
        '        .Text = "Item Desc"
        '    ElseIf cboWise.SelectedIndex = 4 Then
        '        .Text = "Sales Tax"
        '    ElseIf cboWise.SelectedIndex = 5 Then
        '        .Text = "VNo"
        '    ElseIf cboWise.SelectedIndex = 6 Then
        '        .Text = "Division"
        '    ElseIf cboWise.SelectedIndex = 7 Then
        '        .Text = "Invoice Type"
        '    End If

        '    .Col = ColItemName
        '    .Text = "Item Short Name"

        '    .Col = ColUnit
        '    .Text = "Unit"

        '    .Col = ColGrouping
        '    For I = 1 To SprdOption.MaxCols
        '        SprdOption.Row = 1
        '        SprdOption.Col = I
        '        If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
        '            .Text = FillFieldName(I)
        '            Exit For
        '        End If
        '    Next

        '    .Col = ColSALEQTY
        '    .Text = "Qty"

        '    .Col = ColSALEVALUE
        '    .Text = "Value"

        '    .Col = ColCGST
        '    .Text = "CGST Amount"

        '    .Col = ColSGST
        '    .Text = "SGST Amount"

        '    .Col = ColIGST
        '    .Text = "IGST Amount"

        '    .Col = ColExciseDuty
        '    .Text = "Excise Duty Amt"

        '    .Col = ColCessTax
        '    .Text = "Cess Tax Amt"

        '    .Col = ColSHCessTax
        '    .Text = "S.H.E. Cess"

        '    .Col = ColVAT
        '    .Text = "VAT"

        '    .Col = ColCST
        '    .Text = "C.S.T. Amt"

        '    .Col = ColOthers
        '    .Text = "Others Amt"

        'End With

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

            .MaxCols = ColThickness1
            .Row = 0
            .Col = ColCustomer1
            .Text = "Customer"


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

            MainClass.SetSpreadColor(SprdOption, -1)


            .Col = ColThickness1
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer
        Dim mColWidth As Integer

        With SprdMain
            .set_RowHeight(0, 2.5 * RowHeight)
            .Row = Arow
            .set_ColWidth(0, 7)

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
                    Case ColBillNo, ColCustomerCode, ColBillDate, ColCode, ColCustomerLocation, ColCustomerPANNo
                        mColWidth = 10
                    Case ColItemName, ColCustomerName, ColCategory
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

            .Col = ColSALEQTY
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColSALEQTY, 10)

            .Col = ColSALERate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColSALERate, 10)

            .Col = ColSALEVALUE
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColSALEVALUE, 10)

            .Col = ColCGST
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColCGST, 10)

            .Col = ColSGST
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColSGST, 10)

            .Col = ColIGST
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColIGST, 10)

            .Col = ColTCS
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColTCS, 10)

            .Col = ColOthers
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColOthers, 10)

            .Col = ColNetAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColNetAmount, 10)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal

        End With

        'With SprdMain
        '    .set_RowHeight(0, 1.2 * RowHeight)
        '    .Row = Arow
        '    .set_ColWidth(0, 7)

        '    For I = ColGrouping To ColUnit
        '        .Col = I

        '        .CellType = SS_CELL_TYPE_EDIT
        '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
        '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '        .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
        '        .set_RowHeight(Arow, RowHeight)
        '        Select Case I
        '            Case ColGrouping
        '                mColWidth = 25
        '            Case ColCode
        '                mColWidth = 15
        '            Case ColItemName
        '                mColWidth = 25
        '            Case ColUnit
        '                mColWidth = 6
        '        End Select
        '        .set_ColWidth(I, mColWidth)
        '        .ColsFrozen = ColGrouping
        '        .ColHidden = False
        '    Next

        '    For I = ColSALEQTY To ColOthers
        '        .Col = I
        '        .CellType = SS_CELL_TYPE_FLOAT
        '        .TypeFloatDecimalChar = Asc(".")
        '        .TypeFloatDecimalPlaces = IIf(I = ColSALEQTY, 4, 2)
        '        .TypeFloatMax = CDbl("999999999.99")
        '        .TypeFloatMin = CDbl("-999999999.99")
        '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
        '        .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
        '        .set_RowHeight(Arow, RowHeight)
        '        .set_ColWidth(I, 10)
        '    Next

        '    MainClass.SetSpreadColor(SprdMain, -1)
        '    MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
        '    .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        '    SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal

        'End With
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
            mTitle = "Purchase MIS"
        Else
            mRPTName = "SalesMISSumm.RPT"
            mTitle = "Purchase MIS Summary"
        End If

        SprdMain.Row = 0
        SprdMain.Col = 1
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

    Private Function InsertIntoPrintTable() As Boolean

        On Error GoTo PrintDummyErr

        Dim SqlStr As String = ""
        Dim RowNum As Integer
        Dim mGrouping As String
        Dim mCode As String
        Dim mItemName As String
        Dim mUnit As String
        Dim mSaleQty As String
        Dim mSaleValue As String
        Dim mExciseDuty As String
        Dim mVAT As String
        Dim mCessTax As String
        Dim mSHCessTax As String
        Dim mCST As String
        Dim mOthers As String
        Dim mCGSTAmt As String
        Dim mSGSTAmt As String
        Dim mIGSTAmt As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)



        For RowNum = 1 To SprdMain.MaxRows - 2
            With SprdMain
                .Row = RowNum

                .Col = ColGrouping
                If GroupOnItem = True And optType(0).Checked = True Then 'And lblLabelType.text <> "Stock Age" Then
                    mGrouping = ""
                Else
                    mGrouping = MainClass.AllowSingleQuote(Trim(.Text))
                End If

                .Col = ColCode
                mCode = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColItemName
                mItemName = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColSALEQTY
                mSaleQty = .Text

                .Col = ColSALEVALUE
                mSaleValue = .Text


                mExciseDuty = ""


                mCessTax = ""


                mSHCessTax = ""


                mVAT = ""


                mCST = ""

                .Col = ColOthers
                mOthers = .Text

                .Col = ColCGST
                mCGSTAmt = .Text

                .Col = ColSGST
                mSGSTAmt = .Text

                .Col = ColIGST
                mIGSTAmt = .Text

            End With
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " Field1, Field2, Field3, Field4, " & vbCrLf & " Field5, Field6, Field7,FIELD8, FIELD9, FIELD10, FIELD11, FIELD12,FIELD13,FIELD14,FIELD15 )" & vbCrLf & " VALUES ( '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " '" & mGrouping & "', '" & mCode & "', '" & mItemName & "'," & vbCrLf & " '" & mUnit & "', '" & mSaleQty & "'," & vbCrLf & " '" & mSaleValue & "','" & mExciseDuty & "', " & vbCrLf & " '" & mVAT & "', '" & mCessTax & "','" & mSHCessTax & "','" & mCST & "', '" & mOthers & "','" & mCGSTAmt & "','" & mSGSTAmt & "','" & mIGSTAmt & "' ) "

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

    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String


        mSqlStr = mSqlStr & "SELECT * " & " FROM TEMP_PrintDummyData PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " ORDER BY SubRow,Field1,Field3"


        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

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

        ShowPurchaseMIS()

        SprdMain.Refresh()
        FormatSprdMain(-1)
        FillSprdMain()
        GroupByColor()

        Call PrintStatus(True)
        SprdMain.Focus()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim I As Integer

        FieldsVarification = True
        If Not IsDate(txtDateFrom.Text) Then
            FieldsVarification = False
            MsgInformation("Invaild Date")
            txtDateFrom.Focus()
            Exit Function
        ElseIf FYChk((txtDateFrom.Text)) = False Then
            FieldsVarification = False
            txtDateFrom.Focus()
            Exit Function
        End If

        If Not IsDate(txtDateTo.Text) Then
            FieldsVarification = False
            MsgInformation("Invaild Date")
            txtDateTo.Focus()
            Exit Function
        ElseIf FYChk((txtDateTo.Text)) = False Then
            FieldsVarification = False
            txtDateTo.Focus()
            Exit Function
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
    Private Function ShowPurchaseMIS() As Boolean

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mGroupBy As String
        Dim mSqlStr As String
        Dim mOptionalTable As String
        Dim mOptionalJoining As String
        Dim mCATEGORY_CODE As String
        Dim CntLst As Integer
        Dim mAcctCode As String
        Dim mAcctCodeStr As String
        Dim mInvoiceType As String
        Dim mDivisionCode As Double

        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String = ""

        SqlStr = " SELECT CC.COMPANY_NAME,"

        ''Collect the Group Field...

        For I = 1 To SprdOption.MaxCols
            SprdOption.Row = 1
            SprdOption.Col = I
            If SprdOption.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                mGroupBy = GetGroupBy(I)
                If mGroupBy <> "" Then
                    SqlStr = SqlStr & vbCrLf & " " & mGroupBy & ","
                    Exit For
                End If
            End If
        Next

        If lstFieldName.GetItemChecked(1) = True Then
            SqlStr = SqlStr & vbCrLf & " IH.VNO,"
        Else
            SqlStr = SqlStr & vbCrLf & " '',"
        End If

        If lstFieldName.GetItemChecked(2) = True Then
            SqlStr = SqlStr & vbCrLf & " IH.VDATE,"
        Else
            SqlStr = SqlStr & vbCrLf & " '',"
        End If

        If lstFieldName.GetItemChecked(3) = True Then
            SqlStr = SqlStr & vbCrLf & " CMST.SUPP_CUST_CODE,"
        Else
            SqlStr = SqlStr & vbCrLf & " '',"
        End If

        If lstFieldName.GetItemChecked(4) = True Then
            SqlStr = SqlStr & vbCrLf & " CMST.SUPP_CUST_NAME,"
        Else
            SqlStr = SqlStr & vbCrLf & " '',"
        End If

        If lstFieldName.GetItemChecked(5) = True Then
            SqlStr = SqlStr & vbCrLf & " CMST.SUPP_CUST_CITY,"
        Else
            SqlStr = SqlStr & vbCrLf & " '',"
        End If

        If lstFieldName.GetItemChecked(6) = True Then
            SqlStr = SqlStr & vbCrLf & " CUSMST.PAN_NO,"
        Else
            SqlStr = SqlStr & vbCrLf & " '',"
        End If

        If lstFieldName.GetItemChecked(7) = True Then
            SqlStr = SqlStr & vbCrLf & " ID.ITEM_CODE,"
        Else
            SqlStr = SqlStr & vbCrLf & " '',"
        End If

        If lstFieldName.GetItemChecked(8) = True Then
            SqlStr = SqlStr & vbCrLf & " ITEM.ITEM_SHORT_DESC,"
        Else
            SqlStr = SqlStr & vbCrLf & " '',"
        End If

        If lstFieldName.GetItemChecked(9) = True Then
            SqlStr = SqlStr & vbCrLf & " ID.CUSTOMER_PART_NO,"
        Else
            SqlStr = SqlStr & vbCrLf & " '',"
        End If

        If lstFieldName.GetItemChecked(10) = True Then
            SqlStr = SqlStr & vbCrLf & " ITEMCAT.GEN_DESC,"
        Else
            SqlStr = SqlStr & vbCrLf & " '',"
        End If

        If lstFieldName.GetItemChecked(11) = True Then
            SqlStr = SqlStr & vbCrLf & " ID.ITEM_UOM,"
        Else
            SqlStr = SqlStr & vbCrLf & " '',"
        End If

        If lstFieldName.GetItemChecked(12) = True Then
            SqlStr = SqlStr & vbCrLf & " ID.ITEM_RATE,"
        Else
            SqlStr = SqlStr & vbCrLf & " 0,"

        End If

        SqlStr = SqlStr & vbCrLf & "TO_CHAR(SUM(ID.ITEM_QTY)) AS SALEQTY, "

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(ID.ITEM_AMT)) AS SALEVALUE,"

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN CMST.GST_RGN_NO='" & RsCompany.Fields("COMPANY_GST_RGN_NO").Value & "' THEN 0 ELSE ID.CGST_AMOUNT END)) AS CGST_AMOUNT,"
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN CMST.GST_RGN_NO='" & RsCompany.Fields("COMPANY_GST_RGN_NO").Value & "' THEN 0 ELSE ID.SGST_AMOUNT END)) AS SGST_AMOUNT,"
        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN CMST.GST_RGN_NO='" & RsCompany.Fields("COMPANY_GST_RGN_NO").Value & "' THEN 0 ELSE ID.IGST_AMOUNT END)) AS IGST_AMOUNT,"

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(DECODE(IH.ITEMVALUE,0,0,ID.ITEM_AMT*IH.TDSAMOUNT/IH.ITEMVALUE))) AS TCS_AMOUNT,"


        SqlStr = SqlStr & vbCrLf & " TO_CHAR(0) AS OTHER_AMOUNT,"

        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN CMST.GST_RGN_NO='" & RsCompany.Fields("COMPANY_GST_RGN_NO").Value & "' THEN ID.ITEM_AMT ELSE ID.ITEM_AMT+ID.CGST_AMOUNT+ID.SGST_AMOUNT+ ID.IGST_AMOUNT END)) AS NET_AMOUNT"


        Call GetOptionTable(mOptionalTable, mOptionalJoining)

        SqlStr = SqlStr & vbCrLf & " FROM FIN_PURCHASE_HDR IH,FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST CUSMST, FIN_SUPP_CUST_BUSINESS_MST CMST,INV_DIVISION_MST IDIV, FIN_SUPP_CUST_MST IMST"

        '    If cboItemType.ListIndex > 0 Then
        SqlStr = SqlStr & " ,INV_ITEM_MST ITEM, INV_GENERAL_MST ITEMCAT, GEN_COMPANY_MST CC"
        '    End If


        SqlStr = SqlStr & IIf(mOptionalTable = "", "", vbCrLf & mOptionalTable)

        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.COMPANY_CODE=CC.COMPANY_CODE" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY AND IH.CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.COMPANY_CODE=CUSMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CUSMST.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID"

        SqlStr = SqlStr & vbCrLf _
            & " AND ITEM.COMPANY_CODE=ITEMCAT.COMPANY_CODE AND ITEM.CATEGORY_CODE=ITEMCAT.GEN_CODE AND ITEMCAT.GEN_TYPE='C' "

        SqlStr = SqlStr & vbCrLf & " And IH.COMPANY_CODE = IDIV.COMPANY_CODE " & vbCrLf & " And IH.DIV_CODE=IDIV.DIV_CODE"

        SqlStr = SqlStr & vbCrLf & " And ID.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf & " And ID.PUR_ACCOUNT_CODE=IMST.SUPP_CUST_CODE"

        '    If cboItemType.ListIndex > 0 Then
        SqlStr = SqlStr & vbCrLf & " And ID.COMPANY_CODE=ITEM.COMPANY_CODE " & vbCrLf & " And ID.ITEM_CODE=ITEM.ITEM_CODE"
        '    End If

        SqlStr = SqlStr & IIf(GetAttributeCode() = "", "", vbCrLf & GetAttributeCode())

        SqlStr = SqlStr & IIf(mOptionalJoining = "", "", vbCrLf & mOptionalJoining)



        'If CboItemType.SelectedIndex > 0 Then
        '    If MainClass.ValidateWithMasterTable((CboItemType.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And GEN_TYPE='C'") = True Then
        '        mCATEGORY_CODE = IIf(IsDBNull(MasterNo), "", MasterNo)
        '    End If

        '    If Trim(mCATEGORY_CODE) <> "" Then
        '        SqlStr = SqlStr & vbCrLf & "AND ITEM.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCATEGORY_CODE) & "'"
        '    End If
        'End If

        Dim mCategoryCode As String

        If CboItemType.Text.Trim <> "" Then
            For Each r As UltraGridRow In CboItemType.CheckedRows
                If mCategoryCode <> "" Then
                    mCategoryCode += "," & "'" & r.Cells("GEN_CODE").Value.ToString() & "'"
                Else
                    mCategoryCode += "'" & r.Cells("GEN_CODE").Value.ToString() & "'"
                End If
            Next
        End If

        If mCategoryCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE IN (" & mCategoryCode & ")"
        End If


        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                '            lstInvoiceType.ListIndex = CntLst
                If MainClass.ValidateWithMasterTable(mInvoiceType, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAcctCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mAcctCodeStr = IIf(mAcctCodeStr = "", mAcctCode, mAcctCodeStr & ",'" & mAcctCode & "'")
            End If
        Next

        If mAcctCodeStr <> "" Then
            mAcctCodeStr = "(" & mAcctCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " And ID.PUR_ACCOUNT_CODE In " & mAcctCodeStr & ""
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
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & ", " & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " And CC.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        'If cboModvat.SelectedIndex = 1 Then
        '    SqlStr = SqlStr & vbCrLf & " And IH.MODVATAMOUNT+IH.ADEMODVATAMOUNT>0"
        'ElseIf cboModvat.SelectedIndex = 2 Then
        '    SqlStr = SqlStr & vbCrLf & " And IH.MODVATAMOUNT+IH.ADEMODVATAMOUNT=0 And ID.ITEM_ED<>0"
        'End If

        SqlStr = SqlStr & vbCrLf & " And IH.ISFINALPOST='Y' AND IH.VNO<>'-1'"

                If IsDate(txtDateFrom.Text) And IsDate(txtDateTo.Text) Then
            '        SqlStr = SqlStr & vbCrLf & " AND IH.MRRDATE BETWEEN TO_DATE('" & vb6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "') AND TO_DATE('" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"
            If optDate(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND IH.VDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND IH.MRRDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        End If

        ''set Particular Condition...
        '    GetSubTitle = ""
        '    For I = 1 To SprdOption.MaxCols
        '        SprdOption.Row = 2
        '        SprdOption.Col = I
        '        If SprdOption.Text = vbUnchecked Then
        '            SprdOption.Row = 3
        '            SqlStr = GetConditionalQry(SqlStr, I, SprdOption.Text, GetSubTitle)
        '        End If
        '    Next


        SqlStr = SqlStr & vbCrLf & "GROUP BY "

        If mGroupBy <> "" Then
            SqlStr = SqlStr & vbCrLf & "" & mGroupBy & ","
        End If

        If lstFieldName.GetItemChecked(1) = True Then
            SqlStr = SqlStr & vbCrLf & " IH.VNO,"
        End If

        If lstFieldName.GetItemChecked(2) = True Then
            SqlStr = SqlStr & vbCrLf & " IH.VDATE,"
        End If

        If lstFieldName.GetItemChecked(3) = True Then
            SqlStr = SqlStr & vbCrLf & " CMST.SUPP_CUST_CODE,"
        End If

        If lstFieldName.GetItemChecked(4) = True Then
            SqlStr = SqlStr & vbCrLf & " CMST.SUPP_CUST_NAME,"
        End If

        If lstFieldName.GetItemChecked(5) = True Then
            SqlStr = SqlStr & vbCrLf & " CMST.SUPP_CUST_CITY,"
        End If

        If lstFieldName.GetItemChecked(6) = True Then
            SqlStr = SqlStr & vbCrLf & " CUSMST.PAN_NO,"
        End If

        If lstFieldName.GetItemChecked(7) = True Then
            SqlStr = SqlStr & vbCrLf & " ID.ITEM_CODE,"
        End If

        If lstFieldName.GetItemChecked(8) = True Then
            SqlStr = SqlStr & vbCrLf & " ITEM.ITEM_SHORT_DESC,"
        End If

        If lstFieldName.GetItemChecked(9) = True Then
            SqlStr = SqlStr & vbCrLf & " ID.CUSTOMER_PART_NO,"
        End If

        If lstFieldName.GetItemChecked(10) = True Then
            SqlStr = SqlStr & vbCrLf & " ITEMCAT.GEN_DESC,"
        End If

        If lstFieldName.GetItemChecked(11) = True Then
            SqlStr = SqlStr & vbCrLf & " ID.ITEM_UOM,"
        End If

        If lstFieldName.GetItemChecked(12) = True Then
            SqlStr = SqlStr & vbCrLf & " ID.ITEM_RATE,"
        End If


        SqlStr = SqlStr & vbCrLf & "CC.COMPANY_NAME"

        SqlStr = SqlStr & vbCrLf & "ORDER BY CC.COMPANY_NAME,"

        If mGroupBy <> "" Then
            SqlStr = SqlStr & vbCrLf & "" & mGroupBy & ""
        End If

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        SprdColTotal(ColSALEQTY, ColNetAmount)

        ShowPurchaseMIS = True
        Exit Function
InsertErr:
        ShowPurchaseMIS = False
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function GetAttributeCode() As String

        On Error GoTo ERR1
        Dim pSqlStr As String
        Dim mCategoryCode As String

        With SprdOption
            .Col = ColItemCode1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = "ITEM.ITEM_CODE='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColCustomer1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColDivision1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "IDIV.DIV_DESC='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColCancelled1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "IH.CANCELLED='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColInvType1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "IMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColThickness1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "ITEM.MAT_THICHNESS='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColBillNo1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "IH.VNO='" & MainClass.AllowSingleQuote(.Text) & "'"
            End If

            .Col = ColCategory1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                If MainClass.ValidateWithMasterTable(.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "ITEM.CATEGORY_CODE='" & MasterNo & "'"
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
                    pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "ITEM.SUBCATEGORY_CODE='" & MasterNo & "'"
                End If
            End If

            '        .Col = ColModel1
            '        .Row = 2
            '        If .Value = vbUnchecked Then
            '            .Row = 3
            '            pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "ITEM.ITEM_MODEL='" & MainClass.AllowSingleQuote(.Text) & "'"
            '        End If

            '        .Col = ColMake1
            '        .Row = 2
            '        If .Value = vbUnchecked Then
            '            .Row = 3
            '            pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "ITEM.ITEM_MAKE='" & MainClass.AllowSingleQuote(.Text) & "'"
            '        End If

            '        .Col = ColColor1
            '        .Row = 2
            '        If .Value = vbUnchecked Then
            '            .Row = 3
            '            pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "ITEM.ITEM_COLOR='" & MainClass.AllowSingleQuote(.Text) & "'"
            '        End If

            '        .Col = ColDept1
            '        .Row = 2
            '        If .Value = vbUnchecked Then
            '            .Row = 3
            '            pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "( STOCK.DEPT_CODE_TO='" & MainClass.AllowSingleQuote(.Text) & "' OR STOCK.DEPT_CODE_FROM='" & MainClass.AllowSingleQuote(.Text) & "')"
            '            'SqlStr = SqlStr & vbCrLf & " AND ( STOCK.DEPT_CODE_TO='" & MasterNo & "' OR STOCK.DEPT_CODE_FROM='" & MasterNo & "')"
            '        End If

            .Col = ColTariffHeading1
            .Row = 2
            If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                .Row = 3
                If MainClass.ValidateWithMasterTable(.Text, "TARRIF_DESC", "TARRIF_CODE", "FIN_TARRIF_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    pSqlStr = pSqlStr & IIf(pSqlStr = "", "", vbCrLf & " AND ") & "ITEM.TARIFF_CODE='" & MasterNo & "'"
                End If
            End If

        End With
        GetAttributeCode = IIf(pSqlStr = "", "", " AND ") & pSqlStr
        Exit Function
ERR1:
        '    Resume
        MsgBox(Err.Description)
        GetAttributeCode = ""
    End Function



    Private Function GetOptionTable(ByRef pOptionalTable As String, ByRef pOptionJoining As String) As Object
        On Error GoTo ERR1
        Dim pSqlStr As String

        With SprdOption

            '.Col = ColCategory1
            '.Row = 1
            'If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
            '    pOptionalTable = ", INV_GENERAL_MST ITEMCAT"
            '    pOptionJoining = " AND ITEM.COMPANY_CODE=ITEMCAT.COMPANY_CODE AND ITEM.CATEGORY_CODE=ITEMCAT.GEN_CODE AND ITEMCAT.GEN_TYPE='C'"
            'End If

            .Col = ColSubCategory1
            .Row = 1
            If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                pOptionalTable = ", INV_SUBCATEGORY_MST ITEMSUBCAT"
                pOptionJoining = " AND ITEM.COMPANY_CODE=ITEMSUBCAT.COMPANY_CODE AND ITEM.SUBCATEGORY_CODE=ITEMSUBCAT.SUBCATEGORY_CODE AND ITEM.CATEGORY_CODE=ITEMSUBCAT.CATEGORY_CODE"
            End If

            '        .Col = ColCustomer1
            '        .Row = 1
            '        If .Value = vbChecked Or cboWise.ListIndex = 0 Then
            '            pOptionalTable = pOptionalTable & ", FIN_SUPP_CUST_MST"
            '            pOptionJoining = pOptionJoining & " AND IH.COMPANY_CODE= FIN_SUPP_CUST_MST.COMPANY_CODE AND IH.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE"
            '        End If

        End With

        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function
    Private Sub frmParamPurchaseMIS_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormLoaded = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = " MIS Purchase Reports"


        FormLoaded = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
        'Resume
    End Sub

    Private Sub frmParamPurchaseMIS_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo err_Renamed
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)
        MainClass.SetControlsColor(Me)

        optType(0).Checked = True

        txtDateFrom.Text = RsCompany.Fields("Start_Date").Value
        txtDateTo.Text = CStr(RunDate)
        Call PrintStatus(True)

        Call FillCboWise()
        Call FillCboItemType()
        Call FillInvoiceType()

        'SprdMain.DataSource = AData1
        FormatSprdOption(-1)
        FormatSprdMain(-1)
        FillSprdMain()
        FormLoaded = False
        Call frmParamPurchaseMIS_Activated(eventSender, eventArgs)
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
        cboWise.Items.Add("Tariff Heading")
        cboWise.Items.Add("Item Desc")
        cboWise.Items.Add("Sales Tax")
        cboWise.Items.Add("VNo")
        cboWise.Items.Add("Division")
        cboWise.Items.Add("Account Head")
        cboWise.SelectedIndex = 0

        'cboModvat.Items.Clear()
        'cboModvat.Items.Add("All")
        'cboModvat.Items.Add("Only Modvat")
        'cboModvat.Items.Add("W/O Modvat")
        'cboModvat.SelectedIndex = 0

        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub FillCboItemType()

        On Error GoTo FillErr2
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim SqlStr As String

        oledbCnn = New OleDbConnection(StrConn)


        SqlStr = " Select GEN_DESC, GEN_CODE FROM INV_GENERAL_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C' ORDER BY GEN_DESC"

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        CboItemType.DataSource = ds
        CboItemType.DataMember = ""
        Dim c As UltraGridColumn = Me.CboItemType.DisplayLayout.Bands(0).Columns.Add()
        With c
            .Key = "Selected"
            .Header.Caption = String.Empty
            .Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always
            .DataType = GetType(Boolean)
            .DataType = GetType(Boolean)
            .Header.VisiblePosition = 0
        End With
        CboItemType.CheckedListSettings.CheckStateMember = "Selected"
        CboItemType.CheckedListSettings.EditorValueSource = Infragistics.Win.EditorWithComboValueSource.CheckedItems
        ' Set up the control to use a custom list delimiter 
        CboItemType.CheckedListSettings.ListSeparator = " , "
        ' Set ItemCheckArea to Item, so that clicking directly on an item also checks the item
        CboItemType.CheckedListSettings.ItemCheckArea = Infragistics.Win.ItemCheckArea.Item
        CboItemType.DisplayMember = "GEN_DESC"
        CboItemType.ValueMember = "GEN_CODE"

        CboItemType.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Category"
        CboItemType.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Code"

        CboItemType.DisplayLayout.Bands(0).Columns(0).Width = 250
        CboItemType.DisplayLayout.Bands(0).Columns(1).Width = 50


        CboItemType.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown



        oledbAdapter.Dispose()
        oledbCnn.Close()
        'CboItemType.Items.Clear()
        'CboItemType.Items.Add("All")

        'MainClass.FillCombo(CboItemType, "INV_GENERAL_MST", "GEN_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'")

        'CboItemType.SelectedIndex = 0
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
        SqlStr = "SELECT DISTINCT SUPP_CUST_NAME FROM FIN_INVTYPE_MST A, FIN_SUPP_CUST_MST B " & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
            & " AND A.ACCOUNTPOSTCODE=B.SUPP_CUST_CODE" & vbCrLf _
            & " AND A.CATEGORY='P' ORDER BY SUPP_CUST_NAME" ''
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstInvoiceType.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstInvoiceType.Items.Add(RS.Fields("SUPP_CUST_NAME").Value)
                lstInvoiceType.SetItemChecked(CntLst, True)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstInvoiceType.SelectedIndex = 0

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

        lstFieldName.Items.Add("Customer PAN No")
        lstFieldName.SetItemChecked(6, True)

        lstFieldName.Items.Add("Item Code")
        lstFieldName.SetItemChecked(7, True)

        lstFieldName.Items.Add("Item Name")
        lstFieldName.SetItemChecked(8, True)

        lstFieldName.Items.Add("Part No")
        lstFieldName.SetItemChecked(9, True)

        lstFieldName.Items.Add("Item Category")
        lstFieldName.SetItemChecked(10, True)

        lstFieldName.Items.Add("Item Unit")
        lstFieldName.SetItemChecked(11, True)

        lstFieldName.Items.Add("Item Rate")
        lstFieldName.SetItemChecked(12, True)

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
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

    Private Sub frmParamPurchaseMIS_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
                Case ColDivision1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "INV_DIVISION_MST", "DIV_DESC", "DIV_CODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                Case ColInvType1
                    ''Call SearchColMaster(eventArgs.row, eventArgs.col, "FIN_INVTYPE_MST", "NAME", "CODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='O'")
                Case ColItemCode1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
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
                Case ColTariffHeading1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "FIN_TARRIF_MST", "TARRIF_CODE", "TARRIF_DESC", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                Case ColItemDesc1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "FIN_ITEMTYPE_MST", "NAME", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                Case ColSalesTax1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "FIN_INTERFACE_MST", "FORMTYPE", "NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IDENTIFICATION='ST'")
                Case ColBillNo1
                    Call SearchColMaster(eventArgs.row, eventArgs.col, "FIN_PURCHASE_HDR", "VNO", "VDATE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            End Select
        End If
        PrintStatus(False)
    End Sub
    Private Sub PrintStatus(ByRef PrintFlag As Boolean)
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
    End Sub
    Private Sub SearchColMaster(ByRef mRow As Integer, ByRef mCol As Integer, ByRef mTable As String, ByRef mField As String, ByRef mField1 As String, Optional ByRef mConditional As String = "")

        With SprdOption
            SprdOption.Row = 3
            SprdOption.Col = mCol

            If MainClass.SearchGridMaster((SprdOption.Text), mTable, mField, mField1, , , mConditional) = True Then
                '        If MainClass.SearchMaster(SprdOption.Text, mTable, mField, mConditional) = True Then
                .Row = 3
                .Col = mCol
                .Text = IIf(mCol = 1, AcName1, AcName)
            End If
            MainClass.SetFocusToCell(SprdOption, SprdOption.ActiveRow, IIf(SprdOption.MaxCols > mCol, mCol + 1, 1))
        End With
    End Sub
    Private Function GetConditionalQry(ByRef mSqlStr As String, ByRef ColCheck As Integer, ByRef DataFieldName As String, ByRef GetSubTitle As String) As String

        On Error GoTo ERR1
        Dim FieldName As String
        GetConditionalQry = mSqlStr
        FieldName = GetGroupBy(ColCheck)
        '            FieldName = Mid(FieldName, 1, InStr(1, FieldName, ".") - 1)

        If Mid(FieldName, 1, InStr(1, FieldName, ".") - 1) = "FIN_SUPP_CUST_MST" Then
            MainClass.ValidateWithMasterTable(DataFieldName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")

            GetConditionalQry = GetConditionalQry & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MasterNo & "'"
        Else
            GetConditionalQry = GetConditionalQry & vbCrLf & "AND " & FieldName & "='" & MainClass.AllowSingleQuote(Trim(DataFieldName)) & "' "
        End If
        SprdOption.Row = 0
        GetSubTitle = GetSubTitle & IIf(GetSubTitle <> "", " AND ", "") & SprdOption.Text & " : " & DataFieldName
        'GetConditionalQry = GetConditionalQry & vbCrLf & " AND " & FieldName & "='" & mainclass.AllowSingleQuote(Trim(DataFieldName)) & "' "
        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function
    Private Function GetGroupBy(ByRef ColGroup As Integer) As String
        On Error GoTo ERR1
        Dim mFieldName As String
        Select Case ColGroup
            Case ColCustomer1
                mFieldName = "CMST.SUPP_CUST_NAME"
            Case ColDivision1
                mFieldName = "IDIV.DIV_DESC"
            Case ColInvType1
                mFieldName = "IMST.SUPP_CUST_NAME"
            Case ColCategory1
                mFieldName = "ITEMCAT.GEN_DESC"
            Case ColSubCategory1
                mFieldName = "ITEMSUBCAT.SUBCATEGORY_DESC"
            Case ColItemCode1
                If optType(0).Checked = True Then
                    mFieldName = "ID.ITEM_CODE"
                Else
                    mFieldName = "ID.ITEM_CODE || '- ' || ID.ITEM_DESC"
                End If
            Case ColTariffHeading1
                mFieldName = "IH.TARIFFHEADING"
            Case ColItemDesc1
                mFieldName = "IH.ITEMDESC"
            Case ColSalesTax1
                mFieldName = "IH.STFORMNAME"
            Case ColBillNo1
                mFieldName = "IH.VNO"
            Case ColAgtD31
                mFieldName = "IH.REJECTION"
            Case ColCancelled1
                mFieldName = "IH.CANCELLED"
            Case ColThickness1
                mFieldName = "ITEM.MAT_THICHNESS"


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
            Case ColDivision1
                mFieldName = "Division"
            Case ColInvType1
                mFieldName = "Account Head"
            Case ColCategory1
                mFieldName = "Category"
            Case ColSubCategory1
                mFieldName = "Sub Category"
            Case ColItemCode1
                mFieldName = "Item Code"
            Case ColTariffHeading1
                mFieldName = "Tariff Heading"
            Case ColItemDesc1
                mFieldName = "Item Desc"
            Case ColSalesTax1
                mFieldName = "Sales Tax"
            Case ColBillNo1
                mFieldName = "VNo"
            Case ColAgtD31
                mFieldName = "Agt D3"
            Case ColCancelled1
                mFieldName = "Cancelled"
            Case ColThickness1
                mFieldName = "Thickness"
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
        If txtDateFrom.Text = "" Then GoTo EventExitSub
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtDateTo.Text = "" Then GoTo EventExitSub
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
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub GroupByColor()
        Dim mGroup As String
        Dim cntRow As Integer
        Dim mBlackColor As Integer
        Dim mOpening As Double
        Dim mReceipt As Double
        Dim mIssue As Double
        Dim mTotClosing As Double
        Dim mClosing As Double

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColGrouping
                If mGroup <> Trim(.Text) Then
                    If mBlackColor = &HFFFF00 Then
                        mBlackColor = &H80FF80
                    Else
                        mBlackColor = &HFFFF00
                    End If
                    mGroup = Trim(.Text)
                    mTotClosing = 0
                End If

                .Row = cntRow
                .Row2 = cntRow
                .Col = 1
                .Col2 = .MaxCols
                .BlockMode = True
                .BackColor = System.Drawing.ColorTranslator.FromOle(mBlackColor) ''&HFFFF00
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

        SprdMain.Col = ColCustomerPANNo
        If lstFieldName.GetItemChecked(6) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

        SprdMain.Col = ColCode
        If lstFieldName.GetItemChecked(7) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

        SprdMain.Col = ColItemName
        If lstFieldName.GetItemChecked(8) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

        SprdMain.Col = ColPartNo
        If lstFieldName.GetItemChecked(9) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

        SprdMain.Col = ColCategory
        If lstFieldName.GetItemChecked(10) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

        SprdMain.Col = ColUnit
        If lstFieldName.GetItemChecked(11) = True Then
            SprdMain.ColHidden = False
        Else
            SprdMain.ColHidden = True
        End If

        SprdMain.Col = ColSALERate
        If lstFieldName.GetItemChecked(12) = True Then
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
            .MaxRows = .MaxRows + 2
            SprdMain.Col = ColGrouping
            SprdMain.Row = SprdMain.MaxRows
            SprdMain.Text = "TOTAL :"
            SprdMain.Font = VB6.FontChangeBold(SprdMain.Font, True)

            For cntCol = Col To col2
                .Col = cntCol
                For cntRow = 1 To .MaxRows - 2
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
End Class
