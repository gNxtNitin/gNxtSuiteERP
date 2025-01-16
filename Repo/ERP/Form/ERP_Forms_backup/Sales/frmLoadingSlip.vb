Option Strict Off
Option Explicit On
Imports System.ComponentModel
Imports AxFPSpreadADO
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLoadingSlip
    Inherits System.Windows.Forms.Form
    Dim RsLoadMain As ADODB.Recordset
    Dim RsLoadDetail As ADODB.Recordset
    Dim RsLoadDetailOth As ADODB.Recordset
    'Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Dim CurMKey As String
    Dim SqlStr As String = ""

    Private Const ConRowHeight As Short = 15

    Private Const ColRefType As Short = 1
    Private Const ColBillCheck As Short = 2
    Private Const ColRefNo As Short = 3
    Private Const ColRefDate As Short = 4
    Private Const ColCustName As Short = 5
    Private Const ColItemCode As Short = 6
    Private Const ColItemDesc As Short = 7
    Private Const ColItemPartNo As Short = 8
    Private Const ColUOM As Short = 9
    Private Const ColItemWt As Short = 10
    Private Const ColQty As Short = 11
    Private Const ColAmount As Short = 12
    Private Const ColStdPack As Short = 13
    Private Const ColPacks As Short = 14
    Private Const ColPackType As Short = 15
    Private Const ColPackRecd As Short = 16
    Private Const ColPackScanned As Short = 17


    Private Const ColRefNoOth As Short = 1
    Private Const ColRefDateOth As Short = 2
    Private Const ColCustNameOth As Short = 3
    Private Const ColItemDescOth As Short = 4
    Private Const ColQtyOth As Short = 5
    Private Const ColAmountOth As Short = 6
    Private Const ColPacksOth As Short = 7
    Private Const ColItemWtOth As Short = 8
    Private Const ColPackTypeOth As Short = 9

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Dim mSearchKey As String
    Dim cntSearchRow As Long
    Dim cntSearchCol As Long


    Dim pMenu As String

    Private Sub chkAckReceipt_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAckReceipt.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkThirdParty_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkThirdParty.CheckStateChanged

        If lblBookType.Text = "U" Then
            If chkThirdParty.CheckState = System.Windows.Forms.CheckState.Checked Then
                cmdSearchTrip.Enabled = False
                txtCSlipNo.Enabled = False
                txtTripNo.Enabled = True
                cmdsearch.Enabled = True
                txtVehicleNo.Enabled = True
                txtCSlipNo.Text = ""
                txtTripNo.Text = ""
                txtVehicleNo.Text = ""

                txtTransporterName.Enabled = True
                txtVehicleType.Enabled = True

            Else
                'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 1 Then
                '    cmdSearchTrip.Enabled = True
                '    txtCSlipNo.Enabled = True
                '    txtVehicleNo.Enabled = False
                '    txtTripNo.Enabled = False
                '    cmdsearch.Enabled = False
                '    txtTripNo.Text = ""
                '    txtCSlipNo.Text = ""
                '    txtTripNo.Text = ""
                '    txtVehicleNo.Text = ""

                '    txtTransporterName.Enabled = False
                '    txtVehicleType.Enabled = False
                'Else
                cmdSearchTrip.Enabled = False
                txtCSlipNo.Enabled = False
                txtTripNo.Enabled = True
                cmdsearch.Enabled = True
                txtVehicleNo.Enabled = True
                txtCSlipNo.Text = ""
                txtTripNo.Text = ""
                txtVehicleNo.Text = ""

                txtTransporterName.Enabled = True
                txtVehicleType.Enabled = True
                'End If
            End If
        End If
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkWOCollection_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkWOCollection.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtSlipNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsLoadMain.EOF = False Then RsLoadMain.MoveFirst()
            Show1()
            '        ShowInvoiceData
            txtSlipNo.Enabled = True
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click

        On Error GoTo DelErrPart
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mRefNo As Double
        Dim mRefDate As String
        Dim mRefType As String

        If ValidateBranchLocking((txtSlipDate.Text)) = True Then
            Exit Sub
        End If

        If chkAckReceipt.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Acknowledgement Receipt, So cann't be deleted.")
            Exit Sub
        End If
        If txtSlipNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsLoadMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "DSP_LOADING_HDR", (txtSlipNo.Text), RsLoadMain, "", "D") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "DSP_LOADING_HDR", "AUTO_KEY_LOAD", (txtSlipNo.Text)) = False Then GoTo DelErrPart

                If lblBookType.Text = "U" Then
                    SqlStr = " UPDATE DSP_TRIP_HDR SET STATUS='O'," & vbCrLf _
                        & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                        & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                        & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND AUTO_KEY_TRIP=" & Val(txtCSlipNo.Text) & ""

                    PubDBCn.Execute(SqlStr)
                End If

                If lblBookType.Text = "L" Then
                    For cntRow = 1 To SprdMain.MaxRows - 1
                        SprdMain.Row = cntRow
                        SprdMain.Col = ColRefNo
                        mRefNo = Val(SprdMain.Text)

                        SprdMain.Col = ColRefDate
                        mRefDate = SprdMain.Text

                        SprdMain.Col = ColRefType
                        mRefType = Trim(SprdMain.Text)

                        If CDate(mRefDate) >= CDate(PubGSTApplicableDate) Then
                            If mRefType = "D" Then
                                SqlStr = " UPDATE DSP_DESPATCH_HDR SET DESP_STATUS=0," & vbCrLf _
                                    & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                                    & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                                    & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                    & " AND AUTO_KEY_DESP=" & Val(CStr(mRefNo)) & ""

                                PubDBCn.Execute(SqlStr)
                            End If
                        End If
                    Next
                End If

                PubDBCn.Execute("DELETE FROM DSP_LOADING_OTH_DET WHERE AUTO_KEY_LOAD=" & Val(txtSlipNo.Text) & "")
                PubDBCn.Execute("DELETE FROM DSP_LOADING_DET WHERE AUTO_KEY_LOAD=" & Val(txtSlipNo.Text) & "")
                PubDBCn.Execute("DELETE FROM DSP_LOADING_HDR WHERE AUTO_KEY_LOAD=" & Val(txtSlipNo.Text) & "")

                PubDBCn.CommitTrans()
                RsLoadMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        RsLoadMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsLoadMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtSlipNo.Enabled = False
            If txtInDateTime.Text = "__/__/____ __:__" Or txtInDateTime.Text = "" Or Not IsDate(txtInDateTime.Text) Then
                txtInDateTime.Enabled = True
            End If
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart

        Dim SqlStrSub As String
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, False, pMenu)
        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False


        SqlStrSub = " SELECT SERIAL_NO,REF_NO,REF_DATE, SUPP_CUST_NAME,ITEM_SHORT_DESC,PACKED_QTY, NO_OF_PACKETS,ITEM_WT,PACK_TYPE , ITEM_AMT" & vbCrLf _
                & " FROM DSP_LOADING_OTH_DET " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND AUTO_KEY_LOAD='" & MainClass.AllowSingleQuote(txtSlipNo.Text) & "'"



        SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SERIAL_NO"

        Report1.SubreportToChange = Report1.GetNthSubreportName(0)
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStrSub

        Report1.SubreportToChange = ""

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowTermsReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & mRPTName
        'Report1.SQLQuery = mSqlStr
        'Report1.WindowShowGroupTree = False
        Report1.Action = 1

    End Sub

    Private Sub cmdPopulateSuppBill_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulateSuppBill.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim xLoadingNo As Double
        Dim mRefType As String = ""
        Dim mRejDocType As String
        Dim mApplicableDate As String

        mRejDocType = IIf(IsDBNull(RsCompany.Fields("REJECTION_DOCTYPE").Value), "D", RsCompany.Fields("REJECTION_DOCTYPE").Value)
        mApplicableDate = IIf(IsDBNull(RsCompany.Fields("REJ_APPLICABLEDATE").Value), "", RsCompany.Fields("REJ_APPLICABLEDATE").Value)

        If Trim(txtRefNo.Text) = "" Then Exit Sub

        If optShow(0).Checked = True Then
            mRefType = "I"
        ElseIf optShow(1).Checked = True Then
            mRefType = "R"
        ElseIf optShow(2).Checked = True Then
            mRefType = "M"
        ElseIf optShow(3).Checked = True Then
            mRefType = "G"
        ElseIf optShow(4).Checked = True Then
            mRefType = "D"
        End If

        If mRefType = "D" Then
            If mRejDocType = "D" Or mApplicableDate = "" Then

            Else
                MsgBox("Please Made the Invoice for this Despatch Note.")
                Exit Sub
            End If
        End If

        If DuplicateDatainGrid(txtRefNo.Text, mRefType, "") = True Then
            MsgBox("Duplicate Ref No.")
            Exit Sub
        End If

        'If AlreadyLoad(Trim(txtRefNo.Text), mRefType, xLoadingNo) = True Then
        '    MsgBox("Already made a Loading Slip of Such Ref No. Loading Slip No : " & xLoadingNo)
        '    Exit Sub
        'End If
        ''IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''& " AND  IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND

        If optShow(0).Checked = True Then
            SqlStr = " SELECT 'I' AS REF_TYPE, IH.BILLNO As REF_NO, IH.INVOICE_DATE AS REF_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, " & vbCrLf _
                & " IMST.ITEM_SHORT_DESC, ID.ITEM_UOM, ID.CUSTOMER_PART_NO, SUM(ITEM_QTY) AS ITEM_QTY, IMST.PACK_STD, IMST.ITEM_WEIGHT, VEHICLENO , CARRIERS,INNER_PACK_QTY, ID.PACK_TYPE, SUM(ITEM_AMT) AS ITEM_AMT" & vbCrLf _
                & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
                & " AND ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
                & " AND IH.BILLNO='" & (txtRefNo.Text) & "'" & vbCrLf _
                & " GROUP BY IH.BILLNO, IH.INVOICE_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, ID.ITEM_UOM, ID.CUSTOMER_PART_NO,IMST.PACK_STD,IMST.ITEM_WEIGHT,VEHICLENO , CARRIERS, INNER_PACK_QTY, ID.PACK_TYPE" & vbCrLf _
                & " ORDER BY ID.ITEM_CODE"
        ElseIf optShow(1).Checked = True Then
            SqlStr = " SELECT 'R' AS REF_TYPE, IH.AUTO_KEY_PASSNO As REF_NO, IH.GATEPASS_DATE AS REF_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, SUM(AMOUNT) AS ITEM_AMT," & vbCrLf _
                & " IMST.ITEM_SHORT_DESC, ID.ITEM_UOM, IMST.CUSTOMER_PART_NO, SUM(ITEM_QTY) AS ITEM_QTY,IMST.PACK_STD, IMST.ITEM_WEIGHT, '' AS VEHICLENO , '' AS CARRIERS, '' AS INNER_PACK_QTY, '' AS PACK_TYPE" & vbCrLf _
                & " FROM INV_GATEPASS_HDR IH, INV_GATEPASS_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST" & vbCrLf _
                & " WHERE IH.AUTO_KEY_PASSNO=ID.AUTO_KEY_PASSNO" & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
                & " AND ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
                & " AND IH.AUTO_KEY_PASSNO=" & Val(txtRefNo.Text) & "" & vbCrLf _
                & " GROUP BY IH.AUTO_KEY_PASSNO, IH.GATEPASS_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, IMST.ITEM_SHORT_DESC,IMST.CUSTOMER_PART_NO, ID.ITEM_UOM,IMST.PACK_STD,IMST.ITEM_WEIGHT" & vbCrLf _
                & " ORDER BY ID.ITEM_CODE"

        ElseIf optShow(2).Checked = True Then
            SqlStr = " SELECT 'M' AS REF_TYPE, IH.AUTO_KEY_MRR As REF_NO, IH.MRR_DATE AS REF_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE,  SUM(ITEM_RATE * BILL_QTY) AS ITEM_AMT," & vbCrLf _
                & " IMST.ITEM_SHORT_DESC, IMST.CUSTOMER_PART_NO, ID.ITEM_UOM, SUM(BILL_QTY) AS ITEM_QTY,IMST.PACK_STD, IMST.ITEM_WEIGHT, '' AS VEHICLENO , '' AS CARRIERS, '' AS INNER_PACK_QTY, '' AS PACK_TYPE" & vbCrLf _
                & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST" & vbCrLf _
                & " WHERE IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
                & " AND ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
                & " AND IH.AUTO_KEY_MRR=" & Val(txtRefNo.Text) & "" & vbCrLf _
                & " GROUP BY IH.AUTO_KEY_MRR, IH.MRR_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, IMST.CUSTOMER_PART_NO,ID.ITEM_UOM,IMST.PACK_STD,IMST.ITEM_WEIGHT" & vbCrLf _
                & " ORDER BY ID.ITEM_CODE"

        ElseIf optShow(3).Checked = True Then
            SqlStr = " SELECT 'G' AS REF_TYPE, IH.AUTO_KEY_GATE As REF_NO, IH.GATE_DATE AS REF_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE,  SUM(ITEM_RATE * BILL_QTY) AS ITEM_AMT," & vbCrLf _
                & " IMST.ITEM_SHORT_DESC, IMST.CUSTOMER_PART_NO,ID.ITEM_UOM, SUM(BILL_QTY) AS ITEM_QTY,IMST.PACK_STD, IMST.ITEM_WEIGHT" & vbCrLf _
                & " FROM INV_GATEENTRY_HDR IH, INV_GATEENTRY_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST, '' AS VEHICLENO , '' AS CARRIERS, '' AS INNER_PACK_QTY, '' AS PACK_TYPE" & vbCrLf _
                & " WHERE IH.AUTO_KEY_GATE=ID.AUTO_KEY_GATE" & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
                & " AND ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
                & " AND IH.AUTO_KEY_GATE=" & Val(txtRefNo.Text) & "" & vbCrLf _
                & " GROUP BY IH.AUTO_KEY_GATE, IH.GATE_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, IMST.CUSTOMER_PART_NO,ID.ITEM_UOM,IMST.PACK_STD,IMST.ITEM_WEIGHT" & vbCrLf _
                & " ORDER BY ID.ITEM_CODE"

        ElseIf optShow(4).Checked = True Then
            SqlStr = " SELECT 'D' AS REF_TYPE, IH.AUTO_KEY_DESP As REF_NO, IH.DESP_DATE AS REF_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, 0 AS ITEM_AMT," & vbCrLf _
                & " IMST.ITEM_SHORT_DESC, IMST.CUSTOMER_PART_NO,ID.ITEM_UOM, SUM(PACKED_QTY) AS ITEM_QTY,IMST.PACK_STD, IMST.ITEM_WEIGHT, '' AS VEHICLENO , '' AS CARRIERS, '' AS INNER_PACK_QTY, '' AS PACK_TYPE" & vbCrLf _
                & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST" & vbCrLf _
                & " WHERE IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP" & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
                & " AND ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
                & " AND IH.AUTO_KEY_DESP=" & Val(txtRefNo.Text) & " AND IH.DESP_TYPE IN ('Q','L') AND DESP_STATUS=0"

            If CDate(VB6.Format(txtSlipDate.Text, "DD/MM/YYYY")) >= CDate(PubGSTApplicableDate) Then
                SqlStr = SqlStr & vbCrLf & " AND IH.DESP_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND 1=2"
            End If

            SqlStr = SqlStr & vbCrLf & " GROUP BY IH.AUTO_KEY_DESP, IH.DESP_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, IMST.CUSTOMER_PART_NO,ID.ITEM_UOM,IMST.PACK_STD,IMST.ITEM_WEIGHT" & vbCrLf & " ORDER BY ID.ITEM_CODE"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        I = SprdMain.MaxRows

        If RsTemp.EOF = False Then
            If Trim(txtVehicleNo.Text) = "" Then
                txtVehicleNo.Text = IIf(IsDBNull(RsTemp.Fields("VEHICLENO").Value), "", RsTemp.Fields("VEHICLENO").Value)
            End If

            If Trim(txtTransporterName.Text) = "" Then
                txtTransporterName.Text = IIf(IsDBNull(RsTemp.Fields("CARRIERS").Value), "", RsTemp.Fields("CARRIERS").Value)
            End If

            Do While RsTemp.EOF = False
                SprdMain.Row = I

                SprdMain.Col = ColRefType
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("REF_TYPE").Value), "", RsTemp.Fields("REF_TYPE").Value)

                SprdMain.Col = ColBillCheck
                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)

                SprdMain.Col = ColRefNo
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("REF_NO").Value), "", RsTemp.Fields("REF_NO").Value)

                SprdMain.Col = ColRefDate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value), "DD/MM/YYYY")


                SprdMain.Col = ColCustName
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)

                SprdMain.Col = ColItemCode
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                SprdMain.Col = ColItemDesc
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)

                SprdMain.Col = ColItemPartNo
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value)

                SprdMain.Col = ColUOM
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)

                SprdMain.Col = ColItemWt
                SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_WEIGHT").Value), "", RsTemp.Fields("ITEM_WEIGHT").Value), "0.00")

                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)))

                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("ITEM_AMT").Value), 0, RsTemp.Fields("ITEM_AMT").Value)))

                SprdMain.Col = ColStdPack
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("PACK_STD").Value), 0, RsTemp.Fields("PACK_STD").Value)))

                SprdMain.Col = ColPacks
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("INNER_PACK_QTY").Value), 0, RsTemp.Fields("INNER_PACK_QTY").Value)))

                SprdMain.Col = ColPackType
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("PACK_TYPE").Value), "", RsTemp.Fields("PACK_TYPE").Value)


                RsTemp.MoveNext()
                SprdMain.MaxRows = SprdMain.MaxRows + 1
                I = SprdMain.MaxRows
            Loop
        End If

        FormatSprdMain(-1)
        txtRefNo.Text = ""
        CalcTots()

        'If lblBookType.Text = "L" Then
        '    txtTripAmount.Text = CStr(GetVehicleRate(txtVehicleNo.Text, "T", IIf(optFreightType(0).Checked = True, "R", "P")))
        '    txtOthCharges.Text = CStr(GetVehicleRate(txtVehicleNo.Text, "P", IIf(optFreightType(0).Checked = True, "R", "P")))
        'End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function AlreadyLoad(ByRef pRefNo As String, ByRef pRefType As String, ByRef pLoadingNo As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer

        AlreadyLoad = False
        SqlStr = " SELECT DISTINCT IH.AUTO_KEY_LOAD" & vbCrLf _
            & " FROM DSP_LOADING_HDR IH, DSP_LOADING_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_LOAD=ID.AUTO_KEY_LOAD" & vbCrLf _
            & " AND ID.REF_TYPE='" & pRefType & "'" & vbCrLf _
            & " AND ID.REF_NO='" & pRefNo & "'"

        If Val(txtSlipNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_LOAD<>" & Val(txtSlipNo.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            pLoadingNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_LOAD").Value), 0, RsTemp.Fields("AUTO_KEY_LOAD").Value)
            AlreadyLoad = True
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function DuplicateDatainGrid(ByRef mCheckRefNo As String, ByRef mCheckRefType As String, ByRef mCheckItemCode As String) As Boolean
        Dim cntRow As Integer
        'Dim mCheckRefNo As String
        Dim mRefNo As String
        Dim mRefType As String
        Dim mItemCode As String

        DuplicateDatainGrid = False
        If Trim(mCheckRefNo) = "" Then Exit Function

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColRefType
                mRefType = Trim(.Text)

                .Col = ColRefNo
                mRefNo = Trim(.Text)

                If mCheckItemCode = "" Then
                    .Col = ColItemCode
                    mItemCode = ""
                Else
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)
                End If


                .Col = ColItemCode
                If mCheckRefType & mCheckRefNo & mCheckItemCode = mRefType & mRefNo & mItemCode Then
                    DuplicateDatainGrid = True
                    Exit For
                End If
            Next
        End With
    End Function
    Private Function GetLineNo_ItemPartNo(ByRef mCheckRefNo As String, ByRef mCheckRefType As String, ByRef mCheckItemCode As String) As Long
        Dim cntRow As Integer
        'Dim mCheckRefNo As String
        Dim mRefNo As String
        Dim mRefType As String
        Dim mItemCode As String

        GetLineNo_ItemPartNo = 0
        If Trim(mCheckRefNo) = "" Then Exit Function

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColRefType
                mRefType = Trim(.Text)

                .Col = ColRefNo
                mRefNo = Trim(.Text)


                .Col = ColItemPartNo
                mItemCode = Trim(.Text)

                If mCheckRefType & mCheckRefNo & mCheckItemCode = mRefType & mRefNo & mItemCode Then
                    GetLineNo_ItemPartNo = cntRow
                    Exit For
                End If
            Next
        End With
    End Function
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonDespatch(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonDespatch(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call CalcTots()

        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True And CmdAdd.Visible = True Then CmdAdd.Focus()
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
    Private Sub ReportonDespatch(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mReportPrint As Boolean

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""

        Call MainClass.ClearCRptFormulas(Report1)

        Call SelectQryForDespatch(SqlStr)


        mTitle = IIf(lblBookType.Text = "L", "LOADING SLIP", "UNLOADING SLIP")
        mSubTitle = ""
        mRptFileName = "LoadingSlip.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)



        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryForDespatch(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf _
            & " IH.*, ID.*, INVMST.ITEM_WEIGHT "

        ''FROM CLAUSE...

        mSqlStr = mSqlStr & vbCrLf _
            & " FROM DSP_LOADING_HDR IH, DSP_LOADING_DET ID, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.AUTO_KEY_LOAD=ID.AUTO_KEY_LOAD" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.AUTO_KEY_LOAD=" & Val(txtSlipNo.Text) & ""

        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForDespatch = mSqlStr
    End Function
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim I As Short
        Dim SqlStr As String = ""
        Dim nMkey As String
        Dim mVNoSeq As Double
        Dim mDivisionCode As Double
        Dim mEntryDate As String
        Dim mTPVehicle As String
        Dim mAckReceipt As String
        Dim mAckReceiptDate As String
        Dim mWOCollection As String
        Dim mInDateTime As String
        Dim mRefNo As String
        Dim mRefDate As String
        Dim mRefType As String
        Dim cntRow As Integer

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        With SprdMain
            .Row = 1

            .Col = ColRefNo
            mRefNo = .Text

            .Col = ColRefType
            mRefType = Trim(.Text)
        End With

        ''COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "
        If mRefType = "I" Then
            If MainClass.ValidateWithMasterTable(mRefNo, "BILLNO", "DIV_CODE", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
        ElseIf mRefType = "M" Then
            If MainClass.ValidateWithMasterTable(mRefNo, "AUTO_KEY_MRR", "DIV_CODE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
        ElseIf mRefType = "G" Then
            If MainClass.ValidateWithMasterTable(mRefNo, "AUTO_KEY_GATE", "DIV_CODE", "INV_GATEENTRY_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
        ElseIf mRefType = "R" Then
            If MainClass.ValidateWithMasterTable(mRefNo, "AUTO_KEY_PASSNO", "DIV_CODE", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
        ElseIf mRefType = "D" Then
            If MainClass.ValidateWithMasterTable(mRefNo, "AUTO_KEY_DESP", "DIV_CODE", "DSP_DESPATCH_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
        End If

        'If RsCompany.Fields("FYEAR").Value = GetCurrentFYNo(PubDBCn, VB6.Format(PubCurrDate, "DD/MM/YYYY")) Then
        '    mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime()
        'Else
        mEntryDate = VB6.Format(txtSlipDate.Text, "DD-MMM-YYYY HH:MM")
        'End If


        '    If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mDivisionCode = Trim(MasterNo)
        '    End If

        If Val(txtSlipNo.Text) = 0 Then
            mVNoSeq = CDbl(AutoGenSeqNo())
        Else
            mVNoSeq = Val(txtSlipNo.Text)
        End If

        txtSlipNo.Text = CStr(Val(CStr(mVNoSeq)))

        SqlStr = ""

        mTPVehicle = IIf(chkThirdParty.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAckReceipt = IIf(chkAckReceipt.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mWOCollection = IIf(chkWOCollection.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If mAckReceipt = "Y" Or IsDate(txtAckDate.Text) = True Then      '' txtAckDate.Text <> "__/__/____ __:__" Then
            mAckReceiptDate = VB6.Format(txtAckDate.Text, "DD-MMM-YYYY HH:MM")
            mAckReceipt = "Y"
        Else
            mAckReceiptDate = ""
        End If

        If IsDate(txtInDateTime.Text) Then
            mInDateTime = VB6.Format(txtInDateTime.Text, "DD-MMM-YYYY HH:MM")
        Else
            mInDateTime = ""
        End If

        If ADDMode = True Then
            lblMKey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO DSP_LOADING_HDR( " & vbCrLf _
                & " COMPANY_CODE, AUTO_KEY_LOAD, SLIP_DATE," & vbCrLf _
                & " CSLIP_NO, CSLIP_DATE, VEHICLE_NO," & vbCrLf _
                & " TRIP_NO, TRIP_DATE, " & vbCrLf _
                & " TRANSPORTER_NAME, VEHICLE_TYPE, REMARKS,TOT_QTY, TOT_PACK,IS_TP_VEHICLE," & vbCrLf _
                & " ADDUSER, ADDDATE, " & vbCrLf _
                & " MODUSER, MODDATE,BOOKTYPE,IS_ACK_RECEIPT, " & vbCrLf _
                & " ACK_RECEIPTDATE,WO_COLLECTION,TOT_BILLS, TOT_PENDING_BILLS, " & vbCrLf _
                & " TRIP_AMOUNT, OTH_AMOUNT, TOLL_AMOUNT, NET_AMOUNT, GR_NO, GR_DATE, DIV_CODE, FREIGHT_TYPE,GROSS_WT, TEAR_WT,NET_WT) "

            SqlStr = SqlStr & vbCrLf _
                & " VALUES(" & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(CStr(mVNoSeq)) & ", TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                & " " & Val(txtCSlipNo.Text) & ", TO_DATE('" & VB6.Format(txtCSlipDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtVehicleNo.Text)) & "', " & vbCrLf _
                & " " & Val(txtTripNo.Text) & ", TO_DATE('" & VB6.Format(txtTripDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtTransporterName.Text)) & "', '" & MainClass.AllowSingleQuote((txtVehicleType.Text)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', " & Val(lblTotItemQty.Text) & ", " & Val(lblPacket.Text) & ",'" & mTPVehicle & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'',''," & vbCrLf _
                & " '" & lblBookType.Text & "','N','','" & mWOCollection & "'," & vbCrLf _
                & " " & Val(txtTotBills.Text) & "," & Val(txtTotPendingBills.Text) & "," & vbCrLf _
                & " " & Val(txtTripAmount.Text) & ", " & Val(txtOthCharges.Text) & ", " & Val(txtTollTax.Text) & "," & Val(txtNetAmount.Text) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtGRNo.Text)) & "',TO_DATE('" & VB6.Format(txtGRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mDivisionCode & ",'" & IIf(optFreightType(0).Checked = True, "R", "P") & "'," & vbCrLf _
                & " " & Val(txtGrossWt.Text) & "," & Val(txtTearWt.Text) & "," & Val(lblNetWt.Text) & ")"

        ElseIf MODIFYMode = True Then

            SqlStr = ""
            SqlStr = "UPDATE DSP_LOADING_HDR SET SLIP_DATE=TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                & " AUTO_KEY_LOAD =" & Val(CStr(mVNoSeq)) & " ,DIV_CODE = " & mDivisionCode & "," & vbCrLf _
                & " IS_TP_VEHICLE='" & mTPVehicle & "'," & vbCrLf & " CSLIP_NO =" & Val(txtCSlipNo.Text) & " ," & vbCrLf _
                & " CSLIP_DATE=TO_DATE('" & VB6.Format(txtCSlipDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " TRIP_NO =" & Val(txtTripNo.Text) & " ," & vbCrLf _
                & " TRIP_DATE=TO_DATE('" & VB6.Format(txtTripDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " TRANSPORTER_NAME='" & MainClass.AllowSingleQuote((txtTransporterName.Text)) & "', " & vbCrLf _
                & " GR_NO ='" & MainClass.AllowSingleQuote((txtGRNo.Text)) & "' ," & vbCrLf _
                & " GR_DATE =TO_DATE('" & VB6.Format(txtGRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " VEHICLE_NO='" & MainClass.AllowSingleQuote((txtVehicleNo.Text)) & "', " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', VEHICLE_TYPE='" & MainClass.AllowSingleQuote((txtVehicleType.Text)) & "'," & vbCrLf _
                & " TOT_QTY=" & Val(lblTotItemQty.Text) & ", TOT_PACK=" & Val(lblPacket.Text) & "," & vbCrLf _
                & " TRIP_AMOUNT=" & Val(txtTripAmount.Text) & "," & vbCrLf _
                & " OTH_AMOUNT=" & Val(txtOthCharges.Text) & "," & vbCrLf _
                & " TOLL_AMOUNT=" & Val(txtTollTax.Text) & "," & vbCrLf _
                & " NET_AMOUNT=" & Val(txtNetAmount.Text) & "," & vbCrLf _
                & " BOOKTYPE='" & lblBookType.Text & "', FREIGHT_TYPE='" & IIf(optFreightType(0).Checked = True, "R", "P") & "', " & vbCrLf _
                & " IS_ACK_RECEIPT='" & mAckReceipt & "',WO_COLLECTION='" & mWOCollection & "'," & vbCrLf _
                & " ACK_RECEIPTDATE=TO_DATE('" & mAckReceiptDate & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
                & " IN_DATE_TIME=TO_DATE('" & mInDateTime & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
                & " TOT_BILLS =" & Val(txtTotBills.Text) & ", TOT_PENDING_BILLS =" & Val(txtTotPendingBills.Text) & " ," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                & " GROSS_WT=" & Val(txtGrossWt.Text) & ",TEAR_WT=" & Val(txtTearWt.Text) & ",NET_WT=" & Val(lblNetWt.Text) & "" & vbCrLf _
                & " WHERE AUTO_KEY_LOAD ='" & MainClass.AllowSingleQuote((lblMKey.Text)) & "'"
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(Val(CStr(mVNoSeq))) = False Then GoTo ErrPart
        If UpdateDetailOth1(Val(CStr(mVNoSeq))) = False Then GoTo ErrPart

        If lblBookType.Text = "U" Then
            SqlStr = " UPDATE DSP_TRIP_HDR SET STATUS='C'," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND AUTO_KEY_TRIP=" & Val(txtCSlipNo.Text) & ""

            PubDBCn.Execute(SqlStr)
        End If

        'If RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 3 Then
        For cntRow = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = cntRow
            SprdMain.Col = ColRefNo
            mRefNo = (SprdMain.Text)

            SprdMain.Col = ColRefType
            mRefType = Trim(SprdMain.Text)

            ''COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            '' & " AND 

            If mRefType = "I" Then
                SqlStr = " UPDATE FIN_INVOICE_HDR SET VEHICLENO='" & MainClass.AllowSingleQuote(txtVehicleNo.Text) & "'," & vbCrLf _
                    & " GRNO ='" & MainClass.AllowSingleQuote((txtGRNo.Text)) & "' ," & vbCrLf _
                    & " GRDATE =TO_DATE('" & VB6.Format(txtGRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                    & " WHERE BILLNO='" & Trim(mRefNo) & "' AND (GRNO='' OR GRNO IS NULL)"

                PubDBCn.Execute(SqlStr)
            End If
        Next
        'End If


        If lblBookType.Text = "L" Then
            For cntRow = 1 To SprdMain.MaxRows - 1
                SprdMain.Row = cntRow
                SprdMain.Col = ColRefNo
                mRefNo = (SprdMain.Text)

                SprdMain.Col = ColRefDate
                mRefDate = VB6.Format(SprdMain.Text, "DD/MM/YYYY")

                SprdMain.Col = ColRefType
                mRefType = Trim(SprdMain.Text)

                'If CDate(mRefDate) >= CDate(PubGSTApplicableDate) Then
                If mRefType = "D" Then
                    SqlStr = " UPDATE DSP_DESPATCH_HDR SET DESP_STATUS=1," & vbCrLf _
                        & " GRNO ='" & MainClass.AllowSingleQuote((txtGRNo.Text)) & "' ," & vbCrLf _
                        & " GRDATE =TO_DATE('" & VB6.Format(txtGRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                        & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                        & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND AUTO_KEY_DESP=" & Val(mRefNo) & ""

                    PubDBCn.Execute(SqlStr)
                End If
                'End If
            Next
        End If

        UpdateMain1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsLoadMain.Requery() ''.Refresh
        RsLoadDetail.Requery() ''.Refresh
        RsLoadDetailOth.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Load No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume
    End Function
    Private Sub CalcTots()
        On Error GoTo ERR1

        Dim cntRow As Integer

        Dim mQty As Double
        Dim mPack As Double
        Dim mNosOfBill As Integer
        Dim mCountNosOfBill As Integer
        Dim mRefNo As String
        Dim mPendingNosOfBill As Integer
        Dim mRefNoStr As String
        Dim mTripRate As Double


        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColBillCheck
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then

                    .Col = ColItemCode
                    If .Text = "" Then GoTo DontCalc

                    .Col = ColQty
                    mQty = mQty + Val(.Text)

                    .Col = ColPacks
                    mPack = mPack + Val(.Text)
                End If

DontCalc:
            Next cntRow
        End With

        cntRow = 1
        With SprdMainOth
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColRefNoOth
                If .Text = "" Then GoTo DontCalcOth

                .Col = ColQtyOth
                mQty = mQty + Val(.Text)

                .Col = ColPacksOth
                mPack = mPack + Val(.Text)

DontCalcOth:
            Next cntRow
        End With


        lblTotItemQty.Text = VB6.Format(mQty, "0.00")
        lblPacket.Text = VB6.Format(mPack, "0.00")

        txtTripAmount.Text = VB6.Format(Val(txtTripAmount.Text), "0.00")
        txtOthCharges.Text = VB6.Format(Val(txtOthCharges.Text), "0.00")
        txtTollTax.Text = VB6.Format(Val(txtTollTax.Text), "0.00")

        txtNetAmount.Text = VB6.Format(Val(txtTripAmount.Text) + Val(txtOthCharges.Text) + Val(txtTollTax.Text), "0.00")
        lblNetWt.Text = VB6.Format(Val(txtGrossWt.Text) - Val(txtTearWt.Text), "0.00")



        mRefNoStr = ""
        mCountNosOfBill = 0
        mNosOfBill = Val(txtTotBills.Text)

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColBillCheck
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    .Col = ColRefNo
                    If .Text = "" Then GoTo DontCalc1
                    mRefNo = Trim(.Text)

                    If InStr(1, mRefNoStr, Trim(mRefNo)) = 0 Then
                        mCountNosOfBill = mCountNosOfBill + 1
                    End If

                    mRefNoStr = IIf(mRefNoStr = "", mRefNo, mRefNoStr & "," & mRefNo)
                End If
DontCalc1:
            Next cntRow
        End With

        If lblBookType.Text = "L" Then
            mTripRate = GetVehicleRate(txtVehicleNo.Text, "T", IIf(optFreightType(0).Checked = True, "R", "P"), Val(lblNetWt.Text))
            If mTripRate > 0 Then
                txtTripAmount.Text = VB6.Format(mTripRate, "0.00")
            End If
            'txtOthCharges.Text = CStr(GetVehicleRate(txtVehicleNo.Text, "P", IIf(optFreightType(0).Checked = True, "R", "P")))
        End If

        mPendingNosOfBill = mNosOfBill - mCountNosOfBill

        txtTotPendingBills.Text = CStr(mPendingNosOfBill)
        Exit Sub
ERR1:
        'Resume
        If Err.Number = 6 Then Resume Next 'OverFlow
        MsgInformation(Err.Description)
    End Sub


    Private Function AutoGenSeqNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsDNMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mStartingSNo As Integer
        Dim mMaxValue As String

        SqlStr = ""
        mStartingSNo = IIf(lblBookType.Text = "L", 1, 50001)
        mNewSeqNo = mStartingSNo

        SqlStr = "SELECT Max(AUTO_KEY_LOAD)  " & vbCrLf _
            & " FROM DSP_LOADING_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_LOAD,LENGTH(AUTO_KEY_LOAD)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BOOKTYPE='" & lblBookType.Text & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsDNMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = mStartingSNo '' 1
                End If
            End If
        End With
        AutoGenSeqNo = mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchVehicleMaster()
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ViewGrid()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmLoadingSlip_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = IIf(lblBookType.Text = "L", "Loading Slip", "Unloading Slip")

        SqlStr = ""
        SqlStr = "Select * from DSP_LOADING_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLoadMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from DSP_LOADING_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLoadDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from DSP_LOADING_OTH_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLoadDetailOth, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call SetTextLengths()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub frmLoadingSlip_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmLoadingSlip_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        pMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        txtSlipDate.Enabled = True
        ADDMode = False
        MODIFYMode = False

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetTextLengths()

        On Error GoTo SetTextLengthsErr
        txtSlipNo.MaxLength = RsLoadMain.Fields("AUTO_KEY_LOAD").Precision
        txtSlipDate.MaxLength = 20
        txtCSlipNo.MaxLength = RsLoadMain.Fields("CSLIP_NO").Precision
        txtCSlipDate.MaxLength = 10
        txtTripNo.MaxLength = RsLoadMain.Fields("TRIP_NO").Precision
        txtTripDate.MaxLength = 10
        txtVehicleNo.MaxLength = MainClass.SetMaxLength("NAME", "FIN_VEHICLE_MST", PubDBCn)
        txtTransporterName.MaxLength = MainClass.SetMaxLength("TRANSPORTER_NAME", "FIN_VEHICLE_MST", PubDBCn)
        txtVehicleType.MaxLength = MainClass.SetMaxLength("VEHICLE_TYPE", "FIN_VEHICLE_MST", PubDBCn)
        '    txtRefNo
        txtRemarks.MaxLength = RsLoadMain.Fields("REMARKS").DefinedSize

        txtGRNo.MaxLength = RsLoadMain.Fields("GR_NO").DefinedSize
        txtGRDate.MaxLength = RsLoadMain.Fields("GR_DATE").DefinedSize

        txtTotBills.MaxLength = RsLoadMain.Fields("TOT_BILLS").Precision
        txtTotPendingBills.MaxLength = RsLoadMain.Fields("TOT_PENDING_BILLS").Precision

        Exit Sub
SetTextLengthsErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub Clear1()


        txtSlipNo.Text = ""

        'If RsCompany.Fields("FYEAR").Value = GetCurrentFYNo(PubDBCn, VB6.Format(PubCurrDate, "DD/MM/YYYY")) Then
        txtSlipDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime()
        txtCSlipNo.Text = ""
        txtCSlipDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")

        txtTripNo.Text = ""
        txtTripDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")
        'Else
        txtSlipDate.Text = VB6.Format(RunDate, "DD/MM/YYYY") & " " & GetServerTime()
        txtCSlipNo.Text = ""
        txtCSlipDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtTripNo.Text = ""
        txtTripDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        'End If
        txtVehicleNo.Text = ""
        txtTransporterName.Text = ""

        txtGRNo.Text = ""
        txtGRDate.Text = ""

        txtVehicleType.Text = ""
        txtRefNo.Text = ""
        txtRemarks.Text = ""

        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""

        txtTripAmount.Text = ""
        txtOthCharges.Text = ""
        txtTollTax.Text = ""
        txtNetAmount.Text = ""

        txtGrossWt.Text = ""
        txtTearWt.Text = ""
        lblNetWt.Text = ""


        txtTotBills.Text = ""
        txtTotPendingBills.Text = ""
        txtTotPendingBills.Enabled = False
        txtTotBills.Enabled = True

        If lblBookType.Text = "U" Then
            chkAckReceipt.CheckState = System.Windows.Forms.CheckState.Unchecked
            chkAckReceipt.Enabled = True

        Else
            If lblAck.Text = "Y" Then
                chkAckReceipt.Enabled = True ''IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            Else
                chkAckReceipt.Enabled = False
            End If
            chkAckReceipt.CheckState = System.Windows.Forms.CheckState.Unchecked
        End If

        txtAckDate.Text = "__/__/____ __:__"
        txtAckDate.Enabled = chkAckReceipt.Enabled

        txtInDateTime.Text = "__/__/____ __:__"
        txtInDateTime.Enabled = False

        If lblBookType.Text = "U" Then

            txtCSlipNo.Enabled = False
            txtCSlipDate.Enabled = False
            cmdSearchTrip.Enabled = False
            cmdsearch.Enabled = True
            txtTripDate.Enabled = False
            txtTripNo.Enabled = True
            txtVehicleNo.Enabled = True
            txtTransporterName.Enabled = True
            txtVehicleType.Enabled = True
            chkWOCollection.Enabled = True
        Else
            cmdSearchTrip.Enabled = False
            txtCSlipDate.Enabled = False
            txtCSlipNo.Enabled = False
            txtTripDate.Enabled = True
            txtTripNo.Enabled = True

            chkWOCollection.Enabled = False
            chkWOCollection.Visible = False
        End If

        '    cmdSearchTrip.Enabled = True
        '    txtCSlipNo.Enabled = True
        chkThirdParty.Enabled = True
        chkThirdParty.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkWOCollection.CheckState = System.Windows.Forms.CheckState.Unchecked

        lblPacket.Text = "0.00"
        lblTotItemQty.Text = "0.00"

        optFreightType(0).Checked = False
        optFreightType(1).Checked = False
        fraFreightType.Enabled = True

        If lblBookType.Text = "L" Then
            optShow(0).Checked = True
            If lblAck.Text = "Y" Then
                FraShow.Enabled = False
                txtTripNo.Enabled = False
                txtTripDate.Enabled = False
                txtVehicleNo.Enabled = False
                cmdsearch.Enabled = False
                txtTransporterName.Enabled = False
                txtRemarks.Enabled = False
                txtVehicleType.Enabled = False
                txtTotBills.Enabled = False
                chkThirdParty.Enabled = False
            Else
                txtTransporterName.Enabled = True
            End If
        Else
            'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            '    optShow(3).Checked = True
            'Else
            optShow(2).Checked = True
            'End If
        End If


        MainClass.ClearGrid(SprdMain)
        MainClass.ClearGrid(SprdMainOth)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsLoadMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub ShowInvoiceData(ByRef pRsInvoice As ADODB.Recordset)
        On Error GoTo ShowErrPart
        'Dim mSuppCustCode As String
        'Dim pMKey As String
        'Dim mSqlStr As String
        'Dim RsTemp As ADODB.Recordset=Nothing
        'Dim I As Long
        '
        '     If Not pRsInvoice.EOF Then
        '        pMKey = IIf(IsNull(pRsInvoice!mKey), "", pRsInvoice!mKey)
        '        txtSlipNo.Text = VB6.Format(IIf(IsNull(pRsInvoice!BILLNOSEQ), "", pRsInvoice!BILLNOSEQ), "00000")
        '        txtSlipDate.Text = VB6.Format(IIf(IsNull(pRsInvoice!INVOICE_DATE), "", pRsInvoice!INVOICE_DATE), "DD-MM-YYYY")
        '
        '        mSuppCustCode = IIf(IsNull(pRsInvoice!SUPP_CUST_CODE), "", pRsInvoice!SUPP_CUST_CODE)
        '
        '        If MainClass.ValidateWithMasterTable(mSuppCustCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '            txtTransporterName.Text = MasterNo
        '        End If
        '
        '        txtPONo.Text = IIf(IsNull(pRsInvoice!CUST_PO_NO), "", pRsInvoice!CUST_PO_NO)
        '        txtBEDAmount.Text = VB6.Format(IIf(IsNull(pRsInvoice!TOTEDAMOUNT), "", pRsInvoice!TOTEDAMOUNT), "0.00")
        '
        '        MainClass.ClearGrid SprdMain
        '        mSqlStr = "SELECT * FROM FIN_INVOICE_DET WHERE MKEY='" & MainClass.AllowSingleQuote(pMKey) & "'"
        '        MainClass.UOpenRecordSet mSqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '        If RsTemp.EOF = False Then
        '            Do While RsTemp.EOF = False
        '
        '                I = I + 1
        '                SprdMain.MaxRows = SprdMain.MaxRows + 1
        '                SprdMain.Row = I
        '
        '                SprdMain.Col = ColInvItemCode
        '                SprdMain.Text = IIf(IsNull(RsTemp!ITEM_CODE), 0, RsTemp!ITEM_CODE)
        ''                mItemCode = IIf(IsNull(!ITEM_CODE), 0, !ITEM_CODE)
        ''
        ''                SprdMain.Col = ColItemDesc
        ''                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        ''                    SprdMain.Text = MasterNo
        ''                End If
        '
        '                SprdMain.Col = ColInvQty
        '                SprdMain.Text = VB6.Format(IIf(IsNull(RsTemp!ITEM_QTY), 0, RsTemp!ITEM_QTY), "0.00")
        '
        '                RsTemp.MoveNext
        '            Loop
        '        End If
        '    End If
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description, Err.Number)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mIsAckReceipt As String

        With RsLoadMain
            If Not .EOF Then
                lblMKey.Text = .Fields("AUTO_KEY_LOAD").Value
                chkThirdParty.CheckState = IIf(.Fields("IS_TP_VEHICLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                txtSlipNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_LOAD").Value), "", .Fields("AUTO_KEY_LOAD").Value)
                txtSlipDate.Text = VB6.Format(IIf(IsDBNull(.Fields("SLIP_DATE").Value), "", .Fields("SLIP_DATE").Value), "DD/MM/YYYY HH:MM") '' VB6.Format(IIf(IsNull(.Fields("SLIP_DATE").Value), "", .Fields("SLIP_DATE").Value), "DD/MM/YYYY")

                txtCSlipNo.Text = IIf(IsDBNull(.Fields("CSLIP_NO").Value), "", .Fields("CSLIP_NO").Value)
                txtCSlipDate.Text = VB6.Format(IIf(IsDBNull(.Fields("CSLIP_DATE").Value), "", .Fields("CSLIP_DATE").Value), "DD/MM/YYYY")

                txtTripNo.Text = IIf(IsDBNull(.Fields("TRIP_NO").Value), "", .Fields("TRIP_NO").Value)
                txtTripDate.Text = VB6.Format(IIf(IsDBNull(.Fields("TRIP_DATE").Value), "", .Fields("TRIP_DATE").Value), "DD/MM/YYYY")

                txtGRNo.Text = IIf(IsDBNull(.Fields("GR_NO").Value), "", .Fields("GR_NO").Value)
                txtGRDate.Text = VB6.Format(IIf(IsDBNull(.Fields("GR_DATE").Value), "", .Fields("GR_DATE").Value), "DD/MM/YYYY")

                If .Fields("FREIGHT_TYPE").Value = "R" Then
                    optFreightType(0).Checked = True
                Else
                    optFreightType(1).Checked = True
                End If

                If lblBookType.Text = "U" Then
                    fraFreightType.Enabled = False
                End If

                txtVehicleNo.Text = IIf(IsDBNull(.Fields("VEHICLE_NO").Value), "", .Fields("VEHICLE_NO").Value)
                txtTransporterName.Text = IIf(IsDBNull(.Fields("TRANSPORTER_NAME").Value), "", .Fields("TRANSPORTER_NAME").Value)
                txtVehicleType.Text = IIf(IsDBNull(.Fields("VEHICLE_TYPE").Value), "", .Fields("VEHICLE_TYPE").Value)
                txtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)

                txtTotBills.Text = IIf(IsDBNull(.Fields("TOT_BILLS").Value), "", .Fields("TOT_BILLS").Value)
                txtTotPendingBills.Text = IIf(IsDBNull(.Fields("TOT_PENDING_BILLS").Value), "", .Fields("TOT_PENDING_BILLS").Value)

                txtTripAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("TRIP_AMOUNT").Value), 0, .Fields("TRIP_AMOUNT").Value), "0.00")
                txtOthCharges.Text = VB6.Format(IIf(IsDBNull(.Fields("OTH_AMOUNT").Value), 0, .Fields("OTH_AMOUNT").Value), "0.00")
                txtTollTax.Text = VB6.Format(IIf(IsDBNull(.Fields("TOLL_AMOUNT").Value), 0, .Fields("TOLL_AMOUNT").Value), "0.00")
                txtNetAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("NET_AMOUNT").Value), 0, .Fields("NET_AMOUNT").Value), "0.00")

                txtGrossWt.Text = VB6.Format(IIf(IsDBNull(.Fields("GROSS_WT").Value), 0, .Fields("GROSS_WT").Value), "0.00")
                txtTearWt.Text = VB6.Format(IIf(IsDBNull(.Fields("TEAR_WT").Value), 0, .Fields("TEAR_WT").Value), "0.00")
                lblNetWt.Text = VB6.Format(IIf(IsDBNull(.Fields("NET_WT").Value), 0, .Fields("NET_WT").Value), "0.00")


                mIsAckReceipt = IIf(IsDBNull(.Fields("IS_ACK_RECEIPT").Value), "N", .Fields("IS_ACK_RECEIPT").Value)

                chkAckReceipt.CheckState = IIf(mIsAckReceipt = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                '            If lblBookType.text = "L" Then
                chkAckReceipt.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, IIf(mIsAckReceipt = "Y", False, True))
                '            End If

                txtAckDate.Text = VB6.Format(IIf(IsDBNull(.Fields("ACK_RECEIPTDATE").Value), "__/__/____ __:__", .Fields("ACK_RECEIPTDATE").Value), "DD/MM/YYYY HH:MM")
                txtAckDate.Enabled = chkAckReceipt.Enabled

                txtInDateTime.Text = VB6.Format(IIf(IsDBNull(.Fields("IN_DATE_TIME").Value), "__/__/____ __:__", .Fields("IN_DATE_TIME").Value), "DD/MM/YYYY HH:MM")
                If txtInDateTime.Text = "__/__/____ __:__" Or txtInDateTime.Text = "" Or Not IsDate(txtInDateTime.Text) Then
                    txtInDateTime.Enabled = True
                Else
                    txtInDateTime.Enabled = False
                End If

                chkWOCollection.CheckState = IIf(.Fields("WO_COLLECTION").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                lblAddUser.Text = IIf(IsDBNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                lblAddDate.Text = VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                lblModUser.Text = IIf(IsDBNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                lblModDate.Text = VB6.Format(IIf(IsDBNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")

                '            mDivisionCode = IIf(IsNull(!DIV_CODE), -1, !DIV_CODE)
                '
                '            If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                mDivisionDesc = Trim(MasterNo)
                '                cboDivision.Text = mDivisionDesc
                '            End If

                If lblBookType.Text = "U" Then
                    txtCSlipNo.Enabled = False
                    txtCSlipDate.Enabled = False
                    cmdSearchTrip.Enabled = False
                    chkWOCollection.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
                End If


                chkThirdParty.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)

                Call ShowDetail1(CDbl(lblMKey.Text))
                Call ShowDetailOth1(CDbl(lblMKey.Text))
                Call CalcTots()
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsLoadMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim pLoadingNo As Double
        Dim mVehicleOwner As String
        Dim cntRow As Integer
        Dim mInvNo As String
        Dim mVehicleNo As String
        Dim RsTemp As ADODB.Recordset = Nothing

        FieldsVarification = True
        If ValidateBranchLocking((txtSlipDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsLoadMain.EOF = True Then Exit Function

        If MODIFYMode = True And Trim(txtSlipNo.Text) = "" Then
            MsgInformation("Slip No. is Blank")
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtSlipDate.Text) = "" Then
            MsgInformation(" Slip Date is empty. Cannot Save")
            If txtSlipDate.Enabled = True Then txtSlipDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtSlipDate.Text) <> "" Then
            If IsDate(txtSlipDate.Text) = False Then
                MsgInformation(" Invalid Slip Date. Cannot Save")
                If txtSlipDate.Enabled = True Then txtSlipDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If FYChk(VB6.Format(txtSlipDate.Text, "DD/MM/YYYY")) = False Then
            FieldsVarification = False
            If txtSlipDate.Enabled = True Then txtSlipDate.Focus()
            Exit Function
        End If

        If CDate(VB6.Format(txtSlipDate.Text, "DD/MM/YYYY")) > CDate(VB6.Format(PubCurrDate, "DD/MM/YYYY")) Then
            MsgInformation("Slip Date Cannot be greater than current date.")
            FieldsVarification = False
            Exit Function
        End If

        If optFreightType(0).Checked = False And optFreightType(1).Checked = False Then
            MsgInformation("Please Select The Freight Type.")
            FieldsVarification = False
            Exit Function
        End If

        If lblBookType.Text = "U" And chkThirdParty.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Val(txtCSlipNo.Text) = 0 Then
                MsgInformation("Invalid Collection Slip No. Cannot Save")
                If txtCSlipNo.Enabled = True Then txtCSlipNo.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If


        'If Trim(txtTripNo.Text) = "" Then
        '    MsgInformation("Trip No is Blank. Cannot Save")
        '    If txtTripNo.Enabled = True Then txtTripNo.Focus()
        '    FieldsVarification = False
        '    Exit Function
        'End If

        'If Trim(txtTripDate.Text) = "" Then
        '    MsgInformation("Trip Date is empty. Cannot Save")
        '    If txtSlipDate.Enabled = True Then txtTripDate.Focus()
        '    FieldsVarification = False
        '    Exit Function
        'End If
        'If Trim(txtTripDate.Text) <> "" Then
        '    If IsDate(txtTripDate.Text) = False Then
        '        MsgInformation("Invalid Trip Date. Cannot Save")
        '        If txtSlipDate.Enabled = True Then txtTripDate.Focus()
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'End If


        'If Trim(txtCSlipDate.Text) <> "" Then
        '    If IsDate(txtCSlipDate.Text) = False Then
        '        MsgInformation("Invalid Collection Slip Date. Cannot Save")
        '        If txtCSlipDate.Enabled = True Then txtCSlipDate.Focus()
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'End If



        If Trim(txtVehicleNo.Text) = "" Then
            MsgInformation("Vehicle No is Blank. Cannot Save")
            If txtVehicleNo.Enabled = True Then txtVehicleNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtTransporterName.Text) = "" Then
            MsgInformation("Transporter Name is Blank. Cannot Save")
            If txtTransporterName.Enabled = True Then txtTransporterName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If chkThirdParty.CheckState = System.Windows.Forms.CheckState.Checked Then
            If MainClass.ValidateWithMasterTable(txtVehicleNo.Text, "NAME", "VEHICLE_OWNER", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                'mVehicleOwner = Trim(MasterNo)

                'If mVehicleOwner <> "3" Then
                '    MsgInformation("Such Vehicle is not a Third Party Vehicle.")
                '    If chkThirdParty.Enabled = True Then chkThirdParty.Focus()
                '    FieldsVarification = False
                '    Exit Function
                'End If
            Else
                '            If RsCompany.fields("COMPANY_CODE").value = 3 Or RsCompany.fields("COMPANY_CODE").value = 10 Then
                '                MsgInformation "Vehicle not not defined in Master."
                '                If chkThirdParty.Enabled = True Then chkThirdParty.SetFocus
                '                FieldsVarification = False
                '                Exit Function
                '            Else
                '
                '            End If
            End If
        Else
            If lblBookType.Text = "L" Then
                If Val(txtTripAmount.Text) <= 0 Then
                    MsgInformation("Please Enter the Trip Amount.")
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If Val(lblNetWt.Text) <= 0 Then
            MsgInformation("Please Enter the Wt.")
            FieldsVarification = False
            Exit Function
        End If


        If IsDate(txtAckDate.Text) = False Then    '' txtAckDate.Text = "__/__/____ __:__" Then       ''"__/__/____ __:__" Then
        Else
            If PubSuperUser <> "S" Then
                If CDate(txtAckDate.Text) < CDate(txtSlipDate.Text) Then
                    MsgInformation("Acknowledgement Date Cann't be Less Than Slip Date.")
                    If CmdSave.Enabled = True Then CmdSave.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If IsDate(txtInDateTime.Text) = True Then    '' If txtInDateTime.Text <> "__/__/____ __:__" Then
            If CDate(txtInDateTime.Text) < CDate(txtSlipDate.Text) Then
                MsgInformation("In Date Cann't be Less Than Slip Date.")
                If CmdSave.Enabled = True Then CmdSave.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
        '    If lblBookType.Text <> "U" Then
        '        If PendingTrip((txtSlipNo.Text), pLoadingNo) = True Then
        '            MsgInformation("Acknowledgement Receipt not Received for  " & pLoadingNo & " for such Vehicle.")
        '            If CmdSave.Enabled = True Then CmdSave.Focus()
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If
        'Else
        If lblBookType.Text <> "U" Then
            If IsDate(txtAckDate.Text) = True Then  'If txtAckDate.Text <> "__/__/____ __:__" Then
                If IsDate(txtInDateTime.Text) = False Then    'If txtInDateTime.Text = "__/__/____ __:__" Then
                    MsgInformation("In Date Cann't be Blank.")
                    txtInDateTime.Enabled = True
                    If txtInDateTime.Enabled = True Then txtInDateTime.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            If PubSuperUser <> "S" Then
                If PendingInTime((txtSlipNo.Text), pLoadingNo) = True Then
                    MsgInformation("In Date & Time is not Entered for  " & pLoadingNo & " for such Vehicle.")
                    If CmdSave.Enabled = True Then CmdSave.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        'End If

        If Val(txtTotPendingBills.Text) < 0 Then
            MsgInformation("Total Nos of Bill/s is Less than Nos of Bill/s you entered..")
            If CmdSave.Enabled = True Then CmdSave.Focus()
            FieldsVarification = False
            Exit Function
        End If


        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColRefNo
                mInvNo = .Text

                .Col = ColRefType

                If .Text = "I" Then
                    .Col = ColBillCheck
                    If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                        SqlStr = " SELECT VEHICLENO" & vbCrLf _
                                & " FROM FIN_INVOICE_HDR IH" & vbCrLf _
                                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                & " AND IH.BILLNO='" & mInvNo & "'"
                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            mVehicleNo = IIf(IsDBNull(RsTemp.Fields("VEHICLENO").Value), "", RsTemp.Fields("VEHICLENO").Value)
                            If mVehicleNo <> Trim(txtVehicleNo.Text) And mVehicleNo <> "" Then
                                MsgInformation("Vehicle No not Match With Invoice.")
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    End If

                End If
            Next
        End With

        Dim mPartNo As String = ""
        Dim mCheckQty As Double = 0
        Dim mINVQty As Double = 0

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColRefNo
                mInvNo = .Text

                .Col = ColItemCode
                mPartNo = Trim(.Text)

                .Col = ColQty
                mCheckQty = Val(.Text)

                .Col = ColRefType

                If .Text = "I" Then
                    .Col = ColBillCheck
                    If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                        SqlStr = " SELECT SUM(ITEM_QTY) AS ITEM_QTY" & vbCrLf _
                                & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf _
                                & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
                                & " AND IH.BILLNO='" & mInvNo & "'" & vbCrLf _
                                & " AND ID.ITEM_CODE='" & mPartNo & "'"

                        ''AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            mINVQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                            If mCheckQty <> mINVQty Then
                                MsgInformation("Loading Qty not Match With Invoice No :" & mInvNo & " Part No : " & mPartNo)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    End If

                End If
            Next
        End With

        Dim mInvoiceNo As String = ""
        Dim xLoadingNo As Double

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColRefType
                If .Text = "I" Then
                    .Col = ColBillCheck
                    If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                        .Col = ColRefNo
                        mInvNo = .Text

                        If AlreadyLoad(mInvNo, "I", xLoadingNo) = True Then
                            MsgBox("Already made a Loading Slip of Such Ref No. Loading Slip No : " & xLoadingNo)
                            FieldsVarification = False
                            Exit Function
                        End If

                        If InStr(1, mInvoiceNo, mInvNo) = 0 And mInvNo <> "" Then
                            mInvoiceNo = IIf(mInvoiceNo = "", mInvNo, mInvoiceNo & "," & mInvNo)
                        End If
                    End If
                End If
            Next
        End With

        Dim strArray() As String


        If mInvoiceNo <> "" Then
            strArray = Split(mInvoiceNo, ",")
            For y = 0 To UBound(strArray)
                mInvNo = strArray(y)
                mCheckQty = 0
                With SprdMain
                    For cntRow = 1 To .MaxRows - 1
                        .Row = cntRow
                        .Col = ColRefType
                        If .Text = "I" Then
                            .Col = ColBillCheck
                            If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                                .Col = ColRefNo
                                If Trim(.Text) = mInvNo Then
                                    .Col = ColQty
                                    mCheckQty = mCheckQty + Val(.Text)
                                End If
                            End If
                        End If
                    Next
                End With

                SqlStr = " SELECT SUM(ITEM_QTY) AS ITEM_QTY" & vbCrLf _
                        & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf _
                        & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
                        & " AND IH.BILLNO='" & mInvNo & "'"

                ''AND  IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    mINVQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                    If mCheckQty <> mINVQty Then
                        MsgInformation("Loading Qty not Match With Invoice No :" & mInvNo)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If

            Next y
        End If

        If chkWOCollection.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If SprdMain.MaxRows = 1 Then
                MsgInformation("Nothing to Save")
                FieldsVarification = False
                Exit Function
            End If

            If MainClass.ValidDataInGrid(SprdMain, ColRefNo, "S", "Please Check Ref No") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(SprdMain, ColRefDate, "S", "Please Check Ref Date") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(SprdMain, ColCustName, "S", "Please Check Customer Name.") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Please Check Item Code.") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(SprdMain, ColItemDesc, "S", "Please Check Item Description.") = False Then FieldsVarification = False : Exit Function

            If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Item Qty.") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(SprdMain, ColPacks, "N", "Please Check No of Packs.") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(SprdMain, ColPackType, "S", "Please Check Pack Type.") = False Then FieldsVarification = False : Exit Function

            If SprdMainOth.MaxRows > 1 Then
                If MainClass.ValidDataInGrid(SprdMainOth, ColRefNoOth, "S", "Please Check Ref No") = False Then FieldsVarification = False : Exit Function
                If MainClass.ValidDataInGrid(SprdMainOth, ColRefDateOth, "S", "Please Check Ref Date") = False Then FieldsVarification = False : Exit Function
                If MainClass.ValidDataInGrid(SprdMainOth, ColCustNameOth, "S", "Please Check Customer Name.") = False Then FieldsVarification = False : Exit Function
                If MainClass.ValidDataInGrid(SprdMainOth, ColItemDescOth, "S", "Please Check Item Description.") = False Then FieldsVarification = False : Exit Function

                If MainClass.ValidDataInGrid(SprdMainOth, ColItemWtOth, "N", "Please Check Item Wt.") = False Then FieldsVarification = False : Exit Function
                If MainClass.ValidDataInGrid(SprdMainOth, ColQtyOth, "N", "Please Check Item Qty.") = False Then FieldsVarification = False : Exit Function
                If MainClass.ValidDataInGrid(SprdMainOth, ColPacksOth, "N", "Please Check No of Packs.") = False Then FieldsVarification = False : Exit Function
                If MainClass.ValidDataInGrid(SprdMainOth, ColPackTypeOth, "S", "Please Check Pack Type.") = False Then FieldsVarification = False : Exit Function
            End If

        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume
    End Function
    Private Function PendingTrip(ByRef pRefNo As String, ByRef pLoadingNo As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim mSlipDate As String
        Dim mCurrentDate As String

        PendingTrip = False

        For I = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = I
            SprdMain.Col = ColRefType
            If Trim(SprdMain.Text) = "R" Or Trim(SprdMain.Text) = "" Then

            Else
                Exit For
            End If
            If I = SprdMain.MaxRows - 1 Then
                PendingTrip = False
                Exit Function
            End If
        Next
        pLoadingNo = 0
        mCurrentDate = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime()
        SqlStr = " SELECT AUTO_KEY_LOAD, TO_CHAR(SLIP_DATE,'DD-MON-YYYY HH24:MI') AS SLIP_DATE" & vbCrLf _
            & " FROM DSP_LOADING_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND AUTO_KEY_LOAD<>" & Val(pRefNo) & " AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf _
            & " AND VEHICLE_NO='" & MainClass.AllowSingleQuote((txtVehicleNo.Text)) & "' AND IS_ACK_RECEIPT='N' ORDER BY SLIP_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            pLoadingNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_LOAD").Value), 0, RsTemp.Fields("AUTO_KEY_LOAD").Value)
            mSlipDate = IIf(IsDBNull(RsTemp.Fields("SLIP_DATE").Value), "", VB6.Format(RsTemp.Fields("SLIP_DATE").Value, "DD/MM/YYYY HH:MM"))
            mSlipDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 3, CDate(mSlipDate)))
            If RsCompany.Fields("COMPANY_CODE").Value = 1 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
                If CDate(mSlipDate) < CDate(mCurrentDate) Then
                    PendingTrip = True
                End If
            Else
                PendingTrip = True
            End If
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Function PendingInTime(ByRef pRefNo As String, ByRef pLoadingNo As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim mSlipDate As String
        Dim mCurrentDate As String

        PendingInTime = False

        If chkThirdParty.CheckState = System.Windows.Forms.CheckState.Checked Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            PendingInTime = False
            Exit Function
        End If

        For I = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = I
            SprdMain.Col = ColRefType
            If Trim(SprdMain.Text) = "R" Or Trim(SprdMain.Text) = "" Then

            Else
                Exit For
            End If
            If I = SprdMain.MaxRows - 1 Then
                PendingInTime = False
                Exit Function
            End If
        Next
        pLoadingNo = 0
        mCurrentDate = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime()
        SqlStr = " SELECT AUTO_KEY_LOAD, TO_CHAR(SLIP_DATE,'DD-MON-YYYY HH24:MI') AS SLIP_DATE" & vbCrLf _
            & " FROM DSP_LOADING_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND AUTO_KEY_LOAD<>" & Val(pRefNo) & " AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf _
            & " AND VEHICLE_NO='" & MainClass.AllowSingleQuote((txtVehicleNo.Text)) & "'" & vbCrLf _
            & " AND (IN_DATE_TIME='' OR IN_DATE_TIME IS NULL) ORDER BY SLIP_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            pLoadingNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_LOAD").Value), 0, RsTemp.Fields("AUTO_KEY_LOAD").Value)
            mSlipDate = IIf(IsDBNull(RsTemp.Fields("SLIP_DATE").Value), "", VB6.Format(RsTemp.Fields("SLIP_DATE").Value, "DD/MM/YYYY HH:MM"))
            '        mSlipDate = DateAdd("d", 3, mSlipDate)

            PendingInTime = True

        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            AssignGrid(True)
            '        ADataPPOMain.Refresh
            SprdView.Refresh()
            SprdView.Focus()
            FraTop.Visible = False
            Frabot.Visible = False
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraTop.Visible = True
            Frabot.Visible = True
            SprdView.SendToBack()
        End If
        Call FormatSprdView()
        MainClass.ButtonStatus(Me, XRIGHT, RsLoadMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmLoadingSlip_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsLoadMain.Close()
        'PvtDBCn.Close
        RsLoadMain = Nothing
        'Set PvtDBCn = Nothing
    End Sub


    Private Sub optFreightType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optFreightType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optFreightType.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optShow_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optShow.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optShow.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mPacks As Double
        Dim mPacksRecd As Double

        If eventArgs.newRow = -1 Then Exit Sub

        cntRow = SprdMain.ActiveRow
        SprdMain.Row = cntRow

        Select Case eventArgs.col
            Case ColPackType
                SprdMain.Col = ColPackType
                If Trim(SprdMain.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(SprdMain.Text, "NAME", "NAME", "DSP_PACKINGTYPE_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPackType)
                    End If
                End If
            Case ColPackRecd, ColPacks

                SprdMain.Row = cntRow
                SprdMain.Col = ColPacks
                mPacks = Val(SprdMain.Text)

                SprdMain.Col = ColPackRecd
                mPacksRecd = Val(SprdMain.Text)

                If mPacksRecd > mPacks Then
                    MsgInformation("Recd Packs Cann't be Greater than Packs Send.")
                    If lblAck.Text = "Y" Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPackRecd)
                    Else
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPacks)
                    End If
                    eventArgs.cancel = True
                End If
        End Select

        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub


    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtSlipNo.Text = SprdView.Text

        txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtAckDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAckDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAckDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAckDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtAckDate.Text = "  /  /       :" Then GoTo EventExitSub ''"__/__/____ __:__" Then GoTo EventExitSub

        If Not IsDate(txtAckDate.Text) Then
            MsgInformation("Invalid Acknowledgement Date.")
            txtAckDate.Focus()
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtCSlipDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCSlipDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtCSlipDate.Text = "" Then GoTo EventExitSub

        If Not IsDate(txtCSlipDate.Text) Then
            MsgInformation("Invalid Collection Slip Date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtGRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGRDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtGRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGRDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtGRDate.Text = "" Then GoTo EventExitSub

        If Not IsDate(txtGRDate.Text) Then
            MsgInformation("Invalid Date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtGRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGRNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtGRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtGRNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtInDateTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInDateTime.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInDateTime_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInDateTime.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtInDateTime.Text = "__/__/____ __:__" Then GoTo EventExitSub

        If Not IsDate(txtInDateTime.Text) Then
            MsgInformation("Invalid In Date.")
            txtInDateTime.Focus()
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNetAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNetAmount.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNetAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNetAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtNetAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNetAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOthCharges_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOthCharges.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOthCharges_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOthCharges.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtOthCharges_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOthCharges.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRefNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRefNo.Text) = "" Then GoTo EventExitSub

        'If optShow(0).Checked = True Then
        '    If Len(txtRefNo.Text) <= 10 Then
        '        txtRefNo.Text = Val(txtRefNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        '    End If
        'Else
        '    If Len(txtRefNo.Text) <= 8 Then
        '        txtRefNo.Text = Val(txtRefNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        '    End If
        'End If
EventExitSub:
        eventArgs.Cancel = Cancel
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


    Private Sub cmdSearchTrip_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchTrip.Click
        If lblBookType.Text = "U" Then
            SearchTrip()
        End If
    End Sub

    Private Sub txtSlipDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTollTax_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTollTax.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTollTax_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTollTax.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTollTax_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTollTax.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTotBills_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotBills.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        '    CalcTots
    End Sub

    Private Sub txtTotBills_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotBills.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotBills_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTotBills.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTransporterName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTransporterName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTransporterName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTransporterName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTransporterName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSlipNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSlipNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtSlipNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSlipNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mMRRNo As String
        Dim SqlStr As String = ""

        If Trim(txtSlipNo.Text) = "" Then GoTo EventExitSub

        If Len(txtSlipNo.Text) < 6 Then
            txtSlipNo.Text = VB6.Format(Val(txtSlipNo.Text), "00000") & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsLoadMain.EOF = False Then xMkey = RsLoadMain.Fields("mKey").Value
        mMRRNo = Trim(txtSlipNo.Text)

        SqlStr = " SELECT * FROM DSP_LOADING_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_LOAD,LENGTH(AUTO_KEY_LOAD)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AUTO_KEY_LOAD=" & Val(mMRRNo) & " AND BOOKTYPE='" & lblBookType.Text & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLoadMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsLoadMain.EOF = False Then
            Clear1()
            Show1()
            '        TxtCustomerName.Enabled = True
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Loading Note, Use Generate Despatch Note Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM DSP_LOADING_HDR " & " WHERE AUTO_KEY_LOAD=" & Val(xMkey) & " AND BOOKTYPE='" & lblBookType.Text & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLoadMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub frmLoadingSlip_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = ""
        MainClass.ClearGrid(SprdView)

        SqlStr = " SELECT DISTINCT " & vbCrLf _
            & " AUTO_KEY_LOAD, SLIP_DATE, VEHICLE_NO,TRIP_NO,TRIP_DATE, TRANSPORTER_NAME, TOT_QTY ,TOT_PACK,ADDUSER, ADDDATE " & vbCrLf _
            & " FROM DSP_LOADING_HDR " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_LOAD,LENGTH(AUTO_KEY_LOAD)-5,4)='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf _
            & " AND BOOKTYPE='" & lblBookType.Text & "' ORDER BY AUTO_KEY_LOAD"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 20)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 14)
            .ColHidden = False

            .set_ColWidth(2, 14)
            .ColHidden = False

            .set_ColWidth(3, 10)
            .set_ColWidth(4, 12)
            .set_ColWidth(5, 12)
            .set_ColWidth(6, 30)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub


    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1)

            .Col = ColBillCheck
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 2)

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            '        .CellType = SS_CELL_TYPE_FLOAT
            '        .TypeFloatDecimalChar = Asc(".")
            '        .TypeFloatDecimalPlaces = 0
            '        .TypeFloatMax = "99999999999"
            '        .TypeFloatMin = "-99999999999"
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRefNo, 10)

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColRefDate, 7.5)

            .Col = ColCustName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustName, 22)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColItemDesc, 22)

            .Col = ColItemPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColItemPartNo, 10)

            .Col = ColRefType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColRefType, 4)

            .Col = ColUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColUOM, 4)

            .Col = ColItemWt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999.9999")
            .TypeFloatMin = CDbl("-999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColItemWt, 7)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999.9999")
            .TypeFloatMin = CDbl("-999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQty, 7)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999.9999")
            .TypeFloatMin = CDbl("-999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAmount, 9)

            .Col = ColStdPack
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeNumberMax = CDbl("999999")
            .TypeNumberMin = CDbl("-999999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColStdPack, 6)

            .Col = ColPacks
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999.9999")
            .TypeFloatMin = CDbl("-999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPacks, 7)

            .Col = ColPackRecd
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999.9999")
            .TypeFloatMin = CDbl("-999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPackRecd, 7)

            .Col = ColPackType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColPackType, 8)
            '

            .Col = ColPackScanned
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColPackScanned, 7)


            'If FormActive = False Then
            '    .CellType = SS_CELL_TYPE_COMBOBOX
            '    .TypeComboBoxList = "" & Chr(9) & "TROLLY" & Chr(9) & "BIN" & Chr(9) & "BOX" & Chr(9) & "OPEN" & Chr(9) & "BAG"
            '    .TypeComboBoxCurSel = 0
            'End If

            '.set_ColWidth(ColPackType, 8)

            If lblAck.Text = "Y" Then
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRefType, ColRefType)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRefNo, ColPackType)
            Else
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRefType, ColRefType)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRefNo, ColStdPack)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPackRecd, ColPackRecd)
            End If

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPackScanned, ColPackScanned)

        End With
        MainClass.SetSpreadColor(SprdMain, Arow)

        With SprdMainOth
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1)

            .Col = ColRefNoOth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColRefNoOth, 10)

            .Col = ColRefDateOth
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(ColRefDateOth, 7.5)

            .Col = ColCustNameOth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustNameOth, 18)

            .Col = ColItemDescOth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColItemDescOth, 15)

            .Col = ColItemWtOth
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999.9999")
            .TypeFloatMin = CDbl("-999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColItemWtOth, 7)

            .Col = ColQtyOth
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999.9999")
            .TypeFloatMin = CDbl("-999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQtyOth, 7)

            .Col = ColAmountOth
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999.9999")
            .TypeFloatMin = CDbl("-999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAmountOth, 9)

            .Col = ColPacksOth
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999.9999")
            .TypeFloatMin = CDbl("-999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPacksOth, 7)

            .Col = ColPackTypeOth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColPackTypeOth, 8)
            '

        End With
        MainClass.SetSpreadColor(SprdMainOth, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
    End Sub
    Private Function UpdateDetail1(ByRef mAutoKey As Double) As Boolean
        On Error GoTo UpdateDetail1
        Dim I As Short
        Dim mRow As Short
        Dim mItemCode As String
        Dim mQty As Double
        Dim mRefNo As String
        Dim mPacks As Double
        Dim mPackType As String
        Dim mRefType As String
        Dim mItemUOM As String = ""
        Dim mCustName As String
        Dim mItemName As String
        Dim mRefDate As String
        Dim mStdPack As Double
        Dim mPackRecd As Double
        Dim mAmount As Double
        Dim mPackScanned As String

        SqlStr = "DELETE FROM DSP_LOADING_DET WHERE AUTO_KEY_LOAD=" & mAutoKey & ""
        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColRefNo
                mRefNo = Trim(.Text)

                .Col = ColRefDate
                mRefDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColAmount
                mAmount = Val(.Text)

                .Col = ColStdPack
                mStdPack = Val(.Text)

                .Col = ColPacks
                mPacks = Val(.Text)

                .Col = ColPackType
                mPackType = Trim(.Text)

                .Col = ColRefType
                mRefType = Trim(.Text)

                .Col = ColUOM
                mItemUOM = Trim(.Text)

                .Col = ColCustName
                mCustName = Trim(.Text)

                .Col = ColItemDesc
                mItemName = Trim(.Text)

                .Col = ColPackRecd
                mPackRecd = Val(.Text)

                .Col = ColPackScanned
                mPackScanned = Trim(.Text)

                .Col = ColBillCheck
                SqlStr = ""
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then

                    SqlStr = " INSERT INTO DSP_LOADING_DET ( " & vbCrLf _
                            & " COMPANY_CODE, AUTO_KEY_LOAD, " & vbCrLf _
                            & " SERIAL_NO, REF_TYPE, REF_NO, " & vbCrLf _
                            & " ITEM_CODE, ITEM_UOM, PACKED_QTY, " & vbCrLf _
                            & " NO_OF_PACKETS, PACK_TYPE, SUPP_CUST_NAME, ITEM_SHORT_DESC, REF_DATE,PACK_STD,PACK_RECD, ITEM_AMT, PACK_SCANNED) VALUES ( "

                    SqlStr = SqlStr & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mAutoKey & "," & vbCrLf _
                            & " " & I & ", '" & mRefType & "', '" & mRefNo & "', '" & MainClass.AllowSingleQuote(mItemCode) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mItemUOM) & "'," & vbCrLf _
                            & " " & mQty & ", " & mPacks & "," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mPackType) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mCustName) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mItemName) & "', " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(mRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mStdPack & "," & mPackRecd & "," & mAmount & ",'" & mPackScanned & "')"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        'Resume
        MsgBox(Err.Description)
        UpdateDetail1 = False
    End Function
    Private Function UpdateDetailOth1(ByRef mAutoKey As Double) As Boolean
        On Error GoTo UpdateDetailOth1
        Dim I As Short
        Dim mRow As Short
        Dim mPackType As String
        Dim mQty As Double
        Dim mRefNo As String
        Dim mPacks As Double
        Dim mItemWt As Double
        Dim mCustName As String
        Dim mItemName As String
        Dim mRefDate As String
        Dim mAmount As Double


        SqlStr = "DELETE FROM DSP_LOADING_OTH_DET WHERE AUTO_KEY_LOAD=" & mAutoKey & ""
        PubDBCn.Execute(SqlStr)

        With SprdMainOth
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColRefNoOth
                mRefNo = Trim(.Text)

                .Col = ColRefDateOth
                mRefDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColQtyOth
                mQty = Val(.Text)

                .Col = ColAmountOth
                mAmount = Val(.Text)

                .Col = ColItemWtOth
                mItemWt = Val(.Text)

                .Col = ColPacksOth
                mPacks = Val(.Text)

                .Col = ColPackTypeOth
                mPackType = Trim(.Text)

                .Col = ColCustNameOth
                mCustName = Trim(.Text)

                .Col = ColItemDescOth
                mItemName = Trim(.Text)


                SqlStr = ""

                SqlStr = " INSERT INTO DSP_LOADING_OTH_DET ( " & vbCrLf _
                    & " COMPANY_CODE, AUTO_KEY_LOAD, " & vbCrLf _
                    & " SERIAL_NO, REF_NO, " & vbCrLf _
                    & " NO_OF_PACKETS, PACKED_QTY, " & vbCrLf _
                    & " PACK_TYPE, SUPP_CUST_NAME, ITEM_SHORT_DESC, REF_DATE,ITEM_WT, ITEM_AMT) VALUES ( "

                SqlStr = SqlStr & vbCrLf _
                    & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mAutoKey & "," & vbCrLf _
                    & " " & I & ", '" & mRefNo & "', " & vbCrLf _
                    & " " & mPacks & ", " & mQty & "," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mPackType) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mCustName) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mItemName) & "', " & vbCrLf _
                    & " TO_DATE('" & VB6.Format(mRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mItemWt & "," & mAmount & ")"

                PubDBCn.Execute(SqlStr)
            Next
        End With
        UpdateDetailOth1 = True
        Exit Function
UpdateDetailOth1:
        'Resume
        MsgBox(Err.Description)
        UpdateDetailOth1 = False
    End Function
    Private Sub ShowDetailOth1(ByRef mMKey As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mRefNo As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mRefType As String
        Dim mStdPack As Double
        Dim mItemWt As Double

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM DSP_LOADING_OTH_DET " & vbCrLf _
            & " Where AUTO_KEY_LOAD=" & Val(CStr(mMKey)) & "" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLoadDetailOth, ADODB.LockTypeEnum.adLockReadOnly)
        With RsLoadDetailOth
            If .EOF = True Then Exit Sub
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMainOth.Row = I

                SprdMainOth.Col = ColRefNoOth
                mRefNo = IIf(IsDBNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)
                SprdMainOth.Text = mRefNo ''VB6.Format(IIf(IsNull(!SODATE), "", !SODATE), "DD/MM/YYYY")


                SprdMainOth.Col = ColRefDateOth
                SprdMainOth.Text = VB6.Format(IIf(IsDBNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")

                SprdMainOth.Col = ColCustNameOth
                SprdMainOth.Text = VB6.Format(IIf(IsDBNull(.Fields("SUPP_CUST_NAME").Value), "", .Fields("SUPP_CUST_NAME").Value))

                SprdMainOth.Col = ColItemDescOth
                SprdMainOth.Text = Trim(IIf(IsDBNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value))


                SprdMainOth.Col = ColItemWtOth
                SprdMainOth.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_WT").Value), 0, .Fields("ITEM_WT").Value)))

                SprdMainOth.Col = ColQtyOth
                SprdMainOth.Text = CStr(Val(IIf(IsDBNull(.Fields("PACKED_QTY").Value), 0, .Fields("PACKED_QTY").Value)))

                SprdMainOth.Col = ColAmountOth
                SprdMainOth.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_AMT").Value), 0, .Fields("ITEM_AMT").Value)))

                SprdMainOth.Col = ColPacksOth
                SprdMainOth.Text = CStr(Val(IIf(IsDBNull(.Fields("NO_OF_PACKETS").Value), 0, .Fields("NO_OF_PACKETS").Value)))

                SprdMainOth.Col = ColPackTypeOth
                SprdMainOth.Text = IIf(IsDBNull(.Fields("PACK_TYPE").Value), "", .Fields("PACK_TYPE").Value)

                .MoveNext()

                I = I + 1
                SprdMainOth.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Sub ShowDetail1(ByRef mMKey As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemPartNo As String
        Dim mRefNo As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mRefType As String
        Dim mStdPack As Double
        Dim mItemWt As Double
        Dim mPackScanned As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM DSP_LOADING_DET " & vbCrLf _
            & " Where AUTO_KEY_LOAD=" & Val(CStr(mMKey)) & "" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLoadDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsLoadDetail
            If .EOF = True Then Exit Sub
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColBillCheck
                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)

                SprdMain.Col = ColRefType
                SprdMain.Text = IIf(IsDBNull(.Fields("REF_TYPE").Value), "", .Fields("REF_TYPE").Value)
                mRefType = IIf(IsDBNull(.Fields("REF_TYPE").Value), "", .Fields("REF_TYPE").Value)

                SprdMain.Col = ColRefNo
                mRefNo = IIf(IsDBNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)
                SprdMain.Text = mRefNo ''VB6.Format(IIf(IsNull(!SODATE), "", !SODATE), "DD/MM/YYYY")

                ''IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _

                If mRefType = "I" Then
                    SqlStr = "SELECT IH.INVOICE_DATE AS REF_DATE, CMST.SUPP_CUST_NAME " & vbCrLf _
                        & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST CMST " & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
                        & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
                        & " AND IH.BILLNO='" & mRefNo & "'"

                ElseIf mRefType = "R" Then
                    SqlStr = "SELECT IH.GATEPASS_DATE AS REF_DATE, CMST.SUPP_CUST_NAME " & vbCrLf _
                        & " FROM INV_GATEPASS_HDR IH, FIN_SUPP_CUST_MST CMST " & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
                        & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
                        & " AND IH.AUTO_KEY_PASSNO=" & mRefNo & ""

                ElseIf mRefType = "M" Then
                    SqlStr = "SELECT IH.MRR_DATE AS REF_DATE, CMST.SUPP_CUST_NAME " & vbCrLf _
                        & " FROM INV_GATE_HDR IH, FIN_SUPP_CUST_MST CMST " & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
                        & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
                        & " AND IH.AUTO_KEY_MRR=" & mRefNo & ""

                ElseIf mRefType = "G" Then
                    SqlStr = "SELECT IH.GATE_DATE AS REF_DATE, CMST.SUPP_CUST_NAME " & vbCrLf _
                        & " FROM INV_GATEENTRY_HDR IH, FIN_SUPP_CUST_MST CMST " & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
                        & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
                        & " AND IH.AUTO_KEY_GATE=" & mRefNo & ""

                End If
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    SprdMain.Col = ColRefDate
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value), "DD/MM/YYYY")

                    SprdMain.Col = ColCustName
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value))

                Else
                    SprdMain.Col = ColRefDate
                    SprdMain.Text = ""

                    SprdMain.Col = ColCustName
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColItemCode
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = Trim(mItemDesc)

                SprdMain.Col = ColItemPartNo
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "CUSTOMER_PART_NO", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemPartNo = MasterNo
                SprdMain.Text = Trim(mItemPartNo)

                mStdPack = 0
                SprdMain.Col = ColStdPack
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "PACK_STD", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mStdPack = Val(MasterNo)
                SprdMain.Text = CStr(Val(CStr(mStdPack)))

                SprdMain.Col = ColUOM
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                mItemWt = 0
                SprdMain.Col = ColItemWt
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "ITEM_WEIGHT", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemWt = Val(MasterNo)
                SprdMain.Text = CStr(Val(CStr(mItemWt)))

                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("PACKED_QTY").Value), 0, .Fields("PACKED_QTY").Value)))

                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_AMT").Value), 0, .Fields("ITEM_AMT").Value)))

                SprdMain.Col = ColPacks
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("NO_OF_PACKETS").Value), 0, .Fields("NO_OF_PACKETS").Value)))

                SprdMain.Col = ColPackType
                SprdMain.Text = IIf(IsDBNull(.Fields("PACK_TYPE").Value), "", .Fields("PACK_TYPE").Value)

                SprdMain.Col = ColPackRecd
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("PACK_RECD").Value), 0, .Fields("PACK_RECD").Value)))

                SprdMain.Col = ColPackScanned
                SprdMain.Text = IIf(IsDBNull(.Fields("PACK_SCANNED").Value), "", .Fields("PACK_SCANNED").Value)


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

    Private Sub txtCSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCSlipNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCSlipNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCSlipNo.DoubleClick
        If lblBookType.Text = "U" Then
            SearchTrip()
        End If
    End Sub

    Private Sub txtCSlipNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCSlipNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCSlipNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCSlipNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If lblBookType.Text = "U" Then
            If KeyCode = System.Windows.Forms.Keys.F1 Then SearchTrip()
        End If
    End Sub

    Private Sub txtCSlipNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCSlipNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If lblBookType.Text = "L" Then GoTo EventExitSub
        If Val(txtCSlipNo.Text) = 0 Then GoTo EventExitSub

        If Len(txtCSlipNo.Text) <= 6 Then
            txtCSlipNo.Text = Val(txtCSlipNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        'TO_CHAR(AUTO_KEY_TRIP) AS AUTO_KEY_TRIP,TRIP_DATE,,
        SqlStr = "SELECT * " & vbCrLf _
            & " FROM DSP_TRIP_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND AUTO_KEY_TRIP = " & Val(txtCSlipNo.Text) & "" & vbCrLf _
            & " AND STATUS='O' AND AUTO_KEY_TRIP NOT IN (" & vbCrLf _
            & " SELECT CSLIP_NO FROM DSP_LOADING_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND BOOKTYPE='" & lblBookType.Text & "')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtCSlipDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TRIP_DATE").Value), "", RsTemp.Fields("TRIP_DATE").Value), "DD/MM/YYYY")
            txtVehicleNo.Text = IIf(IsDBNull(RsTemp.Fields("VEHICLE_NO").Value), "", RsTemp.Fields("VEHICLE_NO").Value)
            txtTransporterName.Text = IIf(IsDBNull(RsTemp.Fields("TRANSPORTER_NAME").Value), "", RsTemp.Fields("TRANSPORTER_NAME").Value)
            txtVehicleType.Text = IIf(IsDBNull(RsTemp.Fields("VEHICLE_TYPE").Value), "", RsTemp.Fields("VEHICLE_TYPE").Value)

            txtTripNo.Text = IIf(IsDBNull(RsTemp.Fields("TRANSPORTER_TRIP_NO").Value), "", RsTemp.Fields("TRANSPORTER_TRIP_NO").Value)
            txtTripDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TRANSPORTER_TRIP_DATE").Value), "", RsTemp.Fields("TRANSPORTER_TRIP_DATE").Value), "DD/MM/YYYY")

            txtTripAmount.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TRIP_AMOUNT").Value), 0, RsTemp.Fields("TRIP_AMOUNT").Value), "0.00")

            If RsTemp.Fields("FREIGHT_TYPE").Value = "R" Then
                optFreightType(0).Checked = True
            Else
                optFreightType(1).Checked = True
            End If

            fraFreightType.Enabled = False

            chkThirdParty.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
        Else
            MsgBox("Invalid Collection Slip No. Please Check.", MsgBoxStyle.Information)
            chkThirdParty.Enabled = True
            Cancel = True
        End If

        GoTo EventExitSub
ErrPart:

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCSlipDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCSlipDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTripAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTripAmount.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTripAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTripAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTripAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTripAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTripDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTripDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTripDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTripDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtTripDate.Text = "" Then GoTo EventExitSub

        If Not IsDate(txtTripDate.Text) Then
            MsgInformation("Invalid Trip Date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtTripNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTripNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTripNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTripNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVehicleNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicleNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SearchTrip()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "SELECT TO_CHAR(AUTO_KEY_TRIP) AS SLIP_NO,TRIP_DATE AS SLIP_DATE ,VEHICLE_NO, TRANSPORTER_NAME" & vbCrLf _
            & " FROM DSP_TRIP_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND STATUS='O' AND AUTO_KEY_TRIP NOT IN (" & vbCrLf _
            & " SELECT CSLIP_NO FROM DSP_LOADING_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND BOOKTYPE='" & lblBookType.Text & "' AND IS_TP_VEHICLE='N')"

        If MainClass.SearchGridMasterBySQL2((txtCSlipNo.Text), SqlStr) = True Then
            txtCSlipNo.Text = AcName
            txtCSlipNo_Validating(txtCSlipNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SearchVehicleMaster()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtVehicleNo.Text), "FIN_VEHICLE_MST", "NAME", "TRANSPORTER_NAME", "VEHICLE_TYPE", , SqlStr) = True Then
            txtVehicleNo.Text = AcName
            txtVehicleNo_Validating(txtVehicleNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub txtVehicleNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicleNo.DoubleClick
        SearchVehicleMaster()
    End Sub
    Private Sub txtVehicleNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicleNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicleNo.Text, "N")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVehicleNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVehicleNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchVehicleMaster()
    End Sub
    Private Sub txtVehicleNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVehicleNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtVehicleNo.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtVehicleNo.Text), "NAME", "TRANSPORTER_NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            'MsgInformation("Invalid Vehicle No")
            'Cancel = True
            'GoTo EventExitSub
        Else
            txtTransporterName.Text = MasterNo
        End If

        If MainClass.ValidateWithMasterTable((txtVehicleNo.Text), "NAME", "VEHICLE_TYPE", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            'MsgInformation("Invalid Vehicle No")
            'Cancel = True
            'GoTo EventExitSub
        Else
            txtVehicleType.Text = MasterNo
        End If

        'If lblBookType.Text = "L" Then
        '    If Val(txtTripAmount.Text) = 0 Then
        '        txtTripAmount.Text = CStr(GetVehicleRate(txtVehicleNo.Text, "T", IIf(optFreightType(0).Checked = True, "R", "P")))
        '    End If
        '    txtOthCharges.Text = CStr(GetVehicleRate(txtVehicleNo.Text, "P", IIf(optFreightType(0).Checked = True, "R", "P")))
        'End If

        If MainClass.ValidateWithMasterTable(txtVehicleNo.Text, "NAME", "VEHICLE_OWNER", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            If MasterNo = "3" Then
                chkThirdParty.CheckState = System.Windows.Forms.CheckState.Checked
            Else
                chkThirdParty.CheckState = System.Windows.Forms.CheckState.Unchecked
            End If
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function GetVehicleRate(ByRef mVehicleNo As String, ByRef mFieldType As String, ByRef mFreightType As String, ByRef pWt As Double) As Double

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCustomerCode As String
        Dim mCustName As String
        Dim mField1 As String = ""
        Dim mField2 As String = ""
        Dim mField3 As String = ""
        Dim mField4 As String = ""
        Dim mPoint As Double

        GetVehicleRate = 0
        If Trim(mVehicleNo) = "" Then
            GetVehicleRate = 0
            Exit Function
        End If

        SprdMain.Row = 1
        SprdMain.Col = ColCustName
        mCustName = Trim(SprdMain.Text)

        If mCustName = "" Then
            GetVehicleRate = 0
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(mCustName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCustomerCode = Trim(MasterNo)
            Else
                GetVehicleRate = 0
                Exit Function
            End If
        End If

        If mFieldType = "T" Then
            mField1 = "TRIP_RATE"
            mField2 = IIf(mFreightType = "R", "DEFAULT_TRIP_RATE", "PREMIUM_RATE")

            mField3 = "PER_KG_RATE"
            mField4 = "DEFAULT_PER_KG_RATE"

        ElseIf mFieldType = "P" Then
            mPoint = GetVehiclePoint(mCustName)
            If mPoint = 0 Then
                GetVehicleRate = 0
                Exit Function
            End If
            mField1 = "POINT_RATE"
            mField2 = "DEFAULT_POINT_RATE"
            Exit Function
        End If
        SqlStr = "SELECT " & mField1 & " AS TRIP_RATE, " & mField3 & " AS PER_KG_RATE FROM FIN_VEHICLE_RATE_DET" & vbCrLf _
            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf _
            & " AND VEHICLE_NO = '" & MainClass.AllowSingleQuote(mVehicleNo) & "'" & vbCrLf _
            & " AND WEF = (" & vbCrLf _
            & " SELECT MAX(WEF) FROM FIN_VEHICLE_RATE_DET" & vbCrLf _
            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf _
            & " AND VEHICLE_NO = '" & MainClass.AllowSingleQuote(mVehicleNo) & "'" & vbCrLf _
            & " AND WEF <= TO_DATE('" & VB6.Format(txtSlipDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetVehicleRate = IIf(IsDBNull(RsTemp.Fields("TRIP_RATE").Value), 0, RsTemp.Fields("TRIP_RATE").Value)
            If GetVehicleRate = 0 Then
                GetVehicleRate = pWt * IIf(IsDBNull(RsTemp.Fields("PER_KG_RATE").Value), 0, RsTemp.Fields("PER_KG_RATE").Value)
            End If
        Else
            SqlStr = "SELECT " & mField1 & " AS TRIP_RATE, " & mField3 & " AS PER_KG_RATE  FROM FIN_VEHICLE_TP_RATE_DET" & vbCrLf _
                & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf _
                & " AND TRANSPORT_NAME = '" & MainClass.AllowSingleQuote(txtTransporterName.Text) & "'" & vbCrLf _
                & " --AND VT_NAME = '" & MainClass.AllowSingleQuote(txtVehicleType.Text) & "'" & vbCrLf _
                & " AND WEF = (" & vbCrLf _
                & " SELECT MAX(WEF) FROM FIN_VEHICLE_TP_RATE_DET" & vbCrLf _
                & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf _
                & " AND TRANSPORT_NAME = '" & MainClass.AllowSingleQuote(txtTransporterName.Text) & "'" & vbCrLf _
                & " --AND VT_NAME = '" & MainClass.AllowSingleQuote(txtVehicleType.Text) & "'" & vbCrLf _
                & " AND WEF <= TO_DATE('" & VB6.Format(txtSlipDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                GetVehicleRate = IIf(IsDBNull(RsTemp.Fields("TRIP_RATE").Value), 0, RsTemp.Fields("TRIP_RATE").Value)
                If GetVehicleRate = 0 Then
                    GetVehicleRate = pWt * IIf(IsDBNull(RsTemp.Fields("PER_KG_RATE").Value), 0, RsTemp.Fields("PER_KG_RATE").Value)
                End If
            Else
                SqlStr = "SELECT " & mField2 & " AS TRIP_RATE , " & mField4 & " AS DEFAULT_PER_KG_RATE FROM FIN_VEHICLE_RATE_HDR" & vbCrLf _
                    & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf _
                    & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) FROM FIN_VEHICLE_RATE_HDR" & vbCrLf _
                    & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf _
                    & " AND WEF <= TO_DATE('" & VB6.Format(txtSlipDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    GetVehicleRate = IIf(IsDBNull(RsTemp.Fields("TRIP_RATE").Value), 0, RsTemp.Fields("TRIP_RATE").Value)
                    If GetVehicleRate = 0 Then
                        GetVehicleRate = pWt * IIf(IsDBNull(RsTemp.Fields("DEFAULT_PER_KG_RATE").Value), 0, RsTemp.Fields("DEFAULT_PER_KG_RATE").Value)
                    End If
                End If
            End If
        End If
        If mFieldType = "P" Then
            GetVehicleRate = GetVehicleRate * mPoint
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
    End Function

    Private Function GetVehiclePoint(ByRef mCustName As String) As Double
        On Error GoTo ERR1
        Dim cntRow As Integer
        Dim mCheckCustName As String
        Dim mCheckCustNameStr As String
        GetVehiclePoint = 0
        mCheckCustNameStr = mCustName
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCustName
                mCheckCustName = Trim(.Text)
                If InStr(1, mCheckCustNameStr, mCheckCustName) = 0 Then
                    mCheckCustNameStr = mCheckCustNameStr & "," & mCheckCustName
                    GetVehiclePoint = GetVehiclePoint + 1
                End If
            Next
        End With
        Exit Function
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
    End Function
    Private Sub txtVehicleType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicleType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtVehicleType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicleType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicleType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        If eventArgs.row = 0 And eventArgs.col = ColPackType Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColPackType
                If MainClass.SearchGridMaster(.Text, "DSP_PACKINGTYPE_MST", "NAME", "", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColPackType
                    .Text = AcName
                End If
            End With
        End If
    End Sub
    Private Sub txtGrossWt_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGrossWt.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtGrossWt_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGrossWt.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtGrossWt_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGrossWt.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtTearWt_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTearWt.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTearWt_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTearWt.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTearWt_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTearWt.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SprdMainOth_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMainOth.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMainOth_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMainOth.LeaveCell

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mPacks As Double
        Dim mPacksRecd As Double

        If eventArgs.newRow = -1 Then Exit Sub

        cntRow = SprdMainOth.ActiveRow
        SprdMainOth.Row = cntRow

        Select Case eventArgs.col
            Case ColRefNo, ColItemDescOth
                MainClass.AddBlankSprdRow(SprdMainOth, ColRefNo, ConRowHeight)
                FormatSprdMain(SprdMainOth.MaxRows)

        End Select

        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdMainOth_ClickEvent(sender As Object, EventArgs As _DSpreadEvents_ClickEvent) Handles SprdMainOth.ClickEvent
        On Error GoTo ERR1
        Select Case EventArgs.col
            Case 0
                If EventArgs.row > 0 And SprdMainOth.Enabled = True Then
                    MainClass.DeleteSprdRow(SprdMainOth, EventArgs.row, ColRefNo)
                    MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
                    CalcTots()
                End If
        End Select
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPopulateBillAll_Click(sender As Object, e As EventArgs) Handles cmdPopulateBillAll.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim xLoadingNo As Double
        Dim mRefType As String = ""
        Dim mRejDocType As String
        Dim mApplicableDate As String
        Dim mStartDate As String
        Dim mRefNo As String
        Dim mItemCode As String

        mRejDocType = IIf(IsDBNull(RsCompany.Fields("REJECTION_DOCTYPE").Value), "D", RsCompany.Fields("REJECTION_DOCTYPE").Value)
        mApplicableDate = IIf(IsDBNull(RsCompany.Fields("REJ_APPLICABLEDATE").Value), "", RsCompany.Fields("REJ_APPLICABLEDATE").Value)

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            mStartDate = "18/04/2023"
        Else
            mStartDate = "17/11/2023"
        End If


        If Trim(txtVehicleNo.Text) = "" Then Exit Sub

        If optShow(0).Checked = True Then
            mRefType = "I"
        ElseIf optShow(1).Checked = True Then
            mRefType = "R"
        ElseIf optShow(2).Checked = True Then
            mRefType = "M"
        ElseIf optShow(3).Checked = True Then
            mRefType = "G"
        ElseIf optShow(4).Checked = True Then
            mRefType = "D"
        End If

        If mRefType = "D" Then
            If mRejDocType = "D" Or mApplicableDate = "" Then

            Else
                MsgBox("Please Made the Invoice for this Despatch Note.")
                Exit Sub
            End If
        End If



        'If AlreadyLoad(Trim(txtRefNo.Text), mRefType, xLoadingNo) = True Then
        '    MsgBox("Already made a Loading Slip of Such Ref No. Loading Slip No : " & xLoadingNo)
        '    Exit Sub
        'End If
        ''IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''& " AND 

        If optShow(0).Checked = True Then
            SqlStr = " SELECT 'I' AS REF_TYPE, IH.BILLNO As REF_NO, IH.INVOICE_DATE AS REF_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, SUM(ITEM_AMT) AS ITEM_AMT," & vbCrLf _
                & " IMST.ITEM_SHORT_DESC, ID.ITEM_UOM, SUM(ITEM_QTY) AS ITEM_QTY, IMST.PACK_STD, IMST.ITEM_WEIGHT, VEHICLENO , CARRIERS,sum(INNER_PACK_QTY) as INNER_PACK_QTY, ID.PACK_TYPE" & vbCrLf _
                & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
                & " AND ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
                & " AND IH.VEHICLENO='" & (txtVehicleNo.Text) & "'" & vbCrLf _
                & " AND IH.BILLNO NOT IN ("

            SqlStr = SqlStr & vbCrLf _
                & " SELECT DISTINCT ID.REF_NO" & vbCrLf _
                & " FROM DSP_LOADING_HDR IH, DSP_LOADING_DET ID" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.AUTO_KEY_LOAD=ID.AUTO_KEY_LOAD" & vbCrLf _
                & " AND ID.REF_TYPE='I')"

            SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf _
                & " GROUP BY IH.BILLNO, IH.INVOICE_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, ID.ITEM_UOM,IMST.PACK_STD,IMST.ITEM_WEIGHT,VEHICLENO , CARRIERS, ID.PACK_TYPE" & vbCrLf _
                & " ORDER BY IH.INVOICE_DATE,IH.BILLNO,ID.ITEM_CODE"

        ElseIf optShow(1).Checked = True Then
            SqlStr = " Select 'R' AS REF_TYPE, IH.AUTO_KEY_PASSNO As REF_NO, IH.GATEPASS_DATE AS REF_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, SUM(AMOUNT) AS ITEM_AMT," & vbCrLf _
                & " IMST.ITEM_SHORT_DESC, ID.ITEM_UOM, SUM(ITEM_QTY) AS ITEM_QTY,IMST.PACK_STD, IMST.ITEM_WEIGHT, '' AS VEHICLENO , '' AS CARRIERS, '' AS INNER_PACK_QTY, '' AS PACK_TYPE" & vbCrLf _
                & " FROM INV_GATEPASS_HDR IH, INV_GATEPASS_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_PASSNO=ID.AUTO_KEY_PASSNO" & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
                & " AND ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
                & " AND IH.AUTO_KEY_PASSNO=" & Val(txtRefNo.Text) & "" & vbCrLf _
                & " GROUP BY IH.AUTO_KEY_PASSNO, IH.GATEPASS_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, ID.ITEM_UOM,IMST.PACK_STD,IMST.ITEM_WEIGHT" & vbCrLf _
                & " ORDER BY IH.GATEPASS_DATE, IH.AUTO_KEY_PASSNO, ID.ITEM_CODE"

        ElseIf optShow(2).Checked = True Then
            SqlStr = " SELECT 'M' AS REF_TYPE, IH.AUTO_KEY_MRR As REF_NO, IH.MRR_DATE AS REF_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, SUM(ITEM_RATE * BILL_QTY) AS ITEM_AMT," & vbCrLf _
                & " IMST.ITEM_SHORT_DESC, ID.ITEM_UOM, SUM(BILL_QTY) AS ITEM_QTY,IMST.PACK_STD, IMST.ITEM_WEIGHT, '' AS VEHICLENO , '' AS CARRIERS, '' AS INNER_PACK_QTY, '' AS PACK_TYPE" & vbCrLf _
                & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
                & " AND ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
                & " AND IH.AUTO_KEY_MRR=" & Val(txtRefNo.Text) & "" & vbCrLf _
                & " GROUP BY IH.AUTO_KEY_MRR, IH.MRR_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, ID.ITEM_UOM,IMST.PACK_STD,IMST.ITEM_WEIGHT" & vbCrLf _
                & " ORDER BY IH.MRR_DATE, IH.AUTO_KEY_MRR, ID.ITEM_CODE"

        ElseIf optShow(3).Checked = True Then
            SqlStr = " SELECT 'G' AS REF_TYPE, IH.AUTO_KEY_GATE As REF_NO, IH.GATE_DATE AS REF_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, SUM(ITEM_RATE * BILL_QTY) AS ITEM_AMT," & vbCrLf _
                & " IMST.ITEM_SHORT_DESC, ID.ITEM_UOM, SUM(BILL_QTY) AS ITEM_QTY,IMST.PACK_STD, IMST.ITEM_WEIGHT" & vbCrLf _
                & " FROM INV_GATEENTRY_HDR IH, INV_GATEENTRY_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST, '' AS VEHICLENO , '' AS CARRIERS, '' AS INNER_PACK_QTY, '' AS PACK_TYPE" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.AUTO_KEY_GATE=ID.AUTO_KEY_GATE" & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
                & " AND ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
                & " AND IH.AUTO_KEY_GATE=" & Val(txtRefNo.Text) & "" & vbCrLf _
                & " GROUP BY IH.AUTO_KEY_GATE, IH.GATE_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, ID.ITEM_UOM,IMST.PACK_STD,IMST.ITEM_WEIGHT" & vbCrLf _
                & " ORDER BY IH.GATE_DATE, IH.AUTO_KEY_GATE, ID.ITEM_CODE"

        ElseIf optShow(4).Checked = True Then
            SqlStr = " SELECT 'D' AS REF_TYPE, IH.AUTO_KEY_DESP As REF_NO, IH.DESP_DATE AS REF_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, 0 AS ITEM_AMT," & vbCrLf _
                & " IMST.ITEM_SHORT_DESC, ID.ITEM_UOM, SUM(PACKED_QTY) AS ITEM_QTY,IMST.PACK_STD, IMST.ITEM_WEIGHT, '' AS VEHICLENO , '' AS CARRIERS, '' AS INNER_PACK_QTY, '' AS PACK_TYPE" & vbCrLf _
                & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP" & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
                & " AND ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
                & " AND IH.AUTO_KEY_DESP=" & Val(txtRefNo.Text) & " AND IH.DESP_TYPE IN ('Q','L') AND DESP_STATUS=0"

            If CDate(VB6.Format(txtSlipDate.Text, "DD/MM/YYYY")) >= CDate(PubGSTApplicableDate) Then
                SqlStr = SqlStr & vbCrLf & " AND IH.DESP_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND 1=2"
            End If

            SqlStr = SqlStr & vbCrLf & " GROUP BY IH.AUTO_KEY_DESP, IH.DESP_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, ID.ITEM_UOM,IMST.PACK_STD,IMST.ITEM_WEIGHT" & vbCrLf _
                & " ORDER BY IH.AUTO_KEY_DESP, IH.DESP_DATE, ID.ITEM_CODE"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        I = SprdMain.MaxRows

        If RsTemp.EOF = False Then
            If Trim(txtVehicleNo.Text) = "" Then
                txtVehicleNo.Text = IIf(IsDBNull(RsTemp.Fields("VEHICLENO").Value), "", RsTemp.Fields("VEHICLENO").Value)
            End If

            If Trim(txtTransporterName.Text) = "" Then
                txtTransporterName.Text = IIf(IsDBNull(RsTemp.Fields("CARRIERS").Value), "", RsTemp.Fields("CARRIERS").Value)
            End If

            Do While RsTemp.EOF = False
                SprdMain.Row = I

                mRefNo = IIf(IsDBNull(RsTemp.Fields("REF_NO").Value), "", RsTemp.Fields("REF_NO").Value)
                mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                If DuplicateDatainGrid(mRefNo, mRefType, mItemCode) = True Then
                    'MsgBox("Duplicate Ref No.")
                    GoTo GotoNextRow
                End If

                SprdMain.Col = ColRefType
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("REF_TYPE").Value), "", RsTemp.Fields("REF_TYPE").Value)

                SprdMain.Col = ColBillCheck
                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)


                SprdMain.Col = ColRefNo
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("REF_NO").Value), "", RsTemp.Fields("REF_NO").Value)

                SprdMain.Col = ColRefDate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value), "DD/MM/YYYY")


                SprdMain.Col = ColCustName
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)

                SprdMain.Col = ColItemCode
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                SprdMain.Col = ColItemDesc
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)

                SprdMain.Col = ColUOM
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)

                SprdMain.Col = ColItemWt
                SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_WEIGHT").Value), "", RsTemp.Fields("ITEM_WEIGHT").Value), "0.00")

                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)))

                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("ITEM_AMT").Value), 0, RsTemp.Fields("ITEM_AMT").Value)))

                SprdMain.Col = ColStdPack
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("PACK_STD").Value), 0, RsTemp.Fields("PACK_STD").Value)))

                SprdMain.Col = ColPacks
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("INNER_PACK_QTY").Value), 0, RsTemp.Fields("INNER_PACK_QTY").Value)))

                SprdMain.Col = ColPackType
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("PACK_TYPE").Value), "", RsTemp.Fields("PACK_TYPE").Value)

                SprdMain.MaxRows = SprdMain.MaxRows + 1
                I = SprdMain.MaxRows
GotoNextRow:
                RsTemp.MoveNext()

            Loop
        End If

        FormatSprdMain(-1)
        txtRefNo.Text = ""
        CalcTots()

        'If lblBookType.Text = "L" Then
        '    txtTripAmount.Text = CStr(GetVehicleRate(txtVehicleNo.Text, "T", IIf(optFreightType(0).Checked = True, "R", "P")))
        '    txtOthCharges.Text = CStr(GetVehicleRate(txtVehicleNo.Text, "P", IIf(optFreightType(0).Checked = True, "R", "P")))
        'End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmLoadingSlip_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        SprdMainOth.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))

        Frame2.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        FraShow.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        FraTop.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        Frabot.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        'SprdView.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750) ''VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchTransporterMaster()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        'SqlStr = "INSERT INTO FIN_TRANSPORTER_MST (" & vbCrLf _
        '        & " COMPANY_CODE, TRANSPORTER_CODE, TRANSPORTER_NAME, TRANSPORTER_ID, " & vbCrLf _
        '        & " ADDUSER, ADDDATE, MODUSER, MODDATE" & vbCrLf _
        '        & " ) VALUES ( " & vbCrLf

        If MainClass.SearchGridMaster((txtTransporterName.Text), "FIN_TRANSPORTER_MST", "TRANSPORTER_NAME", "TRANSPORTER_ID", "", , SqlStr) = True Then
            txtTransporterName.Text = AcName
            txtTransporterName_Validating(txtTransporterName, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtTransporterName_DoubleClick(sender As Object, e As EventArgs) Handles txtTransporterName.DoubleClick
        SearchTransporterMaster()
    End Sub

    Private Sub txtTransporterName_KeyUp(sender As Object, e As KeyEventArgs) Handles txtTransporterName.KeyUp
        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchTransporterMaster()
    End Sub

    Private Sub txtTransporterName_Validating(sender As Object, e As CancelEventArgs) Handles txtTransporterName.Validating
        Dim Cancel As Boolean = e.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtTransporterName.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtTransporterName.Text), "TRANSPORTER_NAME", "TRANSPORTER_NAME", "FIN_TRANSPORTER_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Transporter Name")
            Cancel = True
            GoTo EventExitSub
        Else
            txtTransporterName.Text = MasterNo
        End If


        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        e.Cancel = Cancel
    End Sub
    Private Sub SprdMain_KeyPressEvent(sender As Object, e As _DSpreadEvents_KeyPressEvent) Handles SprdMain.KeyPressEvent
        'Dim KeyAscii As Short = Asc(e.keyAscii)

        'KeyAscii = MainClass.SetNumericField(KeyAscii)
        'EventArgs.KeyChar = Chr(KeyAscii)
        'If KeyAscii = 67 Then
        '    EventArgs.Handled = True
        'End If

        If e.keyAscii = 6 Then
            SprdMain.Row = 1
            SprdMain.Row2 = SprdMain.MaxRows
            SprdMain.Col = 1
            SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
            SprdMain.BlockMode = True
            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            SprdMain.BlockMode = False

            mSearchKey = ""
            cntSearchRow = 1
            cntSearchCol = 1
            mSearchKey = InputBox("Search :", "Search", mSearchKey)
            If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColRefNo)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F3 And mSearchKey <> "" Then
            If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColRefNo)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
        SprdMain.Refresh()
    End Sub

    Private Sub cmdShowBarcode_Click(sender As Object, e As EventArgs) Handles cmdShowBarcode.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim xLoadingNo As Double
        Dim mRefType As String = ""
        Dim mRejDocType As String
        Dim mApplicableDate As String
        Dim mRefNo As String = ""
        Dim mSeprator As String = ""
        Dim mString As String = ""
        Dim mCheckString As String = ""
        Dim mPartNo As String = ""
        Dim mQty As Double = 0
        Dim mBoxNo As Double = 0
        Dim pLineNo As String
        Dim mRate As Double
        Dim mRunningQty As Double

        Dim mInvQty As Double

        mRejDocType = IIf(IsDBNull(RsCompany.Fields("REJECTION_DOCTYPE").Value), "D", RsCompany.Fields("REJECTION_DOCTYPE").Value)
        mApplicableDate = IIf(IsDBNull(RsCompany.Fields("REJ_APPLICABLEDATE").Value), "", RsCompany.Fields("REJ_APPLICABLEDATE").Value)

        mSeprator = "#"
        mString = UCase(Trim(txtBarCode.Text))

        txtBarCode.Text = ""

        If mString = "" Then Exit Sub
        '232SI-034722#14515KRE G001#300#1

        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        mRefNo = mCheckString


        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        mPartNo = mCheckString


        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        mQty = Val(mCheckString)

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = mString   '' Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        mBoxNo = Val(mCheckString)

        If Trim(mRefNo) = "" Then Exit Sub
        mRefType = "I"

        If mRefType = "D" Then
            If mRejDocType = "D" Or mApplicableDate = "" Then

            Else
                MsgBox("Please Made the Invoice for this Despatch Note.")
                Exit Sub
            End If
        End If

        'If DuplicateDatainGrid(txtRefNo.Text, mRefType, "") = True Then
        '    MsgBox("Duplicate Ref No.")
        '    Exit Sub
        'End If

        pLineNo = GetLineNo_ItemPartNo(mRefNo, mRefType, mPartNo)

        Dim strArray() As String
        Dim pPackScanned As String

        If pLineNo > 0 Then
            SprdMain.Row = pLineNo
            SprdMain.Col = ColPackScanned
            pPackScanned = Trim(SprdMain.Text)

            strArray = Split(pPackScanned, ",")

            For y = 0 To UBound(strArray)
                If Trim(pPackScanned) <> "" Then
                    If mBoxNo = Val(strArray(y)) Then
                        MsgInformation("Aready Scanned.")
                        Exit Sub
                    End If
                End If
            Next y

        End If
        'If AlreadyLoad(Trim(txtRefNo.Text), mRefType, xLoadingNo) = True Then
        '    MsgBox("Already made a Loading Slip of Such Ref No. Loading Slip No : " & xLoadingNo)
        '    Exit Sub
        'End If



        SqlStr = " SELECT 'I' AS REF_TYPE, IH.BILLNO As REF_NO, IH.INVOICE_DATE AS REF_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, " & vbCrLf _
            & " IMST.ITEM_SHORT_DESC, ID.CUSTOMER_PART_NO, ID.ITEM_UOM, IMST.PACK_STD, ID.ITEM_QTY, IMST.ITEM_WEIGHT, VEHICLENO , CARRIERS,INNER_PACK_QTY, ID.PACK_TYPE, ITEM_RATE" & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
            & " AND IH.BILLNO='" & (mRefNo) & "'" & vbCrLf _
            & " AND ID.CUSTOMER_PART_NO='" & (mPartNo) & "'"

        '& vbCrLf _
        '    & " GROUP BY IH.BILLNO, IH.INVOICE_DATE, CMST.SUPP_CUST_NAME, ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, ID.ITEM_UOM,IMST.PACK_STD,IMST.ITEM_WEIGHT,VEHICLENO , CARRIERS, INNER_PACK_QTY, ID.PACK_TYPE" & vbCrLf _
        '    & " ORDER BY ID.ITEM_CODE"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        I = IIf(pLineNo = 0, SprdMain.MaxRows, pLineNo)

        If RsTemp.EOF = False Then
            If Trim(txtVehicleNo.Text) = "" Then
                txtVehicleNo.Text = IIf(IsDBNull(RsTemp.Fields("VEHICLENO").Value), "", RsTemp.Fields("VEHICLENO").Value)
            End If

            If Trim(txtTransporterName.Text) = "" Then
                txtTransporterName.Text = IIf(IsDBNull(RsTemp.Fields("CARRIERS").Value), "", RsTemp.Fields("CARRIERS").Value)
            End If

            Do While RsTemp.EOF = False
                SprdMain.Row = I

                SprdMain.Col = ColRefType
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("REF_TYPE").Value), "", RsTemp.Fields("REF_TYPE").Value)

                SprdMain.Col = ColBillCheck
                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)

                SprdMain.Col = ColRefNo
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("REF_NO").Value), "", RsTemp.Fields("REF_NO").Value)

                SprdMain.Col = ColRefDate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value), "DD/MM/YYYY")


                SprdMain.Col = ColCustName
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)

                SprdMain.Col = ColItemCode
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                SprdMain.Col = ColItemDesc
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)

                SprdMain.Col = ColItemPartNo
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value)

                SprdMain.Col = ColUOM
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)

                SprdMain.Col = ColItemWt
                SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_WEIGHT").Value), "", RsTemp.Fields("ITEM_WEIGHT").Value), "0.00")

                mInvQty = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), "", RsTemp.Fields("ITEM_QTY").Value), "0.00")

                SprdMain.Col = ColQty
                SprdMain.Text = Val(SprdMain.Text) + Val(mQty)
                mRunningQty = Val(SprdMain.Text)

                mRate = IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)

                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(mRunningQty * mRate)

                SprdMain.Col = ColStdPack
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("PACK_STD").Value), 0, RsTemp.Fields("PACK_STD").Value)))

                SprdMain.Col = ColPacks
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("INNER_PACK_QTY").Value), 0, RsTemp.Fields("INNER_PACK_QTY").Value)))

                SprdMain.Col = ColPackType
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("PACK_TYPE").Value), "", RsTemp.Fields("PACK_TYPE").Value)

                SprdMain.Col = ColPackScanned
                SprdMain.Text = IIf(SprdMain.Text = "", mBoxNo, SprdMain.Text & "," & mBoxNo)

                RsTemp.MoveNext()
                SprdMain.MaxRows = SprdMain.MaxRows + IIf(pLineNo = 0, 1, 0)
                I = SprdMain.MaxRows
            Loop
        End If

        FormatSprdMain(-1)
        txtRefNo.Text = ""
        CalcTots()

        'If lblBookType.Text = "L" Then
        '    txtTripAmount.Text = CStr(GetVehicleRate(txtVehicleNo.Text, "T", IIf(optFreightType(0).Checked = True, "R", "P")))
        '    txtOthCharges.Text = CStr(GetVehicleRate(txtVehicleNo.Text, "P", IIf(optFreightType(0).Checked = True, "R", "P")))
        'End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
