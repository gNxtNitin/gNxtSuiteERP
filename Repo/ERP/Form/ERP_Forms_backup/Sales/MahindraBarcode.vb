Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json.Serialization
Imports QRCoder
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Color
Imports System.Drawing
Imports System.Drawing.Printing

Friend Class frmMahindraBarcode
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColMKey As Short = 1
    Private Const ColPONO As Short = 2
    Private Const ColItemSNO As Short = 3
    Private Const ColPartNo As Short = 4
    Private Const ColQty As Short = 5
    Private Const ColInvNo As Short = 6
    Private Const ColInvDate As Short = 7
    Private Const ColInvoiceAmtWithTCS As Short = 8
    Private Const ColExciseAmount As Short = 9
    Private Const ColLRNo As Short = 10
    Private Const ColLRDate As Short = 11
    Private Const ColVehicleNo As Short = 12
    Private Const ColItemAmount As Short = 13
    Private Const ColIRNNo As Short = 14
    Private Const ColVendorGST As Short = 15
    Private Const ColCustomerGST As Short = 16
    Private Const ColPMDesc1 As Short = 17
    Private Const ColPMQnty1 As Short = 18
    Private Const ColPMDesc2 As Short = 19
    Private Const ColPMQnty2 As Short = 20
    Private Const ColPMDesc3 As Short = 21
    Private Const ColPMQnty3 As Short = 22


    Private Const ColFlag As Short = 19

    Dim mActiveRow As Integer
    Dim FormActive As Boolean


    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        cmdShow.Enabled = True
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        cmdShow.Enabled = True
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()

    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        'OptSelection(1).Checked = True
        'If chkCreditNote.Checked = True Then
        '    ShowCreditNote()
        'Else
        Show1()
        'End If
        FormatSprdMain()
        cmdShow.Enabled = True
    End Sub
    Private Sub ShowCreditNote()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mDivision As Double


        'If lblBookType.Text= "II" Then  ''I Invoice , I - IRN

        SqlStr = "SELECT IH.MKEY, PURCHASESEQTYPE AS INVOICESEQTYPE, IH.REJ_CREDITNOTE AS BILLNO, IH.VDATE AS INVOICE_DATE,  " & vbCrLf _
                & " IH.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME, IH.BILL_TO_LOC_ID, 0 AS TRANS_DISTANCE, '' AS VEHICLENO, IH.NETVALUE, " & vbCrLf _
                & " IH.IRN_NO, IH.IRN_ACK_DATE, IH.IRN_ACK_NO, IH.IRN_ACK_DATE, '' AS E_BILLWAYNO," & vbCrLf _
                & " '' AS E_BILLWAYDATE, '' AS E_BILLWAYVAILDUPTO, '' AS E_BILLWAYFILEPATH," & vbCrLf _
                & " '' " & vbCrLf _
                & " FROM FIN_PURCHASE_HDR IH, FIN_SUPP_CUST_MST ACM" & vbCrLf _
                & " WHERE IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & vbCrLf & "AND IH.PURCHASESEQTYPE =2 AND CANCELLED='N'"

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If


        If cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.IRN_NO IS NULL OR IH.IRN_NO='')"
        ElseIf cboShow.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.IRN_NO IS NOT NULL OR IH.IRN_NO<>'')"
        End If



        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If txtBillFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.REJ_CREDITNOTE >= '" & MainClass.AllowSingleQuote(txtBillFrom.Text) & "'" & vbCrLf _
                & " AND IH.REJ_CREDITNOTE <= '" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf _
                & " SELECT IH.MKEY, 9 AS INVOICESEQTYPE, IH.VNO AS BILLNO, IH.VDATE AS INVOICE_DATE,  " & vbCrLf _
                & " IH.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME, IH.BILL_TO_LOC_ID, 0 AS TRANS_DISTANCE, '' AS VEHICLENO, IH.NETVALUE, " & vbCrLf _
                & " IH.IRN_NO, IH.IRN_ACK_DATE, IH.IRN_ACK_NO, IH.IRN_ACK_DATE, '' AS E_BILLWAYNO," & vbCrLf _
                & " '' AS E_BILLWAYDATE, '' AS E_BILLWAYVAILDUPTO, '' AS E_BILLWAYFILEPATH," & vbCrLf _
                & " '' " & vbCrLf _
                & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_CUST_MST ACM" & vbCrLf _
                & " WHERE IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & vbCrLf & "AND IH.GST_APP ='Y' AND ISFINALPOST='Y' AND CANCELLED='N'"

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.IRN_NO IS NULL OR IH.IRN_NO='')"
        ElseIf cboShow.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.IRN_NO IS NOT NULL OR IH.IRN_NO<>'')"
        End If


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If txtBillFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.VNO >= '" & MainClass.AllowSingleQuote(txtBillFrom.Text) & "'" & vbCrLf _
                & " AND IH.VNO <= '" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY 3"


        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub

    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.Focus() : Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmMahindraBarcode_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmMahindraBarcode_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo BSLError
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0

        cboDivision.Items.Clear()
        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        cboDivision.Items.Add("ALL")
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If
        cboDivision.SelectedIndex = 0

        cboShow.Items.Clear()
        cboShow.Items.Add("ALL")
        cboShow.Items.Add("PENDING")
        cboShow.Items.Add("COMPLETE")

        cboShow.SelectedIndex = 0

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False

        chkAllVehicle.CheckState = System.Windows.Forms.CheckState.Checked
        txtVehicle.Enabled = False
        cmdSearchVehicle.Enabled = False

        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtBillFrom.Text = ""
        txtBillTo.Text = ""

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
        Dim mDivision As Double


        'If lblBookType.Text= "II" Then  ''I Invoice , I - IRN

        SqlStr = "SELECT IH.MKEY, IH.CUST_PO_NO, ID.ITEM_SNO, ID.CUSTOMER_PART_NO, ID.ITEM_QTY, " & vbCrLf _
                & " IH.BILLNO, TO_CHAR(IH.INVOICE_DATE,'DD.MM.YYYY'), IH.NETVALUE, (IH.NETCGST_AMOUNT + IH.NETSGST_AMOUNT + IH.NETIGST_AMOUNT) AS GST_AMOUNT," & vbCrLf _
                & " '' , TO_CHAR(IH.INVOICE_DATE,'DD.MM.YYYY'), IH.VEHICLENO, ID.ITEM_RATE, " & vbCrLf _
                & " IH.IRN_NO, CMST.COMPANY_GST_RGN_NO," & vbCrLf _
                & " ACM.GST_RGN_NO, '',0,'',0,'',0 "


        SqlStr = SqlStr & vbCrLf _
                & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_BUSINESS_MST ACM, GEN_COMPANY_MST CMST" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY AND IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND IH.BILL_TO_LOC_ID=ACM.LOCATION_ID " & vbCrLf _
                & " AND IH.Company_Code=CMST.Company_Code " & vbCrLf _
                & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If chkServiceInvoiceOnly.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND IH.INVOICESEQTYPE IN (1,2,3,6,9)"
        Else
            SqlStr = SqlStr & vbCrLf & "AND IH.INVOICESEQTYPE IN (4)"
        End If



        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If cboShow.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.IRN_NO IS NULL OR IH.IRN_NO='')"
        ElseIf cboShow.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & "AND (IH.IRN_NO IS NOT NULL OR IH.IRN_NO<>'')"
        End If


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkAllVehicle.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND IH.VEHICLENO='" & MainClass.AllowSingleQuote(txtVehicle.Text) & "'"
        End If

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If txtBillFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.BILLNO >= '" & MainClass.AllowSingleQuote(txtBillFrom.Text) & "'" & vbCrLf _
                & " AND IH.BILLNO <= '" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY TO_CHAR(IH.INVOICE_DATE,'DD.MM.YYYY'),IH.BILLNO"


        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain()
        With SprdMain

            .MaxCols = ColPMQnty3
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)
            .set_RowHeight(-1, RowHeight)

            .Row = -1

            .Col = ColMKey
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMKey, 12)
            .ColHidden = True

            .Col = ColPONO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColPONO, 10)
            .ColHidden = True

            .Col = ColItemSNO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColItemSNO, 8)

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColPartNo, 12)


            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQty, 7)

            .Col = ColInvNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColInvNo, 12)

            .Col = ColInvDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColInvDate, 12)

            .Col = ColInvoiceAmtWithTCS
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColInvoiceAmtWithTCS, 8)

            .Col = ColExciseAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColExciseAmount, 8)

            .Col = ColLRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColLRNo, 10)

            .Col = ColLRDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColLRDate, 10)



            .Col = ColVehicleNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColVehicleNo, 12)

            .Col = ColItemAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColItemAmount, 12)

            .Col = ColIRNNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditMultiLine = True
            .set_ColWidth(ColIRNNo, 12)

            .Col = ColVendorGST
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColVendorGST, 12)

            .Col = ColCustomerGST
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColCustomerGST, 12)

            .Col = ColPMDesc1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColPMDesc1, 12)

            .Col = ColPMQnty1
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPMQnty1, 12)

            .Col = ColPMDesc2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColPMDesc2, 12)

            .Col = ColPMQnty2
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPMQnty2, 12)

            .Col = ColPMDesc3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColPMDesc3, 12)

            .Col = ColPMQnty3
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPMQnty3, 12)

            MainClass.SetSpreadColor(SprdMain, -1)

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColPMQnty3)
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

            .Col = ColMKey
            .Text = "MKey"

            .Col = ColPONO
            .Text = "PO.SA Number"

            .Col = ColItemSNO
            .Text = "Item Sr No"

            .Col = ColPartNo
            .Text = "Part No"

            .Col = ColQty
            .Text = "ASN Quantity"

            .Col = ColInvNo
            .Text = "Invoice No"

            .Col = ColInvDate
            .Text = "Invoice Date (dd.mm.yyyy)"

            .Col = ColInvoiceAmtWithTCS
            .Text = "Invoice Amount Inclusive of TCS"

            .Col = ColExciseAmount
            .Text = "Excise Amount"

            .Col = ColLRNo
            .Text = "LR No"

            .Col = ColLRDate
            .Text = "LR Date (dd.mm.yyyy)"

            .Col = ColVehicleNo
            .Text = "Veh No"

            .Col = ColItemAmount
            .Text = "Material Base Price"

            .Col = ColIRNNo
            .Text = "IRN Number"

            .Col = ColVendorGST
            .Text = "Vendor GST"

            .Col = ColCustomerGST
            .Text = "Mahindra GST"

            .Col = ColPMDesc1
            .Text = "Packaging Material1"

            .Col = ColPMQnty1
            .Text = "Packaging Material1 Quantity"

            .Col = ColPMDesc2
            .Text = "Packaging Material2"

            .Col = ColPMQnty2
            .Text = "Packaging Material2 Quantity"


            .Col = ColPMDesc3
            .Text = "Packaging Material3"

            .Col = ColPMQnty3
            .Text = "Packaging Material3 Quantity"


        End With
    End Sub
    Private Sub frmMahindraBarcode_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub
    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        'If eventSender.Checked Then
        '    Dim Index As Short = OptSelection.GetIndex(eventSender)
        '    Dim cntRow As Integer
        '    With SprdMain
        '        For cntRow = 1 To .MaxRows
        '            .Row = cntRow
        '            .Col = ColFlag
        '            .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        '        Next
        '    End With
        'End If
    End Sub
    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        cmdShow.Enabled = True
    End Sub
    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(TxtDateFrom.Text)) = False Then
        '        TxtDateFrom.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        cmdShow.Enabled = True
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtDateTo.Text)) = False Then
        '        txtDateTo.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And SUPP_CUST_TYPE In ('S','C')"
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
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
        On Error GoTo ERR1
        Dim SqlStr As String

        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtAccount.Enabled = True
            cmdsearch.Enabled = True
        End If
        cmdShow.Enabled = True
    End Sub

    Private Sub cmdsearch_Click(sender As Object, e As EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub

    Private Sub TxtAccount_TextChanged(sender As Object, e As EventArgs) Handles TxtAccount.TextChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub _OptSelection_1_Click(sender As Object, e As EventArgs) Handles _OptSelection_1.Click
        cmdShow.Enabled = True
    End Sub

    Private Sub _OptSelection_0_Click(sender As Object, e As EventArgs) Handles _OptSelection_0.Click
        cmdShow.Enabled = True
    End Sub

    Private Sub CmdPreview_Click(sender As Object, e As EventArgs) Handles CmdPreview.Click
        On Error GoTo ErrPart
        Dim mInvoicePrint As Boolean
        Dim mAnnexPrint As String
        Dim mSubsidiaryChallanPrint As String
        Dim mEXPAnnexPrint As String
        Dim mMaxRow As Integer
        Dim mSC_All As String
        Dim mSC_F4No As String
        Dim CntCount As Integer
        Dim mInvoicePrintType As String = ""
        Dim mExtraRemarks As String
        Dim mPrintOption As String = ""
        Dim mMKey As String


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
ErrPart:
        frmPrintInvCopy.Close()
        frmPrintInvCopy.Dispose()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ReportOnSales(ByRef mPrintOption As String)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNO As String
        Dim mWithInState As String
        Dim mRMCustomer As Boolean
        Dim CntRow As Long


        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)
        Dim mMKey As String
        Dim pCustomerName As String
        Dim mCustomerCode As String
        Dim pLocation As String
        Dim mInvoiceSeq As String
        Dim mIRNNo As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillNoStr As String

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryForPrint(ByRef mMKey As String, mCustomerCode As String) As String

        Dim pBarCodeString As String = ""
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mInvoicePrintType As String
        Dim CntCount As Integer
        Dim mUpdateStart As Boolean
        Dim mSqlStr As String
        On Error GoTo ErrPart


        Exit Function
ErrPart:
        If mUpdateStart = True Then
            PubDBCn.RollbackTrans()
        End If
        SelectQryForPrint = ""
    End Function


    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        On Error GoTo ErrPart
        Dim mInvoicePrint As Boolean
        Dim mAnnexPrint As String
        Dim mSubsidiaryChallanPrint As String
        Dim mEXPAnnexPrint As String
        Dim mMaxRow As Integer
        Dim mSC_All As String
        Dim mSC_F4No As String
        Dim CntCount As Integer
        Dim mInvoicePrintType As String = ""
        Dim mExtraRemarks As String
        Dim mPrintOption As String = ""
        Dim mMKey As String


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
ErrPart:
        frmPrintInvCopy.Close()
        frmPrintInvCopy.Dispose()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtVehicle_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.DoubleClick
        SearchVehicle()
    End Sub
    Private Sub SearchVehicle()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtVehicle.Text, "FIN_VEHICLE_MST", "NAME", , , , SqlStr)
        If AcName <> "" Then
            txtVehicle.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtVehicle_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicle.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicle.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVehicle_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVehicle.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchVehicle()
    End Sub
    Private Sub txtVehicle_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVehicle.Validating
        '        Dim Cancel As Boolean = eventArgs.Cancel
        '        On Error GoTo ERR1
        '        Dim SqlStr As String

        '        If txtVehicle.Text = "" Then GoTo EventExitSub

        '        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        '        If MainClass.ValidateWithMasterTable((txtVehicle.Text), "NAME", "NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
        '            txtVehicle.Text = UCase(Trim(txtVehicle.Text))
        '        Else
        '            MsgInformation("No Such Vechicle in Vechicle Master")
        '            Cancel = True
        '        End If
        '        GoTo EventExitSub
        'ERR1:
        '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'EventExitSub:
        '        eventArgs.Cancel = Cancel
    End Sub
    Private Sub chkAllVehicle_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllVehicle.CheckStateChanged
        If chkAllVehicle.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtVehicle.Enabled = False
            cmdSearchVehicle.Enabled = False
        Else
            txtVehicle.Enabled = True
            cmdSearchVehicle.Enabled = True
        End If
        cmdShow.Enabled = True
    End Sub

    Private Sub cmdsearchVehicle_Click(sender As Object, e As EventArgs) Handles cmdSearchVehicle.Click
        SearchVehicle()
    End Sub

    Private Sub txtVehicle_TextChanged(sender As Object, e As EventArgs) Handles txtVehicle.TextChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub txtBillFrom_TextChanged(sender As Object, e As EventArgs) Handles txtBillFrom.TextChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub txtBillTo_TextChanged(sender As Object, e As EventArgs) Handles txtBillTo.TextChanged
        cmdShow.Enabled = True
    End Sub
End Class
