Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
Imports System.Data.OleDb
Imports FPSpreadADO
'Imports Infragistics.Win.UltraWinTabControl
Friend Class frmSalesOrderGST
    Inherits System.Windows.Forms.Form
    Dim RsSOMain As ADODB.Recordset ''ADODB.Recordset
    Dim RsSODetail As ADODB.Recordset ''ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim cntSearchRow As Long
    Dim cntSearchCol As Long

    Dim mSearchKey As String
    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String


    Private Const ConRowHeight As Short = 14
    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColItemUOM As Short = 3
    Private Const ColHSNCode As Short = 4
    Private Const ColPartNo As Short = 5
    Private Const ColItemSNo As Short = 6
    Private Const ColAddItemDesc As Short = 7
    Private Const ColCustStoreLoc As Short = 8
    Private Const ColPreviousItemRate As Short = 9
    Private Const ColItemRate As Short = 10
    Private Const ColMRP As Short = 11
    Private Const ColPO_WEF As Short = 12
    Private Const ColValidQty As Short = 13
    Private Const ColValidDate As Short = 14
    Private Const ColMSPCost As Short = 15
    Private Const ColMSPCostAdd As Short = 16
    Private Const ColFreightCost As Short = 17
    Private Const ColMTRCOST As Short = 18
    Private Const ColProcessCost As Short = 19
    Private Const ColCGSTPer As Short = 20
    Private Const ColSGSTPer As Short = 21
    Private Const ColIGSTPer As Short = 22
    Private Const ColAccountName As Short = 23
    Private Const ColSOStatus As Short = 24
    Private Const colRemarks As Short = 25


    Dim FileDBCn As ADODB.Connection
    Dim mSearchStartRow As Integer

    Private Sub cboInvType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInvType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboInvType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInvType.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboOrderType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboOrderType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboOrderType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboOrderType.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub chkApproved_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkApproved.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkDI_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDI.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            If lblAddItem.Text = "Y" Then
                MsgInformation("You Cann't be add in this form.")
                Exit Sub
            End If
            ADDMode = True
            MODIFYMode = False
            Clear1()
            'SprdMain.Enabled = True
            cboInvType.Enabled = True
            txtSONo.Enabled = False
            cmdSearchAmend.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsSOMain.EOF = False Then RsSOMain.MoveFirst()
            Show1()
            txtSONo.Enabled = True
            cmdSearchAmend.Enabled = True
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub cmdAmend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAmend.Click

        Dim mSoNo As Double
        Dim I As Integer
        'Dim pCurrRate As Double
        'Dim mPrice As Double
        'Dim mDisc As Double
        Dim mItemCode As String

        mSoNo = Val(txtSONo.Text)

        If mSoNo = 0 Then
            MsgInformation("Please Select SO.")
            Exit Sub
        End If

        Call txtSONo_Validating(txtSONo, New System.ComponentModel.CancelEventArgs(True))

        '    If CheckUnPostedPO(mPONo) = True Then
        '        txtPONo.Enabled = True
        '        CmdSearchPO.Enabled = True
        '        cmdSearchAmend.Enabled = True
        '        cmdSearchAmend.SetFocus
        '        Exit Sub
        '    End If

        If chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MsgInformation("Please Approved First before Amendment..")
            Exit Sub
        End If

        txtAmendNo.Text = CStr(GetMaxAmendNo(mSoNo))
        txtAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If mItemCode <> "" Then
                    .Col = ColPreviousItemRate
                    .Text = CStr(GetPreviousRate((txtCode.Text), Val(txtSONo.Text), Val(txtAmendNo.Text), mItemCode))
                End If

            Next
        End With

        txtCustomerName.Enabled = False
        cmdsearch.Enabled = False

        txtShipCustomer.Enabled = False
        cmdsearchShipCust.Enabled = False

        chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked
        cmdAmend.Enabled = False
        cmdAmendExcel.Enabled = True

        ADDMode = True
        MODIFYMode = False
        SprdMain.Enabled = True


        SprdAnnex.Enabled = True
        txtSONo.Enabled = False
        cmdSearchAmend.Enabled = False
        MainClass.ButtonStatus(Me, XRIGHT, RsSOMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cmdAmendExcel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAmendExcel.Click
        On Error GoTo ErrPart
        Dim strFilePath As String

        strFilePath = My.Application.Info.DirectoryPath
        If Not fOpenFile(strFilePath, "*.xls", "Excel Data", CommonDialogOpen) Then
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
        Dim cntRow As Integer
        Dim mSqlStr As String
        Dim mItemCode As String
        Dim mChkItemCode As String

        Dim mItemDesc As String
        Dim mUOM As String
        Dim mRate As String
        Dim mWEF As String

        Dim mPreviousItemRate As Double
        Dim mItemRate As Double

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset

        Dim FileConnStr As String

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""

        Dim mTempFile As String

        mTempFile = Mid(strXLSFile, 1, Len(strXLSFile) - 4) & "_Temp" & ".xls"

        CopyFile(strXLSFile, mTempFile, 0)

        FileConnStr = "Provider=MSDASQL.1;Connect Timeout=15;Extended Properties='DSN=Excel Files;DBQ=XXLSFILEX;DefaultDir=XXLSDIRX;DriverId=790;FIL=excel 8.0;MaxBufferSize=2048;PageTimeout=5;UID=admin;';Locale Identifier=1033"
        FileConnStr = Replace(FileConnStr, "XXLSFILEX", mTempFile)
        strTemp = Mid(mTempFile, 1, InStrRev(mTempFile, "\") - 1)
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
                    mChkItemCode = Trim(IIf(IsDBNull(RsFile.Fields(0).Value), "", RsFile.Fields(0).Value))
                    mRate = VB6.Format(Trim(IIf(IsDBNull(RsFile.Fields(2).Value), 0, RsFile.Fields(2).Value)), "0.0000")
                    mWEF = VB6.Format(Trim(IIf(IsDBNull(RsFile.Fields(3).Value), "", RsFile.Fields(3).Value)), "DD-MMM-YYYY")

                    With SprdMain
                        For cntRow = 1 To .MaxRows
                            .Row = cntRow
                            .Col = ColItemCode
                            mItemCode = Trim(.Text)
                            If mItemCode = mChkItemCode Then
                                .Row = cntRow
                                .Col = ColItemRate
                                .Text = VB6.Format(mRate, "0.0000")

                                .Col = ColPO_WEF
                                .Text = VB6.Format(mWEF, "DD/MM/YYYY")

                                Exit For
                            End If
                        Next
                    End With

                    RsFile.MoveNext()
                Loop
            End If
        End If

        If RsFile.State = ADODB.ObjectStateEnum.adStateOpen Then RsFile.Close()
        RsFile = Nothing

        If FileDBCn.State = ADODB.ObjectStateEnum.adStateOpen Then
            FileDBCn.Close()
            FileDBCn = Nothing
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColPreviousItemRate
                mPreviousItemRate = Val(.Text)

                .Col = ColItemRate
                mItemRate = Val(.Text)

                If mPreviousItemRate < mItemRate And mPreviousItemRate > 0 Then ''Increase
                    SprdMain.Row = cntRow
                    SprdMain.Row2 = cntRow
                    SprdMain.Col = 1
                    SprdMain.Col2 = colRemarks
                    SprdMain.BlockMode = True
                    SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFC0)
                    SprdMain.BlockMode = False
                ElseIf mPreviousItemRate > mItemRate And mPreviousItemRate > 0 Then  ''Decrease
                    SprdMain.Row = cntRow
                    SprdMain.Row2 = cntRow
                    SprdMain.Col = 1
                    SprdMain.Col2 = colRemarks
                    SprdMain.BlockMode = True
                    SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0C0FF)
                    SprdMain.BlockMode = False
                Else ''Not Change
                    SprdMain.Row = cntRow
                    SprdMain.Row2 = cntRow
                    SprdMain.Col = 1
                    SprdMain.Col2 = colRemarks
                    SprdMain.BlockMode = True
                    SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                    SprdMain.BlockMode = False
                End If
            Next
        End With
        'red=&H00C0C0FF&
        'g=&H00C0FFC0&
        '    CmdPopFromFile.Enabled = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Function GetMaxAmendNo(ByRef pSONo As Double) As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT MAX(TO_NUMBER(AMEND_NO)) AS AMEND_NO" & vbCrLf & " FROM DSP_SALEORDER_HDR" & vbCrLf & " WHERE AUTO_KEY_SO=" & Val(CStr(pSONo)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDBNull(RsTemp.Fields("AMEND_NO").Value) Then
                GetMaxAmendNo = 0
            Else
                GetMaxAmendNo = Val(RsTemp.Fields("AMEND_NO").Value) + 1
            End If
        Else
            GetMaxAmendNo = 0
        End If

        Exit Function
ErrPart:
        GetMaxAmendNo = Val(txtAmendNo.Text)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click

        On Error GoTo DelErrPart
        Dim SqlStr As String = ""

        If ValidateBranchLocking((txtAmendDate.Text)) = True Then
            Exit Sub
        End If
        If ValidateBookLocking(PubDBCn, CInt(ConLockSO), txtSODate.Text) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, (txtSODate.Text), (txtCustomerName.Text)) = True Then
            Exit Sub
        End If

        If chkApproved.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Approved SO Cann't be Deleted")
            Exit Sub
        End If

        If VB.Left(cboStatus.Text, 1) = "C" Then
            MsgInformation("Closed PO Cann't be Deleted")
            Exit Sub
        End If

        If txtSONo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsSOMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "DSP_SALEORDER_HDR", (txtSONo.Text), RsSOMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "DSP_SALEORDER_HDR", "MKEY", (lblMkey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM DSP_SALEORDER_DET WHERE MKEY=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM DSP_SALEORDER_HDR WHERE MKEY=" & Val(lblMkey.Text) & "")

                SqlStr = " UPDATE DSP_SALEORDER_HDR SET SO_STATUS='O', " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_SO=" & Val(txtSONo.Text) & "" & vbCrLf & " AND AMEND_NO=" & Val(txtAmendNo.Text) - 1 & "" & vbCrLf
                PubDBCn.Execute(SqlStr)

                PubDBCn.CommitTrans()
                RsSOMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsSOMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        '    If chkStatus.Value = vbChecked Then
        '        MsgInformation "Posted PO Cann't be Modified"
        '        Exit Sub
        '    End If

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsSOMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtSONo.Enabled = False
            cmdSearchAmend.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        '    Screen.MousePointer = 11
        '    Call ReportOnPO(crptToWindow)
        '    Screen.MousePointer = 0
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        '    Screen.MousePointer = 11
        '    Call ReportOnPO(crptToPrinter)
        '    Screen.MousePointer = 0
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtSONo_Validating(txtSONo, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            GoTo ErrorHandler
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cboPOType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPOType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboPOType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPOType.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mSoNo As Double
        Dim mStatus As String
        Dim mOrderType As String
        Dim mApproved As String
        Dim mSACCode As String
        Dim mShipToCustCode As String
        Dim mShipToSameBillTo As String
        Dim mDI As String
        Dim mPOType As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mStatus = VB.Left(cboStatus.Text, 1)
        mPOType = VB.Left(cboPOType.Text, 1)
        mOrderType = VB.Left(cboOrderType.Text, 1)
        mApproved = IIf(chkApproved.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mDI = IIf(chkDI.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If MainClass.ValidateWithMasterTable((txtCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            If UpdateSuppMst((txtCode.Text)) = False Then GoTo ErrPart
        End If


        mSACCode = ""
        If MainClass.ValidateWithMasterTable((txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
            mSACCode = MasterNo
        End If

        mShipToSameBillTo = IIf(chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If mShipToSameBillTo = "Y" Then
            mShipToCustCode = Trim(txtCode.Text)
            txtShipTo.Text = txtBillTo.Text
        Else
            mShipToCustCode = ""
            If MainClass.ValidateWithMasterTable((txtShipCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')") = True Then
                mShipToCustCode = MasterNo
            End If
        End If

        SqlStr = ""
        mSoNo = Val(txtSONo.Text)
        If Val(txtSONo.Text) = 0 Then
            mSoNo = AutoGenPONoSeq()
        End If

        If ADDMode = True Then
            lblMkey.Text = mSoNo & VB6.Format(Val(txtAmendNo.Text), "000")

            SqlStr = " INSERT INTO DSP_SALEORDER_HDR ( " & vbCrLf & " MKEY, AUTO_KEY_SO,  COMPANY_CODE," & vbCrLf _
                & " SO_DATE, SUPP_CUST_CODE, CUST_PO_NO, " & vbCrLf _
                & " CUST_PO_DATE, CUST_AMEND_NO, AMEND_NO, AMEND_DATE, " & vbCrLf _
                & " AMEND_WEF_FROM, " & vbCrLf & " ROAD_PERMIT, TYPE_OF_SALE," & vbCrLf _
                & " COMM_DTLS, LC_CLAIMS, INSPECTION_DTL, " & vbCrLf _
                & " DESTINATION_DTL, TRANSPORTER_DTL, MODE_OF_DELV, " & vbCrLf _
                & " FREIGHT_CHARGES, OCTROI_DTL, INSURANCE_DTL, " & vbCrLf _
                & " PAYMENT_DTL, BALANCE_PAY_DTL, DESPATCH_DTL, " & vbCrLf _
                & " SALETAX_PER, EXCISE_DUTY_PER, DISCOUNT_PER, " & vbCrLf _
                & " SO_STATUS, REMARKS, ORDER_TYPE, " & vbCrLf & " ADDUSER, ADDDATE," & vbCrLf _
                & " MODUSER, MODDATE,SO_APPROVED,GOODS_SERVICE, SAC_CODE, ISGSTENABLE_PO, EPCG_NO, EPCG_DATE," & vbCrLf _
                & " BILL_TO_LOC_ID, SHIP_TO_LOC_ID, SHIPPED_TO_PARTY_CODE, SHIPPED_TO_SAMEPARTY,DELIVERY_INSTRUCTION_REQ,PO_TYPE,VENDOR_CODE) "

            SqlStr = SqlStr & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " " & Val(lblMkey.Text) & ", " & mSoNo & ", " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtSODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtCode.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtPONo.Text)) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " " & Val(txtCustAmendNo.Text) & ", " & vbCrLf _
                & " " & Val(txtAmendNo.Text) & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), "

            SqlStr = SqlStr & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRoadPermit.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtSaleType.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtCommission.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtLCClaim.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtInspection.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDestination.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtTransporter.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDespMode.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtFreight.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtOctroi.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtInsurance.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtPayment.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtBalPayment.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDescDetail.Text) & "', " & vbCrLf _
                & " 0,0,0,'" & mStatus & "','" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','" & mOrderType & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'',''," & vbCrLf _
                & " '" & mApproved & "', '" & VB.Left(cboInvType.Text, 1) & "', '" & mSACCode & "','Y','" & MainClass.AllowSingleQuote(txtEPCGNo.Text) & "',TO_DATE('" & VB6.Format(txtEPCGDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtBillTo.Text) & "','" & MainClass.AllowSingleQuote(txtShipTo.Text) & "','" & MainClass.AllowSingleQuote(mShipToCustCode) & "','" & MainClass.AllowSingleQuote(mShipToSameBillTo) & "','" & mDI & "','" & mPOType & "','" & MainClass.AllowSingleQuote(txtVendorCode.Text) & "')"
        End If

        If MODIFYMode = True Then

            If lblAddItem.Text = "Y" Then
                SqlStr = " UPDATE DSP_SALEORDER_HDR SET " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND MKEY =" & Val(lblMkey.Text) & ""
            Else
                SqlStr = " UPDATE DSP_SALEORDER_HDR SET " & vbCrLf _
                    & " AUTO_KEY_SO=" & mSoNo & ", SO_APPROVED='" & mApproved & "',VENDOR_CODE='" & MainClass.AllowSingleQuote(txtVendorCode.Text) & "'," & vbCrLf _
                    & " SO_DATE=TO_DATE('" & VB6.Format(txtSODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf _
                    & " CUST_PO_NO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "', " & vbCrLf _
                    & " CUST_PO_DATE=TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " CUST_AMEND_NO=" & Val(txtCustAmendNo.Text) & ", " & vbCrLf _
                    & " AMEND_NO=" & Val(txtAmendNo.Text) & ", " & vbCrLf _
                    & " AMEND_DATE=TO_DATE('" & VB6.Format(txtAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " AMEND_WEF_FROM=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " SHIPPED_TO_PARTY_CODE='" & MainClass.AllowSingleQuote(mShipToCustCode) & "',SHIPPED_TO_SAMEPARTY='" & MainClass.AllowSingleQuote(mShipToSameBillTo) & "',  "

                SqlStr = SqlStr & vbCrLf _
                    & " ROAD_PERMIT='" & MainClass.AllowSingleQuote(txtRoadPermit.Text) & "', PO_TYPE='" & mPOType & "'," & vbCrLf _
                    & " TYPE_OF_SALE='" & MainClass.AllowSingleQuote(txtSaleType.Text) & "', " & vbCrLf _
                    & " COMM_DTLS='" & MainClass.AllowSingleQuote(txtCommission.Text) & "', " & vbCrLf _
                    & " LC_CLAIMS='" & MainClass.AllowSingleQuote(txtLCClaim.Text) & "', " & vbCrLf _
                    & " INSPECTION_DTL='" & MainClass.AllowSingleQuote(txtInspection.Text) & "', " & vbCrLf _
                    & " DESTINATION_DTL='" & MainClass.AllowSingleQuote(txtDestination.Text) & "', " & vbCrLf _
                    & " TRANSPORTER_DTL='" & MainClass.AllowSingleQuote(txtTransporter.Text) & "', " & vbCrLf _
                    & " MODE_OF_DELV='" & MainClass.AllowSingleQuote(txtDespMode.Text) & "', " & vbCrLf _
                    & " FREIGHT_CHARGES='" & MainClass.AllowSingleQuote(txtFreight.Text) & "', " & vbCrLf _
                    & " OCTROI_DTL='" & MainClass.AllowSingleQuote(txtOctroi.Text) & "', " & vbCrLf _
                    & " INSURANCE_DTL='" & MainClass.AllowSingleQuote(txtInsurance.Text) & "', " & vbCrLf _
                    & " PAYMENT_DTL='" & MainClass.AllowSingleQuote(txtPayment.Text) & "', " & vbCrLf _
                    & " BALANCE_PAY_DTL='" & MainClass.AllowSingleQuote(txtBalPayment.Text) & "', " & vbCrLf _
                    & " DESPATCH_DTL='" & MainClass.AllowSingleQuote(txtDescDetail.Text) & "', " & vbCrLf _
                    & " SO_STATUS='" & mStatus & "', " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf _
                    & " BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'," & vbCrLf _
                    & " SHIP_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtShipTo.Text) & "'," & vbCrLf _
                    & " ORDER_TYPE='" & mOrderType & "', DELIVERY_INSTRUCTION_REQ='" & mDI & "'," & vbCrLf _
                    & " GOODS_SERVICE='" & VB.Left(cboInvType.Text, 1) & "', SAC_CODE = '" & mSACCode & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                    & " EPCG_NO='" & MainClass.AllowSingleQuote(txtEPCGNo.Text) & "',EPCG_DATE=TO_DATE('" & VB6.Format(txtEPCGDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND MKEY =" & Val(lblMkey.Text) & ""
            End If
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1() = False Then GoTo ErrPart

        If lblAddItem.Text = "N" Then
            SqlStr = " UPDATE DSP_SALEORDER_HDR SET SO_STATUS='C', " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_SO=" & mSoNo & "" & vbCrLf & " AND AMEND_NO=" & Val(txtAmendNo.Text) - 1 & "" & vbCrLf
            PubDBCn.Execute(SqlStr)
        End If

        Update1 = True
        PubDBCn.CommitTrans()
        txtSONo.Text = CStr(mSoNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsSOMain.Requery()
        RsSODetail.Requery()
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Sub CalcTots()
        On Error GoTo ERR1
        ''Dim RsMisc As ADODB.Recordset = Nothing
        Dim mGrossAmt As Double
        Dim mQty As Double
        Dim mPrice As Double
        Dim mDisc As Double

        Dim I As Integer
        Dim j As Integer


        mGrossAmt = 0

        '    With SprdMain
        '        j = .MaxRows
        '        For I = 1 To j
        '            .Row = I
        '            mGrossAmt = 0
        '
        '            .Col = ColQty
        '            mQty = Val(.Text)
        '
        '            .Col = ColItemRate
        '            mPrice = Val(.Text)
        '
        '            .Col = ColItemDisc
        '            mDisc = Val(.Text)
        '
        '            .Col = ColGross
        '            .Text = (mQty * mPrice) - (mDisc * 0.01 * mQty * mPrice)
        '
        '        Next I
        '    End With

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Function AutoGenPONoSeq() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        mAutoGen = 1

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_SO)  " & vbCrLf _
            & " FROM DSP_SALEORDER_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_SO,LENGTH(AUTO_KEY_SO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = Mid(mMaxValue, 1, Len(mMaxValue) - 6)
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With

        AutoGenPONoSeq = mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemUOM As String
        Dim mQty As Double
        Dim mRate As Double
        Dim mPartNo As String
        Dim mPackType As String
        Dim mColorDesc As String
        Dim mMRP As Double
        Dim mPOWEF As String
        Dim mMRTCost As Double
        Dim mMSPCostAdd As Double
        Dim mProcessCost As Double
        Dim mMSPCost As Double
        Dim mFreightCost As Double
        Dim mValidQty As Double
        Dim mValidDate As String
        Dim mCGSTPer As String
        Dim mSGSTPer As String
        Dim mIGSTPer As String
        Dim mAcctCode As String = ""
        Dim mAcctName As String
        Dim mHSNCode As String
        Dim mRemarks As String
        Dim mSOStatus As String
        Dim mItemSNo As String
        Dim mAddItemDesc As String
        Dim mCustStoreLoc As String

        If lblAddItem.Text = "N" Then
            SqlStr = "Delete From  DSP_SALEORDER_DET " & vbCrLf & " Where " & vbCrLf & " MKEY=" & Val(lblMkey.Text) & ""

            PubDBCn.Execute(SqlStr)
        End If

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                If lblAddItem.Text = "Y" Then
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_CODE", "DSP_SALEORDER_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MKEY=" & Val(lblMkey.Text) & "") = True Then
                        GoTo NextRow
                    End If
                End If

                .Col = ColItemUOM
                mItemUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColHSNCode
                mHSNCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemRate
                mRate = Val(.Text)

                .Col = ColMRP
                mMRP = Val(.Text)

                .Col = ColMTRCOST
                mMRTCost = Val(.Text)

                .Col = ColProcessCost
                mProcessCost = Val(.Text)

                .Col = ColMSPCost
                mMSPCost = Val(.Text)

                .Col = ColMSPCostAdd
                mMSPCostAdd = Val(.Text)

                .Col = ColFreightCost
                mFreightCost = Val(.Text)

                .Col = ColPartNo
                mPartNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemSNo
                mItemSNo = MainClass.AllowSingleQuote(.Text)

                mPackType = ""
                mColorDesc = ""


                .Col = ColAddItemDesc
                mAddItemDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColCustStoreLoc
                mCustStoreLoc = MainClass.AllowSingleQuote(.Text)

                .Col = ColPO_WEF

                If VB.Left(cboOrderType.Text, 1) = "O" Then
                    If Trim(.Text) = "" Or Not IsDate(.Text) Then
                        mPOWEF = VB6.Format(txtWEF.Text, "DD/MM/YYYY")
                    Else
                        mPOWEF = VB6.Format(.Text, "DD/MM/YYYY")
                    End If
                Else
                    mPOWEF = VB6.Format(txtWEF.Text, "DD/MM/YYYY")
                End If

                .Col = ColValidQty
                mValidQty = Val(.Text)

                .Col = ColValidDate
                mValidDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColCGSTPer
                mCGSTPer = CStr(Val(.Text))

                .Col = ColSGSTPer
                mSGSTPer = CStr(Val(.Text))

                .Col = ColIGSTPer
                mIGSTPer = CStr(Val(.Text))

                .Col = ColAccountName
                mAcctName = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mAcctName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
                    mAcctCode = MasterNo
                End If

                .Col = ColSOStatus
                mSOStatus = Trim(.Text)

                .Col = colRemarks
                mRemarks = Trim(.Text)

                SqlStr = ""

                If mItemCode <> "" And mRate > 0 Then
                    SqlStr = " INSERT INTO DSP_SALEORDER_DET ( " & vbCrLf _
                        & " COMPANY_CODE, MKEY, SERIAL_NO, " & vbCrLf _
                        & " SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf _
                        & " UOM_CODE, PART_NO,ITEM_PRICE, " & vbCrLf _
                        & " PACK_TYPE, COLOUR_DTL, ITEM_MRP, AMEND_WEF, " & vbCrLf _
                        & " MATERIAL_COST, PROCESS_COST, MSP_COST, " & vbCrLf _
                        & " FREIGHT_COST, VALID_QTY, VALID_DATE, " & vbCrLf _
                        & " CGST_PER, SGST_PER, IGST_PER, ACCOUNT_POSTING_CODE, " & vbCrLf _
                        & " HSN_CODE, REMARKS, SO_ITEM_STATUS, ITEM_SNO, MSP_COST_ADD, ADD_ITEM_DESCRIPTION, CUST_STORE_LOC" & vbCrLf _
                        & " ) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " " & Val(lblMkey.Text) & "," & I & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "'," & vbCrLf _
                        & " '" & mItemCode & "','" & mItemUOM & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mPartNo) & "', " & mRate & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mPackType) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mColorDesc) & "', " & mMRP & ",TO_DATE('" & VB6.Format(mPOWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " " & mMRTCost & "," & mProcessCost & "," & mMSPCost & "," & mFreightCost & "," & vbCrLf _
                        & " " & mValidQty & ",TO_DATE('" & VB6.Format(mValidDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & ",'" & MainClass.AllowSingleQuote(mAcctCode) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mHSNCode) & "', '" & MainClass.AllowSingleQuote(mRemarks) & "','" & mSOStatus & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mItemSNo) & "'," & mMSPCostAdd & ",'" & MainClass.AllowSingleQuote(mAddItemDesc) & "','" & MainClass.AllowSingleQuote(mCustStoreLoc) & "') "

                    PubDBCn.Execute(SqlStr)

                    If UpdateSuppCustDet((txtCode.Text), mPartNo, mItemCode, mRate, 0, "S") = False Then GoTo UpdateDetail1
                End If
NextRow:
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" '' AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((txtCustomerName.Text), "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", "SUPP_CUST_ADDR||SUPP_CUST_CITY", SqlStr) = True Then
            txtCustomerName.Text = AcName
            txtCustomerName_Validating(txtCustomerName, New System.ComponentModel.CancelEventArgs(False))

            txtBillTo.Text = AcName2
            txtBillTo_Validating(txtBillTo, New System.ComponentModel.CancelEventArgs(False))
            If txtCode.Enabled = True Then txtCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdsearchShipCust_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchShipCust.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((txtShipCustomer.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtShipCustomer.Text = AcName
            txtShipCustomer_Validating(txtShipCustomer, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchCode()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((txtCode.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", "SUPP_CUST_NAME", , , SqlStr) = True Then
            txtCode.Text = AcName
            txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
            txtCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchAmend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAmend.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtSONo.Text) = "" Then
            Exit Sub
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_SO=" & Val(txtSONo.Text) & ""

        If MainClass.SearchGridMaster("", "DSP_SALEORDER_HDR", "AMEND_NO", "AMEND_DATE", "CUST_AMEND_NO", , SqlStr) = True Then
            txtAmendNo.Text = AcName
            txtAmendDate.Text = AcName1
            txtSONo_Validating(txtSONo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItem.Click

        On Error GoTo ErrPart
        Dim mSearchItem As String
        Dim mFindItemName As String
        Dim I As Integer

        SprdMain.Row = 1
        SprdMain.Row2 = SprdMain.MaxRows
        SprdMain.Col = 1
        SprdMain.Col2 = SprdMain.MaxCols
        SprdMain.BlockMode = True
        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
        SprdMain.BlockMode = False

        mSearchItem = Trim(txtSearchItem.Text)
        Dim counter As Short

        '        Dim ret As Long

        '        With SprdMain
        '            counter = mSearchStartRow

        '            For I = counter To .MaxCols
        '                ret = SprdMain.SearchCol(I, 0, -1, mSearchItem, 2)      '' SearchFlagsPartialMatch)
        '                If ret <> -1 Then
        '                    SprdMain.ShowCell(I, ret, 0)       'PositionUpperLeft)

        '                    SprdMain.Row = ret
        '                    SprdMain.Row2 = ret
        '                    SprdMain.Col = I
        '                    SprdMain.Col2 = I ''SprdMain.ActiveCol
        '                    SprdMain.BlockMode = True
        '                    SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
        '                    SprdMain.BlockMode = False



        '                    mSearchStartRow = I + 1
        '                    GoTo NextRec
        '                End If

        '            Next
        '            mSearchStartRow = 1
        'NextRec:
        '        End With



        With SprdMain
            counter = mSearchStartRow
            For I = counter To .MaxRows
                .Row = I

                .Col = ColItemCode
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, I, ColItemCode)

                    SprdMain.Row = I
                    SprdMain.Row2 = I
                    SprdMain.Col = ColItemCode
                    SprdMain.Col2 = ColItemCode ''SprdMain.ActiveCol
                    SprdMain.BlockMode = True
                    SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                    SprdMain.BlockMode = False

                    mSearchStartRow = I + 1
                    GoTo NextRec
                End If

                .Col = ColItemName
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, I, ColItemCode)

                    SprdMain.Row = I
                    SprdMain.Row2 = I
                    SprdMain.Col = ColItemName
                    SprdMain.Col2 = ColItemName ''SprdMain.ActiveCol
                    SprdMain.BlockMode = True
                    SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                    SprdMain.BlockMode = False

                    mSearchStartRow = I + 1
                    GoTo NextRec
                End If

                .Col = ColPartNo
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, I, ColItemCode)

                    SprdMain.Row = I
                    SprdMain.Row2 = I
                    SprdMain.Col = ColPartNo
                    SprdMain.Col2 = ColPartNo ''SprdMain.ActiveCol
                    SprdMain.BlockMode = True
                    SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                    SprdMain.BlockMode = False

                    mSearchStartRow = I + 1
                    GoTo NextRec
                End If

            Next
            mSearchStartRow = 1
NextRec:
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            'MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh
            FormatSprdView()
            UltraGrid1.Refresh()
            UltraGrid1.Focus()
            UltraGrid1.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            UltraGrid1.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsSOMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmSalesOrderGST_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Sales Order -" & IIf(lblType.Text = "O", "Open", "Closed") & " (Customer P.O. Registration)"

        SqlStr = "Select * From DSP_SALEORDER_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSOMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From DSP_SALEORDER_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSODetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)

        cboStatus.Items.Clear()
        cboStatus.Items.Add("Open")
        cboStatus.Items.Add("Closed")
        '    CboStatus.Enabled = IIf(PubSuperUser = "S" OR PubSuperUser = "A", True, False)

        cboPOType.Items.Clear()
        cboPOType.Items.Add("Regular")
        cboPOType.Items.Add("SPD")
        cboPOType.Items.Add("Export")

        cboOrderType.Items.Clear()
        cboOrderType.Items.Add("Open Order")
        cboOrderType.Items.Add("Closed Order")

        cboOrderType.Enabled = False

        SetTextLengths()
        Clear1()
        If lblAddItem.Text = "N" Then
            If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim SqlStr As String
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)
        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf & " A.AUTO_KEY_SO AS SO_NO, A.SO_DATE AS SO_DATE, " & vbCrLf _
            & " A.CUST_PO_NO AS PO_NO, A.CUST_PO_DATE AS PO_DATE, " & vbCrLf _
            & " A.AMEND_NO, A.AMEND_DATE,  " & vbCrLf & " A.AMEND_WEF_FROM AS WEF,A.SUPP_CUST_CODE, B.SUPP_CUST_NAME AS NAME, " & vbCrLf & " A.TYPE_OF_SALE, A.TRANSPORTER_DTL, A.MODE_OF_DELV, " & vbCrLf & " DECODE(A.SO_STATUS,'O','Open','Closed') AS STATUS, " & vbCrLf & " A.REMARKS " & vbCrLf & " FROM DSP_SALEORDER_HDR A, FIN_SUPP_CUST_MST B " & vbCrLf & " WHERE A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE " & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y'" ''& vbCrLf |            & " AND SUBSTR(AUTO_KEY_SO,LENGTH(AUTO_KEY_SO)-5,4)=" & RsCompany.fields("FYEAR").value & ""

        SqlStr = SqlStr & vbCrLf & " AND ORDER_TYPE='" & Trim(lblType.Text) & "'"

        SqlStr = SqlStr & " ORDER BY SUBSTR(AUTO_KEY_SO,LENGTH(AUTO_KEY_SO)-5,4), A.AUTO_KEY_SO, A.AMEND_NO"

        'MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))

        ClearGroupFromUltraGrid(UltraGrid1)
        ClearFilterFromUltraGrid(UltraGrid1)

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        Me.UltraGrid1.DataSource = ds
        Me.UltraGrid1.DataMember = ""

        CreateGridHeader("S")


        oledbAdapter.Dispose()
        oledbCnn.Close()

        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "SO No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "SO Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Customer PO No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Customer PO Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "Amend No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "Amend Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Amend WEF From"
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "Customer Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "Customer Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Header.Caption = "Type of Sale"
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Header.Caption = "Transporter Details"
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Header.Caption = "Mode of Delivery"

            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Header.Caption = "SO Status"
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
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Width = 200

            'UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.MFGQty).MaskInput = "99999"

            Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            Me.UltraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

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


    Private Sub frmSalesOrderGST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        cboInvType.Items.Clear()
        cboInvType.Items.Add("Goods")
        cboInvType.Items.Add("Services")
        cboInvType.SelectedIndex = -1

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

        mAccountCode = CStr(-1)
        lblMkey.Text = ""
        txtSONo.Text = ""
        txtSODate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtAddress.Text = ""
        txtVendorCode.Text = ""
        chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkApproved.Enabled = False

        chkDI.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtCustomerName.Text = ""
        txtShipCustomer.Text = ""
        txtCode.Text = ""
        txtCode.Enabled = True
        txtCustomerName.Enabled = True
        SprdMain.Enabled = True

        txtPONo.Text = ""
        txtPODate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtEPCGNo.Text = ""
        txtEPCGDate.Text = ""
        txtCustAmendNo.Text = ""
        txtAmendNo.Text = ""
        txtAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtWEF.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        cboStatus.SelectedIndex = 0
        cboPOType.SelectedIndex = 0
        cboOrderType.SelectedIndex = IIf(lblType.Text = "O", 0, 1)
        txtRemarks.Text = ""
        txtShipTo.Text = ""
        txtBillTo.Text = ""
        txtRoadPermit.Text = ""
        txtSaleType.Text = ""
        txtLCClaim.Text = ""
        txtDespMode.Text = ""
        txtFreight.Text = ""
        txtOctroi.Text = ""
        txtCommission.Text = ""
        txtInspection.Text = ""
        txtDestination.Text = ""
        txtTransporter.Text = ""
        txtDescDetail.Text = ""
        txtInsurance.Text = ""
        txtPayment.Text = ""
        txtBalPayment.Text = ""
        TabMain.SelectedIndex = 0

        txtServProvided.Text = ""
        cboInvType.Enabled = True
        cboInvType.SelectedIndex = 0

        txtAmendNo.Enabled = False
        txtAmendDate.Enabled = False
        cmdAmend.Enabled = True
        cmdsearch.Enabled = True
        chkShipTo.CheckState = CheckState.Checked
        chkShipTo.Enabled = True
        cmdsearchShipCust.Enabled = False
        txtShipCustomer.Enabled = False
        cmdShipToSearch.Enabled = False
        txtShipTo.Enabled = False

        cmdAmendExcel.Enabled = False
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        cboStatus.Enabled = False '' IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
        cboOrderType.Enabled = False '' True

        MainClass.ButtonStatus(Me, XRIGHT, RsSOMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        With SprdMain
            .set_RowHeight(-1, ConRowHeight * 1.5)
            .Row = Arow

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSODetail.Fields("ITEM_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("Item_Short_Desc", "INV_ITEM_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 24)

            .Col = ColItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsSODetail.Fields("UOM_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 4)

            .Col = ColItemSNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSODetail.Fields("ITEM_SNO").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 10)

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSODetail.Fields("PART_NO").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 10)

            .Col = ColHSNCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsSODetail.Fields("HSN_CODE").DefinedSize '' MainClass.SetMaxLength("HSN_CODE", "GEN_HSN_MST", PubDBCn)
            '        .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '        .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 8)

            .Col = ColAddItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSODetail.Fields("ADD_ITEM_DESCRIPTION").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 10)
            .ColHidden = False


            .Col = ColCustStoreLoc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSODetail.Fields("CUST_STORE_LOC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 10)
            .ColHidden = False

            .Col = ColItemRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("ITEM_PRICE").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 7)

            .Col = ColPreviousItemRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("ITEM_PRICE").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 7)

            .Col = ColMRP
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("ITEM_PRICE").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 7)
            .ColHidden = True

            .Col = ColMTRCOST
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("ITEM_PRICE").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)
            .ColHidden = True

            .Col = ColMSPCost
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("MSP_COST").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)
            .ColHidden = True

            .Col = ColMSPCostAdd
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("MSP_COST_ADD").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)
            .ColHidden = True

            .Col = ColFreightCost
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("FREIGHT_COST").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)

            .Col = ColPO_WEF
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY

            .set_ColWidth(.Col, 8)

            .Col = ColValidQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("VALID_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)

            .Col = ColValidDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY

            .set_ColWidth(.Col, 8)
            .ColHidden = False

            .Col = ColProcessCost
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("PROCESS_COST").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)
            .ColHidden = True

            .Col = ColCGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("CGST_PER").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)

            .Col = ColSGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("SGST_PER").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)

            .Col = ColIGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("IGST_PER").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)

            .Col = ColAccountName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("NAME", "FIN_INVTYPE_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 24)

            .Col = ColSOStatus
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSODetail.Fields("SO_ITEM_STATUS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 5)
            .ColHidden = True

            .Col = colRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSODetail.Fields("REMARKS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 10)

            .ColsFrozen = ColItemName

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemUOM)

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPartNo, ColPartNo)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPreviousItemRate, ColPreviousItemRate)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCGSTPer, ColIGSTPer)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColSOStatus, ColSOStatus)

            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub FormatSprdView()

        'With SprdView
        '    .Row = -1
        '    .set_RowHeight(0, 300)
        '    .set_ColWidth(0, 500)
        '    .set_ColWidth(1, 1000)
        '    .set_ColWidth(2, 1000)
        '    .set_ColWidth(3, 1000)
        '    .set_ColWidth(4, 1000)
        '    .set_ColWidth(5, 1000)
        '    .set_ColWidth(6, 1000)
        '    .set_ColWidth(7, 1000)
        '    .set_ColWidth(8, 4500)
        '    .set_ColWidth(9, 2000)
        '    .set_ColWidth(10, 2000)
        '    .set_ColWidth(11, 1200)
        '    MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
        '    MainClass.SetSpreadColor(SprdView, -1)
        '    .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        '    MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        'End With
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1

        txtSONo.MaxLength = RsSOMain.Fields("AUTO_KEY_SO").Precision
        txtSODate.MaxLength = 10
        txtCustomerName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtShipCustomer.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtCode.MaxLength = RsSOMain.Fields("SUPP_CUST_CODE").DefinedSize

        txtPONo.MaxLength = RsSOMain.Fields("CUST_PO_NO").DefinedSize
        txtPODate.MaxLength = 10

        txtEPCGNo.MaxLength = RsSOMain.Fields("EPCG_NO").DefinedSize
        txtEPCGDate.MaxLength = 10

        txtCustAmendNo.MaxLength = RsSOMain.Fields("CUST_AMEND_NO").DefinedSize
        txtAmendNo.MaxLength = RsSOMain.Fields("AMEND_NO").DefinedSize
        txtAmendDate.MaxLength = 10
        txtWEF.MaxLength = 10
        txtRemarks.MaxLength = RsSOMain.Fields("REMARKS").DefinedSize

        txtBillTo.MaxLength = RsSOMain.Fields("BILL_TO_LOC_ID").DefinedSize
        txtShipTo.MaxLength = RsSOMain.Fields("SHIP_TO_LOC_ID").DefinedSize

        txtRoadPermit.MaxLength = RsSOMain.Fields("ROAD_PERMIT").DefinedSize
        txtSaleType.MaxLength = RsSOMain.Fields("TYPE_OF_SALE").DefinedSize
        txtLCClaim.MaxLength = RsSOMain.Fields("LC_CLAIMS").DefinedSize
        txtDespMode.MaxLength = RsSOMain.Fields("MODE_OF_DELV").DefinedSize
        txtFreight.MaxLength = RsSOMain.Fields("FREIGHT_CHARGES").DefinedSize
        txtOctroi.MaxLength = RsSOMain.Fields("OCTROI_DTL").DefinedSize
        txtCommission.MaxLength = RsSOMain.Fields("COMM_DTLS").DefinedSize
        txtInspection.MaxLength = RsSOMain.Fields("INSPECTION_DTL").DefinedSize
        txtDestination.MaxLength = RsSOMain.Fields("DESTINATION_DTL").DefinedSize
        txtTransporter.MaxLength = RsSOMain.Fields("TRANSPORTER_DTL").DefinedSize
        txtDescDetail.MaxLength = RsSOMain.Fields("DESPATCH_DTL").DefinedSize
        txtInsurance.MaxLength = RsSOMain.Fields("INSURANCE_DTL").DefinedSize
        txtPayment.MaxLength = RsSOMain.Fields("PAYMENT_DTL").DefinedSize
        txtBalPayment.MaxLength = RsSOMain.Fields("BALANCE_PAY_DTL").DefinedSize
        txtVendorCode.MaxLength = RsSOMain.Fields("VENDOR_CODE").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim cntRow As Integer
        Dim mAcctPostName As String
        Dim mFirstAcctPostName As String
        Dim I As Integer
        Dim mItemCode As String
        Dim pTRNType As String
        Dim mIsStockTransfer As String
        Dim mProdType As String
        Dim mHSNCode As String = ""
        Dim mHSNMstCode As String
        Dim mWEF As String
        Dim mStockType As String
        Dim mLocal As String
        Dim mGSTAmount As Double

        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim mIsExempted As Boolean



        FieldsVarification = True
        '    If ValidateBranchLocking(txtAmendDate.Text) = True Then
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        'If CDate(txtWEF.Text) < CDate(PubGSTApplicableDate) Then
        '    MsgInformation("WEF Date should be Greater than GST Applicable date.")
        '    FieldsVarification = False
        '    Exit Function
        'End If

        If ValidateBookLocking(PubDBCn, CInt(ConLockSO), txtAmendDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateAccountLocking(PubDBCn, (txtAmendDate.Text), (txtCustomerName.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If


        If MODIFYMode = True Then
            If RsSOMain.Fields("SO_STATUS").Value = "C" Then
                MsgInformation("Closed Sale Order Cann't be Modified")
                FieldsVarification = False
                Exit Function
            End If
        End If


        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsSOMain.EOF = True Then Exit Function

        If MODIFYMode = True And Trim(txtSONo.Text) = "" Then
            MsgInformation("SO No. is Blank")
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtSODate.Text) = "" Then
            MsgInformation(" SO Date is empty. Cannot Save")
            txtSODate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtSODate.Text) <> "" Then
            If IsDate(txtSODate.Text) = False Then
                MsgInformation(" Invalid SO Date. Cannot Save")
                txtSODate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtWEF.Text) = "" Then
            MsgInformation(" SO WEF Date is empty. Cannot Save")
            txtWEF.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtWEF.Text) <> "" Then
            If IsDate(txtWEF.Text) = False Then
                MsgInformation(" Invalid SO WEF Date. Cannot Save")
                txtWEF.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MODIFYMode = True And chkApproved.CheckState = System.Windows.Forms.CheckState.Checked Then
            If PubUserID <> "G0416" Then
                If lblAddItem.Text = "N" Then
                    MsgInformation("Approved SO Cann't be Modified")
                    FieldsVarification = False
                    Exit Function
                Else
                    If VB.Left(cboStatus.Text, 1) = "C" Then
                        MsgInformation("Status is Closed, so that cann't be change Order.")
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If
        End If

        If Trim(txtCustomerName.Text) = "" Then
            MsgInformation("Customer Name is Blank. Cannot Save")
            If txtCustomerName.Enabled = True Then txtCustomerName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtCustomerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            txtCode.Text = MasterNo
        Else
            MsgInformation("Invalid Customer Name. Cannot Save")
            If txtCustomerName.Enabled = True Then txtCustomerName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtBillTo.Text) = "" Then
            MsgInformation("Bill To is blank. Cannot Save")
            txtBillTo.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'") = False Then
                MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
                txtBillTo.Focus()
                FieldsVarification = False
            End If
        End If


        If chkShipTo.CheckState = CheckState.Unchecked Then
            If Trim(txtShipCustomer.Text) = "" Then
                MsgInformation("Please Enter the Ship Customer. Cannot Save")
                If txtShipCustomer.Enabled = True Then txtShipCustomer.Focus()
                FieldsVarification = False
                Exit Function
            End If

            Dim mShipCustomerCode As String = ""
            If MainClass.ValidateWithMasterTable((txtShipCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
                MsgInformation("Invalid Ship Customer Name. Cannot Save")
                If txtShipCustomer.Enabled = True Then txtShipCustomer.Focus()
                FieldsVarification = False
                Exit Function
            Else
                mShipCustomerCode = MasterNo
            End If

            If Trim(txtShipTo.Text) = "" Then
                MsgInformation("Ship To is blank. Cannot Save")
                txtShipTo.Focus()
                FieldsVarification = False
                Exit Function
            Else
                If MainClass.ValidateWithMasterTable(txtShipTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShipCustomerCode) & "'") = False Then
                    MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
                    txtShipTo.Focus()
                    FieldsVarification = False
                End If
            End If
        End If
        If MainClass.ValidateWithMasterTable((txtCustomerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INTER_UNIT='Y'") = True Then
            mIsStockTransfer = "Y"
        Else
            mIsStockTransfer = "N"
        End If

        If Trim(txtPONo.Text) = "" Then
            MsgInformation("Customer PO No. is Blank")
            txtPONo.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtPODate.Text) = "" Then
            MsgInformation(" PO Date is empty. Cannot Save")
            txtPODate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtPODate.Text) <> "" Then
            If IsDate(txtPODate.Text) = False Then
                MsgInformation(" Invalid PO Date. Cannot Save")
                txtPODate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtCustAmendNo.Text) = "" Then
            MsgInformation("Customer Amend No. is Blank")
            txtCustAmendNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtWEF.Text) = "" Then
            MsgInformation("WEF Date is empty. Cannot Save")
            txtWEF.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtWEF.Text) <> "" Then
            If IsDate(txtWEF.Text) = False Then
                MsgInformation(" Invalid WEF Date. Cannot Save")
                txtWEF.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If CheckCustAmendNo() = False Then
            txtCustAmendNo.Focus()
            FieldsVarification = False
        End If

        If VB.Left(cboOrderType.Text, 1) = "O" And VB.Left(cboStatus.Text, 1) = "O" Then
            If CheckPreviousPOExists((txtCode.Text), Trim(txtSONo.Text)) = True Then
                FieldsVarification = False
                Exit Function
            End If
        End If



        mLocal = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")

        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)

        SprdMain.Row = 1
        SprdMain.Col = ColAccountName
        mFirstAcctPostName = Trim(UCase(SprdMain.Text))

        For I = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = I
            SprdMain.Col = ColItemCode
            mItemCode = Trim(UCase(SprdMain.Text))
            If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='I'") = True Then
                MsgInformation("Item Status is Closed, So cann't be Saved. [" & mItemCode & "]")
                MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                FieldsVarification = False
                Exit Function
            End If

            If CheckDuplicateItem(I) = True Then
                'MainClass.SetFocusToCell(SprdMain, I, ColCustStoreLoc)
                FieldsVarification = False
                Exit Function
            End If

            SprdMain.Row = I
            SprdMain.Col = ColAccountName
            If Trim(UCase(SprdMain.Text)) = "" Then
                SprdMain.Text = mFirstAcctPostName
            End If
            mAcctPostName = Trim(UCase(SprdMain.Text))

            If mAcctPostName = "" Then
                MsgInformation("Account Post Name Cann't be Blank.")
                MainClass.SetFocusToCell(SprdMain, I, ColAccountName)
                FieldsVarification = False
                Exit Function
            Else
                If Trim(mAcctPostName) <> "" Then
                    If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = False Then
                        MsgInformation("Invaild Account Post Name.")
                        MainClass.SetFocusToCell(SprdMain, I, ColAccountName)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If
        Next

        If cboInvType.Text = "" Then
            MsgInformation("Please Select Goods or Service.")
            FieldsVarification = False
            Exit Function
        End If

        If VB.Left(cboInvType.Text, 1) = "S" Then
            mHSNCode = ""
            If MainClass.ValidateWithMasterTable(txtServProvided.Text, "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                mHSNCode = Trim(IIf(IsDBNull(MasterNo), "", MasterNo))
            Else
                MsgBox("Invalid SAC Code.", MsgBoxStyle.Information)
                cboInvType.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If mHSNCode = "" Then
                MsgBox("SAC Code is Blank. Please check Service.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If


        For I = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = I
            SprdMain.Col = ColItemCode
            mItemCode = Trim(UCase(SprdMain.Text))

            If VB.Left(cboInvType.Text, 1) = "G" Then
                SprdMain.Col = ColHSNCode
                mHSNCode = Trim(UCase(SprdMain.Text))
                If mHSNCode = "" Then
                    MsgInformation("HSN Cann't be Blank.")
                    FieldsVarification = False
                    Exit Function
                End If
                If MainClass.ValidateWithMasterTable(Trim(mHSNCode), "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='" & VB.Left(cboInvType.Text, 1) & "'") = False Then
                    'mHSNMstCode = Trim(IIf(IsDBNull(MasterNo), "", MasterNo))
                    'If mHSNMstCode <> Trim(mHSNCode) Then
                    MsgBox("Please Check HSN Code for Item Code : " & mItemCode & "")
                    MainClass.SetFocusToCell(SprdMain, I, ColHSNCode)
                    FieldsVarification = False
                    Exit Function
                    'End If
                End If
            Else
                SprdMain.Col = ColHSNCode
                SprdMain.Text = mHSNCode
            End If

            mIsExempted = CheckHSNExempted(mHSNCode)
            mProdType = GetProductionType(mItemCode)

            SprdMain.Col = ColPO_WEF
            If IsDate(SprdMain.Text) Then
                mWEF = VB6.Format(SprdMain.Text, "DD/MM/YYYY")
            Else
                mWEF = VB6.Format(txtWEF.Text, "DD/MM/YYYY")
            End If

            If CDate(mWEF) < CDate(PubGSTApplicableDate) Then
                MsgBox("WEF Should be Greater Than GST Applicable Date. Please Check WEF Date for Item Code :  " & Trim(SprdMain.Text))
                FieldsVarification = False
                Exit Function
            End If

            If Trim(mCompanyGSTNo) = Trim(mPartyGSTNo) Or mIsExempted = True Then

            Else
                If mLocal = "Y" Then
                    SprdMain.Col = ColCGSTPer
                    If Val(SprdMain.Text) = 0 Then
                        MsgBox("CGST % not Define for Item Code : " & Trim(mItemCode))
                        FieldsVarification = False
                        Exit Function
                    End If

                    SprdMain.Col = ColSGSTPer
                    If Val(SprdMain.Text) = 0 Then
                        MsgBox("SGST % not Define for Item Code : " & Trim(mItemCode))
                        FieldsVarification = False
                        Exit Function
                    End If
                Else
                    SprdMain.Col = ColIGSTPer
                    If Val(SprdMain.Text) = 0 Then
                        MsgBox("IGST % not Define for Item Code : " & Trim(mItemCode))
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If

            SprdMain.Col = ColAccountName
            mAcctPostName = Trim(UCase(SprdMain.Text))
            If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISSTOCKTRF='" & mIsStockTransfer & "'") = False Then
                MsgInformation("Invoice Type Not a Stock Transfer, Please select Stock Transfer Invoice Type for Item Code " & mItemCode)
                MainClass.SetFocusToCell(SprdMain, I, ColAccountName)
                FieldsVarification = False
                Exit Function
                '                pTRNType = MasterNo
            End If


            If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISSUPPBILL='Y'") = True Then
                MsgInformation("You Cann't be Select Supplimentary Invoice Type for Item Code " & mItemCode)
                MainClass.SetFocusToCell(SprdMain, I, ColAccountName)
                FieldsVarification = False
                Exit Function
                '                pTRNType = MasterNo
            End If



            If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISSALERETURN='Y'") = True Then
                MsgInformation("Cann't be Select Return Invoice Type for Item Code " & mItemCode)
                MainClass.SetFocusToCell(SprdMain, I, ColAccountName)
                FieldsVarification = False
                Exit Function
                '                pTRNType = MasterNo
            End If
            If VB.Left(cboInvType.Text, 1) = "G" Then
                If mProdType = "P" Or mProdType = "I" Then
                    If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND (ISSALECOMP='Y' OR ISSPD='Y')") = False Then
                        MsgInformation("Please Select Component Sale Invoice Type for Item Code " & mItemCode)
                        MainClass.SetFocusToCell(SprdMain, I, ColAccountName)
                        FieldsVarification = False
                        Exit Function
                        '                pTRNType = MasterNo
                    End If
                ElseIf mProdType = "J" Or mProdType = "2" Then
                    If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISSALEJW='Y'") = False Then
                        MsgInformation("Please Select Job Work Invoice Type for Item Code " & mItemCode)
                        MainClass.SetFocusToCell(SprdMain, I, ColAccountName)
                        FieldsVarification = False
                        Exit Function
                        '                pTRNType = MasterNo
                    End If
                ElseIf mProdType = "A" Or mProdType = "T" Or mProdType = "1" Then
                    If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISFIXASSETS='Y'") = False Then
                        MsgInformation("Please Select Assets/Capital Invoice Type for Item Code " & mItemCode)
                        MainClass.SetFocusToCell(SprdMain, I, ColAccountName)
                        FieldsVarification = False
                        Exit Function
                        '                pTRNType = MasterNo
                    End If
                ElseIf mProdType = "R" Or mProdType = "B" Or mProdType = "D" Or mProdType = "3" Then
                    If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND (ISSALE57='Y' OR ISSPD='Y')") = False Then
                        MsgInformation("Please Select Raw Material Invoice Type for Item Code " & mItemCode)
                        MainClass.SetFocusToCell(SprdMain, I, ColAccountName)
                        FieldsVarification = False
                        Exit Function
                        '                pTRNType = MasterNo
                    End If
                Else
                    mStockType = GetStockType(PubDBCn, mItemCode, 1)
                    If mStockType = "SC" Then
                        If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND ISSCRAPSALE='N'") = True Then
                            MsgInformation("Cann't be select Scarp Invoice Type for Item Code " & mItemCode)
                            MainClass.SetFocusToCell(SprdMain, I, ColAccountName)
                            FieldsVarification = False
                            Exit Function
                            '                pTRNType = MasterNo
                        End If
                    Else
                        If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S' AND (ISSALECOMP='Y' or ISSALEJW='Y' OR ISFIXASSETS='Y' OR ISSALE57='Y')") = True Then
                            MsgInformation("Cann't be select Component / Jobwork / Assets / Capital / Raw Material Invoice Type for Item Code " & mItemCode)
                            MainClass.SetFocusToCell(SprdMain, I, ColAccountName)
                            FieldsVarification = False
                            Exit Function
                            '                pTRNType = MasterNo
                        End If
                    End If
                End If
            End If
        Next

        '    CalcTots

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Please Check Item Code.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColItemName, "S", "Please Check Item Description.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColHSNCode, "S", "Please Check Item HSN Code.") = False Then FieldsVarification = False

        If MainClass.ValidDataInGrid(SprdMain, ColItemUOM, "S", "Please Check Unit.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColItemRate, "N", "Please Check Item Price") = False Then FieldsVarification = False

        '    If Left(cboOrderType.Text, 1) = "C" Then
        '        If MainClass.ValidDataInGrid(SprdMain, ColValidQty, "N", "Please Check Valid Qty.") = False Then FieldsVarification = False
        '    End If
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume
    End Function

    Private Function CheckPreviousPOExists(ByRef pSupplierCode As String, ByRef pPONO As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xPoNo As Double
        Dim cntRow As Integer
        Dim pItemCode As String
        Dim mPOType As String = "R"
        '    If PubUserEMPCode = "000416" Then
        '        CheckPreviousPOExists = False
        '        Exit Function
        '    End If

        CheckPreviousPOExists = False

        If Trim(pPONO) = "" Then
            xPoNo = -1
        Else
            xPoNo = Val(pPONO)
        End If
        mPOType = VB.Left(cboInvType.Text, 1)

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColItemCode
                pItemCode = Trim(.Text)

                SqlStr = "SELECT DISTINCT AUTO_KEY_SO " & vbCrLf _
                    & " FROM DSP_SALEORDER_HDR IH,DSP_SALEORDER_DET ID " & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
                    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

                SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf _
                    & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                    & " AND IH.ORDER_TYPE='O' AND SO_STATUS='O' AND ISGSTENABLE_PO='Y'"

                SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_SO <> " & Val(CStr(xPoNo)) & " AND PO_TYPE ='" & mPOType & "' AND BILL_TO_LOC_ID='" & txtBillTo.Text & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    MsgInformation("Item Code : " & pItemCode & " Already made. Against Open PO No. : " & RsTemp.Fields("AUTO_KEY_SO").Value)
                    CheckPreviousPOExists = True
                    Exit Function
                End If
            Next
        End With
        Exit Function
ErrPart:
        CheckPreviousPOExists = True
    End Function
    Private Function CheckCustAmendNo() As Boolean

        On Error GoTo ErrPart
        Dim mCustAmendNo As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckCustAmendNo = True

        If Val(txtAmendNo.Text) = 0 Then Exit Function

        SqlStr = " SELECT MAX(CUST_AMEND_NO) AS CUST_AMEND_NO" & vbCrLf _
            & " FROM DSP_SALEORDER_HDR" & vbCrLf & " WHERE AUTO_KEY_SO=" & Val(txtSONo.Text) & "" & vbCrLf _
            & " AND AMEND_NO<" & Val(txtAmendNo.Text) & " " & vbCrLf & " AND CUST_PO_NO='" & MainClass.AllowSingleQuote((txtPONo.Text)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDBNull(RsTemp.Fields("CUST_AMEND_NO").Value) Then
                CheckCustAmendNo = True
            Else
                mCustAmendNo = Val(RsTemp.Fields("CUST_AMEND_NO").Value)
                If Val(txtCustAmendNo.Text) <> mCustAmendNo + 1 Then
                    MsgInformation("Last Amend No for PO : " & txtPONo.Text & " is : " & mCustAmendNo & ". Please Check.")
                    CheckCustAmendNo = False
                End If
            End If
        End If

        Exit Function
ErrPart:
        CheckCustAmendNo = False
    End Function

    Private Sub frmSalesOrderGST_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        RsSOMain.Close()
        Me.Dispose()
        'RsOpOuts.Close
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function CheckDuplicateItem(ByVal pRow As Integer) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer
        Dim mItemCode As String
        Dim mCheckItemCode As String

        If pRow < 1 Then CheckDuplicateItem = True : Exit Function

        With SprdMain
            .Row = pRow
            .Col = ColItemCode
            mItemCode = UCase(.Text)

            .Col = ColCustStoreLoc
            mItemCode = mItemCode & "-" & UCase(.Text)

            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                mCheckItemCode = UCase(.Text)

                .Col = ColCustStoreLoc
                mCheckItemCode = mCheckItemCode & "-" & UCase(.Text)

                If UCase(mCheckItemCode) = UCase(mItemCode) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Item Code")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
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
            '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False
        End With
    End Sub


    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim xAccountName As String
        Dim xCustStoreLoc As String
        Dim xHSNCode As String
        Dim RsTemp As ADODB.Recordset
        Dim mHSNDesc As String
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double

        If eventArgs.row = 0 And eventArgs.col = ColItemCode And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                SqlStr = GetSearchItem("Y")
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName)
                    .Col = ColItemName
                    .Text = Trim(AcName1)
                    .Col = ColItemUOM
                    .Text = Trim(AcName2)
                    .Col = ColHSNCode
                    .Text = Trim(AcName3)
                    .Col = ColPartNo
                    .Text = Trim(AcName4)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemName And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemName
                SqlStr = GetSearchItem("N")
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemName
                    .Text = Trim(AcName)
                    .Col = ColItemCode
                    .Text = Trim(AcName1)
                    .Col = ColItemUOM
                    .Text = Trim(AcName2)
                    .Col = ColHSNCode
                    .Text = Trim(AcName3)
                    .Col = ColPartNo
                    .Text = Trim(AcName4)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColHSNCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColHSNCode
                If MainClass.SearchGridMaster(.Text, "GEN_HSN_MST", "HSN_CODE", "HSN_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='" & VB.Left(cboInvType.Text, 1) & "'") = True Then
                    .Row = .ActiveRow
                    .Col = ColHSNCode
                    .Text = AcName
                    xHSNCode = Trim(.Text)

                    'SqlStr = "SELECT * FROM GEN_HSN_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HSN_CODE='" & xHSNCode & "'"
                    'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    'If RsTemp.EOF = False Then
                    '    mCGSTPer = 0
                    '    mSGSTPer = 0
                    '    mIGSTPer = 0
                    'Else
                    '    mHSNDesc = ""
                    '    mCGSTPer = 0
                    '    mSGSTPer = 0
                    '    mIGSTPer = 0
                    'End If


                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColHSNCode)
                End If

            End With
        End If




        If eventArgs.row = 0 And eventArgs.col = ColCustStoreLoc Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColCustStoreLoc
                xCustStoreLoc = Trim(.Text)
                If mItemCode <> "" Then
                    SqlStr = " SELECT DISTINCT C.LOC_CODE, C.LOC_DESCRIPTION FROM INV_MODELWISE_PROD_DET A, GEN_MODEL_MST B, DSP_CUST_STORE_LOC_MST C" & vbCrLf _
                            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _
                            & " AND A.MODEL_CODE = B.MODEL_CODE  " & vbCrLf _
                            & " AND B.COMPANY_CODE = C.COMPANY_CODE " & vbCrLf _
                            & " AND C.LOC_CODE = B.LOC_CODE  " & vbCrLf _
                            & " AND A.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

                    If MainClass.SearchGridMasterBySQL2(xCustStoreLoc, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColCustStoreLoc
                        .Text = Trim(AcName)
                    End If
                End If

            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColAccountName Then
            With SprdMain
                .Row = .ActiveRow
                SprdMain.Col = ColItemCode
                mItemCode = Trim(UCase(SprdMain.Text))

                If Trim(mItemCode) = "" Then Exit Sub

                'Dim mProdType As String
                'mProdType = GetProductionType(mItemCode)

                .Col = ColAccountName
                xAccountName = Trim(.Text)

                If MainClass.SearchGridMaster(xAccountName, "FIN_INVTYPE_MST", "NAME", , , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
                    .Row = .ActiveRow
                    .Col = ColAccountName
                    .Text = AcName

                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColAccountName)
                End If
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemName)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))

        If eventArgs.keyCode = System.Windows.Forms.Keys.F3 And mSearchKey <> "" Then
            If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColItemRate)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If

            SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xICode As String
        Dim xAcctPostName As String
        If eventArgs.newRow = -1 Then Exit Sub
        Dim mPreviousItemRate As Double
        Dim mItemRate As Double
        Dim xCustStoreLoc As String
        Dim mHSNCode As String

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                If GetValidItem(xICode) = True Then
                    If CheckDuplicateItem(SprdMain.Row) = False Then
                        If FillGridRow(xICode, ColItemCode) = False Then Exit Sub
                        '                    FormatSprdMain Row
                        '                MainClass.SetFocusToCell SprdMain, Row, ColItemRate
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                End If
            Case ColHSNCode
                SprdMain.Row = SprdMain.ActiveRow


                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                SprdMain.Col = ColHSNCode
                If SprdMain.Text = "" Then Exit Sub

                If SprdMain.Text <> "" Then
                    If MainClass.ValidateWithMasterTable(SprdMain.Text, "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='" & VB.Left(cboInvType.Text, 1) & "'") = False Then
                        MsgInformation("Invaild HSN CODE.")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColHSNCode)
                        Exit Sub
                    End If
                End If

                If FillGridRow(xICode, ColHSNCode) = False Then Exit Sub


            Case ColItemRate
                If CheckItemRate() = True Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColPreviousItemRate
                    mPreviousItemRate = Val(SprdMain.Text)

                    SprdMain.Col = ColItemRate
                    mItemRate = Val(SprdMain.Text)


                    If mPreviousItemRate < mItemRate And mPreviousItemRate > 0 Then ''Increase
                        SprdMain.Row = SprdMain.Row
                        SprdMain.Row2 = SprdMain.Row
                        SprdMain.Col = 1
                        SprdMain.Col2 = colRemarks
                        SprdMain.BlockMode = True
                        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFC0)
                        SprdMain.BlockMode = False
                    ElseIf mPreviousItemRate > mItemRate And mPreviousItemRate > 0 Then  ''Decrease
                        SprdMain.Row = SprdMain.Row
                        SprdMain.Row2 = SprdMain.Row
                        SprdMain.Col = 1
                        SprdMain.Col2 = colRemarks
                        SprdMain.BlockMode = True
                        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0C0FF)
                        SprdMain.BlockMode = False
                    Else ''Not Change
                        SprdMain.Row = SprdMain.Row
                        SprdMain.Row2 = SprdMain.Row
                        SprdMain.Col = 1
                        SprdMain.Col2 = colRemarks
                        SprdMain.BlockMode = True
                        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        SprdMain.BlockMode = False
                    End If
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain(-1)
                End If
            Case ColAccountName
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then GoTo CalcPart
                SprdMain.Col = ColAccountName
                xAcctPostName = SprdMain.Text
                If xAcctPostName <> "" Then
                    If MainClass.ValidateWithMasterTable(xAcctPostName, "NAME", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = False Then
                        MsgInformation("Invaild Account Post Name.")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColAccountName)
                        Exit Sub
                    End If
                End If
            Case ColCustStoreLoc
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then GoTo CalcPart
                SprdMain.Col = ColCustStoreLoc
                xCustStoreLoc = SprdMain.Text
                If xCustStoreLoc <> "" Then
                    If GetValidCustomerStoreLoc(xICode, xCustStoreLoc) = False Then
                        'MsgInformation(xCustStoreLoc & " is a Invaild Store Loc for Item Code : " & xICode)
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCustStoreLoc)
                        Exit Sub
                    End If
                End If

                If CheckDuplicateItem(SprdMain.Row) = True Then
                    'MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCustStoreLoc)
                End If
        End Select
CalcPart:

        Call CalcTots()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function CheckItemRate() As Boolean

        On Error GoTo ERR1
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColItemRate
            If Val(.Text) > 0 Then
                CheckItemRate = True
            Else
                MsgInformation("Please Check the Item Price.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemRate)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FillGridRow(ByRef mItemCode As String, pCol As Long) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mHSNCode As String
        Dim mSaleInvTypeCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mInvTypeDesc As String
        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mPartNo As String

        If mItemCode = "" Then Exit Function

        mLocal = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")

        SqlStr = ""
        SqlStr = " Select INVMST.ITEM_CODE,INVMST.ITEM_SHORT_DESC,INVMST.ISSUE_UOM, " & vbCrLf _
            & " ID.ITEM_RATE,  ID.DISC_PER,ID.CUSTOMER_ITEM_NO , CUSTOMER_PART_NO,ITEM_COLOR," & vbCrLf _
            & " INVMST.CATEGORY_CODE, CMST.SALEINVTYPECODE, CMST.PURCHASEINVTYPECODE, INVMST.HSN_CODE" & vbCrLf _
            & " FROM FIN_SUPP_CUST_DET ID, INV_ITEM_MST INVMST , INV_GENERAL_MST CMST" & vbCrLf _
            & " WHERE ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND INVMST.COMPANY_CODE=CMST.COMPANY_CODE AND INVMST.CATEGORY_CODE=CMST.GEN_CODE" & vbCrLf _
            & " AND ID.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
            & " AND INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND INVMST.ITEM_CODE='" & Trim(mItemCode) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc

                SprdMain.Col = ColItemName
                SprdMain.Text = IIf(IsDBNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)

                SprdMain.Col = ColHSNCode


                If SprdMain.Text = "" Then
                    If VB.Left(cboInvType.Text, 1) = "G" Then
                        mHSNCode = GetHSNCode(mItemCode) 'IIf(IsNull(!HSN_CODE), "", !HSN_CODE)
                    Else
                        mHSNCode = GetSACCode((txtServProvided.Text))
                    End If
                    SprdMain.Text = mHSNCode
                End If

                mHSNCode = SprdMain.Text

                SprdMain.Col = ColItemUOM
                SprdMain.Text = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)

                SprdMain.Col = ColPartNo
                mPartNo = IIf(IsDBNull(.Fields("CUSTOMER_ITEM_NO").Value), "", .Fields("CUSTOMER_ITEM_NO").Value)
                If mPartNo = "" Then
                    mPartNo = IIf(IsDBNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)
                End If
                SprdMain.Text = mPartNo

                'SprdMain.Col = ColColor
                'SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_COLOR").Value), "", .Fields("ITEM_COLOR").Value)

                SprdMain.Col = ColPreviousItemRate
                SprdMain.Text = CStr(GetPreviousRate((txtCode.Text), Val(txtSONo.Text), Val(txtAmendNo.Text), mItemCode))


                SprdMain.Col = ColItemRate
                If Val(SprdMain.Text) = 0 Then
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE").Value), "", .Fields("ITEM_RATE").Value)))
                End If

                mSaleInvTypeCode = IIf(IsDBNull(.Fields("SALEINVTYPECODE").Value), "", .Fields("SALEINVTYPECODE").Value)


                If VB.Left(cboInvType.Text, 1) = "G" Then
                    If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, "0", mPartyGSTNo) = False Then GoTo ERR1
                Else
                    If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo) = False Then GoTo ERR1
                End If

                SprdMain.Col = ColCGSTPer
                SprdMain.Text = VB6.Format(pCGSTPer, "0.00")

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = VB6.Format(pSGSTPer, "0.00")

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = VB6.Format(pIGSTPer, "0.00")

                SprdMain.Col = ColAccountName
                If Trim(SprdMain.Text) = "" Then
                    mInvTypeDesc = ""
                    If MainClass.ValidateWithMasterTable(mSaleInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
                        mInvTypeDesc = MasterNo
                    End If

                    SprdMain.Col = ColAccountName
                    SprdMain.Text = Trim(mInvTypeDesc)
                End If


                MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                FormatSprdMain(-1)

            End With
            FillGridRow = True
        Else
            'SqlStr = " Select INVMST.ITEM_CODE,INVMST.ITEM_SHORT_DESC,INVMST.PURCHASE_UOM, INVMST.IDENT_MARK, INVMST.CATEGORY_CODE, CMST.SALEINVTYPECODE, CMST.PURCHASEINVTYPECODE, INVMST.HSN_CODE " & vbCrLf & " FROM INV_ITEM_MST INVMST, INV_GENERAL_MST CMST" & vbCrLf & " WHERE INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INVMST.COMPANY_CODE=CMST.COMPANY_CODE AND INVMST.CATEGORY_CODE=CMST.GEN_CODE" & vbCrLf & " AND INVMST.ITEM_CODE='" & Trim(mItemCode) & "'"

            SqlStr = " Select INVMST.ITEM_CODE,INVMST.ITEM_SHORT_DESC,INVMST.ISSUE_UOM, " & vbCrLf _
            & " 0 AS ITEM_RATE,  0 AS DISC_PER, CUSTOMER_PART_NO,ITEM_COLOR," & vbCrLf _
            & " INVMST.CATEGORY_CODE, CMST.SALEINVTYPECODE, CMST.PURCHASEINVTYPECODE, INVMST.HSN_CODE" & vbCrLf _
            & " FROM INV_ITEM_MST INVMST , INV_GENERAL_MST CMST" & vbCrLf _
            & " WHERE INVMST.COMPANY_CODE=CMST.COMPANY_CODE AND INVMST.CATEGORY_CODE=CMST.GEN_CODE" & vbCrLf _
            & " AND INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND INVMST.ITEM_CODE='" & Trim(mItemCode) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
            If RsMisc.EOF = False Then
                SprdMain.Row = SprdMain.ActiveRow
                With RsMisc

                    SprdMain.Col = ColItemName
                    SprdMain.Text = IIf(IsDBNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)

                    SprdMain.Col = ColHSNCode


                    If SprdMain.Text = "" Then
                        If VB.Left(cboInvType.Text, 1) = "G" Then
                            mHSNCode = GetHSNCode(mItemCode) 'IIf(IsNull(!HSN_CODE), "", !HSN_CODE)
                        Else
                            mHSNCode = GetSACCode((txtServProvided.Text))
                        End If
                        SprdMain.Text = mHSNCode
                    End If

                    mHSNCode = SprdMain.Text

                    SprdMain.Col = ColItemUOM
                    SprdMain.Text = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)

                    SprdMain.Col = ColPartNo
                    SprdMain.Text = IIf(IsDBNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)

                    'SprdMain.Col = ColColor
                    'SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_COLOR").Value), "", .Fields("ITEM_COLOR").Value)

                    SprdMain.Col = ColPreviousItemRate
                    SprdMain.Text = CStr(GetPreviousRate((txtCode.Text), Val(txtSONo.Text), Val(txtAmendNo.Text), mItemCode))


                    SprdMain.Col = ColItemRate
                    If Val(SprdMain.Text) = 0 Then
                        SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE").Value), "", .Fields("ITEM_RATE").Value)))
                    End If

                    mSaleInvTypeCode = IIf(IsDBNull(.Fields("SALEINVTYPECODE").Value), "", .Fields("SALEINVTYPECODE").Value)


                    If VB.Left(cboInvType.Text, 1) = "G" Then
                        If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, "0", mPartyGSTNo) = False Then GoTo ERR1
                    Else
                        If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo) = False Then GoTo ERR1
                    End If

                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = VB6.Format(pCGSTPer, "0.00")

                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = VB6.Format(pSGSTPer, "0.00")

                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = VB6.Format(pIGSTPer, "0.00")

                    SprdMain.Col = ColAccountName
                    If Trim(SprdMain.Text) = "" Then
                        mInvTypeDesc = ""
                        If MainClass.ValidateWithMasterTable(mSaleInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
                            mInvTypeDesc = MasterNo
                        End If

                        SprdMain.Col = ColAccountName
                        SprdMain.Text = Trim(mInvTypeDesc)
                    End If


                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain(-1)

                End With
                FillGridRow = True
            Else
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, pCol)
                FillGridRow = False
            End If
            'MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, pCol)
            'FillGridRow = False
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function
    Private Function UpdateSuppMst(ByRef xSuppCustCode As String) As Boolean
        On Error GoTo UpdateErrPart
        Dim SqlStr As String = ""
        'UpdateSuppCustDet			
        SqlStr = ""
        SqlStr = " INSERT INTO FIN_SUPP_CUST_HDR ( " & vbCrLf _
            & " COMPANY_CODE,  " & vbCrLf _
            & " SUPP_CUST_CODE,  " & vbCrLf _
            & " PAYMENT_CODE,  " & vbCrLf _
            & " DELIVERY,  " & vbCrLf _
            & " EXCISE_OTHERS,  " & vbCrLf _
            & " MODE_DESPATCH,  " & vbCrLf _
            & " INSPECTION,  " & vbCrLf _
            & " PACKING_FORWARDING,  " & vbCrLf _
            & " INSURANCE,  " & vbCrLf _
            & " OTHERS_COND1,  " & vbCrLf _
            & " OTHERS_COND2, " & vbCrLf _
            & " ADDUSER, ADDDATE, MODUSER, MODDATE) VALUES ( "


        SqlStr = SqlStr & vbCrLf & " " & RsCompany.Fields("Company_Code").Value & ", " & vbCrLf _
            & " '" & xSuppCustCode & "', " & vbCrLf _
            & " '', " & vbCrLf _
            & " '', " & vbCrLf _
            & " '', " & vbCrLf _
            & " '', " & vbCrLf _
            & " '', " & vbCrLf _
            & " '', " & vbCrLf _
            & " '', " & vbCrLf _
            & " '', " & vbCrLf _
            & " '', " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','')"

        PubDBCn.Execute(SqlStr)

        UpdateSuppMst = True

        Exit Function
UpdateErrPart:
        MsgBox(Err.Description)
        UpdateSuppMst = False

        ''Resume			
    End Function

    Private Function UpdateSuppCustDet(ByRef xSuppCustCode As String, ByRef mPartNo As String, ByRef xItemCode As String, ByRef xRate As Double, ByRef xDisc As Double, ByRef xType As String) As Boolean

        On Error GoTo UpdateErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = ""
        SqlStr = " SELECT ITEM_CODE FROM FIN_SUPP_CUST_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  " & vbCrLf _
            & " AND SUPP_CUST_CODE='" & xSuppCustCode & "'  " & vbCrLf _
            & " AND ITEM_CODE='" & Trim(xItemCode) & "'  "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenKeyset, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            If xItemCode <> "" And xRate > 0 Then
                SqlStr = " INSERT INTO FIN_SUPP_CUST_DET ( " & vbCrLf & " COMPANY_CODE , SUPP_CUST_CODE, " & vbCrLf & " ITEM_CODE, ITEM_RATE, " & vbCrLf & " DISC_PER, TRN_TYPE,CUSTOMER_ITEM_NO) "
                SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " '" & RsCompany.Fields("COMPANY_CODE").Value & "','" & MainClass.AllowSingleQuote(xSuppCustCode) & "', " & vbCrLf & " '" & xItemCode & "'," & xRate & ", " & vbCrLf & " " & xDisc & ",'" & xType & "','" & mPartNo & "') "

                PubDBCn.Execute(SqlStr)
            End If
        End If

        UpdateSuppCustDet = True

        Exit Function
UpdateErrPart:
        MsgBox(Err.Description)
        UpdateSuppCustDet = False

        ''Resume			
    End Function
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mSONO As String
        Dim mAmendNO As String

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        mSONO = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(0))
        mAmendNO = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(4))

        txtSONo.Text = CStr(Val(mSONO))
        txtAmendNo.Text = CStr(Val(mAmendNO))

        txtSONo_Validating(txtSONo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())

    End Sub
    'Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
    '    SprdView.Col = 1
    '    SprdView.Row = SprdView.ActiveRow
    '    txtSONo.Text = SprdView.Text

    '    txtSONo_Validating(txtSONo, New System.ComponentModel.CancelEventArgs(False))
    '    CmdView_Click(CmdView, New System.EventArgs())
    'End Sub
    'Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent)
    '    If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    'End Sub

    Private Sub txtAmendDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmendDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmendNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBalPayment_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBalPayment.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBalPayment_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBalPayment.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBalPayment.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.DoubleClick
        Call SearchCode()
    End Sub
    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCode()
    End Sub
    Private Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If Trim(txtCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_Name", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            txtCustomerName.Text = MasterNo
            txtCode.Enabled = False
        Else
            MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        Call AutoCompleteSearch("FIN_SUPP_CUST_BUSINESS_MST ", "LOCATION_ID", "SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'", txtBillTo)



        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCommission_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCommission.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCommission_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCommission.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCommission.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub





    Private Sub txtDescDetail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDescDetail.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDescDetail_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDescDetail.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDescDetail.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDespMode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDespMode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDespMode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDespMode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDespMode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDestination_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDestination.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDestination_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDestination.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDestination.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtEPCGDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEPCGDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEPCGDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEPCGDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEPCGDate.Text) = "" Then GoTo EventExitSub

        If IsDate(txtEPCGDate.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtEPCGNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEPCGNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEPCGNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEPCGNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEPCGNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtFreight_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFreight.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFreight_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFreight.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFreight.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtInspection_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspection.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtInspection_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInspection.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInspection.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtInsurance_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInsurance.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtInsurance_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInsurance.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInsurance.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCustomerName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomerName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCustomerName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomerName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtCustomerName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomerName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCustomerName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomerName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtCustomerName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomerName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(txtCustomerName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtCustomerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
            txtCode.Text = xAcctCode
            txtCode.Enabled = False
        Else
            MsgBox("Invalid Customer Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(txtVendorCode.Text) = "" Then
            If MainClass.ValidateWithMasterTable((txtCustomerName.Text), "SUPP_CUST_NAME", "VENDOR_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
                txtVendorCode.Text = MasterNo
            End If
        End If
        txtBillTo.Text = GetDefaultLocation(xAcctCode)
        'txtShipTo.Text = GetDefaultLocation(xAcctCode)

        If txtBillTo.Text <> "" Then
            If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "SUPP_CUST_ADDR || ',' || SUPP_CUST_CITY || ',' || SUPP_CUST_STATE || ',' || ' GST NO :' || GST_RGN_NO", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'") = True Then
                txtAddress.Text = MasterNo
            End If
        Else
            txtAddress.Text = ""
        End If
        Call AutoCompleteSearch("FIN_SUPP_CUST_BUSINESS_MST ", "LOCATION_ID", "SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'", txtBillTo)
        ''Call AutoCompleteSearch("FIN_SUPP_CUST_BUSINESS_MST ", "LOCATION_ID", "SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'", txtShipTo)


        If ADDMode = True Then
            If MsgQuestion("Populate Data From Customer Detail ...") = CStr(MsgBoxResult.Yes) Then
                Call FillItemFromSuppCustDetail()
            End If
            txtPONo.Focus()
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtShipCustomer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShipCustomer.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtShipCustomer_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShipCustomer.DoubleClick
        cmdsearchShipCust_Click(cmdsearchShipCust, New System.EventArgs())
    End Sub
    Private Sub txtShipCustomer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtShipCustomer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtShipCustomer.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtShipCustomer_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtShipCustomer.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearchShipCust_Click(cmdsearchShipCust, New System.EventArgs())
    End Sub
    Private Sub txtShipCustomer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtShipCustomer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(txtShipCustomer.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtShipCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        Else
            MsgBox("Invalid Ship To Customer Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        'txtBillTo.Text = GetDefaultLocation(xAcctCode)
        txtShipTo.Text = GetDefaultLocation(xAcctCode)

        'Call AutoCompleteSearch("FIN_SUPP_CUST_BUSINESS_MST ", "LOCATION_ID", "SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'", txtBillTo)
        Call AutoCompleteSearch("FIN_SUPP_CUST_BUSINESS_MST ", "LOCATION_ID", "SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'", txtShipTo)

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FillItemFromSuppCustDetail()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim xAcctCode As String
        Dim mSaleorderType As String
        Dim mHSNCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mItemCode As String
        Dim mPartNo As String

        MainClass.ClearGrid(SprdMain, ConRowHeight)

        If Trim(txtBillTo.Text) = "" Then Exit Sub

        mLocal = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")

        SqlStr = " SELECT IH.PAYMENT_CODE, IH.DELIVERY, IH.EXCISE_OTHERS, " & vbCrLf _
            & " IH.MODE_DESPATCH, IH.INSPECTION, IH.PACKING_FORWARDING, " & vbCrLf _
            & " IH.INSURANCE, IH.OTHERS_COND1, IH.OTHERS_COND2, " & vbCrLf _
            & " ID.ITEM_CODE,  INVMST.PURCHASE_UOM, INVMST.ITEM_SHORT_DESC, " & vbCrLf _
            & " ID.ITEM_RATE,  ID.DISC_PER,ID.CUSTOMER_ITEM_NO AS CUSTOMER_PART_NO, INVMST.CUSTOMER_PART_NO AS CUSTOMER_PART_NO_ITEM, INVMST.ITEM_COLOR " & vbCrLf _
            & " FROM FIN_SUPP_CUST_HDR IH, FIN_SUPP_CUST_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=ID.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf & " AND TRN_TYPE IN ('S','J') ORDER BY ID.ITEM_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        I = 1
        If RsTemp.EOF = False Then

            txtDespMode.Text = IIf(IsDBNull(RsTemp.Fields("MODE_DESPATCH").Value), "", RsTemp.Fields("MODE_DESPATCH").Value)
            txtInspection.Text = IIf(IsDBNull(RsTemp.Fields("INSPECTION").Value), "", RsTemp.Fields("INSPECTION").Value)
            txtInsurance.Text = IIf(IsDBNull(RsTemp.Fields("INSURANCE").Value), "", RsTemp.Fields("INSURANCE").Value)


            With SprdMain
                Do While Not RsTemp.EOF
                    .Row = I
                    .Col = ColItemCode
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                    mItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                    .Col = ColItemName
                    .Text = IIf(IsDBNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)

                    .Col = ColItemUOM
                    .Text = IIf(IsDBNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)

                    .Col = ColPartNo
                    mPartNo = IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value)

                    If mPartNo = "" Then
                        mPartNo = IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO_ITEM").Value), "", RsTemp.Fields("CUSTOMER_PART_NO_ITEM").Value)
                    End If
                    .Text = mPartNo     ''IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value)

                    '.Col = ColColor
                    '.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_COLOR").Value), "", RsTemp.Fields("ITEM_COLOR").Value)

                    .Col = ColPreviousItemRate
                    .Text = CStr(GetPreviousRate((txtCode.Text), Val(txtSONo.Text), Val(txtAmendNo.Text), Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))))

                    .Col = ColItemRate
                    .Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), "", RsTemp.Fields("ITEM_RATE").Value)))

                    mSaleorderType = IIf(cboInvType.Text = "", "G", VB.Left(cboInvType.Text, 1))

                    If mSaleorderType = "G" Then
                        mHSNCode = GetHSNCode(mItemCode) 'IIf(IsNull(!HSN_CODE), "", !HSN_CODE)
                    Else
                        mHSNCode = GetSACCode(txtServProvided.Text)
                    End If

                    If mSaleorderType = "G" Then
                        If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, "0", mPartyGSTNo) = False Then GoTo ErrPart
                    Else
                        If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo) = False Then GoTo ErrPart
                    End If

                    .Col = ColHSNCode
                    .Text = mHSNCode

                    .Col = ColCGSTPer
                    .Text = pCGSTPer

                    .Col = ColSGSTPer
                    .Text = pSGSTPer

                    .Col = ColIGSTPer
                    .Text = pIGSTPer


                    '                .Col = ColItemDisc
                    '                .Text = Val(IIf(IsNull(RsTemp!DISC_PER), "", RsTemp!DISC_PER))
                    '
                    I = I + 1
                    .MaxRows = I
                    RsTemp.MoveNext()
                Loop
            End With
        End If
        FormatSprdMain(-1)
        Call CalcTots()

        Exit Sub

ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mAccountName As String = ""
        Dim mInvType As String
        Dim mSACCode As String
        Dim mShipToAccountName As String = ""
        Dim mBillToShipToSame As String = ""
        Dim mShipAccountCode As String = ""
        Clear1()
        If Not RsSOMain.EOF Then
            With RsSOMain
                '            lblMkey.text = IIf(IsNull(!AUTO_KEY_SO), "", !AUTO_KEY_SO)
                lblMkey.Text = IIf(IsDBNull(.Fields("mKey").Value), "", .Fields("mKey").Value)
                txtSONo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_SO").Value), "", .Fields("AUTO_KEY_SO").Value)
                txtSODate.Text = VB6.Format(IIf(IsDBNull(.Fields("SO_DATE").Value), "", .Fields("SO_DATE").Value), "DD/MM/YYYY")

                mAccountCode = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), -1, .Fields("SUPP_CUST_CODE").Value)

                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = MasterNo
                End If

                txtCustomerName.Text = mAccountName
                txtCode.Text = Trim(IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value))
                txtCode.Enabled = False
                txtCustomerName.Enabled = False

                mBillToShipToSame = Trim(IIf(IsDBNull(.Fields("SHIPPED_TO_SAMEPARTY").Value), "", .Fields("SHIPPED_TO_SAMEPARTY").Value))

                chkShipTo.CheckState = IIf(mBillToShipToSame = "Y", CheckState.Checked, CheckState.Unchecked)

                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)
                txtShipTo.Text = IIf(IsDBNull(.Fields("SHIP_TO_LOC_ID").Value), "", .Fields("SHIP_TO_LOC_ID").Value)

                If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "SUPP_CUST_ADDR || ',' || SUPP_CUST_CITY || ',' || SUPP_CUST_STATE || ',' || ' GST NO :' || GST_RGN_NO", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'") = True Then
                    txtAddress.Text = MasterNo
                End If

                txtVendorCode.Text = IIf(IsDBNull(.Fields("VENDOR_CODE").Value), "", .Fields("VENDOR_CODE").Value)

                If mBillToShipToSame = "Y" Then
                    txtShipCustomer.Text = mAccountName
                    mShipAccountCode = mAccountCode

                    txtShipCustomer.Enabled = False
                    cmdsearchShipCust.Enabled = False
                    txtShipTo.Enabled = False
                    cmdShipToSearch.Enabled = False
                Else
                    mShipAccountCode = IIf(IsDBNull(.Fields("SHIPPED_TO_PARTY_CODE").Value), -1, .Fields("SHIPPED_TO_PARTY_CODE").Value)
                    If MainClass.ValidateWithMasterTable(mShipAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mShipToAccountName = MasterNo
                    End If

                    txtShipCustomer.Text = mShipToAccountName

                    txtShipCustomer.Enabled = True
                    cmdsearchShipCust.Enabled = True
                    txtShipTo.Enabled = True
                    cmdShipToSearch.Enabled = True
                End If


                txtPONo.Text = IIf(IsDBNull(.Fields("CUST_PO_NO").Value), "", .Fields("CUST_PO_NO").Value)
                txtPODate.Text = VB6.Format(IIf(IsDBNull(.Fields("CUST_PO_DATE").Value), "", .Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")

                txtEPCGNo.Text = IIf(IsDBNull(.Fields("EPCG_NO").Value), "", .Fields("EPCG_NO").Value)
                txtEPCGDate.Text = VB6.Format(IIf(IsDBNull(.Fields("EPCG_DATE").Value), "", .Fields("EPCG_DATE").Value), "DD/MM/YYYY")



                txtCustAmendNo.Text = IIf(IsDBNull(.Fields("CUST_AMEND_NO").Value), "", .Fields("CUST_AMEND_NO").Value)
                txtAmendNo.Text = IIf(IsDBNull(.Fields("AMEND_NO").Value), "", .Fields("AMEND_NO").Value)
                txtAmendDate.Text = VB6.Format(IIf(IsDBNull(.Fields("AMEND_DATE").Value), "", .Fields("AMEND_DATE").Value), "DD/MM/YYYY")
                txtWEF.Text = VB6.Format(IIf(IsDBNull(.Fields("AMEND_WEF_FROM").Value), "", .Fields("AMEND_WEF_FROM").Value), "DD/MM/YYYY")
                cboStatus.SelectedIndex = IIf(.Fields("SO_STATUS").Value = "O", 0, 1)
                cboPOType.SelectedIndex = IIf(.Fields("PO_TYPE").Value = "R", 0, IIf(.Fields("PO_TYPE").Value = "S", 1, 2))
                cboStatus.Enabled = False       ''IIf(PubSuperUser = "U", False, IIf(.Fields("SO_STATUS").Value = "O", True, False))
                cmdAmend.Enabled = IIf(.Fields("SO_STATUS").Value = "C", False, True)

                chkApproved.CheckState = IIf(.Fields("SO_APPROVED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkDI.CheckState = IIf(.Fields("DELIVERY_INSTRUCTION_REQ").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                cboOrderType.SelectedIndex = IIf(.Fields("ORDER_TYPE").Value = "O", 0, 1)
                cboOrderType.Enabled = False

                mSACCode = IIf(IsDBNull(.Fields("SAC_CODE").Value), "", .Fields("SAC_CODE").Value)
                txtServProvided.Text = ""

                If MainClass.ValidateWithMasterTable(mSACCode, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                    txtServProvided.Text = MasterNo
                End If

                mInvType = IIf(IsDBNull(.Fields("GOODS_SERVICE").Value), "", .Fields("GOODS_SERVICE").Value)

                If mInvType = "G" Then
                    cboInvType.SelectedIndex = 0
                ElseIf mInvType = "S" Then
                    cboInvType.SelectedIndex = 1
                End If
                cboInvType.Enabled = False


                txtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtRoadPermit.Text = IIf(IsDBNull(.Fields("ROAD_PERMIT").Value), "", .Fields("ROAD_PERMIT").Value)
                txtSaleType.Text = IIf(IsDBNull(.Fields("TYPE_OF_SALE").Value), "", .Fields("TYPE_OF_SALE").Value)
                txtLCClaim.Text = IIf(IsDBNull(.Fields("LC_CLAIMS").Value), "", .Fields("LC_CLAIMS").Value)
                txtDespMode.Text = IIf(IsDBNull(.Fields("MODE_OF_DELV").Value), "", .Fields("MODE_OF_DELV").Value)
                txtFreight.Text = IIf(IsDBNull(.Fields("FREIGHT_CHARGES").Value), "", .Fields("FREIGHT_CHARGES").Value)
                txtOctroi.Text = IIf(IsDBNull(.Fields("OCTROI_DTL").Value), "", .Fields("OCTROI_DTL").Value)
                txtCommission.Text = IIf(IsDBNull(.Fields("COMM_DTLS").Value), "", .Fields("COMM_DTLS").Value)
                txtInspection.Text = IIf(IsDBNull(.Fields("INSPECTION_DTL").Value), "", .Fields("INSPECTION_DTL").Value)
                txtDestination.Text = IIf(IsDBNull(.Fields("DESTINATION_DTL").Value), "", .Fields("DESTINATION_DTL").Value)
                txtTransporter.Text = IIf(IsDBNull(.Fields("TRANSPORTER_DTL").Value), "", .Fields("TRANSPORTER_DTL").Value)
                txtDescDetail.Text = IIf(IsDBNull(.Fields("DESPATCH_DTL").Value), "", .Fields("DESPATCH_DTL").Value)
                txtInsurance.Text = IIf(IsDBNull(.Fields("INSURANCE_DTL").Value), "", .Fields("INSURANCE_DTL").Value)
                txtPayment.Text = IIf(IsDBNull(.Fields("PAYMENT_DTL").Value), "", .Fields("PAYMENT_DTL").Value)
                txtBalPayment.Text = IIf(IsDBNull(.Fields("BALANCE_PAY_DTL").Value), "", .Fields("BALANCE_PAY_DTL").Value)
                Call ShowDetail1()

                Call AutoCompleteSearch("FIN_SUPP_CUST_BUSINESS_MST ", "LOCATION_ID", "SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'", txtBillTo)
                Call AutoCompleteSearch("FIN_SUPP_CUST_BUSINESS_MST ", "LOCATION_ID", "SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShipAccountCode) & "'", txtShipTo)

            End With
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True
        txtSONo.Enabled = True
        cmdSearchAmend.Enabled = True
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemUOM)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPartNo, ColPartNo)
        MainClass.ButtonStatus(Me, XRIGHT, RsSOMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mAcctCode As String
        Dim mAcctName As String
        Dim mInvTypeCode As String
        Dim mInvTypeDesc As String
        Dim mHSNCode As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM DSP_SALEORDER_DET " & vbCrLf & " Where " & vbCrLf & " MKEY=" & Val(lblMkey.Text) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSODetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsSODetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1
            I = 1
            '        .MoveFirst

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = mItemCode

                SprdMain.Col = ColItemName
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc


                SprdMain.Col = ColItemUOM
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("UOM_CODE").Value), "", .Fields("UOM_CODE").Value))

                SprdMain.Col = ColPartNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("PART_NO").Value), "", .Fields("PART_NO").Value))

                SprdMain.Col = ColItemSNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_SNO").Value), "", .Fields("ITEM_SNO").Value))


                '            mHSNCode = GetHSNCode(mItemCode)

                SprdMain.Col = ColHSNCode
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value))

                SprdMain.Col = ColAddItemDesc
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ADD_ITEM_DESCRIPTION").Value), "", .Fields("ADD_ITEM_DESCRIPTION").Value))

                SprdMain.Col = ColCustStoreLoc
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("CUST_STORE_LOC").Value), "", .Fields("CUST_STORE_LOC").Value))

                SprdMain.Col = ColPreviousItemRate
                SprdMain.Text = CStr(GetPreviousRate((txtCode.Text), Val(txtSONo.Text), Val(txtAmendNo.Text), mItemCode))

                SprdMain.Col = ColItemRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_PRICE").Value), 0, .Fields("ITEM_PRICE").Value)))

                SprdMain.Col = ColMRP
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_MRP").Value), 0, .Fields("ITEM_MRP").Value)))

                SprdMain.Col = ColMTRCOST
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("MATERIAL_COST").Value), 0, .Fields("MATERIAL_COST").Value)))

                SprdMain.Col = ColProcessCost
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("PROCESS_COST").Value), 0, .Fields("PROCESS_COST").Value)))

                SprdMain.Col = ColMSPCost
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("MSP_COST").Value), 0, .Fields("MSP_COST").Value)))

                SprdMain.Col = ColMSPCostAdd
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("MSP_COST_ADD").Value), 0, .Fields("MSP_COST_ADD").Value)))

                SprdMain.Col = ColFreightCost
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("FREIGHT_COST").Value), 0, .Fields("FREIGHT_COST").Value)))

                SprdMain.Col = ColPO_WEF
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("AMEND_WEF").Value), "", .Fields("AMEND_WEF").Value), "DD/MM/YYYY")

                SprdMain.Col = ColValidQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("VALID_QTY").Value), 0, .Fields("VALID_QTY").Value)))

                SprdMain.Col = ColValidDate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("VALID_DATE").Value), "", .Fields("VALID_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColCGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CGST_PER").Value), 0, .Fields("CGST_PER").Value)))

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SGST_PER").Value), 0, .Fields("SGST_PER").Value)))

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)))

                mInvTypeCode = Trim(IIf(IsDBNull(.Fields("ACCOUNT_POSTING_CODE").Value), "", .Fields("ACCOUNT_POSTING_CODE").Value))
                mInvTypeDesc = ""

                If MainClass.ValidateWithMasterTable(mInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = True Then
                    mInvTypeDesc = MasterNo
                End If

                SprdMain.Col = ColAccountName
                SprdMain.Value = mInvTypeDesc

                SprdMain.Col = colRemarks
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value))

                SprdMain.Col = ColSOStatus
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("SO_ITEM_STATUS").Value), "N", .Fields("SO_ITEM_STATUS").Value))


                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Function GetPreviousRate(ByRef pCustomerCode As String, ByRef pSONo As Double, ByRef pAmendNo As Double, ByRef pItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetPreviousRate = 0
        If pAmendNo = 0 Then Exit Function

        SqlStr = ""
        SqlStr = " SELECT ID.ITEM_PRICE " & vbCrLf & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY = ID.MKEY " & vbCrLf & " AND IH.AUTO_KEY_SO = " & pSONo & " " & vbCrLf & " AND IH.AMEND_NO = " & pAmendNo - 1 & "" & vbCrLf & " AND IH.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(pCustomerCode) & "'" & vbCrLf & " AND ID.ITEM_CODE ='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPreviousRate = IIf(IsDBNull(RsTemp.Fields("ITEM_PRICE").Value), 0, RsTemp.Fields("ITEM_PRICE").Value)
        End If

        Exit Function
ErrPart:
        GetPreviousRate = 0
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
    End Function


    Private Sub txtLCClaim_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLCClaim.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLCClaim_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLCClaim.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLCClaim.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtOctroi_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOctroi.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOctroi_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOctroi.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOctroi.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPayment_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPayment.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPayment_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPayment.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPayment.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPODate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPODate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPODate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPODate.Text) = "" Then GoTo EventExitSub

        If IsDate(txtPODate.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPONo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCustAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustAmendNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRoadPermit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRoadPermit.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRoadPermit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRoadPermit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRoadPermit.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSaleType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSaleType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSaleType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSaleType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSearchItem_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchItem.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSearchItem.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtServProvided_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServProvided.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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
        Dim mServCode As String
        Dim mSACCode As String


        If Trim(txtServProvided.Text) = "" Then GoTo EventExitSub


        If MainClass.ValidateWithMasterTable((txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = False Then
            MsgInformation("Please Select Valid Service Provided")
            Cancel = True
            GoTo EventExitSub
        Else
            mServCode = MasterNo
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


    Private Sub txtSODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSODate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSODate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSODate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtSODate.Text) = "" Then GoTo EventExitSub

        If IsDate(txtSODate.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtSONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSONo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtSONo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSONo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mPONo As Double
        Dim SqlStr As String = ""

        If Trim(txtSONo.Text) = "" Then GoTo EventExitSub
        If Len(txtSONo.Text) < 6 Then
            txtSONo.Text = VB6.Format(Val(txtSONo.Text), "00000") & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mPONo = Val(txtSONo.Text)

        If MODIFYMode = True And RsSOMain.BOF = False Then xMkey = RsSOMain.Fields("mKey").Value

        SqlStr = "SELECT * FROM DSP_SALEORDER_HDR " & " WHERE AUTO_KEY_SO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y'" ''& vbCrLf |            & " AND SUBSTR(AUTO_KEY_SO,LENGTH(AUTO_KEY_SO)-5,4)=" & RsCompany.fields("FYEAR").value & ""

        If Trim(txtAmendNo.Text) = "" Then
            SqlStr = SqlStr & " AND AMEND_NO = (" & vbCrLf _
                & " SELECT MAX(AMEND_NO) FROM DSP_SALEORDER_HDR " & " WHERE AUTO_KEY_SO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf _
                & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND AMEND_NO=" & Val(txtAmendNo.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND ORDER_TYPE='" & Trim(lblType.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSOMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsSOMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such SO No. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM DSP_SALEORDER_HDR WHERE MKEY=" & Val(xMkey) & " AND ISGSTENABLE_PO='Y'" ''& vbCrLf |                & " AND SUBSTR(AUTO_KEY_SO,LENGTH(AUTO_KEY_SO)-5,4)=" & RsCompany.fields("FYEAR").value & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSOMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
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

    Private Sub txtTransporter_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTransporter.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTransporter_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTransporter.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTransporter.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub TxtWef_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function GetSearchItem(ByRef mByCode As String) As String
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String

        xSuppCode = IIf(Trim(txtCode.Text) = "", "-1", Trim(txtCode.Text))

        If mByCode = "Y" Then
            mSqlStr = " SELECT A.ITEM_CODE,A.ITEM_SHORT_DESC, "
        Else
            mSqlStr = " SELECT A.ITEM_SHORT_DESC,A.ITEM_CODE, "
        End If

        mSqlStr = mSqlStr & vbCrLf & " A.ISSUE_UOM, A.HSN_CODE, A.CUSTOMER_PART_NO"

        'mSqlStr = mSqlStr & vbCrLf _
        '    & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf _
        '    & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
        '    & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf _
        '    & " AND B.SUPP_CUST_CODE='" & xSuppCode & "'"

        mSqlStr = mSqlStr & vbCrLf & " FROM INV_ITEM_MST A" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        GetSearchItem = mSqlStr
        Exit Function
ErrPart:
        GetSearchItem = ""

    End Function
    Private Function GetValidItem(ByRef pItemCode As String) As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        xSuppCode = IIf(Trim(txtCode.Text) = "", "-1", Trim(txtCode.Text))


        'mSqlStr = "SELECT B.ITEM_CODE " & vbCrLf & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf & " AND B.SUPP_CUST_CODE='" & xSuppCode & "'" & vbCrLf & " AND B.ITEM_CODE LIKE '" & pItemCode & "%'"

        mSqlStr = "SELECT A.ITEM_CODE " & vbCrLf & " FROM INV_ITEM_MST A " & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND A.ITEM_CODE LIKE '" & pItemCode & "%'"

        '& vbCrLf _
        '    & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf & " AND B.SUPP_CUST_CODE='" & xSuppCode & "'" & vbCrLf & " AND B.ITEM_CODE LIKE '" & pItemCode & "%'"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetValidItem = True
        Else
            MsgInformation(pItemCode & ", Item is not defined.")
            GetValidItem = False
        End If

        Exit Function
ErrPart:
        GetValidItem = False
    End Function

    Private Sub TxtWef_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtWEF.Text) = "" Then GoTo EventExitSub

        If IsDate(txtWEF.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
        End If
        If CDate(txtWEF.Text) < CDate(PubGSTApplicableDate) Then
            MsgInformation("WEF Date should be Greater than GST Applicable date.")
            Cancel = True
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBillTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillTo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillTo.DoubleClick
        cmdBillToSearch_Click(cmdBillToSearch, New System.EventArgs())
    End Sub
    Private Sub txtBillTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBillTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBillTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdBillToSearch_Click(cmdBillToSearch, New System.EventArgs())
    End Sub
    Private Sub txtBillTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(txtCode.Text) = "" Then GoTo EventExitSub
        If Trim(txtBillTo.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        Else
            MsgBox("Invalid Customer Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If


        If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "SUPP_CUST_ADDR || SUPP_CUST_CITY || SUPP_CUST_STATE || ' GST NO :' || GST_RGN_NO", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'") = False Then
            MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        Else
            txtAddress.Text = MasterNo
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdBillToSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBillToSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim xAcctCode As String = ""

        If Trim(txtCode.Text) = "" Then
            MsgInformation("Please select the Customer First")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        End If

        SqlStr = "SELECT LOCATION_ID, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE, GST_RGN_NO " & vbCrLf _
                & " FROM FIN_SUPP_CUST_BUSINESS_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'"

        If MainClass.SearchGridMasterBySQL2((txtBillTo.Text), SqlStr) = True Then
            txtBillTo.Text = AcName
            txtAddress.Text = AcName1 & "," & AcName2 & "," & AcName3 & "," & " GST NO : " & AcName4

            txtBillTo_Validating(txtBillTo, New System.ComponentModel.CancelEventArgs(False))
            If txtBillTo.Enabled = True Then txtBillTo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtShipTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShipTo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtShipTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShipTo.DoubleClick
        cmdShipToSearch_Click(cmdShipToSearch, New System.EventArgs())
    End Sub
    Private Sub txtShipTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtShipTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtShipTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtShipTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtShipTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdShipToSearch_Click(cmdShipToSearch, New System.EventArgs())
    End Sub
    Private Sub txtShipTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtShipTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(txtShipCustomer.Text) = "" Then GoTo EventExitSub
        If Trim(txtShipTo.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(txtShipCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        Else
            MsgBox("Invalid Shipped Customer Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(txtShipTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'") = False Then
            MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdShipToSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShipToSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim xAcctCode As String = ""

        If Trim(txtShipCustomer.Text) = "" Then
            MsgInformation("Please select the Customer First")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtShipCustomer.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        End If

        SqlStr = "SELECT LOCATION_ID, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE, GST_RGN_NO " & vbCrLf _
                & " FROM FIN_SUPP_CUST_BUSINESS_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'"

        If MainClass.SearchGridMasterBySQL2((txtShipTo.Text), SqlStr) = True Then
            txtShipTo.Text = AcName
            txtShipTo_Validating(txtShipTo, New System.ComponentModel.CancelEventArgs(False))
            If txtShipTo.Enabled = True Then txtShipTo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub chkShipTo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShipTo.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtShipCustomer.Enabled = False
            cmdsearchShipCust.Enabled = False
            txtShipTo.Enabled = False
            cmdShipToSearch.Enabled = False
        Else
            txtShipCustomer.Enabled = True
            cmdsearchShipCust.Enabled = True
            txtShipTo.Enabled = True
            cmdShipToSearch.Enabled = True
        End If
    End Sub

    Private Sub UltraGrid1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles UltraGrid1.KeyPress
        'If e.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    End Sub

    Private Sub frmSalesOrderGST_KeyUp(sender As Object, EventArgs As KeyEventArgs) Handles Me.KeyUp



        'If EventArgs.KeyCode = System.Windows.Forms.Keys.F2 Then
        '    SprdMain.Row = 1
        '    SprdMain.Row2 = SprdMain.MaxRows
        '    SprdMain.Col = 1
        '    SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
        '    SprdMain.BlockMode = True
        '    SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
        '    SprdMain.BlockMode = False

        '    mSearchKey = ""
        '    cntSearchRow = 1
        '    cntSearchCol = 1
        '    mSearchKey = InputBox("Search :", "Search", mSearchKey)
        '    If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

        '        SprdMain.Row = cntSearchRow
        '        SprdMain.Row2 = cntSearchRow
        '        SprdMain.Col = 1
        '        SprdMain.Col2 = SprdMain.MaxCols
        '        SprdMain.BlockMode = True
        '        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
        '        SprdMain.BlockMode = False

        '        MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColItemRate)
        '        cntSearchRow = cntSearchRow + 1
        '        cntSearchCol = cntSearchCol + 1
        '    End If
        'End If

        If EventArgs.KeyCode = System.Windows.Forms.Keys.F3 And mSearchKey <> "" Then
            If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColItemRate)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub

    Private Sub frmSalesOrderGST_KeyPress(sender As Object, EventArgs As KeyPressEventArgs) Handles Me.KeyPress

        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        'KeyAscii = MainClass.SetNumericField(KeyAscii)
        'EventArgs.KeyChar = Chr(KeyAscii)
        'If KeyAscii = 67 Then
        '    EventArgs.Handled = True
        'End If

        If KeyAscii = 3 Then
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

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColItemRate)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub

    Private Sub SprdMain_KeyPress(sender As Object, EventArgs As KeyPressEventArgs) Handles SprdMain.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        'KeyAscii = MainClass.SetNumericField(KeyAscii)
        'EventArgs.KeyChar = Chr(KeyAscii)
        'If KeyAscii = 67 Then
        '    EventArgs.Handled = True
        'End If

        If KeyAscii = 3 Then
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

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColItemRate)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub
End Class
