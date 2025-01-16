Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports System.ComponentModel
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl

Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Imports System.Data.OleDb
'Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6
Friend Class frmGatePassGST
    Inherits System.Windows.Forms.Form

    Public Class EWAYBILLPRN
        Public Property GSTIN As String
        Public Property ewbNo As String     'Long
        Public Property Year As Integer
        Public Property Month As Integer
        Public Property EFUserName As String
        Public Property EFPassword As String
        Public Property CDKey As String

        Public Property EWBUserName As String
        Public Property EWBPassword As String

    End Class

    Dim RsReqMain As ADODB.Recordset ''Recordset
    Dim RsReqDetail As ADODB.Recordset ''Recordset
    ''Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim xMyMenu As String

    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColItemDesc As Short = 3
    Private Const ColUOM As Short = 4
    Private Const ColHSNCode As Short = 5
    Private Const ColStockType As Short = 6
    Private Const ColLotNo As Short = 7
    Private Const ColHeatNo As Short = 8
    Private Const ColBatchNo As Short = 9
    Private Const ColStockQty As Short = 10
    Private Const ColQtyKGs As Short = 11
    Private Const ColQty As Short = 12
    Private Const ColReturnQty As Short = 13
    Private Const ColRate As Short = 14
    Private Const ColAmount As Short = 15
    Private Const ColCGSTPer As Short = 16
    Private Const ColCGSTAmount As Short = 17
    Private Const ColSGSTPer As Short = 18
    Private Const ColSGSTAmount As Short = 19
    Private Const ColIGSTPer As Short = 20
    Private Const ColIGSTAmount As Short = 21
    Private Const ColF4No As Short = 22
    Private Const ColIncomingItemCode As Short = 23
    Private Const ColJobOrderNo As Short = 24
    Private Const colRemarks As Short = 25
    Dim mDeptCode As String


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub cboGatePasstype_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGatePasstype.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillTo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboGatePasstype_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGatePasstype.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)

        If ADDMode = True Then
            If VB.Left(cboGatePasstype.Text, 1) = "R" Or VB.Left(cboGatePasstype.Text, 1) = "G" Then
                txtRemarks.Text = "GOODS NOT FOR SALE, MATERIAL GOING IN TROLLY."
            Else
                txtRemarks.Text = "GOODS NOT FOR SALE, PARTY OWN MATERIAL GOING BACK."
            End If
        End If
    End Sub

    Private Sub cboGSTStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGSTStatus.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboGSTStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGSTStatus.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboMaterial_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMaterial.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboMaterial_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMaterial.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkF4status_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkF4status.CheckStateChanged
        Dim mF4No As Double

        mF4No = Val(txtF4no.Text)

        If chkF4status.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtF4no.Text = AUTO57F4()
        Else
            txtF4no.Text = CStr(0)
        End If

    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtGatepassno.Enabled = True
            txtGatePassDate.Enabled = True
            cmdSearch.Enabled = False

        Else
            cmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillCboStatus()

        On Error GoTo FillERR
        Dim RsFormType As ADODB.Recordset


        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1

        cboStatus.Items.Clear()
        cboStatus.Items.Add(("Pending"))
        cboStatus.Items.Add(("Completed"))
        cboStatus.Items.Add(("Cancelled"))

        cboGatePasstype.Items.Clear()
        cboGatePasstype.Items.Add(("RGP"))
        cboGatePasstype.Items.Add(("NRGP"))

        cboMaterial.Items.Clear()
        cboMaterial.Items.Add(("STORES"))
        cboMaterial.Items.Add(("SHOP FLOOR"))

        cboPurpose.Items.Clear()
        cboPurpose.Items.Add("A : None")
        cboPurpose.Items.Add("B : Jobwork")
        cboPurpose.Items.Add("C : Repair / Refill / Work Order")
        cboPurpose.Items.Add("D : Tool Trial")
        cboPurpose.Items.Add("E : Preparation of Tool/Die/Jigs/Fixture")
        cboPurpose.Items.Add("F : Testing / Trial")
        cboPurpose.Items.Add("G : Trolley / Bins")
        cboPurpose.Items.Add("H : FOC - Under Warranty / Re-Repair")
        cboPurpose.Items.Add("I : Fitting into any M/c coming to the company")
        cboPurpose.SelectedIndex = -1

        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdBarCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBarCode.Click
        On Error GoTo ErrPart
        Dim mCustomerCode As String = ""

        If MainClass.ValidateWithMasterTable((txtSuppName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = MasterNo
        End If
        '
        '    If InStr(1, pBARCODEFORMAT1, mCustomerCode, vbTextCompare) >= 1 Then
        '         ''HERO HONDA BARCODE.........
        '        Call PrintBarcode1
        '        Exit Sub
        '    End If
        '
        '    If InStr(1, pBARCODEFORMAT2, mCustomerCode, vbTextCompare) >= 1 Then
        '        ''TVS BARCODE.........
        '        Call PrintBarcode2
        '        Exit Sub
        '    End If

        '    If InStr(1, pBARCODEFORMAT3, mCustomerCode, vbTextCompare) >= 1 Then
        ''HEMA BARCODE.........
        Call PrintBarcode3(mCustomerCode)
        Exit Sub
        '    End If

        Exit Sub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click

        On Error GoTo DelErrPart

        Dim mItemCode As String
        Dim xGatePasstype As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPRINTED As String

        SqlStr = "SELECT PRINTED FROM INV_GATEPASS_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_PASSNO =" & Val(txtGatepassno.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPRINTED = IIf(IsDBNull(RsTemp.Fields("PRINTED").Value), "N", RsTemp.Fields("PRINTED").Value)
            If mPRINTED = "Y" Then
                MsgInformation("Gatepass Print Already taken so that you cann't be Deleted.")
                Exit Sub
            End If
        End If

        If ValidateBranchLocking((txtGatePassDate.Text)) = True Then
            Exit Sub
        End If

        If ValidateBookLocking(PubDBCn, CInt(ConLockGatePass), txtGatePassDate.Text) = True Then
            Exit Sub
        End If

        If Trim(txtChallanno.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub


        If MaterialRecdAgtRGP() = True Then
            MsgInformation("Material Recieved Against This RGP, So Cann't be Deleted")
            Exit Sub
        End If

        '    If chkissue.Value = vbChecked Then
        '        MsgInformation "Issue Completed, Cann't be Deleted"
        '        Exit Sub
        '    End If

        If Not RsReqMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_GATEPASS_HDR", (txtGatepassno.Text), RsReqMain, "AUTO_KEY_PASSNO", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "INV_GATEPASS_DET", (txtGatepassno.Text), RsReqDetail, "AUTO_KEY_PASSNO", "D") = False Then GoTo DelErrPart


                If InsertIntoDeleteTrn(PubDBCn, "INV_GATEPASS_HDR", "AUTO_KEY_PASSNO", (txtGatepassno.Text)) = False Then GoTo DelErrPart
                xGatePasstype = IIf(VB.Left(cboGatePasstype.Text, 3) = "NRG", "NRG", "RGP")

                If DeleteStockTRN(PubDBCn, xGatePasstype, (txtGatepassno.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("UPDATE INV_RGP_SLIP_HDR SET RGP_SLIP_STATUS='N' WHERE  AUTO_KEY_RGPSLIP=" & Val(txtRgpreqno.Text) & "")
                PubDBCn.Execute("DELETE FROM DSP_PAINT57F4_TRN WHERE MKey='" & Val(txtGatepassno.Text) & "' AND  BookSubType='O' AND TRNTYPE='N'")

                PubDBCn.Execute("DELETE FROM INV_RGP_REG_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REF_NO='" & Val(txtGatepassno.Text) & "'  AND BOOKTYPE='M' AND ITEM_IO='O'")

                PubDBCn.Execute("Delete from INV_GATEPASS_DET Where  AUTO_KEY_PASSNO=" & Val(txtGatepassno.Text) & "")
                PubDBCn.Execute("Delete from INV_GATEPASS_HDR Where AUTO_KEY_PASSNO=" & Val(txtGatepassno.Text) & "")

                PubDBCn.CommitTrans()
                RsReqMain.Requery() ''.Refresh
                RsReqDetail.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsReqMain.Requery() ''.Refresh
        RsReqDetail.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPRINTED As String

        '    If chkissue.Value = vbChecked Then
        '        MsgInformation "Issue Completed, Cann't be Modified"
        '        Exit Sub
        '    End If


        SqlStr = "SELECT PRINTED FROM INV_GATEPASS_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_PASSNO =" & Val(txtGatepassno.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPRINTED = IIf(IsDBNull(RsTemp.Fields("PRINTED").Value), "N", RsTemp.Fields("PRINTED").Value)
            If mPRINTED = "Y" Then
                MsgInformation("Gatepass Print Already taken so that you cann't be Modified.")
                Exit Sub
            End If
        End If

        If Trim(txteWayBillNo.Text) <> "" Then
            MsgInformation("EWay Bill Generated so that you cann't be Modified.")
            Exit Sub
        End If

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtGatepassno.Enabled = False
            cmdSearch.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonRgp_Nrgp(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonRgp_Nrgp(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cboTransmode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboTransmode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboVehicleType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboVehicleType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub ReportonRgp_Nrgp(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNO As String
        Dim Response As String
        Dim mReportPrint As Boolean
        Dim CntCount As Integer
        Dim mInvoicePrintType As String
        'Dim SqlStr As String = ""
        Dim mPRINTED As String = "N"
        Dim RsTemp As ADODB.Recordset = Nothing

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        '    If Mode = crptToPrinter Then
        SqlStr = "SELECT PRINTED FROM INV_GATEPASS_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_PASSNO =" & Val(txtGatepassno.Text) & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPRINTED = IIf(IsDBNull(RsTemp.Fields("PRINTED").Value), "N", RsTemp.Fields("PRINTED").Value)
            mPRINTED = IIf(PubSuperUser = "S", "N", mPRINTED)
        End If
        '    Else
        '        mPRINTED = "N"
        '    End If

        frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(4).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(5).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(3).CheckState = System.Windows.Forms.CheckState.Unchecked
        frmPrintInvCopy.chkPrintOption(2).Enabled = True
        frmPrintInvCopy.chkPrintOption(4).Enabled = True
        frmPrintInvCopy.chkPrintOption(5).Enabled = True
        frmPrintInvCopy.chkPrintOption(0).Enabled = True ' IIf(mPRINTED = "Y", False, True)

        frmPrintInvCopy.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If

        SqlStr = ""
        Report1.Reset()
        Call MainClass.ClearCRptFormulas(Report1)
        Call SelectQryForRgp_Nrgp(SqlStr)


        mTitle = "Delivery Challan"
        mSubTitle = "" '' " TIN No. " & IIf(IsNull(RsCompany!TINNO), "", RsCompany!TINNO)
        mRptFileName = "Rgp_Nrgp_GST.rpt"
        mInvoicePrintType = ""


        'For CntCount = 0 To 5
        '    If frmPrintInvCopy.chkPrintOption(CntCount).CheckState = System.Windows.Forms.CheckState.Checked Then
        '        mInvoicePrintType = UCase(frmPrintInvCopy.chkPrintOption(CntCount).Text)
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, mInvoicePrintType)
        '        '            Call ReportOnSales(crptToWindow, mInvoicePrintType, "N", mPrintOption)
        '    End If
        'Next

        If frmPrintInvCopy.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = "UPDATE INV_GATEPASS_HDR SET  PRINTED= 'Y', " & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_PASSNO =" & Val(txtGatepassno.Text) & ""

            PubDBCn.Execute(SqlStr)
        End If

        frmPrintInvCopy.Close()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default


        '    If (RsCompany.fields("COMPANY_CODE").value = 3 Or RsCompany.fields("COMPANY_CODE").value = 10) Then GoTo CretificatePrint
        '    If chkF4status.Value = vbUnchecked Then
        '        Call SelectQryForRgp_Nrgp(SqlStr)
        '
        '
        '        mTitle = "RGP-NRGP  Report"
        '        mSubTitle = " TIN No. " & IIf(IsNull(RsCompany!TINNO), "", RsCompany!TINNO)
        '        mRptFileName = "Rgp_Nrgp_GST.rpt"
        '
        '        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
        '
        '        If Left(cboGatePasstype.Text, 1) = "R" Or Left(cboGatePasstype.Text, 1) = "G" Then
        '            If MainClass.ValidateWithMasterTable(txtSuppcode.Text, "SUPP_CUST_CODE", "WITHIN_DISTT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '                If MasterNo = "N" Then
        '                    Response = MsgQuestion("Do You Want to Print RGP Certificate?")
        '                    If Response = vbYes Then
        '                        Call MainClass.ClearCRptFormulas(Report1)
        '                        Call SelectQryForRgp_Nrgp(SqlStr)
        '                        mTitle = IIf(RsCompany.fields("COMPANY_CODE").value = 1, "(AN ISO/ST/6949 : 2002 CERTIFIED COMPANY)", "")
        '                        mSubTitle = "HGST/CST NO. " & IIf(IsNull(RsCompany!LST_NO), "", RsCompany!LST_NO)
        '                        mSubTitle = mSubTitle & " TIN No. " & IIf(IsNull(RsCompany!TINNO), "", RsCompany!TINNO)
        '                        mRptFileName = "Rgp_Certi.rpt"
        '
        '                        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
        '                    End If
        '                End If
        '            End If
        '        End If
        '
        '    Else
        'CretificatePrint:
        '        frmPrintRGP_F4.Show 1
        '
        '        If G_PrintLedg = False Then
        '            Exit Sub
        '        End If
        '        mReportPrint = False
        '
        '        If frmPrintRGP_F4.chkPrintOption(0) = vbChecked Then
        '            Call SelectQryForRgp_Nrgp(SqlStr)
        '
        '            mTitle = "RGP-NRGP  Report"
        '            mSubTitle = " TIN No. " & IIf(IsNull(RsCompany!TINNO), "", RsCompany!TINNO)
        ''            mRptFileName = "Rgp_Nrgp.rpt"
        '
        '            mRptFileName = "Rgp_Nrgp_GST.rpt"
        '
        '
        '            Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
        '            mReportPrint = True
        '        End If
        '
        '        If mReportPrint = True And frmPrintRGP_F4.chkPrintOption(1) = vbChecked Then
        '            Response = MsgQuestion("Do You Want to Print F4 Challan?")
        '            If Response = vbNo Then GoTo NextRecd
        '        End If
        '
        '        If frmPrintRGP_F4.chkPrintOption(1) = vbChecked Then
        '            Call MainClass.ClearCRptFormulas(Report1)
        '
        '            Call SelectQryForRgp_Nrgp(SqlStr)
        '            mTitle = "TIN No. : " & IIf(IsNull(RsCompany!TINNO), "", RsCompany!TINNO) & ", CE Regn No : " & IIf(IsNull(RsCompany!CENT_EXC_RGN_NO), "", RsCompany!CENT_EXC_RGN_NO)
        '            mSubTitle = IIf(IsNull(RsCompany!COMPANY_CITY), "", RsCompany!COMPANY_CITY)
        '            mRptFileName = "F4Outward.rpt"
        '
        '            Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
        '            mReportPrint = True
        '        End If
        'NextRecd:
        '        If mReportPrint = True And frmPrintRGP_F4.chkPrintOption(2) = vbChecked Then
        '            Response = MsgQuestion("Do You Want to Print RGP Certificate?")
        '            If Response = vbNo Then GoTo NextRecd1
        '        End If
        '
        '        If frmPrintRGP_F4.chkPrintOption(2) = vbChecked Then
        '            If Left(cboGatePasstype.Text, 1) = "R" Or Left(cboGatePasstype.Text, 1) = "G" Then
        '                If MainClass.ValidateWithMasterTable(txtSuppcode.Text, "SUPP_CUST_CODE", "WITHIN_DISTT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '                    If MasterNo = "N" Then
        '                        Call MainClass.ClearCRptFormulas(Report1)
        '                        Call SelectQryForRgp_Nrgp(SqlStr)
        '                        mTitle = IIf(RsCompany.fields("COMPANY_CODE").value = 1, "(AN ISO/ST/6949 : 2002 CERTIFIED COMPANY)", "")
        '                        mSubTitle = "HGST/CST NO. " & IIf(IsNull(RsCompany!LST_NO), "", RsCompany!LST_NO)
        '                        mSubTitle = mSubTitle & " TIN No. " & IIf(IsNull(RsCompany!TINNO), "", RsCompany!TINNO)
        '                        mRptFileName = "Rgp_Certi.rpt"
        '
        '                        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
        '                    End If
        '                End If
        '            End If
        '        End If
        '
        'NextRecd1:
        '        If mReportPrint = True And frmPrintRGP_F4.chkPrintOption(3) = vbChecked Then
        '            Response = MsgQuestion("Do You Want to Print RGP Consumption Detail?")
        '            If Response = vbNo Then GoTo NextRecd4
        '        End If
        '
        '        If frmPrintRGP_F4.chkPrintOption(3) = vbChecked Then
        '            Call MainClass.ClearCRptFormulas(Report1)
        '            Call SelectQryForConsumption(SqlStr)
        '            mTitle = "Material Consumption Detail."
        '            mSubTitle = "Gate Pass No . " & Val(txtGatepassno.Text) & " and Dt. : " & VB6.Format(txtGatePassDate.Text, "DD/MM/YYYY")
        '            mRptFileName = "RgpConsumption.rpt"
        '
        '            Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
        '
        '        End If
        '
        'NextRecd4:
        '        If mReportPrint = True And frmPrintRGP_F4.chkPrintOption(4) = vbChecked Then
        '            Response = MsgQuestion("Do You Want to Print Form JJ?")
        '            If Response = vbNo Then GoTo NextRecd2
        '        End If
        '
        '        If frmPrintRGP_F4.chkPrintOption(4) = vbChecked Then
        '            Call MainClass.ClearCRptFormulas(Report1)
        '            Call SelectQryForRgp_Nrgp(SqlStr)
        '            mTitle = IIf(RsCompany.fields("COMPANY_CODE").value = 1, "(AN ISO/ST/6949 : 2002 CERTIFIED COMPANY)", "")
        '            mSubTitle = "HGST/CST NO. " & IIf(IsNull(RsCompany!LST_NO), "", RsCompany!LST_NO)
        '            mSubTitle = mSubTitle & " TIN No. " & IIf(IsNull(RsCompany!TINNO), "", RsCompany!TINNO)
        '            mRptFileName = "Rgp_Certi_JJ.rpt"
        '
        '            Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
        '        End If
        '
        'NextRecd2:
        '        Unload frmPrintRGP_F4
        '
        '    End If

        Exit Sub
ERR1:
        frmPrintInvCopy.Close()
        frmPrintRGP_F4.Close()
        MsgInformation(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtGatepassno_Validating(txtGatepassno, New System.ComponentModel.CancelEventArgs(False))
            If cmdAdd.Enabled = True Then cmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = " SELECT A.AUTO_KEY_PASSNO,A.GATEPASS_DATE,A.SUPP_CUST_CODE,B.SUPP_CUST_NAME " & vbCrLf & " From INV_GATEPASS_HDR A,FIN_SUPP_CUST_MST B WHERE " & vbCrLf & " a.SUPP_CUST_CODE = b.SUPP_CUST_CODE AND " & vbCrLf & " A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value

        SqlStr = SqlStr & vbCrLf & " AND A.GATEPASS_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " Order by a.AUTO_KEY_PASSNO "

        If MainClass.SearchGridMasterBySQL2((txtGatepassno.Text), SqlStr) = True Then
            txtGatepassno.Text = AcName
            txtGatepassno_Validating(txtGatepassno, New System.ComponentModel.CancelEventArgs(False))
            If txtGatepassno.Enabled = True Then txtGatepassno.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdSearchauth_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchauth.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND RGP_AUTH='Y'"

        If MainClass.SearchGridMaster((txtAuthority.Text), "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , SqlStr) = True Then
            txtAuthority.Text = AcName
            txtAuthorityName.Text = AcName1
            txtAuthority_Validating(txtAuthority, New System.ComponentModel.CancelEventArgs(False))
            If txtAuthority.Enabled = True Then txtAuthority.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub

    Private Sub cmdSearchRgp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchRgp.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = " SELECT B.SUPP_CUST_NAME,A.AUTO_KEY_RGPSLIP,A.RGP_SLIP_DATE,A.SUPP_CUST_CODE " & vbCrLf _
            & " From INV_RGP_SLIP_HDR A,FIN_SUPP_CUST_MST B WHERE " & vbCrLf _
            & " a.COMPANY_CODE = b.COMPANY_CODE AND a.SUPP_CUST_CODE = b.SUPP_CUST_CODE AND " & vbCrLf _
            & " RGP_SLIP_STATUS = 'N' AND " & vbCrLf _
            & " A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value

        SqlStr = SqlStr & vbCrLf & " AND GATEPASS_TYPE='" & lblBookType.Text & "'"

        If lblBookType.Text = "N" Then
            SqlStr = SqlStr & vbCrLf & " AND NRGP_APPROVED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND NRGP_APPROVED = CASE WHEN PURPOSE ='G' OR PURPOSE ='B' OR INTER_UNIT='Y' THEN NRGP_APPROVED ELSE 'Y' END "
        End If

        If MainClass.SearchGridMasterBySQL2((txtRgpreqno.Text), SqlStr) = True Then
            txtRgpreqno.Text = AcName1
            txtRgpreqno_Validating(txtRgpreqno, New System.ComponentModel.CancelEventArgs(False))
            If txtRgpreqno.Enabled = True Then txtRgpreqno.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdServProvided_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdServProvided.Click
        Call SearchProvidedMaster()
    End Sub

    Private Sub frmGatePassGST_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        'Dim xIName As String
        Dim SqlStr As String = ""
        Dim xICode As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pMainItemCode As String
        Dim mBookType As String
        Dim mInConUnit As Double
        Dim mOutConUnit As Double
        Dim mIsManyIn As Boolean
        Dim mLotNo As String
        Dim mUOM As String
        Dim mStockType As String

        If eventArgs.row = 0 And eventArgs.col = ColF4No Then
            With SprdMain
                eventArgs.row = .ActiveRow
                eventArgs.col = ColItemCode
                xICode = Trim(.Text)
                If xICode = "" Then Exit Sub

                '            If chkPaintF4.Value = vbChecked Then
                '                 pMainItemCode = "('" & xICode & "')"
                '                 mIsManyIn = False
                '            Else

                pMainItemCode = GetInJobworkItem(xICode, Trim(txtGatePassDate.Text), mInConUnit, mIsManyIn)

                If pMainItemCode = "" Then
                    pMainItemCode = "('" & xICode & "')"
                Else
                    pMainItemCode = "('" & xICode & "'," & pMainItemCode & ")"
                End If

                mOutConUnit = 1
                '            End If

                mBookType = "P" '' IIf(chkPaintF4.Value = vbChecked, "G", "P")

                '            If mIsManyIn = False Then
                SqlStr = " SELECT PARTY_F4NO, TO_CHAR(SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY)) AS BALQTY " & vbCrLf & " FROM DSP_PAINT57F4_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE IN " & pMainItemCode & " " & vbCrLf & " AND SUPP_CUST_CODE='" & Trim(txtSuppcode.Text) & "' AND BOOKTYPE<>'" & mBookType & "' " & vbCrLf & " AND ISSCRAP='N' "

                If Val(txtGatepassno.Text) <> 0 Then
                    SqlStr = SqlStr & " AND BILL_NO<>'" & MainClass.AllowSingleQuote(txtGatepassno.Text) & "'"
                End If

                SqlStr = SqlStr & vbCrLf & " GROUP BY PARTY_F4NO " & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY)>0"

                eventArgs.col = ColF4No
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    eventArgs.row = .ActiveRow
                    eventArgs.col = ColF4No
                    .Text = AcName
                End If
                '            End If
            End With
        End If
        If eventArgs.row = 0 And eventArgs.col = ColHeatNo Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                xICode = Trim(.Text)

                .Col = ColHeatNo
                mLotNo = Trim(.Text)

                .Col = ColUOM
                mUOM = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                SqlStr = GetItemHeatWiseQry(xICode, (txtGatePassDate.Text), mUOM, "STR", mStockType, mLotNo, ConWH, "DSP", Val(txtGatepassno.Text))
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColHeatNo
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColHeatNo)
            End With
        End If
        If eventArgs.row = 0 And eventArgs.col = ColBatchNo Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                xICode = Trim(.Text)

                .Col = ColBatchNo
                mLotNo = Trim(.Text)

                .Col = ColUOM
                mUOM = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                SqlStr = GetItemLotWiseQry(xICode, (txtGatePassDate.Text), mUOM, "STR", mStockType, mLotNo, ConWH, "DSP", Val(txtGatepassno.Text))
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColBatchNo
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColBatchNo)
            End With
        End If

        '    If eventArgs.Col = 0 And eventArgs.Row > 0 Then    '***ROW DEL. OPTION NOT REQ IN INVOICE
        '        SprdMain.Row=eventArgs.Row
        '        SprdMain.Col = ColItemCode
        '        If eventArgs.Row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
        '            MainClass.DeleteSprdRow SprdMain, Row, ColItemCode
        '            MainClass.SaveStatus Me, ADDMode, MODIFYMode
        '            FormatSprdMain Row
        '        End If
        '    End If

    End Sub



    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent

        Dim mActiveCol As Integer

        mActiveCol = SprdMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColQty Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColQty
                If Val(SprdMain.Text) <> 0 Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        '                    FormatSprdMain SprdMain.MaxRows
                    End If
                End If
                '            SprdMain.Row = SprdMain.MaxRows
            End If
        ElseIf eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then
            If mActiveCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
            If mActiveCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))

        End If
        eventArgs.keyCode = 9999
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mBalQty As Double
        Dim mIssueQty As Double

        Dim xICode As String
        Dim xIUOM As String
        Dim xMRRNo As Double
        Dim xRefNo As String
        Dim mQty As Double
        Dim mStockType As String = ""
        Dim pMainItemCode As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mInConUnit As Double
        Dim mOutConUnit As Double
        Dim mIsManyIn As Boolean

        If eventArgs.newRow = -1 Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow

        Select Case eventArgs.col
            '        Case ColItemCode
            '            SprdMain.Col = ColItemCode
            '            Call FillItemDesc(SprdMain.Text, True)
            '            If DuplicateItem = False Then
            '                FormatSprdMain -1
            '                MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColStockType
            '            End If
            '
            '        Case ColItemName
            '            SprdMain.Col = ColItemCode
            '            Call FillItemDesc(SprdMain.Text, False)
            '            If DuplicateItem = False Then
            '            End If
            Case ColQty
                If CheckQty() = True Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColQty
                    If Val(SprdMain.Text) <> 0 Then
                        If SprdMain.MaxRows = SprdMain.ActiveRow Then
                            MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                            '                        FormatSprdMain SprdMain.MaxRows
                            FormatSprdMain(-1)
                        End If
                    End If
                End If

                '        Case ColStockType
                '            SprdMain.Row = SprdMain.ActiveRow
                '            SprdMain.Col = ColStockType
                '            If Trim(SprdMain.Text) = "" Then Exit Sub
                '
                '            If MainClass.ValidateWithMasterTable(SprdMain.Text, "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.fields("COMPANY_CODE").value & "") = False Then
                '                MsgInformation "InValid Stock Type"
                '                MainClass.SetFocusToCell SprdMain, Row, ColStockType
                '            End If
            Case ColF4No

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                SprdMain.Col = ColF4No
                xRefNo = Trim(SprdMain.Text)
                If xRefNo = "" Then Exit Sub

                If pMainItemCode = "" Then
                    pMainItemCode = "('" & xICode & "')"
                Else
                    pMainItemCode = "('" & xICode & "'," & pMainItemCode & ")"
                End If


                mOutConUnit = 1
                If mIsManyIn = False Then
                    If FillREFDetail(pMainItemCode, xRefNo) = False Then Exit Sub
                End If
        End Select
        Call CalcTots()
        '    FormatSprdMain -1
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function FillREFDetail(ByRef pItemCode As String, ByRef pRefNo As String) As Boolean

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If Val(pRefNo) = 0 Then Exit Function

        SqlStr = " SELECT SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY) AS ITEMQTY " & vbCrLf & " FROM DSP_PAINT57F4_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRIM(PARTY_F4NO)='" & MainClass.AllowSingleQuote(Trim(pRefNo)) & "'" & vbCrLf & " AND ITEM_CODE IN " & pItemCode & " AND ISSCRAP='N'"

        If Trim(txtGatepassno.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND BILL_NO<>'" & MainClass.AllowSingleQuote(txtGatepassno.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY)>0"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            '         SprdMain.Row = SprdMain.ActiveRow
            '         With RsTemp
            '            SprdMain.Col = Col57BalQty
            '            SprdMain.Text = Val(IIf(IsNull(!ITEMQTY), "", !ITEMQTY))
            '         End With
            FillREFDetail = True
        Else
            MsgInformation("Either Invalid 57F4 No or Invalid Item Code for This Item")
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColF4No)
            FillREFDetail = False
        End If

        Exit Function
ERR1:
        FillREFDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function DuplicateItem(ByRef pCol As Integer) As Boolean

        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mItemCode As String
        Dim mLotNo As String
        Dim mCheckLotNo As String = ""

        Dim mCheckHeatNo As String = ""
        Dim mHeatNo As String = ""

        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            mCheckItemCode = Trim(UCase(.Text))

            .Col = ColLotNo
            mCheckLotNo = Trim(UCase(.Text))

            .Col = ColHeatNo
            mCheckHeatNo = Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                .Col = ColLotNo
                mLotNo = Trim(UCase(.Text))

                .Col = ColHeatNo
                mHeatNo = Trim(UCase(.Text))

                If (mItemCode & ":" & mLotNo & ":" & mHeatNo = mCheckItemCode & ":" & mCheckLotNo & ":" & mCheckHeatNo And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                '            If (mItemCode = mCheckItemCode And mCheckItemCode <> "") Then
                '                mCount = mCount + 1
                '            End If

                If mCount > 1 Then
                    DuplicateItem = True
                    MsgInformation("Duplicate Item : " & mCheckItemCode)
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, pCol)
                    Exit Function
                End If
            Next
        End With
    End Function
    Private Function CheckQty() As Boolean

        On Error GoTo ERR1
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColQty
            If Val(.Text) > 0 Then
                CheckQty = True
            Else
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColQty)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function GetItemRateFromCustMst(ByRef pItemCode As String, ByRef pItemUOM As String) As Double

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFactor As Double

        GetItemRateFromCustMst = 0
        SqlStr = " SELECT ID.ITEM_RATE, INVMST.PURCHASE_UOM, INVMST.UOM_FACTOR FROM " & vbCrLf & " FIN_SUPP_CUST_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND ID.SUPP_CUST_CODE='" & Trim(txtSuppcode.Text) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            If pItemUOM = RsTemp.Fields("PURCHASE_UOM").Value Then
                GetItemRateFromCustMst = IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)
            Else
                GetItemRateFromCustMst = IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)
                mFactor = IIf(IsDBNull(RsTemp.Fields("UOM_FACTOR").Value), 0, RsTemp.Fields("UOM_FACTOR").Value)
                GetItemRateFromCustMst = GetItemRateFromCustMst / IIf(mFactor = 0, 1, mFactor)
            End If
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        GetItemRateFromCustMst = 0
    End Function

    Private Sub FillItemDesc(ByRef pItemCode As String, ByRef IsItemCode As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(pItemCode) = "" Then Exit Sub
        With SprdMain
            SqlStr = "SELECT ITEM_CODE, ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            If IsItemCode = True Then
                SqlStr = SqlStr & vbCrLf & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND LTRIM(RTRIM(ITEM_SHORT_DESC))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                .Row = .ActiveRow
                .Col = ColItemCode
                .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                .Col = ColItemName
                .Text = IIf(IsDBNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)

                .Col = ColUOM
                .Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

            Else
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColQty)
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub



    Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent)

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mGatepassno As String

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        mGatepassno = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(0))

        txtGatepassno.Text = mGatepassno
        txtGatepassno_Validating(txtGatepassno, New System.ComponentModel.CancelEventArgs(False))
        If txtChallanno.Enabled = True Then txtChallanno.Focus()
        CmdView_Click(CmdView, New System.EventArgs())

    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
        'With SprdView
        '    .Row = eventArgs.row
        '    .Col = 1
        '    txtGatepassno.Text = .Text
        '    txtGatepassno_Validating(txtGatepassno, New System.ComponentModel.CancelEventArgs(False))
        '    If txtChallanno.Enabled = True Then txtChallanno.Focus()
        '    CmdView_Click(CmdView, New System.EventArgs())
        'End With
    End Sub
    Private Function AutoGenSeqNo(ByRef mDivisionCode As Double, ByRef pAgtPermission As String) As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsMRRMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mStartingNo As Double
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mStartingSNo As Double
        Dim mSeparateSeries As String
        Dim mMaxValue As String

        SqlStr = ""
        mStartingSNo = 1

        SqlStr = "SELECT Max(AUTO_KEY_PASSNO)  " & vbCrLf _
            & " FROM INV_GATEPASS_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_PASSNO,LENGTH(AUTO_KEY_PASSNO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND Agt_Permission='" & pAgtPermission & "'"

        SqlStr = SqlStr & vbCrLf & " AND GATEPASS_TYPE='" & lblBookType.Text & "'"

        If lblBookType.Text = "R" Then        ''If cboGatePasstype.Text = "RGP" Then
            mStartingNo = 1
        ElseIf lblBookType.Text = "N" Then
            mStartingNo = 50001
        Else
            mStartingNo = 60001
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMRRMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = mStartingNo
                End If
            Else
                mNewSeqNo = mStartingNo
            End If
        End With
        AutoGenSeqNo = mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim mReqnum As String = ""
        Dim SqlStr As String = ""
        Dim mVNoSeq As Double
        Dim mChallanNo As Double

        Dim mStatus As String
        Dim mGatepasstype As String
        Dim mMatfrom As String
        Dim mScrap As String
        Dim mDivisionCode As Double
        Dim pAgtPermission As String
        Dim mGSTAPP As String
        Dim mSACCode As String
        Dim mTransMode As String
        Dim mVehicleType As String
        Dim mPartyGSTNo As String
        Dim mSameGSTNo As String


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If MODIFYMode = True Then
            If InsertIntoDelAudit(PubDBCn, "INV_GATEPASS_HDR", (txtGatepassno.Text), RsReqMain, "AUTO_KEY_PASSNO", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "INV_GATEPASS_DET", (txtGatepassno.Text), RsReqDetail, "AUTO_KEY_PASSNO", "M") = False Then GoTo ErrPart
        End If

        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtSuppcode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")
        mSameGSTNo = IIf(mPartyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "Y", "N")

        pAgtPermission = "N" ''IIf(chkAgtPermission.Value = vbChecked, "Y", "N")
        pAgtPermission = IIf(cboGatePasstype.Text = "NRGP", "N", pAgtPermission)
        mScrap = "N" '' IIf(chkScrap.Value = vbChecked, "Y", "N")

        If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        If Val(txtGatepassno.Text) = 0 Then
            mVNoSeq = CDbl(AutoGenSeqNo(mDivisionCode, pAgtPermission))
        Else
            mVNoSeq = Val(txtGatepassno.Text)
        End If

        'If RsCompany.Fields("FYEAR").Value > 2022 Then
        '    txtPrefix.Text = GetDocumentPrefix("J", IIf(mSameGSTNo = "Y", "C", lblBookType.Text))
        'End If

        If Val(txtChallanno.Text) = 0 Then
            mChallanNo = AutoChallanNo()        ''mVNoSeq ''
        Else
            mChallanNo = Val(txtChallanno.Text)
        End If

        'mChallanNo = VB6.Format(Val(CStr(mChallanNo)), ConBillFormat)

        txtChallanno.Text = CStr(Val(CStr(mChallanNo)))
        txtGatepassno.Text = CStr(Val(CStr(mVNoSeq)))



        If cboStatus.Text = "Pending" Then
            mStatus = "N"
        ElseIf cboStatus.Text = "Completed" Then
            mStatus = "Y"
        Else
            mStatus = "C"
        End If

        '      mStatus = "Y"

        If cboMaterial.Text = "STORES" Then
            mMatfrom = "INV"
        Else
            mMatfrom = "PRD"
        End If

        If cboGatePasstype.Text = "RGP" Then
            mGatepasstype = "R"
        ElseIf cboGatePasstype.Text = "NRGP" Then
            mGatepasstype = "N"
        Else
            mGatepasstype = "G"
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
            mSACCode = Trim(MasterNo)
        Else
            mSACCode = ""
        End If

        mGSTAPP = VB.Left(cboGSTStatus.Text, 1)

        mTransMode = VB.Left(cboTransmode.Text, 1)
        mVehicleType = VB.Left(cboVehicleType.Text, 1)


        SqlStr = ""
        If ADDMode = True Then
            lblMKey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO INV_GATEPASS_HDR (" & vbCrLf _
                & " AUTO_KEY_PASSNO, COMPANY_CODE, " & vbCrLf _
                & " GATEPASS_DATE, GATEPASS_TYPE, " & vbCrLf _
                & " SUPP_CUST_CODE, REQ_NO, REQ_DATE, REMOVAL_TIME, " & vbCrLf _
                & " AUTH_EMP_CODE,PRE_EMP_CODE,INPUT_FROM_FLAG,REMARKS, " & vbCrLf _
                & " GATEPASS_STATUS,GATEPASS_NO,OUTWARD_57F4NO,VEHICLE_NO," & vbCrLf _
                & " INWARD_ITEM_CODE, INWARD_ITEM_QTY, ST_38_NO, " & vbCrLf _
                & " EXP_RTN_DATE, IS_SCRAP, " & vbCrLf _
                & " ADDUSER,ADDDATE,MODUSER,MODDATE, " & vbCrLf _
                & " DIV_CODE,PURPOSE, AGT_Permission, " & vbCrLf _
                & " GST_APP, SAC_CODE,BILL_TO_LOC_ID,CHALLAN_PREFIX, TRANSPORT_MODE, VEHICLE_TYPE,GRNO, GRDATE, CARRIERS, TRANSPORTER_GSTNO,TRANS_DISTANCE)"

            SqlStr = SqlStr & vbCrLf & " VALUES( " & vbCrLf _
                & " " & Val(CStr(mVNoSeq)) & ", " & RsCompany.Fields("Company_Code").Value & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtGatePassDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & mGatepasstype & "', '" & MainClass.AllowSingleQuote((txtSuppcode.Text)) & "'," & vbCrLf _
                & " " & Val(txtRgpreqno.Text) & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtRgpreqdate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtRemoval.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtAuthority.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtEmp.Text)) & "', " & vbCrLf _
                & " '" & mMatfrom & "', '" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', " & vbCrLf _
                & " '" & mStatus & "'," & Val(txtChallanno.Text) & "," & vbCrLf _
                & "  " & Val(txtF4no.Text) & ", '" & MainClass.AllowSingleQuote((txtVehicle.Text)) & "', " & vbCrLf _
                & " '', 0, " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtST38No.Text)) & "', TO_DATE('" & VB6.Format(txtReturnDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & mScrap & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'',''," & vbCrLf _
                & " " & mDivisionCode & ",'" & VB.Left(cboPurpose.Text, 1) & "','" & pAgtPermission & "'," & vbCrLf & " '" & mGSTAPP & "', " & vbCrLf _
                & " '" & mSACCode & "','" & MainClass.AllowSingleQuote(txtBillTo.Text) & "','" & txtPrefix.Text & "','" & mTransMode & "','" & mVehicleType & "','" & MainClass.AllowSingleQuote(TxtGRNo.Text) & "',TO_DATE('" & VB6.Format(TxtGRDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtCarriers.Text) & "','" & MainClass.AllowSingleQuote(txtTransportCode.Text) & "'," & Val(txtDistance.Text) & ")"

        ElseIf MODIFYMode = True Then

            SqlStr = ""
            SqlStr = "UPDATE INV_GATEPASS_HDR SET TRANS_DISTANCE=" & Val(txtDistance.Text) & "," & vbCrLf _
                & " GATEPASS_DATE=TO_DATE('" & VB6.Format(txtGatePassDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtSuppcode.Text)) & "',GATEPASS_NO= " & Val(txtChallanno.Text) & "," & vbCrLf _
                & " GRNO='" & MainClass.AllowSingleQuote(TxtGRNo.Text) & "', GRDATE=TO_DATE('" & VB6.Format(TxtGRDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY'), CARRIERS='" & MainClass.AllowSingleQuote(txtCarriers.Text) & "', TRANSPORTER_GSTNO='" & MainClass.AllowSingleQuote(txtTransportCode.Text) & "', " & vbCrLf _
                & " VEHICLE_NO='" & txtVehicle.Text & "',GATEPASS_TYPE='" & mGatepasstype & "', " & vbCrLf _
                & " REMOVAL_TIME ='" & txtRemoval.Text & "', CHALLAN_PREFIX='" & txtPrefix.Text & "'," & vbCrLf _
                & " AUTH_EMP_CODE ='" & txtAuthority.Text & "'," & vbCrLf _
                & " PRE_EMP_CODE ='" & txtEmp.Text & "'," & vbCrLf _
                & " TRANSPORT_MODE='" & mTransMode & "'," & vbCrLf _
                & " VEHICLE_TYPE='" & mVehicleType & "'," & vbCrLf _
                & " BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote((txtBillTo.Text)) & "', " & vbCrLf _
                & " INPUT_FROM_FLAG ='" & mMatfrom & "'," & vbCrLf & " REMARKS ='" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', " & vbCrLf & " GATEPASS_STATUS ='" & mStatus & "'," & vbCrLf & " OUTWARD_57F4NO =" & Val(txtF4no.Text) & "," & vbCrLf _
                & " ST_38_NO ='" & MainClass.AllowSingleQuote((txtST38No.Text)) & "'," & vbCrLf & " EXP_RTN_DATE=TO_DATE('" & VB6.Format(txtReturnDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " IS_SCRAP='" & mScrap & "', AGT_Permission='" & pAgtPermission & "'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf & " DIV_CODE = " & mDivisionCode & ", " & vbCrLf & " PURPOSE='" & VB.Left(cboPurpose.Text, 1) & "', " & vbCrLf & " GST_APP = '" & mGSTAPP & "'," & vbCrLf & " SAC_CODE= '" & mSACCode & "'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND AUTO_KEY_PASSNO =" & Val(lblMKey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1(CStr(Val(CStr(mVNoSeq))), mDivisionCode) = False Then GoTo ErrPart


        '    If ADDMode = True Then
        SqlStr = ""
        SqlStr = " Update INV_RGP_SLIP_HDR SET " & vbCrLf & " RGP_SLIP_STATUS = 'Y' WHERE " & vbCrLf & " AUTO_KEY_RGPSLIP =" & Val(txtRgpreqno.Text) & " AND RGP_SLIP_STATUS='N'"

        PubDBCn.Execute(SqlStr)
        '    End If


        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsReqMain.Requery() ''.Refresh
        RsReqDetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function

        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

        ''Resume
    End Function
    Private Function UpdateDetail1(ByRef pVnoseq As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mSubRowNo As Integer

        Dim mItemCode As String
        Dim mUOM As String = ""
        Dim mStockType As String = ""
        Dim mQty As Double
        Dim mRemarks As String
        Dim mReturnQty As Double
        Dim mF4No As String
        Dim mRate As Double
        Dim pPartyF4Date As String = ""
        Dim pOurVDate As String = ""
        Dim pBookType As String
        Dim pTRNType As String
        Dim mLotNo As String
        Dim mHeatNo As String
        Dim mBatchNo As String
        Dim mJobOrderNo As Double
        Dim mHSNCode As String
        Dim mAmount As Double
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mIncomingItemCode As String
        Dim mQtyKgs As Double
        Dim mItemDesc As String

        SqlStr = " Delete From INV_GATEPASS_DET " & vbCrLf & " WHERE AUTO_KEY_PASSNO=" & Val(lblMKey.Text) & ""
        PubDBCn.Execute(SqlStr)

        If DeleteStockTRN(PubDBCn, IIf(VB.Left(cboGatePasstype.Text, 3) = "NRG", ConStockRefType_NRG, ConStockRefType_RGP), (txtGatepassno.Text)) = False Then GoTo UpdateDetail1Err

        pBookType = "D" '' IIf(chkPaintF4.Value = vbChecked, "P", "D")
        pTRNType = "N" '' IIf(chkPaintF4.Value = vbChecked, "P", "N")



        'AND BookType='" & pBookType & "'
        PubDBCn.Execute("DELETE FROM DSP_PAINT57F4_TRN WHERE MKey='" & lblMKey.Text & "'  AND BookSubType='O' AND TRNTYPE='" & pTRNType & "'")

        '    PubDBCn.Execute "DELETE FROM DSP_OUTWARD57F4_TRN WHERE MKey='" & LblMkey.text & "' AND ITEM_IO='O'"

        PubDBCn.Execute("DELETE FROM INV_RGP_REG_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REF_NO='" & lblMKey.Text & "'  AND BOOKTYPE='M' AND ITEM_IO='O'")

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUOM
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColHSNCode
                mHSNCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColLotNo
                mLotNo = Trim(.Text)

                .Col = ColHeatNo
                mHeatNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColBatchNo
                mBatchNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColStockType
                mStockType = MainClass.AllowSingleQuote(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColQtyKGs
                mQtyKgs = Val(.Text)

                .Col = ColReturnQty
                mReturnQty = Val(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColAmount
                mAmount = Val(.Text)

                .Col = ColCGSTPer
                mCGSTPer = Val(.Text)

                .Col = ColCGSTAmount
                mCGSTAmount = Val(.Text)

                .Col = ColSGSTPer
                mSGSTPer = Val(.Text)

                .Col = ColSGSTAmount
                mSGSTAmount = Val(.Text)

                .Col = ColIGSTPer
                mIGSTPer = Val(.Text)

                .Col = ColIGSTAmount
                mIGSTAmount = Val(.Text)

                .Col = ColF4No
                mF4No = CStr(Val(.Text))

                .Col = colRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemDesc
                mItemDesc = Val(.Text)

                .Col = ColJobOrderNo
                mJobOrderNo = Val(.Text)

                .Col = ColIncomingItemCode
                mIncomingItemCode = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If mItemCode <> "" And mQty > 0 Then
                    mSubRowNo = mSubRowNo + 1
                    SqlStr = " INSERT INTO INV_GATEPASS_DET (AUTO_KEY_PASSNO, SERIAL_NO, ITEM_CODE,ITEM_UOM," & vbCrLf _
                            & " REMARKS, STOCK_TYPE," & vbCrLf _
                            & " ITEM_QTY, ITEM_QTY_KGS,RTN_QTY, GATEPASS_NO, F4NO, " & vbCrLf _
                            & " ITEM_RATE, COMPANY_CODE, LOT_NO, AUTO_KEY_WO,HSN_CODE, AMOUNT,CGST_PER, " & vbCrLf _
                            & " CGST_AMOUNT, SGST_PER, SGST_AMOUNT,IGST_PER, IGST_AMOUNT,HEAT_NO,BATCH_NO,INWARD_ITEM_CODE)" & vbCrLf _
                            & " VALUES (" & Val(lblMKey.Text) & ", " & mSubRowNo & ",'" & mItemCode & "'," & vbCrLf _
                            & " '" & mUOM & "','" & mRemarks & "','" & mStockType & "'," & mQty & ", " & mQtyKgs & "," & vbCrLf _
                            & " " & mReturnQty & "," & txtChallanno.Text & "," & mF4No & "," & mRate & "," & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mLotNo & "'," & mJobOrderNo & "," & vbCrLf _
                            & " '" & mHSNCode & "', " & mAmount & "," & mCGSTPer & ", " & mCGSTAmount & "," & mSGSTPer & "," & vbCrLf _
                            & " " & mSGSTAmount & "," & mIGSTPer & ", " & mIGSTAmount & ",'" & mHeatNo & "','" & mBatchNo & "','" & mIncomingItemCode & "')"
                    PubDBCn.Execute(SqlStr)

                    If Val(mF4No) <> 0 Then
                        Call GetF4detailFromRGP(mF4No, (txtSuppcode.Text), pPartyF4Date, pOurVDate)

                        If UpdatePaintDetail(PubDBCn, (lblMKey.Text), "D", "O", (txtSuppcode.Text), mF4No, pPartyF4Date, (lblMKey.Text), (txtGatePassDate.Text), mItemCode, mQty, "O", mSubRowNo, pTRNType, pOurVDate, "N", "N", "N") = False Then GoTo UpdateDetail1Err

                        '                    If chkScrap.Value = vbChecked Then
                        '                        If UpdatePaintDetail(PubDBCn, lblMKey.text, IIf(chkPaintF4.Value = vbChecked, "P", "D"), "O", _
                        ''                                    txtSuppcode.Text, mF4No, pPartyF4Date, lblMKey.text, txtGatePassDate.Text, _
                        ''                                    mItemCode, mQty, "I", mSubRowNo, pTRNType, pOurVDate, IIf(chkPaintF4.Value = vbChecked, "Y", "N"), "Y") = False Then GoTo UpdateDetail1Err
                        '                    End If

                    End If

                    '                If chkF4status.Value = vbChecked Then
                    '                    If UpdateOutwardDetail(PubDBCn, LblMkey.text, _
                    ''                                txtSuppcode.Text, txtF4no.Text, VB6.Format(txtGatePassDate.Text, "DD/MM/YYYY"), _
                    ''                                LblMkey.text, txtGatePassDate.Text, _
                    ''                                mItemCode, Trim(txtItemcode.Text), mQty, "O", mSubRowNo) = False Then GoTo UpdateDetail1Err
                    '
                    ''                    If UpdatePaintDetail(PubDBCn, lblMKey.text, "D", "O", _
                    '                                txtSuppcode.Text, txtF4no.Tex, VB6.Format(txtGatePassDate.Text, "DD/MM/YYYY"), lblMKey.text, txtGatePassDate.Text, _
                    '                                mItemCode, mQty, "O", mSubRowNo, "J") = False Then GoTo UpdateDetail1Err
                    '
                    '                End If

                    If VB.Left(cboGatePasstype.Text, 3) = "RGP" Or VB.Left(cboGatePasstype.Text, 3) = "GAT" Then
                        If UpdateRGP_TRN(PubDBCn, CDbl(lblMKey.Text), VB6.Format(txtGatePassDate.Text, "DD/MM/YYYY"), CDbl(lblMKey.Text), VB6.Format(txtGatePassDate.Text, "DD/MM/YYYY"), (txtSuppcode.Text), Val(txtF4no.Text), VB6.Format(txtGatePassDate.Text, "DD/MM/YYYY"), (lblMKey.Text), (txtGatePassDate.Text), mItemCode, "", mQty, 0, "O", mSubRowNo, "M", (txtReturnDate.Text), txtBillTo.Text) = False Then GoTo UpdateDetail1Err
                    End If

                    If Mid(RsCompany.Fields("COMPANY_NAME").Value, 1, 3) = "KAY" And CDate(txtGatePassDate.Text) <= CDate("30/06/2022") Then
                    Else
                        If UpdateStockTRN(PubDBCn, IIf(VB.Left(cboGatePasstype.Text, 3) = "NRG", ConStockRefType_NRG, ConStockRefType_RGP), (txtGatepassno.Text), I, (txtGatePassDate.Text), (txtGatePassDate.Text), mStockType, mItemCode, mUOM, mLotNo, mQty, 0, "O", 0, 0, "", "", "STR", "STR", "", IIf(cboStatus.SelectedIndex = 2, "Y", "N"), "To : " & "(" & cboGatePasstype.Text & ")-" & txtSuppName.Text, (txtSuppcode.Text), ConWH, mDivisionCode, VB.Left(cboPurpose.Text, 1), "",, mHeatNo) = False Then GoTo UpdateDetail1Err
                    End If
                    ''mDeptCode, mDeptCode

                    '                If Left(cboGatePasstype.Text, 3) = "RGP" Then
                    '                    If UpdateStockTRN(PubDBCn, ConStockRefType_RGP, txtGatepassno.Text, I, txtGatePassDate.Text, txtGatePassDate.Text, _
                    ''                                mStockType, mItemCode, mUOM, -1, mQty, 0, "I", 0, 0, "", "", "STR", "STR", "", IIf(cboStatus.ListIndex = 2, "Y", "N"), "To : " & "(" & cboGatePasstype.Text & ")-" & txtSuppName.Text, txtSuppcode.Text, ConJW) = False Then GoTo UpdateDetail1Err
                    '                End If
                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function

    Private Sub GetF4detailFromRGP(ByRef pPartyF4No As String, ByRef pPartyCode As String, ByRef pPartyF4Date As String, ByRef pOurVDate As String)

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        pPartyF4Date = ""
        pOurVDate = ""

        mSqlStr = " SELECT PARTY_F4NO,PARTY_F4DATE, VDATE " & vbCrLf & " FROM DSP_PAINT57F4_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pPartyCode & "'" & vbCrLf & " AND PARTY_F4NO='" & pPartyF4No & "'"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pPartyF4Date = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PARTY_F4DATE").Value), "", RsTemp.Fields("PARTY_F4DATE").Value), "DD/MM/YYYY")
            pOurVDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDate").Value), "", RsTemp.Fields("VDate").Value), "DD/MM/YYYY")
        Else
            mSqlStr = " SELECT PARTY_F4NO,PARTY_F4DATE, VDATE " & vbCrLf & " FROM DSP_PAINT57F4_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pPartyCode & "'" & vbCrLf & " AND PARTY_F4NO='" & pPartyF4No & "'" & vbCrLf & " AND PARTY_F4DATE = (" & vbCrLf & " SELECT MAX(PARTY_F4DATE) " & vbCrLf & " FROM DSP_PAINT57F4_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pPartyCode & "'" & vbCrLf & " AND PARTY_F4NO='" & pPartyF4No & "')"


            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                pPartyF4Date = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PARTY_F4DATE").Value), "", RsTemp.Fields("PARTY_F4DATE").Value), "DD/MM/YYYY")
                pOurVDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDate").Value), "", RsTemp.Fields("VDate").Value), "DD/MM/YYYY")
            End If
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim pISGSTRegd As String
        Dim mHSNCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mRGPServiceCode As String = ""
        Dim mPOServiceCode As String
        Dim mPOChargeApp As String
        Dim mRGPChargeApp As String
        Dim cntRow As Integer

        FieldsVarification = True

        If CDate(txtGatePassDate.Text) < CDate(PubGSTApplicableDate) Then
            MsgInformation("Please Made the Gatepass in Old Format.")
            FieldsVarification = False
            Exit Function
        End If

        If ValidateBranchLocking((txtGatePassDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ValidateBookLocking(PubDBCn, CInt(ConLockGatePass), txtGatePassDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If


        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsReqMain.EOF = True Then Exit Function

        If MODIFYMode = True And txtGatepassno.Text = "" Then
            MsgInformation("Gate Pass No. Cann't Blank")
            FieldsVarification = False
            Exit Function
        End If


        If txtGatePassDate.Text = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtGatePassDate.Focus()
            Exit Function
        ElseIf FYChk((txtGatePassDate.Text)) = False Then
            FieldsVarification = False
            If txtGatePassDate.Enabled = True Then txtGatePassDate.Focus()
            Exit Function
        End If

        If txtReturnDate.Text = "" Then
            MsgBox("Return Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtReturnDate.Enabled = True Then txtReturnDate.Focus()
            Exit Function
        ElseIf Not IsDate(txtReturnDate.Text) Then
            FieldsVarification = False
            If txtReturnDate.Enabled = True Then txtReturnDate.Focus()
            Exit Function
        End If

        If CDate(txtGatePassDate.Text) < CDate(txtRgpreqdate.Text) Then
            MsgBox("Slip Date Cann't be Greater than GatePass Date", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtReturnDate.Enabled = True Then txtReturnDate.Focus()
            Exit Function
        End If

        If TxtGRDate.Text <> "" Then
            If FYChk((TxtGRDate.Text)) = False Then
                FieldsVarification = False
                TxtGRDate.Focus()
                Exit Function
            End If
        End If

        If VB.Left(cboGatePasstype.Text, 3) = "RGP" Or VB.Left(cboGatePasstype.Text, 3) = "GAT" Then
            If CDate(txtGatePassDate.Text) > CDate(txtReturnDate.Text) Then
                MsgBox("Return Date Cann't be Less than GatePass Date", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtReturnDate.Enabled = True Then txtReturnDate.Focus()
                Exit Function
            End If
        Else
            txtReturnDate.Text = VB6.Format(txtGatePassDate.Text, "DD/MM/YYYY")
        End If

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            FieldsVarification = False
            Exit Function
        End If

        mLocal = "N"
        'If MainClass.ValidateWithMasterTable(Trim(txtSuppcode.Text), "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mLocal = MasterNo
        'End If

        mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable(Trim(txtSuppcode.Text), "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If
        mLocal = GetPartyBusinessDetail(Trim(txtSuppcode.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtSuppcode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")


        If ADDMode = True Then
            If MainClass.ValidateWithMasterTable((txtSuppcode.Text), "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='C'") = True Then
                MsgBox("Supplier / Customer Master is Closed, So cann't be saved", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtSuppcode.Enabled = True Then txtSuppcode.Focus()
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable((txtSuppcode.Text), "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STOP_RGP='Y'") = True Then
                MsgBox("Supplier / Customer Master is Closed, So cann't be saved", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtSuppcode.Enabled = True Then txtSuppcode.Focus()
                Exit Function
            End If
            If PubGSTApplicable = False Then
                MsgBox("Cann't be made new Entry, before applicable GST.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MainClass.ValidateWithMasterTable((txtSuppName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgInformation("Supplier Name is not a Supplier or Customer Category. Cannot Save")
            If txtSuppName.Enabled = True Then txtSuppName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtBillTo.Text) = "" Then
            MsgInformation("Bill To is blank. Cannot Save")
            txtBillTo.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSuppName.Text) & "'") = False Then
                MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
                txtBillTo.Focus()
                FieldsVarification = False
            End If
        End If


        pISGSTRegd = "N"
        If MainClass.ValidateWithMasterTable((txtSuppcode.Text), "SUPP_CUST_CODE", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            pISGSTRegd = MasterNo
        End If

        If cboGSTStatus.SelectedIndex = -1 Then
            MsgBox("Please select GST Status", MsgBoxStyle.Information)
            If cboGSTStatus.Enabled = True Then cboGSTStatus.Focus()
            FieldsVarification = False
            Exit Function
        End If


        Dim mItemCode As String
        Dim mGSTClass As String
        Dim mItemExempted As String = ""

        If VB.Left(cboGSTStatus.Text, 1) = "N" Then
            With SprdMain
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow

                    .Col = ColItemCode
                    mItemCode = Trim(.Text)

                    mGSTClass = "0"
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "GST_ITEMCLASS", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mGSTClass = MasterNo
                    End If

                    If mGSTClass <> "1" Then
                        MsgInformation("Item is not a Non-GST Item, So that cann't be Save.")
                        FieldsVarification = False
                        Exit Function
                    End If
                Next
            End With
        Else

            With SprdMain
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow

                    .Col = ColItemCode
                    mItemCode = Trim(.Text)

                    mGSTClass = "2"
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "GST_ITEMCLASS", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mGSTClass = MasterNo
                    End If

                    If mGSTClass = "2" Then
                        mItemExempted = CStr(True)
                    Else
                        mItemExempted = CStr(False)
                        Exit For
                    End If
                Next
            End With

            If CBool(mItemExempted) = False Then
                If pISGSTRegd = "Y" And VB.Left(cboGSTStatus.Text, 1) <> "G" Then
                    MsgBox("Supplier is registered, please select the GST Refund.", MsgBoxStyle.Information)
                    ' txtSupplier.SetFocus
                    FieldsVarification = False
                    Exit Function
                End If


                If pISGSTRegd = "N" And VB.Left(cboGSTStatus.Text, 1) <> "R" Then
                    MsgBox("Supplier is not registered, please select the Reverse Charge / Un-Registered.", MsgBoxStyle.Information)
                    ' txtSupplier.SetFocus
                    FieldsVarification = False
                    Exit Function
                End If

                If pISGSTRegd = "E" And VB.Left(cboGSTStatus.Text, 1) <> "E" Then
                    MsgBox("GST Exempted Supplier, please select the GST Exempted.", MsgBoxStyle.Information)
                    ' txtSupplier.SetFocus
                    FieldsVarification = False
                    Exit Function
                End If

                '            If Trim(mPartyGSTNo) = Trim(mCompanyGSTNo) Then
                '        '        If (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) > 0 Then
                '        '            MsgBox "GST Amount Should be Zero.", vbInformation
                '        '            FieldsVarification = False
                '        '            Exit Function
                '        '        End If
                '            Else
                '                If Left(cboGSTStatus.Text, 1) = "G" Or Left(cboGSTStatus.Text, 1) = "R" Then
                '                    If (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) = 0 Then
                '                        MsgBox "GST Amount Cann't be Zero.", vbInformation
                '                        FieldsVarification = False
                '                        Exit Function
                '                    End If
                '                Else
                '                    If (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) > 0 Then
                '                        MsgBox "GST Amount Should not be Zero.", vbInformation
                '                        FieldsVarification = False
                '                        Exit Function
                '                    End If
                '                End If
                '            End If
            End If
        End If


        '    If pISGSTRegd = "Y" And Left(cboGSTStatus.Text, 1) <> "G" Then
        '        MsgBox "Supplier is registered, please select the GST Refund.", vbInformation
        '       ' txtSupplier.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        '
        '    If pISGSTRegd = "N" And Left(cboGSTStatus.Text, 1) = "G" Then
        '        MsgBox "Supplier is not registered, So Cann't be Select GST Refund.", vbInformation
        '       ' txtSupplier.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        '
        '    If pISGSTRegd = "E" And Left(cboGSTStatus.Text, 1) <> "E" Then
        '        MsgBox "GST Exempted Supplier, please select the GST Exempted.", vbInformation
        '       ' txtSupplier.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        mRGPChargeApp = VB.Left(cboGSTStatus.Text, 1)

        If CDate(txtGatePassDate.Text) >= CDate("01/07/2022") Then
            If CheckStockQty(SprdMain, ColStockQty, ColQty, ColItemCode, ColStockType, True) = False Then
                FieldsVarification = False
                Exit Function
            End If
        End If

        If CheckLotStockQty() = False Then
            FieldsVarification = False
            Exit Function
        End If

        If VB.Left(cboPurpose.Text, 1) = "B" Or VB.Left(cboPurpose.Text, 1) = "C" Then
            If Trim(txtServProvided.Text) = "" Then
                MsgBox("Please Select The Service., So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable(Trim(txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = False Then
                MsgBox("Service Provided is not defined in Master, So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            Else
                mRGPServiceCode = MasterNo
            End If
        End If

        With SprdMain
            For mRow = 1 To .MaxRows
                .Row = mRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)
                If Trim(.Text) <> "" Then

                    .Row = mRow
                    .Col = ColHSNCode
                    mHSNCode = Trim(UCase(.Text))
                    If mHSNCode = "" Then
                        mHSNCode = GetHSNCode(mItemCode)
                        .Text = mHSNCode
                        If mHSNCode = "" Then
                            MsgInformation("HSN Cann't be Blank.")
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                        If Len(mHSNCode) < 6 Then
                            MsgInformation("HSN must be Six Digit.")
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    '                    If Left(cboPurpose.Text, 1) = "B" Or Left(cboPurpose.Text, 1) = "C" Then
                    '                        If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal) = False Then GoTo err
                    '                    Else
                    If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo) = False Then GoTo err_Renamed
                    '                    End If


                    If mPartyGSTNo = Trim(IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)) Then

                    Else
                        If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "R" Then
                            If mLocal = "Y" Then
                                If pCGSTPer = 0 Then 'Left(cboGSTStatus.Text, 1) <> "E" And
                                    MsgInformation("CGST % is not Defined for HSN Code : " & mHSNCode)
                                    FieldsVarification = False
                                    Exit Function
                                End If

                                If pSGSTPer = 0 Then 'Left(cboGSTStatus.Text, 1) <> "E" And
                                    MsgInformation("SGST % is not Defined for HSN Code : " & mHSNCode)
                                    FieldsVarification = False
                                    Exit Function
                                End If

                            Else
                                If pIGSTPer = 0 Then 'Left(cboGSTStatus.Text, 1) <> "E" And
                                    MsgInformation("IGST % is not Defined for HSN Code : " & mHSNCode)
                                    FieldsVarification = False
                                    Exit Function
                                End If
                            End If
                        End If
                    End If

                    .Row = mRow
                    .Col = ColCGSTPer
                    .Text = VB6.Format(pCGSTPer, "0.00")

                    .Col = ColSGSTPer
                    .Text = VB6.Format(pSGSTPer, "0.00")

                    .Col = ColIGSTPer
                    .Text = VB6.Format(pIGSTPer, "0.00")

                    .Col = ColStockType
                    If Trim(.Text) = "FG" Then
                        MsgBox("You cann't be send FG Stock through RGP/NRGP.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        MainClass.SetFocusToCell(SprdMain, mRow, ColStockType)
                        Exit Function
                    End If

                    mPOServiceCode = CStr(-1)

                    If VB.Left(cboPurpose.Text, 1) = "B" Or VB.Left(cboPurpose.Text, 1) = "C" Then
                        .Col = ColJobOrderNo
                        If Trim(.Text) <> "" Then
                            If MainClass.ValidateWithMasterTable(Val(.Text), "AUTO_KEY_PO", "SAC_CODE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PO_STATUS='Y' AND PO_CLOSED='N'") = True Then
                                mPOServiceCode = MasterNo
                            End If
                        End If

                        If mRGPServiceCode <> mPOServiceCode Then
                            MsgInformation("Service is not Match with PO.")
                            FieldsVarification = False
                            Exit Function
                        End If

                        .Col = ColJobOrderNo
                        mPOChargeApp = "N"
                        If Trim(.Text) <> "" Then
                            If MainClass.ValidateWithMasterTable(Val(.Text), "AUTO_KEY_PO", "ISGSTAPPLICABLE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PO_STATUS='Y' AND PO_CLOSED='N'") = True Then
                                mPOChargeApp = MasterNo
                            End If
                        End If

                        If mRGPChargeApp <> mPOChargeApp Then
                            MsgInformation("GST Charge Not Match With PO.")
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If
            Next
        End With

        ''    If PubSuperUser = "U" Then
        If MODIFYMode = True Then
            If MaterialRecdAgtRGP() = True Then
                MsgInformation("Material Recieved Against This RGP, So Cann't be Modified")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If ValidateRGPSlip() = False Then
            MsgInformation("RGP Slip details are not Matched., So Cann't be Modified")
            FieldsVarification = False
            Exit Function
        End If

        ''    End If

        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then
        '        If Trim(pJWCompanyCode) = Trim(txtSuppcode.Text) And chkAgtPermission.Value = vbChecked Then
        '            If MsgQuestion("You defined the Jobwork Against Permission, Want to Continue ? ") = vbNo Then    ' User chose Yes.
        '                FieldsVarification = False
        '                Exit Function
        '            End If
        '        End If
        '    End If
        Call CalcTots()

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColRate, "N", "Please Check Rate.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Function CheckLotStockQty() As Boolean

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mLotNo As String
        Dim mAllStockQty As Double
        Dim mStockQty As Double
        Dim mLotQty As Double
        Dim mAutoQCIssue As String
        Dim mStockType As String = ""
        Dim mItemUOM As String = ""
        Dim mDivisionCode As Double
        Dim mCommonDivision As Double
        Dim I As Integer
        Dim mHeatNo As String

        If cboDivision.SelectedIndex >= 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
        Else
            CheckLotStockQty = True
            Exit Function
        End If


        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColUOM
                mItemUOM = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)


                .Col = ColLotNo
                mLotNo = Trim(.Text)

                .Col = ColHeatNo
                mHeatNo = Trim(.Text)

                .Col = ColStockQty
                mStockQty = CDbl(Trim(.Text))

                '            .Col = ColIssueQty
                '            mLotQty = Trim(.Text)

                If mLotNo <> "" Then
                    mLotQty = 0
                    For I = 1 To .MaxRows - 1
                        .Row = I

                        .Col = ColItemCode
                        If mItemCode = Trim(.Text) Then
                            .Col = ColReturnQty
                            mLotQty = mLotQty + Val(.Text)
                        End If
                    Next

                    .Row = cntRow

                    If mLotQty <> 0 Then ''mStockQty > mLotQty And
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "STOCKITEM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STOCKITEM='N'") = False Then

                            If MainClass.ValidateWithMasterTable(mItemCode, "AUTO_INDENT", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_INDENT='Y'") = True Then
                                mAutoQCIssue = "Y"
                            Else
                                mAutoQCIssue = "N"
                            End If

                            mCommonDivision = GetCommonDivCode()
                            mAllStockQty = GetBalanceStockQty(mItemCode, (txtGatePassDate.Text), mItemUOM, "STR", mStockType, "", ConWH, mDivisionCode, ConStockRefType_RGP, Val(txtGatepassno.Text),,, mHeatNo)

                            If RsCompany.Fields("COMPANY_CODE").Value = 1 And mAutoQCIssue = "N" Then
                                mAllStockQty = mAllStockQty + GetBalanceStockQty(mItemCode, (txtGatePassDate.Text), mItemUOM, "STR", "QC", "", ConWH, mDivisionCode, ConStockRefType_RGP, Val(txtGatepassno.Text),,, mHeatNo)
                            End If
                            If mDivisionCode <> mCommonDivision Then
                                If mCommonDivision > 0 Then
                                    mAllStockQty = mAllStockQty + GetBalanceStockQty(mItemCode, (txtGatePassDate.Text), mItemUOM, "STR", mStockType, "", ConWH, mCommonDivision, ConStockRefType_RGP, Val(txtGatepassno.Text),,, mHeatNo)
                                End If
                            End If

                            If mAllStockQty < mLotQty And mLotQty <> 0 Then
                                MsgInformation("You Have Not Enough Stock. For Item Code : " & mItemCode)
                                MainClass.SetFocusToCell(SprdMain, cntRow, ColReturnQty)
                                CheckLotStockQty = False
                                Exit Function
                            End If
                        End If
                    End If
                End If
NextRow:
            Next
        End With
        CheckLotStockQty = True
        Exit Function
ErrPart:
        CheckLotStockQty = False
    End Function

    Private Function MaterialRecdAgtRGP() As Boolean

        On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCount As Integer

        MaterialRecdAgtRGP = False
        SqlStr = " SELECT COUNT(1) AS CNTRECD " & vbCrLf & " FROM INV_RGP_REG_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND RGP_NO='" & txtGatepassno.Text & "'" & vbCrLf & " AND BOOKTYPE='M' AND ITEM_IO='I'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mCount = IIf(IsDBNull(RsTemp.Fields("CNTRECD").Value), 0, RsTemp.Fields("CNTRECD").Value)
            If mCount > 0 Then
                MaterialRecdAgtRGP = True
            End If
        End If

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Function ValidateRGPSlip() As Boolean

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCount As Integer
        Dim CntRow As Long
        Dim mItemCode As String
        Dim mQty As Double
        Dim mIncomingItemCode As String = ""

        ValidateRGPSlip = False

        With SprdMain
            For CntRow = 1 To .MaxRows - 1
                .Row = CntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColIncomingItemCode
                mIncomingItemCode = Trim(.Text)

                SqlStr = " SELECT COUNT(1) AS CNTRECD " & vbCrLf _
                    & " FROM INV_RGP_SLIP_DET " & vbCrLf _
                    & " WHERE AUTO_KEY_RGPSLIP='" & txtRgpreqno.Text & "'" & vbCrLf _
                    & " AND FROM_ITEM_CODE='" & mItemCode & "' AND ITEM_QTY=" & Val(mQty) & ""

                If mIncomingItemCode <> "" Then
                    SqlStr = SqlStr & vbCrLf & "AND INWARD_ITEM_CODE='" & mIncomingItemCode & "'"
                End If

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    mCount = IIf(IsDBNull(RsTemp.Fields("CNTRECD").Value), 0, RsTemp.Fields("CNTRECD").Value)
                    If mCount > 0 Then
                        ValidateRGPSlip = True
                    End If
                Else
                    ValidateRGPSlip = False
                End If
            Next
        End With





        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub frmGatePassGST_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        SqlStr = ""
        SqlStr = "Select * from INV_GATEPASS_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_GATEPASS_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqDetail, ADODB.LockTypeEnum.adLockReadOnly)

        cboGSTStatus.Items.Clear()
        cboGSTStatus.Items.Add("GST Refund")
        cboGSTStatus.Items.Add("Reverse Charge / Un-Registered")
        cboGSTStatus.Items.Add("Exempt")
        cboGSTStatus.Items.Add("Non GST")

        cboGSTStatus.SelectedIndex = -1

        cboTransmode.Items.Clear()
        cboTransmode.Items.Add("1. Road")
        cboTransmode.Items.Add("2. Rail")
        cboTransmode.Items.Add("3. Air")
        cboTransmode.Items.Add("4. Ship")
        cboTransmode.SelectedIndex = 0

        cboVehicleType.Items.Clear()
        cboVehicleType.Items.Add("Regular")
        cboVehicleType.Items.Add("Over Dimensional Cargo")
        cboVehicleType.SelectedIndex = 0


        Call AssignGrid(False)
        Call SetTextLengths()

        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim SqlStr As String
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)
        SqlStr = ""

        ''SELECT CLAUSE...

        SqlStr = " SELECT  A.AUTO_KEY_PASSNO AS REFNO,A.GATEPASS_DATE AS REF_DATE, CHALLAN_PREFIX || GATEPASS_NO AS CHALLAN_NO, B.SUPP_CUST_NAME, " & vbCrLf _
            & " DECODE(GATEPASS_TYPE,'R','RGP','NRGP') AS TYPE, REQ_NO,REQ_DATE, REMOVAL_TIME, " & vbCrLf _
            & " VEHICLE_NO, AUTH_EMP_CODE, PRE_EMP_CODE, " & vbCrLf _
            & " CASE WHEN GATEPASS_STATUS='N' THEN 'Pending' " & vbCrLf _
            & " WHEN GATEPASS_STATUS='Y' THEN 'Completed' ELSE 'Not Completed' END AS STATUS," & vbCrLf _
            & " OUTWARD_57F4NO,REMARKS "

        ''FROM CLAUSE...

        SqlStr = SqlStr & vbCrLf & " FROM INV_GATEPASS_HDR A,FIN_SUPP_CUST_MST B"

        ''WHERE CLAUSE...

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE = B.SUPP_CUST_CODE  " & vbCrLf & " AND SUBSTR(AUTO_KEY_PASSNO,LENGTH(AUTO_KEY_PASSNO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND GATEPASS_TYPE='" & lblBookType.Text & "'"

        ''ORDER BY CLAUSE...

        SqlStr = SqlStr & vbCrLf & " Order by GATEPASS_NO,AUTO_KEY_PASSNO"

        'MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        'FormatSprdView()

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        Me.UltraGrid1.DataSource = ds
        Me.UltraGrid1.DataMember = ""

        CreateGridHeader("S")

        MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")
        oledbAdapter.Dispose()
        oledbCnn.Close()

        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub CreateGridHeader(pShowType As String)
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   to create the grid header
        'Comments       :   Nil
        '----------------------------------------------------------------------------
        Try
            Dim inti As Integer
            'create column header



            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Key = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Gatepass No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Gatepass Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Challan No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Supplier Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "Requisition No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Requisition Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "Removal Time"
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "Vehicle No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Header.Caption = "Authorised Emp Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Header.Caption = "Prepared By"

            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Header.Caption = "Status"
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Header.Caption = "Outward 57 F4 No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Header.Caption = "Remarks"


            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment
            Next

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Width = 100


            'UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.MFGQty).MaskInput = "99999"

            Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            Me.UltraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

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
    'Private Sub FormatSprdView()

    '    With SprdView
    '        .Row = -1

    '        .set_RowHeight(0, 400)

    '        .set_ColWidth(0, 600)
    '        .set_ColWidth(1, 1000)
    '        .set_ColWidth(2, 1000)
    '        .set_ColWidth(3, 2500)
    '        .set_ColWidth(4, 600)
    '        .set_ColWidth(5, 1000)
    '        .set_ColWidth(6, 1000)
    '        .set_ColWidth(7, 600)
    '        .set_ColWidth(8, 1500)
    '        .set_ColWidth(9, 800)
    '        .set_ColWidth(10, 800)
    '        .set_ColWidth(11, 1000)
    '        .set_ColWidth(12, 1000)
    '        .set_ColWidth(13, 2000)


    '        MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
    '        MainClass.SetSpreadColor(SprdView, -1)
    '        SprdView.set_RowHeight(-1, 300)
    '        .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
    '        MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
    '    End With
    'End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .set_RowHeight(0, ConRowHeight * 2)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.5)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsReqDetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColItemCode, 8)

            .Col = ColIncomingItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsReqDetail.Fields("INWARD_ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColIncomingItemCode, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemName, 22)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("WO_DESCRIPTION", "PUR_PURCHASE_DET", PubDBCn)
            .set_ColWidth(ColItemDesc, 35)

            .Col = ColUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_UOM", "INV_GATEPASS_DET", PubDBCn)
            .set_ColWidth(ColUOM, 4)

            .ColsFrozen = ColUOM

            .Col = ColHSNCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("HSN_CODE", "INV_GATEPASS_DET", PubDBCn)
            .set_ColWidth(ColHSNCode, 4)

            .Col = ColLotNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsReqDetail.Fields("LOT_NO").DefinedSize ''MainClass.SetMaxLength("LOT_NO", "INV_GATE_DET", PubDBCn)
            '        .TypeEditLen = MainClass.SetMaxLength("LOT_NO", "INV_GATEPASS_DET", PubDBCn)
            .set_ColWidth(ColLotNo, 5)
            .ColHidden = True

            .Col = ColHeatNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsReqDetail.Fields("HEAT_NO").DefinedSize
            .set_ColWidth(ColHeatNo, 6)

            .Col = ColBatchNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsReqDetail.Fields("BATCH_NO").DefinedSize
            .set_ColWidth(ColBatchNo, 6)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("STOCK_TYPE", "INV_GATEPASS_DET", PubDBCn)
            .set_ColWidth(ColStockType, 3)

            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColStockQty, 7)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQty, 9)

            .Col = ColQtyKGs
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQtyKGs, 9)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)


            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRate, 8)
            '        .ColHidden = True

            For cntCol = ColAmount To ColIGSTAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 8)
            Next

            .Col = ColReturnQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColReturnQty, 6.5)

            .Col = ColF4No
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsReqDetail.Fields("F4NO").DefinedSize ''
            .set_ColWidth(ColF4No, 6)

            .Col = ColJobOrderNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsReqDetail.Fields("AUTO_KEY_WO").DefinedSize ''
            .set_ColWidth(ColJobOrderNo, 5)

            .Col = colRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("REMARKS", "INV_GATEPASS_DET", PubDBCn)
            .set_ColWidth(colRemarks, 5)


        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColReturnQty) ''ColIGSTAmount
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmount, ColIGSTAmount)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIncomingItemCode, ColJobOrderNo)
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsReqDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1

        With RsReqMain
            txtChallanno.MaxLength = .Fields("GATEPASS_NO").Precision
            txtGatePassDate.MaxLength = 10
            txtSuppcode.MaxLength = .Fields("SUPP_CUST_CODE").DefinedSize
            txtRgpreqno.MaxLength = .Fields("REQ_NO").Precision
            txtRgpreqdate.MaxLength = 10
            txtRemoval.MaxLength = 5
            txtGatepassno.MaxLength = .Fields("AUTO_KEY_PASSNO").Precision
            txtRemarks.MaxLength = .Fields("REMARKS").DefinedSize
            txtST38No.MaxLength = .Fields("ST_38_NO").DefinedSize
            txtEmp.MaxLength = .Fields("PRE_EMP_CODE").DefinedSize
            txtAuthority.MaxLength = .Fields("AUTH_EMP_CODE").DefinedSize
            txtVehicle.MaxLength = .Fields("VEHICLE_NO").DefinedSize
            txtF4no.MaxLength = .Fields("VEHICLE_NO").DefinedSize
            'txtQty.Maxlength = .Fields("INWARD_ITEM_QTY").Precision
            txtServProvided.MaxLength = MainClass.SetMaxLength("HSN_DESC", "GEN_HSN_MST", PubDBCn)

            TxtGRNo.MaxLength = .Fields("GRNo").DefinedSize ''						
            TxtGRDate.MaxLength = 10
            txtCarriers.MaxLength = .Fields("CARRIERS").DefinedSize ''						
            'txtTransportCode.MaxLength = .Fields("TRANSPORTER_GSTNO").DefinedSize
            txtDistance.MaxLength = .Fields("TRANS_DISTANCE").Precision

            txtReturnDate.Text = CStr(10)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mF4No As Double
        Dim mIsPaintF4 As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mGSTStatus As String
        Dim mSACCode As String
        Dim mTransMode As String
        Dim mVehicleType As String

        With RsReqMain
            If Not .EOF Then
                txtRgpreqno.Enabled = False
                txtGatepassno.Enabled = False
                lblMKey.Text = .Fields("AUTO_KEY_PASSNO").Value
                txtGatepassno.Text = IIf(IsDBNull(.Fields("AUTO_KEY_PASSNO").Value), 0, .Fields("AUTO_KEY_PASSNO").Value)

                txtPrefix.Text = IIf(IsDBNull(.Fields("CHALLAN_PREFIX").Value), 0, .Fields("CHALLAN_PREFIX").Value)

                txtChallanno.Text = IIf(IsDBNull(.Fields("GATEPASS_NO").Value), 0, .Fields("GATEPASS_NO").Value)

                'mChallanNo = VB6.Format(Val(CStr(mChallanNo)), ConBillFormat)

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                    txtChallanno.Text = VB6.Format(Val(CStr(txtChallanno.Text)), ConBillFormat)
                End If
                txtGatePassDate.Text = VB6.Format(IIf(IsDBNull(.Fields("GATEPASS_DATE").Value), "", .Fields("GATEPASS_DATE").Value), "DD/MM/YYYY")
                txtRgpreqno.Text = IIf(IsDBNull(.Fields("REQ_NO").Value), 0, .Fields("REQ_NO").Value)
                txtRgpreqdate.Text = VB6.Format(IIf(IsDBNull(.Fields("REQ_DATE").Value), "", .Fields("REQ_DATE").Value), "DD/MM/YYYY")

                txtEmp.Text = IIf(IsDBNull(.Fields("PRE_EMP_CODE").Value), "", .Fields("PRE_EMP_CODE").Value)
                txtPrepare.Text = IIf(IsDBNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                txtSuppcode.Text = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)
                txtVehicle.Text = IIf(IsDBNull(.Fields("VEHICLE_NO").Value), "", .Fields("VEHICLE_NO").Value)
                txtAuthority.Text = IIf(IsDBNull(.Fields("AUTH_EMP_CODE").Value), "", .Fields("AUTH_EMP_CODE").Value)
                txtRemoval.Text = IIf(IsDBNull(.Fields("REMOVAL_TIME").Value), "", .Fields("REMOVAL_TIME").Value)
                txtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtST38No.Text = IIf(IsDBNull(.Fields("ST_38_NO").Value), "", .Fields("ST_38_NO").Value)
                txteWayBillNo.Text = IIf(IsDBNull(.Fields("E_BILLWAYNO").Value), "", .Fields("E_BILLWAYNO").Value)
                mF4No = Val(IIf(IsDBNull(.Fields("OUTWARD_57F4NO").Value), 0, .Fields("OUTWARD_57F4NO").Value))
                txtReturnDate.Text = VB6.Format(IIf(IsDBNull(.Fields("EXP_RTN_DATE").Value), "", .Fields("EXP_RTN_DATE").Value), "DD/MM/YYYY")

                If MainClass.ValidateWithMasterTable(Val(txtRgpreqno.Text), "AUTO_KEY_RGPSLIP", "DEPT_CODE", "INV_RGP_SLIP_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDeptCode = MasterNo
                End If

                If Val(CStr(mF4No)) > 0 Then
                    chkF4status.CheckState = System.Windows.Forms.CheckState.Checked
                    chkF4status.Enabled = False
                    txtF4no.Text = CStr(mF4No)
                Else
                    txtF4no.Text = ""
                End If

                mGSTStatus = IIf(IsDBNull(.Fields("GST_APP").Value), "N", .Fields("GST_APP").Value)

                If mGSTStatus = "G" Then
                    cboGSTStatus.SelectedIndex = 0
                ElseIf mGSTStatus = "R" Then
                    cboGSTStatus.SelectedIndex = 1
                ElseIf mGSTStatus = "E" Then
                    cboGSTStatus.SelectedIndex = 2
                Else
                    cboGSTStatus.SelectedIndex = 3
                End If
                cboGSTStatus.Enabled = False


                If .Fields("GATEPASS_TYPE").Value = "G" Then
                    cboGatePasstype.SelectedIndex = 2
                ElseIf .Fields("GATEPASS_TYPE").Value = "N" Then
                    cboGatePasstype.SelectedIndex = 1
                Else
                    cboGatePasstype.SelectedIndex = 0
                End If

                If .Fields("GATEPASS_STATUS").Value = "N" Then
                    cboStatus.SelectedIndex = 0
                ElseIf .Fields("GATEPASS_STATUS").Value = "Y" Then
                    cboStatus.SelectedIndex = 1
                Else
                    cboStatus.SelectedIndex = 2
                End If

                If .Fields("INPUT_FROM_FLAG").Value = "INV" Then
                    cboMaterial.SelectedIndex = 0
                Else
                    cboMaterial.SelectedIndex = 1
                End If

                If .Fields("PURPOSE").Value = "A" Then
                    cboPurpose.SelectedIndex = 0
                ElseIf .Fields("PURPOSE").Value = "B" Then
                    cboPurpose.SelectedIndex = 1
                ElseIf .Fields("PURPOSE").Value = "C" Then
                    cboPurpose.SelectedIndex = 2
                ElseIf .Fields("PURPOSE").Value = "D" Then
                    cboPurpose.SelectedIndex = 3
                ElseIf .Fields("PURPOSE").Value = "E" Then
                    cboPurpose.SelectedIndex = 4
                ElseIf .Fields("PURPOSE").Value = "F" Then
                    cboPurpose.SelectedIndex = 5
                ElseIf .Fields("PURPOSE").Value = "G" Then
                    cboPurpose.SelectedIndex = 6
                ElseIf .Fields("PURPOSE").Value = "H" Then
                    cboPurpose.SelectedIndex = 7
                ElseIf .Fields("PURPOSE").Value = "I" Then
                    cboPurpose.SelectedIndex = 8
                Else
                    cboPurpose.SelectedIndex = 9
                End If

                If MainClass.ValidateWithMasterTable((txtAuthority.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtAuthorityName.Text = MasterNo
                End If

                If MainClass.ValidateWithMasterTable((txtSuppcode.Text), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSuppName.Text = MasterNo
                End If

                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)

                'txtItemCode.Text = Trim(IIf(IsDbNull(.Fields("INWARD_ITEM_CODE").Value), "", .Fields("INWARD_ITEM_CODE").Value))
                'If MainClass.ValidateWithMasterTable((txtItemCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '    txtItemDesc.Text = MasterNo
                'End If

                'txtQty.Text = Trim(IIf(IsDbNull(.Fields("INWARD_ITEM_QTY").Value), "", .Fields("INWARD_ITEM_QTY").Value))

                mIsPaintF4 = GETIsPaintF4(Val(txtRgpreqno.Text))
                '            chkPaintF4.Value = IIf(mIsPaintF4 = "Y", vbChecked, vbUnchecked)

                '            chkScrap.Value = IIf(!IS_SCRAP = "Y", vbChecked, vbUnchecked)

                '            chkAgtPermission.Value = IIf(!AGT_Permission = "Y", vbChecked, vbUnchecked)
                '            chkAgtPermission.Enabled = False ''IIf((!AGT_Permission = "Y" Or Trim(pJWCompanyCode) = Trim(txtSuppcode.Text)) And RsCompany.fields("COMPANY_CODE").value = 1, True, False)

                If MainClass.ValidateWithMasterTable(Val(txtRgpreqno.Text), "AUTO_KEY_RGPSLIP", "DIV_CODE", "INV_RGP_SLIP_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionCode = Val(MasterNo)
                    If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDivisionDesc = Trim(MasterNo)
                        cboDivision.Text = mDivisionDesc
                    End If
                End If

                mSACCode = IIf(IsDBNull(.Fields("SAC_CODE").Value), -1, .Fields("SAC_CODE").Value)
                If MainClass.ValidateWithMasterTable(mSACCode, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                    txtServProvided.Text = Trim(MasterNo)
                Else
                    txtServProvided.Text = ""
                End If

                mTransMode = IIf(IsDBNull(.Fields("TRANSPORT_MODE").Value), "0", .Fields("TRANSPORT_MODE").Value)

                mTransMode = Mid(mTransMode, 1, 1)
                cboTransmode.SelectedIndex = Val(mTransMode) - 1

                mVehicleType = IIf(IsDBNull(.Fields("VEHICLE_TYPE").Value), "", .Fields("VEHICLE_TYPE").Value)
                cboVehicleType.SelectedIndex = IIf(mVehicleType = "R", 0, 1)

                TxtGRNo.Text = IIf(IsDBNull(.Fields("GRNo").Value), "", .Fields("GRNo").Value)
                TxtGRDate.Text = IIf(IsDBNull(.Fields("GRDATE").Value), "", .Fields("GRDATE").Value)
                txtCarriers.Text = IIf(IsDBNull(.Fields("CARRIERS").Value), "", .Fields("CARRIERS").Value)

                txtDistance.Text = IIf(IsDBNull(.Fields("TRANS_DISTANCE").Value), "0", .Fields("TRANS_DISTANCE").Value)



                If MainClass.ValidateWithMasterTable(txtCarriers.Text, "TRANSPORTER_NAME", "TRANSPORTER_ID", "FIN_TRANSPORTER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtTransportCode.Text = Trim(MasterNo)
                End If

                lblAddUser.Text = IIf(IsDBNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                lblAddDate.Text = VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                lblModUser.Text = IIf(IsDBNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                lblModDate.Text = VB6.Format(IIf(IsDBNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")

                cmdSearchRgp.Enabled = False
                Call txtSuppcode_Validating(txtSuppcode, New System.ComponentModel.CancelEventArgs(False))
                Call ShowDetail1(.Fields("AUTO_KEY_PASSNO").Value, mDivisionCode)
                cboGatePasstype.Enabled = False

            End If
        End With
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        '    FormatSprdMain -1

        SprdMain.Enabled = True
        txtGatepassno.Enabled = True
        cmdSearch.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Function GETIsPaintF4(ByRef pReqNo As Double) As String

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GETIsPaintF4 = "N"
        If Val(CStr(pReqNo)) = 0 Then Exit Function

        With SprdMain
            SqlStr = "SELECT ISPAINTF4 FROM INV_RGP_SLIP_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_RGPSLIP=" & pReqNo & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                GETIsPaintF4 = IIf(IsDBNull(RsTemp.Fields("IsPaintF4").Value), "N", RsTemp.Fields("IsPaintF4").Value)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub ShowDetail1(ByVal pReqNum As Double, ByVal mDivisionCode As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        Dim mStkType As String
        Dim mQty As String
        Dim mRemarks As String
        Dim mRate As Double
        Dim mLotNo As String
        Dim mHeatNo As String
        'Dim mJWUOM As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_GATEPASS_DET  " & vbCrLf & " Where AUTO_KEY_PASSNO = " & Val(CStr(pReqNum)) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsReqDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = mItemCode

                SprdMain.Col = ColItemName
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                'mukul'
                SprdMain.Col = ColItemDesc
                If String.IsNullOrEmpty(mItemCode) Then
                    mItemDesc = ""
                Else

                    mItemDesc = GetItemDescription(mItemCode)
                End If
                    SprdMain.Text = mItemDesc

                SprdMain.Col = ColUOM
                mItemUOM = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                SprdMain.Text = mItemUOM

                SprdMain.Col = ColHSNCode
                SprdMain.Text = IIf(IsDBNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value)

                SprdMain.Col = ColLotNo
                mLotNo = IIf(IsDBNull(.Fields("LOT_NO").Value), "", .Fields("LOT_NO").Value)
                SprdMain.Text = mLotNo

                SprdMain.Col = ColHeatNo
                SprdMain.Text = IIf(IsDBNull(.Fields("HEAT_NO").Value), "", .Fields("HEAT_NO").Value)
                mHeatNo = IIf(IsDBNull(.Fields("HEAT_NO").Value), "", .Fields("HEAT_NO").Value)

                SprdMain.Col = ColBatchNo
                SprdMain.Text = IIf(IsDBNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value)

                SprdMain.Col = ColStockType
                mStkType = IIf(IsDBNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)
                SprdMain.Text = mStkType

                SprdMain.Col = ColQty
                mQty = IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)
                SprdMain.Text = mQty

                SprdMain.Col = ColQtyKGs
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_QTY_KGS").Value), 0, .Fields("ITEM_QTY_KGS").Value)

                'mJWUOM = ""
                'If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_JW_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '    mJWUOM = MasterNo
                'End If

                'SprdMain.Col = ColUOM
                'If Trim(SprdMain.Text) = mJWUOM Then
                '    SprdMain.Col = ColQty
                '    mQty = Val(SprdMain.Text)
                'Else
                '    SprdMain.Col = ColQtyKGs
                '    mQty = Val(SprdMain.Text)
                'End If

                SprdMain.Col = ColStockQty
                SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, Trim(txtGatePassDate.Text), mItemUOM, "STR", mStkType, mLotNo, ConWH, mDivisionCode, IIf(cboGatePasstype.SelectedIndex = 1, ConStockRefType_NRG, ConStockRefType_RGP), Val(txtGatepassno.Text),,, mHeatNo))


                SprdMain.Col = ColReturnQty
                SprdMain.Text = IIf(IsDBNull(.Fields("RTN_QTY").Value), 0, .Fields("RTN_QTY").Value)

                SprdMain.Col = ColRate
                mRate = IIf(IsDBNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)
                If mRate = 0 Then
                    mRate = GetItemRateFromCustMst(mItemCode, mItemUOM)
                End If
                SprdMain.Text = CStr(mRate)

                SprdMain.Col = ColAmount
                SprdMain.Text = VB6.Format(CDbl(mQty) * mRate, "0.00")

                SprdMain.Col = ColCGSTPer
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("CGST_PER").Value), 0, .Fields("CGST_PER").Value), "0.00")

                SprdMain.Col = ColCGSTAmount
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("CGST_AMOUNT").Value), 0, .Fields("CGST_AMOUNT").Value), "0.00")

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("SGST_PER").Value), 0, .Fields("SGST_PER").Value), "0.00")

                SprdMain.Col = ColSGSTAmount
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("SGST_AMOUNT").Value), 0, .Fields("SGST_AMOUNT").Value), "0.00")

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value), "0.00")

                SprdMain.Col = ColIGSTAmount
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("IGST_AMOUNT").Value), 0, .Fields("IGST_AMOUNT").Value), "0.00")

                SprdMain.Col = ColF4No
                mQty = IIf(IsDBNull(.Fields("F4NO").Value), "", .Fields("F4NO").Value)
                SprdMain.Text = IIf(mQty = "0", "", mQty)

                SprdMain.Col = colRemarks
                mRemarks = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                SprdMain.Text = mRemarks

                SprdMain.Col = ColItemDesc
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColIncomingItemCode
                SprdMain.Text = IIf(IsDBNull(.Fields("INWARD_ITEM_CODE").Value), "", .Fields("INWARD_ITEM_CODE").Value)

                SprdMain.Col = ColJobOrderNo
                SprdMain.Text = CStr(IIf(IsDBNull(.Fields("AUTO_KEY_WO").Value), "", .Fields("AUTO_KEY_WO").Value))


                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            'MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        AdoDCMain.Refresh
            'FormatSprdView()
            UltraGrid1.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()


        lblMKey.Text = ""

        txtChallanno.Text = ""
        txtGatePassDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtEmp.Text = ""
        txtPrepare.Text = PubUserID
        'txtPrefix.Text = 1

        cboGatePasstype.SelectedIndex = 0
        cboStatus.Enabled = False
        cboPurpose.SelectedIndex = -1
        cboPurpose.Enabled = False
        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        cboDivision.Enabled = False

        txtPrefix.Text = "" ''GetDocumentPrefix("J", lblBookType.Text)
        txtRgpreqno.Text = ""
        txtAuthority.Text = ""
        txtSuppcode.Text = ""
        txtRemarks.Text = "GOODS NOT FOR SALE, MATERIAL GOING IN TROLLY & BIN."
        txtST38No.Text = ""
        txteWayBillNo.Text = ""
        txteWayBillNo.Enabled = False
        txtRgpreqdate.Text = ""
        txtAuthorityName.Text = ""
        txtSuppName.Text = ""
        txtVehicle.Text = ""
        cboStatus.SelectedIndex = 0
        txtF4no.Text = ""
        cboMaterial.SelectedIndex = 0
        txtGatepassno.Text = ""
        txtRemoval.Text = GetServerTime()
        chkF4status.CheckState = System.Windows.Forms.CheckState.Unchecked
        cboGSTStatus.SelectedIndex = -1
        cboGSTStatus.Enabled = True

        cboTransmode.SelectedIndex = 0
        cboVehicleType.SelectedIndex = 0

        '    chkPaintF4.Value = vbUnchecked
        chkF4status.Enabled = True
        lblDept.Text = ""
        'txtItemCode.Text = ""
        'txtItemDesc.Text = ""
        'txtQty.Text = ""
        txtReturnDate.Text = ""
        mDeptCode = ""
        txtAddress.Text = ""
        txtServProvided.Text = ""

        txtF4no.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
        txtGatePassDate.Enabled = False
        txtChallanno.Enabled = IIf(PubSuperUser = "S", True, False)
        txtRgpreqno.Enabled = True
        cmdSearchRgp.Enabled = True
        cmdSearch.Enabled = True
        cboGatePasstype.Enabled = False
        MainClass.ClearGrid(SprdMain)

        txtBillTo.Text = ""

        txtBillTo.Enabled = False

        txtDistance.Text = ""
        TxtGRNo.Text = ""
        TxtGRDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtCarriers.Text = ""
        txtTransportCode.Text = ""

        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""
        'cmdItemCode.Enabled = False
        'txtItemCode.Enabled = False



        '    chkScrap.Value = vbUnchecked
        '    chkScrap.Visible = IIf(RsCompany.fields("COMPANY_CODE").value = 9, True, False)
        '    chkScrap.Enabled = IIf(RsCompany.fields("COMPANY_CODE").value = 9, True, False)
        '    chkAgtPermission.Value = vbUnchecked
        '    chkAgtPermission.Enabled = False '' IIf(RsCompany.fields("COMPANY_CODE").value = 1, True, False)

        Call FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub frmGatePassGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub frmGatePassGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Public Sub frmGatePassGST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim mITEMNAME As String
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        'AdoDCMain.Visible = False
        FillCboStatus()
        txtChallanno.Enabled = True
        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))
        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub txtAuthority_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthority.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAuthority_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthority.DoubleClick
        Call cmdSearchauth_Click(cmdSearchauth, New System.EventArgs())
    End Sub

    Private Sub txtAuthority_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAuthority.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If MainClass.ValidateWithMasterTable((txtAuthority.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND RGP_AUTH='Y'") = True Then
            txtAuthorityName.Text = MasterNo
        Else
            MsgInformation("Invalid Authority Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtChallanNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallanno.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChallanNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallanno.DoubleClick
        Call cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub txtChallanno_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChallanno.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mReqnum As String = ""

        Exit Sub

        If Trim(txtChallanno.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsReqMain.EOF = False Then mReqnum = RsReqMain.Fields("GATEPASS_NO").Value

        SqlStr = "Select * From INV_GATEPASS_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(GATEPASS_NO))=" & Val(txtChallanno.Text) & ""

        SqlStr = SqlStr & vbCrLf & " AND GATEPASS_TYPE='" & lblBookType.Text & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsReqMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Item Consumption, Use Generate Item Consumption Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_GATEPASS_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " LTRIM(RTRIM(GATEPASS_NO))=" & Val(mReqnum) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtF4no_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtF4no.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mF4No As String = ""

        If Val(txtF4no.Text) = 0 Then GoTo EventExitSub

        If MODIFYMode = True And RsReqMain.EOF = False Then mF4No = IIf(IsDBNull(RsReqMain.Fields("OUTWARD_57F4NO").Value), "", RsReqMain.Fields("OUTWARD_57F4NO").Value)

        SqlStr = "Select * From INV_GATEPASS_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUBSTR(AUTO_KEY_PASSNO,LENGTH(AUTO_KEY_PASSNO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND LTRIM(RTRIM(OUTWARD_57F4NO))=" & Val(txtF4no.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsReqMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such No, Use Generate New No Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_GATEPASS_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(OUTWARD_57F4NO))=" & Val(mF4No) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        '    Resume
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtGatePassDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGatePassDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGatePassDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGatePassDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtGatePassDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtGatePassDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmp.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmp.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmp.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtF4no_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtF4no.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGatepassno_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGatepassno.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGatepassno_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGatepassno.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub



    Private Sub txtItemDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRemoval_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemoval.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemoval_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRemoval.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRemoval.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtRemoval.Text) Then
            MsgInformation("Invalid Time.")
            Cancel = True
            GoTo EventExitSub
        End If
        txtRemoval.Text = VB6.Format(txtRemoval.Text, "HH:MM")

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRgpreqdate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRgpreqdate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRgpreqdate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRgpreqdate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtRgpreqdate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtRgpreqdate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRgpreqno_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRgpreqno.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Public Sub txtGatepassno_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGatepassno.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mReqnum As String = ""

        If Trim(txtGatepassno.Text) = "" Then GoTo EventExitSub

        If Len(txtGatepassno.Text) < 6 Then
            txtGatepassno.Text = Val(txtGatepassno.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsReqMain.EOF = False Then mReqnum = RsReqMain.Fields("AUTO_KEY_PASSNO").Value

        SqlStr = "Select * From INV_GATEPASS_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND LTRIM(RTRIM(AUTO_KEY_PASSNO))=" & Val(txtGatepassno.Text) & ""

        SqlStr = SqlStr & vbCrLf & " AND GATEPASS_TYPE='" & lblBookType.Text & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsReqMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such No, Use Generate New No Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_GATEPASS_HDR " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND LTRIM(RTRIM(AUTO_KEY_PASSNO))=" & Val(mReqnum) & ""

                SqlStr = SqlStr & vbCrLf & " AND GATEPASS_TYPE='" & lblBookType.Text & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRgpreqno_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRgpreqno.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRgpreqno_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRgpreqno.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mReqnum As String = ""


        If Trim(txtRgpreqno.Text) = "" Then GoTo EventExitSub
        If Len(txtRgpreqno.Text) < 6 Then
            txtRgpreqno.Text = Val(txtRgpreqno.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        'SqlStr = "Select * From INV_RGP_SLIP_HDR " & vbCrLf _
        '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND LTRIM(RTRIM(AUTO_KEY_RGPSLIP))=" & Val(txtRgpreqno.Text) & ""

        SqlStr = " SELECT A.* " & vbCrLf _
            & " From INV_RGP_SLIP_HDR A,FIN_SUPP_CUST_MST B WHERE " & vbCrLf _
            & " a.COMPANY_CODE = b.COMPANY_CODE AND a.SUPP_CUST_CODE = b.SUPP_CUST_CODE " & vbCrLf _
            & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And LTrim(RTrim(AUTO_KEY_RGPSLIP)) = " & Val(txtRgpreqno.Text) & ""

        SqlStr = SqlStr & vbCrLf & " AND GATEPASS_TYPE='" & lblBookType.Text & "'"

        'If lblBookType.Text = "N" Then
        '    SqlStr = SqlStr & vbCrLf & " AND NRGP_APPROVED='Y'"
        'End If

        If lblBookType.Text = "N" Then
            SqlStr = SqlStr & vbCrLf & " AND NRGP_APPROVED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND NRGP_APPROVED = CASE WHEN PURPOSE ='G' OR PURPOSE ='B' OR INTER_UNIT='Y' THEN NRGP_APPROVED ELSE 'Y' END "
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Clear1()
            txtGatepassno.Enabled = True
            txtGatePassDate.Enabled = True
            cmdSearch.Enabled = False
            If RsTemp.Fields("RGP_SLIP_STATUS").Value = "N" Then
                If ShowRgpSlip(RsTemp) = False Then
                    Cancel = True
                    GoTo EventExitSub
                End If
            Else
                ErrorMsg(RsTemp.Fields("AUTO_KEY_RGPSLIP").Value & " Already made.", , MsgBoxStyle.Critical)
                Cancel = True
            End If
        Else
            ErrorMsg("Please Enter Vaild Requisition Number.", "", MsgBoxStyle.Critical)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtServProvided_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServProvided.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtServProvided_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServProvided.DoubleClick
        SearchProvidedMaster()
    End Sub

    Private Sub txtServProvided_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtServProvided.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtServProvided.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtServProvided_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtServProvided.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchProvidedMaster()
    End Sub

    Private Sub txtServProvided_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServProvided.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        'Dim mReverseChargeApp As String
        Dim mServCode As String
        Dim mSACCode As String

        If Trim(txtServProvided.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT HSN_CODE, HSN_DESC" & vbCrLf & " FROM GEN_HSN_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HSN_DESC='" & MainClass.AllowSingleQuote((txtServProvided.Text)) & "' AND CODETYPE='S'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            MsgInformation("Please Select Valid Service Provided")
            Cancel = True
            GoTo EventExitSub
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchProvidedMaster()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'"

        If MainClass.SearchGridMaster((txtServProvided.Text), "GEN_HSN_MST", "HSN_DESC", "HSN_CODE", , , SqlStr) = True Then
            txtServProvided.Text = AcName
            txtServProvided_Validating(txtServProvided, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtST38No_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtST38No.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtST38No_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtST38No.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSuppcode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuppcode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSuppcode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSuppcode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAddress As String

        If Trim(txtSuppcode.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, " & vbCrLf & " SUPP_CUST_STATE, SUPP_CUST_PIN " & vbCrLf & " FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtSuppcode.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            txtSuppName.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
            mAddress = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value))
            mAddress = mAddress & ", " & IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
            mAddress = mAddress & ", " & IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)

            txtAddress.Text = mAddress
        Else
            txtSuppName.Text = ""
            txtAddress.Text = ""
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtVehicle_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Public Function ShowRgpSlip(ByRef mRsRGP As ADODB.Recordset) As Boolean
        On Error GoTo ErrPart
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        txtRgpreqno.Text = IIf(IsDBNull(mRsRGP.Fields("AUTO_KEY_RGPSLIP").Value), 0, mRsRGP.Fields("AUTO_KEY_RGPSLIP").Value)

        txtRgpreqdate.Text = IIf(IsDBNull(mRsRGP.Fields("RGP_SLIP_DATE").Value), "", mRsRGP.Fields("RGP_SLIP_DATE").Value)
        If MainClass.ValidateWithMasterTable((mRsRGP.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSuppcode.Text = MasterNo
            '    mCustomerCode = Trim(mRsDC.Fields("SUPP_CUST_CODE").Value)
        End If
        txtSuppcode.Text = IIf(IsDBNull(mRsRGP.Fields("SUPP_CUST_CODE").Value), "", mRsRGP.Fields("SUPP_CUST_CODE").Value)
        txtSuppcode.Enabled = False

        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then
        '        If Trim(pJWCompanyCode) = Trim(txtSuppcode.Text) Then
        '            chkAgtPermission.Enabled = True
        '        End If
        '    End If

        txtAuthority.Text = IIf(IsDBNull(mRsRGP.Fields("AUTH_GIVEN_BY").Value), "", mRsRGP.Fields("AUTH_GIVEN_BY").Value)
        txtVehicle.Text = IIf(IsDBNull(mRsRGP.Fields("VEHICLE_NO").Value), "", mRsRGP.Fields("VEHICLE_NO").Value)
        txtEmp.Text = IIf(IsDBNull(mRsRGP.Fields("EMP_CODE").Value), "", mRsRGP.Fields("EMP_CODE").Value)
        '    txtPrepare.Text = ""

        mDeptCode = IIf(IsDBNull(mRsRGP.Fields("DEPT_CODE").Value), "", mRsRGP.Fields("DEPT_CODE").Value)
        txtReturnDate.Text = VB6.Format(IIf(IsDBNull(mRsRGP.Fields("EXP_RTN_DATE").Value), "", mRsRGP.Fields("EXP_RTN_DATE").Value), "DD/MM/YYYY")

        If MainClass.ValidateWithMasterTable((txtAuthority.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtAuthorityName.Text = MasterNo
        End If

        If MainClass.ValidateWithMasterTable((txtSuppcode.Text), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSuppName.Text = MasterNo
        End If

        txtBillTo.Text = IIf(IsDBNull(mRsRGP.Fields("BILL_TO_LOC_ID").Value), "", mRsRGP.Fields("BILL_TO_LOC_ID").Value)
        txtBillTo_Validating(txtBillTo, New System.ComponentModel.CancelEventArgs(False))

        lblDept.Text = IIf(IsDBNull(mRsRGP.Fields("DEPT_CODE").Value), "", mRsRGP.Fields("DEPT_CODE").Value)

        'txtItemCode.Text = Trim(IIf(IsDbNull(mRsRGP.Fields("INWARD_ITEM_CODE").Value), "", mRsRGP.Fields("INWARD_ITEM_CODE").Value))
        'If MainClass.ValidateWithMasterTable((txtItemCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    txtItemDesc.Text = MasterNo
        'End If

        'txtQty.Text = Trim(IIf(IsDbNull(mRsRGP.Fields("INWARD_ITEM_QTY").Value), "", mRsRGP.Fields("INWARD_ITEM_QTY").Value))

        '    chkPaintF4.Value = IIf(mRsRGP!IsPaintF4 = "Y", vbChecked, vbUnchecked)

        If mRsRGP.Fields("GATEPASS_TYPE").Value = "G" Then
            cboGatePasstype.SelectedIndex = 2
        ElseIf mRsRGP.Fields("GATEPASS_TYPE").Value = "N" Then
            cboGatePasstype.SelectedIndex = 1
        Else
            cboGatePasstype.SelectedIndex = 0
        End If

        mDivisionCode = IIf(IsDBNull(mRsRGP.Fields("DIV_CODE").Value), -1, mRsRGP.Fields("DIV_CODE").Value)

        If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionDesc = Trim(MasterNo)
            cboDivision.Text = mDivisionDesc
        End If
        If mRsRGP.Fields("PURPOSE").Value = "A" Then
            cboPurpose.SelectedIndex = 0
        ElseIf mRsRGP.Fields("PURPOSE").Value = "B" Then
            cboPurpose.SelectedIndex = 1
        ElseIf mRsRGP.Fields("PURPOSE").Value = "C" Then
            cboPurpose.SelectedIndex = 2
        ElseIf mRsRGP.Fields("PURPOSE").Value = "D" Then
            cboPurpose.SelectedIndex = 3
        ElseIf mRsRGP.Fields("PURPOSE").Value = "E" Then
            cboPurpose.SelectedIndex = 4
        ElseIf mRsRGP.Fields("PURPOSE").Value = "F" Then
            cboPurpose.SelectedIndex = 5
        ElseIf mRsRGP.Fields("PURPOSE").Value = "G" Then
            cboPurpose.SelectedIndex = 6
        ElseIf mRsRGP.Fields("PURPOSE").Value = "H" Then
            cboPurpose.SelectedIndex = 7
        ElseIf mRsRGP.Fields("PURPOSE").Value = "I" Then
            cboPurpose.SelectedIndex = 8
        Else
            cboPurpose.SelectedIndex = 9
        End If

        Dim mPartyGSTNo As String
        Dim mSameGSTNo As String

        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtSuppcode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")
        mSameGSTNo = IIf(mPartyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "Y", "N")

        If RsCompany.Fields("FYEAR").Value > 2022 Then
            If mRsRGP.Fields("GATEPASS_TYPE").Value = "G" Then
                txtPrefix.Text = GetDocumentPrefix("J", IIf(mSameGSTNo = "Y", "C", lblBookType.Text))
            ElseIf mRsRGP.Fields("GATEPASS_TYPE").Value = "N" Then
                txtPrefix.Text = GetDocumentPrefix("J", IIf(mSameGSTNo = "Y", "U", lblBookType.Text))
            Else
                txtPrefix.Text = GetDocumentPrefix("J", IIf(mSameGSTNo = "Y", "C", lblBookType.Text))
            End If

        End If

        If ShowFromRGPDetail((mRsRGP.Fields("AUTO_KEY_RGPSLIP").Value), mDivisionCode) = False Then GoTo ErrPart
        '    Call FillSprdExp
        ShowRgpSlip = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ShowRgpSlip = False
        '    Resume

    End Function

    Public Function ShowFromRGPDetail(ByRef mRGPNo As Double, ByRef mDivision As Double) As Boolean

        On Error GoTo ErrPart
        Dim RsDc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mTariff As String
        Dim mTariffDesc As String
        Dim mUOM As String = ""
        Dim mRate As Double
        Dim mQty As Double
        Dim mStkType As String
        Dim mLotNo As String
        Dim mHSNCode As String
        Dim mAmount As Double
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mLocal As String
        Dim mOrderNo As Double
        Dim mPOSACCode As String
        Dim mPOChargeApp As String
        Dim mPartyGSTNo As String
        Dim mHeatNo As String

        mLocal = "N"
        'If MainClass.ValidateWithMasterTable(Trim(txtSuppcode.Text), "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mLocal = MasterNo
        'End If

        mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable(Trim(txtSuppcode.Text), "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        mLocal = GetPartyBusinessDetail(Trim(txtSuppcode.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtSuppcode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")

        SqlStr = "SELECT A.* , " & vbCrLf _
            & " GETFIFOITEMRATE_GP(TO_DATE('" & VB6.Format(txtRgpreqdate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),FROM_ITEM_CODE,  " & RsCompany.Fields("COMPANY_CODE").Value & ", 1) AS ITEM_RATE" & vbCrLf _
            & " FROM INV_RGP_SLIP_DET A" & vbCrLf _
            & " WHERE AUTO_KEY_RGPSLIP=" & mRGPNo & "" & vbCrLf _
            & " ORDER BY SERIAL_NO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDc, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdMain
            cntRow = 1
            If RsDc.EOF = False Then
                Do While Not RsDc.EOF


                    .Row = cntRow
                    .Col = ColItemCode
                    .Text = IIf(IsDBNull(RsDc.Fields("FROM_ITEM_CODE").Value), "", RsDc.Fields("FROM_ITEM_CODE").Value)
                    mItemCode = IIf(IsDBNull(RsDc.Fields("FROM_ITEM_CODE").Value), "", RsDc.Fields("FROM_ITEM_CODE").Value)

                    .Col = ColItemName
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Text = MasterNo
                    End If

                    .Col = ColUOM
                    mUOM = IIf(IsDBNull(RsDc.Fields("FROM_ITEM_UOM").Value), "", RsDc.Fields("FROM_ITEM_UOM").Value)
                    .Text = mUOM

                    mHSNCode = GetHSNCode(mItemCode)

                    .Col = ColHSNCode
                    .Text = mHSNCode

                    .Col = ColLotNo
                    mLotNo = IIf(IsDBNull(RsDc.Fields("LOT_NO").Value), "", RsDc.Fields("LOT_NO").Value)
                    .Text = mLotNo

                    .Col = ColHeatNo
                    mHeatNo = IIf(IsDBNull(RsDc.Fields("HEAT_NO").Value), "", RsDc.Fields("HEAT_NO").Value)
                    .Text = mHeatNo

                    .Col = ColStockType
                    mStkType = IIf(IsDBNull(RsDc.Fields("STOCK_TYPE").Value), "", RsDc.Fields("STOCK_TYPE").Value)
                    .Text = mStkType

                    .Col = ColQty
                    mQty = Val(IIf(IsDBNull(RsDc.Fields("ITEM_QTY").Value), "", RsDc.Fields("ITEM_QTY").Value))
                    .Text = VB6.Format(mQty, "0.0000")

                    .Col = ColQtyKGs
                    .Text = IIf(IsDBNull(RsDc.Fields("ITEM_QTY_KGS").Value), 0, RsDc.Fields("ITEM_QTY_KGS").Value)

                    .Col = ColStockQty
                    .Text = CStr(GetBalanceStockQty(mItemCode, Trim(txtGatePassDate.Text), mUOM, "STR", mStkType, mLotNo, ConWH, mDivision, IIf(cboGatePasstype.SelectedIndex = 1, ConStockRefType_NRG, ConStockRefType_RGP), Val(txtGatepassno.Text),,, mHeatNo))


                    .Col = ColRate
                    mRate = Val(IIf(IsDBNull(RsDc.Fields("ITEM_RATE").Value), "", RsDc.Fields("ITEM_RATE").Value)) ''  GetItemRateFromCustMst(Trim(mItemCode), mUOM)
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                        .Text = VB6.Format(mRate, "0.0000")
                    Else
                        .Text = VB6.Format(mRate, "0.00")
                    End If


                    .Col = ColAmount
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                        .Text = VB6.Format(mQty * mRate, "0.0000")
                    Else
                        .Text = VB6.Format(mQty * mRate, "0.00")
                    End If

                    If GetHSNDetails(mHSNCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo) = False Then GoTo ErrPart

                    .Col = ColCGSTPer
                    .Text = VB6.Format(mCGSTPer, "0.00")

                    .Col = ColSGSTPer
                    .Text = VB6.Format(mSGSTPer, "0.00")

                    .Col = ColIGSTPer
                    .Text = VB6.Format(mIGSTPer, "0.00")

                    .Col = ColF4No
                    .Text = VB6.Format(IIf(IsDBNull(RsDc.Fields("F4NO").Value), "", RsDc.Fields("F4NO").Value))

                    .Col = colRemarks
                    .Text = IIf(IsDBNull(RsDc.Fields("REMARKS_PURPOSE").Value), "", RsDc.Fields("REMARKS_PURPOSE").Value)

                    SprdMain.Col = ColIncomingItemCode
                    SprdMain.Text = IIf(IsDBNull(RsDc.Fields("INWARD_ITEM_CODE").Value), "", RsDc.Fields("INWARD_ITEM_CODE").Value)

                    .Col = ColJobOrderNo
                    .Text = CStr(IIf(IsDBNull(RsDc.Fields("AUTO_KEY_WO").Value), "", RsDc.Fields("AUTO_KEY_WO").Value))
                    mOrderNo = IIf(IsDBNull(RsDc.Fields("AUTO_KEY_WO").Value), "", RsDc.Fields("AUTO_KEY_WO").Value)

                    If VB.Left(cboPurpose.Text, 1) = "B" Or VB.Left(cboPurpose.Text, 1) = "C" Then
                        If Val(CStr(mOrderNo)) > 0 And Trim(txtServProvided.Text) = "" Then
                            If MainClass.ValidateWithMasterTable(Val(CStr(mOrderNo)), "AUTO_KEY_PO", "SAC_CODE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And PO_STATUS='Y' AND PO_CLOSED='N'") = True Then
                                mPOSACCode = MasterNo
                                If MainClass.ValidateWithMasterTable(mPOSACCode, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                                    txtServProvided.Text = Trim(MasterNo)
                                End If
                            End If
                        End If

                        If Val(CStr(mOrderNo)) > 0 And cboGSTStatus.SelectedIndex = -1 Then
                            If MainClass.ValidateWithMasterTable(Val(CStr(mOrderNo)), "AUTO_KEY_PO", "ISGSTAPPLICABLE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PO_STATUS='Y' AND PO_CLOSED='N'") = True Then
                                mPOChargeApp = MasterNo
                                If mPOChargeApp = "G" Then
                                    cboGSTStatus.SelectedIndex = 0
                                ElseIf mPOChargeApp = "R" Then
                                    cboGSTStatus.SelectedIndex = 1
                                ElseIf mPOChargeApp = "E" Then
                                    cboGSTStatus.SelectedIndex = 2
                                Else
                                    cboGSTStatus.SelectedIndex = 3
                                End If
                            End If
                        End If
                    End If

                    RsDc.MoveNext()
                    cntRow = cntRow + 1
                    .MaxRows = .MaxRows + 1
                Loop
            End If
        End With
        Call CalcTots()
        FormatSprdMain(-1)
        ShowFromRGPDetail = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ShowFromRGPDetail = False
    End Function
    Private Sub CalcTots()
        On Error GoTo ERR1

        Dim cntRow As Integer

        Dim mQty As Double
        Dim mRate As Double
        Dim mItemValue As Double
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mQtyKGs As Double
        Dim mJWUOM As String
        Dim mItemCode As String

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc
                mItemCode = Trim(.Text)

                'mJWUOM = ""
                'If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_JW_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '    mJWUOM = MasterNo
                'End If

                '.Col = ColUOM
                'If Trim(.Text) = mJWUOM Then
                .Col = ColQty
                mQty = Val(.Text)
                'Else
                '    .Col = ColQtyKGs
                '    mQty = Val(.Text)
                'End If

                .Col = ColRate
                mRate = Val(.Text)

                mItemValue = (mQty * mRate)

                .Col = ColAmount
                .Text = CStr(mItemValue)

                .Col = ColCGSTPer
                mCGSTPer = CDbl(VB6.Format(Val(.Text), "0.00"))

                .Col = ColSGSTPer
                mSGSTPer = CDbl(VB6.Format(Val(.Text), "0.00"))

                .Col = ColIGSTPer
                mIGSTPer = CDbl(VB6.Format(Val(.Text), "0.00"))

                mCGSTAmount = CDbl(VB6.Format(mItemValue * mCGSTPer * 0.01, "0.00"))
                mSGSTAmount = CDbl(VB6.Format(mItemValue * mSGSTPer * 0.01, "0.00"))
                mIGSTAmount = CDbl(VB6.Format(mItemValue * mIGSTPer * 0.01, "0.00"))

                .Col = ColCGSTAmount
                .Text = VB6.Format(mCGSTAmount, "0.00")

                .Col = ColSGSTAmount
                .Text = VB6.Format(mSGSTAmount, "0.00")

                .Col = ColIGSTAmount
                .Text = VB6.Format(mIGSTAmount, "0.00")

DontCalc:
            Next cntRow
        End With



        Exit Sub
ERR1:
        'Resume
        If Err.Number = 6 Then Resume Next 'OverFlow
        MsgInformation(Err.Description)
    End Sub
    Public Function AutoChallanNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsMRRMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Double
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(GATEPASS_NO)  " & vbCrLf _
            & " FROM INV_GATEPASS_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_PASSNO,LENGTH(AUTO_KEY_PASSNO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""


        'If Mid(RsCompany.Fields("COMPANY_NAME").Value, 1, 3) = "KAY" Then
        '    SqlStr = SqlStr & vbCrLf & " AND GATEPASS_TYPE='" & lblBookType.Text & "'"
        'End If

        If Trim(txtPrefix.Text) = "" Then
            SqlStr = SqlStr & vbCrLf & " AND (CHALLAN_PREFIX='' OR CHALLAN_PREFIX IS NULL)"
        Else
            SqlStr = SqlStr & vbCrLf & " AND CHALLAN_PREFIX='" & Trim(txtPrefix.Text) & "'"
        End If


        ''ALTER TABLE INV_GATEPASS_HDR ADD (        Constraint INV_GATEPASS_HDR_UN UNIQUE(COMPANY_CODE, CHALLAN_PREFIX, GATEPASS_NO));


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMRRMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = mMaxValue    '' CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With
        AutoChallanNo = mNewSeqNo   ''& VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Public Function AUTO57F4() As Object

        On Error GoTo AutoGenSeqNoErr
        Dim RsMRRMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mStartingNo As Double

        '    If chkAgtPermission.Value = vbUnchecked Then
        mStartingNo = 1
        '    Else
        '        mStartingNo = 100000
        '    End If

        SqlStr = ""
        SqlStr = "SELECT Max(OUTWARD_57F4NO)  " & vbCrLf _
            & " FROM INV_GATEPASS_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_PASSNO,LENGTH(AUTO_KEY_PASSNO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND AGT_Permission='N'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMainGen, ADODB.LockTypeEnum.adLockReadOnly)

        With RsMRRMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mNewSeqNo = .Fields(0).Value
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = mStartingNo
                End If
            Else
                mNewSeqNo = mStartingNo
            End If
        End With
        AUTO57F4 = mNewSeqNo
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function SelectQryForRgp_Nrgp(ByRef mSqlStr As String) As String
        On Error GoTo ErrPart
        Dim pBarCodeString As String = ""
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mInvoicePrintType As String
        Dim CntCount As Integer
        Dim mUpdateStart As Boolean



        mUpdateStart = True
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_BARCODE_PRINT WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        For CntCount = 0 To 5
            If frmPrintInvCopy.chkPrintOption(CntCount).CheckState = System.Windows.Forms.CheckState.Checked Then
                mInvoicePrintType = UCase(frmPrintInvCopy.chkPrintOption(CntCount).Text)
                SqlStr = "INSERT INTO TEMP_BARCODE_PRINT ( " & vbCrLf _
                    & " USER_ID, MKEY, BARCODE_VALUE, PRINT_INVOICE_TYPE,PRINT_SEQ ) VALUES (" & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "','" & Val(txtGatepassno.Text) & "','" & pBarCodeString & "','" & mInvoicePrintType & "'," & CntCount & ")"

                PubDBCn.Execute(SqlStr)
            End If
        Next

        PubDBCn.CommitTrans()

        mUpdateStart = False

        ''SELECT CLAUSE...


        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC, BCMST.*, PREBY.EMP_NAME, BP.*"

        'mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf _
        '    & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf _
        '    & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf _
        '    & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf _
        '    & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf _
        '    & " CMST.LST_NO, PREBY.EMP_NAME"

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_GATEPASS_HDR IH, INV_GATEPASS_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BCMST, INV_ITEM_MST INVMST,PAY_EMPLOYEE_MST PREBY,TEMP_BARCODE_PRINT BP"
        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " IH.AUTO_KEY_PASSNO=ID.AUTO_KEY_PASSNO" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND CMST.COMPANY_CODE=BCMST.COMPANY_CODE" & vbCrLf _
            & " AND CMST.SUPP_CUST_CODE=BCMST.SUPP_CUST_CODE AND BCMST.LOCATION_ID='" & Trim(txtBillTo.Text) & "'" & vbCrLf _
            & " AND IH.COMPANY_CODE=PREBY.COMPANY_CODE(+)" & vbCrLf _
            & " AND IH.PRE_EMP_CODE=PREBY.EMP_CODE(+)" & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.AUTO_KEY_PASSNO=BP.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_PASSNO=" & Val(txtGatepassno.Text) & " AND BP.USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"


        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY BP.PRINT_SEQ,ID.SERIAL_NO"

        SelectQryForRgp_Nrgp = mSqlStr
ErrPart:
        If mUpdateStart = True Then
            PubDBCn.RollbackTrans()
        End If
        SelectQryForRgp_Nrgp = ""
    End Function
    Private Function SelectQryForConsumption(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf & " ID.*,OUTITEM.ITEM_SHORT_DESC,INITEM.ITEM_SHORT_DESC "

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_RGP_OUT_DET ID, " & vbCrLf & " INV_ITEM_MST OUTITEM, INV_ITEM_MST INITEM"
        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " ID.COMPANY_CODE=OUTITEM.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=OUTITEM.ITEM_CODE" & vbCrLf & " AND ID.COMPANY_CODE=INITEM.COMPANY_CODE" & vbCrLf & " AND ID.IN_ITEM_CODE=INITEM.ITEM_CODE" & vbCrLf & " AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.AUTO_KEY_RGPSLIP=" & Val(txtRgpreqno.Text) & ""

        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.TRN_SERIAL_NO, ID.SERIAL_NO"

        SelectQryForConsumption = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef mInvoicePrintType As String)
        'Dim Printer As New Printer

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mJWDetail As String
        Dim mJWDetail1 As String
        Dim mJWDetail2 As String
        Dim mPermissionNo As String
        Dim mStateName As String = ""
        Dim mStateCode As String = ""
        Dim mWithInState As String = ""
        Dim mWithInCountry As String
        Dim mPlaceofSupply As String
        Dim mServiceName As String
        Dim mSAC As String
        Dim mCompanyStateCode As String

        'If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
        '    SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu, "Y")
        'Else
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, , , "Y")
        'End If

        'If MainClass.ValidateWithMasterTable((txtSuppName.Text), "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mStateName = MasterNo
        '    mStateCode = GetStateCode(mStateName)
        'End If

        'If MainClass.ValidateWithMasterTable((txtSuppName.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mWithInState = MasterNo
        'End If

        'If mWithInState = "N" Then
        '    If MainClass.ValidateWithMasterTable((txtSuppName.Text), "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mWithInCountry = MasterNo
        '    End If
        'End If

        mStateName = GetPartyBusinessDetail(Trim(txtSuppcode.Text), Trim(txtBillTo.Text), "SUPP_CUST_STATE")
        mStateCode = GetStateCode(mStateName)
        mWithInState = GetPartyBusinessDetail(Trim(txtSuppcode.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mWithInCountry = GetPartyBusinessDetail(Trim(txtSuppcode.Text), Trim(txtBillTo.Text), "WITHIN_COUNTRY")

        mPlaceofSupply = VB6.Format(mStateCode, "00") & "-" & mStateName '' IIf(mWithInState = "Y", "INTRA STATE", IIf(mWithInCountry = "Y", "INTER STATE", "EXPORT"))

        MainClass.AssignCRptFormulas(Report1, "mStateCode=""" & mStateCode & """")
        MainClass.AssignCRptFormulas(Report1, "mPlaceofSupply=""" & mPlaceofSupply & """")
        'MainClass.AssignCRptFormulas(Report1, "InvoicePrintType=""" & mInvoicePrintType & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyGSTIN=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "COMPANYCINNo=""" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & """")

        mCompanyStateCode = GetStateCode(IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value))
        MainClass.AssignCRptFormulas(Report1, "CompanyStateCode=""" & mCompanyStateCode & """")

        mServiceName = Trim(txtServProvided.Text)
        mSAC = ""

        If MainClass.ValidateWithMasterTable(mServiceName, "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
            mSAC = MasterNo
            mServiceName = mServiceName & IIf(mSAC <> "", " (SAC : " & mSAC & ")", "")
        End If

        MainClass.AssignCRptFormulas(Report1, "Service=""" & mServiceName & """")
        ''
        '    If RsCompany.fields("COMPANY_CODE").value = 1 And chkAgtPermission.Value = vbChecked Then
        '
        '        If CDate(txtGatePassDate.Text) >= CDate("18/03/2016") Then
        '            mPermissionNo = "F.NO.C.NO.CE/Tech/Div-III/GGN-I/Jobwork/23/14-15/549 Dt.17/03/2016"
        '        Else
        '            mPermissionNo = "F.NO.C.NO.CE/Tech/Div-III/GGN-I/Jobwork/23/14-15/569 Dt.26/03/2015"
        '        End If
        '
        '        mJWDetail = "Removed from the premises of job-worker as permitted by the of Assistant/Deputy Commissioner,Central Excise,Division under " & mPermissionNo & ",Rule4(6) of the Cenvat Credit Rules,2004"
        '        mJWDetail1 = "EUROTHERM HEMA RADIATORS INDIA LTD. UNIT-II, PLOT NO. 5 & 14, SECTOR-6, HSIDC INDUSTRIAL COMPLEX, BAWAL (REWARI) HARYANA"
        '        mJWDetail2 = "Central Excise Regn. No.AABCE3677REM002, Range - 48, Division-10, Rewari, Comm. GGN-II"
        '        MainClass.AssignCRptFormulas Report1, "JWDetail=""" & mJWDetail & """"
        '        MainClass.AssignCRptFormulas Report1, "JWDetail1=""" & mJWDetail1 & """"
        '        MainClass.AssignCRptFormulas Report1, "JWDetail2=""" & mJWDetail2 & """"
        '    End If


        Report1.ReportFileName = PubReportFolderPath & mRptFileName '' PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        ''TEmporyOpen 13-05-2020
        '    Report1.WindowShowPrintBtn = False '' IIf(PubSuperUser = "S", True, False)
        '    Report1.WindowShowPrintSetupBtn = False ''IIf(PubSuperUser = "S", True, False)
        '    Report1.WindowShowExportBtn = IIf(PubSuperUser = "S", True, False)

        Report1.WindowShowPrintBtn = True '' IIf(PubSuperUser = "S", True, False)
        Report1.WindowShowPrintSetupBtn = True ''IIf(PubSuperUser = "S", True, False)
        Report1.WindowShowExportBtn = True

        'Dim prt As Printer
        'If PubUniversalPrinter = "Y" And mMode = Crystal.DestinationConstants.crptToPrinter Then

        '    For Each prt In Printers
        '        If UCase(prt.DeviceName) = UCase("Universal Printer") Then
        '            Printer = prt


        '            Report1.PrinterName = prt.DeviceName
        '            Report1.PrinterDriver = prt.DriverName
        '            Report1.PrinterPort = prt.Port

        '            Report1.PrinterSelect()

        '            ''prt.Orientation = vbPRORLandscape
        '            'Report1.PrintFileName = "D:\" & Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & ".pdf"
        '            Exit For
        '        End If
        '    Next prt
        '    ''
        'End If

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub PrintBarcode3(ByRef mVendorCode As String)
        '        On Error GoTo ErrPart
        '        Dim cntRow As Integer

        '        Dim mRefType As String
        '        Dim mRefNo As String
        '        Dim mBillNo As String
        '        Dim mBillDate As String
        '        Dim mST38No As String
        '        Dim mTransportMode As String
        '        Dim mAssessiableAmount As Double
        '        Dim mExciseDuty As Double
        '        Dim mExciseAmount As Double
        '        Dim mDiscount As Double
        '        Dim mDiscountAmount As Double
        '        Dim mTaxableAmt As Double
        '        Dim mSalesTax As Double
        '        Dim mSalesTaxAmount As Double
        '        Dim mFreightAmount As Double
        '        Dim mNetAmount As Double
        '        Dim mFREIGHTCHARGES As String
        '        Dim mFormDetail As String

        '        Dim mItemCode As String
        '        Dim mQty As Double

        '        Dim mString As String = ""
        '        Dim mHeaderString As String
        '        Dim mSeparator As String
        '        Dim mMaxRow As Integer
        '        Dim mEndRow As Integer
        '        Dim mStatRow As Integer
        '        Dim mFirstBarCode As Boolean
        '        Dim mPartNo As String
        '        Dim mDocsThru As String
        '        Dim mVehicle As String

        '        mSeparator = "#"

        '        '    mVendorCode = ""
        '        mRefType = "J"

        '        If Trim(txtChallanNo.Text) <> "" Then
        '            mBillNo = VB6.Format(Trim(txtChallanNo.Text), "00000")
        '        Else
        '            mBillNo = " "
        '        End If

        '        If IsDate(txtGatePassDate.Text) Then
        '            mBillDate = VB6.Format(txtGatePassDate.Text, "DD-MMM-YYYY")
        '        Else
        '            mBillDate = " "
        '        End If

        '        If Trim(txtST38No.Text) <> "" Then
        '            mST38No = Trim(txtST38No.Text)
        '        Else
        '            mST38No = " "
        '        End If

        '        mTransportMode = " "
        '        mAssessiableAmount = CDbl("0")
        '        mExciseDuty = CDbl("0")
        '        mExciseAmount = CDbl("0")
        '        mDiscount = CDbl("0")
        '        mDiscountAmount = CDbl("0")
        '        mTaxableAmt = CDbl("0")
        '        mSalesTax = CDbl("0")
        '        mSalesTaxAmount = CDbl("0")
        '        mFreightAmount = CDbl("0")
        '        mNetAmount = CDbl("0")
        '        mFREIGHTCHARGES = ""
        '        mFormDetail = " "
        '        mVehicle = ""
        '        mDocsThru = ""

        '        mHeaderString = mVendorCode & mSeparator & mRefType & mSeparator & mRefNo & mSeparator & mBillNo & mSeparator & mBillDate & mSeparator & mST38No & mSeparator & mTransportMode & mSeparator & mAssessiableAmount & mSeparator & mExciseDuty & mSeparator & mExciseAmount & mSeparator & mDiscount & mSeparator & mDiscountAmount & mSeparator & mTaxableAmt & mSeparator & mSalesTax & mSeparator & mSalesTaxAmount & mSeparator & mFreightAmount & mSeparator & mNetAmount & mSeparator & mFREIGHTCHARGES & mSeparator & mFormDetail & mSeparator & mVehicle & mSeparator & mDocsThru

        '        mMaxRow = SprdMain.MaxRows
        '        mStatRow = 0
        '        mFirstBarCode = True

        '        Dim pFileName As String
        '        Dim mFP As Boolean
        '        With SprdMain
        '            For mEndRow = mStatRow To mMaxRow Step 10
        '                For cntRow = mEndRow + 1 To mEndRow + 10
        '                    If cntRow = .MaxRows Then GoTo NextRec
        '                    .Row = cntRow

        '                    .Col = ColItemCode
        '                    mItemCode = Trim(.Text)
        '                    '                mPartNo = ""
        '                    '                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '                    '                    mPartNo = MasterNo
        '                    '                End If
        '                    If Trim(mItemCode) <> "" Then
        '                        mString = mString & mSeparator & mItemCode
        '                    Else
        '                        mString = mString & mSeparator & " "
        '                    End If

        '                    .Col = ColQty
        '                    mQty = CDbl(Trim(.Text))
        '                    If Trim(CStr(mQty)) <> "" Then
        '                        mString = mString & mSeparator & mQty
        '                    Else
        '                        mString = mString & mSeparator & "0"
        '                    End If

        '                    .Col = ColRate
        '                    mQty = CDbl(Trim(.Text))
        '                    If Trim(CStr(mQty)) <> "" Then
        '                        mString = mString & mSeparator & mQty
        '                    Else
        '                        mString = mString & mSeparator & "0"
        '                    End If

        '                Next
        'NextRec:


        '                If mVendorCode = "13909" Then
        '                    mString = mHeaderString & mString & vbEnter
        '                    If CreateOutPutFile(mString, "BarCode.PRN") = False Then GoTo ErrPart
        '                    pFileName = mLocalPath & "\BarCode.Prn"
        '                    Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)
        '                    'App.Path & "\RVIEW.EXE "

        '                Else
        '                    mString = IIf(mFirstBarCode = True, "0", "1") & mSeparator & mHeaderString & mString & vbEnter
        '                    If pBARCODEPRINTER = "Y" Then
        '                        Call Print2DBarcode(mString, "Bill No : " & Trim(mBillNo), MSComm1)
        '                    Else
        '                        If CreateOutPutFile(mString, "PDF.DAT") = False Then GoTo ErrPart

        '                        mString = vbTab & vbTab & vbTab & "Bill No : " & Trim(mBillNo) & vbNewLine & vbNewLine & " "
        '                        If CreateOutPutFile(mString, "Inv.Prn") = False Then GoTo ErrPart
        '                        '    Shell mLocalPath & "\PDF.bat",vbNormalFocus
        '                        mFP = Shell(mLocalPath & "\PDF.bat", AppWinStyle.NormalFocus)
        '                        '    mFP = Shell(App.path & "\PDF.bat", vbNormalFocus)
        '                        '' End
        '                    End If
        '                End If
        '                mString = ""
        '                mFirstBarCode = False
        '            Next
        '        End With

        '        '    mString = mString & vbEnter
        '        '
        '        '    If pBARCODEPRINTER = "Y" Then
        '        '        Call Print2DBarcode(mString, "Bill No : " & Trim(mBillNo), MSComm1)
        '        '    Else
        '        '        If CreateOutPutFile(mString, "PDF.DAT") = False Then GoTo ErrPart
        '        '
        '        '        mString = vbTab & vbTab & vbTab & "Bill No : " & Trim(mBillNo) & vbNewLine & vbNewLine & " "
        '        '        If CreateOutPutFile(mString, "Inv.Prn") = False Then GoTo ErrPart
        '        '    '    Shell mLocalPath & "\PDF.bat",vbNormalFocus
        '        '        Dim mFP As Boolean
        '        '        mFP = Shell(mLocalPath & "\PDF.bat", vbNormalFocus)
        '        '    '    mFP = Shell(App.path & "\PDF.bat", vbNormalFocus)
        '        '       '' End
        '        '    End If
        Exit Sub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtBillTo_Validating(sender As Object, e As CancelEventArgs) Handles txtBillTo.Validating
        Dim Cancel As Boolean = e.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String
        Dim RsTemp As ADODB.Recordset
        Dim mAddress As String

        If Trim(txtSuppcode.Text) = "" Then GoTo EventExitSub
        If Trim(txtBillTo.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(txtSuppcode.Text), "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        Else
            MsgBox("Invalid Customer Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        ''SUPP_CUST_TYPE IN ('S','C')

        SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, " & vbCrLf _
            & " SUPP_CUST_STATE, SUPP_CUST_PIN " & vbCrLf _
            & " FROM FIN_SUPP_CUST_BUSINESS_MST " & vbCrLf _
            & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND " & vbCrLf _
            & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtSuppcode.Text)) & "'" & vbCrLf _
            & " AND LOCATION_ID='" & MainClass.AllowSingleQuote((txtBillTo.Text)) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mAddress = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value))
            mAddress = mAddress & ", " & IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
            mAddress = mAddress & ", " & IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)

            txtAddress.Text = mAddress
        Else
            MsgInformation("Invalid Location Id for such Customer")

            txtAddress.Text = ""
            Cancel = True
        End If


        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        e.Cancel = Cancel
    End Sub
    Private Sub TxtGRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtGRNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtGRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtGRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtGRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtGRDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtGRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtGRDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If TxtGRDate.Text = "" Then GoTo EventExitSub
        If IsDate(TxtGRDate.Text) = False Then
            ErrorMsg("Invalid Date", "", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtCarriers_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCarriers.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCarriers_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCarriers.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtCarriers.Text), "FIN_TRANSPORTER_MST", "TRANSPORTER_NAME", "TRANSPORTER_ID", , , SqlStr) = True Then
            txtCarriers.Text = AcName
            txtTransportCode.Text = AcName1
            If txtCarriers.Enabled = True Then txtCarriers.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub txtCarriers_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCarriers.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtCarriers_DoubleClick(txtCarriers, New System.EventArgs())
    End Sub
    Private Sub txtCarriers_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCarriers.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCarriers.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTransportCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTransportCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTransportCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTransportCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTransportCode.Text, "N")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmGatePassGST_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        Frame6.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        'SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))

        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdeWayBill_Click(sender As Object, e As EventArgs) Handles cmdeWayBill.Click
        On Error GoTo ErrPart
        Dim mFilePath As String
        Dim pCDKey As String
        Dim pEFUserName As String
        Dim pEFPassword As String
        Dim pEWBUserName As String
        Dim pEWBPassword As String
        Dim url As String
        Dim pResponseIdText As String
        Dim mBody As String
        Dim pStatus As String

        If Trim(txteWayBillNo.Text) = "" Then
            MsgInformation("Nothing to print.")
            Exit Sub
        End If


        If GetWebTeleWaySetupContents(url, "P", pCDKey, pEFUserName, pEFPassword, pEWBUserName, pEWBPassword, "N") = False Then GoTo ErrPart

        Dim http As Object  ' MSXML2.XMLHTTP60 '' MSXML.xmlhttp
        http = CreateObject("MSXML2.ServerXMLHTTP")
        http.Open("POST", url, False)

        http.setRequestHeader("Content-Type", "application/json")

        ''        .Clear
        ''        .IsArray = False 'Actually the default after Clear.

        ''        .item("GSTIN") = IIf(IsNull(RsCompany!COMPANY_GST_RGN_NO), "", RsCompany!COMPANY_GST_RGN_NO)
        ''        .item("ewbNo") = Trim(txtEWayBillNo.Text)
        ''        .item("Year") = Year(txtBillDate.Text)
        ''        .item("Month") = Month(txtBillDate.Text)
        ''        .item("EFUserName") = pEFUserName
        ''        .item("EFPassword") = pEFPassword
        ''        .item("CDKey") = pCDKey
        ''        .item("EWBUserName") = pEWBUserName
        ''        .item("EWBPassword") = pEWBPassword
        ''        mBody = .JSON

        Dim details As New List(Of EWAYBILLPRN)()

        details.Add(New EWAYBILLPRN() With {
            .GSTIN = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value),
            .ewbNo = Trim(txteWayBillNo.Text),
            .Year = Year(txtGatePassDate.Text),
            .Month = Month(txtGatePassDate.Text),
            .EFUserName = pEFUserName,
            .EFPassword = pEFPassword,
            .CDKey = pCDKey,
            .EWBUserName = pEWBUserName,
            .EWBPassword = pEWBPassword
         })

        Dim mBodyDetail As String = JsonConvert.SerializeObject(details)


        'mBody = "{""Push_Data_List"":"
        'mBody = mBody & """Data"": "
        mBody = mBody & mBodyDetail
        mBody = Replace(mBody, "[", "")
        mBody = Replace(mBody, "]", "")
        'mBody = mBody & "]"
        'mBody = mBody & "}"

        http.Send(mBody)

        Dim pResponseText As String = http.responseText


        If pResponseText <> "" Then
            Process.Start("explorer.exe", pResponseText)
        End If

        Exit Sub
ErrPart:
        ''Resume
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
        'PubDBCn.RollbackTrans()
    End Sub
    Private Sub txtDistance_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDistance.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDistance_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDistance.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Public Function GetItemDescription(ByRef ItemCode As String) As String
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCatPreFix As String = "'"
        Dim mSubCatPreFix As String = ""
        Dim mDescription As String = ""
        Dim mItemPrefix As String = ""
        Dim mMaxCode As String = ""

        Dim mSuppCustCode As String = ""

        SqlStr = "SELECT NVL(WO_DESCRIPTION, '') AS DESCRIPTION FROM PUR_PURCHASE_DET  Where ITEM_CODE='" & ItemCode & "' Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = True Then
            GetItemDescription = ""
        Else
            mDescription = ""
            If IsDBNull(RsTemp.Fields("DESCRIPTION").Value) Then
                mDescription = ""
            Else
                mDescription = RsTemp.Fields("DESCRIPTION").Value
            End If
            GetItemDescription = mDescription
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetItemDescription = ""
    End Function

End Class
