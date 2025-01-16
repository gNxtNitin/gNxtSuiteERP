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
Imports AxFPSpreadADO

Friend Class frmVehicleNoUpdate
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColMKey As Short = 1
    Private Const ColInvoiceSeq As Short = 2
    Private Const ColInvoiceNo As Short = 3
    Private Const CoInvoiceDate As Short = 4
    Private Const ColCustomerCode As Short = 5
    Private Const ColCustomerName As Short = 6
    Private Const ColLocation As Short = 7
    Private Const ColVendorCode As Short = 8
    Private Const ColDistance As Short = 9
    Private Const ColVechile As Short = 10
    Private Const ColBillAmount As Short = 11
    Private Const ColEWayNo As Short = 12
    Private Const ColEWayDate As Short = 13
    Private Const ColEWayBillUpToValid As Short = 14
    Private Const ColReConsolidationEWayNo As Short = 15
    Private Const ColFlag As Short = 16


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean


    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        cmdShow.Enabled = True
        cmdUpdateVehicleNo.Enabled = False
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        cmdShow.Enabled = True
        cmdUpdateVehicleNo.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
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

        cboReasonCode.SelectedIndex = -1
        txtReason.Text = ""

        If cboShow.SelectedIndex = 0 Then
            Show1()
        ElseIf cboShow.SelectedIndex = 1 Then
            ShowVendorRJ()
        Else
            ShowRGP()
        End If

        FormatSprdMain()
        cmdShow.Enabled = False
        cmdUpdateVehicleNo.Enabled = True
        cmdConsolidatedEWayBill.Enabled = True
    End Sub
    Private Sub ShowVendorRJ()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mDivision As Double


        'If lblBookType.Text= "II" Then  ''I Invoice , I - IRN
        ''CHALLAN_PREFIX GATEPASS_NO

        SqlStr = "SELECT IH.AUTO_KEY_DESP ,1 AS INVOICESEQTYPE, IH.AUTO_KEY_DESP BILLNO,"


        SqlStr = SqlStr & vbCrLf _
                & " IH.DESP_DATE, " & vbCrLf _
                & " IH.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME, IH.BILL_TO_LOC_ID, ACM.VENDOR_CODE, IH.TRANS_DISTANCE, IH.VEHICLE_NO, ITEMVALUE AS NETVALUE, " & vbCrLf _
                & " '' AS IRN_NO, '' AS IRN_ACK_DATE, '' AS IRN_ACK_NO, '' AS IRN_ACK_DATE, IH.E_BILLWAYNO," & vbCrLf _
                & " IH.E_BILLWAYDATE, IH.E_BILLWAYVAILDUPTO, E_BILLWAYFILEPATH,''," & vbCrLf _
                & " '','IRN Print','EWay Print' "

        SqlStr = SqlStr & vbCrLf _
                & " FROM DSP_DESPATCH_HDR IH, FIN_SUPP_CUST_MST ACM, FIN_DNCN_HDR CH" & vbCrLf _
                & " WHERE IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "


        SqlStr = SqlStr & vbCrLf _
            & " AND IH.Company_Code=CH.Company_Code AND IH.AUTO_KEY_SO=CH.MKEY" '' AND  ID.ITEM_CODE=CD.ITEM_CODE "  ''AND CD.MKEY='" & txtSONo.Text & "'"


        SqlStr = SqlStr & vbCrLf & "AND IH.DESPATCHTYPE=2"

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        SqlStr = SqlStr & vbCrLf & "AND (IH.E_BILLWAYNO IS NOT NULL OR IH.E_BILLWAYNO<>'')"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkAllVehicle.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND IH.VEHICLE_NO='" & MainClass.AllowSingleQuote(txtVehicle.Text) & "'"
        End If

        If txtConsolidateEWay.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND IH.CONSOLIDATION_E_BILLWAYDATE='" & MainClass.AllowSingleQuote(txtConsolidateEWay.Text) & "'"
        End If

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.DESP_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.DESP_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        'If txtBillFrom.Text <> "" Then
        '    SqlStr = SqlStr & vbCrLf _
        '        & " AND IH.AUTO_KEY_DESP>='" & MainClass.AllowSingleQuote(txtBillFrom.Text) & "'" & vbCrLf _
        '        & " AND IH.AUTO_KEY_DESP<='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
        'End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY AUTO_KEY_DESP"


        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub

    Private Sub ShowRGP()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mDivision As Double


        'If lblBookType.Text= "II" Then  ''I Invoice , I - IRN
        ''CHALLAN_PREFIX GATEPASS_NO

        SqlStr = "SELECT IH.AUTO_KEY_PASSNO ,1 AS INVOICESEQTYPE, "

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
            SqlStr = SqlStr & vbCrLf & " CHALLAN_PREFIX||TRIM(TO_CHAR(GATEPASS_NO,'000000')) BILLNO," & vbCrLf
        Else
            SqlStr = SqlStr & vbCrLf & " CHALLAN_PREFIX||GATEPASS_NO BILLNO," & vbCrLf
        End If


        SqlStr = SqlStr & vbCrLf _
                & " IH.GATEPASS_DATE, " & vbCrLf _
                & " IH.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME, IH.BILL_TO_LOC_ID, ACM.VENDOR_CODE, IH.TRANS_DISTANCE, IH.VEHICLE_NO, 0 AS NETVALUE, " & vbCrLf _
                & " '' AS IRN_NO, '' AS IRN_ACK_DATE, '' AS IRN_ACK_NO, '' AS IRN_ACK_DATE, IH.E_BILLWAYNO," & vbCrLf _
                & " IH.E_BILLWAYDATE, IH.E_BILLWAYVAILDUPTO, E_BILLWAYFILEPATH,''," & vbCrLf _
                & " '' ,'IRN Print','EWay Print'"

        SqlStr = SqlStr & vbCrLf _
                & " FROM INV_GATEPASS_HDR IH, FIN_SUPP_CUST_MST ACM" & vbCrLf _
                & " WHERE IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        'SqlStr = SqlStr & vbCrLf & "AND IH.INVOICESEQTYPE IN (1,2,6,9)"

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        SqlStr = SqlStr & vbCrLf & "AND (IH.E_BILLWAYNO IS NOT NULL OR IH.E_BILLWAYNO<>'')"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkAllVehicle.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND IH.VEHICLE_NO='" & MainClass.AllowSingleQuote(txtVehicle.Text) & "'"
        End If

        If txtConsolidateEWay.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND IH.CONSOLIDATION_E_BILLWAYDATE='" & MainClass.AllowSingleQuote(txtConsolidateEWay.Text) & "'"
        End If

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.GATEPASS_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.GATEPASS_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        'If txtBillFrom.Text <> "" Then
        '    SqlStr = SqlStr & vbCrLf _
        '        & " AND IH.AUTO_KEY_PASSNO>='" & MainClass.AllowSingleQuote(txtBillFrom.Text) & "'" & vbCrLf _
        '        & " AND IH.AUTO_KEY_PASSNO<='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
        'End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY CHALLAN_PREFIX||GATEPASS_NO"


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
    Public Sub frmVehicleNoUpdate_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmVehicleNoUpdate_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

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

        cboShow.Items.Clear()
        cboShow.Items.Add("INVOICE")
        cboShow.Items.Add("VENDOR REJECTION")
        cboShow.Items.Add("RGP")

        cboShow.SelectedIndex = 0

        cboReasonCode.Items.Clear()
        cboReasonCode.Items.Add("1. DUE TO BREAKDOWN")
        cboReasonCode.Items.Add("2. DUE TO TRANSSHIPMENT")
        cboReasonCode.Items.Add("3. OTHERS (PLS SPECIFY)")
        cboReasonCode.Items.Add("4. FIRST TIME")

        cboReasonCode.SelectedIndex = -1

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False

        'chkAllVehicle.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtConsolidateEWay.Enabled = True
        txtVehicle.Enabled = True
        cmdSearchVehicle.Enabled = True

        txtVehicleNew.Enabled = True
        cmdSearchVehicleNew.Enabled = True

        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        'txtBillFrom.Text = ""
        'txtBillTo.Text = ""

        FormatSprdMain()

        'If lblBookType.Text = "IIG" Then
        '    CmdSave.Enabled = True
        '    cmdGenerateEWayBill.Enabled = True
        'ElseIf lblBookType.Text = "IEG" Then
        '    CmdSave.Enabled = False
        '    cmdGenerateEWayBill.Enabled = True
        'ElseIf lblBookType.Text = "REG" Or lblBookType.Text = "VRJ" Then
        '    CmdSave.Enabled = False
        '    cmdGenerateEWayBill.Enabled = True
        'End If
        cmdShow.Enabled = True
        cmdUpdateVehicleNo.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
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


        SqlStr = "SELECT IH.MKEY, INVOICESEQTYPE, IH.BILLNO, IH.INVOICE_DATE,IH.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME, " & vbCrLf _
                & " IH.BILL_TO_LOC_ID, IH.VENDOR_CODE, IH.TRANS_DISTANCE, IH.VEHICLENO, IH.NETVALUE, " & vbCrLf _
                & " IH.E_BILLWAYNO," & vbCrLf _
                & " IH.E_BILLWAYDATE, IH.E_BILLWAYVAILDUPTO," & vbCrLf _
                & " '' "

        SqlStr = SqlStr & vbCrLf _
                & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST ACM" & vbCrLf _
                & " WHERE IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "


        SqlStr = SqlStr & vbCrLf & "AND IH.INVOICESEQTYPE IN (1,2,3,6,9)"



        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If


        SqlStr = SqlStr & vbCrLf & "AND (IH.E_BILLWAYNO IS NOT NULL OR IH.E_BILLWAYNO<>'')"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkAllVehicle.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND IH.VEHICLENO='" & MainClass.AllowSingleQuote(txtVehicle.Text) & "'"
        End If

        If txtConsolidateEWay.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND IH.CONSOLIDATION_E_BILLWAYDATE='" & MainClass.AllowSingleQuote(txtConsolidateEWay.Text) & "'"
        End If

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        'If txtBillFrom.Text <> "" Then
        '    SqlStr = SqlStr & vbCrLf _
        '        & " AND IH.BILLNO >= '" & MainClass.AllowSingleQuote(txtBillFrom.Text) & "'" & vbCrLf _
        '        & " AND IH.BILLNO <= '" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"
        'End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.BILLNO"


        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain()
        With SprdMain

            .MaxCols = ColFlag
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)
            .set_RowHeight(-1, RowHeight)

            .Row = -1

            .Col = ColMKey
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMKey, 12)
            .ColHidden = True

            .Col = ColInvoiceSeq
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColInvoiceSeq, 6)
            .ColHidden = True

            .Col = ColInvoiceNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColInvoiceNo, 12)

            .Col = CoInvoiceDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(CoInvoiceDate, 10)

            .Col = ColCustomerCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerCode, 7)

            .Col = ColCustomerName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerName, 28)

            .Col = ColVendorCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVendorCode, 8)

            .Col = ColLocation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocation, 12)

            .Col = ColVechile
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColVechile, 8)

            .Col = ColDistance
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColDistance, 8)
            .ColHidden = True ''

            .Col = ColBillAmount
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColBillAmount, 10)


            .Col = ColEWayNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColEWayNo, 12)
            '.ColHidden = IIf(lblBookType.Text = "IEG" Or lblBookType.Text = "REG" Or lblBookType.Text = "EC", False, True)

            .Col = ColEWayDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColEWayDate, 12)
            .ColHidden = True ''IIf(lblBookType.Text = "IEG" Or lblBookType.Text = "REG" Or lblBookType.Text = "EC", False, True)

            .Col = ColEWayBillUpToValid
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColEWayBillUpToValid, 12)
            .ColHidden = True

            .Col = ColReConsolidationEWayNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColReConsolidationEWayNo, 12)

            .Row = -1
            .Col = ColFlag
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColFlag, 8)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)


            MainClass.SetSpreadColor(SprdMain, -1)

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColReConsolidationEWayNo)
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

            .Col = ColInvoiceSeq
            .Text = "Invoice Seq"

            .Col = ColInvoiceNo
            .Text = "Invoice No"

            .Col = CoInvoiceDate
            .Text = "Invoice Date"

            .Col = ColCustomerCode
            .Text = "Customer Code"

            .Col = ColCustomerName
            .Text = "Customer Name"

            .Col = ColVendorCode
            .Text = "Vendor Code"

            .Col = ColLocation
            .Text = "Customer Location"

            .Col = ColDistance
            .Text = "Distance"

            .Col = ColVechile
            .Text = "Vechile No"

            .Col = ColBillAmount
            .Text = "Bill Amount"


            .Col = ColEWayNo
            .Text = "EWay No"

            .Col = ColReConsolidationEWayNo
            .Text = "ReConsolidation EWay No"

            .Col = ColEWayDate
            .Text = "EWay Date"

            .Col = ColEWayBillUpToValid
            .Text = "EWay Bill UpTo Valid"

            .Col = ColFlag
            .Text = "Generate (Yes/No)"

        End With
    End Sub
    Private Sub frmVehicleNoUpdate_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub
    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColFlag
                    .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Next
            End With
        End If
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
        cmdUpdateVehicleNo.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
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
        cmdUpdateVehicleNo.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
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
        cmdUpdateVehicleNo.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
    End Sub

    Private Sub cmdsearch_Click(sender As Object, e As EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub

    Private Sub TxtAccount_TextChanged(sender As Object, e As EventArgs) Handles TxtAccount.TextChanged
        cmdShow.Enabled = True
        cmdUpdateVehicleNo.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
    End Sub

    Private Sub _OptSelection_1_Click(sender As Object, e As EventArgs) Handles _OptSelection_1.Click
        'cmdShow.Enabled = True
        'cmdUpdateVehicleNo.Enabled = False
    End Sub

    Private Sub _OptSelection_0_Click(sender As Object, e As EventArgs) Handles _OptSelection_0.Click
        'cmdShow.Enabled = True
        'cmdUpdateVehicleNo.Enabled = False
    End Sub

    Private Sub cmdUpdateVehicleNo_Click(sender As Object, e As EventArgs) Handles cmdUpdateVehicleNo.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String
        Dim mInvoiceNo As String
        Dim mInvoiceDate As String
        Dim mEWayBillNo As String
        Dim meInvoiceApp As String
        Dim mInvoiceSeq As Long
        Dim mUpdateCount As Integer
        Dim mMKey As String
        Dim mCustomerName As String
        Dim mValue As String
        Dim mLocation As String
        Dim pPINNo As String

        meInvoiceApp = IIf(IsDBNull(RsCompany.Fields("EWAYBILLAPP").Value), "N", RsCompany.Fields("EWAYBILLAPP").Value)
        If meInvoiceApp = "N" Then Exit Sub

        If cboReasonCode.SelectedIndex = -1 Then
            MsgInformation("Please select the Reason Code.")
            Exit Sub
        End If
        If txtReason.Text = "" Then
            MsgInformation("Please select the Reason Remarks.")
            Exit Sub
        End If

        If txtVehicle.Text = "" Then
            MsgInformation("Please select the Old Vehicle No.")
            Exit Sub
        End If

        If txtVehicleNew.Text = "" Then
            MsgInformation("Please select the New Vehicle No.")
            Exit Sub
        End If

        If txtVehicleNew.Text = txtVehicle.Text Then
            MsgInformation("Vehicle No. are same.")
            Exit Sub
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColFlag
                If SprdMain.Value = System.Windows.Forms.CheckState.Checked Then
                    .Col = ColMKey
                    mMKey = Trim(.Text)

                    .Col = ColInvoiceSeq
                    mInvoiceSeq = Val(.Text)

                    .Col = ColInvoiceNo
                    mInvoiceNo = Trim(.Text)

                    .Col = CoInvoiceDate
                    mInvoiceDate = Trim(.Text)

                    .Col = ColCustomerName
                    mCustomerName = Trim(.Text)

                    .Col = ColLocation
                    mLocation = Trim(.Text)

                    .Col = ColEWayNo
                    mEWayBillNo = Trim(.Text)

                    If WebRequestUpdateVehicleNo(mMKey, mInvoiceSeq, mCustomerName, mEWayBillNo) = True Then

                        If cboShow.SelectedIndex = 0 Then

                            SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf _
                                    & " VEHICLENO ='" & txtVehicleNew.Text & "'" & vbCrLf _
                                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                    & " AND MKEY ='" & mMKey & "' AND VEHICLENO ='" & txtVehicle.Text & "'"

                        ElseIf cboShow.SelectedIndex = 1 Then

                            SqlStr = "UPDATE DSP_DESPATCH_HDR SET " & vbCrLf _
                                    & " VEHICLE_NO ='" & txtVehicleNew.Text & "'" & vbCrLf _
                                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                    & " AND AUTO_KEY_DESP ='" & mMKey & "' AND VEHICLE_NO='" & txtVehicle.Text & "' "

                        Else

                            SqlStr = "UPDATE INV_GATEPASS_HDR SET " & vbCrLf _
                                    & " VEHICLE_NO ='" & txtVehicleNew.Text & "'" & vbCrLf _
                                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                    & " AND AUTO_KEY_PASSNO ='" & mMKey & "' AND VEHICLE_NO='" & txtVehicle.Text & "'"

                        End If
                        PubDBCn.Execute(SqlStr)

                        mUpdateCount = mUpdateCount + 1
                    Else
                        GoTo ErrPart
                    End If
                End If
NextRowNo:

            Next
        End With
        PubDBCn.CommitTrans()

        MsgBox("Total " & mUpdateCount & " Invoice Updated.", MsgBoxStyle.Information)
        Exit Sub
ErrPart:
        ''Resume
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
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
        cmdUpdateVehicleNo.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
    End Sub

    Private Sub cmdsearchVehicle_Click(sender As Object, e As EventArgs) Handles cmdSearchVehicle.Click
        SearchVehicle()
    End Sub

    Private Sub txtVehicle_TextChanged(sender As Object, e As EventArgs) Handles txtVehicle.TextChanged
        cmdShow.Enabled = True
        cmdUpdateVehicleNo.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
    End Sub

    Private Sub txtBillFrom_TextChanged(sender As Object, e As EventArgs)
        cmdShow.Enabled = True
        cmdUpdateVehicleNo.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
    End Sub

    Private Sub txtBillTo_TextChanged(sender As Object, e As EventArgs)
        cmdShow.Enabled = True
        cmdUpdateVehicleNo.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
    End Sub

    Private Sub frmVehicleNoUpdate_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        'UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtVehicleNew_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicleNew.DoubleClick
        SearchVehicleNew()
    End Sub
    Private Sub SearchVehicleNew()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtVehicleNew.Text, "FIN_VEHICLE_MST", "NAME", , , , SqlStr)
        If AcName <> "" Then
            txtVehicleNew.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtVehicleNew_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicleNew.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicleNew.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVehicleNew_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVehicleNew.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchVehicleNew()
    End Sub
    Private Sub txtVehicleNew_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVehicleNew.Validating
        '        Dim Cancel As Boolean = eventArgs.Cancel
        '        On Error GoTo ERR1
        '        Dim SqlStr As String

        '        If txtVehicleNew.Text = "" Then GoTo EventExitSub

        '        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        '        If MainClass.ValidateWithMasterTable((txtVehicleNew.Text), "NAME", "NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
        '            txtVehicleNew.Text = UCase(Trim(txtVehicleNew.Text))
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
    Private Sub chkAllVehicleNew_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllVehicleNew.CheckStateChanged
        If chkAllVehicleNew.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtVehicleNew.Enabled = False
            cmdSearchVehicleNew.Enabled = False
        Else
            txtVehicleNew.Enabled = True
            cmdSearchVehicleNew.Enabled = True
        End If
        cmdShow.Enabled = True
        cmdUpdateVehicleNo.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
    End Sub

    Private Sub cmdSearchVehicleNew_Click(sender As Object, e As EventArgs) Handles cmdSearchVehicleNew.Click
        SearchVehicleNew()
    End Sub

    Private Sub txtVehicleNew_TextChanged(sender As Object, e As EventArgs) Handles txtVehicleNew.TextChanged
        'cmdShow.Enabled = True
        'cmdUpdateVehicleNo.Enabled = False
    End Sub
    Public Function WebRequestUpdateVehicleNo(ByRef pMKey As String, ByRef pInvoiceSeq As Long, ByRef pCustomerName As String,
                                              ByRef pEWayBillNo As String) As Boolean
        On Error GoTo ErrPart
        Dim url As String
        Dim pUserGSTin As String
        Dim pDocNo As String
        Dim pDocDate As String

        Dim pTransMode As String
        Dim pTransModeStr As String

        Dim pTransporterId As String
        Dim pTransDocNo As String
        Dim pTransDocDate As String
        Dim pVehicleType As String


        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim pStaus As String

        Dim mBody As String

        Dim pResponseText As String
        Dim pError As String

        Dim pCDKey As String
        Dim pEFUserName As String
        Dim pEFPassword As String
        Dim pEWBUserName As String
        Dim pEWBPassword As String

        Dim pIsTesting As String = "Y"
        Dim xSqlStr As String
        Dim pStateName As String
        Dim pStateCity As String
        Dim pStateCode As String

        Dim RsTempDet As ADODB.Recordset

        If GetWebTeleWaySetupContents(url, "VU", pCDKey, pEFUserName, pEFPassword, pEWBUserName, pEWBPassword, pIsTesting) = False Then GoTo ErrPart

        'If pIsTesting = "Y" Then
        '    ''1000687	29AAACW3775F000	Admin!23..	29AAACW3775F000	Admin!23..	29AAACW3775F000	29AAACW3775F000

        '    url = "http://ip.webtel.in/eWayGSP2/Sandbox/EWayBill/v1.3/GenEWB"
        '    pCDKey = "1000687"
        '    pEFUserName = "29AAACW3775F000"
        '    pEFPassword = "Admin!23.."
        '    pEWBUserName = "29AAACW3775F000"
        '    pEWBPassword = "Admin!23.."
        '    pUserGSTin = "29AAACW3775F000" ' IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) ''"05AAAAU3306Q1ZC" ''
        '    pFromGSTin = "29AAACW3775F000" ' IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) '"05AAAAU3306Q1ZC" ''
        'Else
        pUserGSTin = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) ''"05AAAAU3306Q1ZC" ''
        'End If

        '"CDKey": "1000687
        pStateCity = IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        pStateName = IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value)
        pStateCode = GetStateCode(pStateName)

        If cboShow.SelectedIndex = 0 Then


            xSqlStr = " SELECT IH.* " & vbCrLf _
                    & " FROM FIN_INVOICE_HDR IH" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " And IH.MKEY='" & pMKey & "'"
        ElseIf cboShow.SelectedIndex = 2 Then
            xSqlStr = " SELECT IH.* " & vbCrLf _
                        & " FROM INV_GATEPASS_HDR IH" & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " And IH.AUTO_KEY_PASSNO='" & pMKey & "'"
        Else
            xSqlStr = " SELECT IH.*, IH.TRANSPORTER_NAME AS CARRIERS " & vbCrLf _
                   & " FROM DSP_DESPATCH_HDR IH" & vbCrLf _
                   & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " And IH.AUTO_KEY_DESP='" & pMKey & "'"
        End If

        MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTempDet.EOF = False Then

            If cboShow.SelectedIndex = 0 Then
                pDocNo = IIf(IsDBNull(RsTempDet.Fields("BILLNO").Value), "", RsTempDet.Fields("BILLNO").Value) '' Trim(txtPreInvoice.Text) & Trim(txtInvoiceNo.Text)
                pDocDate = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("INVOICE_DATE").Value), "", RsTempDet.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
            ElseIf cboShow.SelectedIndex = 2 Then
                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                    pDocNo = IIf(IsDBNull(RsTempDet.Fields("CHALLAN_PREFIX").Value), "", RsTempDet.Fields("CHALLAN_PREFIX").Value) & VB6.Format(IIf(IsDBNull(RsTempDet.Fields("GATEPASS_NO").Value), "", RsTempDet.Fields("GATEPASS_NO").Value), "000000") '' Trim(txtPreInvoice.Text) & Trim(txtInvoiceNo.Text)
                Else
                    pDocNo = IIf(IsDBNull(RsTempDet.Fields("CHALLAN_PREFIX").Value), "", RsTempDet.Fields("CHALLAN_PREFIX").Value) & IIf(IsDBNull(RsTempDet.Fields("GATEPASS_NO").Value), "", RsTempDet.Fields("GATEPASS_NO").Value) '' Trim(txtPreInvoice.Text) & Trim(txtInvoiceNo.Text)
                End If
                pDocDate = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("GATEPASS_DATE").Value), "", RsTempDet.Fields("GATEPASS_DATE").Value), "DD/MM/YYYY")
            Else
                pDocNo = IIf(IsDBNull(RsTempDet.Fields("AUTO_KEY_DESP").Value), "", RsTempDet.Fields("AUTO_KEY_DESP").Value) '' Trim(txtPreInvoice.Text) & Trim(txtInvoiceNo.Text)
                pDocDate = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("DESP_DATE").Value), "", RsTempDet.Fields("DESP_DATE").Value), "DD/MM/YYYY")
            End If

            pTransModeStr = IIf(IsDBNull(RsTempDet.Fields("TRANSPORT_MODE").Value), "0", RsTempDet.Fields("TRANSPORT_MODE").Value)
            pTransModeStr = IIf(pTransModeStr = "", "1", pTransModeStr)
            pTransMode = VB.Left(pTransModeStr, 1)       'VB.Left(cboTransmode.Text, 1)
            pTransporterId = IIf(IsDBNull(RsTempDet.Fields("TRANSPORTER_GSTNO").Value), "", RsTempDet.Fields("TRANSPORTER_GSTNO").Value)        '  Trim(txtTransportCode.Text)
            pTransDocNo = IIf(IsDBNull(RsTempDet.Fields("GRNO").Value), "", RsTempDet.Fields("GRNO").Value)        ' Trim(txtTransportDocNo.Text)
            pTransDocDate = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("GRDATE").Value), "", RsTempDet.Fields("GRDATE").Value), "DD/MM/YYYY") ''IIf(pTransDocNo = "", "", Format(txtTransDocDate.Text, "DD/MM/YYYY"))


        End If

        Dim http As Object  ' MSXML2.XMLHTTP60 '' MSXML.xmlhttp
        http = CreateObject("MSXML2.ServerXMLHTTP")
        http.Open("POST", url, False)

        http.setRequestHeader("Content-Type", "application/json")


        mBody = "{""Push_Data_List"":["

        mBody = mBody & "{"
        mBody = mBody & """GSTIN"":""" & pUserGSTin & ""","
        mBody = mBody & """EWBNumber"":""" & pEWayBillNo & ""","
        mBody = mBody & """VehicleNumber"":""" & txtVehicleNew.Text & ""","
        mBody = mBody & """SupPlace"":""" & pStateCity & ""","
        mBody = mBody & """SupState"":""" & pStateCode & ""","
        mBody = mBody & """ReasonCode"":""" & Mid(cboReasonCode.Text, 1, 1) & ""","
        mBody = mBody & """ReasonRem"":""" & txtReason.Text & ""","
        mBody = mBody & """TransDocNo"":""" & pTransDocNo & ""","
        mBody = mBody & """TransDocDate"":""" & VB6.Format(pTransDocDate, "YYYYMMDD") & ""","
        mBody = mBody & """TransMode"":""" & pTransMode & ""","
        mBody = mBody & """EWBUserName"":""" & pEWBUserName & ""","
        mBody = mBody & """EWBPassword"":""" & pEWBPassword & """"

        mBody = mBody & "}],"
        mBody = mBody & """Year"":""" & Year(CDate(pDocDate)) & ""","
        mBody = mBody & """Month"":""" & Month(CDate(pDocDate)) & ""","
        mBody = mBody & """EFUserName"":""" & pEFUserName & ""","
        mBody = mBody & """EFPassword"":""" & pEFPassword & ""","
        mBody = mBody & """CDKey"":""" & pCDKey & """"

        mBody = mBody & "}"

        http.Send(mBody)

        pResponseText = http.responseText

        pResponseText = Replace(pResponseText, "[", "")
        pResponseText = Replace(pResponseText, "]", "")
        pResponseText = Replace(pResponseText, "\", "")
        pResponseText = Replace(pResponseText, """", "'")
        pResponseText = Replace(pResponseText, "'{", "{")
        pResponseText = Replace(pResponseText, "}'", "}")



        Dim post As Object
        pStaus = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .IsSuccess = False})).IsSuccess  '\'IsSuccess

        If UCase(pStaus) = "TRUE" Then
            WebRequestUpdateVehicleNo = True
        End If

        If UCase(pStaus) = "FALSE" Then
            pError = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .ErrorMessage = ""})).ErrorMessage ' JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
            MsgInformation(pError)
            WebRequestUpdateVehicleNo = False
            http = Nothing
            Exit Function
        End If

        'WebRequestUpdateVehicleNo = True
        http = Nothing
        '    Set httpGen = Nothing
        Exit Function
ErrPart:
        'Resume
        WebRequestUpdateVehicleNo = False
        http = Nothing
        'MsgBox(Err.Description)
        'PubDBCn.RollbackTrans()
    End Function
    Private Sub txtConsolidateEWay_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtConsolidateEWay.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtConsolidateEWay.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtConsolidateEWay_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtConsolidateEWay.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchVehicle()
    End Sub


    Private Sub txtConsolidateEWay_TextChanged(sender As Object, e As EventArgs) Handles txtConsolidateEWay.TextChanged
        cmdShow.Enabled = True
        cmdUpdateVehicleNo.Enabled = False
        cmdConsolidatedEWayBill.Enabled = False
    End Sub

    Private Sub cmdConsolidatedEWayBill_Click(sender As Object, e As EventArgs) Handles cmdConsolidatedEWayBill.Click
        ''Public Function WebRequestEWayBillByIRN(ByRef pMKey As String, ByRef pIRNNo As String, pInvoiceSeqType As Long) As Boolean
        On Error GoTo ErrPart
        Dim url As String

        Dim mUserName As String
        Dim mPassword As String

        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mBody As String
        Dim mResponseId As String
        Dim mResponseIdStr As String
        Dim url1 As String
        Dim WebRequestGen As String
        Dim pStaus As String

        Dim pError As String
        Dim mBMPFileName As String

        Dim pResponseText As String

        Dim mCDKey As String
        Dim mEFUserName As String
        Dim mEFPassword As String
        Dim mEWBUserName As String
        Dim mEWBPassword As String
        Dim mIsTesting As String
        Dim pGSTIN As String
        Dim mStateCode As String

        Dim mUpdateStart As Boolean = False

        If chkAllVehicle.CheckState = System.Windows.Forms.CheckState.Checked Or Trim(txtVehicle.Text) = "" Then
            MsgInformation("Please Select The Vehicle No.")
            Exit Sub
        End If

        If Trim(txtConsolidateEWay.Text) = "" Then
            MsgInformation("Please Select The Consolidate EWay.")
            Exit Sub
        End If

        If cboReasonCode.SelectedIndex = -1 Then
            MsgInformation("Please select the Reason Code.")
            Exit Sub
        End If
        If txtReason.Text = "" Then
            MsgInformation("Please select the Reason Remarks.")
            Exit Sub
        End If


        'pCDKey = "1000687"
        'pEFUserName = "29AAACW3775F000"
        'pEFPassword = "Admin!23.."
        'pEWBUserName = "29AAACW3775F000"
        'pEWBPassword = "Admin!23.."

        If GetWebTeleWaySetupContents(url, "RECON", mCDKey, mEFUserName, mEFPassword, mEWBUserName, mEWBPassword, mIsTesting) = False Then GoTo ErrPart

        If mIsTesting = "Y" Then
            url = "http://ip.webtel.in/eWayGSP2/Sandbox/EWayBill/v1.3/GenEWB"
            mCDKey = "1000687"
            mEFUserName = "29AAACW3775F000"
            mEFPassword = "Admin!23.."
            mEWBUserName = "29AAACW3775F000"
            mEWBPassword = "Admin!23.."
            pGSTIN = "29AAACW3775F000" ' IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) ''"05AAAAU3306Q1ZC" ''
        Else
            pGSTIN = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) ''"05AAAAU3306Q1ZC" ''
        End If

        Dim http As Object   '' Dim http As MSXML2.XMLHTTP60 '' MSXML.xmlhttp							
        http = CreateObject("MSXML2.ServerXMLHTTP")


        http.Open("POST", url, False)

        http.setRequestHeader("Content-Type", "application/json")

        'Dim details As New List(Of CONSOLIDATIONEWAYBILLBYIRN)()

        Dim pEWBNo As Long
        Dim pSupPlace As String
        Dim pSupState As String
        Dim pTransdocno As String
        Dim pTransDocDate As String
        Dim pTransMode As String
        Dim pConsolidationEWayNo As Long
        Dim mInvoiceSeq As String
        Dim pMKey As String
        Dim SqlStr As String
        Dim RsTempDet As ADODB.Recordset = Nothing
        Dim pTransModeStr As String

        mBody = "{""Push_Data_List"":["

        For CntRow = 1 To SprdMain.MaxRows
            SprdMain.Row = CntRow
            SprdMain.Col = ColFlag
            If SprdMain.Value = System.Windows.Forms.CheckState.Checked Then

                SprdMain.Col = ColMKey
                pMKey = Trim(SprdMain.Text)

                SprdMain.Col = ColEWayNo

                pEWBNo = Val(SprdMain.Text)

                'SprdMain.Col = ColConsolidationEWayNo
                'pConsolidationEWayNo = Val(SprdMain.Text)

                SprdMain.Col = ColInvoiceSeq
                mInvoiceSeq = Val(SprdMain.Text)

                SprdMain.Col = ColInvoiceNo
                pTransdocno = Trim(SprdMain.Text)

                SprdMain.Col = CoInvoiceDate
                pTransDocDate = Trim(SprdMain.Text)

                pSupPlace = IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
                pSupState = IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value)
                mStateCode = GetStateCode(pSupState)

                If cboShow.SelectedIndex = 0 Then
                    SqlStr = " SELECT IH.* " & vbCrLf _
                        & " FROM FIN_INVOICE_HDR IH" & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " And IH.MKEY='" & pMKey & "'"
                ElseIf cboShow.SelectedIndex = 1 Then
                    SqlStr = " SELECT IH.* " & vbCrLf _
                        & " FROM DSP_DESPATCH_HDR IH" & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " And IH.AUTO_KEY_DESP='" & pMKey & "'"
                Else
                    SqlStr = " SELECT IH.* " & vbCrLf _
                        & " FROM INV_GATEPASS_HDR IH" & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " And IH.AUTO_KEY_PASSNO='" & pMKey & "'"

                End If


                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)

                pTransMode = 1
                If RsTempDet.EOF = False Then
                    pTransModeStr = IIf(IsDBNull(RsTempDet.Fields("TRANSPORT_MODE").Value), "1", RsTempDet.Fields("TRANSPORT_MODE").Value)
                    pTransModeStr = IIf(pTransModeStr = "", "1", pTransModeStr)
                    pTransMode = VB.Left(pTransModeStr, 1)       'VB.Left(cboTransmode.Text, 1)
                End If

                If pEWBNo > 0 And pConsolidationEWayNo = 0 Then
                    mBody = mBody & "{"
                    mBody = mBody & """GSTIN"":""" & pGSTIN & ""","
                    mBody = mBody & """SupState"":""" & mStateCode & ""","
                    mBody = mBody & """SupPlace"":""" & pSupPlace & ""","

                    mBody = mBody & """tripSheetNo"":""" & pEWBNo & ""","
                    mBody = mBody & """TransMode"":""" & pTransMode & ""","
                    mBody = mBody & """Transdocno"":""" & pTransdocno & ""","
                    mBody = mBody & """TransDocDate"":""" & VB6.Format(pTransDocDate, "YYYYMMDD") & ""","
                    mBody = mBody & """VehicleNumber"":""" & txtVehicle.Text & ""","
                    mBody = mBody & """ReasonCode"":""" & Mid(cboReasonCode.Text, 1, 1) & ""","
                    mBody = mBody & """Remark"":""" & txtReason.Text & ""","

                    mBody = mBody & """EWBUserName"":""" & mEWBUserName & ""","
                    mBody = mBody & """EWBPassword"":""" & mEWBPassword & """"
                End If

                If CntRow = SprdMain.MaxRows Then
                    mBody = mBody & "}"
                Else
                    mBody = mBody & "},"
                End If
            End If
        Next


        mBody = mBody & "],"
        mBody = mBody & """Year"":""" & Year(CDate(pTransDocDate)) & ""","
        mBody = mBody & """Month"":""" & Month(CDate(pTransDocDate)) & ""","
        mBody = mBody & """EFUserName"":""" & mEFUserName & ""","
        mBody = mBody & """EFPassword"":""" & mEFPassword & ""","
        mBody = mBody & """CDKey"":""" & mCDKey & """"

        mBody = mBody & "}"


        http.Send(mBody)

        pResponseText = http.responseText

        pResponseText = Replace(pResponseText, "[", "")
        pResponseText = Replace(pResponseText, "]", "")
        pResponseText = Replace(pResponseText, "\", "")
        pResponseText = Replace(pResponseText, """", "'")
        pResponseText = Replace(pResponseText, "'{", "{")
        pResponseText = Replace(pResponseText, "}'", "}")


        Dim meWayResponseID As String
        Dim meWayResponseDate As String

        Dim post As Object
        pStaus = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .IsSuccess = ""})).IsSuccess  '\'IsSuccess

        If UCase(pStaus) = "TRUE" Then
            meWayResponseID = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .EWayBill = ""})).EWayBill   'JsonTest.Item("Irn")
            meWayResponseDate = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .Date = ""})).Date   'JsonTest.Item("Irn")
            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()
            mUpdateStart = True

            For CntRow = 1 To SprdMain.MaxRows
                SprdMain.Row = CntRow
                SprdMain.Col = ColFlag
                If SprdMain.Value = System.Windows.Forms.CheckState.Checked Then
                    SprdMain.Col = ColEWayNo
                    SprdMain.Row = CntRow

                    SprdMain.Col = ColMKey
                    pMKey = Trim(SprdMain.Text)

                    SprdMain.Col = ColReConsolidationEWayNo
                    SprdMain.Text = meWayResponseID

                    If cboShow.SelectedIndex = 0 Then  'INVOICE
                        SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf _
                                    & " CONSOLIDATION_E_BILLWAYNO ='" & Val(meWayResponseID) & "'," & vbCrLf _
                                    & " CONSOLIDATION_E_BILLWAYDATE =TO_DATE('" & VB6.Format(meWayResponseDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                    & " AND MKEY ='" & pMKey & "'"
                    ElseIf cboShow.SelectedIndex = 1 Then ''Despatch
                        SqlStr = "UPDATE DSP_DESPATCH_HDR SET " & vbCrLf _
                                & " CONSOLIDATION_E_BILLWAYNO ='" & Val(meWayResponseID) & "'" & vbCrLf _
                                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                & " AND AUTO_KEY_DESP ='" & pMKey & "'"
                    Else
                        SqlStr = "UPDATE INV_GATEPASS_HDR SET " & vbCrLf _
                                    & " CONSOLIDATION_E_BILLWAYNO ='" & Val(meWayResponseID) & "'" & vbCrLf _
                                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                    & " AND AUTO_KEY_PASSNO ='" & pMKey & "'"

                    End If
                    PubDBCn.Execute(SqlStr)
                End If
            Next

            PubDBCn.CommitTrans()
            'WebRequestCreateEWayBill = meWayResponseID
        End If

        mUpdateStart = False
        If UCase(pStaus) = "FALSE" Then
            pError = (JsonConvert.DeserializeAnonymousType(pResponseText, New With {Key .ErrorMessage = ""})).ErrorMessage ' JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")						
            MsgInformation(pError)
            'WebRequestCreateEWayBill = pError
            http = Nothing
            Exit Sub
        End If
ErrPart:
        '    Resume							
        'http = Nothing							
        MsgBox(Err.Description)
        If mUpdateStart = True Then
            PubDBCn.RollbackTrans()
        End If
    End Sub
End Class
